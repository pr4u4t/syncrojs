#include "messages.h"

#include "staglog.h"

#include <QSqlDatabase>
#include <QSqlError>
#include <QSqlQuery>

static QString controlKey(const QString &form, const QString &control, int index)
{
    return form.trimmed().toLower() + u'|' + control.trimmed().toLower() + u'|' + QString::number(index);
}

Messages &Messages::instance()
{
    static Messages messages;
    return messages;
}

bool Messages::load(const QString &sqlitePath, int linAtt, QString *error)
{
    m_texts.clear();
    m_controls.clear();
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
                QSqlQuery controls(db);
                if (ok && controls.exec(QStringLiteral("SELECT FORM, CONTROLLO, INDICE, \"%1\" FROM MESSAGGI WHERE trim(coalesce(FORM, '')) <> ''").arg(name))) {
                    while (controls.next()) {
                        const QString text = controls.value(3).toString().trimmed();
                        if (!text.isEmpty())
                            m_controls.insert(controlKey(controls.value(0).toString(), controls.value(1).toString(), controls.value(2).toInt()), text);
                    }
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

QString Messages::controlText(const QString &form, const QString &control, int index) const
{
    return m_controls.value(controlKey(form, control, index));
}
