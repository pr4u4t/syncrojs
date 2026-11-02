#include "validazioneordini.h"
#include "sqlcheck.h"

#include "gesord.h"
#include "parmac.h"
#include "staglog.h"
#include "vbcompat.h"

#include <QSqlError>
#include <QSqlQuery>

namespace stag {

namespace {

constexpr int NO = 0;
constexpr int SI = 1;
constexpr int PRE_PATTERN = 2;
constexpr int CUT_PAT = 2;
constexpr int MIN_NUM_CTR = 0;
constexpr int MAX_NUM_CTR = 4;
constexpr int MIN_LET_FTC = 0;
constexpr int MAX_LET_FTC = 1;
constexpr int MIN_OFF_PAT = -100;
constexpr double MUL_OFS_CUT_PAT = 1;
constexpr double MUL_FIN_PRI_TAC = 1.5;
constexpr double MUL_FIN_DOP_TAC = 1.5;

QString field(const ClasseOrdine &ord, int ind, const char *name)
{
    return ord.ord(ind, QLatin1String(name));
}

double number(const ClasseOrdine &ord, int ind, const char *name)
{
    return vb::val(field(ord, ind, name));
}

// val(x) \ TODECMM
qint32 tenths(const ClasseOrdine &ord, int ind, const char *name)
{
    return vb::intDiv(number(ord, ind, name), TODECMM);
}

void setField(ClasseOrdine &ord, int ind, const char *name, const QString &value)
{
    ord.setText(QStringLiteral("ord(%1).%2").arg(ind).arg(QLatin1String(name)), value);
}

bool optionOn(const vb::FixStr &option)
{
    return option == QLatin1String(STR_TRUE);
}

// The tail of err_pat_con, err_fin_pri, err_fin_dop and err_ofs_cut_pat: two formats on a level must agree.
bool differsOnSameLevel(const ClasseOrdine &ord, int ind, const char *name, const char *presence, int value, int lowestLevel, bool chkForAbb)
{
    const int level = int(number(ord, ind, "Ord_liv"));
    if (level < lowestLevel || level > 3 || level < 1 || !chkForAbb || !optionOn(par_mac.opt.cut_pat(level)))
        return false;
    for (int i = 1; i <= 3; ++i) {
        if (number(ord, i, presence) > 0 && i != ind && level == int(number(ord, i, "Ord_liv")) && value != int(number(ord, i, name)))
            return true;
    }
    return false;
}

// A bounded field where zero means "not set" and is rewritten as zeros.
bool checkOptional(ClasseOrdine &ord, int ind, const char *name, int minimum, double maximum, const char *presence, int lowestLevel, bool chkForAbb, int width)
{
    if (ind < 1 || ind > 3)
        return true;
    const int value = int(number(ord, ind, name));
    if (value == 0) {
        setField(ord, ind, name, QString(width, u'0'));
        return false;
    }
    if (value < minimum || value > maximum)
        return true;
    return differsOnSameLevel(ord, ind, name, presence, value, lowestLevel, chkForAbb);
}

} // namespace

bool err_lun_Cod_ord_abb(const ClasseOrdine &ord)
{
    return vb::trim(ord.cod_ord_abb()).isEmpty();
}

// These two compare with blanks only: a field full of NULs passes, as in VB.
bool err_ass_ond(const ClasseOrdine &ord)
{
    const QString value = ord.text(QStringLiteral("tip_car.Cod_ond"));
    return value == QString(value.size(), u' ');
}

bool err_ass_car(const ClasseOrdine &ord)
{
    const QString value = ord.text(QStringLiteral("tip_car.cod_com_car"));
    return value == QString(value.size(), u' ');
}

bool err_alt_bob(const ClasseOrdine &ord, int forMac)
{
    if (par_mac.com_spde.ver_spde == Fosber10)
        return false;
    const qint32 value = vb::intDiv(vb::val(ord.text(QStringLiteral("alt_bob")).replace(QChar(0), u' ')), TODECMM);
    return value < par_mac.car_tec.min_for_mac.val() || value > forMac;
}

bool err_ass_liv(const ClasseOrdine &ord, int ind)
{
    const QString level = field(ord, ind, "Ord_liv");
    bool error = level == QString(level.size(), u' ');
    const QString trimmed = vb::trim(level);
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    // InStr("12", "") is 1, so a blank level only fails through the comparison above.
    if (levels == 1 && !QStringLiteral("1").contains(trimmed))
        error = true;
    else if (levels == 2 && !QStringLiteral("12").contains(trimmed))
        error = true;
    else if (levels == 3 && !QStringLiteral("123").contains(trimmed))
        error = true;
    return error;
}

bool err_abi_liv(const ClasseOrdine &ord, int ind)
{
    const int level = int(number(ord, ind, "Ord_liv"));
    if (level >= 1 && level <= 3)
        return !par_abi.liv(level);
    log_str(QStringLiteral("Modulo validazione_ordini Function err_abi_liv: valore inaspettato per l'espressione Val(ord.ord(ind).Ord_liv). Tale espressione vale infatti '%1'").arg(field(ord, ind, "Ord_liv")), MED_PRIORITY, CosLog::Ord);
    return false;
}

bool err_cod_com(const QString &code)
{
    for (const QChar c : vb::trim(code.toUpper())) {
        if (c.unicode() < 32 || c.unicode() > 127)
            return true;
    }
    return false;
}

bool err_max_sca_lar(const ClasseOrdine &ord)
{
    if (par_mac.com_spde.ver_spde == Fosber10)
        return false;
    const qint32 height = vb::intDiv(vb::val(ord.text(QStringLiteral("alt_bob"))), TODECMM);
    qint32 total = 0;
    for (int i = 1; i <= 3; ++i) {
        if (vb::trim(field(ord, i, "Ord_liv")).isEmpty())
            continue;
        const qint32 width = tenths(ord, i, "sca_lar");
        if (width > height)
            return true;
        total += vb::trim(field(ord, i, "Sca_per_tag")).isEmpty() ? width : qint32(width * number(ord, i, "Sca_per_tag"));
    }
    return total > height;
}

bool err_ass_lun(const ClasseOrdine &ord, int ind)
{
    return tenths(ord, ind, "sca_lun") == 0;
}

bool err_ass_sca_lar(const ClasseOrdine &ord, int ind)
{
    return tenths(ord, ind, "sca_lar") == 0;
}

bool err_sca_lun(const ClasseOrdine &ord, int ind)
{
    const qint32 length = tenths(ord, ind, "sca_lun");
    return length > par_mac.car_tec.max_lun_tag.val() || length < par_mac.car_tec.min_lun_tag.val();
}

bool err_num_sca(const ClasseOrdine &ord, int ind)
{
    return number(ord, ind, "Sca_per_tag") == 0;
}

bool err_som_sca(const ClasseOrdine &ord)
{
    if (par_mac.com_spde.ver_spde == Fosber10)
        return false;
    qint32 total = 0;
    for (int i = 1; i <= 3; ++i) {
        if (vb::trim(field(ord, i, "Ord_liv")).isEmpty())
            continue;
        const qint32 width = tenths(ord, i, "sca_lar");
        total += vb::trim(field(ord, i, "Sca_per_tag")).isEmpty() ? width : qint32(width * number(ord, i, "Sca_per_tag"));
    }
    return total > vb::intDiv(vb::val(ord.text(QStringLiteral("alt_bob"))), TODECMM);
}

bool err_num_fog(const ClasseOrdine &ord, int ind)
{
    return vb::trim(field(ord, ind, "sca_tot_num")).isEmpty() || number(ord, ind, "sca_tot_num") == 0;
}

int err_cut_to_mark(ClasseOrdine &ord, int ind, bool chkForAbb)
{
    if (ind < 1 || ind > 3)
        return RET_BAD;
    const int value = int(number(ord, ind, "cut_to_mark"));
    if (value == 0) {
        setField(ord, ind, "cut_to_mark", QString::number(NO));
        return 0;
    }
    if (value < NO || value > PRE_PATTERN)
        return 1;
    if (value == SI && !optionOn(par_mac.opt.cut_mar))
        return 2;
    const int level = int(number(ord, ind, "Ord_liv"));
    // cut_pat(0) does not exist in VB either: the arrays start at 1, so a level of 0 stops with an error there.
    if (level < 1 || level > 3)
        return 0;
    const bool automatic = optionOn(par_mac.opt.cut_pat_auto(level));
    const bool pattern = optionOn(par_mac.opt.cut_pat(level));
    if (value == PRE_PATTERN && !automatic && !pattern)
        return 2;
    if (chkForAbb && (pattern || automatic)) {
        for (int i = 1; i <= 3; ++i) {
            if (number(ord, i, "sca_lar") > 0 && i != ind && level == int(number(ord, i, "Ord_liv")) && value != int(number(ord, i, "cut_to_mark")))
                return 3;
        }
    }
    return 0;
}

bool err_cod_cut_pat(const ClasseOrdine &ord, int ind, bool chkForAbb)
{
    if (ind < 1 || ind > 3)
        return true;
    const int level = int(number(ord, ind, "Ord_liv"));
    if (level < 1 || level > 3 || !chkForAbb || !optionOn(par_mac.opt.cut_pat(level)))
        return false;
    for (int i = 1; i <= 3; ++i) {
        if (number(ord, i, "sca_lun") > 0 && i != ind && level == int(number(ord, i, "Ord_liv")) && field(ord, i, "pat_cod") != field(ord, ind, "pat_cod"))
            return true;
    }
    return false;
}

bool err_ofs_cut_pat(ClasseOrdine &ord, int ind, bool chkForAbb)
{
    const double limit = vb::cLng((number(ord, qBound(1, ind, 3), "sca_lun") / TODECMM) * MUL_OFS_CUT_PAT);
    return checkOptional(ord, ind, "Ofs_Cut", MIN_OFF_PAT, limit, "sca_lar", 1, chkForAbb, 4);
}

bool err_pri_let_FTC(ClasseOrdine &ord, int ind, bool chkForAbb)
{
    return checkOptional(ord, ind, "fst_FTC", MIN_LET_FTC, MAX_LET_FTC, "sca_lun", 1, chkForAbb, 1);
}

bool err_pat_con(ClasseOrdine &ord, int ind, bool chkForAbb)
{
    return checkOptional(ord, ind, "pat_con", MIN_NUM_CTR, MAX_NUM_CTR, "sca_lar", 0, chkForAbb, 1);
}

bool err_fin_pri(ClasseOrdine &ord, int ind, bool chkForAbb)
{
    const double limit = vb::cLng((number(ord, qBound(1, ind, 3), "sca_lun") / TODECMM) * MUL_FIN_PRI_TAC);
    return checkOptional(ord, ind, "fin_pri", 0, limit, "sca_lar", 1, chkForAbb, 4);
}

bool err_fin_dop(ClasseOrdine &ord, int ind, bool chkForAbb)
{
    const double limit = vb::cLng((number(ord, qBound(1, ind, 3), "sca_lun") / TODECMM) * MUL_FIN_DOP_TAC);
    return checkOptional(ord, ind, "fin_dop", 0, limit, "sca_lar", 1, chkForAbb, 4);
}

// VB loops over all the formats, whatever format it was called for.
bool err_pos_pat(const ClasseOrdine &ord)
{
    const double height = vb::val(ord.text(QStringLiteral("alt_bob"))) / TODECMM;
    for (int i = 1; i <= 3; ++i) {
        const double position = number(ord, i, "pos_pattern");
        if (number(ord, i, "cut_to_mark") > 0 && (position < 0 || position > height))
            return true;
    }
    return false;
}

bool err_mis_pat(ClasseOrdine &ord, int ind)
{
    if (int(number(ord, ind, "cut_to_mark")) != CUT_PAT)
        return false;
    const auto notch = [&](int i) { return int(vb::val(ord.ord(ind, QStringLiteral("L%1").arg(i)))); };
    int stop = 1;
    int longest = 0;
    for (; stop <= 10; ++stop) {
        const int value = notch(stop);
        longest = qMax(longest, value);
        if (value == 0)
            break;
    }
    if (stop == 1)
        return true;
    for (int i = stop; i <= 10; ++i)
        ord.setText(QStringLiteral("ord(%1).L%2").arg(ind).arg(i), QStringLiteral("0"));
    for (int i = 1; i < stop; ++i) {
        if (notch(i) < 5.0 / 64 * longest)
            return true;
        if (i > 1 && notch(i) <= notch(i - 1))
            return true;
    }
    return false;
}

bool err_liv_dop(const ClasseOrdine &ord)
{
    for (int i = 1; i <= 3; ++i) {
        if (tenths(ord, i, "sca_lar") == 0 || vb::trim(field(ord, i, "Ord_liv")).isEmpty())
            continue;
        for (int k = 1; k <= 3; ++k) {
            if (k != i && tenths(ord, k, "sca_lar") > 0 && vb::trim(field(ord, k, "Ord_liv")) == vb::trim(field(ord, i, "Ord_liv"))
                && tenths(ord, i, "sca_lun") != tenths(ord, k, "sca_lun"))
                return true;
        }
    }
    return false;
}

bool con_uff_ass_cod(const ClasseOrdine &ord, int format, QSqlDatabase &db, bool insMan)
{
    if (int(number(ord, format, "cut_to_mark")) != CUT_PAT)
        return true;
    const QString code = vb::trim(QString(field(ord, format, "pat_cod")).replace(QChar(0), u' '));
    if (code.isEmpty())
        return false;

    QSqlQuery find(db);
    sqlPrepare(find, QStringLiteral("SELECT rec_no FROM PATTERN WHERE upper(CODICE) = ?"));
    find.addBindValue(code.toUpper());
    const bool exists = find.exec() && find.next();
    const QString recNo = exists ? find.value(0).toString() : QString();
    find.finish();

    QStringList columns{QStringLiteral("PRI_LET_FC")};
    QVariantList values{vb::trim(field(ord, format, "fst_FTC"))};
    for (int l = 1; l <= 10; ++l) {
        columns << QStringLiteral("L%1").arg(l);
        values << vb::trim(ord.ord(format, QStringLiteral("L%1").arg(l)));
    }
    columns << QStringLiteral("OFS_CUT");
    values << vb::trim(field(ord, format, "Ofs_Cut"));
    if (!exists || !insMan) {
        columns.prepend(QStringLiteral("CONTRASTO"));
        values.prepend(vb::trim(field(ord, format, "pat_con")));
    }

    QSqlQuery write(db);
    if (exists) {
        sqlPrepare(write, QStringLiteral("UPDATE PATTERN SET %1 = ? WHERE rec_no = ?").arg(columns.join(QStringLiteral(" = ?, "))));
        for (const QVariant &value : values)
            write.addBindValue(value);
        write.addBindValue(recNo);
    } else {
        columns.prepend(QStringLiteral("CODICE"));
        values.prepend(vb::trim(field(ord, format, "pat_cod")));
        sqlPrepare(write, QStringLiteral("INSERT INTO PATTERN (%1) VALUES (%2)").arg(columns.join(u',')).arg(QStringList(columns.size(), QStringLiteral("?")).join(u',')));
        for (const QVariant &value : values)
            write.addBindValue(value);
    }
    if (!write.exec())
        log_str(QStringLiteral("con_uff_ass_cod: ") + write.lastError().text(), MED_PRIORITY, CosLog::Gen);
    return true;
}

int val_ord(ClasseOrdine &ord, QSqlDatabase &db, bool insMan)
{
    if (err_lun_Cod_ord_abb(ord))
        return COD_ERR_LUN_COD;
    if (err_ass_ond(ord))
        return COD_ERR_ASS_OND;
    if (err_ass_car(ord))
        return COD_ERR_ASS_CAR;
    if (ordineVuoto(ord))
        return RET_OK;

    bool missingFormat = false;
    for (int i = 1; i <= 3; ++i) {
        if (i == 1 && err_ass_liv(ord, i))
            return COD_ERR_ASS_DAT;
        if (number(ord, i, "Sca_per_tag") != 0 && tenths(ord, i, "sca_lar") != 0 && tenths(ord, i, "sca_lun") != 0) {
            if (missingFormat || err_ass_liv(ord, i))
                return COD_ERR_ASS_DAT;
            if (err_abi_liv(ord, i))
                return COD_ERR_ABI_LIV;
            if (err_cod_com(field(ord, i, "com.cod_com_ord")))
                return COD_ERR_COD_COM;
            if (err_max_sca_lar(ord))
                return COD_ERR_MAX_SCA_LAR;
            if (err_ass_lun(ord, i))
                return COD_ERR_ASS_LUN;
            if (err_sca_lun(ord, i))
                return COD_ERR_SCA_LUN;
            if (err_num_sca(ord, i))
                return COD_ERR_ASS_SCA_PER_TAG;
            if (err_som_sca(ord))
                return COD_ERR_SCA_PER_TAG;
            if (err_num_fog(ord, i))
                return COD_ERR_ASS_NUM_FOG;
            const int cut = err_cut_to_mark(ord, i);
            if (cut == 1)
                return COD_ERR_ASS_CUT_TO_MARK;
            if (cut == 2)
                return COD_ERR_CUT_TO_MARK;
            if (cut == 3)
                return COD_ERR_CUT_TO_MARK_LIV_ABB;
            if (err_cod_cut_pat(ord, i))
                return COD_ERR_COD_PAT;
            if (err_ofs_cut_pat(ord, i))
                return COD_ERR_OFS_CUT_PAT;
            if (err_pri_let_FTC(ord, i))
                return COD_ERR_PRI_LET_FTC;
            if (err_pat_con(ord, i))
                return COD_ERR_PAT_CON;
            if (err_fin_pri(ord, i))
                return COD_ERR_FIN_PRI;
            if (err_fin_dop(ord, i))
                return COD_ERR_FIN_DOP;
            if (!con_uff_ass_cod(ord, i, db, insMan))
                return COD_ERR_COD_PAT;
            if (err_mis_pat(ord, i))
                return COD_ERR_POS_PAT;
        } else {
            setField(ord, i, "Ord_liv", QStringLiteral(" "));
            missingFormat = true;
        }
    }
    if (err_liv_dop(ord))
        return COD_ERR_LIV_LUN_DIV;
    return RET_OK;
}

QString msg_err_val_ord(int result)
{
    switch (result) {
    case COD_ERR_LUN_COD: return QStringLiteral("ins_cod_ord");
    case COD_ERR_ASS_OND: return QStringLiteral("ins_onda");
    case COD_ERR_ASS_CAR: return QStringLiteral("ins_carta");
    case COD_ERR_ALT_BOB: return QStringLiteral("err_alt_bob");
    case COD_ERR_ASS_DAT: return QStringLiteral("ins_all_dat");
    case COD_ERR_ABI_LIV: return QStringLiteral("liv_non_abi");
    case COD_ERR_COD_COM: return QStringLiteral("err_cod_com");
    case COD_ERR_MAX_SCA_LAR: return QStringLiteral("err_max_sca_lar");
    case COD_ERR_ASS_LUN: return QStringLiteral("ins_lun_for");
    case COD_ERR_SCA_LUN: return QStringLiteral("err_sca_lun");
    case COD_ERR_ASS_SCA_PER_TAG: return QStringLiteral("err_ass_sca_per_tag");
    case COD_ERR_SCA_PER_TAG: return QStringLiteral("err_sca_per_tag");
    case COD_ERR_ASS_CUT_TO_MARK: return QStringLiteral("ins_cut_to_mark");
    case COD_ERR_CUT_TO_MARK: return QStringLiteral("cut_to_mark_non_pre");
    case COD_ERR_CUT_TO_MARK_LIV_ABB: return QStringLiteral("cut_to_mark_liv_abb");
    case COD_ERR_COD_PAT: return QStringLiteral("err_cod_pat");
    case COD_ERR_OFS_CUT_PAT: return QStringLiteral("err_ofs_cut_pat");
    case COD_ERR_MIS_PAT: return QStringLiteral("err_mis_pat");
    case COD_ERR_PAT_CON: return QStringLiteral("err_pat_con");
    case COD_ERR_FIN_PRI: return QStringLiteral("err_fin_pri");
    case COD_ERR_FIN_DOP: return QStringLiteral("err_fin_dop");
    case COD_ERR_LIV_LUN_DIV: return QStringLiteral("err_liv_lun_div");
    case COD_ERR_POS_PAT: return QStringLiteral("err_pos_pat");
    default: return QStringLiteral("imp_ese_ope");
    }
}

} // namespace stag
