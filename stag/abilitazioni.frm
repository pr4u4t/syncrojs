VERSION 5.00
Begin VB.Form abilitazioni 
   Appearance      =   0  'Flat
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   0  'None
   ClientHeight    =   5970
   ClientLeft      =   -135
   ClientTop       =   1680
   ClientWidth     =   11910
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   ScaleHeight     =   5970
   ScaleWidth      =   11910
   ShowInTaskbar   =   0   'False
   Begin VB.Timer tim_frm_abi 
      Interval        =   500
      Left            =   150
      Top             =   390
   End
   Begin VB.Frame frm_err_abi 
      Height          =   2895
      Left            =   150
      TabIndex        =   23
      Top             =   3090
      Width           =   11535
      Begin VB.TextBox Desc2 
         Appearance      =   0  'Flat
         BackColor       =   &H8000000F&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000C0&
         Height          =   945
         Left            =   120
         MultiLine       =   -1  'True
         TabIndex        =   26
         Text            =   "abilitazioni.frx":0000
         Top             =   1920
         Width           =   11295
      End
      Begin VB.TextBox Title 
         Appearance      =   0  'Flat
         BackColor       =   &H8000000F&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000C0&
         Height          =   585
         Left            =   810
         MultiLine       =   -1  'True
         TabIndex        =   25
         Text            =   "abilitazioni.frx":000E
         Top             =   210
         Width           =   10575
      End
      Begin VB.TextBox Desc1 
         Appearance      =   0  'Flat
         BackColor       =   &H8000000F&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000C0&
         Height          =   945
         Left            =   120
         MultiLine       =   -1  'True
         TabIndex        =   24
         Text            =   "abilitazioni.frx":001E
         Top             =   960
         Width           =   11295
      End
      Begin VB.Image Img_war 
         Height          =   585
         Left            =   120
         Picture         =   "abilitazioni.frx":002C
         Top             =   210
         Width           =   630
      End
   End
   Begin VB.CheckBox abi_com_PU 
      BackColor       =   &H0000FF00&
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
      Height          =   360
      Left            =   5430
      MaskColor       =   &H00004000&
      Style           =   1  'Graphical
      TabIndex        =   21
      Top             =   2550
      Width           =   650
   End
   Begin VB.CheckBox fun_mot 
      BackColor       =   &H0000FF00&
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
      Height          =   360
      Index           =   33
      Left            =   10035
      MaskColor       =   &H00004000&
      Style           =   1  'Graphical
      TabIndex        =   11
      Top             =   2145
      Width           =   650
   End
   Begin VB.CheckBox fun_mot 
      BackColor       =   &H0000FF00&
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
      Height          =   360
      Index           =   23
      Left            =   7725
      MaskColor       =   &H00004000&
      Style           =   1  'Graphical
      TabIndex        =   10
      Top             =   2145
      Width           =   650
   End
   Begin VB.CheckBox fun_mot 
      BackColor       =   &H0000FF00&
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
      Height          =   360
      Index           =   13
      Left            =   5430
      MaskColor       =   &H00004000&
      Style           =   1  'Graphical
      TabIndex        =   9
      Top             =   2145
      Width           =   650
   End
   Begin VB.CheckBox fun_mot 
      BackColor       =   &H0000FF00&
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
      Height          =   360
      Index           =   32
      Left            =   10035
      MaskColor       =   &H00004000&
      Style           =   1  'Graphical
      TabIndex        =   8
      Top             =   1725
      Width           =   650
   End
   Begin VB.CheckBox fun_mot 
      BackColor       =   &H0000FF00&
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
      Height          =   360
      Index           =   22
      Left            =   7725
      MaskColor       =   &H00004000&
      Style           =   1  'Graphical
      TabIndex        =   7
      Top             =   1725
      Width           =   650
   End
   Begin VB.CheckBox fun_mot 
      BackColor       =   &H0000FF00&
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
      Height          =   360
      Index           =   12
      Left            =   5430
      MaskColor       =   &H00004000&
      Style           =   1  'Graphical
      TabIndex        =   6
      Top             =   1725
      Width           =   650
   End
   Begin VB.CheckBox fun_mot 
      BackColor       =   &H0000FF00&
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
      Height          =   360
      Index           =   31
      Left            =   10035
      MaskColor       =   &H00004000&
      Style           =   1  'Graphical
      TabIndex        =   5
      Top             =   1305
      Width           =   650
   End
   Begin VB.CheckBox fun_mot 
      BackColor       =   &H0000FF00&
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
      Height          =   360
      Index           =   21
      Left            =   7725
      MaskColor       =   &H00004000&
      Style           =   1  'Graphical
      TabIndex        =   4
      Top             =   1305
      Width           =   650
   End
   Begin VB.CheckBox fun_mot 
      BackColor       =   &H0000FF00&
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
      Height          =   360
      Index           =   11
      Left            =   5430
      MaskColor       =   &H00004000&
      Style           =   1  'Graphical
      TabIndex        =   3
      Top             =   1305
      Width           =   650
   End
   Begin VB.CheckBox liv 
      BackColor       =   &H0000FF00&
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
      Height          =   360
      Index           =   3
      Left            =   10035
      MaskColor       =   &H00004000&
      Style           =   1  'Graphical
      TabIndex        =   2
      Top             =   885
      Width           =   650
   End
   Begin VB.CheckBox liv 
      BackColor       =   &H0000FF00&
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
      Height          =   360
      Index           =   2
      Left            =   7725
      MaskColor       =   &H00004000&
      Style           =   1  'Graphical
      TabIndex        =   1
      Top             =   885
      Width           =   650
   End
   Begin VB.CheckBox liv 
      BackColor       =   &H0000FF00&
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
      Height          =   360
      Index           =   1
      Left            =   5430
      MaskColor       =   &H00004000&
      Style           =   1  'Graphical
      TabIndex        =   0
      Top             =   885
      Width           =   650
   End
   Begin VB.Line Line1 
      BorderColor     =   &H00000080&
      BorderWidth     =   2
      X1              =   765
      X2              =   10690
      Y1              =   2940
      Y2              =   2940
   End
   Begin VB.Label lab_abi_com_PU 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Abilitazione comunicazione Punto Unico"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   11.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   255
      Left            =   765
      TabIndex        =   22
      Top             =   2595
      Width           =   4395
   End
   Begin VB.Line Line7 
      BorderColor     =   &H00000080&
      BorderWidth     =   2
      X1              =   765
      X2              =   10690
      Y1              =   2535
      Y2              =   2535
   End
   Begin VB.Line Line6 
      BorderColor     =   &H00000080&
      BorderWidth     =   2
      X1              =   765
      X2              =   10690
      Y1              =   2115
      Y2              =   2115
   End
   Begin VB.Line Line5 
      BorderColor     =   &H00000080&
      BorderWidth     =   2
      X1              =   765
      X2              =   10690
      Y1              =   1695
      Y2              =   1695
   End
   Begin VB.Line Line4 
      BorderColor     =   &H00000080&
      BorderWidth     =   2
      X1              =   765
      X2              =   10690
      Y1              =   1275
      Y2              =   1275
   End
   Begin VB.Label lab_fun_mot 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Funzionamento motore M3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   11.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   255
      Index           =   2
      Left            =   765
      TabIndex        =   20
      Top             =   2190
      Width           =   3615
   End
   Begin VB.Label lab_fun_mot 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Funzionamento motore M2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   11.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   255
      Index           =   1
      Left            =   765
      TabIndex        =   19
      Top             =   1770
      Width           =   3615
   End
   Begin VB.Label lab_fun_mot 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Funzionamento motore M1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   11.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   255
      Index           =   0
      Left            =   765
      TabIndex        =   18
      Top             =   1350
      Width           =   3615
   End
   Begin VB.Label lab_abi_liv 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Abilitazione livello"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   11.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   255
      Left            =   765
      TabIndex        =   17
      Top             =   930
      Width           =   3615
   End
   Begin VB.Label Label1 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "DESCRIZIONE"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   375
      Left            =   765
      TabIndex        =   16
      Top             =   465
      Width           =   2325
   End
   Begin VB.Label lab_liv 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Livello 1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   330
      Index           =   0
      Left            =   5100
      TabIndex        =   15
      Top             =   465
      Width           =   1425
   End
   Begin VB.Label lab_liv 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Livello 2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   330
      Index           =   1
      Left            =   7395
      TabIndex        =   14
      Top             =   465
      Width           =   1425
   End
   Begin VB.Label lab_liv 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Livello 3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   330
      Index           =   2
      Left            =   9690
      TabIndex        =   13
      Top             =   465
      Width           =   1425
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "ABILITAZIONI"
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
      TabIndex        =   12
      Top             =   0
      Width           =   7965
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "abilitazioni.frx":13EE
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "abilitazioni"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
'Iciliouff 03-11-2003
Private WithEvents rmt_abilitazioni_plc As rmt
Attribute rmt_abilitazioni_plc.VB_VarHelpID = -1
Private WithEvents rmt_abilitazioni_macro As rmt
Attribute rmt_abilitazioni_macro.VB_VarHelpID = -1
Private WithEvents rmt_abilitazioni_sch As rmt
Attribute rmt_abilitazioni_sch.VB_VarHelpID = -1
Dim tas_pre As Boolean

