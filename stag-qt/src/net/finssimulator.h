#pragma once

#include <QHash>
#include <QObject>
#include <QTcpServer>
#include <QVector>

class QTcpSocket;

// Minimal Omron CJ PLC answering the FINS/TCP commands used by STAG.
class FinsSimulator : public QObject
{
    Q_OBJECT

public:
    explicit FinsSimulator(quint8 serverNode = 30, QObject *parent = nullptr);
    ~FinsSimulator() override;

    bool listen(const QHostAddress &address = QHostAddress::Any, quint16 port = 9600);
    quint16 port() const { return m_server.serverPort(); }

    quint16 dm(int address) const { return m_dm.value(address); }
    void setDm(int address, quint16 value);
    void setDm(int address, const QVector<quint16> &values);
    quint8 mode() const { return m_mode; }

    // Simulates a PLC that stops answering FINS frames while keeping TCP open.
    void setMuted(bool muted) { m_muted = muted; }

signals:
    void dmWritten(int address, const QVector<quint16> &values);

private:
    void onNewConnection();
    void onReadyRead(QTcpSocket *socket);
    QByteArray answer(QTcpSocket *socket, const QByteArray &packet);

    QTcpServer m_server;
    QVector<quint16> m_dm;
    QHash<QTcpSocket *, QByteArray> m_buffers;
    quint8 m_serverNode;
    quint8 m_nextClientNode = 1;
    quint8 m_mode = 2;
    bool m_muted = false;
};
