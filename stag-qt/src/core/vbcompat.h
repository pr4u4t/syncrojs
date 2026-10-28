#pragma once

#include <QByteArray>
#include <QString>

// Reproduces VB6 runtime behaviour the original STAG code depends on.
namespace vb {

// Windows-1252 ("ANSI" on the original PCs); unmappable characters become '?'.
QByteArray toAnsi(const QString &text);
QString fromAnsi(const QByteArray &bytes);

// dBase text as DAO read it: OEM code pages (850 western, 866 cyrillic).
enum class OemCodePage { Cp850, Cp866 };
QString fromOem(const QByteArray &bytes, OemCodePage codePage = OemCodePage::Cp850);

// Val(): ignores spaces, tabs and line feeds anywhere, reads the longest numeric prefix,
// supports &H / &O prefixes and D/E exponents; anything unparsable is 0.
double val(const QString &text);

// CLng(): banker's rounding (round half to even). ok=false on overflow (VB error 6).
qint32 cLng(double value, bool *ok = nullptr);

// Integer division operator "\": both operands are rounded with CLng first, result truncated.
qint32 intDiv(double dividend, double divisor, bool *ok = nullptr);

// Assignment to "String * length": pads with spaces or truncates on the right.
QString fixedLength(const QString &value, int length);

// Trim$(): removes spaces only (not tabs, not NUL).
QString trim(const QString &text);

// uti_stag.bas int_esa: Right$("0000" & Hex$(Val(v)), 4).
QString intEsa(qint64 value);

// uti_stag.bas hex_bin: 4 bits per hex digit, MSB first; rev=true reverses the whole string (LSB first).
QString hexBin(const QString &hex, bool rev);

// uti_stag.bas leg_lng: unsigned 32-bit value with dm2 as the high word.
double legLng(const QString &dm1, const QString &dm2);

// uti_stag.bas Hex_Dec.
double hexDec(const QString &hex);

} // namespace vb
