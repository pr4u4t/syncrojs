VERSION 5.00
Begin VB.Form leg_cfg 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   8745
   ClientLeft      =   1260
   ClientTop       =   3165
   ClientWidth     =   8910
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   8745
   ScaleWidth      =   8910
   ShowInTaskbar   =   0   'False
   Begin VB.CommandButton Command1 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      Caption         =   "ESC"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   3218
      TabIndex        =   56
      TabStop         =   0   'False
      Top             =   8115
      Width           =   1065
   End
   Begin VB.TextBox txt_idle 
      Height          =   375
      Left            =   825
      TabIndex        =   48
      Text            =   "Text1"
      Top             =   -5000
      Width           =   1125
   End
   Begin VB.Frame Frame1 
      BackColor       =   &H00C0C0FF&
      BorderStyle     =   0  'None
      Height          =   2460
      Left            =   120
      TabIndex        =   34
      Top             =   1050
      Width           =   8655
      Begin VB.Frame Frame2 
         BackColor       =   &H008080FF&
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
         Height          =   510
         Left            =   480
         TabIndex        =   43
         Top             =   1800
         Width           =   7560
         Begin VB.Label l_gra_car 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0FFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   15.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000008&
            Height          =   405
            Left            =   6600
            TabIndex        =   71
            Top             =   45
            Width           =   840
         End
         Begin VB.Label l_for_mac 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0FFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   15.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000008&
            Height          =   405
            Left            =   120
            TabIndex        =   46
            Top             =   45
            Width           =   840
         End
         Begin VB.Label l_num_mot_liv 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0FFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   15.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000008&
            Height          =   405
            Left            =   2520
            TabIndex        =   45
            Top             =   45
            Width           =   360
         End
         Begin VB.Label l_num_mot_abi 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0FFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   15.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000008&
            Height          =   405
            Left            =   4680
            TabIndex        =   44
            Top             =   45
            Width           =   360
         End
      End
      Begin VB.Label Label21 
         Alignment       =   2  'Center
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Grammatura max  cartone"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   11.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   585
         Left            =   6480
         TabIndex        =   72
         Top             =   720
         Width           =   1950
      End
      Begin VB.Label lab_gra_car 
         Alignment       =   2  'Center
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   1  'Fixed Single
         DataField       =   "FOR_MAC"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   15.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   7080
         TabIndex        =   70
         Top             =   1320
         Width           =   840
      End
      Begin VB.Label l_liv 
         Alignment       =   2  'Center
         BackColor       =   &H00FF0000&
         Caption         =   "3"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FFFFFF&
         Height          =   240
         Index           =   1
         Left            =   3240
         TabIndex        =   51
         Top             =   480
         Width           =   165
      End
      Begin VB.Label Label13 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Attuale stato macchina per il livello:"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   225
         Left            =   120
         TabIndex        =   49
         Top             =   480
         Width           =   3015
      End
      Begin VB.Label lab_cfg 
         BackColor       =   &H00FFFFC0&
         BorderStyle     =   1  'Fixed Single
         DataField       =   "DESCR"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   11.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   330
         Left            =   3555
         TabIndex        =   47
         Top             =   135
         Width           =   4725
      End
      Begin VB.Label Label1 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Configurazione livello"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Left            =   360
         TabIndex        =   42
         Top             =   120
         Width           =   3135
      End
      Begin VB.Label Label4 
         Alignment       =   2  'Center
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Num. mot. abilitati per livello"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   11.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   570
         Left            =   4440
         TabIndex        =   40
         Top             =   720
         Width           =   2010
      End
      Begin VB.Label Label3 
         Alignment       =   2  'Center
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Numero motori per livello"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   11.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   600
         Left            =   2280
         TabIndex        =   39
         Top             =   720
         Width           =   2010
      End
      Begin VB.Label Label2 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Formato macchina"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   11.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   465
         Left            =   120
         TabIndex        =   38
         Top             =   720
         Width           =   1950
      End
      Begin VB.Label lab_num_mot_abi 
         Alignment       =   2  'Center
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   1  'Fixed Single
         DataField       =   "N_MOT_AB"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   15.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   5160
         TabIndex        =   37
         Top             =   1320
         Width           =   360
      End
      Begin VB.Label lab_num_mot_liv 
         Alignment       =   2  'Center
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   1  'Fixed Single
         DataField       =   "N_MOT_L"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   15.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   3000
         TabIndex        =   36
         Top             =   1320
         Width           =   360
      End
      Begin VB.Label lab_for_mac 
         Alignment       =   2  'Center
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   1  'Fixed Single
         DataField       =   "FOR_MAC"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   15.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   600
         TabIndex        =   35
         Top             =   1320
         Width           =   840
      End
   End
   Begin VB.CommandButton tas_F1 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      Caption         =   "F11"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   4508
      TabIndex        =   1
      TabStop         =   0   'False
      Top             =   8115
      Width           =   1065
   End
   Begin VB.Label Label7 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Giri massimi motore lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   1005
      TabIndex        =   76
      Top             =   7665
      Width           =   2325
   End
   Begin VB.Label lab_max_gir 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KI_NOR1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   0
      Left            =   4230
      TabIndex        =   75
      Top             =   7635
      Width           =   870
   End
   Begin VB.Label lab_max_gir 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KI_NOR2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   1
      Left            =   5475
      TabIndex        =   74
      Top             =   7635
      Width           =   870
   End
   Begin VB.Label lab_max_gir 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KI_NOR3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   2
      Left            =   6720
      TabIndex        =   73
      Top             =   7635
      Width           =   870
   End
   Begin VB.Label lab_lim_lav_kv 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KI_NOR3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   2
      Left            =   6720
      TabIndex        =   69
      Top             =   7290
      Width           =   870
   End
   Begin VB.Label lab_lim_lav_kv 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KI_NOR2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   1
      Left            =   5475
      TabIndex        =   68
      Top             =   7290
      Width           =   870
   End
   Begin VB.Label lab_lim_lav_kv 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KI_NOR1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   0
      Left            =   4230
      TabIndex        =   67
      Top             =   7290
      Width           =   870
   End
   Begin VB.Label Label18 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Limite di lavoro del Kv"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   1005
      TabIndex        =   66
      Top             =   7350
      Width           =   2325
   End
   Begin VB.Label lab_kv_nor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KI_NOR3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   2
      Left            =   6720
      TabIndex        =   65
      Top             =   6945
      Width           =   870
   End
   Begin VB.Label lab_kv_nor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KI_NOR2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   1
      Left            =   5475
      TabIndex        =   64
      Top             =   6945
      Width           =   870
   End
   Begin VB.Label lab_kv_nor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KI_NOR1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   0
      Left            =   4230
      TabIndex        =   63
      Top             =   6945
      Width           =   870
   End
   Begin VB.Label Label17 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Kv normalizzato"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   1005
      TabIndex        =   62
      Top             =   7005
      Width           =   2325
   End
   Begin VB.Label lab_fil_kp 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KI_NOR3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   2
      Left            =   6720
      TabIndex        =   61
      Top             =   6600
      Width           =   870
   End
   Begin VB.Label lab_fil_kp 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KI_NOR2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   1
      Left            =   5475
      TabIndex        =   60
      Top             =   6600
      Width           =   870
   End
   Begin VB.Label lab_fil_kp 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KI_NOR1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   0
      Left            =   4230
      TabIndex        =   59
      Top             =   6600
      Width           =   870
   End
   Begin VB.Label Label16 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Filtro Kp"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   1005
      TabIndex        =   58
      Top             =   6660
      Width           =   2325
   End
   Begin VB.Image Image1 
      Height          =   345
      Left            =   6570
      Picture         =   "leg_cfg.frx":0000
      Top             =   780
      Width           =   1185
   End
   Begin VB.Label Label15 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
      Caption         =   "Annulla"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   225
      Left            =   3480
      TabIndex        =   57
      Top             =   8490
      Width           =   645
   End
   Begin VB.Label lab_att 
      Alignment       =   2  'Center
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "NON ATTIVA"
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
      Height          =   210
      Index           =   2
      Left            =   6585
      TabIndex        =   55
      Top             =   3675
      Width           =   1170
   End
   Begin VB.Label lab_att 
      Alignment       =   2  'Center
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "NON ATTIVA"
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
      Height          =   210
      Index           =   1
      Left            =   5340
      TabIndex        =   54
      Top             =   3675
      Width           =   1170
   End
   Begin VB.Label lab_att 
      Alignment       =   2  'Center
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "NON ATTIVA"
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
      Height          =   210
      Index           =   0
      Left            =   4095
      TabIndex        =   53
      Top             =   3675
      Width           =   1170
   End
   Begin VB.Label l_liv 
      Alignment       =   2  'Center
      BackColor       =   &H00FF0000&
      Caption         =   "3"
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
      Height          =   360
      Index           =   0
      Left            =   5535
      TabIndex        =   52
      Top             =   15
      Width           =   360
   End
   Begin VB.Label Label14 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Utilizzare i tasti freccia per visualizzare le diverse configurazioni"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   210
      Left            =   660
      TabIndex        =   50
      Top             =   810
      Width           =   5760
   End
   Begin VB.Label Label6 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
      Caption         =   $"leg_cfg.frx":15D2
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00404080&
      Height          =   435
      Left            =   150
      TabIndex        =   41
      Top             =   360
      Width           =   8670
      WordWrap        =   -1  'True
   End
   Begin VB.Label Label5 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Velocità massima taglierina"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   1005
      TabIndex        =   33
      Top             =   4245
      Width           =   2700
   End
   Begin VB.Label Label39 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Kp normalizzato"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   1005
      TabIndex        =   32
      Top             =   5970
      Width           =   2325
   End
   Begin VB.Label Label38 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Ki normalizzato"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   1005
      TabIndex        =   31
      Top             =   6315
      Width           =   2325
   End
   Begin VB.Label Label8 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Tempo accelerazione tagli lunghi"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   1005
      TabIndex        =   30
      Top             =   4590
      Width           =   2970
   End
   Begin VB.Label Label9 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Tempo decelerazione tagli corti"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   1005
      TabIndex        =   29
      Top             =   5625
      Width           =   2985
   End
   Begin VB.Label Label10 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Tempo accelerazione tagli corti"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   1005
      TabIndex        =   28
      Top             =   5280
      Width           =   2985
   End
   Begin VB.Label Label29 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Tempo decelerazione tagli lunghi"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   1005
      TabIndex        =   27
      Top             =   4935
      Width           =   2985
   End
   Begin VB.Label lab_liv 
      Alignment       =   2  'Center
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
      ForeColor       =   &H00000080&
      Height          =   270
      Index           =   0
      Left            =   4155
      TabIndex        =   26
      Top             =   3885
      Width           =   1095
   End
   Begin VB.Label lab_liv 
      Alignment       =   2  'Center
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
      ForeColor       =   &H00000080&
      Height          =   270
      Index           =   1
      Left            =   5400
      TabIndex        =   25
      Top             =   3885
      Width           =   1080
   End
   Begin VB.Label lab_liv 
      Alignment       =   2  'Center
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
      ForeColor       =   &H00000080&
      Height          =   270
      Index           =   2
      Left            =   6645
      TabIndex        =   24
      Top             =   3885
      Width           =   1080
   End
   Begin VB.Label lab_vel_mas_tag 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "VEL_MAX1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   0
      Left            =   4230
      TabIndex        =   23
      Top             =   4185
      Width           =   870
   End
   Begin VB.Label lab_vel_mas_tag 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "VEL_MAX2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   1
      Left            =   5475
      TabIndex        =   22
      Top             =   4185
      Width           =   870
   End
   Begin VB.Label lab_vel_mas_tag 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "VEL_MAX3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   2
      Left            =   6720
      TabIndex        =   21
      Top             =   4185
      Width           =   870
   End
   Begin VB.Label lab_tem_acc_tag_lun 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "TEM_ATL1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   0
      Left            =   4230
      TabIndex        =   20
      Top             =   4530
      Width           =   870
   End
   Begin VB.Label lab_tem_acc_tag_lun 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "TEM_ATL2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   1
      Left            =   5475
      TabIndex        =   19
      Top             =   4530
      Width           =   870
   End
   Begin VB.Label lab_tem_acc_tag_lun 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "TEM_ATL3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   2
      Left            =   6720
      TabIndex        =   18
      Top             =   4530
      Width           =   870
   End
   Begin VB.Label lab_tem_dec_tag_lun 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "TEM_DTL1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   0
      Left            =   4230
      TabIndex        =   17
      Top             =   4875
      Width           =   870
   End
   Begin VB.Label lab_tem_dec_tag_lun 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "TEM_DTL2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   1
      Left            =   5475
      TabIndex        =   16
      Top             =   4875
      Width           =   870
   End
   Begin VB.Label lab_tem_dec_tag_lun 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "TEM_DTL3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   2
      Left            =   6720
      TabIndex        =   15
      Top             =   4875
      Width           =   870
   End
   Begin VB.Label lab_tem_acc_tag_cor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "TEM_ATC1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   0
      Left            =   4230
      TabIndex        =   14
      Top             =   5220
      Width           =   870
   End
   Begin VB.Label lab_tem_acc_tag_cor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "TEM_ATC2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   1
      Left            =   5475
      TabIndex        =   13
      Top             =   5220
      Width           =   870
   End
   Begin VB.Label lab_tem_acc_tag_cor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "TEM_ATC3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   2
      Left            =   6720
      TabIndex        =   12
      Top             =   5220
      Width           =   870
   End
   Begin VB.Label lab_tem_dec_tag_cor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "TEM_DTC1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   0
      Left            =   4230
      TabIndex        =   11
      Top             =   5565
      Width           =   870
   End
   Begin VB.Label lab_tem_dec_tag_cor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "TEM_DTC2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   1
      Left            =   5475
      TabIndex        =   10
      Top             =   5565
      Width           =   870
   End
   Begin VB.Label lab_tem_dec_tag_cor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "TEM_DTC3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   2
      Left            =   6720
      TabIndex        =   9
      Top             =   5565
      Width           =   870
   End
   Begin VB.Label lab_kp_nor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KP_NOR1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   0
      Left            =   4230
      TabIndex        =   8
      Top             =   5910
      Width           =   870
   End
   Begin VB.Label lab_kp_nor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KP_NOR2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   1
      Left            =   5475
      TabIndex        =   7
      Top             =   5910
      Width           =   870
   End
   Begin VB.Label lab_kp_nor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KP_NOR3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   2
      Left            =   6720
      TabIndex        =   6
      Top             =   5910
      Width           =   870
   End
   Begin VB.Label lab_ki_nor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KI_NOR1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   0
      Left            =   4230
      TabIndex        =   5
      Top             =   6255
      Width           =   870
   End
   Begin VB.Label lab_ki_nor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KI_NOR2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   1
      Left            =   5475
      TabIndex        =   4
      Top             =   6255
      Width           =   870
   End
   Begin VB.Label lab_ki_nor 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   1  'Fixed Single
      DataField       =   "KI_NOR3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Index           =   2
      Left            =   6720
      TabIndex        =   3
      Top             =   6255
      Width           =   870
   End
   Begin VB.Label txt_F1 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
      Caption         =   "Conferma"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   225
      Left            =   4650
      TabIndex        =   2
      Top             =   8490
      Width           =   855
   End
   Begin VB.Label Pag_title 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Carica configurazione per il livello"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   15.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00800000&
      Height          =   375
      Left            =   105
      TabIndex        =   0
      Top             =   15
      Width           =   5385
   End
End
Attribute VB_Name = "leg_cfg"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
'Iciliouff 03-11-2003
Private WithEvents rmt_leg_cfg_sch As rmt
Attribute rmt_leg_cfg_sch.VB_VarHelpID = -1

Dim dbf_vuo As Boolean
Dim for_mac As Integer
Dim gra_car As Integer
Dim num_mot_liv As Integer
Dim num_mot_abi As Integer
Dim rs As Recordset
Sub agg_lab()
Dim i As Integer
Dim col As Long

lab_cfg.Caption = rs("DESCR")
lab_for_mac.Caption = rs("FOR_MAC")
lab_gra_car.Caption = rs("GRA_CAR") 'Arturo uff 05-11-02
lab_num_mot_liv.Caption = rs("N_MOT_L")
lab_num_mot_abi.Caption = rs("N_MOT_AB")
For i = 1 To par_mac.car_tec.num_liv_tag

  lab_att(i - 1).Caption = IIf(rs("LIVELLO" & i) = 1, "ATTIVA", "NON ATTIVA")
  lab_att(i - 1).ForeColor = IIf(rs("LIVELLO" & i) = 1, vbRed, vbBlack)
  col = IIf(rs("LIVELLO" & i) = 1, LIGHT_Red, vbWhite)
  lab_vel_mas_tag(i - 1).BackColor = col
  lab_tem_acc_tag_lun(i - 1).BackColor = col
  lab_tem_dec_tag_lun(i - 1).BackColor = col
  lab_tem_acc_tag_cor(i - 1).BackColor = col
  lab_tem_dec_tag_cor(i - 1).BackColor = col
  lab_kp_nor(i - 1).BackColor = col
  lab_ki_nor(i - 1).BackColor = col
  lab_fil_kp(i - 1).BackColor = col
  lab_kv_nor(i - 1).BackColor = col
  lab_lim_lav_kv(i - 1).BackColor = col
  lab_max_gir(i - 1).BackColor = col  'Arturo uff 21-02-03
  
  lab_vel_mas_tag(i - 1).Caption = IIf(IsNull(rs("VEL_MAX" & i)), "", rs("VEL_MAX" & i))
  lab_tem_acc_tag_lun(i - 1).Caption = IIf(IsNull(rs("TEM_ATL" & i)), "", rs("TEM_ATL" & i))
  lab_tem_dec_tag_lun(i - 1).Caption = IIf(IsNull(rs("TEM_DTL" & i)), "", rs("TEM_DTL" & i))
  lab_tem_acc_tag_cor(i - 1).Caption = IIf(IsNull(rs("TEM_ATC" & i)), "", rs("TEM_ATC" & i))
  lab_tem_dec_tag_cor(i - 1).Caption = IIf(IsNull(rs("TEM_DTC" & i)), "", rs("TEM_DTC" & i))
  lab_kp_nor(i - 1).Caption = IIf(IsNull(rs("KP_NOR" & i)), "", rs("KP_NOR" & i))
  lab_ki_nor(i - 1).Caption = IIf(IsNull(rs("KI_NOR" & i)), "", rs("KI_NOR" & i))
  lab_fil_kp(i - 1).Caption = IIf(IsNull(rs("FIL_KP" & i)), "", rs("FIL_KP" & i))
  lab_kv_nor(i - 1).Caption = IIf(IsNull(rs("KV_NOR" & i)), "", rs("KV_NOR" & i))
  lab_lim_lav_kv(i - 1).Caption = IIf(IsNull(rs("LIM_KV" & i)), "", rs("LIM_KV" & i))
  lab_max_gir(i - 1).Caption = IIf(IsNull(rs("MAX_GIR" & i)), "", rs("MAX_GIR" & i))   'Arturo uff 21-02-03
Next i

End Sub


Sub ini_dbf()

Select Case par_mac.car_tec.for_mac + 1
  Case mm1800
    for_mac = 1800
  Case mm2500
    for_mac = 2500
  Case mm2800
    for_mac = 2800
End Select
gra_car = get_gra_car(par_mac.car_tec.gra_car)  'Arturo uff 05-11-02
num_mot_liv = val(par_mac.car_tec.num_mot_liv_tag)
num_mot_abi = par_abi.num_mot_abi(Livello)
lab_cfg.Caption = ""
l_for_mac.Caption = for_mac
l_gra_car.Caption = gra_car   'Arturo uff 05-11-02
l_num_mot_liv.Caption = num_mot_liv
l_num_mot_abi.Caption = num_mot_abi

dbf_vuo = False

Set rs = db.OpenRecordset("select * from Par_cfg order by FOR_MAC, GRA_CAR, N_MOT_L, N_MOT_AB, DESCR;", dbOpenSnapshot)
If rs.RecordCount > 0 Then
    rs.MoveFirst
    If Trim$(par_mac.config_sch_tag_att(Livello).descr) = "" Then
        rs.MoveFirst 'cerco la prima di DEFAULT con quel FOR_MAC, N_MOT_L, N_MOT_AB
        Do While Not rs.EOF
            'Arturo uff 05-11-02
            If "DEFAULT" = rs("DESCR") And for_mac = rs("FOR_MAC") And gra_car = rs("GRA_CAR") _
            And num_mot_liv = rs("N_MOT_L") And num_mot_abi = rs("N_MOT_AB") Then
                Exit Do
            End If
            rs.MoveNext
        Loop
    Else
        Do While Not rs.EOF
          If Trim$(par_mac.config_sch_tag_att(Livello).descr) = rs("DESCR") And rs("LIVELLO" & Livello) = "1" Then  'And for_mac = rs("FOR_MAC") _
          And num_mot_liv = rs("N_MOT_L") And num_mot_abi = rs("N_MOT_AB") Then
              Exit Do
          End If
          rs.MoveNext
        Loop
    End If
    If rs.EOF Then rs.MoveFirst 'configurazione di DEFAULT o attiva non trovata => visualizzo la prima
    agg_lab
