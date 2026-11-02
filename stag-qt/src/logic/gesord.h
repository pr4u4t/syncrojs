#pragma once

#include <QString>

class ClasseOrdine;
class CollezioneOrdini;

// ges_ord.bas, uti_stag order helpers, Principale.ini_vel_max / leggi_pro / scrivi_pro.
namespace stag {

// uti_stag.ordineVuoto (checkDatiTestata = False).
bool ordineVuoto(const ClasseOrdine &rec);
int Shifta();
// ins_ord_ese_per_fine_prod
void insOrdEsePerFineProd();
void Log_lst(const CollezioneOrdini &ord);
void iniVelMax();
void leggiPro();
void scriviPro();
QString calTemCo(qint32 metRim, int vel);

} // namespace stag
