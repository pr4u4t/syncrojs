Attribute VB_Name = "validazione_ordini"
Option Explicit

Function con_uff_ass_cod(ord As Tip_Ordine, f As Integer, Optional ins_man As Boolean = False)
Dim ss1 As Recordset

  con_uff_ass_cod = True
  If val(ord.ord(f).cut_to_mark) = cut_pat Then
    If Trim(Rie_nul(ord.ord(f).pat_cod)) = "" Then
      con_uff_ass_cod = False
    Else
      Set ss1 = db.OpenRecordset("select * from PATTERN where UCase(CODICE)='" + UCase(Trim$(ord.ord(f).pat_cod)) + "'", dbOpenDynaset)
        If ss1.RecordCount > 0 Then
          ss1.Edit
          If Not ins_man Then ss1!CONTRASTO = Format$(ord.ord(f).pat_con)
          ss1!PRI_LET_FC = Format$(ord.ord(f).fst_FTC)
          ss1!L1 = Format$(ord.ord(f).L1)
          ss1!L2 = Format$(ord.ord(f).L2)
          ss1!L3 = Format$(ord.ord(f).L3)
          ss1!L4 = Format$(ord.ord(f).L4)
          ss1!L5 = Format$(ord.ord(f).L5)
          ss1!L6 = Format$(ord.ord(f).L6)
          ss1!L7 = Format$(ord.ord(f).L7)
          ss1!L8 = Format$(ord.ord(f).L8)
          ss1!L9 = Format$(ord.ord(f).L9)
          ss1!L10 = Format$(ord.ord(f).L10)
          'Robertouff 26-11-2007
          ss1!Ofs_Cut = Format$(ord.ord(f).Ofs_Cut)
          ss1.Update
        Else
          ss1.AddNew
          ss1!codice = Format$(ord.ord(f).pat_cod)
          ss1!CONTRASTO = Format$(ord.ord(f).pat_con)
          ss1!PRI_LET_FC = Format$(ord.ord(f).fst_FTC)
          ss1!L1 = Format$(ord.ord(f).L1)
          ss1!L2 = Format$(ord.ord(f).L2)
          ss1!L3 = Format$(ord.ord(f).L3)
          ss1!L4 = Format$(ord.ord(f).L4)
          ss1!L5 = Format$(ord.ord(f).L5)
          ss1!L6 = Format$(ord.ord(f).L6)
          ss1!L7 = Format$(ord.ord(f).L7)
          ss1!L8 = Format$(ord.ord(f).L8)
          ss1!L9 = Format$(ord.ord(f).L9)
          ss1!L10 = Format$(ord.ord(f).L10)
          'Robertouff 26-11-2007
          ss1!Ofs_Cut = Format$(ord.ord(f).Ofs_Cut)
          ss1.Update
        End If
      ss1.Close
    End If
  End If
End Function

Function err_cod_cut_pat(ord As Tip_Ordine, ind As Integer, Optional chk_for_abb As Boolean = True) As Boolean
'Iciliouff 20-11-03
'Ritorna true se il dato di codice del cut to pattern è errato o se il dato è
'impostato differentemente su due formati con lo stesso livello
Dim i As Integer                  'Indice di scansione della struttura
Dim cod_cut_pat_tmp As String     'Codice cut to pattern formato esaminato
Dim liv_tmp As Integer            'Livello formato esaminato

  err_cod_cut_pat = False
  If (ind < 1) Or (ind > 3) Then err_cod_cut_pat = True: Exit Function       'Controllo che l'indice ind sia compatibile con le strutture
  'agrassi_FI15-07-08
  'If chk_for_abb And (par_mac.opt.cut_pat = STR_TRUE) Then
  'agrassi_FI15-07-08
  'If (tmp_val = PRE_PATTERN) And Not (par_mac.opt.cut_pat_auto = STR_TRUE) And Not (par_mac.opt.cut_pat = STR_TRUE) Then err_cut_to_mark = 2: Exit Function
  Dim level As Integer
  level = val(ord.ord(ind).Ord_liv)
  If (level < 0) Or (level > 3) Then Exit Function
  If chk_for_abb And (par_mac.opt.cut_pat(level) = STR_TRUE) Then
    For i = 1 To 3
      If val(ord.ord(i).sca_lun) > 0 And i <> ind Then
        cod_cut_pat_tmp = ord.ord(i).pat_cod
        liv_tmp = val(ord.ord(i).Ord_liv)
        If liv_tmp = val(ord.ord(ind).Ord_liv) Then
          If cod_cut_pat_tmp <> ord.ord(ind).pat_cod Then err_cod_cut_pat = True: Exit Function           'Codice Cut to pattern diverso su due livelli
        End If
      End If
    Next i
  End If
  
End Function
Function err_abi_liv(ord As Tip_Ordine, ind As Integer) As Boolean
err_abi_liv = False
If val(ord.ord(ind).Ord_liv) >= 1 And val(ord.ord(ind).Ord_liv) <= 3 Then
  If Not par_abi.liv(val(ord.ord(ind).Ord_liv)) Then
    err_abi_liv = True
  End If
Else
  'Arturo uff 07-02-03
  'log_str "Modulo validazione_ordini Function err_abi_liv: valore inaspettato per l'espressione Val(ord.ord(ind).Ord_liv). Tale espressione vale infatti " + val(ord.ord(ind).Ord_liv), MED_PRIORITY, log_ord
  log_str "Modulo validazione_ordini Function err_abi_liv: valore inaspettato per l'espressione Val(ord.ord(ind).Ord_liv). Tale espressione vale infatti '" + ord.ord(ind).Ord_liv + "'", MED_PRIORITY, log_ord
End If
End Function

Function err_ass_lun(ord As Tip_Ordine, ind As Integer) As Boolean

err_ass_lun = False
If ((val(ord.ord(ind).sca_lun) \ TODECMM) = 0) Then err_ass_lun = True

End Function

Function err_ass_sca_lar(ord As Tip_Ordine, ind As Integer) As Boolean

err_ass_sca_lar = False
If (val(ord.ord(ind).sca_lar) \ TODECMM) = 0 Then err_ass_sca_lar = True

End Function

Function err_mis_pat(ord As Tip_Ordine, ind As Integer) As Boolean
'Ritorna se c'è un errore nelle misure delle tacche che costituiscono il pattern (L1..L10)
Dim pun As Integer, i As Integer
Dim Lx As Integer, Lmax As Integer, Lprec As Integer

  If ord.ord(ind).cut_to_mark = cut_pat Then
    For pun = 1 To 10
      'Cerco la prima tacca nulla e ricavo la lunghezza del pattern
      Lx = val(Choose(pun, ord.ord(ind).L1, ord.ord(ind).L2, ord.ord(ind).L3, _
            ord.ord(ind).L4, ord.ord(ind).L5, ord.ord(ind).L6, ord.ord(ind).L7, _
            ord.ord(ind).L8, ord.ord(ind).L9, ord.ord(ind).L10))
      If Lx > Lmax Then Lmax = Lx
      If Lx = 0 Then Exit For
    Next
    If pun = 1 Then err_mis_pat = True: Exit Function
    For i = pun To 10
      'Azzero le tacche successive alla prima nulla
      If i = 1 Then ord.ord(ind).L1 = Format$(0)
      If i = 2 Then ord.ord(ind).L2 = Format$(0)
      If i = 3 Then ord.ord(ind).L3 = Format$(0)
      If i = 4 Then ord.ord(ind).L4 = Format$(0)
      If i = 5 Then ord.ord(ind).L5 = Format$(0)
      If i = 6 Then ord.ord(ind).L6 = Format$(0)
      If i = 7 Then ord.ord(ind).L7 = Format$(0)
      If i = 8 Then ord.ord(ind).L8 = Format$(0)
      If i = 9 Then ord.ord(ind).L9 = Format$(0)
      If i = 10 Then ord.ord(ind).L10 = Format$(0)
    Next
    For i = 1 To pun - 1
      'Controllo se le tacche specificate sono sotto la lunghezza minima consentita
      Lx = val(Choose(i, ord.ord(ind).L1, ord.ord(ind).L2, ord.ord(ind).L3, _
            ord.ord(ind).L4, ord.ord(ind).L5, ord.ord(ind).L6, ord.ord(ind).L7, _
            ord.ord(ind).L8, ord.ord(ind).L9, ord.ord(ind).L10))
      If Lx < (5 / 64 * Lmax) Then err_mis_pat = True: Exit Function
      If i > 1 Then
        'Controllo che le tacche specificate abbiano valori incrementali in relazione al loro indice
        Lprec = val(Choose(i - 1, ord.ord(ind).L1, ord.ord(ind).L2, ord.ord(ind).L3, _
              ord.ord(ind).L4, ord.ord(ind).L5, ord.ord(ind).L6, ord.ord(ind).L7, _
              ord.ord(ind).L8, ord.ord(ind).L9, ord.ord(ind).L10))
        If Lx <= Lprec Then err_mis_pat = True: Exit Function
      End If
    Next
  End If

End Function

Function err_num_fog(ord As Tip_Ordine, ind As Integer) As Boolean
'controlla se è stato inserito il numero dei fogli per il formato (ind) dell'ordine

