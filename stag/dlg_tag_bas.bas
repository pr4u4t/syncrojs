Attribute VB_Name = "dlg_tag_bas"
Option Explicit
'PMASSAROFI_05/07/11 - Aggiunta ricezione ordini da MHI via BCD
Dim MisInvPrec(1 To 3) As Long

Function com_plc_ok() As cod_ret
'Verifica se ci sono problemi di comunicazione con il PLC
Dim dm_let As Variant
On Local Error GoTo ErrorHandler

  com_plc_ok = RET_OK
  dm_let = sta_bar.SYSMAC_PLC.DM(0)
  Exit Function
  
ErrorHandler:
  com_plc_ok = RET_BAD
  
End Function



'PMASSAROFI_05/07/11 - Aggiunta ricezione ordini da MHI via BCD
Function PrelevaDatiOrdineDaPLC(NextCutLen As Variant) As Boolean
  Dim risp As Boolean
  Dim temp As ClasseOrdine
  Dim pos_in_que As Integer
  Dim pos_in_list As Integer
  Dim ordine As Tip_Ordine
  Dim ret As cod_ret
  
  risp = False
    
  If sta_bar.led_spde_rx.FillColor = vbGreen Then
     sta_bar.led_spde_rx.FillColor = COL_DARK_Green
  Else
     sta_bar.led_spde_rx.FillColor = vbGreen
  End If
  
  If sta_bar.led_spde_tx.FillColor = sta_bar.led_spde_rx.FillColor Then
     sta_bar.led_spde_tx.FillColor = IIf(sta_bar.led_spde_rx.FillColor = vbGreen, COL_DARK_Green, vbGreen)
  End If
  
  If Not sta_tag.MAC_IN_MANUALE(1) Then sta_bar.lin_no_com_spde.Visible = False
  
  If Not sta_tag.MAC_IN_MANUALE(1) And Not sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1) Then
    Dim ord As Tip_Com_ord  'variabile per le operazioni sulla lista degli ordini
    ord = rie_ord_da_PLC(NextCutLen, ord) 'richiamo e assegno la funzione che riempie la struttura dati da inviare a stag
    val_ord ord 'validazione ordine
  
    If ord.ret = RET_OK Then
      Set temp = New ClasseOrdine
      pos_in_que = 2
      CopyType ordine, Space$(Len(ordine)), Len(ordine)
      ordine = ord.rec
      temp.ordine = Ord_str(ordine)
      If ord_mac.Count < 1 Then
        ret = RET_BAD
      ElseIf ord_mac.Count = 1 Then
        pos_in_list = 2
        ret = ord_mac.add(temp, pos_in_list, True)
      ElseIf ord_mac.Count > 1 Then
        pos_in_list = 2
        ret = ord_mac.Del_blk(pos_in_list, ord_mac.Count, False, True)
        If ret = RET_OK Then
          ret = ord_mac.add(temp, pos_in_list, True)
        End If
      End If
      If ret = RET_BAD Then
        messaggio msg("rif_ord_spde"), True, False
        log_str "tim_tag_plc - sub PrelevaDatiOrdineDaPLC: Failed adding in order list pos ", MED_PRIORITY, log_spde
      Else
        messaggio msg("ric_ord_spde") & " (" & pos_in_que & ") ", False, False
        log_str "tim_tag_plc - sub PrelevaDatiOrdineDaPLC: Successfully added record posizione " + CStr(pos_in_list) _
        + " SPDE_ID/MOD_ID " + ord.rec.spde_id + "/" + ord.rec.spde_ModID, MED_PRIORITY, log_spde
        Dim pTmp As Integer
        For pTmp = 1 To 3
          MisInvPrec(pTmp) = NextCutLen(pTmp)
        Next pTmp
        risp = True
      End If
    Else
      If (ord.ret <> RET_OK) Then
        log_str "tim_tag_plc - sub PrelevaDatiOrdineDaPLC: ordine ricevuto NON valido. Codice di errore: " + Format(ord.ret), MED_PRIORITY, log_spde
      Else
        log_str "tim_tag_plc - sub PrelevaDatiOrdineDaPLC: ordine ricevuto NON formattato correttamente", MED_PRIORITY, log_spde
      End If
    End If
  Else
    'macchina in manuale o cambio ordine in corso
    messaggio msg("rif_ord_spde"), True, False
    If sta_tag.MAC_IN_MANUALE(1) Then
      log_str "tim_tag_plc - sub PrelevaDatiOrdineDaPLC: Invio/Modifica ordine rifiutato: Macchina in manuale", MED_PRIORITY, log_spde
    ElseIf sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1) Then
      log_str "tim_tag_plc - sub PrelevaDatiOrdineDaPLC: Invio/Modifica ordine rifiutato: Cambio ordine in corso", MED_PRIORITY, log_spde
    End If
  End If
  
  PrelevaDatiOrdineDaPLC = risp
End Function

Function rie_ord_da_PLC(lenght As Variant, ordine As Tip_Com_ord) As Tip_Com_ord
'Indice 1 = L1 - INF
'Indice 2 = L2 - MED
'Indice 3 = L3 - SUP
Dim i As Integer
Dim lastIdx As Integer
Dim mt_tot_max As Long
lastIdx = 1
mt_tot_max = 0
For i = 1 To 3
  With ordine.rec.ord(lastIdx)
    If lenght(i) > 0 Then
      If (lenght(i) > 9999) Then lenght(i) = 9999
      .sca_lun = Format(lenght(i)) + "0"
      .com.cod_cli = "none"
      .com.cod_com_ord = "none"
      .cut_to_mark = "0"
      .nom_cli = "none"
      .Ofs_Cut = "0000"
      .Ord_liv = Format(i)
      .sca_lar = "6000"
      .Sca_per_tag = "1"
      .sca_tot_num = "100000"
      .met = Format((.sca_tot_num / .Sca_per_tag) * (val(lenght(i)) / 1000))
      lastIdx = lastIdx + 1
      If (.met > mt_tot_max) Then
        mt_tot_max = .met
      End If
    End If
  End With
Next i

With ordine.rec
  .met_tot_ord = Format(mt_tot_max)
  .spde_id = "0000"
  .spde_ModID = "00"
  .alt_bob = "18000"
  .cod_ord_abb = "01234567890"
  .tip_car.Alt_ond = "0030"
  .tip_car.Cod_cod_ond = "0123"
  .tip_car.cod_com_car = "012345678901234567890123456789012345"
  .tip_car.Cod_ond = "A"
  .tip_car.Pes_mt2 = "0100"
  .tip_car.Vel_aoc = "0350"
  .tip_car.vel_pro = "0350"
End With
rie_ord_da_PLC = ordine

End Function


Sub tim_tag_plc()
'Iciliouff 03-11-2003
'Gestione automatica Plc, Schede di taglio
'Gestione richieste operatore verso Plc, Schede di taglio
'Revisione generale del codice
Const AGG_STA_SCH_TAG As Integer = 1500
Dim i As Integer
Dim met_max As Long
Dim tot_scatole As Long
Dim tot_tagli As Long
Dim resa As Long
Dim lun_scatola As Long
Dim met_man As Double
'Dim P As Long
Dim ord As Tip_Ordine
Static ini_tim_plc As Boolean
Static old_mod_id_cor As String
Static old_mod_id_suc As String
Static ini_per_mod_id As Integer
Dim liv_ord As Integer
'Iciliouff 28-11-2003
Static last_rx_sta_sch_tag(1 To 3) As Long
' ABorelli_FI09/09/2005_1
Dim met_lin As Long
Dim lar_mm As Long
Dim rt As Integer

On Local Error GoTo ges_err

  chk_BlocNum
  
  ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
  ' LETTURA STATO MACCHINA '
  ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
  With sta_bar
    'Arturo uff 02-03-04 (3)
    If sta_tag.com_OK = False Then
      'Inizializzo i colori del LED di rx,tx
      .led_plc_rx.FillColor = vbRed
      .led_plc_tx.FillColor = vbRed
      'CONTROLLO SE LA COMUNICAZIONE CON IL PLC E' OK
              
      'ABorelli_FI17/11/2006_1
      If val(par_mac.com_plc_lan.abi_lan) = 0 Then
         rt = com_plc_ok
      Else
         rt = IIf(sta_bar.Fins.error, RET_BAD, RET_OK)
      End If
      'If com_plc_ok = RET_OK Then        'ABorelli_FI17/11/2006_1
      If rt = RET_OK Then
        messag_win ""
        sta_tag.com_OK = True
        .lin_no_com_plc.Visible = False
        If .frm_com.BackColor <> COL_DARK_Green Then .frm_com.BackColor = COL_DARK_Green
        invio_ini_par_mac
        ini_plc
      Else
        .lin_no_com_plc.Visible = True  'ABorelli_FI17/11/2006_1
        sta_tag.chk_all_sftw
      End If
    End If
    
    If sta_tag.com_OK Then
      ini_tim_plc = False
      'Inizializzo i colori del LED di rx,tx
      .led_plc_rx.FillColor = IIf(.led_plc_rx.FillColor = vbGreen, COL_DARK_Green, vbGreen)
      .led_plc_tx.FillColor = vbRed
      'aggiorno lo stato della macchina
      sta_tag.aggiorna
      'LETTURA STATO SCHEDE DI TAGLIO
      For i = 1 To val(par_mac.car_tec.num_liv_tag)
        If Not com_ini_sch_tag(i) Then
          'Invio iniziale parametri schede di taglio
          If sta_tag.SCH_TAG_LIV_PRONTA(i, 1) Then
            CASO_SND_SCH_TAG = INVIO_INIZIALE_PAR
            snd_sch_tag i, CONFIGURA
          End If
        Else
          'Controllo se è arrivato il momento di leggere lo stato delle schede di taglio
          'RFAZZI_FI 04-01-08
          'agrassi_FI15-07-08
          'If (par_mac.opt.cut_pat = STR_TRUE) Or (par_mac.opt.cut_pat_auto = STR_TRUE) Then
          
          'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
          'If (par_mac.opt.cut_pat(i) = STR_TRUE) Or (par_mac.opt.cut_pat_auto = STR_TRUE) Then
          If (par_mac.opt.cut_pat(i) = STR_TRUE) Or (par_mac.opt.cut_pat_auto(i) = STR_TRUE) Then
            'AGrassi_FI 21-05-2007
            If Abs(GetTickCount() - last_rx_sta_sch_tag(i)) >= AGG_STA_SCH_TAG Then sta_sch_tag.com_OK(i) = (snd_sch_tag(i, tip_cmd.LETTURA_STATO_ORDINE) = RET_OK): last_rx_sta_sch_tag(i) = GetTickCount()
          Else
            sta_sch_tag.com_OK(i) = False
          End If
        End If
      Next i
