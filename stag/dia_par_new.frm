VERSION 5.00
Begin VB.Form dia_par_new 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   2625
   ClientLeft      =   6030
   ClientTop       =   525
   ClientWidth     =   4995
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2625
   ScaleWidth      =   4995
   ShowInTaskbar   =   0   'False
   Begin VB.CheckBox chk_div 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      Caption         =   "Dividi il valore per 10000"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   350
      Left            =   360
      MaskColor       =   &H00FFFFFF&
      TabIndex        =   2
      Top             =   2040
      Value           =   1  'Checked
      Width           =   2400
   End
   Begin VB.TextBox txt_cod 
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   360
      Left            =   1560
      MaxLength       =   5
      TabIndex        =   0
      Top             =   1140
      Width           =   650
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
      Left            =   3480
      TabIndex        =   8
      TabStop         =   0   'False
      Top             =   1560
      Width           =   1215
   End
   Begin VB.TextBox txt_sot_cod 
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   360
      Left            =   1560
      MaxLength       =   3
      TabIndex        =   1
      Top             =   1575
      Width           =   465
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
      Left            =   3780
      TabIndex        =   9
      Top             =   2100
      Width           =   585
   End
   Begin VB.Label Label2 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Sottocodice"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   195
      Left            =   360
      TabIndex        =   7
      Top             =   1635
      Width           =   1245
   End
   Begin VB.Label Label1 
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
      Height          =   195
      Left            =   360
      TabIndex        =   6
      Top             =   1200
      Width           =   1125
   End
   Begin VB.Label Pag_title 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Immisione nuovo parametro"
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
      TabIndex        =   5
      Top             =   75
      Width           =   3645
   End
   Begin VB.Label lab_avv 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
      Caption         =   "Immettere il codice e sottocodice del parametro"
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
      TabIndex        =   4
      Top             =   495
      Width           =   4215
      WordWrap        =   -1  'True
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
      TabIndex        =   3
      Top             =   705
      Width           =   4215
      WordWrap        =   -1  'True
   End
End
Attribute VB_Name = "dia_par_new"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Dim rs As Recordset

Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
    eve_key.chg dia_sta_azi
  Case Else
End Select
End Sub

Private Sub eve_key_F11(Shift As Integer)
Dim tro As Boolean
Dim str As String

Select Case Shift
  Case 0
    tro = False
    If val(txt_cod) <= 0 Or val(txt_sot_cod) < 0 Then
        messaggio msg("par_err"), True, False
        txt_cod.SetFocus
    Else
        par_nuo.change_par_new val(txt_cod), val(txt_sot_cod)
        par_nuo.dividi = CBool(chk_div.Value = 1)
        imp_nuo_par = True
        If rs.RecordCount > 0 Then
          rs.MoveFirst
          While Not rs.EOF And Not tro
            If rs("COD") = txt_cod And rs("SOT_COD") = txt_sot_cod Then
              str = IIf(IsNull(rs("DESCR")), "", rs("DESCR"))
              par_nuo.change_descr str
              tro = True
            Else
              rs.MoveNext
            End If
          Wend
          If Not tro Then par_nuo.change_descr ""
        Else
          par_nuo.change_descr ""
        End If
        eve_key.chg dia_sta_azi
    End If
  Case Else
End Select
End Sub

Private Sub Form_Activate()
Dim str As String
Dim i As Integer
Dim car As String
Dim tro As Boolean

Set rs = db.OpenRecordset("select * from par_azi order by DESCR", dbOpenDynaset)
tro = False
str = ""
If par_nuo.par_pre And par_nuo.get_index > 0 And par_nuo.get_subindex >= 0 Then
    For i = 1 To Len(dia_sta_azi.lab_par_imp.Caption)
      car = Mid$(dia_sta_azi.lab_par_imp.Caption, i, 1)
      If car = "/" Then
          txt_cod = str
          Exit For
      Else
          If car <> "C" And car <> " " Then
            str = str + car
          End If
      End If
    Next i
    txt_sot_cod = Mid$(dia_sta_azi.lab_par_imp.Caption, i + 2, Len(dia_sta_azi.lab_par_imp.Caption) - Len(str) - 4)
Else
    txt_cod.Text = ""
    txt_sot_cod.Text = ""
End If
chk_div.Value = IIf(par_nuo.dividi, 1, 0)
txt_cod.SetFocus
End Sub

Private Sub Form_Deactivate()
rs.Close
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


Private Sub txt_cod_GotFocus()
txt_cod.SelStart = 0
txt_cod.SelLength = Len(txt_cod)
End Sub


Private Sub txt_cod_KeyPress(KeyAscii As Integer)
If KeyAscii = vbKeyReturn Then
        KeyAscii = 0
Else
        KeyAscii = get_num(KeyAscii)
End If
End Sub


Private Sub txt_sot_cod_GotFocus()
txt_sot_cod.SelStart = 0
txt_sot_cod.SelLength = Len(txt_sot_cod)
End Sub


Private Sub txt_sot_cod_KeyPress(KeyAscii As Integer)
If KeyAscii = vbKeyReturn Then
        KeyAscii = 0
Else
        KeyAscii = get_num(KeyAscii)
End If
End Sub


