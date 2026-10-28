#include "collezioneordini.h"

#include "classestato.h"
#include "inistore.h"
#include "messages.h"
#include "recordfile.h"
#include "staglog.h"
#include "stagglobals.h"
#include "uihooks.h"
#include "vbcompat.h"

#include <QFile>

using namespace stag;

int ClasseOrdine::Ord_liv(int i) const
{
    return int(vb::val(ord(i, QStringLiteral("Ord_liv"))));
}

int ClasseOrdine::ind_liv(int indFor) const
{
    const QString liv = ord(indFor, QStringLiteral("Ord_liv"));
    if (vb::trim(liv).isEmpty())
        return 0;
    const int v = int(vb::val(liv));
    if (v == LIV_ALT)
        return 3;
    if (v == LIV_MED)
        return 2;
    if (v == LIV_BAS)
        return 1;
    return 0;
}

CollezioneOrdini::CollezioneOrdini(QObject *parent)
    : QObject(parent)
{
}

int CollezioneOrdini::Init(bool eseg)
{
    if (m_iniz)
        return RET_BAD;
    m_ese = eseg;
    m_iniz = true;
    return RET_OK;
}

bool CollezioneOrdini::manualeOrSpdeKo() const
{
    return (sta_tag && sta_tag->bit(ClasseStato::MAC_IN_MANUALE, 1)) || (sta_tag && !sta_tag->spdeOK());
}

QString CollezioneOrdini::fileName() const
{
    return resolveDataFile(m_ese ? pro_set.Nam_fil_dat_ord_ese : pro_set.Nam_fil_dat_ord);
}

int CollezioneOrdini::Move_blk(int ini, int fin, int dst)
{
    const int count = Count();
    if (ini > count || fin > count || dst > count || ini < 1 || fin < 1 || dst < 1) {
        log_str(QStringLiteral("Mov_blk: Un dato eccedente il numero degli ordini presenti"), MED_PRIORITY, CosLog::Ord);
        messaggio(msg(QStringLiteral("imp_ese_ope")), true, false);
        return RET_BAD;
    }
    if (ini > fin)
        std::swap(ini, fin);
    if (dst == ini)
        return RET_OK;
    if (dst > ini && dst <= fin) {
        log_str(QStringLiteral("Mov_blk: Dati ambigui: ini= %1 fin = %2 dst = %3").arg(ini).arg(fin).arg(dst), MED_PRIORITY, CosLog::Ord);
        messaggio(msg(QStringLiteral("imp_ese_ope")), true, false);
        return RET_BAD;
    }
    if (!manualeOrSpdeKo()) {
        messaggio(msg(QStringLiteral("mac_in_auto")), true, false);
        log_str(QStringLiteral("Move_blk: Tentativo di spostamento di ordini con macchina macchina in automatico"), MED_PRIORITY, CosLog::Ord);
        return RET_BAD;
    }
    const QList<OrdinePtr> block = m_ordini.mid(ini - 1, fin - ini + 1);
    m_ordini.remove(ini - 1, fin - ini + 1);
    for (int i = 1; i <= block.size(); ++i) {
        if (dst + i - 1 <= Count())
            m_ordini.insert(dst + i - 2, block[i - 1]);
        else
            m_ordini.append(block[i - 1]);
    }
    Salva(false);
    m_punOrd = dst;
    return RET_OK;
}

int CollezioneOrdini::Cpy_blk(int ini, int fin, int dst)
{
    if (m_ese) {
        log_str(QStringLiteral("Cpy_blk: Metodo non utilizzabile per ord_mac_ese"), MED_PRIORITY, CosLog::Ord);
        return RET_BAD;
    }
    const int count = Count();
    if (ini > count || fin > count || dst > count + 1 || ini < 1 || fin < 1 || dst < 1) {
        log_str(QStringLiteral("Cpy_blk: Un dato eccedente il numero degli ordini presenti"), MED_PRIORITY, CosLog::Ord);
        return RET_BAD;
    }
    if (ini > fin)
        std::swap(ini, fin);
    const bool out = dst == count + 1;
    if (count + (fin - ini + 1) > MAX_NUM_ORD) {
        log_str(QStringLiteral("Cpy_blk: Si eccede il numero massimo di ordini"), MED_PRIORITY, CosLog::Ord);
        messaggio(msg(QStringLiteral("rag_num_max")), true, false);
        return RET_BAD;
    }
    if (!manualeOrSpdeKo()) {
        log_str(QStringLiteral("Cpy_blk: Tentativo di copia ordini con macchina in automatico"), MED_PRIORITY, CosLog::Ord);
        messaggio(msg(QStringLiteral("mac_in_auto")), true, false);
        return RET_BAD;
    }
    QList<OrdinePtr> copies;
    for (int i = ini; i <= fin; ++i) {
        auto copy = std::make_shared<ClasseOrdine>();
        copy->setOrdine(m_ordini[i - 1]->ordine());
        copies.append(copy);
    }
    for (int i = 1; i <= copies.size(); ++i) {
        copies[i - 1]->set_stag_id(QString::number(Cod_uni()));
        if (!out)
            m_ordini.insert(dst + i - 2, copies[i - 1]);
        else
            m_ordini.append(copies[i - 1]);
    }
    Salva(false);
    m_punOrd = dst;
    return RET_OK;
}

void CollezioneOrdini::Fine()
{
    const QString cod = vb::fixedLength(Count() > 0 ? m_ordini[0]->stag_id() : ft(0, 4), 4);
    setFileINIString(QStringLiteral("ORD_SET"), m_ese ? QStringLiteral("Cod_primo_ese") : QStringLiteral("Cod_primo"), cod, QStringLiteral("Ordini"));
}

int CollezioneOrdini::Cod_uni()
{
    const int codUni = vb::cInt(vb::val(getFileINIString(QStringLiteral("ORD_SET"), QStringLiteral("Cod_uni"), QStringLiteral("9000"), QStringLiteral("Ordini"))));
    const int cod = (codUni >= 9999 || codUni < 9000) ? 9000 : codUni + 1;
    setFileINIString(QStringLiteral("ORD_SET"), QStringLiteral("Cod_uni"), ft(cod, 4), QStringLiteral("Ordini"));
    return codUni;
}

int CollezioneOrdini::Modifica(const OrdinePtr &ord, int pos, bool rcvOrdSpde)
{
    if (pos > Count() + 1 || pos < 1) {
        log_str(QStringLiteral("Modifica: Posizione maggiore del numero di ordini presenti"), MED_PRIORITY, CosLog::Ord);
        return RET_BAD;
    }
    if (m_ese) {
        if (pos <= Count())
            m_ordini.removeAt(pos - 1);
    } else {
        QString tmpId = vb::fixedLength(QString(), 4);
        QString tmpMod = vb::fixedLength(QString(), 2);
        if (!rcvOrdSpde && pos <= Count()) {
            tmpId = vb::fixedLength(m_ordini[pos - 1]->stag_id(), 4);
            tmpMod = vb::fixedLength(m_ordini[pos - 1]->mod_id(), 2);
        }
        if (pos <= Count())
            m_ordini.removeAt(pos - 1);
        if (!rcvOrdSpde) {
            ord->set_stag_id(ft(qint64(vb::val(tmpId)), 4));
            ord->set_mod_id(ft((qint64(vb::val(tmpMod)) + 1) % 100, 2));
        } else {
            ord->set_stag_id(QString::number(Cod_uni()));
            if (par_mac.com_spde.ver_spde == Fosber10)
                ord->set_mod_id(QStringLiteral("00"));
        }
    }
    m_ordini.insert(qMin(pos - 1, int(m_ordini.size())), ord);
    Salva(false);
    if (pos == CORRENTE || pos == SUCCESSIVO)
        inv_ord_sch_tag(pos) = true;
    return RET_OK;
}

int CollezioneOrdini::add(const OrdinePtr &ord, int pos, bool rcvOrdSpde)
{
    if (m_ese) {
        while (Count() > MAX_NUM_ORD_ESE - 1)
            m_ordini.removeLast();
    } else if (Count() > MAX_NUM_ORD - 1) {
        log_str(QStringLiteral("Add: impossibile aggiungere ordine, collezione piena"), MED_PRIORITY, CosLog::Ord);
        return RET_BAD;
    }
    auto tmpOrd = std::make_shared<ClasseOrdine>();
    tmpOrd->setOrdine(ord->ordine());
    if (m_ese)
        pos = 1;
    else if (!rcvOrdSpde || par_mac.com_spde.ver_spde == Fosber10)
        tmpOrd->set_mod_id(QStringLiteral("00"));

    if (pos == MAXINT) {
        tmpOrd->set_stag_id(QString::number(Cod_uni()));
        m_ordini.append(tmpOrd);
    } else {
        if (pos > Count() + 1 || pos < 1) {
            log_str(QStringLiteral("Add: Posizione maggiore del numero di ordini presenti"), MED_PRIORITY, CosLog::Ord);
            return RET_BAD;
        }
        tmpOrd->set_stag_id(QString::number(Cod_uni()));
        if (Count() > 0 && pos <= Count())
            m_ordini.insert(pos - 1, tmpOrd);
        else
            m_ordini.append(tmpOrd);
    }
    m_punOrd = (pos > 0 && pos != MAXINT) ? pos : Count();
    Salva(false);
    return RET_OK;
}

int CollezioneOrdini::Del_blk(int ini, int fin, bool shift, bool cmdSpde, bool aggRepProOldOrd)
{
    const int count = Count();
    if (ini > count || fin > count || ini < 1 || fin < 1) {
        log_str(QStringLiteral("Del_blk: Un dato eccedente il numero di ordini presenti"), MED_PRIORITY, CosLog::Ord);
        return RET_BAD;
    }
    if (ini > fin)
        std::swap(ini, fin);
    if (m_ese && aggRepProOldOrd) {
        m_ordini.remove(ini - 1, fin - ini + 1);
        Salva(shift);
        log_str(QStringLiteral("Del_blk: Cancellazione ordine da ord_mac_ese per aggiornarlo con i nuovi dati di produzione"), MED_PRIORITY, CosLog::Ord);
        return RET_OK;
    }
    if (manualeOrSpdeKo() || !par_abi.abi_com_PU || shift || cmdSpde) {
        m_ordini.remove(ini - 1, fin - ini + 1);
        m_punOrd = ini > 1 ? ini - 1 : 1;
        Salva(shift);
        return RET_OK;
    }
    messaggio(msg(QStringLiteral("mac_in_auto")), true, false);
    log_str(QStringLiteral("Del_blk: Tentativo di cancellazione di ordini con macchina in automatico"), MED_PRIORITY, CosLog::Ord);
    return RET_BAD;
}

void CollezioneOrdini::Leg_dat_ord()
{
    m_punOrd = 0;
    const QString codPrimo = vb::fixedLength(
        vb::trim(getFileINIString(QStringLiteral("ORD_SET"), m_ese ? QStringLiteral("Cod_primo_ese") : QStringLiteral("Cod_primo"), QStringLiteral("9000"), pro_set.Nam_fil_uni)), 4);
    if (!QFile::exists(fileName()))
        Crea_fil_ord();
    const QVector<QByteArray> records = recordfile::read(fileName(), kOrderRecordSize, m_ese ? MAX_NUM_ORD_ESE : MAX_NUM_ORD);
    for (int i = 1; i <= records.size(); ++i) {
        auto ord = std::make_shared<ClasseOrdine>();
        ord->setOrdine(records[i - 1]);
        if (i == 1 && ord->stag_id() != codPrimo && vb::val(ord->stag_id()) != 0) {
            log_str(QStringLiteral("leg_dat_ord: Il codice del primo ordine non corrisponde a quello memorizzato"), MED_PRIORITY, CosLog::Ord);
            Prb_lis_ord = true;
        }
        if (vb::val(ord->stag_id()) != 0) {
            m_ordini.append(ord);
            if (i == 1)
                m_punOrd = 1;
        }
    }
}

void CollezioneOrdini::Crea_fil_ord()
{
    const int count = m_ese ? MAX_NUM_ORD_ESE : MAX_NUM_ORD;
    recordfile::write(fileName(), QVector<QByteArray>(count, ClasseOrdine().ordine()), kOrderRecordSize);
}

OrdinePtr CollezioneOrdini::item(int indice) const
{
    if (indice < 1 || indice > Count()) {
        log_str(QStringLiteral("Ord_lis: Indice %1 fuori range").arg(indice), MED_PRIORITY, CosLog::Ord);
        return std::make_shared<ClasseOrdine>();
    }
    return m_ordini[indice - 1];
}

void CollezioneOrdini::pun_ord_mov_next()
{
    m_punOrd = m_punOrd < Count() ? m_punOrd + 1 : m_punOrd;
}

void CollezioneOrdini::pun_ord_mov_prev()
{
    m_punOrd = m_punOrd > 1 ? m_punOrd - 1 : m_punOrd;
}

void CollezioneOrdini::Salva(bool shift)
{
    const int count = m_ese ? MAX_NUM_ORD_ESE : MAX_NUM_ORD;
    QVector<QByteArray> records;
    records.reserve(count);
    const QByteArray nul = ClasseOrdine().ordine();
    for (int i = 1; i <= count; ++i)
        records.append(i <= Count() ? m_ordini[i - 1]->ordine() : nul);
    QString error;
    if (!recordfile::write(fileName(), records, kOrderRecordSize, &error))
        log_str(QStringLiteral("Salva: ") + error, MED_PRIORITY, CosLog::Ord);
    emit Refresh(shift);
}

int CollezioneOrdini::Tro_id_lis(QString id)
{
    id = (QStringLiteral("0000") + id).right(4);
    for (int i = 1; i <= Count(); ++i) {
        if (vb::trim(m_ordini[i - 1]->stag_id()) == id) {
            m_punOrd = i;
            return RET_OK;
        }
    }
    log_str(QStringLiteral("Tro_id_lis: Ordine con Stag_id ") + id + QStringLiteral(" non trovato"), MED_PRIORITY, CosLog::Ord);
    return RET_BAD;
}

int CollezioneOrdini::Tro_lis(QString cod)
{
    cod = vb::trim(cod).toUpper();
    for (int i = 1; i <= Count(); ++i) {
        if (vb::trim(m_ordini[i - 1]->cod_ord_abb()).toUpper() == cod) {
            m_punOrd = i;
            return RET_OK;
        }
    }
    log_str(QStringLiteral("Tro_lis: Ordine con codice ") + cod + QStringLiteral(" non trovato"), MED_PRIORITY, CosLog::Ord);
    return RET_BAD;
}
