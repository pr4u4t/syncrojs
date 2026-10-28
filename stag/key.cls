VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "key"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Public Event F1(Shift As Integer)
Public Event F2(Shift As Integer)
Public Event F3(Shift As Integer)
Public Event F4(Shift As Integer)
Public Event F5(Shift As Integer)
Public Event F6(Shift As Integer)
Public Event F7(Shift As Integer)
Public Event F8(Shift As Integer)
Public Event F9(Shift As Integer)
Public Event F10(Shift As Integer)
Public Event F11(Shift As Integer)
Public Event F12(Shift As Integer)
Public Event esc(Shift As Integer)
Private WithEvents frm As Form
Attribute frm.VB_VarHelpID = -1
Private Key_Shift As Integer 'memorizza la maschera corrente dei tasti CTRL,SHIFT e ALT
Public Sub create(f As Form)
' crea
  Set frm = f
  f.KeyPreview = True
End Sub


 Sub chg(ByVal nuo As Form, Optional keep)
' carica la form f e mostra le descrizioni dei tasti
  If (pag_cor.name <> "Pwd_frm") And (nuo.name = "Pwd_frm") Then
    for_pre_pwd = CStr(pag_cor.name)
  End If
  Set pag_pre = pag_cor 'memorizzo la pagina chiamante
  Set pag_cor = nuo 'memorizzo la nuova pagina corrente
  pag_cor.Tag = nuo.name 'Uso il capo tag per l'associaizione tra form e descrizione dei tasti funzione
  nuo.show
  nuo.SetFocus
  If IsMissing(keep) Then
    frm.Hide
  End If
  If pag_pre.Tag = "Pwd_frm" Then
    Show_tas_des for_pre_pwd, Key_Shift
  Else
    Show_tas_des pag_cor.Tag, Key_Shift
  End If
End Sub
Private Sub Ges_Key(KeyCode As Integer, Shift As Integer)
Dim f As Form
Dim szbuff As String * 100
Dim i As Integer
'IDisperati_FI 17/08/2006
Const TEMP_FOLDER As String = "TEMP"
Dim ret_val As cod_ret

    On Error GoTo ges_err_ges_key
    Select Case KeyCode
      
      Case vbKeyI                             'Tasto per passare da mm a inch e viceversa
        If Shift = vbShiftMask + vbCtrlMask Then
          If sta_tag.com_OK Then
            abi_mis_ang = IIf(abi_mis_ang, False, True)
            setFileINIString "pro_set", "Inch", IIf(abi_mis_ang, "1", "0")
            'Iciliouff 03-11-2003
'            snd_str_PLC PARAM_CFG_MAC
            messaggio IIf(abi_mis_ang, msg("mis_ang_abi"), msg("mis_ang_dis")), False, False
            If Not sta_bar.rmt_sta_bar_plc.esegui(tip_ope_PLC.PARAM_CFG_MAC, GetTickCount(), 0, 0) Then
              'Rollback dell'impostazione
              abi_mis_ang = IIf(abi_mis_ang, False, True)
              setFileINIString "pro_set", "Inch", IIf(abi_mis_ang, "1", "0")
              messaggio msg("imp_ese_ope"), True
            End If
          End If
        Else
          GoTo Fine
        End If
      Case vbKeyL   'arturo uff 26-11-02 (1)
        If Shift = vbShiftMask + vbCtrlMask Then
'          Salva_log
          'IDisperati_FI 21/08/2006
          'Archiviazione locale
          If Len(pro_set.pro_bck_loc_zip) > 0 Then
            If TestAndCreateDir(pro_set.pro_bck_loc_zip) = RET_OK Then
              ret_val = CreateZIP("LOG", pro_set.JOB, pro_set.Commessa, pro_set.pro_pat & pro_set.nam_fil_log_zip, pro_set.pro_bck_loc_zip)
              messaggio IIf(ret_val = RET_OK, msg("ope_eff"), msg("imp_ese_ope")), is_All:=IIf(ret_val = RET_BAD, True, False)
            Else
              messaggio msg("imp_ese_ope"), is_All:=True
            End If
          Else
            messaggio msg("imp_ese_ope"), is_All:=True
          End If
          'Archiviazione remota dei file di LOG
          If Len(pro_set.pro_bck_rem_zip) > 0 Then
            If TestAndCreateDir(pro_set.pro_bck_rem_zip) = RET_OK Then
              ret_val = CreateZIP("LOG", pro_set.JOB, pro_set.Commessa, pro_set.pro_pat & pro_set.nam_fil_log_zip, pro_set.pro_bck_rem_zip)
            End If
          End If
        Else
          GoTo Fine
        End If
          
      Case vbKeyD
        'IDisperati_FI 17/08/2006
        If Shift = vbShiftMask + vbCtrlMask Then
          'Archiviazione locale
          If Len(pro_set.pro_bck_loc_zip) > 0 Then
            If TestAndCreateDir(pro_set.pro_bck_loc_zip) = RET_OK Then
              If CopyFolderFiles(Left(pro_set.pro_pat, Len(pro_set.pro_pat) - 1), pro_set.pro_bck_loc_zip & "\" & TEMP_FOLDER) = RET_OK Then
                ret_val = CreateZIP("DAT", pro_set.JOB, pro_set.Commessa, pro_set.pro_pat & pro_set.nam_fil_dat_zip, pro_set.pro_bck_loc_zip)
                messaggio IIf(ret_val = RET_OK, msg("ope_eff"), msg("imp_ese_ope")), is_All:=IIf(ret_val = RET_BAD, True, False)
              Else
                messaggio msg("imp_ese_ope"), is_All:=True
              End If
            Else
              messaggio msg("imp_ese_ope"), is_All:=True
            End If
          Else
            messaggio msg("imp_ese_ope"), is_All:=True
          End If
        End If
      Case vbKeyM 'tasto di settaggio STAG in manuale
        'If Shift = vbShiftMask + vbCtrlMask + vbAltMask Then
        '     set_tag_man
        'Else
        '  GoTo Fine
        'End If
     
      Case vbKeyO 'tasto di shift della lista
        If Shift = vbShiftMask + vbCtrlMask Then
          'Arturo uff 04-05-2004
          If pro_set.ute_wrk Then
            If ord_mac.Count >= 1 Then Shifta
          End If
        End If
        
      Case vbKeyP 'tasto password
        If Shift = vbShiftMask + vbCtrlMask Then
          chg Pwd_frm, True
        Else
          GoTo Fine
        End If
      
      Case vbKeyQ 'tasto di shift lista
        'If Shift = vbShiftMask + vbCtrlMask Then
        '  Shifta
        'Else
        '  GoTo Fine
        'End If
     
      Case vbKeyX 'tasto di uscita dal programma
        If Shift = vbShiftMask + vbCtrlMask Then
              messaggio "please wait: STAG is shutting down...", is_All:=True, stay:=True
              CAN_result = CAN_CloseAll
              If CAN_result <> CAN_ERR_OK Then
                CAN_result = CAN_GetErrText(CAN_result, szbuff)
                log_str "Ges_Key: Shut down.  CAN_CloseAll: " + szbuff, MED_PRIORITY, log_can
              End If
              ord_mac_ese.Fine
              ord_mac.Fine
              If sta_bar.com_spde_ser.PortOpen Then sta_bar.com_spde_ser.PortOpen = False
              For i = 1 To par_mac.car_tec.num_liv_tag
                If sta_bar.com_sch_tag(i - 1).PortOpen Then sta_bar.com_sch_tag(i - 1).PortOpen = False
              Next i
              ' ABorelli_FI09/09/2005_1
              scrivi_pro
              sta_bar.Tim_all.Enabled = False
              sta_bar.tim_com_azi.Enabled = False
              sta_bar.Tim_com_PLC.Enabled = False
              sta_bar.tim_com_spde.Enabled = False
              'IDisperati_FI 17/08/2006
              DeleteFolder pro_set.pro_bck_loc_zip & "\" & TEMP_FOLDER
              DisableBlocNum
              Termina
              For Each f In Forms
                Unload f
              Next
              End
        Else
          GoTo Fine
        End If
      
      Case vbKeyS
        'IDisperati_FI 17/08/2006
        If Shift = vbShiftMask + vbCtrlMask Then
          'Solo Archiviazione locale
          If Len(pro_set.pro_bck_loc_zip) > 0 Then
            If TestAndCreateDir(pro_set.pro_bck_loc_zip) = RET_OK Then
              ret_val = CreateZIP("COD", pro_set.JOB, pro_set.Commessa, pro_set.pro_pat & pro_set.nam_fil_cod_zip, pro_set.pro_bck_loc_zip)
              messaggio IIf(ret_val = RET_OK, msg("ope_eff"), msg("imp_ese_ope")), is_All:=IIf(ret_val = RET_BAD, True, False)
            Else
              messaggio msg("imp_ese_ope"), is_All:=True
            End If
          Else
            messaggio msg("imp_ese_ope"), is_All:=True
          End If
        End If
'         If Shift = vbShiftMask + vbCtrlMask Then
'           If Dir$(Left$(pro_set.pro_pat, Len(pro_set.pro_pat) - Len(DIR_DATI) + 1) + "Shutcmd.exe ") <> "" Then
'              messaggio "please wait: STAG is shutting down...", is_All:=True, stay:=True
'              CAN_result = CAN_CloseAll
'              If CAN_result <> CAN_ERR_OK Then
'                CAN_result = CAN_GetErrText(CAN_result, szbuff)
'                log_str "Ges_Key: Shut down.  CAN_CloseAll: " + szbuff, MED_PRIORITY, log_can
'              End If
'              ord_mac_ese.Fine
'              ord_mac.Fine
'              If sta_bar.com_spde_ser.PortOpen Then sta_bar.com_spde_ser.PortOpen = False
'              For i = 1 To par_mac.car_tec.num_liv_tag
'                If sta_bar.com_sch_tag(i - 1).PortOpen Then sta_bar.com_sch_tag(i - 1).PortOpen = False
'              Next i
'              sta_bar.Tim_all.Enabled = False
'              sta_bar.tim_com_azi.Enabled = False
'              sta_bar.Tim_com_PLC.Enabled = False
'              sta_bar.tim_com_spde.Enabled = False
'              Shell (Left$(pro_set.pro_pat, Len(pro_set.pro_pat) - Len(DIR_DATI) + 1) + "Shutcmd.exe /L /Y /C /T:10 " + "'" + msg("sht_dwn") + "'")
'           Else
'             messaggio msg("imp_tro") + Left$(pro_set.pro_pat, Len(pro_set.pro_pat) - Len(DIR_DATI) + 1) + "Shutcmd.exe!", True
'             GoTo Fine
'           End If
'         Else
'           GoTo Fine
'         End If
     
      Case vbKeyEscape
        RaiseEvent esc(Shift)
      Case vbKeyF1
        RaiseEvent F1(Shift)
      Case vbKeyF2
        RaiseEvent F2(Shift)
      Case vbKeyF3
        RaiseEvent F3(Shift)
      Case vbKeyF4
        RaiseEvent F4(Shift)
      Case vbKeyF5
        RaiseEvent F5(Shift)
      Case vbKeyF6
        RaiseEvent F6(Shift)
      Case vbKeyF7
        RaiseEvent F7(Shift)
      Case vbKeyF8
        RaiseEvent F8(Shift)
      Case vbKeyF9
        RaiseEvent F9(Shift)
      Case vbKeyF10
        RaiseEvent F10(Shift)
      Case vbKeyF11
        RaiseEvent F11(Shift)
      Case vbKeyF12
        RaiseEvent F12(Shift)
      Case Else
        GoTo Fine
    End Select
    KeyCode = 0 'annullo il tasto
    
Exit Sub
ges_err_ges_key:
If err.Number = 53 Then
  Resume Next
End If
Exit Sub
Fine:
End Sub


Sub frm_KeyDown(KeyCode As Integer, Shift As Integer)
Static ini As Boolean
  On Error Resume Next
  'controllo sull'allineamento delle variabili e della form attiva
  If Not (frm.name = pag_cor.name And frm.name = Screen.ActiveForm.name) And frm.name <> "pag_msg" _
   And frm.name <> "leg_cfg" And frm.name <> "del_cfg" And frm.name <> "dia_par_new_des" Then
    log_str "ERRORE di disallineamento form :", MED_PRIORITY, log_prob
    log_str "       frm.name= " + frm.name, MED_PRIORITY, log_prob
    log_str "       pag_cor.name= " + pag_cor.name, MED_PRIORITY, log_prob
    log_str "       screen.activeform.name= " + Screen.ActiveForm.name, MED_PRIORITY, log_prob
    log_str "       pag_pre.name= " + pag_pre.name, MED_PRIORITY, log_prob
  End If
  If Shift <> Key_Shift Or ini = False Then
    ini = True
    Key_Shift = Shift 'memorizzo lo stato attuale del tasto shift
    Show_tas_des pag_cor.Tag, Shift
  End If
  Ges_Key KeyCode, Shift
End Sub

Private Sub frm_KeyUp(KeyCode As Integer, Shift As Integer)
  If Shift <> Key_Shift Then
    Key_Shift = Shift 'memorizzo lo stato attuale del tasto shift
    Show_tas_des pag_cor.Tag, Shift
  End If
End Sub

