#include "orderlistmodel.h"

#include "messages.h"
#include "parmac.h"
#include "stabarmodel.h"
#include "unimisang.h"
#include "vbcompat.h"

using namespace stag;

static constexpr quint32 ULTRA_DARK_GREY = 0x101010;
static constexpr quint32 COL_GRAY_ORD = 0xDFE0E0;
static constexpr quint32 COL_FADED_ORANGE = 0x00CAFF;

static QString color(quint32 bgr)
{
    return StaBarModel::vbColor(bgr);
}

// Rie_nul: NULs and blanks both become blanks, the length stays.
static QString rieNul(QString text)
{
    text.replace(QChar(0), u' ');
    return text;
}

static QString foreForCutMode(int cutToMark)
{
    return color(cutToMark == 1 ? StaBarModel::COL_DARK_Green : cutToMark == 2 ? COL_FADED_ORANGE : StaBarModel::vbBlack);
}

// cut_to_mark without an index: the highest mode among the formats.
static int highestCutMode(const ClasseOrdine &order)
{
    int mode = 0;
    for (int i = 1; i <= 3; ++i)
        mode = qMax(mode, int(vb::val(order.ord(i, QStringLiteral("cut_to_mark")))));
    return mode;
}

OrderListModel::OrderListModel(CollezioneOrdini *orders, const QString &grid, QObject *parent)
    : FormModel(parent)
    , m_orders(orders)
    , m_grid(grid)
{
    connect(m_orders, &CollezioneOrdini::Refresh, this, [this] {
        m_ridisegna = true;
        aggiorna();
    });
}

// The part of Form_Load the two lists share (ini_ges_gri_ord in pag_lis_ese).
void OrderListModel::loadGrid()
{
    const int levels = int(par_mac.car_tec.num_liv_tag.val());
    m_numLivLisOrd = levels == 3 ? 3 : levels + 1;
    const int rows = m_numLivLisOrd == 2 ? 22 : 21;
    m_numLisOrd = vb::cLng(double(rows) / m_numLivLisOrd);
    m_punLis = 1;
    m_puntatore = 1;
    m_primo = 1;
    m_ultimo = m_orders->Count() > m_numLisOrd ? m_numLisOrd : m_orders->Count();
    m_ridisegna = true;

    set(m_grid, QStringLiteral("height"), ROW_HEIGHT * (rows + 1));
    set(m_grid, QStringLiteral("rowHeight"), ROW_HEIGHT);
    set(m_grid, QStringLiteral("fixedRows"), 0);
    set(m_grid, QStringLiteral("fixedCols"), 0);
    set(m_grid, QStringLiteral("colWidths"), QVariantList{25, 112, 38, 46, 108, 193, 89, 50, 50, 34, 53});
    // flexAlign: 0 left, 1 centre, 2 right
    set(m_grid, QStringLiteral("colAlign"), QVariantList{2, 1, 1, 0, 0, 0, 0, 1, 1, 1, 2});
    set(m_grid, QStringLiteral("colBold"), QVariantList{true, true, false, false, true, false, false, false, true, false, false});
    set(m_grid, QStringLiteral("colMerge"), QVariantList{true, true, true, false, false, false, false, false, false, false, false});
}

// The page size changes with the details or report panel; the window is placed again around the pointer.
void OrderListModel::resizeList(int rows)
{
    m_numLisOrd = vb::cLng(double(rows) / m_numLivLisOrd);
    set(m_grid, QStringLiteral("height"), ROW_HEIGHT * (rows + 1));
    placeWindow();
    m_ridisegna = true;
    aggiorna();
}

void OrderListModel::prefillDialog(const QString &key)
{
    if (OrderDialogModel *dialog = m_dialogs.value(key))
        dialog->prefill(m_punLis);
}

void OrderListModel::aggiorna()
{
    const int direction = m_keyUp ? 1 : m_keyDown ? -1 : 0;
    adjustWindow(m_orders->Count());
    if (m_ridisegna) {
        rebuildRows();
        paintAllRows();
    } else {
        paintMovedRows(direction);
    }
    publishGrid();
    if (m_punLis > 0 && m_orders->Count() > 0)
        showPanel(m_punLis);
    commit();
}

// The window around leg_pun_ord, shared by Form_Activate and eve_key_F7.
void OrderListModel::placeWindow()
{
    const int count = m_orders->Count();
    m_punLis = m_orders->leg_pun_ord();
    if (m_punLis + (m_numLisOrd - 1) <= count) {
        m_primo = m_punLis;
        m_ultimo = m_primo + (m_numLisOrd - 1);
        m_puntatore = 1;
    } else {
        m_primo = m_punLis - (m_numLisOrd - 1) >= 1 ? m_punLis - (m_numLisOrd - 1) : 1;
        m_ultimo = m_primo + (m_numLisOrd - 1) <= count ? m_primo + (m_numLisOrd - 1) : count;
        m_puntatore = m_primo > 1 ? m_numLisOrd : m_punLis;
    }
}

QStringList OrderListModel::orderRow(int order, int format) const
{
    const OrdinePtr o = m_orders->item(order);
    const auto top = [&](const char *name) { return rieNul(o->text(QLatin1String(name))); };
    QString level, scatole, codCom, resa, nomCli, lun, lar, codCli;
    if (!vb::trim(o->ord(format, QStringLiteral("Ord_liv"))).isEmpty()) {
        const auto field = [&](const char *name) { return o->ord(format, QLatin1String(name)); };
        const auto size = [&](const char *name) {
            return rieNul(abi_mis_ang ? con_dmm_to_inc(vb::trim(field(name))) : QString::number(vb::intDiv(vb::val(field(name)), TODECMM)));
        };
        scatole = vb::trim(field("sca_tot_num"));
        codCom = rieNul(field("com.cod_com_ord"));
        resa = rieNul(field("Sca_per_tag"));
        nomCli = rieNul(field("nom_cli"));
        lun = size("sca_lun");
        lar = size("sca_lar");
        codCli = rieNul(field("com.cod_cli"));
        const int number = int(vb::val(field("Ord_liv")));
        if (number == LIV_BAS)
            level = msg(QStringLiteral("liv_bas"));
        else if (number == LIV_MED)
            level = msg(QStringLiteral("liv_med"));
        else if (number == LIV_ALT)
            level = msg(QStringLiteral("liv_alt"));
    }
    return {QString::number(order), top("cod_ord_abb"), top("tip_car.Cod_ond"), level, codCom, nomCli, codCli, lar, lun, resa, scatole};
}

void OrderListModel::rebuildRows()
{
    m_cells.clear();
    m_cells << QVariant(QStringList{QString(), msg(QStringLiteral("lis_cod_abb")), msg(QStringLiteral("lis_ond")), msg(QStringLiteral("lis_liv")),
                                    msg(QStringLiteral("lis_com")), msg(QStringLiteral("lis_nom_cli")), msg(QStringLiteral("lis_cod_cli")),
                                    msg(QStringLiteral("lis_lar")), msg(QStringLiteral("lis_lun")), msg(QStringLiteral("lis_res")), msg(QStringLiteral("lis_sca"))});
    for (int order = m_primo; order <= m_ultimo; ++order) {
        if (order <= 0)
            continue;
        for (int format = 1; format <= m_numLivLisOrd; ++format)
            m_cells << QVariant(orderRow(order, format));
    }
}

void OrderListModel::paintAllRows()
{
    m_rowBack = {color(ULTRA_DARK_GREY)};
    m_rowFore = {color(StaBarModel::vbWhite)};
    int order = m_primo - 1;
    for (int row = 1; row < m_cells.size(); ++row) {
        const int format = row % m_numLivLisOrd == 0 ? m_numLivLisOrd : row % m_numLivLisOrd;
        if (format == 1)
            ++order;
        const int mode = int(vb::val(m_orders->item(order)->ord(format, QStringLiteral("cut_to_mark"))));
        QString fore = mode > -1 && mode < 3 ? foreForCutMode(mode) : color(StaBarModel::vbBlack);
        const int band = row % (m_numLivLisOrd * 2);
        QString back = band >= 1 && band <= m_numLivLisOrd ? color(COL_GRAY_ORD) : color(StaBarModel::vbWhite);
        if (row <= m_puntatore * m_numLivLisOrd && row >= m_puntatore * m_numLivLisOrd - (m_numLivLisOrd - 1)) {
            back = color(StaBarModel::COL_DARK_Green);
            fore = color(StaBarModel::vbWhite);
        }
        m_rowBack << back;
        m_rowFore << fore;
    }
}

// Without a redraw VB repainted only the selected order and the one it left.
void OrderListModel::paintMovedRows(int direction)
{
    for (int row = m_puntatore * m_numLivLisOrd; row >= m_puntatore * m_numLivLisOrd - (m_numLivLisOrd - 1); --row) {
        if (row > 0 && row < m_rowBack.size()) {
            m_rowBack[row] = color(StaBarModel::COL_DARK_Green);
            m_rowFore[row] = color(StaBarModel::vbWhite);
        }
    }
    const int moved = (m_puntatore + direction) * m_numLivLisOrd;
    const int band = moved % (m_numLivLisOrd * 2);
    const QString back = band >= 1 && band <= m_numLivLisOrd ? color(COL_GRAY_ORD) : color(StaBarModel::vbWhite);
    const QString fore = foreForCutMode(highestCutMode(*m_orders->item(m_punLis + direction)));
    for (int row = moved; row >= moved - (m_numLivLisOrd - 1); --row) {
        if (row > 0 && row < m_rowBack.size()) {
            m_rowBack[row] = back;
            m_rowFore[row] = fore;
        }
    }
}

void OrderListModel::publishGrid()
{
    set(m_grid, QStringLiteral("rows"), m_cells.size());
    set(m_grid, QStringLiteral("cols"), 11);
    set(m_grid, QStringLiteral("cells"), m_cells);
    set(m_grid, QStringLiteral("rowBack"), m_rowBack);
    set(m_grid, QStringLiteral("rowFore"), m_rowFore);
}

// Form_KeyUp
void OrderListModel::keyUp(int key)
{
    const int count = m_orders->Count();
    m_ridisegna = false;
    m_keyUp = false;
    m_keyDown = false;

    if (key == Qt::Key_Down) {
        m_keyDown = true;
        const int previous = m_punLis;
        m_punLis = count > m_punLis ? m_punLis + 1 : count;
        if (m_puntatore < m_numLisOrd && previous != m_punLis)
            ++m_puntatore;
        if (m_punLis == m_ultimo + 1) {
            ++m_primo;
            ++m_ultimo;
            m_ridisegna = true;
        }
    } else if (key == Qt::Key_PageDown) {
        m_ridisegna = true;
        if (count > m_punLis + m_numLisOrd) {
            m_punLis += m_numLisOrd;
            m_primo += m_numLisOrd;
            m_ultimo = count > m_ultimo + m_numLisOrd ? m_ultimo + m_numLisOrd : count;
        } else if (m_ultimo < count) {
            const int remaining = count - m_ultimo;
            m_punLis = count;
            m_primo += m_numLisOrd;
            m_ultimo = m_punLis;
            m_puntatore = remaining;
        } else {
            m_punLis = m_ultimo;
            m_puntatore = m_ultimo - m_primo + 1;
        }
    } else if (key == Qt::Key_PageUp) {
        m_ridisegna = true;
        if (m_punLis - m_numLisOrd >= 1) {
            if (m_ultimo - m_primo == m_numLisOrd - 1) {
                m_punLis -= m_numLisOrd;
                m_primo = m_primo - m_numLisOrd > 1 ? m_primo - m_numLisOrd : 1;
                m_ultimo = m_primo + (m_numLisOrd - 1);
            } else {
                m_ultimo = m_primo - 1;
                m_primo = m_ultimo - (m_numLisOrd - 1);
                m_punLis -= m_numLisOrd;
            }
        } else {
            m_punLis = 1;
            m_puntatore = 1;
            m_primo = 1;
            m_ultimo = count > m_ultimo ? m_numLisOrd : count;
        }
    } else if (key == Qt::Key_Up) {
        m_keyUp = true;
        if (m_punLis > 1)
            --m_punLis;
        if (m_puntatore > 1)
            --m_puntatore;
        if (m_punLis == m_primo - 1) {
            if (m_ultimo - m_primo != m_numLisOrd - 1)
                --m_primo;
            else {
                --m_primo;
                --m_ultimo;
            }
            m_ridisegna = true;
        }
    } else {
        return;
    }
    aggiorna();
}
