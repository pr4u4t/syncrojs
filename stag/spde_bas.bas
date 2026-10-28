Attribute VB_Name = "com_spde_bas"

Option Explicit
Dim id_space As Integer 'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON

'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
'Funzione di riempimento struttura da inviare al plc (passata la lunghezza inviata da depm)
Function rie_ord_simon(lenght As Variant, ordine As Tip_Com_ord) As Tip_Com_ord
'Indice 1 = L1 - INF
'Indice 2 = L2 - MED/SUP
'Indice 3 = L3 - SUP (Solo 3 Liv)
Dim i As Integer
Dim lastIdx As Integer
Dim mt_tot_max As Long
lastIdx = 1
mt_tot_max = 0
For i = 1 To par_mac.car_tec.num_liv_tag
  With ordine.rec.ord(lastIdx)
    If lenght(i) > 0 Then
      If (lenght(i) > 9999) Then lenght(i) = 9999
      .sca_lun = Format(lenght(i)) + "0"
      .com.cod_cli = "none"
      .com.cod_com_ord = "none"
      .cut_to_mark = "0"
      .nom_cli = "none"
      .Ofs_Cut = "0000"
      .Ord_liv = Format(i)
      .sca_lar = "6000"
      .Sca_per_tag = "1"
      .sca_tot_num = "100000"
      .met = Format((.sca_tot_num / .Sca_per_tag) * (val(lenght(i)) / 1000))
      lastIdx = lastIdx + 1
      If (.met > mt_tot_max) Then
        mt_tot_max = .met
      End If
    End If
  End With
Next i

With ordine.rec
  .met_tot_ord = Format(mt_tot_max)
  .spde_id = "0000"
  .spde_ModID = "00"
  .alt_bob = "18000"
  .cod_ord_abb = "01234567890"
  .tip_car.Alt_ond = "0030"
  .tip_car.Cod_cod_ond = "0123"
  .tip_car.cod_com_car = "012345678901234567890123456789012345"
  .tip_car.Cod_ond = "A"
  .tip_car.Pes_mt2 = "0100"
  .tip_car.Vel_aoc = "0350"
  .tip_car.vel_pro = "0350"
End With
rie_ord_simon = ordine

End Function

'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
'Funzione di controllo per la lunghezza del livello passato
Function get_cut_len_simon_from_string(str_to_check As String) As Integer
Dim chr_app As String 'carattere di appoggio
Dim str_app As String 'stringa di appoggio
Dim i As Integer
str_app = ""

If str_to_check <> "" Then  'controllo la stringa passata alla funzione
  str_to_check = LTrim(str_to_check) 'tolgo eventuali spazi presenti
  If Len(str_to_check) > 0 Then 'controllo se adesso la stringa non è vuota, se lo è loggo messaggio di errore
    For i = 1 To Len(str_to_check)
      chr_app = Mid(str_to_check, i, 1) 'prendo un carattere alla volta dopo "L(i)\" corrispondende alla lunghezza
      Select Case chr_app
        Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
          str_app = str_app + chr_app
          If val(str_app) > 32767 Then  'controllo che il valore della stringa non sia maggiore della dimensione massima di integer consentita dal sistema
            log_str "com_spde_bas - get_cut_len_simon_from_string : lunghezza di taglio (" + str_app + ") > max integer(32767). Forzo la lunghezza a 9999", MED_PRIORITY, log_spde
            str_app = "9999"
            Exit For
          End If
        Case Else
          Exit For
        End Select
    Next i
  Else
    log_str "com_spde_bas - get_cut_len_simon_from_string : lunghezza di taglio (" + str_to_check + ") errata(vuota o tutti spazi).Forzo la lunghezza a 0", MED_PRIORITY, log_spde
  End If
  get_cut_len_simon_from_string = val(str_app)
Else
  get_cut_len_simon_from_string = 0
End If

End Function
Function con_agg_ond_car(ord As Tip_Com_ord) As Boolean
'si cerca il tipo di onda+carta  inviato
Dim ss1 As Recordset
Dim s As String

con_agg_ond_car = False

If Trim$(ord.rec.tip_car.Cod_ond) = "" Then Exit Function 'niente da aggiungere o controllare
If Asc(Trim$(ord.rec.tip_car.Cod_ond)) = 0 Then Exit Function 'niente da aggiungere o controllare

If Trim$(ord.rec.tip_car.cod_com_car) = "" Then
  ord.rec.tip_car.cod_com_car = "ONDA" + Space$(1) + Trim$(ord.rec.tip_car.Cod_ond) 'carta di default
Else
  If Asc(Trim$(ord.rec.tip_car.cod_com_car)) = 0 Then
    ord.rec.tip_car.cod_com_car = "ONDA" + Space$(1) + Trim$(ord.rec.tip_car.Cod_ond) 'carta di default
  End If
End If
'andrea 24-02-06
's = "select * from ONDE where Ucase$(ONDA)='" + UCase$(Trim$(ord.rec.tip_car.Cod_ond)) + "'  AND CARTA='" + Trim$(ord.rec.tip_car.cod_com_car) + "' "
s = "select * from ONDE where Ucase(ONDA)='" + UCase$(Trim$(ord.rec.tip_car.Cod_ond)) + "'  AND CARTA='" + Trim$(ord.rec.tip_car.cod_com_car) + "' "
Set ss1 = db.OpenRecordset(s, dbOpenDynaset)
If ss1.RecordCount = 0 Then
  ss1.AddNew
Else
  ss1.Edit
End If
ss1("ONDA") = Trim$(Rie_nul(ord.rec.tip_car.Cod_ond))
ss1("CARTA") = Trim$(Rie_nul(ord.rec.tip_car.cod_com_car))
ss1("SPESSORE") = Trim$(val(Rie_nul(ord.rec.tip_car.Alt_ond)))
ss1("GRAMMATURA") = Trim$(val(Rie_nul(ord.rec.tip_car.Pes_mt2)))
ss1("VELDIPROD") = Trim$(val(Rie_nul(ord.rec.tip_car.vel_pro)))
ss1("VELDICAMBI") = Trim$(val(Rie_nul(ord.rec.tip_car.Vel_aoc)))
ss1("CODONDA") = Trim$(val(Rie_nul(ord.rec.tip_car.Cod_cod_ond)))
ss1("RISERVATO") = "0"

ss1.Update
ss1.Close

con_agg_ond_car = True

End Function
Function dig_con_agg_ond_car(ord As Tip_Ordine) As Boolean
'si cerca il tipo di onda+carta  inviato
Dim ss1 As Recordset
Dim s As String

dig_con_agg_ond_car = False

If Trim$(ord.tip_car.Cod_ond) = "" Then Exit Function 'niente da aggiungere o controllare
If Asc(Trim$(ord.tip_car.Cod_ond)) = 0 Then Exit Function 'niente da aggiungere o controllare

If Trim$(ord.tip_car.cod_com_car) = "" Then
  ord.tip_car.cod_com_car = "ONDA" + Space$(1) + Trim$(ord.tip_car.Cod_ond) 'carta di default
Else
  If Asc(Trim$(ord.tip_car.cod_com_car)) = 0 Then
    ord.tip_car.cod_com_car = "ONDA" + Space$(1) + Trim$(ord.tip_car.Cod_ond) 'carta di default
  End If
End If
'andrea 24-02-06
's = "select * from ONDE where Ucase$(ONDA)='" + UCase$(Trim$(ord.tip_car.Cod_ond)) + "'  AND CARTA='" + Trim$(ord.tip_car.cod_com_car) + "' "
s = "select * from ONDE where Ucase(ONDA)='" + UCase$(Trim$(ord.tip_car.Cod_ond)) + "'  AND CARTA='" + Trim$(ord.tip_car.cod_com_car) + "' "
Set ss1 = db.OpenRecordset(s, dbOpenDynaset)
If ss1.RecordCount = 0 Then
  ss1.AddNew
Else
  ss1.Edit
End If
ss1("ONDA") = Trim$(Rie_nul(ord.tip_car.Cod_ond))
ss1("CARTA") = Trim$(Rie_nul(ord.tip_car.cod_com_car))
ss1("SPESSORE") = Trim$(val(Rie_nul(ord.tip_car.Alt_ond)) \ TODECMM)
ss1("GRAMMATURA") = Trim$(val(Rie_nul(ord.tip_car.Pes_mt2)))
ss1("VELDIPROD") = Trim$(val(Rie_nul(ord.tip_car.vel_pro)))
ss1("VELDICAMBI") = Trim$(val(Rie_nul(ord.tip_car.Vel_aoc)))
ss1("CODONDA") = Trim$(val(Rie_nul(ord.tip_car.Cod_cod_ond)))
ss1("RISERVATO") = "0"

ss1.Update
ss1.Close

dig_con_agg_ond_car = True

End Function

Sub ric_dat_spde(arr As String)
Dim r As String
Static rx As String 'stringa per la memorizzazione dei messaggi in ingresso
Static tx As String 'stringa per la memorizzazione dei messaggi in uscita
Static ult_tx As String
Dim lrc As String
Dim ret As cod_ret
Dim ret_sta As cod_ret
Dim sta_rec As String  ' per lo status record (stato macchina)
Dim ord_ese_rec As String  ' per ordine eseguito ' ABorelli_FI09/09/2005_1
Dim tur_gio_rec As String  ' per consuntivi turno giorno ' ABorelli_FI09/09/2005_1
Dim dfr_spde As tip_pro_ord_fosber20_FOS
Dim dfr_spde_21 As tip_pro_ord_fosber21_FOS     'Iciliouff 20-11-2003
Dim dfr_spde_30 As tip_pro_ord_fosber30_FOS     'Robertouff 26-11-2007
Dim ord As Tip_Com_ord  'variabile per le operazioni sulla lista degli ordini
Dim pos_in_que As Integer
Dim temp As ClasseOrdine
Dim ordine As Tip_Ordine

'************************************************************************************************************************
'*********************************      ROUTINE PROTOCOLLO SPDE2000-STAG      **************************************
'************************************************************************************************************************

r = arr
'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
'If Right$(r, 1) = Chr$(ETX_TAG) Then
If par_mac.com_spde.ver_spde <> Simon10 And Right$(r, 1) = Chr$(ETX_TAG) Then
  rx = rx + Left$(r, Len(r) - 1)  'accodo i caratteri ricevuti ai precedenti (ma levo l'ETX)
  rx = Mid$(rx, 2) 'elimino anche il primo carattere (STX)
  If Len(rx) < COM_SPDE_LEN_BCC + 1 Then 'ho ricevuto troppi pochi caratteri => c'è stato qualche problema
    'Pieri Lenzoni LOC. UTP 22-02-2002 formattazione standard protocollo con SPDE
    tx = tx_to_spde(Chr$(32) + NAK + Format$(COD_ERR_COM, "0000")) 'trasmetto un messaggio di errore
    rx = ""
    ult_tx = tx: tra_dat_spde ult_tx
    log_str "com_spde_bas - sub ric_dat_spde: Bad length received", MED_PRIORITY, log_spde
    Exit Sub
  Else 'ho ricevuto un numero valido di caratteri
    'controllo del LRC
    r = Right$(rx, 2) 'in r memorizzo LRC
    rx = Left$(rx, Len(rx) - (COM_SPDE_LEN_BCC)) 'levo i caratteri che compongono LRC
    lrc = LRC_FCS(rx)
    If r <> lrc Then 'errore nella trasmissione
      'Pieri Lenzoni LOC. UTP 22-02-2002 formattazione standard protocollo con SPDE
      tx = tx_to_spde(Chr$(32) + NAK + Format$(COD_ERR_COM, "0000")) 'trasmetto un messaggio di errore
      rx = ""
      ult_tx = tx: tra_dat_spde ult_tx
      log_str "com_spde_bas - sub ric_dat_spde: Bad lrc received", MED_PRIORITY, log_spde
      Exit Sub
    Else
'      If con_SPDE = 0 And par_mac.com_spde.tip_com = 1 Then
'        'invia la richiesta per l'aggiornamento dell'ora da punto unico
'        'l'invio avviene quando con_spde = 0 => ad ogni avvio del programma
'        sta_bar.sck_tim.RemoteHost = par_mac.com_spde.lan.ind_rem
'        sta_bar.sck_tim.LocalPort = 0
'        sta_bar.sck_tim.SendData Chr$(10)
'      End If
      con_SPDE = GetTickCount()
      If Left$(rx, 1) <> "S" Then log_str "com_spde_bas - sub ric_dat_spde: ricevuto comando " + rx, MED_PRIORITY, log_spde
      r = Left$(rx, 1): rx = Mid$(rx, 2) 'prelevo il Format Record ID
      Select Case r
      
        '*******************************************************************************************************
        Case DFR  'Download Format Record  (ricezione da SPDE di un ordine)
        '*******************************************************************************************************
          If Not sta_tag.MAC_IN_MANUALE(1) And Not sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1) Then
            Select Case par_mac.com_spde.ver_spde
              Case Fosber20 'protocollo con SPDE_2000
                If Len(rx) = Len(dfr_spde) Then
                  CopyType dfr_spde, rx, Len(rx) 'riempio la struttura dfr_spde
                  pos_in_que = val(dfr_spde.Pos_lis)
                Else
                  'Pieri Lenzoni LOC. UTP 22-02-2002 formattazione standard protocollo con SPDE
                  tx = tx_to_spde(LCase(DFR) + NAK + Format$(COD_ERR_DAT_NON_OK, "0000")) 'trasmetto un messaggio di errore
                  rx = ""
                  ult_tx = tx: tra_dat_spde ult_tx
                  log_str "com_spde_bas - sub ric_dat_spde: Bad order lenght received", MED_PRIORITY, log_spde
                  Exit Sub
                End If
              Case Fosber21
                'Iciliouff 20-11-2003
                If Len(rx) = Len(dfr_spde_21) Then
                  CopyType dfr_spde_21, rx, Len(rx) 'riempio la struttura dfr_spde_21
                  pos_in_que = val(dfr_spde_21.Pos_lis)
                Else
                  tx = tx_to_spde(LCase(DFR) + NAK + Format$(COD_ERR_DAT_NON_OK, "0000")) 'trasmetto un messaggio di errore
                  rx = ""
                  ult_tx = tx: tra_dat_spde ult_tx
                  log_str "com_spde_bas - sub ric_dat_spde: Bad order lenght received", MED_PRIORITY, log_spde
                  Exit Sub
                End If
              Case Fosber30
                'Robertouff 26-11-2007
                If Len(rx) = Len(dfr_spde_30) Then
                  CopyType dfr_spde_30, rx, Len(rx) 'riempio la struttura dfr_spde_30
                  pos_in_que = val(dfr_spde_30.Pos_lis)
                Else
                  tx = tx_to_spde(LCase(DFR) + NAK + Format$(COD_ERR_DAT_NON_OK, "0000")) 'trasmetto un messaggio di errore
                  rx = ""
                  ult_tx = tx: tra_dat_spde ult_tx
                  log_str "com_spde_bas - sub ric_dat_spde: Bad order lenght received", MED_PRIORITY, log_spde
                  Exit Sub
                End If
            End Select
            pos_in_que = pos_in_que + 1
            If chk_pos(pos_in_que) Then
              Select Case par_mac.com_spde.ver_spde
                Case Fosber20 'protocollo con SPDE_2000
                  rie_ord_rec ord, dfr_spde 'riempio l'ordine con i dati contenuti in dfr_spde
'                  If Not con_agg_ond_car(ord) Then  ' manca l'onda
'                    tx = tx_to_spde(LCase(DFR) + NAK + Format$(COD_ERR_ASS_OND, "0000")) 'trasmetto un messaggio di errore
'                    rx = ""
'                    messaggio msg("rif_ord_spde") & " (" & pos_in_que & ") ", True, False
'                    ult_tx = tx: tra_dat_spde ult_tx
'                    log_str "com_spde_bas - sub ric_dat_spde: Bad order flute received", MED_PRIORITY, log_spde
'                    Exit Sub
'                  End If
                Case Fosber21 'Informazioni Cut To Pattern
                  'Iciliouff 20-11-2003
                  rie_ord_rec_21 ord, dfr_spde_21 'riempio l'ordine con i dati contenuti in dfr_spde_21
                  
                Case Fosber30 'Informazioni Cut To Pattern
                  'Robertouff 26-11-2007
                  rie_ord_rec_30 ord, dfr_spde_30 'riempio l'ordine con i dati contenuti in dfr_spde_30
              End Select
              If par_mac.com_spde.ver_spde = Fosber20 Or par_mac.com_spde.ver_spde = Fosber21 Or par_mac.com_spde.ver_spde = Fosber30 Then
                If Not con_agg_ond_car(ord) Then  ' manca l'onda
                  tx = tx_to_spde(LCase(DFR) + NAK + Format$(COD_ERR_ASS_OND, "0000")) 'trasmetto un messaggio di errore
                  rx = ""
                  messaggio msg("rif_ord_spde") & " (" & pos_in_que & ") ", True, False
                  ult_tx = tx: tra_dat_spde ult_tx
                  log_str "com_spde_bas - sub ric_dat_spde: Bad order flute received", MED_PRIORITY, log_spde
                  Exit Sub
                End If
              End If
              
              'VALIDAZIONE DELL' ORDINE RICEVUTO
              val_ord ord
              If ord.ret = RET_OK Then
                Set temp = New ClasseOrdine
                CopyType ordine, Space$(Len(ordine)), Len(ordine)
                ordine = ord.rec
                temp.ordine = Ord_str(ordine)
                If ord_mac.Count < pos_in_que Then 'verifico se l'ordine deve essere aggiunto alla lista o è una modifica di un ordine già esistente
                  ret = ord_mac.add(temp, pos_in_que, True)
                Else
                  ret = ord_mac.Modifica(temp, pos_in_que, True)
                End If
                If ret = RET_BAD Then 'raggiunto il numero massimo nella lista ordini MAX_NUM_ORD o posizione errata
                  tx = tx_to_spde(LCase(DFR) + NAK + Format$(COD_ERR_LIS_ORD, "0000")) 'trasmetto un messaggio di errore
                  messaggio msg("rif_ord_spde") & " (" & pos_in_que & ") ", True, False
                  rx = ""
                  ult_tx = tx: tra_dat_spde ult_tx
                  log_str "com_spde_bas - sub ric_dat_spde: Failed adding in order list pos ", MED_PRIORITY, log_spde
                  Exit Sub
                Else
                  'Pieri Lenzoni LOC. UTP 22-02-2002 formattazione standard protocollo con SPDE
                  tx = tx_to_spde(LCase(DFR) + ack + String$(4, Chr$(32))) 'trasmetto un messaggio di OK
                  ult_tx = tx: tra_dat_spde ult_tx
                  messaggio msg("ric_ord_spde") & " (" & pos_in_que & ") ", False, False
                  'RFAZZI_FI 05-01-08
'                  log_str "com_spde_bas - sub ric_dat_spde: Successfully added record posizione " + CStr(pos_in_que) _
'                  + " SPDE_ID/MOD_ID " + ord.rec.spde_id + "/" + ord.rec.mod_id, MED_PRIORITY, log_spde
                  log_str "com_spde_bas - sub ric_dat_spde: Successfully added record posizione " + CStr(pos_in_que) _
                  + " SPDE_ID/MOD_ID " + ord.rec.spde_id + "/" + ord.rec.spde_ModID, MED_PRIORITY, log_spde
                End If
              Else  'ord.ret <> RET_OK
                tx = tx_to_spde(LCase(DFR) + NAK + Format$(ord.ret, "0000")) 'trasmetto un messaggio di errore
                messaggio msg("rif_ord_spde") & " (" & pos_in_que & ") " & ": " + Format$(ord.ret), True, False         'ABorelli_FI09/09/2005_1

                rx = ""
                ult_tx = tx: tra_dat_spde ult_tx
                log_str "com_spde_bas - sub ric_dat_spde: Validation failed: cod " + Format$(ord.ret) + ":" + msg(msg_err_val_ord(var_ord_ute.ret)), MED_PRIORITY, log_spde
                Exit Sub
              End If
            Else  'chk_pos(pos_in_que)= False
              tx = tx_to_spde(LCase(DFR) + NAK + Format$(COD_ERR_LIS_ORD, "0000")) 'trasmetto un messaggio di errore
              messaggio msg("rif_ord_spde") & " (" & pos_in_que & ") ", True, False
              rx = ""
              ult_tx = tx: tra_dat_spde ult_tx
              log_str "com_spde_bas - sub ric_dat_spde: Bad order lenght received", MED_PRIORITY, log_spde
              Exit Sub
            End If
          ElseIf sta_tag.MAC_IN_MANUALE(1) Then
            tx = tx_to_spde(LCase(DFR) + NAK + Format$(COD_ERR_MAC_IN_MAN, "0000")) 'trasmetto un messaggio di errore
            messaggio msg("rif_ord_spde"), True, False
            rx = ""
            ult_tx = tx: tra_dat_spde ult_tx
            log_str "com_spde_bas - sub ric_dat_spde: Invio/Modifica ordine rifiutato: Macchina in manuale", MED_PRIORITY, log_spde
            Exit Sub
          ElseIf sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1) Then
            tx = tx_to_spde(LCase(DFR) + NAK + Format$(COD_ERR_CAM_ORD_IN_COR, "0000")) 'trasmetto un messaggio di errore
            messaggio msg("rif_ord_spde"), True, False
            rx = ""
            ult_tx = tx: tra_dat_spde ult_tx
            log_str "com_spde_bas - sub ric_dat_spde: Invio/Modifica ordine rifiutato: Cambio ordine in corso", MED_PRIORITY, log_spde
            Exit Sub
          End If
          
        '*******************************************************************************************************
        Case ERA  'Erase Record
        '*******************************************************************************************************
          If Not sta_tag.MAC_IN_MANUALE(1) And Not sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1) Then
            If Len(rx) = 1 Then
              pos_in_que = val(Right$(rx, 2)) + 1
              If ord_mac.Count >= pos_in_que Then 'posso cancellare
                ret = ord_mac.Del_blk(pos_in_que, pos_in_que, False, True)
                If ret = RET_OK Then
                  'Pieri Lenzoni LOC. UTP 22-02-2002 formattazione standard protocollo con SPDE
                  tx = tx_to_spde(LCase(ERA) + ack + String$(4, Chr$(32))) 'trasmetto un messaggio di OK
                  ult_tx = tx: tra_dat_spde ult_tx
                  log_str "com_spde_bas - sub ric_dat_spde: Successfully deleted record n. " + Format$(pos_in_que), MED_PRIORITY, log_spde
                  Exit Sub
                Else  'parametro posizione errato
                  tx = tx_to_spde(LCase(ERA) + NAK + Format$(COD_ERR_LIS_ORD, "0000")) 'trasmetto un messaggio di errore
                  rx = ""
                  ult_tx = tx: tra_dat_spde ult_tx
                  log_str "com_spde_bas - sub ric_dat_spde: Failed deleting in order list pos " + Format$(pos_in_que), MED_PRIORITY, log_spde
                  Exit Sub
                End If
              Else  'errore sulla posizione della cancellazione
                tx = tx_to_spde(LCase(ERA) + NAK + Format$(COD_ERR_LIS_ORD, "0000")) 'trasmetto un messaggio di errore
                rx = ""
                ult_tx = tx: tra_dat_spde ult_tx
                log_str "com_spde_bas - sub ric_dat_spde: Bad position in erase received", MED_PRIORITY, log_spde
                Exit Sub
              End If
            Else  'errore sulla lunghezza della stringa
              tx = tx_to_spde(LCase(ERA) + NAK + Format$(COD_ERR_DAT_NON_OK, "0000")) 'trasmetto un messaggio di errore
              rx = ""
              ult_tx = tx: tra_dat_spde ult_tx
              log_str "com_spde_bas - sub ric_dat_spde: Bad erase lenght received", MED_PRIORITY, log_spde
              Exit Sub
            End If
          ElseIf sta_tag.MAC_IN_MANUALE(1) Then
            tx = tx_to_spde(LCase(ERA) + NAK + Format$(COD_ERR_MAC_IN_MAN, "0000")) 'trasmetto un messaggio di errore
            rx = ""
            ult_tx = tx: tra_dat_spde ult_tx
            log_str "com_spde_bas - sub ric_dat_spde: Comando di cancellazione ordine rifiutato: Macchina in manuale", MED_PRIORITY, log_spde
            Exit Sub
          ElseIf sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1) Then
            tx = tx_to_spde(LCase(DFR) + NAK + Format$(COD_ERR_CAM_ORD_IN_COR, "0000")) 'trasmetto un messaggio di errore
            rx = ""
            ult_tx = tx: tra_dat_spde ult_tx
            log_str "com_spde_bas - sub ric_dat_spde: Comando di cancellazione ordine rifiutato: Cambio ordine in corso", MED_PRIORITY, log_spde
            Exit Sub
          End If
          
        '*******************************************************************************************************
        Case USR  'Upload Status Record   (invio stato macchina a SPDE)
        '*******************************************************************************************************
          Select Case par_mac.com_spde.ver_spde
            Case Fosber20, Fosber21
              ret_sta = rie_sta_rec(sta_rec)
              If ret_sta = RET_OK Then
                tx = tx_to_spde(LCase(USR) + sta_rec)
                ult_tx = tx: tra_dat_spde ult_tx
              Else
                'Pieri Lenzoni LOC. UTP 22-02-2002 formattazione standard protocollo con SPDE
                tx = tx_to_spde(LCase(USR) + NAK + Format$(COD_ERR_INV_DAT, "0000")) 'trasmetto un messaggio di errore
                rx = ""
                ult_tx = tx: tra_dat_spde ult_tx
                log_str "com_spde_bas - sub ric_dat_spde: Problem filling status record", MED_PRIORITY, log_spde
                Exit Sub
              End If
            'Robertouff 26-11-2007
            Case Fosber30
              ret_sta = rie_sta_rec_30(sta_rec)
              If ret_sta = RET_OK Then
                tx = tx_to_spde(LCase(USR) + sta_rec)
                ult_tx = tx: tra_dat_spde ult_tx
              Else
                tx = tx_to_spde(LCase(USR) + NAK + Format$(COD_ERR_INV_DAT, "0000")) 'trasmetto un messaggio di errore
                rx = ""
                ult_tx = tx: tra_dat_spde ult_tx
                log_str "com_spde_bas - sub ric_dat_spde: Problem filling status record", MED_PRIORITY, log_spde
                Exit Sub
              End If
          End Select
      
        ' ABorelli_FI09/09/2005_1
        '*******************************************************************************************************
        Case ore  ' last produced order data ' ABorelli_FI09/09/2005_1
        '*******************************************************************************************************
          Select Case par_mac.com_spde.ver_spde
            Case Fosber20
              ret = rie_ord_ese_rec(ord_ese_rec)
              tx = tx_to_spde(LCase(ore) + ord_ese_rec)
              log_str "com_spde_bas - returning " + tx, MED_PRIORITY, log_spde
              ult_tx = tx: tra_dat_spde ult_tx
          End Select
          
        ' ABorelli_FI09/09/2005_1
        '*******************************************************************************************************
        Case REP  'Shift and day production report ' ABorelli_FI09/09/2005_1
        '*******************************************************************************************************
          Select Case par_mac.com_spde.ver_spde
            Case Fosber20
              ret = rie_tur_gio_rec(tur_gio_rec)
              tx = tx_to_spde(LCase(REP) + tur_gio_rec)
              log_str "com_spde_bas - returning " + tx, MED_PRIORITY, log_spde
              ult_tx = tx: tra_dat_spde ult_tx
            Case Else
          End Select
        ' Robertouff 26-11-2007
        '*******************************************************************************************************
        Case UFR  'Past Order production report
        '*******************************************************************************************************
          Select Case par_mac.com_spde.ver_spde
            Case Fosber30
              ret = rie_ord_ese_rec_30(ord_ese_rec)
              tx = tx_to_spde(LCase(UFR) + ord_ese_rec)
              log_str "com_spde_bas - returning " + tx, MED_PRIORITY, log_spde
              ult_tx = tx: tra_dat_spde ult_tx
          End Select

      End Select
      'APalmerini_FI26/01/2005_1
      If r <> USR Then
        Log_lst ord_mac
      End If
      
    End If
  End If
  rx = ""  'svuoto la stringa dei caratteri ricevuti al termine della gestione
  
'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Inizio
ElseIf par_mac.com_spde.ver_spde = Simon10 Then
  con_SPDE = GetTickCount()
  Select Case Right(r, 1)
    Case REQ_TAG_SIMON  'ricevuto comando stato comunicazione a taglierina "?"
    rx = r
      If Len(rx) = 1 Then
        tx = tx_to_spde("K")  'Invio risposta K - comunicazione stabilita con DEPM
        log_str "com_spde_bas - returning " + tx, MED_PRIORITY, log_spde
        ult_tx = tx: tra_dat_spde ult_tx
      'PRocchiccioli_FI_16/12/2010 Emulazione con protocollo taglierina SIMON
      Else
        log_str "com_spde_bas - Error: len(rx)=" + Format(Len(rx)), MED_PRIORITY, log_spde
      End If
    Case ETX_TAG_SIMON  'controllo se il carattere corrisponde a ")"
    rx = r
    log_str "ric_dat_spde: ricevuto comando da DECC: " + rx, MED_PRIORITY, log_spde
    If Left$(rx, 1) = DFR_SIMON Then 'ricevuto comando invio ordine a taglierina "("
      rx = Left$(r, Len(r) - 1)  'accodo i caratteri ricevuti ai precedenti (ma levo l'ETX)
      rx = Mid$(rx, 2) 'elimino anche il primo carattere (STX)
      If Not sta_tag.MAC_IN_MANUALE(1) And Not sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1) Then 'se non siamo in CO o Manuale
        Dim chk_OK As Boolean
        Dim str_chk As String
        Dim pos As Integer
        Dim i As Integer
        Dim dat_len_simon(1 To 3) As Integer
        Dim pos_in_list As Integer
        Dim count_liv As Integer
        Dim count_space As Integer
        Dim count_app As Integer
        Dim count_L As Integer
        pos_in_list = 0
        str_chk = ""
        chk_OK = True
        count_liv = 0
        'PRocchiccioli_FI_16/12/2010 Emulazione con protocollo taglierina SIMON
        Dim ck_pos As Integer
        Dim doubleL As Integer
        Dim str_doub_len As String
        Dim ck_doub_len As Integer
        Dim temp_doubl_len As Integer
        Dim Error_doubL As Boolean
        Dim Liv_in_error As Integer
        str_doub_len = ""
        ck_doub_len = 0
        temp_doubl_len = 0
        doubleL = 0
        Liv_in_error = 0
        Error_doubL = False
        
        For i = 1 To 3
            dat_len_simon(i) = 0
            pos = InStr(1, rx, "L" + Format(i) + "/")
            
            'PRocchiccioli_FI_16/12/2010 Emulazione con protocollo taglierina SIMON
            '***** controllo su doppio invio Ln e coerenza su lunghezza taglio *****
            ck_pos = pos
            str_doub_len = Mid(rx, ck_pos + 3) 'prendo la prima stringa con la lunghezza senza Ln/
            temp_doubl_len = get_cut_len_simon_from_string(str_doub_len) ' prendo la lunghezza e la metto da parte
            Do While ck_pos > 0
            ck_pos = InStr(ck_pos + 1, rx, "L" + Format(i) + "/")
                If ck_pos > 0 Then
                  doubleL = doubleL + 1 ' se trovo doppio Ln incremento
                  str_doub_len = Mid(rx, ck_pos + 3) 'prendo la successiva stringa con la lunghezza senza Ln/
                  ck_doub_len = get_cut_len_simon_from_string(str_doub_len) 'memorizzo lunghezza per confronto
                    If ck_doub_len <> temp_doubl_len Then
                      Error_doubL = True
                      Liv_in_error = i
                    Else
                      temp_doubl_len = ck_doub_len 'memorizzo per confronto successivo
                    End If
                End If
            Loop
            '*************************************************************************
            
            If pos > 0 Then
              count_liv = count_liv + 1
              str_chk = Mid(rx, pos + 3)
              dat_len_simon(i) = get_cut_len_simon_from_string(str_chk)
              'Se nella stringa ricevuta è stato trovato un livello non presente sulla macchina
              '(es. "L3" in caso di master solo 2 liv) rifiuto la stringa
              If i > par_mac.car_tec.num_liv_tag Then
                chk_OK = False
                Exit For
               End If
            Else
              dat_len_simon(i) = 0
            End If
        Next i
        count_space = 0
        count_app = -1
        id_space = 0
        Do While count_app > 0 Or count_app = -1 'Controllo quanti spazi sono presenti nella stringa
          count_app = InStr(IIf(count_app = -1, 1, count_app + 1), rx, " ")
          If count_app > 0 Then
            count_space = count_space + 1
            id_space = count_app
          End If
        Loop
        count_L = 0
        count_app = -1
        Do While count_app > 0 Or count_app = -1 'Controllo quanti L sono presenti nella stringa
          count_app = InStr(IIf(count_app = -1, 1, count_app + 1), rx, "L")
          If count_app > 0 Then
            count_L = count_L + 1
          End If
        Loop
        Dim cntr_space As Boolean, cntr_L As Boolean, cntr_lungh As Boolean
        'PRocchiccioli_FI_16/12/2010 Emulazione con protocollo taglierina SIMON
        'cntr_space = ((count_liv > 1 And id_space > 0 And id_space <> Len(rx) And count_space = count_liv - 1) Or (count_liv = 1 And count_space = 0))
        'cntr_L = (count_L = count_liv)
        'cntr_lungh = ((Len(rx) >= 7 * count_liv - 1) And (Len(rx) <= 8 * count_liv - 1))
        cntr_space = ((count_liv > 1 And id_space > 0 And id_space <> Len(rx) And count_space = doubleL + count_liv - 1) Or (count_liv = 1 And count_space = doubleL + count_liv - 1))
        cntr_L = (count_L = count_liv + doubleL)
        cntr_lungh = ((Len(rx) >= 7 * (count_liv + doubleL) - 1) And (Len(rx) <= 8 * (count_liv + doubleL) - 1))
        'PRocchiccioli_FI_16/12/2010 Emulazione con protocollo taglierina SIMON
        'If cntr_space And cntr_L And cntr_lungh Then
        If cntr_space And cntr_L And cntr_lungh And Not Error_doubL Then
          ord = rie_ord_simon(dat_len_simon, ord) 'richiamo e assegno la funzione che riempie la struttura dati da inviare a stag
          val_ord ord 'validazione ordine
        Else
          If (Not cntr_space) Then
            log_str "ric_dat_spde: ricevuti " + Format(count_space) + " spazi su ordine con " + Format(count_liv) + " livelli; ultimo spazio in pos " + Format(id_space) + ". Stringa NON corretta", MED_PRIORITY, log_spde
          ElseIf (Not cntr_L) Then
            log_str "ric_dat_spde: ricevuti " + Format(count_L) + " L su ordine con " + Format(count_liv) + " livelli. Stringa NON corretta", MED_PRIORITY, log_spde
          ElseIf (Not cntr_lungh) Then
          'PRocchiccioli_FI_16/12/2010 Emulazione con protocollo taglierina SIMON
            log_str "ric_dat_spde: ricevuti " + Format(Len(rx)) + " caratteri su ordine con " + Format(count_liv) + " livelli. Stringa NON corretta", MED_PRIORITY, log_spde
          ElseIf (Error_doubL) Then
            log_str "ric_dat_spde: ricevute due lunghezze di taglio diverse sul livello: " + Format(Liv_in_error), MED_PRIORITY, log_spde
          End If
          chk_OK = False
        End If
        
        If ord.ret = RET_OK And chk_OK = True Then
          Set temp = New ClasseOrdine
          pos_in_que = 2
          CopyType ordine, Space$(Len(ordine)), Len(ordine)
          ordine = ord.rec
          temp.ordine = Ord_str(ordine)
          'verifico se ci sono ordini,se non ci sono invio Ret_Bad(il primo ordine deve essere inserito manualmente come
          'da protocollo), se c'e' un ordine invio SEMPRE l'ordine alla seconda posizione(protocollo con DEPM), e nel caso
          'siano presenti piu ordini i lista modifico il secondo.
          If ord_mac.Count < 1 Then
            ret = RET_BAD
          ElseIf ord_mac.Count = 1 Then
            pos_in_list = 2
            ret = ord_mac.add(temp, pos_in_list, True)
          ElseIf ord_mac.Count > 1 Then
            pos_in_list = 2
            ret = ord_mac.Del_blk(pos_in_list, ord_mac.Count, False, True)
            If ret = RET_OK Then
              ret = ord_mac.add(temp, pos_in_list, True)
            End If
          End If
          If ret = RET_BAD Then 'raggiunto il numero massimo nella lista ordini MAX_NUM_ORD o posizione errata
            snd_err_msg_to_Depm tx, rx, ult_tx 'trasmetto un messaggio di errore a DEPM
            log_str "com_spde_bas - sub ric_dat_spde: Failed adding in order list pos ", MED_PRIORITY, log_spde
            Exit Sub
          Else
            tx = tx_to_spde("Y")  'Invio risposta Y - ordine ricevuto correttamente
            ult_tx = tx: tra_dat_spde ult_tx
            messaggio msg("ric_ord_spde") & " (" & pos_in_que & ") ", False, False
            'RFAZZI_FI 05-01-08
'                  log_str "com_spde_bas - sub ric_dat_spde: Successfully added record posizione " + CStr(pos_in_que) _
'                  + " SPDE_ID/MOD_ID " + ord.rec.spde_id + "/" + ord.rec.mod_id, MED_PRIORITY, log_spde
            log_str "com_spde_bas - sub ric_dat_spde: Successfully added record posizione " + CStr(pos_in_list) _
            + " SPDE_ID/MOD_ID " + ord.rec.spde_id + "/" + ord.rec.spde_ModID, MED_PRIORITY, log_spde
          End If
        Else
          'errore lunghezza di taglio
          snd_err_msg_to_Depm tx, rx, ult_tx 'trasmetto un messaggio di errore a DEPM
          If (ord.ret <> RET_OK) Then
            log_str "ric_dat_spde: ordine ricevuto NON valido. Codice di errore: " + Format(ord.ret), MED_PRIORITY, log_spde
          Else
            log_str "ric_dat_spde: ordine ricevuto NON formattato correttamente", MED_PRIORITY, log_spde
          End If
        Exit Sub
        End If
      Else
        'macchina in manuale o cambio ordine in corso
        tx = tx_to_spde("N")  'trasmetto un messaggio di errore a DEPM
        messaggio msg("rif_ord_spde"), True, False
        rx = ""
        ult_tx = tx: tra_dat_spde ult_tx
        If sta_tag.MAC_IN_MANUALE(1) Then
          log_str "com_spde_bas - sub ric_dat_spde: Invio/Modifica ordine rifiutato: Macchina in manuale", MED_PRIORITY, log_spde
        ElseIf sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1) Then
          log_str "com_spde_bas - sub ric_dat_spde: Invio/Modifica ordine rifiutato: Cambio ordine in corso", MED_PRIORITY, log_spde
        End If
        Exit Sub
      End If
    Else
      log_str "ric_dat_spde: carattere iniziale NON corretto!", MED_PRIORITY, log_spde
    End If
  Case Else
    log_str "ric_dat_spde: primo carattere NON corretto", MED_PRIORITY, log_spde
  End Select
'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
'fine
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

rx = ""  'svuoto la stringa dei caratteri ricevuti al termine della gestione
Else 'la stringa non contiene l'ETX => accodo i caratteri ricevuti ai precedenti
  rx = rx + r
