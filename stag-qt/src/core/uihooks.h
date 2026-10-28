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

} // namespace stag
