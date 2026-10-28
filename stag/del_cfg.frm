VERSION 5.00
Begin VB.Form del_cfg 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   3285
   ClientLeft      =   4830
   ClientTop       =   2340
   ClientWidth     =   7020
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   3285
   ScaleWidth      =   7020
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox txt_idle 
      Height          =   285
      Left            =   540
      TabIndex        =   7
      Text            =   "Text1"
      Top             =   -5000
      Width           =   555
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
      Left            =   2985
      TabIndex        =   0
      TabStop         =   0   'False
      Top             =   2325
      Width           =   1215
   End
   Begin VB.Image Image1 
      Height          =   345
      Left            =   5730
      Picture         =   "del_cfg.frx":0000
      Top             =   960
      Width           =   1185
   End
   Begin VB.Label L_LIV 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "LIV."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   330
      Left            =   4665
      TabIndex        =   10
      Top             =   1875
      Visible         =   0   'False
      Width           =   585
   End
   Begin VB.Label lab_liv 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   330
      Left            =   5550
      TabIndex        =   9
      Top             =   1875
      Visible         =   0   'False
      Width           =   675
   End
   Begin VB.Label lab_att 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "CONFIGURAZIONE ATTIVA"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   330
      Left            =   795
      TabIndex        =   8
      Top             =   1875
      Visible         =   0   'False
      Width           =   3975
   End
   Begin VB.Label Label14 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Utilizzare i tasti freccia per visualizzare le diverse configurazioni"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   210
      Left            =   45
      TabIndex        =   6
      Top             =   1005
      Width           =   5520
   End
   Begin VB.Label lab_cfg 
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      Caption         =   "12345678901234567890123456789012345"
      DataField       =   "DESCR"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   300
      Left            =   2535
      TabIndex        =   5
      Top             =   1410
      Width           =   3855
   End
   Begin VB.Label Pag_title 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Elimina configurazione"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00800000&
      Height          =   375
      Left            =   150
      TabIndex        =   4
      Top             =   165
      Width           =   3585
   End
   Begin VB.Label Label1 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "DESCRIZIONE"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   330
      Left            =   630
      TabIndex        =   3
      Top             =   1395
      Width           =   1860
   End
   Begin VB.Label txt_F1 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
      Caption         =   "Elimina"
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
      Height          =   225
      Left            =   3240
      TabIndex        =   2
      Top             =   2760
      Width           =   765
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
      Left            =   2235
      TabIndex        =   1
      Top             =   660
      Width           =   2745
      WordWrap        =   -1  'True
   End
End
Attribute VB_Name = "del_cfg"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1

Dim dbf_vuo As Boolean
Dim for_mac As Integer
Dim gra_car As Integer 'Arturo uff 05-11-02
Dim num_mot_liv As Integer
Dim num_mot_abi As Integer
Dim rs As Recordset

Sub agg_lab()
Dim i As Integer
Dim str As String

str = ""
lab_att.Visible = True
lab_cfg.Caption = rs("DESCR")
For i = 1 To par_mac.car_tec.num_liv_tag
  If rs("LIVELLO" & i) = 1 Then str = str + CStr(i) + Space$(1)
Next i
If Len(str) > 0 Then
    lab_att.Caption = msg("cfg_att")
    lab_att.ForeColor = vbRed
    lab_liv.Visible = True
    lab_liv.Caption = str
    lab_liv.ForeColor = vbRed
    L_LIV.Visible = True
    L_LIV.ForeColor = vbRed
Else
    lab_att.Caption = msg("cfg_non_att")
    lab_att.ForeColor = vbBlack
    lab_liv.Visible = False
    L_LIV.Visible = False
End If
End Sub


Sub ini_dbf()

'Arturo uff 05-11-02
'Set rs = db.OpenRecordset("select * from Par_cfg order by DESCR,FOR_MAC, N_MOT_L, N_MOT_AB;", dbOpenSnapshot)
Set rs = db.OpenRecordset("select * from Par_cfg order by DESCR,FOR_MAC,GRA_CAR, N_MOT_L, N_MOT_AB;", dbOpenSnapshot)
If rs.RecordCount > 0 Then
    rs.MoveFirst
    Do While Not rs.EOF
'Arturo uff 05-11-02
'      If Trim$(par_mac.config_sch_tag_att(livello).descr) = rs("DESCR") And for_mac = rs("FOR_MAC") _'
      If Trim$(par_mac.config_sch_tag_att(Livello).descr) = rs("DESCR") And for_mac = rs("FOR_MAC") And gra_car = rs("GRA_CAR") _
      And num_mot_liv = rs("N_MOT_L") And num_mot_abi = rs("N_MOT_AB") Then
          Exit Do
      End If
      rs.MoveNext
    Loop
    If Not rs.EOF Then agg_lab
Else
    lab_cfg.Caption = msg("non_cfg_eli")
    dbf_vuo = True
End If

End Sub

Sub ini_lab()
lab_liv.Visible = False
L_LIV.Visible = False
lab_att.Visible = False
End Sub

Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
    rs.Close
    eve_key.chg set_par_sch_tag3
  Case Else
End Select
End Sub

Private Sub eve_key_F11(Shift As Integer)
Dim i As Integer
Dim ret As cod_ret

Select Case Shift
  Case 0
    If Not dbf_vuo Then
      If Trim$(lab_cfg.Caption) <> "DEFAULT" Then
'Arturo uff 05-11-02
'          ret = cancella_cfg(Trim$(lab_cfg.Caption), val(rs("FOR_MAC")), val(rs("N_MOT_L")), val(rs("N_MOT_AB")))
          ret = cancella_cfg(Trim$(lab_cfg.Caption), val(rs("FOR_MAC")), val(rs("GRA_CAR")), val(rs("N_MOT_L")), val(rs("N_MOT_AB")))
          If ret = RET_BAD Then 'si cerca di eliminare una configurazione attiva
              messaggio msg("del_cfg_att"), True, False
          Else
              messaggio msg("ope_eff"), False, False
          End If
      Else
          messaggio msg("del_cfg_def"), True, False
      End If
      rs.Close
      eve_key.chg set_par_sch_tag3
    End If
  Case Else
End Select
End Sub

Private Sub Form_Activate()

Select Case par_mac.car_tec.for_mac + 1
  Case mm1800
    for_mac = 1800
  Case mm2500
    for_mac = 2500
  Case mm2800
    for_mac = 2800
End Select
gra_car = get_gra_car(par_mac.car_tec.gra_car) 'Arturo uff 05-11-02
num_mot_liv = val(par_mac.car_tec.num_mot_liv_tag)
num_mot_abi = val(par_abi.num_mot_abi(Livello))
dbf_vuo = False

ini_lab
ini_dbf
txt_idle.SetFocus
End Sub

Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
Select Case KeyCode
  Case 39
    If rs.RecordCount > 0 Then
        rs.MoveNext
        If rs.EOF Then rs.MovePrevious
        agg_lab
    End If
  Case 37
    If rs.RecordCount > 0 Then
        rs.MovePrevious
        If rs.BOF Then rs.MoveNext
        agg_lab
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


