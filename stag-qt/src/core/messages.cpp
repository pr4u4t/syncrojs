#include "messages.h"

#include "staglog.h"

#include <QSqlDatabase>
#include <QSqlError>
#include <QSqlQuery>

Messages &Messages::instance()
{
    static Messages messages;
    return messages;
}

bool Messages::load(const QString &sqlitePath, int linAtt, QString *error)
{
    m_texts.clear();
    const QString connectionName = QStringLiteral("stag-messages");
    bool ok = false;
    {
        QSqlDatabase db = QSqlDatabase::addDatabase(QStringLiteral("QSQLITE"), connectionName);
        db.setDatabaseName(sqlitePath);
        db.setConnectOptions(QStringLiteral("QSQLITE_OPEN_READONLY"));
        if (db.open()) {
            QSqlQuery column(db);
            column.prepare(QStringLiteral("SELECT name FROM _dbf_fields WHERE table_name = 'MESSAGGI' AND position = ?"));
            column.addBindValue(linAtt + 3);
            if (column.exec() && column.next()) {
                const QString name = column.value(0).toString().replace(u'"', QStringLiteral("\"\""));
                QSqlQuery query(db);
                if (query.exec(QStringLiteral("SELECT CONTROLLO, \"%1\" FROM MESSAGGI WHERE trim(coalesce(FORM, '')) = ''").arg(name))) {
                    while (query.next()) {
                        const QString key = query.value(0).toString().trimmed();
                        if (!m_texts.contains(key))
                            m_texts.insert(key, query.value(1).toString());
                    }
                    ok = true;
                } else if (error) {
                    *error = query.lastError().text();
                }
            } else if (error) {
                *error = QStringLiteral("MESSAGGI has no column for language %1").arg(linAtt);
            }
            db.close();
        } else if (error) {
            *error = db.lastError().text();
        }
    }
    QSqlDatabase::removeDatabase(connectionName);
    return ok;
}

QString Messages::msg(const QString &key) const
{
    const auto found = m_texts.constFind(key);
    if (found != m_texts.constEnd())
        return found.value();
    log_str(QStringLiteral("msg: Impossibile trovare il messaggio <") + key + QStringLiteral("> nel database"), MED_PRIORITY, CosLog::Gen);
    return {};
}
