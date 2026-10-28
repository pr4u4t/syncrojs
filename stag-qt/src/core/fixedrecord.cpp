#include "fixedrecord.h"

#include "vbcompat.h"

#include <stdexcept>

void RecordLayout::add(const QString &name, int length)
{
    if (m_index.contains(name))
        throw std::logic_error("duplicate record field: " + name.toStdString());
    m_index.insert(name, m_fields.size());
    m_fields.append({name, m_size, length});
    m_size += length;
}

void RecordLayout::addGroup(const QString &name, const RecordLayout &group, int count, int firstIndex)
{
    const auto appendGroup = [this, &group](const QString &prefix) {
        for (const Field &field : group.fields())
            add(prefix + u'.' + field.name, field.length);
    };
    if (count == 0) {
        appendGroup(name);
        return;
    }
    for (int index = firstIndex; index < firstIndex + count; ++index)
        appendGroup(QStringLiteral("%1(%2)").arg(name).arg(index));
}

const RecordLayout::Field *RecordLayout::field(const QString &name) const
{
    const auto found = m_index.constFind(name);
    return found == m_index.constEnd() ? nullptr : &m_fields.at(found.value());
}

FixedRecord::FixedRecord(std::shared_ptr<const RecordLayout> layout)
    : m_layout(std::move(layout))
    , m_raw(m_layout->size(), '\0')
{
}

FixedRecord::FixedRecord(std::shared_ptr<const RecordLayout> layout, const QByteArray &raw)
    : FixedRecord(std::move(layout))
{
    setRaw(raw);
}

void FixedRecord::setRaw(const QByteArray &raw)
{
    // VB Get past the end of the file leaves the missing part as NUL.
    m_raw = raw.left(m_layout->size());
    if (m_raw.size() < m_layout->size())
        m_raw.append(QByteArray(m_layout->size() - m_raw.size(), '\0'));
}

const RecordLayout::Field &FixedRecord::requireField(const QString &name) const
{
    const RecordLayout::Field *field = m_layout->field(name);
    if (!field)
        throw std::out_of_range("unknown record field: " + name.toStdString());
    return *field;
}

QString FixedRecord::text(const QString &field) const
{
    const RecordLayout::Field &definition = requireField(field);
    return vb::fromAnsi(m_raw.mid(definition.offset, definition.length));
}

void FixedRecord::setText(const QString &field, const QString &value)
{
    const RecordLayout::Field &definition = requireField(field);
    m_raw.replace(definition.offset, definition.length, vb::toAnsi(vb::fixedLength(value, definition.length)));
}

double FixedRecord::number(const QString &field) const
{
    return vb::val(text(field));
}

bool FixedRecord::isNull() const
{
    return m_raw.count('\0') == m_raw.size();
}
