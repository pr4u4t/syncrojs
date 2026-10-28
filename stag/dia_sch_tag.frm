VERSION 5.00
Begin VB.Form dia_sch_tag 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   6225
   ClientLeft      =   210
   ClientTop       =   1920
   ClientWidth     =   11970
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   ScaleHeight     =   6225
   ScaleWidth      =   11970
   ShowInTaskbar   =   0   'False
   Begin VB.Label lab_ris 
      Alignment       =   2  'Center
      BackStyle       =   0  'Transparent
      Caption         =   "ERROR"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   15.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      Height          =   330
      Left            =   5235
      TabIndex        =   130
      Top             =   -45
      Width           =   1380
   End
   Begin VB.Label lab_par_17 
      BackStyle       =   0  'Transparent
      Caption         =   "KP proporzionale"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   4
      Left            =   8220
      TabIndex        =   129
      Top             =   5310
      Width           =   1395
   End
   Begin VB.Label lab_par_17 
      BackStyle       =   0  'Transparent
      Caption         =   "Fat.mol.enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   3
      Left            =   6660
      TabIndex        =   128
      Top             =   5610
      Width           =   1395
   End
   Begin VB.Label lab_par_17 
      BackStyle       =   0  'Transparent
      Caption         =   "Imp. giro enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   2
      Left            =   6660
      TabIndex        =   127
      Top             =   5460
      Width           =   1395
   End
   Begin VB.Label lab_par_17 
      BackStyle       =   0  'Transparent
      Caption         =   "Giri max mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   1
      Left            =   6660
      TabIndex        =   126
      Top             =   5310
      Width           =   1395
   End
   Begin VB.Label lab_par_16 
      BackStyle       =   0  'Transparent
      Caption         =   "Tem.campion.PIC"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   5
      Left            =   8220
      TabIndex        =   125
      Top             =   4740
      Width           =   1395
   End
   Begin VB.Label lab_par_16 
      BackStyle       =   0  'Transparent
      Caption         =   "KP proporzionale"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   4
      Left            =   8220
      TabIndex        =   124
      Top             =   4590
      Width           =   1395
   End
   Begin VB.Label lab_par_16 
      BackStyle       =   0  'Transparent
      Caption         =   "Fat.mol.enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   3
      Left            =   6660
      TabIndex        =   123
      Top             =   4890
      Width           =   1395
   End
   Begin VB.Label lab_par_16 
      BackStyle       =   0  'Transparent
      Caption         =   "Imp. giro enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   2
      Left            =   6660
      TabIndex        =   122
      Top             =   4740
      Width           =   1395
   End
   Begin VB.Label lab_par_16 
      BackStyle       =   0  'Transparent
      Caption         =   "Giri max mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   1
      Left            =   6660
      TabIndex        =   121
      Top             =   4590
      Width           =   1395
   End
   Begin VB.Label lab_par_14 
      BackStyle       =   0  'Transparent
      Caption         =   "Sovravelocità in zona sincrona in minima velocità"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   1
      Left            =   6660
      TabIndex        =   120
      Top             =   3930
      Width           =   5775
   End
   Begin VB.Label lab_par_13 
      BackStyle       =   0  'Transparent
      Caption         =   "Fat.mol.enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   3
      Left            =   6660
      TabIndex        =   119
      Top             =   3510
      Width           =   1395
   End
   Begin VB.Label lab_par_13 
      BackStyle       =   0  'Transparent
      Caption         =   "Imp. giro enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   2
      Left            =   6660
      TabIndex        =   118
      Top             =   3360
      Width           =   1395
   End
   Begin VB.Label lab_par_13 
      BackStyle       =   0  'Transparent
      Caption         =   "Giri max mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   1
      Left            =   6660
      TabIndex        =   117
      Top             =   3210
      Width           =   1395
   End
   Begin VB.Label lab_par_12 
      BackStyle       =   0  'Transparent
      Caption         =   "La quota inizio zona sincrona è impostata internamente al PLC a 1/5"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   1
      Left            =   6660
      TabIndex        =   116
      Top             =   2640
      Width           =   5775
   End
   Begin VB.Label lab_par_12 
      BackStyle       =   0  'Transparent
      Caption         =   "La zona sincr. ha inizio quando il contatore lame è < del valore impostato"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   2
      Left            =   6660
      TabIndex        =   115
      Top             =   2790
      Width           =   5775
   End
   Begin VB.Label lab_par_11 
      BackStyle       =   0  'Transparent
      Caption         =   "Ang. sincr."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   4
      Left            =   8220
      TabIndex        =   114
      Top             =   1920
      Width           =   1395
   End
   Begin VB.Label lab_par_11 
      BackStyle       =   0  'Transparent
      Caption         =   "Rap. rid. mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   1
      Left            =   6660
      TabIndex        =   113
      Top             =   1920
      Width           =   1395
   End
   Begin VB.Label lab_par_11 
      BackStyle       =   0  'Transparent
      Caption         =   "Fat.mol.enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   3
      Left            =   6660
      TabIndex        =   112
      Top             =   2220
      Width           =   1395
   End
   Begin VB.Label lab_par_11 
      BackStyle       =   0  'Transparent
      Caption         =   "Imp. giro enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   2
      Left            =   6660
      TabIndex        =   111
      Top             =   2070
      Width           =   1395
   End
   Begin VB.Label lab_par_10 
      BackStyle       =   0  'Transparent
      Caption         =   "Circ. lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   2
      Left            =   6660
      TabIndex        =   110
      Top             =   1350
      Width           =   1395
   End
   Begin VB.Label lab_par_10 
      BackStyle       =   0  'Transparent
      Caption         =   "Rap. rid. mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   7
      Left            =   9780
      TabIndex        =   109
      Top             =   1200
      Width           =   1395
   End
   Begin VB.Label lab_par_10 
      BackStyle       =   0  'Transparent
      Caption         =   "Fat.mol.enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   6
      Left            =   8220
      TabIndex        =   108
      Top             =   1500
      Width           =   1395
   End
   Begin VB.Label lab_par_10 
      BackStyle       =   0  'Transparent
      Caption         =   "Imp. giro enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   5
      Left            =   8220
      TabIndex        =   107
      Top             =   1350
      Width           =   1395
   End
   Begin VB.Label lab_par_10 
      BackStyle       =   0  'Transparent
      Caption         =   "Vel.tà max. di lin."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   4
      Left            =   8220
      TabIndex        =   106
      Top             =   1200
      Width           =   1395
   End
   Begin VB.Label lab_par_10 
      BackStyle       =   0  'Transparent
      Caption         =   "Sovr.tà lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   3
      Left            =   6660
      TabIndex        =   105
      Top             =   1500
      Width           =   1395
   End
   Begin VB.Label lab_par_10 
      BackStyle       =   0  'Transparent
      Caption         =   "Tem.acc.tag.lun"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   1
      Left            =   6660
      TabIndex        =   104
      Top             =   1200
      Width           =   1395
   End
   Begin VB.Label lab_par_9 
      BackStyle       =   0  'Transparent
      Caption         =   "Rap. rid. mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   3
      Left            =   6690
      TabIndex        =   103
      Top             =   780
      Width           =   1395
   End
   Begin VB.Label lab_par_9 
      BackStyle       =   0  'Transparent
      Caption         =   "Fat.mol.enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   1
      Left            =   6690
      TabIndex        =   102
      Top             =   480
      Width           =   1395
   End
   Begin VB.Label lab_par_9 
      BackStyle       =   0  'Transparent
      Caption         =   "Imp. giro enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   2
      Left            =   6690
      TabIndex        =   101
      Top             =   630
      Width           =   1395
   End
   Begin VB.Label lab_par_8 
      BackStyle       =   0  'Transparent
      Caption         =   "Rap. rid. mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   3
      Left            =   120
      TabIndex        =   100
      Top             =   5820
      Width           =   1395
   End
   Begin VB.Label lab_par_8 
      BackStyle       =   0  'Transparent
      Caption         =   "Fat.mol.enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   1
      Left            =   120
      TabIndex        =   99
      Top             =   5520
      Width           =   1395
   End
   Begin VB.Label lab_par_8 
      BackStyle       =   0  'Transparent
      Caption         =   "Imp. giro enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   2
      Left            =   120
      TabIndex        =   98
      Top             =   5670
      Width           =   1395
   End
   Begin VB.Label lab_par_8 
      BackStyle       =   0  'Transparent
      Caption         =   "Ang. sincr."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   5
      Left            =   1620
      TabIndex        =   97
      Top             =   5670
      Width           =   1395
   End
   Begin VB.Label lab_par_8 
      BackStyle       =   0  'Transparent
      Caption         =   "Giri max mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   4
      Left            =   1620
      TabIndex        =   96
      Top             =   5520
      Width           =   1395
   End
   Begin VB.Label lab_par_7 
      BackStyle       =   0  'Transparent
      Caption         =   "Lung.taglio testa"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   8
      Left            =   3210
      TabIndex        =   95
      Top             =   4950
      Width           =   1395
   End
   Begin VB.Label lab_par_7 
      BackStyle       =   0  'Transparent
      Caption         =   "Dis.fot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   7
      Left            =   3210
      TabIndex        =   94
      Top             =   4800
      Width           =   1395
   End
   Begin VB.Label lab_par_7 
      BackStyle       =   0  'Transparent
      Caption         =   "Lung. taglio ordine"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   6
      Left            =   1650
      TabIndex        =   93
      Top             =   5100
      Width           =   1395
   End
   Begin VB.Label lab_par_7 
      BackStyle       =   0  'Transparent
      Caption         =   "Circ. lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   4
      Left            =   1650
      TabIndex        =   92
      Top             =   4800
      Width           =   1395
   End
   Begin VB.Label lab_par_7 
      BackStyle       =   0  'Transparent
      Caption         =   "Rap. rid. mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   3
      Left            =   120
      TabIndex        =   91
      Top             =   5100
      Width           =   1395
   End
   Begin VB.Label lab_par_7 
      BackStyle       =   0  'Transparent
      Caption         =   "Sovr.tà lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   5
      Left            =   1650
      TabIndex        =   90
      Top             =   4950
      Width           =   1395
   End
   Begin VB.Label lab_par_7 
      BackStyle       =   0  'Transparent
      Caption         =   "Fat.mol.enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   1
      Left            =   120
      TabIndex        =   89
      Top             =   4800
      Width           =   1395
   End
   Begin VB.Label lab_par_7 
      BackStyle       =   0  'Transparent
      Caption         =   "Imp. giro enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   2
      Left            =   120
      TabIndex        =   88
      Top             =   4950
      Width           =   1395
   End
   Begin VB.Label lab_par_6 
      BackStyle       =   0  'Transparent
      Caption         =   "Lung. taglio ordine"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   6
      Left            =   1650
      TabIndex        =   87
      Top             =   4380
      Width           =   1395
   End
   Begin VB.Label lab_par_6 
      BackStyle       =   0  'Transparent
      Caption         =   "Circ. lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   4
      Left            =   1650
      TabIndex        =   86
      Top             =   4080
      Width           =   1395
   End
   Begin VB.Label lab_par_6 
      BackStyle       =   0  'Transparent
      Caption         =   "Rap. rid. mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   3
      Left            =   120
      TabIndex        =   85
      Top             =   4380
      Width           =   1395
   End
   Begin VB.Label lab_par_6 
      BackStyle       =   0  'Transparent
      Caption         =   "Sovr.tà lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   5
      Left            =   1650
      TabIndex        =   84
      Top             =   4230
      Width           =   1395
   End
   Begin VB.Label lab_par_6 
      BackStyle       =   0  'Transparent
      Caption         =   "Fat.mol.enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   1
      Left            =   120
      TabIndex        =   83
      Top             =   4080
      Width           =   1395
   End
   Begin VB.Label lab_par_6 
      BackStyle       =   0  'Transparent
      Caption         =   "Imp. giro enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   2
      Left            =   120
      TabIndex        =   82
      Top             =   4230
      Width           =   1395
   End
   Begin VB.Label lab_par_5 
      BackStyle       =   0  'Transparent
      Caption         =   "Diam.pul.rol."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   8
      Left            =   3210
      TabIndex        =   81
      Top             =   3510
      Width           =   1395
   End
   Begin VB.Label lab_par_5 
      BackStyle       =   0  'Transparent
      Caption         =   "Fat.mol.enc.pul.rol."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   7
      Left            =   3210
      TabIndex        =   80
      Top             =   3360
      Width           =   1395
   End
   Begin VB.Label lab_par_5 
      BackStyle       =   0  'Transparent
      Caption         =   "Imp/gir enc.pul.rol."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   6
      Left            =   1650
      TabIndex        =   79
      Top             =   3660
      Width           =   1395
   End
   Begin VB.Label lab_par_5 
      BackStyle       =   0  'Transparent
      Caption         =   "Circ. lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   4
      Left            =   1650
      TabIndex        =   78
      Top             =   3360
      Width           =   1395
   End
   Begin VB.Label lab_par_5 
      BackStyle       =   0  'Transparent
      Caption         =   "Rap. rid. mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   3
      Left            =   120
      TabIndex        =   77
      Top             =   3660
      Width           =   1395
   End
   Begin VB.Label lab_par_5 
      BackStyle       =   0  'Transparent
      Caption         =   "Sovr.tà lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   5
      Left            =   1650
      TabIndex        =   76
      Top             =   3510
      Width           =   1395
   End
   Begin VB.Label lab_par_5 
      BackStyle       =   0  'Transparent
      Caption         =   "Fat.mol.enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   1
      Left            =   120
      TabIndex        =   75
      Top             =   3360
      Width           =   1395
   End
   Begin VB.Label lab_par_5 
      BackStyle       =   0  'Transparent
      Caption         =   "Imp. giro enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   2
      Left            =   120
      TabIndex        =   74
      Top             =   3510
      Width           =   1395
   End
   Begin VB.Label lab_par_4 
      BackStyle       =   0  'Transparent
      Caption         =   "Diam.ruo.mis"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   8
      Left            =   3210
      TabIndex        =   73
      Top             =   2790
      Width           =   1395
   End
   Begin VB.Label lab_par_4 
      BackStyle       =   0  'Transparent
      Caption         =   "Fat.mol.enc.ruo.mis"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   7
      Left            =   3210
      TabIndex        =   72
      Top             =   2640
      Width           =   1395
   End
   Begin VB.Label lab_par_4 
      BackStyle       =   0  'Transparent
      Caption         =   "Imp/gir enc.ruo.mis"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   6
      Left            =   1650
      TabIndex        =   71
      Top             =   2940
      Width           =   1395
   End
   Begin VB.Label lab_par_4 
      BackStyle       =   0  'Transparent
      Caption         =   "Circ. lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   4
      Left            =   1650
      TabIndex        =   70
      Top             =   2640
      Width           =   1395
   End
   Begin VB.Label lab_par_4 
      BackStyle       =   0  'Transparent
      Caption         =   "Rap. rid. mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   3
      Left            =   120
      TabIndex        =   69
      Top             =   2940
      Width           =   1395
   End
   Begin VB.Label lab_par_4 
      BackStyle       =   0  'Transparent
      Caption         =   "Sovr.tà lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   5
      Left            =   1650
      TabIndex        =   68
      Top             =   2790
      Width           =   1395
   End
   Begin VB.Label lab_par_3 
      BackStyle       =   0  'Transparent
      Caption         =   "Ang. sincr."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   9
      Left            =   3210
      TabIndex        =   67
      Top             =   2220
      Width           =   1395
   End
   Begin VB.Label lab_par_3 
      BackStyle       =   0  'Transparent
      Caption         =   "Giri max mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   8
      Left            =   3210
      TabIndex        =   66
      Top             =   2070
      Width           =   1395
   End
   Begin VB.Label lab_par_3 
      BackStyle       =   0  'Transparent
      Caption         =   "Circ. lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   7
      Left            =   3210
      TabIndex        =   65
      Top             =   1920
      Width           =   1395
   End
   Begin VB.Label lab_par_3 
      BackStyle       =   0  'Transparent
      Caption         =   "Rap.to rid. mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   6
      Left            =   1650
      TabIndex        =   64
      Top             =   2220
      Width           =   1395
   End
   Begin VB.Label lab_par_3 
      BackStyle       =   0  'Transparent
      Caption         =   "Fat.mol.enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   5
      Left            =   1650
      TabIndex        =   63
      Top             =   2070
      Width           =   1395
   End
   Begin VB.Label lab_par_3 
      BackStyle       =   0  'Transparent
      Caption         =   "Imp. giro enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   4
      Left            =   1650
      TabIndex        =   62
      Top             =   1920
      Width           =   1395
   End
   Begin VB.Label lab_par_3 
      BackStyle       =   0  'Transparent
      Caption         =   "Vel.tà max. di lin."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   3
      Left            =   90
      TabIndex        =   61
      Top             =   2220
      Width           =   1395
   End
   Begin VB.Label lab_par_3 
      BackStyle       =   0  'Transparent
      Caption         =   "Sovr.tà lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   2
      Left            =   90
      TabIndex        =   60
      Top             =   2070
      Width           =   1395
   End
   Begin VB.Label lab_par_3 
      BackStyle       =   0  'Transparent
      Caption         =   "Tem.acc.tag.cor"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   1
      Left            =   90
      TabIndex        =   59
      Top             =   1920
      Width           =   1395
   End
   Begin VB.Label lab_par_4 
      BackStyle       =   0  'Transparent
      Caption         =   "Fat.mol.enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   1
      Left            =   120
      TabIndex        =   58
      Top             =   2640
      Width           =   1395
   End
   Begin VB.Label lab_par_4 
      BackStyle       =   0  'Transparent
      Caption         =   "Imp. giro enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   2
      Left            =   120
      TabIndex        =   57
      Top             =   2790
      Width           =   1395
   End
   Begin VB.Label lab_par_2 
      BackStyle       =   0  'Transparent
      Caption         =   "Ang. sincr."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   9
      Left            =   3210
      TabIndex        =   56
      Top             =   1500
      Width           =   1395
   End
   Begin VB.Label lab_par_2 
      BackStyle       =   0  'Transparent
      Caption         =   "Giri max mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   8
      Left            =   3210
      TabIndex        =   55
      Top             =   1350
      Width           =   1395
   End
   Begin VB.Label lab_par_2 
      BackStyle       =   0  'Transparent
      Caption         =   "Circ. lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   7
      Left            =   3210
      TabIndex        =   54
      Top             =   1200
      Width           =   1395
   End
   Begin VB.Label lab_par_2 
      BackStyle       =   0  'Transparent
      Caption         =   "Rap. rid. mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   6
      Left            =   1650
      TabIndex        =   53
      Top             =   1500
      Width           =   1395
   End
   Begin VB.Label lab_par_2 
      BackStyle       =   0  'Transparent
      Caption         =   "Fat.mol.enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   5
      Left            =   1650
      TabIndex        =   52
      Top             =   1350
      Width           =   1395
   End
   Begin VB.Label lab_par_2 
      BackStyle       =   0  'Transparent
      Caption         =   "Imp. giro enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   4
      Left            =   1650
      TabIndex        =   51
      Top             =   1200
      Width           =   1395
   End
   Begin VB.Label lab_par_2 
      BackStyle       =   0  'Transparent
      Caption         =   "Vel.tà max. di lin."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   3
      Left            =   90
      TabIndex        =   50
      Top             =   1500
      Width           =   1395
   End
   Begin VB.Label lab_par_2 
      BackStyle       =   0  'Transparent
      Caption         =   "Sovr.tà lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   2
      Left            =   90
      TabIndex        =   49
      Top             =   1350
      Width           =   1395
   End
   Begin VB.Label lab_par_2 
      BackStyle       =   0  'Transparent
      Caption         =   "Tem.dec.tag.lun"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   1
      Left            =   90
      TabIndex        =   48
      Top             =   1200
      Width           =   1395
   End
   Begin VB.Label lab_par_1 
      BackStyle       =   0  'Transparent
      Caption         =   "Ang. sincr."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   10
      Left            =   4770
      TabIndex        =   47
      Top             =   480
      Width           =   1395
   End
   Begin VB.Label lab_par_1 
      BackStyle       =   0  'Transparent
      Caption         =   "Giri max mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   9
      Left            =   3210
      TabIndex        =   46
      Top             =   780
      Width           =   1395
   End
   Begin VB.Label lab_par_1 
      BackStyle       =   0  'Transparent
      Caption         =   "Circ. lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   8
      Left            =   3210
      TabIndex        =   45
      Top             =   630
      Width           =   1395
   End
   Begin VB.Label lab_par_1 
      BackStyle       =   0  'Transparent
      Caption         =   "Rap. rid. mot.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   7
      Left            =   3210
      TabIndex        =   44
      Top             =   480
      Width           =   1395
   End
   Begin VB.Label lab_par_1 
      BackStyle       =   0  'Transparent
      Caption         =   "Fat.mol.enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   6
      Left            =   1650
      TabIndex        =   43
      Top             =   780
      Width           =   1395
   End
   Begin VB.Label lab_par_1 
      BackStyle       =   0  'Transparent
      Caption         =   "Imp. giro enc.lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   5
      Left            =   1650
      TabIndex        =   42
      Top             =   630
      Width           =   1395
   End
   Begin VB.Label lab_par_1 
      BackStyle       =   0  'Transparent
      Caption         =   "Vel.tà max. di lin."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   4
      Left            =   1650
      TabIndex        =   41
      Top             =   480
      Width           =   1395
   End
   Begin VB.Label lab_par_1 
      BackStyle       =   0  'Transparent
      Caption         =   "Sovr.tà lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   3
      Left            =   90
      TabIndex        =   40
      Top             =   780
      Width           =   1395
   End
   Begin VB.Label lab_par_1 
      BackStyle       =   0  'Transparent
      Caption         =   "Tem.dec.tag.cor"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   2
      Left            =   90
      TabIndex        =   39
      Top             =   630
      Width           =   1395
   End
   Begin VB.Label lab_par_1 
      BackStyle       =   0  'Transparent
      Caption         =   "Tem.acc.tag.lun"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   225
      Index           =   1
      Left            =   90
      TabIndex        =   38
      Top             =   480
      Width           =   1395
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Errore inseguimento"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   18
      Left            =   6990
      TabIndex        =   37
      Top             =   5850
      Width           =   4965
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "18."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   18
      Left            =   6630
      TabIndex        =   36
      Top             =   5850
      Width           =   315
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Errore scrittura/lettura scheda"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   15
      Left            =   6990
      TabIndex        =   35
      Top             =   4170
      Width           =   4965
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "15."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   15
      Left            =   6630
      TabIndex        =   34
      Top             =   4170
      Width           =   315
   End
   Begin VB.Label lab_liv 
      Alignment       =   2  'Center
      BackColor       =   &H00FFFFFF&
      Caption         =   "1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   255
      Left            =   4170
      TabIndex        =   33
      Top             =   -30
      Width           =   405
   End
   Begin VB.Label lab_sch 
      BackStyle       =   0  'Transparent
      Caption         =   "SCHEDA DI TAGLIO DEL LIVELLO"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   60
      TabIndex        =   32
      Top             =   -30
      Width           =   3975
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Limite caricamento rampa di taglio"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   1
      Left            =   330
      TabIndex        =   31
      Top             =   300
      Width           =   5535
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "1."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   1
      Left            =   60
      TabIndex        =   30
      Top             =   300
      Width           =   315
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Limite caricamento rampa di posizionamento"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   2
      Left            =   330
      TabIndex        =   29
      Top             =   1020
      Width           =   5535
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "2."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   2
      Left            =   60
      TabIndex        =   28
      Top             =   1020
      Width           =   315
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Limite caricamento rampa a tem. acc. tag. cor."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   3
      Left            =   330
      TabIndex        =   27
      Top             =   1740
      Width           =   5535
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "3."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   3
      Left            =   60
      TabIndex        =   26
      Top             =   1740
      Width           =   315
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Limite caricamento BRM encoder linea"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   4
      Left            =   330
      TabIndex        =   25
      Top             =   2460
      Width           =   5535
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "4."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   4
      Left            =   60
      TabIndex        =   24
      Top             =   2460
      Width           =   315
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Limite caricamento BRM encoder ausiliario"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   5
      Left            =   330
      TabIndex        =   23
      Top             =   3180
      Width           =   5535
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "5."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   5
      Left            =   60
      TabIndex        =   22
      Top             =   3180
      Width           =   315
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Limite caricamento quota taglio misura"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   6
      Left            =   330
      TabIndex        =   21
      Top             =   3900
      Width           =   5535
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "6."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   6
      Left            =   60
      TabIndex        =   20
      Top             =   3900
      Width           =   315
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Limite caricamento quota taglio testa"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   7
      Left            =   330
      TabIndex        =   19
      Top             =   4620
      Width           =   5535
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "7."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   7
      Left            =   60
      TabIndex        =   18
      Top             =   4620
      Width           =   315
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Limite caricamento fondo scala rampe"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   8
      Left            =   330
      TabIndex        =   17
      Top             =   5340
      Width           =   5535
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "8."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   8
      Left            =   60
      TabIndex        =   16
      Top             =   5340
      Width           =   315
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Limite caricamento quota contatore lame"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   9
      Left            =   6960
      TabIndex        =   15
      Top             =   300
      Width           =   5535
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "9."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   9
      Left            =   6690
      TabIndex        =   14
      Top             =   300
      Width           =   315
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Limite caricamento quota di posizionamento"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   10
      Left            =   6990
      TabIndex        =   13
      Top             =   1020
      Width           =   4965
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "10."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   10
      Left            =   6630
      TabIndex        =   12
      Top             =   1020
      Width           =   315
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Limite caricamento quota fine zona sincrona"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   11
      Left            =   6990
      TabIndex        =   11
      Top             =   1740
      Width           =   4965
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "11."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   11
      Left            =   6630
      TabIndex        =   10
      Top             =   1740
      Width           =   315
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Limite caricamento quota inizio zona sincrona"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   12
      Left            =   6990
      TabIndex        =   9
      Top             =   2460
      Width           =   4965
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "12."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   12
      Left            =   6630
      TabIndex        =   8
      Top             =   2460
      Width           =   315
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Limite caricamento 2Fmax"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   13
      Left            =   6990
      TabIndex        =   7
      Top             =   3030
      Width           =   4965
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "13."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   13
      Left            =   6630
      TabIndex        =   6
      Top             =   3030
      Width           =   315
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Limite caricamento sovravelocità"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   14
      Left            =   6990
      TabIndex        =   5
      Top             =   3750
      Width           =   4965
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "14."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   14
      Left            =   6630
      TabIndex        =   4
      Top             =   3750
      Width           =   315
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Limite KP normalizzato per PIC"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   16
      Left            =   6990
      TabIndex        =   3
      Top             =   4410
      Width           =   4965
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "16."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   16
      Left            =   6630
      TabIndex        =   2
      Top             =   4410
      Width           =   315
   End
   Begin VB.Label lab_msg_all 
      BackStyle       =   0  'Transparent
      Caption         =   "Limite KI normalizzato per PIC"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   17
      Left            =   6990
      TabIndex        =   1
      Top             =   5130
      Width           =   4965
   End
   Begin VB.Label lab_num 
      BackStyle       =   0  'Transparent
      Caption         =   "17."
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   17
      Left            =   6630
      TabIndex        =   0
      Top             =   5130
      Width           =   315
   End
End
Attribute VB_Name = "dia_sch_tag"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
'Iciliouff 03-11-2003
Private WithEvents rmt_dia_sch_tag_sch As rmt
Attribute rmt_dia_sch_tag_sch.VB_VarHelpID = -1

Dim liv As Integer

Sub col_lab_msg_all(wrd168h As String, wrd169h As String)
Dim str As String
Dim i As Integer
Dim c As Control

If wrd168h + wrd169h = String$(Len(wrd168h) + Len(wrd169h), "0") Then
  lab_ris.Caption = "OK"
  lab_ris.ForeColor = COL_DARK_Green
Else
  lab_ris.Caption = "ERROR"
  lab_ris.ForeColor = COL_ALL_Red
End If
str = rev_str(wrd168h)
For i = 1 To Len(str)
  If val(Mid$(str, i, 1)) = 1 Then
    lab_msg_all(i).ForeColor = vbRed
    lab_num(i).ForeColor = vbRed
    For Each c In Me
      If TypeOf c Is Label And c.name = "lab_par_" & CStr(i) Then
          c.ForeColor = vbRed
      End If
    Next c
  Else
    lab_msg_all(i).ForeColor = vbBlack
    lab_num(i).ForeColor = vbBlack
  End If
Next i

str = rev_str(wrd169h)

If val(Mid$(wrd169h, Len(wrd169h), 1)) = 1 Then
  lab_msg_all(i).ForeColor = vbRed
  lab_num(i).ForeColor = vbRed
  For Each c In Me
    If TypeOf c Is Label And c.name = "lab_par_" & CStr(i) Then
        c.ForeColor = vbRed
    End If
  Next c
Else
  lab_msg_all(i).ForeColor = vbBlack
  lab_num(i).ForeColor = vbBlack
End If
i = i + 1
If val(Mid$(wrd169h, (Len(wrd169h) - 1), 1)) = 1 Then
  lab_msg_all(i).ForeColor = vbRed
  lab_num(i).ForeColor = vbRed
Else
  lab_msg_all(i).ForeColor = vbBlack
  lab_num(i).ForeColor = vbBlack
End If
End Sub


Sub ini_form()
Dim c As Control

For Each c In Me
  If TypeOf c Is Label And c.name <> "lab_sch" And c.name <> "lab_liv" Then c.ForeColor = vbBlack
Next c
lab_ris.Caption = ""
End Sub

Private Sub eve_key_F1(Shift As Integer)
Select Case Shift
  Case 0
  eve_key.chg pag_all
  Case Else
End Select
End Sub

Private Sub eve_key_F10(Shift As Integer)
Select Case Shift
  Case 0
  eve_key.chg pag_pro
  Case Else
End Select
End Sub


Private Sub eve_key_F3(Shift As Integer)
Select Case Shift
  Case 0
  ' avvio della comunicazione
  If val(lab_liv.Caption) <= val(par_mac.car_tec.num_liv_tag) Then
'      CASO_RCV_SCH_TAG = DIAGNOST
      num_com_sch_tag = 1
      'Iciliouff 03-11-2003
      If rmt_dia_sch_tag_sch.esegui(tip_cmd.LETTURA_STATO, GetTickCount(), val(lab_liv.Caption), 0) Then
        messaggio msg("inv_par_sch_tag"), False
      Else
        messaggio msg("imp_ese_ope"), True
      End If
'      snd_sch_tag val(lab_liv.Caption), LETTURA_STATO
  Else
      messaggio msg("sch_tag_non_pre"), True, False
  End If
  Case Else
End Select
End Sub

Private Sub Form_Activate()
ini_form
If pag_pre.name = "all_qe1" Then lab_liv = 1
If pag_pre.name = "all_qe2" Then lab_liv = 2
If pag_pre.name = "all_qe3" Then lab_liv = 3
End Sub

Private Sub Form_Load()
Debug.Print "Inizio Load " & Me.name
Set eve_key = New key
'Iciliouff 03-11-2003
Set rmt_dia_sch_tag_sch = New rmt
rmt_dia_sch_tag_sch.set_plc_ute (2)
rmt_dia_sch_tag_sch.name = "dia_sch_tag_sch"
eve_key.create Me
Carica Me
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub




Private Sub rmt_dia_sch_tag_sch_retbad(err As String)
'Gestione risposta positiva da scheda di taglio
  
  If err <> "" Then messaggio err, True
  rmt_dia_sch_tag_sch.resetta

End Sub

Private Sub rmt_dia_sch_tag_sch_retok(s As String)
'Gestione risposta positiva da scheda di taglio
  
  If s <> "" Then messaggio s, False
  rmt_dia_sch_tag_sch.resetta

End Sub


