#include "alarmstore.h"
#include "sqlcheck.h"

#include "staglog.h"
#include "vbtypes.h"

#include <QSqlError>
#include <QSqlQuery>
#include <QVariant>

namespace stag {

static void checked(QSqlQuery &query, const char *what)
{
    if (!query.exec())
        log_str(QStringLiteral("%1: %2").arg(QLatin1String(what), query.lastError().text()), MED_PRIORITY, CosLog::Gen);
}

void ins_freq_all(QSqlDatabase &db, const QString &table, const QString &description)
{
    const QString name = table.toUpper();
    const QString alarm = description.left(50);
    QSqlQuery find(db);
    sqlPrepare(find, QStringLiteral("SELECT FREQ FROM \"%1\" WHERE ALLARME = ?").arg(name));
    find.addBindValue(alarm);
    checked(find, "ins_freq_all");
    QSqlQuery write(db);
    if (!find.next()) {
        sqlPrepare(write, QStringLiteral("INSERT INTO \"%1\" (ALLARME, FREQ) VALUES (?, 1)").arg(name));
        write.addBindValue(alarm);
    } else {
        const int freq = find.value(0).toInt();
        sqlPrepare(write, QStringLiteral("UPDATE \"%1\" SET FREQ = ? WHERE ALLARME = ?").arg(name));
        write.addBindValue(freq < MAX_FREQ_ALL ? freq + 1 : 1);
        write.addBindValue(alarm);
    }
    find.finish();
    checked(write, "ins_freq_all");
}

void ins_sto(QSqlDatabase &db, const QString &description, const QDateTime &when)
{
    QSqlQuery insert(db);
    sqlPrepare(insert, QStringLiteral("INSERT INTO STO (ALLARME, DATA_ALL, ORA_ALL) VALUES (?, ?, ?)"));
    insert.addBindValue(description.left(50));
    insert.addBindValue(when.date().toString(Qt::ISODate));
    insert.addBindValue(vb::cStrTime(when.time()).left(8));
    checked(insert, "ins_sto");

    // del_sto: the oldest records go first.
    QSqlQuery trim(db);
    sqlPrepare(trim, QStringLiteral("DELETE FROM STO WHERE rec_no NOT IN (SELECT rec_no FROM STO ORDER BY rec_no DESC LIMIT ?)"));
    trim.addBindValue(NUM_REC_STO);
    checked(trim, "del_sto");
}

} // namespace stag
