VERSION 5.00
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Begin VB.Form Ins_ord 
   Appearance      =   0  'Flat
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   6615
   ClientLeft      =   1425
   ClientTop       =   7140
   ClientWidth     =   12060
   ControlBox      =   0   'False
   BeginProperty Font 
      Name            =   "Arial"
      Size            =   9.75
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
   ScaleHeight     =   6615
   ScaleWidth      =   12060
   ShowInTaskbar   =   0   'False
   Begin VB.Data dat_pat 
      Caption         =   "Data3"
      Connect         =   "dBASE III;"
      DatabaseName    =   "C:\usr\stag\dati"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   345
      Left            =   7260
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   2  'Snapshot
      RecordSource    =   "select * from PATTERN order by CODICE"
      Top             =   6120
      Visible         =   0   'False
      Width           =   1260
   End
   Begin VB.Frame frm_liv0 
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      Height          =   1470
      Index           =   2
      Left            =   518
      TabIndex        =   75
      Top             =   4597
      Width           =   11025
      Begin VB.TextBox txt_pos_lat_pat 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Index           =   2
         Left            =   8280
         MaxLength       =   5
         TabIndex        =   43
         Text            =   "12345"
         Top             =   1050
         Width           =   660
      End
      Begin VB.TextBox txt_fin_pri 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Index           =   2
         Left            =   9570
         MaxLength       =   4
         TabIndex        =   45
         Text            =   "1234"
         Top             =   1050
         Width           =   570
      End
      Begin VB.TextBox txt_fin_dop 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Index           =   2
         Left            =   10350
         MaxLength       =   4
         TabIndex        =   46
         Text            =   "1234"
         Top             =   1050
         Width           =   570
      End
      Begin VB.TextBox txt_pat_con 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Index           =   2
         Left            =   9060
         MaxLength       =   1
         TabIndex        =   44
         Text            =   "1"
         Top             =   1050
         Width           =   240
      End
      Begin VB.TextBox txt_lun_for 
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
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
         Height          =   295
         Index           =   2
         Left            =   9660
         TabIndex        =   93
         TabStop         =   0   'False
         Top             =   360
         Width           =   1065
      End
      Begin VB.ComboBox cmb_cut_to_mark 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
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
         Height          =   405
         Index           =   2
         ItemData        =   "Insord.frx":0000
         Left            =   4305
         List            =   "Insord.frx":0002
         Style           =   2  'Dropdown List
         TabIndex        =   41
         Top             =   1005
         Width           =   1350
      End
      Begin VB.TextBox txt_nom_cli 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   2
         Left            =   5415
         MaxLength       =   30
         TabIndex        =   36
         Top             =   375
         Width           =   4050
      End
      Begin VB.TextBox txt_num_tot_fog 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   2
         Left            =   2910
         MaxLength       =   6
         TabIndex        =   40
         Top             =   1050
         Width           =   1230
      End
      Begin VB.TextBox txt_resa 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   2
         Left            =   2385
         MaxLength       =   1
         TabIndex        =   39
         Top             =   1050
         Width           =   390
      End
      Begin VB.TextBox txt_sca_lar 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   2
         Left            =   75
         TabIndex        =   37
         Top             =   1050
         Width           =   1095
      End
      Begin VB.TextBox txt_sca_lun 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   2
         Left            =   1230
         TabIndex        =   38
         Top             =   1050
         Width           =   1095
      End
      Begin VB.TextBox txt_cod_cli 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   2
         Left            =   3570
         MaxLength       =   12
         TabIndex        =   35
         Top             =   375
         Width           =   1605
      End
      Begin VB.TextBox txt_cod_com 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   2
         Left            =   1395
         MaxLength       =   15
         TabIndex        =   34
         Top             =   375
         Width           =   2025
      End
      Begin VB.ComboBox txt_liv 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   405
         Index           =   2
         ItemData        =   "Insord.frx":0004
         Left            =   60
         List            =   "Insord.frx":0006
         Style           =   2  'Dropdown List
         TabIndex        =   33
         Top             =   328
         Width           =   1215
      End
      Begin MSDBCtls.DBCombo cmb_pat_cod 
         Bindings        =   "Insord.frx":0008
         Height          =   420
         Index           =   2
         Left            =   5760
         TabIndex        =   42
         Top             =   990
         Width           =   2430
         _ExtentX        =   4286
         _ExtentY        =   741
         _Version        =   327681
         MatchEntry      =   -1  'True
         Style           =   2
         BackColor       =   16777215
         ForeColor       =   0
         ListField       =   "CODICE"
         Text            =   "cmb_dat_cod"
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
      End
      Begin VB.Label lab_pos_lat_pattern 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "pos.pat"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   2
         Left            =   8280
         TabIndex        =   110
         Top             =   765
         Width           =   690
      End
      Begin VB.Label lab_fin_pri 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "f.prima"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   2
         Left            =   9570
         TabIndex        =   109
         Top             =   765
         Width           =   675
      End
      Begin VB.Label lab_fin_dop 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "f.dopo"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   2
         Left            =   10350
         TabIndex        =   108
         Top             =   765
         Width           =   600
      End
      Begin VB.Label lab_pat_con 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "con."
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   2
         Left            =   9060
         TabIndex        =   107
         Top             =   765
         Width           =   435
      End
      Begin VB.Line lin10 
         Index           =   26
         X1              =   10290
         X2              =   10290
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin10 
         Index           =   25
         X1              =   9510
         X2              =   9510
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin10 
         Index           =   24
         X1              =   9000
         X2              =   9000
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin10 
         Index           =   23
         X1              =   8220
         X2              =   8220
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin10 
         Index           =   22
         X1              =   9570
         X2              =   9570
         Y1              =   60
         Y2              =   750
      End
      Begin VB.Label lab_pat_cod 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "codice pattern"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   2
         Left            =   5730
         TabIndex        =   102
         Top             =   765
         Width           =   1365
      End
      Begin VB.Label lab_lun_for 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "lun.formato"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   2
         Left            =   9660
         TabIndex        =   92
         Top             =   75
         Width           =   1125
      End
      Begin VB.Line lin10 
         BorderWidth     =   2
         Index           =   8
         X1              =   11010
         X2              =   11010
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Label lab_cut_to_mark 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "taglio registro"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   2
         Left            =   4290
         TabIndex        =   86
         Top             =   765
         Width           =   1350
      End
      Begin VB.Line lin10 
         Index           =   5
         X1              =   5670
         X2              =   5670
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin1 
         BorderWidth     =   2
         Index           =   2
         X1              =   10
         X2              =   10
         Y1              =   30
         Y2              =   1460
      End
      Begin VB.Label lab_nom 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "nome cliente"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   2
         Left            =   5415
         TabIndex        =   83
         Top             =   75
         Width           =   1845
      End
      Begin VB.Label lab_num 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "totale scatole"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   2
         Left            =   2910
         TabIndex        =   82
         Top             =   765
         Width           =   1275
      End
      Begin VB.Line lin9 
         Index           =   2
         X1              =   2865
         X2              =   2865
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin8 
         Index           =   2
         X1              =   2340
         X2              =   2340
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin6 
         BorderWidth     =   2
         Index           =   2
         X1              =   11010
         X2              =   11010
         Y1              =   30
         Y2              =   760
      End
      Begin VB.Line lin2 
         Index           =   2
         X1              =   1320
         X2              =   1320
         Y1              =   60
         Y2              =   760
      End
      Begin VB.Line lin4 
         Index           =   2
         X1              =   5280
         X2              =   5280
         Y1              =   60
         Y2              =   760
      End
      Begin VB.Shape shp_liv2 
         FillStyle       =   0  'Solid
         Height          =   30
         Index           =   2
         Left            =   0
         Top             =   750
         Width           =   11020
      End
      Begin VB.Label lab_res 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C8B74&
         BackStyle       =   0  'Transparent
         Caption         =   "resa"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   2
         Left            =   2385
         TabIndex        =   81
         Top             =   765
         Width           =   405
      End
      Begin VB.Label lab_lar 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C8B74&
         BackStyle       =   0  'Transparent
         Caption         =   "larghezza"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   2
         Left            =   75
         TabIndex        =   80
         Top             =   765
         Width           =   945
      End
      Begin VB.Label lab_cod_cli 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C8B74&
         BackStyle       =   0  'Transparent
         Caption         =   "codice cliente"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   2
         Left            =   3570
         TabIndex        =   79
         Top             =   75
         Width           =   1785
      End
      Begin VB.Label lab_liv 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "Livello"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   2
         Left            =   60
         TabIndex        =   78
         Top             =   75
         Width           =   645
      End
      Begin VB.Label lab_com 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "commessa"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   2
         Left            =   1395
         TabIndex        =   77
         Top             =   75
         Width           =   1605
      End
      Begin VB.Label lab_lun 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "lunghezza"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   2
         Left            =   1230
         TabIndex        =   76
         Top             =   765
         Width           =   990
      End
      Begin VB.Shape shp_liv 
         BorderWidth     =   2
         FillStyle       =   0  'Solid
         Height          =   45
         Index           =   2
         Left            =   0
         Top             =   30
         Width           =   11020
      End
      Begin VB.Line lin3 
         Index           =   2
         X1              =   3495
         X2              =   3495
         Y1              =   60
         Y2              =   760
      End
      Begin VB.Line lin7 
         Index           =   2
         X1              =   1170
         X2              =   1170
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Shape shp_liv1 
         BorderWidth     =   2
         FillStyle       =   0  'Solid
         Height          =   45
         Index           =   2
         Left            =   0
         Top             =   1425
         Width           =   11020
      End
      Begin VB.Line lin10 
         Index           =   2
         X1              =   4230
         X2              =   4230
         Y1              =   765
         Y2              =   1455
      End
   End
   Begin VB.Frame frm_liv0 
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      Height          =   1470
      Index           =   1
      Left            =   518
      TabIndex        =   47
      Top             =   3067
      Width           =   11025
      Begin VB.TextBox txt_pos_lat_pat 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Index           =   1
         Left            =   8280
         MaxLength       =   5
         TabIndex        =   29
         Text            =   "12345"
         Top             =   1050
         Width           =   660
      End
      Begin VB.TextBox txt_fin_pri 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Index           =   1
         Left            =   9570
         MaxLength       =   4
         TabIndex        =   31
         Text            =   "1234"
         Top             =   1050
         Width           =   570
      End
      Begin VB.TextBox txt_fin_dop 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Index           =   1
         Left            =   10350
         MaxLength       =   4
         TabIndex        =   32
         Text            =   "1234"
         Top             =   1050
         Width           =   570
      End
      Begin VB.TextBox txt_pat_con 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Index           =   1
         Left            =   9060
         MaxLength       =   1
         TabIndex        =   30
         Text            =   "1"
         Top             =   1050
         Width           =   240
      End
      Begin VB.TextBox txt_lun_for 
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
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
         Height          =   295
         Index           =   1
         Left            =   9660
         TabIndex        =   94
         TabStop         =   0   'False
         Top             =   360
         Width           =   1065
      End
      Begin VB.ComboBox cmb_cut_to_mark 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
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
         Height          =   405
         Index           =   1
         ItemData        =   "Insord.frx":001A
         Left            =   4305
         List            =   "Insord.frx":001C
         Style           =   2  'Dropdown List
         TabIndex        =   27
         Top             =   1005
         Width           =   1350
      End
      Begin VB.TextBox txt_nom_cli 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   1
         Left            =   5415
         MaxLength       =   30
         TabIndex        =   22
         Top             =   375
         Width           =   4050
      End
      Begin VB.TextBox txt_num_tot_fog 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   1
         Left            =   2910
         MaxLength       =   6
         TabIndex        =   26
         Top             =   1050
         Width           =   1230
      End
      Begin VB.TextBox txt_resa 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   1
         Left            =   2385
         MaxLength       =   1
         TabIndex        =   25
         Top             =   1050
         Width           =   390
      End
      Begin VB.TextBox txt_sca_lar 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   1
         Left            =   75
         TabIndex        =   23
         Top             =   1050
         Width           =   1095
      End
      Begin VB.TextBox txt_sca_lun 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   1
         Left            =   1230
         TabIndex        =   24
         Top             =   1050
         Width           =   1095
      End
      Begin VB.TextBox txt_cod_cli 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   1
         Left            =   3570
         MaxLength       =   12
         TabIndex        =   21
         Top             =   375
         Width           =   1605
      End
      Begin VB.TextBox txt_cod_com 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   1
         Left            =   1395
         MaxLength       =   15
         TabIndex        =   20
         Top             =   375
         Width           =   2025
      End
      Begin VB.ComboBox txt_liv 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   405
         Index           =   1
         ItemData        =   "Insord.frx":001E
         Left            =   60
         List            =   "Insord.frx":0020
         Style           =   2  'Dropdown List
         TabIndex        =   19
         Top             =   328
         Width           =   1215
      End
      Begin MSDBCtls.DBCombo cmb_pat_cod 
         Bindings        =   "Insord.frx":0022
         Height          =   420
         Index           =   1
         Left            =   5760
         TabIndex        =   28
         Top             =   990
         Width           =   2430
         _ExtentX        =   4286
         _ExtentY        =   741
         _Version        =   327681
         MatchEntry      =   -1  'True
         Style           =   2
         BackColor       =   16777215
         ForeColor       =   0
         ListField       =   "CODICE"
         Text            =   "cmb_dat_cod"
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
      End
      Begin VB.Label lab_pos_lat_pattern 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "pos.pat"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   1
         Left            =   8280
         TabIndex        =   106
         Top             =   765
         Width           =   690
      End
      Begin VB.Label lab_fin_pri 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "f.prima"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   1
         Left            =   9570
         TabIndex        =   105
         Top             =   765
         Width           =   675
      End
      Begin VB.Label lab_fin_dop 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "f.dopo"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   1
         Left            =   10350
         TabIndex        =   104
         Top             =   765
         Width           =   600
      End
      Begin VB.Label lab_pat_con 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "con."
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   1
         Left            =   9060
         TabIndex        =   103
         Top             =   765
         Width           =   435
      End
      Begin VB.Line lin10 
         Index           =   21
         X1              =   10290
         X2              =   10290
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin10 
         Index           =   20
         X1              =   9510
         X2              =   9510
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin10 
         Index           =   19
         X1              =   9000
         X2              =   9000
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin10 
         Index           =   18
         X1              =   8220
         X2              =   8220
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin10 
         Index           =   17
         X1              =   0
         X2              =   0
         Y1              =   0
         Y2              =   690
      End
      Begin VB.Line lin10 
         Index           =   16
         X1              =   0
         X2              =   0
         Y1              =   0
         Y2              =   690
      End
      Begin VB.Line lin10 
         Index           =   15
         X1              =   0
         X2              =   0
         Y1              =   0
         Y2              =   690
      End
      Begin VB.Line lin10 
         Index           =   14
         X1              =   9570
         X2              =   9570
         Y1              =   60
         Y2              =   750
      End
      Begin VB.Label lab_pat_cod 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "codice pattern"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   1
         Left            =   5730
         TabIndex        =   101
         Top             =   765
         Width           =   1365
      End
      Begin VB.Label lab_lun_for 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "lun.formato"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   1
         Left            =   9660
         TabIndex        =   91
         Top             =   75
         Width           =   1095
      End
      Begin VB.Line lin10 
         BorderWidth     =   2
         Index           =   7
         X1              =   11010
         X2              =   11010
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Label lab_cut_to_mark 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "taglio registro"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   1
         Left            =   4290
         TabIndex        =   85
         Top             =   765
         Width           =   1350
      End
      Begin VB.Line lin10 
         Index           =   4
         X1              =   5670
         X2              =   5670
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin1 
         BorderWidth     =   2
         Index           =   1
         X1              =   10
         X2              =   10
         Y1              =   30
         Y2              =   1460
      End
      Begin VB.Label lab_nom 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "nome cliente"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   1
         Left            =   5415
         TabIndex        =   74
         Top             =   75
         Width           =   1845
      End
      Begin VB.Label lab_num 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "totale scatole"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   1
         Left            =   2910
         TabIndex        =   73
         Top             =   765
         Width           =   1275
      End
      Begin VB.Line lin9 
         Index           =   1
         X1              =   2865
         X2              =   2865
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin8 
         Index           =   1
         X1              =   2340
         X2              =   2340
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin6 
         BorderWidth     =   2
         Index           =   1
         X1              =   11010
         X2              =   11010
         Y1              =   30
         Y2              =   760
      End
      Begin VB.Line lin2 
         Index           =   1
         X1              =   1320
         X2              =   1320
         Y1              =   60
         Y2              =   760
      End
      Begin VB.Line lin4 
         Index           =   1
         X1              =   5280
         X2              =   5280
         Y1              =   60
         Y2              =   760
      End
      Begin VB.Shape shp_liv2 
         FillStyle       =   0  'Solid
         Height          =   30
         Index           =   1
         Left            =   0
         Top             =   750
         Width           =   11020
      End
      Begin VB.Label lab_res 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C8B74&
         BackStyle       =   0  'Transparent
         Caption         =   "resa"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   1
         Left            =   2385
         TabIndex        =   72
         Top             =   765
         Width           =   405
      End
      Begin VB.Label lab_lar 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C8B74&
         BackStyle       =   0  'Transparent
         Caption         =   "larghezza"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   1
         Left            =   75
         TabIndex        =   71
         Top             =   765
         Width           =   945
      End
      Begin VB.Label lab_cod_cli 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C8B74&
         BackStyle       =   0  'Transparent
         Caption         =   "codice cliente"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   1
         Left            =   3570
         TabIndex        =   70
         Top             =   75
         Width           =   1785
      End
      Begin VB.Label lab_liv 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "Livello"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   1
         Left            =   60
         TabIndex        =   69
         Top             =   75
         Width           =   645
      End
      Begin VB.Label lab_com 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "commessa"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   1
         Left            =   1395
         TabIndex        =   68
         Top             =   75
         Width           =   1605
      End
      Begin VB.Label lab_lun 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "lunghezza"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   1
         Left            =   1230
         TabIndex        =   67
         Top             =   765
         Width           =   990
      End
      Begin VB.Shape shp_liv 
         BorderWidth     =   2
         FillStyle       =   0  'Solid
         Height          =   45
         Index           =   1
         Left            =   0
         Top             =   30
         Width           =   11020
      End
      Begin VB.Line lin3 
         Index           =   1
         X1              =   3495
         X2              =   3495
         Y1              =   60
         Y2              =   760
      End
      Begin VB.Line lin7 
         Index           =   1
         X1              =   1170
         X2              =   1170
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Shape shp_liv1 
         BorderWidth     =   2
         FillStyle       =   0  'Solid
         Height          =   45
         Index           =   1
         Left            =   0
         Top             =   1425
         Width           =   11020
      End
      Begin VB.Line lin10 
         Index           =   1
         X1              =   4230
         X2              =   4230
         Y1              =   765
         Y2              =   1455
      End
   End
   Begin VB.Frame frm_liv0 
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      Height          =   1470
      Index           =   0
      Left            =   518
      TabIndex        =   58
      Top             =   1537
      Width           =   11025
      Begin VB.TextBox txt_pat_con 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Index           =   0
         Left            =   9060
         MaxLength       =   1
         TabIndex        =   16
         Text            =   "1"
         Top             =   1050
         Width           =   240
      End
      Begin VB.TextBox txt_fin_dop 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Index           =   0
         Left            =   10350
         MaxLength       =   4
         TabIndex        =   18
         Text            =   "1234"
         Top             =   1050
         Width           =   570
      End
      Begin VB.TextBox txt_fin_pri 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Index           =   0
         Left            =   9570
         MaxLength       =   4
         TabIndex        =   17
         Text            =   "1234"
         Top             =   1050
         Width           =   570
      End
      Begin VB.TextBox txt_pos_lat_pat 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Index           =   0
         Left            =   8280
         MaxLength       =   5
         TabIndex        =   15
         Text            =   "12345"
         Top             =   1050
         Width           =   660
      End
      Begin VB.TextBox txt_lun_for 
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
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
         Height          =   295
         Index           =   0
         Left            =   9660
         TabIndex        =   95
         TabStop         =   0   'False
         Text            =   "12345678"
         Top             =   360
         Width           =   1065
      End
      Begin VB.ComboBox cmb_cut_to_mark 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
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
         Height          =   405
         Index           =   0
         ItemData        =   "Insord.frx":0034
         Left            =   4305
         List            =   "Insord.frx":0036
         Style           =   2  'Dropdown List
         TabIndex        =   13
         Top             =   1005
         Width           =   1350
      End
      Begin VB.ComboBox txt_liv 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   405
         Index           =   0
         ItemData        =   "Insord.frx":0038
         Left            =   60
         List            =   "Insord.frx":003A
         Style           =   2  'Dropdown List
         TabIndex        =   5
         Top             =   328
         Width           =   1215
      End
      Begin VB.TextBox txt_cod_com 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   0
         Left            =   1395
         MaxLength       =   15
         TabIndex        =   6
         Text            =   "123456789012345"
         Top             =   375
         Width           =   2025
      End
      Begin VB.TextBox txt_cod_cli 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   0
         Left            =   3570
         MaxLength       =   12
         TabIndex        =   7
         Text            =   "123456789012"
         Top             =   375
         Width           =   1605
      End
      Begin VB.TextBox txt_sca_lun 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   0
         Left            =   1230
         TabIndex        =   10
         Text            =   "12345678"
         Top             =   1050
         Width           =   1095
      End
      Begin VB.TextBox txt_sca_lar 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   0
         Left            =   45
         TabIndex        =   9
         Text            =   "12345678"
         Top             =   1050
         Width           =   1095
      End
      Begin VB.TextBox txt_resa 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   0
         Left            =   2385
         MaxLength       =   1
         TabIndex        =   11
         Top             =   1050
         Width           =   390
      End
      Begin VB.TextBox txt_num_tot_fog 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   0
         Left            =   2910
         MaxLength       =   6
         TabIndex        =   12
         Text            =   "123456"
         Top             =   1050
         Width           =   1230
      End
      Begin VB.TextBox txt_nom_cli 
         Appearance      =   0  'Flat
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   295
         Index           =   0
         Left            =   5415
         MaxLength       =   30
         TabIndex        =   8
         Text            =   "123456789012345678901234567890"
         Top             =   375
         Width           =   4050
      End
      Begin MSDBCtls.DBCombo cmb_pat_cod 
         Bindings        =   "Insord.frx":003C
         Height          =   420
         Index           =   0
         Left            =   5760
         TabIndex        =   14
         Top             =   990
         Width           =   2430
         _ExtentX        =   4286
         _ExtentY        =   741
         _Version        =   327681
         MatchEntry      =   -1  'True
         Style           =   2
         BackColor       =   16777215
         ForeColor       =   0
         ListField       =   "CODICE"
         Text            =   "cmb_dat_cod"
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
      End
      Begin VB.Label lab_pat_con 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "con."
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   0
         Left            =   9060
         TabIndex        =   100
         Top             =   765
         Width           =   435
      End
      Begin VB.Line lin10 
         Index           =   13
         X1              =   9000
         X2              =   9000
         Y1              =   780
         Y2              =   1470
      End
      Begin VB.Label lab_pat_cod 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "codice pattern"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   0
         Left            =   5730
         TabIndex        =   99
         Top             =   765
         Width           =   1365
      End
      Begin VB.Line lin10 
         Index           =   12
         X1              =   8220
         X2              =   8220
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Label lab_fin_dop 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "f.dopo"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   0
         Left            =   10350
         TabIndex        =   98
         Top             =   765
         Width           =   600
      End
      Begin VB.Line lin10 
         Index           =   11
         X1              =   10290
         X2              =   10290
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Label lab_fin_pri 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "f.prima"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   0
         Left            =   9570
         TabIndex        =   97
         Top             =   765
         Width           =   675
      End
      Begin VB.Label lab_pos_lat_pattern 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "pos.pat"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   0
         Left            =   8280
         TabIndex        =   96
         Top             =   765
         Width           =   690
      End
      Begin VB.Line lin10 
         Index           =   10
         X1              =   9570
         X2              =   9570
         Y1              =   60
         Y2              =   750
      End
      Begin VB.Line lin10 
         Index           =   9
         X1              =   9510
         X2              =   9510
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Label lab_lun_for 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "lun.formato"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   0
         Left            =   9660
         TabIndex        =   90
         Top             =   75
         Width           =   1095
      End
      Begin VB.Line lin10 
         BorderWidth     =   2
         Index           =   6
         X1              =   11010
         X2              =   11010
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Label lab_cut_to_mark 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "taglio registro"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   0
         Left            =   4290
         TabIndex        =   84
         Top             =   765
         Width           =   1320
      End
      Begin VB.Line lin10 
         Index           =   3
         X1              =   5670
         X2              =   5670
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin10 
         Index           =   0
         X1              =   4230
         X2              =   4230
         Y1              =   765
         Y2              =   1455
      End
      Begin VB.Shape shp_liv1 
         BorderWidth     =   2
         FillStyle       =   0  'Solid
         Height          =   45
         Index           =   0
         Left            =   0
         Top             =   1425
         Width           =   11020
      End
      Begin VB.Line lin7 
         Index           =   0
         X1              =   1170
         X2              =   1170
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin3 
         Index           =   0
         X1              =   3495
         X2              =   3495
         Y1              =   60
         Y2              =   760
      End
      Begin VB.Shape shp_liv 
         BorderWidth     =   2
         FillStyle       =   0  'Solid
         Height          =   45
         Index           =   0
         Left            =   0
         Top             =   30
         Width           =   11020
      End
      Begin VB.Label lab_lun 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "lunghezza"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   0
         Left            =   1230
         TabIndex        =   66
         Top             =   765
         Width           =   990
      End
      Begin VB.Label lab_com 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "commessa"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   0
         Left            =   1395
         TabIndex        =   65
         Top             =   75
         Width           =   1605
      End
      Begin VB.Label lab_liv 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "Livello"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   0
         Left            =   60
         TabIndex        =   64
         Top             =   75
         Width           =   645
      End
      Begin VB.Label lab_cod_cli 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C8B74&
         BackStyle       =   0  'Transparent
         Caption         =   "codice cliente"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   0
         Left            =   3570
         TabIndex        =   63
         Top             =   75
         Width           =   1785
      End
      Begin VB.Label lab_lar 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C8B74&
         BackStyle       =   0  'Transparent
         Caption         =   "larghezza"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   0
         Left            =   45
         TabIndex        =   62
         Top             =   765
         Width           =   945
      End
      Begin VB.Label lab_res 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C8B74&
         BackStyle       =   0  'Transparent
         Caption         =   "resa"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   0
         Left            =   2385
         TabIndex        =   61
         Top             =   765
         Width           =   465
      End
      Begin VB.Shape shp_liv2 
         FillStyle       =   0  'Solid
         Height          =   30
         Index           =   0
         Left            =   -30
         Top             =   750
         Width           =   11025
      End
      Begin VB.Line lin4 
         Index           =   0
         X1              =   5280
         X2              =   5280
         Y1              =   60
         Y2              =   760
      End
      Begin VB.Line lin2 
         Index           =   0
         X1              =   1320
         X2              =   1320
         Y1              =   60
         Y2              =   760
      End
      Begin VB.Line lin6 
         BorderWidth     =   2
         Index           =   0
         X1              =   11010
         X2              =   11010
         Y1              =   30
         Y2              =   760
      End
      Begin VB.Line lin8 
         Index           =   0
         X1              =   2340
         X2              =   2340
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Line lin9 
         Index           =   0
         X1              =   2865
         X2              =   2865
         Y1              =   750
         Y2              =   1440
      End
      Begin VB.Label lab_num 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00838D6D&
         BackStyle       =   0  'Transparent
         Caption         =   "totale scatole"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   0
         Left            =   2910
         TabIndex        =   60
         Top             =   765
         Width           =   1275
      End
      Begin VB.Label lab_nom 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H007C857A&
         BackStyle       =   0  'Transparent
         Caption         =   "nome cliente"
         ForeColor       =   &H00000000&
         Height          =   240
         Index           =   0
         Left            =   5415
         TabIndex        =   59
         Top             =   75
         Width           =   1845
      End
      Begin VB.Line lin1 
         BorderWidth     =   2
         Index           =   0
         X1              =   10
         X2              =   10
         Y1              =   30
         Y2              =   1460
      End
   End
   Begin VB.Frame frm_tes 
      BackColor       =   &H00FFFFFF&
      BorderStyle     =   0  'None
      Height          =   885
      Left            =   15
      TabIndex        =   50
      Top             =   547
      Width           =   12135
      Begin VB.TextBox txt_id 
         Alignment       =   1  'Right Justify
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   210
         Left            =   3120
         MaxLength       =   11
         TabIndex        =   89
         Text            =   "9005/00"
         Top             =   110
         Width           =   675
      End
      Begin VB.TextBox txt_met 
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   345
         Left            =   10560
         TabIndex        =   88
         TabStop         =   0   'False
         Top             =   360
         Width           =   1335
      End
      Begin VB.TextBox txt_indice 
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   345
         Left            =   1380
         MaxLength       =   3
         TabIndex        =   0
         TabStop         =   0   'False
         Text            =   "1"
         Top             =   360
         Width           =   645
      End
      Begin VB.TextBox txt_alt_bob 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Left            =   9270
         TabIndex        =   4
         Top             =   360
         Width           =   1185
      End
      Begin VB.Frame Frame1 
         BackColor       =   &H00000000&
         BorderStyle     =   0  'None
         Height          =   765
         Left            =   0
         TabIndex        =   51
         Top             =   30
         Width           =   1245
         Begin VB.Image Image1 
            Height          =   780
            Left            =   0
            Picture         =   "Insord.frx":004E
            Stretch         =   -1  'True
            Top             =   -15
            Width           =   360
         End
         Begin VB.Label label9 
            Alignment       =   2  'Center
            BackStyle       =   0  'Transparent
            Caption         =   "  DATI ORDINE"
            ForeColor       =   &H80000005&
            Height          =   480
            Index           =   1
            Left            =   360
            TabIndex        =   52
            Top             =   180
            Width           =   855
         End
      End
      Begin VB.TextBox txt_cod_abb 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Left            =   2115
         MaxLength       =   11
         TabIndex        =   1
         Top             =   360
         Width           =   1665
      End
      Begin MSDBCtls.DBCombo cmb_onda 
         Bindings        =   "Insord.frx":0890
         DataSource      =   "dat_ond"
         Height          =   465
         Left            =   3855
         TabIndex        =   2
         Top             =   300
         Width           =   930
         _ExtentX        =   1640
         _ExtentY        =   820
         _Version        =   327681
         Style           =   2
         BackColor       =   16777215
         ForeColor       =   0
         ListField       =   "onda"
         Text            =   ""
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
      End
      Begin MSDBCtls.DBCombo cmb_carta 
         Bindings        =   "Insord.frx":08A2
         DataSource      =   "dat_car"
         Height          =   465
         Left            =   4845
         TabIndex        =   3
         Top             =   300
         Width           =   4335
         _ExtentX        =   7646
         _ExtentY        =   820
         _Version        =   327681
         Style           =   2
         BackColor       =   16777215
         ForeColor       =   0
         ListField       =   "carta"
         Text            =   ""
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
      End
      Begin VB.Label Label1 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "lungh. tot."
         ForeColor       =   &H00000000&
         Height          =   240
         Left            =   10560
         TabIndex        =   87
         Top             =   75
         Width           =   960
      End
      Begin VB.Line Line10 
         BorderColor     =   &H00000000&
         Index           =   0
         X1              =   10500
         X2              =   10500
         Y1              =   45
         Y2              =   820
      End
      Begin VB.Line Line1 
         BorderColor     =   &H00000000&
         BorderWidth     =   3
         X1              =   11925
         X2              =   11925
         Y1              =   45
         Y2              =   820
      End
      Begin VB.Line Line10 
         BorderColor     =   &H00000000&
         Index           =   1
         X1              =   9195
         X2              =   9195
         Y1              =   45
         Y2              =   820
      End
      Begin VB.Shape Shape12 
         FillStyle       =   0  'Solid
         Height          =   75
         Left            =   -150
         Top             =   795
         Width           =   12100
      End
      Begin VB.Shape Shape6 
         FillStyle       =   0  'Solid
         Height          =   75
         Index           =   1
         Left            =   1320
         Top             =   30
         Width           =   10635
      End
      Begin VB.Line Line7 
         BorderColor     =   &H00000000&
         Index           =   1
         X1              =   3810
         X2              =   3810
         Y1              =   75
         Y2              =   820
      End
      Begin VB.Line Line6 
         BorderColor     =   &H00000000&
         X1              =   4800
         X2              =   4800
         Y1              =   45
         Y2              =   820
      End
      Begin VB.Label Label5 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "codice"
         ForeColor       =   &H00000000&
         Height          =   240
         Left            =   2115
         TabIndex        =   57
         Top             =   75
         Width           =   630
      End
      Begin VB.Label Label4 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "onda"
         ForeColor       =   &H00000000&
         Height          =   240
         Left            =   3855
         TabIndex        =   56
         Top             =   75
         Width           =   480
      End
      Begin VB.Label Label2 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "tipo carta"
         ForeColor       =   &H00000000&
         Height          =   240
         Left            =   4845
         TabIndex        =   55
         Top             =   75
         Width           =   900
      End
      Begin VB.Label Label20 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "indice"
         ForeColor       =   &H00000000&
         Height          =   240
         Left            =   1365
         TabIndex        =   54
         Top             =   75
         Width           =   585
      End
      Begin VB.Line Line9 
         BorderColor     =   &H00000000&
         X1              =   2040
         X2              =   2040
         Y1              =   75
         Y2              =   820
      End
      Begin VB.Label Label25 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "alt. bobina"
         ForeColor       =   &H00000000&
         Height          =   240
         Left            =   9255
         TabIndex        =   53
         Top             =   75
         Width           =   1020
      End
   End
   Begin VB.TextBox txt_idle 
      Appearance      =   0  'Flat
      BackColor       =   &H000000FF&
      BorderStyle     =   0  'None
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000002&
      Height          =   360
      Left            =   0
      TabIndex        =   49
      TabStop         =   0   'False
      Top             =   -1000
      Width           =   765
   End
   Begin VB.Data dat_ond 
      Caption         =   "Data1"
      Connect         =   "dBASE III;"
      DatabaseName    =   "C:\usr\stag\dati"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   345
      Left            =   8820
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   2  'Snapshot
      RecordSource    =   "select distinct onda from onde order by onda"
      Top             =   6120
      Visible         =   0   'False
      Width           =   1245
   End
   Begin VB.Data dat_car 
      Caption         =   "Data2"
      Connect         =   "dBASE III;"
      DatabaseName    =   "C:\usr\stag\dati"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   345
      Left            =   10290
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   2  'Snapshot
      RecordSource    =   "Onde"
      Top             =   6120
      Visible         =   0   'False
      Width           =   1245
   End
   Begin VB.Label Pag_title 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "GESTIONE ORDINI"
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
      TabIndex        =   48
      Top             =   0
      Width           =   7965
   End
   Begin VB.Image Image3 
      Height          =   300
      Left            =   0
      Picture         =   "Insord.frx":08B4
      Stretch         =   -1  'True
      Top             =   15
      Width           =   11970
   End
