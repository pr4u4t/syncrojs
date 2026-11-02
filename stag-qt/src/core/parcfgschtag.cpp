#include "parcfgschtag.h"
#include "sqlcheck.h"

#include "inifile.h"
#include "inistore.h"
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
    sqlPrepare(q, QStringLiteral("SELECT * FROM PAR_CFG WHERE %1 ORDER BY rec_no LIMIT 1").arg(where.join(QStringLiteral(" AND "))));
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
    sqlPrepare(q, QStringLiteral("UPDATE PAR_CFG SET \"LIVELLO%1\" = '1' WHERE rec_no = ?").arg(liv));
    q.addBindValue(recNo);
    sqlExec(q);
    for (int other : {(liv + 1) % 3 == 0 ? 3 : (liv + 1) % 3, (liv + 2) % 3 == 0 ? 3 : (liv + 2) % 3}) {
        if (!isNull(r.value(QStringLiteral("LIVELLO%1").arg(other))))
            continue;
        QSqlQuery z(db);
        sqlPrepare(z, QStringLiteral("UPDATE PAR_CFG SET \"LIVELLO%1\" = '0' WHERE rec_no = ?").arg(other));
        z.addBindValue(recNo);
        sqlExec(z);
    }
}

void setIni(IniFile &ini, const QString &section, const QString &key, const QString &value)
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
    sqlExec(q, QStringLiteral("UPDATE PAR_CFG SET \"LIVELLO%1\" = '0' WHERE trim(coalesce(\"LIVELLO%1\", '')) <> '' AND CAST(trim(\"LIVELLO%1\") AS REAL) = 1").arg(liv));
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
            setIni(ini, QStringLiteral("sch_tag_liv_%1").arg(i), QStringLiteral("config_att"), par_mac.config_sch_tag_att(i).descr.trimmed());
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
                    setIni(ini, QStringLiteral("sch_tag_liv_%1").arg(i), QStringLiteral("config_att"), par_mac.config_sch_tag_att(i).descr.trimmed());
                }
                assignCfg(par_mac.par_cfg_def(i), r, i);
                par_mac.config_sch_tag_def(i).descr = descr;
                setIni(ini, QStringLiteral("sch_tag_liv_%1").arg(i), QStringLiteral("config_def"), par_mac.config_sch_tag_def(i).descr.trimmed());
            } else {
                if (!setAtt[i] && par_mac.config_sch_tag_att(i).descr.str() != QString()) {
                    par_mac.config_sch_tag_att(i).descr = "";
                    setIni(ini, QStringLiteral("sch_tag_liv_%1").arg(i), QStringLiteral("config_att"), par_mac.config_sch_tag_att(i).descr.trimmed());
                }
                if (par_mac.config_sch_tag_def(i).descr.str() != QString()) {
                    par_mac.config_sch_tag_def(i).descr = "";
                    setIni(ini, QStringLiteral("sch_tag_liv_%1").arg(i), QStringLiteral("config_def"), par_mac.config_sch_tag_def(i).descr.trimmed());
                }
            }
        }
    } else {
        for (int i = 1; i <= numLiv; ++i) {
            if (!par_mac.config_sch_tag_att(i).descr.trimmed().isEmpty()) {
                par_mac.config_sch_tag_att(i).descr = "";
                setIni(ini, QStringLiteral("sch_tag_liv_%1").arg(i), QStringLiteral("config_att"), QString());
            }
            if (!par_mac.config_sch_tag_def(i).descr.trimmed().isEmpty()) {
                par_mac.config_sch_tag_def(i).descr = "";
                setIni(ini, QStringLiteral("sch_tag_liv_%1").arg(i), QStringLiteral("config_def"), QString());
            }
        }
    }
}

