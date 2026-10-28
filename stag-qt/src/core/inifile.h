#pragma once

#include <QString>
#include <QStringList>

// INI access with the semantics of the Win32 Get/WritePrivateProfileString calls STAG used:
// case-insensitive names, first match wins, values trimmed of spaces/tabs and one pair of
// matching quotes, untouched lines (comments, order, CRLF) preserved on write.
class IniFile
{
public:
    explicit IniFile(QString path);

    bool load(QString *error = nullptr);
    bool save(QString *error = nullptr) const;

    QString value(const QString &section, const QString &key, bool *found = nullptr) const;
    void setValue(const QString &section, const QString &key, const QString &value);

    // uti_stag.getFileINIString: a missing key is written back with its default value.
    QString getFileINIString(const QString &section, const QString &key, const QString &defaultValue);

    const QString &path() const { return m_path; }

private:
    int findSection(const QString &section) const;
    int findKey(int sectionLine, const QString &key) const;

    QString m_path;
    QStringList m_lines;
    QString m_eol = QStringLiteral("\r\n");
    bool m_finalEol = true;
};
