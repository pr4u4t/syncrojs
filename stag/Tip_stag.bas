Attribute VB_Name = "Tip_stag"
Option Explicit
'STRUTTURE

' tipo parametri per i files log
Type Tip_log
  Nam_fil_log As String              'Nome del file di log
  Nam_fil_bak As String             'Nome del file di backup del file di log
  MAX_LEN_LOG As Long             'Massima lunghezza del file di log
  Dsk_priority As Integer            'Livello di priorità dal quale si cominciano a registrare i messaggi sul disco
End Type

Enum cos_log
  log_plc = 1     ' log trasmissioni con il PLC ( basso livello )
  log_ord         ' log sugli ordini
  log_spde        ' log SPDE
  log_rmt         ' log comandi remoti
  log_gen         ' log generico
  log_lis         ' log lista ordini
  log_prob      ' per problemi da verificare
  log_can       ' per problemi con la rete can
  log_cfg       'per problemi sul salvataggio, lettura, set e reset di configurazioni per la scheda di taglio
  log_cur       'per problemi sul salvataggio, lettura, set e reset di configurazioni per la curva di taglio
  log_sch_tag 'per problemi di comunicazione con le schede di taglio
  log_azi        'per problemi sulla collezione dei codici errori per gli azionamenti
  log_eve         ' LASCIARE ULTIMO log per scambio eventi

  ' per aggiungere voci, modificare anche in leg_par_sup la choose
End Enum

Enum tip_ope_PLC
  INVIO_ORD = 1                     'invio dell'ordine
  CANC_ORD                          'cancellazione ordine
  MODIF_ORD                         'modifica ordine
  PARAM_MAC                         'parametri macchina
  PARAM_CFG_MAC                     'parametri di configurazione della macchina: abilitazioni, selezione uscita ....ecc.
  PARAM_CUR_TAG                     'parametri per la curva di taglio
  LETTURA_PAR_SCH_TAG               'lettura parametri schede di taglio
  SCRITTURA_PAR_SCH_TAG             'scrittura parametri schede di taglio
  AZZERAMENTO_CONTATORI             'richiesta azzeramento dei contatori: scatole e scarti
  CONFERMA_DATI_SCHEDE_TAGLIO       'comando per rendere attivi i dati su scheda di taglio
End Enum

Enum tip_ope_ord_PLC
  CORRENTE = 1
  SUCCESSIVO = 2
End Enum

Enum tip_ope
  Modifica
  Inserimento
  Nessuna
End Enum

' tipo parametri programma
Type Tip_pro_set
  pro_pat As String                   'PERCORSO DEL PROGRAMMA
  Nam_fil_uni As String                                  'Nome del file che tiene traccia del codice univoco
  Nam_fil_dat_ord As String                          'Nome del file della lista degli ordini
  Nam_fil_dat_ord_ese As String                 'Nome del file della lista degli ordini eseguiti
  lin_att As Integer                  'variabile per il settaggio della nazionalità
  ute_wrk As Boolean              'True => sono abilitati tutti i settaggi riservati
  abi_reg_rep_pro As String  'abilitazione alla registrazione dei report di produzione
  ini_prod As String              '1 = indica che deve ancora essere fatto il primo C.O. dopo l'inizio della produzione, 0 = indica il contrario
  pwd As String                       'password corrente (letta da STAG.INI)
  ric_msg_db As Boolean        'indica se devo ricostruire il DB dei messaggi in caso di errore
  Log(1 To cos_log.log_eve) As Tip_log                        'Struttura per i file di log
  PercX As Integer          'variabile per il dimensionamento degli oggetti in funzione della risoluzione grafica
  PercY As Integer          'variabile per il dimensionamento degli oggetti in funzione della risoluzione grafica
  ute_wrk_liv1  As Boolean  'True => abilitazione alla'accesso alle pagine ingegneristiche
  pwd_liv1 As String        ' 5122001 Stagetti password per accesso alla visualizzazione delle pagine ingegneristiche
  per_cli As String 'Per personalizzazione cliente
  tur As Integer            ' ABorelli_FI09/09/2005_1  turno (1,2 o 3)
  'IDisperati_FI 21/08/2006
  JOB As String                 'Numero Job per il cliente in oggetto
  Commessa As String            'Numero Commessa per SPDE AOC 30.20 in oggetto
  pro_bck_loc_zip As String     'Percorso di archiviazione locale per i file ZIP (COD,DAT,LOG)
  pro_bck_rem_zip As String     'Percorso di archiviazione remoto per i file ZIP (COD,DAT,LOG)
  nam_fil_log_zip As String     'Nome del file zip per i Log      (LOG)
  nam_fil_cod_zip As String     'Nome del file zip per il Codice  (COD)
  nam_fil_dat_zip As String     'Nome del file zip per i Dati     (DAT)
End Type

' tipo data memory
Type Tip_dm
  DM(1 To 100) As String * 4
End Type

' tipo caratteristiche macchina
Type tip_car_tec
  num_liv_tag As String * 1                       'numero livelli di taglio
  num_mot_liv_tag As String * 1                 'numero motori per livello di taglio
  for_mac As tip_for_mac                           'formato della maccina
  gra_car As tip_gra_car                        'grammatura max del cartone 'Arturo uff 05-11-02
  nor_mac As TIP_NOR_MAC                        'tipo di normativa (CE, UL ..) 'Arturo uff 06-03-03
  min_for_mac As String * 4                        'minimo formato della macchina
  min_lun_tag As String * 4                        'minima lunghezza di taglio
  max_lun_tag As String * 4                      'massima lunghezza di taglio
  lun_tag_sca As String * 4                        'lunghezza taglio di scarto
  'RFAZZI_FI15/06/2009. Aggiunto parametro di Lunghezza di Taglio a Default differenziata in caso di Cut to Patter Automatico e Gruppo di Scarto
  lun_tag_sca_pat_aut As String * 4                        'lunghezza taglio di scarto con Cut to Pattern Automatico + Gruppo di Scarto
  pre_all As String * 3                                 'preallarme
  sel_usc_liv(1 To 3) As String * 1               'selezione uscita per livello: Sandwich o Rulletti
  pre_con_pom(1 To 3) As String * 1              'presenza controllo pompa
  sgl_abi_lin_DC_Bus As String * 3              'soglia abilitazione linea su DC Bus 'Arturo uff 09-09-03
  tip_azi_tag As String * 1                     ' LENZE o TDEMACNO  ' ABorelli_FI11/11/2005_1
End Type

' tipo optional
Type tip_opt
  cut_mar As String * 1                                 'abilitazione cut_to_mark
  'agrassi_FI15-07-08
  'cut_pat As String * 1                                 'abilitazione cut to pattern Iciliouff 20-11-2003
  cut_pat(1 To 3) As String * 1                                 'abilitazione cut to pattern Iciliouff 20-11-2003
  sca_liv(1 To 3) As String * 1                         'scarto livello
  'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
  'cut_pat_auto As String * 1                            'abilitazione cut to pattern automatico Robertouff 26-11-2007
  cut_pat_auto(1 To 3) As String * 1                              'abilitazione cut to pattern automatico Robertouff 26-11-2007
End Type

' tipo porta
Type tip_por
  porta As String * 1                                         ' n. porta
  baud As String * 5                                         'baud rate
  bit_par As String * 4                                      ' parita' (E,N,O)
  dati As String * 1                                           ' n. bit dati
  bit_stop As String * 3                                  ' n. bit stop
  TimeOut As String * 5                                     ' timeout
  com_att As Boolean                                         ' per vedere se è attiva la comunicazione con la scheda di taglio
  ricevuto As Boolean                                        ' per verificare se ho ricevuto qualcosa (un ACK o un NAK) dalla scheda di taglio
End Type

'tipo per l'indirizzo FinsGateway
Type tip_ind_fins
  net_id As Integer
  node_id As Integer
  unit_id As Integer
  TIMER_SCANS_PLC As Integer
End Type

'tipo di comunicazione per i socket
Public Enum tip_soc
  PEER      'comunicazione peer to peer
  SERVER  'server socket
  CLIENT  'client socket
End Enum

'tipo per la comunicazione su rete
Type tip_lan
  por_loc As String * 6                                   'indirizzo porta locale
  por_rem As String * 6                                  'indirizzo porta remota
  ind_rem As String                                        'indirizzo remoto
  tip As tip_soc                                             'tipo di comunicazione per i socket
End Type

Enum tip_param_errati_su_com
  SCHEDE_DI_TAGLIO = 1
  PUNTO_UNICO
  NESSUN_PROBLEMA
End Enum

'tipo comunicazione con spde
Type tip_com_spde
  ver_spde As tip_SPDE                                'versione SPDE
  con As Control                                            'controllo associaato alla comunicazione
  tip_com As String * 1                                 'tipo comunicazione   0 seriale  -  1 ethernet
  tip_porta As tip_por                                     'tipo porta seriale
  lan As tip_lan                                               'tipo comunicazione su rete
  rx As String                                               'buffer ricezione
  ult_tx As String                                          'ultima trasmissione
  tem_ult_tx As Long                                      'tempo ultima trasmissione
  err As Integer                                              'contatore errori sulla porta
  err_max As String * 2                                  'errore massimo: numero di tentativi di comunicazione
  TimeOut As String * 5
End Type

' tipo per la versione SPDE
Enum tip_SPDE      ' tipi di punto unico (11 è il protocollo esteso di 10)
  Fosber10 = 1
  'Fosber11 = 2
  Fosber20 = 2 '30112001 Stagetti
  'Escada10 = 4
  'Escada11 = 5
  Fosber21 = 3     'Iciliouff 20-11-2003 Aggiunto protocollo cut to pattern
  Fosber30 = 4  'Robertouff 26-11-2007
  Simon10 = 5   'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
  MHI10 = 6     'PMASSAROFI_05/07/11 - Aggiunta ricezione ordini da MHI via BCD
End Enum

Enum tip_for_mac
  mm1800 = 1
  mm2500 = 2
  mm2800 = 3
End Enum

Enum tip_gra_car  'Arturo uff 05-11-02
  gm2_1800 = 0
  gm2_2600 = 1
End Enum

Enum TIP_NOR_MAC  'Arturo uff 06-03-03
  CE = 0          'Normative europee
  UL = 1          'Normative americane
End Enum

Enum tip_cmd  'comando di lettura o scrittura per la scheda di taglio
  NIET = 0
  CONFIGURA = 1
  LETTURA_STATO = 2
  INFORMAZIONI = 3
  'Iciliouff 20-11-2003
  INVIO_DATI_ORDINE = 4         'I dati relativi all'ordine sono quelli per il taglio a registro (mark o pattern)
  SELEZIONE_CONTRASTO = 5       'Comando utilizzato in fase di acquisizione/memorizzazione contrasto da fotocellula
  LETTURA_STATO_ORDINE = 6      'Comando per acquisire i dati ordine presenti nelle schede di taglio (e ri-allinearli se necessario)