err_num_fog = False
If Trim$(ord.ord(ind).sca_tot_num) = "" Or val(ord.ord(ind).sca_tot_num) = 0 Then err_num_fog = True

End Function

Sub val_ord(s As Tip_Com_ord, Optional ins_man As Boolean = False)
'esegue i controlli per la validazione complessiva degli ordini
'restituisce s.ret con il codice di uscita
Dim i As Integer  'variabile per i cicli
Dim for_mac As Integer
Dim ass_for_pre As Boolean
Dim err_tmp As Integer
'inizializzazione del codice di ritorno
s.ret = RET_BAD

Select Case par_mac.car_tec.for_mac + 1
  Case mm1800
    for_mac = 1800
  Case mm2500
    for_mac = 2500
  Case mm2800
    for_mac = 2800
End Select
'E' importante che i controlli vengano eseguiti in questa sequenza per garantire la consistenza dei controlli stessi !
'controlli sull'ordine

If err_lun_Cod_ord_abb(s.rec) Then s.ret = COD_ERR_LUN_COD: Exit Sub
If err_ass_ond(s.rec) Then s.ret = COD_ERR_ASS_OND: Exit Sub
If err_ass_car(s.rec) Then s.ret = COD_ERR_ASS_CAR: Exit Sub
'Arturo uff 19-02-03 cambiato il controllo sulla massima canna con la larghezza massima dei formati
'If err_alt_bob(s.rec, for_mac) Then s.ret = COD_ERR_ALT_BOB: Exit Sub
' ABorelli_FI09/09/2005_1
'If err_lar_max_for(s.rec, for_mac) Then s.ret = COD_ERR_SOM_SCA: Exit Sub      ' ABorelli_FI09/09/2005_1

'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
If ordineVuoto(s.rec) Then
  s.ret = RET_OK
  Exit Sub
End If

'controlli sui formati
For i = 1 To 3
  If i = 1 And err_ass_liv(s.rec, i) Then s.ret = COD_ERR_ASS_DAT: Exit Sub
  If val(s.rec.ord(i).Sca_per_tag) <> 0 And (val(s.rec.ord(i).sca_lar) \ TODECMM) <> 0 And (val(s.rec.ord(i).sca_lun) \ TODECMM) <> 0 Then
    If ass_for_pre Then s.ret = COD_ERR_ASS_DAT: Exit Sub
    If err_ass_liv(s.rec, i) Then s.ret = COD_ERR_ASS_DAT: Exit Sub
    If err_abi_liv(s.rec, i) Then s.ret = COD_ERR_ABI_LIV: Exit Sub
    If err_cod_com(s.rec.ord(i).com.cod_com_ord) Then s.ret = COD_ERR_COD_COM: Exit Sub
    If err_max_sca_lar(s.rec) Then s.ret = COD_ERR_MAX_SCA_LAR: Exit Sub
    If err_ass_lun(s.rec, i) Then s.ret = COD_ERR_ASS_LUN: Exit Sub
    If err_sca_lun(s.rec, i) Then s.ret = COD_ERR_SCA_LUN: Exit Sub
    If err_num_sca(s.rec, i) Then s.ret = COD_ERR_ASS_SCA_PER_TAG: Exit Sub
    If err_som_sca(s.rec, i) Then s.ret = COD_ERR_SCA_PER_TAG: Exit Sub
    If err_num_fog(s.rec, i) Then s.ret = COD_ERR_ASS_NUM_FOG: Exit Sub
    'Iciliouff 20-11-2003
    err_tmp = err_cut_to_mark(s.rec, i)
    If err_tmp = 1 Then s.ret = COD_ERR_ASS_CUT_TO_MARK: Exit Sub
    If err_tmp = 2 Then s.ret = COD_ERR_CUT_TO_MARK: Exit Sub
    If err_tmp = 3 Then s.ret = COD_ERR_CUT_TO_MARK_LIV_ABB: Exit Sub
    If err_cod_cut_pat(s.rec, i) Then s.ret = COD_ERR_COD_PAT: Exit Sub
    If err_ofs_cut_pat(s.rec, i) Then s.ret = COD_ERR_OFS_CUT_PAT: Exit Sub
    If err_pri_let_FTC(s.rec, i) Then s.ret = COD_ERR_PRI_LET_FTC: Exit Sub  'Arturo uff 15-03-04
    If err_pat_con(s.rec, i) Then s.ret = COD_ERR_PAT_CON: Exit Sub
    If err_fin_pri(s.rec, i) Then s.ret = COD_ERR_FIN_PRI: Exit Sub
    If err_fin_dop(s.rec, i) Then s.ret = COD_ERR_FIN_DOP: Exit Sub
    If Not con_uff_ass_cod(s.rec, i, ins_man) Then s.ret = COD_ERR_COD_PAT: Exit Sub
    If err_mis_pat(s.rec, i) Then s.ret = COD_ERR_POS_PAT: Exit Sub
  Else
    'azzero i valori
    s.rec.ord(i).Ord_liv = " "
    ass_for_pre = True
  End If
Next i
    
If err_liv_dop(s.rec) Then s.ret = COD_ERR_LIV_LUN_DIV: Exit Sub     ' per uno stesso livello si sono specificati 2 formati con diversa lunghezza

'****************************************************
'se arrivo qui => ho passato tutti i controlli
s.ret = RET_OK

End Sub
Function err_liv_dop(ord As Tip_Ordine) As Boolean
' ritorna True se ho due formati sullo stesso livello con lunghezza diversa
Dim i As Integer, k As Integer, res As Boolean
res = False
For i = 1 To 3
  If (val(ord.ord(i).sca_lar) \ TODECMM) <> 0 And Trim$(ord.ord(i).Ord_liv) <> "" Then
    For k = 1 To 3
      If k <> i And (val(ord.ord(k).sca_lar) \ TODECMM) > 0 And Trim$(ord.ord(k).Ord_liv) = Trim$(ord.ord(i).Ord_liv) _
       And (val(ord.ord(i).sca_lun) \ TODECMM) <> (val(ord.ord(k).sca_lun) \ TODECMM) Then
        res = True
        Exit For
      End If
    Next k
  End If
  If res Then Exit For
Next i
err_liv_dop = res
End Function
Function err_ass_car(ord As Tip_Ordine) As Boolean
'controlla che sia inserito il tipo carta
  err_ass_car = ord.tip_car.cod_com_car = Space$(Len(ord.tip_car.cod_com_car))
End Function
Function err_ass_ond(ord As Tip_Ordine) As Boolean
'controlla che sia inserito un tipo di onda
err_ass_ond = ord.tip_car.Cod_ond = Space$(Len(ord.tip_car.Cod_ond))
End Function

Function err_alt_bob(ord As Tip_Ordine, for_mac As Integer) As Boolean
'controlla i limiti sull'altezza della bobina
Dim tmp As Long
tmp = val(Rie_nul(ord.alt_bob)) \ TODECMM
'Se il protocollo utilizzato è il Dig4 2L allora non faccio il controllo sui limiti dell'altezza bobina
'Con questo protocollo infatti non viene passato il valore dell'altezza bobina
'If par_mac.com_spde.ver_spde = Fosber10 Or par_mac.com_spde.ver_spde = Fosber11 Then
If par_mac.com_spde.ver_spde = Fosber10 Then  '30112001 Stagetti tolto protocollo fosber 1.1
  err_alt_bob = False
Else
  err_alt_bob = tmp < val(par_mac.car_tec.min_for_mac) Or tmp > for_mac
End If
End Function

Function err_lar_max_for(ord As Tip_Ordine, for_mac As Integer) As Boolean
'Arturo uff 19-02-03 controlla i limiti sulla larghezza complessiva dei formati
Dim tmp As Long
Dim frm As Integer

tmp = 0
For frm = 1 To 3
  With ord.ord(frm)
    If val(.sca_lar) > 0 And val(.Sca_per_tag) > 0 Then
      tmp = tmp + ((.sca_lar * .Sca_per_tag) / TODECMM)
    End If
  End With
Next frm
'Se il protocollo utilizzato è il Dig4 2L allora non faccio il controllo sui limiti dell'altezza bobina
'Con questo protocollo infatti non viene passato il valore dell'altezza bobina
'If par_mac.com_spde.ver_spde = Fosber10 Or par_mac.com_spde.ver_spde = Fosber11 Then
If par_mac.com_spde.ver_spde = Fosber10 Then  '30112001 Stagetti tolto protocollo fosber 1.1
  err_lar_max_for = False
Else
  err_lar_max_for = tmp < val(par_mac.car_tec.min_for_mac) Or tmp > for_mac
End If
End Function
Function err_cod_com(cod_com As String) As Boolean
Dim i As Integer
Dim tmp_cod_com As String

tmp_cod_com = Trim$(UCase$(cod_com))
  For i = 1 To Len(tmp_cod_com)
'Arturo uff 12-05-03 (1)
'    If ((Asc(Mid$(tmp_cod_com, i, 1)) < 48) Or (Asc(Mid$(tmp_cod_com, i, 1)) > 90) Or ((Asc(Mid$(tmp_cod_com, i, 1)) > 57) _
'        And (Asc(Mid$(tmp_cod_com, i, 1)) < 65))) And (Asc(Mid$(tmp_cod_com, i, 1)) <> 32) And (Asc(Mid$(tmp_cod_com, i, 1)) <> 45) And (Asc(Mid$(tmp_cod_com, i, 1)) <> 46) Then
'      err_cod_com = True
'      Exit Function
'    End If
    If (Asc(Mid$(tmp_cod_com, i, 1)) < 32) Or (Asc(Mid$(tmp_cod_com, i, 1)) > 127) Then
      err_cod_com = True
      Exit Function
    End If
  Next i
End Function

Function err_max_sca_lar(ord As Tip_Ordine) As Boolean
'controlla la larghezza massima di una scatola
Dim i As Integer
Dim tmp As Long

tmp = 0
'Se il protocollo utilizzato è il Dig4 2L allora non faccio il controllo sulla larghezza massima di una scatola
'Con questo protocollo infatti non viene passato il valore dell'altezza bobina
'If par_mac.com_spde.ver_spde = Fosber10 Or par_mac.com_spde.ver_spde = Fosber11 Then
If par_mac.com_spde.ver_spde = Fosber10 Then '30112001 Stagetti tolto protocollo fosber 1.1
  err_max_sca_lar = False
Else
  For i = 1 To 3
    If Trim$(ord.ord(i).Ord_liv) <> "" Then
      If (val(ord.ord(i).sca_lar) \ TODECMM) > (val(ord.alt_bob) \ TODECMM) Then
          err_max_sca_lar = True
          Exit Function
      End If
      If Trim$(ord.ord(i).Sca_per_tag) = "" Then
          tmp = tmp + (val(ord.ord(i).sca_lar) \ TODECMM) * 1
      Else
          tmp = tmp + (val(ord.ord(i).sca_lar) \ TODECMM) * val(ord.ord(i).Sca_per_tag)
      End If
    End If
  Next i
  err_max_sca_lar = tmp > (val(ord.alt_bob) \ TODECMM)
End If
End Function

Function err_num_sca(ord As Tip_Ordine, ind As Integer) As Boolean
'controlla se il numero di scatole è eccessivo

err_num_sca = False
If val(ord.ord(ind).Sca_per_tag) = 0 Then err_num_sca = True

End Function



Function err_cut_to_mark(ord As Tip_Ordine, ind As Integer, Optional chk_for_abb As Boolean = True) As Integer
'Iciliouff 20-11-2003
Dim tmp_val As Integer, tmp_liv As Integer, i As Integer

  err_cut_to_mark = False
  'Controllo validità di ind
  If ind < 1 Or ind > 3 Then err_cut_to_mark = RET_BAD: Exit Function
  tmp_val = val(ord.ord(ind).cut_to_mark)
  'Se val(cut_to_mark)=0 formatto con valore di default
  If tmp_val = 0 Then ord.ord(ind).cut_to_mark = Format$(NO): Exit Function
  'Controllo il valore impostato per cut_to_mark
  If (tmp_val < NO) Or (tmp_val > PRE_PATTERN) Then err_cut_to_mark = 1: Exit Function
  'Controllo se il valore impostato sia coerente con i settaggi macchina
  If (tmp_val = SI) And Not (par_mac.opt.cut_mar = STR_TRUE) Then err_cut_to_mark = 2: Exit Function
  'Robertouff 26-11-2007
  'RFAZZI_FI12/03/2008. Rimosso bug in caso di esistenza Cut-to-Mark
  'If Not (tmp_val = PRE_PATTERN) And Not (par_mac.opt.cut_pat_auto = STR_TRUE) Then err_cut_to_mark = 2: Exit Function
  'agrassi_FI15-07-08
  'If (tmp_val = PRE_PATTERN) And Not (par_mac.opt.cut_pat_auto = STR_TRUE) And Not (par_mac.opt.cut_pat = STR_TRUE) Then err_cut_to_mark = 2: Exit Function
  Dim level As Integer
  level = val(ord.ord(ind).Ord_liv)
  If (level < 0) Or (level > 3) Then Exit Function
  
  'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
  'If (tmp_val = PRE_PATTERN) And Not (par_mac.opt.cut_pat_auto = STR_TRUE) And Not (par_mac.opt.cut_pat(level) = STR_TRUE) Then err_cut_to_mark = 2: Exit Function
  If (tmp_val = PRE_PATTERN) And Not (par_mac.opt.cut_pat_auto(level) = STR_TRUE) And Not (par_mac.opt.cut_pat(level) = STR_TRUE) Then err_cut_to_mark = 2: Exit Function
  'Controllo che il valore di cut_to_mark sia identico su due formati abbinati
  'Robertouff 26-11-2007
  'agrassi_FI15-07-08
  'If chk_for_abb And ((par_mac.opt.cut_pat = STR_TRUE) Or (par_mac.opt.cut_pat_auto = STR_TRUE)) Then
  
  'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
  'If chk_for_abb And ((par_mac.opt.cut_pat(level) = STR_TRUE) Or (par_mac.opt.cut_pat_auto = STR_TRUE)) Then
  If chk_for_abb And ((par_mac.opt.cut_pat(level) = STR_TRUE) Or (par_mac.opt.cut_pat_auto(level) = STR_TRUE)) Then
    For i = 1 To 3
      If val(ord.ord(i).sca_lar) > 0 And i <> ind Then
        tmp_liv = val(ord.ord(ind).Ord_liv)
        If tmp_liv = val(ord.ord(i).Ord_liv) Then
          If tmp_val <> val(ord.ord(i).cut_to_mark) Then err_cut_to_mark = 3: Exit Function
        End If
      End If
    Next i
  End If
  
End Function

Function err_pos_pat(ord As Tip_Ordine, ind As Integer, Optional chk_for_abb As Boolean = True) As Boolean
'RFAZZI_FI 07-01-08
'Ritorna true se il dato di posizione del pattern è errato
Dim i As Integer

For i = 1 To 3
'RFAZZI_FI11/12/2009_1. Bug Fix. ord_mac non è valorizzato in fase di inserimento di un nuovo ordine ma va bene solo in casi di modifica
'  If val(ord.ord(i).cut_to_mark) > 0 And _
'    ((val(ord.ord(i).pos_pattern) < 0) Or _
'    (val(ord.ord(i).pos_pattern) > (ord_mac.item(ind).Alt_ord / TODECMM))) Then
  If val(ord.ord(i).cut_to_mark) > 0 And _
    ((val(ord.ord(i).pos_pattern) < 0) Or _
    (val(ord.ord(i).pos_pattern) > (ord.alt_bob / TODECMM))) Then
        err_pos_pat = True
        Exit Function
  End If
  
Next i

err_pos_pat = False
 
End Function

Function err_pat_con(ord As Tip_Ordine, ind As Integer, Optional chk_for_abb As Boolean = True) As Boolean
'Iciliouff 20-11-2003
Dim tmp_val As Integer, tmp_liv As Integer, i As Integer

  err_pat_con = False
  'Controllo validità di ind
  If ind < 1 Or ind > 3 Then err_pat_con = True: Exit Function
  tmp_val = val(ord.ord(ind).pat_con)
  If tmp_val = 0 Then ord.ord(ind).pat_con = String(Len(ord.ord(ind).pat_con), "0"): Exit Function
  'Controllo il valore impostato per pat_con (fra 0 e 4)
  If (tmp_val < MIN_NUM_CTR) Or (tmp_val > MAX_NUM_CTR) Then err_pat_con = True: Exit Function
  'Controllo che il valore impostato per pat_con sia identico su due formati abbinati
  'agrassi_FI15-07-08
  'If chk_for_abb And (par_mac.opt.cut_pat = STR_TRUE) Then
  Dim level As Integer
  level = val(ord.ord(ind).Ord_liv)
  If (level >= 0) And (level <= 3) Then
    If chk_for_abb And (par_mac.opt.cut_pat(level) = STR_TRUE) Then
      For i = 1 To 3
        If val(ord.ord(i).sca_lar) > 0 And i <> ind Then
          tmp_liv = val(ord.ord(ind).Ord_liv)
          If tmp_liv = val(ord.ord(i).Ord_liv) Then
            If tmp_val <> val(ord.ord(i).pat_con) Then err_pat_con = True: Exit Function
          End If
        End If
      Next i
    End If
  End If
 
End Function

Function err_pri_let_FTC(ord As Tip_Ordine, ind As Integer, Optional chk_for_abb As Boolean = True, Optional i As Integer) As Boolean
'Arturo uff 15-03-04
'ord rappresenta l'ordine
'f rappresenta il formato da controllare
'chk_for_abb specifica se controllare la validità del dato sui formati
'i rappresenta il formato su cui si è verificato che c'è un errore rispetto al formato f
Dim tmp_val As Integer, tmp_liv As Integer

  err_pri_let_FTC = False
  'Controllo validità di ind
  If ind < 1 Or ind > 3 Then err_pri_let_FTC = True: Exit Function
  tmp_val = val(ord.ord(ind).fst_FTC)
  If tmp_val = 0 Then ord.ord(ind).fst_FTC = String(Len(ord.ord(ind).fst_FTC), "0"): Exit Function
  'Controllo il valore impostato per pat_con (fra 0 e 4)
  If (tmp_val < MIN_LET_FTC) Or (tmp_val > MAX_LET_FTC) Then err_pri_let_FTC = True: Exit Function
  'Controllo che il valore impostato per pat_con sia identico su due formati abbinati
  'agrassi_FI15-07-08
  'If chk_for_abb And (par_mac.opt.cut_pat = STR_TRUE) Then
  Dim level As Integer
  level = val(ord.ord(ind).Ord_liv)
  If (level >= 1) And (level <= 3) Then
    If chk_for_abb And (par_mac.opt.cut_pat(level) = STR_TRUE) Then
      For i = 1 To 3
        If val(ord.ord(i).sca_lun) > 0 And i <> ind Then
          tmp_liv = val(ord.ord(ind).Ord_liv)
          If tmp_liv = val(ord.ord(i).Ord_liv) Then
            If tmp_val <> val(ord.ord(i).fst_FTC) Then err_pri_let_FTC = True: Exit Function
          End If
        End If
      Next i
    End If
  End If
 
End Function

Function err_fin_pri(ord As Tip_Ordine, ind As Integer, Optional chk_for_abb As Boolean = True) As Boolean
'Iciliouff 20-11-2003
Dim tmp_val As Integer, tmp_liv As Integer, tmp_lun As Integer, i As Integer

  err_fin_pri = False
  'Controllo validità di ind
  If ind < 1 Or ind > 3 Then err_fin_pri = True: Exit Function
  tmp_val = val(ord.ord(ind).fin_pri)
  tmp_lun = (val(ord.ord(ind).sca_lun) / TODECMM) * MUL_FIN_PRI_TAC
  If tmp_val = 0 Then ord.ord(ind).fin_pri = String(Len(ord.ord(ind).fin_pri), "0"): Exit Function
  'Controllo il valore impostato per fin_pri (fra 0 e 4)
  If (tmp_val < 0) Or (tmp_val > tmp_lun) Then err_fin_pri = True: Exit Function
  'Controllo che il valore impostato per fin_pri sia identico su due formati abbinati
  'agrassi_FI15-07-08
  'If chk_for_abb And (par_mac.opt.cut_pat = STR_TRUE) Then
  Dim level As Integer
  level = val(ord.ord(ind).Ord_liv)
  If (level >= 1) And (level <= 3) Then
    If chk_for_abb And (par_mac.opt.cut_pat(level) = STR_TRUE) Then
      For i = 1 To 3
        If val(ord.ord(i).sca_lar) > 0 And i <> ind Then
          tmp_liv = val(ord.ord(ind).Ord_liv)
          If tmp_liv = val(ord.ord(i).Ord_liv) Then
            If tmp_val <> val(ord.ord(i).fin_pri) Then err_fin_pri = True: Exit Function
          End If
        End If
      Next i
    End If
  End If
 
End Function
Function err_fin_dop(ord As Tip_Ordine, ind As Integer, Optional chk_for_abb As Boolean = True) As Boolean
'Iciliouff 20-11-2003
Dim tmp_val As Integer, tmp_liv As Integer, tmp_lun As Integer, i As Integer

  err_fin_dop = False
  'Controllo validità di ind
  If ind < 1 Or ind > 3 Then err_fin_dop = True: Exit Function
  tmp_val = val(ord.ord(ind).fin_dop)
  tmp_lun = (val(ord.ord(ind).sca_lun) / TODECMM) * MUL_FIN_DOP_TAC
  If tmp_val = 0 Then ord.ord(ind).fin_dop = String(Len(ord.ord(ind).fin_dop), "0"): Exit Function
  'Controllo il valore impostato per fin_dop (fra 0 e 4)
  If (tmp_val < 0) Or (tmp_val > tmp_lun) Then err_fin_dop = True: Exit Function
  'Controllo che il valore impostato per fin_dop sia identico su due formati abbinati
  'agrassi_FI15-07-08
  'If chk_for_abb And (par_mac.opt.cut_pat = STR_TRUE) Then
  Dim level As Integer
  level = val(ord.ord(ind).Ord_liv)
  If (level >= 1) And (level <= 3) Then
    If chk_for_abb And (par_mac.opt.cut_pat(level) = STR_TRUE) Then
      For i = 1 To 3
        If val(ord.ord(i).sca_lar) > 0 And i <> ind Then
          tmp_liv = val(ord.ord(ind).Ord_liv)
          If tmp_liv = val(ord.ord(i).Ord_liv) Then
            If tmp_val <> val(ord.ord(i).fin_dop) Then err_fin_dop = True: Exit Function
          End If
        End If
      Next i
    End If
  End If
 
