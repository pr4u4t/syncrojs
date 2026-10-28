#pragma once

#include <QObject>
#include <QTcpSocket>
#include <QTimer>
#include <QVector>

// Port of stag/Fins.cls: FINS/TCP client with blocking calls that keep the event loop running (VB DoEvents).
class FinsClient : public QObject
{
    Q_OBJECT
    Q_PROPERTY(bool error READ error NOTIFY statusChanged)

public:
    explicit FinsClient(QObject *parent = nullptr);
    ~FinsClient() override;

    int timeOut() const { return m_timeOut; }
    void setTimeOut(int milliseconds);

    bool isEnabled() const { return m_timer.isActive(); }
    void setEnabled(bool enabled);

    QString remoteHost() const { return m_host; }
    void setRemoteHost(const QString &host) { m_host = host; }
    quint16 remotePort() const { return m_port; }
    void setRemotePort(quint16 port) { m_port = port; }

    bool error() const;

    // Each returns -1 on failure like the VB functions.
    qint32 readArea(quint16 offset, quint16 words);
    qint32 writeArea(quint16 offset, const QVector<qint32> &values);
    qint32 runMode();
    qint32 setMonitor();

    // VB RetData: only replaced when a reply arrives, stale after a timeout.
    QVector<qint32> retData() const { return m_retData; }

    // Closes the socket; used when the VB code calls Fins.sck.Close.
    void closeSocket();

signals:
    void statusChanged();
    void logMessage(const QString &text);

private:
    bool notReady() const;
    void incSid();
    qint32 transact(const QByteArray &packet);
    void onTimer();
    void onReadyRead();
    void handlePacket(const QByteArray &b);

    QTcpSocket m_socket;
    QTimer m_timer;
    QString m_host;
    quint16 m_port = 9600;
    int m_timeOut = 1000;
    quint8 m_activeCmd = 0;
    bool m_wait = false;
    qint32 m_retValue = 0;
    QVector<qint32> m_retData;
    QVector<qint32> m_readValue;
    quint8 m_clientNode = 0;
    quint8 m_serverNode = 0;
    quint8 m_sid = 0;
    bool m_lastError = false;
    QByteArray m_buffer;
};
