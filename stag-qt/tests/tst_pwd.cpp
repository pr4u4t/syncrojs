#include "abilitazionimodel.h"
#include "classestato.h"
#include "collezioneordini.h"
#include "inibas.h"
#include "inifile.h"
#include "inistore.h"
#include "messages.h"
#include "pagpromodel.h"
#include "parmac.h"
#include "pwdmodel.h"
#include "stagglobals.h"
#include "uihooks.h"

#include <QFile>
#include <QSignalSpy>
#include <QTemporaryDir>
#include <QTest>

using namespace stag;

// Test passwords only; the machine's real ones never appear here.
class TestPwd : public QObject
{
    Q_OBJECT

private:
    QTemporaryDir *m_dir = nullptr;
    ClasseStato *m_state = nullptr;
    QString m_message;

    void type(PwdModel &model, const QString &control, const QString &text)
    {
        model.controlEvent(control, -1, QStringLiteral("Change"), {{QStringLiteral("text"), text}});
        model.controlEvent(control, -1, QStringLiteral("KeyDown"), {{QStringLiteral("key"), int(Qt::Key_Return)}});
    }
    QString iniValue(const QString &item)
    {
        return getFileINIString(QStringLiteral("PRO_SET"), item, QStringLiteral("<none>"));
    }

private slots:
    void initTestCase()
    {
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 1));
        messaggioHook = [this](const QString &text, bool, bool) { m_message = text; };
    }

    void init()
    {
        delete m_dir;
        m_dir = new QTemporaryDir;
        const QString target = m_dir->filePath(QStringLiteral("stag.ini"));
        QVERIFY(QFile::copy(QStringLiteral(STAG_REFERENCE_DATA "/stag.ini"), target));
        QFile::setPermissions(target, QFile::ReadOwner | QFile::WriteOwner);
        setDataDirectory(m_dir->path());
        legParSup(iniFile(), QString(), m_dir->path() + u'/');
        leggiNumLivTag(iniFile());
        legParMac(iniFile());
        pro_set.pwd = PwdModel::code(QStringLiteral("MAIN"));
        pro_set.pwd_liv1 = PwdModel::code(QStringLiteral("LEVEL"));
        pro_set.ute_wrk = false;
        pro_set.ute_wrk_liv1 = false;
        delete m_state;
        m_state = new ClasseStato;
        stag::sta_tag = m_state;
        m_message.clear();
    }

    void cleanupTestCase() { stag::sta_tag = nullptr; }

    void codingShiftsEachCharacterByItsPosition()
    {
        QCOMPARE(PwdModel::code(QStringLiteral("ABC")), QStringLiteral("BDF"));
        QCOMPARE(PwdModel::decodeShift(QStringLiteral("BDF")), QStringLiteral("ABC"));
    }

    void mainPasswordOpensTheServicePanel()
    {
        PwdModel model;
        model.activate(QString());
        QVERIFY(model.value(QStringLiteral("frm_pwd"), QStringLiteral("visible")).toBool());
        type(model, QStringLiteral("txt_pwd"), QStringLiteral("main"));
        QVERIFY(pro_set.ute_wrk);
        QVERIFY(model.value(QStringLiteral("frm_set"), QStringLiteral("visible")).toBool());
    }

    void levelPasswordClosesAndAllowsOneEntry()
    {
        PwdModel model;
        QSignalSpy close(&model, &FormModel::closeRequested);
        model.activate(QString());
        type(model, QStringLiteral("txt_pwd"), QStringLiteral("level"));
        QVERIFY(pro_set.ute_wrk_liv1);
        QVERIFY(!pro_set.ute_wrk);
        QCOMPARE(close.count(), 1);

        CollezioneOrdini orders;
        PagProModel page(&orders);
        QCOMPARE(page.navigationTarget(QStringLiteral("F3"), 0), QStringLiteral("Set_par_mac"));
        QCOMPARE(page.navigationTarget(QStringLiteral("F3"), 0), QStringLiteral("Pwd_frm"));
    }

    void messagesFollowTheLanguage()
    {
        PwdModel model;
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 8));
        model.activate(QString());
        type(model, QStringLiteral("txt_pwd"), QStringLiteral("nope"));
        const QString polish = model.value(QStringLiteral("txt_msg")).toString();
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 2));
        type(model, QStringLiteral("txt_pwd"), QStringLiteral("nope"));
        const QString german = model.value(QStringLiteral("txt_msg")).toString();
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 1));
        QCOMPARE(polish, QStringLiteral("Bledne haslo: sprobuj ponownie"));
        QCOMPARE(german, QStringLiteral("Falsches Passwort: erneut versuchen"));
    }

    void wrongPasswordStaysAndEmptyCloses()
    {
        PwdModel model;
        QSignalSpy close(&model, &FormModel::closeRequested);
        model.activate(QString());
        type(model, QStringLiteral("txt_pwd"), QStringLiteral("nope"));
        QCOMPARE(model.value(QStringLiteral("txt_msg")).toString(), QStringLiteral("Wrong password: try again"));
        QCOMPARE(close.count(), 0);
        QVERIFY(!pro_set.ute_wrk);
        type(model, QStringLiteral("txt_pwd"), QString());
        QCOMPARE(close.count(), 1);
    }

    void levelPasswordChangeWritesTheNewOne()
    {
        PwdModel model;
        model.activate(QString());
        QVERIFY(model.functionKey(QStringLiteral("F8"), 0));
        type(model, QStringLiteral("txt_chg_pwd"), QStringLiteral("level"));
        type(model, QStringLiteral("txt_chg_pwd"), QStringLiteral("other"));
        type(model, QStringLiteral("txt_chg_pwd"), QStringLiteral("other"));
        QCOMPARE(pro_set.pwd_liv1, PwdModel::code(QStringLiteral("OTHER")));
        QCOMPARE(iniValue(QStringLiteral("Pwd_Liv1")), PwdModel::code(QStringLiteral("OTHER")));
        QCOMPARE(model.value(QStringLiteral("txt_msg")).toString(), QStringLiteral("New password saved"));
    }

    void mismatchedConfirmationChangesNothing()
    {
        PwdModel model;
        model.activate(QString());
        model.functionKey(QStringLiteral("F8"), 0);
        type(model, QStringLiteral("txt_chg_pwd"), QStringLiteral("level"));
        type(model, QStringLiteral("txt_chg_pwd"), QStringLiteral("other"));
        type(model, QStringLiteral("txt_chg_pwd"), QStringLiteral("typo"));
        QCOMPARE(pro_set.pwd_liv1, PwdModel::code(QStringLiteral("LEVEL")));
        QVERIFY(model.value(QStringLiteral("frm_pwd"), QStringLiteral("visible")).toBool());
    }

    // VB's F9 path calls decode(pro_set.pwd) with the level flag off, so the main password lands in Pwd_liv1.
    void changingTheMainPasswordOverwritesTheLevelOneInTheIni()
    {
        PwdModel model;
        model.activate(QString());
        type(model, QStringLiteral("txt_pwd"), QStringLiteral("main"));
        QVERIFY(model.functionKey(QStringLiteral("F9"), 0));
        type(model, QStringLiteral("txt_chg_pwd"), QStringLiteral("main"));
        QCOMPARE(iniValue(QStringLiteral("pwd_liv1")), PwdModel::code(QStringLiteral("MAIN")));
    }

    void serviceProgramsNeedThePasswordAndAreNotStarted()
    {
        PwdModel model;
        model.activate(QString());
        QVERIFY(model.functionKey(QStringLiteral("F3"), 0));
        QVERIFY(m_message.isEmpty());
        pro_set.ute_wrk = true;
        QVERIFY(model.functionKey(QStringLiteral("F3"), 0));
        QCOMPARE(m_message, QStringLiteral("Application not available"));
    }

    void leavingWithF10DropsTheRights()
    {
        PwdModel model;
        pro_set.ute_wrk = true;
        pro_set.ute_wrk_liv1 = true;
        QVERIFY(!model.functionKey(QStringLiteral("F10"), 0));
        QVERIFY(!pro_set.ute_wrk);
        QVERIFY(!pro_set.ute_wrk_liv1);
    }

    void switchesShowTheMachineConfiguration()
    {
        CollezioneOrdini orders;
        AbilitazioniModel model(&orders);
        par_abi.liv(1) = true;
        par_abi.liv(2) = false;
        model.activate(QString());
        QVERIFY(model.isOn(QStringLiteral("liv(1)")));
        QCOMPARE(model.value(QStringLiteral("liv(1)")).toString(), QStringLiteral("ON"));
        QCOMPARE(model.value(QStringLiteral("liv(2)")).toString(), QStringLiteral("OFF"));
        QVERIFY(!model.value(QStringLiteral("liv(3)"), QStringLiteral("enabled")).toBool());
    }

    void communicationSwitchIsLocalAndSavedToTheIni()
    {
        CollezioneOrdini orders;
        AbilitazioniModel model(&orders);
        par_abi.abi_com_PU = true;
        model.activate(QString());
        model.controlEvent(QStringLiteral("abi_com_pu"), -1, QStringLiteral("Click"), {{QStringLiteral("checked"), false}});
        QVERIFY(!par_abi.abi_com_PU);
        QCOMPARE(model.value(QStringLiteral("abi_com_pu")).toString(), QStringLiteral("OFF"));
        QCOMPARE(getFileINIString(QStringLiteral("abilitazioni"), QStringLiteral("abi_com_pu"), QString()), QStringLiteral("0"));
    }

    void machineSwitchesFollowTheVbChecksAndNeverChange()
    {
        CollezioneOrdini orders;
        AbilitazioniModel model(&orders);
        par_abi.liv(1) = true;
        model.activate(QString());
        const auto press = [&] {
            model.controlEvent(QStringLiteral("liv"), 1, QStringLiteral("Click"), {{QStringLiteral("checked"), false}});
        };
        press();
        QCOMPARE(m_message, msg(QStringLiteral("acc_pwd")));
        QVERIFY(model.isOn(QStringLiteral("liv(1)")));
        QVERIFY(par_abi.liv(1));

        pro_set.ute_wrk = true;
        m_state->setComOK(false);
        press();
        QCOMPARE(m_message, msg(QStringLiteral("com_int_con_mac")));

        m_state->setComOK(true);
        press();
        QCOMPARE(m_message, msg(QStringLiteral("mac_non_fas_sto")));
        QVERIFY(par_abi.liv(1));
    }
};

QTEST_GUILESS_MAIN(TestPwd)
#include "tst_pwd.moc"
