VERSION 5.00
Begin VB.Form stato_IO 
   BackColor       =   &H00FFFFFF&
   Caption         =   "Form1"
   ClientHeight    =   5715
   ClientLeft      =   1950
   ClientTop       =   2460
   ClientWidth     =   11880
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   ScaleHeight     =   5715
   ScaleWidth      =   11880
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   7
      Left            =   2880
      Style           =   1  'Graphical
      TabIndex        =   47
      Top             =   2640
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   6
      Left            =   2880
      Style           =   1  'Graphical
      TabIndex        =   46
      Top             =   2400
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   5
      Left            =   2880
      Style           =   1  'Graphical
      TabIndex        =   45
      Top             =   2160
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   4
      Left            =   2880
      Style           =   1  'Graphical
      TabIndex        =   44
      Top             =   1920
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   210
      Index           =   0
      Left            =   2880
      Style           =   1  'Graphical
      TabIndex        =   43
      Top             =   960
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   1
      Left            =   2880
      Style           =   1  'Graphical
      TabIndex        =   42
      Top             =   1200
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   2
      Left            =   2880
      Style           =   1  'Graphical
      TabIndex        =   41
      Top             =   1440
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   3
      Left            =   2880
      Style           =   1  'Graphical
      TabIndex        =   40
      Top             =   1680
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_1 
      Caption         =   "ON"
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
      Index           =   7
      Left            =   2880
      Style           =   1  'Graphical
      TabIndex        =   39
      Top             =   5160
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_1 
      Caption         =   "ON"
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
      Index           =   6
      Left            =   2880
      Style           =   1  'Graphical
      TabIndex        =   38
      Top             =   4920
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_1 
      Caption         =   "ON"
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
      Index           =   5
      Left            =   2880
      Style           =   1  'Graphical
      TabIndex        =   37
      Top             =   4680
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_1 
      Caption         =   "ON"
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
      Index           =   4
      Left            =   2880
      Style           =   1  'Graphical
      TabIndex        =   36
      Top             =   4440
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_1 
      Caption         =   "ON"
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
      Index           =   0
      Left            =   2880
      Style           =   1  'Graphical
      TabIndex        =   35
      Top             =   3480
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_1 
      Caption         =   "ON"
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
      Index           =   1
      Left            =   2880
      Style           =   1  'Graphical
      TabIndex        =   34
      Top             =   3720
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_1 
      Caption         =   "ON"
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
      Index           =   2
      Left            =   2880
      Style           =   1  'Graphical
      TabIndex        =   33
      Top             =   3960
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_1 
      Caption         =   "ON"
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
      Index           =   3
      Left            =   2880
      Style           =   1  'Graphical
      TabIndex        =   32
      Top             =   4200
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   15
      Left            =   6720
      Style           =   1  'Graphical
      TabIndex        =   31
      Top             =   2640
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   14
      Left            =   6720
      Style           =   1  'Graphical
      TabIndex        =   30
      Top             =   2400
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   13
      Left            =   6720
      Style           =   1  'Graphical
      TabIndex        =   29
      Top             =   2160
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   12
      Left            =   6720
      Style           =   1  'Graphical
      TabIndex        =   28
      Top             =   1920
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   8
      Left            =   6720
      Style           =   1  'Graphical
      TabIndex        =   27
      Top             =   960
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   9
      Left            =   6720
      Style           =   1  'Graphical
      TabIndex        =   26
      Top             =   1200
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   10
      Left            =   6720
      Style           =   1  'Graphical
      TabIndex        =   25
      Top             =   1440
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   11
      Left            =   6720
      Style           =   1  'Graphical
      TabIndex        =   24
      Top             =   1680
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_1 
      Caption         =   "ON"
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
      Index           =   15
      Left            =   6720
      Style           =   1  'Graphical
      TabIndex        =   23
      Top             =   5160
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_1 
      Caption         =   "ON"
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
      Index           =   14
      Left            =   6720
      Style           =   1  'Graphical
      TabIndex        =   22
      Top             =   4920
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_1 
      Caption         =   "ON"
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
      Index           =   13
      Left            =   6720
      Style           =   1  'Graphical
      TabIndex        =   21
      Top             =   4680
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_1 
      Caption         =   "ON"
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
      Index           =   12
      Left            =   6720
      Style           =   1  'Graphical
      TabIndex        =   20
      Top             =   4440
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_1 
      Caption         =   "ON"
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
      Index           =   11
      Left            =   6720
      Style           =   1  'Graphical
      TabIndex        =   19
      Top             =   4200
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_1 
      Caption         =   "ON"
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
      Index           =   10
      Left            =   6720
      Style           =   1  'Graphical
      TabIndex        =   18
      Top             =   3960
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_1 
      Caption         =   "ON"
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
      Index           =   9
      Left            =   6720
      Style           =   1  'Graphical
      TabIndex        =   17
      Top             =   3720
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_1 
      Caption         =   "ON"
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
      Index           =   8
      Left            =   6720
      Style           =   1  'Graphical
      TabIndex        =   16
      Top             =   3480
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   23
      Left            =   10680
      Style           =   1  'Graphical
      TabIndex        =   15
      Top             =   2640
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   22
      Left            =   10680
      Style           =   1  'Graphical
      TabIndex        =   14
      Top             =   2400
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   21
      Left            =   10680
      Style           =   1  'Graphical
      TabIndex        =   13
      Top             =   2160
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   20
      Left            =   10680
      Style           =   1  'Graphical
      TabIndex        =   12
      Top             =   1920
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   16
      Left            =   10680
      Style           =   1  'Graphical
      TabIndex        =   11
      Top             =   960
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   17
      Left            =   10680
      Style           =   1  'Graphical
      TabIndex        =   10
      Top             =   1200
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   18
      Left            =   10680
      Style           =   1  'Graphical
      TabIndex        =   9
      Top             =   1440
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox SetIn 
      Caption         =   "ON"
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
      Index           =   19
      Left            =   10680
      Style           =   1  'Graphical
      TabIndex        =   8
      Top             =   1680
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_2 
      Caption         =   "ON"
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
      Index           =   7
      Left            =   10680
      Style           =   1  'Graphical
      TabIndex        =   7
      Top             =   5160
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_2 
      Caption         =   "ON"
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
      Index           =   6
      Left            =   10680
      Style           =   1  'Graphical
      TabIndex        =   6
      Top             =   4920
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_2 
      Caption         =   "ON"
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
      Index           =   5
      Left            =   10680
      Style           =   1  'Graphical
      TabIndex        =   5
      Top             =   4680
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_2 
      Caption         =   "ON"
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
      Index           =   4
      Left            =   10680
      Style           =   1  'Graphical
      TabIndex        =   4
      Top             =   4440
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_2 
      Caption         =   "ON"
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
      Index           =   3
      Left            =   10680
      Style           =   1  'Graphical
      TabIndex        =   3
      Top             =   4200
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_2 
      Caption         =   "ON"
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
      Index           =   2
      Left            =   10680
      Style           =   1  'Graphical
      TabIndex        =   2
      Top             =   3960
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_2 
      Caption         =   "ON"
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
      Index           =   1
      Left            =   10680
      Style           =   1  'Graphical
      TabIndex        =   1
      Top             =   3720
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.CheckBox set_sign_out_2 
      Caption         =   "ON"
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
      Index           =   0
      Left            =   10680
      Style           =   1  'Graphical
      TabIndex        =   0
      Top             =   3480
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "STATO I/O"
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
      TabIndex        =   108
      Top             =   0
      Width           =   7965
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "stato_IO.frx":0000
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
   Begin VB.Shape sign_in_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   7
      Left            =   2640
      Shape           =   3  'Circle
      Top             =   2640
      Width           =   135
   End
   Begin VB.Shape sign_in_1 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   6
      Left            =   2640
      Shape           =   3  'Circle
      Top             =   2400
      Width           =   135
   End
   Begin VB.Shape sign_in_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   5
      Left            =   2640
      Shape           =   3  'Circle
      Top             =   2160
      Width           =   135
   End
   Begin VB.Shape sign_in_1 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   4
      Left            =   2640
      Shape           =   3  'Circle
      Top             =   1920
      Width           =   135
   End
   Begin VB.Label Label23 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Disponibile"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   0
      TabIndex        =   107
      Top             =   2640
      Width           =   2535
   End
   Begin VB.Label Label24 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Scarto Giunta Copertina"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   0
      TabIndex        =   106
      Top             =   2400
      Width           =   2535
   End
   Begin VB.Label Label25 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Scarto Giunte Onda"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   0
      TabIndex        =   105
      Top             =   2160
      Width           =   2535
   End
   Begin VB.Label Label26 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Scarto a Distanza"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   0
      TabIndex        =   104
      Top             =   1920
      Width           =   2535
   End
   Begin VB.Label Label20 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Macchina in Emergenza"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   0
      TabIndex        =   103
      Top             =   1200
      Width           =   2535
   End
   Begin VB.Label Label21 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Disponibile"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   0
      TabIndex        =   102
      Top             =   1440
      Width           =   2535
   End
   Begin VB.Shape sign_in_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   0
      Left            =   2640
      Shape           =   3  'Circle
      Top             =   960
      Width           =   135
   End
   Begin VB.Shape sign_in_1 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   1
      Left            =   2640
      Shape           =   3  'Circle
      Top             =   1200
      Width           =   135
   End
   Begin VB.Shape sign_in_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   2
      Left            =   2640
      Shape           =   3  'Circle
      Top             =   1440
      Width           =   135
   End
   Begin VB.Label Label22 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Disponibile"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   0
      TabIndex        =   101
      Top             =   1680
      Width           =   2535
   End
   Begin VB.Shape sign_in_1 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   3
      Left            =   2640
      Shape           =   3  'Circle
      Top             =   1680
      Width           =   135
   End
   Begin VB.Label Label97 
      BackStyle       =   0  'Transparent
      Caption         =   "Set"
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
      Left            =   3000
      TabIndex        =   100
      Top             =   720
      Visible         =   0   'False
      Width           =   375
   End
   Begin VB.Label Label19 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Piani in Marcia"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   0
      TabIndex        =   99
      Top             =   960
      Width           =   2535
   End
   Begin VB.Label Label1 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      Caption         =   "Configurazione Ingressi D.B."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   -1  'True
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   375
      Left            =   0
      TabIndex        =   98
      Top             =   600
      Width           =   2775
   End
   Begin VB.Shape sign_out_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   7
      Left            =   2640
      Shape           =   3  'Circle
      Top             =   5160
      Width           =   135
   End
   Begin VB.Shape sign_out_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   6
      Left            =   2640
      Shape           =   3  'Circle
      Top             =   4920
      Width           =   135
   End
   Begin VB.Shape sign_out_1 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   5
      Left            =   2640
      Shape           =   3  'Circle
      Top             =   4680
      Width           =   135
   End
   Begin VB.Shape sign_out_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   4
      Left            =   2640
      Shape           =   3  'Circle
      Top             =   4440
      Width           =   135
   End
   Begin VB.Label Label36 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Disponibile"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   0
      TabIndex        =   97
      Top             =   5160
      Width           =   2535
   End
   Begin VB.Label Label33 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Disponibile"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   0
      TabIndex        =   96
      Top             =   4920
      Width           =   2535
   End
   Begin VB.Label Label34 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Disponibile"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   0
      TabIndex        =   95
      Top             =   4680
      Width           =   2535
   End
   Begin VB.Label Label35 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Blocco Velocità Linea"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   0
      TabIndex        =   94
      Top             =   4440
      Width           =   2535
   End
   Begin VB.Label Label27 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Diminuisci Velocità Linea"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   0
      TabIndex        =   93
      Top             =   4200
      Width           =   2535
   End
   Begin VB.Shape sign_out_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   0
      Left            =   2640
      Shape           =   3  'Circle
      Top             =   3480
      Width           =   135
   End
   Begin VB.Label Label28 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Start alla Marcia"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   0
      TabIndex        =   92
      Top             =   3480
      Width           =   2535
   End
   Begin VB.Label Label29 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Normal Stop Linea"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   0
      TabIndex        =   91
      Top             =   3720
      Width           =   2535
   End
   Begin VB.Label Label30 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Aumenta Velocità Linea"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   0
      TabIndex        =   90
      Top             =   3960
      Width           =   2535
   End
   Begin VB.Shape sign_out_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   1
      Left            =   2640
      Shape           =   3  'Circle
      Top             =   3720
      Width           =   135
   End
   Begin VB.Shape sign_out_1 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   2
      Left            =   2640
      Shape           =   3  'Circle
      Top             =   3960
      Width           =   135
   End
   Begin VB.Shape sign_out_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   3
      Left            =   2640
      Shape           =   3  'Circle
      Top             =   4200
      Width           =   135
   End
   Begin VB.Label Label31 
      BackStyle       =   0  'Transparent
      Caption         =   "Set"
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
      Left            =   3000
      TabIndex        =   89
      Top             =   3240
      Visible         =   0   'False
      Width           =   375
   End
   Begin VB.Label Label32 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      Caption         =   "Configurazione Uscite D.B."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   -1  'True
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   375
      Left            =   0
      TabIndex        =   88
      Top             =   3120
      Width           =   2775
   End
   Begin VB.Shape sign_in_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   15
      Left            =   6480
      Shape           =   3  'Circle
      Top             =   2640
      Width           =   135
   End
   Begin VB.Shape sign_in_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   14
      Left            =   6480
      Shape           =   3  'Circle
      Top             =   2400
      Width           =   135
   End
   Begin VB.Shape sign_in_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   13
      Left            =   6480
      Shape           =   3  'Circle
      Top             =   2160
      Width           =   135
   End
   Begin VB.Shape sign_in_1 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   12
      Left            =   6480
      Shape           =   3  'Circle
      Top             =   1920
      Width           =   135
   End
   Begin VB.Label Label8 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Gruppo di Scarto Disabilitato"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3840
      TabIndex        =   87
      Top             =   2640
      Width           =   2535
   End
   Begin VB.Label Label18 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Macchina Pronta all'AOC"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3840
      TabIndex        =   86
      Top             =   2400
      Width           =   2535
   End
   Begin VB.Label Label6 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Macchina Pronta alla Marcia"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3840
      TabIndex        =   85
      Top             =   2160
      Width           =   2535
   End
   Begin VB.Label Label5 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Taglio Effettuato"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3840
      TabIndex        =   84
      Top             =   1920
      Width           =   2535
   End
   Begin VB.Shape sign_in_1 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   11
      Left            =   6480
      Shape           =   3  'Circle
      Top             =   1680
      Width           =   135
   End
   Begin VB.Label Label7 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Start AOC"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3840
      TabIndex        =   83
      Top             =   1680
      Width           =   2535
   End
   Begin VB.Shape sign_in_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   10
      Left            =   6480
      Shape           =   3  'Circle
      Top             =   1440
      Width           =   135
   End
   Begin VB.Shape sign_in_1 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   8
      Left            =   6480
      Shape           =   3  'Circle
      Top             =   960
      Width           =   135
   End
   Begin VB.Shape sign_in_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   9
      Left            =   6480
      Shape           =   3  'Circle
      Top             =   1200
      Width           =   135
   End
   Begin VB.Label Label3 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Normal Stop"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3840
      TabIndex        =   82
      Top             =   1440
      Width           =   2535
   End
   Begin VB.Label Label2 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Diminuisci Velocità"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3840
      TabIndex        =   81
      Top             =   1200
      Width           =   2535
   End
   Begin VB.Label Label4 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Aumenta Velocità"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3840
      TabIndex        =   80
      Top             =   960
      Width           =   2535
   End
   Begin VB.Label Label99 
      BackStyle       =   0  'Transparent
      Caption         =   "Set"
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
      Left            =   6840
      TabIndex        =   79
      Top             =   720
      Visible         =   0   'False
      Width           =   375
   End
   Begin VB.Label Label54 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      Caption         =   "Configurazione Ingressi R.S."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   -1  'True
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   375
      Left            =   3840
      TabIndex        =   78
      Top             =   600
      Width           =   2775
   End
   Begin VB.Shape sign_out_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   15
      Left            =   6480
      Shape           =   3  'Circle
      Top             =   5160
      Width           =   135
   End
   Begin VB.Shape sign_out_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   14
      Left            =   6480
      Shape           =   3  'Circle
      Top             =   4920
      Width           =   135
   End
   Begin VB.Shape sign_out_1 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   13
      Left            =   6480
      Shape           =   3  'Circle
      Top             =   4680
      Width           =   135
   End
   Begin VB.Shape sign_out_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   12
      Left            =   6480
      Shape           =   3  'Circle
      Top             =   4440
      Width           =   135
   End
   Begin VB.Label Label16 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Abilitazione Start AOC da RS"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3840
      TabIndex        =   77
      Top             =   5160
      Width           =   2535
   End
   Begin VB.Label Label13 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "N°Scarti (msb)"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3840
      TabIndex        =   76
      Top             =   4920
      Width           =   2535
   End
   Begin VB.Label Label14 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "N°Scarti"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3840
      TabIndex        =   75
      Top             =   4680
      Width           =   2535
   End
   Begin VB.Label Label15 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "N°Scarti (lsb)"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3840
      TabIndex        =   74
      Top             =   4440
      Width           =   2535
   End
   Begin VB.Label Label17 
      BackStyle       =   0  'Transparent
      Caption         =   "Set"
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
      Left            =   6840
      TabIndex        =   73
      Top             =   3240
      Visible         =   0   'False
      Width           =   375
   End
   Begin VB.Shape sign_out_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   11
      Left            =   6480
      Shape           =   3  'Circle
      Top             =   4200
      Width           =   135
   End
   Begin VB.Shape sign_out_1 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   10
      Left            =   6480
      Shape           =   3  'Circle
      Top             =   3960
      Width           =   135
   End
   Begin VB.Shape sign_out_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   9
      Left            =   6480
      Shape           =   3  'Circle
      Top             =   3720
      Width           =   135
   End
   Begin VB.Label Label12 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Preallarme all'AOC"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3840
      TabIndex        =   72
      Top             =   3960
      Width           =   2535
   End
   Begin VB.Label Label11 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Taglio Continuo"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3840
      TabIndex        =   71
      Top             =   3720
      Width           =   2535
   End
   Begin VB.Label Label10 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Taglio Singolo"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3840
      TabIndex        =   70
      Top             =   3480
      Width           =   2535
   End
   Begin VB.Shape sign_out_1 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   8
      Left            =   6480
      Shape           =   3  'Circle
      Top             =   3480
      Width           =   135
   End
   Begin VB.Label Label9 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Linea in Marcia"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3840
      TabIndex        =   69
      Top             =   4200
      Width           =   2535
   End
   Begin VB.Label Label55 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      Caption         =   "Configurazione Uscite R.S."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   -1  'True
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   375
      Left            =   3960
      TabIndex        =   68
      Top             =   3120
      Width           =   2775
   End
   Begin VB.Shape sign_in_2 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   7
      Left            =   10440
      Shape           =   3  'Circle
      Top             =   2640
      Width           =   135
   End
   Begin VB.Shape sign_in_2 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   6
      Left            =   10440
      Shape           =   3  'Circle
      Top             =   2400
      Width           =   135
   End
   Begin VB.Shape sign_in_2 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   5
      Left            =   10440
      Shape           =   3  'Circle
      Top             =   2160
      Width           =   135
   End
   Begin VB.Shape sign_in_2 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   4
      Left            =   10440
      Shape           =   3  'Circle
      Top             =   1920
      Width           =   135
   End
   Begin VB.Label Label41 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Tca Posizionato"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   7800
      TabIndex        =   67
      Top             =   2640
      Width           =   2535
   End
   Begin VB.Label Label42 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Normal Stop Linea"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   7800
      TabIndex        =   66
      Top             =   2400
      Width           =   2535
   End
   Begin VB.Label Label43 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Diminuisci Velocità Linea"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   7800
      TabIndex        =   65
      Top             =   2160
      Width           =   2535
   End
   Begin VB.Label Label44 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Aumenta Velocità Linea"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   7800
      TabIndex        =   64
      Top             =   1920
      Width           =   2535
   End
   Begin VB.Label Label37 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Macchina Pronta alla Marcia"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   7800
      TabIndex        =   63
      Top             =   960
      Width           =   2535
   End
   Begin VB.Label Label38 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Macchina Pronta al Cambio"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   7800
      TabIndex        =   62
      Top             =   1200
      Width           =   2535
   End
   Begin VB.Label Label39 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Cambio Ordine in Corso"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   7800
      TabIndex        =   61
      Top             =   1440
      Width           =   2535
   End
   Begin VB.Shape sign_in_2 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   0
      Left            =   10440
      Shape           =   3  'Circle
      Top             =   960
      Width           =   135
   End
   Begin VB.Shape sign_in_2 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   1
      Left            =   10440
      Shape           =   3  'Circle
      Top             =   1200
      Width           =   135
   End
   Begin VB.Shape sign_in_2 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   2
      Left            =   10440
      Shape           =   3  'Circle
      Top             =   1440
      Width           =   135
   End
   Begin VB.Label Label40 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Macchina in Emergenza"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   7800
      TabIndex        =   60
      Top             =   1680
      Width           =   2535
   End
   Begin VB.Shape sign_in_2 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   3
      Left            =   10440
      Shape           =   3  'Circle
      Top             =   1680
      Width           =   135
   End
   Begin VB.Label Label98 
      BackStyle       =   0  'Transparent
      Caption         =   "Set"
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
      Left            =   10800
      TabIndex        =   59
      Top             =   690
      Visible         =   0   'False
      Width           =   375
   End
   Begin VB.Shape sign_out_2 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   7
      Left            =   10440
      Shape           =   3  'Circle
      Top             =   5160
      Width           =   135
   End
   Begin VB.Shape sign_out_2 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   6
      Left            =   10440
      Shape           =   3  'Circle
      Top             =   4920
      Width           =   135
   End
   Begin VB.Shape sign_out_2 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   5
      Left            =   10440
      Shape           =   3  'Circle
      Top             =   4680
      Width           =   135
   End
   Begin VB.Shape sign_out_2 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   4
      Left            =   10440
      Shape           =   3  'Circle
      Top             =   4440
      Width           =   135
   End
   Begin VB.Label Label50 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Disponibile"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   7800
      TabIndex        =   58
      Top             =   5160
      Width           =   2415
   End
   Begin VB.Label Label53 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Linea in Marcia"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   7800
      TabIndex        =   57
      Top             =   4920
      Width           =   1575
   End
   Begin VB.Label Label52 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Posizionamento Veloce"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   7800
      TabIndex        =   56
      Top             =   4680
      Width           =   2535
   End
   Begin VB.Label Label51 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Reset Cambio Ordine"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   7800
      TabIndex        =   55
      Top             =   4440
      Width           =   2535
   End
   Begin VB.Label Label49 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Presenza cart. ruota misura"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   7800
      TabIndex        =   54
      Top             =   4200
      Width           =   2535
   End
   Begin VB.Shape sign_out_2 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   3
      Left            =   10440
      Shape           =   3  'Circle
      Top             =   4200
      Width           =   135
   End
   Begin VB.Label Label48 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Preparazione al Cambio"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   7800
      TabIndex        =   53
      Top             =   3480
      Width           =   2535
   End
   Begin VB.Label Label47 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Start Cambio "
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   7800
      TabIndex        =   52
      Top             =   3720
      Width           =   2535
   End
   Begin VB.Label Label46 
      BackColor       =   &H00FF0000&
      BackStyle       =   0  'Transparent
      Caption         =   "Cambio con Taglio Acqua"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   7800
      TabIndex        =   51
      Top             =   3960
      Width           =   2535
   End
   Begin VB.Shape sign_out_2 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   2
      Left            =   10440
      Shape           =   3  'Circle
      Top             =   3960
      Width           =   135
   End
   Begin VB.Shape sign_out_2 
      BackColor       =   &H0000FF00&
      BackStyle       =   1  'Opaque
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   1
      Left            =   10440
      Shape           =   3  'Circle
      Top             =   3720
      Width           =   135
   End
   Begin VB.Shape sign_out_2 
      BackColor       =   &H0000FF00&
      FillColor       =   &H0000FF00&
      FillStyle       =   0  'Solid
      Height          =   255
      Index           =   0
      Left            =   10440
      Shape           =   3  'Circle
      Top             =   3480
      Width           =   135
   End
   Begin VB.Label Label45 
      BackStyle       =   0  'Transparent
      Caption         =   "Set"
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
      Left            =   10800
      TabIndex        =   50
      Top             =   3210
      Visible         =   0   'False
      Width           =   375
   End
   Begin VB.Label Label56 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      Caption         =   "Configurazione Uscite Tca."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   -1  'True
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   375
      Left            =   7800
      TabIndex        =   49
      Top             =   3120
      Width           =   2775
   End
   Begin VB.Label Label57 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      Caption         =   "Configurazione Ingressi Tca."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   -1  'True
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   375
      Left            =   7800
      TabIndex        =   48
      Top             =   600
      Width           =   2775
   End
End
Attribute VB_Name = "stato_IO"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Form_Load()
Debug.Print "Inizio Load " & Me.name
Carica Me
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub


