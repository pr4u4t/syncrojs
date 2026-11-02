#include "stabarform.h"

#include "classestato.h"
#include "collezioneordini.h"
#include "messages.h"
#include "parmac.h"
#include "stabarmodel.h"
#include "stagglobals.h"
#include "unimisang.h"
#include "vbcompat.h"
#include "vbtypes.h"

#include <QDateTime>

using namespace stag;

namespace {

constexpr quint32 MEDIUM_GRAY = 0xC0C0C0;
constexpr quint32 LAMP_UNKNOWN = 0x808080;

QString at(const char *name, int index)
{
    return QStringLiteral("%1(%2)").arg(QLatin1String(name)).arg(index);
}

QString color(quint32 bgr)
{
    return StaBarModel::vbColor(bgr);
}

} // namespace

StaBarFormModel::StaBarFormModel(StaBarModel *bar, QObject *parent)
    : FormModel(parent)
    , m_bar(bar)
{
    connect(bar, &StaBarModel::changed, this, &StaBarFormModel::publish);
    connect(&m_clock, &QTimer::timeout, this, &StaBarFormModel::tickClock);
    m_clock.setInterval(1000);
    m_clock.start();
    set(QStringLiteral("ssp_mes_all"), QStringLiteral("visible"), false);
    // Not as in the .frm: the narrow white pieces it puts at the right of every frame leave the green
    // headers shorter than their cells.
    for (const char *name : {"label9", "label1", "label17", "label5", "label21", "label16", "label22", "label32", "label6", "label2", "label7", "lab_run"})
        set(QLatin1String(name), QStringLiteral("visible"), false);
    // The last frame ends 8 pixels before the edge of the bar: it goes on to the edge.
    set(QStringLiteral("frm_scarti"), QStringLiteral("width"), 69);
    for (int i = 1; i <= 3; ++i)
        set(at("txt_sca", i), QStringLiteral("width"), 65);
}

void StaBarFormModel::publish()
{
    const auto text = [this](const char *control, const char *property) { setText(QLatin1String(control), m_bar->property(property).toString()); };
    const auto paint = [this](const char *control, const char *what, const char *property) {
        const QString value = m_bar->property(property).toString();
        if (!value.isEmpty())
            set(QLatin1String(control), QLatin1String(what), value);
    };
    text("txt_sta_run", "txtStaRun");
    paint("txt_sta_run", "foreColor", "txtStaRunFore");
    paint("txt_sta_run", "backColor", "txtStaRunBack");
    paint("frm_sta_run", "backColor", "frmStaRunBack");
    text("txt_sta_res", "txtStaRes");
    paint("txt_sta_res", "foreColor", "txtStaResFore");
    paint("frm_sta_res", "backColor", "frmStaResBack");
    text("lab_sta_mac", "labStaMac");
    set(QStringLiteral("lab_sta_mac"), QStringLiteral("visible"), !m_bar->property("labStaMac").toString().isEmpty());
    text("txt_mod", "txtMod");
    paint("txt_mod", "foreColor", "txtModFore");
    paint("frm_mod", "backColor", "frmModBack");
    text("txt_vel", "txtVel");
    text("txt_vel_max", "txtVelMax");
    paint("frm_vel", "backColor", "frmVelBack");
    paint("shp_all_mal", "fillColor", "shpAllMal");
    // Not as in VB, where they go green: without the PLC nobody knows the state of these groups. The "!"
    // stays as it is, since chk_all_sftw lights it for the lost communication itself.
    const bool noPlc = m_bar->property("linNoComPlc").toBool();
    for (const auto &[control, property] : {std::pair{"shp_all_har", "shpAllHar"}, {"shp_all_qe", "shpAllQe"}, {"shp_all_bm", "shpAllBm"}, {"shp_all_con", "shpAllCon"}}) {
        if (noPlc)
            set(QLatin1String(control), QStringLiteral("fillColor"), color(LAMP_UNKNOWN));
        else
            paint(control, "fillColor", property);
    }
    set(QStringLiteral("lin_no_com_plc"), QStringLiteral("visible"), m_bar->property("linNoComPlc"));
    set(QStringLiteral("lin_no_com_spde"), QStringLiteral("visible"), m_bar->property("linNoComSpde"));
    paint("lin_no_com_spde", "borderColor", "linNoComSpdeColor");
    paint("frm_com", "backColor", "frmComBack");
    paint("led_plc_rx", "fillColor", "ledPlcRx");
    paint("led_plc_tx", "fillColor", "ledPlcTx");
    setText(at("lab_liv", 1), m_bar->property("labLiv1").toString());
    setText(at("lab_liv", 2), m_bar->property("labLiv2").toString());
    setText(at("lab_liv", 3), m_bar->property("labLiv3").toString());
    text("lab_msg", "labMsg");
    paint("lab_msg", "foreColor", "labMsgFore");
    text("txt_tem_co", "txtTemCo");
    text("txt_met_man", "txtMetMan");
    paint("txt_met_man", "foreColor", "txtMetManFore");
    paint("frm_met_man", "backColor", "frmMetManBack");
    commit();
}

// Form_Load: the levels the machine does not have are gray.
void StaBarFormModel::activate(const QString &)
{
    // clear_cell: the .frm keeps the designer's sample values in its fields.
    clearCounters();
    m_bar->eveTagLiv();
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    for (int level = 1; level <= 2; ++level) {
        if (levels > level)
            continue;
        const int index = level == 2 || levels == 1 ? level : 0;
        if (index == 0)
            continue;
        set(at("liv", index), QStringLiteral("backColor"), color(MEDIUM_GRAY));
        setText(at("lab_liv", index), QString());
        for (const char *name : {"txt_num_sca", "txt_err", "txt_sca"})
            set(at(name, index), QStringLiteral("backColor"), color(MEDIUM_GRAY));
    }
    // The gray pieces only cover the white ones hidden above: the gray field fills the cell by itself.
    for (int i = 0; i <= 2; ++i)
        set(at("lab_gray_med", i), QStringLiteral("visible"), false);
    publish();
    tickClock();
}

void StaBarFormModel::tickClock()
{
    const QDateTime now = vb::now();
    setText(QStringLiteral("txt_ora"), now.toString(QStringLiteral("hh:mm:ss")));
    setText(QStringLiteral("txt_dat"), now.toString(QStringLiteral("dd:MM:yyyy")));
    checkProductionReport();
    commit();
}

void StaBarFormModel::checkProductionReport()
{
    if (pro_set.abi_reg_rep_pro == m_oldAbiRegRepPro)
        return;
    if (stag::sta_tag && stag::sta_tag->comOK())
        messagWin(pro_set.abi_reg_rep_pro == QLatin1String(STR_FALSE) ? msg(QStringLiteral("reg_pro_int")) : QString(), false);
    m_oldAbiRegRepPro = pro_set.abi_reg_rep_pro;
}

// eve_tag_err
void StaBarFormModel::eveTagErr(int liv, const QString &err)
{
    static std::array<qint64, 4> errors{};
    if (vb::trim(err).isEmpty()) {
        setText(at("txt_err", liv), QString());
        errors[size_t(liv)] = 0;
        commit();
        return;
    }
    setText(at("txt_err", liv), abi_mis_ang ? con_dmm_to_inc(err) : err);
    errors[size_t(liv)] = qAbs(qint64(vb::val(err)));
    bool found = false;
    const auto check = [&](int index, int level) {
        const TipSchTagLiv &tag = par_mac.sch_tag.sch_tag_liv(level);
        const bool over = errors[size_t(index)] > tag.lim_inf_tol_tag.val() || errors[size_t(index)] > tag.lim_sup_tol_tag.val();
        found = found || over;
        set(at("txt_err", index), QStringLiteral("foreColor"), color(over ? StaBarModel::COL_ALL_Red : StaBarModel::vbBlack));
    };
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    if (levels == 1) {
        check(3, LIV_ALT);
    } else if (levels == 2) {
        check(1, LIV_BAS);
        check(3, LIV_ALT);
    } else if (levels == 3) {
        for (int i = 1; i <= 3; ++i)
            check(i, i);
    }
    set(QStringLiteral("frm_err"), QStringLiteral("backColor"), color(found ? StaBarModel::COL_ALL_Red : StaBarModel::COL_DARK_Green));
    commit();
}

// eve_tag_num
void StaBarFormModel::eveTagNum(int liv, const QString &mis)
{
    setText(at("txt_num_sca", liv), mis);
    bool found = false;
    if (stag::ord_mac && stag::ord_mac->Count() > 0) {
        const OrdinePtr order = stag::ord_mac->item(1);
        for (int i = 1; i <= 3; ++i) {
            const int level = order->Ord_liv(i);
            if (level == 0)
                continue;
            const bool over = vb::val(value(at("txt_num_sca", level)).toString()) > vb::val(order->ord(i, QStringLiteral("sca_tot_num")));
            found = found || over;
            set(at("txt_num_sca", level), QStringLiteral("foreColor"), color(over ? StaBarModel::COL_ALL_Red : StaBarModel::vbBlack));
        }
    }
    set(QStringLiteral("frm_num_sca"), QStringLiteral("backColor"), color(found ? StaBarModel::COL_ALL_Red : StaBarModel::COL_DARK_Green));
    commit();
}

void StaBarFormModel::eveTagNumErr(int liv, const QString &mis)
{
    setText(at("txt_sca", liv), mis);
    commit();
}

void StaBarFormModel::clearCounters()
{
    for (int i = 1; i <= 3; ++i) {
        setText(at("txt_num_sca", i), QString());
        setText(at("txt_err", i), QString());
        setText(at("txt_sca", i), QString());
    }
    set(QStringLiteral("frm_err"), QStringLiteral("backColor"), color(StaBarModel::COL_DARK_Green));
    commit();
}

void StaBarFormModel::messagWin(const QString &text, bool abiMot)
{
    if (text == m_panel)
        return;
    m_panel = text;
    const QString panel = QStringLiteral("ssp_mes_all");
    setText(QStringLiteral("mes_all"), text);
    set(panel, QStringLiteral("visible"), !text.isEmpty());
    if (!abiMot) {
        set(panel, QStringLiteral("x"), 128);
        set(panel, QStringLiteral("y"), 30);
        set(panel, QStringLiteral("width"), 50 + 56 + 78 + 82);
        set(panel, QStringLiteral("height"), 49);
        set(QStringLiteral("mes_all"), QStringLiteral("width"), 50 + 56 + 78 + 82 - 8);
        set(QStringLiteral("mes_all"), QStringLiteral("height"), 44);
        if (text.isEmpty()) {
            set(QStringLiteral("frm_mod"), QStringLiteral("visible"), true);
            set(at("fra_ora", 0), QStringLiteral("visible"), true);
        }
    } else {
        set(panel, QStringLiteral("x"), 1);
        set(panel, QStringLiteral("y"), 30);
        set(panel, QStringLiteral("width"), 81 + 48);
        set(panel, QStringLiteral("height"), 49);
        set(QStringLiteral("mes_all"), QStringLiteral("width"), 81 + 48 - 8);
        set(QStringLiteral("mes_all"), QStringLiteral("height"), 44);
        set(QStringLiteral("frm_mod"), QStringLiteral("visible"), false);
        set(at("fra_ora", 0), QStringLiteral("visible"), false);
    }
    commit();
}

void StaBarFormModel::ledSpdeRx()
{
    m_ledRx = m_ledRx == StaBarModel::vbGreen ? StaBarModel::COL_DARK_Green : StaBarModel::vbGreen;
    if (m_ledTx == m_ledRx)
        m_ledTx = m_ledRx == StaBarModel::vbGreen ? StaBarModel::COL_DARK_Green : StaBarModel::vbGreen;
    set(QStringLiteral("led_spde_rx"), QStringLiteral("fillColor"), color(m_ledRx));
    set(QStringLiteral("led_spde_tx"), QStringLiteral("fillColor"), color(m_ledTx));
    commit();
}

void StaBarFormModel::ledSpdeTx()
{
    if (!m_txToggle)
        std::swap(m_ledRx, m_ledTx);
    m_txToggle = !m_txToggle;
    set(QStringLiteral("led_spde_rx"), QStringLiteral("fillColor"), color(m_ledRx));
    set(QStringLiteral("led_spde_tx"), QStringLiteral("fillColor"), color(m_ledTx));
    commit();
}

void StaBarFormModel::ledSpdeLost()
{
    m_ledRx = m_ledTx = StaBarModel::vbRed;
    set(QStringLiteral("led_spde_rx"), QStringLiteral("fillColor"), color(m_ledRx));
    set(QStringLiteral("led_spde_tx"), QStringLiteral("fillColor"), color(m_ledTx));
    commit();
}