Enum tip_caso
    inizio = 1
    annulla = 2
    cambio = 3
End Enum

Dim caso As tip_caso
Dim cli_eff As Boolean
Dim col_pre As Long
Dim cli_da_ese As Boolean

Sub chk_abi_mot()
'Arturo uff 05-03-03 Generata funzione che fa apparire un messaggio di errore nel caso ci siano dei motori disabilitati
Dim i As Integer                  'Livello
Dim m As Integer                  'motore
Dim Index As Integer              'indice del tasto
Dim vis_err_inv_mot As Boolean    'visualizza errore inverter motore

vis_err_inv_mot = False
For i = 1 To 3
  For m = 1 To 3
    Index = val(Format$(i, "0") + Format$(m, "0"))
    If fun_mot(Index).Enabled Then
      If fun_mot(Index).Value = 0 Then
        vis_err_inv_mot = True 'questo motore è presente sulla macchina ma disabilitato
        Exit For
      End If
    End If
  Next m
  If vis_err_inv_mot Then Exit For
Next i
         
Title.Text = msg("tit_war_inv_mot")
Desc1.Text = msg("des_war_inv_mot1")
Desc2.Text = msg("des_war_inv_mot2")
frm_err_abi.Visible = vis_err_inv_mot

End Sub

Sub check_dis_mot()
Dim i As Integer
Dim tro As Boolean

tro = False
For i = 1 To val(par_mac.car_tec.num_liv_tag)
  If par_abi_ute.num_mot_abi(i) = 0 And par_abi_ute.liv(i) Then tro = True
Next i
If tro Then
    messag_win msg("abi_un_mot"), True
Else
    If Not sta_bar.frm_mod.Visible And sta_bar.mes_all.Caption = msg("abi_un_mot") Then
        messag_win ""
    End If
End If
End Sub


Sub reset_motori(liv As Integer)
Dim m As Integer
Dim liv_mot As Integer

For m = 1 To par_mac.car_tec.num_mot_liv_tag
    liv_mot = val(CStr(liv) & CStr(m))
    caso = inizio
    If Not par_abi_ute.fun_mot_liv(liv).fun_mot_mot(m) Then
        fun_mot(liv_mot).Value = 0
        fun_mot(liv_mot).BackColor = LIGHT_Red
        fun_mot(liv_mot).Caption = "OFF"
    Else
        fun_mot(liv_mot).Value = 1
        fun_mot(liv_mot).BackColor = LIGHT_Green
        fun_mot(liv_mot).Caption = "ON"
    End If
Next m
caso = cambio
End Sub

Sub ini_CheckBox()
Dim i As Integer
Dim m As Integer
Dim liv_mot As Integer

For i = 1 To par_mac.car_tec.num_liv_tag
  lab_liv(i - 1).ForeColor = DARK_BLUE
  liv(i).Enabled = True
Next i

For i = par_mac.car_tec.num_liv_tag + 1 To 3
  lab_liv(i - 1).ForeColor = MEDIUM_GRAY
  If liv(i).Value = 1 Then liv(i).Value = 0
  liv(i).Caption = ""
  liv(i).BackColor = LIGHT_GRAY
  liv(i).Enabled = False
Next i

For i = 1 To 3
  For m = 1 To 3
      If m <= par_mac.car_tec.num_mot_liv_tag Then
        lab_fun_mot(m - 1).ForeColor = vbBlack
      Else
        lab_fun_mot(m - 1).ForeColor = MEDIUM_GRAY
      End If
      If m <= par_mac.car_tec.num_mot_liv_tag And i <= par_mac.car_tec.num_liv_tag Then
        liv_mot = val(CStr(i) & CStr(m))
        fun_mot(liv_mot).Enabled = True
      Else
        liv_mot = val(CStr(i) & CStr(m))
        If fun_mot(liv_mot).Value = 1 Then fun_mot(liv_mot) = 0
        fun_mot(liv_mot).Caption = ""
        fun_mot(liv_mot).BackColor = LIGHT_GRAY
        fun_mot(liv_mot).Enabled = False
      End If
  Next m
Next i

End Sub





Sub val_Check_Box_activate()
Dim i As Integer
Dim m As Integer
Dim liv_mot As Integer


If Not par_abi.abi_com_PU Then
  abi_com_PU.Value = 0
  abi_com_PU.BackColor = LIGHT_Red
  abi_com_PU.Caption = "OFF"
Else
  abi_com_PU.Value = 1
  abi_com_PU.BackColor = LIGHT_Green
  abi_com_PU.Caption = "ON"
End If
For i = 1 To par_mac.car_tec.num_liv_tag
  If Not par_abi.liv(i) Then
      liv(i).Value = 0
      liv(i).BackColor = LIGHT_Red
      liv(i).Caption = "OFF"
  Else
      liv(i).Value = 1
      liv(i).BackColor = LIGHT_Green
      liv(i).Caption = "ON"
  End If
  For m = 1 To par_mac.car_tec.num_mot_liv_tag
      liv_mot = val(CStr(i) & CStr(m))
      If Not par_abi.fun_mot_liv(i).fun_mot_mot(m) Then
          fun_mot(liv_mot).Value = 0
          fun_mot(liv_mot).BackColor = LIGHT_Red
          fun_mot(liv_mot).Caption = "OFF"
      Else
          fun_mot(liv_mot).Value = 1
          fun_mot(liv_mot).BackColor = LIGHT_Green
          fun_mot(liv_mot).Caption = "ON"
      End If
  Next m
Next i
End Sub


Function ver_ese_ope() As Boolean

ver_ese_ope = False
If ActiveControl.name = "abi_com_PU" Then ver_ese_ope = True: Exit Function
If ActiveControl Is Nothing Then Exit Function
If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Function
If Not sta_tag.com_OK Then messaggio msg("com_int_con_mac"), True: Exit Function
If Not sta_tag.MAC_F_STOP(1) Then messaggio msg("mac_non_fas_sto"), True, False: Exit Function
ver_ese_ope = True

End Function



Private Sub abi_com_PU_Click()

Select Case caso
  
'  Case annulla
'    If abi_com_pu.value = 1 Then
'      abi_com_pu.BackColor = LIGHT_Green
'      abi_com_pu.Caption = "ON"
'    Else
'      abi_com_pu.BackColor = LIGHT_Red
'      abi_com_pu.Caption = "OFF"
'    End If
'    col_pre = abi_com_pu.BackColor
'    abi_com_pu.BackColor = vbWhite
'    caso = cambio
    
  Case cambio
    If cli_da_ese Then
      If abi_com_PU.Value = 1 Then
        par_abi.abi_com_PU = True
        abi_com_PU.BackColor = LIGHT_Green
        abi_com_PU.Caption = "ON"
      Else
        par_abi.abi_com_PU = False
        abi_com_PU.BackColor = LIGHT_Red
        abi_com_PU.Caption = "OFF"
      End If
      cli_eff = True
      scr_par_abi
    End If
End Select
End Sub

Private Sub abi_com_PU_GotFocus()
col_pre = abi_com_PU.BackColor
abi_com_PU.BackColor = vbWhite
End Sub


Private Sub abi_com_PU_LostFocus()
If Not cli_eff Then
  abi_com_PU.BackColor = col_pre
Else
  cli_eff = False
