#include "recordeditmodel.h"
#include "sqlcheck.h"

#include "stabarmodel.h"
#include "uihooks.h"

#include <QSqlError>
#include <QSqlQuery>
#include <QSqlRecord>

using namespace stag;

static const QString GRID = QStringLiteral("lis");
static const int PAGE_ROWS = 20;

RecordEditModel::RecordEditModel(const QString &table, const QString &orderBy, const QStringList &fields, const QStringList &gridColumns,
                                 const QSqlDatabase &db, QObject *parent)
    : FormModel(parent)
    , m_table(table)
    , m_fields(fields)
    , m_db(db)
    , m_orderBy(orderBy)
    , m_gridColumns(gridColumns)
{
}

void RecordEditModel::loadBase(const QVariantList &colWidths)
{
    for (const QString &name : m_fields) {
        set(name, QStringLiteral("modelFocus"), true);
        set(name, QStringLiteral("focusCount"), 0);
    }
    set(GRID, QStringLiteral("fixedRows"), 1);
    set(GRID, QStringLiteral("fixedCols"), 0);
    set(GRID, QStringLiteral("cols"), m_gridColumns.size());
    set(GRID, QStringLiteral("rowHeight"), 18);
    set(GRID, QStringLiteral("colWidths"), colWidths);
    reload();
    selectRecord(m_records.isEmpty() ? -1 : 0);
    setEditable(false);
}

void RecordEditModel::reload()
{
    m_records.clear();
    QSqlQuery query(m_db);
    if (query.exec(QStringLiteral("SELECT * FROM %1 ORDER BY %2").arg(m_table, m_orderBy))) {
        while (query.next()) {
            Record record;
            for (int i = 0; i < query.record().count(); ++i)
                record.insert(query.record().fieldName(i).toUpper(), query.value(i).toString());
            m_records << record;
        }
    }
    publishGrid();
}

void RecordEditModel::publishGrid()
{
    QVariantList cells{QVariant(gridHeaders())};
    for (const Record &record : m_records) {
        QStringList row;
        for (const QString &column : m_gridColumns)
            row << record.value(column);
        cells << QVariant(row);
    }
    set(GRID, QStringLiteral("rows"), cells.size());
    set(GRID, QStringLiteral("cells"), cells);
}

void RecordEditModel::selectRecord(int row)
{
    m_current = row;
    set(GRID, QStringLiteral("row"), row + 1);
    const Record record = row >= 0 && row < m_records.size() ? m_records.at(row) : Record();
    for (const QString &name : m_fields)
        setField(name, record.value(name));
}

int RecordEditModel::rowOf(const QString &column, const QString &value) const
{
    for (int i = 0; i < m_records.size(); ++i) {
        if (m_records.at(i).value(column).compare(value, Qt::CaseInsensitive) == 0)
            return i;
    }
    return -1;
}

void RecordEditModel::setField(const QString &name, const QString &value)
{
    m_values.insert(name, value);
    setText(name, value);
    set(name, QStringLiteral("foreColor"), StaBarModel::vbColor(StaBarModel::vbBlack));
    fieldChanged(name, value);
}

// enable_tab / disable_tab. Fields outside an edit cannot be reached in VB and are read-only here.
void RecordEditModel::setEditable(bool editable, const QStringList &lockedFields)
{
    m_tabStops.clear();
    for (const QString &name : m_fields) {
        const bool locked = !editable || lockedFields.contains(name);
        set(name, QStringLiteral("readOnly"), locked);
        if (!locked)
            m_tabStops << name;
    }
}

void RecordEditModel::focus(const QString &control)
{
    set(control, QStringLiteral("focusCount"), ++m_focusCount);
}

void RecordEditModel::fail(const QString &control, const QString &message)
{
    messaggio(message, true);
    set(control, QStringLiteral("foreColor"), StaBarModel::vbColor(StaBarModel::vbRed));
    focus(control);
}

void RecordEditModel::validateOnFocus(const QString &control)
{
    if (m_ultCon.isEmpty())
        m_ultCon = control;
    if (m_ultCon == control)
        return;
    if (validateField(m_ultCon))
        m_ultCon = control;
}

// Form_KeyDown sends {TAB} or +{TAB} for Return, Down and Up.
void RecordEditModel::moveFocus(int step)
{
    if (!m_editing || m_tabStops.isEmpty())
        return;
    const int position = m_tabStops.indexOf(m_ultCon);
    const int count = int(m_tabStops.size());
    focus(m_tabStops.at((qMax(position, 0) + step + count) % count));
}

void RecordEditModel::controlEvent(const QString &control, int, const QString &event, const QVariantMap &args)
{
    QString name;
    for (const QString &candidate : m_fields) {
        if (candidate.compare(control, Qt::CaseInsensitive) == 0)
            name = candidate;
    }
    if (control == GRID) {
        if (event == QLatin1String("Click") && !m_editing && args.value(QStringLiteral("row")).toInt() >= 1)
            selectRecord(args.value(QStringLiteral("row")).toInt() - 1);
        else
            return;
    } else if (name.isEmpty()) {
        return;
    } else if (event == QLatin1String("Change")) {
        setField(name, args.value(QStringLiteral("text")).toString());
    } else if (event == QLatin1String("GotFocus")) {
        if (!m_editing)
            return;
        validateOnFocus(name);
    } else if (event == QLatin1String("KeyDown")) {
        const int key = args.value(QStringLiteral("key")).toInt();
        if (key == Qt::Key_Return || key == Qt::Key_Enter || key == Qt::Key_Down || key == Qt::Key_Tab)
            moveFocus(1);
        else if (key == Qt::Key_Up || key == Qt::Key_Backtab)
            moveFocus(-1);
        else
            return;
    } else {
        return;
    }
    commit();
}

// Form_KeyDown turns the arrows into {TAB}, which on the list has nowhere to go; only pages move it.
void RecordEditModel::keyUp(int key)
{
    if (m_editing || m_records.isEmpty())
        return;
    if (key == Qt::Key_PageDown)
        selectRecord(qMin(m_current + PAGE_ROWS, int(m_records.size()) - 1));
    else if (key == Qt::Key_PageUp)
        selectRecord(qMax(m_current - PAGE_ROWS, 0));
    else
        return;
    commit();
}

bool RecordEditModel::writeRecord(bool isNew)
{
    QStringList assignments, marks;
    for (const QString &name : m_fields) {
        assignments << name + QStringLiteral(" = ?");
        marks << QStringLiteral("?");
    }
    QSqlQuery write(m_db);
    if (isNew)
        sqlPrepare(write, QStringLiteral("INSERT INTO %1 (%2) VALUES (%3)").arg(m_table, m_fields.join(u','), marks.join(u',')));
    else
        sqlPrepare(write, QStringLiteral("UPDATE %1 SET %2 WHERE rec_no = ?").arg(m_table, assignments.join(u',')));
    for (const QString &name : m_fields)
        write.addBindValue(m_values.value(name));
    if (!isNew)
        write.addBindValue(m_records.at(m_current).value(QStringLiteral("REC_NO")));
    if (!write.exec()) {
        messaggio(write.lastError().text(), true);
        return false;
    }
    return true;
}

bool RecordEditModel::deleteCurrent()
{
    QSqlQuery remove(m_db);
    sqlPrepare(remove, QStringLiteral("DELETE FROM %1 WHERE rec_no = ?").arg(m_table));
    remove.addBindValue(m_records.at(m_current).value(QStringLiteral("REC_NO")));
    if (!remove.exec()) {
        messaggio(remove.lastError().text(), true);
        return false;
    }
    return true;
}
