Attribute VB_Name = "lingue_stag"
Option Explicit
'Costante per la lunghezza del campo dei messaggi
Public mes_dbf_non_pre As Boolean     'True se non è presente messaggi.dbf al momento dell'avvio di Pmac o Spde
Public mg As Collection
Sub chg_lin(lin_att As Integer)
Dim f As Form        'per il ciclo sulle form
Dim c As Control     'per il ciclo sui controlli di ogni form
Dim ds As Recordset  'per la gestione dell'oggeto DataBase
Dim i As Integer

Set ds = db.OpenRecordset("select * from messaggi", dbOpenDynaset)
If ds.RecordCount = 0 Then
  ds.Close
  Exit Sub
End If
ds.MoveFirst
For Each f In Forms
  For Each c In f.Controls
    If tra_ctrl(f, c) Then
        If ds.EOF = False Then
          If TypeOf c Is Label Then
            c.Caption = Format$(ds(lin_att + 3))
          ElseIf TypeOf c Is Frame Then
            c.Caption = Format$(ds(lin_att + 3))
          ElseIf TypeOf c Is CommandButton Then
            c.Caption = Format$(ds(lin_att + 3))
          ElseIf TypeOf c Is OptionButton Then
            c.Caption = Format$(ds(lin_att + 3))
          End If
          'settaggio del tipo di carattere letto da SPDE.ini
          'c.Font.Charset = pro_set.Cha_set
          ds.MoveNext
        End If
    End If
  Next c
Next f
'******  impostazione messaggi
For i = 1 To mg.Count
    If ds.EOF = False Then
      mg(i).item = Format$(ds(lin_att + 3))
      ds.MoveNext
    End If
Next i
ds.Close
End Sub



Sub set_all()
Dim i As Integer, j As Integer, h As Integer
Dim det_all_tmp As String, all_tmp As String
Dim mytb As Recordset

'Descrizione in breve tasto allarme
If Dir$(pro_set.pro_pat + "messaggi.dbf") <> "" Then
  If Dir$(pro_set.pro_pat + "tmp_ind.ndx") <> "" Then
     Kill pro_set.pro_pat + "tmp_ind.ndx"
  End If
  If Dir$(pro_set.pro_pat + "messaggi.inf") <> "" Then
     Kill pro_set.pro_pat + "messaggi.inf"
  End If
  db.Execute "create index tmp_ind on messaggi(controllo)"
  Set mytb = db.OpenRecordset("messaggi", dbOpenTable)
  mytb.Index = "tmp_ind"
   For h = 1 To NUM_GRU_ALL
     For i = 0 To NUM_DAT_MEM_ALL - 1
       For j = 0 To 15
         all_tmp = "all_" + des_pag_all(Format$(h)) + "_" + Format$(i) + Format$(j, "00")
         mytb.Seek "=", all_tmp
         If mytb.NoMatch Then
           msgadd item:="Allarme N. " + Format$(i) + Format$(j, "00"), key:=all_tmp: inc_prg_bar_ini 'Arturo uff 25-07-02
         Else
           msgadd item:=Format$(mytb("italian")), key:=all_tmp: inc_prg_bar_ini 'Arturo uff 25-07-02
         End If
       Next j
     Next i
   Next h
 'Descrizione completa tasto allarme
   For h = 1 To NUM_GRU_ALL
     For i = 0 To NUM_DAT_MEM_ALL - 1
       For j = 0 To 15
         det_all_tmp = "det_all_" + des_pag_all(Format$(h)) + "_" + Format$(i) + Format$(j, "00")
         mytb.Seek "=", det_all_tmp
         If mytb.NoMatch Then
           msgadd item:="Dettagli su allarme N. " + Format$(i) + Format$(j, "00"), key:=det_all_tmp: inc_prg_bar_ini 'Arturo uff 25-07-02
         Else
           msgadd item:=Format$(mytb("italian")), key:=det_all_tmp: inc_prg_bar_ini 'Arturo uff 25-07-02
         End If
       Next j
     Next i
   Next h
   mytb.Close
   If Dir$(pro_set.pro_pat + "messaggi.inf") <> "" Then
      Kill pro_set.pro_pat + "messaggi.inf"
   End If
    If Dir$(pro_set.pro_pat + "tmp_ind.ndx") <> "" Then
      Kill pro_set.pro_pat + "tmp_ind.ndx"
   End If
 'Descrizione in breve azionamenti Lenz
   'APalmerini_FI02/12/2004_1
   'For i = 1 To NUM_AZI  'si è deciso di porre 10 come numero massimo di azionamenti lenze (6 per i motori + 2 per i sendwich di ingresso e uscita, 2 liberi per altri due eventuali motori per i sandwich di ingresso e uscita)
   For i = 1 To 10
      msgadd item:="Allarme az. Lenz N. " + Format$(i), key:="all_" + "L" + Format$(i, "00"): inc_prg_bar_ini 'Arturo uff 25-07-02
   Next i
 'Descrizione completa azionamenti Lenz
   'APalmerini_FI02/12/2004_1
   'For i = 1 To NUM_AZI  'si è deciso di porre 10 come numero massimo di azionamenti lenze (6 per i motori + 2 per i sendwich di ingresso e uscita, 2 liberi per altri due eventuali motori per i sandwich di ingresso e uscita)
   For i = 1 To 10
     msgadd item:="Dettagli allarme az. Lenz N. " + Format$(i), key:="det_all_" + "L" + Format$(i, "00"): inc_prg_bar_ini 'Arturo uff 25-07-02
   Next i
   msgadd item:="", key:="all_" + Space$(3): inc_prg_bar_ini 'Arturo uff 25-07-02 ' allarme non assegnato
   msgadd item:="", key:="det_all_" + Space$(3): inc_prg_bar_ini 'Arturo uff 25-07-02    ' allarme non assegnato
End If
While Frm_ini.prg_bar_ini.Value <= Frm_ini.prg_bar_ini.Max - 1
     inc_prg_bar_ini 'Arturo uff 25-07-02
  Wend
End Sub


'****  variabile globale per i messaggi utente
'Public msg As Collection NOTA: è stata inserita in var_stag


Public Sub msgadd(item As String, key As String, Optional no_warning As Boolean)
Dim m As msg
'On Error GoTo Fine
Set m = New msg
m.key = key
m.item = Mid$(item, 1, LEN_DES)
mg.add item:=m, key:=key
Exit Sub
Fine:
  If IsMissing(no_warning) Or no_warning = False Then
    log_str "on error intervenuta in msgadd" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
  End If
End Sub

Sub set_msg()
'******* crea la collezione msg con i messaggi in italiano
'Nella descrizione delle form, occorre aggiungere lo stato della combinazione dei tasti SHIFT, CTRL e ALT:
'SHIFT=1
'CTRL=2
'ALT=4
Dim i As Integer, j As Integer, h As Integer
Dim det_all_tmp As String, all_tmp As String
Dim mytb As Recordset
Dim flt As Boolean

Set mg = New Collection
'andrea 17-02-06
'PAG_PRO
msgadd item:="Allarmi", key:="pag_pro_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Invio Corrente", key:="pag_pro_2 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Invio Successivo", key:="pag_pro_2 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Parametri Macchina", key:="pag_pro_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Onde", key:="pag_pro_0 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Azzera Conteggio", key:="pag_pro_2 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Dettaglio Ordini", key:="pag_pro_0 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Impostazione data / ora", key:="pag_pro_2 5": inc_prg_bar_ini 'Arturo uff 25-07-02 '6122001 Stagetti
msgadd item:="Lista Pattern", key:="pag_pro_0 6": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Abilitazioni", key:="pag_pro_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02 '5122001 Stagetti
msgadd item:="Inizio/Fine Produzione", key:="pag_pro_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Ordini Eseguiti", key:="pag_pro_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="pag_pro_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02

'SET_PAR_MAC

msgadd item:="Cambia lingua", key:="set_par_mac_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
'msgadd item:="Impostazione data / ora", key:="set_par_mac_2 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Modifica", key:="set_par_mac_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Salva", key:="set_par_mac_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Parametri Comunicazioni", key:="set_par_mac_0 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Parametri Schede Taglio", key:="set_par_mac_0 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Parametri Rulli", key:="set_par_mac_0 6": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Impostazione Allarmi", key:="set_par_mac_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
'msgadd item:="Abilitazioni", key:="set_par_mac_0 8": Frm_ini.prg_bar_ini.value = Frm_ini.prg_bar_ini.value + 1 '5122001 Stagetti il comando è stato spostato nella pag_pro
msgadd item:="Lista Ordini", key:="set_par_mac_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="set_par_mac_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

' ABILITAZIONI

'msgadd item:="Parametri Macchina", key:="abilitazioni_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="abilitazioni_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="abilitazioni_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="abilitazioni_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02


'set_par_com

msgadd item:="Parametri Macchina", key:="set_par_com_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Modifica", key:="set_par_com_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Salva", key:="set_par_com_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="set_par_com_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="set_par_com_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="set_par_com_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02


'set_par_sch_tag1

msgadd item:="Parametri Macchina", key:="set_par_sch_tag1_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Modifica", key:="set_par_sch_tag1_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Modifica Curva di Taglio", key:="set_par_sch_tag1_2 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Salva", key:="set_par_sch_tag1_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Salva Curva di Taglio", key:="set_par_sch_tag1_2 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Default -> Attivi Parziale", key:="set_par_sch_tag1_0 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Default -> Attivi Totale", key:="set_par_sch_tag1_2 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Attivi -> Default Parziale", key:="set_par_sch_tag1_0 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Attivi -> Default Totale", key:="set_par_sch_tag1_2 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Cambia Livello", key:="set_par_sch_tag1_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="set_par_sch_tag1_2 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="set_par_sch_tag1_2 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Successiva", key:="set_par_sch_tag1_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="set_par_sch_tag1_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'set_par_sch_tag2

msgadd item:="Parametri Macchina", key:="set_par_sch_tag2_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Modifica", key:="set_par_sch_tag2_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Salva", key:="set_par_sch_tag2_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Default -> Attivi Parziale", key:="set_par_sch_tag2_0 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Default -> Attivi Totale", key:="set_par_sch_tag2_2 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Attivi -> Default Parziale", key:="set_par_sch_tag2_0 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Attivi -> Default Totale", key:="set_par_sch_tag2_2 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Cambia Livello", key:="set_par_sch_tag2_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="set_par_sch_tag2_2 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="set_par_sch_tag2_2 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Precedente", key:="set_par_sch_tag2_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Successiva", key:="set_par_sch_tag2_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="set_par_sch_tag2_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'set_par_sch_tag3

msgadd item:="Parametri Macchina", key:="set_par_sch_tag3_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Nuova Configurazione", key:="set_par_sch_tag3_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Carica Configurazione", key:="set_par_sch_tag3_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Modifica Configurazione", key:="set_par_sch_tag3_0 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Salva Configurazione", key:="set_par_sch_tag3_0 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Elimina Configurazione", key:="set_par_sch_tag3_0 6": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Cambia Livello", key:="set_par_sch_tag3_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="set_par_sch_tag3_2 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="set_par_sch_tag3_2 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Precedente", key:="set_par_sch_tag3_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Successiva", key:="set_par_sch_tag3_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="set_par_sch_tag3_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'set_par_sch_tag4

msgadd item:="Parametri macchina", key:="set_par_sch_tag4_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Modifica", key:="set_par_sch_tag4_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Salva", key:="set_par_sch_tag4_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Default -> Attivi Parziale", key:="set_par_sch_tag4_0 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Default -> Attivi Totale", key:="set_par_sch_tag4_2 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Attivi -> Default Parziale", key:="set_par_sch_tag4_0 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Attivi -> Default Totale", key:="set_par_sch_tag4_2 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lettura Par. Informazione", key:="set_par_sch_tag4_0 6": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Cambia Livello", key:="set_par_sch_tag4_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="set_par_sch_tag4_2 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="set_par_sch_tag4_2 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Precedente", key:="set_par_sch_tag4_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Successiva", key:="set_par_sch_tag4_0 9": inc_prg_bar_ini 'Robertouff 26-11-2007
msgadd item:="Pagina Produzione", key:="set_par_sch_tag4_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'set_par_sch_tag5

msgadd item:="Parametri macchina", key:="set_par_sch_tag5_0 1": inc_prg_bar_ini 'Robertouff 26-11-2007
msgadd item:="Modifica", key:="set_par_sch_tag5_0 2": inc_prg_bar_ini 'Robertouff 26-11-2007
msgadd item:="Salva", key:="set_par_sch_tag5_0 3": inc_prg_bar_ini 'Robertouff 26-11-2007
msgadd item:="Default -> Attivi Parziale", key:="set_par_sch_tag5_0 4": inc_prg_bar_ini 'Robertouff 26-11-2007
msgadd item:="Default -> Attivi Totale", key:="set_par_sch_tag5_2 4": inc_prg_bar_ini 'Robertouff 26-11-2007
msgadd item:="Attivi -> Default Parziale", key:="set_par_sch_tag5_0 5": inc_prg_bar_ini 'Robertouff 26-11-2007
msgadd item:="Attivi -> Default Totale", key:="set_par_sch_tag5_2 5": inc_prg_bar_ini 'Robertouff 26-11-2007
'RFAZZI_FI 20-04-2008
'msgadd item:="Lettura Par. Informazione", key:="set_par_sch_tag5_0 6": inc_prg_bar_ini 'Robertouff 26-11-2007
msgadd item:="Cambia Livello", key:="set_par_sch_tag5_0 7": inc_prg_bar_ini 'Robertouff 26-11-2007
msgadd item:="Lista Ordini Eseguiti", key:="set_par_sch_tag5_2 8": inc_prg_bar_ini 'Robertouff 26-11-2007
msgadd item:="Lista Ordini", key:="set_par_sch_tag5_2 9": inc_prg_bar_ini 'Robertouff 26-11-2007
msgadd item:="Pagina Precedente", key:="set_par_sch_tag5_0 8": inc_prg_bar_ini 'Robertouff 26-11-2007
msgadd item:="Pagina Produzione", key:="set_par_sch_tag5_0 10": inc_prg_bar_ini 'Robertouff 26-11-2007

'SET_PAR_RUL

msgadd item:="Parametri Macchina", key:="set_par_rul_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Modifica", key:="set_par_rul_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Salva", key:="set_par_rul_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="set_par_rul_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="set_par_rul_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="set_par_rul_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02


'PAG_LIS_ORD

msgadd item:="Dettaglio", key:="pag_lis_ord_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Cancella Ordini", key:="pag_lis_ord_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Muovi Ordini", key:="pag_lis_ord_0 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Copia Ordini", key:="pag_lis_ord_0 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Trova Ordine", key:="pag_lis_ord_0 6": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Dettagli ON/OFF", key:="pag_lis_ord_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="pag_lis_ord_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="pag_lis_ord_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'PAG_LIS_ESE

' ABorelli_FI09/09/2005_1
'andrea 17-02-06
'If par_abi.abi_rep_pro Then
msgadd item:="Produzione Giornaliera", key:="pag_lis_ese_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02
'End If
msgadd item:="Cancella Ordini", key:="pag_lis_ese_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Copia in Lista Ordini", key:="pag_lis_ese_0 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Trova Ordine", key:="pag_lis_ese_0 6": inc_prg_bar_ini 'Arturo uff 25-07-02
'Arturo uff 02-03-04 (1) tolta visualizzazione report di produzione
' ABorelli_FI09/09/2005_1
'andrea 17-02-06
'If par_abi.abi_rep_pro Then
msgadd item:="Report ON/OFF", key:="pag_lis_ese_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
'End If
msgadd item:="Lista Ordini", key:="pag_lis_ese_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="pag_lis_ese_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02
'PAG_PRO_DAY
' ABorelli_FI09/09/2005_1
msgadd item:="Cambio turno", key:="pag_pro_day_0 2": inc_prg_bar_ini
msgadd item:="Azzera produzione", key:="pag_pro_day_0 3": inc_prg_bar_ini
msgadd item:="Lista Ordini Eseguiti", key:="pag_pro_day_0 8": inc_prg_bar_ini
msgadd item:="Lista Ordini", key:="pag_pro_day_0 9": inc_prg_bar_ini
msgadd item:="Pagina Produzione", key:="pag_pro_day_0 10": inc_prg_bar_ini

'INS_ORD

msgadd item:="Nuovo", key:="ins_ord_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Modifica", key:="ins_ord_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Nuovo Nullo", key:="ins_ord_0 3": inc_prg_bar_ini 'PMASSARO_FI05-08-11 - Aggiunta gestione ordine nullo
msgadd item:="Salva", key:="ins_ord_0 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Precedente", key:="ins_ord_0 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Successivo", key:="ins_ord_0 6": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Ordine Corrente", key:="ins_ord_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="ins_ord_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="ins_ord_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="ins_ord_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02


'ONDE

msgadd item:="Nuovo", key:="onde_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Modifica", key:="onde_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Cancella", key:="onde_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Salva", key:="onde_0 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="onde_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="onde_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="onde_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'IMP_ALL
msgadd item:="Impostazione Bit Allarme", key:="imp_all_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Descrizioni Allarmi", key:="imp_all_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Reset Bit Allarme", key:="imp_all_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Scelta Pagina", key:="imp_all_0 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="", key:="imp_all_0 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Parametri Macchina", key:="imp_all_0 6": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="imp_all_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="imp_all_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="imp_all_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'ALL_MAC

msgadd item:="Allarmi", key:="all_mac_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Frequenze Allarmi", key:="all_mac_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="all_mac_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="all_mac_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="all_mac_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'ALL_CON

msgadd item:="Allarmi", key:="all_con_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Frequenze Allarmi", key:="all_con_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="all_con_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="all_con_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="all_con_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'ALL_HAR

msgadd item:="Allarmi", key:="all_har_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Frequenze Allarmi", key:="all_har_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="all_har_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="all_har_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="all_har_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'ALL_QE1

msgadd item:="Allarmi", key:="all_qe1_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Successiva", key:="all_qe1_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Diagnostica Azionamenti", key:="all_qe1_0 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Diagn. Scheda Taglio 1", key:="all_qe1_0 6": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Frequenze Allarmi", key:="all_qe1_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="all_qe1_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="all_qe1_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="all_qe1_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'ALL_QE2

msgadd item:="Allarmi", key:="all_qe2_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Successiva", key:="all_qe2_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Precedente", key:="all_qe2_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Diagnostica Azionamenti", key:="all_qe2_0 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Diagn. Scheda Taglio 2", key:="all_qe2_0 6": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Frequenze Allarmi", key:="all_qe2_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="all_qe2_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="all_qe2_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="all_qe2_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'ALL_QE3

msgadd item:="Allarmi", key:="all_qe3_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Precedente", key:="all_qe3_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Diagn. Scheda Taglio 3", key:="all_qe3_0 6": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Frequenze Allarmi", key:="all_qe3_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="all_qe3_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="all_qe3_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="all_qe3_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'ALL_WAR

msgadd item:="Allarmi", key:="all_war_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Frequenze Allarmi", key:="all_war_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="all_war_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="all_war_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="all_war_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'PAG_ALL

msgadd item:="Allarmi Macchina", key:="pag_all_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Allarmi Consolle", key:="pag_all_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Allarmi Quadri Elettrici", key:="pag_all_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Allarmi Hardware", key:="pag_all_0 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Allarmi Warning", key:="pag_all_0 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Storico Allarmi", key:="pag_all_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="pag_all_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="pag_all_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="pag_all_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'STO_ALL

msgadd item:="Allarmi", key:="sto_all_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="sto_all_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="sto_all_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="sto_all_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

' FRE_MAC

msgadd item:="Allarmi", key:="fre_mac_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="fre_mac_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="fre_mac_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="fre_mac_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

' FRE_CON

msgadd item:="Allarmi", key:="fre_con_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="fre_con_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="fre_con_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="fre_con_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

' FRE_HAR

msgadd item:="Allarmi", key:="fre_har_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="fre_har_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="fre_har_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="fre_har_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

' FRE_QE1

msgadd item:="Allarmi", key:="fre_qe1_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="fre_qe1_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="fre_qe1_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="fre_qe1_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

' FRE_QE2

msgadd item:="Allarmi", key:="fre_qe2_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="fre_qe2_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="fre_qe2_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="fre_qe2_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

' FRE_QE3

msgadd item:="Allarmi", key:="fre_qe3_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="fre_qe3_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="fre_qe3_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="fre_qe3_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

' FRE_WAR

msgadd item:="Allarmi", key:="fre_war_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini Eseguiti", key:="fre_war_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lista Ordini", key:="fre_war_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="fre_war_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

' DIA_STA_AZI

msgadd item:="Allarmi", key:="dia_sta_azi_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="START Comunicazione", key:="dia_sta_azi_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="STOP Comunicazione", key:="dia_sta_azi_0 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Reset Azionamenti", key:="dia_sta_azi_0 6": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Imp. Nuovo Parametro", key:="dia_sta_azi_0 7": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Imp. Param. Descrizione", key:="dia_sta_azi_0 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Salva Descr. Parametro", key:="dia_sta_azi_2 8": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Diagnosi Rete CAN", key:="dia_sta_azi_0 9": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="dia_sta_azi_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

' DIA_SCH_TAG

msgadd item:="Allarmi", key:="dia_sch_tag_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="START Comunicazione", key:="dia_sch_tag_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Reset Scheda di taglio", key:="dia_sch_tag_0 6": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="dia_sch_tag_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'FRM_DEBUG

msgadd item:="Cambio Data Memory", key:="frm_debug_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Dec <-> Hex <-> Bin", key:="frm_debug_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Stop Timer", key:="frm_debug_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Run Timer", key:="frm_debug_0 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Visualizza Descrizioni", key:="frm_debug_0 5": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="frm_debug_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'FRM_DEBUG_DES

msgadd item:="Cambio Data Memory", key:="frm_debug_des_0 1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Dec <-> Hex <-> Bin", key:="frm_debug_des_0 2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Stop Timer", key:="frm_debug_des_0 3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Run Timer", key:="frm_debug_des_0 4": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Visualizza Data Memory", key:="frm_debug_des_0 6": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pagina Produzione", key:="frm_debug_des_0 10": inc_prg_bar_ini 'Arturo uff 25-07-02

'PATTERN        'Iciliouff 20-11-2003

msgadd item:="Nuovo", key:="pattern_0 1": inc_prg_bar_ini
msgadd item:="Modifica", key:="pattern_0 2": inc_prg_bar_ini
msgadd item:="Cancella", key:="pattern_0 3": inc_prg_bar_ini
msgadd item:="Salva", key:="pattern_0 4": inc_prg_bar_ini
msgadd item:="Acquisizione Contrasto", key:="pattern_2 1": inc_prg_bar_ini
msgadd item:="Lista Ordini Eseguiti", key:="pattern_0 8": inc_prg_bar_ini
msgadd item:="Lista Ordini", key:="pattern_0 9": inc_prg_bar_ini
msgadd item:="Pagina Produzione", key:="pattern_0 10": inc_prg_bar_ini

'ADJ_PAT_CON    'Iciliouff 20-11-2003

msgadd item:="Contrasti possibili / liberi", key:="adj_pat_con_0 1": inc_prg_bar_ini
msgadd item:="Cambia Livello", key:="adj_pat_con_0 2": inc_prg_bar_ini
msgadd item:="Start acquisizione", key:="adj_pat_con_0 3": inc_prg_bar_ini
msgadd item:="Acquisizione terminata", key:="adj_pat_con_0 6": inc_prg_bar_ini
msgadd item:="Lista Ordini Eseguiti", key:="adj_pat_con_0 8": inc_prg_bar_ini
msgadd item:="Lista Ordini", key:="adj_pat_con_0 9": inc_prg_bar_ini
msgadd item:="Pagina Produzione", key:="adj_pat_con_0 10": inc_prg_bar_ini


'==========================================================================