End If
End Sub

Private Sub eve_key_F1(Shift As Integer)
'Select Case Shift
 ' Case 0
  'tas_pre = True
  'eve_key.chg set_par_mac
  'Case Else
'End Select
End Sub

Private Sub eve_key_F10(Shift As Integer)
Select Case Shift
  Case 0
  tas_pre = True
  eve_key.chg pag_pro
  Case Else
End Select
End Sub


Private Sub eve_key_F8(Shift As Integer)
Select Case Shift
  Case 0
  tas_pre = True
  eve_key.chg pag_lis_ese
  Case Else
End Select
End Sub


Private Sub eve_key_F9(Shift As Integer)
Select Case Shift
  Case 0
  tas_pre = True
  eve_key.chg pag_lis_ord
  Case Else
End Select
End Sub


Private Sub Form_Activate()

tim_frm_abi.Enabled = True  'Arturo uff 05-03-03
cli_eff = False
caso = inizio
ini_CheckBox
val_Check_Box_activate
par_abi_ute = par_abi
tas_pre = False
liv(1).SetFocus
caso = cambio

End Sub

Private Sub Form_Deactivate()
tas_pre = False
par_abi_ute = par_abi
tim_frm_abi.Enabled = False  'Arturo uff 05-03-03
End Sub


Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
On Error GoTo STAG_Gest_ERR
  

Select Case KeyCode
  Case vbKeySpace
    If Not ver_ese_ope Then
        If ActiveControl.Value = 1 Then
            ActiveControl.Value = 0
        Else
            ActiveControl.Value = 1
        End If
        cli_da_ese = False
        Exit Sub
    Else
        cli_da_ese = True
    End If

  Case vbKeyReturn
    If Not ver_ese_ope Then
        'If ActiveControl.value = 1 Then
        '    ActiveControl.value = 0
        'Else
        '    ActiveControl.value = 1
        'End If
        cli_da_ese = False
        Exit Sub
    Else
      cli_da_ese = True
    End If
    Select Case ActiveControl.name

      Case "fun_mot"
        If fun_mot(ActiveControl.Index).Value = 1 Then
            fun_mot(ActiveControl.Index).Value = 0
        Else
            fun_mot(ActiveControl.Index).Value = 1
        End If

      Case Else
        If ActiveControl.Value = 1 Then
            ActiveControl.Value = 0
        Else
            ActiveControl.Value = 1
        End If

    End Select
  Case vbKeyRight
    chk_shift 'controllo dello stato del tasto SHIFT
    KeyCode = 0
    SendKeys "{TAB}"
  Case vbKeyLeft
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

  Debug.Print "Inizio Load " & Me.name
  Set eve_key = New key
  'Iciliouff 03-11-2003
  Set rmt_abilitazioni_plc = New rmt
  rmt_abilitazioni_plc.set_plc_ute (1)
  rmt_abilitazioni_plc.name = "abilitazioni_plc"
  Set rmt_abilitazioni_sch = New rmt
  rmt_abilitazioni_sch.set_plc_ute (2)
  rmt_abilitazioni_sch.name = "abilitazioni_sch"
  Set rmt_abilitazioni_macro = New rmt
  rmt_abilitazioni_macro.set_plc_ute (3)
  rmt_abilitazioni_macro.name = "abilitazioni_macro"
  eve_key.create Me
  Carica Me
  Debug.Print "Fine Load " & Me.name
  Debug.Print ""
  
End Sub











Private Sub fun_mot_Click(Index As Integer)
Dim i As Integer
Dim m As Integer
Dim for_mac As Integer
Dim gra_car As tip_gra_car 'Arturo 05-11-02
Dim num_mot_liv As Integer
Dim mot As Integer

cli_da_ese = True

