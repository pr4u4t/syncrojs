VERSION 5.00
Object = "{5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0"; "MSFLXGRD.OCX"
Begin VB.Form pag_pro_day 
   Appearance      =   0  'Flat
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6375
   ClientLeft      =   2115
   ClientTop       =   2370
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
   Begin VB.TextBox txt_tur 
      BackColor       =   &H00C0FFFF&
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
      Left            =   1200
      MaxLength       =   35
      TabIndex        =   4
      Text            =   "1"
      Top             =   1800
      Width           =   615
   End
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
      Height          =   1635
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   12100
      Begin VB.Timer tim_ref 
         Interval        =   5000
         Left            =   3960
         Top             =   3360
      End
      Begin MSFlexGridLib.MSFlexGrid lis_pro 
         Height          =   1065
         Left            =   -45
         TabIndex        =   1
         Top             =   0
         Width           =   12045
         _ExtentX        =   21246
         _ExtentY        =   1879
         _Version        =   393216
         Rows            =   1
         Cols            =   12
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
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
      End
   End
   Begin VB.Label Lab_tur 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Turno"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   0
      TabIndex        =   5
      Top             =   1800
      Width           =   1140
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "PRODUZIONE GIORNALIERA"
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
      Picture         =   "pag_pro_day.frx":0000
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "pag_pro_day"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim primavolta As Boolean
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Private Sub aggiorna()
Dim puntatore As Integer

Dim i As Integer, j As Integer

Dim pro_tg As tip_pro_tur_gio_fosber21_FOS
Dim ind As Integer
Dim s As String

puntatore = pro_set.tur     ' evidenzio il turno attuale
Me.txt_tur = pro_set.tur
lis_pro.Redraw = False


    ' ABorelli_FI09/09/2005_1    metto i dati nella griglia
    If Not primavolta Then
          i = lis_pro.Rows
          For j = 1 To i - 1
              lis_pro.RemoveItem (1)
          Next j
    End If
    s = sta_tag.get_pro_tg()
    CopyType pro_tg, s, Len(pro_tg)
    For j = 1 To 3  ' per ogni turno
           lis_pro.AddItem ( _
           msg("ptg_tur") + " " + Format$(j) & vbTab & _
           for_dat(pro_tg.tur(j).dat_ini) & vbTab & _
           for_ora(pro_tg.tur(j).ora_ini) & vbTab & _
           for_dat(pro_tg.tur(j).dat_fin) & vbTab & _
           for_ora(pro_tg.tur(j).ora_fin) & vbTab & _
           for_met(pro_tg.tur(j).met_lin) & vbTab & _
           for_met_qua(pro_tg.tur(j).met_qua) & vbTab & _
           for_val(pro_tg.tur(j).vel_med) & vbTab & _
           sec_ore(pro_tg.tur(j).tem_fer) & vbTab & _
           sec_ore(pro_tg.tur(j).tem_mar) & vbTab & _
           for_val(pro_tg.tur(j).num_fer) & vbTab & _
           for_val(pro_tg.tur(j).num_cam))
    Next j
           lis_pro.AddItem ( _
           msg("ptg_gio") + " " & vbTab & _
           for_dat(pro_tg.gio.dat_ini) & vbTab & _
           for_ora(pro_tg.gio.ora_ini) & vbTab & _
           for_dat(pro_tg.gio.dat_fin) & vbTab & _
           for_ora(pro_tg.gio.ora_fin) & vbTab & _
           for_met(pro_tg.gio.met_lin) & vbTab & _
           for_met_qua(pro_tg.gio.met_qua) & vbTab & _
           for_val(pro_tg.gio.vel_med) & vbTab & _
           sec_ore(pro_tg.gio.tem_fer) & vbTab & _
           sec_ore(pro_tg.gio.tem_mar) & vbTab & _
           for_val(pro_tg.gio.num_fer) & vbTab & _
           for_val(pro_tg.gio.num_cam))

  For i = 1 To 4
    lis_pro.Row = i
    lis_pro.RowHeight(i) = Int(lis_pro.RowHeight(i) * pro_set.PercX / 100)
    For j = 0 To lis_pro.Cols - 1
        lis_pro.col = j
        lis_pro.CellForeColor = vbBlack                       ' caratteri in nero
        If (i Mod 2) <> 0 Then
            lis_pro.CellBackColor = COL_GRAY_ORD           ' sfondo grigio chiaro
        Else
            lis_pro.CellBackColor = vbWhite           ' sfondo bianco
        End If
        If i = puntatore Then
              lis_pro.CellBackColor = COL_DARK_Green    ' sfondo verde
              lis_pro.CellForeColor = vbWhite         ' caratteri in bianco
        End If
        lis_pro.CellFontBold = True                     ' grassetto
    Next j
  Next i

primavolta = False
lis_pro.Redraw = True

End Sub


Sub ini_ges_gri_ord()
Dim i As Integer

'totale larghezza MSFlexGrid = 11960 per lista ordini eseguiti
lis_pro.ColWidth(0) = Int(1000 * ((pro_set.PercX / 100)))  'turno
lis_pro.ColWidth(1) = Int(1000 * (pro_set.PercX / 100))   ' data inizio
lis_pro.ColWidth(2) = Int(1000 * (pro_set.PercX / 100))    ' ora inizio
lis_pro.ColWidth(3) = Int(1000 * (pro_set.PercX / 100))    ' data fine
lis_pro.ColWidth(4) = Int(1000 * (pro_set.PercX / 100))   ' ora fine
lis_pro.ColWidth(5) = Int(1000 * (pro_set.PercX / 100))   ' metri lineari
lis_pro.ColWidth(6) = Int(1000 * (pro_set.PercX / 100))   ' metri quadri
lis_pro.ColWidth(7) = Int(1000 * (pro_set.PercX / 100))    ' vel media
lis_pro.ColWidth(8) = Int(1000 * (pro_set.PercX / 100))    ' tempo fermo
lis_pro.ColWidth(9) = Int(1000 * (pro_set.PercX / 100))    ' tempo marcia
lis_pro.ColWidth(10) = Int(1000 * (pro_set.PercX / 100))   ' numero fermi
lis_pro.ColWidth(11) = Int(1000 * (pro_set.PercX / 100))  ' numero cambi

lis_pro.Row = 0
lis_pro.col = 0
For i = 0 To lis_pro.Cols - 1
  lis_pro.CellBackColor = ULTRA_DARK_GREY
  lis_pro.CellForeColor = vbWhite
  lis_pro.CellFontBold = True
  lis_pro.CellFontSize = Int(lis_pro.CellFontSize * (pro_set.PercX / 100))   ' 120
  lis_pro.CellAlignment = flexAlignLeftCenter
  If lis_pro.col = lis_pro.Cols - 1 Then
    Exit For
  Else
    lis_pro.col = lis_pro.col + 1
  End If
Next i

tra_gri

End Sub




Sub tra_gri()
lis_pro.Row = 0
lis_pro.col = 0
lis_pro.Text = msg("ptg_tur")
lis_pro.col = 1
lis_pro.Text = msg("ptg_dat_ini")
lis_pro.col = 2
lis_pro.Text = msg("ptg_ora_ini")
lis_pro.col = 3
lis_pro.Text = msg("ptg_dat_fin")
lis_pro.col = 4
lis_pro.Text = msg("ptg_ora_fin")
lis_pro.col = 5
lis_pro.Text = msg("ptg_met_pro")
lis_pro.col = 6
lis_pro.Text = msg("ptg_met_qua")
lis_pro.col = 7
lis_pro.Text = msg("ptg_vel_med")
lis_pro.col = 8
lis_pro.Text = msg("ptg_tem_fer")
lis_pro.col = 9
lis_pro.Text = msg("ptg_tem_mar")
lis_pro.col = 10
lis_pro.Text = msg("ptg_num_fer")
lis_pro.col = 11
lis_pro.Text = msg("ptg_num_cam")

End Sub


Private Sub eve_key_F10(Shift As Integer)
Select Case Shift
  Case 0
  eve_key.chg pag_pro
  Case Else
End Select
End Sub

Private Sub eve_key_F2(Shift As Integer)
    ' cambio turno
    eve_key.chg cam_tur, True
End Sub

Private Sub eve_key_F3(Shift As Integer)
    eve_key.chg con_azz, True
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

Private Sub tim_ref_Timer()
' timer di refresh
    aggiorna
End Sub

Private Sub Form_Activate()

tra_gri

txt_idle.SetFocus
Call aggiorna

End Sub

Private Sub Form_Load()
Debug.Print "Inizio Load " & Me.name
Set eve_key = New key
eve_key.create Me
Carica Me
lis_pro.RowHeight(0) = Int(lis_pro.RowHeight(0) * (pro_set.PercX / 100))
lis_pro.Height = lis_pro.RowHeight(0) * 8

primavolta = True

ini_ges_gri_ord
txt_tur = Format$(pro_set.tur)

aggiorna
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub

