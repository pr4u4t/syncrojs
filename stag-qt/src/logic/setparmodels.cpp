#include "setparmodels.h"

#include "classestato.h"
#include "inibas.h"
#include "messages.h"
#include "parmac.h"
#include "stabarmodel.h"
#include "staglog.h"
#include "uihooks.h"
#include "vbcompat.h"
#include "vbtypes.h"

using namespace stag;

namespace {

constexpr quint32 LIGHT_GRAY_COL = 0xE0E0E0;
constexpr int MAX_TIM_ANT_CHI_FLAP = 500;

QString at(const char *name, int index)
{
    return QStringLiteral("%1(%2)").arg(QLatin1String(name)).arg(index);
}

QString gray()
{
    return StaBarModel::vbColor(LIGHT_GRAY_COL);
}

} // namespace

void SetParFormModel::focus(const QString &control)
{
    set(control, QStringLiteral("focusCount"), ++m_focusCount);
}

void SetParFormModel::controlEvent(const QString &name, int index, const QString &event, const QVariantMap &args)
{
    const QString control = index >= 0 ? QStringLiteral("%1(%2)").arg(name).arg(index) : name;
    if (event == QLatin1String("Change") && control.startsWith(QLatin1String("txt_"))) {
        setText(control, args.value(QStringLiteral("text")).toString());
    } else if (event == QLatin1String("KeyDown")) {
        const int key = args.value(QStringLiteral("key")).toInt();
        int step = 0;
        if (key == Qt::Key_Return || key == Qt::Key_Enter || key == Qt::Key_Down || key == Qt::Key_Tab)
            step = 1;
        else if (key == Qt::Key_Up || key == Qt::Key_Backtab)
            step = -1;
        const int position = int(m_tab.indexOf(control));
        if (step == 0 || position < 0)
            return;
        const int count = int(m_tab.size());
        for (int i = 1; i <= count; ++i) {
            const QString &next = m_tab.at((position + step * i + count * 2) % count);
            const QVariant enabled = value(next, QStringLiteral("enabled"));
            if (!enabled.isValid() || enabled.toBool()) {
                focus(next);
                commit();
                return;
            }
        }
    }
}

SetParRulModel::SetParRulModel(ClasseStato *state, QObject *parent)
    : SetParFormModel(parent)
    , m_state(state)
{
    m_tab << QStringLiteral("txt_vel_max_lin");
    for (int i = 0; i < 2; ++i) {
        for (const char *name : {"txt_off_vel_met_min", "txt_off_vel_per", "txt_sov_sfi", "txt_dis_rot_mas"})
            m_tab << at(name, i);
    }
    for (int i = 0; i < 3; ++i) {
        for (const char *name : {"txt_lun_san", "txt_gap_tgl_cor", "txt_gap_tgl_lun", "txt_tem_ape_fla", "txt_tem_chi_fla", "txt_dis_lam_fla"})
            m_tab << at(name, i);
    }
    for (const QString &control : std::as_const(m_tab))
        set(control, QStringLiteral("modelFocus"), true);
}

void SetParRulModel::setCommand(Rmt *command)
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

// fil_par_rul_ing and fil_par_rul_usc_liv
void SetParRulModel::filParRul()
{
    setText(QStringLiteral("txt_vel_max_lin"), par_mac_ute.rul_ing.vel_max_lin.trimmed());
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    for (int i = 0; i <= (levels == 3 ? 1 : 0); ++i) {
        const TipRulNum &rul = par_mac_ute.rul_ing.rul_num(i + 1);
        setText(at("txt_dis_rot_mas", i), rul.dis_rot_mas.trimmed());
        setText(at("txt_off_vel_met_min", i), rul.off_vel_met_min.trimmed());
        setText(at("txt_off_vel_per", i), rul.off_vel_per.trimmed());
        setText(at("txt_sov_sfi", i), rul.sov_sfi.trimmed());
    }
    for (int liv = 1; liv <= levels; ++liv) {
        const TipRulUscLiv &rul = par_mac_ute.rul_usc.rul_usc_liv(liv);
        setText(at("txt_dis_lam_fla", liv - 1), rul.dis_lam_fla.trimmed());
        setText(at("txt_gap_tgl_cor", liv - 1), rul.gap_tgl_cor.trimmed());
        setText(at("txt_gap_tgl_lun", liv - 1), rul.gap_tgl_lun.trimmed());
        setText(at("txt_lun_san", liv - 1), rul.lun_san.trimmed());
        setText(at("txt_tem_ape_fla", liv - 1), rul.tem_ape_fla.trimmed());
        setText(at("txt_tem_chi_fla", liv - 1), rul.tem_chi_fla.trimmed());
    }
}

// ini_txt_lab: what the machine does not have is grayed and empty.
void SetParRulModel::iniTxtLab()
{
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    const int rollers = levels == 3 ? 2 : 1;
    const QString white = QStringLiteral("#ffffff");
    for (int i = 1; i <= 2; ++i) {
        const bool used = i <= rollers;
        for (const char *name : {"txt_off_vel_met_min", "txt_off_vel_per", "txt_sov_sfi", "txt_dis_rot_mas"}) {
            set(at(name, i - 1), QStringLiteral("backColor"), used ? white : gray());
            if (!used)
                setText(at(name, i - 1), QString());
        }
        set(at("lab_tra_ing", i - 1), QStringLiteral("backColor"), used ? white : gray());
        for (const char *name : {"lab_tra_rul", "lab_tra_ing", "lab_rul"})
            set(at(name, i - 1), QStringLiteral("enabled"), used);
    }
    for (int i = 1; i <= 3; ++i) {
        const bool used = i <= levels;
        for (const char *name : {"txt_lun_san", "txt_gap_tgl_cor", "txt_gap_tgl_lun", "txt_tem_ape_fla", "txt_tem_chi_fla", "txt_dis_lam_fla"}) {
            set(at(name, i - 1), QStringLiteral("backColor"), used ? white : gray());
            if (!used)
                setText(at(name, i - 1), QString());
        }
        set(at("lab_tra_usc", i - 1), QStringLiteral("backColor"), used ? white : gray());
        for (const char *name : {"lab_liv", "lab_tra_usc", "lab_tra_liv"})
            set(at(name, i - 1), QStringLiteral("enabled"), used);
    }
}

void SetParRulModel::lock()
{
    for (const QString &control : std::as_const(m_tab))
        set(control, QStringLiteral("enabled"), false);
    focus(QStringLiteral("txt_idle"));
}

void SetParRulModel::activate(const QString &)
{
    m_mod = false;
    filParRul();
    for (int i = 0; i < 3; ++i)
        setText(at("lab_tra_usc", i), QString());
    for (int i = 0; i < 2; ++i)
        setText(at("lab_tra_ing", i), QString());
    iniTxtLab();
    lock();
    commit();
}

// chk_txt_tem_fla
QString SetParRulModel::clampFlap(const QString &control)
{
    const QString text = value(control).toString();
    if (vb::val(text) < 0)
        setText(control, QStringLiteral("0"));
    else if (vb::val(text) > MAX_TIM_ANT_CHI_FLAP)
        setText(control, QString::number(MAX_TIM_ANT_CHI_FLAP));
    return value(control).toString();
}

void SetParRulModel::scrParUte()
{
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    par_mac_ute.rul_ing.vel_max_lin = value(QStringLiteral("txt_vel_max_lin")).toString();
    for (int i = 0; i <= (levels == 3 ? 1 : 0); ++i) {
        TipRulNum &rul = par_mac_ute.rul_ing.rul_num(i + 1);
        rul.dis_rot_mas = value(at("txt_dis_rot_mas", i)).toString();
        rul.off_vel_met_min = value(at("txt_off_vel_met_min", i)).toString();
        rul.off_vel_per = value(at("txt_off_vel_per", i)).toString();
        rul.sov_sfi = value(at("txt_sov_sfi", i)).toString();
    }
    for (int liv = 1; liv <= levels; ++liv) {
        TipRulUscLiv &rul = par_mac_ute.rul_usc.rul_usc_liv(liv);
        rul.dis_lam_fla = value(at("txt_dis_lam_fla", liv - 1)).toString();
        rul.gap_tgl_cor = value(at("txt_gap_tgl_cor", liv - 1)).toString();
        rul.gap_tgl_lun = value(at("txt_gap_tgl_lun", liv - 1)).toString();
        rul.lun_san = value(at("txt_lun_san", liv - 1)).toString();
        rul.tem_ape_fla = clampFlap(at("txt_tem_ape_fla", liv - 1));
        rul.tem_chi_fla = clampFlap(at("txt_tem_chi_fla", liv - 1));
    }
}

bool SetParRulModel::sendOne(bool entry, int index)
{
    const bool sent = m_sender ? m_sender(entry, index) : false;
    if (!m_sender)
        log_str(QStringLiteral("set_par_rul: comunicazione CAN non disponibile"), MED_PRIORITY, CosLog::Can);
    const QString label = at(entry ? "lab_tra_ing" : "lab_tra_usc", index - 1);
    set(label, QStringLiteral("foreColor"), StaBarModel::vbColor(sent ? StaBarModel::COL_DARK_Green : StaBarModel::COL_ALL_Red));
    setText(label, sent ? QStringLiteral("OK") : QStringLiteral("Error"));
    return sent;
}

// snd_par_azi
bool SetParRulModel::sndParAzi()
{
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    bool ok = sendOne(true, 1);
    if (levels == 3)
        ok = sendOne(true, 2) && ok;
    for (int liv = 1; liv <= levels; ++liv)
        ok = sendOne(false, liv) && ok;
    return ok;
}

bool SetParRulModel::functionKey(const QString &key, int shift)
{
    if (key == QLatin1String("ESC")) {
        if (shift == 0 && m_mod) {
            m_mod = false;
            par_mac_ute = par_mac;
            filParRul();
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
            if (m_state->comOK()) {
                m_mod = true;
                const int levels = int(par_mac.car_tec.num_liv_tag.val());
                for (const QString &control : std::as_const(m_tab)) {
                    const int digit = control.endsWith(u')') ? control.at(control.size() - 2).digitValue() : 0;
                    const bool entry = control.startsWith(QLatin1String("txt_off")) || control.startsWith(QLatin1String("txt_sov")) || control.startsWith(QLatin1String("txt_dis_rot"));
                    set(control, QStringLiteral("enabled"), entry ? digit < (levels == 3 ? 2 : 1) : digit < levels);
                }
                focus(QStringLiteral("txt_vel_max_lin"));
                commit();
            } else {
                messaggio(msg(QStringLiteral("com_int_con_mac")), true);
            }
        }
        return true;
    }
    if (key == QLatin1String("F3")) {
        if (shift == 0 && m_mod) {
            messaggio(msg(QStringLiteral("ope_in_cor")), false, false);
            scrParUte();
            if (!sndParAzi()) {
                par_mac_ute.rul_ing = par_mac.rul_ing;
                par_mac_ute.rul_usc = par_mac.rul_usc;
                filParRul();
                messaggio(msg(QStringLiteral("ope_eff_par")), true, false);
            } else {
                par_mac.rul_ing = par_mac_ute.rul_ing;
                par_mac.rul_usc = par_mac_ute.rul_usc;
                if (!m_command || !m_command->esegui(PARAM_MAC, 0, 0))
                    messaggio(msg(QStringLiteral("imp_ese_ope")), true);
                scrParMac();
                messaggio(msg(QStringLiteral("ope_eff")), false, false);
            }
            lock();
            m_mod = false;
            commit();
        }
        return true;
    }
    return m_mod || shift != 0;
}

SetParComModel::SetParComModel(PlcLink *link, ClasseStato *state, QObject *parent)
    : SetParFormModel(parent)
    , m_link(link)
    , m_state(state)
{
    m_tab = QStringList{QStringLiteral("cmb_ver_spde"), QStringLiteral("cmb_tip_com"), QStringLiteral("txt_err_max"), QStringLiteral("txt_timeout"),
                        QStringLiteral("txt_porta_spde"), QStringLiteral("cmb_baud_spde"), QStringLiteral("cmb_bit_par_spde"), QStringLiteral("cmb_dati_spde"),
                        QStringLiteral("cmb_bit_stop_spde"), QStringLiteral("txt_por_loc"), QStringLiteral("txt_por_rem"), QStringLiteral("txt_ind_rem"),
                        QStringLiteral("txt_net_plc"), QStringLiteral("txt_node_plc"), QStringLiteral("txt_unit_plc"), QStringLiteral("txt_timer_scans_plc"),
                        QStringLiteral("txt_ind_plc"), QStringLiteral("txt_por_plc"), QStringLiteral("txt_timeout_lan"), QStringLiteral("chk_abi_lan"),
                        QStringLiteral("txt_timeout_azi"), QStringLiteral("cmb_baud_azi")};
    for (int i = 0; i < 3; ++i) {
        for (const char *name : {"cmb_tip_com_tag", "txt_por_loc_tag", "txt_por_rem_tag", "txt_ind_rem_tag", "txt_porta_sch_tag", "cmb_baud_sch_tag",
                                 "cmb_bit_par_sch_tag", "cmb_dati_sch_tag", "cmb_bit_stop_sch_tag", "txt_timeout_sch_tag"})
            m_tab << at(name, i);
    }
    for (int i = 0; i < 3; ++i)
        m_tab << at("chk_tip_com_tag_udp", i);
    for (const QString &control : std::as_const(m_tab))
        set(control, QStringLiteral("modelFocus"), true);
    iniCombo();
}

void SetParComModel::iniCombo()
{
    const auto items = [this](const QString &control, const QStringList &list) { set(control, QStringLiteral("items"), list); };
    const QStringList tipCom{msg(QStringLiteral("com_ser")), QStringLiteral("Ethernet")};
    const QStringList parity{QStringLiteral("E"), QStringLiteral("M"), QStringLiteral("N"), QStringLiteral("None"), QStringLiteral("O"), QStringLiteral("S")};
    const QStringList stop{QStringLiteral("1"), QStringLiteral("1.5"), QStringLiteral("2")};
    const QStringList data{QStringLiteral("4"), QStringLiteral("5"), QStringLiteral("6"), QStringLiteral("7"), QStringLiteral("8")};
    items(QStringLiteral("cmb_tip_com"), tipCom);
    items(QStringLiteral("cmb_ver_spde"), {msg(QStringLiteral("spde_nes")), QStringLiteral("Fosber V1.0"), QStringLiteral("Fosber V2.0"), QStringLiteral("Fosber V2.1"),
                                           QStringLiteral("Fosber V3.0"), QStringLiteral("Simon V1.0"), QStringLiteral("MHI V1.0")});
    items(QStringLiteral("cmb_baud_spde"), {QStringLiteral("300"), QStringLiteral("1200"), QStringLiteral("2400"), QStringLiteral("4800"), QStringLiteral("9600"),
                                            QStringLiteral("14400"), QStringLiteral("19200"), QStringLiteral("28800"), QStringLiteral("38400"), QStringLiteral("57600")});
    items(QStringLiteral("cmb_bit_par_spde"), parity);
    items(QStringLiteral("cmb_bit_stop_spde"), stop);
    items(QStringLiteral("cmb_dati_spde"), data);
    items(QStringLiteral("cmb_baud_azi"), {QStringLiteral("5"), QStringLiteral("10"), QStringLiteral("20"), QStringLiteral("50"), QStringLiteral("100"),
                                           QStringLiteral("125"), QStringLiteral("250"), QStringLiteral("500"), QStringLiteral("1000")});
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    for (int i = 0; i < 3; ++i) {
        const bool used = i < levels;
        items(at("cmb_baud_sch_tag", i), used ? QStringList{QStringLiteral("1200"), QStringLiteral("2400"), QStringLiteral("4800"), QStringLiteral("9600"), QStringLiteral("14400"),
                                                            QStringLiteral("19200"), QStringLiteral("28800"), QStringLiteral("38400"), QStringLiteral("57600")} : QStringList());
        items(at("cmb_bit_par_sch_tag", i), used ? parity : QStringList());
        items(at("cmb_bit_stop_sch_tag", i), used ? stop : QStringList());
        items(at("cmb_dati_sch_tag", i), used ? data : QStringList());
        items(at("cmb_tip_com_tag", i), used ? tipCom : QStringList());
    }
}

void SetParComModel::selectText(const QString &control, const QString &text)
{
    set(control, QStringLiteral("listIndex"), int(value(control, QStringLiteral("items")).toStringList().indexOf(text)));
}

QString SetParComModel::comboText(const QString &control) const
{
    const QStringList items = value(control, QStringLiteral("items")).toStringList();
    const int index = listIndex(control);
    return index >= 0 && index < items.size() ? items.at(index) : QString();
}

// ges_combo. Val("N") is 0, so every parity that is not a number shows and saves as "E", as in VB.
void SetParComModel::gesCombo()
{
    const TipComSpde &spde = par_mac_ute.com_spde;
    set(QStringLiteral("cmb_ver_spde"), QStringLiteral("listIndex"), spde.ver_spde);
    set(QStringLiteral("cmb_tip_com"), QStringLiteral("listIndex"), int(spde.tip_com.val()));
    selectText(QStringLiteral("cmb_baud_spde"), spde.tip_porta.baud.trimmed());
    selectText(QStringLiteral("cmb_bit_par_spde"), spde.tip_porta.bit_par.trimmed());
    selectText(QStringLiteral("cmb_bit_stop_spde"), spde.tip_porta.bit_stop.trimmed());
    selectText(QStringLiteral("cmb_dati_spde"), spde.tip_porta.dati.trimmed());
    const auto orDefault = [](const FixStr &value, const char *fallback) { return value.val() == 0 ? QString::fromLatin1(fallback) : value.trimmed(); };
    selectText(QStringLiteral("cmb_baud_azi"), orDefault(par_mac_ute.com_azi.baud, "1000"));
    for (int i = 1; i <= int(par_mac.car_tec.num_liv_tag.val()); ++i) {
        const TipPor &por = par_mac_ute.com_sch_tag(i);
        selectText(at("cmb_baud_sch_tag", i - 1), orDefault(por.baud, "38400"));
        selectText(at("cmb_bit_par_sch_tag", i - 1), orDefault(por.bit_par, "E"));
        selectText(at("cmb_bit_stop_sch_tag", i - 1), orDefault(por.bit_stop, "1"));
        selectText(at("cmb_dati_sch_tag", i - 1), orDefault(por.dati, "8"));
        set(at("cmb_tip_com_tag", i - 1), QStringLiteral("listIndex"), int(par_mac_ute.tip_com_sch_tag(i).val()));
    }
}

