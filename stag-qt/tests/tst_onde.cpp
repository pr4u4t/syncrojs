#include "collezioneordini.h"
#include "inibas.h"
#include "inistore.h"
#include "messages.h"
#include "ondemodel.h"
#include "parmac.h"
#include "stagglobals.h"
#include "uihooks.h"

#include <QFile>
#include <QSqlDatabase>
#include <QSqlQuery>
#include <QTemporaryDir>
#include <QTest>

using namespace stag;

class TestOnde : public QObject
{
    Q_OBJECT

private:
    QTemporaryDir m_dir;
    QSqlDatabase m_db;
    CollezioneOrdini *m_orders = nullptr;
    QString m_message;

    void type(OndeModel &model, const QString &control, const QString &text)
    {
        model.controlEvent(control, -1, QStringLiteral("GotFocus"), {});
        model.controlEvent(control, -1, QStringLiteral("Change"), {{QStringLiteral("text"), text}});
    }
    QString focusOf(const OndeModel &model, const QString &control) const
    {
        return model.value(control, QStringLiteral("focusCount")).toString();
    }
    int count(const QString &sql)
    {
        QSqlQuery query(m_db);
        query.exec(sql);
        query.next();
        return query.value(0).toInt();
    }
    int rowOf(const OndeModel &model, const QString &flute, const QString &paper)
    {
        const QVariantList cells = model.value(QStringLiteral("lis"), QStringLiteral("cells")).toList();
        for (int i = 1; i < cells.size(); ++i) {
            if (cells.at(i).toStringList() == QStringList{flute, paper})
                return i;
        }
        return -1;
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
        m_db = QSqlDatabase::addDatabase(QStringLiteral("QSQLITE"), QStringLiteral("onde"));
        m_db.setDatabaseName(copy);
        QVERIFY(m_db.open());
        QVERIFY(Messages::instance().load(copy, 1));
        messaggioHook = [this](const QString &text, bool, bool) { m_message = text; };
        m_orders = new CollezioneOrdini(this);
        QCOMPARE(m_orders->Init(false), int(RET_OK));
        m_orders->Leg_dat_ord();
    }

    void init() { m_message.clear(); }

    void listFollowsTheVbQueryOrder()
    {
        OndeModel model(m_orders, m_db);
        model.load();
        QCOMPARE(model.recordCount(), 200);
        QCOMPARE(model.field(QStringLiteral("ONDA")), QStringLiteral("A"));
        QCOMPARE(model.field(QStringLiteral("CARTA")), QStringLiteral("ONDA A"));
        QCOMPARE(model.field(QStringLiteral("SPESSORE")), QStringLiteral("6894"));
        QCOMPARE(model.value(QStringLiteral("lis"), QStringLiteral("row")).toInt(), 1);
    }

    // The order sent to the PLC carries "027 100 166" for flute B: the same values have to come from ONDE.
    void currentOrderFluteExistsInTheTable()
    {
        const OrdinePtr order = m_orders->item(1);
        QSqlQuery query(m_db);
        query.prepare(QStringLiteral("SELECT SPESSORE FROM ONDE WHERE ONDA = ? AND CARTA = ?"));
        query.addBindValue(order->text(QStringLiteral("tip_car.Cod_ond")).trimmed());
        query.addBindValue(order->text(QStringLiteral("tip_car.cod_com_car")).trimmed());
        QVERIFY(query.exec());
        QVERIFY2(query.next(), qPrintable(order->text(QStringLiteral("tip_car.cod_com_car"))));
        QCOMPARE(query.value(0).toInt(), 27);
    }

    void newRecordCopiesFromTheSameFluteAndSaves()
    {
        OndeModel model(m_orders, m_db);
        model.load();
        QVERIFY(model.functionKey(QStringLiteral("F1"), 0));
        QVERIFY(model.editing());
        QCOMPARE(model.field(QStringLiteral("ONDA")), QString());

        type(model, QStringLiteral("ONDA"), QStringLiteral("B"));
        model.controlEvent(QStringLiteral("CARTA"), -1, QStringLiteral("GotFocus"), {});
        // VB fills the mandatory fields from the first record of that flute.
        QVERIFY(!model.field(QStringLiteral("SPESSORE")).isEmpty());
        QCOMPARE(model.field(QStringLiteral("SPESSORE")), QStringLiteral("27"));

        type(model, QStringLiteral("CARTA"), QStringLiteral("TEST NEW"));
        QVERIFY(model.functionKey(QStringLiteral("F4"), 0));
        QVERIFY(!model.editing());
        QCOMPARE(m_message, msg(QStringLiteral("ope_eff")));
        QCOMPARE(model.field(QStringLiteral("CARTA")), QStringLiteral("TEST NEW"));
        QCOMPARE(count(QStringLiteral("SELECT count(*) FROM ONDE WHERE CARTA = 'TEST NEW'")), 1);
        QCOMPARE(model.value(QStringLiteral("lis"), QStringLiteral("row")).toInt(), rowOf(model, QStringLiteral("B"), QStringLiteral("TEST NEW")));
    }

    void validationSendsTheFocusBack_data()
    {
        QTest::addColumn<QString>("control");
        QTest::addColumn<QString>("value");
        QTest::addColumn<bool>("valid");
        QTest::newRow("speed too low") << "VELDIPROD" << "9" << false;
        QTest::newRow("speed minimum") << "VELDIPROD" << "10" << true;
        QTest::newRow("speed maximum") << "VELDICAMBI" << "400" << true;
        QTest::newRow("speed too high") << "VELDICAMBI" << "401" << false;
        QTest::newRow("no weight") << "GRAMMATURA" << "0" << false;
        QTest::newRow("thickness limit") << "SPESSORE" << "500" << true;
        QTest::newRow("thickness too high") << "SPESSORE" << "501" << false;
        QTest::newRow("code too high") << "CODONDA" << "100" << false;
        QTest::newRow("code minimum") << "CODONDA" << "0" << true;
    }

