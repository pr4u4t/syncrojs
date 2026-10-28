#include "fins.h"
#include "finsclient.h"
#include "finssimulator.h"

#include <QSignalSpy>
#include <QTest>

namespace {

// Plc.log 12/10/26 13.22.11: snd_str_PLC: (DM3500-DM3528)
const char LoggedOrder[] =
    "000D000024DB000305FA008B0000013004F800A70000012C0000000000000000002100270000000000000000000000000000000000000000";

QVector<qint32> words(const QByteArray &hex)
{
    QVector<qint32> out;
    for (int i = 0; i + 4 <= hex.size(); i += 4)
        out.append(qint32(hex.mid(i, 4).toUInt(nullptr, 16)));
    return out;
}

} // namespace

class TestFins : public QObject
{
    Q_OBJECT

private slots:
    void writeFrameMatchesVb()
    {
        const QVector<qint32> values = words(LoggedOrder);
        QCOMPARE(values.size(), 28);
        const QByteArray p = fins::memoryAreaWrite(30, 7, 5, 3500, values);
        const QByteArray expected = QByteArray::fromHex("46494E5300000052000000020000000080000200" "1E000007" "0005" "0102" "820DAC00001C")
            + QByteArray::fromHex(LoggedOrder);
        QCOMPARE(p.toHex(), expected.toHex());
    }

    void negativeValuesWrapLikeVb()
    {
        // Omron_PLC.bas: Val("&H8000") is -32768, Fins.DM writes 65536 + v.
        const QByteArray p = fins::memoryAreaWrite(1, 2, 3, 0, {-32768, -1});
        QCOMPARE(p.right(4).toHex(), QByteArray("8000ffff"));
    }

    void readAndControlFrames()
    {
        QCOMPARE(fins::memoryAreaRead(30, 7, 1, 3600, 100).toHex(),
                 QByteArray("46494e530000001a0000000200000000800002001e00000700010101820e10000064"));
        QCOMPARE(fins::runModeRead(30, 7, 2).toHex(), QByteArray("46494e5300000014000000020000000080000200" "1e0000070002" "0601"));
        QCOMPARE(fins::setMonitorMode(30, 7, 3).toHex(), QByteArray("46494e5300000017000000020000000080000200" "1e0000070003" "0401ffff02"));
        QCOMPARE(fins::nodeAddressRequest().toHex(), QByteArray("46494e530000000c000000000000000000000000"));
    }

    void splitsTcpStream()
    {
        const QByteArray a = fins::nodeAddressResponse(7, 30);
        const QByteArray b = fins::runModeRead(30, 7, 1);
        QByteArray buffer = a + b.left(5);
        QList<QByteArray> packets;
        QVERIFY(fins::takePackets(buffer, packets));
        QCOMPARE(packets.size(), 1);
        QCOMPARE(packets[0], a);
        buffer += b.mid(5);
        QVERIFY(fins::takePackets(buffer, packets));
        QCOMPARE(packets.size(), 2);
        QCOMPARE(packets[1], b);
        QVERIFY(buffer.isEmpty());

        QByteArray garbage("HTTP/1.1 200 OK\r\n");
        QVERIFY(!fins::takePackets(garbage, packets));
        QVERIFY(garbage.isEmpty());
    }

    void clientTalksToSimulator()
    {
        FinsSimulator plc;
        QVERIFY(plc.listen(QHostAddress::LocalHost, 0));
        QSignalSpy written(&plc, &FinsSimulator::dmWritten);

        FinsClient client;
        client.setRemoteHost(QStringLiteral("127.0.0.1"));
        client.setRemotePort(plc.port());
        client.setTimeOut(200);
        QVERIFY(client.error());
        QCOMPARE(client.readArea(3600, 100), -1);
        client.setEnabled(true);
        QTRY_VERIFY_WITH_TIMEOUT(!client.error(), 5000);

        const QVector<qint32> order = words(LoggedOrder);
        QCOMPARE(client.writeArea(3500, order), 0);
        QCOMPARE(written.size(), 1);
        QCOMPARE(written[0][0].toInt(), 3500);
        QCOMPARE(plc.dm(3502), quint16(0x24DB));

        plc.setDm(3600, {0x8E70, 0x0622, 0xFFFF});
        QCOMPARE(client.readArea(3600, 3), 0x8E70);
        QCOMPARE(client.retData(), (QVector<qint32>{0x8E70, 0x0622, 0xFFFF}));

        QCOMPARE(client.setMonitor(), 0);
        QCOMPARE(client.runMode(), 2);
        QCOMPARE(plc.mode(), quint8(2));
    }

    void timeoutKeepsStaleDataAndReconnects()
    {
        FinsSimulator plc;
        QVERIFY(plc.listen(QHostAddress::LocalHost, 0));
        FinsClient client;
        client.setRemoteHost(QStringLiteral("127.0.0.1"));
        client.setRemotePort(plc.port());
        client.setTimeOut(100);
        client.setEnabled(true);
        QTRY_VERIFY_WITH_TIMEOUT(!client.error(), 5000);

        plc.setDm(10, quint16(42));
        QCOMPARE(client.readArea(10, 1), 42);

        plc.setMuted(true);
        plc.setDm(10, quint16(43));
        QCOMPARE(client.readArea(10, 1), -1);
        QVERIFY(client.error());
        QCOMPARE(client.retData(), QVector<qint32>{42});

        plc.setMuted(false);
        QTRY_VERIFY_WITH_TIMEOUT(!client.error(), 5000);
        QCOMPARE(client.readArea(10, 1), 43);
    }
};

QTEST_GUILESS_MAIN(TestFins)
#include "tst_fins.moc"