namespace {

// for_mac, gra_car, num_mot_liv and num_mot_abi of the machine, as the Select Case of every routine here.
struct MachineKey {
    QString forMac;
    QString graCar;
    QString numMotLiv;
    QString numMotAbi;
};

MachineKey machineKey(int liv)
{
    return {QString::number(forMacMm(par_mac.car_tec.for_mac)), QString::number(getGraCar(par_mac.car_tec.gra_car)),
            QString::number(int(par_mac.car_tec.num_mot_liv_tag.val())), QString::number(par_abi.num_mot_abi(liv))};
}

constexpr auto KEY_WHERE = "trim(coalesce(\"DESCR\", '')) = ? AND trim(\"FOR_MAC\") = ? AND trim(\"GRA_CAR\") = ? AND trim(\"N_MOT_L\") = ? AND trim(\"N_MOT_AB\") = ?";

QString number(const FixStr &value)
{
    return QString::number(value.val(), 'g', 15);
}

// The ds("VEL_MAX" & liv) = val(par_mac_ute.par_cfg_att(liv).vel_max_tag) block.
QList<std::pair<QString, QString>> cfgColumns(int liv)
{
    const TipParCfgSchTagLiv &cfg = par_mac_ute.par_cfg_att(liv);
    const QString n = QString::number(liv);
    return {{QStringLiteral("VEL_MAX") + n, number(cfg.vel_max_tag)},
            {QStringLiteral("TEM_ATL") + n, number(cfg.tem_acc_tgl_lun)},
            {QStringLiteral("TEM_DTL") + n, number(cfg.tem_dec_tgl_lun)},
            {QStringLiteral("TEM_ATC") + n, number(cfg.tem_acc_tgl_cor)},
            {QStringLiteral("TEM_DTC") + n, number(cfg.tem_dec_tgl_cor)},
            {QStringLiteral("KP_NOR") + n, number(cfg.Kp_nor)},
            {QStringLiteral("KI_NOR") + n, number(cfg.Ki_nor)},
            {QStringLiteral("FIL_KP") + n, number(cfg.fil_kp)},
            {QStringLiteral("KV_NOR") + n, number(cfg.Kv_nor)},
            {QStringLiteral("LIM_KV") + n, number(cfg.lim_lav_Kv)},
            {QStringLiteral("MAX_GIR") + n, number(cfg.gir_max_mot_lam)}};
}

void updateCfg(QSqlDatabase &db, int recNo, int liv)
{
    QStringList assignments;
    const auto columns = cfgColumns(liv);
    for (const auto &column : columns)
        assignments << QStringLiteral("\"%1\" = ?").arg(column.first);
    QSqlQuery q(db);
    sqlPrepare(q, QStringLiteral("UPDATE PAR_CFG SET %1 WHERE rec_no = ?").arg(assignments.join(QStringLiteral(", "))));
    for (const auto &column : columns)
        q.addBindValue(column.second);
    q.addBindValue(recNo);
    if (!q.exec())
        log_str(QStringLiteral("par_cfg: ") + q.lastError().text(), MED_PRIORITY, CosLog::Cfg);
}

int insertCfg(QSqlDatabase &db, const QString &name, int liv, const MachineKey &key)
{
    QSqlQuery q(db);
    sqlPrepare(q, QStringLiteral("INSERT INTO PAR_CFG (\"DESCR\", \"LIVELLO1\", \"LIVELLO2\", \"LIVELLO3\", \"FOR_MAC\", \"GRA_CAR\", \"N_MOT_L\", \"N_MOT_AB\") VALUES (?, ?, ?, ?, ?, ?, ?, ?)"));
    q.addBindValue(name);
    for (int level = 1; level <= 3; ++level)
        q.addBindValue(level == liv ? QStringLiteral("1") : QStringLiteral("0"));
    q.addBindValue(key.forMac);
    q.addBindValue(key.graCar);
    q.addBindValue(key.numMotLiv);
    q.addBindValue(key.numMotAbi);
    if (!q.exec()) {
        log_str(QStringLiteral("par_cfg: ") + q.lastError().text(), MED_PRIORITY, CosLog::Cfg);
        return 0;
    }
    const int recNo = q.lastInsertId().toInt();
    updateCfg(db, recNo, liv);
    return recNo;
}

int countCfg(QSqlDatabase &db)
{
    QSqlQuery q(db);
    return q.exec(QStringLiteral("SELECT COUNT(*) FROM PAR_CFG")) && q.next() ? q.value(0).toInt() : 0;
}

void bindKey(QSqlQuery &q, const QString &descr, const MachineKey &key)
{
    q.addBindValue(vb::trim(descr));
    q.addBindValue(key.forMac);
    q.addBindValue(key.graCar);
    q.addBindValue(key.numMotLiv);
    q.addBindValue(key.numMotAbi);
}

// Runs after the new record is written: res_cfg_att clears LIVELLO of the record just added too, as in VB.
void afterInsert(QSqlDatabase &db, int liv)
{
    if (countCfg(db) > 1 && !par_mac.config_sch_tag_att(liv).descr.trimmed().isEmpty())
        resCfgAtt(db, liv);
}

} // namespace

