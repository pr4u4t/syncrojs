Attribute VB_Name = "sch_tag_bas"
Option Explicit


Public Sub conferma_cfg_all_liv(liv As Integer)
'Iciliouff 03-11-2003

  If Trim$(par_mac.config_sch_tag_att(liv).descr) <> "" Then  'resetto la precedente configurazione attiva se c'era
    res_cfg_att liv
  End If
  'scrivo nell'ini i nuovi parametri abilitazioni
  par_abi = par_abi_ute
  scr_par_abi
  par_mac.car_tec.num_mot_liv_tag = par_mac_ute.car_tec.num_mot_liv_tag
  scr_par_mac
  
'  'Arturo uff 05-11-02
'  'If carica_new_par_cur_tag(liv, val(par_mac.car_tec.for_mac), val(par_mac.car_tec.num_mot_liv_tag), par_abi.num_mot_abi(liv)) Then
'  If carica_new_par_cur_tag(liv, val(par_mac.car_tec.for_mac), val(par_mac.car_tec.gra_car), val(par_mac.car_tec.num_mot_liv_tag), par_abi.num_mot_abi(liv)) Then
'      snd_str_PLC PARAM_CUR_TAG, , liv
'  Else
'      messaggio msg("val_non_tro_cur_tag"), True, False
'  End If
'  'comunico il nuovo stao macchina
'  If sta_tag.com_OK Then snd_str_PLC PARAM_CFG_MAC
  
  'carico e setto la nuova configurazione attiva
  par_mac.config_sch_tag_att(liv).descr = par_mac_ute.config_sch_tag_att(liv).descr
  par_mac.config_sch_tag_att(liv).num_mot_liv = par_mac_ute.config_sch_tag_att(liv).num_mot_liv
  'Arturo uff 05-11-02 inserito controllo anche su grammatura max cartone
  'set_cfg_att Trim$(par_mac.config_sch_tag_att(liv).descr), val(par_mac.car_tec.for_mac), val(par_mac.car_tec.num_mot_liv_tag), par_abi.num_mot_abi(liv), liv
  set_cfg_att Trim$(par_mac.config_sch_tag_att(liv).descr), val(par_mac.car_tec.for_mac), val(par_mac.car_tec.gra_car), val(par_mac.car_tec.num_mot_liv_tag), par_abi.num_mot_abi(liv), liv
  par_mac.par_cfg_att(liv) = par_mac_ute.par_cfg_att(liv)
  'salvo nell'ini la nuova configurazione attiva
  setFileINIString "sch_tag_liv_" & liv, "config_att", par_mac.config_sch_tag_att(liv).descr, app.EXEName
  'carico la nuova configurazione di DEFAULT
  par_mac.config_sch_tag_def(liv).descr = par_mac_ute.config_sch_tag_def(liv).descr
  par_mac.par_cfg_def(liv) = par_mac_ute.par_cfg_def(liv)
  'salvo nell'ini la nuova configurazione di DEFAULT
  setFileINIString "sch_tag_liv_" & liv, "config_def", par_mac.config_sch_tag_def(liv).descr, app.EXEName
  If pag_cor.name = "set_par_sch_tag3" And liv = Livello Then
      set_par_sch_tag3.fil_par_sch_tag_liv (liv)
  End If
  
  If Not sta_bar.frm_mod.Visible And sta_bar.mes_all.Caption = msg("abi_un_mot") Then
    messag_win ""
  End If
  
End Sub

Public Sub conferma_cfg_liv(liv As Integer)

If Trim$(par_mac.config_sch_tag_att(liv).descr) <> "" Then
    res_cfg_att liv
End If
par_mac.config_sch_tag_att(liv).descr = par_mac_ute.config_sch_tag_att(liv).descr
par_mac.config_sch_tag_att(liv).num_mot_liv = par_mac_ute.config_sch_tag_att(liv).num_mot_liv
'Arturo uff 05-11-02 inserito controllo anche su grammatura max cartone
'set_cfg_att Trim$(par_mac.config_sch_tag_att(liv).descr), val(par_mac.car_tec.for_mac), val(par_mac.car_tec.num_mot_liv_tag), par_abi.num_mot_abi(liv), liv
set_cfg_att Trim$(par_mac.config_sch_tag_att(liv).descr), val(par_mac.car_tec.for_mac), val(par_mac.car_tec.gra_car), val(par_mac.car_tec.num_mot_liv_tag), par_abi.num_mot_abi(liv), liv
par_mac.par_cfg_att(liv) = par_mac_ute.par_cfg_att(liv)
setFileINIString "sch_tag_liv_" & liv, "config_att", par_mac.config_sch_tag_att(liv).descr, app.EXEName
If pag_cor.name = "set_par_sch_tag3" And liv = Livello Then
    set_par_sch_tag3.fil_par_sch_tag_liv (liv)
