Attribute VB_Name = "uti_stag"
Option Explicit

Public Function CreateZIP(ByVal IDComponente As String, ByVal JOB As String, ByVal com As String, ByVal ZipListFileName As String, ByVal DestFolder As String) As cod_ret
'Crea un archivio ZIP per codice,dati o log coerente con documento ProcInt09
'N.B. I percorsi indicati in ZipListFileName sono relativi alla posizione di ZipListFileName stesso, la prima riga indica la diretory di base
Const AOC_SPDE_30_20 As String = "SPD"
Const MASTER_21_30 As String = "MST"
Const TWIN_22_50 As String = "TWE"
Dim MacName, ZipName, Timestamp As String
Dim s As String
Dim shell_ret_val As Double
Dim fso As New FileSystemObject
Dim HasTimeStamp, IsMacZip As Boolean

On Local Error GoTo ErrorHandler

  CreateZIP = RET_BAD
  log_str "CreateZIP: IDComponente=" & IDComponente & " Job=" & JOB & " Com=" & com & " ZipListFileName=" & ZipListFileName & " DestFolder=" & DestFolder, MED_PRIORITY, log_gen
  If Len(JOB) = 0 Or Len(JOB) < 4 Or LCase(JOB) = "xxxx" Then JOB = "xxxx": IsMacZip = True
  If Len(com) = 0 Or Len(com) < 5 Or LCase(com) = "yyccc" Then com = "YYccc": IsMacZip = True
  If Len(ZipListFileName) > 0 Then
    If Not fso.FileExists(ZipListFileName) Or Not (UCase(fso.GetExtensionName(ZipListFileName)) = "TXT") Then Exit Function 'ZipListFileName not found
    ZipListFileName = Chr(34) & ZipListFileName & Chr(34)
  End If
  If Len(DestFolder) > 0 Then
    If Not fso.FolderExists(DestFolder) Then Exit Function     'DestFolder not found
  End If
  If Len(IDComponente) = 0 Or Len(IDComponente) < 3 Then IDComponente = Right(Space(3) & IDComponente, 3)
  MacName = UCase(app.EXEName)
  If InStr(1, MacName, "SPDE") > 0 Then
    MacName = AOC_SPDE_30_20
  ElseIf InStr(1, MacName, "STAG") > 0 Then
    MacName = MASTER_21_30
  ElseIf InStr(1, MacName, "PMAC") > 0 Then
    MacName = TWIN_22_50
  Else
    MacName = Right(Space(3) & MacName, 3)
  End If
  Dim ext As String
  #If CJ = 1 Then
    ext = "a"
  #Else
    ext = "INT"
  #End If
  ZipName = Chr$(34) & DestFolder & "\" & UCase(IDComponente) & "x" & UCase(MacName) & "_" & JOB & com & "_Ver" & Trim(Frm_ini.Vrs_Nmb.Caption) & ext
  If (IDComponente = "LOG" Or IDComponente = "DAT") And Not IsMacZip Then
    HasTimeStamp = True
  Else
    HasTimeStamp = False
  End If
  Timestamp = IIf(HasTimeStamp, " (" & Format(Date, "dd-mm-yyyy") & "_" & Format(Time) & ")", "")
  ZipName = ZipName & Timestamp & ".zip" & Chr$(34)
  s = pro_set.pro_pat & "7za a -tzip " & ZipName & " -p" & LCase(Pwd_frm.decode(pro_set.pwd, True)) & " @" & ZipListFileName
  If Shell(s, vbHide) <> 0 Then CreateZIP = RET_OK
  Set fso = Nothing
  Exit Function
  
ErrorHandler:
  log_str "CreateZIP-ErroreN.=" & Format(err.Number) & "Descrizione errore=" & Format(err.Description) & ": IDComponente=" & IDComponente & " Job=" & JOB & " Com=" & com & " ZipListFileName=" & ZipListFileName & " DestFolder=" & DestFolder, MED_PRIORITY, log_gen
  Set fso = Nothing
  err.Clear
  
End Function

Function CopyFolderFiles(ByVal SourcePath As String, ByVal DestPath As String) As cod_ret
'Funzione che copia tutti i file da una cartella di origine (in modo non ricorsivo) ad una di destinazione
Dim fso As New FileSystemObject
Dim f As File

On Local Error GoTo ErrorHandler
  
  CopyFolderFiles = RET_BAD
  If Len(SourcePath) > 0 Then TestAndCreateDir SourcePath
  If Len(DestPath) > 0 Then TestAndCreateDir DestPath
  For Each f In fso.GetFolder(SourcePath).Files
    f.Copy DestPath & "\" & f.name, True
  Next f
  CopyFolderFiles = RET_OK
  Set fso = Nothing
  Set f = Nothing
  
  Exit Function
  
ErrorHandler:
  log_str "CopyFolderFiles-ErroreN.=" & Format(err.Number) & "Descrizione errore=" & Format(err.Description) & ": SourcePath=" & SourcePath & " DestPath=" & DestPath, MED_PRIORITY, log_gen
  Set fso = Nothing
  Set f = Nothing
  err.Clear
  
End Function

Function DeleteFolder(ByVal DestPath As String) As cod_ret
'Funzione che copia tutti i file da una cartella (in modo non ricorsivo) di origine ad una di destinazione
Dim fso As New FileSystemObject

On Local Error GoTo ErrorHandler
  
  DeleteFolder = RET_BAD
  If Len(DestPath) > 0 Then
    If fso.FolderExists(DestPath) Then fso.DeleteFolder DestPath, True
  End If
  DeleteFolder = RET_OK
  Set fso = Nothing
  Exit Function
  
ErrorHandler:
  log_str "CopyFolderFiles-ErroreN.=" & Format(err.Number) & "Descrizione errore=" & Format(err.Description) & " DestPath=" & DestPath, MED_PRIORITY, log_gen
  Set fso = Nothing
  err.Clear
  
End Function

'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
Public Function ordineVuoto(rec As Tip_Ordine, Optional checkDatiTestata As Boolean = False) As Boolean
  Dim i As Integer

  ordineVuoto = True
  
  If checkDatiTestata Then
    If err_lun_Cod_ord_abb(rec) Or err_ass_ond(rec) Or err_ass_car(rec) Then
      ordineVuoto = False
      Exit Function
    End If
  End If
  
  For i = 1 To 3
    If val(rec.ord(i).Sca_per_tag) <> 0 And (val(rec.ord(i).sca_lar) \ TODECMM) <> 0 And (val(rec.ord(i).sca_lun) \ TODECMM) <> 0 Then
      ordineVuoto = False
      Exit Function
    End If
  Next i
End Function
Function TestAndCreateDir(ByVal DirPath As String) As cod_ret
'Funzione che controlla l'esistenza di una directory ed eventualmente la crea
Dim fso As New FileSystemObject

On Local Error GoTo ErrorHandler
  
  TestAndCreateDir = RET_BAD
  If Len(DirPath) > 0 Then
    If Not fso.FolderExists(DirPath) Then fso.CreateFolder DirPath
  End If
  TestAndCreateDir = RET_OK
  Set fso = Nothing
  Exit Function

ErrorHandler:
  Debug.Print "Setup: ErroreN.=" & Format(err.Number) & " Descrizione=" & Format(err.Description)
  Set fso = Nothing
  err.Clear

End Function
Function cal_tem_co(met_rim As Long, vel As Integer) As String
Dim app As Double
Dim app1 As Double
Dim app2 As Long
Dim pos As Integer
Dim ore As String
Dim min As String
Dim sec As String

If vel = 0 Or met_rim <= 0 Then
  If met_rim <= 0 Then
    cal_tem_co = "00:00:00"
  Else
    cal_tem_co = "23:59:59"
  End If
Else
  app = met_rim / vel
  app1 = app / 60
  app2 = CLng(app1)
  If app2 = 0 Then
    ore = "00"
    app2 = CLng(app)
    If app2 = 0 Then
      min = "00"
      sec = Format$(app * 60, "00")
    Else
      pos = InStr(1, CStr(app), ",")
      If pos = 0 Then
        min = Format$(app, "00")
        sec = "00"
      Else
        min = Format$(Mid$(CStr(app), 1, pos - 1), "00")
        app1 = (CDbl("0" & Mid$(CStr(app), pos)) * 60) Mod 60
        sec = Format$(app1, "00")
      End If
    End If
  Else
    pos = InStr(1, CStr(app1), ",")
    If pos = 0 Then
      ore = Format$(app1, "00")
      min = "00"
      sec = "00"
    Else
      ore = Format$(Mid$(CStr(app1), 1, pos - 1), "00")
      min = Format$(app Mod 60, "00")
      pos = InStr(1, CStr(app), ",")
      If pos = 0 Then
        sec = "00"
      Else
        app1 = (CDbl("0" & Mid$(CStr(app), pos)) * 60) Mod 60
        sec = Format$(app1, "00")
      End If
    End If
  End If
  cal_tem_co = ore & ":" & min & ":" & sec
End If
End Function



Sub chk_BlocNum()
'controllo lo stato del tasto BlocNum e lo setta se non risulta azionato
Dim retval As Long

retval = GetKeyState(VK_NUMLOCK)
If (retval And &H1) <> &H1 Then
  keybd_event VK_NUMLOCK, 0, 0, 0
  keybd_event VK_NUMLOCK, 0, KEYEVENTF_KEYUP, 0
End If
End Sub

Sub DisableBlocNum()
'controllo lo stato del tasto BlocNum e lo setta se non risulta azionato
Dim retval As Long

retval = GetKeyState(VK_NUMLOCK)
If (retval And &H1) = &H1 Then
  keybd_event VK_NUMLOCK, 0, 0, 0
  keybd_event VK_NUMLOCK, 0, KEYEVENTF_KEYUP, 0
End If
End Sub
Sub chk_shift()
'controllo dello stato del tasto SHIFT
'se SHIFT è premuto lo resetta
Dim retval As Long

retval = GetKeyState(VK_SHIFT)
If retval < 0 Then 'se è < 0 allora c'è il tasto SHIFT premuto
  keybd_event VK_SHIFT, 0, 0, 0
  keybd_event VK_SHIFT, 0, KEYEVENTF_KEYUP, 0
  retval = GetKeyState(VK_SHIFT)
End If
End Sub


Function get_gra_car(g_car As tip_gra_car)
  Select Case g_car
    Case tip_gra_car.gm2_1800
      get_gra_car = 1800
    Case tip_gra_car.gm2_2600
      get_gra_car = 2600
    Case Else
      get_gra_car = g_car
  End Select
End Function

Sub inc_prg_bar_ini()
'Arturo uff 25-07-02 questa funzione controlla che non si superi il valore massimo impostabile sulla
'progress bar sulla form frm_ini

  If Frm_ini.prg_bar_ini.Value < Frm_ini.prg_bar_ini.Max Then
    Frm_ini.prg_bar_ini.Value = Frm_ini.prg_bar_ini.Value + 1
  End If

End Sub

Sub Log_lst(ord As CollezioneOrdini)
Const DIM_COL = 15  'Dimensione in caratteri di una colonna relativa all'id. Tale valore deve essere >= del numero di caratteri che rappresentano l'id.
Dim p As Integer

    log_str "Stato lista: Pos   |ID SPDE        |ID STAG        |", MED_PRIORITY, log_lis
    For p = 1 To ord_mac.Count
      With ord.item(p)
        If Len(.spde_id) <= DIM_COL And Len(.stag_id) <= DIM_COL Then
          log_str Space$(13) + CStr(p) + Space$(6 - Len(CStr(p))) + "|" + .spde_id + Space$(15 - Len(.spde_id)) + "|" + .stag_id + Space$(15 - Len(.stag_id)) + "|", MED_PRIORITY, log_lis
        End If
      End With
    Next p

End Sub

Function LRC_FCS(Mess As String) As String
'calcola l'LRC per il protocollo di comunicazione seriale con punto unico
Dim i As Integer, chk As Long
Dim res As String

  chk = 0
  
  For i = 1 To Len(Mess)
    chk = chk + Asc(Mid$(Mess, i, 1))
  Next i
  chk = Not chk
  
  res = Hex$(chk)
  
  LRC_FCS = Right$("00" + res, 2)
  
End Function
Function comp_2(s As String) As String
'la stringa passata s deve essere in binario: il risultato è il complemento a 2 di tale stringa
Dim i As Integer
Dim str As String
Dim tro As Boolean

str = ""
tro = False
For i = Len(s) To 1 Step -1
  If Not tro Then
      str = Mid$(s, i, 1) + str
      If Mid$(s, i, 1) = "1" Then tro = True
  Else
      str = IIf(Mid$(s, i, 1) = "1", "0", "1") + str
  End If
Next i
comp_2 = str
End Function

Sub log_str(stringa As String, Priority As Integer, Log As cos_log, Optional show As Boolean = True)
Dim stri As String
Dim Nam_fil_log As String     'nome del file di log
Dim Nam_fil_bak As String     'nome del file di backup del file di log
Dim MAX_LEN_LOG As Long       'massima lunghezza del file di log
Dim Dsk_priority As Integer   'livello di priorità dal quale si cominciano a registrare i messaggi sul disco
Dim ret_deb As String  'ritorno debug a video
  stri = Format$(Now, "dd/mm/yy") + " | " + Format$(Now, "hh:mm:ss") + " | " + stringa
   'controllo se devo scrivere il messaggio su Hard Disk
  If Priority <= DSK_DEF_PRIORITY Then
    Open pro_set.Log(Log).Nam_fil_log For Append As #NUM_FIL_LOG
    Print #NUM_FIL_LOG, stri
    Close #NUM_FIL_LOG
    'controllo sulla lunghezza del file di log
    If FileLen(pro_set.Log(Log).Nam_fil_log) > pro_set.Log(Log).MAX_LEN_LOG Then
      If Dir$(pro_set.Log(Log).Nam_fil_bak) <> "" Then
        Kill pro_set.Log(Log).Nam_fil_bak
      End If
     Name pro_set.Log(Log).Nam_fil_log As pro_set.Log(Log).Nam_fil_bak
    End If
  End If
  If show Then
    If frm_debug.Visible Then frm_debug.msg_box_AddMsg frm_debug.log_box, stri
  End If
  If Priority = MAX_PRIORITY Then
    MsgBox stringa, vbOKOnly, "Fatal error"
    End
  End If
End Sub
 Function chr_ok(char As Integer) As Integer
'indica se il carattere passato è riconosciuto da windows. Viene utilizzata per filtrare i disturbi di rete sulle comunicazioni
'  Select Case char
'    Case 32 To 126, 8 To 10, 13, STX_TAG, ETX_TAG
'      chr_ok = True
'    Case Else
'      chr_ok = False
'  End Select
End Function

Function BCC_FCS(msg As String) As String
'calcolo il BCC per il protocollo di comunicazione seriale con il punto unico
Dim i As Integer, chk As Long
Dim res As String

  chk = 0
  
  For i = 1 To Len(msg)
    chk = chk + Asc(Mid$(msg, i, 1))
  Next i
  res = Hex$(chk)
  
  BCC_FCS = Right$("00" + res, 2)

End Function
Public Function Ord_str(ord As Tip_Ordine) As String
'Copia un ordine in una stringa
Dim s As String
  s = Space$(Len(ord))
  CopyString s, ord, Len(ord)
  Ord_str = s
End Function




Function pos_car(s As String, car As String) As Integer
'restituisce la posizione del carattere car nella stringa s
'se il carattere non c'è restituisce 0

pos_car = InStr(1, s, car)

End Function

Public Function str_ord(s As String, Optional ret As cod_ret) As Tip_Ordine
'Copia una stringa in un ordine
Dim ord As Tip_Ordine
  ret = RET_BAD
  If Len(s) = Len(ord) Then
    CopyType ord, s, Len(ord)
    str_ord = ord
    ret = RET_OK
  Else
    log_str "Str_ord: stringa di lunghezza non valida", MED_PRIORITY, log_ord
  End If
End Function









 






Function des_pag_all(str_pag As String) As String
Select Case Mid$(str_pag, 1, 1)
  Case 1
    des_pag_all = "mac"
  Case 2
    des_pag_all = "har"
  Case 3
    des_pag_all = "con"
  Case 4
    des_pag_all = "qe1"
  Case 5
    des_pag_all = "qe2"
  Case 6
    des_pag_all = "qe3"
  Case 7
    des_pag_all = "war"
End Select
End Function



Public Sub dimensiona(f As Form)
Dim c As Control
Dim i As Integer
Dim PercX As Integer
Dim PercY As Integer

PercX = pro_set.PercX
PercY = pro_set.PercY

For Each c In f

  If TypeOf c Is Shape Or TypeOf c Is Image Or _
  TypeOf c Is MSFlexGrid Or TypeOf c Is PictureBox Then
        If (f.name <> "Frm_ini" Or (f.name = "Frm_ini" And c.name <> "img_title")) And _
         ((f.name <> "Sel_lin") Or (f.name = "Sel_lin" And c.name <> "img_title")) Then
            c.Width = Int(c.Width * PercX / 100)
            c.Top = Int(c.Top * PercY / 100)
            c.Left = Int(c.Left * PercX / 100)
            c.Height = Int(c.Height * PercY / 100)
        End If
    ElseIf TypeOf c Is Frame Then
            c.Width = Int(c.Width * PercX / 100)
            c.Top = Int(c.Top * PercY / 100)
            c.Left = Int(c.Left * PercX / 100)
            c.Height = Int(c.Height * PercY / 100)
            c.FontSize = Int(c.FontSize * PercY / 100)
    ElseIf TypeOf c Is Line Then
            c.X1 = Int(c.X1 * PercX / 100)
            c.X2 = Int(c.X2 * PercX / 100)
            c.Y1 = Int(c.Y1 * PercY / 100)
            c.Y2 = Int(c.Y2 * PercY / 100)
  ElseIf TypeOf c Is Label Or TypeOf c Is TextBox Or TypeOf c Is CommandButton _
  Or TypeOf c Is CheckBox Or TypeOf c Is SSCheck Then
        If (f.name <> "Sel_lin") Or (f.name = "Sel_lin" And c.name <> "Pag_title") Then
            c.Height = Int(c.Height * PercY / 100)
            c.Width = Int(c.Width * PercX / 100)
            c.Left = Int(c.Left * PercX / 100)
            c.Top = Int(c.Top * PercY / 100)
            c.FontSize = Int(c.FontSize * PercY / 100)
        End If
  ElseIf TypeOf c Is ComboBox Then
            c.Width = Int(c.Width * PercX / 100)
            c.Left = Int(c.Left * PercX / 100)
            c.Top = Int(c.Top * PercY / 100)
            c.FontSize = Int(c.FontSize * PercY / 100)
  ElseIf TypeOf c Is DBCombo Then
            c.Width = Int(c.Width * PercX / 100)
            c.Left = Int(c.Left * PercX / 100)
            c.Top = Int(c.Top * PercY / 100)
  ElseIf TypeOf c Is DBGrid Then
            c.Width = Int(c.Width * PercX / 100)
            c.Top = Int(c.Top * PercY / 100)
            c.Left = Int(c.Left * PercX / 100)
            c.Height = Int(c.Height * PercY / 100)
            c.RowHeight = c.RowHeight * PercY / 100
            For i = 0 To c.Columns.Count - 1
                c.Columns(i).Width = c.Columns(i).Width * PercX / 100
            Next i
  ElseIf TypeOf c Is ProgressBar Then
            c.Width = Int(c.Width * PercX / 100)
            c.Top = Int(c.Top * PercY / 100)
            c.Left = Int(c.Left * PercX / 100)
            c.Height = Int(c.Height * PercY / 100)
'  ElseIf TypeOf c Is Timer Or TypeOf c Is data Or TypeOf c Is MSComm Or TypeOf c Is SYSMAC_C _
'    Or TypeOf c Is Winsock Then
'      'non deve fare niente
  Else
            log_str "Dimensiona: tipo di controllo non previsto: " + c.name, MED_PRIORITY, log_gen
  End If
              
Next

End Sub


Sub Ridim(f As Form)
  f.Width = Int(f.Width * pro_set.PercX / 100)
  f.Height = Int(f.Height * pro_set.PercY / 100)
End Sub