Else
    lab_cfg.Caption = msg("non_cfg_leg")
    dbf_vuo = True
End If


End Sub


Sub ini_lab()
Dim i As Integer

L_LIV(0) = Livello
L_LIV(1) = Livello
For i = 1 To par_mac.car_tec.num_liv_tag
    lab_vel_mas_tag(i - 1).Caption = ""
    lab_tem_acc_tag_lun(i - 1).Caption = ""
    lab_tem_acc_tag_cor(i - 1).Caption = ""
    lab_tem_dec_tag_lun(i - 1).Caption = ""
    lab_tem_dec_tag_cor(i - 1).Caption = ""
    lab_ki_nor(i - 1).Caption = ""
    lab_kp_nor(i - 1).Caption = ""
    lab_fil_kp(i - 1).Caption = ""
    lab_kv_nor(i - 1).Caption = ""
    lab_lim_lav_kv(i - 1).Caption = ""
    lab_max_gir(i - 1).Caption = ""           'Arturo uff 21-02-03
    lab_vel_mas_tag(i - 1).BackColor = vbWhite
    lab_tem_acc_tag_lun(i - 1).BackColor = vbWhite
    lab_tem_acc_tag_cor(i - 1).BackColor = vbWhite
    lab_tem_dec_tag_lun(i - 1).BackColor = vbWhite
    lab_tem_dec_tag_cor(i - 1).BackColor = vbWhite
    lab_ki_nor(i - 1).BackColor = vbWhite
    lab_kp_nor(i - 1).BackColor = vbWhite
    lab_fil_kp(i - 1).BackColor = vbWhite
    lab_kv_nor(i - 1).BackColor = vbWhite
    lab_lim_lav_kv(i - 1).BackColor = vbWhite
    lab_max_gir(i - 1).BackColor = vbWhite    'Arturo uff 21-02-03
    lab_liv(i - 1).BackColor = vbWhite
Next i
For i = par_mac.car_tec.num_liv_tag + 1 To 3
    lab_vel_mas_tag(i - 1).Caption = ""
    lab_tem_acc_tag_lun(i - 1).Caption = ""
    lab_tem_acc_tag_cor(i - 1).Caption = ""
    lab_tem_dec_tag_lun(i - 1).Caption = ""
    lab_tem_dec_tag_cor(i - 1).Caption = ""
    lab_ki_nor(i - 1).Caption = ""
    lab_kp_nor(i - 1).Caption = ""
    lab_fil_kp(i - 1).Caption = ""
    lab_kv_nor(i - 1).Caption = ""
    lab_lim_lav_kv(i - 1).Caption = ""
    lab_max_gir(i - 1).Caption = ""           'Arturo uff 21-02-03
    lab_vel_mas_tag(i - 1).BackColor = LIGHT_GRAY
    lab_tem_acc_tag_lun(i - 1).BackColor = LIGHT_GRAY
    lab_tem_acc_tag_cor(i - 1).BackColor = LIGHT_GRAY
    lab_tem_dec_tag_lun(i - 1).BackColor = LIGHT_GRAY
    lab_tem_dec_tag_cor(i - 1).BackColor = LIGHT_GRAY
    lab_ki_nor(i - 1).BackColor = LIGHT_GRAY
    lab_kp_nor(i - 1).BackColor = LIGHT_GRAY
    lab_fil_kp(i - 1).BackColor = LIGHT_GRAY
    lab_kv_nor(i - 1).BackColor = LIGHT_GRAY
    lab_lim_lav_kv(i - 1).BackColor = LIGHT_GRAY
    lab_max_gir(i - 1).BackColor = LIGHT_GRAY 'Arturo uff 21-02-03
    lab_liv(i - 1).BackColor = LIGHT_GRAY
Next i
End Sub

Sub scr_par_ute(liv As Integer)
par_mac_ute.config_sch_tag_att(liv).descr = Trim$(lab_cfg.Caption)
par_mac_ute.config_sch_tag_att(liv).num_mot_liv = lab_num_mot_liv
par_mac_ute.par_cfg_att(liv).vel_max_tag = Trim$(lab_vel_mas_tag(liv - 1).Caption)
par_mac_ute.par_cfg_att(liv).tem_acc_tgl_cor = Trim$(lab_tem_acc_tag_cor(liv - 1).Caption)
par_mac_ute.par_cfg_att(liv).tem_acc_tgl_lun = Trim$(lab_tem_acc_tag_lun(liv - 1).Caption)
par_mac_ute.par_cfg_att(liv).tem_dec_tgl_cor = Trim$(lab_tem_dec_tag_cor(liv - 1).Caption)
par_mac_ute.par_cfg_att(liv).tem_dec_tgl_lun = Trim$(lab_tem_dec_tag_lun(liv - 1).Caption)
par_mac_ute.par_cfg_att(liv).Ki_nor = Trim$(lab_ki_nor(liv - 1).Caption)
par_mac_ute.par_cfg_att(liv).Kp_nor = Trim$(lab_kp_nor(liv - 1).Caption)
par_mac_ute.par_cfg_att(liv).fil_kp = Trim$(lab_fil_kp(liv - 1).Caption)
par_mac_ute.par_cfg_att(liv).Kv_nor = Trim$(lab_kv_nor(liv - 1).Caption)
par_mac_ute.par_cfg_att(liv).lim_lav_Kv = Trim$(lab_lim_lav_kv(liv - 1).Caption)
par_mac_ute.par_cfg_att(liv).gir_max_mot_lam = Trim$(lab_max_gir(liv - 1).Caption) 'Arturo uff 21-02-03
End Sub

Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
    rs.Close
    eve_key.chg set_par_sch_tag3
  Case Else
End Select
End Sub

Private Sub eve_key_F11(Shift As Integer)
Dim ret As cod_ret

Select Case Shift
  Case 0
    If Not dbf_vuo Then
      If Screen.ActiveForm.name = Me.name Then eve_key.chg set_par_sch_tag3
      'Arturo uff 05-11-02
      If lab_for_mac <> for_mac Or lab_gra_car <> gra_car Or lab_num_mot_liv <> num_mot_liv Or lab_num_mot_abi <> num_mot_abi Then
        messaggio msg("imp_car_cfg"), True, False
      Else
        If Trim$(par_mac.config_sch_tag_att(val(L_LIV(0).Caption)).descr) <> Trim$(lab_cfg.Caption) Then
          messaggio msg("ope_in_cor"), False, False
          scr_par_ute (val(L_LIV(0).Caption))
          'scrivo nella scheda di taglio
          num_com_sch_tag = 1
          CASO_SND_SCH_TAG = CARICA_NEW_CFG
          'Iciliouff 03-11-2003
          If rmt_leg_cfg_sch.esegui(tip_cmd.CONFIGURA, GetTickCount(), val(L_LIV(0).Caption), 0) Then
            messaggio msg("inv_par_sch_tag"), False
          Else
            messaggio msg("imp_ese_ope"), True
          End If
'          snd_sch_tag val(l_liv(0).Caption), CONFIGURA
        End If
      End If
    rs.Close
    End If
  Case Else
End Select
End Sub

Private Sub Form_Activate()

ini_lab
ini_dbf
txt_idle.Enabled = True 'Arturo uff 07-11-02
txt_idle.SetFocus

End Sub

Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
Select Case KeyCode
  Case 39
    If rs.RecordCount > 0 Then
      If rs.AbsolutePosition < rs.RecordCount - 1 Then 'arturo uff 03-03-03
        rs.MoveNext
        If rs.EOF Then rs.MovePrevious
        agg_lab
      End If
    End If
  Case 37
    If rs.RecordCount > 0 Then
      If rs.AbsolutePosition > 2 Then  'arturo uff 03-03-03
        rs.MovePrevious
        If rs.BOF Then rs.MoveNext
        agg_lab
      End If
    End If
  Case Else
    eve_key.frm_KeyDown KeyCode, Shift
End Select
End Sub


Private Sub Form_Load()
Debug.Print "Inizio Load " & Me.name
Set eve_key = New key
'Iciliouff 03-11-2003
Set rmt_leg_cfg_sch = New rmt
rmt_leg_cfg_sch.set_plc_ute (2)
rmt_leg_cfg_sch.name = "leg_cfg_sch"
eve_key.create Me
Ridim Me
Center Me
dimensiona Me
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub


Private Sub rmt_leg_cfg_sch_retbad(err As String)
'Gestione risposta negativa da scheda di taglio
  
  If err <> "" Then messaggio err, True
  rmt_leg_cfg_sch.resetta
  
End Sub

Private Sub rmt_leg_cfg_sch_retok(s As String)
'Gestione risposta positiva da scheda di taglio
  
  If s <> "" Then messaggio s, False
  rmt_leg_cfg_sch.resetta

End Sub


