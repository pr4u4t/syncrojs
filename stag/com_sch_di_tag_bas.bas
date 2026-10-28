Attribute VB_Name = "com_sch_di_tag_bas"
Option Explicit

Dim val_sing_par As String


Public Function CHK_SUM(ByVal str As String) As String
'calcolo del Check sum per il protocollo seriale con la scheda di taglio
Dim i As Integer
Dim som As Long
Dim ris As String
'agrassi_FI28-05-08
If Len(str) < COM_SCH_TAG_LEN_STX Then
  CHK_SUM = "  "
  Exit Function
End If
som = 0
str = Right$(str, Len(str) - COM_SCH_TAG_LEN_STX)
For i = 1 To Len(str)
    som = som + Asc(Mid$(str, i, 1))
Next i
ris = Hex$(som)
CHK_SUM = Right$(FT2 + ris, 2)

End Function

'MLAMIONI_PMASSARO_FI27/10/2010 - Gestione comunicazione UDP-TCP per schede di taglio
Function connect_done(liv As Integer) As Boolean
Dim remote_host As String
Dim remote_port As Integer

On Local Error GoTo gErr

If liv >= 1 And liv <= 3 Then
  remote_host = Trim$(sta_bar.sck_sch_tag(liv - 1).RemoteHost)
  remote_port = Int(sta_bar.sck_sch_tag(liv - 1).RemotePort)
  With sta_bar.sck_sch_tag(liv - 1)
    If (.Protocol = sckTCPProtocol) Then
      If .State = sckConnected Then
        connect_done = True
      Else
        .Close
        .Connect remote_host, remote_port
        
        Dim ok_connect As Boolean
        Dim first_Time As Long
        Dim isElapsedTime As Boolean
        
        ok_connect = False
        first_Time = GetTickCount()
        isElapsedTime = False
        
        Dim idxP As Integer
        For idxP = 1 To 3
          While (((Not ok_connect) And (Not isElapsedTime)))
            ok_connect = (.State = sckConnected)
            isElapsedTime = ((GetTickCount() - first_Time) > 4000)
            DoEvents
          Wend
          If ok_connect = False Then
            If .State = sckConnected Then
              connect_done = True
            Else
              .Close
              .Connect remote_host, remote_port
              isElapsedTime = False
              first_Time = GetTickCount
            End If
          Else
            Exit For
          End If
        Next idxP
        If ok_connect = True Then
          Debug.Print "livello " + Format(liv) + " stato connesso"
          log_str "livello " + Format(liv) + " stato connesso", MED_PRIORITY, log_gen
        End If
        connect_done = ok_connect
      End If
    Else
      connect_done = True
    End If
  End With
Else
  connect_done = False
End If

Exit Function

gErr:
  log_str "on error intervenuta in Connect_Done: " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  connect_done = (sta_bar.sck_sch_tag(liv - 1).State = sckConnected)
End Function

Public Sub rea_dat_sch_tag(liv As Integer, rx As String)
''Provvede alla gestione della stringa ricevuta
'Dim fcs As String
'
'
'fcs = Right$(rx, COM_SCH_TAG_LEN_BCC)
'rx = Left$(rx, Len(rx) - COM_SCH_TAG_LEN_BCC) 'tolgo il check sum dalla stringa
'If fcs = CHK_SUM(rx) Then
'    rx = Right$(rx, Len(rx) - 1) 'tolgo STX
'    Select Case CASO_RCV_SCH_TAG
'      Case PARAM_INFORM
'        rea_rx_inf liv, rx
'        set_par_sch_tag4.fil_par_inf (liv)
'      Case DIAGNOST
'        rea_rx_dia liv, rx
'    End Select
'    messaggio msg("ope_eff"), False, False
'Else  ' check sum errato
'    messaggio msg("err_com_sch_tag") & " " & liv, True, False, True, liv
'End If

End Sub



Public Sub rea_rx_dia(liv As Integer, str As String)
Dim i As Integer
Dim wrd As String
Dim arr() As String
Dim c As Integer

'caso lettura parametri informativi della scheda di taglio
If Len(str) <> 8 Then
  log_str "sub rea_rx_dia del modulo com_sch_di_tag_bas: la stringa ricevuta contiene più WORD di quelle attese (2)", MED_PRIORITY, log_sch_tag
End If
ReDim arr(1 To (Len(str) / 4))
c = 1
For i = 1 To Len(str) - 3 Step 4
  wrd = Mid$(str, i, 4)
  arr(c) = CStr(hex_bin(wrd, False))
  c = c + 1
Next i
dia_sch_tag.col_lab_msg_all arr(1), arr(2)
End Sub



Public Sub rea_rx_inf(liv As Integer, str As String)
Dim i As Integer
Dim wrd As String
Dim arr() As String
Dim c As Integer

'caso lettura parametri informativi della scheda di taglio
ReDim arr(1 To (Len(str) / 4))
c = 1
For i = 1 To Len(str) - 3 Step 4
  wrd = Mid$(str, i, 4)
  arr(c) = CStr(Hex_Dec(wrd))
  c = c + 1
Next i
par_mac.sch_tag.par_inf(liv).ver_pro_sch_tag = arr(1)
par_mac.sch_tag.par_inf(liv).ver_sch_tag = arr(2)
par_mac.sch_tag.par_inf(liv).ver_PIC = arr(3)
par_mac.sch_tag.par_inf(liv).ver_rad_qua_con_arr = arr(4)
par_mac.sch_tag.par_inf(liv).qua_bit_rad_qua_con_arr = arr(5)
par_mac.sch_tag.par_inf(liv).per_arr_rad_qua_con_arr = arr(6)
par_mac.sch_tag.par_inf(liv).ver_rad_qua_sen_arr = arr(7)
par_mac.sch_tag.par_inf(liv).qua_bit_rad_qua_sen_arr = arr(8)
par_mac_ute.sch_tag.par_inf(liv) = par_mac.sch_tag.par_inf(liv)
End Sub

Public Sub rea_sta_sch_tag(ByVal liv As Integer, ByVal mes As String)
'Iciliouff 20-11-2003
'Assegna lo stato ordine alla scheda di taglio
Static old_mes(1 To 3) As String

  If Len(mes) = Len(sta_sch_tag.stato(liv)) Then
    sta_sch_tag.stato(liv).ID_ord(SUCCESSIVO) = Left$(mes, 4)
    sta_sch_tag.stato(liv).ID_ord(CORRENTE) = Right$(mes, 4)
    If mes <> old_mes(liv) Then log_str "rea_sta_tag: Nuovo stato su livello" & str(liv) & " ID Corrente = " & sta_sch_tag.stato(liv).ID_ord(CORRENTE) & " ID Successivo = " & sta_sch_tag.stato(liv).ID_ord(SUCCESSIVO) & " Vecchio stato = " & old_mes(liv), MED_PRIORITY, log_sch_tag
    old_mes(liv) = mes
  End If
  
End Sub

Function rx_sch_tag(ByVal r As String, ByVal liv As Integer)
'Gestione comunicazioni con scheda di taglio
'Analizza comando ricevuto da host
Const MAX_LEN_RX As Integer = 1100      'Numero max di byte ricevibili da WProto
'Comandi da host
Dim ric_ok As Boolean
Dim fcs As String
Dim first_char As String * 1
Dim com As String
Dim pun As Integer, tmp_char As String * 1, pos_eot As Integer, stx_ok As Boolean, pos_stx_suc As Integer
Static rx As String
Dim tmp As String


  'TODO Inserire il controllo della risposta coerente alla domanda (un ACK non significa niente se voglio scrivere un vettore)
  If par_mac.com_sch_tag(liv).com_att Then
    rx = rx + r
    Debug.Print "RECV*" & str(Time) & " " & GetTickCount() & " STRINGA RX - " & rx
    r = ""
    pun = 1
    If Len(rx) > 0 Then
      Do Until pun > Len(rx)
        tmp_char = Mid(rx, pun, 1)
        If tmp_char = Chr$(ACK_ST) Then
          'Risposta Positiva da scheda di taglio
          Debug.Print "RECV " & str(Time) & " " & GetTickCount() & " RICEVUTO ACK! - " & rx
          par_mac.com_sch_tag(liv).ricevuto = True
          par_mac.com_sch_tag(liv).com_att = False
          ges_fine_trasmissione liv, True
'          If CASO_SND_SCH_TAG <> INVIO_INIZIALE_PAR Then
'            messaggio msg("tra_eff_sch_tag") & " " & liv, False, False, True, liv
'          Else
'            messaggio msg("inv_par_sch_tag"), False, False
'          End If
          log_str "snd_str_sch_tag: ricevuto ACK_ST", MED_PRIORITY, log_sch_tag
          rx = Mid(rx, pun + 1)
        ElseIf tmp_char = Chr$(NAK_ST) Then
          'Risposta Negativa da scheda di taglio (ricevuto=false in modo da ritentare la comunicazione da snd_str_sch_tag)
          Debug.Print "RECV " & str(Time) & " " & GetTickCount() & " RICEVUTO NACK! - " & rx
          par_mac.com_sch_tag(liv).ricevuto = False
          par_mac.com_sch_tag(liv).com_att = False
          'par_mac.com_sch_tag(liv).com_att = False
          ges_fine_trasmissione liv, False
