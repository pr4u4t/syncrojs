#include "collezioneordini.h"
#include "inibas.h"
#include "inistore.h"
#include "messages.h"
#include "paglisordmodel.h"
#include "parmac.h"

#include <QFile>
#include <QSignalSpy>
#include <QTemporaryDir>
#include <QTest>

using namespace stag;

// Ord_mac_ese.dat holds 100 orders, enough to scroll through; the navigation expectations are
// worked out by hand from Form_KeyUp in pag_lis_ord.frm.
class TestPagLisOrd : public QObject
{
    Q_OBJECT

private:
    QTemporaryDir m_dir;
    CollezioneOrdini *m_orders = nullptr;

    QStringList row(const PagLisOrdModel &model, int index)
    {
        return model.value(QStringLiteral("lis_ord"), QStringLiteral("cells")).toList().at(index).toStringList();
    }

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
        leggiNumLivTag(iniFile());
        legParMac(iniFile());
        iniLiv();
        abi_mis_ang = false;
        m_orders = new CollezioneOrdini(this);
        QCOMPARE(m_orders->Init(true), int(RET_OK));
        m_orders->Leg_dat_ord();
        QCOMPARE(m_orders->Count(), 100);
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 1));
    }

    void loadShowsTheFirstPage()
    {
        PagLisOrdModel model(m_orders);
        model.load();
        QCOMPARE(model.ordersPerPage(), 7);
        QCOMPARE(model.first(), 1);
        QCOMPARE(model.last(), 7);
        const QVariantList cells = model.value(QStringLiteral("lis_ord"), QStringLiteral("cells")).toList();
        QCOMPARE(cells.size(), 1 + 7 * 3);
        const OrdinePtr first = m_orders->item(1);
        const QStringList firstRow = row(model, 1);
        QCOMPARE(firstRow[0], QStringLiteral("1"));
        QCOMPARE(firstRow[1].trimmed(), first->cod_ord_abb().trimmed());
        QCOMPARE(firstRow[4].trimmed(), first->ord(1, QStringLiteral("com.cod_com_ord")).trimmed());
        QCOMPARE(firstRow[8], QString::number(vb::intDiv(vb::val(first->ord(1, QStringLiteral("sca_lun"))), TODECMM)));
        QCOMPARE(row(model, 4)[0], QStringLiteral("2"));
        QCOMPARE(row(model, 0)[1], msg(QStringLiteral("lis_cod_abb")));
    }

    void selectionPaintsTheOrderRowsGreen()
    {
        PagLisOrdModel model(m_orders);
        model.load();
        const QVariantList back = model.value(QStringLiteral("lis_ord"), QStringLiteral("rowBack")).toList();
        for (int i = 1; i <= 3; ++i)
            QCOMPARE(back[i].toString(), QStringLiteral("#008000"));
        // i Mod (3 * 2) in 1..3 is grey; rows 4-6 of the next order are white.
        QCOMPARE(back[4].toString(), QStringLiteral("#ffffff"));
        QCOMPARE(back[7].toString(), QStringLiteral("#e0e0df"));
        QCOMPARE(model.value(QStringLiteral("lis_ord"), QStringLiteral("rowBack")).toList().at(0).toString(), QStringLiteral("#101010"));

        model.keyUp(Qt::Key_Down);
        const QVariantList moved = model.value(QStringLiteral("lis_ord"), QStringLiteral("rowBack")).toList();
        QCOMPARE(moved[4].toString(), QStringLiteral("#008000"));
    }

    // VB's aggiorna leaves pun_lis on the last row after Form_Load; Form_Activate always follows and resets it.
    void loadAloneLeavesThePointerOnTheLastRowUntilActivate()
    {
        PagLisOrdModel model(m_orders);
        model.load();
        QCOMPARE(model.selected(), 7);
        model.activate(QStringLiteral("Pag_pro"));
        QCOMPARE(model.selected(), 1);
        QCOMPARE(model.cursor(), 1);
    }

    void arrowsMoveAndScrollTheWindow()
    {
        PagLisOrdModel model(m_orders);
        model.load();
        model.activate(QStringLiteral("Pag_pro"));
        for (int i = 0; i < 6; ++i)
            model.keyUp(Qt::Key_Down);
        QCOMPARE(model.selected(), 7);
        QCOMPARE(model.cursor(), 7);
        QCOMPARE(model.first(), 1);
        model.keyUp(Qt::Key_Down);
        QCOMPARE(model.selected(), 8);
        QCOMPARE(model.cursor(), 7);
        QCOMPARE(model.first(), 2);
        QCOMPARE(model.last(), 8);
        for (int i = 0; i < 7; ++i)
            model.keyUp(Qt::Key_Up);
        QCOMPARE(model.selected(), 1);
        QCOMPARE(model.first(), 1);
        model.keyUp(Qt::Key_Up);
        QCOMPARE(model.selected(), 1);
    }

    void pagesKeepTheCursorAndStopAtTheEnds()
    {
        PagLisOrdModel model(m_orders);
        model.load();
        model.activate(QStringLiteral("Pag_pro"));
        model.keyUp(Qt::Key_PageDown);
        // VB quirk kept on purpose: aggiorna moves pun_lis to ultimo while the highlight (puntatore)
        // stays on the first row, so the selected order is not the one shown in green.
        QCOMPARE(model.selected(), 14);
        QCOMPARE(model.cursor(), 1);
        QCOMPARE(model.first(), 8);
        QCOMPARE(model.last(), 14);
        model.keyUp(Qt::Key_PageUp);
        QCOMPARE(model.selected(), 1); // primo shrank, so aggiorna moves pun_lis to primo
        QCOMPARE(model.first(), 1);
        QCOMPARE(model.last(), 7);
        model.keyUp(Qt::Key_PageUp);
        QCOMPARE(model.selected(), 1);
        for (int i = 0; i < 20; ++i)
            model.keyUp(Qt::Key_PageDown);
        QCOMPARE(model.selected(), 100);
        QCOMPARE(model.last(), 100);
    }

    void activateResetsThePointerExceptFromItsDialogs()
    {
        PagLisOrdModel model(m_orders);
        model.load();
        model.activate(QStringLiteral("Pag_pro"));
        m_orders->scr_pun_ord(5);
        model.activate(QStringLiteral("Del_ord"));
        QCOMPARE(model.selected(), 11); // same quirk: ultimo grew from 7 to 11
        QCOMPARE(model.first(), 5);
        QCOMPARE(model.cursor(), 1);
        m_orders->scr_pun_ord(5);
        model.activate(QStringLiteral("Pag_pro"));
        QCOMPARE(model.selected(), 1);
    }

    void detailsShrinkThePageAndFillTheFields()
    {
        PagLisOrdModel model(m_orders);
        model.load();
        m_orders->scr_pun_ord(1);
        QVERIFY(model.functionKey(QStringLiteral("F7"), 0));
        QVERIFY(model.detailsVisible());
        QCOMPARE(model.ordersPerPage(), 6);
        QCOMPARE(model.value(QStringLiteral("frm_det"), QStringLiteral("visible")).toBool(), true);
        QCOMPARE(model.value(QStringLiteral("txt_lun_tot")).toString(), m_orders->item(1)->text(QStringLiteral("met_tot_ord")));
        QVERIFY(model.functionKey(QStringLiteral("F7"), 0));
        QCOMPARE(model.ordersPerPage(), 7);
    }

    void functionKeysFollowTheList()
    {
        PagLisOrdModel model(m_orders);
        model.load();
        model.activate(QStringLiteral("Pag_pro"));
        model.keyUp(Qt::Key_Down);
        QVERIFY(!model.functionKey(QStringLiteral("F1"), 0));
        QCOMPARE(m_orders->leg_pun_ord(), 2);
        QVERIFY(!model.functionKey(QStringLiteral("F3"), 0));

        CollezioneOrdini empty;
        PagLisOrdModel emptyModel(&empty);
        emptyModel.load();
        QVERIFY(emptyModel.functionKey(QStringLiteral("F3"), 0));
        QVERIFY(emptyModel.functionKey(QStringLiteral("F7"), 0));
        QVERIFY(!emptyModel.detailsVisible());
    }
};

QTEST_GUILESS_MAIN(TestPagLisOrd)
#include "tst_paglisord.moc"
