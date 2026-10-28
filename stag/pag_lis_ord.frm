VERSION 5.00
Object = "{5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0"; "msflxgrd.ocx"
Begin VB.Form pag_lis_ord 
   Appearance      =   0  'Flat
   BackColor       =   &H80000005&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6390
   ClientLeft      =   -240
   ClientTop       =   675
   ClientWidth     =   12045
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
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   6390
   ScaleWidth      =   12045
   ShowInTaskbar   =   0   'False
   Begin VB.Frame Frm_grid 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BorderStyle     =   0  'None
      Caption         =   "Frame1"
      ForeColor       =   &H80000008&
      Height          =   6705
      Left            =   0
      TabIndex        =   0
      Top             =   330
      Width           =   12100
      Begin VB.Frame frm_det 
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         Caption         =   "Frame1"
         Height          =   2070
         Left            =   -30
         TabIndex        =   4
         Top             =   4830
         Visible         =   0   'False
         Width           =   12150
         Begin VB.TextBox txt_lun_tot 
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   0  'None
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Left            =   4695
            TabIndex        =   14
            Top             =   540
            Width           =   2130
         End
         Begin VB.TextBox txt_cut_to_mark 
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   0  'None
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   2
            Left            =   7905
            TabIndex        =   13
            Top             =   795
            Width           =   1350
         End
         Begin VB.TextBox txt_cut_to_mark 
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   0  'None
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   1
            Left            =   6300
            TabIndex        =   12
            Top             =   795
            Width           =   1350
         End
         Begin VB.TextBox txt_cut_to_mark 
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   0  'None
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   0
            Left            =   4695
            TabIndex        =   11
            Top             =   795
            Width           =   1350
         End
         Begin VB.TextBox txt_alt_bob 
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   0  'None
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Left            =   4695
            TabIndex        =   10
            Top             =   285
            Width           =   2130
         End
         Begin VB.TextBox txt_cod_com_car 
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   0  'None
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Left            =   4695
            TabIndex        =   9
            Top             =   30
            Width           =   5925
         End
         Begin VB.Label Label2 
            BackStyle       =   0  'Transparent
            Caption         =   "Lunghezza totale:"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00808000&
            Height          =   285
            Left            =   1710
            TabIndex        =   15
            Top             =   525
            Width           =   1995
         End
         Begin VB.Line Line1 
            BorderWidth     =   2
            X1              =   -135
            X2              =   14910
            Y1              =   0
            Y2              =   0
         End
         Begin VB.Image Image1 
            Height          =   360
            Left            =   90
            Picture         =   "pag_lis_ord.frx":0000
            Stretch         =   -1  'True
            Top             =   45
            Width           =   390
         End
         Begin VB.Label lab_cut_to_mark 
            BackStyle       =   0  'Transparent
            Caption         =   "Cut-to-mark:"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00808000&
            Height          =   285
            Left            =   1710
            TabIndex        =   8
            Top             =   780
            Width           =   1995
         End
         Begin VB.Label Label1 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Dettagli"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00FFFFFF&
            Height          =   285
            Left            =   570
            TabIndex        =   7
            Top             =   75
            Width           =   915
         End
         Begin VB.Shape Shape1 
            BackColor       =   &H00808000&
            BackStyle       =   1  'Opaque
            BorderColor     =   &H00808000&
            BorderStyle     =   0  'Transparent
            FillColor       =   &H00808000&
            Height          =   375
            Left            =   465
            Top             =   45
            Width           =   1035
         End
         Begin VB.Label lab_cod_com_car 
            BackStyle       =   0  'Transparent
            Caption         =   "Codice composizione carta:"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00808000&
            Height          =   285
            Left            =   1710
            TabIndex        =   6
            Top             =   15
            Width           =   2985
         End
         Begin VB.Label lab_alt_bob 
            BackStyle       =   0  'Transparent
            Caption         =   "Altezza bobina:"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00808000&
            Height          =   285
            Left            =   1710
            TabIndex        =   5
            Top             =   270
            Width           =   1995
         End
      End
      Begin VB.TextBox txt_idle 
         Height          =   345
         Left            =   7290
         TabIndex        =   3
         Top             =   -1000
         Width           =   1245
      End
      Begin MSFlexGridLib.MSFlexGrid lis_ord 
         Height          =   4875
         Left            =   0
         TabIndex        =   1
         Top             =   0
         Width           =   12050
         _ExtentX        =   21246
         _ExtentY        =   8599
         _Version        =   393216
         Rows            =   1
         Cols            =   11
         FixedRows       =   0
         FixedCols       =   0
         BackColor       =   16777215
         BackColorFixed  =   16777215
         ForeColorSel    =   0
         BackColorBkg    =   16777215
         GridColor       =   0
         FocusRect       =   2
         ScrollBars      =   0
         MergeCells      =   2
         BorderStyle     =   0
         Appearance      =   0
      End
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "LISTA ORDINI"
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
      Width           =   9105
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "pag_lis_ord.frx":07C2
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "pag_lis_ord"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim pun_lis As Integer
Dim primavolta As Boolean
Dim ridisegna As Boolean
Dim primo As Integer
Dim ultimo As Integer
Dim num_lis_ord As Integer
Dim puntatore As Integer
Dim keyup As Boolean
Dim keydown As Boolean
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Private WithEvents eve_ord_mac As CollezioneOrdini
Attribute eve_ord_mac.VB_VarHelpID = -1
Private Sub aggiorna()

Dim i As Integer, j As Integer
Dim prog As String, Cod_ond As String, Com_car As String, alt_bob As String, uni As String, pun_pre As String, pun_suc As String, cod_abb As String
Dim scatole As String, sca_lar As String, sca_lun As String, cod_com As String, resa As String, nom_cli As String, cod_cli As String
Dim liv_ord As String
Dim a As Integer
Dim ord As Tip_Ordine
Static old_ord_mac As Integer
Static old_primo As Integer
Static old_ultimo As Integer
Dim num_ord_mac As Integer
'Icilio3545 11-01-2004
Dim pun, ind As Integer

If keyup Then a = 1
If keydown Then a = -1

lis_ord.Redraw = False

num_ord_mac = ord_mac.Count

If num_ord_mac <> old_ord_mac And primo = old_primo And ultimo = old_ultimo And Not sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1) Then  'è arrivato un comando di invio/cancellazione di un ordine da SPDE
  If num_ord_mac > old_ord_mac Then 'è arrivato un ordine da SPDE
    primo = IIf(num_ord_mac > num_lis_ord, num_ord_mac - num_lis_ord, 1)
    ultimo = num_ord_mac
    puntatore = num_ord_mac - primo + 1
    pun_lis = primo + puntatore - 1
  Else  'è arrivato un comando di cancellazione di un ordine
    If num_ord_mac = 0 Then
      primo = 0: ultimo = 0: puntatore = 0: pun_lis = 0
    Else
      primo = IIf(num_ord_mac < primo, primo - num_lis_ord, primo)
      ultimo = num_ord_mac
      puntatore = num_ord_mac - primo + 1
      pun_lis = primo + puntatore - 1
    End If
  End If
Else
  ultimo = IIf(ultimo > num_ord_mac, num_ord_mac, ultimo)
  primo = IIf(primo > num_ord_mac, num_ord_mac, primo)
  If ultimo > old_ultimo Then
    pun_lis = ultimo
  ElseIf primo < old_primo Then
    pun_lis = primo
  End If
  puntatore = IIf(puntatore > num_ord_mac, num_ord_mac, puntatore)
  pun_lis = IIf(pun_lis > num_ord_mac, num_ord_mac, pun_lis)
End If
old_ord_mac = num_ord_mac
old_primo = primo
old_ultimo = ultimo

If ridisegna Then

    If Not primavolta Then
          i = lis_ord.Rows
          For j = 1 To i - 1
              lis_ord.RemoveItem (1)
          Next j
    End If

    For j = primo To ultimo
      If j > 0 Then
        prog = j
        CopyType ord, ord_mac.item(indice:=j).ordine, Len(ord_mac.item(indice:=j).ordine)
        'uni = ord_mac.item(indice:=j).stag_id
        cod_abb = Rie_nul(ord.cod_ord_abb)
        alt_bob = Rie_nul(IIf(abi_mis_ang, con_dmm_to_inc(Trim$(ord.alt_bob)), CStr(val(ord.alt_bob) \ TODECMM)))
        Cod_ond = Rie_nul(ord.tip_car.Cod_ond)
        Com_car = Rie_nul(ord.tip_car.cod_com_car)
        For i = 1 To num_liv_lis_ord
          If Trim$(ord.ord(i).Ord_liv) <> "" Then
            scatole = Trim$(ord.ord(i).sca_tot_num)
            cod_com = Rie_nul(ord.ord(i).com.cod_com_ord)
            resa = Rie_nul(ord.ord(i).Sca_per_tag)
            nom_cli = Rie_nul(ord.ord(i).nom_cli)
            sca_lun = Rie_nul(IIf(abi_mis_ang, con_dmm_to_inc(Trim$(ord.ord(i).sca_lun)), CStr(val(ord.ord(i).sca_lun) \ TODECMM)))
            sca_lar = Rie_nul(IIf(abi_mis_ang, con_dmm_to_inc(Trim$(ord.ord(i).sca_lar)), CStr(val(ord.ord(i).sca_lar) \ TODECMM)))
            cod_cli = Rie_nul(ord.ord(i).com.cod_cli)
            Select Case val(ord.ord(i).Ord_liv)
              Case LIV_BAS: liv_ord = Format$(msg("liv_bas"))
              Case LIV_MED: liv_ord = Format$(msg("liv_med"))
              Case LIV_ALT: liv_ord = Format$(msg("liv_alt"))
              Case Else: liv_ord = ""
            End Select
          Else
            liv_ord = ""
            scatole = ""
            cod_com = ""
            resa = ""
            nom_cli = ""
            sca_lun = ""
            sca_lar = ""
            cod_cli = ""
          End If
          lis_ord.AddItem (prog & vbTab & cod_abb & vbTab & Cod_ond & vbTab & liv_ord & vbTab & cod_com & vbTab & _
           nom_cli & vbTab & cod_cli & vbTab & sca_lar & vbTab & sca_lun & vbTab _
           & resa & vbTab & scatole) '& vbTab & IIf(i Mod (val(par_mac.car_tec.num_liv_tag) + 1) = 1, uni, ""))
        Next i
      End If
    Next j
End If

If ridisegna Then
  pun = primo - 1
  For i = 1 To lis_ord.Rows - 1
    lis_ord.Row = i
    lis_ord.RowHeight(i) = Int(lis_ord.RowHeight(i) * pro_set.PercX / 100)
    'Icilio3545 11-01-2004
    ind = IIf((i Mod (num_liv_lis_ord) = 0), num_liv_lis_ord, (i Mod (num_liv_lis_ord)))
    pun = IIf(ind = 1, pun + 1, pun)
    For j = 0 To lis_ord.Cols - 1
        lis_ord.col = j
        If j = 0 Or j = lis_ord.Cols - 1 Then
          lis_ord.CellAlignment = flexAlignRightCenter
        ElseIf (j > 0 And j < 3) Or (j >= 7 And j <= 9) Then
          lis_ord.CellAlignment = flexAlignCenterCenter
        Else
          lis_ord.CellAlignment = flexAlignLeftCenter
        End If
        'Icilio3545 11-01-2004 Cambio colore a seconda dell'ordine cut to pattern/mark
        lis_ord.CellForeColor = vbBlack                       ' caratteri in nero
        If (ord_mac.item(pun).cut_to_mark(ind) > -1) And (ord_mac.item(pun).cut_to_mark(ind) < 3) Then
          lis_ord.CellForeColor = Choose(ord_mac.item(pun).cut_to_mark(ind) + 1, vbBlack, COL_DARK_Green, COL_FADED_ORANGE)
        End If
        If (i Mod num_liv_lis_ord * 2) >= 1 And (i Mod num_liv_lis_ord * 2) <= num_liv_lis_ord Then
            lis_ord.CellBackColor = COL_GRAY_ORD           ' sfondo grigio chiaro
        Else
            lis_ord.CellBackColor = vbWhite           ' sfondo bianco
        End If
        If j < 2 Or j = 4 Or j = 8 Then lis_ord.CellFontBold = True                       ' grassetto
        If j < 3 And num_liv_lis_ord > 1 Then
          lis_ord.CellFontSize = 10 * pro_set.PercX / 100                         ' dimensione carattere
        Else
          lis_ord.CellFontSize = Int(lis_ord.CellFontSize * pro_set.PercX / 100)        ' dimensione carattere
        End If
        If i <= puntatore * num_liv_lis_ord And i >= puntatore * num_liv_lis_ord - (num_liv_lis_ord - 1) Then
              lis_ord.CellBackColor = COL_DARK_Green        ' sfondo verde
              lis_ord.CellForeColor = vbWhite             ' caratteri in bianco
        End If
    Next j
  Next i

Else
      
  For i = puntatore * num_liv_lis_ord To puntatore * num_liv_lis_ord - (num_liv_lis_ord - 1) Step -1
    If i > 0 And i < lis_ord.Rows Then
      lis_ord.Row = i
      For j = 0 To lis_ord.Cols - 1
          lis_ord.col = j
          lis_ord.CellBackColor = COL_DARK_Green    ' sfondo verde
          'Icilio3545 11-01-2004
          lis_ord.CellForeColor = vbWhite         ' caratteri in bianco
          pun = pun_lis
      Next j
    End If
  Next i
  For i = (puntatore + a) * num_liv_lis_ord To (puntatore + a) * num_liv_lis_ord - (num_liv_lis_ord - 1) Step -1
    If i > 0 And i < lis_ord.Rows Then
      lis_ord.Row = i
      For j = 0 To lis_ord.Cols - 1
          lis_ord.col = j
          lis_ord.CellBackColor = IIf((puntatore + a) * num_liv_lis_ord Mod (num_liv_lis_ord * 2) >= 1 And (puntatore + a) * num_liv_lis_ord _
          Mod (num_liv_lis_ord * 2) <= num_liv_lis_ord, COL_GRAY_ORD, vbWhite) ' sfondo grigio chiaro o bianco
          'Icilio3545 11-01-2004
          pun = pun_lis + a
          lis_ord.CellForeColor = Choose(ord_mac.item(pun).cut_to_mark() + 1, vbBlack, COL_DARK_Green, COL_FADED_ORANGE)
      Next j
    End If
  Next i
  
End If

primavolta = False
lis_ord.Redraw = True

'gestione dettagli
If pun_lis > 0 And num_ord_mac > 0 And frm_det.Visible Then
    ges_det (pun_lis)
End If
  
End Sub




Sub ges_det(pun_ord As Integer)
Dim i As Integer
Dim ord As Tip_Ordine

CopyType ord, ord_mac.item(indice:=pun_ord).ordine, Len(ord_mac.item(indice:=pun_ord).ordine)
txt_cod_com_car.Text = Trim$(Rie_nul(ord.tip_car.cod_com_car))
txt_alt_bob.Text = Trim$(val(IIf(abi_mis_ang, con_dmm_to_inc(Trim$(Rie_nul(ord.alt_bob))), CStr(val(ord.alt_bob) \ TODECMM))))
txt_lun_tot = IIf(abi_mis_ang, con_met_to_fet(Trim$(Rie_nul(ord.met_tot_ord))), ord.met_tot_ord)
For i = 1 To 3
  If Trim$(ord.ord(i).Ord_liv) <> "" Then
    txt_cut_to_mark(i - 1).Text = IIf(val(ord.ord(i).cut_to_mark) = 1, msg("msg_si"), msg("msg_no"))
  Else
    txt_cut_to_mark(i - 1).Text = ""
  End If
Next i

End Sub


