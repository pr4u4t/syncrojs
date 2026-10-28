Attribute VB_Name = "Principale"
Option Explicit

Dim szbuff As String * 100
Dim hwnd As Long
Dim szName As String



Sub ini_can(baud As String, Mess As Boolean)
' Inizializzazione della rete CAN
Dim b As Integer
'Public Const CAN_BAUD_1M = &H14        '   1 MBit / s
'Public Const CAN_BAUD_500K = &H1C      ' 500 kBit / s
'Public Const CAN_BAUD_250K = &H11C     ' 250 kBit / s
'Public Const CAN_BAUD_125K = &H31C     ' 125 kBit / s
'Public Const CAN_BAUD_100K = &H432F    ' 100 kBit / s
'Public Const CAN_BAUD_50K = &H472F     '  50 kBit / s
'Public Const CAN_BAUD_20K = &H532F     '  20 kBit / s
'Public Const CAN_BAUD_10K = &H672F     '  10 kBit / s
'Public Const CAN_BAUD_5K = &H7F7F      '   5 kBit / s

Select Case val(Trim$(baud))
  Case 1000
    b = CAN_BAUD_1M
  Case 500
    b = CAN_BAUD_500K
  Case 250
    b = CAN_BAUD_250K
  Case 125
    b = CAN_BAUD_125K
  Case 100
    b = CAN_BAUD_100K
  Case 50
    b = CAN_BAUD_50K
  Case 20
    b = CAN_BAUD_20K
  Case 10
    b = CAN_BAUD_10K
  Case 5
    b = CAN_BAUD_5K
End Select

'registrazione hardware
CAN_result = CAN_RegisterHardware(HAR_CAN_DONGLE, 2, &H378, 7)
If CAN_result <> CAN_ERR_OK Then
  If CAN_result = CAN_ERR_REGTEST Then
    'Error during register test of the CAN-controller
    CAN_result = CAN_GetErrText(CAN_result, szbuff)
    log_str "Modulo Principale: sub ini_can. CAN_RegisterHardware: Errore durante il test di registrazione del controller CAN " + Rie_nul(szbuff, ""), MED_PRIORITY, log_can
    If Mess Then messaggio "Errore durante il test di registrazione del controller CAN", True, False
    'APalmerini_FI16/08/2004_1 (1)
    'Exit Sub
  ElseIf CAN_result = CAN_ERR_NOVXD Then
    CAN_result = CAN_GetErrText(CAN_result, szbuff)
    log_str "Modulo Principale: sub ini_can. VxD (virtual device driver) not found " + Rie_nul(szbuff, ""), MED_PRIORITY, log_can
    If Mess Then messaggio "VxD (virtual device driver) not found " + Rie_nul(szbuff, ""), True, False
  Else
    CAN_result = CAN_GetErrText(CAN_result, szbuff)
    log_str "Modulo Principale: sub ini_can. " + Rie_nul(szbuff, ""), MED_PRIORITY, log_can
    If Mess Then messaggio msg("pro_dis_can") + Rie_nul(szbuff, ""), True, False
  End If
'APalmerini_FI16/08/2004_1 (1)
End If
'Else
'registrazione rete
CAN_result = CAN_RegisterNet(1, "Lenze", 1, b)
If CAN_result <> CAN_ERR_OK Then
  CAN_result = CAN_GetErrText(CAN_result, szbuff)
  log_str "Modulo Principale: sub ini_can. CAN_RegisterNet: " + Rie_nul(szbuff, ""), MED_PRIORITY, log_can
End If
' first register the client...
CAN_result = CAN_RegisterClient(app.EXEName, hwnd, hClient)
If CAN_result <> CAN_ERR_OK Then
  CAN_result = CAN_GetErrText(CAN_result, szbuff)
  log_str "Modulo Principale: sub ini_can. CAN_RegisterClient: " + Rie_nul(szbuff, ""), MED_PRIORITY, log_can
End If
' ...and connect him to a net
CAN_result = CAN_ConnectToNet(hClient, "Lenze", hNet)
If CAN_result <> CAN_ERR_OK Then
  CAN_result = CAN_GetErrText(CAN_result, szbuff)
  log_str "Modulo Principale: sub ini_can. CAN_ConnectToNet: " + Rie_nul(szbuff, ""), MED_PRIORITY, log_can
End If
'CAN_result = CAN_GetNetName(hNet, szName)
'If CAN_result <> CAN_ERR_OK Then
'  CAN_result = CAN_GetErrText(CAN_result, szbuff)
'  log_str "Modulo Principale: main.  CAN_GetNetName: " + Rie_nul(szbuff, ""), MED_PRIORITY, log_can
'End If
CAN_result = CAN_HardwareOfNet(hNet, 1)
If CAN_result <> CAN_ERR_OK Then
  CAN_result = CAN_GetErrText(CAN_result, szbuff)
  log_str "Modulo Principale: sub ini_can. CAN_HardwareOfNet: " + Rie_nul(szbuff, ""), MED_PRIORITY, log_can
End If
  
CAN_result = CAN_SetBaud(1, b)
If CAN_result <> CAN_ERR_OK Then
  CAN_result = CAN_GetErrText(CAN_result, szbuff)
  log_str "Modulo Principale: sub ini_can. CAN_SetBaud: " + Rie_nul(szbuff, ""), MED_PRIORITY, log_can
End If
'For i = 0 To 255
'    CAN_result = CAN_SetBaud(i, 14)
'    If CAN_result <> CAN_ERR_ILLHW Or i = 255 Then Exit For
'Next i
    ' use CAN-ID 0x100
'      msg_can.ID = &H100
msg_can.RTR = 0
msg_can.LEN = 8
'send = False
'APalmerini_FI16/08/2004_1 (1)
'End If

End Sub

Sub ini_liv()

Select Case val(par_mac.car_tec.num_liv_tag)
  Case 1
    LIV_ALT = 1
    LIV_BAS = 0
    LIV_MED = 0
  Case 2
    LIV_BAS = 1
    LIV_ALT = 2
    LIV_MED = 0
  Case 3
    LIV_BAS = 1
    LIV_MED = 2
    LIV_ALT = 3
End Select
End Sub

Sub ini_num_con()
Dim f As Form

For Each f In Forms
  num_con = num_con + f.Controls.Count
Next f
End Sub



Sub ini_vel_max()
Dim tmp_ord As Tip_Ordine
Dim m As Integer
Dim liv_tmp As Integer
Dim liv_ord As Integer
Dim lung As Long
Dim vel_cur_tag As Integer
Dim i As Integer

On Local Error GoTo ErrorHandler

If ord_mac.Count >= 1 Then
  CopyType tmp_ord, ord_mac.item(1).ordine, Len(ord_mac.item(1).ordine)
  For i = 1 To 3
    If Trim$(tmp_ord.ord(i).Ord_liv) <> "" Then
      liv_tmp = val(tmp_ord.ord(i).Ord_liv)
      liv_ord = cal_liv(liv_tmp)
      lung = val(tmp_ord.ord(i).sca_lun) \ TODECMM
      vel_cur_tag = cal_vel_cur_tag(liv_tmp, lung)
      vel_max_tag_liv(liv_ord) = Min_num(val(par_mac.par_cfg_att(liv_tmp).vel_max_tag), vel_cur_tag)
    End If
  Next i
End If
Select Case val(par_mac.car_tec.num_liv_tag)
  Case 1
    If vel_max_tag_liv(3) = 0 Then vel_max_tag_liv(3) = val(par_mac.par_cfg_att(1).vel_max_tag)
  Case 2
    If vel_max_tag_liv(1) = 0 Then vel_max_tag_liv(1) = val(par_mac.par_cfg_att(1).vel_max_tag)
    If vel_max_tag_liv(3) = 0 Then vel_max_tag_liv(3) = val(par_mac.par_cfg_att(2).vel_max_tag)
  Case 3
    For i = 1 To 3
      If vel_max_tag_liv(i) = 0 Then
        vel_max_tag_liv(i) = val(par_mac.par_cfg_att(i).vel_max_tag)
      End If
    Next i
End Select
Exit Sub

ErrorHandler:
  log_str "Ini_vel_max: Err.N.=" & Format(err.Number) & " Description=" & err.Description, MED_PRIORITY, log_gen
  err.Clear
  
End Sub