End If

End Sub
Sub dig_ric_dat_spde(rx As String)
'gestione della comunicazione con il punto unico Fosber
Dim tx As String 'stringa per la memorizzazione dei messaggi in uscita
Dim bcc As String
Dim cmd As String
Dim fcs As String
Dim pos_in_que As Integer
Dim so_spde(1 To 3) As tip_set_ord_dig4
Dim so_spde_1L As tip_set_ord_dig4
Dim dis_sta As tip_dis_sta_dig4
Dim ordine As Tip_Ordine
Dim tmp_ord As ClasseOrdine
Dim ret As cod_ret
Dim sta_ord As String
Dim sta_dia As String
Dim num_liv_tag As Integer
Static num_liv As Integer
Static str As String
Static old_spde_id As String
Dim spde_id As String
Dim i As Integer
Dim app As String
Dim liv As String
Static old_liv As String
Dim s As Tip_Com_ord
Dim k As Integer
Dim for_rie(1 To 3) As Boolean
Dim Fine As Boolean

'************************************************************************************************************************
'*********************************      ROUTINE PROTOCOLLO DIGITEC      *********************************************
'************************************************************************************************************************

If Right$(rx, 1) = Chr$(ETX_PU) Then
  log_str "dig_ric_dat_spde: rx = " + rx, MED_PRIORITY, log_spde  'Arturo uff 12-11-02 (1) scrivo la stringa intera nel file di log
  rx = Left$(rx, Len(rx) - 1) 'tolgo l'ETX
  rx = Right$(rx, Len(rx) - 1) ' tolgo STX
  If Len(rx) <= DIG_COM_SPDE_LEN_LRC Then  ' ho ricevuto troppi pochi caratteri => c'è stato qualche problema
    tx = dig_mes_spde(NAK) 'preparazione messaggio di errore
    tra_dat_spde (tx) 'invio il messaggio
    log_str "dig_ric_dat_spde: Bad length received", MED_PRIORITY, log_spde
    str = ""  'Arturo uff 12-11-02 (2)
    Exit Sub
  End If
  num_liv_tag = par_mac.car_tec.num_liv_tag
  bcc = BCC_FCS(Chr$(STX_PU) + Left$(rx, Len(rx) - COM_SPDE_LEN_BCC))
  fcs = Right$(rx, COM_SPDE_LEN_BCC)
  If bcc <> fcs Then
      tx = dig_mes_spde(NAK) 'preparazione messaggio di errore
      tra_dat_spde (tx) 'invio il messaggio di errore
      'Arturo uff 12-11-02 (1)
      'log_str "dig_rig_dat_spde: checksum errato", MED_PRIORITY, log_spde    '29112001 Stagetti
      log_str "dig_rig_dat_spde: checksum errato. Ricevuto '" + fcs + "'; calcolato '" + bcc + "'", MED_PRIORITY, log_spde '29112001 Stagetti
  Else
      con_SPDE = GetTickCount()
      rx = Left$(rx, Len(rx) - COM_SPDE_LEN_BCC) ' tolgo il BCC
      cmd = Mid$(rx, DIG_COM_SPDE_LEN_DAT_LIB + 2, DIG_COM_SPDE_LEN_COD_COM)
      Select Case cmd
          '*******************************************************************************************************************
          Case SO 'codice comando Set Ordine
              If Not (sta_tag.MAC_IN_MANUALE(1)) And Not (sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1)) Then   'deve essere macchina in automatico e cambio ordine completato
                If Len(rx) = Len(so_spde_1L) Then   'deve essere giusta la lunghezza della stringa ricevuta
                  spde_id = Mid$(rx, DIG_COM_SPDE_LEN_DAT_LIB + 3, DIG_COM_SPDE_LEN_ID)
                  liv = Mid$(rx, DIG_COM_SPDE_LEN_DAT_LIB + 1, 1)
                  'andrea per obaly 15-02-06
                  If UCase(pro_set.per_cli) <> "MODEL_OBALY" Then
                    'Resa standard la seguente modifica.
                    'Inizialmente era stata introdotta per ESCADA sull'impianto di Linpac Atlanta, si rende tuttavia necessaria anche
                    'sullo standard (e quindi anche nella comunicazione con SPDE) visto che si sono presentati casi anomali di perdite di stringhe su rete e seriale nell'impianto
                    'di Arcore.
                    'Se arriva il livello 2 e non ho ancora ricevuto il livello 1 resetto tutte le variabili (per sicurezza) e rifiuto il formato
                    'Ad Atlanta è presente ESCADA che non legge lo stato e non considera gli ACK e NACK. Si sono inoltre verificate perdite di stringhe in ricezione sulla seriale
                    'ESCADA manda sempre il livello 1 e poi il livello 2 => se ricevo il livello 2 e non ho ricevuto il livello 1 => suppongo di aver perso tale ricezione
                    If num_liv = 0 And liv = "2" Then
                      'Ricezione del livello 2 e non ho ancora ricevuto il livello 1
                      old_spde_id = "": old_liv = ""
                      tx = dig_mes_spde(NAK) 'preparazione messaggio di errore
                      tra_dat_spde (tx) 'invio il messaggio di errore
                      log_str "dig_ric_dat_spde: è arrivato il livello 2 dell'ordine " + spde_id + " e non era ancora arrivato il livello 1", MED_PRIORITY, log_spde
                      log_str tx, MED_PRIORITY, log_spde
                      str = ""  'Arturo uff 12-11-02 (2)
                      Exit Sub
                    End If
                                      
                    If num_liv > 0 And spde_id <> old_spde_id Then
                      'Arrivata parte di un ordine da rifiutare
                      log_str "dig_ric_dat_spde - case so: è arrivata una parte di un ordine inatteso. SPDE ID aspettato = " + old_spde_id + " SPDE ID arrivato = " + spde_id, MED_PRIORITY, log_spde   '29112001 Stagetti
                      Exit Sub
                    End If
                  End If

                  If num_liv > 0 And liv = old_liv And spde_id = old_spde_id Then
                    'Arrivata parte di un ordine già ricevuta: mando comunque un ACK
                    tx = dig_mes_spde(ack) 'preparazione messaggio di conferma dell'inserimento ordine in lista avvenuto
                    tra_dat_spde (tx) 'invio il messaggio di ricezione corretta
                    log_str "dig_ric_dat_spde - case so: è arrivata una parte di un ordine già ricevuta.", MED_PRIORITY, log_spde   '29112001 Stagetti
                    log_str tx, MED_PRIORITY, log_spde
                    Exit Sub
                  End If
                  
                  'Ciò che è arrivato è potenzialmente giusto
                  If num_liv = 0 Then
                  
                    'Arrivata prima parte dell'ordine
                    old_spde_id = spde_id
                    old_liv = liv
                    tx = dig_mes_spde(ack) 'preparazione messaggio di conferma dell'inserimento ordine in lista avvenuto
                    tra_dat_spde (tx) 'invio il messaggio di ricezione corretta
                    str = str + rx
                    'Arturo uff 12-11-02 (1)
                    'log_str "dig_ric_dat_spde - case so: ricezione prima parte dell'ordine corretta. Livello " + CStr(liv) + ": " + rx, MED_PRIORITY, log_spde '29112001 Stagetti
                    log_str "dig_ric_dat_spde: ricezione prima parte dell'ordine. Livello " + CStr(liv), MED_PRIORITY, log_spde '29112001 Stagetti
                  Else
                    If spde_id = old_spde_id And old_liv <> liv Then
                      'Arrivata seconda parte dell'ordine
                      'Non mando ancora alcun ACK visto che non so ancora se l'ordine è da validare o meno
                      str = str + rx
                      'Arturo uff 12-11-02 (1)
                      'log_str "dig_ric_dat_spde - case so: ricezione seconda parte dell'ordine corretta. Livello " + CStr(liv) + ": " + rx, MED_PRIORITY, log_spde '29112001 Stagetti
                      log_str "dig_ric_dat_spde: ricezione seconda parte dell'ordine. Livello " + CStr(liv), MED_PRIORITY, log_spde '29112001 Stagetti
                    End If
                  End If
                    
                  If (num_liv > 0 And old_liv <> liv) Or num_liv = 0 Then num_liv = num_liv + 1
                    
                  If num_liv = num_liv_tag Then
                    num_liv = 0
                    old_spde_id = ""
                    old_liv = ""
                    'inizializzo la struttura dell'ordine
                    i = 1
                    k = 1
                    Fine = False
                    While k <= 3 And Not Fine
                      for_rie(k) = False
                      app = Mid$(str, 1 + (i - 1) * Len(so_spde_1L), Len(so_spde_1L))
                      If Trim$(app) <> "" Then
                        CopyType so_spde(i), app, Len(so_spde_1L)
                        If val(so_spde(i).lun_sca) > 300 Then ' formato del livello i da riempire visto che la lunghezza taglio non è = 300
                          dig_rie_ord ordine.ord(k), so_spde(i)
                          for_rie(k) = True
                          k = k + 1
                        End If
                      Else
                        Fine = True
                      End If
                      i = i + 1
                    Wend
                    For i = 1 To 3
                      If Not for_rie(i) Then
                        app = Space$(Len(ordine.ord(i)))
                        CopyType ordine.ord(i), app, Len(ordine.ord(i))
                      End If
                    Next i
                    str = ""
                    ordine.spde_id = so_spde(1).spde_id
                    ordine.cod_ord_abb = so_spde(1).spde_id 'DECISO CON PAOLO: mettere codice SPDE_ID nel codice abbinamento dell'ordine
                    ordine.pre_all = Right$(Trim$(so_spde(1).pre_all), 3)
                    ordine.tip_car.cod_com_car = so_spde(1).car
                    ordine.tip_car.Cod_ond = Left$(so_spde(1).ONDA, 3)
                    ordine.met_tot_ord = CStr(Max_num(val(ordine.ord(1).met), val(ordine.ord(2).met)))
                    ordine.met_tot_ord = CStr(Max_num(val(ordine.met_tot_ord), val(ordine.ord(3).met)))
                    If Not dig_con_agg_ond_car(ordine) Then  ' l'onda inviata è errata
                      tx = dig_mes_spde(NAK) 'preparazione messaggio di errore
                      tra_dat_spde (tx) 'invio il messaggio di errore
                      log_str tx, MED_PRIORITY, log_spde
                      log_str "com_spde_bas - sub dig_ric_dat_spde: onda errata", MED_PRIORITY, log_spde
                    Else
                      pos_in_que = val(so_spde(1).pos)
                      pos_in_que = pos_in_que + 1
                      If Not chk_pos(pos_in_que) Then    ' verifico l'esattezza della posizione nella lista
                        tx = dig_mes_spde(NAK) 'preparazione messaggio di errore
                        tra_dat_spde (tx) 'invio il messaggio di errore
                        log_str tx, MED_PRIORITY, log_spde
                        log_str "dig_ric_dat_spde: la posizione dell'ordine nella lista non è esatta " + CStr(pos_in_que), MED_PRIORITY, log_spde   '29112001 Stagetti
                      Else
                        s.rec = ordine
                        'VALIDAZIONE DELL'ORDINE RICEVUTO
                        val_ord s
                        If s.ret = RET_OK Then
                          Set tmp_ord = New ClasseOrdine
                          CopyType ordine, Space$(Len(ordine)), Len(ordine)
                          ordine = s.rec
                          tmp_ord.ordine = Ord_str(ordine)
                          If ord_mac.Count < pos_in_que Then 'verifico se l'ordine deve essere aggiunto alla lista o è una modifica di un ordine già esistente
                            ret = ord_mac.add(tmp_ord, , True)
                          Else
                            ret = ord_mac.Modifica(tmp_ord, pos_in_que, True)
                          End If
                          If ret = RET_BAD Then 'raggiunto il numero massimo nella lista ordini MAX_NUM_ORD o posizione errata
                            tx = dig_mes_spde(NAK) 'preparazione messaggio di errore
                            tra_dat_spde (tx) 'invio il messaggio di errore
                            log_str tx, MED_PRIORITY, log_spde
                            log_str "com_spde_bas - sub dig_ric_dat_spde: Failed adding in order list pos " + CStr(pos_in_que), MED_PRIORITY, log_spde
                            log_str "com_spde_bas - sub dig_ric_dat_spde: Failed adding in order list pos " + CStr(pos_in_que), MED_PRIORITY, log_spde
                          Else
                            tx = dig_mes_spde(ack) 'preparazione messaggio di conferma dell'inserimento ordine in lista avvenuto
                            tra_dat_spde (tx) 'invio il messaggio di ricezione corretta
                            log_str tx, MED_PRIORITY, log_spde
                            'RFAZZI_FI 05-01-08
                            'log_str "com_spde_bas - sub dig_ric_dat_spde: Successfully added record posizione " + CStr(pos_in_que) + " SPDE_ID/MOD_ID " + ordine.spde_id + "/" + ordine.mod_id, MED_PRIORITY, log_spde
                            log_str "com_spde_bas - sub dig_ric_dat_spde: Successfully added record posizione " + CStr(pos_in_que) + " SPDE_ID/MOD_ID " + ordine.spde_id + "/" + ordine.spde_ModID, MED_PRIORITY, log_spde
                          End If
                        Else
                          tx = dig_mes_spde(NAK) 'preparazione messaggio di errore
                          tra_dat_spde (tx) 'invio il messaggio di errore
                          log_str tx, MED_PRIORITY, log_spde
                          'Arturo uff 12-11-02 (1)
                          'log_str "Odine non validato. ID " + CStr(ordine.spde_id), MED_PRIORITY, log_spde
                          log_str "Odine non validato. ID " + CStr(ordine.spde_id) + ". s.ret = '" + CStr(s.ret) + "'", MED_PRIORITY, log_spde
                        End If
                      End If
                    End If
                  End If
                Else  ' lunghezza stringa errata
                  tx = dig_mes_spde(NAK) 'preparazione messaggio di errore
                  tra_dat_spde (tx) 'invio il messaggio di errore
                  log_str tx, MED_PRIORITY, log_spde
                  log_str "dig_ric_dat_spde - case so: lunghezza della stringa errata", MED_PRIORITY, log_spde    '29112001 Stagetti
                End If
              Else
                tx = dig_mes_spde(NAK) 'preparazione messaggio di errore
                tra_dat_spde (tx) 'invio il messaggio di errore
                log_str tx, MED_PRIORITY, log_spde
                log_str "dig_ric_dat_spde - case so: Master in AOC = " + IIf(sta_tag.MAC_IN_MANUALE(1) = True, "SI", "NO") + " o in Manuale = " + IIf(sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1) = True, "SI", "NO"), MED_PRIORITY, log_spde '29112001 Stagetti
                str = ""  'Arturo uff 12-11-02 (2)
              End If
          '*******************************************************************************************************************
          Case DSO 'codice comando Display Stato Ordine
              If Len(rx) = Len(dis_sta) Then   'deve essere giusta la lunghezza della stringa ricevuta
                  CopyType dis_sta, rx, Len(rx)
                  ret = dig_rie_sta_ord(sta_ord, val(dis_sta.liv))
                  If ret = RET_OK Then
                      tx = dig_mes_spde(sta_ord)
                      tra_dat_spde (tx)
                  Else
                      tx = dig_mes_spde(NAK) 'preparazione messaggio di errore IMPOSSIBILE LEGGERE LO STATO MACCHINA
                      tra_dat_spde (tx) 'invio il messaggio di errore
                      log_str "dig_ric_dat_spde - case dso: impossibile leggere lo stato della macchina", MED_PRIORITY, log_spde    '29112001 Stagetti
                  End If
              Else
                  tx = dig_mes_spde(NAK) 'preparazione messaggio di errore LA LUNGHEZZA DELLA STRINGA E' ERRATA
                  tra_dat_spde (tx) 'invio il messaggio di errore
                  log_str "dig_ric_dat_spde - case dso: la lunghezza della stringa è errata ", MED_PRIORITY, log_spde    '29112001 Stagetti
              End If
         
          '*******************************************************************************************************************
          Case DVO 'codice comando Display Vecchio Ordine
              If Len(rx) = Len(dis_sta) Then   'deve essere giusta la lunghezza della stringa ricevuta
                  CopyType dis_sta, rx, Len(rx)
                  ret = dig_rie_sta_vec_ord(sta_ord, val(dis_sta.liv))
                  If ret = RET_OK Then
                      tx = dig_mes_spde(sta_ord)
                      tra_dat_spde (tx)
                  Else
                      tx = dig_mes_spde(NAK) 'preparazione messaggio di errore IMPOSSIBILE LEGGERE LO STATO MACCHINA
                      tra_dat_spde (tx) 'invio il messaggio di errore
                      log_str "dig_ric_dat_spde - case dvo: impossibile leggere lo stato della macchina ", MED_PRIORITY, log_spde    '29112001 Stagetti
                  End If
              Else
                  tx = dig_mes_spde(NAK) 'preparazione messaggio di errore LA LUNGHEZZA DELLA STRINGA E' ERRATA
                  tra_dat_spde (tx) 'invio il messaggio di errore
                  log_str "dig_ric_dat_spde - case dvo: la lunghezza della stringa è errata", MED_PRIORITY, log_spde   '29112001 Stagetti
              End If
          '*******************************************************************************************************************
          Case DD 'codice comando Display Diagnostica
          If Len(rx) = Len(dis_sta) Then   'deve essere giusta la lunghezza della stringa ricevuta
                CopyType dis_sta, rx, Len(rx)
                ret = dig_rie_sta_dia(sta_dia, val(dis_sta.liv))
                If ret = RET_OK Then
                    tx = dig_mes_spde(sta_dia)
                    tra_dat_spde (tx)
                Else
                    tx = dig_mes_spde(NAK) 'preparazione messaggio di errore IMPOSSIBILE LEGGERE LO STATO MACCHINA
                    tra_dat_spde (tx) 'invio il messaggio di errore
                    log_str "dig_ric_dat_spde - case DD: impossibile leggere lo stato della macchina", MED_PRIORITY, log_spde    '29112001 Stagetti
                End If
          Else
                tx = dig_mes_spde(NAK) 'preparazione messaggio di errore LA LUNGHEZZA DELLA STRINGA E' ERRATA
                tra_dat_spde (tx) 'invio il messaggio di errore
                log_str "dig_ric_dat_spde - case DD: la lunghezza della stringa è errata", MED_PRIORITY, log_spde    '29112001 Stagetti
          End If
  
          '*******************************************************************************************************************
          Case MFP 'codice comando Modifica Fogli da Produrre
      
      End Select
  
  End If
