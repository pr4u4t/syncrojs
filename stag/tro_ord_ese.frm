VERSION 5.00
Begin VB.Form tro_ord_ese 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   2595
   ClientLeft      =   7005
   ClientTop       =   4590
   ClientWidth     =   4875
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2595
   ScaleWidth      =   4875
   ShowInTaskbar   =   0   'False
   Begin VB.CommandButton tas_F1 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      Caption         =   "F11"
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
      Left            =   1845
      TabIndex        =   4
      TabStop         =   0   'False
      Top             =   1807
      Width           =   1215
   End
   Begin VB.TextBox txt_cod 
      Height          =   255
      Left            =   1860
      TabIndex        =   1
      Top             =   1237
      Width           =   1185
   End
   Begin VB.Label lab_avv_1 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
      Caption         =   "(Esc per annullare)"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00404080&
      Height          =   195
      Left            =   390
      TabIndex        =   6
      Top             =   607
      Width           =   4215
      WordWrap        =   -1  'True
   End
   Begin VB.Label txt_F1 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
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
      Left            =   2145
      TabIndex        =   5
      Top             =   2317
      Width           =   585
   End
   Begin VB.Label Label1 
      Alignment       =   2  'Center
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Codice"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Left            =   1545
      TabIndex        =   3
      Top             =   937
      Width           =   1845
   End
   Begin VB.Label lab_avv 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
      Caption         =   "Indicare il codice abbinamento dell'ordine"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00404080&
      Height          =   225
      Left            =   390
      TabIndex        =   2
      Top             =   397
      Width           =   4215
      WordWrap        =   -1  'True
   End
   Begin VB.Label Pag_title 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Trova ordine"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   330
      TabIndex        =   0
      Top             =   60
      Width           =   2805
   End
End
Attribute VB_Name = "tro_ord_ese"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Dim cod As String



Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
    eve_key.chg pag_lis_ese
  Case Else
End Select
End Sub

Private Sub eve_key_F11(Shift As Integer)
Dim risp As cod_ret

Select Case Shift
  Case 0
    If Len(txt_cod.Text) = 0 Then
        messaggio msg("par_man"), True, False
    Else
        cod = CStr(txt_cod.Text)
        messaggio msg("ope_in_cor"), False, False
        risp = ord_mac_ese.Tro_lis(cod)
        If risp = RET_OK Then
          messaggio msg("ope_eff"), False, False
          eve_key.chg pag_lis_ese
        Else
          messaggio msg("cod_non_pre"), True, False
          txt_cod.Text = ""
          txt_cod.SetFocus
        End If
    End If
  Case Else
End Select
End Sub


Private Sub Form_Activate()
txt_cod.Text = ""
txt_cod.SetFocus
End Sub

Private Sub Form_Load()
Debug.Print "Inizio Load " & Me.name
Set eve_key = New key
eve_key.create Me
Ridim Me
Center Me
dimensiona Me
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub


Private Sub txt_cod_GotFocus()
txt_cod.SelStart = 0
txt_cod.SelLength = Len(txt_cod)
End Sub

Private Sub txt_cod_KeyPress(KeyAscii As Integer)
'If KeyAscii = vbKeyReturn Then
'        KeyAscii = 0
'End If
End Sub


