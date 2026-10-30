#include "insordmodel.h"

#include "classestato.h"
#include "messages.h"
#include "parmac.h"
#include "stabarmodel.h"
#include "stagglobals.h"
#include "stagrecords.h"
#include "uihooks.h"
#include "unimisang.h"
#include "validazioneordini.h"
#include "vbcompat.h"

#include <QSqlQuery>

#include <cmath>

using namespace stag;

static constexpr int NO = 0;
static constexpr int SI = 1;
static constexpr int PRE_PATTERN = 2;
static constexpr int CUT_PAT = 2;

static QString at(const char *name, int index)
{
    return QStringLiteral("%1(%2)").arg(QLatin1String(name)).arg(index);
}

static const QStringList HEADER_TEXTS{QStringLiteral("txt_cod_abb"), QStringLiteral("txt_alt_bob")};
static const char *const FORMAT_TEXTS[] = {"txt_cod_com", "txt_cod_cli", "txt_nom_cli", "txt_sca_lar", "txt_sca_lun", "txt_resa",
                                           "txt_num_tot_fog", "txt_pos_lat_pat", "txt_pat_con", "txt_fin_pri", "txt_fin_dop"};

// Uni_mis_ang: inches back to tenths of a millimetre and to millimetres.
static qint32 incToDmm(const QString &inches)
{
    return vb::cLng(vb::val(inches) * TODECMM * INCHMM);
}

InsOrdModel::InsOrdModel(CollezioneOrdini *orders, const QSqlDatabase &db, QObject *parent)
    : FormModel(parent)
    , m_orders(orders)
    , m_db(db)
{
    m_rec.setOrdine(QByteArray(kOrderRecordSize, ' '));
    connect(m_orders, &CollezioneOrdini::Refresh, this, [this](bool shift) { refresh(shift); });
}

void InsOrdModel::setText(const QString &control, const QString &value)
{
    m_text.insert(control, value);
    FormModel::setText(control, value);
}

void InsOrdModel::setListIndex(const QString &control, int index)
{
    m_listIndex.insert(control, index);
    set(control, QStringLiteral("listIndex"), index);
}

void InsOrdModel::setDbCombo(const QString &control, const QStringList &items, const QString &text)
{
    set(control, QStringLiteral("items"), items);
    setText(control, text);
}

void InsOrdModel::focus(const QString &control)
{
    set(control, QStringLiteral("focusCount"), ++m_focusCount);
}

void InsOrdModel::load()
{
    m_controls = HEADER_TEXTS;
    m_controls << QStringLiteral("cmb_onda") << QStringLiteral("cmb_carta");
    for (int i = 0; i < 3; ++i) {
        for (const char *name : FORMAT_TEXTS)
            m_controls << at(name, i);
        m_controls << at("txt_liv", i) << at("cmb_cut_to_mark", i) << at("cmb_pat_cod", i);
    }
    for (const QString &control : m_controls) {
        set(control, QStringLiteral("modelFocus"), true);
        set(control, QStringLiteral("focusCount"), 0);
    }
    const int width = abi_mis_ang ? 8 : 4; // acc_new_car: one character less than the field, more for inches
    set(QStringLiteral("txt_alt_bob"), QStringLiteral("maxLength"), width);
    const auto numeric = [this](const QString &control, const QString &extra = QString()) {
        set(control, QStringLiteral("digitsOnly"), true);
        if (!extra.isEmpty())
            set(control, QStringLiteral("extraChars"), extra);
    };
    const QString dot = abi_mis_ang ? QStringLiteral(".") : QString();
    numeric(QStringLiteral("txt_alt_bob"), dot);
    for (int i = 0; i < 3; ++i) {
        numeric(at("txt_sca_lar", i), dot);
        numeric(at("txt_sca_lun", i), dot);
        set(at("txt_sca_lar", i), QStringLiteral("maxLength"), width);
        set(at("txt_sca_lun", i), QStringLiteral("maxLength"), width);
        numeric(at("txt_resa", i));
        numeric(at("txt_num_tot_fog", i));
        numeric(at("txt_pos_lat_pat", i), QStringLiteral("-"));
        numeric(at("txt_pat_con", i));
        numeric(at("txt_fin_pri", i));
        numeric(at("txt_fin_dop", i));
    }
    iniForm();
    reloadFlutes();
    reloadPatterns();
    enableFrames(false);
    applyCutPatternStops();
    if (m_orders->Count() > 0)
        riempiCelle(qMax(m_orders->leg_pun_ord(), 1));
    commit();
}

// ini_form
void InsOrdModel::iniForm()
{
    QStringList levels{msg(QStringLiteral("liv_alt"))};
    m_levelData = {LIV_ALT};
    const int machineLevels = int(par_mac.car_tec.num_liv_tag.val());
    if (machineLevels == 3) {
        levels << msg(QStringLiteral("liv_med"));
        m_levelData << LIV_MED;
    }
    if (machineLevels >= 2) {
        levels << msg(QStringLiteral("liv_bas"));
        m_levelData << LIV_BAS;
    }
    QStringList modes{msg(QStringLiteral("msg_no")), msg(QStringLiteral("msg_si"))};
    m_cutData = {NO, SI};
    bool pattern = false;
    for (int level = 1; level <= 3; ++level)
        pattern = pattern || par_mac.opt.cut_pat(level) == QLatin1String(STR_TRUE) || par_mac.opt.cut_pat_auto(level) == QLatin1String(STR_TRUE);
    if (pattern) {
        modes << msg(QStringLiteral("msg_cut_pat"));
        m_cutData << PRE_PATTERN;
    }
    for (int i = 0; i < 3; ++i) {
        set(at("txt_liv", i), QStringLiteral("items"), levels);
        set(at("cmb_cut_to_mark", i), QStringLiteral("items"), modes);
    }
}

void InsOrdModel::reloadFlutes()
{
    QStringList flutes;
    QSqlQuery query(m_db);
    if (query.exec(QStringLiteral("SELECT DISTINCT ONDA FROM ONDE ORDER BY ONDA"))) {
        while (query.next())
            flutes << query.value(0).toString();
    }
    set(QStringLiteral("cmb_onda"), QStringLiteral("items"), flutes);
}

// cmb_onda_Change: the papers of that flute, showing the first.
void InsOrdModel::reloadPapers(const QString &flute)
{
    QStringList papers;
    QSqlQuery query(m_db);
    query.prepare(QStringLiteral("SELECT CARTA FROM ONDE WHERE ONDA = ?"));
    query.addBindValue(vb::trim(flute));
    if (query.exec()) {
        while (query.next())
            papers << query.value(0).toString();
    }
    setDbCombo(QStringLiteral("cmb_carta"), papers, papers.value(0));
}

void InsOrdModel::reloadPatterns()
{
    QStringList codes;
    QSqlQuery query(m_db);
    if (query.exec(QStringLiteral("SELECT CODICE FROM PATTERN ORDER BY CODICE"))) {
        while (query.next())
            codes << query.value(0).toString();
    }
    for (int i = 0; i < 3; ++i)
        set(at("cmb_pat_cod", i), QStringLiteral("items"), codes);
}

void InsOrdModel::enableFrames(bool enabled)
{
    for (const QString &control : m_controls)
        set(control, QStringLiteral("enabled"), enabled);
    set(QStringLiteral("frm_tes"), QStringLiteral("enabled"), enabled);
    for (int i = 0; i < 3; ++i)
        set(at("frm_liv0", i), QStringLiteral("enabled"), enabled);
}

// clear_cell
void InsOrdModel::clearCell()
{
    for (const QString &control : m_controls) {
        if (control.startsWith(QLatin1String("txt_liv")) || control.startsWith(QLatin1String("cmb_cut_to_mark")))
            setListIndex(control, -1);
        else
            setText(control, QString());
    }
    setText(QStringLiteral("txt_met"), QString());
    setText(QStringLiteral("txt_id"), QString());
    setText(QStringLiteral("txt_indice"), QString());
    for (int i = 0; i < 3; ++i)
        setText(at("txt_lun_for", i), QString());
}

