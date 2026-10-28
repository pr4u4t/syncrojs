Attribute VB_Name = "ins_ord_bas"
Option Explicit


Function val_ins_ord(hwnd As Long, Index As Integer) As Boolean
Dim liv As Integer
Dim err_ric As Integer
Dim for_mac As Integer

'determina se i valori inseriti nell'ultimo controllo utilizzato (Ins_ord.ins_ord.ult_con) sono validi
'restituisce True se non ci sono problemi

val_ins_ord = True
If Ins_ord.ult_con = 0 Then Ins_ord.ult_con = hwnd: Ins_ord.ult_ind = -1 'inizializzazione di ins_ord.ult_con e ins_ord.ult_ind
If Ins_ord.ult_con = hwnd Then Exit Function 'eseguo i calcoli solo se ho cambiato controllo

val_ins_ord = False

'salvo i dati della form nell'ordine
'se sono in inch le converto in mm e mt
'If abi_mis_ang Then
'    get_new_dat_ang var_ord_ute.rec.ord
'Else
  get_new_dat var_ord_ute
'End If

'eseguo i controlli solo quando sto andando avanti e non quando torno indietro
If Ins_ord.flg_rev = True And Ins_ord.ult_con <> Ins_ord.txt_cod_abb.hwnd Then
  Ins_ord.flg_rev = False
  Ins_ord.ult_con = hwnd
  Ins_ord.ult_ind = Index
  Exit Function
End If

'sezione di validazione
Select Case Ins_ord.ult_con
  
  Case Ins_ord.txt_cod_abb.hwnd  'Codice dell'ordine
    If err_lun_Cod_ord_abb(var_ord_ute.rec) Then
      messaggio msg("ins_cod_ord"), is_All:=True
      Ins_ord.txt_cod_abb.SetFocus
      Exit Function
    End If
    If Ins_ord.flg_rev Then 'impedisco di andare indietro dal primo controllo
      Ins_ord.txt_cod_abb.SetFocus
      Ins_ord.flg_rev = False
      Exit Function
    End If
    
  Case Ins_ord.cmb_onda.hwnd 'presenza tipo di onda
    If err_ass_ond(var_ord_ute.rec) Then
      messaggio msg("ins_onda"), is_All:=True
      Ins_ord.cmb_onda.SetFocus
      Exit Function
    End If

  Case Ins_ord.cmb_carta.hwnd 'presenza tipo di carta
    If err_ass_car(var_ord_ute.rec) Then
      messaggio msg("ins_carta"), is_All:=True
      Ins_ord.cmb_carta.SetFocus
      Exit Function
    End If
    
  Case Ins_ord.txt_alt_bob.hwnd 'Altezza bobina dell'ordine
    Select Case par_mac.car_tec.for_mac + 1
      Case mm1800
        for_mac = 1800
      Case mm2500
        for_mac = 2500
      Case mm2800
        for_mac = 2800
    End Select
    If err_alt_bob(var_ord_ute.rec, for_mac) Then
      If abi_mis_ang Then
        messaggio msg("val_min") + " " + con_dmm_to_inc(CStr(val(par_mac.car_tec.min_for_mac) * TODECMM)) + "  " + msg("val_max") + " " + con_dmm_to_inc(CStr(val(for_mac * TODECMM))), is_All:=True
      Else
        messaggio msg("val_min") + " " + Trim$(par_mac.car_tec.min_for_mac) + "  " + msg("val_max") + " " + str$(for_mac), is_All:=True
      End If
      Ins_ord.txt_alt_bob.SetFocus
      Exit Function
    End If
    
  Case Ins_ord.txt_liv(Ins_ord.ult_ind).hwnd 'livello di lavorazione
    If err_ass_liv(var_ord_ute.rec, Ins_ord.ult_ind + 1) Then
      messaggio msg("ins_all_dat"), is_All:=True
      Ins_ord.txt_liv(Ins_ord.ult_ind).SetFocus
      Exit Function
    End If
    If err_abi_liv(var_ord_ute.rec, Ins_ord.ult_ind + 1) Then
      messaggio msg("liv_non_abi"), is_All:=True
      Ins_ord.txt_liv(Ins_ord.ult_ind).SetFocus
      Exit Function
    End If
    
  Case Ins_ord.txt_cod_com(Ins_ord.ult_ind).hwnd  'commessa ordine
    If err_cod_com(Ins_ord.txt_cod_com(Ins_ord.ult_ind).Text) Then
      'errore nel codice commessa
        messaggio msg("car_non_per"), is_All:=True
        Ins_ord.txt_cod_com(Ins_ord.ult_ind).SetFocus
        Exit Function
    End If
    
  Case Ins_ord.txt_sca_lar(Ins_ord.ult_ind).hwnd 'Larghezza scatola del formato
    If err_ass_sca_lar(var_ord_ute.rec, Ins_ord.ult_ind + 1) Then
      messaggio msg("ins_lar"), is_All:=True
      Ins_ord.txt_sca_lar(Ins_ord.ult_ind).SetFocus
      Exit Function
    ElseIf err_max_sca_lar(var_ord_ute.rec) Then
      messaggio msg("mis_ord_ecc"), is_All:=True
      Ins_ord.txt_sca_lar(Ins_ord.ult_ind).SetFocus
      Exit Function
    End If
  
  Case Ins_ord.txt_sca_lun(Ins_ord.ult_ind).hwnd 'Lunghezza formato a quel livello
    If err_ass_lun(var_ord_ute.rec, Ins_ord.ult_ind + 1) Then
      messaggio msg("ins_lun_for"), is_All:=True
      Ins_ord.txt_sca_lun(Ins_ord.ult_ind).SetFocus
      Exit Function
    End If
    If err_sca_lun(var_ord_ute.rec, Ins_ord.ult_ind + 1) Then
      If abi_mis_ang Then
        messaggio msg("val_min") + " " + con_dmm_to_inc(CStr(val(par_mac.car_tec.min_lun_tag) * TODECMM)) + "  " + msg("val_max") + " " + con_dmm_to_inc(CStr(val(par_mac.car_tec.max_lun_tag) * TODECMM)), is_All:=True
      Else
        messaggio msg("val_min") + " " + Trim$(par_mac.car_tec.min_lun_tag) + "  " + msg("val_max") + " " + Trim$(par_mac.car_tec.max_lun_tag), is_All:=True
      End If
      Ins_ord.txt_sca_lun(Ins_ord.ult_ind).SetFocus
      Exit Function
    End If
      
  Case Ins_ord.txt_resa(Ins_ord.ult_ind).hwnd 'numero di scatole per formato
    If err_num_sca(var_ord_ute.rec, Ins_ord.ult_ind + 1) Then
      messaggio msg("val_min") + " 1 ", is_All:=True
      Ins_ord.txt_resa(Ins_ord.ult_ind).SetFocus
      Exit Function
    ElseIf err_som_sca(var_ord_ute.rec, Ins_ord.ult_ind + 1) Then
      messaggio msg("mis_ord_ecc"), is_All:=True
      Ins_ord.txt_resa(Ins_ord.ult_ind).SetFocus
      Exit Function
    End If
    