'Messaggi di allarme
msgadd item:="Impossibile eseguire l'operazione richiesta", key:="imp_ese_ope": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Raggiunto il numero massimo di elementi nella lista ordini", key:="rag_num_max": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Parametri errati", key:="par_err": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Codice errato", key:="cod_err": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Codice non presente nella lista ordini", key:="cod_non_pre": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Operazione in corso", key:="ope_in_cor": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Operazione effettuata", key:="ope_eff": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Attenzione: alcuni parametri non trasmessi !", key:="ope_eff_par": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Esistono ordini che utilizzano questa onda", key:="imp_can_ond": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Carta gia' codificata per questa onda", key:="car_esi": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Mancano dei valori fondamentali", key:="man_cam_ond": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Numero massimo di caratteri:", key:="num_max_car": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Indirizzo PLC errato", key:="err_sys_plc": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Registrazione dati di produzione interrotta", key:="reg_pro_int"
msgadd item:="Inizio registrazione dati di produzione", key:="reg_pro_ini"
msgadd item:="Scheda PLC non presente", key:="sch_plc_non_pre": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Verificare indirizzo del PLC nei parametri comunicazioni", key:="ver_ind_plc": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Attenzione! Verificare che la lista degli ordini sia corretta", key:="att_con_lis_ord": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Parametri mancanti", key:="par_man": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Operazione annullata", key:="ope_ann": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Macchina in manuale", key:="mac_in_man": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Macchina in automatico", key:="mac_in_auto": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Macchina non in FAST STOP", key:="mac_non_fas_sto": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lettura parametri per la nuova configurazione della scheda di taglio", key:="let_par_cfg": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Configurazione non trovata per l'attuale stato macchina del livello", key:="par_cfg_non_tro": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Valori non trovati per la curva di taglio", key:="val_non_tro_cur_tag": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Configurazione gia' presente", key:="cfg_pre": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Immettere una descrizione per la configurazione", key:="imm_des": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Inserire una nuova configurazione per questo livello", key:="ins_cfg_liv": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Non ci sono configurazioni da leggere", key:="non_cfg_leg": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Non ci sono configurazioni da eliminare", key:="non_cfg_eli": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Nessuna configurazione da modificare per questo livello", key:="non_mod_cfg": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Nessuna configurazione da salvare per questo livello", key:="non_sal_cfg": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Impossibile caricare questa configurazione con l'attuale stato macchina", key:="imp_car_cfg": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Impossibile disabilitare tutti i motori di un livello", key:="imp_dis_all_mot": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Abilitare almeno un motore sui livelli abilitati", key:="abi_un_mot": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="E' necessario abilitare il livello", key:="abi_il_liv": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Non e' consentito eliminare una configurazione di DEFAULT", key:="del_cfg_def": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Reset non effettuato: configurazione precedente non trovata", key:="cfg_pre_non_tro": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="CONFIGURAZIONE ATTIVA", key:="cfg_att": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="CONFIGURAZIONE NON ATTIVA", key:="cfg_non_att": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Impossibile eliminare la configurazione perche' attiva", key:="del_cfg_att": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Operazione di reset in corso", key:="ope_res_in_cor": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Warning: Can Reset Hardware Error", key:="can_res_err": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Attendere", key:="att": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Comunicazione gia' interrotta", key:="com_int": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Comunicazione gia' abilitata", key:="com_abi": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Il parametro impostato non e' presente", key:="par_non_pre": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Abilitato", key:="mot_abi": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Non abil.", key:="mot_non_abi": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Impossibile cambiare il numero dei livelli di taglio", key:="imp_cam_num_liv_tag": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Impossibile cambiare il formato della macchina", key:="imp_cam_for_mac": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Impossibile cambiare la grammatura max della carta", key:="imp_cam_gra_car": inc_prg_bar_ini 'Arturo uff 05-11-02
msgadd item:="Impossibile cambiare il tipo macchina", key:="imp_cam_tip_mac": inc_prg_bar_ini 'Arturo uff 05-11-02
msgadd item:="Minimo formato macchina errato", key:="err_min_for_mac": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Disabilitare il motore", key:="dis_mot": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="del livello", key:="liv": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Timeout nella comunicazione con la scheda di taglio del livello", key:="tim_out_com_sch_tag": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Errore sulla comunicazione con la scheda di taglio del livello", key:="err_com_sch_tag": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Trasmissione effettuata alla scheda di taglio del livello", key:="tra_eff_sch_tag": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Invio parametri schede di taglio", key:="inv_par_sch_tag": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Rilevato problema sull'impostazione delle porte seriali", key:="pro_imp_por_ser": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Parametri di comunicazione non validi", key:="par_com_non_val": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="La minima lunghezza di taglio non puo' essere inferiore a", key:="min_lun_tag": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="La lista degli ordini non e' vuota", key:="lis_ord_non_vuo": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Impossibile trovare ", key:="imp_tro": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Accesso solo con Password.", key:="acc_pwd": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="La porta", key:="msg_por": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="non e' aperta", key:="por_non_ape": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="e' gia' aperta", key:="por_ape": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="non e' presente", key:="por_non_pre": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Comunicazione interrotta con la macchina", key:="com_int_con_mac": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Scheda di taglio non presente", key:="sch_tag_non_pre": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="La scheda di taglio del livello", key:="sch_tag_del_liv": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="non e' pronta", key:="non_pro": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Comunicazione interrotta con la scheda di taglio", key:="com_int_con_sch": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Nessuna descrizione disponibile", key:="nes_des_dis": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Misure anglosassoni abilitate", key:="mis_ang_abi": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Misure anglosassoni disabilitate", key:="mis_ang_dis": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="AVVERTIMENTO:", key:="tit_war_inv_mot": inc_prg_bar_ini 'Arturo uff 05-03-03
msgadd item:="ATTENZIONE! Scollegare il cavo inverter - motore lato inverter prima di ripartire.", key:="des_war_inv_mot1": inc_prg_bar_ini 'Arturo uff 05-03-03
msgadd item:="Prima di riabilitare il motore ricollegare il cavo.", key:="des_war_inv_mot2": inc_prg_bar_ini 'Arturo uff 05-03-03
msgadd item:="Memoria fotocellula pattern completamente utilizzata", key:="err_no_pat_con_lib": inc_prg_bar_ini     'Iciliouff 09-12-2003
msgadd item:="Nessuna procedura di acquisizione in corso", key:="err_no_pro_acq_cor": inc_prg_bar_ini     'Iciliouff 09-12-2003
msgadd item:="Procedura di acquisizione in corso", key:="err_pro_acq_cor": inc_prg_bar_ini     'Iciliouff 09-12-2003

'Messaggi per la Ins_Ord
msgadd item:="Inserire un codice per l'ordine", key:="ins_cod_ord": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Inserire un codice per l'onda", key:="ins_onda": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Inserire un codice per la composizione delle carte", key:="ins_carta": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Valore minimo", key:="val_min": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Valore massimo", key:="val_max": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Carattere non permesso", key:="car_non_per": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Inserire la larghezza della scatola", key:="ins_lar": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Misura dell'ordine eccessiva", key:="mis_ord_ecc": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Inserire la lunghezza della scatola.", key:="ins_lun_for": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Inserire tutti i dati per il formato da lavorare", key:="ins_all_dat": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Il livello assegnato non e' abilitato", key:="liv_non_abi": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Inserire il dato relativo al Cut to mark", key:="ins_cut_to_mark": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Cut to mark non presente nella configurazione macchina", key:="cut_to_mark_non_pre": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Cut to mark diverso su due formati abbinati su stesso livello", key:="cut_to_mark_liv_abb": inc_prg_bar_ini 'Iciliouff 20-11-2003
msgadd item:="Dato codice pattern diverso su due formati abbinati su stesso livello", key:="err_cod_pat": inc_prg_bar_ini 'errore: dato codice pattern impostato in modo diverso sui due formati abbinati sullo stesso livello
msgadd item:="Errore su dato offset di taglio ", key:="err_ofs_cut_pat": inc_prg_bar_ini            'errore: dato offset taglio registro non corretto
msgadd item:="Errore su quote pattern (L1..L10)", key:="err_mis_pat": inc_prg_bar_ini                'errore: dato L1..L10 errati
msgadd item:="Errore contrasto fotocellula pattern", key:="err_pat_con": inc_prg_bar_ini                'errore: dato contrasto fotocellula non corretto
msgadd item:="Errore finestra prima del pattern", key:="err_fin_pri": inc_prg_bar_ini                'errore: dato finestra prima del pattern/mark non corretto
msgadd item:="Errore finestra dopo del pattern", key:="err_fin_dop": inc_prg_bar_ini                'errore: dato finestra dopo del pattern/mark non corretto
msgadd item:="Errore nell'altezza bobina", key:="err_alt_bob": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Errore nella commessa di un formato", key:="err_cod_com": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Misura eccessiva nella larghezza delle scatole", key:="err_max_sca_lar": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Errore nella lunghezza scatola di un formato", key:="err_sca_lun": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Inserire il dato relativo alla resa", key:="err_ass_sca_per_tag": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Errore nella resa di un formato", key:="err_sca_per_tag": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Due formati sullo stesso livello devono avere uguale lunghezza scatola", key:="err_liv_lun_div": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Valore errato", key:="err_val": inc_prg_bar_ini ' ABorelli_FI09/09/2005_1
'RAFZZI_FI 07-01-08
msgadd item:="Errore Posizione laterale del Pattern", key:="err_pos_pat": inc_prg_bar_ini


msgadd item:="Problemi sul dispositivo CAN", key:="pro_dis_can": inc_prg_bar_ini 'Arturo uff 25-07-02

'Messaggi per ricevimento ordini da SPDE
msgadd item:="Ricevuto ordine da DRY-END CONTROL", key:="ric_ord_spde": inc_prg_bar_ini 'Arturo uff 25-07-02 'PRocchiccioli 16/12/10
msgadd item:="Rifiutato ordine da DRY-END CONTROL", key:="rif_ord_spde": inc_prg_bar_ini 'Arturo uff 25-07-02 'PRocchiccioli 16/12/10

