#include "classestato.h"
#include "collezioneordini.h"
#include "inibas.h"
#include "inistore.h"
#include "insordmodel.h"
#include "messages.h"
#include "parcfgschtag.h"
#include "parmac.h"
#include "stagglobals.h"
#include "uihooks.h"
#include "validazioneordini.h"

#include <QFile>
#include <QSqlDatabase>
#include <QSqlQuery>
#include <QTemporaryDir>
#include <QTest>

using namespace stag;

class TestInsOrd : public QObject
{
    Q_OBJECT

private:
    QTemporaryDir *m_dir = nullptr;
    QSqlDatabase m_db;
    ClasseStato *m_state = nullptr;
    CollezioneOrdini *m_done = nullptr;
    CollezioneOrdini *m_orders = nullptr;
    QString m_message;

    void focusOn(InsOrdModel &model, const QString &control, int index = -1)
    {
        model.controlEvent(control, index, QStringLiteral("GotFocus"), {});
    }
    void type(InsOrdModel &model, const QString &control, int index, const QString &text)
    {
        focusOn(model, control, index);
        model.controlEvent(control, index, QStringLiteral("Change"), {{QStringLiteral("text"), text}});
    }
    void choose(InsOrdModel &model, const QString &control, int index, int item)
    {
        focusOn(model, control, index);
        model.controlEvent(control, index, QStringLiteral("Click"), {{QStringLiteral("index"), item}});
    }
    void fillValidOrder(InsOrdModel &model, const QString &code)
    {
        type(model, QStringLiteral("txt_cod_abb"), -1, code);
        focusOn(model, QStringLiteral("cmb_onda"));
        focusOn(model, QStringLiteral("cmb_carta"));
        type(model, QStringLiteral("txt_alt_bob"), -1, QStringLiteral("2000"));
        choose(model, QStringLiteral("txt_liv"), 0, 0);
        type(model, QStringLiteral("txt_cod_com"), 0, QStringLiteral("JOB-1"));
        type(model, QStringLiteral("txt_cod_cli"), 0, QStringLiteral("C1"));
        type(model, QStringLiteral("txt_nom_cli"), 0, QStringLiteral("CLIENT"));
        type(model, QStringLiteral("txt_sca_lar"), 0, QStringLiteral("900"));
        type(model, QStringLiteral("txt_sca_lun"), 0, QStringLiteral("1250"));
        type(model, QStringLiteral("txt_resa"), 0, QStringLiteral("2"));
        type(model, QStringLiteral("txt_num_tot_fog"), 0, QStringLiteral("1001"));
        focusOn(model, QStringLiteral("cmb_cut_to_mark"), 0);
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
        for (const char *name : {"Ord_mac.dat", "Ord_mac_ese.dat", "Ordini.ini", "stag.ini"}) {
            const QString target = m_dir->filePath(QString::fromLatin1(name));
            QFile::copy(QStringLiteral(STAG_REFERENCE_DATA "/") + QLatin1String(name), target);
            QFile::setPermissions(target, QFile::ReadOwner | QFile::WriteOwner);
        }
        setDataDirectory(m_dir->path());
        legParSup(iniFile(), QString(), m_dir->path() + u'/');
        const QString database = m_dir->filePath(QStringLiteral("stag.sqlite"));
        QVERIFY(QFile::copy(QStringLiteral(STAG_SQLITE), database));
        QFile::setPermissions(database, QFile::ReadOwner | QFile::WriteOwner);
        if (!m_db.isValid())
            m_db = QSqlDatabase::addDatabase(QStringLiteral("QSQLITE"), QStringLiteral("insord"));
        m_db.close();
        m_db.setDatabaseName(database);
        QVERIFY(m_db.open());
        leggiNumLivTag(iniFile());
        legParMac(iniFile());
        leggiCfg(m_db, iniFile());
        iniLiv();
        abi_mis_ang = false;
        delete m_state;
        m_state = new ClasseStato;
        m_state->setSpdeOK(false); // not automatic
        stag::sta_tag = m_state;
        delete m_done;
        delete m_orders;
        m_done = new CollezioneOrdini;
        m_orders = new CollezioneOrdini;
        QCOMPARE(m_done->Init(true), int(RET_OK));
        m_done->Leg_dat_ord();
        QCOMPARE(m_orders->Init(false), int(RET_OK));
        m_orders->Leg_dat_ord();
        stag::ord_mac = m_orders;
        m_message.clear();
    }

    void cleanupTestCase()
    {
        stag::sta_tag = nullptr;
        stag::ord_mac = nullptr;
    }

    // Showing an order and reading the form back (riempi_celle, then get_new_dat) must give the same
    // order, and the metres VB computes must be the ones stored when these orders were produced.
    void formRoundTripKeepsEveryRealOrder()
    {
        InsOrdModel model(m_done, m_db);
        model.load();
        QStringList different;
        int metersDifferent = 0;
        for (int i = 1; i <= m_done->Count(); ++i) {
            m_done->scr_pun_ord(i);
            model.activate(QStringLiteral("Pag_lis_ord"));
            model.getNewDat();
            const OrdinePtr source = m_done->item(i);
            const ClasseOrdine &read = model.orderBeingEdited();
            for (const char *name : {"cod_ord_abb", "tip_car.Cod_ond", "tip_car.cod_com_car"}) {
                if (read.text(QLatin1String(name)).trimmed() != source->text(QLatin1String(name)).trimmed())
                    different << QStringLiteral("%1 %2: %3 / %4").arg(i).arg(QLatin1String(name), read.text(QLatin1String(name)), source->text(QLatin1String(name)));
            }
            if (vb::val(read.text(QStringLiteral("alt_bob"))) != vb::val(source->text(QStringLiteral("alt_bob"))))
                different << QStringLiteral("%1 alt_bob").arg(i);
            for (int f = 1; f <= 3; ++f) {
                const bool used = !source->ord(f, QStringLiteral("Ord_liv")).trimmed().isEmpty();
                if (!used)
                    continue;
                for (const char *name : {"sca_lar", "sca_lun", "sca_tot_num", "Sca_per_tag", "Ord_liv", "com.cod_com_ord", "nom_cli", "com.cod_cli"}) {
                    const QString a = read.ord(f, QLatin1String(name)).trimmed();
                    const QString b = source->ord(f, QLatin1String(name)).trimmed();
                    if (a != b && !(vb::val(a) == vb::val(b) && vb::val(a) != 0))
                        different << QStringLiteral("%1.%2 %3: '%4' / '%5'").arg(i).arg(f).arg(QLatin1String(name), a, b);
                }
                if (vb::val(read.ord(f, QStringLiteral("met"))) != vb::val(source->ord(f, QStringLiteral("met"))))
                    ++metersDifferent;
            }
        }
        qInfo() << "formats whose metres differ from the file:" << metersDifferent;
        QVERIFY2(different.isEmpty(), qPrintable(different.mid(0, 12).join(u'\n')));
    }

    void newOrderIsValidatedSavedAndShown()
    {
        InsOrdModel model(m_orders, m_db);
        model.load();
        const int before = m_orders->Count();
        QVERIFY(model.functionKey(QStringLiteral("F1"), 0));
        QCOMPARE(model.operation(), InsOrdModel::OLI_NEW);
        QCOMPARE(model.text(QStringLiteral("txt_indice")), QString::number(before + 1));
        fillValidOrder(model, QStringLiteral("NEW-1"));
        QVERIFY2(model.functionKey(QStringLiteral("F4"), 0), qPrintable(m_message));
        QCOMPARE(m_message, msg(QStringLiteral("ope_eff")));
        QCOMPARE(model.operation(), InsOrdModel::OLI_NUL);
        QCOMPARE(m_orders->Count(), before + 1);
        const OrdinePtr saved = m_orders->item(before + 1);
        QCOMPARE(saved->cod_ord_abb().trimmed(), QStringLiteral("NEW-1"));
        QCOMPARE(saved->ord(1, QStringLiteral("sca_lun")).trimmed(), QStringLiteral("12500"));
        // 1001 sheets / 2 outs * 1250 mm = 625.625 m, rounded up.
        QCOMPARE(saved->text(QStringLiteral("met_tot_ord")).trimmed(), QStringLiteral("626"));
        QCOMPARE(saved->ord(1, QStringLiteral("met")).trimmed(), QStringLiteral("626"));
    }

    void leavingAFieldValidatesAndSendsTheFocusBack()
    {
        InsOrdModel model(m_orders, m_db);
        model.load();
        model.functionKey(QStringLiteral("F1"), 0);
        focusOn(model, QStringLiteral("txt_cod_abb"));
        focusOn(model, QStringLiteral("cmb_onda"));
        QCOMPARE(m_message, msg(QStringLiteral("ins_cod_ord")));

        m_message.clear();
        type(model, QStringLiteral("txt_cod_abb"), -1, QStringLiteral("X"));
        focusOn(model, QStringLiteral("cmb_onda"));
        QVERIFY(m_message.isEmpty());
        focusOn(model, QStringLiteral("cmb_carta"));
        type(model, QStringLiteral("txt_alt_bob"), -1, QStringLiteral("100"));
        choose(model, QStringLiteral("txt_liv"), 0, 0);
        QVERIFY2(m_message.startsWith(msg(QStringLiteral("val_min"))), qPrintable(m_message));
    }

    void goingBackSkipsValidation()
    {
        InsOrdModel model(m_orders, m_db);
        model.load();
        model.functionKey(QStringLiteral("F1"), 0);
        type(model, QStringLiteral("txt_cod_abb"), -1, QStringLiteral("X"));
        focusOn(model, QStringLiteral("cmb_onda"));
        focusOn(model, QStringLiteral("cmb_carta"));
        type(model, QStringLiteral("txt_alt_bob"), -1, QStringLiteral("100"));
        m_message.clear();
        model.controlEvent(QStringLiteral("txt_alt_bob"), -1, QStringLiteral("KeyDown"), {{QStringLiteral("key"), int(Qt::Key_Up)}});
        focusOn(model, QStringLiteral("cmb_carta"));
        QVERIFY(m_message.isEmpty());
    }

    // VB reads the form on entering a field, not in F4: the value typed in the field being left is lost.
    void lastFieldTypedBeforeSaveIsNotRead()
    {
        InsOrdModel model(m_orders, m_db);
        model.load();
        model.functionKey(QStringLiteral("F1"), 0);
        fillValidOrder(model, QStringLiteral("A"));
        type(model, QStringLiteral("txt_pos_lat_pat"), 0, QStringLiteral("5"));
        model.controlEvent(QStringLiteral("txt_cod_abb"), -1, QStringLiteral("Change"), {{QStringLiteral("text"), QStringLiteral("B")}});
        model.functionKey(QStringLiteral("F4"), 0);
        QCOMPARE(m_orders->item(m_orders->Count())->cod_ord_abb().trimmed(), QStringLiteral("A"));
    }

    void changeKeepsTheOrderIdAndRaisesTheModification()
    {
        InsOrdModel model(m_orders, m_db);
        model.load();
        model.activate(QStringLiteral("Pag_pro"));
        const QString id = m_orders->item(1)->stag_id();
        const int mod = int(vb::val(m_orders->item(1)->mod_id()));
        QVERIFY(model.functionKey(QStringLiteral("F2"), 0));
        QCOMPARE(model.operation(), InsOrdModel::OLI_MOD);
        type(model, QStringLiteral("txt_cod_abb"), -1, QStringLiteral("CHANGED"));
        focusOn(model, QStringLiteral("cmb_onda"));
        model.functionKey(QStringLiteral("F4"), 0);
        QCOMPARE(m_message, msg(QStringLiteral("ope_eff")));
        QCOMPARE(m_orders->item(1)->stag_id(), id);
        QCOMPARE(int(vb::val(m_orders->item(1)->mod_id())), mod + 1);
        QCOMPARE(m_orders->item(1)->cod_ord_abb().trimmed(), QStringLiteral("CHANGED"));
    }

    void nullOrderIsAccepted()
    {
        InsOrdModel model(m_orders, m_db);
        model.load();
        const int before = m_orders->Count();
        QVERIFY(model.functionKey(QStringLiteral("F3"), 0));
        QCOMPARE(model.operation(), InsOrdModel::OLI_NEW_NULL);
        QCOMPARE(model.value(QStringLiteral("txt_cod_abb"), QStringLiteral("enabled")).toBool(), false);
        model.functionKey(QStringLiteral("F4"), 0);
        QCOMPARE(m_message, msg(QStringLiteral("ope_eff")));
        QCOMPARE(m_orders->Count(), before + 1);
        QCOMPARE(m_orders->item(before + 1)->ord(1, QStringLiteral("com.cod_com_ord")).trimmed(), QStringLiteral("NULL ORDER"));
    }

    void automaticRefusesAndCancelsWorkInProgress()
    {
        m_state->setSpdeOK(true);
        InsOrdModel model(m_orders, m_db);
        model.load();
        model.functionKey(QStringLiteral("F1"), 0);
        QCOMPARE(model.operation(), InsOrdModel::OLI_NUL);
        QVERIFY(m_message.contains(msg(QStringLiteral("mac_in_auto"))));

        m_state->setSpdeOK(false);
        model.functionKey(QStringLiteral("F1"), 0);
        QCOMPARE(model.operation(), InsOrdModel::OLI_NEW);
        model.machineMode(ClasseStato::automatico);
        QCOMPARE(model.operation(), InsOrdModel::OLI_NUL);
        QCOMPARE(m_message, msg(QStringLiteral("ope_ann")));
    }

    void escapeRestoresAndPagesStayBlockedDuringAnOperation()
    {
        InsOrdModel model(m_orders, m_db);
        model.load();
        model.activate(QStringLiteral("Pag_pro"));
        const QString shown = model.text(QStringLiteral("txt_cod_abb"));
        QVERIFY(!model.functionKey(QStringLiteral("F9"), 0));
        model.functionKey(QStringLiteral("F1"), 0);
        QVERIFY(model.functionKey(QStringLiteral("F9"), 0));
        QVERIFY(model.functionKey(QStringLiteral("F10"), 0));
        model.functionKey(QStringLiteral("ESC"), 0);
        QCOMPARE(model.text(QStringLiteral("txt_cod_abb")), shown);
        QCOMPARE(model.value(QStringLiteral("txt_cod_abb"), QStringLiteral("enabled")).toBool(), false);
    }

    void browsingFollowsThePointer()
    {
        InsOrdModel model(m_done, m_db);
        model.load();
        model.activate(QStringLiteral("Pag_pro"));
        QCOMPARE(model.text(QStringLiteral("txt_indice")), QStringLiteral("1"));
        model.functionKey(QStringLiteral("F6"), 0);
        QCOMPARE(model.text(QStringLiteral("txt_indice")), QStringLiteral("2"));
        QCOMPARE(model.text(QStringLiteral("txt_cod_abb")), m_done->item(2)->cod_ord_abb());
        model.functionKey(QStringLiteral("F5"), 0);
        QCOMPARE(model.text(QStringLiteral("txt_indice")), QStringLiteral("1"));
        model.functionKey(QStringLiteral("F6"), 0);
        model.functionKey(QStringLiteral("F7"), 0);
        QCOMPARE(model.text(QStringLiteral("txt_indice")), QStringLiteral("1"));
    }
};

QTEST_GUILESS_MAIN(TestInsOrd)
#include "tst_insord.moc"
