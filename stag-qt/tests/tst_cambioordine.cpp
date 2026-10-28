#include "classestato.h"
#include "collezioneordini.h"
#include "finsclient.h"
#include "finssimulator.h"
#include "gesord.h"
#include "inibas.h"
#include "inistore.h"
#include "parcurtag.h"
#include "parmac.h"
#include "stagglobals.h"
#include "vbtypes.h"

#include <QFile>
#include <QSignalSpy>
#include <QTemporaryDir>
#include <QTest>

using namespace stag;

class TestCambioOrdine : public QObject
{
    Q_OBJECT

private slots:
    void regionalDates()
    {
        // Ord_mac_ese.dat: ini "14.13 PM", fin "16.35 PM" on 10/22/20 -> tem_mar 8520
        QCOMPARE(vb::dateDiffSeconds(QStringLiteral("10/22/20 14.13 PM"), QStringLiteral("10/22/20 16.35 PM")), qint64(8520));
        QCOMPARE(vb::dateDiffSeconds(QStringLiteral("10/22/20 06.59 PM"), QStringLiteral("10/22/20 14.13 PM")), qint64(-17160));
        QVERIFY_THROWS_EXCEPTION(vb::Error, vb::dateDiffSeconds(QStringLiteral("10/22/20 36.56 PM"), QStringLiteral("10/22/20 06.59 PM")));
        QCOMPARE(vb::cStrDate(QDate(2026, 10, 22)), QStringLiteral("10/22/2026"));
        QCOMPARE(vb::cStrTime(QTime(16, 14, 13)), QStringLiteral("4.14.13 PM"));
        QCOMPARE(vb::formatStr(QStringLiteral("-3"), 3), QStringLiteral("-003"));
        QCOMPARE(vb::formatStr(QString(), 3), QString());
        QCOMPARE(vb::cStrSingle(990.601f), QStringLiteral("990.601"));
    }

    void cuttingCurve()
    {
        LIV_BAS = 1;
        LIV_ALT = 2;
        LIV_MED = 0;
        TipCurTag &c = cur_tag(1);
        for (int m = 1; m <= 15; ++m) {
            c.Lun_tag(m) = 500 + (m - 1) * 100;
            c.vel_max(m) = 100 + (m - 1) * 10;
        }
        QCOMPARE(calVelCurTag(1, 500), 100);
        QCOMPARE(calVelCurTag(1, 550), 105);
        QCOMPARE(calVelCurTag(1, 1900), 240);
        QCOMPARE(calVelCurTag(1, 5000), 0);
        QCOMPARE(calLiv(2), 3);
        QCOMPARE(calLiv(1), 1);
    }

