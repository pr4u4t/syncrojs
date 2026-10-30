#include "collezioneordini.h"
#include "inibas.h"
#include "inistore.h"
#include "messages.h"
#include "parcfgschtag.h"
#include "parmac.h"
#include "validazioneordini.h"

#include <QFile>
#include <QSqlDatabase>
#include <QSqlQuery>
#include <QTemporaryDir>
#include <QTest>

using namespace stag;

class TestValidazione : public QObject
{
    Q_OBJECT

private:
    QTemporaryDir m_dir;
    QSqlDatabase m_db;
    CollezioneOrdini *m_done = nullptr;
    CollezioneOrdini *m_current = nullptr;

    OrdinePtr copyOf(int index)
    {
        auto copy = std::make_shared<ClasseOrdine>();
        copy->setOrdine(m_done->item(index)->ordine());
        return copy;
    }
    int validate(ClasseOrdine &order) { return val_ord(order, m_db); }

private slots:
    void initTestCase()
    {
        for (const char *name : {"Ord_mac.dat", "Ord_mac_ese.dat", "Ordini.ini", "stag.ini"}) {
            const QString target = m_dir.filePath(QString::fromLatin1(name));
            QFile::copy(QStringLiteral(STAG_REFERENCE_DATA "/") + QLatin1String(name), target);
            QFile::setPermissions(target, QFile::ReadOwner | QFile::WriteOwner);
        }
        setDataDirectory(m_dir.path());
        legParSup(iniFile(), QString(), m_dir.path() + u'/');
        const QString copy = m_dir.filePath(QStringLiteral("stag.sqlite"));
        QVERIFY(QFile::copy(QStringLiteral(STAG_SQLITE), copy));
        QFile::setPermissions(copy, QFile::ReadOwner | QFile::WriteOwner);
        m_db = QSqlDatabase::addDatabase(QStringLiteral("QSQLITE"), QStringLiteral("validation"));
        m_db.setDatabaseName(copy);
        QVERIFY(m_db.open());
        leggiNumLivTag(iniFile());
        legParMac(iniFile());
        leggiCfg(m_db, iniFile());
        iniLiv();
        abi_mis_ang = false;
        m_done = new CollezioneOrdini(this);
        QCOMPARE(m_done->Init(true), int(RET_OK));
        m_done->Leg_dat_ord();
        m_current = new CollezioneOrdini(this);
        QCOMPARE(m_current->Init(false), int(RET_OK));
        m_current->Leg_dat_ord();
    }

    // Every one of these orders went through the VB validation on the machine and was produced.
    void everyOrderTheMachineRanPassesValidation()
    {
        QStringList refused;
        int checked = 0;
        for (int i = 1; i <= m_done->Count(); ++i) {
            OrdinePtr order = copyOf(i);
            const int result = validate(*order);
            ++checked;
            if (result != RET_OK)
                refused << QStringLiteral("%1: %2").arg(order->cod_ord_abb().trimmed()).arg(result);
        }
        QCOMPARE(checked, 100);
        QVERIFY2(refused.isEmpty(), qPrintable(refused.join(u',')));

        auto current = std::make_shared<ClasseOrdine>();
        current->setOrdine(m_current->item(1)->ordine());
        QCOMPARE(validate(*current), int(RET_OK));
    }

    void orderChecksFollowTheVbSequence_data()
    {
        QTest::addColumn<QString>("field");
        QTest::addColumn<QString>("value");
        QTest::addColumn<int>("expected");
        QTest::newRow("no code") << "cod_ord_abb" << " " << COD_ERR_LUN_COD;
        QTest::newRow("no flute") << "tip_car.Cod_ond" << "   " << COD_ERR_ASS_OND;
        QTest::newRow("no paper") << "tip_car.cod_com_car" << " " << COD_ERR_ASS_CAR;
        QTest::newRow("first level blank") << "ord(1).Ord_liv" << " " << COD_ERR_ASS_DAT;
        QTest::newRow("level 3 on a two level machine") << "ord(1).Ord_liv" << "3" << COD_ERR_ASS_DAT;
        QTest::newRow("format wider than the reel") << "ord(1).sca_lar" << "99990" << COD_ERR_MAX_SCA_LAR;
        QTest::newRow("sheet below the minimum") << "ord(1).sca_lun" << "04990" << COD_ERR_SCA_LUN;
        QTest::newRow("sheet over the maximum") << "ord(1).sca_lun" << "60010" << COD_ERR_SCA_LUN;
        QTest::newRow("too many outs for the reel") << "ord(1).Sca_per_tag" << "9" << COD_ERR_MAX_SCA_LAR;
        QTest::newRow("no sheets") << "ord(1).sca_tot_num" << "     0" << COD_ERR_ASS_NUM_FOG;
        QTest::newRow("cut mode out of range") << "ord(1).cut_to_mark" << "3" << COD_ERR_ASS_CUT_TO_MARK;
        QTest::newRow("cut to mark not fitted") << "ord(1).cut_to_mark" << "1" << COD_ERR_CUT_TO_MARK;
        QTest::newRow("non ASCII job number") << "ord(1).com.cod_com_ord" << QString(QChar(0x20AC)) << COD_ERR_COD_COM;
    }

    void orderChecksFollowTheVbSequence()
    {
        QFETCH(QString, field);
        QFETCH(QString, value);
        QFETCH(int, expected);
        OrdinePtr order = copyOf(1);
        QCOMPARE(validate(*order), int(RET_OK));
        order = copyOf(1);
        order->setText(field, value);
        QCOMPARE(validate(*order), expected);
    }

