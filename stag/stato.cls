VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "ClasseStato"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True


Option Explicit
   '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
   '' Eventi Taglierina MASTER:                 ''
   '' Genera gli eventi di allarme per i client ''
   ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Enum sta_reset             'STATO RESET
    rip                            ' macchina ripristinata
    e_stop                      ' emergency stop
    f_stop                       ' fast stop
    n_stop                      ' normal stop
    ness_reset                'serve per gestire le operazioni di visualizzazione nella sta_bar nel caso di indirizzo sbagliato con il PLC
End Enum

Enum sta_ord            ' STATO CORRENTE O SUCCESSIVO
  ord_nok = 1     ' ordine disallineato
  ord_ok            ' ordine allineato
  ord_ass           ' ordine non presente
End Enum

Enum tip_ord
  CORR = 1
  SUCC
End Enum

Enum Sta_mac
  mac_in_man      'macchina in manutenzione
  mac_in_sim       'macchina in simulazione
  mac_in_lap       'macchina in lappatura
  ness_mac          'serve per gestire le operazioni di visualizzazione nella sta_bar nel caso di indirizzo sbagliato con il PLC
End Enum

Enum sta_mod
  manuale       'macchina in manuale
  automatico   'macchina in automatico
End Enum

Public Event run(run As Integer)                        ' evento per lo stato di running
Public Event reset(sta_res As sta_reset)            ' evento per lo stato reset
Public Event Mac(Sta_mac As Sta_mac)    ' evento per lo stato manutenzione,simulazione,ciclo di lappatura
Public Event modo(Sta_mac As sta_mod)             ' evento per lo stato manuale o automatic
Public Event ord(eve As sta_ord, ord As tip_ord)   ' evento sullo stato degli ordini corrente e successivo
Public Event liv()                                                ' evento per il cambio dei messaggi in lingua sui livelli della sta_bar
Public Event spde(ok As Boolean)                    ' tipo evento per la comunicazione con spde
Public Event MASTER(ok As Boolean)                ' tipo evento per la comunicazione con la master
Public Event vel(v As Long)                                 ' tipo evento per variazione della velocità di linea
Public Event gruall(v As String)                           ' evento sul gruppo allarmi: restituisco una stringa di 5 bit in binario
Public Event sezall(all_PLC As String)                'evento per allarme: restituisco una stringa di 160 bit (10 DM)
Public Event err(liv As Integer, err As String)          'evento per rilevare l'errore di taglio
Public Event num(liv As Integer, mis As String)         'evento per rilevare il numero di tagli effettuati
Public Event numerr(liv As Integer, mis As String)         'evento per rilevare il numero di tagli errati
Public Event velmax(v As Integer)        'evento per il cambio della velocità massima in conseguenza di un cambio configurazione scheda di taglio
Private stato As Tip_sta_mac               ' variabile privata per lo stato macchina
Private old_stato As Tip_sta_mac         ' variabile privata per memorizzare lo stato precedente e rilevare i cambiamenti
Private cam_lin As Boolean
' ABorelli_FI09/09/2005_1
Private tot_ese As tot_pro_ese
Private tot_tg As tip_pro_tur_gio_fosber21_FOS
Const ORD_COR_ASS = 1    'bit 0
Const ORD_SUC_ASS = 2    'bit 1
Const COM_SPDE_NO = 4    'bit2
Const COM_PLC_NO = 8       'bit3
Const COM_ST_INI_1_NO = 16   'bit 4 : comunicazione iniziale con le schede di taglio (bit 4 = scheda 1 , bit 5 = scheda 2 , bit 6 = scheda 3)

Sub chg_lin()
cam_lin = True
End Sub

Sub chk_all_sftw()
'Controlla se ci sono allarmi software. Se ve ne sono restituisce True altrimenti False
'Bit 0 -> Ordine corrente assente, Bit 1 -> Ordine successivo assente

Dim val_war As Long
Dim i As Integer
Dim all As Boolean
Dim val_fin As Double
Dim num_ord As Integer
Dim str As String

val_war = 0
num_ord = ord_mac.Count
If num_ord < 2 Then 'non ho l'ordine successivo
    all = True
    val_war = val_war + ORD_SUC_ASS
    If num_ord = 0 Then val_war = val_war + ORD_COR_ASS  'non ho l'ordine corrente
End If

If par_abi.abi_com_PU And par_abi.abi_err_com_PU Then   ' ABorelli_FI09/09/2005_1

  If Not stato.spde_OK And Not (stato.sta_PLC.MAC_IN_MANUALE = "1") Then
    'SIMONE 18/04/02: riabilitata visualizzazione visto che la sua disabilitazione rappresentava più una personalizzazione da fare con ESCADA
    'Nel Job di Linpac Atlanta infatti, Escada non faceva mai un polling per la richiesta di stato => andava sempre in timeout
    If Not pro_set.per_cli = "LINPAC_ATLANTA" Then val_war = val_war + COM_SPDE_NO '30112001 Stagetti Disabilitato visualizzazione allarme comunicazione con spde
    all = True
  End If
End If

If Not stato.com_OK Then
  val_war = val_war + COM_PLC_NO
  all = True
End If

For i = 1 To val(par_mac.car_tec.num_liv_tag)
  If Not com_ini_sch_tag(i) Then
      val_war = val_war + (COM_ST_INI_1_NO * (2 ^ (i - 1)))
      all = True
  End If
Next i

stato.all_PLC.All_att = Mid$(stato.all_PLC.All_att, 1, Len(stato.all_PLC.All_att) - 16) & hex_bin(int_esa(val_war), True)

If all Then
  
  val_fin = Hex_Dec(bin_hex(stato.all_PLC.gru_all))
  If Not (val_fin And (2 ^ POS_GRU_MAL)) = 2 ^ POS_GRU_MAL Then
      val_fin = val_fin + 2 ^ (POS_GRU_MAL)
      stato.all_PLC.gru_all = hex_bin(int_esa(val_fin), False)
  End If
  
  If Not stato.com_OK Then 'Se non comunico con la macchina non faccio nemmeno l'aggiorna => invio gli eventi sugli allarmi
      If old_stato.all_PLC.All_att <> stato.all_PLC.All_att Then
          old_stato.all_PLC.All_att = stato.all_PLC.All_att
          RaiseEvent sezall(stato.all_PLC.All_att)
          RaiseEvent gruall(stato.all_PLC.gru_all)
      End If
  End If
End If

End Sub

Public Property Get gru_all() As String
'Dati letti da: DM 3660
gru_all = stato.all_PLC.gru_all
End Property



Public Property Get MAC_N_STOP(v As Integer) As Boolean
If val(stato.sta_PLC.MAC_N_STOP) = v Then
  MAC_N_STOP = True
Else
  MAC_N_STOP = False
  'str_err = msg("ord_cor_pos")
End If
End Property

Public Property Get MAC_F_STOP(v As Integer) As Boolean
If val(stato.sta_PLC.MAC_F_STOP) = v Then
  MAC_F_STOP = True
Else
  MAC_F_STOP = False
  'str_err = msg("ord_cor_pos")
End If
End Property

Public Property Get MAC_E_STOP(v As Integer) As Boolean
If val(stato.sta_PLC.MAC_E_STOP) = v Then
  MAC_E_STOP = True
Else
  MAC_E_STOP = False
  'str_err = msg("ord_cor_pos")
End If
End Property

Public Property Get MAC_RIPRISTINATA(v As Integer) As Boolean
If val(stato.sta_PLC.MAC_RIPRISTINATA) = v Then
  MAC_RIPRISTINATA = True
Else
  MAC_RIPRISTINATA = False
  'str_err = msg("ord_cor_pos")
End If
End Property

Public Property Get MAC_PRONTA_ALLA_MARCIA(v As Integer) As Boolean
If val(stato.sta_PLC.MAC_PRONTA_MARCIA) = v Then
  MAC_PRONTA_ALLA_MARCIA = True
Else
  MAC_PRONTA_ALLA_MARCIA = False
  'str_err = msg("ord_cor_pos")
End If
End Property

Public Property Get MAC_PRONTA_AL_CAMBIO(v As Integer) As Boolean
If val(stato.sta_PLC.MAC_PRONTA_AL_CAMBIO) = v Then
  MAC_PRONTA_AL_CAMBIO = True
Else
  MAC_PRONTA_AL_CAMBIO = False
  'str_err = msg("ord_cor_pos")
End If
End Property

Public Property Get MAC_IN_MARCIA(v As Integer) As Boolean
If val(stato.sta_PLC.MAC_IN_MARCIA) = v Then
  MAC_IN_MARCIA = True
Else
  MAC_IN_MARCIA = False
  'str_err = msg("ord_cor_pos")
End If
End Property

Public Property Get MAC_IN_PREALLARME(v As Integer) As Boolean
If val(stato.sta_PLC.MAC_IN_PREALLARME) = v Then
  MAC_IN_PREALLARME = True
