#pragma once

#include <QByteArray>
#include <QString>
#include <QVector>

// Files written with VB "Open ... For Random ... Len = n": fixed-size records, no header.
namespace recordfile {

// Reads `count` records of `recordSize` bytes; missing bytes past the end read as NUL like VB Get.
QVector<QByteArray> read(const QString &path, int recordSize, int count, QString *error = nullptr);

// Writes all records (each must be exactly recordSize bytes) atomically.
bool write(const QString &path, const QVector<QByteArray> &records, int recordSize, QString *error = nullptr);

} // namespace recordfile
