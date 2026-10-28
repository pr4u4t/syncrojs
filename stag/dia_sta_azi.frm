VERSION 5.00
Begin VB.Form dia_sta_azi 
   Appearance      =   0  'Flat
   BackColor       =   &H00A00000&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6300
   ClientLeft      =   45
   ClientTop       =   2190
   ClientWidth     =   11955
   ControlBox      =   0   'False
   ForeColor       =   &H00FFFFFF&
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   ScaleHeight     =   5971.084
   ScaleMode       =   0  'User
   ScaleWidth      =   11955
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox txt_idle 
      Height          =   285
      Left            =   4200
      TabIndex        =   127
      Text            =   "Text1"
      Top             =   -844
      Width           =   1020
   End
   Begin VB.Frame Frame3 
      BackColor       =   &H00C0C0C0&
      Caption         =   "AZIONAMENTI MOTORI RULLI DI USCITA"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000080&
      Height          =   1340
      Left            =   0
      TabIndex        =   91
      Top             =   496
      Width           =   11955
      Begin VB.Frame frm_azi_usc 
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         Caption         =   "Frame1"
         Height          =   1040
         Index           =   35
         Left            =   45
         TabIndex        =   92
         Top             =   240
         Width           =   11865
         Begin VB.Shape shp 
            BackColor       =   &H000000FF&
            BackStyle       =   1  'Opaque
            BorderColor     =   &H000000FF&
            FillColor       =   &H000000FF&
            Height          =   75
            Index           =   15
            Left            =   11760
            Shape           =   3  'Circle
            Top             =   840
            Width           =   75
         End
         Begin VB.Shape shp 
            BackColor       =   &H000000FF&
            BackStyle       =   1  'Opaque
            BorderColor     =   &H000000FF&
            FillColor       =   &H000000FF&
            Height          =   75
            Index           =   25
            Left            =   11760
            Shape           =   3  'Circle
            Top             =   480
            Width           =   75
         End
         Begin VB.Shape shp 
            BackColor       =   &H000000FF&
            BackStyle       =   1  'Opaque
            BorderColor     =   &H000000FF&
            FillColor       =   &H000000FF&
            Height          =   75
            Index           =   35
            Left            =   11760
            Shape           =   3  'Circle
            Top             =   120
            Width           =   75
         End
         Begin VB.Label lab_azi 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Mot. liv. 1"
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
            Height          =   285
            Index           =   15
            Left            =   15
            TabIndex        =   113
            Top             =   765
            Width           =   1005
         End
         Begin VB.Label lab_azi 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Mot. liv. 2"
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
            Height          =   285
            Index           =   25
            Left            =   15
            TabIndex        =   112
            Top             =   405
            Width           =   1005
         End
         Begin VB.Label lab_azi 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Mot. liv. 3"
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
            Height          =   285
            Index           =   35
            Left            =   15
            TabIndex        =   111
            Top             =   45
            Width           =   1005
         End
         Begin VB.Label lab_des_err 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   405
            Index           =   15
            Left            =   6285
            TabIndex        =   110
            Top             =   765
            Width           =   4065
         End
         Begin VB.Label lab_sta_azi 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   15
            Left            =   1830
            TabIndex        =   109
            Top             =   750
            Width           =   495
         End
         Begin VB.Label lab_sta_azi 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   25
            Left            =   1830
            TabIndex        =   108
            Top             =   390
            Width           =   495
         End
         Begin VB.Label lab_sta_azi 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   35
            Left            =   1830
            TabIndex        =   107
            Top             =   30
            Width           =   495
         End
         Begin VB.Label lab_cod_err 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   15
            Left            =   4080
            TabIndex        =   106
            Top             =   750
            Width           =   615
         End
         Begin VB.Label lab_cod_err 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   25
            Left            =   4080
            TabIndex        =   105
            Top             =   390
            Width           =   615
         End
         Begin VB.Label lab_cod_err 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   35
            Left            =   4080
            TabIndex        =   104
            Top             =   30
            Width           =   615
         End
         Begin VB.Label lab_dis 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   15
            Left            =   4890
            TabIndex        =   103
            Top             =   750
            Width           =   1335
         End
         Begin VB.Label lab_dis 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   25
            Left            =   4890
            TabIndex        =   102
            Top             =   390
            Width           =   1335
         End
         Begin VB.Label lab_dis 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   35
            Left            =   4890
            TabIndex        =   101
            Top             =   30
            Width           =   1335
         End
         Begin VB.Label lab_des_err 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   405
            Index           =   25
            Left            =   6285
            TabIndex        =   100
            Top             =   405
            Width           =   4065
         End
         Begin VB.Label lab_des_err 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   405
            Index           =   35
            Left            =   6285
            TabIndex        =   99
            Top             =   45
            Width           =   4065
         End
         Begin VB.Label lab_sta_des 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   15
            Left            =   2535
            TabIndex        =   98
            Top             =   750
            Width           =   1335
         End
         Begin VB.Label lab_sta_des 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   25
            Left            =   2535
            TabIndex        =   97
            Top             =   390
            Width           =   1335
         End
         Begin VB.Label lab_sta_des 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   35
            Left            =   2535
            TabIndex        =   96
            Top             =   30
            Width           =   1335
         End
         Begin VB.Label lab_par_nuo 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00008000&
            Height          =   270
            Index           =   15
            Left            =   10275
            TabIndex        =   95
            Top             =   750
            Width           =   1455
         End
         Begin VB.Label lab_par_nuo 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00008000&
            Height          =   270
            Index           =   25
            Left            =   10275
            TabIndex        =   94
            Top             =   390
            Width           =   1455
         End
         Begin VB.Label lab_par_nuo 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00008000&
            Height          =   270
            Index           =   35
            Left            =   10275
            TabIndex        =   93
            Top             =   30
            Width           =   1455
         End
      End
   End
   Begin VB.Frame Frame2 
      BackColor       =   &H00C0C0C0&
      Caption         =   "AZIONAMENTI MOTORI RULLI DI TAGLIO"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000080&
      Height          =   3540
      Left            =   0
      TabIndex        =   7
      Top             =   2803
      Width           =   11955
      Begin VB.Frame frm_azi_rul 
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         Caption         =   "Frame1"
         Height          =   1040
         Index           =   2
         Left            =   45
         TabIndex        =   52
         Top             =   240
         Width           =   11865
         Begin VB.CheckBox chk_abi 
            BackColor       =   &H00008000&
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00FFFFFF&
            Height          =   240
            Index           =   33
            Left            =   960
            Style           =   1  'Graphical
            TabIndex        =   120
            Top             =   35
            Width           =   825
         End
         Begin VB.CheckBox chk_abi 
            BackColor       =   &H00008000&
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00FFFFFF&
            Height          =   240
            Index           =   31
            Left            =   960
            Style           =   1  'Graphical
            TabIndex        =   119
            Top             =   755
            Width           =   825
         End
         Begin VB.CheckBox chk_abi 
            BackColor       =   &H00008000&
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00FFFFFF&
            Height          =   240
            Index           =   32
            Left            =   960
            Style           =   1  'Graphical
            TabIndex        =   118
            Top             =   395
            Width           =   825
         End
         Begin VB.Label lab_liv 
            BackStyle       =   0  'Transparent
            Caption         =   "3"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   21.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   495
            Index           =   2
            Left            =   645
            TabIndex        =   117
            Top             =   255
            Width           =   270
         End
         Begin VB.Shape shp 
            BackColor       =   &H000000FF&
            BackStyle       =   1  'Opaque
            BorderColor     =   &H000000FF&
            FillColor       =   &H000000FF&
            Height          =   75
            Index           =   31
            Left            =   11760
            Shape           =   3  'Circle
            Top             =   840
            Width           =   75
         End
         Begin VB.Shape shp 
            BackColor       =   &H000000FF&
            BackStyle       =   1  'Opaque
            BorderColor     =   &H000000FF&
            FillColor       =   &H000000FF&
            Height          =   75
            Index           =   32
            Left            =   11760
            Shape           =   3  'Circle
            Top             =   480
            Width           =   75
         End
         Begin VB.Shape shp 
            BackColor       =   &H000000FF&
            BackStyle       =   1  'Opaque
            BorderColor     =   &H000000FF&
            FillColor       =   &H000000FF&
            Height          =   75
            Index           =   33
            Left            =   11760
            Shape           =   3  'Circle
            Top             =   120
            Width           =   75
         End
         Begin VB.Label lab_azi 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Mot. 1"
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
            Height          =   285
            Index           =   31
            Left            =   15
            TabIndex        =   73
            Top             =   765
            Width           =   720
         End
         Begin VB.Label lab_azi 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Mot. 2"
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
            Height          =   285
            Index           =   32
            Left            =   15
            TabIndex        =   72
            Top             =   405
            Width           =   720
         End
         Begin VB.Label lab_azi 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Mot. 3"
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
            Height          =   285
            Index           =   33
            Left            =   15
            TabIndex        =   71
            Top             =   45
            Width           =   720
         End
         Begin VB.Label lab_des_err 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   405
            Index           =   31
            Left            =   6285
            TabIndex        =   70
            Top             =   765
            Width           =   4065
         End
         Begin VB.Label lab_sta_azi 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   31
            Left            =   1830
            TabIndex        =   69
            Top             =   750
            Width           =   495
         End
         Begin VB.Label lab_sta_azi 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   32
            Left            =   1830
            TabIndex        =   68
            Top             =   390
            Width           =   495
         End
         Begin VB.Label lab_sta_azi 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   33
            Left            =   1830
            TabIndex        =   67
            Top             =   30
            Width           =   495
         End
         Begin VB.Label lab_cod_err 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   31
            Left            =   4080
            TabIndex        =   66
            Top             =   750
            Width           =   615
         End
         Begin VB.Label lab_cod_err 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   32
            Left            =   4080
            TabIndex        =   65
            Top             =   390
            Width           =   615
         End
         Begin VB.Label lab_cod_err 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   33
            Left            =   4080
            TabIndex        =   64
            Top             =   30
            Width           =   615
         End
         Begin VB.Label lab_dis 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   31
            Left            =   4890
            TabIndex        =   63
            Top             =   750
            Width           =   1335
         End
         Begin VB.Label lab_dis 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   32
            Left            =   4890
            TabIndex        =   62
            Top             =   390
            Width           =   1335
         End
         Begin VB.Label lab_dis 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   33
            Left            =   4890
            TabIndex        =   61
            Top             =   30
            Width           =   1335
         End
         Begin VB.Label lab_des_err 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   405
            Index           =   32
            Left            =   6285
            TabIndex        =   60
            Top             =   405
            Width           =   4065
         End
         Begin VB.Label lab_des_err 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   405
            Index           =   33
            Left            =   6285
            TabIndex        =   59
            Top             =   45
            Width           =   4065
         End
         Begin VB.Label lab_sta_des 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   31
            Left            =   2535
            TabIndex        =   58
            Top             =   750
            Width           =   1335
         End
         Begin VB.Label lab_sta_des 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   32
            Left            =   2535
            TabIndex        =   57
            Top             =   390
            Width           =   1335
         End
         Begin VB.Label lab_sta_des 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   33
            Left            =   2535
            TabIndex        =   56
            Top             =   30
            Width           =   1335
         End
         Begin VB.Label lab_par_nuo 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00008000&
            Height          =   270
            Index           =   31
            Left            =   10275
            TabIndex        =   55
            Top             =   750
            Width           =   1455
         End
         Begin VB.Label lab_par_nuo 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00008000&
            Height          =   270
            Index           =   32
            Left            =   10275
            TabIndex        =   54
            Top             =   390
            Width           =   1455
         End
         Begin VB.Label lab_par_nuo 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00008000&
            Height          =   270
            Index           =   33
            Left            =   10275
            TabIndex        =   53
            Top             =   30
            Width           =   1455
         End
      End
      Begin VB.Frame frm_azi_rul 
         BackColor       =   &H00008000&
         BorderStyle     =   0  'None
         Caption         =   "Frame1"
         Height          =   1040
         Index           =   1
         Left            =   45
         TabIndex        =   30
         Top             =   1320
         Width           =   11865
         Begin VB.CheckBox chk_abi 
            BackColor       =   &H00008000&
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00FFFFFF&
            Height          =   240
            Index           =   23
            Left            =   960
            Style           =   1  'Graphical
            TabIndex        =   123
            Top             =   35
            Width           =   825
         End
         Begin VB.CheckBox chk_abi 
            BackColor       =   &H00008000&
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00FFFFFF&
            Height          =   240
            Index           =   21
            Left            =   960
            Style           =   1  'Graphical
            TabIndex        =   122
            Top             =   755
            Width           =   825
         End
         Begin VB.CheckBox chk_abi 
            BackColor       =   &H00008000&
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00FFFFFF&
            Height          =   240
            Index           =   22
            Left            =   960
            Style           =   1  'Graphical
            TabIndex        =   121
            Top             =   395
            Width           =   825
         End
         Begin VB.Label lab_liv 
            BackStyle       =   0  'Transparent
            Caption         =   "2"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   21.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   495
            Index           =   1
            Left            =   645
            TabIndex        =   116
            Top             =   255
            Width           =   315
         End
         Begin VB.Shape shp 
            BackColor       =   &H000000FF&
            BackStyle       =   1  'Opaque
            BorderColor     =   &H000000FF&
            FillColor       =   &H000000FF&
            Height          =   75
            Index           =   21
            Left            =   11760
            Shape           =   3  'Circle
            Top             =   840
            Width           =   75
         End
         Begin VB.Shape shp 
            BackColor       =   &H000000FF&
            BackStyle       =   1  'Opaque
            BorderColor     =   &H000000FF&
            FillColor       =   &H000000FF&
            Height          =   75
            Index           =   22
            Left            =   11760
            Shape           =   3  'Circle
            Top             =   480
            Width           =   75
         End
         Begin VB.Shape shp 
            BackColor       =   &H000000FF&
            BackStyle       =   1  'Opaque
            BorderColor     =   &H000000FF&
            FillColor       =   &H000000FF&
            Height          =   75
            Index           =   23
            Left            =   11760
            Shape           =   3  'Circle
            Top             =   120
            Width           =   75
         End
         Begin VB.Label lab_azi 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Mot. 1"
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
            Height          =   285
            Index           =   21
            Left            =   15
            TabIndex        =   51
            Top             =   765
            Width           =   705
         End
         Begin VB.Label lab_azi 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Mot. 2"
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
            Height          =   285
            Index           =   22
            Left            =   15
            TabIndex        =   50
            Top             =   405
            Width           =   705
         End
         Begin VB.Label lab_azi 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Mot. 3"
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
            Height          =   285
            Index           =   23
            Left            =   15
            TabIndex        =   49
            Top             =   45
            Width           =   705
         End
         Begin VB.Label lab_des_err 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   405
            Index           =   21
            Left            =   6285
            TabIndex        =   48
            Top             =   765
            Width           =   4065
         End
         Begin VB.Label lab_sta_azi 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   21
            Left            =   1830
            TabIndex        =   47
            Top             =   750
            Width           =   495
         End
         Begin VB.Label lab_sta_azi 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   22
            Left            =   1830
            TabIndex        =   46
            Top             =   390
            Width           =   495
         End
         Begin VB.Label lab_sta_azi 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   23
            Left            =   1830
            TabIndex        =   45
            Top             =   30
            Width           =   495
         End
         Begin VB.Label lab_cod_err 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   21
            Left            =   4080
            TabIndex        =   44
            Top             =   750
            Width           =   615
         End
         Begin VB.Label lab_cod_err 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   22
            Left            =   4080
            TabIndex        =   43
            Top             =   390
            Width           =   615
         End
         Begin VB.Label lab_cod_err 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   23
            Left            =   4080
            TabIndex        =   42
            Top             =   30
            Width           =   615
         End
         Begin VB.Label lab_dis 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   21
            Left            =   4890
            TabIndex        =   41
            Top             =   750
            Width           =   1335
         End
         Begin VB.Label lab_dis 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   22
            Left            =   4890
            TabIndex        =   40
            Top             =   390
            Width           =   1335
         End
         Begin VB.Label lab_dis 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   23
            Left            =   4890
            TabIndex        =   39
            Top             =   30
            Width           =   1335
         End
         Begin VB.Label lab_des_err 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   405
            Index           =   22
            Left            =   6285
            TabIndex        =   38
            Top             =   405
            Width           =   4065
         End
         Begin VB.Label lab_des_err 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   345
            Index           =   23
            Left            =   6285
            TabIndex        =   37
            Top             =   45
            Width           =   4065
         End
         Begin VB.Label lab_sta_des 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   21
            Left            =   2535
            TabIndex        =   36
            Top             =   750
            Width           =   1335
         End
         Begin VB.Label lab_sta_des 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   22
            Left            =   2535
            TabIndex        =   35
            Top             =   390
            Width           =   1335
         End
         Begin VB.Label lab_sta_des 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   23
            Left            =   2535
            TabIndex        =   34
            Top             =   30
            Width           =   1335
         End
         Begin VB.Label lab_par_nuo 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00008000&
            Height          =   270
            Index           =   21
            Left            =   10275
            TabIndex        =   33
            Top             =   750
            Width           =   1455
         End
         Begin VB.Label lab_par_nuo 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00008000&
            Height          =   270
            Index           =   22
            Left            =   10275
            TabIndex        =   32
            Top             =   390
            Width           =   1455
         End
         Begin VB.Label lab_par_nuo 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00008000&
            Height          =   270
            Index           =   23
            Left            =   10275
            TabIndex        =   31
            Top             =   30
            Width           =   1455
         End
      End
      Begin VB.Frame frm_azi_rul 
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         Caption         =   "Frame1"
         Height          =   1040
         Index           =   0
         Left            =   45
         TabIndex        =   8
         Top             =   2400
         Width           =   11865
         Begin VB.CheckBox chk_abi 
            BackColor       =   &H00008000&
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00FFFFFF&
            Height          =   240
            Index           =   13
            Left            =   960
            Style           =   1  'Graphical
            TabIndex        =   126
            Top             =   35
            Width           =   825
         End
         Begin VB.CheckBox chk_abi 
            BackColor       =   &H00008000&
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00FFFFFF&
            Height          =   240
            Index           =   11
            Left            =   960
            Style           =   1  'Graphical
            TabIndex        =   125
            Top             =   755
            Width           =   825
         End
         Begin VB.CheckBox chk_abi 
            BackColor       =   &H00008000&
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00FFFFFF&
            Height          =   240
            Index           =   12
            Left            =   960
            Style           =   1  'Graphical
            TabIndex        =   124
            Top             =   395
            Width           =   825
         End
         Begin VB.Label lab_liv 
            BackStyle       =   0  'Transparent
            Caption         =   "1"
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   21.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   480
            Index           =   0
            Left            =   645
            TabIndex        =   115
            Top             =   255
            Width           =   255
         End
         Begin VB.Shape shp 
            BackColor       =   &H000000FF&
            BackStyle       =   1  'Opaque
            BorderColor     =   &H000000FF&
            FillColor       =   &H000000FF&
            Height          =   75
            Index           =   11
            Left            =   11760
            Shape           =   3  'Circle
            Top             =   840
            Width           =   75
         End
         Begin VB.Shape shp 
            BackColor       =   &H000000FF&
            BackStyle       =   1  'Opaque
            BorderColor     =   &H000000FF&
            FillColor       =   &H000000FF&
            Height          =   75
            Index           =   12
            Left            =   11760
            Shape           =   3  'Circle
            Top             =   480
            Width           =   75
         End
         Begin VB.Shape shp 
            BackColor       =   &H000000FF&
            BackStyle       =   1  'Opaque
            BorderColor     =   &H000000FF&
            FillColor       =   &H000000FF&
            Height          =   75
            Index           =   13
            Left            =   11760
            Shape           =   3  'Circle
            Top             =   120
            Width           =   75
         End
         Begin VB.Label lab_azi 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Mot. 1"
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
            Height          =   285
            Index           =   11
            Left            =   15
            TabIndex        =   29
            Top             =   765
            Width           =   735
         End
         Begin VB.Label lab_azi 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Mot. 2"
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
            Height          =   285
            Index           =   12
            Left            =   15
            TabIndex        =   28
            Top             =   405
            Width           =   735
         End
         Begin VB.Label lab_azi 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Mot. 3"
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
            Height          =   285
            Index           =   13
            Left            =   15
            TabIndex        =   27
            Top             =   45
            Width           =   735
         End
         Begin VB.Label lab_des_err 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   405
            Index           =   11
            Left            =   6285
            TabIndex        =   26
            Top             =   765
            Width           =   4065
         End
         Begin VB.Label lab_sta_azi 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   11
            Left            =   1830
            TabIndex        =   25
            Top             =   750
            Width           =   495
         End
         Begin VB.Label lab_sta_azi 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   12
            Left            =   1830
            TabIndex        =   24
            Top             =   390
            Width           =   495
         End
         Begin VB.Label lab_sta_azi 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   13
            Left            =   1830
            TabIndex        =   23
            Top             =   30
            Width           =   495
         End
         Begin VB.Label lab_cod_err 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   11
            Left            =   4080
            TabIndex        =   22
            Top             =   750
            Width           =   615
         End
         Begin VB.Label lab_cod_err 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   12
            Left            =   4080
            TabIndex        =   21
            Top             =   390
            Width           =   615
         End
         Begin VB.Label lab_cod_err 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   13
            Left            =   4080
            TabIndex        =   20
            Top             =   30
            Width           =   615
         End
         Begin VB.Label lab_dis 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   11
            Left            =   4890
            TabIndex        =   19
            Top             =   750
            Width           =   1335
         End
         Begin VB.Label lab_dis 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   12
            Left            =   4890
            TabIndex        =   18
            Top             =   390
            Width           =   1335
         End
         Begin VB.Label lab_dis 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   13
            Left            =   4890
            TabIndex        =   17
            Top             =   30
            Width           =   1335
         End
         Begin VB.Label lab_des_err 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   405
            Index           =   12
            Left            =   6285
            TabIndex        =   16
            Top             =   405
            Width           =   4065
         End
         Begin VB.Label lab_des_err 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   405
            Index           =   13
            Left            =   6285
            TabIndex        =   15
            Top             =   45
            Width           =   4065
         End
         Begin VB.Label lab_sta_des 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   11
            Left            =   2535
            TabIndex        =   14
            Top             =   750
            Width           =   1335
         End
         Begin VB.Label lab_sta_des 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   12
            Left            =   2535
            TabIndex        =   13
            Top             =   390
            Width           =   1335
         End
         Begin VB.Label lab_sta_des 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   13
            Left            =   2535
            TabIndex        =   12
            Top             =   30
            Width           =   1335
         End
         Begin VB.Label lab_par_nuo 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00008000&
            Height          =   270
            Index           =   11
            Left            =   10275
            TabIndex        =   11
            Top             =   750
            Width           =   1455
         End
         Begin VB.Label lab_par_nuo 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00008000&
            Height          =   270
            Index           =   12
            Left            =   10275
            TabIndex        =   10
            Top             =   390
            Width           =   1455
         End
         Begin VB.Label lab_par_nuo 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00008000&
            Height          =   270
            Index           =   13
            Left            =   10275
            TabIndex        =   9
            Top             =   30
            Width           =   1455
         End
      End
   End
   Begin VB.Frame Frame1 
      BackColor       =   &H00C0C0C0&
      Caption         =   "AZIONAMENTI MOTORI RULLI DI INGRESSO"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000080&
      Height          =   977
      Left            =   0
      TabIndex        =   0
      Top             =   1826
      Width           =   11955
      Begin VB.Frame frm_azi_ing 
         BackColor       =   &H00008000&
         BorderStyle     =   0  'None
         Caption         =   "Frame1"
         Height          =   675
         Index           =   15
         Left            =   45
         TabIndex        =   74
         Top             =   240
         Width           =   11865
         Begin VB.Shape shp 
            BackColor       =   &H000000FF&
            BackStyle       =   1  'Opaque
            BorderColor     =   &H000000FF&
            FillColor       =   &H000000FF&
            Height          =   75
            Index           =   1
            Left            =   11760
            Shape           =   3  'Circle
            Top             =   480
            Width           =   75
         End
         Begin VB.Shape shp 
            BackColor       =   &H000000FF&
            BackStyle       =   1  'Opaque
            BorderColor     =   &H000000FF&
            FillColor       =   &H000000FF&
            Height          =   75
            Index           =   2
            Left            =   11760
            Shape           =   3  'Circle
            Top             =   120
            Width           =   75
         End
         Begin VB.Label lab_azi 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Mot. liv. 1 - 2"
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
            Height          =   285
            Index           =   1
            Left            =   15
            TabIndex        =   88
            Top             =   405
            Width           =   1380
         End
         Begin VB.Label lab_azi 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Mot. liv. 3"
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
            Height          =   285
            Index           =   2
            Left            =   15
            TabIndex        =   87
            Top             =   45
            Width           =   1005
         End
         Begin VB.Label lab_des_err 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   405
            Index           =   1
            Left            =   6285
            TabIndex        =   86
            Top             =   405
            Width           =   4065
         End
         Begin VB.Label lab_sta_azi 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   1
            Left            =   1830
            TabIndex        =   85
            Top             =   390
            Width           =   495
         End
         Begin VB.Label lab_sta_azi 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   2
            Left            =   1830
            TabIndex        =   84
            Top             =   30
            Width           =   495
         End
         Begin VB.Label lab_cod_err 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   1
            Left            =   4080
            TabIndex        =   83
            Top             =   390
            Width           =   615
         End
         Begin VB.Label lab_cod_err 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   2
            Left            =   4080
            TabIndex        =   82
            Top             =   30
            Width           =   615
         End
         Begin VB.Label lab_dis 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   1
            Left            =   4890
            TabIndex        =   81
            Top             =   390
            Width           =   1335
         End
         Begin VB.Label lab_dis 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   2
            Left            =   4890
            TabIndex        =   80
            Top             =   30
            Width           =   1335
         End
         Begin VB.Label lab_des_err 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000FF&
            Height          =   405
            Index           =   2
            Left            =   6285
            TabIndex        =   79
            Top             =   45
            Width           =   4065
         End
         Begin VB.Label lab_sta_des 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   1
            Left            =   2535
            TabIndex        =   78
            Top             =   390
            Width           =   1335
         End
         Begin VB.Label lab_sta_des 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   270
            Index           =   2
            Left            =   2535
            TabIndex        =   77
            Top             =   30
            Width           =   1335
         End
         Begin VB.Label lab_par_nuo 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00008000&
            Height          =   270
            Index           =   1
            Left            =   10275
            TabIndex        =   76
            Top             =   390
            Width           =   1455
         End
         Begin VB.Label lab_par_nuo 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00008000&
            Height          =   270
            Index           =   2
            Left            =   10275
            TabIndex        =   75
            Top             =   30
            Width           =   1455
         End
      End
   End
   Begin VB.Label lab_des_par 
      Alignment       =   1  'Right Justify
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H0000FFFF&
      Height          =   225
      Left            =   7500
      TabIndex        =   128
      Top             =   300
      Width           =   4425
   End
   Begin VB.Line ln_no_com2 
      BorderColor     =   &H00FFFFFF&
      BorderWidth     =   2
      X1              =   170
      X2              =   670
      Y1              =   402.811
      Y2              =   42.651
   End
   Begin VB.Line ln_no_com1 
      BorderColor     =   &H00FFFFFF&
      BorderWidth     =   2
      X1              =   170
      X2              =   670
      Y1              =   42.651
      Y2              =   402.811
   End
   Begin VB.Label lab_com 
      Alignment       =   2  'Center
      BackStyle       =   0  'Transparent
      Caption         =   "COM"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H0000FFFF&
      Height          =   255
      Left            =   40
      TabIndex        =   114
      Top             =   137
      Width           =   735
   End
   Begin VB.Shape shp_com2 
      BorderColor     =   &H00FF00FF&
      BorderWidth     =   3
      Height          =   375
      Left            =   150
      Shape           =   2  'Oval
      Top             =   63
      Width           =   510
   End
   Begin VB.Shape shp_com1 
      BackColor       =   &H00000000&
      BackStyle       =   1  'Opaque
      Height          =   380
      Left            =   155
      Shape           =   2  'Oval
      Top             =   75
      Width           =   510
   End
   Begin VB.Line lin_par_nuo 
      BorderColor     =   &H0000FFFF&
      BorderWidth     =   3
      Visible         =   0   'False
      X1              =   10350
      X2              =   11750
      Y1              =   199.036
      Y2              =   199.036
   End
   Begin VB.Line Line2 
      BorderColor     =   &H0000FF00&
      BorderWidth     =   3
      X1              =   4155
      X2              =   10255
      Y1              =   199.036
      Y2              =   199.036
   End
   Begin VB.Label Label3 
      BackStyle       =   0  'Transparent
      Caption         =   "Par. C 168 / 1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H0000FF00&
      Height          =   255
      Left            =   6525
      TabIndex        =   90
      Top             =   -30
      Width           =   1695
   End
   Begin VB.Line Line1 
      BorderColor     =   &H0000FF00&
      BorderWidth     =   3
      X1              =   1935
      X2              =   3875
      Y1              =   199.036
      Y2              =   199.036
   End
   Begin VB.Label Label2 
      BackStyle       =   0  'Transparent
      Caption         =   "Par. C 183 / 0"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H0000FF00&
      Height          =   255
      Left            =   2205
      TabIndex        =   89
      Top             =   -30
      Width           =   1695
   End
   Begin VB.Label lab_par_imp 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H0000FFFF&
      Height          =   285
      Left            =   10275
      TabIndex        =   6
      Top             =   0
      Width           =   1575
   End
   Begin VB.Label Label5 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Stato"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FFFFFF&
      Height          =   285
      Left            =   2565
      TabIndex        =   5
      Top             =   243
      Width           =   1335
   End
   Begin VB.Label Label6 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Display"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FFFFFF&
      Height          =   285
      Left            =   5100
      TabIndex        =   4
      Top             =   243
      Width           =   975
   End
   Begin VB.Label Label23 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Val. 183/0"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FFFFFF&
      Height          =   285
      Left            =   1530
      TabIndex        =   3
      Top             =   243
      Width           =   1140
   End
   Begin VB.Label Label1 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Val. 168/1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FFFFFF&
      Height          =   285
      Left            =   3855
      TabIndex        =   2
      Top             =   243
      Width           =   1095
   End
   Begin VB.Label Label4 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      Caption         =   "Descr. err."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FFFFFF&
      Height          =   195
      Left            =   6270
      TabIndex        =   1
      Top             =   300
      Width           =   1125
   End
End
Attribute VB_Name = "dia_sta_azi"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim pri_msg As Boolean 'serve per vedere se è la prima volta che visualizzo un messaggio di attesa
Dim dia_ret As Boolean
Dim imp_par As Boolean
Dim imp_par_des As Boolean
Dim com_avv As Boolean
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1

Sub col_azi(ind As Tip_ind_lenze, ret As cod_ret, tip_dat As tip_dat_com_azi)
Dim i As Integer
Dim m As Integer
Dim col_blk_183 As Boolean
Dim col_blk_168 As Boolean

Select Case tip_dat
  
  Case diagnostica
    If par_nuo.get_index > 0 And par_nuo.get_subindex >= 0 And par_nuo.par_pre Then
      lab_par_imp.Caption = "C " + CStr(par_nuo.get_index) + " / " + CStr(par_nuo.get_subindex)
      lin_par_nuo.Visible = True
    Else
      lab_par_imp.Caption = ""
      lin_par_nuo.Visible = False
      For i = 1 To 2
        lab_par_nuo(i).Caption = ""
      Next i
      For i = 15 To 35 Step 10
        lab_par_nuo(i).Caption = ""
      Next i
      For i = 10 To 30 Step 10
        For m = 1 To 3
            lab_par_nuo(i + m).Caption = ""
        Next m
      Next i
    End If
    col_blk_183 = False
    col_blk_168 = False
    If ret = RET_BAD Then
        shp(ind).BackColor = COL_ALL_Red
        shp(ind).BorderColor = COL_ALL_Red
        lab_sta_azi(ind).ForeColor = DARK_BLUE
        lab_sta_azi(ind).Caption = "Error"
        lab_sta_des(ind).ForeColor = DARK_BLUE
        lab_sta_des(ind).Caption = "Error"
        lab_cod_err(ind).ForeColor = DARK_BLUE
        lab_cod_err(ind).Caption = "Error"
        lab_dis(ind).ForeColor = DARK_BLUE
        lab_dis(ind).Caption = "Error"
        lab_des_err(ind).ForeColor = DARK_BLUE
        lab_des_err(ind).Caption = "Errore sulla comunicazione"
        If par_nuo.get_index > 0 And par_nuo.get_subindex >= 0 And par_nuo.par_pre Then
            lab_par_nuo(ind).ForeColor = DARK_BLUE
            lab_par_nuo(ind).Caption = "Error"
        End If
    Else
        shp(ind).BackColor = COL_ALL_Green
        shp(ind).BorderColor = COL_ALL_Green
        Select Case ind
          Case IND_SAN_ING, IND_SAN_ING2
            If Trim$(par_mac.par_azi_ing(ind).sta) = "0" Then col_blk_183 = True
            If Trim$(par_mac.par_azi_ing(ind).cod_err) = "0" Then col_blk_168 = True
          Case IND_SAN_USC_LIV_1, IND_SAN_USC_LIV_2, IND_SAN_USC_LIV_3
            i = val(Left$(ind, 1))
            If Trim$(par_mac.par_azi_usc(i).sta) = "0" Then col_blk_183 = True
            If Trim$(par_mac.par_azi_usc(i).cod_err) = "0" Then col_blk_168 = True
          Case IND_AZ_1_LIV_1, IND_AZ_2_LIV_1, IND_AZ_3_LIV_1, IND_AZ_1_LIV_2, IND_AZ_2_LIV_2, _
           IND_AZ_3_LIV_2, IND_AZ_1_LIV_3, IND_AZ_2_LIV_3, IND_AZ_3_LIV_3
            i = val(Left$(ind, 1))
            m = val(Right$(ind, 1))
            If Trim$(par_mac.par_azi_tag(i).mot(m).sta) = "0" Then col_blk_183 = True
            If Trim$(par_mac.par_azi_tag(i).mot(m).cod_err) = "0" Then col_blk_168 = True
        End Select
        lab_par_nuo(ind).ForeColor = vbBlack
        If col_blk_183 Then
              lab_sta_azi(ind).ForeColor = vbBlack
              lab_sta_des(ind).ForeColor = vbBlack
        Else
              lab_sta_azi(ind).ForeColor = COL_ALL_Red
              lab_sta_des(ind).ForeColor = COL_ALL_Red
        End If
        If col_blk_168 Then
              lab_cod_err(ind).ForeColor = vbBlack
              lab_dis(ind).ForeColor = vbBlack
              lab_des_err(ind).ForeColor = vbBlack
        Else
              lab_cod_err(ind).ForeColor = COL_ALL_Red
              lab_dis(ind).ForeColor = COL_ALL_Red
              lab_des_err(ind).ForeColor = COL_ALL_Red
        End If
        Select Case ind
          Case IND_SAN_ING, IND_SAN_ING2
            lab_sta_azi(ind).Caption = Trim$(par_mac.par_azi_ing(ind).sta)
            lab_sta_des(ind).Caption = cod_des("183/" & lab_sta_azi(ind).Caption)
            lab_cod_err(ind).Caption = Trim$(par_mac.par_azi_ing(ind).cod_err)
            lab_dis(ind).Caption = cod_dis("168/" & lab_cod_err(ind).Caption)
            lab_des_err(ind).Caption = cod_des("168/" & lab_cod_err(ind).Caption)
            If par_nuo.get_index > 0 And par_nuo.get_subindex >= 0 And par_nuo.par_pre Then
                lab_par_nuo(ind).Caption = Trim$(par_mac.par_azi_ing(ind).par_new)
            Else
                lab_par_nuo(ind).Caption = ""
            End If
          Case IND_SAN_USC_LIV_1, IND_SAN_USC_LIV_2, IND_SAN_USC_LIV_3
            lab_sta_azi(ind).Caption = Trim$(par_mac.par_azi_usc(i).sta)
            lab_sta_des(ind).Caption = cod_des("183/" & lab_sta_azi(ind).Caption)
            lab_cod_err(ind).Caption = Trim$(par_mac.par_azi_usc(i).cod_err)
            lab_dis(ind).Caption = cod_dis("168/" & lab_cod_err(ind).Caption)
            lab_des_err(ind).Caption = cod_des("168/" & lab_cod_err(ind).Caption)
            If par_nuo.get_index > 0 And par_nuo.get_subindex >= 0 And par_nuo.par_pre Then
                lab_par_nuo(ind).Caption = Trim$(par_mac.par_azi_usc(i).par_new)
            Else
                lab_par_nuo(ind).Caption = ""
            End If
          Case IND_AZ_1_LIV_1, IND_AZ_2_LIV_1, IND_AZ_3_LIV_1, IND_AZ_1_LIV_2, IND_AZ_2_LIV_2, _
           IND_AZ_3_LIV_2, IND_AZ_1_LIV_3, IND_AZ_2_LIV_3, IND_AZ_3_LIV_3
            lab_sta_azi(ind).Caption = Trim$(par_mac.par_azi_tag(i).mot(m).sta)
            lab_sta_des(ind).Caption = cod_des("183/" & lab_sta_azi(ind).Caption)
            lab_cod_err(ind).Caption = Trim$(par_mac.par_azi_tag(i).mot(m).cod_err)
            lab_dis(ind).Caption = cod_dis("168/" & lab_cod_err(ind).Caption)
            lab_des_err(ind).Caption = cod_des("168/" & lab_cod_err(ind).Caption)
            If par_nuo.get_index > 0 And par_nuo.get_subindex >= 0 And par_nuo.par_pre Then
                lab_par_nuo(ind).Caption = Trim$(par_mac.par_azi_tag(i).mot(m).par_new)
            Else
                lab_par_nuo(ind).Caption = ""
            End If
        End Select
    End If
  
  Case reset
    If ret = RET_BAD Then
        shp(ind).BackColor = COL_ALL_Red
        shp(ind).BorderColor = COL_ALL_Red
        lab_des_err(ind).ForeColor = DARK_BLUE
        lab_des_err(ind).Caption = "Errore sulla comunicazione"
    Else
        shp(ind).BackColor = COL_ALL_Green
        shp(ind).BorderColor = COL_ALL_Green
        lab_des_err(ind).ForeColor = vbBlack
        lab_des_err(ind).Caption = "Reset effettuato"
    End If

End Select
End Sub


Sub del_lab()
Dim i As Integer
Dim m As Integer

For i = 1 To 2
  lab_sta_azi(i).Caption = ""
  lab_sta_des(i).Caption = ""
  lab_cod_err(i).Caption = ""
  lab_dis(i).Caption = ""
  lab_des_err(i).Caption = ""
  lab_par_nuo(i).Caption = ""
Next i
For i = 15 To 35 Step 10
  lab_sta_azi(i).Caption = ""
  lab_sta_des(i).Caption = ""
  lab_cod_err(i).Caption = ""
  lab_dis(i).Caption = ""
  lab_des_err(i).Caption = ""
  lab_par_nuo(i).Caption = ""
Next i
For m = 10 To 30 Step 10
  For i = 1 To 3
      lab_sta_azi(m + i).Caption = ""
      lab_sta_des(m + i).Caption = ""
      lab_cod_err(m + i).Caption = ""
      lab_dis(m + i).Caption = ""
      lab_des_err(m + i).Caption = ""
      lab_par_nuo(m + i).Caption = ""
  Next i
Next m

End Sub

Sub rcv_par_dia_azi()
Dim i As Integer
Dim m As Integer
Dim ret As cod_ret
Static col As Boolean

If dia_ret Or imp_par Or imp_par_des Then
  dia_ret = False
  imp_par = False
  imp_par_des = False
Else
  'del_lab
End If
lab_com.Visible = Not lab_com.Visible
shp_com1.Visible = Not shp_com1.Visible
shp_com2.Visible = Not shp_com2.Visible
ret = snd_par_rul_ing(IND_SAN_ING, diagnostica)
col_azi IND_SAN_ING, ret, diagnostica
If par_mac.car_tec.num_liv_tag = 3 Then
    ret = snd_par_rul_ing(IND_SAN_ING2, diagnostica)
    col_azi IND_SAN_ING2, ret, diagnostica
End If
For i = 1 To par_mac.car_tec.num_liv_tag
    ret = snd_par_rul_usc(i, diagnostica)
    col_azi IND_SAN_USC_LIV_1 + (i - 1) * 10, ret, diagnostica
Next i
For i = 1 To par_mac.car_tec.num_liv_tag
  For m = 1 To par_mac.car_tec.num_mot_liv_tag
    If par_mac.car_tec.tip_azi_tag = 0 Then     ' ABorelli_FI11/11/2005_1
        ret = snd_par_rul_tag(i, m, diagnostica)
        col_azi IND_AZ_1_LIV_1 + (i - 1) * 10 + (m - 1), ret, diagnostica
    Else
        dis_azi IND_AZ_1_LIV_1 + (i - 1) * 10 + (m - 1)
    End If
  Next m
Next i
If pri_msg Then
  lab_com.Visible = True
  shp_com1.Visible = True
  shp_com2.Visible = True
  ln_no_com1.Visible = False
  ln_no_com2.Visible = False
  messaggio msg("ope_eff"), False, False
  pri_msg = False
End If
End Sub

Sub ini_lab()
Dim i As Integer
Dim m As Integer
Dim str As String

del_lab
For i = 1 To par_mac.car_tec.num_liv_tag
    lab_liv(i - 1).ForeColor = vbBlack
Next i
For i = par_mac.car_tec.num_liv_tag + 1 To 3
    lab_liv(i - 1).ForeColor = MEDIUM_GRAY
Next i
'Azionamenti rulli di ingresso
lab_azi(1).ForeColor = vbBlack
shp(1).Visible = True
shp(1).BackColor = COL_ALL_Red
shp(1).BorderColor = COL_ALL_Red
If par_mac.car_tec.num_liv_tag = 3 Then
    lab_azi(2).ForeColor = vbBlack
    shp(2).Visible = True
    shp(2).BackColor = COL_ALL_Red
    shp(2).BorderColor = COL_ALL_Red
Else
    lab_azi(2).ForeColor = MEDIUM_GRAY
    shp(2).Visible = False
End If
'Azionamenti rulli di uscita
For i = 1 To par_mac.car_tec.num_liv_tag
  str = CStr(i) + "5"
  lab_azi(val(str)).ForeColor = vbBlack
  shp(val(str)).Visible = True
  shp(val(str)).BackColor = COL_ALL_Red
  shp(val(str)).BorderColor = COL_ALL_Red
Next i
For i = par_mac.car_tec.num_liv_tag + 1 To 3
  str = CStr(i) + "5"
  lab_azi(val(str)).ForeColor = MEDIUM_GRAY
  shp(val(str)).Visible = False
Next i
'Azionamenti rulli di taglio
For i = 1 To par_mac.car_tec.num_liv_tag
  For m = 1 To par_mac.car_tec.num_mot_liv_tag
    str = CStr(i) + CStr(m)
    lab_azi(val(str)).ForeColor = vbBlack
    shp(val(str)).Visible = True
    shp(val(str)).BackColor = COL_ALL_Red
    shp(val(str)).BorderColor = COL_ALL_Red
    If par_abi.fun_mot_liv(i).fun_mot_mot(m) Then
        chk_abi(val(str)).Value = 1
        chk_abi(val(str)).Caption = msg("mot_abi")
        chk_abi(val(str)).ForeColor = vbBlack
        chk_abi(val(str)).BackColor = LIGHT_Green
    Else
        chk_abi(val(str)).Value = 0
        chk_abi(val(str)).Caption = msg("mot_non_abi")
        chk_abi(val(str)).ForeColor = vbWhite
        chk_abi(val(str)).BackColor = COL_DARK_Red
    End If
  Next m
Next i
For i = 1 To par_mac.car_tec.num_liv_tag
  For m = par_mac.car_tec.num_mot_liv_tag + 1 To 3
    str = CStr(i) + CStr(m)
    lab_azi(val(str)).ForeColor = MEDIUM_GRAY
    shp(val(str)).Visible = False
    chk_abi(val(str)).Caption = ""
    chk_abi(val(str)).Value = 0
    chk_abi(val(str)).BackColor = MEDIUM_GRAY
  Next m
