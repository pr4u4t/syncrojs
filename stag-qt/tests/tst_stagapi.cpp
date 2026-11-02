#include "collezioneordini.h"
#include "pagpromodel.h"
#include "stabarform.h"
#include "stabarmodel.h"
#include "stagapi.h"
#include "stagapp.h"

#include <QFile>
#include <QHostAddress>
#include <QJsonArray>
#include <QJsonDocument>
#include <QJsonObject>
#include <QSignalSpy>
#include <QTcpSocket>
#include <QTemporaryDir>
#include <QTest>

class TestStagApi : public QObject
{
    Q_OBJECT

private:
    // What the server sends for what is written, until it closes the connection.
    static QByteArray ask(quint16 port, const QByteArray &command)
    {
        // The server is on this thread, so the waiting has to keep its events going.
        QTcpSocket socket;
        QSignalSpy closed(&socket, &QTcpSocket::disconnected);
        QByteArray answer;
        connect(&socket, &QTcpSocket::connected, &socket, [&] { socket.write(command); });
        connect(&socket, &QTcpSocket::readyRead, &socket, [&] { answer += socket.readAll(); });
        socket.connectToHost(QHostAddress::LocalHost, port);
        return closed.wait(3000) ? answer : QByteArrayLiteral("no answer");
    }

private slots:
    void answersOnlyTheStatusCommand()
    {
        int reads = 0;
        StagApi api([&reads] { ++reads; return QByteArrayLiteral("{\"a\":1}"); });
        QVERIFY(api.listen(QHostAddress::LocalHost, 0));
        QCOMPARE(ask(api.port(), "Stag.Status"), QByteArrayLiteral("{\"a\":1}"));
        QCOMPARE(ask(api.port(), "Stag.Status\r\n"), QByteArrayLiteral("{\"a\":1}"));
        QCOMPARE(ask(api.port(), "Stag.Write 1"), QByteArrayLiteral("error"));
        QCOMPARE(ask(api.port(), "x"), QByteArrayLiteral("error"));
        QCOMPARE(reads, 2);
    }

    void aSilentClientIsDropped()
    {
        StagApi api([] { return QByteArray(); });
        QVERIFY(api.listen(QHostAddress::LocalHost, 0));
        QTcpSocket socket;
        QSignalSpy closed(&socket, &QTcpSocket::disconnected);
        socket.connectToHost(QHostAddress::LocalHost, api.port());
        QVERIFY(closed.wait(4000));
    }

    void statusShowsTheBarAndTheAlarms()
    {
        CollezioneOrdini orders;
        PagProModel page(&orders);
        StaBarModel bar;
        bar.eveTagVel(123);
        QVERIFY(QJsonDocument::fromJson(stagStatusJson(bar, page)).object().value(QStringLiteral("alarms")).isNull());
        bar.setLinNoComPlc(false);
        QJsonObject status = QJsonDocument::fromJson(stagStatusJson(bar, page)).object();
        QCOMPARE(status.value(QStringLiteral("speed")).toString(), bar.property("txtVel").toString().trimmed());
        QVERIFY(status.value(QStringLiteral("current")).toObject().value(QStringLiteral("levels")).toArray().isEmpty());
        QCOMPARE(status.value(QStringLiteral("alarms")).toObject().value(QStringLiteral("general")).toBool(), false);
        bar.setProperty("shpAllMal", StaBarModel::vbColor(StaBarModel::vbRed));
        status = QJsonDocument::fromJson(stagStatusJson(bar, page)).object();
        QCOMPARE(status.value(QStringLiteral("alarms")).toObject().value(QStringLiteral("general")).toBool(), true);
    }

    void lampsAreGreyWithoutThePlc()
    {
        StaBarModel bar;
        StaBarFormModel form(&bar);
        const auto fill = [&form](const char *control) {
            return form.state().value(QLatin1String(control)).toMap().value(QStringLiteral("fillColor")).toString();
        };
        const QString grey = StaBarModel::vbColor(0x808080);
        const QString green = StaBarModel::vbColor(StaBarModel::LIGHT_Green);
        const QString red = StaBarModel::vbColor(StaBarModel::RED_GRU_ALL);

        bar.setLinNoComPlc(true);
        for (const char *lamp : {"shp_all_har", "shp_all_qe", "shp_all_bm", "shp_all_con"})
            QCOMPARE(fill(lamp), grey);
        QCOMPARE(fill("shp_all_mal"), green);

        bar.setLinNoComPlc(false);
        QCOMPARE(fill("shp_all_har"), green);

        // The bit of the hardware group; the state survives a loss of the connection.
        bar.eveTagGruall(QStringLiteral("10"));
        QCOMPARE(fill("shp_all_har"), red);
        QCOMPARE(fill("shp_all_con"), green);
        bar.setLinNoComPlc(true);
        QCOMPARE(fill("shp_all_har"), grey);
        bar.setLinNoComPlc(false);
        QCOMPARE(fill("shp_all_har"), red);
    }

    void writesFollowTheIniAndTheOption()
    {
        // ini value (empty: no key), command line option, expected.
        struct Case { const char *ini; bool option; bool expected; };
        for (const Case &c : {Case{"", false, false}, {"0", false, false}, {"1", false, true}, {" 1 ", false, true},
                              {"yes", false, false}, {"true", false, false}, {"0", true, true}}) {
            QTemporaryDir dir;
            const QString ini = dir.filePath(QStringLiteral("stag.ini"));
            QVERIFY(QFile::copy(QStringLiteral(STAG_REFERENCE_DATA "/stag.ini"), ini));
            QFile::setPermissions(ini, QFile::ReadOwner | QFile::WriteOwner);
            for (const char *name : {"Ord_mac.dat", "Ord_mac_ese.dat", "Ordini.ini"})
                QFile::copy(QStringLiteral(STAG_REFERENCE_DATA "/") + QLatin1String(name), dir.filePath(QLatin1String(name)));
            const QString db = dir.filePath(QStringLiteral("stag.sqlite"));
            QVERIFY(QFile::copy(QStringLiteral(STAG_SQLITE), db));
            QFile::setPermissions(db, QFile::ReadOwner | QFile::WriteOwner);
            if (*c.ini) {
                QFile file(ini);
                QVERIFY(file.open(QIODevice::Append));
                file.write(QByteArray("\r\n[Plc]\r\nEnableWrites=") + c.ini + "\r\n");
            }
            StagApp::Options options;
            options.dataDir = dir.path();
            options.sqlitePath = db;
            options.plcHostOverride = QStringLiteral("127.0.0.1");
            options.plcPortOverride = 1;
            options.plcWrites = c.option;
            StagApp app(options);
            app.start();
            QCOMPARE(app.plc().writesEnabled(), c.expected);
            if (!*c.ini) {
                QFile file(ini);
                QVERIFY(file.open(QIODevice::ReadOnly));
                QVERIFY(file.readAll().contains("EnableWrites=0"));
            }
        }
    }
};

QTEST_GUILESS_MAIN(TestStagApi)
#include "tst_stagapi.moc"
