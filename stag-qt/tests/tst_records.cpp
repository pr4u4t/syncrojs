#include "recordfile.h"
#include "stagrecords.h"

#include <QDir>
#include <QFile>
#include <QTemporaryDir>
#include <QTest>

class TestRecords : public QObject
{
    Q_OBJECT

private:
    static QString reference(const QString &name) { return QDir(QStringLiteral(STAG_REFERENCE_DATA)).filePath(name); }

    static QByteArray fileBytes(const QString &path)
    {
        QFile file(path);
        return file.open(QIODevice::ReadOnly) ? file.readAll() : QByteArray();
    }

private slots:
    void layoutSizesMatchVbTypes()
    {
        QCOMPARE(stag::orderLayout()->size(), 702);
        QCOMPARE(stag::shiftProductionLayout()->size(), 364);
        QCOMPARE(stag::orderProductionLayout()->size(), 18);
        QCOMPARE(stag::orderLayout()->field("ord(1).com.cod_com_ord")->offset, 104);
        QCOMPARE(stag::orderLayout()->field("rep_pro.ora_ini")->offset, 551);
    }

    void orderFilesRoundTripByteForByte_data()
    {
        QTest::addColumn<QString>("name");
        QTest::newRow("queue") << QStringLiteral("Ord_mac.dat");
        QTest::newRow("executed") << QStringLiteral("Ord_mac_ese.dat");
    }

    void orderFilesRoundTripByteForByte()
    {
        QFETCH(QString, name);
        const QString source = reference(name);
        if (!QFile::exists(source))
            QSKIP("reference data not available");

        const QVector<QByteArray> raw = recordfile::read(source, stag::kOrderRecordSize, stag::kOrderFileRecords);
        QVector<QByteArray> rewritten;
        for (const QByteArray &bytes : raw)
            rewritten.append(stag::OrderRecord(bytes).raw());

        QTemporaryDir directory;
        const QString target = directory.filePath(name);
        QVERIFY(recordfile::write(target, rewritten, stag::kOrderRecordSize));
        QCOMPARE(fileBytes(target), fileBytes(source));
    }

    void decodesQueuedOrder()
    {
        const QString source = reference(QStringLiteral("Ord_mac.dat"));
        if (!QFile::exists(source))
            QSKIP("reference data not available");

        const QVector<QByteArray> raw = recordfile::read(source, stag::kOrderRecordSize, stag::kOrderFileRecords);
        const stag::OrderRecord first(raw.first());
        QVERIFY(first.isUsed());
        QCOMPARE(first.text("cod_ord_abb"), QStringLiteral("5291.2     "));
        QCOMPARE(first.text("tip_car.cod_com_car").trimmed(), QStringLiteral("FALA A"));
        QCOMPARE(first.text("ord(1).com.cod_com_ord").trimmed(), QStringLiteral("R1212026101"));
        QVERIFY(stag::OrderRecord(raw.at(1)).isNull());
        QVERIFY(!stag::OrderRecord(raw.at(1)).isUsed());
    }

    void setTextUsesVbAssignment()
    {
        stag::OrderRecord order;
        order.setText("stag_id", "12");
        QCOMPARE(order.text("stag_id"), QStringLiteral("12  "));
        order.setText("stag_id", "123456");
        QCOMPARE(order.text("stag_id"), QStringLiteral("1234"));
        QCOMPARE(order.raw().size(), 702);
        QVERIFY(order.isUsed());
    }

    void productionFilesRoundTrip_data()
    {
        QTest::addColumn<QString>("name");
        QTest::addColumn<int>("size");
        QTest::newRow("shift") << QStringLiteral("dat_prd.dat") << stag::kShiftProductionSize;
        QTest::newRow("order") << QStringLiteral("dat_prd_ord.dat") << stag::kOrderProductionSize;
    }

    void productionFilesRoundTrip()
    {
        QFETCH(QString, name);
        QFETCH(int, size);
        const QString source = reference(name);
        if (!QFile::exists(source))
            QSKIP("reference data not available");

        const QVector<QByteArray> raw = recordfile::read(source, size, 1);
        QTemporaryDir directory;
        QVERIFY(recordfile::write(directory.filePath(name), raw, size));
        QCOMPARE(fileBytes(directory.filePath(name)), fileBytes(source));

        if (name == QLatin1String("dat_prd.dat")) {
            const FixedRecord production(stag::shiftProductionLayout(), raw.first());
            QCOMPARE(production.text("tur(1).dat_ini"), QStringLiteral("20250331"));
            QCOMPARE(production.text("gio.dat_ini"), QStringLiteral("20250331"));
        }
    }
};

QTEST_APPLESS_MAIN(TestRecords)
#include "tst_records.moc"