Sub Leg_ord()
  Frm_ini.lbl_Wrn.Caption = "Loading orders files...": Frm_ini.Refresh: DoEvents
  cnv_ord       ' converte se necessario i file ordini al nuovo formato     ' ABorelli_FI09/09/2005_1
  Set ord_mac = New CollezioneOrdini
  If ord_mac.Init(False) = RET_BAD Then
    log_str "Leg_ord: Problema sull'inizializzazione della classe Ord_mac", MED_PRIORITY, log_ord
  End If
  ord_mac.Leg_dat_ord
  Set ord_mac_ese = New CollezioneOrdini
  If ord_mac_ese.Init(True) = RET_BAD Then
    log_str "Leg_ord: Problema sull'inizializzazione della classe Ord_mac", MED_PRIORITY, log_ord
  End If
  ord_mac_ese.Leg_dat_ord
End Sub

Sub load_form()

Load Onde: inc_prg_bar_ini 'Arturo uff 25-07-02
Load set_par_rul: inc_prg_bar_ini 'Arturo uff 25-07-02
Load Ins_ord: inc_prg_bar_ini 'Arturo uff 25-07-02
Load cop_ord: inc_prg_bar_ini 'Arturo uff 25-07-02
Load Del_ord: inc_prg_bar_ini 'Arturo uff 25-07-02
Load del_ord_ese: inc_prg_bar_ini 'Arturo uff 25-07-02
Load mov_ese: inc_prg_bar_ini 'Arturo uff 25-07-02
Load Mov_ord: inc_prg_bar_ini 'Arturo uff 25-07-02
Load pag_lis_ord: inc_prg_bar_ini 'Arturo uff 25-07-02
Load pag_pro: inc_prg_bar_ini 'Arturo uff 25-07-02
Load pag_lis_ese: inc_prg_bar_ini 'Arturo uff 25-07-02
Load pag_pro_day:  inc_prg_bar_ini ' ABorelli_FI09/09/2005_1
Load set_par_mac: inc_prg_bar_ini 'Arturo uff 25-07-02
Load set_par_com: inc_prg_bar_ini 'Arturo uff 25-07-02
Load set_par_sch_tag1: inc_prg_bar_ini 'Arturo uff 25-07-02
Load set_par_sch_tag2: inc_prg_bar_ini 'Arturo uff 25-07-02
Load set_par_sch_tag3: inc_prg_bar_ini 'Arturo uff 25-07-02
Load set_par_sch_tag4: inc_prg_bar_ini 'Arturo uff 25-07-02
Load abilitazioni: inc_prg_bar_ini 'Arturo uff 25-07-02
Load imp_all: inc_prg_bar_ini 'Arturo uff 25-07-02
Load all_mac: inc_prg_bar_ini 'Arturo uff 25-07-02
Load all_war: inc_prg_bar_ini 'Arturo uff 25-07-02
Load all_qe1: inc_prg_bar_ini 'Arturo uff 25-07-02
Load all_qe3: inc_prg_bar_ini 'Arturo uff 25-07-02
Load all_qe2: inc_prg_bar_ini 'Arturo uff 25-07-02
Load all_har: inc_prg_bar_ini 'Arturo uff 25-07-02
Load all_con: inc_prg_bar_ini 'Arturo uff 25-07-02
Load pag_all: inc_prg_bar_ini 'Arturo uff 25-07-02
Load sto_all: inc_prg_bar_ini 'Arturo uff 25-07-02
Load fre_war: inc_prg_bar_ini 'Arturo uff 25-07-02
Load fre_qe1: inc_prg_bar_ini 'Arturo uff 25-07-02
Load fre_qe2: inc_prg_bar_ini 'Arturo uff 25-07-02
Load fre_qe3: inc_prg_bar_ini 'Arturo uff 25-07-02
Load fre_har: inc_prg_bar_ini 'Arturo uff 25-07-02
Load fre_con: inc_prg_bar_ini 'Arturo uff 25-07-02
Load fre_mac: inc_prg_bar_ini 'Arturo uff 25-07-02
Load dia_ret_azi: inc_prg_bar_ini 'Arturo uff 25-07-02
Load dia_sta_azi: inc_prg_bar_ini 'Arturo uff 25-07-02
Load dia_par_new: inc_prg_bar_ini 'Arturo uff 25-07-02
Load sal_cfg: inc_prg_bar_ini 'Arturo uff 25-07-02
Load leg_cfg: inc_prg_bar_ini 'Arturo uff 25-07-02
Load del_cfg: inc_prg_bar_ini 'Arturo uff 25-07-02
Load pag_msg: inc_prg_bar_ini 'Arturo uff 25-07-02
Load frm_debug: inc_prg_bar_ini 'Arturo uff 25-07-02
Load frm_debug_des: inc_prg_bar_ini 'Arturo uff 25-07-02
Load Sel_lin: inc_prg_bar_ini 'Arturo uff 25-07-02
Load Pwd_frm: inc_prg_bar_ini 'Arturo uff 25-07-02
Load Frm_data: inc_prg_bar_ini 'Arturo uff 25-07-02
Load dia_sch_tag: inc_prg_bar_ini 'Arturo uff 25-07-02
Load dia_par_des: inc_prg_bar_ini 'Arturo uff 25-07-02
Load dia_par_new_des: inc_prg_bar_ini 'Arturo uff 25-07-02
Load tro_ord: inc_prg_bar_ini 'Arturo uff 25-07-02
Load tro_ord_ese: inc_prg_bar_ini 'Arturo uff 25-07-02
'Icilio3545 Caricate le nuove form
Load adj_pat_con: inc_prg_bar_ini
Load Pattern: inc_prg_bar_ini
'Load stato_IO
'andrea per rom
Load cam_tur
Load con_azz
End Sub

Sub Main()
Dim i As Integer
On Local Error GoTo ges_err
Dim VER As String

'ABorelli_FI17/11/2006_1
#If CJ Then
' definire CJ=1 in progetto,proprieta' di stag,crea,argomenti di compilazione condizionale
' cancellare il controllo SYSMAC_PLC dalla form sta_bar.frm da errore altrimenti
  par_mac.com_plc_lan.abi_lan = 1
#Else
' definire CJ=0 in progetto,proprieta' di stag,crea,argomenti di compilazione condizionale
' rimettere il controllo SYSMAC_PLC nella form sta_bar.frm da errore altrimenti
  par_mac.com_plc_lan.abi_lan = 0
#End If
pro_set.PercX = Int(((Screen.Width / Screen.TwipsPerPixelX) / 800) * 100)
pro_set.PercY = Int(((Screen.Height / Screen.TwipsPerPixelY) / 600) * 100)


'Arturo uff 13-11-02
'setFileINIString "Info", "Version", Trim(Frm_ini.Vrs_Nmb.Caption), app.EXEName
'IDisperati_FI 21/08/2006
pro_set.JOB = getFileINIString("pro_set", "Job", "xxxx")
pro_set.Commessa = getFileINIString("pro_set", "Commessa", "YYccc")
Frm_ini.JOB.Caption = pro_set.JOB
Frm_ini.Commessa.Caption = pro_set.Commessa


'Arturo uff 26-11-02 (2)
'Set ist_com_plc = New com_plc     'Iciliouff 29-10-2003 Inserimento classe Rmt
'Iciliouff 03-11-2003
Set rmt_ope_plc = New rmt
Set rmt_ope_sch = New rmt
Set rmt_ope_macro = New rmt
'Impostazione UTE delle varie rmt 1=Plc, 2=Schede Taglio, 3=Macro Plc/Schede Taglio
rmt_ope_plc.set_plc_ute (1)
rmt_ope_plc.name = "rmt_ope_plc"
rmt_ope_sch.set_plc_ute (2)
rmt_ope_sch.name = "rmt_ope_sch"
rmt_ope_macro.set_plc_ute (3)
rmt_ope_macro.name = "rmt_ope_macro"

'Iciliouff 20-11-2003
With dummy_order_sch_tag
  .L1 = int_esa(30)
  .L2 = int_esa(35)
  .L3 = int_esa(40)
  .L4 = int_esa(0)
  .L5 = int_esa(0)
  .L6 = int_esa(0)
  .L7 = int_esa(0)
  .L8 = int_esa(0)
  .L9 = int_esa(0)
  .L10 = int_esa(0)
  .bit_cut_reg = int_esa(6)
  .Ofs_Cut = int_esa(0)
  .ID_ord = "FFFF"
  .libero = int_esa(0)
  .fin_pri = int_esa(0)
  .fin_dop = int_esa(0)
End With

Set sta_tag = New ClasseStato
Set plc_ind = New ind_plc
Set par_nuo = New par_new

num_con = 0
Frm_ini.show
Livello = 1
'inizializzazione parametri di configurazione supervisore
Frm_ini.lbl_Wrn.Caption = "Reading program settings...": Frm_ini.Refresh: DoEvents
leg_par_sup                             'leggo i parametri in pro_set

'agrassi_FI09-12-2009_1
Dim ver_pre As String
ver_pre = getFileINIString("Info", "Version", "0.0.0")
If Ctr_ver_pre(ver_pre, "4.0.0") Then
  If Dir$(pro_set.pro_pat + "PATTERN.DBF") <> "" Then
    If Dir$(pro_set.pro_pat + "PATTERNori.DBF") <> "" Then Kill pro_set.pro_pat + "PATTERNori.DBF"
    CopyFile pro_set.pro_pat + "PATTERN.DBF", pro_set.pro_pat + "PATTERNori.DBF", 1
    Kill pro_set.pro_pat + "PATTERN.DBF"
  End If
End If

setFileINIString "Info", "Version", Trim(Frm_ini.Vrs_Nmb.Caption), app.EXEName

'Arturo uff 12-11-02
VER = "Main: avvio stag ver" + getFileINIString("Info", "Version", "0.0.0")
log_str VER, MED_PRIORITY, log_spde
log_str VER, MED_PRIORITY, log_plc
log_str VER, MED_PRIORITY, log_can
log_str VER, MED_PRIORITY, log_sch_tag

'apertura database
Set wk = Workspaces(0)
Set db = wk.OpenDatabase(pro_set.pro_pat, False, False, "DBase III;")

chk_fil

'inizializzazione parametri di configurazione macchina
Frm_ini.lbl_Wrn.Caption = "Reading cut off settings...": Frm_ini.Refresh: DoEvents
Frm_ini.prg_bar_ini.Visible = True
par_mac.car_tec.num_liv_tag = val(getFileINIString("car_tec", "num_liv_tag", "2"))
If (val(par_mac.car_tec.num_liv_tag) > 3 Or val(par_mac.car_tec.num_liv_tag) < 0) Then par_mac.car_tec.num_liv_tag = "2"
Frm_ini.prg_bar_ini.Max = 34 + 92 * val(par_mac.car_tec.num_liv_tag)
leg_par_mac                            'leggo i parametri macchina

Frm_ini.lbl_Wrn.Caption = "Reading configuration settings...": Frm_ini.Refresh: DoEvents
Frm_ini.prg_bar_ini.Visible = False
leggi_cfg
leggi_par_cur_tag
' ABorelli_FI09/09/2005_1
leggi_pro       ' inizializza i dati di produzione leggendoli da dat_prd.mac

ini_liv

'inizializzo le struttura di appoggio
par_mac_ute = par_mac
par_abi_ute = par_abi

''''''''''''''''''''''''''''''''''''''''''''''''''
'Arturo uff 26-11-02 (2)
'Set ist_com_plc = New com_plc
'Set sta_tag = New ClasseStato
'Set plc_ind = New ind_plc
'Set par_nuo = New par_new

NUM_AZI = par_mac.car_tec.num_liv_tag * par_mac.car_tec.num_mot_liv_tag
NUM_ALL = NUM_DAT_MEM_ALL * 16
NUM_GRU_ALL = NUM_DAT_MEM_ALL / 3
num_liv_lis_ord = IIf(val(par_mac.car_tec.num_liv_tag) = 3, 3, val(par_mac.car_tec.num_liv_tag) + 1)
'settaggio per l'avvio automatico
AutoAdminLogon
If Dir(pro_set.pro_pat + "AutoAdminLogon.exe") <> "" Then
   Call Shell(pro_set.pro_pat + "AutoAdminLogon.exe", vbMinimizedNoFocus)
End If

Frm_ini.prg_bar_ini.Visible = True
Frm_ini.prg_bar_ini.Value = 0
Frm_ini.prg_bar_ini.Max = MAX_NUM_ORD_ESE + MAX_NUM_ORD
Leg_ord
ini_vel_max
Frm_ini.prg_bar_ini.Value = 0

Frm_ini.lbl_Wrn.Caption = "Creating message's and code's collection...": Frm_ini.Refresh: DoEvents
Frm_ini.prg_bar_ini.Visible = True
Frm_ini.prg_bar_ini.Max = 6600
set_cod_azi                             'aggiungo alla collezione anche le descrizioni del parametro 168 per la diagnostica sugli azionamenti
set_msg                                   'aggiungo i messaggi

