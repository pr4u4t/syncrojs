#include "finsclient.h"

#include "fins.h"

#include <QElapsedTimer>
#include <QEventLoop>

FinsClient::FinsClient(QObject *parent)
    : QObject(parent)
{
    m_timer.setInterval(m_timeOut * 2);
    connect(&m_timer, &QTimer::timeout, this, &FinsClient::onTimer);
    connect(&m_socket, &QTcpSocket::readyRead, this, &FinsClient::onReadyRead);
    connect(&m_socket, &QTcpSocket::disconnected, this, [this] { m_socket.abort(); });
    connect(&m_socket, &QTcpSocket::stateChanged, this, &FinsClient::statusChanged);
}

FinsClient::~FinsClient()
{
    m_socket.disconnect(this);
}

void FinsClient::setTimeOut(int milliseconds)
{
    m_timeOut = milliseconds;
    m_timer.setInterval(milliseconds * 2);
}

void FinsClient::setEnabled(bool enabled)
{
    if (enabled)
        m_timer.start();
    else
        m_timer.stop();
}

bool FinsClient::error() const
{
    return notReady() || m_lastError;
}

bool FinsClient::notReady() const
{
    return m_socket.state() != QAbstractSocket::ConnectedState || m_serverNode == 0;
}

void FinsClient::closeSocket()
{
    m_socket.abort();
    m_buffer.clear();
}

void FinsClient::incSid()
{
    m_sid = m_sid == 255 ? 0 : quint8(m_sid + 1);
}

qint32 FinsClient::readArea(quint16 offset, quint16 words)
{
    m_readValue = QVector<qint32>(words, 0);
    if (notReady())
        return -1;
    incSid();
    const qint32 ret = transact(fins::memoryAreaRead(m_serverNode, m_clientNode, m_sid, offset, words));
    if (!m_lastError)
        m_retData = m_readValue;
    return ret;
}

qint32 FinsClient::writeArea(quint16 offset, const QVector<qint32> &values)
{
    if (notReady())
        return -1;
    incSid();
    return transact(fins::memoryAreaWrite(m_serverNode, m_clientNode, m_sid, offset, values));
}

qint32 FinsClient::runMode()
{
    if (notReady())
        return -1;
    incSid();
    return transact(fins::runModeRead(m_serverNode, m_clientNode, m_sid));
}

qint32 FinsClient::setMonitor()
{
    if (notReady())
        return -1;
    incSid();
    return transact(fins::setMonitorMode(m_serverNode, m_clientNode, m_sid));
}

qint32 FinsClient::transact(const QByteArray &packet)
{
    m_activeCmd = fins::FrameSend;
    m_socket.write(packet);

    QElapsedTimer elapsed;
    elapsed.start();
    m_wait = true;
    QEventLoop loop;
    QTimer tick;
    tick.setInterval(1);
    connect(&tick, &QTimer::timeout, &loop, [&] {
        if (!m_wait || elapsed.elapsed() >= m_timeOut)
            loop.quit();
    });
    tick.start();
    loop.exec();

    if (!m_wait) {
        m_lastError = false;
        emit statusChanged();
        return m_retValue;
    }
    m_lastError = true;
    emit statusChanged();
    return -1;
}

void FinsClient::onTimer()
{
    switch (m_socket.state()) {
    case QAbstractSocket::UnconnectedState:
        m_clientNode = 0;
        m_serverNode = 0;
        m_buffer.clear();
        m_socket.connectToHost(m_host, m_port);
        break;
    case QAbstractSocket::ConnectedState:
        if (m_serverNode == 0) {
            m_activeCmd = fins::NodeAddressResponse;
            m_socket.write(fins::nodeAddressRequest());
        }
        if (m_lastError) {
            closeSocket();
            m_lastError = false;
            emit statusChanged();
        }
        break;
    default:
        closeSocket();
        break;
    }
}

void FinsClient::onReadyRead()
{
    m_buffer.append(m_socket.readAll());
    QList<QByteArray> packets;
    if (!fins::takePackets(m_buffer, packets))
        emit logMessage(QStringLiteral("Discarded corrupt FINS stream"));
    for (const QByteArray &p : packets)
        handlePacket(p);
}

void FinsClient::handlePacket(const QByteArray &b)
{
    if (b.size() < fins::TcpHeaderSize)
        return;
    const quint8 command = quint8(b[11]);
    const quint8 errorCode = quint8(b[15]);
    if (errorCode != 0) {
        emit logMessage(QStringLiteral("Error number %1 on command %2").arg(errorCode).arg(command));
        return;
    }
    if (command != m_activeCmd) {
        emit logMessage(QStringLiteral("Wrong answer: command %1, answer to command %2").arg(m_activeCmd).arg(command));
        return;
    }
    switch (command) {
    case fins::NodeAddressResponse:
        if (b.size() >= 24) {
            m_serverNode = quint8(b[23]);
            m_clientNode = quint8(b[19]);
            emit statusChanged();
        }
        break;
    case fins::FrameSend: {
        if (b.size() < fins::ResponseDataOffset)
            return;
        if (quint8(b[23]) != m_serverNode) {
            emit logMessage(QStringLiteral("Received data from Wrong ServerNode %1").arg(quint8(b[23])));
            return;
        }
        if (quint8(b[25]) != m_sid) {
            emit logMessage(QStringLiteral("Received reply with wrong sid %1 instead of %2").arg(quint8(b[25])).arg(m_sid));
            return;
        }
        m_wait = false;
        const quint8 mrc = quint8(b[26]);
        const quint8 src = quint8(b[27]);
        if (b[28] != 0 || b[29] != 0) {
            emit logMessage(QStringLiteral("Error code%1%2 returned to command %3%4")
                                .arg(quint8(b[28])).arg(quint8(b[29])).arg(mrc).arg(src));
            m_retValue = -1;
            return;
        }
        m_retValue = 0;
        if (mrc == 1 && src == 1) {
            const int words = (b.size() - fins::ResponseDataOffset) / 2;
            m_readValue = QVector<qint32>(words);
            for (int k = 0; k < words; ++k)
                m_readValue[k] = fins::word(b, fins::ResponseDataOffset + k * 2);
            m_retValue = words > 0 ? m_readValue[0] : 0;
        } else if (mrc == 6 && src == 1) {
            m_retValue = b.size() > 31 ? quint8(b[31]) : 0;
        }
        break;
    }
    case fins::FrameError:
        emit logMessage(QStringLiteral("Received error frame: error %1").arg(errorCode));
        break;
    default:
        emit logMessage(QStringLiteral("Unpredicted answer, command %1").arg(command));
        break;
    }
}