// riempi_celle
void InsOrdModel::riempiCelle(int order)
{
    const OrdinePtr source = m_orders->item(order);
    m_rec.setOrdine(source->ordine());
    const auto top = [&](const char *name) { return source->text(QLatin1String(name)); };
    setText(QStringLiteral("txt_alt_bob"), abi_mis_ang ? con_dmm_to_inc(vb::trim(top("alt_bob"))) : QString::number(vb::intDiv(vb::val(top("alt_bob")), TODECMM)));
    setText(QStringLiteral("txt_met"), abi_mis_ang ? con_met_to_fet(vb::trim(top("met_tot_ord"))) : top("met_tot_ord"));
    setText(QStringLiteral("txt_cod_abb"), QString(top("cod_ord_abb")).replace(QChar(0), u' '));
    setText(QStringLiteral("txt_id"), top("stag_id") + u'/' + top("mod_id"));
    setText(QStringLiteral("cmb_onda"), vb::trim(top("tip_car.Cod_ond")));
    reloadPapers(top("tip_car.Cod_ond"));
    setText(QStringLiteral("cmb_carta"), vb::trim(top("tip_car.cod_com_car")));
    setText(QStringLiteral("txt_indice"), QString::number(m_orders->leg_pun_ord()));

    for (int i = 1; i <= 3; ++i) {
        const int form = i - 1;
        const auto field = [&](const char *name) { return source->ord(i, QLatin1String(name)); };
        const bool used = !vb::trim(field("Ord_liv")).isEmpty();
        setListIndex(at("txt_liv", form), m_levelData.indexOf(int(vb::val(field("Ord_liv")))));
        int cut = -1;
        if (used)
            cut = int(vb::val(field("cut_to_mark"))) == 0 ? 0 : int(m_cutData.indexOf(int(vb::val(field("cut_to_mark")))));
        setListIndex(at("cmb_cut_to_mark", form), cut);

        const auto show = [&](const char *control, const QString &value) { setText(at(control, form), used ? value : QString()); };
        setText(at("txt_lun_for", form), !used ? QString() : abi_mis_ang ? con_met_to_fet(vb::trim(field("met"))) : field("met"));
        show("txt_cod_com", field("com.cod_com_ord"));
        show("txt_cod_cli", field("com.cod_cli"));
        show("txt_nom_cli", field("nom_cli"));
        if (abi_mis_ang) {
            show("txt_sca_lar", con_dmm_to_inc(vb::trim(field("sca_lar"))));
            show("txt_sca_lun", con_dmm_to_inc(vb::trim(field("sca_lun"))));
        } else {
            show("txt_sca_lar", QString::number(vb::intDiv(vb::val(field("sca_lar")), TODECMM)));
            show("txt_sca_lun", QString::number(vb::intDiv(vb::val(field("sca_lun")), TODECMM)));
            // In VB the pattern fields are only filled in this millimetre branch.
            show("cmb_pat_cod", field("pat_cod"));
            show("txt_pos_lat_pat", vb::trim(field("pos_pattern")));
            show("txt_pat_con", vb::trim(field("pat_con")));
            show("txt_fin_pri", vb::trim(field("fin_pri")));
            show("txt_fin_dop", vb::trim(field("fin_dop")));
        }
        show("txt_resa", field("Sca_per_tag"));
        show("txt_num_tot_fog", field("sca_tot_num"));
        if (!used) {
            setText(at("cmb_pat_cod", form), QString());
            setText(at("txt_pos_lat_pat", form), QString());
            setText(at("txt_pat_con", form), QString());
            setText(at("txt_fin_pri", form), QString());
            setText(at("txt_fin_dop", form), QString());
        }
    }
}

// get_new_dat
void InsOrdModel::getNewDat()
{
    const int machineWidth = par_mac.car_tec.for_mac == 0 ? 18000 : par_mac.car_tec.for_mac == 1 ? 25000 : 28000;
    const auto clip = [](qint32 value, qint32 maximum, qint32 minimum, int width) {
        if (value > maximum)
            return QString(width, u'9');
        if (value < minimum)
            return QString(width, u'0');
        return QString::number(value);
    };
    m_rec.setText(QStringLiteral("alt_bob"), abi_mis_ang
        ? clip(incToDmm(text(QStringLiteral("txt_alt_bob"))), machineWidth, qint32(par_mac.car_tec.min_for_mac.val()) * TODECMM, 5)
        : vb::cStr(vb::val(text(QStringLiteral("txt_alt_bob"))) * TODECMM));
    m_rec.setText(QStringLiteral("cod_ord_abb"), vb::trim(text(QStringLiteral("txt_cod_abb"))));
    m_rec.setText(QStringLiteral("tip_car.cod_com_car"), vb::trim(text(QStringLiteral("cmb_carta"))));
    m_rec.setText(QStringLiteral("tip_car.Cod_ond"), vb::trim(text(QStringLiteral("cmb_onda"))));
    m_rec.setText(QStringLiteral("mod_id"), QStringLiteral("00"));

    double total = 0;
    for (int i = 0; i < 3; ++i) {
        const auto setOrd = [&](const char *name, const QString &value) {
            m_rec.setText(QStringLiteral("ord(%1).%2").arg(i + 1).arg(QLatin1String(name)), value);
        };
        const QString lengthText = text(at("txt_sca_lun", i));
        double meters = 0;
        const double outs = vb::val(text(at("txt_resa", i)));
        if (outs > 0) {
            const double length = abi_mis_ang ? double(vb::cLng(vb::val(lengthText) * INCHMM)) : vb::val(lengthText);
            meters = double(vb::cLng((vb::val(text(at("txt_num_tot_fog", i))) / outs) * length)) / 1000;
        }
        if (meters != std::floor(meters))
            meters = std::floor(meters) + 1;
        setOrd("met", vb::cStr(meters));
        total = qMax(total, meters);
        setOrd("com.cod_com_ord", vb::trim(text(at("txt_cod_com", i))));
        setOrd("com.cod_cli", vb::trim(text(at("txt_cod_cli", i))));
        setOrd("nom_cli", vb::trim(text(at("txt_nom_cli", i))));
        if (abi_mis_ang) {
            const qint32 width = incToDmm(text(at("txt_sca_lar", i)));
            setOrd("sca_lar", width > machineWidth ? QString(5, u'9') : QString::number(width));
            setOrd("sca_lun", clip(incToDmm(lengthText), qint32(par_mac.car_tec.max_lun_tag.val()) * TODECMM,
                                   qint32(par_mac.car_tec.min_lun_tag.val()) * TODECMM, 5));
        } else {
            setOrd("sca_lar", vb::cStr(vb::val(text(at("txt_sca_lar", i))) * TODECMM));
            setOrd("sca_lun", vb::cStr(vb::val(lengthText) * TODECMM));
        }
        setOrd("Sca_per_tag", vb::trim(text(at("txt_resa", i))));
        setOrd("sca_tot_num", vb::trim(text(at("txt_num_tot_fog", i))));
        const int cut = listIndex(at("cmb_cut_to_mark", i));
        setOrd("cut_to_mark", cut == -1 ? QStringLiteral(" ") : QString::number(cut));
        const int level = listIndex(at("txt_liv", i));
        setOrd("Ord_liv", level == -1 ? QStringLiteral(" ") : QString::number(m_levelData.value(level)));

        if (cut == CUT_PAT) {
            const QString code = text(at("cmb_pat_cod", i));
            setOrd("pat_cod", code);
            QSqlQuery pattern(m_db);
            pattern.prepare(QStringLiteral("SELECT * FROM PATTERN WHERE upper(CODICE) = ?"));
            pattern.addBindValue(code.toUpper());
            if (pattern.exec() && pattern.next()) {
                setOrd("fst_FTC", pattern.value(QStringLiteral("PRI_LET_FC")).toString());
                for (int l = 1; l <= 10; ++l)
                    m_rec.setText(QStringLiteral("ord(%1).L%2").arg(i + 1).arg(l), pattern.value(QStringLiteral("L%1").arg(l)).toString());
                setOrd("Ofs_Cut", pattern.value(QStringLiteral("OFS_CUT")).toString());
            }
            setOrd("pos_pattern", vb::trim(text(at("txt_pos_lat_pat", i))));
            setOrd("pat_con", vb::trim(text(at("txt_pat_con", i))));
            setOrd("fin_pri", vb::trim(text(at("txt_fin_pri", i))));
            setOrd("fin_dop", vb::trim(text(at("txt_fin_dop", i))));
        }
    }
    m_rec.setText(QStringLiteral("met_tot_ord"), vb::cStr(total));
}

