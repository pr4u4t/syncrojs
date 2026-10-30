#include "abilitazionimodel.h"

#include "classestato.h"
#include "inistore.h"
#include "messages.h"
#include "parmac.h"
#include "stabarmodel.h"
#include "stagglobals.h"
#include "uihooks.h"
#include "vbcompat.h"

using namespace stag;

static constexpr quint32 LIGHT_GREEN = 0x00FF00;
static constexpr quint32 LIGHT_RED = 0x9895FF;
static constexpr quint32 LIGHT_GRAY = 0xE0E0E0;
static constexpr quint32 MEDIUM_GRAY = 0x808080;
static constexpr quint32 DARK_BLUE = 0xC00000;

static QString color(quint32 bgr)
{
    return StaBarModel::vbColor(bgr);
}

AbilitazioniModel::AbilitazioniModel(CollezioneOrdini *orders, QObject *parent)
    : FormModel(parent)
    , m_orders(orders)
{
}

void AbilitazioniModel::showSwitch(const QString &control, bool on)
{
    set(control, QStringLiteral("checked"), on);
    set(control, QStringLiteral("backColor"), color(on ? LIGHT_GREEN : LIGHT_RED));
    setText(control, on ? QStringLiteral("ON") : QStringLiteral("OFF"));
}

void AbilitazioniModel::setCommand(Rmt *command)
{
    m_command = command;
    connect(command, &Rmt::retok, this, [this](const QString &text) {
        par_abi = par_abi_ute;
        if (!text.isEmpty())
            messaggio(text, false);
        m_command->resetta();
        commit();
    });
    connect(command, &Rmt::retbad, this, [this](const QString &text) {
        // Back to the values before the change, in the ini file and on the page.
        par_abi_ute = par_abi;
        scrParAbi(false);
        if (!m_pendingKey.isEmpty())
            revert(m_pendingKey);
        if (!text.isEmpty())
            messaggio(text, true);
        m_command->resetta();
        commit();
    });
}

// scr_par_abi
void AbilitazioniModel::scrParAbi(bool useUser)
{
    const TipAbi &values = useUser ? par_abi_ute : par_abi;
    const auto bit = [](bool on) { return on ? QStringLiteral("1") : QStringLiteral("0"); };
    setFileINIString(QStringLiteral("abilitazioni"), QStringLiteral("abi_com_pu"), bit(values.abi_com_PU));
    for (int i = 1; i <= int(par_mac.car_tec.num_liv_tag.val()); ++i) {
        const QString section = QStringLiteral("abi_liv_%1").arg(i);
        setFileINIString(section, QStringLiteral("liv"), bit(values.liv(i)));
        for (int m = 1; m <= int(par_mac.car_tec.num_mot_liv_tag.val()); ++m)
            setFileINIString(section, QStringLiteral("fun_mot%1").arg(m), bit(values.fun_mot_liv(i).fun_mot_mot(m)));
    }
}

// liv_Click, once ver_ese_ope, manual mode and the empty list are satisfied.
void AbilitazioniModel::switchLevel(const QString &key, int level)
{
    par_abi_ute = par_abi;
    const bool on = isOn(key);
    showSwitch(key, on);
    par_abi_ute.liv(level) = on;
    if (!on) {
        for (int m = 1; m <= int(par_mac.car_tec.num_mot_liv_tag.val()); ++m) {
            par_abi_ute.fun_mot_liv(level).fun_mot_mot(m) = false;
            showSwitch(QStringLiteral("fun_mot(%1%2)").arg(level).arg(m), false);
        }
        par_abi_ute.num_mot_abi(level) = 0;
    }
    messaggio(msg(QStringLiteral("msg_inv_par_mac")), false);
    scrParAbi(true);
    m_pendingKey = key;
    if (!m_command || !m_command->esegui(PARAM_CFG_MAC, level, 0)) {
        messaggio(msg(QStringLiteral("imp_ese_ope")), true);
        par_abi_ute = par_abi;
        scrParAbi(false);
        revert(key);
    }
}

// Form_Activate: ini_CheckBox and val_Check_Box_activate
void AbilitazioniModel::activate(const QString &)
{
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    const int motors = int(par_mac.car_tec.num_mot_liv_tag.val());
    m_tabOrder.clear();
    for (int i = 1; i <= 3; ++i) {
        const QString control = QStringLiteral("liv(%1)").arg(i);
        const bool fitted = i <= levels;
        set(QStringLiteral("lab_liv(%1)").arg(i - 1), QStringLiteral("foreColor"), color(fitted ? DARK_BLUE : MEDIUM_GRAY));
        set(control, QStringLiteral("enabled"), fitted);
        set(control, QStringLiteral("modelFocus"), true);
        if (fitted) {
            showSwitch(control, par_abi.liv(i));
            m_tabOrder << control;
        } else {
            set(control, QStringLiteral("checked"), false);
            setText(control, QString());
            set(control, QStringLiteral("backColor"), color(LIGHT_GRAY));
        }
    }
    for (int m = 1; m <= 3; ++m) {
        set(QStringLiteral("lab_fun_mot(%1)").arg(m - 1), QStringLiteral("foreColor"), color(m <= motors ? StaBarModel::vbBlack : MEDIUM_GRAY));
        for (int i = 1; i <= 3; ++i) {
            const QString control = QStringLiteral("fun_mot(%1%2)").arg(i).arg(m);
            const bool fitted = m <= motors && i <= levels;
            set(control, QStringLiteral("enabled"), fitted);
            set(control, QStringLiteral("modelFocus"), true);
            if (fitted) {
                showSwitch(control, par_abi.fun_mot_liv(i).fun_mot_mot(m));
                m_tabOrder << control;
            } else {
                set(control, QStringLiteral("checked"), false);
                setText(control, QString());
                set(control, QStringLiteral("backColor"), color(LIGHT_GRAY));
            }
        }
    }
    const QString pu = QStringLiteral("abi_com_pu");
    set(pu, QStringLiteral("modelFocus"), true);
    showSwitch(pu, par_abi.abi_com_PU);
    m_tabOrder << pu;
    par_abi_ute = par_abi;
    chkAbiMot();
    set(QStringLiteral("liv(1)"), QStringLiteral("focusCount"), ++m_focusCount);
    commit();
}

// chk_abi_mot: a warning while a fitted motor is switched off.
void AbilitazioniModel::chkAbiMot()
{
    bool off = false;
    for (int i = 1; i <= 3 && !off; ++i) {
        for (int m = 1; m <= 3 && !off; ++m) {
            const QString control = QStringLiteral("fun_mot(%1%2)").arg(i).arg(m);
            off = value(control, QStringLiteral("enabled")).toBool() && !isOn(control);
        }
    }
    setText(QStringLiteral("title"), Messages::instance().find(QStringLiteral("tit_war_inv_mot")));
    setText(QStringLiteral("desc1"), Messages::instance().find(QStringLiteral("des_war_inv_mot1")));
    setText(QStringLiteral("desc2"), Messages::instance().find(QStringLiteral("des_war_inv_mot2")));
    set(QStringLiteral("frm_err_abi"), QStringLiteral("visible"), off);
}

// ver_ese_ope
bool AbilitazioniModel::verEseOpe(const QString &control) const
{
    if (control == QLatin1String("abi_com_pu"))
        return true;
    if (!pro_set.ute_wrk) {
        messaggio(msg(QStringLiteral("acc_pwd")), true);
        return false;
    }
    if (!stag::sta_tag || !stag::sta_tag->comOK()) {
        messaggio(msg(QStringLiteral("com_int_con_mac")), true);
        return false;
    }
    if (!stag::sta_tag->bit(ClasseStato::MAC_F_STOP, 1)) {
        messaggio(msg(QStringLiteral("mac_non_fas_sto")), true, false);
        return false;
    }
    return true;
}

void AbilitazioniModel::revert(const QString &control)
{
    showSwitch(control, !isOn(control));
}

// liv_Click, fun_mot_Click and abi_com_PU_Click, up to the command they would send.
void AbilitazioniModel::toggled(const QString &key, const QString &control, int index)
{
    if (control == QLatin1String("abi_com_pu")) {
        par_abi.abi_com_PU = isOn(key);
        showSwitch(key, par_abi.abi_com_PU);
        setFileINIString(QStringLiteral("abilitazioni"), QStringLiteral("abi_com_pu"), par_abi.abi_com_PU ? QStringLiteral("1") : QStringLiteral("0"));
        return;
    }
    if (!verEseOpe(control)) {
        revert(key);
        chkAbiMot();
        return;
    }
    if (control == QLatin1String("liv")) {
        if (!stag::sta_tag->bit(ClasseStato::MAC_IN_MANUALE, 1))
            messaggio(msg(QStringLiteral("imp_ese_ope")) + QStringLiteral(". ") + msg(QStringLiteral("mac_in_auto")), true, false);
        else if (m_orders->Count() != 0)
            messaggio(msg(QStringLiteral("imp_ese_ope")) + QStringLiteral(". ") + msg(QStringLiteral("lis_ord_non_vuo")), true, false);
        else {
            switchLevel(key, index);
            chkAbiMot();
            return;
        }
    } else {
        const int level = index / 10;
        if (!par_abi.liv(level))
            messaggio(msg(QStringLiteral("abi_il_liv")) + QStringLiteral(" %1").arg(level), true, false);
        else
            messaggio(msg(QStringLiteral("imp_ese_ope")) + QStringLiteral(" (PLC)"), true, false);
    }
    revert(key);
    chkAbiMot();
}

// tim_frm_abi_Timer
void AbilitazioniModel::blink()
{
    if (!value(QStringLiteral("frm_err_abi"), QStringLiteral("visible")).toBool())
        return;
    m_blink = !m_blink;
    const QString shown = color(m_blink ? 0xC00000 : 0x0000C0);
    for (const char *name : {"title", "desc1", "desc2"})
        set(QLatin1String(name), QStringLiteral("foreColor"), shown);
    commit();
}

void AbilitazioniModel::controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args)
{
    if (control == QLatin1String("tim_frm_abi")) {
        if (event == QLatin1String("Timer"))
            blink();
        return;
    }
    const QString key = index >= 0 ? QStringLiteral("%1(%2)").arg(control).arg(index) : control;
    if (!m_tabOrder.contains(key))
        return;
    if (event == QLatin1String("Click")) {
        set(key, QStringLiteral("checked"), args.value(QStringLiteral("checked")));
        toggled(key, control, index);
        commit();
    } else if (event == QLatin1String("KeyDown")) {
        const int pressed = args.value(QStringLiteral("key")).toInt();
        const int count = int(m_tabOrder.size());
        const int position = int(m_tabOrder.indexOf(key));
        QString next;
        if (pressed == Qt::Key_Right || pressed == Qt::Key_Tab)
            next = m_tabOrder.at((position + 1) % count);
        else if (pressed == Qt::Key_Left || pressed == Qt::Key_Backtab)
            next = m_tabOrder.at((position - 1 + count) % count);
        else
            return;
        set(next, QStringLiteral("focusCount"), ++m_focusCount);
        commit();
    }
}
