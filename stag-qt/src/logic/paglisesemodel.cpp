#include "paglisesemodel.h"

#include "messages.h"
#include "parmac.h"
#include "stabarmodel.h"
#include "unimisang.h"
#include "vbcompat.h"

#include <QSet>

using namespace stag;

static const QString REPORT = QStringLiteral("lis_rep");

static QString rieNul(QString text)
{
    text.replace(QChar(0), u' ');
    return text;
}

// spde_bas.sec_ore
static QString secOre(const QString &seconds)
{
    const qint32 v = vb::cLng(vb::val(seconds));
    const qint32 s = v % 60;
    const qint32 m = ((v - s) / 60) % 60;
    const qint32 o = (v - s - m * 60) / 3600;
    return QStringLiteral("%1.%2.%3").arg(o, 2, 10, QLatin1Char('0')).arg(m, 2, 10, QLatin1Char('0')).arg(s, 2, 10, QLatin1Char('0'));
}

PagLisEseModel::PagLisEseModel(CollezioneOrdini *orders, QObject *parent)
    : OrderListModel(orders, QStringLiteral("lis_ord_ese"), parent)
{
}

void PagLisEseModel::load()
{
    m_reportVisible = false;
    for (const char *panel : {"frm_rep", "frm_rep_pro", "lis_rep"})
        set(QLatin1String(panel), QStringLiteral("visible"), false);
    // frm_rep sits right under the list's design height, frm_rep_pro under frm_rep.
    set(QStringLiteral("frm_rep"), QStringLiteral("y"), 223);
    set(QStringLiteral("frm_rep_pro"), QStringLiteral("y"), 279);
    set(REPORT, QStringLiteral("rows"), 4);
    set(REPORT, QStringLiteral("cols"), 5);
    set(REPORT, QStringLiteral("fixedRows"), 0);
    set(REPORT, QStringLiteral("fixedCols"), 0);
    set(REPORT, QStringLiteral("rowHeight"), 28);
    set(REPORT, QStringLiteral("cellPixelSize"), 20);
    set(REPORT, QStringLiteral("colWidths"), QVariantList{140, 166, 166, 166, 166});
    set(REPORT, QStringLiteral("colAlign"), QVariantList{0, 1, 1, 1, 1});
    set(REPORT, QStringLiteral("colBold"), QVariantList{true, true, true, true, true});
    const QString yellow = StaBarModel::vbColor(0x00FFFF); // COL_Yellow
    const QString report = StaBarModel::vbColor(0xC0E0FF); // COL_REPORT
    set(REPORT, QStringLiteral("rowBack"), QVariantList{yellow, report, report, report});
    const QString blue = StaBarModel::vbColor(0xFF0000);
    set(REPORT, QStringLiteral("rowFore"), QVariantList{StaBarModel::vbColor(StaBarModel::vbBlack), blue, blue, blue});
    loadGrid();
    aggiorna();
}

void PagLisEseModel::activate(const QString &previousForm)
{
    static const QSet<QString> keepPointer{QStringLiteral("tro_ord_ese"), QStringLiteral("mov_ese"), QStringLiteral("del_ord_ese")};
    if (!keepPointer.contains(previousForm.toLower()))
        m_orders->scr_pun_ord(1);
    placeWindow();
    m_ridisegna = true;
    aggiorna();
}

void PagLisEseModel::adjustWindow(int count)
{
    m_ultimo = qMin(m_ultimo, count);
    m_primo = qMin(m_primo, count);
    m_puntatore = qMin(m_puntatore, count);
    m_punLis = qMin(m_punLis, count);
}

// ges_rep
void PagLisEseModel::showPanel(int order)
{
    if (!m_reportVisible)
        return;
    const OrdinePtr o = m_orders->item(order);
    const auto report = [&](const char *name) { return o->text(QStringLiteral("rep_pro.") + QLatin1String(name)); };
    const auto trimmed = [&](const char *name) { return vb::trim(rieNul(report(name))); };

    setText(QStringLiteral("txt_ora_ini"), trimmed("ora_ini"));
    setText(QStringLiteral("txt_ora_fin"), trimmed("ora_fin"));
    setText(QStringLiteral("txt_dat_ini"), trimmed("dat_ini"));
    setText(QStringLiteral("txt_dat_fin"), trimmed("dat_fin"));
    setText(QStringLiteral("txt_vel_med"), abi_mis_ang ? con_met_to_fet(trimmed("vel_med")) : trimmed("vel_med"));
    const QString meters = vb::cStr(vb::intDiv(vb::val(report("met_tot")), 1));
    setText(QStringLiteral("txt_met_pro"), abi_mis_ang ? con_met_to_fet(meters) : meters);
    setText(QStringLiteral("txt_tem_fer"), secOre(rieNul(report("tem_fer"))));
    setText(QStringLiteral("txt_tem_mar"), secOre(rieNul(report("tem_mar"))));
    setText(QStringLiteral("txt_num_fer"), trimmed("num_fer"));

    QVariantList rows;
    rows << QVariant(QStringList{msg(QStringLiteral("rep_liv")), msg(QStringLiteral("rep_fog_buo")), msg(QStringLiteral("rep_fog_err")),
                                 msg(QStringLiteral("rep_fog_sca")), QStringLiteral("mm scarto")});
    bool filled[4] = {false, false, false, false};
    for (int i = 1; i <= 3; ++i) {
        const QString levelText = o->ord(i, QStringLiteral("Ord_liv"));
        const int level = int(vb::val(levelText));
        QStringList row(5);
        if (!vb::trim(levelText).isEmpty() && level > 0 && level <= 3 && !filled[level]) {
            filled[level] = true;
            if (level == LIV_BAS)
                row[0] = msg(QStringLiteral("liv_bas"));
            else if (level == LIV_MED)
                row[0] = msg(QStringLiteral("liv_med"));
            else if (level == LIV_ALT)
                row[0] = msg(QStringLiteral("liv_alt"));
            const auto counter = [&](const char *name) { return rieNul(o->text(QStringLiteral("rep_pro.liv(%1).%2").arg(i).arg(QLatin1String(name)))); };
            row[1] = counter("fog_buo");
            row[2] = counter("fog_err");
            row[3] = counter("fog_sca");
            row[4] = counter("mil_sca");
        }
        rows << QVariant(row);
    }
    set(REPORT, QStringLiteral("cells"), rows);
}

bool PagLisEseModel::functionKey(const QString &key, int shift)
{
    if (shift != 0)
        return false;
    if (key == QLatin1String("F2"))
        return !par_abi.abi_rep_pro;
    if (key == QLatin1String("F3") || key == QLatin1String("F5") || key == QLatin1String("F6")) {
        if (m_orders->Count() == 0)
            return true;
        prefillDialog(key);
        return false;
    }
    if (key == QLatin1String("F7")) {
        if (par_abi.abi_rep_pro && m_orders->Count() > 0) {
            m_reportVisible = !m_reportVisible;
            for (const char *panel : {"frm_rep", "frm_rep_pro", "lis_rep"})
                set(QLatin1String(panel), QStringLiteral("visible"), m_reportVisible);
            const int rows = m_reportVisible ? 12 : (m_numLivLisOrd == 2 ? 22 : 21);
            resizeList(rows);
        }
        return true;
    }
    return false;
}

QStringList PagLisEseModel::hiddenFunctionKeys() const
{
    return par_abi.abi_rep_pro ? QStringList{} : QStringList{QStringLiteral("F2"), QStringLiteral("F7")};
}