Next i
For i = par_mac.car_tec.num_liv_tag + 1 To 3
  For m = 1 To 3
    str = CStr(i) + CStr(m)
    lab_azi(val(str)).ForeColor = MEDIUM_GRAY
    shp(val(str)).Visible = False
    chk_abi(val(str)).Caption = ""
    chk_abi(val(str)).Value = 0
    chk_abi(val(str)).BackColor = MEDIUM_GRAY
  Next m
Next i

End Sub

Sub dis_azi(num As Integer)
' ABorelli_FI11/11/2005_1
    lab_sta_azi(num).Enabled = False
    lab_sta_des(num).Enabled = False
    lab_cod_err(num).Enabled = False
    lab_dis(num).Enabled = False
    lab_des_err(num).Enabled = False
    lab_par_nuo(num).Enabled = False
    frm_azi_rul(0).Enabled = False
    frm_azi_rul(1).Enabled = False
    frm_azi_rul(2).Enabled = False
    
End Sub

Sub reset_azi()
Dim i As Integer
Dim m As Integer
Dim ret As cod_ret

del_lab
ret = snd_par_rul_ing(IND_SAN_ING, reset)
col_azi IND_SAN_ING, ret, reset

If par_mac.car_tec.num_liv_tag = 3 Then
    ret = snd_par_rul_ing(IND_SAN_ING2, reset)
    col_azi IND_SAN_ING2, ret, reset
End If

For i = 1 To par_mac.car_tec.num_liv_tag
    ret = snd_par_rul_usc(i, reset)
    col_azi i * 10 + 5, ret, reset
Next i

For i = 1 To par_mac.car_tec.num_liv_tag
  For m = 1 To par_mac.car_tec.num_mot_liv_tag
    If par_mac.car_tec.tip_azi_tag = 0 Then     ' ABorelli_FI11/11/2005_1
        ret = snd_par_rul_tag(i, m, reset)
        col_azi i * 10 + m, ret, reset
    Else
        dis_azi i * 10 + m
    End If
  Next m
Next i

End Sub

Private Sub eve_key_F1(Shift As Integer)
Select Case Shift
  Case 0
    dia_ret = False
    imp_par = False
    imp_par_des = False
    com_avv = False
    eve_key.chg pag_all
  Case Else
End Select
End Sub

Private Sub eve_key_F10(Shift As Integer)
Select Case Shift
  Case 0
    com_avv = False
    dia_ret = False
    imp_par = False
    imp_par_des = False
    eve_key.chg pag_pro
  Case Else
End Select
End Sub


Private Sub eve_key_F3(Shift As Integer)
Select Case Shift
  Case 0
    ' avvio della comunicazione
    
    If sta_bar.tim_com_azi.Enabled Then
      messaggio msg("com_abi"), False, False
    Else
      messaggio msg("att"), False, True
      pri_msg = True
      com_avv = True
      sta_bar.tim_com_azi.Enabled = True
    End If
  Case Else
End Select
End Sub


Private Sub eve_key_F4(Shift As Integer)
' interruzione della comunicazione
Dim i As Integer
Dim m As Integer

Select Case Shift
  Case 0
    If sta_bar.tim_com_azi.Enabled Then
      sta_bar.tim_com_azi.Enabled = False
      lab_com.Visible = True
      shp_com1.Visible = True
      shp_com2.Visible = True
      ln_no_com1.Visible = True
      ln_no_com2.Visible = True
      com_avv = False
      messaggio msg("ope_eff"), False, False
      For i = 1 To 2
        shp(i).BackColor = COL_ALL_Red
        shp(i).BorderColor = COL_ALL_Red
      Next i
      For i = 15 To 35 Step 10
        shp(i).BackColor = COL_ALL_Red
        shp(i).BorderColor = COL_ALL_Red
      Next i
      For i = 10 To 30 Step 10
        For m = 1 To 3
          shp(i + m).BackColor = COL_ALL_Red
          shp(i + m).BorderColor = COL_ALL_Red
        Next m
      Next i
    Else
      messaggio msg("com_int"), False, False
    End If
  Case Else
End Select
End Sub

Private Sub eve_key_F6(Shift As Integer)
Dim szbuff As String * 100

Select Case Shift
  Case 0
    'reset degli azionamenti
    CAN_result = CAN_ResetHardware(HAR_CAN_DONGLE)
    If CAN_result <> 0 Then
        CAN_result = CAN_GetErrText(CAN_result, szbuff)
        log_str "snd_par_azi. Form: set_par_rul. CAN_ResetHardware: " & szbuff, MED_PRIORITY, log_can
        messaggio msg("can_res_err"), True, False
    Else
        messaggio msg("ope_res_in_cor"), False, False
        reset_azi
        messaggio msg("ope_eff"), False, False
    End If
  Case Else
End Select
End Sub




Private Sub eve_key_F7(Shift As Integer)

Select Case Shift
  Case 0 ' impostazione parametro
    imp_par = True
    eve_key.chg dia_par_new, keep:=1
  Case 2
  Case Else
End Select
End Sub


Private Sub eve_key_F8(Shift As Integer)

Select Case Shift
  Case 0 ' impostazione parametro per descr
    imp_par_des = True
    eve_key.chg dia_par_new_des, keep:=1
  Case 2 ' salvataggio descrizione
    If Not pro_set.ute_wrk Then messaggio msg("acc_pwd"), True: Exit Sub
    eve_key.chg dia_par_des, keep:=1
  Case Else
End Select
End Sub


Private Sub eve_key_F9(Shift As Integer)
Select Case Shift
  Case 0
    ' diagnosi rete CAN
    dia_ret = True
    eve_key.chg dia_ret_azi, keep:=1
  Case Else
End Select
End Sub


Private Sub Form_Activate()
Dim m As Integer
Dim i As Integer

If Not dia_ret And Not imp_par Then ini_lab
If par_nuo.get_index > 0 And par_nuo.get_subindex >= 0 Or imp_nuo_par Then
  lab_par_imp.Caption = "C " + CStr(par_nuo.get_index) + " / " + CStr(par_nuo.get_subindex)
  lin_par_nuo.Visible = True
Else
  lab_par_imp.Caption = ""
  lin_par_nuo.Visible = False
  If imp_par Then messaggio msg("par_non_pre"), True, False
End If
lab_des_par.Caption = Trim$(par_nuo.get_descr)
lab_com.Visible = True
shp_com1.Visible = True
shp_com2.Visible = True
If (dia_ret Or imp_par Or imp_par_des) And com_avv Then
    sta_bar.tim_com_azi.Enabled = True
    ln_no_com1.Visible = False
    ln_no_com2.Visible = False
Else
    ln_no_com1.Visible = True
    ln_no_com2.Visible = True
End If
disable_tab Me
txt_idle.SetFocus
End Sub

Private Sub Form_Deactivate()

sta_bar.tim_com_azi.Enabled = False

End Sub


Private Sub Form_Load()
Debug.Print "Inizio Load " & Me.name
Set eve_key = New key
eve_key.create Me
Carica Me
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub


Private Sub Form_Unload(Cancel As Integer)
sta_bar.tim_com_azi.Enabled = False
End Sub



