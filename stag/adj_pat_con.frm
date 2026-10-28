VERSION 5.00
Begin VB.Form adj_pat_con 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   6285
   ClientLeft      =   1500
   ClientTop       =   2850
   ClientWidth     =   11910
   ControlBox      =   0   'False
   BeginProperty Font 
      Name            =   "Arial"
      Size            =   8.25
      Charset         =   0
      Weight          =   700
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   ForeColor       =   &H00FFFFFF&
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6285
   ScaleWidth      =   11910
   ShowInTaskbar   =   0   'False
   Begin VB.ComboBox cmb_pat_con_sel 
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   18
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00008000&
      Height          =   555
      Left            =   4260
      Style           =   2  'Dropdown List
      TabIndex        =   0
      Top             =   1755
      Width           =   1095
   End
   Begin VB.Label Lab_proc_acq 
      Alignment       =   2  'Center
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "!! PROCEDURA DI ACQUISIZIONE IN CORSO !!"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   21.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000080FF&
      Height          =   1215
      Left            =   690
      TabIndex        =   5
      Top             =   3810
      Width           =   10485
   End
   Begin VB.Shape Sha_proc_acq 
      BorderColor     =   &H00000000&
      Height          =   1785
      Left            =   390
      Top             =   3510
      Width           =   11085
   End
   Begin VB.Shape Shape2 
      Height          =   765
      Left            =   390
      Top             =   840
      Width           =   5055
   End
   Begin VB.Shape Shape1 
      Height          =   765
      Left            =   390
      Top             =   1650
      Width           =   5055
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "ACQUISIZIONE CONTRASTO PATTERN"
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
      TabIndex        =   4
      Top             =   -30
      Width           =   7965
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "adj_pat_con.frx":0000
      Stretch         =   -1  'True
      Top             =   -15
      Width           =   11970
   End
   Begin VB.Label Lab_liv 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Livello"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00800000&
      Height          =   435
      Left            =   495
      TabIndex        =   3
      Top             =   1110
      Width           =   1095
   End
   Begin VB.Label txt_liv_tag 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   24
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00008000&
      Height          =   585
      Left            =   1890
      TabIndex        =   2
      Top             =   960
      Width           =   435
   End
   Begin VB.Label Lab_con_memo 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Acquisisci Area Contrasto N. "
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00800000&
      Height          =   405
      Left            =   495
      TabIndex        =   1
      Top             =   1875
      Width           =   3525
   End
End
Attribute VB_Name = "adj_pat_con"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
'Icilio3545 04-01-2004
Private WithEvents rmt_adj_pat_con_macro As rmt
Attribute rmt_adj_pat_con_macro.VB_VarHelpID = -1

Dim tog_vis_pat_con As Boolean

Sub fill_cmb_pat_con(Optional free As Boolean = True)
'Valorizza la cmb_pat_con_sel con i valori per contrasto
Dim ss As Recordset
Dim arr_val As Variant
Dim ind As Integer

On Local Error GoTo ErrorHandler

  With cmb_pat_con_sel
    If Not free Then
      'Tutti i valori possibili
      .Clear
      'Icilio3545 04-01-2004 Modificati dati contrasto
      .AddItem "1": .ItemData(.NewIndex) = "01"
      .AddItem "2": .ItemData(.NewIndex) = "02"
      .AddItem "3": .ItemData(.NewIndex) = "03"
      .AddItem "4": .ItemData(.NewIndex) = "04"
    Else
      'Solo i valori disponibili
      arr_val = Array(1, 2, 3, 4)
      Set ss = db.OpenRecordset("select distinct CONTRASTO from PATTERN order by CONTRASTO", dbOpenSnapshot)
      If ss.RecordCount > 0 Then
        ss.MoveLast: ss.MoveFirst
        .Clear
        Do While Not ss.EOF
          For ind = 0 To UBound(arr_val)
            If val(arr_val(ind)) = val(ss!CONTRASTO) And arr_val(ind) <> "" Then arr_val(ind) = "": Exit For
          Next
          ss.MoveNext
        Loop
        ss.Close
'        Debug.Print "> Aree Fotocellula libere"
        For ind = 0 To UBound(arr_val)
          If arr_val(ind) <> "" Then
'            Debug.Print "Area N." & str(arr_val(ind)) & " libera"
            .AddItem arr_val(ind): .ItemData(.NewIndex) = Format(val(arr_val(ind)) - 1, "00")
          End If
        Next
        If .ListCount > 0 Then .ListIndex = 0
      Else
        messaggio msg("err_no_pat_con_lib"), True
      End If
      Set ss = Nothing
    End If
  End With
  Exit Sub

