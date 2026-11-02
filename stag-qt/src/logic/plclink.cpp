#include "plclink.h"

#include "classestato.h"
#include "finsclient.h"
#include "messages.h"
#include "parcurtag.h"
#include "parmac.h"
#include "staglog.h"
#include "uihooks.h"
#include "vbcompat.h"
#include "vbtypes.h"

#include <QEventLoop>
#include <QTimer>

using namespace stag;

static constexpr int COMMAND_TIMEOUT_MS = 10000;

Rmt::Rmt(const QString &name, RmtChannel channel, PlcLink *link, QObject *parent)
    : QObject(parent)
    , m_name(name)
    , m_channel(channel)
    , m_link(link)
{
}

bool Rmt::esegui(int ric, int liv, int numOrd, const QVariant &dat)
{
    PlcLink::Queue &queue = m_link->m_queues[size_t(m_channel)];
    const QString request = m_name + QStringLiteral(" %1 %2").arg(ric).arg(liv) + QStringLiteral(" con dati aggiuntivi");
    if (queue.ric != -1) {
        log_str(QStringLiteral("rmt: rmt occupata ") + request, MED_PRIORITY, CosLog::Rmt);
        return false;
    }
    queue.ric = ric;
    queue.liv = liv;
    queue.numOrd = numOrd;
    queue.ret = RET_WRK;
    queue.dat = dat;
    queue.owner = this;
    queue.started.start();
    log_str(QStringLiteral("rmt: esegui ") + request, MED_PRIORITY, CosLog::Rmt);
    return true;
}

int Rmt::ric() const
{
    return m_link->m_queues[size_t(m_channel)].ric;
}

int Rmt::liv() const
{
    return m_link->m_queues[size_t(m_channel)].liv;
}

void Rmt::resetta()
{
    PlcLink::Queue &queue = m_link->m_queues[size_t(m_channel)];
    queue.ric = -1;
    queue.ret = RET_ZRO;
}

void Rmt::rispondi(int ret, const QString &text)
{
    log_str(QStringLiteral("rmt: rispondi %1 per ").arg(ret) + m_name, MED_PRIORITY, CosLog::Rmt);
    m_link->m_queues[size_t(m_channel)].ret = ret;
    if (ret == RET_OK)
        emit retok(text);
    else
        emit retbad(text);
}

PlcLink::PlcLink(FinsClient *fins, ClasseStato *state, CollezioneOrdini *orders, QObject *parent)
    : QObject(parent)
    , m_fins(fins)
    , m_state(state)
    , m_orders(orders)
    , m_boards(orders)
{
}

// The write of snd_str_PLC: after TIMER_SCANS_PLC * 5 ms, and logged only when it differs from the last one.
bool PlcLink::send(const PlcFrame &frame)
{
    if (!m_writes)
        return false;
    QEventLoop wait;
    QTimer::singleShot(par_mac.indirizzo_plc.TIMER_SCANS_PLC * 5, &wait, &QEventLoop::quit);
    wait.exec();
    m_fins->writeArea(3500, frame.words());
    if (frame.text() != m_lastLogged) {
        log_str(frame.logLine(), MED_PRIORITY, CosLog::Plc);
        m_lastLogged = frame.text();
    }
    return true;
}

void PlcLink::sendStartup()
{
    if (!m_writes) {
        log_str(QStringLiteral("PlcLink: scrittura verso PLC disabilitata, nessun invio iniziale"), MED_PRIORITY, CosLog::Plc);
        return;
    }
    if (!m_state->comOK()) {
        log_str(QStringLiteral("Sub invio_ini_par_mac: invio iniziale dei parametri al PLC FALLITO !"), MED_PRIORITY, CosLog::Plc);
        return;
    }
    for (int level = 1; level <= int(par_mac.car_tec.num_liv_tag.val()); ++level)
        send(frameCurTag(level));
    send(frameParCfgMac());
    send(frameParMac());
    m_fins->setMonitor();
    if (!m_fins->error() && m_fins->runMode() == 2)
        log_str(QStringLiteral("Inizializzazione PLC: PLC messo in modalità Monitor"), MED_PRIORITY, CosLog::Plc);
    else
        log_str(QStringLiteral("Fallita inizializzazione PLC per errore di rete"), MED_PRIORITY, CosLog::Plc);
}

