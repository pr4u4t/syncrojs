VERSION 5.00
Begin VB.Form set_par_sch_tag3 
   Appearance      =   0  'Flat
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6240
   ClientLeft      =   195
   ClientTop       =   975
   ClientWidth     =   11955
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6240
   ScaleMode       =   0  'User
   ScaleWidth      =   20490.61
   ShowInTaskbar   =   0   'False
   Begin VB.Frame Frame1 
      BackColor       =   &H00FFFFFF&
      Caption         =   "CONFIGURAZIONI"
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
      Left            =   6930
      TabIndex        =   37
      Top             =   330
      Width           =   5040
      Begin VB.Frame frm_liv 
         BackColor       =   &H00FFFFFF&
         Caption         =   "LIVELLO 3"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   1815
         Index           =   2
         Left            =   255
         TabIndex        =   52
         Top             =   4005
         Width           =   4515
         Begin VB.Label l_con_att 
            BackColor       =   &H00FFFFFF&
            BackStyle       =   0  'Transparent
            Caption         =   "CONFIGURAZIONE ATTIVA"
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
            Left            =   165
            TabIndex        =   58
            Top             =   240
            Width           =   2715
         End
         Begin VB.Label lab_con_att 
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   285
            Index           =   2
            Left            =   165
            TabIndex        =   57
            Top             =   460
            Width           =   3825
         End
         Begin VB.Label l_con_def 
            BackColor       =   &H00FFFFFF&
            BackStyle       =   0  'Transparent
            Caption         =   "CONFIGURAZIONE DI DEFAULT"
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
            Left            =   165
            TabIndex        =   56
            Top             =   840
            Width           =   3030
         End
         Begin VB.Label lab_con_def 
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   285
            Index           =   2
            Left            =   165
            TabIndex        =   55
            Top             =   1065
            Width           =   3825
         End
         Begin VB.Label l_num_mot_abi 
            BackColor       =   &H00FFFFFF&
            BackStyle       =   0  'Transparent
            Caption         =   "NUMERO MOTORI ABILITATI"
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
            Left            =   165
            TabIndex        =   54
            Top             =   1485
            Width           =   2835
         End
         Begin VB.Label lab_num_mot_abi 
            Alignment       =   2  'Center
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   285
            Index           =   2
            Left            =   3645
            TabIndex        =   53
            Top             =   1440
            Width           =   345
         End
      End
      Begin VB.Frame frm_liv 
         BackColor       =   &H00FFFFFF&
         Caption         =   "LIVELLO 2"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   1815
         Index           =   1
         Left            =   255
         TabIndex        =   45
         Top             =   2130
         Width           =   4515
         Begin VB.Label l_con_att 
            BackColor       =   &H00FFFFFF&
            BackStyle       =   0  'Transparent
            Caption         =   "CONFIGURAZIONE ATTIVA"
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
            Left            =   165
            TabIndex        =   51
            Top             =   240
            Width           =   2715
         End
         Begin VB.Label lab_con_att 
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   285
            Index           =   1
            Left            =   165
            TabIndex        =   50
            Top             =   460
            Width           =   3825
         End
         Begin VB.Label l_con_def 
            BackColor       =   &H00FFFFFF&
            BackStyle       =   0  'Transparent
            Caption         =   "CONFIGURAZIONE DI DEFAULT"
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
            Left            =   165
            TabIndex        =   49
            Top             =   840
            Width           =   3030
         End
         Begin VB.Label lab_con_def 
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   285
            Index           =   1
            Left            =   165
            TabIndex        =   48
            Top             =   1065
            Width           =   3825
         End
         Begin VB.Label l_num_mot_abi 
            BackColor       =   &H00FFFFFF&
            BackStyle       =   0  'Transparent
            Caption         =   "NUMERO MOTORI ABILITATI"
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
            Left            =   165
            TabIndex        =   47
            Top             =   1485
            Width           =   2835
         End
         Begin VB.Label lab_num_mot_abi 
            Alignment       =   2  'Center
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   285
            Index           =   1
            Left            =   3645
            TabIndex        =   46
            Top             =   1440
            Width           =   345
         End
      End
      Begin VB.Frame frm_liv 
         BackColor       =   &H00FFFFFF&
         Caption         =   "LIVELLO 1"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   1815
         Index           =   0
         Left            =   255
         TabIndex        =   38
         Top             =   255
         Width           =   4515
         Begin VB.Label lab_num_mot_abi 
            Alignment       =   2  'Center
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   285
            Index           =   0
            Left            =   3645
            TabIndex        =   44
            Top             =   1440
            Width           =   345
         End
         Begin VB.Label l_num_mot_abi 
            BackColor       =   &H00FFFFFF&
            BackStyle       =   0  'Transparent
            Caption         =   "NUMERO MOTORI ABILITATI"
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
            Left            =   165
            TabIndex        =   43
            Top             =   1485
            Width           =   2835
         End
         Begin VB.Label lab_con_def 
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   285
            Index           =   0
            Left            =   165
            TabIndex        =   42
            Top             =   1065
            Width           =   3825
         End
         Begin VB.Label l_con_def 
            BackColor       =   &H00FFFFFF&
            BackStyle       =   0  'Transparent
            Caption         =   "CONFIGURAZIONE DI DEFAULT"
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
            Left            =   165
            TabIndex        =   41
            Top             =   840
            Width           =   3030
         End
         Begin VB.Label lab_con_att 
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   285
            Index           =   0
            Left            =   165
            TabIndex        =   40
            Top             =   460
            Width           =   3825
         End
         Begin VB.Label l_con_att 
            BackColor       =   &H00FFFFFF&
            BackStyle       =   0  'Transparent
            Caption         =   "CONFIGURAZIONE ATTIVA"
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
            Left            =   165
            TabIndex        =   39
            Top             =   240
            Width           =   2715
         End
      End
   End
   Begin VB.TextBox txt_idle 
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
      Left            =   480
      TabIndex        =   36
      Top             =   -5000
      Width           =   885
   End
   Begin VB.Frame Frame2 
      BackColor       =   &H00FFFFFF&
      Caption         =   "PARAMETRI DIPENDENTI DALLA CONFIGURAZIONE DELLA MACCHINA"
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
      Width           =   6915
      Begin VB.TextBox txt_gir_max_mot_lam_def 
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
         Left            =   4080
         MaxLength       =   5
         TabIndex        =   71
         TabStop         =   0   'False
         Top             =   5280
         Width           =   915
      End
      Begin VB.TextBox txt_gir_max_mot_lam_att 
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
         Left            =   3150
         MaxLength       =   5
         TabIndex        =   70
         Top             =   5280
         Width           =   915
      End
      Begin VB.TextBox txt_lim_lav_kv_def 
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
         Left            =   4080
         MaxLength       =   4
         TabIndex        =   65
         TabStop         =   0   'False
         Top             =   4875
         Width           =   915
      End
      Begin VB.TextBox txt_lim_lav_kv_att 
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
         Left            =   3150
         MaxLength       =   4
         TabIndex        =   10
         Top             =   4875
         Width           =   915
      End
      Begin VB.TextBox txt_kv_nor_def 
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
         Left            =   4080
         MaxLength       =   4
         TabIndex        =   63
         TabStop         =   0   'False
         Top             =   4455
         Width           =   915
      End
      Begin VB.TextBox txt_kv_nor_att 
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
         Left            =   3150
         MaxLength       =   4
         TabIndex        =   9
         Top             =   4455
         Width           =   915
      End
      Begin VB.TextBox txt_fil_kp_def 
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
         Left            =   4080
         MaxLength       =   4
         TabIndex        =   61
         TabStop         =   0   'False
         Top             =   4035
         Width           =   915
      End
      Begin VB.TextBox txt_fil_kp_att 
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
         Left            =   3150
         MaxLength       =   4
         TabIndex        =   8
         Top             =   4035
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
         TabIndex        =   33
         Top             =   450
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
            TabIndex        =   34
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
            TabIndex        =   35
            Top             =   120
            Width           =   2175
         End
      End
      Begin VB.TextBox txt_vel_max_tag_att 
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
         Left            =   3150
         MaxLength       =   3
         TabIndex        =   1
         Top             =   1095
         Width           =   915
      End
      Begin VB.TextBox txt_vel_max_tag_def 
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
         Left            =   4080
         MaxLength       =   3
         TabIndex        =   17
         TabStop         =   0   'False
         Top             =   1095
         Width           =   915
      End
      Begin VB.TextBox txt_kp_nor_att 
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
         Left            =   3150
         MaxLength       =   2
         TabIndex        =   6
         Top             =   3195
         Width           =   915
      End
      Begin VB.TextBox txt_kp_nor_def 
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
         Left            =   4080
         MaxLength       =   2
         TabIndex        =   16
         TabStop         =   0   'False
         Top             =   3195
         Width           =   915
      End
      Begin VB.TextBox txt_ki_nor_att 
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
         Left            =   3150
         MaxLength       =   4
         TabIndex        =   7
         Top             =   3615
         Width           =   915
      End
      Begin VB.TextBox txt_ki_nor_def 
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
         Left            =   4080
         MaxLength       =   4
         TabIndex        =   15
         TabStop         =   0   'False
         Top             =   3615
         Width           =   915
      End
      Begin VB.TextBox txt_tem_acc_tgl_cor_def 
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
         Left            =   4080
         MaxLength       =   4
         TabIndex        =   14
         TabStop         =   0   'False
         Top             =   2355
         Width           =   915
      End
      Begin VB.TextBox txt_tem_dec_tgl_cor_def 
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
         Left            =   4080
         MaxLength       =   4
         TabIndex        =   13
         TabStop         =   0   'False
         Top             =   2775
         Width           =   915
      End
      Begin VB.TextBox txt_tem_acc_tgl_cor_att 
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
         Left            =   3150
         MaxLength       =   4
         TabIndex        =   4
         Top             =   2355
         Width           =   915
      End
      Begin VB.TextBox txt_tem_dec_tgl_cor_att 
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
         Left            =   3150
         MaxLength       =   4
         TabIndex        =   5
         Top             =   2775
         Width           =   915
      End
      Begin VB.TextBox txt_tem_dec_tgl_lun_att 
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
         Left            =   3150
         MaxLength       =   4
         TabIndex        =   3
         Top             =   1935
         Width           =   915
      End
      Begin VB.TextBox txt_tem_acc_tgl_lun_att 
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
         Left            =   3150
         MaxLength       =   4
         TabIndex        =   2
         Top             =   1515
         Width           =   915
      End
      Begin VB.TextBox txt_tem_dec_tgl_lun_def 
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
         Left            =   4080
         MaxLength       =   4
         TabIndex        =   12
         TabStop         =   0   'False
         Top             =   1935
         Width           =   915
      End
      Begin VB.TextBox txt_tem_acc_tgl_lun_def 
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
         Left            =   4080
         MaxLength       =   4
         TabIndex        =   11
         TabStop         =   0   'False
         Top             =   1515
         Width           =   915
      End
      Begin VB.Label Label31 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "giri"
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
         Left            =   5100
         TabIndex        =   73
         Top             =   5370
         Width           =   375
      End
      Begin VB.Label Label15 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Giri massimi motore lame"
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
         Left            =   60
         TabIndex        =   69
         Top             =   5400
         Width           =   2325
      End
      Begin VB.Label Label14 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "1; 2; 3; 4; 5"
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
         Left            =   5085
         TabIndex        =   68
         Top             =   4125
         Width           =   1065
      End
      Begin VB.Label Label13 
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
         Left            =   5085
         TabIndex        =   67
         Top             =   4965
         Width           =   765
      End
      Begin VB.Label Label6 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Limite di lavoro del Kv"
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
         Top             =   4965
         Width           =   2325
      End
      Begin VB.Label Label3 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Kv normalizzato"
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
         TabIndex        =   64
         Top             =   4545
         Width           =   2325
      End
      Begin VB.Label Label2 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Filtro Kp"
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
         Top             =   4125
         Width           =   2325
      End
      Begin VB.Label Label1 
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
         Left            =   5085
         TabIndex        =   60
         Top             =   3705
         Width           =   465
      End
      Begin VB.Label lab_ki_nor 
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
         Left            =   5550
         TabIndex        =   59
         Top             =   3705
         Width           =   420
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
         Left            =   4230
         TabIndex        =   31
         Top             =   780
         Width           =   705
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
         Left            =   3300
         TabIndex        =   30
         Top             =   780
         Width           =   645
      End
      Begin VB.Label Label5 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Velocità massima taglierina"
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
         Top             =   1185
         Width           =   2715
      End
      Begin VB.Label Label49 
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
         Left            =   5085
         TabIndex        =   28
         Top             =   1185
         Width           =   495
      End
      Begin VB.Label Label39 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Kp normalizzato"
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
         TabIndex        =   27
         Top             =   3285
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
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   26
         Top             =   3705
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
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   25
         Top             =   1605
         Width           =   2565
      End
      Begin VB.Label Label9 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Tempo decelerazione tagli corti"
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
         TabIndex        =   24
         Top             =   2835
         Width           =   2580
      End
      Begin VB.Label Label10 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Tempo accelerazione tagli corti"
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
         Top             =   2415
         Width           =   2490
      End
      Begin VB.Label Label29 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Tempo decelerazione tagli lunghi"
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
         TabIndex        =   22
         Top             =   2025
         Width           =   2565
      End
      Begin VB.Label Label22 
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
         Left            =   5085
         TabIndex        =   21
         Top             =   2865
         Width           =   765
      End
      Begin VB.Label Label21 
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
         Left            =   5085
         TabIndex        =   20
         Top             =   2445
         Width           =   765
      End
      Begin VB.Label Label12 
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
         Left            =   5085
         TabIndex        =   19
         Top             =   2025
         Width           =   765
      End
      Begin VB.Label Label11 
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
         Left            =   5085
         TabIndex        =   18
         Top             =   1605
         Width           =   765
      End
   End
   Begin VB.Label Label17 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "giri"
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
      Left            =   5070
      TabIndex        =   72
      Top             =   5700
      Width           =   765
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "PARAMETRI CONTROLLO TAGLIO   PAG. 3/5"
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
      TabIndex        =   32
      Top             =   0
      Width           =   7785
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "set_par_sch_tag3.frx":0000
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "set_par_sch_tag3"
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

