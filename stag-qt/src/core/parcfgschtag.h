#pragma once

#include <QString>

class IniFile;
class QSqlDatabase;

// par_cfg_sch_tag.bas: configuration dependent knife parameters from PAR_CFG.
namespace stag {

int getGraCar(int gCar);
// for_mac enum (0..2 as stored in stag.ini) to the PAR_CFG format in mm.
int forMacMm(int forMac);

void resCfgAtt(QSqlDatabase &db, int liv);
void leggiCfg(QSqlDatabase &db, IniFile &ini);
bool trovaCfgAdatta(QSqlDatabase &db, int numMotAbi, int liv);
bool dbfVuo(QSqlDatabase &db);
bool troCfg(QSqlDatabase &db, const QString &name, int liv);
void setCfgAtt(QSqlDatabase &db, const QString &descr, int liv);
void salvaCfgDef(QSqlDatabase &db, const QString &name, int liv);
void salvaCfgAtt(QSqlDatabase &db, const QString &name, int liv);
void modificaCfg(QSqlDatabase &db, const QString &descr, int liv);
// False when the configuration is active on a level or was not found.
bool cancellaCfg(QSqlDatabase &db, const QString &descr, int forMac, int graCar, int numMotLiv, int numMotAbi);

} // namespace stag
