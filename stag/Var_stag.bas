Attribute VB_Name = "Var_stag"
Option Explicit
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Costanti per la scrittura dei file di log
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Const MAX_PRIORITY = 0  'priorità massima: un warning con questa priorità termina il programma
Public Const MED_PRIORITY = 5  'priorità ordinaria: per i normali messaggi di warning
Public Const MIN_PRIORITY = 10 'priorità minima
Public Const DSK_DEF_PRIORITY = 5  'livello di priorità dal quale si cominciano a registrare i messaggi sul disco

''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Costanti per i comandi di aggiornamento lista ordini
''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Const OLI_NUL = 0     'nessuna richiesta
Public Const OLI_NEW = 1    'inserimento nuovo ordine
Public Const OLI_REA = 2     'lettura ordine (da codice univoco)
Public Const OLI_GET = 3     'lettura ordine (da indice operatore)
Public Const OLI_MOD = 4    'modifica ordine
Public Const OLI_DLB = 5     'cancellazione blocco ordini da fare
Public Const OLI_MVB = 6    'spostamento blocco ordini da fare
Public Const OLI_VAL = 7    'validazione della lista degli ordini
Public Const OLI_CPE = 8    'copia ordine eseguito
'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
Public Const OLI_NEW_NULL = 9

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Costanti per la comunicazione con il punto unico  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Const STX_PU = 2      'Start of Transmission per protocollo DIGITEC
Public Const ETX_PU = 3      'End of Transmission per protocollo DIGITEC
Public Const ack = "A"  'Acknowledge
Public Const NAK = "N"  'Not Acknowledge

'costanti per il protocollo di comunicazione STANDARD tra punto unico e STAG
Public Const COM_SPDE_LEN_MIN = 5 'lunghezza minima comando

'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
Public Const COM_DEPM_LEN_MIN_SIMON = 1 'lunghezza minima comando
'costanti per il protocollo di comunicazione DIGITEC tra punto unico e STAG
Public Const DIG_COM_SPDE_LEN_LRC = 9 'lunghezza minima comando
Public Const DIG_COM_SPDE_LEN_DAT_LIB = 6 'lunghezza dati liberi
Public Const DIG_COM_SPDE_LEN_COD_COM = 1 'lunghezza codice comando
Public Const DIG_COM_SPDE_LEN_ID = 4 'lunghezza campo spde_id
Public Const SO = 1  'codice comando Set Ordine
Public Const DSO = 2  'codice comando Display Stato Ordine
Public Const DVO = 7  'codice comando Display Vecchio Ordine
Public Const DD = 8  'codice comando Display Diagnostica
Public Const MFP = 9  'codice comando Modifica Fogli da Produrre

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Costanti per la comunicazione con la schedia di taglio  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Const STX_ST = &H2      'Start of Transmission
Public Const EOT_ST = &H4      'End of Transmission
Public Const ACK_ST = &H6  'Acknowledge"
Public Const NAK_ST = &H15  'Not Acknowledge
Public Const WPROTO_LEGGIMEMORIA = 1
Public Const WPROTO_SCRIVIMEMORIA = 2
Public Const WPROTO_LEGGIVETTORE = 3
Public Const WPROTO_SCRIVIVETTORE = 4
Public Const WPROTO_SYN_ST = &H16      'Reset della comunicazione

Public Const COM_SCH_TAG_LEN_BCC = 2
Public Const COM_SCH_TAG_LEN_MIN = 7
Public Const COM_SCH_TAG_LEN_STX = 1

Public Const IND_INI_SCR_PAR = &H3400
Public Const IND_INI_LET_PAR_INF = &H36A0
Public Const IND_INI_LET_PAR_STA = &H36D0
'Iciliouff 20-11-2003
Public Const IND_INI_SCR_DAT_ORD = &H34E0       'Indirizzo di scrittura dati ordine wm70
Public Const IND_INI_LET_ID_ORD = &H36F8        'Indirizzo di lettura ID ordine vm17C (ID ordine successivo)
'Robertouff 26-11-2007
Public Const IND_INI_SCR_PAR_DIV = &H3500       'Indirizzo di scrittura parametri per gruppo di scarto doppio livello


Public Const NUM_PAR_INF = 8
Public Const NUM_WRD_STA = 2
'Icilio3545 04-01-2004 portato a 79
Public Const NUM_PAR_CFG = 79
'Iciliouff 20-11-2003
Public Const NUM_PAR_DAT_ORD = 17
Public Const NUM_WRD_ID_ORD = 2                'Per leggere solo gli ID
'Robertouff 26-11-2007
Public Const NUM_PAR_DIV_AUT = 22

''''''''''''''''''''''''''''''
'Indirizzi Scheda di taglio
''''''''''''''''''''''''''''''
Public Const IND_VEL_SIM_LAP = 0
Public Const IND_VEL_LAM_JOG = 1
Public Const IND_DIS_FOT_LAM = 16
Public Const IND_LUN_TAG_TES = 17
Public Const IND_LUN_TAG_MIN = 18
Public Const IND_LUN_TAG_MAX = 19
Public Const IND_LIM_INF_TOL_TAG = 20
Public Const IND_LIM_SUP_TOL_TAG = 21
Public Const IND_GIR_MOT_LAM_RIF = 22
Public Const IND_VEL_MAX_TAG = 23
Public Const IND_ANG_SINCR = 24
Public Const IND_SOV_LAM = 25
Public Const IND_VEL_ZON_SINC_VMIN = 26
Public Const IND_DIA_RUO_MIS = 27
Public Const IND_DIA_PULL_ROLL = 28
Public Const IND_CIR_LAM = 29
Public Const IND_RAP_RID_MOT_LAM = 30
Public Const IND_IMP_GIR_ENC_RUO_MIS = 31
Public Const IND_IMP_GIR_ENC_PULL_ROLL = 32
Public Const IND_IMP_GIR_ENC_LAM = 33
Public Const IND_FAT_MOL_ENC_RUO_MIS = 34
Public Const IND_FAT_MOL_ENC_PULL_ROLL = 35
Public Const IND_FAT_MOL_ENC_LAM = 36
Public Const IND_TEM_ACC_TGL_LUN = 37
Public Const IND_TEM_DEC_TGL_LUN = 38
Public Const IND_TEM_ACC_TGL_COR = 39
Public Const IND_TEM_DEC_TGL_COR = 40
Public Const IND_OFF_RAM_POS = 41
Public Const IND_OFF_RAM_TAG = 42
Public Const IND_FIN_VEL_OTT_PEN = 43
Public Const IND_FAT_K01_OTT_PEN = 44
Public Const IND_GIR_MAX_MOT_LAM = 45
Public Const IND_KP_NOR = 46
Public Const IND_KI_NOR = 47
Public Const IND_TEM_CAM_STA_VEL = 48
Public Const IND_SOG_ERR_INS = 49
Public Const IND_FIL_KP = 50
Public Const IND_KV_NOR = 51
Public Const IND_LIM_LAV_KV = 52
Public Const IND_OFF_TAG_REG = 64
Public Const IND_FIN_PRI_TAC = 65
Public Const IND_FIN_DOP_TAC = 66
Public Const IND_DIS_FTC_TAC_LAM = 72
Public Const IND_COR_TAG_REG_1°_SOG = 73
Public Const IND_SOG_VEL_COR = 74
Public Const IND_COR_TAG_REG_2°_SOG = 75
'Iciliouff 20-11-2003 Gli indici 76,77 son attualmente liberi
Public Const IND_VEL_FTC_PAT = 78          'Velocità di traslazione FTC (mm/sec X 1000)
Public Const IND_DIS_ASS_SEN_FUO_CAR = 79  'Distanza asse macchina da sensore fuori carter (mm)

'Robertouff 26-11-2007
Public Const IND_SOV_TAP_USC_PER_SCA = 80           'Sovravelocità tappeti uscita durante la fase di scarto (##.#%)
Public Const IND_MIS_SCA_DEF = 81                   'Taglio a default (mm)
Public Const IND_SCA_PRI_GIU = 82                   'Scarto prima della giunta(mm)
Public Const IND_SCA_DOP_GIU = 83                   'Scarto dopo la giunta(mm)
Public Const IND_DIS_SEN_LAM = 84                   'Distanza fra sensore giunta e lame(mm)
Public Const IND_DIS_LAM_FLP = 85                   'Distanza fra lame e inizio Flap di Scarto(mm)
Public Const IND_DIS_LAM_RUL_NO_CRH = 86            'Distanza fra lame e Rullo No Crash Scarto(mm)
Public Const IND_TEM_APE_FLP = 87                   'Tempo apertura flap(msec)
Public Const IND_TEM_CHI_FLP = 88                   'Tempo chiusura flap(msec)
Public Const IND_TEM_APE_RUL_NO_CRH = 89            'Tempo apertura Rullo No-Crash(msec)
Public Const IND_TEM_CHI_RUL_NO_CRH = 90            'Tempo chiusura Rullo No-Crash(msec)
Public Const IND_TEM_SCA_PRE_APE_RUL_NO_CRH = 91    'Tempo scambio pressione apertura Rullo No-Crash(msec)
Public Const IND_TEM_SCA_PRE_CHI_RUL_NO_CRH = 92    'Tempo scambio pressione chiusura Rullo No-Crash(msec)
Public Const IND_MAX_LUN_SCA = 93                   'Massima lunghezza scartabile(mm)
Public Const IND_MIN_LUN_SCA = 94                   'Minima lunghezza scartabile(mm)
Public Const IND_VEL_1 = 95                         'Soglia velocità 1 (m/min)
Public Const IND_LUN_1 = 96                         'Soglia lunghezza taglio 1 (mm)
Public Const IND_VEL_2 = 97                         'Soglia velocità 2 (m/min)
Public Const IND_LUN_2 = 98                         'Soglia lunghezza taglio 2 (mm)
Public Const IND_VEL_3 = 99                         'Soglia velocità 3 (m/min)
Public Const IND_LUN_3 = 100                        'Soglia lunghezza taglio 3 (mm)
'RFAZZI_FI27/03/2008
'Public Const IND_TEM_ATT_CAM_PRE_RUL_NO_CRH = 101   'Tempo attivazione cambio pressione rullo no-crash(msec)
Public Const IND_LUN_SUP_3 = 101                     'Soglia lunghezza taglio superiori soglia 3 (mm)
Public Const IND_TEM_ATT_CAM_PRE_RUL_NO_CRH = 102   'Tempo attivazione cambio pressione rullo no-crash(msec)


'Iciliouff 20-11-2003
''Indici per dati Cut to pattern
'Public Const IND_1°_QUO_PAT = 112           'Quote pattern
'Public Const IND_2°_QUO_PAT = 113
'Public Const IND_3°_QUO_PAT = 114
'Public Const IND_4°_QUO_PAT = 115
'Public Const IND_5°_QUO_PAT = 116
'Public Const IND_6°_QUO_PAT = 117
'Public Const IND_7°_QUO_PAT = 118
'Public Const IND_8°_QUO_PAT = 119
'Public Const IND_9°_QUO_PAT = 120
'Public Const IND_10°_QUO_PAT = 121
'Public Const IND_BIT_CUT_PAT = 122          'Bit per taglio pattern
'Public Const IND_OFF_CUT_PAT = 123          'Offset taglio pattern
'Public Const IND_ID_ORD = 124               'ID ordine (per allineamento STAG - Scheda taglio)
'Public Const IND_FIN_PRI = 125              'Finestra prima del mark/pattern
'Public Const IND_FIN_DOP = 126              'Finestra dopo del mark/pattern

Public Const MAX_TIM_ANT_CHI_FLAP = 500     '[ms] 'Arturo uff 07-03-03
Public Const LUN_SAN_RUL = "470"            '[mm] 'Arturo uff 30-04-03 valore di lunghezza sandwich da impostare quando l'uscita è su rulletti

'Iciliouff 20-11-2003 costanti per il cut to pattern
Public Const MIN_NUM_CTR = 0        'Min numero che può essere impostato come contrasto
Public Const MAX_NUM_CTR = 4        'Max numero che può essere impostato come contrasto
Public Const MIN_LET_FTC = 0        'Min numero che può essere impostato come prima lettura fotocellula
Public Const MAX_LET_FTC = 1        'Max numero che può essere impostato come prima lettura fotocellula
Public Const MIN_LEN_PAT = 0        'Lunghezza min pattern [mm]
Public Const MAX_LEN_PAT = 999      'Lunghezza max pattern [mm]
Public Const NO__ONE = 0            'Ne' cut to mark ne' cut to pattern
Public Const cut_mar = 1            'Impostazione cut to mark
Public Const cut_pat = 2            'Impostazione cut to pattern
Public Const MIN_OFF_PAT = -100     'Offset minimo impostabile nella casellina del offset pattern
Public Const MUL_OFS_CUT_PAT = 1    'Moltiplicatore della lunghezza del foglio per ottenere la massima lunghezza di offset cut pattern
Public Const MUL_FIN_PRI_TAC = 1.5  'Moltiplicatore della lunghezza del foglio per ottenere la massima dimensione della finestra prima della tacca
Public Const MUL_FIN_DOP_TAC = 1.5  'Moltiplicatore della lunghezza del foglio per ottenere la massima dimensione della finestra dopo della tacca


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Costanti per la comunicazione con gli azionamenti        '
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Const HAR_CAN_DONGLE = 1 ' costante per la registrazione nella rete CAN del dispositivo CAN_DONGLE

''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Costanti per i colori
''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Const COL_REPORT = &HC0E0FF
Public Const COL_REPORT1 = &HCFEFFF
Public Const COL1 = &HDBF9E6 'Verde
Public Const COL2 = &HFDECFF 'Rosa
Public Const COL3 = &HCFFEFF 'Giallo
Public Const COL4 = &HCEF0FF 'Marrone chiaro
Public Const COL5 = &HC9FCCC 'Verde più scuro
Public Const COL_MODO = &H80&      'Marrone
Public Const MED_CEL = &HFF0000
Public Const ULTRA_DARK_GREY = &H101010 'nero scuro per l'intestazione delle grilglie degli ordini
Public Const RED_GRU_ALL = &HFF&
Public Const LIGHT_CEL = &HFFFFC0
Public Const LIGHT_BLUE = &HFFC0C0
Public Const COL_SEL_BLUE = &H800000
Public Const LIGHT_GRAY = &HE0E0E0
Public Const MEDIUM_GRAY = &HC0C0C0
Public Const DARK_GRAY = &H808080
Public Const COL_Yellow = &HFFFF&
Public Const LIGHT_Green = &HFF00&
Public Const LIGHT_Red = &H9895FF
Public Const LIGHT_vbyellow = &HC0FFFF
Public Const DARK_Yellow = &HC0C0&
Public Const COL_ALL_Yellow = &HE8E8&
Public Const DARK_BLUE = &HC00000
Public Const COL_BLUE = &HFF8800
Public Const COL_ALL_Red = &HC0&
Public Const COL_DARK_Red = &HA0&
Public Const COL_ALL_Green = &HC000&
Public Const COL_GREEN_ORD = &H808000
Public Const COL_GRAY_ORD = &HDFE0E0
Public Const COL_DARK_Green = &H8000&
Public Const COL_Green = &H8000&
Public Const COL_ALL_pink = &HFF80FF
Public Const COL_FADED_ORANGE = &HCAFF&       'Icilio3545 11-01-2004
'''''''''''''''''''''''''''''''''''''''''''''''''''''
' Costanti per il settaggio della nazionalità
''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Const ITA = 0 'Italia
Public Const ING = 1 'U.K.
Public Const FRA = 2 'Francia

'''''''''''''''''''''''''''''''''''''''''''''''''''''
' Costanti per il controllo dei limiti del valore della soglia abilitazione linea su DC Bus
''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Const LIM_INF_SGL_ABI_LIN_DC_BUS = 200       'Volt 'Arturo uff 09-09-03
Public Const LIM_SUP_SGL_ABI_LIN_DC_BUS = 500       'Volt 'Arturo uff 09-09-03
Public Const VAL_DEF_SGL_ABI_LIN_DC_BUS = "330"     'Volt valore di default 'Arturo uff 09-09-03

'Costanti per la gestione dei numeri
Public Const MAXINT = 32767


''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Costanti utilizzate nella funzione Autoadminlogon
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Const REG_SZ = 1                         ' Unicode nul terminated string

                                  
Const STANDARD_RIGHTS_ALL = &H1F0000
Const KEY_QUERY_VALUE = &H1
Const KEY_SET_VALUE = &H2
Const KEY_CREATE_SUB_KEY = &H4
Const KEY_ENUMERATE_SUB_KEYS = &H8
Const KEY_NOTIFY = &H10
Const KEY_CREATE_LINK = &H20
Const Synchronize = &H100000

Public Const KEY_ALL_ACCESS = ((STANDARD_RIGHTS_ALL Or _
                                  KEY_QUERY_VALUE Or _
                                  KEY_SET_VALUE Or _
                                  KEY_CREATE_SUB_KEY Or _
                                  KEY_ENUMERATE_SUB_KEYS Or _
                                  KEY_NOTIFY Or _
                                  KEY_CREATE_LINK) And _
                                  Not Synchronize)

Public Const DIR_DATI = "\dati\"
' ABorelli_FI09/09/2005_1
Public Const NAM_FIL_PRD = "dat_prd.dat"        ' ABorelli_FI09/09/2005_1

Public Const NAM_FIL_PRD_ESE = "dat_prd_ord.dat"        ' ABorelli_FI09/09/2005_1


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Costanti per la gestione dei file binari
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Const NUM_FIL_LOG = 10         'File di log
Public Const NUM_FIL_BAK = 20          'File backup del file di log
Public Const NUM_FIL_PRO = 30         'File di debug
Public Const NUM_FIL_ORD = 30         'File degli ordini
Public Const NUM_FIL_PRD = 40         'File di produzione       ABorelli_FI09/09/2005_1

Public Const NUM_FIL_PRD_ESE = 50         'File di produzione   ABorelli_FI09/09/2005_1


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Costanti per la gestione degli ordini
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Const MAX_NUM_ORD = 100                             'Massimo numero di ordini in lista
Public Const MAX_NUM_ORD_ESE = 100                   'Massimo numero di ordini eseguiti in lista
Public Const MAX_NUM_ORD_IN_LIS = 5                 'Numero massimo di ordini ricevuti da SPDE presenti nella lista

'Costanti per la formattazione delle stringhe
Public Const FT1 = "0"
Public Const FT2 = "00"
Public Const FT3 = "000"
Public Const FT4 = "0000"
Public Const FT5 = "00000"
Public Const FT6 = "000000"
Public Const FT7 = "0000000"
Public Const FT8 = "00000000"
Public Const FT11 = "00000000000"
Public Const FT12 = "000000000000"
Public Const FT15 = "000000000000000"
Public Const FT30 = "000000000000000000000000000000"
Public Const FT36 = "000000000000000000000000000000000000"

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Costanti per la gestione delle stringhe
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Const STR_TRUE = "1"
Public Const STR_FALSE = "0"

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Costante per il messaggio log nella form di debug
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Const MAX_LEN_TXT = 30000

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Costanti per la gestione dbf
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public db As Database
Public wk As Workspace
Public Const MAX_FREQ_ALL = 500000 '21122001 Stagetti
Public Const NUM_REC_STO = 200
Public Const NUM_REC_CAN = 100
Public Const RIG_ALL = 6
Public Const COL_ALL = 6

Public Const POS_GRU_HAR = 1
Public Const POS_GRU_QE_MST = 2
Public Const POS_GRU_QE_SLV = 3
Public Const POS_GRU_QE_EST = 4
Public Const POS_GRU_BM = 5
Public Const POS_GRU_CON = 6
Public Const POS_GRU_MAL = 7
Public Const NUM_DAT_MEM_ALL = 21

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Costanti per l'abilitazione del cut_to_mark su un ordine
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Const NO = 0
Public Const SI = 1
'Iciliouff 20-11-2003
Public Const PRE_PATTERN = 2

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Costanti per la gestione del timer tim_all
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Const NUM_SEC_MES = 3000

Public Const LEN_DES = 100

''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Costanti per i comandi al PLC
''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Const PLC_NUL = 0          'nessuna richiesta
Public Const PLC_INV_CUR = 1  'richiesta invio ordine corrente
Public Const PLC_INV_SUC = 2  'richiesta invio ordine successivo
Public Const PLC_CAN_COR = 3  'richiesta cancellazione ordine corrente
Public Const PLC_CAN_SUC = 4  'richiesta cancellazione ordine successivo
Public Const PLC_CAN_ALL = 5  'richiesta cancellazione corrente e successivo
Public Const PLC_AFF_LAM = 6  'richiesta affilatura lama
Public Const PLC_LUB_LAM = 7  'richiesta lubrificazione lama
Public Const PLC_MOD_MET = 8  'richiesta modifica metri lavorati dalla lama
Public Const PLC_INV_PAR_KK = 9  'richiesta invio parametri modificabili dall'operatore
Public Const PLC_INV_PRE = 10  'richiesta invio pressione di cordonatura settata a mano
Public Const PLC_INV_PAR_BAS = 11  'richiesta invio settaggi modificabili solo dal tecnico
Public Const PLC_INV_PAR_AOC = 12  'richiesta invio settaggi modificabili solo dal tecnico
Public Const PLC_INV_WEB = 13 'richiesta modifica stecche del web

''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Costanti per la gestione delle unità di misura
''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Const TODECMM = 10
Public Const TOINCHES = 1

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'''''''''''''''''   VARIABILI     STAG  '''''''''''''''''''''''
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public NUM_AZI As Integer
Public NUM_ALL As Integer
Public NUM_GRU_ALL As Integer

'Variabile per disabilitare la ricostruzione del Database delle lingue (avvio rapido del programma)
Public skip_db_lang As Boolean

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Variabili globali per i comandi di aggiornamento lista ordini
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public var_ord_ute As Tip_Com_ord                               'struttura per richiesta operazioni utente sulla lista ordini

'Variabile per l'individuazione di problemi sulla lista ordini
Public Prb_lis_ord As Boolean

'variabile che indica il numero di livelli per ogni ordine nelle liste degli ordini e ordini eseguiti
'nel caso di macchina a 2 livelli si accetta anche un ordine su 3 livelli per implementare il caso di
'un ordine fittizio di scarto
Public num_liv_lis_ord As Integer

'variabile per il numero delle righe della lista ordini
Public num_rig_ord As Integer
'variabile per il numero delle righe della lista ordini eseguiti
Public num_rig_ese As Integer

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public con_SPDE As Long  ' contatore per sapere se SPDE e' vivo
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Iciliouff 03-11-2003
'Public con_sch_tag(1 To 3) As Long  ' contatori per la comunicazione con le schede di taglio
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public com_ini_sch_tag(1 To 3) As Boolean ' variabili booleane per vedere se è avvenuta la comunicazione iniziale dei parametri alla scheda di taglio
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public num_com_sch_tag As Integer 'serve per diversificare il caso di salvataggio di nuovi parametri o la copia da default in attivi su un solo livello ed il caso di copia da default in attivi totale
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public CASO_SND_SCH_TAG As tip_cfg_sch_tag 'serve per identificare il caso di configurazione: carica nuova, modifica, inserimento di DEFAULT, inserimento ATTIVA
Public CASO_RCV_SCH_TAG As tip_ric_sch_tag 'serve per identificare il caso di ricezione: parametri informazione o diagnostica
Public dummy_order_sch_tag As tip_dat_ord_sch_tag
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

'variabili  globali per allarmi
'Public err_num As String 'messaggio di allarme da far apparire (chiave della classe mg)
'Public prg_all(1 To 4) As Integer 'TODO eliminare queste variabili

Public ele_all_tmp As Variant
Public ele_all_mac As Variant
Public ele_all_con As Variant
Public ele_all_har As Variant
Public ele_all_war As Variant
Public ele_all_qe1 As Variant
Public ele_all_qe2 As Variant
Public ele_all_qe3 As Variant

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Stato macchina
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public sta_tag As ClasseStato
'Iciliouff 20-11-2003
Public sta_sch_tag As tip_sta_sch_tag
Public plc_ind As ind_plc
Public par_nuo As par_new

Public pro_set As Tip_pro_set  'contiene le variabili per il programma

Public sta_tag_ute As tip_sta_mac_ute

Public DM As Tip_dm
Public pag_cor As Form
Public pag_pre As Form
Public for_pre_pwd As String
Public cur_tag(1 To 3) As tip_cur_tag
Public vel_max_tag_liv(1 To 3) As Integer
Public par_mac As tip_par_mac
Public par_mac_ute As tip_par_mac        'struttura per memorizzare le modifiche dell'utente
Public par_abi As tip_abi                        'abilitazioni
Public par_abi_ute As tip_abi                        'struttura per memorizzare le modifiche dell'utente
Public mod_in_cor As Boolean                  'modifica in corso (per la memorizzazione nella struttura di appoggio)
Public sal_in_cor As Boolean
Public esc_pre As Boolean

Public ord_mac As CollezioneOrdini
Public ord_mac_ese As CollezioneOrdini

Public Livello As Integer
Public form_att As String
'Iciliouff 29-10-2003 Inserimento/revisione classe Rmt
'Public ist_com_plc As com_plc
Public com_plc_ute(1 To 10) As Tip_Com_PLC       'struttura per i comandi al PLC da parte dell'utente
'Iciliouff 03-11-2003 Gestione richieste operatore
Public rmt_ope_plc As rmt           'Struttura per la gestione delle richieste operatore verso il PLC
Public rmt_ope_sch As rmt           'Struttura per la gestione delle richieste operatore verso le schede di taglio
Public rmt_ope_macro As rmt         'Struttura per la gestione delle richieste operatore che coinvolgono più dispositivi contemporaneamente
Public inv_ord_plc(1 To 2) As Boolean            'Forza il reinvio ordine corrente/successivo a PLC
'Icilio3545 08-01-2004
Public inv_ord_sch_tag(1 To 2) As Boolean        'Pos.Ordine=(1 to 2) Forza invio a schede di taglio
Public old_ind As tip_ind_fins
Public num_con As Integer

Public msg_rul As Boolean   ' indica se è in atto la visualizzazione dei messaggi sulla comunicazione con gli azionamenti
Public new_msg_rul As Boolean   ' indica se è in atto la visualizzazione di nuovi messaggi sulla comunicazione con gli azionamenti
Public imp_nuo_par As Boolean       ' indica se ho impostato un nuovo parametro nella form dia_par_new

Public str_msg_dia_CAN As String * 1000  ' stringa per contenere il messaggio di diagnosi della rete CAN
Public str_msg_ver_CAN As String * 300  ' stringa per contenere il messaggio di diagnosi della rete CAN

'variabili per i livelli
Public LIV_BAS As Integer
Public LIV_MED As Integer
Public LIV_ALT As Integer

Public Const plcAreaDM As Long = &H820000
