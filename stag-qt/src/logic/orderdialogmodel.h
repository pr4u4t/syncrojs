#pragma once

#include "collezioneordini.h"
#include "formmodel.h"

// The small dialogs over the two order lists: Del_ord, Mov_ord, cop_ord, tro_ord and their _ese versions.
// Each frm repeats the same code with its own bounds, so the operation is chosen when the model is built.
class OrderDialogModel : public FormModel
{
    Q_OBJECT

public:
    enum Kind { DeleteOrders, MoveOrders, CopyOrders, FindOrder, DeleteExecuted, CopyExecuted, FindExecuted };

    // orders is the list the dialog works on; target is ord_mac for CopyExecuted.
    OrderDialogModel(Kind kind, CollezioneOrdini *orders, CollezioneOrdini *target, QObject *parent = nullptr);

    // The list page fills the numbers before opening: txt_from = txt_to = pun_lis, txt_dst = "".
    void prefill(int order);
    void activate(const QString &previousForm) override;
    void controlEvent(const QString &control, int index, const QString &event, const QVariantMap &args) override;
    // true while the dialog stays open.
    bool functionKey(const QString &key, int shift) override;

    QString field(const QString &name) const { return m_values.value(name); }

private:
    void setField(const QString &name, const QString &value);
    void focus(const QString &name);
    void clearFields(const QStringList &names, const QString &focusOn);
    bool blockOperation();
    bool findOperation();

    Kind m_kind;
    CollezioneOrdini *m_orders;
    CollezioneOrdini *m_target;
    QStringList m_tabOrder;
    QHash<QString, QString> m_values;
    int m_focusCount = 0;
};

// ges_ord.cop_ese: copies executed orders back into the list with their production data cleared.
int cop_ese(CollezioneOrdini &executed, CollezioneOrdini &orders, int ini, int fin, int pos);
