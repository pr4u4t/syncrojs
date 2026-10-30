#include "orderdialogmodel.h"

#include "classestato.h"
#include "messages.h"
#include "parmac.h"
#include "staglog.h"
#include "stagglobals.h"
#include "uihooks.h"
#include "vbcompat.h"

using namespace stag;

static const QString FROM = QStringLiteral("txt_from");
static const QString TO = QStringLiteral("txt_to");
static const QString DST = QStringLiteral("txt_dst");
static const QString COD = QStringLiteral("txt_cod");

OrderDialogModel::OrderDialogModel(Kind kind, CollezioneOrdini *orders, CollezioneOrdini *target, QObject *parent)
    : FormModel(parent)
    , m_kind(kind)
    , m_orders(orders)
    , m_target(target)
{
    switch (kind) {
    case DeleteOrders:
    case DeleteExecuted:
        m_tabOrder = {TO, FROM};
        break;
    case MoveOrders:
    case CopyOrders:
    case CopyExecuted:
        m_tabOrder = {TO, DST, FROM};
        break;
    case FindOrder:
    case FindExecuted:
        m_tabOrder = {COD};
        break;
    }
    for (const QString &name : m_tabOrder) {
        set(name, QStringLiteral("modelFocus"), true);
        set(name, QStringLiteral("focusCount"), 0);
        if (name != COD)
            set(name, QStringLiteral("digitsOnly"), true);
    }
}

void OrderDialogModel::setField(const QString &name, const QString &value)
{
    m_values.insert(name, value);
    setText(name, value);
}

void OrderDialogModel::focus(const QString &name)
{
    set(name, QStringLiteral("focusCount"), ++m_focusCount);
}

void OrderDialogModel::clearFields(const QStringList &names, const QString &focusOn)
{
    for (const QString &name : names)
        setField(name, QString());
    if (!focusOn.isEmpty())
        focus(focusOn);
}

void OrderDialogModel::prefill(int order)
{
    if (m_kind == FindOrder || m_kind == FindExecuted) {
        setField(COD, QString());
    } else {
        setField(FROM, QString::number(order));
        setField(TO, QString::number(order));
        if (m_tabOrder.contains(DST))
            setField(DST, QString());
    }
    commit();
}

void OrderDialogModel::activate(const QString &)
{
    if (m_kind == FindExecuted)
        setField(COD, QString());
    focus(m_tabOrder.first());
    commit();
}

void OrderDialogModel::controlEvent(const QString &control, int, const QString &event, const QVariantMap &args)
{
    if (!m_tabOrder.contains(control))
        return;
    if (event == QLatin1String("Change")) {
        m_values.insert(control, args.value(QStringLiteral("text")).toString());
        setText(control, m_values.value(control));
    } else if (event == QLatin1String("KeyDown")) {
        const int key = args.value(QStringLiteral("key")).toInt();
        const int count = int(m_tabOrder.size());
        const int position = int(m_tabOrder.indexOf(control));
        if (key == Qt::Key_Return || key == Qt::Key_Enter || key == Qt::Key_Down || key == Qt::Key_Tab)
            focus(m_tabOrder.at((position + 1) % count));
        else if (key == Qt::Key_Up || key == Qt::Key_Backtab)
            focus(m_tabOrder.at((position - 1 + count) % count));
        else
            return;
        commit();
    }
}

bool OrderDialogModel::functionKey(const QString &key, int shift)
{
    if (key != QLatin1String("F11") || shift != 0)
        return false;
    const bool stay = m_kind == FindOrder || m_kind == FindExecuted ? findOperation() : blockOperation();
    commit();
    return stay;
}

bool OrderDialogModel::findOperation()
{
    if (m_values.value(COD).isEmpty()) {
        messaggio(msg(QStringLiteral("par_man")), true, false);
        return true;
    }
    messaggio(msg(QStringLiteral("ope_in_cor")), false, false);
    if (m_orders->Tro_lis(m_values.value(COD)) == RET_OK) {
        messaggio(msg(QStringLiteral("ope_eff")), false, false);
        return false;
    }
    messaggio(msg(QStringLiteral("cod_non_pre")), true, false);
    clearFields({COD}, COD);
    return true;
}

// eve_key_F11. CInt would stop VB with an overflow on a number over 32767; such a number is simply out of range here.
bool OrderDialogModel::blockOperation()
{
    const bool withDestination = m_tabOrder.contains(DST);
    if (m_values.value(FROM).isEmpty() || m_values.value(TO).isEmpty() || (withDestination && m_values.value(DST).isEmpty())) {
        if (m_kind == CopyOrders)
            focus(FROM);
        messaggio(msg(QStringLiteral("par_man")), true, false);
        return true;
    }
    const int ini = int(vb::val(m_values.value(FROM)));
    const int fin = int(vb::val(m_values.value(TO)));
    const int dst = int(vb::val(m_values.value(DST)));
    const int count = m_orders->Count();

    if (m_kind == CopyExecuted) {
        const int result = cop_ese(*m_orders, *m_target, ini, fin, dst);
        QString focusOn;
        QStringList cleared;
        if (fin > count || fin <= 0) {
            cleared << TO;
            focusOn = TO;
        }
        if (ini > count || ini <= 0) {
            cleared << FROM << TO;
            focusOn = FROM;
        }
        if (dst > m_target->Count() || dst < 0) {
            cleared << FROM << TO << DST;
            focusOn = FROM;
        }
        clearFields(cleared, focusOn);
        if (result != RET_OK)
            return true;
        messaggio(msg(QStringLiteral("ope_eff")), false, false);
        m_orders->scr_pun_ord(ini);
        return false;
    }

    bool valid = false;
    if (m_kind == DeleteOrders || m_kind == DeleteExecuted)
        valid = ini <= fin && fin <= count && ini > 0;
    else if (m_kind == MoveOrders)
        valid = fin <= count && ini > 0 && dst > 0 && ini <= count && dst <= count && fin > 0;
    else
        valid = ini > 0 && ini <= fin && fin <= count && dst > 0 && dst <= count + 1;

    if (!valid) {
        messaggio(msg(QStringLiteral("par_err")), true, false);
        QString focusOn;
        QStringList cleared;
        if (withDestination && (dst <= 0 || dst > count)) {
            cleared << DST;
            focusOn = DST;
        }
        // cop_ord compares these two with ord_mac_ese, not with its own list.
        const int bound = m_kind == CopyOrders && stag::ord_mac_ese ? stag::ord_mac_ese->Count() : count;
        if (fin > bound || fin <= 0) {
            cleared << TO;
            if (withDestination)
                cleared << DST;
            focusOn = TO;
        }
        if (ini > fin || ini > bound || ini <= 0) {
            cleared << FROM << TO;
            if (withDestination)
                cleared << DST;
            focusOn = FROM;
        }
        clearFields(cleared, focusOn);
        return true;
    }

    messaggio(msg(QStringLiteral("ope_in_cor")), false, false);
    int result = RET_BAD;
    if (m_kind == DeleteOrders || m_kind == DeleteExecuted)
        result = m_orders->Del_blk(ini, fin, false, false);
    else if (m_kind == MoveOrders)
        result = m_orders->Move_blk(ini, fin, dst);
    else
        result = m_orders->Cpy_blk(ini, fin, dst);
    if (result != RET_OK)
        return true;
    messaggio(msg(QStringLiteral("ope_eff")), false, false);
    return false;
}

int cop_ese(CollezioneOrdini &executed, CollezioneOrdini &orders, int ini, int fin, int pos)
{
    const bool manual = stag::sta_tag && stag::sta_tag->bit(ClasseStato::MAC_IN_MANUALE, 1);
    if (!(manual || (stag::sta_tag && !stag::sta_tag->spdeOK()))) {
        log_str(QStringLiteral("cop_ese: Tentativo di copia ordini eseguiti nella lista degli ordini da eseguire con macchina in automatico"), MED_PRIORITY, CosLog::Ord);
        messaggio(msg(QStringLiteral("mac_in_auto")), true, false);
        return RET_BAD;
    }
    if (!(fin <= executed.Count() && ini <= executed.Count() && pos > 0 && ini > 0 && fin > 0
          && (pos <= orders.Count() + 1 || (pos == 1 && orders.Count() == 0)))) {
        messaggio(msg(QStringLiteral("par_err")), true, false);
        return RET_BAD;
    }
    messaggio(msg(QStringLiteral("ope_in_cor")), false, false);
    if (ini > fin)
        std::swap(ini, fin);
    int result = RET_BAD;
    for (int i = fin; i >= ini; --i) {
        auto copy = std::make_shared<ClasseOrdine>();
        copy->setOrdine(executed.item(i)->ordine());
        for (const char *name : {"dat_fin", "dat_ini", "ora_fin", "ora_ini", "met_tot", "vel_med", "tem_fer", "tem_mar", "num_fer"})
            copy->setText(QStringLiteral("rep_pro.") + QLatin1String(name), QString());
        for (int j = 1; j <= 3; ++j) {
            for (const char *name : {"fog_buo", "fog_sca", "fog_err"})
                copy->setText(QStringLiteral("rep_pro.liv(%1).%2").arg(j).arg(QLatin1String(name)), QStringLiteral("0"));
        }
        if (orders.add(copy, pos) == RET_BAD) {
            messaggio(msg(QStringLiteral("rag_num_max")), true, false);
            break;
        }
        result = RET_OK;
    }
    return result;
}