End
Attribute VB_Name = "Ins_ord"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private WithEvents eve_key As key
Attribute eve_key.VB_VarHelpID = -1
Private WithEvents eve_sta As ClasseStato
Attribute eve_sta.VB_VarHelpID = -1
Private WithEvents eve_ord_mac As CollezioneOrdini
Attribute eve_ord_mac.VB_VarHelpID = -1
Dim temp As ClasseOrdine
Dim contatore As Integer
Public tip_op As Integer
Public flg_rev As Boolean
Public ult_con As Long    'memorizzo l'handle dell'ultimo controllo per poterci tornare in caso di errore
Public ult_ind As Integer 'memorizzo l'indice (ovvero il livello) dell'ultimo controllo
Private lun_txt_sca_lar As Integer
Private lun_txt_sca_lun As Integer
Private lun_txt_alt_bob As Integer
Const CAR_AGG_INC = 4

Sub abi_con_cut_pat(abi As Boolean, Optional ind As Variant)
'Abilita i controlli cut to pattern
Dim i As Integer
  
  If IsMissing(ind) Then
    For i = 0 To 2
      cmb_pat_cod(i).TabStop = abi
      'RFAZZI_FI 07-01-08
      'txt_ofs_cut(i).TabStop = abi
      'RFAZZI_FI12/03/2008.Aggiunta gestione per saltare il campo in caso di Cut to Pattern NON automatico
      'agrassi_FI15-07-08
      'If par_mac.opt.cut_pat Then
      If val(par_mac.opt.cut_pat(i)) = 1 Then
        txt_pos_lat_pat(i).TabStop = False
      Else
        txt_pos_lat_pat(i).TabStop = abi
      End If
      txt_pat_con(i).TabStop = abi
      txt_fin_pri(i).TabStop = abi
      txt_fin_dop(i).TabStop = abi
    Next i
  Else
    cmb_pat_cod(ind).TabStop = abi
    'RFAZZI_FI 07-01-08
    'txt_ofs_cut(ind).TabStop = abi
    'RFAZZI_FI12/03/2008.Aggiunta gestione per saltare il campo in caso di Cut to Pattern NON automatico
    'agrassi_FI15-07-08
    'If par_mac.opt.cut_pat Then
    If val(par_mac.opt.cut_pat(ind + 1)) = 1 Then
      txt_pos_lat_pat(ind).TabStop = False
    Else
      txt_pos_lat_pat(ind).TabStop = abi
    End If
    txt_pat_con(ind).TabStop = abi
    txt_fin_pri(ind).TabStop = abi
    txt_fin_dop(ind).TabStop = abi
  End If
  
End Sub


Function acc_new_car(c As Control, key As Integer) As Boolean
Dim l_tot As Integer
Dim app As Integer
Dim s As String
Dim pos_pnt As Integer
Dim ord As Tip_Ordine
acc_new_car = False

Select Case c.name
  Case "txt_alt_bob"
    l_tot = lun_txt_alt_bob
    app = Len(ord.alt_bob)
    
  Case "txt_sca_lar"
    l_tot = lun_txt_sca_lar
    app = Len(ord.ord(1).sca_lar)
    
  Case "txt_sca_lun"
    l_tot = lun_txt_sca_lun
    app = Len(ord.ord(1).sca_lun)
End Select

If Len(Trim$(c.Text)) + 1 > l_tot Then Exit Function

If abi_mis_ang Then
  s = String(app, "9")
  s = con_dmm_to_inc(s)
  app = InStr(1, s, ".") - 1
  If app <= 0 Then app = Len(s)
  pos_pnt = InStr(1, Trim$(c.Text), ".")
  If pos_pnt <= 0 Then
    If (Len(Trim$(c.Text)) + 1) > app And key <> 46 And key <> vbKeyDecimal Then Exit Function
  Else
    If (Len(Trim$(c.Text)) - pos_pnt) = CAR_AGG_INC - 1 Then Exit Function
  End If
