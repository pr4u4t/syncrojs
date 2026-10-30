#include "pwdmodel.h"

#include "inistore.h"
#include "messages.h"
#include "parmac.h"
#include "stabarmodel.h"
#include "uihooks.h"

using namespace stag;

static const QString SECTION = QStringLiteral("PRO_SET");
// The defaults VB falls back to when the ini has no password.
static const QString CPWD = QStringLiteral("QCG");
static const QString CPWD_L1 = QStringLiteral("GQVFJX");
static constexpr quint32 COL_ALL_RED = StaBarModel::COL_ALL_Red;
static constexpr quint32 VB_BLUE = 0xFF0000;

QString PwdModel::code(const QString &text)
{
    QString result;
    for (int i = 0; i < text.size(); ++i)
        result += QChar(text.at(i).unicode() + i + 1);
    return result;
}

QString PwdModel::decodeShift(const QString &text)
{
    QString result;
    for (int i = 0; i < text.size(); ++i)
        result += QChar(text.at(i).unicode() - i - 1);
    return result.toUpper();
}

// decode: besides returning the password it writes the stored form to the ini. The name written depends
// on mainPassword, and VB's F9 path passes False for the main password, so that path saves it as Pwd_liv1.
QString PwdModel::decode(const QString &stored, bool mainPassword)
{
    const QString source = stored.isEmpty() ? (mainPassword ? CPWD : CPWD_L1) : stored;
    setFileINIString(SECTION, mainPassword ? QStringLiteral("pwd") : QStringLiteral("pwd_liv1"), source);
    return decodeShift(source);
}

void PwdModel::showPanels(bool settings, bool password, bool change)
{
    set(QStringLiteral("frm_set"), QStringLiteral("visible"), settings);
    set(QStringLiteral("frm_pwd"), QStringLiteral("visible"), password);
    set(QStringLiteral("frm_chg_pwd"), QStringLiteral("visible"), change);
}

void PwdModel::setMessage(const QString &text, quint32 color)
{
    setText(QStringLiteral("txt_msg"), text);
    set(QStringLiteral("txt_msg"), QStringLiteral("foreColor"), StaBarModel::vbColor(color));
}

void PwdModel::activate(const QString &)
{
    for (const char *name : {"txt_pwd", "txt_chg_pwd"})
        set(QLatin1String(name), QStringLiteral("modelFocus"), true);
    setMessage(QStringLiteral("password"), COL_ALL_RED);
    m_pwd.clear();
    setText(QStringLiteral("txt_pwd"), QString());
    showPanels(false, true, false);
    set(QStringLiteral("txt_pwd"), QStringLiteral("focusCount"), ++m_focusCount);
    commit();
}

void PwdModel::controlEvent(const QString &control, int, const QString &event, const QVariantMap &args)
{
    if (event == QLatin1String("Change")) {
        const QString text = args.value(QStringLiteral("text")).toString();
        if (control == QLatin1String("txt_pwd"))
            m_pwd = text;
        else if (control == QLatin1String("txt_chg_pwd"))
            m_chgPwd = text;
        else
            return;
        setText(control, text);
    } else if (event == QLatin1String("KeyDown")) {
        const int key = args.value(QStringLiteral("key")).toInt();
        if (key != Qt::Key_Return && key != Qt::Key_Enter)
            return;
        if (control == QLatin1String("txt_pwd"))
            enterPassword();
        else if (control == QLatin1String("txt_chg_pwd"))
            enterChange();
        else
            return;
    } else {
        return;
    }
    commit();
}

// txt_pwd_KeyDown
void PwdModel::enterPassword()
{
    const QString typed = m_pwd.toUpper();
    if (typed == decode(pro_set.pwd, true)) {
        setMessage(QStringLiteral("Password corretta"), VB_BLUE);
        pro_set.ute_wrk = true;
        setFileINIString(SECTION, QStringLiteral("Pwd"), pro_set.pwd);
        showPanels(true, false, false);
    } else if (typed == decode(pro_set.pwd_liv1, false)) {
        if (m_pwd.isEmpty())
            return;
        setMessage(QStringLiteral("Password corretta"), VB_BLUE);
        pro_set.ute_wrk_liv1 = true;
        setFileINIString(SECTION, QStringLiteral("pwd_liv1"), pro_set.pwd_liv1);
        emit closeRequested();
    } else if (typed.isEmpty()) {
        emit closeRequested();
    } else {
        setMessage(QStringLiteral("Password errata: riprova"), COL_ALL_RED);
        m_pwd.clear();
        setText(QStringLiteral("txt_pwd"), QString());
        pro_set.ute_wrk = false;
        showPanels(false, true, false);
        set(QStringLiteral("txt_pwd"), QStringLiteral("focusCount"), ++m_focusCount);
    }
}

void PwdModel::startChange(ChangeState state)
{
    showPanels(false, false, true);
    set(QStringLiteral("frm_chg_pwd"), QStringLiteral("y"), 0);
    askChange(QStringLiteral("Inserire la password corrente"), state);
}

void PwdModel::askChange(const QString &message, ChangeState next)
{
    setText(QStringLiteral("txt_msg_chg_pwd"), message);
    m_chgPwd.clear();
    setText(QStringLiteral("txt_chg_pwd"), QString());
    set(QStringLiteral("txt_chg_pwd"), QStringLiteral("focusCount"), ++m_focusCount);
    m_state = next;
}

// txt_chg_pwd_KeyDown
void PwdModel::enterChange()
{
    const QString typed = m_chgPwd.toUpper();
    const auto leave = [this] { showPanels(false, true, false); };
    switch (m_state) {
    case InOldPwd:
        if (typed == decode(pro_set.pwd, false))
            askChange(QStringLiteral("Inserire la nuova password"), InNewPwd);
        else
            leave();
        break;
    case InNewPwd:
        if (!m_chgPwd.isEmpty()) {
            m_newPwd = typed;
            askChange(QStringLiteral("Confermare la nuova password"), ConfNewPwd);
        } else {
            leave();
        }
        break;
    case ConfNewPwd:
        if (typed == m_newPwd) {
            pro_set.pwd = code(typed);
            setFileINIString(SECTION, QStringLiteral("Pwd"), pro_set.pwd);
            setText(QStringLiteral("txt_msg"), QStringLiteral("Nuova password memorizzata"));
            m_pwd.clear();
            setText(QStringLiteral("txt_pwd"), QString());
        }
        leave();
        break;
    case InOldPwdLiv1:
        if (typed == decode(pro_set.pwd_liv1, false)) {
            askChange(QStringLiteral("Inserire la nuova password"), InNewPwdLiv1);
        } else {
            leave();
            setMessage(QStringLiteral("Password corrente errata"), COL_ALL_RED);
        }
        break;
    case InNewPwdLiv1:
        if (!m_chgPwd.isEmpty()) {
            m_newPwdLiv1 = typed;
            askChange(QStringLiteral("Confermare la nuova password"), ConfNewPwdLiv1);
        } else {
            leave();
        }
        break;
    case ConfNewPwdLiv1:
        if (typed == m_newPwdLiv1) {
            pro_set.pwd_liv1 = code(typed);
            setFileINIString(SECTION, QStringLiteral("Pwd_Liv1"), pro_set.pwd_liv1);
            setText(QStringLiteral("txt_msg"), QStringLiteral("Nuova password memorizzata"));
            m_pwd.clear();
            setText(QStringLiteral("txt_pwd"), QString());
        }
        leave();
        break;
    }
}

bool PwdModel::functionKey(const QString &key, int shift)
{
    if (shift != 0)
        return false;
    if (key == QLatin1String("F1"))
        return !pro_set.ute_wrk;
    if (key == QLatin1String("F10")) {
        pro_set.ute_wrk = false;
        pro_set.ute_wrk_liv1 = false;
        return false;
    }
    if (key == QLatin1String("F2") || key == QLatin1String("F3") || key == QLatin1String("F4") || key == QLatin1String("F5")
        || key == QLatin1String("F6") || key == QLatin1String("F7")) {
        if (pro_set.ute_wrk)
            messaggio(QStringLiteral("Applicazione non disponibile"), false);
        return true;
    }
    if (key == QLatin1String("F8")) {
        startChange(InOldPwdLiv1);
        commit();
        return true;
    }
    if (key == QLatin1String("F9")) {
        if (pro_set.ute_wrk) {
            startChange(InOldPwd);
            commit();
        }
        return true;
    }
    return false;
}
