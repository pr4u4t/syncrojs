#pragma once

#include "formmodel.h"

#include <QTimer>

class StaBarModel;

// sta_bar.frm on its generated form: what StaBarModel keeps goes onto the controls, plus what only this
// form has: counters of the level, clock, panel of messag_win and the SPDE leds.
class StaBarFormModel : public FormModel
{
    Q_OBJECT

public:
    explicit StaBarFormModel(StaBarModel *bar, QObject *parent = nullptr);

    void activate(const QString &previousForm) override;

    void eveTagErr(int liv, const QString &err);
    void eveTagNum(int liv, const QString &mis);
    void eveTagNumErr(int liv, const QString &mis);
    // del_all and eve_ord_mac_Refresh with an empty list.
    void clearCounters();
    // messag_win: the panel over the speeds, or over the mode with abiMot.
    void messagWin(const QString &text, bool abiMot);
    const QString &messagWinText() const { return m_panel; }
    // rx_dat, tra_dat_spde, tim_com_spde
    void ledSpdeRx();
    void ledSpdeTx();
    void ledSpdeLost();
    // The block of tim_all_Timer about the production report.
    void checkProductionReport();

private:
    void publish();
    void tickClock();

    StaBarModel *m_bar;
    QTimer m_clock;
    QString m_panel;
    QString m_oldAbiRegRepPro;
    quint32 m_ledRx = 0x008000;
    quint32 m_ledTx = 0x008000;
    bool m_txToggle = false;
};
