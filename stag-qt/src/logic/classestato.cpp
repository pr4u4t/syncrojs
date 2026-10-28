#include "classestato.h"

#include "collezioneordini.h"
#include "finsclient.h"
#include "gesord.h"
#include "inistore.h"
#include "parcurtag.h"
#include "parmac.h"
#include "stagrecords.h"
#include "staglog.h"
#include "stagglobals.h"
#include "uihooks.h"
#include "vbcompat.h"
#include "vbtypes.h"

#include <cmath>
#include <functional>

using namespace stag;

namespace {

constexpr int VEL_MAX = 999;

QString fixedNumber(qint64 value, int length)
{
    return vb::fixedLength(QString::number(value), length);
}

// uti_stag.bin_hex: the bit string is MSB first, digits read with Val().
qint64 binValue(const QString &bits)
{
    qint64 h = 0;
    for (const QChar c : bits)
        h = qint64(vb::val(QString(c))) + 2 * h;
    return h;
}

} // namespace

ClasseStato::ClasseStato(QObject *parent)
    : QObject(parent)
    , m_totEse(orderProductionLayout())
    , m_totTg(shiftProductionLayout())
{
    m_stato.spdeOK = true;
    m_tickCount.start();
}

bool ClasseStato::bit(StaPlcBit b, int v) const
{
    return int(vb::val(m_stato.staPlc.mid(b, 1))) == v;
}

bool ClasseStato::gruAllAtt(int posGru) const
{
    if (posGru < 1 || posGru > 7) {
        log_str(QStringLiteral("Property Get GRU_ALL_ATT della ClasseStato: l'indice passato alla property non \u00E8 corretto"), MED_PRIORITY, CosLog::Gen);
        return false;
    }
    return m_stato.gruAllAtt[posGru];
}

void ClasseStato::setGruAllAtt(int posGru, bool v)
{
    if (posGru < 1 || posGru > 7) {
        log_str(QStringLiteral("Property Let GRU_ALL_ATT della ClasseStato: l'indice passato alla property non \u00E8 corretto"), MED_PRIORITY, CosLog::Gen);
        return;
    }
    m_stato.gruAllAtt[posGru] = v;
}

void ClasseStato::chkAllSftw()
{
    constexpr int ORD_COR_ASS = 1;
    constexpr int ORD_SUC_ASS = 2;
    constexpr int COM_SPDE_NO = 4;
    constexpr int COM_PLC_NO = 8;
    constexpr int COM_ST_INI_1_NO = 16;

    qint64 valWar = 0;
    bool all = false;
    const int numOrd = ord_mac ? ord_mac->Count() : 0;
    if (numOrd < 2) {
        all = true;
        valWar += ORD_SUC_ASS;
        if (numOrd == 0)
            valWar += ORD_COR_ASS;
    }
    if (par_abi.abi_com_PU && par_abi.abi_err_com_PU) {
        if (!m_stato.spdeOK && m_stato.staPlc.mid(MAC_IN_MANUALE, 1) != QStringLiteral("1")) {
            if (pro_set.per_cli != QStringLiteral("LINPAC_ATLANTA"))
                valWar += COM_SPDE_NO;
            all = true;
        }
    }
    if (!m_stato.comOK) {
        valWar += COM_PLC_NO;
        all = true;
    }
    for (int i = 1; i <= int(par_mac.car_tec.num_liv_tag.val()) && i <= 3; ++i) {
        if (!com_ini_sch_tag(i)) {
            valWar += COM_ST_INI_1_NO * (1 << (i - 1));
            all = true;
        }
    }

    m_stato.allAtt = m_stato.allAtt.left(m_stato.allAtt.size() - 16) + vb::hexBin(vb::intEsa(valWar), true);

    if (all) {
        double valFin = vb::hexDec(vb::intEsa(binValue(m_stato.gruAll)));
        const qint64 mal = qint64(1) << POS_GRU_MAL;
        if ((qint64(valFin) & mal) != mal) {
            valFin += double(mal);
            m_stato.gruAll = vb::hexBin(vb::intEsa(qint64(valFin)), false);
        }
        if (!m_stato.comOK && m_oldStato.allAtt != m_stato.allAtt) {
            m_oldStato.allAtt = m_stato.allAtt;
            emit sezall(m_stato.allAtt);
            emit gruall(m_stato.gruAll);
        }
    }
}

void ClasseStato::aggiornaSpde()
{
    if (m_iniSpde == 0) {
        m_oldSpde = true;
        m_iniSpde = 1;
    }
    if (m_oldSpde != m_stato.spdeOK) {
        emit spde(m_stato.spdeOK);
        m_oldSpde = m_stato.spdeOK;
    }
}

void ClasseStato::assStato(const QVector<qint32> &dm)
{
    const QString plcLogCur = QStringLiteral("%1*%2*%3*%4").arg(dm[2]).arg(dm[3]).arg(dm[8]).arg(dm[13]);
    if (plcLogCur != m_oldPlcLogCur) {
        log_str(QStringLiteral("Plc_cur: ") + plcLogCur, MED_PRIORITY, CosLog::Ord);
        m_oldPlcLogCur = plcLogCur;
    }
    m_stato.staPlc = vb::hexBin(vb::intEsa(dm[0]), true) + vb::hexBin(vb::intEsa(dm[1]), true);
    if (m_ctx.macInManualeUte)
        m_stato.staPlc[MAC_IN_MANUALE] = u'1';
    m_stato.codOrdCor = fixedNumber(dm[2], 4);
    for (int liv = 1; liv <= 3; ++liv) {
        const int base = 3 + (liv - 1) * 5;
        TipOrdPlc &o = m_stato.ordPlc[liv];
        o.lunTag = dm[base];
        o.numFogBuo = vb::legLng(vb::intEsa(dm[base + 1]), vb::intEsa(dm[base + 2]));
        o.numFogErr = dm[base + 3];
        o.errTag = dm[base + 4];
    }
    m_stato.codOrdSuc = fixedNumber(dm[20], 4);
    for (int liv = 1; liv <= 3; ++liv) {
        m_stato.lunTagSuc[liv] = dm[20 + liv];
        m_stato.nextCutLenFromExternal[liv] = dm[24 + liv];
    }
    m_stato.velLin[1] = dm[50];
    const int presVel = dm[50];
    if (std::abs(presVel - m_oldVel) > 2) {
        log_str(QStringLiteral("Speed: %1").arg(presVel), MED_PRIORITY, CosLog::Plc);
        m_oldVel = presVel;
    }
    m_stato.velLin[2] = dm[51];
    m_stato.velLin[3] = dm[52];
    m_stato.velAnaSanIng = dm[53];
    m_stato.velSanIng = dm[54];
    for (int i = 1; i <= 3; ++i)
        m_stato.velSanUsc[i] = dm[54 + i];

    QString strAll = vb::hexBin(vb::intEsa(dm[60]), false);
    for (int i = 61; i <= 80; ++i)
        strAll += vb::hexBin(vb::intEsa(dm[i]), true);
    // CopyType over Tip_all_PLC: the last 16 characters of All_att (software alarms) are kept.
    m_stato.gruAll = strAll.left(16);
    m_stato.allAtt = strAll.mid(16) + m_stato.allAtt.mid(strAll.size() - 16);
    m_stato.ris3500 = fixedNumber(dm[98], 16);
    m_stato.ris3501 = fixedNumber(dm[99], 16);
}

int ClasseStato::firstOne(const QString &bits)
{
    const int index = bits.indexOf(u'1');
    return index < 0 ? 0 : index + 1;
}

QString ClasseStato::runString(const Stato &s) const
{
    QString r;
    for (StaPlcBit b : {MAC_CAM_ORD_IN_CORSO, MAC_CAM_ORD_COMPLETATO, MAC_IN_PREALLARME, MAC_PRONTA_AL_CAMBIO, MAC_IN_MARCIA, MAC_PRONTA_MARCIA})
        r += s.staPlc.at(b);
    return r;
}

QString ClasseStato::resetString(const Stato &s) const
{
    QString r;
    for (StaPlcBit b : {MAC_E_STOP, MAC_F_STOP, MAC_N_STOP, MAC_RIPRISTINATA})
        r += s.staPlc.at(b);
    return r;
}

QString ClasseStato::macString(const Stato &s) const
{
    QString r;
    for (StaPlcBit b : {MAC_IN_MANUTENZIONE, MAC_IN_SIMULAZIONE, MAC_IN_LAPPATURA})
        r += s.staPlc.at(b);
    return r;
}

const ClasseStato::TipOrdPlc &ClasseStato::ordPlcOf(const Stato &s, int i)
{
    if (i < 1 || i > 3)
        vb::raise(9);
    return s.ordPlc[std::size_t(i)];
}

const ClasseStato::TipOrdPlc &ClasseStato::ordPlcAt(int i) const
{
    return ordPlcOf(m_stato, i);
}

namespace {

qint32 at3(const std::array<qint32, 4> &a, int i)
{
    if (i < 1 || i > 3)
        vb::raise(9);
    return a[std::size_t(i)];
}

// sca_lun \ TODECMM on the String field: the text must be numeric.
qint32 lunMm(const ClasseOrdine &o, int i)
{
    return vb::intDiv(vb::cLngStr(o.ord(i, QStringLiteral("sca_lun"))), TODECMM);
}

} // namespace

void ClasseStato::aggiorna(FinsClient &fins)
{
    m_fins = &fins;
    if (fins.error()) {
        emit mac(ness_mac);
        emit reset(ness_reset);
        emit run(7);
        emit gruall(QStringLiteral("0"));
        m_stato.comOK = false;
        return;
    }
    try {
        aggiornaBody(fins);
    } catch (const vb::Error &e) {
        m_oldStato = m_stato;
        if (e.number == 6) {
            messaggio(QStringLiteral("OVERFLOW"), true, true);
            log_str(QStringLiteral("sub aggiorna: OVERFLOW (6)"), MED_PRIORITY, CosLog::Gen);
        } else {
            log_str(QStringLiteral("on error intervenuta in ClasseStato aggiorna: %1 (%2)").arg(QString::fromLatin1(e.what())).arg(e.number), MED_PRIORITY, CosLog::Gen);
        }
    }
}

void ClasseStato::aggiornaBody(FinsClient &fins)
{
    fins.readArea(3600, 100);
    const QVector<qint32> dm = fins.retData();
    if (dm.size() < 100)
        vb::raise(9);
    assStato(dm);
    const int numOrd = ord_mac ? ord_mac->Count() : 0;
    chkAllSftw();

    if (m_oldStato.gruAll != m_stato.gruAll || m_ini == 0)
        emit gruall(m_stato.gruAll);
    if (m_oldStato.allAtt != m_stato.allAtt || m_ini == 0)
        emit sezall(m_stato.allAtt);

    const bool camOrdInCorso0 = m_stato.staPlc.mid(MAC_CAM_ORD_IN_CORSO, 1) == QStringLiteral("0");

    // GESTIONE PER L'ORDINE SUCCESSIVO
    if (numOrd >= 2 && camOrdInCorso0) {
        const OrdinePtr o = ord_mac->item(2);
        for (int i = 1; i <= 3; ++i) {
            const QString ordLiv = o->ord(i, QStringLiteral("Ord_liv"));
            if (vb::trim(ordLiv).isEmpty())
                continue;
            const int l = vb::cIntStr(ordLiv);
            if (m_oldStato.codOrdSuc != m_stato.codOrdSuc || at3(m_stato.lunTagSuc, l) != at3(m_oldStato.lunTagSuc, l)
                || m_oldNumOrd != numOrd || m_camLin) {
                if (vb::cIntStr(m_stato.codOrdSuc) == vb::cIntStr(o->stag_id())
                    && (ordineVuoto(*o) || at3(m_stato.lunTagSuc, l) == vb::intDiv(vb::val(o->ord(i, QStringLiteral("sca_lun"))), TODECMM))) {
                    emit ord(ord_ok, SUCC);
                } else {
                    emit ord(ord_nok, SUCC);
                    break;
                }
            }
        }
    } else if (numOrd < 2) {
        if (m_oldNumOrd != numOrd || m_ini == 0 || m_camLin)
            emit ord(ord_ass, SUCC);
    }

    // GESTIONE PER L'ORDINE CORRENTE
    QString idCor;
    std::array<bool, 4> rieForOrdCor{};
    if (numOrd >= 1) {
        const OrdinePtr o = ord_mac->item(1);
        if (camOrdInCorso0) {
            idCor = vb::trim(o->stag_id()) + vb::trim(o->mod_id());
            for (int i = 1; i <= 3; ++i) {
                const QString ordLiv = o->ord(i, QStringLiteral("Ord_liv"));
                if (vb::trim(ordLiv).isEmpty())
                    continue;
                const int l = vb::cIntStr(ordLiv);
                if (m_oldStato.codOrdCor != m_stato.codOrdCor || ordPlcOf(m_stato, l).lunTag != ordPlcOf(m_oldStato, l).lunTag
                    || numOrd != m_oldNumOrd || m_camLin || m_ini == 0 || m_oldDisCur) {
                    if (vb::cIntStr(m_stato.codOrdCor) == vb::cIntStr(o->stag_id())
                        && (ordineVuoto(*o) || ordPlcOf(m_stato, l).lunTag == vb::intDiv(vb::val(o->ord(i, QStringLiteral("sca_lun"))), TODECMM))) {
                        emit ord(ord_ok, CORR);
                        if (m_oldDisCur) {
                            m_oldDisCur = false;
                            log_str(QStringLiteral("---Fine corr.dis."), MED_PRIORITY, CosLog::Ord);
                        }
                    } else {
                        emit ord(ord_nok, CORR);
                        if (!m_oldDisCur) {
                            m_oldDisCur = true;
                            log_str(QStringLiteral("--->Corr.dis. Plc=%1, Stag=%2, lev=%3: Plc=%4, Stag=%5")
                                        .arg(m_stato.codOrdCor, o->stag_id())
                                        .arg(i)
                                        .arg(ordPlcOf(m_stato, l).lunTag)
                                        .arg(lunMm(*o, i)),
                                    MED_PRIORITY, CosLog::Ord);
                        }
                        break;
                    }
                }
            }
        }

        for (int i = 1; i <= 3; ++i) {
            const int livTmp = o->ind_liv(i);
            if (livTmp != 0) {
                rieForOrdCor[std::size_t(livTmp)] = true;
                const int l = int(vb::val(o->ord(i, QStringLiteral("Ord_liv"))));
                const int velCurTag = calVelCurTag(l, vb::intDiv(vb::val(o->ord(i, QStringLiteral("sca_lun"))), TODECMM));
                vel_max_tag_liv(livTmp) = qint16(std::min(int(par_mac.par_cfg_att(l).vel_max_tag.val()), velCurTag));

                const TipSchTagLiv &st = par_mac.sch_tag.sch_tag_liv(l);
                if (ordPlcOf(m_oldStato, l).errTag != ordPlcOf(m_stato, l).errTag || numOrd != m_oldNumOrd || idCor != m_oldIdCor
                    || m_oldLimInf[std::size_t(l)] != vb::cLngStr(st.lim_inf_tol_tag) || m_oldLimSup[std::size_t(l)] != vb::cLngStr(st.lim_sup_tol_tag)
                    || m_ini == 0 || m_oldAbiMisAng != abi_mis_ang) {
                    m_oldLimInf[std::size_t(l)] = vb::cLngStr(st.lim_inf_tol_tag);
                    m_oldLimSup[std::size_t(l)] = vb::cLngStr(st.lim_sup_tol_tag);
                    qint32 errTag = ordPlcOf(m_stato, l).errTag;
                    if ((errTag & 0x8000) == 0x8000)
                        errTag -= 65536;
                    emit err(livTmp, QString::number(errTag));
                }
                const double scaPerTag = vb::val(o->ord(i, QStringLiteral("Sca_per_tag")));
                if (ordPlcOf(m_oldStato, l).numFogBuo != ordPlcOf(m_stato, l).numFogBuo || numOrd != m_oldNumOrd || idCor != m_oldIdCor || m_ini == 0)
                    emit num(livTmp, vb::cStr(ordPlcOf(m_stato, l).numFogBuo * scaPerTag));
                if (ordPlcOf(m_oldStato, l).numFogErr != ordPlcOf(m_stato, l).numFogErr || numOrd != m_oldNumOrd || m_ini == 0 || idCor != m_oldIdCor)
                    emit numerr(livTmp, vb::cStr(ordPlcOf(m_stato, l).numFogErr * scaPerTag));
            } else {
                for (int k = 1; k <= 3; ++k) {
                    if (!rieForOrdCor[std::size_t(k)]) {
                        emit err(k, QString());
                        emit numerr(k, QString());
                        emit num(k, QString());
                    }
                }
            }
        }
    } else if (m_oldNumOrd != numOrd || m_ini == 0 || m_camLin) {
        emit ord(ord_ass, CORR);
        switch (int(par_mac.car_tec.num_liv_tag.val())) {
        case 1:
            vel_max_tag_liv(3) = qint16(par_mac.par_cfg_att(1).vel_max_tag.val());
            break;
        case 2:
            vel_max_tag_liv(1) = qint16(par_mac.par_cfg_att(1).vel_max_tag.val());
            vel_max_tag_liv(3) = qint16(par_mac.par_cfg_att(2).vel_max_tag.val());
            break;
        case 3:
            for (int k = 1; k <= 3; ++k)
                vel_max_tag_liv(k) = qint16(par_mac.par_cfg_att(k).vel_max_tag.val());
            break;
        default:
            break;
        }
    }

    if (m_stato.velLin[1] != m_oldStato.velLin[1] || m_ini == 0 || numOrd != m_oldNumOrd || m_oldAbiMisAng != abi_mis_ang)
        emit vel(m_stato.velLin[1]);

    const auto minCfgVelMax = [] {
        int v = int(par_mac.par_cfg_att(1).vel_max_tag.val());
        for (int i = 2; i <= int(par_mac.car_tec.num_liv_tag.val()); ++i)
            v = std::min(v, int(par_mac.par_cfg_att(i).vel_max_tag.val()));
        return v;
    };
    int velMaxTag;
    if (numOrd > 0) {
        velMaxTag = VEL_MAX;
        for (int i = 1; i <= 3; ++i) {
            if (rieForOrdCor[std::size_t(i)] && velMaxTag > vel_max_tag_liv(i))
                velMaxTag = vel_max_tag_liv(i);
        }
    } else {
        velMaxTag = minCfgVelMax();
    }
    if (velMaxTag == VEL_MAX)
        velMaxTag = minCfgVelMax();
    if (velMaxTag != m_oldVelMaxTag || m_ini == 0 || numOrd != m_oldNumOrd || m_oldAbiMisAng != abi_mis_ang) {
        m_oldVelMaxTag = velMaxTag;
        emit velmax(velMaxTag);
    }
    m_oldNumOrd = numOrd;
    m_oldIdCor = idCor;

    if (runString(m_oldStato) != runString(m_stato) || m_camLin) {
        const int k = firstOne(runString(m_stato));
        m_stato.staRun = k >= 1 && k <= 6 ? k : fer;
        emit run(m_stato.staRun);
    }

    tempi(false, false, false);

    // GESTIONE DEL BIT DI CAMBIO ORDINE COMPLETATO
    const QString coCompletato = m_stato.staPlc.mid(MAC_CAM_ORD_COMPLETATO, 1);
    if (m_oldStato.staPlc.mid(MAC_CAM_ORD_COMPLETATO, 1) != coCompletato) {
        if (coCompletato == QStringLiteral("1")) {
            if (vb::trim(pro_set.ini_prod) == QString::fromLatin1(STR_TRUE)
                && vb::trim(ord_mac->item(1)->stag_id()) == vb::trim(ord_mac_ese->item(1)->old_stag_id())) {
                if (ord_mac_ese->Del_blk(1, 1, false, false, true) != RET_OK)
                    log_str(QStringLiteral("aggiorna: Cancellazione ordine da ord_mac_ese per aggiornarlo con i nuovi dati di produzione NON ESEGUITA"), MED_PRIORITY, CosLog::Ord);
                pro_set.ini_prod = QString::fromLatin1(STR_FALSE);
                setFileINIString(QStringLiteral("PRO_SET"), QStringLiteral("ini_prod"), pro_set.ini_prod);
            }
            Shifta();
        } else if (m_ini == 1) {
            rea_dat_ord_pro(false);
        }
    }

    if (resetString(m_oldStato) != resetString(m_stato) || m_camLin) {
        switch (firstOne(resetString(m_stato))) {
        case 1: emit reset(e_stop); break;
        case 2: emit reset(f_stop); break;
        case 3: emit reset(n_stop); break;
        case 4: emit reset(rip); break;
        default: emit reset(ness_reset); break;
        }
    }

    const QString macNow = macString(m_stato);
    if (macString(m_oldStato) != macNow || m_camLin) {
        int k = firstOne(macNow);
        if (k == 1 && macNow.at(2) == u'1')
            k = 3;
        switch (k) {
        case 1: emit mac(mac_in_man); break;
        case 2: emit mac(mac_in_sim); break;
        case 3: emit mac(mac_in_lap); break;
        default: emit mac(ness_mac); break;
        }
    }

    if (m_stato.staPlc.at(MAC_IN_MANUALE) != m_oldStato.staPlc.at(MAC_IN_MANUALE) || m_camLin)
        emit modo(m_stato.staPlc.at(MAC_IN_MANUALE) == u'1' ? manuale : automatico);
    if (m_camLin || m_ini == 0)
        emit liv();

    if (m_stato.staPlc != m_oldStato.staPlc)
        log_str(QStringLiteral("Stato Macchina: ") + m_stato.staPlc, MED_PRIORITY, CosLog::Plc);

    m_oldStato = m_stato;
    m_oldAbiMisAng = abi_mis_ang;
    m_ini = 1;
    m_camLin = false;
}

void ClasseStato::tempi(bool resOrd, bool resTur, bool resGio)
{
    const QString tur = QStringLiteral("tur(%1).").arg(pro_set.tur);
    const auto setNow = [this](const QString &group) {
        const QDateTime now = QDateTime::currentDateTime();
        m_totTg.setText(group + QStringLiteral("dat_ini"), now.toString(QStringLiteral("yyyyMMdd")));
        m_totTg.setText(group + QStringLiteral("ora_ini"), now.toString(QStringLiteral("hhmmss")));
    };
    const auto blank = [](FixedRecord &r, const QString &prefix) {
        for (const RecordLayout::Field &f : r.layout().fields()) {
            if (f.name.startsWith(prefix))
                r.setText(f.name, QString());
        }
    };
    const auto add = [](FixedRecord &r, const QString &field, qint64 inc) {
        r.setText(field, vb::cStr(vb::val(r.text(field)) + double(inc)));
    };

    if (resOrd) {
        blank(m_totEse, QString());
        m_lasTicFerOrd = 0;
    }
    if (resGio) {
        blank(m_totTg, QString());
        m_lasTicFerGio = 0;
        m_lasTicFerTur = 0;
        setNow(QStringLiteral("gio."));
        setNow(tur);
    }
    if (resTur) {
        blank(m_totTg, tur);
        m_lasTicFerTur = 0;
        setNow(tur);
    }

    // GetTickCount(): never 0 once running.
    const qint64 tic = m_tickCount.elapsed() + 1;
    if (m_lasTic == 0) {
        m_lasTic = tic;
        return;
    }
    if (tic - m_lasTic < 1000)
        return;

    if (pro_set.abi_reg_rep_pro == QString::fromLatin1(STR_TRUE)) {
        if (m_stato.velSanIng != 0) {
            m_lasTicFerOrd = 0;
            m_lasTicFerGio = 0;
            m_lasTicFerTur = 0;
        } else {
            if (m_lasTicFerOrd == 0) {
                m_lasTicFerOrd = tic;
                add(m_totEse, QStringLiteral("num_fer"), 1);
                add(m_totTg, QStringLiteral("gio.num_fer"), 1);
                add(m_totTg, tur + QStringLiteral("num_fer"), 1);
            } else {
                const qint64 inc = (tic - m_lasTicFerOrd) / 1000;
                add(m_totEse, QStringLiteral("tem_fer"), inc);
                m_lasTicFerOrd += inc * 1000;
            }
            if (m_lasTicFerGio == 0) {
                m_lasTicFerGio = tic;
            } else {
                const qint64 inc = (tic - m_lasTicFerGio) / 1000;
                add(m_totTg, QStringLiteral("gio.tem_fer"), inc);
                m_lasTicFerGio += inc * 1000;
            }
            if (m_lasTicFerTur == 0) {
                m_lasTicFerTur = tic;
            } else {
                const qint64 inc = (tic - m_lasTicFerTur) / 1000;
                add(m_totTg, tur + QStringLiteral("tem_fer"), inc);
                m_lasTicFerTur += inc * 1000;
            }
        }
    } else {
        m_lasTicFerOrd = 0;
        m_lasTicFerGio = 0;
        m_lasTicFerTur = 0;
    }

    for (const QString &group : {tur, QStringLiteral("gio.")}) {
        const QString datIni = vb::trim(m_totTg.text(group + QStringLiteral("dat_ini")));
        const QString oraIni = vb::trim(m_totTg.text(group + QStringLiteral("ora_ini")));
        if (datIni.isEmpty() || oraIni.isEmpty())
            continue;
        const QDate d(int(vb::val(datIni.mid(0, 4))), int(vb::val(datIni.mid(4, 2))), int(vb::val(datIni.mid(6, 2))));
        const QTime t(int(vb::val(oraIni.mid(0, 2))), int(vb::val(oraIni.mid(2, 2))), int(vb::val(oraIni.mid(4, 2))));
        if (!d.isValid() || !t.isValid()) {
            log_str(QStringLiteral("tempi: errore 5, Invalid procedure call or argument"), MED_PRIORITY, CosLog::Lis);
            continue;
        }
        const qint64 sec = QDateTime(d, t).secsTo(QDateTime::currentDateTime());
        m_totTg.setText(group + QStringLiteral("tem_mar"), vb::cStr(double(sec) - vb::val(m_totTg.text(group + QStringLiteral("tem_fer")))));
    }
    m_lasTic = tic;
}

qint32 ClasseStato::agg_med(const QString &group) const
{
    const QString datIni = vb::trim(m_totTg.text(group + QStringLiteral("dat_ini")));
    const QString oraIni = vb::trim(m_totTg.text(group + QStringLiteral("ora_ini")));
    if (datIni.isEmpty() || oraIni.isEmpty())
        return 0;
    const QDateTime start(QDate(int(vb::val(datIni.mid(0, 4))), int(vb::val(datIni.mid(4, 2))), int(vb::val(datIni.mid(6, 2)))),
                          QTime(int(vb::val(oraIni.mid(0, 2))), int(vb::val(oraIni.mid(2, 2))), int(vb::val(oraIni.mid(4, 2)))));
    const qint64 sec = start.secsTo(QDateTime::currentDateTime());
    if (sec == 0)
        return 0;
    bool ok = true;
    const qint32 v = vb::cLng(vb::val(m_totTg.text(group + QStringLiteral("met_lin"))) / double(sec) * 60, &ok);
    if (!ok)
        vb::raise(6);
    return v;
}

void ClasseStato::lunghezza(qint32 met, qint32 larMm)
{
    if (m_iniLunghezza != 0) {
        qint32 dif = met - m_metCur;
        if (dif < 0)
            dif = met;
        const qint32 difq = qint32(std::floor((double(dif) * larMm + m_resq) / 1000));
        m_resq = dif * larMm + m_resq - difq * 1000;
        const QString tur = QStringLiteral("tur(%1).").arg(pro_set.tur);
        for (const QString &group : {QStringLiteral("gio."), tur}) {
            m_totTg.setText(group + QStringLiteral("met_lin"), vb::cStr(vb::val(m_totTg.text(group + QStringLiteral("met_lin"))) + dif));
            m_totTg.setText(group + QStringLiteral("met_qua"), vb::cStr(vb::val(m_totTg.text(group + QStringLiteral("met_qua"))) + difq));
        }
        m_totTg.setText(QStringLiteral("gio.vel_med"), vb::cStr(agg_med(QStringLiteral("gio."))));
        m_totTg.setText(tur + QStringLiteral("vel_med"), vb::cStr(agg_med(tur)));
    } else {
        m_iniLunghezza = 1;
    }
    m_metCur = met;
}

void ClasseStato::chg_tur(int newTur)
{
    const QString tur = QStringLiteral("tur(%1).").arg(pro_set.tur);
    const QDateTime now = QDateTime::currentDateTime();
    m_totTg.setText(tur + QStringLiteral("dat_fin"), now.toString(QStringLiteral("yyyyMMdd")));
    m_totTg.setText(tur + QStringLiteral("ora_fin"), now.toString(QStringLiteral("hhmmss")));
    pro_set.tur = qint16(newTur);
    tempi(false, true, false);
    setFileINIString(QStringLiteral("pro_set"), QStringLiteral("tur"), QString::number(newTur));
}

void ClasseStato::chg_ord()
{
    const QString tur = QStringLiteral("tur(%1).").arg(pro_set.tur);
    m_totTg.setText(tur + QStringLiteral("num_cam"), vb::cStr(vb::val(m_totTg.text(tur + QStringLiteral("num_cam"))) + 1));
    m_totTg.setText(QStringLiteral("gio.num_cam"), vb::cStr(vb::val(m_totTg.text(QStringLiteral("gio.num_cam"))) + 1));
}

void ClasseStato::rea_dat_ord_pro(bool fineProd)
{
    if (pro_set.abi_reg_rep_pro != QString::fromLatin1(STR_TRUE))
        return;
    if (!ord_mac_ese || ord_mac_ese->Count() == 0) {
        log_str(QStringLiteral("Sub rea_dat_ord_pro della ClasseStato. Lettura dei dati dell'ultimo ordine prodotto: la lista degli ordini eseguiti risulta vuota"), MED_PRIORITY, CosLog::Lis);
        return;
    }
    ClasseOrdine temp;
    temp.setOrdine(ord_mac_ese->item(1)->ordine());
    const int numLiv = int(par_mac.car_tec.num_liv_tag.val());
    float m = 0;

    // normal = change of order (scrap from the PLC report), otherwise end of production.
    const auto assign = [&](const std::function<double(int)> &buo, const std::function<double(int)> &errF, const std::function<double(int)> &sca,
                            const std::function<QString(int)> &milSca, bool normal) {
        for (int i = 1; i <= 3; ++i) {
            const QString f = QStringLiteral("rep_pro.liv(%1).").arg(i);
            if (vb::val(temp.ord(i, QStringLiteral("Ord_liv"))) == 0)
                continue;
            const int livOrd = int(vb::val(temp.ord(i, QStringLiteral("Ord_liv"))));
            const qint32 tmpRes = qint32(vb::val(temp.ord(i, QStringLiteral("Sca_per_tag"))));
            temp.setText(f + QStringLiteral("fog_buo"), vb::cStr(buo(livOrd) * tmpRes));
            temp.setText(f + QStringLiteral("fog_err"), vb::cStr(errF(livOrd) * tmpRes));
            temp.setText(f + QStringLiteral("fog_sca"), normal ? vb::cStr(sca(livOrd) * tmpRes) : QStringLiteral("0"));
            if (normal)
                temp.setText(f + QStringLiteral("mil_sca"), milSca(livOrd));
            QString line = QStringLiteral("FORMATO: %1 LIVELLO: %2 Fogli Buoni = %3 Fogli Errati = %4 Fogli Scarto = %5")
                               .arg(i).arg(livOrd).arg(temp.text(f + QStringLiteral("fog_buo")), temp.text(f + QStringLiteral("fog_err")), temp.text(f + QStringLiteral("fog_sca")));
            if (normal)
                line += QStringLiteral(" MM Scarto = ") + temp.text(f + QStringLiteral("mil_sca"));
            log_str(line, MED_PRIORITY, CosLog::Lis);
            if (tmpRes > 0) {
                const double lun = (vb::val(temp.ord(i, QStringLiteral("sca_lun"))) / TODECMM) / 1000;
                const double fogli = normal ? buo(livOrd) + sca(livOrd) / tmpRes : buo(livOrd);
                const float tmpMet = float(fogli * lun);
                if (m < tmpMet)
                    m = tmpMet;
            }
        }
    };

    if (!fineProd) {
        if (m_fins)
            m_fins->readArea(3630, 19);
        const QVector<qint32> dmPro = m_fins ? m_fins->retData() : QVector<qint32>();
        const auto d = [&dmPro](int i) -> qint32 {
            if (i < 0 || i >= dmPro.size())
                vb::raise(9);
            return dmPro[i];
        };
        m_stato.codOrdPro = vb::fixedLength(QString::number(d(0)), 4);
        if (vb::trim(m_stato.codOrdPro) != vb::trim(temp.old_stag_id())) {
            log_str(QStringLiteral("Sub rea_dat_ord_pro della ClasseStato. Lettura dei dati dell'ultimo ordine prodotto: l'ID dell'ultimo ordine prodotto non coincide con l'ID del primo ordine della lista ordini eseguiti"), MED_PRIORITY, CosLog::Lis);
            log_str(QStringLiteral("Codice collezione eseguiti: ") + vb::trim(temp.old_stag_id()) + QStringLiteral(" Codice nel PLC: ") + vb::trim(m_stato.codOrdPro), MED_PRIORITY, CosLog::Lis);
            return;
        }
        const auto legLng = [&d](int lo, int hi) {
            bool ok = true;
            const qint32 v = vb::cLng(vb::legLng(vb::intEsa(d(lo)), vb::intEsa(d(hi))), &ok);
            if (!ok)
                vb::raise(6);
            return v;
        };
        for (int liv = 1; liv <= 3; ++liv) {
            TipOrdPro &p = m_stato.ordPro[std::size_t(liv)];
            if (liv == 1 || numLiv > liv - 1) {
                const int base = 1 + (liv - 1) * 4;
                p.numFogBuo = legLng(base, base + 1);
                p.numFogErr = d(base + 2);
                p.numFogSca = d(base + 3);
                p.numMilSca = legLng(14 + (liv - 1) * 2, 15 + (liv - 1) * 2);
            } else {
                p = TipOrdPro();
            }
        }
        log_str(QStringLiteral("REPORT ORDINE ") + vb::trim(m_stato.codOrdPro), MED_PRIORITY, CosLog::Lis);
        log_str(QStringLiteral("Valori letti da PLC"), MED_PRIORITY, CosLog::Lis);
        for (int i = 1; i <= 3; ++i) {
            const TipOrdPro &p = m_stato.ordPro[std::size_t(i)];
            log_str(QStringLiteral("Fogli Buoni = %1 Fogli Errati = %2 Fogli Scarto = %3 MM Scarto = %4").arg(p.numFogBuo).arg(p.numFogErr).arg(p.numFogSca).arg(p.numMilSca), MED_PRIORITY, CosLog::Lis);
        }
        log_str(QStringLiteral("Valori assegnati alla struttura ord_mac_ese"), MED_PRIORITY, CosLog::Lis);
        const auto pro = [this](int liv) -> const TipOrdPro & {
            if (liv < 1 || liv > 3)
                vb::raise(9);
            return m_stato.ordPro[std::size_t(liv)];
        };
        assign([&](int l) { return double(pro(l).numFogBuo); }, [&](int l) { return double(pro(l).numFogErr); },
               [&](int l) { return double(pro(l).numFogSca); }, [&](int l) { return vb::cStr(pro(l).numMilSca); }, true);
    } else {
        log_str(QStringLiteral("FINE PRODUZIONE. REPORT ORDINE -- ID SPDE: ") + vb::trim(temp.text(QStringLiteral("spde_id"))) + QStringLiteral(" ID STAG: ") + vb::trim(temp.stag_id()), MED_PRIORITY, CosLog::Lis);
        log_str(QStringLiteral("Valori letti da PLC"), MED_PRIORITY, CosLog::Lis);
        for (int i = 1; i <= 3; ++i)
            log_str(QStringLiteral("Fogli Buoni = %1 Fogli Errati = %2").arg(vb::cStr(m_stato.ordPlc[std::size_t(i)].numFogBuo)).arg(m_stato.ordPlc[std::size_t(i)].numFogErr), MED_PRIORITY, CosLog::Lis);
        log_str(QStringLiteral("Valori assegnati alla struttura ord_mac_ese"), MED_PRIORITY, CosLog::Lis);
        assign([&](int l) { return ordPlcAt(l).numFogBuo; }, [&](int l) { return double(ordPlcAt(l).numFogErr); }, [](int) { return 0.0; },
               [](int) { return QString(); }, false);
    }
    temp.setText(QStringLiteral("rep_pro.met_tot"), vb::trim(vb::cStrSingle(m)));
    ins_dat_pro(temp);
}

void ClasseStato::ins_dat_pro(ClasseOrdine &tmpOrd)
{
    const QString datIni = vb::trim(tmpOrd.text(QStringLiteral("rep_pro.dat_ini")));
    const QString oraIni = vb::trim(tmpOrd.text(QStringLiteral("rep_pro.ora_ini")));
    const QString datFin = vb::trim(tmpOrd.text(QStringLiteral("rep_pro.dat_fin")));
    const QString oraFin = vb::trim(tmpOrd.text(QStringLiteral("rep_pro.ora_fin")));
    if (!datIni.isEmpty() && !oraIni.isEmpty() && !datFin.isEmpty() && !oraFin.isEmpty()) {
        const qint64 s = vb::dateDiffSeconds(datIni + u' ' + oraIni, datFin + u' ' + oraFin);
        const float met = float(vb::val(vb::trim(tmpOrd.text(QStringLiteral("rep_pro.met_tot")))));
        if (s == 0)
            vb::raise(11);
        bool ok = true;
        const qint16 vel = vb::cInt(double(met) * 60 / double(s), &ok);
        if (!ok)
            vb::raise(6);
        tmpOrd.setText(QStringLiteral("rep_pro.vel_med"), vb::formatStr(QString::number(vel), 3));
    } else {
        tmpOrd.setText(QStringLiteral("rep_pro.vel_med"), QString());
    }
    log_str(QStringLiteral("Velocit\u00E0 Media: ") + vb::cStr(vb::val(tmpOrd.text(QStringLiteral("rep_pro.vel_med")))) + QStringLiteral(" Metri Prodotti: ")
                + vb::cStr(vb::val(tmpOrd.text(QStringLiteral("rep_pro.met_tot")))),
            MED_PRIORITY, CosLog::Lis);
    auto copy = std::make_shared<ClasseOrdine>(tmpOrd);
    if (ord_mac_ese->Modifica(copy, 1, false) == RET_OK)
        log_str(QStringLiteral("Salvataggio dei nuovi dati su collezione ord_mac_ese e su file eseguito"), MED_PRIORITY, CosLog::Lis);
    else
        log_str(QStringLiteral("Salvataggio dei nuovi dati su collezione ord_mac_ese e su file NON eseguito"), MED_PRIORITY, CosLog::Lis);
}
