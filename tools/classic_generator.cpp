#include <boost/graph/adjacency_list.hpp>
#include <boost/graph/maximum_weighted_matching.hpp>

#include <algorithm>
#include <array>
#include <cerrno>
#include <cmath>
#include <cstdint>
#include <cstring>
#include <fstream>
#include <iostream>
#include <limits>
#include <map>
#include <optional>
#include <random>
#include <set>
#include <sstream>
#include <stdexcept>
#include <string>
#include <tuple>
#include <utility>
#include <vector>

#if !defined(_WIN32)
#include <arpa/inet.h>
#include <sys/socket.h>
#include <sys/stat.h>
#include <sys/un.h>
#include <unistd.h>
#endif

namespace {

constexpr int kMaxOuts = 5;
constexpr int64_t kTenthsPerMillimeter = 10;
constexpr int64_t kTenthsPerMeter = 10000;
constexpr int64_t kDefaultMaxTrimTenthsMm = 2300;
constexpr size_t kRecordWidth = 587;
constexpr size_t kRecordCount = 100;
constexpr size_t kFileTrailerWidth = 340;
constexpr size_t kMaxCsvBytes = 8 * 1024 * 1024;

struct Order {
    size_t inputIndex = 0;
    int64_t webWidth = 0;
    std::string qualityId;
    std::string flute;
    std::string orderNumber;
    std::string customerName;
    std::string customerCode;
    int64_t sheetWidth = 0;
    int64_t sheetLength = 0;
    int64_t quantity = 0;
    int64_t sheetsPerStack = 0;
    std::array<int64_t, 5> scores{};
};

struct Pattern {
    int firstOuts = 0;
    int secondOuts = 0;
    int64_t runMeters = 0;
    int64_t usedWidth = 0;
};

struct Setup {
    Order first;
    std::optional<Order> second;
    Pattern pattern;
};

using GroupKey = std::tuple<int64_t, std::string, std::string>;
using MatchingGraph = boost::adjacency_list<
    boost::vecS,
    boost::vecS,
    boost::undirectedS,
    boost::no_property,
    boost::property<boost::edge_weight_t, int64_t>>;

std::string trim(const std::string& value) {
    const size_t first = value.find_first_not_of(" \t\r\n");
    if (first == std::string::npos) {
        return "";
    }
    const size_t last = value.find_last_not_of(" \t\r\n");
    return value.substr(first, last - first + 1);
}

std::vector<std::string> parseCsvLine(const std::string& line) {
    std::vector<std::string> fields;
    std::string field;
    bool quoted = false;

    for (size_t index = 0; index < line.size(); ++index) {
        const char character = line[index];
        if (quoted) {
            if (character == '"') {
                if (index + 1 < line.size() && line[index + 1] == '"') {
                    field.push_back('"');
                    ++index;
                } else {
                    quoted = false;
                }
            } else {
                field.push_back(character);
            }
        } else if (character == ',' ) {
            fields.push_back(field);
            field.clear();
        } else if (character == '"') {
            if (!field.empty()) {
                throw std::runtime_error("Unexpected quote in CSV field");
            }
            quoted = true;
        } else {
            field.push_back(character);
        }
    }

    if (quoted) {
        throw std::runtime_error("CSV quoted fields cannot span multiple lines");
    }
    fields.push_back(field);
    return fields;
}

size_t utf16Length(const std::string& value) {
    size_t units = 0;
    for (size_t index = 0; index < value.size();) {
        const unsigned char lead = static_cast<unsigned char>(value[index]);
        size_t bytes = 0;
        uint32_t codepoint = 0;

        if (lead <= 0x7f) {
            bytes = 1;
            codepoint = lead;
        } else if (lead >= 0xc2 && lead <= 0xdf) {
            bytes = 2;
            codepoint = lead & 0x1f;
        } else if (lead >= 0xe0 && lead <= 0xef) {
            bytes = 3;
            codepoint = lead & 0x0f;
        } else if (lead >= 0xf0 && lead <= 0xf4) {
            bytes = 4;
            codepoint = lead & 0x07;
        } else {
            throw std::runtime_error("CSV contains invalid UTF-8");
        }

        if (index + bytes > value.size()) {
            throw std::runtime_error("CSV contains incomplete UTF-8");
        }
        for (size_t offset = 1; offset < bytes; ++offset) {
            const unsigned char continuation = static_cast<unsigned char>(value[index + offset]);
            if ((continuation & 0xc0) != 0x80) {
                throw std::runtime_error("CSV contains invalid UTF-8");
            }
            codepoint = (codepoint << 6) | (continuation & 0x3f);
        }

        if ((bytes == 2 && codepoint < 0x80) ||
            (bytes == 3 && codepoint < 0x800) ||
            (bytes == 4 && codepoint < 0x10000) ||
            codepoint > 0x10ffff ||
            (codepoint >= 0xd800 && codepoint <= 0xdfff)) {
            throw std::runtime_error("CSV contains invalid UTF-8 codepoint");
        }

        units += codepoint > 0xffff ? 2 : 1;
        index += bytes;
    }
    return units;
}

void appendFixed(std::string& record, const std::string& value, size_t width, const std::string& name) {
    const size_t valueWidth = utf16Length(value);
    if (valueWidth > width) {
        throw std::runtime_error(name + " exceeds fixed-width field size " + std::to_string(width));
    }
    record += value;
    record.append(width - valueWidth, ' ');
}

int64_t parseInteger(const std::string& value, const std::string& name, bool allowZero = false) {
    const std::string cleaned = trim(value);
    if (cleaned.empty()) {
        throw std::runtime_error(name + " is required");
    }

    size_t parsed = 0;
    const long long number = std::stoll(cleaned, &parsed);
    if (parsed != cleaned.size() || number < (allowZero ? 0 : 1)) {
        throw std::runtime_error(name + " must be " + (allowZero ? "a non-negative integer" : "a positive integer"));
    }
    return number;
}

int64_t parseMillimeters(const std::string& value, const std::string& name, bool allowZero = false) {
    const std::string cleaned = trim(value);
    if (cleaned.empty()) {
        throw std::runtime_error(name + " is required");
    }

    size_t parsed = 0;
    const long double millimeters = std::stold(cleaned, &parsed);
    if (parsed != cleaned.size() || !std::isfinite(static_cast<double>(millimeters)) || millimeters < 0) {
        throw std::runtime_error(name + " must be a non-negative number of millimeters");
    }
    if (!allowZero && millimeters == 0) {
        throw std::runtime_error(name + " must be greater than zero");
    }

    const long double raw = millimeters * kTenthsPerMillimeter;
    const long long rounded = std::llround(raw);
    if (std::fabs(raw - rounded) > 0.000001L) {
        throw std::runtime_error(name + " supports at most one decimal place");
    }
    return rounded;
}

int64_t requiredRunMeters(const Order& order, int outs) {
    const int64_t numerator = order.quantity * order.sheetLength;
    const int64_t denominator = static_cast<int64_t>(outs) * kTenthsPerMeter;
    return (numerator + denominator - 1) / denominator;
}

std::optional<Pattern> optimizePattern(
    const Order& first,
    const Order* second,
    int64_t maxTrimTenthsMm) {
    Pattern best;
    best.runMeters = std::numeric_limits<int64_t>::max();
    bool found = false;

    for (int firstOuts = 1; firstOuts <= kMaxOuts; ++firstOuts) {
        for (int secondOuts = second ? 1 : 0; secondOuts <= (second ? kMaxOuts : 0); ++secondOuts) {
            const int64_t usedWidth = first.sheetWidth * firstOuts +
                (second ? second->sheetWidth * secondOuts : 0);
            const int64_t trimWidth = first.webWidth - usedWidth;
            if (trimWidth < 0 || trimWidth > maxTrimTenthsMm) {
                continue;
            }

            const int64_t firstRun = requiredRunMeters(first, firstOuts);
            const int64_t secondRun = second ? requiredRunMeters(*second, secondOuts) : 0;
            const int64_t runMeters = std::max(firstRun, secondRun);
            if (!found || runMeters < best.runMeters ||
                (runMeters == best.runMeters && usedWidth > best.usedWidth) ||
                (runMeters == best.runMeters && usedWidth == best.usedWidth &&
                    std::pair<int, int>(firstOuts, secondOuts) > std::pair<int, int>(best.firstOuts, best.secondOuts))) {
                best = { firstOuts, secondOuts, runMeters, usedWidth };
                found = true;
            }
        }
    }

    return found ? std::optional<Pattern>(best) : std::nullopt;
}

std::vector<std::string> requiredHeaders() {
    return {
        "web_width_mm", "quality_id", "flute", "order_number", "customer_name",
        "customer_code", "sheet_width_mm", "sheet_length_mm", "quantity_sheets",
        "sheets_per_stack", "score1", "score2", "score3", "score4", "score5"
    };
}

std::vector<Order> readOrders(std::istream& input) {
    std::string line;
    if (!std::getline(input, line)) {
        throw std::runtime_error("Input CSV is empty");
    }
    std::vector<std::string> headers = parseCsvLine(line);
    if (!headers.empty() && headers[0].size() >= 3 &&
        static_cast<unsigned char>(headers[0][0]) == 0xef &&
        static_cast<unsigned char>(headers[0][1]) == 0xbb &&
        static_cast<unsigned char>(headers[0][2]) == 0xbf) {
        headers[0].erase(0, 3);
    }

    std::map<std::string, size_t> columns;
    for (size_t index = 0; index < headers.size(); ++index) {
        const std::string name = trim(headers[index]);
        if (!columns.emplace(name, index).second) {
            throw std::runtime_error("Duplicate CSV column: " + name);
        }
    }
    for (const std::string& required : requiredHeaders()) {
        if (columns.find(required) == columns.end()) {
            throw std::runtime_error("Missing CSV column: " + required);
        }
    }

    auto get = [&](const std::vector<std::string>& fields, const std::string& name) -> std::string {
        const auto column = columns.find(name);
        return column == columns.end() || column->second >= fields.size() ? "" : fields[column->second];
    };

    std::vector<Order> orders;
    size_t lineNumber = 1;
    while (std::getline(input, line)) {
        ++lineNumber;
        if (trim(line).empty()) {
            continue;
        }
        try {
            const std::vector<std::string> fields = parseCsvLine(line);
            if (fields.size() != headers.size()) {
                throw std::runtime_error("CSV column count does not match header");
            }

            Order order;
            order.inputIndex = orders.size();
            order.webWidth = parseMillimeters(get(fields, "web_width_mm"), "web_width_mm");
            order.qualityId = trim(get(fields, "quality_id"));
            order.flute = trim(get(fields, "flute"));
            order.orderNumber = trim(get(fields, "order_number"));
            order.customerName = trim(get(fields, "customer_name"));
            order.customerCode = trim(get(fields, "customer_code"));
            order.sheetWidth = parseMillimeters(get(fields, "sheet_width_mm"), "sheet_width_mm");
            order.sheetLength = parseMillimeters(get(fields, "sheet_length_mm"), "sheet_length_mm");
            order.quantity = parseInteger(get(fields, "quantity_sheets"), "quantity_sheets");
            order.sheetsPerStack = parseInteger(get(fields, "sheets_per_stack"), "sheets_per_stack");

            if (order.qualityId.empty() || order.flute.empty() || order.orderNumber.empty()) {
                throw std::runtime_error("quality_id, flute, and order_number cannot be empty");
            }
            for (size_t index = 0; index < order.scores.size(); ++index) {
                const std::string column = "score" + std::to_string(index + 1);
                const std::string value = get(fields, column);
                order.scores[index] = value.empty() ? 0 : parseMillimeters(value, column, true);
            }

            if (order.sheetWidth > order.webWidth) {
                throw std::runtime_error("sheet_width_mm exceeds web_width_mm");
            }
            if (order.orderNumber.size() > 30 || order.customerName.size() > 15 || order.customerCode.size() > 12) {
                throw std::runtime_error("order/customer text exceeds classic DAT field size");
            }
            orders.push_back(std::move(order));
        } catch (const std::exception& error) {
            throw std::runtime_error("CSV line " + std::to_string(lineNumber) + ": " + error.what());
        }
    }

    if (orders.empty()) {
        throw std::runtime_error("Input CSV contains no orders");
    }
    return orders;
}

std::vector<Setup> optimizeGroup(const std::vector<Order>& orders, int64_t maxTrimTenthsMm) {
    const size_t count = orders.size();
    if (count > kRecordCount * 2) {
        throw std::runtime_error("A compatible order group exceeds the 100-setup DAT capacity");
    }

    std::vector<std::optional<Pattern>> singles(count);
    int64_t maxRunMeters = 0;
    for (size_t index = 0; index < count; ++index) {
        singles[index] = optimizePattern(orders[index], nullptr, maxTrimTenthsMm);
        maxRunMeters = std::max(maxRunMeters, requiredRunMeters(orders[index], 1));
    }

    MatchingGraph graph(count);
    std::map<std::pair<size_t, size_t>, Pattern> pairPatterns;
    const int64_t tieScale = static_cast<int64_t>(count) + 1;
    const int64_t unmatchedPenalty = (maxRunMeters + 1) * tieScale;
    for (size_t first = 0; first < count; ++first) {
        for (size_t second = first + 1; second < count; ++second) {
            const auto pattern = optimizePattern(orders[first], &orders[second], maxTrimTenthsMm);
            if (!pattern) {
                continue;
            }
            const int64_t firstUnmatchedCost = singles[first] ? singles[first]->runMeters : unmatchedPenalty;
            const int64_t secondUnmatchedCost = singles[second] ? singles[second]->runMeters : unmatchedPenalty;
            const int64_t runSavings = firstUnmatchedCost + secondUnmatchedCost - pattern->runMeters;
            if (runSavings < 0) {
                continue;
            }

            const int64_t weight = runSavings * tieScale + 1;
            const auto edge = boost::add_edge(first, second, graph);
            boost::put(boost::edge_weight, graph, edge.first, weight);
            pairPatterns.emplace(std::make_pair(first, second), *pattern);
        }
    }

    std::vector<Setup> setups;
    std::vector<bool> matched(count, false);
    if (count > 1) {
        using Vertex = boost::graph_traits<MatchingGraph>::vertex_descriptor;
        std::vector<Vertex> mate(count, boost::graph_traits<MatchingGraph>::null_vertex());
        const auto mateMap = boost::make_iterator_property_map(mate.begin(), boost::get(boost::vertex_index, graph));
        boost::maximum_weighted_matching(graph, mateMap);

        for (size_t first = 0; first < count; ++first) {
            const Vertex partner = mate[first];
            if (partner == boost::graph_traits<MatchingGraph>::null_vertex() || first >= partner) {
                continue;
            }
            const size_t second = static_cast<size_t>(partner);
            const auto pair = pairPatterns.find(std::make_pair(first, second));
            if (pair == pairPatterns.end()) {
                throw std::runtime_error("Matching selected a missing pair pattern");
            }
            setups.push_back({ orders[first], orders[second], pair->second });
            matched[first] = true;
            matched[second] = true;
        }
    }

    for (size_t index = 0; index < count; ++index) {
        if (!matched[index]) {
            if (!singles[index]) {
                throw std::runtime_error("No feasible single-order cut or compatible pair for order " + orders[index].orderNumber);
            }
            setups.push_back({ orders[index], std::nullopt, *singles[index] });
        }
    }

    std::sort(setups.begin(), setups.end(), [](const Setup& left, const Setup& right) {
        const size_t leftIndex = left.second ? std::min(left.first.inputIndex, left.second->inputIndex) : left.first.inputIndex;
        const size_t rightIndex = right.second ? std::min(right.first.inputIndex, right.second->inputIndex) : right.first.inputIndex;
        return leftIndex < rightIndex;
    });
    return setups;
}

void appendOrder(std::string& record, const Order* order, int outs, int64_t runMeters, const std::string& fixedField) {
    if (!order) {
        appendFixed(record, "", 15, "customer name");
        appendFixed(record, "", 12, "customer code");
        appendFixed(record, "", 30, "order number");
        appendFixed(record, "", 5, "sheet width");
        for (int score = 0; score < 5; ++score) appendFixed(record, "", 5, "score");
        appendFixed(record, "", 4, "sheets per stack");
        appendFixed(record, "", 5, "sheet length");
        appendFixed(record, "", 6, "quantity sheets");
        appendFixed(record, "", 1, "outs");
        appendFixed(record, "", 8, "linear meters");
        appendFixed(record, fixedField, 2, "fixed field");
        return;
    }

    appendFixed(record, order->customerName, 15, "customer name");
    appendFixed(record, order->customerCode, 12, "customer code");
    appendFixed(record, order->orderNumber, 30, "order number");
    appendFixed(record, std::to_string(order->sheetWidth), 5, "sheet width");
    for (int64_t score : order->scores) appendFixed(record, std::to_string(score), 5, "score");
    appendFixed(record, std::to_string(order->sheetsPerStack), 4, "sheets per stack");
    appendFixed(record, std::to_string(order->sheetLength), 5, "sheet length");
    appendFixed(record, std::to_string(order->quantity), 6, "quantity sheets");
    appendFixed(record, std::to_string(outs), 1, "outs");
    appendFixed(record, std::to_string(runMeters), 8, "linear meters");
    appendFixed(record, fixedField, 2, "fixed field");
}

std::string makeRecord(const Setup& setup, size_t setupNumber, int64_t setupId) {
    std::string record;
    appendFixed(record, std::to_string(setupNumber), 11, "program number");
    appendFixed(record, "", 44, "scheduled papers");
    appendFixed(record, std::to_string(setupId), 12, "Fosber ID");
    appendFixed(record, setup.first.qualityId, 36, "quality ID");
    appendFixed(record, setup.first.flute, 23, "flute");
    appendFixed(record, std::to_string(setup.first.webWidth), 5, "web width");
    appendFixed(record, std::to_string(setup.pattern.runMeters), 10, "linear meters");
    appendFixed(record, "G", 1, "G");

    appendOrder(record, &setup.first, setup.pattern.firstOuts, setup.pattern.runMeters, "20");
    appendOrder(record,
        setup.second ? &*setup.second : nullptr,
        setup.pattern.secondOuts,
        setup.pattern.runMeters,
        "10");

    appendFixed(record, "", 57, "unmapped field 38");
    appendFixed(record, "0    0    0    0    0    0    0   0", 56, "unmapped field 39");
    appendFixed(record, "", 8, "unmapped field 40");
    appendFixed(record, "", 8, "unmapped field 41");
    record.append(90, ' ');

    if (utf16Length(record) != kRecordWidth) {
        throw std::runtime_error("Internal error: generated DAT slot is not 587 characters");
    }
    return record;
}

std::vector<Setup> optimize(const std::vector<Order>& orders, int64_t maxTrimTenthsMm) {
    std::map<GroupKey, std::vector<Order>> groups;
    for (const Order& order : orders) {
        groups[GroupKey(order.webWidth, order.qualityId, order.flute)].push_back(order);
    }

    std::vector<Setup> setups;
    for (const auto& group : groups) {
        std::vector<Setup> part = optimizeGroup(group.second, maxTrimTenthsMm);
        setups.insert(setups.end(), part.begin(), part.end());
    }

    if (setups.size() > kRecordCount) {
        throw std::runtime_error("Classic DAT supports at most 100 setups per file; split the CSV input");
    }
    return setups;
}

void writeDat(const std::vector<Setup>& setups, std::ostream& output) {
    std::random_device entropy;
    std::mt19937_64 random(entropy());
    std::uniform_int_distribution<int64_t> setupIdRange(1, std::numeric_limits<int32_t>::max());
    std::set<int64_t> setupIds;

    for (size_t index = 0; index < kRecordCount; ++index) {
        if (index < setups.size()) {
            int64_t setupId;
            do {
                setupId = setupIdRange(random);
            } while (!setupIds.insert(setupId).second);
            output << makeRecord(setups[index], index + 1, setupId);
        } else {
            output << std::string(kRecordWidth, ' ');
        }
    }
    output << std::string(kFileTrailerWidth, ' ');
    if (!output) {
        throw std::runtime_error("Failed while writing generated DAT");
    }
}

std::string generateDat(const std::string& csv, int64_t maxTrimTenthsMm) {
    std::istringstream input(csv);
    const std::vector<Order> orders = readOrders(input);
    const std::vector<Setup> setups = optimize(orders, maxTrimTenthsMm);
    std::ostringstream output(std::ios::out | std::ios::binary);
    writeDat(setups, output);
    return output.str();
}

void runSelfTest() {
    Order first;
    first.webWidth = 23000;
    first.sheetWidth = 5500;
    first.sheetLength = 10350;
    first.quantity = 933;
    first.sheetsPerStack = 100;

    Order second;
    second.webWidth = 23000;
    second.sheetWidth = 6000;
    second.sheetLength = 16350;
    second.quantity = 196;
    second.sheetsPerStack = 100;

    const auto pattern = optimizePattern(first, &second, kDefaultMaxTrimTenthsMm);
    if (!pattern || pattern->firstOuts != 3 || pattern->secondOuts != 1 || pattern->runMeters != 322) {
        throw std::runtime_error("Self-test failed for fixture setup 3712");
    }
    const std::vector<Setup> setups = optimizeGroup({ first, second }, kDefaultMaxTrimTenthsMm);
    if (setups.size() != 1 || setups[0].pattern.firstOuts != 3 ||
        setups[0].pattern.secondOuts != 1 || setups[0].pattern.runMeters != 322) {
        throw std::runtime_error("Self-test failed to pair individually infeasible setup 3712 orders");
    }

    Order wideFirst = first;
    wideFirst.webWidth = 25000;
    wideFirst.sheetWidth = 5400;
    wideFirst.quantity = 1000;
    wideFirst.sheetLength = 10000;
    Order wideSecond = wideFirst;
    wideSecond.sheetWidth = 7400;
    const auto widePair = optimizeGroup({ wideFirst, wideSecond }, kDefaultMaxTrimTenthsMm);
    if (optimizePattern(wideFirst, nullptr, kDefaultMaxTrimTenthsMm) ||
        optimizePattern(wideSecond, nullptr, kDefaultMaxTrimTenthsMm) ||
        widePair.size() != 1 || widePair[0].pattern.firstOuts != 3 ||
        widePair[0].pattern.secondOuts != 1) {
        throw std::runtime_error("Self-test failed to force-pair two single-order-infeasible inputs");
    }
    std::cout << "classic optimizer self-test passed\n";
}

#if !defined(_WIN32)
void sendAll(int client, const void* data, size_t size) {
    const auto* bytes = static_cast<const char*>(data);
    size_t sent = 0;
    while (sent < size) {
        const ssize_t count = ::send(client, bytes + sent, size - sent, MSG_NOSIGNAL);
        if (count < 0 && errno == EINTR) {
            continue;
        }
        if (count <= 0) {
            throw std::runtime_error("Socket write failed");
        }
        sent += static_cast<size_t>(count);
    }
}

void sendFrame(int client, uint8_t status, const std::string& payload) {
    if (payload.size() > std::numeric_limits<uint32_t>::max()) {
        throw std::runtime_error("Socket response is too large");
    }
    const uint32_t networkLength = htonl(static_cast<uint32_t>(payload.size()));
    std::array<unsigned char, 5> header{};
    header[0] = status;
    std::memcpy(header.data() + 1, &networkLength, sizeof(networkLength));
    sendAll(client, header.data(), header.size());
    sendAll(client, payload.data(), payload.size());
}

std::string receiveRequest(int client) {
    std::string request;
    std::array<char, 8192> buffer{};
    while (true) {
        const ssize_t count = ::recv(client, buffer.data(), buffer.size(), 0);
        if (count < 0 && errno == EINTR) {
            continue;
        }
        if (count < 0) {
            throw std::runtime_error("Socket read failed");
        }
        if (count == 0) {
            break;
        }
        if (request.size() + static_cast<size_t>(count) > kMaxCsvBytes + 32) {
            throw std::runtime_error("CSV request exceeds 8 MiB");
        }
        request.append(buffer.data(), static_cast<size_t>(count));
    }
    return request;
}

void serveClient(int client) {
    try {
        const std::string request = receiveRequest(client);
        const size_t separator = request.find('\n');
        if (separator == std::string::npos) {
            throw std::runtime_error("Request is missing max-trim header");
        }
        const std::string trimHeader = request.substr(0, separator);
        size_t parsed = 0;
        const long long maxTrimTenthsMm = std::stoll(trimHeader, &parsed);
        if (parsed != trimHeader.size() || maxTrimTenthsMm < 0) {
            throw std::runtime_error("Invalid max-trim header");
        }
        const std::string csv = request.substr(separator + 1);
        sendFrame(client, 0, generateDat(csv, maxTrimTenthsMm));
    } catch (const std::exception& error) {
        try {
            sendFrame(client, 1, error.what());
        } catch (...) {
        }
    }
    ::close(client);
}

void runSocketServer(const std::string& socketPath) {
    if (socketPath.empty() || socketPath.size() >= sizeof(sockaddr_un::sun_path)) {
        throw std::runtime_error("Unix socket path is empty or too long");
    }

    const int server = ::socket(AF_UNIX, SOCK_STREAM, 0);
    if (server < 0) {
        throw std::runtime_error("Cannot create Unix socket");
    }

    sockaddr_un address{};
    address.sun_family = AF_UNIX;
    std::memcpy(address.sun_path, socketPath.c_str(), socketPath.size() + 1);
    ::unlink(socketPath.c_str());

    if (::bind(server, reinterpret_cast<sockaddr*>(&address), sizeof(address)) < 0) {
        ::close(server);
        throw std::runtime_error("Cannot bind Unix socket: " + std::string(std::strerror(errno)));
    }
    if (::chmod(socketPath.c_str(), 0660) < 0 || ::listen(server, 16) < 0) {
        const std::string message = std::strerror(errno);
        ::close(server);
        ::unlink(socketPath.c_str());
        throw std::runtime_error("Cannot prepare Unix socket: " + message);
    }

    std::cout << "classic-generator listening on " << socketPath << std::endl;
    while (true) {
        const int client = ::accept(server, nullptr, nullptr);
        if (client < 0 && errno == EINTR) {
            continue;
        }
        if (client < 0) {
            const std::string message = std::strerror(errno);
            ::close(server);
            ::unlink(socketPath.c_str());
            throw std::runtime_error("Unix socket accept failed: " + message);
        }
        serveClient(client);
    }
}
#endif

} // namespace

int main(int argc, char** argv) {
    try {
        if (argc == 2 && std::string(argv[1]) == "--self-test") {
            runSelfTest();
            return 0;
        }
#if !defined(_WIN32)
        if (argc == 3 && std::string(argv[1]) == "--socket") {
            runSocketServer(argv[2]);
            return 0;
        }
#endif
        if (argc < 3 || argc > 4) {
            std::cerr << "Usage: classic-generator <orders.csv> <output.dat> [max-trim-mm]\n"
#if !defined(_WIN32)
                      << "       classic-generator --socket <unix-socket-path>\n"
#endif
                ;
            return 2;
        }

        std::ifstream input(argv[1], std::ios::binary);
        if (!input) {
            throw std::runtime_error("Cannot open input CSV: " + std::string(argv[1]));
        }
        const int64_t maxTrim = argc == 4
            ? parseMillimeters(argv[3], "max-trim-mm", true)
            : kDefaultMaxTrimTenthsMm;
        const std::vector<Order> orders = readOrders(input);
        const std::vector<Setup> setups = optimize(orders, maxTrim);
        std::ofstream output(argv[2], std::ios::binary);
        if (!output) {
            throw std::runtime_error("Cannot open output DAT: " + std::string(argv[2]));
        }
        writeDat(setups, output);

        std::cout << "Wrote " << setups.size() << " setups to " << argv[2] << "\n";
        return 0;
    } catch (const std::exception& error) {
        std::cerr << "classic-generator: " << error.what() << "\n";
        return 1;
    }
}
