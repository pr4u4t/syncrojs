VERSION 5.00
Begin VB.Form con_azz 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   2685
   ClientLeft      =   5190
   ClientTop       =   5535
   ClientWidth     =   6615
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   2685
   ScaleWidth      =   6615
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox txt_tur 
      BackColor       =   &H00C0FFFF&
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
      Left            =   3525
      MaxLength       =   1
      TabIndex        =   4
      Text            =   "1"
      Top             =   720
      Width           =   615
   End
   Begin VB.CommandButton tas_F1 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      Caption         =   "F11"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   2910
      TabIndex        =   1
      TabStop         =   0   'False
      Top             =   1710
      Width           =   1215
   End
   Begin VB.Label Lab_des 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Turno iniziale"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   1200
      TabIndex        =   5
      Top             =   720
      Width           =   2220
   End
   Begin VB.Label lab_avv_1 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
      Caption         =   "(Esc per annullare)"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00404080&
      Height          =   195
      Left            =   2145
      TabIndex        =   3
      Top             =   1410
      Width           =   2745
      WordWrap        =   -1  'True
   End
   Begin VB.Label txt_F1 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H00C0C0C0&
      BackStyle       =   0  'Transparent
      Caption         =   "Conferma"
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
      Height          =   225
      Left            =   3120
      TabIndex        =   2
      Top             =   2160
      Width           =   855
   End
   Begin VB.Label Pag_title 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Conferma azzeramento dati"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00800000&
      Height          =   375
      Left            =   195
      TabIndex        =   0
      Top             =   135
      Width           =   5985
   End
End
Attribute VB_Name = "con_azz"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1

Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
    eve_key.chg pag_pro_day
  Case Else
End Select
End Sub

Private Sub eve_key_F11(Shift As Integer)
Dim ret As cod_ret
Dim v As Integer
Select Case Shift
  Case 0
    ' conferma azzeramento dati di produzione
    v = val(txt_tur)
    If v >= 1 And v <= 3 Then
        pro_set.tur = v
        setFileINIString "pro_set", "tur", Format$(v)
        sta_tag.tempi True, True, True
        eve_key.chg pag_pro_day
    Else
        messaggio msg("err_val"), True, False
        txt_tur.SetFocus
    End If
  Case Else
End Select
End Sub

Private Sub Form_Load()
Debug.Print "Inizio Load " & Me.name
Set eve_key = New key
'Iciliouff 03-11-2003
eve_key.create Me
Ridim Me
Center Me
dimensiona Me
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub






