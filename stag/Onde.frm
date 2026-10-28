VERSION 5.00
Object = "{00028C01-0000-0000-0000-000000000046}#1.0#0"; "DBGRID32.OCX"
Begin VB.Form Onde 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6570
   ClientLeft      =   -360
   ClientTop       =   1485
   ClientWidth     =   12060
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   ScaleHeight     =   6570
   ScaleWidth      =   12060
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox SPESSORE_inc 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   300
      Left            =   9930
      MaxLength       =   7
      TabIndex        =   14
      TabStop         =   0   'False
      Text            =   "1234"
      Top             =   3375
      Width           =   1575
   End
   Begin VB.TextBox VELDICAMBI_inc 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   300
      Left            =   9930
      MaxLength       =   6
      TabIndex        =   18
      TabStop         =   0   'False
      Text            =   "300"
      Top             =   2835
      Width           =   1410
   End
   Begin VB.TextBox VELDIPROD_inc 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   300
      Left            =   9930
      MaxLength       =   6
      TabIndex        =   17
      TabStop         =   0   'False
      Text            =   "300"
      Top             =   2310
      Width           =   1410
   End
   Begin VB.TextBox CODONDA 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "CODONDA"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   300
      Left            =   7230
      MaxLength       =   3
      TabIndex        =   6
      Text            =   "30"
      Top             =   3900
      Width           =   660
   End
   Begin MSDBGrid.DBGrid lis 
      Bindings        =   "Onde.frx":0000
      Height          =   5715
      Left            =   60
      OleObjectBlob   =   "Onde.frx":0010
      TabIndex        =   7
      Top             =   420
      Width           =   4335
   End
   Begin VB.TextBox VELDIPROD 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "VELDIPROD"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   300
      Left            =   7230
      MaxLength       =   3
      TabIndex        =   3
      Text            =   "300"
      Top             =   2310
      Width           =   660
   End
   Begin VB.TextBox ONDA 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "ONDA"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   300
      Left            =   7230
      MaxLength       =   3
      TabIndex        =   0
      Text            =   "ABC"
      Top             =   720
      Width           =   660
   End
   Begin VB.TextBox VELDICAMBI 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "VELDICAMBI"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   300
      Left            =   7230
      MaxLength       =   3
      TabIndex        =   4
      Text            =   "300"
      Top             =   2835
      Width           =   660
   End
   Begin VB.TextBox GRAMMATURA 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "GRAMMATURA"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   300
      Left            =   7230
      MaxLength       =   5
      TabIndex        =   2
      Text            =   "12345"
      Top             =   1785
      Width           =   1050
   End
   Begin VB.TextBox SPESSORE 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "SPESSORE"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   300
      Left            =   7230
      MaxLength       =   4
      TabIndex        =   5
      Text            =   "1234"
      Top             =   3375
      Width           =   825
   End
   Begin VB.TextBox CARTA 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "CARTA"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   300
      Left            =   7230
      MaxLength       =   36
      TabIndex        =   1
      Text            =   "A"
      Top             =   1245
      Width           =   4695
   End
   Begin VB.Data Data1 
      Connect         =   "dBASE III;"
      DatabaseName    =   "C:\usr\stag"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   345
      Left            =   5520
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   1  'Dynaset
      RecordSource    =   "select * from onde order by onda, carta"
      Top             =   5850
      Visible         =   0   'False
      Width           =   1500
   End
   Begin VB.Label L_codonda 
      Alignment       =   1  'Right Justify
      BackStyle       =   0  'Transparent
      Caption         =   "Codice onda"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   225
      Left            =   4455
      TabIndex        =   16
      Top             =   3915
      Width           =   2595
      WordWrap        =   -1  'True
   End
   Begin VB.Line Line1 
      Index           =   6
      X1              =   15000
      X2              =   4275
      Y1              =   4260
      Y2              =   4260
   End
   Begin VB.Line Line1 
      Index           =   5
      X1              =   15000
      X2              =   4275
      Y1              =   3735
      Y2              =   3735
   End
   Begin VB.Line Line1 
      Index           =   4
      X1              =   15000
      X2              =   4275
      Y1              =   3195
      Y2              =   3195
   End
   Begin VB.Line Line1 
      Index           =   3
      X1              =   15000
      X2              =   4275
      Y1              =   2670
      Y2              =   2670
   End
   Begin VB.Line Line1 
      Index           =   2
      X1              =   15000
      X2              =   4275
      Y1              =   2145
      Y2              =   2145
   End
   Begin VB.Line Line1 
      Index           =   1
      X1              =   15000
      X2              =   4275
      Y1              =   1605
      Y2              =   1605
   End
   Begin VB.Line Line1 
      Index           =   0
      X1              =   15000
      X2              =   4275
      Y1              =   1080
      Y2              =   1080
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "GESTIONE ONDE"
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
      TabIndex        =   15
      Top             =   0
      Width           =   7965
   End
   Begin VB.Label L_spessore 
      Alignment       =   1  'Right Justify
      BackStyle       =   0  'Transparent
      Caption         =   "Spessore"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   225
      Left            =   4455
      TabIndex        =   13
      Top             =   3390
      Width           =   2595
      WordWrap        =   -1  'True
   End
   Begin VB.Label L_onda 
      Alignment       =   1  'Right Justify
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Onda"
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
      Height          =   225
      Left            =   4455
      TabIndex        =   12
      Top             =   735
      Width           =   2595
      WordWrap        =   -1  'True
   End
   Begin VB.Label L_carta 
      Alignment       =   1  'Right Justify
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Composizione Carte"
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
      Height          =   225
      Left            =   4455
      TabIndex        =   11
      Top             =   1260
      Width           =   2595
      WordWrap        =   -1  'True
   End
   Begin VB.Label L_grammatura 
      Alignment       =   1  'Right Justify
      BackStyle       =   0  'Transparent
      Caption         =   "Grammatura"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   225
      Left            =   4455
      TabIndex        =   10
      Top             =   1800
      Width           =   2595
      WordWrap        =   -1  'True
   End
   Begin VB.Label L_veldiprod 
      Alignment       =   1  'Right Justify
      BackStyle       =   0  'Transparent
      Caption         =   "Velocita' di produzione"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   225
      Left            =   4455
      TabIndex        =   9
      Top             =   2325
      Width           =   2595
      WordWrap        =   -1  'True
   End
   Begin VB.Label L_vrldicambi 
      Alignment       =   1  'Right Justify
      BackStyle       =   0  'Transparent
      Caption         =   "Velocita' di cambio"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   225
      Left            =   4455
      TabIndex        =   8
      Top             =   2850
      Width           =   2595
      WordWrap        =   -1  'True
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "Onde.frx":09E3
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "Onde"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim mod_ond As Boolean 'flag di sincronizzazione per le operazioni sul dbase
Dim flg_rev As Boolean 'True se sto tornando al controllo precedente
Dim ult_con As Long    'memorizzo l'handle dell'ultimo controllo per poterci tornare in caso di errore
Dim flg_pri_pas As Integer 'flag di memorizzazione per riempire i vari campi con i dati della prima onda di quel tipo memorizzata
Dim nuo_ond As Boolean  'true se sto inserendo una nuova onda

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Private WithEvents eve_tag As ClasseStato
Attribute eve_tag.VB_VarHelpID = -1



Sub enable_tab_onde()
Dim c As Control
For Each c In Me
  If TypeOf c Is TextBox And Right$(c.name, 3) <> "inc" Then
    c.TabStop = True
  End If
Next c
End Sub

Private Sub CARTA_Change()
CARTA.ForeColor = vbBlack
End Sub

Private Sub CARTA_GotFocus()

On Error GoTo PMAC_Gest_ERR
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
  val_ins_ond CARTA.hwnd

Exit Sub
PMAC_Gest_ERR:
log_str "on error intervenuta in carta_gotfocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next
End Sub

Private Sub CARTA_KeyPress(KeyAscii As Integer)
KeyAscii = Asc((UCase$(Chr(KeyAscii))))
End Sub

Private Sub CODONDA_Change()
CODONDA.ForeColor = vbBlack
End Sub

Private Sub CODONDA_GotFocus()
  On Error GoTo PMAC_Gest_ERR
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
  val_ins_ond CODONDA.hwnd

Exit Sub
PMAC_Gest_ERR:
log_str "on error intervenuta in codonda_gotfocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next

End Sub


Private Sub CODONDA_KeyPress(KeyAscii As Integer)
If KeyAscii = vbKeyReturn Then
   KeyAscii = 0
Else
  KeyAscii = get_num(KeyAscii)
End If

End Sub

Function val_ond() As Boolean
'restituisce false se manca l'inserimento di almeno 1 valore fondamentale
val_ond = True
 
 If Trim$(Onde.ONDA) = "" Or Trim$(Onde.CARTA) = "" Then

    val_ond = False
  End If
End Function

Sub val_ins_ond(hwnd As Long)
'determina se i valori inseriti nell'ultimo controllo utilizzato (ult_con) sono validi
Static ond As String
Static car As String
Dim ss1 As Recordset
Dim ss2 As Recordset

If ult_con = 0 Then ult_con = hwnd
If ult_con = hwnd Then Exit Sub      'eseguo i calcoli solo se ho cambiato controllo

'eseguo i controlli solo quando sto andando avanti e non quando torno indietro
If flg_rev = True And ult_con <> ONDA.hwnd Then
  flg_rev = False
  ult_con = hwnd
  Exit Sub
End If

'sezione di validazione
Select Case ult_con
   'Codice dell'ordine
  Case ONDA.hwnd
    If Len(ONDA.Text) = 0 Then   'max 3 caratteri
      messaggio msg("ins_onda"), is_All:=True
      ONDA.ForeColor = vbRed
      ONDA.SetFocus
      Exit Sub
    Else
      'ricerca la prima onda uguale se sto inserendo una nuova onda, inserisco automaticamente i campi obbligatori
      If nuo_ond Then
        ond = ONDA.Text
        Set ss2 = db.OpenRecordset("select * from ONDE where onda='" + ond + "'", dbOpenSnapshot)
        If ss2.RecordCount > 0 Then
          
         
          GRAMMATURA.Text = Format$(ss2!GRAMMATURA)
          VELDIPROD.Text = Format$(ss2!VELDIPROD)
          VELDICAMBI.Text = Format$(ss2!VELDICAMBI)
          SPESSORE.Text = Format$(ss2!SPESSORE)
          CODONDA.Text = Format$(ss2!CODONDA)
        End If
        ss2.Close
      End If
    End If
    If flg_rev Then 'impedisco di andare indietro dal primo controllo
      ONDA.SetFocus
      flg_rev = False
      Exit Sub
    End If
  'controllo esistenza del tipo di carta
  Case CARTA.hwnd
    car = CARTA.Text
    If Len(car) = 0 Then
      messaggio msg("ins_carta"), is_All:=True
      CARTA.SetFocus
      CARTA.ForeColor = vbRed
      Exit Sub
    End If
    'se sto inserendo una nuova onda controllo che il codice per la composizione delle carte sia nuovo
    If nuo_ond Then
      Set ss1 = db.OpenRecordset("select * from ONDE where onda='" + ond + "' and carta='" + car + "' ", dbOpenSnapshot)
      If ss1.RecordCount > 0 Then
        messaggio msg("car_esi"), is_All:=True
        CARTA.SetFocus
        CARTA.ForeColor = vbRed
        Exit Sub
      End If
      ss1.Close
    End If
'    'controllo la velocità di produzione
  Case VELDIPROD.hwnd
    If val(VELDIPROD.Text) < 10 Or val(VELDIPROD.Text) > 400 Then
      messaggio msg("val_min") + " 10 " + msg("val_max") + " " + "400", is_All:=True
      VELDIPROD.SetFocus
      VELDIPROD.ForeColor = vbRed
      Exit Sub
    End If
  'controllo velocità di cambio
  Case VELDICAMBI.hwnd
    If val(VELDICAMBI.Text) < 10 Or val(VELDICAMBI.Text) > 400 Then
      messaggio msg("val_min") + " 10 " + msg("val_max") + " " + "400", is_All:=True
      VELDICAMBI.SetFocus
      VELDICAMBI.ForeColor = vbRed
      Exit Sub
    End If
   'controllo sulla grammatura
  Case GRAMMATURA.hwnd
    If val(GRAMMATURA.Text) <= 0 Then
      messaggio msg("val_min") + " 1", is_All:=True
      GRAMMATURA.ForeColor = vbRed
      GRAMMATURA.SetFocus
      Exit Sub
    End If
   ' controllo sullo spessore dell'onda
  Case SPESSORE.hwnd
    If val(SPESSORE.Text) <= 0 Or val(SPESSORE.Text) > 500 Then
      messaggio msg("val_min") + " 1 " + msg("val_max") + " 500", is_All:=True
      SPESSORE.ForeColor = vbRed
      SPESSORE.SetFocus
      Exit Sub
    End If
  Case CODONDA.hwnd
    If val(CODONDA.Text) < 0 Or val(CODONDA.Text) > 99 Then
      messaggio msg("val_min") + "  0" + msg("val_max") + " 99", is_All:=True
      CODONDA.ForeColor = vbRed
      CODONDA.SetFocus
      Exit Sub
    End If
End Select

'se arrivo qui, significa che le validazioni hanno avuto successo, quindi aggiorno
'il puntatore all'ultimo controllo attivo
ult_con = hwnd

End Sub




Private Function dbval(v As Variant) As Double
   If IsNull(v) Then
     dbval = 0
   Else
     dbval = val(v)
   End If
End Function
Sub ass_ond()
' riassegna i valori null delle vaschette

If GRAMMATURA = "" Then GRAMMATURA = "0"
If VELDIPROD = "" Then VELDIPROD = "0"
If VELDICAMBI = "" Then VELDICAMBI = "0"
If SPESSORE = "" Then SPESSORE = "0"
If CODONDA = "" Then CODONDA = "0"

End Sub

Sub abort()
  'annulla le modifiche in corso
  If mod_ond Then
    Onde.Data1.Recordset.CancelUpdate
    Onde.lis.ReBind
    mod_ond = False
    nuo_ond = False
    'riassegna agli ordini i valori dell'onda modificati
    disable_tab Me
    lis.TabStop = True
    lis.SetFocus
  End If
End Sub


Private Sub eve_key_esc(Shift As Integer)
  Select Case Shift
    Case 0
      abort 'annulla modifiche
    Case Else
  End Select
End Sub


Private Sub eve_key_F1(Shift As Integer)
  Select Case Shift
    Case 0
      'inserimento di un nuovo tipo di onda
        If Not mod_ond Then
          Onde.Data1.Recordset.AddNew
          enable_tab_onde
          Onde.ONDA.SetFocus
          mod_ond = True
          ult_con = 0
          nuo_ond = True
          lis.TabStop = False
        End If
      
    Case Else
  End Select
End Sub

Private Sub eve_key_F10(Shift As Integer)
  Select Case Shift
    Case 0
      ' torna alla pagina di produzione
      If Not mod_ond Then
          eve_key.chg pag_pro
      End If
      
    Case Else
  End Select
  
End Sub


Private Sub eve_key_F2(Shift As Integer)
Dim flg_ond_pre As Boolean
Dim i As Integer
Dim tmp As ClasseOrdine
Dim ord As Tip_Ordine

  Select Case Shift
    Case 0
     'modifica record esistente

      If Not mod_ond Then
        Onde.Data1.Recordset.Edit
        'Se l'onda che si vuol modificare e' in uso il focus e' diverso
        flg_ond_pre = False

        For i = 1 To ord_mac.Count
          Set tmp = ord_mac.item(indice:=i)
          CopyType ord, ord_mac.item(indice:=i).ordine, Len(ord_mac.item(indice:=i).ordine)
          If Trim$(ord.tip_car.Cod_ond) = Trim$(Onde.ONDA) And Trim$(ord.tip_car.cod_com_car) = Trim$(Onde.CARTA) Then
            flg_ond_pre = True
            Exit For
          End If
        Next i
        enable_tab_onde
        If flg_ond_pre Then
          Onde.GRAMMATURA.SetFocus
          Onde.ONDA.TabStop = False
          Onde.CARTA.TabStop = False
        Else
          Onde.ONDA.SetFocus
          Onde.ONDA.TabStop = True
          Onde.CARTA.TabStop = True
        End If
        mod_ond = True
        ult_con = 0
        nuo_ond = False
        lis.TabStop = False
      End If
     
    Case Else
  End Select
End Sub

Private Sub eve_key_F3(Shift As Integer)
Dim tmp As ClasseOrdine
Dim i As Integer
Dim flg_ond_pre As Boolean
Dim ord As Tip_Ordine

  Select Case Shift
    Case 0
       'cancellazione record


      If Not mod_ond Then
        'controllo che non ci sia alcun ordine in lista con l'onda da cancellare
        flg_ond_pre = False

        For i = 1 To ord_mac.Count
          Set tmp = ord_mac.item(indice:=i)
          CopyType ord, ord_mac.item(indice:=i).ordine, Len(ord_mac.item(indice:=i).ordine)
          If Trim$(ord.tip_car.Cod_ond) = Onde.ONDA And Trim$(ord.tip_car.cod_com_car) = Onde.CARTA Then
            flg_ond_pre = True
            Exit For
          End If
        Next i
        If Not flg_ond_pre Then
          If dbval(Onde.Data1.Recordset!RISERVATO) = 0 Then
            Onde.Data1.Recordset.Delete
            Onde.Data1.Recordset.MovePrevious
            If Onde.Data1.Recordset.BOF = True Then
              Onde.Data1.Recordset.MoveNext
            End If
          End If
          nuo_ond = False
        Else
           messaggio msg("imp_can_ond"), is_All:=True, stay:=False
        End If
      End If
     
    Case Else
  End Select
End Sub


Private Sub eve_key_F4(Shift As Integer)
  Select Case Shift
    Case 0
      'salvataggio onda
      Dim new_ond As String
      Dim new_car As String
      Dim i As Integer
        nuo_ond = False
        If mod_ond Then
          If val_ond Then
          
            ass_ond   ' per togliere valori null
            Onde.Data1.Recordset.Update
            Onde.Data1.Recordset.Bookmark = Onde.Data1.Recordset.LastModified
            new_ond = Format$(Onde.Data1.Recordset!ONDA)
            new_car = Format$(Onde.Data1.Recordset!CARTA)
            Onde.Data1.Refresh
            Onde.Data1.Recordset.FindFirst "onda='" + new_ond + "' and carta='" + new_car + "'"
            Onde.lis.ReBind
            mod_ond = False
            messaggio msg("ope_eff"), is_All:=False
            disable_tab Me
            lis.TabStop = True
            lis.SetFocus
          Else
            messaggio msg("man_cam_ond"), is_All:=True
            Select Case ult_con
              Case ONDA.hwnd
                ONDA.SetFocus
              Case CARTA.hwnd
                CARTA.SetFocus
            End Select
            Exit Sub
          End If
        End If
      
    Case Else
  End Select
End Sub


Private Sub eve_key_F8(Shift As Integer)
  Select Case Shift
    Case 0
      ' vado alla pagina ordini eseguiti
      If Not mod_ond Then
          eve_key.chg pag_lis_ese
      End If
      
    Case Else
  End Select
End Sub

Private Sub eve_key_F9(Shift As Integer)
Select Case Shift
    Case 0
      ' vado alla pagina lista ordini
      If Not mod_ond Then
          eve_key.chg pag_lis_ord
      End If
      
    Case Else
  End Select
End Sub


Private Sub Form_Activate()
Data1.Refresh
lis.Caption = msg("db_grid_onde")
lis.Columns(0).Caption = msg("db_grid_onda")
lis.Columns(1).Caption = msg("db_grid_carta")
VELDIPROD_inc.Visible = abi_mis_ang
VELDICAMBI_inc.Visible = abi_mis_ang
SPESSORE_inc.Visible = abi_mis_ang
lis.SetFocus
End Sub

Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
On Error GoTo STAG_Gest_ERR
  
  If ActiveControl Is Nothing Then Exit Sub
  Select Case KeyCode
    Case vbKeyReturn, vbKeyDown
      chk_shift 'controllo dello stato del tasto SHIFT
      KeyCode = 0
      SendKeys "{TAB}"
    Case vbKeyUp
      chk_shift 'controllo dello stato del tasto SHIFT
      KeyCode = 0
      SendKeys "+{TAB}"
  End Select
  
  Exit Sub

STAG_Gest_ERR:
log_str "on error intervenuta in Form_KeyDown della form " + Me.name + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next
End Sub


Private Sub Form_Load()

On Error GoTo STAG_Gest_ERR
  Debug.Print "Inizio Load " & Me.name
  Carica Me
    
  Data1.DatabaseName = Left$(pro_set.pro_pat, Len(pro_set.pro_pat) - 1)
  Set eve_key = New key
  eve_key.create Me
  Set eve_tag = sta_tag
  Debug.Print "Fine Load " & Me.name
  Debug.Print ""
Exit Sub
STAG_Gest_ERR:

Resume Next
End Sub


Private Sub GRAMMATURA_Change()
GRAMMATURA.ForeColor = vbBlack
End Sub


Private Sub GRAMMATURA_GotFocus()
  On Error GoTo PMAC_Gest_ERR
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
  val_ins_ond GRAMMATURA.hwnd

Exit Sub
PMAC_Gest_ERR:
log_str "on error intervenuta in grammatura_gotfocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next

End Sub


Private Sub GRAMMATURA_KeyPress(KeyAscii As Integer)
If KeyAscii = vbKeyReturn Then
   KeyAscii = 0
Else
  KeyAscii = get_num(KeyAscii)
End If
End Sub


Private Sub ONDA_Change()
ONDA.ForeColor = vbBlack
End Sub


Private Sub ONDA_GotFocus()
Dim c As Control

On Error GoTo PMAC_Gest_ERR

If ONDA.TabStop = False Then
  For Each c In Controls
    If TypeName(c) = "TextBox" Then
      c.TabStop = True
    End If
  Next c
End If
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
val_ins_ond ONDA.hwnd

Exit Sub
PMAC_Gest_ERR:
log_str "on error intervenuta in onda_gotfocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next
End Sub


Private Sub ONDA_KeyPress(KeyAscii As Integer)
KeyAscii = Asc((UCase$(Chr(KeyAscii))))
End Sub


Private Sub SPESSORE_Change()
Dim inc As String

SPESSORE.ForeColor = vbBlack
inc = con_dmm_to_inc(SPESSORE.Text)
If val(inc) <> 0 Then
  SPESSORE_inc.Text = inc
End If

End Sub


Private Sub SPESSORE_GotFocus()
  On Error GoTo PMAC_Gest_ERR
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
  val_ins_ond SPESSORE.hwnd

Exit Sub
PMAC_Gest_ERR:
log_str "on error intervenuta in spessore_gotfocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next

End Sub


Private Sub SPESSORE_KeyPress(KeyAscii As Integer)
If KeyAscii = vbKeyReturn Then
   KeyAscii = 0
Else
  KeyAscii = get_num(KeyAscii)
End If
End Sub


Private Sub VELDICAMBI_Change()
VELDICAMBI.ForeColor = vbBlack
VELDICAMBI_inc = con_met_to_fet(VELDICAMBI.Text)
End Sub


Private Sub VELDICAMBI_GotFocus()
    On Error GoTo PMAC_Gest_ERR
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
  val_ins_ond VELDICAMBI.hwnd

Exit Sub
PMAC_Gest_ERR:
log_str "on error intervenuta in veldicambi_gotfocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next

End Sub


Private Sub VELDICAMBI_KeyPress(KeyAscii As Integer)
If KeyAscii = vbKeyReturn Then
   KeyAscii = 0
Else
  KeyAscii = get_num(KeyAscii)
End If
End Sub


Private Sub VELDIPROD_Change()
VELDIPROD.ForeColor = vbBlack
VELDIPROD_inc = con_met_to_fet(VELDIPROD.Text)
End Sub


Private Sub VELDIPROD_GotFocus()
  On Error GoTo PMAC_Gest_ERR
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
  val_ins_ond VELDIPROD.hwnd

Exit Sub
PMAC_Gest_ERR:
log_str "on error intervenuta in veldiprod_gotfocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next

End Sub


Private Sub VELDIPROD_KeyPress(KeyAscii As Integer)
If KeyAscii = vbKeyReturn Then
   KeyAscii = 0
Else
  KeyAscii = get_num(KeyAscii)
End If
End Sub



