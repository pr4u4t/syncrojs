#pragma once

class IniFile;
class QSqlDatabase;

// par_cfg_sch_tag.bas: configuration dependent knife parameters from PAR_CFG.
namespace stag {

int getGraCar(int gCar);
// for_mac enum (0..2 as stored in stag.ini) to the PAR_CFG format in mm.
int forMacMm(int forMac);

void resCfgAtt(QSqlDatabase &db, int liv);
void leggiCfg(QSqlDatabase &db, IniFile &ini);

} // namespace stag
