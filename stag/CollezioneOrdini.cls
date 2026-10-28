VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "CollezioneOrdini"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit
Private Ordini As New Collection
Private ese As Boolean
Private Iniz As Boolean
Private pun_ord As Integer
Public Event Refresh(Shift As Boolean)

Friend Function Init(eseg As Boolean) As cod_ret
'Inizializza la proprietà Ese e poi blocca la possibilità di modificarla ponendo Iniz=False
If Iniz = False Then
  ese = eseg
  Iniz = True
  Init = RET_OK
Else
  Init = RET_BAD
End If
End Function

Public Function leg_pun_ord() As Integer
leg_pun_ord = pun_ord
End Function


Friend Function Move_blk(ini As Integer, fin As Integer, dst As Integer) As cod_ret
'Sposta il blocco da ini a fin nella posizione dst
Dim ord() As ClasseOrdine
Dim app As Integer
Dim i As Integer

  Move_blk = RET_BAD
  If (ini > Ordini.Count) Or (fin > Ordini.Count) Or (dst > Ordini.Count) Or (ini < 1) Or (fin < 1) Or (dst < 1) Then
    log_str "Mov_blk: Un dato eccedente il numero degli ordini presenti", MED_PRIORITY, log_ord
    messaggio msg("imp_ese_ope"), True, False
    Exit Function
  End If
  If ini > fin Then
    app = fin
    fin = ini
    ini = app
  End If
  If (dst = ini) Then
    Move_blk = RET_OK
    Exit Function
  End If
  If (dst > ini) And (dst <= fin) Then
    log_str "Mov_blk: Dati ambigui: ini= " & ini & " fin = " & fin & " dst = " & dst, MED_PRIORITY, log_ord
    messaggio msg("imp_ese_ope"), True, False
    Move_blk = RET_BAD
    Exit Function
  End If
  If sta_tag.MAC_IN_MANUALE(1) Or Not sta_tag.spde_OK Then
    ReDim ord(1 To (fin - ini + 1))
    For i = ini To fin
      Set ord(i - ini + 1) = Ordini(i)
    Next i
    For i = ini To fin
      Ordini.Remove ini
      'Attenzione! sembra che l'indice sia sbagliato, ma non lo è
    Next i
    For i = 1 To (fin - ini + 1)
      If ((dst + i - 1) <= Ordini.Count) Then
        Ordini.add ord(i), , (dst + i - 1)
      Else
        Ordini.add ord(i)
      End If
    Next i
    Salva (False)
    Move_blk = RET_OK
    pun_ord = dst
  Else
    messaggio msg("mac_in_auto"), True, False
    log_str "Move_blk: Tentativo di spostamento di ordini con macchina macchina in automatico", MED_PRIORITY, log_ord
    Exit Function
  End If
  
End Function

Friend Function Cpy_blk(ini As Integer, fin As Integer, dst As Integer) As cod_ret
'Copia il blocco da ini a fin nella posizione dst (gli ordini sorgenti possono anche essere eseguiti)
'Attenzione! nel caso sor_ese=True l'istanza della classe cui il metodo si riferisce è Ord_mac_ese,
'quindi la collezione Ordini è riferita agli eseguiti; se voglio fare operazioni sugli ordini da eseguire
'devo lavorare sull'istanza della classe Ord_mac attraverso i suoi metodi e le sue proprietà pubbliche
Dim ord() As New ClasseOrdine
Dim tmp_ord As New ClasseOrdine
Dim tmp As Integer
Dim i As Integer, Out As Boolean
  Cpy_blk = RET_BAD
  Out = False
  If ese Then
    log_str "Cpy_blk: Metodo non utilizzabile per ord_mac_ese", MED_PRIORITY, log_ord
    Exit Function
  End If
  If (ini > Ordini.Count) Or (fin > Ordini.Count) Or (dst > Ordini.Count + 1) Or (ini < 1) Or (fin < 1) Or (dst < 1) Then
    log_str "Cpy_blk: Un dato eccedente il numero degli ordini presenti", MED_PRIORITY, log_ord
    Exit Function
  End If
  If ini > fin Then
    tmp = fin
    fin = ini
    ini = tmp
  End If
  If (dst = Ordini.Count + 1) Then Out = True
  If (Ordini.Count + (fin - ini + 1)) > MAX_NUM_ORD Then
    log_str "Cpy_blk: Si eccede il numero massimo di ordini", MED_PRIORITY, log_ord
    messaggio msg("rag_num_max"), True, False
    Exit Function
  End If
  If sta_tag.MAC_IN_MANUALE(1) Or Not sta_tag.spde_OK Then
    ReDim ord(1 To (fin - ini + 1))
    For i = ini To fin
      Set tmp_ord = Ordini(i)
      ord(i - ini + 1).ordine = tmp_ord.ordine
    Next i
    For i = 1 To (fin - ini + 1)
        ord(i).stag_id = Cod_uni
        If Not Out Then
          Ordini.add ord(i), , dst + i - 1
        Else
          Ordini.add ord(i)
        End If
    Next i
    Salva (False)
    Cpy_blk = RET_OK
    pun_ord = dst
  Else
    log_str "Cpy_blk: Tentativo di copia ordini con macchina in automatico", MED_PRIORITY, log_ord
    messaggio msg("mac_in_auto"), True, False
    Exit Function
  End If
End Function

Friend Sub Fine()
'Procedura finale che scrive nel file Ordini.ini il codice del primo elemento della lista
Dim cod As String * 4
Dim tmp_ord As ClasseOrdine
  If Count > 0 Then
    Set tmp_ord = Ordini(1)
    cod = tmp_ord.stag_id
  Else
    cod = Format$(0, FT4)
  End If
  setFileINIString "ORD_SET", IIf(ese, "Cod_primo_ese", "Cod_primo"), cod, "Ordini"
End Sub



Private Function Cod_uni() As Integer
Dim cod As Integer
  Cod_uni = val(getFileINIString("ORD_SET", "Cod_uni", "9000", "Ordini"))
  cod = IIf(Cod_uni >= 9999 Or Cod_uni < 9000, 9000, Cod_uni + 1)
  setFileINIString "ORD_SET", "Cod_uni", Format$(cod, FT4), "Ordini"
End Function



Friend Function Modifica(ord As ClasseOrdine, pos As Integer, Optional rcv_ord_spde As Boolean = False) As cod_ret
'Modifica un ordine della collezione
'L'ordine passato viene sostituito a quello in posizione Pos
Dim tmp_id As String * 4
Dim tmp_mod As String * 2

  Modifica = RET_BAD
  If (pos > Ordini.Count + 1) Or (pos < 1) Then
    log_str "Modifica: Posizione maggiore del numero di ordini presenti", MED_PRIORITY, log_ord
    Modifica = RET_BAD
    Exit Function
  End If
  
  If ese Then
    Ordini.Remove pos
    If pos > 1 Then
      Ordini.add ord, , , (pos - 1)
    Else
      If Ordini.Count > 0 Then
        Ordini.add ord, , pos
      Else
        Ordini.add ord
      End If
    End If
  Else
    If Not rcv_ord_spde Then
      tmp_id = Ordini(pos).stag_id
      tmp_mod = Ordini(pos).mod_id
    End If
    Ordini.Remove pos
    If Not rcv_ord_spde Then
      ord.stag_id = Format$(val(tmp_id), FT4)
      ord.mod_id = Format$((val(tmp_mod) + 1) Mod 100, FT2)
    Else
      ord.stag_id = Cod_uni
      'If par_mac.com_spde.ver_spde = Fosber11 Or par_mac.com_spde.ver_spde = Fosber10 Then ord.mod_id = "00"
      If par_mac.com_spde.ver_spde = Fosber10 Then ord.mod_id = "00" '30112001 Stagetti tolto protocollo fosber 1.1
    End If
    If pos > 1 Then
      Ordini.add ord, , , (pos - 1)
    Else
      If Ordini.Count > 0 Then
        Ordini.add ord, , pos
      Else
        Ordini.add ord
      End If
    End If
  End If
  Modifica = RET_OK
  Salva (False)
  
  'Icilio3545 09-01-2004 Forza il reinvio del corrente/successivo a scheda di taglio se necessario
  If pos = CORRENTE Or pos = SUCCESSIVO Then
    inv_ord_sch_tag(pos) = True
  End If
  
  
End Function
Friend Function add(ByVal ord As ClasseOrdine, Optional pos As Integer = MAXINT, Optional rcv_ord_spde As Boolean = False) As cod_ret
'Aggiunge un ordine alla collezione
'L'ordine viene aggiunto in posizione Pos, se questo dato è presente
'Altrimenti si aggiunge in fondo alla collezione
'Nel caso di ordini eseguiti, la posizione in cui viene inserito l'ordine è sempre 1, indipendentemente da Pos
Dim tmp_ord As New ClasseOrdine
Dim i As Integer

  add = RET_BAD
  If ese Then
    If Ordini.Count > MAX_NUM_ORD_ESE - 1 Then
      Do
        Ordini.Remove Ordini.Count
      Loop While Ordini.Count > MAX_NUM_ORD_ESE - 1
    End If
  Else
    If Ordini.Count > MAX_NUM_ORD - 1 Then
      log_str "Add: impossibile aggiungere ordine, collezione piena", MED_PRIORITY, log_ord
      add = RET_BAD
      Exit Function
    End If
  End If
  Set tmp_ord = New ClasseOrdine
  tmp_ord.ordine = ord.ordine
  If ese Then
    pos = 1
  Else
    'If Not rcv_ord_spde Or par_mac.com_spde.ver_spde = Fosber10 Or par_mac.com_spde.ver_spde = Fosber11 Then
    If Not rcv_ord_spde Or par_mac.com_spde.ver_spde = Fosber10 Then '30112001 Stagetti tolto protocollo fosber 1.1
      tmp_ord.mod_id = "00"
    End If
  End If
  If pos = MAXINT Then
    tmp_ord.stag_id = Cod_uni
    Ordini.add tmp_ord
    add = RET_OK
  Else
    If (pos > Ordini.Count + 1) Or (pos < 1) Then
      log_str "Add: Posizione maggiore del numero di ordini presenti", MED_PRIORITY, log_ord
      add = RET_BAD
      Exit Function
    End If
    tmp_ord.stag_id = Cod_uni
    
    If Ordini.Count > 0 And pos <= Ordini.Count Then
      Ordini.add tmp_ord, , pos
    Else
      Ordini.add tmp_ord
    End If
    add = RET_OK
  End If
  If pos > 0 And pos <> MAXINT Then
    pun_ord = pos
  Else
    pun_ord = Ordini.Count
  End If
  Salva (False)
  
End Function
Friend Function Count() As Integer
'Restituisce il numero di ordini nella collezione
Count = Ordini.Count
End Function




Friend Function Del_blk(ini As Integer, fin As Integer, Shift As Boolean, cmd_spde As Boolean, Optional agg_rep_pro_old_ord As Boolean = False) As cod_ret
'Rimuovo gli ordini da ini a fin
Dim app As Integer
Dim i As Integer

  'andrea per rom
  On Error GoTo g_err
  Del_blk = RET_BAD
  If (ini > Ordini.Count) Or (fin > Ordini.Count) Or (ini < 1) Or (fin < 1) Then
    log_str "Del_blk: Un dato eccedente il numero di ordini presenti", MED_PRIORITY, log_ord
    Exit Function
  End If
  If ini > fin Then
    app = ini
    ini = fin
    fin = app
  End If
  If ese And agg_rep_pro_old_ord Then
    'caso di cancellazione 1° record degli eseguiti per aggiornamento dello stesso con il nuovo report di produzione
    For i = ini To fin
      Ordini.Remove (ini)
      'Attenzione! sembra che l'indice sia sbagliato, ma non lo è
    Next i
    Salva (Shift)
    Del_blk = RET_OK
    log_str "Del_blk: Cancellazione ordine da ord_mac_ese per aggiornarlo con i nuovi dati di produzione", MED_PRIORITY, log_ord
    Exit Function
  End If
  If sta_tag.MAC_IN_MANUALE(1) Or Not par_abi.abi_com_PU Or Not sta_tag.spde_OK Or Shift Or cmd_spde Then
    For i = ini To fin
      Ordini.Remove (ini)
      'Attenzione! sembra che l'indice sia sbagliato, ma non lo è
    Next i
    pun_ord = IIf(ini > 1, ini - 1, 1)
    Salva (Shift)
    Del_blk = RET_OK
    Exit Function
  Else
    messaggio msg("mac_in_auto"), True, False
    log_str "Del_blk: Tentativo di cancellazione di ordini con macchina in automatico", MED_PRIORITY, log_ord
    Exit Function
  End If
Exit Function

g_err:
log_str "Del_blk: errore " + Format(err.Number) + ", " + err.Description, MED_PRIORITY, log_gen
End Function


 
Friend Sub Leg_dat_ord()
'Procedura iniziale che popola la collezione con gli ordini memorizzati in Ord_mac.dat o Ord_mac_ese.dat
Dim Cod_primo As String * 4
Dim ord As New ClasseOrdine
Dim tmp_ord As Tip_Ordine
Dim i As Integer
  pun_ord = 0
  Cod_primo = Trim$(getFileINIString("ORD_SET", IIf(ese, "Cod_primo_ese", "Cod_primo"), "9000", pro_set.Nam_fil_uni))
  If Dir$(pro_set.pro_pat + IIf(ese, pro_set.Nam_fil_dat_ord_ese, pro_set.Nam_fil_dat_ord)) = "" Then
    Crea_fil_ord
  End If
  Open pro_set.pro_pat + IIf(ese, pro_set.Nam_fil_dat_ord_ese, pro_set.Nam_fil_dat_ord) For Random As #NUM_FIL_ORD Len = Len(tmp_ord)
  For i = 1 To IIf(ese, MAX_NUM_ORD_ESE, MAX_NUM_ORD)
    inc_prg_bar_ini 'Arturo uff 25-07-02
    Get #NUM_FIL_ORD, i, tmp_ord
    Set ord = New ClasseOrdine
    ord.ordine = Ord_str(tmp_ord)
    If i = 1 Then
      If (ord.stag_id <> Cod_primo) And (val(ord.stag_id) <> 0) Then
        log_str "leg_dat_ord: Il codice del primo ordine non corrisponde a quello memorizzato", MED_PRIORITY, log_ord
        Prb_lis_ord = True
      End If
    End If
    If (val(ord.stag_id) <> 0) Then
      Ordini.add ord
      If i = 1 Then pun_ord = 1
    End If
  Next i
  Close #NUM_FIL_ORD
End Sub

Private Sub Crea_fil_ord()
Dim ord_nul As New ClasseOrdine
Dim tmp_ord_nul As Tip_Ordine
Dim ind_ord As Integer
Dim i As Integer
  Open pro_set.pro_pat + IIf(ese, pro_set.Nam_fil_dat_ord_ese, pro_set.Nam_fil_dat_ord) For Random As #NUM_FIL_ORD Len = Len(tmp_ord_nul)
  ind_ord = IIf(ese, MAX_NUM_ORD_ESE, MAX_NUM_ORD)
  For i = 1 To ind_ord
    tmp_ord_nul = str_ord(ord_nul.ordine)
    Put #NUM_FIL_ORD, i, tmp_ord_nul
  Next i
  Close #NUM_FIL_ORD
End Sub
Friend Function item(ByVal indice As Integer) As ClasseOrdine
'Restituisce l'ordine in posizione indice
Dim tmp_ord_nul As Tip_Ordine
Dim ord_nul As New ClasseOrdine
  If (indice < 1) Or (indice > Ordini.Count) Then
    ord_nul.ordine = Ord_str(tmp_ord_nul)
    log_str "Ord_lis: Indice " + Format$(indice) + " fuori range", MED_PRIORITY, log_ord
    Set item = ord_nul
    Exit Function
  End If
Set item = Ordini(indice)
End Function

Public Sub pun_ord_mov_next()

pun_ord = IIf(pun_ord < Ordini.Count, pun_ord + 1, pun_ord)

End Sub

Public Sub pun_ord_mov_prev()

pun_ord = IIf(pun_ord > 1, pun_ord - 1, pun_ord)

End Sub


Friend Sub Salva(Shift As Boolean)
Dim ord_nul As New ClasseOrdine
Dim ord As New ClasseOrdine
Dim tmp_ord As Tip_Ordine
Dim i As Integer
'Salva gli ordini della collezione nel file Ord_mac.dat o Ord_mac_ese.dat
  Open pro_set.pro_pat + IIf(ese, pro_set.Nam_fil_dat_ord_ese, pro_set.Nam_fil_dat_ord) For Random As #NUM_FIL_ORD Len = Len(tmp_ord)
  For i = 1 To IIf(ese, MAX_NUM_ORD_ESE, MAX_NUM_ORD)
    If (i <= Ordini.Count) Then
      Set ord = Ordini(i)
      tmp_ord = str_ord(ord.ordine)
      Put #NUM_FIL_ORD, i, tmp_ord
    Else
      Put #NUM_FIL_ORD, i, str_ord(ord_nul.ordine)
    End If
  Next i
  Close #NUM_FIL_ORD
RaiseEvent Refresh(Shift)
End Sub













Public Sub scr_pun_ord(nuo_pun_ord As Integer)
pun_ord = nuo_pun_ord
End Sub

Friend Function Tro_id_lis(ID As String) As cod_ret
'Restituisce la posizione nella collezione, dell'ordine con codice stag_id=cod
'Se il codice non corrisponde a nessun ordine, la funzione restituisce 0
Dim i As Integer
Dim tmp_ord As New ClasseOrdine
  ID = Right$("0000" + ID, 4)
  Tro_id_lis = RET_BAD
  For i = 1 To Ordini.Count
    Set tmp_ord = Ordini(i)
    If Trim$(tmp_ord.stag_id) = ID Then
        Exit For
    End If
  Next i
  If i <= Ordini.Count Then
    Tro_id_lis = RET_OK
    pun_ord = i
  Else
    log_str "Tro_id_lis: Ordine con Stag_id " + ID + " non trovato", MED_PRIORITY, log_ord
  End If
End Function

Friend Function Tro_lis(cod As String) As cod_ret
'Restituisce la posizione nella collezione, dell'ordine con codice cod
'Se il codice non corrisponde a nessun ordine, la funzione restituisce 0
Dim i As Integer
Dim tmp_ord As New ClasseOrdine
  cod = UCase$(Trim$(cod))
  Tro_lis = RET_BAD
  For i = 1 To Ordini.Count
    Set tmp_ord = Ordini(i)
    If UCase$(Trim$(tmp_ord.cod_ord_abb)) = cod Then
        Exit For
    End If
  Next i
  If i <= Ordini.Count Then
    Tro_lis = RET_OK
    pun_ord = i
  Else
    log_str "Tro_lis: Ordine con codice " + cod + " non trovato", MED_PRIORITY, log_ord
  End If
End Function


Private Sub Class_Initialize()
Iniz = False
End Sub


Private Sub Class_Terminate()
Iniz = False
End Sub