Frm_ini.prg_bar_ini.Value = 0
Frm_ini.prg_bar_ini.Max = 54
Frm_ini.prg_bar_ini.Visible = True
Frm_ini.lbl_Wrn.Caption = "Loading forms...": Frm_ini.Refresh: DoEvents
  'Carica le form senza visualizzarle
Load Tasti: inc_prg_bar_ini 'Arturo uff 25-07-02
Load sta_bar: inc_prg_bar_ini 'Arturo uff 25-07-02
'visualizzo i tasti e la barra di stato
vis_for Tasti, True
vis_for sta_bar, True

load_form

'Inizializzazione delle porte seriali e della rete ethernet
ini_porte_SPDE
ini_porte_SCHEDE_TAGLIO

' Inizializzazione della rete CAN
ini_can Trim$(par_mac.com_azi.baud), False
'ABorelli_FI17/11/2006_1
If val(par_mac.com_plc_lan.abi_lan) <> 0 Then
  sta_bar.Fins.RemoteHost = par_mac.com_plc_lan.ind_rem
  sta_bar.Fins.RemotePort = par_mac.com_plc_lan.por_rem
  sta_bar.Fins.TimeOut = par_mac.com_plc_lan.TimeOut
  sta_bar.Fins.Enabled = True
End If

'Iciliouff 03-11-2003 Spostato in tim_tag_plc
' Inizializzazione dell'indirizzo PLC
'ini_plc

Frm_ini.prg_bar_ini.Value = 0

'Iciliouff 03-11-2003 Spostato in tim_tag_plc
'Invio dei parametri macchina al PLC
'invio_ini_par_mac

ini_num_con 'conta il numero di controlli

Frm_ini.prg_bar_ini.Visible = False

If Not skip_db_lang Then set_lin Command$   'imposto il programma sulla lingua corrente di ini

'cancello schermata iniziale
Frm_ini.Hide

'visualizzo la pagina di produzione
pag_pro.show

If Prb_lis_ord Then
    messaggio msg("att_con_lis_ord"), True
    Prb_lis_ord = False
End If

Set pag_cor = pag_pro
pag_cor.Tag = pag_pro.name

'visualizzo la descrizione dei tasti
Show_tas_des pag_pro.name, Shift:=0

pag_pro.SetFocus

'faccio partire i timer
sta_bar.Tim_com_PLC.Enabled = True
sta_bar.Tim_all.Enabled = True
sta_bar.tim_com_spde.Enabled = True

Exit Sub
ges_err:

  log_str "on error Principale.bas Main." + " err.Description = '" + err.Description + "'; err.Number = '" + Format$(err.Number) + "'", MED_PRIORITY, log_gen
  If err.Number = 0 Then
    Resume Next
  ElseIf err.Number = 53 Then
    Resume Next
  ElseIf err.Number = 57 Then  'valore della proprietà non valido (resume next messo nel caso che le fins gateway non siano installate). Debug
    Resume Next
  ElseIf err.Number = 380 Then
    Resume Next
  ElseIf err.Number = 3018 Then
    log_str "Main: Errore fatale nr. '" + Format(err.Number) + "': " + err.Description + " Confrontare le colonne con i valori descritti in stampatello ed eventualmente sostituire i db par_cfg.dbf e/o cur_tag.dbf con l'ultima versione.", MAX_PRIORITY, log_gen
  Else
    log_str "on error Principale.bas Main. Errore '" + Format(err.Number) + "' non gestito", MED_PRIORITY, log_gen
  End If
End Sub

Sub leggi_pro()
' ABorelli_FI09/09/2005_1
Dim dat_prd As tip_pro_tur_gio_fosber21_FOS
Dim dat_prd_ese As tot_pro_ese
Dim s As String

'IDisperati_FI 21/08/2006
On Local Error GoTo ErrorHandler

' dati turno giorno
    sta_tag.tempi res_ord:=False, res_tur:=False, res_gio:=True     ' inizializzo i tempi di turno e giorno
    s = sta_tag.get_pro_tg
    CopyType dat_prd, s, Len(dat_prd)
    If Dir$(pro_set.pro_pat + NAM_FIL_PRD) = "" Then
        ' crea un file nuovo
        Open pro_set.pro_pat + NAM_FIL_PRD For Random As #NUM_FIL_PRD Len = Len(dat_prd)
        Put #NUM_FIL_PRD, 1, dat_prd
        Close #NUM_FIL_PRD
    ElseIf FileLen(pro_set.pro_pat + NAM_FIL_PRD) <> Len(dat_prd) Then
        Kill pro_set.pro_pat + NAM_FIL_PRD
        Open pro_set.pro_pat + NAM_FIL_PRD For Random As #NUM_FIL_PRD Len = Len(dat_prd)
        Put #NUM_FIL_PRD, 1, dat_prd
        Close #NUM_FIL_PRD
    Else    ' tutto ok
        Open pro_set.pro_pat + NAM_FIL_PRD For Random As #NUM_FIL_PRD Len = Len(dat_prd)
        Get #NUM_FIL_PRD, 1, dat_prd
        Close #NUM_FIL_PRD
    End If
    If val(dat_prd.gio.tem_fer) > 600000000 Or val(dat_prd.gio.met_lin) > 600000000 Then         ' per overflow
        s = Space$(Len(dat_prd))
    Else
        CopyString s, dat_prd, Len(s)
    End If
    sta_tag.set_pro_tg s
' dati produzione ordine
    s = Space$(Len(dat_prd_ese))
    If Dir$(pro_set.pro_pat + NAM_FIL_PRD_ESE) = "" Then
        ' crea un file nuovo
        CopyType dat_prd_ese, s, Len(dat_prd_ese)
        Open pro_set.pro_pat + NAM_FIL_PRD_ESE For Random As #NUM_FIL_PRD_ESE Len = Len(dat_prd_ese)
        'IDisperati_FI 21/08/2006 Il numero di file era errato
        Put #NUM_FIL_PRD_ESE, 1, dat_prd_ese
        Close #NUM_FIL_PRD_ESE
'        Put #NUM_FIL_PRD, 1, dat_prd_ese
'        Close #NUM_FIL_PRD
    ElseIf FileLen(pro_set.pro_pat + NAM_FIL_PRD_ESE) <> Len(dat_prd_ese) Then
        CopyType dat_prd_ese, s, Len(dat_prd_ese)
        Kill pro_set.pro_pat + NAM_FIL_PRD_ESE
        Open pro_set.pro_pat + NAM_FIL_PRD_ESE For Random As #NUM_FIL_PRD_ESE Len = Len(dat_prd_ese)
        Put #NUM_FIL_PRD_ESE, 1, dat_prd_ese
        Close #NUM_FIL_PRD_ESE
    Else    ' tutto ok
        Open pro_set.pro_pat + NAM_FIL_PRD_ESE For Random As #NUM_FIL_PRD_ESE Len = Len(dat_prd_ese)
        Get #NUM_FIL_PRD_ESE, 1, dat_prd_ese
        Close #NUM_FIL_PRD_ESE
    End If
    If val(dat_prd_ese.tem_fer) > 600000000 Then          ' per overflow
        s = Space$(Len(dat_prd_ese))
    Else
        CopyString s, dat_prd_ese, Len(s)
    End If
    sta_tag.set_pro_ese s
    Exit Sub
    
ErrorHandler:
  log_str "Cnv_ord: Err.N.=" & Format(err.Number) & " Description=" & err.Description, MED_PRIORITY, log_gen
  Close #NUM_FIL_PRD_ESE
  Close #NUM_FIL_PRD
  err.Clear
        
End Sub
Sub scrivi_pro()
' ABorelli_FI09/09/2005_1
Dim dat_prd As tip_pro_tur_gio_fosber21_FOS
Dim dat_prd_ese As tot_pro_ese
Dim s As String
        s = sta_tag.get_pro_tg
        Open pro_set.pro_pat + NAM_FIL_PRD For Output As #NUM_FIL_PRD
        Print #NUM_FIL_PRD, s;
        Close #NUM_FIL_PRD
        
        s = sta_tag.get_pro_ese
        Open pro_set.pro_pat + NAM_FIL_PRD_ESE For Output As #NUM_FIL_PRD_ESE
        Print #NUM_FIL_PRD_ESE, s;
        Close #NUM_FIL_PRD_ESE

End Sub
