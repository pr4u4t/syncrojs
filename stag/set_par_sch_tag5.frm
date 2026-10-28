VERSION 5.00
Begin VB.Form set_par_sch_tag5 
   Appearance      =   0  'Flat
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   7830
   ClientLeft      =   -150
   ClientTop       =   630
   ClientWidth     =   11880
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   7830
   ScaleMode       =   0  'User
   ScaleWidth      =   20362.06
   ShowInTaskbar   =   0   'False
   Begin VB.Frame Frame2 
      BackColor       =   &H00FFFFFF&
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00008000&
      Height          =   5890
      Index           =   1
      Left            =   5985
      TabIndex        =   47
      Top             =   330
      Width           =   5985
      Begin VB.TextBox txt_sog_lun_sup_3_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3675
         MaxLength       =   4
         TabIndex        =   98
         TabStop         =   0   'False
         Top             =   4920
         Width           =   915
      End
      Begin VB.TextBox txt_sog_lun_sup_3_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2745
         MaxLength       =   4
         TabIndex        =   20
         Top             =   4920
         Width           =   915
      End
      Begin VB.TextBox txt_tem_att_cam_pre_rul_no_crh_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2745
         MaxLength       =   4
         TabIndex        =   21
         Top             =   5310
         Width           =   915
      End
      Begin VB.TextBox txt_sog_lun_3_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2745
         MaxLength       =   4
         TabIndex        =   19
         Top             =   4525
         Width           =   915
      End
      Begin VB.TextBox txt_sog_vel_3_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2745
         MaxLength       =   4
         TabIndex        =   18
         Top             =   4100
         Width           =   915
      End
      Begin VB.TextBox txt_tem_att_cam_pre_rul_no_crh_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3675
         MaxLength       =   4
         TabIndex        =   77
         TabStop         =   0   'False
         Top             =   5310
         Width           =   915
      End
      Begin VB.TextBox txt_sog_lun_3_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3675
         MaxLength       =   4
         TabIndex        =   76
         TabStop         =   0   'False
         Top             =   4525
         Width           =   915
      End
      Begin VB.TextBox txt_sog_vel_3_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3675
         MaxLength       =   4
         TabIndex        =   75
         TabStop         =   0   'False
         Top             =   4100
         Width           =   915
      End
      Begin VB.TextBox txt_sog_lun_2_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3675
         MaxLength       =   4
         TabIndex        =   74
         TabStop         =   0   'False
         Top             =   3675
         Width           =   915
      End
      Begin VB.TextBox txt_sog_lun_2_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2745
         MaxLength       =   4
         TabIndex        =   17
         Top             =   3675
         Width           =   915
      End
      Begin VB.TextBox txt_sog_vel_2_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3675
         MaxLength       =   4
         TabIndex        =   72
         TabStop         =   0   'False
         Top             =   3250
         Width           =   915
      End
      Begin VB.TextBox txt_sog_vel_2_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2745
         MaxLength       =   4
         TabIndex        =   16
         Top             =   3250
         Width           =   915
      End
      Begin VB.TextBox txt_min_lun_sca_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2745
         MaxLength       =   4
         TabIndex        =   13
         Top             =   1975
         Width           =   915
      End
      Begin VB.TextBox txt_min_lun_sca_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3675
         MaxLength       =   4
         TabIndex        =   59
         TabStop         =   0   'False
         Top             =   1975
         Width           =   915
      End
      Begin VB.TextBox txt_sog_lun_1_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3675
         MaxLength       =   4
         TabIndex        =   58
         TabStop         =   0   'False
         Top             =   2825
         Width           =   915
      End
      Begin VB.TextBox txt_sog_lun_1_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2745
         MaxLength       =   4
         TabIndex        =   15
         Top             =   2825
         Width           =   915
      End
      Begin VB.TextBox txt_sog_vel_1_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3675
         MaxLength       =   4
         TabIndex        =   57
         TabStop         =   0   'False
         Top             =   2400
         Width           =   915
      End
      Begin VB.TextBox txt_sog_vel_1_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2745
         MaxLength       =   4
         TabIndex        =   14
         Top             =   2400
         Width           =   915
      End
      Begin VB.TextBox txt_max_lun_sca_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3675
         MaxLength       =   4
         TabIndex        =   50
         TabStop         =   0   'False
         Top             =   1550
         Width           =   915
      End
      Begin VB.TextBox txt_max_lun_sca_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2745
         MaxLength       =   4
         TabIndex        =   12
         Top             =   1550
         Width           =   915
      End
      Begin VB.TextBox txt_tem_sca_pre_chi_rul_no_crh_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3675
         MaxLength       =   4
         TabIndex        =   49
         TabStop         =   0   'False
         Top             =   1125
         Width           =   915
      End
      Begin VB.TextBox txt_tem_sca_pre_chi_rul_no_crh_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2745
         MaxLength       =   4
         TabIndex        =   11
         Top             =   1125
         Width           =   915
      End
      Begin VB.TextBox txt_tem_sca_pre_ape_rul_no_crh_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3675
         MaxLength       =   4
         TabIndex        =   48
         TabStop         =   0   'False
         Top             =   700
         Width           =   915
      End
      Begin VB.TextBox txt_tem_sca_pre_ape_rul_no_crh_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2745
         MaxLength       =   4
         TabIndex        =   10
         Top             =   700
         Width           =   915
      End
      Begin VB.Label Label2 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Lung.Taglio sopra a soglia 3"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   100
         Top             =   4920
         Width           =   2685
      End
      Begin VB.Label Label41 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mm"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   10
         Left            =   4650
         TabIndex        =   99
         Top             =   4920
         Width           =   765
      End
      Begin VB.Label Label41 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mm"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   9
         Left            =   4650
         TabIndex        =   97
         Top             =   4525
         Width           =   765
      End
      Begin VB.Label Label41 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mm"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   8
         Left            =   4650
         TabIndex        =   96
         Top             =   3675
         Width           =   765
      End
      Begin VB.Label Label41 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mm"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   7
         Left            =   4650
         TabIndex        =   95
         Top             =   2825
         Width           =   765
      End
      Begin VB.Label Label41 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mm"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   6
         Left            =   4650
         TabIndex        =   94
         Top             =   1975
         Width           =   765
      End
      Begin VB.Label Label41 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mm"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   5
         Left            =   4650
         TabIndex        =   93
         Top             =   1550
         Width           =   765
      End
      Begin VB.Label Label29 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "msec"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   6
         Left            =   4650
         TabIndex        =   88
         Top             =   5310
         Width           =   765
      End
      Begin VB.Label Label29 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "msec"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   1
         Left            =   4650
         TabIndex        =   83
         Top             =   1125
         Width           =   765
      End
      Begin VB.Label Label28 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mt/min"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   2
         Left            =   4650
         TabIndex        =   82
         Top             =   4100
         Width           =   500
      End
      Begin VB.Label Label28 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mt/min"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   1
         Left            =   4650
         TabIndex        =   81
         Top             =   3250
         Width           =   500
      End
      Begin VB.Label Label24 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "T. attiv.cambio press.rullo NoCrash"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   80
         Top             =   5310
         Width           =   2685
      End
      Begin VB.Label Label23 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Lung.Taglio fino a soglia 3"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   79
         Top             =   4525
         Width           =   2685
      End
      Begin VB.Label Label22 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Soglia velocità 3"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   78
         Top             =   4100
         Width           =   2685
      End
      Begin VB.Label Label20 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Lung.Taglio fino a soglia 2"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   73
         Top             =   3675
         Width           =   2685
      End
      Begin VB.Label Label14 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Soglia velocità 2"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   71
         Top             =   3250
         Width           =   2685
      End
      Begin VB.Label Label15 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Minima lunghezza scartabile"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   63
         Top             =   1975
         Width           =   2325
      End
      Begin VB.Label Label57 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Soglia velocità 1"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   62
         Top             =   2400
         Width           =   2685
      End
      Begin VB.Label Label26 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Lung.Taglio fino a soglia 1"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   61
         Top             =   2825
         Width           =   2685
      End
      Begin VB.Label Label28 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mt/min"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   0
         Left            =   4650
         TabIndex        =   60
         Top             =   2400
         Width           =   500
      End
      Begin VB.Label Label9 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "DEFAULT"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Left            =   3785
         TabIndex        =   56
         Top             =   300
         Width           =   705
      End
      Begin VB.Label Label8 
         Alignment       =   2  'Center
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "ATTIVI"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Left            =   2895
         TabIndex        =   55
         Top             =   300
         Width           =   645
      End
      Begin VB.Label Label29 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "msec"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   0
         Left            =   4650
         TabIndex        =   54
         Top             =   700
         Width           =   765
      End
      Begin VB.Label Label12 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Massima lunghezza scartabile"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   53
         Top             =   1550
         Width           =   2565
      End
      Begin VB.Label Label21 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "T. cambio press.disc.NoCrash"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   52
         Top             =   1125
         Width           =   2565
      End
      Begin VB.Label Label7 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "T. cambio press.salita NoCrash"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   51
         Top             =   700
         Width           =   2685
      End
   End
   Begin VB.TextBox txt_idle 
      Alignment       =   1  'Right Justify
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Left            =   660
      TabIndex        =   34
      Top             =   6570
      Width           =   645
   End
   Begin VB.Frame Frame3 
      BackColor       =   &H00FFFFFF&
      Caption         =   "PARAMETRI GRUPPO DI SCARTO PER LIVELLO DI TAGLIO"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   5890
      Left            =   0
      TabIndex        =   22
      Top             =   330
      Width           =   5970
      Begin VB.TextBox txt_tem_chi_rul_no_crh_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3690
         MaxLength       =   4
         TabIndex        =   70
         TabStop         =   0   'False
         Top             =   5375
         Width           =   915
      End
      Begin VB.TextBox txt_tem_chi_rul_no_crh_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2760
         MaxLength       =   4
         TabIndex        =   9
         Top             =   5375
         Width           =   915
      End
      Begin VB.TextBox txt_tem_ape_rul_no_crh_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3690
         MaxLength       =   4
         TabIndex        =   68
         TabStop         =   0   'False
         Top             =   4950
         Width           =   915
      End
      Begin VB.TextBox txt_tem_ape_rul_no_crh_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2760
         MaxLength       =   4
         TabIndex        =   8
         Top             =   4950
         Width           =   915
      End
      Begin VB.TextBox txt_tem_chi_flp_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3690
         MaxLength       =   4
         TabIndex        =   45
         TabStop         =   0   'False
         Top             =   4525
         Width           =   915
      End
      Begin VB.TextBox txt_tem_chi_flp_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2760
         MaxLength       =   4
         TabIndex        =   7
         Top             =   4525
         Width           =   915
      End
      Begin VB.TextBox txt_tem_ape_flp_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3690
         MaxLength       =   4
         TabIndex        =   43
         TabStop         =   0   'False
         Top             =   4100
         Width           =   915
      End
      Begin VB.TextBox txt_tem_ape_flp_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2760
         MaxLength       =   4
         TabIndex        =   6
         Top             =   4100
         Width           =   915
      End
      Begin VB.TextBox txt_dis_lam_rul_no_crh_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2760
         MaxLength       =   4
         TabIndex        =   5
         Top             =   3675
         Width           =   915
      End
      Begin VB.TextBox txt_dis_lam_rul_no_crh_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3690
         MaxLength       =   4
         TabIndex        =   41
         TabStop         =   0   'False
         Top             =   3675
         Width           =   915
      End
      Begin VB.TextBox txt_dis_lam_flp_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2760
         MaxLength       =   4
         TabIndex        =   4
         Top             =   3250
         Width           =   915
      End
      Begin VB.TextBox txt_dis_lam_flp_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3690
         MaxLength       =   4
         TabIndex        =   39
         TabStop         =   0   'False
         Top             =   3250
         Width           =   915
      End
      Begin VB.Frame Frame4 
         Appearance      =   0  'Flat
         BackColor       =   &H00B5717B&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   580
         Left            =   0
         TabIndex        =   35
         Top             =   525
         Width           =   2715
         Begin VB.TextBox txt_liv_tag 
            Alignment       =   2  'Center
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
            Height          =   540
            Left            =   2370
            TabIndex        =   36
            Text            =   "00000"
            Top             =   10
            Width           =   330
         End
         Begin VB.Label Label16 
            BackColor       =   &H00800000&
            BackStyle       =   0  'Transparent
            Caption         =   "Livello di taglio"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   15
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00FFFFFF&
            Height          =   345
            Left            =   90
            TabIndex        =   37
            Top             =   120
            Width           =   2175
         End
      End
      Begin VB.TextBox txt_sov_tap_usc_per_sca_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2760
         MaxLength       =   4
         TabIndex        =   0
         Top             =   1550
         Width           =   915
      End
      Begin VB.TextBox txt_sov_tap_usc_per_sca_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3690
         MaxLength       =   4
         TabIndex        =   32
         TabStop         =   0   'False
         Top             =   1550
         Width           =   915
      End
      Begin VB.TextBox txt_dis_sen_lam_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3690
         MaxLength       =   8
         TabIndex        =   28
         TabStop         =   0   'False
         Top             =   2825
         Width           =   915
      End
      Begin VB.TextBox txt_dis_sen_lam_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2760
         MaxLength       =   8
         TabIndex        =   3
         Top             =   2825
         Width           =   915
      End
      Begin VB.TextBox txt_fin_sca_fin_giu_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3690
         MaxLength       =   4
         TabIndex        =   27
         TabStop         =   0   'False
         Top             =   2400
         Width           =   915
      End
      Begin VB.TextBox txt_fin_sca_fin_giu_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2760
         MaxLength       =   4
         TabIndex        =   2
         Top             =   2400
         Width           =   915
      End
      Begin VB.TextBox txt_fin_sca_pri_giu_def 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00808080&
         Height          =   405
         Left            =   3690
         MaxLength       =   4
         TabIndex        =   26
         TabStop         =   0   'False
         Top             =   1975
         Width           =   915
      End
      Begin VB.TextBox txt_fin_sca_pri_giu_att 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2760
         MaxLength       =   4
         TabIndex        =   1
         Top             =   1975
         Width           =   915
      End
      Begin VB.Label Label41 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mm"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   4
         Left            =   4665
         TabIndex        =   92
         Top             =   3675
         Width           =   765
      End
      Begin VB.Label Label41 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mm"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   3
         Left            =   4665
         TabIndex        =   91
         Top             =   3250
         Width           =   765
      End
      Begin VB.Label Label41 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mm"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   2
         Left            =   4665
         TabIndex        =   90
         Top             =   2825
         Width           =   765
      End
      Begin VB.Label Label41 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mm"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   1
         Left            =   4665
         TabIndex        =   89
         Top             =   2400
         Width           =   765
      End
      Begin VB.Label Label29 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "msec"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   5
         Left            =   4665
         TabIndex        =   87
         Top             =   5375
         Width           =   765
      End
      Begin VB.Label Label29 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "msec"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   4
         Left            =   4665
         TabIndex        =   86
         Top             =   4950
         Width           =   765
      End
      Begin VB.Label Label29 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "msec"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   3
         Left            =   4665
         TabIndex        =   85
         Top             =   4525
         Width           =   765
      End
      Begin VB.Label Label29 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "msec"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   2
         Left            =   4665
         TabIndex        =   84
         Top             =   4100
         Width           =   765
      End
      Begin VB.Label Label13 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "T. chiusura Rullo No-Crash"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   69
         Top             =   5375
         Width           =   2685
      End
      Begin VB.Label Label5 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "T. apertura Rullo No-Crash"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   67
         Top             =   4950
         Width           =   2685
      End
      Begin VB.Label Label11 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "%"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   5565
         TabIndex        =   66
         Top             =   1550
         Width           =   300
      End
      Begin VB.Label Label10 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "/ 10 ="
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   4680
         TabIndex        =   65
         Top             =   1550
         Width           =   465
      End
      Begin VB.Label lab_txt_sov_tap_usc_per_sca 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "99,9"
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
         Left            =   5190
         TabIndex        =   64
         Top             =   1550
         Width           =   420
      End
      Begin VB.Label Label30 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "T. chiusura flap"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   46
         Top             =   4525
         Width           =   2685
      End
      Begin VB.Label Label3 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "T. apertura flap"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   44
         Top             =   4100
         Width           =   2325
      End
      Begin VB.Label Label41 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mm"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   0
         Left            =   4665
         TabIndex        =   42
         Top             =   1975
         Width           =   765
      End
      Begin VB.Label Label1 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Distanza lame->Flap di Scarto"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   40
         Top             =   3250
         Width           =   2325
      End
      Begin VB.Label Label55 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Sovrav. Sandwich in fase di scarto"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   33
         Top             =   1550
         Width           =   2640
      End
      Begin VB.Label Label31 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Distanza segnale giunta-> lame"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   31
         Top             =   2825
         Width           =   2325
      End
      Begin VB.Label Label47 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Finestra Scarto dopo la giunta"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   30
         Top             =   2400
         Width           =   2625
      End
      Begin VB.Label Label54 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Finestra Scarto prima della giunta"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   29
         Top             =   1975
         Width           =   2625
      End
      Begin VB.Label Label17 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "DEFAULT"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Left            =   3800
         TabIndex        =   25
         Top             =   1185
         Width           =   705
      End
      Begin VB.Label Label18 
         Alignment       =   2  'Center
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "ATTIVI"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Left            =   2895
         TabIndex        =   24
         Top             =   1185
         Width           =   645
      End
      Begin VB.Label Label6 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Distanza lame->Rullo No Crash"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   23
         Top             =   3675
         Width           =   2625
      End
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "PARAMETRI CONTROLLO TAGLIO  PAG. 5/5"
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
      TabIndex        =   38
      Top             =   0
      Width           =   7785
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "set_par_sch_tag5.frx":0000
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "set_par_sch_tag5"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
'Iciliouff 03-11-2003
Private WithEvents rmt_set_par_sch As rmt
Attribute rmt_set_par_sch.VB_VarHelpID = -1

Const sec_liv = "sch_tag_liv_"



Sub att_in_def(tot As Boolean)
Dim i As Integer
  
  'scrittura nelle textbox
  txt_sov_tap_usc_per_sca_def = txt_sov_tap_usc_per_sca_att
  txt_fin_sca_pri_giu_def = txt_fin_sca_pri_giu_att
  txt_fin_sca_fin_giu_def = txt_fin_sca_fin_giu_att
  txt_dis_sen_lam_def = txt_dis_sen_lam_att
  txt_dis_lam_flp_def = txt_dis_lam_flp_att
  txt_dis_lam_rul_no_crh_def = txt_dis_lam_rul_no_crh_att
  txt_tem_ape_flp_def = txt_tem_ape_flp_att
  txt_tem_chi_flp_def = txt_tem_chi_flp_att
  txt_tem_ape_rul_no_crh_def = txt_tem_ape_rul_no_crh_att
  txt_tem_chi_rul_no_crh_def = txt_tem_chi_rul_no_crh_att
  txt_tem_sca_pre_ape_rul_no_crh_def = txt_tem_sca_pre_ape_rul_no_crh_att
  txt_tem_sca_pre_chi_rul_no_crh_def = txt_tem_sca_pre_chi_rul_no_crh_att
  txt_max_lun_sca_def = txt_max_lun_sca_att
  txt_min_lun_sca_def = txt_min_lun_sca_def
  txt_sog_vel_1_def = txt_sog_vel_1_att
  txt_sog_lun_1_def = txt_sog_lun_1_att
  txt_sog_vel_2_def = txt_sog_vel_2_att
  txt_sog_lun_2_def = txt_sog_lun_2_att
  txt_sog_vel_3_def = txt_sog_vel_3_att
  txt_sog_lun_3_def = txt_sog_lun_3_att
  'RFAZZI_FI27/03/2008
  txt_sog_lun_sup_3_def = txt_sog_lun_sup_3_att
  txt_tem_att_cam_pre_rul_no_crh_def = txt_tem_att_cam_pre_rul_no_crh_att
  
  'scrittura delle variabili
  If tot Then par_mac_ute.sch_tag_def = par_mac_ute.sch_tag Else scr_par_def_ute_liv (txt_liv_tag.Text)
  
  'gestione comunicazioni
  If tot Then par_mac.sch_tag_def = par_mac_ute.sch_tag_def Else par_mac.sch_tag_def.sch_tag_liv(txt_liv_tag.Text) = par_mac_ute.sch_tag_def.sch_tag_liv(txt_liv_tag.Text)
  
  'scrittura nell'ini
  If tot Then
    For i = 1 To par_mac.car_tec.num_liv_tag
      scr_par_sch_tag_def_liv_ini i
    Next i
    If Me.name = form_att Then
        set_par_sch_tag1.att_in_def (True)
        'RFAZZI_FI19/03/2008
        set_par_sch_tag2.att_in_def (True)
        set_par_sch_tag4.att_in_def (True)
    End If
  Else
    scr_par_sch_tag_def_liv_ini txt_liv_tag
  End If

End Sub


Sub fil_par_sch_tag_liv(liv As Integer)

'caricamento dei parametri nella form scheda di taglio per livello

txt_sov_tap_usc_per_sca_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).sov_tap_usc_per_sca)
lab_txt_sov_tap_usc_per_sca = val(txt_sov_tap_usc_per_sca_att) / 10
txt_fin_sca_pri_giu_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).sca_pri_giu)
txt_fin_sca_fin_giu_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).sca_dop_giu)
txt_dis_sen_lam_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).dis_sen_lam)
txt_dis_lam_flp_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).dis_lam_flp)
txt_dis_lam_rul_no_crh_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).dis_lam_rul_no_crh)
txt_tem_ape_flp_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).tem_ape_flp)
txt_tem_chi_flp_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).tem_chi_flp)
txt_tem_ape_rul_no_crh_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).tem_ape_rul_no_crh)
txt_tem_chi_rul_no_crh_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).tem_chi_rul_no_crh)
txt_tem_sca_pre_ape_rul_no_crh_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).tem_sca_pre_ape_rul_no_crh)
txt_tem_sca_pre_chi_rul_no_crh_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).tem_sca_pre_chi_rul_no_crh)
txt_max_lun_sca_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).max_lun_sca)
txt_min_lun_sca_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).min_lun_sca)
txt_sog_vel_1_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_vel_1)
txt_sog_lun_1_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_lun_1)
txt_sog_vel_2_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_vel_2)
txt_sog_lun_2_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_lun_2)
txt_sog_vel_3_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_vel_3)
txt_sog_lun_3_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_lun_3)
'RFAZZI_FI27/03/2008
txt_sog_lun_sup_3_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_lun_sup_3)
txt_tem_att_cam_pre_rul_no_crh_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).tem_att_cam_pre_rul_no_crh)

End Sub


Sub fil_par_sch_tag_def_liv(liv As Integer)
'caricamento dei parametri nella form scheda di taglio per livello

txt_sov_tap_usc_per_sca_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).sov_tap_usc_per_sca)
lab_txt_sov_tap_usc_per_sca = val(txt_sov_tap_usc_per_sca_att) / 10
txt_fin_sca_pri_giu_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).sca_pri_giu)
txt_fin_sca_fin_giu_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).sca_dop_giu)
txt_dis_sen_lam_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).dis_sen_lam)
txt_dis_lam_flp_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).dis_lam_flp)
txt_dis_lam_rul_no_crh_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).dis_lam_rul_no_crh)
txt_tem_ape_flp_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).tem_ape_flp)
txt_tem_chi_flp_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).tem_chi_flp)
txt_tem_ape_rul_no_crh_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).tem_ape_rul_no_crh)
txt_tem_chi_rul_no_crh_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).tem_chi_rul_no_crh)
txt_tem_sca_pre_ape_rul_no_crh_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).tem_sca_pre_ape_rul_no_crh)
txt_tem_sca_pre_chi_rul_no_crh_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).tem_sca_pre_chi_rul_no_crh)
txt_max_lun_sca_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).max_lun_sca)
txt_min_lun_sca_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).min_lun_sca)
txt_sog_vel_1_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_vel_1)
txt_sog_lun_1_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_lun_1)
txt_sog_vel_2_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_vel_2)
txt_sog_lun_2_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_lun_2)
txt_sog_vel_3_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_vel_3)
txt_sog_lun_3_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_lun_3)
'RFAZZI_FI27/03/2008
txt_sog_lun_sup_3_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_lun_sup_3)
txt_tem_att_cam_pre_rul_no_crh_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).tem_att_cam_pre_rul_no_crh)

End Sub

Sub scr_par_att_ute_liv(liv As Integer)


'PARAMETRI PER LIVELLO DI TAGLIO
  'RFAZZI_FI 02-01-08
  par_mac_ute.sch_tag.sch_tag_liv(liv).sov_tap_usc_per_sca = txt_sov_tap_usc_per_sca_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).sca_pri_giu = txt_fin_sca_pri_giu_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).sca_dop_giu = txt_fin_sca_fin_giu_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).dis_sen_lam = txt_dis_sen_lam_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).dis_lam_flp = txt_dis_lam_flp_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).dis_lam_rul_no_crh = txt_dis_lam_rul_no_crh_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).tem_ape_flp = txt_tem_ape_flp_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).tem_chi_flp = txt_tem_chi_flp_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).tem_ape_rul_no_crh = txt_tem_ape_rul_no_crh_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).tem_chi_rul_no_crh = txt_tem_chi_rul_no_crh_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).tem_sca_pre_ape_rul_no_crh = txt_tem_sca_pre_ape_rul_no_crh_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).tem_sca_pre_chi_rul_no_crh = txt_tem_sca_pre_chi_rul_no_crh_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).max_lun_sca = txt_max_lun_sca_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).min_lun_sca = txt_min_lun_sca_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).sog_vel_1 = txt_sog_vel_1_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).sog_lun_1 = txt_sog_lun_1_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).sog_vel_2 = txt_sog_vel_2_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).sog_lun_2 = txt_sog_lun_2_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).sog_vel_3 = txt_sog_vel_3_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).sog_lun_3 = txt_sog_lun_3_att
  'RFAZZI_FI27/03/2008
  par_mac_ute.sch_tag.sch_tag_liv(liv).sog_lun_sup_3 = txt_sog_lun_sup_3_att
  
  par_mac_ute.sch_tag.sch_tag_liv(liv).tem_att_cam_pre_rul_no_crh = txt_tem_att_cam_pre_rul_no_crh_att
  
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


Private Sub rmt_set_par_sch_retbad(err As String)
'Gestione risposta negativa da scheda di taglio
  
  If err <> "" Then messaggio err, True
  rmt_set_par_sch.resetta

End Sub

Private Sub rmt_set_par_sch_retok(s As String)
'Gestione risposta positiva da scheda di taglio
  
  If s <> "" Then messaggio s, False
  rmt_set_par_sch.resetta

End Sub



Private Sub txt_dis_lam_rul_no_crh_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_dis_sen_lam_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_fin_sca_pri_giu_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_sog_lun_3_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub
Private Sub txt_sog_lun_sup_3_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_sog_vel_2_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub





Private Sub txt_sov_tap_usc_per_sca_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_sov_tap_usc_per_sca_att_KeyUp(KeyCode As Integer, Shift As Integer)
lab_txt_sov_tap_usc_per_sca = val(txt_sov_tap_usc_per_sca_att) / 10
End Sub


Sub scr_par_def_ute_liv(liv As Integer)
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).sov_tap_usc_per_sca = txt_sov_tap_usc_per_sca_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).sca_pri_giu = txt_fin_sca_pri_giu_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).sca_dop_giu = txt_fin_sca_fin_giu_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).dis_sen_lam = txt_dis_sen_lam_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).dis_lam_flp = txt_dis_lam_flp_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).dis_lam_rul_no_crh = txt_dis_lam_rul_no_crh_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).tem_ape_flp = txt_tem_ape_flp_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).tem_chi_flp = txt_tem_chi_flp_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).tem_ape_rul_no_crh = txt_tem_ape_rul_no_crh_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).tem_chi_rul_no_crh = txt_tem_chi_rul_no_crh_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).tem_sca_pre_ape_rul_no_crh = txt_tem_sca_pre_ape_rul_no_crh_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).tem_sca_pre_chi_rul_no_crh = txt_tem_sca_pre_chi_rul_no_crh_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).max_lun_sca = txt_max_lun_sca_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).min_lun_sca = txt_min_lun_sca_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_vel_1 = txt_sog_vel_1_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_lun_1 = txt_sog_lun_1_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_vel_2 = txt_sog_vel_2_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_lun_2 = txt_sog_lun_2_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_vel_3 = txt_sog_vel_3_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_lun_3 = txt_sog_lun_3_def
  'RFAZZI_FI27/03/2008
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_lun_sup_3 = txt_sog_lun_sup_3_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).tem_att_cam_pre_rul_no_crh = txt_tem_att_cam_pre_rul_no_crh_def
End Sub




Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
    If mod_in_cor Then
        esc_pre = True
        mod_in_cor = False
        par_mac_ute = par_mac
        
        'RFAZZI_FI 20-04-2008
        ini_form
        If par_mac.opt.sca_liv(Livello) = "1" Then
          fil_par_sch_tag_liv (txt_liv_tag.Text)
        End If
        
        disable_tab Me
        txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
        txt_idle.TabStop = True
        txt_idle.SetFocus
    End If
  Case Else
End Select
End Sub



Private Sub eve_key_F1(Shift As Integer)
Select Case Shift
  Case 0
    If Not mod_in_cor Then eve_key.chg set_par_mac
  Case Else
End Select
End Sub

Private Sub eve_key_F10(Shift As Integer)
Select Case Shift
  Case 0
    If Not mod_in_cor Then eve_key.chg pag_pro
  Case Else
End Select
End Sub

Private Sub eve_key_F2(Shift As Integer)
Select Case Shift
  Case 0
    'modifica
    If ver_ese_ope(False) Then
      esc_pre = False
      sal_in_cor = False
      'RFAZZI_FI 20-04-2008
      If par_mac.opt.sca_liv(Livello) = "1" Then
        enable_txt_pag_sch_tag Me
        txt_idle.TabStop = False
        txt_sov_tap_usc_per_sca_att.SetFocus
        mod_in_cor = True
      End If
    End If
  Case Else
End Select
End Sub

Private Sub eve_key_F3(Shift As Integer)
Select Case Shift
  Case 0
    'salva
    If mod_in_cor Then
      sal_in_cor = True
      messaggio msg("ope_in_cor"), False, True
      scr_par_att_ute_liv (txt_liv_tag.Text)
      'scrivo nella scheda di taglio
      num_com_sch_tag = 1
      CASO_SND_SCH_TAG = NOT_CFG
      'Iciliouff 03-11-2003 Gestione con RMT
      If rmt_set_par_sch.esegui(tip_cmd.CONFIGURA, GetTickCount(), Livello, 0) Then
        messaggio msg("inv_par_sch_tag"), False
      Else
        messaggio msg("imp_ese_ope"), True
      End If
'      snd_sch_tag livello, CONFIGURA
      
      If Screen.ActiveForm.name = Me.name Then
          disable_tab Me
          txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
          txt_idle.TabStop = True
          txt_idle.SetFocus
      End If
      mod_in_cor = False
    End If
  Case Else
End Select
End Sub


Private Sub eve_key_F4(Shift As Integer)
'copia da default in attivi
Select Case Shift
Case 0 ' parziale
  If ver_ese_ope(False) Then
    messaggio msg("ope_in_cor"), False, False
    CASO_SND_SCH_TAG = NOT_CFG
    num_com_sch_tag = 1
    def_in_att False, Me
    'Iciliouff 03-11-2003
    If rmt_set_par_sch.esegui(tip_cmd.CONFIGURA, GetTickCount(), Livello, 0, False) Then
      messaggio msg("ope_in_cor"), False
    Else
      messaggio msg("imp_ese_ope"), False
    End If
  End If
Case 2  ' totale
  If ver_ese_ope(True) Then
    messaggio msg("ope_in_cor"), False, False
    CASO_SND_SCH_TAG = NOT_CFG
    num_com_sch_tag = val(par_mac.car_tec.num_liv_tag)
    def_in_att (True)
    'Iciliouff 03-11-2003
    If rmt_set_par_sch.esegui(tip_cmd.CONFIGURA, GetTickCount(), Livello, 0, True) Then
      messaggio msg("ope_in_cor"), False
    Else
      messaggio msg("imp_ese_ope"), False
    End If
  End If
End Select
End Sub

Function ver_ese_ope(tot As Boolean) As Boolean
Dim i As Integer

ver_ese_ope = False
If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Function
If Not tot Then
    If Not com_ini_sch_tag(Livello) Then messaggio msg("com_int_con_sch"), True: Exit Function
    If Not sta_tag.SCH_TAG_LIV_PRONTA(Livello, 1) Then messaggio msg("sch_tag_del_liv") & " " & CStr(Livello) & " " & msg("non_pro"), True: Exit Function
Else
    For i = 1 To val(par_mac.car_tec.num_liv_tag)
      If Not com_ini_sch_tag(i) Then messaggio msg("com_int_con_sch"), True: Exit Function
      If Not sta_tag.SCH_TAG_LIV_PRONTA(i, 1) Then messaggio msg("sch_tag_del_liv") & " " & CStr(i) & " " & msg("non_pro"), True: Exit Function
    Next i
End If
If mod_in_cor Then Exit Function
If Not sta_tag.com_OK Then messaggio msg("com_int_con_mac"), True: Exit Function
ver_ese_ope = True

End Function
Private Sub eve_key_F5(Shift As Integer)
'copia da attivi in default
If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Sub
If Not mod_in_cor Then
    messaggio msg("ope_in_cor"), False, False
    Select Case Shift
        Case 0 ' parziale
        att_in_def (False)
        Case 2 ' totale
        form_att = Me.name
        att_in_def (True)
    End Select
    messaggio msg("ope_eff"), False, False
End If
End Sub


Private Sub eve_key_F7(Shift As Integer)
Select Case Shift
  Case 0
    'cambio livello
    If Not mod_in_cor Then
        If par_mac.car_tec.num_liv_tag > 1 Then
          If Livello < par_mac.car_tec.num_liv_tag Then
              Livello = Livello + 1
              txt_liv_tag.Text = Livello
          Else:
              Livello = 1
              txt_liv_tag.Text = Livello
          End If
          'RFAZZI_FI 20-04-2008
          ini_form
          If par_mac.opt.sca_liv(Livello) = "1" Then
            fil_par_sch_tag_liv (txt_liv_tag.Text)
            fil_par_sch_tag_def_liv (txt_liv_tag.Text)
          End If
        End If
    End If
  Case Else
End Select
End Sub

Private Sub eve_key_F8(Shift As Integer)
'pagina precedente o ordini eseguiti
If Not mod_in_cor Then
  Select Case Shift
  Case 0
    eve_key.chg set_par_sch_tag4
  Case 2
    eve_key.chg pag_lis_ese
  Case Else
  End Select
End If
End Sub

Private Sub eve_key_F9(Shift As Integer)
'pagina successiva o lista ordini
If Not mod_in_cor Then
  Select Case Shift
  Case 0
  Case 2
    eve_key.chg pag_lis_ord
  Case Else
  End Select
End If
End Sub

Sub ini_form()
If par_mac.opt.sca_liv(Livello) = "0" Then

  'Attivi
  txt_sov_tap_usc_per_sca_att = 0
  lab_txt_sov_tap_usc_per_sca = 0
  txt_fin_sca_pri_giu_att = 0
  txt_fin_sca_fin_giu_att = 0
  txt_dis_sen_lam_att = 0
  txt_dis_lam_flp_att = 0
  txt_dis_lam_rul_no_crh_att = 0
  txt_tem_ape_flp_att = 0
  txt_tem_chi_flp_att = 0
  txt_tem_ape_rul_no_crh_att = 0
  txt_tem_chi_rul_no_crh_att = 0
  txt_tem_sca_pre_ape_rul_no_crh_att = 0
  txt_tem_sca_pre_chi_rul_no_crh_att = 0
  txt_max_lun_sca_att = 0
  txt_min_lun_sca_att = 0
  txt_sog_vel_1_att = 0
  txt_sog_lun_1_att = 0
  txt_sog_vel_2_att = 0
  txt_sog_lun_2_att = 0
  txt_sog_vel_3_att = 0
  txt_sog_lun_3_att = 0
  txt_sog_lun_sup_3_att = 0
  txt_tem_att_cam_pre_rul_no_crh_att = 0

  'Default
  txt_sov_tap_usc_per_sca_def = 0
  txt_fin_sca_pri_giu_def = 0
  txt_fin_sca_fin_giu_def = 0
  txt_dis_sen_lam_def = 0
  txt_dis_lam_flp_def = 0
  txt_dis_lam_rul_no_crh_def = 0
  txt_tem_ape_flp_def = 0
  txt_tem_chi_flp_def = 0
  txt_tem_ape_rul_no_crh_def = 0
  txt_tem_chi_rul_no_crh_def = 0
  txt_tem_sca_pre_ape_rul_no_crh_def = 0
  txt_tem_sca_pre_chi_rul_no_crh_def = 0
  txt_max_lun_sca_def = 0
  txt_min_lun_sca_def = 0
  txt_sog_vel_1_def = 0
  txt_sog_lun_1_def = 0
  txt_sog_vel_2_def = 0
  txt_sog_lun_2_def = 0
  txt_sog_vel_3_def = 0
  txt_sog_lun_3_def = 0
  txt_sog_lun_sup_3_def = 0
  txt_tem_att_cam_pre_rul_no_crh_def = 0

  'Enabled
  txt_sov_tap_usc_per_sca_att.Enabled = False
  lab_txt_sov_tap_usc_per_sca.Enabled = False
  txt_fin_sca_pri_giu_att.Enabled = False
  txt_fin_sca_fin_giu_att.Enabled = False
  txt_dis_sen_lam_att.Enabled = False
  txt_dis_lam_flp_att.Enabled = False
  txt_dis_lam_rul_no_crh_att.Enabled = False
  txt_tem_ape_flp_att.Enabled = False
  txt_tem_chi_flp_att.Enabled = False
  txt_tem_ape_rul_no_crh_att.Enabled = False
  txt_tem_chi_rul_no_crh_att.Enabled = False
  txt_tem_sca_pre_ape_rul_no_crh_att.Enabled = False
  txt_tem_sca_pre_chi_rul_no_crh_att.Enabled = False
  txt_max_lun_sca_att.Enabled = False
  txt_min_lun_sca_att.Enabled = False
  txt_sog_vel_1_att.Enabled = False
  txt_sog_lun_1_att.Enabled = False
  txt_sog_vel_2_att.Enabled = False
  txt_sog_lun_2_att.Enabled = False
  txt_sog_vel_3_att.Enabled = False
  txt_sog_lun_3_att.Enabled = False
  txt_sog_lun_sup_3_att.Enabled = False
  txt_tem_att_cam_pre_rul_no_crh_att.Enabled = False

Else

  'Enabled
  txt_sov_tap_usc_per_sca_att.Enabled = True
  lab_txt_sov_tap_usc_per_sca.Enabled = True
  txt_fin_sca_pri_giu_att.Enabled = True
  txt_fin_sca_fin_giu_att.Enabled = True
  txt_dis_sen_lam_att.Enabled = True
  txt_dis_lam_flp_att.Enabled = True
  txt_dis_lam_rul_no_crh_att.Enabled = True
  txt_tem_ape_flp_att.Enabled = True
  txt_tem_chi_flp_att.Enabled = True
  txt_tem_ape_rul_no_crh_att.Enabled = True
  txt_tem_chi_rul_no_crh_att.Enabled = True
  txt_tem_sca_pre_ape_rul_no_crh_att.Enabled = True
  txt_tem_sca_pre_chi_rul_no_crh_att.Enabled = True
  txt_max_lun_sca_att.Enabled = True
  txt_min_lun_sca_att.Enabled = True
  txt_sog_vel_1_att.Enabled = True
  txt_sog_lun_1_att.Enabled = True
  txt_sog_vel_2_att.Enabled = True
  txt_sog_lun_2_att.Enabled = True
  txt_sog_vel_3_att.Enabled = True
  txt_sog_lun_3_att.Enabled = True
  txt_sog_lun_sup_3_att.Enabled = True
  txt_tem_att_cam_pre_rul_no_crh_att.Enabled = True

End If

End Sub

Private Sub Form_Activate()
If mod_in_cor Then mod_in_cor = False
If sal_in_cor Then sal_in_cor = False
If esc_pre Then esc_pre = True

'RFAZZI_FI 20-04-2008
txt_liv_tag.Text = Livello
ini_form
If par_mac.opt.sca_liv(Livello) = "1" Then
  fil_par_sch_tag_liv (Livello)
  fil_par_sch_tag_def_liv (Livello)
End If
txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
txt_idle.SetFocus

End Sub

Private Sub txt_dis_lam_flp_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub Form_Load()
Debug.Print "Inizio Load " & Me.name
disable_tab Me
 
Set eve_key = New key
'Iciliouff 03-11-2003
Set rmt_set_par_sch = New rmt
rmt_set_par_sch.set_plc_ute (2)
rmt_set_par_sch.name = "set_par_sch_02"
eve_key.create Me

Carica Me
Debug.Print "Fine Load " & Me.name
Debug.Print ""

End Sub

Private Sub txt_tem_ape_rul_no_crh_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_tem_att_cam_pre_rul_no_crh_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_tem_chi_flp_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_tem_ape_flp_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_tem_chi_rul_no_crh_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub



Private Sub txt_min_lun_sca_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_fin_sca_fin_giu_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub







Private Sub txt_tem_sca_pre_ape_rul_no_crh_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_tem_sca_pre_chi_rul_no_crh_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_max_lun_sca_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub









Private Sub txt_sog_lun_1_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_sog_vel_1_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_sog_lun_2_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_sog_vel_3_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub




