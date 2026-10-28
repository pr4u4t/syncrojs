VERSION 5.00
Begin VB.Form dia_ret_azi 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   2910
   ClientLeft      =   3375
   ClientTop       =   2730
   ClientWidth     =   7995
   ControlBox      =   0   'False
   BeginProperty Font 
      Name            =   "Arial"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   2910
   ScaleWidth      =   7995
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox txt_idle 
      Height          =   315
      Left            =   5280
      TabIndex        =   3
      Text            =   "Text1"
      Top             =   -5000
      Width           =   495
   End
   Begin VB.TextBox txt_msg_dia 
      BackColor       =   &H00FFFFFF&
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   1695
      Left            =   180
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   2
      Top             =   960
      Width           =   7695
   End
   Begin VB.Label lab_avv_1 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
      Caption         =   "(Esc per annullare)"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00404080&
      Height          =   435
      Left            =   1680
      TabIndex        =   1
      Top             =   600
      Width           =   4215
      WordWrap        =   -1  'True
   End
   Begin VB.Label Pag_title 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Diagnosi della rete CAN"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   360
      TabIndex        =   0
      Top             =   120
      Width           =   3975
   End
End
Attribute VB_Name = "dia_ret_azi"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1

Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
    eve_key.chg dia_sta_azi
  Case Else
End Select
End Sub

Private Sub Form_Activate()
Dim i As Integer
Dim str As String
Dim str1 As String
Dim car As String
Dim szName As String * 100
Dim szbuff As String * 100

messaggio msg("ope_in_cor"), False, False

CAN_result = CAN_GetDiagnostic(str_msg_dia_CAN)
'CAN_result = CAN_CloseAll
''registrazione hardware
'  CAN_result = CAN_RegisterHardware(HAR_CAN_DONGLE, 2, &H378, 7)
'  If CAN_result <> CAN_ERR_OK Then
'    CAN_result = CAN_GetErrText(CAN_result, szbuff)
'    log_str "Form Load set_par_rul.  CAN_RegisterHardware: " + szbuff, MED_PRIORITY, log_can
'  End If
'  'registrazione rete
'  CAN_result = CAN_RegisterNet(1, "Lenze", 1, &H14)
'  If CAN_result <> CAN_ERR_OK Then
'    CAN_result = CAN_GetErrText(CAN_result, szbuff)
'    log_str "Form Load set_par_rul.  CAN_RegisterNet: " + szbuff, MED_PRIORITY, log_can
'  End If
'  ' first register the client...
'  CAN_result = CAN_RegisterClient(app.EXEName, hWnd, hClient)
'  If CAN_result <> CAN_ERR_OK Then
'    CAN_result = CAN_GetErrText(CAN_result, szbuff)
'    log_str "Form Load set_par_rul.  CAN_RegisterClient: " + szbuff, MED_PRIORITY, log_can
'  End If
'  ' ...and connect him to a net
'  CAN_result = CAN_ConnectToNet(hClient, "Lenze", hNet)
'  If CAN_result <> CAN_ERR_OK Then
'    CAN_result = CAN_GetErrText(CAN_result, szbuff)
'    log_str "Form Load set_par_rul.  CAN_ConnectToNet: " + szbuff, MED_PRIORITY, log_can
'  End If
'
'  CAN_result = CAN_GetNetName(hNet, szName)
'  If CAN_result <> CAN_ERR_OK Then
'    CAN_result = CAN_GetErrText(CAN_result, szbuff)
'    log_str "Form Load set_par_rul.  CAN_GetNetName: " + szbuff, MED_PRIORITY, log_can
'  End If
'
'
'
'  CAN_result = CAN_HardwareOfNet(hNet, 1)
'  If CAN_result <> CAN_ERR_OK Then
'    CAN_result = CAN_GetErrText(CAN_result, szbuff)
'    log_str "Form Load set_par_rul.  CAN_HardwareOfNet: " + szbuff, MED_PRIORITY, log_can
'  End If
'
'  CAN_result = CAN_SetBaud(1, &H14)
'  If CAN_result <> CAN_ERR_OK Then
'    CAN_result = CAN_GetErrText(CAN_result, szbuff)
'    log_str "Form Load set_par_rul.  CAN_SetBaud: " + szbuff, MED_PRIORITY, log_can
'  End If
'
''For i = 0 To 255
''    CAN_result = CAN_SetBaud(i, 14)
''    If CAN_result <> CAN_ERR_ILLHW Or i = 255 Then Exit For
''Next i
'      ' use CAN-ID 0x100
''      msg_can.ID = &H100
'msg_can.RTR = 0
'msg_can.LEN = 8
''send = False

CAN_result = CAN_VersionInfo(str_msg_ver_CAN)

txt_msg_dia.Text = ""



For i = 1 To Len(str_msg_ver_CAN)
  car = Mid$(str_msg_ver_CAN, i, 1)
  If car = Chr$(10) Or car = Chr$(0) Then
      str = str + Space$(2)
  Else
      str = str + car
  End If
Next i

For i = 1 To Len(str_msg_dia_CAN)
  car = Mid$(str_msg_dia_CAN, i, 1)
  If car = Chr$(10) Or car = Chr$(0) Then
      str1 = str1 + Space$(2)
  Else
      str1 = str1 + car
  End If
Next i
messaggio msg("ope_eff"), False, False
txt_msg_dia.Text = str + str1
txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
txt_idle.SetFocus
txt_msg_dia.TabStop = False

End Sub

Private Sub Form_Load()
Debug.Print "Inizio Load " & Me.name
Set eve_key = New key
eve_key.create Me
Me.Width = Int(Me.Width * pro_set.PercX / 100)
Me.Height = Int(Me.Height * pro_set.PercY / 100)
Center Me
dimensiona Me
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub


