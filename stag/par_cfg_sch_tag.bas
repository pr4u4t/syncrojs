Attribute VB_Name = "par_cfg_sch_tag"
Option Explicit

'Arturo uff 05-11-02 aggiunto parametro gra_car
'Function cancella_cfg(descr As String, for_mac As Integer, num_mot_liv As Integer, num_mot_abi As Integer) As cod_ret
Function cancella_cfg(descr As String, for_mac As Integer, gra_car As tip_gra_car, num_mot_liv As Integer, num_mot_abi As Integer) As cod_ret
'Elimina una configurazione non attiva
Dim ds As Recordset
Dim i As Integer
Select Case for_mac + 1
  Case mm1800
    for_mac = 1800
  Case mm2500
    for_mac = 2500
  Case mm2800
    for_mac = 2800
End Select
gra_car = get_gra_car(gra_car)
cancella_cfg = RET_BAD
Set ds = db.OpenRecordset("par_cfg", dbOpenTable)
If ds.RecordCount <> 0 Then
    ds.MoveFirst
    While Not ds.EOF
      If ds("DESCR") = Trim$(descr) And ds("FOR_MAC") = for_mac And ds("GRA_CAR") = gra_car And ds("N_MOT_L") = num_mot_liv And ds("N_MOT_AB") = num_mot_abi Then
          For i = 1 To par_mac.car_tec.num_liv_tag
            If ds("LIVELLO" & i) = 1 Then Exit Function
          Next i
          ds.Delete
          cancella_cfg = RET_OK
          Exit Function
      End If
      ds.MoveNext
    Wend
End If
    
ds.Close
End Function


Function tro_cfg(nam_cfg As String, liv As Integer) As Boolean
Dim ds As Recordset
Dim for_mac As Integer
Dim gra_car As Integer  'Arturo uff 02-11-02
Dim num_mot_liv As Integer
Dim num_mot_abi As Integer

tro_cfg = True
Set ds = db.OpenRecordset("par_cfg", dbOpenTable)
Select Case par_mac.car_tec.for_mac + 1
  Case mm1800
    for_mac = 1800
  Case mm2500
    for_mac = 2500
  Case mm2800
    for_mac = 2800
End Select
gra_car = get_gra_car(par_mac.car_tec.gra_car)  'Arturo uff 02-11-02
num_mot_liv = val(par_mac.car_tec.num_mot_liv_tag)
num_mot_abi = par_abi.num_mot_abi(liv)
If ds.RecordCount > 0 Then 'se il file non è vuoto verifico se la configurazione esiste già
    ds.MoveFirst
    Do While Not ds.EOF
      'Arturo uff 02-11-02
      If ds("DESCR") = Trim$(nam_cfg) And ds("FOR_MAC") = for_mac And ds("GRA_CAR") = gra_car And ds("N_MOT_L") = num_mot_liv And ds("N_MOT_AB") = num_mot_abi Then
          Exit Function
      End If
      ds.MoveNext
    Loop
End If
tro_cfg = False
End Function

Sub res_cfg_att(liv As Integer)
'Trova la precedente configurazione attiva per il livello liv e resetta il campo LIVELLO a 0
Dim ds As Recordset

Set ds = db.OpenRecordset("par_cfg", dbOpenTable)
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



'Arturo uff 05-11-02 inserito controllo anche su grammatura max cartone
'Sub set_cfg_att(descr As String, for_mac As Integer, num_mot_liv As Integer, num_mot_abi As Integer, liv As Integer)
Sub set_cfg_att(descr As String, for_mac As Integer, gra_car As tip_gra_car, num_mot_liv As Integer, num_mot_abi As Integer, liv As Integer)
'Trova la  configurazione descr per il livello liv e setta il campo LIVELLO a 1
Dim ds As Recordset

Select Case for_mac + 1
  Case mm1800
    for_mac = 1800
  Case mm2500
    for_mac = 2500
  Case mm2800
    for_mac = 2800
End Select
gra_car = get_gra_car(gra_car)  'Arturo uff 05-11-02
Set ds = db.OpenRecordset("par_cfg", dbOpenTable)
ds.MoveFirst
Do While Not ds.EOF
  'Arturo uff 05-11-02
  If ds("DESCR") = Trim$(descr) And ds("FOR_MAC") = for_mac And ds("GRA_CAR") = gra_car _
  And ds("N_MOT_L") = num_mot_liv And ds("N_MOT_AB") = num_mot_abi Then
        ds.Edit
        ds("LIVELLO" & liv) = 1
        ds.Update
  End If
  ds.MoveNext
Loop
ds.Close
End Sub
Sub salva_cfg_def(nam_cfg As String, liv As Integer)
Dim ds As Recordset
Dim for_mac As Integer
Dim gra_car As Integer 'Arturo uff 05-11-02
Dim num_mot_liv As Integer
Dim num_mot_abi As Integer
Dim i As Integer
Dim cfg_tro As Boolean

Set ds = db.OpenRecordset("par_cfg", dbOpenTable)
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
If ds.RecordCount <> 0 Then 'se il file non è vuoto verifico se la configurazione esiste già
    ds.MoveFirst
    cfg_tro = False
    Do While Not ds.EOF
'Arturo uff 05-11-02
'      If ds("DESCR") = Trim$(nam_cfg) And ds("FOR_MAC") = for_mac And ds("N_MOT_L") = num_mot_liv And ds("N_MOT_AB") = num_mot_abi Then
      If ds("DESCR") = Trim$(nam_cfg) And ds("FOR_MAC") = for_mac And ds("GRA_CAR") = gra_car And ds("N_MOT_L") = num_mot_liv And ds("N_MOT_AB") = num_mot_abi Then
          cfg_tro = True
          Exit Do
      End If
      ds.MoveNext
    Loop
    If cfg_tro Then 'configurazione presente
        ds.Edit
        ds("LIVELLO" & liv) = 1
        If IsNull(ds("LIVELLO" & IIf((liv + 1) Mod 3 = 0, 3, (liv + 1) Mod 3))) Then ds("LIVELLO" & IIf((liv + 1) Mod 3 = 0, 3, (liv + 1) Mod 3)) = 0
        If IsNull(ds("LIVELLO" & IIf((liv + 2) Mod 3 = 0, 3, (liv + 2) Mod 3))) Then ds("LIVELLO" & IIf((liv + 2) Mod 3 = 0, 3, (liv + 2) Mod 3)) = 0
        ds("VEL_MAX" & liv) = val(par_mac_ute.par_cfg_att(liv).vel_max_tag)
        ds("TEM_ATL" & liv) = val(par_mac_ute.par_cfg_att(liv).tem_acc_tgl_lun)
        ds("TEM_DTL" & liv) = val(par_mac_ute.par_cfg_att(liv).tem_dec_tgl_lun)
        ds("TEM_ATC" & liv) = val(par_mac_ute.par_cfg_att(liv).tem_acc_tgl_cor)
        ds("TEM_DTC" & liv) = val(par_mac_ute.par_cfg_att(liv).tem_dec_tgl_cor)
        ds("KP_NOR" & liv) = val(par_mac_ute.par_cfg_att(liv).Kp_nor)
        ds("KI_NOR" & liv) = val(par_mac_ute.par_cfg_att(liv).Ki_nor)
        ds("FIL_KP" & liv) = val(par_mac_ute.par_cfg_att(liv).fil_kp)
        ds("KV_NOR" & liv) = val(par_mac_ute.par_cfg_att(liv).Kv_nor)
        ds("LIM_KV" & liv) = val(par_mac_ute.par_cfg_att(liv).lim_lav_Kv)
        ds("MAX_GIR" & liv) = val(par_mac_ute.par_cfg_att(liv).gir_max_mot_lam)   'Arturo uff 21-02-03
        ds.Update      'salvo i dati appena scritti
        par_mac_ute.config_sch_tag_att(liv).num_mot_liv = num_mot_liv
        par_mac_ute.config_sch_tag_att(liv).descr = Trim$(nam_cfg)
        setFileINIString "sch_tag_liv_" & liv, "config_att", Trim$(par_mac_ute.config_sch_tag_att(liv).descr), app.EXEName
        If nam_cfg = "DEFAULT" Then
        'sto inserendo per la prima volta una configurazione di DEFAULT come attiva => devo inserirla anche come DEFAULT
            par_mac_ute.config_sch_tag_def(liv).descr = Trim$(nam_cfg)
            setFileINIString "sch_tag_liv_" & liv, "config_def", Trim(par_mac_ute.config_sch_tag_def(liv).descr), app.EXEName
        End If
        If ds.RecordCount > 1 And Trim$(par_mac.config_sch_tag_att(liv).descr) <> "" Then   'metto il campo LIVELLO a 0 per la precedente configurazione attiva
            res_cfg_att liv
        End If
        ds.Close
        Exit Sub
    End If
End If
If Not cfg_tro Then
    ds.AddNew
    ds("DESCR") = nam_cfg
    ds("LIVELLO" & liv) = 1
    If IsNull(ds("LIVELLO" & IIf((liv + 1) Mod 3 = 0, 3, (liv + 1) Mod 3))) Then ds("LIVELLO" & IIf((liv + 1) Mod 3 = 0, 3, (liv + 1) Mod 3)) = 0
    If IsNull(ds("LIVELLO" & IIf((liv + 2) Mod 3 = 0, 3, (liv + 2) Mod 3))) Then ds("LIVELLO" & IIf((liv + 2) Mod 3 = 0, 3, (liv + 2) Mod 3)) = 0
    ds("FOR_MAC") = for_mac
    ds("GRA_CAR") = gra_car 'Arturo uff 05-11-02
    ds("N_MOT_L") = num_mot_liv
    ds("N_MOT_AB") = num_mot_abi
    ds("VEL_MAX" & liv) = val(par_mac_ute.par_cfg_att(liv).vel_max_tag)
    ds("TEM_ATL" & liv) = val(par_mac_ute.par_cfg_att(liv).tem_acc_tgl_lun)
    ds("TEM_DTL" & liv) = val(par_mac_ute.par_cfg_att(liv).tem_dec_tgl_lun)
    ds("TEM_ATC" & liv) = val(par_mac_ute.par_cfg_att(liv).tem_acc_tgl_cor)
    ds("TEM_DTC" & liv) = val(par_mac_ute.par_cfg_att(liv).tem_dec_tgl_cor)
    ds("KP_NOR" & liv) = val(par_mac_ute.par_cfg_att(liv).Kp_nor)
    ds("KI_NOR" & liv) = val(par_mac_ute.par_cfg_att(liv).Ki_nor)
    ds("FIL_KP" & liv) = val(par_mac_ute.par_cfg_att(liv).fil_kp)
    ds("KV_NOR" & liv) = val(par_mac_ute.par_cfg_att(liv).Kv_nor)
    ds("LIM_KV" & liv) = val(par_mac_ute.par_cfg_att(liv).lim_lav_Kv)
    ds("MAX_GIR" & liv) = val(par_mac_ute.par_cfg_att(liv).gir_max_mot_lam)  'Arturo uff 21-02-03
    ds.Update      'salvo i dati appena scritti
    par_mac_ute.config_sch_tag_att(liv).descr = Trim$(nam_cfg)
    par_mac_ute.config_sch_tag_att(liv).num_mot_liv = num_mot_liv
    setFileINIString "sch_tag_liv_" & liv, "config_att", Trim$(par_mac_ute.config_sch_tag_att(liv).descr), app.EXEName
    If nam_cfg = "DEFAULT" Then
    'sto inserendo per la prima volta una configurazione di DEFAULT come attiva => devo inserirla anche come DEFAULT
        par_mac_ute.config_sch_tag_def(liv).descr = Trim$(nam_cfg)
        setFileINIString "sch_tag_liv_" & liv, "config_def", Trim(par_mac_ute.config_sch_tag_def(liv).descr), app.EXEName
    End If
    If ds.RecordCount > 1 And Trim$(par_mac.config_sch_tag_att(liv).descr) <> "" Then  'metto il campo LIVELLO a 0 per la precedente configurazione attiva
        res_cfg_att liv
    End If
    ds.Close
End If
End Sub
Sub salva_cfg_att(nam_cfg As String, liv As Integer)
Dim ds As Recordset
Dim for_mac As Integer
Dim gra_car As Integer  'Arturo uff 05-11-02
Dim num_mot_liv As Integer
Dim num_mot_abi As Integer
Dim i As Integer

Set ds = db.OpenRecordset("par_cfg", dbOpenTable)
Select Case par_mac.car_tec.for_mac + 1
  Case mm1800
    for_mac = 1800
  Case mm2500
    for_mac = 2500
  Case mm2800
    for_mac = 2800
End Select
gra_car = get_gra_car(par_mac.car_tec.gra_car)  'Arturo uff 05-11-02
num_mot_liv = val(par_mac.car_tec.num_mot_liv_tag)
num_mot_abi = par_abi.num_mot_abi(liv)
ds.AddNew
ds("DESCR") = Trim$(nam_cfg)
ds("LIVELLO" & liv) = 1
If IsNull(ds("LIVELLO" & IIf((liv + 1) Mod 3 = 0, 3, (liv + 1) Mod 3))) Then ds("LIVELLO" & IIf((liv + 1) Mod 3 = 0, 3, (liv + 1) Mod 3)) = 0
If IsNull(ds("LIVELLO" & IIf((liv + 2) Mod 3 = 0, 3, (liv + 2) Mod 3))) Then ds("LIVELLO" & IIf((liv + 2) Mod 3 = 0, 3, (liv + 2) Mod 3)) = 0
ds("FOR_MAC") = for_mac
ds("GRA_CAR") = gra_car  'Arturo uff 05-11-02
ds("N_MOT_L") = num_mot_liv
ds("N_MOT_AB") = num_mot_abi
ds("VEL_MAX" & liv) = val(par_mac_ute.par_cfg_att(liv).vel_max_tag)
ds("TEM_ATL" & liv) = val(par_mac_ute.par_cfg_att(liv).tem_acc_tgl_lun)
ds("TEM_DTL" & liv) = val(par_mac_ute.par_cfg_att(liv).tem_dec_tgl_lun)
ds("TEM_ATC" & liv) = val(par_mac_ute.par_cfg_att(liv).tem_acc_tgl_cor)
ds("TEM_DTC" & liv) = val(par_mac_ute.par_cfg_att(liv).tem_dec_tgl_cor)
ds("KP_NOR" & liv) = val(par_mac_ute.par_cfg_att(liv).Kp_nor)
ds("KI_NOR" & liv) = val(par_mac_ute.par_cfg_att(liv).Ki_nor)
ds("FIL_KP" & liv) = val(par_mac_ute.par_cfg_att(liv).fil_kp)
ds("KV_NOR" & liv) = val(par_mac_ute.par_cfg_att(liv).Kv_nor)
ds("LIM_KV" & liv) = val(par_mac_ute.par_cfg_att(liv).lim_lav_Kv)
ds("MAX_GIR" & liv) = val(par_mac_ute.par_cfg_att(liv).gir_max_mot_lam)  'Arturo uff 21-02-03
ds.Update      'salvo i dati appena scritti
par_mac_ute.config_sch_tag_att(liv).descr = Trim$(nam_cfg)
par_mac_ute.config_sch_tag_att(liv).num_mot_liv = num_mot_liv
setFileINIString "sch_tag_liv_" & liv, "config_att", Trim$(par_mac_ute.config_sch_tag_att(liv).descr), app.EXEName
If ds.RecordCount > 1 And Trim$(par_mac.config_sch_tag_att(liv).descr) <> "" Then   'metto il campo LIVELLO a 0 per la precedente configurazione attiva
    res_cfg_att liv
End If
ds.Close
End Sub


Sub modifica_cfg(descr As String, liv As Integer)

'cerca la configurazione descr, for_mac, num_mot_liv, num_mot_abi nel dbf e
'se esiste modifica i valori dei parametri per questa configurazione

Dim ds As Recordset
Dim i As Integer
Dim for_mac As Integer
Dim gra_car As Integer 'Arturo uff 05-11-02
Dim num_mot_liv As Integer
Dim num_mot_abi As Integer
Dim cfg_tro As Boolean

Set ds = db.OpenRecordset("par_cfg", dbOpenTable)

If ds.RecordCount <> 0 Then
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
    ds.MoveFirst
    cfg_tro = False
    Do While Not ds.EOF
'Arturo uff 05-11-02
'      If ds("DESCR") = Trim$(descr) And ds("FOR_MAC") = for_mac And ds("N_MOT_L") = num_mot_liv _'
      If ds("DESCR") = Trim$(descr) And ds("FOR_MAC") = for_mac And ds("GRA_CAR") = gra_car And ds("N_MOT_L") = num_mot_liv _
      And ds("N_MOT_AB") = num_mot_abi And ds("LIVELLO" & liv) = 1 Then
          cfg_tro = True
          Exit Do
      End If
      ds.MoveNext
    Loop
    If Not cfg_tro Then 'configurazione non presente
        ds.Close
        Exit Sub
    Else 'configurazione presente
      ds.Edit
      ds("VEL_MAX" & liv) = val(par_mac_ute.par_cfg_att(liv).vel_max_tag)
      ds("TEM_ATL" & liv) = val(par_mac_ute.par_cfg_att(liv).tem_acc_tgl_lun)
      ds("TEM_DTL" & liv) = val(par_mac_ute.par_cfg_att(liv).tem_dec_tgl_lun)
      ds("TEM_ATC" & liv) = val(par_mac_ute.par_cfg_att(liv).tem_acc_tgl_cor)
      ds("TEM_DTC" & liv) = val(par_mac_ute.par_cfg_att(liv).tem_dec_tgl_cor)
      ds("KP_NOR" & liv) = val(par_mac_ute.par_cfg_att(liv).Kp_nor)
      ds("KI_NOR" & liv) = val(par_mac_ute.par_cfg_att(liv).Ki_nor)
      ds("FIL_KP" & liv) = val(par_mac_ute.par_cfg_att(liv).fil_kp)
      ds("KV_NOR" & liv) = val(par_mac_ute.par_cfg_att(liv).Kv_nor)
      ds("LIM_KV" & liv) = val(par_mac_ute.par_cfg_att(liv).lim_lav_Kv)
      ds("MAX_GIR" & liv) = val(par_mac_ute.par_cfg_att(liv).gir_max_mot_lam)   'Arturo uff 21-02-03
      ds.Update
    End If

End If

ds.Close
End Sub
Sub leggi_cfg()
Dim ds As Recordset
Dim i As Integer
Dim set_att(1 To 3) As Boolean
Dim descr As String
Dim for_mac As Integer
Dim gra_car As Integer 'Arturo uff 05-11-02
Dim num_mot_liv As Integer

If Dir$(pro_set.pro_pat + "par_cfg.inf") <> "" Then
    Kill pro_set.pro_pat + "par_cfg.inf"
End If
If Dir$(pro_set.pro_pat + "t_i_par.ndx") <> "" Then
    Kill pro_set.pro_pat + "t_i_par.ndx"
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
'    db.Execute "create index t_i_par on par_cfg(DESCR,LIVELLO" & i & ",FOR_MAC,N_MOT_L,N_MOT_AB)"
    db.Execute "create index t_i_par on par_cfg(DESCR,LIVELLO" & i & ",FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB)"
    Set ds = db.OpenRecordset("par_cfg", dbOpenTable)
    If ds.RecordCount > 0 Then 'se il file è vuoto esco dalla routine
        'Lettura parametri relativi all'ultima configurazione attiva (con l'attuale configurazione della macchina)
        ds.Index = "t_i_par"
        set_att(i) = False
        res_cfg_att (i) 'Arturo uff 05-11-02
        descr = Trim$(par_mac.config_sch_tag_att(i).descr)
'Arturo uff 05-11-02
'        ds.Seek "=", descr, "1", for_mac, num_mot_liv, par_abi.num_mot_abi(i)
        ds.Seek "=", descr, "1", for_mac, gra_car, num_mot_liv, par_abi.num_mot_abi(i)
        If Not ds.NoMatch Then 'configurazione presente
          par_mac.par_cfg_att(i).vel_max_tag = IIf(IsNull(ds("VEL_MAX" & i)), "", ds("VEL_MAX" & i))
          par_mac.par_cfg_att(i).tem_acc_tgl_lun = IIf(IsNull(ds("TEM_ATL" & i)), "", ds("TEM_ATL" & i))
          par_mac.par_cfg_att(i).tem_dec_tgl_lun = IIf(IsNull(ds("TEM_DTL" & i)), "", ds("TEM_DTL" & i))
          par_mac.par_cfg_att(i).tem_acc_tgl_cor = IIf(IsNull(ds("TEM_ATC" & i)), "", ds("TEM_ATC" & i))
          par_mac.par_cfg_att(i).tem_dec_tgl_cor = IIf(IsNull(ds("TEM_DTC" & i)), "", ds("TEM_DTC" & i))
          par_mac.par_cfg_att(i).Kp_nor = IIf(IsNull(ds("KP_NOR" & i)), "", ds("KP_NOR" & i))
          par_mac.par_cfg_att(i).Ki_nor = IIf(IsNull(ds("KI_NOR" & i)), "", ds("KI_NOR" & i))
          par_mac.par_cfg_att(i).fil_kp = IIf(IsNull(ds("FIL_KP" & i)), "", ds("FIL_KP" & i))
          par_mac.par_cfg_att(i).Kv_nor = IIf(IsNull(ds("KV_NOR" & i)), "", ds("KV_NOR" & i))
          par_mac.par_cfg_att(i).lim_lav_Kv = IIf(IsNull(ds("LIM_KV" & i)), "", ds("LIM_KV" & i))
          par_mac.par_cfg_att(i).gir_max_mot_lam = IIf(IsNull(ds("MAX_GIR" & i)), "", ds("MAX_GIR" & i))    'Arturo uff 21-02-03
          set_att(i) = True
          par_mac.config_sch_tag_att(i).num_mot_liv = ds("N_MOT_L")
          par_mac.config_sch_tag_att(i).descr = descr
          setFileINIString "sch_tag_liv_" & i, "config_att", Trim(par_mac.config_sch_tag_att(i).descr), app.EXEName
        End If
    Else ' recordcount = 0
        ds.Close
        If Dir$(pro_set.pro_pat + "par_cfg.inf") <> "" Then
            Kill pro_set.pro_pat + "par_cfg.inf"
        End If
        If Dir$(pro_set.pro_pat + "t_i_par.ndx") <> "" Then
            Kill pro_set.pro_pat + "t_i_par.ndx"
        End If
        Exit For
    End If
    ds.Close
    If Dir$(pro_set.pro_pat + "par_cfg.inf") <> "" Then
        Kill pro_set.pro_pat + "par_cfg.inf"
    End If
    If Dir$(pro_set.pro_pat + "t_i_par.ndx") <> "" Then
        Kill pro_set.pro_pat + "t_i_par.ndx"
    End If
Next i
'Lettura parametri relativi alla configurazione di default (con l'attuale configurazione della macchina)
'Arturo uff 05-11-02
'db.Execute "create index t_i_par on par_cfg(DESCR,FOR_MAC,N_MOT_L,N_MOT_AB)"
db.Execute "create index t_i_par on par_cfg(DESCR,FOR_MAC,GRA_CAR,N_MOT_L,N_MOT_AB)"
Set ds = db.OpenRecordset("par_cfg", dbOpenTable)
If ds.RecordCount > 0 Then
    ds.Index = "t_i_par"
    For i = 1 To par_mac.car_tec.num_liv_tag
        descr = Trim$(par_mac.config_sch_tag_def(i).descr)
        If descr = "" Then descr = "DEFAULT"
'Arturo uff 05-11-02
'        ds.Seek "=", descr, for_mac, gra_car, num_mot_liv, par_abi.num_mot_abi(i)
        ds.Seek "=", descr, for_mac, gra_car, num_mot_liv, par_abi.num_mot_abi(i)
        If Not ds.NoMatch Then 'configurazione di DEFAULT presente
            If Not set_att(i) Then 'se non ho settato la configurazione attiva per questo livello, metto attiva la DEFAULT appena trovata
                par_mac.par_cfg_att(i).vel_max_tag = IIf(IsNull(ds("VEL_MAX" & i)), "", ds("VEL_MAX" & i))
                par_mac.par_cfg_att(i).tem_acc_tgl_lun = IIf(IsNull(ds("TEM_ATL" & i)), "", ds("TEM_ATL" & i))
                par_mac.par_cfg_att(i).tem_dec_tgl_lun = IIf(IsNull(ds("TEM_DTL" & i)), "", ds("TEM_DTL" & i))
                par_mac.par_cfg_att(i).tem_acc_tgl_cor = IIf(IsNull(ds("TEM_ATC" & i)), "", ds("TEM_ATC" & i))
                par_mac.par_cfg_att(i).tem_dec_tgl_cor = IIf(IsNull(ds("TEM_DTC" & i)), "", ds("TEM_DTC" & i))
                par_mac.par_cfg_att(i).Kp_nor = IIf(IsNull(ds("KP_NOR" & i)), "", ds("KP_NOR" & i))
                par_mac.par_cfg_att(i).Ki_nor = IIf(IsNull(ds("KI_NOR" & i)), "", ds("KI_NOR" & i))
                par_mac.par_cfg_att(i).fil_kp = IIf(IsNull(ds("FIL_KP" & i)), "", ds("FIL_KP" & i))
                par_mac.par_cfg_att(i).Kv_nor = IIf(IsNull(ds("KV_NOR" & i)), "", ds("KV_NOR" & i))
                par_mac.par_cfg_att(i).lim_lav_Kv = IIf(IsNull(ds("LIM_KV" & i)), "", ds("LIM_KV" & i))
                par_mac.par_cfg_att(i).gir_max_mot_lam = IIf(IsNull(ds("MAX_GIR" & i)), "", ds("MAX_GIR" & i))   'Arturo uff 21-02-03
                par_mac.config_sch_tag_att(i).num_mot_liv = ds("N_MOT_L")
                set_att(i) = True
                ds.Edit
                ds("LIVELLO" & i) = 1
                If IsNull(ds("LIVELLO" & IIf((i + 1) Mod 3 = 0, 3, (i + 1) Mod 3))) Then ds("LIVELLO" & IIf((i + 1) Mod 3 = 0, 3, (i + 1) Mod 3)) = 0
                If IsNull(ds("LIVELLO" & IIf((i + 2) Mod 3 = 0, 3, (i + 2) Mod 3))) Then ds("LIVELLO" & IIf((i + 2) Mod 3 = 0, 3, (i + 2) Mod 3)) = 0
                ds.Update
                par_mac.config_sch_tag_att(i).descr = descr
                setFileINIString "sch_tag_liv_" & i, "config_att", Trim(par_mac.config_sch_tag_att(i).descr), app.EXEName
            End If
            par_mac.par_cfg_def(i).vel_max_tag = IIf(IsNull(ds("VEL_MAX" & i)), "", ds("VEL_MAX" & i))
            par_mac.par_cfg_def(i).tem_acc_tgl_lun = IIf(IsNull(ds("TEM_ATL" & i)), "", ds("TEM_ATL" & i))
            par_mac.par_cfg_def(i).tem_dec_tgl_lun = IIf(IsNull(ds("TEM_DTL" & i)), "", ds("TEM_DTL" & i))
            par_mac.par_cfg_def(i).tem_acc_tgl_cor = IIf(IsNull(ds("TEM_ATC" & i)), "", ds("TEM_ATC" & i))
            par_mac.par_cfg_def(i).tem_dec_tgl_cor = IIf(IsNull(ds("TEM_DTC" & i)), "", ds("TEM_DTC" & i))
            par_mac.par_cfg_def(i).Kp_nor = IIf(IsNull(ds("KP_NOR" & i)), "", ds("KP_NOR" & i))
            par_mac.par_cfg_def(i).Ki_nor = IIf(IsNull(ds("KI_NOR" & i)), "", ds("KI_NOR" & i))
            par_mac.par_cfg_def(i).fil_kp = IIf(IsNull(ds("FIL_KP" & i)), "", ds("FIL_KP" & i))
            par_mac.par_cfg_def(i).Kv_nor = IIf(IsNull(ds("KV_NOR" & i)), "", ds("KV_NOR" & i))
            par_mac.par_cfg_def(i).lim_lav_Kv = IIf(IsNull(ds("LIM_KV" & i)), "", ds("LIM_KV" & i))
            par_mac.par_cfg_def(i).gir_max_mot_lam = IIf(IsNull(ds("MAX_GIR" & i)), "", ds("MAX_GIR" & i))   'Arturo uff 21-02-03
            par_mac.config_sch_tag_def(i).descr = descr
            setFileINIString "sch_tag_liv_" & i, "config_def", Trim(par_mac.config_sch_tag_def(i).descr), app.EXEName
        Else ' non esiste la configurazione di DEFAULT per questo stato macchina
            If Not set_att(i) Then
                If par_mac.config_sch_tag_att(i).descr <> "" Then
                    par_mac.config_sch_tag_att(i).descr = ""
                    setFileINIString "sch_tag_liv_" & i, "config_att", Trim(par_mac.config_sch_tag_att(i).descr), app.EXEName
                End If
            End If
            If par_mac.config_sch_tag_def(i).descr <> "" Then
                par_mac.config_sch_tag_def(i).descr = ""
                setFileINIString "sch_tag_liv_" & i, "config_def", Trim(par_mac.config_sch_tag_def(i).descr), app.EXEName
            End If
        End If
    Next i
Else ' il file non contiene alcuna configurazione
  For i = 1 To par_mac.car_tec.num_liv_tag
      If Trim$(par_mac.config_sch_tag_att(i).descr) <> "" Then
        par_mac.config_sch_tag_att(i).descr = ""
        setFileINIString "sch_tag_liv_" & i, "config_att", Trim(par_mac.config_sch_tag_att(i).descr), app.EXEName
      End If
      If Trim$(par_mac.config_sch_tag_def(i).descr) <> "" Then
        par_mac.config_sch_tag_def(i).descr = ""
        setFileINIString "sch_tag_liv_" & i, "config_def", Trim(par_mac.config_sch_tag_def(i).descr), app.EXEName
      End If
  Next i
End If
ds.Close
If Dir$(pro_set.pro_pat + "par_cfg.inf") <> "" Then
    Kill pro_set.pro_pat + "par_cfg.inf"
End If
If Dir$(pro_set.pro_pat + "t_i_par.ndx") <> "" Then
    Kill pro_set.pro_pat + "t_i_par.ndx"
End If
End Sub
'Arturo uff 05-11-02 inserito controllo anche su grammatura max cartone
'Public Function trova_cfg_adatta(for_mac As Integer, num_mot_liv As Integer, num_mot_abi As Integer, liv As Integer) As Boolean
Public Function trova_cfg_adatta(for_mac As Integer, gra_car As tip_gra_car, num_mot_liv As Integer, num_mot_abi As Integer, liv As Integer) As Boolean
'Prima cerca una configurazione non di DEFAULT adatta per lo stato macchina del livello liv. Se una tale configurazione non esiste
'cerca la configurazione di DEFAULT. Se anche tale configurazione non esiste restituisce False. L'eventuale configurazione non di
'DEFAULT viene caricata nella par_mac_ute come configurazione attiva in attesa di vedere l'esito della comunicazione con la scheda di taglio del
'livello liv. La configurazione di DEFAULT per quello stato viene invece caricata nella par_mac_ute com configurazione di DEFAULT

Dim ds As Recordset
Dim tro As Boolean

Select Case for_mac + 1
  Case mm1800
    for_mac = 1800
  Case mm2500
    for_mac = 2500
  Case mm2800
    for_mac = 2800
End Select
gra_car = get_gra_car(gra_car) 'Arturo uff 05-11-02
trova_cfg_adatta = False
tro = False
Set ds = db.OpenRecordset("par_cfg", dbOpenTable)
ds.MoveFirst
Do While Not ds.EOF   'Cerco una configurazione non di DEFAULT con questo stato macchina
'Arturo uff 05-11-02
'  If ds("DESCR") <> "DEFAULT" And ds("FOR_MAC") = for_mac And ds("N_MOT_L") = num_mot_liv _'
  If ds("DESCR") <> "DEFAULT" And ds("FOR_MAC") = for_mac And ds("GRA_CAR") = gra_car And ds("N_MOT_L") = num_mot_liv _
    And ds("N_MOT_AB") = num_mot_abi Then
      trova_cfg_adatta = True
      tro = True
      par_mac_ute.config_sch_tag_att(liv).descr = Trim$(ds("DESCR"))
      par_mac_ute.config_sch_tag_att(liv).num_mot_liv = num_mot_liv
      par_mac_ute.par_cfg_att(liv).vel_max_tag = IIf(IsNull(ds("VEL_MAX" & liv)), "", ds("VEL_MAX" & liv))
      par_mac_ute.par_cfg_att(liv).tem_acc_tgl_lun = IIf(IsNull(ds("TEM_ATL" & liv)), "", ds("TEM_ATL" & liv))
      par_mac_ute.par_cfg_att(liv).tem_dec_tgl_lun = IIf(IsNull(ds("TEM_DTL" & liv)), "", ds("TEM_DTL" & liv))
      par_mac_ute.par_cfg_att(liv).tem_acc_tgl_cor = IIf(IsNull(ds("TEM_ATC" & liv)), "", ds("TEM_ATC" & liv))
      par_mac_ute.par_cfg_att(liv).tem_dec_tgl_cor = IIf(IsNull(ds("TEM_DTC" & liv)), "", ds("TEM_DTC" & liv))
      par_mac_ute.par_cfg_att(liv).Kp_nor = IIf(IsNull(ds("KP_NOR" & liv)), "", ds("KP_NOR" & liv))
      par_mac_ute.par_cfg_att(liv).Ki_nor = IIf(IsNull(ds("KI_NOR" & liv)), "", ds("KI_NOR" & liv))
      par_mac_ute.par_cfg_att(liv).fil_kp = IIf(IsNull(ds("FIL_KP" & liv)), "", ds("FIL_KP" & liv))
      par_mac_ute.par_cfg_att(liv).Kv_nor = IIf(IsNull(ds("KV_NOR" & liv)), "", ds("KV_NOR" & liv))
      par_mac_ute.par_cfg_att(liv).lim_lav_Kv = IIf(IsNull(ds("LIM_KV" & liv)), "", ds("LIM_KV" & liv))
      par_mac_ute.par_cfg_att(liv).gir_max_mot_lam = IIf(IsNull(ds("MAX_GIR" & liv)), "", ds("MAX_GIR" & liv))   'Arturo uff 21-02-03
      Exit Do
  End If
  ds.MoveNext
Loop
ds.MoveFirst
Do While Not ds.EOF    'Cerco la configurazione di DEFAULT con questo stato macchina
'Arturo uff 05-11-02
'  If ds("DESCR") = "DEFAULT" And ds("FOR_MAC") = for_mac And ds("N_MOT_L") = num_mot_liv _'
  If ds("DESCR") = "DEFAULT" And ds("FOR_MAC") = for_mac And ds("GRA_CAR") = gra_car And ds("N_MOT_L") = num_mot_liv _
   And ds("N_MOT_AB") = num_mot_abi Then
      If Not tro Then  'non ho trovato una configurazione non di DEFAULT => prendo quella di DEFAULT appena trovata come attiva
          trova_cfg_adatta = True
          par_mac_ute.config_sch_tag_att(liv).descr = Trim$(ds("DESCR"))
          par_mac_ute.config_sch_tag_att(liv).num_mot_liv = num_mot_liv
          par_mac_ute.par_cfg_att(liv).vel_max_tag = IIf(IsNull(ds("VEL_MAX" & liv)), "", ds("VEL_MAX" & liv))
          par_mac_ute.par_cfg_att(liv).tem_acc_tgl_lun = IIf(IsNull(ds("TEM_ATL" & liv)), "", ds("TEM_ATL" & liv))
          par_mac_ute.par_cfg_att(liv).tem_dec_tgl_lun = IIf(IsNull(ds("TEM_DTL" & liv)), "", ds("TEM_DTL" & liv))
          par_mac_ute.par_cfg_att(liv).tem_acc_tgl_cor = IIf(IsNull(ds("TEM_ATC" & liv)), "", ds("TEM_ATC" & liv))
          par_mac_ute.par_cfg_att(liv).tem_dec_tgl_cor = IIf(IsNull(ds("TEM_DTC" & liv)), "", ds("TEM_DTC" & liv))
          par_mac_ute.par_cfg_att(liv).Kp_nor = IIf(IsNull(ds("KP_NOR" & liv)), "", ds("KP_NOR" & liv))
          par_mac_ute.par_cfg_att(liv).Ki_nor = IIf(IsNull(ds("KI_NOR" & liv)), "", ds("KI_NOR" & liv))
          par_mac_ute.par_cfg_att(liv).fil_kp = IIf(IsNull(ds("FIL_KP" & liv)), "", ds("FIL_KP" & liv))
          par_mac_ute.par_cfg_att(liv).Kv_nor = IIf(IsNull(ds("KV_NOR" & liv)), "", ds("KV_NOR" & liv))
          par_mac_ute.par_cfg_att(liv).lim_lav_Kv = IIf(IsNull(ds("LIM_KV" & liv)), "", ds("LIM_KV" & liv))
          par_mac_ute.par_cfg_att(liv).gir_max_mot_lam = IIf(IsNull(ds("MAX_GIR" & liv)), "", ds("MAX_GIR" & liv))   'Arturo uff 21-02-03
      End If
      par_mac_ute.config_sch_tag_def(liv).descr = Trim$(ds("DESCR"))
      par_mac_ute.par_cfg_def(liv).vel_max_tag = IIf(IsNull(ds("VEL_MAX" & liv)), "", ds("VEL_MAX" & liv))
      par_mac_ute.par_cfg_def(liv).tem_acc_tgl_lun = IIf(IsNull(ds("TEM_ATL" & liv)), "", ds("TEM_ATL" & liv))
      par_mac_ute.par_cfg_def(liv).tem_dec_tgl_lun = IIf(IsNull(ds("TEM_DTL" & liv)), "", ds("TEM_DTL" & liv))
      par_mac_ute.par_cfg_def(liv).tem_acc_tgl_cor = IIf(IsNull(ds("TEM_ATC" & liv)), "", ds("TEM_ATC" & liv))
      par_mac_ute.par_cfg_def(liv).tem_dec_tgl_cor = IIf(IsNull(ds("TEM_DTC" & liv)), "", ds("TEM_DTC" & liv))
      par_mac_ute.par_cfg_def(liv).Kp_nor = IIf(IsNull(ds("KP_NOR" & liv)), "", ds("KP_NOR" & liv))
      par_mac_ute.par_cfg_def(liv).Ki_nor = IIf(IsNull(ds("KI_NOR" & liv)), "", ds("KI_NOR" & liv))
      par_mac_ute.par_cfg_def(liv).fil_kp = IIf(IsNull(ds("FIL_KP" & liv)), "", ds("FIL_KP" & liv))
      par_mac_ute.par_cfg_def(liv).Kv_nor = IIf(IsNull(ds("KV_NOR" & liv)), "", ds("KV_NOR" & liv))
      par_mac_ute.par_cfg_def(liv).lim_lav_Kv = IIf(IsNull(ds("LIM_KV" & liv)), "", ds("LIM_KV" & liv))
      par_mac_ute.par_cfg_def(liv).gir_max_mot_lam = IIf(IsNull(ds("MAX_GIR" & liv)), "", ds("MAX_GIR" & liv)) 'Arturo uff 21-02-03
      Exit Do
  End If
  ds.MoveNext
Loop

ds.Close
  
End Function


