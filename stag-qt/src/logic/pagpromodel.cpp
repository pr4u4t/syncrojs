#include "pagpromodel.h"

#include "classestato.h"
#include "messages.h"
#include "parmac.h"
#include "stabarmodel.h"
#include "stagglobals.h"
#include "uihooks.h"
#include "unimisang.h"
#include "vbcompat.h"

using namespace stag;

static constexpr quint32 COL_BLUE = 0xFF8800;

PagProModel::PagProModel(CollezioneOrdini *orders, QObject *parent)
    : FormModel(parent)
    , m_orders(orders)
{
    connect(m_orders, &CollezioneOrdini::Refresh, this, [this] { refresh(); });
}

void PagProModel::refresh()
{
    const int count = m_orders->Count();
    showOrder(QStringLiteral("cor"), count >= 1 ? m_orders->item(1) : nullptr);
    showOrder(QStringLiteral("suc"), count >= 2 ? m_orders->item(2) : nullptr);
    commit();
}

// dis_cor and dis_suc, which differ only in the control names.
void PagProModel::showOrder(const QString &prefix, const OrdinePtr &order)
{
    const auto text = [&](const QString &field) { return order ? vb::trim(order->text(field)) : QString(); };
    setText(prefix + QStringLiteral("_cod_ord"), order ? order->cod_ord_abb() : QString());
    setText(prefix + QStringLiteral("_tip_car"), text(QStringLiteral("tip_car.cod_com_car")));
    setText(prefix + QStringLiteral("_tip_ond"), text(QStringLiteral("tip_car.Cod_ond")));
    const QString total = text(QStringLiteral("met_tot_ord"));
    setText(prefix + QStringLiteral("_met_tot"), order && abi_mis_ang ? con_met_to_fet(total) : total);

    for (int i = 1; i <= 3; ++i) {
        const auto control = [&](const char *name) { return QStringLiteral("%1_%2(%3)").arg(prefix, QLatin1String(name)).arg(i - 1); };
        const QString level = order ? vb::trim(order->ord(i, QStringLiteral("Ord_liv"))) : QString();
        if (level.isEmpty()) {
            for (const char *name : {"liv", "com", "lun_sca", "lar_sca", "sca", "res", "met"})
                setText(control(name), QString());
            continue;
        }
        const auto field = [&](const char *name) { return order->ord(i, QLatin1String(name)); };
        setText(control("com"), vb::trim(field("com.cod_com_ord")));
        setText(control("res"), vb::trim(field("Sca_per_tag")));
        setText(control("sca"), vb::trim(field("sca_tot_num")));
        const int levelNumber = int(vb::val(level));
        QString levelText;
        if (levelNumber == LIV_BAS)
            levelText = msg(QStringLiteral("liv_bas"));
        else if (levelNumber == LIV_MED)
            levelText = msg(QStringLiteral("liv_med"));
        else if (levelNumber == LIV_ALT)
            levelText = msg(QStringLiteral("liv_alt"));
        setText(control("liv"), levelText);
        if (abi_mis_ang) {
            setText(control("lun_sca"), con_dmm_to_inc(vb::trim(field("sca_lun"))));
            setText(control("lar_sca"), con_dmm_to_inc(vb::trim(field("sca_lar"))));
        } else {
            setText(control("lun_sca"), QString::number(vb::intDiv(vb::val(field("sca_lun")), TODECMM)));
            setText(control("lar_sca"), QString::number(vb::intDiv(vb::val(field("sca_lar")), TODECMM)));
        }
        const QString meters = vb::trim(field("met"));
        setText(control("met"), abi_mis_ang ? con_met_to_fet(meters) : meters);
    }
}

void PagProModel::setCommand(Rmt *command)
{
    m_command = command;
    connect(command, &Rmt::retok, this, [this](const QString &text) {
        if (!text.isEmpty())
            stag::messaggio(text, false);
        m_command->resetta();
    });
    connect(command, &Rmt::retbad, this, [this](const QString &text) {
        if (!text.isEmpty())
            stag::messaggio(text, true);
        m_command->resetta();
    });
}

// The Ctrl keys of eve_key_F1, F2 and F4: operator commands to the PLC.
bool PagProModel::sendCommand(int request, int order)
{
    if (!stag::sta_tag || !stag::sta_tag->comOK()) {
        stag::messaggio(msg(QStringLiteral("com_int_con_mac")), true, false);
        return true;
    }
    if (m_command && m_command->esegui(request, 0, order))
        stag::messaggio(msg(QStringLiteral("ope_in_cor")), false, false);
    else
        stag::messaggio(msg(QStringLiteral("imp_ese_ope")), false, false);
    return true;
}

bool PagProModel::functionKey(const QString &key, int shift)
{
    if (shift == 2) {
        if (key == QLatin1String("F1") || key == QLatin1String("F2")) {
            const bool next = key == QLatin1String("F2");
            if (m_orders->Count() < (next ? 2 : 1)) {
                stag::messaggio(msg(QStringLiteral("msg_ord_ass")), true, false);
                return true;
            }
            return sendCommand(stag::INVIO_ORD, next ? stag::SUCCESSIVO : stag::CORRENTE);
        }
        if (key == QLatin1String("F4"))
            return sendCommand(stag::AZZERAMENTO_CONTATORI, 0);
        return false;
    }
    // eve_key_F6: the pattern list only exists on a machine with cut to pattern.
    if (key != QLatin1String("F6") || shift != 0)
        return false;
    for (int level = 1; level <= 3; ++level) {
        if (par_mac.opt.cut_pat(level) == QLatin1String(STR_TRUE) || par_mac.opt.cut_pat_auto(level) == QLatin1String(STR_TRUE))
            return false;
    }
    stag::messaggio(msg(QStringLiteral("imp_ese_ope")), true);
    return true;
}

// eve_key_F3: without a password this key opens Pwd_frm; the right to enter lasts one use.
QString PagProModel::navigationTarget(const QString &key, int shift)
{
    if (key != QLatin1String("F3") || shift != 0)
        return {};
    const bool allowed = pro_set.ute_wrk_liv1 || pro_set.ute_wrk;
    pro_set.ute_wrk_liv1 = false;
    return allowed ? QStringLiteral("Set_par_mac") : QStringLiteral("Pwd_frm");
}

void PagProModel::orderStatus(int event, int order)
{
    if (event == ClasseStato::ord_ok) {
        if (order == ClasseStato::CORR)
            setColor(order, msg(QStringLiteral("msg_ord_in_lav")), COL_BLUE);
        else if (order == ClasseStato::SUCC)
            setColor(order, msg(QStringLiteral("msg_ord_all")), StaBarModel::COL_DARK_Green);
    } else if (event == ClasseStato::ord_nok) {
        setColor(order, msg(QStringLiteral("msg_ord_non_all")), StaBarModel::COL_ALL_Red);
    } else if (event == ClasseStato::ord_ass) {
        setColor(order, msg(QStringLiteral("msg_ord_ass")), StaBarModel::COL_ALL_Red);
    }
    commit();
}

// set_col
void PagProModel::setColor(int index, const QString &text, quint32 color)
{
    if (index < 1 || index > 2)
        return;
    const QString shown = StaBarModel::vbColor(color);
    set(QStringLiteral("frm_sta(%1)").arg(index), QStringLiteral("backColor"), shown);
    const QString label = QStringLiteral("lab_ord(%1)").arg(index);
    set(label, QStringLiteral("backColor"), shown);
    setText(label, text);
    set(label, QStringLiteral("foreColor"), StaBarModel::vbColor(color == StaBarModel::vbWhite ? StaBarModel::vbBlack : StaBarModel::vbWhite));
}
