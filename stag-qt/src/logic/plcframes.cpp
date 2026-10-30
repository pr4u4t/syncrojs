#include "plcframes.h"

#include "gesord.h"
#include "parcurtag.h"
#include "parmac.h"
#include "staglog.h"
#include "vbcompat.h"

#include <QStringList>

#include <array>

namespace stag {

namespace {

constexpr int NUM_DAT_MEM_PLC = 50;
const QString ZERO = QStringLiteral("0000");

// DM3500
constexpr int NEW_DAT = 1;
constexpr int INV_ORD_COR = 2;
constexpr int DAT_ORD = 8;
constexpr int DEL_ORD = 16;
constexpr int MOD_ORD = 32;
constexpr int INV_PAR_MAC = 64;
constexpr int AZZ_CONT = 512;
constexpr int INV_SET_DAT_SCH_TAG = 1024;
// DM3501
constexpr int INV_PAR_CFG_MAC = 1;
constexpr int INV_PAR_CUR_TAG_LIV_1 = 2;

constexpr int CUT_MAR = 1;
constexpr int CUT_PAT = 2;

using Words = std::array<QString, NUM_DAT_MEM_PLC + 1>;

Words blank()
{
    Words dm;
    dm.fill(ZERO);
    return dm;
}

PlcFrame finish(const Words &dm, int count)
{
    PlcFrame frame;
    frame.dm.reserve(count);
    for (int i = 0; i < count; ++i)
        frame.dm << dm[size_t(i)];
    return frame;
}

// int_esa of a VB expression: Val, then Hex$ rounds to a whole number.
QString esa(double value)
{
    return vb::intEsa(vb::cLng(value));
}

QString esa(const FixStr &value)
{
    return vb::intEsa(vb::cLng(value.val()));
}

// rie_dat_ord
void fillOrder(Words &dm, int indice, const ClasseOrdine &order, const ClasseOrdine *next)
{
    constexpr int p = 4;
    constexpr int CTR = 8;
    int dmTmp = 0;
    int ctrPre = 0;
    const bool empty = ordineVuoto(order);

    for (int i = 1; i <= 3; ++i) {
        const auto field = [&](const char *name) { return order.ord(i, QLatin1String(name)); };
        if (vb::trim(field("Ord_liv")).isEmpty())
            continue;
        const int liv = int(vb::val(field("Ord_liv")));
        const int block = p + (liv - 1) * 4;
        if (dm[size_t(block)] != ZERO)
            continue;

        const int livOrd = calLiv(liv);
        dmTmp = empty ? 0 : dmTmp + (1 << (liv - 1));

        const int cutToMark = int(vb::val(field("cut_to_mark")));
        const bool optMark = par_mac.opt.cut_mar == QLatin1String("1");
        const bool optPat = par_mac.opt.cut_pat(liv) == QLatin1String("1");
        const bool optAuto = par_mac.opt.cut_pat_auto(liv) == QLatin1String("1");
        if (optAuto) {
            if ((optMark && cutToMark == CUT_MAR) || (optPat && cutToMark == CUT_PAT) || cutToMark == CUT_PAT) {
                ctrPre = 1 << (2 + liv);
                dmTmp += ctrPre;
            }
        } else {
            if ((optPat && cutToMark == CUT_PAT) || (optMark && cutToMark == CUT_MAR)) {
                ctrPre = -1;
                dmTmp |= 1 << (liv + 3);
            }
            if (ctrPre != 0)
                dmTmp |= CTR;
        }

        const qint32 lung = vb::intDiv(vb::val(field("sca_lun")), TODECMM);
        dm[size_t(block)] = vb::intEsa(lung);
        QString cuts;
        if (empty) {
            dm[size_t(block)] = vb::intEsa(300);
            cuts = QStringLiteral("00000000");
        } else {
            const qint32 tagli = vb::intDiv(vb::val(field("sca_tot_num")), vb::val(field("Sca_per_tag")));
            cuts = QString::number(quint32(tagli), 16).toUpper().rightJustified(8, u'0').right(8);
        }
        dm[size_t(block + 1)] = cuts.right(4);
        dm[size_t(block + 2)] = cuts.left(4);

        int velCurTag = 0;
        if (empty) {
            for (int l = 1; l <= int(par_mac.car_tec.num_liv_tag.val()); ++l)
                velCurTag = qMax(int(par_mac.par_cfg_att(l).vel_max_tag.val()), velCurTag);
        } else {
            velCurTag = calVelCurTag(liv, lung);
            if (indice == 1)
                vel_max_tag_liv(livOrd) = qint16(qMin(int(par_mac.par_cfg_att(liv).vel_max_tag.val()), velCurTag));
        }
        dm[size_t(block + 3)] = vb::intEsa(velCurTag);

        int preAll = 0;
        if (vb::val(order.ord(1, QStringLiteral("sca_lun"))) != 0) {
            const bool nextCutPat = next && indice > 0 && int(vb::val(next->ord(i, QStringLiteral("cut_to_mark")))) == CUT_PAT;
            const QString ownPreAll = order.text(QStringLiteral("pre_all"));
            if ((nextCutPat || cutToMark == CUT_PAT) && optPat)
                preAll = int(par_mac.car_tec.pre_all.val());
            else
                preAll = vb::trim(ownPreAll).isEmpty() || vb::val(ownPreAll) == 0 ? int(par_mac.car_tec.pre_all.val()) : int(vb::val(ownPreAll));
            dm[size_t(16 + (liv - 1))] = esa(preAll / ((vb::val(field("sca_lun")) / TODECMM) / 1000));
        } else {
            dm[16] = ZERO;
        }
        log_str(QStringLiteral("rie_dat_ord: Preallarme per livello %1 impostato a %2 mt.").arg(livOrd).arg(preAll), MED_PRIORITY, CosLog::SchTag);

        const double altOrd = order.Alt_ord();
        if (cutToMark == CUT_PAT) {
            dm[size_t(19 + (liv - 1) * 2)] = esa(vb::val(field("Ofs_Cut")));
            if (par_mac.opt.cut_pat(liv).val() != 0) {
                const double quota = (altOrd / 2) / TODECMM - par_mac.sch_tag.sch_tag_liv(liv).dis_edg_ftc.val();
                dm[size_t(20 + (liv - 1) * 2)] = esa(quota >= 0 ? quota : 0);
                dm[size_t(25 + (liv - 1))] = ZERO;
            } else {
                dm[size_t(20 + (liv - 1) * 2)] = vb::val(field("pos_pattern")) != 0 ? esa(vb::val(field("pos_pattern"))) : ZERO;
                dm[size_t(25 + (liv - 1))] = esa(-(altOrd / 2) / TODECMM);
            }
        } else {
            dm[size_t(19 + (liv - 1) * 2)] = ZERO;
            dm[size_t(20 + (liv - 1) * 2)] = ZERO;
            dm[size_t(25 + (liv - 1))] = par_mac.opt.cut_pat_auto(liv).val() != 0 ? esa(-(altOrd / 2) / TODECMM) : ZERO;
        }
    }
    dm[3] = vb::intEsa(dmTmp);
}

} // namespace

QVector<qint32> PlcFrame::words() const
{
    QVector<qint32> values;
    values.reserve(dm.size());
    for (const QString &word : dm)
        values << word.toInt(nullptr, 16);
    return values;
}

QString PlcFrame::logLine() const
{
    return QStringLiteral("snd_str_PLC: (DM3500-DM35%1) = '%2'").arg(dm.size(), 2, 10, QLatin1Char('0')).arg(text());
}

std::optional<PlcFrame> frameOrder(OrderOperation operation, int indice, const ClasseOrdine &order, const ClasseOrdine *next)
{
    Words dm = blank();
    dm[0] = vb::intEsa(NEW_DAT + INV_ORD_COR * indice + (operation == OrderOperation::Send ? DAT_ORD : MOD_ORD));
    dm[2] = esa(vb::val(order.stag_id()));
    try {
        fillOrder(dm, indice, order, next);
    } catch (const vb::Error &) {
        return std::nullopt;
    }
    return finish(dm, 28);
}

PlcFrame frameDeleteOrder(int indice)
{
    Words dm = blank();
    dm[0] = vb::intEsa(NEW_DAT + INV_ORD_COR * indice + DEL_ORD);
    return finish(dm, 1);
}

// rie_dat_par_mac
PlcFrame frameParMac()
{
    Words dm = blank();
    dm[0] = vb::intEsa(NEW_DAT + INV_PAR_MAC);
    dm[2] = esa(par_mac.rul_ing.rul_num(1).dis_rot_mas);
    dm[3] = esa(par_mac.sch_tag.sch_tag_liv(1).dis_fot_lam);
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    for (int i = 1; i <= levels; ++i) {
        const int base = (i - 1) * 5;
        dm[size_t(5 + base)] = esa(par_mac.rul_usc.rul_usc_liv(i).lun_san);
        dm[size_t(6 + base)] = esa(par_mac.rul_usc.rul_usc_liv(i).dis_lam_fla);
        dm[size_t(7 + base)] = esa(par_mac.car_tec.lun_tag_sca);
        dm[size_t(8 + base)] = esa(par_mac.rul_usc.rul_usc_liv(i).tem_ape_fla);
        dm[size_t(9 + base)] = esa(par_mac.car_tec.lun_tag_sca_pat_aut);
    }
    dm[22] = esa(par_mac.car_tec.sgl_abi_lin_DC_Bus);
    return finish(dm, 23);
}

// rie_dat_par_cfg_mac
PlcFrame frameParCfgMac()
{
    constexpr int ABI_LIV = 2, SEL_USC_SND = 16, N_MOT_LIV = 256, TIP_AZI = 4096, NOR_MAC = 16384, MIS_ANG = 32768;
    constexpr int MOT_ABI = 2, GRU_SCA = 16, CUT_TO_MARK = 32, CUT_TO_PAT = 64, CUT_TO_PAT_AUTO = 128, CON_POM = 256;

    Words dm = blank();
    dm[0] = vb::intEsa(NEW_DAT);
    dm[1] = vb::intEsa(INV_PAR_CFG_MAC);

    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    const int motors = int(par_mac.car_tec.num_mot_liv_tag.val());
    qint32 v = 0;
    for (int i = 1; i <= 3; ++i) {
        if (par_abi.liv(i))
            v += ABI_LIV << (i - 1);
    }
    for (int i = 1; i <= levels; ++i) {
        if (par_mac.car_tec.sel_usc_liv(i) == QLatin1String("0")) {
            v += SEL_USC_SND;
            break;
        }
    }
    if (motors >= 2)
        v += N_MOT_LIV;
    if (motors >= 3)
        v += N_MOT_LIV * 2;
    if (par_mac.car_tec.nor_mac == UL)
        v |= NOR_MAC;
    if (abi_mis_ang)
        v += MIS_ANG;
    if (par_mac.car_tec.tip_azi_tag == QLatin1String("1"))
        v += TIP_AZI;
    dm[2] = vb::intEsa(v);

    for (int i = 1; i <= levels; ++i) {
        v = 0;
        for (int m = 1; m <= motors; ++m) {
            if (par_abi.fun_mot_liv(i).fun_mot_mot(m))
                v += MOT_ABI << (m - 1);
        }
        if (int(par_mac.opt.sca_liv(i).val()) == 1)
            v += GRU_SCA;
        if (int(par_mac.opt.cut_mar.val()) == 1)
            v += CUT_TO_MARK;
        if (int(par_mac.opt.cut_pat(i).val()) == 1)
            v += CUT_TO_PAT;
        if (int(par_mac.opt.cut_pat_auto(i).val()) == 1)
            v += CUT_TO_PAT_AUTO;
        if (int(par_mac.car_tec.pre_con_pom(i).val()) == 1)
            v += CON_POM;
        dm[size_t(3 + i - 1)] = vb::intEsa(v);
    }
    return finish(dm, 6);
}

// rie_dat_par_cur_tag
PlcFrame frameCurTag(int liv)
{
    constexpr int points = 15;
    Words dm = blank();
    dm[0] = vb::intEsa(NEW_DAT);
    dm[1] = vb::intEsa(INV_PAR_CUR_TAG_LIV_1 << (liv - 1));
    dm[2] = vb::intEsa(points - 1);
    dm[3] = vb::intEsa(cur_tag(liv).Lun_tag(points));
    dm[4] = vb::intEsa(cur_tag(liv).vel_max(1));
    for (int i = 1; i <= points; ++i) {
        dm[size_t(2 + 2 * i + 1)] = vb::intEsa(cur_tag(liv).Lun_tag(i));
        dm[size_t(2 + 2 * i + 2)] = vb::intEsa(cur_tag(liv).vel_max(i));
    }
    return finish(dm, 2 + 2 * points + 2 + 1);
}

PlcFrame frameResetCounters()
{
    Words dm = blank();
    dm[0] = vb::intEsa(NEW_DAT + AZZ_CONT);
    return finish(dm, 1);
}

PlcFrame frameConfirmCutBoards()
{
    Words dm = blank();
    dm[0] = vb::intEsa(NEW_DAT + INV_ORD_COR + INV_SET_DAT_SCH_TAG);
    return finish(dm, 1);
}

} // namespace stag