Sub messag_win(stri As String, Optional abi_mot As Boolean = False)
  Const MES_DIF_HOR = 120
  Const MES_DIF_VER = 100
  'Const MES_ALL_WID = 11000
  Const MES_MES_RAP = 0.7
  Const MES_SSP_BAK_MES = vbGreen
  Const MES_LAB_FOR_MES = vbBlack
  'const SSP_TOP = Sta_bar.fra_all.Top
  'Const SSP_LEFT = Sta_bar.fra_all.Left
  'Const SSP_HEIG = Sta_bar.fra_all.Height
  Const FONT_HEIG = 16

  If stri = sta_bar.mes_all.Caption Then Exit Sub 'esco se i due messaggi coincidono
  If Not abi_mot Then
    sta_bar.SSP_mes_all.Width = sta_bar.frm_vel.Width + sta_bar.Fra_vel_max.Width + sta_bar.frm_met_man.Width + sta_bar.frm_tem_co.Width - Int(10 * (pro_set.PercX / 100))
    sta_bar.SSP_mes_all.Height = sta_bar.frm_vel.Height '- 70
    sta_bar.SSP_mes_all.Top = sta_bar.frm_vel.Top
    sta_bar.SSP_mes_all.Left = sta_bar.frm_vel.Left
    sta_bar.SSP_mes_all.ZOrder (0)
    sta_bar.mes_all.Font.Size = FONT_HEIG
    sta_bar.mes_all.Width = sta_bar.SSP_mes_all.Width - MES_DIF_HOR
    sta_bar.mes_all.Height = sta_bar.SSP_mes_all.Height - MES_DIF_VER
    sta_bar.mes_all.Left = MES_DIF_HOR \ 2
    sta_bar.mes_all.Top = -20 'MES_DIF_VER \ 2
    sta_bar.mes_all.Alignment = 2
    sta_bar.mes_all.Caption = stri  'visualizzo il messaggio
    If stri = "" Then 'nascondo il pannello dei messaggi
      sta_bar.SSP_mes_all.Left = sta_bar.Left * 2 + sta_bar.Width + 100
      sta_bar.SSP_mes_all.ZOrder (1)
      sta_bar.frm_vel.Visible = True
      sta_bar.Fra_vel_max.Visible = True
      sta_bar.frm_met_man.Visible = True
      sta_bar.frm_tem_co.Visible = True
      sta_bar.fra_ora(0).Visible = True
      sta_bar.frm_mod.Visible = True
    End If
Else
    sta_bar.SSP_mes_all.Left = 10 * (pro_set.PercX / 100)
    sta_bar.SSP_mes_all.Width = sta_bar.frm_mod.Left + sta_bar.frm_mod.Width
    sta_bar.SSP_mes_all.Height = sta_bar.frm_mod.Height
    sta_bar.SSP_mes_all.Top = sta_bar.frm_mod.Top
    sta_bar.mes_all.Font.Size = FONT_HEIG
    sta_bar.mes_all.Width = sta_bar.SSP_mes_all.Width - MES_DIF_HOR
    sta_bar.mes_all.Height = sta_bar.SSP_mes_all.Height - MES_DIF_VER
    sta_bar.mes_all.Left = MES_DIF_HOR \ 2
    sta_bar.mes_all.Top = -20 'MES_DIF_VER \ 2
    sta_bar.mes_all.Alignment = 2
    sta_bar.mes_all.Caption = stri  'visualizzo il messaggio
    sta_bar.frm_mod.Visible = False
    sta_bar.fra_ora(0).Visible = False
End If
End Sub

Public Function Rie_nul(ByVal s As String, Optional car_rie As String = " ") As String
Dim l As Integer, i As Integer
  l = Len(s)
  For i = 1 To l
    If Mid$(s, i, 1) = Chr$(0) Or Mid$(s, i, 1) = Chr$(32) Then
      Mid$(s, i, 1) = car_rie
    End If
  Next i
  Rie_nul = s
End Function

Sub Show_tas_des(f_name As String, Shift As Integer)
'visualizza la descrizione dei tasti funzionali, considerando anche lo stato di Shift
'f_name rappresenta la form attiva
Dim k As Integer, i As Integer, res As Integer
Dim Text As String
  For k = 1 To 10 'Gestione dei tasti funzione (non genero la stringa di log)
    Tasti.des_fun(k).Caption = msg(f_name + "_" + Format$(Shift, "0") + " " + Format$(k), Log:=False)
    res = 0 'controllo se ci sono dei messaggi associati ai tasti Shift, Ctrl e Alt
    For i = 1 To 7 'ciclo su tutte le possibili combinazioni dei tasti funzione
      If msg(f_name + "_" + Format$(i, "0") + " " + Format$(k), Log:=False) <> "" Then
        res = res Or i 'aggiungo i bit coinvolti
      End If
    Next i
    Text = "" 'visualizzazione del risultato ottenuto
    If (res And vbShiftMask) > 0 Then Text = Text + "S"
    If (res And vbCtrlMask) > 0 Then Text = Text + "C"
    If (res And vbAltMask) > 0 Then Text = Text + "A"
    Tasti.txt_shift(k).Caption = Text
  Next k
End Sub

Sub AutoAdminLogon()
'Imposta i settaggi per l'avvio automatico del computer
Dim s As String, Value As String, item As String
Dim kHandle As Long
Dim res As Long
 
  s = "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"
  res = RegOpenKeyEx(HKEY_LOCAL_MACHINE, s, 0, KEY_ALL_ACCESS, kHandle)
  If res <> ERROR_SUCCESS Then
    log_str "AutoAdminLogon: impossibile aprire la chiave Winlogon", MED_PRIORITY, log_gen
    Exit Sub
  End If
  'andreacs 18-03-01   rimessa questa roba
  'settaggio di AutoAdminLogon
  item = "AutoAdminLogon": Value = "1"
  res = RegSetValueEx(kHandle, item, 0, REG_SZ, ByVal Value, Len(Value))
 ' settaggio di DefaultPassword
  item = "DefaultPassword": Value = "fosber"
  res = RegSetValueEx(kHandle, item, 0, REG_SZ, ByVal Value, Len(Value))
  'settaggio di DefaultUserName
  item = "DefaultUserName": Value = "Administrator"
  res = RegSetValueEx(kHandle, item, 0, REG_SZ, ByVal Value, Len(Value))
  
 End Sub

Function bin_hex(bin As String) As String
' converte da binario a esadecimale, il numero binario e' scritto in reverse ( dal - significativo )
Dim i As Integer, h As Long
Dim str As String
For i = 0 To Len(bin) - 1
  str = str + Mid$(bin, Len(bin) - i, 1)
Next i
h = 0
For i = 0 To Len(str) - 1
    h = val(Mid$(str, Len(str) - i, 1)) + 2 * h
Next i
bin_hex = int_esa(h)
End Function

Function bin_hex_azi(bin As String) As String
' converte da binario a esadecimale
Dim i As Integer, h As Double
Dim ris As Double
Dim str As String

h = 0
For i = 0 To Len(bin) - 1
    h = val(Mid$(bin, Len(bin) - i, 1)) + 2 * h
Next i
bin_hex_azi = int_esa_azi(h)

ris = 0
h = 1
For i = 0 To Len(bin) - 1
    If i > 0 Then h = h * 2
    If val(Mid$(bin, Len(bin) - i, 1)) = 1 Then ris = ris + h
Next i
bin_hex_azi = int_esa_azi(ris)


End Function
Function bin_lng_hex(bin As String) As String
' converte da binario long a esadecimale, il numero binario e' scritto in reverse ( dal - significativo )
Dim i As Integer, h As Long
h = 0
  For i = 0 To 31
    h = val(Mid$(bin, 32 - i, 1)) + 2 * h
  Next i
  bin_lng_hex = lng_esa(h)
End Function

Sub Center(f As Form)
'centra la form passata nello schermo
  f.Left = (Screen.Width - f.Width) \ 2
  f.Top = (Screen.Height - f.Height) \ 2
End Sub

Function int_bcd(v As Variant) As String
' converte il valore in stringa bcd
  int_bcd = Right$("0000" + Trim$(Format$(v)), 4)
End Function

