#include "paglisordmodel.h"

#include "classestato.h"
#include "messages.h"
#include "parmac.h"
#include "stabarmodel.h"
#include "stagglobals.h"
#include "unimisang.h"
#include "vbcompat.h"

#include <QSet>

using namespace stag;

static const QString GRID = QStringLiteral("lis_ord");

static QString rieNul(QString text)
{
    text.replace(QChar(0), u' ');
    return text;
}

PagLisOrdModel::PagLisOrdModel(CollezioneOrdini *orders, QObject *parent)
    : OrderListModel(orders, GRID, parent)
{
}

void PagLisOrdModel::load()
{
    m_detailsVisible = false;
    set(QStringLiteral("frm_det"), QStringLiteral("visible"), false);
    set(QStringLiteral("frm_det"), QStringLiteral("y"), 325);
    loadGrid();
    aggiorna();
}

void PagLisOrdModel::activate(const QString &previousForm)
{
    static const QSet<QString> keepPointer{QStringLiteral("ins_ord"), QStringLiteral("del_ord"), QStringLiteral("cop_ord"),
                                           QStringLiteral("mov_ord"), QStringLiteral("tro_ord")};
    if (!keepPointer.contains(previousForm.toLower()))
        m_orders->scr_pun_ord(1);
    placeWindow();
    m_ridisegna = true;
    aggiorna();
}

void PagLisOrdModel::adjustWindow(int count)
{
    const bool changing = stag::sta_tag && stag::sta_tag->bit(ClasseStato::MAC_CAM_ORD_IN_CORSO, 1);

    if (count != m_oldCount && m_primo == m_oldPrimo && m_ultimo == m_oldUltimo && !changing) {
        if (count > m_oldCount) { // an order came from SPDE
            m_primo = count > m_numLisOrd ? count - m_numLisOrd : 1;
            m_ultimo = count;
            m_puntatore = count - m_primo + 1;
            m_punLis = m_primo + m_puntatore - 1;
        } else if (count == 0) { // a delete command
            m_primo = m_ultimo = m_puntatore = m_punLis = 0;
        } else {
            m_primo = count < m_primo ? m_primo - m_numLisOrd : m_primo;
            m_ultimo = count;
            m_puntatore = count - m_primo + 1;
            m_punLis = m_primo + m_puntatore - 1;
        }
    } else {
        m_ultimo = qMin(m_ultimo, count);
        m_primo = qMin(m_primo, count);
        if (m_ultimo > m_oldUltimo)
            m_punLis = m_ultimo;
        else if (m_primo < m_oldPrimo)
            m_punLis = m_primo;
        m_puntatore = qMin(m_puntatore, count);
        m_punLis = qMin(m_punLis, count);
    }
    m_oldCount = count;
    m_oldPrimo = m_primo;
    m_oldUltimo = m_ultimo;
}

// ges_det
void PagLisOrdModel::showPanel(int order)
{
    if (!m_detailsVisible)
        return;
    const OrdinePtr o = m_orders->item(order);
    const QString altBob = o->text(QStringLiteral("alt_bob"));
    const QString total = o->text(QStringLiteral("met_tot_ord"));
    setText(QStringLiteral("txt_cod_com_car"), vb::trim(rieNul(o->text(QStringLiteral("tip_car.cod_com_car")))));
    setText(QStringLiteral("txt_alt_bob"), vb::cStr(abi_mis_ang ? vb::val(con_dmm_to_inc(vb::trim(rieNul(altBob)))) : double(vb::intDiv(vb::val(altBob), TODECMM))));
    setText(QStringLiteral("txt_lun_tot"), abi_mis_ang ? con_met_to_fet(vb::trim(rieNul(total))) : total);
    for (int i = 1; i <= 3; ++i) {
        QString shown;
        if (!vb::trim(o->ord(i, QStringLiteral("Ord_liv"))).isEmpty())
            shown = msg(int(vb::val(o->ord(i, QStringLiteral("cut_to_mark")))) == 1 ? QStringLiteral("msg_si") : QStringLiteral("msg_no"));
        setText(QStringLiteral("txt_cut_to_mark(%1)").arg(i - 1), shown);
    }
}

bool PagLisOrdModel::functionKey(const QString &key, int shift)
{
    if (shift != 0)
        return false;
    if (key == QLatin1String("F1") || key == QLatin1String("F8") || key == QLatin1String("F10")) {
        m_orders->scr_pun_ord(m_punLis);
        return false;
    }
    if (key == QLatin1String("F3") || key == QLatin1String("F4") || key == QLatin1String("F5") || key == QLatin1String("F6"))
    {
        if (m_orders->Count() == 0)
            return true; // the dialogs only open with orders in the list
        prefillDialog(key);
        return false;
    }
    if (key == QLatin1String("F7")) {
        if (m_orders->Count() > 0) {
            m_detailsVisible = !m_detailsVisible;
            set(QStringLiteral("frm_det"), QStringLiteral("visible"), m_detailsVisible);
            resizeList(m_detailsVisible ? 18 : 22);
        }
        return true;
    }
    return false;
}
