Attribute VB_Name = "ges_porte"
Option Explicit

Sub ini_porte_SCHEDE_TAGLIO()
Dim i As Integer
On Local Error GoTo STAG_ges_err

  For i = 1 To par_mac.car_tec.num_liv_tag
    'agrassi_FI08-03-2010_1
    If par_mac.tip_com_sch_tag(i) = 0 Then
      If sta_bar.com_sch_tag(i - 1).PortOpen Then
        sta_bar.com_sch_tag(i - 1).PortOpen = False
      End If
      If Trim$(par_mac.com_sch_tag(i).baud) <> "" And Trim$(par_mac.com_sch_tag(i).bit_par) <> "" _
        And Trim$(par_mac.com_sch_tag(i).dati) <> "" And Trim$(par_mac.com_sch_tag(i).bit_stop) <> "" Then
          sta_bar.com_sch_tag(i - 1).Settings = Format$(par_mac.com_sch_tag(i).baud) + "," + Format$(par_mac.com_sch_tag(i).bit_par) + "," + Format$(par_mac.com_sch_tag(i).dati) + "," + Format$(par_mac.com_sch_tag(i).bit_stop)
          sta_bar.com_sch_tag(i - 1).CommPort = par_mac.com_sch_tag(i).porta
          sta_bar.com_sch_tag(i - 1).PortOpen = True
          sta_bar.com_sch_tag(i - 1).OutBufferCount = 0
      End If
    'agrassi_FI08-03-2010_1
    ElseIf par_mac.tip_com_sch_tag(i) = 1 Then
      If sta_bar.sck_sch_tag(i - 1).State <> sckClosed Then sta_bar.sck_sch_tag(i - 1).Close
      If Trim(par_mac.lan_sch_tag(i).por_loc) <> "" And Trim(par_mac.lan_sch_tag(i).por_rem) <> "" And Trim(par_mac.lan_sch_tag(i).ind_rem) <> "" Then
        sta_bar.sck_sch_tag(i - 1).LocalPort = par_mac.lan_sch_tag(i).por_loc
        sta_bar.sck_sch_tag(i - 1).RemotePort = par_mac.lan_sch_tag(i).por_rem
        sta_bar.sck_sch_tag(i - 1).RemoteHost = par_mac.lan_sch_tag(i).ind_rem
        'MLAMIONI_PMASSARO_FI27/10/2010 - Gestione comunicazione UDP-TCP per schede di taglio
        'sta_bar.sck_sch_tag(i - 1).Protocol = sckUDPProtocol
        If par_mac.lan_sch_tag_UDP(i) = True Then
          sta_bar.sck_sch_tag(i - 1).Protocol = sckUDPProtocol
        Else
          sta_bar.sck_sch_tag(i - 1).Protocol = sckTCPProtocol
        End If
      End If
    End If
  Next i

For i = par_mac.car_tec.num_liv_tag + 1 To 3
    If sta_bar.com_sch_tag(i - 1).PortOpen Then sta_bar.com_sch_tag(i - 1).PortOpen = False
Next i

Exit Sub
STAG_ges_err:
Select Case err.Number
    Case 8005 'apertura di una porta occupata da altri
      log_str "on error intervenuta in ini_porte_SCHEDE_TAGLIO del modulo ges_porte" + ": " + err.Description + " Porta num. " + Format$(sta_bar.com_sch_tag(i - 1).CommPort) + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
      Resume Next
    Case 8002 'tentativo di apertura di una porta non presente
      log_str "on error intervenuta in ini_porte_SCHEDE_TAGLIO del modulo ges_porte" + ": " + err.Description + " Porta num. " + Format$(sta_bar.com_sch_tag(i - 1).CommPort) + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
      Resume Next
    Case Else
      log_str "on error intervenuta in ini_porte_SCHEDE_TAGLIO del modulo ges_porte" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
End Select
End Sub


Sub ini_porte_SPDE()

On Local Error GoTo STAG_ges_err

'se la comunicazione con SPDE è di tipo seriale
'apro la porta seriale per comunicare con il punto unico
If par_mac.com_spde.tip_com = "0" Then
    If sta_bar.com_spde_ser.PortOpen Then sta_bar.com_spde_ser.PortOpen = False
    sta_bar.com_spde_ser.Settings = Format$(par_mac.com_spde.tip_porta.baud) + "," + Format$(par_mac.com_spde.tip_porta.bit_par) + "," + Format$(par_mac.com_spde.tip_porta.dati) + "," + Format$(par_mac.com_spde.tip_porta.bit_stop)
    sta_bar.com_spde_ser.CommPort = par_mac.com_spde.tip_porta.porta
    sta_bar.com_spde_ser.PortOpen = True
    sta_bar.com_spde_ser.OutBufferCount = 0
    Set par_mac.com_spde.con = sta_bar.com_spde_ser
Else  'comunicazione su rete
    sta_bar.com_spde_lan.Close
    sta_bar.com_spde_lan.RemoteHost = par_mac.com_spde.lan.ind_rem
    sta_bar.com_spde_lan.LocalPort = par_mac.com_spde.lan.por_loc
    sta_bar.com_spde_lan.RemotePort = par_mac.com_spde.lan.por_rem
    par_mac.com_spde.lan.tip = PEER
    sta_bar.com_spde_lan.Protocol = sckUDPProtocol
    Set par_mac.com_spde.con = sta_bar.com_spde_lan
'    sta_bar.com_spde_lan.Bind par_mac.com_spde.lan.por_loc
    sta_bar.Tim_com_spde_lan.Enabled = True
End If

Exit Sub

STAG_ges_err:
Select Case err.Number
    Case 8005 'apertura di una porta occupata da altri
      log_str "on error intervenuta in ini_porte_SPDE del modulo ges_porte" + ": " + err.Description + " Porta num. " + Format$(sta_bar.com_spde_ser.CommPort) + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
    Case 8002 'tentativo di apertura di una porta non presente
      log_str "on error intervenuta in ini_porte_SPDE del modulo ges_porte" + ": " + err.Description + " Porta num. " + Format$(sta_bar.com_spde_ser.CommPort) + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
    Case Else
      log_str "on error intervenuta in ini_porte_SPDE del modulo ges_porte" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
End Select

End Sub


Function porte_chg() As Boolean
Dim i As Integer
Dim str_ute As String
Dim str As String
Dim str_sch_tag_ute As String
Dim str_sch_tag As String

porte_chg = False

If par_mac_ute.com_spde.tip_com <> par_mac.com_spde.tip_com Then
    porte_chg = True
    Exit Function
End If
If par_mac_ute.com_spde.tip_com = "0" And (par_mac_ute.com_spde.tip_porta.baud <> par_mac.com_spde.tip_porta.baud Or _
  par_mac_ute.com_spde.tip_porta.bit_par <> par_mac.com_spde.tip_porta.bit_par Or _
  par_mac_ute.com_spde.tip_porta.bit_stop <> par_mac.com_spde.tip_porta.bit_stop Or _
  par_mac_ute.com_spde.tip_porta.dati <> par_mac.com_spde.tip_porta.dati Or _
  par_mac_ute.com_spde.tip_porta.porta <> par_mac.com_spde.tip_porta.porta) Then
      porte_chg = True
      Exit Function
End If
If par_mac_ute.com_spde.tip_com = "1" And (par_mac_ute.com_spde.lan.ind_rem <> par_mac.com_spde.lan.ind_rem Or _
  par_mac_ute.com_spde.lan.por_loc <> par_mac.com_spde.lan.por_loc Or _
  par_mac_ute.com_spde.lan.por_rem <> par_mac.com_spde.lan.por_rem Or _
  par_mac_ute.com_spde.lan.tip <> par_mac.com_spde.lan.tip) Then
      porte_chg = True
      Exit Function
End If

For i = 1 To par_mac.car_tec.num_liv_tag
    'MLAMIONI_PMASSARO_FI27/10/2010 - Gestione comunicazione UDP-TCP per schede di taglio
    'Aggiunto nell'IF la riga "par_mac_ute.lan_sch_tag_UDP(i) <> par_mac.lan_sch_tag_UDP(i)"
    
      'agrassi_FI08-03-2010_1
    If par_mac_ute.com_sch_tag(i).baud <> par_mac.com_sch_tag(i).baud Or _
      par_mac_ute.com_sch_tag(i).bit_par <> par_mac.com_sch_tag(i).bit_par Or _
      par_mac_ute.com_sch_tag(i).bit_stop <> par_mac.com_sch_tag(i).bit_stop Or _
      par_mac_ute.com_sch_tag(i).dati <> par_mac.com_sch_tag(i).dati Or _
      par_mac_ute.com_sch_tag(i).porta <> par_mac.com_sch_tag(i).porta Or _
      par_mac_ute.tip_com_sch_tag(i) <> par_mac.tip_com_sch_tag(i) Or _
      par_mac_ute.lan_sch_tag(i).ind_rem <> par_mac.lan_sch_tag(i).ind_rem Or _
      par_mac_ute.lan_sch_tag(i).por_loc <> par_mac.lan_sch_tag(i).por_loc Or _
      par_mac_ute.lan_sch_tag(i).por_rem <> par_mac.lan_sch_tag(i).por_rem Or _
      par_mac_ute.lan_sch_tag_UDP(i) <> par_mac.lan_sch_tag_UDP(i) _
    Then
        porte_chg = True
        Exit Function
    End If
Next i
End Function

Function set_porte() As tip_param_errati_su_com
Dim i As Integer
Dim spde As Boolean
On Local Error GoTo ges_err_porte

spde = True

If par_mac_ute.com_spde.tip_com = "0" Then
  If par_mac.com_spde.tip_com = "1" Then sta_bar.com_spde_lan.Close
  If sta_bar.com_spde_ser.PortOpen Then sta_bar.com_spde_ser.PortOpen = False
  sta_bar.com_spde_ser.Settings = Trim$(par_mac_ute.com_spde.tip_porta.baud) + "," + Trim$(par_mac_ute.com_spde.tip_porta.bit_par) + "," + Trim$(par_mac_ute.com_spde.tip_porta.dati) + "," + Trim$(par_mac_ute.com_spde.tip_porta.bit_stop)
  sta_bar.com_spde_ser.CommPort = par_mac_ute.com_spde.tip_porta.porta
  sta_bar.com_spde_ser.PortOpen = True
  sta_bar.com_spde_ser.OutBufferCount = 0
  Set par_mac_ute.com_spde.con = sta_bar.com_spde_ser
End If
If par_mac_ute.com_spde.tip_com = "1" Then
    If par_mac.com_spde.tip_com = "0" Then
      If sta_bar.com_spde_ser.PortOpen Then sta_bar.com_spde_ser.PortOpen = False
    End If
    sta_bar.com_spde_lan.Close
    sta_bar.com_spde_lan.RemoteHost = par_mac_ute.com_spde.lan.ind_rem
    sta_bar.com_spde_lan.LocalPort = val(par_mac_ute.com_spde.lan.por_loc)
    sta_bar.com_spde_lan.RemotePort = val(par_mac_ute.com_spde.lan.por_rem)
    par_mac.com_spde.lan.tip = PEER
    sta_bar.com_spde_lan.Protocol = sckUDPProtocol
    Set par_mac_ute.com_spde.con = sta_bar.com_spde_lan
    'sta_bar.com_spde_lan.Bind par_mac_ute.com_spde.lan.por_loc
    sta_bar.Tim_com_spde_lan.Enabled = True
End If
spde = False

For i = 1 To par_mac.car_tec.num_liv_tag
    If sta_bar.com_sch_tag(i - 1).PortOpen Then sta_bar.com_sch_tag(i - 1).PortOpen = False
    'agrassi_FI08-03-2010_1
    If sta_bar.sck_sch_tag(i - 1).State <> sckClosed Then sta_bar.sck_sch_tag(i - 1).Close
Next i
For i = 1 To par_mac.car_tec.num_liv_tag
  'agrassi_FI08-03-2010_1
  If par_mac_ute.tip_com_sch_tag(i) = 0 Then
    sta_bar.com_sch_tag(i - 1).Settings = Format$(par_mac_ute.com_sch_tag(i).baud) + "," + Format$(par_mac_ute.com_sch_tag(i).bit_par) + "," + Format$(par_mac_ute.com_sch_tag(i).dati) + "," + Format$(par_mac_ute.com_sch_tag(i).bit_stop)
    sta_bar.com_sch_tag(i - 1).CommPort = par_mac_ute.com_sch_tag(i).porta
    sta_bar.com_sch_tag(i - 1).PortOpen = True
    sta_bar.com_sch_tag(i - 1).OutBufferCount = 0
  'agrassi_FI08-03-2010_1
  ElseIf par_mac_ute.tip_com_sch_tag(i) = 1 Then
    sta_bar.sck_sch_tag(i - 1).LocalPort = par_mac_ute.lan_sch_tag(i).por_loc
    sta_bar.sck_sch_tag(i - 1).RemotePort = par_mac_ute.lan_sch_tag(i).por_rem
    sta_bar.sck_sch_tag(i - 1).RemoteHost = par_mac_ute.lan_sch_tag(i).ind_rem
  End If
Next i
set_porte = NESSUN_PROBLEMA

Exit Function

ges_err_porte:

If spde Then
  set_porte = PUNTO_UNICO
Else
  set_porte = SCHEDE_DI_TAGLIO
End If

Select Case err.Number
  Case 8015
    messaggio msg("par_com_non_val"), True, False
  Case 8002 'tentativo di apertura di una porta non presente
    If Not spde Then
      messaggio msg("msg_por") & " " & CStr(sta_bar.com_sch_tag(i - 1).CommPort) & " " & msg("por_non_pre"), True
    Else
      messaggio msg("msg_por") & " " & CStr(sta_bar.com_spde_ser.CommPort) & " " & msg("por_non_pre"), True
    End If
  Case 8005 'tentativo di aprire una porta già aperta
    If Not spde Then
      messaggio msg("msg_por") & " " & CStr(sta_bar.com_sch_tag(i - 1).CommPort) & " " & msg("por_ape"), True
    Else
      messaggio msg("msg_por") & " " & CStr(sta_bar.com_spde_ser.CommPort) & " " & msg("por_ape"), True
    End If
  Case Else
    log_str "on error intervenuta in set_porte del modulo ges_porte" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
End Select

End Function

Function porte_ok() As Boolean
Dim i As Integer
Dim m As Integer

porte_ok = False
If par_mac_ute.com_spde.tip_com = "0" Then
    If par_mac_ute.com_spde.tip_porta.porta < 1 Or par_mac_ute.com_spde.tip_porta.porta > 6 Then
      Exit Function
    End If
End If
For i = 1 To par_mac.car_tec.num_liv_tag
  If par_mac_ute.com_sch_tag(i).porta < 1 Or par_mac_ute.com_sch_tag(i).porta > 6 Then
    Exit Function
  End If
Next i

If par_mac_ute.com_spde.tip_com = "0" Then
  For i = 1 To par_mac.car_tec.num_liv_tag
    If par_mac_ute.com_spde.tip_porta.porta = par_mac_ute.com_sch_tag(i).porta Then
      Exit Function
    End If
  Next i
End If

For i = 1 To par_mac.car_tec.num_liv_tag - 1
  For m = i + 1 To par_mac.car_tec.num_liv_tag
    If par_mac_ute.com_sch_tag(i).porta = par_mac_ute.com_sch_tag(m).porta Then
      Exit Function
    End If
  Next m
Next i

porte_ok = True
End Function

