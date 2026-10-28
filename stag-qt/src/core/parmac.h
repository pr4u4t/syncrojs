#pragma once

#include "vbtypes.h"

#include <QString>

// Tip_stag.bas parameter types and the Var_stag.bas globals par_mac, par_abi, pro_set.
namespace stag {

using vb::Array;
using vb::FixStr;

enum TipForMac { mm1800 = 1, mm2500 = 2, mm2800 = 3 };
enum TipGraCar { gm2_1800 = 0, gm2_2600 = 1 };
enum TipNorMac { CE = 0, UL = 1 };
enum TipSpde { Fosber10 = 1, Fosber20 = 2, Fosber21 = 3, Fosber30 = 4, Simon10 = 5, MHI10 = 6 };
enum TipSoc { PEER, SERVER, CLIENT };

constexpr int LIM_INF_SGL_ABI_LIN_DC_BUS = 200;
constexpr int LIM_SUP_SGL_ABI_LIN_DC_BUS = 500;
inline const char *const VAL_DEF_SGL_ABI_LIN_DC_BUS = "330";
inline const char *const STR_TRUE = "1";
inline const char *const STR_FALSE = "0";

struct TipCarTec {
    FixStr num_liv_tag{1};
    FixStr num_mot_liv_tag{1};
    qint32 for_mac = 0;
    qint32 gra_car = 0;
    qint32 nor_mac = 0;
    FixStr min_for_mac{4};
    FixStr min_lun_tag{4};
    FixStr max_lun_tag{4};
    FixStr lun_tag_sca{4};
    FixStr lun_tag_sca_pat_aut{4};
    FixStr pre_all{3};
    Array<FixStr, 1, 3> sel_usc_liv{FixStr(1)};
    Array<FixStr, 1, 3> pre_con_pom{FixStr(1)};
    FixStr sgl_abi_lin_DC_Bus{3};
    FixStr tip_azi_tag{1};
};

struct TipOpt {
    FixStr cut_mar{1};
    Array<FixStr, 1, 3> cut_pat{FixStr(1)};
    Array<FixStr, 1, 3> sca_liv{FixStr(1)};
    Array<FixStr, 1, 3> cut_pat_auto{FixStr(1)};
};

struct TipPor {
    FixStr porta{1};
    FixStr baud{5};
    FixStr bit_par{4};
    FixStr dati{1};
    FixStr bit_stop{3};
    FixStr TimeOut{5};
    bool com_att = false;
    bool ricevuto = false;
};

struct TipIndFins {
    qint16 net_id = 0;
    qint16 node_id = 0;
    qint16 unit_id = 0;
    qint16 TIMER_SCANS_PLC = 0;
};

struct TipLan {
    FixStr por_loc{6};
    FixStr por_rem{6};
    QString ind_rem;
    qint32 tip = PEER;
};

struct TipComSpde {
    qint32 ver_spde = 0;
    FixStr tip_com{1};
    TipPor tip_porta;
    TipLan lan;
    QString rx;
    QString ult_tx;
    qint32 tem_ult_tx = 0;
    qint16 err = 0;
    FixStr err_max{2};
    FixStr TimeOut{5};
};

struct TipSchTagLiv {
    FixStr dia_ruo_mis{8};
    FixStr vel_lam_jog{3};
    FixStr imp_gir_enc_ruo_mis{4};
    FixStr dis_fot_lam{8};
    FixStr dia_pull_roll{8};
    FixStr lun_tag_tes{8};
    FixStr sov_lam{3};
    FixStr dis_FTC_tac_lam{8};
    FixStr imp_gir_enc_lam{4};
    FixStr imp_gir_enc_pull_roll{4};
    FixStr fat_mol_enc_lam{1};
    FixStr fat_mol_enc_ruo_mis{1};
    FixStr fat_mol_enc_pull_roll{1};
    FixStr lim_inf_tol_tag{4};
    FixStr lim_sup_tol_tag{4};
    FixStr rap_rid_mot_lam{5};
    FixStr cir_lam{4};
    FixStr ang_sincr{3};
    FixStr vel_zon_sinc_vmin{5};
    FixStr fin_vel_ott_pen{3};
    FixStr fat_k01_ott_pen{3};
    FixStr off_ram_pos{3};
    FixStr off_ram_tag{3};
    FixStr vel_sim_lap{3};
    FixStr lun_tag_min{8};
    FixStr lun_tag_max{8};
    FixStr gir_mot_lam_rif{4};
    FixStr tem_cam_sta_vel{4};
    FixStr sog_err_ins{4};
    FixStr off_tag_reg{4};
    FixStr fin_pri_tac{4};
    FixStr fin_dop_tac{4};
    FixStr cor_tag_reg_1o_sog{4};   // cor_tag_reg_1°_sog
    FixStr cor_tag_reg_2o_sog{4};   // cor_tag_reg_2°_sog
    FixStr sog_vel_cor{3};
    FixStr vel_ftc{4};
    FixStr dis_cen_mac_sen_fuo_car{4};
    FixStr dis_edg_ftc{4};
    FixStr sov_tap_usc_per_sca{4};
    FixStr sca_pri_giu{4};
    FixStr sca_dop_giu{4};
    FixStr dis_sen_lam{8};
    FixStr dis_lam_flp{4};
    FixStr dis_lam_rul_no_crh{4};
    FixStr tem_ape_flp{4};
    FixStr tem_chi_flp{4};
    FixStr tem_ape_rul_no_crh{4};
    FixStr tem_chi_rul_no_crh{4};
    FixStr tem_sca_pre_ape_rul_no_crh{4};
    FixStr tem_sca_pre_chi_rul_no_crh{4};
    FixStr max_lun_sca{4};
    FixStr min_lun_sca{4};
    FixStr sog_vel_1{4};
    FixStr sog_lun_1{4};
    FixStr sog_vel_2{4};
    FixStr sog_lun_2{4};
    FixStr sog_vel_3{4};
    FixStr sog_lun_3{4};
    FixStr sog_lun_sup_3{4};
    FixStr tem_att_cam_pre_rul_no_crh{4};
};

struct TipParInf {
    FixStr ver_pro_sch_tag{5};
    FixStr ver_sch_tag{5};
    FixStr ver_PIC{5};
    FixStr ver_rad_qua_con_arr{5};
    FixStr qua_bit_rad_qua_con_arr{5};
    FixStr per_arr_rad_qua_con_arr{5};
    FixStr ver_rad_qua_sen_arr{5};
    FixStr qua_bit_rad_qua_sen_arr{5};
};

struct TipParCfgSchTagLiv {
    FixStr vel_max_tag{3};
    FixStr tem_acc_tgl_lun{4};
    FixStr tem_dec_tgl_lun{4};
    FixStr tem_acc_tgl_cor{4};
    FixStr tem_dec_tgl_cor{4};
    FixStr Kp_nor{4};
    FixStr Ki_nor{4};
    FixStr fil_kp{4};
    FixStr Kv_nor{4};
    FixStr lim_lav_Kv{3};
    FixStr gir_max_mot_lam{4};
};

struct TipSchTag {
    Array<TipParInf, 1, 3> par_inf;
    Array<TipSchTagLiv, 1, 3> sch_tag_liv;
};

struct TipRulNum {
    FixStr off_vel_met_min{3};
    FixStr off_vel_per{2};
    FixStr sov_sfi{2};
    FixStr dis_rot_mas{6};
};

struct TipRulIng {
    FixStr vel_max_lin{3};
    Array<TipRulNum, 1, 2> rul_num;
};

struct TipRulUscLiv {
    FixStr lun_san{4};
    FixStr gap_tgl_cor{5};
    FixStr gap_tgl_lun{5};
    FixStr tem_ape_fla{4};
    FixStr tem_chi_fla{4};
    FixStr dis_lam_fla{4};
};

struct TipRulUsc {
    Array<TipRulUscLiv, 1, 3> rul_usc_liv;
};

struct TipComAzi {
    FixStr TimeOut{5};
    FixStr baud{4};
};

struct TipParAzi {
    FixStr sta{3};
    FixStr cod_err{4};
    FixStr par_new{20};
};

struct TipParAziTag {
    Array<TipParAzi, 1, 3> mot;
};

struct TipAbiMot {
    Array<bool, 1, 3> fun_mot_mot{false};
};

struct TipAbi {
    Array<TipAbiMot, 1, 3> fun_mot_liv;
    Array<qint16, 1, 3> num_mot_abi{0};
    Array<bool, 1, 3> liv{false};
    bool abi_com_PU = false;
    bool abi_rep_pro = false;
    bool abi_err_com_PU = false;
};

struct TipConfigSchTag {
    FixStr descr{35};
    qint16 num_mot_liv = 0;
};

struct TipPlcLan {
    FixStr por_rem{5};
    QString ind_rem;
    FixStr TimeOut{5};
    FixStr abi_lan{1};
};

struct TipParMac {
    TipCarTec car_tec;
    TipOpt opt;
    TipComAzi com_azi;
    TipComSpde com_spde;
    Array<FixStr, 1, 3> tip_com_sch_tag{FixStr(1)};
    Array<TipLan, 1, 3> lan_sch_tag;
    Array<bool, 1, 3> lan_sch_tag_UDP{false};
    Array<TipPor, 1, 3> com_sch_tag;
    TipIndFins indirizzo_plc;
    TipPlcLan com_plc_lan;
    Array<TipParAzi, 1, 3> par_azi_usc;
    Array<TipParAzi, 1, 2> par_azi_ing;
    Array<TipParAziTag, 1, 3> par_azi_tag;
    Array<TipConfigSchTag, 1, 3> config_sch_tag_att;
    Array<TipConfigSchTag, 1, 3> config_sch_tag_def;
    Array<TipParCfgSchTagLiv, 1, 3> par_cfg_att;
    Array<TipParCfgSchTagLiv, 1, 3> par_cfg_def;
    TipSchTag sch_tag_def;
    TipSchTag sch_tag;
    TipRulIng rul_ing;
    TipRulUsc rul_usc;
};

struct TipProSet {
    QString pro_pat;
    QString Nam_fil_uni;
    QString Nam_fil_dat_ord;
    QString Nam_fil_dat_ord_ese;
    qint16 lin_att = 0;
    bool ute_wrk = false;
    QString abi_reg_rep_pro;
    QString ini_prod;
    QString pwd;
    bool ric_msg_db = false;
    qint16 PercX = 0;
    qint16 PercY = 0;
    bool ute_wrk_liv1 = false;
    QString pwd_liv1;
    QString per_cli;
    qint16 tur = 0;
    QString JOB;
    QString Commessa;
    QString pro_bck_loc_zip;
    QString pro_bck_rem_zip;
    QString nam_fil_log_zip;
    QString nam_fil_cod_zip;
    QString nam_fil_dat_zip;
};

inline TipParMac par_mac;
inline TipParMac par_mac_ute;
inline TipAbi par_abi;
inline TipAbi par_abi_ute;
inline TipProSet pro_set;
inline bool abi_mis_ang = false;

// codici_errore.txt
enum CodRet { RET_OK = 1, RET_ZRO = 0, RET_WRK = 2, RET_EXE = 3, RET_BAD = -10 };

constexpr int MAXINT = 32767;
constexpr int MAX_NUM_ORD = 100;
constexpr int MAX_NUM_ORD_ESE = 100;
constexpr int TODECMM = 10;
constexpr int CORRENTE = 1;
constexpr int SUCCESSIVO = 2;

inline qint16 LIV_BAS = 0;
inline qint16 LIV_MED = 0;
inline qint16 LIV_ALT = 0;
inline bool Prb_lis_ord = false;
inline Array<bool, 1, 3> com_ini_sch_tag{false};
inline Array<qint16, 1, 3> vel_max_tag_liv{0};
inline Array<bool, 1, 2> inv_ord_sch_tag{false};

// Format$(v, "000"): zero padded integer.
inline QString ft(qint64 value, int digits)
{
    const QString s = QString::number(value < 0 ? -value : value).rightJustified(digits, u'0');
    return value < 0 ? u'-' + s : s;
}

} // namespace stag