void PlcLink::sendOrder(int operation, int which)
{
    if (operation == CANC_ORD) {
        send(frameDeleteOrder(which));
        return;
    }
    const OrdinePtr order = m_orders->item(which);
    const OrdinePtr next = m_orders->Count() >= which + 1 ? m_orders->item(which + 1) : nullptr;
    const auto frame = frameOrder(operation == INVIO_ORD ? OrderOperation::Send : OrderOperation::Modify, which, *order, next.get());
    if (frame)
        send(*frame);
}

// set_dat_ini_pro
void PlcLink::setStartDate(int order)
{
    const OrdinePtr item = m_orders->item(order);
    const QDateTime now = vb::now();
    item->setText(QStringLiteral("rep_pro.dat_ini"), vb::cStrDate(now.date()));
    item->setText(QStringLiteral("rep_pro.ora_ini"), (QStringLiteral("0") + vb::trim(vb::cStrTime(now.time()))).right(8));
}

void PlcLink::alignOrders()
{
    const int count = m_orders->Count();
    const auto ids = [](const OrdinePtr &order) {
        return QStringLiteral(" ID Ordine SPDE: ") + vb::trim(order->text(QStringLiteral("spde_id"))) + QStringLiteral(" ID Ordine STAG: ") + vb::trim(order->stag_id());
    };
    if (m_state->bit(ClasseStato::MAC_CAM_ORD_COMPLETATO, 0)) {
        if (count >= 1) {
            const OrdinePtr current = m_orders->item(1);
            if (m_state->cod_ord_cor() != current->stag_id() || m_force[CORRENTE]) {
                log_str(QStringLiteral("Invio ordine corrente.") + ids(current) + QStringLiteral(" ID Ordine PLC: ") + vb::trim(m_state->cod_ord_cor())
                            + QStringLiteral(" Forzatura = ") + (m_force[CORRENTE] ? QStringLiteral("ON") : QStringLiteral("OFF")),
                        MED_PRIORITY, CosLog::Lis);
                sendOrder(INVIO_ORD, CORRENTE);
                send(frameResetCounters());
                setStartDate(1);
            } else if (current->mod_id() != m_oldModCor) {
                log_str(QStringLiteral("Invio modifica ordine corrente:") + ids(current) + u'/' + vb::trim(current->mod_id()), MED_PRIORITY, CosLog::Lis);
                sendOrder(MODIF_ORD, CORRENTE);
            }
        } else if (vb::val(m_state->cod_ord_cor()) != 0) {
            log_str(QStringLiteral("Invio cancellazione ordine corrente. ID Ordine PLC: ") + vb::trim(m_state->cod_ord_cor()), MED_PRIORITY, CosLog::Lis);
            sendOrder(CANC_ORD, CORRENTE);
        }
    }
    if (count > 1) {
        const OrdinePtr next = m_orders->item(2);
        if (m_state->cod_ord_suc() != next->stag_id() || m_force[SUCCESSIVO]) {
            log_str(QStringLiteral("Invio ordine successivo.") + ids(next) + QStringLiteral(" ID Ordine PLC: ") + vb::trim(m_state->cod_ord_suc())
                        + QStringLiteral(" Forzatura = ") + (m_force[SUCCESSIVO] ? QStringLiteral("ON") : QStringLiteral("OFF")),
                    MED_PRIORITY, CosLog::Lis);
            sendOrder(INVIO_ORD, SUCCESSIVO);
        } else if (next->mod_id() != m_oldModSuc) {
            log_str(QStringLiteral("Invio modifica ordine successivo:") + ids(next) + u'/' + vb::trim(next->mod_id()), MED_PRIORITY, CosLog::Lis);
            sendOrder(MODIF_ORD, SUCCESSIVO);
        }
    } else if (vb::val(m_state->cod_ord_suc()) != 0) {
        log_str(QStringLiteral("Invio cancellazione ordine successivo. ID Ordine PLC: ") + vb::trim(m_state->cod_ord_suc()), MED_PRIORITY, CosLog::Lis);
        sendOrder(CANC_ORD, SUCCESSIVO);
    }
}

void PlcLink::servePlcQueue()
{
    Queue &queue = m_queues[ChannelPlc];
    if (queue.ret != RET_WRK)
        return;
    Rmt *owner = queue.owner;
    const auto answer = [&](bool sent) {
        if (!m_state->comOK())
            owner->rispondi(RET_BAD, msg(QStringLiteral("com_int_con_mac")));
        else if (sent)
            owner->rispondi(RET_OK, msg(QStringLiteral("ope_eff")));
        else
            owner->rispondi(RET_BAD, msg(QStringLiteral("imp_ese_ope")));
    };
    const bool online = m_state->comOK();
    switch (queue.ric) {
    case INVIO_ORD:
        if (queue.numOrd != CORRENTE && queue.numOrd != SUCCESSIVO) {
            owner->rispondi(RET_BAD, msg(QStringLiteral("par_err")));
        } else {
            const bool can = online && m_writes && m_orders->Count() >= queue.numOrd;
            if (can) {
                sendOrder(INVIO_ORD, queue.numOrd);
                for (int i = 1; i <= 3; ++i) {
                    if (par_mac.opt.cut_pat(i) == QLatin1String("1"))
                        inv_ord_sch_tag(queue.numOrd) = true;
                }
            }
            answer(can);
        }
        break;
    case AZZERAMENTO_CONTATORI:
        answer(online && send(frameResetCounters()));
        break;
    case PARAM_MAC:
        answer(online && send(frameParMac()));
        break;
    case CONFERMA_DATI_SCHEDE_TAGLIO:
        answer(online && sendBoardConfirm());
        break;
    case PARAM_CFG_MAC:
        answer(online && send(frameParCfgMac()));
        break;
    case PARAM_CUR_TAG:
        if (!online) {
            owner->rispondi(RET_BAD, msg(QStringLiteral("com_int_con_mac")));
        } else if (queue.dat.toBool()) {
            bool all = true;
            for (int level = 1; level <= int(par_mac.car_tec.num_liv_tag.val()); ++level) {
                if (m_db && caricaNewParCurTag(*m_db, level))
                    send(frameCurTag(level));
                else
                    all = false;
            }
            // VB answers with the outcome of the last level and the text of all of them.
            owner->rispondi(all ? RET_OK : RET_BAD, msg(all ? QStringLiteral("ope_eff") : QStringLiteral("val_non_tro_cur_tag")));
        } else if (queue.liv > 0 && queue.liv < 4) {
            answer(send(frameCurTag(queue.liv)));
        } else {
            owner->rispondi(RET_BAD, msg(QStringLiteral("val_non_tro_cur_tag")));
        }
        break;
    default:
        owner->rispondi(RET_BAD, msg(QStringLiteral("par_err")));
    }
    if (queue.ret == RET_WRK && queue.started.elapsed() > COMMAND_TIMEOUT_MS) {
        log_str(QStringLiteral("tim_tag_plc: Rmt %1 in time out eseguendo l'operazione %2").arg(owner->name()).arg(queue.ric), MED_PRIORITY, CosLog::Rmt);
        owner->rispondi(RET_BAD, msg(QStringLiteral("imp_ese_ope")));
    }
}

void PlcLink::serveCardQueue()
{
    Queue &queue = m_queues[ChannelCards];
    if (queue.ret != RET_WRK)
        return;
    Rmt *owner = queue.owner;
    const int liv = queue.liv;
    const bool validLevel = liv > 0 && liv < 4;
    if (queue.ric == CONFIGURA) {
        if (queue.dat.toBool()) {
            bool all = true;
            for (int level = 1; level <= int(par_mac.car_tec.num_liv_tag.val()); ++level)
                all = m_boards.snd(level, CONFIGURA) == RET_OK && all;
            owner->rispondi(all ? RET_OK : RET_BAD, msg(all ? QStringLiteral("ope_eff") : QStringLiteral("ope_eff_par")));
        } else if (validLevel) {
            const int ret = m_boards.snd(liv, CONFIGURA);
            owner->rispondi(ret, msg(ret == RET_OK ? QStringLiteral("ope_eff") : QStringLiteral("com_int_con_mac")));
        } else {
            owner->rispondi(RET_ZRO, msg(QStringLiteral("par_err")));
        }
    } else if (queue.ric == INFORMAZIONI || queue.ric == LETTURA_STATO || queue.ric == SELEZIONE_CONTRASTO) {
        if (validLevel) {
            const int ret = m_boards.snd(liv, queue.ric, 1, queue.dat.toInt());
            owner->rispondi(ret, msg(ret == RET_OK ? QStringLiteral("ope_eff") : QStringLiteral("com_int_con_mac")));
        } else {
            owner->rispondi(RET_ZRO, msg(QStringLiteral("par_err")));
        }
    } else {
        owner->rispondi(RET_BAD, msg(QStringLiteral("imp_ese_ope")));
    }
}

// The LETTURA STATO SCHEDE DI TAGLIO block of tim_tag_plc.
void PlcLink::readBoards()
{
    constexpr qint64 AGG_STA_SCH_TAG = 1500;
    for (int i = 1; i <= int(par_mac.car_tec.num_liv_tag.val()); ++i) {
        if (!com_ini_sch_tag(i)) {
            if (m_state->bit(ClasseStato::StaPlcBit(ClasseStato::SCH_TAG_LIV_1_PRONTA + i - 1), 1)) {
                m_boards.setSendCase(INVIO_INIZIALE_PAR);
                m_boards.snd(i, CONFIGURA);
            }
        } else if (par_mac.opt.cut_pat(i) == QLatin1String("1") || par_mac.opt.cut_pat_auto(i) == QLatin1String("1")) {
            QElapsedTimer &last = m_lastBoardRead[size_t(i)];
            if (!last.isValid() || last.elapsed() >= AGG_STA_SCH_TAG) {
                m_boards.setComOK(i, m_boards.snd(i, LETTURA_STATO_ORDINE) == RET_OK);
                last.start();
            }
        } else {
            m_boards.setComOK(i, false);
        }
    }
}

bool PlcLink::sendBoardConfirm()
{
    log_str(QStringLiteral("snd_str_plc: Invio conferma dati inviati a scheda di taglio verso PLC"), MED_PRIORITY, CosLog::SchTag);
    return send(frameConfirmCutBoards());
}

// The GESTIONE SCHEDE DI TAGLIO block of tim_tag_plc.
void PlcLink::alignBoards()
{
    constexpr qint64 EVERY_MS = 2 * 1500;
    bool option = false;
    for (int i = 1; i <= 3; ++i)
        option = option || par_mac.opt.cut_pat(i) == QLatin1String("1") || par_mac.opt.cut_pat_auto(i) == QLatin1String("1");
    if (!option || !m_state->comOK() || m_state->bit(ClasseStato::MAC_CAM_ORD_IN_CORSO, 1) || !m_state->bit(ClasseStato::MAC_CAM_ORD_COMPLETATO, 0)
        || m_boards.ftcRun() || (m_lastBoardAlign.isValid() && m_lastBoardAlign.elapsed() < EVERY_MS))
        return;

    for (int numOrd = CORRENTE; numOrd <= SUCCESSIVO; ++numOrd) {
        const QString name = numOrd == CORRENTE ? QStringLiteral("corrente") : QStringLiteral("successivo");
        bool ret = true;
        bool dis = false;
        for (int liv = 1; liv <= int(par_mac.car_tec.num_liv_tag.val()); ++liv) {
            if (!m_boards.comOK(liv)) {
                log_str(QStringLiteral("tim_tag_plc: Comunicazione con scheda di taglio livello %1 è interrotta").arg(liv), MED_PRIORITY, CosLog::SchTag);
                continue;
            }
            if (m_orders->Count() < numOrd)
                continue;
            const OrdinePtr order = m_orders->item(numOrd);
            const bool forced = inv_ord_sch_tag(numOrd);
            if (order->stag_id() == m_boards.idOrd(liv, numOrd) && !forced)
                continue;
            bool present = false;
            for (int i = 1; i <= 3; ++i)
                present = present || order->Ord_liv(i) == liv;
            log_str(QStringLiteral("tim_tag_plc: Invio ordine ") + (present ? QString() : QStringLiteral("dummy ")) + name
                        + QStringLiteral(" a scheda di taglio livello %1 per ").arg(liv) + (forced ? QStringLiteral("forzatura") : QStringLiteral("disallineamento"))
                        + QStringLiteral(" StagId = ") + order->stag_id() + QStringLiteral(" IdSchTag = ") + m_boards.idOrd(liv, numOrd),
                    MED_PRIORITY, CosLog::SchTag);
            ret = m_boards.snd(liv, INVIO_DATI_ORDINE, numOrd) == RET_OK && ret;
            dis = true;
        }
        if (inv_ord_sch_tag(numOrd) && ret && dis)
            inv_ord_sch_tag(numOrd) = false;
        if (numOrd == CORRENTE && ret && dis) {
            sendBoardConfirm();
            break;
        }
    }
    m_lastBoardAlign.start();
}

void PlcLink::serveMacroQueue()
{
    Queue &queue = m_queues[ChannelMacro];
    if (queue.ret != RET_WRK)
        return;
    if (queue.ric == PLC_PARAM_MAC_PARAM_CFG_MAC) {
        if (m_state->comOK() && send(frameParMac()) && send(frameParCfgMac()))
            queue.owner->rispondi(RET_OK, msg(QStringLiteral("ope_eff")));
        else
            queue.owner->rispondi(RET_BAD, msg(QStringLiteral("ope_eff_par")));
    } else if (queue.ric == ACQ_CON_SCH_PLC) {
        if (queue.liv <= 0 || queue.liv >= 4) {
            queue.owner->rispondi(RET_ZRO, msg(QStringLiteral("par_err")));
        } else if (m_boards.snd(queue.liv, SELEZIONE_CONTRASTO, 1, queue.dat.toInt()) == RET_OK && m_state->comOK()) {
            sendBoardConfirm();
            queue.owner->rispondi(RET_OK, msg(QStringLiteral("ope_eff")));
        } else {
            queue.owner->rispondi(RET_BAD, msg(QStringLiteral("com_int_con_mac")));
        }
    } else if (queue.ric == PLC_PARAM_CFG_MAC_PARAM_CUR_TAG) {
        queue.owner->setRollBackPlc(false);
        if (!m_state->comOK()) {
            queue.owner->rispondi(RET_BAD, msg(QStringLiteral("com_int_con_mac")));
        } else {
            send(frameParCfgMac());
            if (m_db && caricaNewParCurTag(*m_db, queue.liv)) {
                send(frameCurTag(queue.liv));
                queue.owner->rispondi(RET_OK, msg(QStringLiteral("ope_eff")));
            } else {
                queue.owner->setRollBackPlc(true);
                queue.owner->rispondi(RET_BAD, msg(QStringLiteral("val_non_tro_cur_tag")));
            }
        }
    } else {
        queue.owner->rispondi(RET_BAD, msg(QStringLiteral("imp_ese_ope")));
    }
}

void PlcLink::tick()
{
    if (!m_modIdsRead) {
        if (m_orders->Count() >= 1)
            m_oldModCor = m_orders->item(1)->mod_id();
        if (m_orders->Count() > 1)
            m_oldModSuc = m_orders->item(2)->mod_id();
        m_modIdsRead = true;
    }
    if (m_writes)
        readBoards();
    if (m_writes && m_state->comOK() && !m_state->bit(ClasseStato::MAC_CAM_ORD_IN_CORSO, 1))
        alignOrders();
    if (m_orders->Count() >= 1)
        m_oldModCor = m_orders->item(1)->mod_id();
    if (m_orders->Count() > 1)
        m_oldModSuc = m_orders->item(2)->mod_id();
    if (m_writes)
        alignBoards();
    servePlcQueue();
    serveCardQueue();
    serveMacroQueue();
}
