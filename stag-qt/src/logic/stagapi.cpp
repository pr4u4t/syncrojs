#include "stagapi.h"

#include "pagpromodel.h"
#include "stabarmodel.h"

#include <QJsonArray>
#include <QJsonDocument>
#include <QJsonObject>
#include <QTcpSocket>
#include <QTimer>

namespace {

const QByteArray COMMAND_STATUS = QByteArrayLiteral("Stag.Status");
constexpr int MAX_CLIENTS = 8;
constexpr int CLIENT_TIMEOUT_MS = 2000;

QJsonObject orderJson(const PagProModel &page, const QString &prefix)
{
    const QVariantMap state = page.state();
    const auto text = [&](const QString &control) {
        return state.value(control).toMap().value(QStringLiteral("text")).toString().trimmed();
    };
    QJsonArray levels;
    for (int i = 0; i < 3; ++i) {
        const auto field = [&](const char *name) { return text(QStringLiteral("%1_%2(%3)").arg(prefix, QLatin1String(name)).arg(i)); };
        if (field("liv").isEmpty())
            continue;
        levels.append(QJsonObject{
            {QStringLiteral("level"), field("liv")},
            {QStringLiteral("job"), field("com")},
            {QStringLiteral("width"), field("lar_sca")},
            {QStringLiteral("length"), field("lun_sca")},
            {QStringLiteral("outs"), field("res")},
            {QStringLiteral("sheets"), field("sca")},
            {QStringLiteral("meters"), field("met")},
        });
    }
    return QJsonObject{
        {QStringLiteral("code"), text(prefix + QStringLiteral("_cod_ord"))},
        {QStringLiteral("flute"), text(prefix + QStringLiteral("_tip_ond"))},
        {QStringLiteral("paper"), text(prefix + QStringLiteral("_tip_car"))},
        {QStringLiteral("total"), text(prefix + QStringLiteral("_met_tot"))},
        {QStringLiteral("levels"), levels},
    };
}

} // namespace

QByteArray stagStatusJson(const StaBarModel &staBar, const PagProModel &pagPro)
{
    const auto text = [&](const char *property) { return staBar.property(property).toString().trimmed(); };
    // The lamps are green unless their alarm group is active.
    const auto alarm = [&](const char *property) { return staBar.property(property).toString() != StaBarModel::vbColor(StaBarModel::LIGHT_Green); };
    const QJsonObject status{
        {QStringLiteral("plcConnected"), !staBar.property("linNoComPlc").toBool()},
        {QStringLiteral("running"), text("txtStaRun")},
        {QStringLiteral("reset"), text("txtStaRes")},
        {QStringLiteral("machine"), text("labStaMac")},
        {QStringLiteral("mode"), text("txtMod")},
        {QStringLiteral("speed"), text("txtVel")},
        {QStringLiteral("maxSpeed"), text("txtVelMax")},
        {QStringLiteral("metersLeft"), text("txtMetMan")},
        {QStringLiteral("timeToChange"), text("txtTemCo")},
        {QStringLiteral("message"), text("labMsg")},
        // null without the PLC: the groups are unknown then, not free of alarms.
        {QStringLiteral("alarms"), staBar.property("linNoComPlc").toBool() ? QJsonValue() : QJsonObject{
            {QStringLiteral("general"), alarm("shpAllMal")},
            {QStringLiteral("hardware"), alarm("shpAllHar")},
            {QStringLiteral("electrical"), alarm("shpAllQe")},
            {QStringLiteral("onboard"), alarm("shpAllBm")},
            {QStringLiteral("console"), alarm("shpAllCon")},
        }},
        {QStringLiteral("current"), orderJson(pagPro, QStringLiteral("cor"))},
        {QStringLiteral("next"), orderJson(pagPro, QStringLiteral("suc"))},
    };
    return QJsonDocument(status).toJson(QJsonDocument::Compact);
}

StagApi::StagApi(Snapshot status, QObject *parent)
    : QObject(parent)
    , m_status(std::move(status))
{
    connect(&m_server, &QTcpServer::newConnection, this, &StagApi::accept);
}

bool StagApi::listen(const QHostAddress &address, quint16 port)
{
    return m_server.listen(address, port);
}

// All on the signals of the socket: a slow client never holds up the timer of the PLC loop.
void StagApi::accept()
{
    while (QTcpSocket *client = m_server.nextPendingConnection()) {
        if (m_clients >= MAX_CLIENTS) {
            client->abort();
            client->deleteLater();
            continue;
        }
        ++m_clients;
        auto *limit = new QTimer(client);
        limit->setSingleShot(true);
        connect(limit, &QTimer::timeout, client, &QTcpSocket::abort);
        limit->start(CLIENT_TIMEOUT_MS);
        connect(client, &QTcpSocket::disconnected, this, [this, client] {
            --m_clients;
            client->deleteLater();
        });
        connect(client, &QTcpSocket::readyRead, client, [this, client] {
            const QByteArray command = client->peek(COMMAND_STATUS.size() + 3).trimmed();
            if (command != COMMAND_STATUS && COMMAND_STATUS.startsWith(command))
                return; // not all of it yet
            client->readAll();
            client->write(command == COMMAND_STATUS ? m_status() : QByteArrayLiteral("error"));
            client->disconnectFromHost();
        });
    }
}
