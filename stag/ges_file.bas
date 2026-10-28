Attribute VB_Name = "ges_file"
Option Explicit

Sub cos_cur_tag()
'Arturo uff 05-11-02 modificata la sub per permettere la ricostruzione del db con il dato di grammatura del cartone
Dim i As Integer
Dim j As tip_gra_car 'Arturo uff 05-11-02
Dim for_mac As Integer
Dim gra_car As Integer 'Arturo uff 05-11-02

db.Execute ( _
"create table cur_tag ( LIVELLO1 TEXT(1), LIVELLO2 TEXT(1), LIVELLO3 TEXT(1), FOR_MAC INTEGER2, GRA_CAR INTEGER2, N_MOT_L INTEGER2, N_MOT_AB INTEGER2, " + _
"LUN_TAG0 INTEGER4, VEL_TAG0 INTEGER4, LUN_TAG1 INTEGER4, VEL_TAG1 INTEGER4, LUN_TAG2 INTEGER4, VEL_TAG2 INTEGER4, " + _
"LUN_TAG3 INTEGER4, VEL_TAG3 INTEGER4, LUN_TAG4 INTEGER4, VEL_TAG4 INTEGER4, LUN_TAG5 INTEGER4, VEL_TAG5 INTEGER4, " + _
"LUN_TAG6 INTEGER4, VEL_TAG6 INTEGER4, LUN_TAG7 INTEGER4, VEL_TAG7 INTEGER4, LUN_TAG8 INTEGER4, VEL_TAG8 INTEGER4, " + _
"LUN_TAG9 INTEGER4, VEL_TAG9 INTEGER4, LUN_TAG10 INTEGER4, VEL_TAG10 INTEGER4, LUN_TAG11 INTEGER4, VEL_TAG11 INTEGER4, " + _
"LUN_TAG12 INTEGER4, VEL_TAG12 INTEGER4, LUN_TAG13 INTEGER4, VEL_TAG13 INTEGER4, LUN_TAG14 INTEGER4, VEL_TAG14 INTEGER4)")

For j = tip_gra_car.gm2_1800 To tip_gra_car.gm2_2600
  gra_car = get_gra_car(j)
  For i = 1 To 3
    Select Case i
      Case 1
        for_mac = 1800
      Case 2
        for_mac = 2500
      Case 3
        for_mac = 2800
    End Select
    db.Execute ("insert into cur_tag (LIVELLO1,LIVELLO2,LIVELLO3,FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB) values ('0','0','0'," & for_mac & "," & gra_car & ",1,1)")
    db.Execute ("insert into cur_tag (LIVELLO1,LIVELLO2,LIVELLO3,FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB) values ('0','0','0'," & for_mac & "," & gra_car & ",2,1)")
    db.Execute ("insert into cur_tag (LIVELLO1,LIVELLO2,LIVELLO3,FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB) values ('0','0','0'," & for_mac & "," & gra_car & ",2,2)")
    db.Execute ("insert into cur_tag (LIVELLO1,LIVELLO2,LIVELLO3,FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB) values ('0','0','0'," & for_mac & "," & gra_car & ",3,1)")
    db.Execute ("insert into cur_tag (LIVELLO1,LIVELLO2,LIVELLO3,FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB) values ('0','0','0'," & for_mac & "," & gra_car & ",3,2)")
    db.Execute ("insert into cur_tag (LIVELLO1,LIVELLO2,LIVELLO3,FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB) values ('0','0','0'," & for_mac & "," & gra_car & ",3,3)")
  Next i
Next j

End Sub
Sub cos_pat()
'Iciliouff 20-11-2003
'Crea Pattern.DBF
'Robertouff 26-11-2007
  db.Execute ("create table PATTERN (CODICE TEXT(50), CONTRASTO TEXT(1), PRI_LET_FC TEXT(1)," + _
  "L1 TEXT(4), L2 TEXT(4), L3 TEXT(4), L4 TEXT(4), L5 TEXT(4), L6 TEXT(4), L7 TEXT(4), L8 TEXT(4), L9 TEXT(4), L10 TEXT(4),OFS_CUT TEXT(5))")