End If

End Sub

Public Sub conferma_ins_cfg_att_liv(liv As Integer)
salva_cfg_att par_mac_ute.config_sch_tag_att(liv).descr, liv
par_mac.config_sch_tag_att(liv).descr = par_mac_ute.config_sch_tag_att(liv).descr
par_mac.config_sch_tag_att(liv).num_mot_liv = par_mac_ute.config_sch_tag_att(liv).num_mot_liv
par_mac.par_cfg_att(liv) = par_mac_ute.par_cfg_att(liv)
If pag_cor.name = "set_par_sch_tag3" And liv = Livello Then
    set_par_sch_tag3.fil_par_sch_tag_liv (liv)
End If
End Sub

Public Sub conferma_ins_cfg_def_liv(liv As Integer)

salva_cfg_def "DEFAULT", liv
par_mac.config_sch_tag_att(liv).descr = par_mac_ute.config_sch_tag_att(liv).descr
par_mac.config_sch_tag_att(liv).num_mot_liv = par_mac_ute.config_sch_tag_att(liv).num_mot_liv
par_mac.config_sch_tag_def(liv).descr = par_mac_ute.config_sch_tag_def(liv).descr
par_mac.par_cfg_att(liv) = par_mac_ute.par_cfg_att(liv)
par_mac.par_cfg_def(liv) = par_mac_ute.par_cfg_def(liv)
If pag_cor.name = "set_par_sch_tag3" And liv = Livello Then
    set_par_sch_tag3.fil_par_sch_tag_liv (liv)
    set_par_sch_tag3.fil_par_sch_tag_def_liv (liv)
End If

End Sub

Public Sub conferma_mod_cfg_liv(liv As Integer)
Dim liv_tmp As Integer

modifica_cfg Trim$(par_mac.config_sch_tag_att(liv).descr), liv
par_mac.par_cfg_att(liv) = par_mac_ute.par_cfg_att(liv)
liv_tmp = cal_liv(liv)
If liv_tmp <> 0 Then
  vel_max_tag_liv(liv_tmp) = par_mac.par_cfg_att(liv).vel_max_tag
Else
  log_str "Sub conferma_mod_cfg_liv del Modulo sch_tag_bas. Si è verificata una situazione errata: liv_tmp = 0", MED_PRIORITY, log_sch_tag
End If
If pag_cor.name = "set_par_sch_tag3" And liv = Livello Then
    set_par_sch_tag3.fil_par_sch_tag_liv (liv)
End If
If Trim$(par_mac.config_sch_tag_att(liv).descr) = "DEFAULT" Then
    par_mac.par_cfg_def(liv) = par_mac_ute.par_cfg_def(liv)
    If pag_cor.name = "set_par_sch_tag3" And liv = Livello Then
        set_par_sch_tag3.fil_par_sch_tag_def_liv (liv)
    End If
End If
End Sub

Public Sub conferma_valori_liv(liv As Integer)
par_mac.sch_tag.sch_tag_liv(liv) = par_mac_ute.sch_tag.sch_tag_liv(liv)
scr_par_sch_tag_att_liv_ini (liv)
If num_com_sch_tag > 1 And liv = Livello Then
    Select Case pag_cor.name
      Case "set_par_sch_tag1"
        set_par_sch_tag1.fil_par_sch_tag_liv (Livello)
      Case "set_par_sch_tag2"
        set_par_sch_tag2.fil_par_sch_tag_liv (Livello)
      Case "set_par_sch_tag4"
        set_par_sch_tag4.fil_par_sch_tag_liv (Livello)
      'RFAZZI_FI19/03/2008
      Case "set_par_sch_tag5"
        set_par_sch_tag5.fil_par_sch_tag_liv (Livello)
    End Select
End If
End Sub

