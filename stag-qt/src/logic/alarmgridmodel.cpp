#include "alarmgridmodel.h"

#include "alarmstore.h"
#include "classestato.h"
#include "messages.h"
#include "stabarmodel.h"
#include "vbcompat.h"
#include "vbtypes.h"

#include <QDate>
#include <QSqlQuery>

using namespace stag;

static constexpr quint32 COL_ALL_PINK = 0xFF80FF;
static constexpr quint32 COL_GREEN = 0x008000;

static QString color(quint32 bgr)
{
    return StaBarModel::vbColor(bgr);
}

AlarmGridModel::AlarmGridModel(const QString &code, const QSqlDatabase &db, QObject *parent)
    : FormModel(parent)
    , m_code(code)
    , m_db(db)
    , m_grid(code + QStringLiteral("_griglia"))
    , m_detail(code + QStringLiteral("_det_all"))
{
    m_back.fill(StaBarModel::vbWhite);
    m_fore.fill(StaBarModel::vbBlack);
}

void AlarmGridModel::load()
{
    set(m_grid, QStringLiteral("rows"), RIG_ALL);
    set(m_grid, QStringLiteral("cols"), COL_ALL);
    set(m_grid, QStringLiteral("fixedRows"), 0);
    set(m_grid, QStringLiteral("fixedCols"), 0);
    // Form_Load: rows and columns share the grid equally, text centred (ColAlignment 4).
    set(m_grid, QStringLiteral("fitCells"), true);
    set(m_grid, QStringLiteral("colAlign"), QVariantList{1, 1, 1, 1, 1, 1});
    legAll();
    commit();
}

// leg_all: the first RIG_ALL * COL_ALL records of all_xxx.dbf give each cell its address.
void AlarmGridModel::legAll()
{
    m_address.fill(QString());
    QSqlQuery query(m_db);
    int position = 0;
    if (query.exec(QStringLiteral("SELECT DM FROM \"ALL_%1\" ORDER BY rec_no").arg(m_code.toUpper()))) {
        while (query.next() && position < RIG_ALL * COL_ALL)
            m_address[size_t(position++)] = query.value(0).toString().trimmed();
    }
    enterCell();
    publish();
}

// val_bit. An address "1215" is word 12, bit 15. VB also maps "L01".. to bits after the alarm words, but
// those records are 37-39 of every all_xxx table and never reach the 36 cells.
bool AlarmGridModel::valBit(int row, int col) const
{
    const QString address = addressAt(row, col);
    if (address.isEmpty() || address.startsWith(u'L'))
        return false;
    const int position = int(vb::val(address.left(address.size() - 2))) * 16 + int(vb::val(address.right(2)));
    return vb::val(m_new.mid(position, 1)) != 0;
}

// agg_col_gri
void AlarmGridModel::setAlarms(const QString &bits)
{
    m_new = bits;
    if (m_old == m_new)
        return;
    for (int row = 0; row < RIG_ALL; ++row) {
        for (int col = 0; col < COL_ALL; ++col) {
            const size_t cell = size_t(row * COL_ALL + col);
            if (valBit(row, col)) {
                if (m_back[cell] != StaBarModel::COL_ALL_Red) {
                    const QString description = Messages::instance().find(QStringLiteral("all_%1_%2").arg(m_code, addressAt(row, col)));
                    ins_freq_all(m_db, QStringLiteral("f_a_") + m_code, description);
                    ins_sto(m_db, description, now());
                }
                m_back[cell] = StaBarModel::COL_ALL_Red;
                m_fore[cell] = StaBarModel::vbWhite;
            } else {
                m_back[cell] = StaBarModel::vbWhite;
                m_fore[cell] = StaBarModel::vbBlack;
            }
        }
    }
    m_old = m_new;
    publish();
    commit();
}

// griglia_EnterCell: the cursor is pink on an active alarm and green otherwise.
void AlarmGridModel::enterCell()
{
    const size_t cell = size_t(m_row * COL_ALL + m_col);
    const bool active = valBit(m_row, m_col);
    m_back[cell] = active ? COL_ALL_PINK : COL_GREEN;
    m_fore[cell] = active ? StaBarModel::vbBlack : StaBarModel::vbWhite;
    const QString address = addressAt(m_row, m_col);
    const QString shown = address.isEmpty() ? QString(3, u' ') : address;
    set(m_detail, QStringLiteral("foreColor"), color(active ? StaBarModel::COL_ALL_Red : StaBarModel::vbBlack));
    setText(m_detail, shown + u' ' + Messages::instance().find(QStringLiteral("det_all_%1_%2").arg(m_code, shown)));
}

// griglia_LeaveCell
void AlarmGridModel::leaveCell()
{
    const size_t cell = size_t(m_row * COL_ALL + m_col);
    const bool wasActive = m_back[cell] == COL_ALL_PINK;
    m_back[cell] = wasActive ? StaBarModel::COL_ALL_Red : StaBarModel::vbWhite;
    m_fore[cell] = wasActive ? StaBarModel::vbWhite : StaBarModel::vbBlack;
}

void AlarmGridModel::moveCursor(int row, int col)
{
    if (row < 0 || row >= RIG_ALL || col < 0 || col >= COL_ALL || (row == m_row && col == m_col))
        return;
    leaveCell();
    m_row = row;
    m_col = col;
    enterCell();
    publish();
    commit();
}

void AlarmGridModel::activate(const QString &)
{
    leaveCell();
    m_row = 0;
    m_col = 0;
    legAll();
    commit();
}

void AlarmGridModel::keyUp(int key)
{
    if (key == Qt::Key_Up)
        moveCursor(m_row - 1, m_col);
    else if (key == Qt::Key_Down)
        moveCursor(m_row + 1, m_col);
    else if (key == Qt::Key_Left)
        moveCursor(m_row, m_col - 1);
    else if (key == Qt::Key_Right)
        moveCursor(m_row, m_col + 1);
}

void AlarmGridModel::controlEvent(const QString &control, int, const QString &event, const QVariantMap &args)
{
    if (control == m_grid && event == QLatin1String("Click"))
        moveCursor(args.value(QStringLiteral("row")).toInt(), args.value(QStringLiteral("col")).toInt());
}

void AlarmGridModel::publish()
{
    QVariantList cells, back, fore;
    for (int row = 0; row < RIG_ALL; ++row) {
        QStringList texts, backs, fores;
        for (int col = 0; col < COL_ALL; ++col) {
            const size_t cell = size_t(row * COL_ALL + col);
            const QString address = addressAt(row, col);
            texts << (address.isEmpty() ? QString() : Messages::instance().find(QStringLiteral("all_%1_%2").arg(m_code, address)));
            backs << color(m_back[cell]);
            fores << color(m_fore[cell]);
        }
        cells << QVariant(texts);
        back << QVariant(backs);
        fore << QVariant(fores);
    }
    set(m_grid, QStringLiteral("cells"), cells);
    set(m_grid, QStringLiteral("cellBack"), back);
    set(m_grid, QStringLiteral("cellFore"), fore);
}

void PagAllModel::load()
{
    for (int i = 0; i <= 2; ++i)
        set(QStringLiteral("shp_cancello(%1)").arg(i), QStringLiteral("visible"), false);
    setGroups(QString(16, u'0'));
}

void PagAllModel::setGroups(const QString &v)
{
    // Bit n of the value is character n of the reversed string (POS_GRU_* count from the right).
    const auto on = [&](int pos) { return pos < v.size() && v.at(v.size() - 1 - pos) == u'1'; };
    const bool console = on(ClasseStato::POS_GRU_CON);
    set(QStringLiteral("img_consolle(1)"), QStringLiteral("visible"), console);
    set(QStringLiteral("img_consolle(0)"), QStringLiteral("visible"), !console);

    const bool machine = on(ClasseStato::POS_GRU_BM);
    const bool cabinets = on(ClasseStato::POS_GRU_QE_MST) || on(ClasseStato::POS_GRU_QE_SLV) || on(ClasseStato::POS_GRU_QE_EST);
    const int master = machine ? (cabinets ? 2 : 1) : (cabinets ? 3 : 0);
    for (int i = 0; i <= 3; ++i)
        set(QStringLiteral("img_master(%1)").arg(i), QStringLiteral("visible"), i == master);

    for (int pos = ClasseStato::POS_GRU_HAR; pos <= ClasseStato::POS_GRU_MAL; ++pos)
        set(QStringLiteral("lab_all(%1)").arg(pos), QStringLiteral("foreColor"), color(on(pos) ? StaBarModel::COL_ALL_Red : StaBarModel::vbBlack));
    commit();
}

AlarmTableModel::AlarmTableModel(const QString &grid, const QString &query, const QStringList &headers, const QVariantList &widths,
                                 const QSqlDatabase &db, QObject *parent)
    : FormModel(parent)
    , m_grid(grid)
    , m_query(query)
    , m_headers(headers)
    , m_widths(widths)
    , m_db(db)
{
}

void AlarmTableModel::activate(const QString &)
{
    QVariantList cells{QVariant(m_headers)};
    QVariantList back{color(0xC0C0C0)};
    QSqlQuery query(m_db);
    if (query.exec(m_query)) {
        while (query.next()) {
            QStringList row;
            for (int i = 0; i < m_headers.size(); ++i) {
                const QString text = query.value(i).toString();
                // Stored as a date in the DBF and shown with the plant's regional format.
                const QDate date = QDate::fromString(text, Qt::ISODate);
                row << (date.isValid() ? vb::cStrDate(date) : text);
            }
            cells << QVariant(row);
        }
    }
    set(m_grid, QStringLiteral("fixedRows"), 1);
    set(m_grid, QStringLiteral("fixedCols"), 0);
    // Form_Load: the grid fills the page from under the title bar.
    set(m_grid, QStringLiteral("x"), 0);
    set(m_grid, QStringLiteral("y"), 22);
    set(m_grid, QStringLiteral("width"), 800);
    set(m_grid, QStringLiteral("height"), 402);
    set(m_grid, QStringLiteral("cols"), m_headers.size());
    set(m_grid, QStringLiteral("rows"), cells.size());
    set(m_grid, QStringLiteral("colWidths"), m_widths);
    set(m_grid, QStringLiteral("cells"), cells);
    commit();
}