End Function
Function err_ofs_cut_pat(ord As Tip_Ordine, ind As Integer, Optional chk_for_abb As Boolean = True) As Boolean
'Iciliouff 20-11-2003
Dim tmp_val As Integer, tmp_liv As Integer, tmp_lun As Integer, i As Integer

  err_ofs_cut_pat = False
  'Controllo validità di ind
  If ind < 1 Or ind > 3 Then err_ofs_cut_pat = True: Exit Function
  tmp_val = val(ord.ord(ind).Ofs_Cut)
  tmp_lun = (val(ord.ord(ind).sca_lun) / TODECMM) * MUL_OFS_CUT_PAT
  If tmp_val = 0 Then ord.ord(ind).Ofs_Cut = String(Len(ord.ord(ind).Ofs_Cut), "0"): Exit Function
  'Controllo il valore impostato per ofs_cut (fra -100 e la metà della lunghezza del foglio)
  If (tmp_val < MIN_OFF_PAT) Or (tmp_val > tmp_lun) Then err_ofs_cut_pat = True: Exit Function
  'Controllo che il valore impostato per ofs_cut sia identico su due formati abbinati
  'agrassi_FI15-07-08
  'If chk_for_abb And (par_mac.opt.cut_pat = STR_TRUE) Then
  Dim level As Integer
  level = val(ord.ord(ind).Ord_liv)
  If (level >= 1) And (level <= 3) Then
    If chk_for_abb And (par_mac.opt.cut_pat(level) = STR_TRUE) Then
      For i = 1 To 3
        If val(ord.ord(i).sca_lar) > 0 And i <> ind Then
          tmp_liv = val(ord.ord(ind).Ord_liv)
          If tmp_liv = val(ord.ord(i).Ord_liv) Then
            If tmp_val <> val(ord.ord(i).Ofs_Cut) Then err_ofs_cut_pat = True: Exit Function
          End If
        End If
      Next i
    End If
  End If
 
End Function

Function err_som_sca(ord As Tip_Ordine, ind As Integer) As Boolean
'controlla se il numero delle scatole moltiplicato per la larghezza eccede l'altezza bobina
Dim i As Integer
Dim tmp As Long
  tmp = 0
  'Se il protocollo utilizzato è il Dig4 2L allora non faccio questo controllo
  'Con questo protocollo infatti non viene passato il valore dell'altezza bobina
  'If par_mac.com_spde.ver_spde = Fosber10 Or par_mac.com_spde.ver_spde = Fosber11 Then
  If par_mac.com_spde.ver_spde = Fosber10 Then '30112001 Stagetti tolto protocollo fosber 1.1
    err_som_sca = False
  Else
    For i = 1 To 3
      If Trim$(ord.ord(i).Ord_liv) <> "" Then
        If Trim$(ord.ord(i).Sca_per_tag) = "" Then
            tmp = tmp + (val(ord.ord(i).sca_lar) \ TODECMM) * 1
        Else
            tmp = tmp + (val(ord.ord(i).sca_lar) \ TODECMM) * val(ord.ord(i).Sca_per_tag)
        End If
      End If
    Next i
    err_som_sca = tmp > (val(ord.alt_bob) \ TODECMM)
  End If
End Function

Function err_ass_liv(ord As Tip_Ordine, ind As Integer) As Boolean
'controlla che sia stato inserito il livello di lavorazione
'Arturo uff 07-02-03 inserito controllo su validità livello di lavorazione (monolivllo: SUP; doppio: SUP, INF; triplo: SUP, INF, MED)

err_ass_liv = ord.ord(ind).Ord_liv = Space$(Len(ord.ord(ind).Ord_liv))

'Arturo uff 07-02-03
Select Case val(par_mac.car_tec.num_liv_tag)
  Case 1
    If InStr("1", Trim$(ord.ord(ind).Ord_liv)) = 0 Then err_ass_liv = True
  Case 2
    If InStr("12", Trim$(ord.ord(ind).Ord_liv)) = 0 Then err_ass_liv = True
  Case 3
    If InStr("123", Trim$(ord.ord(ind).Ord_liv)) = 0 Then err_ass_liv = True
End Select

End Function

Function err_lun_Cod_ord_abb(ord As Tip_Ordine) As Boolean
'controlla che esista un codice cliente
err_lun_Cod_ord_abb = (Len(Trim$(ord.cod_ord_abb)) = 0)
End Function

Function err_sca_lun(ord As Tip_Ordine, ind As Integer) As Boolean

err_sca_lun = False
If ((val(ord.ord(ind).sca_lun) \ TODECMM) > val(par_mac.car_tec.max_lun_tag)) Or ((val(ord.ord(ind).sca_lun) \ TODECMM) < val(par_mac.car_tec.min_lun_tag)) Then
  err_sca_lun = True
End If
End Function


