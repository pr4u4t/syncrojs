VERSION 5.00
Object = "{00028C01-0000-0000-0000-000000000046}#1.0#0"; "DBGRID32.OCX"
Begin VB.Form Pattern 
   Appearance      =   0  'Flat
   BackColor       =   &H80000005&
   ClientHeight    =   7905
   ClientLeft      =   -630
   ClientTop       =   600
   ClientWidth     =   11880
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   ScaleHeight     =   7905
   ScaleWidth      =   11880
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox Ofs_Cut 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "OFS_CUT"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   330
      Left            =   7800
      MaxLength       =   5
      TabIndex        =   12
      Top             =   5790
      Width           =   3400
   End
   Begin VB.Data Data1 
      Connect         =   "dBASE III;"
      DatabaseName    =   "C:\usr\stag\dati"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   345
      Left            =   2520
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   1  'Dynaset
      RecordSource    =   "select * from Pattern order by codice"
      Top             =   6480
      Visible         =   0   'False
      Width           =   1500
   End
   Begin VB.TextBox CODICE 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "CODICE"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   330
      Left            =   7830
      MaxLength       =   16
      TabIndex        =   28
      Top             =   420
      Width           =   3400
   End
   Begin VB.TextBox L10 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "L10"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   330
      Left            =   7830
      MaxLength       =   5
      TabIndex        =   11
      Top             =   5400
      Width           =   3400
   End
   Begin VB.TextBox L2 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "L2"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   330
      Left            =   7830
      MaxLength       =   5
      TabIndex        =   3
      Top             =   2070
      Width           =   3400
   End
   Begin VB.TextBox L4 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "L4"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   330
      Left            =   7830
      MaxLength       =   5
      TabIndex        =   5
      Top             =   2910
      Width           =   3400
   End
   Begin VB.TextBox L3 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "L3"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   330
      Left            =   7830
      MaxLength       =   5
      TabIndex        =   4
      Top             =   2490
      Width           =   3400
   End
   Begin VB.TextBox CONTRASTO 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "CONTRASTO"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   330
      Left            =   7830
      MaxLength       =   1
      TabIndex        =   0
      Top             =   810
      Width           =   3405
   End
   Begin VB.TextBox L1 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "L1"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   330
      Left            =   7830
      MaxLength       =   5
      TabIndex        =   2
      Top             =   1650
      Width           =   3400
   End
   Begin VB.TextBox L8 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "L8"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   330
      Left            =   7830
      MaxLength       =   5
      TabIndex        =   9
      Top             =   4590
      Width           =   3400
   End
   Begin VB.TextBox L5 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "L5"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   330
      Left            =   7830
      MaxLength       =   5
      TabIndex        =   6
      Top             =   3330
      Width           =   3400
   End
   Begin VB.TextBox L7 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "L7"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   330
      Left            =   7830
      MaxLength       =   5
      TabIndex        =   8
      Top             =   4170
      Width           =   3400
   End
   Begin VB.TextBox PRI_LET_FC 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "PRI_LET_FC"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   330
      Left            =   7830
      MaxLength       =   1
      TabIndex        =   1
      Top             =   1230
      Width           =   3400
   End
   Begin VB.TextBox L6 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "L6"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   330
      Left            =   7830
      MaxLength       =   5
      TabIndex        =   7
      Top             =   3750
      Width           =   3400
   End
   Begin VB.TextBox L9 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      DataField       =   "L9"
      DataSource      =   "Data1"
      BeginProperty Font 
         Name            =   "Terminal"
         Size            =   12
         Charset         =   255
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   330
      Left            =   7830
      MaxLength       =   5
      TabIndex        =   10
      Top             =   5010
      Width           =   3400
   End
   Begin MSDBGrid.DBGrid lis 
      Bindings        =   "Pattern.frx":0000
      Height          =   5895
      Left            =   0
      OleObjectBlob   =   "Pattern.frx":0010
      TabIndex        =   13
      Top             =   480
      Width           =   3975
   End
   Begin VB.Line Line1 
      Index           =   3
      X1              =   10500
      X2              =   4200
      Y1              =   6120
      Y2              =   6120
   End
   Begin VB.Label L_Ofs_Pattern 
      Alignment       =   1  'Right Justify
      BackStyle       =   0  'Transparent
      Caption         =   "OffSet Taglio"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   225
      Left            =   4020
      TabIndex        =   29
      Top             =   5880
      Width           =   3495
      WordWrap        =   -1  'True
   End
   Begin VB.Label L_DistIniPatf5tacca 
      Alignment       =   1  'Right Justify
      BackStyle       =   0  'Transparent
      Caption         =   "Distanza inizio pattern - fine 5° tacca"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   225
      Left            =   4020
      TabIndex        =   27
      Top             =   5520
      Width           =   3495
      WordWrap        =   -1  'True
   End
   Begin VB.Line Line1 
      Index           =   13
      X1              =   10500
      X2              =   4140
      Y1              =   5760
      Y2              =   5760
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "GESTIONE PATTERN"
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
      Left            =   30
      TabIndex        =   15
      Top             =   0
      Width           =   7965
   End
   Begin VB.Image Image3 
      Height          =   330
      Left            =   0
      Picture         =   "Pattern.frx":09DB
      Stretch         =   -1  'True
      Top             =   0
      Width           =   11970
   End
   Begin VB.Label L_DistIniPatf1tacca 
      Alignment       =   1  'Right Justify
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Distanza inizio pattern - fine 1° tacca"
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
      Height          =   255
      Left            =   4020
      TabIndex        =   26
      Top             =   2130
      Width           =   3495
      WordWrap        =   -1  'True
   End
   Begin VB.Label L_DistIniPatf2tacca 
      Alignment       =   1  'Right Justify
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Distanza inizio pattern - fine 2° tacca"
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
      Height          =   255
      Left            =   4020
      TabIndex        =   25
      Top             =   2970
      Width           =   3495
      WordWrap        =   -1  'True
   End
   Begin VB.Label L_DistIniPati2tacca 
      Alignment       =   1  'Right Justify
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Distanza inizio pattern - inizio 2° tacca"
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
      Height          =   255
      Left            =   4020
      TabIndex        =   24
      Top             =   2550
      Width           =   3495
      WordWrap        =   -1  'True
   End
   Begin VB.Label L_DistIniPati1tacca 
      Alignment       =   1  'Right Justify
      BackStyle       =   0  'Transparent
      Caption         =   "Distanza inizio pattern - inizio 1° tacca"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   255
      Left            =   4020
      TabIndex        =   23
      Top             =   1680
      Width           =   3495
      WordWrap        =   -1  'True
   End
   Begin VB.Label L_DistIniPati4tacca 
      Alignment       =   1  'Right Justify
      BackStyle       =   0  'Transparent
      Caption         =   "Distanza inizio pattern - inizio 4° tacca"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   285
      Left            =   4020
      TabIndex        =   22
      Top             =   4260
      Width           =   3495
      WordWrap        =   -1  'True
   End
   Begin VB.Label L_DistIniPatf3tacca 
      Alignment       =   1  'Right Justify
      BackStyle       =   0  'Transparent
      Caption         =   "Distanza inizio pattern - fine 3° tacca"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   225
      Left            =   4020
      TabIndex        =   21
      Top             =   3840
      Width           =   3495
      WordWrap        =   -1  'True
   End
   Begin VB.Label L_DistIniPati3tacca 
      Alignment       =   1  'Right Justify
      BackStyle       =   0  'Transparent
      Caption         =   "Distanza inizio pattern - inizio 3° tacca"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   255
      Left            =   4020
      TabIndex        =   20
      Top             =   3390
      Width           =   3495
      WordWrap        =   -1  'True
   End
   Begin VB.Label L_contrasto 
      Alignment       =   1  'Right Justify
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Contrasto fotocellula rilevamento pattern"
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
      Height          =   210
      Left            =   4020
      TabIndex        =   19
      Top             =   870
      Width           =   3495
      WordWrap        =   -1  'True
   End
   Begin VB.Label L_codice 
      Alignment       =   1  'Right Justify
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Codice Pattern"
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
      Height          =   255
      Left            =   4020
      TabIndex        =   18
      Top             =   480
      Width           =   3495
      WordWrap        =   -1  'True
   End
   Begin VB.Label L_DistIniPatf4tacca 
      Alignment       =   1  'Right Justify
      BackStyle       =   0  'Transparent
      Caption         =   "Distanza inizio pattern - fine 4° tacca"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   225
      Left            =   4020
      TabIndex        =   17
      Top             =   4680
      Width           =   3495
      WordWrap        =   -1  'True
   End
   Begin VB.Label L_PrimaLetFTC 
      Alignment       =   1  'Right Justify
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Prima lettura fotocellula"
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
      Height          =   255
      Left            =   4020
      TabIndex        =   16
      Top             =   1290
      Width           =   3495
      WordWrap        =   -1  'True
   End
   Begin VB.Line Line1 
      Index           =   0
      X1              =   10530
      X2              =   4170
      Y1              =   720
      Y2              =   720
   End
   Begin VB.Line Line1 
      Index           =   1
      X1              =   10530
      X2              =   4170
      Y1              =   1140
      Y2              =   1140
   End
   Begin VB.Line Line1 
      Index           =   2
      X1              =   10530
      X2              =   4170
      Y1              =   1560
      Y2              =   1560
   End
   Begin VB.Line Line1 
      Index           =   4
      X1              =   10530
      X2              =   4170
      Y1              =   1980
      Y2              =   1980
   End
   Begin VB.Line Line1 
      Index           =   5
      X1              =   10530
      X2              =   4170
      Y1              =   2400
      Y2              =   2400
   End
   Begin VB.Line Line1 
      Index           =   6
      X1              =   10530
      X2              =   4170
      Y1              =   2820
      Y2              =   2820
   End
   Begin VB.Line Line1 
      Index           =   7
      X1              =   10530
      X2              =   4170
      Y1              =   3240
      Y2              =   3240
   End
   Begin VB.Line Line1 
      Index           =   8
      X1              =   10530
      X2              =   4170
      Y1              =   3660
      Y2              =   3660
   End
   Begin VB.Line Line1 
      Index           =   9
      X1              =   10530
      X2              =   4170
      Y1              =   4080
      Y2              =   4080
   End
   Begin VB.Line Line1 
      Index           =   10
      X1              =   10530
      X2              =   4170
      Y1              =   4500
      Y2              =   4500
   End
   Begin VB.Line Line1 
      Index           =   11
      X1              =   10530
      X2              =   4170
      Y1              =   4920
      Y2              =   4920
   End
   Begin VB.Line Line1 
      Index           =   12
      X1              =   10500
      X2              =   4140
      Y1              =   5340
      Y2              =   5340
   End
   Begin VB.Label L_DistIniPati5tacca 
      Alignment       =   1  'Right Justify
      BackStyle       =   0  'Transparent
      Caption         =   "Distanza inizio pattern - inizio 5° tacca"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   225
      Left            =   4020
      TabIndex        =   14
      Top             =   5100
      Width           =   3495
      WordWrap        =   -1  'True
   End
End
Attribute VB_Name = "Pattern"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim mod_pat As Boolean 'flag di sincronizzazione per le operazioni sul dbase
Dim flg_rev As Boolean 'True se sto tornando al controllo precedente
Dim ult_con As Long    'memorizzo l'handle dell'ultimo controllo per poterci tornare in caso di errore
Dim flg_pri_pas As Integer 'flag di memorizzazione per riempire i vari campi con i dati della prima onda di quel tipo memorizzata
Dim nuo_pat As Boolean  'true se sto inserendo una nuova onda
Dim foto_pat As Recordset     'Fotografia della riga del db che si desidera modificare
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1

Function err_mis_pat() As Boolean
'Ritorna se c'è un errore nelle misure delle tacche che costituiscono il pattern (L1..L10)
Dim pun As Integer, i As Integer
Dim Lx As Integer, Lmax As Integer, Lprec As Integer

  For pun = 1 To 10
    'Cerco la prima tacca nulla e ricavo la lunghezza del pattern
    Lx = val(Choose(pun, L1, L2, L3, L4, L5, L6, L7, L8, L9, L10))
    If Lx > Lmax Then Lmax = Lx
    If Lx = 0 Then Exit For
  Next
  If pun = 1 Then err_mis_pat = True: Exit Function
  For i = pun To 10
    'Azzero le tacche successive alla prima nulla
    If i = 1 Then L1 = Format$(0)
    If i = 2 Then L2 = Format$(0)
    If i = 3 Then L3 = Format$(0)
    If i = 4 Then L4 = Format$(0)
    If i = 5 Then L5 = Format$(0)
    If i = 6 Then L6 = Format$(0)
    If i = 7 Then L7 = Format$(0)
    If i = 8 Then L8 = Format$(0)
    If i = 9 Then L9 = Format$(0)
    If i = 10 Then L10 = Format$(0)
  Next
  For i = 1 To pun - 1
    'Controllo se le tacche specificate sono sotto la lunghezza minima consentita
    Lx = val(Choose(i, L1, L2, L3, L4, L5, L6, L7, L8, L9, L10))
    If Lx < (5 / 64 * Lmax) Then
'      ult_con = Switch(i = 1, L1.hwnd, i = 2, L2.hwnd, i = 3, L3.hwnd, i = 4, L4.hwnd, i = 5, L5.hwnd, i = 6, L6.hwnd, i = 7, L7.hwnd, i = 8, L8.hwnd, i = 9, L9.hwnd, i = 10, L10.hwnd)
      err_mis_pat = True: Exit Function
    End If
    If i > 1 Then
      'Controllo che le tacche specificate abbiano valori incrementali in relazione al loro indice
      Lprec = val(Choose(i - 1, L1, L2, L3, L4, L5, L6, L7, L8, L9, L10))
      If Lx <= Lprec Then err_mis_pat = True: Exit Function
    End If
  Next

End Function
Private Sub abi_cam_ang(abi As Boolean)

'txt_gra_ang.Visible = abi
'txt_mel_ang.Visible = abi
'txt_mta_ang.Visible = abi
'txt_pre_ang.Visible = abi
'txt_spe_ang.Visible = abi
'txt_vec_ang.Visible = abi
'txt_vep_ang.Visible = abi
'
'If abi Then
'  txt_gra_ang.Text = GRAMMATURA.Text
'  txt_mel_ang.Text = con_met_to_fet(MTRALUBCON.Text)
'  txt_mta_ang.Text = con_met_to_fet(MTRAAFCON.Text)
'  txt_pre_ang.Text = con_mm_to_inc(Format$(val(PRESSIONE.Text) \ 10, "#####0.0"))
'  txt_spe_ang.Text = con_mm_to_inc(Format$(val(SPESSORE.Text) \ 10, "#####0.0"))
'  txt_vec_ang.Text = con_met_to_fet(VELDICAMBI.Text)
'  txt_vep_ang.Text = con_met_to_fet(VELDIPROD.Text)
'End If

End Sub

Sub abi_con_pat(abi As Boolean)
'Iciliouff 20-11-2003
Dim c As TextBox
On Local Error GoTo gesterr

  For Each c In Me
    c.Enabled = abi
  Next c
  Exit Sub

gesterr:
  log_str "abi_con_pat: Errore in Pattern =" & str(err.Number) & " " & err.Description, MED_PRIORITY, log_gen
  Resume Next
End Sub

Sub val_ins_pat(hwnd As Long, Optional error As Boolean)
'determina se i valori inseriti nell'ultimo controllo utilizzato (ult_con) sono validi
Static cod As String
Dim ss1 As Recordset

error = False

If ult_con = 0 Then ult_con = hwnd
If ult_con = hwnd Then Exit Sub       'eseguo i calcoli solo se ho cambiato controllo

