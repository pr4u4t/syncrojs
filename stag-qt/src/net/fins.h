#pragma once

#include <QByteArray>
#include <QList>
#include <QVector>

// Omron FINS over TCP, frame layout as built by stag/Fins.cls.
namespace fins {

constexpr quint16 DefaultPort = 9600;
constexpr int TcpHeaderSize = 16;
constexpr int FrameOffset = TcpHeaderSize;      // ICF of the FINS frame
constexpr int ResponseDataOffset = 30;          // first byte after MRES/SRES
constexpr quint8 AreaDM = 0x82;
constexpr int MaxPacketSize = 8 + 4096;

enum TcpCommand : quint8 {
    NodeAddressRequest = 0,
    NodeAddressResponse = 1,
    FrameSend = 2,
    FrameError = 3,
};

QByteArray tcpHeader(quint8 command, int payloadLength, quint8 error = 0);
QByteArray nodeAddressRequest();
QByteArray nodeAddressResponse(quint8 clientNode, quint8 serverNode);

// VB: Fins.DM(offset, siz, v, wrt). Values are VB Longs; negatives are stored as 65536 + v.
QByteArray memoryAreaRead(quint8 serverNode, quint8 clientNode, quint8 sid, quint16 offset, quint16 words);
QByteArray memoryAreaWrite(quint8 serverNode, quint8 clientNode, quint8 sid, quint16 offset, const QVector<qint32> &values);
QByteArray runModeRead(quint8 serverNode, quint8 clientNode, quint8 sid);
QByteArray setMonitorMode(quint8 serverNode, quint8 clientNode, quint8 sid);

// Removes every complete packet from the stream buffer. Returns false when the stream is corrupt.
bool takePackets(QByteArray &buffer, QList<QByteArray> &packets);

quint16 word(const QByteArray &bytes, int offset);

} // namespace fins