'  Case txt_sca_tot_num(Ins_ord.ins_ord.ult_ind).hwnd 'numero di fogli del formato
'    err_ric = err_sca_tot_num(var_ord_ute.rec.ord, Ins_ord.ins_ord.ult_ind + 1)
'    If err_ric > 0 Then
'      If err_ric = 1 Then messaggio msg("ins_sca_tot_num"), is_All:=True
'      If err_ric = 2 Then messaggio msg("err_num_fog_ecc") + " max:" + Format$(Int((99999000 / Val(Ins_ord.txt_lun(Ins_ord.ins_ord.ult_ind)))) * Val(Ins_ord.txt_sca_per_tag(Ins_ord.ins_ord.ult_ind))), is_All:=True
'      Ins_ord.txt_sca_tot_num(Ins_ord.ins_ord.ult_ind).SetFocus
'      Exit Function
'    End If
  
  Case Ins_ord.cmb_cut_to_mark(Ins_ord.ult_ind).hwnd
    'Cut_to_mark
    err_ric = err_cut_to_mark(var_ord_ute.rec, Ins_ord.ult_ind + 1, False)
    If err_ric > 0 Then
      If err_ric = 1 Then messaggio msg("ins_cut_to_mark"), is_All:=True
      If err_ric = 2 Then messaggio msg("cut_to_mark_non_pre"), is_All:=True
      If err_ric = 3 Then messaggio msg("cut_to_mark_liv_abb"), is_All:=True    'Iciliouff 20-11-2003
      Ins_ord.cmb_cut_to_mark(Ins_ord.ult_ind).SetFocus
      Exit Function
    End If
    
  Case Ins_ord.cmb_pat_cod(Ins_ord.ult_ind).hwnd
    'Codice pattern - Non è necessaria nessuna validazione
    
  'RFAZZI_FI 07-01-08
  'Case Ins_ord.txt_ofs_cut(Ins_ord.ult_ind).hwnd
  Case Ins_ord.txt_pos_lat_pat(Ins_ord.ult_ind).hwnd
    'Posizione laterale del pattern
    If err_pos_pat(var_ord_ute.rec, Ins_ord.ult_ind + 1, False) Then
      messaggio msg("err_pos_pat"), is_All:=True
      Ins_ord.txt_pos_lat_pat(Ins_ord.ult_ind).SetFocus
      Exit Function
    End If
    
  Case Ins_ord.txt_pat_con(Ins_ord.ult_ind).hwnd
    'Contrasto
    If err_pat_con(var_ord_ute.rec, Ins_ord.ult_ind + 1, False) Then
      messaggio msg("err_pat_con"), is_All:=True
      Ins_ord.txt_pat_con(Ins_ord.ult_ind).SetFocus
      Exit Function
    End If
    
  Case Ins_ord.txt_fin_pri(Ins_ord.ult_ind).hwnd
    'Finestra prima
    If err_fin_pri(var_ord_ute.rec, Ins_ord.ult_ind + 1, False) Then
      messaggio msg("err_fin_pri"), is_All:=True
      Ins_ord.txt_fin_pri(Ins_ord.ult_ind).SetFocus
      Exit Function
    End If
    
  Case Ins_ord.txt_fin_dop(Ins_ord.ult_ind).hwnd
    'Finestra dopo
    If err_fin_dop(var_ord_ute.rec, Ins_ord.ult_ind + 1, False) Then
      messaggio msg("err_fin_dop"), is_All:=True
      Ins_ord.txt_fin_dop(Ins_ord.ult_ind).SetFocus
      Exit Function
    End If
    
End Select

'se arrivo qui, significa che le validazioni hanno avuto successo, quindi aggiorno
'il puntatore all'ultimo controllo attivo
Ins_ord.ult_con = hwnd
Ins_ord.ult_ind = Index
val_ins_ord = True

End Function

Function msg_err_val_ord(ret As Integer) As String
'restituisce il messaggio da visualizzare a seconda del codice passato da val_ord
Dim s As String
Select Case ret
  Case COD_ERR_LUN_COD: s = "ins_cod_ord"                                   'errore per assenza codice ordine
  Case COD_ERR_ASS_OND: s = "ins_onda"                                        'errore assenza onda
  Case COD_ERR_ASS_CAR: s = "ins_carta"                                        'errore assenza carta
  Case COD_ERR_ALT_BOB: s = "err_alt_bob"                                    'errore altezza bobina
  Case COD_ERR_ASS_DAT: s = "ins_all_dat"                                      'errore assenza livello su un formato dell'ordine
  Case COD_ERR_ABI_LIV: s = "liv_non_abi"                                       'errore livello non abilitato per il formato dell'ordine
  Case COD_ERR_COD_COM: s = "err_cod_com"                                  'errore carattere errato codice commessa di un formato dell'ordine
  Case COD_ERR_MAX_SCA_LAR: s = "err_max_sca_lar"                    'errore sulla misura della larghezza scatola di un formato dell'ordine
  Case COD_ERR_ASS_LUN: s = "ins_lun_for"                                     'errore per assenza della lunghezza scatola di un formato dell'ordine
  Case COD_ERR_SCA_LUN: s = "err_sca_lun"                                     'errore sulla misura della lunghezza scatola di un formato dell'ordine
  Case COD_ERR_ASS_SCA_PER_TAG: s = "err_ass_sca_per_tag"      'errore per assenza scatole per taglio di un formato dell'ordine
  Case COD_ERR_SCA_PER_TAG: s = "err_sca_per_tag"                      'errore di misura eccessiva sulle scatole per taglio di un formato dell'ordine
  Case COD_ERR_ASS_CUT_TO_MARK: s = "ins_cut_to_mark"            'errore per mancata impostazione del cut_to_mark su un formato dell'ordine
  Case COD_ERR_CUT_TO_MARK: s = "cut_to_mark_non_pre"             'errore di impostazione cut_to_mark su un formato dell'ordine: si è cercato di impostare un ordine cut_to_mark senza che questo sia presente nella macchina
  Case COD_ERR_CUT_TO_MARK_LIV_ABB: s = "cut_to_mark_liv_abb"             'errore di impostazione cut_to_mark su un formato dell'ordine: dato cut to mark diverso per i due formati abbinati sullo stesso livello
  Case COD_ERR_COD_PAT: s = "err_cod_pat"                                 'errore: dato codice pattern impostato in modo diverso sui due formati abbinati sullo stesso livello
  Case COD_ERR_OFS_CUT_PAT: s = "err_ofs_cut_pat"                         'errore: dato offset taglio registro non corretto
  Case COD_ERR_MIS_PAT: s = "err_mis_pat"                                 'errore: dato L1..L10 errato
  Case COD_ERR_PAT_CON: s = "err_pat_con"                                 'errore: dato contrasto fotocellula non corretto
  Case COD_ERR_FIN_PRI: s = "err_fin_pri"                                 'errore: dato finestra prima del pattern/mark non corretto
  Case COD_ERR_FIN_DOP: s = "err_fin_dop"                                 'errore: dato finestra dopo del pattern/mark non corretto
  Case COD_ERR_LIV_LUN_DIV: s = "err_liv_lun_div"                         'errore per presenza di due o più formati sullo stesso livello ma con lunghezza diversa
  'RFAZZI_FI 07-01-08
  Case COD_ERR_POS_PAT: s = "err_pos_pat"                                   'errore posizione laterale del Pattern
  
  
  Case Else: s = "imp_ese_ope"                                                              'errore generico
End Select
msg_err_val_ord = s
End Function

Sub get_new_dat(ord As Tip_Com_ord)
'aggiorna i dati dell'ordine con i valori presenti nella form di inserimento ordini
Dim i As Integer
Dim som As Double
Dim for_mac As String
Dim tmp As Long

With ord
  Select Case par_mac.car_tec.for_mac + 1
    Case mm1800
      for_mac = "18000"
    Case mm2500
      for_mac = "25000"
    Case mm2800
      for_mac = "28000"
  End Select
  
  If abi_mis_ang Then     ' tutta questa parte di codice serve per evitare il troncamento visto che .rec.alt_bob è una stringa di 5 caratteri e la con_inc_to_dmm può restituire stringhe più lunghe
    tmp = CLng(con_inc_to_dmm(Ins_ord.txt_alt_bob.Text))
    If tmp > val(for_mac) Then
      .rec.alt_bob = String$(Len(.rec.alt_bob), "9")
    ElseIf tmp < val(par_mac.car_tec.min_for_mac) * TODECMM Then
      .rec.alt_bob = String$(Len(.rec.alt_bob), "0")
    Else
      .rec.alt_bob = tmp
    End If
  Else
    .rec.alt_bob = CStr(val(Ins_ord.txt_alt_bob.Text) * TODECMM)
  End If
  
  .rec.cod_ord_abb = Trim$(Ins_ord.txt_cod_abb.Text)
  .rec.tip_car.cod_com_car = Trim$(Ins_ord.cmb_carta.Text)
  If Len(Ins_ord.cmb_onda.Text) = 0 Then
    .rec.tip_car.Cod_ond = Space$(Len(.rec.tip_car.Cod_ond))
  Else
    .rec.tip_car.Cod_ond = Trim$(Ins_ord.cmb_onda.Text)
  End If
  .rec.mod_id = "00"
  For i = 0 To 2
    som = 0
    If val(Ins_ord.txt_resa(i)) > 0 Then
        som = CLng((val(Ins_ord.txt_num_tot_fog(i)) / val(Ins_ord.txt_resa(i))) * IIf(abi_mis_ang, con_inc_to_mm(Ins_ord.txt_sca_lun(i)), val(Ins_ord.txt_sca_lun(i)))) / 1000
    End If
    If som <> Int(som) Then som = Int(som) + 1
    .rec.ord(i + 1).met = CStr(som)
    .rec.ord(i + 1).com.cod_com_ord = Trim$(Ins_ord.txt_cod_com(i).Text)
    .rec.ord(i + 1).com.cod_cli = Trim$(Ins_ord.txt_cod_cli(i).Text)
    .rec.ord(i + 1).nom_cli = Trim$(Ins_ord.txt_nom_cli(i).Text)
    If abi_mis_ang Then     ' tutta questa parte di codice serve per evitare il troncamento visto che .rec.ord(i + 1).sca_lar è una stringa di 5 caratteri e la con_inc_to_dmm può restituire stringhe più lunghe
      tmp = CLng(con_inc_to_dmm(Ins_ord.txt_sca_lar(i).Text))
      If tmp > val(for_mac) Then
        .rec.ord(i + 1).sca_lar = String$(Len(.rec.ord(i + 1).sca_lar), "9")
      Else
        .rec.ord(i + 1).sca_lar = CStr(val(con_inc_to_dmm(Ins_ord.txt_sca_lar(i).Text)))
      End If
    Else
      .rec.ord(i + 1).sca_lar = CStr(val(Ins_ord.txt_sca_lar(i).Text) * TODECMM)
    End If
    If abi_mis_ang Then     ' tutta questa parte di codice serve per evitare il troncamento visto che .rec.ord(i + 1).sca_lun è una stringa di 5 caratteri e la con_inc_to_dmm può restituire stringhe più lunghe
      tmp = CLng(con_inc_to_dmm(Ins_ord.txt_sca_lun(i).Text))
      If tmp > val(par_mac.car_tec.max_lun_tag) * TODECMM Then
        .rec.ord(i + 1).sca_lun = String$(Len(.rec.ord(i + 1).sca_lun), "9")
      ElseIf tmp < val(par_mac.car_tec.min_lun_tag) * TODECMM Then
        .rec.ord(i + 1).sca_lun = String$(Len(.rec.ord(i + 1).sca_lun), "0")
      Else
        .rec.ord(i + 1).sca_lun = CStr(val(con_inc_to_dmm(Ins_ord.txt_sca_lun(i).Text)))
      End If
    Else
      .rec.ord(i + 1).sca_lun = CStr(val(Ins_ord.txt_sca_lun(i).Text) * TODECMM)
    End If
    .rec.ord(i + 1).Sca_per_tag = Trim$(Ins_ord.txt_resa(i).Text)
    .rec.ord(i + 1).sca_tot_num = Trim$(Ins_ord.txt_num_tot_fog(i).Text)
    If Ins_ord.cmb_cut_to_mark(i).ListIndex = -1 Then
      .rec.ord(i + 1).cut_to_mark = Space$(Len(.rec.ord(i + 1).cut_to_mark))
    Else
      .rec.ord(i + 1).cut_to_mark = Trim$(Ins_ord.cmb_cut_to_mark(i).ListIndex)
    End If
    If Ins_ord.txt_liv(i).ListIndex = -1 Then
      .rec.ord(i + 1).Ord_liv = Space$(Len(.rec.ord(i + 1).Ord_liv))
    Else
      .rec.ord(i + 1).Ord_liv = Format$(Ins_ord.txt_liv(i).ItemData(Ins_ord.txt_liv(i).ListIndex))
    End If
    'Iciliouff 20-11-2003
    'andrea 20-04-06
    If val(.rec.ord(i + 1).cut_to_mark) = cut_pat Then
      'Codice pattern
      .rec.ord(i + 1).pat_cod = Ins_ord.cmb_pat_cod(i).Text
      Dim rs As Recordset
      Set rs = db.OpenRecordset("select * from PATTERN where Ucase(CODICE)='" + UCase$(.rec.ord(i + 1).pat_cod) + "'", dbOpenSnapshot)
      If rs.RecordCount > 0 Then
        'Dati misure tacche L1..L10 + FST_FTC
        .rec.ord(i + 1).fst_FTC = rs("PRI_LET_FC")
        .rec.ord(i + 1).L1 = rs("L1")
        .rec.ord(i + 1).L2 = rs("L2")
        .rec.ord(i + 1).L3 = rs("L3")
        .rec.ord(i + 1).L4 = rs("L4")
        .rec.ord(i + 1).L5 = rs("L5")
        .rec.ord(i + 1).L6 = rs("L6")
        .rec.ord(i + 1).L7 = rs("L7")
        .rec.ord(i + 1).L8 = rs("L8")
        .rec.ord(i + 1).L9 = rs("L9")
        .rec.ord(i + 1).L10 = rs("L10")
        'Robertouff 26-11-2007
        .rec.ord(i + 1).Ofs_Cut = rs("OFS_CUT")
        
      End If
      rs.Close
      'RFAZZI_FI 07-01-08
'      'Offset di taglio pattern
'      .rec.ord(i + 1).Ofs_Cut = Trim$(Ins_ord.txt_ofs_cut(i).Text)
      'Posizione laterale del pattern
      .rec.ord(i + 1).pos_pattern = Trim$(Ins_ord.txt_pos_lat_pat(i).Text)
      'Contrasto
      .rec.ord(i + 1).pat_con = Trim$(Ins_ord.txt_pat_con(i).Text)
      'Finestra prima
      .rec.ord(i + 1).fin_pri = Trim$(Ins_ord.txt_fin_pri(i).Text)
      'Finestra dopo
      .rec.ord(i + 1).fin_dop = Trim$(Ins_ord.txt_fin_dop(i).Text)
    End If
  Next i
  .rec.met_tot_ord = CStr(Max_num(CLng(.rec.ord(1).met), CLng(.rec.ord(2).met)))
  .rec.met_tot_ord = CStr(Max_num(val(.rec.met_tot_ord), CLng(.rec.ord(3).met)))
End With
End Sub



