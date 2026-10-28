#include "staglog.h"

#include "inifile.h"
#include "vbcompat.h"

#include <QCoreApplication>
#include <QDateTime>
#include <QDir>
#include <QFile>
#include <QFileInfo>

StagLog &StagLog::instance()
{
    static StagLog log;
    return log;
}

void StagLog::configure(IniFile &ini, const QString &proPat)
{
    static const char *const items[] = {"Plc", "Ord", "Spde", "Rmt", "Gen", "Lis", "Prob", "Can", "Cfg", "Cur", "Sch_tag", "Azi", "Eve"};
    for (int i = 1; i <= int(CosLog::Eve); ++i) {
        const QString item = QString::fromLatin1(items[i - 1]);
        TipLog &entry = m_logs[i];
        entry.namFilLog = proPat + ini.getFileINIString(QStringLiteral("LOG"), QStringLiteral("Nam_fil_log_") + item, item + QStringLiteral(".log"));
        entry.namFilBak = proPat + ini.getFileINIString(QStringLiteral("LOG"), QStringLiteral("Nam_fil_bak_") + item, item + QStringLiteral("_old.log"));
        entry.maxLenLog = qint64(vb::val(ini.getFileINIString(QStringLiteral("LOG"), QStringLiteral("Max_len_log_") + item, QStringLiteral("1400000&"))));
        // str$() of a positive number has a leading space.
        entry.dskPriority = int(vb::val(ini.getFileINIString(QStringLiteral("LOG"), QStringLiteral("Dsk_priority_") + item, QStringLiteral(" %1").arg(DSK_DEF_PRIORITY))));
    }
}

void StagLog::logStr(const QString &text, int priority, CosLog log, bool show)
{
    // Format$(Now, "hh:mm:ss") used the Italian locale time separator.
    const QDateTime now = QDateTime::currentDateTime();
    const QString line = now.toString(QStringLiteral("dd/MM/yy")) + QStringLiteral(" | ") + now.toString(QStringLiteral("hh.mm.ss")) + QStringLiteral(" | ") + text;
    const TipLog &entry = m_logs[int(log)];
    if (priority <= DSK_DEF_PRIORITY && !entry.namFilLog.isEmpty()) {
        QFile file(entry.namFilLog);
        if (file.open(QIODevice::Append)) {
            file.write(vb::toAnsi(line) + "\r\n");
            file.close();
        }
        if (QFileInfo(entry.namFilLog).size() > entry.maxLenLog) {
            QFile::remove(entry.namFilBak);
            QFile::rename(entry.namFilLog, entry.namFilBak);
        }
    }
    if (show)
        emit shown(line);
    if (priority == MAX_PRIORITY) {
        qCritical("Fatal error: %s", qPrintable(text));
        QCoreApplication::exit(1);
    }
}
