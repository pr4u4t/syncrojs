Attribute VB_Name = "ini_bas"
Option Explicit

Const sec = "sch_tag"
Const sec_liv = "sch_tag_liv_"

Sub ini_val_non_uti()
Dim i As Integer
Dim m As Integer

'INIZIALIZZO A 0 O A "" LE VARIABILI PER I LIVELLI NON PRESENTI
For i = val(par_mac.car_tec.num_liv_tag) + 1 To 3
    par_mac.car_tec.sel_usc_liv(i) = ""
    par_mac.car_tec.pre_con_pom(i) = ""
    par_mac.opt.sca_liv(i) = ""
    par_abi.liv(i) = False
    par_abi.num_mot_abi(i) = 0
    For m = 1 To 3
      par_abi.fun_mot_liv(i).fun_mot_mot(m) = False
    Next m
    
    'parametri attivi
    par_mac.config_sch_tag_att(i).descr = ""
    par_mac.sch_tag.sch_tag_liv(i).dia_ruo_mis = ""
    par_mac.sch_tag.sch_tag_liv(i).imp_gir_enc_ruo_mis = ""
    par_mac.sch_tag.sch_tag_liv(i).vel_lam_jog = ""
    par_mac.sch_tag.sch_tag_liv(i).ang_sincr = ""
    par_mac.sch_tag.sch_tag_liv(i).cir_lam = ""
    par_mac.sch_tag.sch_tag_liv(i).cor_tag_reg_1°_sog = ""
    par_mac.sch_tag.sch_tag_liv(i).cor_tag_reg_2°_sog = ""
    par_mac.sch_tag.sch_tag_liv(i).dia_pull_roll = ""
    par_mac.sch_tag.sch_tag_liv(i).dis_fot_lam = ""
    par_mac.sch_tag.sch_tag_liv(i).dis_FTC_tac_lam = ""
    par_mac.sch_tag.sch_tag_liv(i).fat_k01_ott_pen = ""
    par_mac.sch_tag.sch_tag_liv(i).fat_mol_enc_lam = ""
    par_mac.sch_tag.sch_tag_liv(i).fat_mol_enc_pull_roll = ""
    par_mac.sch_tag.sch_tag_liv(i).fat_mol_enc_ruo_mis = ""
    par_mac.sch_tag.sch_tag_liv(i).fin_vel_ott_pen = ""
    'par_mac.sch_tag.sch_tag_liv(i).gir_max_mot_lam = ""  'Arturo uff 21-02-03
    par_mac.sch_tag.sch_tag_liv(i).imp_gir_enc_lam = ""
    par_mac.sch_tag.sch_tag_liv(i).imp_gir_enc_pull_roll = ""
    par_mac.sch_tag.sch_tag_liv(i).lun_tag_tes = ""
    par_mac.sch_tag.sch_tag_liv(i).off_tag_reg = ""
    par_mac.sch_tag.sch_tag_liv(i).off_ram_pos = ""
    par_mac.sch_tag.sch_tag_liv(i).off_ram_tag = ""
    par_mac.sch_tag.sch_tag_liv(i).rap_rid_mot_lam = ""
    par_mac.sch_tag.sch_tag_liv(i).sov_lam = ""
    par_mac.sch_tag.sch_tag_liv(i).vel_zon_sinc_vmin = ""
    par_mac.sch_tag.sch_tag_liv(i).lim_inf_tol_tag = ""
    par_mac.sch_tag.sch_tag_liv(i).lim_sup_tol_tag = ""
    par_mac.sch_tag.sch_tag_liv(i).vel_sim_lap = ""
    par_mac.sch_tag.sch_tag_liv(i).lun_tag_min = ""
    par_mac.sch_tag.sch_tag_liv(i).lun_tag_max = ""
    par_mac.sch_tag.sch_tag_liv(i).gir_mot_lam_rif = ""
    par_mac.sch_tag.sch_tag_liv(i).tem_cam_sta_vel = ""
    par_mac.sch_tag.sch_tag_liv(i).sog_err_ins = ""
    par_mac.sch_tag.sch_tag_liv(i).fin_pri_tac = ""
    par_mac.sch_tag.sch_tag_liv(i).fin_dop_tac = ""
    par_mac.sch_tag.sch_tag_liv(i).sog_vel_cor = ""
    'Iciliouff 20-11-2003
    par_mac.sch_tag.sch_tag_liv(i).vel_ftc = ""
    par_mac.sch_tag.sch_tag_liv(i).dis_cen_mac_sen_fuo_car = ""
    par_mac.sch_tag.sch_tag_liv(i).dis_edg_ftc = ""
    'Robertouff 26-11-2007
    par_mac.sch_tag.sch_tag_liv(i).sov_tap_usc_per_sca = ""
    par_mac.sch_tag.sch_tag_liv(i).sca_pri_giu = ""
    par_mac.sch_tag.sch_tag_liv(i).sca_dop_giu = ""
    par_mac.sch_tag.sch_tag_liv(i).dis_sen_lam = ""
    par_mac.sch_tag.sch_tag_liv(i).dis_lam_flp = ""
    par_mac.sch_tag.sch_tag_liv(i).dis_lam_rul_no_crh = ""
    par_mac.sch_tag.sch_tag_liv(i).tem_ape_flp = ""
    par_mac.sch_tag.sch_tag_liv(i).tem_chi_flp = ""
    par_mac.sch_tag.sch_tag_liv(i).tem_ape_rul_no_crh = ""
    par_mac.sch_tag.sch_tag_liv(i).tem_chi_rul_no_crh = ""
    par_mac.sch_tag.sch_tag_liv(i).tem_sca_pre_ape_rul_no_crh = ""
    par_mac.sch_tag.sch_tag_liv(i).tem_sca_pre_chi_rul_no_crh = ""
    par_mac.sch_tag.sch_tag_liv(i).max_lun_sca = ""
    par_mac.sch_tag.sch_tag_liv(i).min_lun_sca = ""
    par_mac.sch_tag.sch_tag_liv(i).sog_vel_1 = ""
    par_mac.sch_tag.sch_tag_liv(i).sog_lun_1 = ""
    par_mac.sch_tag.sch_tag_liv(i).sog_vel_2 = ""
    par_mac.sch_tag.sch_tag_liv(i).sog_lun_2 = ""
    par_mac.sch_tag.sch_tag_liv(i).sog_vel_3 = ""
    par_mac.sch_tag.sch_tag_liv(i).sog_lun_3 = ""
    'RFAZZI_FI27/03/2008
    par_mac.sch_tag.sch_tag_liv(i).sog_lun_sup_3 = ""
    par_mac.sch_tag.sch_tag_liv(i).tem_att_cam_pre_rul_no_crh = ""
    
    
    
 
 'parametri di default
    
    par_mac.config_sch_tag_def(i).descr = ""
    par_mac.sch_tag_def.sch_tag_liv(i).dia_ruo_mis = ""
    par_mac.sch_tag_def.sch_tag_liv(i).imp_gir_enc_ruo_mis = ""
    par_mac.sch_tag_def.sch_tag_liv(i).vel_lam_jog = ""
    par_mac.sch_tag_def.sch_tag_liv(i).ang_sincr = ""
    par_mac.sch_tag_def.sch_tag_liv(i).cir_lam = ""
    par_mac.sch_tag_def.sch_tag_liv(i).cor_tag_reg_1°_sog = ""
    par_mac.sch_tag_def.sch_tag_liv(i).cor_tag_reg_2°_sog = ""
    par_mac.sch_tag_def.sch_tag_liv(i).dia_pull_roll = ""
    par_mac.sch_tag_def.sch_tag_liv(i).dis_fot_lam = ""
    par_mac.sch_tag_def.sch_tag_liv(i).dis_FTC_tac_lam = ""
    par_mac.sch_tag_def.sch_tag_liv(i).fat_k01_ott_pen = ""
    par_mac.sch_tag_def.sch_tag_liv(i).fat_mol_enc_lam = ""
    par_mac.sch_tag_def.sch_tag_liv(i).fat_mol_enc_pull_roll = ""
    par_mac.sch_tag_def.sch_tag_liv(i).fat_mol_enc_ruo_mis = ""
    par_mac.sch_tag_def.sch_tag_liv(i).fin_vel_ott_pen = ""
    'par_mac.sch_tag_def.sch_tag_liv(i).gir_max_mot_lam = ""  'Arturo uff 21-02-03
    par_mac.sch_tag_def.sch_tag_liv(i).imp_gir_enc_lam = ""
    par_mac.sch_tag_def.sch_tag_liv(i).imp_gir_enc_pull_roll = ""
    par_mac.sch_tag_def.sch_tag_liv(i).lun_tag_tes = ""
    par_mac.sch_tag_def.sch_tag_liv(i).off_tag_reg = ""
    par_mac.sch_tag_def.sch_tag_liv(i).off_ram_pos = ""
    par_mac.sch_tag_def.sch_tag_liv(i).off_ram_tag = ""
    par_mac.sch_tag_def.sch_tag_liv(i).rap_rid_mot_lam = ""
    par_mac.sch_tag_def.sch_tag_liv(i).sov_lam = ""
    par_mac.sch_tag_def.sch_tag_liv(i).vel_zon_sinc_vmin = ""
    par_mac.sch_tag_def.sch_tag_liv(i).lim_inf_tol_tag = ""
    par_mac.sch_tag_def.sch_tag_liv(i).lim_sup_tol_tag = ""
    par_mac.sch_tag_def.sch_tag_liv(i).vel_sim_lap = ""
    par_mac.sch_tag_def.sch_tag_liv(i).lun_tag_min = ""
    par_mac.sch_tag_def.sch_tag_liv(i).lun_tag_max = ""
    par_mac.sch_tag_def.sch_tag_liv(i).gir_mot_lam_rif = ""
    par_mac.sch_tag_def.sch_tag_liv(i).tem_cam_sta_vel = ""
    par_mac.sch_tag_def.sch_tag_liv(i).sog_err_ins = ""
    par_mac.sch_tag_def.sch_tag_liv(i).fin_pri_tac = ""
    par_mac.sch_tag_def.sch_tag_liv(i).fin_dop_tac = ""
    par_mac.sch_tag_def.sch_tag_liv(i).sog_vel_cor = ""
    'Iciliouff 20-11-2003
    par_mac.sch_tag_def.sch_tag_liv(i).vel_ftc = ""
    par_mac.sch_tag_def.sch_tag_liv(i).dis_cen_mac_sen_fuo_car = ""
    par_mac.sch_tag_def.sch_tag_liv(i).dis_edg_ftc = ""
    par_mac.sch_tag_def.sch_tag_liv(i).sov_tap_usc_per_sca = ""
    par_mac.sch_tag_def.sch_tag_liv(i).sca_pri_giu = ""
    par_mac.sch_tag_def.sch_tag_liv(i).sca_dop_giu = ""
    par_mac.sch_tag_def.sch_tag_liv(i).dis_sen_lam = ""
    par_mac.sch_tag_def.sch_tag_liv(i).dis_lam_flp = ""
    par_mac.sch_tag_def.sch_tag_liv(i).dis_lam_rul_no_crh = ""
    par_mac.sch_tag_def.sch_tag_liv(i).tem_ape_flp = ""
    par_mac.sch_tag_def.sch_tag_liv(i).tem_chi_flp = ""
    par_mac.sch_tag_def.sch_tag_liv(i).tem_ape_rul_no_crh = ""
    par_mac.sch_tag_def.sch_tag_liv(i).tem_chi_rul_no_crh = ""
    par_mac.sch_tag_def.sch_tag_liv(i).tem_sca_pre_ape_rul_no_crh = ""
    par_mac.sch_tag_def.sch_tag_liv(i).tem_sca_pre_chi_rul_no_crh = ""
    par_mac.sch_tag_def.sch_tag_liv(i).max_lun_sca = ""
    par_mac.sch_tag_def.sch_tag_liv(i).min_lun_sca = ""
    par_mac.sch_tag_def.sch_tag_liv(i).sog_lun_1 = ""
    par_mac.sch_tag_def.sch_tag_liv(i).sog_vel_1 = ""
    par_mac.sch_tag_def.sch_tag_liv(i).sog_lun_2 = ""
    par_mac.sch_tag_def.sch_tag_liv(i).sog_vel_2 = ""
    par_mac.sch_tag_def.sch_tag_liv(i).sog_lun_3 = ""
    par_mac.sch_tag_def.sch_tag_liv(i).sog_vel_3 = ""
    'RFAZZI_FI27/03/2008
    par_mac.sch_tag_def.sch_tag_liv(i).sog_lun_sup_3 = ""
    par_mac.sch_tag_def.sch_tag_liv(i).tem_att_cam_pre_rul_no_crh = ""
    
   'lettura dei param. rulli di uscita per livello
    par_mac.rul_usc.rul_usc_liv(i).dis_lam_fla = ""
    par_mac.rul_usc.rul_usc_liv(i).gap_tgl_cor = ""
    par_mac.rul_usc.rul_usc_liv(i).gap_tgl_lun = ""
    par_mac.rul_usc.rul_usc_liv(i).lun_san = ""
    par_mac.rul_usc.rul_usc_liv(i).tem_ape_fla = ""
    par_mac.rul_usc.rul_usc_liv(i).tem_chi_fla = ""  'Arturo uff 07-03-03

