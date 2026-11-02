#pragma once

#include "collezioneordini.h"
#include "formmodel.h"
#include "plclink.h"

#include <QSqlDatabase>
#include <QStringList>

// abilitazioni.frm. A level goes to the PLC through rmt; a motor goes to its cut board, or with no
// configurations in the table straight to the PLC with a new cutting curve.
class AbilitazioniModel : public FormModel
{
    Q_OBJECT

public:
    explicit AbilitazioniModel(CollezioneOrdini *orders, QObject *parent = nullptr);

    // rmt_abilitazioni_plc
    void setCommand(Rmt *command);
    // rmt_abilitazioni_sch and rmt_abilitazioni_macro
    void setMotorCommands(Rmt *board, Rmt *macro, PlcLink *link, QSqlDatabase db);
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
    void switchMotor(const QString &key, int index);
    void checkDisMot();
    void rollBackMotor();
    void scrParAbi(bool useUser);

    CollezioneOrdini *m_orders;
    Rmt *m_command = nullptr;
    Rmt *m_board = nullptr;
    Rmt *m_macro = nullptr;
    PlcLink *m_link = nullptr;
    QSqlDatabase m_db;
    bool m_plcRollBack = false;
    QString m_pendingKey;
    QStringList m_tabOrder;
    int m_focusCount = 0;
    bool m_blink = false;
};