End Sub


Function dbf_vuo(name As String) As Boolean
Dim rs As Recordset

Set rs = db.OpenRecordset("select * from " & name, dbOpenSnapshot)
If rs.RecordCount = 0 Then dbf_vuo = True

End Function
Sub leg_dat(v As Variant, s As String, nel As Integer)
' legge nel record dalla query s in una variabile v
Dim ds As Recordset
    Set ds = db.OpenRecordset(s, dbOpenSnapshot)
    If ds.RecordCount >= 1 Then
      v = ds.GetRows(CVar(nel))
    Else
      v = Empty
    End If
    ds.Close
End Sub
Sub cos_lin()
  db.Execute ( _
  "create table lingue (lingua TEXT(20), nomrecdb TEXT(9)," + _
  "attuale INTEGER2)")
db.Execute ("insert into lingue (lingua,nomrecdb,attuale) values ('ITALIANO','ITALIAN',1)")
db.Execute ("insert into lingue (lingua,nomrecdb,attuale) values ('INGLESE','INGLESE',0)")
db.Execute ("insert into lingue (lingua,nomrecdb,attuale) values ('TEDESCO','TEDESCO',0)")
db.Execute ("insert into lingue (lingua,nomrecdb,attuale) values ('SPAGNOLO','SPAGNOLO',0)")
db.Execute ("insert into lingue (lingua,nomrecdb,attuale) values ('OLANDESE','OLANDESE',0)")
db.Execute ("insert into lingue (lingua,nomrecdb,attuale) values ('CECO','CECO',0)")
db.Execute ("insert into lingue (lingua,nomrecdb,attuale) values ('FRANCESE','FRANCESE',0)")
db.Execute ("insert into lingue (lingua,nomrecdb,attuale) values ('PORTOGHESE','PORTOGHESE',0)")
End Sub




Public Sub ins_sto(allarme As String, giorno As Date, ora As Date)
Dim rs As Recordset

Set rs = db.OpenRecordset("sto.dbf", dbOpenDynaset)
rs.AddNew
rs("ALLARME") = allarme
rs("DATA_ALL") = giorno
If Len(CStr(ora)) > 8 Then 'Per risolvere problemi su impostazioni internazionali: nel caso di NT in inglese il dato ora ha anche l'informazione AM e PM
  rs("ORA_ALL") = Left$(CStr(ora), 8)
Else
  rs("ORA_ALL") = CStr(ora)
End If
rs("ORA_ALL") = Left$(CStr(ora), 8)
rs.Update
If rs.RecordCount > NUM_REC_STO Then
      rs.Close
      del_sto ("sto")
Else
      rs.Close
End If

End Sub
Public Sub ins_freq_all(nam_tab As String, des_all As String)
Dim rs As Recordset

Set rs = db.OpenRecordset(nam_tab & ".dbf", dbOpenTable)
rs.Index = nam_tab
rs.Seek "=", Mid$(des_all, 1, 50)
If rs.NoMatch Then
    rs.AddNew
    rs("ALLARME") = Mid$(des_all, 1, 50)
    rs("FREQ") = 1
    rs.Update
Else
    If val(rs("FREQ")) < MAX_FREQ_ALL Then  '21122001 Stagetti
      rs.Edit
      rs("FREQ") = val(rs("FREQ")) + 1
      rs.Update
    Else
      rs.Edit
      rs("FREQ") = 1   '21122001 Stagetti
      rs.Update
    End If
End If
rs.Close
End Sub
Sub del_sto(name As String)
Dim rs As Recordset

Set rs = db.OpenRecordset(name, dbOpenDynaset)
rs.MoveLast
rs.MoveFirst
While rs.RecordCount > NUM_REC_STO

  'rs.Edit 'Arturo uff 07-02-03 (2)
  rs.Delete
  rs.MoveNext
  
Wend
rs.Close

End Sub
Sub cos_sto()
Dim tmp_db As Recordset

  'APalmerini_FI07/01/2005_1
  'db.Execute ("create table sto (ALLARME text(50), DATA_ALL DATETIME, ORA_ALL TEXT(8))")
  If Dir$(pro_set.pro_pat + "MESSAGGI.DBF", vbNormal) <> "" Then
    Set tmp_db = db.OpenRecordset("MESSAGGI.DBF", dbOpenTable)
    'RFAZZI_FI 09-01-08
    If tmp_db.Fields(3).Size > 100 Then
        db.Execute ("create table sto (ALLARME text(" + CStr(tmp_db.Fields(3).Size) + "), DATA_ALL DATETIME, ORA_ALL TEXT(10))")
    Else
        db.Execute ("create table sto (ALLARME text(100), DATA_ALL DATETIME, ORA_ALL TEXT(10))")
    End If
    tmp_db.Close
  Else
    db.Execute ("create table sto (ALLARME text(100), DATA_ALL DATETIME, ORA_ALL TEXT(10))")
  End If
  
End Sub

'APalmerini_FI07/01/2005_1
Sub chk_sto()
'Controllo se la lunghezza del campo ALLARME di STO.DBF è coerente con quella della colonna ITALIANO del
'db MESSAGGI.DBF e la correggo eventualmente.
Dim tmp_db As Recordset
Dim sto_db As Recordset

  Set sto_db = db.OpenRecordset("STO.DBF", dbOpenTable)
  If Dir$(pro_set.pro_pat + "MESSAGGI.DBF", vbNormal) <> "" Then
    Set tmp_db = db.OpenRecordset("MESSAGGI.DBF", dbOpenTable)
    If tmp_db.Fields(3).Size <> sto_db.Fields(0).Size Then
      sto_db.Close
      If Dir$(pro_set.pro_pat + "STO.DBF", vbNormal) <> "" Then Kill pro_set.pro_pat + "STO.DBF"
      cos_sto
    End If
    tmp_db.Close
  Else
    sto_db.Close
  End If
  
End Sub

Sub cos_par_azi()
  
  db.Execute ("create table par_azi (DESCR text(50), COD text(4), SOT_COD text(2))")
  
End Sub

Sub cos_par_cfg()
'Arturo uff 05-11-02 modificata la sub per permettere la ricostruzione del db con il dato di grammatura del cartone
'Arturo uff 21-02-03 modificata la sub per permettere la ricostruzione del db con il dato di massimo numero giri motore
Dim i As Integer
Dim j As tip_gra_car
Dim for_mac As Integer
Dim gra_car As Integer

db.Execute ( _
"create table par_cfg (DESCR TEXT(35), LIVELLO1 TEXT(1), LIVELLO2 TEXT(1), LIVELLO3 TEXT(1), FOR_MAC INTEGER2, GRA_CAR INTEGER2, N_MOT_L INTEGER2, N_MOT_AB INTEGER2, " + _
"VEL_MAX1 INTEGER4,TEM_ATL1 INTEGER4,TEM_DTL1 INTEGER4, TEM_ATC1 INTEGER4, " + _
"TEM_DTC1 INTEGER4, KP_NOR1 INTEGER4, KI_NOR1 INTEGER4 ,FIL_KP1 INTEGER4,KV_NOR1 INTEGER4,LIM_KV1 INTEGER4, MAX_GIR1 INTEGER4, VEL_MAX2 INTEGER4, " + _
"TEM_ATL2 INTEGER4, TEM_DTL2 INTEGER4, TEM_ATC2 INTEGER4, TEM_DTC2 INTEGER4, " + _
"KP_NOR2 INTEGER4, KI_NOR2 INTEGER4,FIL_KP2 INTEGER4,KV_NOR2 INTEGER4,LIM_KV2 INTEGER4, MAX_GIR2 INTEGER4, VEL_MAX3 INTEGER4,TEM_ATL3 INTEGER4, TEM_DTL3 INTEGER4, " + _
"TEM_ATC3 INTEGER4, TEM_DTC3 INTEGER4, KP_NOR3 INTEGER4, KI_NOR3 INTEGER4,FIL_KP3 INTEGER4,KV_NOR3 INTEGER4, LIM_KV3 INTEGER4, MAX_GIR3 INTEGER4)")

For j = tip_gra_car.gm2_1800 To tip_gra_car.gm2_2600
  gra_car = get_gra_car(j)
  For i = 1 To 3
    Select Case i
      Case 1
        for_mac = 1800
      Case 2
        for_mac = 2500
      Case 3
        for_mac = 2800
    End Select
    db.Execute ("insert into par_cfg (DESCR,LIVELLO1,LIVELLO2,LIVELLO3,FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB) values ('DEFAULT','0','0','0'," & for_mac & "," & gra_car & ",1,1)")
    db.Execute ("insert into par_cfg (DESCR,LIVELLO1,LIVELLO2,LIVELLO3,FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB) values ('DEFAULT','0','0','0'," & for_mac & "," & gra_car & ",2,1)")
    db.Execute ("insert into par_cfg (DESCR,LIVELLO1,LIVELLO2,LIVELLO3,FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB) values ('DEFAULT','0','0','0'," & for_mac & "," & gra_car & ",2,2)")
    db.Execute ("insert into par_cfg (DESCR,LIVELLO1,LIVELLO2,LIVELLO3,FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB) values ('DEFAULT','0','0','0'," & for_mac & "," & gra_car & ",3,1)")
    db.Execute ("insert into par_cfg (DESCR,LIVELLO1,LIVELLO2,LIVELLO3,FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB) values ('DEFAULT','0','0','0'," & for_mac & "," & gra_car & ",3,2)")
    db.Execute ("insert into par_cfg (DESCR,LIVELLO1,LIVELLO2,LIVELLO3,FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB) values ('DEFAULT','0','0','0'," & for_mac & "," & gra_car & ",3,3)")
  Next i
Next j
End Sub


Sub cos_ond()
'  db.Execute ("insert into onde (onda,riservato) values ('A',1)")
    db.Execute ( _
  "create table onde (ONDA TEXT(3), CARTA TEXT(36), SPESSORE INTEGER2, GRAMMATURA INTEGER2, " + _
  "VELDIPROD INTEGER2, VELDICAMBI INTEGER2, CODONDA INTEGER2, RISERVATO INTEGER2)")
  db.Execute ("insert into onde (onda,carta,riservato) values ('A','ONDA A',1)")

End Sub

Sub cos_freq_all(nam_tab As String)

db.Execute ("create table " & nam_tab & " (ALLARME TEXT(50), FREQ INTEGER4)")
db.Execute "create UNIQUE index " & nam_tab & " on " & nam_tab & "(ALLARME) with primary"

End Sub

Sub cos_all(nam_tab As String, rig As Integer, col As Integer)
  Dim r As Integer, c As Integer
  Dim tb As Table
  Dim s As String
  db.Execute ("create table " & nam_tab & " (DM TEXT(4),LIBERO1 TEXT(10))")
  Set tb = db.OpenTable(nam_tab)
  
  For r = 1 To rig
    For c = 1 To col
      tb.AddNew
      tb("dm") = Format$((r - 1)) + Format$(c - 1, "00")
      tb.Update
    Next c
  Next r
  For c = 1 To NUM_AZI
    tb.AddNew
    tb("dm") = "L" + Format$(c, "00")
    tb.Update
  Next c
  tb.Close
End Sub
Sub cos_ord()
  db.Execute ( _
  "create table Ser_ord (DATO TEXT(15),PRES INTEGER," + _
  "LUNGH TEXT(5),XCOOR INTEGER,YCOOR INTEGER,NER TEXT(5)," + _
  "DOPPLUN TEXT(5),DRAFT TEXT(5),COMPR TEXT(1))")
End Sub


Sub chk_fil()
' routine di inizializzazione e controllo file
    If Dir$(pro_set.pro_pat + "all_mac.dbf") = "" Then
      cos_all "all_mac", rig:=RIG_ALL, col:=COL_ALL
    End If
    If Dir$(pro_set.pro_pat + "f_a_mac.dbf") = "" Then
      cos_freq_all "f_a_mac"
    Else
      ric_ind "f_a_mac" 'Arturo uff 08-02-03 (1)
    End If
    If Dir$(pro_set.pro_pat + "all_qe1.dbf") = "" Then
      cos_all "all_qe1", rig:=RIG_ALL, col:=COL_ALL
    End If
    If Dir$(pro_set.pro_pat + "f_a_qe1.dbf") = "" Then
      cos_freq_all "f_a_qe1"
    Else
      ric_ind "f_a_qe1" 'Arturo uff 08-02-03 (1)
    End If
    If Dir$(pro_set.pro_pat + "all_qe2.dbf") = "" Then
      cos_all "all_qe2", rig:=RIG_ALL, col:=COL_ALL
    End If
    If Dir$(pro_set.pro_pat + "f_a_qe2.dbf") = "" Then
      cos_freq_all "f_a_qe2"
    Else
      ric_ind "f_a_qe2" 'Arturo uff 08-02-03 (1)
    End If
    If Dir$(pro_set.pro_pat + "all_qe3.dbf") = "" Then
      cos_all "all_qe3", rig:=RIG_ALL, col:=COL_ALL
    End If
    If Dir$(pro_set.pro_pat + "f_a_qe3.dbf") = "" Then
      cos_freq_all "f_a_qe3"
    Else
      ric_ind "f_a_qe3" 'Arturo uff 08-02-03 (1)
    End If
    If Dir$(pro_set.pro_pat + "all_con.dbf") = "" Then
      cos_all "all_con", rig:=RIG_ALL, col:=COL_ALL
    End If
    If Dir$(pro_set.pro_pat + "f_a_con.dbf") = "" Then
      cos_freq_all "f_a_con"
    Else
      ric_ind "f_a_con" 'Arturo uff 08-02-03 (1)
    End If
    If Dir$(pro_set.pro_pat + "all_har.dbf") = "" Then
      cos_all "all_har", rig:=RIG_ALL, col:=COL_ALL
    End If
    If Dir$(pro_set.pro_pat + "f_a_har.dbf") = "" Then
      cos_freq_all "f_a_har"
    Else
      ric_ind "f_a_har" 'Arturo uff 08-02-03 (1)
    End If
    If Dir$(pro_set.pro_pat + "all_war.dbf") = "" Then
      cos_all "all_war", rig:=RIG_ALL, col:=COL_ALL
    End If
    If Dir$(pro_set.pro_pat + "f_a_war.dbf") = "" Then
      cos_freq_all "f_a_war"
    Else
      ric_ind "f_a_war" 'Arturo uff 08-02-03 (1)
    End If
    
    If Dir$(pro_set.pro_pat + "sto.dbf") = "" Then
      cos_sto
    Else
      chk_sto  'APalmerini_FI07/01/2005_1
    End If
    If Dir$(pro_set.pro_pat + "onde.dbf") = "" Then
      cos_ond
    End If
    If Dir$(pro_set.pro_pat + "lingue.dbf") = "" Then
      cos_lin
    End If
    If Dir$(pro_set.pro_pat + "par_cfg.dbf") = "" Then
      cos_par_cfg
    End If
    If Dir$(pro_set.pro_pat + "par_azi.dbf") = "" Then
      cos_par_azi
    End If
    If Dir$(pro_set.pro_pat + "cur_tag.dbf") = "" Then
      cos_cur_tag
    End If
    'Iciliouff 20-11-2003
    If Dir$(pro_set.pro_pat + "PATTERN.DBF") = "" Then
      cos_pat
    End If
    
End Sub



Sub ric_ind(nam_tab As String)
'Arturo uff 08-02-03 (1) ricreo i file di indice ogni volta che carico stag per evitare eventuali corruzioni dei file ndx e inf
   
  If Dir$(pro_set.pro_pat & nam_tab & ".ndx") <> "" Then Kill pro_set.pro_pat & nam_tab & ".ndx"
  If Dir$(pro_set.pro_pat & nam_tab & ".inf") <> "" Then Kill pro_set.pro_pat & nam_tab & ".inf"
  db.Execute "create UNIQUE index " & nam_tab & " on " & nam_tab & "(ALLARME) with primary"

End Sub


