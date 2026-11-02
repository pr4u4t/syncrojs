#include "schtagframes.h"

#include "parmac.h"
#include "vbcompat.h"

using namespace stag;

namespace {

constexpr int NUM_PAR_CFG = 79;
constexpr int NUM_PAR_DIV_AUT = 22;
constexpr int CUT_PAT = 2;
constexpr int WPROTO_LEGGIVETTORE = 3;
constexpr int WPROTO_SCRIVIVETTORE = 4;

QString esa(double value)
{
    return vb::intEsa(vb::cLng(value));
}

QString esa(const FixStr &value)
{
    return esa(value.val());
}

QString esa(const QString &value)
{
    return esa(vb::val(value));
}

QString finishFrame(QString frame)
{
    frame += boardChecksum(frame);
    frame += QLatin1Char(EOT_ST);
    return frame;
}

struct BoardRecord {
    std::array<QString, 10> quote;
    QString bits = esa(0.0);
    QString offset = esa(0.0);
    QString id = QStringLiteral("FFFF");
    QString free = esa(0.0);
    QString before = esa(0.0);
    QString after = esa(0.0);

    BoardRecord()
    {
        quote.fill(esa(0.0));
    }

    QString text() const
    {
        QString out;
        for (const QString &q : quote)
            out += q;
        return out + bits + offset + id.leftJustified(4, u' ', true) + free + before + after;
    }
};

// dummy_order_sch_tag of Principale.bas
BoardRecord dummyRecord()
{
    BoardRecord record;
    record.quote[0] = esa(30.0);
    record.quote[1] = esa(35.0);
    record.quote[2] = esa(40.0);
    record.bits = esa(6.0);
    return record;
}

} // namespace

QString stag::boardChecksum(const QString &withStx)
{
    if (withStx.isEmpty())
        return QStringLiteral("  ");
    qint64 sum = 0;
    for (int i = 1; i < withStx.size(); ++i)
        sum += withStx.at(i).unicode();
    return QString::number(sum, 16).toUpper().rightJustified(2, u'0').right(2);
}

QString stag::boardReadVector(int address, int words)
{
    return finishFrame(QLatin1Char(STX_ST) + QString::number(WPROTO_LEGGIVETTORE) + vb::intEsa(words) + vb::intEsa(address));
}

QString stag::boardWriteVector(int address, const QString &payload)
{
    return finishFrame(QLatin1Char(STX_ST) + QString::number(WPROTO_SCRIVIVETTORE) + vb::intEsa(address) + payload);
}

QString stag::boardOrderDummy(const QString &id)
{
    BoardRecord record = dummyRecord();
    record.id = id;
    return record.text();
}

QString stag::boardOrder(const ClasseOrdine &order, int i)
{
    constexpr int BIT_FST_FTC = 1;
    constexpr int BIT_ABI_PAT = 2;
    constexpr int BIT_BKG_A = 4;
    constexpr int BIT_BKG_B = 8;
    const auto field = [&](const char *name) { return order.ord(i, QLatin1String(name)); };

    BoardRecord record;
    for (int q = 0; q < 10; ++q)
        record.quote[size_t(q)] = esa(field(QStringLiteral("L%1").arg(q + 1).toLatin1().constData()));
    int bits = int(vb::val(field("fst_FTC"))) == 1 ? BIT_FST_FTC : 0;
    bits += int(vb::val(field("cut_to_mark"))) == CUT_PAT ? BIT_ABI_PAT : 0;
    const int contrast = int(vb::val(field("pat_con")));
    if (contrast > 0) {
        bits += ((contrast - 1) & 1) == 1 ? BIT_BKG_A : 0;
        bits += ((contrast - 1) & 2) == 2 ? BIT_BKG_B : 0;
    } else {
        bits += BIT_BKG_A;
    }
    record.bits = vb::intEsa(bits);
    record.offset = esa(field("Ofs_Cut"));
    record.id = order.stag_id();
    record.before = esa(field("fin_pri"));
    record.after = esa(field("fin_dop"));
    return record.text();
}

QString stag::boardContrast(int contrast)
{
    constexpr int BIT_BKG_A = 4;
    constexpr int BIT_BKG_B = 8;
    BoardRecord record = dummyRecord();
    int bits = ((contrast - 1) & 1) == 1 ? BIT_BKG_A : 0;
    bits += ((contrast - 1) & 2) == 2 ? BIT_BKG_B : 0;
    record.bits = vb::intEsa(bits);
    return record.text();
}

std::array<QString, 2> stag::boardConfig(int liv, const ClasseOrdine *current)
{
    std::array<QString, 103> par;
    par.fill(QStringLiteral("0000"));
    const TipSchTagLiv &tag = par_mac_ute.sch_tag.sch_tag_liv(liv);
    const TipParCfgSchTagLiv &cfg = par_mac_ute.par_cfg_att(liv);

    par[0] = esa(tag.vel_sim_lap);
    par[1] = esa(tag.vel_lam_jog);
    par[16] = esa(tag.dis_fot_lam);
    par[17] = esa(tag.lun_tag_tes);
    par[18] = esa(tag.lun_tag_min);
    par[19] = esa(tag.lun_tag_max);
    par[20] = esa(tag.lim_inf_tol_tag);
    par[21] = esa(tag.lim_sup_tol_tag);
    par[22] = esa(tag.gir_mot_lam_rif);
    par[23] = esa(cfg.vel_max_tag);
    par[24] = esa(tag.ang_sincr);
    par[25] = esa(tag.sov_lam);
    par[26] = esa(tag.vel_zon_sinc_vmin);
    par[27] = esa(tag.dia_ruo_mis);
    par[28] = esa(tag.dia_pull_roll);
    par[29] = esa(tag.cir_lam);
    par[30] = esa(tag.rap_rid_mot_lam);
    par[31] = esa(tag.imp_gir_enc_ruo_mis);
    par[32] = esa(tag.imp_gir_enc_pull_roll);
    par[33] = esa(tag.imp_gir_enc_lam);
    par[34] = esa(tag.fat_mol_enc_ruo_mis);
    par[35] = esa(tag.fat_mol_enc_pull_roll);
    par[36] = esa(tag.fat_mol_enc_lam);
    par[37] = esa(cfg.tem_acc_tgl_lun);
    par[38] = esa(cfg.tem_dec_tgl_lun);
    par[39] = esa(cfg.tem_acc_tgl_cor);
    par[40] = esa(cfg.tem_dec_tgl_cor);
    par[41] = esa(tag.off_ram_pos);
    par[42] = esa(tag.off_ram_tag);
    par[43] = esa(tag.fin_vel_ott_pen);
    par[44] = esa(tag.fat_k01_ott_pen);
    par[45] = esa(cfg.gir_max_mot_lam);
    par[46] = esa(cfg.Kp_nor);
    par[47] = esa(cfg.Ki_nor);
    par[48] = esa(tag.tem_cam_sta_vel);
    par[49] = esa(tag.sog_err_ins);
    par[50] = esa(cfg.fil_kp);
    par[51] = esa(cfg.Kv_nor);
    par[52] = esa(cfg.lim_lav_Kv);

    const auto isOn = [](const FixStr &option) { return int(option.val()) == 1; };
    bool cutOption = isOn(par_mac.opt.cut_mar);
    for (int i = 1; i <= 3; ++i)
        cutOption = cutOption || isOn(par_mac.opt.cut_pat(i)) || isOn(par_mac.opt.cut_pat_auto(i));
    if (cutOption) {
        // Icilio3545: with a pattern order in work its data goes, on the assumption of one pattern per order.
        int pattern = 0;
        for (int i = 1; current && i <= 3 && !pattern; ++i) {
            if (int(vb::val(current->ord(i, QStringLiteral("cut_to_mark")))) == CUT_PAT)
                pattern = i;
        }
        par[64] = pattern ? esa(current->ord(pattern, QStringLiteral("Ofs_Cut"))) : esa(tag.off_tag_reg);
        par[65] = pattern ? esa(current->ord(pattern, QStringLiteral("fin_pri"))) : esa(tag.fin_pri_tac);
        par[66] = pattern ? esa(current->ord(pattern, QStringLiteral("fin_dop"))) : esa(tag.fin_dop_tac);
        par[72] = esa(tag.dis_FTC_tac_lam);
        par[73] = esa(tag.cor_tag_reg_1o_sog);
        par[74] = esa(tag.sog_vel_cor);
        par[75] = esa(tag.cor_tag_reg_2o_sog);
        par[78] = esa(tag.vel_ftc);
        par[79] = esa(tag.dis_cen_mac_sen_fuo_car);
    }

    if (isOn(par_mac_ute.opt.sca_liv(liv))) {
        par[80] = esa(tag.sov_tap_usc_per_sca);
        par[81] = esa(par_mac_ute.car_tec.lun_tag_sca);
        par[82] = esa(tag.sca_pri_giu);
        par[83] = esa(tag.sca_dop_giu);
        par[84] = esa(tag.dis_sen_lam);
        par[85] = esa(tag.dis_lam_flp);
        par[86] = esa(tag.dis_lam_rul_no_crh);
        par[87] = esa(tag.tem_ape_flp);
        par[88] = esa(tag.tem_chi_flp);
        par[89] = esa(tag.tem_ape_rul_no_crh);
        par[90] = esa(tag.tem_chi_rul_no_crh);
        par[91] = esa(tag.tem_sca_pre_ape_rul_no_crh);
        par[92] = esa(tag.tem_sca_pre_chi_rul_no_crh);
        par[93] = esa(tag.max_lun_sca);
        par[94] = esa(tag.min_lun_sca);
        par[95] = esa(tag.sog_vel_1);
        par[96] = esa(tag.sog_lun_1);
        par[97] = esa(tag.sog_vel_2);
        par[98] = esa(tag.sog_lun_2);
        par[99] = esa(tag.sog_vel_3);
        par[100] = esa(tag.sog_lun_3);
        par[101] = esa(tag.sog_lun_sup_3);
        par[102] = esa(tag.tem_att_cam_pre_rul_no_crh);
    }

    std::array<QString, 2> parts;
    for (int i = 0; i <= NUM_PAR_CFG; ++i)
        parts[0] += par[size_t(i)];
    for (int i = NUM_PAR_CFG + 1; i <= NUM_PAR_CFG + NUM_PAR_DIV_AUT + 1; ++i)
        parts[1] += par[size_t(i)];
    return parts;
}
