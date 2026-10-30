#pragma once

#include "formmodel.h"

#include <QDateTime>
#include <QSqlDatabase>
#include <QStringList>

#include <array>
#include <functional>

// all_mac, all_con, all_har, all_war, all_qe1, all_qe2 and all_qe3: the same form seven times, a
// 6 x 6 grid of alarm bits. Every form listens all the time, also when not shown: a bit going
// active is what writes the alarm history.
class AlarmGridModel : public FormModel
{
    Q_OBJECT

public:
    static constexpr int RIG_ALL = 6;
    static constexpr int COL_ALL = 6;

    AlarmGridModel(const QString &code, const QSqlDatabase &db, QObject *parent = nullptr);

    // Form_Load
    void load();
    void activate(const QString &previousForm) override;
    void keyUp(int key) override;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;

    // eve_sta_sezall
    void setAlarms(const QString &bits);
    // Date and Time in VB; replaced by the tests.
    std::function<QDateTime()> now = [] { return QDateTime::currentDateTime(); };

    QString addressAt(int row, int col) const { return m_address[size_t(row * COL_ALL + col)]; }
    bool valBit(int row, int col) const;

private:
    void legAll();
    void enterCell();
    void leaveCell();
    void moveCursor(int row, int col);
    void publish();

    QString m_code;
    QSqlDatabase m_db;
    QString m_grid;
    QString m_detail;
    std::array<QString, RIG_ALL * COL_ALL> m_address;
    std::array<quint32, RIG_ALL * COL_ALL> m_back{};
    std::array<quint32, RIG_ALL * COL_ALL> m_fore{};
    QString m_new = QStringLiteral("0");
    QString m_old;
    int m_row = 0;
    int m_col = 0;
};

// pag_all.frm: the alarm groups as images and labels.
class PagAllModel : public FormModel
{
    Q_OBJECT

public:
    using FormModel::FormModel;

    void load();
    // eve_sta_gruall
    void setGroups(const QString &v);
};

// fre_*.frm and sto_all.frm: a read-only query shown in a grid, read again on every Form_Activate.
class AlarmTableModel : public FormModel
{
    Q_OBJECT

public:
    AlarmTableModel(const QString &grid, const QString &query, const QStringList &headers, const QVariantList &widths,
                    const QSqlDatabase &db, QObject *parent = nullptr);

    void activate(const QString &previousForm) override;

private:
    QString m_grid;
    QString m_query;
    QStringList m_headers;
    QVariantList m_widths;
    QSqlDatabase m_db;
};
