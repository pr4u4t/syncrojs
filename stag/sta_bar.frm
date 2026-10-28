VERSION 5.00
Object = "{648A5603-2C6E-101B-82B6-000000000014}#1.1#0"; "MSCOMM32.OCX"
Object = "{248DD890-BB45-11CF-9ABC-0080C7E7B78D}#1.0#0"; "MSWINSCK.OCX"
Begin VB.Form sta_bar 
   Appearance      =   0  'Flat
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   0  'None
   ClientHeight    =   1815
   ClientLeft      =   11295
   ClientTop       =   1080
   ClientWidth     =   12000
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2246.434
   ScaleMode       =   0  'User
   ScaleWidth      =   12120
   ShowInTaskbar   =   0   'False
   Begin VB.Timer tim_fins 
      Enabled         =   0   'False
      Left            =   7800
      Top             =   0
   End
   Begin MSWinsockLib.Winsock sck_Fins 
      Left            =   7320
      Top             =   0
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
   End
   Begin VB.Timer Tim_com_spde_lan 
      Enabled         =   0   'False
      Interval        =   1000
      Left            =   0
      Top             =   -45
   End
   Begin VB.Frame frm_com 
      BackColor       =   &H00008000&
      BorderStyle     =   0  'None
      Enabled         =   0   'False
      Height          =   750
      Left            =   -135
      TabIndex        =   73
      Top             =   1140
      Width           =   1911
      Begin VB.Label lab_sta_mac 
         Alignment       =   2  'Center
         BackStyle       =   0  'Transparent
         Caption         =   "S"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   20.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H0000FFFF&
         Height          =   405
         Left            =   1110
         TabIndex        =   79
         Top             =   90
         Visible         =   0   'False
         Width           =   390
      End
      Begin VB.Line lin_no_com_plc 
         BorderWidth     =   3
         Visible         =   0   'False
         X1              =   1035
         X2              =   1542
         Y1              =   570
         Y2              =   90
      End
      Begin VB.Line lin_no_com_spde 
         BorderWidth     =   3
         Visible         =   0   'False
         X1              =   210
         X2              =   710
         Y1              =   570
         Y2              =   90
      End
      Begin VB.Image ima_mas 
         Height          =   480
         Left            =   1065
         Picture         =   "sta_bar.frx":0000
         Stretch         =   -1  'True
         Top             =   90
         Width           =   480
      End
      Begin VB.Shape led_spde_tx 
         FillColor       =   &H000000FF&
         FillStyle       =   0  'Solid
         Height          =   105
         Left            =   750
         Shape           =   3  'Circle
         Top             =   165
         Width           =   105
      End
      Begin VB.Shape led_spde_rx 
         FillColor       =   &H000000FF&
         FillStyle       =   0  'Solid
         Height          =   105
         Left            =   750
         Shape           =   3  'Circle
         Top             =   360
         Width           =   105
      End
      Begin VB.Shape led_plc_tx 
         FillColor       =   &H000000FF&
         FillStyle       =   0  'Solid
         Height          =   105
         Left            =   1575
         Shape           =   3  'Circle
         Top             =   165
         Width           =   105
      End
      Begin VB.Shape led_plc_rx 
         FillColor       =   &H000000FF&
         FillStyle       =   0  'Solid
         Height          =   105
         Left            =   1575
         Shape           =   3  'Circle
         Top             =   360
         Width           =   105
      End
      Begin VB.Label lab_t_plc 
         BackStyle       =   0  'Transparent
         Caption         =   "T"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   1710
         TabIndex        =   78
         Top             =   90
         Width           =   255
      End
      Begin VB.Label lab_r_plc 
         BackStyle       =   0  'Transparent
         Caption         =   "R"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   1710
         TabIndex        =   77
         Top             =   315
         Width           =   255
      End
      Begin VB.Label lab_t_spde 
         BackStyle       =   0  'Transparent
         Caption         =   "T"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   885
         TabIndex        =   76
         Top             =   90
         Width           =   255
      End
      Begin VB.Label lab_r_spde 
         BackStyle       =   0  'Transparent
         Caption         =   "R"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Left            =   885
         TabIndex        =   75
         Top             =   315
         Width           =   180
      End
      Begin VB.Image ima_spde 
         Height          =   480
         Left            =   240
         Picture         =   "sta_bar.frx":0C42
         Stretch         =   -1  'True
         Top             =   90
         Width           =   480
      End
      Begin VB.Label Label4 
         BackColor       =   &H00FFFFFF&
         Height          =   585
         Left            =   180
         TabIndex        =   74
         Top             =   40
         Width           =   1680
      End
   End
   Begin VB.Frame frm_tem_co 
      BackColor       =   &H00008000&
      BorderStyle     =   0  'None
      Caption         =   "Frame1"
      Height          =   735
      Left            =   4725
      TabIndex        =   69
      Top             =   450
      Width           =   1243
      Begin VB.TextBox txt_tem_co 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   15
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   460
         Left            =   23
         TabIndex        =   70
         Text            =   "01:45:60"
         Top             =   225
         Width           =   1215
      End
      Begin VB.Label Label7 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   630
         Left            =   1140
         TabIndex        =   72
         Top             =   0
         Width           =   105
         WordWrap        =   -1  'True
      End
      Begin VB.Label Label3 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Tempo CO"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000005&
         Height          =   240
         Left            =   30
         TabIndex        =   71
         Top             =   0
         Width           =   1005
      End
   End
   Begin MSWinsockLib.Winsock sck_tim 
      Left            =   5910
      Top             =   0
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
      RemoteHost      =   "200.0.0.200"
      RemotePort      =   13
      LocalPort       =   99
   End
   Begin MSWinsockLib.Winsock com_spde_lan 
      Left            =   5460
      Top             =   0
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
      Protocol        =   1
   End
   Begin VB.Frame frm_scarti 
      BackColor       =   &H00008000&
      BorderStyle     =   0  'None
      Enabled         =   0   'False
      Height          =   1440
      Left            =   11079
      TabIndex        =   60
      Top             =   450
      Width           =   930
      Begin VB.TextBox txt_sca 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Index           =   3
         Left            =   30
         TabIndex        =   63
         Text            =   "23"
         Top             =   225
         Width           =   900
      End
      Begin VB.TextBox txt_sca 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Index           =   2
         Left            =   30
         TabIndex        =   62
         Text            =   "351"
         Top             =   600
         Width           =   900
      End
      Begin VB.TextBox txt_sca 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Index           =   1
         Left            =   30
         TabIndex        =   61
         Text            =   "4810"
         Top             =   975
         Width           =   900
      End
      Begin VB.Label lab_gray_med 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0C0C0&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   570
         Index           =   2
         Left            =   825
         TabIndex        =   68
         ToolTipText     =   "0"
         Top             =   450
         Width           =   105
         WordWrap        =   -1  'True
      End
      Begin VB.Label lab_sca 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Scarti"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000005&
         Height          =   240
         Left            =   30
         TabIndex        =   65
         Top             =   0
         Width           =   555
      End
      Begin VB.Label Label2 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   1290
         Left            =   825
         TabIndex        =   64
         Top             =   0
         Width           =   105
         WordWrap        =   -1  'True
      End
   End
   Begin VB.Frame frm_liv 
      BackColor       =   &H00008000&
      BorderStyle     =   0  'None
      Enabled         =   0   'False
      Height          =   1440
      Left            =   7808
      TabIndex        =   51
      Top             =   450
      Width           =   997
      Begin VB.Label lab_liv 
         Appearance      =   0  'Flat
         BackColor       =   &H00008000&
         BackStyle       =   0  'Transparent
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
         Height          =   270
         Index           =   1
         Left            =   60
         TabIndex        =   59
         Top             =   1005
         Width           =   930
      End
      Begin VB.Label lab_liv 
         Appearance      =   0  'Flat
         BackColor       =   &H00008000&
         BackStyle       =   0  'Transparent
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
         Height          =   270
         Index           =   2
         Left            =   60
         TabIndex        =   58
         Top             =   630
         Width           =   930
      End
      Begin VB.Label lab_liv 
         Appearance      =   0  'Flat
         BackColor       =   &H00008000&
         BackStyle       =   0  'Transparent
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
         Height          =   270
         Index           =   3
         Left            =   60
         TabIndex        =   57
         Top             =   255
         Width           =   930
      End
      Begin VB.Label Label6 
         Appearance      =   0  'Flat
         BackColor       =   &H0080FFFF&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   255
         Left            =   890
         TabIndex        =   56
         Top             =   0
         Width           =   105
         WordWrap        =   -1  'True
      End
      Begin VB.Label liv 
         Appearance      =   0  'Flat
         BackColor       =   &H0080FFFF&
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
         Height          =   345
         Index           =   1
         Left            =   30
         TabIndex        =   55
         Top             =   975
         Width           =   960
      End
      Begin VB.Label liv 
         Appearance      =   0  'Flat
         BackColor       =   &H0080FFFF&
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
         Height          =   345
         Index           =   2
         Left            =   30
         TabIndex        =   54
         Top             =   600
         Width           =   960
      End
      Begin VB.Label liv 
         Appearance      =   0  'Flat
         BackColor       =   &H0080FFFF&
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
         Height          =   345
         Index           =   3
         Left            =   30
         TabIndex        =   53
         Top             =   225
         Width           =   960
      End
      Begin VB.Label label23 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00008000&
         BackStyle       =   0  'Transparent
         Caption         =   "Livello"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FFFFFF&
         Height          =   240
         Left            =   30
         TabIndex        =   52
         Top             =   0
         Width           =   645
      End
   End
   Begin VB.Frame frm_sta_res 
      BackColor       =   &H00008000&
      BorderStyle     =   0  'None
      Enabled         =   0   'False
      Height          =   750
      Left            =   5970
      TabIndex        =   47
      Top             =   1140
      Width           =   1845
      Begin VB.TextBox txt_sta_res 
         Appearance      =   0  'Flat
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
         Height          =   390
         Left            =   30
         TabIndex        =   48
         Text            =   "Emergency Stop"
         Top             =   240
         Width           =   1815
      End
      Begin VB.Label lab_sta_res 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Stato reset"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000005&
         Height          =   240
         Left            =   30
         TabIndex        =   50
         Top             =   15
         Width           =   1020
      End
      Begin VB.Label Label32 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   585
         Left            =   1740
         TabIndex        =   49
         Top             =   0
         Width           =   105
         WordWrap        =   -1  'True
      End
   End
   Begin VB.Frame frm_sta_run 
      BackColor       =   &H00008000&
      BorderStyle     =   0  'None
      Enabled         =   0   'False
      Height          =   675
      Left            =   5970
      TabIndex        =   42
      Top             =   450
      Width           =   1845
      Begin VB.TextBox txt_sta_run 
         Appearance      =   0  'Flat
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
         Height          =   390
         Left            =   30
         TabIndex        =   43
         Text            =   "Pronta alla marcia"
         Top             =   225
         Width           =   1815
      End
      Begin VB.Label lab_run 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   615
         Left            =   1740
         TabIndex        =   45
         Top             =   0
         Width           =   105
         WordWrap        =   -1  'True
      End
      Begin VB.Label lab_sta_run 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Stato running"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000005&
         Height          =   240
         Left            =   30
         TabIndex        =   44
         Top             =   0
         Width           =   1290
      End
   End
   Begin VB.Frame frm_num_sca 
      BackColor       =   &H00008000&
      BorderStyle     =   0  'None
      Enabled         =   0   'False
      Height          =   1440
      Left            =   8804
      TabIndex        =   35
      Top             =   450
      Width           =   1305
      Begin VB.TextBox txt_num_sca 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Index           =   1
         Left            =   30
         TabIndex        =   38
         Text            =   "78500"
         Top             =   975
         Width           =   1275
      End
      Begin VB.TextBox txt_num_sca 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Index           =   2
         Left            =   30
         TabIndex        =   37
         Text            =   "4586022"
         Top             =   600
         Width           =   1275
      End
      Begin VB.TextBox txt_num_sca 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Index           =   3
         Left            =   30
         TabIndex        =   36
         Text            =   "125420"
         Top             =   225
         Width           =   1275
      End
      Begin VB.Label lab_gray_med 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0C0C0&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   570
         Index           =   0
         Left            =   1200
         TabIndex        =   66
         ToolTipText     =   "0"
         Top             =   480
         Width           =   105
         WordWrap        =   -1  'True
      End
      Begin VB.Label lab_numero_sca 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Scatole"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000005&
         Height          =   240
         Left            =   30
         TabIndex        =   41
         Top             =   0
         Width           =   720
      End
      Begin VB.Label Label22 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   495
         Left            =   3030
         TabIndex        =   40
         Top             =   -60
         Width           =   90
         WordWrap        =   -1  'True
      End
      Begin VB.Label Label16 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   1230
         Left            =   1200
         TabIndex        =   39
         ToolTipText     =   "0"
         Top             =   0
         Width           =   105
         WordWrap        =   -1  'True
      End
   End
   Begin VB.Frame frm_err 
      BackColor       =   &H00008000&
      BorderStyle     =   0  'None
      Enabled         =   0   'False
      Height          =   1440
      Left            =   10104
      TabIndex        =   29
      Top             =   450
      Width           =   975
      Begin VB.TextBox txt_err 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Index           =   1
         Left            =   30
         TabIndex        =   32
         Text            =   "4810"
         Top             =   975
         Width           =   945
      End
      Begin VB.TextBox txt_err 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Index           =   2
         Left            =   30
         TabIndex        =   31
         Text            =   "351"
         Top             =   600
         Width           =   945
      End
      Begin VB.TextBox txt_err 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Index           =   3
         Left            =   30
         TabIndex        =   30
         Text            =   "23"
         Top             =   225
         Width           =   945
      End
      Begin VB.Label lab_gray_med 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0C0C0&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   570
         Index           =   1
         Left            =   870
         TabIndex        =   67
         ToolTipText     =   "0"
         Top             =   510
         Width           =   105
         WordWrap        =   -1  'True
      End
      Begin VB.Label Label21 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   1290
         Left            =   870
         TabIndex        =   34
         Top             =   0
         Width           =   105
         WordWrap        =   -1  'True
      End
      Begin VB.Label lab_errore 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Err."
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000005&
         Height          =   240
         Left            =   30
         TabIndex        =   33
         Top             =   0
         Width           =   330
      End
   End
   Begin VB.Frame fra_ora 
      BackColor       =   &H00000000&
      BorderStyle     =   0  'None
      Enabled         =   0   'False
      Height          =   585
      Index           =   0
      Left            =   73
      TabIndex        =   26
      Top             =   495
      Width           =   1084
      Begin VB.TextBox txt_dat 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H00000000&
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
         ForeColor       =   &H00FFFFFF&
         Height          =   255
         Left            =   45
         TabIndex        =   28
         Text            =   "22/09/2000"
         Top             =   345
         Width           =   1035
      End
      Begin VB.TextBox txt_ora 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H00000000&
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
         ForeColor       =   &H00FFFFFF&
         Height          =   270
         Left            =   53
         TabIndex        =   27
         Text            =   "14:25:30"
         Top             =   0
         Width           =   960
      End
   End
   Begin VB.Frame frm_mod 
      BackColor       =   &H00008000&
      BorderStyle     =   0  'None
      Enabled         =   0   'False
      Height          =   735
      Left            =   1223
      TabIndex        =   18
      Top             =   450
      Width           =   733
      Begin VB.TextBox txt_mod 
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
         ForeColor       =   &H00FF0000&
         Height          =   460
         Left            =   45
         TabIndex        =   46
         Text            =   "Auto"
         Top             =   225
         Width           =   675
      End
      Begin VB.Label lab_mod 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Modo"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000005&
         Height          =   240
         Left            =   50
         TabIndex        =   20
         Top             =   0
         Width           =   525
      End
      Begin VB.Label Label5 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   645
         Left            =   622
         TabIndex        =   19
         Top             =   0
         Width           =   105
         WordWrap        =   -1  'True
      End
   End
   Begin VB.TextBox txt_idle 
      Height          =   285
      Left            =   4620
      TabIndex        =   17
      Top             =   -5000
      Width           =   645
   End
   Begin VB.PictureBox SSP_mes_all 
      BackColor       =   &H000000C0&
      Height          =   315
      Left            =   2670
      ScaleHeight     =   255
      ScaleWidth      =   1065
      TabIndex        =   15
      Top             =   75
      Width           =   1125
      Begin VB.Label mes_all 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Indirizzo C-Compolet errato"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000005&
         Height          =   195
         Left            =   150
         TabIndex        =   16
         Top             =   60
         Width           =   795
      End
   End
   Begin VB.Timer Tim_com_PLC 
      Enabled         =   0   'False
      Interval        =   300
      Left            =   1995
      Top             =   -45
   End
   Begin VB.Timer Tim_all 
      Enabled         =   0   'False
      Interval        =   500
      Left            =   1500
      Top             =   -45
   End
   Begin VB.Timer tim_com_spde 
      Enabled         =   0   'False
      Interval        =   300
      Left            =   1005
      Top             =   -45
   End
   Begin VB.Timer tim_com_azi 
      Enabled         =   0   'False
      Interval        =   200
      Left            =   495
      Top             =   -45
   End
   Begin VB.Frame frm_met_man 
      BackColor       =   &H00008000&
      BorderStyle     =   0  'None
      Caption         =   "Frame1"
      Height          =   735
      Left            =   3540
      TabIndex        =   10
      Top             =   450
      Width           =   1185
      Begin VB.TextBox txt_met_man 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   15.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   460
         Left            =   23
         TabIndex        =   13
         Text            =   "155712"
         Top             =   225
         Width           =   1155
      End
      Begin VB.Label Lab_metri_man 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Metri"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000005&
         Height          =   240
         Left            =   30
         TabIndex        =   12
         Top             =   0
         Width           =   480
      End
      Begin VB.Label Label17 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   630
         Left            =   1080
         TabIndex        =   11
         Top             =   0
         Width           =   105
         WordWrap        =   -1  'True
      End
   End
   Begin VB.Frame Fra_vel_max 
      BackColor       =   &H00008000&
      BorderStyle     =   0  'None
      Caption         =   "Frame1"
      Height          =   735
      Left            =   2693
      TabIndex        =   6
      Top             =   450
      Width           =   855
      Begin VB.TextBox txt_vel_max 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   15.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   460
         Left            =   37
         TabIndex        =   9
         Text            =   "260"
         Top             =   225
         Width           =   813
      End
      Begin VB.Label Lab_velocita_max 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "V. max"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000005&
         Height          =   240
         Left            =   40
         TabIndex        =   8
         Top             =   0
         Width           =   675
      End
      Begin VB.Label Label1 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   630
         Left            =   742
         TabIndex        =   7
         Top             =   0
         Width           =   105
         WordWrap        =   -1  'True
      End
   End
   Begin VB.Frame frm_vel 
      BackColor       =   &H00008000&
      BorderStyle     =   0  'None
      Enabled         =   0   'False
      Height          =   735
      Left            =   1943
      TabIndex        =   2
      Top             =   450
      Width           =   754
      Begin VB.TextBox txt_vel 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   15.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   460
         Left            =   30
         TabIndex        =   5
         Text            =   "230"
         Top             =   225
         Width           =   713
      End
      Begin VB.Label Label9 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   630
         Left            =   642
         TabIndex        =   4
         Top             =   0
         Width           =   105
         WordWrap        =   -1  'True
      End
      Begin VB.Label lab_velocita 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Vel."
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000005&
         Height          =   240
         Left            =   40
         TabIndex        =   3
         Top             =   0
         Width           =   375
      End
   End
   Begin VB.Frame frm_all 
      BackColor       =   &H00008000&
      BorderStyle     =   0  'None
      Enabled         =   0   'False
      Height          =   705
      Left            =   1765
      TabIndex        =   0
      Top             =   1178
      Width           =   4230
      Begin VB.Label lab_bm 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Bordo macchina"
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
         Height          =   420
         Left            =   0
         TabIndex        =   25
         Top             =   60
         Width           =   975
      End
      Begin VB.Label lbl_con 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Consolle"
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
         Height          =   210
         Left            =   1005
         TabIndex        =   24
         Top             =   165
         Width           =   750
      End
      Begin VB.Label lbl_qe 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Quadri elettrici"
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
         Height          =   420
         Left            =   1830
         TabIndex        =   23
         Top             =   60
         Width           =   855
      End
      Begin VB.Label Lab_har 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Hardware"
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
         Height          =   210
         Left            =   2685
         TabIndex        =   22
         Top             =   165
         Width           =   870
      End
      Begin VB.Label Lab_mal 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "!"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   330
         Left            =   3855
         TabIndex        =   21
         Top             =   120
         Width           =   105
      End
      Begin VB.Shape Shp_all_qe 
         BorderStyle     =   0  'Transparent
         FillColor       =   &H0000FF00&
         FillStyle       =   0  'Solid
         Height          =   495
         Left            =   1995
         Shape           =   3  'Circle
         Top             =   60
         Width           =   525
      End
      Begin VB.Shape Shp_all_bm 
         BorderStyle     =   0  'Transparent
         FillColor       =   &H0000FF00&
         FillStyle       =   0  'Solid
         Height          =   495
         Left            =   210
         Shape           =   3  'Circle
         Top             =   60
         Width           =   525
      End
      Begin VB.Shape Shp_all_con 
         BorderStyle     =   0  'Transparent
         FillColor       =   &H0000FF00&
         FillStyle       =   0  'Solid
         Height          =   495
         Left            =   1125
         Shape           =   3  'Circle
         Top             =   60
         Width           =   525
      End
      Begin VB.Shape Shp_all_har 
         BorderStyle     =   0  'Transparent
         FillColor       =   &H0000FF00&
         FillStyle       =   0  'Solid
         Height          =   495
         Left            =   2865
         Shape           =   3  'Circle
         Top             =   60
         Width           =   525
      End
      Begin VB.Shape Shp_all_mal 
         BorderStyle     =   0  'Transparent
         FillColor       =   &H0000FF00&
         FillStyle       =   0  'Solid
         Height          =   495
         Left            =   3615
         Shape           =   3  'Circle
         Top             =   60
         Width           =   585
      End
      Begin VB.Label lab_all 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   595
         Left            =   7
         TabIndex        =   1
         Top             =   -10
         Width           =   4200
         WordWrap        =   -1  'True
      End
   End
   Begin MSCommLib.MSComm com_sch_tag 
      Index           =   2
      Left            =   9480
      Top             =   -135
      _ExtentX        =   1005
      _ExtentY        =   1005
      _Version        =   327681
      CommPort        =   6
      DTREnable       =   0   'False
      InBufferSize    =   2048
      NullDiscard     =   -1  'True
      OutBufferSize   =   2048
      RThreshold      =   1
      SThreshold      =   1
   End
   Begin MSCommLib.MSComm com_sch_tag 
      Index           =   1
      Left            =   8880
      Top             =   -135
      _ExtentX        =   1005
      _ExtentY        =   1005
      _Version        =   327681
      CommPort        =   5
      DTREnable       =   0   'False
      InBufferSize    =   2048
      NullDiscard     =   -1  'True
      OutBufferSize   =   2048
      RThreshold      =   1
      SThreshold      =   1
   End
   Begin MSCommLib.MSComm com_sch_tag 
      Index           =   0
      Left            =   8280
      Top             =   -135
      _ExtentX        =   1005
      _ExtentY        =   1005
      _Version        =   327681
      CommPort        =   4
      DTREnable       =   0   'False
      InBufferSize    =   2048
      NullDiscard     =   -1  'True
      OutBufferSize   =   2048
      RThreshold      =   1
      SThreshold      =   1
   End
   Begin MSCommLib.MSComm com_spde_ser 
      Left            =   6465
      Top             =   -135
      _ExtentX        =   1005
      _ExtentY        =   1005
      _Version        =   327681
      CommPort        =   3
      DTREnable       =   0   'False
      InBufferSize    =   4096
      NullDiscard     =   -1  'True
      OutBufferSize   =   1024
      RThreshold      =   1
      SThreshold      =   1
   End
   Begin MSWinsockLib.Winsock sck_sch_tag 
      Index           =   0
      Left            =   10080
      Top             =   0
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
      Protocol        =   1
   End
   Begin MSWinsockLib.Winsock sck_sch_tag 
      Index           =   1
      Left            =   10560
      Top             =   0
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
      Protocol        =   1
   End
   Begin MSWinsockLib.Winsock sck_sch_tag 
      Index           =   2
      Left            =   11040
      Top             =   0
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
      Protocol        =   1
   End
   Begin VB.Line Line1 
      BorderColor     =   &H00000000&
      BorderWidth     =   2
      X1              =   5863.05
      X2              =   8000.21
      Y1              =   1394.893
      Y2              =   1394.893
   End
   Begin VB.Shape Shape3 
      BorderWidth     =   2
      Height          =   690
      Left            =   15
      Top             =   450
      Width           =   1208
   End
   Begin VB.Shape Shape2 
      FillStyle       =   0  'Solid
      Height          =   60
      Left            =   -30
      Top             =   0
      Width           =   13005
   End
   Begin VB.Shape Shape1 
      FillStyle       =   0  'Solid
      Height          =   60
      Left            =   -30
      Top             =   390
      Width           =   13005
   End
   Begin VB.Label lab_msg 
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   15.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000C0&
      Height          =   360
      Left            =   105
      TabIndex        =   14
      Top             =   30
      Width           =   11820
   End
End
Attribute VB_Name = "sta_bar"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Public WithEvents eve_tag As ClasseStato
Attribute eve_tag.VB_VarHelpID = -1
Public WithEvents eve_ind_plc As ind_plc
Attribute eve_ind_plc.VB_VarHelpID = -1
'Iciliouff 03-11-2003
Public WithEvents rmt_sta_bar_plc As rmt
Attribute rmt_sta_bar_plc.VB_VarHelpID = -1
Public WithEvents rmt_sta_bar_macro As rmt     'Viene utilizzata SOLO per gestire le richieste verso il PLC che si generano da codice es.risposta positiva da sch(NO OPERATORE)
Attribute rmt_sta_bar_macro.VB_VarHelpID = -1

Private WithEvents eve_ord_mac As CollezioneOrdini
Attribute eve_ord_mac.VB_VarHelpID = -1
Private lamp_run As Boolean 'indica se la label dello stato run deve lampeggiare o meno
Public Fins As Fins
#If CJ Then
  Public SYSMAC_PLC As Object
#End If

Public Sub del_all()
Dim i As Integer

For i = 1 To 3
    txt_num_sca(i) = ""
    txt_err(i) = ""
    txt_sca(i) = ""
Next i
If frm_err.BackColor <> COL_DARK_Green Then frm_err.BackColor = COL_DARK_Green
txt_mod = ""
If frm_mod.BackColor <> COL_DARK_Green Then frm_mod.BackColor = COL_DARK_Green
txt_vel = ""
txt_vel_max = ""
txt_met_man = ""
If lab_sta_mac.Visible Then lab_sta_mac.Visible = False
txt_sta_run = ""
If frm_sta_run.BackColor <> COL_DARK_Green Then frm_sta_run.BackColor = COL_DARK_Green
txt_sta_res = ""
If frm_sta_res.BackColor <> COL_DARK_Green Then frm_sta_res.BackColor = COL_DARK_Green
If Not sta_tag.spde_OK Then
  frm_com.BackColor = COL_ALL_Red
Else
  frm_com.BackColor = COL_DARK_Green
End If

If Shp_all_bm.FillColor <> LIGHT_Green Then Shp_all_bm.FillColor = LIGHT_Green
If Shp_all_con.FillColor <> LIGHT_Green Then Shp_all_con.FillColor = LIGHT_Green
If Shp_all_har.FillColor <> LIGHT_Green Then Shp_all_har.FillColor = LIGHT_Green
'If Shp_all_mal.FillColor <> LIGHT_Green Then Shp_all_mal.FillColor = LIGHT_Green
If Shp_all_qe.FillColor <> LIGHT_Green Then Shp_all_qe.FillColor = LIGHT_Green
End Sub


Public Sub mes_PLC(ind_rete As Integer, ind_nodo As Integer, ind_unita As Integer)

messag_win msg("err_sys_plc")
lin_no_com_plc.Visible = True
If Not sta_tag.spde_OK Then frm_com.BackColor = COL_ALL_Red
If lab_msg.Caption <> msg("sch_plc_non_pre") Then messaggio msg("ver_ind_plc"), True, False
log_str msg("err_sys_plc") & ". Net_ID = " & ind_rete & " Node_ID = " & ind_nodo & " Unit_ID = " & ind_unita & ".", MED_PRIORITY, log_plc, False
sta_tag.com_OK = False

del_all
End Sub

Function bigny_mask(toformat As String) As String
' PRocchiccioli FI_28_12_10
' Impianto di Bigny. Il supervisore Marquipe DECC invia L1 L2 e L3 anche se la macchina
' e' a 2 livelli. L1 e L2 sono il livello inferiore  e L3 quello superiore
' questo perche' hanno esigenze particolari con lo stacker.
' Per evitare di modficare radicalmente il programma "maschero" la stringa ricevuta
' L1->L1
' L2->L1
' L3->L2
Dim posL As Long
Dim found As Boolean

found = False
posL = 0
posL = InStr(1, toformat, "L2")
If posL = 0 Then
    bigny_mask = toformat
Else
    bigny_mask = toformat
    Do While posL > 0
    Mid(bigny_mask, posL, 2) = "L1"
    posL = InStr(posL + 1, bigny_mask, "L2")
    found = True
    Loop
End If

posL = 0
posL = InStr(1, bigny_mask, "L3")
If posL = 0 Then
    If found <> True Then bigny_mask = toformat
Else
    Do While posL > 0
    Mid(bigny_mask, posL, 2) = "L2"
    posL = InStr(posL + 1, bigny_mask, "L3")
    Loop
End If

End Function


Function prel_pri_cmd(arr As String) As String
Dim pos_stx As Long
Dim pos_etx As Long

'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
Dim pos_stx_simon As Long
Dim pos_etx_simon As Long
Dim status_tag_simon As Long

'PMASSARO_FI22/02/2011 - Corretto Bug su validazione stringa con protocollo SIMON10
'La riga "pos_stx = InStr(1, arr, Chr$(STX_TAG)" non andava modificata
'pos_stx = InStr(1, Chr$(STX_TAG), arr)
pos_stx = InStr(1, arr, Chr$(STX_TAG))

'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
status_tag_simon = InStr(arr, REQ_TAG_SIMON)
pos_stx_simon = InStr(arr, STX_TAG_SIMON)
pos_etx_simon = InStr(arr, ETX_TAG_SIMON)

'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
'If pos_stx > 0 Then 'ricevuto STX
If par_mac.com_spde.ver_spde <> Simon10 And pos_stx > 0 Then 'ricevuto STX
  pos_etx = InStr(pos_stx + 1, arr, Chr$(ETX_TAG))
  If pos_etx > 0 Then 'ricevuto ETX
   'PRocchiccioli_FI_16/12/2010 Emulazione con protocollo taglierina SIMON
   'Non tornava se la stringa non iniziava con STX (Es. 123STXsacETX)
   'prel_pri_cmd = Mid$(arr, pos_stx, pos_etx)
    prel_pri_cmd = Mid$(arr, pos_stx, pos_etx - pos_stx + 1)
    arr = Mid$(arr, pos_etx + 1)
  Else
    prel_pri_cmd = ""
  End If
'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
'Inizio
ElseIf par_mac.com_spde.ver_spde = Simon10 And (pos_stx_simon > 0 Or status_tag_simon > 0) Then 'ricevuto STX

  If pos_stx_simon > 0 And status_tag_simon = 0 Then        'se ho ricevuto carattere "(" di invio ordine restituisco i dati contenuti dell'ordine
    pos_etx_simon = InStr(pos_stx_simon + 1, arr, ETX_TAG_SIMON)
    If pos_etx_simon > 0 Then 'ricevuto ETX
      'PRocchiccioli_FI_16/12/2010 Emulazione con protocollo taglierina SIMON
      'prel_pri_cmd = Mid$(arr, pos_stx_simon, pos_etx_simon)
      prel_pri_cmd = Mid$(arr, pos_stx_simon, pos_etx_simon - pos_stx_simon + 1)
      arr = Mid$(arr, pos_etx_simon + 1)
    Else
      prel_pri_cmd = ""
    End If
  ElseIf pos_stx_simon = 0 And status_tag_simon > 0 Then    'se ho ricevuto carattere "?" richiesto stato link DEPM-TAGLIERINA restituisco "?"
    prel_pri_cmd = "?"
    arr = ""
    'PRocchiccioli_FI_30/12/2010
    ' stringa troncata da carattere " ?"  causato da probabile
    ' interruzione dall' invio ordine dal sistema. Alimentazione o
    ' cancellazione ordine o modalita' invio da Marquipe DECC
    ElseIf pos_stx_simon > 0 And status_tag_simon > 0 Then  ' ricevuto "(L1/1200 L2?"
        prel_pri_cmd = "X" 'carattere identificativo errore comunicazione
        arr = ""
  End If
'Fine
Else
  prel_pri_cmd = ""
End If
End Function

Sub rx_dat(rx As String)
Static arr As String
Dim pos_stx As Long
Dim i As Long
Dim tx As String
Dim tmp As Long
Static formatted As String


If led_spde_rx.FillColor = vbGreen Then
  led_spde_rx.FillColor = COL_DARK_Green
Else
  led_spde_rx.FillColor = vbGreen
End If
If led_spde_tx.FillColor = led_spde_rx.FillColor Then
  led_spde_tx.FillColor = IIf(led_spde_rx.FillColor = vbGreen, COL_DARK_Green, vbGreen)
End If
'led_spde_tx.FillColor = vbRed
If Not sta_tag.MAC_IN_MANUALE(1) Then lin_no_com_spde.Visible = False
    arr = arr + rx
    pos_stx = InStr(1, arr, Chr$(STX_TAG))
    i = 1
    'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
    'If pos_stx > 0 then
    If pos_stx > 0 Or par_mac.com_spde.ver_spde = Simon10 Then
      Do While i < 3
        pos_stx = InStr(pos_stx + 1, arr, Chr$(STX_TAG))
        If pos_stx = 0 Then Exit Do
        i = i + 1
      Loop
      If i >= 3 Then arr = Left$(arr, pos_stx - 1)
      
        rx = prel_pri_cmd(arr)
        
        'PMASSARO_FI22/02/2011 - Corretto Bug su validazione stringa con protocollo SIMON10
        'Aggiunto IF altrimenti andava in conflitto con le altre versioni di protocollo
        If par_mac.com_spde.ver_spde = Simon10 Then
        'PRocchiccioli_FI_30/12/2010
        If rx = "X" Then
            ' troncata stringa da MarquipDecc con "?" richiesta collegamento
            arr = "" ' pulizia arr
            log_str "rx_dat: troncata stringa da MarquipDecc con richiesta collegamento", MED_PRIORITY, log_spde
        End If
        
          '**************************
          ' PRocchiccioli FI_28_12_10
          'aggiunta maschera ricezione stringa per impianto Bigny.
          ' Vedi commento nella funzione bigny_mask!
          rx = bigny_mask(rx) ' conversione livelli
          '**************************
        End If
        
      If Trim$(rx) <> "" Then
        If str_ric_OK(rx) Then 'Controllo lunghezza stringa richiesta
          Select Case par_mac.com_spde.ver_spde
            'Case Fosber10, Fosber11 'protocollo DIGITEC
            Case Fosber10 '30112001 Stagetti tolto protocollo fosber 1.1protocollo DIGITEC
              dig_ric_dat_spde (rx)
            Case Fosber20, Fosber21, Fosber30   'Iciliouff 20-11-2003/Robertouff 26-11-2007
              'Protocollo con SPDE_2000, Protocollo per dati cut to pattern
              ric_dat_spde (rx)
            Case Simon10
              'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
            '***************************
              ric_dat_spde (rx)
          End Select
        Else
          'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
          tx = dig_mes_spde(NAK) 'preparazione messaggio di errore
          tra_dat_spde (tx) 'invio il messaggio
        End If
      Else
        'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
        'etx o stx non presenti
        ' PRocchiccioli FI_28_12_10
        ' problema invio stringhe spezzate. Prima di accettare ordine invia msg ordine non accettato
        'messaggio msg("rif_ord_spde"), True, False 'preparazione messaggio di errore
        'tra_dat_spde ("N") 'invio il messaggio
        log_str "rx_dat: ricevuto comando NON valido: " + arr, MED_PRIORITY, log_spde
        ' PRocchiccioli FI_28_12_10
        ' problema invio stringhe spezzate da prel_pri_cmd.
        '(L1/1200 L2/12 ("1invio")->la funzione ritorna con rx stringa vuota
        ' e cosi' svuoterei variabile arr di ricezione
        'arr = ""
      End If
    Else
      arr = ""
    End If
  
End Sub

Private Sub com_sch_tag_OnComm(Index As Integer)
'Iciliouff 03-11-2003
'Gestisce gli eventi delle comunicazioni con la scheda di taglio
Dim r As String
  If par_mac.tip_com_sch_tag(Index + 1) = 0 Then
    Select Case com_sch_tag(Index).CommEvent
      Case comEvSend
        led_plc_tx.FillColor = vbGreen
        led_plc_rx.FillColor = vbRed
      Case comEvReceive
        r = com_sch_tag(Index).Input
        rx_sch_tag r, Index + 1
    End Select
  End If
  
'Static ric_stx As Boolean 'ricevuto stx
'Dim r As String
'Static rx As String
'
'Select Case com_sch_tag(Index).CommEvent
'  ' Errors
'    Case comEventBreak    ' A Break was received.
'    ' Code to handle a BREAK goes here.
'    Case comEventCDTO ' CD (RLSD) Timeout.
'    Case comEventCTSTO    ' CTS Timeout.
'    Case comEventDSRTO    ' DSR Timeout.
'    Case comEventFrame    ' Framing Error
'    Case comEventOverrun  ' Data Lost.
'    Case comEventRxOver   ' Receive buffer overflow.
'    Case comEventRxParity ' Parity Error.
'    Case comEventTxFull   ' Transmit buffer full.
'  ' Events
'    Case comEvCD   ' Change in the CD line.
'    Case comEvCTS  ' Change in the CTS line.
'    Case comEvDSR  ' Change in the DSR line.
'    Case comEvRing ' Change in the Ring Indicator.
'    Case comEvSend
'      led_plc_tx.FillColor = vbGreen
'      led_plc_rx.FillColor = vbRed
'    Case comEvReceive  ' Received RThreshold # of chars.
'      r = com_sch_tag(Index).Input 'prelevo il buffer in ricezione
'      'led_plc_rx.FillColor = vbGreen
'      'led_plc_tx.FillColor = vbRed
'
''     If chr_ok(Asc(r)) Then ' controllo se il carattere ricevuto è tra quelli riconosciuti da Windows
'
'          Select Case Asc(r)
'
'            Case STX_ST   'ho ricevuto STX
'              rx = r
'              ric_stx = True
'            Case EOT_ST   'ho ricevuto EOT
'              If ric_stx Then
'                  ric_stx = False
'                  If Len(rx) <= COM_SCH_TAG_LEN_MIN Then
'                      ' lunghezza minima non raggiunta dalla stringa ricevuta
'                      messaggio msg("err_com_sch_tag") & " " & Index + 1, True, False, True, Index + 1
'                      'Arturo uff 12-11-02 (4)
'                      log_str "snd_str_sch_tag: lunghezza minima non raggiunta dalla stringa ricevuta", MED_PRIORITY, log_sch_tag
'                  Else
'                      par_mac.com_sch_tag(Index + 1).ricevuto = True
'                      par_mac.com_sch_tag(Index + 1).com_att = False
'                      rea_dat_sch_tag Index + 1, rx
'                      'Arturo uff 12-11-02 (4)
'                      log_str "snd_str_sch_tag: rx = '" + rx + "'", MED_PRIORITY, log_sch_tag
'                  End If
'              End If
'            Case ACK_ST   'ho ricevuto un ACK
'              par_mac.com_sch_tag(Index + 1).ricevuto = True
'              par_mac.com_sch_tag(Index + 1).com_att = False
'              ges_fine_trasmissione Index + 1, True
'              If CASO_SND_SCH_TAG <> INVIO_INIZIALE_PAR Then
'                messaggio msg("tra_eff_sch_tag") & " " & Index + 1, False, False, True, Index + 1
'              Else
'                messaggio msg("inv_par_sch_tag"), False, False
'              End If
'              'Arturo uff 12-11-02 (4)
'              log_str "snd_str_sch_tag: ricevuto ACK_ST", MED_PRIORITY, log_sch_tag
'            Case NAK_ST   'ho ricevuto un NAK
'              par_mac.com_sch_tag(Index + 1).ricevuto = True
'              par_mac.com_sch_tag(Index + 1).com_att = False
'              ges_fine_trasmissione Index + 1, False
'              If CASO_SND_SCH_TAG <> INVIO_INIZIALE_PAR Then
'                messaggio msg("ope_ann") & ". " & msg("err_com_sch_tag") & " " & Index + 1, True, False, True, Index + 1
'              End If
'              'Arturo uff 12-11-02 (4)
'              log_str "snd_str_sch_tag: ricevuto NAK_ST", MED_PRIORITY, log_sch_tag
'            Case Else   'qualsiasi altro carattere
'              If ric_stx Then rx = rx + r
'          End Select
'     'Else ' carattere non riconosciuto da Windows
'
'     'End If
'
'End Select
End Sub


Private Sub com_spde_lan_DataArrival(ByVal bytesTotal As Long)
Dim r As String
'andrea 09-07-02 uff
On Error GoTo gesterr
If TypeName(par_mac.com_spde.con) = "Nothing" Then
  ini_porte_SPDE
Else
  If par_mac.com_spde.con = com_spde_lan Then
    com_spde_lan.GetData r
    If bytesTotal <> Len(r) Then
      'log_str "com_spde_lan_DataArrival: not last transmission", MED_PRIORITY, log_spde
    End If
    If par_abi.abi_com_PU Then rx_dat (r)
  End If
End If
Exit Sub
gesterr:
'andrea 09-07-02 uff
      log_str "Com_spde_lan_DATAARRIVAL: Errore numero" + str(err.Number) + ", Descrizione = " + err.Description + ", Sorgente = " + err.Source + ", Ultimo errore Dll" + str(err.LastDllError) + ", File Help = " + err.HelpFile, MED_PRIORITY, log_gen
  log_str "Byte ricevuti " + Format(bytesTotal) + ", stato " + Format(com_spde_lan.State) + ", stringa ricevuta " + r, MED_PRIORITY, log_gen
  If com_spde_lan.State <> sckClosed Then com_spde_lan.Close: log_str "Il socket è stato chiuso", MED_PRIORITY, log_gen
End Sub

Private Sub com_spde_ser_OnComm()
'gestisce gli eventi delle comunicazioni con il Punto Unico Ingresso Dati
Dim r As String
Select Case com_spde_ser.CommEvent

  ' Errors
    Case comEventBreak    ' A Break was received.
    ' Code to handle a BREAK goes here.
    Case comEventCDTO ' CD (RLSD) Timeout.
    Case comEventCTSTO    ' CTS Timeout.
    Case comEventDSRTO    ' DSR Timeout.
    Case comEventFrame    ' Framing Error
    Case comEventOverrun  ' Data Lost.
    Case comEventRxOver   ' Receive buffer overflow.
    Case comEventRxParity ' Parity Error.
    Case comEventTxFull   ' Transmit buffer full.
  ' Events
    Case comEvCD   ' Change in the CD line.
    Case comEvCTS  ' Change in the CTS line.
    Case comEvDSR  ' Change in the DSR line.
    Case comEvRing ' Change in the Ring Indicator.
    Case comEvSend
    Case comEvReceive  ' Received RThreshold # of chars.
      r = com_spde_ser.Input
      If par_abi.abi_com_PU Then rx_dat (r)

End Select
End Sub


Private Sub eve_ind_plc_newplc(ind_rete As Integer, ind_nodo As Integer, ind_unita As Integer)
Dim i As Integer
Dim error As Boolean
On Local Error GoTo ges_err

i = 0
error = False
      'ABorelli_FI17/11/2006_1

If val(par_mac.com_plc_lan.abi_lan) = 0 Then
SYSMAC_PLC.NetID = ind_rete
SYSMAC_PLC.NodeID = ind_nodo
SYSMAC_PLC.UnitID = ind_unita
Else
  Fins.Enabled = False
  Fins.sck.Close
  Fins.RemoteHost = par_mac.com_plc_lan.ind_rem
  Fins.RemotePort = par_mac.com_plc_lan.por_rem
  Fins.TimeOut = par_mac.com_plc_lan.TimeOut
  Fins.Enabled = True
End If
If Not error Then
    sta_tag.com_OK = True
    messag_win ""
    If lin_no_com_plc.Visible Then
        lin_no_com_plc.Visible = False
    End If
    If frm_com.BackColor <> COL_DARK_Green Then
      frm_com.BackColor = COL_DARK_Green
    End If
Else
    mes_PLC ind_rete, ind_nodo, ind_unita
End If

Exit Sub
ges_err:
    If err.Number = 57 Then 'errore sull'indirizzo finsgateway
        error = True
        i = i + 1
        If i < 3 Then
            Resume Next
        End If
    Else
        log_str "on error intervenuta in eve_ind_plc new_plc della form sta_bar" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
    End If

End Sub

Private Sub eve_ord_mac_Refresh(Shift As Boolean)
Dim i As Integer
If ord_mac.Count = 0 Then
    txt_vel_max = ""
    txt_met_man = ""
    For i = 1 To 3
      txt_num_sca(i) = ""
      txt_err(i) = ""
      txt_sca(i).Text = ""
    Next i
    frm_err.BackColor = COL_DARK_Green
    frm_met_man.BackColor = COL_DARK_Green
End If
End Sub

Private Sub eve_tag_err(liv As Integer, err As String)
Dim i As Integer
Dim tro As Boolean
Static errore(1 To 3) As Long
'evento per rilevare variazioni dell'errore di taglio (per livello)

If Trim$(err) = "" Then
  txt_err(liv).Text = ""
  errore(liv) = 0
  Exit Sub
End If
txt_err(liv).Text = IIf(abi_mis_ang, con_dmm_to_inc(err), err)
errore(liv) = val(Abs(err))
tro = False

Select Case val(par_mac.car_tec.num_liv_tag)
  
  Case 1
    If errore(3) > val(par_mac.sch_tag.sch_tag_liv(LIV_ALT).lim_inf_tol_tag) Or _
     errore(3) > val(par_mac.sch_tag.sch_tag_liv(LIV_ALT).lim_sup_tol_tag) Then
        tro = True
        txt_err(3).ForeColor = COL_ALL_Red
    Else
        txt_err(3).ForeColor = vbBlack
    End If
  
  Case 2
    If errore(1) > val(par_mac.sch_tag.sch_tag_liv(LIV_BAS).lim_inf_tol_tag) Or _
     errore(1) > val(par_mac.sch_tag.sch_tag_liv(LIV_BAS).lim_sup_tol_tag) Then
        tro = True
        txt_err(1).ForeColor = COL_ALL_Red
    Else
        txt_err(1).ForeColor = vbBlack
    End If
    If errore(3) > val(par_mac.sch_tag.sch_tag_liv(LIV_ALT).lim_inf_tol_tag) Or _
     errore(3) > val(par_mac.sch_tag.sch_tag_liv(LIV_ALT).lim_sup_tol_tag) Then
        tro = True
        txt_err(3).ForeColor = COL_ALL_Red
    Else
        txt_err(3).ForeColor = vbBlack
    End If
  
  Case 3
    For i = 1 To 3
      If errore(i) > val(par_mac.sch_tag.sch_tag_liv(i).lim_inf_tol_tag) Or _
       errore(i) > val(par_mac.sch_tag.sch_tag_liv(i).lim_sup_tol_tag) Then
          tro = True
          txt_err(i).ForeColor = COL_ALL_Red
      Else
          txt_err(i).ForeColor = vbBlack
      End If
    Next i

End Select

If tro Then
  frm_err.BackColor = COL_ALL_Red
Else
  frm_err.BackColor = COL_DARK_Green
End If

End Sub

Private Sub eve_tag_gruall(v As String)
'evento per i semafori dei gruppi allarmi
' non e' compresa la gestione dell' allarme di comunicazione PLC (quest'ultima viene verificata alla sta_bar_load)
' non e' compresa la gestione dell' allarme di comunicazione con SPDE
' bordo macchina, consolle, quadri elettrici, hardware, malfunzionamenti
Dim val As Double

  val = Hex_Dec(bin_hex(v))
  
  If (val And (2 ^ POS_GRU_MAL)) = 2 ^ POS_GRU_MAL Then   'malfunzionamenti / manutenzione
    Shp_all_mal.FillColor = RED_GRU_ALL
    sta_tag.gru_all_att(POS_GRU_MAL) = True
  Else
    Shp_all_mal.FillColor = LIGHT_Green
    sta_tag.gru_all_att(POS_GRU_MAL) = False
  End If
  
  If (val And (2 ^ POS_GRU_HAR)) = 2 ^ POS_GRU_HAR Then                   ' Hardware
    Shp_all_har.FillColor = RED_GRU_ALL
    sta_tag.gru_all_att(POS_GRU_HAR) = True
  Else
    Shp_all_har.FillColor = LIGHT_Green
    sta_tag.gru_all_att(POS_GRU_HAR) = False
  End If
  
  sta_tag.gru_all_att(POS_GRU_QE_MST) = IIf((val And (2 ^ POS_GRU_QE_MST)) <> 0, True, False)
  sta_tag.gru_all_att(POS_GRU_QE_SLV) = IIf((val And (2 ^ POS_GRU_QE_SLV)) <> 0, True, False)
  sta_tag.gru_all_att(POS_GRU_QE_EST) = IIf((val And (2 ^ POS_GRU_QE_EST)) <> 0, True, False)
  If (val And (2 ^ POS_GRU_QE_MST + 2 ^ POS_GRU_QE_SLV + 2 ^ POS_GRU_QE_EST)) <> 0 Then ' quadro master,slave,esterno
    Shp_all_qe.FillColor = RED_GRU_ALL
  Else
    Shp_all_qe.FillColor = LIGHT_Green
  End If
  
  If (val And (2 ^ POS_GRU_BM)) = 2 ^ POS_GRU_BM Then                   ' bordo macchina
    Shp_all_bm.FillColor = RED_GRU_ALL
    sta_tag.gru_all_att(POS_GRU_BM) = True
  Else
    Shp_all_bm.FillColor = LIGHT_Green
    sta_tag.gru_all_att(POS_GRU_BM) = False
  End If
  
  If (val And (2 ^ POS_GRU_CON)) = 2 ^ POS_GRU_CON Then                   ' consolle
    Shp_all_con.FillColor = RED_GRU_ALL
    sta_tag.gru_all_att(POS_GRU_CON) = True
  Else
    Shp_all_con.FillColor = LIGHT_Green
    sta_tag.gru_all_att(POS_GRU_CON) = False
  End If

End Sub

Private Sub eve_tag_liv()
lab_liv(3).Caption = msg("liv_alt")
If val(par_mac.car_tec.num_liv_tag) = 3 Then
  lab_liv(2).Caption = msg("liv_med")
Else
  lab_liv(2).Caption = ""
End If
If val(par_mac.car_tec.num_liv_tag) >= 2 Then
  lab_liv(1).Caption = msg("liv_bas")
Else
  lab_liv(1).Caption = ""
End If
End Sub

Private Sub eve_tag_mac(Mac As Sta_mac)


Select Case Mac
  Case mac_in_lap 'MACCHINA IN LAPPATURA
    lab_sta_mac.Caption = "L"
    lab_sta_mac.Visible = True
  Case mac_in_man 'MACCHINA IN MANUTENZIONE
    lab_sta_mac.Caption = "M"
    lab_sta_mac.Visible = True
  Case mac_in_sim 'MACCHINA IN SIMULAZIONE
    lab_sta_mac.Caption = "S"
    lab_sta_mac.Visible = True
  Case ness_mac
    lab_sta_mac.Caption = ""
    lab_sta_mac.Visible = False
End Select

End Sub

Private Sub eve_tag_modo(Sta_mac As sta_mod)

Select Case Sta_mac
  Case automatico
    txt_mod = msg("msg_mac_in_aut")
    txt_mod.ForeColor = DARK_BLUE
    frm_mod.BackColor = COL_DARK_Green
    If sta_tag.spde_OK Then             ' ABorelli_FI09/09/2005_1
      lin_no_com_spde.Visible = False
    End If
  Case manuale
    txt_mod = msg("msg_mac_in_man")
    txt_mod.ForeColor = COL_MODO
    frm_mod.BackColor = COL_MODO
    If sta_tag.spde_OK Then
      lin_no_com_spde.Visible = True
      lin_no_com_spde.BorderColor = RED_GRU_ALL
    End If
End Select
End Sub

Private Sub eve_tag_num(liv As Integer, mis As String)
'evento per le variazioni del numero di fogli tagliati (per livello)
Dim i As Integer
Dim tro As Boolean
Dim ord As Tip_Ordine
Dim liv_tmp

CopyType ord, ord_mac.item(1).ordine, Len(ord_mac.item(1).ordine)
tro = False
txt_num_sca(liv).Text = mis
For i = 1 To 3
  liv_tmp = ord_mac.item(1).ind_liv(i)
  If liv_tmp <> 0 Then
      If val(txt_num_sca(liv_tmp).Text) > val(ord.ord(i).sca_tot_num) Then
        tro = True
        txt_num_sca(liv_tmp).ForeColor = COL_ALL_Red
        'Exit For
      Else
        txt_num_sca(liv_tmp).ForeColor = vbBlack
      End If
  End If
Next i
If tro Then
  frm_num_sca.BackColor = COL_ALL_Red
Else
  frm_num_sca.BackColor = COL_DARK_Green
End If
End Sub


Private Sub eve_tag_numerr(liv As Integer, mis As String)
'evento per le variazioni del numero di fogli errati (per livello)
txt_sca(liv).Text = mis
End Sub



Private Sub eve_tag_reset(sta_res As sta_reset)
'evento per la gestione ripristino e reset
Select Case sta_res
Case sta_reset.e_stop
  txt_sta_res = "Emergency Stop"
  txt_sta_res.ForeColor = COL_ALL_Red
  frm_sta_res.BackColor = COL_ALL_Red
Case sta_reset.f_stop
  txt_sta_res = "Fast Stop"
  txt_sta_res.ForeColor = COL_ALL_Red
  frm_sta_res.ForeColor = COL_ALL_Red
Case sta_reset.n_stop
  txt_sta_res = "Normal Stop"
  txt_sta_res.ForeColor = vbBlack
  frm_sta_res.BackColor = COL_DARK_Green
Case sta_reset.rip
  txt_sta_res = msg("msg_rip")
  txt_sta_res.ForeColor = vbBlack
  frm_sta_res.BackColor = COL_DARK_Green
Case sta_reset.ness_reset
  txt_sta_res = ""
  txt_sta_res.ForeColor = vbBlack
  frm_sta_res.BackColor = COL_DARK_Green
End Select
End Sub

Private Sub eve_tag_run(run As Integer)
'evento per le variazioni dello stato di running
Dim s_run As sta_run

s_run = run
Select Case s_run

Case co_in_cor 'CAMBIO ORDINE IN CORSO
  txt_sta_run = msg("msg_co_in_cor")
  txt_sta_run.ForeColor = DARK_BLUE
  txt_sta_run.BackColor = vbWhite
  frm_sta_run.BackColor = DARK_BLUE
  lab_run.BackColor = vbWhite
  lamp_run = False
Case co_com 'CAMBIO ORDINE COMPLETATO
  txt_sta_run = msg("msg_co_com")
  txt_sta_run.ForeColor = DARK_BLUE
  txt_sta_run.BackColor = vbWhite
  frm_sta_run.BackColor = DARK_BLUE
  lab_run.BackColor = vbWhite
  lamp_run = False
Case pre 'MACCHINA IN PREALLARME
  txt_sta_run = msg("msg_pre")
  txt_sta_run.ForeColor = vbWhite
  txt_sta_run.BackColor = COL_DARK_Green
  lab_run.BackColor = COL_DARK_Green
  frm_sta_run.BackColor = COL_DARK_Green
  lamp_run = True
Case pro_cam 'MACCHINA PRONTA AL CAMBIO
  txt_sta_run = msg("msg_pro_cam")
  txt_sta_run.ForeColor = vbBlack
  txt_sta_run.BackColor = vbWhite
  frm_sta_run.BackColor = COL_DARK_Green
  lab_run.BackColor = vbWhite
  lamp_run = False
Case in_mar 'MACCHINA IN MARCIA
  txt_sta_run = msg("msg_in_mar")
  txt_sta_run.ForeColor = vbBlack
  txt_sta_run.BackColor = vbWhite
  frm_sta_run.BackColor = COL_DARK_Green
  lab_run.BackColor = vbWhite
  lamp_run = False
Case pro_mar 'MACCHINA PRONTA ALLA MARCIA
  txt_sta_run = msg("msg_pro_mar")
  txt_sta_run.ForeColor = vbBlack
  txt_sta_run.BackColor = vbWhite
  frm_sta_run.BackColor = COL_DARK_Green
  lab_run.BackColor = vbWhite
  lamp_run = False
Case fer 'MACCHINA FERMA
  txt_sta_run = msg("msg_fer")
  txt_sta_run.ForeColor = COL_ALL_Red
  txt_sta_run.BackColor = vbWhite
  frm_sta_run.BackColor = COL_ALL_Red
  lab_run.BackColor = vbWhite
  lamp_run = False
End Select
End Sub

Private Sub eve_tag_SPDE(ok As Boolean)
If ok Then
  If Not sta_tag.MAC_IN_MANUALE(1) Then
    lin_no_com_spde.Visible = False
  Else
    lin_no_com_spde.Visible = True
    lin_no_com_spde.BorderColor = RED_GRU_ALL
  End If
Else
  If Not sta_tag.MAC_IN_MANUALE(1) Then
    lin_no_com_spde.Visible = True
    lin_no_com_spde.BorderColor = vbBlack
  Else
    lin_no_com_spde.Visible = True
    lin_no_com_spde.BorderColor = RED_GRU_ALL
  End If
End If

If lin_no_com_spde.Visible And lin_no_com_plc.Visible Then
  frm_com.BackColor = COL_ALL_Red
Else
  frm_com.BackColor = COL_DARK_Green
End If
End Sub

Private Sub eve_tag_vel(v As Long)
'varaizione della velocità di linea

txt_vel.Text = IIf(abi_mis_ang, val(con_met_to_fet(CStr(v))), v)
If v > val(IIf(abi_mis_ang, con_fet_to_met(txt_vel_max.Text), txt_vel_max.Text)) Then
  frm_vel.BackColor = COL_ALL_Red
Else
  frm_vel.BackColor = COL_DARK_Green
End If
End Sub

Private Sub eve_tag_velmax(v As Integer)
'velocità massima

txt_vel_max.Text = IIf(abi_mis_ang, val(con_met_to_fet(CStr(v))), v)
If val(IIf(abi_mis_ang, con_fet_to_met(txt_vel.Text), txt_vel.Text)) > v Then
  frm_vel.BackColor = COL_ALL_Red
Else
  frm_vel.BackColor = COL_DARK_Green
End If
End Sub


Private Sub Form_Activate()
txt_idle.SetFocus
End Sub

Private Sub Form_Load()
Dim i As Integer

On Local Error GoTo ges_err

Debug.Print "Inizio Load " & Me.name
Set eve_tag = sta_tag
Set eve_ind_plc = plc_ind
Set eve_ord_mac = ord_mac
'Iciliouff 03-11-2003
Set rmt_sta_bar_plc = New rmt
rmt_sta_bar_plc.set_plc_ute (1)
rmt_sta_bar_plc.name = "rmt_sta_bar_plc"
Set rmt_sta_bar_macro = New rmt
rmt_sta_bar_macro.set_plc_ute (3)
rmt_sta_bar_macro.name = "rmt_sta_bar_ric_plc_auto"

'ABorelli_FI17/11/2006_1
Set Fins = New Fins
Set Fins.sck = Me.sck_Fins
Set Fins.tim = Me.tim_fins
Me.Width = Screen.Width
Me.Left = 0
Me.Height = 1850 * (pro_set.PercY / 100)
Me.Top = Screen.Height - Me.Height
clear_cell Me
dimensiona Me
'nascondo il pannello dei messaggi
SSP_mes_all.Left = sta_bar.Left + sta_bar.Left + sta_bar.Width + 100
'azzero la label dei messaggi
lab_msg.Caption = ""

'Inizializzazione LABEL dei livelli
If val(par_mac.car_tec.num_liv_tag) < 3 Then
  liv(2).BackColor = MEDIUM_GRAY
  lab_liv(2).Caption = ""
  txt_num_sca(2).BackColor = MEDIUM_GRAY
  txt_err(2).BackColor = MEDIUM_GRAY
  txt_sca(2).BackColor = MEDIUM_GRAY
  For i = 0 To 2
    lab_gray_med(i).Visible = True
    lab_gray_med(i).BackColor = MEDIUM_GRAY
  Next i
  If val(par_mac.car_tec.num_liv_tag) = 1 Then
    liv(1).BackColor = MEDIUM_GRAY
    lab_liv(1).Caption = ""
    txt_num_sca(1).BackColor = MEDIUM_GRAY
    txt_err(1).BackColor = MEDIUM_GRAY
    txt_sca(1).BackColor = MEDIUM_GRAY
  End If
Else
  For i = 0 To 2
    lab_gray_med(i).Visible = False
  Next i
End If
Debug.Print "Fine Load " & Me.name
Debug.Print ""

Exit Sub
ges_err:
    Select Case err.Number
    Case 57 ' problema con l'indirizzo finsgateway
      log_str msg("err_sys_plc") & ". Net_ID = " & par_mac.indirizzo_plc.net_id & " Node_ID = " & par_mac.indirizzo_plc.node_id & " Unit_ID = " & par_mac.indirizzo_plc.unit_id & ".", MED_PRIORITY, log_plc, False
      Resume Next
    Case Else
      log_str "on error intervenuta in form load della sta_bar" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
    End Select
End Sub

Private Sub rmt_sta_bar_macro_retbad(err As String)
'Gestisce risposta negativa macro

  log_str "rmt_sta_bar_macro RETBAD", MED_PRIORITY, log_cfg
  If err <> "" Then messaggio err, False
  rmt_sta_bar_macro.resetta

End Sub

Private Sub rmt_sta_bar_macro_retok(s As String)
'Gestisce risposta positiva macro

  log_str "rmt_sta_bar_macro RETOK", MED_PRIORITY, log_cfg
  If s <> "" Then messaggio s, False
  rmt_sta_bar_macro.resetta

End Sub


Private Sub rmt_sta_bar_plc_retbad(err As String)
'Gestione risposta negativa richieste rmt_sta_bar_plc
  
  If rmt_sta_bar_plc.ric = tip_ope_PLC.PARAM_CFG_MAC Then
    'Rollback dell'impostazione
    abi_mis_ang = IIf(abi_mis_ang, False, True)
    setFileINIString "pro_set", "Inch", IIf(abi_mis_ang, "1", "0")
    If err <> "" Then messaggio err, True
  End If
  rmt_sta_bar_plc.resetta

End Sub

Private Sub rmt_sta_bar_plc_retok(s As String)
'Gestisce le risposte positive di rmt_sta_bar_plc

  If s <> "" Then messaggio s, False
  rmt_sta_bar_plc.resetta
  
End Sub


Private Sub sck_sch_tag_DataArrival(Index As Integer, ByVal bytesTotal As Long)
'agrassi_FI08-03-2010_1
'Gestisce gli eventi delle comunicazioni con la scheda di taglio
Dim r As String
'agrassi_FI13-10-2004_1
On Error GoTo g_err
  If par_mac.tip_com_sch_tag(Index + 1) = 1 Then
    If bytesTotal > 0 Then
      sck_sch_tag(Index).GetData r, , bytesTotal
      rx_sch_tag r, Index + 1
    End If
  End If
'agrassi_FI13-10-2004_1
Exit Sub
g_err: log_str "Sck_spde_DataArrival: errore " + err.Description, MED_PRIORITY, log_gen
If (err.Number = 10054) Then Resume Next
End Sub

Private Sub sck_tim_DataArrival(ByVal bytesTotal As Long)
' imposta l' ora a quella del calcolatore che gli ha risposto
Dim s As String, t As String
'andrea 09-07-02 uff
On Error GoTo gesterr
sck_tim.GetData s
If Asc(Right$(s, 1)) = 0 And Asc(Right$(s, 2)) = 10 Then
  t = Mid$(s, Len(s) - 9, 8)
  log_str "Aggiornamento orario da tcpip porta " + sck_tim.RemotePort + " con SPDE: ora " + t + newline, MIN_PRIORITY, log_gen
  Time = t
End If
Exit Sub
gesterr:
'andrea 09-07-02 uff
  log_str "Sck_tim_DATAARRIVAL: Errore numero" + str(err.Description) + ", Descrizione = " + err.Description + ", Sorgente = " + err.Source + ", Ultimo errore Dll" + str(err.LastDllError) + ", File Help = " + err.HelpFile, MED_PRIORITY, log_gen
  log_str "Byte ricevuti " + Format(bytesTotal) + ", stato " + Format(sck_tim.State) + ", stringa ricevuta " + s, MED_PRIORITY, log_gen
  If sck_tim.State <> sckClosed Then sck_tim.Close: log_str "Il socket è stato chiuso", MED_PRIORITY, log_gen
End Sub




Private Sub tim_all_Timer()
'timer per la gestione della visualizzazione dei messaggi e delle informazioni presenti presenti sulla barra di stato
Static tim_out_msg_rul As Long 'serve per memorizzare il time out dei messaggi sulla form set_par_rul
Static tim_out_msg As Long 'serve per memorizzare il time out degli allarmi nella label
Static old_lab_col(1 To 2) As Long
Static old_msg As String      'memorizza il messaggio corrente nella label
Static old_back As Long
Static old_abi_reg_rep_pro As String
Dim i As Integer

'Iciliouff 03-11-2003 Spostata la gestione in snd_sch_tag
''Gestione del TIMEOUT sulla comunicazione con le schede di taglio
'For i = 1 To par_mac.car_tec.num_liv_tag
'  If par_mac.com_sch_tag(i).com_att Then
'    If (GetTickCount() - con_sch_tag(i)) / 1000 > (val(par_mac.com_sch_tag(i).timeout) / 1000) And Not par_mac.com_sch_tag(i).ricevuto Then
'        par_mac.com_sch_tag(i).com_att = False
'        ges_fine_trasmissione i, False
'        If CASO_SND_SCH_TAG <> INVIO_INIZIALE_PAR Then
'          messaggio msg("tim_out_com_sch_tag") & " " & i, True, False, True, i
'          messaggio msg("ope_eff"), False, False
'        End If
'    End If
'  End If
'Next i

If new_msg_rul Then
    new_msg_rul = False
    tim_out_msg_rul = 0
End If

If msg_rul Then
    tim_out_msg_rul = tim_out_msg_rul + 1
    'l'ultimo messaggio resta visualizzato per 6 secondi
    If tim_out_msg_rul = (NUM_SEC_MES + 3000) / Tim_all.Interval Then
      tim_out_msg_rul = 0
      set_par_rul.del_lab
      msg_rul = False
    End If
End If


'Impostazione data e ora correnti
txt_ora.Text = Format$(Time, "hh:mm:ss")
txt_dat.Text = Format$(Date, "dd:mm:yyyy")

'Impostazione dei lampeggiamenti in sta_bar
If lamp_run Then
  If txt_sta_run.BackColor = vbWhite Then
      txt_sta_run.ForeColor = vbWhite
      txt_sta_run.BackColor = old_back
      lab_run.BackColor = old_back
  Else
      old_back = txt_sta_run.BackColor
      txt_sta_run.BackColor = vbWhite
      lab_run.BackColor = vbWhite
      txt_sta_run.ForeColor = vbBlack
  End If
End If
  
'visualizzazione messaggio speciale per interruzione registrazione produzione
'la comunicazione con il PLC deve essere ON e l'abilitazione alla registrazione dei report di produzione deve essere off
If pro_set.abi_reg_rep_pro <> old_abi_reg_rep_pro Then
  If sta_tag.com_OK Then
    If pro_set.abi_reg_rep_pro = STR_FALSE Then
      messag_win msg("reg_pro_int") 'segnalo che la produzione è stata interrotta
    Else
      messag_win "" 'cancello il frame dei messaggi
    End If
  End If
  old_abi_reg_rep_pro = pro_set.abi_reg_rep_pro
End If

'Impostazione dei lampeggiamenti in pag_pro
'If lamp_run Then
'    For i = 1 To 2
'        If pag_pro.lab_ord(i).BackColor = vbWhite Then
'          pag_pro.lab_ord(i).BackColor = old_lab_col(i)
'          pag_pro.lab_ord(i).ForeColor = vbWhite
'        Else
'          old_lab_col(i) = pag_pro.lab_ord(i).BackColor
'          pag_pro.lab_ord(i).BackColor = vbWhite
'          If i = 1 Then
'            pag_pro.lab_ord(i).ForeColor = vbBlack
'          Else
'            pag_pro.lab_ord(i).ForeColor = DARK_GRAY
'          End If
'        End If
'    Next i
'End If

'cancello la stringa degli allarmi utente (label)
If Len(lab_msg.Caption) > 0 And old_msg = lab_msg.Caption Then
  tim_out_msg = tim_out_msg + 1
  'l'ultimo messaggio resta visualizzato per 3 secondi
  If tim_out_msg = NUM_SEC_MES / Tim_all.Interval Then
    tim_out_msg = 0
    lab_msg.Caption = ""
  End If
Else
  tim_out_msg = 0
  old_msg = lab_msg.Caption
End If


'*************************************
'CONTROLLO SUL FOCUS DELLA FORM ATTIVA
'*************************************
If (Screen.ActiveForm.name = sta_bar.name Or Screen.ActiveForm.name = Tasti.name) _
 Or (Not pro_set.ute_wrk) Then
    Call SetForegroundWindow(pag_cor.hwnd) 'restituisco il focus alla form corrente
    'IDisperati_FI 21/08/2006
'    Mouse False 'nascondo il mouse
    If Screen.MousePointer = vbDefault Then Mouse False
End If
  
End Sub

Private Sub tim_com_azi_Timer()
dia_sta_azi.rcv_par_dia_azi
End Sub

Private Sub tim_com_PLC_Timer()

  tim_tag_plc
' ABorelli_FI09/09/2005_1
  sta_tag.tempi False, False, False
  
End Sub


Private Sub Tim_com_spde_lan_Timer()
  com_spde_lan.SendData ""
End Sub

Private Sub tim_com_spde_Timer()
Static ini As Integer
Static cont_ini As Long

'lenzoni uff. UTP 15/07/2002
'esclusione errore di comunicazione con escada e protocollo fosber v1.0
Dim esc_tim_out As Boolean
If UCase(pro_set.per_cli) = "LINPAC ATLANTA" Then
  esc_tim_out = False
Else
  esc_tim_out = True
End If

If ini = 0 Then
    
    If (cont_ini / (1 / (tim_com_spde.Interval / 1000))) > (val(par_mac.com_spde.TimeOut) / 1000) Or sta_tag.MAC_IN_MANUALE(1) _
    Or Not sta_tag.spde_OK Or con_SPDE > 0 Then
        ini = 1
        If sta_tag.MAC_IN_MANUALE(1) Then messaggio msg("mac_in_man"), False, False
    Else
        cont_ini = cont_ini + 1
    End If

Else
  If par_abi.abi_err_com_PU Then   ' ABorelli_FI09/09/2005_1
    If sta_tag.com_OK Then
        'PMASSAROFI_05/07/11 - Aggiunta ricezione ordini da MHI via BCD
        If par_mac.com_spde.ver_spde = MHI10 Then
          con_SPDE = GetTickCount()
        End If
        
        If ((GetTickCount() - con_SPDE) / 1000 > (val(par_mac.com_spde.TimeOut) / 1000) Or sta_tag.MAC_IN_MANUALE(1)) And esc_tim_out Then
          sta_tag.spde_OK = False
          lin_no_com_spde.Visible = True
          If Not sta_tag.MAC_IN_MANUALE(1) Then
            led_spde_rx.FillColor = vbRed
            led_spde_tx.FillColor = vbRed
            lin_no_com_spde.BorderColor = vbBlack
          Else
            led_spde_rx.FillColor = vbRed
            led_spde_tx.FillColor = vbRed
            lin_no_com_spde.BorderColor = RED_GRU_ALL
          End If
        Else
          sta_tag.spde_OK = True
          lin_no_com_spde.Visible = False
        End If
        sta_tag.aggiorna_spde
    Else
        'la raiseevent spde nell'aggiorna la fa solo se la scheda PLC è presente e quindi comunico con la macchina
        'nel caso in cui la scheda non sia presente (e quindi sta_tag.com_OK sia false) forzo l'evento di comunicazione con spde
        'eve_tag_SPDE quando ho raggiunto il timeout
        If (GetTickCount() - con_SPDE) / 1000 > (val(par_mac.com_spde.TimeOut) / 1000) Or sta_tag.MAC_IN_MANUALE(1) Then
              sta_tag.spde_OK = False
        Else
              sta_tag.spde_OK = True
        End If
        eve_tag_SPDE (sta_tag.spde_OK)
    End If
  End If
End If

End Sub