Function lng_bcd(v As Variant) As String
' converte il valore in una stringa  8 caratteri bcd,
' formattandola e scambiando i due byte
Dim s As String
  s = Right$("00000000" + Trim$(Format$(v)), 8)
  lng_bcd = Right$(s, 4) + Left$(s, 4)
End Function
Function lng_esa(v As Variant) As String
' converte il valore in una stringa  8 caratteri esadecimale
  lng_esa = Right$(String$(8, "0") + Hex$(v), 8)
End Function

Function Sng(v As Variant) As Single
'converte il valore passato in single:utilizza Csng se possibile, altrimenti
'utilizza Val
On Error GoTo usaVal
  Sng = CSng(v)
  Exit Function
usaVal:
  Sng = val(v)
End Function



Sub Termina()
Dim k As Integer
Dim s As String
  AutoAdminLogon 'settaggio password e logon automatico
End
End Sub

Sub Mouse(v As Boolean)
'nasconde o visualizza il mouse
  If Not v Then
    If Screen.MousePointer <> vbCustom Then
      Screen.MouseIcon = LoadPicture(pro_set.pro_pat + "null.ico")
      Screen.MousePointer = vbCustom
    End If
  Else
    Screen.MousePointer = vbDefault
  End If
End Sub

Function newline(Optional n As Integer, Optional stp As Integer) As String
  'restituisce la stringa di "Ritorno a Capo n volte" (se n non è presente -> n=1)
  '(Su SPDE) Se è presente l'handle della stampante (stp), ne aggiorna il contatore delle righe stampate
  Dim newl As String, i As Integer
  newl = ""
  If IsMissing(n) Or n <= 0 Then n = 1
  For i = 1 To n
    newl = newl + Chr$(vbKeyReturn) + Chr$(10)
  Next i
  #If psp = 1 Then
    If (Not IsMissing(stp)) And (stp <> 0) Then lin.sta(stp).num_lin_stp = lin.sta(stp).num_lin_stp + n
  #End If
    newline = newl
End Function


Sub Carica(f As Form, Optional form_pic As Boolean)

If form_pic = False Then
  f.Left = 0
  f.Width = Screen.Width
  f.Top = Tasti.Top + Tasti.Height
  f.Height = sta_bar.Top - f.Top
  If pro_set.PercX <> 100 Or pro_set.PercY <> 100 Then dimensiona f
End If
  
End Sub

Function hex_bin(s As String, rev As Boolean) As String
' converte da esadecimale a binario
Dim i As Integer
Dim h As String
h = ""
For i = 1 To Len(s)
  h = h + nib_bin(Mid$(s, i, 1))
Next i
If rev Then
  hex_bin = rev_str(h)
Else
  hex_bin = h
End If
End Function

Function rev_str(s As String) As String
Dim tmp_st1 As String, tmp_st2 As String
Dim i As Integer
tmp_st2 = s
For i = 1 To Len(s)
  tmp_st1 = Left(tmp_st2, 1) + tmp_st1
  tmp_st2 = Right$(tmp_st2, Len(tmp_st2) - 1)
Next i
rev_str = tmp_st1
End Function

Function int_esa(v As Variant) As String
' converte il valore in stringa esadecimale
'Arturo uff 23-07-02 si è posta la val sul variant v per evitare errori di run time nel caso in v finissero degli 0 binari.
'  int_esa = Right$(String$(4, "0") + Hex$(v), 4)
  int_esa = Right$(String$(4, "0") + Hex$(val(v)), 4)
End Function

Function int_esa_azi(v As Variant) As String
' converte il valore in stringa esadecimale
Dim str As String

str = Hex$(v)
If Len(str) = 1 Then
    int_esa_azi = "0" + str
Else
    int_esa_azi = str
End If
End Function
Sub sum_esa(v As String, num As Integer)
Dim AD As Long
  AD = "&H" + v + num
  v = int_esa(AD)
End Sub

Function leg_lng(dm1 As String, dm2 As String) As Double
 'variato da simone 1-2-2000
Dim d As Double
  
  d = CDbl("&H0" + dm2 + dm1)
  If d < 0 Then d = 2 ^ 32 + d
  leg_lng = d
End Function

 Function leg_int(DM As Tip_dm, ind As Integer) As Long
Dim d As Long
  leg_int = CLng("&H0" + DM.DM(ind))
End Function

Sub inv_str(s As String)
'inverte i caratteri della stringa a gruppi di 4
Dim tmp As String

Dim i As Integer, k As Integer, h As Integer

If Len(s) Mod 4 <> 0 Then Exit Sub
tmp = ""
k = Len(s) \ 4 'k indica il numero di pacchetti da invertire
For i = 1 To k 'per ogni pacchetto
  For h = 4 To 1 Step -1
    tmp = tmp + Mid$(s, h, 1)
  Next h
  s = Mid$(s, 5)
Next i
s = tmp
End Sub

Sub dm_int(DM As Tip_dm, ind As Integer, v As Variant)
  DM.DM(ind) = int_esa(v)
End Sub

Sub dm_lng(DM As Tip_dm, ind As Integer, v As Variant)
' mette il valore v in due dm
Dim s As String
  s = String$(8, "0") + Hex$(v)
  DM.DM(ind) = Right$(s, 4)
  DM.DM(ind + 1) = Mid$(s, Len(s) - 7, 4)
End Sub

Sub messaggio(stri As String, is_All As Boolean, Optional stay As Boolean, Optional vis_for As Boolean = False, Optional liv As Integer = 0)
Dim i As Integer

'is_All indica se si tratta di un messaggio di allarme o di una semplice comunicazione
'stay indica se il messaggio deve rimanere a video (fino ad un nuovo messaggio o deve scomparire)
If Not vis_for Then
  If stri = sta_bar.lab_msg.Caption Then Exit Sub
  sta_bar.lab_msg.ForeColor = IIf(is_All, COL_ALL_Red, COL_DARK_Green)
  sta_bar.lab_msg.Caption = stri
  If sta_bar.lab_msg.Width >= sta_bar.Width Then
  sta_bar.lab_msg.Left = 30
  Else 'centro il messaggio
  sta_bar.lab_msg.Left = (sta_bar.Width - sta_bar.lab_msg.Width) \ 2
  End If
  
  'nel campo tag del controllo messaggio memorizzo lo stato della variabile stay
  If IsMissing(stay) Or stay = False Then
    sta_bar.lab_msg.Tag = ""
  Else
    sta_bar.lab_msg.Tag = "STAY"
  End If