    void twoFormatsOnALevelMustShareTheLength()
    {
        OrdinePtr order = copyOf(1);
        order->setText(QStringLiteral("ord(2).Ord_liv"), order->ord(1, QStringLiteral("Ord_liv")));
        order->setText(QStringLiteral("ord(2).sca_lun"), QStringLiteral("12340"));
        QCOMPARE(validate(*order), COD_ERR_LIV_LUN_DIV);
    }

    void aMissingFormatBlanksItsLevelAndNothingMayFollow()
    {
        OrdinePtr order = copyOf(1);
        order->setText(QStringLiteral("ord(2).Sca_per_tag"), QStringLiteral("0"));
        QCOMPARE(validate(*order), int(RET_OK));
        QCOMPARE(order->ord(2, QStringLiteral("Ord_liv")), QStringLiteral(" "));

        order = copyOf(1);
        order->setText(QStringLiteral("ord(1).Sca_per_tag"), QStringLiteral("0"));
        QCOMPARE(validate(*order), COD_ERR_ASS_DAT);
    }

    void aDisabledLevelIsRefused()
    {
        const bool saved = par_abi.liv(1);
        par_abi.liv(1) = false;
        OrdinePtr order = copyOf(1);
        order->setText(QStringLiteral("ord(1).Ord_liv"), QStringLiteral("1"));
        order->setText(QStringLiteral("ord(2).Ord_liv"), QStringLiteral("2"));
        QCOMPARE(validate(*order), COD_ERR_ABI_LIV);
        par_abi.liv(1) = saved;
    }

    void anEmptyOrderIsAccepted()
    {
        OrdinePtr order = copyOf(1);
        for (int i = 1; i <= 3; ++i)
            order->setText(QStringLiteral("ord(%1).Sca_per_tag").arg(i), QStringLiteral("0"));
        QCOMPARE(validate(*order), int(RET_OK));
    }

    // cut_pat_1 = 1 in the machine's stag.ini.
    void cutToPatternChecksAndStoresThePattern()
    {
        OrdinePtr order = copyOf(1);
        const int format = order->Ord_liv(1) == 1 ? 1 : 2;
        order->setText(QStringLiteral("ord(%1).cut_to_mark").arg(format), QStringLiteral("2"));
        // Orders from the planner hold zeros here, which count as a code; only a blank one is refused.
        order->setText(QStringLiteral("ord(%1).pat_cod").arg(format), QStringLiteral(" "));
        QCOMPARE(validate(*order), COD_ERR_COD_PAT);

        order->setText(QStringLiteral("ord(%1).pat_cod").arg(format), QStringLiteral("NEWPAT"));
        for (int l = 1; l <= 3; ++l)
            order->setText(QStringLiteral("ord(%1).L%2").arg(format).arg(l), QString::number(l * 10));
        QCOMPARE(validate(*order), int(RET_OK));
        QSqlQuery stored(m_db);
        QVERIFY(stored.exec(QStringLiteral("SELECT L1, L3, L4 FROM PATTERN WHERE CODICE = 'NEWPAT'")));
        QVERIFY(stored.next());
        QCOMPARE(stored.value(0).toString(), QStringLiteral("10"));
        QCOMPARE(stored.value(1).toString(), QStringLiteral("30"));
        QCOMPARE(stored.value(2).toString(), QStringLiteral("000")); // copied as the order holds it, before err_mis_pat clears it

        order->setText(QStringLiteral("ord(%1).L2").arg(format), QStringLiteral("5"));
        QCOMPARE(validate(*order), COD_ERR_POS_PAT);

        order->setText(QStringLiteral("ord(%1).L2").arg(format), QStringLiteral("20"));
        order->setText(QStringLiteral("ord(%1).pat_con").arg(format), QStringLiteral("5"));
        QCOMPARE(validate(*order), COD_ERR_PAT_CON);
    }

    void cutToPatternOnAnUnequippedLevelIsRefused()
    {
        OrdinePtr order = copyOf(1);
        const int format = order->Ord_liv(1) == 2 ? 1 : 2;
        QCOMPARE(order->Ord_liv(format), 2);
        order->setText(QStringLiteral("ord(%1).cut_to_mark").arg(format), QStringLiteral("2"));
        QCOMPARE(validate(*order), COD_ERR_CUT_TO_MARK);
    }

    void errorCodesNameTheirMessages()
    {
        QCOMPARE(msg_err_val_ord(COD_ERR_LIV_LUN_DIV), QStringLiteral("err_liv_lun_div"));
        QCOMPARE(msg_err_val_ord(COD_ERR_POS_PAT), QStringLiteral("err_pos_pat"));
        QCOMPARE(msg_err_val_ord(9999), QStringLiteral("imp_ese_ope"));
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 1));
        for (const int code : {COD_ERR_LUN_COD, COD_ERR_ASS_OND, COD_ERR_ASS_DAT, COD_ERR_SCA_LUN, COD_ERR_LIV_LUN_DIV})
            QVERIFY2(!Messages::instance().find(msg_err_val_ord(code)).isEmpty(), qPrintable(msg_err_val_ord(code)));
    }
};

QTEST_GUILESS_MAIN(TestValidazione)
#include "tst_validazione.moc"
