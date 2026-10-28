VERSION 5.00
Begin VB.Form set_par_sch_tag1 
   Appearance      =   0  'Flat
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6225
   ClientLeft      =   75
   ClientTop       =   1335
   ClientWidth     =   11955
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6225
   ScaleMode       =   0  'User
   ScaleWidth      =   20490.61
   ShowInTaskbar   =   0   'False
   Begin VB.Frame Frame2 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      Caption         =   "CURVA DI TAGLIO"
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
      Height          =   1335
      Index           =   0
      Left            =   0
      TabIndex        =   95
      Top             =   4875
      Width           =   11962
      Begin VB.TextBox txt_vel_tag 
         Alignment       =   2  'Center
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
         Index           =   14
         Left            =   11175
         MaxLength       =   3
         TabIndex        =   44
         Top             =   765
         Width           =   720
      End
      Begin VB.TextBox txt_vel_tag 
         Alignment       =   2  'Center
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
         Index           =   13
         Left            =   10470
         MaxLength       =   3
         TabIndex        =   43
         Top             =   765
         Width           =   720
      End
      Begin VB.TextBox txt_vel_tag 
         Alignment       =   2  'Center
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
         Index           =   12
         Left            =   9765
         MaxLength       =   3
         TabIndex        =   42
         Top             =   765
         Width           =   720
      End
      Begin VB.TextBox txt_vel_tag 
         Alignment       =   2  'Center
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
         Index           =   11
         Left            =   9060
         MaxLength       =   3
         TabIndex        =   41
         Top             =   765
         Width           =   720
      End
      Begin VB.TextBox txt_vel_tag 
         Alignment       =   2  'Center
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
         Index           =   10
         Left            =   8355
         MaxLength       =   3
         TabIndex        =   40
         Top             =   765
         Width           =   720
      End
      Begin VB.TextBox txt_vel_tag 
         Alignment       =   2  'Center
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
         Index           =   9
         Left            =   7650
         MaxLength       =   3
         TabIndex        =   39
         Top             =   765
         Width           =   720
      End
      Begin VB.TextBox txt_vel_tag 
         Alignment       =   2  'Center
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
         Index           =   8
         Left            =   6945
         MaxLength       =   3
         TabIndex        =   38
         Top             =   765
         Width           =   720
      End
      Begin VB.TextBox txt_vel_tag 
         Alignment       =   2  'Center
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
         Index           =   7
         Left            =   6240
         MaxLength       =   3
         TabIndex        =   37
         Top             =   765
         Width           =   720
      End
      Begin VB.TextBox txt_vel_tag 
         Alignment       =   2  'Center
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
         Index           =   6
         Left            =   5535
         MaxLength       =   3
         TabIndex        =   36
         Top             =   765
         Width           =   720
      End
      Begin VB.TextBox txt_vel_tag 
         Alignment       =   2  'Center
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
         Index           =   5
         Left            =   4830
         MaxLength       =   3
         TabIndex        =   35
         Top             =   765
         Width           =   720
      End
      Begin VB.TextBox txt_vel_tag 
         Alignment       =   2  'Center
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
         Index           =   4
         Left            =   4125
         MaxLength       =   3
         TabIndex        =   34
         Top             =   765
         Width           =   720
      End
      Begin VB.TextBox txt_vel_tag 
         Alignment       =   2  'Center
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
         Index           =   3
         Left            =   3420
         MaxLength       =   3
         TabIndex        =   33
         Top             =   765
         Width           =   720
      End
      Begin VB.TextBox txt_vel_tag 
         Alignment       =   2  'Center
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
         Index           =   2
         Left            =   2715
         MaxLength       =   3
         TabIndex        =   32
         Top             =   765
         Width           =   720
      End
      Begin VB.TextBox txt_vel_tag 
         Alignment       =   2  'Center
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
         Index           =   1
         Left            =   2010
         MaxLength       =   3
         TabIndex        =   31
         Top             =   765
         Width           =   720
      End
      Begin VB.TextBox txt_vel_tag 
         Alignment       =   2  'Center
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
         Index           =   0
         Left            =   1305
         MaxLength       =   3
         TabIndex        =   30
         Top             =   765
         Width           =   720
      End
      Begin VB.TextBox txt_Lun_tag 
         Alignment       =   2  'Center
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
         Index           =   14
         Left            =   11175
         MaxLength       =   4
         TabIndex        =   29
         Top             =   360
         Width           =   720
      End
      Begin VB.TextBox txt_Lun_tag 
         Alignment       =   2  'Center
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
         Index           =   13
         Left            =   10470
         MaxLength       =   4
         TabIndex        =   28
         Top             =   360
         Width           =   720
      End
      Begin VB.TextBox txt_Lun_tag 
         Alignment       =   2  'Center
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
         Index           =   12
         Left            =   9765
         MaxLength       =   4
         TabIndex        =   27
         Top             =   360
         Width           =   720
      End
      Begin VB.TextBox txt_Lun_tag 
         Alignment       =   2  'Center
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
         Index           =   11
         Left            =   9060
         MaxLength       =   4
         TabIndex        =   26
         Top             =   360
         Width           =   720
      End
      Begin VB.TextBox txt_Lun_tag 
         Alignment       =   2  'Center
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
         Index           =   10
         Left            =   8355
         MaxLength       =   4
         TabIndex        =   25
         Top             =   360
         Width           =   720
      End
      Begin VB.TextBox txt_Lun_tag 
         Alignment       =   2  'Center
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
         Index           =   9
         Left            =   7650
         MaxLength       =   4
         TabIndex        =   24
         Top             =   360
         Width           =   720
      End
      Begin VB.TextBox txt_Lun_tag 
         Alignment       =   2  'Center
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
         Index           =   8
         Left            =   6945
         MaxLength       =   4
         TabIndex        =   23
         Top             =   360
         Width           =   720
      End
      Begin VB.TextBox txt_Lun_tag 
         Alignment       =   2  'Center
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
         Index           =   7
         Left            =   6240
         MaxLength       =   4
         TabIndex        =   22
         Top             =   360
         Width           =   720
      End
      Begin VB.TextBox txt_Lun_tag 
         Alignment       =   2  'Center
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
         Index           =   6
         Left            =   5535
         MaxLength       =   4
         TabIndex        =   21
         Top             =   360
         Width           =   720
      End
      Begin VB.TextBox txt_Lun_tag 
         Alignment       =   2  'Center
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
         Index           =   5
         Left            =   4830
         MaxLength       =   4
         TabIndex        =   20
         Top             =   360
         Width           =   720
      End
      Begin VB.TextBox txt_Lun_tag 
         Alignment       =   2  'Center
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
         Index           =   4
         Left            =   4125
         MaxLength       =   4
         TabIndex        =   19
         Top             =   360
         Width           =   720
      End
      Begin VB.TextBox txt_Lun_tag 
         Alignment       =   2  'Center
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
         Index           =   3
         Left            =   3420
         MaxLength       =   4
         TabIndex        =   18
         Top             =   360
         Width           =   720
      End
      Begin VB.TextBox txt_Lun_tag 
         Alignment       =   2  'Center
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
         Index           =   2
         Left            =   2715
         MaxLength       =   4
         TabIndex        =   17
         Top             =   360
         Width           =   720
      End
      Begin VB.TextBox txt_Lun_tag 
         Alignment       =   2  'Center
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
         Index           =   1
         Left            =   2010
         MaxLength       =   4
         TabIndex        =   16
         Top             =   360
         Width           =   720
      End
      Begin VB.TextBox txt_Lun_tag 
         Alignment       =   2  'Center
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
         Index           =   0
         Left            =   1305
         MaxLength       =   4
         TabIndex        =   15
         Top             =   360
         Width           =   720
      End
      Begin VB.Label Label3 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H00B5717B&
         BorderStyle     =   1  'Fixed Single
         Caption         =   "Velocità"
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
         Height          =   360
         Left            =   45
         TabIndex        =   101
         Top             =   780
         Width           =   1245
      End
      Begin VB.Label Label1 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H00B5717B&
         BorderStyle     =   1  'Fixed Single
         Caption         =   "Lunghezza"
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
         Height          =   360
         Left            =   45
         TabIndex        =   96
         Top             =   375
         Width           =   1245
      End
   End
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
      Height          =   4530
      Index           =   1
      Left            =   5985
      TabIndex        =   58
      Top             =   330
      Width           =   5985
      Begin VB.TextBox txt_cir_lam_def 
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
         MaxLength       =   3
         TabIndex        =   78
         TabStop         =   0   'False
         Top             =   3937
         Width           =   915
      End
      Begin VB.TextBox txt_cir_lam_att 
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
         MaxLength       =   3
         TabIndex        =   14
         Top             =   3937
         Width           =   915
      End
      Begin VB.TextBox txt_sog_err_ins_def 
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
         TabIndex        =   73
         TabStop         =   0   'False
         Top             =   3510
         Width           =   915
      End
      Begin VB.TextBox txt_sog_err_ins_att 
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
         Top             =   3510
         Width           =   915
      End
      Begin VB.TextBox txt_ang_sin_def 
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
         MaxLength       =   3
         TabIndex        =   72
         TabStop         =   0   'False
         Top             =   3090
         Width           =   915
      End
      Begin VB.TextBox txt_ang_sin_att 
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
         MaxLength       =   3
         TabIndex        =   12
         Top             =   3090
         Width           =   915
      End
      Begin VB.TextBox txt_fat_mol_enc_pull_roll_def 
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
         MaxLength       =   1
         TabIndex        =   62
         TabStop         =   0   'False
         Top             =   2670
         Width           =   915
      End
      Begin VB.TextBox txt_fat_mol_enc_pull_roll_att 
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
         MaxLength       =   1
         TabIndex        =   11
         Top             =   2670
         Width           =   915
      End
      Begin VB.TextBox txt_fat_mol_enc_ruo_mis_att 
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
         MaxLength       =   1
         TabIndex        =   10
         Top             =   2250
         Width           =   915
      End
      Begin VB.TextBox txt_fat_mol_enc_lam_att 
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
         MaxLength       =   1
         TabIndex        =   9
         Top             =   1830
         Width           =   915
      End
      Begin VB.TextBox txt_imp_gir_enc_pull_roll_att 
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
         TabIndex        =   8
         Top             =   1410
         Width           =   915
      End
      Begin VB.TextBox txt_fat_mol_enc_ruo_mis_def 
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
         MaxLength       =   1
         TabIndex        =   61
         TabStop         =   0   'False
         Top             =   2250
         Width           =   915
      End
      Begin VB.TextBox txt_fat_mol_enc_lam_def 
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
         MaxLength       =   1
         TabIndex        =   60
         TabStop         =   0   'False
         Top             =   1830
         Width           =   915
      End
      Begin VB.TextBox txt_imp_gir_enc_pull_roll_def 
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
         Left            =   3645
         MaxLength       =   4
         TabIndex        =   59
         TabStop         =   0   'False
         Top             =   1410
         Width           =   915
      End
      Begin VB.Label Label59 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Circonferenza lame"
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
         Top             =   4027
         Width           =   2325
      End
      Begin VB.Label Label31 
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
         Left            =   4650
         TabIndex        =   79
         Top             =   4027
         Width           =   375
      End
      Begin VB.Label Label9 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Soglia per err. inseguimento"
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
         TabIndex        =   77
         Top             =   3600
         Width           =   2325
      End
      Begin VB.Label Label56 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Angolo di sincronismo"
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
         TabIndex        =   76
         Top             =   3180
         Width           =   2325
      End
      Begin VB.Label Label6 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "0-2047"
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
         Left            =   4650
         TabIndex        =   75
         Top             =   3600
         Width           =   645
      End
      Begin VB.Label Label22 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "gradi (°)"
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
         Left            =   4650
         TabIndex        =   74
         Top             =   3150
         Width           =   615
      End
      Begin VB.Label Label20 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "1; 2; 4"
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
         Left            =   4650
         TabIndex        =   71
         Top             =   2370
         Width           =   765
      End
      Begin VB.Label Label14 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "1; 2; 4"
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
         Left            =   4650
         TabIndex        =   70
         Top             =   1920
         Width           =   510
      End
      Begin VB.Label Label5 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "1; 2; 4"
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
         Left            =   4650
         TabIndex        =   69
         Top             =   2790
         Width           =   765
      End
      Begin VB.Label Label25 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Imp/giro encoder pull_roll"
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
         TabIndex        =   68
         Top             =   1470
         Width           =   2325
      End
      Begin VB.Label Label24 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Fatt. molt. encoder lame"
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
         Top             =   1890
         Width           =   2325
      End
      Begin VB.Label Label61 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Fatt. molt. encoder ruota misura"
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
         TabIndex        =   66
         Top             =   2340
         Width           =   2565
      End
      Begin VB.Label Label32 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Fatt. molt. encoder pull_roll"
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
         TabIndex        =   65
         Top             =   2760
         Width           =   2325
      End
      Begin VB.Label Label7 
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
         Left            =   2865
         TabIndex        =   64
         Top             =   1050
         Width           =   645
      End
      Begin VB.Label Label4 
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
         Left            =   3750
         TabIndex        =   63
         Top             =   1050
         Width           =   705
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
      Left            =   1650
      TabIndex        =   56
      Top             =   7320
      Width           =   645
   End
   Begin VB.Frame Frame3 
      BackColor       =   &H00FFFFFF&
      Caption         =   "PARAMETRI PER LIVELLO DI TAGLIO"
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
      Height          =   4530
      Left            =   0
      TabIndex        =   45
      Top             =   330
      Width           =   5970
      Begin VB.TextBox txt_rap_rid_mot_lam_def 
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
         Left            =   3540
         MaxLength       =   5
         TabIndex        =   97
         TabStop         =   0   'False
         Top             =   3937
         Width           =   915
      End
      Begin VB.TextBox txt_rap_rid_mot_lam_att 
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
         Left            =   2610
         MaxLength       =   5
         TabIndex        =   7
         Top             =   3937
         Width           =   915
      End
      Begin VB.TextBox txt_imp_gir_enc_lam_def 
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
         Left            =   3540
         MaxLength       =   4
         TabIndex        =   93
         TabStop         =   0   'False
         Top             =   3517
         Width           =   915
      End
      Begin VB.TextBox txt_imp_gir_enc_lam_att 
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
         Left            =   2610
         MaxLength       =   4
         TabIndex        =   6
         Top             =   3517
         Width           =   915
      End
      Begin VB.TextBox txt_imp_gir_enc_ruo_mis_att 
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
         Left            =   2610
         MaxLength       =   4
         TabIndex        =   3
         Top             =   2250
         Width           =   915
      End
      Begin VB.TextBox txt_vel_lam_jog_att 
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
         Left            =   2610
         MaxLength       =   3
         TabIndex        =   2
         Top             =   1830
         Width           =   915
      End
      Begin VB.TextBox txt_imp_gir_enc_ruo_mis_def 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00FFFFFF&
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
         Left            =   3540
         MaxLength       =   4
         TabIndex        =   85
         TabStop         =   0   'False
         Top             =   2250
         Width           =   915
      End
      Begin VB.TextBox txt_vel_lam_jog_def 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00FFFFFF&
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
         Left            =   3540
         MaxLength       =   3
         TabIndex        =   84
         TabStop         =   0   'False
         Top             =   1830
         Width           =   915
      End
      Begin VB.TextBox txt_dia_ruo_mis_def 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00FFFFFF&
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
         Left            =   3540
         MaxLength       =   5
         TabIndex        =   83
         TabStop         =   0   'False
         Top             =   1410
         Width           =   915
      End
      Begin VB.TextBox txt_dia_ruo_mis_att 
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
         Left            =   2610
         MaxLength       =   5
         TabIndex        =   1
         Top             =   1410
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
         Left            =   20
         TabIndex        =   54
         Top             =   360
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
            TabIndex        =   57
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
            TabIndex        =   55
            Top             =   120
            Width           =   2175
         End
      End
      Begin VB.TextBox txt_dis_fot_lam_def 
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
         Left            =   3540
         MaxLength       =   5
         TabIndex        =   51
         TabStop         =   0   'False
         Top             =   2670
         Width           =   915
      End
      Begin VB.TextBox txt_dia_pull_roll_def 
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
         Left            =   3540
         MaxLength       =   5
         TabIndex        =   50
         TabStop         =   0   'False
         Top             =   3090
         Width           =   915
      End
      Begin VB.TextBox txt_dis_fot_lam_att 
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
         Left            =   2610
         MaxLength       =   5
         TabIndex        =   4
         Top             =   2670
         Width           =   915
      End
      Begin VB.TextBox txt_dia_pull_roll_att 
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
         Left            =   2610
         MaxLength       =   5
         TabIndex        =   5
         Top             =   3090
         Width           =   915
      End
      Begin VB.Label lab_rap_rid_mot_lam 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "9,9999"
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
         Left            =   5265
         TabIndex        =   100
         Top             =   4027
         Width           =   525
      End
      Begin VB.Label Label15 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "/ 10000 ="
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
         Left            =   4545
         TabIndex        =   99
         Top             =   4027
         Width           =   735
      End
      Begin VB.Label Label58 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Rap.to di riduzione mot/lame"
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
         TabIndex        =   98
         Top             =   4027
         Width           =   2325
      End
      Begin VB.Label Label23 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Imp/giro encoder lame"
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
         TabIndex        =   94
         Top             =   3607
         Width           =   2325
      End
      Begin VB.Label Label2 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Imp/giro encoder ruota misura"
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
         TabIndex        =   92
         Top             =   2340
         Width           =   2715
      End
      Begin VB.Label Label11 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Velocità lame in Jog "
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
         TabIndex        =   91
         Top             =   1920
         Width           =   2715
      End
      Begin VB.Label Label36 
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
         Left            =   4545
         TabIndex        =   90
         Top             =   1920
         Width           =   450
      End
      Begin VB.Label Label43 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Diametro ruota di misura"
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
         TabIndex        =   89
         Top             =   1500
         Width           =   2715
      End
      Begin VB.Label Label66 
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
         Left            =   5655
         TabIndex        =   88
         Top             =   1500
         Width           =   330
      End
      Begin VB.Label Label37 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "/ 100 ="
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
         Left            =   4530
         TabIndex        =   87
         Top             =   1500
         Width           =   525
      End
      Begin VB.Label lab_dia_ruo_mis 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "999,99"
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
         Left            =   5070
         TabIndex        =   86
         Top             =   1500
         Width           =   525
      End
      Begin VB.Label lab_dia_pull_roll 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "999,99"
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
         Left            =   5070
         TabIndex        =   82
         Top             =   3180
         Width           =   525
      End
      Begin VB.Label Label38 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "/ 100 ="
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
         Left            =   4530
         TabIndex        =   81
         Top             =   3180
         Width           =   525
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
         Left            =   2745
         TabIndex        =   53
         Top             =   1050
         Width           =   645
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
         Left            =   3650
         TabIndex        =   52
         Top             =   1050
         Width           =   705
      End
      Begin VB.Label Label13 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Diametro pull_roll"
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
         TabIndex        =   49
         Top             =   3180
         Width           =   2325
      End
      Begin VB.Label Label10 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Distanza fotocellula <-> lame"
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
         TabIndex        =   48
         Top             =   2760
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
         Left            =   5655
         TabIndex        =   47
         Top             =   3180
         Width           =   375
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
         Left            =   4545
         TabIndex        =   46
         Top             =   2760
         Width           =   375
      End
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "PARAMETRI CONTROLLO TAGLIO   PAG. 1/5"
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
      TabIndex        =   0
      Top             =   0
      Width           =   7785
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "set_par_sch_tag1.frx":0000
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "set_par_sch_tag1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1

