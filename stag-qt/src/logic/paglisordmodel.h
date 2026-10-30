#pragma once

#include "orderlistmodel.h"

// pag_lis_ord.frm: the list of orders to run. Reading and scrolling only; delete, move, copy and
// find open their own dialogs.
class PagLisOrdModel : public OrderListModel
{
    Q_OBJECT

public:
    explicit PagLisOrdModel(CollezioneOrdini *orders, QObject *parent = nullptr);

    // Form_Load
    void load();
    void activate(const QString &previousForm) override;
    bool functionKey(const QString &key, int shift) override;

    bool detailsVisible() const { return m_detailsVisible; }

protected:
    void adjustWindow(int count) override;
    void showPanel(int order) override;

private:
    bool m_detailsVisible = false;
    int m_oldCount = 0;
    int m_oldPrimo = 0;
    int m_oldUltimo = 0;
};
