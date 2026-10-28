#include "recordfile.h"

#include <QFile>
#include <QSaveFile>

namespace recordfile {

QVector<QByteArray> read(const QString &path, int recordSize, int count, QString *error)
{
    QFile file(path);
    if (!file.open(QIODevice::ReadOnly)) {
        if (error)
            *error = file.errorString();
        return {};
    }
    const QByteArray data = file.readAll();
    QVector<QByteArray> records;
    records.reserve(count);
    for (int index = 0; index < count; ++index) {
        QByteArray record = data.mid(qsizetype(index) * recordSize, recordSize);
        if (record.size() < recordSize)
            record.append(QByteArray(recordSize - record.size(), '\0'));
        records.append(record);
    }
    return records;
}

bool write(const QString &path, const QVector<QByteArray> &records, int recordSize, QString *error)
{
    QSaveFile file(path);
    if (!file.open(QIODevice::WriteOnly)) {
        if (error)
            *error = file.errorString();
        return false;
    }
    for (const QByteArray &record : records) {
        if (record.size() != recordSize) {
            if (error)
                *error = QStringLiteral("record of %1 bytes, expected %2").arg(record.size()).arg(recordSize);
            file.cancelWriting();
            return false;
        }
        file.write(record);
    }
    if (!file.commit()) {
        if (error)
            *error = file.errorString();
        return false;
    }
    return true;
}

} // namespace recordfile
