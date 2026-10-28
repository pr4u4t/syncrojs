VERSION 5.00
Begin VB.Form pag_pro 
   Appearance      =   0  'Flat
   BackColor       =   &H80000005&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6225
   ClientLeft      =   -1755
   ClientTop       =   1410
   ClientWidth     =   11970
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
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   6225
   ScaleMode       =   0  'User
   ScaleWidth      =   11970
   ShowInTaskbar   =   0   'False
   Begin VB.Frame frm_sta 
      Appearance      =   0  'Flat
      BackColor       =   &H00008000&
      BorderStyle     =   0  'None
      ForeColor       =   &H80000008&
      Height          =   420
      Index           =   1
      Left            =   0
      TabIndex        =   53
      Top             =   3315
      Width           =   15000
      Begin VB.Label lab_ord 
         BackColor       =   &H00FFFFFF&
         Caption         =   "Stato ordine corrente"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   280
         Index           =   1
         Left            =   60
         TabIndex        =   54
         Top             =   65
         Width           =   15000
      End
   End
   Begin VB.Frame frm_sta 
      Appearance      =   0  'Flat
      BackColor       =   &H00008000&
      BorderStyle     =   0  'None
      ForeColor       =   &H80000008&
      Height          =   420
      Index           =   2
      Left            =   0
      TabIndex        =   51
      Top             =   332
      Width           =   15000
      Begin VB.Label lab_ord 
         BackColor       =   &H00FFFFFF&
         Caption         =   "Stato ordine successivo"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   280
         Index           =   2
         Left            =   60
         TabIndex        =   52
         Top             =   65
         Width           =   15000
      End
   End
   Begin VB.Frame Frame1 
      BackColor       =   &H00000000&
      BorderStyle     =   0  'None
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   660
      Left            =   0
      TabIndex        =   14
      Top             =   3750
      Width           =   1455
      Begin VB.Image Image1 
         Height          =   600
         Left            =   0
         Picture         =   "pag_pro.frx":0000
         Stretch         =   -1  'True
         Top             =   30
         Width           =   360
      End
      Begin VB.Label Label1 
         Alignment       =   2  'Center
         BackStyle       =   0  'Transparent
         Caption         =   "  Ordine Corrente"
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
         Height          =   615
         Left            =   300
         TabIndex        =   15
         Top             =   90
         Width           =   1185
      End
   End
   Begin VB.Frame Frame2 
      BackColor       =   &H00C0C0C0&
      BorderStyle     =   0  'None
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   660
      Left            =   0
      TabIndex        =   0
      Top             =   765
      Width           =   1455
      Begin VB.Image Image2 
         Height          =   615
         Left            =   0
         Picture         =   "pag_pro.frx":0842
         Stretch         =   -1  'True
         Top             =   0
         Width           =   360
      End
      Begin VB.Label Label53 
         Alignment       =   2  'Center
         BackStyle       =   0  'Transparent
         Caption         =   "Ordine Successivo"
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
         Height          =   615
         Left            =   300
         TabIndex        =   1
         Top             =   90
         Width           =   1185
      End
   End
   Begin VB.Label suc_met_tot 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Left            =   10320
      TabIndex        =   102
      Top             =   1035
      Width           =   1050
   End
   Begin VB.Label Label45 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Lung. totale"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Left            =   10320
      TabIndex        =   101
      Top             =   810
      Width           =   1635
   End
   Begin VB.Line Line17 
      BorderColor     =   &H00C0C0C0&
      X1              =   10275
      X2              =   10275
      Y1              =   795
      Y2              =   1400
   End
   Begin VB.Label cor_met_tot 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Left            =   10320
      TabIndex        =   100
      Top             =   4020
      Width           =   1050
   End
   Begin VB.Label Label43 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Lung. totale"
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
      Height          =   345
      Left            =   10320
      TabIndex        =   99
      Top             =   3795
      Width           =   1635
   End
   Begin VB.Line Line16 
      BorderColor     =   &H00000000&
      X1              =   10275
      X2              =   10275
      Y1              =   3780
      Y2              =   4385
   End
   Begin VB.Label Label40 
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
      Height          =   285
      Left            =   10185
      TabIndex        =   98
      Top             =   750
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Label Label39 
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
      Height          =   285
      Left            =   10185
      TabIndex        =   97
      Top             =   3720
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Label suc_met 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   1
      Left            =   10320
      TabIndex        =   96
      Top             =   2370
      Width           =   1620
   End
   Begin VB.Label suc_met 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   2
      Left            =   10320
      TabIndex        =   95
      Top             =   2850
      Width           =   1620
   End
   Begin VB.Label suc_met 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   0
      Left            =   10320
      TabIndex        =   94
      Top             =   1875
      Width           =   1620
   End
   Begin VB.Label cor_met 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   1
      Left            =   10320
      TabIndex        =   93
      Top             =   5355
      Width           =   1620
   End
   Begin VB.Label cor_met 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   2
      Left            =   10320
      TabIndex        =   92
      Top             =   5835
      Width           =   1620
   End
   Begin VB.Label cor_met 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   0
      Left            =   10320
      TabIndex        =   91
      Top             =   4860
      Width           =   1620
   End
   Begin VB.Label suc_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   1
      Left            =   8580
      TabIndex        =   90
      Top             =   2355
      Width           =   1680
   End
   Begin VB.Label suc_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   2
      Left            =   8580
      TabIndex        =   89
      Top             =   2835
      Width           =   1680
   End
   Begin VB.Label suc_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   0
      Left            =   8580
      TabIndex        =   88
      Top             =   1860
      Width           =   1680
   End
   Begin VB.Label cor_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   0
      Left            =   8580
      TabIndex        =   87
      Top             =   4845
      Width           =   1680
   End
   Begin VB.Label cor_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   2
      Left            =   8580
      TabIndex        =   86
      Top             =   5820
      Width           =   1680
   End
   Begin VB.Label cor_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   1
      Left            =   8580
      TabIndex        =   85
      Top             =   5340
      Width           =   1680
   End
   Begin VB.Label suc_res 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   1
      Left            =   7500
      TabIndex        =   84
      Top             =   2355
      Width           =   1020
   End
   Begin VB.Label suc_res 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   2
      Left            =   7500
      TabIndex        =   83
      Top             =   2835
      Width           =   1020
   End
   Begin VB.Label suc_res 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   0
      Left            =   7500
      TabIndex        =   82
      Top             =   1860
      Width           =   1020
   End
   Begin VB.Label cor_res 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   0
      Left            =   7500
      TabIndex        =   81
      Top             =   4845
      Width           =   1020
   End
   Begin VB.Label cor_res 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   2
      Left            =   7500
      TabIndex        =   80
      Top             =   5820
      Width           =   1020
   End
   Begin VB.Label cor_res 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   1
      Left            =   7500
      TabIndex        =   79
      Top             =   5340
      Width           =   1020
   End
   Begin VB.Label suc_lar_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   1
      Left            =   4275
      TabIndex        =   78
      Top             =   2355
      Width           =   1545
   End
   Begin VB.Label suc_lar_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   2
      Left            =   4275
      TabIndex        =   77
      Top             =   2835
      Width           =   1545
   End
   Begin VB.Label suc_lar_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   0
      Left            =   4275
      TabIndex        =   76
      Top             =   1860
      Width           =   1545
   End
   Begin VB.Label cor_lar_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   0
      Left            =   4275
      TabIndex        =   75
      Top             =   4845
      Width           =   1545
   End
   Begin VB.Label cor_lar_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   2
      Left            =   4275
      TabIndex        =   74
      Top             =   5820
      Width           =   1545
   End
   Begin VB.Label cor_lar_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   1
      Left            =   4275
      TabIndex        =   73
      Top             =   5340
      Width           =   1545
   End
   Begin VB.Label suc_lun_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   1
      Left            =   5895
      TabIndex        =   72
      Top             =   2355
      Width           =   1515
   End
   Begin VB.Label suc_lun_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   2
      Left            =   5895
      TabIndex        =   71
      Top             =   2835
      Width           =   1515
   End
   Begin VB.Label suc_lun_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   0
      Left            =   5895
      TabIndex        =   70
      Top             =   1860
      Width           =   1515
   End
   Begin VB.Label cor_lun_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   0
      Left            =   5895
      TabIndex        =   69
      Top             =   4845
      Width           =   1515
   End
   Begin VB.Label cor_lun_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   2
      Left            =   5895
      TabIndex        =   68
      Top             =   5820
      Width           =   1515
   End
   Begin VB.Label cor_lun_sca 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   1
      Left            =   5895
      TabIndex        =   67
      Top             =   5340
      Width           =   1515
   End
   Begin VB.Label suc_com 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   1
      Left            =   1320
      TabIndex        =   66
      Top             =   2355
      Width           =   2895
   End
   Begin VB.Label suc_com 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   2
      Left            =   1320
      TabIndex        =   65
      Top             =   2835
      Width           =   2895
   End
   Begin VB.Label suc_com 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Index           =   0
      Left            =   1320
      TabIndex        =   64
      Top             =   1860
      Width           =   2895
   End
   Begin VB.Label cor_com 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   0
      Left            =   1320
      TabIndex        =   63
      Top             =   4845
      Width           =   2895
   End
   Begin VB.Label cor_com 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   2
      Left            =   1320
      TabIndex        =   62
      Top             =   5820
      Width           =   2895
   End
   Begin VB.Label cor_com 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Index           =   1
      Left            =   1320
      TabIndex        =   61
      Top             =   5340
      Width           =   2895
   End
   Begin VB.Label suc_liv 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   345
      Index           =   1
      Left            =   30
      TabIndex        =   60
      Top             =   2355
      Width           =   1230
   End
   Begin VB.Label suc_liv 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   345
      Index           =   2
      Left            =   30
      TabIndex        =   59
      Top             =   2835
      Width           =   1230
   End
   Begin VB.Label suc_liv 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   345
      Index           =   0
      Left            =   30
      TabIndex        =   58
      Top             =   1860
      Width           =   1230
   End
   Begin VB.Label cor_liv 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   345
      Index           =   0
      Left            =   30
      TabIndex        =   57
      Top             =   4845
      Width           =   1230
   End
   Begin VB.Label cor_liv 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   345
      Index           =   2
      Left            =   30
      TabIndex        =   56
      Top             =   5820
      Width           =   1230
   End
   Begin VB.Label cor_liv 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   345
      Index           =   1
      Left            =   30
      TabIndex        =   55
      Top             =   5340
      Width           =   1230
   End
   Begin VB.Label Label42 
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
      Height          =   1245
      Left            =   11880
      TabIndex        =   50
      Top             =   4740
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Label Label41 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Lung. formato"
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
      Height          =   315
      Left            =   10305
      TabIndex        =   49
      Top             =   4455
      Width           =   1665
   End
   Begin VB.Label Label38 
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
      Height          =   1245
      Left            =   11880
      TabIndex        =   48
      Top             =   1755
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Label Label18 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Lung. formato"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   10305
      TabIndex        =   47
      Top             =   1470
      Width           =   1665
   End
   Begin VB.Line Line14 
      BorderColor     =   &H00C0C0C0&
      X1              =   10275
      X2              =   10275
      Y1              =   3228
      Y2              =   1755
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
      Height          =   1245
      Left            =   1185
      TabIndex        =   46
      Top             =   4680
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Label Label12 
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
      Height          =   1245
      Left            =   4140
      TabIndex        =   45
      Top             =   4680
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Label Label11 
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
      Height          =   1245
      Left            =   5760
      TabIndex        =   44
      Top             =   4680
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Label Label10 
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
      Height          =   1245
      Left            =   8438
      TabIndex        =   43
      Top             =   4680
      Width           =   90
      WordWrap        =   -1  'True
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
      Height          =   1245
      Left            =   10185
      TabIndex        =   42
      Top             =   4650
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Label Label8 
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
      Height          =   1485
      Left            =   7365
      TabIndex        =   41
      Top             =   4620
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Line Line15 
      BorderColor     =   &H00000000&
      X1              =   10275
      X2              =   10275
      Y1              =   6220
      Y2              =   4770
   End
   Begin VB.Shape Shape6 
      BorderColor     =   &H00000000&
      FillStyle       =   0  'Solid
      Height          =   45
      Left            =   0
      Top             =   5730
      Width           =   12855
   End
   Begin VB.Label Label36 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Tot. scatole"
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
      Height          =   315
      Left            =   8550
      TabIndex        =   40
      Top             =   4455
      Width           =   1635
   End
   Begin VB.Label Label22 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Larghezza"
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
      Height          =   315
      Left            =   4260
      TabIndex        =   39
      Top             =   4455
      Width           =   1365
   End
   Begin VB.Label Label21 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Lunghezza"
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
      Height          =   315
      Left            =   5880
      TabIndex        =   38
      Top             =   4455
      Width           =   1380
   End
   Begin VB.Label Label20 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Commessa"
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
      Height          =   315
      Left            =   1305
      TabIndex        =   37
      Top             =   4455
      Width           =   2250
   End
   Begin VB.Label Label19 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Livello"
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
      Height          =   315
      Left            =   30
      TabIndex        =   36
      Top             =   4455
      Width           =   990
   End
   Begin VB.Line Line10 
      BorderColor     =   &H00000000&
      X1              =   8535
      X2              =   8535
      Y1              =   6220
      Y2              =   4770
   End
   Begin VB.Line Line5 
      BorderColor     =   &H00000000&
      X1              =   5850
      X2              =   5850
      Y1              =   6220
      Y2              =   4770
   End
   Begin VB.Line Line4 
      BorderColor     =   &H00000000&
      X1              =   4230
      X2              =   4230
      Y1              =   6220
      Y2              =   4770
   End
   Begin VB.Line Line3 
      BorderColor     =   &H00000000&
      X1              =   1275
      X2              =   1275
      Y1              =   6220
      Y2              =   4770
   End
   Begin VB.Shape Shape5 
      BorderColor     =   &H00000000&
      FillStyle       =   0  'Solid
      Height          =   75
      Left            =   0
      Top             =   6225
      Width           =   12855
   End
   Begin VB.Shape Shape4 
      BorderColor     =   &H00000000&
      FillStyle       =   0  'Solid
      Height          =   45
      Left            =   0
      Top             =   5250
      Width           =   12855
   End
   Begin VB.Shape Shape3 
      BorderColor     =   &H00000000&
      FillStyle       =   0  'Solid
      Height          =   45
      Left            =   0
      Top             =   4755
      Width           =   12855
   End
   Begin VB.Line Line2 
      BorderColor     =   &H00000000&
      X1              =   7455
      X2              =   7455
      Y1              =   6220
      Y2              =   4770
   End
   Begin VB.Label Label3 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Resa"
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
      Height          =   315
      Left            =   7485
      TabIndex        =   35
      Top             =   4455
      Width           =   705
   End
   Begin VB.Label Label37 
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
      Height          =   1245
      Left            =   10185
      TabIndex        =   34
      Top             =   1650
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Label Label4 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Resa"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   7485
      TabIndex        =   33
      Top             =   1470
      Width           =   705
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
      Height          =   1485
      Left            =   7365
      TabIndex        =   32
      Top             =   1620
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Line Line8 
      BorderColor     =   &H00C0C0C0&
      X1              =   7455
      X2              =   7455
      Y1              =   3235
      Y2              =   1785
   End
   Begin VB.Label Label35 
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
      Height          =   285
      Left            =   11880
      TabIndex        =   31
      Top             =   3750
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Label Label34 
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
      Height          =   285
      Left            =   4855
      TabIndex        =   30
      Top             =   3720
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Label Label33 
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
      Height          =   285
      Left            =   3540
      TabIndex        =   29
      Top             =   3720
      Width           =   90
      WordWrap        =   -1  'True
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
      Height          =   285
      Left            =   11880
      TabIndex        =   28
      Top             =   720
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Label Label27 
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
      Height          =   285
      Left            =   4855
      TabIndex        =   27
      Top             =   720
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Label Label23 
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
      Height          =   285
      Left            =   3540
      TabIndex        =   26
      Top             =   750
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
      Height          =   1245
      Left            =   8438
      TabIndex        =   25
      Top             =   1680
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Label Label15 
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
      Height          =   1245
      Left            =   5760
      TabIndex        =   24
      Top             =   1680
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Label Label14 
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
      Height          =   1245
      Left            =   4140
      TabIndex        =   23
      Top             =   1680
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Label Label13 
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
      Height          =   1245
      Left            =   1185
      TabIndex        =   22
      Top             =   1680
      Width           =   90
      WordWrap        =   -1  'True
   End
   Begin VB.Shape Shape12 
      BorderStyle     =   0  'Transparent
      FillStyle       =   0  'Solid
      Height          =   75
      Left            =   1530
      Top             =   3750
      Width           =   15000
   End
   Begin VB.Shape Shape10 
      BorderStyle     =   0  'Transparent
      FillStyle       =   0  'Solid
      Height          =   75
      Left            =   -60
      Top             =   4365
      Width           =   13035
   End
   Begin VB.Line Line7 
      BorderColor     =   &H00000000&
      X1              =   3630
      X2              =   3630
      Y1              =   3780
      Y2              =   4385
   End
   Begin VB.Line Line6 
      BorderColor     =   &H00000000&
      X1              =   4950
      X2              =   4950
      Y1              =   3780
      Y2              =   4385
   End
   Begin VB.Label Label7 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Codice"
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
      Height          =   345
      Left            =   1545
      TabIndex        =   21
      Top             =   3795
      Width           =   1155
   End
   Begin VB.Label Label6 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Onda"
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
      Height          =   345
      Left            =   3675
      TabIndex        =   20
      Top             =   3795
      Width           =   1155
   End
   Begin VB.Label Label5 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Tipo carta"
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
      Height          =   345
      Left            =   4995
      TabIndex        =   19
      Top             =   3795
      Width           =   1155
   End
   Begin VB.Label cor_cod_ord 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Left            =   1545
      TabIndex        =   18
      Top             =   4020
      Width           =   2070
   End
   Begin VB.Label cor_tip_ond 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Left            =   3675
      TabIndex        =   17
      Top             =   4020
      Width           =   1050
   End
   Begin VB.Label cor_tip_car 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   345
      Left            =   4995
      TabIndex        =   16
      Top             =   4020
      Width           =   5250
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "PRODUZIONE"
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
      TabIndex        =   13
      Top             =   0
      Width           =   7785
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "pag_pro.frx":1084
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
   Begin VB.Shape Shape8 
      BorderStyle     =   0  'Transparent
      FillColor       =   &H00C0C0C0&
      FillStyle       =   0  'Solid
      Height          =   75
      Left            =   1530
      Top             =   765
      Width           =   15000
   End
   Begin VB.Shape Shape7 
      BorderStyle     =   0  'Transparent
      FillColor       =   &H00C0C0C0&
      FillStyle       =   0  'Solid
      Height          =   75
      Left            =   -60
      Top             =   1380
      Width           =   13035
   End
   Begin VB.Line Line35 
      BorderColor     =   &H00C0C0C0&
      X1              =   3630
      X2              =   3630
      Y1              =   795
      Y2              =   1400
   End
   Begin VB.Line Line1 
      BorderColor     =   &H00C0C0C0&
      X1              =   4950
      X2              =   4950
      Y1              =   795
      Y2              =   1400
   End
   Begin VB.Shape Shape9 
      BorderColor     =   &H00C0C0C0&
      FillColor       =   &H00C0C0C0&
      FillStyle       =   0  'Solid
      Height          =   45
      Left            =   0
      Top             =   1770
      Width           =   12855
   End
   Begin VB.Shape Shape1 
      BorderColor     =   &H00C0C0C0&
      FillColor       =   &H00C0C0C0&
      FillStyle       =   0  'Solid
      Height          =   45
      Left            =   0
      Top             =   2265
      Width           =   12855
   End
   Begin VB.Shape Shape2 
      BorderColor     =   &H00C0C0C0&
      FillColor       =   &H00C0C0C0&
      FillStyle       =   0  'Solid
      Height          =   75
      Left            =   0
      Top             =   3225
      Width           =   12855
   End
   Begin VB.Line Line9 
      BorderColor     =   &H00C0C0C0&
      X1              =   1275
      X2              =   1275
      Y1              =   3235
      Y2              =   1785
   End
   Begin VB.Line Line11 
      BorderColor     =   &H00C0C0C0&
      X1              =   4230
      X2              =   4230
      Y1              =   3235
      Y2              =   1785
   End
   Begin VB.Line Line12 
      BorderColor     =   &H00C0C0C0&
      X1              =   5850
      X2              =   5850
      Y1              =   3235
      Y2              =   1785
   End
   Begin VB.Line Line13 
      BorderColor     =   &H00C0C0C0&
      X1              =   8535
      X2              =   8535
      Y1              =   3235
      Y2              =   1785
   End
   Begin VB.Label Label24 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Livello"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   30
      TabIndex        =   12
      Top             =   1470
      Width           =   990
   End
   Begin VB.Label Label25 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Commessa"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   1305
      TabIndex        =   11
      Top             =   1470
      Width           =   2250
   End
   Begin VB.Label Label26 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Lunghezza"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   5880
      TabIndex        =   10
      Top             =   1470
      Width           =   1380
   End
   Begin VB.Label Label28 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Larghezza"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   4260
      TabIndex        =   9
      Top             =   1470
      Width           =   1365
   End
   Begin VB.Label Label29 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Tot. scatole"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   8550
      TabIndex        =   8
      Top             =   1470
      Width           =   1635
   End
   Begin VB.Label Label48 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Codice"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Left            =   1545
      TabIndex        =   7
      Top             =   810
      Width           =   1155
   End
   Begin VB.Label Label30 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Onda"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Left            =   3675
      TabIndex        =   6
      Top             =   810
      Width           =   1155
   End
   Begin VB.Label Label31 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Tipo carta"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Left            =   4995
      TabIndex        =   5
      Top             =   810
      Width           =   1155
   End
   Begin VB.Shape Shape11 
      BorderColor     =   &H00C0C0C0&
      FillColor       =   &H00C0C0C0&
      FillStyle       =   0  'Solid
      Height          =   45
      Left            =   0
      Top             =   2745
      Width           =   12855
   End
   Begin VB.Label suc_cod_ord 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Left            =   1545
      TabIndex        =   4
      Top             =   1035
      Width           =   2070
   End
   Begin VB.Label suc_tip_ond 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Left            =   3675
      TabIndex        =   3
      Top             =   1035
      Width           =   1050
   End
   Begin VB.Label suc_tip_car 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00808080&
      Height          =   345
      Left            =   4995
      TabIndex        =   2
      Top             =   1035
      Width           =   5250
   End
End
Attribute VB_Name = "pag_pro"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Private WithEvents eve_sta As ClasseStato
Attribute eve_sta.VB_VarHelpID = -1
Private WithEvents eve_ord_mac As CollezioneOrdini
Attribute eve_ord_mac.VB_VarHelpID = -1
'Iciliouff 03-11-2003 Rmt locali
Private WithEvents rmt_plc_pag_pro As rmt
Attribute rmt_plc_pag_pro.VB_VarHelpID = -1

Private tem_ord_cor As ClasseOrdine
Private tem_ord_suc As ClasseOrdine




Private Sub set_col(Index As Variant, str As String, in_col As Long, Optional out_col)
'disegna i colori nei frame di stato degli ordini correnti e successivo
Dim out_c As Long
  If Index < frm_sta.LBound Or Index > frm_sta.UBound Then Exit Sub
  If IsMissing(out_col) Then out_c = in_col Else out_c = out_col
  frm_sta(Index).BackColor = out_c 'colore esterno
  lab_ord(Index).BackColor = in_col 'colore interno
  lab_ord(Index).Caption = str 'stringa
  If in_col = vbWhite Then
    lab_ord(Index).ForeColor = vbBlack 'Scrivo in nero su bianco
  Else
    lab_ord(Index).ForeColor = vbWhite 'scrivo in bianco sugli altri colori
  End If
End Sub

Private Sub eve_key_F1(Shift As Integer)

Select Case Shift
  Case 0
  eve_key.chg pag_all
  Case 2  'invio ordine corrente
    If ord_mac.Count >= 1 Then
      If sta_tag.com_OK Then
        log_str "Pag_pro eve_key_F1: Invio manuale ordine corrente. ID Ordine SPDE: " & Trim$(ord_mac.item(1).spde_id) & " ID Ordine STAG: " & Trim$(ord_mac.item(1).stag_id), MED_PRIORITY, log_lis
        'Iciliouff 03-11-2003 Rimpiazzo l'invio ordine
'        snd_str_PLC INVIO_ORD, CORRENTE       'invio dati ordine corrente
        If rmt_plc_pag_pro.esegui(tip_ope_PLC.INVIO_ORD, GetTickCount(), 0, CORRENTE) Then
          messaggio msg("ope_in_cor"), False, False
        Else
          messaggio msg("imp_ese_ope"), False, False
        End If
  '      messaggio msg("ope_eff"), False, False
      Else
        messaggio msg("com_int_con_mac"), True, False
      End If
    Else
      messaggio msg("msg_ord_ass"), True, False
    End If
  Case Else
  
End Select

End Sub

Private Sub eve_key_F12(Shift As Integer)
Select Case Shift
  Case 0
  eve_key.chg frm_debug
  Case Else
End Select
End Sub

Private Sub eve_key_F2(Shift As Integer)

Select Case Shift
  Case 0
    eve_key.chg abilitazioni '6122001 Stagetti
  Case 2  'invio ordine successivo
    If ord_mac.Count > 1 Then
      If sta_tag.com_OK Then
        log_str "Pag_pro eve_key_F2: Invio manuale ordine successivo. ID Ordine SPDE: " & Trim$(ord_mac.item(2).spde_id) & " ID Ordine STAG: " & Trim$(ord_mac.item(2).stag_id), MED_PRIORITY, log_lis
        'Iciliouff 03-11-2003 Rimpiazzo l'invio ordine
'        snd_str_PLC INVIO_ORD, SUCCESSIVO       'invio dati ordine successivo
        If rmt_plc_pag_pro.esegui(tip_ope_PLC.INVIO_ORD, GetTickCount(), 0, SUCCESSIVO) Then
          messaggio msg("ope_in_cor"), False, False
        Else
          messaggio msg("imp_ese_ope"), False, False
        End If
'        messaggio msg("ope_eff"), False, False
      Else
        messaggio msg("com_int_con_mac"), True, False
      End If
    Else
      messaggio msg("msg_ord_ass"), True, False
    End If
  Case Else
  
End Select

End Sub

Private Sub eve_key_F3(Shift As Integer)
Select Case Shift
  Case 0
    If Not (pro_set.ute_wrk_liv1) And Not (pro_set.ute_wrk) Then
      eve_key.chg Pwd_frm, True
    Else
      eve_key.chg set_par_mac '5122001 Stagetti se true viene abilitata la pagina delle impostazioni ingegneristiche
    End If
    pro_set.ute_wrk_liv1 = False
  Case Else
  End Select
End Sub


Private Sub eve_key_F4(Shift As Integer)

Select Case Shift
  Case 0
    eve_key.chg Onde
  Case 2
    If sta_tag.com_OK Then  'richiesta azzeramento contatori
      log_str "Pag_pro eve_key_F2: Invio manuale ordine successivo. ID Ordine SPDE: " & Trim$(ord_mac.item(2).spde_id) & " ID Ordine STAG: " & Trim$(ord_mac.item(2).stag_id), MED_PRIORITY, log_ord
      'Iciliouff 03-11-2003
'      snd_str_PLC AZZERAMENTO_CONTATORI
      If rmt_plc_pag_pro.esegui(tip_ope_PLC.AZZERAMENTO_CONTATORI, GetTickCount(), 0, 0) Then
        messaggio msg("ope_in_cor"), False, False
      Else
        messaggio msg("imp_ese_ope"), False, False
      End If
'      messaggio msg("ope_eff"), False, False
    Else
      messaggio msg("com_int_con_mac"), True, False
    End If
  Case Else
End Select
End Sub

Private Sub eve_key_F5(Shift As Integer)
Select Case Shift
  Case 0
  eve_key.chg Ins_ord
  Case 2
    eve_key.chg Frm_data, keep:=1 '6122001 Stagetti
  Case Else
End Select
End Sub

Private Sub eve_key_F6(Shift As Integer)
'Iciliouff 20-11-2003 Inserimento Db Cut to pattern
  
  Select Case Shift
    Case 0
      'agrassi_FI15-07-08
      'If (par_mac.opt.cut_pat = STR_TRUE) Or (par_mac.opt.cut_pat_auto = STR_TRUE) Then
      
      'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
      'If (par_mac.opt.cut_pat(1) = STR_TRUE) Or (par_mac.opt.cut_pat(2) = STR_TRUE) Or (par_mac.opt.cut_pat(3) = STR_TRUE) Or (par_mac.opt.cut_pat_auto = STR_TRUE) Then
      If (par_mac.opt.cut_pat(1) = STR_TRUE) Or (par_mac.opt.cut_pat(2) = STR_TRUE) Or (par_mac.opt.cut_pat(3) = STR_TRUE) Or (par_mac.opt.cut_pat_auto(1) = STR_TRUE) Or (par_mac.opt.cut_pat_auto(2) = STR_TRUE) Or (par_mac.opt.cut_pat_auto(3) = STR_TRUE) Then
        eve_key.chg Pattern
      Else
        messaggio msg("imp_ese_ope"), True
      End If
    Case Else
  End Select
  
End Sub

Private Sub eve_key_F7(Shift As Integer)

Select Case Shift
  Case 0  'comando di inizio/fine di produzione
  If Not sta_tag.com_OK Then
    messaggio msg("com_int_con_mac"), True, False
  Else
    If pro_set.abi_reg_rep_pro = STR_FALSE Then
      'INIZIO PRODUZIONE
      pro_set.abi_reg_rep_pro = STR_TRUE
      pro_set.ini_prod = STR_TRUE
      setFileINIString "PRO_SET", "ini_prod", pro_set.ini_prod    'scrivo su file
      setFileINIString "PRO_SET", "abi_reg_rep_pro", pro_set.abi_reg_rep_pro   'scrivo su file
      messaggio msg("reg_pro_ini"), False
    Else
      'FINE PRODUZIONE
      If Not sta_tag.MAC_IN_MARCIA(1) And ord_mac.Count > 0 Then  'la linea non deve essere in marcia ed ho almeno un ordine in lista
        ins_ord_ese_per_fine_prod 'inserisco l'ordine nella lista degli eseguiti senza fare lo shift della lista
        sta_tag.rea_dat_ord_pro (True) 'leggo i dati di produzione dell'ordine eseguito
        pro_set.abi_reg_rep_pro = STR_FALSE
        setFileINIString "PRO_SET", "abi_reg_rep_pro", pro_set.abi_reg_rep_pro   'scrivo su file
        messaggio msg("reg_pro_int"), False
      Else
        messaggio msg("imp_ese_ope") & " " & msg("msg_in_mar"), True, False
      End If
    End If
  End If
  Case 2
  
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


Private Sub eve_ord_mac_Refresh(Shift As Boolean)
If ord_mac.Count >= 2 Then
  dis_cor (False)
  dis_suc (False)
Else
  If ord_mac.Count = 1 Then
    dis_cor (False)
    dis_suc (True)
  Else
    dis_cor (True)
    dis_suc (True)
  End If
End If
End Sub





Private Sub eve_sta_ord(eve As sta_ord, ord As tip_ord)

Select Case eve
  
  Case sta_ord.ord_ok
    
    Select Case ord
      Case CORR
        set_col ord, msg("msg_ord_in_lav"), COL_BLUE
      Case SUCC
        set_col ord, msg("msg_ord_all"), COL_DARK_Green
    End Select
    
  Case sta_ord.ord_nok
    set_col ord, msg("msg_ord_non_all"), COL_ALL_Red
  
  Case sta_ord.ord_ass
    set_col ord, msg("msg_ord_ass"), COL_ALL_Red
End Select

End Sub




Private Sub eve_sta_run(run As Integer)
'Dim i As Integer
'
'If run = pre Then 'caso preallarme
'  For i = 1 To 2
'    set_col i, lab_ord(i).Caption, frm_sta(i).BackColor
'  Next i
'End If
End Sub


Private Sub Form_Activate()
If ord_mac.Count > 1 Then
        dis_cor (False)
        dis_suc (False)
    Else
        If ord_mac.Count = 1 Then
            dis_cor (False)
            dis_suc (True)
        Else
            dis_cor (True)
            dis_suc (True)
        End If
    End If
End Sub

Private Sub Form_Load()
    Debug.Print "Inizio Load " & Me.name
    Set eve_sta = sta_tag
    Set eve_key = New key
    Set eve_ord_mac = ord_mac
    'Iciliouff 03-11-2003
    Set rmt_plc_pag_pro = New rmt
    rmt_plc_pag_pro.set_plc_ute (1)
    rmt_plc_pag_pro.name = "rmt_plc_pag_pro"
    eve_key.create Me
    Carica Me
    Debug.Print "Fine Load " & Me.name
    Debug.Print ""
End Sub

Private Sub dis_cor(blank As Boolean)
Dim i As Integer
Dim ord As Tip_Ordine

On Local Error GoTo GEST_ERR:

If blank Then
  cor_cod_ord.Caption = ""
  cor_tip_car.Caption = ""
  cor_tip_ond.Caption = ""
  cor_met_tot.Caption = ""
  For i = 1 To 3
    cor_liv(i - 1).Caption = ""
    cor_com(i - 1).Caption = ""
    cor_lun_sca(i - 1).Caption = ""
    cor_lar_sca(i - 1).Caption = ""
    cor_sca(i - 1).Caption = ""
    cor_res(i - 1).Caption = ""
    cor_met(i - 1).Caption = ""
  Next i
Else
  CopyType ord, ord_mac.item(1).ordine, Len(ord_mac.item(1).ordine)
  Set tem_ord_cor = ord_mac.item(1)
  cor_cod_ord.Caption = tem_ord_cor.cod_ord_abb
  cor_tip_car.Caption = Trim$(ord.tip_car.cod_com_car)
  cor_tip_ond.Caption = Trim$(ord.tip_car.Cod_ond)
  cor_met_tot.Caption = IIf(abi_mis_ang, con_met_to_fet(Trim$(ord.met_tot_ord)), Trim$(ord.met_tot_ord))
  For i = 1 To 3
    If Trim$(ord.ord(i).Ord_liv) <> "" Then
      cor_com(i - 1).Caption = Trim$(ord.ord(i).com.cod_com_ord)
      cor_res(i - 1).Caption = Trim$(ord.ord(i).Sca_per_tag)
      cor_sca(i - 1).Caption = Trim$(ord.ord(i).sca_tot_num)
      Select Case val(ord.ord(i).Ord_liv)
        Case LIV_BAS: cor_liv(i - 1).Caption = Format$(msg("liv_bas"))
        Case LIV_MED: cor_liv(i - 1).Caption = Format$(msg("liv_med"))
        Case LIV_ALT: cor_liv(i - 1).Caption = Format$(msg("liv_alt"))
        Case Else: cor_liv(i - 1).Caption = ""
      End Select
      If abi_mis_ang Then
        cor_lun_sca(i - 1).Caption = con_dmm_to_inc(Trim$(ord.ord(i).sca_lun))
        cor_lar_sca(i - 1).Caption = con_dmm_to_inc(Trim$(ord.ord(i).sca_lar))
      Else
        cor_lun_sca(i - 1).Caption = CStr(val(ord.ord(i).sca_lun) \ TODECMM)
        cor_lar_sca(i - 1).Caption = CStr(val(ord.ord(i).sca_lar) \ TODECMM)
      End If
      cor_met(i - 1).Caption = IIf(abi_mis_ang, con_met_to_fet(Trim$(ord.ord(i).met)), Trim$(ord.ord(i).met))
    Else
      cor_com(i - 1).Caption = ""
      cor_res(i - 1).Caption = ""
      cor_sca(i - 1).Caption = ""
      cor_lun_sca(i - 1).Caption = ""
      cor_lar_sca(i - 1).Caption = ""
      cor_met(i - 1).Caption = ""
      cor_liv(i - 1).Caption = ""
    End If
  Next i
End If
Exit Sub

GEST_ERR:
If err.Number = 6 Then 'overflow
    log_str "on error intervenuta in dis_cor della form pag_pro" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
    Resume Next
Else
    log_str "on error intervenuta in dis_cor della form pag_pro" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
End If

End Sub

Private Sub dis_suc(blank As Boolean)
Dim i As Integer
Dim ord As Tip_Ordine

If blank Then
    suc_cod_ord.Caption = ""
    suc_tip_car.Caption = ""
    suc_tip_ond.Caption = ""
    suc_met_tot.Caption = ""
    For i = 1 To 3
      suc_liv(i - 1).Caption = ""
      suc_com(i - 1).Caption = ""
      suc_lun_sca(i - 1).Caption = ""
      suc_lar_sca(i - 1).Caption = ""
      suc_sca(i - 1).Caption = ""
      suc_res(i - 1).Caption = ""
      suc_met(i - 1).Caption = ""
    Next i
Else
    CopyType ord, ord_mac.item(2).ordine, Len(ord_mac.item(2).ordine)
    Set tem_ord_suc = ord_mac.item(2)
    suc_cod_ord = Trim$(tem_ord_suc.cod_ord_abb)
    suc_tip_car = Trim$(ord.tip_car.cod_com_car)
    suc_tip_ond = Trim$(ord.tip_car.Cod_ond)
    suc_met_tot = IIf(abi_mis_ang, con_met_to_fet(Trim$(ord.met_tot_ord)), Trim$(ord.met_tot_ord))
    For i = 1 To 3
      If Trim$(ord.ord(i).Ord_liv) <> "" Then
        suc_com(i - 1).Caption = Trim$(ord.ord(i).com.cod_com_ord)
        suc_res(i - 1).Caption = Trim$(ord.ord(i).Sca_per_tag)
        suc_sca(i - 1).Caption = Trim$(ord.ord(i).sca_tot_num)
        Select Case val(ord.ord(i).Ord_liv)
          Case LIV_BAS: suc_liv(i - 1).Caption = Format$(msg("liv_bas"))
          Case LIV_MED: suc_liv(i - 1).Caption = Format$(msg("liv_med"))
          Case LIV_ALT: suc_liv(i - 1).Caption = Format$(msg("liv_alt"))
          Case Else: suc_liv(i - 1).Caption = ""
        End Select
        If abi_mis_ang Then
          suc_lun_sca(i - 1).Caption = con_dmm_to_inc(Trim$(ord.ord(i).sca_lun))
          suc_lar_sca(i - 1).Caption = con_dmm_to_inc(Trim$(ord.ord(i).sca_lar))
        Else
          suc_lun_sca(i - 1).Caption = CStr(val(ord.ord(i).sca_lun) \ TODECMM)
          suc_lar_sca(i - 1).Caption = CStr(val(ord.ord(i).sca_lar) \ TODECMM)
        End If
        suc_met(i - 1).Caption = IIf(abi_mis_ang, con_met_to_fet(Trim$(ord.ord(i).met)), Trim$(ord.ord(i).met))
      Else
        suc_com(i - 1).Caption = ""
        suc_res(i - 1).Caption = ""
        suc_sca(i - 1).Caption = ""
        suc_lun_sca(i - 1).Caption = ""
        suc_lar_sca(i - 1).Caption = ""
        suc_met(i - 1).Caption = ""
        suc_liv(i - 1).Caption = ""
      End If
    Next i
End If
End Sub



Private Sub rmt_plc_pag_pro_retbad(err As String)
'Risposta negativa a comando verso il PLC
  
  If err <> "" Then messaggio err, True
  rmt_plc_pag_pro.resetta
  
End Sub

Private Sub rmt_plc_pag_pro_retok(s As String)
'Risposta affermativa a comando verso il PLC
  
  If s <> "" Then messaggio s, False
  rmt_plc_pag_pro.resetta
  
End Sub


