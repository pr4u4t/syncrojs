VERSION 5.00
Begin VB.Form Tasti 
   Appearance      =   0  'Flat
   AutoRedraw      =   -1  'True
   BackColor       =   &H80000005&
   BorderStyle     =   0  'None
   ClientHeight    =   900
   ClientLeft      =   1770
   ClientTop       =   3990
   ClientWidth     =   11925
   ControlBox      =   0   'False
   BeginProperty Font 
      Name            =   "Arial"
      Size            =   8.25
      Charset         =   0
      Weight          =   700
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   ForeColor       =   &H80000008&
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   900
   ScaleWidth      =   11925
   ShowInTaskbar   =   0   'False
   Tag             =   "for"
   Begin VB.Label txt_shift 
      BackStyle       =   0  'Transparent
      ForeColor       =   &H00B6717B&
      Height          =   180
      Index           =   10
      Left            =   11565
      TabIndex        =   29
      Top             =   180
      Width           =   390
   End
   Begin VB.Label txt_shift 
      BackStyle       =   0  'Transparent
      ForeColor       =   &H00B6717B&
      Height          =   180
      Index           =   9
      Left            =   10215
      TabIndex        =   28
      Top             =   180
      Width           =   390
   End
   Begin VB.Label txt_shift 
      BackStyle       =   0  'Transparent
      ForeColor       =   &H00B6717B&
      Height          =   180
      Index           =   8
      Left            =   9045
      TabIndex        =   27
      Top             =   180
      Width           =   390
   End
   Begin VB.Label txt_shift 
      BackStyle       =   0  'Transparent
      ForeColor       =   &H00B6717B&
      Height          =   180
      Index           =   7
      Left            =   7845
      TabIndex        =   26
      Top             =   180
      Width           =   390
   End
   Begin VB.Label txt_shift 
      BackStyle       =   0  'Transparent
      ForeColor       =   &H00B6717B&
      Height          =   180
      Index           =   6
      Left            =   6675
      TabIndex        =   25
      Top             =   180
      Width           =   390
   End
   Begin VB.Label txt_shift 
      BackStyle       =   0  'Transparent
      ForeColor       =   &H00B6717B&
      Height          =   180
      Index           =   5
      Left            =   5460
      TabIndex        =   24
      Top             =   180
      Width           =   390
   End
   Begin VB.Label txt_shift 
      BackStyle       =   0  'Transparent
      ForeColor       =   &H00B6717B&
      Height          =   180
      Index           =   4
      Left            =   4275
      TabIndex        =   23
      Top             =   180
      Width           =   390
   End
   Begin VB.Label txt_shift 
      BackStyle       =   0  'Transparent
      ForeColor       =   &H00B6717B&
      Height          =   180
      Index           =   3
      Left            =   3075
      TabIndex        =   22
      Top             =   180
      Width           =   390
   End
   Begin VB.Label txt_shift 
      BackStyle       =   0  'Transparent
      ForeColor       =   &H00B6717B&
      Height          =   180
      Index           =   2
      Left            =   1890
      TabIndex        =   21
      Top             =   180
      Width           =   390
   End
   Begin VB.Shape Shp_bianca 
      BackStyle       =   1  'Opaque
      BorderStyle     =   0  'Transparent
      Height          =   450
      Index           =   2
      Left            =   2300
      Top             =   -10
      Width           =   90
   End
   Begin VB.Label txt_shift 
      BackStyle       =   0  'Transparent
      ForeColor       =   &H00B6717B&
      Height          =   180
      Index           =   1
      Left            =   705
      TabIndex        =   20
      Top             =   180
      Width           =   390
   End
   Begin VB.Shape Shp_bordo 
      BorderWidth     =   4
      Height          =   910
      Left            =   0
      Top             =   0
      Width           =   12035
   End
   Begin VB.Shape Shp_bianca 
      BackStyle       =   1  'Opaque
      BorderStyle     =   0  'Transparent
      Height          =   450
      Index           =   9
      Left            =   10605
      Top             =   -10
      Width           =   90
   End
   Begin VB.Shape Shp_bianca 
      BackStyle       =   1  'Opaque
      BorderStyle     =   0  'Transparent
      Height          =   450
      Index           =   8
      Left            =   9430
      Top             =   -10
      Width           =   90
   End
   Begin VB.Shape Shp_bianca 
      BackStyle       =   1  'Opaque
      BorderStyle     =   0  'Transparent
      Height          =   450
      Index           =   7
      Left            =   8240
      Top             =   -10
      Width           =   90
   End
   Begin VB.Shape Shp_bianca 
      BackStyle       =   1  'Opaque
      BorderStyle     =   0  'Transparent
      Height          =   450
      Index           =   6
      Left            =   7070
      Top             =   -10
      Width           =   90
   End
   Begin VB.Shape Shp_bianca 
      BackStyle       =   1  'Opaque
      BorderStyle     =   0  'Transparent
      Height          =   450
      Index           =   5
      Left            =   5860
      Top             =   -10
      Width           =   90
   End
   Begin VB.Shape Shp_bianca 
      BackStyle       =   1  'Opaque
      BorderStyle     =   0  'Transparent
      Height          =   450
      Index           =   4
      Left            =   4660
      Top             =   -10
      Width           =   90
   End
   Begin VB.Shape Shp_bianca 
      BackStyle       =   1  'Opaque
      BorderStyle     =   0  'Transparent
      Height          =   450
      Index           =   3
      Left            =   3470
      Top             =   -10
      Width           =   90
   End
   Begin VB.Shape Shp_bianca 
      BackStyle       =   1  'Opaque
      BorderStyle     =   0  'Transparent
      Height          =   450
      Index           =   0
      Left            =   1095
      Top             =   -10
      Width           =   90
   End
   Begin VB.Label des_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
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
      Height          =   510
      Index           =   2
      Left            =   1245
      TabIndex        =   19
      Top             =   420
      Width           =   1100
      WordWrap        =   -1  'True
   End
   Begin VB.Line Line9 
      BorderColor     =   &H00B6717B&
      BorderWidth     =   4
      X1              =   10710
      X2              =   10710
      Y1              =   0
      Y2              =   1000
   End
   Begin VB.Line Line8 
      BorderColor     =   &H00B6717B&
      BorderWidth     =   4
      X1              =   9540
      X2              =   9540
      Y1              =   0
      Y2              =   1000
   End
   Begin VB.Line Line7 
      BorderColor     =   &H00B6717B&
      BorderWidth     =   4
      X1              =   8340
      X2              =   8340
      Y1              =   0
      Y2              =   1000
   End
   Begin VB.Line Line6 
      BorderColor     =   &H00B6717B&
      BorderWidth     =   4
      X1              =   7170
      X2              =   7170
      Y1              =   0
      Y2              =   1000
   End
   Begin VB.Line Line5 
      BorderColor     =   &H00B6717B&
      BorderWidth     =   4
      X1              =   5970
      X2              =   5970
      Y1              =   0
      Y2              =   1000
   End
   Begin VB.Line Line4 
      BorderColor     =   &H00B6717B&
      BorderWidth     =   4
      X1              =   4770
      X2              =   4770
      Y1              =   -30
      Y2              =   970
   End
   Begin VB.Line Line3 
      BorderColor     =   &H00B6717B&
      BorderWidth     =   4
      X1              =   3570
      X2              =   3570
      Y1              =   0
      Y2              =   1000
   End
   Begin VB.Line Line2 
      BorderColor     =   &H00B6717B&
      BorderWidth     =   4
      X1              =   2400
      X2              =   2400
      Y1              =   0
      Y2              =   1000
   End
   Begin VB.Line Line1 
      BorderColor     =   &H00B6717B&
      BorderWidth     =   4
      X1              =   1200
      X2              =   1200
      Y1              =   0
      Y2              =   1000
   End
   Begin VB.Label tas_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "F1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   15.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   300
      Index           =   1
      Left            =   45
      TabIndex        =   18
      Top             =   90
      Width           =   540
   End
   Begin VB.Label tas_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "F2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   15.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   305
      Index           =   2
      Left            =   1245
      TabIndex        =   17
      Top             =   90
      Width           =   540
   End
   Begin VB.Label tas_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "F3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   15.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   305
      Index           =   3
      Left            =   2445
      TabIndex        =   16
      Top             =   90
      Width           =   540
   End
   Begin VB.Label tas_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "F4"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   15.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   300
      Index           =   4
      Left            =   3615
      TabIndex        =   15
      Top             =   90
      Width           =   540
   End
   Begin VB.Label tas_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "F5"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   15.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   305
      Index           =   5
      Left            =   4815
      TabIndex        =   14
      Top             =   90
      Width           =   540
   End
   Begin VB.Label tas_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "F6"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   15.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   305
      Index           =   6
      Left            =   6015
      TabIndex        =   13
      Top             =   90
      Width           =   540
   End
   Begin VB.Label tas_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "F7"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   15.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   300
      Index           =   7
      Left            =   7215
      TabIndex        =   12
      Top             =   90
      Width           =   540
   End
   Begin VB.Label tas_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "F8"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   15.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   305
      Index           =   8
      Left            =   8385
      TabIndex        =   11
      Top             =   90
      Width           =   540
   End
   Begin VB.Label tas_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "F9"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   15.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   305
      Index           =   9
      Left            =   9585
      TabIndex        =   10
      Top             =   90
      Width           =   540
   End
   Begin VB.Label tas_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "F10"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   15.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   300
      Index           =   10
      Left            =   10755
      TabIndex        =   9
      Top             =   90
      Width           =   540
   End
   Begin VB.Label des_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   510
      Index           =   3
      Left            =   2445
      TabIndex        =   8
      Top             =   420
      Width           =   1100
      WordWrap        =   -1  'True
   End
   Begin VB.Label des_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   510
      Index           =   4
      Left            =   3615
      TabIndex        =   7
      Top             =   420
      Width           =   1100
      WordWrap        =   -1  'True
   End
   Begin VB.Label des_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   510
      Index           =   5
      Left            =   4815
      TabIndex        =   6
      Top             =   420
      Width           =   1100
      WordWrap        =   -1  'True
   End
   Begin VB.Label des_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   510
      Index           =   6
      Left            =   6015
      TabIndex        =   5
      Top             =   420
      Width           =   1100
      WordWrap        =   -1  'True
   End
   Begin VB.Label des_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   " "
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   510
      Index           =   7
      Left            =   7215
      TabIndex        =   4
      Top             =   420
      Width           =   1100
      WordWrap        =   -1  'True
   End
   Begin VB.Label des_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   " "
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   510
      Index           =   8
      Left            =   8385
      TabIndex        =   3
      Top             =   420
      Width           =   1100
      WordWrap        =   -1  'True
   End
   Begin VB.Label des_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   " "
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   510
      Index           =   9
      Left            =   9585
      TabIndex        =   2
      Top             =   420
      Width           =   1100
      WordWrap        =   -1  'True
   End
   Begin VB.Label des_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   " "
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   510
      Index           =   10
      Left            =   10755
      TabIndex        =   1
      Top             =   420
      Width           =   1100
      WordWrap        =   -1  'True
   End
   Begin VB.Label des_fun 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   510
      Index           =   1
      Left            =   45
      TabIndex        =   0
      Top             =   420
      Width           =   1100
      WordWrap        =   -1  'True
   End
   Begin VB.Shape Shp_viola 
      BorderStyle     =   0  'Transparent
      FillColor       =   &H00B6717B&
      FillStyle       =   0  'Solid
      Height          =   180
      Left            =   0
      Top             =   0
      Width           =   12000
   End
End
Attribute VB_Name = "Tasti"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit













Private Sub Form_Load()
'carica la form dei tasti, che resterà sempre visibile
  Debug.Print "Inizio Load " & Me.name
  Me.Width = Screen.Width
  Me.Move 0, 0
  Me.Height = Int(Me.Height * pro_set.PercY / 100)

  dimensiona Me
  Debug.Print "Fine Load " & Me.name
  Debug.Print ""
End Sub

Private Sub Form_Unload(Cancel As Integer)
'  esci = 1
End Sub

