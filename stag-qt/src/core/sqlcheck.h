#pragma once

#include "staglog.h"

#include <QSqlError>
#include <QSqlQuery>

// A failed statement used to pass unnoticed: DAO raised an error here, which VB logged. Both return what
// Qt returned, so a caller that handles the failure still can.
inline bool sqlPrepare(QSqlQuery &query, const QString &statement)
{
    const bool ok = query.prepare(statement);
    if (!ok)
        log_str(QStringLiteral("SQL prepare fallita: ") + query.lastError().text() + QStringLiteral(" (") + statement + u')', MED_PRIORITY, CosLog::Gen);
    return ok;
}

inline bool sqlExec(QSqlQuery &query)
{
    const bool ok = query.exec();
    if (!ok)
        log_str(QStringLiteral("SQL fallita: ") + query.lastError().text() + QStringLiteral(" (") + query.lastQuery() + u')', MED_PRIORITY, CosLog::Gen);
    return ok;
}

inline bool sqlExec(QSqlQuery &query, const QString &statement)
{
    const bool ok = query.exec(statement);
    if (!ok)
        log_str(QStringLiteral("SQL fallita: ") + query.lastError().text() + QStringLiteral(" (") + statement + u')', MED_PRIORITY, CosLog::Gen);
    return ok;
}
