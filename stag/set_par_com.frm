VERSION 5.00
Begin VB.Form set_par_com 
   Appearance      =   0  'Flat
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6300
   ClientLeft      =   975
   ClientTop       =   4140
   ClientWidth     =   11955
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6300
   ScaleMode       =   0  'User
   ScaleWidth      =   20490.61
   ShowInTaskbar   =   0   'False
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
      Height          =   315
      Left            =   0
      TabIndex        =   80
      Top             =   -5000
      Width           =   315
   End
   Begin VB.Frame frm_com 
      BackColor       =   &H00FFFFFF&
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   6010
      Left            =   8
      TabIndex        =   53
      Top             =   300
      Width           =   11940
      Begin VB.Frame Frame3 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         Caption         =   "PLC LAN"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00800000&
         Height          =   1605
         Left            =   8940
         TabIndex        =   96
         Top             =   2600
         Width           =   2910
         Begin VB.CheckBox chk_abi_lan 
            BackColor       =   &H80000005&
            Enabled         =   0   'False
            Height          =   255
            Left            =   2640
            TabIndex        =   19
            TabStop         =   0   'False
            Top             =   840
            Width           =   255
         End
         Begin VB.TextBox txt_timeout_lan 
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
            Left            =   840
            MaxLength       =   5
            TabIndex        =   18
            Text            =   "1000"
            Top             =   1080
            Width           =   915
         End
         Begin VB.TextBox txt_por_plc 
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
            Left            =   840
            MaxLength       =   5
            TabIndex        =   17
            Text            =   "9600"
            Top             =   720
            Width           =   915
         End
         Begin VB.TextBox txt_ind_plc 
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
            Left            =   840
            TabIndex        =   16
            Text            =   "127.0.0.1"
            Top             =   360
            Width           =   1995
         End
         Begin VB.Label Label23 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "TimeOut"
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
            Height          =   210
            Left            =   0
            TabIndex        =   102
            Top             =   1200
            Width           =   825
         End
         Begin VB.Label Label11 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Abilitato"
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
            Height          =   255
            Index           =   7
            Left            =   1920
            TabIndex        =   101
            Top             =   840
            Width           =   735
         End
         Begin VB.Label Label22 
            Alignment       =   1  'Right Justify
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            Caption         =   "Unit ID"
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
            Height          =   300
            Left            =   825
            TabIndex        =   100
            Top             =   4060
            Width           =   735
         End
         Begin VB.Label Label21 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Indirizzo"
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
            Height          =   300
            Left            =   120
            TabIndex        =   99
            Top             =   435
            Width           =   735
         End
         Begin VB.Label Label20 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "ms"
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
            Height          =   210
            Left            =   1800
            TabIndex        =   98
            Top             =   1200
            Width           =   465
         End
         Begin VB.Label Label11 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Porta"
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
            Height          =   255
            Index           =   6
            Left            =   120
            TabIndex        =   97
            Top             =   840
            Width           =   615
         End
      End
      Begin VB.Frame Frame2 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         Caption         =   "AZIONAMENTI"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00800000&
         Height          =   1740
         Left            =   8940
         TabIndex        =   82
         Top             =   4260
         Width           =   2910
         Begin VB.ComboBox cmb_baud_azi 
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
            ItemData        =   "set_par_com.frx":0000
            Left            =   1185
            List            =   "set_par_com.frx":0002
            Style           =   2  'Dropdown List
            TabIndex        =   21
            Top             =   960
            Width           =   945
         End
         Begin VB.TextBox txt_timeout_azi 
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
            Left            =   1185
            MaxLength       =   5
            TabIndex        =   20
            Text            =   "20000"
            Top             =   360
            Width           =   795
         End
         Begin VB.Label Label11 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "kBit / s"
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
            Height          =   255
            Index           =   4
            Left            =   2190
            TabIndex        =   95
            Top             =   1050
            Width           =   735
         End
         Begin VB.Label Label11 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Baud"
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
            Height          =   255
            Index           =   3
            Left            =   240
            TabIndex        =   94
            Top             =   1050
            Width           =   735
         End
         Begin VB.Label Label19 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "ms"
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
            Height          =   210
            Left            =   2040
            TabIndex        =   93
            Top             =   480
            Width           =   585
         End
         Begin VB.Label Label13 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Time-out"
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
            Height          =   300
            Left            =   240
            TabIndex        =   84
            Top             =   435
            Width           =   855
         End
         Begin VB.Label Label5 
            Alignment       =   1  'Right Justify
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            Caption         =   "Unit ID"
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
            Height          =   300
            Left            =   825
            TabIndex        =   83
            Top             =   4060
            Width           =   735
         End
      End
      Begin VB.Frame Frame1 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         Caption         =   "SCHEDE DI TAGLIO"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00800000&
         Height          =   4090
         Left            =   30
         TabIndex        =   71
         Top             =   1910
         Width           =   8850
         Begin VB.CheckBox chk_tip_com_tag_UDP 
            BackColor       =   &H80000005&
            Caption         =   "UDP"
            Height          =   255
            Index           =   2
            Left            =   7950
            TabIndex        =   109
            Top             =   840
            Value           =   1  'Checked
            Width           =   855
         End
         Begin VB.CheckBox chk_tip_com_tag_UDP 
            BackColor       =   &H80000005&
            Caption         =   "UDP"
            Height          =   255
            Index           =   1
            Left            =   5400
            TabIndex        =   108
            Top             =   840
            Value           =   1  'Checked
            Width           =   855
         End
         Begin VB.CheckBox chk_tip_com_tag_UDP 
            BackColor       =   &H80000005&
            Caption         =   "UDP"
            Height          =   255
            Index           =   0
            Left            =   2700
            TabIndex        =   107
            Top             =   840
            Value           =   1  'Checked
            Width           =   855
         End
         Begin VB.TextBox txt_ind_rem_tag 
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
            Left            =   6720
            MaxLength       =   15
            TabIndex        =   45
            Text            =   "200.0.0.200"
            Top             =   1440
            Width           =   1830
         End
         Begin VB.TextBox txt_ind_rem_tag 
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
            Left            =   4200
            MaxLength       =   15
            TabIndex        =   35
            Text            =   "200.0.0.200"
            Top             =   1440
            Width           =   1830
         End
         Begin VB.TextBox txt_ind_rem_tag 
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
            Left            =   1500
            MaxLength       =   15
            TabIndex        =   25
            Text            =   "200.0.0.200"
            Top             =   1440
            Width           =   1830
         End
         Begin VB.TextBox txt_por_rem_tag 
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
            Left            =   6720
            MaxLength       =   6
            TabIndex        =   44
            Text            =   "200"
            Top             =   1000
            Width           =   990
         End
         Begin VB.TextBox txt_por_rem_tag 
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
            Left            =   4200
            MaxLength       =   6
            TabIndex        =   34
            Text            =   "200"
            Top             =   1000
            Width           =   990
         End
         Begin VB.TextBox txt_por_rem_tag 
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
            Left            =   1500
            MaxLength       =   6
            TabIndex        =   24
            Text            =   "200"
            Top             =   1000
            Width           =   990
         End
         Begin VB.TextBox txt_por_loc_tag 
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
            Left            =   6720
            MaxLength       =   6
            TabIndex        =   43
            Text            =   "100"
            Top             =   600
            Width           =   990
         End
         Begin VB.TextBox txt_por_loc_tag 
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
            Left            =   4200
            MaxLength       =   6
            TabIndex        =   33
            Text            =   "100"
            Top             =   600
            Width           =   990
         End
         Begin VB.TextBox txt_por_loc_tag 
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
            Left            =   1500
            MaxLength       =   6
            TabIndex        =   23
            Text            =   "100"
            Top             =   600
            Width           =   990
         End
         Begin VB.ComboBox cmb_tip_com_tag 
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   360
            Index           =   2
            Left            =   6720
            Style           =   2  'Dropdown List
            TabIndex        =   42
            Top             =   240
            Width           =   1200
         End
         Begin VB.ComboBox cmb_tip_com_tag 
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   360
            Index           =   1
            Left            =   4200
            Style           =   2  'Dropdown List
            TabIndex        =   32
            Top             =   240
            Width           =   1200
         End
         Begin VB.ComboBox cmb_tip_com_tag 
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   360
            Index           =   0
            Left            =   1500
            Style           =   2  'Dropdown List
            TabIndex        =   22
            Top             =   240
            Width           =   1200
         End
         Begin VB.ComboBox cmb_bit_stop_sch_tag 
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
            ItemData        =   "set_par_com.frx":0004
            Left            =   7300
            List            =   "set_par_com.frx":0006
            Style           =   2  'Dropdown List
            TabIndex        =   50
            Top             =   3150
            Width           =   735
         End
         Begin VB.ComboBox cmb_bit_stop_sch_tag 
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
            ItemData        =   "set_par_com.frx":0008
            Left            =   4815
            List            =   "set_par_com.frx":000A
            Style           =   2  'Dropdown List
            TabIndex        =   40
            Top             =   3150
            Width           =   735
         End
         Begin VB.ComboBox cmb_bit_stop_sch_tag 
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
            ItemData        =   "set_par_com.frx":000C
            Left            =   2115
            List            =   "set_par_com.frx":000E
            Style           =   2  'Dropdown List
            TabIndex        =   30
            Top             =   3150
            Width           =   735
         End
         Begin VB.ComboBox cmb_dati_sch_tag 
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
            ItemData        =   "set_par_com.frx":0010
            Left            =   6720
            List            =   "set_par_com.frx":0012
            Style           =   2  'Dropdown List
            TabIndex        =   49
            Top             =   3150
            Width           =   555
         End
         Begin VB.ComboBox cmb_dati_sch_tag 
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
            ItemData        =   "set_par_com.frx":0014
            Left            =   4200
            List            =   "set_par_com.frx":0016
            Style           =   2  'Dropdown List
            TabIndex        =   39
            Top             =   3150
            Width           =   555
         End
         Begin VB.ComboBox cmb_dati_sch_tag 
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
            ItemData        =   "set_par_com.frx":0018
            Left            =   1500
            List            =   "set_par_com.frx":001A
            Style           =   2  'Dropdown List
            TabIndex        =   29
            Top             =   3150
            Width           =   555
         End
         Begin VB.ComboBox cmb_bit_par_sch_tag 
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
            ItemData        =   "set_par_com.frx":001C
            Left            =   6720
            List            =   "set_par_com.frx":001E
            Style           =   2  'Dropdown List
            TabIndex        =   48
            Top             =   2730
            Width           =   1020
         End
         Begin VB.ComboBox cmb_bit_par_sch_tag 
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
            ItemData        =   "set_par_com.frx":0020
            Left            =   4200
            List            =   "set_par_com.frx":0022
            Style           =   2  'Dropdown List
            TabIndex        =   38
            Top             =   2730
            Width           =   1020
         End
         Begin VB.ComboBox cmb_bit_par_sch_tag 
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
            ItemData        =   "set_par_com.frx":0024
            Left            =   1500
            List            =   "set_par_com.frx":0026
            Style           =   2  'Dropdown List
            TabIndex        =   28
            Top             =   2730
            Width           =   1020
         End
         Begin VB.ComboBox cmb_baud_sch_tag 
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
            ItemData        =   "set_par_com.frx":0028
            Left            =   6720
            List            =   "set_par_com.frx":002A
            Style           =   2  'Dropdown List
            TabIndex        =   47
            Top             =   2310
            Width           =   1095
         End
         Begin VB.ComboBox cmb_baud_sch_tag 
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
            ItemData        =   "set_par_com.frx":002C
            Left            =   4200
            List            =   "set_par_com.frx":002E
            Style           =   2  'Dropdown List
            TabIndex        =   37
            Top             =   2310
            Width           =   1095
         End
         Begin VB.ComboBox cmb_baud_sch_tag 
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
            ItemData        =   "set_par_com.frx":0030
            Left            =   1500
            List            =   "set_par_com.frx":0032
            Style           =   2  'Dropdown List
            TabIndex        =   27
            Top             =   2310
            Width           =   1095
         End
         Begin VB.TextBox txt_timeout_sch_tag 
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
            Left            =   6720
            MaxLength       =   5
            TabIndex        =   51
            Text            =   "2000"
            Top             =   3550
            Width           =   795
         End
         Begin VB.TextBox txt_timeout_sch_tag 
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
            Left            =   4200
            MaxLength       =   5
            TabIndex        =   41
            Text            =   "2000"
            Top             =   3550
            Width           =   795
         End
         Begin VB.TextBox txt_timeout_sch_tag 
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
            Left            =   1500
            MaxLength       =   5
            TabIndex        =   31
            Text            =   "20000"
            Top             =   3550
            Width           =   795
         End
         Begin VB.TextBox txt_porta_sch_tag 
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
            Left            =   6720
            MaxLength       =   2
            TabIndex        =   46
            Text            =   "4"
            Top             =   1890
            Width           =   375
         End
         Begin VB.TextBox txt_porta_sch_tag 
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
            Left            =   4200
            MaxLength       =   2
            TabIndex        =   36
            Text            =   "3"
            Top             =   1890
            Width           =   375
         End
         Begin VB.TextBox txt_porta_sch_tag 
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
            Left            =   1500
            MaxLength       =   2
            TabIndex        =   26
            Text            =   "5"
            Top             =   1890
            Width           =   375
         End
         Begin VB.Label Label25 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Indirizzo remoto"
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
            Height          =   255
            Left            =   120
            TabIndex        =   106
            Top             =   1560
            Width           =   1335
         End
         Begin VB.Label Label6 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Porta remota"
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
            Height          =   255
            Left            =   120
            TabIndex        =   105
            Top             =   1140
            Width           =   1275
         End
         Begin VB.Label Label4 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Porta locale"
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
            Height          =   255
            Left            =   120
            TabIndex        =   104
            Top             =   720
            Width           =   1275
         End
         Begin VB.Label Label24 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Tipo"
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
            Height          =   255
            Left            =   120
            TabIndex        =   103
            Top             =   300
            Width           =   465
         End
         Begin VB.Label Label18 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "ms"
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
            Height          =   210
            Left            =   7380
            TabIndex        =   92
            Top             =   3600
            Width           =   585
         End
         Begin VB.Label Label17 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "ms"
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
            Height          =   210
            Left            =   4920
            TabIndex        =   91
            Top             =   3600
            Width           =   585
         End
         Begin VB.Label Label16 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "ms"
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
            Height          =   210
            Left            =   2220
            TabIndex        =   90
            Top             =   3600
            Width           =   585
         End
         Begin VB.Label Label2 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Time-out"
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
            Height          =   255
            Left            =   120
            TabIndex        =   86
            Top             =   3630
            Width           =   945
         End
         Begin VB.Label Label_liv 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "3° livello"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   9
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000080FF&
            Height          =   255
            Index           =   2
            Left            =   7995
            TabIndex        =   79
            Top             =   300
            Width           =   795
         End
         Begin VB.Label Label_liv 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "2° livello"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   9
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000080FF&
            Height          =   255
            Index           =   1
            Left            =   5470
            TabIndex        =   78
            Top             =   300
            Width           =   795
         End
         Begin VB.Label Label_liv 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "1° livello"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   9
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000080FF&
            Height          =   255
            Index           =   0
            Left            =   2745
            TabIndex        =   77
            Top             =   300
            Width           =   915
         End
         Begin VB.Label Label12 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Porta"
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
            Height          =   255
            Index           =   0
            Left            =   120
            TabIndex        =   76
            Top             =   1980
            Width           =   735
         End
         Begin VB.Label Label11 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Baud"
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
            Height          =   255
            Index           =   0
            Left            =   120
            TabIndex        =   75
            Top             =   2400
            Width           =   735
         End
         Begin VB.Label Label10 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Parità"
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
            Height          =   255
            Index           =   0
            Left            =   120
            TabIndex        =   74
            Top             =   2820
            Width           =   735
         End
         Begin VB.Label Label9 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Dati"
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
            Height          =   255
            Index           =   0
            Left            =   120
            TabIndex        =   73
            Top             =   3240
            Width           =   735
         End
         Begin VB.Label Label8 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Stop"
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
            Height          =   255
            Index           =   0
            Left            =   720
            TabIndex        =   72
            Top             =   3240
            Width           =   735
         End
      End
      Begin VB.Frame frm_com_spde 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         Caption         =   "PUNTO UNICO"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00800000&
         Height          =   1770
         Left            =   30
         TabIndex        =   58
         Top             =   128
         Width           =   8850
         Begin VB.ComboBox cmb_bit_stop_spde 
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
            ItemData        =   "set_par_com.frx":0034
            Left            =   4650
            List            =   "set_par_com.frx":0036
            Style           =   2  'Dropdown List
            TabIndex        =   8
            Top             =   1345
            Width           =   705
         End
         Begin VB.ComboBox cmb_dati_spde 
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
            ItemData        =   "set_par_com.frx":0038
            Left            =   4050
            List            =   "set_par_com.frx":003A
            Style           =   2  'Dropdown List
            TabIndex        =   7
            Top             =   1345
            Width           =   555
         End
         Begin VB.ComboBox cmb_bit_par_spde 
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
            ItemData        =   "set_par_com.frx":003C
            Left            =   4050
            List            =   "set_par_com.frx":003E
            Style           =   2  'Dropdown List
            TabIndex        =   6
            Top             =   970
            Width           =   1020
         End
         Begin VB.ComboBox cmb_baud_spde 
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
            ItemData        =   "set_par_com.frx":0040
            Left            =   4050
            List            =   "set_par_com.frx":0042
            Style           =   2  'Dropdown List
            TabIndex        =   5
            Top             =   590
            Width           =   1095
         End
         Begin VB.TextBox txt_timeout 
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
            Left            =   1140
            MaxLength       =   5
            TabIndex        =   3
            Text            =   "20000"
            Top             =   1320
            Width           =   795
         End
         Begin VB.ComboBox cmb_tip_com 
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   360
            Left            =   1140
            Style           =   2  'Dropdown List
            TabIndex        =   1
            Top             =   570
            Width           =   1200
         End
         Begin VB.ComboBox cmb_ver_spde 
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   360
            ItemData        =   "set_par_com.frx":0044
            Left            =   1140
            List            =   "set_par_com.frx":0046
            Style           =   2  'Dropdown List
            TabIndex        =   0
            Top             =   210
            Width           =   1755
         End
         Begin VB.TextBox txt_ind_rem 
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
            Left            =   6990
            MaxLength       =   15
            TabIndex        =   11
            Text            =   "200.200.200.200"
            Top             =   1040
            Width           =   1830
         End
         Begin VB.TextBox txt_err_max 
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
            Left            =   1140
            MaxLength       =   1
            TabIndex        =   2
            Text            =   "3"
            Top             =   920
            Width           =   270
         End
         Begin VB.TextBox txt_por_rem 
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
            Left            =   6990
            MaxLength       =   6
            TabIndex        =   10
            Text            =   "200"
            Top             =   620
            Width           =   990
         End
         Begin VB.TextBox txt_por_loc 
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
            Left            =   6990
            MaxLength       =   6
            TabIndex        =   9
            Text            =   "100"
            Top             =   200
            Width           =   990
         End
         Begin VB.TextBox txt_porta_spde 
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
            Left            =   4050
            MaxLength       =   2
            TabIndex        =   4
            Text            =   "5"
            Top             =   200
            Width           =   375
         End
         Begin VB.Label Label15 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "ms"
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
            Height          =   210
            Left            =   1860
            TabIndex        =   89
            Top             =   1485
            Width           =   585
         End
         Begin VB.Label Label47 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Time-out"
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
            Height          =   255
            Left            =   135
            TabIndex        =   70
            Top             =   1420
            Width           =   945
         End
         Begin VB.Label Label48 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Errore max"
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
            Height          =   255
            Left            =   135
            TabIndex        =   69
            Top             =   1000
            Width           =   945
         End
         Begin VB.Label Label49 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Indirizzo remoto"
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
            Height          =   255
            Left            =   5715
            TabIndex        =   68
            Top             =   1125
            Width           =   1335
         End
         Begin VB.Label Label50 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Porta remota"
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
            Height          =   255
            Left            =   5715
            TabIndex        =   67
            Top             =   705
            Width           =   1275
         End
         Begin VB.Label Label51 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Porta locale"
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
            Height          =   255
            Left            =   5715
            TabIndex        =   66
            Top             =   285
            Width           =   1275
         End
         Begin VB.Label Label52 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Tipo"
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
            Height          =   255
            Left            =   135
            TabIndex        =   65
            Top             =   645
            Width           =   465
         End
         Begin VB.Label Label53 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Versione"
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
            Height          =   255
            Left            =   135
            TabIndex        =   64
            Top             =   285
            Width           =   885
         End
         Begin VB.Label Label54 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Stop"
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
            Height          =   255
            Left            =   3570
            TabIndex        =   63
            Top             =   1440
            Width           =   525
         End
         Begin VB.Label Label55 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Dati"
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
            Height          =   255
            Left            =   3090
            TabIndex        =   62
            Top             =   1440
            Width           =   555
         End
         Begin VB.Label Label56 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Parità"
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
            Height          =   255
            Left            =   3330
            TabIndex        =   61
            Top             =   1035
            Width           =   555
         End
         Begin VB.Label Label57 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Baud"
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
            Height          =   255
            Left            =   3330
            TabIndex        =   60
            Top             =   660
            Width           =   555
         End
         Begin VB.Label Label58 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Porta"
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
            Height          =   255
            Left            =   3330
            TabIndex        =   59
            Top             =   285
            Width           =   555
         End
      End
      Begin VB.Frame frm_com_plc 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         Caption         =   "PLC"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00800000&
         Height          =   2370
         Left            =   8940
         TabIndex        =   54
         Top             =   128
         Width           =   2910
         Begin VB.TextBox txt_timer_scans_plc 
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
            Left            =   2055
            MaxLength       =   2
            TabIndex        =   15
            Text            =   "10"
            Top             =   1890
            Width           =   510
         End
         Begin VB.TextBox txt_net_plc 
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
            Left            =   2055
            MaxLength       =   1
            TabIndex        =   12
            Text            =   "2"
            Top             =   450
            Width           =   285
         End
         Begin VB.TextBox txt_unit_plc 
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
            Left            =   2055
            MaxLength       =   2
            TabIndex        =   14
            Text            =   "10"
            Top             =   1410
            Width           =   420
         End
         Begin VB.TextBox txt_node_plc 
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
            Left            =   2055
            MaxLength       =   3
            TabIndex        =   13
            Text            =   "230"
            Top             =   930
            Width           =   555
         End
         Begin VB.Label Label14 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "ms"
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
            Height          =   210
            Left            =   2490
            TabIndex        =   88
            Top             =   1965
            Width           =   585
         End
         Begin VB.Label Label7 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Tempo scansione PLC"
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
            Height          =   300
            Left            =   60
            TabIndex        =   87
            Top             =   1965
            Width           =   1995
         End
         Begin VB.Label Label1 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Indirizzo FinsGateway"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   9
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000080FF&
            Height          =   300
            Left            =   420
            TabIndex        =   85
            Top             =   225
            Width           =   2415
         End
         Begin VB.Label Label3 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Unit ID"
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
            Height          =   300
            Left            =   60
            TabIndex        =   81
            Top             =   1485
            Width           =   1215
         End
         Begin VB.Label Label61 
            Alignment       =   1  'Right Justify
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            Caption         =   "Unit ID"
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
            Height          =   300
            Left            =   825
            TabIndex        =   57
            Top             =   4060
            Width           =   735
         End
         Begin VB.Label Label62 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Node Number"
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
            Height          =   300
            Left            =   60
            TabIndex        =   56
            Top             =   1005
            Width           =   1215
         End
         Begin VB.Label Label63 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Network"
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
            Height          =   300
            Left            =   60
            TabIndex        =   55
            Top             =   525
            Width           =   855
         End
      End
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "PARAMETRI COMUNICAZIONI"
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
      TabIndex        =   52
      Top             =   0
      Width           =   7785
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "set_par_com.frx":0048
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "set_par_com"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Dim eve_ind_plc As ind_plc
Attribute eve_ind_plc.VB_VarHelpID = -1



