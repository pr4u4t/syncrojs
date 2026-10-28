#pragma once

#include <QString>

class IniFile;

// uti_stag getFileINIString / setFileINIString: "<data dir>/<name>.ini", name defaults to the exe name (stag).
namespace stag {

void setDataDirectory(const QString &dir);
QString dataDirectory();
// Windows file names are case-insensitive: returns the existing file matching `name`, or dir/name.
QString resolveDataFile(const QString &name);

IniFile &iniFile(const QString &fname = QString());
QString getFileINIString(const QString &section, const QString &item, const QString &def, const QString &fname = QString());
void setFileINIString(const QString &section, const QString &item, const QString &value, const QString &fname = QString());

} // namespace stag