Else
  MAC_IN_PREALLARME = False
  'str_err = msg("ord_cor_pos")
End If
End Property

Public Property Get MAC_CAMBIO_ORDINE_IN_CORSO(v As Integer) As Boolean
If val(stato.sta_PLC.MAC_CAM_ORD_IN_CORSO) = v Then
  MAC_CAMBIO_ORDINE_IN_CORSO = True
Else
  MAC_CAMBIO_ORDINE_IN_CORSO = False
  'str_err = msg("ord_cor_pos")
End If
End Property

Public Property Get MAC_CAMBIO_ORDINE_COMPLETATO(v As Integer) As Boolean
If val(stato.sta_PLC.MAC_CAM_ORD_COMPLETATO) = v Then
  MAC_CAMBIO_ORDINE_COMPLETATO = True
Else
  MAC_CAMBIO_ORDINE_COMPLETATO = False
  'str_err = msg("ord_cor_pos")
End If
End Property
Public Property Get MAC_LIV1_IN_LAV(v As Integer) As Boolean
If val(stato.sta_PLC.LIV1_IN_LAV) = v Then
  MAC_LIV1_IN_LAV = True
Else
  MAC_LIV1_IN_LAV = False
  'str_err = msg("ord_cor_pos")
End If
End Property

Public Property Get MAC_LIV2_IN_LAV(v As Integer) As Boolean
If val(stato.sta_PLC.LIV2_IN_LAV) = v Then
  MAC_LIV2_IN_LAV = True
Else
  MAC_LIV2_IN_LAV = False
  'str_err = msg("ord_cor_pos")
End If
End Property

Public Property Get MAC_LIV3_IN_LAV(v As Integer) As Boolean
If val(stato.sta_PLC.LIV3_IN_LAV) = v Then
  MAC_LIV3_IN_LAV = True
Else
  MAC_LIV3_IN_LAV = False
  'str_err = msg("ord_cor_pos")
End If
End Property



Public Property Get MAC_PRE_CAR_LIV1(v As Integer) As Boolean
If val(stato.sta_PLC.MAC_PRE_CAR_LIV1) = v Then
  MAC_PRE_CAR_LIV1 = True
Else
  MAC_PRE_CAR_LIV1 = False
  'str_err = msg("ord_cor_pos")
End If
End Property

Public Property Get MAC_PRE_CAR_LIV2(v As Integer) As Boolean
If val(stato.sta_PLC.MAC_PRE_CAR_LIV2) = v Then
  MAC_PRE_CAR_LIV2 = True
Else
  MAC_PRE_CAR_LIV2 = False
  'str_err = msg("ord_cor_pos")
End If
End Property

Public Property Get MAC_PRE_CAR_LIV3(v As Integer) As Boolean
If val(stato.sta_PLC.MAC_PRE_CAR_LIV3) = v Then
  MAC_PRE_CAR_LIV3 = True
Else
  MAC_PRE_CAR_LIV3 = False
  'str_err = msg("ord_cor_pos")
End If
End Property

Public Property Get MAC_IN_MANUALE(v As Integer) As Boolean
If val(stato.sta_PLC.MAC_IN_MANUALE) = v Then
  MAC_IN_MANUALE = True
Else
  MAC_IN_MANUALE = False
  'str_err = msg("ord_cor_pos")
End If
End Property
Friend Sub rea_dat_ord_pro(fine_prod As Boolean)
Dim i As Integer
Dim liv_ord As Integer
Dim DM_PRO As Variant
Dim tmp_met As Single
Dim m As Single
Dim tmp_res As Long
Dim temp As ClasseOrdine
Dim ret As Integer

If pro_set.abi_reg_rep_pro = STR_TRUE Then 'ho iniziato la produzione
    If ord_mac_ese.Count = 0 Then
      log_str "Sub rea_dat_ord_pro della ClasseStato. Lettura dei dati dell'ultimo ordine prodotto: la lista degli ordini eseguiti risulta vuota", MED_PRIORITY, log_lis
    Else
      Set temp = New ClasseOrdine
      temp.ordine = ord_mac_ese.item(1).ordine
      If Not fine_prod Then
        'CASO NORMALE CAMBIO ORDINE
        If val(par_mac.com_plc_lan.abi_lan) = 0 Then      'ABorelli_FI17/11/2006_1
          'Robertouff 26-11-2007
          'DM_PRO = sta_bar.SYSMAC_PLC.readArea(plcAreaDM, 3630, 13, vbVariant Or vbArray)
          DM_PRO = sta_bar.SYSMAC_PLC.readArea(plcAreaDM, 3630, 19, vbVariant Or vbArray)
        Else
          'Robertouff 26-11-2007
          'sta_bar.Fins.readArea plcAreaDM, 3630, 13
          sta_bar.Fins.readArea plcAreaDM, 3630, 19
          DM_PRO = sta_bar.Fins.RetData
        End If
        stato.cod_ord_pro = DM_PRO(0)
        If Trim$(stato.cod_ord_pro) <> Trim$(temp.old_stag_id) Then
          log_str "Sub rea_dat_ord_pro della ClasseStato. Lettura dei dati dell'ultimo ordine prodotto: l'ID dell'ultimo ordine prodotto non coincide con l'ID del primo ordine della lista ordini eseguiti", MED_PRIORITY, log_lis
          log_str "Codice collezione eseguiti: " & Trim$(temp.old_stag_id) & " Codice nel PLC: " & Trim$(stato.cod_ord_pro), MED_PRIORITY, log_lis
        Else
          stato.ord_pro(1).num_fog_buo = leg_lng(int_esa(DM_PRO(1)), int_esa(DM_PRO(2)))
          stato.ord_pro(1).num_fog_err = DM_PRO(3)
          stato.ord_pro(1).num_fog_sca = DM_PRO(4)
          'Robertouff 26-11-2007
          stato.ord_pro(1).num_mil_sca = leg_lng(int_esa(DM_PRO(14)), int_esa(DM_PRO(15)))
          If par_mac.car_tec.num_liv_tag > 1 Then
            stato.ord_pro(2).num_fog_buo = leg_lng(int_esa(DM_PRO(5)), int_esa(DM_PRO(6)))
            stato.ord_pro(2).num_fog_err = DM_PRO(7)
            stato.ord_pro(2).num_fog_sca = DM_PRO(8)
            'Robertouff 26-11-2007
            stato.ord_pro(2).num_mil_sca = leg_lng(int_esa(DM_PRO(16)), int_esa(DM_PRO(17)))
          Else
            stato.ord_pro(2).num_fog_buo = 0
            stato.ord_pro(2).num_fog_err = 0
            stato.ord_pro(2).num_fog_sca = 0
            'Robertouff 26-11-2007
            stato.ord_pro(2).num_mil_sca = 0
          End If
          If par_mac.car_tec.num_liv_tag > 2 Then
            stato.ord_pro(3).num_fog_buo = leg_lng(int_esa(DM_PRO(9)), int_esa(DM_PRO(10)))
            stato.ord_pro(3).num_fog_err = DM_PRO(11)
            stato.ord_pro(3).num_fog_sca = DM_PRO(12)
            'Robertouff 26-11-2007
            stato.ord_pro(3).num_mil_sca = leg_lng(int_esa(DM_PRO(18)), int_esa(DM_PRO(19)))
          Else
            stato.ord_pro(3).num_fog_buo = 0
            stato.ord_pro(3).num_fog_err = 0
            stato.ord_pro(3).num_fog_sca = 0
            'Robertouff 26-11-2007
            stato.ord_pro(3).num_mil_sca = 0
          End If
          log_str "REPORT ORDINE " & Trim$(stato.cod_ord_pro), MED_PRIORITY, log_lis
          log_str "Valori letti da PLC", MED_PRIORITY, log_lis
          'RFAZZI_FI 09-01-08
          For i = 1 To 3
            log_str "Fogli Buoni = " & CStr(stato.ord_pro(i).num_fog_buo) & " Fogli Errati = " & CStr(stato.ord_pro(i).num_fog_err) & " Fogli Scarto = " & CStr(stato.ord_pro(i).num_fog_sca) & " MM Scarto = " & CStr(stato.ord_pro(i).num_mil_sca), MED_PRIORITY, log_lis
          Next i
          m = 0
          log_str "Valori assegnati alla struttura ord_mac_ese", MED_PRIORITY, log_lis
          For i = 1 To 3
            If val(temp.Ord_liv(i)) <> 0 Then
              liv_ord = val(temp.Ord_liv(i))
              tmp_res = temp.Sca_per_tag(i)
              temp.fog_buo(i) = CStr(val(stato.ord_pro(liv_ord).num_fog_buo) * val(tmp_res))
              temp.fog_err(i) = CStr(val(stato.ord_pro(liv_ord).num_fog_err) * val(tmp_res))
              temp.fog_sca(i) = CStr(val(stato.ord_pro(liv_ord).num_fog_sca) * val(tmp_res))
              'RFAZZI_FI 09-01-08
              temp.num_mil_sca(i) = CStr(val(stato.ord_pro(liv_ord).num_mil_sca))
            
              log_str "FORMATO: " & i & " LIVELLO: " & liv_ord & " Fogli Buoni = " & temp.fog_buo(i) & " Fogli Errati = " & temp.fog_err(i) & " Fogli Scarto = " & temp.fog_sca(i) & " MM Scarto = " & temp.num_mil_sca(i), MED_PRIORITY, log_lis
              If tmp_res > 0 Then
                tmp_met = Sng((val(stato.ord_pro(liv_ord).num_fog_buo) + val(stato.ord_pro(liv_ord).num_fog_sca) / tmp_res) * (((val(temp.sca_lun(i)) / TODECMM) / 1000)))
                If m < tmp_met Then m = tmp_met
              End If
            End If
          Next i
          temp.met_pro = Trim$(m)
          ins_dat_pro temp
        End If
        'CASO FINE PRODUZIONE
      Else
        'RFAZZI_FI 09-01-08
        log_str "FINE PRODUZIONE. REPORT ORDINE -- ID SPDE: " & Trim$(temp.spde_id) & " ID STAG: " & Trim$(temp.stag_id), MED_PRIORITY, log_lis
        log_str "Valori letti da PLC", MED_PRIORITY, log_lis
        For i = 1 To 3
          log_str "Fogli Buoni = " & CStr(stato.ord_plc(i).num_fog_buo) & " Fogli Errati = " & CStr(stato.ord_plc(i).num_fog_err), MED_PRIORITY, log_lis
        Next i
        m = 0
        log_str "Valori assegnati alla struttura ord_mac_ese", MED_PRIORITY, log_lis
        For i = 1 To 3
          If val(temp.Ord_liv(i)) <> 0 Then
            liv_ord = val(temp.Ord_liv(i))
            tmp_res = temp.Sca_per_tag(i)
            temp.fog_buo(i) = CStr(val(stato.ord_plc(liv_ord).num_fog_buo) * val(tmp_res))
            temp.fog_err(i) = CStr(val(stato.ord_plc(liv_ord).num_fog_err) * val(tmp_res))
            temp.fog_sca(i) = "0"
            log_str "FORMATO: " & i & " LIVELLO: " & liv_ord & " Fogli Buoni = " & temp.fog_buo(i) & " Fogli Errati = " & temp.fog_err(i) & " Fogli Scarto = " & temp.fog_sca(i), MED_PRIORITY, log_lis
            If tmp_res > 0 Then
              tmp_met = Sng(val(stato.ord_plc(liv_ord).num_fog_buo) * (((val(temp.sca_lun(i)) / TODECMM) / 1000)))
              If m < tmp_met Then m = tmp_met
            End If
          End If
        Next i
        temp.met_pro = Trim$(m)
        ins_dat_pro temp
      End If
    End If
End If
End Sub
Friend Sub ins_dat_pro(tmp_ord As ClasseOrdine)
'calcolo della velocità media per l'ordine appena prodotto e salvataggio dei dati su collezione e su file
Dim s As Long
Dim ret As cod_ret

If Trim$(tmp_ord.dat_ini) <> "" And Trim$(tmp_ord.ora_ini) <> "" And _
 Trim$(tmp_ord.dat_fin) <> "" And Trim$(tmp_ord.ora_fin) <> "" Then
  s = DateDiff("s", Trim$(tmp_ord.dat_ini) + " " + Trim$(tmp_ord.ora_ini), Trim$(tmp_ord.dat_fin) + " " + Trim$(tmp_ord.ora_fin))
  tmp_ord.vel_med = CStr(CInt(Sng(Trim$(tmp_ord.met_pro)) * 60 / s))
Else
  tmp_ord.vel_med = ""
End If
log_str "Velocità Media: " & CStr(val(tmp_ord.vel_med)) & " Metri Prodotti: " & CStr(val(tmp_ord.met_pro)), MED_PRIORITY, log_lis
ret = ord_mac_ese.Modifica(tmp_ord, 1, False)
If ret = RET_OK Then
  log_str "Salvataggio dei nuovi dati su collezione ord_mac_ese e su file eseguito", MED_PRIORITY, log_lis
Else
  log_str "Salvataggio dei nuovi dati su collezione ord_mac_ese e su file NON eseguito", MED_PRIORITY, log_lis
End If

End Sub



Public Property Get SCH_TAG_LIV_PRONTA(liv As Integer, v As Integer) As Boolean
Dim pro As Boolean

pro = False
Select Case liv
  Case 1
    If val(stato.sta_PLC.SCH_TAG_LIV_1_PRONTA) = v Then pro = True
  Case 2
    If val(stato.sta_PLC.SCH_TAG_LIV_2_PRONTA) = v Then pro = True
  Case 3
    If val(stato.sta_PLC.SCH_TAG_LIV_3_PRONTA) = v Then pro = True
End Select
SCH_TAG_LIV_PRONTA = pro

End Property
Public Property Get SCH_TAG_LIV_2_PRONTA(v As Integer) As Boolean
If val(stato.sta_PLC.SCH_TAG_LIV_2_PRONTA) = v Then
  SCH_TAG_LIV_2_PRONTA = True
Else
  SCH_TAG_LIV_2_PRONTA = False
End If
End Property
Public Property Get SCH_TAG_LIV_3_PRONTA(v As Integer) As Boolean
If val(stato.sta_PLC.SCH_TAG_LIV_3_PRONTA) = v Then
  SCH_TAG_LIV_3_PRONTA = True
Else
  SCH_TAG_LIV_3_PRONTA = False
End If
End Property
Public Property Get MAC_IN_MANUTENZIONE(v As Integer) As Boolean
If val(stato.sta_PLC.MAC_IN_MANUTENZIONE) = v Then
  MAC_IN_MANUTENZIONE = True
Else
  MAC_IN_MANUTENZIONE = False
  'str_err = msg("ord_cor_pos")
End If
End Property

Public Property Get MAC_IN_SIMULAZIONE(v As Integer) As Boolean
If val(stato.sta_PLC.MAC_IN_SIMULAZIONE) = v Then
  MAC_IN_SIMULAZIONE = True
Else
  MAC_IN_SIMULAZIONE = False
  'str_err = msg("ord_cor_pos")
End If
End Property

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  PROPRIETA' DELLO STATO  IN LETTURA E SCRITTURA   '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Property Get com_OK() As Boolean
com_OK = stato.com_OK
End Property

Public Property Let com_OK(v As Boolean)

stato.com_OK = v

End Property

Public Property Get spde_OK() As Boolean
  spde_OK = stato.spde_OK
End Property

Public Property Let spde_OK(v As Boolean)

stato.spde_OK = v

End Property

Sub aggiorna_spde()
Static ini As Integer
Static old_spde As Boolean

If ini = 0 Then
    old_spde = True
    ini = 1
End If
If old_spde <> stato.spde_OK Then  'SPDE
  RaiseEvent spde(stato.spde_OK)
  old_spde = stato.spde_OK
End If

End Sub
Public Property Get shi_lis() As Boolean
  shi_lis = stato.shi_lis
End Property

Public Property Let shi_lis(v As Boolean)
stato.shi_lis = v
End Property

Public Property Get sta_run() As Integer
  sta_run = stato.sta_run
End Property


'PMASSAROFI_05/07/11 - Aggiunta ricezione ordini da MHI via BCD
Public Property Get next_cut_len_from_external(liv As Integer) As Long
  next_cut_len_from_external = val(stato.next_cut_len_from_external(liv))
End Property

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  FUNZIONE PER LA LETTURA DELLO STATO DAL PLC   ''
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Friend Sub aggiorna()
' aggiorna i propri dati e genera i corrispondenti eventi per i client
' leggo dal PLC i lo stato codificato in DM3600-3699
Dim ret As cod_ret
Dim ret_sta_plc As Variant
Dim i As Integer
Dim k As Integer
Dim stringa As String
Dim old_stringa As String
Dim num_ord As Integer
Dim flag As Boolean
Dim rie_for_ord_cor(1 To 3) As Boolean
Dim j As Integer
Dim ord As Tip_Ordine
Dim liv_tmp As Integer
Dim err_tag As Long
Static ini As Integer
Static old_vel_max_tag As Integer
Static old_num_ord As Integer
Dim vel_max_tag As Integer
Dim vel_cur_tag As Integer
Static old_lim_inf(1 To 3) As Long
Static old_lim_sup(1 To 3) As Long
Const vel_max = 999
Static old_gru_mal As Boolean
Static log_fat_err As Integer
Static log_non_fat_err As Integer
Static OLD_ID_COR As String
Dim ID_COR As String
Dim num_sca_pro As Long
Static old_abi_mis_ang As Boolean
Dim appoggio As String
Dim appoggio1 As String
Dim b As Boolean
'AGrassi_FI 17-04-2007
Static old_dis_cur As Boolean