Sub ges_combo()
Dim i As Integer
'imposto le combobox con i valori che ho nell'INI
cmb_ver_spde.ListIndex = Trim(par_mac_ute.com_spde.ver_spde)
cmb_tip_com.ListIndex = Trim(par_mac_ute.com_spde.tip_com)
cmb_baud_spde.Text = Trim$(par_mac_ute.com_spde.tip_porta.baud)
cmb_bit_par_spde.Text = Trim$(par_mac_ute.com_spde.tip_porta.bit_par)
cmb_bit_stop_spde.Text = Trim$(par_mac_ute.com_spde.tip_porta.bit_stop)
cmb_dati_spde.Text = Trim$(par_mac_ute.com_spde.tip_porta.dati)
cmb_baud_azi.Text = IIf(val(Trim$(par_mac_ute.com_azi.baud)) = 0, "1000", Trim$(par_mac_ute.com_azi.baud))
For i = 1 To par_mac.car_tec.num_liv_tag
  cmb_baud_sch_tag(i - 1).Text = IIf(val(Trim$(par_mac_ute.com_sch_tag(i).baud)) = 0, "38400", Trim$(par_mac_ute.com_sch_tag(i).baud))
  cmb_bit_par_sch_tag(i - 1).Text = IIf(val(Trim$(par_mac_ute.com_sch_tag(i).bit_par)) = 0, "E", Trim$(par_mac_ute.com_sch_tag(i).bit_par))
  cmb_bit_stop_sch_tag(i - 1).Text = IIf(val(Trim$(par_mac_ute.com_sch_tag(i).bit_stop)) = 0, "1", Trim$(par_mac_ute.com_sch_tag(i).bit_stop))
  cmb_dati_sch_tag(i - 1).Text = IIf(val(Trim$(par_mac_ute.com_sch_tag(i).dati)) = 0, "8", Trim$(par_mac_ute.com_sch_tag(i).dati))
  'agrassi_FI08-03-2010_1
  cmb_tip_com_tag(i - 1).ListIndex = Trim(par_mac_ute.tip_com_sch_tag(i))