'eseguo i controlli solo quando sto andando avanti e non quando torno indietro
If flg_rev = True And ult_con <> CODICE.hwnd Then
  flg_rev = False
  ult_con = hwnd
  Exit Sub
End If

'sezione di validazione
Select Case ult_con
  'Codice dell'ordine
  Case CODICE.hwnd
    If Len(CODICE.Text) = 0 Then
      messaggio msg("ins_pat"), is_All:=True
      CODICE.ForeColor = vbRed
      CODICE.SetFocus
      error = True
      Exit Sub
    Else
      'se inserisco un nuovo pattern controllo nel db se sia già stato inserito; se sì, carico i campi di tale pattern
      If nuo_pat Then
        cod = CODICE.Text
        Set ss1 = db.OpenRecordset("select * from PATTERN where Ucase(CODICE)='" + UCase$(cod) + "'", dbOpenSnapshot)
        If ss1.RecordCount > 0 Then
          If Data1.Recordset.EditMode <> dbEditNone Then
            Data1.Recordset.CancelUpdate
          End If
          Data1.Recordset.FindFirst "CODICE='" + cod + "' "
          mod_pat = True
          nuo_pat = False
        End If
        ss1.Close
      End If
    End If
    
    If flg_rev Then 'impedisco di andare indietro dal primo controllo
      CODICE.SetFocus
      flg_rev = False
      error = True
      Exit Sub
    End If
  'controllo esistenza del tipo di carta
  Case CONTRASTO.hwnd
    'il campo contrasto deve essere numerico compreso fra 0 e 4 caratteri
    CONTRASTO.Text = CStr(val(CONTRASTO.Text))
    If val(CONTRASTO.Text) < MIN_NUM_CTR Or val(CONTRASTO.Text) > MAX_NUM_CTR Then
      messaggio msg("val_min") + " = " + CStr(MIN_NUM_CTR) + "; " + msg("val_max") + " = " + CStr(MAX_NUM_CTR), is_All:=True
      CONTRASTO.ForeColor = vbRed
      CONTRASTO.SetFocus
      error = True
      Exit Sub
    End If
  Case PRI_LET_FC.hwnd
    PRI_LET_FC.Text = CStr(val(PRI_LET_FC.Text))
    If val(PRI_LET_FC.Text) < MIN_LET_FTC Or val(PRI_LET_FC.Text) > MAX_LET_FTC Then
      messaggio msg("val_min") + " = " + CStr(MIN_LET_FTC) + "; " + msg("val_max") + " = " + CStr(MAX_LET_FTC), is_All:=True
      PRI_LET_FC.ForeColor = vbRed
      PRI_LET_FC.SetFocus
      error = True
      Exit Sub
    End If
  Case L1.hwnd
    L1.Text = CStr(val(L1.Text))
    If val(L1.Text) <= 0 Or val(L1.Text) > MAX_LEN_PAT Then
      messaggio msg("val_min") + " > 0; " + msg("val_max") + " = " + CStr(MAX_LEN_PAT), is_All:=True
      L1.ForeColor = vbRed
      L1.SetFocus
      error = True
      Exit Sub
    End If
  Case L2.hwnd
    L2.Text = CStr(val(L2.Text))
    If val(L2.Text) <> 0 And (val(L2.Text) <= val(L1.Text) Or val(L2.Text) > MAX_LEN_PAT) Then
      messaggio msg("val_min") + " > " + L1.Text + "; " + msg("val_max") + " = " + CStr(MAX_LEN_PAT), is_All:=True
      L2.ForeColor = vbRed
      L2.SetFocus
      error = True
      Exit Sub
    End If
  Case L3.hwnd
    L3.Text = CStr(val(L3.Text))
    If val(L3.Text) <> 0 And (val(L3.Text) <= val(L2.Text) Or val(L3.Text) > MAX_LEN_PAT) Then
      messaggio msg("val_min") + " > " + L2.Text + "; " + msg("val_max") + " = " + CStr(MAX_LEN_PAT), is_All:=True
      L3.ForeColor = vbRed
      L3.SetFocus
      error = True
      Exit Sub
    End If
  Case L4.hwnd
    L4.Text = CStr(val(L4.Text))
    If val(L4.Text) <> 0 And (val(L4.Text) <= val(L3.Text) Or val(L4.Text) > MAX_LEN_PAT) Then
      messaggio msg("val_min") + " > " + L3.Text + "; " + msg("val_max") + " = " + CStr(MAX_LEN_PAT), is_All:=True
      L4.ForeColor = vbRed
      L4.SetFocus
      error = True
      Exit Sub
    End If
  Case L5.hwnd
    L5.Text = CStr(val(L5.Text))
    If val(L5.Text) <> 0 And (val(L5.Text) <= val(L4.Text) Or val(L5.Text) > MAX_LEN_PAT) Then
      messaggio msg("val_min") + " > " + L4.Text + "; " + msg("val_max") + " = " + CStr(MAX_LEN_PAT), is_All:=True
      L5.ForeColor = vbRed
      L5.SetFocus
      error = True
      Exit Sub
    End If
  Case L6.hwnd
    L6.Text = CStr(val(L6.Text))
    If val(L6.Text) <> 0 And (val(L6.Text) <= val(L5.Text) Or val(L6.Text) > MAX_LEN_PAT) Then
      messaggio msg("val_min") + " > " + L5.Text + "; " + msg("val_max") + " = " + CStr(MAX_LEN_PAT), is_All:=True
      L6.ForeColor = vbRed
      L6.SetFocus
      error = True
      Exit Sub
    End If
  Case L7.hwnd
    L7.Text = CStr(val(L7.Text))
    If val(L7.Text) <> 0 And (val(L7.Text) <= val(L6.Text) Or val(L7.Text) > MAX_LEN_PAT) Then
      messaggio msg("val_min") + " > " + L6.Text + "; " + msg("val_max") + " = " + CStr(MAX_LEN_PAT), is_All:=True
      L7.ForeColor = vbRed
      L7.SetFocus
      error = True
      Exit Sub
    End If
  Case L8.hwnd
    L8.Text = CStr(val(L8.Text))
    If val(L8.Text) <> 0 And (val(L8.Text) <= val(L7.Text) Or val(L8.Text) > MAX_LEN_PAT) Then
      messaggio msg("val_min") + " > " + L1.Text + "; " + msg("val_max") + " = " + CStr(MAX_LEN_PAT), is_All:=True
      L8.ForeColor = vbRed
      L8.SetFocus
      error = True
      Exit Sub
    End If
  Case L9.hwnd
    L9.Text = CStr(val(L9.Text))
    If val(L9.Text) <> 0 And (val(L9.Text) <= val(L8.Text) Or val(L9.Text) > MAX_LEN_PAT) Then
      messaggio msg("val_min") + " > " + L1.Text + "; " + msg("val_max") + " = " + CStr(MAX_LEN_PAT), is_All:=True
      L9.ForeColor = vbRed
      L9.SetFocus
      error = True
      Exit Sub
    End If
  Case L10.hwnd
    L10.Text = CStr(val(L10.Text))
    If val(L10.Text) <> 0 And (val(L10.Text) <= val(L9.Text) Or val(L10.Text) > MAX_LEN_PAT) Then
      messaggio msg("val_min") + " > " + L9.Text + "; " + msg("val_max") + " = " + CStr(MAX_LEN_PAT), is_All:=True
      L10.ForeColor = vbRed
      L10.SetFocus
      error = True
      Exit Sub
    End If
  'Robertouff 26-11-2007
  Case Ofs_Cut.hwnd
    Ofs_Cut.Text = CStr(val(Ofs_Cut.Text))
  
  Case Else
