#include "schtagpages.h"

#include "classestato.h"
#include "inibas.h"
#include "inistore.h"
#include "messages.h"
#include "parcfgschtag.h"
#include "parcurtag.h"
#include "stabarmodel.h"
#include "staglog.h"
#include "uihooks.h"
#include "vbcompat.h"
#include "vbtypes.h"

#include <QSqlQuery>
#include <QSqlRecord>

using namespace stag;

namespace {

struct SchTagFil {
    const char *control;
    FixStr TipSchTagLiv::*field;
    bool user;
    bool defaults;
};
struct SchTagScr {
    const char *control;
    FixStr TipSchTagLiv::*field;
};
struct SchTagCopy {
    const char *from;
    const char *to;
};
struct SchTagIni {
    const char *key;
    FixStr TipSchTagLiv::*field;
};

#include "schtagtables.inc"

template <typename T>
struct Span {
    const T *first = nullptr;
    size_t count = 0;
    const T *begin() const { return first; }
    const T *end() const { return first + count; }
};
template <typename T, size_t N>
Span<T> span(const T (&table)[N])
{
    return {table, N};
}

struct PageTables {
    Span<SchTagFil> filAtt;
    Span<SchTagFil> filDef;
    Span<SchTagScr> scrAtt;
    Span<SchTagScr> scrDef;
    Span<SchTagCopy> copy;
    Span<const char *> tab;
    Span<FixStr TipSchTagLiv::*> defInAtt;
};

PageTables tables(int page)
{
    switch (page) {
    case 1:
        return {span(FIL_ATT_1), span(FIL_DEF_1), span(SCR_ATT_1), span(SCR_DEF_1), span(COPY_1), span(TAB_1), span(DEF_IN_ATT_1)};
    case 2:
        return {span(FIL_ATT_2), span(FIL_DEF_2), span(SCR_ATT_2), span(SCR_DEF_2), span(COPY_2), span(TAB_2), span(DEF_IN_ATT_2)};
    case 4:
        return {span(FIL_ATT_4), span(FIL_DEF_4), span(SCR_ATT_4), span(SCR_DEF_4), span(COPY_4), span(TAB_4), span(DEF_IN_ATT_4)};
    default:
        return {span(FIL_ATT_5), span(FIL_DEF_5), span(SCR_ATT_5), span(SCR_DEF_5), span(COPY_5), span(TAB_5), span(DEF_IN_ATT_5)};
    }
}

// The labels with a value's real unit, kept by fil_par_sch_tag_liv and the _KeyUp events.
struct ScaledLabel {
    int page;
    const char *label;
    const char *text;
    double divisor;
};
constexpr ScaledLabel LABELS[] = {
    {1, "lab_dia_ruo_mis", "txt_dia_ruo_mis_att", 100},
    {1, "lab_dia_pull_roll", "txt_dia_pull_roll_att", 100},
    {1, "lab_rap_rid_mot_lam", "txt_rap_rid_mot_lam_att", 10000},
    {2, "lab_vel_zon_sinc_vmin", "txt_vel_zon_sinc_vmin_att", 10},
    {2, "lab_sov_lam", "txt_sov_lam_att", 10},
    {5, "lab_txt_sov_tap_usc_per_sca", "txt_sov_tap_usc_per_sca_att", 10},
};

QString number(double value)
{
    return vb::cStr(value);
}

void scrSchTagIni(const Span<SchTagIni> &keys, const TipSchTag &source, int liv)
{
    const QString section = QStringLiteral("sch_tag_liv_%1").arg(liv);
    for (const SchTagIni &row : keys)
        setFileINIString(section, QLatin1String(row.key), (source.sch_tag_liv(liv).*row.field).trimmed());
}

struct CfgField {
    const char *control;
    FixStr TipParCfgSchTagLiv::*field;
};
constexpr CfgField CFG_FIELDS[] = {
    {"vel_max_tag", &TipParCfgSchTagLiv::vel_max_tag},
    {"tem_acc_tgl_lun", &TipParCfgSchTagLiv::tem_acc_tgl_lun},
    {"tem_dec_tgl_lun", &TipParCfgSchTagLiv::tem_dec_tgl_lun},
    {"tem_acc_tgl_cor", &TipParCfgSchTagLiv::tem_acc_tgl_cor},
    {"tem_dec_tgl_cor", &TipParCfgSchTagLiv::tem_dec_tgl_cor},
    {"kp_nor", &TipParCfgSchTagLiv::Kp_nor},
    {"ki_nor", &TipParCfgSchTagLiv::Ki_nor},
    {"fil_kp", &TipParCfgSchTagLiv::fil_kp},
    {"kv_nor", &TipParCfgSchTagLiv::Kv_nor},
    {"lim_lav_kv", &TipParCfgSchTagLiv::lim_lav_Kv},
    {"gir_max_mot_lam", &TipParCfgSchTagLiv::gir_max_mot_lam},
};

constexpr quint32 LIGHT_GRAY_COL = 0xE0E0E0;

QString at(const char *name, int index)
{
    return QStringLiteral("%1(%2)").arg(QLatin1String(name)).arg(index);
}

QList<QVariantMap> readRecords(QSqlDatabase &db, const QString &order)
{
    QList<QVariantMap> records;
    QSqlQuery q(db);
    if (!q.exec(QStringLiteral("SELECT * FROM PAR_CFG ORDER BY ") + order))
        return records;
    while (q.next()) {
        QVariantMap row;
        const QSqlRecord record = q.record();
        for (int i = 0; i < record.count(); ++i)
            row.insert(record.fieldName(i), record.value(i));
        records << row;
    }
    return records;
}

QString field(const QVariantMap &row, const QString &name)
{
    return row.value(name).toString().trimmed();
}

int fieldInt(const QVariantMap &row, const QString &name)
{
    return int(vb::val(field(row, name)));
}

} // namespace

SchTagConfig::SchTagConfig(QSqlDatabase db, PlcLink *link, CollezioneOrdini *orders, QObject *parent)
    : QObject(parent)
    , m_db(db)
    , m_link(link)
    , m_orders(orders)
    , m_macro(new Rmt(QStringLiteral("rmt_sta_bar_macro"), ChannelMacro, link, this))
{
    connect(&link->boards(), &SchTagLink::configurationEnded, this, &SchTagConfig::ended);
    connect(m_macro, &Rmt::retok, this, [this](const QString &text) {
        log_str(QStringLiteral("rmt_sta_bar_macro RETOK"), MED_PRIORITY, CosLog::Cfg);
        if (!text.isEmpty())
            messaggio(text, false);
        m_macro->resetta();
    });
    connect(m_macro, &Rmt::retbad, this, [this](const QString &error) {
        log_str(QStringLiteral("rmt_sta_bar_macro RETBAD"), MED_PRIORITY, CosLog::Cfg);
        if (!error.isEmpty())
            messaggio(error, false);
        m_macro->resetta();
    });
}

void SchTagConfig::ended(int liv, int sendCase, bool ack)
{
    const auto copyAttDescr = [liv](TipParMac &to, const TipParMac &from) {
        to.config_sch_tag_att(liv).descr = from.config_sch_tag_att(liv).descr;
        to.config_sch_tag_att(liv).num_mot_liv = from.config_sch_tag_att(liv).num_mot_liv;
    };
    const QString section = QStringLiteral("sch_tag_liv_%1").arg(liv);
    const QString failed = msg(QStringLiteral("ope_ann")) + QStringLiteral(". ") + msg(QStringLiteral("err_com_sch_tag")) + u' ' + QString::number(liv);
    const QString done = msg(QStringLiteral("tra_eff_sch_tag")) + u' ' + QString::number(liv);

    switch (sendCase) {
    case CARICA_NEW_CFG:
        if (ack) {
            if (!par_mac.config_sch_tag_att(liv).descr.trimmed().isEmpty())
                resCfgAtt(m_db, liv);
            copyAttDescr(par_mac, par_mac_ute);
            setCfgAtt(m_db, par_mac.config_sch_tag_att(liv).descr.trimmed(), liv);
            par_mac.par_cfg_att(liv) = par_mac_ute.par_cfg_att(liv);
            setFileINIString(section, QStringLiteral("config_att"), par_mac.config_sch_tag_att(liv).descr.str());
        } else {
            copyAttDescr(par_mac_ute, par_mac);
            par_mac_ute.par_cfg_att(liv) = par_mac.par_cfg_att(liv);
        }
        emit configurationChanged(liv, false);
        break;
    case MODIFICA_OLD_CFG:
        if (ack) {
            modificaCfg(m_db, par_mac.config_sch_tag_att(liv).descr.trimmed(), liv);
            par_mac.par_cfg_att(liv) = par_mac_ute.par_cfg_att(liv);
            const int level = calLiv(liv);
            if (level != 0)
                vel_max_tag_liv(level) = qint16(par_mac.par_cfg_att(liv).vel_max_tag.val());
            else
                log_str(QStringLiteral("Sub conferma_mod_cfg_liv del Modulo sch_tag_bas. Si è verificata una situazione errata: liv_tmp = 0"), MED_PRIORITY, CosLog::Cfg);
            emit configurationChanged(liv, false);
            if (par_mac.config_sch_tag_att(liv).descr.trimmed() == QLatin1String("DEFAULT")) {
                par_mac.par_cfg_def(liv) = par_mac_ute.par_cfg_def(liv);
                emit configurationChanged(liv, true);
            }
        } else {
            par_mac_ute.par_cfg_att(liv) = par_mac.par_cfg_att(liv);
            emit configurationChanged(liv, false);
            if (par_mac.config_sch_tag_att(liv).descr.trimmed() == QLatin1String("DEFAULT"))
                par_mac_ute.par_cfg_def(liv) = par_mac.par_cfg_def(liv);
        }
        break;
    case INSERIMENTO_CFG_DEFAULT:
        if (ack) {
            salvaCfgDef(m_db, QStringLiteral("DEFAULT"), liv);
            copyAttDescr(par_mac, par_mac_ute);
            par_mac.config_sch_tag_def(liv).descr = par_mac_ute.config_sch_tag_def(liv).descr;
            par_mac.par_cfg_att(liv) = par_mac_ute.par_cfg_att(liv);
            par_mac.par_cfg_def(liv) = par_mac_ute.par_cfg_def(liv);
        } else {
            copyAttDescr(par_mac_ute, par_mac);
            par_mac_ute.config_sch_tag_def(liv).descr = par_mac.config_sch_tag_def(liv).descr;
            par_mac_ute.par_cfg_att(liv) = par_mac.par_cfg_att(liv);
            par_mac_ute.par_cfg_def(liv) = par_mac.par_cfg_def(liv);
        }
        emit configurationChanged(liv, false);
        emit configurationChanged(liv, true);
        break;
    case INSERIMENTO_CFG_ATTIVA:
        if (ack) {
            salvaCfgAtt(m_db, par_mac_ute.config_sch_tag_att(liv).descr.str(), liv);
            copyAttDescr(par_mac, par_mac_ute);
            par_mac.par_cfg_att(liv) = par_mac_ute.par_cfg_att(liv);
        } else {
            copyAttDescr(par_mac_ute, par_mac);
            par_mac_ute.par_cfg_att(liv) = par_mac.par_cfg_att(liv);
        }
        emit configurationChanged(liv, false);
        break;
    case NOT_CFG:
        if (ack) {
            par_mac.sch_tag.sch_tag_liv(liv) = par_mac_ute.sch_tag.sch_tag_liv(liv);
            scrSchTagIni(span(INI_ATT), par_mac.sch_tag, liv);
            if (num_com_sch_tag > 1 && liv == Livello)
                emit valuesChanged(liv);
            messaggioLiv(done, false, liv);
            if (m_macro->esegui(PLC_PARAM_CFG_MAC_PARAM_CUR_TAG, liv, 0)) {
                log_str(QStringLiteral("Ges_fine_trasmissione: Invio parametri macchina e parametri di configurazione a PLC"), MED_PRIORITY, CosLog::Cfg);
            } else {
                messaggio(msg(QStringLiteral("ope_eff_par")), true);
                log_str(QStringLiteral("Ges_fine_trasmissione: ATTENZIONE! Non è stato possibile inviare i parametri macchina e parametri di configurazione a PLC"), MED_PRIORITY, CosLog::Cfg);
            }
        } else {
            par_mac_ute.sch_tag.sch_tag_liv(liv) = par_mac.sch_tag.sch_tag_liv(liv);
            if (num_com_sch_tag == 1 && Livello == liv)
                emit valuesChanged(liv);
            messaggioLiv(failed, true, liv);
        }
        break;
    case CAMBIO_STATO_MACCHINA:
        if (ack) {
            if (!par_mac.config_sch_tag_att(liv).descr.trimmed().isEmpty())
                resCfgAtt(m_db, liv);
            par_abi = par_abi_ute;
            scrParAbi();
            par_mac.car_tec.num_mot_liv_tag = par_mac_ute.car_tec.num_mot_liv_tag;
            scrParMac();
            copyAttDescr(par_mac, par_mac_ute);
            setCfgAtt(m_db, par_mac.config_sch_tag_att(liv).descr.trimmed(), liv);
            par_mac.par_cfg_att(liv) = par_mac_ute.par_cfg_att(liv);
            setFileINIString(section, QStringLiteral("config_att"), par_mac.config_sch_tag_att(liv).descr.str());
            par_mac.config_sch_tag_def(liv).descr = par_mac_ute.config_sch_tag_def(liv).descr;
            par_mac.par_cfg_def(liv) = par_mac_ute.par_cfg_def(liv);
            setFileINIString(section, QStringLiteral("config_def"), par_mac.config_sch_tag_def(liv).descr.str());
            emit configurationChanged(liv, false);
            clearMessagWin(msg(QStringLiteral("abi_un_mot")));
            messaggioLiv(done, false, liv);
            if (m_macro->esegui(PLC_PARAM_MAC_PARAM_CFG_MAC, liv, 0)) {
                log_str(QStringLiteral("Ges_fine_trasmissione: Invio curva di taglio e parametri di configurazione a PLC"), MED_PRIORITY, CosLog::Cfg);
            } else {
                messaggio(msg(QStringLiteral("ope_eff_par")), true);
                log_str(QStringLiteral("Ges_fine_trasmissione: ATTENZIONE! Non \u00E8 stato possibile inviare i dati di curva di taglio e parametri di configurazione a PLC"), MED_PRIORITY, CosLog::Cfg);
            }
        } else {
            copyAttDescr(par_mac_ute, par_mac);
            par_mac_ute.par_cfg_att(liv) = par_mac.par_cfg_att(liv);
            par_abi_ute = par_abi;
            par_mac_ute.config_sch_tag_def(liv).descr = par_mac.config_sch_tag_def(liv).descr;
            par_mac_ute.par_cfg_def(liv) = par_mac.par_cfg_def(liv);
            emit configurationChanged(liv, false);
            messaggioLiv(failed, true, liv);
        }
        break;
    default:
        break;
    }

    if (ack && (sendCase == CARICA_NEW_CFG || sendCase == INSERIMENTO_CFG_ATTIVA || sendCase == INSERIMENTO_CFG_DEFAULT || sendCase == MODIFICA_OLD_CFG))
        afterConfiguration(liv);
}

// The velocity is compared after conferma_* copied it, so chg is always false and the orders are not
// sent again: VB's code as it stands.
void SchTagConfig::afterConfiguration(int liv)
{
    messaggioLiv(msg(QStringLiteral("tra_eff_sch_tag")) + u' ' + QString::number(liv), false, liv);
    const bool chg = par_mac_ute.par_cfg_att(liv).vel_max_tag.str() != par_mac.par_cfg_att(liv).vel_max_tag.str();
    m_link->forceOrder(CORRENTE, m_orders->Count() > 0 && chg);
    m_link->forceOrder(SUCCESSIVO, m_orders->Count() > 1 && chg);
}

SchTagPageBase::SchTagPageBase(PlcLink *link, ClasseStato *state, QObject *parent)
    : FormModel(parent)
    , m_link(link)
    , m_state(state)
{
}

void SchTagPageBase::setCommand(Rmt *command)
{
    m_command = command;
    connect(command, &Rmt::retok, this, [this](const QString &text) {
        if (!text.isEmpty())
            messaggio(text, false);
        m_command->resetta();
    });
    connect(command, &Rmt::retbad, this, [this](const QString &error) {
        if (!error.isEmpty())
            messaggio(error, true);
        m_command->resetta();
    });
}

bool SchTagPageBase::ready(int liv)
{
    if (!com_ini_sch_tag(liv)) {
        messaggio(msg(QStringLiteral("com_int_con_sch")), true);
        return false;
    }
    if (!m_state->bit(ClasseStato::StaPlcBit(ClasseStato::SCH_TAG_LIV_1_PRONTA + liv - 1), 1)) {
        messaggio(msg(QStringLiteral("sch_tag_del_liv")) + u' ' + QString::number(liv) + u' ' + msg(QStringLiteral("non_pro")), true);
        return false;
    }
    return true;
}

bool SchTagPageBase::verEseOpe(bool tot)
{
    if (!pro_set.ute_wrk) {
        messaggio(msg(QStringLiteral("acc_pwd")), true);
        return false;
    }
    if (!tot) {
        if (!ready(Livello))
            return false;
    } else {
        for (int i = 1; i <= int(par_mac.car_tec.num_liv_tag.val()); ++i) {
            if (!ready(i))
                return false;
        }
    }
    if (editing())
        return false;
    if (!m_state->comOK()) {
        messaggio(msg(QStringLiteral("com_int_con_mac")), true);
        return false;
    }
    return true;
}

void SchTagPageBase::sendConfigure(int sendCase, int liv, bool all, bool quietWhenBusy)
{
    m_link->boards().setSendCase(sendCase);
    if (m_command->esegui(CONFIGURA, liv, 0, all))
        messaggio(msg(quietWhenBusy ? QStringLiteral("ope_in_cor") : QStringLiteral("inv_par_sch_tag")), false);
    else
        messaggio(msg(QStringLiteral("imp_ese_ope")), !quietWhenBusy);
}

void SchTagPageBase::setEditable(const QStringList &controls, bool editable)
{
    for (const QString &control : controls)
        set(control, QStringLiteral("readOnly"), !editable);
}

void SchTagPageBase::focus(const QString &control)
{
    set(control, QStringLiteral("focusCount"), ++m_focusCount);
}

void SchTagPageBase::idle()
{
    focus(QStringLiteral("txt_idle"));
}

// Form_KeyDown turns Return and the arrows into Tab; only the fields enabled for the edit can be reached.
void SchTagPageBase::controlEvent(const QString &name, int index, const QString &event, const QVariantMap &args)
{
    const QString control = index >= 0 ? QStringLiteral("%1(%2)").arg(name).arg(index) : name;
    if (event == QLatin1String("Change")) {
        setText(control, args.value(QStringLiteral("text")).toString());
        changed(control);
        commit();
    } else if (event == QLatin1String("KeyDown")) {
        const int key = args.value(QStringLiteral("key")).toInt();
        int step = 0;
        if (key == Qt::Key_Return || key == Qt::Key_Enter || key == Qt::Key_Down || key == Qt::Key_Tab)
            step = 1;
        else if (key == Qt::Key_Up || key == Qt::Key_Backtab)
            step = -1;
        const int position = m_tab.indexOf(control);
        if (step == 0 || position < 0 || m_tab.isEmpty())
            return;
        for (int i = 1; i <= m_tab.size(); ++i) {
            const QString &next = m_tab.at((position + step * i + m_tab.size() * 2) % m_tab.size());
            const QVariant enabled = value(next, QStringLiteral("enabled"));
            if (!enabled.isValid() || enabled.toBool()) {
                focus(next);
                commit();
                return;
            }
        }
    }
}

SetParSchTagModel::SetParSchTagModel(int page, QSqlDatabase db, PlcLink *link, ClasseStato *state, QObject *parent)
    : SchTagPageBase(link, state, parent)
    , m_page(page)
    , m_db(db)
{
    const PageTables t = tables(page);
    for (const char *const name : t.tab)
        m_attControls << QString::fromLatin1(name);
    m_tab = m_attControls;
    for (const QString &control : std::as_const(m_attControls))
        set(control, QStringLiteral("modelFocus"), true);
    for (const SchTagFil &row : t.filDef)
        set(QString::fromLatin1(row.control), QStringLiteral("readOnly"), true);
    if (page == 1) {
        for (int i = 0; i < 15; ++i) {
            set(at("txt_lun_tag", i), QStringLiteral("readOnly"), true);
            set(at("txt_vel_tag", i), QStringLiteral("readOnly"), true);
            set(at("txt_lun_tag", i), QStringLiteral("modelFocus"), true);
            set(at("txt_vel_tag", i), QStringLiteral("modelFocus"), true);
        }
    }
    if (page == 4) {
        connect(&link->boards(), &SchTagLink::informationRead, this, [this](int liv) {
            filInf(liv);
            commit();
        });
    }
}

void SetParSchTagModel::setPlcCommand(Rmt *command)
{
    m_plc = command;
    connect(command, &Rmt::retok, this, [this](const QString &text) {
        const int liv = Livello;
        for (int i = 1; i <= int(par_mac.car_tec.num_liv_tag.val()); ++i) {
            if (i != liv && cur_tag(i).descr == cur_tag(liv).descr)
                cur_tag(i) = cur_tag(liv);
        }
        scrParCurTagDbf(m_db, liv);
        m_plc->resetta();
        if (!text.isEmpty())
            messaggio(text, false);
    });
    connect(command, &Rmt::retbad, this, [this](const QString &error) {
        cur_tag(Livello) = m_curUte[size_t(Livello)];
        m_plc->resetta();
        if (!error.isEmpty())
            messaggio(error, true);
    });
}

bool SetParSchTagModel::cutOption() const
{
    return par_mac.opt.cut_mar == QLatin1String("1") || cutPatOption()
        || par_mac.opt.cut_pat_auto(1) == QLatin1String("1") || par_mac.opt.cut_pat_auto(2) == QLatin1String("1") || par_mac.opt.cut_pat_auto(3) == QLatin1String("1");
}

bool SetParSchTagModel::cutPatOption() const
{
    return par_mac.opt.cut_pat(1) == QLatin1String("1") || par_mac.opt.cut_pat(2) == QLatin1String("1") || par_mac.opt.cut_pat(3) == QLatin1String("1");
}

void SetParSchTagModel::filAtt(int liv)
{
    for (const SchTagFil &row : tables(m_page).filAtt) {
        const TipParMac &source = row.user ? par_mac_ute : par_mac;
        const TipSchTag &tree = row.defaults ? source.sch_tag_def : source.sch_tag;
        setText(QString::fromLatin1(row.control), (tree.sch_tag_liv(liv).*row.field).trimmed());
    }
    setLabels();
    commit();
}

void SetParSchTagModel::filDef(int liv)
{
    for (const SchTagFil &row : tables(m_page).filDef) {
        const TipParMac &source = row.user ? par_mac_ute : par_mac;
        const TipSchTag &tree = row.defaults ? source.sch_tag_def : source.sch_tag;
        setText(QString::fromLatin1(row.control), (tree.sch_tag_liv(liv).*row.field).trimmed());
    }
    commit();
}

void SetParSchTagModel::setLabels()
{
    for (const ScaledLabel &label : LABELS) {
        if (label.page == m_page)
            setText(QString::fromLatin1(label.label), number(vb::val(value(QString::fromLatin1(label.text)).toString()) / label.divisor));
    }
}

void SetParSchTagModel::changed(const QString &control)
{
    for (const ScaledLabel &label : LABELS) {
        if (label.page == m_page && control == QLatin1String(label.text))
            setText(QString::fromLatin1(label.label), number(vb::val(value(control).toString()) / label.divisor));
    }
}

void SetParSchTagModel::filInf(int liv)
{
    const TipParInf &inf = par_mac.sch_tag.par_inf(liv);
    setText(QStringLiteral("txt_ver_pro_sch_tag"), inf.ver_pro_sch_tag.str());
    setText(QStringLiteral("txt_ver_sch_tag"), inf.ver_sch_tag.str());
    setText(QStringLiteral("txt_ver_pic"), inf.ver_PIC.str());
    setText(QStringLiteral("txt_ver_rad_qua_con_arr"), inf.ver_rad_qua_con_arr.str());
    setText(QStringLiteral("txt_qua_bit_rad_qua_con_arr"), inf.qua_bit_rad_qua_con_arr.str());
    setText(QStringLiteral("txt_per_arr_rad_qua_con_arr"), inf.per_arr_rad_qua_con_arr.str());
    setText(QStringLiteral("txt_ver_rad_qua_sen_arr"), inf.ver_rad_qua_sen_arr.str());
    setText(QStringLiteral("txt_qua_bit_rad_qua_sen_arr"), inf.qua_bit_rad_qua_sen_arr.str());
}

void SetParSchTagModel::filCur(int liv)
{
    for (int i = 1; i <= 15; ++i) {
        setText(at("txt_lun_tag", i - 1), QString::number(cur_tag(liv).Lun_tag(i)));
        setText(at("txt_vel_tag", i - 1), QString::number(cur_tag(liv).vel_max(i)));
    }
}

void SetParSchTagModel::iniForm()
{
    const PageTables t = tables(m_page);
    if (m_page == 4) {
        const bool on = cutOption();
        set(QStringLiteral("frame1"), QStringLiteral("enabled"), on);
        if (!on) {
            for (const SchTagFil &row : t.filAtt) {
                setText(QString::fromLatin1(row.control), QString());
                set(QString::fromLatin1(row.control), QStringLiteral("enabled"), false);
            }
            for (const SchTagFil &row : t.filDef)
                setText(QString::fromLatin1(row.control), QString());
        } else {
            for (const SchTagFil &row : t.filAtt)
                set(QString::fromLatin1(row.control), QStringLiteral("enabled"), true);
            for (const char *name : {"txt_vel_ftc_att", "txt_dis_cen_mac_sen_fuo_car_att", "txt_dis_edg_ftc_att"})
                set(QLatin1String(name), QStringLiteral("enabled"), cutPatOption());
        }
    } else if (m_page == 5) {
        const bool on = par_mac.opt.sca_liv(Livello) != QLatin1String("0");
        set(QStringLiteral("lab_txt_sov_tap_usc_per_sca"), QStringLiteral("enabled"), on);
        for (const SchTagFil &row : t.filAtt)
            set(QString::fromLatin1(row.control), QStringLiteral("enabled"), on);
        if (!on) {
            setText(QStringLiteral("lab_txt_sov_tap_usc_per_sca"), QStringLiteral("0"));
            for (const SchTagFil &row : t.filAtt)
                setText(QString::fromLatin1(row.control), QStringLiteral("0"));
            for (const SchTagFil &row : t.filDef)
                setText(QString::fromLatin1(row.control), QStringLiteral("0"));
        }
    }
}

void SetParSchTagModel::enableAtt(const QString &first)
{
    setEditable(m_attControls, true);
    m_tab = m_attControls;
    focus(first);
    commit();
}

void SetParSchTagModel::disableAll()
{
    setEditable(m_attControls, false);
    if (m_page == 1) {
        for (int i = 0; i < 15; ++i) {
            set(at("txt_lun_tag", i), QStringLiteral("readOnly"), true);
            set(at("txt_vel_tag", i), QStringLiteral("readOnly"), true);
        }
    }
    idle();
    commit();
}

void SetParSchTagModel::activate(const QString &)
{
    m_mod = false;
    m_modCur = false;
    iniForm();
    setText(QStringLiteral("txt_liv_tag"), QString::number(Livello));
    if (m_page == 4)
        setText(QStringLiteral("lab_liv"), QString::number(Livello));
    const bool fill = m_page == 4 ? cutOption() : m_page == 5 ? par_mac.opt.sca_liv(Livello) == QLatin1String("1") : true;
    if (fill) {
        filAtt(Livello);
        filDef(Livello);
    }
    if (m_page == 1)
        filCur(Livello);
    if (m_page == 4)
        filInf(Livello);
    disableAll();
}

void SetParSchTagModel::scrAttUte(int liv)
{
    for (const SchTagScr &row : tables(m_page).scrAtt)
        par_mac_ute.sch_tag.sch_tag_liv(liv).*row.field = value(QString::fromLatin1(row.control)).toString();
}

void SetParSchTagModel::scrDefUte(int liv)
{
    for (const SchTagScr &row : tables(m_page).scrDef)
        par_mac_ute.sch_tag_def.sch_tag_liv(liv).*row.field = value(QString::fromLatin1(row.control)).toString();
}

void SetParSchTagModel::attInDef(bool tot)
{
    const int liv = int(vb::val(value(QStringLiteral("txt_liv_tag")).toString()));
    for (const SchTagCopy &row : tables(m_page).copy)
        setText(QString::fromLatin1(row.to), value(QString::fromLatin1(row.from)).toString());
    if (tot) {
        par_mac_ute.sch_tag_def = par_mac_ute.sch_tag;
        par_mac.sch_tag_def = par_mac_ute.sch_tag_def;
        for (int i = 1; i <= int(par_mac.car_tec.num_liv_tag.val()); ++i)
            scrSchTagIni(span(INI_DEF), par_mac.sch_tag_def, i);
    } else {
        scrDefUte(liv);
        par_mac.sch_tag_def.sch_tag_liv(liv) = par_mac_ute.sch_tag_def.sch_tag_liv(liv);
        scrSchTagIni(span(INI_DEF), par_mac.sch_tag_def, liv);
    }
    commit();
}

void SetParSchTagModel::defInAtt(bool tot)
{
    if (tot) {
        par_mac_ute.sch_tag = par_mac.sch_tag_def;
        return;
    }
    for (FixStr TipSchTagLiv::*const member : tables(m_page).defInAtt)
        par_mac_ute.sch_tag.sch_tag_liv(Livello).*member = par_mac.sch_tag_def.sch_tag_liv(Livello).*member;
}

bool SetParSchTagModel::functionKey(const QString &key, int shift)
{
    const int level = int(vb::val(value(QStringLiteral("txt_liv_tag")).toString()));
    if (key == QLatin1String("ESC")) {
        if (shift != 0 || !editing())
            return true;
        if (m_mod) {
            m_mod = false;
            par_mac_ute = par_mac;
            if (m_page == 1) {
                for (int i = 1; i <= int(par_mac.car_tec.num_liv_tag.val()); ++i)
                    par_mac.com_sch_tag(i).com_att = false;
            }
            filAtt(level);
        }
        if (m_modCur) {
            m_modCur = false;
            filCur(level);
        }
        disableAll();
        return true;
    }
    if (key == QLatin1String("F2")) {
        if (shift == 0 && m_page == 4 ? (verEseOpe(false) && cutOption()) : shift == 0 && verEseOpe(false)) {
            m_mod = true;
            enableAtt(m_page == 1 ? QStringLiteral("txt_dia_ruo_mis_att") : m_page == 2 ? QStringLiteral("txt_vel_zon_sinc_vmin_att")
                      : m_page == 4 ? QStringLiteral("txt_off_tag_reg_att") : QStringLiteral("txt_sov_tap_usc_per_sca_att"));
        } else if (shift == 2 && m_page == 1) {
            if (!pro_set.ute_wrk) {
                messaggio(msg(QStringLiteral("acc_pwd")), true);
            } else if (!editing()) {
                if (!m_state->comOK()) {
                    messaggio(msg(QStringLiteral("com_int_con_mac")), true);
                } else {
                    m_modCur = true;
                    m_tab.clear();
                    for (int i = 0; i < 15; ++i) {
                        m_tab << at("txt_lun_tag", i);
                        set(at("txt_lun_tag", i), QStringLiteral("readOnly"), false);
                    }
                    for (int i = 0; i < 15; ++i) {
                        m_tab << at("txt_vel_tag", i);
                        set(at("txt_vel_tag", i), QStringLiteral("readOnly"), false);
                    }
                    focus(at("txt_lun_tag", 0));
                    commit();
                }
            }
        }
        return true;
    }
    if (key == QLatin1String("F3")) {
        if (m_mod && shift == 0) {
            messaggio(msg(QStringLiteral("ope_in_cor")), false, true);
            scrAttUte(level);
            num_com_sch_tag = 1;
            sendConfigure(NOT_CFG, level, false, false);
            m_mod = false;
            disableAll();
        } else if (m_modCur && shift == 2 && m_page == 1) {
            messaggio(msg(QStringLiteral("ope_in_cor")), false, true);
            m_curUte[size_t(level)] = cur_tag(level);
            for (int i = 1; i <= 15; ++i) {
                cur_tag(level).Lun_tag(i) = qint32(vb::val(value(at("txt_lun_tag", i - 1)).toString()));
                cur_tag(level).vel_max(i) = qint32(vb::val(value(at("txt_vel_tag", i - 1)).toString()));
            }
            if (!m_plc || !m_plc->esegui(PARAM_CUR_TAG, level, 0, false))
                messaggio(msg(QStringLiteral("imp_ese_ope")), false, false);
            m_modCur = false;
            disableAll();
        }
        return true;
    }
    if (key == QLatin1String("F4")) {
        if ((shift == 0 || shift == 2) && verEseOpe(shift == 2)) {
            messaggio(msg(QStringLiteral("ope_in_cor")), false, false);
            num_com_sch_tag = shift == 2 ? int(par_mac.car_tec.num_liv_tag.val()) : 1;
            defInAtt(shift == 2);
            sendConfigure(NOT_CFG, Livello, shift == 2, true);
        }
        return true;
    }
    if (key == QLatin1String("F5")) {
        if (!pro_set.ute_wrk) {
            messaggio(msg(QStringLiteral("acc_pwd")), true);
        } else if (!editing()) {
            if (m_page == 4 && !cutOption()) {
                messaggio(msg(QStringLiteral("cut_to_mark_non_pre")), true);
            } else {
                messaggio(msg(QStringLiteral("ope_in_cor")), false, false);
                if (shift == 0 || shift == 2)
                    attInDef(shift == 2);
                messaggio(msg(QStringLiteral("ope_eff")), false, false);
            }
        }
        return true;
    }
    if (key == QLatin1String("F6") && m_page == 4) {
        if (shift != 0)
            return true;
        if (!pro_set.ute_wrk) {
            messaggio(msg(QStringLiteral("acc_pwd")), true);
        } else if (!m_mod) {
            if (!m_state->bit(ClasseStato::StaPlcBit(ClasseStato::SCH_TAG_LIV_1_PRONTA + Livello - 1), 1)) {
                messaggio(msg(QStringLiteral("sch_tag_del_liv")) + u' ' + QString::number(Livello) + u' ' + msg(QStringLiteral("non_pro")), true);
            } else {
                messaggio(msg(QStringLiteral("ope_in_cor")), false, false);
                num_com_sch_tag = 1;
                if (m_command->esegui(INFORMAZIONI, Livello, 0))
                    messaggio(msg(QStringLiteral("inv_par_sch_tag")), false);
                else
                    messaggio(msg(QStringLiteral("imp_ese_ope")), true);
            }
        }
        return true;
    }
    if (key == QLatin1String("F7")) {
        const int levels = int(par_mac.car_tec.num_liv_tag.val());
        if (shift == 0 && !editing() && levels > 1) {
            Livello = Livello < levels ? Livello + 1 : 1;
            setText(QStringLiteral("txt_liv_tag"), QString::number(Livello));
            if (m_page == 5)
                iniForm();
            if (m_page != 5 || par_mac.opt.sca_liv(Livello) == QLatin1String("1")) {
                filAtt(Livello);
                filDef(Livello);
            }
            if (m_page == 1)
                filCur(Livello);
            if (m_page == 4)
                filInf(Livello);
            commit();
        }
        return true;
    }
    if (key == QLatin1String("F1") || key == QLatin1String("F10"))
        return shift != 0 || editing();
    if (key == QLatin1String("F8"))
        return editing() || (shift == 0 && m_page == 1) || (shift != 0 && shift != 2);
    if (key == QLatin1String("F9"))
        return editing() || (shift == 0 && m_page == 5) || (shift != 0 && shift != 2);
    return false;
}

SetParSchTag3Model::SetParSchTag3Model(QSqlDatabase db, PlcLink *link, ClasseStato *state, QObject *parent)
    : SchTagPageBase(link, state, parent)
    , m_db(db)
{
    for (const CfgField &row : CFG_FIELDS) {
        const QString att = QStringLiteral("txt_%1_att").arg(QLatin1String(row.control));
        m_tab << att;
        set(att, QStringLiteral("modelFocus"), true);
        set(QStringLiteral("txt_%1_def").arg(QLatin1String(row.control)), QStringLiteral("readOnly"), true);
    }
}

void SetParSchTag3Model::filAtt(int liv)
{
    setText(at("lab_con_att", liv - 1), par_mac.config_sch_tag_att(liv).descr.trimmed());
    for (const CfgField &row : CFG_FIELDS)
        setText(QStringLiteral("txt_%1_att").arg(QLatin1String(row.control)), (par_mac.par_cfg_att(liv).*row.field).trimmed());
    setText(QStringLiteral("lab_ki_nor"), number(vb::val(value(QStringLiteral("txt_ki_nor_att")).toString()) / 10));
    commit();
}

void SetParSchTag3Model::filDef(int liv)
{
    setText(at("lab_con_def", liv - 1), par_mac.config_sch_tag_def(liv).descr.str());
    for (const CfgField &row : CFG_FIELDS)
        setText(QStringLiteral("txt_%1_def").arg(QLatin1String(row.control)), (par_mac.par_cfg_def(liv).*row.field).trimmed());
    commit();
}

void SetParSchTag3Model::changed(const QString &control)
{
    if (control == QLatin1String("txt_ki_nor_att"))
        setText(QStringLiteral("lab_ki_nor"), number(vb::val(value(control).toString()) / 10));
}

void SetParSchTag3Model::iniForm()
{
    static constexpr quint32 WHITE = 0xFFFFFF;
    static constexpr quint32 WRONG_MOTORS = 0xB5717B;
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    setText(QStringLiteral("txt_liv_tag"), QString::number(Livello));
    for (int i = 1; i <= 3; ++i) {
        const bool used = i <= levels;
        setText(at("lab_con_att", i - 1), used ? par_mac.config_sch_tag_att(i).descr.str() : QString());
        setText(at("lab_con_def", i - 1), used ? par_mac.config_sch_tag_def(i).descr.str() : QString());
        setText(at("lab_num_mot_abi", i - 1), used ? QString::number(par_abi.num_mot_abi(i)) : QString());
        const bool fine = !used || par_mac.config_sch_tag_att(i).num_mot_liv == int(par_mac.car_tec.num_mot_liv_tag.val())
            || par_mac.config_sch_tag_att(i).descr.trimmed().isEmpty();
        set(at("frm_liv", i - 1), QStringLiteral("backColor"), StaBarModel::vbColor(fine ? WHITE : WRONG_MOTORS));
    }
}

void SetParSchTag3Model::leave()
{
    QStringList controls;
    for (const CfgField &row : CFG_FIELDS)
        controls << QStringLiteral("txt_%1_att").arg(QLatin1String(row.control));
    setEditable(controls, false);
    idle();
    commit();
}

void SetParSchTag3Model::activate(const QString &)
{
    m_ope = Nessuna;
    iniForm();
    filAtt(Livello);
    filDef(Livello);
    leave();
}

bool SetParSchTag3Model::verEseOpe3()
{
    if (!pro_set.ute_wrk) {
        messaggio(msg(QStringLiteral("acc_pwd")), true);
        return false;
    }
    if (!ready(Livello) || editing())
        return false;
    if (!m_state->comOK()) {
        messaggio(msg(QStringLiteral("com_int_con_mac")), true);
        return false;
    }
    return true;
}

void SetParSchTag3Model::scrAttUte(int liv)
{
    for (const CfgField &row : CFG_FIELDS)
        par_mac_ute.par_cfg_att(liv).*row.field = value(QStringLiteral("txt_%1_att").arg(QLatin1String(row.control))).toString();
}

void SetParSchTag3Model::scrDefUte(int liv)
{
    for (const CfgField &row : CFG_FIELDS)
        par_mac_ute.par_cfg_def(liv).*row.field = (par_mac_ute.par_cfg_att(liv).*row.field).str();
}

bool SetParSchTag3Model::functionKey(const QString &key, int shift)
{
    const QString first = QStringLiteral("txt_vel_max_tag_att");
    const auto edit = [&](Ope ope) {
        m_ope = ope;
        for (const QString &control : std::as_const(m_tab))
            set(control, QStringLiteral("readOnly"), false);
        focus(first);
        commit();
    };
    if (key == QLatin1String("ESC")) {
        if (shift == 0 && editing()) {
            m_ope = Nessuna;
            par_mac_ute = par_mac;
            const int level = int(vb::val(value(QStringLiteral("txt_liv_tag")).toString()));
            filAtt(level);
            filDef(level);
            leave();
        }
        return true;
    }
    if (shift != 0)
        return key != QLatin1String("F8") && key != QLatin1String("F9");
    if (key == QLatin1String("F2")) {
        if (verEseOpe3()) {
            for (const QString &control : std::as_const(m_tab))
                setText(control, QString());
            edit(Inserimento);
        }
        return true;
    }
    if (key == QLatin1String("F3")) {
        if (!verEseOpe3())
            return true;
        if (par_mac.config_sch_tag_att(Livello).descr.trimmed().isEmpty()) {
            messaggio(msg(QStringLiteral("ins_cfg_liv")), true);
            return true;
        }
        return false;
    }
    if (key == QLatin1String("F4")) {
        if (verEseOpe3()) {
            if (!par_mac.config_sch_tag_att(int(vb::val(value(QStringLiteral("txt_liv_tag")).toString()))).descr.trimmed().isEmpty())
                edit(Modifica);
            else
                messaggio(msg(QStringLiteral("non_mod_cfg")), true, false);
        }
        return true;
    }
    if (key == QLatin1String("F5")) {
        const int liv = int(vb::val(value(QStringLiteral("txt_liv_tag")).toString()));
        if (m_ope == Modifica) {
            messaggio(msg(QStringLiteral("ope_in_cor")), false, false);
            scrAttUte(liv);
            if (par_mac.config_sch_tag_att(liv).descr.trimmed() == QLatin1String("DEFAULT"))
                scrDefUte(liv);
            num_com_sch_tag = 1;
            sendConfigure(MODIFICA_OLD_CFG, liv, false, false);
            m_ope = Nessuna;
            leave();
            return true;
        }
        if (m_ope == Inserimento) {
            messaggio(msg(QStringLiteral("ope_in_cor")), false, false);
            scrAttUte(liv);
            if (par_mac.config_sch_tag_att(liv).descr.trimmed().isEmpty()) {
                scrDefUte(liv);
                num_com_sch_tag = 1;
                sendConfigure(INSERIMENTO_CFG_DEFAULT, liv, false, false);
                m_ope = Nessuna;
                leave();
                return true;
            }
            m_ope = Nessuna;
            return false;
        }
        messaggio(msg(QStringLiteral("non_sal_cfg")), true, false);
        return true;
    }
    if (key == QLatin1String("F6")) {
        if (!pro_set.ute_wrk) {
            messaggio(msg(QStringLiteral("acc_pwd")), true);
            return true;
        }
        return editing();
    }
    if (key == QLatin1String("F7")) {
        const int levels = int(par_mac.car_tec.num_liv_tag.val());
        if (!editing() && levels > 1) {
            Livello = Livello < levels ? Livello + 1 : 1;
            setText(QStringLiteral("txt_liv_tag"), QString::number(Livello));
            filAtt(Livello);
            filDef(Livello);
        }
        return true;
    }
    return editing();
}

namespace {
// TabIndex order of set_par_mac.
const QStringList PAR_MAC_TAB{QStringLiteral("cmb_num_liv_tag"), QStringLiteral("cmb_num_mot_liv_tag"), QStringLiteral("cmb_for_mac"),
    QStringLiteral("cmb_usc_liv(0)"), QStringLiteral("cmb_usc_liv(1)"), QStringLiteral("cmb_usc_liv(2)"),
    QStringLiteral("cmb_pre_con_pom(0)"), QStringLiteral("cmb_pre_con_pom(1)"), QStringLiteral("cmb_pre_con_pom(2)"),
    QStringLiteral("txt_min_for_mac"), QStringLiteral("txt_min_lun_tag"), QStringLiteral("txt_max_lun_tag"), QStringLiteral("txt_lun_tag_sca"),
    QStringLiteral("txt_pre_all"), QStringLiteral("cmb_gra_car"), QStringLiteral("cmb_nor_mac"), QStringLiteral("txt_sgl_abi_lin_dc_bus"),
    QStringLiteral("cmb_tip_azi"), QStringLiteral("chk_cut_to_mar"),
    QStringLiteral("chk_cut_to_pat(0)"), QStringLiteral("chk_cut_to_pat(1)"), QStringLiteral("chk_cut_to_pat(2)"),
    QStringLiteral("chk_sca_liv(0)"), QStringLiteral("chk_sca_liv(1)"), QStringLiteral("chk_sca_liv(2)"),
    QStringLiteral("chk_cut_to_pat_auto(0)"), QStringLiteral("chk_cut_to_pat_auto(1)"), QStringLiteral("chk_cut_to_pat_auto(2)"),
    QStringLiteral("txt_lun_tag_sca_cut_pat_auto")};
const QString LUN_SAN_RUL = QStringLiteral("470");
} // namespace

SetParMacModel::SetParMacModel(QSqlDatabase db, ClasseStato *state, QObject *parent)
    : FormModel(parent)
    , m_db(db)
    , m_state(state)
{
    for (const QString &control : PAR_MAC_TAB)
        set(control, QStringLiteral("modelFocus"), true);
}

void SetParMacModel::setCommands(Rmt *plc, Rmt *macro)
{
    m_plc = plc;
    m_macro = macro;
    for (Rmt *command : {plc, macro}) {
        connect(command, &Rmt::retok, this, [command](const QString &text) {
            if (!text.isEmpty())
                messaggio(text, false);
            command->resetta();
        });
        connect(command, &Rmt::retbad, this, [command](const QString &error) {
            if (!error.isEmpty())
                messaggio(error, true);
            command->resetta();
        });
    }
}

void SetParMacModel::combo(const QString &control, const QStringList &items, int index)
{
    set(control, QStringLiteral("items"), items);
    set(control, QStringLiteral("listIndex"), index);
}

QString SetParMacModel::comboText(const QString &control) const
{
    const QStringList items = value(control, QStringLiteral("items")).toStringList();
    const int index = listIndex(control);
    return index >= 0 && index < items.size() ? items.at(index) : QString();
}

// Fil_par_mac
void SetParMacModel::filParMac()
{
    const TipCarTec &car = par_mac.car_tec;
    const int levels = int(car.num_liv_tag.val());
    setText(QStringLiteral("txt_min_lun_tag"), car.min_lun_tag.trimmed());
    setText(QStringLiteral("txt_max_lun_tag"), car.max_lun_tag.trimmed());
    setText(QStringLiteral("txt_lun_tag_sca"), car.lun_tag_sca.trimmed());
    setText(QStringLiteral("txt_lun_tag_sca_cut_pat_auto"), car.lun_tag_sca_pat_aut.trimmed());
    setText(QStringLiteral("txt_min_for_mac"), car.min_for_mac.trimmed());
    setText(QStringLiteral("txt_sgl_abi_lin_dc_bus"), car.sgl_abi_lin_DC_Bus.trimmed());
    setText(QStringLiteral("txt_pre_all"), car.pre_all.trimmed());
    const QStringList oneToThree{QStringLiteral("1"), QStringLiteral("2"), QStringLiteral("3")};
    combo(QStringLiteral("cmb_num_liv_tag"), oneToThree, levels - 1);
    combo(QStringLiteral("cmb_num_mot_liv_tag"), oneToThree, int(car.num_mot_liv_tag.val()) - 1);
    combo(QStringLiteral("cmb_for_mac"), {QStringLiteral("1800"), QStringLiteral("2500"), QStringLiteral("2800")}, car.for_mac);
    combo(QStringLiteral("cmb_gra_car"), {QStringLiteral("1800"), QStringLiteral("2600")}, car.gra_car);
    combo(QStringLiteral("cmb_nor_mac"), {QStringLiteral("CE"), QStringLiteral("UL")}, car.nor_mac);
    combo(QStringLiteral("cmb_tip_azi"), {QStringLiteral("LENZE"), QStringLiteral("TDEMACNO")}, int(car.tip_azi_tag.val()));
    set(QStringLiteral("chk_cut_to_mar"), QStringLiteral("checked"), int(par_mac.opt.cut_mar.val()) != 0);
    for (int i = 1; i <= 3; ++i) {
        const bool used = i <= levels;
        const QString usc = at("cmb_usc_liv", i - 1);
        const QString pom = at("cmb_pre_con_pom", i - 1);
        combo(usc, used ? QStringList{msg(QStringLiteral("usc_san")), msg(QStringLiteral("usc_rul"))} : QStringList(), used ? int(car.sel_usc_liv(i).val()) : -1);
        combo(pom, used ? QStringList{msg(QStringLiteral("opt_no")), msg(QStringLiteral("opt_si"))} : QStringList(), used ? int(car.pre_con_pom(i).val()) : -1);
        set(usc, QStringLiteral("backColor"), used ? QStringLiteral("#ffffff") : StaBarModel::vbColor(LIGHT_GRAY_COL));
        set(pom, QStringLiteral("backColor"), used ? QStringLiteral("#ffffff") : StaBarModel::vbColor(LIGHT_GRAY_COL));
        set(at("chk_cut_to_pat", i - 1), QStringLiteral("checked"), used && int(par_mac.opt.cut_pat(i).val()) != 0);
        set(at("chk_sca_liv", i - 1), QStringLiteral("checked"), used && int(par_mac.opt.sca_liv(i).val()) != 0);
        set(at("chk_cut_to_pat_auto", i - 1), QStringLiteral("checked"), used && int(par_mac.opt.cut_pat_auto(i).val()) != 0);
    }
}

// disable_frames. VB shows the checkboxes grayed; here they are disabled.
void SetParMacModel::lock()
{
    for (const QString &control : PAR_MAC_TAB)
        set(control, QStringLiteral("enabled"), false);
    set(QStringLiteral("txt_idle"), QStringLiteral("focusCount"), ++m_focusCount);
}

// enable_frames: every combo and text box, the checkboxes of the levels fitted.
void SetParMacModel::unlock()
{
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    for (const QString &control : PAR_MAC_TAB) {
        bool on = true;
        if (control.startsWith(QLatin1String("chk_")) && control.endsWith(u')'))
            on = control.at(control.size() - 2).digitValue() < levels;
        set(control, QStringLiteral("enabled"), on);
    }
}

void SetParMacModel::activate(const QString &)
{
    m_mod = false;
    filParMac();
    lock();
    commit();
}

void SetParMacModel::annullaOpe()
{
    par_mac_ute = par_mac;
    par_abi_ute = par_abi;
    filParMac();
    lock();
    m_mod = false;
}

// ver_lun_tag
bool SetParMacModel::verLunTag()
{
    qint32 lunMin = cur_tag(1).Lun_tag(1);
    for (int i = 2; i <= int(par_mac.car_tec.num_liv_tag.val()); ++i)
        lunMin = qMin(lunMin, cur_tag(i).Lun_tag(1));
    if (vb::val(value(QStringLiteral("txt_min_lun_tag")).toString()) < lunMin) {
        messaggio(msg(QStringLiteral("min_lun_tag")) + u' ' + QString::number(lunMin), true, false);
        return false;
    }
    return true;
}

// scr_par_mac_ute
void SetParMacModel::scrParMacUte()
{
    TipCarTec &car = par_mac_ute.car_tec;
    const auto text = [this](const char *name) { return value(QLatin1String(name)).toString(); };
    car.num_liv_tag = QString::number(listIndex(QStringLiteral("cmb_num_liv_tag")) + 1);
    car.num_mot_liv_tag = QString::number(listIndex(QStringLiteral("cmb_num_mot_liv_tag")) + 1);
    car.for_mac = listIndex(QStringLiteral("cmb_for_mac"));
    car.gra_car = listIndex(QStringLiteral("cmb_gra_car"));
    car.nor_mac = listIndex(QStringLiteral("cmb_nor_mac"));
    car.min_for_mac = text("txt_min_for_mac");
    car.min_lun_tag = text("txt_min_lun_tag");
    car.max_lun_tag = text("txt_max_lun_tag");
    car.lun_tag_sca = text("txt_lun_tag_sca");
    car.lun_tag_sca_pat_aut = text("txt_lun_tag_sca_cut_pat_auto");
    car.pre_all = text("txt_pre_all");
    car.sgl_abi_lin_DC_Bus = text("txt_sgl_abi_lin_dc_bus");
    car.tip_azi_tag = QString::number(listIndex(QStringLiteral("cmb_tip_azi")));

    bool noPattern = true;
    for (int i = 0; i < 3; ++i)
        noPattern = noPattern && !checked(at("chk_cut_to_pat", i)) && !checked(at("chk_cut_to_pat_auto", i));
    par_mac_ute.opt.cut_mar = noPattern && checked(QStringLiteral("chk_cut_to_mar")) ? QStringLiteral("1") : QStringLiteral("0");
    set(QStringLiteral("chk_cut_to_mar"), QStringLiteral("checked"), int(par_mac_ute.opt.cut_mar.val()) != 0);
    for (int i = 1; i <= 3; ++i) {
        par_mac_ute.opt.cut_pat(i) = checked(at("chk_cut_to_pat", i - 1)) ? QStringLiteral("1") : QStringLiteral("0");
        par_mac_ute.opt.sca_liv(i) = checked(at("chk_sca_liv", i - 1)) ? QStringLiteral("1") : QStringLiteral("0");
        par_mac_ute.opt.cut_pat_auto(i) = checked(at("chk_cut_to_pat_auto", i - 1)) ? QStringLiteral("1") : QStringLiteral("0");
        const int usc = listIndex(at("cmb_usc_liv", i - 1));
        const int pom = listIndex(at("cmb_pre_con_pom", i - 1));
        car.sel_usc_liv(i) = usc == -1 ? QString() : QString::number(usc);
        car.pre_con_pom(i) = pom == -1 ? QString() : QString::number(pom);
        if (usc >= 0) {
            TipRulUscLiv &rul = par_mac_ute.rul_usc.rul_usc_liv(i);
            if (usc == 1)
                rul.lun_san = LUN_SAN_RUL;
            else if (rul.lun_san.val() == vb::val(LUN_SAN_RUL))
                rul.lun_san = getFileINIString(QStringLiteral("rul_usc_liv_%1").arg(i), QStringLiteral("lun_def_san"), QStringLiteral("1700"));
        }
    }
}

// eve_key_F3. The checks write into par_mac as they pass, so a later failure keeps the earlier values:
// annulla_ope then copies them to par_mac_ute. The check of nor_mac reads cmb_gra_car, as in VB.
void SetParMacModel::save()
{
    const auto refuse = [this](const QString &text) {
        messaggio(text, true, false);
        annullaOpe();
        commit();
    };
    const int levels = comboText(QStringLiteral("cmb_num_liv_tag")).toInt();
    if (levels < 1 || levels > 3)
        return refuse(msg(QStringLiteral("imp_cam_num_liv_tag")));
    par_mac.car_tec.num_liv_tag = QString::number(levels);
    const int forMac = listIndex(QStringLiteral("cmb_for_mac"));
    if (forMac + 1 < mm1800 || forMac + 1 > mm2800)
        return refuse(msg(QStringLiteral("imp_cam_for_mac")));
    par_mac.car_tec.for_mac = forMac;
    const int graCar = listIndex(QStringLiteral("cmb_gra_car"));
    if (graCar < gm2_1800 || graCar > gm2_2600)
        return refuse(msg(QStringLiteral("imp_cam_gra_car")));
    par_mac.car_tec.gra_car = graCar;
    if (graCar < 0 || graCar > 1)
        return refuse(msg(QStringLiteral("imp_cam_tip_mac")));
    par_mac.car_tec.nor_mac = listIndex(QStringLiteral("cmb_nor_mac"));
    const QString dcBus = value(QStringLiteral("txt_sgl_abi_lin_dc_bus")).toString();
    if (vb::val(dcBus) < LIM_INF_SGL_ABI_LIN_DC_BUS || vb::val(dcBus) > LIM_SUP_SGL_ABI_LIN_DC_BUS) {
        set(QStringLiteral("txt_sgl_abi_lin_dc_bus"), QStringLiteral("foreColor"), StaBarModel::vbColor(StaBarModel::vbRed));
        return refuse(msg(QStringLiteral("par_err")) + QStringLiteral(". (%1-%2 Volt)").arg(LIM_INF_SGL_ABI_LIN_DC_BUS).arg(LIM_SUP_SGL_ABI_LIN_DC_BUS));
    }
    set(QStringLiteral("txt_sgl_abi_lin_dc_bus"), QStringLiteral("foreColor"), QStringLiteral("#000000"));
    par_mac.car_tec.sgl_abi_lin_DC_Bus = dcBus;
    const QString minFor = value(QStringLiteral("txt_min_for_mac")).toString();
    if (vb::val(minFor) <= 0 || vb::val(minFor) > vb::val(comboText(QStringLiteral("cmb_for_mac"))))
        return refuse(msg(QStringLiteral("err_min_for_mac")));
    par_mac_ute.car_tec.min_for_mac = minFor;
    if (value(QStringLiteral("txt_min_lun_tag")).toString() != par_mac.car_tec.min_lun_tag.trimmed() && !verLunTag()) {
        annullaOpe();
        commit();
        return;
    }
    messaggio(msg(QStringLiteral("ope_in_cor")), false, false);
    scrParMacUte();
    if (par_mac_ute.car_tec.num_mot_liv_tag.str() != par_mac.car_tec.num_mot_liv_tag.str()) {
        // VB has "If Not dbf_vuo("Par_cfg") And False": the branch through the cut boards never runs.
        if (m_plc && m_plc->esegui(PARAM_CUR_TAG, 0, 0, true)) {
            par_mac.car_tec.num_mot_liv_tag = par_mac_ute.car_tec.num_mot_liv_tag;
        } else {
            annullaOpe();
            commit();
            return;
        }
    }
    TipCarTec &car = par_mac.car_tec;
    const TipCarTec &user = par_mac_ute.car_tec;
    car.lun_tag_sca = user.lun_tag_sca;
    car.lun_tag_sca_pat_aut = user.lun_tag_sca_pat_aut;
    car.max_lun_tag = user.max_lun_tag;
    car.min_lun_tag = user.min_lun_tag;
    car.pre_all = user.pre_all;
    car.min_for_mac = user.min_for_mac;
    car.gra_car = user.gra_car;
    car.nor_mac = user.nor_mac;
    car.sgl_abi_lin_DC_Bus = user.sgl_abi_lin_DC_Bus;
    for (int i = 1; i <= 3; ++i) {
        car.sel_usc_liv(i) = user.sel_usc_liv(i);
        car.pre_con_pom(i) = user.pre_con_pom(i);
    }
    par_mac.opt = par_mac_ute.opt;
    par_mac.rul_usc = par_mac_ute.rul_usc;
    car.tip_azi_tag = user.tip_azi_tag;
    leggiCfg(m_db, iniFile());
    leggiParCurTag(m_db);
    scrParMac();
    if (!m_macro || !m_macro->esegui(PLC_PARAM_MAC_PARAM_CFG_MAC, 0, 0))
        messaggio(msg(QStringLiteral("ope_eff_par")), true);
    m_mod = false;
    lock();
    commit();
}

