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
    // msg() without the log line, for keys that are legitimately absent (unused function keys).
    QString find(const QString &key) const { return m_texts.value(key); }
    // lingue_stag.imposta: caption of a control in the current language, empty when there is none.
    // imp_all: mg.item(key).item = text
    void setText(const QString &key, const QString &text) { m_texts.insert(key, text); }
    // A caption of a form that MESSAGGI does not hold, by its Italian text; the same text when there is none.
    QString captionText(const QString &italian) const { return m_captions.value(italian.trimmed(), italian); }
    QString controlText(const QString &form, const QString &control, int index) const;

private:
    QHash<QString, QString> m_texts;
    QHash<QString, QString> m_captions;
    QHash<QString, QString> m_controls;
};

inline QString msg(const QString &key)
{
    return Messages::instance().msg(key);
}
