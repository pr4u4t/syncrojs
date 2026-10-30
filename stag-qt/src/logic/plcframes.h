#pragma once

#include "collezioneordini.h"

#include <QString>
#include <QVector>

#include <optional>

// Omron_PLC.snd_str_PLC without the sending: the words written from DM3500 on. Kept apart from the
// FINS client so every frame can be compared with the snd_str_PLC lines of the machine's Plc.log.
namespace stag {

struct PlcFrame {
    QVector<QString> dm;

    // The hex string VB logged and wrote.
    QString text() const { return dm.join(QString()); }
    QVector<qint32> words() const;
    QString logLine() const;
};

enum class OrderOperation { Send, Modify };

// The order frames need ord_mac only for the order at that position and the following one.
// nullopt where VB's error handler left snd_str_PLC without writing anything (Sca_per_tag = 0).
std::optional<PlcFrame> frameOrder(OrderOperation operation, int indice, const ClasseOrdine &order, const ClasseOrdine *next);
PlcFrame frameDeleteOrder(int indice);
PlcFrame frameParMac();
PlcFrame frameParCfgMac();
PlcFrame frameCurTag(int liv);
PlcFrame frameResetCounters();
PlcFrame frameConfirmCutBoards();

} // namespace stag
