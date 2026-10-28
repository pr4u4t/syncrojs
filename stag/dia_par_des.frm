VERSION 5.00
Begin VB.Form dia_par_des 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   2595
   ClientLeft      =   3720
   ClientTop       =   3765
   ClientWidth     =   8130
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2595
   ScaleWidth      =   8130
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
      Left            =   3428
      TabIndex        =   1
      TabStop         =   0   'False
      Top             =   1650
      Width           =   1215
   End
   Begin VB.TextBox txt_des 
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
      Left            =   1965
      MaxLength       =   50
      TabIndex        =   0
      Text            =   "12345678901234567890123456789012345678901234567890"
      Top             =   1050
      Width           =   5535
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
      Left            =   1965
      TabIndex        =   5
      Top             =   645
      Width           =   4215
      WordWrap        =   -1  'True
   End
   Begin VB.Label Pag_title 
      Alignment       =   2  'Center
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Immisione descrizione nuovo parametro"
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
      Left            =   1065
      TabIndex        =   4
      Top             =   285
      Width           =   6015
   End
   Begin VB.Label Label1 
      Alignment       =   1  'Right Justify
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Descrizione"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   45
      TabIndex        =   3
      Top             =   1080
      Width           =   1755
   End
   Begin VB.Label txt_F1 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
      Caption         =   "Salva"
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
      Left            =   3840
      TabIndex        =   2
      Top             =   2100
      Width           =   435
   End
End
Attribute VB_Name = "dia_par_des"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Dim par_tro As Boolean
Dim codice As Long
Dim sot_codice As Long
Dim par As tip_par_nuo_azi
Private Function tro_par(cod As Long, sot_cod As Long) As tip_par_nuo_azi
Dim rs As Recordset

Set rs = db.OpenRecordset("par_azi", dbOpenTable)
If rs.RecordCount > 0 Then
    rs.MoveFirst
    While Not rs.EOF
      If rs("COD") = CStr(cod) And rs("SOT_COD") = CStr(sot_cod) Then
          tro_par.cod = cod
          tro_par.sot_cod = sot_cod
          tro_par.descr = rs("DESCR")
          rs.Close
          Exit Function
      End If
      rs.MoveNext
    Wend
End If
rs.Close
tro_par.cod = -1
tro_par.sot_cod = -1
tro_par.descr = ""
End Function


Sub chg_par(cod As Long, sot_cod As Long)
Dim rs As Recordset

Set rs = db.OpenRecordset("par_azi", dbOpenTable)
rs.MoveFirst
While Not rs.EOF
  If rs("COD") = CStr(cod) And rs("SOT_COD") = CStr(sot_cod) Then
      rs.Edit
      rs("DESCR") = txt_des
      rs.Update
      Exit Sub
  End If
  rs.MoveNext
Wend
rs.Close
End Sub


Sub ins_par(cod As Long, sot_cod As Long)
Dim rs As Recordset

Set rs = db.OpenRecordset("par_azi", dbOpenTable)
rs.AddNew
rs("DESCR") = txt_des
rs("COD") = CStr(codice)
rs("SOT_COD") = CStr(sot_codice)
rs.Update
rs.Close
End Sub

Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
  eve_key.chg dia_sta_azi
  Case Else
End Select
End Sub

Private Sub eve_key_F11(Shift As Integer)
Select Case Shift
  Case 0
  If par_tro Then
    chg_par codice, sot_codice
  Else
    ins_par codice, sot_codice
  End If
  par_nuo.change_descr txt_des
  messaggio msg("ope_eff"), False, False
  eve_key.chg dia_sta_azi
  Case Else
End Select
End Sub

Private Sub Form_Activate()
Dim m As tip_par_nuo_azi

codice = par_nuo.get_index
sot_codice = par_nuo.get_subindex
m = tro_par(codice, sot_codice)
If m.cod <> -1 Then
  txt_des = m.descr
  par_tro = True
Else
  txt_des = ""
  par_tro = False
End If

txt_des.SetFocus

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


Private Sub txt_des_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