End If
End Sub


Function chk_pos(ByVal pos As Integer) As Boolean
'Verifica se l'ordine inviato da SPDE può essere inserito in lista

If ord_mac.Count + 1 < pos Or pos <= 0 Or pos > MAX_NUM_ORD_IN_LIS Then ' non si accettano buchi nella lista
  chk_pos = False
  Exit Function
End If

chk_pos = True
End Function


Sub dig_rie_ord(ord As Tip_Formato, rec As tip_set_ord_dig4)
'riempie la struttura ord con i dati ricevuti da SPDE
Dim s As String
Dim i As Integer
Dim liv As Integer
Dim som As Double

liv = rec.liv
s = Space$(Len(ord))
CopyType ord, s, Len(s)
With ord
  .nom_cli = Trim$(rec.cli)
  .Ord_liv = rec.liv
  .com.cod_com_ord = Trim$(rec.com)
  If val(rec.lun_sca) > 9999 Then log_str "Ricevuto ordine con campo lunghezza > 4 caratteri", MED_PRIORITY, log_spde
  .sca_lun = CStr(val(Rie_nul(rec.lun_sca)) * TODECMM)
  .sca_lar = CStr(val(Rie_nul(rec.lar_sca)) * TODECMM)
  .sca_tot_num = CStr(val(rec.num_fog))
  .Sca_per_tag = Right$(rec.Sca_per_tag, 1)
  som = 0
  If val(.Sca_per_tag) > 0 Then
    som = (val(.sca_tot_num) / val(.Sca_per_tag)) * (val(rec.lun_sca) / 1000)
  End If
  If som <> Int(som) Then som = som + 1
  .met = CStr(Int(som))
End With

End Sub
Function dig_rie_sta_dia(s As String, liv As Integer) As cod_ret
'setta la stringa s con lo stato della produzione (ordine corrente + identificativo ordine successivo)
Dim loc_sta As tip_ris_dia_dig4
Dim app As String
Dim all(1 To 3) As Integer ' all(1) -> Err. QE potenza   all(2) -> Err. QE logica   all(3) -> Err. bordo macchina
Dim i As Integer
Dim lun_ID As Integer
Dim tmp As Tip_Ordine

If sta_tag.com_OK = False Then
    dig_rie_sta_dia = RET_BAD
Else
    s = Space$(Len(loc_sta))
    CopyType loc_sta, s, Len(loc_sta)
    lun_ID = Len(tmp.spde_id)
    If ord_mac.Count >= 1 Then
        loc_sta.ID_ord_cor = Rie_nul(ord_mac.item(1).spde_id)
        If ord_mac.Count > 1 Then
          loc_sta.ID_ord_suc = Rie_nul(ord_mac.item(2).spde_id)
        Else
          loc_sta.ID_ord_suc = Space$(lun_ID)
        End If
    Else
        loc_sta.ID_ord_cor = Space$(lun_ID)
        loc_sta.ID_ord_suc = Space$(lun_ID)
    End If
    
    For i = 1 To 3
        all(i) = 0
    Next i
    
    app = sta_tag.gru_all
    app = rev_str(app)
    If Mid$(app, POS_GRU_MAL, 1) = "1" Or Mid$(app, POS_GRU_HAR, 1) = "1" Then ' malfunzionamenti o hardware
      For i = 1 To 3
        all(i) = 1      ' metto tutti i gruppi allarme a 1
      Next i
    Else
      If Mid$(app, POS_GRU_QE_MST, 1) = "1" Or Mid$(app, POS_GRU_QE_SLV, 1) = "1" Or Mid$(app, POS_GRU_QE_EST, 1) = "1" Then   ' quadri elettrici
        all(1) = 1    ' QE potenza
        all(2) = 1    ' QE logica
      End If
      If Mid$(app, POS_GRU_CON, 1) = "1" Or Mid$(app, POS_GRU_BM, 1) = "1" Then  ' consolle o bordo macchina
        all(3) = 1    ' Bordo macchina
      End If
    End If
    
    app = String$(2, "0") ' 2 byte liberi nella stringa
    For i = 1 To 3
        app = app + CStr(all(i))
    Next i
    loc_sta.err_mac = app
    CopyString s, loc_sta, Len(loc_sta)
    dig_rie_sta_dia = RET_OK
End If
End Function

Function dig_rie_sta_ord(s As String, liv As Integer) As cod_ret
'setta la stringa s con lo stato della produzione (ordine corrente + identificativo ordine successivo)
Dim loc_sta As tip_ris_sta_ord_dig4
Dim ordine As Tip_Ordine

  If sta_tag.com_OK = False Then
    dig_rie_sta_ord = RET_BAD
  Else
    s = Space$(Len(loc_sta))
    CopyType loc_sta, s, Len(loc_sta)   ' inizializza a zero la variabile locale di stato produzione
    If ord_mac.Count >= 1 Then
        loc_sta.ID_ord_cor = ord_mac.item(1).spde_id
        If ord_mac.Count > 1 Then
              loc_sta.ID_ord_suc = ord_mac.item(2).spde_id
        Else
              loc_sta.ID_ord_suc = Space$(4)
        End If
        loc_sta.num_fog_tag = sta_tag.num_tag_eff(liv)
        loc_sta.num_fog_sca = sta_tag.ord_pro_num_fog_sca(liv)
        loc_sta.car = ord_mac.item(1).cod_com_car
        loc_sta.ONDA = ord_mac.item(1).Cod_ond
        loc_sta.pre_all = par_mac.car_tec.pre_all 'verificare se trasmettere il preallarme dell'ordine corrente o il preallarme impostato sulla macchina
        CopyType ordine, ord_mac.item(1).ordine, Len(ord_mac.item(1).ordine)
        loc_sta.nom_cli = ordine.ord(liv).nom_cli
        loc_sta.com = ordine.ord(liv).com.cod_com_ord
        loc_sta.Lun_tag = ordine.ord(liv).sca_lun
        loc_sta.lar_tag = ordine.ord(liv).sca_lar
        loc_sta.num_fog_imp = ordine.ord(liv).sca_tot_num
        loc_sta.num_fog_tag = ordine.ord(liv).Sca_per_tag
        CopyString s, loc_sta, Len(loc_sta)
        dig_rie_sta_ord = RET_OK
    Else
        loc_sta.ID_ord_cor = Space$(4)
        loc_sta.ID_ord_suc = Space$(4)
    End If
    
  End If

End Function

Function dig_rie_sta_vec_ord(s As String, liv As Integer) As cod_ret
'setta la stringa s con lo stato dell'ordine prodotto
'Dim loc_sta As tip_ris_sta_vec_ord_dig4
'Dim ordine As Tip_Ordine
'
'  If sta_tag.com_OK = False Then
'    dig_rie_sta_vec_ord = RET_BAD
'  Else
'    s = Space$(Len(loc_sta))
'    CopyType loc_sta, s, Len(loc_sta)   ' inizializza a zero la variabile locale di stato produzione
'    If ord_mac_ese.Count >= 1 Then
'        loc_sta.id_ord = ord_mac_ese.item(1).spde_id
'    Else
'        loc_sta.id_ord = Space$(4)
'    End If
End Function




Sub rie_ord_rec(ord As Tip_Com_ord, rec As tip_pro_ord_fosber20_FOS)
' riempie un ordine con la struttura che arriva da remoto
Dim i As Integer
Dim s As String
Dim som As Double

s = Space$(Len(ord.rec))
CopyType ord.rec, s, Len(s)
With ord.rec
  .spde_id = Left$(rec.spde_id_mod_id, Len(.spde_id))
  'RFAZZI_FI 05-01-08
  .spde_ModID = Right$(rec.spde_id_mod_id, Len(.mod_id))
  .mod_id = Right$(rec.spde_id_mod_id, Len(.mod_id))
  .alt_bob = rec.alt_bob_ord
  .cod_ord_abb = rec.cod_abb
  .tip_car.Alt_ond = rec.spess_car
  .tip_car.Cod_cod_ond = rec.cod_cod_onda
  .tip_car.cod_com_car = rec.comp_car
  .tip_car.Cod_ond = rec.cod_onda
  .tip_car.Pes_mt2 = rec.gramm_car
  .tip_car.Vel_aoc = rec.vel_cam_ord
  .tip_car.vel_pro = rec.vel_prod
  .met_tot_ord = rec.lun_tot_ord
  .pre_all = rec.preall_ord
  For i = 1 To 3
    Select Case UCase$(rec.for_ord(i).Livello) 'traduzione del livello di lavorazione dell'ordine
      Case "L":
        .ord(i).Ord_liv = Format$(LIV_BAS)
        If val(par_mac.car_tec.num_liv_tag) = 1 Or LIV_BAS = 0 Then log_str "com_spde_bas sub rie_ord_rec: ricevuto ordine con livello errato per un formato", MED_PRIORITY, log_spde
      Case "M":
        .ord(i).Ord_liv = Format$(LIV_MED)
        If val(par_mac.car_tec.num_liv_tag) <= 2 Or LIV_MED = 0 Then log_str "com_spde_bas sub rie_ord_rec: ricevuto ordine con livello errato per un formato", MED_PRIORITY, log_spde
      Case "U": .ord(i).Ord_liv = Format$(LIV_ALT)
    End Select
    'PMASSARO_FI05/09/11 - Aggiunto Trim sull'assegnazione della commessa ricevuta da SPDE o altro supervisore
    '.ord(i).com.cod_com_ord = rec.for_ord(i).comm_ord
    .ord(i).com.cod_com_ord = Trim(rec.for_ord(i).comm_ord)
    
    'ABorelli_FI19/03/2007_1 trim cutomer code received from SPDE
    '.ord(i).com.cod_cli = rec.for_ord(i).codi_cli
    .ord(i).com.cod_cli = Trim$(rec.for_ord(i).codi_cli)
    .ord(i).cut_to_mark = rec.for_ord(i).cut_to_mar
    som = val(rec.for_ord(i).met_for)
    If som <> Int(som) Then som = Int(som) + 1
    .ord(i).met = CStr(som)
    .ord(i).nom_cli = rec.for_ord(i).nome_cli
    .ord(i).sca_lar = rec.for_ord(i).lar_for
    .ord(i).sca_lun = rec.for_ord(i).lun_di_tag
    .ord(i).Sca_per_tag = rec.for_ord(i).num_sca_per_tag
    .ord(i).sca_tot_num = rec.for_ord(i).num_fogli
    'ripulisco i campi per il report produzione
    .rep_pro.liv(i).fog_buo = "0"
    .rep_pro.liv(i).fog_err = "0"
    .rep_pro.liv(i).fog_sca = "0"
    .rep_pro.met_tot = ""
    .rep_pro.vel_med = ""
    .rep_pro.num_fer = ""       ' ABorelli_FI09/09/2005_1

    .rep_pro.tem_fer = ""
    .rep_pro.tem_mar = ""
  Next i
End With

End Sub

Sub rie_ord_rec_21(ord As Tip_Com_ord, rec As tip_pro_ord_fosber21_FOS)
'Valorizza un ordine con la struttura che arriva da remoto
Dim i As Integer
Dim s As String
Dim som As Double
Dim met As Long

  s = Space$(Len(ord.rec))
  CopyType ord.rec, s, Len(s)
  With ord.rec
    .spde_id = Left$(rec.spde_id_mod_id, Len(.spde_id))
    'RFAZZI_FI 05-01-08
    .spde_ModID = Right$(rec.spde_id_mod_id, Len(.mod_id))
    .mod_id = Right$(rec.spde_id_mod_id, Len(.mod_id))
    .alt_bob = rec.alt_bob_ord
    .cod_ord_abb = rec.cod_abb
    .tip_car.Alt_ond = rec.spess_car
    .tip_car.Cod_cod_ond = rec.cod_cod_onda
    .tip_car.cod_com_car = rec.comp_car
    .tip_car.Cod_ond = rec.cod_onda
    .tip_car.Pes_mt2 = rec.gramm_car
    .tip_car.Vel_aoc = rec.vel_cam_ord
    .tip_car.vel_pro = rec.vel_prod
    .pre_all = rec.preall_ord
    met = 0
    For i = 1 To 3
      Select Case UCase$(rec.for_ord(i).Livello) 'traduzione del livello di lavorazione dell'ordine
        Case "L":
          .ord(i).Ord_liv = Format$(LIV_BAS)
          If val(par_mac.car_tec.num_liv_tag) = 1 Or LIV_BAS = 0 Then log_str "com_spde_bas sub rie_ord_rec: ricevuto ordine con livello errato per un formato", MED_PRIORITY, log_spde
        Case "M":
          .ord(i).Ord_liv = Format$(LIV_MED)
          If val(par_mac.car_tec.num_liv_tag) <= 2 Or LIV_MED = 0 Then log_str "com_spde_bas sub rie_ord_rec: ricevuto ordine con livello errato per un formato", MED_PRIORITY, log_spde
        Case "U": .ord(i).Ord_liv = Format$(LIV_ALT)
      End Select
      'PMASSARO_FI05/09/11 - Aggiunto Trim sull'assegnazione della commessa ricevuta da SPDE o altro supervisore
      '.ord(i).com.cod_com_ord = rec.for_ord(i).comm_ord
      .ord(i).com.cod_com_ord = Trim(rec.for_ord(i).comm_ord)
      
      
      'ABorelli_FI19/03/2007_1 trim cutomer code received from SPDE
      '.ord(i).com.cod_cli = rec.for_ord(i).codi_cli
      .ord(i).com.cod_cli = Trim$(rec.for_ord(i).codi_cli)
      'Dati Cut to registry
      .ord(i).cut_to_mark = rec.for_ord(i).cut_to_mar
      .ord(i).pat_cod = rec.for_ord(i).cut_pat.pat_cod
      .ord(i).pat_con = rec.for_ord(i).cut_pat.pat_con
      .ord(i).fst_FTC = rec.for_ord(i).cut_pat.fst_FTC
      .ord(i).Ofs_Cut = rec.for_ord(i).cut_pat.Ofs_Cut
      .ord(i).L1 = rec.for_ord(i).cut_pat.L1
      .ord(i).L2 = rec.for_ord(i).cut_pat.L2
      .ord(i).L3 = rec.for_ord(i).cut_pat.L3
      .ord(i).L4 = rec.for_ord(i).cut_pat.L4
      .ord(i).L5 = rec.for_ord(i).cut_pat.L5
      .ord(i).L6 = rec.for_ord(i).cut_pat.L6
      .ord(i).L7 = rec.for_ord(i).cut_pat.L7
      .ord(i).L8 = rec.for_ord(i).cut_pat.L8
      .ord(i).L9 = rec.for_ord(i).cut_pat.L9
      .ord(i).L10 = rec.for_ord(i).cut_pat.L10
      .ord(i).fin_pri = rec.for_ord(i).cut_pat.fin_pri
      .ord(i).fin_dop = rec.for_ord(i).cut_pat.fin_dop
      'RFAZZI_FI 05-01-08
      .ord(i).pos_pattern = 0
      som = 0
      If val(rec.for_ord(i).num_sca_per_tag) > 0 Then
        som = CLng((val(rec.for_ord(i).num_fogli) / val(rec.for_ord(i).num_sca_per_tag)) * val(rec.for_ord(i).lun_di_tag) / TODECMM) / 1000
      End If
      If som <> Int(som) Then som = Int(som) + 1
      .ord(i).met = CStr(som)
      .ord(i).nom_cli = rec.for_ord(i).nome_cli
      .ord(i).sca_lar = rec.for_ord(i).lar_for
      .ord(i).sca_lun = rec.for_ord(i).lun_di_tag
      'Icilio3545 03-01-2004
      .ord(i).Sca_per_tag = Format(val(rec.for_ord(i).num_sca_per_tag))
      .ord(i).sca_tot_num = rec.for_ord(i).num_fogli
      'ripulisco i campi per il report produzione
      .rep_pro.liv(i).fog_buo = "0"
      .rep_pro.liv(i).fog_err = "0"
      .rep_pro.liv(i).fog_sca = "0"
      .rep_pro.met_tot = ""
      .rep_pro.vel_med = ""
      .rep_pro.num_fer = "0"    ' ABorelli_FI09/09/2005_1
      .rep_pro.tem_fer = "0"
      .rep_pro.tem_mar = "0"
      'Calcolo i dati per l'ordine ricevuto
      If Trim$(.ord(i).Ord_liv) <> "" And val(.ord(i).Ord_liv) <> 0 Then
        If CLng(.ord(i).met) > met Then met = CLng(.ord(i).met)
      End If
    Next i
    .met_tot_ord = CStr(met)

End With

End Sub
Sub rie_ord_rec_30(ord As Tip_Com_ord, rec As tip_pro_ord_fosber30_FOS)
'Valorizza un ordine con la struttura che arriva da remoto
Dim i As Integer
Dim s As String
Dim som As Double
Dim met As Long

  s = Space$(Len(ord.rec))
  CopyType ord.rec, s, Len(s)
  With ord.rec
    .spde_id = Left$(rec.spde_id_mod_id, Len(.spde_id))
    'RFAZZI_FI 05-01-08
    .spde_ModID = Right$(rec.spde_id_mod_id, Len(.mod_id))
    .mod_id = Right$(rec.spde_id_mod_id, Len(.mod_id))
    .alt_bob = rec.alt_bob_ord
    .cod_ord_abb = rec.cod_abb
    .tip_car.Alt_ond = rec.spess_car
    .tip_car.Cod_cod_ond = rec.cod_cod_onda
    .tip_car.cod_com_car = rec.comp_car
    .tip_car.Cod_ond = rec.cod_onda
    .tip_car.Pes_mt2 = rec.gramm_car
    .tip_car.Vel_aoc = rec.vel_cam_ord
    .tip_car.vel_pro = rec.vel_prod
    .pre_all = rec.preall_ord
    met = 0
    For i = 1 To 3
      Select Case UCase$(rec.for_ord(i).Livello) 'traduzione del livello di lavorazione dell'ordine
        Case "L":
          .ord(i).Ord_liv = Format$(LIV_BAS)
          If val(par_mac.car_tec.num_liv_tag) = 1 Or LIV_BAS = 0 Then log_str "com_spde_bas sub rie_ord_rec: ricevuto ordine con livello errato per un formato", MED_PRIORITY, log_spde
        Case "M":
          .ord(i).Ord_liv = Format$(LIV_MED)
          If val(par_mac.car_tec.num_liv_tag) <= 2 Or LIV_MED = 0 Then log_str "com_spde_bas sub rie_ord_rec: ricevuto ordine con livello errato per un formato", MED_PRIORITY, log_spde
        Case "U": .ord(i).Ord_liv = Format$(LIV_ALT)
      End Select
      'PMASSARO_FI05/09/11 - Aggiunto Trim sull'assegnazione della commessa ricevuta da SPDE o altro supervisore
      '.ord(i).com.cod_com_ord = rec.for_ord(i).comm_ord
      .ord(i).com.cod_com_ord = Trim(rec.for_ord(i).comm_ord)
      
      .ord(i).com.cod_cli = Trim$(rec.for_ord(i).codi_cli)
      'Dati Cut to registry
      .ord(i).cut_to_mark = rec.for_ord(i).cut_to_mar
      .ord(i).pat_cod = rec.for_ord(i).cut_pat.pat_cod
      .ord(i).pat_con = rec.for_ord(i).cut_pat.pat_con
      .ord(i).fst_FTC = rec.for_ord(i).cut_pat.fst_FTC
      .ord(i).Ofs_Cut = rec.for_ord(i).cut_pat.Ofs_Cut
      .ord(i).L1 = rec.for_ord(i).cut_pat.L1
      .ord(i).L2 = rec.for_ord(i).cut_pat.L2
      .ord(i).L3 = rec.for_ord(i).cut_pat.L3
      .ord(i).L4 = rec.for_ord(i).cut_pat.L4
      .ord(i).L5 = rec.for_ord(i).cut_pat.L5
      .ord(i).L6 = rec.for_ord(i).cut_pat.L6
      .ord(i).L7 = rec.for_ord(i).cut_pat.L7
      .ord(i).L8 = rec.for_ord(i).cut_pat.L8
      .ord(i).L9 = rec.for_ord(i).cut_pat.L9
      .ord(i).L10 = rec.for_ord(i).cut_pat.L10
      .ord(i).fin_pri = rec.for_ord(i).cut_pat.fin_pri
      .ord(i).fin_dop = rec.for_ord(i).cut_pat.fin_dop
      .ord(i).pos_pattern = rec.for_ord(i).cut_pat.pos_pattern
      som = 0
      If val(rec.for_ord(i).num_sca_per_tag) > 0 Then
        som = CLng((val(rec.for_ord(i).num_fogli) / val(rec.for_ord(i).num_sca_per_tag)) * val(rec.for_ord(i).lun_di_tag) / TODECMM) / 1000
      End If
      If som <> Int(som) Then som = Int(som) + 1
      .ord(i).met = CStr(som)
      .ord(i).nom_cli = rec.for_ord(i).nome_cli
      .ord(i).sca_lar = rec.for_ord(i).lar_for
      .ord(i).sca_lun = rec.for_ord(i).lun_di_tag
      .ord(i).Sca_per_tag = Format(val(rec.for_ord(i).num_sca_per_tag))
      .ord(i).sca_tot_num = rec.for_ord(i).num_fogli
      'ripulisco i campi per il report produzione
      .rep_pro.liv(i).fog_buo = "0"
      .rep_pro.liv(i).fog_err = "0"
      .rep_pro.liv(i).fog_sca = "0"
      .rep_pro.met_tot = ""
      .rep_pro.vel_med = ""
      .rep_pro.num_fer = "0"
      .rep_pro.tem_fer = "0"
      .rep_pro.tem_mar = "0"
      
      'Calcolo i dati per l'ordine ricevuto
      If Trim$(.ord(i).Ord_liv) <> "" And val(.ord(i).Ord_liv) <> 0 Then
        If CLng(.ord(i).met) > met Then met = CLng(.ord(i).met)
      End If
    Next i
    .met_tot_ord = CStr(met)

End With

End Sub
Function rie_sta_rec(s As String) As cod_ret
'setta la stringa s con lo stato corrente di STAG per la trasmissione a SPDE
Dim loc_sta As tip_sta_master_FOS
Dim i As Integer
Dim num_ord As Integer
Dim str_sta_mac As String
Dim str_gru_all_att As String
Dim sta_mst As Tip_sta_mst_FOS
Dim gru_all_att As Tip_gru_all_att_FOS

' inizializza a Space$ le strutture utilizzate
s = Space$(Len(loc_sta.stag))
CopyType loc_sta.stag, s, Len(loc_sta.stag)
s = Space$(Len(sta_mst))
CopyType sta_mst, s, Len(sta_mst)
s = Space$(Len(gru_all_att))
CopyType gru_all_att, s, Len(gru_all_att)

sta_mst.MAC_IN_MANUALE = IIf(sta_tag.MAC_IN_MANUALE(1), STR_TRUE, STR_FALSE) 'MANUALE/AUTOMATICO
sta_mst.MAC_CO_IN_CORSO = IIf(sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1), STR_TRUE, STR_FALSE)  'CO in corso/CO completato
sta_mst.All_att = Right$(sta_tag.gru_all, 1) 'presenza di allarmi attivi
sta_mst.PLC_com = IIf(sta_tag.com_OK, STR_FALSE, STR_TRUE) 'comunicazione con il PLC

'comunicazioni con le schede di taglio e abilitazione livelli
For i = 1 To val(par_mac.car_tec.num_liv_tag)
  sta_mst.SCH_TAG_com(i) = IIf(Not com_ini_sch_tag(i), STR_TRUE, STR_FALSE)
  sta_mst.LIV_ABI(i) = IIf(par_abi.liv(i), STR_TRUE, STR_FALSE)
Next i
For i = val(par_mac.car_tec.num_liv_tag) + 1 To 3
  sta_mst.SCH_TAG_com(i) = STR_FALSE
  sta_mst.LIV_ABI(i) = STR_FALSE
Next i

num_ord = ord_mac.Count
'allineamento del corrente e del successivo con il PLC
If num_ord >= 1 Then
  sta_mst.Ord_cor_non_all = IIf(sta_tag.cod_ord_cor <> ord_mac.item(1).stag_id, STR_TRUE, STR_FALSE)
  If num_ord > 1 Then
    sta_mst.Ord_suc_non_all = IIf(sta_tag.cod_ord_suc <> ord_mac.item(2).stag_id, STR_TRUE, STR_FALSE)
  End If
End If

'consenti allineamento corrente e successivo al PUNTO UNICO
If sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1) Or sta_tag.MAC_IN_MANUALE(1) Then
  sta_mst.Con_all_cor = STR_FALSE
  sta_mst.Con_all_suc = STR_FALSE
Else
  sta_mst.Con_all_cor = STR_TRUE
  sta_mst.Con_all_suc = STR_TRUE
End If
sta_mst.lib1 = String$(Len(sta_mst.lib1), "0")

str_sta_mac = Space$(Len(sta_mst))
CopyString str_sta_mac, sta_mst, Len(sta_mst)
loc_sta.stag.Sta_mac = bin_hex(rev_str(str_sta_mac))

'sezione gruppi allarme attivi
gru_all_att.gru_HAR = IIf(sta_tag.gru_all_att(POS_GRU_HAR), STR_TRUE, STR_FALSE)
gru_all_att.gru_QE_MST = IIf(sta_tag.gru_all_att(POS_GRU_QE_MST), STR_TRUE, STR_FALSE)
gru_all_att.gru_QE_SLV = IIf(sta_tag.gru_all_att(POS_GRU_QE_SLV), STR_TRUE, STR_FALSE)
gru_all_att.gru_QE_EST = IIf(sta_tag.gru_all_att(POS_GRU_QE_EST), STR_TRUE, STR_FALSE)
gru_all_att.gru_BM = IIf(sta_tag.gru_all_att(POS_GRU_BM), STR_TRUE, STR_FALSE)
gru_all_att.gru_CON = IIf(sta_tag.gru_all_att(POS_GRU_CON), STR_TRUE, STR_FALSE)
gru_all_att.gru_MAL = IIf(sta_tag.gru_all_att(POS_GRU_MAL), STR_TRUE, STR_FALSE)



gru_all_att.lib3 = String$(Len(gru_all_att.lib3), "0")

str_gru_all_att = Space$(Len(gru_all_att))
CopyString str_gru_all_att, gru_all_att, Len(gru_all_att)
loc_sta.stag.gru_all_att = bin_hex(rev_str(str_gru_all_att))

'sezione validazione ordini
If num_ord > MAX_NUM_ORD_IN_LIS Then
  log_str "com_spde_bas sub rie_sta_rec: Figurano più di 5 ordini in lista", MED_PRIORITY, log_spde
  num_ord = MAX_NUM_ORD_IN_LIS
End If
For i = 1 To num_ord
  loc_sta.stag.ord_val(i).val = STR_TRUE  'ordine valido
  'RFAZZI_FI 05-01-08
  'Non ritorno indietro il modID per permettere una modifica locale dell'ordine
  'loc_sta.stag.ord_val(i).ID_ord = Trim$(ord_mac.item(i).spde_id) & Trim$(ord_mac.item(i).mod_id)
  loc_sta.stag.ord_val(i).ID_ord = Trim$(ord_mac.item(i).spde_id) & Trim$(ord_mac.item(i).spde_ModID)
Next i
For i = num_ord + 1 To MAX_NUM_ORD_IN_LIS
  loc_sta.stag.ord_val(i).val = "2" 'ordine non presente nella lista
  'il campo ID degli ordini non presenti è già inizializzato a Space$
Next i

s = Space$(Len(loc_sta.stag))
CopyString s, loc_sta.stag, Len(loc_sta.stag)

rie_sta_rec = RET_OK

End Function
Function rie_sta_rec_30(s As String) As cod_ret
'setta la stringa s con lo stato corrente di STAG per la trasmissione a SPDE
Dim loc_sta As tip_sta_master_FOS_30
Dim i As Integer
Dim num_ord As Integer
Dim str_sta_mac As String
Dim str_gru_all_att As String
Dim sta_mst As Tip_sta_mst_FOS
Dim gru_all_att As Tip_gru_all_att_FOS
Dim ord_ese As tip_pro_ord_ese_fosber30_FOS
Dim ord As Tip_Ordine
Const REP_RDY = 0


' inizializza a Space$ le strutture utilizzate
s = Space$(Len(loc_sta.stag))
CopyType loc_sta.stag, s, Len(loc_sta.stag)
s = Space$(Len(sta_mst))
CopyType sta_mst, s, Len(sta_mst)
s = Space$(Len(gru_all_att))
CopyType gru_all_att, s, Len(gru_all_att)

sta_mst.MAC_IN_MANUALE = IIf(sta_tag.MAC_IN_MANUALE(1), STR_TRUE, STR_FALSE) 'MANUALE/AUTOMATICO
sta_mst.MAC_CO_IN_CORSO = IIf(sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1), STR_TRUE, STR_FALSE)  'CO in corso/CO completato
sta_mst.All_att = Right$(sta_tag.gru_all, 1) 'presenza di allarmi attivi
sta_mst.PLC_com = IIf(sta_tag.com_OK, STR_FALSE, STR_TRUE) 'comunicazione con il PLC

'comunicazioni con le schede di taglio e abilitazione livelli
For i = 1 To val(par_mac.car_tec.num_liv_tag)
  sta_mst.SCH_TAG_com(i) = IIf(Not com_ini_sch_tag(i), STR_TRUE, STR_FALSE)
  sta_mst.LIV_ABI(i) = IIf(par_abi.liv(i), STR_TRUE, STR_FALSE)
Next i
For i = val(par_mac.car_tec.num_liv_tag) + 1 To 3
  sta_mst.SCH_TAG_com(i) = STR_FALSE
  sta_mst.LIV_ABI(i) = STR_FALSE
Next i

num_ord = ord_mac.Count
'allineamento del corrente e del successivo con il PLC
If num_ord >= 1 Then
  sta_mst.Ord_cor_non_all = IIf(sta_tag.cod_ord_cor <> ord_mac.item(1).stag_id, STR_TRUE, STR_FALSE)
  If num_ord > 1 Then
    sta_mst.Ord_suc_non_all = IIf(sta_tag.cod_ord_suc <> ord_mac.item(2).stag_id, STR_TRUE, STR_FALSE)
  End If
End If

'consenti allineamento corrente e successivo al PUNTO UNICO
If sta_tag.MAC_CAMBIO_ORDINE_IN_CORSO(1) Or sta_tag.MAC_IN_MANUALE(1) Then
  sta_mst.Con_all_cor = STR_FALSE
  sta_mst.Con_all_suc = STR_FALSE
Else
  sta_mst.Con_all_cor = STR_TRUE
  sta_mst.Con_all_suc = STR_TRUE
End If
sta_mst.lib1 = String$(Len(sta_mst.lib1), "0")

str_sta_mac = Space$(Len(sta_mst))
CopyString str_sta_mac, sta_mst, Len(sta_mst)
loc_sta.stag.Sta_mac = bin_hex(rev_str(str_sta_mac))

'sezione gruppi allarme attivi
gru_all_att.gru_HAR = IIf(sta_tag.gru_all_att(POS_GRU_HAR), STR_TRUE, STR_FALSE)
gru_all_att.gru_QE_MST = IIf(sta_tag.gru_all_att(POS_GRU_QE_MST), STR_TRUE, STR_FALSE)
gru_all_att.gru_QE_SLV = IIf(sta_tag.gru_all_att(POS_GRU_QE_SLV), STR_TRUE, STR_FALSE)
gru_all_att.gru_QE_EST = IIf(sta_tag.gru_all_att(POS_GRU_QE_EST), STR_TRUE, STR_FALSE)
gru_all_att.gru_BM = IIf(sta_tag.gru_all_att(POS_GRU_BM), STR_TRUE, STR_FALSE)
gru_all_att.gru_CON = IIf(sta_tag.gru_all_att(POS_GRU_CON), STR_TRUE, STR_FALSE)
gru_all_att.gru_MAL = IIf(sta_tag.gru_all_att(POS_GRU_MAL), STR_TRUE, STR_FALSE)



gru_all_att.lib3 = String$(Len(gru_all_att.lib3), "0")

str_gru_all_att = Space$(Len(gru_all_att))
CopyString str_gru_all_att, gru_all_att, Len(gru_all_att)
loc_sta.stag.gru_all_att = bin_hex(rev_str(str_gru_all_att))

'sezione validazione ordini
If num_ord > MAX_NUM_ORD_IN_LIS Then
  log_str "com_spde_bas sub rie_sta_rec: Figurano più di 5 ordini in lista", MED_PRIORITY, log_spde
  num_ord = MAX_NUM_ORD_IN_LIS
End If
For i = 1 To num_ord
  loc_sta.stag.ord_val(i).val = STR_TRUE  'ordine valido
  'RFAZZI_FI 05-01-08
  'Non ritorno indietro il modID per permettere una modifica locale dell'ordine
  'loc_sta.stag.ord_val(i).ID_ord = Trim$(ord_mac.item(i).spde_id) & Trim$(ord_mac.item(i).mod_id)
  loc_sta.stag.ord_val(i).ID_ord = Trim$(ord_mac.item(i).spde_id) & Trim$(ord_mac.item(i).spde_ModID)
Next i
For i = num_ord + 1 To MAX_NUM_ORD_IN_LIS
  loc_sta.stag.ord_val(i).val = "2" 'ordine non presente nella lista
  'il campo ID degli ordini non presenti è già inizializzato a Space$
Next i

'valorizzo past order
s = Space$(Len(ord_ese))
CopyType ord_ese, s, Len(ord_ese)
CopyType ord, ord_mac_ese.item(1).ordine, Len(ord_mac_ese.item(1).ordine)

'RFAZZI_FI 05-01-08
'loc_sta.stag.ord_val(MAX_NUM_ORD_IN_LIS + 1).ID_ord = Rie_nul(ord.spde_id & ord.mod_id)
loc_sta.stag.ord_val(MAX_NUM_ORD_IN_LIS + 1).ID_ord = Rie_nul(ord.spde_id & ord.spde_ModID)
loc_sta.stag.ord_val(MAX_NUM_ORD_IN_LIS + 1).val = IIf(val(ord.rep_pro.met_tot) <> 0, 2 ^ REP_RDY, 0)



s = Space$(Len(loc_sta.stag))
CopyString s, loc_sta.stag, Len(loc_sta.stag)

rie_sta_rec_30 = RET_OK

End Function

Function snd_err_msg_to_Depm(tx_depm As String, rx_depm As String, ult_tx_depm As String)
  tx_depm = tx_to_spde("N")  'trasmetto un messaggio di errore a DEPM
  messaggio msg("rif_ord_spde"), True, False
  rx_depm = ""
  ult_tx_depm = tx_depm: tra_dat_spde ult_tx_depm
End Function

Sub tra_dat_spde(msg As String)
'invio il messaggio a spde su rete o seriale
Static col As Integer
Dim tmp As Long

'log_str "tra_dat_spde", 5, log_spde
If par_mac.com_spde.tip_com = 0 Then
  'comunicazione seriale
  par_mac.com_spde.con.Output = msg
ElseIf par_mac.com_spde.tip_com = 1 Then
  'comunicazione su rete
  par_mac.com_spde.con.SendData "" + msg
  log_str "Inviato: " + msg, 5, log_spde
End If
If col = 0 Then
  tmp = sta_bar.led_spde_rx.FillColor
  sta_bar.led_spde_rx.FillColor = sta_bar.led_spde_tx.FillColor
  sta_bar.led_spde_tx.FillColor = tmp
  col = 1
Else
  col = 0
End If
'If col = 0 Then
'  sta_bar.led_spde_tx.FillColor = COL_DARK_Green
'  col = 1
'Else
'  sta_bar.led_spde_tx.FillColor = vbGreen
'  col = 0
'End If
'sta_bar.led_spde_rx.FillColor = vbRed
End Sub
Public Function str_ric_OK(rx As String) As Boolean
Dim i As Integer
Dim car_err As Boolean
Dim len_err As Boolean

  car_err = False
  Select Case par_mac.com_spde.ver_spde
    'Case Fosber10, Fosber11
    Case Fosber10 '30112001 Stagetti tolto protocollo fosber 1.1
      len_err = IIf(Len(rx) > DIG_COM_SPDE_LEN_LRC, False, True)
    Case Fosber20, Fosber21
      len_err = IIf(Len(rx) >= COM_SPDE_LEN_MIN, False, True)
    Case Simon10 'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
      len_err = IIf(Len(rx) >= COM_DEPM_LEN_MIN_SIMON, False, True)
    End Select
  str_ric_OK = (Not car_err And Not len_err)
  
End Function

Function dig_mes_spde(msg As String) As String
If msg = NAK Then msg = msg + Space(5)
dig_mes_spde = Chr$(STX_PU) + msg + BCC_FCS(Chr$(STX_PU) + msg) + Chr$(ETX_PU)
End Function
 
Function tx_to_spde(msg As String) As String
'tx_to_spde = Chr$(STX_TAG) + msg + LRC_FCS(msg) + Chr$(ETX_TAG)  'stringa completa con i caratteri di inizio e fine e con i controlli
'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
'prepara la stringa da trasmettere per spedire il messaggio msg
If Not (par_mac.com_spde.ver_spde = Simon10) Then
  tx_to_spde = Chr$(STX_TAG) + msg + LRC_FCS(msg) + Chr$(ETX_TAG)  'stringa completa con i caratteri di inizio e fine e con i controlli
ElseIf (par_mac.com_spde.ver_spde = Simon10) Then
  tx_to_spde = msg
End If
End Function
Function rie_ord_ese_rec(s As String) As cod_ret  ' ABorelli_FI09/09/2005_1
Dim ord_ese As tip_pro_ord_ese_fosber21_FOS
Dim ord As Tip_Ordine
Dim i As Integer
Dim lar As Integer
Dim st As String
    ' inizializza a Space$ le strutture utilizzate
    s = Space$(Len(ord_ese))
    CopyType ord_ese, s, Len(ord_ese)
    CopyType ord, ord_mac_ese.item(indice:=1).ordine, Len(ord_mac_ese.item(indice:=1).ordine)
    ' calcolo larghezza ordine
    lar = 0
    For i = 1 To 3
        lar = lar + val(ord.ord(i).sca_lar) * val(ord.ord(i).Sca_per_tag) / 10
    Next i
    ord_ese.cod_abb = Rie_nul(ord.cod_ord_abb)
    
    ord_ese.spde_id_mod_id = Rie_nul(ord.spde_id & ord.mod_id)
    st = Rie_nul(ord.rep_pro.dat_ini)
    If Trim$(st) <> "" Then
        ord_ese.dat_ini = "20" + Mid$(st, 1, 2) + Mid$(st, 4, 2) + Mid$(st, 7, 2)
    Else
        ord_ese.dat_ini = st
    End If
    ord_ese.met_lin = Rie_nul(ord.rep_pro.met_tot)
    ord_ese.met_lin = Format$(val(ord_ese.met_lin) \ 1) ' si arrotondano
    ord_ese.met_qua = Format$(val(ord_ese.met_lin) * lar \ 1000)
    st = Rie_nul(ord.rep_pro.ora_ini)
    If Trim$(st) <> "" Then
        ord_ese.ora_ini = Mid$(st, 1, 2) + Mid$(st, 4, 2) + Mid$(st, 7, 2)
    Else
        ord_ese.ora_ini = st
    End If
    st = Rie_nul(ord.rep_pro.ora_fin)
    If Trim$(st) <> "" Then
        ord_ese.ora_fin = Mid$(st, 1, 2) + Mid$(st, 4, 2) + Mid$(st, 7, 2)
    Else
        ord_ese.ora_fin = st
    End If
    'st = DateDiff("s", Trim$(ORD.rep_pro.dat_ini) + " " + Trim$(ORD.rep_pro.ora_ini), Trim$(ORD.rep_pro.dat_fin) + " " + Trim$(ORD.rep_pro.ora_fin))
    'st = Format$(val(st) \ 60&)
    'ord_ese.tem_mar = Rie_nul(st)
    
    ord_ese.tem_fer = Rie_nul(Format$(val(ord.rep_pro.tem_fer) \ 60))
    ord_ese.tem_mar = Rie_nul(Format$(val(ord.rep_pro.tem_mar) \ 60))
    ord_ese.num_fer = Rie_nul(ord.rep_pro.num_fer)
    ord_ese.vel_med = Rie_nul(ord.rep_pro.vel_med)
    For i = 1 To 3
        ord_ese.for_ord(i).fog_no = Rie_nul(ord.rep_pro.liv(i).fog_err)
        ord_ese.for_ord(i).fog_ok = Rie_nul(ord.rep_pro.liv(i).fog_buo)
        ord_ese.for_ord(i).liv = Rie_nul(ord.ord(i).Ord_liv)
        Select Case ord_ese.for_ord(i).liv
            Case "1"
                ord_ese.for_ord(i).liv = "L"
            Case "2"
                If par_mac.car_tec.num_liv_tag = 2 Then
                    ord_ese.for_ord(i).liv = "U"
                Else
                    ord_ese.for_ord(i).liv = "M"
                End If
            Case "3"
                ord_ese.for_ord(i).liv = "U"
        End Select
    Next i
    CopyString s, ord_ese, Len(ord_ese)
End Function
Function rie_ord_ese_rec_30(s As String) As cod_ret  'Robertouff 26-11-2007
Dim ord_ese As tip_pro_ord_ese_fosber30_FOS
Dim ord As Tip_Ordine
Dim i As Integer
Dim lar As Integer
Dim st As String
    ' inizializza a Space$ le strutture utilizzate
    s = Space$(Len(ord_ese))
    CopyType ord_ese, s, Len(ord_ese)
    CopyType ord, ord_mac_ese.item(indice:=1).ordine, Len(ord_mac_ese.item(indice:=1).ordine)
    ' calcolo larghezza ordine
    lar = 0
    For i = 1 To 3
        lar = lar + val(ord.ord(i).sca_lar) * val(ord.ord(i).Sca_per_tag) / 10
    Next i
    ord_ese.cod_abb = Rie_nul(ord.cod_ord_abb)
    
    'RFAZZI_FI 05-01-08
    'ord_ese.spde_id_mod_id = Rie_nul(ord.spde_id & ord.mod_id)
    ord_ese.spde_id_mod_id = Rie_nul(ord.spde_id & ord.spde_ModID)
    st = Rie_nul(ord.rep_pro.dat_ini)
    If Trim$(st) <> "" Then
        ord_ese.dat_ini = "20" + Mid$(st, 1, 2) + Mid$(st, 4, 2) + Mid$(st, 7, 2)
    Else
        ord_ese.dat_ini = st
    End If
    ord_ese.met_lin = Rie_nul(ord.rep_pro.met_tot)
    ord_ese.met_lin = Format$(val(ord_ese.met_lin) \ 1) ' si arrotondano
    ord_ese.met_qua = Format$(val(ord_ese.met_lin) * lar \ 1000)
    st = Rie_nul(ord.rep_pro.ora_ini)
    If Trim$(st) <> "" Then
        ord_ese.ora_ini = Mid$(st, 1, 2) + Mid$(st, 4, 2) + Mid$(st, 7, 2)
    Else
        ord_ese.ora_ini = st
    End If
    st = Rie_nul(ord.rep_pro.ora_fin)
    If Trim$(st) <> "" Then
        ord_ese.ora_fin = Mid$(st, 1, 2) + Mid$(st, 4, 2) + Mid$(st, 7, 2)
    Else
        ord_ese.ora_fin = st
    End If
    
    ord_ese.tem_fer = Rie_nul(Format$(val(ord.rep_pro.tem_fer) \ 60))
    ord_ese.tem_mar = Rie_nul(Format$(val(ord.rep_pro.tem_mar) \ 60))
    ord_ese.num_fer = Rie_nul(ord.rep_pro.num_fer)
    ord_ese.vel_med = Rie_nul(ord.rep_pro.vel_med)
    For i = 1 To 3
        ord_ese.for_ord(i).fog_no = Rie_nul(ord.rep_pro.liv(i).fog_err)
        ord_ese.for_ord(i).fog_ok = Rie_nul(ord.rep_pro.liv(i).fog_buo)
        ord_ese.for_ord(i).liv = Rie_nul(ord.ord(i).Ord_liv)
        ord_ese.for_ord(i).mm_scarto = Rie_nul(ord.rep_pro.liv(i).mil_sca)
        Select Case ord_ese.for_ord(i).liv
            Case "1"
                ord_ese.for_ord(i).liv = "L"
            Case "2"
                If par_mac.car_tec.num_liv_tag = 2 Then
                    ord_ese.for_ord(i).liv = "U"
                Else
                    ord_ese.for_ord(i).liv = "M"
                End If
            Case "3"
                ord_ese.for_ord(i).liv = "U"
        End Select
    Next i
    CopyString s, ord_ese, Len(ord_ese)
End Function
Function rie_tur_gio_rec(s As String) As cod_ret        ' ABorelli_FI09/09/2005_1

Dim tur_gio As tip_pro_tur_gio_fosber21_FOS
    ' inizializza a Space$ le strutture utilizzate
    s = Space$(Len(tur_gio))
    CopyType tur_gio, s, Len(tur_gio)
    'tur_gio.gio
    'tur_gio.tur (i)
    
    
    CopyString s, tur_gio, Len(tur_gio)
End Function

Function for_dat(s As String) As String
' formatta le date      ABorelli_FI09/09/2005_1

    If Trim$(s) = "" Then
        for_dat = ""
    Else
        for_dat = Mid$(s, 7, 2) + "." + Mid$(s, 5, 2) + "." + Mid$(s, 1, 4)
    End If
End Function
Function for_val(s As String) As String
' formatta valori numerici              ABorelli_FI09/09/2005_1

    If Trim$(s) = "" Then
        for_val = "0"
    Else
        for_val = Trim$(s)
    End If
End Function
Function for_ora(s As String) As String
' formatta le ore               ABorelli_FI09/09/2005_1

    If Trim$(s) = "" Then
        for_ora = ""
    Else
        for_ora = Mid$(s, 1, 2) + "." + Mid$(s, 3, 2) + "." + Mid$(s, 5, 2)
    End If
End Function
Function for_met(s As String) As String
' formatta i metri (o feet)              ABorelli_FI09/09/2005_1
    If abi_mis_ang Then
       for_met = con_met_to_fet(s)
    Else
        If Trim$(s) = "" Then
            for_met = "0"
        Else
            for_met = Trim$(s)
        End If
    End If
End Function

Function for_met_qua(s As String) As String
' formatta i metri quadri (o square feet)               ABorelli_FI09/09/2005_1
    If abi_mis_ang Then
       for_met_qua = con_met_qua_to_squ_fet(s)
    Else
        If Trim$(s) = "" Then
            for_met_qua = "0"
        Else
            for_met_qua = Trim$(s)
        End If
    End If
End Function


Function sec_ore(st As String) As String
' converte da numero di secondi in ore:minuti:secondi           ABorelli_FI09/09/2005_1

Dim v As Long, s As Long, m As Long, o As Long
    v = val(st)
    s = v Mod 60
    m = ((v - s) / 60) Mod 60
    o = (v - s - m * 60) / 3600
    sec_ore = Format$(o, "00") + "." + Format$(m, "00") + "." + Format$(s, "00")
    Debug.Print sec_ore
End Function
