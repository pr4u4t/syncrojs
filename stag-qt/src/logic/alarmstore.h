#pragma once

#include <QDateTime>
#include <QSqlDatabase>
#include <QString>

// ges_file.bas: alarm history (STO) and alarm frequency (F_A_*) tables.
namespace stag {

constexpr int MAX_FREQ_ALL = 500000;
constexpr int NUM_REC_STO = 200;

void ins_freq_all(QSqlDatabase &db, const QString &table, const QString &description);
void ins_sto(QSqlDatabase &db, const QString &description, const QDateTime &when);

} // namespace stag