ErrorHandler:
  log_str "fill_cmb_pat_con: " & Format(err.Number) & " " & err.Description, MED_PRIORITY, log_sch_tag
  Set ss = Nothing

End Sub


Sub Vis_proc_Acq(vis As Boolean)
  
  Lab_proc_acq.Visible = vis
  Sha_proc_acq.Visible = vis

End Sub

Private Sub eve_key_F1(Shift As Integer)
  
On Local Error GoTo ErrorHandler

  Select Case Shift
    Case 0
      'Visualizza tutti i valori di contrasto/quelli liberi
      fill_cmb_pat_con tog_vis_pat_con
      tog_vis_pat_con = IIf(tog_vis_pat_con, False, True)
      If cmb_pat_con_sel.ListCount > 0 Then cmb_pat_con_sel.ListIndex = 0
      cmb_pat_con_sel.SetFocus
  End Select
  Exit Sub
  
ErrorHandler:
  log_str "eve_key_F1: " & Format(err.Number) & " " & err.Description, MED_PRIORITY, log_sch_tag
  If cmb_pat_con_sel.ListCount > 0 Then cmb_pat_con_sel.ListIndex = 0
  cmb_pat_con_sel.SetFocus
  
End Sub


Private Sub eve_key_F10(Shift As Integer)
  
  Select Case Shift
    Case 0
      eve_key.chg pag_pro
    Case Else
  End Select

End Sub

Private Sub eve_key_F2(Shift As Integer)
  'cambio livello
  If Not sta_sch_tag.Set_ftc_run Then
    If par_mac.car_tec.num_liv_tag > 1 Then
      If Livello < par_mac.car_tec.num_liv_tag Then
        Livello = Livello + 1
        txt_liv_tag.Caption = Livello
      Else
        Livello = 1
        txt_liv_tag.Caption = Livello
      End If
    End If
  Else
    messaggio msg("err_pro_acq_cor"), True
  End If
End Sub

Private Sub eve_key_F3(Shift As Integer)
  
  Select Case Shift
    Case 0
      'Invia contrasto a scheda di taglio
      If sta_tag.MAC_F_STOP(1) Then   'Icilio3545 09-01-2004
        If (cmb_pat_con_sel.ListIndex <> -1 Or Not sta_sch_tag.Set_ftc_run) Then
          If rmt_adj_pat_con_macro.esegui(tip_ope_macro.ACQ_CON_SCH_PLC, GetTickCount(), Livello, 1, cmb_pat_con_sel.ItemData(cmb_pat_con_sel.ListIndex)) Then
            messaggio msg("ope_in_cor"), False
            cmb_pat_con_sel.Enabled = False
            sta_sch_tag.Set_ftc_run = True
          Else
            messaggio msg("imp_ese_ope"), True
          End If
        Else
          messaggio msg("imp_ese_ope"), True
        End If
      Else
        messaggio msg("mac_non_fas_sto"), True
      End If
  End Select

End Sub


Private Sub eve_key_F6(Shift As Integer)
'Procedura di apprendimento contrasto terminata
  
  If sta_sch_tag.Set_ftc_run Then
    sta_sch_tag.Set_ftc_run = False
    cmb_pat_con_sel.Enabled = True
    'icilio3545 04-01-2004 Nascondo il messaggio di acquisizione in corso a procedura terminata
    Vis_proc_Acq False
  Else
    messaggio msg("err_no_pro_acq_cor"), True
  End If
  
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
  
  fill_cmb_pat_con
  Livello = 1
  Vis_proc_Acq False
  
End Sub

Private Sub Form_Load()

  Debug.Print "Inizio Load " & Me.name
  Set eve_key = New key
  'Icilio3545 04-01-2004
  Set rmt_adj_pat_con_macro = New rmt
  rmt_adj_pat_con_macro.set_plc_ute (3)
  rmt_adj_pat_con_macro.name = "rmt_adj_pat_con_macro"
  eve_key.create Me
  Carica Me
'  Ridim Me
'  Center Me
'  dimensiona Me
  
  Debug.Print "Fine Load " & Me.name
  Debug.Print ""
  
  
End Sub


Private Sub rmt_adj_pat_con_macro_retbad(err As String)
'Gestisce risposte negative da macro
  
  sta_sch_tag.Set_ftc_run = False
  If err <> "" Then messaggio err, True
  rmt_adj_pat_con_macro.resetta
  cmb_pat_con_sel.Enabled = True
  Vis_proc_Acq False
  
End Sub

Private Sub rmt_adj_pat_con_macro_retok(s As String)
'Gestisce risposte positive da macro
  
  sta_sch_tag.Set_ftc_run = True
  If s <> "" Then messaggio s, False
  rmt_adj_pat_con_macro.resetta
  Vis_proc_Acq True
  
End Sub






