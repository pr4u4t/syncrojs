#pragma once

#include "fixedrecord.h"

#include <memory>

// Layouts of the VB6 types STAG stores on disk (Tip_stag.bas, Task_TAG_ver_all.bas).
// Field names keep the original VB names so the port can be traced line by line.
namespace stag {

constexpr int kOrderRecordSize = 702;           // Tip_Ordine, Ord_mac.dat / Ord_mac_ese.dat
constexpr int kOrderFileRecords = 100;          // MAX_NUM_ORD / MAX_NUM_ORD_ESE
constexpr int kShiftProductionSize = 364;       // tip_pro_tur_gio_fosber21_FOS, dat_prd.dat
constexpr int kOrderProductionSize = 18;        // tot_pro_ese, dat_prd_ord.dat

std::shared_ptr<const RecordLayout> orderLayout();
std::shared_ptr<const RecordLayout> shiftProductionLayout();
std::shared_ptr<const RecordLayout> orderProductionLayout();

// Tip_Ordine: one setup ("abbinamento") with up to three formats ord(1..3).
class OrderRecord : public FixedRecord
{
public:
    OrderRecord();
    explicit OrderRecord(const QByteArray &raw);

    // CollezioneOrdini.Leg_dat_ord keeps a record only when Val(stag_id) <> 0.
    bool isUsed() const;
};

} // namespace stag
