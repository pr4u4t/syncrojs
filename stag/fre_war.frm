VERSION 5.00
Object = "{00028C01-0000-0000-0000-000000000046}#1.0#0"; "DBGRID32.OCX"
Begin VB.Form fre_war 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   0  'None
   ClientHeight    =   6345
   ClientLeft      =   -1170
   ClientTop       =   3240
   ClientWidth     =   11955
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6345
   ScaleWidth      =   11955
   ShowInTaskbar   =   0   'False
   Begin MSDBGrid.DBGrid dbgrid_a_w 
      Bindings        =   "fre_war.frx":0000
      Height          =   375
      Left            =   30
      OleObjectBlob   =   "fre_war.frx":003A
      TabIndex        =   1
      Top             =   330
      Width           =   11955
   End
   Begin VB.Data data_a_w 
      Connect         =   "dBASE III;"
      DatabaseName    =   ""
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   345
      Left            =   10290
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   1  'Dynaset
      RecordSource    =   ""
      Top             =   5880
      Visible         =   0   'False
      Width           =   1215
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "FREQUENZE  MALFUNZIONAMENTI"
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
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   7785
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "fre_war.frx":0A18
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "fre_war"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1

Private Sub eve_key_F1(Shift As Integer)
 Select Case Shift
    Case 0
      eve_key.chg pag_all
    Case Else
  End Select

End Sub


Private Sub eve_key_F10(Shift As Integer)
 Select Case Shift
    Case 0
      eve_key.chg pag_pro
    Case Else
  End Select

End Sub


Private Sub eve_key_F8(Shift As Integer)
 Select Case Shift
    Case 0
      eve_key.chg pag_lis_ese
    Case Else
  End Select
End Sub

Private Sub eve_key_F9(Shift As Integer)
 Select Case Shift
    Case 0
      eve_key.chg pag_lis_ord
    Case Else
  End Select
End Sub


Private Sub Form_Activate()
Debug.Print "Inizio Load " & Me.name
Set data_a_w.Recordset = db.OpenRecordset("select * from f_a_war order by  FREQ DESC;", dbOpenSnapshot)
data_a_w.Refresh
dbgrid_a_w.ReBind
dbgrid_a_w.TabStop = True
dbgrid_a_w.SetFocus
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub

Private Sub Form_Load()
Dim i As Integer
'On Error GoTo STAG_Gest_ERR
Debug.Print "Inizio Load " & Me.name
data_a_w.DatabaseName = Left$(pro_set.pro_pat, Len(pro_set.pro_pat) - 1)
Set eve_key = New key
eve_key.create Me
Carica Me
dbgrid_a_w.Width = Me.Width
dbgrid_a_w.Left = 0
dbgrid_a_w.Top = Image3.Height + Image3.Top + 20 * pro_set.PercY / 100
dbgrid_a_w.Height = Me.Height - dbgrid_a_w.Top
Debug.Print "Fine Load " & Me.name
Debug.Print ""
'Exit Sub
'STAG_Gest_ERR:
'Resume Next

End Sub