    void validationSendsTheFocusBack()
    {
        QFETCH(QString, control);
        QFETCH(QString, value);
        QFETCH(bool, valid);
        OndeModel model(m_orders, m_db);
        model.load();
        model.functionKey(QStringLiteral("F2"), 0);
        type(model, control, value);
        const QString before = focusOf(model, control);
        model.controlEvent(control == QLatin1String("CARTA") ? QStringLiteral("ONDA") : QStringLiteral("CARTA"), -1, QStringLiteral("GotFocus"), {});
        QCOMPARE(m_message.isEmpty(), valid);
        QCOMPARE(focusOf(model, control) == before, valid);
    }

    void duplicatePaperIsRefusedOnlyForNewRecords()
    {
        OndeModel model(m_orders, m_db);
        model.load();
        model.functionKey(QStringLiteral("F1"), 0);
        type(model, QStringLiteral("ONDA"), QStringLiteral("B"));
        type(model, QStringLiteral("CARTA"), QStringLiteral("12345"));
        model.controlEvent(QStringLiteral("GRAMMATURA"), -1, QStringLiteral("GotFocus"), {});
        QCOMPARE(m_message, msg(QStringLiteral("car_esi")));
    }

    void saveNeedsFluteAndPaperAndEscapeRestores()
    {
        OndeModel model(m_orders, m_db);
        model.load();
        const QString paper = model.field(QStringLiteral("CARTA"));
        const int records = model.recordCount();
        model.functionKey(QStringLiteral("F1"), 0);
        model.functionKey(QStringLiteral("F4"), 0);
        QCOMPARE(m_message, msg(QStringLiteral("man_cam_ond")));
        QVERIFY(model.editing());
        QVERIFY(model.functionKey(QStringLiteral("ESC"), 0));
        QVERIFY(!model.editing());
        QCOMPARE(model.field(QStringLiteral("CARTA")), paper);
        QCOMPARE(model.recordCount(), records);
    }

    void flutesInUseCannotBeDeletedAndKeepTheirKey()
    {
        OndeModel model(m_orders, m_db);
        model.load();
        const OrdinePtr order = m_orders->item(1);
        const int row = rowOf(model, order->text(QStringLiteral("tip_car.Cod_ond")).trimmed(), order->text(QStringLiteral("tip_car.cod_com_car")).trimmed());
        QVERIFY(row > 0);
        model.controlEvent(QStringLiteral("lis"), -1, QStringLiteral("Click"), {{QStringLiteral("row"), row}});

        const int records = model.recordCount();
        model.functionKey(QStringLiteral("F3"), 0);
        QCOMPARE(model.recordCount(), records);
        QCOMPARE(m_message, msg(QStringLiteral("imp_can_ond")));

        model.functionKey(QStringLiteral("F2"), 0);
        QVERIFY(model.value(QStringLiteral("ONDA"), QStringLiteral("readOnly")).toBool());
        QVERIFY(model.value(QStringLiteral("CARTA"), QStringLiteral("readOnly")).toBool());
        QVERIFY(!model.value(QStringLiteral("GRAMMATURA"), QStringLiteral("readOnly")).toBool());
        model.functionKey(QStringLiteral("ESC"), 0);
    }

    void reservedRecordsStayAndOthersGo()
    {
        OndeModel model(m_orders, m_db);
        model.load();
        const int records = model.recordCount();
        model.functionKey(QStringLiteral("F3"), 0); // row 1 is A / ONDA A, RISERVATO = 1
        QCOMPARE(model.recordCount(), records);
        QVERIFY(m_message.isEmpty());

        model.controlEvent(QStringLiteral("lis"), -1, QStringLiteral("Click"), {{QStringLiteral("row"), 2}});
        model.functionKey(QStringLiteral("F3"), 0);
        QCOMPARE(model.recordCount(), records - 1);
    }

    void pagesCannotBeLeftDuringAnEdit()
    {
        OndeModel model(m_orders, m_db);
        model.load();
        QVERIFY(!model.functionKey(QStringLiteral("F10"), 0));
        model.functionKey(QStringLiteral("F1"), 0);
        QVERIFY(model.functionKey(QStringLiteral("F10"), 0));
        QVERIFY(model.functionKey(QStringLiteral("F9"), 0));
        model.functionKey(QStringLiteral("ESC"), 0);
    }

    void returnMovesAlongTheTabOrder()
    {
        OndeModel model(m_orders, m_db);
        model.load();
        model.functionKey(QStringLiteral("F1"), 0);
        model.controlEvent(QStringLiteral("ONDA"), -1, QStringLiteral("GotFocus"), {});
        const QString before = focusOf(model, QStringLiteral("CARTA"));
        model.controlEvent(QStringLiteral("ONDA"), -1, QStringLiteral("KeyDown"), {{QStringLiteral("key"), int(Qt::Key_Return)}});
        QVERIFY(focusOf(model, QStringLiteral("CARTA")) != before);
        model.functionKey(QStringLiteral("ESC"), 0);
    }
};

QTEST_GUILESS_MAIN(TestOnde)
#include "tst_onde.moc"
