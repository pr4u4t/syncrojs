VERSION 5.00
Object = "{5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0"; "Msflxgrd.ocx"
Begin VB.Form all_qe2 
   Appearance      =   0  'Flat
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6555
   ClientLeft      =   -195
   ClientTop       =   3600
   ClientWidth     =   11940
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6555
   ScaleWidth      =   11940
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox qe2_det_all 
      BackColor       =   &H00FFFFFF&
      BeginProperty Font 
         Name            =   "Comic Sans MS"
         Size            =   14.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   510
      Left            =   30
      TabIndex        =   1
      TabStop         =   0   'False
      Text            =   "Text1"
      Top             =   5625
      Width           =   11895
   End
   Begin MSFlexGridLib.MSFlexGrid qe2_griglia 
      Height          =   5205
      Left            =   30
      TabIndex        =   0
      Top             =   420
      Width           =   11880
      _ExtentX        =   20955
      _ExtentY        =   9181
      _Version        =   393216
      FixedRows       =   0
      FixedCols       =   0
      BackColor       =   16777215
      ForeColor       =   64
      BackColorSel    =   16777215
      ForeColorSel    =   0
      BackColorBkg    =   16777215
      GridColor       =   0
      WordWrap        =   -1  'True
      FocusRect       =   2
      GridLinesFixed  =   1
      ScrollBars      =   0
      GridLineWidth   =   4
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "ALLARMI Q.E. SLAVE"
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
      TabIndex        =   2
      Top             =   0
      Width           =   7965
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "all_qe2.frx":0000
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "all_qe2"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim all_new_mac As String, all_old_mac As String
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Private WithEvents eve_sta As ClasseStato
Attribute eve_sta.VB_VarHelpID = -1

Private Function val_bit(rig As Integer, col As Integer) As Boolean
' ritorna il valore del bit associato alla cella
Dim i As Integer
Dim a As String

i = pos_ind(rig, col)          ' posizione nell' array indirizzi
If Not IsNull(ele_all_qe2(0, i)) Then
  If Left$(ele_all_qe2(0, i), 1) = "L" Then
    i = val(Right$(ele_all_qe2(0, i), 2)) + NUM_ALL
  Else
    a = Mid$(ele_all_qe2(0, i), 1, Len(ele_all_qe2(0, i)) - 2)
    i = (val(a) * 16 + val(Right$(ele_all_qe2(0, i), 2)) + 1)  ' posizione nella stringa dei bit
  End If
  If Asc(Rie_nul(all_new_mac)) = 0 Then
    val_bit = 1
  Else
    val_bit = val(Mid$(all_new_mac, i, 1))
  End If
Else
  val_bit = False   ' casella non assegnata
End If
End Function
Private Function scr(rig As Integer, col As Integer) As String
' ritorna il messaggio associato alla cella
Dim i As Integer
  scr = ele_all_qe2(0, pos_ind(rig, col)) & ""
  If (scr = "") Then scr = Space$(3)
End Function

Private Function pos_ind(rig As Integer, col As Integer) As Integer
  pos_ind = col + qe2_griglia.Cols * rig  ' posizione nell' array indirizzi
End Function
Sub leg_all()
' imposta gli allarmi nella griglia prendendoli da var_ala
Dim k As Integer, j As Integer

  leg_dat ele_all_qe2, "select * from all_qe2", RIG_ALL * COL_ALL  ' legge in memoria all_qe2.dbf
  qe2_griglia.Redraw = False
  For k = 0 To RIG_ALL - 1
      For j = 0 To COL_ALL - 1
        qe2_griglia.TextMatrix(k, j) = msg("all_qe2_" + scr(k, j))
        qe2_griglia.Row = k
        qe2_griglia.col = j
        If qe2_griglia.TextMatrix(k, j) = "" Then
            qe2_griglia.CellBackColor = vbWhite
        End If
      Next j
  Next k
  qe2_griglia.Redraw = True
  qe2_griglia_EnterCell
End Sub


Private Sub agg_col_gri()
Dim i As Integer, j As Integer
Dim des As String


If all_old_mac <> all_new_mac Then
'qe2_griglia_EnterCell         ' aggiorna il testo in fondo alla pagina
'aggiorna i colori della griglia
qe2_griglia.Redraw = False
For i = 0 To qe2_griglia.Rows - 1
    For j = 0 To qe2_griglia.Cols - 1
      qe2_griglia.Row = i
      qe2_griglia.col = j
      If val_bit(i, j) Then
        If qe2_griglia.CellBackColor <> COL_ALL_Red Then
            des = msg("all_qe2_" + scr(i, j))
            ins_freq_all "f_a_qe2", des
            ins_sto des, Date, Time
        End If
        qe2_griglia.CellBackColor = COL_ALL_Red
        qe2_griglia.CellForeColor = vbWhite

      Else
        qe2_griglia.CellBackColor = vbWhite
        qe2_griglia.CellForeColor = vbBlack
      End If
    Next j
Next i
qe2_griglia.Redraw = True
all_old_mac = all_new_mac
End If
End Sub

Private Sub eve_key_F5(Shift As Integer)
  Select Case Shift
    Case 0
      If Not (pro_set.ute_wrk_liv1) And Not (pro_set.ute_wrk) Then
        eve_key.chg Pwd_frm, True
      Else
        eve_key.chg dia_sta_azi
      End If
      pro_set.ute_wrk_liv1 = False
    Case Else
  End Select
End Sub

Private Sub eve_key_F6(Shift As Integer)
  Select Case Shift
    Case 0
      If Not (pro_set.ute_wrk_liv1) And Not (pro_set.ute_wrk) Then
        eve_key.chg Pwd_frm, True
      Else
        eve_key.chg dia_sch_tag
      End If
      pro_set.ute_wrk_liv1 = False
    Case Else
  End Select
End Sub


Private Sub eve_key_F7(Shift As Integer)
  Select Case Shift
    Case 0
      eve_key.chg fre_qe2
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


Private Sub Form_Deactivate()
qe2_griglia_LeaveCell
End Sub

Private Sub qe2_griglia_EnterCell()

Dim i As Integer, s As String, v As Boolean

If qe2_griglia.Redraw Then
    s = scr(qe2_griglia.Row, qe2_griglia.col)
    v = val_bit(qe2_griglia.Row, qe2_griglia.col)
    qe2_griglia.CellBackColor = IIf(v, COL_ALL_Red, vbWhite)  ' per foreground
    qe2_det_all.ForeColor = IIf(v, COL_ALL_Red, vbBlack)
    qe2_griglia.CellBackColor = IIf(qe2_griglia.CellBackColor = COL_ALL_Red, COL_ALL_pink, COL_Green)  ' per foreground
    qe2_griglia.CellForeColor = IIf(qe2_griglia.CellBackColor = COL_ALL_pink, vbBlack, vbWhite)
    qe2_det_all.Text = s + " " + msg("det_all_qe2_" + s)
    If v And Left$(s, 1) = "L" Then     ' visualizza anche il codice di errore LENZ
      i = val(Right$(s, 2) - 1) * 2 + 1
      qe2_det_all.Text = qe2_det_all.Text + " "  ' + sta_mac.Sta_azi_LEN(i) + " " + sta_mac.Sta_azi_LEN(i + 1)
    End If
End If

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

Private Sub eve_key_F2(Shift As Integer)
'pagina successiva
  Select Case Shift
    Case 0
      eve_key.chg all_qe3
    Case Else
  End Select

End Sub


Private Sub eve_key_F3(Shift As Integer)
'pagina precedente
  Select Case Shift
    Case 0
      eve_key.chg all_qe1
    Case Else
  End Select

End Sub


Private Sub eve_sta_sezall(all_PLC As String)
all_new_mac = all_PLC
agg_col_gri
End Sub


Private Sub Form_Activate()
  leg_all   ' imposta le scritte nella form
  qe2_griglia.col = 0
  qe2_griglia.Row = 0
End Sub

Private Sub Form_Load()
Dim k As Integer

Debug.Print "Inizio Load " & Me.name
Carica Me

Set eve_key = New key
eve_key.create Me
Set eve_sta = sta_tag

leg_dat ele_all_qe2, "select * from all_qe2", RIG_ALL * COL_ALL  ' legge in memoria all_qe2.dbf
all_new_mac = "0"
 ' imposta dimensioni qe2_griglia
qe2_griglia.Cols = COL_ALL 'numero di colonne
qe2_griglia.Rows = RIG_ALL  'numero di righe

For k = 0 To qe2_griglia.Cols - 1
    qe2_griglia.ColWidth(k) = qe2_griglia.Width / (qe2_griglia.Cols) * 0.99
    qe2_griglia.ColAlignment(k) = 4
Next k
  
For k = 0 To qe2_griglia.Rows - 1
    qe2_griglia.RowHeight(k) = qe2_griglia.Height / (qe2_griglia.Rows) * 0.99
Next k

'imposta scritte nella qe2_griglia
leg_all
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub

Private Sub qe2_griglia_LeaveCell()
If qe2_griglia.Redraw Then
  If qe2_griglia.CellBackColor = COL_ALL_pink Then
    qe2_griglia.CellBackColor = COL_ALL_Red   ' per foreground
    qe2_griglia.CellForeColor = vbWhite
  Else
    qe2_griglia.CellBackColor = qe2_griglia.BackColor    ' per foreground
    qe2_griglia.CellForeColor = vbBlack
  End If
End If
End Sub