Dim ope As tip_ope
Sub del_txt()
txt_vel_max_tag_att.Text = ""
txt_tem_acc_tgl_lun_att.Text = ""
txt_tem_dec_tgl_lun_att.Text = ""
txt_tem_acc_tgl_cor_att.Text = ""
txt_tem_dec_tgl_cor_att.Text = ""
txt_kp_nor_att.Text = ""
txt_ki_nor_att.Text = ""
txt_fil_kp_att.Text = ""
txt_kv_nor_att.Text = ""
txt_lim_lav_kv_att.Text = ""
txt_gir_max_mot_lam_att.Text = "" 'Arturo uff 21-02-03
End Sub



Sub fil_par_sch_tag_def_liv(liv As Integer)
'caricamento dei parametri nella form scheda di taglio per livello

lab_con_def(liv - 1) = par_mac.config_sch_tag_def(liv).descr
txt_vel_max_tag_def = Trim(par_mac.par_cfg_def(liv).vel_max_tag)
txt_tem_acc_tgl_lun_def = Trim(par_mac.par_cfg_def(liv).tem_acc_tgl_lun)
txt_tem_dec_tgl_lun_def = Trim(par_mac.par_cfg_def(liv).tem_dec_tgl_lun)
txt_tem_acc_tgl_cor_def = Trim(par_mac.par_cfg_def(liv).tem_acc_tgl_cor)
txt_tem_dec_tgl_cor_def = Trim(par_mac.par_cfg_def(liv).tem_dec_tgl_cor)
txt_kp_nor_def = Trim(par_mac.par_cfg_def(liv).Kp_nor)
txt_ki_nor_def = Trim(par_mac.par_cfg_def(liv).Ki_nor)
txt_fil_kp_def = Trim(par_mac.par_cfg_def(liv).fil_kp)
txt_kv_nor_def = Trim(par_mac.par_cfg_def(liv).Kv_nor)
txt_lim_lav_kv_def = Trim(par_mac.par_cfg_def(liv).lim_lav_Kv)
txt_gir_max_mot_lam_def = Trim(par_mac.par_cfg_def(liv).gir_max_mot_lam) 'Arturo uff 21-02-03
End Sub

