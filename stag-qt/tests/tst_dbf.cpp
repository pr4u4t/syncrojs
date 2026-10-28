#include "dbf.h"
#include "sqlitemigration.h"

#include <QDir>
#include <QFile>
#include <QSqlDatabase>
#include <QSqlQuery>
#include <QTemporaryDir>
#include <QTest>

class TestDbf : public QObject
{
    Q_OBJECT

private:
    static QString reference(const QString &name) { return QDir(QStringLiteral(STAG_REFERENCE_DATA)).filePath(name); }

private slots:
    void readsOndeTable()
    {
        if (!QFile::exists(reference("ONDE.DBF")))
            QSKIP("reference data not available");
        DbfTable table;
        QString error;
        QVERIFY2(table.load(reference("ONDE.DBF"), &error), qPrintable(error));
        QCOMPARE(table.recordCount(), 206);
        QCOMPARE(table.fields().size(), 14);
        QCOMPARE(table.fields().first().name, QStringLiteral("ONDA"));
        int deleted = 0;
        for (int record = 0; record < table.recordCount(); ++record)
            deleted += table.isDeleted(record) ? 1 : 0;
        QCOMPARE(deleted, 6);
    }

    void migratesAllTablesToSqlite()
    {
        if (!QFile::exists(reference("ONDE.DBF")))
            QSKIP("reference data not available");
        QTemporaryDir directory;
        const QString database = directory.filePath("stag.sqlite");
        const sqlitemigration::Result result = sqlitemigration::migrateDirectory(QStringLiteral(STAG_REFERENCE_DATA), database);
        QVERIFY2(result.errors.isEmpty(), qPrintable(result.errors.join('\n')));
        QVERIFY(result.migratedTables.contains("MESSAGGI"));

        {
            QSqlDatabase sqlite = QSqlDatabase::addDatabase("QSQLITE", "verify");
            sqlite.setDatabaseName(database);
            QVERIFY(sqlite.open());
            QSqlQuery query(sqlite);
            QVERIFY(query.exec("SELECT COUNT(*) FROM ONDE"));
            QVERIFY(query.next());
            QCOMPARE(query.value(0).toInt(), 200);
            QVERIFY(query.exec("SELECT COUNT(*) FROM MESSAGGI"));
            QVERIFY(query.next());
            QCOMPARE(query.value(0).toInt(), 5661);
            QVERIFY(query.exec("SELECT length FROM _dbf_fields WHERE table_name = 'ONDE' AND name = 'CARTA'"));
            QVERIFY(query.next());
            QCOMPARE(query.value(0).toInt(), 36);
            sqlite.close();
        }
        QSqlDatabase::removeDatabase("verify");
    }
};

QTEST_MAIN(TestDbf)
#include "tst_dbf.moc"
