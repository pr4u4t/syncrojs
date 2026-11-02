#include "schtaglink.h"

#include "parmac.h"
#include "staglog.h"
#include "vbcompat.h"
#include "vbtypes.h"

#include <QHostAddress>
#include <QTimer>
#include <QUdpSocket>

using namespace stag;

static constexpr int COM_SCH_TAG_LEN_BCC = 2;
static constexpr int TIP_COM_LAN = 1;

SchTagLink::SchTagLink(CollezioneOrdini *orders, QObject *parent)
    : QObject(parent)
    , m_orders(orders)
{
}

QUdpSocket *SchTagLink::socket(int liv)
{
    QUdpSocket *&udp = m_sockets[size_t(liv)];
    if (udp)
        return udp;
    if (int(par_mac.tip_com_sch_tag(liv).val()) != TIP_COM_LAN || !par_mac.lan_sch_tag_UDP(liv) || par_mac.lan_sch_tag(liv).ind_rem.trimmed().isEmpty()) {
        log_str(QStringLiteral("snd_str_sch_tag: livello %1 non configurato per UDP").arg(liv), MED_PRIORITY, CosLog::SchTag);
        return nullptr;
    }
    auto *created = new QUdpSocket(this);
    if (!created->bind(QHostAddress::AnyIPv4, quint16(par_mac.lan_sch_tag(liv).por_loc.val()))) {
        log_str(QStringLiteral("snd_str_sch_tag: errore apertura porta locale livello %1: ").arg(liv) + created->errorString(), MED_PRIORITY, CosLog::SchTag);
        delete created;
        return nullptr;
    }
    connect(created, &QUdpSocket::readyRead, this, [this, created, liv] {
        while (created->hasPendingDatagrams()) {
            QByteArray data(int(created->pendingDatagramSize()), Qt::Uninitialized);
            created->readDatagram(data.data(), data.size());
            rx(QString::fromLatin1(data), liv);
        }
    });
    udp = created;
    return udp;
}

void SchTagLink::closeSockets()
{
    for (QUdpSocket *&udp : m_sockets) {
        delete udp;
        udp = nullptr;
    }
}

int SchTagLink::snd(int liv, int cmd, int indice, int contrast)
{
    m_cmdAtt = cmd;
    switch (cmd) {
    case CONFIGURA: {
        const auto parts = boardConfig(liv, m_orders->Count() > 0 ? m_orders->item(1).get() : nullptr);
        // The outcome of the first part is overwritten by the second one, in VB too.
        sndStr(liv, boardWriteVector(IND_INI_SCR_PAR, parts[0]));
        return sndStr(liv, boardWriteVector(IND_INI_SCR_PAR_DIV, parts[1]));
    }
    case LETTURA_STATO:
        m_receive = RcvDiagnostics;
        return sndStr(liv, boardReadVector(IND_INI_LET_PAR_STA, NUM_WRD_STA));
    case INFORMAZIONI:
        m_receive = RcvInform;
        return sndStr(liv, boardReadVector(IND_INI_LET_PAR_INF, NUM_PAR_INF));
    case LETTURA_STATO_ORDINE:
        m_receive = RcvOrderState;
        return sndStr(liv, boardReadVector(IND_INI_LET_ID_ORD, NUM_WRD_ID_ORD));
    case INVIO_DATI_ORDINE: {
        if (indice <= 0 || indice > m_orders->Count()) {
            log_str(QStringLiteral("snd_sch_tag - il parametro 'indice' non è valido = %1").arg(indice), MED_PRIORITY, CosLog::SchTag);
            return RET_BAD;
        }
        const OrdinePtr order = m_orders->item(indice);
        int i = 1;
        while (i <= 3 && order->Ord_liv(i) != liv)
            ++i;
        QString payload;
        if (i > 3) {
            log_str(QStringLiteral("snd_sch_tag - Invio ordine dummy perchè il parametro 'liv' = %1 non appartiene all'ordine con codice = ").arg(liv) + order->text(QStringLiteral("cod_ord_abb")), MED_PRIORITY, CosLog::SchTag);
            payload = boardOrderDummy(order->stag_id());
        } else if (int(vb::val(order->ord(i, QStringLiteral("cut_to_mark")))) == 2) {
            payload = boardOrder(*order, i);
        } else {
            payload = boardOrderDummy(order->stag_id());
        }
        return sndStr(liv, boardWriteVector(IND_INI_SCR_DAT_ORD, payload));
    }
    case SELEZIONE_CONTRASTO:
        if (contrast > 0 && contrast <= 4)
            return sndStr(liv, boardWriteVector(IND_INI_SCR_DAT_ORD, boardContrast(contrast)));
        log_str(QStringLiteral("snd_sch_tag - Procedura di auto apprendimento contrasto da fotocellula: il valore di contrasto indicato non è corretto = %1").arg(contrast), MED_PRIORITY, CosLog::SchTag);
        return RET_ZRO;
    default:
        log_str(QStringLiteral("snd_sch_tag - Comando sconosciuto con codice = %1").arg(cmd), MED_PRIORITY, CosLog::SchTag);
        return RET_BAD;
    }
}

int SchTagLink::sndStr(int liv, const QString &frame)
{
    if (!m_enabled)
        return RET_BAD;
    QUdpSocket *udp = socket(liv);
    if (!udp)
        return RET_BAD;
    const QHostAddress host(par_mac.lan_sch_tag(liv).ind_rem.trimmed());
    const auto port = quint16(par_mac.lan_sch_tag(liv).por_rem.val());
    TipPor &com = par_mac.com_sch_tag(liv);

    udp->writeDatagram(QByteArray(1, SYN_ST), host, port);
    for (int errors = 0; errors <= 2;) {
        udp->writeDatagram(frame.toLatin1(), host, port);
        com.com_att = true;
        com.ricevuto = false;
        log_str(frame, MED_PRIORITY, CosLog::SchTag);

        QEventLoop wait;
        m_wait = &wait;
        QTimer::singleShot(int(com.TimeOut.val()), &wait, &QEventLoop::quit);
        wait.exec();
        m_wait = nullptr;

        if (com.ricevuto)
            return RET_OK;
        ++errors;
    }
    com.com_att = false;
    return RET_BAD;
}

// rx_sch_tag. Mid$ positions are kept as in VB, the two cuts after a bad message included.
void SchTagLink::rx(const QString &received, int liv)
{
    TipPor &com = par_mac.com_sch_tag(liv);
    if (!com.com_att)
        return;
    const auto mid = [this](int from) { m_rx = from <= m_rx.size() ? m_rx.mid(from - 1) : QString(); };
    m_rx += received;
    while (!m_rx.isEmpty()) {
        const QChar first = m_rx.at(0);
        if (first == QLatin1Char(ACK_ST)) {
            com.ricevuto = true;
            com.com_att = false;
            endTransmission(liv, true);
            log_str(QStringLiteral("snd_str_sch_tag: ricevuto ACK_ST"), MED_PRIORITY, CosLog::SchTag);
            mid(2);
        } else if (first == QLatin1Char(NAK_ST)) {
            com.ricevuto = false;
            com.com_att = false;
            endTransmission(liv, false);
            log_str(QStringLiteral("snd_str_sch_tag: ricevuto NAK_ST"), MED_PRIORITY, CosLog::SchTag);
            mid(2);
        } else if (first == QLatin1Char(STX_ST)) {
            const int posEot = m_rx.indexOf(QLatin1Char(EOT_ST), 0) + 1;
            if (posEot <= 1)
                break;
            const int posNext = m_rx.indexOf(QLatin1Char(STX_ST), 1) + 1;
            if ((posNext > posEot || posNext == 0) && m_rx.size() > COM_SCH_TAG_LEN_BCC) {
                QString message = m_rx.left(posEot - 1);
                if (message.size() > COM_SCH_TAG_LEN_BCC) {
                    const QString fcs = message.right(COM_SCH_TAG_LEN_BCC);
                    message.chop(COM_SCH_TAG_LEN_BCC);
                    if (fcs == boardChecksum(message)) {
                        message = message.mid(1);
                        com.ricevuto = true;
                        com.com_att = false;
                        switch (m_receive) {
                        case RcvInform:
                            readInformation(liv, message);
                            emit informationRead(liv);
                            break;
                        case RcvDiagnostics:
                            readDiagnostics(liv, message);
                            break;
                        case RcvOrderState:
                            readOrderState(liv, message);
                            break;
                        case RcvNone:
                            break;
                        }
                        m_receive = RcvNone;
                        log_str(QStringLiteral("snd_str_sch_tag: rx = '") + m_rx + u'\'', MED_PRIORITY, CosLog::SchTag);
                    } else {
                        com.com_att = false;
                        log_str(QStringLiteral("snd_str_sch_tag: rx = '") + m_rx + QStringLiteral("' check sum errato"), MED_PRIORITY, CosLog::SchTag);
                        mid(2);
                    }
                } else {
                    com.com_att = false;
                    log_str(QStringLiteral("snd_str_sch_tag: rx = '") + m_rx + QStringLiteral("' string troppo corta"), MED_PRIORITY, CosLog::SchTag);
                    mid(2);
                }
                mid(m_rx.size() >= posEot ? posEot + 1 : m_rx.size());
            } else {
                mid(2);
            }
        } else {
            mid(2);
        }
    }
    if (m_wait && !com.com_att)
        m_wait->quit();
}