Sub ini_form()
Dim i As Integer

txt_liv_tag.Text = Livello
For i = 1 To par_mac.car_tec.num_liv_tag
  lab_con_att(i - 1) = par_mac.config_sch_tag_att(i).descr
  lab_con_def(i - 1) = par_mac.config_sch_tag_def(i).descr
  lab_num_mot_abi(i - 1) = par_abi.num_mot_abi(i)
'  frm_liv(i - 1).BackColor = &HFFFFFF
'  l_con_att(i - 1).ForeColor = &HFFFFFF
'  l_con_def(i - 1).ForeColor = &HFFFFFF
'  l_num_mot_abi(i - 1).ForeColor = &HFFFFFF
'  lab_con_att(i - 1).BackColor = &HFFFFFF
'  lab_con_def(i - 1).BackColor = &HFFFFFF
'  lab_num_mot_abi(i - 1).BackColor = &HFFFFFF
  If par_mac.config_sch_tag_att(i).num_mot_liv = par_mac.car_tec.num_mot_liv_tag Or Trim$(par_mac.config_sch_tag_att(i).descr) = "" Then
    frm_liv(i - 1).BackColor = &HFFFFFF
  Else
    frm_liv(i - 1).BackColor = &HB5717B
  End If
Next i
For i = par_mac.car_tec.num_liv_tag + 1 To 3
  frm_liv(i - 1).BackColor = &HFFFFFF
'  l_con_att(i - 1).ForeColor = &HFFFFFF
'  l_con_def(i - 1).ForeColor = &HFFFFFF
'  l_num_mot_abi(i - 1).ForeColor = &HFFFFFF
'  lab_con_att(i - 1).BackColor = &HFFFFFF
'  lab_con_def(i - 1).BackColor = &HFFFFFF
'  lab_num_mot_abi(i - 1).BackColor = &HFFFFFF
  lab_con_att(i - 1).Caption = ""
  lab_con_def(i - 1).Caption = ""
  lab_num_mot_abi(i - 1).Caption = ""
Next i
End Sub



Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
    If ope = Modifica Or ope = Inserimento Then
        ope = Nessuna
        par_mac_ute = par_mac
        fil_par_sch_tag_liv (txt_liv_tag.Text)
        fil_par_sch_tag_def_liv (txt_liv_tag.Text)
        disable_tab Me
        txt_idle.SetFocus
    End If
  Case Else
End Select
End Sub

Private Sub eve_key_F1(Shift As Integer)
Select Case Shift
  Case 0
    If Not ope = Inserimento And Not ope = Modifica Then eve_key.chg set_par_mac
  Case Else
End Select
End Sub

Private Sub eve_key_F10(Shift As Integer)
Select Case Shift
  Case 0
    If Not ope = Modifica And Not ope = Inserimento Then eve_key.chg pag_pro
  Case Else
