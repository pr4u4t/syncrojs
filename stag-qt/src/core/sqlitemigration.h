#pragma once

#include <QString>
#include <QStringList>

class DbfTable;

// One-time migration of the STAG dBase tables into a single SQLite database.
// Each DBF becomes a table of the same name with a 1-based `rec_no` keeping the physical
// record order DAO navigated in; deleted records are skipped. Original field definitions
// are kept in `_dbf_fields` so values can be formatted exactly as dBase stored them.
namespace sqlitemigration {

struct Result {
    QStringList migratedTables;
    QStringList errors;
};

Result migrateDirectory(const QString &dbfDirectory, const QString &sqlitePath);
bool migrateTable(const QString &connectionName, const QString &tableName, const DbfTable &table, QString *error);

} // namespace sqlitemigration