Sub tra_gri()
lis_ord.Row = 0
lis_ord.col = 0
lis_ord.Text = ""
lis_ord.col = 1
lis_ord.Text = msg("lis_cod_abb")
lis_ord.col = 2
lis_ord.Text = msg("lis_ond")
lis_ord.col = 3
lis_ord.Text = msg("lis_liv")
lis_ord.col = 4
lis_ord.Text = msg("lis_com")
lis_ord.col = 5
lis_ord.Text = msg("lis_nom_cli")
lis_ord.col = 6
lis_ord.Text = msg("lis_cod_cli")
lis_ord.col = 7
lis_ord.Text = msg("lis_lar")
lis_ord.col = 8
lis_ord.Text = msg("lis_lun")
lis_ord.col = 9
lis_ord.Text = msg("lis_res")
lis_ord.col = 10
lis_ord.Text = msg("lis_sca")
'lis_ord.col = 11
'lis_ord.Text = "Uni"
End Sub

Private Sub eve_key_F1(Shift As Integer)
Select Case Shift
  Case 0
  'dettaglio
  ord_mac.scr_pun_ord (pun_lis)
  eve_key.chg Ins_ord
  Case Else
End Select
End Sub

Private Sub eve_key_F10(Shift As Integer)
Select Case Shift
  Case 0
  ord_mac.scr_pun_ord (pun_lis)
  eve_key.chg pag_pro
  Case Else
End Select
End Sub


Private Sub eve_key_F3(Shift As Integer)
Select Case Shift
  Case 0
  'cancello uno o più ordini
  If ord_mac.Count > 0 Then
        Del_ord.txt_from = CStr(pun_lis)
        Del_ord.txt_to = CStr(pun_lis)
        eve_key.chg Del_ord, keep:=1
  End If
  Case Else
End Select
End Sub


Private Sub eve_key_F4(Shift As Integer)
Select Case Shift
  Case 0
  'sposto uno o più ordini
  If ord_mac.Count > 0 Then
      Mov_ord.txt_from = CStr(pun_lis)
      Mov_ord.txt_to = CStr(pun_lis)
      Mov_ord.txt_dst = ""
      eve_key.chg Mov_ord, keep:=1
  End If
  Case Else
End Select
End Sub


Private Sub eve_key_F5(Shift As Integer)
Select Case Shift
  Case 0
  'copio uno o più ordini
  If ord_mac.Count > 0 Then
      cop_ord.txt_from = CStr(pun_lis)
      cop_ord.txt_to = CStr(pun_lis)
      cop_ord.txt_dst = ""
      eve_key.chg cop_ord, keep:=1
  End If
  Case Else
End Select
End Sub

Private Sub eve_key_F6(Shift As Integer)
Select Case Shift
  Case 0
  'trova ordine
  If ord_mac.Count > 0 Then
      tro_ord.txt_cod = ""
      eve_key.chg tro_ord, keep:=1
  End If
  Case Else
End Select
End Sub


Private Sub eve_key_F7(Shift As Integer)
Select Case Shift
  Case 0
    'Dettagli ON/OFF
    If ord_mac.Count > 0 Then
      frm_det.Visible = Not frm_det.Visible
      num_rig_ord = IIf(frm_det.Visible, 18, 22)
      num_lis_ord = num_rig_ord / num_liv_lis_ord
      lis_ord.Height = lis_ord.RowHeight(0) * (num_rig_ord + 1)
      pun_lis = ord_mac.leg_pun_ord
      If pun_lis + (num_lis_ord - 1) <= ord_mac.Count Then
        primo = pun_lis
        ultimo = primo + (num_lis_ord - 1)
        puntatore = 1
      Else
        primo = IIf(pun_lis - (num_lis_ord - 1) >= 1, pun_lis - (num_lis_ord - 1), 1)
        ultimo = IIf(primo + (num_lis_ord - 1) <= ord_mac.Count, primo + (num_lis_ord - 1), ord_mac.Count)
        puntatore = IIf(primo > 1, num_lis_ord, pun_lis)
      End If
      ridisegna = True
      Call aggiorna
    End If
  Case Else
End Select
End Sub

Private Sub eve_key_F8(Shift As Integer)
Select Case Shift
  Case 0
    ord_mac.scr_pun_ord (pun_lis)
    eve_key.chg pag_lis_ese
  Case Else
End Select
End Sub

Private Sub eve_ord_mac_Refresh(Shift As Boolean)
ridisegna = True
aggiorna
End Sub

Private Sub Form_Activate()
Dim i As Integer

tra_gri
txt_idle.SetFocus
If pag_pre.name <> "Ins_ord" And pag_pre.name <> "Del_ord" And pag_pre.name <> "cop_ord" _
And pag_pre.name <> "Mov_ord" And pag_pre.name <> "tro_ord" Then ord_mac.scr_pun_ord (1)

pun_lis = ord_mac.leg_pun_ord
If pun_lis + (num_lis_ord - 1) <= ord_mac.Count Then
  primo = pun_lis
  ultimo = primo + (num_lis_ord - 1)
  puntatore = 1
Else
  primo = IIf(pun_lis - (num_lis_ord - 1) >= 1, pun_lis - (num_lis_ord - 1), 1)
  ultimo = IIf(primo + (num_lis_ord - 1) <= ord_mac.Count, primo + (num_lis_ord - 1), ord_mac.Count)
  puntatore = IIf(primo > 1, num_lis_ord, pun_lis)
End If
ridisegna = True
Call aggiorna
End Sub

Private Sub Form_KeyUp(KeyCode As Integer, Shift As Integer)
Dim a As Integer
Dim pun_lis_pre As Integer

ridisegna = False
keyup = False
keydown = False


Select Case KeyCode

    Case vbKeyDown
        keydown = True
        pun_lis_pre = pun_lis
        pun_lis = IIf(ord_mac.Count > pun_lis, pun_lis + 1, ord_mac.Count)
        If puntatore < num_lis_ord And pun_lis_pre <> pun_lis Then puntatore = puntatore + 1
        If pun_lis = ultimo + 1 Then
              primo = primo + 1
              ultimo = ultimo + 1
              ridisegna = True
        End If
    
    Case vbKeyPageDown
        
        ridisegna = True
        If ord_mac.Count > pun_lis + num_lis_ord Then
              pun_lis = pun_lis + num_lis_ord
              primo = primo + num_lis_ord
              ultimo = IIf(ord_mac.Count > ultimo + num_lis_ord, ultimo + num_lis_ord, ord_mac.Count)
        Else
              If ultimo < ord_mac.Count Then
                  a = ord_mac.Count - ultimo
                  pun_lis = ord_mac.Count
                  primo = primo + num_lis_ord
                  ultimo = pun_lis
                  puntatore = a
              Else
                  a = ultimo - primo + 1
                  pun_lis = ultimo
                  puntatore = a
              End If
        End If

    Case vbKeyPageUp
        
        ridisegna = True
        If pun_lis - num_lis_ord >= 1 Then
            
            If ultimo - primo = num_lis_ord - 1 Then
                pun_lis = pun_lis - num_lis_ord
                primo = IIf(primo - num_lis_ord > 1, primo - num_lis_ord, 1)
                ultimo = primo + (num_lis_ord - 1)
            Else
                ultimo = primo - 1
                primo = ultimo - (num_lis_ord - 1)
                pun_lis = pun_lis - num_lis_ord
            End If
        
        Else
            pun_lis = 1
            puntatore = 1
            primo = 1
            ultimo = IIf(ord_mac.Count > ultimo, num_lis_ord, ord_mac.Count)
        End If
  
    Case vbKeyUp
        keyup = True
        If pun_lis > 1 Then pun_lis = pun_lis - 1
        If puntatore > 1 Then puntatore = puntatore - 1
        If pun_lis = primo - 1 Then
            If ultimo - primo = num_lis_ord - 1 Then
                  primo = primo - 1
                  ultimo = ultimo - 1
            Else
                  primo = primo - 1
            End If
            ridisegna = True
        End If
End Select

If KeyCode = vbKeyDown Or KeyCode = vbKeyUp Or KeyCode = vbKeyPageUp Or KeyCode = vbKeyPageDown Then Call aggiorna


End Sub

Private Sub Form_Load()
Dim i As Integer
Dim j As Integer
Debug.Print "Inizio Load " & Me.name
Set eve_ord_mac = ord_mac
Set eve_key = New key
eve_key.create Me
Carica Me
lis_ord.RowHeight(0) = Int(lis_ord.RowHeight(0) * (pro_set.PercX / 100))
num_rig_ord = IIf(num_liv_lis_ord = 2, 22, 21)
num_lis_ord = num_rig_ord / num_liv_lis_ord
frm_det.Visible = False
frm_det.Top = lis_ord.Top + lis_ord.Height
lis_ord.Height = lis_ord.RowHeight(0) * (num_rig_ord + 1)
pun_lis = 1
puntatore = 1
primo = 1
ultimo = IIf(ord_mac.Count > num_lis_ord, num_lis_ord, ord_mac.Count)
ridisegna = True

keyup = False
keydown = False

primavolta = True
'totale larghezza MSFlexGrid = 11960
lis_ord.ColWidth(0) = Int(370 * ((pro_set.PercX / 100))) 'progressivo
lis_ord.ColWidth(1) = Int(1680 * (pro_set.PercX / 100))  'codice abbinamento
lis_ord.ColWidth(2) = Int(570 * (pro_set.PercX / 100))  'codice onda
lis_ord.ColWidth(3) = Int(690 * (pro_set.PercX / 100))   'livello
lis_ord.ColWidth(4) = Int(1620 * (pro_set.PercX / 100))   'commessa
lis_ord.ColWidth(5) = Int(2890 * (pro_set.PercX / 100))   ' nome cliente
lis_ord.ColWidth(6) = Int(1340 * (pro_set.PercX / 100))   ' codice cliente
lis_ord.ColWidth(7) = Int(750 * (pro_set.PercX / 100))     'larghezza
lis_ord.ColWidth(8) = Int(750 * (pro_set.PercX / 100))     'lunghezza
lis_ord.ColWidth(9) = Int(510 * (pro_set.PercX / 100))    'resa
lis_ord.ColWidth(10) = Int(790 * (pro_set.PercX / 100))     'scatole
'lis_ord.ColWidth(11) = Int(650 * (pro_set.PercX / 100))  'uni

lis_ord.Row = 0
lis_ord.col = 0
For i = 0 To lis_ord.Cols - 1
  lis_ord.CellBackColor = ULTRA_DARK_GREY
  lis_ord.CellForeColor = vbWhite
  lis_ord.CellFontBold = True
  lis_ord.CellFontSize = Int(lis_ord.CellFontSize * (pro_set.PercX / 100))
  If i = 0 Or i = lis_ord.Cols - 1 Then
    lis_ord.CellAlignment = flexAlignRightCenter
  ElseIf i = 1 Or (i >= 7 And i <= 9) Then
    lis_ord.CellAlignment = flexAlignCenterCenter
  Else
    lis_ord.CellAlignment = flexAlignLeftCenter
  End If
  If lis_ord.col = lis_ord.Cols - 1 Then
    Exit For
  Else
    lis_ord.col = lis_ord.col + 1
  End If
Next i
lis_ord.col = 0
lis_ord.Text = ""
lis_ord.col = 1
lis_ord.Text = msg("lis_cod_abb")
lis_ord.col = 2
lis_ord.Text = msg("lis_ond")
lis_ord.col = 3
lis_ord.Text = msg("lis_liv")
lis_ord.col = 4
lis_ord.Text = msg("lis_com")
lis_ord.col = 5
lis_ord.Text = msg("lis_nom_cli")
lis_ord.col = 6
lis_ord.Text = msg("lis_cod_cli")
lis_ord.col = 7
lis_ord.Text = msg("lis_lar")
lis_ord.col = 8
lis_ord.Text = msg("lis_lun")
lis_ord.col = 9
lis_ord.Text = msg("lis_res")
lis_ord.col = 10
lis_ord.Text = msg("lis_sca")
'lis_ord.col = 11
'lis_ord.Text = "Uni"

For j = 0 To lis_ord.Cols - 1
    If j < 3 Then
      lis_ord.MergeCol(j) = True
    Else
      lis_ord.MergeCol(j) = False
    End If
Next j

If ord_mac.Count > 0 Then aggiorna
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub


