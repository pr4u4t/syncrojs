#pragma once

#include "alarmgridmodel.h"
#include "classestato.h"
#include "collezioneordini.h"
#include "finsclient.h"
#include "paglisesemodel.h"
#include "plclink.h"
#include "paglisordmodel.h"
#include "pagpromodel.h"
#include "stabarmodel.h"

#include <QHash>
#include <QObject>
#include <QSqlDatabase>
#include <QTimer>

class PagMsgModel;
class SpdeLink;
class StaBarFormModel;

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
        // Off unless asked for: with it false nothing at all is written to the PLC.
        bool plcWrites = false;
    };

    explicit StagApp(const Options &options, QObject *parent = nullptr);

    StaBarModel *staBar() { return &m_staBar; }
    PagMsgModel *pagMsg() const { return m_pagMsg; }
    PagProModel *pagPro() { return &m_pagPro; }
    PagLisOrdModel *pagLisOrd() { return &m_pagLisOrd; }
    PagLisEseModel *pagLisEse() { return &m_pagLisEse; }
    // The pages created in load(), by form name.
    const QHash<QString, FormModel *> &pageModels() const { return m_pageModels; }
    ClasseStato &staTag() { return m_staTag; }
    FinsClient &fins() { return m_fins; }
    PlcLink &plc() { return m_plc; }

    // Principale.Main without the PLC: configuration, messages and order files.
    void load();
    void start();
    void timTagPlc();
    // key.cls shutdown: Ordini.ini first codes and production totals.
    void shutdown();

private:
    void legOrd();
    void loadPages();
    void loadSchTagPages();

    Options m_options;
    QSqlDatabase m_db;
    FinsClient m_fins;
    ClasseStato m_staTag;
    StaBarModel m_staBar;
    CollezioneOrdini m_ordMac;
    CollezioneOrdini m_ordMacEse;
    PagProModel m_pagPro;
    PagLisOrdModel m_pagLisOrd;
    PagLisEseModel m_pagLisEse;
    PagAllModel m_pagAll;
    PlcLink m_plc;
    SpdeLink *m_spde = nullptr;
    StaBarFormModel *m_staBarForm = nullptr;
    PagMsgModel *m_pagMsg = nullptr;
    QTimer m_timComSpde;
    QHash<QString, FormModel *> m_pageModels;
    QTimer m_timComPlc;
    bool m_inTimTagPlc = false;
};
