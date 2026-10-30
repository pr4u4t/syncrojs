#pragma once

#include "collezioneordini.h"
#include "formmodel.h"
#include "plclink.h"

// pag_pro.frm: current and next order on the main page.
class PagProModel : public FormModel
{
    Q_OBJECT

public:
    explicit PagProModel(CollezioneOrdini *orders, QObject *parent = nullptr);
    // rmt_plc_pag_pro
    void setCommand(Rmt *command);

    void activate(const QString &) override { refresh(); }
    bool functionKey(const QString &key, int shift) override;
    QString navigationTarget(const QString &key, int shift) override;
    // eve_ord_mac_Refresh, Form_Activate
    void refresh();
    // eve_sta_ord, with ClasseStato::StaOrd and ClasseStato::TipOrd values
    void orderStatus(int event, int order);

private:
    void showOrder(const QString &prefix, const OrdinePtr &order);
    void setColor(int index, const QString &text, quint32 color);

    bool sendCommand(int request, int order);

    CollezioneOrdini *m_orders;
    Rmt *m_command = nullptr;
};
