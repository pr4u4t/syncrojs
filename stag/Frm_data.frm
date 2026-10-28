VERSION 5.00
Begin VB.Form Frm_data 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   2685
   ClientLeft      =   5160
   ClientTop       =   1290
   ClientWidth     =   3540
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2685
   ScaleWidth      =   3540
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox txt_sec 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BorderStyle     =   0  'None
      Height          =   315
      Left            =   2520
      MaxLength       =   2
      TabIndex        =   13
      Text            =   "Text1"
      Top             =   1410
      Width           =   645
   End
   Begin VB.Timer tim_ora 
      Enabled         =   0   'False
      Interval        =   1000
      Left            =   90
      Top             =   2070
   End
   Begin VB.CommandButton tas_F1 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      Caption         =   "F3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   1260
      MaskColor       =   &H00FFFFFF&
      Style           =   1  'Graphical
      TabIndex        =   10
      TabStop         =   0   'False
      Top             =   2010
      Width           =   1215
   End
   Begin VB.TextBox txt_min 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BorderStyle     =   0  'None
      Height          =   315
      Left            =   1500
      MaxLength       =   2
      TabIndex        =   9
      Text            =   "Text1"
      Top             =   1410
      Width           =   645
   End
   Begin VB.TextBox txt_ore 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BorderStyle     =   0  'None
      Height          =   315
      Left            =   480
      MaxLength       =   2
      TabIndex        =   8
      Text            =   "Text1"
      Top             =   1410
      Width           =   645
   End
   Begin VB.TextBox txt_year 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BorderStyle     =   0  'None
      Height          =   315
      Left            =   2520
      MaxLength       =   4
      TabIndex        =   5
      Text            =   "Text1"
      Top             =   750
      Width           =   645
   End
   Begin VB.TextBox txt_month 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BorderStyle     =   0  'None
      Height          =   315
      Left            =   1500
      MaxLength       =   2
      TabIndex        =   4
      Text            =   "Text1"
      Top             =   750
      Width           =   645
   End
   Begin VB.TextBox txt_day 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BorderStyle     =   0  'None
      Height          =   315
      Left            =   480
      MaxLength       =   2
      TabIndex        =   3
      Text            =   "Text1"
      Top             =   750
      Width           =   645
   End
   Begin VB.Line Line6 
      BorderWidth     =   2
      X1              =   90
      X2              =   3540
      Y1              =   1350
      Y2              =   1350
   End
   Begin VB.Line Line5 
      BorderWidth     =   2
      X1              =   90
      X2              =   3540
      Y1              =   690
      Y2              =   690
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   -8400
      Picture         =   "Frm_data.frx":0000
      Stretch         =   -1  'True
      Top             =   0
      Width           =   11970
   End
   Begin VB.Line Line3 
      X1              =   2280
      X2              =   2400
      Y1              =   1560
      Y2              =   1560
   End
   Begin VB.Label lab_sec 
      Alignment       =   2  'Center
      BackColor       =   &H00FFFFFF&
      Caption         =   "secondi"
      Height          =   255
      Left            =   2220
      TabIndex        =   12
      Top             =   1130
      Width           =   1245
   End
   Begin VB.Label txt_F1 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H00FFFFFF&
      Caption         =   "Esegui"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   210
      Left            =   1575
      TabIndex        =   11
      Top             =   2445
      Width           =   585
   End
   Begin VB.Line Line4 
      X1              =   1260
      X2              =   1380
      Y1              =   1560
      Y2              =   1560
   End
   Begin VB.Label lab_min 
      Alignment       =   2  'Center
      BackColor       =   &H00FFFFFF&
      Caption         =   "minuti"
      Height          =   255
      Left            =   1210
      TabIndex        =   7
      Top             =   1130
      Width           =   1245
   End
   Begin VB.Label lab_ore 
      Alignment       =   2  'Center
      BackColor       =   &H00FFFFFF&
      Caption         =   "ore"
      Height          =   255
      Left            =   180
      TabIndex        =   6
      Top             =   1130
      Width           =   1245
   End
   Begin VB.Line Line1 
      X1              =   1260
      X2              =   1380
      Y1              =   900
      Y2              =   900
   End
   Begin VB.Line Line2 
      X1              =   2280
      X2              =   2400
      Y1              =   900
      Y2              =   900
   End
   Begin VB.Label lab_year 
      Alignment       =   2  'Center
      BackColor       =   &H00FFFFFF&
      Caption         =   "anno"
      Height          =   255
      Left            =   2220
      TabIndex        =   2
      Top             =   460
      Width           =   1245
   End
   Begin VB.Label lab_month 
      Alignment       =   2  'Center
      BackColor       =   &H00FFFFFF&
      Caption         =   "mese"
      Height          =   255
      Left            =   1200
      TabIndex        =   1
      Top             =   460
      Width           =   1245
   End
   Begin VB.Label lab_day 
      Alignment       =   2  'Center
      BackColor       =   &H00FFFFFF&
      Caption         =   "giorno"
      Height          =   255
      Left            =   180
      TabIndex        =   0
      Top             =   460
      Width           =   1245
   End
End
Attribute VB_Name = "Frm_data"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1



Private Sub my_key(KeyCode As Integer)
  Select Case KeyCode
    Case vbKeyReturn, vbKeyDown
      KeyCode = 0
      SendKeys "{tab}"
      'ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
    Case vbKeyUp
      KeyCode = 0
      SendKeys "+{tab}"
      'ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
    Case vbKey0 To vbKey9, vbKeyNumpad0 To vbKeyNumpad9, vbKeyCancel, vbKeyBack
      tim_ora.Enabled = False
    Case Else
  End Select
  
End Sub

Private Sub upd_time()
Dim today
  today = Date 'data corrente
  'data
  txt_day.Text = Format$(Day(today), "00")
  txt_month.Text = Format$(Month(today), "00")
  txt_year.Text = Format$(Year(today), "0000")
  'ora
  today = Time
  txt_ore.Text = Format$(Hour(today), "00")
  txt_min.Text = Format$(Minute(today), "00")
  txt_sec.Text = Format$(Second(today), "00")

End Sub

Private Sub eve_key_esc(Shift As Integer)
  Select Case Shift
    Case 0
      'eve_key.chg set_par_mac
      eve_key.chg pag_pre '6122001 Stagetti
    Case Else
  End Select
End Sub

Private Sub eve_key_F3(Shift As Integer)
On Error GoTo STAG_Gest_ERR
  Select Case Shift
    Case 0
      'setta la data e l'ora con i dati impostati
      Date = DateSerial(val(txt_year.Text), val(txt_month.Text), val(txt_day.Text))
      Time = TimeSerial(val(txt_ore.Text), val(txt_min.Text), val(txt_sec.Text))
      eve_key.chg pag_pre '6122001 Stagetti
    Case Else
  End Select
Exit Sub
STAG_Gest_ERR:
log_str "on error intervenuta in frm_data eve_key_F3" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next
End Sub

Private Sub Form_Activate()
tim_ora.Enabled = True
upd_time
txt_day.SetFocus
End Sub

Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
On Error GoTo STAG_Gest_ERR
  
  If ActiveControl Is Nothing Then Exit Sub
  Select Case KeyCode
    Case vbKeyReturn, vbKeyRight
      chk_shift 'controllo dello stato del tasto SHIFT
      KeyCode = 0
      SendKeys "{TAB}"
    Case vbKeyLeft
      chk_shift 'controllo dello stato del tasto SHIFT
      KeyCode = 0
      SendKeys "+{TAB}"
  End Select
  
  Exit Sub

STAG_Gest_ERR:
log_str "on error intervenuta in Form_KeyDown della form " + Me.name + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next
End Sub


Private Sub Form_Load()
  Debug.Print "Inizio Load " & Me.name
  Set eve_key = New key
  eve_key.create Me
  Ridim Me
  dimensiona Me
  Center Me
  Debug.Print "Fine Load " & Me.name
  Debug.Print ""
End Sub

Private Sub tim_ora_Timer()
  'aggiorna i dati riguardanti l'ora e la data
  upd_time
End Sub

Private Sub txt_day_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_day_KeyDown(KeyCode As Integer, Shift As Integer)
  my_key KeyCode
End Sub


Private Sub txt_min_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_min_KeyDown(KeyCode As Integer, Shift As Integer)
  my_key KeyCode
End Sub

Private Sub txt_month_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_month_KeyDown(KeyCode As Integer, Shift As Integer)
  my_key KeyCode
End Sub

Private Sub txt_ore_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_ore_KeyDown(KeyCode As Integer, Shift As Integer)
  my_key KeyCode
End Sub

Private Sub txt_sec_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_sec_KeyDown(KeyCode As Integer, Shift As Integer)
  my_key KeyCode
End Sub

Private Sub txt_year_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_year_KeyDown(KeyCode As Integer, Shift As Integer)
  my_key KeyCode
End Sub

