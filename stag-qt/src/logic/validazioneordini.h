#pragma once

#include "collezioneordini.h"

#include <QSqlDatabase>
#include <QString>

// validazione_ordini.bas. Some checks also normalize the order, as in VB (zeros, blank levels).
namespace stag {

constexpr int COD_ERR_LUN_COD = 100;
constexpr int COD_ERR_ALT_BOB = 101;
constexpr int COD_ERR_ASS_OND = 102;
constexpr int COD_ERR_ASS_SCA_PER_TAG = 150;
constexpr int COD_ERR_ASS_CAR = 151;
constexpr int COD_ERR_ABI_LIV = 152;
constexpr int COD_ERR_COD_COM = 153;
constexpr int COD_ERR_SCA_LUN = 154;
constexpr int COD_ERR_ASS_CUT_TO_MARK = 155;
constexpr int COD_ERR_CUT_TO_MARK = 156;
constexpr int COD_ERR_LIV_LUN_DIV = 157;
constexpr int COD_ERR_ASS_DAT = 158;
constexpr int COD_ERR_MAX_SCA_LAR = 159;
constexpr int COD_ERR_ASS_LUN = 160;
constexpr int COD_ERR_SCA_PER_TAG = 161;
constexpr int COD_ERR_ASS_NUM_FOG = 162;
constexpr int COD_ERR_CUT_TO_MARK_LIV_ABB = 167;

constexpr int COD_ERR_COD_PAT = 168;
constexpr int COD_ERR_OFS_CUT_PAT = 169;
constexpr int COD_ERR_PAT_CON = 170;
constexpr int COD_ERR_FIN_PRI = 171;
constexpr int COD_ERR_FIN_DOP = 172;
constexpr int COD_ERR_MIS_PAT = 173;
constexpr int COD_ERR_PRI_LET_FTC = 174;
constexpr int COD_ERR_POS_PAT = 175;

bool err_lun_Cod_ord_abb(const ClasseOrdine &ord);
bool err_ass_ond(const ClasseOrdine &ord);
bool err_ass_car(const ClasseOrdine &ord);
bool err_alt_bob(const ClasseOrdine &ord, int forMac);
bool err_ass_liv(const ClasseOrdine &ord, int ind);
bool err_abi_liv(const ClasseOrdine &ord, int ind);
bool err_cod_com(const QString &code);
bool err_max_sca_lar(const ClasseOrdine &ord);
bool err_ass_lun(const ClasseOrdine &ord, int ind);
bool err_ass_sca_lar(const ClasseOrdine &ord, int ind);
bool err_sca_lun(const ClasseOrdine &ord, int ind);
bool err_num_sca(const ClasseOrdine &ord, int ind);
bool err_som_sca(const ClasseOrdine &ord);
bool err_num_fog(const ClasseOrdine &ord, int ind);
int err_cut_to_mark(ClasseOrdine &ord, int ind, bool chkForAbb = true);
bool err_cod_cut_pat(const ClasseOrdine &ord, int ind, bool chkForAbb = true);
bool err_ofs_cut_pat(ClasseOrdine &ord, int ind, bool chkForAbb = true);
bool err_pri_let_FTC(ClasseOrdine &ord, int ind, bool chkForAbb = true);
bool err_pat_con(ClasseOrdine &ord, int ind, bool chkForAbb = true);
bool err_fin_pri(ClasseOrdine &ord, int ind, bool chkForAbb = true);
bool err_fin_dop(ClasseOrdine &ord, int ind, bool chkForAbb = true);
bool err_pos_pat(const ClasseOrdine &ord);
bool err_mis_pat(ClasseOrdine &ord, int ind);
bool err_liv_dop(const ClasseOrdine &ord);
// Writes the pattern of the format into PATTERN; false when a cut-to-pattern format has no code.
bool con_uff_ass_cod(const ClasseOrdine &ord, int format, QSqlDatabase &db, bool insMan = false);

// val_ord: RET_OK or a COD_ERR_* code.
int val_ord(ClasseOrdine &ord, QSqlDatabase &db, bool insMan = false);
// msg_err_val_ord: the key of the message for a val_ord result.
QString msg_err_val_ord(int result);

} // namespace stag
