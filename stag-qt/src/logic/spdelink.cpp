#include "spdelink.h"
#include "sqlcheck.h"

#include "classestato.h"
#include "fixedrecord.h"
#include "gesord.h"
#include "messages.h"
#include "parmac.h"
#include "staglog.h"
#include "stagrecords.h"
#include "uihooks.h"
#include "validazioneordini.h"
#include "vbcompat.h"
#include "vbtypes.h"

#include <QHostAddress>
#include <QSqlError>
#include <QSqlQuery>
#include <QUdpSocket>

#include <cmath>

using namespace stag;

namespace {

constexpr QChar STX_TAG(2);
constexpr QChar ETX_TAG(3);
constexpr int MAX_NUM_ORD_IN_LIS = 5;
constexpr int COM_SPDE_LEN_BCC = 2;
constexpr int COD_ERR_COM_SPDE = 1123;
constexpr int COD_ERR_CAM_ORD_IN_COR = 1108;
constexpr int COD_ERR_LIS_ORD = 1122;
constexpr int COD_ERR_MAC_IN_MAN = 1126;
constexpr int COD_ERR_DAT_NON_OK = 1129;

QString fit(const QString &text, int size)
{
    return text.leftJustified(size, u' ', true);
}

// bin_hex of a text of '0' and '1'; a field VB left blank counts as 0.
QString binHex(const QString &bits)
{
    bool ok = false;
    QString digits = bits;
    digits.replace(u' ', u'0');
    return QString::number(digits.toUInt(&ok, 2), 16).toUpper().rightJustified(4, u'0');
}

QString rieNul(const QString &text)
{
    QString out = text;
    out.replace(QChar(0), u' ');
    return out;
}

QString bit(bool on)
{
    return on ? QStringLiteral("1") : QStringLiteral("0");
}

// Offsets of tip_pro_ord_fosber30_FOS.
struct Cursor {
    const QString &data;
    int pos = 0;
    QString take(int size)
    {
        const QString out = data.mid(pos, size);
        pos += size;
        return out;
    }
};

constexpr int FOSBER30_SIZE = 158 + 3 * 195;

} // namespace

SpdeLink::SpdeLink(CollezioneOrdini *orders, CollezioneOrdini *executed, ClasseStato *state, QSqlDatabase db, QObject *parent)
    : QObject(parent)
    , m_orders(orders)
    , m_executed(executed)
    , m_state(state)
    , m_db(db)
{
}

void SpdeLink::setEnabled(bool enabled)
{
    m_enabled = enabled;
    if (!enabled) {
        delete m_socket;
        m_socket = nullptr;
    }
}

// ini_porte_SPDE
void SpdeLink::open()
{
    if (m_socket || !m_enabled)
        return;
    if (par_mac.com_spde.ver_spde != Fosber30 || par_mac.com_spde.tip_com.trimmed() != QLatin1String("1")) {
        log_str(QStringLiteral("SpdeLink: solo Fosber 3.0 su rete"), MED_PRIORITY, CosLog::Spde);
        return;
    }
    auto *udp = new QUdpSocket(this);
    if (!udp->bind(QHostAddress::AnyIPv4, quint16(par_mac.com_spde.lan.por_loc.val()))) {
        log_str(QStringLiteral("ini_porte_SPDE: ") + udp->errorString(), MED_PRIORITY, CosLog::Spde);
        delete udp;
        return;
    }
    connect(udp, &QUdpSocket::readyRead, this, [this, udp] {
        while (udp->hasPendingDatagrams()) {
            QByteArray data(int(udp->pendingDatagramSize()), Qt::Uninitialized);
            udp->readDatagram(data.data(), data.size());
            if (par_abi.abi_com_PU)
                rxDat(QString::fromLatin1(data));
        }
    });
    m_socket = udp;
    m_keepAlive.start();
}

void SpdeLink::tick()
{
    if (!m_enabled)
        return;
    open();
    if (m_socket && m_keepAlive.elapsed() >= 1000) {
        m_socket->writeDatagram(QByteArray(), QHostAddress(par_mac.com_spde.lan.ind_rem.trimmed()), quint16(par_mac.com_spde.lan.por_rem.val()));
        m_keepAlive.start();
    }
    const double timeOut = par_mac.com_spde.TimeOut.val() / 1000;
    const bool manual = m_state->bit(ClasseStato::MAC_IN_MANUALE, 1);
    if (m_ini == 0) {
        if (m_contIni / (1 / 0.3) > timeOut || manual || !m_state->spdeOK() || m_lastRx.isValid()) {
            m_ini = 1;
            if (manual)
                messaggio(msg(QStringLiteral("mac_in_man")), false, false);
        } else {
            ++m_contIni;
        }
    } else if (par_abi.abi_err_com_PU) {
        const bool isLost = !m_lastRx.isValid() || m_lastRx.elapsed() / 1000.0 > timeOut || manual;
        m_state->setSpdeOK(!isLost);
        if (isLost)
            emit lost();
        m_state->aggiornaSpde();
    }
}

QString SpdeLink::lrc(const QString &message)
{
    qint64 sum = 0;
    for (const QChar c : message)
        sum += c.unicode();
    return QString::number((~sum) & 0xFF, 16).toUpper().rightJustified(2, u'0');
}

QString SpdeLink::frame(const QString &message)
{
    return STX_TAG + message + lrc(message) + ETX_TAG;
}

void SpdeLink::traDatSpde(const QString &text)
{
    if (m_socket)
        m_socket->writeDatagram(text.toLatin1(), QHostAddress(par_mac.com_spde.lan.ind_rem.trimmed()), quint16(par_mac.com_spde.lan.por_rem.val()));
    log_str(QStringLiteral("Inviato: ") + text, 5, CosLog::Spde);
    emit sent(text);
}

// rx_dat. str_ric_OK has no case for Fosber 3.0, so it always passes.
void SpdeLink::rxDat(const QString &text)
{
    emit received();
    m_arr += text;
    int posStx = int(m_arr.indexOf(STX_TAG));
    if (posStx < 0) {
        m_arr.clear();
        return;
    }
    int found = 1;
    int at = posStx;
    while (found < 3) {
        at = int(m_arr.indexOf(STX_TAG, at + 1));
        if (at < 0)
            break;
        ++found;
    }
    if (found >= 3)
        m_arr = m_arr.left(at);
    // prel_pri_cmd
    const int posEtx = int(m_arr.indexOf(ETX_TAG, posStx + 1));
    if (posEtx < 0) {
        log_str(QStringLiteral("rx_dat: ricevuto comando NON valido: ") + m_arr, MED_PRIORITY, CosLog::Spde);
        return;
    }
    const QString command = m_arr.mid(posStx, posEtx - posStx + 1);
    m_arr = m_arr.mid(posEtx + 1);
    ricDatSpde(command);
}

void SpdeLink::refuse(QChar record, int code, const QString &logText, bool showRefused, int position)
{
    traDatSpde(frame(record + QLatin1Char('N') + ft(code, 4)));
    m_rx.clear();
    if (showRefused)
        messaggio(msg(QStringLiteral("rif_ord_spde")) + (position > 0 ? QStringLiteral(" (%1) ").arg(position) : QString()), true, false);
    log_str(QStringLiteral("com_spde_bas - sub ric_dat_spde: ") + logText, MED_PRIORITY, CosLog::Spde);
}

// ric_dat_spde
void SpdeLink::ricDatSpde(const QString &command)
{
    if (command.right(1) != ETX_TAG) {
        m_rx += command;
        return;
    }
    m_rx += command.left(command.size() - 1);
    m_rx = m_rx.mid(1);
    if (m_rx.size() < COM_SPDE_LEN_BCC + 1) {
        refuse(QChar(32), COD_ERR_COM_SPDE, QStringLiteral("Bad length received"));
        return;
    }
    const QString sentLrc = m_rx.right(COM_SPDE_LEN_BCC);
    m_rx.chop(COM_SPDE_LEN_BCC);
    if (sentLrc != lrc(m_rx)) {
        refuse(QChar(32), COD_ERR_COM_SPDE, QStringLiteral("Bad lrc received"));
        return;
    }
    m_lastRx.start();
    if (!m_rx.startsWith(u'S'))
        log_str(QStringLiteral("com_spde_bas - sub ric_dat_spde: ricevuto comando ") + m_rx, MED_PRIORITY, CosLog::Spde);
    const QChar record = m_rx.at(0);
    const QString data = m_rx.mid(1);
    m_rx.clear();
    if (record == u'D') {
        receiveOrder(data);
    } else if (record == u'E') {
        eraseOrder(data);
    } else if (record == u'S') {
        traDatSpde(frame(QLatin1Char('s') + statusRecord()));
    } else if (record == u'U') {
        const QString text = frame(QLatin1Char('u') + executedRecord());
        log_str(QStringLiteral("com_spde_bas - returning ") + text, MED_PRIORITY, CosLog::Spde);
        traDatSpde(text);
    }
    if (record != u'S')
        Log_lst(*m_orders);
}

// rie_ord_rec_30
void SpdeLink::fillOrder(ClasseOrdine &order, const QString &data) const
{
    Cursor rec{data};
    const QString ids = rec.take(6);
    rec.take(2); // Pos_lis
    const auto put = [&](const QString &field, const QString &value) { order.setText(field, value); };
    put(QStringLiteral("spde_id"), ids.left(4));
    put(QStringLiteral("spde_ModID"), ids.right(2));
    put(QStringLiteral("mod_id"), ids.right(2));
    put(QStringLiteral("cod_ord_abb"), rec.take(16));
    put(QStringLiteral("alt_bob"), rec.take(5));
    put(QStringLiteral("tip_car.Cod_ond"), rec.take(3));
    put(QStringLiteral("tip_car.cod_com_car"), rec.take(56));
    put(QStringLiteral("tip_car.Alt_ond"), rec.take(3));
    put(QStringLiteral("tip_car.Pes_mt2"), rec.take(5));
    put(QStringLiteral("tip_car.Vel_aoc"), rec.take(3));
    put(QStringLiteral("tip_car.vel_pro"), rec.take(3));
    put(QStringLiteral("tip_car.Cod_cod_ond"), rec.take(3));
    put(QStringLiteral("pre_all"), rec.take(3));
    rec.take(50);
    qint64 metTot = 0;
    for (int i = 1; i <= 3; ++i) {
        const QString o = QStringLiteral("ord(%1).").arg(i);
        const QString commessa = rec.take(20);
        const QString cliente = rec.take(15);
        const QString nome = rec.take(30);
        const QString level = rec.take(1).toUpper();
        const QString lar = rec.take(5);
        const QString lun = rec.take(5);
        const QString outs = rec.take(2);
        const QString sheets = rec.take(6);
        if (level == QLatin1String("L")) {
            put(o + QStringLiteral("Ord_liv"), QString::number(LIV_BAS));
            if (par_mac.car_tec.num_liv_tag.val() == 1 || LIV_BAS == 0)
                log_str(QStringLiteral("com_spde_bas sub rie_ord_rec: ricevuto livello L non presente"), MED_PRIORITY, CosLog::Spde);
        } else if (level == QLatin1String("M")) {
            put(o + QStringLiteral("Ord_liv"), QString::number(LIV_MED));
            if (par_mac.car_tec.num_liv_tag.val() <= 2 || LIV_MED == 0)
                log_str(QStringLiteral("com_spde_bas sub rie_ord_rec: ricevuto livello M non presente"), MED_PRIORITY, CosLog::Spde);
        } else if (level == QLatin1String("U")) {
            put(o + QStringLiteral("Ord_liv"), QString::number(LIV_ALT));
        }
        put(o + QStringLiteral("com.cod_com_ord"), vb::trim(commessa));
        put(o + QStringLiteral("com.cod_cli"), vb::trim(cliente));
        put(o + QStringLiteral("cut_to_mark"), rec.take(1));
        put(o + QStringLiteral("pat_cod"), rec.take(16));
        put(o + QStringLiteral("pat_con"), rec.take(1));
        put(o + QStringLiteral("fst_FTC"), rec.take(1));
        put(o + QStringLiteral("Ofs_Cut"), rec.take(4));
        for (int q = 1; q <= 10; ++q)
            put(o + QStringLiteral("L%1").arg(q), rec.take(3));
        put(o + QStringLiteral("fin_pri"), rec.take(4));
        put(o + QStringLiteral("fin_dop"), rec.take(4));
        put(o + QStringLiteral("pos_pattern"), rec.take(5));
        rec.take(45);
        double som = 0;
        if (vb::val(outs) > 0) {
            som = double(vb::cLng((vb::val(sheets) / vb::val(outs)) * vb::val(lun) / TODECMM)) / 1000;
            if (som != std::floor(som))
                som = std::floor(som) + 1;
        }
        put(o + QStringLiteral("met"), vb::cStr(som));
        put(o + QStringLiteral("nom_cli"), nome);
        put(o + QStringLiteral("sca_lar"), lar);
        put(o + QStringLiteral("sca_lun"), lun);
        put(o + QStringLiteral("Sca_per_tag"), vb::cStr(vb::val(outs)));
        put(o + QStringLiteral("sca_tot_num"), sheets);
        const QString p = QStringLiteral("rep_pro.liv(%1).").arg(i);
        put(p + QStringLiteral("fog_buo"), QStringLiteral("0"));
        put(p + QStringLiteral("fog_err"), QStringLiteral("0"));
        put(p + QStringLiteral("fog_sca"), QStringLiteral("0"));
        const QString level_ = vb::trim(order.ord(i, QStringLiteral("Ord_liv")));
        if (!level_.isEmpty() && vb::val(level_) != 0)
            metTot = qMax(metTot, qint64(vb::cLng(som)));
    }
    put(QStringLiteral("rep_pro.met_tot"), QString());
    put(QStringLiteral("rep_pro.vel_med"), QString());
    put(QStringLiteral("rep_pro.num_fer"), QStringLiteral("0"));
    put(QStringLiteral("rep_pro.tem_fer"), QStringLiteral("0"));
    put(QStringLiteral("rep_pro.tem_mar"), QStringLiteral("0"));
    put(QStringLiteral("met_tot_ord"), QString::number(metTot));
}

// con_agg_ond_car: the flute comes with the order and goes into ONDE.
bool SpdeLink::addOnda(ClasseOrdine &order)
{
    const QString flute = vb::trim(order.text(QStringLiteral("tip_car.Cod_ond")));
    if (flute.isEmpty() || flute.at(0).unicode() == 0)
        return false;
    QString paper = vb::trim(order.text(QStringLiteral("tip_car.cod_com_car")));
    if (paper.isEmpty() || paper.at(0).unicode() == 0) {
        paper = QStringLiteral("ONDA ") + flute;
        order.setText(QStringLiteral("tip_car.cod_com_car"), paper);
    }
    const auto number = [&](const char *field) { return vb::cStr(vb::val(rieNul(order.text(QLatin1String(field))))); };
    QSqlQuery find(m_db);
    sqlPrepare(find, QStringLiteral("SELECT rec_no FROM ONDE WHERE upper(ONDA) = ? AND CARTA = ?"));
    find.addBindValue(flute.toUpper());
    find.addBindValue(paper);
    const bool exists = find.exec() && find.next();
    QSqlQuery q(m_db);
    if (exists) {
        sqlPrepare(q, QStringLiteral("UPDATE ONDE SET ONDA = ?, CARTA = ?, SPESSORE = ?, GRAMMATURA = ?, VELDIPROD = ?, VELDICAMBI = ?, CODONDA = ?, RISERVATO = '0' WHERE rec_no = ?"));
    } else {
        sqlPrepare(q, QStringLiteral("INSERT INTO ONDE (ONDA, CARTA, SPESSORE, GRAMMATURA, VELDIPROD, VELDICAMBI, CODONDA, RISERVATO) VALUES (?, ?, ?, ?, ?, ?, ?, '0')"));
    }
    q.addBindValue(flute);
    q.addBindValue(paper);
    q.addBindValue(number("tip_car.Alt_ond"));
    q.addBindValue(number("tip_car.Pes_mt2"));
    q.addBindValue(number("tip_car.vel_pro"));
    q.addBindValue(number("tip_car.Vel_aoc"));
    q.addBindValue(number("tip_car.Cod_cod_ond"));
    if (exists)
        q.addBindValue(find.value(0));
    if (!q.exec())
        log_str(QStringLiteral("con_agg_ond_car: ") + q.lastError().text(), MED_PRIORITY, CosLog::Spde);
    return true;
}

void SpdeLink::receiveOrder(const QString &data)
{
    constexpr QChar R(u'd');
    if (m_state->bit(ClasseStato::MAC_IN_MANUALE, 1))
        return refuse(R, COD_ERR_MAC_IN_MAN, QStringLiteral("Invio/Modifica ordine rifiutato: Macchina in manuale"), true);
    if (m_state->bit(ClasseStato::MAC_CAM_ORD_IN_CORSO, 1))
        return refuse(R, COD_ERR_CAM_ORD_IN_COR, QStringLiteral("Invio/Modifica ordine rifiutato: Cambio ordine in corso"), true);
    if (data.size() != FOSBER30_SIZE)
        return refuse(R, COD_ERR_DAT_NON_OK, QStringLiteral("Bad order lenght received"));
    const int position = int(vb::val(data.mid(6, 2))) + 1;
    if (m_orders->Count() + 1 < position || position <= 0 || position > MAX_NUM_ORD_IN_LIS)
        return refuse(R, COD_ERR_LIS_ORD, QStringLiteral("Bad order lenght received"), true, position);
    auto order = std::make_shared<ClasseOrdine>();
    fillOrder(*order, data);
    if (!addOnda(*order))
        return refuse(R, COD_ERR_ASS_OND, QStringLiteral("Bad order flute received"), true, position);
    const int result = val_ord(*order, m_db);
    if (result != RET_OK) {
        traDatSpde(frame(R + QLatin1Char('N') + ft(result, 4)));
        messaggio(msg(QStringLiteral("rif_ord_spde")) + QStringLiteral(" (%1) : %2").arg(position).arg(result), true, false);
        log_str(QStringLiteral("com_spde_bas - sub ric_dat_spde: Validation failed: cod %1:").arg(result) + msg(msg_err_val_ord(result)), MED_PRIORITY, CosLog::Spde);
        return;
    }
    const int ret = m_orders->Count() < position ? m_orders->add(order, position, true) : m_orders->Modifica(order, position, true);
    if (ret == RET_BAD)
        return refuse(R, COD_ERR_LIS_ORD, QStringLiteral("Failed adding in order list pos "), true, position);
    traDatSpde(frame(R + QStringLiteral("A    ")));
    messaggio(msg(QStringLiteral("ric_ord_spde")) + QStringLiteral(" (%1) ").arg(position), false, false);
    log_str(QStringLiteral("com_spde_bas - sub ric_dat_spde: Successfully added record posizione %1 SPDE_ID/MOD_ID ").arg(position) + data.left(4) + u'/' + data.mid(4, 2), MED_PRIORITY, CosLog::Spde);
}

void SpdeLink::eraseOrder(const QString &data)
{
    constexpr QChar R(u'e');
    if (m_state->bit(ClasseStato::MAC_IN_MANUALE, 1))
        return refuse(R, COD_ERR_MAC_IN_MAN, QStringLiteral("Comando di cancellazione ordine rifiutato: Macchina in manuale"));
    // The record letter of this answer is "d", in VB too.
    if (m_state->bit(ClasseStato::MAC_CAM_ORD_IN_CORSO, 1))
        return refuse(u'd', COD_ERR_CAM_ORD_IN_COR, QStringLiteral("Comando di cancellazione ordine rifiutato: Cambio ordine in corso"));
    if (data.size() != 1)
        return refuse(R, COD_ERR_DAT_NON_OK, QStringLiteral("Bad erase lenght received"));
    const int position = int(vb::val(data.right(2))) + 1;
    if (m_orders->Count() < position)
        return refuse(R, COD_ERR_LIS_ORD, QStringLiteral("Bad position in erase received"));
    if (m_orders->Del_blk(position, position, false, true) != RET_OK)
        return refuse(R, COD_ERR_LIS_ORD, QStringLiteral("Failed deleting in order list pos %1").arg(position));
    traDatSpde(frame(R + QStringLiteral("A    ")));
    log_str(QStringLiteral("com_spde_bas - sub ric_dat_spde: Successfully deleted record n. %1").arg(position), MED_PRIORITY, CosLog::Spde);
}

QString SpdeLink::statusRecord() const
{
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    const int count = m_orders->Count();
    const bool manual = m_state->bit(ClasseStato::MAC_IN_MANUALE, 1);
    const bool change = m_state->bit(ClasseStato::MAC_CAM_ORD_IN_CORSO, 1);
    QString mst = bit(manual) + bit(change) + m_state->gruAll().right(1) + bit(!m_state->comOK());
    for (int i = 1; i <= 3; ++i)
        mst += bit(i <= levels && !com_ini_sch_tag(i));
    for (int i = 1; i <= 3; ++i)
        mst += bit(i <= levels && par_abi.liv(i));
    mst += QStringLiteral("00");
    mst += count >= 1 ? bit(m_state->cod_ord_cor() != m_orders->item(1)->stag_id()) : QStringLiteral(" ");
    mst += count > 1 ? bit(m_state->cod_ord_suc() != m_orders->item(2)->stag_id()) : QStringLiteral(" ");
    mst += bit(!change && !manual) + bit(!change && !manual);
    std::reverse(mst.begin(), mst.end());

    QString groups;
    for (const int pos : {ClasseStato::POS_GRU_HAR, ClasseStato::POS_GRU_QE_MST, ClasseStato::POS_GRU_QE_SLV, ClasseStato::POS_GRU_QE_EST, ClasseStato::POS_GRU_BM, ClasseStato::POS_GRU_CON, ClasseStato::POS_GRU_MAL})
        groups += bit(m_state->gruAllAtt(pos));
    groups += QStringLiteral("000000000");
    std::reverse(groups.begin(), groups.end());

    QString out = binHex(mst) + binHex(groups);
    for (int i = 1; i <= MAX_NUM_ORD_IN_LIS; ++i) {
        if (i <= count) {
            const OrdinePtr order = m_orders->item(i);
            out += QLatin1Char('1') + fit(vb::trim(order->text(QStringLiteral("spde_id"))) + vb::trim(order->text(QStringLiteral("spde_ModID"))), 6);
        } else {
            out += QLatin1Char('2') + QString(6, u' ');
        }
    }
    if (m_executed->Count() > 0) {
        const OrdinePtr last = m_executed->item(1);
        out += (vb::val(last->text(QStringLiteral("rep_pro.met_tot"))) != 0 ? QLatin1Char('1') : QLatin1Char('0'));
        out += fit(rieNul(last->text(QStringLiteral("spde_id")) + last->text(QStringLiteral("spde_ModID"))), 6);
    } else {
        out += QStringLiteral("0      ");
    }
    return out + QString(6, u' ');
}

QString SpdeLink::executedRecord() const
{
    constexpr int SIZE = 6 + 16 + 8 + 6 + 6 + 9 * 6 + 3 * 28;
    if (m_executed->Count() == 0)
        return QString(SIZE, u' ');
    const ClasseOrdine &ord = *m_executed->item(1);
    const auto report = [&](const char *field) { return rieNul(ord.text(QStringLiteral("rep_pro.") + QLatin1String(field))); };
    int lar = 0;
    for (int i = 1; i <= 3; ++i) {
        const double width = lar + vb::val(ord.ord(i, QStringLiteral("sca_lar"))) * vb::val(ord.ord(i, QStringLiteral("Sca_per_tag"))) / 10;
        lar = vb::cInt(width);
    }
    const qint64 metLin = vb::cLng(vb::val(fit(report("met_tot"), 9)));
    const QString dateIni = report("dat_ini");
    const QString oraIni = report("ora_ini");
    const QString oraFin = report("ora_fin");
    QString out = fit(rieNul(ord.text(QStringLiteral("spde_id")) + ord.text(QStringLiteral("spde_ModID"))), 6);
    out += fit(rieNul(ord.text(QStringLiteral("cod_ord_abb"))), 16);
    out += fit(vb::trim(dateIni).isEmpty() ? dateIni : QStringLiteral("20") + dateIni.mid(0, 2) + dateIni.mid(3, 2) + dateIni.mid(6, 2), 8);
    out += fit(vb::trim(oraIni).isEmpty() ? oraIni : oraIni.mid(0, 2) + oraIni.mid(3, 2) + oraIni.mid(6, 2), 6);
    out += fit(vb::trim(oraFin).isEmpty() ? oraFin : oraFin.mid(0, 2) + oraFin.mid(3, 2) + oraFin.mid(6, 2), 6);
    out += fit(QString::number(vb::cLng(vb::val(report("tem_mar"))) / 60), 9);
    out += fit(QString::number(vb::cLng(vb::val(report("tem_fer"))) / 60), 9);
    out += fit(report("vel_med"), 9);
    out += fit(report("num_fer"), 9);
    out += fit(QString::number(metLin), 9);
    out += fit(QString::number(metLin * lar / 1000), 9);
    for (int i = 1; i <= 3; ++i) {
        const QString p = QStringLiteral("rep_pro.liv(%1).").arg(i);
        out += fit(rieNul(ord.text(p + QStringLiteral("fog_buo"))), 9);
        out += fit(rieNul(ord.text(p + QStringLiteral("fog_err"))), 9);
        const QString level = vb::trim(ord.ord(i, QStringLiteral("Ord_liv")));
        out += level == QLatin1String("1") ? QStringLiteral("L") : level == QLatin1String("2") ? (par_mac.car_tec.num_liv_tag.val() == 2 ? QStringLiteral("U") : QStringLiteral("M"))
            : level == QLatin1String("3") ? QStringLiteral("U") : fit(level, 1);
        out += fit(rieNul(ord.text(p + QStringLiteral("mil_sca"))), 9);
    }
    return out;
}
