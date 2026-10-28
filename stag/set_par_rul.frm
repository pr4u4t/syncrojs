VERSION 5.00
Begin VB.Form set_par_rul 
   Appearance      =   0  'Flat
   BackColor       =   &H00FFFFC0&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6300
   ClientLeft      =   45
   ClientTop       =   2235
   ClientWidth     =   11955
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6300
   ScaleMode       =   0  'User
   ScaleWidth      =   11955
   ShowInTaskbar   =   0   'False
   Begin VB.Frame Frame1 
      BackColor       =   &H00C0FEFE&
      Caption         =   "ESITO DELLE COMUNICAZIONI"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   2085
      Left            =   0
      TabIndex        =   55
      Top             =   4320
      Width           =   11955
      Begin VB.Frame Frame5 
         BackColor       =   &H00C0FEFE&
         Caption         =   "RULLI DI INGRESSO"
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
         Height          =   1185
         Left            =   105
         TabIndex        =   64
         Top             =   400
         Width           =   5370
         Begin VB.Label lab_tra_rul 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Rullo 1"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   195
            Index           =   0
            Left            =   2925
            TabIndex        =   69
            Top             =   240
            Width           =   930
         End
         Begin VB.Label lab_tra_rul 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Rullo 2"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   195
            Index           =   1
            Left            =   3930
            TabIndex        =   68
            Top             =   240
            Width           =   930
         End
         Begin VB.Label lab_tra_ing 
            Alignment       =   2  'Center
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   -1  'True
               Strikethrough   =   0   'False
            EndProperty
            Height          =   405
            Index           =   0
            Left            =   2940
            TabIndex        =   67
            Top             =   600
            Width           =   900
         End
         Begin VB.Label Label3 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Trasmissione"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   14.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   -1  'True
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000008&
            Height          =   285
            Left            =   135
            TabIndex        =   66
            Top             =   615
            Width           =   3255
         End
         Begin VB.Label lab_tra_ing 
            Alignment       =   2  'Center
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   -1  'True
               Strikethrough   =   0   'False
            EndProperty
            Height          =   405
            Index           =   1
            Left            =   3945
            TabIndex        =   65
            Top             =   600
            Width           =   900
         End
      End
      Begin VB.Frame Frame4 
         BackColor       =   &H00C0FEFE&
         Caption         =   "RULLI DI USCITA"
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
         Height          =   1185
         Left            =   5520
         TabIndex        =   56
         Top             =   400
         Width           =   6335
         Begin VB.Label lab_tra_usc 
            Alignment       =   2  'Center
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   -1  'True
               Strikethrough   =   0   'False
            EndProperty
            Height          =   405
            Index           =   2
            Left            =   4935
            TabIndex        =   63
            Top             =   600
            Width           =   900
         End
         Begin VB.Label lab_tra_usc 
            Alignment       =   2  'Center
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   -1  'True
               Strikethrough   =   0   'False
            EndProperty
            Height          =   405
            Index           =   1
            Left            =   3975
            TabIndex        =   62
            Top             =   600
            Width           =   900
         End
         Begin VB.Label Label15 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Trasmissione"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   14.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   -1  'True
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000008&
            Height          =   285
            Left            =   135
            TabIndex        =   61
            Top             =   615
            Width           =   3255
         End
         Begin VB.Label lab_tra_liv 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Livello 1"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   195
            Index           =   0
            Left            =   2980
            TabIndex        =   60
            Top             =   240
            Width           =   930
         End
         Begin VB.Label lab_tra_liv 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Livello 2"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   195
            Index           =   1
            Left            =   3940
            TabIndex        =   59
            Top             =   240
            Width           =   930
         End
         Begin VB.Label lab_tra_liv 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Livello 3"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   195
            Index           =   2
            Left            =   4900
            TabIndex        =   58
            Top             =   240
            Width           =   930
         End
         Begin VB.Label lab_tra_usc 
            Alignment       =   2  'Center
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   -1  'True
               Strikethrough   =   0   'False
            EndProperty
            Height          =   405
            Index           =   0
            Left            =   3015
            TabIndex        =   57
            Top             =   600
            Width           =   900
         End
      End
   End
   Begin VB.Frame Frame2 
      BackColor       =   &H00C0E0FF&
      Caption         =   "PARAMETRI RULLI DI INGRESSO"
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
      Height          =   3900
      Left            =   0
      TabIndex        =   43
      Top             =   340
      Width           =   5475
      Begin VB.TextBox txt_off_vel_met_min 
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
         Index           =   1
         Left            =   4050
         MaxLength       =   2
         TabIndex        =   6
         Top             =   1350
         Width           =   900
      End
      Begin VB.TextBox txt_off_vel_per 
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
         Index           =   1
         Left            =   4050
         MaxLength       =   3
         TabIndex        =   7
         Top             =   1950
         Width           =   900
      End
      Begin VB.TextBox txt_sov_sfi 
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
         Index           =   1
         Left            =   4050
         MaxLength       =   3
         TabIndex        =   8
         Top             =   2550
         Width           =   900
      End
      Begin VB.TextBox txt_dis_rot_mas 
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
         Index           =   1
         Left            =   4050
         MaxLength       =   5
         TabIndex        =   9
         Top             =   3150
         Width           =   900
      End
      Begin VB.TextBox txt_dis_rot_mas 
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
         Index           =   0
         Left            =   3045
         MaxLength       =   5
         TabIndex        =   5
         Top             =   3150
         Width           =   900
      End
      Begin VB.TextBox txt_sov_sfi 
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
         Index           =   0
         Left            =   3045
         MaxLength       =   3
         TabIndex        =   4
         Top             =   2550
         Width           =   900
      End
      Begin VB.TextBox txt_off_vel_per 
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
         Index           =   0
         Left            =   3045
         MaxLength       =   3
         TabIndex        =   3
         Top             =   1950
         Width           =   900
      End
      Begin VB.TextBox txt_off_vel_met_min 
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
         Index           =   0
         Left            =   3045
         MaxLength       =   2
         TabIndex        =   2
         Top             =   1350
         Width           =   900
      End
      Begin VB.TextBox txt_vel_max_lin 
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
         Left            =   3045
         MaxLength       =   3
         TabIndex        =   1
         Top             =   390
         Width           =   900
      End
      Begin VB.Label lab_rul 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Rullo 2"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   195
         Index           =   1
         Left            =   4035
         TabIndex        =   54
         Top             =   945
         Width           =   930
      End
      Begin VB.Label lab_rul 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Rullo 1"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   195
         Index           =   0
         Left            =   3030
         TabIndex        =   53
         Top             =   945
         Width           =   930
      End
      Begin VB.Label Label14 
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
         Height          =   285
         Left            =   4995
         TabIndex        =   52
         Top             =   3240
         Width           =   405
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
         Height          =   285
         Left            =   4995
         TabIndex        =   51
         Top             =   2640
         Width           =   405
      End
      Begin VB.Label Label12 
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
         Height          =   285
         Left            =   4995
         TabIndex        =   50
         Top             =   2040
         Width           =   405
      End
      Begin VB.Label Label11 
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
         Height          =   285
         Left            =   4995
         TabIndex        =   49
         Top             =   1440
         Width           =   405
      End
      Begin VB.Label Label10 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Distanza tra Rotary-Shear e Master"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   285
         Left            =   120
         TabIndex        =   48
         Top             =   3225
         Width           =   3255
      End
      Begin VB.Label Label9 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Sovravelocità allo sfilamento"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   285
         Left            =   120
         TabIndex        =   47
         Top             =   2625
         Width           =   3255
      End
      Begin VB.Label Label8 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Offset di velocità in percentuale"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   285
         Left            =   120
         TabIndex        =   46
         Top             =   2025
         Width           =   3255
      End
      Begin VB.Label Label7 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Offset di velocità in m/min"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   285
         Left            =   120
         TabIndex        =   45
         Top             =   1425
         Width           =   3255
      End
      Begin VB.Label Label6 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Velocità massima di linea"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   285
         Left            =   120
         TabIndex        =   44
         Top             =   465
         Width           =   3255
      End
   End
   Begin VB.Frame Frame3 
      BackColor       =   &H00C0C0FF&
      Caption         =   "PARAMETRI RULLI DI USCITA"
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
      Height          =   3900
      Left            =   5520
      TabIndex        =   29
      Top             =   340
      Width           =   6435
      Begin VB.TextBox txt_tem_chi_fla 
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
         Index           =   0
         Left            =   3015
         MaxLength       =   3
         TabIndex        =   14
         Top             =   2670
         Width           =   900
      End
      Begin VB.TextBox txt_tem_chi_fla 
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
         Index           =   1
         Left            =   3975
         MaxLength       =   3
         TabIndex        =   20
         Top             =   2670
         Width           =   900
      End
      Begin VB.TextBox txt_tem_chi_fla 
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
         Index           =   2
         Left            =   4935
         MaxLength       =   3
         TabIndex        =   26
         Top             =   2670
         Width           =   900
      End
      Begin VB.TextBox txt_tem_ape_fla 
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
         Index           =   2
         Left            =   4935
         MaxLength       =   3
         TabIndex        =   25
         Top             =   2190
         Width           =   900
      End
      Begin VB.TextBox txt_lun_san 
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
         Index           =   1
         Left            =   3975
         MaxLength       =   4
         TabIndex        =   16
         Top             =   750
         Width           =   900
      End
      Begin VB.TextBox txt_dis_lam_fla 
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
         Index           =   2
         Left            =   4935
         MaxLength       =   4
         TabIndex        =   27
         Top             =   3150
         Width           =   900
      End
      Begin VB.TextBox txt_dis_lam_fla 
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
         Index           =   1
         Left            =   3975
         MaxLength       =   4
         TabIndex        =   21
         Top             =   3150
         Width           =   900
      End
      Begin VB.TextBox txt_tem_ape_fla 
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
         Index           =   1
         Left            =   3975
         MaxLength       =   3
         TabIndex        =   19
         Top             =   2190
         Width           =   900
      End
      Begin VB.TextBox txt_gap_tgl_lun 
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
         Index           =   2
         Left            =   4935
         MaxLength       =   5
         TabIndex        =   24
         Top             =   1710
         Width           =   900
      End
      Begin VB.TextBox txt_gap_tgl_lun 
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
         Index           =   1
         Left            =   3975
         MaxLength       =   5
         TabIndex        =   18
         Top             =   1710
         Width           =   900
      End
      Begin VB.TextBox txt_gap_tgl_cor 
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
         Index           =   2
         Left            =   4935
         MaxLength       =   5
         TabIndex        =   23
         Top             =   1230
         Width           =   900
      End
      Begin VB.TextBox txt_gap_tgl_cor 
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
         Index           =   1
         Left            =   3975
         MaxLength       =   5
         TabIndex        =   17
         Top             =   1230
         Width           =   900
      End
      Begin VB.TextBox txt_lun_san 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00FFFFFF&
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
         Left            =   4935
         MaxLength       =   4
         TabIndex        =   22
         Top             =   750
         Width           =   900
      End
      Begin VB.TextBox txt_lun_san 
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
         Index           =   0
         Left            =   3015
         MaxLength       =   4
         TabIndex        =   10
         Top             =   750
         Width           =   900
      End
      Begin VB.TextBox txt_gap_tgl_cor 
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
         Index           =   0
         Left            =   3015
         MaxLength       =   5
         TabIndex        =   11
         Top             =   1230
         Width           =   900
      End
      Begin VB.TextBox txt_gap_tgl_lun 
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
         Index           =   0
         Left            =   3015
         MaxLength       =   5
         TabIndex        =   12
         Top             =   1710
         Width           =   900
      End
      Begin VB.TextBox txt_tem_ape_fla 
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
         Index           =   0
         Left            =   3015
         MaxLength       =   3
         TabIndex        =   13
         Top             =   2190
         Width           =   900
      End
      Begin VB.TextBox txt_dis_lam_fla 
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
         Index           =   0
         Left            =   3015
         MaxLength       =   4
         TabIndex        =   15
         Top             =   3150
         Width           =   900
      End
      Begin VB.Label Label17 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Tempo di anticipo chiusura flap"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   285
         Left            =   135
         TabIndex        =   71
         Top             =   2745
         Width           =   3255
      End
      Begin VB.Label Label16 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "ms"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Left            =   5865
         TabIndex        =   70
         Top             =   2760
         Width           =   555
      End
      Begin VB.Label Label5 
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
         Height          =   285
         Left            =   5850
         TabIndex        =   42
         Top             =   3240
         Width           =   555
      End
      Begin VB.Label Label4 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "ms"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Left            =   5850
         TabIndex        =   41
         Top             =   2280
         Width           =   555
      End
      Begin VB.Label Label2 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mm/100"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Left            =   5850
         TabIndex        =   40
         Top             =   1800
         Width           =   555
      End
      Begin VB.Label Label1 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "mm/100"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Left            =   5850
         TabIndex        =   39
         Top             =   1320
         Width           =   555
      End
      Begin VB.Label lab_liv 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Livello 3"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   195
         Index           =   2
         Left            =   4900
         TabIndex        =   38
         Top             =   360
         Width           =   930
      End
      Begin VB.Label lab_liv 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Livello 2"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   195
         Index           =   1
         Left            =   3940
         TabIndex        =   37
         Top             =   360
         Width           =   930
      End
      Begin VB.Label lab_liv 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Livello 1"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   195
         Index           =   0
         Left            =   2980
         TabIndex        =   36
         Top             =   360
         Width           =   930
      End
      Begin VB.Label Label40 
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
         Height          =   285
         Left            =   5850
         TabIndex        =   35
         Top             =   840
         Width           =   555
      End
      Begin VB.Label Label23 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Lunghezza Sandwich"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   285
         Left            =   135
         TabIndex        =   34
         Top             =   825
         Width           =   3255
      End
      Begin VB.Label Label24 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Gap tagli corti (< L. Sandwich)"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   285
         Left            =   135
         TabIndex        =   33
         Top             =   1305
         Width           =   3255
      End
      Begin VB.Label Label25 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Gap tagli lunghi (>= L. Sandwich)"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   285
         Left            =   135
         TabIndex        =   32
         Top             =   1785
         Width           =   3255
      End
      Begin VB.Label Label27 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Tempo di anticipo apertura flap"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   285
         Left            =   135
         TabIndex        =   31
         Top             =   2265
         Width           =   3255
      End
      Begin VB.Label Label28 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Distanza tra lame e flap"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   285
         Left            =   135
         TabIndex        =   30
         Top             =   3225
         Width           =   3255
      End
   End
   Begin VB.TextBox txt_idle 
      Alignment       =   1  'Right Justify
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   285
      Left            =   0
      TabIndex        =   28
      Top             =   6660
      Width           =   600
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "PARAMETRI RULLI"
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
      Picture         =   "set_par_rul.frx":0000
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "set_par_rul"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
'Iciliouff 03-11-2003
Public WithEvents rmt_set_par_rul_plc As rmt
Attribute rmt_set_par_rul_plc.VB_VarHelpID = -1
Dim giu As Boolean
Dim su As Boolean

Function chk_txt_tem_fla(TextB As TextBox) As String
'Arturo uff 07-03-03 controllo sulla validità dei valori del tempo di anticipo chiusura flap
Dim liv As Integer

  If val(TextB.Text) < 0 Then
    TextB.Text = Format$(0)
  ElseIf val(TextB.Text) > MAX_TIM_ANT_CHI_FLAP Then
    TextB.Text = Format$(MAX_TIM_ANT_CHI_FLAP)
  End If
  chk_txt_tem_fla = TextB.Text
  
End Function

Sub del_lab()
Dim i As Integer

For i = 1 To 3
    lab_tra_usc(i - 1).Caption = ""
Next i
For i = 1 To 2
    lab_tra_ing(i - 1).Caption = ""
Next i

End Sub

Sub fil_par_rul_ing()
Dim num_rul As Integer
Dim i As Integer
'caricamento parametri dei rulli di ingresso
If par_mac.car_tec.num_liv_tag = 3 Then
    num_rul = 1
Else
    num_rul = 0
End If
txt_vel_max_lin.Text = Trim(par_mac_ute.rul_ing.vel_max_lin)
For i = 0 To num_rul
    txt_dis_rot_mas(i).Text = Trim(par_mac_ute.rul_ing.rul_num(i + 1).dis_rot_mas)
    txt_off_vel_met_min(i).Text = Trim(par_mac_ute.rul_ing.rul_num(i + 1).off_vel_met_min)
    txt_off_vel_per(i).Text = Trim(par_mac_ute.rul_ing.rul_num(i + 1).off_vel_per)
    txt_sov_sfi(i).Text = Trim(par_mac_ute.rul_ing.rul_num(i + 1).sov_sfi)
Next i
End Sub


Sub fil_par_rul_usc_liv()
'caricamento dei parametri dei rulli di uscita per livello
Dim liv As Integer

For liv = 1 To par_mac.car_tec.num_liv_tag
    txt_dis_lam_fla(liv - 1).Text = Trim(par_mac_ute.rul_usc.rul_usc_liv(liv).dis_lam_fla)
    txt_gap_tgl_cor(liv - 1).Text = Trim(par_mac_ute.rul_usc.rul_usc_liv(liv).gap_tgl_cor)
    txt_gap_tgl_lun(liv - 1).Text = Trim(par_mac_ute.rul_usc.rul_usc_liv(liv).gap_tgl_lun)
    txt_lun_san(liv - 1).Text = Trim(par_mac_ute.rul_usc.rul_usc_liv(liv).lun_san)
    txt_tem_ape_fla(liv - 1).Text = Trim(par_mac_ute.rul_usc.rul_usc_liv(liv).tem_ape_fla)
    txt_tem_chi_fla(liv - 1).Text = Trim(par_mac_ute.rul_usc.rul_usc_liv(liv).tem_chi_fla)  'Arturo uff 07-03-03
Next liv

End Sub

Sub ini_txt_lab()
'inizializza le TextBox e le Label in base al numero di livelli presenti
Dim i As Integer
Dim num_rul As Integer

If par_mac.car_tec.num_liv_tag = 3 Then
    num_rul = 2
Else
    num_rul = 1
End If

txt_vel_max_lin.Enabled = True
txt_vel_max_lin.BackColor = &HFFFFFF              'bianco

For i = 1 To num_rul
  txt_off_vel_met_min(i - 1).Enabled = True
  txt_off_vel_per(i - 1).Enabled = True
  txt_sov_sfi(i - 1).Enabled = True
  txt_dis_rot_mas(i - 1).Enabled = True
  lab_tra_rul(i - 1).Enabled = True
  lab_tra_ing(i - 1).Enabled = True
  lab_rul(i - 1).Enabled = True
  lab_tra_ing(i - 1).BackColor = &HFFFFFF                   'bianco
  txt_off_vel_met_min(i - 1).BackColor = &HFFFFFF    'bianco
  txt_off_vel_per(i - 1).BackColor = &HFFFFFF             'bianco
  txt_sov_sfi(i - 1).BackColor = &HFFFFFF                   'bianco
  txt_dis_rot_mas(i - 1).BackColor = &HFFFFFF          'bianco
Next i
For i = num_rul + 1 To 2
  txt_off_vel_met_min(i - 1).Enabled = False
  txt_off_vel_per(i - 1).Enabled = False
  txt_sov_sfi(i - 1).Enabled = False
  txt_dis_rot_mas(i - 1).Enabled = False
  txt_off_vel_met_min(i - 1).BackColor = LIGHT_GRAY
  txt_off_vel_per(i - 1).BackColor = LIGHT_GRAY
  txt_sov_sfi(i - 1).BackColor = LIGHT_GRAY
  txt_dis_rot_mas(i - 1).BackColor = LIGHT_GRAY
  lab_tra_ing(i - 1).BackColor = LIGHT_GRAY
  lab_rul(i - 1).Enabled = False
  lab_tra_ing(i - 1).Enabled = False
  lab_tra_rul(i - 1).Enabled = False
  txt_off_vel_met_min(i - 1).Text = ""
  txt_off_vel_per(i - 1).Text = ""
  txt_sov_sfi(i - 1).Text = ""
  txt_dis_rot_mas(i - 1).Text = ""
