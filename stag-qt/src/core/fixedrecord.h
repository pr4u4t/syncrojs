#pragma once

#include <QByteArray>
#include <QHash>
#include <QString>
#include <QVector>

#include <memory>

// Byte layout of a VB6 user-defined type made only of "String * n" fields,
// as written by Put/Get on a file opened For Random.
class RecordLayout
{
public:
    struct Field {
        QString name;
        int offset = 0;
        int length = 0;
    };

    void add(const QString &name, int length);
    // Embeds another layout; VB arrays "x(1 To n)" become "x(1).field" ... "x(n).field".
    void addGroup(const QString &name, const RecordLayout &group, int count = 0, int firstIndex = 1);

    int size() const { return m_size; }
    const QVector<Field> &fields() const { return m_fields; }
    const Field *field(const QString &name) const;

private:
    QVector<Field> m_fields;
    QHash<QString, int> m_index;
    int m_size = 0;
};

class FixedRecord
{
public:
    explicit FixedRecord(std::shared_ptr<const RecordLayout> layout);
    FixedRecord(std::shared_ptr<const RecordLayout> layout, const QByteArray &raw);

    const RecordLayout &layout() const { return *m_layout; }
    const QByteArray &raw() const { return m_raw; }
    void setRaw(const QByteArray &raw);

    // Field content exactly as VB sees it (padding and NUL bytes included).
    QString text(const QString &field) const;
    // VB assignment semantics: padded with spaces or truncated to the field length.
    void setText(const QString &field, const QString &value);
    // Val() of the field.
    double number(const QString &field) const;

    // A record never written by STAG is all NUL bytes.
    bool isNull() const;

private:
    const RecordLayout::Field &requireField(const QString &name) const;

    std::shared_ptr<const RecordLayout> m_layout;
    QByteArray m_raw;
};