    void cambioOrdineCompletato()
    {
        QTemporaryDir dir;
        for (const char *name : {"Ord_mac.dat", "Ord_mac_ese.dat", "Ordini.ini", "stag.ini"}) {
            QFile::copy(QStringLiteral(STAG_REFERENCE_DATA "/") + QString::fromLatin1(name), dir.filePath(QString::fromLatin1(name)));
            QFile::setPermissions(dir.filePath(QString::fromLatin1(name)), QFile::ReadOwner | QFile::WriteOwner);
        }
        setDataDirectory(dir.path());
        legParSup(iniFile(), QString(), dir.path() + u'/');
        leggiNumLivTag(iniFile());
        legParMac(iniFile());
        iniLiv();
        pro_set.abi_reg_rep_pro = QStringLiteral("1");
        pro_set.ini_prod = QStringLiteral("0");
        vb::setNow(QDateTime(QDate(2026, 10, 22), QTime(16, 14, 13)));

        ClasseStato stato;
        sta_tag = &stato;
        CollezioneOrdini ordMac, ordMacEse;
        ordMac.Init(false);
        ordMac.Leg_dat_ord();
        ordMacEse.Init(true);
        ordMacEse.Leg_dat_ord();
        ord_mac = &ordMac;
        ord_mac_ese = &ordMacEse;
        const int count = ordMac.Count();
        const int countEse = ordMacEse.Count();
        QVERIFY(count >= 1);
        const OrdinePtr first = ordMac.item(1);
        const QString stagId = first->stag_id();
        first->setText(QStringLiteral("rep_pro.dat_ini"), QStringLiteral("10/22/20"));
        first->setText(QStringLiteral("rep_pro.ora_ini"), QStringLiteral("01.00 PM"));

        FinsSimulator plc;
        QVERIFY(plc.listen(QHostAddress::LocalHost, 0));
        FinsClient fins;
        fins.setRemoteHost(QStringLiteral("127.0.0.1"));
        fins.setRemotePort(plc.port());
        fins.setTimeOut(200);
        fins.setEnabled(true);
        QTRY_VERIFY_WITH_TIMEOUT(!fins.error(), 5000);
        stato.setComOK(true);

        QSignalSpy ordEvents(&stato, &ClasseStato::ord);
        plc.setDm(3600, {0x0071, 0x0000});
        plc.setDm(3602, quint16(stagId.toInt()));
        stato.aggiorna(fins);
        QVERIFY(!ordEvents.isEmpty());

        // CAM_ORD_COMPLETATO 0 -> 1: Shifta moves the current order to the executed list.
        plc.setDm(3600, quint16(0x8071));
        stato.aggiorna(fins);
        QCOMPARE(ordMac.Count(), count - 1);
        QCOMPARE(ordMacEse.Count(), qMin(countEse + 1, MAX_NUM_ORD_ESE));
        QCOMPARE(ordMacEse.item(1)->text(QStringLiteral("old_stag_id")), stagId);
        QCOMPARE(ordMacEse.item(1)->text(QStringLiteral("rep_pro.dat_fin")), QStringLiteral("10/22/20"));
        QCOMPARE(ordMacEse.item(1)->text(QStringLiteral("rep_pro.ora_fin")), QStringLiteral("14.13 PM"));

        // 1 -> 0: production report of the last order read from DM3630-3648.
        int liv = 0, format = 0;
        for (int i = 1; i <= 3 && !liv; ++i) {
            if (vb::val(first->ord(i, QStringLiteral("Ord_liv"))) != 0) {
                liv = int(vb::val(first->ord(i, QStringLiteral("Ord_liv"))));
                format = i;
            }
        }
        QVERIFY(liv >= 1 && liv <= 2);
        QVector<quint16> report(19, 0);
        report[0] = quint16(stagId.toInt());
        report[1 + (liv - 1) * 4] = 4000;     // fogli buoni (low word)
        report[3 + (liv - 1) * 4] = 4;        // fogli errati
        plc.setDm(3630, report);
        plc.setDm(3600, quint16(0x0071));
        stato.aggiorna(fins);
        const double resa = vb::val(first->ord(format, QStringLiteral("Sca_per_tag")));
        const QString f = QStringLiteral("rep_pro.liv(%1).").arg(format);
        QCOMPARE(vb::val(ordMacEse.item(1)->text(f + QStringLiteral("fog_buo"))), 4000 * resa);
        QCOMPARE(vb::val(ordMacEse.item(1)->text(f + QStringLiteral("fog_err"))), 4 * resa);
        QVERIFY(vb::val(ordMacEse.item(1)->text(QStringLiteral("rep_pro.met_tot"))) > 0);
        // 13:00 -> 14:13 read back as minutes.seconds, like the original
        QCOMPARE(vb::trim(ordMacEse.item(1)->text(QStringLiteral("rep_pro.tem_mar"))), QStringLiteral("4380"));
        vb::setNow(QDateTime());

        sta_tag = nullptr;
        ord_mac = nullptr;
        ord_mac_ese = nullptr;
    }
};

QTEST_GUILESS_MAIN(TestCambioOrdine)
#include "tst_cambioordine.moc"
