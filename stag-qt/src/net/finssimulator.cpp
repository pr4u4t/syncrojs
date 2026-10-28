#include "finssimulator.h"

#include "fins.h"

#include <QTcpSocket>

namespace {

constexpr int DmWords = 32768;

QByteArray responseFrame(const QByteArray &request, quint16 endCode, const QByteArray &data = {})
{
    const QByteArray f = request.mid(fins::FrameOffset, 12);
    QByteArray r(12, '\0');
    r[0] = char(0xC0);
    r[2] = char(0x02);
    r[3] = f[6];    // DNA <- SNA
    r[4] = f[7];    // DA1 <- SA1
    r[5] = f[8];    // DA2 <- SA2
    r[6] = f[3];    // SNA <- DNA
    r[7] = f[4];    // SA1 <- DA1
    r[8] = f[5];    // SA2 <- DA2
    r[9] = f[9];    // SID
    r[10] = f[10];  // MRC
    r[11] = f[11];  // SRC
    r.append(char(endCode >> 8));
    r.append(char(endCode));
    r.append(data);
    return fins::tcpHeader(fins::FrameSend, r.size()) + r;
}

} // namespace

FinsSimulator::FinsSimulator(quint8 serverNode, QObject *parent)
    : QObject(parent)
    , m_dm(DmWords, 0)
    , m_serverNode(serverNode)
{
    connect(&m_server, &QTcpServer::newConnection, this, &FinsSimulator::onNewConnection);
}

FinsSimulator::~FinsSimulator()
{
    for (QTcpSocket *socket : m_server.findChildren<QTcpSocket *>()) {
        socket->disconnect(this);
        delete socket;
    }
}

bool FinsSimulator::listen(const QHostAddress &address, quint16 port)
{
    return m_server.listen(address, port);
}

void FinsSimulator::setDm(int address, quint16 value)
{
    if (address >= 0 && address < DmWords)
        m_dm[address] = value;
}

void FinsSimulator::setDm(int address, const QVector<quint16> &values)
{
    for (int i = 0; i < values.size(); ++i)
        setDm(address + i, values[i]);
}

void FinsSimulator::onNewConnection()
{
    while (QTcpSocket *socket = m_server.nextPendingConnection()) {
        connect(socket, &QTcpSocket::readyRead, this, [this, socket] { onReadyRead(socket); });
        connect(socket, &QTcpSocket::disconnected, this, [this, socket] {
            m_buffers.remove(socket);
            socket->deleteLater();
        });
    }
}

void FinsSimulator::onReadyRead(QTcpSocket *socket)
{
    QByteArray &buffer = m_buffers[socket];
    buffer.append(socket->readAll());
    QList<QByteArray> packets;
    if (!fins::takePackets(buffer, packets)) {
        socket->abort();
        return;
    }
    for (const QByteArray &p : packets) {
        const QByteArray reply = answer(socket, p);
        if (!reply.isEmpty())
            socket->write(reply);
    }
}

QByteArray FinsSimulator::answer(QTcpSocket *socket, const QByteArray &p)
{
    Q_UNUSED(socket);
    if (p.size() < fins::TcpHeaderSize)
        return {};
    const quint8 command = quint8(p[11]);
    if (command == fins::NodeAddressRequest) {
        const quint8 client = m_nextClientNode++;
        if (m_nextClientNode == m_serverNode || m_nextClientNode == 0)
            ++m_nextClientNode;
        return fins::nodeAddressResponse(client, m_serverNode);
    }
    if (command != fins::FrameSend || p.size() < fins::FrameOffset + 12 || m_muted)
        return {};

    const quint8 mrc = quint8(p[26]);
    const quint8 src = quint8(p[27]);
    if (mrc == 1 && (src == 1 || src == 2)) {
        if (p.size() < 34 || quint8(p[28]) != fins::AreaDM)
            return responseFrame(p, 0x1101);
        const int start = fins::word(p, 29);
        const int words = fins::word(p, 32);
        if (start + words > DmWords)
            return responseFrame(p, 0x1103);
        if (src == 1) {
            QByteArray data;
            for (int i = 0; i < words; ++i) {
                data.append(char(m_dm[start + i] >> 8));
                data.append(char(m_dm[start + i]));
            }
            return responseFrame(p, 0, data);
        }
        if (p.size() < 34 + words * 2)
            return responseFrame(p, 0x1001);
        QVector<quint16> values(words);
        for (int i = 0; i < words; ++i)
            values[i] = m_dm[start + i] = fins::word(p, 34 + i * 2);
        emit dmWritten(start, values);
        return responseFrame(p, 0);
    }
    if (mrc == 4 && src == 1) {
        if (p.size() >= 31)
            m_mode = quint8(p[30]);
        return responseFrame(p, 0);
    }
    if (mrc == 6 && src == 1) {
        QByteArray data(26, '\0');
        data[0] = char(0x01);  // running
        data[1] = char(m_mode);
        return responseFrame(p, 0, data);
    }
    return responseFrame(p, 0x0401);
}
