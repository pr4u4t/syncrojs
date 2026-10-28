#pragma once

#include "vbcompat.h"

#include <QByteArray>
#include <QString>
#include <QVariant>
#include <QVector>

// Reader for the dBase III tables STAG accessed through DAO (no memo fields are used).
class DbfTable
{
public:
    struct Field {
        QString name;
        char type = 'C';   // C, N, F, L, D
        int length = 0;
        int decimals = 0;
    };

    bool load(const QString &path, QString *error = nullptr);

    const QVector<Field> &fields() const { return m_fields; }
    int recordCount() const { return m_records.size(); }
    bool isDeleted(int record) const;
    // C: text without trailing spaces (as DAO returns it); N/F: double or null; L: bool or null; D: QDate or null.
    QVariant value(int record, int field) const;
    QString rawText(int record, int field) const;
    void setCodePage(int field, vb::OemCodePage codePage) { m_codePages[field] = codePage; }

private:
    QVector<Field> m_fields;
    QVector<int> m_offsets;
    QVector<vb::OemCodePage> m_codePages;
    QVector<QByteArray> m_records;
};
