#pragma once

#include "formmodel.h"
#include "messages.h"
#include "stabarmodel.h"

#include <QHash>
#include <QObject>
#include <QQmlEngine>
#include <QStringList>

// What the generated forms and the shell need from the C++ side (context property "stagUi").
class FormUi : public QObject
{
    Q_OBJECT

public:
    explicit FormUi(StaBarModel *staBar, QObject *parent = nullptr)
        : QObject(parent)
        , m_staBar(staBar)
    {
    }

    void addModel(const QString &form, FormModel *model)
    {
        // Returned to QML without a parent, it would otherwise be deleted by the JS garbage collector.
        QQmlEngine::setObjectOwnership(model, QQmlEngine::CppOwnership);
        m_models.insert(form.toLower(), model);
    }

    // The ported logic of a form; null while the form still only shows its layout.
    Q_INVOKABLE QObject *formModel(const QString &form) const { return m_models.value(form.toLower()); }

    // Function key, control event, key up and Form_Activate of a form's model, kept from taking the program down.
    Q_INVOKABLE bool functionKey(QObject *model, const QString &key, int shift)
    {
        auto *form = qobject_cast<FormModel *>(model);
        return form && guardedFormCall(QStringLiteral("eve_key_") + key, [&] { return form->functionKey(key, shift); }, true);
    }
    Q_INVOKABLE void controlEvent(QObject *model, const QString &control, int index, const QString &event, const QVariantMap &args)
    {
        if (auto *form = qobject_cast<FormModel *>(model))
            guardedFormCall(control + u'_' + event, [&] { form->controlEvent(control, index, event, args); return true; }, false);
    }
    Q_INVOKABLE void keyUp(QObject *model, int key)
    {
        if (auto *form = qobject_cast<FormModel *>(model))
            guardedFormCall(QStringLiteral("Form_KeyUp"), [&] { form->keyUp(key); return true; }, false);
    }
    Q_INVOKABLE void activate(QObject *model, const QString &previous)
    {
        if (auto *form = qobject_cast<FormModel *>(model))
            guardedFormCall(QStringLiteral("Form_Activate"), [&] { form->activate(previous); return true; }, false);
    }

    // pag_pre, for the Form_Activate of the page being shown.
    Q_INVOKABLE void setPreviousForm(const QString &form) { m_previousForm = form; }
    Q_INVOKABLE QString previousForm() const { return m_previousForm; }

    Q_INVOKABLE QString controlText(const QString &form, const QString &control, int index) const
    {
        return Messages::instance().controlText(form, control, index);
    }

    // txt_shift of Show_tas_des: S, C and A on the keys that do something with Shift, Ctrl or Alt.
    Q_INVOKABLE QStringList functionMarks(const QString &form) const
    {
        QStringList marks;
        for (int key = 1; key <= 10; ++key) {
            int used = 0;
            for (int shift = 1; shift <= 7; ++shift) {
                if (!Messages::instance().find(form.toLower() + u'_' + QString::number(shift) + u' ' + QString::number(key)).isEmpty())
                    used |= shift;
            }
            marks << QString((used & 1) ? QStringLiteral("S") : QString()) + ((used & 2) ? QStringLiteral("C") : QString()) + ((used & 4) ? QStringLiteral("A") : QString());
        }
        return marks;
    }

    Q_INVOKABLE QString captionText(const QString &italian) const { return Messages::instance().captionText(italian); }

    // Show_tas_des: captions of F1-F10 for a page and the held Shift/Ctrl/Alt mask.
    Q_INVOKABLE QStringList functionLabels(const QString &form, int shift) const
    {
        QStringList labels;
        for (int key = 1; key <= 10; ++key)
            labels << Messages::instance().find(form.toLower() + u'_' + QString::number(shift) + u' ' + QString::number(key));
        return labels;
    }

signals:
    void languageChanged();
    void overlayRequested(const QString &form);

private:
    StaBarModel *m_staBar;
    QString m_previousForm;
    QHash<QString, FormModel *> m_models;
};
