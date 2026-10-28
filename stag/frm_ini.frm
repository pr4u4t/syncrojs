VERSION 5.00
Object = "{6B7E6392-850A-101B-AFC0-4210102A8DA7}#1.3#0"; "COMCTL32.OCX"
Begin VB.Form Frm_ini 
   BackColor       =   &H80000005&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   4155
   ClientLeft      =   930
   ClientTop       =   2235
   ClientWidth     =   7065
   ControlBox      =   0   'False
   Icon            =   "frm_ini.frx":0000
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form2"
   Moveable        =   0   'False
   ScaleHeight     =   4155
   ScaleWidth      =   7065
   ShowInTaskbar   =   0   'False
   Begin VB.Frame Frame1 
      BackColor       =   &H80000005&
      BorderStyle     =   0  'None
      Height          =   4110
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   7200
      Begin ComctlLib.ProgressBar prg_bar_ini 
         Height          =   130
         Left            =   45
         TabIndex        =   12
         Top             =   3900
         Visible         =   0   'False
         Width           =   7005
         _ExtentX        =   12356
         _ExtentY        =   238
         _Version        =   327682
         Appearance      =   0
         Max             =   104
      End
      Begin VB.TextBox Text1 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         Enabled         =   0   'False
         Height          =   555
         Left            =   0
         TabIndex        =   13
         TabStop         =   0   'False
         Top             =   0
         Width           =   75
      End
      Begin VB.Label Commessa 
         BackColor       =   &H80000009&
         Caption         =   "YYccc"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   6120
         TabIndex        =   18
         Top             =   1710
         Width           =   855
      End
      Begin VB.Label LblCommessa 
         Alignment       =   1  'Right Justify
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         Caption         =   "Order"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   11.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   5415
         TabIndex        =   17
         Top             =   1680
         Width           =   570
      End
      Begin VB.Label JOB 
         BackColor       =   &H80000009&
         Caption         =   "XXXX"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   6120
         TabIndex        =   16
         Top             =   1470
         Width           =   855
      End
      Begin VB.Label LblJOB 
         Alignment       =   1  'Right Justify
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         Caption         =   "Job"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   11.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   5640
         TabIndex        =   15
         Top             =   1440
         Width           =   345
      End
      Begin VB.Label Vrs_Nmb 
         BackColor       =   &H80000009&
         Caption         =   "4.1.7"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   11.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   6120
         TabIndex        =   14
         Top             =   1200
         Width           =   855
      End
      Begin VB.Image img_title 
         Height          =   300
         Left            =   -4950
         Picture         =   "frm_ini.frx":030A
         Top             =   75
         Width           =   11970
      End
      Begin VB.Image img_master 
         Appearance      =   0  'Flat
         Height          =   1695
         Left            =   420
         Picture         =   "frm_ini.frx":BE7C
         Stretch         =   -1  'True
         Top             =   765
         Width           =   1695
      End
      Begin VB.Label Label2 
         Alignment       =   1  'Right Justify
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         Caption         =   "with Service Pack 6"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   11.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   270
         Left            =   4800
         TabIndex        =   11
         Top             =   2460
         Width           =   2070
      End
      Begin VB.Label Label1 
         BackColor       =   &H80000005&
         Caption         =   "Please wait"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   165
         Left            =   120
         TabIndex        =   10
         Top             =   3480
         Width           =   1335
      End
      Begin VB.Label lab_cod 
         BackColor       =   &H80000005&
         Caption         =   "55060 Monsagrati (Lucca)"
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
         Left            =   4380
         TabIndex        =   9
         Top             =   3330
         Width           =   2130
      End
      Begin VB.Label lbl_via 
         BackColor       =   &H80000005&
         Caption         =   "via Provinciale per Camaiore, 27/28"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Left            =   4380
         TabIndex        =   8
         Top             =   3120
         Width           =   2640
      End
      Begin VB.Label lblCopyright 
         BackColor       =   &H80000005&
         Caption         =   "Copyright © 2000"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Left            =   4380
         TabIndex        =   3
         Top             =   2700
         Visible         =   0   'False
         Width           =   2100
      End
      Begin VB.Label lblCompany 
         BackColor       =   &H80000005&
         Caption         =   "Fosber SpA"
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
         Left            =   4380
         TabIndex        =   2
         Top             =   2910
         Width           =   1380
      End
      Begin VB.Label lbl_Wrn 
         BackColor       =   &H80000005&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Left            =   120
         TabIndex        =   1
         Top             =   3660
         Width           =   4000
      End
      Begin VB.Label lblVersion 
         Alignment       =   1  'Right Justify
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         Caption         =   "Versione"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   11.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   270
         Left            =   5040
         TabIndex        =   4
         Top             =   1200
         Width           =   945
      End
      Begin VB.Label lblPlatform 
         Alignment       =   1  'Right Justify
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         Caption         =   "Windows NT 4.0"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   15.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         Left            =   4440
         TabIndex        =   5
         Top             =   2100
         Width           =   2415
      End
      Begin VB.Label lblProductName 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         Caption         =   "STAG"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   32.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   645
         Left            =   2460
         TabIndex        =   7
         Top             =   1110
         Width           =   1800
      End
      Begin VB.Label lblCompanyProduct 
         AutoSize        =   -1  'True
         BackColor       =   &H80000009&
         BackStyle       =   0  'Transparent
         Caption         =   "Cut Off Control"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   2415
         TabIndex        =   6
         Top             =   690
         Width           =   2610
      End
   End
End
Attribute VB_Name = "Frm_ini"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Option Explicit
'andreacs 30-03-01   With service pack 6
Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
  skip_db_lang = Shift = vbShiftMask 'se premo shift all'avvio non rigenero il db delle lingue
  If KeyCode = vbKeyEscape Then
    End
  End If
End Sub

Private Sub Form_KeyPress(KeyAscii As Integer)
    'Unload Me
End Sub

Private Sub Form_Load()
    'Arturo uff 13-11-02
    'Debug.Print "Inizio Load " & Me.name
    'lblVersion.Caption = "Vers. " & app.Major & "." & app.Minor & "." & app.Revision
    lblProductName.Caption = app.Title
    Ridim Me
    Center Me
    dimensiona Me
    img_title.Left = Int(img_title.Left - (14 / 10) * (img_title.Left * (pro_set.PercX - 100) / 100))
    img_title.Width = img_title.Width * pro_set.PercX / 100
End Sub

Private Sub Frame1_Click()
'    Unload Me
End Sub