On Local Error GoTo ges_err
'controllo lo stato della comunicazione con il finsgateway
'ABorelli_FI17/11/2006_1
'If sta_bar.SYSMAC_PLC.FatalError Then
If val(par_mac.com_plc_lan.abi_lan) = 0 Then
  b = sta_bar.SYSMAC_PLC.FatalError
Else
  b = sta_bar.Fins.error
End If
If b Then
      ' errore sul PLC
      RaiseEvent Mac(ness_mac)
      RaiseEvent reset(ness_reset)
      RaiseEvent run(7)
      RaiseEvent gruall(0)
      sta_tag.com_OK = False
      If val(par_mac.com_plc_lan.abi_lan) = 0 Then      'ABorelli_FI17/11/2006_1

      messaggio "PLC: " & sta_bar.SYSMAC_PLC.FatalErrorDescription, True, True
      If log_fat_err = 0 Then
          log_str sta_bar.SYSMAC_PLC.FatalErrorDescription, MED_PRIORITY, log_plc
          log_fat_err = 1
        End If
      End If
Else
    'leggo DM 3600-3699
    'ABorelli_FI17/11/2006_1
    If val(par_mac.com_plc_lan.abi_lan) = 0 Then
    If sta_bar.SYSMAC_PLC.NonfatalError And log_non_fat_err = 0 Then
        log_str sta_bar.SYSMAC_PLC.NonfatalErrorDescription, MED_PRIORITY, log_plc
        log_non_fat_err = 1
    End If
    ret_sta_plc = sta_bar.SYSMAC_PLC.readArea(plcAreaDM, 3600, 100, vbVariant Or vbArray)
    Else
      sta_bar.Fins.readArea plcAreaDM, 3600, 100
      ret_sta_plc = sta_bar.Fins.RetData
    End If
    'assegno lo stato in base alla risposta del PLC
    ass_stato ret_sta_plc
    'conto il numero di ordini presenti in collezione ordini (questo dato mi serve per alcune condizioni)
    num_ord = ord_mac.Count
    chk_all_sftw  'controllo gli allarmi Software
    'GESTIONE ALLARMI
    If old_stato.all_PLC.gru_all <> stato.all_PLC.gru_all _
     Or old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id _
     Or old_ind.unit_id <> par_mac.indirizzo_plc.unit_id Or ini = 0 Then
        RaiseEvent gruall(stato.all_PLC.gru_all)   'gruppo allarmi DM 3619
    End If
    If old_stato.all_PLC.All_att <> stato.all_PLC.All_att _
     Or old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id _
     Or old_ind.unit_id <> par_mac.indirizzo_plc.unit_id Or ini = 0 Then
        RaiseEvent sezall(stato.all_PLC.All_att)    'stringa dei bit allarme DM 3620-3629
    End If
    
    'GESTIONE PER L'ORDINE SUCCESSIVO
    If num_ord >= 2 And stato.sta_PLC.MAC_CAM_ORD_IN_CORSO = "0" Then
      CopyType ord, ord_mac.item(2).ordine, Len(ord_mac.item(2).ordine)
      For i = 1 To 3
          If Trim$(ord.ord(i).Ord_liv) <> "" Then
              'GESTIONE DELLO STATO DELL'ORDINE SUCCESSIVO
              ' codice ord succ, lunghezza di taglio successiva devono coincidere
              ' verifico l'allineamento tra stag e master
              
              If old_stato.cod_ord_suc <> stato.cod_ord_suc Or stato.lun_tag_suc(ord.ord(i).Ord_liv) <> old_stato.lun_tag_suc(ord.ord(i).Ord_liv) _
                Or old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id _
                Or old_ind.unit_id <> par_mac.indirizzo_plc.unit_id Or old_num_ord <> num_ord Or cam_lin Then
                  'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
                  'La lunghezza non deve essere controllata in caso di ordine nullo
                  '''If CInt(stato.cod_ord_suc) = CInt(ord.stag_id) And CLng(stato.lun_tag_suc(ord.ord(i).Ord_liv)) = CLng(val(ord.ord(i).sca_lun) \ TODECMM) Then
                  '''        RaiseEvent ord(ord_ok, SUCC)
                  '''Else
                  '''        RaiseEvent ord(ord_nok, SUCC)
                  '''        Exit For
                  '''End If
                  If CInt(stato.cod_ord_suc) = CInt(ord.stag_id) And _
                    (ordineVuoto(ord) Or _
                     (CLng(stato.lun_tag_suc(ord.ord(i).Ord_liv)) = CLng(val(ord.ord(i).sca_lun) \ TODECMM))) Then
                          RaiseEvent ord(ord_ok, SUCC)
                  Else
                          RaiseEvent ord(ord_nok, SUCC)
                          Exit For
                  End If
              End If
          End If
      Next i
    Else
      If Not (num_ord >= 2) Then
        If old_num_ord <> num_ord Or ini = 0 _
         Or old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id _
         Or old_ind.unit_id <> par_mac.indirizzo_plc.unit_id Or cam_lin Then
          RaiseEvent ord(ord_ass, SUCC)
        End If
      End If
    End If
    
    'GESTIONE PER L'ORDINE CORRENTE
    If num_ord >= 1 Then
      CopyType ord, ord_mac.item(1).ordine, Len(ord_mac.item(1).ordine)
      If stato.sta_PLC.MAC_CAM_ORD_IN_CORSO = "0" Then
        ID_COR = Trim$(ord.stag_id) + Trim$(ord.mod_id)
        For i = 1 To 3
            If Trim$(ord.ord(i).Ord_liv) <> "" Then
                'GESTIONE DELLO STATO DELL'ORDINE CORRENTE
                ' codice ord corrente, lunghezza di taglio corrente devono coincidere
                ' verifico l'allineamento tra stag e master
                'agrassi saica 02-11-2007