End Select

'se arrivo qui, significa che le validazioni hanno avuto successo, quindi aggiorno
'il puntatore all'ultimo controllo attivo
ult_con = hwnd

End Sub



Private Function dbval(v As Variant) As Double
   If IsNull(v) Then
     dbval = 0
   Else
     dbval = val(v)
   End If
End Function
 



Function val_pat() As Boolean
'restituisce false se manca l'inserimento di almeno 1 valore fondamentale
val_pat = True
 
  If Trim$(CODICE) = "" Or Trim$(CONTRASTO) = "" Or Trim$(PRI_LET_FC) = "" Or _
     Trim$(L1) = "" Or Trim$(L2) = "" Or Trim$(L3) = "" Or Trim$(L4) = "" Or Trim$(L5) = "" Or _
     Trim$(L6) = "" Or Trim$(L7) = "" Or Trim$(L8) = "" Or Trim$(L9) = "" Or Trim$(L10) = "" _
  Then
    val_pat = False
  End If

End Function

Private Sub CODICE_Change()
  CODICE.ForeColor = vbBlack
End Sub

Private Sub CODICE_GotFocus()
On Error GoTo ges_err_pat

  CODICE.SelStart = 0: CODICE.SelLength = Len(CODICE.Text)
  val_ins_pat CODICE.hwnd

Exit Sub
ges_err_pat:
  log_str "on error intervenuta in CODICE_GotFocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub CONTRASTO_Change()
  CONTRASTO.ForeColor = vbBlack
End Sub

Private Sub CONTRASTO_GotFocus()
On Error GoTo ges_err_pat

  CONTRASTO.SelStart = 0: CONTRASTO.SelLength = Len(CONTRASTO.Text)
  val_ins_pat CONTRASTO.hwnd

Exit Sub
ges_err_pat:
  log_str "on error intervenuta in CONTRASTO_GotFocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub Ofs_cut_Change()
  Ofs_Cut.ForeColor = vbBlack
End Sub

Private Sub Ofs_cut_GotFocus()
On Error GoTo ges_err_pat

  Ofs_Cut.SelStart = 0: Ofs_Cut.SelLength = Len(Ofs_Cut.Text)
  val_ins_pat Ofs_Cut.hwnd

Exit Sub
ges_err_pat:
  log_str "on error intervenuta in Ofs_cut_GotFocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next

End Sub

Private Sub PRI_LET_FC_Change()
  PRI_LET_FC.ForeColor = vbBlack
End Sub

Private Sub PRI_LET_FC_GotFocus()
On Error GoTo ges_err_pat

  PRI_LET_FC.SelStart = 0: PRI_LET_FC.SelLength = Len(PRI_LET_FC.Text)
  val_ins_pat PRI_LET_FC.hwnd

Exit Sub
ges_err_pat:
  log_str "on error intervenuta in PRI_LET_FC_GotFocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub L1_Change()
  L1.ForeColor = vbBlack
