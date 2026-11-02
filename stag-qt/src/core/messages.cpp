#include "messages.h"
#include "sqlcheck.h"

#include "staglog.h"

#include <QSqlDatabase>
#include <QSqlError>
#include <QSqlQuery>

namespace {

#include "translations.inc"

// lingue_stag: lin_att of the four languages the program is translated into.
constexpr int LIN_EN = 1;
constexpr int LIN_DE = 2;
constexpr int LIN_PL = 8;

template <typename Row>
const char *column(const Row &row, int linAtt)
{
    switch (linAtt) {
    case LIN_EN: return row.en;
    case LIN_DE: return row.de;
    case LIN_PL: return row.pl;
    default: return nullptr;
    }
}

// The text of a language: its own, else the table above, else the Italian rather than a blank field.
QString resolved(const QString &own, const QString &italian, int linAtt)
{
    // A leading "_" marks a text of the column that nobody has translated yet.
    const QString mine = own.trimmed().startsWith(u'_') ? QString() : own.trimmed();
    const QString it = italian.trimmed();
    if (linAtt == 0 || it.isEmpty() || (!mine.isEmpty() && mine != it))
        return linAtt == 0 || it.isEmpty() ? own : mine;
    if (it == QLatin1String("Frame1")) // a designer's leftover, never a caption
        return mine;
    for (const auto &row : TRANSLATIONS) {
        if (it == QString::fromUtf8(row.it)) {
            if (const char *text = column(row, linAtt))
                return QString::fromUtf8(text);
        }
    }
    for (const auto &row : TRANSLATION_PREFIXES) {
        const QString prefix = QString::fromUtf8(row.it);
        if (it.startsWith(prefix)) {
            if (const char *text = column(row, linAtt))
                return QString::fromUtf8(text) + it.mid(prefix.size());
        }
    }
    return mine.isEmpty() ? italian : mine;
}

} // namespace

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
            sqlPrepare(column, QStringLiteral("SELECT name FROM _dbf_fields WHERE table_name = 'MESSAGGI' AND position = ?"));
            column.addBindValue(linAtt + 3);
            if (column.exec() && column.next()) {
                const QString name = column.value(0).toString().replace(u'"', QStringLiteral("\"\""));
                QSqlQuery query(db);
                if (query.exec(QStringLiteral("SELECT CONTROLLO, \"%1\", ITALIAN FROM MESSAGGI WHERE trim(coalesce(FORM, '')) = ''").arg(name))) {
                    while (query.next()) {
                        const QString key = query.value(0).toString().trimmed();
                        if (!m_texts.contains(key))
                            m_texts.insert(key, resolved(query.value(1).toString(), query.value(2).toString(), linAtt));
                    }
                    ok = true;
                } else if (error) {
                    *error = query.lastError().text();
                }
                QSqlQuery controls(db);
                if (ok && controls.exec(QStringLiteral("SELECT FORM, CONTROLLO, INDICE, \"%1\", ITALIAN FROM MESSAGGI WHERE trim(coalesce(FORM, '')) <> ''").arg(name))) {
                    while (controls.next()) {
                        const QString text = resolved(controls.value(3).toString(), controls.value(4).toString(), linAtt).trimmed();
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
    m_captions.clear();
    for (const auto &row : TRANSLATIONS) {
        if (const char *text = column(row, linAtt))
            m_captions.insert(QString::fromUtf8(row.it), QString::fromUtf8(text));
    }
    for (const auto &row : EXTRA_MESSAGES) {
        const QString key = QString::fromUtf8(row.key);
        if (!m_texts.contains(key)) {
            const char *text = column(row, linAtt);
            m_texts.insert(key, QString::fromUtf8(text ? text : row.it));
        }
    }
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
