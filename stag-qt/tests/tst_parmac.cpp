#include "inibas.h"
#include "inifile.h"
#include "parcfgschtag.h"
#include "parmac.h"
#include "sqlitemigration.h"

#include <QFile>
#include <QSqlDatabase>
#include <QSqlQuery>
#include <QTemporaryDir>
#include <QTest>

using namespace stag;

class TestParMac : public QObject
{
    Q_OBJECT

private:
    QTemporaryDir m_dir;

    QString copyIni()
    {
        const QString path = m_dir.filePath(QStringLiteral("stag.ini"));
        QFile::remove(path);
        QFile::copy(QStringLiteral(STAG_REFERENCE_DATA "/stag.ini"), path);
        QFile::setPermissions(path, QFile::ReadOwner | QFile::WriteOwner);
        return path;
    }

private slots:
    void fixedStrings()
    {
        vb::FixStr f(4);
        QCOMPARE(f.str(), QString(4, QChar(0)));
        f = 15933.0;
        QCOMPARE(f.str(), QStringLiteral("1593"));
        f = "12";
        QCOMPARE(f.str(), QStringLiteral("12  "));
        QCOMPARE(f.val(), 12.0);
        QCOMPARE(vb::cStr(0.5), QStringLiteral("0.5"));
        QVERIFY(vb::cBool(QStringLiteral("1")));
        QVERIFY(!vb::cBool(QStringLiteral("0")));
        QVERIFY(vb::cBool(QStringLiteral("True")));
    }

    void legParMacReadsReferenceIni()
    {
        IniFile ini(copyIni());
        QVERIFY(ini.load());
        legParSup(ini, QString(), m_dir.path() + u'/');
        leggiNumLivTag(ini);
        legParMac(ini);

        QCOMPARE(pro_set.lin_att, qint16(1));
        QCOMPARE(par_mac.car_tec.num_liv_tag.str(), QStringLiteral("2"));
        QCOMPARE(par_mac.car_tec.num_mot_liv_tag.str(), QStringLiteral("2"));
        QCOMPARE(par_mac.car_tec.for_mac, 1);
        QCOMPARE(par_mac.car_tec.min_for_mac.str(), QStringLiteral("300 "));
        QCOMPARE(par_abi.num_mot_abi(1), qint16(2));
        QCOMPARE(par_abi.num_mot_abi(3), qint16(0));
        QCOMPARE(par_mac.config_sch_tag_att(1).descr.trimmed(), QStringLiteral("DEFAULT"));
        QCOMPARE(par_mac.lan_sch_tag(1).ind_rem, QStringLiteral("10.151.151.32"));
        QCOMPARE(par_mac.lan_sch_tag(2).por_rem.str(), QStringLiteral("4002  "));
        QVERIFY(par_mac.lan_sch_tag_UDP(1));
        QCOMPARE(par_mac.com_plc_lan.ind_rem, QStringLiteral("10.151.151.30"));
        QCOMPARE(par_mac.com_plc_lan.por_rem.str(), QStringLiteral("9600 "));
        QCOMPARE(par_mac.com_plc_lan.TimeOut.val(), 1000.0);
        QCOMPARE(par_mac.com_spde.lan.ind_rem, QStringLiteral("10.151.151.1"));
        QCOMPARE(par_mac.com_spde.lan.por_rem.val(), 250.0);
        QCOMPARE(par_mac.indirizzo_plc.TIMER_SCANS_PLC, qint16(20));
        QCOMPARE(par_mac.com_sch_tag(3).baud.str(), QStringLiteral("     "));
        QCOMPARE(par_mac.sch_tag.sch_tag_liv(3).dia_ruo_mis.str(), QStringLiteral("        "));
        QCOMPARE(par_mac.sch_tag.sch_tag_liv(1).dia_ruo_mis.length(), 8);
    }

    void leggiCfgPicksDefaultConfiguration()
    {
        const QString dbPath = m_dir.filePath(QStringLiteral("stag.sqlite"));
        const sqlitemigration::Result migrated = sqlitemigration::migrateDirectory(QStringLiteral(STAG_REFERENCE_DATA), dbPath);
        QVERIFY(migrated.errors.isEmpty());

        IniFile ini(copyIni());
        QVERIFY(ini.load());
        legParSup(ini, QString(), m_dir.path() + u'/');
        leggiNumLivTag(ini);
        legParMac(ini);
        {
            QSqlDatabase db = QSqlDatabase::addDatabase(QStringLiteral("QSQLITE"), QStringLiteral("tst_parmac"));
            db.setDatabaseName(dbPath);
            QVERIFY(db.open());
            leggiCfg(db, ini);

            // for_mac=1 (2500 mm), gra_car=0 (1800), 2 motors per level, 2 enabled -> PAR_CFG record 9
            QCOMPARE(par_mac.par_cfg_att(1).vel_max_tag.str(), QStringLiteral("350"));
            QCOMPARE(par_mac.par_cfg_att(2).vel_max_tag.str(), QStringLiteral("350"));
            QCOMPARE(par_mac.par_cfg_def(1).vel_max_tag.str(), QStringLiteral("350"));
            QCOMPARE(par_mac.config_sch_tag_att(1).num_mot_liv, qint16(2));
            QSqlQuery q(db);
            QVERIFY(q.exec(QStringLiteral("SELECT rec_no FROM PAR_CFG WHERE trim(LIVELLO1) = '1'")));
            QVERIFY(q.next());
            QCOMPARE(q.value(0).toInt(), 9);
            QVERIFY(!q.next());
            db.close();
        }
        QSqlDatabase::removeDatabase(QStringLiteral("tst_parmac"));
        QVERIFY(ini.load());
        QCOMPARE(ini.value(QStringLiteral("sch_tag_liv_1"), QStringLiteral("config_att")), QStringLiteral("DEFAULT"));
    }
};

QTEST_GUILESS_MAIN(TestParMac)
#include "tst_parmac.moc"