'Descrizione livelli
msgadd item:="ALTO", key:="liv_alt": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="BASSO", key:="liv_bas": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="MEDIO", key:="liv_med": inc_prg_bar_ini 'Arturo uff 25-07-02

'Descrizione per presenza cut_to_mark/pattern sugli ordini
msgadd item:="Si.", key:="msg_si": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="No.", key:="msg_no": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pattern", key:="msg_cut_pat": inc_prg_bar_ini 'Iciliouff 20-11-2003

'Descrizione dei messaggi sulla sta_bar
msgadd item:="CO in corso", key:="msg_co_in_cor": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="CO completato", key:="msg_co_com": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Preallarme", key:="msg_pre": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pronta al cambio", key:="msg_pro_cam": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Macchina in marcia", key:="msg_in_mar": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Pronta alla marcia", key:="msg_pro_mar": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Macchina ferma", key:="msg_fer": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Macch. ripristinata", key:="msg_rip": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Man", key:="msg_mac_in_man": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Auto", key:="msg_mac_in_aut": inc_prg_bar_ini 'Arturo uff 25-07-02

'Descrizione dei messaggi sulla pag_pro
msgadd item:="Ordine in lavoro", key:="msg_ord_in_lav": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Ordine allineato", key:="msg_ord_all": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Ordine non allineato", key:="msg_ord_non_all": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Ordine non presente", key:="msg_ord_ass": inc_prg_bar_ini 'Arturo uff 25-07-02

'Messaggio di invio parametri macchina al PLC
msgadd item:="Invio parametri alla macchina", key:="msg_inv_par_mac": inc_prg_bar_ini 'Arturo uff 25-07-02

'Messaggio di invio parametri curva di taglio al PLC
msgadd item:="Invio parametri curva di taglio alla macchina", key:="msg_inv_par_cur_tag": inc_prg_bar_ini 'Arturo uff 25-07-02

'Messaggio di invio ordini alla macchina
msgadd item:="Invio ordine", key:="msg_inv_ord": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Invio modifica ordine", key:="msg_inv_mod_ord": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="corrente", key:="msg_cor": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="successivo", key:="msg_suc": inc_prg_bar_ini 'Arturo uff 25-07-02

'Descrizione colonne delle liste ordini
msgadd item:="Cod. Ordine", key:="lis_cod_abb": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Onda", key:="lis_ond": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Liv.", key:="lis_liv": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Commessa", key:="lis_com": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Nome Cliente", key:="lis_nom_cli": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Codice Cliente", key:="lis_cod_cli": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Larg.", key:="lis_lar": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Lung.", key:="lis_lun": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Resa", key:="lis_res": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Scatole", key:="lis_sca": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Livello", key:="rep_liv": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Fogli Buoni", key:="rep_fog_buo": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Fogli Errati", key:="rep_fog_err": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Fogli di Scarto", key:="rep_fog_sca": inc_prg_bar_ini 'Arturo uff 25-07-02

'Descrizione colonne della DbGrid nella pagina onde
msgadd item:="Onde", key:="db_grid_onde": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Onda", key:="db_grid_onda": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="Carta", key:="db_grid_carta": inc_prg_bar_ini 'Arturo uff 25-07-02

' ABorelli_FI09/09/2005_1
'Descrizione colonne dei report turno giorno
msgadd item:="Turno", key:="ptg_tur": inc_prg_bar_ini
msgadd item:="Data ini.", key:="ptg_dat_ini": inc_prg_bar_ini
msgadd item:="Ora ini.", key:="ptg_ora_ini": inc_prg_bar_ini
msgadd item:="Data fine", key:="ptg_dat_fin": inc_prg_bar_ini
msgadd item:="Ora fine", key:="ptg_ora_fin": inc_prg_bar_ini
msgadd item:="Metri lin.", key:="ptg_met_pro": inc_prg_bar_ini
msgadd item:="Metri qua.", key:="ptg_met_qua": inc_prg_bar_ini
msgadd item:="V media", key:="ptg_vel_med": inc_prg_bar_ini
msgadd item:="T. fermo", key:="ptg_tem_fer": inc_prg_bar_ini
msgadd item:="T. marcia", key:="ptg_tem_mar": inc_prg_bar_ini
msgadd item:="N. fermi", key:="ptg_num_fer": inc_prg_bar_ini
msgadd item:="N. cambi", key:="ptg_num_cam": inc_prg_bar_ini
msgadd item:="Giorno", key:="ptg_gio": inc_prg_bar_ini

'Iciliouff 20-11-2003
'Descrizione colonne della DbGrid nella pagina pattern
msgadd item:="PATTERN", key:="pattern": inc_prg_bar_ini
msgadd item:="Codice", key:="sig_cod": inc_prg_bar_ini

'Descrizione del parametro 168/1 relativo alla diagnostica sugli azionamenti
For i = 1 To cod_azi.Count
  If Left$(cod_azi(i).key, 3) = "168" Then
      msgadd item:=cod_azi(i).item1, key:=cod_azi(i).key: inc_prg_bar_ini 'Arturo uff 25-07-02
  End If
Next i

'Descrizione impostazione pagine di allarmi

msgadd item:="1 Allarmi Macchina", key:="AMAC": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="2 Allarmi Hardware", key:="AHAR": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="3 Allarmi Consolle", key:="ACON": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="4 Allarmi Quadro Elettrico 1", key:="AQE1": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="5 Allarmi Quadro Elettrico 2", key:="AQE2": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="6 Allarmi Quadro Elettrico 3", key:="AQE3": inc_prg_bar_ini 'Arturo uff 25-07-02
msgadd item:="7 Malfunzionamenti", key:="AWAR": inc_prg_bar_ini 'Arturo uff 25-07-02

set_all

End Sub

Function msg(key As String, Optional Log As Boolean) As String
'log deve essere settato a False per disabilitare la scrittura del file di Log
On Local Error GoTo Ges_Log
  msg = mg.item(key).item
  Exit Function
Ges_Log:
  If IsMissing(Log) Or Log = True Then
    log_str "msg: Impossibile trovare il messaggio <" + key + "> nel database", MED_PRIORITY, log_gen
  End If
End Function
Sub imposta(db As Database, lin_att As Integer)
Dim f As Form
Dim c As Control
Dim ds As Recordset
Dim i As Integer, k As Integer
Dim ind As Integer
Dim tb As Recordset

On Local Error GoTo GEST_ERR

If pro_set.ric_msg_db Then
   While Frm_ini.prg_bar_ini.Value <= Frm_ini.prg_bar_ini.Max - 1
      inc_prg_bar_ini 'Arturo uff 25-07-02
   Wend
   Frm_ini.prg_bar_ini.Visible = True: Frm_ini.Refresh: DoEvents
   Frm_ini.lbl_Wrn.Caption = "Rebuilding language's database ....": Frm_ini.Refresh: DoEvents
   Frm_ini.prg_bar_ini.Value = 0
   '********* copia tabella messaggi
   FileCopy pro_set.pro_pat & "messaggi.dbf", pro_set.pro_pat & "tmp_mes.dbf"
   If Dir$(pro_set.pro_pat & "tmp_mes.ndx") <> "" Then Kill pro_set.pro_pat & "tmp_mes.ndx"             'uccido l'indice
   If Dir$(pro_set.pro_pat & "tmp_mes.inf") <> "" Then Kill pro_set.pro_pat & "tmp_mes.inf"
   db.Execute "create index tmp_mes on tmp_mes(form,controllo,indice)"
   Set tb = db.OpenRecordset("tmp_mes", dbOpenTable)
   tb.Index = "tmp_mes"
   '************* crea tabella messaggi vuota
   If Dir$(pro_set.pro_pat + "messaggi.dbf") <> "" Then Kill pro_set.pro_pat & "messaggi.dbf"
   creadb db
   '************** aggiunta dei record ordinati in messaggi
   Set ds = db.OpenRecordset("select * from messaggi", dbOpenDynaset)
   Frm_ini.prg_bar_ini.Max = num_con
   If mes_dbf_non_pre Then Frm_ini.prg_bar_ini.Max = Frm_ini.prg_bar_ini.Max + NUM_GRU_ALL * NUM_DAT_MEM_ALL * 16 * 2 + 2 * NUM_AZI + 2
   Frm_ini.prg_bar_ini.Visible = True: Frm_ini.Refresh: DoEvents
   For Each f In Forms
        For Each c In f.Controls
            inc_prg_bar_ini 'Arturo uff 25-07-02
            If tra_ctrl(f, c) Then
                ind = 0
                ind = c.Index
                tb.Seek "=", f.name, c.name, Format$(ind)
                If Not tb.NoMatch Then
                        ds.AddNew
                        'copio tutti i campi del record
                        For i = 0 To tb.Fields.Count - 1
                            If i <> 3 Then
                                ds(i) = tb(i)
                            Else
                                ds(i) = c.Caption
                            End If
                        Next i
                        ds.Update      'salvo i dati appena scritti
                Else
                        ds.AddNew
                        ds("form") = f.name
                        ds("controllo") = c.name
                        ds("indice") = ind
                        If TypeOf c Is Label Or TypeOf c Is Frame Or TypeOf c Is CommandButton Or TypeOf c Is CheckBox Then
                                ds("italian") = Mid$(c.Caption, 1, LEN_DES)
                        End If
                        ds.Update
                End If
            End If
        Next c
   Next f
   If mes_dbf_non_pre Then
      ds.Close
      set_all
      Set ds = db.OpenRecordset("select * from messaggi", dbOpenDynaset)
      mes_dbf_non_pre = False
   End If
   
   Frm_ini.lbl_Wrn.Caption = "Building messages...": Frm_ini.Refresh: DoEvents
   Frm_ini.prg_bar_ini.Value = 0
   Frm_ini.prg_bar_ini.Max = mg.Count
   
      
   '************** impostazione messaggi
   For i = 1 To mg.Count
        Frm_ini.prg_bar_ini.Value = i
        tb.Seek "=", "", mg(i).key, "0"
        If Not tb.NoMatch Then
              ds.AddNew
              For k = 0 To tb.Fields.Count - 1
                  ds(k) = tb(k)
              Next k
              ds("italian") = Mid$(mg(i).item, 1, LEN_DES)   'per evitare problemi di disallineamento copio per sicurezza la descrizione del messaggio
              ds.Update
        Else
              ds.AddNew
              ds("controllo") = mg(i).key
              ds("italian") = Mid$(mg(i).item, 1, LEN_DES)
              ds("indice") = 0
              ds.Update
        End If
   Next i
   Frm_ini.prg_bar_ini.Visible = False: Frm_ini.Refresh: DoEvents
   tb.Close
   ds.Close
   '***************** toglie la tabella copia
   If Dir$(pro_set.pro_pat + "tmp_mes.dbf") <> "" Then Kill pro_set.pro_pat & "tmp_mes.dbf"
   If Dir$(pro_set.pro_pat + "tmp_mes.ndx") <> "" Then Kill pro_set.pro_pat & "tmp_mes.ndx"
   If Dir$(pro_set.pro_pat + "tmp_mes.inf") <> "" Then Kill pro_set.pro_pat & "tmp_mes.inf"
  
   'If not mes_dbf_non_pre Then Leggi pro_set.lin_att
End If
  
Exit Sub
  
GEST_ERR:
  If err.Number = 343 Then
      Resume Next
 Else
      log_str "on error intervenuta in imposta" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
 End If

End Sub
  
Function tra_ctrl(f As Form, c As Control) As Boolean
' ritorna vero se il controllo deve essere tradotto
  tra_ctrl = False
'  If (f.name = "Frm_data") Or (f.name = "Pwd_frm") Or (f.name = "Frm_ini") Or (Left$(f.name, 3) = "set") Or (f.name = "frm_debug") Or (f.name = "frm_debug_des") _
   Or (f.name = "abilitazioni") Or (f.name = "del_cfg") Or (f.name = "dia_par_new") Or (f.name = "dia_ret_azi") _
   Or (f.name = "dia_sta_azi") Or (f.name = "dia_sch_tag") Or (f.name = "imp_all") Or (f.name = "leg_cfg") Or (f.name = "sal_cfg") Or (f.name = "sta_bar" And c.name = "mes_all") _
   Or (f.name = "sta_bar" And c.name = "lab_msg") Or (f.name = "sta_bar" And c.name = "lab_sta_mac") Or (f.name = "sta_bar" And c.name = "lab_gray_med") Then
  If (f.name = "Frm_data") Or (f.name = "Pwd_frm") Or (f.name = "Frm_ini") Or (Left$(f.name, 3) = "set") Or (f.name = "frm_debug") Or (f.name = "frm_debug_des") _
   Or (f.name = "del_cfg") Or (f.name = "dia_par_new") Or (f.name = "dia_ret_azi") _
   Or (f.name = "dia_sta_azi") Or (f.name = "dia_sch_tag") Or (f.name = "imp_all") Or (f.name = "leg_cfg") Or (f.name = "sal_cfg") Or (f.name = "sta_bar" And c.name = "mes_all") _
   Or (f.name = "sta_bar" And c.name = "lab_msg") Or (f.name = "sta_bar" And c.name = "lab_sta_mac") Or (f.name = "sta_bar" And c.name = "lab_gray_med") Then
    tra_ctrl = False
  Else
    If TypeOf c Is Label Or TypeOf c Is Frame Or TypeOf c Is CommandButton Or TypeOf c Is CheckBox Then
      tra_ctrl = True
    End If
  End If
End Function
Public Sub creadb(db As Database)
Dim cmd As String
Dim rs As Recordset
Dim s As String

  s = "select * from lingue"
  Set rs = db.OpenRecordset(s, dbOpenDynaset)
  cmd = "create table messaggi (FORM TEXT(20), CONTROLLO TEXT(30), INDICE INTEGER2, ITALIAN TEXT("
  cmd = cmd + Format$(LEN_DES)
  rs.MoveNext
  While Not rs.EOF
    cmd = cmd + ")," + rs(1) + " TEXT("
    cmd = cmd + Format$(LEN_DES)
    rs.MoveNext
  Wend
  rs.Close
  cmd = cmd + "))"
  db.Execute (cmd)

End Sub

Function Leggi(lin_att As Integer) As Boolean
'******* Restituisce True se ci sono problemi
Dim f As Form        'per il ciclo sulle form
Dim c As Control     'per il ciclo sui controlli di ogni form
Dim ds As Recordset  'per la gestione dell'oggeto DataBase
Dim i As Integer
Dim ind As Integer
Dim capt As Variant
Dim cha_set As Integer '13122001 Stagetti gestione dei caratteri internazionali

On Error GoTo GEST_ERR

Leggi = False
cha_set = val(getFileINIString("PRO_SET", "CHA_SET", "0"))
Set ds = db.OpenRecordset("select * from messaggi", dbOpenDynaset)

'se il file messaggi è vuoto esco dalla funzione con leggi = true
If ds.RecordCount = 0 Then
  ds.Close
  Leggi = True
  Exit Function
End If
ds.MoveFirst
Frm_ini.lbl_Wrn.Caption = "Loading language's database...": Frm_ini.Refresh: DoEvents
Frm_ini.prg_bar_ini.Max = Forms.Count + mg.Count
Frm_ini.prg_bar_ini.Visible = True: Frm_ini.Refresh: DoEvents

Frm_ini.prg_bar_ini.Value = 0
For Each f In Forms
  inc_prg_bar_ini 'Arturo uff 25-07-02
  For Each c In f.Controls
    If tra_ctrl(f, c) Then
        ind = 0       ' per i controlli che non hanno indice
        ind = c.Index
        If ds.EOF = False Then
            capt = ds("italian")
            If IsNull(capt) Then capt = ""
            'andrea 17-02-06
'            If Trim$(Format$(ds("form"))) <> Trim$(f.name) Or Trim$(Format$(ds("controllo"))) <> Trim$(c.name) Or ds("indice") <> ind _
'              Or Trim$(capt) <> Trim$(c.Caption) Then
            If Trim$(Format$(ds("form"))) <> Trim$(f.name) Or Trim$(Format$(ds("controllo"))) <> Trim$(c.name) Or ds("indice") <> ind Then
                 log_str "1 db lingue disallineato: in db =" + Trim$(Format$(ds("form"))) + " " + Trim$(Format$(ds("controllo"))) + " " + Format$(ds("indice")) + ",in form=" + Trim$(f.name) + " " + Trim$(c.name) + " " + Trim$(ind), MED_PRIORITY, log_gen
                 ds.Close
                 Leggi = True
                 Exit Function
            Else
               If TypeOf c Is Label Then
                 c.Caption = Format$(ds(lin_att + 3))
                 ElseIf TypeOf c Is Frame Then
                 c.Caption = Format$(ds(lin_att + 3))
                 ElseIf TypeOf c Is CommandButton Then
                 c.Caption = Format$(ds(lin_att + 3))
                 ElseIf TypeOf c Is CheckBox Then
                 c.Caption = Format$(ds(lin_att + 3))
                 ElseIf TypeOf c Is OptionButton Then
                 c.Caption = Format$(ds(lin_att + 3))
               End If
               c.Font.Charset = cha_set '13122001 stagetti gestione dei caratteri internazionali
            End If
            ds.MoveNext
        Else
            log_str "2 db lingue terminato, in form=" + Trim$(f.name) + " " + Trim$(c.name) + " " + Trim$(ind), MED_PRIORITY, log_gen
            ds.Close
            Leggi = True
            Exit Function
        End If
    Else
      '13122001 stagetti gestione dei caratteri internazionali
      'alessandro 06-04-2006
      'If TypeOf c Is TextBox Then
      If (TypeOf c Is TextBox) Or (TypeOf c Is DBGrid) Or (TypeOf c Is DBCombo) Or (TypeOf c Is ComboBox) Or (TypeOf c Is MSFlexGrid) Then
        c.Font.Charset = cha_set
      End If
    End If
  Next c
Next f

'******  impostazione messaggi
For i = 1 To mg.Count
  If ds.EOF = False Then
    capt = ds("italian")
    If IsNull(capt) Then capt = ""
    inc_prg_bar_ini 'Arturo uff 25-07-02
    'andrea 17-02-06
    'If Format$(ds("form")) <> "" Or Trim$(ds("controllo") & "") <> Trim$(mg(i).key) Or capt <> Trim$(mg(i).item) Then
    If Format$(ds("form")) <> "" Or Trim$(ds("controllo") & "") <> Trim$(mg(i).key) Then
      log_str "3 db lingue disallineato: in db =" + Trim$(Format$(ds("form"))) + " " + Trim$(Format$(ds("controllo"))) + " " + Format$(ds("indice")) + ", in msg=" + Trim$(mg(i).key), MED_PRIORITY, log_gen
      Leggi = True
      ds.Close
      Exit Function
    Else
      If Trim$(Format$(ds("italian"))) <> Left$(Trim$(mg(i).item), LEN_DES) And pro_set.ric_msg_db And lin_att = ITA Then
        ds.Edit
        ds("italian") = Left$(mg(i).item, LEN_DES)
        ds.Update
      End If
      mg(i).item = Format$(ds(lin_att + 3))
    End If
  Else
    log_str "4 db lingue terminato, key= " + mg(i).key + " in msg=" + Trim$(mg(i).item), MED_PRIORITY, log_prob
    Leggi = True
    ds.Close
    Exit Function
  End If
  ds.MoveNext
Next i
ds.Close
Exit Function

GEST_ERR:
If err.Number = 343 Then
  Resume Next
Else
  log_str "on error intervenuta in imposta" + ": " + err.Description + " (" + Format$(err.Number) + ")", MED_PRIORITY, log_gen
End If
End Function
  
Sub set_lin(ini As String)
' se ini <> "" vengono aggiornate le scritte in italiano nel dbf


'se non esiste messaggi.dbf la creo
mes_dbf_non_pre = True
If Dir$(pro_set.pro_pat + "messaggi.dbf") = "" Then
  creadb db
Else
  mes_dbf_non_pre = False
End If

'******* legge da dbf le descrizioni controlli e messaggi e li assegna
' la leggi assegna i messaggi e se c'è un errore restituisce leggi=true
' ==> se ho errore devo ricreare tutta la tabella con imposta
If Leggi(lin_att:=pro_set.lin_att) Then
    imposta db:=db, lin_att:=pro_set.lin_att
End If

End Sub

