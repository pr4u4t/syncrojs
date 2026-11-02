#pragma once

#include "collezioneordini.h"
#include "schtagframes.h"

#include <QElapsedTimer>
#include <QEventLoop>
#include <QObject>
#include <QString>

#include <array>

class QUdpSocket;

// snd_sch_tag, snd_str_sch_tag and rx_sch_tag over the UDP sockets of sta_bar. Like VB it waits for the
// answer inside the call, the event loop running meanwhile. Only the network mode of tip_com_sch_tag is
// here. Nothing leaves without setEnabled(true): every frame starts with SYN, which resets the slave.
class SchTagLink : public QObject
{
    Q_OBJECT

public:
    explicit SchTagLink(CollezioneOrdini *orders, QObject *parent = nullptr);

    void setEnabled(bool enabled) { m_enabled = enabled; }
    bool enabled() const { return m_enabled; }

    int snd(int liv, int cmd, int indice = 1, int contrast = 0);
    // set_porte: the sockets come back from par_mac at the next frame.
    void closeSockets();

    // sta_sch_tag
    bool comOK(int liv) const { return m_comOK[size_t(liv)]; }
    void setComOK(int liv, bool ok) { m_comOK[size_t(liv)] = ok; }
    QString idOrd(int liv, int which) const { return m_idOrd[size_t(liv)][size_t(which)]; }
    bool ftcRun() const { return m_ftcRun; }
    void setFtcRun(bool run) { m_ftcRun = run; }
    // CASO_SND_SCH_TAG
    void setSendCase(int sendCase) { m_sendCase = sendCase; }

signals:
    // ges_fine_trasmissione for the configuration pages, which are not ported yet.
    void configurationEnded(int liv, int sendCase, bool ack);
    // set_par_sch_tag4.fil_par_inf
    void informationRead(int liv);
    // dia_sch_tag.col_lab_msg_all
    void diagnosticsRead(int liv, const QString &first, const QString &second);

private:
    enum Receive { RcvNone, RcvInform, RcvDiagnostics, RcvOrderState };

    QUdpSocket *socket(int liv);
    int sndStr(int liv, const QString &frame);
    void rx(const QString &received, int liv);
    void endTransmission(int liv, bool ack);
    void readOrderState(int liv, const QString &mes);
    void readInformation(int liv, const QString &text);
    void readDiagnostics(int liv, const QString &text);

    CollezioneOrdini *m_orders;
    bool m_enabled = false;
    std::array<QUdpSocket *, 4> m_sockets{};
    std::array<bool, 4> m_comOK{};
    std::array<std::array<QString, 3>, 4> m_idOrd;
    std::array<QString, 4> m_oldMes;
    bool m_ftcRun = false;
    int m_cmdAtt = stag::CMD_NIET;
    int m_sendCase = 0;
    Receive m_receive = RcvNone;
    QString m_rx; // Static rx of rx_sch_tag: one buffer for all the levels
    QEventLoop *m_wait = nullptr;
};