Dim mod_in_cor_cur_tag As Boolean
'Iciliouff 03-11-2003 Aggiunta struttura temporanea per contenere la curva di taglio
'(precedente alla modifica operatore) finchè non viene scritta con successo nel Plc
Private cur_tag_ute(1 To 3) As tip_cur_tag
Private WithEvents rmt_set_par_plc As rmt
Attribute rmt_set_par_plc.VB_VarHelpID = -1
Private WithEvents rmt_set_par_sch As rmt
Attribute rmt_set_par_sch.VB_VarHelpID = -1

Sub enable_txt_cur_tag()
Dim i As Integer

For i = 0 To 14
  txt_Lun_tag(i).TabStop = True
  txt_vel_tag(i).TabStop = True
Next i
End Sub

Sub fil_par_cur_tag_liv(liv As Integer)
Dim i As Integer

For i = 1 To 15
  txt_Lun_tag(i - 1) = cur_tag(liv).Lun_tag(i)
  txt_vel_tag(i - 1) = cur_tag(liv).vel_max(i)
Next i
End Sub
Sub copy_par_cur_tag(ByVal liv As Integer)
'Iciliouff 03-11-2003
'Copia la curva di taglio attiva in struttura temporanea
Dim i As Integer

  cur_tag_ute(liv).descr = cur_tag(liv).descr
  For i = 1 To 15
    cur_tag_ute(liv).Lun_tag(i) = cur_tag(liv).Lun_tag(i)
    cur_tag_ute(liv).vel_max(i) = cur_tag(liv).vel_max(i)
  Next i

End Sub

Sub roll_back_par_cur_tag(ByVal liv As Integer)
'Iciliouff 03-11-2003
'Roll-back della curva di taglio temporanea in struttura attiva su STAG
Dim i As Integer

  cur_tag(liv).descr = cur_tag_ute(liv).descr
  For i = 1 To 15
    cur_tag(liv).Lun_tag(i) = cur_tag_ute(liv).Lun_tag(i)
    cur_tag(liv).vel_max(i) = cur_tag_ute(liv).vel_max(i)
  Next i

End Sub
Sub leg_par_cur_tag(liv As Integer)
'Iciliouff 03-11-2003
'Legge la curva di taglio dalla form
Dim i As Integer

For i = 1 To 15
  cur_tag(liv).Lun_tag(i) = val(txt_Lun_tag(i - 1))
  cur_tag(liv).vel_max(i) = val(txt_vel_tag(i - 1))
Next i

End Sub

Sub scr_par_cur_tag(liv As Integer)
Dim i As Integer

For i = 1 To 15
  cur_tag(liv).Lun_tag(i) = val(txt_Lun_tag(i - 1))
  cur_tag(liv).vel_max(i) = val(txt_vel_tag(i - 1))
Next i
For i = 1 To val(par_mac.car_tec.num_liv_tag)
  If i <> liv And cur_tag(i).descr = cur_tag(liv).descr Then cur_tag(i) = cur_tag(liv)
Next i
scr_par_cur_tag_dbf liv
End Sub
Sub scr_par_in_dbf(liv As Integer)
'Iciliouff 03-11-2003
'Scrittura parametri su dbf
Dim i As Integer

  
  For i = 1 To val(par_mac.car_tec.num_liv_tag)
    If i <> liv And cur_tag(i).descr = cur_tag(liv).descr Then cur_tag(i) = cur_tag(liv)
  Next i
  scr_par_cur_tag_dbf liv
  
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
If mod_in_cor Or mod_in_cor_cur_tag Then Exit Function
If Not sta_tag.com_OK Then messaggio msg("com_int_con_mac"), True: Exit Function
ver_ese_ope = True

End Function

Private Sub eve_key_F8(Shift As Integer)
If Not mod_in_cor And Not mod_in_cor_cur_tag Then
  Select Case Shift
    Case 0
    
    Case 2
      eve_key.chg pag_lis_ese
    Case Else
    
  End Select
End If
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



Private Sub rmt_set_par_plc_retbad(err As String)
'Gestisce la risposta negativa da PLC

  If rmt_set_par_plc.ric = tip_ope_PLC.PARAM_CUR_TAG Then
    'La curva di taglio non è stata scritta con successo in Plc, si recuperano i parametri
    roll_back_par_cur_tag (rmt_set_par_plc.liv)
    rmt_set_par_plc.resetta
    If err <> "" Then messaggio err, True
  Else
  End If
  
End Sub

Private Sub rmt_set_par_plc_retok(s As String)
'Gestisce la risposta positiva da PLC

  If rmt_set_par_plc.ric = tip_ope_PLC.PARAM_CUR_TAG Then
    'La curva di taglio è stata scritta con successo in Plc, si salvano i parametri
    scr_par_in_dbf (rmt_set_par_plc.liv)
    rmt_set_par_plc.resetta
    If s <> "" Then messaggio s, False
  Else
  End If
  
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

Private Sub txt_ang_sin_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_cir_lam_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_dia_pull_roll_att_KeyUp(KeyCode As Integer, Shift As Integer)
lab_dia_pull_roll = val(txt_dia_pull_roll_att) / 100
End Sub

Private Sub txt_dia_ruo_mis_att_KeyUp(KeyCode As Integer, Shift As Integer)
lab_dia_ruo_mis = val(txt_dia_ruo_mis_att) / 100
End Sub

Private Sub txt_Lun_tag_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_rap_rid_mot_lam_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_rap_rid_mot_lam_att_KeyUp(KeyCode As Integer, Shift As Integer)
lab_rap_rid_mot_lam = val(txt_rap_rid_mot_lam_att) / 10000
End Sub


Private Sub txt_rap_rid_mot_lam_att_LostFocus()
'If mod_in_cor And txt_rap_rid_mot_lam_att.Text <> par_mac.sch_tag.sch_tag_liv(txt_liv_tag).rap_rid_mot_lam Then
'       par_mac.sch_tag.sch_tag_liv(txt_liv_tag.Text).rap_rid_mot_lam = txt_rap_rid_mot_lam_att.Text
'       setFileINIString sec_liv + txt_liv_tag, "rap_rid_mot_lam", par_mac.sch_tag.sch_tag_liv(txt_liv_tag).rap_rid_mot_lam, app.EXEName
'End If

End Sub
Private Sub txt_sog_err_ins_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub



















Sub att_in_def(tot As Boolean)
Dim i As Integer

  'scrittura delle textbox
  txt_dia_ruo_mis_def = txt_dia_ruo_mis_att
  txt_vel_lam_jog_def = txt_vel_lam_jog_att
  txt_imp_gir_enc_ruo_mis_def = txt_imp_gir_enc_ruo_mis_att
  txt_dis_fot_lam_def = txt_dis_fot_lam_att
  txt_dia_pull_roll_def = txt_dia_pull_roll_att
  'txt_gir_max_mot_lam_def = txt_gir_max_mot_lam_att 'Arturo uff 21-02-03
  txt_imp_gir_enc_lam_def = txt_imp_gir_enc_lam_att
  txt_imp_gir_enc_pull_roll_def = txt_imp_gir_enc_pull_roll_att
  txt_fat_mol_enc_lam_def = txt_fat_mol_enc_lam_att
  txt_fat_mol_enc_ruo_mis_def = txt_fat_mol_enc_ruo_mis_att
  txt_fat_mol_enc_pull_roll_def = txt_fat_mol_enc_pull_roll_att
  txt_ang_sin_def = txt_ang_sin_att
  txt_cir_lam_def = txt_cir_lam_att
  txt_rap_rid_mot_lam_def = txt_rap_rid_mot_lam_att
  txt_sog_err_ins_def = txt_sog_err_ins_att
  
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
          set_par_sch_tag2.att_in_def (True)
          set_par_sch_tag4.att_in_def (True)
          'RFAZZI_FI19/03/2008
          set_par_sch_tag5.att_in_def (True)
    End If
  Else
    scr_par_sch_tag_def_liv_ini txt_liv_tag
  End If

End Sub








Sub fil_par_sch_tag_def_liv(liv As Integer)
'caricamento dei parametri nella form scheda di taglio per livello

txt_dia_ruo_mis_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).dia_ruo_mis)
txt_vel_lam_jog_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).vel_lam_jog)
txt_imp_gir_enc_ruo_mis_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).imp_gir_enc_ruo_mis)
txt_dis_fot_lam_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).dis_fot_lam)
txt_dia_pull_roll_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).dia_pull_roll)
'txt_gir_max_mot_lam_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).gir_max_mot_lam)  'Arturo uff 21-02-03
txt_imp_gir_enc_lam_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).imp_gir_enc_lam)
txt_imp_gir_enc_pull_roll_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).imp_gir_enc_pull_roll)
txt_fat_mol_enc_lam_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).fat_mol_enc_lam)
txt_fat_mol_enc_ruo_mis_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).fat_mol_enc_ruo_mis)
txt_fat_mol_enc_pull_roll_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).fat_mol_enc_pull_roll)
txt_ang_sin_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).ang_sincr)
txt_cir_lam_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).cir_lam)
txt_rap_rid_mot_lam_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).rap_rid_mot_lam)
txt_sog_err_ins_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_err_ins)

End Sub

Sub fil_par_sch_tag_liv(liv As Integer)

'caricamento dei parametri nella form scheda di taglio per livello

