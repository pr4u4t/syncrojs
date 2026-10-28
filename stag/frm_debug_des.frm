VERSION 5.00
Begin VB.Form frm_debug_des 
   Appearance      =   0  'Flat
   BackColor       =   &H80000005&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6300
   ClientLeft      =   -555
   ClientTop       =   3390
   ClientWidth     =   11955
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6300
   ScaleWidth      =   11955
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox txt_idle 
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
      Left            =   0
      TabIndex        =   102
      Top             =   -4000
      Width           =   1425
   End
   Begin VB.Timer tim_debug 
      Enabled         =   0   'False
      Interval        =   300
      Left            =   8910
      Top             =   6180
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   49
      Left            =   10530
      TabIndex        =   99
      Top             =   5415
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   41
      Left            =   10530
      TabIndex        =   88
      Top             =   2535
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   42
      Left            =   10530
      TabIndex        =   87
      Top             =   2895
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   43
      Left            =   10530
      TabIndex        =   86
      Top             =   3255
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   44
      Left            =   10530
      TabIndex        =   85
      Top             =   3615
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   45
      Left            =   10530
      TabIndex        =   84
      Top             =   3975
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   46
      Left            =   10530
      TabIndex        =   83
      Top             =   4335
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   47
      Left            =   10530
      TabIndex        =   82
      Top             =   4695
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   48
      Left            =   10530
      TabIndex        =   81
      Top             =   5055
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   40
      Left            =   10530
      TabIndex        =   80
      Top             =   2175
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   39
      Left            =   10530
      TabIndex        =   76
      Top             =   1815
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   38
      Left            =   10530
      TabIndex        =   75
      Top             =   1455
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   37
      Left            =   10530
      TabIndex        =   74
      Top             =   1095
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   21
      Left            =   6555
      TabIndex        =   56
      Top             =   1455
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   22
      Left            =   6555
      TabIndex        =   55
      Top             =   1815
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   23
      Left            =   6555
      TabIndex        =   54
      Top             =   2175
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   24
      Left            =   6555
      TabIndex        =   53
      Top             =   2535
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   25
      Left            =   6555
      TabIndex        =   52
      Top             =   2895
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   26
      Left            =   6555
      TabIndex        =   51
      Top             =   3255
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   27
      Left            =   6555
      TabIndex        =   50
      Top             =   3615
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   28
      Left            =   6555
      TabIndex        =   49
      Top             =   3975
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   29
      Left            =   6555
      TabIndex        =   48
      Top             =   4335
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   30
      Left            =   6555
      TabIndex        =   47
      Top             =   4695
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   31
      Left            =   6555
      TabIndex        =   46
      Top             =   5055
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   32
      Left            =   6555
      TabIndex        =   45
      Top             =   5415
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   33
      Left            =   6555
      TabIndex        =   44
      Top             =   5775
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   34
      Left            =   10530
      TabIndex        =   43
      Top             =   15
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   35
      Left            =   10530
      TabIndex        =   42
      Top             =   375
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   36
      Left            =   10530
      TabIndex        =   41
      Top             =   735
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   20
      Left            =   6555
      TabIndex        =   40
      Top             =   1095
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   18
      Left            =   6555
      TabIndex        =   36
      Top             =   375
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   19
      Left            =   6555
      TabIndex        =   35
      Top             =   735
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   17
      Left            =   6555
      TabIndex        =   34
      Top             =   15
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   1
      Left            =   2580
      TabIndex        =   16
      Top             =   375
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   2
      Left            =   2580
      TabIndex        =   15
      Top             =   735
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   3
      Left            =   2580
      TabIndex        =   14
      Top             =   1095
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   4
      Left            =   2580
      TabIndex        =   13
      Top             =   1455
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   5
      Left            =   2580
      TabIndex        =   12
      Top             =   1815
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   6
      Left            =   2580
      TabIndex        =   11
      Top             =   2175
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   7
      Left            =   2580
      TabIndex        =   10
      Top             =   2535
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   8
      Left            =   2580
      TabIndex        =   9
      Top             =   2895
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   9
      Left            =   2580
      TabIndex        =   8
      Top             =   3255
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   10
      Left            =   2580
      TabIndex        =   7
      Top             =   3615
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   11
      Left            =   2580
      TabIndex        =   6
      Top             =   3975
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   12
      Left            =   2580
      TabIndex        =   5
      Top             =   4335
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   13
      Left            =   2580
      TabIndex        =   4
      Top             =   4695
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   14
      Left            =   2580
      TabIndex        =   3
      Top             =   5055
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   15
      Left            =   2580
      TabIndex        =   2
      Top             =   5415
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   16
      Left            =   2580
      TabIndex        =   1
      Top             =   5775
      Width           =   1455
   End
   Begin VB.TextBox txt_dm 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   6.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Index           =   0
      Left            =   2580
      TabIndex        =   0
      Top             =   15
      Width           =   1455
   End
   Begin VB.Label Label2 
      BackStyle       =   0  'Transparent
      Caption         =   "Data Memory"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   195
      Left            =   9345
      TabIndex        =   104
      Top             =   5760
      Width           =   1230
   End
   Begin VB.Label lab_int_DM 
      BackColor       =   &H00C0E0FF&
      BorderStyle     =   1  'Fixed Single
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000080&
      Height          =   240
      Left            =   10530
      TabIndex        =   103
      Top             =   5745
      Width           =   1260
   End
   Begin VB.Label Label1 
      BackStyle       =   0  'Transparent
      Caption         =   "Dati in"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   195
      Left            =   9915
      TabIndex        =   101
      Top             =   6015
      Width           =   630
   End
   Begin VB.Label lab_vis 
      BackColor       =   &H00C0FFFF&
      BorderStyle     =   1  'Fixed Single
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00800000&
      Height          =   240
      Left            =   10530
      TabIndex        =   100
      Top             =   5985
      Width           =   1260
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   40
      Left            =   8010
      TabIndex        =   98
      Top             =   2160
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   41
      Left            =   8010
      TabIndex        =   97
      Top             =   2520
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   42
      Left            =   8010
      TabIndex        =   96
      Top             =   2880
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   43
      Left            =   8010
      TabIndex        =   95
      Top             =   3240
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   44
      Left            =   8010
      TabIndex        =   94
      Top             =   3600
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   45
      Left            =   8010
      TabIndex        =   93
      Top             =   3960
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   46
      Left            =   8010
      TabIndex        =   92
      Top             =   4320
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   47
      Left            =   8010
      TabIndex        =   91
      Top             =   4680
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   48
      Left            =   8010
      TabIndex        =   90
      Top             =   5040
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   49
      Left            =   8010
      TabIndex        =   89
      Top             =   5400
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   37
      Left            =   8010
      TabIndex        =   79
      Top             =   1080
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   38
      Left            =   8010
      TabIndex        =   78
      Top             =   1440
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   39
      Left            =   8010
      TabIndex        =   77
      Top             =   1800
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   20
      Left            =   4035
      TabIndex        =   73
      Top             =   1080
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   21
      Left            =   4035
      TabIndex        =   72
      Top             =   1440
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   22
      Left            =   4035
      TabIndex        =   71
      Top             =   1800
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   23
      Left            =   4035
      TabIndex        =   70
      Top             =   2160
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   24
      Left            =   4035
      TabIndex        =   69
      Top             =   2520
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   25
      Left            =   4035
      TabIndex        =   68
      Top             =   2880
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   26
      Left            =   4035
      TabIndex        =   67
      Top             =   3240
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   27
      Left            =   4035
      TabIndex        =   66
      Top             =   3600
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   28
      Left            =   4035
      TabIndex        =   65
      Top             =   3960
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   29
      Left            =   4035
      TabIndex        =   64
      Top             =   4320
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   30
      Left            =   4035
      TabIndex        =   63
      Top             =   4680
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   31
      Left            =   4035
      TabIndex        =   62
      Top             =   5040
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   32
      Left            =   4035
      TabIndex        =   61
      Top             =   5400
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   33
      Left            =   4035
      TabIndex        =   60
      Top             =   5760
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   34
      Left            =   8010
      TabIndex        =   59
      Top             =   0
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   35
      Left            =   8010
      TabIndex        =   58
      Top             =   360
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   36
      Left            =   8010
      TabIndex        =   57
      Top             =   720
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   17
      Left            =   4035
      TabIndex        =   39
      Top             =   0
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   18
      Left            =   4035
      TabIndex        =   38
      Top             =   360
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   19
      Left            =   4035
      TabIndex        =   37
      Top             =   720
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   0
      Left            =   60
      TabIndex        =   33
      Top             =   0
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   1
      Left            =   60
      TabIndex        =   32
      Top             =   360
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   2
      Left            =   60
      TabIndex        =   31
      Top             =   720
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   3
      Left            =   60
      TabIndex        =   30
      Top             =   1080
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   4
      Left            =   60
      TabIndex        =   29
      Top             =   1440
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   5
      Left            =   60
      TabIndex        =   28
      Top             =   1800
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   6
      Left            =   60
      TabIndex        =   27
      Top             =   2160
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   7
      Left            =   60
      TabIndex        =   26
      Top             =   2520
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   8
      Left            =   60
      TabIndex        =   25
      Top             =   2880
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   9
      Left            =   60
      TabIndex        =   24
      Top             =   3240
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   10
      Left            =   60
      TabIndex        =   23
      Top             =   3600
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   11
      Left            =   60
      TabIndex        =   22
      Top             =   3960
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   12
      Left            =   60
      TabIndex        =   21
      Top             =   4320
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   13
      Left            =   60
      TabIndex        =   20
      Top             =   4680
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   14
      Left            =   60
      TabIndex        =   19
      Top             =   5040
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   15
      Left            =   60
      TabIndex        =   18
      Top             =   5400
      Width           =   2505
   End
   Begin VB.Label lab_dm 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   390
      Index           =   16
      Left            =   60
      TabIndex        =   17
      Top             =   5760
      Width           =   2505
   End
End
Attribute VB_Name = "frm_debug_des"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Dim DM As Integer
Enum tip_vis_des
  dec = 0 'visualizzazione dei dati in decimale
  esa = 1 'visualizzazione dei dati in esadecimale
  bin = 2 'visualizzazione dei dati in binario
End Enum
Dim str(0 To 2) As String
Dim vis As tip_vis_des
Dim des As Collection
Dim d As tip_des_debug
Sub add_des(item As String, key As String)

d.desc = item
d.chiave = key
des.add item:=d.desc, key:=d.chiave

End Sub


Sub col_lab()
Dim i As Integer

i = 0
Select Case DM
  
  Case 3500
    lab_dm(i).BackColor = LIGHT_CEL
    i = i + 1
    lab_dm(i).BackColor = vbWhite
    i = i + 1
    lab_dm(i).BackColor = LIGHT_Red
    i = i + 1
    lab_dm(i).BackColor = LIGHT_vbyellow
    For i = 4 To 7
      lab_dm(i).BackColor = COL1
    Next i
    For i = 8 To 11
      lab_dm(i).BackColor = COL2
    Next i
    For i = 12 To 15
      lab_dm(i).BackColor = COL4
    Next i
    lab_dm(i).BackColor = LIGHT_GRAY
    For i = 17 To 49
      lab_dm(i).BackColor = vbWhite
    Next i
  
  Case 3550
    For i = 0 To 49
      lab_dm(i).BackColor = vbWhite
    Next i
  
  Case 3600
    lab_dm(i).BackColor = LIGHT_CEL
    i = i + 1
    lab_dm(i).BackColor = LIGHT_CEL
    i = i + 1
    lab_dm(i).BackColor = LIGHT_Red
    For i = 3 To 7
      lab_dm(i).BackColor = COL1
    Next i
    For i = 8 To 12
      lab_dm(i).BackColor = COL2
    Next i
    For i = 13 To 17
      lab_dm(i).BackColor = COL4
    Next i
    For i = 18 To 19
      lab_dm(i).BackColor = vbWhite
    Next i
    lab_dm(i).BackColor = LIGHT_Red
    i = i + 1
    lab_dm(i).BackColor = COL1
    i = i + 1
    lab_dm(i).BackColor = COL2
    i = i + 1
    lab_dm(i).BackColor = COL4
    For i = 24 To 29
      lab_dm(i).BackColor = vbWhite
    Next i
    lab_dm(30).BackColor = LIGHT_Red
    For i = 31 To 34
      lab_dm(i).BackColor = COL1
    Next i
    For i = 35 To 38
      lab_dm(i).BackColor = COL2
    Next i
    For i = 39 To 42
      lab_dm(i).BackColor = COL4
    Next i
    For i = 43 To 49
      lab_dm(i).BackColor = vbWhite
    Next i
  
  Case 3650
    lab_dm(i).BackColor = COL1
    i = i + 1
    lab_dm(i).BackColor = COL2
    i = i + 1
    lab_dm(i).BackColor = COL4
    i = i + 1
    lab_dm(i).BackColor = LIGHT_BLUE
    i = i + 1
    lab_dm(i).BackColor = LIGHT_BLUE
    i = i + 1
    lab_dm(i).BackColor = COL1
    i = i + 1
    lab_dm(i).BackColor = COL2
    i = i + 1
    lab_dm(i).BackColor = COL4
    For i = 8 To 9
      lab_dm(i).BackColor = vbWhite
    Next i
    lab_dm(10).BackColor = COL_ALL_pink
    For i = 11 To 13
      lab_dm(i).BackColor = COL5
    Next i
    For i = 14 To 16
      lab_dm(i).BackColor = COL3
    Next i
    For i = 17 To 19
      lab_dm(i).BackColor = COL5
    Next i
    For i = 20 To 22
      lab_dm(i).BackColor = COL3
    Next i
    For i = 23 To 25
      lab_dm(i).BackColor = COL5
    Next i
    For i = 26 To 28
      lab_dm(i).BackColor = COL3
    Next i
    For i = 29 To 31
      lab_dm(i).BackColor = COL5
    Next i
    For i = 32 To 47
      lab_dm(i).BackColor = vbWhite
    Next i
    For i = 48 To 49
      lab_dm(i).BackColor = LIGHT_CEL
    Next i
End Select
End Sub

Sub fil_des()
Dim i As Integer
Dim m As Integer

Set des = New Collection

i = 3500
add_des "Comando", CStr(i)
i = i + 1
add_des "Riserva", CStr(i)
i = i + 1
add_des "ID Ordine Corrente/Successivo", CStr(i)
i = i + 1
add_des "Configurazione Ordine Corrente/Successivo", CStr(i)
i = i + 1
add_des "Lunghezza Taglio Livello 1", CStr(i)
i = i + 1
add_des "N° Tagli Livello 1 1° DM", CStr(i)
i = i + 1
add_des "N° Tagli Livello 1 2° DM", CStr(i)
i = i + 1
add_des "Velocità Massima Formato Livello 1", CStr(i)
i = i + 1
add_des "Lunghezza Taglio Livello 2", CStr(i)
i = i + 1
add_des "N° Tagli Livello 2 1° DM", CStr(i)
i = i + 1
add_des "N° Tagli Livello 2 2° DM", CStr(i)
i = i + 1
add_des "Velocità Massima Formato Livello 2", CStr(i)
i = i + 1
add_des "Lunghezza Taglio Livello 3", CStr(i)
i = i + 1
add_des "N° Tagli Livello 3 1° DM", CStr(i)
i = i + 1
add_des "N° Tagli Livello 3 2° DM", CStr(i)
i = i + 1
add_des "Velocità Massima Formato Livello 3", CStr(i)
i = i + 1
add_des "Preallarme (metri)", CStr(i)
For m = i + 1 To 3599
  add_des "Riserva", CStr(m)
Next m
i = m
add_des "Stato Macchina 1° DM", CStr(i)
i = i + 1
add_des "Stato Macchina 2° DM", CStr(i)
i = i + 1
add_des "ID Ordine Corrente", CStr(i)
i = i + 1
add_des "Lunghezza di Taglio Livello 1 Ordine Corrente", CStr(i)
i = i + 1
add_des "N° Tagli Buoni Livello 1 1° DM Ordine Corrente", CStr(i)
i = i + 1
add_des "N° Tagli Buoni Livello 1 2° DM Ordine Corrente", CStr(i)
i = i + 1
add_des "N° Tagli Errati Livello 1 Ordine Corrente", CStr(i)
i = i + 1
add_des "Errore di Taglio Livello 1 Ordine Corrente", CStr(i)
i = i + 1
add_des "Lunghezza di Taglio Livello 2 Ordine Corrente", CStr(i)
i = i + 1
add_des "N° Tagli Buoni Livello 2 1° DM Ordine Corrente", CStr(i)
i = i + 1
add_des "N° Tagli Buoni Livello 2 2° DM Ordine Corrente", CStr(i)
i = i + 1
add_des "N° Tagli Errati Livello 2 Ordine Corrente", CStr(i)
i = i + 1
add_des "Errore di Taglio Livello 2 Ordine Corrente", CStr(i)
i = i + 1
add_des "Lunghezza di Taglio Livello 3 Ordine Corrente", CStr(i)
i = i + 1
add_des "N° Tagli Buoni Livello 3 1° DM Ordine Corrente", CStr(i)
i = i + 1
add_des "N° Tagli Buoni Livello 3 2° DM Ordine Corrente", CStr(i)
i = i + 1
add_des "N° Tagli Errati Livello 3 Ordine Corrente", CStr(i)
i = i + 1
add_des "Errore di Taglio Livello 3 Ordine Corrente", CStr(i)
For m = i + 1 To 3619
  add_des "Riserva", CStr(m)
Next m
i = m
add_des "ID Ordine Successivo", CStr(i)
i = i + 1
add_des "Lunghezza di Taglio Livello 1 Ordine Successivo", CStr(i)
i = i + 1
add_des "Lunghezza di Taglio Livello 2 Ordine Successivo", CStr(i)
i = i + 1
add_des "Lunghezza di Taglio Livello 3 Ordine Successivo", CStr(i)
For m = i + 1 To 3629
  add_des "Riserva", CStr(m)
Next m
i = m
add_des "ID Ultimo Ordine Prodotto", CStr(i)
i = i + 1
add_des "N° Fogli Buoni Livello 1 1° DM Ultimo Ordine Prodotto", CStr(i)
i = i + 1
add_des "N° Fogli Buoni Livello 1 2° DM Ultimo Ordine Prodotto", CStr(i)
i = i + 1
add_des "N° Fogli Errati Livello 1 Ultimo Ordine Prodotto", CStr(i)
i = i + 1
add_des "N° Fogli Scarto Livello 1 Ultimo Ordine Prodotto", CStr(i)
i = i + 1
add_des "N° Fogli Buoni Livello 2 1° DM Ultimo Ordine Prodotto", CStr(i)
i = i + 1
add_des "N° Fogli Buoni Livello 2 2° DM Ultimo Ordine Prodotto", CStr(i)
i = i + 1
add_des "N° Fogli Errati Livello 2 Ultimo Ordine Prodotto", CStr(i)
i = i + 1
add_des "N° Fogli Scarto Livello 2 Ultimo Ordine Prodotto", CStr(i)
i = i + 1
add_des "N° Fogli Buoni Livello 3 1° DM Ultimo Ordine Prodotto", CStr(i)
i = i + 1
add_des "N° Fogli Buoni Livello 3 2° DM Ultimo Ordine Prodotto", CStr(i)
i = i + 1
add_des "N° Fogli Errati Livello 3 Ultimo Ordine Prodotto", CStr(i)
i = i + 1
add_des "N° Fogli Scarto Livello 3 Ultimo Ordine Prodotto", CStr(i)
For m = i + 1 To 3649
  add_des "Riserva", CStr(m)
Next m
i = m
add_des "Velocità Lame (Scheda di Taglio 1)", CStr(i)
i = i + 1
add_des "Velocità Lame (Scheda di Taglio 2)", CStr(i)
i = i + 1
add_des "Velocità Lame (Scheda di Taglio 3)", CStr(i)
i = i + 1
add_des "Riferimento Analogico Sanwich Ingresso", CStr(i)
i = i + 1
add_des "Velocità Sandwich Ingresso", CStr(i)
i = i + 1
add_des "Velocità Sandwich Uscita Livello 1", CStr(i)
i = i + 1
add_des "Velocità Sandwich Uscita Livello 2", CStr(i)
i = i + 1
add_des "Velocità Sandwich Uscita Livello 3", CStr(i)
For m = i + 1 To 3659
  add_des "Riserva", CStr(m)
Next m
i = m
add_des "Identificazione Zone di Allarme", CStr(i)
i = i + 1
add_des "Allarmi Hardware 1° DM", CStr(i)
i = i + 1
add_des "Allarmi Hardware 2° DM", CStr(i)
i = i + 1
add_des "Allarmi Hardware 3° DM", CStr(i)
i = i + 1
add_des "Allarmi QE Master 1° DM", CStr(i)
i = i + 1
add_des "Allarmi QE Master 2° DM", CStr(i)
i = i + 1
add_des "Allarmi QE Master 3° DM", CStr(i)
i = i + 1
add_des "Allarmi QE Slave 1° DM", CStr(i)
i = i + 1
add_des "Allarmi QE Slave 2° DM", CStr(i)
i = i + 1
add_des "Allarmi QE Slave 3° DM", CStr(i)
i = i + 1
add_des "Allarmi QE Esterno 1° DM", CStr(i)
i = i + 1
add_des "Allarmi QE Esterno 2° DM", CStr(i)
i = i + 1
add_des "Allarmi QE Esterno 3° DM", CStr(i)
i = i + 1
add_des "Allarmi Bordo Macchina 1° DM", CStr(i)
i = i + 1
add_des "Allarmi Bordo Macchina 2° DM", CStr(i)
i = i + 1
add_des "Allarmi Bordo Macchina 3° DM", CStr(i)
i = i + 1
add_des "Allarmi Consolle 1° DM", CStr(i)
i = i + 1
add_des "Allarmi Consolle 2° DM", CStr(i)
i = i + 1
add_des "Allarmi Consolle 3° DM", CStr(i)
i = i + 1
add_des "Allarmi Malfunzionamenti (Manutenzione) 1° DM", CStr(i)
i = i + 1
add_des "Allarmi Malfunzionamenti (Manutenzione) 2° DM", CStr(i)
i = i + 1
add_des "Allarmi Malfunzionamenti (Software) 3° DM", CStr(i)
For m = i + 1 To 3697
  add_des "Riserva", CStr(m)
Next m
i = m
add_des "ACK -> Copia del DM 3500", CStr(i)
i = i + 1
add_des "ACK -> Copia del DM 3501", CStr(i)
End Sub


Sub fil_pag()
Dim i As Integer

For i = 0 To 49
  lab_dm(i).Caption = des.item(CStr(DM + i))
Next i
End Sub

Private Sub eve_key_F1(Shift As Integer)
Select Case Shift
  Case 0
  'cambio data memory
  Select Case DM
    Case 3500, 3550, 3600
      DM = DM + 50
    Case 3650
      DM = 3500
  End Select
  lab_int_DM = CStr(DM) & " - " & CStr(DM + 49)
  col_lab
  fil_pag
  Case Else
End Select
End Sub

Private Sub eve_key_F10(Shift As Integer)
Select Case Shift
  Case 0
  'pagina produzione
  tim_debug.Enabled = False
  eve_key.chg pag_pro
  Case Else
End Select
End Sub


Private Sub eve_key_F2(Shift As Integer)
Select Case Shift
  Case 0
  'cambio modo di visualizzazione dei dati
  vis = (vis + 1) Mod 3
  lab_vis = str(CInt(vis))
  Case Else
End Select
End Sub

Private Sub eve_key_F3(Shift As Integer)
Select Case Shift
  Case 0
  'stop timer
  tim_debug.Enabled = False
  Case Else
End Select
End Sub


Private Sub eve_key_F4(Shift As Integer)
Select Case Shift
  Case 0
  'run timer
  If sta_tag.com_OK Then tim_debug.Enabled = True
  Case Else
End Select
End Sub


Private Sub eve_key_F6(Shift As Integer)
Select Case Shift
  Case 0
  'visualizza data memory
  tim_debug.Enabled = False
  eve_key.chg frm_debug
  Case Else
End Select
End Sub


Private Sub Form_Activate()
lab_vis = str(CInt(vis))
lab_int_DM = CStr(DM) & " - " & CStr(DM + 49)
If sta_tag.com_OK Then
    tim_debug.Enabled = True
Else
    del_txt_frm_deb Me
End If
col_lab
fil_des
fil_pag
txt_idle.SetFocus
End Sub


Private Sub Form_Load()
Debug.Print "Inizio Load " & Me.name
disable_tab Me
DM = 3500
vis = dec
col_lab
fil_des
str(0) = "Decimale"
str(1) = "Esadecimale"
str(2) = "Binario"
Set eve_key = New key
eve_key.create Me
Carica Me
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub

Private Sub tim_debug_Timer()
Dim ret_sta_plc As Variant
Dim i As Integer
Dim ret As Integer
      'ABorelli_FI17/11/2006_1

If val(par_mac.com_plc_lan.abi_lan) = 0 Then
  ret_sta_plc = sta_bar.SYSMAC_PLC.readArea(plcAreaDM, DM, 100, vbVariant Or vbArray)
Else
  sta_bar.Fins.readArea plcAreaDM, DM, 100
  ret_sta_plc = sta_bar.Fins.RetData
End If
For i = 0 To 49
  Select Case vis
    Case 0  'decimale
      txt_dm(i).Text = ret_sta_plc(i)
    Case 1  'esadecimale
      txt_dm(i).Text = int_esa(ret_sta_plc(i))
    Case 2  'binario
      txt_dm(i).Text = hex_bin(int_esa(ret_sta_plc(i)), False)
    End Select
Next i
End Sub