Next i

For i = 1 To par_mac.car_tec.num_liv_tag
  txt_lun_san(i - 1).Enabled = True
  txt_gap_tgl_cor(i - 1).Enabled = True
  txt_gap_tgl_lun(i - 1).Enabled = True
  txt_tem_ape_fla(i - 1).Enabled = True
  txt_tem_chi_fla(i - 1).Enabled = True           'Arturo uff 07-03-03
  txt_dis_lam_fla(i - 1).Enabled = True
  txt_lun_san(i - 1).BackColor = &HFFFFFF                   'bianco
  txt_gap_tgl_cor(i - 1).BackColor = &HFFFFFF             'bianco
  txt_gap_tgl_lun(i - 1).BackColor = &HFFFFFF             'bianco
  txt_tem_ape_fla(i - 1).BackColor = &HFFFFFF           'bianco
  txt_tem_chi_fla(i - 1).BackColor = &HFFFFFF     'Arturo uff 07-03-03
  txt_dis_lam_fla(i - 1).BackColor = &HFFFFFF            'bianco
  lab_tra_usc(i - 1).BackColor = &HFFFFFF                 'bianco
  lab_liv(i - 1).Enabled = True
  lab_tra_usc(i - 1).Enabled = True
  lab_tra_liv(i - 1).Enabled = True
Next i
For i = par_mac.car_tec.num_liv_tag + 1 To 3
  txt_lun_san(i - 1).Enabled = False
  txt_gap_tgl_cor(i - 1).Enabled = False
  txt_gap_tgl_lun(i - 1).Enabled = False
  txt_tem_ape_fla(i - 1).Enabled = False
  txt_tem_chi_fla(i - 1).Enabled = False          'Arturo uff 07-03-03
  txt_dis_lam_fla(i - 1).Enabled = False
  txt_lun_san(i - 1).BackColor = LIGHT_GRAY
  txt_gap_tgl_cor(i - 1).BackColor = LIGHT_GRAY
  txt_gap_tgl_lun(i - 1).BackColor = LIGHT_GRAY
  txt_tem_ape_fla(i - 1).BackColor = LIGHT_GRAY
  txt_tem_chi_fla(i - 1).BackColor = LIGHT_GRAY   'Arturo uff 07-03-03
  txt_dis_lam_fla(i - 1).BackColor = LIGHT_GRAY
  lab_tra_usc(i - 1).BackColor = LIGHT_GRAY
  lab_liv(i - 1).Enabled = False
  lab_tra_usc(i - 1).Enabled = False
  lab_tra_liv(i - 1).Enabled = False
  txt_lun_san(i - 1).Text = ""
  txt_gap_tgl_cor(i - 1).Text = ""
  txt_gap_tgl_lun(i - 1).Text = ""
  txt_tem_ape_fla(i - 1).Text = ""
  txt_tem_chi_fla(i - 1).Text = ""                 'Arturo uff 07-03-03
  txt_dis_lam_fla(i - 1).Text = ""
Next i
End Sub

Sub scr_par_ute_rul_ing()
Dim num_rul As Integer
Dim i As Integer
'caricamento parametri dei rulli di ingresso dalla form nella struttura di appoggio
If par_mac.car_tec.num_liv_tag = 3 Then
    num_rul = 1
Else
    num_rul = 0
End If
par_mac_ute.rul_ing.vel_max_lin = txt_vel_max_lin.Text
For i = 0 To num_rul
    par_mac_ute.rul_ing.rul_num(i + 1).dis_rot_mas = txt_dis_rot_mas(i).Text
    par_mac_ute.rul_ing.rul_num(i + 1).off_vel_met_min = txt_off_vel_met_min(i).Text
    par_mac_ute.rul_ing.rul_num(i + 1).off_vel_per = txt_off_vel_per(i).Text
    par_mac_ute.rul_ing.rul_num(i + 1).sov_sfi = txt_sov_sfi(i).Text
Next i
End Sub

Sub scr_par_ute_rul_usc_liv()
Dim liv As Integer

'caricamento dei parametri dei rulli di uscita per livello dalla form nella struttura di appoggio
For liv = 1 To par_mac.car_tec.num_liv_tag
  With par_mac_ute.rul_usc.rul_usc_liv(liv)
    .dis_lam_fla = txt_dis_lam_fla(liv - 1).Text
    .gap_tgl_cor = txt_gap_tgl_cor(liv - 1).Text
    .gap_tgl_lun = txt_gap_tgl_lun(liv - 1).Text
    .lun_san = txt_lun_san(liv - 1).Text
    .tem_ape_fla = chk_txt_tem_fla(txt_tem_ape_fla(liv - 1))     'Arturo uff 07-03-03
    .tem_chi_fla = chk_txt_tem_fla(txt_tem_chi_fla(liv - 1))     'Arturo uff 07-03-03
  End With
Next liv

End Sub

Function snd_par_azi(tip_dat As tip_dat_com_azi) As cod_ret
Dim res As cod_ret
Dim err As Boolean
Dim i As Integer

del_msg_can
If msg_rul Then
  del_lab
  new_msg_rul = True
End If
err = False

If snd_par_rul_ing(IND_SAN_ING, tip_dat) = RET_BAD Then
    err = True
    lab_tra_ing(IND_SAN_ING - 1).ForeColor = COL_ALL_Red
    lab_tra_ing(IND_SAN_ING - 1).Caption = "Error"
Else
    lab_tra_ing(IND_SAN_ING - 1).ForeColor = COL_DARK_Green
    lab_tra_ing(IND_SAN_ING - 1).Caption = "OK"
End If
If par_mac.car_tec.num_liv_tag = 3 Then
    If snd_par_rul_ing(IND_SAN_ING2, tip_dat) = RET_BAD Then
        err = True
        lab_tra_ing(IND_SAN_ING2 - 1).ForeColor = COL_ALL_Red
        lab_tra_ing(IND_SAN_ING2 - 1).Caption = "Error"
    Else
        lab_tra_ing(IND_SAN_ING2 - 1).ForeColor = COL_DARK_Green
        lab_tra_ing(IND_SAN_ING2 - 1).Caption = "OK"
    End If
End If

For i = 1 To par_mac.car_tec.num_liv_tag
    If snd_par_rul_usc(i, tip_dat) = RET_BAD Then
        err = True
        lab_tra_usc(i - 1).ForeColor = COL_ALL_Red
        lab_tra_usc(i - 1).Caption = "Error"
    Else
        lab_tra_usc(i - 1).ForeColor = COL_DARK_Green
        lab_tra_usc(i - 1).Caption = "OK"
    End If
Next i

If err Then
  snd_par_azi = RET_BAD
Else
  snd_par_azi = RET_OK
End If

End Function

Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
    If mod_in_cor Then
        mod_in_cor = False
        esc_pre = True
        par_mac_ute = par_mac
        fil_par_rul_ing
        fil_par_rul_usc_liv
        disable_tab Me
        disable_frames Me  'Arturo uff 07-03-03
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
    If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Sub
    'If sta_tag.MAC_F_STOP(1) Then
        If Not mod_in_cor Then
          If sta_tag.com_OK Then
            esc_pre = False
            sal_in_cor = False
            enable_tab Me
            enable_frames Me  'Arturo uff 07-03-03
            txt_idle.TabStop = False
            txt_vel_max_lin.SetFocus
            mod_in_cor = True
          Else
              messaggio msg("com_int_con_mac"), True
          End If
        End If
    'Else
    '    messaggio msg("mac_non_fas_sto"), True, False
    'End If
  Case Else
End Select
End Sub


