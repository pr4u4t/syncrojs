#pragma once

#include "collezioneordini.h"
#include "formmodel.h"
#include "plclink.h"

#include <QStringList>

// abilitazioni.frm. A level goes to the PLC through rmt. A motor also needs the cut boards or a new
// cutting curve, not ported yet, so motor switches keep their value and say so.
class AbilitazioniModel : public FormModel
{
    Q_OBJECT

public:
    AbilitazioniModel(CollezioneOrdini *orders, QObject *parent = nullptr);

    // rmt_abilitazioni_plc
    void setCommand(Rmt *command);
    void activate(const QString &previousForm) override;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;

    bool isOn(const QString &control) const { return value(control, QStringLiteral("checked")).toBool(); }

private:
    bool verEseOpe(const QString &control) const;
    void showSwitch(const QString &control, bool on);
    void revert(const QString &control);
    void chkAbiMot();
    void toggled(const QString &key, const QString &control, int index);
    void blink();
    void switchLevel(const QString &key, int level);
    void scrParAbi(bool useUser);

    CollezioneOrdini *m_orders;
    Rmt *m_command = nullptr;
    QString m_pendingKey;
    QStringList m_tabOrder;
    int m_focusCount = 0;
    bool m_blink = false;
};
