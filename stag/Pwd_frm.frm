VERSION 5.00
Begin VB.Form Pwd_frm 
   BackColor       =   &H00C0FFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   4185
   ClientLeft      =   6870
   ClientTop       =   1380
   ClientWidth     =   4560
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4185
   ScaleWidth      =   4560
   ShowInTaskbar   =   0   'False
   Begin VB.Frame frm_set 
      BackColor       =   &H00C0FFFF&
      BorderStyle     =   0  'None
      Height          =   3700
      Left            =   30
      TabIndex        =   2
      Top             =   390
      Visible         =   0   'False
      Width           =   4425
      Begin VB.TextBox Text22 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   255
         Left            =   590
         TabIndex        =   28
         TabStop         =   0   'False
         Text            =   "Avvia SysWin"
         Top             =   2110
         Width           =   3800
      End
      Begin VB.TextBox Text21 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   255
         Left            =   90
         TabIndex        =   25
         TabStop         =   0   'False
         Text            =   "Ctrl+Shift+X per uscire dal programma"
         Top             =   3400
         Width           =   3800
      End
      Begin VB.TextBox Text20 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   255
         Left            =   570
         TabIndex        =   24
         TabStop         =   0   'False
         Text            =   "Riabilita tutte le protezioni"
         Top             =   3090
         Width           =   3800
      End
      Begin VB.TextBox Text19 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   255
         Left            =   570
         TabIndex        =   23
         TabStop         =   0   'False
         Text            =   "Avvia NT Explorer"
         Top             =   525
         Width           =   3800
      End
      Begin VB.TextBox Text18 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   255
         Left            =   570
         TabIndex        =   22
         TabStop         =   0   'False
         Text            =   "Apri sessione Dos"
         Top             =   855
         Width           =   3800
      End
      Begin VB.TextBox Text17 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   255
         Left            =   570
         TabIndex        =   21
         TabStop         =   0   'False
         Text            =   "Avvia Task Manager di Windows NT"
         Top             =   1170
         Width           =   3800
      End
      Begin VB.TextBox Text16 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   255
         Left            =   570
         TabIndex        =   20
         TabStop         =   0   'False
         Text            =   "Avvia PcAnyWhere"
         Top             =   1485
         Width           =   3800
      End
      Begin VB.TextBox Text15 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   255
         Left            =   570
         TabIndex        =   19
         TabStop         =   0   'False
         Text            =   "Apri file sorgenti in Visual Basic"
         Top             =   1815
         Width           =   3800
      End
      Begin VB.TextBox Text14 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   255
         Left            =   570
         TabIndex        =   18
         TabStop         =   0   'False
         Text            =   " "
         Top             =   2130
         Width           =   3800
      End
      Begin VB.TextBox Text13 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   255
         Left            =   570
         TabIndex        =   17
         TabStop         =   0   'False
         Text            =   "Modifica Password Livello 1"
         ToolTipText     =   " "
         Top             =   2445
         Width           =   3800
      End
      Begin VB.TextBox Text12 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   255
         Left            =   540
         TabIndex        =   16
         TabStop         =   0   'False
         Text            =   " Modifica Password"
         Top             =   2775
         Width           =   3800
      End
      Begin VB.TextBox Text11 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   255
         Left            =   570
         TabIndex        =   15
         TabStop         =   0   'False
         Text            =   "Abilita mouse"
         Top             =   210
         Width           =   3800
      End
      Begin VB.TextBox Text10 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   255
         Left            =   90
         TabIndex        =   14
         TabStop         =   0   'False
         Text            =   "F10"
         Top             =   3090
         Width           =   500
      End
      Begin VB.TextBox Text9 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   255
         Left            =   90
         TabIndex        =   13
         TabStop         =   0   'False
         Text            =   "F2"
         Top             =   530
         Width           =   500
      End
      Begin VB.TextBox Text8 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   255
         Left            =   90
         TabIndex        =   12
         TabStop         =   0   'False
         Text            =   "F3"
         Top             =   850
         Width           =   500
      End
      Begin VB.TextBox Text7 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   255
         Left            =   90
         TabIndex        =   11
         TabStop         =   0   'False
         Text            =   "F4"
         Top             =   1170
         Width           =   500
      End
      Begin VB.TextBox Text6 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   255
         Left            =   90
         TabIndex        =   9
         TabStop         =   0   'False
         Text            =   "F5"
         Top             =   1490
         Width           =   500
      End
      Begin VB.TextBox Text5 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   255
         Left            =   90
         TabIndex        =   8
         TabStop         =   0   'False
         Text            =   "F6"
         Top             =   1810
         Width           =   500
      End
      Begin VB.TextBox Text4 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   255
         Left            =   90
         TabIndex        =   7
         TabStop         =   0   'False
         Text            =   "F7"
         Top             =   2130
         Width           =   500
      End
      Begin VB.TextBox Text3 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   255
         Left            =   90
         TabIndex        =   6
         TabStop         =   0   'False
         Text            =   "F8"
         Top             =   2450
         Width           =   500
      End
      Begin VB.TextBox Text2 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   255
         Left            =   90
         TabIndex        =   5
         TabStop         =   0   'False
         Text            =   "F9"
         Top             =   2770
         Width           =   500
      End
      Begin VB.TextBox Text1 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   255
         Left            =   90
         TabIndex        =   4
         TabStop         =   0   'False
         Text            =   "F1"
         Top             =   210
         Width           =   500
      End
   End
   Begin VB.Frame frm_chg_pwd 
      BackColor       =   &H00C0FFFF&
      Height          =   855
      Left            =   30
      TabIndex        =   26
      Top             =   360
      Visible         =   0   'False
      Width           =   4425
      Begin VB.TextBox txt_msg_chg_pwd 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         ForeColor       =   &H000000C0&
         Height          =   255
         Left            =   60
         TabIndex        =   27
         TabStop         =   0   'False
         Text            =   "Inserire la password corrente"
         Top             =   240
         Width           =   4275
      End
      Begin VB.TextBox txt_chg_pwd 
         Height          =   285
         IMEMode         =   3  'DISABLE
         Left            =   120
         PasswordChar    =   "*"
         TabIndex        =   10
         Top             =   480
         Width           =   4215
      End
   End
   Begin VB.Frame frm_pwd 
      BackColor       =   &H00C0FFFF&
      Height          =   1095
      Left            =   30
      TabIndex        =   1
      Top             =   360
      Width           =   4425
      Begin VB.TextBox txt_pwd 
         Height          =   285
         IMEMode         =   3  'DISABLE
         Left            =   120
         PasswordChar    =   "*"
         TabIndex        =   0
         Top             =   660
         Width           =   4215
      End
      Begin VB.TextBox txt_msg 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H00C0FFFF&
         BorderStyle     =   0  'None
         ForeColor       =   &H000000C0&
         Height          =   255
         Left            =   60
         TabIndex        =   3
         TabStop         =   0   'False
         Text            =   "Inserire la password"
         Top             =   240
         Width           =   4275
      End
   End
   Begin VB.Image img_title 
      Height          =   300
      Left            =   -7460
      Picture         =   "Pwd_frm.frx":0000
      Top             =   0
      Width           =   11970
   End
End
Attribute VB_Name = "Pwd_frm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CPWD = "QCG"
Private Const CPWD_L1 = "GQVFJX" '6122001 Stagetti gestione password primo livello
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Private Enum enu_chg_pwd
  in_old_pwd
  in_new_pwd
  conf_new_pwd
  conf_new_pwd_liv1 '5122001 Stagetti gestione password abilitazione pagine ingegneristiche
  in_old_pwd_liv1 '5122001 Stagetti gestione password abilitazione pagine ingegneristiche
  in_new_pwd_liv1 '5122001 Stagetti gestione password abilitazione pagine ingegneristiche
End Enum
Private chg_pwd_sta As enu_chg_pwd
Private Const PC_ANY_1 = "C:\Programmi\Symantec\PcAnyWhere\WinAw32.exe"
Private Const PC_ANY_1a = "C:\Program Files\Symantec\PcAnyWhere\WinAw32.exe"
Private Const PC_ANY_2 = "C:\Programmi\PcAnyWhere\WinAw32.exe"
Private Const PC_ANY_2a = "C:\Program Files\PcAnyWhere\WinAw32.exe"
Private Const TSK_MAN = "C:\winnt\system32\Taskmgr.exe"
Private Const DOS_SHL = "C:\winnt\system32\cmd.exe"
Private Const BASE_PATH = "C:\usr\"
Private Const VB5a = "C:\Programmi\DevStudio\vb\vb5.exe"
Private Const VB5b = "C:\Program Files\DevStudio\vb\vb5.exe"
Private Const VB5c = "C:\Programmi\VisualStudio\vb\vb5.exe"
Private Const VB5d = "C:\Program Files\VisualStudio\vb\vb5.exe"
Private Const SysWina = "C:\Program Files\SysWin34\SysWin.exe"
Private Const SysWinb = "C:\Programmi\SysWin34\SysWin.exe"
Private Const SysWinc = "C:\SysWin34\SysWin.exe"

Private Function code(s As String, Optional liv_pwd As Boolean) As String
'Data una stringa restituisce la stringa codificata
Dim i As Long, lun As Long
Dim res As String
'6122001 Stagetti
Dim Param As String

  lun = Len(s)
  res = ""
  If lun > 0 Then
    For i = 1 To lun
      res = res + Chr$(Asc(Mid$(s, i, 1)) + i)
    Next i
  Else
'    res = UCase$(CPWD)
'    setFileINIString "PRO_SET", "Pwd", res, app.Path 'scrivo la stringa su file
    '6122001 Stagetti
    res = UCase$(IIf(liv_pwd, CPWD, CPWD_L1))
    Param = IIf(liv_pwd, "Pwd", "Pwd_liv1")
    setFileINIString "PRO_SET", Param, res ', app.Path 'scrivo la stringa su file

  End If
  code = res
End Function
Function decode(ByVal s As String, Optional liv_pwd As Boolean) As String
'Data una stringa codificata, restituisce la stringa originata
Dim i As Long, lun As Long
Dim res As String
Dim Param As String
Dim appoggio As String
  If s = "" Then 's = CPWD
    '6122001 Stagetti gestione password livello 1
    s = IIf(liv_pwd, CPWD, CPWD_L1)
  End If
  lun = Len(s)
  res = ""
  If lun > 0 Then
    For i = 1 To lun
      res = res + Chr$(Asc(Mid$(s, i, 1)) - i)
    Next i
    Param = IIf(liv_pwd, "pwd", "pwd_liv1")
    setFileINIString "PRO_SET", Param, s ', app.Path 'scrivo la stringa su file
  End If
  decode = UCase$(res)
End Function
Private Sub eve_key_esc(Shift As Integer)
  Select Case Shift
    Case 0
      eve_key.chg pag_pre
      'andrea 19-04-02 uff
      Tasti.Show
    Case Else
  End Select
End Sub

Private Sub eve_key_F1(Shift As Integer)
  Select Case Shift
    Case 0
        If Not pro_set.ute_wrk Then Exit Sub
        Mouse True
        eve_key.chg pag_pre
    Case Else
  End Select
End Sub


Private Sub eve_key_F10(Shift As Integer)
  Select Case Shift
    Case 0
        If Not (pro_set.ute_wrk) And Not (pro_set.ute_wrk_liv1) Then '512001 Stagetti
          eve_key.chg pag_pre
          Exit Sub
        End If
        pro_set.ute_wrk = False
        pro_set.ute_wrk_liv1 = False '512001 Stagetti
        eve_key.chg pag_pre
    Case Else
  End Select
End Sub

Private Sub eve_key_F2(Shift As Integer)
  Select Case Shift
    Case 0
      If Not pro_set.ute_wrk Then Exit Sub
      Shell "explorer", vbNormalFocus
    Case Else
  End Select
Exit Sub
End Sub

Private Sub eve_key_F3(Shift As Integer)
 Select Case Shift
    Case 0
      If Not pro_set.ute_wrk Then Exit Sub
      If dir$(DOS_SHL) <> "" Then
        Shell DOS_SHL, vbNormalFocus
      Else
        messaggio "Applicazione non trovata: usare Explorer (F2) per cercarla manualmente", False
      End If
    Case Else
  End Select
End Sub

Private Sub eve_key_F4(Shift As Integer)
 Select Case Shift
    Case 0
      If Not pro_set.ute_wrk Then Exit Sub
      If dir$(TSK_MAN) <> "" Then
        Shell TSK_MAN, vbNormalFocus
      Else
        messaggio "Applicazione non trovata: usare Explorer (F2) per cercarla manualmente", False
      End If
    Case Else
  End Select
End Sub

Private Sub eve_key_F5(Shift As Integer)
 Select Case Shift
    Case 0
      If Not pro_set.ute_wrk Then Exit Sub
      If dir$(PC_ANY_1) <> "" Then
        Shell PC_ANY_1, vbNormalFocus
      ElseIf dir$(PC_ANY_1a) <> "" Then
        Shell PC_ANY_1a, vbNormalFocus
      ElseIf dir$(PC_ANY_2) <> "" Then
        Shell PC_ANY_2, vbNormalFocus
      ElseIf dir$(PC_ANY_2a) <> "" Then
        Shell PC_ANY_2a, vbNormalFocus
      Else
        messaggio "Applicazione non trovata: usare Explorer (F2) per cercarla manualmente", False
      End If
    Case Else
  End Select
End Sub


Private Sub eve_key_F6(Shift As Integer)
Dim cmp_path As String
 Select Case Shift
    Case 0
      If Not pro_set.ute_wrk Then Exit Sub
      cmp_path = BASE_PATH + app.EXEName + "\" + app.EXEName + ".vbp"
      If dir(cmp_path) <> "" Then
        If dir(VB5a) <> "" Then
          Shell VB5a + " " + cmp_path, vbNormalFocus
        ElseIf dir(VB5b) <> "" Then
          Shell VB5b + " " + cmp_path, vbNormalFocus
        ElseIf dir(VB5c) <> "" Then
          Shell VB5c + " " + cmp_path, vbNormalFocus
        ElseIf dir(VB5d) <> "" Then
          Shell VB5d + " " + cmp_path, vbNormalFocus
        Else
          messaggio "Applicazione non trovata: usare Explorer (F2) per cercarla manualmente", False
        End If
      Else
        messaggio "Applicazione non trovata: usare Explorer (F2) per cercarla manualmente", False
      End If
    Case Else
  End Select
End Sub


Private Sub eve_key_F7(Shift As Integer)
 Select Case Shift
    Case 0
      If Not pro_set.ute_wrk Then Exit Sub
      If dir(SysWina) <> "" Then
        Shell SysWina, vbNormalFocus
      ElseIf dir(SysWinb) <> "" Then
        Shell SysWinb, vbNormalFocus
      ElseIf dir(SysWinc) <> "" Then
        Shell SysWinc, vbNormalFocus
      Else
        messaggio "Applicazione non trovata: usare Explorer (F2) per cercarla manualmente", False
      End If
    Case Else
  End Select
End Sub

Private Sub eve_key_F8(Shift As Integer)
'5122001 Stagetti Modifica password abilitazione password pagine ingegneristiche
  Select Case Shift
    Case 0
      frm_set.Visible = False
      frm_pwd.Visible = False
      frm_chg_pwd.Visible = True
      frm_chg_pwd.Top = img_title.Height
      txt_msg_chg_pwd = "Inserire la password corrente"
      txt_chg_pwd.Text = ""
      txt_chg_pwd.SetFocus
      chg_pwd_sta = in_old_pwd_liv1
    Case Else
  End Select
End Sub

Private Sub eve_key_F9(Shift As Integer)
Select Case Shift
    Case 0
      If Not pro_set.ute_wrk Then Exit Sub
        frm_set.Visible = False
        frm_pwd.Visible = False
        frm_chg_pwd.Visible = True
        frm_chg_pwd.Top = img_title.Height
        txt_msg_chg_pwd = "Inserire la password corrente"
        txt_chg_pwd.Text = ""
        txt_chg_pwd.SetFocus
        chg_pwd_sta = in_old_pwd
    Case Else
  End Select
End Sub

Private Sub Form_Activate()
    'messaggio iniziale di inserimento password
    txt_msg.Text = "password"
    txt_pwd.Text = ""
    txt_msg.ForeColor = COL_ALL_Red
    frm_set.Visible = False
    txt_pwd.Enabled = True
    frm_pwd.Visible = True
    frm_chg_pwd.Visible = False
End Sub

Private Sub Form_Load()
  'Debug.Print "Inizio Load " & Me.Name
  Set eve_key = New key
  eve_key.create Me
  #If psp <> 1 And psp <> 0 Then
    Me.Left = Screen.Width - Me.Width - 80 * (pro_set.PercX / 100)
    Me.Top = 80 * (pro_set.PercX / 100)
  #Else
    Me.Left = Screen.Width - Me.Width - 80
    Me.Top = 80
  #End If
  'Debug.Print "Fine Load " & Me.Name
  'Debug.Print ""
End Sub


Private Sub txt_chg_pwd_KeyDown(KeyCode As Integer, Shift As Integer)
Static new_pwd As String 'memorizza la nuova password
Static new_pwd_liv1 As String '5122001 Stagetti memorizza la nuova password di abilitazione pagine igegneristiche
If KeyCode = vbKeyReturn Then
  Select Case chg_pwd_sta
    Case enu_chg_pwd.in_old_pwd
      If UCase$(txt_chg_pwd.Text) = decode(pro_set.pwd) Then
        'password corretta
        txt_msg_chg_pwd = "Inserire la nuova password"
        txt_chg_pwd.Text = ""
        txt_chg_pwd.SetFocus
        chg_pwd_sta = enu_chg_pwd.in_new_pwd
      Else 'esco
        frm_chg_pwd.Visible = False
        frm_pwd.Visible = True
        frm_set.Visible = False
      End If
    Case enu_chg_pwd.in_new_pwd
      If Len(txt_chg_pwd.Text) > 0 Then
        new_pwd = UCase$(txt_chg_pwd.Text)
        txt_msg_chg_pwd = "Confermare la nuova password"
        txt_chg_pwd.Text = ""
        txt_chg_pwd.SetFocus
        chg_pwd_sta = enu_chg_pwd.conf_new_pwd
      Else 'esco
        frm_chg_pwd.Visible = False
        frm_pwd.Visible = True
        frm_set.Visible = False
      End If
    Case enu_chg_pwd.conf_new_pwd
      If UCase$(txt_chg_pwd.Text) = new_pwd Then 'la conferma è andata a buon fine
        frm_chg_pwd.Visible = False
        frm_pwd.Visible = True
        pro_set.pwd = code(UCase$(txt_chg_pwd.Text))
        setFileINIString "PRO_SET", "Pwd", pro_set.pwd  'scrivo la pwd su file
        txt_msg.Text = "Nuova password memorizzata"
        txt_pwd.Text = ""
        frm_set.Visible = False
      Else 'esco
        frm_chg_pwd.Visible = False
        frm_pwd.Visible = True
        frm_set.Visible = False
      End If
'==========================================================================================
'5122001 Stagetti gestione password abilitazione pagine igegneristiche
    Case enu_chg_pwd.in_old_pwd_liv1
      If UCase$(txt_chg_pwd.Text) = decode(pro_set.pwd_liv1) Then
        'password corretta
        txt_msg_chg_pwd = "Inserire la nuova password"
        txt_chg_pwd.Text = ""
        txt_chg_pwd.SetFocus
        chg_pwd_sta = enu_chg_pwd.in_new_pwd_liv1
      Else 'esco
        frm_chg_pwd.Visible = False
        frm_pwd.Visible = True
        frm_set.Visible = False
        txt_msg.ForeColor = COL_ALL_Red
        txt_msg.Text = "Password corrente errata"
      End If
    Case enu_chg_pwd.in_new_pwd_liv1
      If Len(txt_chg_pwd.Text) > 0 Then
        new_pwd_liv1 = UCase$(txt_chg_pwd.Text)
        txt_msg_chg_pwd = "Confermare la nuova password"
        txt_chg_pwd.Text = ""
        txt_chg_pwd.SetFocus
        chg_pwd_sta = conf_new_pwd_liv1
      Else 'esco
        frm_chg_pwd.Visible = False
        frm_pwd.Visible = True
        frm_set.Visible = False
      End If
    Case enu_chg_pwd.conf_new_pwd_liv1
      If UCase$(txt_chg_pwd.Text) = new_pwd_liv1 Then 'la conferma è andata a buon fine
        frm_chg_pwd.Visible = False
        frm_pwd.Visible = True
        pro_set.pwd_liv1 = code(UCase$(txt_chg_pwd.Text))
        setFileINIString "PRO_SET", "Pwd_Liv1", pro_set.pwd_liv1  'scrivo la pwd su file
        txt_msg.Text = "Nuova password memorizzata"
        txt_pwd.Text = ""
        frm_set.Visible = False
      Else 'esco
        frm_chg_pwd.Visible = False
        frm_pwd.Visible = True
        frm_set.Visible = False
      End If
'==========================================================================================
      
  End Select
End If
End Sub


Private Sub txt_pwd_GotFocus()
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
End Sub

Private Sub txt_pwd_KeyDown(KeyCode As Integer, Shift As Integer)
Dim s As String
  If KeyCode = vbKeyReturn Then
    'controllo password inserita
    If UCase$(txt_pwd.Text) = decode(pro_set.pwd, True) Then
      txt_msg.Text = "Password corretta"
      txt_msg.ForeColor = vbBlue
      pro_set.ute_wrk = True
      setFileINIString "PRO_SET", "Pwd", pro_set.pwd  'scrivo la stringa su file
      frm_set.Visible = True
      frm_pwd.Visible = False
    'Else
      'txt_msg.Text = "Password errata: riprova"
      'txt_msg.ForeColor = COL_ALL_Red
      'txt_pwd.Text = ""
      'pro_set.ute_wrk = False
      'frm_set.Visible = False
      'frm_pwd.Visible = True
      'txt_pwd.SetFocus
    'End If
  'End If
'===========================================================================================
'5122001 Stagetti Gestione password abilitazione pagine ingegneristiche
    'controllo password inserita
    ElseIf UCase$(txt_pwd.Text) = decode(pro_set.pwd_liv1, False) Then
      If txt_pwd.Text = "" Then Exit Sub
        txt_msg.Text = "Password corretta"
        txt_msg.ForeColor = vbBlue
        pro_set.ute_wrk_liv1 = True
        setFileINIString "PRO_SET", "pwd_liv1", pro_set.pwd_liv1  'scrivo la stringa su file
        'frm_set.Visible = True
        Pwd_frm.Visible = False    'aggiunta
        'frm_pwd.Visible = False
        eve_key.chg pag_pre
      Else
        If UCase$(txt_pwd.Text) = "" Then
          'pro_set.ute_wrk_liv1 = True
          Pwd_frm.Visible = False
          eve_key.chg pag_pre
        Else
          txt_msg.Text = "Password errata: riprova"
          txt_msg.ForeColor = COL_ALL_Red
          txt_pwd.Text = ""
          pro_set.ute_wrk = False
          frm_set.Visible = False
          frm_pwd.Visible = True
          txt_pwd.SetFocus
        End If
      End If
'=========================================================================================
  End If
End Sub