'          If CASO_SND_SCH_TAG <> INVIO_INIZIALE_PAR Then
'            messaggio msg("ope_ann") & ". " & msg("err_com_sch_tag") & " " & liv, True, False, True, liv
'          End If
          log_str "snd_str_sch_tag: ricevuto NAK_ST", MED_PRIORITY, log_sch_tag
          rx = Mid(rx, pun + 1)
        ElseIf tmp_char = Chr$(STX_ST) Then
          'Ritorno dati da scheda di taglio
          pos_eot = InStr(pun, rx, Chr$(EOT_ST))
          If pos_eot > pun Then
            pos_stx_suc = InStr(pun + 1, rx, Chr$(STX_ST))
            If (pos_stx_suc > pos_eot Or pos_stx_suc = 0) And Len(rx) > COM_SCH_TAG_LEN_BCC Then
              'Messaggio valido fra pun e pos_eot
              tmp = rx
              tmp = Mid(tmp, pun, pos_eot - pun)
              Debug.Print "RECV " & str(Time) & " " & GetTickCount() & " STRINGA RX - " & rx
              'Controllo del check sum ricevuto
              'agrassi_FI28-05-08
              If Len(tmp) > COM_SCH_TAG_LEN_BCC Then
                fcs = Right$(tmp, COM_SCH_TAG_LEN_BCC)
                tmp = Left$(tmp, Len(tmp) - COM_SCH_TAG_LEN_BCC)       'tolgo il check sum dalla stringa
                If fcs = CHK_SUM(tmp) Then
                  'Interpretazione comandi ricevuti
                  com = Mid$(tmp, 2, 1)
                  'check sum corretto
                  tmp = Mid$(tmp, 2)                                  'tolgo STX
                  par_mac.com_sch_tag(liv).ricevuto = True
                  par_mac.com_sch_tag(liv).com_att = False
                  Select Case CASO_RCV_SCH_TAG
                    Case PARAM_INFORM
                      rea_rx_inf liv, tmp
                      set_par_sch_tag4.fil_par_inf (liv)
                    Case DIAGNOST
                      rea_rx_dia liv, tmp
                    'Iciliouff 20-11-2003
                    Case STATO_ORDINE
                      rea_sta_sch_tag liv, tmp
                  End Select
                  CASO_RCV_SCH_TAG = tip_ric_sch_tag.NIET
                  'Arturo uff 12-11-02 (4)
                  log_str "snd_str_sch_tag: rx = '" + rx + "'", MED_PRIORITY, log_sch_tag
                Else
                  'check sum errato
                  Debug.Print "RECV " & str(Time) & " " & GetTickCount() & " CHECK SUM ERRATO! - " & rx
                  par_mac.com_sch_tag(liv).com_att = False
                  log_str "snd_str_sch_tag: rx = '" + rx + "' check sum errato", MED_PRIORITY, log_sch_tag
                  'Scorro il prossimo messaggio (se esiste)
                  rx = Mid(rx, pun + 1)
                End If
              Else
                  'string troppo corta
                  Debug.Print "RECV " & str(Time) & " " & GetTickCount() & " string troppo corta! - " & rx
                  par_mac.com_sch_tag(liv).com_att = False
                  log_str "snd_str_sch_tag: rx = '" + rx + "' string troppo corta", MED_PRIORITY, log_sch_tag
                  'Scorro il prossimo messaggio (se esiste)
                  rx = Mid(rx, pun + 1)
              End If

              'Passo al prossimo messaggio
              rx = Mid(rx, IIf(Len(rx) >= pos_eot, pos_eot + 1, Len(rx)))
              Debug.Print "RECV " & str(Time) & " " & GetTickCount() & " PASSO AL PROSSIMO MESSAGGIO! - " & rx
            Else
              'Frammento di messaggio, scorro il puntatore fino all'inizio del prossimo carattere
              rx = Mid(rx, pun + 1)
            End If
          Else
            'Stringa non completa con EOT, esco dal ciclo ed attendo i prossimi messaggi
            Debug.Print "RECV " & str(Time) & " " & GetTickCount() & " STRINGA NON COMPLETA CON EOT! - " & rx
            Exit Do
          End If
        Else
          'Passiamo al prossimo carattere ricevuto
          rx = Mid(rx, pun + 1)
        End If
      Loop
    End If
  End If

End Function

Public Function snd_sch_tag(liv As Integer, cmd As tip_cmd, Optional ByVal indice As Integer = 1, Optional ByVal CONTRASTO As Integer) As cod_ret
'Iciliouff 03-11-2003 Trasformata la snd_sch_tag in funzione che ritorna RET_OK, RET_BAD
'In funzione ad esito trasmissione con scheda di taglio
'indice = 1   Invio ordine corrente
'indice = 2   Invio ordine successivo
Dim str As String
Dim par As tip_par_cfg
Dim i As Integer
Dim ord_sch_tag As tip_dat_ord_sch_tag
Const BIT_FST_FTC = 1     'Bit 0  - Colore inizio pattern 1=nero 0=bianco
Const BIT_ABI_PAT = 2     'Bit 1  - Abilita pattern ("decodifica fotocellula")
Const BIT_BKG_A = 4       'Bit 2  - Seleziona background A
Const BIT_BKG_B = 8       'Bit 3  - Seleziona background B
    
  sta_sch_tag.Cmd_att = cmd
  Select Case cmd
    Case CONFIGURA
      str = String(Len(par), "0")
      CopyType par, str, Len(str)
      par.arr_par(IND_VEL_SIM_LAP) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).vel_sim_lap))
      par.arr_par(IND_VEL_LAM_JOG) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).vel_lam_jog))
      par.arr_par(IND_DIS_FOT_LAM) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).dis_fot_lam))
      par.arr_par(IND_LUN_TAG_TES) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).lun_tag_tes))
      par.arr_par(IND_LUN_TAG_MIN) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).lun_tag_min))
      par.arr_par(IND_LUN_TAG_MAX) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).lun_tag_max))
      par.arr_par(IND_LIM_INF_TOL_TAG) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).lim_inf_tol_tag))
      par.arr_par(IND_LIM_SUP_TOL_TAG) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).lim_sup_tol_tag))
      par.arr_par(IND_GIR_MOT_LAM_RIF) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).gir_mot_lam_rif))
      par.arr_par(IND_VEL_MAX_TAG) = int_esa(val(par_mac_ute.par_cfg_att(liv).vel_max_tag))
      par.arr_par(IND_ANG_SINCR) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).ang_sincr))
      par.arr_par(IND_SOV_LAM) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).sov_lam))
      par.arr_par(IND_VEL_ZON_SINC_VMIN) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).vel_zon_sinc_vmin))
      par.arr_par(IND_DIA_RUO_MIS) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).dia_ruo_mis))
      par.arr_par(IND_DIA_PULL_ROLL) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).dia_pull_roll))
      par.arr_par(IND_CIR_LAM) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).cir_lam))
      par.arr_par(IND_RAP_RID_MOT_LAM) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).rap_rid_mot_lam))
      par.arr_par(IND_IMP_GIR_ENC_RUO_MIS) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).imp_gir_enc_ruo_mis))
      par.arr_par(IND_IMP_GIR_ENC_PULL_ROLL) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).imp_gir_enc_pull_roll))
      par.arr_par(IND_IMP_GIR_ENC_LAM) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).imp_gir_enc_lam))
      par.arr_par(IND_FAT_MOL_ENC_RUO_MIS) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).fat_mol_enc_ruo_mis))
      par.arr_par(IND_FAT_MOL_ENC_PULL_ROLL) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).fat_mol_enc_pull_roll))
      par.arr_par(IND_FAT_MOL_ENC_LAM) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).fat_mol_enc_lam))
      par.arr_par(IND_TEM_ACC_TGL_LUN) = int_esa(val(par_mac_ute.par_cfg_att(liv).tem_acc_tgl_lun))
      par.arr_par(IND_TEM_DEC_TGL_LUN) = int_esa(val(par_mac_ute.par_cfg_att(liv).tem_dec_tgl_lun))
      par.arr_par(IND_TEM_ACC_TGL_COR) = int_esa(val(par_mac_ute.par_cfg_att(liv).tem_acc_tgl_cor))
      par.arr_par(IND_TEM_DEC_TGL_COR) = int_esa(val(par_mac_ute.par_cfg_att(liv).tem_dec_tgl_cor))
      par.arr_par(IND_OFF_RAM_POS) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).off_ram_pos))
      par.arr_par(IND_OFF_RAM_TAG) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).off_ram_tag))
      par.arr_par(IND_FIN_VEL_OTT_PEN) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).fin_vel_ott_pen))
      par.arr_par(IND_FAT_K01_OTT_PEN) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).fat_k01_ott_pen))
      'Arturo uff 21-02-03
      'par.arr_par(IND_GIR_MAX_MOT_LAM) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).gir_max_mot_lam))
      par.arr_par(IND_GIR_MAX_MOT_LAM) = int_esa(val(par_mac_ute.par_cfg_att(liv).gir_max_mot_lam))
      par.arr_par(IND_KP_NOR) = int_esa(val(par_mac_ute.par_cfg_att(liv).Kp_nor))
      par.arr_par(IND_KI_NOR) = int_esa(val(par_mac_ute.par_cfg_att(liv).Ki_nor))
      par.arr_par(IND_TEM_CAM_STA_VEL) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).tem_cam_sta_vel))
      par.arr_par(IND_SOG_ERR_INS) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_err_ins))
      par.arr_par(IND_FIL_KP) = int_esa(val(par_mac_ute.par_cfg_att(liv).fil_kp))
      par.arr_par(IND_KV_NOR) = int_esa(val(par_mac_ute.par_cfg_att(liv).Kv_nor))
      par.arr_par(IND_LIM_LAV_KV) = int_esa(val(par_mac_ute.par_cfg_att(liv).lim_lav_Kv))
      'Icilio3545 08-01-2004
      'Robertouff 26-11-2007
      'agrassi_FI15-07-08
      'If val(par_mac_ute.opt.cut_mar) = 1 Or val(par_mac_ute.opt.cut_pat) = 1 Or val(par_mac_ute.opt.cut_pat_auto) = 1 Then
      
      'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
      'If val(par_mac.opt.cut_mar) = 1 Or val(par_mac.opt.cut_pat(1)) = 1 Or val(par_mac.opt.cut_pat(2)) = 1 Or val(par_mac.opt.cut_pat(3)) = 1 Or val(par_mac_ute.opt.cut_pat_auto) = 1 Then
      If val(par_mac.opt.cut_mar) = 1 Or val(par_mac.opt.cut_pat(1)) = 1 Or val(par_mac.opt.cut_pat(2)) = 1 Or val(par_mac.opt.cut_pat(3)) = 1 Or val(par_mac.opt.cut_pat_auto(1)) = 1 Or val(par_mac.opt.cut_pat_auto(2)) = 1 Or val(par_mac.opt.cut_pat_auto(3)) = 1 Then
        'Icilio3545 08-01-2004 Se è in lavoro un ordine con pattern, vengono inviati i suoi dati soltanto
        'IMPORTANTE: L'ipotesi è che non possono esistere due formati con pattern sullo stesso ordine
        Dim tmp_off_tag_reg, tmp_fin_pri_tac, tmp_fin_dop_tac As String
        Dim IsPattern As Boolean
        If ord_mac.Count > 0 Then
          For i = 1 To 3
            If ord_mac.item(1).cut_to_mark(i) = cut_pat Then Exit For
          Next i
          If i < 4 Then
            tmp_off_tag_reg = Format(ord_mac.item(1).Ofs_Cut(i))
            tmp_fin_pri_tac = Format(ord_mac.item(1).fin_pri(i))
            tmp_fin_dop_tac = Format(ord_mac.item(1).fin_dop(i))
            IsPattern = True
          End If
        End If
        par.arr_par(IND_OFF_TAG_REG) = IIf(IsPattern, int_esa(tmp_off_tag_reg), int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).off_tag_reg)))
        par.arr_par(IND_FIN_PRI_TAC) = IIf(IsPattern, int_esa(tmp_fin_pri_tac), int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).fin_pri_tac)))
        par.arr_par(IND_FIN_DOP_TAC) = IIf(IsPattern, int_esa(tmp_fin_dop_tac), int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).fin_dop_tac)))
        par.arr_par(IND_DIS_FTC_TAC_LAM) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).dis_FTC_tac_lam))
        par.arr_par(IND_COR_TAG_REG_1°_SOG) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).cor_tag_reg_1°_sog))
        par.arr_par(IND_SOG_VEL_COR) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_vel_cor))
        par.arr_par(IND_COR_TAG_REG_2°_SOG) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).cor_tag_reg_2°_sog))
        par.arr_par(IND_VEL_FTC_PAT) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).vel_ftc))
        par.arr_par(IND_DIS_ASS_SEN_FUO_CAR) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).dis_cen_mac_sen_fuo_car))
      Else
        par.arr_par(IND_OFF_TAG_REG) = int_esa(0)
        par.arr_par(IND_FIN_PRI_TAC) = int_esa(0)
        par.arr_par(IND_FIN_DOP_TAC) = int_esa(0)
        par.arr_par(IND_DIS_FTC_TAC_LAM) = int_esa(0)
        par.arr_par(IND_COR_TAG_REG_1°_SOG) = int_esa(0)
        par.arr_par(IND_SOG_VEL_COR) = int_esa(0)
        par.arr_par(IND_COR_TAG_REG_2°_SOG) = int_esa(0)
        par.arr_par(IND_VEL_FTC_PAT) = int_esa(0)
        par.arr_par(IND_DIS_ASS_SEN_FUO_CAR) = int_esa(0)
      End If
      str = ""
      For i = 0 To NUM_PAR_CFG
        str = str + par.arr_par(i)
      Next i
      
      'MLAMIONI_PMASSARO_FI27/10/2010 - Gestione comunicazione UDP-TCP per schede di taglio
      'snd_sch_tag = snd_str_sch_tag(liv, WPROTO_SCRIVIVETTORE, IND_INI_SCR_PAR, str)
      If connect_done(liv) Then
        snd_sch_tag = snd_str_sch_tag(liv, WPROTO_SCRIVIVETTORE, IND_INI_SCR_PAR, str)
      Else
        snd_sch_tag = False
      End If
      
      
      'Robertouff 26-11-2007
      If (val(par_mac_ute.opt.sca_liv(liv)) = 1) Then
        par.arr_par(IND_SOV_TAP_USC_PER_SCA) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).sov_tap_usc_per_sca)) ' deve essere inserito come 108 e accanto visualizzato 10.8
        'RFAZZI_FI 08-01-08
        'par.arr_par(IND_MIS_SCA_DEF) = int_esa(1200)
        'RFAZZI_FI19/03/2008. Modificato sotto usando par_mac_ute anzichè par_mac
        par.arr_par(IND_MIS_SCA_DEF) = int_esa(par_mac_ute.car_tec.lun_tag_sca)
        par.arr_par(IND_SCA_PRI_GIU) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).sca_pri_giu))
        par.arr_par(IND_SCA_DOP_GIU) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).sca_dop_giu))
        par.arr_par(IND_DIS_SEN_LAM) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).dis_sen_lam))
        par.arr_par(IND_DIS_LAM_FLP) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).dis_lam_flp))
        par.arr_par(IND_DIS_LAM_RUL_NO_CRH) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).dis_lam_rul_no_crh))
        par.arr_par(IND_TEM_APE_FLP) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).tem_ape_flp))
        par.arr_par(IND_TEM_CHI_FLP) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).tem_chi_flp))
        par.arr_par(IND_TEM_APE_RUL_NO_CRH) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).tem_ape_rul_no_crh))
        par.arr_par(IND_TEM_CHI_RUL_NO_CRH) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).tem_chi_rul_no_crh))
        par.arr_par(IND_TEM_SCA_PRE_APE_RUL_NO_CRH) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).tem_sca_pre_ape_rul_no_crh))
        par.arr_par(IND_TEM_SCA_PRE_CHI_RUL_NO_CRH) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).tem_sca_pre_chi_rul_no_crh))
        par.arr_par(IND_MAX_LUN_SCA) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).max_lun_sca))
        par.arr_par(IND_MIN_LUN_SCA) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).min_lun_sca))
        par.arr_par(IND_VEL_1) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_vel_1))
        par.arr_par(IND_LUN_1) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_lun_1))
        par.arr_par(IND_VEL_2) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_vel_2))
        par.arr_par(IND_LUN_2) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_lun_2))
        par.arr_par(IND_VEL_3) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_vel_3))
        par.arr_par(IND_LUN_3) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_lun_3))
        'RFAZZI_FI27/03/2008
        par.arr_par(IND_LUN_SUP_3) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).sog_lun_sup_3))
        par.arr_par(IND_TEM_ATT_CAM_PRE_RUL_NO_CRH) = int_esa(val(par_mac_ute.sch_tag.sch_tag_liv(liv).tem_att_cam_pre_rul_no_crh))
      Else
        par.arr_par(IND_SOV_TAP_USC_PER_SCA) = int_esa(0)
        par.arr_par(IND_MIS_SCA_DEF) = int_esa(0)
        par.arr_par(IND_SCA_PRI_GIU) = int_esa(0)
        par.arr_par(IND_SCA_DOP_GIU) = int_esa(0)
        par.arr_par(IND_DIS_SEN_LAM) = int_esa(0)
        par.arr_par(IND_DIS_LAM_FLP) = int_esa(0)
        par.arr_par(IND_DIS_LAM_RUL_NO_CRH) = int_esa(0)
        par.arr_par(IND_TEM_APE_FLP) = int_esa(0)
        par.arr_par(IND_TEM_CHI_FLP) = int_esa(0)
        par.arr_par(IND_TEM_APE_RUL_NO_CRH) = int_esa(0)
        par.arr_par(IND_TEM_CHI_RUL_NO_CRH) = int_esa(0)
        par.arr_par(IND_TEM_SCA_PRE_APE_RUL_NO_CRH) = int_esa(0)
        par.arr_par(IND_TEM_SCA_PRE_CHI_RUL_NO_CRH) = int_esa(0)
        par.arr_par(IND_MAX_LUN_SCA) = int_esa(0)
        par.arr_par(IND_MIN_LUN_SCA) = int_esa(0)
        par.arr_par(IND_VEL_1) = int_esa(0)
        par.arr_par(IND_LUN_1) = int_esa(0)
        par.arr_par(IND_VEL_2) = int_esa(0)
        par.arr_par(IND_LUN_2) = int_esa(0)
        par.arr_par(IND_VEL_3) = int_esa(0)
        par.arr_par(IND_LUN_3) = int_esa(0)
        'RFAZZI_FI27/03/2008
        par.arr_par(IND_LUN_SUP_3) = int_esa(0)
        par.arr_par(IND_TEM_ATT_CAM_PRE_RUL_NO_CRH) = int_esa(0)
      End If
      
      str = ""
      'agrassi_FI12-07-2011_1
      'For i = NUM_PAR_CFG + 1 To NUM_PAR_CFG + NUM_PAR_DIV_AUT
      For i = NUM_PAR_CFG + 1 To NUM_PAR_CFG + NUM_PAR_DIV_AUT + 1
        str = str + par.arr_par(i)
      Next i
      snd_sch_tag = snd_str_sch_tag(liv, WPROTO_SCRIVIVETTORE, IND_INI_SCR_PAR_DIV, str)
      
    Case LETTURA_STATO
      CASO_RCV_SCH_TAG = DIAGNOST
      snd_sch_tag = snd_str_sch_tag(liv, WPROTO_LEGGIVETTORE, IND_INI_LET_PAR_STA, , NUM_WRD_STA)
    Case INFORMAZIONI
      CASO_RCV_SCH_TAG = PARAM_INFORM
      snd_sch_tag = snd_str_sch_tag(liv, WPROTO_LEGGIVETTORE, IND_INI_LET_PAR_INF, , NUM_PAR_INF)
    'Iciliouff 20-11-2003
    Case LETTURA_STATO_ORDINE
      'Lettura dati ordine per controllo allineamento
      CASO_RCV_SCH_TAG = STATO_ORDINE
      snd_sch_tag = snd_str_sch_tag(liv, WPROTO_LEGGIVETTORE, IND_INI_LET_ID_ORD, , NUM_WRD_ID_ORD)
    Case INVIO_DATI_ORDINE
      'Tutti i dati relativi all'ordine in corso vengono trasferiti ad ogni singola scheda
      snd_sch_tag = RET_BAD
      If indice > 0 And indice <= ord_mac.Count Then
        For i = 1 To 3
          If ord_mac.item(indice).Ord_liv(i) = liv Then Exit For
        Next i
        If i <= 3 Then
          If ord_mac.item(indice).cut_to_mark(i) = 2 Then
            'Il livello richiesto esiste in ordine per indice i
            Dim cfg_bit As Long
            With ord_sch_tag
              'Valorizzazione ordine
              'Quote tacche
              .L1 = int_esa(ord_mac.item(indice).L1(i))
              .L2 = int_esa(ord_mac.item(indice).L2(i))
              .L3 = int_esa(ord_mac.item(indice).L3(i))
              .L4 = int_esa(ord_mac.item(indice).L4(i))
              .L5 = int_esa(ord_mac.item(indice).L5(i))
              .L6 = int_esa(ord_mac.item(indice).L6(i))
              .L7 = int_esa(ord_mac.item(indice).L7(i))
              .L8 = int_esa(ord_mac.item(indice).L8(i))
              .L9 = int_esa(ord_mac.item(indice).L9(i))
              .L10 = int_esa(ord_mac.item(indice).L10(i))
              'Bit utilizzati dal cut to registry (pattern o mark)
              cfg_bit = IIf(ord_mac.item(indice).fst_FTC(i) = 1, BIT_FST_FTC, 0)
              cfg_bit = cfg_bit + IIf(ord_mac.item(indice).cut_to_mark(i) = 2, BIT_ABI_PAT, 0)
              If ord_mac.item(indice).pat_con(i) > 0 Then
                'Il valore del contrasto viene codificato per la fotocellula (Area1='00',Area2='01',Area3='10',Area4='11')
                cfg_bit = cfg_bit + IIf(((ord_mac.item(indice).pat_con(i) - 1) And 1) = 1, BIT_BKG_A, 0)
                cfg_bit = cfg_bit + IIf(((ord_mac.item(indice).pat_con(i) - 1) And 2) = 2, BIT_BKG_B, 0)
              Else
                'Se il contrasto non è specificato, invio un default (prima area di contrasto fotocellula)
                cfg_bit = cfg_bit + BIT_BKG_A
              End If
              .bit_cut_reg = int_esa(cfg_bit)
              'Offset per centraggio taglio su pattern
              .Ofs_Cut = int_esa(ord_mac.item(indice).Ofs_Cut(i))
              'Id ordine inviato (stag_id)
              .ID_ord = ord_mac.item(indice).stag_id
              'Finestra prima e dopo la tacca
              .fin_pri = int_esa(ord_mac.item(indice).fin_pri(i))
              .fin_dop = int_esa(ord_mac.item(indice).fin_dop(i))
            End With
          Else
            'Dati cut to pattern assenti - Invio ordine dummy
            ord_sch_tag = dummy_order_sch_tag
            ord_sch_tag.ID_ord = ord_mac.item(indice).stag_id
          End If
        Else
          'Il livello liv non esiste in ordine - Invio ordine dummy
          log_str "snd_sch_tag - Invio ordine dummy perchè il parametro 'liv' = " & Format$(liv) & " non appartiene all'ordine con codice = " & ord_mac.item(indice).cod_ord_abb, MED_PRIORITY, log_sch_tag
          ord_sch_tag = dummy_order_sch_tag
          ord_sch_tag.ID_ord = ord_mac.item(indice).stag_id
        End If
        str = String(Len(ord_sch_tag), "0")
        CopyString str, ord_sch_tag, Len(ord_sch_tag)
        snd_sch_tag = snd_str_sch_tag(liv, WPROTO_SCRIVIVETTORE, IND_INI_SCR_DAT_ORD, str)
      Else
        log_str "snd_sch_tag - il parametro 'indice' non è valido = " & Format$(indice), MED_PRIORITY, log_sch_tag
      End If
    Case SELEZIONE_CONTRASTO
      'Si indirizza l'area di memoria della fotocellula per la procedura di auto apprendimento
      If CONTRASTO > 0 And CONTRASTO <= 4 Then
        ord_sch_tag = dummy_order_sch_tag
        With ord_sch_tag
          'Bit utilizzati dal cut to registry (pattern o mark)
          'Il valore del contrasto viene codificato per la fotocellula (Area1='00',Area2='01',Area3='10',Area4='11')
          cfg_bit = cfg_bit + IIf(((CONTRASTO - 1) And 1) = 1, BIT_BKG_A, 0)
          cfg_bit = cfg_bit + IIf(((CONTRASTO - 1) And 2) = 2, BIT_BKG_B, 0)
          .bit_cut_reg = int_esa(cfg_bit)
          'Id ordine dummy in modo da riallineare automaticamente l'ordine alla fine della procedura di auto apprendimento contrasto
          .ID_ord = "FFFF"
        End With
        str = String(Len(ord_sch_tag), "0")
        CopyString str, ord_sch_tag, Len(ord_sch_tag)
        snd_sch_tag = snd_str_sch_tag(liv, WPROTO_SCRIVIVETTORE, IND_INI_SCR_DAT_ORD, str)
      Else
        log_str "snd_sch_tag - Procedura di auto apprendimento contrasto da fotocellula: il valore di contrasto indicato non è corretto = " & Format$(CONTRASTO), MED_PRIORITY, log_sch_tag
      End If
    Case Else
      snd_sch_tag = RET_BAD
      log_str "snd_sch_tag - Comando sconosciuto con codice = " & Format$(cmd), MED_PRIORITY, log_sch_tag
  End Select

End Function





Function snd_str_sch_tag(liv As Integer, cmd As Integer, ind_wrd As Integer, Optional val_wrd As String = "", Optional num_wrd As Integer = 0) As cod_ret
'Iciliouff 03-11-2003 Gestione invio stringa alla scheda di taglio
Dim stringa As String
Dim lun As Integer
Dim i As Integer
Dim cnt_err As Integer
Dim tmp As Long

'On Local Error GoTo ges_err
On Local Error Resume Next
  
  snd_str_sch_tag = RET_BAD
  'Invio di comando Reset Slave (non ha bisogno di risposta)
  'agrassi_FI08-03-2010_1
  If par_mac.tip_com_sch_tag(liv) = 0 Then
    sta_bar.com_sch_tag(liv - 1).Output = Chr$(WPROTO_SYN_ST)
  ElseIf par_mac.tip_com_sch_tag(liv) = 1 Then
    sta_bar.sck_sch_tag(liv - 1).SendData Chr(WPROTO_SYN_ST)
  End If
  'Valorizzazione della richiesta verso scheda di taglio
  stringa = Chr$(STX_ST) + Format(cmd)
  Select Case cmd
    Case WPROTO_LEGGIMEMORIA
      ' lettura di una Word
      stringa = stringa + int_esa(ind_wrd)
    Case WPROTO_SCRIVIMEMORIA
      ' scrittura di una Word
      If val_wrd = "" Then Exit Function
      stringa = stringa + int_esa(ind_wrd) + Format$(val_wrd, FT4)
    Case WPROTO_LEGGIVETTORE
      ' lettura  di un vettore di Word
      If num_wrd <= 0 Then Exit Function
      stringa = stringa + int_esa(num_wrd) + int_esa(ind_wrd)
    Case WPROTO_SCRIVIVETTORE
      ' scrittura di un vettore di Word
      If val_wrd = "" Or Len(val_wrd) Mod 4 <> 0 Then Exit Function
      stringa = stringa + int_esa(ind_wrd) + val_wrd
    Case Else
      Exit Function
  End Select
  stringa = stringa + CHK_SUM(stringa) + Chr$(EOT_ST)
  
  'Sincronizzazioni comunicazioni
  cnt_err = 0
  Dim ult_tmp As Long
  Do While True
    ult_tmp = GetTickCount()
    Debug.Print ">>> Invio per livello =" & str(liv) & " Comando =" & str(cmd)
    Debug.Print "SEND " & str(Time) & " " & ult_tmp & " CNT_ERR - " & Format(cnt_err) & " STRINGA - " & stringa
