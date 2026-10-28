#pragma once

#include "vbcompat.h"

#include <QDateTime>
#include <QString>

#include <array>
#include <stdexcept>
#include <utility>

namespace vb {

// Run-time error caught by an "On Error GoTo" handler of the ported procedure.
struct Error : std::runtime_error {
    Error(int number, const char *description)
        : std::runtime_error(description)
        , number(number)
    {
    }
    int number;
};

[[noreturn]] inline void raise(int number)
{
    switch (number) {
    case 5: throw Error(5, "Invalid procedure call or argument");
    case 6: throw Error(6, "Overflow");
    case 9: throw Error(9, "Subscript out of range");
    case 11: throw Error(11, "Division by zero");
    case 13: throw Error(13, "Type mismatch");
    default: throw Error(number, "Application-defined or object-defined error");
    }
}

// CStr() of a Double: integers without decimals, otherwise up to 15 significant digits.
QString cStr(double value);
// CStr() of a Single: 7 significant digits.
QString cStrSingle(float value);

// CInt()/CLng() of a String: blanks around a number are accepted, anything else is error 13.
qint16 cIntStr(const QString &text);
qint32 cLngStr(const QString &text);

// Format$(v, "000") with v a String: numeric text is zero padded, "" stays "".
QString formatStr(const QString &text, int digits);

// Regional settings of the original PCs (short date M/d/yyyy, time h.mm.ss AM/PM).
// Now/Date/Time; tests can freeze the clock.
QDateTime now();
void setNow(const QDateTime &frozen);
QString cStrDate(const QDate &date);
QString cStrTime(const QTime &time);
// CDate() of text written with those settings; invalid text is error 13.
QDateTime cDate(const QString &text);
// DateDiff("s", a, b) with string arguments.
qint64 dateDiffSeconds(const QString &a, const QString &b);

// CBool() of a String used in a comparison with True/False: numeric text, "True"/"False".
bool cBool(const QString &text);

// CInt(): banker's rounding to a 16-bit Integer; ok=false on overflow (VB error 6).
qint16 cInt(double value, bool *ok = nullptr);

// Variable of type "String * n": starts as n NUL characters, assignments pad with spaces or truncate.
class FixStr
{
public:
    explicit FixStr(int length)
        : m_value(length, QChar(0))
    {
    }

    FixStr &operator=(const QString &value)
    {
        m_value = fixedLength(value, int(m_value.size()));
        return *this;
    }
    FixStr &operator=(const char *value) { return *this = QString::fromLatin1(value); }
    FixStr &operator=(double value) { return *this = cStr(value); }
    FixStr &operator=(int value) { return *this = cStr(value); }
    FixStr &operator=(const FixStr &other)
    {
        return *this = other.m_value;
    }
    FixStr(const FixStr &other) = default;

    operator QString() const { return m_value; }
    const QString &str() const { return m_value; }
    int length() const { return int(m_value.size()); }
    double val() const { return vb::val(m_value); }
    QString trimmed() const { return vb::trim(m_value); }

    bool operator==(const QString &other) const { return m_value == other; }
    bool operator!=(const QString &other) const { return m_value != other; }

private:
    QString m_value;
};

// "Dim a(Lo To Hi) As T"
template <typename T, int Lo, int Hi>
class Array
{
    static constexpr std::size_t N = std::size_t(Hi - Lo + 1);

    template <std::size_t... I>
    static std::array<T, N> filled(const T &value, std::index_sequence<I...>)
    {
        return {{(void(I), value)...}};
    }

public:
    Array() = default;
    explicit Array(const T &init)
        : m_items(filled(init, std::make_index_sequence<N>{}))
    {
    }

    T &operator()(int index)
    {
        if (index < Lo || index > Hi)
            raise(9);
        return m_items[std::size_t(index - Lo)];
    }
    const T &operator()(int index) const
    {
        if (index < Lo || index > Hi)
            raise(9);
        return m_items[std::size_t(index - Lo)];
    }
    static constexpr int lbound() { return Lo; }
    static constexpr int ubound() { return Hi; }

private:
    std::array<T, N> m_items;
};

} // namespace vb
