#pragma once

#include "collezioneordini.h"
#include "formmodel.h"
#include "orderdialogmodel.h"

#include <QHash>

// What pag_lis_ord.frm and pag_lis_ese.frm have in common: three grid rows per order, the scrolling
// window and its colours. Each page keeps its own copy of this code in VB.
class OrderListModel : public FormModel
{
    Q_OBJECT

public:
    OrderListModel(CollezioneOrdini *orders, const QString &grid, QObject *parent = nullptr);

    void keyUp(int key) override;
    // The dialog a function key opens over this list; it gets pun_lis before opening.
    void setDialog(const QString &key, OrderDialogModel *dialog) { m_dialogs.insert(key, dialog); }

    int first() const { return m_primo; }
    int last() const { return m_ultimo; }
    int selected() const { return m_punLis; }
    int cursor() const { return m_puntatore; }
    int ordersPerPage() const { return m_numLisOrd; }

protected:
    static constexpr int ROW_HEIGHT = 17;

    void loadGrid();
    void placeWindow();
    void resizeList(int rows);
    void aggiorna();
    void prefillDialog(const QString &key);
    // Start of aggiorna, which differs between the pages.
    virtual void adjustWindow(int count) = 0;
    // ges_det / ges_rep, when the panel is visible.
    virtual void showPanel(int order) = 0;

    CollezioneOrdini *m_orders;
    QHash<QString, OrderDialogModel *> m_dialogs;
    QString m_grid;
    int m_numLivLisOrd = 3;
    int m_numLisOrd = 7;
    int m_punLis = 1;
    int m_primo = 1;
    int m_ultimo = 0;
    int m_puntatore = 1;
    bool m_ridisegna = true;
    bool m_keyUp = false;
    bool m_keyDown = false;

private:
    void rebuildRows();
    void paintAllRows();
    void paintMovedRows(int direction);
    void publishGrid();
    QStringList orderRow(int order, int format) const;

    QVariantList m_cells;
    QVariantList m_rowBack;
    QVariantList m_rowFore;
};