'                If old_stato.cod_ord_cor <> stato.cod_ord_cor Or stato.ord_plc(ord.ord(i).Ord_liv).Lun_tag <> old_stato.ord_plc(ord.ord(i).Ord_liv).Lun_tag _
'                  Or old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id _
'                  Or old_ind.unit_id <> par_mac.indirizzo_plc.unit_id Or num_ord <> old_num_ord Or cam_lin Or ini = 0 Then
                If old_stato.cod_ord_cor <> stato.cod_ord_cor Or stato.ord_plc(ord.ord(i).Ord_liv).Lun_tag <> old_stato.ord_plc(ord.ord(i).Ord_liv).Lun_tag _
                  Or old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id _
                  Or old_ind.unit_id <> par_mac.indirizzo_plc.unit_id Or num_ord <> old_num_ord Or cam_lin Or ini = 0 Or old_dis_cur Then
                    'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
                    'La lunghezza non deve essere controllata in caso di ordine nullo
                    '''If CInt(stato.cod_ord_cor) = CInt(ord.stag_id) And CLng(stato.ord_plc(ord.ord(i).Ord_liv).Lun_tag) = CLng(val(ord.ord(i).sca_lun) \ TODECMM) Then
                    If CInt(stato.cod_ord_cor) = CInt(ord.stag_id) And _
                       (ordineVuoto(ord) Or _
                        (CLng(stato.ord_plc(ord.ord(i).Ord_liv).Lun_tag) = CLng(val(ord.ord(i).sca_lun) \ TODECMM))) Then
                          RaiseEvent ord(ord_ok, CORR)
                          'AGrassi_FI 17-04-2007
                          If (old_dis_cur) Then
                            old_dis_cur = False
                            log_str "---Fine corr.dis.", MED_PRIORITY, log_ord
                          End If
                    Else
                          RaiseEvent ord(ord_nok, CORR)
                          'AGrassi_FI 17-04-2007
                          If (Not old_dis_cur) Then
                            old_dis_cur = True
                            log_str "--->Corr.dis. Plc=" + stato.cod_ord_cor + ", Stag=" + ord.stag_id + ", lev=" + Format(i) + ": Plc=" + Format(stato.ord_plc(ord.ord(i).Ord_liv).Lun_tag) + ", Stag=" + Format(ord.ord(i).sca_lun \ TODECMM), MED_PRIORITY, log_ord
                          End If
                          Exit For
                    End If
                End If
            End If
        Next i
      End If
      
      For i = 1 To 3
        'If Trim$(ord.ord(i).Ord_liv) <> "" Then
          liv_tmp = ord_mac.item(1).ind_liv(i)
          If liv_tmp <> 0 Then
            rie_for_ord_cor(liv_tmp) = True
            vel_cur_tag = cal_vel_cur_tag(val((ord.ord(i).Ord_liv)), CLng(val(ord.ord(i).sca_lun) \ TODECMM))
            vel_max_tag_liv(liv_tmp) = Min_num(val(par_mac.par_cfg_att(val((ord.ord(i).Ord_liv))).vel_max_tag), vel_cur_tag)
            'GESTIONE DELL'ERRORE DI TAGLIO
            'se l'errore di taglio del livello i-esimo letto dal PLC è cambiato alzo un evento per notificarlo ai client
            If old_stato.ord_plc(val(ord.ord(i).Ord_liv)).err_tag <> stato.ord_plc(val(ord.ord(i).Ord_liv)).err_tag Or _
              old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id Or _
              old_ind.unit_id <> par_mac.indirizzo_plc.unit_id Or num_ord <> old_num_ord Or ID_COR <> OLD_ID_COR Or _
              old_lim_inf(val(ord.ord(i).Ord_liv)) <> par_mac.sch_tag.sch_tag_liv(val(ord.ord(i).Ord_liv)).lim_inf_tol_tag Or _
              old_lim_sup(val(ord.ord(i).Ord_liv)) <> par_mac.sch_tag.sch_tag_liv(val(ord.ord(i).Ord_liv)).lim_sup_tol_tag Or _
              ini = 0 Or old_abi_mis_ang <> abi_mis_ang Then
                  old_lim_inf(ord.ord(i).Ord_liv) = par_mac.sch_tag.sch_tag_liv(ord.ord(i).Ord_liv).lim_inf_tol_tag
                  old_lim_sup(ord.ord(i).Ord_liv) = par_mac.sch_tag.sch_tag_liv(ord.ord(i).Ord_liv).lim_sup_tol_tag
                  If ((stato.ord_plc(val(ord.ord(i).Ord_liv)).err_tag) And (2 ^ 15)) = 2 ^ 15 Then 'errore negativo
                    err_tag = stato.ord_plc(val(ord.ord(i).Ord_liv)).err_tag - (2 ^ 16)
                  Else
                    err_tag = stato.ord_plc(val(ord.ord(i).Ord_liv)).err_tag
                  End If
                  RaiseEvent err(liv_tmp, CStr(err_tag))
            End If
            
            'GESTIONE DEL NUMERO DI SCATOLE PRODOTTE
            'se il numero di tagli effettuati cambia lo notifico ai client
            If old_stato.ord_plc(val(ord.ord(i).Ord_liv)).num_fog_buo <> stato.ord_plc(val(ord.ord(i).Ord_liv)).num_fog_buo _
              Or old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id _
              Or old_ind.unit_id <> par_mac.indirizzo_plc.unit_id Or num_ord <> old_num_ord Or ID_COR <> OLD_ID_COR Or ini = 0 Then
                  num_sca_pro = (val(stato.ord_plc(val(ord.ord(i).Ord_liv)).num_fog_buo) * val(ord.ord(i).Sca_per_tag))
                  RaiseEvent num(liv_tmp, CStr(num_sca_pro))
            End If
            
            'GESTIONE DEL NUMERO DI TAGLI ERRATI
            'se il numero di tagli effettuati cambia lo notifico ai client
            If old_stato.ord_plc(val(ord.ord(i).Ord_liv)).num_fog_err <> stato.ord_plc(val(ord.ord(i).Ord_liv)).num_fog_err _
               Or old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id _
               Or old_ind.unit_id <> par_mac.indirizzo_plc.unit_id Or num_ord <> old_num_ord Or ini = 0 Or ID_COR <> OLD_ID_COR Then
                    RaiseEvent numerr(liv_tmp, CStr(val(stato.ord_plc(val(ord.ord(i).Ord_liv)).num_fog_err) * val(ord.ord(i).Sca_per_tag)))
            End If
          Else
            For k = 1 To 3
              If Not rie_for_ord_cor(k) Then
                  RaiseEvent err(k, "")
                  RaiseEvent numerr(k, "")
                  RaiseEvent num(k, "")
              End If
            Next k
          End If
'        Else
'          For k = 1 To 3
'            If Not rie_for_ord_cor(k) Then
'                RaiseEvent err(k, "")
'                RaiseEvent numerr(k, "")
'                RaiseEvent num(k, "")
'            End If
'          Next k
'        End If
      Next i
    Else
      If Not (num_ord >= 1) Then
        If old_num_ord <> num_ord Or ini = 0 _
         Or old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id _
         Or old_ind.unit_id <> par_mac.indirizzo_plc.unit_id Or cam_lin Then
          RaiseEvent ord(ord_ass, CORR)
          Select Case val(par_mac.car_tec.num_liv_tag)
              Case 1
                vel_max_tag_liv(3) = val(par_mac.par_cfg_att(1).vel_max_tag)
              Case 2
                vel_max_tag_liv(1) = val(par_mac.par_cfg_att(1).vel_max_tag)
                vel_max_tag_liv(3) = val(par_mac.par_cfg_att(2).vel_max_tag)
              Case 3
                For k = 1 To val(par_mac.car_tec.num_liv_tag)
                  vel_max_tag_liv(k) = val(par_mac.par_cfg_att(k).vel_max_tag)
                Next k
          End Select
        End If
      End If
    End If
          
    'GESTIONE DELLA VELOCITA' DI LINEA
    'se la velocità di linea letta dal PLC è cambiata alzo un evento per notificare la nuova velocità ai client
    If Not (StrComp(stato.vel_lin(1), old_stato.vel_lin(1)) = 0) Or ini = 0 _
      Or old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id _
      Or old_ind.unit_id <> par_mac.indirizzo_plc.unit_id Or num_ord <> old_num_ord Or old_abi_mis_ang <> abi_mis_ang Then
        RaiseEvent vel(CLng(stato.vel_lin(1)))
    End If
    
    'GESTIONE DELLA VELOCITA' MASSIMA
    If num_ord > 0 Then
        vel_max_tag = vel_max
        For i = 1 To 3
          If rie_for_ord_cor(i) Then
            If vel_max_tag > vel_max_tag_liv(i) Then vel_max_tag = vel_max_tag_liv(i)
          End If
        Next i
    Else
        vel_max_tag = val(par_mac.par_cfg_att(1).vel_max_tag)
        For i = 2 To val(par_mac.car_tec.num_liv_tag)
            If vel_max_tag > val(par_mac.par_cfg_att(i).vel_max_tag) Then vel_max_tag = val(par_mac.par_cfg_att(i).vel_max_tag)
        Next i
    End If
    If vel_max_tag = 999 Then
        vel_max_tag = val(par_mac.par_cfg_att(1).vel_max_tag)
        For i = 2 To val(par_mac.car_tec.num_liv_tag)
            If vel_max_tag > val(par_mac.par_cfg_att(i).vel_max_tag) Then vel_max_tag = val(par_mac.par_cfg_att(i).vel_max_tag)
        Next i
    End If
    If vel_max_tag <> old_vel_max_tag Or ini = 0 _
      Or old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id _
      Or old_ind.unit_id <> par_mac.indirizzo_plc.unit_id Or num_ord <> old_num_ord Or old_abi_mis_ang <> abi_mis_ang Then
        old_vel_max_tag = vel_max_tag
        RaiseEvent velmax(vel_max_tag)
    End If
          
    old_num_ord = num_ord
    OLD_ID_COR = ID_COR
    'GESTIONE DELLO STATO RUNNING
    ' l'ordine di composizione della stringa viene dato dalla priorità degli stati
    stringa = stato.sta_PLC.MAC_CAM_ORD_IN_CORSO _
                    & stato.sta_PLC.MAC_CAM_ORD_COMPLETATO _
                    & stato.sta_PLC.MAC_IN_PREALLARME _
                    & stato.sta_PLC.MAC_PRONTA_AL_CAMBIO _
                    & stato.sta_PLC.MAC_IN_MARCIA _
                    & stato.sta_PLC.MAC_PRONTA_MARCIA _

    old_stringa = old_stato.sta_PLC.MAC_CAM_ORD_IN_CORSO _
                    & old_stato.sta_PLC.MAC_CAM_ORD_COMPLETATO _
                    & old_stato.sta_PLC.MAC_IN_PREALLARME _
                    & old_stato.sta_PLC.MAC_PRONTA_AL_CAMBIO _
                    & old_stato.sta_PLC.MAC_IN_MARCIA _
                    & old_stato.sta_PLC.MAC_PRONTA_MARCIA _

    If Not (StrComp(old_stringa, stringa) = 0) _
      Or old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id _
      Or old_ind.unit_id <> par_mac.indirizzo_plc.unit_id Or cam_lin Then
        k = 0
        For i = 1 To Len(stringa)
            If Mid(stringa, i, 1) = "1" Then
                k = i
                Exit For
            End If
        Next i
                    
        Select Case k
        
        Case 1
            stato.sta_run = co_in_cor
            RaiseEvent run(1)        ' cambio ordine in corso
        Case 2
            stato.sta_run = co_com
            RaiseEvent run(2)        ' cambio ordine completato
        Case 3
            stato.sta_run = pre
            RaiseEvent run(3)       ' macchina in preallarme
        Case 4
            stato.sta_run = pro_cam
            RaiseEvent run(4)       ' macchina pronta al cambio
        Case 5
            stato.sta_run = in_mar
            RaiseEvent run(5)       ' macchina in marcia
        Case 6
            stato.sta_run = pro_mar
            RaiseEvent run(6)       ' macchina pronta alla marcia
        Case Else
            stato.sta_run = fer
            RaiseEvent run(7)       ' macchina ferma (Emergenza)
        End Select
    End If
    
    tempi False, False, False          ' aggiornamento tempi di fermo   ABorelli_FI09/09/2005_1

    'GESTIONE DEL BIT DI CAMBIO ORDINE COMPLETATO
    If (old_stato.sta_PLC.MAC_CAM_ORD_COMPLETATO <> stato.sta_PLC.MAC_CAM_ORD_COMPLETATO) Then
      'Or old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id
      'Or old_ind.unit_id <> par_mac.indirizzo_plc.unit_id) Or cam_lin Then
        If stato.sta_PLC.MAC_CAM_ORD_COMPLETATO = "1" Then
          'transizione del bit da 0 a 1 => faccio lo shift della lista ordini
          If Trim$(pro_set.ini_prod) = STR_TRUE Then  'se è il primo cambio ordine della giornata o comunque dopo l'inizio produzione
            If Trim$(ord_mac.item(1).stag_id) = Trim$(ord_mac_ese.item(1).old_stag_id) Then
              'caso in cui avevo fatto una fine produzione per un ordine non completamente finito
              'in questo caso infatti mi ritrovo lo stesso ordine nella prima posizione degli eseguiti => vado a cancellare tale record perché con lo shift
              'e con la successiva lettura dei dati di produzione inserirò di nuovo l'ordine nella lista degli eseguiti con il report di produzione aggiornato
              ret = ord_mac_ese.Del_blk(1, 1, False, False, True)
              If ret <> RET_OK Then log_str "aggiorna: Cancellazione ordine da ord_mac_ese per aggiornarlo con i nuovi dati di produzione NON ESEGUITA", MED_PRIORITY, log_ord
              pro_set.ini_prod = STR_FALSE 'non mi aspetto più il primo cambio ordine dopo un fine produzione
              setFileINIString "PRO_SET", "ini_prod", pro_set.ini_prod    'scrivo su file
            End If
          End If
          ret = Shifta()
        Else
          'transizione del bit da 1 a 0 => leggo i dati dell'ultimo ordine prodotto
          If ini = 1 Then rea_dat_ord_pro (False)
        End If
    End If
    
    'GESTIONE DELLO STATO DI RESET
    ' l'ordine di composizione della stringa viene dato dalla priorità degli stati
    stringa = stato.sta_PLC.MAC_E_STOP _
                    & stato.sta_PLC.MAC_F_STOP _
                    & stato.sta_PLC.MAC_N_STOP _
                    & stato.sta_PLC.MAC_RIPRISTINATA
                    
    old_stringa = old_stato.sta_PLC.MAC_E_STOP _
                    & old_stato.sta_PLC.MAC_F_STOP _
                    & old_stato.sta_PLC.MAC_N_STOP _
                    & old_stato.sta_PLC.MAC_RIPRISTINATA
                 
    If Not (StrComp(old_stringa, stringa) = 0) _
      Or old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id _
      Or old_ind.unit_id <> par_mac.indirizzo_plc.unit_id Or cam_lin Then
        k = 0
        For i = 1 To Len(stringa)
            If Mid(stringa, i, 1) = "1" Then
                k = i
                Exit For
            End If
        Next i
        Select Case k
        
        Case 1
            RaiseEvent reset(e_stop)
        Case 2
            RaiseEvent reset(f_stop)
        Case 3
            RaiseEvent reset(n_stop)
        Case 4
            RaiseEvent reset(rip)
        Case Else
            RaiseEvent reset(ness_reset)
        End Select
    End If
        
    'GESTIONE STATO DELLA MACCHINA
    'stati della macchina : in manutenzione, in simulazione, in lappatura
    
    stringa = stato.sta_PLC.MAC_IN_MANUTENZIONE & stato.sta_PLC.MAC_IN_SIMULAZIONE _
                  & stato.sta_PLC.MAC_IN_LAPPATURA
    
    old_stringa = old_stato.sta_PLC.MAC_IN_MANUTENZIONE & old_stato.sta_PLC.MAC_IN_SIMULAZIONE _
                          & old_stato.sta_PLC.MAC_IN_LAPPATURA
    
    If Not (StrComp(old_stringa, stringa) = 0) _
      Or old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id _
      Or old_ind.unit_id <> par_mac.indirizzo_plc.unit_id Or cam_lin Then
        
        k = 0
        For i = 1 To Len(stringa)
            If Mid(stringa, i, 1) = "1" Then
                k = i
                Exit For
            End If
        Next i
        If k = 1 And Mid$(stringa, 3, 1) = 1 Then k = 3 'se è in manutenzione ed in lappatura => faccio vedere la lappatura
        Select Case k
        
          Case 1
              RaiseEvent Mac(mac_in_man)
          Case 2
              RaiseEvent Mac(mac_in_sim)
          Case 3
              RaiseEvent Mac(mac_in_lap)
          Case Else
              RaiseEvent Mac(ness_mac)
        End Select
        
    End If
    
    'stati della macchina : in manuale o in automatico
    
    If stato.sta_PLC.MAC_IN_MANUALE <> old_stato.sta_PLC.MAC_IN_MANUALE _
      Or old_ind.net_id <> par_mac.indirizzo_plc.net_id Or old_ind.node_id <> par_mac.indirizzo_plc.node_id _
      Or old_ind.unit_id <> par_mac.indirizzo_plc.unit_id Or cam_lin Then
        If stato.sta_PLC.MAC_IN_MANUALE = "1" Then
          RaiseEvent modo(manuale)
        Else
          RaiseEvent modo(automatico)
        End If
    End If
    If cam_lin Or ini = 0 Then
      RaiseEvent liv
    End If
    'Si logga ogni cambio stato della macchina
    appoggio = Space$(Len(stato.sta_PLC))
    appoggio1 = Space$(Len(old_stato.sta_PLC))
    CopyString appoggio, stato.sta_PLC, Len(stato.sta_PLC)
    CopyString appoggio1, old_stato.sta_PLC, Len(old_stato.sta_PLC)
    If appoggio <> appoggio1 Then
      log_str "Stato Macchina: " + appoggio, MED_PRIORITY, log_plc
    End If
    'memorizzo lo stato precedente e l'eventuale indirizzo precedente del PLC
    old_stato = stato
    old_ind = par_mac.indirizzo_plc
    old_abi_mis_ang = abi_mis_ang
    ini = 1
    cam_lin = False
End If
Exit Sub
ges_err:
 'agrassi_FI09-09-09_1
 old_stato = stato
 If err.Number = 57 Then 'problema con l'indirizzo finsgateway
    'Metto ini = 0 in modo che quando riparto con la comunicazione l'aggiorna rialzi tutti gli eventi: non basta testare il vecchio ed il nuovo indirizzo del PLC
    'CASO PARTICOLARE TROVATO CASUALMENTE: in caso di avvio di una nuova applicazione e di ritorno a stag (non chiudendo l'altro programma ma
    'abbassandolo a icona) viene generato un errore (err.number = 57) di indirizzo sbagliato per l'oggetto compolet anche se in realtà è quello giusto
    ini = 0
    sta_bar.mes_PLC par_mac.indirizzo_plc.net_id, par_mac.indirizzo_plc.node_id, par_mac.indirizzo_plc.unit_id
    Exit Sub
Else
  If err.Number = 6 Then
    messaggio "OVERFLOW", True, True
    log_str "sub aggiorna: OVERFLOW" + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Else
    log_str "on error intervenuta in ClasseStato aggiorna" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  End If
End If
End Sub
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Sub tempi(res_ord As Boolean, res_tur As Boolean, res_gio As Boolean)
' ABorelli_FI09/09/2005_1
Static las_tic As Long      ' ultima volta che e' stata chiamata la routine
Static las_tic_fer_ord          ' istante di ultima totalizzazione fermata attuale, se <> 0 c'e' fermata in corso
Static las_tic_fer_gio          ' istante di ultima totalizzazione fermata attuale, se <> 0 c'e' fermata in corso
Static las_tic_fer_tur          ' istante di ultima totalizzazione fermata attuale, se <> 0 c'e' fermata in corso
Dim inc As Integer, tic As Long
Dim sec As Long
Dim d1 As Date, d2 As Date
'andrea per rom
On Error GoTo g_err

If res_ord = True Then                                  ' comando di reset ordine
    CopyType tot_ese, Space$(Len(tot_ese)), Len(tot_ese)
    las_tic_fer_ord = 0
End If
If res_gio = True Then                                  ' comando di reset giorno
    CopyType tot_tg, Space$(Len(tot_tg)), Len(tot_tg)
    las_tic_fer_gio = 0
    las_tic_fer_tur = 0
    tot_tg.gio.dat_ini = Format$(Now, "yyyymmdd")
    tot_tg.gio.ora_ini = Format$(Now, "hhmmss")
    tot_tg.tur(pro_set.tur).dat_ini = Format$(Now, "yyyymmdd")
    tot_tg.tur(pro_set.tur).ora_ini = Format$(Now, "hhmmss")
End If
If res_tur = True Then                                  ' comando di reset turno
    CopyType tot_tg.tur(pro_set.tur), Space$(Len(tot_tg.tur(pro_set.tur))), Len(tot_tg.tur(pro_set.tur))
    las_tic_fer_tur = 0
    tot_tg.tur(pro_set.tur).dat_ini = Format$(Now, "yyyymmdd")
    tot_tg.tur(pro_set.tur).ora_ini = Format$(Now, "hhmmss")
End If
tic = GetTickCount()    ' acquisisco il tempo
If las_tic = 0 Then ' prima volta che si chiama la routine
    las_tic = tic
    Exit Sub
End If
If tic < las_tic Then     ' la GetTickCount() e' andata in overflow
    las_tic = tic
    If las_tic_fer_ord <> 0 Then
        las_tic_fer_ord = tic
    End If
    If las_tic_fer_gio <> 0 Then
        las_tic_fer_gio = tic
    End If
    If las_tic_fer_tur <> 0 Then
        las_tic_fer_tur = tic
    End If
End If
If tic - las_tic < 1000 Then             ' troppo presto
    Exit Sub
End If
If pro_set.abi_reg_rep_pro = STR_TRUE Then      ' si registrano le fermate
    If stato.vel_san_ing <> 0 Then  ' siamo normalmente in marcia o la linea e' ripartita
            las_tic_fer_ord = 0
            las_tic_fer_gio = 0
            las_tic_fer_tur = 0
    Else
        If las_tic_fer_ord = 0 Then     ' inizio fermata
            las_tic_fer_ord = tic
            tot_ese.num_fer = val(tot_ese.num_fer) + 1
            tot_tg.gio.num_fer = val(tot_tg.gio.num_fer) + 1
            tot_tg.tur(pro_set.tur).num_fer = val(tot_tg.tur(pro_set.tur).num_fer) + 1
        Else
            inc = (tic - las_tic_fer_ord) \ 1000        ' in secondi
            tot_ese.tem_fer = val(tot_ese.tem_fer) + inc
            las_tic_fer_ord = las_tic_fer_ord + inc * 1000&
        End If
        If las_tic_fer_gio = 0 Then     ' inizio fermata x giorno
            las_tic_fer_gio = tic
        Else
            inc = (tic - las_tic_fer_gio) \ 1000
            tot_tg.gio.tem_fer = val(tot_tg.gio.tem_fer) + inc
            las_tic_fer_gio = las_tic_fer_gio + inc * 1000&
        End If
        If las_tic_fer_tur = 0 Then     ' inizio fermata x giorno
            las_tic_fer_tur = tic
        Else
            inc = (tic - las_tic_fer_tur) \ 1000
            tot_tg.tur(pro_set.tur).tem_fer = val(tot_tg.tur(pro_set.tur).tem_fer) + inc
            las_tic_fer_tur = las_tic_fer_tur + inc * 1000&
        End If
    End If
Else
    las_tic_fer_ord = 0
    las_tic_fer_gio = 0
    las_tic_fer_tur = 0
End If
If Trim$(tot_tg.tur(pro_set.tur).dat_ini) <> "" And Trim$(tot_tg.tur(pro_set.tur).ora_ini) <> "" Then
    d1 = DateSerial(val(Mid$(tot_tg.tur(pro_set.tur).dat_ini, 1, 4)), val(Mid$(tot_tg.tur(pro_set.tur).dat_ini, 5, 2)), val(Mid$(tot_tg.tur(pro_set.tur).dat_ini, 7, 2)))
    d2 = TimeSerial(val(Mid$(tot_tg.tur(pro_set.tur).ora_ini, 1, 2)), val(Mid$(tot_tg.tur(pro_set.tur).ora_ini, 3, 2)), val(Mid$(tot_tg.tur(pro_set.tur).ora_ini, 5, 2)))
    d1 = d1 + d2
    sec = DateDiff("s", d1, Now)
    tot_tg.tur(pro_set.tur).tem_mar = Format$(sec - val(tot_tg.tur(pro_set.tur).tem_fer))
End If
If Trim$(tot_tg.gio.dat_ini) <> "" And Trim$(tot_tg.gio.ora_ini) <> "" Then
    d1 = DateSerial(val(Mid$(tot_tg.gio.dat_ini, 1, 4)), val(Mid$(tot_tg.gio.dat_ini, 5, 2)), val(Mid$(tot_tg.gio.dat_ini, 7, 2)))
    d2 = TimeSerial(val(Mid$(tot_tg.gio.ora_ini, 1, 2)), val(Mid$(tot_tg.gio.ora_ini, 3, 2)), val(Mid$(tot_tg.gio.ora_ini, 5, 2)))
    d1 = d1 + d2
    sec = DateDiff("s", d1, Now)
    tot_tg.gio.tem_mar = Format$(sec - val(tot_tg.gio.tem_fer))
End If
las_tic = tic
'andrea per rom
Exit Sub

g_err:
log_str "tempi: errore " + Format(err.Number) + ", " + err.Description, MED_PRIORITY, log_lis
Resume Next
End Sub
Private Sub ass_stato(DM As Variant)
Dim str_all As String
Dim i As Integer
Dim app As String
'AGrassi_FI 17-04-2007
Dim plc_log_cur As String
Static old_plc_log_cur As String
'agrassi_FI15-04-2001_1
Dim pres_vel As Integer
Static old_vel As Integer

plc_log_cur = Format(DM(2)) + "*"
plc_log_cur = plc_log_cur + Format(DM(3)) + "*"
plc_log_cur = plc_log_cur + Format(DM(8)) + "*"
plc_log_cur = plc_log_cur + Format(DM(13))
If (plc_log_cur <> old_plc_log_cur) Then
  log_str "Plc_cur: " + plc_log_cur, MED_PRIORITY, log_ord
  old_plc_log_cur = plc_log_cur
End If
CopyType stato.sta_PLC, hex_bin(int_esa(DM(0)), True) + hex_bin(int_esa(DM(1)), True), 32     'stringa di bit per stato macchina
If sta_tag_ute.MAC_IN_MANUALE_UTE Then  '<> stato.sta_PLC.MAC_IN_MANUALE
      stato.sta_PLC.MAC_IN_MANUALE = STR_TRUE
End If
'app = Space$(Len(stato.sta_PLC))
'CopyString app, stato.sta_PLC, Len(stato.sta_PLC)
stato.cod_ord_cor = DM(2)
stato.ord_plc(1).Lun_tag = DM(3)
stato.ord_plc(1).num_fog_buo = leg_lng(int_esa(DM(4)), int_esa(DM(5)))    'decimale con segno doppia lunghezza
stato.ord_plc(1).num_fog_err = DM(6)
stato.ord_plc(1).err_tag = DM(7)
stato.ord_plc(2).Lun_tag = DM(8)
stato.ord_plc(2).num_fog_buo = leg_lng(int_esa(DM(9)), int_esa(DM(10)))    'decimale con segno doppia lunghezza
stato.ord_plc(2).num_fog_err = DM(11)
stato.ord_plc(2).err_tag = DM(12)
stato.ord_plc(3).Lun_tag = DM(13)
stato.ord_plc(3).num_fog_buo = leg_lng(int_esa(DM(14)), int_esa(DM(15)))    'decimale con segno doppia lunghezza
stato.ord_plc(3).num_fog_err = DM(16)
stato.ord_plc(3).err_tag = DM(17)
stato.cod_ord_suc = DM(20)
stato.lun_tag_suc(1) = DM(21)
stato.lun_tag_suc(2) = DM(22)
stato.lun_tag_suc(3) = DM(23)

'PMASSAROFI_05/07/11 - Aggiunta ricezione ordini da MHI via BCD
stato.next_cut_len_from_external(1) = DM(25)   '1° Livello  - INF
stato.next_cut_len_from_external(2) = DM(26)   '2° Livello  - MED/SUP
stato.next_cut_len_from_external(3) = DM(27)   '3° Livello  - SUP

'I dati dell'ultimo ordine prodotto li leggo solo alla fine del cambio ordine
stato.vel_lin(1) = DM(50)
'agrassi_FI15-04-2001_1
pres_vel = val(DM(50))
If (Abs(pres_vel - old_vel) > 2) Then
  log_str "Speed: " + Format(pres_vel), MED_PRIORITY, log_plc
  old_vel = pres_vel
End If
stato.vel_lin(2) = DM(51)
stato.vel_lin(3) = DM(52)
stato.vel_ana_san_ing = DM(53)
stato.vel_san_ing = DM(54)
stato.vel_san_usc(1) = DM(55)
stato.vel_san_usc(2) = DM(56)
stato.vel_san_usc(3) = DM(57)
str_all = ""
str_all = hex_bin(int_esa(DM(60)), False)
For i = 61 To 80  'Il DM 3681 è dedicato agli allarmi Software
    str_all = str_all + hex_bin(int_esa(DM(i)), True)
Next i
CopyType stato.all_PLC, str_all, Len(str_all)        'stringa di bit per diagnostica allarmi
'Tolto perché non usato
'If hex_bin(int_esa(DM(81)), True) <> String(16, "0") Then
'  log_str "ClasseStato - Sub: ass_stato. Il DM 3681 letto dal PLC e dedicato agli allarmi Software ha valore:" & hex_bin(int_esa(DM(i)), True), MED_PRIORITY, log_plc
'End If
stato.ris_3500 = DM(98)
stato.ris_3501 = DM(99)

End Sub
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' PROPRIETA' IN SOLA LETTURA DELLO STATO '
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Property Get cod_ord_cor() As String
'Dati letti da: DM 3602
cod_ord_cor = stato.cod_ord_cor
End Property
Public Property Get gru_all_att(pos_gru As Integer) As Boolean
'restituisce "1" se c'è un allarme attivo per il gruppo allarme di posizione pos_gru; "0" altrimenti
If pos_gru < 1 Or pos_gru > 7 Then
  log_str "Property Get GRU_ALL_ATT della ClasseStato: l'indice passato alla property non è corretto", MED_PRIORITY, log_gen
  Exit Property
End If
gru_all_att = stato.gru_all_att(pos_gru)
End Property
Public Property Let gru_all_att(pos_gru As Integer, v As Boolean)
'setta ad "1" o a "0" il GRU_ALL_ATT(pos_gru)
If pos_gru < 1 Or pos_gru > 7 Then
  log_str "Property Let GRU_ALL_ATT della ClasseStato: l'indice passato alla property non è corretto", MED_PRIORITY, log_gen
  Exit Property
End If
stato.gru_all_att(pos_gru) = v
End Property

Public Property Get cod_ord_suc() As String
'Dati letti da: DM 3603
cod_ord_suc = stato.cod_ord_suc
End Property

Public Property Get lun_tag_cor(i As Integer) As Long
'Dati letti da: DM 3604-3606
lun_tag_cor = stato.ord_plc(i).Lun_tag
End Property

Public Property Get lun_tag_suc(i As Integer) As Long
'Dati letti da: DM 3607.3609
lun_tag_suc = stato.lun_tag_suc(i)
End Property

Public Property Get num_tag_eff(i As Integer) As Long
'Dati letti da: DM 3610-3615
num_tag_eff = stato.ord_plc(i).num_fog_buo
End Property

Public Property Get err_tag(i As Integer) As Long
'Dati letti da: DM 3616-3618
err_tag = stato.ord_plc(i).err_tag
End Property

Public Property Get num_fog_sca(i As Integer) As Long
'Dati letti da: DM 3606-3611-3616
num_fog_sca = stato.ord_plc(i).num_fog_err
End Property

Public Property Get vel_lin(i As Integer) As Long
'Dati letti da: DM 3650-3651-3652
vel_lin = stato.vel_lin(i)
End Property


Public Property Get vel_ana_san_ing() As Long
'Dati letti da: DM 3653
vel_ana_san_ing = stato.vel_ana_san_ing
End Property

Public Property Get vel_san_ing() As Long
'Dati letti da: DM 3654
vel_san_ing = stato.vel_san_ing
End Property

Public Property Get vel_san_usc(i As Integer) As Long
'Dati letti da: DM 3655-3656-3657
vel_san_usc = stato.vel_san_usc(i)
End Property

Public Property Get ord_pro_num_fog_buo(i As Integer) As Long
'Dati letti da: DM 3606-3611-3616
ord_pro_num_fog_buo = stato.ord_pro(i).num_fog_buo
End Property

Public Property Get ord_pro_num_fog_err(i As Integer) As Long
'Dati letti da: DM 3606-3611-3616
ord_pro_num_fog_err = stato.ord_pro(i).num_fog_err
End Property

Public Property Get ord_pro_num_fog_sca(i As Integer) As Long
'Dati letti da: DM 3606-3611-3616
ord_pro_num_fog_sca = stato.ord_pro(i).num_fog_sca
End Property

Private Sub Class_Initialize()
stato.spde_OK = True
End Sub


Public Function get_pro_ese() As String
' ritorna la produzione dell'ordine prodotto        ' ABorelli_FI09/09/2005_1
Dim s As String, i As Integer
    s = Space$(Len(tot_ese))
    CopyString s, tot_ese, Len(s)
    get_pro_ese = s
End Function
Public Function get_pro_tg() As String
' ritorna la produzione turno giorno        ' ABorelli_FI09/09/2005_1
Dim s As String
    s = Space$(Len(tot_tg))
    CopyString s, tot_tg, Len(s)
    get_pro_tg = s
End Function
Public Function set_pro_tg(s As String)
' imposta la produzione turno giorno        ' ABorelli_FI09/09/2005_1
    CopyType tot_tg, s, Len(tot_tg)
End Function
Public Function set_pro_ese(s As String)
' imposta la produzione dell'ordine prodotto        ' ABorelli_FI09/09/2005_1
    CopyType tot_ese, s, Len(tot_ese)
End Function
Public Sub lunghezza(met As Long, lar_mm As Long)
' ABorelli_FI09/09/2005_1
' aggiorna i metri fatti per turno e per giorno
Static ini As Integer
Static met_cur As Long, resq As Integer
Dim dif As Long, difq As Long
    If ini <> 0 Then
        dif = met - met_cur
        If dif < 0 Then     ' c'e' stato un cambio ordine
            dif = met
        End If
        difq = Int((dif * lar_mm + resq) / 1000)
        resq = dif * lar_mm + resq - difq * 1000    ' si contera' anche il resto
        tot_tg.gio.met_lin = val(tot_tg.gio.met_lin) + dif
        tot_tg.gio.met_qua = val(tot_tg.gio.met_qua) + difq
        tot_tg.tur(pro_set.tur).met_lin = val(tot_tg.tur(pro_set.tur).met_lin) + dif
        tot_tg.tur(pro_set.tur).met_qua = val(tot_tg.tur(pro_set.tur).met_qua) + difq
        tot_tg.gio.vel_med = agg_med(tot_tg.gio)
        tot_tg.tur(pro_set.tur).vel_med = agg_med(tot_tg.tur(pro_set.tur))
    Else
        ini = 1
    End If
    met_cur = met
End Sub
Private Function agg_med(dat As tip_pro_fosber21_FOS) As Long
' ABorelli_FI09/09/2005_1
Dim d1 As Date, d2 As Date, sec As Long
    agg_med = 0
    If Trim$(dat.dat_ini) <> "" And Trim$(dat.ora_ini) <> "" Then
        d1 = DateSerial(val(Mid$(dat.dat_ini, 1, 4)), val(Mid$(dat.dat_ini, 5, 2)), val(Mid$(dat.dat_ini, 7, 2)))
        d2 = TimeSerial(val(Mid$(dat.ora_ini, 1, 2)), val(Mid$(dat.ora_ini, 3, 2)), val(Mid$(dat.ora_ini, 5, 2)))
        d1 = d1 + d2
        sec = DateDiff("s", d1, Now)
        If sec <> 0 Then
            agg_med = val(dat.met_lin) / sec * 60
        End If
    End If
End Function
Public Sub chg_tur(new_tur As Integer)
' ABorelli_FI09/09/2005_1
Dim d As Date
        tot_tg.tur(pro_set.tur).dat_fin = Format$(Now, "yyyymmdd")
        tot_tg.tur(pro_set.tur).ora_fin = Format$(Now, "hhmmss")
        pro_set.tur = new_tur
        sta_tag.tempi res_ord:=False, res_tur:=True, res_gio:=False
        setFileINIString "pro_set", "tur", Format$(new_tur)
End Sub
Public Sub chg_ord()
'andrea per rom
On Error GoTo g_err
' ABorelli_FI09/09/2005_1
   tot_tg.tur(pro_set.tur).num_cam = Format$(val(tot_tg.tur(pro_set.tur).num_cam) + 1)
   tot_tg.gio.num_cam = Format$(val(tot_tg.gio.num_cam) + 1)
'andrea per rom
Exit Sub
g_err:
log_str "chg_ord: errore " + Format(err.Number) + ", " + err.Description, MED_PRIORITY, log_gen
Resume Next
End Sub