Sub def_in_att(tot As Boolean, Optional for_att As Form)
Dim i As Integer

  'scrittura nelle variabili
  If tot Then
    par_mac_ute.sch_tag = par_mac.sch_tag_def
  Else:
    Select Case for_att.name
      Case "set_par_sch_tag1"
        par_mac_ute.sch_tag.sch_tag_liv(Livello).dia_ruo_mis = par_mac.sch_tag_def.sch_tag_liv(Livello).dia_ruo_mis
        par_mac_ute.sch_tag.sch_tag_liv(Livello).imp_gir_enc_ruo_mis = par_mac.sch_tag_def.sch_tag_liv(Livello).imp_gir_enc_ruo_mis
        par_mac_ute.sch_tag.sch_tag_liv(Livello).vel_lam_jog = par_mac.sch_tag_def.sch_tag_liv(Livello).vel_lam_jog
        par_mac_ute.sch_tag.sch_tag_liv(Livello).dis_fot_lam = par_mac.sch_tag_def.sch_tag_liv(Livello).dis_fot_lam
        par_mac_ute.sch_tag.sch_tag_liv(Livello).dia_pull_roll = par_mac.sch_tag_def.sch_tag_liv(Livello).dia_pull_roll
        par_mac_ute.sch_tag.sch_tag_liv(Livello).lun_tag_tes = par_mac.sch_tag_def.sch_tag_liv(Livello).lun_tag_tes
        par_mac_ute.sch_tag.sch_tag_liv(Livello).lim_inf_tol_tag = par_mac.sch_tag_def.sch_tag_liv(Livello).lim_inf_tol_tag
        par_mac_ute.sch_tag.sch_tag_liv(Livello).lim_sup_tol_tag = par_mac.sch_tag_def.sch_tag_liv(Livello).lim_sup_tol_tag
        'par_mac_ute.sch_tag.sch_tag_liv(livello).gir_max_mot_lam = par_mac.sch_tag_def.sch_tag_liv(livello).gir_max_mot_lam   'Arturo uff 21-02-03
        par_mac_ute.sch_tag.sch_tag_liv(Livello).imp_gir_enc_lam = par_mac.sch_tag_def.sch_tag_liv(Livello).imp_gir_enc_lam
        par_mac_ute.sch_tag.sch_tag_liv(Livello).imp_gir_enc_pull_roll = par_mac.sch_tag_def.sch_tag_liv(Livello).imp_gir_enc_pull_roll
        par_mac_ute.sch_tag.sch_tag_liv(Livello).fat_mol_enc_lam = par_mac.sch_tag_def.sch_tag_liv(Livello).fat_mol_enc_lam
        par_mac_ute.sch_tag.sch_tag_liv(Livello).fat_mol_enc_ruo_mis = par_mac.sch_tag_def.sch_tag_liv(Livello).fat_mol_enc_ruo_mis
        par_mac_ute.sch_tag.sch_tag_liv(Livello).fat_mol_enc_pull_roll = par_mac.sch_tag_def.sch_tag_liv(Livello).fat_mol_enc_pull_roll
        par_mac_ute.sch_tag.sch_tag_liv(Livello).ang_sincr = par_mac.sch_tag_def.sch_tag_liv(Livello).ang_sincr
        par_mac_ute.sch_tag.sch_tag_liv(Livello).sog_err_ins = par_mac.sch_tag_def.sch_tag_liv(Livello).sog_err_ins
        par_mac_ute.sch_tag.sch_tag_liv(Livello).tem_cam_sta_vel = par_mac.sch_tag_def.sch_tag_liv(Livello).tem_cam_sta_vel
        par_mac_ute.sch_tag.sch_tag_liv(Livello).lun_tag_min = par_mac.sch_tag_def.sch_tag_liv(Livello).lun_tag_min
        par_mac_ute.sch_tag.sch_tag_liv(Livello).lun_tag_max = par_mac.sch_tag_def.sch_tag_liv(Livello).lun_tag_max
        par_mac_ute.sch_tag.sch_tag_liv(Livello).rap_rid_mot_lam = par_mac.sch_tag_def.sch_tag_liv(Livello).rap_rid_mot_lam
        par_mac_ute.sch_tag.sch_tag_liv(Livello).cir_lam = par_mac.sch_tag_def.sch_tag_liv(Livello).cir_lam
      
      Case "set_par_sch_tag2"
        par_mac_ute.sch_tag.sch_tag_liv(Livello).vel_zon_sinc_vmin = par_mac.sch_tag_def.sch_tag_liv(Livello).vel_zon_sinc_vmin
        par_mac_ute.sch_tag.sch_tag_liv(Livello).fin_vel_ott_pen = par_mac.sch_tag_def.sch_tag_liv(Livello).fin_vel_ott_pen
        par_mac_ute.sch_tag.sch_tag_liv(Livello).fat_k01_ott_pen = par_mac.sch_tag_def.sch_tag_liv(Livello).fat_k01_ott_pen
        par_mac_ute.sch_tag.sch_tag_liv(Livello).off_ram_pos = par_mac.sch_tag_def.sch_tag_liv(Livello).off_ram_pos
        par_mac_ute.sch_tag.sch_tag_liv(Livello).off_ram_tag = par_mac.sch_tag_def.sch_tag_liv(Livello).off_ram_tag
        par_mac_ute.sch_tag.sch_tag_liv(Livello).gir_mot_lam_rif = par_mac.sch_tag_def.sch_tag_liv(Livello).gir_mot_lam_rif
        par_mac_ute.sch_tag.sch_tag_liv(Livello).sov_lam = par_mac.sch_tag_def.sch_tag_liv(Livello).sov_lam
        par_mac_ute.sch_tag.sch_tag_liv(Livello).vel_sim_lap = par_mac.sch_tag_def.sch_tag_liv(Livello).vel_sim_lap
      
      Case "set_par_sch_tag4"
        par_mac_ute.sch_tag.sch_tag_liv(Livello).off_tag_reg = par_mac.sch_tag_def.sch_tag_liv(Livello).off_tag_reg
        par_mac_ute.sch_tag.sch_tag_liv(Livello).fin_pri_tac = par_mac.sch_tag_def.sch_tag_liv(Livello).fin_pri_tac
        par_mac_ute.sch_tag.sch_tag_liv(Livello).fin_dop_tac = par_mac.sch_tag_def.sch_tag_liv(Livello).fin_dop_tac
        par_mac_ute.sch_tag.sch_tag_liv(Livello).cor_tag_reg_1°_sog = par_mac.sch_tag_def.sch_tag_liv(Livello).cor_tag_reg_1°_sog
        par_mac_ute.sch_tag.sch_tag_liv(Livello).sog_vel_cor = par_mac.sch_tag_def.sch_tag_liv(Livello).sog_vel_cor
        par_mac_ute.sch_tag.sch_tag_liv(Livello).cor_tag_reg_2°_sog = par_mac.sch_tag_def.sch_tag_liv(Livello).cor_tag_reg_2°_sog
        par_mac_ute.sch_tag.sch_tag_liv(Livello).dis_FTC_tac_lam = par_mac.sch_tag_def.sch_tag_liv(Livello).dis_FTC_tac_lam
        'Iciliouff 20-11-2003
        par_mac_ute.sch_tag.sch_tag_liv(Livello).vel_ftc = par_mac.sch_tag_def.sch_tag_liv(Livello).vel_ftc
        par_mac_ute.sch_tag.sch_tag_liv(Livello).dis_cen_mac_sen_fuo_car = par_mac.sch_tag_def.sch_tag_liv(Livello).dis_cen_mac_sen_fuo_car
        par_mac_ute.sch_tag.sch_tag_liv(Livello).dis_edg_ftc = par_mac.sch_tag_def.sch_tag_liv(Livello).dis_edg_ftc
      'RFAZZI_FI19/03/2008
      Case "set_par_sch_tag5"
        par_mac_ute.sch_tag.sch_tag_liv(Livello).sov_tap_usc_per_sca = par_mac.sch_tag_def.sch_tag_liv(Livello).sov_tap_usc_per_sca
        par_mac_ute.sch_tag.sch_tag_liv(Livello).sca_pri_giu = par_mac.sch_tag_def.sch_tag_liv(Livello).sca_pri_giu
        par_mac_ute.sch_tag.sch_tag_liv(Livello).sca_dop_giu = par_mac.sch_tag_def.sch_tag_liv(Livello).sca_dop_giu
        par_mac_ute.sch_tag.sch_tag_liv(Livello).dis_sen_lam = par_mac.sch_tag_def.sch_tag_liv(Livello).dis_sen_lam
        par_mac_ute.sch_tag.sch_tag_liv(Livello).dis_lam_flp = par_mac.sch_tag_def.sch_tag_liv(Livello).dis_lam_flp
        par_mac_ute.sch_tag.sch_tag_liv(Livello).dis_lam_rul_no_crh = par_mac.sch_tag_def.sch_tag_liv(Livello).dis_lam_rul_no_crh
        par_mac_ute.sch_tag.sch_tag_liv(Livello).tem_ape_flp = par_mac.sch_tag_def.sch_tag_liv(Livello).tem_ape_flp
        par_mac_ute.sch_tag.sch_tag_liv(Livello).tem_chi_flp = par_mac.sch_tag_def.sch_tag_liv(Livello).tem_chi_flp
        par_mac_ute.sch_tag.sch_tag_liv(Livello).tem_ape_rul_no_crh = par_mac.sch_tag_def.sch_tag_liv(Livello).tem_ape_rul_no_crh
        par_mac_ute.sch_tag.sch_tag_liv(Livello).tem_chi_rul_no_crh = par_mac.sch_tag_def.sch_tag_liv(Livello).tem_chi_rul_no_crh
        par_mac_ute.sch_tag.sch_tag_liv(Livello).tem_sca_pre_ape_rul_no_crh = par_mac.sch_tag_def.sch_tag_liv(Livello).tem_sca_pre_ape_rul_no_crh
        par_mac_ute.sch_tag.sch_tag_liv(Livello).tem_sca_pre_chi_rul_no_crh = par_mac.sch_tag_def.sch_tag_liv(Livello).tem_sca_pre_chi_rul_no_crh
        par_mac_ute.sch_tag.sch_tag_liv(Livello).max_lun_sca = par_mac.sch_tag_def.sch_tag_liv(Livello).max_lun_sca
        par_mac_ute.sch_tag.sch_tag_liv(Livello).min_lun_sca = par_mac.sch_tag_def.sch_tag_liv(Livello).min_lun_sca
        par_mac_ute.sch_tag.sch_tag_liv(Livello).sog_vel_1 = par_mac.sch_tag_def.sch_tag_liv(Livello).sog_vel_1
        par_mac_ute.sch_tag.sch_tag_liv(Livello).sog_lun_1 = par_mac.sch_tag_def.sch_tag_liv(Livello).sog_lun_1
        par_mac_ute.sch_tag.sch_tag_liv(Livello).sog_vel_2 = par_mac.sch_tag_def.sch_tag_liv(Livello).sog_vel_2
        par_mac_ute.sch_tag.sch_tag_liv(Livello).sog_lun_2 = par_mac.sch_tag_def.sch_tag_liv(Livello).sog_lun_2
        par_mac_ute.sch_tag.sch_tag_liv(Livello).sog_vel_3 = par_mac.sch_tag_def.sch_tag_liv(Livello).sog_vel_3
        par_mac_ute.sch_tag.sch_tag_liv(Livello).sog_lun_3 = par_mac.sch_tag_def.sch_tag_liv(Livello).sog_lun_3
        'RFAZZI_FI27/03/2008
        par_mac_ute.sch_tag.sch_tag_liv(Livello).sog_lun_sup_3 = par_mac.sch_tag_def.sch_tag_liv(Livello).sog_lun_sup_3
        par_mac_ute.sch_tag.sch_tag_liv(Livello).tem_att_cam_pre_rul_no_crh = par_mac.sch_tag_def.sch_tag_liv(Livello).tem_att_cam_pre_rul_no_crh
      
    End Select
  End If

