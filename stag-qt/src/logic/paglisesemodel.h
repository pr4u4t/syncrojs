#pragma once

#include "orderlistmodel.h"

// pag_lis_ese.frm: the executed orders and their production report.
class PagLisEseModel : public OrderListModel
{
    Q_OBJECT

public:
    explicit PagLisEseModel(CollezioneOrdini *orders, QObject *parent = nullptr);

    // Form_Load
    void load();
    void activate(const QString &previousForm) override;
    bool functionKey(const QString &key, int shift) override;
    QStringList hiddenFunctionKeys() const override;

    bool reportVisible() const { return m_reportVisible; }

protected:
    void adjustWindow(int count) override;
    void showPanel(int order) override;

private:
    bool m_reportVisible = false;
};