// The Select Case of val_ins_ord. Indexed cases compare with the control at ult_ind, so a field whose
// GotFocus passed another index (txt_cod_cli passes 0) is simply not validated.
bool InsOrdModel::validateLast()
{
    const QString name = m_ultCon;
    const int form = m_ultInd + 1;
    const bool sameIndex = m_ultConIndex == m_ultInd;
    const auto refuse = [&](const QString &message) {
        messaggio(message, true);
        focus(m_ultConIndex >= 0 ? QStringLiteral("%1(%2)").arg(name).arg(m_ultConIndex) : name);
        return false;
    };
    const auto key = [](const char *text) { return msg(QLatin1String(text)); };
    const auto limits = [&](const vb::FixStr &low, const QString &high) {
        return key("val_min") + u' ' + vb::trim(low) + QStringLiteral("  ") + key("val_max") + u' ' + high;
    };

    if (name == QLatin1String("txt_cod_abb")) {
        if (err_lun_Cod_ord_abb(m_rec))
            return refuse(key("ins_cod_ord"));
    } else if (name == QLatin1String("cmb_onda")) {
        if (err_ass_ond(m_rec) || vb::trim(m_rec.text(QStringLiteral("tip_car.Cod_ond"))).isEmpty())
            return refuse(key("ins_onda"));
    } else if (name == QLatin1String("cmb_carta")) {
        if (err_ass_car(m_rec) || vb::trim(m_rec.text(QStringLiteral("tip_car.cod_com_car"))).isEmpty())
            return refuse(key("ins_carta"));
    } else if (name == QLatin1String("txt_alt_bob")) {
        const int machineWidth = par_mac.car_tec.for_mac == 0 ? 1800 : par_mac.car_tec.for_mac == 1 ? 2500 : 2800;
        if (err_alt_bob(m_rec, machineWidth))
            return refuse(limits(par_mac.car_tec.min_for_mac, QStringLiteral(" %1").arg(machineWidth)));
    } else if (!sameIndex) {
        return true;
    } else if (name == QLatin1String("txt_liv")) {
        if (err_ass_liv(m_rec, form))
            return refuse(key("ins_all_dat"));
        if (err_abi_liv(m_rec, form))
            return refuse(key("liv_non_abi"));
    } else if (name == QLatin1String("txt_cod_com")) {
        if (err_cod_com(text(at("txt_cod_com", m_ultInd))))
            return refuse(key("car_non_per"));
    } else if (name == QLatin1String("txt_sca_lar")) {
        if (err_ass_sca_lar(m_rec, form))
            return refuse(key("ins_lar"));
        if (err_max_sca_lar(m_rec))
            return refuse(key("mis_ord_ecc"));
    } else if (name == QLatin1String("txt_sca_lun")) {
        if (err_ass_lun(m_rec, form))
            return refuse(key("ins_lun_for"));
        if (err_sca_lun(m_rec, form))
            return refuse(limits(par_mac.car_tec.min_lun_tag, vb::trim(par_mac.car_tec.max_lun_tag)));
    } else if (name == QLatin1String("txt_resa")) {
        if (err_num_sca(m_rec, form))
            return refuse(key("val_min") + QStringLiteral(" 1 "));
        if (err_som_sca(m_rec))
            return refuse(key("mis_ord_ecc"));
    } else if (name == QLatin1String("cmb_cut_to_mark")) {
        const int error = err_cut_to_mark(m_rec, form, false);
        if (error == 1)
            return refuse(key("ins_cut_to_mark"));
        if (error == 2)
            return refuse(key("cut_to_mark_non_pre"));
        if (error == 3)
            return refuse(key("cut_to_mark_liv_abb"));
    } else if (name == QLatin1String("txt_pos_lat_pat")) {
        if (err_pos_pat(m_rec))
            return refuse(key("err_pos_pat"));
    } else if (name == QLatin1String("txt_pat_con")) {
        if (err_pat_con(m_rec, form, false))
            return refuse(key("err_pat_con"));
    } else if (name == QLatin1String("txt_fin_pri")) {
        if (err_fin_pri(m_rec, form, false))
            return refuse(key("err_fin_pri"));
    } else if (name == QLatin1String("txt_fin_dop")) {
        if (err_fin_dop(m_rec, form, false))
            return refuse(key("err_fin_dop"));
    }
    return true;
}

// val_ins_ord
bool InsOrdModel::valInsOrd(const QString &control, int index, int passedIndex)
{
    if (m_ultCon.isEmpty()) {
        m_ultCon = control;
        m_ultConIndex = index;
        m_ultInd = -1;
    }
    if (m_ultCon == control && m_ultConIndex == index)
        return true;

    getNewDat();
    // Going back does not validate, except when leaving the first field.
    if (m_flgRev && m_ultCon != QLatin1String("txt_cod_abb")) {
        m_flgRev = false;
        m_ultCon = control;
        m_ultConIndex = index;
        m_ultInd = passedIndex;
        return false;
    }
    if (!validateLast())
        return false;
    if (m_ultCon == QLatin1String("txt_cod_abb") && m_flgRev) {
        focus(QStringLiteral("txt_cod_abb"));
        m_flgRev = false;
        return false;
    }
    m_ultCon = control;
    m_ultConIndex = index;
    m_ultInd = passedIndex;
    return true;
}

void InsOrdModel::gotFocus(const QString &key, const QString &name, int index)
{
    if (m_tipOp != OLI_NEW && m_tipOp != OLI_MOD)
        return;
    const bool wasGoingBack = m_flgRev;
    const QString previous = m_ultConIndex >= 0 ? QStringLiteral("%1(%2)").arg(m_ultCon).arg(m_ultConIndex) : m_ultCon;
    // txt_nom_cli has no check of its own and txt_cod_cli and the header fields always pass 0.
    bool result = true;
    if (name != QLatin1String("txt_nom_cli")) {
        const int passed = name == QLatin1String("txt_cod_cli") || index < 0 ? 0 : index;
        result = valInsOrd(name, index, passed);
    }
    if (!wasGoingBack && !previous.isEmpty() && name != QLatin1String("txt_nom_cli"))
        set(previous, QStringLiteral("foreColor"), StaBarModel::vbColor(result ? StaBarModel::vbBlack : StaBarModel::vbRed));
    if (name.startsWith(QLatin1String("txt_")) && name != QLatin1String("txt_liv")) {
        set(key, QStringLiteral("backColor"), StaBarModel::vbColor(StaBarModel::vbBlack));
        set(key, QStringLiteral("foreColor"), StaBarModel::vbColor(StaBarModel::vbWhite));
    }
    if (name == QLatin1String("cmb_onda") && result && vb::trim(text(key)).isEmpty()) {
        // No flute yet: the first of the table.
        QSqlQuery first(m_db);
        if (first.exec(QStringLiteral("SELECT DISTINCT ONDA FROM ONDE")) && first.next())
            comboChanged(key, first.value(0).toString());
    }
    if (name == QLatin1String("cmb_pat_cod")) {
        // A code that is not in PATTERN falls back to the first one.
        QSqlQuery same(m_db);
        same.prepare(QStringLiteral("SELECT 1 FROM PATTERN WHERE upper(CODICE) = ?"));
        same.addBindValue(text(key).toUpper());
        if (!(same.exec() && same.next())) {
            reloadPatterns();
            QSqlQuery first(m_db);
            const bool any = first.exec(QStringLiteral("SELECT CODICE FROM PATTERN ORDER BY CODICE")) && first.next();
            comboChanged(key, any ? first.value(0).toString() : QString());
        }
    }
}

void InsOrdModel::comboChanged(const QString &key, const QString &value)
{
    setText(key, value);
    if (key == QLatin1String("cmb_onda")) {
        if (!vb::trim(value).isEmpty())
            reloadPapers(value);
    } else if (key.startsWith(QLatin1String("cmb_pat_cod"))) {
        // cmb_pat_cod_Change: the contrast of the new code.
        const int index = key.mid(key.indexOf(u'(') + 1, 1).toInt();
        QSqlQuery contrast(m_db);
        contrast.prepare(QStringLiteral("SELECT CONTRASTO FROM PATTERN WHERE upper(CODICE) = ?"));
        contrast.addBindValue(value.toUpper());
        if (contrast.exec() && contrast.next() && m_oldPattern.value(index) != value)
            setText(at("txt_pat_con", index), contrast.value(0).toString());
        m_oldPattern.insert(index, value);
    }
}

// abi_con_cut_pat
void InsOrdModel::setCutStops(int format, bool enabled)
{
    m_cutStops.insert(format, enabled);
}

void InsOrdModel::applyCutPatternStops()
{
    for (int i = 0; i < 3; ++i)
        setCutStops(i, par_mac.opt.cut_pat(i + 1) == QLatin1String(STR_TRUE) || par_mac.opt.cut_pat_auto(i + 1) == QLatin1String(STR_TRUE));
}

// {TAB} over the form's TabIndex order: the header, then each format; pattern fields only when enabled.
void InsOrdModel::tab(const QString &from, int step)
{
    QStringList stops{QStringLiteral("txt_cod_abb"), QStringLiteral("cmb_onda"), QStringLiteral("cmb_carta"), QStringLiteral("txt_alt_bob")};
    for (int i = 0; i < 3; ++i) {
        stops << at("txt_liv", i) << at("txt_cod_com", i) << at("txt_cod_cli", i) << at("txt_nom_cli", i) << at("txt_sca_lar", i)
              << at("txt_sca_lun", i) << at("txt_resa", i) << at("txt_num_tot_fog", i) << at("cmb_cut_to_mark", i);
        if (m_cutStops.value(i)) {
            stops << at("cmb_pat_cod", i);
            if (par_mac.opt.cut_pat(i + 1).val() != 1)
                stops << at("txt_pos_lat_pat", i);
            stops << at("txt_pat_con", i) << at("txt_fin_pri", i) << at("txt_fin_dop", i);
        }
    }
    const int position = int(stops.indexOf(from));
    const int count = int(stops.size());
    focus(stops.at((qMax(position, 0) + step + count) % count));
}

void InsOrdModel::controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args)
{
    const QString key = index >= 0 ? QStringLiteral("%1(%2)").arg(control).arg(index) : control;
    if (!m_controls.contains(key))
        return;
    if (event == QLatin1String("Change")) {
        const QString value = args.value(QStringLiteral("text")).toString();
        if (control.startsWith(QLatin1String("cmb_")) && control != QLatin1String("cmb_cut_to_mark"))
            comboChanged(key, value);
        else
            setText(key, value);
        set(key, QStringLiteral("foreColor"), StaBarModel::vbColor(StaBarModel::vbBlack));
    } else if (event == QLatin1String("Click")) {
        const int selected = args.value(QStringLiteral("index")).toInt();
        if (control == QLatin1String("txt_liv")) {
            m_listIndex.insert(key, selected);
        } else if (control == QLatin1String("cmb_cut_to_mark")) {
            m_listIndex.insert(key, selected);
            setCutStops(index, m_cutData.value(selected) == PRE_PATTERN);
        } else {
            return;
        }
    } else if (event == QLatin1String("GotFocus")) {
        gotFocus(key, control, index);
    } else if (event == QLatin1String("LostFocus")) {
        if (control.startsWith(QLatin1String("txt_")) && control != QLatin1String("txt_liv")) {
            set(key, QStringLiteral("backColor"), StaBarModel::vbColor(StaBarModel::vbWhite));
            set(key, QStringLiteral("foreColor"), StaBarModel::vbColor(StaBarModel::vbBlack));
        }
    } else if (event == QLatin1String("KeyDown")) {
        if (m_tipOp != OLI_NEW && m_tipOp != OLI_MOD)
            return;
        const int pressed = args.value(QStringLiteral("key")).toInt();
        if (pressed == Qt::Key_Return || pressed == Qt::Key_Enter || pressed == Qt::Key_Down || pressed == Qt::Key_Tab) {
            m_flgRev = false;
            tab(key, 1);
        } else if (pressed == Qt::Key_Up || pressed == Qt::Key_Backtab) {
            if (key == QLatin1String("txt_cod_abb")) {
                focus(key);
            } else {
                m_flgRev = true;
                tab(key, -1);
            }
        } else {
            return;
        }
    } else {
        return;
    }
    commit();
}

bool InsOrdModel::machineFree() const
{
    return stag::sta_tag && (stag::sta_tag->bit(ClasseStato::MAC_IN_MANUALE, 1) || !stag::sta_tag->spdeOK());
}

void InsOrdModel::newOrder()
{
    if (m_orders->Count() >= MAX_NUM_ORD) {
        messaggio(msg(QStringLiteral("rag_num_max")), true, false);
        return;
    }
    if (!machineFree()) {
        messaggio(msg(QStringLiteral("imp_ese_ope")) + QStringLiteral(". ") + msg(QStringLiteral("mac_in_auto")), true, false);
        return;
    }
    m_ultCon.clear();
    m_ultConIndex = -1;
    m_ultInd = -1;
    enableFrames(true);
    m_tipOp = OLI_NEW;
    clearCell();
    m_rec.setOrdine(QByteArray(kOrderRecordSize, ' '));
    setText(QStringLiteral("txt_indice"), QString::number(m_orders->Count() + 1));
    focus(QStringLiteral("txt_cod_abb"));
}

// createNullOrderForm
void InsOrdModel::nullOrder()
{
    if (m_orders->Count() >= MAX_NUM_ORD) {
        messaggio(msg(QStringLiteral("rag_num_max")), true, false);
        return;
    }
    if (!machineFree()) {
        messaggio(msg(QStringLiteral("imp_ese_ope")) + QStringLiteral(". ") + msg(QStringLiteral("mac_in_auto")), true, false);
        return;
    }
    m_tipOp = OLI_NEW_NULL;
    m_ultCon.clear();
    m_ultConIndex = -1;
    m_ultInd = -1;
    const QString flute = vb::trim(text(QStringLiteral("cmb_onda")));
    clearCell();
    if (flute.isEmpty()) {
        QSqlQuery first(m_db);
        if (first.exec(QStringLiteral("SELECT DISTINCT ONDA FROM ONDE")) && first.next())
            comboChanged(QStringLiteral("cmb_onda"), first.value(0).toString());
    } else {
        comboChanged(QStringLiteral("cmb_onda"), flute);
    }
    setText(QStringLiteral("txt_cod_abb"), QStringLiteral("1"));
    setText(QStringLiteral("txt_alt_bob"), QStringLiteral("1800"));
    setListIndex(at("txt_liv", 0), 0);
    setText(at("txt_cod_com", 0), QStringLiteral("NULL ORDER"));
    setText(at("txt_nom_cli", 0), QStringLiteral("NULL ORDER"));
    setText(at("txt_sca_lar", 0), QStringLiteral("0"));
    setText(at("txt_sca_lun", 0), QStringLiteral("0"));
    setText(at("txt_resa", 0), QStringLiteral("0"));
    setText(at("txt_num_tot_fog", 0), QStringLiteral("0"));
    setText(QStringLiteral("txt_indice"), QString::number(m_orders->Count() + 1));
    m_rec.setOrdine(QByteArray(kOrderRecordSize, ' '));
    enableFrames(false);
}

// eve_key_F4. For a normal order the data are those of the last get_new_dat, made on entering the last
// field: VB does not read the form again here, so a value typed in the field being left is not saved.
void InsOrdModel::save()
{
    m_ultCon.clear();
    if (m_tipOp == OLI_NUL)
        return;
    messaggio(msg(QStringLiteral("ope_in_cor")), false, false);
    if (m_tipOp == OLI_NEW_NULL)
        getNewDat();
    const int result = val_ord(m_rec, m_db, true);
    if (result != RET_OK) {
        messaggio(msg(msg_err_val_ord(result)), true);
        focus(QStringLiteral("txt_cod_abb"));
        return;
    }
    qint32 meters = 0;
    for (int i = 1; i <= 3; ++i) {
        const QString level = m_rec.ord(i, QStringLiteral("Ord_liv"));
        if (!vb::trim(level).isEmpty() && vb::val(level) != 0)
            meters = qMax(meters, vb::cLng(vb::val(m_rec.ord(i, QStringLiteral("met")))));
    }
    m_rec.setText(QStringLiteral("met_tot_ord"), QString::number(meters));

    auto order = std::make_shared<ClasseOrdine>();
    order->setOrdine(m_rec.ordine());
    const bool isNew = m_tipOp == OLI_NEW || m_tipOp == OLI_NEW_NULL;
    const int answer = isNew ? m_orders->add(order) : m_orders->Modifica(order, m_orders->leg_pun_ord());
    if (answer == RET_BAD && isNew)
        messaggio(msg(QStringLiteral("rag_num_max")), true, false);
    if (answer == RET_OK && m_orders->Count() > 0)
        messaggio(msg(QStringLiteral("ope_eff")), false, false);
    if (answer == RET_OK && isNew)
        m_orders->scr_pun_ord(m_orders->Count());
    if (m_orders->Count() > 0)
        riempiCelle(m_orders->leg_pun_ord());
    m_tipOp = OLI_NUL;
    enableFrames(false);
    focus(QStringLiteral("txt_idle"));
}

