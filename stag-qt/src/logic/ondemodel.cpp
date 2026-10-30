#include "ondemodel.h"

#include "messages.h"
#include "parmac.h"
#include "stabarmodel.h"
#include "uihooks.h"
#include "unimisang.h"
#include "vbcompat.h"

#include <QSqlQuery>
#include <QSqlRecord>

using namespace stag;

static const QStringList FIELDS{QStringLiteral("ONDA"), QStringLiteral("CARTA"), QStringLiteral("GRAMMATURA"), QStringLiteral("VELDIPROD"),
                                QStringLiteral("VELDICAMBI"), QStringLiteral("SPESSORE"), QStringLiteral("CODONDA")};
static const QStringList KEY_FIELDS{QStringLiteral("ONDA"), QStringLiteral("CARTA")};

OndeModel::OndeModel(CollezioneOrdini *orders, const QSqlDatabase &db, QObject *parent)
    : RecordEditModel(QStringLiteral("ONDE"), QStringLiteral("ONDA, CARTA"), FIELDS, {QStringLiteral("ONDA"), QStringLiteral("CARTA")}, db, parent)
    , m_orders(orders)
{
}

void OndeModel::load()
{
    set(QStringLiteral("ONDA"), QStringLiteral("upperCase"), true);
    set(QStringLiteral("CARTA"), QStringLiteral("upperCase"), true);
    for (const char *name : {"GRAMMATURA", "VELDIPROD", "VELDICAMBI", "SPESSORE", "CODONDA"})
        set(QLatin1String(name), QStringLiteral("digitsOnly"), true);
    loadBase({60, 205});
    commit();
}

// Data1.Refresh, and the visibility of the inch fields.
void OndeModel::activate(const QString &)
{
    for (const char *name : {"VELDIPROD_inc", "VELDICAMBI_inc", "SPESSORE_inc"})
        set(QLatin1String(name), QStringLiteral("visible"), abi_mis_ang);
    reload();
    selectRecord(qMin(m_current, int(m_records.size()) - 1));
    commit();
}

QStringList OndeModel::gridHeaders() const
{
    return {msg(QStringLiteral("db_grid_onda")), msg(QStringLiteral("db_grid_carta"))};
}

// The _Change handlers: the inch fields follow.
void OndeModel::fieldChanged(const QString &name, const QString &value)
{
    if (name == QLatin1String("SPESSORE")) {
        const QString inches = con_dmm_to_inc(value);
        if (vb::val(inches) != 0)
            setText(QStringLiteral("SPESSORE_inc"), inches);
    } else if (name == QLatin1String("VELDIPROD") || name == QLatin1String("VELDICAMBI")) {
        setText(name + QStringLiteral("_inc"), con_met_to_fet(value));
    }
}

// val_ins_ond
bool OndeModel::validateField(const QString &last)
{
    const double number = vb::val(m_values.value(last));
    const auto range = [&](double minimum, double maximum) {
        return msg(QStringLiteral("val_min")) + QStringLiteral(" %1 ").arg(minimum) + msg(QStringLiteral("val_max")) + QStringLiteral(" %1").arg(maximum);
    };
    if (last == QLatin1String("ONDA")) {
        if (m_values.value(last).isEmpty()) {
            fail(last, msg(QStringLiteral("ins_onda")));
            return false;
        }
        if (m_nuoOnd) {
            m_ond = m_values.value(last);
            QSqlQuery same(m_db);
            same.prepare(QStringLiteral("SELECT GRAMMATURA, VELDIPROD, VELDICAMBI, SPESSORE, CODONDA FROM ONDE WHERE ONDA = ?"));
            same.addBindValue(m_ond);
            if (same.exec() && same.next()) {
                for (int i = 0; i < same.record().count(); ++i)
                    setField(same.record().fieldName(i).toUpper(), same.value(i).toString());
            }
        }
    } else if (last == QLatin1String("CARTA")) {
        const QString paper = m_values.value(last);
        if (paper.isEmpty()) {
            fail(last, msg(QStringLiteral("ins_carta")));
            return false;
        }
        if (m_nuoOnd) {
            QSqlQuery same(m_db);
            same.prepare(QStringLiteral("SELECT 1 FROM ONDE WHERE ONDA = ? AND CARTA = ?"));
            same.addBindValue(m_ond);
            same.addBindValue(paper);
            if (same.exec() && same.next()) {
                fail(last, msg(QStringLiteral("car_esi")));
                return false;
            }
        }
    } else if (last == QLatin1String("VELDIPROD") || last == QLatin1String("VELDICAMBI")) {
        if (number < 10 || number > 400) {
            fail(last, range(10, 400));
            return false;
        }
    } else if (last == QLatin1String("GRAMMATURA")) {
        if (number <= 0) {
            fail(last, msg(QStringLiteral("val_min")) + QStringLiteral(" 1"));
            return false;
        }
    } else if (last == QLatin1String("SPESSORE")) {
        if (number <= 0 || number > 500) {
            fail(last, range(1, 500));
            return false;
        }
    } else if (last == QLatin1String("CODONDA")) {
        if (number < 0 || number > 99) {
            fail(last, msg(QStringLiteral("val_min")) + QStringLiteral("  0") + msg(QStringLiteral("val_max")) + QStringLiteral(" 99"));
            return false;
        }
    }
    return true;
}

