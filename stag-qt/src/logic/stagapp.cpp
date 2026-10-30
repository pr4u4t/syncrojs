#include "stagapp.h"

#include "gesord.h"
#include "inibas.h"
#include "inifile.h"
#include "inistore.h"
#include "abilitazionimodel.h"
#include "insordmodel.h"
#include "pwdmodel.h"
#include "messages.h"
#include "ondemodel.h"
#include "patternmodel.h"
#include "parcfgschtag.h"
#include "parcurtag.h"
#include "parmac.h"
#include "staglog.h"
#include "stagglobals.h"
#include "uihooks.h"
#include "vbcompat.h"
#include "vbtypes.h"

#include <QDir>
#include <QSqlError>

#include <cmath>
#include <limits>

StagApp::StagApp(const Options &options, QObject *parent)
    : QObject(parent)
    , m_options(options)
    , m_pagPro(&m_ordMac)
    , m_pagLisOrd(&m_ordMac)
    , m_pagLisEse(&m_ordMacEse)
    , m_plc(&m_fins, &m_staTag, &m_ordMac)
{
    stag::sta_tag = &m_staTag;
    stag::messaggioHook = [this](const QString &text, bool isAll, bool stay) { m_staBar.messaggio(text, isAll, stay); };
    m_timComPlc.setInterval(300);
    connect(&m_timComPlc, &QTimer::timeout, this, &StagApp::timTagPlc);

    connect(&m_staTag, &ClasseStato::ord, &m_pagPro, &PagProModel::orderStatus);
    connect(&m_staTag, &ClasseStato::run, &m_staBar, &StaBarModel::eveTagRun);
    connect(&m_staTag, &ClasseStato::reset, &m_staBar, &StaBarModel::eveTagReset);
    connect(&m_staTag, &ClasseStato::mac, &m_staBar, &StaBarModel::eveTagMac);
    connect(&m_staTag, &ClasseStato::modo, this, [this](int staMod) {
        m_staBar.context().macInManuale = m_staTag.bit(ClasseStato::MAC_IN_MANUALE, 1);
        m_staBar.eveTagModo(staMod);
    });
    connect(&m_staTag, &ClasseStato::spde, this, [this](bool ok) {
        m_staBar.context().spdeOK = m_staTag.spdeOK();
        m_staBar.context().macInManuale = m_staTag.bit(ClasseStato::MAC_IN_MANUALE, 1);
        m_staBar.eveTagSpde(ok);
    });
    connect(&m_staTag, &ClasseStato::vel, &m_staBar, &StaBarModel::eveTagVel);
    connect(&m_staTag, &ClasseStato::velmax, &m_staBar, &StaBarModel::eveTagVelmax);
    connect(&m_staTag, &ClasseStato::liv, &m_staBar, &StaBarModel::eveTagLiv);
    connect(&m_staTag, &ClasseStato::gruall, this, [this](const QString &v) {
        const QVector<bool> att = m_staBar.eveTagGruall(v);
        for (int pos = ClasseStato::POS_GRU_HAR; pos <= ClasseStato::POS_GRU_MAL; ++pos)
            m_staTag.setGruAllAtt(pos, att[pos]);
    });
    connect(&m_fins, &FinsClient::logMessage, this, [](const QString &text) { log_str(text, MED_PRIORITY, CosLog::Plc); });
}

void StagApp::load()
{
    QString error;
    QString proPat = QDir(m_options.dataDir).absolutePath();
    if (!proPat.endsWith(u'/'))
        proPat += u'/';
    stag::setDataDirectory(proPat);
    IniFile &ini = stag::iniFile();
    stag::legParSup(ini, QString(), proPat);

    m_db = QSqlDatabase::addDatabase(QStringLiteral("QSQLITE"), QStringLiteral("stag"));
    m_db.setDatabaseName(m_options.sqlitePath);
    if (!m_db.open())
        qWarning("database: %s", qPrintable(m_db.lastError().text()));

    stag::leggiNumLivTag(ini);
    stag::legParMac(ini);
    stag::leggiCfg(m_db, ini);
    stag::leggiParCurTag(m_db);
    stag::leggiPro();
    stag::iniLiv();
    stag::par_mac_ute = stag::par_mac;
    stag::par_abi_ute = stag::par_abi;
    legOrd();
    stag::iniVelMax();

    if (!Messages::instance().load(m_options.sqlitePath, stag::pro_set.lin_att, &error))
        qWarning("messages: %s", qPrintable(error));
    m_pagLisOrd.load();
    m_pagLisEse.load();
    loadPages();
}

void StagApp::start()
{
    load();

    const stag::TipPlcLan &lan = stag::par_mac.com_plc_lan;
    m_fins.setRemoteHost(m_options.plcHostOverride.isEmpty() ? lan.ind_rem : m_options.plcHostOverride);
    m_fins.setRemotePort(m_options.plcPortOverride ? m_options.plcPortOverride : quint16(lan.por_rem.val()));
    m_fins.setTimeOut(int(lan.TimeOut.val()));
    m_fins.setEnabled(true);
    m_plc.setWritesEnabled(m_options.plcWrites);
    if (!m_options.plcWrites)
        log_str(QStringLiteral("PlcLink: scrittura verso PLC disabilitata"), MED_PRIORITY, CosLog::Plc);

    m_timComPlc.start();
}

void StagApp::loadPages()
{
    m_pagAll.load();
    m_pageModels.insert(QStringLiteral("Pag_all"), &m_pagAll);
    connect(&m_staTag, &ClasseStato::gruall, &m_pagAll, &PagAllModel::setGroups);

    const QVariantList frequencyWidths{600, 180};
    const QStringList frequencyHeaders{QStringLiteral("ALLARME"), QStringLiteral("FREQUENZA")};
    for (const auto &[code, grid] : {std::pair("mac", "dbgrid_a_m"), std::pair("con", "dbgrid_a_c"), std::pair("har", "dbgrid_a_h"),
                                     std::pair("war", "dbgrid_a_w"), std::pair("qe1", "dbgrid_a_q1"), std::pair("qe2", "dbgrid_a_q2"),
                                     std::pair("qe3", "dbgrid_a_q3")}) {
        const QString name = QString::fromLatin1(code);
        auto *alarms = new AlarmGridModel(name, m_db, this);
        alarms->load();
        connect(&m_staTag, &ClasseStato::sezall, alarms, &AlarmGridModel::setAlarms);
        m_pageModels.insert(QStringLiteral("All_") + name, alarms);
        m_pageModels.insert(QStringLiteral("Fre_") + name,
                             new AlarmTableModel(QString::fromLatin1(grid),
                                                 QStringLiteral("SELECT ALLARME, FREQ FROM \"F_A_%1\" ORDER BY FREQ DESC").arg(name.toUpper()),
                                                 frequencyHeaders, frequencyWidths, m_db, this));
    }
    auto *flutes = new OndeModel(&m_ordMac, m_db, this);
    flutes->load();
    m_pageModels.insert(QStringLiteral("Onde"), flutes);
    auto *patterns = new PatternModel(&m_ordMac, m_db, this);
    patterns->load();
    m_pageModels.insert(QStringLiteral("Pattern"), patterns);
    auto *insOrd = new InsOrdModel(&m_ordMac, m_db, this);
    insOrd->load();
    connect(&m_staTag, &ClasseStato::modo, insOrd, &InsOrdModel::machineMode);
    m_pageModels.insert(QStringLiteral("Ins_ord"), insOrd);

    m_pageModels.insert(QStringLiteral("Pwd_frm"), new PwdModel(this));
    auto *abilitazioni = new AbilitazioniModel(&m_ordMac, this);
    abilitazioni->setCommand(new Rmt(QStringLiteral("abilitazioni_plc"), stag::ChannelPlc, &m_plc, abilitazioni));
    m_pageModels.insert(QStringLiteral("Abilitazioni"), abilitazioni);
    m_pagPro.setCommand(new Rmt(QStringLiteral("rmt_plc_pag_pro"), stag::ChannelPlc, &m_plc, &m_pagPro));

    const auto dialog = [this](const char *form, OrderDialogModel::Kind kind, OrderListModel &list, const char *key, CollezioneOrdini *orders) {
        auto *model = new OrderDialogModel(kind, orders, &m_ordMac, this);
        list.setDialog(QString::fromLatin1(key), model);
        m_pageModels.insert(QString::fromLatin1(form), model);
    };
    dialog("Del_ord", OrderDialogModel::DeleteOrders, m_pagLisOrd, "F3", &m_ordMac);
    dialog("Mov_ord", OrderDialogModel::MoveOrders, m_pagLisOrd, "F4", &m_ordMac);
    dialog("Cop_ord", OrderDialogModel::CopyOrders, m_pagLisOrd, "F5", &m_ordMac);
    dialog("Tro_ord", OrderDialogModel::FindOrder, m_pagLisOrd, "F6", &m_ordMac);
    dialog("Del_ord_ese", OrderDialogModel::DeleteExecuted, m_pagLisEse, "F3", &m_ordMacEse);
    dialog("Mov_ese", OrderDialogModel::CopyExecuted, m_pagLisEse, "F5", &m_ordMacEse);
    dialog("Tro_ord_ese", OrderDialogModel::FindExecuted, m_pagLisEse, "F6", &m_ordMacEse);

    m_pageModels.insert(QStringLiteral("Sto_all"),
                         new AlarmTableModel(QStringLiteral("dbgrid_sto_all"),
                                             QStringLiteral("SELECT ALLARME, DATA_ALL, ORA_ALL FROM STO ORDER BY DATA_ALL DESC, ORA_ALL DESC"),
                                             {QStringLiteral("ALLARME"), QStringLiteral("DATA"), QStringLiteral("ORA")},
                                             {560, 120, 100}, m_db, this));
}

void StagApp::legOrd()
{
    // cnv_ord (conversion of pre-2005 order files) is not needed: the files are already in the current format.
    stag::ord_mac = &m_ordMac;
    if (m_ordMac.Init(false) == stag::RET_BAD)
        log_str(QStringLiteral("Leg_ord: Problema sull'inizializzazione della classe Ord_mac"), MED_PRIORITY, CosLog::Ord);
    m_ordMac.Leg_dat_ord();
    stag::ord_mac_ese = &m_ordMacEse;
    if (m_ordMacEse.Init(true) == stag::RET_BAD)
        log_str(QStringLiteral("Leg_ord: Problema sull'inizializzazione della classe Ord_mac"), MED_PRIORITY, CosLog::Ord);
    m_ordMacEse.Leg_dat_ord();
}

void StagApp::timTagPlc()
{
    // Tim_com_PLC can fire again while readArea waits for the PLC; VB did not re-enter here either.
    if (m_inTimTagPlc)
        return;
    m_inTimTagPlc = true;

    if (!m_staTag.comOK()) {
        m_staBar.setLedPlc(StaBarModel::vbRed, StaBarModel::vbRed);
        if (!m_fins.error()) {
            m_staTag.setComOK(true);
            m_staBar.setLinNoComPlc(false);
            m_staBar.setFrmComBack(StaBarModel::COL_DARK_Green);
            m_plc.sendStartup();
        } else {
            m_staBar.setLinNoComPlc(true);
            m_staTag.chkAllSftw();
        }
    }

    if (m_staTag.comOK()) {
        const bool green = m_staBar.ledPlcRx() == StaBarModel::vbColor(StaBarModel::vbGreen);
        m_staBar.setLedPlc(green ? StaBarModel::COL_DARK_Green : StaBarModel::vbGreen, StaBarModel::vbRed);
        m_staTag.aggiorna(m_fins);
        m_plc.tick();
    }

    // calcolo dei metri mancanti al fine ordine
    try {
        qint32 metMax = std::numeric_limits<qint32>::min();
        qint32 larMm = 0;
        if (m_ordMac.Count() > 0) {
            const OrdinePtr ord = m_ordMac.item(1);
            qint32 metLin = 0;
            for (int i = 1; i <= 3; ++i) {
                if (vb::trim(ord->ord(i, QStringLiteral("Ord_liv"))).isEmpty())
                    continue;
                const int livOrd = int(vb::val(ord->ord(i, QStringLiteral("Ord_liv"))));
                if (livOrd <= 0 || livOrd > 3)
                    continue;
                const qint32 totScatole = vb::cLng(vb::val(ord->ord(i, QStringLiteral("sca_tot_num"))));
                const qint32 resa = vb::cLng(vb::val(ord->ord(i, QStringLiteral("Sca_per_tag"))));
                const qint32 lunScatola = vb::intDiv(vb::val(ord->ord(i, QStringLiteral("sca_lun"))), stag::TODECMM);
                larMm = vb::cLng(larMm + vb::val(ord->ord(i, QStringLiteral("sca_lar"))) / stag::TODECMM * resa);
                const qint32 totTagli = m_staTag.num_tag_eff(livOrd);
                double metMan = 0;
                if (resa != 0)
                    metMan = ((double(totScatole) / resa - totTagli) / 1000) * lunScatola;
                if (metMan != std::floor(metMan))
                    metMan = std::floor(metMan) + 1;
                if (metMan > metMax)
                    metMax = vb::cLng(metMan);
                metLin = vb::intDiv(double(totTagli) * lunScatola, 1000);
            }
            if (stag::pro_set.abi_reg_rep_pro == QString::fromLatin1(stag::STR_TRUE))
                m_staTag.lunghezza(metLin, larMm);
            // abi_mis_ang (feet) conversions are not ported: the plant uses metres.
            const QString temCo = stag::calTemCo(metMax, int(vb::val(m_staBar.txtVel())));
            const QString metMan = metMax == std::numeric_limits<qint32>::min() ? QStringLiteral("0") : QString::number(metMax);
            m_staBar.setMetMan(temCo, metMan, metMax < 0);
        }
    } catch (const vb::Error &e) {
        log_str(QStringLiteral("on error intervenuta in tim_tag_plc: %1 (%2)").arg(QString::fromLatin1(e.what())).arg(e.number), MED_PRIORITY, CosLog::Gen);
    }

    m_staTag.tempi(false, false, false);
    m_inTimTagPlc = false;
}

void StagApp::shutdown()
{
    m_ordMacEse.Fine();
    m_ordMac.Fine();
    stag::scriviPro();
    m_timComPlc.stop();
}
