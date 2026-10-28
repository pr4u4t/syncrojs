Attribute VB_Name = "conv_ord"
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Type tip_rep_pro_old
  ora_ini As String * 8
  dat_ini As String * 8
  ora_fin As String * 8
  dat_fin As String * 8
  met_tot As String * 8
  vel_med As String * 3
  liv(1 To 3) As tip_rep_pro_liv
End Type

Type Tip_Ordine_old
  'Da inserire
  cod_ord_abb As String * 11   'Codice abbinamento
  spde_id As String * 4          'codice univoco spde
  stag_id As String * 4           'codice univoco stag
  mod_id As String * 2            'campo modifica ordine
  old_stag_id As String * 4           'vecchio codice univoco stag per gli ordini eseguiti
  old_mod_id As String * 2            'vecchio campo modifica ordine per gli ordini eseguiti
  tip_car As Tip_ONDA         'Dati del Cartone
  alt_bob As String * 5       'Altezza della bobina in mm/10
  met_tot_ord As String * 8 ' metri totali dell'ordine
  pre_all As String * 3   'metri per preallarme
  ord(1 To 3) As Tip_Formato
  rep_pro As tip_rep_pro_old  'tipo per il report di produzione
End Type




Sub cnv_ord()
Dim i As Integer
Dim ord_new As Tip_Ordine
Dim ord_old As Tip_Ordine_old
Const FIL_ORD_NEW = "Ord_mac_new.dat"
Const FIL_ORD_ESE_NEW = "Ord_mac_ese_new.dat"
Const FIL_ORD_BAC = "Ord_mac_ori.dat"
Const FIL_ORD_ESE_BAC = "Ord_mac_ese_ori.dat"

'IDisperati_FI 21/08/2006
On Local Error GoTo ErrorHandler

  If FileLen(pro_set.pro_pat + pro_set.Nam_fil_dat_ord) = CLng(MAX_NUM_ORD) * Len(ord_old) Then
      ' backup
      FileCopy pro_set.pro_pat + pro_set.Nam_fil_dat_ord, pro_set.pro_pat + FIL_ORD_BAC
      ' import ordini
      Open pro_set.pro_pat + pro_set.Nam_fil_dat_ord For Random As #1 Len = Len(ord_old)
      Open pro_set.pro_pat + FIL_ORD_NEW For Random As #2 Len = Len(ord_new)
      For i = 1 To MAX_NUM_ORD
          Get #1, i, ord_old
          ord_new.cod_ord_abb = ord_old.cod_ord_abb
          ord_new.spde_id = ord_old.spde_id
          ord_new.stag_id = ord_old.stag_id
          ord_new.mod_id = ord_old.mod_id
          ord_new.old_stag_id = ord_old.old_stag_id
          ord_new.old_mod_id = ord_old.old_mod_id
          ord_new.tip_car = ord_old.tip_car
          ord_new.alt_bob = ord_old.alt_bob
          ord_new.met_tot_ord = ord_old.met_tot_ord
          ord_new.pre_all = ord_old.pre_all
          ord_new.ord(1) = ord_old.ord(1)
          ord_new.ord(2) = ord_old.ord(2)
          ord_new.ord(3) = ord_old.ord(3)
          ord_new.rep_pro.dat_fin = ord_old.rep_pro.dat_fin
          ord_new.rep_pro.ora_ini = ord_old.rep_pro.ora_ini
          ord_new.rep_pro.dat_ini = ord_old.rep_pro.dat_ini
          ord_new.rep_pro.ora_fin = ord_old.rep_pro.ora_fin
          ord_new.rep_pro.dat_fin = ord_old.rep_pro.dat_fin
          ord_new.rep_pro.met_tot = ord_old.rep_pro.met_tot
          ord_new.rep_pro.tem_fer = ""
          ord_new.rep_pro.tem_mar = ""
          ord_new.rep_pro.num_fer = ""
          ord_new.rep_pro.vel_med = ord_old.rep_pro.vel_med
          ord_new.rep_pro.liv(1) = ord_old.rep_pro.liv(1)
          ord_new.rep_pro.liv(2) = ord_old.rep_pro.liv(2)
          ord_new.rep_pro.liv(3) = ord_old.rep_pro.liv(3)
          Put #2, i, ord_new
      Next i
      Close #1
      Close #2
      ' sostituisco il vecchio file con il nuovo
      FileCopy pro_set.pro_pat + FIL_ORD_NEW, pro_set.pro_pat + pro_set.Nam_fil_dat_ord
      log_str "Archivio ordini aggiornato al nuovo formato", MED_PRIORITY, log_gen
  End If
  If FileLen(pro_set.pro_pat + pro_set.Nam_fil_dat_ord_ese) = CLng(MAX_NUM_ORD_ESE) * Len(ord_old) Then
      ' backup
      FileCopy pro_set.pro_pat + pro_set.Nam_fil_dat_ord_ese, pro_set.pro_pat + FIL_ORD_ESE_BAC
      ' import ordini eseguiti
      Open pro_set.pro_pat + pro_set.Nam_fil_dat_ord_ese For Random As #1 Len = Len(ord_old)
      Open pro_set.pro_pat + FIL_ORD_ESE_NEW For Random As #2 Len = Len(ord_new)
      For i = 1 To MAX_NUM_ORD_ESE
          Get #1, i, ord_old
          ord_new.cod_ord_abb = ord_old.cod_ord_abb
          ord_new.spde_id = ord_old.spde_id
          ord_new.stag_id = ord_old.stag_id
          ord_new.mod_id = ord_old.mod_id
          ord_new.old_stag_id = ord_old.old_stag_id
          ord_new.old_mod_id = ord_old.old_mod_id
          ord_new.tip_car = ord_old.tip_car
          ord_new.alt_bob = ord_old.alt_bob
          ord_new.met_tot_ord = ord_old.met_tot_ord
          ord_new.pre_all = ord_old.pre_all
          ord_new.ord(1) = ord_old.ord(1)
          ord_new.ord(2) = ord_old.ord(2)
          ord_new.ord(3) = ord_old.ord(3)
          ord_new.rep_pro.dat_fin = ord_old.rep_pro.dat_fin
          ord_new.rep_pro.ora_ini = ord_old.rep_pro.ora_ini
          ord_new.rep_pro.dat_ini = ord_old.rep_pro.dat_ini
          ord_new.rep_pro.ora_fin = ord_old.rep_pro.ora_fin
          ord_new.rep_pro.dat_fin = ord_old.rep_pro.dat_fin
          ord_new.rep_pro.met_tot = ord_old.rep_pro.met_tot
          ord_new.rep_pro.tem_fer = ""
          ord_new.rep_pro.tem_mar = ""
          ord_new.rep_pro.num_fer = ""
          ord_new.rep_pro.vel_med = ord_old.rep_pro.vel_med
          ord_new.rep_pro.liv(1) = ord_old.rep_pro.liv(1)
          ord_new.rep_pro.liv(2) = ord_old.rep_pro.liv(2)
          ord_new.rep_pro.liv(3) = ord_old.rep_pro.liv(3)
          Put #2, i, ord_new
      Next i
      Close #1
      Close #2
      ' sostituisco il vecchio file con il nuovo
      FileCopy pro_set.pro_pat + FIL_ORD_ESE_NEW, pro_set.pro_pat + pro_set.Nam_fil_dat_ord_ese
      log_str "Archivio ordini eseguiti aggiornato al nuovo formato", MED_PRIORITY, log_gen
  End If
  Exit Sub
  
ErrorHandler:
  log_str "Cnv_ord: Err.N.=" & Format(err.Number) & " Description=" & err.Description, MED_PRIORITY, log_gen
  Close #1
  Close #2
  err.Clear
    

End Sub
