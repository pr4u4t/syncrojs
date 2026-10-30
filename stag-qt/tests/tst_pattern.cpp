#include "collezioneordini.h"
#include "inibas.h"
#include "inistore.h"
#include "messages.h"
#include "parmac.h"
#include "patternmodel.h"
#include "uihooks.h"

#include <QFile>
#include <QSqlDatabase>
#include <QSqlQuery>
#include <QTemporaryDir>
#include <QTest>

using namespace stag;

class TestPattern : public QObject
{
    Q_OBJECT

private:
    QTemporaryDir m_dir;
    QSqlDatabase m_db;
    CollezioneOrdini *m_orders = nullptr;
    QString m_message;

    void type(PatternModel &model, const QString &control, const QString &text)
    {
        model.controlEvent(control, -1, QStringLiteral("GotFocus"), {});
        model.controlEvent(control, -1, QStringLiteral("Change"), {{QStringLiteral("text"), text}});
    }
    void fill(PatternModel &model, const QString &code, const QStringList &lengths)
    {
        type(model, QStringLiteral("CODICE"), code);
        type(model, QStringLiteral("CONTRASTO"), QStringLiteral("1"));
        type(model, QStringLiteral("PRI_LET_FC"), QStringLiteral("0"));
        for (int i = 1; i <= 10; ++i)
            type(model, QStringLiteral("L%1").arg(i), lengths.value(i - 1, QStringLiteral("0")));
    }
    void selectCode(PatternModel &model, const QString &code)
    {
        const QVariantList cells = model.value(QStringLiteral("lis"), QStringLiteral("cells")).toList();
        for (int i = 1; i < cells.size(); ++i) {
            if (cells.at(i).toStringList().first() == code)
                model.controlEvent(QStringLiteral("lis"), -1, QStringLiteral("Click"), {{QStringLiteral("row"), i}});
        }
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
        const QString copy = m_dir.filePath(QStringLiteral("stag.sqlite"));
        QVERIFY(QFile::copy(QStringLiteral(STAG_SQLITE), copy));
        QFile::setPermissions(copy, QFile::ReadOwner | QFile::WriteOwner);
        m_db = QSqlDatabase::addDatabase(QStringLiteral("QSQLITE"), QStringLiteral("pattern"));
        m_db.setDatabaseName(copy);
        QVERIFY(m_db.open());
        QVERIFY(Messages::instance().load(copy, 1));
        messaggioHook = [this](const QString &text, bool, bool) { m_message = text; };
        m_orders = new CollezioneOrdini(this);
        QCOMPARE(m_orders->Init(true), int(RET_OK)); // 100 orders; none uses a pattern code unless a test sets it
        m_orders->Leg_dat_ord();
    }

    void init() { m_message.clear(); }

    void listIsOrderedByCode()
    {
        PatternModel model(m_orders, m_db);
        model.load();
        QCOMPARE(model.recordCount(), 4);
        QCOMPARE(model.field(QStringLiteral("CODICE")), QStringLiteral("PI013"));
        QCOMPARE(model.field(QStringLiteral("OFS_CUT")), QStringLiteral("97"));
    }

    // The machine's own PI013 holds L4 = "``": Val gives 0, so everything after L3 counts as unused.
    void realRecordWithAGarbageNotchIsTruncatedOnSave()
    {
        PatternModel model(m_orders, m_db);
        model.load();
        model.functionKey(QStringLiteral("F2"), 0);
        QVERIFY(model.functionKey(QStringLiteral("F4"), 0));
        QCOMPARE(m_message, msg(QStringLiteral("ope_eff")));
        QCOMPARE(model.field(QStringLiteral("L4")), QStringLiteral("0"));
        QCOMPARE(model.field(QStringLiteral("L7")), QStringLiteral("0"));
        QCOMPARE(model.field(QStringLiteral("L3")), QStringLiteral("22"));
    }

    void newRecordIsSavedWithItsNotches()
    {
        PatternModel model(m_orders, m_db);
        model.load();
        const int records = model.recordCount();
        model.functionKey(QStringLiteral("F1"), 0);
        fill(model, QStringLiteral("NEW1"), {QStringLiteral("10"), QStringLiteral("20"), QStringLiteral("30")});
        QVERIFY(model.functionKey(QStringLiteral("F4"), 0));
        QVERIFY(!model.editing());
        QCOMPARE(model.recordCount(), records + 1);
        QCOMPARE(model.field(QStringLiteral("CODICE")), QStringLiteral("NEW1"));
        QCOMPARE(model.value(QStringLiteral("lis"), QStringLiteral("row")).toInt(), model.currentRow() + 1);
    }

    void measureErrorsBlockSaving_data()
    {
        QTest::addColumn<QStringList>("lengths");
        QTest::addColumn<bool>("accepted");
        QTest::newRow("no notches") << QStringList{} << false;
        QTest::newRow("not increasing") << QStringList{"20", "20", "30"} << false;
        QTest::newRow("first shorter than 5/64 of the longest") << QStringList{"7", "100"} << false;
        QTest::newRow("exactly 5/64 of 64") << QStringList{"5", "64"} << true;
        QTest::newRow("notches after a zero are ignored") << QStringList{"10", "20", "0", "5"} << true;
    }

    void measureErrorsBlockSaving()
    {
        QFETCH(QStringList, lengths);
        QFETCH(bool, accepted);
        PatternModel model(m_orders, m_db);
        model.load();
        model.functionKey(QStringLiteral("F1"), 0);
        fill(model, QStringLiteral("M") + QString::number(qHash(QTest::currentDataTag()) % 9999), lengths);
        model.functionKey(QStringLiteral("F4"), 0);
        QCOMPARE(model.editing(), !accepted);
        QCOMPARE(m_message, accepted ? msg(QStringLiteral("ope_eff")) : msg(QStringLiteral("err_mis_pat")));
        if (!accepted)
            model.functionKey(QStringLiteral("ESC"), 0);
    }

    void fieldValidationFollowsTheLimits_data()
    {
        QTest::addColumn<QString>("control");
        QTest::addColumn<QString>("value");
        QTest::addColumn<bool>("valid");
        QTest::newRow("contrast maximum") << "CONTRASTO" << "4" << true;
        QTest::newRow("contrast too high") << "CONTRASTO" << "5" << false;
        QTest::newRow("first reading") << "PRI_LET_FC" << "1" << true;
        QTest::newRow("first reading too high") << "PRI_LET_FC" << "2" << false;
        QTest::newRow("L1 required") << "L1" << "0" << false;
        QTest::newRow("L1 limit") << "L1" << "999" << true;
        QTest::newRow("L1 too long") << "L1" << "1000" << false;
    }

    void fieldValidationFollowsTheLimits()
    {
        QFETCH(QString, control);
        QFETCH(QString, value);
        QFETCH(bool, valid);
        PatternModel model(m_orders, m_db);
        model.load();
        model.functionKey(QStringLiteral("F2"), 0);
        type(model, control, value);
        model.controlEvent(QStringLiteral("OFS_CUT"), -1, QStringLiteral("GotFocus"), {});
        QCOMPARE(m_message.isEmpty(), valid);
        model.functionKey(QStringLiteral("ESC"), 0);
    }

    void aKnownCodeTurnsTheNewRecordIntoAnEdit()
    {
        PatternModel model(m_orders, m_db);
        model.load();
        const int records = model.recordCount();
        model.functionKey(QStringLiteral("F1"), 0);
        type(model, QStringLiteral("CODICE"), QStringLiteral("ti002"));
        model.controlEvent(QStringLiteral("CONTRASTO"), -1, QStringLiteral("GotFocus"), {});
        QCOMPARE(model.field(QStringLiteral("CODICE")), QStringLiteral("TI002"));
        QVERIFY(model.editing());
        model.functionKey(QStringLiteral("F4"), 0);
        QCOMPARE(model.recordCount(), records);
    }

    void usedCodesCannotBeDeletedAndSavingUpdatesTheOrders()
    {
        const OrdinePtr order = m_orders->item(2);
        order->setText(QStringLiteral("ord(1).pat_cod"), QStringLiteral("TI002"));
        PatternModel model(m_orders, m_db);
        model.load();
        selectCode(model, QStringLiteral("TI002"));
        const int records = model.recordCount();

        model.functionKey(QStringLiteral("F3"), 0);
        QCOMPARE(model.recordCount(), records);
        QCOMPARE(m_message, msg(QStringLiteral("imp_can_pat")));

        model.functionKey(QStringLiteral("F2"), 0);
        QVERIFY(model.value(QStringLiteral("CODICE"), QStringLiteral("readOnly")).toBool());
        type(model, QStringLiteral("CONTRASTO"), QStringLiteral("3"));
        type(model, QStringLiteral("L1"), QStringLiteral("21"));
        model.functionKey(QStringLiteral("F4"), 0);
        QCOMPARE(order->ord(1, QStringLiteral("pat_con")).trimmed(), QStringLiteral("3"));
        QCOMPARE(order->ord(1, QStringLiteral("L1")).trimmed(), QStringLiteral("21"));
        QCOMPARE(order->ord(1, QStringLiteral("L2")).trimmed(), QStringLiteral("26"));
        order->setText(QStringLiteral("ord(1).pat_cod"), QString());
    }

    // The condition in VB is ord_mac.Count > 1: a lone order keeps its old values.
    void aSingleOrderIsNotUpdated()
    {
        CollezioneOrdini single;
        auto order = std::make_shared<ClasseOrdine>(*m_orders->item(1));
        order->setText(QStringLiteral("ord(1).pat_cod"), QStringLiteral("TI002"));
        order->setText(QStringLiteral("ord(1).pat_con"), QStringLiteral("0"));
        single.add(order);
        QCOMPARE(single.Count(), 1);
        PatternModel model(&single, m_db);
        model.load();
        selectCode(model, QStringLiteral("TI002"));
        model.functionKey(QStringLiteral("F2"), 0);
        type(model, QStringLiteral("CONTRASTO"), QStringLiteral("2"));
        model.functionKey(QStringLiteral("F4"), 0);
        QCOMPARE(m_message, msg(QStringLiteral("ope_eff")));
        QCOMPARE(single.item(1)->ord(1, QStringLiteral("pat_con")).trimmed(), QStringLiteral("0"));
    }

    void escapeRestoresAndPagesStayBlockedDuringAnEdit()
    {
        PatternModel model(m_orders, m_db);
        model.load();
        const QString contrast = model.field(QStringLiteral("CONTRASTO"));
        model.functionKey(QStringLiteral("F2"), 0);
        type(model, QStringLiteral("CONTRASTO"), QStringLiteral("9"));
        QVERIFY(model.functionKey(QStringLiteral("F10"), 0));
        QVERIFY(model.functionKey(QStringLiteral("F1"), 2));
        model.functionKey(QStringLiteral("ESC"), 0);
        QCOMPARE(model.field(QStringLiteral("CONTRASTO")), contrast);
        QVERIFY(!model.functionKey(QStringLiteral("F10"), 0));
        QVERIFY(!model.functionKey(QStringLiteral("F1"), 2));
    }

    void deleteKeepsThePositionOrTheNewLastRow()
    {
        PatternModel model(m_orders, m_db);
        model.load();
        model.controlEvent(QStringLiteral("lis"), -1, QStringLiteral("Click"), {{QStringLiteral("row"), 2}});
        const int count = model.recordCount();
        model.functionKey(QStringLiteral("F3"), 0);
        QCOMPARE(model.recordCount(), count - 1);
        QCOMPARE(model.currentRow(), 1);
        model.controlEvent(QStringLiteral("lis"), -1, QStringLiteral("Click"), {{QStringLiteral("row"), count - 1}});
        model.functionKey(QStringLiteral("F3"), 0);
        QCOMPARE(model.currentRow(), model.recordCount() - 1);
    }
};

QTEST_GUILESS_MAIN(TestPattern)
#include "tst_pattern.moc"