txt_dia_ruo_mis_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).dia_ruo_mis)
lab_dia_ruo_mis = val(txt_dia_ruo_mis_att) / 100
txt_vel_lam_jog_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).vel_lam_jog)
txt_imp_gir_enc_ruo_mis_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).imp_gir_enc_ruo_mis)
txt_dis_fot_lam_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).dis_fot_lam)
txt_dia_pull_roll_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).dia_pull_roll)
lab_dia_pull_roll = val(txt_dia_pull_roll_att) / 100
'txt_gir_max_mot_lam_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).gir_max_mot_lam)   'Arturo uff 21-02-03
txt_imp_gir_enc_lam_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).imp_gir_enc_lam)
txt_imp_gir_enc_pull_roll_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).imp_gir_enc_pull_roll)
txt_fat_mol_enc_lam_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).fat_mol_enc_lam)
txt_fat_mol_enc_ruo_mis_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).fat_mol_enc_ruo_mis)
txt_fat_mol_enc_pull_roll_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).fat_mol_enc_pull_roll)
txt_ang_sin_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).ang_sincr)
txt_cir_lam_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).cir_lam)
txt_sog_err_ins_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_err_ins)
txt_rap_rid_mot_lam_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).rap_rid_mot_lam)
lab_rap_rid_mot_lam = val(txt_rap_rid_mot_lam_att) / 10000

End Sub








Sub scr_par_def_ute_liv(liv As Integer)

  par_mac_ute.sch_tag_def.sch_tag_liv(liv).dia_ruo_mis = txt_dia_ruo_mis_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).vel_lam_jog = txt_vel_lam_jog_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).imp_gir_enc_ruo_mis = txt_imp_gir_enc_ruo_mis_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).dis_fot_lam = txt_dis_fot_lam_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).dia_pull_roll = txt_dia_pull_roll_def
  'par_mac_ute.sch_tag_def.sch_tag_liv(liv).gir_max_mot_lam = txt_gir_max_mot_lam_def   'Arturo uff 21-02-03
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).imp_gir_enc_lam = txt_imp_gir_enc_lam_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).imp_gir_enc_pull_roll = txt_imp_gir_enc_pull_roll_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).fat_mol_enc_lam = txt_fat_mol_enc_lam_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).fat_mol_enc_ruo_mis = txt_fat_mol_enc_ruo_mis_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).fat_mol_enc_pull_roll = txt_fat_mol_enc_pull_roll_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).rap_rid_mot_lam = txt_rap_rid_mot_lam_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).cir_lam = txt_cir_lam_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).ang_sincr = txt_ang_sin_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).sog_err_ins = txt_sog_err_ins_def
  
End Sub








Sub scr_par_att_ute_liv(liv As Integer)


'PARAMETRI PER LIVELLO DI TAGLIO

  par_mac_ute.sch_tag.sch_tag_liv(liv).dia_ruo_mis = txt_dia_ruo_mis_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).vel_lam_jog = txt_vel_lam_jog_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).imp_gir_enc_ruo_mis = txt_imp_gir_enc_ruo_mis_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).dis_fot_lam = txt_dis_fot_lam_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).dia_pull_roll = txt_dia_pull_roll_att
  'par_mac_ute.sch_tag.sch_tag_liv(liv).gir_max_mot_lam = txt_gir_max_mot_lam_att   'Arturo uff 21-02-03
  par_mac_ute.sch_tag.sch_tag_liv(liv).imp_gir_enc_lam = txt_imp_gir_enc_lam_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).imp_gir_enc_pull_roll = txt_imp_gir_enc_pull_roll_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).fat_mol_enc_lam = txt_fat_mol_enc_lam_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).fat_mol_enc_ruo_mis = txt_fat_mol_enc_ruo_mis_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).fat_mol_enc_pull_roll = txt_fat_mol_enc_pull_roll_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).rap_rid_mot_lam = txt_rap_rid_mot_lam_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).cir_lam = txt_cir_lam_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).ang_sincr = txt_ang_sin_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).sog_err_ins = txt_sog_err_ins_att
   
  End Sub




Private Sub eve_key_esc(Shift As Integer)
Dim i As Integer
Select Case Shift
  Case 0
  If mod_in_cor Or mod_in_cor_cur_tag Then
      esc_pre = True
      If mod_in_cor Then
          mod_in_cor = False
          par_mac_ute = par_mac
          For i = 1 To par_mac.car_tec.num_liv_tag
            par_mac.com_sch_tag(i).com_att = False
          Next i
          fil_par_sch_tag_liv (txt_liv_tag.Text)
      End If
      If mod_in_cor_cur_tag Then
        mod_in_cor_cur_tag = False
        fil_par_cur_tag_liv (txt_liv_tag.Text)
      End If
      disable_tab Me
      txt_idle.TabStop = True
      txt_idle.SetFocus
  End If
  Case Else
End Select
End Sub



Private Sub eve_key_F1(Shift As Integer)
Select Case Shift
  Case 0
  If Not mod_in_cor And Not mod_in_cor_cur_tag Then eve_key.chg set_par_mac
  Case Else
End Select
End Sub

Private Sub eve_key_F10(Shift As Integer)
Select Case Shift
  Case 0
  If Not mod_in_cor And Not mod_in_cor_cur_tag Then eve_key.chg pag_pro
  Case Else
End Select
End Sub

Private Sub eve_key_F2(Shift As Integer)
'modifica
Select Case Shift
Case 0 'modifica parametri configurazione
  If ver_ese_ope(False) Then
    esc_pre = False
    sal_in_cor = False
    txt_idle.TabStop = False
    enable_txt_pag_sch_tag Me
    txt_dia_ruo_mis_att.SetFocus
    mod_in_cor = True
  End If
Case 2 'modifica parametri curva di taglio
  If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Sub
  If mod_in_cor Or mod_in_cor_cur_tag Then Exit Sub
  If Not sta_tag.com_OK Then messaggio msg("com_int_con_mac"), True: Exit Sub
  esc_pre = False
  sal_in_cor = False
  txt_idle.TabStop = False
  enable_txt_cur_tag
  txt_Lun_tag(0).SetFocus
  mod_in_cor_cur_tag = True
Case Else
End Select
End Sub

Private Sub eve_key_F3(Shift As Integer)
'Select Case Shift
'Case 0

  'salva
  If (mod_in_cor And Shift = 0) Or (mod_in_cor_cur_tag And Shift = 2) Then
    sal_in_cor = True
    messaggio msg("ope_in_cor"), False, True
    Select Case Shift
    Case 0
      scr_par_att_ute_liv (txt_liv_tag.Text)
      'scrivo nella scheda di taglio
      num_com_sch_tag = 1
      CASO_SND_SCH_TAG = NOT_CFG
      'Iciliouff 03-11-2003 Gestione con RMT
      If rmt_set_par_sch.esegui(tip_cmd.CONFIGURA, GetTickCount(), val(txt_liv_tag.Text), 0) Then
        messaggio msg("inv_par_sch_tag"), False
      Else
        messaggio msg("imp_ese_ope"), True
      End If
'      snd_sch_tag val(txt_liv_tag.Text), CONFIGURA
      mod_in_cor = False
    Case 2
      'Iciliouff 03-11-2003 Richiesta di scrittura curva di taglio
      copy_par_cur_tag (val(txt_liv_tag.Text))
      leg_par_cur_tag (val(txt_liv_tag.Text))
      If Not rmt_set_par_plc.esegui(tip_ope_PLC.PARAM_CUR_TAG, GetTickCount(), val(txt_liv_tag.Text), 0) Then
        messaggio msg("imp_ese_ope"), False, False
      End If
'      scr_par_cur_tag (txt_liv_tag.Text)
'      snd_str_PLC PARAM_CUR_TAG, , livello
      mod_in_cor_cur_tag = False
    End Select
    
    If Screen.ActiveForm.name = Me.name Then
        disable_tab Me
        txt_idle.TabStop = True
        txt_idle.SetFocus
    End If
  End If
  
'  Case Else
'End Select
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
    If rmt_set_par_sch.esegui(tip_cmd.CONFIGURA, GetTickCount(), Livello, 0, True) Then
      messaggio msg("ope_in_cor"), False
    Else
      messaggio msg("imp_ese_ope"), False
    End If
  End If
Case Else
End Select
End Sub

Private Sub eve_key_F5(Shift As Integer)
'copia da attivi in default

If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Sub
If Not mod_in_cor And Not mod_in_cor_cur_tag Then
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
'cambio livello
Select Case Shift
  Case 0
  If Not mod_in_cor And Not mod_in_cor_cur_tag Then
    If par_mac.car_tec.num_liv_tag > 1 Then
      If Livello < par_mac.car_tec.num_liv_tag Then
          Livello = Livello + 1
      Else
          Livello = 1
      End If
      txt_liv_tag.Text = Livello
      fil_par_sch_tag_liv (txt_liv_tag.Text)
      fil_par_sch_tag_def_liv (txt_liv_tag.Text)
      fil_par_cur_tag_liv (txt_liv_tag.Text)
    End If
  End If
  Case Else
End Select
End Sub

Private Sub eve_key_F9(Shift As Integer)
'pagina successiva o lista ordini
If Not mod_in_cor And Not mod_in_cor_cur_tag Then
  Select Case Shift
    Case 0
      eve_key.chg set_par_sch_tag2
    Case 2
      eve_key.chg pag_lis_ord
    Case Else
    
  End Select
End If
End Sub

Private Sub Form_Activate()
If mod_in_cor Then mod_in_cor = False
If sal_in_cor Then sal_in_cor = False
If esc_pre Then esc_pre = False
txt_liv_tag.Text = Livello
fil_par_sch_tag_def_liv (Livello)
fil_par_sch_tag_liv (Livello)
fil_par_cur_tag_liv (Livello)
txt_idle.SetFocus

End Sub

Private Sub Form_Load()
Debug.Print "Inizio Load " & Me.name
disable_tab Me

Set eve_key = New key
eve_key.create Me

'Iciliouff 03-11-2003
Set rmt_set_par_plc = New rmt
rmt_set_par_plc.set_plc_ute (1)
rmt_set_par_plc.name = "set_par_plc_01"
Set rmt_set_par_sch = New rmt
rmt_set_par_sch.set_plc_ute (2)
rmt_set_par_sch.name = "set_par_sch_01"

Carica Me
Debug.Print "Fine Load " & Me.name
Debug.Print ""

End Sub









Private Sub txt_dia_pull_roll_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_dia_pull_roll_att_LostFocus()
'If mod_in_cor And txt_dia_pull_roll_att.Text <> par_mac.sch_tag.sch_tag_liv(txt_liv_tag).dia_pull_roll Then
'       par_mac.sch_tag.sch_tag_liv(txt_liv_tag.Text).dia_pull_roll = txt_dia_pull_roll_att.Text
'       setFileINIString sec_liv + txt_liv_tag, "dia_pull_roll", par_mac.sch_tag.sch_tag_liv(txt_liv_tag).dia_pull_roll, app.EXEName
'End If
End Sub

Private Sub txt_dia_ruo_mis_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_dia_ruo_mis_att_LostFocus()
'If mod_in_cor And txt_dia_ruo_mis_att.Text <> par_mac.sch_tag.dia_ruo_mis Then
'       'invio dato alla scheda di taglio
'       par_mac.sch_tag.dia_ruo_mis = txt_dia_ruo_mis_att
'       setFileINIString sec, "dia_ruo_mis", par_mac.sch_tag.dia_ruo_mis, app.EXEName
'End If
End Sub

Private Sub txt_dis_fot_lam_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub








Private Sub txt_dis_fot_lam_att_LostFocus()
'If mod_in_cor And txt_dis_fot_lam_att.Text <> par_mac.sch_tag.sch_tag_liv(txt_liv_tag).dis_FTC_lam Then
'      par_mac.sch_tag.sch_tag_liv(txt_liv_tag.Text).dis_FTC_lam = txt_dis_fot_lam_att.Text
'      setFileINIString sec_liv + txt_liv_tag, "dis_FTC_lam", par_mac.sch_tag.sch_tag_liv(txt_liv_tag).dis_FTC_lam, app.EXEName
'End If
End Sub

Private Sub txt_fat_mol_enc_lam_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_fat_mol_enc_lam_att_LostFocus()
'If mod_in_cor And txt_fat_mol_enc_lam_att.Text <> par_mac.sch_tag.sch_tag_liv(txt_liv_tag).fat_mol_enc_lam Then
'       par_mac.sch_tag.sch_tag_liv(txt_liv_tag.Text).fat_mol_enc_lam = txt_fat_mol_enc_lam_att.Text
'       setFileINIString sec_liv + txt_liv_tag, "fat_mol_enc_lam", par_mac.sch_tag.sch_tag_liv(txt_liv_tag).fat_mol_enc_lam, app.EXEName
'End If

End Sub


Private Sub txt_fat_mol_enc_pull_roll_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_fat_mol_enc_pull_roll_att_LostFocus()
'If mod_in_cor And txt_fat_mol_enc_pull_roll_att.Text <> par_mac.sch_tag.sch_tag_liv(txt_liv_tag).fat_mol_enc_pull_roll Then
'       par_mac.sch_tag.sch_tag_liv(txt_liv_tag.Text).fat_mol_enc_pull_roll = txt_fat_mol_enc_pull_roll_att.Text
'       setFileINIString sec_liv + txt_liv_tag, "fat_mol_enc_pull_roll", par_mac.sch_tag.sch_tag_liv(txt_liv_tag).fat_mol_enc_pull_roll, app.EXEName
'End If

End Sub

Private Sub txt_fat_mol_enc_ruo_mis_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub





Private Sub txt_fat_mol_enc_ruo_mis_att_LostFocus()
'If mod_in_cor And txt_fat_mol_enc_ruo_mis_att.Text <> par_mac.sch_tag.sch_tag_liv(txt_liv_tag).fat_mol_enc_ruo_mis Then
'       par_mac.sch_tag.sch_tag_liv(txt_liv_tag.Text).fat_mol_enc_ruo_mis = txt_fat_mol_enc_ruo_mis_att.Text
'       setFileINIString sec_liv + txt_liv_tag, "fat_mol_enc_ruo_mis", par_mac.sch_tag.sch_tag_liv(txt_liv_tag).fat_mol_enc_ruo_mis, app.EXEName
'End If

End Sub

Private Sub txt_gir_max_mot_lam_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_gir_max_mot_lam_att_LostFocus()
'If mod_in_cor And txt_gir_max_mot_lam_att.Text <> par_mac.sch_tag.sch_tag_liv(txt_liv_tag).gir_max_mot_lam Then
'       par_mac.sch_tag.sch_tag_liv(txt_liv_tag.Text).gir_max_mot_lam = txt_gir_max_mot_lam_att.Text
'       setFileINIString sec_liv + txt_liv_tag, "gir_max_mot_lam", par_mac.sch_tag.sch_tag_liv(txt_liv_tag).gir_max_mot_lam, app.EXEName
'End If

End Sub


Private Sub txt_imp_gir_enc_lam_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_imp_gir_enc_lam_att_LostFocus()
'If mod_in_cor And txt_imp_gir_enc_lam_att.Text <> par_mac.sch_tag.sch_tag_liv(txt_liv_tag).imp_gir_enc_lam Then
'       par_mac.sch_tag.sch_tag_liv(txt_liv_tag.Text).imp_gir_enc_lam = txt_imp_gir_enc_lam_att.Text
'       setFileINIString sec_liv + txt_liv_tag, "imp_gir_enc_lam", par_mac.sch_tag.sch_tag_liv(txt_liv_tag).imp_gir_enc_lam, app.EXEName
'End If

End Sub

Private Sub txt_imp_gir_enc_pull_roll_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_imp_gir_enc_pull_roll_att_LostFocus()
'If mod_in_cor And txt_imp_gir_enc_pull_roll_att.Text <> par_mac.sch_tag.sch_tag_liv(txt_liv_tag).imp_gir_enc_pull_roll Then
'       par_mac.sch_tag.sch_tag_liv(txt_liv_tag.Text).imp_gir_enc_pull_roll = txt_imp_gir_enc_pull_roll_att.Text
'       setFileINIString sec_liv + txt_liv_tag, "imp_gir_enc_pull_roll", par_mac.sch_tag.sch_tag_liv(txt_liv_tag).imp_gir_enc_pull_roll, app.EXEName
'End If

End Sub

Private Sub txt_imp_gir_enc_ruo_mis_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub




Private Sub txt_imp_gir_enc_ruo_mis_att_LostFocus()
'If mod_in_cor And txt_imp_gir_enc_ruo_mis_att.Text <> par_mac.sch_tag.imp_gir_enc_ruo_mis Then
'       'invio al PLC
'      par_mac.sch_tag.imp_gir_enc_ruo_mis = txt_imp_gir_enc_ruo_mis_att.Text
'       setFileINIString sec, "imp_gir_enc_ruo_mis", par_mac.sch_tag.imp_gir_enc_ruo_mis, app.EXEName
'End If
End Sub





Private Sub txt_vel_lam_jog_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_vel_lam_jog_att_LostFocus()
'If mod_in_cor And txt_vel_lam_jog_att.Text <> par_mac.sch_tag.jog_lam Then
'       'invio al PLC
'       par_mac.sch_tag.jog_lam = txt_vel_lam_jog_att.Text
'       setFileINIString sec, "jog_lam", par_mac.sch_tag.jog_lam, app.EXEName
'End If
End Sub























Private Sub txt_vel_tag_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


