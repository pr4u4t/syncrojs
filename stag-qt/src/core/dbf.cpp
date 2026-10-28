#include "dbf.h"

#include "vbcompat.h"

#include <QDate>
#include <QFile>
#include <QtEndian>

bool DbfTable::load(const QString &path, QString *error)
{
    const auto fail = [error](const QString &message) {
        if (error)
            *error = message;
        return false;
    };

    m_fields.clear();
    m_offsets.clear();
    m_codePages.clear();
    m_records.clear();

    QFile file(path);
    if (!file.open(QIODevice::ReadOnly))
        return fail(file.errorString());
    const QByteArray data = file.readAll();
    if (data.size() < 33)
        return fail(QStringLiteral("file too short for a dBase header"));

    const auto *bytes = reinterpret_cast<const uchar *>(data.constData());
    const quint32 count = qFromLittleEndian<quint32>(bytes + 4);
    const quint16 headerLength = qFromLittleEndian<quint16>(bytes + 8);
    const quint16 recordLength = qFromLittleEndian<quint16>(bytes + 10);

    int offset = 1; // deletion flag
    for (int position = 32; position + 32 <= headerLength && bytes[position] != 0x0D; position += 32) {
        Field field;
        field.name = QString::fromLatin1(reinterpret_cast<const char *>(bytes + position), qstrnlen(reinterpret_cast<const char *>(bytes + position), 11));
        field.type = static_cast<char>(bytes[position + 11]);
        field.length = bytes[position + 16];
        field.decimals = bytes[position + 17];
        if (field.type == 'M')
            return fail(QStringLiteral("memo field %1 is not supported").arg(field.name));
        m_fields.append(field);
        m_offsets.append(offset);
        m_codePages.append(vb::OemCodePage::Cp850);
        offset += field.length;
    }
    if (offset != recordLength)
        return fail(QStringLiteral("field lengths (%1) do not match the record length (%2)").arg(offset).arg(recordLength));
    if (qsizetype(headerLength) + qsizetype(count) * recordLength > data.size())
        return fail(QStringLiteral("file is shorter than %1 records").arg(count));

    m_records.reserve(int(count));
    for (quint32 index = 0; index < count; ++index)
        m_records.append(data.mid(headerLength + qsizetype(index) * recordLength, recordLength));
    return true;
}

bool DbfTable::isDeleted(int record) const
{
    return m_records.at(record).at(0) == '*';
}

QString DbfTable::rawText(int record, int field) const
{
    return vb::fromOem(m_records.at(record).mid(m_offsets.at(field), m_fields.at(field).length), m_codePages.at(field));
}

QVariant DbfTable::value(int record, int field) const
{
    const QString text = rawText(record, field);
    switch (m_fields.at(field).type) {
    case 'N':
    case 'F': {
        const QString trimmed = text.trimmed();
        bool ok = false;
        const double number = trimmed.toDouble(&ok);
        return ok ? QVariant(number) : QVariant();
    }
    case 'L': {
        const QChar flag = text.trimmed().isEmpty() ? QChar(u'?') : text.trimmed().at(0).toUpper();
        if (flag == u'T' || flag == u'Y')
            return true;
        if (flag == u'F' || flag == u'N')
            return false;
        return {};
    }
    case 'D': {
        const QDate date = QDate::fromString(text.trimmed(), QStringLiteral("yyyyMMdd"));
        return date.isValid() ? QVariant(date) : QVariant();
    }
    default: {
        qsizetype end = text.size();
        while (end > 0 && text.at(end - 1) == u' ')
            --end;
        return text.left(end);
    }
    }
}