void SetParMacModel::controlEvent(const QString &name, int index, const QString &event, const QVariantMap &args)
{
    const QString control = index >= 0 ? QStringLiteral("%1(%2)").arg(name).arg(index) : name;
    if (event == QLatin1String("Change") && control.startsWith(QLatin1String("txt_"))) {
        setText(control, args.value(QStringLiteral("text")).toString());
    } else if (event == QLatin1String("Click")) {
        if (control.startsWith(QLatin1String("chk_")))
            set(control, QStringLiteral("checked"), args.value(QStringLiteral("checked")));
        else if (control.startsWith(QLatin1String("cmb_")))
            set(control, QStringLiteral("listIndex"), args.value(QStringLiteral("index")));
    } else if (event == QLatin1String("KeyDown")) {
        const int key = args.value(QStringLiteral("key")).toInt();
        int step = 0;
        if (key == Qt::Key_Return || key == Qt::Key_Enter || key == Qt::Key_Down || key == Qt::Key_Tab)
            step = 1;
        else if (key == Qt::Key_Up || key == Qt::Key_Backtab)
            step = -1;
        const int position = int(PAR_MAC_TAB.indexOf(control));
        if (step == 0 || position < 0)
            return;
        const int count = int(PAR_MAC_TAB.size());
        for (int i = 1; i <= count; ++i) {
            const QString &next = PAR_MAC_TAB.at((position + step * i + count * 2) % count);
            if (value(next, QStringLiteral("enabled")).toBool()) {
                set(next, QStringLiteral("focusCount"), ++m_focusCount);
                commit();
                return;
            }
        }
    }
}

bool SetParMacModel::functionKey(const QString &key, int shift)
{
    if (key == QLatin1String("ESC")) {
        if (shift == 0 && m_mod) {
            m_mod = false;
            par_mac_ute = par_mac;
            filParMac();
            lock();
            commit();
        }
        return true;
    }
    if (key == QLatin1String("F2")) {
        if (shift != 0)
            return true;
        if (!pro_set.ute_wrk) {
            messaggio(msg(QStringLiteral("acc_pwd")), true);
        } else if (!m_mod) {
            if (!m_state->comOK()) {
                messaggio(msg(QStringLiteral("com_int_con_mac")), true);
            } else if (!m_state->bit(ClasseStato::MAC_F_STOP, 1)) {
                messaggio(msg(QStringLiteral("mac_non_fas_sto")), true, false);
            } else {
                m_mod = true;
                unlock();
                set(QStringLiteral("cmb_num_liv_tag"), QStringLiteral("focusCount"), ++m_focusCount);
                commit();
            }
        }
        return true;
    }
    if (key == QLatin1String("F3")) {
        if (shift == 0 && m_mod)
            save();
        return true;
    }
    if (key == QLatin1String("F8"))
        return true;
    return m_mod || shift != 0;
}

SalCfgModel::SalCfgModel(QSqlDatabase db, PlcLink *link, QObject *parent)
    : FormModel(parent)
    , m_db(db)
    , m_link(link)
{
}

void SalCfgModel::setCommand(Rmt *command)
{
    m_command = command;
    connect(command, &Rmt::retok, this, [this](const QString &text) {
        if (!text.isEmpty())
            messaggio(text, true);
        m_command->resetta();
    });
    connect(command, &Rmt::retbad, this, [this](const QString &error) {
        if (!error.isEmpty())
            messaggio(error, true);
        m_command->resetta();
    });
}

void SalCfgModel::activate(const QString &)
{
    setText(QStringLiteral("txt_des"), QString());
    set(QStringLiteral("txt_des"), QStringLiteral("focusCount"), ++m_focusCount);
    commit();
}

void SalCfgModel::controlEvent(const QString &control, int, const QString &event, const QVariantMap &args)
{
    if (event == QLatin1String("Change") && control == QLatin1String("txt_des"))
        setText(control, args.value(QStringLiteral("text")).toString());
}

bool SalCfgModel::functionKey(const QString &key, int shift)
{
    if (shift != 0)
        return true;
    if (key == QLatin1String("ESC")) {
        par_mac_ute.par_cfg_att(Livello) = par_mac.par_cfg_att(Livello);
        return false;
    }
    if (key != QLatin1String("F11"))
        return false;
    const QString description = value(QStringLiteral("txt_des")).toString();
    if (description.isEmpty()) {
        messaggio(msg(QStringLiteral("imm_des")), true, false);
        return true;
    }
    if (troCfg(m_db, description.trimmed(), Livello)) {
        par_mac_ute.par_cfg_att(Livello) = par_mac.par_cfg_att(Livello);
        messaggio(msg(QStringLiteral("cfg_pre")), true, false);
    } else {
        num_com_sch_tag = 1;
        m_link->boards().setSendCase(INSERIMENTO_CFG_ATTIVA);
        par_mac_ute.config_sch_tag_att(Livello).descr = description.trimmed();
        if (m_command->esegui(CONFIGURA, Livello, 0, false))
            messaggio(msg(QStringLiteral("inv_par_sch_tag")), false);
        else
            messaggio(msg(QStringLiteral("imp_ese_ope")), true);
    }
    return false;
}

DelCfgModel::DelCfgModel(QSqlDatabase db, QObject *parent)
    : FormModel(parent)
    , m_db(db)
{
}

void DelCfgModel::aggLab()
{
    const QVariantMap &row = m_records.at(m_pos);
    QString levels;
    for (int i = 1; i <= int(par_mac.car_tec.num_liv_tag.val()); ++i) {
        if (fieldInt(row, QStringLiteral("LIVELLO%1").arg(i)) == 1)
            levels += QString::number(i) + u' ';
    }
    const QString red = StaBarModel::vbColor(StaBarModel::vbRed);
    set(QStringLiteral("lab_att"), QStringLiteral("visible"), true);
    setText(QStringLiteral("lab_cfg"), field(row, QStringLiteral("DESCR")));
    const bool active = !levels.isEmpty();
    setText(QStringLiteral("lab_att"), msg(active ? QStringLiteral("cfg_att") : QStringLiteral("cfg_non_att")));
    set(QStringLiteral("lab_att"), QStringLiteral("foreColor"), active ? red : QStringLiteral("#000000"));
    set(QStringLiteral("lab_liv"), QStringLiteral("visible"), active);
    set(QStringLiteral("l_liv"), QStringLiteral("visible"), active);
    if (active) {
        setText(QStringLiteral("lab_liv"), levels);
        set(QStringLiteral("lab_liv"), QStringLiteral("foreColor"), red);
        set(QStringLiteral("l_liv"), QStringLiteral("foreColor"), red);
    }
}

void DelCfgModel::activate(const QString &)
{
    m_empty = false;
    set(QStringLiteral("lab_liv"), QStringLiteral("visible"), false);
    set(QStringLiteral("l_liv"), QStringLiteral("visible"), false);
    set(QStringLiteral("lab_att"), QStringLiteral("visible"), false);
    m_records = readRecords(m_db, QStringLiteral("trim(DESCR), CAST(FOR_MAC AS INTEGER), CAST(GRA_CAR AS INTEGER), CAST(N_MOT_L AS INTEGER), CAST(N_MOT_AB AS INTEGER), rec_no"));
    m_pos = 0;
    if (m_records.isEmpty()) {
        setText(QStringLiteral("lab_cfg"), msg(QStringLiteral("non_cfg_eli")));
        m_empty = true;
    } else {
        const QString descr = par_mac.config_sch_tag_att(Livello).descr.trimmed();
        const int forMac = forMacMm(par_mac.car_tec.for_mac);
        const int graCar = getGraCar(par_mac.car_tec.gra_car);
        const int numMotLiv = int(par_mac.car_tec.num_mot_liv_tag.val());
        const int numMotAbi = par_abi.num_mot_abi(Livello);
        for (; m_pos < m_records.size(); ++m_pos) {
            const QVariantMap &row = m_records.at(m_pos);
            if (field(row, QStringLiteral("DESCR")) == descr && fieldInt(row, QStringLiteral("FOR_MAC")) == forMac && fieldInt(row, QStringLiteral("GRA_CAR")) == graCar
                && fieldInt(row, QStringLiteral("N_MOT_L")) == numMotLiv && fieldInt(row, QStringLiteral("N_MOT_AB")) == numMotAbi)
                break;
        }
        if (m_pos < m_records.size())
            aggLab();
    }
    commit();
}

void DelCfgModel::keyUp(int key)
{
    if (m_records.isEmpty())
        return;
    if (key == Qt::Key_Right) {
        if (m_pos < m_records.size() - 1)
            ++m_pos;
        else if (m_pos >= m_records.size())
            return;
    } else if (key == Qt::Key_Left) {
        if (m_pos > 0)
            --m_pos;
    } else {
        return;
    }
    aggLab();
    commit();
}

bool DelCfgModel::functionKey(const QString &key, int shift)
{
    if (shift != 0)
        return true;
    if (key != QLatin1String("F11"))
        return false;
    if (m_empty)
        return true;
    // With no record shown VB stops on an error reading rs at EOF.
    if (m_pos >= m_records.size())
        return false;
    const QString shown = value(QStringLiteral("lab_cfg")).toString().trimmed();
    if (shown == QLatin1String("DEFAULT")) {
        messaggio(msg(QStringLiteral("del_cfg_def")), true, false);
        return false;
    }
    const QVariantMap &row = m_records.at(m_pos);
    if (cancellaCfg(m_db, shown, fieldInt(row, QStringLiteral("FOR_MAC")), fieldInt(row, QStringLiteral("GRA_CAR")), fieldInt(row, QStringLiteral("N_MOT_L")), fieldInt(row, QStringLiteral("N_MOT_AB"))))
        messaggio(msg(QStringLiteral("ope_eff")), false, false);
    else
        messaggio(msg(QStringLiteral("del_cfg_att")), true, false);
    return false;
}

LegCfgModel::LegCfgModel(QSqlDatabase db, PlcLink *link, QObject *parent)
    : FormModel(parent)
    , m_db(db)
    , m_link(link)
{
}

void LegCfgModel::setCommand(Rmt *command)
{
    m_command = command;
    connect(command, &Rmt::retok, this, [this](const QString &text) {
        if (!text.isEmpty())
            messaggio(text, false);
        m_command->resetta();
    });
    connect(command, &Rmt::retbad, this, [this](const QString &error) {
        if (!error.isEmpty())
            messaggio(error, true);
        m_command->resetta();
    });
}

namespace {
struct LegLabel {
    const char *label;
    const char *column;
};
constexpr LegLabel LEG_LABELS[] = {
    {"lab_vel_mas_tag", "VEL_MAX"}, {"lab_tem_acc_tag_lun", "TEM_ATL"}, {"lab_tem_dec_tag_lun", "TEM_DTL"}, {"lab_tem_acc_tag_cor", "TEM_ATC"},
    {"lab_tem_dec_tag_cor", "TEM_DTC"}, {"lab_kp_nor", "KP_NOR"}, {"lab_ki_nor", "KI_NOR"}, {"lab_fil_kp", "FIL_KP"},
    {"lab_kv_nor", "KV_NOR"}, {"lab_lim_lav_kv", "LIM_KV"}, {"lab_max_gir", "MAX_GIR"},
};
constexpr quint32 LIGHT_RED_COL = 0x9895FF;
} // namespace

void LegCfgModel::iniLab()
{
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    setText(at("l_liv", 0), QString::number(Livello));
    setText(at("l_liv", 1), QString::number(Livello));
    for (int i = 1; i <= 3; ++i) {
        const QString color = i <= levels ? QStringLiteral("#ffffff") : StaBarModel::vbColor(LIGHT_GRAY_COL);
        for (const LegLabel &row : LEG_LABELS) {
            setText(at(row.label, i - 1), QString());
            set(at(row.label, i - 1), QStringLiteral("backColor"), color);
        }
        set(at("lab_liv", i - 1), QStringLiteral("backColor"), color);
    }
}

void LegCfgModel::iniDbf()
{
    m_forMac = QString::number(forMacMm(par_mac.car_tec.for_mac));
    m_graCar = QString::number(getGraCar(par_mac.car_tec.gra_car));
    m_numMotLiv = QString::number(int(par_mac.car_tec.num_mot_liv_tag.val()));
    m_numMotAbi = QString::number(par_abi.num_mot_abi(Livello));
    setText(QStringLiteral("lab_cfg"), QString());
    setText(QStringLiteral("l_for_mac"), m_forMac);
    setText(QStringLiteral("l_gra_car"), m_graCar);
    setText(QStringLiteral("l_num_mot_liv"), m_numMotLiv);
    setText(QStringLiteral("l_num_mot_abi"), m_numMotAbi);
    m_empty = false;
    m_records = readRecords(m_db, QStringLiteral("CAST(FOR_MAC AS INTEGER), CAST(GRA_CAR AS INTEGER), CAST(N_MOT_L AS INTEGER), CAST(N_MOT_AB AS INTEGER), trim(DESCR), rec_no"));
    m_pos = 0;
    if (m_records.isEmpty()) {
        setText(QStringLiteral("lab_cfg"), msg(QStringLiteral("non_cfg_leg")));
        m_empty = true;
        return;
    }
    const QString active = par_mac.config_sch_tag_att(Livello).descr.trimmed();
    const auto sameMotors = [&](const QVariantMap &row) { return field(row, QStringLiteral("N_MOT_L")) == m_numMotLiv && field(row, QStringLiteral("N_MOT_AB")) == m_numMotAbi; };
    for (; m_pos < m_records.size(); ++m_pos) {
        const QVariantMap &row = m_records.at(m_pos);
        if (active.isEmpty()) {
            if (field(row, QStringLiteral("DESCR")) == QLatin1String("DEFAULT") && field(row, QStringLiteral("FOR_MAC")) == m_forMac && field(row, QStringLiteral("GRA_CAR")) == m_graCar && sameMotors(row))
                break;
        } else if (field(row, QStringLiteral("DESCR")) == active && field(row, QStringLiteral("LIVELLO%1").arg(Livello)) == QLatin1String("1") && sameMotors(row)) {
            break;
        }
    }
    if (m_pos >= m_records.size())
        m_pos = 0;
    aggLab();
}

void LegCfgModel::aggLab()
{
    const QVariantMap &row = m_records.at(m_pos);
    setText(QStringLiteral("lab_cfg"), field(row, QStringLiteral("DESCR")));
    setText(QStringLiteral("lab_for_mac"), field(row, QStringLiteral("FOR_MAC")));
    setText(QStringLiteral("lab_gra_car"), field(row, QStringLiteral("GRA_CAR")));
    setText(QStringLiteral("lab_num_mot_liv"), field(row, QStringLiteral("N_MOT_L")));
    setText(QStringLiteral("lab_num_mot_abi"), field(row, QStringLiteral("N_MOT_AB")));
    for (int i = 1; i <= int(par_mac.car_tec.num_liv_tag.val()); ++i) {
        const bool active = fieldInt(row, QStringLiteral("LIVELLO%1").arg(i)) == 1;
        setText(at("lab_att", i - 1), msg(active ? QStringLiteral("cfg_attiva") : QStringLiteral("cfg_non_attiva")));
        set(at("lab_att", i - 1), QStringLiteral("foreColor"), active ? StaBarModel::vbColor(StaBarModel::vbRed) : QStringLiteral("#000000"));
        const QString color = active ? StaBarModel::vbColor(LIGHT_RED_COL) : QStringLiteral("#ffffff");
        for (const LegLabel &label : LEG_LABELS) {
            set(at(label.label, i - 1), QStringLiteral("backColor"), color);
            setText(at(label.label, i - 1), field(row, QString::fromLatin1(label.column) + QString::number(i)));
        }
    }
}

void LegCfgModel::activate(const QString &)
{
    iniLab();
    iniDbf();
    commit();
}

// AbsolutePosition > 2: VB does not let the arrow go back to the first two records.
void LegCfgModel::keyUp(int key)
{
    if (m_records.isEmpty())
        return;
    if (key == Qt::Key_Right && m_pos < m_records.size() - 1)
        ++m_pos;
    else if (key == Qt::Key_Left && m_pos > 2)
        --m_pos;
    else
        return;
    aggLab();
    commit();
}

void LegCfgModel::scrParUte(int liv)
{
    TipParCfgSchTagLiv &cfg = par_mac_ute.par_cfg_att(liv);
    const auto label = [&](const char *name) { return value(at(name, liv - 1)).toString().trimmed(); };
    par_mac_ute.config_sch_tag_att(liv).descr = value(QStringLiteral("lab_cfg")).toString().trimmed();
    par_mac_ute.config_sch_tag_att(liv).num_mot_liv = qint16(vb::val(value(QStringLiteral("lab_num_mot_liv")).toString()));
    cfg.vel_max_tag = label("lab_vel_mas_tag");
    cfg.tem_acc_tgl_cor = label("lab_tem_acc_tag_cor");
    cfg.tem_acc_tgl_lun = label("lab_tem_acc_tag_lun");
    cfg.tem_dec_tgl_cor = label("lab_tem_dec_tag_cor");
    cfg.tem_dec_tgl_lun = label("lab_tem_dec_tag_lun");
    cfg.Ki_nor = label("lab_ki_nor");
    cfg.Kp_nor = label("lab_kp_nor");
    cfg.fil_kp = label("lab_fil_kp");
    cfg.Kv_nor = label("lab_kv_nor");
    cfg.lim_lav_Kv = label("lab_lim_lav_kv");
    cfg.gir_max_mot_lam = label("lab_max_gir");
}

bool LegCfgModel::functionKey(const QString &key, int shift)
{
    if (shift != 0)
        return true;
    if (key != QLatin1String("F11"))
        return false;
    if (m_empty)
        return true;
    if (value(QStringLiteral("lab_for_mac")).toString() != m_forMac || value(QStringLiteral("lab_gra_car")).toString() != m_graCar
        || value(QStringLiteral("lab_num_mot_liv")).toString() != m_numMotLiv || value(QStringLiteral("lab_num_mot_abi")).toString() != m_numMotAbi) {
        messaggio(msg(QStringLiteral("imp_car_cfg")), true, false);
        return false;
    }
    const int liv = int(vb::val(value(at("l_liv", 0)).toString()));
    if (par_mac.config_sch_tag_att(liv).descr.trimmed() != value(QStringLiteral("lab_cfg")).toString().trimmed()) {
        messaggio(msg(QStringLiteral("ope_in_cor")), false, false);
        scrParUte(liv);
        num_com_sch_tag = 1;
        m_link->boards().setSendCase(CARICA_NEW_CFG);
        if (m_command->esegui(CONFIGURA, liv, 0, false))
            messaggio(msg(QStringLiteral("inv_par_sch_tag")), false);
        else
            messaggio(msg(QStringLiteral("imp_ese_ope")), true);
    }
    return false;
}
