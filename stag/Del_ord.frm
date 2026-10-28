VERSION 5.00
Begin VB.Form Del_ord 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   2610
   ClientLeft      =   1950
   ClientTop       =   3540
   ClientWidth     =   4815
   ControlBox      =   0   'False
   BeginProperty Font 
      Name            =   "Arial"
      Size            =   9
      Charset         =   0
      Weight          =   700
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   ForeColor       =   &H80000008&
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   2610
   ScaleWidth      =   4815
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
      Left            =   1800
      TabIndex        =   5
      TabStop         =   0   'False
      Top             =   1770
      Width           =   1215
   End
   Begin VB.TextBox txt_to 
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   2970
      TabIndex        =   0
      Top             =   1290
      Width           =   800
   End
   Begin VB.TextBox txt_from 
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   960
      TabIndex        =   2
      Top             =   1290
      Width           =   800
   End
   Begin VB.Label lab_avv_1 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
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
      Height          =   210
      Left            =   270
      TabIndex        =   8
      Top             =   750
      Width           =   4215
      WordWrap        =   -1  'True
   End
   Begin VB.Label Pag_title 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Cancellazione ordini"
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
      Left            =   240
      TabIndex        =   7
      Top             =   150
      Width           =   4515
   End
   Begin VB.Label txt_F1 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H00FFFFFF&
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
      Left            =   2115
      TabIndex        =   6
      Top             =   2250
      Width           =   585
   End
   Begin VB.Label lab_to 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "A"
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
      Left            =   3300
      TabIndex        =   1
      Top             =   1050
      Width           =   135
   End
   Begin VB.Label lab_from 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Da"
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
      Left            =   1245
      TabIndex        =   3
      Top             =   1050
      Width           =   225
   End
   Begin VB.Label lab_avv 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Attenzione verranno CANCELLATI i seguenti ordini"
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
      Height          =   210
      Left            =   270
      TabIndex        =   4
      Top             =   540
      Width           =   4215
      WordWrap        =   -1  'True
   End
End
Attribute VB_Name = "Del_ord"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Dim inizio As Integer
Dim Fine As Integer

Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
    eve_key.chg pag_lis_ord
  Case Else
End Select
End Sub

Private Sub eve_key_F11(Shift As Integer)
Dim risp As cod_ret

Select Case Shift
  Case 0
    If Len(txt_from.Text) = 0 Or Len(txt_to.Text) = 0 Then
          messaggio msg("par_man"), True, False
    Else
          inizio = CInt(txt_from.Text)
          Fine = CInt(txt_to.Text)
          If inizio <= Fine And Fine <= ord_mac.Count And inizio > 0 Then
                messaggio msg("ope_in_cor"), False, False
                risp = ord_mac.Del_blk(inizio, Fine, False, False)
          Else
                messaggio msg("par_err"), True, False
                risp = RET_BAD
                If Fine > ord_mac.Count Or Fine <= 0 Then
                  txt_to.Text = ""
                  txt_to.SetFocus
                End If
                If inizio > Fine Or inizio > ord_mac.Count Or inizio <= 0 Then
                  txt_from.Text = ""
                  txt_to.Text = ""
                  txt_from.SetFocus
                End If
                Exit Sub
          End If
          If risp = RET_OK Then
                messaggio msg("ope_eff"), False, False
                eve_key.chg pag_lis_ord
          End If
    End If
  Case Else
End Select
End Sub

Private Sub Form_Activate()

txt_to.SetFocus

End Sub

Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
On Error GoTo STAG_Gest_ERR
  
  If ActiveControl Is Nothing Then Exit Sub
  Select Case KeyCode
    Case vbKeyReturn, vbKeyDown
      chk_shift 'controllo dello stato del tasto SHIFT
      KeyCode = 0
      SendKeys "{TAB}"
    Case vbKeyUp
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
Center Me
dimensiona Me
Debug.Print "Fine Load " & Me.name
Debug.Print ""

End Sub


Private Sub txt_from_GotFocus()
 txt_from.SelStart = 0
  txt_from.SelLength = Len(txt_from)
End Sub

Private Sub txt_from_KeyPress(KeyAscii As Integer)
 If KeyAscii = vbKeyReturn Then
        KeyAscii = 0
    Else
        KeyAscii = get_num(KeyAscii)
    End If
End Sub


Private Sub txt_to_GotFocus()
 txt_to.SelStart = 0
  txt_to.SelLength = Len(txt_to)
End Sub

Private Sub txt_to_KeyPress(KeyAscii As Integer)
 If KeyAscii = vbKeyReturn Then
        KeyAscii = 0
    Else
        KeyAscii = get_num(KeyAscii)
    End If
End Sub


