VERSION 5.00
Begin VB.Form set_par_sch_tag2 
   Appearance      =   0  'Flat
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6555
   ClientLeft      =   2460
   ClientTop       =   1035
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
      TabIndex        =   44
      Top             =   330
      Width           =   5985
      Begin VB.TextBox txt_lun_tag_tes_att 
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
         Top             =   2175
         Width           =   915
      End
      Begin VB.TextBox txt_lun_tag_tes_def 
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
         Top             =   2175
         Width           =   915
      End
      Begin VB.TextBox txt_lim_sup_tol_tag_def 
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
         Top             =   3022
         Width           =   915
      End
      Begin VB.TextBox txt_lim_sup_tol_tag_att 
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
         Top             =   3022
         Width           =   915
      End
      Begin VB.TextBox txt_lim_inf_tol_tag_def 
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
         TabIndex        =   56
         TabStop         =   0   'False
         Top             =   2602
         Width           =   915
      End
      Begin VB.TextBox txt_lim_inf_tol_tag_att 
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
         Top             =   2602
         Width           =   915
      End
      Begin VB.TextBox txt_lun_tag_mas_def 
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
         TabIndex        =   47
         TabStop         =   0   'False
         Top             =   1755
         Width           =   915
      End
      Begin VB.TextBox txt_lun_tag_mas_att 
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
         Top             =   1755
         Width           =   915
      End
      Begin VB.TextBox txt_lun_tag_min_def 
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
         TabIndex        =   46
         TabStop         =   0   'False
         Top             =   1335
         Width           =   915
      End
      Begin VB.TextBox txt_lun_tag_min_att 
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
         TabIndex        =   9
         Top             =   1335
         Width           =   915
      End
      Begin VB.TextBox txt_tem_cam_sta_vel_def 
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
         TabIndex        =   45
         TabStop         =   0   'False
         Top             =   915
         Width           =   915
      End
      Begin VB.TextBox txt_tem_cam_sta_vel_att 
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
         TabIndex        =   8
         Top             =   915
         Width           =   915
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
         Left            =   4650
         TabIndex        =   64
         Top             =   2265
         Width           =   375
      End
      Begin VB.Label Label15 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Lunghezza taglio di testa"
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
         Top             =   2265
         Width           =   2325
      End
      Begin VB.Label Label57 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Limite inferiore tolleranza di taglio"
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
         Top             =   2692
         Width           =   2685
      End
      Begin VB.Label Label26 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Limite superiore tolleranza di taglio"
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
         Top             =   3112
         Width           =   2685
      End
      Begin VB.Label Label27 
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
         Left            =   4650
         TabIndex        =   60
         Top             =   3112
         Width           =   375
      End
      Begin VB.Label Label28 
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
         Left            =   4650
         TabIndex        =   59
         Top             =   2692
         Width           =   375
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
         TabIndex        =   55
         Top             =   510
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
         TabIndex        =   54
         Top             =   510
         Width           =   645
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
         Height          =   255
         Left            =   4650
         TabIndex        =   53
         Top             =   1425
         Width           =   765
      End
      Begin VB.Label Label39 
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
         TabIndex        =   52
         Top             =   1845
         Width           =   765
      End
      Begin VB.Label Label29 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "1; 2; 3; 4"
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
         TabIndex        =   51
         Top             =   1005
         Width           =   765
      End
      Begin VB.Label Label12 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Lunghezza taglio massima"
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
         TabIndex        =   50
         Top             =   1845
         Width           =   2565
      End
      Begin VB.Label Label21 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Lunghezza taglio minima"
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
         Top             =   1425
         Width           =   2565
      End
      Begin VB.Label Label7 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Tempo campionamento stadio di vel."
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
         Top             =   1005
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
      TabIndex        =   26
      Top             =   6570
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
      Height          =   5890
      Left            =   0
      TabIndex        =   14
      Top             =   330
      Width           =   5970
      Begin VB.TextBox txt_vel_sim_lap_def 
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
         MaxLength       =   3
         TabIndex        =   39
         TabStop         =   0   'False
         Top             =   4792
         Width           =   915
      End
      Begin VB.TextBox txt_vel_sim_lap_att 
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
         MaxLength       =   3
         TabIndex        =   7
         Top             =   4792
         Width           =   915
      End
      Begin VB.TextBox txt_sov_lam_def 
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
         TabIndex        =   36
         TabStop         =   0   'False
         Top             =   4372
         Width           =   915
      End
      Begin VB.TextBox txt_sov_lam_att 
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
         Top             =   4372
         Width           =   915
      End
      Begin VB.TextBox txt_gir_mot_lam_rif_att 
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
         MaxLength       =   5
         TabIndex        =   5
         Top             =   3952
         Width           =   915
      End
      Begin VB.TextBox txt_gir_mot_lam_rif_def 
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
         MaxLength       =   5
         TabIndex        =   33
         TabStop         =   0   'False
         Top             =   3952
         Width           =   915
      End
      Begin VB.TextBox txt_off_ram_tag_att 
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
         MaxLength       =   3
         TabIndex        =   4
         Top             =   3525
         Width           =   915
      End
      Begin VB.TextBox txt_off_ram_tag_def 
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
         MaxLength       =   3
         TabIndex        =   31
         TabStop         =   0   'False
         Top             =   3525
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
         TabIndex        =   27
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
            TabIndex        =   28
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
            TabIndex        =   29
            Top             =   120
            Width           =   2175
         End
      End
      Begin VB.TextBox txt_vel_zon_sinc_vmin_att 
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
         MaxLength       =   5
         TabIndex        =   0
         Top             =   1845
         Width           =   915
      End
      Begin VB.TextBox txt_vel_zon_sinc_vmin_def 
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
         MaxLength       =   5
         TabIndex        =   24
         TabStop         =   0   'False
         Top             =   1845
         Width           =   915
      End
      Begin VB.TextBox txt_off_ram_pos_def 
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
         MaxLength       =   3
         TabIndex        =   20
         TabStop         =   0   'False
         Top             =   3105
         Width           =   915
      End
      Begin VB.TextBox txt_off_ram_pos_att 
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
         MaxLength       =   3
         TabIndex        =   3
         Top             =   3105
         Width           =   915
      End
      Begin VB.TextBox txt_fat_k01_ott_pen_def 
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
         MaxLength       =   3
         TabIndex        =   19
         TabStop         =   0   'False
         Top             =   2685
         Width           =   915
      End
      Begin VB.TextBox txt_fat_k01_ott_pen_att 
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
         MaxLength       =   3
         TabIndex        =   2
         Top             =   2685
         Width           =   915
      End
      Begin VB.TextBox txt_fin_vel_ott_pen_def 
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
         MaxLength       =   3
         TabIndex        =   18
         TabStop         =   0   'False
         Top             =   2265
         Width           =   915
      End
      Begin VB.TextBox txt_fin_vel_ott_pen_att 
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
         MaxLength       =   3
         TabIndex        =   1
         Top             =   2265
         Width           =   915
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
         TabIndex        =   67
         Top             =   1935
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
         TabIndex        =   66
         Top             =   1935
         Width           =   465
      End
      Begin VB.Label lab_vel_zon_sinc_vmin 
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
         TabIndex        =   65
         Top             =   1935
         Width           =   420
      End
      Begin VB.Label lab_sov_lam 
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
         TabIndex        =   43
         Top             =   4462
         Width           =   420
      End
      Begin VB.Label Label4 
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
         TabIndex        =   42
         Top             =   4462
         Width           =   465
      End
      Begin VB.Label Label19 
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
         Left            =   4665
         TabIndex        =   41
         Top             =   4882
         Width           =   495
      End
      Begin VB.Label Label30 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Velocità per simulazione e lappatura"
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
         Top             =   4882
         Width           =   2685
      End
      Begin VB.Label Label3 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Sovravelocità lame"
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
         TabIndex        =   38
         Top             =   4462
         Width           =   2325
      End
      Begin VB.Label Label37 
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
         TabIndex        =   37
         Top             =   4462
         Width           =   300
      End
      Begin VB.Label Label41 
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
         Left            =   4665
         TabIndex        =   35
         Top             =   2355
         Width           =   765
      End
      Begin VB.Label Label2 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "rpm"
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
         Left            =   4665
         TabIndex        =   34
         Top             =   4042
         Width           =   525
      End
      Begin VB.Label Label1 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Offset rampa di taglio"
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
         TabIndex        =   32
         Top             =   3615
         Width           =   2325
      End
      Begin VB.Label Label55 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Velocità in zona sinc. alla Vmin"
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
         Top             =   1935
         Width           =   2640
      End
      Begin VB.Label Label31 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Offset rampa di posizione"
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
         Top             =   3195
         Width           =   2325
      End
      Begin VB.Label Label47 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Fattore K01 per ottim. pendenze"
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
         Top             =   2775
         Width           =   2625
      End
      Begin VB.Label Label54 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Finestra vel. per ottim. pendenze"
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
         TabIndex        =   21
         Top             =   2355
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
         TabIndex        =   17
         Top             =   1485
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
         TabIndex        =   16
         Top             =   1485
         Width           =   645
      End
      Begin VB.Label Label6 
         BackColor       =   &H00FFFFFF&
         BackStyle       =   0  'Transparent
         Caption         =   "Giri motore lame per riferimento"
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
         TabIndex        =   15
         Top             =   4042
         Width           =   2625
      End
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "PARAMETRI CONTROLLO TAGLIO   PAG. 2/5"
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
      TabIndex        =   30
      Top             =   0
      Width           =   7785
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "set_par_sch_tag2.frx":0000
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "set_par_sch_tag2"
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
  txt_vel_zon_sinc_vmin_def = txt_vel_zon_sinc_vmin_att
  txt_fin_vel_ott_pen_def = txt_fin_vel_ott_pen_att
  txt_fat_k01_ott_pen_def = txt_fat_k01_ott_pen_att
  txt_off_ram_pos_def = txt_off_ram_pos_att
  txt_off_ram_tag_def = txt_off_ram_tag_att
  txt_gir_mot_lam_rif_def = txt_gir_mot_lam_rif_att
  txt_sov_lam_def = txt_sov_lam_att
  txt_vel_sim_lap_def = txt_vel_sim_lap_att
  txt_lun_tag_tes_def = txt_lun_tag_tes_att
  txt_lim_inf_tol_tag_def = txt_lim_inf_tol_tag_att
  txt_lim_sup_tol_tag_def = txt_lim_sup_tol_tag_att
  txt_lun_tag_min_def = txt_lun_tag_min_att
  txt_lun_tag_mas_def = txt_lun_tag_mas_att
  txt_tem_cam_sta_vel_def = txt_tem_cam_sta_vel_att
  
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
        set_par_sch_tag4.att_in_def (True)
        'RFAZZI_FI19/03/2008
        set_par_sch_tag5.att_in_def (True)
    End If
  Else
    scr_par_sch_tag_def_liv_ini txt_liv_tag
  End If

End Sub


Sub fil_par_sch_tag_liv(liv As Integer)

'caricamento dei parametri nella form scheda di taglio per livello

txt_vel_zon_sinc_vmin_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).vel_zon_sinc_vmin)
lab_vel_zon_sinc_vmin = val(txt_vel_zon_sinc_vmin_att) / 10
txt_fin_vel_ott_pen_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).fin_vel_ott_pen)
txt_fat_k01_ott_pen_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).fat_k01_ott_pen)
txt_off_ram_pos_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).off_ram_pos)
txt_off_ram_tag_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).off_ram_tag)
txt_gir_mot_lam_rif_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).gir_mot_lam_rif)
txt_sov_lam_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).sov_lam)
lab_sov_lam = val(txt_sov_lam_att) / 10
txt_vel_sim_lap_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).vel_sim_lap)
txt_lun_tag_tes_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).lun_tag_tes)
txt_lim_inf_tol_tag_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).lim_inf_tol_tag)
txt_lim_sup_tol_tag_att = Trim(par_mac_ute.sch_tag.sch_tag_liv(liv).lim_sup_tol_tag)
txt_lun_tag_min_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).lun_tag_min)
txt_lun_tag_mas_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).lun_tag_max)
txt_tem_cam_sta_vel_att = Trim(par_mac.sch_tag.sch_tag_liv(liv).tem_cam_sta_vel)

End Sub


Sub fil_par_sch_tag_def_liv(liv As Integer)
'caricamento dei parametri nella form scheda di taglio per livello

txt_vel_zon_sinc_vmin_def = Trim(par_mac.sch_tag_def.sch_tag_liv(liv).vel_zon_sinc_vmin)
txt_fin_vel_ott_pen_def = Trim(par_mac.sch_tag_def.sch_tag_liv(liv).fin_vel_ott_pen)
txt_fat_k01_ott_pen_def = Trim(par_mac.sch_tag_def.sch_tag_liv(liv).fat_k01_ott_pen)
txt_off_ram_pos_def = Trim(par_mac.sch_tag_def.sch_tag_liv(liv).off_ram_pos)
txt_off_ram_tag_def = Trim(par_mac.sch_tag_def.sch_tag_liv(liv).off_ram_tag)
txt_gir_mot_lam_rif_def = Trim(par_mac.sch_tag_def.sch_tag_liv(liv).gir_mot_lam_rif)
txt_sov_lam_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).sov_lam)
txt_vel_sim_lap_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).vel_sim_lap)
txt_lun_tag_tes_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).lun_tag_tes)
txt_lim_inf_tol_tag_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).lim_inf_tol_tag)
txt_lim_sup_tol_tag_def = Trim(par_mac_ute.sch_tag_def.sch_tag_liv(liv).lim_sup_tol_tag)
txt_lun_tag_min_def = Trim(par_mac.sch_tag_def.sch_tag_liv(liv).lun_tag_min)
txt_lun_tag_mas_def = Trim(par_mac.sch_tag_def.sch_tag_liv(liv).lun_tag_max)
txt_tem_cam_sta_vel_def = Trim(par_mac.sch_tag_def.sch_tag_liv(liv).tem_cam_sta_vel)

End Sub

Sub scr_par_att_ute_liv(liv As Integer)


'PARAMETRI PER LIVELLO DI TAGLIO


  par_mac_ute.sch_tag.sch_tag_liv(liv).vel_zon_sinc_vmin = txt_vel_zon_sinc_vmin_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).fin_vel_ott_pen = txt_fin_vel_ott_pen_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).fat_k01_ott_pen = txt_fat_k01_ott_pen_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).off_ram_pos = txt_off_ram_pos_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).off_ram_tag = txt_off_ram_tag_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).gir_mot_lam_rif = txt_gir_mot_lam_rif_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).sov_lam = txt_sov_lam_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).vel_sim_lap = txt_vel_sim_lap_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).lun_tag_tes = txt_lun_tag_tes_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).lim_inf_tol_tag = txt_lim_inf_tol_tag_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).lim_sup_tol_tag = txt_lim_sup_tol_tag_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).tem_cam_sta_vel = txt_tem_cam_sta_vel_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).lun_tag_min = txt_lun_tag_min_att
  par_mac_ute.sch_tag.sch_tag_liv(liv).lun_tag_max = txt_lun_tag_mas_att
  
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

Private Sub txt_sov_lam_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub





Private Sub txt_sov_lam_att_KeyUp(KeyCode As Integer, Shift As Integer)
lab_sov_lam = val(txt_sov_lam_att) / 10
End Sub


Sub scr_par_def_ute_liv(liv As Integer)


  par_mac_ute.sch_tag_def.sch_tag_liv(liv).vel_zon_sinc_vmin = txt_vel_zon_sinc_vmin_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).fin_vel_ott_pen = txt_fin_vel_ott_pen_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).fat_k01_ott_pen = txt_fat_k01_ott_pen_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).off_ram_pos = txt_off_ram_pos_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).off_ram_tag = txt_off_ram_tag_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).gir_mot_lam_rif = txt_gir_mot_lam_rif_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).sov_lam = txt_sov_lam_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).vel_sim_lap = txt_vel_sim_lap_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).lun_tag_tes = txt_lun_tag_tes_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).lim_inf_tol_tag = txt_lim_inf_tol_tag_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).lim_sup_tol_tag = txt_lim_sup_tol_tag_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).tem_cam_sta_vel = txt_tem_cam_sta_vel_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).lun_tag_min = txt_lun_tag_min_def
  par_mac_ute.sch_tag_def.sch_tag_liv(liv).lun_tag_max = txt_lun_tag_mas_def
  
End Sub




Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
    If mod_in_cor Then
        esc_pre = True
        mod_in_cor = False
        par_mac_ute = par_mac
        fil_par_sch_tag_liv (txt_liv_tag.Text)
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
      txt_vel_zon_sinc_vmin_att.SetFocus
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
          fil_par_sch_tag_liv (txt_liv_tag.Text)
          fil_par_sch_tag_def_liv (txt_liv_tag.Text)
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
    eve_key.chg set_par_sch_tag1
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
    eve_key.chg set_par_sch_tag3
  Case 2
    eve_key.chg pag_lis_ord
  Case Else
  End Select
End If
End Sub

Private Sub Form_Activate()
If mod_in_cor Then mod_in_cor = False
If sal_in_cor Then sal_in_cor = False
If esc_pre Then esc_pre = True
txt_liv_tag.Text = Livello
fil_par_sch_tag_liv (Livello)
fil_par_sch_tag_def_liv (Livello)
txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
txt_idle.SetFocus

End Sub

Private Sub txt_lim_inf_tol_tag_att_GotFocus()
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
Private Sub txt_lim_sup_tol_tag_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_lun_tag_mas_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_lun_tag_min_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_lun_tag_tes_att_LostFocus()
'If mod_in_cor And txt_lun_tag_tes_att.Text <> par_mac.sch_tag.sch_tag_liv(txt_liv_tag).lun_tag_tes Then
'       par_mac.sch_tag.sch_tag_liv(txt_liv_tag.Text).lun_tag_tes = txt_lun_tag_tes_att.Text
'       setFileINIString sec_liv + txt_liv_tag, "lun_tag_tes", par_mac.sch_tag.sch_tag_liv(txt_liv_tag).lun_tag_tes, app.EXEName
'End If

End Sub

Private Sub txt_lun_tag_tes_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_tem_cam_sta_vel_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub




Private Sub txt_fat_k01_ott_pen_att_GotFocus()
 ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_fin_vel_ott_pen_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_gir_mot_lam_rif_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub









Private Sub txt_off_ram_pos_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_off_ram_tag_att_GotFocus()
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_vel_sim_lap_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_vel_zon_sinc_vmin_att_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_vel_zon_sinc_vmin_att_KeyUp(KeyCode As Integer, Shift As Integer)
lab_vel_zon_sinc_vmin = val(txt_vel_zon_sinc_vmin_att) / 10
End Sub


