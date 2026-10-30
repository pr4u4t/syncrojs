#pragma once

#include "collezioneordini.h"
#include "recordeditmodel.h"

// Onde.frm: the table of flute and paper combinations.
class OndeModel : public RecordEditModel
{
    Q_OBJECT

public:
    OndeModel(CollezioneOrdini *orders, const QSqlDatabase &db, QObject *parent = nullptr);

    void load();
    void activate(const QString &previousForm) override;
    bool functionKey(const QString &key, int shift) override;

protected:
    QStringList gridHeaders() const override;
    bool validateField(const QString &field) override;
    void fieldChanged(const QString &name, const QString &value) override;

private:
    bool usedByAnOrder(bool trimmedKey) const;
    void newRecord();
    void editRecord();
    void deleteRecord();
    void save();
    void abort();

    CollezioneOrdini *m_orders;
    bool m_nuoOnd = false;
    QString m_ond;
};
