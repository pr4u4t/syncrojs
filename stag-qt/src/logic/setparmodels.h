#pragma once

#include "formmodel.h"
#include "plclink.h"
#include "schtagpages.h"

#include <QStringList>

#include <functional>

class ClasseStato;

// What set_par_rul and set_par_com share: F2/ESC edit mode and Return/arrows as Tab over TabIndex order.
class SetParFormModel : public FormModel
{
    Q_OBJECT

public:
    using FormModel::FormModel;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;

protected:
    void focus(const QString &control);
    QStringList m_tab;
    int m_focusCount = 0;
};

// set_par_rul. The values go to the drives over CAN before they are saved. CAN is not ported, so without a
// sender every drive answers as one that does not respond: "Error", ope_eff_par and the old values, as VB
// does when the bus is down.
class SetParRulModel : public SetParFormModel
{
    Q_OBJECT

public:
    explicit SetParRulModel(ClasseStato *state, QObject *parent = nullptr);

    // snd_par_rul_ing (entry true, index 1..2) and snd_par_rul_usc (entry false, level): true when sent.
    using DriveSender = std::function<bool(bool entry, int index)>;
    void setDriveSender(DriveSender sender) { m_sender = std::move(sender); }
    void setCommand(Rmt *command);

    void activate(const QString &previousForm) override;
    bool functionKey(const QString &key, int shift) override;

private:
    void filParRul();
    void iniTxtLab();
    void scrParUte();
    void lock();
    bool sndParAzi();
    QString clampFlap(const QString &control);
    bool sendOne(bool entry, int index);

    ClasseStato *m_state;
    Rmt *m_command = nullptr;
    DriveSender m_sender;
    bool m_mod = false;
};

// set_par_com. The serial ports and CAN are not ported, so changes of theirs are only saved; the cut boards
// take the new sockets at their next frame.
class SetParComModel : public SetParFormModel
{
    Q_OBJECT

public:
    SetParComModel(PlcLink *link, ClasseStato *state, QObject *parent = nullptr);

    void activate(const QString &previousForm) override;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;
    bool functionKey(const QString &key, int shift) override;

signals:
    // ind_plc.newplc
    void plcAddressChanged();

private:
    void iniCombo();
    void gesCombo();
    void filParCom();
    void scrParUte();
    bool porteChg() const;
    bool porteOk() const;
    void lock();
    void selectText(const QString &control, const QString &text);
    QString comboText(const QString &control) const;
    int listIndex(const QString &control) const { return value(control, QStringLiteral("listIndex")).toInt(); }

    PlcLink *m_link;
    ClasseStato *m_state;
    bool m_mod = false;
};
