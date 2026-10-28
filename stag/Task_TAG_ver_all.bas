Attribute VB_Name = "Task_TAG_ver_all"
Option Explicit

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Costanti per la comunicazione con STAG              '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

'caratteri di terminazione utilizzati in scambio dati
Public Const STX_TAG = 2      'Start of Transmission
Public Const ETX_TAG = 3      'End of Transmission

'costanti per il protocollo di comunicazione STANDARD tra punto unico e STAG
Public Const DFR = "D" 'Comando di Download Format Record
Public Const USR = "S" 'Comando di Upload Status Record
Public Const ERA = "E" 'Comando di erase
Public Const ore = "O" 'consuntivo ultimo ordine eseguito ' ABorelli_FI09/09/2005_1
Public Const REP = "R" 'Report turno giorno ' ABorelli_FI09/09/2005_1
Public Const con = "C" 'Robertouff 26-11-2007
Public Const UFR = "U" 'Robertouff 26-11-2007

'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
Public Const REQ_TAG_SIMON As String = "?"  'MLamioni_FI03/11/2010 - Carattere di richiesta comunicazione
Public Const ETX_TAG_SIMON As String = ")" 'MLamioni_FI03/11/2010 - Carattere fine stringa dati
Public Const STX_TAG_SIMON As String = "(" 'MLamioni_FI03/11/2010 - Carattere inizio stringa dati
Public Const DFR_SIMON As String = "(" 'MLamioni_FI03/11/2010 - Carattere inizio trasmissione dati ordine

'Public Const ACK = "A"  'Acknowledge
'Public Const NAK = "N"  'Not Acknowledge
Public Const COM_SPDE_LEN_BCC = 2 'lunghezza campo CRC

'****************************************************************************************************************************
'*********************************  STRUTTURE PER PROTOCOLLO STAG-SPDE2000  ****************************
'****************************************************************************************************************************

'COMANDO DI STATO (S)

'Struttura stato taglierina
Type Tip_sta_mst_FOS
  MAC_IN_MANUALE As String * 1
  MAC_CO_IN_CORSO As String * 1
  All_att As String * 1
  PLC_com As String * 1
  SCH_TAG_com(1 To 3) As String * 1
  LIV_ABI(1 To 3) As String * 1
  lib1 As String * 2
  Ord_cor_non_all As String * 1
  Ord_suc_non_all As String * 1
  Con_all_cor As String * 1              'consenti allineamento corrente
  Con_all_suc As String * 1              'consenti allineamento successivo
End Type

'Struttura gruppo allarme attivo
Type Tip_gru_all_att_FOS
  gru_HAR As String * 1
  gru_QE_MST As String * 1
  gru_QE_SLV As String * 1
  gru_QE_EST As String * 1
  gru_BM As String * 1
  gru_CON As String * 1
  gru_MAL As String * 1
  lib3 As String * 9
End Type

'Struttura elemento ordine in struttura stato
Type tip_ord_val_FOS
  val As String * 1                                            'Stato ordine
  ID_ord As String * 6                                      'Spde Identifier associato all'ordine
End Type

'Struttura globale stato taglierina
Type tip_sta_master_fosber20_FOS
  Sta_mac As String * 4                                  'Stato macchina da Stag
  gru_all_att As String * 4                               'Descrizione dei gruppi di allarmi attivi
  ord_val(1 To 5) As tip_ord_val_FOS                    'Vettore dei primi XXX ordini in lista MAX_NUM_LIS_TAG
  lib As String * 13                                           'Liberi
End Type

'Struttura Stag
Type tip_sta_master_FOS
  stag As tip_sta_master_fosber20_FOS               'Struttura globale di stato Stag
End Type

'Struttura globale stato taglierina
Type tip_sta_master_fosber_FOS_30
  Sta_mac As String * 4                                  'Stato macchina da Stag
  gru_all_att As String * 4                               'Descrizione dei gruppi di allarmi attivi
  ord_val(1 To 6) As tip_ord_val_FOS                    'Vettore dei primi XXX ordini in lista MAX_NUM_LIS_TAG e Ultimo Ordine Prodotto
  lib As String * 6                                           'Liberi
End Type

'Struttura Stag
Type tip_sta_master_FOS_30
  stag As tip_sta_master_fosber_FOS_30               'Struttura globale di stato Stag
End Type

'COMANDO DI PROGRAMMAZIONE ORDINE (D)

'Struttura singola sezione formato ordine
Type tip_for_ord_fosber20_FOS
  pos_for As String * 1                     'Lato lavorazione ordine (1=Sx, 0=Dx)
  comm_ord As String * 15               'Codice commessa cliente
  codi_cli As String * 12                    'Codice del cliente
  nome_cli As String * 30                  'Nome del cliente
  Livello As String * 1                         'Livello di lavorazione ordine (U=alto, M=medio, L=basso)
  lar_for As String * 5                       'Larghezza foglio in decimi di mm
  lun_di_tag As String * 5                 'Lunghezza foglio in decimi di mm
  num_sca_per_tag As String * 1    'Numero scatole per taglio
  num_fogli As String * 6                  'Numero di tagli ordine
  cut_to_mar As String * 1               'Specifica se il taglio sarà pilotato con Cut to Mark o meno (1=Si Cut to Mark, 0=No Cut to Mark)
  met_for As String * 7                     'Lunghezza ordine in mt
  lib5 As String * 16                          'Liberi
End Type

'Struttura ordine per taglierina
Type tip_pro_ord_fosber20_FOS
  spde_id_mod_id As String * 6             'Spde Identifier associato all'ordine
  Pos_lis As String * 2                            'Posizione ordine in lista
  cod_abb As String * 11                       'Codice abbinamento ordine uffici
  alt_bob_ord As String * 5                    'Altezza bobina in mm
  cod_onda As String * 3                       'Codice tipo onda (A, B, BC ...)
  comp_car As String * 36                     'Codice composizione carta
  spess_car As String * 3                      'Spessore cartone in decimi di mm
  gramm_car As String * 5                      'Grammatura della carta in gr/ m*m
  vel_cam_ord As String * 3                   'Velocità di cambio ordine in mt/ min
  vel_prod As String * 3                          'Velocità di produzione in mt/ min
  cod_cod_onda As String * 3                'Codice onda per stacker
  lun_tot_ord As String * 7                      'Lunghezza totale ordine in mt
  preall_ord As String * 3                        'Preallarme in metri prima del cambio ordine
  lib4 As String * 9                                   'Liberi
  for_ord(1 To 3) As tip_for_ord_fosber20_FOS
End Type

'Iciliouff 20-11-2003
'PROTOCOLLO FOSBER 2.1
Type tip_cut_pat_bsh
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
End Type

Type tip_for_ord_fosber21_FOS
  comm_ord As String * 20                 'Codice commessa cliente
  codi_cli As String * 15                 'Codice del cliente
  nome_cli As String * 30                 'Nome del cliente
  Livello As String * 1                   'Livello di lavorazione ordine (U=alto, M=medio, L=basso)
  lar_for As String * 5                   'Larghezza foglio in decimi di mm
  lun_di_tag As String * 5                'Lunghezza foglio in decimi di mm
  num_sca_per_tag As String * 2           'Numero scatole per taglio
  num_fogli As String * 6                 'Numero di tagli ordine
  cut_to_mar As String * 1                'Specifica se il taglio sarà pilotato con Cut to Mark o meno (1=Si Cut to Mark, 0=No Cut to Mark)
  cut_pat As tip_cut_pat_bsh              'Dati cut to pattern
  free As String * 50                     'Liberi
End Type

Type tip_pro_ord_fosber21_FOS
  spde_id_mod_id As String * 6            'Spde Identifier associato all'ordine
  Pos_lis As String * 2                   'Posizione ordine in lista
  cod_abb As String * 16                  'Codice abbinamento ordine uffici
  alt_bob_ord As String * 5               'Altezza bobina in mm
  cod_onda As String * 3                  'Codice tipo onda (A, B, BC ...)
  comp_car As String * 56                 'Codice composizione carta
  spess_car As String * 3                 'Spessore cartone in decimi di mm
  gramm_car As String * 5                 'Grammatura della carta in gr/ m*m
  vel_cam_ord As String * 3               'Velocità di cambio ordine in mt/ min
  vel_prod As String * 3                  'Velocità di produzione in mt/ min
  cod_cod_onda As String * 3              'Codice onda per stacker
  preall_ord As String * 3                'Preallarme in metri prima del cambio ordine
  free As String * 50                     'Liberi
  for_ord(1 To 3) As tip_for_ord_fosber21_FOS
End Type


Type tip_for_ord_ese_fosber21_FOS   ' ABorelli_FI09/09/2005_1
    fog_ok As String * 9   '
    fog_no As String * 9   '
    liv As String * 1      ' L,M, oppure U
End Type

Type tip_pro_ord_ese_fosber21_FOS   ' ABorelli_FI09/09/2005_1
  spde_id_mod_id As String * 6            'Spde Identifier associato all'ordine
  cod_abb As String * 16                  'Codice abbinamento ordine uffici
  dat_ini As String * 8   ' yyyymmdd
  ora_ini As String * 6   ' hhmmss
  ora_fin As String * 6   ' hhmmss
  tem_mar As String * 9   ' secondi
  tem_fer As String * 9   ' secondi
  vel_med As String * 9   ' metri/minuto
  num_fer As String * 9   '
  met_lin As String * 9   '
  met_qua As String * 9   '
  for_ord(1 To 3)   As tip_for_ord_ese_fosber21_FOS
End Type
Type tip_pro_fosber21_FOS   ' ABorelli_FI09/09/2005_1
    dat_ini As String * 8   ' yyyymmdd
    ora_ini As String * 6   ' hhmmss
    dat_fin As String * 8   ' yyyymmdd
    ora_fin As String * 6   ' hhmmss
    tem_mar As String * 9   ' secondi
    tem_fer As String * 9   ' secondi
    vel_med As String * 9   ' metri/minuto
    num_fer As String * 9   '
    met_lin As String * 9   '
    met_qua As String * 9   '
    num_cam As String * 9   '
End Type
Type tip_pro_tur_gio_fosber21_FOS   ' ABorelli_FI09/09/2005_1
    tur(1 To 3) As tip_pro_fosber21_FOS
    gio As tip_pro_fosber21_FOS
End Type

'Robertouff 26-11-2007
'PROTOCOLLO FOSBER 3.0
Type tip_cut_pat_bsh_30
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
  pos_pattern As String * 5         'Posizione laterale del Pattern
End Type

Type tip_for_ord_fosber30_FOS
  comm_ord As String * 20                 'Codice commessa cliente
  codi_cli As String * 15                 'Codice del cliente
  nome_cli As String * 30                 'Nome del cliente
  Livello As String * 1                   'Livello di lavorazione ordine (U=alto, M=medio, L=basso)
  lar_for As String * 5                   'Larghezza foglio in decimi di mm
  lun_di_tag As String * 5                'Lunghezza foglio in decimi di mm
  num_sca_per_tag As String * 2           'Numero scatole per taglio
  num_fogli As String * 6                 'Numero di tagli ordine
  cut_to_mar As String * 1                'Specifica se il taglio sarà pilotato con Cut to Mark o meno (1=Si Cut to Mark, 0=No Cut to Mark)
  cut_pat As tip_cut_pat_bsh_30           'Dati cut to pattern
  free As String * 45                     'Liberi
End Type

Type tip_pro_ord_fosber30_FOS
  spde_id_mod_id As String * 6            'Spde Identifier associato all'ordine
  Pos_lis As String * 2                   'Posizione ordine in lista
  cod_abb As String * 16                  'Codice abbinamento ordine uffici
  alt_bob_ord As String * 5               'Altezza bobina in mm
  cod_onda As String * 3                  'Codice tipo onda (A, B, BC ...)
  comp_car As String * 56                 'Codice composizione carta
  spess_car As String * 3                 'Spessore cartone in decimi di mm
  gramm_car As String * 5                 'Grammatura della carta in gr/ m*m
  vel_cam_ord As String * 3               'Velocità di cambio ordine in mt/ min
  vel_prod As String * 3                  'Velocità di produzione in mt/ min
  cod_cod_onda As String * 3              'Codice onda per stacker
  preall_ord As String * 3                'Preallarme in metri prima del cambio ordine
  free As String * 50                     'Liberi
  for_ord(1 To 3) As tip_for_ord_fosber30_FOS
End Type

Type tip_for_ord_ese_fosber30_FOS   'Robertouff 26-11-2007
    fog_ok As String * 9   '
    fog_no As String * 9   '
    liv As String * 1      ' L,M, oppure U
    mm_scarto As String * 9   ' mm scarto fatto
End Type

Type tip_pro_ord_ese_fosber30_FOS   'Robertouff 26-11-2007
  spde_id_mod_id As String * 6            'Spde Identifier associato all'ordine
  cod_abb As String * 16                  'Codice abbinamento ordine uffici
  dat_ini As String * 8   ' yyyymmdd
  ora_ini As String * 6   ' hhmmss
  ora_fin As String * 6   ' hhmmss
  tem_mar As String * 9   ' secondi
  tem_fer As String * 9   ' secondi
  vel_med As String * 9   ' metri/minuto
  num_fer As String * 9   '
  met_lin As String * 9   '
  met_qua As String * 9   '
  for_ord(1 To 3)   As tip_for_ord_ese_fosber30_FOS
End Type
