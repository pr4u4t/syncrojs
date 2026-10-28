VERSION 5.00
Begin VB.Form set_par_sch_tag4 
   Appearance      =   0  'Flat
   BackColor       =   &H80000005&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6555
   ClientLeft      =   105
   ClientTop       =   1500
   ClientWidth     =   11955
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6555
   ScaleMode       =   0  'User
   ScaleWidth      =   20490.61
   ShowInTaskbar   =   0   'False
   Begin VB.Frame Frame2 
      BackColor       =   &H80000005&
      Caption         =   "INFORMAZIONI"
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
      Left            =   5985
      TabIndex        =   42
      Top             =   330
      Width           =   5985
      Begin VB.TextBox txt_ver_rad_qua_sen_arr 
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
         Left            =   4365
         TabIndex        =   50
         Top             =   4190
         Width           =   1125
      End
      Begin VB.TextBox txt_per_arr_rad_qua_con_arr 
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
         Left            =   4365
         TabIndex        =   49
         Top             =   3770
         Width           =   1125
      End
      Begin VB.TextBox txt_qua_bit_rad_qua_con_arr 
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
         Left            =   4365
         TabIndex        =   48
         Top             =   3350
         Width           =   1125
      End
      Begin VB.TextBox txt_ver_rad_qua_con_arr 
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
         Left            =   4365
         TabIndex        =   47
         Top             =   2925
         Width           =   1125
      End
      Begin VB.TextBox txt_ver_PIC 
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
         Left            =   4365
         TabIndex        =   46
         Top             =   2505
         Width           =   1125
      End
      Begin VB.TextBox txt_ver_sch_tag 
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
         Left            =   4365
         TabIndex        =   45
         Top             =   2085
         Width           =   1125
      End
      Begin VB.TextBox txt_ver_pro_sch_tag 
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
         Left            =   4365
         TabIndex        =   44
         Top             =   1665
         Width           =   1125
      End
      Begin VB.TextBox txt_qua_bit_rad_qua_sen_arr 
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
         Left            =   4365
         TabIndex        =   43
         Top             =   4612
         Width           =   1125
      End
      Begin VB.Label lab_liv 
         Alignment       =   2  'Center
         BackColor       =   &H80000005&
         Caption         =   "3"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   17.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   420
         Left            =   2460
         TabIndex        =   60
         Top             =   675
         Width           =   300
      End
      Begin VB.Label Label9 
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
         ForeColor       =   &H00000000&
         Height          =   345
         Left            =   105
         TabIndex        =   59
         Top             =   720
         Width           =   2355
      End
      Begin VB.Label Label8 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Versione programma scheda di taglio"
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
         TabIndex        =   58
         Top             =   1755
         Width           =   3405
      End
      Begin VB.Label Label7 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Versione scheda di taglio"
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
         TabIndex        =   57
         Top             =   2175
         Width           =   2325
      End
      Begin VB.Label Label6 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Versione PIC"
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
         TabIndex        =   56
         Top             =   2595
         Width           =   2325
      End
      Begin VB.Label Label5 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Versione radice quadrata con arrotondamento"
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
         TabIndex        =   55
         Top             =   3015
         Width           =   4125
      End
      Begin VB.Label Label4 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Percentuale di arrot. radice quadrata con arrot."
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
         TabIndex        =   54
         Top             =   3860
         Width           =   5085
      End
      Begin VB.Label Label3 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Versione radice quadrata senza arrotondamento"
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
         Top             =   4280
         Width           =   4005
      End
      Begin VB.Label Label2 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Quantità di bit radice quadrata con arrotondamento"
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
         Top             =   3440
         Width           =   4125
      End
      Begin VB.Label Label1 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Quantità di bit radice quadrata senza arrotondamento"
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
         Top             =   4703
         Width           =   4005
      End
   End
   Begin VB.TextBox txt_idle 
      Alignment       =   1  'Right Justify
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   360
      Left            =   0
      TabIndex        =   35
      Text            =   "00000"
      Top             =   -4200
      Width           =   645
   End
   Begin VB.Frame Frame1 
      BackColor       =   &H80000005&
      Caption         =   "PARAMETRI PER CUT-TO-MARK PER LIVELLO DI TAGLIO"
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
      TabIndex        =   0
      Top             =   330
      Width           =   5970
      Begin VB.TextBox txt_vel_ftc_def 
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
         Left            =   4335
         MaxLength       =   4
         TabIndex        =   18
         TabStop         =   0   'False
         Top             =   4350
         Width           =   915
      End
      Begin VB.TextBox txt_dis_cen_mac_sen_fuo_car_att 
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
         Left            =   3405
         MaxLength       =   4
         TabIndex        =   9
         Top             =   4770
         Width           =   915
      End
      Begin VB.TextBox txt_dis_cen_mac_sen_fuo_car_def 
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
         Left            =   4335
         MaxLength       =   4
         TabIndex        =   19
         TabStop         =   0   'False
         Top             =   4770
         Width           =   915
      End
      Begin VB.TextBox txt_dis_edg_ftc_att 
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
         Left            =   3405
         MaxLength       =   3
         TabIndex        =   10
         Top             =   5190
         Width           =   915
      End
      Begin VB.TextBox txt_dis_edg_ftc_def 
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
         Left            =   4335
         MaxLength       =   3
         TabIndex        =   20
         TabStop         =   0   'False
         Top             =   5190
         Width           =   915
      End
      Begin VB.TextBox txt_vel_ftc_att 
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
         Left            =   3405
         MaxLength       =   4
         TabIndex        =   8
         Top             =   4350
         Width           =   915
      End
      Begin VB.TextBox txt_off_tag_reg_att 
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
         Left            =   3405
         MaxLength       =   3
         TabIndex        =   1
         Top             =   1425
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
         TabIndex        =   32
         Top             =   600
         Width           =   2715
         Begin VB.TextBox txt_liv_tag 
            Alignment       =   2  'Center
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
            TabIndex        =   33
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
            TabIndex        =   34
            Top             =   120
            Width           =   2175
         End
      End
      Begin VB.TextBox txt_dis_FTC_tac_lam_def 
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
         Left            =   4335
         MaxLength       =   4
         TabIndex        =   17
         TabStop         =   0   'False
         Top             =   3945
         Width           =   915
      End
      Begin VB.TextBox txt_dis_FTC_tac_lam_att 
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
         Left            =   3405
         MaxLength       =   4
         TabIndex        =   7
         Top             =   3945
         Width           =   915
      End
      Begin VB.TextBox txt_cor_tag_reg_2°_sog_def 
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
         Left            =   4335
         MaxLength       =   3
         TabIndex        =   16
         TabStop         =   0   'False
         Top             =   3525
         Width           =   915
      End
      Begin VB.TextBox txt_cor_tag_reg_2°_sog_att 
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
         Left            =   3405
         MaxLength       =   3
         TabIndex        =   6
         Top             =   3525
         Width           =   915
      End
      Begin VB.TextBox txt_sog_vel_cor_def 
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
         Left            =   4335
         MaxLength       =   3
         TabIndex        =   15
         TabStop         =   0   'False
         Top             =   3105
         Width           =   915
      End
      Begin VB.TextBox txt_sog_vel_cor_att 
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
         Left            =   3405
         MaxLength       =   3
         TabIndex        =   5
         Top             =   3105
         Width           =   915
      End
      Begin VB.TextBox txt_cor_tag_reg_1°_sog_def 
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
         Left            =   4335
         MaxLength       =   3
         TabIndex        =   14
         TabStop         =   0   'False
         Top             =   2685
         Width           =   915
      End
      Begin VB.TextBox txt_cor_tag_reg_1°_sog_att 
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
         Left            =   3405
         MaxLength       =   3
         TabIndex        =   4
         Top             =   2685
         Width           =   915
      End
      Begin VB.TextBox txt_fin_dop_tac_def 
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
         Left            =   4335
         MaxLength       =   3
         TabIndex        =   13
         TabStop         =   0   'False
         Top             =   2265
         Width           =   915
      End
      Begin VB.TextBox txt_fin_dop_tac_att 
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
         Left            =   3405
         MaxLength       =   3
         TabIndex        =   3
         Top             =   2265
         Width           =   915
      End
      Begin VB.TextBox txt_fin_pri_tac_def 
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
         Left            =   4335
         MaxLength       =   3
         TabIndex        =   12
         TabStop         =   0   'False
         Top             =   1845
         Width           =   915
      End
      Begin VB.TextBox txt_fin_pri_tac_att 
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
         Left            =   3405
         MaxLength       =   3
         TabIndex        =   2
         Top             =   1845
         Width           =   915
      End
      Begin VB.TextBox txt_off_tag_reg_def 
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
         Left            =   4335
         MaxLength       =   3
         TabIndex        =   11
         TabStop         =   0   'False
         Top             =   1425
         Width           =   915
      End
      Begin VB.Label Label23 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mm/sec"
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
         Left            =   5340
         TabIndex        =   66
         Top             =   4440
         Width           =   525
      End
      Begin VB.Label Label18 
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
         Left            =   5340
         TabIndex        =   65
         Top             =   5280
         Width           =   525
      End
      Begin VB.Label Label17 
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
         Left            =   5340
         TabIndex        =   64
         Top             =   4860
         Width           =   525
      End
      Begin VB.Label Label15 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Distanza fotocellula - asse fotocellule bordo"
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
         Left            =   180
         TabIndex        =   63
         Top             =   5280
         Width           =   3195
      End
      Begin VB.Label Label12 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Distanza asse mac - sensore fuori carter"
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
         Left            =   180
         TabIndex        =   62
         Top             =   4860
         Width           =   2985
      End
      Begin VB.Label Label11 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Velocità traslazione fotocellula"
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
         Left            =   180
         TabIndex        =   61
         Top             =   4440
         Width           =   2325
      End
      Begin VB.Label Label13 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Offset taglio a registro"
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
         Left            =   180
         TabIndex        =   41
         Top             =   1515
         Width           =   2325
      End
      Begin VB.Label Label14 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Finestra prima della tacca"
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
         Left            =   180
         TabIndex        =   40
         Top             =   1935
         Width           =   2325
      End
      Begin VB.Label Label19 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Finestra dopo la tacca"
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
         Left            =   180
         TabIndex        =   39
         Top             =   2355
         Width           =   2325
      End
      Begin VB.Label Label20 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Correzione taglio a reg. 1° soglia"
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
         Left            =   180
         TabIndex        =   38
         Top             =   2775
         Width           =   2565
      End
      Begin VB.Label Label22 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Correzione taglio a reg. 2° soglia"
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
         Left            =   180
         TabIndex        =   37
         Top             =   3615
         Width           =   2565
      End
      Begin VB.Label Label10 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Distanza FTC tacche <-> lama"
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
         Left            =   180
         TabIndex        =   36
         Top             =   4035
         Width           =   2325
      End
      Begin VB.Label Label51 
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
         Left            =   5340
         TabIndex        =   30
         Top             =   4035
         Width           =   375
      End
      Begin VB.Label Label38 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "m/min"
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
         Left            =   5340
         TabIndex        =   29
         Top             =   3195
         Width           =   525
      End
      Begin VB.Label Label37 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "dmm"
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
         Left            =   5340
         TabIndex        =   28
         Top             =   2775
         Width           =   525
      End
      Begin VB.Label Label36 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "dmm"
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
         Left            =   5340
         TabIndex        =   27
         Top             =   3615
         Width           =   525
      End
      Begin VB.Label Label35 
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
         Left            =   5340
         TabIndex        =   26
         Top             =   1935
         Width           =   525
      End
      Begin VB.Label Label34 
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
         Left            =   5340
         TabIndex        =   25
         Top             =   2355
         Width           =   525
      End
      Begin VB.Label Label21 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Soglia di velocità per correzione"
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
         Left            =   180
         TabIndex        =   24
         Top             =   3195
         Width           =   2325
      End
      Begin VB.Label Label33 
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
         Left            =   5340
         TabIndex        =   23
         Top             =   1515
         Width           =   525
      End
      Begin VB.Label Label30 
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
         Left            =   3555
         TabIndex        =   22
         Top             =   1065
         Width           =   645
      End
      Begin VB.Label Label42 
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
         Left            =   4440
         TabIndex        =   21
         Top             =   1065
         Width           =   705
      End
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "PARAMETRI CONTROLLO TAGLIO   PAG. 4/5"
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
      TabIndex        =   31
      Top             =   0
      Width           =   7785
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "set_par_sch_tag4.frx":0000
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "set_par_sch_tag4"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Const sec_liv = "sch_tag_liv_"
'Iciliouff 03-11-2003
Private WithEvents rmt_set_par_sch As rmt
Attribute rmt_set_par_sch.VB_VarHelpID = -1


Function ver_ese_ope(tot As Boolean) As Boolean
Dim i As Integer

ver_ese_ope = False
If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Function
'Robertouff 26-11-2007
'If Not (par_mac.opt.cut_mar = STR_TRUE Or par_mac.opt.cut_pat = STR_TRUE Or par_mac.opt.cut_pat_auto = STR_TRUE) Then messaggio msg("imp_ese_ope"), True: Exit Function
'agrassi_FI15-07-08
Dim cut_to_pat As Boolean
cut_to_pat = (par_mac.opt.cut_pat(1) = STR_TRUE) Or (par_mac.opt.cut_pat(2) = STR_TRUE) Or (par_mac.opt.cut_pat(3) = STR_TRUE)
'If Not (par_mac.opt.cut_mar = STR_TRUE Or par_mac.opt.cut_pat = STR_TRUE) Then messaggio msg("imp_ese_ope"), True: Exit Function
    
'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
'If Not (par_mac.opt.cut_mar = STR_TRUE Or cut_to_pat Or par_mac.opt.cut_pat_auto = STR_TRUE) Then messaggio msg("imp_ese_ope"), True: Exit Function
Dim cut_to_pat_auto As Boolean
cut_to_pat_auto = (par_mac.opt.cut_pat_auto(1) = STR_TRUE) Or (par_mac.opt.cut_pat_auto(2) = STR_TRUE) Or (par_mac.opt.cut_pat_auto(3) = STR_TRUE)

If Not (par_mac.opt.cut_mar = STR_TRUE Or cut_to_pat Or cut_to_pat_auto) Then messaggio msg("imp_ese_ope"), True: Exit Function

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
Sub att_in_def(tot As Boolean)
Dim i As Integer

  'scrittura nelle textbox
  txt_off_tag_reg_def = txt_off_tag_reg_att
  txt_fin_pri_tac_def = txt_fin_pri_tac_att
  txt_fin_dop_tac_def = txt_fin_dop_tac_att
  txt_cor_tag_reg_1°_sog_def = txt_cor_tag_reg_1°_sog_att
  txt_cor_tag_reg_2°_sog_def = txt_cor_tag_reg_2°_sog_att
  txt_sog_vel_cor_def = txt_sog_vel_cor_att
  txt_dis_FTC_tac_lam_def = txt_dis_FTC_tac_lam_att
  'Iciliouff 20-11-2003
  txt_vel_ftc_def = txt_vel_ftc_att
  txt_dis_cen_mac_sen_fuo_car_def = txt_dis_cen_mac_sen_fuo_car_att
  txt_dis_edg_ftc_def = txt_dis_edg_ftc_att
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
        set_par_sch_tag2.att_in_def (True)
        'RFAZZI_FI19/03/2008
        set_par_sch_tag5.att_in_def (True)
    End If
  Else
    scr_par_sch_tag_def_liv_ini txt_liv_tag
  End If

End Sub

Sub ini_form()

  'Robertouff 26-11-2007
  'agrassi_FI15-07-08
  Dim cut_to_pat As Boolean
  cut_to_pat = (par_mac.opt.cut_pat(1) = STR_TRUE) Or (par_mac.opt.cut_pat(2) = STR_TRUE) Or (par_mac.opt.cut_pat(3) = STR_TRUE)
  'If par_mac.opt.cut_mar = "0" And par_mac.opt.cut_pat = "0" And par_mac.opt.cut_pat_auto = "0" Then
  
  'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
  'If par_mac.opt.cut_mar = "0" And Not cut_to_pat And par_mac.opt.cut_pat_auto = "0" Then
  Dim cut_to_pat_auto As Boolean
  cut_to_pat_auto = (par_mac.opt.cut_pat_auto(1) = STR_TRUE) Or (par_mac.opt.cut_pat_auto(2) = STR_TRUE) Or (par_mac.opt.cut_pat_auto(3) = STR_TRUE)
  If par_mac.opt.cut_mar = "0" And Not cut_to_pat And Not cut_to_pat_auto Then
  '    Frame1.BackColor = MEDIUM_GRAY
    Frame1.Enabled = False
    'Attivi
    txt_off_tag_reg_att = ""
    txt_fin_pri_tac_att = ""
    txt_fin_dop_tac_att = ""
    txt_cor_tag_reg_1°_sog_att = ""
    txt_sog_vel_cor_att = ""
    txt_cor_tag_reg_2°_sog_att = ""
    txt_dis_FTC_tac_lam_att = ""
    'Iciliouff 20-11-2003
    txt_vel_ftc_att = ""
    txt_dis_cen_mac_sen_fuo_car_att = ""
    txt_dis_edg_ftc_att = ""
    
    'Default
    txt_off_tag_reg_def = ""
    txt_fin_pri_tac_def = ""
    txt_fin_dop_tac_def = ""
    txt_cor_tag_reg_1°_sog_def = ""
    txt_sog_vel_cor_def = ""
    txt_cor_tag_reg_2°_sog_def = ""
    txt_dis_FTC_tac_lam_def = ""
    'Iciliouff 20-11-2003
    txt_vel_ftc_def = ""
    txt_dis_cen_mac_sen_fuo_car_def = ""
    txt_dis_edg_ftc_def = ""
    
    txt_off_tag_reg_att.Enabled = False
    txt_fin_pri_tac_att.Enabled = False
    txt_fin_dop_tac_att.Enabled = False
    txt_cor_tag_reg_1°_sog_att.Enabled = False
    txt_sog_vel_cor_att.Enabled = False
    txt_cor_tag_reg_2°_sog_att.Enabled = False
    txt_dis_FTC_tac_lam_att.Enabled = False
    'Iciliouff 20-11-2003
    txt_vel_ftc_att.Enabled = False
    txt_dis_cen_mac_sen_fuo_car_att.Enabled = False
    txt_dis_edg_ftc_att.Enabled = False
  Else
  '    Frame1.BackColor = &HFFC0FF
    Frame1.Enabled = True
    txt_off_tag_reg_att.Enabled = True
    txt_fin_pri_tac_att.Enabled = True
    txt_fin_dop_tac_att.Enabled = True
    txt_cor_tag_reg_1°_sog_att.Enabled = True
    txt_sog_vel_cor_att.Enabled = True
    txt_cor_tag_reg_2°_sog_att.Enabled = True
    txt_dis_FTC_tac_lam_att.Enabled = True
    'Iciliouff 20-11-2003
    'Robertouff 26-11-2007
'    txt_vel_ftc_att.Enabled = (par_mac.opt.cut_pat = STR_TRUE) Or (par_mac.opt.cut_pat_auto = STR_TRUE)
'    txt_dis_cen_mac_sen_fuo_car_att.Enabled = (par_mac.opt.cut_pat = STR_TRUE) Or (par_mac.opt.cut_pat_auto = STR_TRUE)
'    txt_dis_edg_ftc_att.Enabled = (par_mac.opt.cut_pat = STR_TRUE) Or (par_mac.opt.cut_pat_auto = STR_TRUE)
    'agrassi_FI15-07-08
    'txt_vel_ftc_att.Enabled = (par_mac.opt.cut_pat = STR_TRUE)
    'txt_dis_cen_mac_sen_fuo_car_att.Enabled = (par_mac.opt.cut_pat = STR_TRUE)
    'txt_dis_edg_ftc_att.Enabled = (par_mac.opt.cut_pat = STR_TRUE)
    txt_vel_ftc_att.Enabled = cut_to_pat
    txt_dis_cen_mac_sen_fuo_car_att.Enabled = cut_to_pat
    txt_dis_edg_ftc_att.Enabled = cut_to_pat
  End If

End Sub

Sub fil_par_inf(liv As Integer)
txt_ver_pro_sch_tag = par_mac.sch_tag.par_inf(liv).ver_pro_sch_tag
txt_ver_sch_tag = par_mac.sch_tag.par_inf(liv).ver_sch_tag
txt_ver_PIC = par_mac.sch_tag.par_inf(liv).ver_PIC
txt_ver_rad_qua_con_arr = par_mac.sch_tag.par_inf(liv).ver_rad_qua_con_arr
txt_qua_bit_rad_qua_con_arr = par_mac.sch_tag.par_inf(liv).qua_bit_rad_qua_con_arr
txt_per_arr_rad_qua_con_arr = par_mac.sch_tag.par_inf(liv).per_arr_rad_qua_con_arr
txt_ver_rad_qua_sen_arr = par_mac.sch_tag.par_inf(liv).ver_rad_qua_sen_arr
txt_qua_bit_rad_qua_sen_arr = par_mac.sch_tag.par_inf(liv).qua_bit_rad_qua_sen_arr
End Sub

Sub fil_par_sch_tag_liv(liv As Integer)

  'caricamento dei parametri nella form scheda di taglio per livello
  txt_off_tag_reg_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).off_tag_reg)
  txt_fin_pri_tac_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).fin_pri_tac)
  txt_fin_dop_tac_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).fin_dop_tac)
  txt_cor_tag_reg_1°_sog_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).cor_tag_reg_1°_sog)
  txt_cor_tag_reg_2°_sog_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).cor_tag_reg_2°_sog)
  txt_sog_vel_cor_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).sog_vel_cor)
  txt_dis_FTC_tac_lam_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).dis_FTC_tac_lam)
  'Iciliouff 20-11-2003
  txt_vel_ftc_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).vel_ftc)
  txt_dis_cen_mac_sen_fuo_car_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).dis_cen_mac_sen_fuo_car)
  txt_dis_edg_ftc_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).dis_edg_ftc)

End Sub


Sub fil_par_sch_tag_def_liv(liv As Integer)
'caricamento dei parametri nella form scheda di taglio per livello

  txt_off_tag_reg_def = Trim(par_mac.sch_tag_def.sch_tag_liv(liv).off_tag_reg)
  txt_fin_pri_tac_def = Trim(par_mac.sch_tag_def.sch_tag_liv(liv).fin_pri_tac)
  txt_fin_dop_tac_def = Trim(par_mac.sch_tag_def.sch_tag_liv(liv).fin_dop_tac)
  txt_cor_tag_reg_1°_sog_def = Trim(par_mac.sch_tag_def.sch_tag_liv(liv).cor_tag_reg_1°_sog)
  txt_cor_tag_reg_2°_sog_def = Trim(par_mac.sch_tag_def.sch_tag_liv(liv).cor_tag_reg_2°_sog)
  txt_sog_vel_cor_def = Trim(par_mac.sch_tag_def.sch_tag_liv(liv).sog_vel_cor)
  txt_dis_FTC_tac_lam_def = Trim(par_mac.sch_tag_def.sch_tag_liv(liv).dis_FTC_tac_lam)
  'Iciliouff 20-11-2003
  txt_vel_ftc_def = Trim(par_mac.sch_tag.sch_tag_liv(liv).vel_ftc)
  txt_dis_cen_mac_sen_fuo_car_def = Trim(par_mac.sch_tag.sch_tag_liv(liv).dis_cen_mac_sen_fuo_car)
  txt_dis_edg_ftc_def = Trim(par_mac.sch_tag.sch_tag_liv(liv).dis_edg_ftc)

End Sub

Sub scr_par_att_ute_liv(liv As Integer)

  'PARAMETRI PER LIVELLO DI TAGLIO
  par_mac_ute.sch_tag.sch_tag_liv(liv).off_tag_reg = txt_off_tag_reg_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).sog_vel_cor = txt_sog_vel_cor_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).cor_tag_reg_2°_sog = txt_cor_tag_reg_2°_sog_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).cor_tag_reg_1°_sog = txt_cor_tag_reg_1°_sog_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).fin_dop_tac = txt_fin_dop_tac_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).fin_pri_tac = txt_fin_pri_tac_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).dis_FTC_tac_lam = txt_dis_FTC_tac_lam_att
  'Iciliouff 20-11-2003
  par_mac_ute.sch_tag.sch_tag_liv(liv).vel_ftc = txt_vel_ftc_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).dis_cen_mac_sen_fuo_car = txt_dis_cen_mac_sen_fuo_car_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).dis_edg_ftc = txt_dis_edg_ftc_att

End Sub

Sub scr_par_def_ute_liv(liv As Integer)

  par_mac_ute.sch_tag_def.sch_tag_liv(liv).off_tag_reg = txt_off_tag_reg_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_vel_cor = txt_sog_vel_cor_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).cor_tag_reg_2°_sog = txt_cor_tag_reg_2°_sog_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).cor_tag_reg_1°_sog = txt_cor_tag_reg_1°_sog_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).fin_dop_tac = txt_fin_dop_tac_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).fin_pri_tac = txt_fin_pri_tac_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).dis_FTC_tac_lam = txt_dis_FTC_tac_lam_def
  'iciliouff 20-11-2003
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).vel_ftc = txt_vel_ftc_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).dis_cen_mac_sen_fuo_car = txt_dis_cen_mac_sen_fuo_car_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).dis_edg_ftc = txt_dis_edg_ftc_def

End Sub


Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
    If mod_in_cor Then
        esc_pre = True
        mod_in_cor = False
        par_mac_ute = par_mac
        fil_par_sch_tag_liv (txt_liv_tag.Text)
    '    fil_par_sch_tag_def_liv (txt_liv_tag.Text)
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
      enable_txt_pag_sch_tag Me
      txt_idle.TabStop = False
      txt_off_tag_reg_att.SetFocus
      mod_in_cor = True
    End If
  Case Else
End Select
End Sub


Private Sub eve_key_F3(Shift As Integer)
Select Case Shift
  Case 0
    'salva
    If mod_in_cor Then
        messaggio msg("ope_in_cor"), False, True
        sal_in_cor = True
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
'        snd_sch_tag livello, CONFIGURA
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

Private Sub eve_key_F5(Shift As Integer)
'copia da attivi in default
If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Sub
If Not mod_in_cor Then
  'Robertouff 26-11-2007
  'agrassi_FI15-07-08
  Dim cut_to_pat As Boolean
  cut_to_pat = (par_mac.opt.cut_pat(1) = STR_TRUE) Or (par_mac.opt.cut_pat(2) = STR_TRUE) Or (par_mac.opt.cut_pat(3) = STR_TRUE)
  'If par_mac.opt.cut_mar = STR_TRUE Or par_mac.opt.cut_pat = STR_TRUE Or par_mac.opt.cut_pat_auto = STR_TRUE Then
  
  'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
  'If par_mac.opt.cut_mar = STR_TRUE Or cut_to_pat Or par_mac.opt.cut_pat_auto = STR_TRUE Then
  Dim cut_to_pat_auto As Boolean
  cut_to_pat_auto = (par_mac.opt.cut_pat_auto(1) = STR_TRUE) Or (par_mac.opt.cut_pat_auto(2) = STR_TRUE) Or (par_mac.opt.cut_pat_auto(3) = STR_TRUE)
  If par_mac.opt.cut_mar = STR_TRUE Or cut_to_pat Or cut_to_pat_auto Then
    messaggio msg("ope_in_cor"), False, False
    Select Case Shift
        Case 0 ' parziale
        att_in_def (False)
        Case 2 ' totale
        form_att = Me.name
        att_in_def (True)
    End Select
    messaggio msg("ope_eff"), False, False
  Else
    messaggio msg("cut_to_mark_non_pre"), True
  End If
End If
End Sub


Private Sub eve_key_F6(Shift As Integer)
Select Case Shift
  Case 0
    If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Sub
    If Not mod_in_cor Then
      If Not sta_tag.SCH_TAG_LIV_PRONTA(Livello, 1) Then
        messaggio msg("sch_tag_del_liv") & " " & CStr(Livello) & " " & msg("non_pro"), True
      Else
        messaggio msg("ope_in_cor"), False, False
        num_com_sch_tag = 1
'        CASO_RCV_SCH_TAG = PARAM_INFORM
        'Iciliouff 03-11-2003 Gestione con RMT
        If rmt_set_par_sch.esegui(tip_cmd.INFORMAZIONI, GetTickCount(), Livello, 0) Then
          messaggio msg("inv_par_sch_tag"), False
        Else
          messaggio msg("imp_ese_ope"), True
        End If
'        snd_sch_tag livello, INFORMAZIONI
      End If
    End If
  Case Else
End Select
End Sub

Private Sub eve_key_F7(Shift As Integer)
Select Case Shift
  Case 0
    'cambio livello
    If Not mod_in_cor Then
        If par_mac.car_tec.num_liv_tag > 1 Then
          If Livello < par_mac.car_tec.num_liv_tag Then
              Livello = Livello + 1
          Else:
              Livello = 1
          End If
          txt_liv_tag.Text = Livello
          lab_liv = Livello
          fil_par_sch_tag_liv (txt_liv_tag.Text)
          fil_par_sch_tag_def_liv (txt_liv_tag.Text)
          fil_par_inf (Livello)
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
      eve_key.chg set_par_sch_tag3
    Case 2
      eve_key.chg pag_lis_ese
    Case Else
  End Select
End If
End Sub

Private Sub eve_key_F9(Shift As Integer)
If Not mod_in_cor Then
  Select Case Shift
    Case 0
      eve_key.chg set_par_sch_tag5
    Case 2
      eve_key.chg pag_lis_ord
    Case Else
  End Select
End If
End Sub


Private Sub Form_Activate()

ini_form

If mod_in_cor Then mod_in_cor = False
If sal_in_cor Then sal_in_cor = False
If esc_pre Then esc_pre = True

txt_liv_tag.Text = Livello
lab_liv = Livello
'Iciliouff 20-11-2003
'Robertouff 26-11-2007
'agrassi_FI15-07-08
Dim cut_to_pat As Boolean
cut_to_pat = (par_mac.opt.cut_pat(1) = STR_TRUE) Or (par_mac.opt.cut_pat(2) = STR_TRUE) Or (par_mac.opt.cut_pat(3) = STR_TRUE)
'If par_mac.opt.cut_mar = STR_TRUE Or par_mac.opt.cut_pat = STR_TRUE Or par_mac.opt.cut_pat_auto = STR_TRUE Then
  
'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
'If par_mac.opt.cut_mar = STR_TRUE Or cut_to_pat Or par_mac.opt.cut_pat_auto = STR_TRUE Then
Dim cut_to_pat_auto As Boolean
cut_to_pat_auto = (par_mac.opt.cut_pat_auto(1) = STR_TRUE) Or (par_mac.opt.cut_pat_auto(2) = STR_TRUE) Or (par_mac.opt.cut_pat_auto(3) = STR_TRUE)
If par_mac.opt.cut_mar = STR_TRUE Or cut_to_pat Or cut_to_pat_auto Then
    fil_par_sch_tag_liv (Livello)
    fil_par_sch_tag_def_liv (Livello)
End If
fil_par_inf (Livello)
txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
txt_idle.SetFocus

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
Debug.Print "Inizio Load " & Me.name
disable_tab Me
 
Set eve_key = New key
eve_key.create Me
'Iciliouff 03-11-2003
Set rmt_set_par_sch = New rmt
rmt_set_par_sch.set_plc_ute (2)
rmt_set_par_sch.name = "set_par_sch_04"

Carica Me
Debug.Print "Fine Load " & Me.name
Debug.Print ""

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


Private Sub txt_cor_tag_reg_1°_sog_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_cor_tag_reg_2°_sog_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_dis_cen_mac_sen_fuo_car_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_dis_FTC_tac_lam_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub




Private Sub txt_fin_dop_tac_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_fin_pri_tac_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_off_tag_reg_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_sog_vel_cor_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_vel_ftp_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


