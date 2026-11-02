#pragma once

#include <QString>

#include <functional>

// Calls from the business modules into the forms, wired by the application.
namespace stag {

// uti_stag.messaggio without vis_for: sta_bar.lab_msg.
inline std::function<void(const QString &text, bool isAll, bool stay)> messaggioHook;

inline void messaggio(const QString &text, bool isAll, bool stay = false)
{
    if (messaggioHook)
        messaggioHook(text, isAll, stay);
}

// messaggio with vis_for: pag_msg, one line per level; without it, lab_msg as usual.
inline std::function<void(const QString &text, bool isAll, int liv)> messaggioLivHook;

inline void messaggioLiv(const QString &text, bool isAll, int liv)
{
    if (messaggioLivHook)
        messaggioLivHook(text, isAll, liv);
    else
        messaggio(text, isAll);
}

// messag_win: the panel of sta_bar.
inline std::function<void(const QString &text, bool abiMot)> messagWinHook;
inline std::function<QString()> messagWinTextHook;

inline void messagWin(const QString &text, bool abiMot = false)
{
    if (messagWinHook)
        messagWinHook(text, abiMot);
    else if (!text.isEmpty())
        messaggio(text, true, true);
}

// The If of check_dis_mot and conferma_cfg_all_liv: clears the panel only when it shows this text.
inline void clearMessagWin(const QString &text)
{
    if (messagWinTextHook && messagWinTextHook() == text)
        messagWin(QString(), true);
}

// chg_lin: the forms take their captions again.
inline std::function<void()> languageChangedHook;

} // namespace stag