Next i
End Sub

Sub ini_combo()
Dim i As Integer

'ripulisco le combo box della form
cmb_ver_spde.Clear
cmb_tip_com.Clear
cmb_baud_spde.Clear
cmb_bit_par_spde.Clear
cmb_dati_spde.Clear
cmb_bit_stop_spde.Clear
For i = 1 To par_mac.car_tec.num_liv_tag
  cmb_baud_sch_tag(i - 1).Clear
  cmb_bit_par_sch_tag(i - 1).Clear
  cmb_dati_sch_tag(i - 1).Clear
  cmb_bit_stop_sch_tag(i - 1).Clear
  'agrassi_FI08-03-2010_1
  cmb_tip_com_tag(i - 1).Clear
Next i

'imposto le combo box della form
cmb_tip_com.AddItem "Seriale", 0
cmb_tip_com.AddItem "Ethernet", 1

cmb_ver_spde.AddItem "Nessuno", 0
cmb_ver_spde.AddItem "Fosber V1.0", 1
'cmb_ver_spde.AddItem "Fosber V1.1", 2
cmb_ver_spde.AddItem "Fosber V2.0", 2  '30112001 Stagetti
'cmb_ver_spde.AddItem "Escada V1.0", 4
'cmb_ver_spde.AddItem "Escada V1.1", 5
cmb_ver_spde.AddItem "Fosber V2.1", 3     'Iciliouff 20-11-2003
cmb_ver_spde.AddItem "Fosber V3.0", 4     'Robertouff 26-11-2007
cmb_ver_spde.AddItem "Simon V1.0", 5      'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
cmb_ver_spde.AddItem "MHI V1.0", 6        'PMASSAROFI_05/07/11 - Aggiunta ricezione ordini da MHI via BCD


cmb_baud_spde.AddItem "300", 0      'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
cmb_baud_spde.AddItem "1200", 1
cmb_baud_spde.AddItem "2400", 2
cmb_baud_spde.AddItem "4800", 3
cmb_baud_spde.AddItem "9600", 4
cmb_baud_spde.AddItem "14400", 5
cmb_baud_spde.AddItem "19200", 6
cmb_baud_spde.AddItem "28800", 7
cmb_baud_spde.AddItem "38400", 8
cmb_baud_spde.AddItem "57600", 9


cmb_bit_par_spde.AddItem "E", 0
cmb_bit_par_spde.AddItem "M", 1
cmb_bit_par_spde.AddItem "N", 2
cmb_bit_par_spde.AddItem "None", 3
cmb_bit_par_spde.AddItem "O", 4
cmb_bit_par_spde.AddItem "S", 5

cmb_bit_stop_spde.AddItem "1", 0
cmb_bit_stop_spde.AddItem "1.5", 1
cmb_bit_stop_spde.AddItem "2", 2

cmb_dati_spde.AddItem "4", 0
cmb_dati_spde.AddItem "5", 1
cmb_dati_spde.AddItem "6", 2
cmb_dati_spde.AddItem "7", 3
cmb_dati_spde.AddItem "8", 4


cmb_baud_azi.AddItem "5", 0
cmb_baud_azi.AddItem "10", 1
cmb_baud_azi.AddItem "20", 2
cmb_baud_azi.AddItem "50", 3
cmb_baud_azi.AddItem "100", 4
cmb_baud_azi.AddItem "125", 5
cmb_baud_azi.AddItem "250", 6
cmb_baud_azi.AddItem "500", 7
cmb_baud_azi.AddItem "1000", 8

For i = 1 To par_mac.car_tec.num_liv_tag
  cmb_baud_sch_tag(i - 1).AddItem "1200", 0
  cmb_baud_sch_tag(i - 1).AddItem "2400", 1
  cmb_baud_sch_tag(i - 1).AddItem "4800", 2
  cmb_baud_sch_tag(i - 1).AddItem "9600", 3
  cmb_baud_sch_tag(i - 1).AddItem "14400", 4
  cmb_baud_sch_tag(i - 1).AddItem "19200", 5
  cmb_baud_sch_tag(i - 1).AddItem "28800", 6
  cmb_baud_sch_tag(i - 1).AddItem "38400", 7
  cmb_baud_sch_tag(i - 1).AddItem "57600", 8
  
  cmb_bit_par_sch_tag(i - 1).AddItem "E", 0
  cmb_bit_par_sch_tag(i - 1).AddItem "M", 1
  cmb_bit_par_sch_tag(i - 1).AddItem "N", 2
  cmb_bit_par_sch_tag(i - 1).AddItem "None", 3
  cmb_bit_par_sch_tag(i - 1).AddItem "O", 4
  cmb_bit_par_sch_tag(i - 1).AddItem "S", 5
  
  cmb_bit_stop_sch_tag(i - 1).AddItem "1", 0
  cmb_bit_stop_sch_tag(i - 1).AddItem "1.5", 1
  cmb_bit_stop_sch_tag(i - 1).AddItem "2", 2
  
  cmb_dati_sch_tag(i - 1).AddItem "4", 0
  cmb_dati_sch_tag(i - 1).AddItem "5", 1
  cmb_dati_sch_tag(i - 1).AddItem "6", 2
  cmb_dati_sch_tag(i - 1).AddItem "7", 3
  cmb_dati_sch_tag(i - 1).AddItem "8", 4
  
  'agrassi_FI08-03-2010_1
  cmb_tip_com_tag(i - 1).AddItem "Seriale", 0
  cmb_tip_com_tag(i - 1).AddItem "Ethernet", 1
Next i

End Sub







Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
    If mod_in_cor Then
        mod_in_cor = False
        esc_pre = True
        par_mac_ute = par_mac
        fil_par_com
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
    If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Sub
    If Not mod_in_cor Then
        mod_in_cor = True
        esc_pre = False
        sal_in_cor = False
        enable_tab Me
        txt_idle.TabStop = False
        cmb_ver_spde.SetFocus
    End If
  Case Else
End Select
End Sub

Private Sub eve_key_F3(Shift As Integer)
'salva
Dim i As Integer
Dim tmp As tip_param_errati_su_com
Dim szbuff As String * 100

Select Case Shift
  Case 0
    If mod_in_cor Then
      messaggio msg("ope_in_cor"), False, False
      sal_in_cor = True
      esc_pre = False
      scr_par_mac_ute
      'Gestione cambio impostazioni comunicazione su azionamenti
      If par_mac_ute.com_azi.baud <> par_mac.com_azi.baud Then
        CAN_result = CAN_CloseAll
        If CAN_result <> CAN_ERR_OK Then
          CAN_result = CAN_GetErrText(CAN_result, szbuff)
          If CAN_result = CAN_ERR_NOVXD Then
            messaggio msg("pro_dis_can") & ": VxD (virtual device driver) not found", True, False
            log_str "set_par_com: EVE_KEY_F3.  CAN_CloseAll: VxD (virtual device driver) not found " + szbuff, MED_PRIORITY, log_can
          Else
            messaggio msg("pro_dis_can") & ": " & Rie_nul(szbuff, ""), True, False
            log_str "set_par_com: EVE_KEY_F3.  CAN_CloseAll: " + Rie_nul(szbuff, ""), MED_PRIORITY, log_can
          End If
          par_mac_ute = par_mac
          fil_par_com
          disable_tab Me
          txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
          txt_idle.TabStop = True
          txt_idle.SetFocus
          mod_in_cor = False
          Exit Sub
        End If
        ini_can Trim$(par_mac_ute.com_azi.baud), False
        par_mac.com_azi.baud = par_mac_ute.com_azi.baud
        par_mac.com_azi.TimeOut = par_mac_ute.com_azi.TimeOut
      End If
      'Gestione cambio impostazioni comunicazione su schede di taglio e SPDE
      If porte_chg Then
        If porte_ok Then
          tmp = set_porte
          If tmp <> NESSUN_PROBLEMA Then
            If tmp = PUNTO_UNICO Then
              par_mac_ute = par_mac
              ini_porte_SPDE
              ini_porte_SCHEDE_TAGLIO
              fil_par_com
              disable_tab Me
              txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
              txt_idle.TabStop = True
              txt_idle.SetFocus
              mod_in_cor = False
              Exit Sub
            Else
              par_mac.com_spde = par_mac_ute.com_spde
              par_mac.indirizzo_plc = par_mac_ute.indirizzo_plc
              par_mac.com_azi.TimeOut = par_mac_ute.com_azi.TimeOut
              For i = 1 To val(par_mac.car_tec.num_liv_tag)
                par_mac_ute.com_sch_tag(i) = par_mac.com_sch_tag(i)
              Next i
              ini_porte_SCHEDE_TAGLIO
              eve_ind_plc.change_ind_plc par_mac.indirizzo_plc.net_id, par_mac.indirizzo_plc.node_id, par_mac.indirizzo_plc.unit_id
              scr_par_mac
              fil_par_com
              disable_tab Me
              txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
              txt_idle.TabStop = True
              txt_idle.SetFocus
              mod_in_cor = False
              Exit Sub
            End If
          End If
        Else
          par_mac_ute = par_mac
          fil_par_com
          messaggio msg("pro_imp_por_ser"), True, False
          disable_tab Me
          txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
          txt_idle.TabStop = True
          txt_idle.SetFocus
          mod_in_cor = False
          Exit Sub
        End If
      End If
      par_mac = par_mac_ute
      eve_ind_plc.change_ind_plc par_mac.indirizzo_plc.net_id, par_mac.indirizzo_plc.node_id, par_mac.indirizzo_plc.unit_id
      scr_par_mac
      disable_tab Me
      txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
      txt_idle.TabStop = True
      txt_idle.SetFocus
      mod_in_cor = False
      If sta_tag.com_OK Then messaggio msg("ope_eff"), False, False
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
Dim i As Integer
Debug.Print "Inizio Load " & Me.name
If mod_in_cor Then mod_in_cor = False
If sal_in_cor Then sal_in_cor = False
If esc_pre Then esc_pre = False

For i = 1 To par_mac.car_tec.num_liv_tag
  txt_porta_sch_tag(i - 1).Enabled = True
  cmb_baud_sch_tag(i - 1).Enabled = True
  cmb_bit_par_sch_tag(i - 1).Enabled = True
  cmb_dati_sch_tag(i - 1).Enabled = True
  cmb_bit_stop_sch_tag(i - 1).Enabled = True
  txt_timeout_sch_tag(i - 1).Enabled = True
  'agrassi_FI08-03-2010_1
  txt_por_loc_tag(i - 1).Enabled = True
  txt_por_rem_tag(i - 1).Enabled = True
  txt_ind_rem_tag(i - 1).Enabled = True
  cmb_tip_com_tag(i - 1).Enabled = True
  chk_tip_com_tag_UDP(i - 1).Enabled = True               'MLAMIONI_PMASSARO_FI27/10/2010 - Gestione comunicazione UDP-TCP per schede di taglio
  txt_porta_sch_tag(i - 1).BackColor = &HFFFFFF                'bianco
  cmb_baud_sch_tag(i - 1).BackColor = &HFFFFFF                'bianco
  cmb_bit_par_sch_tag(i - 1).BackColor = &HFFFFFF             'bianco
  cmb_dati_sch_tag(i - 1).BackColor = &HFFFFFF                  'bianco
  cmb_bit_stop_sch_tag(i - 1).BackColor = &HFFFFFF           'bianco
  txt_timeout_sch_tag(i - 1).BackColor = &HFFFFFF           'bianco
  'agrassi_FI08-03-2010_1
  txt_por_loc_tag(i - 1).BackColor = &HFFFFFF
  txt_por_rem_tag(i - 1).BackColor = &HFFFFFF
  txt_ind_rem_tag(i - 1).BackColor = &HFFFFFF
  cmb_tip_com_tag(i - 1).BackColor = &HFFFFFF
  chk_tip_com_tag_UDP(i - 1).BackColor = &HFFFFFF          'MLAMIONI_PMASSARO_FI27/10/2010
  
  Label_liv(i - 1).Enabled = True
Next i
For i = par_mac.car_tec.num_liv_tag + 1 To 3
  txt_porta_sch_tag(i - 1).Enabled = False
  cmb_baud_sch_tag(i - 1).Enabled = False
  cmb_bit_par_sch_tag(i - 1).Enabled = False
  cmb_dati_sch_tag(i - 1).Enabled = False
  cmb_bit_stop_sch_tag(i - 1).Enabled = False
  txt_timeout_sch_tag(i - 1).Enabled = False
  'agrassi_FI08-03-2010_1
  txt_por_loc_tag(i - 1).Enabled = False
  txt_por_rem_tag(i - 1).Enabled = False
  txt_ind_rem_tag(i - 1).Enabled = False
  cmb_tip_com_tag(i - 1).Enabled = False
  chk_tip_com_tag_UDP(i - 1).Enabled = False              'MLAMIONI_PMASSARO_FI27/10/2010
  chk_tip_com_tag_UDP(i - 1).BackColor = LIGHT_GRAY
  txt_porta_sch_tag(i - 1).BackColor = LIGHT_GRAY
  cmb_baud_sch_tag(i - 1).BackColor = LIGHT_GRAY
  cmb_bit_par_sch_tag(i - 1).BackColor = LIGHT_GRAY
  cmb_dati_sch_tag(i - 1).BackColor = LIGHT_GRAY
  cmb_bit_stop_sch_tag(i - 1).BackColor = LIGHT_GRAY
  txt_timeout_sch_tag(i - 1).BackColor = LIGHT_GRAY
  'agrassi_FI08-03-2010_1
  txt_por_loc_tag(i - 1).BackColor = LIGHT_GRAY
  txt_por_rem_tag(i - 1).BackColor = LIGHT_GRAY
  txt_ind_rem_tag(i - 1).BackColor = LIGHT_GRAY
  cmb_tip_com_tag(i - 1).BackColor = LIGHT_GRAY
  
  Label_liv(i - 1).Enabled = False
  txt_porta_sch_tag(i - 1).Text = ""
  cmb_baud_sch_tag(i - 1).ListIndex = -1
  cmb_bit_par_sch_tag(i - 1).ListIndex = -1
  cmb_dati_sch_tag(i - 1).ListIndex = -1
  cmb_bit_stop_sch_tag(i - 1).ListIndex = -1
  txt_timeout_sch_tag(i - 1).Text = ""
  'agrassi_FI08-03-2010_1
  txt_por_loc_tag(i - 1).Text = ""
  txt_por_rem_tag(i - 1).Text = ""
  txt_ind_rem_tag(i - 1).Text = ""
  cmb_tip_com_tag(i - 1).ListIndex = -1
Next i

fil_par_com

txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
txt_idle.SetFocus
Debug.Print "Fine Load " & Me.name
Debug.Print ""
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
ini_combo
Set eve_key = New key
Set eve_ind_plc = plc_ind
eve_key.create Me
Carica Me
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub


Private Sub txt_ind_plc_gotfocus()
      'ABorelli_FI17/11/2006_1

ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_ind_rem_tag_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)

End Sub


Private Sub txt_node_plc_GotFocus()
      'ABorelli_FI17/11/2006_1

ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_por_loc_tag_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)

End Sub


Private Sub txt_por_plc_gotfocus()
      'ABorelli_FI17/11/2006_1

ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_por_rem_tag_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)

End Sub


Private Sub txt_porta_sch_tag_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_timeout_azi_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_timeout_lan_gotfocus()
      'ABorelli_FI17/11/2006_1

ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_timeout_sch_tag_GotFocus(Index As Integer)
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_timer_scans_plc_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_unit_plc_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_err_max_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_ind_rem_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_por_loc_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_por_rem_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_net_plc_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub


Private Sub txt_porta_spde_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub



Private Sub txt_timeout_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub fil_par_com()
'riempie i test_box della pagina di settaggio macchina con le variabili attuali
' settaggio macchina
Dim i As Integer
Dim c As Control
Dim m As Integer

'caricamento dei parametri nella form
txt_porta_spde.Text = Trim(par_mac_ute.com_spde.tip_porta.porta)
txt_por_loc.Text = Trim(par_mac_ute.com_spde.lan.por_loc)
txt_por_rem.Text = Trim(par_mac_ute.com_spde.lan.por_rem)
txt_ind_rem.Text = Trim(par_mac_ute.com_spde.lan.ind_rem)
txt_err_max.Text = Trim(par_mac_ute.com_spde.err_max)
txt_timeout.Text = Trim(par_mac_ute.com_spde.TimeOut)

'ABorelli_FI17/11/2006_1
txt_ind_plc = Trim(par_mac_ute.com_plc_lan.ind_rem)
txt_por_plc = Trim(par_mac_ute.com_plc_lan.por_rem)
txt_timeout_lan = Trim(par_mac_ute.com_plc_lan.TimeOut)
chk_abi_lan.Value = val(par_mac_ute.com_plc_lan.abi_lan)

txt_net_plc.Text = par_mac_ute.indirizzo_plc.net_id
txt_node_plc.Text = par_mac_ute.indirizzo_plc.node_id
txt_unit_plc.Text = par_mac_ute.indirizzo_plc.unit_id
txt_timer_scans_plc = par_mac_ute.indirizzo_plc.TIMER_SCANS_PLC

For i = 1 To par_mac.car_tec.num_liv_tag
    txt_porta_sch_tag(i - 1) = Trim(par_mac_ute.com_sch_tag(i).porta)
    txt_timeout_sch_tag(i - 1) = Trim(par_mac_ute.com_sch_tag(i).TimeOut)
    'agrassi_FI08-03-2010_1
    txt_por_loc_tag(i - 1) = Trim(par_mac_ute.lan_sch_tag(i).por_loc)
    txt_por_rem_tag(i - 1) = Trim(par_mac_ute.lan_sch_tag(i).por_rem)
    txt_ind_rem_tag(i - 1) = Trim(par_mac_ute.lan_sch_tag(i).ind_rem)
    'MLAMIONI_PMASSARO_FI27/10/2010 - Gestione comunicazione UDP-TCP per schede di taglio
    chk_tip_com_tag_UDP(i - 1).Value = IIf(par_mac_ute.lan_sch_tag_UDP(i) = True, 1, 0)
Next i

txt_timeout_azi.Text = Trim(par_mac_ute.com_azi.TimeOut)
' disabilito le text relative alle schede di taglio non presenti
If par_mac.car_tec.num_liv_tag < 3 Then
    For m = i To 3
        txt_porta_sch_tag(m - 1).Enabled = False
        txt_timeout_sch_tag(m - 1).Enabled = False
        'agrassi_FI08-03-2010_1
        txt_por_loc_tag(m - 1).Enabled = False
        txt_por_rem_tag(m - 1).Enabled = False
        txt_ind_rem_tag(m - 1).Enabled = False
        'MLAMIONI_PMASSARO_FI27/10/2010 - Gestione comunicazione UDP-TCP per schede di taglio
        chk_tip_com_tag_UDP(m - 1).Enabled = False

    Next m
End If

ges_combo
End Sub
Sub scr_par_mac_ute()
Dim i As Integer
'caricamento dei parametri dalla form nella struttura di appoggio

 par_mac_ute.com_spde.ver_spde = cmb_ver_spde.ListIndex
 par_mac_ute.com_spde.tip_com = cmb_tip_com.ListIndex
 par_mac_ute.com_spde.tip_porta.baud = cmb_baud_spde.Text
 par_mac_ute.com_spde.tip_porta.bit_par = cmb_bit_par_spde.Text
 par_mac_ute.com_spde.tip_porta.dati = cmb_dati_spde.Text
 par_mac_ute.com_spde.tip_porta.bit_stop = cmb_bit_stop_spde.Text
 par_mac_ute.com_spde.tip_porta.porta = txt_porta_spde.Text
 
 par_mac_ute.com_spde.lan.por_loc = txt_por_loc.Text
 par_mac_ute.com_spde.lan.por_rem = txt_por_rem.Text
 par_mac_ute.com_spde.lan.ind_rem = txt_ind_rem.Text
 par_mac_ute.com_spde.err_max = txt_err_max.Text
 par_mac_ute.com_spde.TimeOut = txt_timeout.Text

'ABorelli_FI17/11/2006_1
 par_mac_ute.com_plc_lan.ind_rem = txt_ind_plc
 par_mac_ute.com_plc_lan.por_rem = txt_por_plc
 par_mac_ute.com_plc_lan.TimeOut = txt_timeout_lan
 'par_mac_ute.com_plc_lan.abi_lan = Format$(chk_abi_lan.Value)

 par_mac_ute.indirizzo_plc.net_id = txt_net_plc.Text
 par_mac_ute.indirizzo_plc.node_id = txt_node_plc.Text
 par_mac_ute.indirizzo_plc.unit_id = txt_unit_plc.Text
 par_mac_ute.indirizzo_plc.TIMER_SCANS_PLC = txt_timer_scans_plc
 
 par_mac_ute.com_azi.TimeOut = txt_timeout_azi.Text
 par_mac_ute.com_azi.baud = cmb_baud_azi.Text
 
 For i = 1 To par_mac.car_tec.num_liv_tag
  par_mac_ute.com_sch_tag(i).porta = txt_porta_sch_tag(i - 1)
  par_mac_ute.com_sch_tag(i).TimeOut = txt_timeout_sch_tag(i - 1)
  par_mac_ute.com_sch_tag(i).baud = cmb_baud_sch_tag(i - 1).Text
  par_mac_ute.com_sch_tag(i).bit_par = cmb_bit_par_sch_tag(i - 1)
  par_mac_ute.com_sch_tag(i).bit_stop = cmb_bit_stop_sch_tag(i - 1)
  par_mac_ute.com_sch_tag(i).dati = cmb_dati_sch_tag(i - 1)
  'agrassi_FI08-03-2010_1
  par_mac_ute.tip_com_sch_tag(i) = cmb_tip_com_tag(i - 1).ListIndex
  par_mac_ute.lan_sch_tag(i).por_loc = txt_por_loc_tag(i - 1)
  par_mac_ute.lan_sch_tag(i).por_rem = txt_por_rem_tag(i - 1)
  par_mac_ute.lan_sch_tag(i).ind_rem = txt_ind_rem_tag(i - 1)
  
  'MLAMIONI_PMASSARO_FI27/10/2010 - Gestione comunicazione UDP-TCP per schede di taglio
  par_mac_ute.lan_sch_tag_UDP(i) = IIf(chk_tip_com_tag_UDP(i - 1).Value = 1, True, False)
  
 Next i
 
End Sub
