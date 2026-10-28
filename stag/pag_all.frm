VERSION 5.00
Begin VB.Form pag_all 
   Appearance      =   0  'Flat
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6630
   ClientLeft      =   660
   ClientTop       =   420
   ClientWidth     =   11970
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6630
   ScaleMode       =   0  'User
   ScaleWidth      =   17636.84
   ShowInTaskbar   =   0   'False
   Begin VB.Image img_consolle 
      Height          =   2220
      Index           =   1
      Left            =   7950
      Picture         =   "pag_all.frx":0000
      Top             =   3855
      Width           =   2160
   End
   Begin VB.Image img_consolle 
      Height          =   2220
      Index           =   0
      Left            =   7950
      Picture         =   "pag_all.frx":FA02
      Top             =   3855
      Width           =   2160
   End
   Begin VB.Label lab_all 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "ALLARMI MALFUNZIONAMENTI"
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
      Index           =   7
      Left            =   7815
      TabIndex        =   7
      Top             =   1785
      Width           =   4035
   End
   Begin VB.Label lab_all 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "ALLARMI CONSOLLE"
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
      Index           =   6
      Left            =   7815
      TabIndex        =   6
      Top             =   1395
      Width           =   4035
   End
   Begin VB.Label lab_all 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "ALLARMI QE ESTERNO"
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
      Index           =   4
      Left            =   7815
      TabIndex        =   5
      Top             =   2955
      Width           =   4035
   End
   Begin VB.Label lab_all 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "ALLARMI QE SLAVE"
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
      Index           =   3
      Left            =   7815
      TabIndex        =   4
      Top             =   2565
      Width           =   4035
   End
   Begin VB.Label lab_all 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "ALLARMI QE MASTER"
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
      Index           =   2
      Left            =   7815
      TabIndex        =   3
      Top             =   2175
      Width           =   4035
   End
   Begin VB.Label lab_all 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "ALLARMI HARDWARE"
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
      Index           =   1
      Left            =   7815
      TabIndex        =   2
      Top             =   1005
      Width           =   4035
   End
   Begin VB.Label lab_all 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "ALLARMI BORDO MACCHINA"
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
      Index           =   5
      Left            =   7815
      TabIndex        =   1
      Top             =   615
      Width           =   4035
   End
   Begin VB.Image img_master 
      Height          =   5505
      Index           =   3
      Left            =   420
      Picture         =   "pag_all.frx":1F404
      Top             =   555
      Width           =   6765
   End
   Begin VB.Image img_master 
      Height          =   5490
      Index           =   2
      Left            =   420
      Picture         =   "pag_all.frx":33B62
      Top             =   555
      Width           =   6810
   End
   Begin VB.Image img_master 
      Height          =   5490
      Index           =   1
      Left            =   420
      Picture         =   "pag_all.frx":481DC
      Top             =   555
      Width           =   6825
   End
   Begin VB.Shape shp_cancello 
      BackStyle       =   1  'Opaque
      FillColor       =   &H000000FF&
      FillStyle       =   0  'Solid
      Height          =   455
      Index           =   2
      Left            =   3380
      Shape           =   3  'Circle
      Top             =   3926
      Visible         =   0   'False
      Width           =   351
   End
   Begin VB.Shape shp_cancello 
      BackStyle       =   1  'Opaque
      FillColor       =   &H000000FF&
      FillStyle       =   0  'Solid
      Height          =   455
      Index           =   1
      Left            =   1534
      Shape           =   3  'Circle
      Top             =   1196
      Visible         =   0   'False
      Width           =   351
   End
   Begin VB.Shape shp_cancello 
      BackStyle       =   1  'Opaque
      FillColor       =   &H000000FF&
      FillStyle       =   0  'Solid
      Height          =   455
      Index           =   0
      Left            =   5174
      Shape           =   3  'Circle
      Top             =   3640
      Visible         =   0   'False
      Width           =   351
   End
   Begin VB.Image img_master 
      Height          =   5490
      Index           =   0
      Left            =   420
      Picture         =   "pag_all.frx":5C856
      Top             =   555
      Width           =   6810
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "ALLARMI"
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
      TabIndex        =   0
      Top             =   0
      Width           =   6825
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "pag_all.frx":70ED0
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "pag_all"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Private WithEvents eve_sta As ClasseStato
Attribute eve_sta.VB_VarHelpID = -1

Private Sub eve_key_F1(Shift As Integer)
  Select Case Shift
    Case 0
      eve_key.chg all_mac
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

Private Sub eve_key_F2(Shift As Integer)
  Select Case Shift
    Case 0
      eve_key.chg all_con
    Case Else
  End Select

End Sub


Private Sub eve_key_F3(Shift As Integer)
  Select Case Shift
    Case 0
      eve_key.chg all_qe1
    Case Else
  End Select

End Sub


Private Sub eve_key_F4(Shift As Integer)
  Select Case Shift
    Case 0
      eve_key.chg all_har
    Case Else
  End Select

End Sub


Private Sub eve_key_F5(Shift As Integer)
  Select Case Shift
    Case 0
      eve_key.chg all_war
    Case Else
  End Select

End Sub


Private Sub eve_key_F7(Shift As Integer)
 Select Case Shift
    Case 0
      eve_key.chg sto_all
    Case Else
  End Select
End Sub

Private Sub eve_key_F8(Shift As Integer)
 Select Case Shift
    Case 0
      eve_key.chg pag_lis_ese
    Case Else
  End Select

End Sub

Private Sub eve_key_F9(Shift As Integer)
 Select Case Shift
    Case 0
      eve_key.chg pag_lis_ord
    Case Else
  End Select
End Sub


Private Sub eve_sta_gruall(v As String)
' v è una stringa di 5 bit che individuano i gruppi allarme
'1° bit  = malfunzionamenti
'2° bit = hardware
'3° bit = quadri elettrici di potenza
'4° bit = consolle
'5° bit = bordo macchina

'img_master(0) corrisponde a nessun allarme
'img_master(1) corrisponde ad allarmi bordo macchina
'img_master(2) corrisponde ad allarmi bordo macchina + quadri elettrici
'img_master(3) corrisponde ad allarmi quadri elettrici

'img_consolle(0) corrisponde a nessun allarme
'img_consolle(1) corrisponde a allarme consolle


Dim i As Integer
Dim str_BM As String
Dim str_QE As String
Dim QE As Integer
Dim str As String
Dim va As Double

va = Hex_Dec(bin_hex(v))
str = rev_str(v)
If (va And (2 ^ POS_GRU_CON)) = 2 ^ POS_GRU_CON Then
        img_consolle(1).Visible = True
        img_consolle(0).Visible = False
Else
        img_consolle(1).Visible = False
        img_consolle(0).Visible = True
End If
str_BM = val(Mid$(str, POS_GRU_BM + 1, 1))
QE = 0
For i = POS_GRU_QE_MST + 1 To POS_GRU_QE_EST + 1
  QE = QE + val(Mid$(str, i, 1))
Next i
If QE > 0 Then
  str_QE = "1"
Else
  str_QE = "0"
End If
str = str_BM + str_QE

Select Case str
    Case "00"   'né bordo macchina né QE
      img_master(0).Visible = True
      For i = 1 To 3
        img_master(i).Visible = False
      Next i
    Case "01"   'solo QE
      img_master(3).Visible = True
      For i = 0 To 2
        img_master(i).Visible = False
      Next i
    Case "10"    'solo bordo macchina
      For i = 0 To 3
        If i <> 1 Then
            img_master(i).Visible = False
        Else
            img_master(i).Visible = True
        End If
      Next i
    Case "11"    'bordo macchina + QE
      For i = 0 To 3
        If i <> 2 Then
            img_master(i).Visible = False
        Else
            img_master(i).Visible = True
        End If
      Next i
End Select

For i = POS_GRU_HAR To POS_GRU_MAL
    If (va And (2 ^ i)) = 2 ^ i Then
      lab_all(i).ForeColor = COL_ALL_Red
    Else
      lab_all(i).ForeColor = vbBlack
    End If
Next i

End Sub


Private Sub Form_Load()
'img_master(0) corrisponde a nessun allarme
'img_master(1) corrisponde ad allarmi bordo macchina
'img_master(2) corrisponde ad allarmi bordo macchina + quadri elettrici
'img_master(3) corrisponde ad allarmi quadri elettrici
  
'img_consolle(0) corrisponde a nessun allarme
'img_consolle(1) corrisponde a allarme consolle

  Dim i As Integer
  Debug.Print "Inizio Load " & Me.name
  Set eve_sta = sta_tag
  Set eve_key = New key
  eve_key.create Me
  Carica Me
  img_consolle(0).Visible = True
  img_consolle(1).Visible = False
  img_master(0).Visible = True
  For i = 1 To 3
          img_master(i).Visible = False
  Next i
  
  For i = 0 To 2
      shp_cancello(i).Visible = False
  Next i
  Debug.Print "Fine Load " & Me.name
  Debug.Print ""
End Sub