End Sub
Private Sub L1_GotFocus()
On Error GoTo ges_err_pat

  L1.SelStart = 0: L1.SelLength = Len(L1.Text)
  val_ins_pat L1.hwnd

Exit Sub
ges_err_pat:
  log_str "on error intervenuta in L1_GotFocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub L2_Change()
  L2.ForeColor = vbBlack
End Sub
Private Sub L2_GotFocus()
On Error GoTo ges_err_pat

  L2.SelStart = 0: L2.SelLength = Len(L2.Text)
  val_ins_pat L2.hwnd

Exit Sub
ges_err_pat:
  log_str "on error intervenuta in L2_GotFocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub L3_Change()
  L3.ForeColor = vbBlack
End Sub
Private Sub L3_GotFocus()
On Error GoTo ges_err_pat

  L3.SelStart = 0: L3.SelLength = Len(L3.Text)
  val_ins_pat L3.hwnd

Exit Sub
ges_err_pat:
  log_str "on error intervenuta in L3_GotFocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub L4_Change()
  L4.ForeColor = vbBlack
End Sub
Private Sub L4_GotFocus()
On Error GoTo ges_err_pat

  L4.SelStart = 0: L4.SelLength = Len(L4.Text)
  val_ins_pat L4.hwnd

Exit Sub
ges_err_pat:
  log_str "on error intervenuta in L4_GotFocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub L5_Change()
  L5.ForeColor = vbBlack
End Sub
Private Sub L5_GotFocus()
On Error GoTo ges_err_pat

  L5.SelStart = 0: L5.SelLength = Len(L5.Text)
  val_ins_pat L5.hwnd

Exit Sub
ges_err_pat:
  log_str "on error intervenuta in L5_GotFocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub L6_Change()
  L6.ForeColor = vbBlack
End Sub
Private Sub L6_GotFocus()
On Error GoTo ges_err_pat

  L6.SelStart = 0: L6.SelLength = Len(L6.Text)
  val_ins_pat L6.hwnd

Exit Sub
ges_err_pat:
  log_str "on error intervenuta in L6_GotFocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub L7_Change()
  L7.ForeColor = vbBlack
End Sub
Private Sub L7_GotFocus()
On Error GoTo ges_err_pat

  L7.SelStart = 0: L7.SelLength = Len(L7.Text)
  val_ins_pat L7.hwnd

Exit Sub
ges_err_pat:
  log_str "on error intervenuta in L7_GotFocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub L8_Change()
  L8.ForeColor = vbBlack
End Sub
Private Sub L8_GotFocus()
On Error GoTo ges_err_pat

  L8.SelStart = 0: L8.SelLength = Len(L8.Text)
  val_ins_pat L8.hwnd

Exit Sub
ges_err_pat:
  log_str "on error intervenuta in L8_GotFocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub L9_Change()
  L9.ForeColor = vbBlack
End Sub
Private Sub L9_GotFocus()
On Error GoTo ges_err_pat

  L9.SelStart = 0: L9.SelLength = Len(L9.Text)
  val_ins_pat L9.hwnd

Exit Sub
ges_err_pat:
  log_str "on error intervenuta in L9_GotFocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub L10_Change()
  L10.ForeColor = vbBlack
End Sub
Private Sub L10_GotFocus()
On Error GoTo ges_err_pat

  L10.SelStart = 0: L10.SelLength = Len(L10.Text)
  val_ins_pat L10.hwnd

Exit Sub
ges_err_pat:
  log_str "on error intervenuta in L10_GotFocus" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub eve_key_esc(Shift As Integer)
  Select Case Shift
    Case 0
      If mod_pat Or nuo_pat Then
        abort 'annulla modifiche
        'Iciliouff 20-11-2003
        abi_con_pat False
      End If
    Case Else
  End Select
End Sub

Sub abort()
  'annulla le modifiche in corso
  If mod_pat Then
    If Not nuo_pat Then
      If Data1.Recordset.EditMode <> dbEditNone Then
        Data1.Recordset.CancelUpdate
      Else
        If foto_pat.RecordCount > 0 Then
          CODICE.Text = Format$(foto_pat!CODICE)
          CONTRASTO.Text = Format$(foto_pat!CONTRASTO)
          PRI_LET_FC.Text = Format$(foto_pat!PRI_LET_FC)
          L1.Text = Format$(foto_pat!L1)
          L2.Text = Format$(foto_pat!L2)
          L3.Text = Format$(foto_pat!L3)
          L4.Text = Format$(foto_pat!L4)
          L5.Text = Format$(foto_pat!L5)
          L6.Text = Format$(foto_pat!L6)
          L7.Text = Format$(foto_pat!L7)
          L8.Text = Format$(foto_pat!L8)
          L9.Text = Format$(foto_pat!L9)
          L10.Text = Format$(foto_pat!L10)
          'Robertouff 26-11-2007
          Ofs_Cut.Text = Format$(foto_pat!Ofs_Cut)
          foto_pat.Close
        End If
      End If
      lis.ReBind
    Else
      'Icilio3545 06-01-2004 Il nuovo pattern veniva memorizzato in PATTERN.DBF anche se incompleto
      If Data1.Recordset.EditMode <> dbEditNone Then Data1.Recordset.CancelUpdate: lis.ReBind
    End If
    'riassegna agli ordini i valori del pattern modificati
    mod_pat = False
    nuo_pat = False
    disable_tab Me
    lis.TabStop = True
    lis.SetFocus
  End If

End Sub

Function chk_esi_cod(cod As String) As Boolean
'ritorna true se il codice è già utilizzato da almeno un ordine in lista degli ordini da eseguire
Dim i As Integer
Dim f As Integer

  chk_esi_cod = False
  For i = 1 To ord_mac.Count
    For f = 1 To 3
      If Trim$(cod) = Trim$(ord_mac.item(i).pat_cod(f)) Then
        chk_esi_cod = True
        Exit For
      End If
    Next f
  Next i

End Function


Private Sub eve_key_F1(Shift As Integer)
  Select Case Shift
    Case 0
      'inserimento di un nuovo tipo di onda
      If Not mod_pat Then
        Data1.Recordset.AddNew
        'Iciliouff 20-11-2003
        abi_con_pat True
        enable_tab Me
        CODICE.SetFocus
        mod_pat = True
        ult_con = 0
        nuo_pat = True
        lis.TabStop = False
      End If
    Case 2
      'Iciliouff 20-11-2003
      If Not mod_pat Then
        eve_key.chg adj_pat_con
      End If
    Case Else
  End Select

End Sub

Private Sub eve_key_F10(Shift As Integer)
  Select Case Shift
    Case 0
      ' torna alla pagina di produzione
      If Not (mod_pat Or nuo_pat) Then
          eve_key.chg pag_pro
      End If
      
    Case Else
  End Select
End Sub
Private Sub eve_key_F12(Shift As Integer)
  Select Case Shift
    Case 0
      
    Case Else
  End Select
End Sub

Private Sub eve_key_F2(Shift As Integer)
Dim flg_pat_pre As Boolean
Dim i As Integer

  Select Case Shift
    Case 0
     'modifica record esistente
      If Data1.Recordset.RecordCount > 0 Then
        If Not mod_pat Then
          Set foto_pat = db.OpenRecordset("select * from PATTERN where Ucase(CODICE)='" + UCase$(CODICE.Text) + "' ", dbOpenSnapshot)
          'Se il pattern che si vuol modificare e' in uso il focus e' diverso
          flg_pat_pre = chk_esi_cod(CODICE.Text)
          'Iciliouff 20-11-2003
          abi_con_pat True
          enable_tab Me
          If flg_pat_pre Then
            CONTRASTO.SetFocus
            CODICE.TabStop = False
          Else
            CODICE.SetFocus
            CODICE.TabStop = True
          End If
          mod_pat = True
          ult_con = 0
          nuo_pat = False
          lis.TabStop = False
        End If
      End If
    Case Else
  End Select
End Sub

Private Sub eve_key_F3(Shift As Integer)
Dim flg_pat_pre As Boolean
Dim i As Integer
  
  Select Case Shift
    Case 0
       'cancellazione record
      If Not mod_pat Then
        'controllo che non ci sia alcun ordine in lista con l'onda da cancellare
        flg_pat_pre = chk_esi_cod(CODICE.Text)
        If Not flg_pat_pre Then
          With Data1.Recordset
            If .RecordCount > 0 Then
              .Delete
              .MovePrevious
              If .AbsolutePosition < .RecordCount - 1 Then
                .MoveNext
              End If
            End If
          End With
          'Data1.Refresh
          'lis.ReBind
          nuo_pat = False
        Else
           messaggio msg("imp_can_pat"), is_All:=True
        End If
      End If
     
    Case Else
  End Select
End Sub
Private Sub eve_key_F4(Shift As Integer)
Dim new_cod As String
Dim i As Integer
Dim f As Integer
Dim Ctr As Control
Dim err As Boolean
'Icilio3545 03-01-2004
Dim s As String

  Select Case Shift
    Case 0
      'salvataggio pattern
      If Data1.Recordset.RecordCount >= 0 And (mod_pat Or nuo_pat) Then
'        For Each Ctr In Me
'          If TypeName(Ctr) = "TextBox" Then
'            If UCase$(Left$(Ctr.name, 1)) = "L" Then Ctr.Text = CStr(val(Ctr.Text)) 'Le lunghezze del pattern se sono a stringa vuota vengono salvate come "0"
'            val_ins_pat Ctr.hwnd, err
'            If err Then Exit Sub
'          End If
'        Next Ctr
        
        If mod_pat Then
          'Icilio3545 03-01-2004 Inserito controllo su misure pattern
          If val_pat And Not err_mis_pat() Then
            ass_pat   ' per togliere valori null
            If Not mod_pat Then foto_pat.Close
            If nuo_pat Then
              Data1.Recordset.AddNew
            Else
              Data1.Recordset.Edit
              Data1.Recordset.Update
            End If
            Data1.Recordset.Bookmark = Data1.Recordset.LastModified
            new_cod = Format$(Data1.Recordset!CODICE)
            Data1.Refresh
            Data1.Recordset.FindFirst "CODICE='" + new_cod + "' "
            lis.ReBind
            mod_pat = False
            nuo_pat = False
            'riassegna i valori modificati agli ordini
            If ord_mac.Count > 1 Then
              For i = 1 To ord_mac.Count  'todo rimettere 2???  RFAZZI_FI 06-01-08
                For f = 1 To 3
                  If Trim$(CODICE) = Trim$(ord_mac.item(i).pat_cod(f)) Then
                    With ord_mac.item(i)
                      .pat_con(f) = Trim$(CONTRASTO)
                      .fst_FTC(f) = Trim$(PRI_LET_FC)
                      .L1(f) = Trim$(L1)
                      .L2(f) = Trim$(L2)
                      .L3(f) = Trim$(L3)
                      .L4(f) = Trim$(L4)
                      .L5(f) = Trim$(L5)
                      .L6(f) = Trim$(L6)
                      .L7(f) = Trim$(L7)
                      .L8(f) = Trim$(L8)
                      .L9(f) = Trim$(L9)
                      .L10(f) = Trim$(L10)
                      'Robertouff 26-11-2007
                      .Ofs_Cut(f) = Trim$(Ofs_Cut)
                      
                    End With
                  End If
                Next f
              Next i
            End If
            messaggio msg("ope_eff"), is_All:=False
            'Iciliouff 20-11-2003
            abi_con_pat False
            disable_tab Me
            lis.TabStop = True
            lis.SetFocus
          Else
            'Icilio3545 03-01-2004 gestione messaggi di errore
            s = IIf(err_mis_pat(), msg("err_mis_pat"), msg("man_cam_ond"))
            messaggio s, is_All:=True
            Select Case ult_con
              Case CODICE.hwnd
                CODICE.SetFocus
              Case CONTRASTO.hwnd
                CONTRASTO.SetFocus
              Case PRI_LET_FC.hwnd
                PRI_LET_FC.SetFocus
              Case L1.hwnd
                L1.SetFocus
              Case L2.hwnd
                L2.SetFocus
              Case L3.hwnd
                L3.SetFocus
              Case L4.hwnd
                L4.SetFocus
              Case L5.hwnd
                L5.SetFocus
              Case L6.hwnd
                L6.SetFocus
              Case L7.hwnd
                L7.SetFocus
              Case L8.hwnd
                L8.SetFocus
              Case L9.hwnd
                L9.SetFocus
              Case L10.hwnd
                L10.SetFocus
              'Robertouff 26-11-2007
              Case Ofs_Cut.hwnd
                Ofs_Cut.SetFocus
            End Select
            Exit Sub
          End If
        End If
      End If
    Case Else
  End Select
End Sub
Sub ass_pat()
' riassegna i valori null delle vaschette
If CODICE = "" Then CODICE = "0"
If CONTRASTO = "" Then CONTRASTO = "0"
If PRI_LET_FC = "" Then PRI_LET_FC = "0"
If L1 = "" Then L1 = "0"
If L2 = "" Then L2 = "0"
If L3 = "" Then L3 = "0"
If L4 = "" Then L4 = "0"
If L5 = "" Then L5 = "0"
If L6 = "" Then L6 = "0"
If L7 = "" Then L7 = "0"
If L8 = "" Then L8 = "0"
If L9 = "" Then L9 = "0"
If L10 = "" Then L10 = "0"
'Robertouff 26-11-2007
If Ofs_Cut = "" Then Ofs_Cut = "0"

End Sub

Private Sub eve_key_F8(Shift As Integer)
  Select Case Shift
    Case 0
      If Not mod_pat Then
        eve_key.chg pag_lis_ese
      End If
    Case Else
  End Select
End Sub
Private Sub eve_key_F9(Shift As Integer)
  Select Case Shift
    Case 0
      If Not (mod_pat Or nuo_pat) Then
        eve_key.chg pag_lis_ord
      End If
    Case Else
  End Select
End Sub

Private Sub Form_Activate()
  Dim tmp_lar As Long
  
  lis.Caption = msg("pattern")
  lis.Columns(0).Caption = msg("sig_cod")
  
  If abi_mis_ang Then
      tmp_lar = CODICE.Width \ 2
  Else
      tmp_lar = CODICE.Width
  End If
  abi_cam_ang abi_mis_ang
  CODICE.Width = tmp_lar
  CONTRASTO.Width = tmp_lar
  PRI_LET_FC.Width = tmp_lar
  L1.Width = tmp_lar
  L2.Width = tmp_lar
  L3.Width = tmp_lar
  L4.Width = tmp_lar
  L5.Width = tmp_lar
  L6.Width = tmp_lar
  L7.Width = tmp_lar
  L8.Width = tmp_lar
  L9.Width = tmp_lar
  L10.Width = tmp_lar
  'Robertouff 26-11-2007
  Ofs_Cut.Width = tmp_lar
  Data1.Refresh
  lis.SetFocus
End Sub

Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
  
  Select Case KeyCode
    Case vbKeyReturn, vbKeyDown
      KeyCode = 0
      key_tab shi:=False
    Case vbKeyUp
      KeyCode = 0
      key_tab shi:=True
  End Select


End Sub

Private Sub Form_Load()
On Error GoTo ges_err_pat

  Carica Me
  'Iciliouff 20-11-2003
  abi_con_pat False
  Data1.DatabaseName = Left$(pro_set.pro_pat, Len(pro_set.pro_pat) - 1)
  'Data1.RecordSource = "select * from PATTERN order by codice"
  Set eve_key = New key
  eve_key.create Me
  
Exit Sub
ges_err_pat:
log_str "on error intervenuta in pattern form_load" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next
End Sub
