#include "inifile.h"

#include "vbcompat.h"

#include <QFile>
#include <QSaveFile>

namespace {

constexpr int kMaxValueLength = 2047; // tmp = String$(2048, 32) in getFileINIString

QString trimBlanks(const QString &text)
{
    qsizetype start = 0;
    qsizetype end = text.size();
    while (start < end && (text.at(start) == u' ' || text.at(start) == u'\t'))
        ++start;
    while (end > start && (text.at(end - 1) == u' ' || text.at(end - 1) == u'\t'))
        --end;
    return text.mid(start, end - start);
}

bool sectionName(const QString &line, QString *name)
{
    const QString trimmed = trimBlanks(line);
    if (!trimmed.startsWith(u'['))
        return false;
    const qsizetype close = trimmed.indexOf(u']');
    if (name)
        *name = trimBlanks(close < 0 ? trimmed.mid(1) : trimmed.mid(1, close - 1));
    return true;
}

} // namespace

IniFile::IniFile(QString path)
    : m_path(std::move(path))
{
}

bool IniFile::load(QString *error)
{
    m_lines.clear();
    QFile file(m_path);
    if (!file.exists())
        return true; // Win32 treats a missing file as empty
    if (!file.open(QIODevice::ReadOnly)) {
        if (error)
            *error = file.errorString();
        return false;
    }
    QString text = vb::fromAnsi(file.readAll());
    m_eol = text.contains(QStringLiteral("\r\n")) || !text.contains(u'\n') ? QStringLiteral("\r\n") : QStringLiteral("\n");
    text.replace(QStringLiteral("\r\n"), QStringLiteral("\n"));
    m_lines = text.split(u'\n');
    m_finalEol = !m_lines.isEmpty() && m_lines.last().isEmpty();
    if (m_finalEol)
        m_lines.removeLast();
    return true;
}

bool IniFile::save(QString *error) const
{
    QSaveFile file(m_path);
    if (!file.open(QIODevice::WriteOnly)) {
        if (error)
            *error = file.errorString();
        return false;
    }
    QString text = m_lines.join(m_eol);
    if (!m_lines.isEmpty() && m_finalEol)
        text += m_eol;
    file.write(vb::toAnsi(text));
    if (!file.commit()) {
        if (error)
            *error = file.errorString();
        return false;
    }
    return true;
}

int IniFile::findSection(const QString &section) const
{
    for (int line = 0; line < m_lines.size(); ++line) {
        QString name;
        if (sectionName(m_lines.at(line), &name) && name.compare(section, Qt::CaseInsensitive) == 0)
            return line;
    }
    return -1;
}

int IniFile::findKey(int sectionLine, const QString &key) const
{
    for (int line = sectionLine + 1; line < m_lines.size(); ++line) {
        if (sectionName(m_lines.at(line), nullptr))
            break;
        const qsizetype equals = m_lines.at(line).indexOf(u'=');
        if (equals >= 0 && trimBlanks(m_lines.at(line).left(equals)).compare(key, Qt::CaseInsensitive) == 0)
            return line;
    }
    return -1;
}

QString IniFile::value(const QString &section, const QString &key, bool *found) const
{
    const int sectionLine = findSection(section);
    const int keyLine = sectionLine < 0 ? -1 : findKey(sectionLine, key);
    if (found)
        *found = keyLine >= 0;
    if (keyLine < 0)
        return {};
    const QString &line = m_lines.at(keyLine);
    QString result = trimBlanks(line.mid(line.indexOf(u'=') + 1));
    if (result.size() >= 2 && (result.front() == u'"' || result.front() == u'\'') && result.back() == result.front())
        result = result.mid(1, result.size() - 2);
    return result.left(kMaxValueLength);
}

void IniFile::setValue(const QString &section, const QString &key, const QString &value)
{
    m_finalEol = true;
    int sectionLine = findSection(section);
    if (sectionLine < 0) {
        m_lines.append(QStringLiteral("[%1]").arg(section));
        m_lines.append(QStringLiteral("%1=%2").arg(key, value));
        return;
    }
    const int keyLine = findKey(sectionLine, key);
    if (keyLine >= 0) {
        const QString &line = m_lines.at(keyLine);
        m_lines[keyLine] = trimBlanks(line.left(line.indexOf(u'='))) + u'=' + value;
        return;
    }
    // New keys go after the last non-blank line of the section.
    int insertAt = sectionLine + 1;
    for (int line = sectionLine + 1; line < m_lines.size() && !sectionName(m_lines.at(line), nullptr); ++line) {
        if (!trimBlanks(m_lines.at(line)).isEmpty())
            insertAt = line + 1;
    }
    m_lines.insert(insertAt, QStringLiteral("%1=%2").arg(key, value));
}

QString IniFile::getFileINIString(const QString &section, const QString &key, const QString &defaultValue)
{
    bool found = false;
    const QString result = value(section, key, &found);
    if (found)
        return result;
    setValue(section, key, defaultValue);
    save();
    return defaultValue;
}