End Enum

Enum tip_cfg_sch_tag
  CARICA_NEW_CFG = 1
  MODIFICA_OLD_CFG = 2
  INSERIMENTO_CFG_DEFAULT = 3
  INSERIMENTO_CFG_ATTIVA = 4
  NOT_CFG = 5
  CAMBIO_STATO_MACCHINA = 6
  INVIO_INIZIALE_PAR = 7
  NESSUNO = 8
End Enum

Enum tip_ric_sch_tag
  NIET = 0
  DIAGNOST = 1
  PARAM_INFORM = 2
  'Iciliouff 20-11-2003
  STATO_ORDINE = 3
End Enum

'Iciliouff 03-11-2003
Enum tip_ope_macro
  PLC_PARAM_CFG_MAC_PARAM_CUR_TAG   'Aggiorna cfg macchina (abilitazioni, selezione uscita etc.) + Aggiorna curva di taglio (se è possibile trovarla nel dbf) x livello indicato
  PLC_PARAM_MAC_PARAM_CFG_MAC
  'Icilio3545 04-01-2004
  ACQ_CON_SCH_PLC                   'Invia un contrasto alla scheda di taglio di un livello e lo rende attivo in macchina con il comando di conferma verso PLC
End Enum

Enum cod_err_macro
  NIET = 0
  ROLL_BACK_PLC
  ROLL_BACK_SCH
  ROLL_BACK_AZM
End Enum

'Robertouff 26-11-2007
Type tip_par_cfg
  'arr_par(0 To 79) As String * 4
  'RFAZZI_FI27/03/2008
  arr_par(0 To 102) As String * 4
End Type

'Iciliouff 20-11-2003
Type tip_dat_ord_sch_tag
  L1 As String * 4                            'Quote tacche
  L2 As String * 4
  L3 As String * 4
  L4 As String * 4
  L5 As String * 4
  L6 As String * 4
  L7 As String * 4
  L8 As String * 4
  L9 As String * 4
  L10 As String * 4
  bit_cut_reg As String * 4                   'Bit utilizzati dal cut to registry (pattern o mark)
    'Bit 0  - Colore inizio pattern 1=nero 0=bianco
    'Bit 1  - Abilita pattern ("decodifica fotocellula")
    'Bit 2  - Seleziona background A
    'Bit 3  - Seleziona background B
    'Bit 4  - libero
    'Bit 5  - libero
    'Bit 6  - libero
    'Bit 7  - libero
    'Bit 8  - libero
    'Bit 9  - libero
    'Bit 10 - libero
  Ofs_Cut As String * 4                       'Offset per centraggio taglio su pattern
  ID_ord As String * 4                        'Id ordine inviato (stag_id)
  libero As String * 4
  fin_pri As String * 4                       'Finestra prima della tacca/pattern
  fin_dop As String * 4                       'Finestra dopo la tacca/pattern
End Type

' tipo parametri scheda di taglio per singolo livello
Type tip_sch_tag_liv
  dia_ruo_mis As String * 8                          'diametro delle ruota di misura
  vel_lam_jog As String * 3                           'velocità lame in jog
  imp_gir_enc_ruo_mis As String * 4           'impulsi per giro dell'encoder ruota di misura
  dis_fot_lam As String * 8                             'distanza tra fotocellula e lame
  dia_pull_roll As String * 8                            'diametro pull_roll
  lun_tag_tes As String * 8                            'lunghezza taglio testa
  sov_lam As String * 3                                  ' % sovravelocità lame
  dis_FTC_tac_lam As String * 8                  'distanza FTC tacche-lama
  'gir_max_mot_lam As String * 4                   'giri massimi motore lame 'Arturo uff 21-02-03 spostato nella struttura tip_par_cfg_sch_tag_liv perchè il dato viene preso dal db cfg_mac invece che da stag.ini
  imp_gir_enc_lam As String * 4                   'impulsi per giro dell'encoder lame
  imp_gir_enc_pull_roll As String * 4            'impulsi per giro dell'encoder pull_roll
  fat_mol_enc_lam As String * 1                  'fattore moltiplicativo encoder lame
  fat_mol_enc_ruo_mis As String * 1           'fattore moltiplicativo encoder ruota di misura
  fat_mol_enc_pull_roll As String * 1            'fattore moltiplicativo encoder pull_roll
  lim_inf_tol_tag As String * 4                       'limite inferiore tolleranza di taglio
  lim_sup_tol_tag As String * 4                       'limite superiore tolleranza di taglio
  rap_rid_mot_lam As String * 5                   'rapporto di riduzione tra motore e lame
  cir_lam As String * 4                                  'circonferenza lame
  ang_sincr As String * 3                             'angolo di sincronismo
  vel_zon_sinc_vmin As String * 5             '% sovravelocità in zona sinc. alla velocità minima
  fin_vel_ott_pen As String * 3                    'finestra di velocità per ottimizzazione pendenze
  fat_k01_ott_pen As String * 3                   'fattore K01 per ottimizzazione pendenze
  off_ram_pos As String * 3                         'offset rampa di posizione
  off_ram_tag As String * 3                          'offset rampa di taglio
  vel_sim_lap As String * 3                       'velocità per simulazione e lappatura
  lun_tag_min As String * 8                       'lunghezza di taglio minima
  lun_tag_max As String * 8                      'lunghezza di taglio massima
  gir_mot_lam_rif As String * 4                  'giri motore lame per riferimento
  tem_cam_sta_vel As String * 4               'tempo di campionamento stadio di velocità
  sog_err_ins As String * 4                         'soglia per errore di inseguimento
  off_tag_reg As String * 4                          'offset taglio a registro
  fin_pri_tac As String * 4                           'finestra prima della tacca
  fin_dop_tac As String * 4                         'finestra dopo la tacca
  cor_tag_reg_1°_sog As String * 4           'correzione taglio a registro 1° soglia
  cor_tag_reg_2°_sog As String * 4           'correzione taglio a registro 2° soglia
  sog_vel_cor As String * 3                        'soglia di velocità per correzione
  'Iciliouff 20-11-2003
  vel_ftc As String * 4                       'Velocità di traslazione fotocellula taglio registro (mm/sec X 1000)
  dis_cen_mac_sen_fuo_car As String * 4       'Distanza centro macchina da sensore fuori carter
  dis_edg_ftc As String * 4                   'Distanza meccanica fra fotocellule bordo scatola e FTC
  'Robertouff 26-11-2007
  sov_tap_usc_per_sca As String * 4           'Sovravelocità tappeti uscita durante la fase di scarto (##.#%)
  sca_pri_giu As String * 4                   'Scarto prima della giunta(mm)
  sca_dop_giu As String * 4                   'Scarto dopo la giunta(mm)
  dis_sen_lam As String * 8                   'Distanza fra sensore giunta e lame(mm)
  dis_lam_flp As String * 4                   'Distanza fra lame e inizio Flap di Scarto(mm)
  dis_lam_rul_no_crh As String * 4            'Distanza fra lame e Rullo No Crash Scarto(mm)
  tem_ape_flp As String * 4                   'Tempo apertura flap(msec)
  tem_chi_flp As String * 4                   'Tempo chiusura flap(msec)
  tem_ape_rul_no_crh As String * 4            'Tempo apertura Rullo No-Crash(msec)
  tem_chi_rul_no_crh As String * 4            'Tempo chiusura Rullo No-Crash(msec)
  tem_sca_pre_ape_rul_no_crh As String * 4    'Tempo scambio pressione apertura Rullo No-Crash(msec)
  tem_sca_pre_chi_rul_no_crh As String * 4    'Tempo scambio pressione chiusura Rullo No-Crash(msec)
  max_lun_sca As String * 4                   'Massima lunghezza scartabile(mm)
  min_lun_sca As String * 4                   'Minima lunghezza scartabile(mm)
  sog_vel_1 As String * 4                     'Soglia velocità 1 (m/min)
  sog_lun_1 As String * 4                     'Soglia lunghezza taglio 1 (mm)
  sog_vel_2 As String * 4                     'Soglia velocità 2 (m/min)
  sog_lun_2 As String * 4                     'Soglia lunghezza taglio 2 (mm)
  sog_vel_3 As String * 4                     'Soglia velocità 3 (m/min)
  sog_lun_3 As String * 4                     'Soglia lunghezza taglio 3 (mm)
  'RFAZZI_FI27/03/2008
  sog_lun_sup_3 As String * 4                 'Soglia lunghezza taglio per velocità superiori a 3 (mm)
  tem_att_cam_pre_rul_no_crh As String * 4    'Tempo attivazione cambio pressione rullo no-crash(msec)
End Type

Type tip_par_inf   'tipo per i parametri informazione di ogni scheda di taglio
  ver_pro_sch_tag As String * 5                 'versione programma della scheda di taglio
  ver_sch_tag As String * 5                         'versione scheda di taglio
  ver_PIC As String * 5                                'versione PIC
  ver_rad_qua_con_arr As String * 5           'versione radice quadrata con arrotondamento
  qua_bit_rad_qua_con_arr As String * 5     'quantità di bit radice quadrata con arrotondamento
  per_arr_rad_qua_con_arr As String * 5     'percentuale di arrotondamento radice quadrata con arrotondamento
  ver_rad_qua_sen_arr As String * 5             'versione radice quadrata senza arrotondamento
  qua_bit_rad_qua_sen_arr As String * 5       'quantità di bit radice quadrata senza arrotondamento
End Type

' tipo parametri per la scheda di taglio dipendenti dalla configurazione della macchina
Type tip_par_cfg_sch_tag_liv
  vel_max_tag As String * 3                      'velocità massima della taglierina
  tem_acc_tgl_lun As String * 4                'tempo di accelerazione tagli lunghi
  tem_dec_tgl_lun As String * 4                'tempo di decelerazione tagli lunghi
  tem_acc_tgl_cor As String * 4               'tempo di accelerazione tagli corti
  tem_dec_tgl_cor As String * 4               'tempo di decelerazione tagli corti
  Kp_nor As String * 4                                'Kp normalizzato
  Ki_nor As String * 4                                  'Ki normalizzato
  fil_kp As String * 4                                  'filtro Kp
  Kv_nor As String * 4                                  ' Kv normalizzato
  lim_lav_Kv As String * 3                            ' limite di lavoro del Kv
  gir_max_mot_lam As String * 4               'Arturo uff 21-02-03 preso dalla struttura tip_sch_tag_liv perchè attualmente valorizzato assieme a questi parametri
End Type

Type tip_cur_tag
  'Definizione della Curva di Taglio della Master
  descr As String       'descrizione curva di taglio: formato macchina - num. motori per livello - num. motori abilitati sul livello
  Lun_tag(1 To 15) As Long     'lunghezza di taglio
  vel_max(1 To 15) As Long     'Velocità di Taglio alla lughezza impostata
End Type

' tipo parametri scheda di taglio
Type tip_sch_tag
  par_inf(1 To 3) As tip_par_inf
  sch_tag_liv(1 To 3) As tip_sch_tag_liv
End Type

Type tip_rul_num
  off_vel_met_min As String * 3             'offset di velocità in m/min
  off_vel_per As String * 2                      'offset di velocità in percentuale
  sov_sfi As String * 2                            'sovravelocità allo sfilamento
  dis_rot_mas As String * 6                   'distanza tra Rotary-Shear e Master
End Type
' parametri rulli di ingresso
Type tip_rul_ing
  vel_max_lin As String * 3                       'regolazione riferimento d'ingresso
  rul_num(1 To 2) As tip_rul_num
End Type


'parametri rulli in uscita per livello
Type tip_rul_usc_liv
  lun_san As String * 4                                   'lunghezza sandwich
  gap_tgl_cor As String * 5                             'gap tagli corti < L. sandwich
  gap_tgl_lun As String * 5                             'gap tagli lunghi >= L. sandwich
  tem_ape_fla As String * 4                            'tempo apertura flap
  tem_chi_fla As String * 4                             'tempo chiusura flap 'Arturo uff 07-03-03
  dis_lam_fla As String * 4                              'distanza tra lame e flap
End Type

'parametri rulli in uscita
Type tip_rul_usc
  rul_usc_liv(1 To 3) As tip_rul_usc_liv
End Type

Type tip_com_azi
  TimeOut As String * 5
  baud As String * 4
End Type

Type tip_par_nuo_azi
  descr As String * 50
  cod As Long
  sot_cod As Long
End Type

Type tip_par_azi
  sta As String * 3
  'des_sta As String * 20
  cod_err As String * 4
  'dis As String * 4
  'des As String * 35
  par_new As String * 20
End Type

Type tip_par_azi_tag
  mot(1 To 3) As tip_par_azi
End Type

Type tip_abi_mot
  fun_mot_mot(1 To 3) As Boolean
End Type

Type tip_abi 'tipo per le abilitazioni
  fun_mot_liv(1 To 3) As tip_abi_mot   'funzionamento motore per livello
  num_mot_abi(1 To 3) As Integer        ' indica il numero di motori abilitati per livello
  liv(1 To 3) As Boolean                'abilitazione livello
  abi_com_PU As Boolean                    'abilitazione comunicazione Punto Unico
  'Iciliouff 08-07-2004
  abi_rep_pro As Boolean                'abilitazione visualizzazione report di produzione
  abi_err_com_PU As Boolean    ' ABorelli_FI09/09/2005_1        'abilita segnalazione errore di comunicazione punto unico
End Type

Type tip_config_sch_tag
descr As String * 35
num_mot_liv As Integer
End Type

'ABorelli_FI17/11/2006_1
Type tip_plc_lan
    por_rem As String * 5
    ind_rem As String
    TimeOut As String * 5
    abi_lan As String * 1
End Type
' tipo parametri macchina
Type tip_par_mac
  car_tec As tip_car_tec                          'caratteristiche tecniche
  opt As tip_opt                                         'optional
  com_azi As tip_com_azi
  com_spde As tip_com_spde                  'comunicazione con SPDE
  'agrassi_FI08-03-2010_1
  tip_com_sch_tag(1 To 3) As String * 1
  lan_sch_tag(1 To 3) As tip_lan
  'MLAMIONI_PMASSARO_FI27/10/2010 - Gestione comunicazione UDP-TCP per schede di taglio
  lan_sch_tag_UDP(1 To 3) As Boolean 'tipo di comunicazione UDP o TCP per le schede di taglio
  com_sch_tag(1 To 3) As tip_por     'comunicazione con le schede di taglio
  indirizzo_plc As tip_ind_fins             'comunicazione con PLC
  com_plc_lan As tip_plc_lan        'ABorelli_FI17/11/2006_1
  par_azi_usc(1 To 3) As tip_par_azi                        'parametri di diagnostica azionamenti dei rulli di uscita
  par_azi_ing(1 To 2) As tip_par_azi                        'parametri di diagnostica azionamenti dei rulli di ingresso
  par_azi_tag(1 To 3) As tip_par_azi_tag                        'parametri di diagnostica azionamenti dei rulli di taglio
  config_sch_tag_att(1 To 3) As tip_config_sch_tag                            'configurazione attiva per livello di taglio
  config_sch_tag_def(1 To 3) As tip_config_sch_tag                            'configurazione di default
  par_cfg_att(1 To 3) As tip_par_cfg_sch_tag_liv       'parametri dipendenti dalla configurazione della macchina
  par_cfg_def(1 To 3) As tip_par_cfg_sch_tag_liv       'parametri dipendenti dalla configurazione della macchina
  sch_tag_def As tip_sch_tag                'parametri di default della scheda di taglio
  sch_tag As tip_sch_tag                         'parametri scheda di taglio
  rul_ing As tip_rul_ing                              'parametri rulli di ingresso
  rul_usc As tip_rul_usc                           'parametri rulli in uscita
End Type


