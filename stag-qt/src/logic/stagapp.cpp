#include "stagapp.h"

#include "gesord.h"
#include "inibas.h"
#include "inifile.h"
#include "inistore.h"
#include "messages.h"
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
{
    stag::sta_tag = &m_staTag;
    stag::messaggioHook = [this](const QString &text, bool isAll, bool stay) { m_staBar.messaggio(text, isAll, stay); };
    m_timComPlc.setInterval(300);
    connect(&m_timComPlc, &QTimer::timeout, this, &StagApp::timTagPlc);

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

void StagApp::start()
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

    const stag::TipPlcLan &lan = stag::par_mac.com_plc_lan;
    m_fins.setRemoteHost(m_options.plcHostOverride.isEmpty() ? lan.ind_rem : m_options.plcHostOverride);
    m_fins.setRemotePort(m_options.plcPortOverride ? m_options.plcPortOverride : quint16(lan.por_rem.val()));
    m_fins.setTimeOut(int(lan.TimeOut.val()));
    m_fins.setEnabled(true);

    m_timComPlc.start();
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
            // invio_ini_par_mac / ini_plc follow with the par_mac port.
        } else {
            m_staBar.setLinNoComPlc(true);
            m_staTag.chkAllSftw();
        }
    }

    if (m_staTag.comOK()) {
        const bool green = m_staBar.ledPlcRx() == StaBarModel::vbColor(StaBarModel::vbGreen);
        m_staBar.setLedPlc(green ? StaBarModel::COL_DARK_Green : StaBarModel::vbGreen, StaBarModel::vbRed);
        m_staTag.aggiorna(m_fins);
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
