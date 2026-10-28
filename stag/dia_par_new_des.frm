VERSION 5.00
Begin VB.Form dia_par_new_des 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   2370
   ClientLeft      =   1560
   ClientTop       =   2955
   ClientWidth     =   9285
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   2370
   ScaleWidth      =   9285
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox txt_idle 
      Height          =   285
      Left            =   1020
      TabIndex        =   5
      Top             =   -800
      Width           =   495
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
      Left            =   4035
      TabIndex        =   1
      TabStop         =   0   'False
      Top             =   1350
      Width           =   1215
   End
   Begin VB.Label lab_des 
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   330
      Left            =   2130
      TabIndex        =   4
      Top             =   300
      Width           =   5565
   End
   Begin VB.Image Image1 
      Height          =   345
      Left            =   7740
      Picture         =   "dia_par_new_des.frx":0000
      Top             =   270
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
      Left            =   2535
      TabIndex        =   3
      Top             =   1080
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
      Left            =   4335
      TabIndex        =   2
      Top             =   1890
      Width           =   585
   End
   Begin VB.Label Pag_title 
      Alignment       =   1  'Right Justify
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Descrizione"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   285
      Left            =   60
      TabIndex        =   0
      Top             =   300
      Width           =   1995
   End
End
Attribute VB_Name = "dia_par_new_des"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1

Dim rs As Recordset
Dim dbf_vuo As Boolean
Dim cod As Long
Dim sot_cod As Long

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
  If rs("COD") <> CStr(par_nuo.get_index) Or rs("SOT_COD") <> CStr(par_nuo.get_subindex) Then
    par_nuo.change_par_new val(rs("COD")), val(rs("SOT_COD"))
    par_nuo.change_descr Lab_des
    imp_nuo_par = True
  End If
  eve_key.chg dia_sta_azi
  Case Else
End Select
End Sub

Private Sub Form_Activate()
Dim tro As Boolean

cod = par_nuo.get_index
sot_cod = par_nuo.get_subindex
Set rs = db.OpenRecordset("select * from par_azi order by DESCR", dbOpenDynaset)
If rs.RecordCount = 0 Then
  Lab_des.Caption = msg("nes_des_dis")
  dbf_vuo = True
Else
  If cod > 0 And sot_cod >= 0 Then
    rs.MoveFirst
    tro = False
    While Not rs.EOF And Not tro
      If rs("COD") = CStr(cod) And rs("SOT_COD") = CStr(sot_cod) Then
        tro = True
        Lab_des = IIf(IsNull(rs("DESCR")), "", rs("DESCR"))
      Else
        rs.MoveNext
      End If
    Wend
  Else
    rs.MoveFirst
    Lab_des = IIf(IsNull(rs("DESCR")), "", rs("DESCR"))
  End If
  dbf_vuo = False
End If
txt_idle.SetFocus
End Sub

Private Sub Form_Deactivate()
rs.Close
End Sub

Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
Select Case KeyCode
  Case 39
    If rs.RecordCount > 0 Then
        If Not rs.EOF Then rs.MoveNext
        If rs.EOF Then rs.MovePrevious
        Lab_des = IIf(IsNull(rs("DESCR")), "", rs("DESCR"))
    End If
  Case 37
    If rs.RecordCount > 0 Then
        If Not rs.BOF Then rs.MovePrevious
        If rs.BOF Then rs.MoveNext
        Lab_des = IIf(IsNull(rs("DESCR")), "", rs("DESCR"))
    End If
  Case Else
    eve_key.frm_KeyDown KeyCode, Shift
End Select
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


