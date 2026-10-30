#pragma once

#include "parmac.h"
#include "vbcompat.h"

#include <QString>

#include <cmath>

// Uni_mis_ang.bas conversions used by the pages.
namespace stag {

constexpr double INCHMM = 25.4;
constexpr double FEETMT = 3.2808;

// Format(x, "#######0.0000"). VB's appr() only rounds to 3 decimals when the regional decimal
// separator is ','; the plant's Windows uses '.', so it returned the string untouched.
inline QString con_dmm_to_inc(const QString &dmm)
{
    return QString::number(vb::val(dmm) / (INCHMM * TODECMM), 'f', 4);
}

// Format$(x, "######0")
inline QString con_met_to_fet(const QString &met)
{
    return QString::number(std::llround(vb::val(met) * FEETMT));
}

} // namespace stag
