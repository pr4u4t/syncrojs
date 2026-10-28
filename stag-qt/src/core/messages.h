#pragma once

#include <QHash>
#include <QString>

// lingue_stag.msg(): texts from MESSAGGI (form = "", controllo = key) in the column lin_att + 3.
class Messages
{
public:
    static Messages &instance();

    bool load(const QString &sqlitePath, int linAtt, QString *error = nullptr);
    QString msg(const QString &key) const;

private:
    QHash<QString, QString> m_texts;
};

inline QString msg(const QString &key)
{
    return Messages::instance().msg(key);
}
