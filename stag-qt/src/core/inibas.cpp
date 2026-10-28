#include "inibas.h"

#include "inifile.h"
#include "parmac.h"
#include "staglog.h"

#include <initializer_list>

namespace stag {
namespace {

const QString sec_liv = QStringLiteral("sch_tag_liv_");

struct Read {
    FixStr TipSchTagLiv::*field;
    const char *key;
    const char *def;
};

QString s(const char *utf8)
{
    return QString::fromUtf8(utf8);
}

QString num(int i)
{
    return QString::number(i);
}

double valIni(IniFile &ini, const QString &section, const QString &key, const QString &def)
{
    return vb::val(ini.getFileINIString(section, key, def));
}

void readAll(IniFile &ini, const QString &section, TipSchTagLiv &liv, std::initializer_list<Read> reads)
{
    for (const Read &r : reads)
        liv.*r.field = valIni(ini, section, s(r.key), s(r.def));
}

void clearAll(TipSchTagLiv &liv, std::initializer_list<FixStr TipSchTagLiv::*> fields)
{
    for (FixStr TipSchTagLiv::*f : fields)
        liv.*f = "";
}

bool cutRegistro(int i)
{
    return par_mac.opt.cut_mar.val() == 1 || par_mac.opt.cut_pat(i).val() == 1 || par_mac.opt.cut_pat_auto(i).val() == 1;
}

} // namespace

void leggiNumLivTag(IniFile &ini)
{
    par_mac.car_tec.num_liv_tag = valIni(ini, s("car_tec"), s("num_liv_tag"), s("2"));
    if (par_mac.car_tec.num_liv_tag.val() > 3 || par_mac.car_tec.num_liv_tag.val() < 0)
        par_mac.car_tec.num_liv_tag = "2";
}

void legParSup(IniFile &ini, const QString &command, const QString &proPatOverride)
{
    if (vb::trim(command).toUpper() == QStringLiteral("UTE")) {
        pro_set.ute_wrk = true;
        pro_set.ute_wrk_liv1 = true;
    }
    pro_set.lin_att = vb::cInt(valIni(ini, s("pro_set"), s("lin_att"), s("0")));
    pro_set.per_cli = ini.getFileINIString(s("pro_set"), s("per_cli"), QString());
    pro_set.pro_pat = ini.getFileINIString(s("pro_set"), s("pro_pat"), s("C:\\usr\\stag\\"));
    if (!proPatOverride.isEmpty())
        pro_set.pro_pat = proPatOverride;
    pro_set.nam_fil_log_zip = ini.getFileINIString(s("pro_set"), s("nam_fil_log_zip"), s("Stag_ZipListFile_LOG.txt"));
    pro_set.nam_fil_cod_zip = ini.getFileINIString(s("pro_set"), s("nam_fil_cod_zip"), s("Stag_ZipListFile_COD.txt"));
    pro_set.nam_fil_dat_zip = ini.getFileINIString(s("pro_set"), s("nam_fil_dat_zip"), s("Stag_ZipListFile_DAT.txt"));
    pro_set.pro_bck_loc_zip = ini.getFileINIString(s("pro_set"), s("pro_bck_loc_zip"), s("C:\\BACKUP"));
    pro_set.pro_bck_rem_zip = ini.getFileINIString(s("pro_set"), s("pro_bck_rem_zip"), s("\\\\MDW\\BACKUP"));
    pro_set.Nam_fil_uni = ini.getFileINIString(s("pro_set"), s("Nam_fil_uni"), s("Ordini"));
    pro_set.Nam_fil_dat_ord = ini.getFileINIString(s("pro_set"), s("Nam_fil_dat_ord"), s("Ord_mac.dat"));
    pro_set.Nam_fil_dat_ord_ese = ini.getFileINIString(s("pro_set"), s("Nam_fil_dat_ord_ese"), s("Ord_mac_ese.dat"));
    pro_set.abi_reg_rep_pro = ini.getFileINIString(s("pro_set"), s("abi_reg_rep_pro"), s(STR_FALSE));
    pro_set.ini_prod = ini.getFileINIString(s("pro_set"), s("ini_prod"), s(STR_FALSE));
    pro_set.pwd = ini.getFileINIString(s("pro_set"), s("pwd"), QString());
    pro_set.pwd_liv1 = ini.getFileINIString(s("pro_set"), s("pwd_liv1"), QString());
    pro_set.ric_msg_db = valIni(ini, s("pro_set"), s("ric_msg_db"), s("0")) != 0;
    abi_mis_ang = valIni(ini, s("pro_set"), s("Inch"), s("0")) != 0;
    StagLog::instance().configure(ini, pro_set.pro_pat);
    pro_set.tur = vb::cInt(valIni(ini, s("pro_set"), s("tur"), s("1")));
    if (pro_set.tur > 3 || pro_set.tur < 1)
        pro_set.tur = 1;
}

void legParMac(IniFile &ini)
{
    TipCarTec &ct = par_mac.car_tec;
    const QString car_tec = s("car_tec");

    ct.num_mot_liv_tag = valIni(ini, car_tec, s("num_mot_liv_tag"), s("2"));
    // VB: val(num_mot_liv_tag > 3) is Val("True"/"False") = 0, so only the "< 0" test is effective.
    if (ct.num_mot_liv_tag.val() < 0)
        ct.num_mot_liv_tag = "2";
    ct.for_mac = vb::cLng(valIni(ini, car_tec, s("for_mac"), s("2")));
    ct.gra_car = vb::cLng(valIni(ini, car_tec, s("gra_car"), num(gm2_1800)));
    ct.nor_mac = vb::cLng(valIni(ini, car_tec, s("nor_mac"), num(CE)));
    if (ct.for_mac < 0 || ct.for_mac > 2)
        ct.for_mac = 1;
    if (ct.gra_car < gm2_1800 || ct.gra_car > gm2_2600)
        ct.gra_car = gm2_1800;
    if (ct.nor_mac < CE || ct.nor_mac > UL)
        ct.nor_mac = CE;
    ct.min_for_mac = valIni(ini, car_tec, s("min_for_mac"), s("200"));
    if (ct.min_for_mac.val() < 0)
        ct.min_for_mac = "200";
    ct.min_lun_tag = valIni(ini, car_tec, s("min_lun_tag"), s("300"));
    if (ct.min_lun_tag.val() < 0)
        ct.min_lun_tag = "300";
    ct.max_lun_tag = valIni(ini, car_tec, s("max_lun_tag"), s("6000"));
    if (ct.max_lun_tag.val() < 0)
        ct.max_lun_tag = "6000";
    ct.lun_tag_sca = valIni(ini, car_tec, s("lun_tag_sca"), s("300"));
    if (ct.lun_tag_sca.val() < 0)
        ct.lun_tag_sca = "300";
    ct.lun_tag_sca_pat_aut = valIni(ini, car_tec, s("lun_tag_sca_cut_pat_auto"), s("300"));
    if (ct.lun_tag_sca_pat_aut.val() < 0)
        ct.lun_tag_sca_pat_aut = "300";
    ct.sgl_abi_lin_DC_Bus = valIni(ini, car_tec, s("sgl_abi_lin_DC_Bus"), s(VAL_DEF_SGL_ABI_LIN_DC_BUS));
    if (ct.sgl_abi_lin_DC_Bus.val() < LIM_INF_SGL_ABI_LIN_DC_BUS || ct.sgl_abi_lin_DC_Bus.val() > LIM_SUP_SGL_ABI_LIN_DC_BUS) {
        // The original resets lun_tag_sca here, not sgl_abi_lin_DC_Bus.
        ct.lun_tag_sca = VAL_DEF_SGL_ABI_LIN_DC_BUS;
        ct.lun_tag_sca_pat_aut = VAL_DEF_SGL_ABI_LIN_DC_BUS;
    }
    const int numLiv = int(ct.num_liv_tag.val());
    for (int i = 1; i <= numLiv; ++i) {
        ct.sel_usc_liv(i) = valIni(ini, car_tec, s("sel_usc_liv_") + num(i), s("0"));
        ct.pre_con_pom(i) = valIni(ini, car_tec, s("pre_con_pom_") + num(i), s("0"));
    }
    ct.pre_all = valIni(ini, car_tec, s("pre_all"), s("50"));
    ct.tip_azi_tag = valIni(ini, car_tec, s("tip_azi_tag"), s("0"));

    par_mac.opt.cut_mar = ini.getFileINIString(s("opt"), s("cut_mar"), s("0"));
    const QString cutPatDef = ini.getFileINIString(s("opt"), s("cut_pat"), s("0"));
    const QString cutPatAuto = ini.getFileINIString(s("opt"), s("cut_pat_auto"), s("0"));
    for (int i = 1; i <= numLiv; ++i) {
        par_mac.opt.cut_pat(i) = vb::cStr(valIni(ini, s("opt"), s("cut_pat_") + num(i), cutPatDef));
        par_mac.opt.sca_liv(i) = vb::cStr(valIni(ini, s("opt"), s("sca_liv_") + num(i), s("0")));
        par_mac.opt.cut_pat_auto(i) = ini.getFileINIString(s("opt"), s("cut_pat_auto_") + num(i), cutPatAuto);
    }

    par_abi.abi_com_PU = valIni(ini, s("abilitazioni"), s("abi_com_pu"), s("1")) != 0;
    for (int i = 1; i <= numLiv; ++i) {
        const QString section = s("abi_liv_") + num(i);
        par_abi.liv(i) = valIni(ini, section, s("liv"), s("1")) != 0;
        par_abi.num_mot_abi(i) = 0;
        for (int m = 1; m <= int(ct.num_mot_liv_tag.val()); ++m) {
            par_abi.fun_mot_liv(i).fun_mot_mot(m) = valIni(ini, section, s("fun_mot") + num(m), s("1")) != 0;
            if (par_abi.fun_mot_liv(i).fun_mot_mot(m))
                par_abi.num_mot_abi(i) = qint16(par_abi.num_mot_abi(i) + 1);
        }
    }
    par_abi.abi_rep_pro = valIni(ini, s("abilitazioni"), s("abi_rep_pro"), s("0")) != 0;
    par_abi.abi_err_com_PU = valIni(ini, s("abilitazioni"), s("abi_err_com_pu"), s("1")) != 0;

    using L = TipSchTagLiv;
    for (int i = 1; i <= numLiv; ++i) {
        const QString section = sec_liv + num(i);
        TipSchTagLiv &att = par_mac.sch_tag.sch_tag_liv(i);
        TipSchTagLiv &def = par_mac.sch_tag_def.sch_tag_liv(i);

        par_mac.config_sch_tag_att(i).descr = ini.getFileINIString(section, s("config_att"), QString());
        readAll(ini, section, att, {
            {&L::dia_ruo_mis, "dia_ruo_mis", "15933"},
            {&L::imp_gir_enc_ruo_mis, "imp_gir_enc_ruo_mis", "2048"},
            {&L::vel_lam_jog, "vel_lam_jog", "2"},
            {&L::ang_sincr, "ang_sincr", "50"},
            {&L::cir_lam, "cir_lam", "816"},
            {&L::dia_pull_roll, "dia_pull_roll", "15323"},
            {&L::dis_fot_lam, "dis_fot_lam", "12000"},
            {&L::fat_k01_ott_pen, "fat_k01_ott_pen", "30"},
            {&L::fat_mol_enc_lam, "fat_mol_enc_lam", "4"},
            {&L::fat_mol_enc_pull_roll, "fat_mol_enc_pull_roll", "4"},
            {&L::fat_mol_enc_ruo_mis, "fat_mol_enc_ruo_mis", "4"},
            {&L::fin_vel_ott_pen, "fin_vel_ott_pen", "80"},
            {&L::imp_gir_enc_lam, "imp_gir_enc_lam", "2048"},
            {&L::imp_gir_enc_pull_roll, "imp_gir_enc_pull_roll", "2048"},
            {&L::lun_tag_tes, "lun_tag_tes", "200"},
            {&L::off_ram_pos, "off_ram_pos", "116"},
            {&L::off_ram_tag, "off_ram_tag", "54"},
            {&L::rap_rid_mot_lam, "rap_rid_mot_lam", "27368"},
            {&L::sov_lam, "sov_lam", "20"},
            {&L::vel_zon_sinc_vmin, "vel_zon_sinc_vmin", "15"},
            {&L::lim_inf_tol_tag, "lim_inf_tol_tag", "10"},
            {&L::lim_sup_tol_tag, "lim_sup_tol_tag", "10"},
            {&L::vel_sim_lap, "vel_sim_lap", "30"},
            {&L::lun_tag_min, "lun_tag_min", "300"},
            {&L::lun_tag_max, "lun_tag_max", "6000"},
            {&L::gir_mot_lam_rif, "gir_mot_lam_rif", "1250"},
            {&L::tem_cam_sta_vel, "tem_cam_sta_vel", "1"},
            {&L::sog_err_ins, "sog_err_ins", "2500"},
        });
        if (cutRegistro(i)) {
            readAll(ini, section, att, {
                {&L::off_tag_reg, "off_tag_reg", "0"},
                {&L::fin_pri_tac, "fin_pri_tac", "0"},
                {&L::fin_dop_tac, "fin_dop_tac", "0"},
                {&L::cor_tag_reg_1o_sog, "cor_tag_reg_1\u00B0_sog", "0"},
                {&L::cor_tag_reg_2o_sog, "cor_tag_reg_2\u00B0_sog", "6"},
                {&L::dis_FTC_tac_lam, "dis_FTC_tac_lam", "1588"},
                {&L::sog_vel_cor, "sog_vel_cor", "180"},
                {&L::vel_ftc, "vel_ftc", "2530"},
                {&L::dis_cen_mac_sen_fuo_car, "dis_cen_mac_sen_fuo_car", "1370"},
                {&L::dis_edg_ftc, "dis_edg_ftc", "35"},
            });
        } else {
            clearAll(att, {&L::off_tag_reg, &L::fin_pri_tac, &L::fin_dop_tac, &L::cor_tag_reg_1o_sog, &L::cor_tag_reg_2o_sog,
                           &L::dis_FTC_tac_lam, &L::sog_vel_cor, &L::vel_ftc, &L::dis_cen_mac_sen_fuo_car, &L::dis_edg_ftc});
        }
        const std::initializer_list<Read> scarto = {
            {&L::sov_tap_usc_per_sca, "sov_tap_usc_per_sca", "100"},
            {&L::sca_pri_giu, "sca_pri_giu", "250"},
            {&L::sca_dop_giu, "sca_dop_giu", "250"},
            {&L::dis_sen_lam, "dis_sen_lam", "15000"},
            {&L::dis_lam_flp, "dis_lam_flp", "915"},
            {&L::dis_lam_rul_no_crh, "dis_lam_rul_no_crh", "800"},
            {&L::tem_ape_flp, "tem_ape_flp", "56"},
            {&L::tem_chi_flp, "tem_chi_flp", "50"},
            {&L::tem_ape_rul_no_crh, "tem_ape_rul_no_crh", "100"},
            {&L::tem_chi_rul_no_crh, "tem_chi_rul_no_crh", "100"},
            {&L::tem_sca_pre_ape_rul_no_crh, "tem_sca_pre_ape_rul_no_crh", "20"},
            {&L::tem_sca_pre_chi_rul_no_crh, "tem_sca_pre_chi_rul_no_crh", "20"},
            {&L::max_lun_sca, "max_lun_sca", "1400"},
            {&L::min_lun_sca, "min_lun_sca", "600"},
            {&L::sog_vel_1, "ind_vel_1", "100"},
            {&L::sog_lun_1, "ind_lun_1", "600"},
            {&L::sog_vel_2, "ind_vel_2", "200"},
            {&L::sog_lun_2, "ind_lun_2", "700"},
            {&L::sog_vel_3, "ind_vel_3", "300"},
            {&L::sog_lun_3, "ind_lun_3", "800"},
            {&L::sog_lun_sup_3, "ind_lun_sup_3", "800"},
            {&L::tem_att_cam_pre_rul_no_crh, "tem_att_cam_pre_rul_no_crh", "300"},
        };
        readAll(ini, section, att, scarto);

        par_mac.config_sch_tag_def(i).descr = ini.getFileINIString(section, s("config_def"), QString());
        readAll(ini, section, def, {
            {&L::dia_ruo_mis, "dia_ruo_mis_def", "15933"},
            {&L::imp_gir_enc_ruo_mis, "imp_gir_enc_ruo_mis_def", "2048"},
            {&L::vel_lam_jog, "vel_lam_jog_def", "2"},
            {&L::ang_sincr, "ang_sincr_def", "50"},
            {&L::cir_lam, "cir_lam_def", "816"},
            {&L::dia_pull_roll, "dia_pull_roll_def", "15323"},
            {&L::dis_fot_lam, "dis_fot_lam_def", "12000"},
            {&L::fat_k01_ott_pen, "fat_k01_ott_pen_def", "30"},
            {&L::fat_mol_enc_lam, "fat_mol_enc_lam_def", "4"},
            {&L::fat_mol_enc_pull_roll, "fat_mol_enc_pull_roll_def", "4"},
            {&L::fat_mol_enc_ruo_mis, "fat_mol_enc_ruo_mis_def", "4"},
            {&L::fin_vel_ott_pen, "fin_vel_ott_pen_def", "80"},
            {&L::imp_gir_enc_lam, "imp_gir_enc_lam_def", "2048"},
            {&L::imp_gir_enc_pull_roll, "imp_gir_enc_pull_roll_def", "2048"},
            {&L::lun_tag_tes, "lun_tag_tes_def", "20"},
            {&L::off_ram_pos, "off_ram_pos_def", "116"},
            {&L::off_ram_tag, "off_ram_tag_def", "54"},
            {&L::rap_rid_mot_lam, "rap_rid_mot_lam_def", "27368"},
            {&L::sov_lam, "sov_lam_def", "20"},
            {&L::vel_zon_sinc_vmin, "vel_zon_sinc_vmin_def", "15"},
            {&L::lim_inf_tol_tag, "lim_inf_tol_tag_def", "10"},
            {&L::lim_sup_tol_tag, "lim_sup_tol_tag_def", "10"},
            {&L::vel_sim_lap, "vel_sim_lap_def", "30"},
            {&L::lun_tag_min, "lun_tag_min_def", "300"},
            {&L::lun_tag_max, "lun_tag_max_def", "6000"},
            {&L::gir_mot_lam_rif, "gir_mot_lam_rif_def", "1250"},
            {&L::tem_cam_sta_vel, "tem_cam_sta_vel_def", "1"},
            {&L::sog_err_ins, "sog_err_ins_def", "2500"},
        });
        if (cutRegistro(i)) {
            readAll(ini, section, def, {
                {&L::cor_tag_reg_1o_sog, "cor_tag_reg_1\u00B0_sog_def", "0"},
                {&L::cor_tag_reg_2o_sog, "cor_tag_reg_2\u00B0_sog_def", "6"},
                {&L::off_tag_reg, "off_tag_reg_def", "0"},
                {&L::fin_pri_tac, "fin_pri_tac_def", "0"},
                {&L::fin_dop_tac, "fin_dop_tac_def", "0"},
                {&L::dis_FTC_tac_lam, "dis_FTC_tac_lam_def", "1588"},
                {&L::sog_vel_cor, "sog_vel_cor_def", "180"},
                {&L::vel_ftc, "vel_ftc_def", "2530"},
                {&L::dis_cen_mac_sen_fuo_car, "dis_cen_mac_sen_fuo_car_def", "1370"},
                {&L::dis_edg_ftc, "dis_edg_ftc_def", "35"},
            });
        } else {
            clearAll(def, {&L::cor_tag_reg_1o_sog, &L::cor_tag_reg_2o_sog, &L::off_tag_reg, &L::fin_pri_tac, &L::fin_dop_tac,
                           &L::dis_FTC_tac_lam, &L::sog_vel_cor, &L::vel_ftc, &L::dis_cen_mac_sen_fuo_car, &L::dis_edg_ftc});
        }
        // The defaults of this group are read from the same keys as the active values.
        readAll(ini, section, def, scarto);

        const QString rulUsc = s("rul_usc_liv_") + num(i);
        TipRulUscLiv &ru = par_mac.rul_usc.rul_usc_liv(i);
        ru.dis_lam_fla = valIni(ini, rulUsc, s("dis_lam_fla"), QString());
        ru.gap_tgl_cor = valIni(ini, rulUsc, s("gap_tgl_cor"), QString());
        ru.gap_tgl_lun = valIni(ini, rulUsc, s("gap_tgl_lun"), QString());
        ru.lun_san = valIni(ini, rulUsc, s("lun_san"), QString());
        ru.tem_ape_fla = valIni(ini, rulUsc, s("tem_ape_fla"), s("0"));
        ru.tem_chi_fla = valIni(ini, rulUsc, s("tem_chi_fla"), s("0"));

        const QString com = s("com_sch_tag_") + num(i);
        TipPor &por = par_mac.com_sch_tag(i);
        qint32 tmp1 = vb::cLng(valIni(ini, com, s("baud"), s("38400")));
        por.baud = tmp1 == 0 ? s("38400") : vb::cStr(tmp1);
        QString tmp = vb::trim(ini.getFileINIString(com, s("bit_par"), s("e")));
        por.bit_par = tmp.isEmpty() ? s("e") : tmp;
        tmp1 = vb::cLng(valIni(ini, com, s("dati"), s("8")));
        por.dati = tmp1 == 0 ? s("8") : vb::cStr(tmp1);
        tmp1 = vb::cLng(valIni(ini, com, s("porta"), num(i + 2)));
        por.porta = tmp1 == 0 ? num(i + 2) : vb::cStr(tmp1);
        tmp1 = vb::cLng(valIni(ini, com, s("bit_stop"), s("1")));
        por.bit_stop = tmp1 == 0 ? s("1") : vb::cStr(tmp1);
        tmp1 = vb::cLng(valIni(ini, com, s("timeout"), s("2000")));
        por.TimeOut = tmp1 == 0 ? s("2000") : vb::cStr(tmp1);
        par_mac.tip_com_sch_tag(i) = valIni(ini, com, s("tip_com"), s("1"));
        par_mac.lan_sch_tag(i).por_loc = valIni(ini, com, s("por_loc"), s("4001"));
        par_mac.lan_sch_tag(i).por_rem = valIni(ini, com, s("por_rem"), s("4000"));
        par_mac.lan_sch_tag(i).ind_rem = ini.getFileINIString(com, s("ind_rem"), s("127.0.0.1"));
        tmp1 = vb::cLng(valIni(ini, com, s("tip_com_udp"), s("1")));
        // IIf(Trim$(tmp1) = True, 1, 0): the text is compared as a Boolean.
        par_mac.lan_sch_tag_UDP(i) = vb::cBool(vb::cStr(tmp1));
    }

    TipRulIng &ri = par_mac.rul_ing;
    ri.vel_max_lin = valIni(ini, s("rul_ing"), s("vel_max_lin"), s("350"));
    for (int r = 1; r <= 2; ++r) {
        TipRulNum &rn = ri.rul_num(r);
        if (r == 1 || ct.num_liv_tag.val() == 3) {
            const QString section = s("rul_ing_") + num(r);
            rn.dis_rot_mas = valIni(ini, section, s("dis_rot_mas"), QString());
            rn.off_vel_met_min = valIni(ini, section, s("off_vel_met_min"), QString());
            rn.off_vel_per = valIni(ini, section, s("off_vel_per"), QString());
            rn.sov_sfi = valIni(ini, section, s("sov_sfi"), QString());
        } else {
            rn.dis_rot_mas = "";
            rn.off_vel_met_min = "";
            rn.off_vel_per = "";
            rn.sov_sfi = "";
        }
    }

    TipComSpde &spde = par_mac.com_spde;
    const QString com_spde = s("com_spde");
    spde.ver_spde = vb::cLng(valIni(ini, com_spde, s("ver_spde"), s("2")));
    spde.tip_com = valIni(ini, com_spde, s("tip_com"), s("1"));
    spde.tip_porta.porta = valIni(ini, com_spde, s("porta"), s("6"));
    spde.tip_porta.baud = valIni(ini, com_spde, s("baud"), s("19200"));
    QString tmp = vb::trim(ini.getFileINIString(com_spde, s("bit_par"), s("n")));
    spde.tip_porta.bit_par = tmp.isEmpty() ? s("n") : tmp;
    spde.tip_porta.dati = valIni(ini, com_spde, s("dati"), s("8"));
    spde.tip_porta.bit_stop = valIni(ini, com_spde, s("bit_stop"), s("1"));
    spde.lan.por_loc = valIni(ini, com_spde, s("por_loc"), s("100"));
    spde.lan.por_rem = valIni(ini, com_spde, s("por_rem"), s("200"));
    tmp = vb::trim(ini.getFileINIString(com_spde, s("ind_rem"), s("200.0.0.200")));
    spde.lan.ind_rem = tmp.isEmpty() ? s("200.0.0.200") : tmp;
    spde.err_max = valIni(ini, com_spde, s("err_max"), s("3"));
    spde.TimeOut = valIni(ini, com_spde, s("timeout"), s("2000"));

    TipIndFins &plc = par_mac.indirizzo_plc;
    plc.net_id = vb::cInt(valIni(ini, s("com_plc"), s("net_id"), s("2")));
    plc.node_id = vb::cInt(valIni(ini, s("com_plc"), s("node_id"), s("230")));
    plc.unit_id = vb::cInt(valIni(ini, s("com_plc"), s("unit_id"), s("0")));
    plc.TIMER_SCANS_PLC = vb::cInt(valIni(ini, s("com_plc"), s("timer_scans_plc"), s("30")));

    par_mac.com_azi.TimeOut = valIni(ini, s("com_azi"), s("timeout"), s("2000"));
    par_mac.com_azi.baud = valIni(ini, s("com_azi"), s("baud"), s("1000"));

    par_mac.com_plc_lan.ind_rem = vb::trim(ini.getFileINIString(s("com_plc_lan"), s("ind_rem"), s("127.0.0.1")));
    par_mac.com_plc_lan.por_rem = vb::trim(ini.getFileINIString(s("com_plc_lan"), s("por_rem"), s("50000")));
    par_mac.com_plc_lan.TimeOut = valIni(ini, s("com_plc_lan"), s("timeout"), s("1000"));
    iniValNonUti();
}

void iniLiv()
{
    switch (int(par_mac.car_tec.num_liv_tag.val())) {
    case 1:
        LIV_ALT = 1;
        LIV_BAS = 0;
        LIV_MED = 0;
        break;
    case 2:
        LIV_BAS = 1;
        LIV_ALT = 2;
        LIV_MED = 0;
        break;
    case 3:
        LIV_BAS = 1;
        LIV_MED = 2;
        LIV_ALT = 3;
        break;
    default:
        break;
    }
}

void iniValNonUti()
{
    using L = TipSchTagLiv;
    for (int i = int(par_mac.car_tec.num_liv_tag.val()) + 1; i <= 3; ++i) {
        par_mac.car_tec.sel_usc_liv(i) = "";
        par_mac.car_tec.pre_con_pom(i) = "";
        par_mac.opt.sca_liv(i) = "";
        par_abi.liv(i) = false;
        par_abi.num_mot_abi(i) = 0;
        for (int m = 1; m <= 3; ++m)
            par_abi.fun_mot_liv(i).fun_mot_mot(m) = false;

        // gir_max_mot_lam moved to par_cfg; every other sch_tag_liv field is cleared for both sets.
        for (TipSchTagLiv *liv : {&par_mac.sch_tag.sch_tag_liv(i), &par_mac.sch_tag_def.sch_tag_liv(i)}) {
            clearAll(*liv, {&L::dia_ruo_mis, &L::imp_gir_enc_ruo_mis, &L::vel_lam_jog, &L::ang_sincr, &L::cir_lam,
                            &L::cor_tag_reg_1o_sog, &L::cor_tag_reg_2o_sog, &L::dia_pull_roll, &L::dis_fot_lam, &L::dis_FTC_tac_lam,
                            &L::fat_k01_ott_pen, &L::fat_mol_enc_lam, &L::fat_mol_enc_pull_roll, &L::fat_mol_enc_ruo_mis,
                            &L::fin_vel_ott_pen, &L::imp_gir_enc_lam, &L::imp_gir_enc_pull_roll, &L::lun_tag_tes, &L::off_tag_reg,
                            &L::off_ram_pos, &L::off_ram_tag, &L::rap_rid_mot_lam, &L::sov_lam, &L::vel_zon_sinc_vmin,
                            &L::lim_inf_tol_tag, &L::lim_sup_tol_tag, &L::vel_sim_lap, &L::lun_tag_min, &L::lun_tag_max,
                            &L::gir_mot_lam_rif, &L::tem_cam_sta_vel, &L::sog_err_ins, &L::fin_pri_tac, &L::fin_dop_tac,
                            &L::sog_vel_cor, &L::vel_ftc, &L::dis_cen_mac_sen_fuo_car, &L::dis_edg_ftc, &L::sov_tap_usc_per_sca,
                            &L::sca_pri_giu, &L::sca_dop_giu, &L::dis_sen_lam, &L::dis_lam_flp, &L::dis_lam_rul_no_crh,
                            &L::tem_ape_flp, &L::tem_chi_flp, &L::tem_ape_rul_no_crh, &L::tem_chi_rul_no_crh,
                            &L::tem_sca_pre_ape_rul_no_crh, &L::tem_sca_pre_chi_rul_no_crh, &L::max_lun_sca, &L::min_lun_sca,
                            &L::sog_vel_1, &L::sog_lun_1, &L::sog_vel_2, &L::sog_lun_2, &L::sog_vel_3, &L::sog_lun_3,
                            &L::sog_lun_sup_3, &L::tem_att_cam_pre_rul_no_crh});
        }
        par_mac.config_sch_tag_att(i).descr = "";
        par_mac.config_sch_tag_def(i).descr = "";

        TipRulUscLiv &ru = par_mac.rul_usc.rul_usc_liv(i);
        ru.dis_lam_fla = "";
        ru.gap_tgl_cor = "";
        ru.gap_tgl_lun = "";
        ru.lun_san = "";
        ru.tem_ape_fla = "";
        ru.tem_chi_fla = "";

        TipPor &por = par_mac.com_sch_tag(i);
        por.baud = "";
        por.bit_par = "";
        por.dati = "";
        por.porta = "";
        por.bit_stop = "";
        por.TimeOut = "";
        par_mac.tip_com_sch_tag(i) = 0;
        par_mac.lan_sch_tag(i).por_loc = "";
        par_mac.lan_sch_tag(i).por_rem = "";
        par_mac.lan_sch_tag(i).ind_rem.clear();
    }
}

} // namespace stag