'Iciliouff 03-11-2003 TODO Modificare
''comunicazione scheda di taglio
'If tot Then
'    For i = 1 To par_mac.car_tec.num_liv_tag
'      snd_sch_tag i, CONFIGURA
'    Next i
'Else
'    snd_sch_tag livello, CONFIGURA
'End If
End Sub

Public Sub ges_fine_trasmissione(liv As Integer, ack As Boolean)
'Iciliouff 03-11-2003
'Gestione delle risposte da schede di taglio
Dim i As Integer
Dim chg As Boolean
  
  If sta_sch_tag.Cmd_att = CONFIGURA Then
    Select Case CASO_SND_SCH_TAG
      Case CARICA_NEW_CFG
        If ack Then
          conferma_cfg_liv (liv)
        Else
          resetta_cfg_liv (liv)
        End If
      Case MODIFICA_OLD_CFG
        If ack Then
          conferma_mod_cfg_liv (liv)
        Else
          resetta_mod_cfg_liv (liv)
        End If
      Case INSERIMENTO_CFG_DEFAULT
        If ack Then
          conferma_ins_cfg_def_liv (liv)
        Else
          resetta_ins_cfg_def_liv (liv)
        End If
      Case INSERIMENTO_CFG_ATTIVA
        If ack Then
          conferma_ins_cfg_att_liv (liv)
        Else
          resetta_ins_cfg_att_liv (liv)
        End If
      Case NOT_CFG 'salvataggio nuovi parametri
        If ack Then
          conferma_valori_liv (liv)
          'Iciliouff 03-11-2003
          messaggio msg("tra_eff_sch_tag") & " " & liv, False, False, True, liv
          If sta_bar.rmt_sta_bar_macro.esegui(tip_ope_macro.PLC_PARAM_CFG_MAC_PARAM_CUR_TAG, GetTickCount(), liv, 0) Then
            log_str "Ges_fine_trasmissione: Invio parametri macchina e parametri di configurazione a PLC", MED_PRIORITY, log_cfg
          Else
            messaggio msg("ope_eff_par"), True
            log_str "Ges_fine_trasmissione: ATTENZIONE! Non è stato possibile inviare i parametri macchina e parametri di configurazione a PLC", MED_PRIORITY, log_cfg
          End If
    '      snd_str_PLC PARAM_MAC
    '      snd_str_PLC PARAM_CFG_MAC
        Else
          resetta_valori_liv (liv)
          messaggio msg("ope_ann") & ". " & msg("err_com_sch_tag") & " " & liv, True, False, True, liv
        End If
      Case CAMBIO_STATO_MACCHINA 'salvataggio parametri di stato macchina che cambiano la configurazione per le schede di taglio
        If ack Then
          conferma_cfg_all_liv (liv)
          'carico i nuovi valori per la curva di taglio
          messaggio msg("tra_eff_sch_tag") & " " & liv, False, False, True, liv
          If sta_bar.rmt_sta_bar_macro.esegui(tip_ope_macro.PLC_PARAM_MAC_PARAM_CFG_MAC, GetTickCount(), liv, 0) Then
            log_str "Ges_fine_trasmissione: Invio curva di taglio e parametri di configurazione a PLC", MED_PRIORITY, log_cfg
          Else
            messaggio msg("ope_eff_par"), True
            log_str "Ges_fine_trasmissione: ATTENZIONE! Non è stato possibile inviare i dati di curva di taglio e parametri di configurazione a PLC", MED_PRIORITY, log_cfg
          End If
        Else
          resetta_cfg_all_liv (liv)
          messaggio msg("ope_ann") & ". " & msg("err_com_sch_tag") & " " & liv, True, False, True, liv
        End If
      Case INVIO_INIZIALE_PAR   'invio iniziale parametri scheda di taglio
        com_ini_sch_tag(liv) = ack
    End Select
  
    If (CASO_SND_SCH_TAG = CARICA_NEW_CFG Or CASO_SND_SCH_TAG = INSERIMENTO_CFG_ATTIVA _
     Or CASO_SND_SCH_TAG = INSERIMENTO_CFG_DEFAULT Or CASO_SND_SCH_TAG = MODIFICA_OLD_CFG) And ack Then
      If ack Then
        messaggio msg("tra_eff_sch_tag") & " " & liv, False, False, True, liv
      Else
        messaggio msg("ope_ann") & ". " & msg("err_com_sch_tag") & " " & liv, True, False, True, liv
      End If
      'invio di nuovo l'ordine corrente e successivo visto che l'inserimento, caricamento o modifica di una configurazione
      'potrebbe cambiare il valore della velocità massima della taglierina per gli ordini
      chg = IIf(par_mac_ute.par_cfg_att(liv).vel_max_tag <> par_mac.par_cfg_att(liv).vel_max_tag, True, False)
      inv_ord_plc(CORRENTE) = IIf(ord_mac.Count > 0 And chg, True, False)   'Forzo l'invio dell'ordine corrente
      inv_ord_plc(SUCCESSIVO) = IIf(ord_mac.Count > 1 And chg, True, False)   'Forzo l'invio dell'ordine successivo
    End If
  ElseIf sta_sch_tag.Cmd_att = INVIO_DATI_ORDINE Then
    'Risposta
'    messaggio IIf(ack, msg("tra_eff_sch_tag") & " " & liv, msg("ope_ann") & ". " & msg("err_com_sch_tag") & " " & liv), Not ack, False, True, liv
  End If
  sta_sch_tag.Cmd_att = tip_cmd.NIET    'Il comando in corso è stato gestito
  
End Sub

Public Sub resetta_cfg_all_liv(liv As Integer)

par_mac_ute.config_sch_tag_att(liv).descr = par_mac.config_sch_tag_att(liv).descr
par_mac_ute.config_sch_tag_att(liv).num_mot_liv = par_mac.config_sch_tag_att(liv).num_mot_liv
par_mac_ute.par_cfg_att(liv) = par_mac.par_cfg_att(liv)
par_abi_ute = par_abi
par_mac_ute.config_sch_tag_def(liv).descr = par_mac.config_sch_tag_def(liv).descr
par_mac_ute.par_cfg_def(liv) = par_mac.par_cfg_def(liv)
If pag_cor.name = "set_par_sch_tag3" And liv = Livello Then
    set_par_sch_tag3.fil_par_sch_tag_liv (liv)
End If

End Sub


Public Sub resetta_cfg_liv(liv As Integer)

par_mac_ute.config_sch_tag_att(liv).descr = par_mac.config_sch_tag_att(liv).descr
par_mac_ute.config_sch_tag_att(liv).num_mot_liv = par_mac.config_sch_tag_att(liv).num_mot_liv
par_mac_ute.par_cfg_att(liv) = par_mac.par_cfg_att(liv)
If Livello = liv And pag_cor.name = "set_par_sch_tag3" Then
  set_par_sch_tag3.fil_par_sch_tag_liv (liv)
