#include "collezioneordini.h"
#include "inibas.h"
#include "inistore.h"
#include "messages.h"
#include "paglisesemodel.h"
#include "parmac.h"

#include <QFile>
#include <QRegularExpression>
#include <QTemporaryDir>
#include <QTest>

using namespace stag;

class TestPagLisEse : public QObject
{
    Q_OBJECT

private:
    QTemporaryDir m_dir;
    CollezioneOrdini *m_orders = nullptr;

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
        par_abi.abi_rep_pro = true;
        m_orders = new CollezioneOrdini(this);
        QCOMPARE(m_orders->Init(true), int(RET_OK));
        m_orders->Leg_dat_ord();
        QVERIFY(Messages::instance().load(QStringLiteral(STAG_SQLITE), 1));
    }

    // The last report in Lis.log is the first record of Ord_mac_ese.dat:
    // "FORMATO: 1 LIVELLO: 2 Fogli Buoni = 254 ... Velocita Media: 2 Metri Prodotti: 297".
    void reportMatchesTheLastEntryOfTheListLog()
    {
        QFile file(QStringLiteral(STAG_REFERENCE_DATA "/Lis.log"));
        QVERIFY(file.open(QIODevice::ReadOnly));
        const QStringList lines = QString::fromLatin1(file.readAll()).split(QStringLiteral("\r\n"), Qt::SkipEmptyParts);
        static const QRegularExpression format(QStringLiteral(R"(FORMATO: (\d) LIVELLO: (\d) Fogli Buoni = (\S+)\s+Fogli Errati = (\S+)\s+Fogli Scarto = (\S+)\s+MM Scarto = (\S+))"));
        static const QRegularExpression totals(QStringLiteral(R"(Media: (\d+) Metri Prodotti: (\d+))"));
        int at = lines.size() - 1;
        while (at >= 0 && !totals.match(lines[at]).hasMatch())
            --at;
        QVERIFY(at > 1);
        const QRegularExpressionMatch total = totals.match(lines[at]);

        PagLisEseModel model(m_orders);
        model.load();
        model.activate(QStringLiteral("Pag_pro"));
        QVERIFY(model.functionKey(QStringLiteral("F7"), 0));
        QCOMPARE(int(vb::val(model.value(QStringLiteral("txt_vel_med")).toString())), total.captured(1).toInt());
        QCOMPARE(model.value(QStringLiteral("txt_met_pro")).toString(), total.captured(2));

        const QVariantList rows = model.value(QStringLiteral("lis_rep"), QStringLiteral("cells")).toList();
        int checked = 0;
        for (int i = at - 1; i >= 0 && format.match(lines[i]).hasMatch(); --i, ++checked) {
            const QRegularExpressionMatch match = format.match(lines[i]);
            const QStringList row = rows.at(match.captured(1).toInt()).toStringList();
            QCOMPARE(row[1].trimmed(), match.captured(3));
            QCOMPARE(row[2].trimmed(), match.captured(4));
            QCOMPARE(row[3].trimmed(), match.captured(5));
            QCOMPARE(row[4].trimmed(), match.captured(6));
        }
        QCOMPARE(checked, 2);
    }

    void reportTimesAreShownAsHoursMinutesSeconds()
    {
        PagLisEseModel model(m_orders);
        model.load();
        model.activate(QStringLiteral("Pag_pro"));
        model.functionKey(QStringLiteral("F7"), 0);
        // tem_mar 8520 s in the first record, as the C++ port's test of the shift totals also reads.
        QCOMPARE(model.value(QStringLiteral("txt_tem_mar")).toString(), QStringLiteral("02.22.00"));
    }

    void pageDownKeepsThePointerTheOrderListLoses()
    {
        PagLisEseModel model(m_orders);
        model.load();
        model.activate(QStringLiteral("Pag_pro"));
        model.keyUp(Qt::Key_PageDown);
        QCOMPARE(model.selected(), 8);
        QCOMPARE(model.first(), 8);
        QCOMPARE(model.last(), 14);
    }

    void reportPanelShrinksTheList()
    {
        PagLisEseModel model(m_orders);
        model.load();
        model.activate(QStringLiteral("Pag_pro"));
        QCOMPARE(model.ordersPerPage(), 7);
        model.functionKey(QStringLiteral("F7"), 0);
        QCOMPARE(model.ordersPerPage(), 4);
        QCOMPARE(model.value(QStringLiteral("frm_rep"), QStringLiteral("visible")).toBool(), true);
        model.functionKey(QStringLiteral("F7"), 0);
        QCOMPARE(model.ordersPerPage(), 7);
        QCOMPARE(model.value(QStringLiteral("frm_rep"), QStringLiteral("visible")).toBool(), false);
    }

    void withoutTheOptionReportKeysAreHiddenAndDead()
    {
        par_abi.abi_rep_pro = false;
        PagLisEseModel model(m_orders);
        model.load();
        QCOMPARE(model.hiddenFunctionKeys(), (QStringList{QStringLiteral("F2"), QStringLiteral("F7")}));
        QVERIFY(model.functionKey(QStringLiteral("F2"), 0));
        QVERIFY(model.functionKey(QStringLiteral("F7"), 0));
        QVERIFY(!model.reportVisible());
        par_abi.abi_rep_pro = true;
    }
};

QTEST_GUILESS_MAIN(TestPagLisEse)
#include "tst_paglisese.moc"