Select Case caso
    
  Case annulla
    If fun_mot(Index).Value = 1 Then
        fun_mot(Index).BackColor = LIGHT_Green
        fun_mot(Index).Caption = "ON"
    Else
        fun_mot(Index).BackColor = LIGHT_Red
        fun_mot(Index).Caption = "OFF"
    End If
    col_pre = fun_mot(Index).BackColor
    fun_mot(Index).BackColor = vbWhite
    caso = cambio
    
  Case cambio
    'Arturo uff 12-05-03 (2)
    If ver_ese_ope Then
      If cli_da_ese Then
          i = val(Left$(Index, 1))
          m = val(Right$(Index, 1))
          If Not par_abi.liv(i) Then
              messaggio msg("abi_il_liv") & " " & i, True, False
              par_abi_ute = par_abi
              caso = annulla
              If fun_mot(Index).Value = 1 Then
                  fun_mot(Index).Value = 0
              Else
                  fun_mot(Index).Value = 1
              End If
              scr_par_abi 'Arturo uff 26-07-02 salvo i par_abi sull'ini
          Else
              If fun_mot(Index).Value = 1 Then
                  par_abi_ute.fun_mot_liv(i).fun_mot_mot(m) = True
                  fun_mot(Index).BackColor = LIGHT_Green
                  fun_mot(Index).Caption = "ON"
              Else
                  par_abi_ute.fun_mot_liv(i).fun_mot_mot(m) = False
                  fun_mot(Index).BackColor = LIGHT_Red
                  fun_mot(Index).Caption = "OFF"
              End If
              par_abi_ute.num_mot_abi(i) = 0
              For mot = 1 To par_mac.car_tec.num_mot_liv_tag
                If par_abi_ute.fun_mot_liv(i).fun_mot_mot(mot) = True Then par_abi_ute.num_mot_abi(i) = par_abi_ute.num_mot_abi(i) + 1
              Next mot
              If par_abi_ute.num_mot_abi(i) = 0 Then
                  messaggio msg("imp_dis_all_mot"), True, False
                  par_abi_ute = par_abi
                  caso = annulla
                  fun_mot(Index).Value = 1
              Else
                  Select Case par_mac.car_tec.for_mac + 1
                    Case mm1800
                      for_mac = 1800
                    Case mm2500
                      for_mac = 2500
                    Case mm2800
                      for_mac = 2800
                  End Select
                  gra_car = get_gra_car(par_mac.car_tec.gra_car) 'Arturo uff 05-11-02
                  If Not dbf_vuo("Par_cfg") Then
                      messaggio msg("let_par_cfg"), False, False
                      num_mot_liv = val(par_mac.car_tec.num_mot_liv_tag)
  'Arturo uff 05-11-02
  '                    If Not trova_cfg_adatta(for_mac, val(par_mac.car_tec.num_mot_liv_tag), par_abi_ute.num_mot_abi(i), i) Then 'configurazione non trovata
                      If Not trova_cfg_adatta(for_mac, gra_car, val(par_mac.car_tec.num_mot_liv_tag), par_abi_ute.num_mot_abi(i), i) Then 'configurazione non trovata
                          messaggio msg("par_cfg_non_tro") & " " & i, True, False
                          par_abi_ute = par_abi
                          caso = annulla
                          If fun_mot(Index).Value = 1 Then
                              fun_mot(Index).Value = 0
                          Else
                              fun_mot(Index).Value = 1
                          End If
                      Else
                          'scrivo nella scheda di taglio
                          num_com_sch_tag = 1
                          
                          CASO_SND_SCH_TAG = CAMBIO_STATO_MACCHINA
                          scr_par_abi True
                          If rmt_abilitazioni_sch.esegui(tip_cmd.CONFIGURA, GetTickCount(), i, 0) Then
                            messaggio msg("ope_in_cor"), False
                          Else
                            messaggio msg("imp_ese_ope"), True
                          End If
                          cli_eff = True
'                          par_abi = par_abi_ute 'Arturo uff 26-07-02 salvo i par_abi_ute in par_abi per rendere definitiva la modifica
'                          snd_sch_tag i, CONFIGURA
'                          scr_par_abi 'Arturo uff 26-07-02 salvo i par_abi sull'ini
'                          cli_eff = True
                      End If
                  Else
                      cli_eff = True
                      'Iciliouff 03-11-2003
                      scr_par_abi True
                      If rmt_abilitazioni_macro.esegui(tip_ope_macro.PLC_PARAM_CFG_MAC_PARAM_CUR_TAG, GetTickCount(), i, 0) Then
                        messaggio msg("ope_incor"), False
                      Else
                        messaggio msg("imp_ese_ope"), True
                      End If
