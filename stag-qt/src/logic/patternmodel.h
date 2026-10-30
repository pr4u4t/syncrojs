#pragma once

#include "collezioneordini.h"
#include "recordeditmodel.h"

// Pattern.frm: the cut-to-pattern codes. Saving copies the values into the orders that use the code.
class PatternModel : public RecordEditModel
{
    Q_OBJECT

public:
    PatternModel(CollezioneOrdini *orders, const QSqlDatabase &db, QObject *parent = nullptr);

    void load();
    void activate(const QString &previousForm) override;
    bool functionKey(const QString &key, int shift) override;

protected:
    QStringList gridHeaders() const override;
    bool validateField(const QString &field) override;

private:
    bool usedByAnOrder(const QString &code) const;
    bool errMisPat();
    void newRecord();
    void editRecord();
    void deleteRecord();
    void save();
    void abort();
    void copyToOrders();

    CollezioneOrdini *m_orders;
    bool m_nuoPat = false;
};