End Select
End Sub


Private Sub eve_key_F2(Shift As Integer)
Select Case Shift
  Case 0
    'inserimento nuova configurazione
    If ver_ese_ope Then
      ope = Inserimento
      del_txt
      enable_txt_pag_sch_tag Me
      txt_vel_max_tag_att.SetFocus
    End If
  Case Else
End Select
End Sub


Function ver_ese_ope() As Boolean

ver_ese_ope = False
If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Function
If Not com_ini_sch_tag(Livello) Then messaggio msg("com_int_con_sch"), True: Exit Function
If Not sta_tag.SCH_TAG_LIV_PRONTA(Livello, 1) Then messaggio msg("sch_tag_del_liv") & " " & CStr(Livello) & " " & msg("non_pro"), True: Exit Function
If ope = Modifica Or ope = Inserimento Then Exit Function
If Not sta_tag.com_OK Then messaggio msg("com_int_con_mac"), True: Exit Function
ver_ese_ope = True

End Function
Private Sub eve_key_F3(Shift As Integer)
Select Case Shift
  Case 0
    'leggi configurazione
    If ver_ese_ope Then
      If Trim$(par_mac.config_sch_tag_att(Livello).descr) = "" Then
        messaggio msg("ins_cfg_liv"), True
      Else
        eve_key.chg leg_cfg, keep:=1
      End If
    End If
  Case Else
End Select
End Sub


Private Sub eve_key_F4(Shift As Integer)
Select Case Shift
  Case 0
    'modifica
    If ver_ese_ope Then
      If Trim$(par_mac.config_sch_tag_att(val(txt_liv_tag.Text)).descr) <> "" Then
        ope = Modifica
        enable_txt_pag_sch_tag Me
        txt_vel_max_tag_att.SetFocus
      Else
        messaggio msg("non_mod_cfg"), True, False
      End If
    End If
  Case Else
End Select
End Sub


Private Sub eve_key_F5(Shift As Integer)
Dim i As Integer
Dim liv As Integer

Select Case Shift
  Case 0
    'salva parametri dipendenti dalla configurazione nel dbf
    liv = val(txt_liv_tag.Text)
    Select Case ope
    
      Case Modifica
        messaggio msg("ope_in_cor"), False, False
        scr_par_att_ute_liv (liv)
        If Trim$(par_mac.config_sch_tag_att(liv).descr) = "DEFAULT" Then  'modifico una configurazione attiva che ha descrizione DEFAULT
            scr_par_def_ute_liv (liv)
        End If
        'comunico alla scheda di taglio i nuovi valori
        num_com_sch_tag = 1
        CASO_SND_SCH_TAG = MODIFICA_OLD_CFG
        'Iciliouff 03-11-2003 Gestione con RMT
        If rmt_set_par_sch.esegui(tip_cmd.CONFIGURA, GetTickCount(), liv, 0) Then
          messaggio msg("inv_par_sch_tag"), False
        Else
          messaggio msg("imp_ese_ope"), True
        End If
'        snd_sch_tag liv, CONFIGURA
        ope = Nessuna
        If Screen.ActiveForm.name = Me.name Then
            txt_idle.SetFocus
            disable_tab Me
        End If
        
      Case Inserimento
        messaggio msg("ope_in_cor"), False, False
        scr_par_att_ute_liv (liv)
        If Trim$(par_mac.config_sch_tag_att(liv).descr) = "" Then  'inserisco per la prima volta una configurazione per questo livello => è quella di DEFAULT che setto anche come attiva
            scr_par_def_ute_liv (liv)
            'comunico alla scheda di taglio i nuovi valori
            num_com_sch_tag = 1
            CASO_SND_SCH_TAG = INSERIMENTO_CFG_DEFAULT
            'Iciliouff 03-11-2003 Gestione con RMT
            If rmt_set_par_sch.esegui(tip_cmd.CONFIGURA, GetTickCount(), liv, 0) Then
              messaggio msg("inv_par_sch_tag"), False
            Else
              messaggio msg("imp_ese_ope"), True
            End If
'            snd_sch_tag liv, CONFIGURA
            ope = Nessuna
            If Screen.ActiveForm.name = Me.name Then
              txt_idle.SetFocus
              disable_tab Me
            End If
        Else 'inserisco una configurazione nuova per questo livello
            ope = Nessuna
            eve_key.chg sal_cfg, keep:=1
        End If
      
      Case Nessuna
        messaggio msg("non_sal_cfg"), True, False
    End Select
  Case Else
End Select

End Sub

Private Sub eve_key_F6(Shift As Integer)
Select Case Shift
  Case 0
    'cancella una configurazione dal dbf
    If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Sub
    If Not ope = Modifica And Not ope = Inserimento Then
      eve_key.chg del_cfg, keep:=1
    End If
  Case Else
End Select
End Sub


Private Sub eve_key_F7(Shift As Integer)
Select Case Shift
  Case 0
    'cambio livello
    If Not ope = Modifica And Not ope = Inserimento Then
        If par_mac.car_tec.num_liv_tag > 1 Then
          If Livello < par_mac.car_tec.num_liv_tag Then
              Livello = Livello + 1
              txt_liv_tag.Text = Livello
          Else:
              Livello = 1
              txt_liv_tag.Text = Livello
          End If
          fil_par_sch_tag_liv (txt_liv_tag.Text)
          fil_par_sch_tag_def_liv (txt_liv_tag.Text)
        End If
    End If
  Case Else
End Select
End Sub


Private Sub eve_key_F8(Shift As Integer)
'pagina precedente o ordini eseguiti
If Not ope = Modifica And Not ope = Inserimento Then
  Select Case Shift
  Case 0
    eve_key.chg set_par_sch_tag2
  Case 2
    eve_key.chg pag_lis_ese
  Case Else
  End Select
End If
End Sub


Private Sub eve_key_F9(Shift As Integer)
'pagina successiva o lista ordini
If Not ope = Modifica And Not ope = Inserimento Then
  Select Case Shift
  Case 0
    eve_key.chg set_par_sch_tag4
  Case 2
    eve_key.chg pag_lis_ord
  Case Else
  End Select
End If
End Sub


Private Sub Form_Activate()

ope = Nessuna
ini_form
fil_par_sch_tag_liv (Livello)
fil_par_sch_tag_def_liv (Livello)

If Screen.ActiveForm.name = Me.name Then
    txt_idle.SetFocus
    disable_tab Me
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


Private Sub Form_Load()
Debug.Print "Inizio Load " & Me.name
disable_tab Me
 
Set eve_key = New key
eve_key.create Me
'Iciliouff 03-11-2003
Set rmt_set_par_sch = New rmt
rmt_set_par_sch.set_plc_ute (2)
rmt_set_par_sch.name = "set_par_sch_03"

Carica Me
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub

Sub fil_par_sch_tag_liv(liv As Integer)

'caricamento dei parametri nella form scheda di taglio per livello
lab_con_att(liv - 1).Caption = Trim(par_mac.config_sch_tag_att(liv).descr)
txt_vel_max_tag_att = Trim(par_mac.par_cfg_att(liv).vel_max_tag)
txt_tem_acc_tgl_lun_att = Trim(par_mac.par_cfg_att(liv).tem_acc_tgl_lun)
txt_tem_dec_tgl_lun_att = Trim(par_mac.par_cfg_att(liv).tem_dec_tgl_lun)
txt_tem_acc_tgl_cor_att = Trim(par_mac.par_cfg_att(liv).tem_acc_tgl_cor)
txt_tem_dec_tgl_cor_att = Trim(par_mac.par_cfg_att(liv).tem_dec_tgl_cor)
txt_kp_nor_att = Trim(par_mac.par_cfg_att(liv).Kp_nor)
txt_ki_nor_att = Trim(par_mac.par_cfg_att(liv).Ki_nor)
lab_ki_nor = val(txt_ki_nor_att) / 10
txt_fil_kp_att = Trim(par_mac.par_cfg_att(liv).fil_kp)
txt_kv_nor_att = Trim(par_mac.par_cfg_att(liv).Kv_nor)
txt_lim_lav_kv_att = Trim(par_mac.par_cfg_att(liv).lim_lav_Kv)
txt_gir_max_mot_lam_att = Trim(par_mac.par_cfg_att(liv).gir_max_mot_lam) 'Arturo uff 21-02-03
End Sub
Sub scr_par_att_ute_liv(liv As Integer)


'PARAMETRI PER LIVELLO DI TAGLIO

  par_mac_ute.par_cfg_att(liv).vel_max_tag = txt_vel_max_tag_att
  par_mac_ute.par_cfg_att(liv).tem_acc_tgl_lun = txt_tem_acc_tgl_lun_att
  par_mac_ute.par_cfg_att(liv).tem_dec_tgl_lun = txt_tem_dec_tgl_lun_att
  par_mac_ute.par_cfg_att(liv).tem_acc_tgl_cor = txt_tem_acc_tgl_cor_att
  par_mac_ute.par_cfg_att(liv).tem_dec_tgl_cor = txt_tem_dec_tgl_cor_att
  par_mac_ute.par_cfg_att(liv).Kp_nor = txt_kp_nor_att
  par_mac_ute.par_cfg_att(liv).Ki_nor = txt_ki_nor_att
  par_mac_ute.par_cfg_att(liv).fil_kp = txt_fil_kp_att
  par_mac_ute.par_cfg_att(liv).Kv_nor = txt_kv_nor_att
  par_mac_ute.par_cfg_att(liv).lim_lav_Kv = txt_lim_lav_kv_att
  par_mac_ute.par_cfg_att(liv).gir_max_mot_lam = txt_gir_max_mot_lam_att 'Arturo uff 21-02-03
End Sub

Sub scr_par_def_ute_liv(liv As Integer)

  par_mac_ute.par_cfg_def(liv).vel_max_tag = par_mac_ute.par_cfg_att(liv).vel_max_tag
  par_mac_ute.par_cfg_def(liv).tem_acc_tgl_lun = par_mac_ute.par_cfg_att(liv).tem_acc_tgl_lun
  par_mac_ute.par_cfg_def(liv).tem_dec_tgl_lun = par_mac_ute.par_cfg_att(liv).tem_dec_tgl_lun
  par_mac_ute.par_cfg_def(liv).tem_acc_tgl_cor = par_mac_ute.par_cfg_att(liv).tem_acc_tgl_cor
  par_mac_ute.par_cfg_def(liv).tem_dec_tgl_cor = par_mac_ute.par_cfg_att(liv).tem_dec_tgl_cor
  par_mac_ute.par_cfg_def(liv).Kp_nor = par_mac_ute.par_cfg_att(liv).Kp_nor
  par_mac_ute.par_cfg_def(liv).Ki_nor = par_mac_ute.par_cfg_att(liv).Ki_nor
  par_mac_ute.par_cfg_def(liv).fil_kp = par_mac_ute.par_cfg_att(liv).fil_kp
  par_mac_ute.par_cfg_def(liv).Kv_nor = par_mac_ute.par_cfg_att(liv).Kv_nor
  par_mac_ute.par_cfg_def(liv).lim_lav_Kv = par_mac_ute.par_cfg_att(liv).lim_lav_Kv
  par_mac_ute.par_cfg_def(liv).gir_max_mot_lam = par_mac_ute.par_cfg_att(liv).gir_max_mot_lam 'Arturo uff 21-02-03
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

Private Sub txt_fil_kp_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_gir_max_mot_lam_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_ki_nor_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_ki_nor_att_KeyUp(KeyCode As Integer, Shift As Integer)
lab_ki_nor = val(txt_ki_nor_att) / 10
End Sub


Private Sub txt_kv_nor_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_lim_lav_kv_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_tem_acc_tgl_lun_att_GotFocus()
 ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_kp_nor_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_tem_acc_tgl_cor_att_GotFocus()
 ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_tem_dec_tgl_cor_att_GotFocus()
 ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_tem_dec_tgl_lun_att_GotFocus()
 ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_vel_max_tag_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub




