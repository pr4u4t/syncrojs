Attribute VB_Name = "ges_ord"
Option Explicit

Public Function cop_ese(ini As Integer, fin As Integer, pos As Integer) As cod_ret
Dim i As Integer
Dim p As ClasseOrdine
Dim tmp_ord As New ClasseOrdine
Dim app As Integer
Dim j As Integer

cop_ese = RET_BAD
If sta_tag.MAC_IN_MANUALE(1) Or Not sta_tag.spde_OK Then
    If fin <= ord_mac_ese.Count And ini <= ord_mac_ese.Count And pos > 0 And ini > 0 And fin > 0 _
    And (pos <= (ord_mac.Count + 1) Or (pos = 1 And ord_mac.Count = 0)) Then
          messaggio msg("ope_in_cor"), False, False
          If ini > fin Then
              app = ini
              ini = fin
              fin = app
          End If
          For i = fin To ini Step -1
            Set p = ord_mac_ese.item(indice:=i)
            tmp_ord.ordine = p.ordine
            With tmp_ord
              .dat_fin = ""
              .dat_ini = ""
              .ora_fin = ""
              .ora_ini = ""
              .met_pro = ""
              .vel_med = ""
              ' ABorelli_FI09/09/2005_1
              .tem_fer = ""
              .tem_mar = ""
              .num_fer = ""
              For j = 1 To 3
                .fog_buo(j) = "0"
                .fog_sca(j) = "0"
                .fog_err(j) = "0"
              Next j
            End With
            If ord_mac.add(tmp_ord, pos) = RET_BAD Then
              messaggio msg("rag_num_max"), True, False
              Exit For
            Else
              cop_ese = RET_OK
            End If
          Next i
    Else
         messaggio msg("par_err"), True, False
    End If
Else
    log_str "cop_ese: Tentativo di copia ordini eseguiti nella lista degli ordini da eseguire con macchina in automatico", MED_PRIORITY, log_ord
    messaggio msg("mac_in_auto"), True, False
    Exit Function
End If
End Function

Public Sub ins_ord_ese_per_fine_prod()
'subroutine per l'inserimento dell'ordine lavorato nella collezione degli eseguiti nel caso di comando di fine produzione
Dim tmp_ord As ClasseOrdine
Dim ret As cod_ret
' ABorelli_FI09/09/2005_1

Dim d As Long, s As String
Dim ord_ese As tot_pro_ese

If ord_mac.Count > 0 Then
  Set tmp_ord = New ClasseOrdine
  tmp_ord.ordine = ord_mac.item(1).ordine
  'Aggiungo i dati di produzione per l'ordine appena prodotto: ora fine produzione e data fine produzione
  tmp_ord.dat_fin = Date
  ' ABorelli_FI09/09/2005_1
  tmp_ord.ora_fin = Right$("0" + Trim$(Time), 8)
  sta_tag.tempi res_ord:=False, res_tur:=False, res_gio:=False        ' refresh dei tempi di produzione ordine
  s = sta_tag.get_pro_ese()
  CopyType ord_ese, s, Len(ord_ese)
  d = DateDiff("s", Trim$(tmp_ord.dat_ini) + " " + Trim$(tmp_ord.ora_ini), Trim$(tmp_ord.dat_fin) + " " + Trim$(tmp_ord.ora_fin))
  tmp_ord.tem_mar = Format$(d - val(ord_ese.tem_fer))
  tmp_ord.num_fer = ord_ese.num_fer
  sta_tag.tempi res_ord:=True, res_tur:=False, res_gio:=False        ' reset dei tempi di produzione ordine
  
  tmp_ord.old_stag_id = ord_mac.item(1).stag_id
  tmp_ord.old_mod_id = ord_mac.item(1).mod_id
  ret = ord_mac_ese.add(tmp_ord)
  If ret <> RET_OK Then log_str "ins_ord_ese_per_fine_prod: ordine non inserito nella collezione degli eseguiti", MED_PRIORITY, log_lis
Else
  log_str "ins_ord_ese_per_fine_prod: non c'è nessun ordine in lista", MED_PRIORITY, log_lis
End If
End Sub



Public Function Shifta() As cod_ret
Dim tmp_ord_cor As ClasseOrdine
Dim tmp_ord_suc As ClasseOrdine
Dim tmp_stag_id As String * 4    'serve per mettere il vecchio stag_id nell'old_stag_id dell'ordine eseguito
Dim tmp_mod_id As String * 2    'serve per mettere il vecchio mod_id nell'old_mod_id dell'ordine eseguito
  ' ABorelli_FI09/09/2005_1
Dim ord_ese As tot_pro_ese
Dim d As Long, s As String

'andrea per rom
On Error GoTo g_err

Shifta = RET_BAD
If ord_mac.Count < 1 Then
  log_str "Shift: non c'è nessun ordine in lista", MED_PRIORITY, log_lis
Else
  Set tmp_ord_cor = ord_mac.item(1)
  If ord_mac.Count = 1 Then
      log_str "Modulo: ges_ord - Function: Shifta. L'ordine successivo non è presente", MED_PRIORITY, log_lis
  Else
    Set tmp_ord_suc = ord_mac.item(2)
    log_str "CO. Ordine eseguito -- ID SPDE: " + tmp_ord_cor.spde_id + " ID STAG: " + tmp_ord_cor.stag_id + " -> Ordine in lavoro -- ID SPDE: " + tmp_ord_suc.spde_id + " ID STAG: " + tmp_ord_suc.stag_id, MED_PRIORITY, log_lis
  End If
'Aggiungo i dati di produzione per l'ordine appena prodotto: ora fine produzione e data fine produzione
  ord_mac.item(1).dat_fin = CStr(Date)
' ABorelli_FI09/09/2005_1
  ord_mac.item(1).ora_fin = Right$("0" + Trim$(CStr(Time)), 8)
  log_str "Shifta: memorizzate data e ora di fine ordine", MED_PRIORITY, log_lis
  s = sta_tag.get_pro_ese()
  CopyType ord_ese, s, Len(ord_ese)
  sta_tag.tempi res_ord:=False, res_tur:=False, res_gio:=False        ' refresh dei tempi di produzione ordine
  ord_mac.item(1).tem_fer = ord_ese.tem_fer
  d = DateDiff("s", Trim$(ord_mac.item(1).dat_ini) + " " + Trim$(ord_mac.item(1).ora_ini), Trim$(ord_mac.item(1).dat_fin) + " " + Trim$(ord_mac.item(1).ora_fin))
  log_str "Shifta: eseguito DateDiff", MED_PRIORITY, log_lis
  ord_mac.item(1).tem_mar = Format$(d - val(ord_ese.tem_fer))
  ord_mac.item(1).num_fer = ord_ese.num_fer
  sta_tag.chg_ord
  sta_tag.tempi res_ord:=True, res_tur:=False, res_gio:=False        ' reset dei tempi di produzione ordine
  If ord_mac.Count > 1 Then
    'Aggiungo i dati di produzione per l'ordine da produrre: ora inizio produzione e data inizio produzione
    ord_mac.item(2).dat_ini = CStr(Date)
    ord_mac.item(2).ora_ini = Right$("0" + Trim$(CStr(Time)), 8)      'ABorelli_FI09/09/2005_1
  End If
  log_str "Shifta: memorizzate data e ora di inizio ordine", MED_PRIORITY, log_lis
  tmp_stag_id = ord_mac.item(1).stag_id
  tmp_mod_id = ord_mac.item(1).mod_id
  If ord_mac.Del_blk(1, 1, True, False) = RET_OK Then
'    'Aggiungo i dati di produzione per l'ordine appena prodotto: ora fine produzione e data fine produzione
    log_str "Shifta: eseguita cancellazione corrente per shift", MED_PRIORITY, log_lis
    tmp_ord_cor.old_mod_id = tmp_mod_id
    tmp_ord_cor.old_stag_id = tmp_stag_id
    ord_mac_ese.add tmp_ord_cor
'    ord_mac_ese.item(1).dat_fin = Date
'    ord_mac_ese.item(1).ora_fin = Time
'
'    'Aggiungo i dati di produzione per l'ordine da produrre: ora inizio produzione e data inizio produzione
'    ord_mac.item(1).dat_ini = Date
'    ord_mac.item(1).ora_ini = Time
    Shifta = RET_OK
  Else
    log_str "Modulo: ges_ord - Function: Shifta. Impossibile aggiungere il primo ordine nella lista eseguiti", MED_PRIORITY, log_lis
  End If
  Log_lst ord_mac 'APalmerini_FI26/01/2005_1
End If
'andrea per rom
Exit Function

g_err:
log_str "Shifta: errore " + Format(err.Number) + ", " + err.Description, MED_PRIORITY, log_gen
Resume Next
End Function