// ges_fine_trasmissione. The other cases belong to the configuration pages.
void SchTagLink::endTransmission(int liv, bool ack)
{
    if (m_cmdAtt == CONFIGURA) {
        if (m_sendCase == INVIO_INIZIALE_PAR)
            com_ini_sch_tag(liv) = ack;
        else
            emit configurationEnded(liv, m_sendCase, ack);
    }
    m_cmdAtt = CMD_NIET;
}

// rea_sta_sch_tag
void SchTagLink::readOrderState(int liv, const QString &mes)
{
    if (mes.size() != 8)
        return;
    m_idOrd[size_t(liv)][SUCCESSIVO] = mes.left(4);
    m_idOrd[size_t(liv)][CORRENTE] = mes.right(4);
    if (mes != m_oldMes[size_t(liv)]) {
        log_str(QStringLiteral("rea_sta_tag: Nuovo stato su livello %1 ID Corrente = ").arg(liv) + mes.right(4) + QStringLiteral(" ID Successivo = ") + mes.left(4)
                    + QStringLiteral(" Vecchio stato = ") + m_oldMes[size_t(liv)], MED_PRIORITY, CosLog::SchTag);
    }
    m_oldMes[size_t(liv)] = mes;
}

// rea_rx_inf
void SchTagLink::readInformation(int liv, const QString &text)
{
    QStringList words;
    for (int i = 0; i + 4 <= text.size(); i += 4)
        words << QString::number(text.mid(i, 4).toInt(nullptr, 16));
    if (words.size() < NUM_PAR_INF)
        return;
    TipParInf &inf = par_mac.sch_tag.par_inf(liv);
    inf.ver_pro_sch_tag = words[0];
    inf.ver_sch_tag = words[1];
    inf.ver_PIC = words[2];
    inf.ver_rad_qua_con_arr = words[3];
    inf.qua_bit_rad_qua_con_arr = words[4];
    inf.per_arr_rad_qua_con_arr = words[5];
    inf.ver_rad_qua_sen_arr = words[6];
    inf.qua_bit_rad_qua_sen_arr = words[7];
    par_mac_ute.sch_tag.par_inf(liv) = inf;
}

// rea_rx_dia
void SchTagLink::readDiagnostics(int liv, const QString &text)
{
    if (text.size() != 8)
        log_str(QStringLiteral("sub rea_rx_dia del modulo com_sch_di_tag_bas: la stringa ricevuta contiene più WORD di quelle attese (2)"), MED_PRIORITY, CosLog::SchTag);
    if (text.size() < 8)
        return;
    emit diagnosticsRead(liv, text.mid(0, 4), text.mid(4, 4));
}
