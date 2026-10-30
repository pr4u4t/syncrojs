#pragma once

#include "collezioneordini.h"
#include "plcframes.h"

#include <QElapsedTimer>
#include <QObject>
#include <QVariant>

#include <array>

class ClasseStato;
class FinsClient;
class PlcLink;

namespace stag {

// tip_ope_PLC
enum TipOpePlc { INVIO_ORD = 1, CANC_ORD, MODIF_ORD, PARAM_MAC, PARAM_CFG_MAC, PARAM_CUR_TAG, LETTURA_PAR_SCH_TAG,
                 SCRITTURA_PAR_SCH_TAG, AZZERAMENTO_CONTATORI, CONFERMA_DATI_SCHEDE_TAGLIO };
// tip_ope_macro
enum TipOpeMacro { PLC_PARAM_CFG_MAC_PARAM_CUR_TAG = 0, PLC_PARAM_MAC_PARAM_CFG_MAC, ACQ_CON_SCH_PLC };
// The three queues of com_plc_ute, one request each.
enum RmtChannel { ChannelPlc = 1, ChannelCards = 2, ChannelMacro = 3 };

} // namespace stag

// rmt.cls: a page's handle on one of the queues. esegui takes the queue or says it is busy; the timer
// of the PLC carries the request out and answers through retok or retbad.
class Rmt : public QObject
{
    Q_OBJECT

public:
    Rmt(const QString &name, stag::RmtChannel channel, PlcLink *link, QObject *parent = nullptr);

    bool esegui(int ric, int liv, int numOrd, const QVariant &dat = QVariant());
    void resetta();
    void rispondi(int ret, const QString &text);
    QString name() const { return m_name; }

signals:
    void retok(const QString &text);
    void retbad(const QString &error);

private:
    QString m_name;
    stag::RmtChannel m_channel;
    PlcLink *m_link;
};

// What tim_tag_plc does towards the machine once the state is read: start-up parameters, keeping the
// current and next order aligned, and carrying out the pages' requests. Nothing is written unless
// setWritesEnabled(true) was called: --enable-plc-writes on the command line.
class PlcLink : public QObject
{
    Q_OBJECT

public:
    PlcLink(FinsClient *fins, ClasseStato *state, CollezioneOrdini *orders, QObject *parent = nullptr);

    void setWritesEnabled(bool enabled) { m_writes = enabled; }
    bool writesEnabled() const { return m_writes; }

    // invio_ini_par_mac and ini_plc_lan, when the connection comes up.
    void sendStartup();
    // The part of tim_tag_plc after sta_tag.aggiorna.
    void tick();

    // inv_ord_plc: forces sending the current (1) or next (2) order again.
    void forceOrder(int which, bool force) { m_force[size_t(which)] = force; }

private:
    friend class Rmt;
    struct Queue {
        int ric = -1;
        int ret = 0;
        int liv = 0;
        int numOrd = 0;
        QVariant dat;
        Rmt *owner = nullptr;
        QElapsedTimer started;
    };

    bool send(const stag::PlcFrame &frame);
    void sendOrder(int operation, int which);
    void alignOrders();
    void servePlcQueue();
    void serveCardQueue();
    void serveMacroQueue();
    void setStartDate(int order);

    FinsClient *m_fins;
    ClasseStato *m_state;
    CollezioneOrdini *m_orders;
    std::array<Queue, 4> m_queues;
    std::array<bool, 3> m_force{};
    bool m_writes = false;
    bool m_modIdsRead = false;
    QString m_oldModCor;
    QString m_oldModSuc;
    QString m_lastLogged;
};