'Arturo uff 02-03-04 (3) spostato sopra (si metteva sta_tag.com_OK = true senza aver riempito lo stato tramite la sta_tag.aggiorna
'    Else
'      'Inizializzo i colori del LED di rx,tx
'      .led_plc_rx.FillColor = vbRed
'      .led_plc_tx.FillColor = vbRed
'      'CONTROLLO SE LA COMUNICAZIONE CON IL PLC E' OK
'      If com_plc_ok = RET_OK Then
'        messag_win ""
'        sta_tag.com_OK = True
'        .lin_no_com_plc.Visible = False
'        If .frm_com.BackColor <> COL_DARK_Green Then .frm_com.BackColor = COL_DARK_Green
'        invio_ini_par_mac
'        ini_plc
'      End If
    End If
    
    'calcolo dei metri mancanti al fine ordine
    met_max = -2147483648#  'minimo numero in formato long
    lar_mm = 0  'ABorelli_FI09/09/2005_1
    If ord_mac.Count > 0 Then
      CopyType ord, ord_mac.item(1).ordine, Len(ord_mac.item(1).ordine)
      met_lin = 0       'ABorelli_FI09/09/2005_1
      For i = 1 To 3
        If Trim$(ord.ord(i).Ord_liv) <> "" Then
          liv_ord = val(ord.ord(i).Ord_liv)
          If liv_ord > 0 And liv_ord <= 3 Then
            tot_scatole = CLng(val(ord.ord(i).sca_tot_num))
            resa = CLng(val(ord.ord(i).Sca_per_tag))
            lun_scatola = CLng(val(ord.ord(i).sca_lun) \ TODECMM)
            lar_mm = lar_mm + val(ord.ord(i).sca_lar) / TODECMM * resa ' ABorelli_FI09/09/2005_1
            tot_tagli = sta_tag.num_tag_eff(liv_ord)
            If resa <> 0 Then met_man = (((tot_scatole / resa) - tot_tagli) / 1000) * lun_scatola
            If met_man <> Int(met_man) Then met_man = Int(met_man) + 1
            If met_man > met_max Then met_max = met_man
            met_lin = tot_tagli * lun_scatola \ 1000     ' ABorelli_FI09/09/2005_1
          End If
        End If
      Next i
      If pro_set.abi_reg_rep_pro = STR_TRUE Then        'ABorelli_FI09/09/2005_1
        sta_tag.lunghezza met_lin, lar_mm
      End If
      'calcolo tempo mancante al co
      .txt_tem_co.Text = cal_tem_co(met_max, IIf(abi_mis_ang, val(con_fet_to_met(.txt_vel)), val(.txt_vel)))
      If met_max = -2147483648# Then
        .txt_met_man.Text = "0"
      Else
        If abi_mis_ang Then
          .txt_met_man.Text = con_met_to_fet(CStr(met_max))
        Else
          .txt_met_man.Text = met_max
        End If
      End If
      If met_max >= 0 Then
        .txt_met_man.ForeColor = vbBlack
        .frm_met_man.BackColor = COL_DARK_Green
      Else
        .txt_met_man.ForeColor = COL_ALL_Red
        .frm_met_man.BackColor = COL_ALL_Red
      End If
    End If
    
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''' OPERAZIONI AUTOMATICHE '''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'GESTIONE PLC
    'inizializzazione dei mod_id per il corrente e successivo
    If ini_per_mod_id = 0 Then
      If ord_mac.Count >= 1 Then old_mod_id_cor = ord_mac.item(1).mod_id
      If ord_mac.Count > 1 Then old_mod_id_suc = ord_mac.item(2).mod_id
      ini_per_mod_id = 1
    End If
 
    'Iciliouff 03-11-2003
    If sta_tag.com_OK Then
    
      'PMASSAROFI_05/07/11 - Aggiunta ricezione ordini da MHI via BCD
      'If par_mac.com_spde.ver_spde = MHI10 And sta_tag.MAC_DAT_ORD_SUC_FROM_PLC(1) Then
      If par_mac.com_spde.ver_spde = MHI10 Then
        Dim NextCutLen(1 To 3) As Integer
        Dim idx As Integer
                
        'idx=1 => DM 1° Livello (INF)          idx=2 => DM 2° Livello (MED/SUP)          idx=3 =>  DM 3° Livello (SUP)
        For idx = 1 To 3
          If idx > val(par_mac.car_tec.num_liv_tag) Then
            NextCutLen(idx) = 0
          Else
            NextCutLen(idx) = IIf(sta_tag.next_cut_len_from_external(idx) >= par_mac.car_tec.min_lun_tag, sta_tag.next_cut_len_from_external(idx), 0)
          End If
        Next idx
                
        Dim OKSend As Boolean
        OKSend = False
        
        'se non siamo in cambio e siamo in AUTO
        If sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(0) And sta_tag.MAC_IN_MANUALE(0) Then
          For idx = 1 To val(par_mac.car_tec.num_liv_tag)
            If NextCutLen(idx) <> MisInvPrec(idx) Or _
               NextCutLen(idx) <> sta_tag.lun_tag_suc(idx) Or _
               ord_mac.Count < 2 Then 'forzo il controllo anche se la lista non ha ordini: al limite mostreremo il messaggio di errore
               
               OKSend = True
               Exit For
            End If
          Next idx
        End If

        If OKSend Then
          sta_tag.spde_OK = PrelevaDatiOrdineDaPLC(NextCutLen)
        Else
          sta_tag.spde_OK = True
        End If
      End If
      
      If Not sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1) Then
        'Allineo gli ordini fuori dalla fase di cambio ordine
        If sta_tag.MAC_CAMBIO_ORDINE_COMPLETATO(0) Then
          'Allineo il corrente quando il cambio ordine è completato (per completare il cambio deve essere inviato l'ordine successivo)
          If ord_mac.Count >= 1 Then
            'ORDINE CORRENTE PRESENTE
            If (sta_tag.cod_ord_cor <> ord_mac.item(1).stag_id) Or inv_ord_plc(CORRENTE) Then           'disallineamento sull'ordine corrente o ordine modificato
              log_str "Invio ordine corrente. ID Ordine SPDE: " & Trim$(ord_mac.item(1).spde_id) & " ID Ordine STAG: " & Trim$(ord_mac.item(1).stag_id) & " ID Ordine PLC: " & Trim$(sta_tag.cod_ord_cor) & " Forzatura = " & IIf(inv_ord_plc(CORRENTE), "ON", "OFF"), MED_PRIORITY, log_lis
              snd_str_PLC INVIO_ORD, CORRENTE       'invio dati ordine corrente
              snd_str_PLC AZZERAMENTO_CONTATORI, CORRENTE       'Arturo uff 02-03-04 (2) azzeramento contatori per invio corrente
              set_dat_ini_pro (1)                   'inizializzo la data e l'ora di inizio produzione per l'ordine corrente
            Else
              If ord_mac.item(1).mod_id <> old_mod_id_cor Then    'ordine corrente modificato => invio la modifica
                log_str "Invio modifica ordine corrente: ID Ordine SPDE: " & Trim$(ord_mac.item(1).spde_id) & " ID Ordine STAG: " & Trim$(ord_mac.item(1).stag_id) & "/" & Trim$(ord_mac.item(1).mod_id), MED_PRIORITY, log_lis
                snd_str_PLC MODIF_ORD, CORRENTE     'invio dati modificati dell' ordine corrente  BP
              End If
            End If
          Else
            'ORDINE CORRENTE ASSENTE
            If val(sta_tag.cod_ord_cor) <> 0 Then
              log_str "Invio cancellazione ordine corrente. ID Ordine PLC: " & Trim$(sta_tag.cod_ord_cor), MED_PRIORITY, log_lis
              snd_str_PLC CANC_ORD, CORRENTE        'invio comando di cancellazione dell'ordine corrente
            End If
          End If
        End If
        
        'PMASSARO_FI30/08/11 - Personalizzazione Zcarton per inviare l'ordine successivo al PLC on ritardo rispetto al fine cambio
        Dim tmpAttesa As Integer
        If UCase(pro_set.per_cli) = "3893_ZCARTON" Then
          tmpAttesa = 2000
        Else
          tmpAttesa = 0
        End If
        
        If ord_mac.Count > 1 Then
          'ORDINE SUCCESSIVO PRESENTE
          If sta_tag.cod_ord_suc <> ord_mac.item(2).stag_id Or inv_ord_plc(SUCCESSIVO) Then
            If (tmpAttesa > 0) Then Sleep (tmpAttesa) 'PMASSARO_FI30/08/11 - Personalizzazione Zcarton per inviare l'ordine successivo al PLC on ritardo rispetto al fine cambio
            'disallineamento sull'ordine successivo o ordine modificato
            log_str "Invio ordine successivo. ID Ordine SPDE: " & Trim$(ord_mac.item(2).spde_id) & " ID Ordine STAG: " & Trim$(ord_mac.item(2).stag_id) & " ID Ordine PLC: " & Trim$(sta_tag.cod_ord_suc) & " Forzatura = " & IIf(inv_ord_plc(SUCCESSIVO), "ON", "OFF"), MED_PRIORITY, log_lis
            snd_str_PLC INVIO_ORD, SUCCESSIVO       'invio dati ordine successivo
          Else
            If ord_mac.item(2).mod_id <> old_mod_id_suc Then
              If (tmpAttesa > 0) Then Sleep (tmpAttesa) 'PMASSARO_FI30/08/11 - Personalizzazione Zcarton per inviare l'ordine successivo al PLC on ritardo rispetto al fine cambio
              'ordine successivo modificato => invio la modifica
              log_str "Invio modifica ordine successivo. ID Ordine SPDE: " & Trim$(ord_mac.item(2).spde_id) & " ID Ordine STAG: " & Trim$(ord_mac.item(2).stag_id) & "/" & Trim$(ord_mac.item(2).mod_id), MED_PRIORITY, log_lis
              snd_str_PLC MODIF_ORD, SUCCESSIVO     'invio dati modificati dell' ordine successivo
            End If
          End If
        Else
          'ORDINE SUCCESSIVO ASSENTE
          If val(sta_tag.cod_ord_suc) <> 0 Then
            If (tmpAttesa > 0) Then Sleep (tmpAttesa) 'PMASSARO_FI30/08/11 - Personalizzazione Zcarton per inviare l'ordine successivo al PLC on ritardo rispetto al fine cambio
            log_str "Invio cancellazione ordine successivo. ID Ordine PLC: " & Trim$(sta_tag.cod_ord_suc), MED_PRIORITY, log_lis
            snd_str_PLC CANC_ORD, SUCCESSIVO        'invio comando di cancellazione dell'ordine successivo
          End If
        End If
      End If
    End If
    If ord_mac.Count >= 1 Then old_mod_id_cor = ord_mac.item(1).mod_id
    If ord_mac.Count > 1 Then old_mod_id_suc = ord_mac.item(2).mod_id
    
    'GESTIONE SCHEDE DI TAGLIO
    Static last_chk_ord_sch As Long
    Dim ind_ord As Integer, num_ord As Integer
    Dim ret As Boolean, dis As Boolean
    Dim liv As Integer
    'AGrassi_FI 21-05-2007
    'RFAZZI_FI 04-01-08
    'AGrassi_FI 21-05-2007
    'agrassi_FI15-07-08
    'If (par_mac.opt.cut_pat = STR_TRUE Or par_mac.opt.cut_pat_auto = STR_TRUE) And sta_tag.com_OK
    
    'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
