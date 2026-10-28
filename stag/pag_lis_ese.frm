VERSION 5.00
Object = "{5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0"; "msflxgrd.ocx"
Begin VB.Form pag_lis_ese 
   Appearance      =   0  'Flat
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6375
   ClientLeft      =   -495
   ClientTop       =   2100
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
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   6375
   ScaleWidth      =   12045
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox txt_idle 
      Height          =   315
      Left            =   1440
      TabIndex        =   3
      Text            =   "Text1"
      Top             =   -1000
      Width           =   375
   End
   Begin VB.Frame Frm_grid 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BorderStyle     =   0  'None
      Caption         =   "Frame1"
      ForeColor       =   &H80000008&
      Height          =   6945
      Left            =   0
      TabIndex        =   0
      Top             =   330
      Width           =   12100
      Begin VB.Frame frm_rep_pro 
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         Height          =   1950
         Left            =   0
         TabIndex        =   18
         Top             =   4170
         Visible         =   0   'False
         Width           =   12150
         Begin MSFlexGridLib.MSFlexGrid lis_rep 
            Height          =   3465
            Left            =   0
            TabIndex        =   19
            Top             =   5
            Visible         =   0   'False
            Width           =   12045
            _ExtentX        =   21246
            _ExtentY        =   6112
            _Version        =   393216
            Rows            =   4
            Cols            =   5
            FixedRows       =   0
            FixedCols       =   0
            BackColor       =   16777215
            BackColorFixed  =   16777215
            BackColorBkg    =   16777215
            GridColor       =   0
            AllowBigSelection=   0   'False
            TextStyle       =   1
            TextStyleFixed  =   1
            FocusRect       =   2
            FillStyle       =   1
            GridLines       =   3
            GridLinesFixed  =   3
            ScrollBars      =   0
            MergeCells      =   2
            BorderStyle     =   0
            Appearance      =   0
         End
      End
      Begin VB.Frame frm_rep 
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         Height          =   840
         Left            =   0
         TabIndex        =   4
         Top             =   3320
         Visible         =   0   'False
         Width           =   12150
         Begin VB.TextBox txt_num_fer 
            Appearance      =   0  'Flat
            BorderStyle     =   0  'None
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   11.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00C00000&
            Height          =   315
            Left            =   11280
            TabIndex        =   25
            Text            =   "235"
            Top             =   330
            Width           =   600
         End
         Begin VB.TextBox txt_tem_fer 
            Appearance      =   0  'Flat
            BorderStyle     =   0  'None
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   11.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00C00000&
            Height          =   315
            Left            =   9120
            TabIndex        =   24
            Text            =   "235"
            Top             =   330
            Width           =   960
         End
         Begin VB.TextBox txt_tem_mar 
            Appearance      =   0  'Flat
            BorderStyle     =   0  'None
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   11.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00C00000&
            Height          =   315
            Left            =   10200
            TabIndex        =   23
            Text            =   "235"
            Top             =   330
            Width           =   960
         End
         Begin VB.TextBox txt_vel_med 
            Appearance      =   0  'Flat
            BorderStyle     =   0  'None
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   11.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00C00000&
            Height          =   315
            Left            =   7860
            TabIndex        =   17
            Text            =   "235"
            Top             =   330
            Width           =   600
         End
         Begin VB.TextBox txt_met_pro 
            Appearance      =   0  'Flat
            BorderStyle     =   0  'None
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   11.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00C00000&
            Height          =   315
            Left            =   6390
            TabIndex        =   16
            Text            =   "14520"
            Top             =   330
            Width           =   1000
         End
         Begin VB.TextBox txt_dat_fin 
            Appearance      =   0  'Flat
            BorderStyle     =   0  'None
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   11.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00C00000&
            Height          =   315
            Left            =   5220
            TabIndex        =   15
            Text            =   "22/09/00"
            Top             =   330
            Width           =   1000
         End
         Begin VB.TextBox txt_ora_fin 
            Appearance      =   0  'Flat
            BorderStyle     =   0  'None
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   11.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00C00000&
            Height          =   315
            Left            =   4080
            TabIndex        =   14
            Text            =   "10.15.32"
            Top             =   330
            Width           =   1000
         End
         Begin VB.TextBox txt_dat_ini 
            Appearance      =   0  'Flat
            BorderStyle     =   0  'None
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   11.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00C00000&
            Height          =   315
            Left            =   2790
            TabIndex        =   13
            Text            =   "22/09/00"
            Top             =   330
            Width           =   1000
         End
         Begin VB.TextBox txt_ora_ini 
            Appearance      =   0  'Flat
            BorderStyle     =   0  'None
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   11.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00C00000&
            Height          =   315
            Left            =   1650
            TabIndex        =   12
            Text            =   "10.15.32"
            Top             =   330
            Width           =   1000
         End
         Begin VB.Label num_fer 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "N fermi"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   225
            Left            =   11280
            TabIndex        =   22
            Top             =   45
            Width           =   1095
         End
         Begin VB.Label lab_tem_mar 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "T. marcia"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   225
            Left            =   10200
            TabIndex        =   21
            Top             =   45
            Width           =   1005
         End
         Begin VB.Label lb_tem_fer 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "T. fermo"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   225
            Left            =   9120
            TabIndex        =   20
            Top             =   45
            Width           =   945
         End
         Begin VB.Line Line4 
            X1              =   11160
            X2              =   11160
            Y1              =   0
            Y2              =   720
         End
         Begin VB.Line Line3 
            X1              =   10080
            X2              =   10080
            Y1              =   0
            Y2              =   720
         End
         Begin VB.Line Line1 
            BorderWidth     =   3
            X1              =   -60
            X2              =   14790
            Y1              =   0
            Y2              =   0
         End
         Begin VB.Line Line2 
            X1              =   9000
            X2              =   9000
            Y1              =   0
            Y2              =   720
         End
         Begin VB.Line Line17 
            BorderWidth     =   2
            X1              =   0
            X2              =   15045
            Y1              =   720
            Y2              =   720
         End
         Begin VB.Line Line16 
            X1              =   6300
            X2              =   6300
            Y1              =   0
            Y2              =   720
         End
         Begin VB.Line Line11 
            X1              =   7770
            X2              =   7770
            Y1              =   0
            Y2              =   720
         End
         Begin VB.Line Line9 
            X1              =   5145
            X2              =   5145
            Y1              =   0
            Y2              =   720
         End
         Begin VB.Line Line8 
            X1              =   3990
            X2              =   3990
            Y1              =   0
            Y2              =   720
         End
         Begin VB.Line Line7 
            X1              =   2715
            X2              =   2715
            Y1              =   0
            Y2              =   720
         End
         Begin VB.Line Line6 
            X1              =   1560
            X2              =   1560
            Y1              =   0
            Y2              =   720
         End
         Begin VB.Label lab_vel_med 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Vel. Media"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   225
            Left            =   7860
            TabIndex        =   11
            Top             =   45
            Width           =   1100
         End
         Begin VB.Label lab_met_pro 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Metri Prodotti"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   225
            Left            =   6405
            TabIndex        =   10
            Top             =   45
            Width           =   1300
         End
         Begin VB.Label lab_dat_fin 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Data Fine"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   225
            Left            =   5235
            TabIndex        =   9
            Top             =   45
            Width           =   1005
         End
         Begin VB.Label lab_ora_fin 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Ora Fine"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   225
            Left            =   4080
            TabIndex        =   8
            Top             =   45
            Width           =   1005
         End
         Begin VB.Label lab_dat_ini 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Data Inizio"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   225
            Left            =   2805
            TabIndex        =   7
            Top             =   45
            Width           =   1335
         End
         Begin VB.Label lab_ora_ini 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Ora Inizio"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   225
            Left            =   1650
            TabIndex        =   6
            Top             =   45
            Width           =   1000
         End
         Begin VB.Label Label1 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Report"
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
            TabIndex        =   5
            Top             =   225
            Width           =   915
         End
         Begin VB.Image Image1 
            Height          =   540
            Left            =   60
            Picture         =   "pag_lis_ese.frx":0000
            Stretch         =   -1  'True
            Top             =   105
            Width           =   390
         End
         Begin VB.Shape Shape1 
            BackColor       =   &H00808000&
            BackStyle       =   1  'Opaque
            BorderColor     =   &H00808000&
            BorderStyle     =   0  'Transparent
            FillColor       =   &H00808000&
            Height          =   555
            Left            =   435
            Top             =   105
            Width           =   1065
         End
      End
      Begin MSFlexGridLib.MSFlexGrid lis_ord_ese 
         Height          =   3345
         Left            =   0
         TabIndex        =   1
         Top             =   0
         Width           =   12045
         _ExtentX        =   21246
         _ExtentY        =   5900
         _Version        =   393216
         Rows            =   1
         Cols            =   11
         FixedRows       =   0
         FixedCols       =   0
         BackColor       =   16777215
         BackColorFixed  =   16777215
         BackColorBkg    =   16777215
         GridColor       =   0
         AllowBigSelection=   0   'False
         FocusRect       =   2
         GridLinesFixed  =   1
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
      Caption         =   "LISTA ORDINI ESEGUITI"
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
      Width           =   6825
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "pag_lis_ese.frx":07C2
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "pag_lis_ese"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim pun_lis_ese As Integer
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
Private WithEvents eve_ord_mac_ese As CollezioneOrdini
Attribute eve_ord_mac_ese.VB_VarHelpID = -1
Private Sub aggiorna()

Dim i As Integer, j As Integer

Dim prog As String, Cod_ond As String, Com_car As String, alt_bob As String, uni As String, pun_pre As String, pun_suc As String, cod_abb As String
Dim scatole As String, sca_lar As String, sca_lun As String, cod_com As String, resa As String, nom_cli As String, cod_cli As String
Dim liv_ord As String

Dim a As Integer
Dim ord As Tip_Ordine
'Icilio3545 11-01-2004
Dim pun, ind As Integer

If keyup Then a = 1
If keydown Then a = -1


lis_ord_ese.Redraw = False

ultimo = IIf(ultimo > ord_mac_ese.Count, ord_mac_ese.Count, ultimo)
primo = IIf(primo > ord_mac_ese.Count, ord_mac_ese.Count, primo)
puntatore = IIf(puntatore > ord_mac_ese.Count, ord_mac_ese.Count, puntatore)
pun_lis_ese = IIf(pun_lis_ese > ord_mac_ese.Count, ord_mac_ese.Count, pun_lis_ese)

If ridisegna Then

    If Not primavolta Then
          i = lis_ord_ese.Rows
          For j = 1 To i - 1
              lis_ord_ese.RemoveItem (1)
          Next j
    End If

    For j = primo To ultimo
      If j > 0 Then
        CopyType ord, ord_mac_ese.item(indice:=j).ordine, Len(ord_mac_ese.item(indice:=j).ordine)
        prog = j
        'uni = ord_mac_ese.item(indice:=j).stag_id
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
          lis_ord_ese.AddItem (prog & vbTab & cod_abb & vbTab & Cod_ond & vbTab & liv_ord & vbTab & cod_com & vbTab & _
           nom_cli & vbTab & cod_cli & vbTab & sca_lar & vbTab & sca_lun & vbTab & _
           resa & vbTab & scatole) ' & vbTab & IIf(i Mod (val(par_mac.car_tec.num_liv_tag) + 1) = 1, uni, ""))
        Next i
      End If
    Next j
End If

If ridisegna Then
  pun = primo - 1
  For i = 1 To lis_ord_ese.Rows - 1
    lis_ord_ese.Row = i
    lis_ord_ese.RowHeight(i) = Int(lis_ord_ese.RowHeight(i) * pro_set.PercX / 100)
    'Icilio3545 11-01-2004
    ind = IIf((i Mod (num_liv_lis_ord) = 0), num_liv_lis_ord, (i Mod (num_liv_lis_ord)))
    pun = IIf(ind = 1, pun + 1, pun)
    For j = 0 To lis_ord_ese.Cols - 1
        lis_ord_ese.col = j
        If j = 0 Or j = lis_ord_ese.Cols - 1 Then
          lis_ord_ese.CellAlignment = flexAlignRightCenter
        ElseIf (j > 0 And j < 3) Or (j >= 7 And j <= 9) Then
          lis_ord_ese.CellAlignment = flexAlignCenterCenter
        Else
          lis_ord_ese.CellAlignment = flexAlignLeftCenter
        End If
        'Icilio3545 11-01-2004 Cambio colore a seconda dell'ordine cut to pattern/mark
        lis_ord_ese.CellForeColor = vbBlack                       ' caratteri in nero
        If (ord_mac_ese.item(pun).cut_to_mark(ind) > -1) And (ord_mac_ese.item(pun).cut_to_mark(ind) < 3) Then
          lis_ord_ese.CellForeColor = Choose(ord_mac_ese.item(pun).cut_to_mark(ind) + 1, vbBlack, COL_DARK_Green, COL_FADED_ORANGE)
        End If
        If (i Mod num_liv_lis_ord * 2) >= 1 And (i Mod num_liv_lis_ord * 2) <= num_liv_lis_ord Then
            lis_ord_ese.CellBackColor = COL_GRAY_ORD           ' sfondo grigio chiaro
        Else
            lis_ord_ese.CellBackColor = vbWhite           ' sfondo bianco
        End If
        If j < 2 Or j = 4 Or j = 8 Then lis_ord_ese.CellFontBold = True                     ' grassetto
        If j < 3 And num_liv_lis_ord > 1 Then
          lis_ord_ese.CellFontSize = 10 * pro_set.PercX / 100                         ' dimensione carattere
        Else
          lis_ord_ese.CellFontSize = Int(lis_ord_ese.CellFontSize * pro_set.PercX / 100)        ' dimensione carattere
        End If
        If i <= puntatore * num_liv_lis_ord And i >= puntatore * num_liv_lis_ord - (num_liv_lis_ord - 1) Then
            lis_ord_ese.CellBackColor = COL_DARK_Green        ' sfondo verde
            lis_ord_ese.CellForeColor = vbWhite             ' caratteri in bianco
        End If
    Next j
  Next i

Else
      For i = puntatore * num_liv_lis_ord To puntatore * num_liv_lis_ord - (num_liv_lis_ord - 1) Step -1
        If i > 0 And i < lis_ord_ese.Rows Then
          lis_ord_ese.Row = i
          For j = 0 To lis_ord_ese.Cols - 1
              lis_ord_ese.col = j
              lis_ord_ese.CellBackColor = COL_DARK_Green    ' sfondo verde
              'Icilio3545 11-01-2004
              lis_ord_ese.CellForeColor = vbWhite         ' caratteri in bianco
              pun = pun_lis_ese
          Next j
        End If
      Next i
      For i = (puntatore + a) * num_liv_lis_ord To (puntatore + a) * num_liv_lis_ord - (num_liv_lis_ord - 1) Step -1
        If i > 0 And i < lis_ord_ese.Rows Then
          lis_ord_ese.Row = i
          For j = 0 To lis_ord_ese.Cols - 1
              lis_ord_ese.col = j
              lis_ord_ese.CellBackColor = IIf((puntatore + a) * num_liv_lis_ord Mod (num_liv_lis_ord * 2) >= 1 And (puntatore + a) * num_liv_lis_ord _
              Mod (num_liv_lis_ord * 2) <= num_liv_lis_ord, COL_GRAY_ORD, vbWhite) ' sfondo grigio chiaro o bianco
              'Icilio3545 11-01-2004
              pun = pun_lis_ese + a
              lis_ord_ese.CellForeColor = Choose(ord_mac_ese.item(pun).cut_to_mark() + 1, vbBlack, COL_DARK_Green, COL_FADED_ORANGE)
          Next j
        End If
      
      Next i
      
End If


primavolta = False
lis_ord_ese.Redraw = True

'gestione Report Produzione
If pun_lis_ese > 0 And ord_mac_ese.Count > 0 And frm_rep.Visible Then
    ges_rep (pun_lis_ese)
End If
End Sub


Sub ini_ges_gri_ord()
Dim i As Integer

'totale larghezza MSFlexGrid = 11960 per lista ordini eseguiti
lis_ord_ese.ColWidth(0) = Int(370 * ((pro_set.PercX / 100))) 'progressivo
lis_ord_ese.ColWidth(1) = Int(1680 * (pro_set.PercX / 100))  'codice abbinamento
lis_ord_ese.ColWidth(2) = Int(570 * (pro_set.PercX / 100))  'codice onda
lis_ord_ese.ColWidth(3) = Int(690 * (pro_set.PercX / 100))   'livello
lis_ord_ese.ColWidth(4) = Int(1620 * (pro_set.PercX / 100))   'commessa
lis_ord_ese.ColWidth(5) = Int(2890 * (pro_set.PercX / 100))   ' nome cliente
lis_ord_ese.ColWidth(6) = Int(1340 * (pro_set.PercX / 100))   ' codice cliente
lis_ord_ese.ColWidth(7) = Int(750 * (pro_set.PercX / 100))     'larghezza
lis_ord_ese.ColWidth(8) = Int(750 * (pro_set.PercX / 100))     'lunghezza
lis_ord_ese.ColWidth(9) = Int(510 * (pro_set.PercX / 100))    'resa
lis_ord_ese.ColWidth(10) = Int(790 * (pro_set.PercX / 100))     'scatole
'lis_ord_ese.ColWidth(11) = Int(650 * (pro_set.PercX / 100))  'uni

lis_ord_ese.Row = 0
lis_ord_ese.col = 0
For i = 0 To lis_ord_ese.Cols - 1
  lis_ord_ese.CellBackColor = ULTRA_DARK_GREY
  lis_ord_ese.CellForeColor = vbWhite
  lis_ord_ese.CellFontBold = True
  lis_ord_ese.CellFontSize = Int(lis_ord_ese.CellFontSize * (pro_set.PercX / 100))
  If i = 0 Or i = lis_ord_ese.Cols - 1 Then
    lis_ord_ese.CellAlignment = flexAlignRightCenter
  ElseIf i = 1 Or (i >= 7 And i <= 9) Then
    lis_ord_ese.CellAlignment = flexAlignCenterCenter
  Else
    lis_ord_ese.CellAlignment = flexAlignLeftCenter
  End If
  If lis_ord_ese.col = lis_ord_ese.Cols - 1 Then
    Exit For
  Else
    lis_ord_ese.col = lis_ord_ese.col + 1
  End If
Next i

lis_ord_ese.col = 0
lis_ord_ese.Text = ""
lis_ord_ese.col = 1
lis_ord_ese.Text = msg("lis_cod_abb")
lis_ord_ese.col = 2
lis_ord_ese.Text = msg("lis_ond")
lis_ord_ese.col = 3
lis_ord_ese.Text = msg("lis_liv")
lis_ord_ese.col = 4
lis_ord_ese.Text = msg("lis_com")
lis_ord_ese.col = 5
lis_ord_ese.Text = msg("lis_nom_cli")
lis_ord_ese.col = 6
lis_ord_ese.Text = msg("lis_cod_cli")
lis_ord_ese.col = 7
lis_ord_ese.Text = msg("lis_lar")
lis_ord_ese.col = 8
lis_ord_ese.Text = msg("lis_lun")
lis_ord_ese.col = 9
lis_ord_ese.Text = msg("lis_res")
lis_ord_ese.col = 10
lis_ord_ese.Text = msg("lis_sca")
'lis_ord_ese.col = 11
'lis_ord_ese.Text = "Uni"

For i = 0 To lis_ord_ese.Cols - 1
    If i < 3 Then
      lis_ord_ese.MergeCol(i) = True
    Else
      lis_ord_ese.MergeCol(i) = False
    End If
Next i

End Sub

Sub ini_ges_gri_rep()
Dim i As Integer
Dim j As Integer

'totale larghezza MSFlexGrid = 11960 per lista report
'lis_rep.ColWidth(0) = Int(2660 * ((pro_set.PercX / 100))) 'livello
'lis_rep.ColWidth(1) = Int(3100 * ((pro_set.PercX / 100))) 'fogli buoni
'lis_rep.ColWidth(2) = Int(3100 * ((pro_set.PercX / 100))) 'fogli errati
'lis_rep.ColWidth(3) = Int(3100 * ((pro_set.PercX / 100))) 'fogli scarto
'RFAZZI_FI 09-01-08
lis_rep.ColWidth(0) = Int(2100 * ((pro_set.PercX / 100))) 'livello
lis_rep.ColWidth(1) = Int(2500 * ((pro_set.PercX / 100))) 'fogli buoni
lis_rep.ColWidth(2) = Int(2500 * ((pro_set.PercX / 100))) 'fogli errati
lis_rep.ColWidth(3) = Int(2500 * ((pro_set.PercX / 100))) 'fogli scarto
lis_rep.ColWidth(4) = Int(2500 * ((pro_set.PercX / 100))) 'mm scarto al gruppo di scarto

For i = 0 To lis_rep.Rows - 1
    lis_rep.Row = i
    lis_rep.col = 0
    For j = 0 To lis_rep.Cols - 1
      If i = 0 Then
          lis_rep.CellBackColor = COL_Yellow
          lis_rep.CellForeColor = vbBlack
      Else
          lis_rep.CellBackColor = COL_REPORT
          lis_rep.CellForeColor = vbBlue
      End If
      lis_rep.CellFontBold = True
      lis_rep.CellFontSize = Int((15) * (pro_set.PercX / 100))
      'lis_rep.CellFontSize = IIf(i = 0, Int((15) * (pro_set.PercX / 100)), Int(13 * (pro_set.PercX / 100)))
      lis_rep.CellAlignment = IIf(j = 0, flexAlignLeftCenter, flexAlignCenterCenter)
      If lis_rep.col = lis_rep.Cols - 1 Then
        Exit For
      Else
        lis_rep.col = lis_rep.col + 1
      End If
    Next j
    lis_rep.RowHeight(i) = IIf(i = 0, (lis_rep.RowHeight(i) + 180) * (pro_set.PercX / 100), (lis_rep.RowHeight(i) + 140) * (pro_set.PercX / 100))
Next i

lis_rep.Row = 0
lis_rep.col = 0
lis_rep.Text = msg("rep_liv")
lis_rep.col = 1
lis_rep.Text = msg("rep_fog_buo")
lis_rep.col = 2
lis_rep.Text = msg("rep_fog_err")
lis_rep.col = 3
lis_rep.Text = msg("rep_fog_sca")

'RFAZZI_FI 09-01-08
lis_rep.col = 4
lis_rep.Text = "mm scartati " 'msg("mm_sca")


End Sub


Sub ges_rep(pun_ord As Integer)
Dim fog_buo As String, fog_sca As String, fog_err As String, dat_fin As String, dat_ini As String, ora_fin As String, ora_ini As String, met_pro As String, tem_fer As String, tem_mar As String, num_fer As String
Dim vel_med As String
Dim ord As Tip_Ordine
Dim liv_ord As String
Dim i As Integer
Dim rie_rep_pro(1 To 3) As Boolean
Dim j As Integer
'RFAZZI_FI 09-01-08
Dim mil_sca As String

CopyType ord, ord_mac_ese.item(indice:=pun_ord).ordine, Len(ord_mac_ese.item(indice:=pun_ord).ordine)
dat_ini = Trim$(Rie_nul(ord.rep_pro.dat_ini))
dat_fin = Trim$(Rie_nul(ord.rep_pro.dat_fin))
ora_ini = Trim$(Rie_nul(ord.rep_pro.ora_ini))
ora_fin = Trim$(Rie_nul(ord.rep_pro.ora_fin))
vel_med = Trim$(IIf(abi_mis_ang, con_met_to_fet(Trim$(Rie_nul(ord.rep_pro.vel_med))), Rie_nul(ord.rep_pro.vel_med)))
met_pro = Trim$(Rie_nul(IIf(abi_mis_ang, con_met_to_fet(Trim$(Rie_nul(val(ord.rep_pro.met_tot) \ 1))), Rie_nul(val(ord.rep_pro.met_tot) \ 1))))
' ABorelli_FI09/09/2005_1
tem_fer = sec_ore(Rie_nul(ord.rep_pro.tem_fer))
tem_mar = sec_ore(Rie_nul(ord.rep_pro.tem_mar))
num_fer = Trim$(Rie_nul(ord.rep_pro.num_fer))
txt_ora_ini = ora_ini
txt_ora_fin = ora_fin
txt_dat_ini = dat_ini
txt_dat_fin = dat_fin
txt_met_pro = met_pro
txt_vel_med = vel_med
txt_tem_fer = tem_fer
txt_tem_mar = tem_mar
txt_num_fer = num_fer
For i = 1 To 3
  lis_rep.Row = i
  If Trim$(ord.ord(i).Ord_liv) <> "" And val(ord.ord(i).Ord_liv) > 0 Then
    Select Case val(ord.ord(i).Ord_liv)
      Case LIV_BAS: liv_ord = Format$(msg("liv_bas"))
      Case LIV_MED: liv_ord = Format$(msg("liv_med"))
      Case LIV_ALT: liv_ord = Format$(msg("liv_alt"))
      Case Else: liv_ord = ""
    End Select
    If Not rie_rep_pro(val(ord.ord(i).Ord_liv)) Then
      fog_buo = Rie_nul(ord.rep_pro.liv(i).fog_buo)
      fog_sca = Rie_nul(ord.rep_pro.liv(i).fog_sca)
      fog_err = Rie_nul(ord.rep_pro.liv(i).fog_err)
      'RAFZZI_FI 09-01-08
      mil_sca = Rie_nul(ord.rep_pro.liv(i).mil_sca)
      rie_rep_pro(val(ord.ord(i).Ord_liv)) = True
      lis_rep.col = 0
      lis_rep.Text = liv_ord
      lis_rep.col = 1
      lis_rep.Text = fog_buo
      lis_rep.col = 2
      lis_rep.Text = fog_err
      lis_rep.col = 3
      lis_rep.Text = fog_sca
      'RAFZZI_FI 09-01-08
      lis_rep.col = 4
      lis_rep.Text = mil_sca
    
    Else
      For j = 0 To lis_rep.Cols - 1
        lis_rep.col = j
        lis_rep.Text = ""
      Next j
    End If
  Else
    For j = 0 To lis_rep.Cols - 1
      lis_rep.col = j
      lis_rep.Text = ""
    Next j
  End If
Next i
End Sub

Sub tra_gri()
lis_ord_ese.Row = 0
lis_ord_ese.col = 0
lis_ord_ese.Text = ""
lis_ord_ese.col = 1
lis_ord_ese.Text = msg("lis_cod_abb")
lis_ord_ese.col = 2
lis_ord_ese.Text = msg("lis_ond")
lis_ord_ese.col = 3
lis_ord_ese.Text = msg("lis_liv")
lis_ord_ese.col = 4
lis_ord_ese.Text = msg("lis_com")
lis_ord_ese.col = 5
lis_ord_ese.Text = msg("lis_nom_cli")
lis_ord_ese.col = 6
lis_ord_ese.Text = msg("lis_cod_cli")
lis_ord_ese.col = 7
lis_ord_ese.Text = msg("lis_lar")
lis_ord_ese.col = 8
lis_ord_ese.Text = msg("lis_lun")
lis_ord_ese.col = 9
lis_ord_ese.Text = msg("lis_res")
lis_ord_ese.col = 10
lis_ord_ese.Text = msg("lis_sca")
'lis_ord_ese.col = 11
'lis_ord_ese.Text = "Uni"

lis_rep.col = 0
lis_rep.Text = msg("rep_liv")
lis_rep.col = 1
lis_rep.Text = msg("rep_fog_buo")
lis_rep.col = 2
lis_rep.Text = msg("rep_fog_err")
lis_rep.col = 3
lis_rep.Text = msg("rep_fog_sca")
'RFAZZI_FI 09-01-08
lis_rep.col = 4
lis_rep.Text = "mm scarto"   'msg("mm_sca")

End Sub

Private Sub eve_key_F3(Shift As Integer)
Select Case Shift
  Case 0
  'del block
  If ord_mac_ese.Count > 0 Then
      del_ord_ese.txt_from = CStr(pun_lis_ese)
      del_ord_ese.txt_to = CStr(pun_lis_ese)
      eve_key.chg del_ord_ese, keep:=1
  End If
  Case Else
End Select

End Sub


Private Sub eve_key_F10(Shift As Integer)
Select Case Shift
  Case 0
    'RFAZZI_FI 31-01-08
    Tasti.des_fun(2).Visible = True
    Tasti.des_fun(7).Visible = True
    eve_key.chg pag_pro
  Case Else
End Select
End Sub
Private Sub eve_key_F2(Shift As Integer)
' ABorelli_FI09/09/2005_1
If par_abi.abi_rep_pro Then
    Select Case Shift
      Case 0
      'production day
          eve_key.chg pag_pro_day, keep:=1
      Case Else
    End Select
End If
End Sub

Private Sub eve_key_F5(Shift As Integer)
Select Case Shift
  Case 0
  'copio il blocco negli ordini macchina
  
  If ord_mac_ese.Count > 0 Then
      mov_ese.txt_from = CStr(pun_lis_ese)
      mov_ese.txt_to = CStr(pun_lis_ese)
      mov_ese.txt_dst = ""
      eve_key.chg mov_ese, keep:=1
  End If
  Case Else
End Select
End Sub

Private Sub eve_key_F6(Shift As Integer)
Select Case Shift
  Case 0
  ' trova ordine
  If ord_mac_ese.Count > 0 Then
      tro_ord.txt_cod = ""
      eve_key.chg tro_ord_ese, keep:=1
  End If
  Case Else
End Select
End Sub

Private Sub eve_key_F7(Shift As Integer)
Select Case Shift
  Case 0
    'Report ON/OFF
    'Iciliouff 08-07-2004
'    If 0 Then  'Arturo uff 02-03-04 (1) non permetto la visualizzazione dei report di produzione
    If par_abi.abi_rep_pro Then
      If ord_mac_ese.Count > 0 Then
        frm_rep.Visible = Not frm_rep.Visible
        frm_rep_pro.Visible = frm_rep.Visible
        lis_rep.Visible = frm_rep.Visible
        Select Case num_liv_lis_ord
          Case 2
            num_rig_ese = IIf(frm_rep.Visible, 12, 22)
          Case 3
            num_rig_ese = IIf(frm_rep.Visible, 12, 21)
        End Select
        num_lis_ord = num_rig_ese / num_liv_lis_ord
        lis_ord_ese.Height = lis_ord_ese.RowHeight(0) * (num_rig_ese + 1)
        pun_lis_ese = ord_mac_ese.leg_pun_ord
        If pun_lis_ese + (num_lis_ord - 1) <= ord_mac_ese.Count Then
          primo = pun_lis_ese
          ultimo = primo + (num_lis_ord - 1)
          puntatore = 1
        Else
          primo = IIf(pun_lis_ese - (num_lis_ord - 1) >= 1, pun_lis_ese - (num_lis_ord - 1), 1)
          ultimo = IIf(primo + (num_lis_ord - 1) <= ord_mac_ese.Count, primo + (num_lis_ord - 1), ord_mac_ese.Count)
          puntatore = IIf(primo > 1, num_lis_ord, pun_lis_ese)
        End If
        ridisegna = True
        Call aggiorna
      End If
    End If
  Case Else
End Select
End Sub

Private Sub eve_key_F9(Shift As Integer)
Select Case Shift
  Case 0
    'RFAZZI_FI 31-01-08
    Tasti.des_fun(2).Visible = True
    Tasti.des_fun(7).Visible = True
    eve_key.chg pag_lis_ord
  Case Else
End Select
End Sub

Private Sub eve_ord_mac_ese_Refresh(Shift As Boolean)
ridisegna = True
aggiorna
End Sub

Private Sub Form_Activate()

tra_gri

txt_idle.SetFocus
If pag_pre.name <> "tro_ord_ese" And pag_pre.name <> "mov_ese" And pag_pre.name <> "del_ord_ese" Then
      ord_mac_ese.scr_pun_ord (1)
End If
pun_lis_ese = ord_mac_ese.leg_pun_ord
If pun_lis_ese + (num_lis_ord - 1) <= ord_mac_ese.Count Then
  primo = pun_lis_ese
  ultimo = primo + (num_lis_ord - 1)
  puntatore = 1
Else
  primo = IIf(pun_lis_ese - (num_lis_ord - 1) >= 1, pun_lis_ese - (num_lis_ord - 1), 1)
  ultimo = IIf(primo + (num_lis_ord - 1) <= ord_mac_ese.Count, primo + (num_lis_ord - 1), ord_mac_ese.Count)
  puntatore = IIf(primo > 1, num_lis_ord, pun_lis_ese)
End If
ridisegna = True
Call aggiorna
'andrea 17-02-06
Tasti.des_fun(2).Visible = par_abi.abi_rep_pro
Tasti.des_fun(7).Visible = par_abi.abi_rep_pro
End Sub

Private Sub Form_KeyUp(KeyCode As Integer, Shift As Integer)

Dim pun_lis_ese_pre As Integer
Dim a As Integer

ridisegna = False
keyup = False
keydown = False



Select Case KeyCode

    Case vbKeyDown
        keydown = True
        pun_lis_ese_pre = pun_lis_ese
        pun_lis_ese = IIf(ord_mac_ese.Count > pun_lis_ese, pun_lis_ese + 1, ord_mac_ese.Count)
        If puntatore < num_lis_ord And pun_lis_ese <> pun_lis_ese_pre Then puntatore = puntatore + 1
        If pun_lis_ese = ultimo + 1 Then
              primo = primo + 1
              ultimo = ultimo + 1
              ridisegna = True
        End If
    
    Case vbKeyPageDown
        
        ridisegna = True
        If ord_mac_ese.Count > pun_lis_ese + num_lis_ord Then
              pun_lis_ese = pun_lis_ese + num_lis_ord
              primo = primo + num_lis_ord
              ultimo = IIf(ord_mac_ese.Count > ultimo + num_lis_ord, ultimo + num_lis_ord, ord_mac_ese.Count)
        Else
              If ultimo < ord_mac_ese.Count Then
                  a = ord_mac_ese.Count - ultimo
                  pun_lis_ese = ord_mac_ese.Count
                  primo = primo + num_lis_ord
                  ultimo = pun_lis_ese
                  puntatore = a
              Else
                  a = ultimo - primo + 1
                  pun_lis_ese = ultimo
                  puntatore = a
              End If
        End If

    Case vbKeyPageUp
        
        ridisegna = True
        If pun_lis_ese - num_lis_ord >= 1 Then
            
            If ultimo - primo = num_lis_ord - 1 Then
                pun_lis_ese = pun_lis_ese - num_lis_ord
                primo = IIf(primo - num_lis_ord > 1, primo - num_lis_ord, 1)
                ultimo = primo + (num_lis_ord - 1)
            Else
                ultimo = primo - 1
                primo = ultimo - (num_lis_ord - 1)
                pun_lis_ese = pun_lis_ese - num_lis_ord
            End If
        
        Else
            pun_lis_ese = 1
            puntatore = 1
            primo = 1
            ultimo = IIf(ord_mac_ese.Count > ultimo, num_lis_ord, ord_mac_ese.Count)
        End If
  
    Case vbKeyUp
        keyup = True
        If pun_lis_ese > 1 Then pun_lis_ese = pun_lis_ese - 1
        If puntatore > 1 Then puntatore = puntatore - 1
        If pun_lis_ese = primo - 1 Then
            If ultimo - primo = (num_lis_ord - 1) Then
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
Dim j As Integer
Debug.Print "Inizio Load " & Me.name
Set eve_key = New key
Set eve_ord_mac_ese = ord_mac_ese
eve_key.create Me
Carica Me
lis_ord_ese.RowHeight(0) = Int(lis_ord_ese.RowHeight(0) * (pro_set.PercX / 100))
num_rig_ese = IIf(num_liv_lis_ord = 2, 22, 21)
num_lis_ord = num_rig_ese / num_liv_lis_ord
frm_rep.Visible = False
frm_rep_pro.Visible = False
frm_rep.Top = lis_ord_ese.Top + lis_ord_ese.Height
frm_rep_pro.Top = frm_rep.Top + frm_rep.Height
lis_ord_ese.Height = lis_ord_ese.RowHeight(0) * (num_rig_ese + 1)
pun_lis_ese = 1
puntatore = 1
primo = 1
ultimo = IIf(ord_mac_ese.Count > num_lis_ord, num_lis_ord, ord_mac_ese.Count)
ridisegna = True

keyup = False
keydown = False

primavolta = True

ini_ges_gri_ord

ini_ges_gri_rep

If ord_mac_ese.Count > 0 Then aggiorna
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub






