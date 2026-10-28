VERSION 5.00
Object = "{5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0"; "Msflxgrd.ocx"
Begin VB.Form all_con 
   Appearance      =   0  'Flat
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6345
   ClientLeft      =   1470
   ClientTop       =   2820
   ClientWidth     =   11970
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6345
   ScaleWidth      =   11970
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox con_det_all 
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
      Top             =   5640
      Width           =   11895
   End
   Begin MSFlexGridLib.MSFlexGrid con_griglia 
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
      Caption         =   "ALLARMI CONSOLLE"
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
      Picture         =   "all_con.frx":0000
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "all_con"
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
If Not IsNull(ele_all_con(0, i)) Then
  If Left$(ele_all_con(0, i), 1) = "L" Then
    i = val(Right$(ele_all_con(0, i), 2)) + NUM_ALL
  Else
    a = Mid$(ele_all_con(0, i), 1, Len(ele_all_con(0, i)) - 2)
    i = (val(a) * 16 + val(Right$(ele_all_con(0, i), 2)) + 1)  ' posizione nella stringa dei bit
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
  scr = ele_all_con(0, pos_ind(rig, col)) & ""
  If (scr = "") Then scr = Space$(3)
End Function

Private Function pos_ind(rig As Integer, col As Integer) As Integer
  pos_ind = col + con_griglia.Cols * rig  ' posizione nell' array indirizzi
End Function

Sub leg_all()
' imposta gli allarmi nella griglia prendendoli da var_ala
Dim k As Integer, j As Integer

leg_dat ele_all_con, "select * from all_con", RIG_ALL * COL_ALL
con_griglia.Redraw = False
For k = 0 To RIG_ALL - 1
    For j = 0 To COL_ALL - 1
        con_griglia.TextMatrix(k, j) = msg("all_con_" + scr(k, j))
        con_griglia.Row = k
        con_griglia.col = j
        If con_griglia.TextMatrix(k, j) = "" Then
            con_griglia.CellBackColor = vbWhite
        End If
    Next j
Next k
con_griglia.Redraw = True
con_griglia_EnterCell
End Sub

Private Sub agg_col_gri()
Dim i As Integer, j As Integer
Dim des As String

If all_old_mac <> all_new_mac Then
'con_griglia_EnterCell         ' aggiorna il testo in fondo alla pagina
'aggiorna i colori della griglia
con_griglia.Redraw = False
For i = 0 To con_griglia.Rows - 1
    For j = 0 To con_griglia.Cols - 1
      con_griglia.Row = i
      con_griglia.col = j
      If val_bit(i, j) Then
        If con_griglia.CellBackColor <> COL_ALL_Red Then
            des = msg("all_con_" + scr(i, j))
            ins_freq_all "f_a_con", des
            ins_sto des, Date, Time
        End If
        con_griglia.CellBackColor = COL_ALL_Red
        con_griglia.CellForeColor = vbWhite

      Else
        con_griglia.CellBackColor = vbWhite
        con_griglia.CellForeColor = vbBlack
      End If
    Next j
Next i
con_griglia.Redraw = True
all_old_mac = all_new_mac
End If
End Sub

Private Sub con_griglia_EnterCell()

Dim i As Integer, s As String, v As Boolean

If con_griglia.Redraw Then
    s = scr(con_griglia.Row, con_griglia.col)
    v = val_bit(con_griglia.Row, con_griglia.col)
    con_griglia.CellBackColor = IIf(v, COL_ALL_Red, vbWhite)  ' per foreground
    con_det_all.ForeColor = IIf(v, COL_ALL_Red, vbBlack)
    con_griglia.CellBackColor = IIf(con_griglia.CellBackColor = COL_ALL_Red, COL_ALL_pink, COL_Green)  ' per foreground
    con_griglia.CellForeColor = IIf(con_griglia.CellBackColor = COL_ALL_pink, vbBlack, vbWhite)
    con_det_all.Text = s + " " + msg("det_all_con_" + s)
    If v And Left$(s, 1) = "L" Then     ' visualizza anche il codice di errore LENZ
      i = val(Right$(s, 2) - 1) * 2 + 1
      con_det_all.Text = con_det_all.Text + " "  ' + sta_mac.Sta_azi_LEN(i) + " " + sta_mac.Sta_azi_LEN(i + 1)
    End If
End If

End Sub

Private Sub con_griglia_LeaveCell()
If con_griglia.Redraw Then
  If con_griglia.CellBackColor = COL_ALL_pink Then
    con_griglia.CellBackColor = COL_ALL_Red   ' per foreground
    con_griglia.CellForeColor = vbWhite
  Else
    con_griglia.CellBackColor = con_griglia.BackColor    ' per foreground
    con_griglia.CellForeColor = vbBlack
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

Private Sub eve_key_F7(Shift As Integer)
  Select Case Shift
    Case 0
      eve_key.chg fre_con
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


Private Sub eve_sta_sezall(all_PLC As String)
all_new_mac = all_PLC
agg_col_gri
End Sub


Private Sub Form_Activate()
  leg_all   ' imposta le scritte nella form
  con_griglia.col = 0
  con_griglia.Row = 0
End Sub

Private Sub Form_Deactivate()
con_griglia_LeaveCell
End Sub


Private Sub Form_Load()
Dim k As Integer

Debug.Print "Inizio Load " & Me.name
Carica Me

Set eve_key = New key
eve_key.create Me
Set eve_sta = sta_tag

leg_dat ele_all_con, "select * from all_con", RIG_ALL * COL_ALL  ' legge in memoria all_con.dbf
all_new_mac = "0"
 ' imposta dimensioni con_griglia
con_griglia.Cols = COL_ALL 'numero di colonne
con_griglia.Rows = RIG_ALL  'numero di righe

For k = 0 To con_griglia.Cols - 1
    con_griglia.ColWidth(k) = con_griglia.Width / (con_griglia.Cols) * 0.99
    con_griglia.ColAlignment(k) = 4
Next k
  
For k = 0 To con_griglia.Rows - 1
    con_griglia.RowHeight(k) = con_griglia.Height / (con_griglia.Rows) * 0.99
Next k

'imposta scritte nella con_griglia
leg_all
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub
