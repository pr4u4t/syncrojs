#include "classestato.h"
#include "collezioneordini.h"
#include "inibas.h"
#include "inistore.h"
#include "messages.h"
#include "orderdialogmodel.h"
#include "paglisordmodel.h"
#include "parmac.h"
#include "stagglobals.h"
#include "uihooks.h"

#include <QFile>
#include <QSet>
#include <QTemporaryDir>
#include <QTest>

using namespace stag;

class TestOrderDialogs : public QObject
{
    Q_OBJECT

private:
    QTemporaryDir *m_dir = nullptr;
    CollezioneOrdini *m_orders = nullptr;
    CollezioneOrdini *m_done = nullptr;
    ClasseStato *m_state = nullptr;
    QString m_message;

    void type(OrderDialogModel &model, const QString &control, const QString &text)
    {
        model.controlEvent(control, -1, QStringLiteral("Change"), {{QStringLiteral("text"), text}});
    }
    QStringList ids(const CollezioneOrdini &list) const
    {
        QStringList result;
        for (int i = 1; i <= list.Count(); ++i)
            result << list.item(i)->cod_ord_abb().trimmed();
        return result;
    }

private slots:
    void initTestCase()
    {
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 1));
        messaggioHook = [this](const QString &text, bool, bool) { m_message = text; };
        par_abi.abi_com_PU = false; // deletion is then allowed whatever the machine state
    }

    // A list of 100 real orders, from the executed file, for each test.
    void init()
    {
        delete m_dir;
        m_dir = new QTemporaryDir;
        for (const char *name : {"Ord_mac.dat", "Ord_mac_ese.dat", "Ordini.ini", "stag.ini"}) {
            const QString target = m_dir->filePath(QString::fromLatin1(name));
            QFile::copy(QStringLiteral(STAG_REFERENCE_DATA "/") + QLatin1String(name), target);
            QFile::setPermissions(target, QFile::ReadOwner | QFile::WriteOwner);
        }
        setDataDirectory(m_dir->path());
        legParSup(iniFile(), QString(), m_dir->path() + u'/');
        par_abi.abi_com_PU = false;
        delete m_state;
        m_state = new ClasseStato;
        stag::sta_tag = m_state;
        delete m_orders;
        delete m_done;
        m_orders = new CollezioneOrdini;
        m_done = new CollezioneOrdini;
        QCOMPARE(m_orders->Init(false), int(RET_OK));
        m_orders->Leg_dat_ord();
        QCOMPARE(m_done->Init(true), int(RET_OK));
        m_done->Leg_dat_ord();
        stag::ord_mac = m_orders;
        stag::ord_mac_ese = m_done;
        m_message.clear();
    }

    void cleanupTestCase()
    {
        stag::sta_tag = nullptr;
        stag::ord_mac = nullptr;
        stag::ord_mac_ese = nullptr;
    }

    void listFillsTheDialogWithTheSelectedOrder()
    {
        OrderDialogModel dialog(OrderDialogModel::DeleteExecuted, m_done, m_orders);
        PagLisOrdModel list(m_done);
        list.setDialog(QStringLiteral("F3"), &dialog);
        list.load();
        list.activate(QStringLiteral("Pag_pro"));
        list.keyUp(Qt::Key_Down);
        QVERIFY(!list.functionKey(QStringLiteral("F3"), 0));
        QCOMPARE(dialog.field(QStringLiteral("txt_from")), QStringLiteral("2"));
        QCOMPARE(dialog.field(QStringLiteral("txt_to")), QStringLiteral("2"));
    }

    void deleteRemovesTheRangeAndClosesTheDialog()
    {
        const QStringList before = ids(*m_done);
        OrderDialogModel dialog(OrderDialogModel::DeleteExecuted, m_done, m_orders);
        dialog.prefill(2);
        type(dialog, QStringLiteral("txt_to"), QStringLiteral("3"));
        QVERIFY(!dialog.functionKey(QStringLiteral("F11"), 0));
        QCOMPARE(m_message, msg(QStringLiteral("ope_eff")));
        QCOMPARE(m_done->Count(), 98);
        QCOMPARE(ids(*m_done).at(1), before.at(3));
    }

    void wrongRangesStayOpenAndClearTheFields_data()
    {
        QTest::addColumn<QString>("from");
        QTest::addColumn<QString>("to");
        QTest::addColumn<QString>("clearedFrom");
        QTest::addColumn<QString>("clearedTo");
        QTest::newRow("to beyond the list") << "1" << "101" << "1" << "";
        QTest::newRow("from after to") << "5" << "2" << "" << "";
        QTest::newRow("from zero") << "0" << "2" << "" << "";
    }

    void wrongRangesStayOpenAndClearTheFields()
    {
        QFETCH(QString, from);
        QFETCH(QString, to);
        QFETCH(QString, clearedFrom);
        QFETCH(QString, clearedTo);
        OrderDialogModel dialog(OrderDialogModel::DeleteExecuted, m_done, m_orders);
        type(dialog, QStringLiteral("txt_from"), from);
        type(dialog, QStringLiteral("txt_to"), to);
        QVERIFY(dialog.functionKey(QStringLiteral("F11"), 0));
        QCOMPARE(m_message, msg(QStringLiteral("par_err")));
        QCOMPARE(dialog.field(QStringLiteral("txt_from")), clearedFrom);
        QCOMPARE(dialog.field(QStringLiteral("txt_to")), clearedTo);
        QCOMPARE(m_done->Count(), 100);
    }

    void emptyFieldsAskForParameters()
    {
        OrderDialogModel dialog(OrderDialogModel::MoveOrders, m_orders, m_orders);
        type(dialog, QStringLiteral("txt_from"), QStringLiteral("1"));
        type(dialog, QStringLiteral("txt_to"), QStringLiteral("1"));
        QVERIFY(dialog.functionKey(QStringLiteral("F11"), 0));
        QCOMPARE(m_message, msg(QStringLiteral("par_man")));
    }

    void inAutomaticTheCollectionRefusesAndTheDialogStays()
    {
        par_abi.abi_com_PU = true;
        m_state->setSpdeOK(true);
        QVERIFY(!m_state->bit(ClasseStato::MAC_IN_MANUALE, 1));
        OrderDialogModel dialog(OrderDialogModel::DeleteExecuted, m_done, m_orders);
        dialog.prefill(1);
        QVERIFY(dialog.functionKey(QStringLiteral("F11"), 0));
        QCOMPARE(m_message, msg(QStringLiteral("mac_in_auto")));
        QCOMPARE(m_done->Count(), 100);
    }

    void findMovesThePointerOrClearsTheCode()
    {
        OrderDialogModel dialog(OrderDialogModel::FindExecuted, m_done, m_orders);
        const QString wanted = m_done->item(5)->cod_ord_abb().trimmed();
        type(dialog, QStringLiteral("txt_cod"), wanted);
        QVERIFY(!dialog.functionKey(QStringLiteral("F11"), 0));
        QCOMPARE(m_done->leg_pun_ord(), 5);
        type(dialog, QStringLiteral("txt_cod"), QStringLiteral("NOPE"));
        QVERIFY(dialog.functionKey(QStringLiteral("F11"), 0));
        QCOMPARE(m_message, msg(QStringLiteral("cod_non_pre")));
        QCOMPARE(dialog.field(QStringLiteral("txt_cod")), QString());
    }

    void copyExecutedClearsTheProductionData()
    {
        m_state->setSpdeOK(false);
        const int before = m_orders->Count();
        const OrdinePtr source = m_done->item(1);
        QCOMPARE(cop_ese(*m_done, *m_orders, 1, 1, 1), int(RET_OK));
        QCOMPARE(m_orders->Count(), before + 1);
        const OrdinePtr copy = m_orders->item(1);
        QCOMPARE(copy->cod_ord_abb(), source->cod_ord_abb());
        QCOMPARE(copy->text(QStringLiteral("rep_pro.dat_ini")).trimmed(), QString());
        QCOMPARE(copy->text(QStringLiteral("rep_pro.liv(1).fog_buo")).trimmed(), QStringLiteral("0"));
        QVERIFY(source->text(QStringLiteral("rep_pro.liv(1).fog_buo")).trimmed() != QStringLiteral("0"));
    }

    void returnWalksTheTabOrderOfTheForm()
    {
        OrderDialogModel dialog(OrderDialogModel::MoveOrders, m_orders, m_orders);
        dialog.activate(QString());
        const int to = dialog.value(QStringLiteral("txt_to"), QStringLiteral("focusCount")).toInt();
        QVERIFY(to > 0);
        dialog.controlEvent(QStringLiteral("txt_to"), -1, QStringLiteral("KeyDown"), {{QStringLiteral("key"), int(Qt::Key_Return)}});
        QVERIFY(dialog.value(QStringLiteral("txt_dst"), QStringLiteral("focusCount")).toInt() > to);
        dialog.controlEvent(QStringLiteral("txt_dst"), -1, QStringLiteral("KeyDown"), {{QStringLiteral("key"), int(Qt::Key_Return)}});
        QVERIFY(dialog.value(QStringLiteral("txt_from"), QStringLiteral("focusCount")).toInt() > to);
    }

    void movedOrdersAreSavedToTheFile()
    {
        m_state->setSpdeOK(false);
        QCOMPARE(cop_ese(*m_done, *m_orders, 1, 3, 1), int(RET_OK));
        const QStringList before = ids(*m_orders);
        QCOMPARE(before.size(), 4);

        OrderDialogModel dialog(OrderDialogModel::MoveOrders, m_orders, m_orders);
        type(dialog, QStringLiteral("txt_from"), QStringLiteral("1"));
        type(dialog, QStringLiteral("txt_to"), QStringLiteral("1"));
        type(dialog, QStringLiteral("txt_dst"), QStringLiteral("3"));
        QVERIFY(!dialog.functionKey(QStringLiteral("F11"), 0));
        QCOMPARE(m_message, msg(QStringLiteral("ope_eff")));
        const QStringList moved = ids(*m_orders);
        QVERIFY(moved != before);
        QCOMPARE(QSet<QString>(moved.cbegin(), moved.cend()), QSet<QString>(before.cbegin(), before.cend()));

        m_orders->Fine();
        CollezioneOrdini reloaded;
        QCOMPARE(reloaded.Init(false), int(RET_OK));
        reloaded.Leg_dat_ord();
        QCOMPARE(ids(reloaded), moved);
    }

    void copyAddsUnderNewIdsAndRefusesInAutomatic()
    {
        m_state->setSpdeOK(false);
        QCOMPARE(cop_ese(*m_done, *m_orders, 1, 1, 1), int(RET_OK));
        const QString firstId = m_orders->item(1)->stag_id();
        OrderDialogModel dialog(OrderDialogModel::CopyOrders, m_orders, m_orders);
        type(dialog, QStringLiteral("txt_from"), QStringLiteral("1"));
        type(dialog, QStringLiteral("txt_to"), QStringLiteral("1"));
        type(dialog, QStringLiteral("txt_dst"), QStringLiteral("3"));
        QVERIFY(!dialog.functionKey(QStringLiteral("F11"), 0));
        QCOMPARE(m_orders->Count(), 3);
        QCOMPARE(m_orders->item(3)->cod_ord_abb(), m_orders->item(1)->cod_ord_abb());
        QVERIFY(m_orders->item(3)->stag_id() != firstId);

        m_state->setSpdeOK(true);
        QVERIFY(dialog.functionKey(QStringLiteral("F11"), 0));
        QCOMPARE(m_message, msg(QStringLiteral("mac_in_auto")));
        QCOMPARE(m_orders->Count(), 3);
        QCOMPARE(cop_ese(*m_done, *m_orders, 1, 1, 1), int(RET_BAD));
        QCOMPARE(m_message, msg(QStringLiteral("mac_in_auto")));
    }
};

QTEST_GUILESS_MAIN(TestOrderDialogs)
#include "tst_orderdialogs.moc"