bool dbfVuo(QSqlDatabase &db)
{
    return countCfg(db) == 0;
}

// The first configuration that is not DEFAULT for this machine state, else the DEFAULT one; the DEFAULT
// one also becomes the default of the level. All in par_mac_ute, waiting for the card to answer.
bool trovaCfgAdatta(QSqlDatabase &db, int numMotAbi, int liv)
{
    MachineKey key = machineKey(liv);
    key.numMotAbi = QString::number(numMotAbi);
    QSqlQuery q(db);
    sqlPrepare(q, QStringLiteral("SELECT * FROM PAR_CFG WHERE trim(\"FOR_MAC\") = ? AND trim(\"GRA_CAR\") = ? AND trim(\"N_MOT_L\") = ? AND trim(\"N_MOT_AB\") = ? ORDER BY rec_no"));
    q.addBindValue(key.forMac);
    q.addBindValue(key.graCar);
    q.addBindValue(key.numMotLiv);
    q.addBindValue(key.numMotAbi);
    if (!q.exec())
        return false;
    bool found = false;
    bool foundOther = false;
    while (q.next() && !foundOther) {
        const QSqlRecord r = q.record();
        const QString descr = field(r, QStringLiteral("DESCR")).trimmed();
        if (descr != QLatin1String("DEFAULT")) {
            foundOther = true;
            found = true;
            par_mac_ute.config_sch_tag_att(liv).descr = descr;
            par_mac_ute.config_sch_tag_att(liv).num_mot_liv = qint16(key.numMotLiv.toInt());
            assignCfg(par_mac_ute.par_cfg_att(liv), r, liv);
        }
    }
    q.seek(-1);
    while (q.next()) {
        const QSqlRecord r = q.record();
        if (field(r, QStringLiteral("DESCR")).trimmed() != QLatin1String("DEFAULT"))
            continue;
        if (!foundOther) {
            found = true;
            par_mac_ute.config_sch_tag_att(liv).descr = QStringLiteral("DEFAULT");
            par_mac_ute.config_sch_tag_att(liv).num_mot_liv = qint16(key.numMotLiv.toInt());
            assignCfg(par_mac_ute.par_cfg_att(liv), r, liv);
        }
        par_mac_ute.config_sch_tag_def(liv).descr = QStringLiteral("DEFAULT");
        assignCfg(par_mac_ute.par_cfg_def(liv), r, liv);
        break;
    }
    return found;
}

bool troCfg(QSqlDatabase &db, const QString &name, int liv)
{
    QSqlQuery q(db);
    sqlPrepare(q, QStringLiteral("SELECT rec_no FROM PAR_CFG WHERE %1 LIMIT 1").arg(QLatin1String(KEY_WHERE)));
    bindKey(q, name, machineKey(liv));
    return q.exec() && q.next();
}

void setCfgAtt(QSqlDatabase &db, const QString &descr, int liv)
{
    QSqlQuery q(db);
    sqlPrepare(q, QStringLiteral("UPDATE PAR_CFG SET \"LIVELLO%1\" = '1' WHERE %2").arg(liv).arg(QLatin1String(KEY_WHERE)));
    bindKey(q, descr, machineKey(liv));
    sqlExec(q);
}

void salvaCfgDef(QSqlDatabase &db, const QString &name, int liv)
{
    const MachineKey key = machineKey(liv);
    QSqlQuery q(db);
    sqlPrepare(q, QStringLiteral("SELECT * FROM PAR_CFG WHERE %1 ORDER BY rec_no LIMIT 1").arg(QLatin1String(KEY_WHERE)));
    bindKey(q, name, key);
    if (q.exec() && q.next()) {
        const int recNo = q.value(QStringLiteral("rec_no")).toInt();
        setLivello(db, recNo, liv, q.record());
        updateCfg(db, recNo, liv);
    } else {
        insertCfg(db, name, liv, key);
    }
    const QString section = QStringLiteral("sch_tag_liv_%1").arg(liv);
    par_mac_ute.config_sch_tag_att(liv).descr = vb::trim(name);
    par_mac_ute.config_sch_tag_att(liv).num_mot_liv = qint16(key.numMotLiv.toInt());
    setFileINIString(section, QStringLiteral("config_att"), par_mac_ute.config_sch_tag_att(liv).descr.trimmed());
    if (name == QLatin1String("DEFAULT")) {
        par_mac_ute.config_sch_tag_def(liv).descr = vb::trim(name);
        setFileINIString(section, QStringLiteral("config_def"), par_mac_ute.config_sch_tag_def(liv).descr.trimmed());
    }
    afterInsert(db, liv);
}

void salvaCfgAtt(QSqlDatabase &db, const QString &name, int liv)
{
    const MachineKey key = machineKey(liv);
    insertCfg(db, vb::trim(name), liv, key);
    par_mac_ute.config_sch_tag_att(liv).descr = vb::trim(name);
    par_mac_ute.config_sch_tag_att(liv).num_mot_liv = qint16(key.numMotLiv.toInt());
    setFileINIString(QStringLiteral("sch_tag_liv_%1").arg(liv), QStringLiteral("config_att"), par_mac_ute.config_sch_tag_att(liv).descr.trimmed());
    afterInsert(db, liv);
}

void modificaCfg(QSqlDatabase &db, const QString &descr, int liv)
{
    QSqlQuery q(db);
    sqlPrepare(q, QStringLiteral("SELECT rec_no FROM PAR_CFG WHERE %1 AND CAST(\"LIVELLO%2\" AS INTEGER) = 1 ORDER BY rec_no LIMIT 1").arg(QLatin1String(KEY_WHERE)).arg(liv));
    bindKey(q, descr, machineKey(liv));
    if (q.exec() && q.next())
        updateCfg(db, q.value(0).toInt(), liv);
}

bool cancellaCfg(QSqlDatabase &db, const QString &descr, int forMac, int graCar, int numMotLiv, int numMotAbi)
{
    const MachineKey key{QString::number(forMacMm(forMac)), QString::number(getGraCar(graCar)), QString::number(numMotLiv), QString::number(numMotAbi)};
    QSqlQuery q(db);
    sqlPrepare(q, QStringLiteral("SELECT * FROM PAR_CFG WHERE %1 ORDER BY rec_no LIMIT 1").arg(QLatin1String(KEY_WHERE)));
    bindKey(q, descr, key);
    if (!q.exec() || !q.next())
        return false;
    for (int i = 1; i <= int(par_mac.car_tec.num_liv_tag.val()); ++i) {
        if (q.value(QStringLiteral("LIVELLO%1").arg(i)).toInt() == 1)
            return false;
    }
    QSqlQuery d(db);
    sqlPrepare(d, QStringLiteral("DELETE FROM PAR_CFG WHERE rec_no = ?"));
    d.addBindValue(q.value(QStringLiteral("rec_no")));
    return d.exec();
}

} // namespace stag
