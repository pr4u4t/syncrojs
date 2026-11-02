#pragma once

#include "collezioneordini.h"

#include <QElapsedTimer>
#include <QObject>
#include <QSqlDatabase>
#include <QString>

class ClasseStato;
class QUdpSocket;

// The link with the plant supervisor: rx_dat and ric_dat_spde of sta_bar and spde_bas, in the Fosber 3.0
// protocol over UDP that the machine is set to. The other protocols and the serial line are not ported.
// Nothing is opened without setEnabled(true): a received order changes the order list.
class SpdeLink : public QObject
{
    Q_OBJECT

public:
    SpdeLink(CollezioneOrdini *orders, CollezioneOrdini *executed, ClasseStato *state, QSqlDatabase db, QObject *parent = nullptr);

    void setEnabled(bool enabled);
    bool enabled() const { return m_enabled; }

    // tim_com_spde every 300 ms, Tim_com_spde_lan every second.
    void tick();
    // rx_dat with what arrived.
    void rxDat(const QString &received);

    // LRC_FCS and tx_to_spde.
    static QString lrc(const QString &message);
    static QString frame(const QString &message);
    // rie_sta_rec_30 and rie_ord_ese_rec_30: without their record letter.
    QString statusRecord() const;
    QString executedRecord() const;

signals:
    void sent(const QString &frame);
    void received();
    void lost();

private:
    void open();
    void ricDatSpde(const QString &command);
    void traDatSpde(const QString &frame);
    void refuse(QChar record, int code, const QString &logText, bool showRefused = false, int position = 0);
    void receiveOrder(const QString &data);
    void eraseOrder(const QString &data);
    bool addOnda(ClasseOrdine &order);
    void fillOrder(ClasseOrdine &order, const QString &data) const;

    CollezioneOrdini *m_orders;
    CollezioneOrdini *m_executed;
    ClasseStato *m_state;
    QSqlDatabase m_db;
    bool m_enabled = false;
    QUdpSocket *m_socket = nullptr;
    QString m_arr;
    QString m_rx;
    QElapsedTimer m_lastRx;
    QElapsedTimer m_keepAlive;
    int m_ini = 0;
    int m_contIni = 0;
};
