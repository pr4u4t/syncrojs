#pragma once

#include "classestato.h"
#include "collezioneordini.h"
#include "finsclient.h"
#include "stabarmodel.h"

#include <QObject>
#include <QSqlDatabase>
#include <QTimer>

// Principale.Main start-up and the Tim_com_PLC loop (dlg_tag_bas.tim_tag_plc), as far as ported.
class StagApp : public QObject
{
    Q_OBJECT

public:
    struct Options {
        QString dataDir;        // pro_set.pro_pat: stag.ini, *.dat, logs
        QString sqlitePath;     // migrated DBF tables
        QString plcHostOverride;
        quint16 plcPortOverride = 0;
    };

    explicit StagApp(const Options &options, QObject *parent = nullptr);

    StaBarModel *staBar() { return &m_staBar; }
    ClasseStato &staTag() { return m_staTag; }
    FinsClient &fins() { return m_fins; }

    void start();
    void timTagPlc();
    // key.cls shutdown: Ordini.ini first codes and production totals.
    void shutdown();

private:
    void legOrd();

    Options m_options;
    QSqlDatabase m_db;
    FinsClient m_fins;
    ClasseStato m_staTag;
    StaBarModel m_staBar;
    CollezioneOrdini m_ordMac;
    CollezioneOrdini m_ordMacEse;
    QTimer m_timComPlc;
    bool m_inTimTagPlc = false;
};
