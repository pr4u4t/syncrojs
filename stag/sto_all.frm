VERSION 5.00
Object = "{00028C01-0000-0000-0000-000000000046}#1.0#0"; "DBGRID32.OCX"
Begin VB.Form sto_all 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   0  'None
   ClientHeight    =   6705
   ClientLeft      =   -690
   ClientTop       =   3870
   ClientWidth     =   11940
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6705
   ScaleWidth      =   11940
   ShowInTaskbar   =   0   'False
   Begin MSDBGrid.DBGrid dbgrid_sto_all 
      Bindings        =   "sto_all.frx":0000
      Height          =   375
      Left            =   0
      OleObjectBlob   =   "sto_all.frx":0024
      TabIndex        =   1
      Top             =   330
      Width           =   11955
   End
   Begin VB.Data data_sto_all 
      Connect         =   "dBASE III;"
      DatabaseName    =   ""
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   375
      Left            =   8400
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   2  'Snapshot
      RecordSource    =   ""
      Top             =   5760
      Visible         =   0   'False
      Width           =   1215
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "STORICO ALLARMI"
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
      Picture         =   "sto_all.frx":0BA6
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "sto_all"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Sub comp_sto(name As String)
' compatta il file dbf eliminando i record cancellati (che sono stati marcati)
'Dim cmd As String 'Arturo uff 08-02-03 (2)
'Dim i As Integer  'Arturo uff 08-02-03 (2)
Dim rs As Recordset
'Dim tb1 As Recordset  'Arturo uff 08-02-03 (2)
'Dim tb2 As Recordset  'Arturo uff 08-02-03 (2)

On Error GoTo err_comp_sto
'Arturo uff 08-02-03 (2)
'Set rs = db.OpenRecordset(name, dbOpenDynaset)
'If Dir$(pro_set.pro_pat & "t2_" & name & ".dbf") <> "" Then Kill (pro_set.pro_pat & "t2_" & name & ".dbf")
'
'db.Execute ("create table t2_" & name & "(ALLARME TEXT(50),DATA_ALL DATE, ORA_ALL TEXT(8))") 'testata
'Set tb2 = db.OpenRecordset("t2_" & name, dbOpenDynaset)
'
'Set data_sto_all.Recordset = tb2
'
'If Dir$(pro_set.pro_pat & "t1_" & name & ".dbf") <> "" Then Kill (pro_set.pro_pat & "t1_" & name & ".dbf")
'
'    cmd = "create table t1_" & name & "(ALLARME TEXT(50),DATA_ALL DATE, ORA_ALL TEXT(8))"  'testata
'    db.Execute (cmd)
'    Set tb1 = db.OpenRecordset("t1_" & name, dbOpenDynaset)
'    rs.MoveFirst
'    While Not rs.EOF
'            tb1.AddNew
'            For i = 0 To 2
'                tb1(i) = rs(i)
'            Next i
'            tb1.Update
'            rs.MoveNext
'    Wend
'    rs.Close
'
'    If Dir$(pro_set.pro_pat & name & ".dbf") <> "" Then
'        Kill (pro_set.pro_pat & name & ".dbf")
'    End If
'    tb1.Close
'    FileCopy pro_set.pro_pat & "t1_" & name & ".dbf", pro_set.pro_pat & name & ".dbf"
'    Kill (pro_set.pro_pat & "t1_" & name & ".dbf")
'    Set data_sto_all.Recordset = db.OpenRecordset("sto", dbOpenDynaset)
'    tb2.Close
'    Kill (pro_set.pro_pat & "t2_" & name & ".dbf")
  If Dir$(pro_set.pro_pat & "t1_" & name & ".dbf") <> "" Then Kill (pro_set.pro_pat & "t1_" & name & ".dbf")
  db.Execute ("SELECT * INTO t1_" & name & " FROM " & name)
  FileCopy pro_set.pro_pat & "t1_" & name & ".dbf", pro_set.pro_pat & name & ".dbf"
  Kill (pro_set.pro_pat & "t1_" & name & ".dbf")
Exit Sub
err_comp_sto:
  If Dir$(pro_set.pro_pat & name & ".dbf") <> "" Then Kill (pro_set.pro_pat & name & ".dbf")
  db.Execute ("create table " & name & "(ALLARME TEXT(50),DATA_ALL DATE, ORA_ALL TEXT(8))")
  log_str "Err.Number='" & err.Number & "'; Err.Desc='" & err.Description & "'", MED_PRIORITY, log_gen
End Sub


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
Dim rs As Recordset
Static Done As Boolean 'Arturo uff 07-04-03

If Not Done Then 'La prima volta che si entra nella form si compatta il db (si tolgono i record vuoti) 'Arturo uff 07-04-03
  Set rs = db.OpenRecordset("sto.dbf", dbOpenTable)
  If rs.RecordCount > NUM_REC_STO Then
      rs.Close
      comp_sto ("sto")
  End If
  Done = True
End If

Set data_sto_all.Recordset = db.OpenRecordset("select * from sto order by DATA_ALL DESC, ORA_ALL DESC ;", dbOpenSnapshot)
data_sto_all.Refresh
dbgrid_sto_all.ReBind
dbgrid_sto_all.TabStop = True
dbgrid_sto_all.SetFocus

End Sub

Private Sub Form_Load()
Dim i As Integer
Dim rs As Recordset
Debug.Print "Inizio Load " & Me.name
Set rs = db.OpenRecordset("sto.dbf", dbOpenTable)
If rs.RecordCount > NUM_REC_STO Then
    rs.Close
    comp_sto ("sto")
End If

data_sto_all.DatabaseName = Left$(pro_set.pro_pat, Len(pro_set.pro_pat) - 1)
Set eve_key = New key
eve_key.create Me
Carica Me
dbgrid_sto_all.Width = Me.Width
dbgrid_sto_all.Left = 0
dbgrid_sto_all.Top = Image3.Height + Image3.Top + 20
dbgrid_sto_all.Height = Me.Height - dbgrid_sto_all.Top
Debug.Print "Fine Load " & Me.name
Debug.Print ""

End Sub