'Dati relativi al data base delle onde
Type tip_db_onda
  ONDA As String * 3
  CARTA As String * 36
  PRESSIONE As String * 6
  SPESSORE As String * 6
  GRAMMATURA As String * 6
  VELDIPROD As String * 6
  VELDICAMBI As String * 6
  MTRAAFCON As String * 6
  MTRALUBCON As String * 6
  TEMPODIAFF As String * 6
  TEMPODILUB As String * 6
  NAFRIFXTOT As String * 6
  RISERVATO As String * 6
End Type

'Dati relativi alle onde
Type Tip_ONDA
  cod_com_car As String * 36       'Codice univoco carte composizione onda
  Cod_ond As String * 3                'codice del tipo di onda
  Alt_ond As String * 4                   'altezza onda in decimi di mm
  Vel_aoc As String * 4                 'velocità cambio ordine
  vel_pro As String * 4                  'velocità produzione
  Pes_mt2 As String * 4                 'peso in gr/mt2
  Cod_cod_ond As String * 4       'codice onda
End Type

'Dati commessa
Type Tip_Commessa
  cod_com_ord As String * 15  'Codice commessa ordine
  cod_cli As String * 12            'Codice cliente
End Type

' Dati relativi al singolo ordine
Type Tip_Formato
  com As Tip_Commessa         'Dati commessa
  nom_cli As String * 30           'Nome esteso del cliente
  sca_lar As String * 5             'larghezza scatola in mm/10
  sca_lun As String * 5            'lunghezza scatola in mm/10
  sca_tot_num As String * 6   'numero scatole totali (numero fogli)
  Sca_per_tag As String * 1    'numero scatole per taglio (resa)
  met As String * 8                  'Metri dell'ordine
  Ord_liv As String * 1             'formato  (1=Basso, 2=Medio, 3=Alto)
  'Iciliouff 20-11-2003 Inserimento dati di Cut to Pattern
  cut_to_mark As String * 1         'Presenza Pre-stampato 0=non presente,1=Cut to mark,2=Cut to pattern
  pat_cod As String * 16            'Codice identificativo pattern
  pat_con As String * 1             'Contrasto (0=auto select,1,2,3,4)
  fst_FTC As String * 1             'Prima lettura FTC - specifica se il pattern inizia su sfondo chiaro=0 o su fondo scuro=1
  Ofs_Cut As String * 4             'Offset fra taglio e pattern - da -100mm. a lunghezza foglio
  L1 As String * 3                  'Distanza inizio pattern - inizio 1°tacca
  L2 As String * 3                  'Distanza inizio pattern - fine 1°tacca
  L3 As String * 3                  'Distanza inizio pattern - inizio 2°tacca
  L4 As String * 3                  'Distanza inizio pattern - fine 2°tacca
  L5 As String * 3                  'Distanza inizio pattern - inizio 3°tacca
  L6 As String * 3                  'Distanza inizio pattern - fine 3°tacca
  L7 As String * 3                  'Distanza inizio pattern - inizio 4°tacca
  L8 As String * 3                  'Distanza inizio pattern - fine 4°tacca
  L9 As String * 3                  'Distanza inizio pattern - inizio 5°tacca
  L10 As String * 3                 'Distanza inizio pattern - fine 5°tacca
  fin_pri As String * 4             'Finestra prima della tacca/pattern
  fin_dop As String * 4             'Finestra dopo la tacca/pattern
  pos_pattern As String * 5         'Posizione laterale del pattern sul foglio
End Type

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'''''''''''''   TIPI PER IL REPORT DI PRODUZIONE   ''''''''''''''''''
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Type tip_rep_pro_liv
  fog_buo As String * 7
  fog_err As String * 7
  fog_sca As String * 7
  mil_sca As String * 7 'Robertouff 26-11-2007
End Type

Type tip_rep_pro
  ora_ini As String * 8
  dat_ini As String * 8
  ora_fin As String * 8
  dat_fin As String * 8
  met_tot  As String * 8
  tem_fer As String * 8     ' ABorelli_FI09/09/2005_1
  tem_mar As String * 8
  num_fer As String * 8
  vel_med As String * 3
  liv(1 To 3) As tip_rep_pro_liv
End Type
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Type Tip_Ordine
  'Da inserire
  cod_ord_abb As String * 11   'Codice abbinamento
  spde_id As String * 4          'codice univoco spde
  'RFAZZI_FI 05-01-08
  spde_ModID As String * 2        'codice ModID univoco spde
  stag_id As String * 4           'codice univoco stag
  mod_id As String * 2            'campo modifica ordine
  old_stag_id As String * 4           'vecchio codice univoco stag per gli ordini eseguiti
  old_mod_id As String * 2            'vecchio campo modifica ordine per gli ordini eseguiti
  tip_car As Tip_ONDA         'Dati del Cartone
  alt_bob As String * 5       'Altezza della bobina in mm/10
  met_tot_ord As String * 8 ' metri totali dell'ordine
  pre_all As String * 3   'metri per preallarme
  ord(1 To 3) As Tip_Formato
  rep_pro As tip_rep_pro  'tipo per il report di produzione
End Type

'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
Type Tip_Ordine_DEPM
  'Da inserire
  cod_ord_abb As String * 11   'Codice abbinamento
  spde_id As String * 4          'codice univoco spde
  'RFAZZI_FI 05-01-08
  spde_ModID As String * 2        'codice ModID univoco spde
  stag_id As String * 4           'codice univoco stag
  mod_id As String * 2            'campo modifica ordine
  old_stag_id As String * 4           'vecchio codice univoco stag per gli ordini eseguiti
  old_mod_id As String * 2            'vecchio campo modifica ordine per gli ordini eseguiti
  tip_car As Tip_ONDA         'Dati del Cartone
  alt_bob As String * 5       'Altezza della bobina in mm/10
  met_tot_ord As String * 8 ' metri totali dell'ordine
  pre_all As String * 3   'metri per preallarme
  ord(1 To 3) As Tip_Formato
  rep_pro As tip_rep_pro  'tipo per il report di produzione
End Type

'struttura temporanea che contiene l'ordine inserito, modificato o arrivato da SPDE
Type Tip_Com_ord
  rec As Tip_Ordine  'dati ordine da inserire o modificato
  ret As Integer       'codice di ritorno (errori)
End Type


''''''''''''''''''''''''''''''''''''''''''''''''''
'bit di stato del PLC
'''''''''''''''''''''''''''''''''''''''''''''''''''
Type Tip_sta_PLC
  
  'DM 3600 bit 0-15
  MAC_RIPRISTINATA As String * 1                         '0
  MAC_E_STOP As String * 1                                  '1
  MAC_F_STOP As String * 1                                   '2
  MAC_N_STOP As String * 1                                   '3
  MAC_PRONTA_MARCIA As String * 1                   '4
  MAC_PRONTA_AL_CAMBIO As String * 1            '5
  MAC_IN_MARCIA As String * 1                               '6
  MAC_IN_PREALLARME As String * 1                     '7
  MAC_CAM_ORD_IN_CORSO As String * 1          '8
  LIV1_IN_LAV As String * 1                                       '9
  LIV2_IN_LAV As String * 1                                      '10
  LIV3_IN_LAV As String * 1                                     '11
  MAC_PRE_CAR_LIV1 As String * 1                      '12
  MAC_PRE_CAR_LIV2 As String * 1                      '13
  MAC_PRE_CAR_LIV3 As String * 1                      '14
  MAC_CAM_ORD_COMPLETATO As String * 1                '15
  'DM 3601 bit 0-15
  MAC_IN_MANUALE As String * 1
  MAC_IN_MANUTENZIONE As String * 1
  MAC_IN_SIMULAZIONE As String * 1
  MAC_IN_LAPPATURA As String * 1
  MAC_JOG_NASTRI_IN_COR As String * 1
  SCH_TAG_LIV_1_PRONTA As String * 1
  SCH_TAG_LIV_2_PRONTA As String * 1
  SCH_TAG_LIV_3_PRONTA As String * 1
  free2 As String * 8 'Non utilizzabili perchè per loggare i vari segnali di cambio
End Type

Type Tip_all_PLC
  gru_all As String * 16       'bit gruppi di allarme -macchina-QE-hardware-consolle-warning
  All_att As String * 336     'allarmi attivi DM 3661 -> 3680 + 3681 per gli allarmi Software
End Type

Type tip_ord_PLC
  Lun_tag As Long 'DM 3603, DM3608, DM 3613
  num_fog_buo As Long 'DM 3604-3605, DM3609-3610, DM3614-3615
  num_fog_err As Long   'DM3606,3611,3616
  err_tag As Long           'DM 3607, DM3612, DM3617
End Type

Type tip_ord_pro
  num_fog_buo As Long 'DM3631-3632, DM3635-3636, DM3639-3640
  num_fog_err As Long 'DM3633, DM3637, DM3641
  num_fog_sca As Long 'DM3634, DM3638, DM3642
  'Robertouff 26-11-2007
  num_mil_sca As Long 'DM3644, DM3646, DM3648
End Type
'
Enum sta_run            ' STATO RUNNING
    co_in_cor = 1         ' cambio ordine in corso
    co_com                  'cambio ordine completato
    pre                         ' macchina in preallarme
    pro_cam                 ' macchina pronta al cambio
    in_mar                    ' macchina in marcia
    pro_mar                  ' macchina pronta alla marcia
    fer                         ' macchina ferma (Emergenza)
End Enum

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' stato della macchina e del PLC
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

'Iciliouff 20-11-2003
Type tip_sta_sch_tag_liv
  ID_ord(CORRENTE To SUCCESSIVO) As String * 4
'  ID_ord_suc As String * 4        'wm17C - E' la copia di wm7C in attesa di essere reso attivo
'  ID_ord_cor As String * 4        'wm17D - E' l'ID dell'ordine corrente in lavoro sulla scheda di taglio
End Type

Type tip_sta_sch_tag
  com_OK(1 To 3) As Boolean
  Cmd_att As tip_cmd              'Comando in corso verso scheda di taglio
  Set_ftc_run As Boolean          'Fase di autoapprendimento fotocellula in corso
  stato(1 To 3) As tip_sta_sch_tag_liv
End Type

Type Tip_sta_mac

  'stato macchina
  sta_PLC As Tip_sta_PLC                                   'DM 3600-3601  2 DM
  'dati ordine corrente
  cod_ord_cor As String * 4                                 'DM 3602       1 DM
  ord_plc(1 To 3)  As tip_ord_PLC                    'DM 3603-3617   15 DM
  
  free1 As String * 8
  
  'dati ordine successivo
  cod_ord_suc As String * 4                                'DM 3620       1 DM
  lun_tag_suc(1 To 3) As Long                     'DM 3621-3623  3 DM
      
  
  'PMASSAROFI_05/07/11 - Aggiunta ricezione ordini da MHI via BCD
  'free2 As String * 24
  free2 As String * 4
  next_cut_len_from_external(1 To 3) As Long                'DM 3625-26-27    3 DM
  free6 As String * 8
  
  
  'dati ultimo ordine prodotto
  cod_ord_pro As String * 4                                 'DM 3630
  ord_pro(1 To 3) As tip_ord_pro                            'DM 3631-DM 3642
  
  free3 As String * 28
  
  'velocità misurate
  vel_lin(1 To 3) As Long                                  'DM 3650-DM 3652
  vel_ana_san_ing As Long                                'DM 3653
  vel_san_ing As Long                                       'DM 3654
  vel_san_usc(1 To 3) As Long                                 'DM 3655-DM3657
  
  free4 As String * 8
  
  'diagnostica e allarmi
  all_PLC As Tip_all_PLC                                      'DM 3660-3684 11 DM
  gru_all_att(1 To 7) As Boolean              'per indicare se c'è un allarme attivo su uno dei 7 gruppi allarme
  free5 As String * 52
  
  'risposta alle trasmissioni
  ris_3500 As String * 16                                          'DM 3698
  ris_3501 As String * 16                                           'DM 3699
  
  'Variabili di stato per la COM_PLC
  shi_lis As Boolean    'True al DIFD del cambio ordine completato
  com_OK As Boolean     'True se la comunicazione non ha problemi
  spde_OK As Boolean    'True se l' SPDE comunica
  'No_Gap As Boolean     'True se no gap
  'stato macchina software
  sta_run As sta_run
End Type

Type tip_sta_mac_ute
  MAC_IN_MANUALE_UTE As Boolean
End Type

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' strutture per la RMT
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Type Tip_Com_PLC
  ric As Integer              'tipo di operazione
  liv As Integer              'Livello ordine 'Iciliouff 29-10-2003 Reinserito per motivi di allineamento codice con PMAC, SPDE
  num_ord As Integer          'Numero ordine 1=CORRENTE, 2=SUCCESSIVO        'Iciliouff 29-10-2003 Reinserito per motivi di allineamento codice con PMAC, SPDE
  dat As Variant              'Valorizzato con dati aggiuntivi (se richiesti dal comando)
  ret As Integer              'codice di ritorno (errori)
  tim_out As Long             'time out
  sta As Integer              '
  err As String
  cod_err As Integer
  abi_vis_err As Boolean
  obj As rmt       ' per sapere a chi ritornare il messaggio di ritorno
End Type

Type tip_des_debug
  Desc As String
  chiave As String
End Type

'****************************************************************************************************************************
'***************************************** STRUTTURE PER PROTOCOLLO DIGITEC  *****************************************
'****************************************************************************************************************************

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' struttura per la ricezione del comando set ordine dal punto unico
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Type tip_set_ord_dig4
  liberi As String * 6
  liv As String * 1                 ' livello 1=inf 2=sup o  1=inf 2=med 3=sup
  cod As String * 1                 ' mettere a "1"  , 31 hex
  spde_id As String * 4           'codice univoco identificativo dell'ordine
  car As String * 8                 ' tipo carta
  ONDA As String * 4           ' tipo onda
  cli As String * 12                'cliente
  com As String * 8                 'commessa
  pos As String * 2                 ' 0 = corrente, 1 = successivo
  lun_sca As String * 5             ' lunghezza scatola
  lar_sca As String * 4             ' larghezza totale
  num_fog As String * 6             ' numero fogli
  Sca_per_tag As String * 2         ' fogli per taglio
  pre_all As String * 4             ' preallarme in metri
End Type


''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' struttura per la ricezione del comando display stato ordine
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Type tip_dis_sta_dig4
  liberi As String * 6
  liv As String * 1                 ' livello 1=inf 2=sup o  1=inf 2=med 3=sup
  cod As String * 1
End Type

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' struttura dati per la risposta al comando display stato ordine
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Type tip_ris_sta_ord_dig4
  ID_ord_cor As String * 4
  ID_ord_suc As String * 4
  num_fog_tag As String * 6
  num_fog_sca As String * 6
  car As String * 8
  ONDA As String * 4
  nom_cli As String * 12
  com As String * 8
  Lun_tag As String * 5
  lar_tag As String * 4
  num_fog_imp As String * 6
  resa As String * 2
  pre_all As String * 4
End Type

Type tip_ris_sta_vec_ord_dig4
  ID_ord As String * 4
  com As String * 8
  Lun_tag As String * 5
  ora_ini_pro As String * 6 ' HHMMSS
  ora_fin_pro As String * 6 ' HHMMSS
  tem_mar As String * 7 ' secondi
  tem_fer As String * 7 ' secondi
  nun_fog_OK As String * 6
  num_fog_NO As String * 6
  met_lin_pro As String * 7
  met_qua_pro As String * 7
  num_fer_lin As String * 6
  vel_med_pro As String * 6
End Type

Type tip_ris_dia_dig4
  ID_ord_cor As String * 4
  ID_ord_suc As String * 4
  err_mac As String * 5 ' 1° byte libero, 2° byte libero, 3° byte err. QE potenza, 4° byta err. QE logica, 5° byte err. bordo macchina
End Type

Type tot_pro_ese                ' ABorelli_FI09/09/2005_1

  tem_fer As String * 9   ' secondi
  num_fer As String * 9   '
End Type