End If
End Sub
Public Sub resetta_ins_cfg_att_liv(liv As Integer)

par_mac_ute.config_sch_tag_att(liv).descr = par_mac.config_sch_tag_att(liv).descr
par_mac_ute.config_sch_tag_att(liv).num_mot_liv = par_mac.config_sch_tag_att(liv).num_mot_liv
par_mac_ute.par_cfg_att(liv) = par_mac.par_cfg_att(liv)
If pag_cor.name = "set_par_sch_tag3" And liv = Livello Then
    set_par_sch_tag3.fil_par_sch_tag_liv (liv)
End If

End Sub

Public Sub resetta_ins_cfg_def_liv(liv As Integer)
par_mac_ute.config_sch_tag_att(liv).descr = par_mac.config_sch_tag_att(liv).descr
par_mac_ute.config_sch_tag_att(liv).num_mot_liv = par_mac.config_sch_tag_att(liv).num_mot_liv
par_mac_ute.config_sch_tag_def(liv).descr = par_mac.config_sch_tag_def(liv).descr
par_mac_ute.par_cfg_att(liv) = par_mac.par_cfg_att(liv)
par_mac_ute.par_cfg_def(liv) = par_mac.par_cfg_def(liv)
If pag_cor.name = "set_par_sch_tag3" And liv = Livello Then
    set_par_sch_tag3.fil_par_sch_tag_liv (liv)
    set_par_sch_tag3.fil_par_sch_tag_def_liv (liv)
End If
End Sub


Public Sub resetta_mod_cfg_liv(liv As Integer)
par_mac_ute.par_cfg_att(liv) = par_mac.par_cfg_att(liv)
If pag_cor.name = "set_par_sch_tag3" And liv = Livello Then
    set_par_sch_tag3.fil_par_sch_tag_liv (liv)
End If
If Trim$(par_mac.config_sch_tag_att(liv).descr) = "DEFAULT" Then
    par_mac_ute.par_cfg_def(liv) = par_mac.par_cfg_def(liv)
End If
End Sub


Public Sub resetta_valori_liv(liv As Integer)

par_mac_ute.sch_tag.sch_tag_liv(liv) = par_mac.sch_tag.sch_tag_liv(liv)
If num_com_sch_tag = 1 And Livello = liv Then
    Select Case pag_cor.name
      Case "set_par_sch_tag1"
        set_par_sch_tag1.fil_par_sch_tag_liv (liv)
      Case "set_par_sch_tag2"
        set_par_sch_tag2.fil_par_sch_tag_liv (liv)
      Case "set_par_sch_tag4"
        set_par_sch_tag4.fil_par_sch_tag_liv (liv)
      'RFAZZI_FI19/03/2008
      Case "set_par_sch_tag5"
        set_par_sch_tag5.fil_par_sch_tag_liv (liv)
    End Select
End If
End Sub


