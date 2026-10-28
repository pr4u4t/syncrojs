#include "vbtypes.h"

#include <QRegularExpression>

#include <cmath>

namespace vb {

QString cStr(double value)
{
    if (std::isfinite(value) && std::floor(value) == value && std::fabs(value) < 1e15)
        return QString::number(qint64(value));
    return QString::number(value, 'g', 15);
}

bool cBool(const QString &text)
{
    const QString t = text.trimmed();
    if (t.compare(QStringLiteral("True"), Qt::CaseInsensitive) == 0)
        return true;
    if (t.compare(QStringLiteral("False"), Qt::CaseInsensitive) == 0)
        return false;
    return val(t) != 0;
}

qint16 cInt(double value, bool *ok)
{
    bool inRange = true;
    const qint32 v = cLng(value, &inRange);
    inRange = inRange && v >= -32768 && v <= 32767;
    if (ok)
        *ok = inRange;
    return inRange ? qint16(v) : 0;
}

namespace {

double strictNumber(const QString &text)
{
    bool ok = false;
    const double v = trim(text).toDouble(&ok);
    if (!ok)
        raise(13);
    return v;
}

} // namespace

QString cStrSingle(float value)
{
    return QString::number(double(value), 'g', 7);
}

qint16 cIntStr(const QString &text)
{
    bool ok = false;
    const qint16 v = cInt(strictNumber(text), &ok);
    if (!ok)
        raise(6);
    return v;
}

qint32 cLngStr(const QString &text)
{
    bool ok = false;
    const qint32 v = cLng(strictNumber(text), &ok);
    if (!ok)
        raise(6);
    return v;
}

QString formatStr(const QString &text, int digits)
{
    bool ok = false;
    const double v = trim(text).toDouble(&ok);
    if (!ok)
        return text;
    const qint64 n = qint64(std::llround(v));
    const QString s = QString::number(n < 0 ? -n : n).rightJustified(digits, u'0');
    return n < 0 ? u'-' + s : s;
}

namespace {

QDateTime g_frozenNow;

} // namespace

QDateTime now()
{
    return g_frozenNow.isValid() ? g_frozenNow : QDateTime::currentDateTime();
}

void setNow(const QDateTime &frozen)
{
    g_frozenNow = frozen;
}

QString cStrDate(const QDate &date)
{
    return date.toString(QStringLiteral("M/d/yyyy"));
}

QString cStrTime(const QTime &time)
{
    const int h12 = time.hour() % 12 == 0 ? 12 : time.hour() % 12;
    return QStringLiteral("%1.%2.%3 %4")
        .arg(h12)
        .arg(time.minute(), 2, 10, QLatin1Char('0'))
        .arg(time.second(), 2, 10, QLatin1Char('0'))
        .arg(time.hour() < 12 ? QStringLiteral("AM") : QStringLiteral("PM"));
}

QDateTime cDate(const QString &text)
{
    QDate date(1899, 12, 30);
    QTime time(0, 0);
    int ampm = 0;
    bool any = false;
    for (const QString &token : trim(text).split(u' ', Qt::SkipEmptyParts)) {
        if (token.compare(QStringLiteral("AM"), Qt::CaseInsensitive) == 0) {
            ampm = 1;
        } else if (token.compare(QStringLiteral("PM"), Qt::CaseInsensitive) == 0) {
            ampm = 2;
        } else if (token.contains(u'/')) {
            const QStringList p = token.split(u'/');
            if (p.size() != 3)
                raise(13);
            int year = p[2].toInt();
            if (p[2].size() <= 2)
                year += year < 30 ? 2000 : 1900;
            date = QDate(year, p[0].toInt(), p[1].toInt());
            if (!date.isValid())
                raise(13);
            any = true;
        } else if (token.contains(u'.') || token.contains(u':')) {
            const QStringList p = token.split(QRegularExpression(QStringLiteral("[.:]")));
            if (p.size() < 2 || p.size() > 3)
                raise(13);
            time = QTime(p[0].toInt(), p[1].toInt(), p.size() == 3 ? p[2].toInt() : 0);
            if (!time.isValid())
                raise(13);
            any = true;
        } else {
            raise(13);
        }
    }
    if (!any)
        raise(13);
    // "06.59 PM" is 18:59, "14.13 PM" stays 14:13.
    if (ampm == 2 && time.hour() < 12)
        time = time.addSecs(12 * 3600);
    else if (ampm == 1 && time.hour() == 12)
        time = time.addSecs(-12 * 3600);
    return QDateTime(date, time);
}

qint64 dateDiffSeconds(const QString &a, const QString &b)
{
    return cDate(a).secsTo(cDate(b));
}

} // namespace vb
