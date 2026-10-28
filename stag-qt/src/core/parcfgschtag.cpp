#include "parcfgschtag.h"

#include "inifile.h"
#include "parmac.h"
#include "staglog.h"

#include <QSqlDatabase>
#include <QSqlError>
#include <QSqlQuery>
#include <QSqlRecord>

namespace stag {
namespace {

bool isNull(const QVariant &v)
{
    // DAO returns Null for blank dBase character fields.
    return v.isNull() || v.toString().trimmed().isEmpty();
}

QString field(const QSqlRecord &r, const QString &name)
{
    const QVariant v = r.value(name);
    return isNull(v) ? QString() : v.toString();
}

void assignCfg(TipParCfgSchTagLiv &cfg, const QSqlRecord &r, int i)
{
    const QString n = QString::number(i);
    cfg.vel_max_tag = field(r, QStringLiteral("VEL_MAX") + n);
    cfg.tem_acc_tgl_lun = field(r, QStringLiteral("TEM_ATL") + n);
    cfg.tem_dec_tgl_lun = field(r, QStringLiteral("TEM_DTL") + n);
    cfg.tem_acc_tgl_cor = field(r, QStringLiteral("TEM_ATC") + n);
    cfg.tem_dec_tgl_cor = field(r, QStringLiteral("TEM_DTC") + n);
    cfg.Kp_nor = field(r, QStringLiteral("KP_NOR") + n);
    cfg.Ki_nor = field(r, QStringLiteral("KI_NOR") + n);
    cfg.fil_kp = field(r, QStringLiteral("FIL_KP") + n);
    cfg.Kv_nor = field(r, QStringLiteral("KV_NOR") + n);
    cfg.lim_lav_Kv = field(r, QStringLiteral("LIM_KV") + n);
    cfg.gir_max_mot_lam = field(r, QStringLiteral("MAX_GIR") + n);
}

bool hasRecords(QSqlDatabase &db)
{
    QSqlQuery q(db);
    return q.exec(QStringLiteral("SELECT COUNT(*) FROM PAR_CFG")) && q.next() && q.value(0).toInt() > 0;
}

// Table Seek on the composite index: first record (index order, then physical order) with equal keys.
bool seek(QSqlDatabase &db, const QStringList &columns, const QStringList &values, QSqlRecord *found, int *recNo)
{
    QStringList where;
    for (const QString &c : columns)
        where.append(QStringLiteral("trim(coalesce(\"%1\", '')) = ?").arg(c));
    QSqlQuery q(db);
    q.prepare(QStringLiteral("SELECT * FROM PAR_CFG WHERE %1 ORDER BY rec_no LIMIT 1").arg(where.join(QStringLiteral(" AND "))));
    for (const QString &v : values)
        q.addBindValue(vb::trim(v));
    if (!q.exec()) {
        log_str(QStringLiteral("leggi_cfg: ") + q.lastError().text(), MED_PRIORITY, CosLog::Cfg);
        return false;
    }
    if (!q.next())
        return false;
    *found = q.record();
    *recNo = q.value(QStringLiteral("rec_no")).toInt();
    return true;
}

void setLivello(QSqlDatabase &db, int recNo, int liv, const QSqlRecord &r)
{
    QSqlQuery q(db);
    q.prepare(QStringLiteral("UPDATE PAR_CFG SET \"LIVELLO%1\" = '1' WHERE rec_no = ?").arg(liv));
    q.addBindValue(recNo);
    q.exec();
    for (int other : {(liv + 1) % 3 == 0 ? 3 : (liv + 1) % 3, (liv + 2) % 3 == 0 ? 3 : (liv + 2) % 3}) {
        if (!isNull(r.value(QStringLiteral("LIVELLO%1").arg(other))))
            continue;
        QSqlQuery z(db);
        z.prepare(QStringLiteral("UPDATE PAR_CFG SET \"LIVELLO%1\" = '0' WHERE rec_no = ?").arg(other));
        z.addBindValue(recNo);
        z.exec();
    }
}

void setFileINIString(IniFile &ini, const QString &section, const QString &key, const QString &value)
{
    ini.setValue(section, key, value);
    ini.save();
}

} // namespace

int getGraCar(int gCar)
{
    switch (gCar) {
    case gm2_1800: return 1800;
    case gm2_2600: return 2600;
    default: return gCar;
    }
}

int forMacMm(int forMac)
{
    switch (forMac + 1) {
    case mm1800: return 1800;
    case mm2500: return 2500;
    case mm2800: return 2800;
    default: return forMac;
    }
}

void resCfgAtt(QSqlDatabase &db, int liv)
{
    QSqlQuery q(db);
    q.exec(QStringLiteral("UPDATE PAR_CFG SET \"LIVELLO%1\" = '0' WHERE trim(coalesce(\"LIVELLO%1\", '')) <> '' AND CAST(trim(\"LIVELLO%1\") AS REAL) = 1").arg(liv));
}

void leggiCfg(QSqlDatabase &db, IniFile &ini)
{
    const int numLiv = int(par_mac.car_tec.num_liv_tag.val());
    const QString forMac = QString::number(forMacMm(par_mac.car_tec.for_mac));
    const QString graCar = QString::number(getGraCar(par_mac.car_tec.gra_car));
    const QString numMotLiv = QString::number(int(par_mac.car_tec.num_mot_liv_tag.val()));
    bool setAtt[4] = {false, false, false, false};

    for (int i = 1; i <= numLiv; ++i) {
        if (!hasRecords(db))
            break;
        setAtt[i] = false;
        resCfgAtt(db, i);
        const QString descr = par_mac.config_sch_tag_att(i).descr.trimmed();
        QSqlRecord r;
        int recNo = 0;
        if (seek(db, {QStringLiteral("DESCR"), QStringLiteral("LIVELLO%1").arg(i), QStringLiteral("FOR_MAC"), QStringLiteral("GRA_CAR"),
                      QStringLiteral("N_MOT_L"), QStringLiteral("N_MOT_AB")},
                 {descr, QStringLiteral("1"), forMac, graCar, numMotLiv, QString::number(par_abi.num_mot_abi(i))}, &r, &recNo)) {
            assignCfg(par_mac.par_cfg_att(i), r, i);
            setAtt[i] = true;
            par_mac.config_sch_tag_att(i).num_mot_liv = vb::cInt(vb::val(r.value(QStringLiteral("N_MOT_L")).toString()));
            par_mac.config_sch_tag_att(i).descr = descr;
            setFileINIString(ini, QStringLiteral("sch_tag_liv_%1").arg(i), QStringLiteral("config_att"), par_mac.config_sch_tag_att(i).descr.trimmed());
        }
    }

    if (hasRecords(db)) {
        for (int i = 1; i <= numLiv; ++i) {
            QString descr = par_mac.config_sch_tag_def(i).descr.trimmed();
            if (descr.isEmpty())
                descr = QStringLiteral("DEFAULT");
            QSqlRecord r;
            int recNo = 0;
            if (seek(db, {QStringLiteral("DESCR"), QStringLiteral("FOR_MAC"), QStringLiteral("GRA_CAR"), QStringLiteral("N_MOT_L"), QStringLiteral("N_MOT_AB")},
                     {descr, forMac, graCar, numMotLiv, QString::number(par_abi.num_mot_abi(i))}, &r, &recNo)) {
                if (!setAtt[i]) {
                    assignCfg(par_mac.par_cfg_att(i), r, i);
                    par_mac.config_sch_tag_att(i).num_mot_liv = vb::cInt(vb::val(r.value(QStringLiteral("N_MOT_L")).toString()));
                    setAtt[i] = true;
                    setLivello(db, recNo, i, r);
                    par_mac.config_sch_tag_att(i).descr = descr;
                    setFileINIString(ini, QStringLiteral("sch_tag_liv_%1").arg(i), QStringLiteral("config_att"), par_mac.config_sch_tag_att(i).descr.trimmed());
                }
                assignCfg(par_mac.par_cfg_def(i), r, i);
                par_mac.config_sch_tag_def(i).descr = descr;
                setFileINIString(ini, QStringLiteral("sch_tag_liv_%1").arg(i), QStringLiteral("config_def"), par_mac.config_sch_tag_def(i).descr.trimmed());
            } else {
                if (!setAtt[i] && par_mac.config_sch_tag_att(i).descr.str() != QString()) {
                    par_mac.config_sch_tag_att(i).descr = "";
                    setFileINIString(ini, QStringLiteral("sch_tag_liv_%1").arg(i), QStringLiteral("config_att"), par_mac.config_sch_tag_att(i).descr.trimmed());
                }
                if (par_mac.config_sch_tag_def(i).descr.str() != QString()) {
                    par_mac.config_sch_tag_def(i).descr = "";
                    setFileINIString(ini, QStringLiteral("sch_tag_liv_%1").arg(i), QStringLiteral("config_def"), par_mac.config_sch_tag_def(i).descr.trimmed());
                }
            }
        }
    } else {
        for (int i = 1; i <= numLiv; ++i) {
            if (!par_mac.config_sch_tag_att(i).descr.trimmed().isEmpty()) {
                par_mac.config_sch_tag_att(i).descr = "";
                setFileINIString(ini, QStringLiteral("sch_tag_liv_%1").arg(i), QStringLiteral("config_att"), QString());
            }
            if (!par_mac.config_sch_tag_def(i).descr.trimmed().isEmpty()) {
                par_mac.config_sch_tag_def(i).descr = "";
                setFileINIString(ini, QStringLiteral("sch_tag_liv_%1").arg(i), QStringLiteral("config_def"), QString());
            }
        }
    }
}

} // namespace stag