// eve_key_esc
void InsOrdModel::cancel()
{
    m_tipOp = OLI_NUL;
    clearCell();
    if (m_orders->Count() > 0)
        riempiCelle(m_orders->leg_pun_ord());
    enableFrames(false);
    focus(QStringLiteral("txt_idle"));
}

bool InsOrdModel::functionKey(const QString &key, int shift)
{
    if (shift != 0)
        return false;
    const bool idle = m_tipOp == OLI_NUL;
    if (key == QLatin1String("ESC")) {
        cancel();
    } else if (key == QLatin1String("F1")) {
        if (idle)
            newOrder();
    } else if (key == QLatin1String("F2")) {
        if (idle && m_orders->Count() > 0) {
            if (machineFree()) {
                m_ultCon.clear();
                m_ultConIndex = -1;
                m_ultInd = -1;
                enableFrames(true);
                m_tipOp = OLI_MOD;
                focus(QStringLiteral("txt_cod_abb"));
            } else {
                messaggio(msg(QStringLiteral("imp_ese_ope")) + QStringLiteral(". ") + msg(QStringLiteral("mac_in_auto")), true, false);
            }
        }
    } else if (key == QLatin1String("F3")) {
        if (idle)
            nullOrder();
    } else if (key == QLatin1String("F4")) {
        save();
    } else if (key == QLatin1String("F5") || key == QLatin1String("F6")) {
        if (idle) {
            if (key == QLatin1String("F5"))
                m_orders->pun_ord_mov_prev();
            else
                m_orders->pun_ord_mov_next();
            if (m_orders->leg_pun_ord() > 0)
                riempiCelle(m_orders->leg_pun_ord());
        }
    } else if (key == QLatin1String("F7")) {
        if (idle && m_orders->Count() > 0) {
            m_orders->scr_pun_ord(1);
            riempiCelle(1);
        }
    } else if (key == QLatin1String("F8") || key == QLatin1String("F9") || key == QLatin1String("F10")) {
        return !idle;
    } else {
        return false;
    }
    commit();
    return true;
}

// Form_Activate
void InsOrdModel::activate(const QString &previousForm)
{
    enableFrames(false);
    m_tipOp = OLI_NUL;
    if (previousForm.compare(QLatin1String("Pag_lis_ord"), Qt::CaseInsensitive) != 0 && m_orders->Count() > 0)
        m_orders->scr_pun_ord(1);
    reloadFlutes();
    reloadPatterns();
    applyCutPatternStops();
    if (m_orders->Count() > 0) {
        riempiCelle(qMax(m_orders->leg_pun_ord(), 1));
    } else {
        clearCell();
    }
    commit();
}

// eve_ord_mac_Refresh
void InsOrdModel::refresh(bool shift)
{
    if (!shift)
        return;
    const int count = m_orders->Count();
    const int shown = int(vb::val(text(QStringLiteral("txt_indice"))));
    if (m_tipOp == OLI_NUL) {
        if (count > 0 && shown > 1) {
            setText(QStringLiteral("txt_indice"), QString::number(shown - 1));
            m_orders->scr_pun_ord(shown - 1);
        } else if (count > 0 && shown == 1) {
            riempiCelle(1);
        }
        if (count == 0)
            clearCell();
        commit();
        return;
    }
    messaggio(msg(QStringLiteral("ope_ann")), true, false);
    if (m_tipOp == OLI_MOD) {
        if (shown > 1) {
            m_orders->scr_pun_ord(shown - 1);
            riempiCelle(shown - 1);
        } else if (count > 1) {
            m_orders->scr_pun_ord(1);
            riempiCelle(1);
        } else {
            clearCell();
            m_orders->scr_pun_ord(0);
        }
    } else if (count > 0) {
        m_orders->scr_pun_ord(count);
        riempiCelle(count);
    } else {
        clearCell();
        m_orders->scr_pun_ord(0);
    }
    enableFrames(false);
    m_tipOp = OLI_NUL;
    focus(QStringLiteral("txt_idle"));
    commit();
}

void InsOrdModel::machineMode(int mode)
{
    if (mode != ClasseStato::automatico || m_tipOp == OLI_NUL)
        return;
    messaggio(msg(QStringLiteral("ope_ann")), true, false);
    if (m_tipOp == OLI_MOD) {
        riempiCelle(int(vb::val(text(QStringLiteral("txt_indice")))));
    } else if (m_orders->Count() > 0) {
        m_orders->scr_pun_ord(m_orders->Count());
        riempiCelle(m_orders->Count());
    } else {
        clearCell();
        m_orders->scr_pun_ord(0);
    }
    enableFrames(false);
    m_tipOp = OLI_NUL;
    focus(QStringLiteral("txt_idle"));
    commit();
}
