#include "gesord.h"

#include "classestato.h"
#include "collezioneordini.h"
#include "inistore.h"
#include "parcurtag.h"
#include "parmac.h"
#include "recordfile.h"
#include "stagglobals.h"
#include "staglog.h"
#include "stagrecords.h"
#include "vbtypes.h"

#include <QDateTime>
#include <QFile>
#include <QFileInfo>

#include <cmath>

namespace stag {
namespace {

const QString NAM_FIL_PRD = QStringLiteral("dat_prd.dat");
const QString NAM_FIL_PRD_ESE = QStringLiteral("dat_prd_ord.dat");

QString nowDate()
{
    return vb::cStrDate(vb::now().date());
}

// Right$("0" + Trim$(CStr(Time)), 8)
QString nowTime()
{
    return (QStringLiteral("0") + vb::trim(vb::cStrTime(vb::now().time()))).right(8);
}

// Format$(x, "00") of a Double.
QString f00(double x)
{
    const qint64 n = qint64(std::llround(x));
    return QString::number(n).rightJustified(2, u'0');
}

QByteArray readOrCreate(const QString &name, const QByteArray &fresh)
{
    const QString path = resolveDataFile(name);
    if (!QFile::exists(path) || QFileInfo(path).size() != fresh.size()) {
        QFile::remove(path);
        recordfile::write(path, {fresh}, int(fresh.size()));
        return fresh;
    }
    return recordfile::read(path, int(fresh.size()), 1).value(0);
}

} // namespace

bool ordineVuoto(const ClasseOrdine &rec)
{
    for (int i = 1; i <= 3; ++i) {
        if (vb::val(rec.ord(i, QStringLiteral("Sca_per_tag"))) != 0 && vb::intDiv(vb::val(rec.ord(i, QStringLiteral("sca_lar"))), TODECMM) != 0
            && vb::intDiv(vb::val(rec.ord(i, QStringLiteral("sca_lun"))), TODECMM) != 0)
            return false;
    }
    return true;
}

int Shifta()
{
    int shifta = RET_BAD;
    if (ord_mac->Count() < 1) {
        log_str(QStringLiteral("Shift: non c'\u00E8 nessun ordine in lista"), MED_PRIORITY, CosLog::Lis);
        return shifta;
    }
    const OrdinePtr tmpOrdCor = ord_mac->item(1);
    if (ord_mac->Count() == 1) {
        log_str(QStringLiteral("Modulo: ges_ord - Function: Shifta. L'ordine successivo non \u00E8 presente"), MED_PRIORITY, CosLog::Lis);
    } else {
        const OrdinePtr tmpOrdSuc = ord_mac->item(2);
        log_str(QStringLiteral("CO. Ordine eseguito -- ID SPDE: ") + tmpOrdCor->text(QStringLiteral("spde_id")) + QStringLiteral(" ID STAG: ") + tmpOrdCor->stag_id()
                    + QStringLiteral(" -> Ordine in lavoro -- ID SPDE: ") + tmpOrdSuc->text(QStringLiteral("spde_id")) + QStringLiteral(" ID STAG: ") + tmpOrdSuc->stag_id(),
                MED_PRIORITY, CosLog::Lis);
    }
    const OrdinePtr cor = ord_mac->item(1);
    cor->setText(QStringLiteral("rep_pro.dat_fin"), nowDate());
    cor->setText(QStringLiteral("rep_pro.ora_fin"), nowTime());
    log_str(QStringLiteral("Shifta: memorizzate data e ora di fine ordine"), MED_PRIORITY, CosLog::Lis);
    const FixedRecord ordEse(orderProductionLayout(), sta_tag->get_pro_ese());
    sta_tag->tempi(false, false, false);
    cor->setText(QStringLiteral("rep_pro.tem_fer"), ordEse.text(QStringLiteral("tem_fer")));
    qint64 d = 0;
    try {
        d = vb::dateDiffSeconds(vb::trim(cor->text(QStringLiteral("rep_pro.dat_ini"))) + u' ' + vb::trim(cor->text(QStringLiteral("rep_pro.ora_ini"))),
                                vb::trim(cor->text(QStringLiteral("rep_pro.dat_fin"))) + u' ' + vb::trim(cor->text(QStringLiteral("rep_pro.ora_fin"))));
    } catch (const vb::Error &e) {
        // On Error GoTo g_err ... Resume Next
        log_str(QStringLiteral("Shifta: errore %1, %2").arg(e.number).arg(QString::fromLatin1(e.what())), MED_PRIORITY, CosLog::Gen);
    }
    log_str(QStringLiteral("Shifta: eseguito DateDiff"), MED_PRIORITY, CosLog::Lis);
    cor->setText(QStringLiteral("rep_pro.tem_mar"), vb::cStr(double(d) - vb::val(ordEse.text(QStringLiteral("tem_fer")))));
    cor->setText(QStringLiteral("rep_pro.num_fer"), ordEse.text(QStringLiteral("num_fer")));
    sta_tag->chg_ord();
    sta_tag->tempi(true, false, false);
    if (ord_mac->Count() > 1) {
        const OrdinePtr next = ord_mac->item(2);
        next->setText(QStringLiteral("rep_pro.dat_ini"), nowDate());
        next->setText(QStringLiteral("rep_pro.ora_ini"), nowTime());
    }
    log_str(QStringLiteral("Shifta: memorizzate data e ora di inizio ordine"), MED_PRIORITY, CosLog::Lis);
    const QString tmpStagId = vb::fixedLength(cor->stag_id(), 4);
    const QString tmpModId = vb::fixedLength(cor->mod_id(), 2);
    if (ord_mac->Del_blk(1, 1, true, false) == RET_OK) {
        log_str(QStringLiteral("Shifta: eseguita cancellazione corrente per shift"), MED_PRIORITY, CosLog::Lis);
        tmpOrdCor->setText(QStringLiteral("old_mod_id"), tmpModId);
        tmpOrdCor->setText(QStringLiteral("old_stag_id"), tmpStagId);
        ord_mac_ese->add(tmpOrdCor);
        shifta = RET_OK;
    } else {
        log_str(QStringLiteral("Modulo: ges_ord - Function: Shifta. Impossibile aggiungere il primo ordine nella lista eseguiti"), MED_PRIORITY, CosLog::Lis);
    }
    Log_lst(*ord_mac);
    return shifta;
}

void Log_lst(const CollezioneOrdini &ord)
{
    constexpr int DIM_COL = 15;
    log_str(QStringLiteral("Stato lista: Pos   |ID SPDE        |ID STAG        |"), MED_PRIORITY, CosLog::Lis);
    for (int p = 1; p <= ord_mac->Count(); ++p) {
        const OrdinePtr o = ord.item(p);
        const QString spdeId = o->text(QStringLiteral("spde_id"));
        const QString stagId = o->stag_id();
        if (spdeId.size() <= DIM_COL && stagId.size() <= DIM_COL) {
            const QString pos = QString::number(p);
            log_str(QString(13, u' ') + pos + QString(qMax(0, 6 - int(pos.size())), u' ') + u'|' + spdeId + QString(15 - spdeId.size(), u' ') + u'|' + stagId
                        + QString(15 - stagId.size(), u' ') + u'|',
                    MED_PRIORITY, CosLog::Lis);
        }
    }
}

void iniVelMax()
{
    try {
        if (ord_mac->Count() >= 1) {
            const OrdinePtr tmpOrd = ord_mac->item(1);
            for (int i = 1; i <= 3; ++i) {
                if (vb::trim(tmpOrd->ord(i, QStringLiteral("Ord_liv"))).isEmpty())
                    continue;
                const int livTmp = int(vb::val(tmpOrd->ord(i, QStringLiteral("Ord_liv"))));
                const int livOrd = calLiv(livTmp);
                const qint32 lung = vb::intDiv(vb::val(tmpOrd->ord(i, QStringLiteral("sca_lun"))), TODECMM);
                const int velCurTag = calVelCurTag(livTmp, lung);
                vel_max_tag_liv(livOrd) = qint16(std::min(int(par_mac.par_cfg_att(livTmp).vel_max_tag.val()), velCurTag));
            }
        }
        switch (int(par_mac.car_tec.num_liv_tag.val())) {
        case 1:
            if (vel_max_tag_liv(3) == 0)
                vel_max_tag_liv(3) = qint16(par_mac.par_cfg_att(1).vel_max_tag.val());
            break;
        case 2:
            if (vel_max_tag_liv(1) == 0)
                vel_max_tag_liv(1) = qint16(par_mac.par_cfg_att(1).vel_max_tag.val());
            if (vel_max_tag_liv(3) == 0)
                vel_max_tag_liv(3) = qint16(par_mac.par_cfg_att(2).vel_max_tag.val());
            break;
        case 3:
            for (int i = 1; i <= 3; ++i) {
                if (vel_max_tag_liv(i) == 0)
                    vel_max_tag_liv(i) = qint16(par_mac.par_cfg_att(i).vel_max_tag.val());
            }
            break;
        default:
            break;
        }
    } catch (const vb::Error &e) {
        log_str(QStringLiteral("Ini_vel_max: Err.N.=%1 Description=%2").arg(e.number).arg(QString::fromLatin1(e.what())), MED_PRIORITY, CosLog::Gen);
    }
}

void leggiPro()
{
    sta_tag->tempi(false, false, true);
    FixedRecord datPrd(shiftProductionLayout(), readOrCreate(NAM_FIL_PRD, sta_tag->get_pro_tg()));
    if (vb::val(datPrd.text(QStringLiteral("gio.tem_fer"))) > 600000000 || vb::val(datPrd.text(QStringLiteral("gio.met_lin"))) > 600000000)
        sta_tag->set_pro_tg(QByteArray(kShiftProductionSize, ' '));
    else
        sta_tag->set_pro_tg(datPrd.raw());

    FixedRecord datPrdEse(orderProductionLayout(), readOrCreate(NAM_FIL_PRD_ESE, QByteArray(kOrderProductionSize, ' ')));
    if (vb::val(datPrdEse.text(QStringLiteral("tem_fer"))) > 600000000)
        sta_tag->set_pro_ese(QByteArray(kOrderProductionSize, ' '));
    else
        sta_tag->set_pro_ese(datPrdEse.raw());
}

void scriviPro()
{
    recordfile::write(resolveDataFile(NAM_FIL_PRD), {sta_tag->get_pro_tg()}, kShiftProductionSize);
    recordfile::write(resolveDataFile(NAM_FIL_PRD_ESE), {sta_tag->get_pro_ese()}, kOrderProductionSize);
}

QString calTemCo(qint32 metRim, int vel)
{
    if (vel == 0 || metRim <= 0)
        return metRim <= 0 ? QStringLiteral("00:00:00") : QStringLiteral("23:59:59");
    // InStr(CStr(x), ",") never matches: the original PCs used '.' as decimal separator.
    const double app = double(metRim) / vel;
    const double app1 = app / 60;
    QString ore, min, sec;
    if (vb::cLng(app1) == 0) {
        ore = QStringLiteral("00");
        if (vb::cLng(app) == 0) {
            min = QStringLiteral("00");
            sec = f00(app * 60);
        } else {
            min = f00(app);
            sec = QStringLiteral("00");
        }
    } else {
        ore = f00(app1);
        min = QStringLiteral("00");
        sec = QStringLiteral("00");
    }
    return ore + u':' + min + u':' + sec;
}

} // namespace stag
