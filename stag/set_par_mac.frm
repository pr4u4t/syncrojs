VERSION 5.00
Begin VB.Form set_par_mac 
   Appearance      =   0  'Flat
   BackColor       =   &H80000005&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6330
   ClientLeft      =   1320
   ClientTop       =   5175
   ClientWidth     =   11955
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6330
   ScaleMode       =   0  'User
   ScaleWidth      =   11955
   ShowInTaskbar   =   0   'False
   Begin VB.CheckBox chk_cut_to_pat_auto 
      Alignment       =   1  'Right Justify
      BackColor       =   &H80000005&
      Caption         =   "Cut to pattern Auto Liv3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   350
      Index           =   2
      Left            =   8880
      MaskColor       =   &H00FFFFFF&
      TabIndex        =   27
      Top             =   5400
      Value           =   1  'Checked
      Width           =   2760
   End
   Begin VB.CheckBox chk_cut_to_pat_auto 
      Alignment       =   1  'Right Justify
      BackColor       =   &H80000005&
      Caption         =   "Cut to pattern Auto Liv2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   350
      Index           =   1
      Left            =   8880
      MaskColor       =   &H00FFFFFF&
      TabIndex        =   26
      Top             =   4920
      Value           =   1  'Checked
      Width           =   2760
   End
   Begin VB.TextBox txt_lun_tag_sca_cut_pat_auto 
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
      Left            =   7200
      MaxLength       =   4
      TabIndex        =   57
      Top             =   2640
      Width           =   810
   End
   Begin VB.CheckBox chk_cut_to_pat 
      Alignment       =   1  'Right Justify
      BackColor       =   &H80000005&
      Caption         =   "Cut to pattern Liv3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   350
      Index           =   2
      Left            =   8880
      MaskColor       =   &H00FFFFFF&
      TabIndex        =   21
      Top             =   2520
      Value           =   1  'Checked
      Width           =   2760
   End
   Begin VB.CheckBox chk_cut_to_pat 
      Alignment       =   1  'Right Justify
      BackColor       =   &H80000005&
      Caption         =   "Cut to pattern Liv2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   350
      Index           =   1
      Left            =   8880
      MaskColor       =   &H00FFFFFF&
      TabIndex        =   20
      Top             =   2040
      Value           =   1  'Checked
      Width           =   2760
   End
   Begin VB.CheckBox chk_cut_to_pat_auto 
      Alignment       =   1  'Right Justify
      BackColor       =   &H80000005&
      Caption         =   "Cut to pattern Auto Liv1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   350
      Index           =   0
      Left            =   8880
      MaskColor       =   &H00FFFFFF&
      TabIndex        =   25
      Top             =   4440
      Value           =   1  'Checked
      Width           =   2760
   End
   Begin VB.ComboBox cmb_tip_azi 
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
      Left            =   7155
      Style           =   2  'Dropdown List
      TabIndex        =   17
      Top             =   4920
      Width           =   1260
   End
   Begin VB.CheckBox chk_cut_to_pat 
      Alignment       =   1  'Right Justify
      BackColor       =   &H80000005&
      Caption         =   "Cut to pattern Liv1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   350
      Index           =   0
      Left            =   8880
      MaskColor       =   &H00FFFFFF&
      TabIndex        =   19
      Top             =   1560
      Value           =   1  'Checked
      Width           =   2760
   End
   Begin VB.TextBox txt_sgl_abi_lin_DC_Bus 
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
      Left            =   7185
      MaxLength       =   3
      TabIndex        =   16
      Top             =   4500
      Width           =   840
   End
   Begin VB.ComboBox cmb_nor_mac 
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
      Left            =   7185
      Style           =   2  'Dropdown List
      TabIndex        =   15
      Top             =   4050
      Width           =   855
   End
   Begin VB.ComboBox cmb_gra_car 
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
      Left            =   7200
      Style           =   2  'Dropdown List
      TabIndex        =   14
      Top             =   3600
      Width           =   825
   End
   Begin VB.TextBox txt_pre_all 
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
      Left            =   7200
      MaxLength       =   3
      TabIndex        =   13
      Top             =   3150
      Width           =   810
   End
   Begin VB.ComboBox cmb_pre_con_pom 
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
      Left            =   2745
      Style           =   2  'Dropdown List
      TabIndex        =   8
      Top             =   4275
      Width           =   975
   End
   Begin VB.ComboBox cmb_pre_con_pom 
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
      Left            =   2745
      Style           =   2  'Dropdown List
      TabIndex        =   7
      Top             =   3870
      Width           =   975
   End
   Begin VB.ComboBox cmb_pre_con_pom 
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
      Left            =   2745
      Style           =   2  'Dropdown List
      TabIndex        =   6
      Top             =   3465
      Width           =   975
   End
   Begin VB.ComboBox cmb_usc_liv 
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
      Left            =   2730
      Style           =   2  'Dropdown List
      TabIndex        =   5
      Top             =   3045
      Width           =   1695
   End
   Begin VB.ComboBox cmb_usc_liv 
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
      Left            =   2730
      Style           =   2  'Dropdown List
      TabIndex        =   4
      Top             =   2640
      Width           =   1695
   End
   Begin VB.ComboBox cmb_usc_liv 
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
      Left            =   2730
      Style           =   2  'Dropdown List
      TabIndex        =   3
      Top             =   2235
      Width           =   1695
   End
   Begin VB.TextBox txt_min_for_mac 
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
      Left            =   7200
      MaxLength       =   4
      TabIndex        =   9
      Top             =   1000
      Width           =   810
   End
   Begin VB.CheckBox chk_sca_liv 
      Alignment       =   1  'Right Justify
      BackColor       =   &H80000005&
      Caption         =   "Scarto livello 3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   350
      Index           =   2
      Left            =   8880
      MaskColor       =   &H00FFFFFF&
      TabIndex        =   24
      Top             =   3960
      Value           =   1  'Checked
      Width           =   2760
   End
   Begin VB.CheckBox chk_sca_liv 
      Alignment       =   1  'Right Justify
      BackColor       =   &H80000005&
      Caption         =   "Scarto livello 2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   350
      Index           =   1
      Left            =   8880
      MaskColor       =   &H00FFFFFF&
      TabIndex        =   23
      Top             =   3480
      Value           =   1  'Checked
      Width           =   2760
   End
   Begin VB.CheckBox chk_sca_liv 
      Alignment       =   1  'Right Justify
      BackColor       =   &H80000005&
      Caption         =   "Scarto livello 1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   350
      Index           =   0
      Left            =   8880
      MaskColor       =   &H00FFFFFF&
      TabIndex        =   22
      Top             =   3000
      Value           =   1  'Checked
      Width           =   2760
   End
   Begin VB.CheckBox chk_cut_to_mar 
      Alignment       =   1  'Right Justify
      BackColor       =   &H80000005&
      Caption         =   "Cut to mark"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   350
      Left            =   8880
      MaskColor       =   &H00FFFFFF&
      TabIndex        =   18
      Top             =   1050
      Value           =   1  'Checked
      Width           =   2760
   End
   Begin VB.ComboBox cmb_for_mac 
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
      Left            =   2730
      Style           =   2  'Dropdown List
      TabIndex        =   2
      Top             =   1815
      Width           =   1005
   End
   Begin VB.ComboBox cmb_num_mot_liv_tag 
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
      Left            =   2730
      Style           =   2  'Dropdown List
      TabIndex        =   1
      Top             =   1410
      Width           =   660
   End
   Begin VB.ComboBox cmb_num_liv_tag 
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
      Left            =   2730
      Style           =   2  'Dropdown List
      TabIndex        =   0
      Top             =   1000
      Width           =   660
   End
   Begin VB.TextBox txt_lun_tag_sca 
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
      Left            =   7200
      MaxLength       =   4
      TabIndex        =   12
      Top             =   2235
      Width           =   810
   End
   Begin VB.TextBox txt_max_lun_tag 
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
      Left            =   7200
      MaxLength       =   4
      TabIndex        =   11
      Top             =   1815
      Width           =   810
   End
   Begin VB.TextBox txt_min_lun_tag 
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
      Left            =   7200
      MaxLength       =   4
      TabIndex        =   10
      Top             =   1410
      Width           =   810
   End
   Begin VB.TextBox txt_idle 
      Appearance      =   0  'Flat
      Height          =   280
      Left            =   3600
      TabIndex        =   33
      Top             =   -2000
      Width           =   800
   End
   Begin VB.Label Label27 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Lunghezza di taglio scarto + pattern automatico"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   525
      Left            =   4755
      TabIndex        =   59
      Top             =   2730
      Width           =   2415
   End
   Begin VB.Label Label4 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "mm"
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
      Height          =   195
      Left            =   8070
      TabIndex        =   58
      Top             =   2805
      Width           =   420
   End
   Begin VB.Label Label3 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Tipo azionamento di taglio"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   315
      Left            =   4560
      TabIndex        =   56
      Top             =   5010
      Width           =   2475
   End
   Begin VB.Line Line1 
      X1              =   8610
      X2              =   8610
      Y1              =   510
      Y2              =   5850
   End
   Begin VB.Label Label26 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Volt"
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
      Height          =   195
      Left            =   8055
      TabIndex        =   55
      Top             =   4590
      Width           =   420
   End
   Begin VB.Label Label25 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Soglia abil. linea su DC Bus"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   300
      Left            =   4740
      TabIndex        =   54
      Top             =   4590
      Width           =   2415
   End
   Begin VB.Label Label24 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Tipo macchina"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   315
      Left            =   4740
      TabIndex        =   53
      Top             =   4125
      Width           =   2115
   End
   Begin VB.Label Label23 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "g/m²"
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
      Height          =   315
      Left            =   8070
      TabIndex        =   52
      Top             =   3660
      Width           =   420
   End
   Begin VB.Label Label22 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Grammatura carta max"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   315
      Left            =   4755
      TabIndex        =   51
      Top             =   3675
      Width           =   2115
   End
   Begin VB.Label Label21 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Preallarme"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   300
      Left            =   4755
      TabIndex        =   50
      Top             =   3240
      Width           =   2415
   End
   Begin VB.Label Label20 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "m"
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
      Height          =   195
      Left            =   8115
      TabIndex        =   49
      Top             =   3240
      Width           =   420
   End
   Begin VB.Label Label19 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Presenza controllo pompa livello 3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   465
      Left            =   270
      TabIndex        =   48
      Top             =   4260
      Width           =   2475
   End
   Begin VB.Label Label18 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Presenza controllo pompa livello 2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   465
      Left            =   270
      TabIndex        =   47
      Top             =   3840
      Width           =   2475
   End
   Begin VB.Label Label17 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Presenza controllo pompa livello 1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   465
      Left            =   270
      TabIndex        =   46
      Top             =   3435
      Width           =   2475
   End
   Begin VB.Label Label10 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Uscita livello 3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   315
      Left            =   255
      TabIndex        =   45
      Top             =   3135
      Width           =   2475
   End
   Begin VB.Label Label8 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Uscita livello 2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   315
      Left            =   255
      TabIndex        =   44
      Top             =   2730
      Width           =   2475
   End
   Begin VB.Label Label7 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Uscita livello 1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   315
      Left            =   255
      TabIndex        =   43
      Top             =   2325
      Width           =   2475
   End
   Begin VB.Label Label6 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Minimo formato macchina"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   315
      Left            =   4755
      TabIndex        =   42
      Top             =   1095
      Width           =   2415
   End
   Begin VB.Label Label5 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "mm"
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
      Height          =   195
      Left            =   8070
      TabIndex        =   41
      Top             =   1090
      Width           =   420
   End
   Begin VB.Label Label2 
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "OPTIONAL"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00800000&
      Height          =   285
      Left            =   8925
      TabIndex        =   40
      Top             =   450
      Width           =   1230
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "CARATTERISTICHE TECNICHE"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00800000&
      Height          =   285
      Left            =   240
      TabIndex        =   39
      Top             =   450
      Width           =   3615
   End
   Begin VB.Label Label16 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "mm"
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
      Height          =   195
      Left            =   8070
      TabIndex        =   38
      Top             =   2400
      Width           =   420
   End
   Begin VB.Label Label15 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "mm"
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
      Height          =   195
      Left            =   8070
      TabIndex        =   37
      Top             =   1905
      Width           =   420
   End
   Begin VB.Label Label9 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "mm"
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
      Height          =   195
      Left            =   8070
      TabIndex        =   36
      Top             =   1500
      Width           =   420
   End
   Begin VB.Label Label14 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Lunghezza di taglio scarto"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   300
      Left            =   4755
      TabIndex        =   35
      Top             =   2325
      Width           =   2415
   End
   Begin VB.Label Label13 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Max. lunghezza di taglio"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   315
      Left            =   4755
      TabIndex        =   34
      Top             =   1905
      Width           =   2415
   End
   Begin VB.Label Label12 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Min. lunghezza di taglio"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   315
      Left            =   4755
      TabIndex        =   32
      Top             =   1500
      Width           =   2415
   End
   Begin VB.Label Label11 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Formato macchina"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   315
      Left            =   255
      TabIndex        =   31
      Top             =   1905
      Width           =   2475
   End
   Begin VB.Label n_mot_liv_tag 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "N° motori per livello di taglio"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   315
      Left            =   240
      TabIndex        =   30
      Top             =   1500
      Width           =   2475
   End
   Begin VB.Label n_liv_tag 
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "N° livelli di taglio"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   315
      Left            =   255
      TabIndex        =   29
      Top             =   1090
      Width           =   2475
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "PARAMETRI MACCHINA"
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
      TabIndex        =   28
      Top             =   0
      Width           =   7785
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "set_par_mac.frx":0000
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "set_par_mac"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
'Iciliouff 03-11-2003
Private WithEvents rmt_set_par_mac_sch As rmt
Attribute rmt_set_par_mac_sch.VB_VarHelpID = -1
Private WithEvents rmt_set_par_mac_plc As rmt
Attribute rmt_set_par_mac_plc.VB_VarHelpID = -1
Private WithEvents rmt_set_par_mac_macro As rmt
Attribute rmt_set_par_mac_macro.VB_VarHelpID = -1

Dim col_back_ini As Long
Dim col_fore_ini As Long
Function accetta_nuo_cfg() As Boolean
Dim i As Integer
Dim m As Integer
Dim ok As Boolean

accetta_nuo_cfg = False

For i = 1 To val(par_mac.car_tec.num_liv_tag)
  If par_mac_ute.car_tec.num_mot_liv_tag < par_mac.car_tec.num_mot_liv_tag Then   'Caso in cui i motori non più presenti risultano abilitati
      For m = val(par_mac_ute.car_tec.num_mot_liv_tag) + 1 To val(par_mac.car_tec.num_mot_liv_tag)
          If par_abi_ute.fun_mot_liv(i).fun_mot_mot(m) Then
              messaggio msg("ope_ann") & ". " & msg("dis_mot") & " " & m & " " & msg("liv") & " " & i, True, False
              Exit Function
          End If
      Next m
  End If
  ok = False
  For m = 1 To val(par_mac_ute.car_tec.num_mot_liv_tag)
      If par_abi.fun_mot_liv(i).fun_mot_mot(m) Then
          ok = True
          Exit For
      End If
  Next m
  If Not ok Then          'Caso in cui non ho nessun motore abilitato in seguito alla nuova configurazione
      messaggio msg("imp_dis_all_mot"), True, False
      Exit Function
  End If
Next i

accetta_nuo_cfg = True
End Function

Sub annulla_ope()

par_mac_ute = par_mac
par_abi_ute = par_abi
Fil_par_mac
If Screen.ActiveForm.name = Me.name Then
    disable_tab Me
    txt_idle.SetFocus
End If
mod_in_cor = False

End Sub




Function ver_lun_tag() As Boolean
Dim r As Integer
Dim lun_min As Long
Dim i As Integer

ver_lun_tag = False
lun_min = cur_tag(1).Lun_tag(1)
For i = 2 To val(par_mac.car_tec.num_liv_tag)
  If lun_min > cur_tag(i).Lun_tag(1) Then lun_min = cur_tag(i).Lun_tag(1)
Next i
If val(txt_min_lun_tag.Text) < lun_min Then
  messaggio msg("min_lun_tag") & " " & CStr(lun_min), True, False
Else
  ver_lun_tag = True
End If

End Function

Private Sub chk_cut_to_mar_GotFocus()

chk_cut_to_mar.BackColor = COL_SEL_BLUE
chk_cut_to_mar.ForeColor = vbWhite

End Sub


Private Sub chk_cut_to_mar_LostFocus()

chk_cut_to_mar.BackColor = col_back_ini
chk_cut_to_mar.ForeColor = col_fore_ini

End Sub




Private Sub chk_cut_to_pat_GotFocus(Index As Integer)
  'agrassi_FI15-07-08
'  chk_cut_to_pat.BackColor = COL_SEL_BLUE
'  chk_cut_to_pat.ForeColor = vbWhite
  chk_cut_to_pat(Index).BackColor = COL_SEL_BLUE
  chk_cut_to_pat(Index).ForeColor = vbWhite
  
End Sub


Private Sub chk_cut_to_pat_LostFocus(Index As Integer)

  'agrassi_FI15-07-08
'  chk_cut_to_pat.BackColor = col_back_ini
'  chk_cut_to_pat.ForeColor = col_fore_ini
  chk_cut_to_pat(Index).BackColor = col_back_ini
  chk_cut_to_pat(Index).ForeColor = col_fore_ini

End Sub

'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
'Private Sub chk_cut_to_pat_auto_GotFocus()
'
'  chk_cut_to_pat_auto.BackColor = COL_SEL_BLUE
'  chk_cut_to_pat_auto.ForeColor = vbWhite
'
'End Sub
'
'
'Private Sub chk_cut_to_pat_auto_LostFocus()
'
'  chk_cut_to_pat_auto.BackColor = col_back_ini
'  chk_cut_to_pat_auto.ForeColor = col_fore_ini
'
'End Sub
Private Sub chk_cut_to_pat_auto_GotFocus(Index As Integer)
  
  chk_cut_to_pat_auto(Index).BackColor = COL_SEL_BLUE
  chk_cut_to_pat_auto(Index).ForeColor = vbWhite
  
End Sub


Private Sub chk_cut_to_pat_auto_LostFocus(Index As Integer)

  chk_cut_to_pat_auto(Index).BackColor = col_back_ini
  chk_cut_to_pat_auto(Index).ForeColor = col_fore_ini

End Sub

Private Sub chk_sca_liv_GotFocus(Index As Integer)
chk_sca_liv(Index).BackColor = COL_SEL_BLUE
chk_sca_liv(Index).ForeColor = vbWhite
End Sub


Private Sub chk_sca_liv_LostFocus(Index As Integer)
chk_sca_liv(Index).BackColor = col_back_ini
chk_sca_liv(Index).ForeColor = col_fore_ini
End Sub


Private Sub Combo1_Change()

End Sub

Private Sub eve_key_F1(Shift As Integer)
If Not mod_in_cor Then
  Select Case Shift
  Case 0
    eve_key.chg Sel_lin, keep:=1
  'Case 2
    'eve_key.chg Frm_data, keep:=1
  Case Else
  
  End Select
End If
End Sub

Private Sub eve_key_F4(Shift As Integer)
If Not mod_in_cor Then
  Select Case Shift
  Case 0
    eve_key.chg set_par_com
  Case 2
    
  Case Else
  
  End Select
End If
End Sub

Private Sub eve_key_F8(Shift As Integer)
'Select Case Shift
  'Case 0
   ' If Not mod_in_cor Then eve_key.chg abilitazioni
  'Case Else
'End Select
End Sub


Private Sub eve_key_F9(Shift As Integer)
Select Case Shift
  Case 0
    If Not mod_in_cor Then eve_key.chg pag_lis_ord
  Case Else
End Select
End Sub


Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
On Error GoTo STAG_Gest_ERR
  
  If ActiveControl Is Nothing Then Exit Sub
  Select Case KeyCode
    Case vbKeyReturn, vbKeyDown
      chk_shift 'controllo dello stato del tasto SHIFT
      KeyCode = 0
      key_tab (False)
    Case vbKeyUp
      chk_shift 'controllo dello stato del tasto SHIFT
      KeyCode = 0
      key_tab (True)
  End Select
  
  Exit Sub

STAG_Gest_ERR:
log_str "on error intervenuta in Form_KeyDown della form " + Me.name + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next
End Sub

Private Sub rmt_set_par_mac_macro_retbad(err As String)
'Gestione risposta negativa da macro

  If err <> "" Then messaggio err, True
  rmt_set_par_mac_macro.resetta
  
End Sub

Private Sub rmt_set_par_mac_macro_retok(s As String)
'Gestisce risposta positiva da macro
  
  If s <> "" Then messaggio s, False
  rmt_set_par_mac_macro.resetta
  
End Sub

Private Sub rmt_set_par_mac_plc_retbad(err As String)
'Gestisce risposta negativa da Plc
  
  If err <> "" Then messaggio err, True
  rmt_set_par_mac_plc.resetta
  
End Sub

Private Sub rmt_set_par_mac_plc_retok(s As String)
'Gestisce risposta positiva da Plc
  
  If s <> "" Then messaggio s, False
  rmt_set_par_mac_plc.resetta
  
End Sub


Private Sub rmt_set_par_mac_sch_retbad(err As String)
'Gestisce risposta negativa da scheda di taglio

  If err <> "" Then messaggio err, True
  rmt_set_par_mac_sch.resetta

End Sub

Private Sub rmt_set_par_mac_sch_retok(s As String)
'Gestisce risposta positiva da scheda di taglio

  If s <> "" Then messaggio s, False
  rmt_set_par_mac_sch.resetta

End Sub

Private Sub txt_lun_tag_sca_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub
'RFAZZI_FI15/06/2009. Aggiunto parametro di Lunghezza di Taglio a Default differenziata in caso di Cut to Patter Automatico e Gruppo di Scarto
Private Sub txt_lun_tag_sca_cut_pat_auto_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub
Private Sub txt_max_lun_tag_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_min_for_mac_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_min_lun_tag_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub















Private Sub Fil_par_mac()
'riempie i test_box della pagina di settaggio macchina con le variabili attuali
' settaggio macchina
Dim i As Integer
Dim c As Control

'caricamento dei parametri nella form
txt_min_lun_tag.Text = Trim(par_mac.car_tec.min_lun_tag)
txt_max_lun_tag.Text = Trim(par_mac.car_tec.max_lun_tag)
txt_lun_tag_sca.Text = Trim(par_mac.car_tec.lun_tag_sca)
'RFAZZI_FI15/06/2009. Aggiunto parametro di Lunghezza di Taglio a Default differenziata in caso di Cut to Patter Automatico e Gruppo di Scarto
txt_lun_tag_sca_cut_pat_auto.Text = Trim(par_mac.car_tec.lun_tag_sca_pat_aut)
txt_min_for_mac.Text = Trim(par_mac.car_tec.min_for_mac)
txt_sgl_abi_lin_DC_Bus = Trim(par_mac.car_tec.sgl_abi_lin_DC_Bus)   'Arturo uff 09-09-03
txt_pre_all.Text = Trim$(par_mac.car_tec.pre_all)
'ripulisco le combo box della form
cmb_num_liv_tag.Clear
cmb_num_mot_liv_tag.Clear
cmb_for_mac.Clear
For i = 0 To 2
  cmb_usc_liv(i).Clear
Next i
cmb_gra_car.Clear   'Arturo uff 05-11-02
cmb_nor_mac.Clear   'Arturo uff 06-03-03
cmb_tip_azi.Clear   ' ABorelli_FI11/11/2005_1

'imposto le combo box della form
For i = 1 To 3
    cmb_num_liv_tag.AddItem i, i - 1
    cmb_num_mot_liv_tag.AddItem i, i - 1
Next i
cmb_for_mac.AddItem 1800, 0
cmb_for_mac.AddItem 2500, 1
cmb_for_mac.AddItem 2800, 2
For i = 1 To val(par_mac.car_tec.num_liv_tag)
  cmb_usc_liv(i - 1).Enabled = True
  cmb_usc_liv(i - 1).AddItem "Sandwich", 0
  cmb_usc_liv(i - 1).AddItem "Rulletti", 1
  cmb_pre_con_pom(i - 1).Enabled = True
  cmb_pre_con_pom(i - 1).AddItem "No", 0
  cmb_pre_con_pom(i - 1).AddItem "Si", 1
Next i
cmb_gra_car.AddItem 1800, 0 'Arturo uff 05-11-02
cmb_gra_car.AddItem 2600, 1 'Arturo uff 05-11-02
cmb_nor_mac.AddItem "CE", 0 'Arturo uff 06-03-03
cmb_nor_mac.AddItem "UL", 1 'Arturo uff 06-03-03

For i = val(par_mac.car_tec.num_liv_tag) + 1 To 3
  cmb_usc_liv(i - 1).Enabled = False
  cmb_usc_liv(i - 1).BackColor = LIGHT_GRAY
  cmb_pre_con_pom(i - 1).Enabled = False
  cmb_pre_con_pom(i - 1).BackColor = LIGHT_GRAY
Next i

cmb_tip_azi.AddItem "LENZE"     ' ABorelli_FI11/11/2005_1
cmb_tip_azi.AddItem "TDEMACNO"  ' ABorelli_FI11/11/2005_1
'imposto le combobox con i valori che ho nell'INI

cmb_for_mac.ListIndex = Trim(par_mac.car_tec.for_mac)
cmb_gra_car.ListIndex = par_mac.car_tec.gra_car   'Arturo uff 05-11-02
cmb_nor_mac.ListIndex = par_mac.car_tec.nor_mac   'Arturo uff 06-03-03
cmb_num_liv_tag.ListIndex = Trim(par_mac.car_tec.num_liv_tag - 1)
cmb_num_mot_liv_tag.ListIndex = Trim(par_mac.car_tec.num_mot_liv_tag - 1)
For i = 1 To val(par_mac.car_tec.num_liv_tag)
  cmb_usc_liv(i - 1).ListIndex = val(par_mac.car_tec.sel_usc_liv(i))
  cmb_pre_con_pom(i - 1).ListIndex = val(par_mac.car_tec.pre_con_pom(i))
Next i
cmb_tip_azi.ListIndex = val(par_mac.car_tec.tip_azi_tag)    ' ABorelli_FI11/11/2005_1

'imposto le checkbox
chk_cut_to_mar.Value = CInt(par_mac.opt.cut_mar)
'Iciliouff 20-11-2003
'agrassi_FI15-07-08
'chk_cut_to_pat.value = CInt(par_mac.opt.cut_pat)
For i = 1 To val(par_mac.car_tec.num_liv_tag)
  'agrassi_FI15-07-08
  chk_cut_to_pat(i - 1).Enabled = True
  chk_cut_to_pat(i - 1).Value = CInt(par_mac.opt.cut_pat(i))
  chk_sca_liv(i - 1).Enabled = True
  chk_sca_liv(i - 1).Value = val(Trim$(par_mac.opt.sca_liv(i)))
  'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
  chk_cut_to_pat_auto(i - 1).Enabled = True
  chk_cut_to_pat_auto(i - 1).Value = CInt(val(par_mac.opt.cut_pat_auto(i)))
Next i
For i = val(par_mac.car_tec.num_liv_tag) + 1 To 3
  'agrassi_FI15-07-08
  chk_cut_to_pat(i - 1).Enabled = False
  chk_cut_to_pat(i - 1).Value = 0
  chk_sca_liv(i - 1).Enabled = False
  chk_sca_liv(i - 1).Value = 0
  'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
  chk_cut_to_pat_auto(i - 1).Enabled = False
  chk_cut_to_pat_auto(i - 1).Value = 0
Next i

'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
'Robertouff 26-11-2007
'chk_cut_to_pat_auto.Value = CInt(par_mac.opt.cut_pat_auto)

End Sub
Sub scr_par_mac_ute()
'caricamento dei parametri dalla form nella struttura di appoggio
Dim i As Integer

  par_mac_ute.car_tec.num_liv_tag = cmb_num_liv_tag.ListIndex + 1
  par_mac_ute.car_tec.num_mot_liv_tag = cmb_num_mot_liv_tag.ListIndex + 1
  par_mac_ute.car_tec.for_mac = cmb_for_mac.ListIndex
  par_mac_ute.car_tec.gra_car = cmb_gra_car.ListIndex   'Arturo uff 05-11-02
  par_mac_ute.car_tec.nor_mac = cmb_nor_mac.ListIndex   'Arturo uff 06-03-03
  par_mac_ute.car_tec.min_for_mac = txt_min_for_mac.Text
  par_mac_ute.car_tec.min_lun_tag = txt_min_lun_tag.Text
  par_mac_ute.car_tec.max_lun_tag = txt_max_lun_tag.Text
  par_mac_ute.car_tec.lun_tag_sca = txt_lun_tag_sca.Text
  'RFAZZI_FI15/06/2009. Aggiunto parametro di Lunghezza di Taglio a Default differenziata in caso di Cut to Patter Automatico e Gruppo di Scarto
  par_mac_ute.car_tec.lun_tag_sca_pat_aut = txt_lun_tag_sca_cut_pat_auto.Text
  par_mac_ute.car_tec.pre_all = txt_pre_all.Text
  par_mac_ute.car_tec.sgl_abi_lin_DC_Bus = txt_sgl_abi_lin_DC_Bus.Text  'Arturo uff 09-09-03
  par_mac_ute.car_tec.tip_azi_tag = cmb_tip_azi.ListIndex   ' ABorelli_FI11/11/2005_1
  
  'PMASSARO_FI08/02/10 - Non si riusciva più ad abilitare il Cut-To-Mark
  '''''''''''''''''''''''''''''''
  'Iciliouff 20-11-2003
  'chk_cut_to_mar.Value = val(par_mac_ute.opt.cut_mar)
  ''''''''''''''''''''''''''''''''''''''''''''''''''''
  
  'agrassi_FI15-07-08
  'par_mac_ute.opt.cut_pat = Format$(chk_cut_to_pat.Value)
  'Robertouff 26-11-2007
  'par_mac_ute.opt.cut_mar = IIf(chk_cut_to_pat.value = 0, Format$(chk_cut_to_mar.value), STR_FALSE)
  
  'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
  'par_mac_ute.opt.cut_mar = IIf((chk_cut_to_pat(0).Value = 0) And (chk_cut_to_pat(1).Value = 0) And (chk_cut_to_pat(2).Value = 0) And (chk_cut_to_pat_auto.Value = 0), Format$(chk_cut_to_mar.Value), STR_FALSE)
  par_mac_ute.opt.cut_mar = IIf((chk_cut_to_pat(0).Value = 0) And (chk_cut_to_pat(1).Value = 0) And (chk_cut_to_pat(2).Value = 0) And (chk_cut_to_pat_auto(0).Value = 0) And (chk_cut_to_pat_auto(1).Value = 0) And (chk_cut_to_pat_auto(2).Value = 0), Format$(chk_cut_to_mar.Value), STR_FALSE)
  
  'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
  'Robertouff 26-11-2007
  'par_mac_ute.opt.cut_pat_auto = Format$(chk_cut_to_pat_auto.Value)
  
  'PMASSARO_FI08/02/10 - Non si riusciva più ad abilitare il Cut-To-Mark
  chk_cut_to_mar.Value = val(par_mac_ute.opt.cut_mar)
  
  For i = 1 To 3
    'agrassi_FI15-07-08
    par_mac_ute.opt.cut_pat(i) = chk_cut_to_pat(i - 1).Value
    par_mac_ute.opt.sca_liv(i) = chk_sca_liv(i - 1).Value
    par_mac_ute.car_tec.sel_usc_liv(i) = IIf(cmb_usc_liv(i - 1).ListIndex = -1, "", cmb_usc_liv(i - 1).ListIndex)
    par_mac_ute.car_tec.pre_con_pom(i) = IIf(cmb_pre_con_pom(i - 1).ListIndex = -1, "", cmb_pre_con_pom(i - 1).ListIndex)
  
    'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
    par_mac_ute.opt.cut_pat_auto(i) = Format$(chk_cut_to_pat_auto(i - 1).Value)
    
    'Arturo uff 20-02-03
    With par_mac_ute.rul_usc.rul_usc_liv(i)
      If cmb_usc_liv(i - 1).ListIndex >= 0 Then
        If val(par_mac_ute.car_tec.sel_usc_liv(i)) = 1 Then
          'se rulletti imposto automaticamente la lunghezza a 0
          'Arturo uff 30-04-03
          '.lun_san = "0"
          .lun_san = LUN_SAN_RUL
        Else
          'se sandwich controllo: se la lunghezza è = 0 la forzo al valore di default
          If val(.lun_san) = val(LUN_SAN_RUL) Then .lun_san = getFileINIString("rul_usc_liv_" & i, "lun_def_san", "1700")
        End If
      End If
    End With
  Next i
 
End Sub



Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
    If mod_in_cor Then
        mod_in_cor = False
        par_mac_ute = par_mac
        Fil_par_mac
        disable_tab Me
        txt_idle.SetFocus
        disable_frames Me   ' Arturo uff 05-11-02 x disabilitazione controlli su frame
    End If
    txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
    txt_idle.SetFocus
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
    If Not mod_in_cor Then
      If sta_tag.com_OK Then
          If sta_tag.MAC_F_STOP(1) Then
            mod_in_cor = True
            enable_frames Me
            enable_tab Me
            txt_idle.TabStop = False
            cmb_num_liv_tag.SetFocus
          Else
            messaggio msg("mac_non_fas_sto"), True, False
          End If
      Else 'sta_tag.com_OK = false
          messaggio msg("com_int_con_mac"), True
      End If
    End If
  Case Else
End Select
End Sub

Private Sub eve_key_F3(Shift As Integer)
'salva
Dim i As Integer
Dim num_mot_abi As Integer
Dim problema As Boolean
Dim l As Integer
Dim snd_ST As Boolean

Select Case Shift
  Case 0
    If mod_in_cor Then
'NC220816Arturo Uff 22-07-02 si è cambiato il controllo sul numero di livelli per permetterne la scrittura su file STAG.INI
'      If cmb_num_liv_tag.Text <> par_mac.car_tec.num_liv_tag Then
'          messaggio msg("imp_cam_num_liv_tag"), True, False
'          annulla_ope
'          Exit Sub
'      End If
      If cmb_num_liv_tag.Text < 1 Or cmb_num_liv_tag.Text > 3 Then
        messaggio msg("imp_cam_num_liv_tag"), True, False
        annulla_ope
        Exit Sub
      Else
        par_mac.car_tec.num_liv_tag = cmb_num_liv_tag.Text
      End If
'Arturo Uff 25-07-02
'      If cmb_for_mac.ListIndex <> par_mac.car_tec.for_mac Then
      If (cmb_for_mac.ListIndex + 1) < tip_for_mac.mm1800 Or (cmb_for_mac.ListIndex + 1) > tip_for_mac.mm2800 Then
        messaggio msg("imp_cam_for_mac"), True, False
        annulla_ope
        Exit Sub
      Else
        par_mac.car_tec.for_mac = cmb_for_mac.ListIndex
      End If
'Arturo uff 05-11-02 inserito controllo validità dato grammatura
      If (cmb_gra_car.ListIndex) < tip_gra_car.gm2_1800 Or (cmb_gra_car.ListIndex) > tip_gra_car.gm2_2600 Then
        messaggio msg("imp_cam_gra_car"), True, False
        annulla_ope
        Exit Sub
      Else
        par_mac.car_tec.gra_car = cmb_gra_car.ListIndex
      End If
'Arturo uff 06-03-03 inserito controllo validità dato tipo normative macchina
      If (cmb_gra_car.ListIndex) < TIP_NOR_MAC.CE Or (cmb_gra_car.ListIndex) > TIP_NOR_MAC.UL Then
        messaggio msg("imp_cam_tip_mac"), True, False
        annulla_ope
        Exit Sub
      Else
        par_mac.car_tec.nor_mac = cmb_nor_mac.ListIndex
      End If
'Arturo uff 09-09-03 inserito controllo validità dato soglia abilitazione linea per DC Bus
      If val(txt_sgl_abi_lin_DC_Bus.Text) < LIM_INF_SGL_ABI_LIN_DC_BUS Or val(txt_sgl_abi_lin_DC_Bus.Text) > LIM_SUP_SGL_ABI_LIN_DC_BUS Then
        txt_sgl_abi_lin_DC_Bus.ForeColor = vbRed
        messaggio msg("par_err") + ". (" + Format$(LIM_INF_SGL_ABI_LIN_DC_BUS) + "-" + Format$(LIM_SUP_SGL_ABI_LIN_DC_BUS) + " Volt)", True, False
        annulla_ope
        Exit Sub
      Else
        txt_sgl_abi_lin_DC_Bus.ForeColor = vbBlack
        par_mac.car_tec.sgl_abi_lin_DC_Bus = txt_sgl_abi_lin_DC_Bus.Text
      End If
'Arturo Uff 25-07-02
'      If txt_min_for_mac.Text <> Trim$(par_mac_ute.car_tec.min_for_mac) Then
      If val(txt_min_for_mac.Text) <= 0 Or val(txt_min_for_mac.Text) > val(cmb_for_mac.Text) Then
        messaggio msg("err_min_for_mac"), True, False
        annulla_ope
        Exit Sub
      Else
        par_mac_ute.car_tec.min_for_mac = Format(txt_min_for_mac.Text)
      End If
'      End If
'Arturo Uff 25-07-02
'      If txt_min_lun_tag.Text <> Trim$(par_mac.car_tec.min_lun_tag) Then
      If txt_min_lun_tag.Text <> Trim$(par_mac.car_tec.min_lun_tag) Then
        If Not ver_lun_tag Then
          annulla_ope
          Exit Sub
        End If
      End If
      messaggio msg("ope_in_cor"), False, False
      scr_par_mac_ute
      snd_ST = False
      ' verifico se è stato cambiato il parametro numero di motori per livello: in questo caso verifico se la nuova configurazione è accettabile, e se lo è,
      ' devo modificare alcuni parametri per la scheda di taglio
      If par_mac_ute.car_tec.num_mot_liv_tag <> par_mac.car_tec.num_mot_liv_tag Then
          If Not dbf_vuo("Par_cfg") And False Then    'iciliouff 03-11-2003 Da togliere and false
              If Not accetta_nuo_cfg Then
                  annulla_ope
                  Exit Sub
              Else
                  messaggio msg("let_par_cfg"), False, False
                  problema = False
                  For i = 1 To val(par_mac.car_tec.num_liv_tag)
'Arturo uff 05-11-02
'                      If Not trova_cfg_adatta(val(par_mac.car_tec.for_mac), val(par_mac_ute.car_tec.num_mot_liv_tag), par_abi.num_mot_abi(i), i) Then ' configurazione non trovata
                      If Not trova_cfg_adatta(val(par_mac.car_tec.for_mac), val(par_mac.car_tec.gra_car), val(par_mac_ute.car_tec.num_mot_liv_tag), par_abi.num_mot_abi(i), i) Then ' configurazione non trovata
                          messaggio msg("par_cfg_non_tro") & " " & i, True, False ' ????? si potrebbe anche inserire la nuova configurazione con parametri della scheda di taglio aggiornati di una certa percentuale
                          problema = True
                          Exit For
                      End If
                  Next i
                  If problema Then
                      ' Non è stata trovata una configurazione adatta per un livello di taglio
                      annulla_ope
                      Exit Sub
                  Else
                      'scrivo nelle scheda di taglio
                      num_com_sch_tag = val(par_mac.car_tec.num_liv_tag)
                      CASO_SND_SCH_TAG = CAMBIO_STATO_MACCHINA
                      snd_ST = True
                      If Not rmt_set_par_mac_sch.esegui(tip_cmd.CONFIGURA, GetTickCount(), 0, 0, True) Then
                        messaggio msg("imp_ese_ope"), True
                      End If
'                      For i = 1 To val(par_mac.car_tec.num_liv_tag)
'                          snd_sch_tag i, CONFIGURA
'                      Next i
                  End If
              End If
          Else 'non ho alcuna configurazione nel dbf e permetto la modifica del numero di motori presenti per livello di taglio
              'carico i nuovi valori per la curva di taglio
              rmt_set_par_mac_plc.abi_vis_err = False
              If rmt_set_par_mac_plc.esegui(tip_ope_PLC.PARAM_CUR_TAG, GetTickCount(), 0, 0, True) Then
                par_mac.car_tec.num_mot_liv_tag = par_mac_ute.car_tec.num_mot_liv_tag
              Else
                'La rmt della scheda di taglio è occupata, annullo l'operazione
                annulla_ope
                Exit Sub
              End If
'              For i = 1 To val(par_mac.car_tec.num_liv_tag)
''Arturo uff 05-11-02
''                If carica_new_par_cur_tag(i, val(par_mac.car_tec.for_mac), val(par_mac.car_tec.num_mot_liv_tag), par_abi.num_mot_abi(i)) Then
'                If carica_new_par_cur_tag(i, val(par_mac.car_tec.for_mac), val(par_mac.car_tec.gra_car), val(par_mac.car_tec.num_mot_liv_tag), par_abi.num_mot_abi(i)) Then
'                    snd_str_PLC PARAM_CUR_TAG, , i
'                Else
'                    messaggio msg("val_non_tro_cur_tag"), True, False
'                End If
'              Next i
          End If
      End If
      
      par_mac.car_tec.lun_tag_sca = par_mac_ute.car_tec.lun_tag_sca
      'RFAZZI_FI15/06/2009. Aggiunto parametro di Lunghezza di Taglio a Default differenziata in caso di Cut to Patter Automatico e Gruppo di Scarto
      par_mac.car_tec.lun_tag_sca_pat_aut = par_mac_ute.car_tec.lun_tag_sca_pat_aut
      par_mac.car_tec.max_lun_tag = par_mac_ute.car_tec.max_lun_tag
      par_mac.car_tec.min_lun_tag = par_mac_ute.car_tec.min_lun_tag
      par_mac.car_tec.pre_all = par_mac_ute.car_tec.pre_all
      par_mac.car_tec.min_for_mac = par_mac_ute.car_tec.min_for_mac
      par_mac.car_tec.gra_car = par_mac_ute.car_tec.gra_car 'Arturo uff 05-11-02
      par_mac.car_tec.nor_mac = par_mac_ute.car_tec.nor_mac 'Arturo uff 06-03-03
      par_mac.car_tec.sgl_abi_lin_DC_Bus = par_mac_ute.car_tec.sgl_abi_lin_DC_Bus 'Arturo uff 09-09-03
      For i = 1 To 3
        par_mac.car_tec.sel_usc_liv(i) = par_mac_ute.car_tec.sel_usc_liv(i)
        par_mac.car_tec.pre_con_pom(i) = par_mac_ute.car_tec.pre_con_pom(i)
      Next i
      par_mac.opt = par_mac_ute.opt
      par_mac.rul_usc = par_mac_ute.rul_usc  'Arturo uff 20-02-03
      par_mac.car_tec.tip_azi_tag = par_mac_ute.car_tec.tip_azi_tag ' ABorelli_FI11/11/2005_1
      If Not snd_ST Then
          leggi_cfg 'Arturo uff 05-11-02
          leggi_par_cur_tag 'Arturo uff 05-11-02
          scr_par_mac
          'Iciliouff 03-11-2003
          If Not rmt_set_par_mac_macro.esegui(tip_ope_macro.PLC_PARAM_MAC_PARAM_CFG_MAC, GetTickCount(), 0, 0) Then
            messaggio msg("ope_eff_par"), True
          End If
'          snd_str_PLC PARAM_MAC
'          snd_str_PLC PARAM_CFG_MAC
      End If
      If Screen.ActiveForm.name = Me.name Then
          disable_tab Me
          txt_idle.SetFocus
      End If
      mod_in_cor = False
      disable_frames Me   ' Arturo uff 05-11-02 x disabilitazione controlli su frame
    End If
    txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
    txt_idle.SetFocus
  Case Else
End Select
End Sub

Private Sub eve_key_F5(Shift As Integer)
If Not mod_in_cor Then
  Select Case Shift
  Case 0
    eve_key.chg set_par_sch_tag1
  Case 2
  
  Case Else
  End Select
End If
End Sub

Private Sub eve_key_F6(Shift As Integer)
Select Case Shift
  Case 0
    If Not mod_in_cor Then eve_key.chg set_par_rul
  Case Else
End Select
End Sub

Private Sub eve_key_F7(Shift As Integer)
Select Case Shift
  Case 0
    If Not mod_in_cor Then eve_key.chg imp_all
  Case Else
End Select
End Sub

Private Sub Form_Activate()
'Iciliouff 03-11-2003
Set rmt_set_par_mac_sch = New rmt
rmt_set_par_mac_sch.set_plc_ute (2)
rmt_set_par_mac_sch.name = "rmt_set_par_mac_sch"
txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
txt_idle.SetFocus
disable_tab Me
If mod_in_cor Then mod_in_cor = False
Fil_par_mac
disable_frames Me   ' Arturo uff 05-11-02 x disabilitazione controlli su frame
txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
txt_idle.SetFocus
End Sub

Private Sub Form_Load()
Debug.Print "Inizio Load " & Me.name
Set eve_key = New key
'Iciliouff 03-11-2003
Set rmt_set_par_mac_plc = New rmt
rmt_set_par_mac_plc.set_plc_ute (1)
rmt_set_par_mac_plc.name = "rmt_set_par_mac_plc"
Set rmt_set_par_mac_macro = New rmt
rmt_set_par_mac_macro.set_plc_ute (3)
rmt_set_par_mac_macro.name = "rmt_set_par_mac_macro"

eve_key.create Me
Carica Me
col_back_ini = chk_cut_to_mar.BackColor
col_fore_ini = chk_cut_to_mar.ForeColor
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub




Private Sub txt_pre_all_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


'Arturo uff 09-09-03
Private Sub txt_sgl_abi_lin_DC_Bus_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


