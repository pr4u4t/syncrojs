#include "vbcompat.h"

#include <QHash>

#include <algorithm>
#include <cmath>
#include <limits>

namespace vb {
namespace {

// 0x80-0x9F of Windows-1252; undefined positions map to the C1 control of the same value.
const char16_t kHighTable[32] = {
    0x20AC, 0x0081, 0x201A, 0x0192, 0x201E, 0x2026, 0x2020, 0x2021, 0x02C6, 0x2030, 0x0160, 0x2039, 0x0152, 0x008D, 0x017D, 0x008F,
    0x0090, 0x2018, 0x2019, 0x201C, 0x201D, 0x2022, 0x2013, 0x2014, 0x02DC, 0x2122, 0x0161, 0x203A, 0x0153, 0x009D, 0x017E, 0x0178,
};

const char16_t kCp850[128] = {
    0x00C7, 0x00FC, 0x00E9, 0x00E2, 0x00E4, 0x00E0, 0x00E5, 0x00E7, 0x00EA, 0x00EB, 0x00E8, 0x00EF, 0x00EE, 0x00EC, 0x00C4, 0x00C5,
    0x00C9, 0x00E6, 0x00C6, 0x00F4, 0x00F6, 0x00F2, 0x00FB, 0x00F9, 0x00FF, 0x00D6, 0x00DC, 0x00F8, 0x00A3, 0x00D8, 0x00D7, 0x0192,
    0x00E1, 0x00ED, 0x00F3, 0x00FA, 0x00F1, 0x00D1, 0x00AA, 0x00BA, 0x00BF, 0x00AE, 0x00AC, 0x00BD, 0x00BC, 0x00A1, 0x00AB, 0x00BB,
    0x2591, 0x2592, 0x2593, 0x2502, 0x2524, 0x00C1, 0x00C2, 0x00C0, 0x00A9, 0x2563, 0x2551, 0x2557, 0x255D, 0x00A2, 0x00A5, 0x2510,
    0x2514, 0x2534, 0x252C, 0x251C, 0x2500, 0x253C, 0x00E3, 0x00C3, 0x255A, 0x2554, 0x2569, 0x2566, 0x2560, 0x2550, 0x256C, 0x00A4,
    0x00F0, 0x00D0, 0x00CA, 0x00CB, 0x00C8, 0x0131, 0x00CD, 0x00CE, 0x00CF, 0x2518, 0x250C, 0x2588, 0x2584, 0x00A6, 0x00CC, 0x2580,
    0x00D3, 0x00DF, 0x00D4, 0x00D2, 0x00F5, 0x00D5, 0x00B5, 0x00FE, 0x00DE, 0x00DA, 0x00DB, 0x00D9, 0x00FD, 0x00DD, 0x00AF, 0x00B4,
    0x00AD, 0x00B1, 0x2017, 0x00BE, 0x00B6, 0x00A7, 0x00F7, 0x00B8, 0x00B0, 0x00A8, 0x00B7, 0x00B9, 0x00B3, 0x00B2, 0x25A0, 0x00A0,
};

const char16_t kCp866[128] = {
    0x0410, 0x0411, 0x0412, 0x0413, 0x0414, 0x0415, 0x0416, 0x0417, 0x0418, 0x0419, 0x041A, 0x041B, 0x041C, 0x041D, 0x041E, 0x041F,
    0x0420, 0x0421, 0x0422, 0x0423, 0x0424, 0x0425, 0x0426, 0x0427, 0x0428, 0x0429, 0x042A, 0x042B, 0x042C, 0x042D, 0x042E, 0x042F,
    0x0430, 0x0431, 0x0432, 0x0433, 0x0434, 0x0435, 0x0436, 0x0437, 0x0438, 0x0439, 0x043A, 0x043B, 0x043C, 0x043D, 0x043E, 0x043F,
    0x2591, 0x2592, 0x2593, 0x2502, 0x2524, 0x2561, 0x2562, 0x2556, 0x2555, 0x2563, 0x2551, 0x2557, 0x255D, 0x255C, 0x255B, 0x2510,
    0x2514, 0x2534, 0x252C, 0x251C, 0x2500, 0x253C, 0x255E, 0x255F, 0x255A, 0x2554, 0x2569, 0x2566, 0x2560, 0x2550, 0x256C, 0x2567,
    0x2568, 0x2564, 0x2565, 0x2559, 0x2558, 0x2552, 0x2553, 0x256B, 0x256A, 0x2518, 0x250C, 0x2588, 0x2584, 0x258C, 0x2590, 0x2580,
    0x0440, 0x0441, 0x0442, 0x0443, 0x0444, 0x0445, 0x0446, 0x0447, 0x0448, 0x0449, 0x044A, 0x044B, 0x044C, 0x044D, 0x044E, 0x044F,
    0x0401, 0x0451, 0x0404, 0x0454, 0x0407, 0x0457, 0x040E, 0x045E, 0x00B0, 0x2219, 0x00B7, 0x221A, 0x2116, 0x00A4, 0x25A0, 0x00A0,
};

const QHash<char16_t, char> &reverseHighTable()
{
    static const QHash<char16_t, char> table = [] {
        QHash<char16_t, char> result;
        for (int index = 0; index < 32; ++index)
            result.insert(kHighTable[index], static_cast<char>(0x80 + index));
        return result;
    }();
    return table;
}

bool isVbBlank(QChar c)
{
    return c == u' ' || c == u'\t' || c == u'\n';
}

} // namespace

QByteArray toAnsi(const QString &text)
{
    QByteArray result;
    result.reserve(text.size());
    for (const QChar c : text) {
        const char16_t code = c.unicode();
        if (code < 0x80 || (code >= 0xA0 && code <= 0xFF)) {
            result.append(static_cast<char>(code));
            continue;
        }
        const auto found = reverseHighTable().constFind(code);
        result.append(found != reverseHighTable().constEnd() ? found.value() : '?');
    }
    return result;
}

QString fromAnsi(const QByteArray &bytes)
{
    QString result;
    result.reserve(bytes.size());
    for (const char byte : bytes) {
        const auto code = static_cast<unsigned char>(byte);
        result.append(code >= 0x80 && code < 0xA0 ? QChar(kHighTable[code - 0x80]) : QChar(code));
    }
    return result;
}

QString fromOem(const QByteArray &bytes, OemCodePage codePage)
{
    const char16_t *table = codePage == OemCodePage::Cp866 ? kCp866 : kCp850;
    QString result;
    result.reserve(bytes.size());
    for (const char byte : bytes) {
        const auto code = static_cast<unsigned char>(byte);
        result.append(code >= 0x80 ? QChar(table[code - 0x80]) : QChar(code));
    }
    return result;
}

QString intEsa(qint64 value)
{
    const QString hex = QString::number(quint32(qint32(value)), 16).toUpper();
    return (QStringLiteral("0000") + hex).right(4);
}

QString hexBin(const QString &hex, bool rev)
{
    QString bits;
    for (const QChar c : hex) {
        bool ok = false;
        const uint nibble = QString(c).toUInt(&ok, 16);
        if (ok)
            bits += QStringLiteral("%1").arg(nibble, 4, 2, QLatin1Char('0'));
    }
    if (rev)
        std::reverse(bits.begin(), bits.end());
    return bits;
}

double legLng(const QString &dm1, const QString &dm2)
{
    return double((dm2 + dm1).toULongLong(nullptr, 16) & 0xFFFFFFFFull);
}

double hexDec(const QString &hex)
{
    double result = 0;
    for (const QChar c : hex) {
        bool ok = false;
        const uint nibble = QString(c).toUInt(&ok, 16);
        result = result * 16 + (ok ? nibble : 0);
    }
    return result;
}

double val(const QString &text)
{
    QString compact;
    compact.reserve(text.size());
    for (const QChar c : text) {
        if (c == QChar(u'\0'))
            break;
        if (!isVbBlank(c))
            compact.append(c);
    }

    if (compact.startsWith(u'&') && compact.size() > 1) {
        const QChar kind = compact.at(1).toUpper();
        const int base = kind == u'H' ? 16 : kind == u'O' ? 8 : 0;
        if (base) {
            qint64 value = 0;
            for (int index = 2; index < compact.size() && value <= 0xFFFFFFFFLL; ++index) {
                const char16_t c = compact.at(index).toUpper().unicode();
                int digit = -1;
                if (c >= u'0' && c <= u'9')
                    digit = c - u'0';
                else if (base == 16 && c >= u'A' && c <= u'F')
                    digit = c - u'A' + 10;
                if (digit < 0 || digit >= base)
                    break;
                value = value * base + digit;
            }
            // VB types the literal as Integer (16 bit) or Long (32 bit), so high values become negative.
            if (value > 0x7FFF && value <= 0xFFFF)
                value -= 0x10000;
            else if (value > 0x7FFFFFFFLL && value <= 0xFFFFFFFFLL)
                value -= 0x100000000LL;
            return static_cast<double>(value);
        }
    }

    int end = 0;
    if (end < compact.size() && (compact.at(end) == u'+' || compact.at(end) == u'-'))
        ++end;
    bool digits = false;
    while (end < compact.size() && compact.at(end).isDigit()) {
        ++end;
        digits = true;
    }
    if (end < compact.size() && compact.at(end) == u'.') {
        ++end;
        while (end < compact.size() && compact.at(end).isDigit()) {
            ++end;
            digits = true;
        }
    }
    if (!digits)
        return 0.0;
    if (end < compact.size() && (compact.at(end).toUpper() == u'E' || compact.at(end).toUpper() == u'D')) {
        int exponentEnd = end + 1;
        if (exponentEnd < compact.size() && (compact.at(exponentEnd) == u'+' || compact.at(exponentEnd) == u'-'))
            ++exponentEnd;
        const int exponentDigits = exponentEnd;
        while (exponentEnd < compact.size() && compact.at(exponentEnd).isDigit())
            ++exponentEnd;
        if (exponentEnd > exponentDigits)
            end = exponentEnd;
    }
    QString number = compact.left(end);
    number.replace(u'D', u'E').replace(u'd', u'e');
    return number.toDouble();
}

qint32 cLng(double value, bool *ok)
{
    const double rounded = std::nearbyint(value); // default FE_TONEAREST = half to even
    const bool inRange = std::isfinite(rounded)
        && rounded >= std::numeric_limits<qint32>::min()
        && rounded <= std::numeric_limits<qint32>::max();
    if (ok)
        *ok = inRange;
    return inRange ? static_cast<qint32>(rounded) : 0;
}

qint32 intDiv(double dividend, double divisor, bool *ok)
{
    bool okA = false;
    bool okB = false;
    const qint32 a = cLng(dividend, &okA);
    const qint32 b = cLng(divisor, &okB);
    const bool valid = okA && okB && b != 0;
    if (ok)
        *ok = valid;
    return valid ? a / b : 0;
}

QString fixedLength(const QString &value, int length)
{
    return value.size() >= length ? value.left(length) : value + QString(length - value.size(), u' ');
}

QString trim(const QString &text)
{
    qsizetype start = 0;
    qsizetype end = text.size();
    while (start < end && text.at(start) == u' ')
        ++start;
    while (end > start && text.at(end - 1) == u' ')
        --end;
    return text.mid(start, end - start);
}

} // namespace vb
