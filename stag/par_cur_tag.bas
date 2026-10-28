Attribute VB_Name = "par_cur_tag"
Option Explicit

'Arturo uff 05-11-02
'Function carica_new_par_cur_tag(liv As Integer, for_mac As Integer, num_mot_liv As Integer, num_mot_abi As Integer) As Boolean
Function carica_new_par_cur_tag(liv As Integer, for_mac As Integer, gra_car As tip_gra_car, num_mot_liv As Integer, num_mot_abi As Integer) As Boolean
Dim m As Integer
Dim ds As Recordset

Select Case for_mac + 1
  Case mm1800
    for_mac = 1800
  Case mm2500
    for_mac = 2500
  Case mm2800
    for_mac = 2800
End Select
gra_car = get_gra_car(gra_car) 'Arturo uff 05-11-02
If Dir$(pro_set.pro_pat + "cur_tag.inf") <> "" Then
    Kill pro_set.pro_pat + "cur_tag.inf"
End If
If Dir$(pro_set.pro_pat + "t_i_cur.ndx") <> "" Then
    Kill pro_set.pro_pat + "t_i_cur.ndx"
End If
'Arturo uff 05-11-02
'db.Execute "create index t_i_cur on cur_tag (FOR_MAC,N_MOT_L,N_MOT_AB)"
db.Execute "create index t_i_cur on cur_tag (FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB)"
Set ds = db.OpenRecordset("cur_tag", dbOpenTable)
If ds.RecordCount > 0 Then
  'Lettura parametri relativi alla configurazione della macchina
  ds.Index = "t_i_cur"
'Arturo uff 05-11-02
'  ds.Seek "=", for_mac, num_mot_liv, num_mot_abi
  ds.Seek "=", for_mac, gra_car, num_mot_liv, num_mot_abi
  If Not ds.NoMatch Then 'configurazione presente
      rie_val_cur_tag liv, ds
      res_cur_tag liv
      ds.Edit
      ds("LIVELLO" & liv) = 1  'setto come attivo per questo livello
      ds.Update
'Arturo uff 05-11-02
'      cur_tag(liv).descr = CStr(for_mac) & CStr(gra_car) & CStr(num_mot_liv) & CStr(num_mot_abi)
      cur_tag(liv).descr = CStr(for_mac) & CStr(gra_car) & CStr(num_mot_liv) & CStr(num_mot_abi)
      carica_new_par_cur_tag = True
  Else
'Arturo uff 05-11-02
'      log_str "Sub carica_new_par_cur_tag del Modulo par_cur_tag: non esiste alcun record con formato, num. motori e num. motori abilitati = " + CStr(for_mac) + " " + CStr(num_mot_liv) + " " + CStr(num_mot_abi) + " per il livello" + liv, MED_PRIORITY, log_cur
      'Iciliouff 03-11-2003 Corretto liv con cstr(liv)
      log_str "Sub carica_new_par_cur_tag del Modulo par_cur_tag: non esiste alcun record con formato, num. motori e num. motori abilitati = " + CStr(for_mac) + " " + CStr(gra_car) + " " + CStr(num_mot_liv) + " " + CStr(num_mot_abi) + " per il livello" + CStr(liv), MED_PRIORITY, log_cur
      carica_new_par_cur_tag = False
  End If
Else
  log_str "Sub carica_new_par_cur_tag del Modulo par_cur_tag: il file cur_tag.dbf è vuoto", MED_PRIORITY, log_cur
  carica_new_par_cur_tag = False
End If
ds.Close
If Dir$(pro_set.pro_pat + "cur_tag.inf") <> "" Then
    Kill pro_set.pro_pat + "cur_tag.inf"
End If
If Dir$(pro_set.pro_pat + "t_i_cur.ndx") <> "" Then
    Kill pro_set.pro_pat + "t_i_cur.ndx"
End If
End Function

Sub res_cur_tag(liv As Integer)
'Trova la precedente configurazione attiva per il livello liv e resetta il campo LIVELLO a 0
Dim ds As Recordset

Set ds = db.OpenRecordset("cur_tag", dbOpenTable)
ds.MoveFirst
Do While Not ds.EOF
  If ds("LIVELLO" & liv) = 1 Then
      ds.Edit
      ds("LIVELLO" & liv) = 0
      ds.Update
  End If
  ds.MoveNext
Loop
ds.Close

End Sub

Sub leggi_par_cur_tag()
Dim ds As Recordset
Dim i As Integer
Dim m As Integer
Dim l As Integer
Dim for_mac As Integer
Dim gra_car As Integer 'Arturo uff 05-11-02
Dim num_mot_liv As Integer

If Dir$(pro_set.pro_pat + "cur_tag.inf") <> "" Then
    Kill pro_set.pro_pat + "cur_tag.inf"
End If
If Dir$(pro_set.pro_pat + "t_i_cur.ndx") <> "" Then
    Kill pro_set.pro_pat + "t_i_cur.ndx"
End If
Select Case par_mac.car_tec.for_mac + 1
  Case mm1800
    for_mac = 1800
  Case mm2500
    for_mac = 2500
  Case mm2800
    for_mac = 2800
End Select
gra_car = get_gra_car(par_mac.car_tec.gra_car) 'Arturo uff 05-11-02
num_mot_liv = val(par_mac.car_tec.num_mot_liv_tag)
For i = 1 To par_mac.car_tec.num_liv_tag
'Arturo uff 05-11-02
'    db.Execute "create index t_i_cur on cur_tag(FOR_MAC,N_MOT_L,N_MOT_AB)"
    db.Execute "create index t_i_cur on cur_tag(FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB)"
    Set ds = db.OpenRecordset("cur_tag", dbOpenTable)
    If ds.RecordCount > 0 Then
        'Lettura parametri relativi alla configurazione della macchina
        ds.Index = "t_i_cur"
        res_cur_tag i
'Arturo uff 05-11-02
'        ds.Seek "=", for_mac, num_mot_liv, par_abi.num_mot_abi(i)
        ds.Seek "=", for_mac, gra_car, num_mot_liv, par_abi.num_mot_abi(i)
        If Not ds.NoMatch Then 'configurazione presente
          rie_val_cur_tag i, ds
          ds.Edit
          ds("LIVELLO" & i) = 1
          ds.Update
'Arturo uff 05-11-02
'          cur_tag(i).descr = CStr(for_mac) & CStr(num_mot_liv) & CStr(par_abi.num_mot_abi(i))
          cur_tag(i).descr = CStr(for_mac) & CStr(gra_car) & CStr(num_mot_liv) & CStr(par_abi.num_mot_abi(i))
        Else
'Arturo uff 05-11-02
'          log_str "Sub leggi_par_cur_tag del Modulo par_cur_tag: non è presente alcun record con formato, num. motori e num. motori abilitati = " + CStr(for_mac) + " " + CStr(num_mot_liv) + " " + CStr(par_abi.num_mot_abi(i)), MED_PRIORITY, log_cur
          log_str "Sub leggi_par_cur_tag del Modulo par_cur_tag: non è presente alcun record con formato, num. motori e num. motori abilitati = " + CStr(for_mac) + " " + CStr(gra_car) + " " + CStr(num_mot_liv) + " " + CStr(par_abi.num_mot_abi(i)), MED_PRIORITY, log_cur
          For m = 0 To 14
            cur_tag(i).Lun_tag(m + 1) = 0
            cur_tag(i).vel_max(m + 1) = 0
          Next m
          cur_tag(i).descr = ""
        End If
    Else ' recordcount = 0
        log_str "Sub leggi_par_cur_tag del Modulo par_cur_tag: il file cur_tag.dbf è vuoto", MED_PRIORITY, log_cur
        For l = 1 To 3
          For m = 0 To 14
            cur_tag(l).Lun_tag(m + 1) = 0
            cur_tag(l).vel_max(m + 1) = 0
          Next m
          cur_tag(l).descr = ""
        Next l
        ds.Close
        If Dir$(pro_set.pro_pat + "cur_tag.inf") <> "" Then
            Kill pro_set.pro_pat + "cur_tag.inf"
        End If
        If Dir$(pro_set.pro_pat + "t_i_cur.ndx") <> "" Then
            Kill pro_set.pro_pat + "t_i_cur.ndx"
        End If
        Exit Sub
    End If
    ds.Close
    If Dir$(pro_set.pro_pat + "cur_tag.inf") <> "" Then
        Kill pro_set.pro_pat + "cur_tag.inf"
    End If
    If Dir$(pro_set.pro_pat + "t_i_cur.ndx") <> "" Then
        Kill pro_set.pro_pat + "t_i_cur.ndx"
    End If
Next i

End Sub

Sub rie_val_cur_tag(liv As Integer, rs As Recordset)
Dim m As Integer

For m = 0 To 14
  cur_tag(liv).Lun_tag(m + 1) = val(IIf(IsNull(rs("LUN_TAG" & m)), 0, rs("LUN_TAG" & m)))
  cur_tag(liv).vel_max(m + 1) = val(IIf(IsNull(rs("VEL_TAG" & m)), 0, rs("VEL_TAG" & m)))
Next m

End Sub


Sub scr_par_cur_tag_dbf(liv As Integer)
Dim for_mac As Integer
Dim gra_car As Integer 'Arturo uff 05-11-02
Dim i As Integer
Dim num_mot_liv As Integer
Dim num_mot_abi As Integer
Dim ds As Recordset

Select Case par_mac.car_tec.for_mac + 1
  Case mm1800
    for_mac = 1800
  Case mm2500
    for_mac = 2500
  Case mm2800
    for_mac = 2800
End Select
gra_car = get_gra_car(par_mac.car_tec.gra_car) 'Arturo uff 05-11-02
num_mot_liv = val(par_mac.car_tec.num_mot_liv_tag)
num_mot_abi = par_abi.num_mot_abi(liv)
If Dir$(pro_set.pro_pat + "cur_tag.inf") <> "" Then
    Kill pro_set.pro_pat + "cur_tag.inf"
End If
If Dir$(pro_set.pro_pat + "t_i_cur.ndx") <> "" Then
    Kill pro_set.pro_pat + "t_i_cur.ndx"
End If

'Arturo uff 05-11-02
'db.Execute "create index t_i_cur on cur_tag(LIVELLO" & liv & ",FOR_MAC,N_MOT_L,N_MOT_AB)"
db.Execute "create index t_i_cur on cur_tag(LIVELLO" & liv & ",FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB)"
Set ds = db.OpenRecordset("cur_tag", dbOpenTable)
If ds.RecordCount > 0 Then
  ds.Index = "t_i_cur"
'Arturo uff 05-11-02
'  ds.Seek "=", "1", for_mac, num_mot_liv, num_mot_abi
  ds.Seek "=", "1", for_mac, gra_car, num_mot_liv, num_mot_abi
  If Not ds.NoMatch Then 'configurazione presente
    scr_val_cur_tag liv, ds
  Else
    log_str "Sub scr_par_cur_tag_dbf del Modulo par_cur_tag: non era settato alcun record per il livello " + CStr(liv), MED_PRIORITY, log_cur
'Arturo uff 05-11-02
'    ds.Seek "=", "0", for_mac, num_mot_liv, num_mot_abi
    ds.Seek "=", "0", for_mac, gra_car, num_mot_liv, num_mot_abi
    If Not ds.NoMatch Then 'configurazione non settata presente => la setto
      ds.Edit
      ds("LIVELLO" & i) = 1
      ds.Update
      scr_val_cur_tag liv, ds
    Else
'Arturo uff 05-11-02
'      log_str "Sub scr_par_cur_tag_dbf del Modulo par_cur_tag: non è presente alcun record con formato, num. motori e num. motori abilitati = " + CStr(for_mac) + " " + CStr(num_mot_liv) + " " + CStr(num_mot_abi), MED_PRIORITY, log_cur
      log_str "Sub scr_par_cur_tag_dbf del Modulo par_cur_tag: non è presente alcun record con formato, num. motori e num. motori abilitati = " + CStr(for_mac) + " " + CStr(gra_car) + " " + CStr(num_mot_liv) + " " + CStr(num_mot_abi), MED_PRIORITY, log_cur
    End If
  End If
Else
  log_str "Sub scr_par_cur_tag_dbf del Modulo par_cur_tag. Il file risulta vuoto", MED_PRIORITY, log_cur
End If
ds.Close
If Dir$(pro_set.pro_pat + "cur_tag.inf") <> "" Then
    Kill pro_set.pro_pat + "cur_tag.inf"
End If
If Dir$(pro_set.pro_pat + "t_i_cur.ndx") <> "" Then
    Kill pro_set.pro_pat + "t_i_cur.ndx"
End If
End Sub


Sub scr_val_cur_tag(liv As Integer, rs As Recordset)
Dim m As Integer

rs.Edit
For m = 0 To 14
  rs("LUN_TAG" & m) = cur_tag(liv).Lun_tag(m + 1)
  rs("VEL_TAG" & m) = cur_tag(liv).vel_max(m + 1)
Next m
rs.Update

End Sub


