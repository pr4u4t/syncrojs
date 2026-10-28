VERSION 5.00
Begin VB.Form del_ord_ese 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   2685
   ClientLeft      =   3975
   ClientTop       =   1515
   ClientWidth     =   4680
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2685
   ScaleWidth      =   4680
   ShowInTaskbar   =   0   'False
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
      Left            =   803
      TabIndex        =   2
      Top             =   1387
      Width           =   800
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
      Left            =   2843
      TabIndex        =   1
      Top             =   1387
      Width           =   800
   End
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
      Left            =   1643
      TabIndex        =   0
      TabStop         =   0   'False
      Top             =   1867
      Width           =   1215
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
      Left            =   210
      TabIndex        =   8
      Top             =   847
      Width           =   4215
      WordWrap        =   -1  'True
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
      Left            =   210
      TabIndex        =   7
      Top             =   637
      Width           =   4215
      WordWrap        =   -1  'True
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
      Left            =   1095
      TabIndex        =   6
      Top             =   1147
      Width           =   225
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
      Left            =   3150
      TabIndex        =   5
      Top             =   1147
      Width           =   135
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
      Left            =   1965
      TabIndex        =   4
      Top             =   2347
      Width           =   585
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
      Left            =   90
      TabIndex        =   3
      Top             =   127
      Width           =   4515
   End
End
Attribute VB_Name = "del_ord_ese"
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
    eve_key.chg pag_lis_ese
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
          If inizio <= Fine And Fine <= ord_mac_ese.Count And inizio > 0 Then
                messaggio msg("ope_in_cor"), False, False
                risp = ord_mac_ese.Del_blk(inizio, Fine, False, False)
          Else
                messaggio msg("par_err"), True, False
                risp = RET_BAD
                If Fine > ord_mac_ese.Count Or Fine <= 0 Then
                  txt_to.Text = ""
                  txt_to.SetFocus
                End If
                If inizio > Fine Or inizio > ord_mac_ese.Count Or inizio <= 0 Then
                  txt_from.Text = ""
                  txt_to.Text = ""
                  txt_from.SetFocus
                End If
                Exit Sub
          End If
          If risp = RET_OK Then
                messaggio msg("ope_eff"), False, False
                eve_key.chg pag_lis_ese
          End If
    End If
  Case Else
End Select
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


