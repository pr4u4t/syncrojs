VERSION 5.00
Begin VB.Form sal_cfg 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   2685
   ClientLeft      =   5280
   ClientTop       =   1650
   ClientWidth     =   6615
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   2685
   ScaleWidth      =   6615
   ShowInTaskbar   =   0   'False
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
      TabIndex        =   3
      TabStop         =   0   'False
      Top             =   1710
      Width           =   1215
   End
   Begin VB.TextBox txt_des 
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
      Left            =   2445
      MaxLength       =   35
      TabIndex        =   2
      Text            =   "12345678901234567890123456789012345"
      Top             =   825
      Width           =   3855
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
      TabIndex        =   5
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
      Caption         =   "Salva"
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
      Left            =   3285
      TabIndex        =   4
      Top             =   2160
      Width           =   525
   End
   Begin VB.Label Label1 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "DESCRIZIONE"
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
      Left            =   315
      TabIndex        =   1
      Top             =   840
      Width           =   1860
   End
   Begin VB.Label Pag_title 
      BackColor       =   &H80000009&
      BackStyle       =   0  'Transparent
      Caption         =   "Salva configurazione"
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
      Width           =   4425
   End
End
Attribute VB_Name = "sal_cfg"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
'Iciliouff 03-11-2003
Private WithEvents rmt_sal_cfg_sch As rmt
Attribute rmt_sal_cfg_sch.VB_VarHelpID = -1

Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
    par_mac_ute.par_cfg_att(livello) = par_mac.par_cfg_att(livello)
    eve_key.chg set_par_sch_tag3
  Case Else
End Select
End Sub

Private Sub eve_key_F11(Shift As Integer)
Dim ret As cod_ret

Select Case Shift
  Case 0
    If Len(txt_des) = 0 Then
        messaggio msg("imm_des"), True, False
    Else
        eve_key.chg set_par_sch_tag3
        If tro_cfg(Trim$(txt_des), livello) Then
            par_mac_ute.par_cfg_att(livello) = par_mac.par_cfg_att(livello)
            messaggio msg("cfg_pre"), True, False
        Else
            'scrivo nella scheda di taglio
            num_com_sch_tag = 1
            CASO_SND_SCH_TAG = INSERIMENTO_CFG_ATTIVA
            par_mac_ute.config_sch_tag_att(livello).descr = Trim$(txt_des)
            'Iciliouff 03-11-2003
            If rmt_sal_cfg_sch.esegui(tip_cmd.CONFIGURA, GetTickCount(), livello, 0) Then
              messaggio msg("inv_par_sch_tag"), False
            Else
              messaggio msg("imp_ese_ope"), True
            End If
'            snd_sch_tag livello, CONFIGURA
        End If
    End If
  Case Else
End Select
End Sub

Private Sub Form_Activate()
txt_des.Text = ""
txt_des.SetFocus
End Sub

Private Sub Form_Load()
Debug.Print "Inizio Load " & Me.name
Set eve_key = New key
'Iciliouff 03-11-2003
Set rmt_sal_cfg_sch = New rmt
rmt_sal_cfg_sch.set_plc_ute (2)
rmt_sal_cfg_sch.name = "sal_cfg_sch"
eve_key.create Me
Ridim Me
Center Me
dimensiona Me
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub


Private Sub rmt_sal_cfg_sch_retbad(err As String)
'Gestione risposta negativa da scheda di taglio
  
  If err <> "" Then messaggio err, True
  rmt_sal_cfg_sch.resetta
  
End Sub

Private Sub rmt_sal_cfg_sch_retok(s As String)
'Gestione risposta positiva da scheda di taglio
  
  If s <> "" Then messaggio s, True
  rmt_sal_cfg_sch.resetta
  
End Sub

Private Sub txt_des_GotFocus()
txt_des.SelStart = 0
txt_des.SelLength = Len(Trim$(txt_des))
End Sub


