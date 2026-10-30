#include "patternmodel.h"

#include "messages.h"
#include "parmac.h"
#include "uihooks.h"
#include "vbcompat.h"

#include <QSqlQuery>

using namespace stag;

static constexpr int MIN_NUM_CTR = 0;
static constexpr int MAX_NUM_CTR = 4;
static constexpr int MIN_LET_FTC = 0;
static constexpr int MAX_LET_FTC = 1;
static constexpr int MAX_LEN_PAT = 999;

// TabIndex order of the form; CODICE comes last and is where a new record starts.
static const QStringList FIELDS{QStringLiteral("CONTRASTO"), QStringLiteral("PRI_LET_FC"), QStringLiteral("L1"), QStringLiteral("L2"),
                                QStringLiteral("L3"), QStringLiteral("L4"), QStringLiteral("L5"), QStringLiteral("L6"),
                                QStringLiteral("L7"), QStringLiteral("L8"), QStringLiteral("L9"), QStringLiteral("L10"),
                                QStringLiteral("OFS_CUT"), QStringLiteral("CODICE")};

static QString length(int number)
{
    return QStringLiteral("L%1").arg(number);
}

PatternModel::PatternModel(CollezioneOrdini *orders, const QSqlDatabase &db, QObject *parent)
    : RecordEditModel(QStringLiteral("PATTERN"), QStringLiteral("CODICE"), FIELDS, {QStringLiteral("CODICE")}, db, parent)
    , m_orders(orders)
{
}

void PatternModel::load()
{
    loadBase({250});
    commit();
}

void PatternModel::activate(const QString &)
{
    reload();
    selectRecord(qMin(m_current, int(m_records.size()) - 1));
    commit();
}

QStringList PatternModel::gridHeaders() const
{
    return {msg(QStringLiteral("sig_cod"))};
}

// val_ins_pat. Numbers are rewritten as CStr(Val(x)) when the field is left.
bool PatternModel::validateField(const QString &last)
{
    const auto normalize = [&]() {
        const double number = vb::val(m_values.value(last));
        setField(last, vb::cStr(number));
        return number;
    };
    const auto bounded = [&](int minimum, int maximum) {
        const double number = normalize();
        if (number < minimum || number > maximum) {
            fail(last, msg(QStringLiteral("val_min")) + QStringLiteral(" = %1; ").arg(minimum) + msg(QStringLiteral("val_max")) + QStringLiteral(" = %1").arg(maximum));
            return false;
        }
        return true;
    };

    if (last == QLatin1String("CODICE")) {
        const QString code = m_values.value(last);
        if (code.isEmpty()) {
            fail(last, msg(QStringLiteral("ins_pat")));
            return false;
        }
        // A code that already exists turns the new record into an edit of that one.
        if (m_nuoPat) {
            const int existing = rowOf(QStringLiteral("CODICE"), code);
            if (existing >= 0) {
                selectRecord(existing);
                m_nuoPat = false;
            }
        }
        return true;
    }
    if (last == QLatin1String("CONTRASTO"))
        return bounded(MIN_NUM_CTR, MAX_NUM_CTR);
    if (last == QLatin1String("PRI_LET_FC"))
        return bounded(MIN_LET_FTC, MAX_LET_FTC);
    if (last == QLatin1String("OFS_CUT")) {
        normalize();
        return true;
    }
    if (last == QLatin1String("L1")) {
        const double number = normalize();
        if (number <= 0 || number > MAX_LEN_PAT) {
            fail(last, msg(QStringLiteral("val_min")) + QStringLiteral(" > 0; ") + msg(QStringLiteral("val_max")) + QStringLiteral(" = %1").arg(MAX_LEN_PAT));
            return false;
        }
        return true;
    }
    if (last.startsWith(u'L')) {
        const int index = last.mid(1).toInt();
        const double number = normalize();
        const double previous = vb::val(m_values.value(length(index - 1)));
        if (number != 0 && (number <= previous || number > MAX_LEN_PAT)) {
            // VB names L1 as the minimum in the messages of L8 and L9.
            const QString shown = m_values.value(index == 8 || index == 9 ? QStringLiteral("L1") : length(index - 1));
            fail(last, msg(QStringLiteral("val_min")) + QStringLiteral(" > ") + shown + QStringLiteral("; ") + msg(QStringLiteral("val_max")) + QStringLiteral(" = %1").arg(MAX_LEN_PAT));
            return false;
        }
    }
    return true;
}

// err_mis_pat: true when the notches are not increasing or too short; the ones after the first zero are cleared.
bool PatternModel::errMisPat()
{
    int stop = 1;
    int longest = 0;
    for (; stop <= 10; ++stop) {
        const int value = int(vb::val(m_values.value(length(stop))));
        longest = qMax(longest, value);
        if (value == 0)
            break;
    }
    if (stop == 1)
        return true;
    for (int i = stop; i <= 10; ++i)
        setField(length(i), QStringLiteral("0"));
    for (int i = 1; i < stop; ++i) {
        const int value = int(vb::val(m_values.value(length(i))));
        if (value < 5.0 / 64 * longest)
            return true;
        if (i > 1 && value <= int(vb::val(m_values.value(length(i - 1)))))
            return true;
    }
    return false;
}

// chk_esi_cod
bool PatternModel::usedByAnOrder(const QString &code) const
{
    for (int i = 1; i <= m_orders->Count(); ++i) {
        for (int format = 1; format <= 3; ++format) {
            if (vb::trim(code) == vb::trim(m_orders->item(i)->ord(format, QStringLiteral("pat_cod"))))
                return true;
        }
    }
    return false;
}

bool PatternModel::functionKey(const QString &key, int shift)
{
    if (key == QLatin1String("F1") && shift == 2)
        return m_editing; // adj_pat_con opens only outside an edit
    if (shift != 0)
        return false;
    if (key == QLatin1String("F1")) {
        if (!m_editing)
            newRecord();
    } else if (key == QLatin1String("F2")) {
        if (!m_records.isEmpty() && !m_editing)
            editRecord();
    } else if (key == QLatin1String("F3")) {
        if (!m_editing)
            deleteRecord();
    } else if (key == QLatin1String("F4")) {
        save();
    } else if (key == QLatin1String("ESC")) {
        abort();
    } else if (key == QLatin1String("F8") || key == QLatin1String("F9") || key == QLatin1String("F10")) {
        return m_editing;
    } else {
        return false;
    }
    commit();
    return true;
}

void PatternModel::newRecord()
{
    for (const QString &name : FIELDS)
        setField(name, QString());
    setEditable(true);
    m_editing = true;
    m_ultCon.clear();
    m_nuoPat = true;
    focus(QStringLiteral("CODICE"));
}

void PatternModel::editRecord()
{
    // A code used by an order keeps its name: the edit starts from the contrast.
    const bool used = usedByAnOrder(m_values.value(QStringLiteral("CODICE")));
    setEditable(true, used ? QStringList{QStringLiteral("CODICE")} : QStringList{});
    m_editing = true;
    m_ultCon.clear();
    m_nuoPat = false;
    focus(used ? QStringLiteral("CONTRASTO") : QStringLiteral("CODICE"));
}

void PatternModel::deleteRecord()
{
    if (usedByAnOrder(m_values.value(QStringLiteral("CODICE")))) {
        messaggio(msg(QStringLiteral("imp_can_pat")), true);
        return;
    }
    if (!m_records.isEmpty()) {
        const int row = m_current;
        deleteCurrent();
        reload();
        selectRecord(qMin(row, int(m_records.size()) - 1));
    }
    m_nuoPat = false;
}

void PatternModel::save()
{
    if (!m_editing)
        return;
    bool complete = true;
    for (const QString &name : FIELDS) {
        if (name != QLatin1String("OFS_CUT") && vb::trim(m_values.value(name)).isEmpty())
            complete = false;
    }
    // Both conditions are evaluated in VB, so the notches after a zero are cleared either way.
    const bool wrongMeasures = errMisPat();
    if (!complete || wrongMeasures) {
        messaggio(wrongMeasures ? msg(QStringLiteral("err_mis_pat")) : msg(QStringLiteral("man_cam_ond")), true);
        if (!m_ultCon.isEmpty())
            focus(m_ultCon);
        return;
    }
    // ass_pat
    if (m_values.value(QStringLiteral("OFS_CUT")).isEmpty())
        setField(QStringLiteral("OFS_CUT"), QStringLiteral("0"));

    if (!writeRecord(m_nuoPat))
        return;
    const QString code = m_values.value(QStringLiteral("CODICE"));
    reload();
    selectRecord(qMax(rowOf(QStringLiteral("CODICE"), code), 0));
    m_editing = false;
    m_nuoPat = false;
    copyToOrders();
    messaggio(msg(QStringLiteral("ope_eff")), false);
    setEditable(false);
}

// "riassegna i valori modificati agli ordini". VB does it only with more than one order in the list.
void PatternModel::copyToOrders()
{
    if (m_orders->Count() <= 1)
        return;
    for (int i = 1; i <= m_orders->Count(); ++i) {
        const OrdinePtr order = m_orders->item(i);
        for (int format = 1; format <= 3; ++format) {
            if (vb::trim(m_values.value(QStringLiteral("CODICE"))) != vb::trim(order->ord(format, QStringLiteral("pat_cod"))))
                continue;
            const auto copy = [&](const QString &target, const QString &source) {
                order->setText(QStringLiteral("ord(%1).%2").arg(format).arg(target), vb::trim(m_values.value(source)));
            };
            copy(QStringLiteral("pat_con"), QStringLiteral("CONTRASTO"));
            copy(QStringLiteral("fst_FTC"), QStringLiteral("PRI_LET_FC"));
            for (int l = 1; l <= 10; ++l)
                copy(length(l), length(l));
            copy(QStringLiteral("Ofs_Cut"), QStringLiteral("OFS_CUT"));
        }
    }
}

void PatternModel::abort()
{
    if (!m_editing)
        return;
    m_editing = false;
    m_nuoPat = false;
    selectRecord(m_current);
    setEditable(false);
}
