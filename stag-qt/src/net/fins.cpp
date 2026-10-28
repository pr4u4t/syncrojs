#include "fins.h"

namespace fins {
namespace {

void appendBe32(QByteArray &out, quint32 value)
{
    out.append(char(value >> 24));
    out.append(char(value >> 16));
    out.append(char(value >> 8));
    out.append(char(value));
}

quint32 be32(const QByteArray &bytes, int offset)
{
    return quint32(quint8(bytes[offset])) << 24 | quint32(quint8(bytes[offset + 1])) << 16
        | quint32(quint8(bytes[offset + 2])) << 8 | quint32(quint8(bytes[offset + 3]));
}

QByteArray commandFrame(quint8 serverNode, quint8 clientNode, quint8 sid, quint8 mrc, quint8 src)
{
    QByteArray b(12, '\0');
    b[0] = char(0x80);  // ICF
    b[2] = char(0x02);  // GCT
    b[4] = char(serverNode);
    b[7] = char(clientNode);
    b[9] = char(sid);
    b[10] = char(mrc);
    b[11] = char(src);
    return b;
}

QByteArray packet(const QByteArray &frame)
{
    return tcpHeader(FrameSend, frame.size()) + frame;
}

QByteArray memoryArea(quint8 serverNode, quint8 clientNode, quint8 sid, bool write, quint16 offset, quint16 words)
{
    QByteArray b = commandFrame(serverNode, clientNode, sid, 1, write ? 2 : 1);
    b.append(char(AreaDM));
    b.append(char(offset >> 8));
    b.append(char(offset));
    b.append('\0');
    b.append(char(words >> 8));
    b.append(char(words));
    return b;
}

} // namespace

QByteArray tcpHeader(quint8 command, int payloadLength, quint8 error)
{
    QByteArray b("FINS");
    appendBe32(b, quint32(8 + payloadLength));
    appendBe32(b, command);
    appendBe32(b, error);
    return b;
}

QByteArray nodeAddressRequest()
{
    return tcpHeader(NodeAddressRequest, 4) + QByteArray(4, '\0');
}

QByteArray nodeAddressResponse(quint8 clientNode, quint8 serverNode)
{
    QByteArray b = tcpHeader(NodeAddressResponse, 8);
    appendBe32(b, clientNode);
    appendBe32(b, serverNode);
    return b;
}

QByteArray memoryAreaRead(quint8 serverNode, quint8 clientNode, quint8 sid, quint16 offset, quint16 words)
{
    return packet(memoryArea(serverNode, clientNode, sid, false, offset, words));
}

QByteArray memoryAreaWrite(quint8 serverNode, quint8 clientNode, quint8 sid, quint16 offset, const QVector<qint32> &values)
{
    QByteArray b = memoryArea(serverNode, clientNode, sid, true, offset, quint16(values.size()));
    for (qint32 v : values) {
        const quint16 w = quint16((v >= 0 ? v : 65536 + v) & 0xFFFF);
        b.append(char(w >> 8));
        b.append(char(w));
    }
    return packet(b);
}

QByteArray runModeRead(quint8 serverNode, quint8 clientNode, quint8 sid)
{
    return packet(commandFrame(serverNode, clientNode, sid, 6, 1));
}

QByteArray setMonitorMode(quint8 serverNode, quint8 clientNode, quint8 sid)
{
    QByteArray b = commandFrame(serverNode, clientNode, sid, 4, 1);
    b.append(char(0xFF));
    b.append(char(0xFF));
    b.append(char(0x02));
    return packet(b);
}

bool takePackets(QByteArray &buffer, QList<QByteArray> &packets)
{
    while (buffer.size() >= 8) {
        if (!buffer.startsWith("FINS")) {
            buffer.clear();
            return false;
        }
        const quint32 length = be32(buffer, 4);
        if (length < 8 || length > MaxPacketSize - 8) {
            buffer.clear();
            return false;
        }
        const int total = int(length) + 8;
        if (buffer.size() < total)
            break;
        packets.append(buffer.left(total));
        buffer.remove(0, total);
    }
    return true;
}

quint16 word(const QByteArray &bytes, int offset)
{
    return quint16(quint8(bytes[offset]) << 8 | quint8(bytes[offset + 1]));
}

} // namespace fins