'    'Seriale
'    sta_bar.com_sch_tag(liv - 1).OutBufferCount = 0
'    sta_bar.com_sch_tag(liv - 1).InBufferCount = 0
'    sta_bar.com_sch_tag(liv - 1).Output = stringa
    'agrassi_FI08-03-2010_1
    If par_mac.tip_com_sch_tag(liv) = 0 Then
      'Seriale
      sta_bar.com_sch_tag(liv - 1).OutBufferCount = 0
      sta_bar.com_sch_tag(liv - 1).InBufferCount = 0
      sta_bar.com_sch_tag(liv - 1).Output = stringa
    ElseIf par_mac.tip_com_sch_tag(liv) = 1 Then
      'Rete
      sta_bar.sck_sch_tag(liv - 1).SendData stringa
    End If
    par_mac.com_sch_tag(liv).com_att = True             'Comando attivo
    par_mac.com_sch_tag(liv).ricevuto = False           'Nessuna risposta da sch_tag
    log_str stringa, MED_PRIORITY, cos_log.log_sch_tag
    tmp = GetTickCount()
    Do While True
      'Controllo Time-Out
      If (GetTickCount() - tmp > val(par_mac.com_sch_tag(liv).TimeOut)) And par_mac.com_sch_tag(liv).com_att Then
        'Incremento il contatore dei tentativi
        Debug.Print "SEND " & str(Time) & " " & GetTickCount() & " CNT_ERR - " & Format(cnt_err) & " STRINGA - " & stringa
        cnt_err = cnt_err + 1
        Exit Do
      End If
      'Controllo risposta negativa
      If Not par_mac.com_sch_tag(liv).ricevuto And Not par_mac.com_sch_tag(liv).com_att Then
        Debug.Print "SEND " & str(Time) & " " & GetTickCount() & " CNT_ERR - " & Format(cnt_err) & " NACK! - " & stringa
        cnt_err = cnt_err + 1
        Exit Do
      End If
      'Controllo ricezione stringa valida
      If par_mac.com_sch_tag(liv).ricevuto Then
        Debug.Print "SEND " & str(Time) & " " & GetTickCount() & " CNT_ERR - " & Format(cnt_err) & " STRINGA RICEVUTA!"
        cnt_err = 0
        Exit Do
      End If
      DoEvents
      Sleep (1)
    Loop
    'Controllo l'esito del tentativo corrente (esco al terzo tentativo)
    If cnt_err = 0 Then
      snd_str_sch_tag = RET_OK: Exit Function
    ElseIf cnt_err > 2 Then
      Debug.Print "SEND " & str(Time) & " " & GetTickCount() & " CNT_ERR - " & Format(cnt_err) & " TIMEOUT!"
      par_mac.com_sch_tag(liv).com_att = False
      Exit Function
    End If
  Loop
  
End Function

