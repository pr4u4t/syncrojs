Attribute VB_Name = "Omron_PLC"
Option Explicit

Const NUM_DAT_MEM_PLC = 50
Dim DM(0 To NUM_DAT_MEM_PLC) As String * 4
Dim NUM_DAT_PAR_CUR_TAG As Integer


Function cal_liv(liv As Integer) As Integer
If liv > 0 Then
  Select Case liv
    Case LIV_ALT
      cal_liv = 3
    Case LIV_MED
      cal_liv = 2
    Case LIV_BAS
      cal_liv = 1
    Case Else
      cal_liv = 0
  End Select
Else
  cal_liv = 0
End If
End Function

Function cal_vel_cur_tag(liv As Integer, l As Long) As Integer
Dim m As Integer
Dim vel_cur_tag As Integer

For m = 1 To 14
  If cur_tag(liv).Lun_tag(m) <= l And cur_tag(liv).Lun_tag(m + 1) >= l Then
    vel_cur_tag = Int((((l - cur_tag(liv).Lun_tag(m)) * (cur_tag(liv).vel_max(m + 1) - cur_tag(liv).vel_max(m))) + cur_tag(liv).vel_max(m) * (cur_tag(liv).Lun_tag(m + 1) - cur_tag(liv).Lun_tag(m))) / (cur_tag(liv).Lun_tag(m + 1) - cur_tag(liv).Lun_tag(m)))
    Exit For
  End If
Next m

If m = 14 Then
  If cur_tag(liv).Lun_tag(m + 1) < l Then vel_cur_tag = cur_tag(liv).vel_max(m + 1)
End If
cal_vel_cur_tag = vel_cur_tag

End Function

Function liv_in_uso(liv As Integer, indice As Integer) As Boolean
Dim i As Integer

liv_in_uso = False
For i = 1 To 3
  If ord_mac(indice).item.Ord_liv(i) = liv Then
    liv_in_uso = True
    Exit Function
  End If
Next i
End Function
Sub rie_dat_ord(indice As Integer)
'Iciliouff 20-11-2003
'Valorizza i dati per ordine corrente o successivo (vedi 'indice')
'Aree Dati da DM3503 a DM3524
Dim i As Integer
Dim DM_TMP As Integer
Dim str As String
Dim liv_tmp As Integer
Dim liv_ord As Integer
Dim tmp_ord As Tip_Ordine
Const p = 4
Const LIV_1 = 1
Const LIV_2 = 2
Const LIV_3 = 4
Const Ctr = 8                   'Iciliouff 20-11-2003 Cut to register (mark/pattern) Bit3 DM3503
Dim ctr_pre As Integer          'Iciliouff 20-11-2003 Memorizza la presenza di Cut to register (mark/pattern)
Dim lung As Long
Dim vel_cur_tag As Integer
Dim pre_all As Integer
  
  CopyType tmp_ord, ord_mac.item(indice).ordine, Len(ord_mac.item(indice).ordine)
  ctr_pre = 0
  For i = 1 To 3
    If Trim$(tmp_ord.ord(i).Ord_liv) <> "" Then
      liv_tmp = val(tmp_ord.ord(i).Ord_liv)
      If DM(p + (liv_tmp - 1) * 4) = "0000" Then
        'DM3503 CONFIGURAZIONE ORDINE
        'BIT 0,1,2 Cfg.Livelli in uso
        liv_ord = cal_liv(val(tmp_ord.ord(i).Ord_liv))
        
        'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
        If ordineVuoto(tmp_ord) Then
          DM_TMP = 0 'settaggio HR per indicare 'ordine nullo'
        Else
          DM_TMP = DM_TMP + 2 ^ (liv_tmp - 1) 'per il DM 3503: livello in uso
        End If
        
        
        'RFAZZI_FI09/12/2009_2. Aggiunta gestione per differente configurazione bit verso il PLC. Il discriminante è la presenza del CutToPatternAutomatico

        'Gestione protocollo PLC
        If par_mac.opt.cut_pat_auto(liv_tmp) = "1" Then
          'Nuovo protocollo PLC ( macchina con cut to pattern automatico )
          
          'BIT 3 Abi.Cut to register
          If (par_mac.opt.cut_mar = "1" And val(tmp_ord.ord(i).cut_to_mark) = cut_mar) Or _
              (par_mac.opt.cut_pat(liv_tmp) = "1" And val(tmp_ord.ord(i).cut_to_mark) = cut_pat) Or _
              (par_mac.opt.cut_pat_auto(liv_tmp) = "1" And val(tmp_ord.ord(i).cut_to_mark) = cut_pat) Then
              ctr_pre = (2 ^ (2 + liv_tmp))
              DM_TMP = DM_TMP + ctr_pre
          End If
          
        Else
          'Vecchio protocollo PLC ( macchina senza cut to pattern automatico )
        
          'agrassi_FI09-02-2010_1
          'BIT 3        Cut to register presente nell'ordine
          'BIT 4, 5, 6  Cut to register livello 1, 2, 3
          'If par_mac.opt.cut_mar = "1" And val(tmp_ord.ord(i).cut_to_mark) = cut_mar Then ctr_pre = True
          'agrassi_FI15-07-08
          'If par_mac.opt.cut_pat = "1" And val(tmp_ord.ord(i).cut_to_mark) = cut_pat Then ctr_pre = True
          'agrassi_FI17-12-08
          'If par_mac.opt.cut_pat(liv_tmp) = "1" And val(tmp_ord.ord(i).cut_to_mark) = cut_pat Then ctr_pre = True
          'If par_mac.opt.cut_pat(liv_tmp) = "1" And val(tmp_ord.ord(i).cut_to_mark) = cut_pat Then
          If (par_mac.opt.cut_pat(liv_tmp) = "1" And val(tmp_ord.ord(i).cut_to_mark) = cut_pat) Or _
             (par_mac.opt.cut_mar = "1" And val(tmp_ord.ord(i).cut_to_mark) = cut_mar) Then
            ctr_pre = True
            DM_TMP = DM_TMP Or 2 ^ (liv_tmp + 3)
          End If
          If ctr_pre Then DM_TMP = DM_TMP Or Ctr
        End If
                    
        'DM3504,3508,3512 LUNGHEZZA DI TAGLIO PER LIVELLO
        DM(p + (liv_tmp - 1) * 4) = int_esa(CStr(val(tmp_ord.ord(i).sca_lun) \ TODECMM))
        'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
        If ordineVuoto(tmp_ord) Then
          DM(p + (liv_tmp - 1) * 4) = int_esa(300) 'lunghezza di taglio per non creare problemi alla Master
          str = lng_esa(Format$(0))
        Else
          str = lng_esa(Format$(val(tmp_ord.ord(i).sca_tot_num) \ val(tmp_ord.ord(i).Sca_per_tag)))
        End If
        'DM3505,3509,3513 N°TAGLI PER LIVELLO 2DM
        DM(p + (liv_tmp - 1) * 4 + 1) = Right$(str, 4)
        DM(p + (liv_tmp - 1) * 4 + 2) = Left$(str, 4)
        'DM3507,3511,3515 VELOCITA' MASSIMA FORMATO PER LIVELLO
        'Confronto la velocità calcolata in base alla curva di taglio con quella permessa dalla scheda di
        'taglio per l'attuale configurazione sul livello liv_tmp
        lung = val(tmp_ord.ord(i).sca_lun) \ TODECMM
        
        'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
        If ordineVuoto(tmp_ord) Then
          Dim tmpl As Integer
          vel_cur_tag = 0
          For tmpl = 1 To val(par_mac.car_tec.num_liv_tag)
            vel_cur_tag = Max_num(val(par_mac.par_cfg_att(tmpl).vel_max_tag), vel_cur_tag)
          Next tmpl
        Else
          vel_cur_tag = cal_vel_cur_tag(liv_tmp, lung)
          If indice = 1 Then vel_max_tag_liv(liv_ord) = Min_num(val(par_mac.par_cfg_att(liv_tmp).vel_max_tag), vel_cur_tag)
        End If
        DM(p + (liv_tmp - 1) * 4 + 3) = int_esa(vel_cur_tag)
        'DM3516,3517,3518 PREALLARME PER LIVELLO
        If val(tmp_ord.ord(1).sca_lun) <> 0 Then
          'Icilio3545 10-01-2004 In caso di cut to pattern, si considera il preallarme impostato su STAG
          'Per permettere il posizionamento a quota della fotocellula lettura pattern
          Dim is_ord_suc_cut_pat As Boolean
          is_ord_suc_cut_pat = False
          If (ord_mac.Count >= indice + 1) And (indice > 0) Then is_ord_suc_cut_pat = (ord_mac.item(indice + 1).cut_to_mark(i) = cut_pat)
          'agrassi_FI15-07-08
          'If (is_ord_suc_cut_pat Or tmp_ord.ord(i).cut_to_mark = cut_pat) And (par_mac.opt.cut_pat = STR_TRUE) Then
          If (is_ord_suc_cut_pat Or tmp_ord.ord(i).cut_to_mark = cut_pat) And (par_mac.opt.cut_pat(liv_tmp) = STR_TRUE) Then
            pre_all = val(par_mac.car_tec.pre_all)
          Else
            'APalmerini_FI16/08/2004_1 (2)
            'pre_all = IIf((Trim$(tmp_ord.pre_all) = "" Or val(tmp_ord.pre_all) = 0), _
            '          val(par_mac.car_tec.pre_all), pre_all = val(tmp_ord.pre_all))
            pre_all = IIf((Trim$(tmp_ord.pre_all) = "" Or val(tmp_ord.pre_all) = 0), _
                      val(par_mac.car_tec.pre_all), val(tmp_ord.pre_all))
          End If
          DM(16 + (liv_tmp - 1)) = int_esa((pre_all / ((val(tmp_ord.ord(i).sca_lun) / TODECMM) / 1000)))
        Else
          DM(16) = "0000"
        End If
        log_str "rie_dat_ord: Preallarme per livello " & Format(liv_ord) & " impostato a " & Format(pre_all) & " mt.", MED_PRIORITY, log_sch_tag

        Dim quo_pre_foto_liv As Variant 'integer
        If (tmp_ord.ord(i).cut_to_mark = cut_pat) Then
          'DM3519,3521,3523 OFFSET TAGLIO PER LIVELLO
          DM(19 + (liv_tmp - 1) * 2) = int_esa(val(tmp_ord.ord(i).Ofs_Cut))
          
          'RFAZZI_FI 06-01-08
          'DM3520,3522,3524 QUOTA PREPOSIZIONAMENTO FOTOCELLULE PER LIVELLO
          'agrassi_FI15-07-08
          'If par_mac.opt.cut_pat Then
          If par_mac.opt.cut_pat(liv_tmp) Then
            quo_pre_foto_liv = (ord_mac.item(indice).Alt_ord / 2) / TODECMM - val(par_mac.sch_tag.sch_tag_liv(liv_tmp).dis_edg_ftc)
            DM(20 + (liv_tmp - 1) * 2) = int_esa(IIf(quo_pre_foto_liv >= 0, quo_pre_foto_liv, 0))
            'RFAZZI_FI 14-04-08
            DM(25 + (liv_tmp - 1)) = "0000"       'DM3525,3526,3527  RIFERIMENTO BORDO CARTONE RISPETTO AL CENTRO MACCHINA
          Else
            If (val(tmp_ord.ord(i).pos_pattern) <> 0) Then
              'RFAZZI_FI 08-01-08
              'quo_pre_foto_liv = int_esa(val(tmp_ord.ord(i).pos_pattern))
              'RFAZZI_FI 14-04-08
              'quo_pre_foto_liv = int_esa(-((ord_mac.item(indice).Alt_ord / 2) / TODECMM) + val(tmp_ord.ord(i).pos_pattern))
              quo_pre_foto_liv = int_esa(val(tmp_ord.ord(i).pos_pattern))
            Else
              'porto la FD vicino al bordo del cartone lato operatore
              'RFAZZI_FI 14-04-08
              'quo_pre_foto_liv = int_esa(-((ord_mac.item(indice).Alt_ord / 2) / TODECMM))
              quo_pre_foto_liv = "0000"
            End If
            DM(20 + (liv_tmp - 1) * 2) = quo_pre_foto_liv
            'RFAZZI_FI 14-04-08
            DM(25 + (liv_tmp - 1)) = int_esa(-(ord_mac.item(indice).Alt_ord / 2) / TODECMM)  'DM3525,3526,3527  RIFERIMENTO BORDO CARTONE RISPETTO AL CENTRO MACCHINA
          End If
        Else
          'RFAZZI_FI 08-01-08
          'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
          'If par_mac.opt.cut_pat_auto Then
          If par_mac.opt.cut_pat_auto(liv_tmp) Then
            DM(19 + (liv_tmp - 1) * 2) = "0000"                                                    'DM3519,3521,3523 OFFSET TAGLIO PER LIVELLO
            'RFAZZI_FI 14-04-08
            'DM(20 + (liv_tmp - 1) * 2) = int_esa(-(ord_mac.item(indice).Alt_ord / 2) / TODECMM)    'DM3520,3522,3524 QUOTA PREPOSIZIONAMENTO FOTOCELLULE PER LIVELLO
            DM(20 + (liv_tmp - 1) * 2) = "0000"                                                    'DM3520,3522,3524 QUOTA PREPOSIZIONAMENTO FOTOCELLULE PER LIVELLO
            DM(25 + (liv_tmp - 1)) = int_esa(-(ord_mac.item(indice).Alt_ord / 2) / TODECMM)        'DM3525,3526,3527  RIFERIMENTO BORDO CARTONE RISPETTO AL CENTRO MACCHINA
          Else
            DM(19 + (liv_tmp - 1) * 2) = "0000"   'DM3519,3521,3523 OFFSET TAGLIO PER LIVELLO
            DM(20 + (liv_tmp - 1) * 2) = "0000"   'DM3520,3522,3524 QUOTA PREPOSIZIONAMENTO FOTOCELLULE PER LIVELLO
            DM(25 + (liv_tmp - 1)) = "0000"       'DM3525,3526,3527  RIFERIMENTO BORDO CARTONE RISPETTO AL CENTRO MACCHINA
          End If
        End If
      End If
    End If
  Next i
  'If ctr_pre Then DM_TMP = DM_TMP + Ctr
  DM(3) = int_esa(DM_TMP)

End Sub

Sub rie_dat_par_cfg_mac()
Dim i As Integer
Dim m As Integer
Dim v As Long
Dim str As String
Const ABI_LIV = 2       'livelli abilitati                      bit 1,2,3 del DM3502
Const SEL_USC_SND = 16  'uscita livelli: rulletti o sandwich    bit 4 del DM 3502
Const N_MOT_LIV = 256   'n° motori installati                   bit 8,9 del DM 3502
Const TIP_AZI = 4096    ' tipo azionamenti                      bit 12 del DM3502  ' ABorelli_FI11/11/2005_1
Const nor_mac = 16384   'Normative macchina CE, UL ecc.         bit 14 del DM3502->DM0000 'Arturo uff 06-03-03
Const MIS_ANG = 32768   'abilitazione delle misure anglosassoni bit 15 del DM 3502
Const MOT_ABI = 2       'motori abilitati                       bit 1,2,3 dei DM 3503,3504,3505
Const GRU_SCA = 16      'presenza gruppo di scarto              bit 4 dei DM3503,3504,3505
Const cut_to_mark = 32  'presenza cut to mark                   bit 5 dei DM 3503,3504,3505
Const cut_to_pat = 64   'presenza cut to pattern                bit 6 dei DM 3503,3504,3505
'Robertouff 26-11-2007
Const cut_to_pat_auto = 128 'presenza cut to pattern Automat    bit 7 dei DM 3503,3504,3505
Const CON_POM = 256     'presenza controllo pompa               bit 8 del DM 3503,3504,3505

v = 0
For i = 1 To 3
  If par_abi.liv(i) Then v = v + (ABI_LIV * 2 ^ (i - 1))
Next i
For i = 1 To val(par_mac.car_tec.num_liv_tag)
  If par_mac.car_tec.sel_usc_liv(i) = "0" Then  'uscita a sandwich su un livello => faccio l'or delle uscite dei livelli per poter implementare (se servirà) la possibilità di avere diverse uscite nei livelli
    v = v + SEL_USC_SND
    Exit For
  End If
Next i

Select Case val(par_mac.car_tec.num_mot_liv_tag)
  Case 1
    str = "00"
  Case 2
    str = "01"
  Case 3
    str = "11"
End Select

If Right$(str, 1) = "1" Then
  v = v + N_MOT_LIV 'caso di 2 motori
  If Left$(str, 1) = "1" Then v = v + N_MOT_LIV * 2 'caso di 3 motori
End If

If par_mac.car_tec.nor_mac = TIP_NOR_MAC.UL Then v = v Or nor_mac 'Arturo uff 06-03-03

If abi_mis_ang Then v = v + MIS_ANG
If par_mac.car_tec.tip_azi_tag = "1" Then       ' ABorelli_FI11/11/2005_1
    v = v + TIP_AZI
End If

DM(2) = int_esa(v) 'DM 3502

For i = 1 To val(par_mac.car_tec.num_liv_tag)
  v = 0
  For m = 1 To val(par_mac.car_tec.num_mot_liv_tag)
    If par_abi.fun_mot_liv(i).fun_mot_mot(m) Then v = v + (MOT_ABI * 2 ^ (m - 1))
  Next m
  If val(par_mac.opt.sca_liv(i)) = 1 Then v = v + GRU_SCA
  If val(par_mac.opt.cut_mar) = 1 Then v = v + cut_to_mark
  'agrassi_FI15-07-08
  'If val(par_mac.opt.cut_pat) = 1 Then v = v + CUT_TO_PAT       'Iciliouff 20-11-2003
  If val(par_mac.opt.cut_pat(i)) = 1 Then v = v + cut_to_pat       'Iciliouff 20-11-2003
  'Robertouff 26-11-2007
  
  'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
  'If val(par_mac.opt.cut_pat_auto) = 1 Then v = v + cut_to_pat_auto
  If val(par_mac.opt.cut_pat_auto(i)) = 1 Then v = v + cut_to_pat_auto
  
  If val(par_mac.car_tec.pre_con_pom(i)) = 1 Then v = v + CON_POM
  DM(3 + i - 1) = int_esa(v) 'DM 3503,3504,3505
Next i

End Sub

Sub rie_dat_par_cur_tag(liv As Integer)
Dim i As Long

DM(2) = int_esa(UBound(cur_tag(liv).Lun_tag) - 1)
DM(3) = int_esa(cur_tag(liv).Lun_tag(UBound(cur_tag(liv).Lun_tag)))
DM(4) = int_esa(cur_tag(liv).vel_max(1))
For i = 1 To UBound(cur_tag(liv).Lun_tag)
  DM(2 + 2 * i + 1) = int_esa(cur_tag(liv).Lun_tag(i)) 'dm 3505-7-9-11-..........-31-33
  DM(2 + 2 * i + 2) = int_esa(cur_tag(liv).vel_max(i))  'dm 3506-8-10-12-.........-32-34
Next i
NUM_DAT_PAR_CUR_TAG = 2 + 2 * (i - 1) + 2
End Sub


Sub rie_dat_par_mac()
'riempie i DM con i parametri macchina da inviare al PLC
Dim i As Integer

DM(2) = int_esa(par_mac.rul_ing.rul_num(1).dis_rot_mas)        'distanza da Rotary Shear: prendo quello relativo al rullo 1
DM(3) = int_esa(par_mac.sch_tag.sch_tag_liv(1).dis_fot_lam)       'distanza da fotocellula Ruota di Misura: prendo quello relativo alla scheda di taglio del livello 1
'DM (4)     Riserva
For i = 1 To val(par_mac.car_tec.num_liv_tag)
    DM(5 + (i - 1) * 5) = int_esa(par_mac.rul_usc.rul_usc_liv(i).lun_san) 'lunghezza sanwich livello 1,2,3
    DM(6 + (i - 1) * 5) = int_esa(par_mac.rul_usc.rul_usc_liv(i).dis_lam_fla)    'distanza taglio - flap scarto livello 1,2,3
    DM(7 + (i - 1) * 5) = int_esa(par_mac.car_tec.lun_tag_sca)     'lunghezza taglio a default livello 1: stesso dato per tutti i livelli presenti
    DM(8 + (i - 1) * 5) = int_esa(par_mac.rul_usc.rul_usc_liv(i).tem_ape_fla)     'tempo di anticipo apertura flap 'Arturo uff 07-03-03
    'RFAZZI_FI15/06/2009. Aggiunto parametro di Lunghezza di Taglio a Default differenziata in caso di Cut to Patter Automatico e Gruppo di Scarto
    'DM (9)     Riserva
    'DM (14)      Riserva
    'DM (19)      Riserva
    DM(9 + (i - 1) * 5) = int_esa(par_mac.car_tec.lun_tag_sca_pat_aut)     'lunghezza taglio a default livello 1: stesso dato per tutti i livelli presenti
Next i
For i = val(par_mac.car_tec.num_liv_tag) + 1 To 3
    DM(5 + (i - 1) * 5) = int_esa(0)
    DM(6 + (i - 1) * 5) = int_esa(0)
    DM(7 + (i - 1) * 5) = int_esa(0)
    DM(8 + (i - 1) * 5) = int_esa(0) 'Arturo uff 07-03-03
Next i
DM(20) = int_esa(0)                         'riserva    'Arturo uff 09-09-03
DM(21) = int_esa(0)                         'riserva    'Arturo uff 09-09-03
DM(22) = int_esa(par_mac.car_tec.sgl_abi_lin_DC_Bus)    'Arturo uff 09-09-03
End Sub


Sub snd_str_PLC(tip_ope As tip_ope_PLC, Optional ByVal ordine As tip_ope_ord_PLC, Optional liv As Integer, Optional vis_mes As Boolean = True)
Dim i As Integer
Dim str_plc As Variant
Dim info As String
'DM3500
Const NEW_DAT = 1
Const INV_ORD_COR = 2               'Per l'ordine successivo ho 4 = INV_ORD_COR * indice dove indice = 2
Const DAT_ORD = 8
Const Del_ord = 16
Const MOD_ORD = 32
Const INV_PAR_MAC = 64
Const LET_PAR_SCH_TAG = 128
Const SCR_PAR_SCH_TAG = 256
Const AZZ_CONT = 512                'comando di azzeramento dei contatori: scatole e scarti
'Iciliouff 20-11-2003
Const INV_SET_DAT_SCH_TAG = 1024    'comando di set dati per schede di taglio

'DM3501
Const INV_PAR_CFG_MAC = 1           'comando di invio parametri di configurazione (sul DM 3501)
Const INV_PAR_CUR_TAG_LIV_1 = 2     'comando di invio parametri curva di taglio liv. 1 (sul DM 3501)

Dim indice As Integer
Dim NUM_DM_RIEMPITI As Integer
Dim tmp_tim As Long
Dim ok_snd  As Boolean
Dim str_ord As String
Static str_plc_old As String 'Arturo uff 12-11-02 (3)
'ABorelli_FI17/11/2006_1
Dim b() As Long

On Error GoTo ges_err_snd_str_PLC

If tip_ope >= 1 And tip_ope <= 3 And IsMissing(ordine) Then
    Select Case tip_ope
      Case INVIO_ORD
        info = "INVIO ORDINE"
      Case CANC_ORD
        info = "CANCELLAZIONE ORDINE"
      Case MODIF_ORD
        info = "MODIFICA ORDINE"
    End Select
    log_str "Modulo: Omron_PLC - Sub: snd_str_PLC. Operazione di " & info & ": parametro CORRENTE o SUCCESSIVO mancante.", MED_PRIORITY, log_plc
    If vis_mes Then messaggio msg("imp_ese_ope"), True, False
Else
  If tip_ope = PARAM_CUR_TAG And IsMissing(liv) Then
    info = "INVIO PARAMETRI CURVA DI TAGLIO"
    log_str "Modulo: Omron_PLC - Sub: snd_str_PLC. Operazione di " & info & ": parametro liv mancante.", MED_PRIORITY, log_plc
    If vis_mes Then messaggio msg("imp_ese_ope"), True, False
  Else
    sta_bar.led_plc_rx.FillColor = vbRed
    sta_bar.led_plc_tx.FillColor = vbGreen
    If tip_ope >= 1 And tip_ope <= 3 Then indice = ordine
    For i = 0 To NUM_DAT_MEM_PLC
      DM(i) = String$(4, "0")
    Next i
    
    Select Case tip_ope
      
      Case INVIO_ORD
        DM(0) = int_esa(NEW_DAT + INV_ORD_COR * indice + DAT_ORD) 'dm 3500   comando per invio ordine
        DM(2) = int_esa(ord_mac.item(indice).stag_id) 'dm 3502   id ordine
        rie_dat_ord (indice)
        'Iciliouff 08-07-2004 Portato a 25
        'NUM_DM_RIEMPITI = 25
        'RFAZZI_FI 14-04-08
        NUM_DM_RIEMPITI = 28
        If vis_mes Then
          str_ord = IIf(indice = 1, msg("msg_cor"), msg("msg_suc"))
          If indice = 2 And Left$(sta_bar.lab_msg.Caption, 10) = Left$(msg("msg_inv_ord"), 10) Then
              messaggio sta_bar.lab_msg.Caption & " - " & msg("msg_suc"), False, False
          Else
              messaggio msg("msg_inv_ord") & " " & str_ord, False, False
          End If
         End If
      Case CANC_ORD
        DM(0) = int_esa(NEW_DAT + INV_ORD_COR * indice + Del_ord)
        NUM_DM_RIEMPITI = 1
        
      Case MODIF_ORD
        DM(0) = int_esa(NEW_DAT + INV_ORD_COR * indice + MOD_ORD)
        DM(2) = int_esa(ord_mac.item(indice).stag_id) 'dm 3502   id ordine
        rie_dat_ord (indice)
        'Iciliouff 08-07-2004 Portato a 25
        'RFAZZI_FI 14-04-08
        NUM_DM_RIEMPITI = 28
        If vis_mes Then
          str_ord = IIf(indice = 1, msg("msg_cor"), msg("msg_suc"))
          messaggio msg("msg_inv_mod_ord") & " " & str_ord, False, False
        End If
      Case LETTURA_PAR_SCH_TAG
      
      Case SCRITTURA_PAR_SCH_TAG
      
      Case PARAM_MAC
        DM(0) = int_esa(NEW_DAT + INV_PAR_MAC)
        rie_dat_par_mac
        NUM_DM_RIEMPITI = 23  '20 Arturo uff 09-09-03 '17  'Arturo uff 07-03-03
        If vis_mes Then messaggio msg("msg_inv_par_mac"), False
        
      Case PARAM_CFG_MAC
        DM(0) = int_esa(NEW_DAT)
        DM(1) = int_esa(INV_PAR_CFG_MAC)
        rie_dat_par_cfg_mac
        NUM_DM_RIEMPITI = 6
        If vis_mes Then messaggio msg("msg_inv_par_mac"), False
        
      Case PARAM_CUR_TAG
        DM(0) = int_esa(NEW_DAT)
        DM(1) = int_esa(INV_PAR_CUR_TAG_LIV_1 * (2 ^ (liv - 1)))
        rie_dat_par_cur_tag liv
        NUM_DM_RIEMPITI = NUM_DAT_PAR_CUR_TAG + 1
        If vis_mes Then messaggio msg("msg_inv_par_cur_tag"), False
        
      Case AZZERAMENTO_CONTATORI
        DM(0) = int_esa(NEW_DAT + AZZ_CONT)
        NUM_DM_RIEMPITI = 1
      
      Case CONFERMA_DATI_SCHEDE_TAGLIO
        'Iciliouff 20-11-2003
        DM(0) = int_esa(NEW_DAT + INV_ORD_COR + INV_SET_DAT_SCH_TAG)
        NUM_DM_RIEMPITI = 1
        'Icilio3545 04-01-2004 Log su scheda di taglio ogni volta che invio la conferma a PLC
        log_str "snd_str_plc: Invio conferma dati inviati a scheda di taglio verso PLC", MED_PRIORITY, log_sch_tag
    End Select
    
    str_plc = ""
    ReDim b(NUM_DM_RIEMPITI - 1)  'ABorelli_FI17/11/2006_1
    For i = 0 To NUM_DM_RIEMPITI - 1
      str_plc = str_plc & DM(i)
      b(i) = val("&H" + DM(i))  'ABorelli_FI17/11/2006_1
    Next i
    
    ok_snd = True
    tmp_tim = GetTickCount
    Do While ok_snd
      If GetTickCount - tmp_tim > val(par_mac.indirizzo_plc.TIMER_SCANS_PLC) * 5 Then
          If val(par_mac.com_plc_lan.abi_lan) = 0 Then      'ABorelli_FI17/11/2006_1

          sta_bar.SYSMAC_PLC.writeArea plcAreaDM, 3500, NUM_DM_RIEMPITI, str_plc
          Else
            sta_bar.Fins.writeArea plcAreaDM, 3500, NUM_DM_RIEMPITI, b
          End If
          ok_snd = False
      End If
    DoEvents
    Loop
    'Arturo uff 12-11-02 (3)
    If str_plc <> str_plc_old Then
      log_str "snd_str_PLC: (DM3500-DM35" + Format(NUM_DM_RIEMPITI, "00") + ") = '" + str_plc + "'", MED_PRIORITY, log_plc
      str_plc_old = str_plc
    End If
  End If
End If
Exit Sub
ges_err_snd_str_PLC:
  If err.Number = 57 Then
    Resume Next
  End If
End Sub

