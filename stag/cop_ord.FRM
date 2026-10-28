VERSION 5.00
Begin VB.Form cop_ord 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   2595
   ClientLeft      =   3375
   ClientTop       =   1650
   ClientWidth     =   5025
   ControlBox      =   0   'False
   DrawWidth       =   10
   BeginProperty Font 
      Name            =   "Arial"
      Size            =   8.25
      Charset         =   0
      Weight          =   700
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
   ScaleHeight     =   2595
   ScaleWidth      =   5025
   ShowInTaskbar   =   0   'False
   Begin VB.PictureBox Picture1 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      ForeColor       =   &H80000008&
      Height          =   495
      Left            =   2745
      Picture         =   "cop_ord.frx":0000
      ScaleHeight     =   495
      ScaleWidth      =   495
      TabIndex        =   9
      TabStop         =   0   'False
      Top             =   1162
      Width           =   495
   End
   Begin VB.TextBox txt_dst 
      Height          =   255
      Left            =   3585
      TabIndex        =   1
      Top             =   1282
      Width           =   800
   End
   Begin VB.TextBox txt_from 
      Height          =   255
      Left            =   345
      TabIndex        =   3
      Top             =   1282
      Width           =   800
   End
   Begin VB.TextBox txt_to 
      Height          =   255
      Left            =   1545
      TabIndex        =   0
      Top             =   1282
      Width           =   800
   End
   Begin VB.CommandButton tas_F1 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      Caption         =   "F11"
      Height          =   375
      Left            =   1785
      TabIndex        =   2
      TabStop         =   0   'False
      Top             =   1762
      Width           =   1215
   End
   Begin VB.Label lab_avv_1 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "(Esc per annullare)"
      ForeColor       =   &H00404080&
      Height          =   210
      Left            =   255
      TabIndex        =   11
      Top             =   712
      Width           =   4215
      WordWrap        =   -1  'True
   End
   Begin VB.Label lab_dst 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
      Caption         =   "destinazione"
      ForeColor       =   &H80000008&
      Height          =   210
      Left            =   3405
      TabIndex        =   8
      Top             =   1042
      Width           =   1095
   End
   Begin VB.Label lab_avv 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Indicare il record iniziale, finale"
      ForeColor       =   &H00404080&
      Height          =   210
      Left            =   255
      TabIndex        =   7
      Top             =   502
      Width           =   4215
      WordWrap        =   -1  'True
   End
   Begin VB.Label lab_from 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
      Caption         =   "Inizio"
      ForeColor       =   &H80000008&
      Height          =   210
      Left            =   510
      TabIndex        =   6
      Top             =   1042
      Width           =   465
   End
   Begin VB.Label lab_to 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
      Caption         =   "fine"
      ForeColor       =   &H80000008&
      Height          =   210
      Left            =   1740
      TabIndex        =   5
      Top             =   1042
      Width           =   345
   End
   Begin VB.Label txt_F1 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
      Caption         =   "Esegui"
      ForeColor       =   &H80000008&
      Height          =   210
      Left            =   2100
      TabIndex        =   4
      Top             =   2242
      Width           =   585
   End
   Begin VB.Label Pag_title 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Copia ordini"
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
      Left            =   255
      TabIndex        =   10
      Top             =   142
      Width           =   4515
   End
End
Attribute VB_Name = "cop_ord"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Dim inizio As Integer
Dim Fine As Integer
Dim dest As Integer


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
    If Len(txt_from.Text) = 0 Or Len(txt_to.Text) = 0 Or Len(txt_dst) = 0 Then
          txt_from.SetFocus
          messaggio msg("par_man"), True, False
    Else
          inizio = CInt(txt_from.Text)
          Fine = CInt(txt_to.Text)
          dest = CInt(txt_dst.Text)
          If inizio > 0 And inizio <= Fine And Fine <= ord_mac.Count And dest > 0 And dest <= ord_mac.Count + 1 Then
              messaggio msg("ope_in_cor"), False, False
              risp = ord_mac.Cpy_blk(inizio, Fine, dest)
          Else
              messaggio msg("par_err"), True, False
              risp = RET_BAD
              If dest <= 0 Or dest > ord_mac.Count Then
                txt_dst.Text = ""
                txt_dst.SetFocus
              End If
              If Fine > ord_mac_ese.Count Or Fine <= 0 Then
                txt_to.Text = ""
                txt_dst.Text = ""
                txt_to.SetFocus
              End If
              If inizio > Fine Or inizio > ord_mac_ese.Count Or inizio <= 0 Then
                txt_from.Text = ""
                txt_to.Text = ""
                txt_dst.Text = ""
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


Private Sub txt_dst_GotFocus()
 txt_dst.SelStart = 0
  txt_dst.SelLength = Len(txt_dst)
End Sub

Private Sub txt_dst_KeyPress(KeyAscii As Integer)
 If KeyAscii = vbKeyReturn Then
        KeyAscii = 0
    Else
        KeyAscii = get_num(KeyAscii)
    End If
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


