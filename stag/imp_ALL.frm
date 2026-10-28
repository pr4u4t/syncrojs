VERSION 5.00
Object = "{5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0"; "MSFLXGRD.OCX"
Begin VB.Form imp_all 
   Appearance      =   0  'Flat
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6300
   ClientLeft      =   2010
   ClientTop       =   1650
   ClientWidth     =   11955
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6300
   ScaleWidth      =   11955
   ShowInTaskbar   =   0   'False
   Begin VB.ComboBox txt_pag_allarmi 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      Enabled         =   0   'False
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   405
      Left            =   3300
      Style           =   2  'Dropdown List
      TabIndex        =   8
      Top             =   4710
      Width           =   5205
   End
   Begin MSFlexGridLib.MSFlexGrid add 
      Height          =   6075
      Left            =   0
      TabIndex        =   3
      Top             =   0
      Width           =   975
      _ExtentX        =   1720
      _ExtentY        =   10716
      _Version        =   393216
      Cols            =   1
      FixedCols       =   0
      BackColor       =   16777215
      ForeColor       =   0
      BackColorFixed  =   12632256
      ForeColorFixed  =   0
      BackColorSel    =   8388736
      BackColorBkg    =   16777215
      GridColor       =   0
      Enabled         =   0   'False
      FocusRect       =   2
      HighLight       =   2
      ScrollBars      =   2
   End
   Begin VB.TextBox det_all 
      BackColor       =   &H00FFFFFF&
      Enabled         =   0   'False
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   300
      Left            =   3330
      TabIndex        =   1
      Top             =   5700
      Width           =   8505
   End
   Begin VB.TextBox all 
      BackColor       =   &H00FFFFFF&
      Enabled         =   0   'False
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   300
      Left            =   3330
      TabIndex        =   2
      Top             =   5280
      Width           =   8505
   End
   Begin MSFlexGridLib.MSFlexGrid griglia 
      Height          =   4035
      Left            =   945
      TabIndex        =   0
      Top             =   480
      Width           =   10965
      _ExtentX        =   19341
      _ExtentY        =   7117
      _Version        =   393216
      FixedRows       =   0
      FixedCols       =   0
      BackColor       =   16777215
      ForeColor       =   0
      BackColorFixed  =   255
      ForeColorFixed  =   0
      BackColorSel    =   4210752
      ForeColorSel    =   16777215
      BackColorBkg    =   16777215
      GridColor       =   0
      WordWrap        =   -1  'True
      FocusRect       =   2
      HighLight       =   2
      GridLinesFixed  =   1
      ScrollBars      =   0
      GridLineWidth   =   4
   End
   Begin VB.Label Label1 
      Alignment       =   1  'Right Justify
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Pagina"
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
      Left            =   1065
      TabIndex        =   7
      Top             =   4830
      Width           =   2040
   End
   Begin VB.Line Line4 
      X1              =   960
      X2              =   3120
      Y1              =   5640
      Y2              =   5640
   End
   Begin VB.Line Line3 
      X1              =   3210
      X2              =   11865
      Y1              =   5640
      Y2              =   5640
   End
   Begin VB.Line Line2 
      BorderWidth     =   2
      X1              =   11895
      X2              =   11895
      Y1              =   6045
      Y2              =   4455
   End
   Begin VB.Line Line1 
      BorderWidth     =   2
      X1              =   810
      X2              =   11890
      Y1              =   6060
      Y2              =   6060
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "IMPOSTAZIONE ALLARMI"
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
      Left            =   1100
      TabIndex        =   6
      Top             =   0
      Width           =   6000
   End
   Begin VB.Label Lab_all 
      Alignment       =   1  'Right Justify
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Descrizione"
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
      Left            =   1050
      TabIndex        =   5
      Top             =   5325
      Width           =   2055
   End
   Begin VB.Label Lab_det_all 
      Alignment       =   1  'Right Justify
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Descriz. dettagliata"
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
      Left            =   1050
      TabIndex        =   4
      Top             =   5730
      Width           =   2040
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "imp_ALL.frx":0000
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "imp_all"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Dim ope_abi As Boolean
Dim imp_bit As Boolean
Dim riga As Integer

Private Sub ESC_imp_all()

'textbox impostazione messaggio della griglia
If all.Enabled = True Then
    griglia.Enabled = True
    griglia.SetFocus
    all.Enabled = False
    det_all.Enabled = False
    griglia_EnterCell
End If

'msflexgrid impostazione bit
If add.Enabled = True Then
    griglia.Enabled = True
    griglia.SetFocus
    add.Row = 1
    add.Enabled = False
End If

If txt_pag_allarmi.Enabled = True Then
    griglia.Enabled = True
    griglia.SetFocus
    txt_pag_allarmi.Enabled = False
End If

ope_abi = True

End Sub

Sub fill_griglia_all(pag As Integer)

'riempie la griglia con i messaggi di allarmi
Dim k As Integer, j As Integer
 Select Case pag
  Case 1
    ele_all_tmp = ele_all_mac
  Case 2
    ele_all_tmp = ele_all_har
  Case 3
    ele_all_tmp = ele_all_con
  Case 4
    ele_all_tmp = ele_all_qe1
  Case 5
    ele_all_tmp = ele_all_qe2
  Case 6
    ele_all_tmp = ele_all_qe3
  Case 7
    ele_all_tmp = ele_all_war
 End Select
 For k = 0 To RIG_ALL - 1
    For j = 0 To COL_ALL - 1
      griglia.TextMatrix(k, j) = ele_all_tmp(0, COL_ALL * (k) + j) & ""
    Next j
  Next k
End Sub

Private Sub imp_all_imp_des_all()
' imposta descrizione allarme
Static mia_ope As Boolean
Dim det_all_tmp As String, all_tmp As String

If ope_abi Or mia_ope Then   ' abilita modifica bit allarme
  If griglia.Text <> "" Then
    If add.Enabled = False Then   ' abilita modifica descrizione
      If griglia.Enabled = True Then
        griglia.Enabled = False
        all.Enabled = True
        det_all.Enabled = True
        all.SetFocus
        mia_ope = True
        ope_abi = False
      Else
        det_all_tmp = "det_all_" + des_pag_all(txt_pag_allarmi.Text) + "_"
        all_tmp = "all_" + des_pag_all(txt_pag_allarmi.Text) + "_"
        sal_des det_all_tmp, all_tmp ' salva su dbf ed imposta mg in memoria
        griglia.Enabled = True
        griglia.SetFocus
        all.Enabled = False
        det_all.Enabled = False
        mia_ope = False
        ope_abi = True
      End If
    End If
  End If
End If
End Sub

Private Sub imp_all_reset()
' resetta associazione con bit di allarme
 
  If ope_abi Then
    ele_all_tmp(0, pos_ind(griglia.Row, griglia.col)) = Null   ' allarmi.dbf in memoria
    griglia.Text = Space$(3)       ' metto nella griglia spazi
    sal_ind Space$(3), "all_" + des_pag_all(txt_pag_allarmi.Text)
    det_all.Text = ""
    all.Text = ""
  End If

End Sub

Sub sal_des(det_all_tmp As String, all_tmp As String)
' aggiorna il database messaggi
Dim tmp_rec As Recordset
Dim tmp_lin As String

  If griglia.Text <> "" Then
    If pro_set.lin_att = ITA Then
      db.Execute ("UPDATE MESSAGGI SET ITALIAN='" + all.Text + "' WHERE CONTROLLO ='" + all_tmp + griglia.Text + "'")
      db.Execute ("UPDATE MESSAGGI SET ITALIAN='" + det_all.Text + "' WHERE CONTROLLO ='" + det_all_tmp + griglia.Text + "'")
      mg.item(all_tmp + griglia.Text).item = all.Text
      mg.item(det_all_tmp + griglia.Text).item = det_all.Text
    Else
      Set tmp_rec = db.OpenRecordset("select * from messaggi", dbOpenSnapshot)
      tmp_lin = tmp_rec.Fields(3 + pro_set.lin_att).name
      tmp_rec.Close
      db.Execute ("UPDATE MESSAGGI SET " + tmp_lin + "='" + all.Text + "' WHERE CONTROLLO ='" + all_tmp + griglia.Text + "'")
      db.Execute ("UPDATE MESSAGGI SET " + tmp_lin + "='" + det_all.Text + "' WHERE CONTROLLO ='" + det_all_tmp + griglia.Text + "'")
      mg.item(all_tmp + griglia.Text).item = all.Text
      mg.item(det_all_tmp + griglia.Text).item = det_all.Text
    End If
  End If
End Sub

Private Function scr(rig As Integer, col As Integer) As String
' ritorna il messaggio associato alla cella
Dim i As Integer
  scr = ele_all_tmp(0, pos_ind(rig, col)) & ""
  If (scr = "") Then scr = Space$(3)
End Function

Private Function pos_ind(rig As Integer, col As Integer) As Integer
  pos_ind = col + griglia.Cols * rig  ' posizione nell' array indirizzi
End Function

Private Sub Imp_all_imp_all()
' imposta bit di allarme
Static mia_ope As Boolean
Dim det_all_tmp As String, all_tmp As String, nam_db_tmp As String

If ope_abi Or mia_ope Then   ' abilita modifica bit allarme
    If griglia.Enabled = True Then
      griglia.Enabled = False
      add.Enabled = True
      imp_bit = True
      add.SetFocus
      add.Row = riga
      mia_ope = True
      ope_abi = False
    Else
      det_all_tmp = "det_all_" + des_pag_all(txt_pag_allarmi.Text) + "_"
      all_tmp = "all_" + des_pag_all(txt_pag_allarmi.Text) + "_"
      nam_db_tmp = "all_" + des_pag_all(txt_pag_allarmi.Text)
      ele_all_tmp(0, pos_ind(griglia.Row, griglia.col)) = add.Text            ' allarmi.dbf in memoria
      griglia.Text = add.Text                         ' metto nella griglia l' indirizzo
      'aggiorno il db degli allarmi con l'indirizzo
      sal_ind add.Text, nam_db_tmp
      'aggiungo il messaggio e la descrizione dell'allarme
      det_all.Text = msg(det_all_tmp + scr(griglia.Row, griglia.col))
      all.Text = msg(all_tmp + scr(griglia.Row, griglia.col))
      
      griglia.Enabled = True
      griglia.SetFocus
      add.Enabled = False
      mia_ope = False
      ope_abi = True
      imp_bit = False
    End If
End If

End Sub

Private Sub sal_ind(s As String, nam_db As String)
' aggiorna il database allarmi
Dim rc As Recordset
    Set rc = db.OpenRecordset(nam_db, dbOpenTable)
    rc.Move pos_ind(griglia.Row, griglia.col)
    rc.Edit
    rc("dm") = s
    rc.Update
    rc.Close
End Sub

Private Sub add_EnterCell()
If imp_bit Then
  add.CellBackColor = COL_Green  ' per foreground
  add.CellForeColor = vbWhite
End If
End Sub

Private Sub add_GotFocus()
add.CellBackColor = COL_Green  ' per foreground
add.CellForeColor = vbWhite
End Sub

Private Sub add_LeaveCell()
add.CellBackColor = add.BackColor    ' per foreground
add.CellForeColor = vbBlack
End Sub

Private Sub add_LostFocus()
add.CellBackColor = add.BackColor    ' per foreground
add.CellForeColor = vbBlack
riga = add.Row
End Sub

Private Sub all_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub det_all_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub eve_key_esc(Shift As Integer)
  Select Case Shift
    Case 0
      ESC_imp_all
    Case Else
  End Select
  
End Sub

Private Sub eve_key_F1(Shift As Integer)
If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Sub
  Select Case Shift
    Case 0
      Imp_all_imp_all
    Case Else
  End Select
  
End Sub

Private Sub eve_key_F10(Shift As Integer)
  Select Case Shift
    Case 0
      If ope_abi Then eve_key.chg pag_pro
    Case Else
  End Select

End Sub



Private Sub eve_key_F2(Shift As Integer)
If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Sub
  Select Case Shift
    Case 0
      imp_all_imp_des_all
    Case Else
  End Select
End Sub

Private Sub eve_key_F3(Shift As Integer)
If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Sub
  Select Case Shift
    Case 0
      imp_all_reset
    Case Else
  End Select
  
End Sub
Private Sub eve_key_F4(Shift As Integer)
Static mia_ope As Boolean
If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Sub
  Select Case Shift
    Case 0
    'setta la pagina degli allarmi
      If ope_abi Or mia_ope Then
        If txt_pag_allarmi.Enabled = False Then
          txt_pag_allarmi.Enabled = True
          griglia.Enabled = False
          txt_pag_allarmi.SetFocus
          mia_ope = True
          ope_abi = False
        Else
          txt_pag_allarmi.Enabled = False
          griglia.Enabled = True
          Select Case Mid$(txt_pag_allarmi.Text, 1, 1)
              Case 1
                leg_dat ele_all_mac, "select * from all_mac", RIG_ALL * COL_ALL
              Case 2
                leg_dat ele_all_har, "select * from all_har", RIG_ALL * COL_ALL
              Case 3
                leg_dat ele_all_con, "select * from all_con", RIG_ALL * COL_ALL
              Case 4
                leg_dat ele_all_qe1, "select * from all_qe1", RIG_ALL * COL_ALL
              Case 5
                leg_dat ele_all_qe2, "select * from all_qe2", RIG_ALL * COL_ALL
              Case 6
                leg_dat ele_all_qe3, "select * from all_qe3", RIG_ALL * COL_ALL
              Case 7
                leg_dat ele_all_war, "select * from all_war", RIG_ALL * COL_ALL
          End Select
          fill_griglia_all val(Mid$(txt_pag_allarmi.Text, 1, 1))
          griglia_EnterCell
          griglia.SetFocus
          mia_ope = False
          ope_abi = True
        End If
      End If
    Case Else
  End Select
End Sub

Private Sub eve_key_F6(Shift As Integer)
'parametri macchina
  Select Case Shift
    Case 0
      If ope_abi Then eve_key.chg set_par_mac
    Case Else
  End Select

End Sub

Private Sub eve_key_F8(Shift As Integer)
'ordini eseguiti
  Select Case Shift
    Case 0
      If ope_abi Then eve_key.chg pag_lis_ese
    Case Else
  End Select

End Sub


Private Sub eve_key_F9(Shift As Integer)
'lista ordini
  Select Case Shift
    Case 0
      If ope_abi Then eve_key.chg pag_lis_ord
    Case Else
  End Select
End Sub

Private Sub Form_Activate()
  ope_abi = True
  imp_bit = False
  riga = 1
  txt_pag_allarmi.Clear
  txt_pag_allarmi.AddItem mg.item("AMAC").item
  txt_pag_allarmi.AddItem mg.item("AHAR").item
  txt_pag_allarmi.AddItem mg.item("ACON").item
  txt_pag_allarmi.AddItem mg.item("AQE1").item
  txt_pag_allarmi.AddItem mg.item("AQE2").item
  txt_pag_allarmi.AddItem mg.item("AQE3").item
  txt_pag_allarmi.AddItem mg.item("AWAR").item
  txt_pag_allarmi.ListIndex = 0
  
  'inizializzazione degli allarmi
  'da togliere
  'leg_dat ele_all_tmp, "select * from allarmi", RIG_ALL * COL_ALL
  
  leg_dat ele_all_mac, "select * from all_mac", RIG_ALL * COL_ALL
  leg_dat ele_all_har, "select * from all_har", RIG_ALL * COL_ALL
  leg_dat ele_all_con, "select * from all_con", RIG_ALL * COL_ALL
  leg_dat ele_all_qe1, "select * from all_qe1", RIG_ALL * COL_ALL
  leg_dat ele_all_qe2, "select * from all_qe2", RIG_ALL * COL_ALL
  leg_dat ele_all_qe3, "select * from all_qe3", RIG_ALL * COL_ALL
  leg_dat ele_all_war, "select * from all_war", RIG_ALL * COL_ALL
  ele_all_tmp = ele_all_mac
  
  fill_griglia_all Mid$(txt_pag_allarmi.Text, 1, 1)
  griglia_EnterCell
End Sub

Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
  Select Case KeyCode
  Case vbKeyReturn, vbKeyDown
    chk_shift 'controllo dello stato del tasto SHIFT
    KeyCode = 0
    key_tab shi:=False
  Case vbKeyUp
    chk_shift 'controllo dello stato del tasto SHIFT
    KeyCode = 0
    key_tab shi:=True
  End Select
  
End Sub

Private Sub Form_KeyPress(KeyAscii As Integer)
  If KeyAscii = vbKeyReturn Or KeyAscii = Asc("'") Then KeyAscii = 0
End Sub

Private Sub Form_Load()
Dim ds As Recordset
Dim j As Integer, k As Integer, i As Integer
  Debug.Print "Inizio Load " & Me.name
  Carica Me
  Set eve_key = New key
  eve_key.create Me
  
  txt_pag_allarmi.AddItem mg.item("AMAC").item
  txt_pag_allarmi.AddItem mg.item("AHAR").item
  txt_pag_allarmi.AddItem mg.item("ACON").item
  txt_pag_allarmi.AddItem mg.item("AQE1").item
  txt_pag_allarmi.AddItem mg.item("AQE2").item
  txt_pag_allarmi.AddItem mg.item("AQE3").item
  txt_pag_allarmi.AddItem mg.item("AWAR").item
  txt_pag_allarmi.ListIndex = 0

  'inizializzazione degli allarmi
  
  leg_dat ele_all_mac, "select * from all_mac", RIG_ALL * COL_ALL
  leg_dat ele_all_har, "select * from all_har", RIG_ALL * COL_ALL
  leg_dat ele_all_con, "select * from all_con", RIG_ALL * COL_ALL
  leg_dat ele_all_qe1, "select * from all_qe1", RIG_ALL * COL_ALL
  leg_dat ele_all_qe2, "select * from all_qe2", RIG_ALL * COL_ALL
  leg_dat ele_all_qe3, "select * from all_qe3", RIG_ALL * COL_ALL
  leg_dat ele_all_war, "select * from all_war", RIG_ALL * COL_ALL
  
  ele_all_tmp = ele_all_mac
   'aggiunge tante righe per gli azionamenti num_azi
  add.Rows = NUM_ALL + 1 + NUM_AZI
  add.Row = 0
  add.TextMatrix(0, 0) = "Indirizzo"
  
  For i = 0 To NUM_DAT_MEM_ALL - 1
    For j = 0 To 15
      add.TextMatrix(i * 16 + j + 1, 0) = Format$(i) + Format$(j, "00")
      add.ColAlignment(0) = flexAlignLeftCenter
    Next j
  Next i
  'aggiunge in fondo lo spazio per i lenze
  For i = 1 To NUM_AZI
    add.TextMatrix(i + NUM_ALL, 0) = "L" + Format$(i, "00")
  Next i

  add.Row = 1
  griglia.Cols = COL_ALL 'numero di colonne
  griglia.Rows = RIG_ALL  'numero di righe
  
  For k = 0 To griglia.Cols - 1
    griglia.ColWidth(k) = griglia.Width / (griglia.Cols)
    griglia.ColAlignment(k) = 4
  Next k
  
  For k = 0 To griglia.Rows - 1
    griglia.RowHeight(k) = griglia.Height / (griglia.Rows)
  Next k
  griglia_EnterCell
  
 fill_griglia_all 0
 Debug.Print "Fine Load " & Me.name
 Debug.Print ""
End Sub

Private Sub griglia_EnterCell()
Dim det_all_tmp As String, all_tmp As String

  det_all_tmp = "det_all_" + des_pag_all(txt_pag_allarmi.Text) + "_"
  all_tmp = "all_" + des_pag_all(txt_pag_allarmi.Text) + "_"
  griglia.CellBackColor = COL_Green  ' per foreground
  griglia.CellForeColor = vbWhite
  det_all.Text = msg(det_all_tmp + scr(griglia.Row, griglia.col))
  all.Text = msg(all_tmp + scr(griglia.Row, griglia.col))

End Sub

Private Sub griglia_LeaveCell()
griglia.CellBackColor = griglia.BackColor   ' per foreground
griglia.CellForeColor = vbBlack
End Sub




