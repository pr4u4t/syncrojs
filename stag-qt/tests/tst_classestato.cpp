#include "classestato.h"
#include "finsclient.h"
#include "finssimulator.h"
#include "parmac.h"
#include "staglog.h"
#include "vbcompat.h"

#include <QSignalSpy>
#include <QTest>

class TestClasseStato : public QObject
{
    Q_OBJECT

private slots:
    void vbHexHelpers()
    {
        QCOMPARE(vb::intEsa(0x0E71), QStringLiteral("0E71"));
        QCOMPARE(vb::intEsa(-1), QStringLiteral("FFFF"));
        QCOMPARE(vb::intEsa(0x12345), QStringLiteral("2345"));
        QCOMPARE(vb::hexBin(QStringLiteral("0E71"), false), QStringLiteral("0000111001110001"));
        QCOMPARE(vb::hexBin(QStringLiteral("0E71"), true), QStringLiteral("1000111001110000"));
        QCOMPARE(vb::legLng(QStringLiteral("5678"), QStringLiteral("1234")), double(0x12345678));
        QCOMPARE(vb::legLng(QStringLiteral("FFFF"), QStringLiteral("FFFF")), 4294967295.0);
        QCOMPARE(vb::hexDec(QStringLiteral("0080")), 128.0);
    }

    void oemCodePages()
    {
        QCOMPARE(vb::fromOem(QByteArray("M\xA0quina")), QStringLiteral("M\u00E1quina"));
        QCOMPARE(vb::fromOem(QByteArray("H\x99G")), QStringLiteral("H\u00D6G"));
        QCOMPARE(vb::fromOem(QByteArray("\x82\x9B\x91\x8E\x8A\x88\x89"), vb::OemCodePage::Cp866), QStringLiteral("\u0412\u042B\u0421\u041E\u041A\u0418\u0419"));
    }

    void assStatoDecodesDmImage()
    {
        ClasseStato stato;
        QVector<qint32> dm(100, 0);
        dm[0] = 0x0E71;
        dm[1] = 0x4460;
        dm[2] = 9183;
        dm[3] = 1200;
        dm[4] = 0x5678;
        dm[5] = 0x1234;
        dm[7] = 0xFFFE;
        dm[20] = 12;
        dm[50] = 93;
        dm[60] = 0x0002;
        dm[61] = 0x0001;
        stato.assStato(dm);
        // Plc.log 12/10/26 13.19.28
        QCOMPARE(stato.stato().staPlc, QStringLiteral("10001110011100000000011000100010"));
        QCOMPARE(stato.stato().codOrdCor, QStringLiteral("9183"));
        QCOMPARE(stato.stato().codOrdSuc, QStringLiteral("12  "));
        QCOMPARE(stato.stato().ordPlc[1].lunTag, 1200);
        QCOMPARE(stato.stato().ordPlc[1].numFogBuo, double(0x12345678));
        QCOMPARE(stato.stato().ordPlc[1].errTag, 0xFFFE);
        QCOMPARE(stato.stato().velLin[1], 93);
        QCOMPARE(stato.stato().gruAll, QStringLiteral("0000000000000010"));
        QCOMPARE(stato.stato().allAtt.left(16), QStringLiteral("1000000000000000"));
        QCOMPARE(stato.stato().allAtt.size(), 336);
        QVERIFY(stato.bit(ClasseStato::MAC_RIPRISTINATA, 1));
        QVERIFY(stato.bit(ClasseStato::MAC_IN_MANUALE, 0));
    }

    void softwareAlarmsWithoutPlc()
    {
        stag::par_mac.car_tec.num_liv_tag = "2";
        stag::par_abi.abi_com_PU = true;
        stag::par_abi.abi_err_com_PU = true;
        ClasseStato stato;
        QSignalSpy sezall(&stato, &ClasseStato::sezall);
        QSignalSpy gruall(&stato, &ClasseStato::gruall);
        stato.chkAllSftw();
        // no orders (1+2), no PLC (8), knife boards 1-2 not initialised (16+32)
        QCOMPARE(stato.stato().allAtt.right(16), QStringLiteral("1101110000000000"));
        QCOMPARE(stato.stato().gruAll, QStringLiteral("0000000010000000"));
        QCOMPARE(sezall.size(), 1);
        QCOMPARE(gruall.size(), 1);
        stato.chkAllSftw();
        QCOMPARE(sezall.size(), 1);
    }

    void aggiornaRaisesEventsFromPlc()
    {
        FinsSimulator plc;
        QVERIFY(plc.listen(QHostAddress::LocalHost, 0));
        plc.setDm(3600, {0x0E71, 0x4460});
        plc.setDm(3650, quint16(93));

        FinsClient fins;
        fins.setRemoteHost(QStringLiteral("127.0.0.1"));
        fins.setRemotePort(plc.port());
        fins.setTimeOut(200);

        ClasseStato stato;
        stag::par_mac.car_tec.num_liv_tag = "2";
        stag::par_mac.par_cfg_att(1).vel_max_tag = "250";
        stag::par_mac.par_cfg_att(2).vel_max_tag = "200";
        stag::par_mac.par_cfg_att(3).vel_max_tag = "999";
        QSignalSpy run(&stato, &ClasseStato::run);
        QSignalSpy reset(&stato, &ClasseStato::reset);
        QSignalSpy modo(&stato, &ClasseStato::modo);
        QSignalSpy mac(&stato, &ClasseStato::mac);
        QSignalSpy vel(&stato, &ClasseStato::vel);
        QSignalSpy velmax(&stato, &ClasseStato::velmax);
        QSignalSpy ord(&stato, &ClasseStato::ord);
        QSignalSpy logged(&StagLog::instance(), &StagLog::shown);

        stato.aggiorna(fins);
        QCOMPARE(run.takeFirst().at(0).toInt(), 7);
        QCOMPARE(reset.takeFirst().at(0).toInt(), int(ClasseStato::ness_reset));

        fins.setEnabled(true);
        QTRY_VERIFY_WITH_TIMEOUT(!fins.error(), 5000);
        stato.setComOK(true);
        stato.aggiorna(fins);
        QCOMPARE(run.takeFirst().at(0).toInt(), int(ClasseStato::pro_cam));
        QCOMPARE(reset.takeFirst().at(0).toInt(), int(ClasseStato::rip));
        QCOMPARE(modo.takeFirst().at(0).toInt(), int(ClasseStato::automatico));
        QCOMPARE(mac.size(), 2);
        QCOMPARE(mac.last().at(0).toInt(), int(ClasseStato::ness_mac));
        QCOMPARE(vel.takeFirst().at(0).toInt(), 93);
        QCOMPARE(velmax.takeFirst().at(0).toInt(), 200);
        QCOMPARE(ord.size(), 2);

        bool stateLogged = false;
        for (const QList<QVariant> &line : logged)
            stateLogged |= line.at(0).toString().endsWith(QStringLiteral("| Stato Macchina: 10001110011100000000011000100010"));
        QVERIFY(stateLogged);

        // Plc.log 13.21.30: bit 7 (in preallarme) rises.
        plc.setDm(3600, quint16(0x0EF1));
        stato.aggiorna(fins);
        QCOMPARE(run.takeFirst().at(0).toInt(), int(ClasseStato::pre));
        QVERIFY(reset.isEmpty());
        QVERIFY(vel.isEmpty());
    }
};

QTEST_GUILESS_MAIN(TestClasseStato)
#include "tst_classestato.moc"