'lettura dei parametri di comunicazione con le schede di taglio
    par_mac.com_sch_tag(i).baud = ""
    par_mac.com_sch_tag(i).bit_par = ""
    par_mac.com_sch_tag(i).dati = ""
    par_mac.com_sch_tag(i).porta = ""
    par_mac.com_sch_tag(i).bit_stop = ""
    par_mac.com_sch_tag(i).TimeOut = ""
    'agrassi_FI08-03-2010_1
    par_mac.tip_com_sch_tag(i) = 0
    par_mac.lan_sch_tag(i).por_loc = ""
    par_mac.lan_sch_tag(i).por_rem = ""
    par_mac.lan_sch_tag(i).ind_rem = ""
Next i


End Sub

Sub scr_par_abi(Optional usa_ute As Boolean = False)
'Iciliouff 03-11-2003
'Scrive i parametri macchina modificabili da operatore e/o da tecnico sul file INI
Dim i As Integer
Dim m As Integer
Dim sec As String
Dim par_tmp As tip_abi

  par_tmp = par_abi
  If usa_ute Then par_tmp = par_abi_ute
  sec = "abilitazioni"
  setFileINIString sec, "abi_com_pu", IIf(par_tmp.abi_com_PU, 1, 0), app.EXEName
  For i = 1 To par_mac.car_tec.num_liv_tag
    sec = "abi_liv_" & i
    setFileINIString sec, "liv", IIf(par_tmp.liv(i), 1, 0), app.EXEName
    For m = 1 To par_mac.car_tec.num_mot_liv_tag
      setFileINIString sec, "fun_mot" & m, IIf(par_tmp.fun_mot_liv(i).fun_mot_mot(m), 1, 0), app.EXEName
    Next m
  Next i
  
End Sub


Sub scr_par_sch_tag_att_liv_ini(liv As Integer)
Dim str As String
Dim str1 As String

str = CStr(liv)
str1 = sec_liv + str

setFileINIString str1, "dia_ruo_mis", Trim$(par_mac.sch_tag.sch_tag_liv(liv).dia_ruo_mis), app.EXEName
setFileINIString str1, "vel_lam_jog", Trim$(par_mac.sch_tag.sch_tag_liv(liv).vel_lam_jog), app.EXEName
setFileINIString str1, "imp_gir_enc_ruo_mis", Trim$(par_mac.sch_tag.sch_tag_liv(liv).imp_gir_enc_ruo_mis), app.EXEName
setFileINIString str1, "dis_fot_lam", Trim$(par_mac.sch_tag.sch_tag_liv(liv).dis_fot_lam), app.EXEName
setFileINIString str1, "dia_pull_roll", Trim$(par_mac.sch_tag.sch_tag_liv(liv).dia_pull_roll), app.EXEName
setFileINIString str1, "lun_tag_tes", Trim$(par_mac.sch_tag.sch_tag_liv(liv).lun_tag_tes), app.EXEName
setFileINIString str1, "lim_inf_tol_tag", Trim$(par_mac.sch_tag.sch_tag_liv(liv).lim_inf_tol_tag), app.EXEName
setFileINIString str1, "lim_sup_tol_tag", Trim$(par_mac.sch_tag.sch_tag_liv(liv).lim_sup_tol_tag), app.EXEName
'setFileINIString str1, "gir_max_mot_lam", Trim$(par_mac.sch_tag.sch_tag_liv(liv).gir_max_mot_lam), app.EXEName     'Arturo uff 21-02-03
setFileINIString str1, "imp_gir_enc_lam", Trim$(par_mac.sch_tag.sch_tag_liv(liv).imp_gir_enc_lam), app.EXEName
setFileINIString str1, "imp_gir_enc_pull_roll", Trim$(par_mac.sch_tag.sch_tag_liv(liv).imp_gir_enc_pull_roll), app.EXEName
setFileINIString str1, "fat_mol_enc_lam", Trim$(par_mac.sch_tag.sch_tag_liv(liv).fat_mol_enc_lam), app.EXEName
setFileINIString str1, "fat_mol_enc_ruo_mis", Trim$(par_mac.sch_tag.sch_tag_liv(liv).fat_mol_enc_ruo_mis), app.EXEName
setFileINIString str1, "fat_mol_enc_pull_roll", Trim$(par_mac.sch_tag.sch_tag_liv(liv).fat_mol_enc_pull_roll), app.EXEName
setFileINIString str1, "rap_rid_mot_lam", Trim$(par_mac.sch_tag.sch_tag_liv(liv).rap_rid_mot_lam), app.EXEName
setFileINIString str1, "cir_lam", Trim$(par_mac.sch_tag.sch_tag_liv(liv).cir_lam), app.EXEName
setFileINIString str1, "ang_sincr", Trim$(par_mac.sch_tag.sch_tag_liv(liv).ang_sincr), app.EXEName
setFileINIString str1, "sog_err_ins", Trim$(par_mac.sch_tag.sch_tag_liv(liv).sog_err_ins), app.EXEName
setFileINIString str1, "tem_cam_sta_vel", Trim$(par_mac.sch_tag.sch_tag_liv(liv).tem_cam_sta_vel), app.EXEName
setFileINIString str1, "lun_tag_min", Trim$(par_mac.sch_tag.sch_tag_liv(liv).lun_tag_min), app.EXEName
setFileINIString str1, "lun_tag_max", Trim$(par_mac.sch_tag.sch_tag_liv(liv).lun_tag_max), app.EXEName

setFileINIString str1, "vel_zon_sinc_vmin", Trim$(par_mac.sch_tag.sch_tag_liv(liv).vel_zon_sinc_vmin), app.EXEName
setFileINIString str1, "fin_vel_ott_pen", Trim$(par_mac.sch_tag.sch_tag_liv(liv).fin_vel_ott_pen), app.EXEName
setFileINIString str1, "fat_k01_ott_pen", Trim$(par_mac.sch_tag.sch_tag_liv(liv).fat_k01_ott_pen), app.EXEName
setFileINIString str1, "off_ram_pos", Trim$(par_mac.sch_tag.sch_tag_liv(liv).off_ram_pos), app.EXEName
setFileINIString str1, "off_ram_tag", Trim$(par_mac.sch_tag.sch_tag_liv(liv).off_ram_tag), app.EXEName
setFileINIString str1, "gir_mot_lam_rif", Trim$(par_mac.sch_tag.sch_tag_liv(liv).gir_mot_lam_rif), app.EXEName
setFileINIString str1, "sov_lam", Trim$(par_mac.sch_tag.sch_tag_liv(liv).sov_lam), app.EXEName
setFileINIString str1, "vel_sim_lap", Trim$(par_mac.sch_tag.sch_tag_liv(liv).vel_sim_lap), app.EXEName

setFileINIString str1, "config_att", Trim$(par_mac.config_sch_tag_att(liv).descr), app.EXEName

setFileINIString str1, "off_tag_reg", Trim$(par_mac.sch_tag.sch_tag_liv(liv).off_tag_reg), app.EXEName
setFileINIString str1, "fin_pri_tac", Trim$(par_mac.sch_tag.sch_tag_liv(liv).fin_pri_tac), app.EXEName
setFileINIString str1, "fin_dop_tac", Trim$(par_mac.sch_tag.sch_tag_liv(liv).fin_dop_tac), app.EXEName
setFileINIString str1, "sog_vel_cor", Trim$(par_mac.sch_tag.sch_tag_liv(liv).sog_vel_cor), app.EXEName
setFileINIString str1, "cor_tag_reg_1°_sog", Trim$(par_mac.sch_tag.sch_tag_liv(liv).cor_tag_reg_1°_sog), app.EXEName
setFileINIString str1, "cor_tag_reg_2°_sog", Trim$(par_mac.sch_tag.sch_tag_liv(liv).cor_tag_reg_2°_sog), app.EXEName
setFileINIString str1, "dis_FTC_tac_lam", Trim$(par_mac.sch_tag.sch_tag_liv(liv).dis_FTC_tac_lam), app.EXEName
'Iciliouff 20-11-2003
setFileINIString str1, "vel_ftc", Trim$(par_mac.sch_tag.sch_tag_liv(liv).vel_ftc), app.EXEName
setFileINIString str1, "dis_cen_mac_sen_fuo_car", Trim$(par_mac.sch_tag.sch_tag_liv(liv).dis_cen_mac_sen_fuo_car), app.EXEName
setFileINIString str1, "dis_edg_ftc", Trim$(par_mac.sch_tag.sch_tag_liv(liv).dis_edg_ftc), app.EXEName