void SetParComModel::filParCom()
{
    const TipComSpde &spde = par_mac_ute.com_spde;
    setText(QStringLiteral("txt_porta_spde"), spde.tip_porta.porta.trimmed());
    setText(QStringLiteral("txt_por_loc"), spde.lan.por_loc.trimmed());
    setText(QStringLiteral("txt_por_rem"), spde.lan.por_rem.trimmed());
    setText(QStringLiteral("txt_ind_rem"), spde.lan.ind_rem.trimmed());
    setText(QStringLiteral("txt_err_max"), spde.err_max.trimmed());
    setText(QStringLiteral("txt_timeout"), spde.TimeOut.trimmed());
    setText(QStringLiteral("txt_ind_plc"), par_mac_ute.com_plc_lan.ind_rem.trimmed());
    setText(QStringLiteral("txt_por_plc"), par_mac_ute.com_plc_lan.por_rem.trimmed());
    setText(QStringLiteral("txt_timeout_lan"), par_mac_ute.com_plc_lan.TimeOut.trimmed());
    set(QStringLiteral("chk_abi_lan"), QStringLiteral("checked"), par_mac_ute.com_plc_lan.abi_lan.val() != 0);
    setText(QStringLiteral("txt_net_plc"), QString::number(par_mac_ute.indirizzo_plc.net_id));
    setText(QStringLiteral("txt_node_plc"), QString::number(par_mac_ute.indirizzo_plc.node_id));
    setText(QStringLiteral("txt_unit_plc"), QString::number(par_mac_ute.indirizzo_plc.unit_id));
    setText(QStringLiteral("txt_timer_scans_plc"), QString::number(par_mac_ute.indirizzo_plc.TIMER_SCANS_PLC));
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    for (int i = 1; i <= 3; ++i) {
        const bool used = i <= levels;
        setText(at("txt_porta_sch_tag", i - 1), used ? par_mac_ute.com_sch_tag(i).porta.trimmed() : QString());
        setText(at("txt_timeout_sch_tag", i - 1), used ? par_mac_ute.com_sch_tag(i).TimeOut.trimmed() : QString());
        setText(at("txt_por_loc_tag", i - 1), used ? par_mac_ute.lan_sch_tag(i).por_loc.trimmed() : QString());
        setText(at("txt_por_rem_tag", i - 1), used ? par_mac_ute.lan_sch_tag(i).por_rem.trimmed() : QString());
        setText(at("txt_ind_rem_tag", i - 1), used ? par_mac_ute.lan_sch_tag(i).ind_rem.trimmed() : QString());
        set(at("chk_tip_com_tag_udp", i - 1), QStringLiteral("checked"), used && par_mac_ute.lan_sch_tag_UDP(i));
        set(at("label_liv", i - 1), QStringLiteral("enabled"), used);
        for (const char *name : {"txt_porta_sch_tag", "cmb_baud_sch_tag", "cmb_bit_par_sch_tag", "cmb_dati_sch_tag", "cmb_bit_stop_sch_tag", "txt_timeout_sch_tag",
                                 "txt_por_loc_tag", "txt_por_rem_tag", "txt_ind_rem_tag", "cmb_tip_com_tag", "chk_tip_com_tag_udp"})
            set(at(name, i - 1), QStringLiteral("backColor"), used ? QStringLiteral("#ffffff") : gray());
    }
    setText(QStringLiteral("txt_timeout_azi"), par_mac_ute.com_azi.TimeOut.trimmed());
    gesCombo();
}

void SetParComModel::lock()
{
    for (const QString &control : std::as_const(m_tab))
        set(control, QStringLiteral("enabled"), false);
    focus(QStringLiteral("txt_idle"));
}

void SetParComModel::activate(const QString &)
{
    m_mod = false;
    iniCombo();
    filParCom();
    lock();
    commit();
}

void SetParComModel::controlEvent(const QString &name, int index, const QString &event, const QVariantMap &args)
{
    const QString control = index >= 0 ? QStringLiteral("%1(%2)").arg(name).arg(index) : name;
    if (event == QLatin1String("Click")) {
        if (control.startsWith(QLatin1String("chk_")))
            set(control, QStringLiteral("checked"), args.value(QStringLiteral("checked")));
        else if (control.startsWith(QLatin1String("cmb_")))
            set(control, QStringLiteral("listIndex"), args.value(QStringLiteral("index")));
        return;
    }
    SetParFormModel::controlEvent(name, index, event, args);
}

// scr_par_mac_ute. chk_abi_lan is shown and never read back, as in VB.
void SetParComModel::scrParUte()
{
    const auto text = [this](const QString &control) { return value(control).toString(); };
    TipComSpde &spde = par_mac_ute.com_spde;
    spde.ver_spde = listIndex(QStringLiteral("cmb_ver_spde"));
    spde.tip_com = QString::number(listIndex(QStringLiteral("cmb_tip_com")));
    spde.tip_porta.baud = comboText(QStringLiteral("cmb_baud_spde"));
    spde.tip_porta.bit_par = comboText(QStringLiteral("cmb_bit_par_spde"));
    spde.tip_porta.dati = comboText(QStringLiteral("cmb_dati_spde"));
    spde.tip_porta.bit_stop = comboText(QStringLiteral("cmb_bit_stop_spde"));
    spde.tip_porta.porta = text(QStringLiteral("txt_porta_spde"));
    spde.lan.por_loc = text(QStringLiteral("txt_por_loc"));
    spde.lan.por_rem = text(QStringLiteral("txt_por_rem"));
    spde.lan.ind_rem = text(QStringLiteral("txt_ind_rem"));
    spde.err_max = text(QStringLiteral("txt_err_max"));
    spde.TimeOut = text(QStringLiteral("txt_timeout"));
    par_mac_ute.com_plc_lan.ind_rem = text(QStringLiteral("txt_ind_plc"));
    par_mac_ute.com_plc_lan.por_rem = text(QStringLiteral("txt_por_plc"));
    par_mac_ute.com_plc_lan.TimeOut = text(QStringLiteral("txt_timeout_lan"));
    par_mac_ute.indirizzo_plc.net_id = vb::cInt(vb::val(text(QStringLiteral("txt_net_plc"))));
    par_mac_ute.indirizzo_plc.node_id = vb::cInt(vb::val(text(QStringLiteral("txt_node_plc"))));
    par_mac_ute.indirizzo_plc.unit_id = vb::cInt(vb::val(text(QStringLiteral("txt_unit_plc"))));
    par_mac_ute.indirizzo_plc.TIMER_SCANS_PLC = vb::cInt(vb::val(text(QStringLiteral("txt_timer_scans_plc"))));
    par_mac_ute.com_azi.TimeOut = text(QStringLiteral("txt_timeout_azi"));
    par_mac_ute.com_azi.baud = comboText(QStringLiteral("cmb_baud_azi"));
    for (int i = 1; i <= int(par_mac.car_tec.num_liv_tag.val()); ++i) {
        TipPor &por = par_mac_ute.com_sch_tag(i);
        por.porta = text(at("txt_porta_sch_tag", i - 1));
        por.TimeOut = text(at("txt_timeout_sch_tag", i - 1));
        por.baud = comboText(at("cmb_baud_sch_tag", i - 1));
        por.bit_par = comboText(at("cmb_bit_par_sch_tag", i - 1));
        por.bit_stop = comboText(at("cmb_bit_stop_sch_tag", i - 1));
        por.dati = comboText(at("cmb_dati_sch_tag", i - 1));
        par_mac_ute.tip_com_sch_tag(i) = QString::number(listIndex(at("cmb_tip_com_tag", i - 1)));
        par_mac_ute.lan_sch_tag(i).por_loc = text(at("txt_por_loc_tag", i - 1));
        par_mac_ute.lan_sch_tag(i).por_rem = text(at("txt_por_rem_tag", i - 1));
        par_mac_ute.lan_sch_tag(i).ind_rem = text(at("txt_ind_rem_tag", i - 1));
        par_mac_ute.lan_sch_tag_UDP(i) = value(at("chk_tip_com_tag_udp", i - 1), QStringLiteral("checked")).toBool();
    }
}

// porte_chg
bool SetParComModel::porteChg() const
{
    const TipComSpde &u = par_mac_ute.com_spde;
    const TipComSpde &p = par_mac.com_spde;
    if (u.tip_com.str() != p.tip_com.str())
        return true;
    if (u.tip_com.trimmed() == QLatin1String("0")
        && (u.tip_porta.baud.str() != p.tip_porta.baud.str() || u.tip_porta.bit_par.str() != p.tip_porta.bit_par.str() || u.tip_porta.bit_stop.str() != p.tip_porta.bit_stop.str()
            || u.tip_porta.dati.str() != p.tip_porta.dati.str() || u.tip_porta.porta.str() != p.tip_porta.porta.str()))
        return true;
    if (u.tip_com.trimmed() == QLatin1String("1")
        && (u.lan.ind_rem != p.lan.ind_rem || u.lan.por_loc.str() != p.lan.por_loc.str() || u.lan.por_rem.str() != p.lan.por_rem.str() || u.lan.tip != p.lan.tip))
        return true;
    for (int i = 1; i <= int(par_mac.car_tec.num_liv_tag.val()); ++i) {
        const TipPor &a = par_mac_ute.com_sch_tag(i);
        const TipPor &b = par_mac.com_sch_tag(i);
        if (a.baud.str() != b.baud.str() || a.bit_par.str() != b.bit_par.str() || a.bit_stop.str() != b.bit_stop.str() || a.dati.str() != b.dati.str() || a.porta.str() != b.porta.str()
            || par_mac_ute.tip_com_sch_tag(i).str() != par_mac.tip_com_sch_tag(i).str() || par_mac_ute.lan_sch_tag(i).ind_rem != par_mac.lan_sch_tag(i).ind_rem
            || par_mac_ute.lan_sch_tag(i).por_loc.str() != par_mac.lan_sch_tag(i).por_loc.str() || par_mac_ute.lan_sch_tag(i).por_rem.str() != par_mac.lan_sch_tag(i).por_rem.str()
            || par_mac_ute.lan_sch_tag_UDP(i) != par_mac.lan_sch_tag_UDP(i))
            return true;
    }
    return false;
}

// porte_ok: a card's serial port must be valid and its own even when the card is on the network.
bool SetParComModel::porteOk() const
{
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    const bool serial = par_mac_ute.com_spde.tip_com.trimmed() == QLatin1String("0");
    const auto bad = [](const FixStr &port) { return port.val() < 1 || port.val() > 6; };
    if (serial && bad(par_mac_ute.com_spde.tip_porta.porta))
        return false;
    for (int i = 1; i <= levels; ++i) {
        if (bad(par_mac_ute.com_sch_tag(i).porta))
            return false;
        if (serial && par_mac_ute.com_spde.tip_porta.porta.str() == par_mac_ute.com_sch_tag(i).porta.str())
            return false;
    }
    for (int i = 1; i < levels; ++i) {
        for (int m = i + 1; m <= levels; ++m) {
            if (par_mac_ute.com_sch_tag(i).porta.str() == par_mac_ute.com_sch_tag(m).porta.str())
                return false;
        }
    }
    return true;
}

bool SetParComModel::functionKey(const QString &key, int shift)
{
    if (key == QLatin1String("ESC")) {
        if (shift == 0 && m_mod) {
            m_mod = false;
            par_mac_ute = par_mac;
            filParCom();
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
            m_mod = true;
            const int levels = int(par_mac.car_tec.num_liv_tag.val());
            for (const QString &control : std::as_const(m_tab))
                set(control, QStringLiteral("enabled"), !control.endsWith(u')') || control.at(control.size() - 2).digitValue() < levels);
            focus(QStringLiteral("cmb_ver_spde"));
            commit();
        }
        return true;
    }
    if (key == QLatin1String("F3")) {
        if (shift == 0 && m_mod) {
            messaggio(msg(QStringLiteral("ope_in_cor")), false, false);
            scrParUte();
            if (porteChg()) {
                if (!porteOk()) {
                    par_mac_ute = par_mac;
                    filParCom();
                    messaggio(msg(QStringLiteral("pro_imp_por_ser")), true, false);
                    lock();
                    m_mod = false;
                    commit();
                    return true;
                }
                m_link->boards().closeSockets();
            }
            const TipIndFins old = par_mac.indirizzo_plc;
            par_mac = par_mac_ute;
            scrParMac();
            // change_ind_plc
            const TipIndFins &now = par_mac.indirizzo_plc;
            if (old.net_id != now.net_id || old.node_id != now.node_id || old.unit_id != now.unit_id || par_mac.com_plc_lan.abi_lan.val() != 0)
                emit plcAddressChanged();
            lock();
            m_mod = false;
            if (m_state->comOK())
                messaggio(msg(QStringLiteral("ope_eff")), false, false);
            commit();
        }
        return true;
    }
    return m_mod || shift != 0;
}