'                      par_abi = par_abi_ute
'                      snd_str_PLC PARAM_CFG_MAC
'  'Arturo uff 05-11-02
'  '                   If carica_new_par_cur_tag(i, for_mac, val(par_mac.car_tec.num_mot_liv_tag), par_abi.num_mot_abi(i)) Then
'                      If carica_new_par_cur_tag(i, for_mac, gra_car, val(par_mac.car_tec.num_mot_liv_tag), par_abi.num_mot_abi(i)) Then
'                          snd_str_PLC PARAM_CUR_TAG, , i
'                      Else
'                          messaggio msg("val_non_tro_cur_tag"), True, False
'                      End If
'                      scr_par_abi
                  End If
              End If
          End If
      End If
    Else
      caso = annulla
      If fun_mot(Index).Value = 1 Then
          fun_mot(Index).Value = 0
      Else
          fun_mot(Index).Value = 1
      End If
    End If
End Select

chk_abi_mot  'Arturo uff 05-03-03

End Sub

Private Sub fun_mot_GotFocus(Index As Integer)
col_pre = fun_mot(Index).BackColor
fun_mot(Index).BackColor = vbWhite
End Sub

Private Sub fun_mot_LostFocus(Index As Integer)
If Not cli_eff Then
  fun_mot(Index).BackColor = col_pre
Else
  cli_eff = False
End If
End Sub

Private Sub liv_Click(Index As Integer)
Dim m As Integer

Select Case caso
  
  Case annulla
    If liv(Index).Value = 1 Then
        liv(Index).BackColor = LIGHT_Green
        liv(Index).Caption = "ON"
    Else
        liv(Index).BackColor = LIGHT_Red
        liv(Index).Caption = "OFF"
    End If
    col_pre = liv(Index).BackColor
    liv(Index).BackColor = vbWhite
    caso = cambio
    
  Case cambio
    'Arturo uff 12-05-03 (2)
    If ver_ese_ope Then
      If cli_da_ese Then
        If sta_tag.MAC_IN_MANUALE(1) Then
          'Iciliouff 03-11-2003
          par_abi_ute = par_abi
          If ord_mac.Count = 0 Then
              If liv(Index).Value = 1 Then
                  par_abi_ute.liv(Index) = True
                  liv(Index).BackColor = LIGHT_Green
                  liv(Index).Caption = "ON"
              Else
                  par_abi_ute.liv(Index) = False
                  liv(Index).BackColor = LIGHT_Red
                  liv(Index).Caption = "OFF"
              End If
              If Not par_abi_ute.liv(Index) Then
                  For m = 1 To val(par_mac.car_tec.num_mot_liv_tag)
                      par_abi_ute.fun_mot_liv(Index).fun_mot_mot(m) = False
                  Next m
                  par_abi_ute.num_mot_abi(Index) = 0
                  reset_motori (Index)
              End If
              check_dis_mot
              cli_eff = True
              messaggio msg("msg_inv_par_mac"), False
              'Iciliouff 03-11-2003
'              snd_str_PLC PARAM_CFG_MAC
              scr_par_abi True
              If Not rmt_abilitazioni_plc.esegui(tip_ope_PLC.PARAM_CFG_MAC, GetTickCount(), Index, 0) Then
                messaggio msg("imp_ese_ope"), True
              End If
          Else
              messaggio msg("imp_ese_ope") + ". " + msg("lis_ord_non_vuo"), True, False
              caso = annulla
              If liv(Index).Value = 1 Then
                  liv(Index).Value = 0
              Else
                  liv(Index).Value = 1
              End If
          End If
        Else
          messaggio msg("imp_ese_ope") + ". " + msg("mac_in_auto"), True, False
          caso = annulla
          If liv(Index).Value = 1 Then
              liv(Index).Value = 0
          Else
              liv(Index).Value = 1
          End If
        End If
      End If
    Else
      caso = annulla
      If liv(Index).Value = 1 Then
          liv(Index).Value = 0
      Else
          liv(Index).Value = 1
      End If
    End If

End Select
End Sub







Private Sub liv_GotFocus(Index As Integer)
col_pre = liv(Index).BackColor
liv(Index).BackColor = vbWhite
End Sub