'Robertouff 26-11-2007
setFileINIString str1, "sov_tap_usc_per_sca", Trim$(par_mac.sch_tag.sch_tag_liv(liv).sov_tap_usc_per_sca), app.EXEName
setFileINIString str1, "sca_pri_giu", Trim$(par_mac.sch_tag.sch_tag_liv(liv).sca_pri_giu), app.EXEName
setFileINIString str1, "sca_dop_giu", Trim$(par_mac.sch_tag.sch_tag_liv(liv).sca_dop_giu), app.EXEName
setFileINIString str1, "dis_sen_lam", Trim$(par_mac.sch_tag.sch_tag_liv(liv).dis_sen_lam), app.EXEName
setFileINIString str1, "dis_lam_flp", Trim$(par_mac.sch_tag.sch_tag_liv(liv).dis_lam_flp), app.EXEName
setFileINIString str1, "dis_lam_rul_no_crh", Trim$(par_mac.sch_tag.sch_tag_liv(liv).dis_lam_rul_no_crh), app.EXEName
setFileINIString str1, "tem_ape_flp", Trim$(par_mac.sch_tag.sch_tag_liv(liv).tem_ape_flp), app.EXEName
setFileINIString str1, "tem_chi_flp", Trim$(par_mac.sch_tag.sch_tag_liv(liv).tem_chi_flp), app.EXEName
'RFAZZI_FI19/03/2008
setFileINIString str1, "tem_ape_rul_no_crh", Trim$(par_mac.sch_tag.sch_tag_liv(liv).tem_ape_rul_no_crh), app.EXEName
setFileINIString str1, "tem_chi_rul_no_crh", Trim$(par_mac.sch_tag.sch_tag_liv(liv).tem_chi_rul_no_crh), app.EXEName
setFileINIString str1, "tem_sca_pre_ape_rul_no_crh", Trim$(par_mac.sch_tag.sch_tag_liv(liv).tem_sca_pre_ape_rul_no_crh), app.EXEName
setFileINIString str1, "tem_sca_pre_chi_rul_no_crh", Trim$(par_mac.sch_tag.sch_tag_liv(liv).tem_sca_pre_chi_rul_no_crh), app.EXEName
setFileINIString str1, "max_lun_sca", Trim$(par_mac.sch_tag.sch_tag_liv(liv).max_lun_sca), app.EXEName
setFileINIString str1, "min_lun_sca", Trim$(par_mac.sch_tag.sch_tag_liv(liv).min_lun_sca), app.EXEName
setFileINIString str1, "ind_vel_1", Trim$(par_mac.sch_tag.sch_tag_liv(liv).sog_vel_1), app.EXEName
setFileINIString str1, "ind_lun_1", Trim$(par_mac.sch_tag.sch_tag_liv(liv).sog_lun_1), app.EXEName
setFileINIString str1, "ind_vel_2", Trim$(par_mac.sch_tag.sch_tag_liv(liv).sog_vel_2), app.EXEName
setFileINIString str1, "ind_lun_2", Trim$(par_mac.sch_tag.sch_tag_liv(liv).sog_lun_2), app.EXEName
setFileINIString str1, "ind_vel_3", Trim$(par_mac.sch_tag.sch_tag_liv(liv).sog_vel_3), app.EXEName
setFileINIString str1, "ind_lun_3", Trim$(par_mac.sch_tag.sch_tag_liv(liv).sog_lun_3), app.EXEName
'RFAZZI_FI27/03/2008
setFileINIString str1, "ind_lun_sup_3", Trim$(par_mac.sch_tag.sch_tag_liv(liv).sog_lun_sup_3), app.EXEName
setFileINIString str1, "tem_att_cam_pre_rul_no_crh", Trim$(par_mac.sch_tag.sch_tag_liv(liv).tem_att_cam_pre_rul_no_crh), app.EXEName

    
End Sub
Sub scr_par_sch_tag_def_liv_ini(liv As Integer)

Dim str As String
Dim str1 As String

str = CStr(liv)
str1 = sec_liv + str
      
setFileINIString str1, "dia_ruo_mis_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).dia_ruo_mis), app.EXEName
setFileINIString str1, "vel_lam_jog_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).vel_lam_jog), app.EXEName
setFileINIString str1, "imp_gir_enc_ruo_mis_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).imp_gir_enc_ruo_mis), app.EXEName
setFileINIString str1, "dis_fot_lam_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).dis_fot_lam), app.EXEName
setFileINIString str1, "dia_pull_roll_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).dia_pull_roll), app.EXEName
setFileINIString str1, "lun_tag_tes_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).lun_tag_tes), app.EXEName
setFileINIString str1, "lim_inf_tol_tag_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).lim_inf_tol_tag), app.EXEName
setFileINIString str1, "lim_sup_tol_tag_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).lim_sup_tol_tag), app.EXEName
'setFileINIString str1, "gir_max_mot_lam_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).gir_max_mot_lam), app.EXEName   'Arturo uff 21-02-03
setFileINIString str1, "imp_gir_enc_lam_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).imp_gir_enc_lam), app.EXEName
setFileINIString str1, "imp_gir_enc_pull_roll_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).imp_gir_enc_pull_roll), app.EXEName
setFileINIString str1, "fat_mol_enc_lam_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).fat_mol_enc_lam), app.EXEName
setFileINIString str1, "fat_mol_enc_ruo_mis_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).fat_mol_enc_ruo_mis), app.EXEName
setFileINIString str1, "fat_mol_enc_pull_roll_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).fat_mol_enc_pull_roll), app.EXEName
setFileINIString str1, "rap_rid_mot_lam_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).rap_rid_mot_lam), app.EXEName
setFileINIString str1, "cir_lam_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).cir_lam), app.EXEName
setFileINIString str1, "ang_sincr_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).ang_sincr), app.EXEName
setFileINIString str1, "sog_err_ins_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).sog_err_ins), app.EXEName
setFileINIString str1, "tem_cam_sta_vel_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).tem_cam_sta_vel), app.EXEName
setFileINIString str1, "lun_tag_min_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).lun_tag_min), app.EXEName
setFileINIString str1, "lun_tag_max_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).lun_tag_max), app.EXEName
          
setFileINIString str1, "vel_zon_sinc_vmin_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).vel_zon_sinc_vmin), app.EXEName
setFileINIString str1, "fin_vel_ott_pen_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).fin_vel_ott_pen), app.EXEName
setFileINIString str1, "fat_k01_ott_pen_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).fat_k01_ott_pen), app.EXEName
setFileINIString str1, "off_ram_pos_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).off_ram_pos), app.EXEName
setFileINIString str1, "off_ram_tag_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).off_ram_tag), app.EXEName
setFileINIString str1, "gir_mot_lam_rif_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).gir_mot_lam_rif), app.EXEName
setFileINIString str1, "sov_lam_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).sov_lam), app.EXEName
setFileINIString str1, "vel_sim_lap_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).vel_sim_lap), app.EXEName

setFileINIString str1, "config_def", Trim$(par_mac.config_sch_tag_def(liv).descr), app.EXEName
    
setFileINIString str1, "off_tag_reg_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).off_tag_reg), app.EXEName
setFileINIString str1, "fin_pri_tac_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).fin_pri_tac), app.EXEName
setFileINIString str1, "fin_dop_tac_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).fin_dop_tac), app.EXEName
setFileINIString str1, "sog_vel_cor_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).sog_vel_cor), app.EXEName
setFileINIString str1, "cor_tag_reg_1°_sog_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).cor_tag_reg_1°_sog), app.EXEName
setFileINIString str1, "cor_tag_reg_2°_sog_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).cor_tag_reg_2°_sog), app.EXEName
setFileINIString str1, "dis_FTC_tac_lam_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).dis_FTC_tac_lam), app.EXEName
'Iciliouff 20-11-2003
setFileINIString str1, "vel_ftc_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).vel_ftc), app.EXEName
setFileINIString str1, "dis_cen_mac_sen_fuo_car_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).dis_cen_mac_sen_fuo_car), app.EXEName
setFileINIString str1, "dis_edg_ftc_def", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).dis_edg_ftc), app.EXEName
'Robertouff 26-11-2007
'RFAZZI_FI19/03/2008.Corretto sotto perchè mandavamo par_mac.sch_tag
setFileINIString str1, "sov_tap_usc_per_sca", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).sov_tap_usc_per_sca), app.EXEName
setFileINIString str1, "sca_pri_giu", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).sca_pri_giu), app.EXEName
setFileINIString str1, "sca_dop_giu", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).sca_dop_giu), app.EXEName
setFileINIString str1, "dis_sen_lam", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).dis_sen_lam), app.EXEName
setFileINIString str1, "dis_lam_flp", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).dis_lam_flp), app.EXEName
setFileINIString str1, "dis_lam_rul_no_crh", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).dis_lam_rul_no_crh), app.EXEName
setFileINIString str1, "tem_ape_flp", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).tem_ape_flp), app.EXEName
setFileINIString str1, "tem_chi_flp", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).tem_chi_flp), app.EXEName
setFileINIString str1, "tem_ape_rul_no_crh", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).tem_ape_rul_no_crh), app.EXEName
setFileINIString str1, "tem_chi_rul_no_crh", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).tem_chi_rul_no_crh), app.EXEName
setFileINIString str1, "tem_sca_pre_ape_rul_no_crh", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).tem_sca_pre_ape_rul_no_crh), app.EXEName
setFileINIString str1, "tem_sca_pre_chi_rul_no_crh", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).tem_sca_pre_chi_rul_no_crh), app.EXEName
setFileINIString str1, "max_lun_sca", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).max_lun_sca), app.EXEName
setFileINIString str1, "min_lun_sca", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).min_lun_sca), app.EXEName
setFileINIString str1, "ind_vel_1", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).sog_vel_1), app.EXEName
setFileINIString str1, "ind_lun_1", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).sog_lun_1), app.EXEName
setFileINIString str1, "ind_vel_2", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).sog_vel_2), app.EXEName
setFileINIString str1, "ind_lun_2", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).sog_lun_2), app.EXEName
setFileINIString str1, "ind_vel_3", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).sog_vel_3), app.EXEName
setFileINIString str1, "ind_lun_3", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).sog_lun_3), app.EXEName
'RFAZZI_FI27/03/2008
setFileINIString str1, "ind_lun_sup_3", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).sog_lun_sup_3), app.EXEName

setFileINIString str1, "tem_att_cam_pre_rul_no_crh", Trim$(par_mac.sch_tag_def.sch_tag_liv(liv).tem_att_cam_pre_rul_no_crh), app.EXEName


End Sub
Sub scr_par_mac()
Dim i As Integer
Dim m As Integer
'scrive i parametri macchina modificabili da operatore e/o da tecnico sul file INI

Dim sec As String

 
  
  sec = "car_tec"     'sezione caratteristiche tecniche
  setFileINIString sec, "num_liv_tag", Trim$(par_mac.car_tec.num_liv_tag), app.EXEName
  setFileINIString sec, "num_mot_liv_tag", Trim$(par_mac.car_tec.num_mot_liv_tag), app.EXEName
  setFileINIString sec, "for_mac", Trim$(par_mac.car_tec.for_mac), app.EXEName
  setFileINIString sec, "gra_car", Trim$(par_mac.car_tec.gra_car), app.EXEName  'Arturo uff 05-11-02
  setFileINIString sec, "nor_mac", Trim$(par_mac.car_tec.nor_mac), app.EXEName  'Arturo uff 06-03-03
  setFileINIString sec, "min_for_mac", Trim$(par_mac.car_tec.min_for_mac), app.EXEName
  setFileINIString sec, "min_lun_tag", Trim$(par_mac.car_tec.min_lun_tag), app.EXEName
  setFileINIString sec, "max_lun_tag", Trim$(par_mac.car_tec.max_lun_tag), app.EXEName
  setFileINIString sec, "lun_tag_sca", Trim$(par_mac.car_tec.lun_tag_sca), app.EXEName
  'RFAZZI_FI15/06/2009. Aggiunto parametro di Lunghezza di Taglio a Default differenziata in caso di Cut to Patter Automatico e Gruppo di Scarto
  setFileINIString sec, "lun_tag_sca_cut_pat_auto", Trim$(par_mac.car_tec.lun_tag_sca_pat_aut), app.EXEName
  setFileINIString sec, "sgl_abi_lin_DC_Bus", Trim$(par_mac.car_tec.sgl_abi_lin_DC_Bus), app.EXEName  'Arturo uff 09-09-03
  setFileINIString sec, "pre_all", Trim$(par_mac.car_tec.pre_all), app.EXEName
  For i = 1 To 3
    setFileINIString sec, "sel_usc_liv_" & i, Trim$(par_mac.car_tec.sel_usc_liv(i)), app.EXEName
    setFileINIString sec, "pre_con_pom_" & i, Trim$(par_mac.car_tec.pre_con_pom(i)), app.EXEName
  Next i
  setFileINIString sec, "tip_azi_tag", Trim$(par_mac.car_tec.tip_azi_tag), app.EXEName  ' ABorelli_FI11/11/2005_1
  
  sec = "opt"     'sezione optional
  setFileINIString sec, "cut_mar", Trim$(par_mac.opt.cut_mar), app.EXEName
  'agrassi_FI15-07-08
  'setFileINIString sec, "cut_pat", Trim$(par_mac.opt.cut_pat), app.EXEName      'Iciliouff 20-11-2003
  
  'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
  'setFileINIString sec, "cut_pat_auto", Trim$(par_mac.opt.cut_pat_auto), app.EXEName       'Robertouff 26-11-2007
  For i = 1 To 3
    'agrassi_FI15-07-08
    setFileINIString sec, "cut_pat_" & i, Trim$(par_mac.opt.cut_pat(i)), app.EXEName      'Iciliouff 20-11-2003
    setFileINIString sec, "sca_liv_" & i, Trim$(par_mac.opt.sca_liv(i)), app.EXEName
    'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
    setFileINIString sec, "cut_pat_auto_" & i, Trim$(par_mac.opt.cut_pat_auto(i)), app.EXEName      'Robertouff 26-11-2007
  Next i
  sec = "rul_ing"
  setFileINIString sec, "vel_max_lin", Trim$(par_mac.rul_ing.vel_max_lin), app.EXEName
  For i = 1 To 2
      sec = "rul_ing_" & i     'sezione rulli di ingresso
      setFileINIString sec, "dis_rot_mas", Trim$(par_mac.rul_ing.rul_num(i).dis_rot_mas), app.EXEName
      setFileINIString sec, "off_vel_met_min", Trim$(par_mac.rul_ing.rul_num(i).off_vel_met_min), app.EXEName
      setFileINIString sec, "off_vel_per", Trim$(par_mac.rul_ing.rul_num(i).off_vel_per), app.EXEName
      setFileINIString sec, "sov_sfi", Trim$(par_mac.rul_ing.rul_num(i).sov_sfi), app.EXEName
  Next i
  
  For i = 1 To 3
  
    'sezione rulli di uscita per livello
    sec = "rul_usc_liv_" & i
    setFileINIString sec, "dis_lam_fla", Trim$(par_mac.rul_usc.rul_usc_liv(i).dis_lam_fla), app.EXEName
    setFileINIString sec, "gap_tgl_cor", Trim$(par_mac.rul_usc.rul_usc_liv(i).gap_tgl_cor), app.EXEName
    setFileINIString sec, "gap_tgl_lun", Trim$(par_mac.rul_usc.rul_usc_liv(i).gap_tgl_lun), app.EXEName
    setFileINIString sec, "lun_san", Trim$(par_mac.rul_usc.rul_usc_liv(i).lun_san), app.EXEName
    setFileINIString sec, "tem_ape_fla", Trim$(par_mac.rul_usc.rul_usc_liv(i).tem_ape_fla), app.EXEName
    setFileINIString sec, "tem_chi_fla", Trim$(par_mac.rul_usc.rul_usc_liv(i).tem_chi_fla), app.EXEName  'Arturo uff 07-03-03
    
    'sezione comunicazione con le schede di taglio
    sec = "com_sch_tag_" & i
    setFileINIString sec, "porta", Trim$(par_mac.com_sch_tag(i).porta), app.EXEName
    setFileINIString sec, "baud", Trim$(par_mac.com_sch_tag(i).baud), app.EXEName
    setFileINIString sec, "bit_par", Trim$(par_mac.com_sch_tag(i).bit_par), app.EXEName
    setFileINIString sec, "dati", Trim$(par_mac.com_sch_tag(i).dati), app.EXEName
    setFileINIString sec, "bit_stop", Trim$(par_mac.com_sch_tag(i).bit_stop), app.EXEName
    setFileINIString sec, "timeout", Trim$(par_mac.com_sch_tag(i).TimeOut), app.EXEName
    'agrassi_FI08-03-2010_1
    setFileINIString sec, "tip_com", Trim$(par_mac.tip_com_sch_tag(i)), app.EXEName
    setFileINIString sec, "por_loc", Trim$(par_mac.lan_sch_tag(i).por_loc), app.EXEName
    setFileINIString sec, "por_rem", Trim$(par_mac.lan_sch_tag(i).por_rem), app.EXEName
    setFileINIString sec, "ind_rem", Trim$(par_mac.lan_sch_tag(i).ind_rem), app.EXEName
    'MLAMIONI_PMASSARO_FI27/10/2010 - Gestione comunicazione UDP-TCP per schede di taglio
  
    setFileINIString sec, "tip_com_udp", IIf(Trim$(par_mac.lan_sch_tag_UDP(i)) = True, 1, 0), app.EXEName
    
  Next i
     
  sec = "com_spde"      'sezione comunicazione con SPDE
  setFileINIString sec, "ver_spde", Trim$(par_mac.com_spde.ver_spde), app.EXEName
  setFileINIString sec, "tip_com", Trim$(par_mac.com_spde.tip_com), app.EXEName
  setFileINIString sec, "porta", Trim$(par_mac.com_spde.tip_porta.porta), app.EXEName
  setFileINIString sec, "baud", Trim$(par_mac.com_spde.tip_porta.baud), app.EXEName
  setFileINIString sec, "bit_par", Trim$(par_mac.com_spde.tip_porta.bit_par), app.EXEName
  setFileINIString sec, "dati", Trim$(par_mac.com_spde.tip_porta.dati), app.EXEName
  setFileINIString sec, "bit_stop", Trim$(par_mac.com_spde.tip_porta.bit_stop), app.EXEName
  setFileINIString sec, "por_loc", Trim$(par_mac.com_spde.lan.por_loc), app.EXEName
  setFileINIString sec, "por_rem", Trim$(par_mac.com_spde.lan.por_rem), app.EXEName
  setFileINIString sec, "ind_rem", Trim$(par_mac.com_spde.lan.ind_rem), app.EXEName
  setFileINIString sec, "err_max", Trim$(par_mac.com_spde.err_max), app.EXEName
  setFileINIString sec, "timeout", Trim$(par_mac.com_spde.TimeOut), app.EXEName
  
  sec = "com_plc"     'sezione comunicazione con il PLC
  setFileINIString sec, "net_id", par_mac.indirizzo_plc.net_id, app.EXEName
  setFileINIString sec, "node_id", par_mac.indirizzo_plc.node_id, app.EXEName
  setFileINIString sec, "unit_id", par_mac.indirizzo_plc.unit_id, app.EXEName
  setFileINIString sec, "timer_scans_plc", par_mac.indirizzo_plc.TIMER_SCANS_PLC, app.EXEName
  
  sec = "com_azi"      'sezione comunicazione con gli Azionamenti
  setFileINIString sec, "timeout", Trim$(par_mac.com_azi.TimeOut), app.EXEName
  setFileINIString sec, "baud", Trim$(par_mac.com_azi.baud), app.EXEName
  
  ' ABorelli_FI17/11/2006_1
  sec = "com_plc_lan"      'sezione comunicazione con il plc in rete
  setFileINIString sec, "timeout", Trim$(par_mac.com_plc_lan.TimeOut), app.EXEName
  setFileINIString sec, "por_rem", Trim$(par_mac.com_plc_lan.por_rem), app.EXEName
  setFileINIString sec, "ind_rem", Trim$(par_mac.com_plc_lan.ind_rem), app.EXEName
  'setFileINIString sec, "abi_lan", Trim$(par_mac.com_plc_lan.abi_lan), app.EXEName
End Sub


Sub leg_par_mac()
Dim i As Integer
Dim m As Integer
Dim tmp As String
Dim tmp1 As Long

'legge dal file INI i parametri macchina modificabili dalla pagina dell'operatore e dalla pagina del tecnico

'lettura dei param. caratteristiche tecniche

    par_mac.car_tec.num_mot_liv_tag = val(getFileINIString("car_tec", "num_mot_liv_tag", "2")): inc_prg_bar_ini 'Arturo uff 25-07-02
    If (val(par_mac.car_tec.num_mot_liv_tag > 3) Or val(par_mac.car_tec.num_mot_liv_tag) < 0) Then par_mac.car_tec.num_mot_liv_tag = "2"
    par_mac.car_tec.for_mac = val(getFileINIString("car_tec", "for_mac", "2")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.car_tec.gra_car = val(getFileINIString("car_tec", "gra_car", Format(tip_gra_car.gm2_1800))): inc_prg_bar_ini  'Arturo uff 05-11-02
    par_mac.car_tec.nor_mac = val(getFileINIString("car_tec", "nor_mac", Format(TIP_NOR_MAC.CE))): inc_prg_bar_ini    'Arturo uff 06-03-03
    If val(par_mac.car_tec.for_mac) < 0 Or val(par_mac.car_tec.for_mac) > 2 Then
        par_mac.car_tec.for_mac = "1"
    End If
    'Arturo uff 05-11-02 inserito controllo su validità dato di grammatura
    If par_mac.car_tec.gra_car < tip_gra_car.gm2_1800 Or par_mac.car_tec.gra_car > tip_gra_car.gm2_2600 Then
        par_mac.car_tec.gra_car = tip_gra_car.gm2_1800
    End If
    'Arturo uff 06-03-03 inserito controllo su validità dato di tipo normative macchina
    If par_mac.car_tec.nor_mac < TIP_NOR_MAC.CE Or par_mac.car_tec.nor_mac > TIP_NOR_MAC.UL Then
        par_mac.car_tec.nor_mac = TIP_NOR_MAC.CE
    End If
    par_mac.car_tec.min_for_mac = val(getFileINIString("car_tec", "min_for_mac", "200")): inc_prg_bar_ini 'Arturo uff 25-07-02
    If val(par_mac.car_tec.min_for_mac) < 0 Then
        par_mac.car_tec.min_for_mac = "200"
    End If
    par_mac.car_tec.min_lun_tag = val(getFileINIString("car_tec", "min_lun_tag", "300")): inc_prg_bar_ini 'Arturo uff 25-07-02
    If val(par_mac.car_tec.min_lun_tag) < 0 Then par_mac.car_tec.min_lun_tag = "300"
    par_mac.car_tec.max_lun_tag = val(getFileINIString("car_tec", "max_lun_tag", "6000")): inc_prg_bar_ini 'Arturo uff 25-07-02
    If val(par_mac.car_tec.max_lun_tag) < 0 Then par_mac.car_tec.max_lun_tag = "6000"
    par_mac.car_tec.lun_tag_sca = val(getFileINIString("car_tec", "lun_tag_sca", "300")): inc_prg_bar_ini 'Arturo uff 25-07-02
    If val(par_mac.car_tec.lun_tag_sca) < 0 Then par_mac.car_tec.lun_tag_sca = "300"
    'RFAZZI_FI15/06/2009. Aggiunto parametro di Lunghezza di Taglio a Default differenziata in caso di Cut to Patter Automatico e Gruppo di Scarto
    par_mac.car_tec.lun_tag_sca_pat_aut = val(getFileINIString("car_tec", "lun_tag_sca_cut_pat_auto", "300")): inc_prg_bar_ini
    If val(par_mac.car_tec.lun_tag_sca_pat_aut) < 0 Then par_mac.car_tec.lun_tag_sca_pat_aut = "300"
    par_mac.car_tec.sgl_abi_lin_DC_Bus = val(getFileINIString("car_tec", "sgl_abi_lin_DC_Bus", VAL_DEF_SGL_ABI_LIN_DC_BUS)): inc_prg_bar_ini  'Arturo uff 09-09-03
    If val(par_mac.car_tec.sgl_abi_lin_DC_Bus) < LIM_INF_SGL_ABI_LIN_DC_BUS Or val(par_mac.car_tec.sgl_abi_lin_DC_Bus) > LIM_SUP_SGL_ABI_LIN_DC_BUS Then
      par_mac.car_tec.lun_tag_sca = VAL_DEF_SGL_ABI_LIN_DC_BUS
      'RFAZZI_FI15/06/2009. Aggiunto parametro di Lunghezza di Taglio a Default differenziata in caso di Cut to Patter Automatico e Gruppo di Scarto
      par_mac.car_tec.lun_tag_sca_pat_aut = VAL_DEF_SGL_ABI_LIN_DC_BUS
    End If
    For i = 1 To val(par_mac.car_tec.num_liv_tag)
        par_mac.car_tec.sel_usc_liv(i) = val(getFileINIString("car_tec", "sel_usc_liv_" & i, "0")): inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.car_tec.pre_con_pom(i) = val(getFileINIString("car_tec", "pre_con_pom_" & i, "0")): inc_prg_bar_ini 'Arturo uff 25-07-02
    Next i
    par_mac.car_tec.pre_all = val(getFileINIString("car_tec", "pre_all", "50")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.car_tec.tip_azi_tag = val(getFileINIString("car_tec", "tip_azi_tag", "0")): inc_prg_bar_ini ' ABorelli_FI11/11/2005_1
    
    'lettura dei param. optional
    par_mac.opt.cut_mar = (getFileINIString("opt", "cut_mar", "0")): inc_prg_bar_ini 'Arturo uff 25-07-02
    'agrassi_FI15-07-08
    'par_mac.opt.cut_pat = (getFileINIString("opt", "cut_pat", "0")): inc_prg_bar_ini    'Iciliouff 20-11-2003
    Dim cut_pat_def As String
    cut_pat_def = (getFileINIString("opt", "cut_pat", "0")): inc_prg_bar_ini    'Iciliouff 20-11-2003
    
    'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
    Dim cut_pat_auto As String
    cut_pat_auto = (getFileINIString("opt", "cut_pat_auto", "0"))
    For i = 1 To val(par_mac.car_tec.num_liv_tag)
        par_mac.opt.cut_pat(i) = CStr(val(getFileINIString("opt", "cut_pat_" & i, cut_pat_def))) 'Arturo uff 25-07-02
        par_mac.opt.sca_liv(i) = CStr(val(getFileINIString("opt", "sca_liv_" & i, "0"))): inc_prg_bar_ini 'Arturo uff 25-07-02
        'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
        par_mac.opt.cut_pat_auto(i) = (getFileINIString("opt", "cut_pat_auto_" & i, cut_pat_auto)): inc_prg_bar_ini  'Robertouff 26-11-2007
    Next i
    'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
    'par_mac.opt.cut_pat_auto = (getFileINIString("opt", "cut_pat_auto", "0")): inc_prg_bar_ini   'Robertouff 26-11-2007
    
  'lettura dei param. abilitazioni
  
  par_abi.abi_com_PU = CBool(val(getFileINIString("abilitazioni", "abi_com_pu", "1"))): inc_prg_bar_ini 'Arturo uff 25-07-02
  For i = 1 To val(par_mac.car_tec.num_liv_tag)
    par_abi.liv(i) = CBool(val(getFileINIString("abi_liv_" & i, "liv", "1"))): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_abi.num_mot_abi(i) = 0
    For m = 1 To par_mac.car_tec.num_mot_liv_tag
      par_abi.fun_mot_liv(i).fun_mot_mot(m) = CBool(val(getFileINIString("abi_liv_" & i, "fun_mot" & m, "1"))): inc_prg_bar_ini 'Arturo uff 25-07-02
      If par_abi.fun_mot_liv(i).fun_mot_mot(m) Then par_abi.num_mot_abi(i) = par_abi.num_mot_abi(i) + 1
    Next m
  Next i
  'Iciliouff 08-07-2004
  par_abi.abi_rep_pro = CBool(val(getFileINIString("abilitazioni", "abi_rep_pro", "0"))): inc_prg_bar_ini
  ' ABorelli_FI09/09/2005_1
  par_abi.abi_err_com_PU = CBool(val(getFileINIString("abilitazioni", "abi_err_com_pu", "1"))): inc_prg_bar_ini
'lettura parametri per livello di taglio
  For i = 1 To val(par_mac.car_tec.num_liv_tag)
  'parametri attivi
    par_mac.config_sch_tag_att(i).descr = getFileINIString(sec_liv & i, "config_att", ""): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).dia_ruo_mis = val(getFileINIString(sec_liv & i, "dia_ruo_mis", "15933")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).imp_gir_enc_ruo_mis = val(getFileINIString(sec_liv & i, "imp_gir_enc_ruo_mis", "2048")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).vel_lam_jog = val(getFileINIString(sec_liv & i, "vel_lam_jog", "2")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).ang_sincr = val(getFileINIString(sec_liv & i, "ang_sincr", "50")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).cir_lam = val(getFileINIString(sec_liv & i, "cir_lam", "816")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).dia_pull_roll = val(getFileINIString(sec_liv & i, "dia_pull_roll", "15323")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).dis_fot_lam = val(getFileINIString(sec_liv & i, "dis_fot_lam", "12000")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).fat_k01_ott_pen = val(getFileINIString(sec_liv & i, "fat_k01_ott_pen", "30")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).fat_mol_enc_lam = val(getFileINIString(sec_liv & i, "fat_mol_enc_lam", "4")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).fat_mol_enc_pull_roll = val(getFileINIString(sec_liv & i, "fat_mol_enc_pull_roll", "4")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).fat_mol_enc_ruo_mis = val(getFileINIString(sec_liv & i, "fat_mol_enc_ruo_mis", "4")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).fin_vel_ott_pen = val(getFileINIString(sec_liv & i, "fin_vel_ott_pen", "80")): inc_prg_bar_ini 'Arturo uff 25-07-02
    'par_mac.sch_tag.sch_tag_liv(i).gir_max_mot_lam = val(getFileINIString(sec_liv & i, "gir_max_mot_lam", "")): inc_prg_bar_ini 'Arturo uff 25-07-02 'Arturo uff 21-02-03
    par_mac.sch_tag.sch_tag_liv(i).imp_gir_enc_lam = val(getFileINIString(sec_liv & i, "imp_gir_enc_lam", "2048")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).imp_gir_enc_pull_roll = val(getFileINIString(sec_liv & i, "imp_gir_enc_pull_roll", "2048")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).lun_tag_tes = val(getFileINIString(sec_liv & i, "lun_tag_tes", "200")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).off_ram_pos = val(getFileINIString(sec_liv & i, "off_ram_pos", "116")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).off_ram_tag = val(getFileINIString(sec_liv & i, "off_ram_tag", "54")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).rap_rid_mot_lam = val(getFileINIString(sec_liv & i, "rap_rid_mot_lam", "27368")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).sov_lam = val(getFileINIString(sec_liv & i, "sov_lam", "20")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).vel_zon_sinc_vmin = val(getFileINIString(sec_liv & i, "vel_zon_sinc_vmin", "15")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).lim_inf_tol_tag = val(getFileINIString(sec_liv & i, "lim_inf_tol_tag", "10")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).lim_sup_tol_tag = val(getFileINIString(sec_liv & i, "lim_sup_tol_tag", "10")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).vel_sim_lap = val(getFileINIString(sec_liv & i, "vel_sim_lap", "30")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).lun_tag_min = val(getFileINIString(sec_liv & i, "lun_tag_min", "300")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).lun_tag_max = val(getFileINIString(sec_liv & i, "lun_tag_max", "6000")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).gir_mot_lam_rif = val(getFileINIString(sec_liv & i, "gir_mot_lam_rif", "1250")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).tem_cam_sta_vel = val(getFileINIString(sec_liv & i, "tem_cam_sta_vel", "1")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag.sch_tag_liv(i).sog_err_ins = val(getFileINIString(sec_liv & i, "sog_err_ins", "2500")): inc_prg_bar_ini 'Arturo uff 25-07-02
    
    'Iciliouff 20-11-2003
    'Robertouff 26-11-2007
    'agrassi_FI15-07-08
    'If val(par_mac.opt.cut_mar) = 1 Or val(par_mac.opt.cut_pat) = 1 Or val(par_mac.opt.cut_pat_auto) = 1 Then
    
    'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
    'If val(par_mac.opt.cut_mar) = 1 Or val(par_mac.opt.cut_pat(i)) = 1 Or val(par_mac.opt.cut_pat_auto) = 1 Then
    If val(par_mac.opt.cut_mar) = 1 Or val(par_mac.opt.cut_pat(i)) = 1 Or val(par_mac.opt.cut_pat_auto(i)) = 1 Then
        par_mac.sch_tag.sch_tag_liv(i).off_tag_reg = val(getFileINIString(sec_liv & i, "off_tag_reg", "0")): inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag.sch_tag_liv(i).fin_pri_tac = val(getFileINIString(sec_liv & i, "fin_pri_tac", "0")): inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag.sch_tag_liv(i).fin_dop_tac = val(getFileINIString(sec_liv & i, "fin_dop_tac", "0")): inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag.sch_tag_liv(i).cor_tag_reg_1°_sog = val(getFileINIString(sec_liv & i, "cor_tag_reg_1°_sog", "0")): inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag.sch_tag_liv(i).cor_tag_reg_2°_sog = val(getFileINIString(sec_liv & i, "cor_tag_reg_2°_sog", "6")): inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag.sch_tag_liv(i).dis_FTC_tac_lam = val(getFileINIString(sec_liv & i, "dis_FTC_tac_lam", "1588")): inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag.sch_tag_liv(i).sog_vel_cor = val(getFileINIString(sec_liv & i, "sog_vel_cor", "180")): inc_prg_bar_ini 'Arturo uff 25-07-02
        'Iciliouff 20-11-2003
        par_mac.sch_tag.sch_tag_liv(i).vel_ftc = val(getFileINIString(sec_liv & i, "vel_ftc", "2530")): inc_prg_bar_ini
        par_mac.sch_tag.sch_tag_liv(i).dis_cen_mac_sen_fuo_car = val(getFileINIString(sec_liv & i, "dis_cen_mac_sen_fuo_car", "1370")): inc_prg_bar_ini
        par_mac.sch_tag.sch_tag_liv(i).dis_edg_ftc = val(getFileINIString(sec_liv & i, "dis_edg_ftc", "35")): inc_prg_bar_ini
    Else
        par_mac.sch_tag.sch_tag_liv(i).off_tag_reg = "": inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag.sch_tag_liv(i).fin_pri_tac = "": inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag.sch_tag_liv(i).fin_dop_tac = "": inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag.sch_tag_liv(i).cor_tag_reg_1°_sog = "": inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag.sch_tag_liv(i).cor_tag_reg_2°_sog = "": inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag.sch_tag_liv(i).dis_FTC_tac_lam = "": inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag.sch_tag_liv(i).sog_vel_cor = "": inc_prg_bar_ini 'Arturo uff 25-07-02
        'Iciliouff 20-11-2003
        par_mac.sch_tag.sch_tag_liv(i).vel_ftc = "": inc_prg_bar_ini
        par_mac.sch_tag.sch_tag_liv(i).dis_cen_mac_sen_fuo_car = "": inc_prg_bar_ini
        par_mac.sch_tag.sch_tag_liv(i).dis_edg_ftc = "": inc_prg_bar_ini
    End If
 
    'Robertouff 26-11-2007
    par_mac.sch_tag.sch_tag_liv(i).sov_tap_usc_per_sca = val(getFileINIString(sec_liv & i, "sov_tap_usc_per_sca", "100")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).sca_pri_giu = val(getFileINIString(sec_liv & i, "sca_pri_giu", "250")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).sca_dop_giu = val(getFileINIString(sec_liv & i, "sca_dop_giu", "250")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).dis_sen_lam = val(getFileINIString(sec_liv & i, "dis_sen_lam", "15000")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).dis_lam_flp = val(getFileINIString(sec_liv & i, "dis_lam_flp", "915")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).dis_lam_rul_no_crh = val(getFileINIString(sec_liv & i, "dis_lam_rul_no_crh", "800")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).tem_ape_flp = val(getFileINIString(sec_liv & i, "tem_ape_flp", "56")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).tem_chi_flp = val(getFileINIString(sec_liv & i, "tem_chi_flp", "50")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).tem_ape_rul_no_crh = val(getFileINIString(sec_liv & i, "tem_ape_rul_no_crh", "100")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).tem_chi_rul_no_crh = val(getFileINIString(sec_liv & i, "tem_chi_rul_no_crh", "100")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).tem_sca_pre_ape_rul_no_crh = val(getFileINIString(sec_liv & i, "tem_sca_pre_ape_rul_no_crh", "20")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).tem_sca_pre_chi_rul_no_crh = val(getFileINIString(sec_liv & i, "tem_sca_pre_chi_rul_no_crh", "20")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).max_lun_sca = val(getFileINIString(sec_liv & i, "max_lun_sca", "1400")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).min_lun_sca = val(getFileINIString(sec_liv & i, "min_lun_sca", "600")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).sog_vel_1 = val(getFileINIString(sec_liv & i, "ind_vel_1", "100")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).sog_lun_1 = val(getFileINIString(sec_liv & i, "ind_lun_1", "600")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).sog_vel_2 = val(getFileINIString(sec_liv & i, "ind_vel_2", "200")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).sog_lun_2 = val(getFileINIString(sec_liv & i, "ind_lun_2", "700")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).sog_vel_3 = val(getFileINIString(sec_liv & i, "ind_vel_3", "300")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).sog_lun_3 = val(getFileINIString(sec_liv & i, "ind_lun_3", "800")): inc_prg_bar_ini
    'RFAZZI_FI27/03/2008
    par_mac.sch_tag.sch_tag_liv(i).sog_lun_sup_3 = val(getFileINIString(sec_liv & i, "ind_lun_sup_3", "800")): inc_prg_bar_ini
    par_mac.sch_tag.sch_tag_liv(i).tem_att_cam_pre_rul_no_crh = val(getFileINIString(sec_liv & i, "tem_att_cam_pre_rul_no_crh", "300")): inc_prg_bar_ini
        
    'parametri di default
    
    par_mac.config_sch_tag_def(i).descr = getFileINIString(sec_liv & i, "config_def", ""): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).dia_ruo_mis = val(getFileINIString(sec_liv & i, "dia_ruo_mis_def", "15933")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).imp_gir_enc_ruo_mis = val(getFileINIString(sec_liv & i, "imp_gir_enc_ruo_mis_def", "2048")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).vel_lam_jog = val(getFileINIString(sec_liv & i, "vel_lam_jog_def", "2")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).ang_sincr = val(getFileINIString(sec_liv & i, "ang_sincr_def", "50")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).cir_lam = val(getFileINIString(sec_liv & i, "cir_lam_def", "816")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).dia_pull_roll = val(getFileINIString(sec_liv & i, "dia_pull_roll_def", "15323")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).dis_fot_lam = val(getFileINIString(sec_liv & i, "dis_fot_lam_def", "12000")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).fat_k01_ott_pen = val(getFileINIString(sec_liv & i, "fat_k01_ott_pen_def", "30")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).fat_mol_enc_lam = val(getFileINIString(sec_liv & i, "fat_mol_enc_lam_def", "4")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).fat_mol_enc_pull_roll = val(getFileINIString(sec_liv & i, "fat_mol_enc_pull_roll_def", "4")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).fat_mol_enc_ruo_mis = val(getFileINIString(sec_liv & i, "fat_mol_enc_ruo_mis_def", "4")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).fin_vel_ott_pen = val(getFileINIString(sec_liv & i, "fin_vel_ott_pen_def", "80")): inc_prg_bar_ini 'Arturo uff 25-07-02
    'par_mac.sch_tag_def.sch_tag_liv(i).gir_max_mot_lam = val(getFileINIString(sec_liv & i, "gir_max_mot_lam_def", "")): inc_prg_bar_ini 'Arturo uff 25-07-02 'Arturo uff 21-02-03
    par_mac.sch_tag_def.sch_tag_liv(i).imp_gir_enc_lam = val(getFileINIString(sec_liv & i, "imp_gir_enc_lam_def", "2048")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).imp_gir_enc_pull_roll = val(getFileINIString(sec_liv & i, "imp_gir_enc_pull_roll_def", "2048")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).lun_tag_tes = val(getFileINIString(sec_liv & i, "lun_tag_tes_def", "20")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).off_ram_pos = val(getFileINIString(sec_liv & i, "off_ram_pos_def", "116")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).off_ram_tag = val(getFileINIString(sec_liv & i, "off_ram_tag_def", "54")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).rap_rid_mot_lam = val(getFileINIString(sec_liv & i, "rap_rid_mot_lam_def", "27368")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).sov_lam = val(getFileINIString(sec_liv & i, "sov_lam_def", "20")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).vel_zon_sinc_vmin = val(getFileINIString(sec_liv & i, "vel_zon_sinc_vmin_def", "15")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).lim_inf_tol_tag = val(getFileINIString(sec_liv & i, "lim_inf_tol_tag_def", "10")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).lim_sup_tol_tag = val(getFileINIString(sec_liv & i, "lim_sup_tol_tag_def", "10")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).vel_sim_lap = val(getFileINIString(sec_liv & i, "vel_sim_lap_def", "30")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).lun_tag_min = val(getFileINIString(sec_liv & i, "lun_tag_min_def", "300")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).lun_tag_max = val(getFileINIString(sec_liv & i, "lun_tag_max_def", "6000")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).gir_mot_lam_rif = val(getFileINIString(sec_liv & i, "gir_mot_lam_rif_def", "1250")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).tem_cam_sta_vel = val(getFileINIString(sec_liv & i, "tem_cam_sta_vel_def", "1")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.sch_tag_def.sch_tag_liv(i).sog_err_ins = val(getFileINIString(sec_liv & i, "sog_err_ins_def", "2500")): inc_prg_bar_ini 'Arturo uff 25-07-02
    
    'Robertouff 26-11-2007
    'agrassi_FI15-07-08
    'If val(par_mac.opt.cut_mar) = 1 Or val(par_mac.opt.cut_pat) = 1 Or val(par_mac.opt.cut_pat_auto) = 1 Then
    
    'RFAZZI_FI09/12/2009_1. Aggiunta gestione presenza del Cut to Pattern Automatico x livello
    'If val(par_mac.opt.cut_mar) = 1 Or val(par_mac.opt.cut_pat(i)) = 1 Or val(par_mac.opt.cut_pat_auto) = 1 Then
    If val(par_mac.opt.cut_mar) = 1 Or val(par_mac.opt.cut_pat(i)) = 1 Or val(par_mac.opt.cut_pat_auto(i)) = 1 Then
        par_mac.sch_tag_def.sch_tag_liv(i).cor_tag_reg_1°_sog = val(getFileINIString(sec_liv & i, "cor_tag_reg_1°_sog_def", "0")): inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag_def.sch_tag_liv(i).cor_tag_reg_2°_sog = val(getFileINIString(sec_liv & i, "cor_tag_reg_2°_sog_def", "6")): inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag_def.sch_tag_liv(i).off_tag_reg = val(getFileINIString(sec_liv & i, "off_tag_reg_def", "0")): inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag_def.sch_tag_liv(i).fin_pri_tac = val(getFileINIString(sec_liv & i, "fin_pri_tac_def", "0")): inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag_def.sch_tag_liv(i).fin_dop_tac = val(getFileINIString(sec_liv & i, "fin_dop_tac_def", "0")): inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag_def.sch_tag_liv(i).dis_FTC_tac_lam = val(getFileINIString(sec_liv & i, "dis_FTC_tac_lam_def", "1588")): inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag_def.sch_tag_liv(i).sog_vel_cor = val(getFileINIString(sec_liv & i, "sog_vel_cor_def", "180")): inc_prg_bar_ini 'Arturo uff 25-07-02
        'Iciliouff 20-11-2003
        par_mac.sch_tag_def.sch_tag_liv(i).vel_ftc = val(getFileINIString(sec_liv & i, "vel_ftc_def", "2530")): inc_prg_bar_ini
        par_mac.sch_tag_def.sch_tag_liv(i).dis_cen_mac_sen_fuo_car = val(getFileINIString(sec_liv & i, "dis_cen_mac_sen_fuo_car_def", "1370")): inc_prg_bar_ini
        par_mac.sch_tag_def.sch_tag_liv(i).dis_edg_ftc = val(getFileINIString(sec_liv & i, "dis_edg_ftc_def", "35")): inc_prg_bar_ini
    Else
        par_mac.sch_tag_def.sch_tag_liv(i).cor_tag_reg_1°_sog = "": inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag_def.sch_tag_liv(i).cor_tag_reg_2°_sog = "": inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag_def.sch_tag_liv(i).off_tag_reg = "": inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag_def.sch_tag_liv(i).fin_pri_tac = "": inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag_def.sch_tag_liv(i).fin_dop_tac = "": inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag_def.sch_tag_liv(i).dis_FTC_tac_lam = "": inc_prg_bar_ini 'Arturo uff 25-07-02
        par_mac.sch_tag_def.sch_tag_liv(i).sog_vel_cor = "": inc_prg_bar_ini 'Arturo uff 25-07-02
        'Iciliouff 20-11-2003
        par_mac.sch_tag_def.sch_tag_liv(i).vel_ftc = "": inc_prg_bar_ini
        par_mac.sch_tag_def.sch_tag_liv(i).dis_cen_mac_sen_fuo_car = "": inc_prg_bar_ini
        par_mac.sch_tag_def.sch_tag_liv(i).dis_edg_ftc = "": inc_prg_bar_ini
    End If
    
    'Robertouff 26-11-2007
    par_mac.sch_tag_def.sch_tag_liv(i).sov_tap_usc_per_sca = val(getFileINIString(sec_liv & i, "sov_tap_usc_per_sca", "100")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).sca_pri_giu = val(getFileINIString(sec_liv & i, "sca_pri_giu", "250")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).sca_dop_giu = val(getFileINIString(sec_liv & i, "sca_dop_giu", "250")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).dis_sen_lam = val(getFileINIString(sec_liv & i, "dis_sen_lam", "15000")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).dis_lam_flp = val(getFileINIString(sec_liv & i, "dis_lam_flp", "915")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).dis_lam_rul_no_crh = val(getFileINIString(sec_liv & i, "dis_lam_rul_no_crh", "800")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).tem_ape_flp = val(getFileINIString(sec_liv & i, "tem_ape_flp", "56")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).tem_chi_flp = val(getFileINIString(sec_liv & i, "tem_chi_flp", "50")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).tem_ape_rul_no_crh = val(getFileINIString(sec_liv & i, "tem_ape_rul_no_crh", "100")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).tem_chi_rul_no_crh = val(getFileINIString(sec_liv & i, "tem_chi_rul_no_crh", "100")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).tem_sca_pre_ape_rul_no_crh = val(getFileINIString(sec_liv & i, "tem_sca_pre_ape_rul_no_crh", "20")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).tem_sca_pre_chi_rul_no_crh = val(getFileINIString(sec_liv & i, "tem_sca_pre_chi_rul_no_crh", "20")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).max_lun_sca = val(getFileINIString(sec_liv & i, "max_lun_sca", "1400")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).min_lun_sca = val(getFileINIString(sec_liv & i, "min_lun_sca", "600")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).sog_vel_1 = val(getFileINIString(sec_liv & i, "ind_vel_1", "100")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).sog_lun_1 = val(getFileINIString(sec_liv & i, "ind_lun_1", "600")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).sog_vel_2 = val(getFileINIString(sec_liv & i, "ind_vel_2", "200")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).sog_lun_2 = val(getFileINIString(sec_liv & i, "ind_lun_2", "700")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).sog_vel_3 = val(getFileINIString(sec_liv & i, "ind_vel_3", "300")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).sog_lun_3 = val(getFileINIString(sec_liv & i, "ind_lun_3", "800")): inc_prg_bar_ini
    'RFAZZI_FI27/03/2008
    par_mac.sch_tag_def.sch_tag_liv(i).sog_lun_sup_3 = val(getFileINIString(sec_liv & i, "ind_lun_sup_3", "800")): inc_prg_bar_ini
    par_mac.sch_tag_def.sch_tag_liv(i).tem_att_cam_pre_rul_no_crh = val(getFileINIString(sec_liv & i, "tem_att_cam_pre_rul_no_crh", "300")): inc_prg_bar_ini
    
    'lettura dei param. rulli di uscita per livello
    par_mac.rul_usc.rul_usc_liv(i).dis_lam_fla = val(getFileINIString("rul_usc_liv_" & i, "dis_lam_fla", "")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.rul_usc.rul_usc_liv(i).gap_tgl_cor = val(getFileINIString("rul_usc_liv_" & i, "gap_tgl_cor", "")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.rul_usc.rul_usc_liv(i).gap_tgl_lun = val(getFileINIString("rul_usc_liv_" & i, "gap_tgl_lun", "")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.rul_usc.rul_usc_liv(i).lun_san = val(getFileINIString("rul_usc_liv_" & i, "lun_san", "")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.rul_usc.rul_usc_liv(i).tem_ape_fla = val(getFileINIString("rul_usc_liv_" & i, "tem_ape_fla", "0")): inc_prg_bar_ini 'Arturo uff 25-07-02 'Arturo uff 07-03-03 modificato default
    par_mac.rul_usc.rul_usc_liv(i).tem_chi_fla = val(getFileINIString("rul_usc_liv_" & i, "tem_chi_fla", "0")): inc_prg_bar_ini 'Arturo uff 07-03-03

    'lettura dei parametri di comunicazione con le schede di taglio
    tmp1 = val(getFileINIString("com_sch_tag_" & i, "baud", "38400")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.com_sch_tag(i).baud = IIf(tmp1 = 0, "38400", CStr(tmp1))
    tmp = Trim$(getFileINIString("com_sch_tag_" & i, "bit_par", "e")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.com_sch_tag(i).bit_par = IIf(tmp = "", "e", tmp)
    tmp1 = val(getFileINIString("com_sch_tag_" & i, "dati", "8")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.com_sch_tag(i).dati = IIf(tmp1 = 0, "8", CStr(tmp1))
    tmp1 = val(getFileINIString("com_sch_tag_" & i, "porta", i + 2)): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.com_sch_tag(i).porta = IIf(tmp1 = 0, CStr(i + 2), CStr(tmp1))
    tmp1 = val(getFileINIString("com_sch_tag_" & i, "bit_stop", "1")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.com_sch_tag(i).bit_stop = IIf(tmp1 = 0, "1", CStr(tmp1))
    tmp1 = val(getFileINIString("com_sch_tag_" & i, "timeout", "2000")): inc_prg_bar_ini 'Arturo uff 25-07-02
    par_mac.com_sch_tag(i).TimeOut = IIf(tmp1 = 0, "2000", CStr(tmp1))
    'agrassi_FI08-03-2010_1
    par_mac.tip_com_sch_tag(i) = val(getFileINIString("com_sch_tag_" & i, "tip_com", "1"))
    par_mac.lan_sch_tag(i).por_loc = val(getFileINIString("com_sch_tag_" & i, "por_loc", "4001"))
    par_mac.lan_sch_tag(i).por_rem = val(getFileINIString("com_sch_tag_" & i, "por_rem", "4000"))
    par_mac.lan_sch_tag(i).ind_rem = getFileINIString("com_sch_tag_" & i, "ind_rem", "127.0.0.1")
    
    'MLAMIONI_PMASSARO_FI27/10/2010 - Gestione comunicazione UDP-TCP per schede di taglio
    tmp1 = val(getFileINIString("com_sch_tag_" & i, "tip_com_udp", "1")): inc_prg_bar_ini
    par_mac.lan_sch_tag_UDP(i) = IIf(Trim$(tmp1) = True, 1, 0)
    
      
Next i

'lettura dei param. rulli di ingresso
par_mac.rul_ing.vel_max_lin = val(getFileINIString("rul_ing", "vel_max_lin", "350")): inc_prg_bar_ini 'Arturo uff 25-07-02

par_mac.rul_ing.rul_num(1).dis_rot_mas = val(getFileINIString("rul_ing_1", "dis_rot_mas", "")): inc_prg_bar_ini 'Arturo uff 25-07-02
par_mac.rul_ing.rul_num(1).off_vel_met_min = val(getFileINIString("rul_ing_1", "off_vel_met_min", "")): inc_prg_bar_ini 'Arturo uff 25-07-02
par_mac.rul_ing.rul_num(1).off_vel_per = val(getFileINIString("rul_ing_1", "off_vel_per", "")): inc_prg_bar_ini 'Arturo uff 25-07-02
par_mac.rul_ing.rul_num(1).sov_sfi = val(getFileINIString("rul_ing_1", "sov_sfi", "")): inc_prg_bar_ini 'Arturo uff 25-07-02

If val(par_mac.car_tec.num_liv_tag) = 3 Then
  par_mac.rul_ing.rul_num(2).dis_rot_mas = val(getFileINIString("rul_ing_2", "dis_rot_mas", "")): inc_prg_bar_ini 'Arturo uff 25-07-02
  par_mac.rul_ing.rul_num(2).off_vel_met_min = val(getFileINIString("rul_ing_2", "off_vel_met_min", "")): inc_prg_bar_ini 'Arturo uff 25-07-02
  par_mac.rul_ing.rul_num(2).off_vel_per = val(getFileINIString("rul_ing_2", "off_vel_per", "")): inc_prg_bar_ini 'Arturo uff 25-07-02
  par_mac.rul_ing.rul_num(2).sov_sfi = val(getFileINIString("rul_ing_2", "sov_sfi", "")): inc_prg_bar_ini 'Arturo uff 25-07-02
Else
  par_mac.rul_ing.rul_num(2).dis_rot_mas = "": inc_prg_bar_ini 'Arturo uff 25-07-02
  par_mac.rul_ing.rul_num(2).off_vel_met_min = "": inc_prg_bar_ini 'Arturo uff 25-07-02
  par_mac.rul_ing.rul_num(2).off_vel_per = "": inc_prg_bar_ini 'Arturo uff 25-07-02
  par_mac.rul_ing.rul_num(2).sov_sfi = "": inc_prg_bar_ini 'Arturo uff 25-07-02
End If

'lettura dei param. di comunicazione con SPDE
par_mac.com_spde.ver_spde = val(getFileINIString("com_spde", "ver_spde", "2")): inc_prg_bar_ini 'Arturo uff 25-07-02
par_mac.com_spde.tip_com = val(getFileINIString("com_spde", "tip_com", "1")): inc_prg_bar_ini 'Arturo uff 25-07-02
par_mac.com_spde.tip_porta.porta = val(getFileINIString("com_spde", "porta", "6")): inc_prg_bar_ini 'Arturo uff 25-07-02
par_mac.com_spde.tip_porta.baud = val(getFileINIString("com_spde", "baud", "19200")): inc_prg_bar_ini 'Arturo uff 25-07-02
tmp = Trim$(getFileINIString("com_spde", "bit_par", "n")): inc_prg_bar_ini 'Arturo uff 25-07-02
par_mac.com_spde.tip_porta.bit_par = IIf(tmp = "", "n", tmp)
par_mac.com_spde.tip_porta.dati = val(getFileINIString("com_spde", "dati", "8")): inc_prg_bar_ini 'Arturo uff 25-07-02
par_mac.com_spde.tip_porta.bit_stop = val(getFileINIString("com_spde", "bit_stop", "1")): inc_prg_bar_ini 'Arturo uff 25-07-02
par_mac.com_spde.lan.por_loc = val(getFileINIString("com_spde", "por_loc", "100")): inc_prg_bar_ini 'Arturo uff 25-07-02
par_mac.com_spde.lan.por_rem = val(getFileINIString("com_spde", "por_rem", "200")): inc_prg_bar_ini 'Arturo uff 25-07-02
tmp = Trim$(getFileINIString("com_spde", "ind_rem", "200.0.0.200")): inc_prg_bar_ini 'Arturo uff 25-07-02
par_mac.com_spde.lan.ind_rem = IIf(tmp = "", "200.0.0.200", tmp)
par_mac.com_spde.err_max = val(getFileINIString("com_spde", "err_max", "3")): inc_prg_bar_ini 'Arturo uff 25-07-02
par_mac.com_spde.TimeOut = val(getFileINIString("com_spde", "timeout", "2000")): inc_prg_bar_ini 'Arturo uff 25-07-02

'lettura dei param. di comunicazione con PLC
par_mac.indirizzo_plc.net_id = val(getFileINIString("com_plc", "net_id", "2")): inc_prg_bar_ini 'Arturo uff 25-07-02
par_mac.indirizzo_plc.node_id = val(getFileINIString("com_plc", "node_id", "230")): inc_prg_bar_ini 'Arturo uff 25-07-02
par_mac.indirizzo_plc.unit_id = val(getFileINIString("com_plc", "unit_id", "0")): inc_prg_bar_ini 'Arturo uff 25-07-02
par_mac.indirizzo_plc.TIMER_SCANS_PLC = val(getFileINIString("com_plc", "timer_scans_plc", "30")): inc_prg_bar_ini 'Arturo uff 25-07-02

'lettura dei param. di comunicazione con gli Azionamenti
par_mac.com_azi.TimeOut = val(getFileINIString("com_azi", "timeout", "2000")): inc_prg_bar_ini 'Arturo uff 25-07-02
par_mac.com_azi.baud = val(getFileINIString("com_azi", "baud", "1000")): inc_prg_bar_ini 'Arturo uff 25-07-02

'ABorelli_FI17/11/2006_1 lettura dei paramteri di comunicazione con plc in lan
par_mac.com_plc_lan.ind_rem = Trim$(getFileINIString("com_plc_lan", "ind_rem", "127.0.0.1"))
par_mac.com_plc_lan.por_rem = Trim$(getFileINIString("com_plc_lan", "por_rem", "50000"))
par_mac.com_plc_lan.TimeOut = val(getFileINIString("com_plc_lan", "timeout", "1000"))
'par_mac.com_plc_lan.abi_lan = val(getFileINIString("com_plc_lan", "abi_lan", "1"))
ini_val_non_uti

While Frm_ini.prg_bar_ini.Value <= Frm_ini.prg_bar_ini.Max - 1
  inc_prg_bar_ini 'Arturo uff 25-07-02
Wend
End Sub

Sub leg_par_sup()
Dim i As Integer
Dim item As String

If Trim$(UCase$(Command())) = "UTE" Then
  pro_set.ute_wrk = True 'disabilito tutte le sicurezze
  pro_set.ute_wrk_liv1 = True 'abilito accesso a pagine ingegneristiche
End If

pro_set.lin_att = val(getFileINIString("pro_set", "lin_att", "0"))
pro_set.per_cli = getFileINIString("pro_set", "per_cli", "")
pro_set.pro_pat = getFileINIString("pro_set", "pro_pat", "C:\usr\stag\")
'IDisperati_FI 21/08/2006
pro_set.nam_fil_log_zip = getFileINIString("pro_set", "nam_fil_log_zip", "Stag_ZipListFile_LOG.txt")
pro_set.nam_fil_cod_zip = getFileINIString("pro_set", "nam_fil_cod_zip", "Stag_ZipListFile_COD.txt")
pro_set.nam_fil_dat_zip = getFileINIString("pro_set", "nam_fil_dat_zip", "Stag_ZipListFile_DAT.txt")
pro_set.pro_bck_loc_zip = getFileINIString("pro_set", "pro_bck_loc_zip", "C:\BACKUP")
pro_set.pro_bck_rem_zip = getFileINIString("pro_set", "pro_bck_rem_zip", "\\MDW\BACKUP")

pro_set.Nam_fil_uni = getFileINIString("pro_set", "Nam_fil_uni", "Ordini")
pro_set.Nam_fil_dat_ord = getFileINIString("pro_set", "Nam_fil_dat_ord", "Ord_mac.dat")
pro_set.Nam_fil_dat_ord_ese = getFileINIString("pro_set", "Nam_fil_dat_ord_ese", "Ord_mac_ese.dat")
'pro_set.ute_wrk = IIf(val(getFileINIString("pro_set", "ute_wrk", "0")) = 0, False, True)
pro_set.abi_reg_rep_pro = getFileINIString("pro_set", "abi_reg_rep_pro", STR_FALSE)
pro_set.ini_prod = getFileINIString("pro_set", "ini_prod", STR_FALSE)
pro_set.pwd = getFileINIString("pro_set", "pwd", "")
pro_set.pwd_liv1 = getFileINIString("pro_set", "pwd_liv1", "") '5122001 Stagetti gestione password abilitazione pagine ingegneristiche
pro_set.ric_msg_db = IIf(val(getFileINIString("pro_set", "ric_msg_db", "0")) = 0, False, True)
abi_mis_ang = IIf(val(getFileINIString("pro_set", "Inch", "0")) = 0, False, True)
For i = 1 To cos_log.log_eve
    item = Choose(i, "Plc", "Ord", "Spde", "Rmt", "Gen", "Lis", "Prob", "Can", "Cfg", "Cur", "Sch_tag", "Azi", "Eve")
    pro_set.Log(i).Nam_fil_log = pro_set.pro_pat + getFileINIString("LOG", "Nam_fil_log_" + item, item + ".log")
    pro_set.Log(i).Nam_fil_bak = pro_set.pro_pat + getFileINIString("LOG", "Nam_fil_bak_" + item, item + "_old.log")
    pro_set.Log(i).MAX_LEN_LOG = val(getFileINIString("LOG", "Max_len_log_" + item, "1400000&"))
    pro_set.Log(i).Dsk_priority = val(getFileINIString("LOG", "Dsk_priority_" + item, str$(DSK_DEF_PRIORITY)))
Next i
' ABorelli_FI09/09/2005_1
pro_set.tur = val(getFileINIString("pro_set", "tur", "1"))
If pro_set.tur > 3 Or pro_set.tur < 1 Then
    pro_set.tur = 1
End If
End Sub

