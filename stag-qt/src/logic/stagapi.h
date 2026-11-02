#pragma once

#include <QByteArray>
#include <QObject>
#include <QTcpServer>

#include <functional>

class PagProModel;
class StaBarModel;

// [Api] of stag.ini: what the web page reads, the way it reads Syncro. One command per connection, the answer
// and the close. Only reads: nothing that comes in here reaches the PLC, the orders or the settings.
class StagApi : public QObject
{
    Q_OBJECT

public:
    using Snapshot = std::function<QByteArray()>;

    explicit StagApi(Snapshot status, QObject *parent = nullptr);

    bool listen(const QHostAddress &address, quint16 port);
    quint16 port() const { return m_server.serverPort(); }
    QString errorString() const { return m_server.errorString(); }

private:
    void accept();

    Snapshot m_status;
    QTcpServer m_server;
    int m_clients = 0;
};

// The answer to Stag.Status: what the status bar and the production page show, in the language of the program.
QByteArray stagStatusJson(const StaBarModel &staBar, const PagProModel &pagPro);
