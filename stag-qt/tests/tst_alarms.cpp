#include "alarmgridmodel.h"
#include "alarmstore.h"
#include "messages.h"
#include "stabarmodel.h"

#include <QDateTime>
#include <QFile>
#include <QSet>
#include <QSqlDatabase>
#include <QSqlQuery>
#include <QTemporaryDir>
#include <QTest>

using namespace stag;

static const QStringList CODES{QStringLiteral("mac"), QStringLiteral("con"), QStringLiteral("har"), QStringLiteral("war"),
                               QStringLiteral("qe1"), QStringLiteral("qe2"), QStringLiteral("qe3")};

class TestAlarms : public QObject
{
    Q_OBJECT

private:
    QTemporaryDir m_dir;
    QSqlDatabase m_db;

    int count(const QString &sql)
    {
        QSqlQuery query(m_db);
        query.exec(sql);
        query.next();
        return query.value(0).toInt();
    }

private slots:
    void initTestCase()
    {
        const QString copy = m_dir.filePath(QStringLiteral("stag.sqlite"));
        QVERIFY(QFile::copy(QStringLiteral(STAG_SQLITE), copy));
        QFile::setPermissions(copy, QFile::ReadOwner | QFile::WriteOwner);
        m_db = QSqlDatabase::addDatabase(QStringLiteral("QSQLITE"), QStringLiteral("alarms"));
        m_db.setDatabaseName(copy);
        QVERIFY(m_db.open());
        QVERIFY(Messages::instance().load(copy, 1));
    }

    // Every alarm the VB program wrote to its history must be reachable: a cell of one of the seven
    // grids whose address gives exactly that text.
    void everyAlarmInTheMachineHistoryHasACell()
    {
        QSet<QString> reachable;
        for (const QString &code : CODES) {
            AlarmGridModel model(code, m_db);
            model.load();
            for (int row = 0; row < AlarmGridModel::RIG_ALL; ++row) {
                for (int col = 0; col < AlarmGridModel::COL_ALL; ++col) {
                    const QString address = model.addressAt(row, col);
                    if (!address.isEmpty())
                        reachable << Messages::instance().find(QStringLiteral("all_%1_%2").arg(code, address));
                }
            }
        }
        QSqlQuery history(m_db);
        QVERIFY(history.exec(QStringLiteral("SELECT DISTINCT ALLARME FROM STO")));
        int alarms = 0;
        QStringList missing;
        while (history.next()) {
            ++alarms;
            if (!reachable.contains(history.value(0).toString()))
                missing << history.value(0).toString();
        }
        QCOMPARE(alarms, 40);
        QVERIFY2(missing.isEmpty(), qPrintable(missing.join(u'|')));
    }

    void addressNamesWordAndBit()
    {
        AlarmGridModel model(QStringLiteral("mac"), m_db);
        model.load();
        QCOMPARE(model.addressAt(0, 0), QStringLiteral("1200"));
        QCOMPARE(model.addressAt(0, 5), QStringLiteral("1215"));
        QString bits(336, u'0');
        bits[12 * 16 + 15] = u'1'; // word 12, bit 15
        model.setAlarms(bits);
        QVERIFY(model.valBit(0, 5));
        QVERIFY(!model.valBit(0, 0));
        QVERIFY(!model.valBit(1, 0) || !model.addressAt(1, 0).isEmpty());
    }

    void aBitGoingActiveWritesHistoryAndFrequencyOnce()
    {
        AlarmGridModel model(QStringLiteral("mac"), m_db);
        model.load();
        model.now = [] { return QDateTime(QDate(2026, 10, 30), QTime(14, 5, 9)); };
        const QString text = Messages::instance().find(QStringLiteral("all_mac_1201"));
        QVERIFY(!text.isEmpty());

        const int freq = count(QStringLiteral("SELECT coalesce(max(FREQ), 0) FROM F_A_MAC WHERE ALLARME = '%1'").arg(text));
        QString bits(336, u'0');
        bits[12 * 16 + 1] = u'1';
        model.setAlarms(bits);

        QCOMPARE(count(QStringLiteral("SELECT FREQ FROM F_A_MAC WHERE ALLARME = '%1'").arg(text)), freq + 1);
        QSqlQuery last(m_db);
        last.exec(QStringLiteral("SELECT ALLARME, DATA_ALL, ORA_ALL FROM STO ORDER BY rec_no DESC LIMIT 1"));
        QVERIFY(last.next());
        QCOMPARE(last.value(0).toString(), text);
        QCOMPARE(last.value(1).toString(), QStringLiteral("2026-10-30"));
        QCOMPARE(last.value(2).toString(), QStringLiteral("2.05.09 ")); // Left$(CStr(Time), 8), as in the STO table

        // Still on: nothing new. Off and on again: a new event.
        QString other = bits;
        other[12 * 16 + 2] = u'1';
        const int before = count(QStringLiteral("SELECT FREQ FROM F_A_MAC WHERE ALLARME = '%1'").arg(text));
        model.setAlarms(other);
        QCOMPARE(count(QStringLiteral("SELECT FREQ FROM F_A_MAC WHERE ALLARME = '%1'").arg(text)), before);
        model.setAlarms(QString(336, u'0'));
        model.setAlarms(bits);
        QCOMPARE(count(QStringLiteral("SELECT FREQ FROM F_A_MAC WHERE ALLARME = '%1'").arg(text)), before + 1);
    }

    void historyKeepsTheNewestTwoHundred()
    {
        for (int i = 0; i < 5; ++i)
            ins_sto(m_db, QStringLiteral("extra %1").arg(i), QDateTime(QDate(2026, 10, 30), QTime(8, 0, i)));
        QCOMPARE(count(QStringLiteral("SELECT count(*) FROM STO")), NUM_REC_STO);
        QCOMPARE(count(QStringLiteral("SELECT count(*) FROM STO WHERE ALLARME = 'extra 4'")), 1);
    }

    void frequencyWrapsAtTheMaximum()
    {
        QSqlQuery seed(m_db);
        QVERIFY(seed.exec(QStringLiteral("INSERT INTO F_A_CON (ALLARME, FREQ) VALUES ('wrap', %1)").arg(MAX_FREQ_ALL)));
        ins_freq_all(m_db, QStringLiteral("f_a_con"), QStringLiteral("wrap"));
        QCOMPARE(count(QStringLiteral("SELECT FREQ FROM F_A_CON WHERE ALLARME = 'wrap'")), 1);
    }

    void cursorIsPinkOnAnActiveAlarmAndRestoresRed()
    {
        AlarmGridModel model(QStringLiteral("mac"), m_db);
        model.load();
        QString bits(336, u'0');
        bits[12 * 16 + 1] = u'1';
        model.setAlarms(bits);
        const auto back = [&](int row, int col) {
            return model.value(QStringLiteral("mac_griglia"), QStringLiteral("cellBack")).toList().at(row).toStringList().at(col);
        };
        QCOMPARE(back(0, 1), StaBarModel::vbColor(StaBarModel::COL_ALL_Red));
        model.keyUp(Qt::Key_Right);
        QCOMPARE(back(0, 1), StaBarModel::vbColor(0xFF80FF));
        model.keyUp(Qt::Key_Right);
        QCOMPARE(back(0, 1), StaBarModel::vbColor(StaBarModel::COL_ALL_Red));
        QCOMPARE(back(0, 2), StaBarModel::vbColor(0x008000));
    }

    void groupsChooseTheMachineImages()
    {
        PagAllModel model;
        model.load();
        const auto shown = [&](int i) { return model.value(QStringLiteral("img_master(%1)").arg(i), QStringLiteral("visible")).toBool(); };
        QVERIFY(shown(0));
        QString bits(16, u'0');
        bits[15 - 5] = u'1'; // POS_GRU_BM
        model.setGroups(bits);
        QVERIFY(shown(1));
        QVERIFY(!shown(0));
        bits[15 - 2] = u'1'; // POS_GRU_QE_MST
        model.setGroups(bits);
        QVERIFY(shown(2));
        bits[15 - 5] = u'0';
        model.setGroups(bits);
        QVERIFY(shown(3));
        QCOMPARE(model.value(QStringLiteral("lab_all(2)"), QStringLiteral("foreColor")).toString(), StaBarModel::vbColor(StaBarModel::COL_ALL_Red));
        bits[15 - 6] = u'1'; // POS_GRU_CON
        model.setGroups(bits);
        QVERIFY(model.value(QStringLiteral("img_consolle(1)"), QStringLiteral("visible")).toBool());
    }

    void historyGridShowsNewestFirstInThePlantDateFormat()
    {
        AlarmTableModel model(QStringLiteral("dbgrid_sto_all"), QStringLiteral("SELECT ALLARME, DATA_ALL, ORA_ALL FROM STO ORDER BY DATA_ALL DESC, ORA_ALL DESC"),
                              {QStringLiteral("ALLARME"), QStringLiteral("DATA"), QStringLiteral("ORA")}, {560, 120, 100}, m_db);
        model.activate(QString());
        const QVariantList cells = model.value(QStringLiteral("dbgrid_sto_all"), QStringLiteral("cells")).toList();
        QCOMPARE(cells.size(), NUM_REC_STO + 1);
        QCOMPARE(cells.at(1).toStringList().at(1), QStringLiteral("10/30/2026"));
    }
};

QTEST_GUILESS_MAIN(TestAlarms)
#include "tst_alarms.moc"