Private Sub eve_key_F3(Shift As Integer)
Dim l As Integer
Select Case Shift
  Case 0
    'salva
    
    Dim res As cod_ret
    
    If mod_in_cor Then
        sal_in_cor = True
        messaggio msg("ope_in_cor"), False, False
        scr_par_ute_rul_ing
        scr_par_ute_rul_usc_liv
        
        res = snd_par_azi(configurazione)
        
        If res = RET_BAD Then
            par_mac_ute.rul_ing = par_mac.rul_ing
            par_mac_ute.rul_usc = par_mac.rul_usc
            fil_par_rul_ing
            fil_par_rul_usc_liv
            messaggio msg("ope_eff_par"), True, False
        Else
            par_mac.rul_ing = par_mac_ute.rul_ing
            par_mac.rul_usc = par_mac_ute.rul_usc
            'Iciliouff 03-11-2003
            If Not rmt_set_par_rul_plc.esegui(tip_ope_PLC.PARAM_MAC, GetTickCount(), 0, 0) Then
              messaggio msg("imp_ese_ope"), True
            End If
'            snd_str_PLC (PARAM_MAC)
            
            scr_par_mac
            messaggio msg("ope_eff"), False, False
        End If
        disable_tab Me
        disable_frames Me  'Arturo uff 07-03-03
        txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
        txt_idle.TabStop = True
        txt_idle.SetFocus
        mod_in_cor = False
    End If
  Case Else
End Select
End Sub

Private Sub eve_key_F8(Shift As Integer)
Select Case Shift
  Case 0
    If Not mod_in_cor Then eve_key.chg pag_lis_ese
  Case Else
End Select
End Sub

Private Sub eve_key_F9(Shift As Integer)
Select Case Shift
  Case 0
    If Not mod_in_cor Then eve_key.chg pag_lis_ord
  Case Else
End Select
End Sub


Private Sub Form_Activate()

giu = False
fil_par_rul_usc_liv
fil_par_rul_ing
If mod_in_cor Then mod_in_cor = False
If sal_in_cor Then sal_in_cor = False
If esc_pre Then esc_pre = False

del_lab
ini_txt_lab

disable_frames Me  'Arturo uff 07-03-03
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
Dim i As Byte
Dim res As cod_ret
Debug.Print "Inizio Load " & Me.name
Set eve_key = New key
'Iciliouff 03-11-2003
Set rmt_set_par_rul_plc = New rmt
rmt_set_par_rul_plc.set_plc_ute (1)
rmt_set_par_rul_plc.name = "rmt_set_par_rul_plc"

eve_key.create Me
Carica Me
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub
































Private Sub rmt_set_par_rul_plc_retbad(err As String)
'Gestione risposta negativa da Plc

  If err <> "" Then messaggio err, True
  rmt_set_par_rul_plc.resetta

End Sub

Private Sub rmt_set_par_rul_plc_retok(s As String)
'Gestione risposta positiva da Plc

  If s <> "" Then messaggio s, False
  rmt_set_par_rul_plc.resetta

End Sub


Private Sub txt_dis_lam_fla_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub




Private Sub txt_dis_rot_mas_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_dis_rot_mas_KeyDown(Index As Integer, KeyCode As Integer, Shift As Integer)
If KeyCode = vbKeyDown Then
  giu = True
Else
  giu = False
End If
End Sub

Private Sub txt_dis_rot_mas_LostFocus(Index As Integer)
If sal_in_cor Or esc_pre Then
    sal_in_cor = False
    esc_pre = False
    disable_tab Me
    txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
    txt_idle.TabStop = True
    txt_idle.SetFocus
Else
    'If giu Then
    '  If Index = 1 Or val(par_mac.car_tec.num_liv_tag) < 3 Then
    '    txt_lun_san(0).SetFocus
    '  End If
    'End If
End If
End Sub


Private Sub txt_gap_tgl_cor_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_gap_tgl_lun_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_lun_san_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub



Private Sub txt_off_vel_met_min_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_off_vel_per_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_vel_max_lin_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_sov_sfi_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_tem_ape_fla_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_tem_chi_fla_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text) 'Arturo uff 07-03-03
End Sub
