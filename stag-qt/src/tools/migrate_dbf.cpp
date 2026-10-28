#include "sqlitemigration.h"

#include <QCoreApplication>
#include <QTextStream>

int main(int argc, char *argv[])
{
    QCoreApplication app(argc, argv);
    QTextStream out(stdout);
    QTextStream err(stderr);
    const QStringList arguments = app.arguments();
    if (arguments.size() != 3) {
        err << "usage: stag-migrate-dbf <stag/dati directory> <output.sqlite>\n";
        return 2;
    }

    const sqlitemigration::Result result = sqlitemigration::migrateDirectory(arguments.at(1), arguments.at(2));
    for (const QString &table : result.migratedTables)
        out << "migrated " << table << '\n';
    for (const QString &error : result.errors)
        err << "error: " << error << '\n';
    return result.errors.isEmpty() ? 0 : 1;
}
