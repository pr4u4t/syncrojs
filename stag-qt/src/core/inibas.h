#pragma once

class IniFile;
class QString;

// ini_bas.bas and the stag.ini reads of Principale.Main.
namespace stag {

// Main: num_liv_tag is read before leg_par_mac.
void leggiNumLivTag(IniFile &ini);
// proPatOverride replaces the Windows pro_pat of stag.ini (logs and data files are relative to it).
void legParSup(IniFile &ini, const QString &command, const QString &proPatOverride);
void legParMac(IniFile &ini);
void iniValNonUti();
// Principale.ini_liv
void iniLiv();

} // namespace stag
