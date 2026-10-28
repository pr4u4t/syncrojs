VERSION 5.00
Begin VB.Form Sel_lin 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   1935
   ClientLeft      =   3315
   ClientTop       =   2340
   ClientWidth     =   6990
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
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   ScaleHeight     =   1935
   ScaleWidth      =   6990
   ShowInTaskbar   =   0   'False
   Begin VB.ComboBox cmb_lin 
      Appearance      =   0  'Flat
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
      Left            =   1950
      Style           =   2  'Dropdown List
      TabIndex        =   2
      Top             =   585
      Width           =   3195
   End
   Begin VB.CommandButton tas_F1 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      Caption         =   "F5"
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
      Left            =   2888
      TabIndex        =   0
      TabStop         =   0   'False
      Top             =   1155
      Width           =   1215
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "SELEZIONE LINGUA"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   15.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FFFFFF&
      Height          =   435
      Left            =   15
      TabIndex        =   3
      Top             =   0
      Width           =   4365
   End
   Begin VB.Image img_title 
      Height          =   300
      Left            =   -5040
      Picture         =   "Sel_lin.frx":0000
      Top             =   15
      Width           =   11970
   End
   Begin VB.Label lab_ese 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
      Caption         =   "Cambia"
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
      TabIndex        =   1
      Top             =   1635
      Width           =   645
   End
End
Attribute VB_Name = "Sel_lin"
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
        eve_key.chg set_par_mac
    Case Else
  End Select

End Sub

Private Sub eve_key_F5(Shift As Integer)
  Select Case Shift
    Case 0
      If Not pro_set.ute_wrk Then messaggio msg("acc_con_pwd"), is_All:=False: Exit Sub 'solo tecnici!
      messaggio msg("ope_in_cor"), False
      'CAMBIA LINGUA
      If pro_set.lin_att <> Sel_lin.cmb_lin.ListIndex Then
        pro_set.lin_att = Sel_lin.cmb_lin.ListIndex
        chg_lin lin_att:=Sel_lin.cmb_lin.ListIndex
        setFileINIString "pro_set", "Lin_att", pro_set.lin_att, app.EXEName
        sta_tag.chg_lin
      End If
      Show_tas_des Me.name, Shift:=0
      messaggio msg("ope_eff"), False
      eve_key.chg set_par_mac
  End Select
End Sub


Private Sub Form_Activate()
Dim rs As Recordset
Dim s As String
Dim i As Integer

s = "select * from lingue"
  Set rs = db.OpenRecordset(s, dbOpenDynaset)
  cmb_lin.Clear
  i = 0
  While Not rs.EOF
    cmb_lin.AddItem rs(0), i
    i = i + 1
    rs.MoveNext
  Wend
 rs.Close
 cmb_lin.ListIndex = pro_set.lin_att
 cmb_lin.SetFocus
End Sub

Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
  'controllo che i tasti freccia vengano utilizzati come invio
  On Local Error GoTo g_err:
  If ActiveControl Is Nothing Then Exit Sub
  Select Case KeyCode
    Case vbKeyReturn, vbKeyDown
      KeyCode = 0
      key_tab shi:=False
    Case vbKeyUp
      KeyCode = 0
      key_tab shi:=True
      Ins_ord.flg_rev = True 'sto tornando sui miei passi...
  End Select
 Exit Sub
g_err:
log_str "on error intervenuta in sel_lin Form_KeyDown" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
End Sub


Private Sub Form_Load()
  Debug.Print "Inizio Load " & Me.name
  Set eve_key = New key
  eve_key.create Me
  Ridim Me
  Center Me
  dimensiona Me
  img_title.Left = Int(img_title.Left - (14 / 10) * (img_title.Left * (pro_set.PercX - 100) / 100))
  img_title.Width = img_title.Width * pro_set.PercX / 100
  Debug.Print "Fine Load " & Me.name
  Debug.Print ""
End Sub


