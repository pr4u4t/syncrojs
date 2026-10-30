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

    // pag_pre, for the Form_Activate of the page being shown.
    Q_INVOKABLE void setPreviousForm(const QString &form) { m_previousForm = form; }
    Q_INVOKABLE QString previousForm() const { return m_previousForm; }

    Q_INVOKABLE QString controlText(const QString &form, const QString &control, int index) const
    {
        return Messages::instance().controlText(form, control, index);
    }

    // Show_tas_des: captions of F1-F10 for a page and the held Shift/Ctrl/Alt mask.
    Q_INVOKABLE QStringList functionLabels(const QString &form, int shift) const
    {
        QStringList labels;
        for (int key = 1; key <= 10; ++key)
            labels << Messages::instance().find(form.toLower() + u'_' + QString::number(shift) + u' ' + QString::number(key));
        return labels;
    }

    // Function keys whose VB behaviour has not been ported yet do nothing but say so.
    Q_INVOKABLE void notPorted(const QString &form, const QString &key)
    {
        if (m_staBar)
            m_staBar->messaggio(msg(QStringLiteral("imp_ese_ope")) + QStringLiteral(" (%1 %2)").arg(form, key), true, false);
    }

private:
    StaBarModel *m_staBar;
    QString m_previousForm;
    QHash<QString, FormModel *> m_models;
};
