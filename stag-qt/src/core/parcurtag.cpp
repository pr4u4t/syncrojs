#include "parcurtag.h"

#include "parcfgschtag.h"
#include "parmac.h"
#include "staglog.h"

#include <QSqlDatabase>
#include <QSqlQuery>
#include <QSqlRecord>

namespace stag {
namespace {

void rieValCurTag(int liv, const QSqlRecord &rs)
{
    for (int m = 0; m <= 14; ++m) {
        const QVariant lun = rs.value(QStringLiteral("LUN_TAG%1").arg(m));
        const QVariant vel = rs.value(QStringLiteral("VEL_TAG%1").arg(m));
        cur_tag(liv).Lun_tag(m + 1) = qint32(vb::val(lun.isNull() ? QStringLiteral("0") : lun.toString()));
        cur_tag(liv).vel_max(m + 1) = qint32(vb::val(vel.isNull() ? QStringLiteral("0") : vel.toString()));
    }
}

void clear(int liv)
{
    for (int m = 1; m <= 15; ++m) {
        cur_tag(liv).Lun_tag(m) = 0;
        cur_tag(liv).vel_max(m) = 0;
    }
    cur_tag(liv).descr.clear();
}

} // namespace

void resCurTag(QSqlDatabase &db, int liv)
{
    QSqlQuery q(db);
    q.exec(QStringLiteral("UPDATE CUR_TAG SET \"LIVELLO%1\" = '0' WHERE trim(coalesce(\"LIVELLO%1\", '')) <> '' AND CAST(trim(\"LIVELLO%1\") AS REAL) = 1").arg(liv));
}

void leggiParCurTag(QSqlDatabase &db)
{
    const int forMac = forMacMm(par_mac.car_tec.for_mac);
    const int graCar = getGraCar(par_mac.car_tec.gra_car);
    const int numMotLiv = int(par_mac.car_tec.num_mot_liv_tag.val());
    for (int i = 1; i <= int(par_mac.car_tec.num_liv_tag.val()); ++i) {
        QSqlQuery count(db);
        if (!count.exec(QStringLiteral("SELECT COUNT(*) FROM CUR_TAG")) || !count.next() || count.value(0).toInt() == 0) {
            log_str(QStringLiteral("Sub leggi_par_cur_tag del Modulo par_cur_tag: il file cur_tag.dbf \u00E8 vuoto"), MED_PRIORITY, CosLog::Cur);
            for (int l = 1; l <= 3; ++l)
                clear(l);
            return;
        }
        resCurTag(db, i);
        QSqlQuery q(db);
        q.prepare(QStringLiteral("SELECT * FROM CUR_TAG WHERE FOR_MAC = ? AND GRA_CAR = ? AND N_MOT_L = ? AND N_MOT_AB = ? ORDER BY rec_no LIMIT 1"));
        q.addBindValue(forMac);
        q.addBindValue(graCar);
        q.addBindValue(numMotLiv);
        q.addBindValue(int(par_abi.num_mot_abi(i)));
        if (q.exec() && q.next()) {
            rieValCurTag(i, q.record());
            QSqlQuery u(db);
            u.prepare(QStringLiteral("UPDATE CUR_TAG SET \"LIVELLO%1\" = '1' WHERE rec_no = ?").arg(i));
            u.addBindValue(q.value(QStringLiteral("rec_no")));
            u.exec();
            cur_tag(i).descr = QString::number(forMac) + QString::number(graCar) + QString::number(numMotLiv) + QString::number(par_abi.num_mot_abi(i));
        } else {
            log_str(QStringLiteral("Sub leggi_par_cur_tag del Modulo par_cur_tag: non \u00E8 presente alcun record con formato, num. motori e num. motori abilitati = %1 %2 %3 %4")
                        .arg(forMac).arg(graCar).arg(numMotLiv).arg(par_abi.num_mot_abi(i)),
                    MED_PRIORITY, CosLog::Cur);
            clear(i);
        }
    }
}

int calLiv(int liv)
{
    if (liv <= 0)
        return 0;
    if (liv == LIV_ALT)
        return 3;
    if (liv == LIV_MED)
        return 2;
    if (liv == LIV_BAS)
        return 1;
    return 0;
}

int calVelCurTag(int liv, qint32 l)
{
    const TipCurTag &c = cur_tag(liv);
    int velCurTag = 0;
    int m = 1;
    for (; m <= 14; ++m) {
        if (c.Lun_tag(m) <= l && c.Lun_tag(m + 1) >= l) {
            const double num = double(l - c.Lun_tag(m)) * (c.vel_max(m + 1) - c.vel_max(m)) + double(c.vel_max(m)) * (c.Lun_tag(m + 1) - c.Lun_tag(m));
            const double den = double(c.Lun_tag(m + 1) - c.Lun_tag(m));
            if (den == 0)
                vb::raise(11);
            velCurTag = int(std::floor(num / den));
            break;
        }
    }
    // After a completed For loop m is 15, so this branch only runs when the match is in the last segment.
    if (m == 14 && c.Lun_tag(m + 1) < l)
        velCurTag = c.vel_max(m + 1);
    return velCurTag;
}

} // namespace stag