End If

acc_new_car = True

End Function


Private Sub createNullOrderForm()
'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
'Valorizzazione campi

On Error GoTo STAG_Gest_ERR
  enable_frames Me
  txt_met = ""
  clear_cell Me
  
  txt_cod_abb.Text = "1"
  If Format$(cmb_onda.Text) = "" Then 'se non ho ancora inserito un'onda, metto la prima del db
    dat_ond.RecordSource = "select distinct onda from onde"
    dat_ond.Refresh
    cmb_onda.Text = dat_ond.Recordset("onda")
  End If
  txt_alt_bob.Text = "1800"
  
  txt_liv(0).Text = txt_liv(0).List(0)
  txt_cod_com(0).Text = "NULL ORDER"
  txt_nom_cli(0).Text = "NULL ORDER"
  txt_sca_lar(0).Text = "0"
  txt_sca_lun(0).Text = "0"
  txt_resa(0).Text = "0"
  txt_num_tot_fog(0).Text = "0"
  txt_indice = ord_mac.Count + 1
  txt_id.TabStop = False
  disable_frames Me
Exit Sub
STAG_Gest_ERR:
log_str "on error intervenuta in cmb_onda_GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next
  
End Sub
Sub ini_form()
Dim i As Integer

For i = 0 To 2
  txt_liv(i).AddItem msg("liv_alt")
  txt_liv(i).ItemData(txt_liv(i).NewIndex) = LIV_ALT
  If par_mac.car_tec.num_liv_tag = 3 Then
      txt_liv(i).AddItem msg("liv_med")
      txt_liv(i).ItemData(txt_liv(i).NewIndex) = LIV_MED
  End If
  If par_mac.car_tec.num_liv_tag >= 2 Then
      txt_liv(i).AddItem msg("liv_bas")
      txt_liv(i).ItemData(txt_liv(i).NewIndex) = LIV_BAS
  End If
Next i
For i = 0 To 2
  cmb_cut_to_mark(i).Clear    'Iciliouff 20-11-2003
  cmb_cut_to_mark(i).AddItem msg("msg_no")
  cmb_cut_to_mark(i).ItemData(cmb_cut_to_mark(i).NewIndex) = NO
  cmb_cut_to_mark(i).AddItem msg("msg_si")
  cmb_cut_to_mark(i).ItemData(cmb_cut_to_mark(i).NewIndex) = SI
  'Iciliouff 20-11-2003
  'Robertouff 26-11-2007
  'agrassi_FI15-07-08
  'If par_mac.opt.cut_pat = STR_TRUE Then
  
  'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
  'If par_mac.opt.cut_pat(i + 1) = STR_TRUE Or par_mac.opt.cut_pat_auto = STR_TRUE Then
  If par_mac.opt.cut_pat(1) = STR_TRUE Or par_mac.opt.cut_pat(2) = STR_TRUE Or par_mac.opt.cut_pat(3) = STR_TRUE Or par_mac.opt.cut_pat_auto(1) = STR_TRUE Or par_mac.opt.cut_pat_auto(2) = STR_TRUE Or par_mac.opt.cut_pat_auto(3) = STR_TRUE Then
    cmb_cut_to_mark(i).AddItem msg("msg_cut_pat")
    cmb_cut_to_mark(i).ItemData(cmb_cut_to_mark(i).NewIndex) = PRE_PATTERN
  End If
Next i

End Sub

Private Sub riempi_celle(indice As Integer)
Dim i As Integer
Dim k As Integer
Dim ordine As Tip_Ordine


Set temp = ord_mac.item(indice)
ordine = str_ord(temp.ordine)
var_ord_ute.rec = ordine
With ordine
    txt_alt_bob = IIf(abi_mis_ang, con_dmm_to_inc(Trim$(.alt_bob)), CStr(val(.alt_bob) \ TODECMM))
    txt_met.Text = IIf(abi_mis_ang, con_met_to_fet(Trim$(.met_tot_ord)), .met_tot_ord)
    txt_cod_abb = Rie_nul(.cod_ord_abb)
    txt_id = .stag_id & "/" & .mod_id
    'Icilio3521 27-03-2004 Invertita carta ed onda per evitare di visualizzare carta errata
    'cmb_carta.Text = .tip_car.cod_com_car
    cmb_onda.Text = .tip_car.Cod_ond
    cmb_carta.Text = .tip_car.cod_com_car
    txt_indice = ord_mac.leg_pun_ord
    For i = 0 To 2
      txt_liv(i).Clear
      txt_liv(i).AddItem msg("liv_alt")
      txt_liv(i).ItemData(txt_liv(i).NewIndex) = LIV_ALT
      If par_mac.car_tec.num_liv_tag = 3 Then
          txt_liv(i).AddItem msg("liv_med")
          txt_liv(i).ItemData(txt_liv(i).NewIndex) = LIV_MED
      End If
      If par_mac.car_tec.num_liv_tag >= 2 Then
          txt_liv(i).AddItem msg("liv_bas")
          txt_liv(i).ItemData(txt_liv(i).NewIndex) = LIV_BAS
      End If
      cmb_cut_to_mark(i).Clear
      cmb_cut_to_mark(i).AddItem msg("msg_no")
      cmb_cut_to_mark(i).ItemData(cmb_cut_to_mark(i).NewIndex) = NO
      cmb_cut_to_mark(i).AddItem msg("msg_si")
      cmb_cut_to_mark(i).ItemData(cmb_cut_to_mark(i).NewIndex) = SI
      'Iciliouff 20-11-2003
      'Robertouff 26-11-2007
      'agrassi_FI15-07-08
      'If par_mac.opt.cut_pat = STR_TRUE Or par_mac.opt.cut_pat_auto = STR_TRUE Then
      
      'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
      'If par_mac.opt.cut_pat(i + 1) = STR_TRUE Or par_mac.opt.cut_pat_auto = STR_TRUE Then
      If par_mac.opt.cut_pat(1) = STR_TRUE Or par_mac.opt.cut_pat(2) = STR_TRUE Or par_mac.opt.cut_pat(3) = STR_TRUE Or par_mac.opt.cut_pat_auto(1) = STR_TRUE Or par_mac.opt.cut_pat_auto(2) = STR_TRUE Or par_mac.opt.cut_pat_auto(3) = STR_TRUE Then
        cmb_cut_to_mark(i).AddItem msg("msg_cut_pat")
        cmb_cut_to_mark(i).ItemData(cmb_cut_to_mark(i).NewIndex) = PRE_PATTERN
      End If
    Next i
    For i = 1 To 3
      txt_liv(i - 1).ListIndex = -1
      For k = 0 To txt_liv(i - 1).ListCount - 1
        If txt_liv(i - 1).ItemData(k) = val(.ord(i).Ord_liv) Then
          txt_liv(i - 1).ListIndex = k
        End If
      Next k
      cmb_cut_to_mark(i - 1).ListIndex = -1
      For k = 0 To cmb_cut_to_mark(i - 1).ListCount - 1
        If Trim$(.ord(i).Ord_liv) <> "" Then
          If val(.ord(i).cut_to_mark) = 0 Then
              cmb_cut_to_mark(i - 1).ListIndex = 0
              Exit For
          Else
              If cmb_cut_to_mark(i - 1).ItemData(k) = val(.ord(i).cut_to_mark) Then
                cmb_cut_to_mark(i - 1).ListIndex = k
              End If
          End If
        End If
      Next k
      If Trim$(.ord(i).Ord_liv) <> "" Then
          txt_lun_for(i - 1).Text = IIf(abi_mis_ang, con_met_to_fet(Trim$(.ord(i).met)), .ord(i).met)
          txt_cod_com(i - 1).Text = .ord(i).com.cod_com_ord
          txt_cod_cli(i - 1).Text = .ord(i).com.cod_cli
          txt_nom_cli(i - 1).Text = .ord(i).nom_cli
          If abi_mis_ang Then
            txt_sca_lar(i - 1).Text = con_dmm_to_inc(Trim$(.ord(i).sca_lar))
            txt_sca_lun(i - 1).Text = con_dmm_to_inc(Trim$(.ord(i).sca_lun))
            
          Else
            txt_sca_lar(i - 1).Text = CStr(val(.ord(i).sca_lar) \ TODECMM)
            txt_sca_lun(i - 1).Text = CStr(val(.ord(i).sca_lun) \ TODECMM)
            'Iciliouff 20-11-2003
            Ins_ord.cmb_pat_cod(i - 1).Text = .ord(i).pat_cod            'Codice pattern
            'RFAZZI_FI 07_01_08
            'txt_ofs_cut(i - 1).Text = Trim$(.ord(i).Ofs_Cut)              'Posizione laterale Pattern
            txt_pos_lat_pat(i - 1).Text = Trim$(.ord(i).pos_pattern)             'Posizione laterale Pattern
            txt_pat_con(i - 1).Text = Trim$(.ord(i).pat_con)            'Contrasto
            txt_fin_pri(i - 1).Text = Trim$(.ord(i).fin_pri)             'Finestra prima
            txt_fin_dop(i - 1).Text = Trim$(.ord(i).fin_dop)            'Finestra dopo
          End If
          txt_resa(i - 1).Text = .ord(i).Sca_per_tag
          txt_num_tot_fog(i - 1).Text = .ord(i).sca_tot_num
      Else
          txt_liv(i - 1).ListIndex = -1
          cmb_cut_to_mark(i - 1).ListIndex = -1
          txt_lun_for(i - 1).Text = ""
          txt_cod_com(i - 1).Text = ""
          txt_cod_cli(i - 1).Text = ""
          txt_nom_cli(i - 1).Text = ""
          txt_sca_lar(i - 1).Text = ""
          txt_sca_lun(i - 1).Text = ""
          txt_resa(i - 1).Text = ""
          txt_num_tot_fog(i - 1).Text = ""
          'Iciliouff 20-11-2003
          cmb_pat_cod(i - 1).Text = ""            'Codice pattern
          'RFAZZI_FI 07_01_08
          'txt_ofs_cut(i - 1).Text = ""            'Offset di taglio pattern
          txt_pos_lat_pat(i - 1).Text = ""
          txt_pat_con(i - 1).Text = ""            'Contrasto
          txt_fin_pri(i - 1).Text = ""            'Finestra prima
          txt_fin_dop(i - 1).Text = ""            'Finestra dopo
      End If
    Next i
End With

End Sub


Private Sub cmb_carta_GotFocus()
On Error GoTo STAG_Gest_ERR
If Not flg_rev Then
  If val_ins_ord(Me.ActiveControl.hwnd, 0) Then
    cmb_onda.ForeColor = vbBlack
  Else
    cmb_onda.ForeColor = vbRed
  End If
Else
  Call val_ins_ord(Me.ActiveControl.hwnd, 0)
End If

Exit Sub
STAG_Gest_ERR:
log_str "on error intervenuta in cmb_carta_GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next
End Sub

Private Sub cmb_carta_KeyDown(KeyCode As Integer, Shift As Integer)
Form_KeyDown KeyCode, Shift
eve_key.frm_KeyDown KeyCode, Shift
ges_vbkeydbCombo KeyCode, Shift
End Sub


Private Sub cmb_carta_LostFocus()
cmb_carta.ForeColor = vbBlack
End Sub

Private Sub cmb_cut_to_mark_GotFocus(Index As Integer)
On Error GoTo STAG_Gest_ERR
'ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
If Not flg_rev Then
  If val_ins_ord(Me.ActiveControl.hwnd, Index) Then
    txt_num_tot_fog(Index).ForeColor = vbBlack
    'If par_mac.opt.cut_mar = "0" Then cmb_cut_to_mark(Index).ListIndex = 0
  Else
    txt_num_tot_fog(Index).ForeColor = vbRed
  End If
Else
  Call val_ins_ord(Me.ActiveControl.hwnd, Index)
End If

Exit Sub
STAG_Gest_ERR:
log_str "on error intervenuta in cmb_cut_to_mark_GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next
End Sub


Private Sub cmb_cut_to_mark_KeyPress(Index As Integer, KeyAscii As Integer)
  If KeyAscii = vbKeyReturn Then
     KeyAscii = 0
  Else
    KeyAscii = get_num(KeyAscii, , True)
  End If
  
End Sub

Private Sub cmb_cut_to_mark_KeyUp(Index As Integer, KeyCode As Integer, Shift As Integer)
  
  If cmb_cut_to_mark(Index).ListIndex <> -1 Then
    abi_con_cut_pat cmb_cut_to_mark(Index).ItemData(cmb_cut_to_mark(Index).ListIndex) = PRE_PATTERN, Index
  Else
    abi_con_cut_pat False, Index
  End If

End Sub

Private Sub cmb_onda_Change()
On Error GoTo STAG_Gest_ERR
  
  cmb_onda.ForeColor = vbBlack
  If Format$(cmb_onda.Text) <> "" Then
    dat_car.RecordSource = "select carta from onde where onda='" + cmb_onda.Text + "'"
    dat_car.Refresh
    cmb_carta.Text = dat_car.Recordset("carta")
  End If
  Exit Sub
  
STAG_Gest_ERR:
  Resume Next
End Sub

Private Sub cmb_onda_GotFocus()
On Error GoTo STAG_Gest_ERR
If Not flg_rev Then
  If val_ins_ord(Me.ActiveControl.hwnd, 0) Then
    txt_cod_abb.ForeColor = vbBlack
  Else
    txt_cod_abb.ForeColor = vbRed
    Exit Sub
  End If
Else
  Call val_ins_ord(Me.ActiveControl.hwnd, 0)
End If

If Format$(cmb_onda.Text) = "" Then 'se non ho ancora inserito un'onda, metto la prima del db
  dat_ond.RecordSource = "select distinct onda from onde"
  dat_ond.Refresh
  cmb_onda.Text = dat_ond.Recordset("onda")
End If

Exit Sub
STAG_Gest_ERR:
log_str "on error intervenuta in cmb_onda_GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next

End Sub


Private Sub cmb_onda_KeyDown(KeyCode As Integer, Shift As Integer)
  Form_KeyDown KeyCode, Shift
  eve_key.frm_KeyDown KeyCode, Shift
  ges_vbkeydbCombo KeyCode, Shift
End Sub


Private Sub cmb_onda_LostFocus()
cmb_onda.ForeColor = vbBlack
End Sub

Private Sub cmb_pat_cod_Change(Index As Integer)
On Error GoTo STAG_Gest_ERR
Static old_pat_con(0 To 2) As String
Dim rs As Recordset

  cmb_pat_cod(Index).ForeColor = vbBlack
  If Format$(cmb_pat_cod(Index).Text) = "" Then
    dat_pat.RecordSource = "select * from PATTERN order by CODICE"
    dat_pat.Refresh
  End If
  
  'Cambio il valore di contrasto se necessario
  Set rs = db.OpenRecordset("select CONTRASTO from PATTERN where Ucase(CODICE)='" + UCase$(cmb_pat_cod(Index).Text) + "'", dbOpenSnapshot)
  If rs.RecordCount > 0 And old_pat_con(Index) <> cmb_pat_cod(Index).Text Then txt_pat_con(Index).Text = rs("CONTRASTO")
  rs.Close
  old_pat_con(Index) = cmb_pat_cod(Index).Text
  Exit Sub
  
STAG_Gest_ERR:
  Resume Next
  
End Sub

Private Sub cmb_pat_cod_GotFocus(Index As Integer)
On Error GoTo STAG_Gest_ERR
Dim rs As Recordset

  If Not flg_rev Then
    If val_ins_ord(Me.ActiveControl.hwnd, Index) Then
      txt_num_tot_fog(Index).ForeColor = vbBlack
    Else
      txt_num_tot_fog(Index).ForeColor = vbRed
    End If
  Else
    Call val_ins_ord(Me.ActiveControl.hwnd, Index)
  End If
  
  'Se il Codice non è contenuta nel DB PATTERN allora aggiorno il controllo con i CODICI esistenti
  Set rs = db.OpenRecordset("select * from PATTERN where Ucase(CODICE)='" + UCase$(cmb_pat_cod(Index).Text) + "'", dbOpenSnapshot)
  If rs.RecordCount = 0 Then
    dat_pat.RecordSource = "select * from PATTERN order by CODICE"
    dat_pat.Refresh
    cmb_pat_cod.item(Index).Refresh
    If dat_pat.Recordset.RecordCount > 0 Then
      cmb_pat_cod(Index).Text = dat_pat.Recordset("CODICE")
    Else
      cmb_pat_cod(Index).Text = ""
    End If
  End If
  rs.Close
  
  
  Exit Sub
STAG_Gest_ERR:
  log_str "on error intervenuta in cmb_pat_cod_GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next

End Sub

Private Sub cmb_pat_cod_KeyDown(Index As Integer, KeyCode As Integer, Shift As Integer)
  Form_KeyDown KeyCode, Shift
  eve_key.frm_KeyDown KeyCode, Shift
  ges_vbkeydbCombo KeyCode, Shift
End Sub

Private Sub eve_key_esc(Shift As Integer)
Select Case Shift
  Case 0
    tip_op = OLI_NUL
    clear_cell Me
    txt_indice = ""
    If ord_mac.Count > 0 Then   ' se la lista degli ordini non è vuota
      riempi_celle (ord_mac.leg_pun_ord)
    End If
    disable_frames Me
    txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
    txt_idle.SetFocus
  Case Else
End Select
End Sub

Private Sub eve_key_F1(Shift As Integer)
'nuovo
Dim app As String

Select Case Shift
  Case 0
    If tip_op = OLI_NUL Then
      If ord_mac.Count < MAX_NUM_ORD Then
        If sta_tag.MAC_IN_MANUALE(1) Or Not sta_tag.spde_OK Then
          'azzero le variabili di memorizzazione
          ult_con = 0 'memorizzo l'handle dell'ultimo controllo per poterci tornare in caso di errore
          ult_ind = -1 'memorizzo l'indice (ovvero il livello) dell'ultimo controllo
          enable_frames Me
          tip_op = OLI_NEW
          txt_met = ""
          contatore = ord_mac.Count + 1
          clear_cell Me
          'Inizializzo la struttura var_ord_ute
          app = Space$(Len(var_ord_ute))
          CopyType var_ord_ute, app, Len(var_ord_ute)
          txt_indice = ord_mac.Count + 1
          txt_id.TabStop = False
          txt_cod_abb.SetFocus
        Else
          messaggio msg("imp_ese_ope") + ". " + msg("mac_in_auto"), True, False
        End If
      Else
        messaggio msg("rag_num_max"), True, False
      End If
    End If
  Case Else
End Select
End Sub

Private Sub eve_key_F10(Shift As Integer)
Select Case Shift
  Case 0
    If tip_op = OLI_NUL Then eve_key.chg pag_pro
  Case Else
End Select
End Sub

Private Sub eve_key_F2(Shift As Integer)
Select Case Shift
  Case 0
    'modifica
    If tip_op = OLI_NUL Then
      If ord_mac.Count > 0 Then
        If sta_tag.MAC_IN_MANUALE(1) Or Not sta_tag.spde_OK Then
            'azzero le variabili di memorizzazione
            ult_con = 0 'memorizzo l'handle dell'ultimo controllo per poterci tornare in caso di errore
            ult_ind = -1 'memorizzo l'indice (ovvero il livello) dell'ultimo controllo
            enable_frames Me
            txt_cod_abb.SetFocus
            txt_id.TabStop = False
            tip_op = OLI_MOD
        Else
            messaggio msg("imp_ese_ope") + ". " + msg("mac_in_auto"), True, False
        End If
      End If
    End If
  Case Else
End Select
End Sub


Private Sub eve_key_F3(Shift As Integer)
'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
'nuovo ordine nullo
Dim app As String

Select Case Shift
  Case 0
    If tip_op = OLI_NUL Then
      If ord_mac.Count < MAX_NUM_ORD Then
        If sta_tag.MAC_IN_MANUALE(1) Or Not sta_tag.spde_OK Then
          tip_op = OLI_NEW_NULL
          'azzero le variabili di memorizzazione
          ult_con = 0 'memorizzo l'handle dell'ultimo controllo per poterci tornare in caso di errore
          ult_ind = -1 'memorizzo l'indice (ovvero il livello) dell'ultimo controllo
          contatore = ord_mac.Count + 1
          createNullOrderForm
          'Inizializzo la struttura var_ord_ute
          app = Space$(Len(var_ord_ute))
          CopyType var_ord_ute, app, Len(var_ord_ute)
          
        Else
          messaggio msg("imp_ese_ope") + ". " + msg("mac_in_auto"), True, False
        End If
      Else
        messaggio msg("rag_num_max"), True, False
      End If
    End If
  Case Else
End Select
End Sub

Private Sub eve_key_F4(Shift As Integer)
'salva nuovo ordine o modifica l'ordine visualizzato
Dim ordine As Tip_Ordine
Dim risp As cod_ret
Dim i As Integer
Dim met As Long
Dim ret As Integer

On Local Error GoTo GEST_ERR:
Select Case Shift
  Case 0
    ult_con = 0
    'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
    'If tip_op = OLI_NEW Or tip_op = OLI_MOD Then
    If tip_op = OLI_NEW Or tip_op = OLI_MOD Or tip_op = OLI_NEW_NULL Then
        messaggio msg("ope_in_cor"), False, False
        
        'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
        If tip_op = OLI_NEW_NULL Then
          get_new_dat var_ord_ute
        End If
        
        val_ord var_ord_ute, True     'Iciliouff 20-11-2003
        ret = var_ord_ute.ret
        If ret <> RET_OK Then
            messaggio msg(msg_err_val_ord(var_ord_ute.ret)), is_All:=True
            txt_cod_abb.SetFocus
        Else
            Set temp = New ClasseOrdine
            CopyType ordine, Space$(Len(ordine)), Len(ordine)
            'temp.ordine = Ord_str(ordine)
            met = 0
            For i = 1 To 3
              If Trim$(var_ord_ute.rec.ord(i).Ord_liv) <> "" And val(var_ord_ute.rec.ord(i).Ord_liv) <> 0 Then
                If CLng(var_ord_ute.rec.ord(i).met) > met Then met = CLng(var_ord_ute.rec.ord(i).met)
              End If
            Next i
            var_ord_ute.rec.met_tot_ord = CStr(met)
            ordine = var_ord_ute.rec
    '        With ordine
    '          .alt_bob = CStr(val(txt_alt_bob) * TODECMM)
    '          .cod_ord_abb = txt_cod_abb
    '          .tip_car.cod_com_car = cmb_carta.Text
    '          .tip_car.Cod_ond = cmb_onda.Text
    '          For i = 1 To 3
    '            If txt_liv(i - 1).ListIndex = -1 Then
    '              .ord(i).Ord_liv = Space$(Len(.ord(i).Ord_liv))
    '            Else
    '              .ord(i).Ord_liv = Format$(txt_liv(i - 1).ItemData(txt_liv(i - 1).ListIndex))
    '              som = CLng(((val(txt_num_tot_fog(i - 1)) / val(txt_resa(i - 1))) / 1000) * val(txt_sca_lun(i - 1)))
    '              .ord(i).met = CStr(som)
    '              .ord(i).com.cod_com_ord = txt_cod_com(i - 1).Text
    '              .ord(i).com.cod_cli = txt_cod_cli(i - 1).Text
    '              .ord(i).nom_cli = txt_nom_cli(i - 1).Text
    '              .ord(i).sca_lar = CStr(val(txt_sca_lar(i - 1).Text) * TODECMM)
    '              .ord(i).sca_lun = CStr(val(txt_sca_lun(i - 1).Text) * TODECMM)
    '              .ord(i).Sca_per_tag = txt_resa(i - 1).Text
    '              .ord(i).sca_tot_num = txt_num_tot_fog(i - 1).Text
    '            End If
    '            If cmb_cut_to_mark(i - 1).ListIndex = -1 Then
    '              .ord(i).cut_to_mark = Space$(Len(.ord(i).cut_to_mark))
    '            Else
    '              .ord(i).cut_to_mark = Format$(cmb_cut_to_mark(i - 1).ItemData(cmb_cut_to_mark(i - 1).ListIndex))
    '            End If
    '          Next i
    '        End With
            temp.ordine = Ord_str(ordine)
            
            'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
            'If tip_op = OLI_NEW Then risp = ord_mac.add(temp)
            If tip_op = OLI_NEW Or tip_op = OLI_NEW_NULL Then risp = ord_mac.add(temp)
            
            If tip_op = OLI_MOD Then risp = ord_mac.Modifica(temp, ord_mac.leg_pun_ord)
            
            'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
            'If risp = RET_BAD And tip_op = OLI_NEW Then
            If risp = RET_BAD And (tip_op = OLI_NEW Or tip_op = OLI_NEW_NULL) Then
                messaggio msg("rag_num_max"), True, False
            End If
            If risp = RET_OK And ord_mac.Count > 0 Then
                messaggio msg("ope_eff"), False, False
            End If
            
            'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
            'If risp = RET_OK And tip_op = OLI_NEW Then
            If risp = RET_OK And (tip_op = OLI_NEW Or tip_op = OLI_NEW_NULL) Then
                ord_mac.scr_pun_ord (ord_mac.Count)
            End If
            If ord_mac.Count > 0 Then
                  riempi_celle (ord_mac.leg_pun_ord)
            End If
            tip_op = OLI_NUL
            disable_frames Me
            txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
            txt_idle.SetFocus
        End If
    End If
    
    Exit Sub
  
  Case Else

End Select
GEST_ERR:
If err.Number = 6 Then 'overflow
    log_str "on error intervenuta in eve_key F4 della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
    Resume Next
Else
    log_str "on error intervenuta in eve_key F4 della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
End If
End Sub

Private Sub eve_key_F5(Shift As Integer)

If tip_op = OLI_NUL Then
  Select Case Shift
  Case 0 'precedente
    ord_mac.pun_ord_mov_prev
    If ord_mac.leg_pun_ord > 0 Then riempi_celle (ord_mac.leg_pun_ord)
    txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
    txt_idle.SetFocus
  Case Else
  End Select
End If
End Sub

Private Sub eve_key_F6(Shift As Integer)

If tip_op = OLI_NUL Then
  Select Case Shift
  Case 0 'successivo
    ord_mac.pun_ord_mov_next
    If ord_mac.leg_pun_ord > 0 Then riempi_celle (ord_mac.leg_pun_ord)
    tip_op = OLI_NUL
    txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
    txt_idle.SetFocus
  Case Else
  End Select
End If
End Sub

Private Sub eve_key_F7(Shift As Integer)
'ordine corrente
Select Case Shift
  Case 0
  If tip_op = OLI_NUL Then
      If ord_mac.Count > 0 Then
        contatore = 1
        ord_mac.scr_pun_ord (1)
        riempi_celle (contatore)
        tip_op = OLI_NUL
        txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
        txt_idle.SetFocus
      End If
  End If
  Case Else
End Select
End Sub

Private Sub eve_key_F8(Shift As Integer)
Select Case Shift
  Case 0
  If tip_op = OLI_NUL Then eve_key.chg pag_lis_ese
  Case Else
End Select
End Sub

Private Sub eve_key_F9(Shift As Integer)
Select Case Shift
  Case 0
  If tip_op = OLI_NUL Then eve_key.chg pag_lis_ord
  Case Else
End Select
End Sub


Private Sub eve_ord_mac_Refresh(Shift As Boolean)

If Shift Then
    Select Case tip_op
    Case OLI_NUL
        'gestisco lo shift in fase di visualizzazione
        If ord_mac.Count > 0 And val(txt_indice) > 1 Then
            txt_indice = (val(txt_indice) - 1)
            ord_mac.scr_pun_ord (val(txt_indice))
        Else
            If ord_mac.Count > 0 And val(txt_indice) = 1 Then
                riempi_celle (1)
            End If
        End If
        If ord_mac.Count = 0 Then
            clear_cell Me
            txt_indice = ""
        End If
            
    'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
    'Case OLI_NEW
    Case OLI_NEW, OLI_NEW_NULL
        'gestione dello shift in fase di inserimento
        messaggio msg("ope_ann"), True, False
        If ord_mac.Count > 0 Then
            ord_mac.scr_pun_ord (ord_mac.Count)
            riempi_celle (ord_mac.Count)
        Else
            clear_cell Me
            ord_mac.scr_pun_ord (0)
            txt_indice = ""
        End If
        disable_frames Me
        tip_op = OLI_NUL
        txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
        txt_idle.SetFocus
    
    Case OLI_MOD
        messaggio msg("ope_ann"), True, False
        If txt_indice > 1 Then
          ord_mac.scr_pun_ord (val(txt_indice) - 1)
          riempi_celle (val(txt_indice) - 1)
        Else
            If ord_mac.Count > 1 Then
              ord_mac.scr_pun_ord (1)
              riempi_celle (1)
            Else
              clear_cell Me
              ord_mac.scr_pun_ord (0)
              txt_indice = ""
            End If
        End If
        disable_frames Me
        tip_op = OLI_NUL
        txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
        txt_idle.SetFocus
    End Select
End If
End Sub

Private Sub eve_sta_modo(Sta_mac As sta_mod)
If Sta_mac = automatico And tip_op <> OLI_NUL Then
    Select Case tip_op

    'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
    'Case OLI_NEW
     Case OLI_NEW, OLI_NEW_NULL
        'gestione della transizione a "macchina in automatico" in fase di inserimento
        messaggio msg("ope_ann"), True, False
        If ord_mac.Count > 0 Then
            ord_mac.scr_pun_ord (ord_mac.Count)
            riempi_celle (ord_mac.Count)
        Else
            clear_cell Me
            ord_mac.scr_pun_ord (0)
            txt_indice = ""
        End If
        disable_frames Me
        tip_op = OLI_NUL
        txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
        txt_idle.SetFocus

    Case OLI_MOD
        'gestione della transizione a "macchina in automatico" in fase di modifica
        messaggio msg("ope_ann"), True, False
        riempi_celle (val(txt_indice))
        disable_frames Me
        tip_op = OLI_NUL
        txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
        txt_idle.SetFocus

    End Select
End If
End Sub


Private Sub Form_Activate()

  disable_frames Me
  tip_op = OLI_NUL
  txt_idle.Enabled = True ' Arturo uff 05-11-02 x disabilitazione controlli su frame
  txt_idle.SetFocus
  If pag_pre.name <> "pag_lis_ord" And ord_mac.Count > 0 Then ord_mac.scr_pun_ord (1)
  contatore = ord_mac.leg_pun_ord
  dat_ond.Refresh
  'Iciliouff 20-11-2003
  dat_pat.RecordSource = "select * from PATTERN order by CODICE"
  dat_pat.Refresh
  Dim i As Integer
  For i = cmb_pat_cod.LBound To cmb_pat_cod.UBound
    cmb_pat_cod.item(i).Refresh
  Next i
  'Robertouff 26-11-2007
  'agrassi_FI15-07-08
  'abi_con_cut_pat par_mac.opt.cut_pat = STR_TRUE Or par_mac.opt.cut_pat_auto
  For i = 0 To 2
    'agrassi_FI17-12-08
    'abi_con_cut_pat par_mac.opt.cut_pat(i + i) = STR_TRUE, i
    
    'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
    'abi_con_cut_pat par_mac.opt.cut_pat(i + 1) = STR_TRUE Or par_mac.opt.cut_pat_auto, i
    abi_con_cut_pat par_mac.opt.cut_pat(i + 1) = STR_TRUE Or par_mac.opt.cut_pat_auto(i + 1) = STR_TRUE, i
  Next i
  ini_txt
  If ord_mac.Count > 0 Then
    riempi_celle (contatore)
  Else
    clear_cell Me
    txt_indice = ""
  End If
  
End Sub

Sub ini_txt()
Dim ord As Tip_Ordine

With ord
  lun_txt_alt_bob = Len(.alt_bob) - 1 + IIf(abi_mis_ang, CAR_AGG_INC, 0)
  lun_txt_sca_lar = Len(.ord(1).sca_lar) - 1 + IIf(abi_mis_ang, CAR_AGG_INC, 0)
  lun_txt_sca_lun = Len(.ord(1).sca_lun) - 1 + IIf(abi_mis_ang, CAR_AGG_INC, 0)
End With
End Sub

Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
On Error GoTo STAG_Gest_ERR
  
  If tip_op = OLI_MOD Or tip_op = OLI_NEW Then
    If ActiveControl Is Nothing Then Exit Sub
    'chk_BlocNum
    Select Case KeyCode
      Case vbKeyReturn, vbKeyDown
        chk_shift 'controllo dello stato del tasto SHIFT
        KeyCode = 0
        SendKeys "{TAB}"
        flg_rev = False
      Case vbKeyUp
        chk_shift 'controllo dello stato del tasto SHIFT
        If KeyCode = vbKeyUp And Shift = 0 And LCase$(Screen.ActiveControl.name) = "txt_cod_abb" Then
          KeyCode = 0
          Shift = 0
          Ins_ord.txt_cod_abb.SetFocus
          Exit Sub
        End If
        KeyCode = 0
        SendKeys "+{TAB}"
        flg_rev = True 'sto tornando sui miei passi......
    End Select
  End If
  Exit Sub

STAG_Gest_ERR:
  log_str "on error intervenuta in Form_KeyDown della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
  
End Sub
Private Sub Form_Load()
Debug.Print "Inizio Load " & Me.name
tip_op = OLI_NUL
Set eve_ord_mac = ord_mac
Set eve_sta = sta_tag
Set eve_key = New key
eve_key.create Me
Carica Me
contatore = ord_mac.leg_pun_ord
ini_form
If ord_mac.Count > 0 Then riempi_celle (contatore)
Debug.Print "Fine Load " & Me.name
Debug.Print ""
End Sub

Private Sub txt_alt_bob_GotFocus()
On Error GoTo STAG_Gest_ERR
txt_alt_bob.BackColor = vbBlack
txt_alt_bob.ForeColor = vbWhite
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
If Not flg_rev Then
  If val_ins_ord(Me.ActiveControl.hwnd, 0) Then
    cmb_carta.ForeColor = vbBlack
  Else
    cmb_carta.ForeColor = vbRed
  End If
Else
  Call val_ins_ord(Me.ActiveControl.hwnd, 0)
End If

Exit Sub
STAG_Gest_ERR:
log_str "on error intervenuta in txt_alt_bob_GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next

End Sub


Private Sub txt_alt_bob_KeyPress(KeyAscii As Integer)
If KeyAscii = vbKeyReturn Then
   KeyAscii = 0
Else
  KeyAscii = get_num(KeyAscii, True)
  If KeyAscii <> 0 Then
    If ActiveControl.SelLength > 0 Then Exit Sub
    If Not acc_new_car(ActiveControl, KeyAscii) And KeyAscii >= 32 Then KeyAscii = 0
  End If
End If
End Sub


Private Sub txt_alt_bob_LostFocus()
txt_alt_bob.BackColor = vbWhite
txt_alt_bob.ForeColor = vbBlack
End Sub

Private Sub txt_cod_abb_GotFocus()
On Error GoTo STAG_Gest_ERR
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
txt_cod_abb.BackColor = vbBlack
txt_cod_abb.ForeColor = vbWhite
'If (var_ord_ute.ret = RET_OK) Or (var_ord_ute.ret = RET_ZRO) Then
  Call val_ins_ord(Me.ActiveControl.hwnd, 0)
'End If

Exit Sub
STAG_Gest_ERR:
log_str "on error intervenuta in txt_cod_abb_GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next

End Sub




Private Sub txt_cod_abb_KeyPress(KeyAscii As Integer)
If KeyAscii = vbKeyReturn Then
   KeyAscii = 0
End If
End Sub


Private Sub txt_cod_abb_LostFocus()
txt_cod_abb.BackColor = vbWhite
txt_cod_abb.ForeColor = vbBlack
End Sub

Private Sub txt_cod_cli_GotFocus(Index As Integer)
On Error GoTo STAG_Gest_ERR
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
txt_cod_cli(Index).BackColor = vbBlack
txt_cod_cli(Index).ForeColor = vbWhite
If Not flg_rev Then
  If val_ins_ord(Me.ActiveControl.hwnd, 0) Then
    txt_cod_com(Index).ForeColor = vbBlack
  Else
    txt_cod_com(Index).ForeColor = vbRed
  End If
Else
  Call val_ins_ord(Me.ActiveControl.hwnd, 0)
End If

Exit Sub
STAG_Gest_ERR:
log_str "on error intervenuta in txt_cod_cli_GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next

End Sub


Private Sub txt_cod_cli_LostFocus(Index As Integer)
txt_cod_cli(Index).BackColor = vbWhite
txt_cod_cli(Index).ForeColor = vbBlack
End Sub

Private Sub txt_cod_com_GotFocus(Index As Integer)

On Error GoTo STAG_Gest_ERR
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
txt_cod_com(Index).BackColor = vbBlack
txt_cod_com(Index).ForeColor = vbWhite
If Not flg_rev Then
  If val_ins_ord(Me.ActiveControl.hwnd, Index) Then
    txt_liv(Index).ForeColor = MED_CEL
  Else
    txt_liv(Index).ForeColor = vbRed
  End If
Else
  Call val_ins_ord(Me.ActiveControl.hwnd, Index)
End If

Exit Sub
STAG_Gest_ERR:
log_str "on error intervenuta in txt_cod_com_GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next

End Sub


Private Sub txt_cod_com_LostFocus(Index As Integer)
txt_cod_com(Index).ForeColor = vbBlack
txt_cod_com(Index).BackColor = vbWhite
End Sub


Private Sub txt_fin_dop_GotFocus(Index As Integer)
On Error GoTo STAG_Gest_ERR
  
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
  txt_fin_dop(Index).BackColor = vbBlack
  txt_fin_dop(Index).ForeColor = vbWhite
  If Not flg_rev Then
    If val_ins_ord(Me.ActiveControl.hwnd, Index) Then txt_fin_pri(Index).ForeColor = vbBlack Else txt_fin_pri(Index).ForeColor = vbRed
  Else
    Call val_ins_ord(Me.ActiveControl.hwnd, Index)
  End If
  Exit Sub
  
STAG_Gest_ERR:
  log_str "on error intervenuta in" & ActiveControl.name & " GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub


Private Sub txt_fin_dop_KeyPress(Index As Integer, KeyAscii As Integer)
  If KeyAscii = vbKeyReturn Then
     KeyAscii = 0
  Else
    KeyAscii = get_num(KeyAscii)
  End If
End Sub


Private Sub txt_fin_dop_LostFocus(Index As Integer)
On Error GoTo STAG_Gest_ERR
  
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
  txt_fin_dop(Index).BackColor = vbWhite
  txt_fin_dop(Index).ForeColor = vbBlack
  Exit Sub
  
STAG_Gest_ERR:
  log_str "on error intervenuta in txt_fin_pri_LostFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub txt_fin_pri_GotFocus(Index As Integer)
On Error GoTo STAG_Gest_ERR
  
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
  txt_fin_pri(Index).BackColor = vbBlack
  txt_fin_pri(Index).ForeColor = vbWhite
  If Not flg_rev Then
    If val_ins_ord(Me.ActiveControl.hwnd, Index) Then txt_pat_con(Index).ForeColor = vbBlack Else txt_pat_con(Index).ForeColor = vbRed
  Else
    Call val_ins_ord(Me.ActiveControl.hwnd, Index)
  End If
  Exit Sub
  
STAG_Gest_ERR:
  log_str "on error intervenuta in" & ActiveControl.name & " GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub


Private Sub txt_fin_pri_KeyPress(Index As Integer, KeyAscii As Integer)
  If KeyAscii = vbKeyReturn Then
     KeyAscii = 0
  Else
    KeyAscii = get_num(KeyAscii)
  End If
End Sub


Private Sub txt_fin_pri_LostFocus(Index As Integer)
On Error GoTo STAG_Gest_ERR
  
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
  txt_fin_pri(Index).BackColor = vbWhite
  txt_fin_pri(Index).ForeColor = vbBlack
  Exit Sub
  
STAG_Gest_ERR:
  log_str "on error intervenuta in txt_fin_pri_LostFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub


Private Sub txt_liv_GotFocus(Index As Integer)
On Error GoTo STAG_Gest_ERR
If Not flg_rev Then
    If val_ins_ord(Me.ActiveControl.hwnd, Index) Then
        If Index = 0 Then
            txt_alt_bob.ForeColor = vbBlack
        Else
            cmb_cut_to_mark(Index - 1).ForeColor = vbBlack
        End If
    Else
        If Index = 0 Then
            txt_alt_bob.ForeColor = vbRed
        Else
            cmb_cut_to_mark(Index - 1).ForeColor = vbRed
        End If
    End If
Else
    Call val_ins_ord(Me.ActiveControl.hwnd, Index)
End If

Exit Sub
STAG_Gest_ERR:
log_str "on error intervenuta in txt_liv_GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next
End Sub


Private Sub txt_nom_cli_GotFocus(Index As Integer)

ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
txt_nom_cli(Index).BackColor = vbBlack
txt_nom_cli(Index).ForeColor = vbWhite

End Sub


Private Sub txt_nom_cli_LostFocus(Index As Integer)
txt_nom_cli(Index).BackColor = vbWhite
txt_nom_cli(Index).ForeColor = vbBlack
End Sub

Private Sub txt_num_tot_fog_Change(Index As Integer)
'Dim som As Long
'
'On Local Error GoTo GEST_ERR:
'If val(txt_resa(Index)) <> 0 Then
'  som = CLng(((val(txt_num_tot_fog(Index)) / val(txt_resa(Index))) / 1000) * val(txt_sca_lun(Index)))
'  txt_lun_for(Index) = CStr(som)
'  If val(txt_met) < som Then
'    txt_met = CStr(som)
'  End If
'Else
'  If Trim$(txt_met) = "" Then
'    txt_met = "0"
'    txt_lun_for(Index) = "0"
'  End If
'End If
'Exit Sub
'
'GEST_ERR:
'If err.Number = 6 Then 'overflow
'    log_str "on error intervenuta in txt_num_tot_fog(" & Index & ")_Change della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
'    Resume Next
'Else
'    log_str "on error intervenuta in txt_num_tot_fog(" & Index & ")_Change della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
'End If

End Sub

Private Sub txt_num_tot_fog_GotFocus(Index As Integer)
On Error GoTo STAG_Gest_ERR
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
txt_num_tot_fog(Index).BackColor = vbBlack
txt_num_tot_fog(Index).ForeColor = vbWhite
If Not flg_rev Then
  If val_ins_ord(Me.ActiveControl.hwnd, Index) Then
    txt_resa(Index).ForeColor = vbBlack
  Else
    txt_resa(Index).ForeColor = vbRed
  End If
Else
  Call val_ins_ord(Me.ActiveControl.hwnd, Index)
End If

Exit Sub
STAG_Gest_ERR:
log_str "on error intervenuta in txt_num_tot_fog_GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next
End Sub



Private Sub txt_num_tot_fog_KeyPress(Index As Integer, KeyAscii As Integer)
  If KeyAscii = vbKeyReturn Then
     KeyAscii = 0
  Else
    KeyAscii = get_num(KeyAscii)
  End If
End Sub

Private Sub txt_num_tot_fog_LostFocus(Index As Integer)
Dim som As Long

On Local Error GoTo GEST_ERR:
txt_num_tot_fog(Index).BackColor = vbWhite
txt_num_tot_fog(Index).ForeColor = vbBlack
'If val(txt_resa(0)) <> 0 Then
'  som = CLng(((val(txt_num_tot_fog(Index)) / val(txt_resa(Index))) / 1000) * val(txt_sca_lun(Index)))
'  txt_met = CStr(som)
'  txt_lun_for(Index) = CStr(som)
'Else
'  txt_met = "0"
'  txt_lun_for(Index) = "0"
'End If
Exit Sub
    
GEST_ERR:
If err.Number = 6 Then 'overflow
    log_str "on error intervenuta in txt_num_tot_fog(" & Index & ")_LostFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
    Resume Next
Else
    log_str "on error intervenuta in txt_num_tot_fog(" & Index & ")_LostFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
End If

End Sub


'RFAZZI_FI 07-01-08
Private Sub txt_pos_lat_pat_GotFocus(Index As Integer)
On Error GoTo STAG_Gest_ERR
  
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
  txt_pos_lat_pat(Index).BackColor = vbBlack
  txt_pos_lat_pat(Index).ForeColor = vbWhite
  If Not flg_rev Then
    val_ins_ord Me.ActiveControl.hwnd, Index
  Else
    Call val_ins_ord(Me.ActiveControl.hwnd, Index)
  End If
  Exit Sub
  
STAG_Gest_ERR:
  log_str "on error intervenuta in" & ActiveControl.name & " GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub txt_pos_lat_pat_KeyPress(Index As Integer, KeyAscii As Integer)
  If KeyAscii = vbKeyReturn Then
     KeyAscii = 0
  Else
    KeyAscii = get_num(KeyAscii, , True)
  End If
End Sub

'RFAZZI_FI 07-01-08
Private Sub txt_pos_lat_pat_LostFocus(Index As Integer)
Dim som As Long

On Local Error GoTo GEST_ERR:
  txt_pos_lat_pat(Index).BackColor = vbWhite
  txt_pos_lat_pat(Index).ForeColor = vbBlack
  Exit Sub
    
GEST_ERR:
  log_str "on error intervenuta in txt_pos_lat_pat_LostFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub txt_pat_con_GotFocus(Index As Integer)
On Error GoTo STAG_Gest_ERR
  
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
  txt_pat_con(Index).BackColor = vbBlack
  txt_pat_con(Index).ForeColor = vbWhite
  If Not flg_rev Then
    'RFAZZI_FI 07-01-08
    'If val_ins_ord(Me.ActiveControl.hwnd, Index) Then txt_ofs_cut(Index).ForeColor = vbBlack Else txt_ofs_cut(Index).ForeColor = vbRed
    If val_ins_ord(Me.ActiveControl.hwnd, Index) Then txt_pos_lat_pat(Index).ForeColor = vbBlack Else txt_pos_lat_pat(Index).ForeColor = vbRed
    
  Else
    Call val_ins_ord(Me.ActiveControl.hwnd, Index)
  End If
  Exit Sub
  
STAG_Gest_ERR:
  log_str "on error intervenuta in txt_pat_con_GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub txt_pat_con_KeyPress(Index As Integer, KeyAscii As Integer)
  If KeyAscii = vbKeyReturn Then
     KeyAscii = 0
  Else
    KeyAscii = get_num(KeyAscii)
  End If
End Sub


Private Sub txt_pat_con_LostFocus(Index As Integer)
On Error GoTo STAG_Gest_ERR
  
  ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
  txt_pat_con(Index).BackColor = vbWhite
  txt_pat_con(Index).ForeColor = vbBlack
  Exit Sub
  
STAG_Gest_ERR:
  log_str "on error intervenuta in txt_pat_con_LostFocus GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  Resume Next
End Sub

Private Sub txt_resa_Change(Index As Integer)
'Dim som As Long
'
'On Local Error GoTo GEST_ERR:
'If val(txt_resa(Index)) <> 0 Then
'  som = CLng(((val(txt_num_tot_fog(Index)) / val(txt_resa(Index))) / 1000) * val(txt_sca_lun(Index)))
'  txt_lun_for(Index) = CStr(som)
'  If val(txt_met) < som Then
'    txt_met = CStr(som)
'  End If
'Else
'  If Trim$(txt_met) = "" Then
'    txt_met = "0"
'    txt_lun_for(Index) = "0"
'  End If
'End If
'Exit Sub
'
'GEST_ERR:
'If err.Number = 6 Then 'overflow
'    log_str "on error intervenuta in txt_resa(" & Index & ")_Change della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
'    Resume Next
'Else
'    log_str "on error intervenuta in txt_resa(" & Index & ")_Change della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
'End If
End Sub

Private Sub txt_resa_GotFocus(Index As Integer)
On Error GoTo STAG_Gest_ERR
txt_resa(Index).BackColor = vbBlack
txt_resa(Index).ForeColor = vbWhite
If Not flg_rev Then
  If val_ins_ord(Me.ActiveControl.hwnd, Index) Then
    txt_sca_lun(Index).ForeColor = vbBlack
  Else
    txt_sca_lun(Index).ForeColor = vbRed
    Exit Sub
  End If
Else
  Call val_ins_ord(Me.ActiveControl.hwnd, Index)
End If
If Len(txt_resa(Index).Text) = 0 And tip_op = OLI_NEW Then txt_resa(Index).Text = "1"
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)

Exit Sub
STAG_Gest_ERR:
log_str "on error intervenuta in txt_resa_GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next
End Sub


Private Sub txt_resa_KeyPress(Index As Integer, KeyAscii As Integer)
If KeyAscii = vbKeyReturn Then
   KeyAscii = 0
Else
  KeyAscii = get_num(KeyAscii)
End If
End Sub

Private Sub txt_resa_LostFocus(Index As Integer)
txt_resa(Index).BackColor = vbWhite
txt_resa(Index).ForeColor = vbBlack
End Sub

Private Sub txt_sca_lar_GotFocus(Index As Integer)
On Error GoTo STAG_Gest_ERR
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
txt_sca_lar(Index).BackColor = vbBlack
txt_sca_lar(Index).ForeColor = vbWhite
If Not flg_rev Then
  If val_ins_ord(Me.ActiveControl.hwnd, Index) Then
    txt_nom_cli(Index).ForeColor = vbBlack
  Else
    txt_nom_cli(Index).ForeColor = vbRed
  End If
Else
  Call val_ins_ord(Me.ActiveControl.hwnd, Index)
End If

Exit Sub
STAG_Gest_ERR:
log_str "on error intervenuta in txt_sca_lar_GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next
End Sub


Private Sub txt_sca_lar_KeyPress(Index As Integer, KeyAscii As Integer)
If KeyAscii = vbKeyReturn Then
   KeyAscii = 0
Else
  KeyAscii = get_num(KeyAscii, True)
  If KeyAscii <> 0 Then
    If ActiveControl.SelLength > 0 Then Exit Sub
    If Not acc_new_car(ActiveControl, KeyAscii) And KeyAscii >= 32 Then KeyAscii = 0
  End If
End If

End Sub


Private Sub txt_sca_lar_LostFocus(Index As Integer)
txt_sca_lar(Index).ForeColor = vbBlack
txt_sca_lar(Index).BackColor = vbWhite
End Sub

Private Sub txt_sca_lun_Change(Index As Integer)
'Dim som As Long

'On Local Error GoTo GEST_ERR:
'If val(txt_resa(Index)) <> 0 Then
'  som = CLng(((val(txt_num_tot_fog(Index)) / val(txt_resa(Index)))) * (val(txt_sca_lun(Index)) / 1000))
'  txt_lun_for(Index) = CStr(som)
'  If val(txt_met) < som Then
'    txt_met = CStr(som)
'  End If
'Else
'  If Trim$(txt_met) = "" Then
'    txt_met = "0"
'    txt_lun_for(Index) = "0"
'  End If
'End If
'Exit Sub

'GEST_ERR:
'If err.Number = 6 Then 'overflow
'    log_str "on error intervenuta in txt_sca_lun(" & Index & ")_Change della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
'    Resume Next
'Else
'    log_str "on error intervenuta in txt_sca_lun(" & Index & ")_Change della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
'End If
End Sub

Private Sub txt_sca_lun_GotFocus(Index As Integer)
On Error GoTo STAG_Gest_ERR
txt_sca_lun(Index).BackColor = vbBlack
txt_sca_lun(Index).ForeColor = vbWhite
ActiveControl.SelStart = 0: ActiveControl.SelLength = Len(ActiveControl.Text)
If Not flg_rev Then
  If val_ins_ord(Me.ActiveControl.hwnd, Index) Then
    txt_sca_lar(Index).ForeColor = vbBlack
  Else
    txt_sca_lar(Index).ForeColor = vbRed
  End If
Else
  Call val_ins_ord(Me.ActiveControl.hwnd, Index)
End If

Exit Sub
STAG_Gest_ERR:
log_str "on error intervenuta in txt_sca_lun_GotFocus della form Ins_ord" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
Resume Next
End Sub


Private Sub txt_sca_lun_KeyPress(Index As Integer, KeyAscii As Integer)
If KeyAscii = vbKeyReturn Then
   KeyAscii = 0
Else
  KeyAscii = get_num(KeyAscii, True)
  If KeyAscii <> 0 Then
    If ActiveControl.SelLength > 0 Then Exit Sub
    If Not acc_new_car(ActiveControl, KeyAscii) And KeyAscii >= 32 Then KeyAscii = 0
  End If
End If
End Sub


Private Sub txt_sca_lun_LostFocus(Index As Integer)
txt_sca_lun(Index).ForeColor = vbBlack
txt_sca_lun(Index).BackColor = vbWhite
End Sub