Private Sub liv_LostFocus(Index As Integer)
If Not cli_eff Then
  liv(Index).BackColor = col_pre
Else
  cli_eff = False
End If
End Sub


Private Sub rmt_abilitazioni_macro_retbad(err As String)
'Gestione risposta negativa di macro
  
  If rmt_abilitazioni_macro.ric = tip_ope_macro.PLC_PARAM_CFG_MAC_PARAM_CUR_TAG Then
    par_abi_ute = par_abi       'Rollback dei parametri antecedenti la modifica
    scr_par_abi
    Form_Activate
    If err <> "" Then messaggio err, True
    If rmt_abilitazioni_macro.err_cod = cod_err_macro.ROLL_BACK_PLC Then
      If rmt_abilitazioni_plc.esegui(tip_ope_PLC.PARAM_CFG_MAC, GetTickCount(), rmt_abilitazioni_macro.liv, 0, True) Then    'cerco di ripristinare i parametri precedenti verso il PLC
        log_str "Abilitazioni: ATTENZIONE - Roll Back configurazione PLC", MED_PRIORITY, log_cfg
        rmt_abilitazioni_plc.abi_vis_err = False
      End If
    End If
  End If
  rmt_abilitazioni_macro.resetta
  
End Sub

Private Sub rmt_abilitazioni_macro_retok(s As String)
'Gestione macro OK

  If rmt_abilitazioni_macro.ric = tip_ope_macro.PLC_PARAM_CFG_MAC_PARAM_CUR_TAG Then
    par_abi = par_abi_ute   'La scrittura dei parametri è andata a buon fine, aggiorno par_abi
    scr_par_abi
  End If
  If s <> "" Then messaggio s, False
  rmt_abilitazioni_macro.resetta
  
End Sub

Private Sub rmt_abilitazioni_plc_retbad(err As String)
'Gestione risposta negativa rmt
Dim ind As Integer

  If rmt_abilitazioni_plc.ric = tip_ope_PLC.PARAM_CFG_MAC Then
    par_abi_ute = par_abi
    scr_par_abi
    If Not rmt_abilitazioni_plc.dat Then
      'Evito di riaggiornare la pagina abilitazioni se viene eseguito un roll-back da macro
      ind = rmt_abilitazioni_plc.liv
      caso = annulla
      liv(ind).Value = IIf(liv(ind).Value = 1, 0, 1)
      liv_Click (ind)
    End If
  End If
  If err <> "" Then messaggio err, True
  rmt_abilitazioni_plc.resetta

End Sub

Private Sub rmt_abilitazioni_plc_retok(s As String)
'Gestione risposta positiva da rmt

  If rmt_abilitazioni_plc.ric = tip_ope_PLC.PARAM_CFG_MAC Then
    par_abi = par_abi_ute
  End If
  If s <> "" Then messaggio s, False
  rmt_abilitazioni_plc.resetta
  
End Sub

Private Sub rmt_abilitazioni_sch_retbad(err As String)
'Gestione risposta negativa da scheda di taglio

  If rmt_abilitazioni_sch.ric = tip_cmd.CONFIGURA Then
    par_abi_ute = par_abi
    scr_par_abi
    Form_Activate
  End If
  If err <> "" Then messaggio err, True
  rmt_abilitazioni_sch.resetta
  
End Sub

Private Sub rmt_abilitazioni_sch_retok(s As String)
'Gestione risposta positiva

  If rmt_abilitazioni_sch.ric = tip_cmd.CONFIGURA Then
    par_abi = par_abi_ute
  End If
  If s <> "" Then messaggio s, False
  rmt_abilitazioni_sch.resetta
  
End Sub

Private Sub tim_frm_abi_Timer()
'Arturo uff 05-03-03 aggiunta per far lampeggiare il testo
If frm_err_abi.Visible Then
  If Title.ForeColor = &HC00000 Then
    Title.ForeColor = &HC0&
    Desc1.ForeColor = &HC0&
    Desc2.ForeColor = &HC0&
  Else
    Title.ForeColor = &HC00000
    Desc1.ForeColor = &HC00000
    Desc2.ForeColor = &HC00000
  End If
End If

End Sub


