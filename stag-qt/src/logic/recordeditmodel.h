#pragma once

#include "formmodel.h"

#include <QHash>
#include <QSqlDatabase>
#include <QStringList>
#include <QVector>

// What Onde.frm and Pattern.frm share: a table bound to a Data control, listed in a grid and edited
// in text boxes, where leaving a field validates it and Return, Up and Down walk the tab order.
class RecordEditModel : public FormModel
{
    Q_OBJECT

public:
    using Record = QHash<QString, QString>;

    RecordEditModel(const QString &table, const QString &orderBy, const QStringList &fields, const QStringList &gridColumns,
                    const QSqlDatabase &db, QObject *parent = nullptr);

    void keyUp(int key) override;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;

    bool editing() const { return m_editing; }
    int currentRow() const { return m_current; }
    int recordCount() const { return int(m_records.size()); }
    QString field(const QString &name) const { return m_values.value(name); }

protected:
    void loadBase(const QVariantList &colWidths);
    void reload();
    void selectRecord(int row);
    int rowOf(const QString &column, const QString &value) const;
    void setField(const QString &name, const QString &value);
    void setEditable(bool editable, const QStringList &lockedFields = {});
    void focus(const QString &control);
    void fail(const QString &control, const QString &message);
    // INSERT, or UPDATE of the current record; the error goes to the status bar.
    bool writeRecord(bool isNew);
    bool deleteCurrent();

    virtual QStringList gridHeaders() const = 0;
    // val_ins_*: m_ultCon is the field being left; returns false after fail().
    virtual bool validateField(const QString &field) = 0;
    // The _Change handlers beyond the black text.
    virtual void fieldChanged(const QString &, const QString &) {}

    QString m_table;
    QStringList m_fields;
    QStringList m_tabStops;
    QVector<Record> m_records;
    QSqlDatabase m_db;
    Record m_values;
    int m_current = -1;
    bool m_editing = false;
    QString m_ultCon;

private:
    void publishGrid();
    void validateOnFocus(const QString &control);
    void moveFocus(int step);

    QString m_orderBy;
    QStringList m_gridColumns;
    int m_focusCount = 0;
};
