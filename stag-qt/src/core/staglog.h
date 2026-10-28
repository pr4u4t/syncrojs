#pragma once

#include <QObject>
#include <QString>
#include <QVector>

class IniFile;

// Port of uti_stag.log_str and the pro_set.Log() table filled by leg_par_sup.
enum class CosLog {
    Plc = 1,
    Ord,
    Spde,
    Rmt,
    Gen,
    Lis,
    Prob,
    Can,
    Cfg,
    Cur,
    SchTag,
    Azi,
    Eve,
};

constexpr int MAX_PRIORITY = 0;
constexpr int MED_PRIORITY = 5;
constexpr int DSK_DEF_PRIORITY = 5;

class StagLog : public QObject
{
    Q_OBJECT

public:
    struct TipLog {
        QString namFilLog;
        QString namFilBak;
        qint64 maxLenLog = 1400000;
        int dskPriority = DSK_DEF_PRIORITY;
    };

    static StagLog &instance();

    // leg_par_sup: file names relative to pro_pat, read (and defaulted) through getFileINIString.
    void configure(IniFile &ini, const QString &proPat);
    const TipLog &log(CosLog log) const { return m_logs[int(log)]; }

    void logStr(const QString &text, int priority, CosLog log, bool show = true);

signals:
    // frm_debug log box.
    void shown(const QString &line);

private:
    StagLog() = default;
    QVector<TipLog> m_logs = QVector<TipLog>(int(CosLog::Eve) + 1);
};

inline void log_str(const QString &text, int priority, CosLog log, bool show = true)
{
    StagLog::instance().logStr(text, priority, log, show);
}
