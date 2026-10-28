#include "inistore.h"

#include "inifile.h"

#include <QDir>
#include <QHash>

#include <memory>

namespace stag {
namespace {

QString g_dataDir = QStringLiteral(".");

QHash<QString, std::shared_ptr<IniFile>> &files()
{
    static QHash<QString, std::shared_ptr<IniFile>> cache;
    return cache;
}

} // namespace

void setDataDirectory(const QString &dir)
{
    g_dataDir = dir;
    files().clear();
}

QString dataDirectory()
{
    return g_dataDir;
}

QString resolveDataFile(const QString &name)
{
    const QDir dir(g_dataDir);
    if (dir.exists(name))
        return dir.filePath(name);
    const QStringList entries = dir.entryList(QDir::Files);
    for (const QString &entry : entries) {
        if (entry.compare(name, Qt::CaseInsensitive) == 0)
            return dir.filePath(entry);
    }
    return dir.filePath(name);
}

IniFile &iniFile(const QString &fname)
{
    const QString name = (fname.isEmpty() ? QStringLiteral("stag") : fname) + QStringLiteral(".ini");
    const QString key = name.toLower();
    auto &cache = files();
    auto found = cache.find(key);
    if (found == cache.end()) {
        auto ini = std::make_shared<IniFile>(resolveDataFile(name));
        ini->load();
        found = cache.insert(key, ini);
    }
    return *found.value();
}

QString getFileINIString(const QString &section, const QString &item, const QString &def, const QString &fname)
{
    return iniFile(fname).getFileINIString(section, item, def);
}

void setFileINIString(const QString &section, const QString &item, const QString &value, const QString &fname)
{
    IniFile &ini = iniFile(fname);
    ini.setValue(section, item, value);
    ini.save();
}

} // namespace stag
