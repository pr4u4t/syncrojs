#pragma once

#include "collezioneordini.h"

#include <QString>

#include <array>

// com_sch_di_tag_bas without the sending: the WProto frames towards the cut boards. Kept apart from the
// socket so every frame can be compared with the machine's Sch_tag.log.
namespace stag {

// tip_cmd
enum TipCmd { CMD_NIET = 0, CONFIGURA, LETTURA_STATO, INFORMAZIONI, INVIO_DATI_ORDINE, SELEZIONE_CONTRASTO, LETTURA_STATO_ORDINE };
// tip_cfg_sch_tag
enum TipCfgSchTag { CARICA_NEW_CFG = 1, MODIFICA_OLD_CFG, INSERIMENTO_CFG_DEFAULT, INSERIMENTO_CFG_ATTIVA, NOT_CFG, CAMBIO_STATO_MACCHINA, INVIO_INIZIALE_PAR };

constexpr int IND_INI_SCR_PAR = 0x3400;
constexpr int IND_INI_SCR_DAT_ORD = 0x34E0;
constexpr int IND_INI_SCR_PAR_DIV = 0x3500;
constexpr int IND_INI_LET_PAR_INF = 0x36A0;
constexpr int IND_INI_LET_PAR_STA = 0x36D0;
constexpr int IND_INI_LET_ID_ORD = 0x36F8;
constexpr int NUM_PAR_INF = 8;
constexpr int NUM_WRD_STA = 2;
constexpr int NUM_WRD_ID_ORD = 2;

constexpr char SYN_ST = 0x16;
constexpr char STX_ST = 0x02;
constexpr char EOT_ST = 0x04;
constexpr char ACK_ST = 0x06;
constexpr char NAK_ST = 0x15;

// CHK_SUM: sum of the characters after STX, last two hex digits.
QString boardChecksum(const QString &withStx);
// snd_str_sch_tag: the whole frame, STX to EOT.
QString boardReadVector(int address, int words);
QString boardWriteVector(int address, const QString &payload);

// tip_dat_ord_sch_tag as the hex text VB copied from the record.
QString boardOrderDummy(const QString &id);
QString boardOrder(const ClasseOrdine &order, int i);
QString boardContrast(int contrast);
// CONFIGURA: arr_par 0..79 for IND_INI_SCR_PAR and 80..102 for IND_INI_SCR_PAR_DIV.
std::array<QString, 2> boardConfig(int liv, const ClasseOrdine *current);

} // namespace stag