'    If (par_mac.opt.cut_pat(1) = STR_TRUE Or par_mac.opt.cut_pat(2) = STR_TRUE Or par_mac.opt.cut_pat(3) = STR_TRUE Or par_mac.opt.cut_pat_auto = STR_TRUE) And sta_tag.com_OK _
'       And Not sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1) _
'       And sta_tag.MAC_CAMBIO_ORDINE_COMPLETATO(0) _
'       And Not (sta_sch_tag.Set_ftc_run) _
'       And Abs(GetTickCount() - last_chk_ord_sch) >= 2 * AGG_STA_SCH_TAG Then
    If (par_mac.opt.cut_pat(1) = STR_TRUE Or par_mac.opt.cut_pat(2) = STR_TRUE Or par_mac.opt.cut_pat(3) = STR_TRUE Or par_mac.opt.cut_pat_auto(1) = STR_TRUE Or par_mac.opt.cut_pat_auto(2) = STR_TRUE Or par_mac.opt.cut_pat_auto(3) = STR_TRUE) And sta_tag.com_OK _
       And Not sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1) _
       And sta_tag.MAC_CAMBIO_ORDINE_COMPLETATO(0) _
       And Not (sta_sch_tag.Set_ftc_run) _
       And Abs(GetTickCount() - last_chk_ord_sch) >= 2 * AGG_STA_SCH_TAG Then
      'Allineo le schede di taglio solo se è abilitato l'optional cut to pattern e sono fuori dalla fase di set fotocellula
      For num_ord = CORRENTE To SUCCESSIVO
        ret = True: dis = False
        For liv = 1 To val(par_mac.car_tec.num_liv_tag)
          If sta_sch_tag.com_OK(liv) Then
            'La comunicazione con la scheda di taglio livello liv è attiva
            If ord_mac.Count >= num_ord Then
              'Cerco l'indice ordine con livello = liv
              For ind_ord = 1 To 3
                If ord_mac.item(num_ord).Ord_liv(ind_ord) = liv Then Exit For
              Next ind_ord
              'Allineamento ordine num_ord - esimo
              If ind_ord < 4 Then
                'Dati ordine per livello presenti
                If (ord_mac.item(num_ord).stag_id <> sta_sch_tag.stato(liv).ID_ord(num_ord)) Or inv_ord_sch_tag(num_ord) Then
                  'Invio i dati ordine alla scheda di taglio
                  If Not inv_ord_sch_tag(num_ord) Then
                    log_str "tim_tag_plc: Invio ordine " & IIf(num_ord = CORRENTE, "corrente", "successivo") & " a scheda di taglio livello " & Format$(liv) & " per disallineamento StagId = " & ord_mac.item(num_ord).stag_id & " IdSchTag = " & IIf(num_ord = CORRENTE, sta_sch_tag.stato(liv).ID_ord(num_ord), sta_sch_tag.stato(liv).ID_ord(num_ord)), MED_PRIORITY, log_sch_tag
                  Else
                    log_str "tim_tag_plc: Invio ordine " & IIf(num_ord = CORRENTE, "corrente", "successivo") & " a scheda di taglio livello " & Format$(liv) & " per forzatura StagId = " & ord_mac.item(num_ord).stag_id & " IdSchTag = " & IIf(num_ord = CORRENTE, sta_sch_tag.stato(liv).ID_ord(num_ord), sta_sch_tag.stato(liv).ID_ord(num_ord)), MED_PRIORITY, log_sch_tag
                  End If
                  ret = (snd_sch_tag(liv, INVIO_DATI_ORDINE, num_ord) = RET_OK) And ret
                  dis = True    'Almeno un livello è stato trovato disallineato
                End If
              Else
                'Dati ordine per livello non presenti, invio ordine dummy
                 If (ord_mac.item(num_ord).stag_id <> sta_sch_tag.stato(liv).ID_ord(num_ord)) Or inv_ord_sch_tag(num_ord) Then
                  If Not inv_ord_sch_tag(num_ord) Then
                    log_str "tim_tag_plc: Invio ordine dummy " & IIf(num_ord = CORRENTE, "corrente", "successivo") & " a scheda di taglio livello " & Format$(liv) & " per disallineamento StagId = " & ord_mac.item(num_ord).stag_id & " IdSchTag = " & IIf(num_ord = CORRENTE, sta_sch_tag.stato(liv).ID_ord(num_ord), sta_sch_tag.stato(liv).ID_ord(num_ord)), MED_PRIORITY, log_sch_tag
                  Else
                    log_str "tim_tag_plc: Invio ordine dummy " & IIf(num_ord = CORRENTE, "corrente", "successivo") & " a scheda di taglio livello " & Format$(liv) & " per forzatura StagId = " & ord_mac.item(num_ord).stag_id & " IdSchTag = " & IIf(num_ord = CORRENTE, sta_sch_tag.stato(liv).ID_ord(num_ord), sta_sch_tag.stato(liv).ID_ord(num_ord)), MED_PRIORITY, log_sch_tag
                  End If
                  ret = (snd_sch_tag(liv, INVIO_DATI_ORDINE, num_ord) = RET_OK) And ret
                  dis = True    'Almeno un livello è stato trovato disallineato
                End If
              End If
            End If
          Else
            'La comunicazione con la scheda di taglio livello liv è interrotta
            log_str "tim_tag_plc: Comunicazione con scheda di taglio livello " & Format$(liv) & " è interrotta", MED_PRIORITY, log_sch_tag
          End If
        Next liv
        'Dati ordine ricevuti da tutte le schede di taglio, chiedo al PLC che attivi in macchina i dati appena inviati
        'Se invio la conferma a PLC, devo uscire dal ciclo di allineamento ordini per permettere il cambiamento dello stato
        If inv_ord_sch_tag(num_ord) And ret And dis Then inv_ord_sch_tag(num_ord) = False
        'RFAZZI_FI 20-04-2008
        'aggiunto controllo per inviare la conferma al PLC solo se tutte le schede sono state aggiornate
        If num_ord = CORRENTE And ret And dis Then
            snd_str_PLC CONFERMA_DATI_SCHEDE_TAGLIO, , , False: Debug.Print "Invio conferma dati a PLC per ordine corrente": Exit For
        End If
      Next num_ord
      last_chk_ord_sch = GetTickCount()
    End If

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''' OPERAZIONI MANUALI ''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Iciliouff 03-11-2003
    'GESTIONE PLC
    Dim ret_plc As cod_ret
    Dim s As String
    Dim vis_plc As Boolean
    Dim dat_plc As Variant
    Dim liv_plc As Integer
    If rmt_ope_plc.ret = RET_WRK Then
      vis_plc = rmt_ope_plc.obj.abi_vis_err
      dat_plc = rmt_ope_plc.obj.dat
      If rmt_ope_plc.obj.ric = tip_ope_PLC.INVIO_ORD Then
        'Invio ordine corrente/successivo
        Dim cor_suc As Integer
        cor_suc = rmt_ope_plc.obj.num_ord
        If cor_suc = CORRENTE Or cor_suc = SUCCESSIVO Then
          If sta_tag.com_OK Then
            snd_str_PLC tip_ope_PLC.INVIO_ORD, cor_suc, , vis_plc
            ret_plc = RET_OK: s = msg("ope_eff")
            'Icilio3545 08-01-2004 Forzo l'invio del corrente a schede di taglio
            'agrassi_FI15-07-08
            'If val(par_mac.opt.cut_pat) = 1 Then inv_ord_sch_tag(cor_suc) = True
            If val(par_mac.opt.cut_pat(1)) = 1 Or val(par_mac.opt.cut_pat(2)) = 1 Or val(par_mac.opt.cut_pat(3)) = 1 Then inv_ord_sch_tag(cor_suc) = True
          Else: ret_plc = RET_BAD: s = msg("com_int_con_mac")
          End If
        Else: ret_plc = RET_BAD: s = msg("par_err")
        End If
        rmt_ope_plc.obj.rispondi ret_plc, s
      ElseIf rmt_ope_plc.obj.ric = tip_ope_PLC.AZZERAMENTO_CONTATORI _
          Or rmt_ope_plc.obj.ric = tip_ope_PLC.PARAM_MAC _
          Or rmt_ope_plc.obj.ric = tip_ope_PLC.CONFERMA_DATI_SCHEDE_TAGLIO Then
        'Azzeramento contatori/Parametri Macchina
        If sta_tag.com_OK Then
          snd_str_PLC rmt_ope_plc.obj.ric, , , vis_plc
          ret_plc = RET_OK: s = msg("ope_eff")
        Else: ret_plc = RET_BAD: s = msg("com_int_con_mac")
        End If
        rmt_ope_plc.obj.rispondi ret_plc, s
      ElseIf rmt_ope_plc.obj.ric = tip_ope_PLC.PARAM_CUR_TAG Then
        'Invio parametri curva di taglio per livello/globale
        If sta_tag.com_OK Then
          Dim ret_tot_plc As Boolean
          If dat_plc Then
            ret_tot_plc = True
            For liv_plc = 1 To par_mac.car_tec.num_liv_tag
              ret_plc = RET_BAD
              If carica_new_par_cur_tag(liv_plc, val(par_mac.car_tec.for_mac), get_gra_car(par_mac.car_tec.gra_car), val(par_mac.car_tec.num_mot_liv_tag), par_abi_ute.num_mot_abi(liv_plc)) Then
                snd_str_PLC PARAM_CUR_TAG, , liv_plc, vis_plc
                ret_plc = RET_OK
              End If
              ret_tot_plc = IIf(ret_plc = RET_OK, True, False) And ret_tot_plc
            Next liv_plc
            s = IIf(ret_tot_plc, msg("ope_eff"), msg("val_non_tro_cur_tag"))
          Else
            If rmt_ope_plc.obj.liv > 0 And rmt_ope_plc.obj.liv < 4 Then
              snd_str_PLC tip_ope_PLC.PARAM_CUR_TAG, , rmt_ope_plc.obj.liv, vis_plc
            Else: snd_str_PLC tip_ope_PLC.PARAM_CUR_TAG, , , vis_plc
            End If
            ret_plc = RET_OK: s = msg("ope_eff")
          End If
        Else: ret_plc = RET_BAD: s = msg("com_int_con_mac")
        End If
        rmt_ope_plc.obj.rispondi ret_plc, s
      ElseIf rmt_ope_plc.obj.ric = tip_ope_PLC.PARAM_CFG_MAC Then
        'Invio configurazione macchina
        If sta_tag.com_OK Then
          snd_str_PLC tip_ope_PLC.PARAM_CFG_MAC, , , vis_plc
          ret_plc = RET_OK: s = msg("ope_eff")
        Else: ret_plc = RET_BAD: s = msg("com_int_con_mac")
        End If
        rmt_ope_plc.obj.rispondi ret_plc, s
      'Icilio3545 04-01-2004 Invio conferma dati schede di taglio
      ElseIf rmt_ope_plc.obj.ric = tip_ope_PLC.CONFERMA_DATI_SCHEDE_TAGLIO Then
        If sta_tag.com_OK Then
          snd_str_PLC CONFERMA_DATI_SCHEDE_TAGLIO, , , False
          ret_plc = RET_OK: s = msg("ope_eff")
        Else: ret_plc = RET_BAD: s = msg("com_int_con_mac")
        End If
      Else
        'Comando sconosciuto
        rmt_ope_plc.obj.rispondi RET_BAD, msg("par_err")
      End If
      'Timeout comandi plc
      If ((GetTickCount() - rmt_ope_plc.obj.tim_out) > 10000) And (rmt_ope_plc.obj.ret = RET_WRK) Then
        log_str "tim_tag_plc: Rmt " & rmt_ope_plc.obj.name & " in time out eseguendo l'operazione " & Format(rmt_ope_plc.obj.ric), MED_PRIORITY, log_rmt
        rmt_ope_plc.obj.rispondi RET_BAD, msg("imp_ese_ope")
      End If
    End If
    
    'GESTIONE SCHEDE DI TAGLIO
    Dim ret_sch_tag As cod_ret
    Dim dat_sch As Variant
    If rmt_ope_sch.ret = RET_WRK Then
      liv = rmt_ope_sch.liv
      dat_sch = rmt_ope_sch.dat
      If rmt_ope_sch.ric = tip_cmd.CONFIGURA Then
        'Richiesta di configurazione scheda di taglio
        If dat_sch Then
          Dim pun As Integer
          Dim ret_tot As Boolean
          ret_tot = True
          For pun = 1 To par_mac.car_tec.num_liv_tag
            ret_tot = (snd_sch_tag(pun, CONFIGURA) = RET_OK) And ret_tot
          Next pun
          ret_sch_tag = IIf(ret_tot, RET_OK, RET_BAD)
          s = IIf(ret_tot, msg("ope_eff"), msg("ope_eff_par"))
        Else
          If liv > 0 And liv < 4 Then
            ret_sch_tag = snd_sch_tag(liv, CONFIGURA)
            s = IIf(ret_sch_tag = RET_OK, msg("ope_eff"), msg("com_int_con_mac"))
          Else: ret_plc = RET_BAD: s = msg("par_err")
          End If
        End If
        rmt_ope_sch.obj.rispondi ret_sch_tag, s
      ElseIf rmt_ope_sch.ric = tip_cmd.INFORMAZIONI Or rmt_ope_sch.ric = tip_cmd.LETTURA_STATO Or rmt_ope_sch.ric = tip_cmd.SELEZIONE_CONTRASTO Then
        'Richiesta informazioni da scheda di taglio
        'Richiesta lettura stato
        'Selezione contrasto
        If liv > 0 And liv < 4 Then
          ret_sch_tag = snd_sch_tag(liv, rmt_ope_sch.ric, , dat_sch)
          s = IIf(ret_sch_tag = RET_OK, msg("ope_eff"), msg("com_int_con_mac"))
        Else: ret_plc = RET_BAD: s = msg("par_err")
        End If
        rmt_ope_sch.obj.rispondi ret_sch_tag, s
      Else
        'Comando sconosciuto
        rmt_ope_sch.obj.rispondi RET_BAD, msg("imp_ese_ope")
      End If
      'Timeout comandi scheda di taglio
      If ((GetTickCount() - rmt_ope_sch.obj.tim_out) > 10000) And (rmt_ope_sch.obj.ret = RET_WRK) Then
        log_str "tim_tag_plc: Rmt " & rmt_ope_sch.obj.name & " in time out eseguendo l'operazione " & Format(rmt_ope_sch.obj.ric), MED_PRIORITY, log_rmt
        rmt_ope_sch.obj.rispondi RET_BAD, msg("imp_ese_ope")
      End If
    End If
    
    'GESTIONE MACRO
    Dim ret_macro As cod_ret
    Dim liv_macro As Integer
    Dim dat_macro As Integer
    Dim vis_macro As Boolean
    If rmt_ope_macro.ret = RET_WRK Then
      liv_macro = rmt_ope_macro.liv
      dat_macro = rmt_ope_macro.dat
      vis_macro = rmt_ope_macro.obj.abi_vis_err
      If rmt_ope_macro.ric = tip_ope_macro.PLC_PARAM_CFG_MAC_PARAM_CUR_TAG Then
        rmt_ope_macro.obj.err_cod = cod_err_macro.NIET
        If sta_tag.com_OK Then
          snd_str_PLC tip_ope_PLC.PARAM_CFG_MAC, , , vis_macro
          If carica_new_par_cur_tag(liv_macro, val(par_mac.car_tec.for_mac), get_gra_car(par_mac.car_tec.gra_car), val(par_mac.car_tec.num_mot_liv_tag), par_abi_ute.num_mot_abi(liv_macro)) Then
            snd_str_PLC PARAM_CUR_TAG, , liv_macro, vis_macro
            ret_macro = RET_OK: s = msg("ope_eff")
          Else
            ret_macro = RET_BAD: s = msg("val_non_tro_cur_tag")
            rmt_ope_macro.obj.err_cod = cod_err_macro.ROLL_BACK_PLC
          End If
        Else
          ret_macro = RET_BAD: s = msg("com_int_con_mac")
        End If
        rmt_ope_macro.obj.rispondi ret_macro, s
      ElseIf rmt_ope_macro.ric = tip_ope_macro.PLC_PARAM_MAC_PARAM_CFG_MAC Then
        rmt_ope_macro.err_cod = cod_err_macro.NIET
        If sta_tag.com_OK Then
          snd_str_PLC tip_ope_PLC.PARAM_MAC, , , vis_macro
          snd_str_PLC tip_ope_PLC.PARAM_CFG_MAC, , , vis_macro
          ret_macro = RET_OK: s = msg("ope_eff")
        Else
          ret_macro = RET_BAD: s = msg("ope_eff_par")
        End If
        rmt_ope_macro.obj.rispondi ret_macro, s
      'Icilio3545 04-01-2004 Gestione macro di acquisizione contrasto x scheda di taglio - PLC
      ElseIf rmt_ope_macro.ric = tip_ope_macro.ACQ_CON_SCH_PLC Then
        rmt_ope_macro.obj.err_cod = cod_err_macro.NIET
        If liv_macro > 0 And liv_macro < 4 Then
          ret_macro = snd_sch_tag(liv_macro, tip_cmd.SELEZIONE_CONTRASTO, , dat_macro)
          If ret_macro = RET_OK Then
            'Invio a scheda di taglio riuscito, procediamo con PLC
            If sta_tag.com_OK Then
              snd_str_PLC CONFERMA_DATI_SCHEDE_TAGLIO, , , False
              ret_macro = RET_OK: s = msg("ope_eff")
            Else: ret_macro = RET_BAD: s = msg("com_int_con_mac")
            End If
          Else: ret_macro = RET_BAD: s = msg("com_int_con_mac")
          End If
        Else: ret_plc = RET_BAD: s = msg("par_err")
        End If
        rmt_ope_macro.obj.rispondi ret_macro, s
      Else
        'Comando sconosciuto
        rmt_ope_macro.obj.rispondi RET_BAD, msg("imp_ese_ope")
      End If
    End If
    
  End With
  Exit Sub
  
ges_err:
  If err.Number = 57 Then
    If Not ini_tim_plc Then
        ini_tim_plc = True
        If sta_tag.com_OK Then sta_bar.mes_PLC par_mac.indirizzo_plc.net_id, par_mac.indirizzo_plc.node_id, par_mac.indirizzo_plc.unit_id
    End If
    If Not sta_tag.com_OK Then sta_tag.chk_all_sftw  'Se non comunico con la macchina non faccio nemmeno l'aggiorna => chiamo la funzione di controllo degli allarmi software
    Exit Sub
  End If
  If err.Number = 6 Then
      messaggio "OVERFLOW nel calcolo dei metri mancanti", True, True
  Else
      log_str "on error intervenuta in Tim_com_PLC" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  End If
  
End Sub


Function ini_plc()
'Iciliouff 03-11-2003 Spostata da Principale.bas
'Modificata per ritornare un cod_ret
On Local Error GoTo ges_err
  
'ABorelli_FI17/11/2006_1
  If val(par_mac.com_plc_lan.abi_lan) <> 0 Then
    ini_plc_lan
    Exit Function
  Else
  plc_ind.change_ind_plc par_mac.indirizzo_plc.net_id, par_mac.indirizzo_plc.node_id, par_mac.indirizzo_plc.unit_id
  'metto il PLC in modalità Monitor
  If sta_tag.com_OK Then
    If sta_bar.SYSMAC_PLC.RunMode <> 2 Then
      log_str "Inizializzazione PLC: PLC messo in modalità Monitor", MED_PRIORITY, log_plc
      sta_bar.SYSMAC_PLC.RunMode = 2
    End If
  End If
  Exit Function
  End If
  
ges_err:
 Select Case err.Number
  Case 57 'modifica inserita in data 04-01-01
    'per individuare il caso di indirizzo errato per la scheda PLC
    sta_tag.com_OK = False
    sta_bar.mes_PLC par_mac.indirizzo_plc.net_id, par_mac.indirizzo_plc.node_id, par_mac.indirizzo_plc.unit_id
  Case 91 ' caso in cui la scheda plc non è presente => in questo caso da errore di "variabile del blocco with non impostata" quando si chiama il metodo change_ind_plc della classe ind_plc
    messaggio msg("sch_plc_non_pre"), True, False
    Exit Function
  Case Else
    log_str "Sub ini_plc: " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
 End Select
 
End Function

Sub ini_plc_lan()
      'ABorelli_FI17/11/2006_1
On Local Error GoTo ges_err1
  sta_bar.Fins.SetMonitor
  If sta_bar.Fins.error = 0 And sta_bar.Fins.RunMode = 2 Then
    log_str "Inizializzazione PLC: PLC messo in modalità Monitor", MED_PRIORITY, log_plc
  Else
    log_str "Fallita inizializzazione PLC per errore di rete", MED_PRIORITY, log_plc
  End If
  Exit Sub
ges_err1:
  Exit Sub
  log_str "Sub ini_plc_lan: " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
End Sub
Sub invio_ini_par_mac()
'Iciliouff 03-11-2003 Spostata da Principale.bas
Dim i As Integer

  If sta_tag.com_OK Then
      For i = 1 To val(par_mac.car_tec.num_liv_tag)
        snd_str_PLC PARAM_CUR_TAG, , i
      Next i
      snd_str_PLC PARAM_CFG_MAC
      snd_str_PLC PARAM_MAC
  Else
      log_str "Sub invio_ini_par_mac: invio iniziale dei parametri al PLC FALLITO !", MED_PRIORITY, log_plc
  End If

End Sub
