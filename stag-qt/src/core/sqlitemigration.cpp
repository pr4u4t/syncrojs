#include "sqlitemigration.h"

#include "dbf.h"

#include <QDate>
#include <QDir>
#include <QFileInfo>
#include <QSqlDatabase>
#include <QSqlError>
#include <QSqlQuery>

#include <cmath>

namespace sqlitemigration {
namespace {

QString quoted(const QString &identifier)
{
    return u'"' + QString(identifier).replace(u'"', QStringLiteral("\"\"")) + u'"';
}

QString columnType(const DbfTable::Field &field)
{
    switch (field.type) {
    case 'N':
    case 'F':
        return field.decimals > 0 ? QStringLiteral("REAL") : QStringLiteral("INTEGER");
    case 'L':
        return QStringLiteral("INTEGER");
    default:
        return QStringLiteral("TEXT");
    }
}

QVariant sqlValue(const DbfTable::Field &field, const QVariant &value)
{
    if (value.isNull())
        return QVariant();
    if ((field.type == 'N' || field.type == 'F') && field.decimals == 0) {
        const double number = value.toDouble();
        return std::floor(number) == number ? QVariant(qint64(number)) : QVariant(number);
    }
    if (field.type == 'L')
        return value.toBool() ? 1 : 0;
    if (field.type == 'D')
        return value.toDate().toString(Qt::ISODate);
    return value;
}

bool exec(QSqlQuery &query, const QString &sql, QString *error)
{
    if (query.exec(sql))
        return true;
    if (error)
        *error = query.lastError().text();
    return false;
}

} // namespace

bool migrateTable(const QString &connectionName, const QString &tableName, const DbfTable &table, QString *error)
{
    QSqlDatabase database = QSqlDatabase::database(connectionName);
    QSqlQuery query(database);

    QStringList columns{QStringLiteral("rec_no INTEGER PRIMARY KEY")};
    for (const DbfTable::Field &field : table.fields())
        columns.append(quoted(field.name) + u' ' + columnType(field));

    if (!exec(query, QStringLiteral("DROP TABLE IF EXISTS %1").arg(quoted(tableName)), error)
        || !exec(query, QStringLiteral("CREATE TABLE %1 (%2)").arg(quoted(tableName), columns.join(QStringLiteral(", "))), error))
        return false;
    QSqlQuery cleanup(database);
    cleanup.prepare(QStringLiteral("DELETE FROM _dbf_fields WHERE table_name = ?"));
    cleanup.addBindValue(tableName);
    if (!cleanup.exec()) {
        if (error)
            *error = cleanup.lastError().text();
        return false;
    }

    QSqlQuery fieldInsert(database);
    fieldInsert.prepare(QStringLiteral("INSERT INTO _dbf_fields (table_name, position, name, type, length, decimals) VALUES (?, ?, ?, ?, ?, ?)"));
    for (int index = 0; index < table.fields().size(); ++index) {
        const DbfTable::Field &field = table.fields().at(index);
        for (const QVariant &value : {QVariant(tableName), QVariant(index), QVariant(field.name), QVariant(QString(QChar::fromLatin1(field.type))),
                                      QVariant(field.length), QVariant(field.decimals)})
            fieldInsert.addBindValue(value);
        if (!fieldInsert.exec()) {
            if (error)
                *error = fieldInsert.lastError().text();
            return false;
        }
    }

    QStringList placeholders(table.fields().size() + 1, QStringLiteral("?"));
    QSqlQuery insert(database);
    insert.prepare(QStringLiteral("INSERT INTO %1 VALUES (%2)").arg(quoted(tableName), placeholders.join(QStringLiteral(", "))));
    for (int record = 0; record < table.recordCount(); ++record) {
        if (table.isDeleted(record))
            continue;
        insert.addBindValue(record + 1);
        for (int field = 0; field < table.fields().size(); ++field)
            insert.addBindValue(sqlValue(table.fields().at(field), table.value(record, field)));
        if (!insert.exec()) {
            if (error)
                *error = insert.lastError().text();
            return false;
        }
    }
    return true;
}

Result migrateDirectory(const QString &dbfDirectory, const QString &sqlitePath)
{
    Result result;
    const QString connectionName = QStringLiteral("stag-migration-%1").arg(quintptr(&result));
    {
        QSqlDatabase database = QSqlDatabase::addDatabase(QStringLiteral("QSQLITE"), connectionName);
        database.setDatabaseName(sqlitePath);
        if (!database.open()) {
            result.errors.append(database.lastError().text());
        } else {
            QSqlQuery query(database);
            QString error;
            if (!exec(query, QStringLiteral("CREATE TABLE IF NOT EXISTS _dbf_fields (table_name TEXT, position INTEGER, name TEXT, type TEXT, length INTEGER, decimals INTEGER, PRIMARY KEY (table_name, position))"), &error)) {
                result.errors.append(error);
            } else {
                const QFileInfoList files = QDir(dbfDirectory).entryInfoList({QStringLiteral("*.dbf"), QStringLiteral("*.DBF")}, QDir::Files, QDir::Name);
                for (const QFileInfo &file : files) {
                    DbfTable table;
                    const QString tableName = file.completeBaseName().toUpper();
                    if (!table.load(file.absoluteFilePath(), &error)) {
                        result.errors.append(QStringLiteral("%1: %2").arg(file.fileName(), error));
                        continue;
                    }
                    for (int field = 0; field < table.fields().size(); ++field) {
                        if (table.fields().at(field).name.compare(QStringLiteral("RUSSO"), Qt::CaseInsensitive) == 0)
                            table.setCodePage(field, vb::OemCodePage::Cp866);
                    }
                    database.transaction();
                    if (migrateTable(connectionName, tableName, table, &error)) {
                        database.commit();
                        result.migratedTables.append(tableName);
                    } else {
                        database.rollback();
                        result.errors.append(QStringLiteral("%1: %2").arg(file.fileName(), error));
                    }
                }
            }
            database.close();
        }
    }
    QSqlDatabase::removeDatabase(connectionName);
    return result;
}

} // namespace sqlitemigration
