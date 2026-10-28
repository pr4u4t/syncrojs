#pragma once

#include "vbtypes.h"

#include <QString>

class QSqlDatabase;

// par_cur_tag.bas and the cutting curve helpers of Omron_PLC.bas.
namespace stag {

struct TipCurTag {
    QString descr;
    vb::Array<qint32, 1, 15> Lun_tag{0};
    vb::Array<qint32, 1, 15> vel_max{0};
};

inline vb::Array<TipCurTag, 1, 3> cur_tag;

void leggiParCurTag(QSqlDatabase &db);
void resCurTag(QSqlDatabase &db, int liv);

int calLiv(int liv);
int calVelCurTag(int liv, qint32 l);

} // namespace stag