bool OndeModel::usedByAnOrder(bool trimmedKey) const
{
    const QString flute = m_values.value(QStringLiteral("ONDA"));
    const QString paper = m_values.value(QStringLiteral("CARTA"));
    for (int i = 1; i <= m_orders->Count(); ++i) {
        const OrdinePtr order = m_orders->item(i);
        const QString orderFlute = vb::trim(order->text(QStringLiteral("tip_car.Cod_ond")));
        const QString orderPaper = vb::trim(order->text(QStringLiteral("tip_car.cod_com_car")));
        if (orderFlute == (trimmedKey ? vb::trim(flute) : flute) && orderPaper == (trimmedKey ? vb::trim(paper) : paper))
            return true;
    }
    return false;
}

bool OndeModel::functionKey(const QString &key, int shift)
{
    if (shift != 0)
        return false;
    if (key == QLatin1String("F1")) {
        if (!m_editing)
            newRecord();
    } else if (key == QLatin1String("F2")) {
        if (!m_editing)
            editRecord();
    } else if (key == QLatin1String("F3")) {
        if (!m_editing)
            deleteRecord();
    } else if (key == QLatin1String("F4")) {
        save();
    } else if (key == QLatin1String("ESC")) {
        abort();
    } else if (key == QLatin1String("F8") || key == QLatin1String("F9") || key == QLatin1String("F10")) {
        return m_editing; // no leaving the page in the middle of an edit
    } else {
        return false;
    }
    commit();
    return true;
}

void OndeModel::newRecord()
{
    for (const QString &name : FIELDS)
        setField(name, QString());
    setEditable(true);
    m_editing = true;
    m_ultCon.clear();
    m_nuoOnd = true;
    focus(QStringLiteral("ONDA"));
}

void OndeModel::editRecord()
{
    if (m_current < 0)
        return;
    // A flute in use keeps its key: the edit starts from the weight.
    const bool used = usedByAnOrder(true);
    setEditable(true, used ? KEY_FIELDS : QStringList{});
    m_editing = true;
    m_ultCon.clear();
    m_nuoOnd = false;
    focus(used ? QStringLiteral("GRAMMATURA") : QStringLiteral("ONDA"));
}

void OndeModel::deleteRecord()
{
    if (m_current < 0)
        return;
    if (usedByAnOrder(false)) {
        messaggio(msg(QStringLiteral("imp_can_ond")), true, false);
        return;
    }
    const Record record = m_records.at(m_current);
    if (vb::val(record.value(QStringLiteral("RISERVATO"))) == 0) {
        deleteCurrent();
        const int previous = m_current - 1;
        reload();
        selectRecord(previous >= 0 ? previous : (m_records.isEmpty() ? -1 : 0));
    }
    m_nuoOnd = false;
}

void OndeModel::save()
{
    const bool wasNew = m_nuoOnd;
    m_nuoOnd = false;
    if (!m_editing)
        return;
    if (vb::trim(m_values.value(QStringLiteral("ONDA"))).isEmpty() || vb::trim(m_values.value(QStringLiteral("CARTA"))).isEmpty()) {
        messaggio(msg(QStringLiteral("man_cam_ond")), true);
        if (m_ultCon == QLatin1String("ONDA") || m_ultCon == QLatin1String("CARTA"))
            focus(m_ultCon);
        return;
    }
    // ass_ond
    for (const char *name : {"GRAMMATURA", "VELDIPROD", "VELDICAMBI", "SPESSORE", "CODONDA"}) {
        if (m_values.value(QLatin1String(name)).isEmpty())
            setField(QLatin1String(name), QStringLiteral("0"));
    }
    if (!writeRecord(wasNew))
        return;
    const QString flute = m_values.value(QStringLiteral("ONDA"));
    const QString paper = m_values.value(QStringLiteral("CARTA"));
    reload();
    int found = 0;
    for (int i = 0; i < m_records.size(); ++i) {
        if (m_records.at(i).value(QStringLiteral("ONDA")) == flute && m_records.at(i).value(QStringLiteral("CARTA")) == paper) {
            found = i;
            break;
        }
    }
    selectRecord(found);
    m_editing = false;
    messaggio(msg(QStringLiteral("ope_eff")), false);
    setEditable(false);
}

void OndeModel::abort()
{
    if (!m_editing)
        return;
    m_editing = false;
    m_nuoOnd = false;
    selectRecord(m_current);
    setEditable(false);
}