Else  'caso di visualizzazione pagina messaggi
  If liv > 0 And liv <= val(par_mac.car_tec.num_liv_tag) Then
      If Not Screen.ActiveForm.name = pag_msg.name Then
          pag_msg.show
          pag_msg.Left = 0
          pag_msg.Width = Screen.Width
          pag_msg.Height = 0
          For i = 1 To num_com_sch_tag
            If num_com_sch_tag > 1 Then
                pag_msg.Height = pag_msg.Height + pag_msg.lab_msg(i - 1).Height
                If i > 1 Then
                    pag_msg.lab_msg(i - 1).Top = pag_msg.lab_msg(i - 2).Top + pag_msg.lab_msg(i - 2).Height + pro_set.PercY / 100 * 50
                Else
                    pag_msg.lab_msg(i - 1).Top = pro_set.PercY / 100 * 150
                End If
            Else
                pag_msg.Height = pag_msg.Height + pag_msg.lab_msg(liv - 1).Height
                pag_msg.lab_msg(liv - 1).Top = pro_set.PercY / 100 * 150
            End If
          Next i
          If num_com_sch_tag > 1 Then
              pag_msg.lab_avv_1.Top = pag_msg.lab_msg(num_com_sch_tag - 1).Top + pag_msg.lab_msg(num_com_sch_tag - 1).Height + pro_set.PercY / 100 * 80
          Else
              pag_msg.lab_avv_1.Top = pag_msg.lab_msg(liv - 1).Top + pag_msg.lab_msg(liv - 1).Height + pro_set.PercY / 100 * 80
          End If
          pag_msg.Height = pag_msg.Height + pag_msg.lab_avv_1.Height + pro_set.PercY / 100 * 600
          Center pag_msg
          For i = 0 To 2
            pag_msg.lab_msg(i).Caption = ""
          Next i
      End If
      pag_msg.lab_msg(liv - 1).ForeColor = IIf(is_All, COL_ALL_Red, COL_DARK_Green)
      pag_msg.lab_msg(liv - 1).Caption = stri
      pag_msg.lab_msg(liv - 1).Left = (pag_msg.Width - pag_msg.lab_msg(liv - 1).Width) \ 2
  End If
End If
End Sub

'Function str_to_arr(rig As String, vet() As Integer, max_nel As Integer)
''Restituisce TRUE se non ci sono problemi
'Dim i As Integer
'Dim rig_loc As String
'Dim num As String
'
'rig_loc = rig
'i = 1
'str_to_arr = True
'While Len(rig_loc) > 0 And i <= max_nel
'  'scarto gli spazi
'  While Left$(rig_loc, 1) = " " And Len(rig_loc) > 0
'    rig_loc = Mid$(rig_loc, 2)
'  Wend
'  num = ""
'  While Left$(rig_loc, 1) <> " " And Len(rig_loc) > 0
'    num = num + Left$(rig_loc, 1)
'    rig_loc = Mid$(rig_loc, 2)
'  Wend
'  vet(i) = Val(num) * TODECMM
'  i = i + 1
'Wend
''controllo errori
'If i > max_nel Then
'  str_to_arr = False
'  log_str "str_to_arr: " + "Superata la dimensione massima dell'array", MED_PRIORITY, log_gen
'End If
'End Function


Function nib_bin(s As String) As String
'restituisce una stringa con la rappresentazione
'binaria su quattro cifre del numero esadecimale
'passato come parametro
  Select Case s
     Case "0": nib_bin = "0000"
     Case "1": nib_bin = "0001"
     Case "2": nib_bin = "0010"
     Case "3": nib_bin = "0011"
     Case "4": nib_bin = "0100"
     Case "5": nib_bin = "0101"
     Case "6": nib_bin = "0110"
     Case "7": nib_bin = "0111"
     Case "8": nib_bin = "1000"
     Case "9": nib_bin = "1001"
     Case "A": nib_bin = "1010"
     Case "B": nib_bin = "1011"
     Case "C": nib_bin = "1100"
     Case "D": nib_bin = "1101"
     Case "E": nib_bin = "1110"
     Case "F": nib_bin = "1111"
     Case Else
      nib_bin = ""
      log_str "HEX_BIN: valore errato " & s, MED_PRIORITY, log_gen
  End Select
End Function

Public Function Hex_Dec(ByVal s As String) As Double
'restituisce la conversione decimale della stringa passata, interpretata come esadecimale
Dim i As Long, res As Double
  res = 0
  For i = 1 To Len(s)
    res = res + val("&H" + Right$(s, 1)) * 16 ^ (i - 1)
    s = Mid$(s, 1, Len(s) - 1)
  Next i
  Hex_Dec = CDbl(res)
End Function

Function get_memory_info() As Long
'restituisce la memoria fisica del sistema
Dim ms As MEMORYSTATUS
  ms.dwLength = Len(ms)
  GlobalMemoryStatus ms
  get_memory_info = ms.dwTotalPhys
End Function

Sub disable_txt_and_combo(f As Form)
'disabilita i controlli TextBox e ComboBox per la form passata
Dim c As Control
  For Each c In f
    If TypeOf c Is TextBox Or TypeOf c Is ComboBox Then
      c.Enabled = False
    End If
  Next c
End Sub

Sub get_frm_ini(frm As Form, section As String, FIL As String)
' imposta nella form i controlli prendendoli dal .ini (gli item che hanno lo stesso nome)
' prende i controlli nella form e li imposta nel .ini ( gli item hanno lo stesso nome )
Dim c As Control
For Each c In frm.Controls
  If TypeOf c Is TextBox Then
    'c.Text = getFileINIString(section, Mid$(c.name, 5), "", FIL)
    c.Text = getFileINIString(section, Mid$(c.name, InStr(1, c.name, "_", vbBinaryCompare) + 1), "", FIL)
  ElseIf TypeOf c Is OptionButton Then
    'c.value = getFileINIString(section, Mid$(c.name, 5), "", FIL)
    c.Value = CInt(getFileINIString(section, Mid$(c.name, InStr(1, c.name, "_", vbBinaryCompare) + 1), "", FIL))
  ElseIf TypeOf c Is CheckBox Then
    'c.value = getFileINIString(section, Mid$(c.name, 5), "", FIL)
    c.Value = CInt(getFileINIString(section, Mid$(c.name, InStr(1, c.name, "_", vbBinaryCompare) + 1), "", FIL))
  ElseIf TypeOf c Is ComboBox Then
    c.ListIndex = CInt(getFileINIString(section, Mid$(c.name, InStr(1, c.name, "_", vbBinaryCompare) + 1), "0", FIL))
  End If
Next c

End Sub

Function getFileINIString$(ByVal section$, ByVal szItem$, ByVal szDefault$, Optional ByVal FName$)
  Const DEFAULT_VALUE = "DEFAULT_VALUE"
  Dim tmp As String
  Dim X As Integer
  Dim FileName As String
  If Len(FName$) = 0 Then
    FileName = app.Path + DIR_DATI + app.EXEName
  Else
    FileName = app.Path + DIR_DATI + FName$
  End If
  tmp = String$(2048, 32)
  X = GetPrivateProfileString(section$, szItem$, DEFAULT_VALUE, tmp, Len(tmp), FileName + ".ini")
  getFileINIString = Mid$(tmp, 1, X)
  If DEFAULT_VALUE = Mid$(tmp, 1, X) Then 'se leggo il valore di default riscrivo la voce nel file INI (per essere sicuro che tutte le voci vengano registrate)
    setFileINIString section$, szItem$, szDefault$, FName
    getFileINIString = szDefault$
  End If
End Function

Sub key_tab(shi As Boolean)
Dim scan As Integer
  If shi = False Then
    scan = MapVirtualKey(VK_TAB, 0)
    keybd_event VK_TAB, scan, 0, 0
    keybd_event VK_TAB, scan, KEYEVENTF_KEYUP, 0
  Else
    scan = MapVirtualKey(VK_SHIFT, 0)
    keybd_event VK_SHIFT, scan, 0, 0
    scan = MapVirtualKey(VK_TAB, 0)
    keybd_event VK_TAB, scan, 0, 0
    keybd_event VK_TAB, scan, KEYEVENTF_KEYUP, 0
    scan = MapVirtualKey(VK_SHIFT, 0)
    keybd_event VK_SHIFT, scan, KEYEVENTF_KEYUP, 0
  End If
End Sub

Sub key_END(shi As Boolean)
Dim scan As Integer
  If shi = False Then
    scan = MapVirtualKey(vbKeyEnd, 0)
    keybd_event vbKeyEnd, scan, 0, 0
    keybd_event vbKeyEnd, scan, KEYEVENTF_KEYUP, 0
  Else
    scan = MapVirtualKey(VK_SHIFT, 0)
    keybd_event VK_SHIFT, scan, 0, 0
    scan = MapVirtualKey(vbKeyEnd, 0)
    keybd_event vbKeyEnd, scan, 0, 0
    keybd_event vbKeyEnd, scan, KEYEVENTF_KEYUP, 0
    scan = MapVirtualKey(VK_SHIFT, 0)
    keybd_event VK_SHIFT, scan, KEYEVENTF_KEYUP, 0
  End If
End Sub






Public Sub set_str_len(s As String, lun As Long)
'aggiunge degli spazi in modo da rendere tutte le stringhe lunghe uguali
Dim k As Integer
  k = Len(s)
  If k < lun Then
    s = s + Space$(lun - k)
  End If
End Sub

Sub setFileINIString(ByVal section$, ByVal szItem$, ByVal szString$, Optional ByVal FName$)
  Dim X As Integer
  Dim FileName As String
  If Len(FName$) = 0 Then
    FileName = app.Path + DIR_DATI + app.EXEName
  Else
    FileName = app.Path + DIR_DATI + FName$
  End If
    X = WritePrivateProfileString(section$, szItem$, szString$, FileName$ + ".ini")
End Sub

Sub vis_for(f As Form, vis As Boolean)
' se vis e' true la visualizza, altrimenti la nasconde
  Dim r As Long
  Dim hwnd As Long
  Dim currRect As RECT
  Dim currWinP As WINDOWPLACEMENT
   
  hwnd = f.hwnd
   
  currWinP.Length = Len(currWinP)
  r = GetWindowPlacement(hwnd, currWinP)
  currWinP.flags = 0&
  If vis Then
    currWinP.showCmd = SW_SHOWNOACTIVATE
  Else
    currWinP.showCmd = SW_HIDE
  End If
  r = SetWindowPlacement(hwnd, currWinP)

End Sub

Function Ctr_ver_pre(ver1 As String, ver2 As String) As Boolean
'Andrea 01-03-02
'Restituisce True solo se la versione ver1 è precedente alla versione ver2, False altrimenti
Dim v(1 To 2) As String
Dim ind1(1 To 2) As Integer
Dim ind2(1 To 2) As Integer
Dim ind3(1 To 2) As Integer
Dim p(1 To 2) As Integer
Dim s(1 To 2) As Integer
Dim t(1 To 2) As Integer
Dim i As Integer
Ctr_ver_pre = False
v(1) = Trim(Rie_nul(ver1))
v(2) = Trim(Rie_nul(ver2))
If Not (is_ver(v(1)) And is_ver(v(2))) Then Exit Function
For i = 1 To 2
  ind1(i) = InStr(v(i), ".")
  If ind1(i) > 1 Then
    p(i) = val(Left(v(i), ind1(i) - 1))
  Else
    Exit Function
  End If
  ind2(i) = InStr(ind1(i) + 1, v(i), ".")
  If ind2(i) > (ind1(i) + 1) Then
    s(i) = val(Mid(v(i), ind1(i) + 1, ind2(i) - ind1(i) - 1))
  Else
    Exit Function
  End If
  ind3(i) = InStr(ind2(i) + 1, v(i), ".")
  If ind3(i) > 0 Then
    t(i) = val(Mid(v(i), ind2(i) + 1, ind3(i) - ind2(i) - 1))
  Else
    t(i) = val(Right(v(i), Len(v(i)) - ind2(i)))
  End If
Next i
If p(1) > p(2) Then
  Ctr_ver_pre = False
Else
  If p(1) < p(2) Then
    Ctr_ver_pre = True
  Else
    If s(1) > s(2) Then
      Ctr_ver_pre = False
    Else
      If s(1) < s(2) Then
        Ctr_ver_pre = True
      Else
        If t(1) > t(2) Then
          Ctr_ver_pre = False
        Else
          If t(1) < t(2) Then
            Ctr_ver_pre = True
          Else
            Ctr_ver_pre = False
          End If
        End If
      End If
    End If
  End If
End If
End Function

Function is_ver(VER As String) As Boolean
Dim v As String
Dim ind1 As Integer, ind2 As Integer, ind3 As Integer
Dim p As String, s As String, t As String
'andrea 01-03-02
'Restituisce True solo se ver ha il formato di una versione, cioè p.s.t, dove p, s e t sono tre numeri interi
  v = Trim(Rie_nul(VER))
  If Len(v) < 5 Then Exit Function
  ind1 = InStr(v, ".")
  If ind1 > 1 Then
    ind2 = InStr(ind1 + 1, v, ".")
    If ind2 > (ind1 + 1) Then
      ind3 = InStr(ind2 + 1, v, ".")
      p = Left(v, ind1 - 1)
      s = Mid(v, ind1 + 1, ind2 - ind1 - 1)
      If ind3 > 0 Then
        t = Mid(v, ind2 + 1, ind3 - ind2 - 1)
      Else
        t = Right(v, Len(v) - ind2)
      End If
      If IsNumeric(p) And IsNumeric(s) And IsNumeric(t) Then is_ver = True
    Else
      is_ver = False
    End If
  Else
    is_ver = False
  End If
End Function

Sub Salva_log(Optional auto As Boolean = False)
'arturo uff 26-11-02 (1) copia/incolla della funzione presa da SPDE
'andreacs 17-03-01 aggiunta questa funzione
Const MAX_NUM_DIR_LOG = 5
Dim cnt_dir_log As Integer
Dim nom_dir As String, path_dir As String, nom_fil As String
Dim Data_file As Date, Data_oldest As Date
Dim nome_oldest As String
Static running As Boolean
On Local Error GoTo g_err
  If running Then Exit Sub
  running = True
  messaggio "copia in corso...", False
  nom_dir = "Log" + Format$(Now, "dd-mm-yy_hh.mm")
  path_dir = pro_set.pro_pat + nom_dir
  If (Not auto) Then
    'Controllo se ci sono troppe cartelle di log vecchie: se sì, le cancello
    cnt_dir_log = 0
    Data_oldest = 0
    nom_fil = Dir$(pro_set.pro_pat + "Log*", vbDirectory)
    Do While nom_fil <> ""
      If Len(nom_fil) <> 17 Then Exit Do
      Data_file = Mid(nom_fil, Len(nom_fil) - 13, 8) + " " + Right(nom_fil, 5)
      If Not IsDate(Data_file) Then Exit Do
      cnt_dir_log = cnt_dir_log + 1
      If DateDiff("n", Data_oldest, 0) <> 0 Then
        If DateDiff("n", Data_file, Data_oldest) > 0 Then
          Data_oldest = Data_file
          nome_oldest = nom_fil
        End If
      Else
        Data_oldest = Data_file
        nome_oldest = nom_fil
      End If
      nom_fil = Dir$(, vbDirectory)
    Loop
    If cnt_dir_log >= MAX_NUM_DIR_LOG Then
      Kill pro_set.pro_pat + nome_oldest + "\*.*"
      RmDir pro_set.pro_pat + nome_oldest
    End If
  End If
  If Dir$(path_dir, vbDirectory) = "" Then
    MkDir path_dir
  End If
  nom_fil = Dir$(pro_set.pro_pat + "*.log")
  While nom_fil <> ""
    If InStr(LCase(nom_fil), "copia") = 0 And InStr(LCase(nom_fil), "copy") = 0 Then
      If (Not auto) Or LCase(nom_fil) = "sta_plc.log" Then
        FileCopy pro_set.pro_pat + nom_fil, path_dir + "\" + nom_fil
        DoEvents
      End If
    End If
    nom_fil = Dir$
  Wend
  nom_fil = Dir$(pro_set.pro_pat + "*.dat")
  While nom_fil <> ""
    FileCopy pro_set.pro_pat + nom_fil, path_dir + "\" + nom_fil
    DoEvents
    nom_fil = Dir$
  Wend
  If (Not auto) Then
    nom_fil = Dir$(pro_set.pro_pat + "*.ini")
    While nom_fil <> ""
      FileCopy pro_set.pro_pat + nom_fil, path_dir + "\" + nom_fil
      DoEvents
      nom_fil = Dir$
    Wend
  End If
  messaggio "Log files copiati", False
  running = False
  Exit Sub
g_err:
messaggio "Errore durante la copia dei Log", True
Resume Next
End Sub
Sub disable_frames(for_att As Form)
Dim c As Control
For Each c In for_att
' Arturo uff 05-11-02 x disabilitazione controlli su frame
'  If TypeOf c Is SSPanel Or TypeOf c Is Frame Then
  If TypeOf c Is SSPanel Or TypeOf c Is Frame Or TypeOf c Is ComboBox Or TypeOf c Is TextBox Or TypeOf c Is DBCombo Then
    c.Enabled = False
  ElseIf TypeOf c Is CheckBox Then
    If c.Value = vbChecked Then c.Value = vbGrayed
  End If
Next c
End Sub

Sub clear_cell(for_att As Form)
Dim c As Control
  For Each c In for_att
    If TypeOf c Is data Then c.RecordSource = ""
    If TypeOf c Is TextBox Or TypeOf c Is DBCombo Then
      c.Text = ""
    ElseIf TypeOf c Is ComboBox Then
      c.ListIndex = -1
    End If
  Next c
End Sub

Sub enable_frames(for_att As Form)
Dim c As Control
For Each c In for_att
' Arturo uff 05-11-02 x disabilitazione controlli su frame
'  If TypeOf c Is SSPanel Or TypeOf c Is Frame Then
  If TypeOf c Is SSPanel Or TypeOf c Is Frame Or TypeOf c Is ComboBox Or TypeOf c Is TextBox Or TypeOf c Is DBCombo Then
    c.Enabled = True
  ElseIf TypeOf c Is CheckBox Then
    If c.Value = vbGrayed Then c.Value = vbChecked
  End If
Next c
End Sub

Sub enable_tab(for_att As Form)
Dim c As Control
For Each c In for_att
  If TypeOf c Is TextBox Or TypeOf c Is ComboBox Or TypeOf c Is CheckBox Then
    c.TabStop = True
  End If
Next c
End Sub

Sub enable_txt_pag_sch_tag(f As Form)
Dim c As Control
For Each c In f
  If TypeOf c Is TextBox And Right$(c.name, 3) = "att" Then
    c.TabStop = True
  End If
Next c
End Sub
Sub del_txt_frm_deb(f As Form)
Dim c As Control
For Each c In f
  If TypeOf c Is TextBox And Right$(c.name, 2) = "dm" Then
    c.Text = ""
  End If
Next c
End Sub
Sub disable_tab(for_att As Form)
Dim c As Control
For Each c In for_att
  If TypeOf c Is TextBox Or TypeOf c Is ComboBox Or TypeOf c Is CheckBox Then
    c.TabStop = False
  End If
Next c
End Sub

Function arr_to_str(arr, ini As Integer, fin As Integer) As String
'restituisce una stringa contenente l'array convertito a partire da ini fino a fin
Dim i As Integer
Dim str As String
str = ""
For i = ini To fin
  str = str + Format$(arr(i)) + " "
Next i
arr_to_str = str
End Function

Sub ges_vbkeydbCombo(KeyCode As Integer, Shift As Integer)
'Ridirige i tasti freccia per la gestione dei dbCombo
  Select Case KeyCode
    Case vbKeyLeft
      KeyCode = vbKeyUp
    Case vbKeyRight
      KeyCode = vbKeyDown
    Case vbKeyF4
      KeyCode = 0 'annullo il tasto per evitare la comparsa dell'elenco a discesa
  End Select
End Sub

'Sub dec_int(n As Integer)
''decrementa ciclicamente un intero
'  n = IIf(n = 0, MAXINT, n - 1)
'End Sub

Function get_ind_lis(con As ComboBox, str As String) As Integer
'restituisce l'indice dell'item str in un ComboBox
Dim i As Integer
If Len(Trim$(str)) = 0 Then
  get_ind_lis = -1
  Exit Function
End If
i = 0
Do While i < con.ListCount
  con.ListIndex = i
  If con.Text = Trim$(str) Then Exit Do
  i = i + 1
Loop
If i = con.ListCount Then
  get_ind_lis = -1
Else
  get_ind_lis = i
End If

End Function

Function get_num(KeyAscii As Integer, Optional accetta_punto As Boolean = False, Optional accetta_segno = False) As Integer

If abi_mis_ang Then
  If (KeyAscii > 47 And KeyAscii < 58) Or KeyAscii = vbKeyBack Or ((KeyAscii = vbKeyDecimal Or KeyAscii = 46) And accetta_punto) Or (KeyAscii = 45 And accetta_segno) Then
    get_num = KeyAscii
  Else
    Beep
    get_num = 0    'accetto solo numeri
  End If
Else
  If (KeyAscii > 47 And KeyAscii < 58) Or KeyAscii = vbKeyBack Or (KeyAscii = 45 And accetta_segno) Then
    get_num = KeyAscii
  Else
    Beep
    get_num = 0    'accetto solo numeri
  End If
End If

End Function



'Sub inc_int(n As Integer)   'incrementa ciclicamente un intero
'  n = IIf(n = MAXINT, 1, n + 1)
'End Sub

Function Max_num(n1, n2) As Variant
  Max_num = IIf(n1 > n2, n1, n2)
End Function

Function Min_num(n1, n2) As Variant
 Min_num = IIf(n1 < n2, n1, n2)
End Function

Sub DX_align(dest As String, src As String)
'scrive in dest la stringa src allineata a destra
  If Len(dest) > Len(Trim$(src)) Then
    dest = Space$(Len(dest) - Len(Trim$(src))) + Trim$(src)
  Else
    dest = src
  End If
End Sub

