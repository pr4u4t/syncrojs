#pragma once

#include "staglog.h"

#include <QObject>
#include <QVariantMap>

#include <exception>

// The handler of key.cls Ges_Key and the On Error of the forms: a VB runtime error in a form's code ends
// in the log and the program goes on. An exception crossing into QML would end the process.
template <typename Fn>
auto guardedFormCall(const QString &where, Fn &&fn, decltype(fn()) fallback)
{
    try {
        return fn();
    } catch (const std::exception &e) {
        log_str(QStringLiteral("on error intervenuta in ") + where + QStringLiteral(": ") + QString::fromUtf8(e.what()), MED_PRIORITY, CosLog::Gen);
    }
    return fallback;
}

// The VB code of a form assigns control properties (lab_ord(1).Caption = ...). A model does the
// same into state, keyed "control" or "control(index)", and VbForm copies it onto the controls.
class FormModel : public QObject
{
    Q_OBJECT
    Q_PROPERTY(QVariantMap state READ state NOTIFY stateChanged)

public:
    using QObject::QObject;

    QVariantMap state() const { return m_state; }

    // Form_Activate; previousForm is VB's pag_pre.
    Q_INVOKABLE virtual void activate(const QString &previousForm) { Q_UNUSED(previousForm) }
    // Form_KeyUp
    Q_INVOKABLE virtual void keyUp(int key) { Q_UNUSED(key) }
    // <control>_<event> of the form's controls.
    Q_INVOKABLE virtual void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args)
    {
        Q_UNUSED(control)
        Q_UNUSED(index)
        Q_UNUSED(event)
        Q_UNUSED(args)
    }
    // eve_key_<key>: true when the model handled it and the shell must not change page.
    Q_INVOKABLE virtual bool functionKey(const QString &key, int shift)
    {
        Q_UNUSED(key)
        Q_UNUSED(shift)
        return false;
    }

    // Tasti.des_fun(k).Visible = False for these keys, "F2" style, while this page is shown.
    Q_INVOKABLE virtual QStringList hiddenFunctionKeys() const { return {}; }

    QVariant value(const QString &control, const QString &property = QStringLiteral("text")) const
    {
        return m_state.value(control.toLower()).toMap().value(property);
    }

    // Where this key goes when VB chooses between targets; empty leaves it to nav.js.
    Q_INVOKABLE virtual QString navigationTarget(const QString &key, int shift)
    {
        Q_UNUSED(key)
        Q_UNUSED(shift)
        return {};
    }

signals:
    void stateChanged();
    // eve_key.chg pag_pre from inside the form's own logic.
    void closeRequested();

protected:
    void set(const QString &control, const QString &property, const QVariant &value)
    {
        const QString key = control.toLower();
        QVariantMap properties = m_state.value(key).toMap();
        properties.insert(property, value);
        m_state.insert(key, properties);
    }
    void setText(const QString &control, const QString &text) { set(control, QStringLiteral("text"), text); }
    void commit() { emit stateChanged(); }

private:
    QVariantMap m_state;
};
