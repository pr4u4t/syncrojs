Attribute VB_Name = "com_lenze"
Option Explicit

Public cod_azi As Collection

Dim cont_lenze As Long

Public CAN_result As Integer
Public hClient As Byte
Public hNet As Byte
Public msg_can As TCANMsg
Public dwSendTime As Long
Public dwReceivetime As Long

Public Const CH1_TO_DRV = 1536            ' identifier nel caso di trasmissione sul canale 1
Public Const CH1_FROM_DRV = 1408      ' identifier nel caso di ricezione dal canale 1

Public Const CS_WRITE_REQUEST = "001"     ' command specifier nel caso di write request
Public Const CS_WRITE_RESPONSE = "011"  ' command specifier nel caso di write response
Public Const CS_READ_REQUEST = "010"      ' command specifier nel caso di read request
Public Const CS_READ_RESPONSE = "010"   ' command specifier nel caso di read response
Public Const ERROR_RESPONSE = "100"      ' command specifier nel caso di error response

Public Const Index = 24575                      ' costante per il calcolo dell'index LOW byte e dell'index HIGH byte

Enum Tip_len_data_bytes
    FORMATO_1_byte = 1
    FORMATO_2_byte = 2
    FORMATO_3_byte = 3
    FORMATO_4_byte = 4
End Enum

Enum Tip_tra_lenze
    send = 1
    receive = 2
End Enum

Enum tip_dat_com_azi
    reset = 1
    diagnostica = 2
    configurazione = 3
End Enum

Enum Tip_ind_lenze
    
    IND_SAN_ING = 1                  'indirizzo lenze SANDWICH INGRESSO
    IND_SAN_ING2 = 2                  'indirizzo lenze SANDWICH INGRESSO aggiuntivo nel caso di 3 livelli
    
    IND_SAN_USC_LIV_1 = 15          'indirizzo lenze SANDWICH USCITA del livello 1
    IND_SAN_USC_LIV_2 = 25          'indirizzo lenze SANDWICH USCITA del livello 2
    IND_SAN_USC_LIV_3 = 35          'indirizzo lenze SANDWICH USCITA del livello 3
    
    IND_AZ_1_LIV_1 = 11                  'indirizzo lenze AZ. MOTORE 1 del livello 1
    IND_AZ_2_LIV_1 = 12                  'indirizzo lenze AZ. MOTORE 2 del livello 1
    IND_AZ_3_LIV_1 = 13                  'indirizzo lenze AZ. MOTORE 3 del livello 1
    

    IND_AZ_1_LIV_2 = 21                  'indirizzo lenze AZ. MOTORE 1 del livello 2
    IND_AZ_2_LIV_2 = 22                  'indirizzo lenze AZ. MOTORE 2 del livello 2
    IND_AZ_3_LIV_2 = 23                  'indirizzo lenze AZ. MOTORE 3 del livello 2
    

    IND_AZ_1_LIV_3 = 31                  'indirizzo lenze AZ. MOTORE 1 del livello 3
    IND_AZ_2_LIV_3 = 32                  'indirizzo lenze AZ. MOTORE 2 del livello 3
    IND_AZ_3_LIV_3 = 33                  'indirizzo lenze AZ. MOTORE 3 del livello 3
    
End Enum

Public Function com_cod_err() As Boolean
'restituisce  true se il command code risulta essere ERROR RESPONSE
If msg_can.data(0) = 128 Then
    com_cod_err = True
Else
    com_cod_err = False
End If
End Function

Public Sub set_cod_azi()
'Allarmi per la diagnostica sugli azionamenti

Set cod_azi = New Collection

'PARAMETRO 183/0
codadd item1:="OK", key:="183/0": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Init", key:="183/101": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Trip", key:="183/102": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="RFG P-OFF", key:="183/103": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="IMP Message", key:="183/104": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Power off", key:="183/105": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="not rdy C135", key:="183/111": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="not rdy AIF", key:="183/112": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="not rdy CAN", key:="183/113": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="CINH term 28", key:="183/121": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="CINH int 1", key:="183/122": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="CINH int 2", key:="183/123": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="CINH C135/STP", key:="183/124": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="CINH AIF", key:="183/125": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="CINH CAN", key:="183/126": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Lock mode", key:="183/141": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="IMP", key:="183/142": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="QSP ext term", key:="183/151": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="QSP C135/STP", key:="183/152": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="QSP AIF", key:="183/153": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="QSP CAN", key:="183/154": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="DC break", key:="183/161": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="DC beark C135", key:="183/162": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="DC beark AIF", key:="183/163": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="DC beark CAN", key:="183/164": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Motor ident run", key:="183/170": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Warning", key:="183/250": inc_prg_bar_ini 'Arturo uff 25-07-02

'PARAMETRO 168/1
codadd item1:="OK", key:="168/0", item2:="No fail": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato corto circuito", key:="168/11", item2:="OC1 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato contatto della fase motore con la terra", key:="168/12", item2:="OC2 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="", key:="168/13", item2:="OC3 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato sovraccarico l x t", key:="168/15", item2:="OC5 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="", key:="168/22", item2:="LUQ trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato problema sulla fase del motore", key:="168/32", item2:="LP1 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="La temperatura e' piu' alta di quella impostata", key:="168/50", item2:="OH trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevata eccessiva temperatura motore", key:="168/53", item2:="OH3 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="La temp. e' piu' alta di quella impost. nel par. C121", key:="168/57", item2:="OH7 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="PTC ai terminals T1,T2 indica surriscaldam. mot.", key:="168/58", item2:="OH8 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore nella comunicazione", key:="168/61", item2:="CE0 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore comunicazione CAN_IN_1", key:="168/62", item2:="CE1 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore comunicazione CAN_IN_2", key:="168/63", item2:="CE2 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore comunicazione CAN_IN_3", key:="168/64", item2:="CE3 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato stato BUS_OFF", key:="168/65", item2:="CE4 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="", key:="168/70", item2:="U15 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore di sistema", key:="168/71", item2:="CCr trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato settaggio dei parametri errato", key:="168/72", item2:="PR1 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato settaggio dei parametri errato", key:="168/73", item2:="PR2 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore nella sequenza di programma", key:="168/74", item2:="PEr trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato settaggio dei parametri errato", key:="168/75", item2:="PR0 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato settaggio dei parametri errato", key:="168/77", item2:="PR3 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato settaggio dei parametri errato", key:="168/78", item2:="PR4 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Errore di inizializzazione dei parametri", key:="168/79", item2:="PI trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore sul resolver", key:="168/82", item2:="Sd2 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore sull'encoder: input X9 PIN 8", key:="168/83", item2:="Sd3 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevata alimentazione di corrente difettosa", key:="168/85", item2:="Sd5 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore sul sensore: input X7/X8", key:="168/86", item2:="Sd6 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore sull'encoder", key:="168/87", item2:="Sd7 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore esterno (TRIP-SET)", key:="168/91", item2:="EEr trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore interno", key:="168/105", item2:="H05 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Stadio di alimentazione non corretto", key:="168/107", item2:="H07 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Errore sul sensore di temperatura", key:="168/110", item2:="H10 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Errore sul sensore di temperatura interno", key:="168/111", item2:="H11 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="", key:="168/140", item2:="Id1 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="", key:="168/141", item2:="Id2 trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Eccesso sulla velocita' massima del sistema", key:="168/200", item2:="NMAX trip": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato basso voltaggio", key:="168/1030", item2:="LU message": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore esterno (TRIP-SET)", key:="168/1091", item2:="EEr message": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato sovraccarico l x t", key:="168/2015", item2:="OC5 warning": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato alto voltaggio", key:="168/2020", item2:="OU warning": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato problema sulla fase del motore", key:="168/2032", item2:="LP1 warning": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="La temp. e' piu' alta di quella impost. (par. C122)", key:="168/2054", item2:="OH4 warning": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="La temp. e' piu' alta di quella impost. (par. C121)", key:="168/2057", item2:="OH7 warning": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="PTC ai terminals T1,T2 indica surriscald. mot.", key:="168/2058", item2:="OH8 warning": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore nella comunicazione", key:="168/2061", item2:="CE0 warning": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore comunicazione CAN_IN_1", key:="168/2062", item2:="CE1 warning": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore comunicazione CAN_IN_2", key:="168/2063", item2:="CE2 warning": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore comunicazione CAN_IN_3", key:="168/2064", item2:="CE3 warning": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato stato BUS_OFF", key:="168/2065", item2:="CE4 warning": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore sul resolver", key:="168/2082", item2:="Sd2 warning": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore sull'encoder: input X9 PIN 8", key:="168/2083", item2:="Sd3 warning": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevata alimentazione di corrente difettosa", key:="168/2085", item2:="Sd5 warning": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore sul sensore: input X7/X8", key:="168/2086", item2:="Sd6 warning": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore sull'encoder", key:="168/2087", item2:="Sd7 warning": inc_prg_bar_ini 'Arturo uff 25-07-02
codadd item1:="Rilevato errore esterno (TRIP-SET)", key:="168/2091", item2:="EEr warning": inc_prg_bar_ini 'Arturo uff 25-07-02

'codadd item1:="Differenza di fase o frequenza di ingresso fuori range", key:="???", item2:="P03": inc_prg_bar_ini 'Arturo uff 25-07-02
'codadd item1:="Superati i limiti nella fase o nella frequenza di ingresso", key:="???", item2:="P13": inc_prg_bar_ini 'Arturo uff 25-07-02


End Sub

Function cod_des(key As String, Optional Log As Boolean) As String
'log deve essere settato a False per disabilitare la scrittura del file di Log
On Local Error GoTo Ges_Log
  cod_des = cod_azi.item(key).item1
  Exit Function
Ges_Log:
  If IsMissing(Log) Or Log = True Then
    log_str "cod_des: Impossibile trovare il codice errore azionamento <" + key + "> nella collezione", MED_PRIORITY, log_azi
  End If
End Function
Function cod_dis(key As String, Optional Log As Boolean) As String
'log deve essere settato a False per disabilitare la scrittura del file di Log
On Local Error GoTo Ges_Log
  cod_dis = cod_azi.item(key).item2
  Exit Function
Ges_Log:
  If IsMissing(Log) Or Log = True Then
    log_str "cod_dis: Impossibile trovare il codice errore azionamento <" + key + "> nella collezione", MED_PRIORITY, log_azi
  End If
End Function
Function snd_par_rul_ing(AZ_SAN_ING As Tip_ind_lenze, tip_dat As tip_dat_com_azi) As cod_ret
'trasmissione parametri rullo di ingresso
Dim i As Long
Dim str As String
Dim szbuff As String * 100
Dim Index As Long
Dim subindex As Long
Dim data As Long
Dim str1 As String
Dim dat_reg_rif_ing As Double
snd_par_rul_ing = RET_OK
Select Case tip_dat

  Case configurazione ' caso di trasmissione dati di configurazione per gli azionamenti
    ' Parametro regolazione riferimento d'ingresso
    dat_reg_rif_ing = (val(par_mac_ute.rul_ing.vel_max_lin) / 385) * 100
    str = pre_str_lenze(send, AZ_SAN_ING, 27, 1, FORMATO_4_byte, dat_reg_rif_ing)
    rie_msg_can (str)
    'Arturo uff 12-11-02 (5)
    log_str "snd_par_rul_ing: str = '" + str + "'", MED_PRIORITY, log_can
    CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
    cont_lenze = GetTickCount
    If CAN_result <> CAN_ERR_OK Then
        CAN_result = CAN_GetErrText(CAN_result, szbuff)
        log_str "snd_par_rul_ing. Invio parametri di configurazione. CAN_Write sull'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & " " & szbuff, MED_PRIORITY, log_can
        snd_par_rul_ing = RET_BAD
        Exit Function
    Else
        CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + AZ_SAN_ING)
        If CAN_result <> CAN_ERR_OK Then
          CAN_result = CAN_GetErrText(CAN_result, szbuff)
          log_str "snd_par_rul_ing. Invio parametri di configurazione. CAN_RegisterMsg sull'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & " " & szbuff, MED_PRIORITY, log_can
          snd_par_rul_ing = RET_BAD
          Exit Function
        End If
        CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
          CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        Wend
        If CAN_result = CAN_ERR_QRCVEMPTY Then
          log_str "snd_par_rul_ing. Invio parametri di configurazione. Timeout nella comunicazione con l'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING, MED_PRIORITY, log_can
          snd_par_rul_ing = RET_BAD
          Exit Function
        Else
          par_mac.rul_ing.vel_max_lin = par_mac_ute.rul_ing.vel_max_lin
        End If
    End If
    
    ' Parametro offset di velocità in m/min
    str = pre_str_lenze(send, AZ_SAN_ING, 475, 1, FORMATO_4_byte, val(par_mac_ute.rul_ing.rul_num(AZ_SAN_ING).off_vel_met_min))
    rie_msg_can (str)
    CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
    cont_lenze = GetTickCount
    If CAN_result <> CAN_ERR_OK Then
        CAN_result = CAN_GetErrText(CAN_result, szbuff)
        log_str "snd_par_rul_ing. Invio parametri di configurazione. CAN_Write sull'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & " " & szbuff, MED_PRIORITY, log_can
        snd_par_rul_ing = RET_BAD
        Exit Function
    Else
        CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + AZ_SAN_ING)
        If CAN_result <> CAN_ERR_OK Then
          CAN_result = CAN_GetErrText(CAN_result, szbuff)
          log_str "snd_par_rul_ing. Invio parametri di configurazione. CAN_RegisterMsg sull'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & " " & szbuff, MED_PRIORITY, log_can
          snd_par_rul_ing = RET_BAD
          Exit Function
        End If
        CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
          CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        Wend
        If CAN_result = CAN_ERR_QRCVEMPTY Then
          log_str "snd_par_rul_ing. Invio parametri di configurazione. Timeout nella comunicazione con l'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING, MED_PRIORITY, log_can
          snd_par_rul_ing = RET_BAD
          Exit Function
        Else
          par_mac.rul_ing.rul_num(AZ_SAN_ING).off_vel_met_min = par_mac_ute.rul_ing.rul_num(AZ_SAN_ING).off_vel_met_min
        End If
    End If
    
    ' i=1 offset di velocità in percentuale, i=2 sovravelocità allo sfilamento, i =3 distanza tra Rotary Shear e Master
    For i = 1 To 3
        
        Select Case i
            Case 1
              data = val(par_mac_ute.rul_ing.rul_num(AZ_SAN_ING).off_vel_per)
            Case 2
              data = val(par_mac_ute.rul_ing.rul_num(AZ_SAN_ING).sov_sfi)
            Case 3
              data = val(par_mac_ute.rul_ing.rul_num(AZ_SAN_ING).dis_rot_mas)
        End Select
        
        str = pre_str_lenze(send, AZ_SAN_ING, 473, i, FORMATO_4_byte, data)
        rie_msg_can (str)
        CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
        cont_lenze = GetTickCount
        If CAN_result <> CAN_ERR_OK Then
            CAN_result = CAN_GetErrText(CAN_result, szbuff)
            log_str "snd_par_rul_ing. Invio parametri di configurazione. CAN_Write sull'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & " " & szbuff, MED_PRIORITY, log_can
            snd_par_rul_ing = RET_BAD
            Exit Function
        Else
            CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + AZ_SAN_ING)
            If CAN_result <> CAN_ERR_OK Then
              CAN_result = CAN_GetErrText(CAN_result, szbuff)
              log_str "snd_par_rul_ing. Invio parametri di configurazione. CAN_RegisterMsg sull'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & " " & szbuff, MED_PRIORITY, log_can
              snd_par_rul_ing = RET_BAD
              Exit Function
            End If
            CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
            While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
              CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
            Wend
            If CAN_result = CAN_ERR_QRCVEMPTY Then
              log_str "snd_par_rul_ing. Invio parametri di configurazione. Timeout nella comunicazione con l'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING, MED_PRIORITY, log_can
              snd_par_rul_ing = RET_BAD
              Exit Function
            Else
              Select Case i
                Case 1
                  par_mac.rul_ing.rul_num(AZ_SAN_ING).off_vel_per = par_mac_ute.rul_ing.rul_num(AZ_SAN_ING).off_vel_per
                Case 2
                  par_mac.rul_ing.rul_num(AZ_SAN_ING).sov_sfi = par_mac_ute.rul_ing.rul_num(AZ_SAN_ING).sov_sfi
                Case 3
                  par_mac.rul_ing.rul_num(AZ_SAN_ING).dis_rot_mas = par_mac_ute.rul_ing.rul_num(AZ_SAN_ING).dis_rot_mas
              End Select
            End If
        End If
    Next i
    
    ' Salvataggio Permanente dei parametri trasmessi
    str = pre_str_lenze(send, AZ_SAN_ING, 3, 0, FORMATO_4_byte, "1")
    rie_msg_can (str)
    CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
    cont_lenze = GetTickCount
    If CAN_result <> CAN_ERR_OK Then
        CAN_result = CAN_GetErrText(CAN_result, szbuff)
        log_str "snd_par_rul_ing. Invio parametri di configurazione. CAN_Write sull'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & " " & szbuff, MED_PRIORITY, log_can
        snd_par_rul_ing = RET_BAD
        Exit Function
    Else
        CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + AZ_SAN_ING)
        If CAN_result <> CAN_ERR_OK Then
          CAN_result = CAN_GetErrText(CAN_result, szbuff)
          log_str "snd_par_rul_ing. Invio parametri di configurazione. CAN_RegisterMsg sull'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & " " & szbuff, MED_PRIORITY, log_can
          snd_par_rul_ing = RET_BAD
          Exit Function
        End If
        CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
          CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        Wend
        If CAN_result = CAN_ERR_QRCVEMPTY Then
          log_str "snd_par_rul_ing. Invio parametri di configurazione. Timeout nella comunicazione con l'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING, MED_PRIORITY, log_can
          snd_par_rul_ing = RET_BAD
          Exit Function
        End If
    End If
    
  Case reset  ' caso di trasmissione comando di reset per gli azionamenti
    str = pre_str_lenze(send, AZ_SAN_ING, 358, 0, FORMATO_4_byte, 1)
    rie_msg_can (str)
    CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
    cont_lenze = GetTickCount
    If CAN_result <> CAN_ERR_OK Then
        CAN_result = CAN_GetErrText(CAN_result, szbuff)
        log_str "snd_par_rul_ing. Invio comando di reset. CAN_Write sull'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & " " & szbuff, MED_PRIORITY, log_can
        snd_par_rul_ing = RET_BAD
        Exit Function
    Else
        CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + AZ_SAN_ING)
        If CAN_result <> CAN_ERR_OK Then
          CAN_result = CAN_GetErrText(CAN_result, szbuff)
          log_str "snd_par_rul_ing. Invio comando di reset. CAN_RegisterMsg sull'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & " " & szbuff, MED_PRIORITY, log_can
          snd_par_rul_ing = RET_BAD
          Exit Function
        End If
        CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
          CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        Wend
        If CAN_result = CAN_ERR_QRCVEMPTY Then
          log_str "snd_par_rul_ing. Invio comando di reset. Timeout nella comunicazione con l'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING, MED_PRIORITY, log_can
          snd_par_rul_ing = RET_BAD
          Exit Function
        End If
    End If
    
  Case diagnostica  ' caso di trasmissione richiesta parametri di diagnostica
    str = pre_str_lenze(receive, AZ_SAN_ING, 183, 0, FORMATO_4_byte)   ' STATO AZIONAMENTI PAR. 183
    rie_msg_can (str)
    CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
    cont_lenze = GetTickCount
    If CAN_result <> CAN_ERR_OK Then
        CAN_result = CAN_GetErrText(CAN_result, szbuff)
        snd_par_rul_ing = RET_BAD
        log_str "snd_par_rul_ing. Invio comando di richiesta parametri di diagnostica. CAN_Write sull'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & szbuff, MED_PRIORITY, log_can
        Exit Function
    Else
        CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + AZ_SAN_ING)
        If CAN_result <> CAN_ERR_OK Then
            CAN_result = CAN_GetErrText(CAN_result, szbuff)
            snd_par_rul_ing = RET_BAD
            log_str "snd_par_rul_ing. Invio comando di richiesta parametri di diagnostica. CAN_RegisterMsg sull'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & szbuff, MED_PRIORITY, log_can
            Exit Function
        End If
        CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
            CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        Wend
        If CAN_result = CAN_ERR_QRCVEMPTY Then
            snd_par_rul_ing = RET_BAD
            log_str "snd_par_rul_ing. Invio comando di richiesta parametri di diagnostica. Timeout nella comunicazione con l'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & szbuff, MED_PRIORITY, log_can
            Exit Function
        Else
          str1 = tra_msg(False)
          par_mac.par_azi_ing(AZ_SAN_ING).sta = str1
        End If
    End If
    str = pre_str_lenze(receive, AZ_SAN_ING, 168, 1, FORMATO_4_byte)      ' FAIL N ° ACT. PAR. 168
    rie_msg_can (str)
    CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
    cont_lenze = GetTickCount
    If CAN_result <> CAN_ERR_OK Then
        CAN_result = CAN_GetErrText(CAN_result, szbuff)
        snd_par_rul_ing = RET_BAD
        log_str "snd_par_rul_ing. Invio comando di richiesta parametri di diagnostica. CAN_Write sull'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & szbuff, MED_PRIORITY, log_can
        Exit Function
    Else
        CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + AZ_SAN_ING)
        If CAN_result <> CAN_ERR_OK Then
            CAN_result = CAN_GetErrText(CAN_result, szbuff)
            snd_par_rul_ing = RET_BAD
            log_str "snd_par_rul_ing. Invio comando di richiesta parametri di diagnostica. CAN_RegisterMsg sull'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & szbuff, MED_PRIORITY, log_can
            Exit Function
        End If
        CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
            CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        Wend
        If CAN_result = CAN_ERR_QRCVEMPTY Then
            snd_par_rul_ing = RET_BAD
            log_str "snd_par_rul_ing. Invio comando di richiesta parametri di diagnostica. Timeout nella comunicazione con l'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & szbuff, MED_PRIORITY, log_can
            Exit Function
        Else
          str1 = tra_msg(False)
          par_mac.par_azi_ing(AZ_SAN_ING).cod_err = str1
        End If
    End If
    If par_nuo.get_index > 0 And par_nuo.get_subindex >= 0 Then
        Index = par_nuo.get_index
        subindex = par_nuo.get_subindex
        str = pre_str_lenze(receive, AZ_SAN_ING, Index, subindex, FORMATO_4_byte)    ' PARAMETRO IMPOSTABILE
        rie_msg_can (str)
        CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
        cont_lenze = GetTickCount
        If CAN_result <> CAN_ERR_OK Then
            CAN_result = CAN_GetErrText(CAN_result, szbuff)
            snd_par_rul_ing = RET_BAD
            log_str "snd_par_rul_ing. Invio comando di richiesta parametri di diagnostica. CAN_Write sull'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & szbuff, MED_PRIORITY, log_can
            Exit Function
        Else
            CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + AZ_SAN_ING)
            If CAN_result <> CAN_ERR_OK Then
                CAN_result = CAN_GetErrText(CAN_result, szbuff)
                snd_par_rul_ing = RET_BAD
                log_str "snd_par_rul_ing. Invio comando di richiesta parametri di diagnostica. CAN_RegisterMsg sull'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & szbuff, MED_PRIORITY, log_can
                Exit Function
            End If
            CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
            While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
                CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
            Wend
            If CAN_result = CAN_ERR_QRCVEMPTY Then
                snd_par_rul_ing = RET_BAD
                log_str "snd_par_rul_ing. Invio comando di richiesta parametri di diagnostica. Timeout nella comunicazione con l'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & szbuff, MED_PRIORITY, log_can
                Exit Function
            Else
                If com_cod_err Then
                    If par_nuo.par_pre Or imp_nuo_par Then
                        imp_nuo_par = False
                        messaggio msg("par_non_pre"), True, False
                        par_nuo.change_par_new 0, 0
                    End If
                    par_nuo.par_pre = False
                    log_str "snd_par_rul_ing. Invio comando di richiesta parametri di diagnostica. Parametro C " & Index & "/" & subindex & " non presente per l'azionamento del rullo di ingresso di indirizzo " & AZ_SAN_ING & szbuff, MED_PRIORITY, log_can
                Else
                    par_nuo.par_pre = True
                    str1 = tra_msg(True)
                    par_mac.par_azi_ing(AZ_SAN_ING).par_new = str1
                End If
            End If
        End If
    End If
End Select
End Function

Function snd_par_rul_usc(liv As Integer, tip_dat As tip_dat_com_azi) As cod_ret
'trasmissione parametri rulli di uscita

Dim i As Integer
Dim str As String
Dim szbuff As String * 100
Dim Index As Long
Dim subindex As Long
Dim str1 As String
Dim dato As String

Select Case tip_dat

  Case configurazione ' caso di trasmissione dati di configurazione per gli azionamenti
    ' i = 1 lunghezza sandwich, i = 2 gap tagli corti, i = 3 gap tagli lunghi, i = 4 tempo apertura flap, i = 5 distanza tra lame e flap
    subindex = 0
    For i = 1 To 5 ' mando tutti i parametri (5) all'azionamento del sandwich di uscita del livello liv
        If i <= 3 Then
            subindex = i
        Else
            subindex = i + 4
        End If
        Select Case subindex
              Case 1
                  dato = par_mac_ute.rul_usc.rul_usc_liv(liv).lun_san
              Case 2
                  dato = par_mac_ute.rul_usc.rul_usc_liv(liv).gap_tgl_cor
              Case 3
                  dato = par_mac_ute.rul_usc.rul_usc_liv(liv).gap_tgl_lun
              Case 8
                  'Arturo uff 07-03-03 al lenze si invia il tempo di chiusura
                  'dato = par_mac_ute.rul_usc.rul_usc_liv(liv).tem_ape_fla
                  dato = par_mac_ute.rul_usc.rul_usc_liv(liv).tem_chi_fla
              Case 9
                  dato = par_mac_ute.rul_usc.rul_usc_liv(liv).dis_lam_fla
          End Select
        
        str = pre_str_lenze(send, IND_SAN_USC_LIV_1 + (liv - 1) * 10, 473, subindex, FORMATO_4_byte, val(dato))
        rie_msg_can (str)
        CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
        cont_lenze = GetTickCount
        If CAN_result <> CAN_ERR_OK Then
            CAN_result = CAN_GetErrText(CAN_result, szbuff)
            log_str "snd_par_rul_usc. Invio parametri di configurazione. CAN_Write sull'azionamento del sandwich di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10 & " " & szbuff, MED_PRIORITY, log_can
            snd_par_rul_usc = RET_BAD
            Exit Function
        Else
            CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + IND_SAN_USC_LIV_1 + (liv - 1) * 10)
            If CAN_result <> CAN_ERR_OK Then
              CAN_result = CAN_GetErrText(CAN_result, szbuff)
              log_str "snd_par_rul_usc. Invio parametri di configurazione. CAN_RegisterMsg sull'azionamento del sandwich di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10 & " " & szbuff, MED_PRIORITY, log_can
              snd_par_rul_usc = RET_BAD
              Exit Function
            End If
            CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
            While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
              CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
            Wend
            If CAN_result = CAN_ERR_QRCVEMPTY Then
              log_str "snd_par_rul_usc. Invio parametri di configurazione. Timeout nella comunicazione con l'azionamento del sandwich di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10, MED_PRIORITY, log_can
              snd_par_rul_usc = RET_BAD
              Exit Function
            Else
              Select Case subindex
                  Case 1
                      par_mac.rul_usc.rul_usc_liv(liv).lun_san = par_mac_ute.rul_usc.rul_usc_liv(liv).lun_san
                  Case 2
                      par_mac.rul_usc.rul_usc_liv(liv).gap_tgl_cor = par_mac_ute.rul_usc.rul_usc_liv(liv).gap_tgl_cor
                  Case 3
                      par_mac.rul_usc.rul_usc_liv(liv).gap_tgl_lun = par_mac_ute.rul_usc.rul_usc_liv(liv).gap_tgl_lun
                  Case 8
                      'Arturo uff 07-03-03 al lenze si invia il tempo di chiusura
                      'par_mac.rul_usc.rul_usc_liv(liv).tem_ape_fla = par_mac_ute.rul_usc.rul_usc_liv(liv).tem_ape_fla
                      par_mac.rul_usc.rul_usc_liv(liv).tem_chi_fla = par_mac_ute.rul_usc.rul_usc_liv(liv).tem_chi_fla
                  Case 9
                      par_mac.rul_usc.rul_usc_liv(liv).dis_lam_fla = par_mac_ute.rul_usc.rul_usc_liv(liv).dis_lam_fla
              End Select
            End If
        End If
    Next i
    
    ' Salvataggio Permanente dei parametri trasmessi
    str = pre_str_lenze(send, IND_SAN_USC_LIV_1 + (liv - 1) * 10, 3, 0, FORMATO_4_byte, "1")
    rie_msg_can (str)
    CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
    cont_lenze = GetTickCount
    If CAN_result <> CAN_ERR_OK Then
        CAN_result = CAN_GetErrText(CAN_result, szbuff)
        log_str "snd_par_rul_usc. Invio parametri di configurazione. CAN_Write sull'azionamento del sandwich di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10 & " " & szbuff, MED_PRIORITY, log_can
        snd_par_rul_usc = RET_BAD
        Exit Function
    Else
        CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + IND_SAN_USC_LIV_1 + (liv - 1) * 10)
        If CAN_result <> CAN_ERR_OK Then
          CAN_result = CAN_GetErrText(CAN_result, szbuff)
          log_str "snd_par_rul_usc. Invio parametri di configurazione. CAN_RegisterMsg sull'azionamento del sandwich di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10 & " " & szbuff, MED_PRIORITY, log_can
          snd_par_rul_usc = RET_BAD
          Exit Function
        End If
        CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
          CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        Wend
        If CAN_result = CAN_ERR_QRCVEMPTY Then
          log_str "snd_par_rul_usc. Invio parametri di configurazione. Timeout nella comunicazione con l'azionamento del sandwich di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10, MED_PRIORITY, log_can
          snd_par_rul_usc = RET_BAD
          Exit Function
        End If
    End If
    
  Case reset  ' caso di trasmissione comando di reset per gli azionamenti
    CAN_result = CAN_ResetHardware(HAR_CAN_DONGLE)
    str = pre_str_lenze(send, IND_SAN_USC_LIV_1 + (liv - 1) * 10, 358, 0, FORMATO_4_byte, 1)
    rie_msg_can (str)
    CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
    cont_lenze = GetTickCount
    If CAN_result <> CAN_ERR_OK Then
        CAN_result = CAN_GetErrText(CAN_result, szbuff)
        log_str "snd_par_rul_usc. Invio comando di reset. CAN_Write sull'azionamento del rullo di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10 & " " & szbuff, MED_PRIORITY, log_can
        snd_par_rul_usc = RET_BAD
        Exit Function
    Else
        CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + IND_SAN_USC_LIV_1 + (liv - 1) * 10)
        If CAN_result <> CAN_ERR_OK Then
          CAN_result = CAN_GetErrText(CAN_result, szbuff)
          log_str "snd_par_rul_usc. Invio comando di reset. CAN_RegisterMsg sull'azionamento del rullo di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10 & " " & szbuff, MED_PRIORITY, log_can
          snd_par_rul_usc = RET_BAD
          Exit Function
        End If
        CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
          CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        Wend
        If CAN_result = CAN_ERR_QRCVEMPTY Then
          log_str "snd_par_rul_usc. Invio comando di reset. Timeout nella comunicazione con l'azionamento del rullo di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10, MED_PRIORITY, log_can
          snd_par_rul_usc = RET_BAD
          Exit Function
        End If
    End If
  
  Case diagnostica  ' caso di trasmissione richiesta parametri di diagnostica
    str = pre_str_lenze(receive, IND_SAN_USC_LIV_1 + (liv - 1) * 10, 183, 0, FORMATO_4_byte)   ' STATO AZIONAMENTI PAR. 183
    rie_msg_can (str)
    CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
    cont_lenze = GetTickCount
    If CAN_result <> CAN_ERR_OK Then
        CAN_result = CAN_GetErrText(CAN_result, szbuff)
        snd_par_rul_usc = RET_BAD
        log_str "snd_par_rul_usc. Invio comando di richiesta parametri di diagnostica. CAN_Write sull'azionamento del rullo di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10 & szbuff, MED_PRIORITY, log_can
        Exit Function
    Else
        CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + (IND_SAN_USC_LIV_1 + (liv - 1) * 10))
        If CAN_result <> CAN_ERR_OK Then
            CAN_result = CAN_GetErrText(CAN_result, szbuff)
            snd_par_rul_usc = RET_BAD
            log_str "snd_par_rul_usc. Invio comando di richiesta parametri di diagnostica. CAN_RegisterMsg sull'azionamento del rullo di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10 & szbuff, MED_PRIORITY, log_can
            Exit Function
        End If
        CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
            CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        Wend
        If CAN_result = CAN_ERR_QRCVEMPTY Then
            snd_par_rul_usc = RET_BAD
            log_str "snd_par_rul_usc. Invio comando di richiesta parametri di diagnostica. Timeout nella comunicazione con l'azionamento del rullo di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10 & szbuff, MED_PRIORITY, log_can
            Exit Function
        Else
          str1 = tra_msg(False)
          par_mac.par_azi_usc(liv).sta = str1
        End If
    End If
    str = pre_str_lenze(receive, IND_SAN_USC_LIV_1 + (liv - 1) * 10, 168, 1, FORMATO_4_byte)  ' FAIL N° ACT. PAR. 168
    rie_msg_can (str)
    CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
    cont_lenze = GetTickCount
    If CAN_result <> CAN_ERR_OK Then
        CAN_result = CAN_GetErrText(CAN_result, szbuff)
        snd_par_rul_usc = RET_BAD
        log_str "snd_par_rul_usc. Invio comando di richiesta parametri di diagnostica. CAN_Write sull'azionamento del rullo di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10 & szbuff, MED_PRIORITY, log_can
        Exit Function
    Else
        CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + (IND_SAN_USC_LIV_1 + (liv - 1) * 10))
        If CAN_result <> CAN_ERR_OK Then
            CAN_result = CAN_GetErrText(CAN_result, szbuff)
            snd_par_rul_usc = RET_BAD
            log_str "snd_par_rul_usc. Invio comando di richiesta parametri di diagnostica. CAN_RegisterMsg sull'azionamento del rullo di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10 & szbuff, MED_PRIORITY, log_can
            Exit Function
        End If
        CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
            CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        Wend
        If CAN_result = CAN_ERR_QRCVEMPTY Then
            snd_par_rul_usc = RET_BAD
            log_str "snd_par_rul_usc. Invio comando di richiesta parametri di diagnostica. Timeout nella comunicazione con l'azionamento del rullo di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10 & szbuff, MED_PRIORITY, log_can
            Exit Function
        Else
          str1 = tra_msg(False)
          par_mac.par_azi_usc(liv).cod_err = str1
        End If
    End If
    If par_nuo.get_index > 0 And par_nuo.get_subindex >= 0 Then
        Index = par_nuo.get_index
        subindex = par_nuo.get_subindex
        str = pre_str_lenze(receive, IND_SAN_USC_LIV_1 + (liv - 1) * 10, Index, subindex, FORMATO_4_byte)  ' PARAMETRO IMPOSTABILE
        rie_msg_can (str)
        CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
        cont_lenze = GetTickCount
        If CAN_result <> CAN_ERR_OK Then
            CAN_result = CAN_GetErrText(CAN_result, szbuff)
            snd_par_rul_usc = RET_BAD
            log_str "snd_par_rul_usc. Invio comando di richiesta parametri di diagnostica. CAN_Write sull'azionamento del rullo di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10 & szbuff, MED_PRIORITY, log_can
            Exit Function
        Else
            CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + (IND_SAN_USC_LIV_1 + (liv - 1) * 10))
            If CAN_result <> CAN_ERR_OK Then
                CAN_result = CAN_GetErrText(CAN_result, szbuff)
                snd_par_rul_usc = RET_BAD
                log_str "snd_par_rul_usc. Invio comando di richiesta parametri di diagnostica. CAN_RegisterMsg sull'azionamento del rullo di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10 & szbuff, MED_PRIORITY, log_can
                Exit Function
            End If
            CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
            While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
                CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
            Wend
            If CAN_result = CAN_ERR_QRCVEMPTY Then
                snd_par_rul_usc = RET_BAD
                log_str "snd_par_rul_usc. Invio comando di richiesta parametri di diagnostica. Timeout nella comunicazione con l'azionamento del rullo di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10 & szbuff, MED_PRIORITY, log_can
                Exit Function
            Else
                If com_cod_err Then
                    If par_nuo.par_pre Or imp_nuo_par Then
                        imp_nuo_par = False
                        messaggio msg("par_non_pre"), True, False
                        par_nuo.change_par_new 0, 0
                    End If
                    par_nuo.par_pre = False
                    log_str "snd_par_rul_usc. Invio comando di richiesta parametri di diagnostica. Parametro C " & Index & "/" & subindex & " non presente per l'azionamento del rullo di uscita di indirizzo " & IND_SAN_USC_LIV_1 + (liv - 1) * 10 & szbuff, MED_PRIORITY, log_can
                Else
                    par_nuo.par_pre = True
                    str1 = tra_msg(True)
                    par_mac.par_azi_usc(liv).par_new = str1
                End If
            End If
        End If
    End If
End Select
End Function

Function snd_par_rul_tag(liv As Integer, mot As Integer, tip_dat As tip_dat_com_azi) As cod_ret
'trasmissione parametri rulli di uscita
Dim i As Integer
Dim str As String
Dim szbuff As String * 100
Dim sub_index As Integer
Dim str1 As String
Dim ind As Tip_ind_lenze
Dim Index As Long
Dim subindex As Long

Select Case liv
  Case 1
    ind = IND_AZ_1_LIV_1
  Case 2
    ind = IND_AZ_1_LIV_2
  Case 3
    ind = IND_AZ_1_LIV_3
End Select
ind = ind + (mot - 1)
Select Case tip_dat
  Case reset  ' caso di trasmissione comando di reset per gli azionamenti
    CAN_result = CAN_ResetHardware(HAR_CAN_DONGLE)
    str = pre_str_lenze(send, ind, 358, 0, FORMATO_4_byte, 1)
    rie_msg_can (str)
    CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
    cont_lenze = GetTickCount
    If CAN_result <> CAN_ERR_OK Then
        CAN_result = CAN_GetErrText(CAN_result, szbuff)
        log_str "snd_par_rul_tag. Invio comando di reset. CAN_Write sull'azionamento del rullo di taglio di indirizzo " & ind & " " & szbuff, MED_PRIORITY, log_can
        snd_par_rul_tag = RET_BAD
        Exit Function
    Else
        CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + ind)
        If CAN_result <> CAN_ERR_OK Then
          CAN_result = CAN_GetErrText(CAN_result, szbuff)
          log_str "snd_par_rul_tag. Invio comando di reset. CAN_RegisterMsg sull'azionamento del rullo di taglio di indirizzo " & ind & " " & szbuff, MED_PRIORITY, log_can
          snd_par_rul_tag = RET_BAD
          Exit Function
        End If
        CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
          CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        Wend
        If CAN_result = CAN_ERR_QRCVEMPTY Then
          log_str "snd_par_rul_tag. Invio comando di reset. Timeout nella comunicazione con l'azionamento del rullo di taglio di indirizzo " & ind, MED_PRIORITY, log_can
          snd_par_rul_tag = RET_BAD
          Exit Function
        End If
    End If

  Case diagnostica  ' caso di trasmissione richiesta parametri di diagnostica
    str = pre_str_lenze(receive, ind, 183, 0, FORMATO_4_byte)   ' STATO AZIONAMENTI PAR. 183
    rie_msg_can (str)
    CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
    cont_lenze = GetTickCount
    If CAN_result <> CAN_ERR_OK Then
        CAN_result = CAN_GetErrText(CAN_result, szbuff)
        snd_par_rul_tag = RET_BAD
        log_str "snd_par_rul_tag. Invio comando di richiesta parametri di diagnostica. CAN_Write sull'azionamento del rullo di taglio di indirizzo " & ind & szbuff, MED_PRIORITY, log_can
        Exit Function
    Else
        CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + ind)
        If CAN_result <> CAN_ERR_OK Then
            CAN_result = CAN_GetErrText(CAN_result, szbuff)
            snd_par_rul_tag = RET_BAD
            log_str "snd_par_rul_tag. Invio comando di richiesta parametri di diagnostica. CAN_RegisterMsg sull'azionamento del rullo di taglio di indirizzo " & ind & szbuff, MED_PRIORITY, log_can
            Exit Function
        End If
        CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
            CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        Wend
        If CAN_result = CAN_ERR_QRCVEMPTY Then
            snd_par_rul_tag = RET_BAD
            log_str "snd_par_rul_tag. Invio comando di richiesta parametri di diagnostica. Timeout nella comunicazione con l'azionamento del rullo di taglio di indirizzo " & ind & szbuff, MED_PRIORITY, log_can
            Exit Function
        Else
          str1 = tra_msg(False)
          par_mac.par_azi_tag(liv).mot(mot).sta = str1
        End If
    End If
    str = pre_str_lenze(receive, ind, 168, 1, FORMATO_4_byte)   ' FAIL N° ACT. PAR. 168
    rie_msg_can (str)
    CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
    cont_lenze = GetTickCount
    If CAN_result <> CAN_ERR_OK Then
        CAN_result = CAN_GetErrText(CAN_result, szbuff)
        snd_par_rul_tag = RET_BAD
        log_str "snd_par_rul_tag. Invio comando di richiesta parametri di diagnostica. CAN_Write sull'azionamento del rullo di taglio di indirizzo " & ind & szbuff, MED_PRIORITY, log_can
        Exit Function
    Else
        CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + ind)
        If CAN_result <> CAN_ERR_OK Then
            CAN_result = CAN_GetErrText(CAN_result, szbuff)
            snd_par_rul_tag = RET_BAD
            log_str "snd_par_rul_tag. Invio comando di richiesta parametri di diagnostica. CAN_RegisterMsg sull'azionamento del rullo di taglio di indirizzo " & ind & szbuff, MED_PRIORITY, log_can
            Exit Function
        End If
        CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
            CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
        Wend
        If CAN_result = CAN_ERR_QRCVEMPTY Then
            snd_par_rul_tag = RET_BAD
            log_str "snd_par_rul_tag. Invio comando di richiesta parametri di diagnostica. Timeout nella comunicazione con l'azionamento del rullo di taglio di indirizzo " & ind & szbuff, MED_PRIORITY, log_can
            Exit Function
        Else
          str1 = tra_msg(False)
          par_mac.par_azi_tag(liv).mot(mot).cod_err = str1
        End If
    End If
    If par_nuo.get_index > 0 And par_nuo.get_subindex >= 0 Then
        Index = par_nuo.get_index
        subindex = par_nuo.get_subindex
        str = pre_str_lenze(receive, ind, Index, subindex, FORMATO_4_byte)   ' PARAMETRO IMPOSTABILE
        rie_msg_can (str)
        CAN_result = CAN_Write(hClient, hNet, msg_can, dwSendTime)
        cont_lenze = GetTickCount
        If CAN_result <> CAN_ERR_OK Then
            CAN_result = CAN_GetErrText(CAN_result, szbuff)
            snd_par_rul_tag = RET_BAD
            log_str "snd_par_rul_tag. Invio comando di richiesta parametri di diagnostica. CAN_Write sull'azionamento del rullo di taglio di indirizzo " & ind & szbuff, MED_PRIORITY, log_can
            Exit Function
        Else
            CAN_result = CAN_RegisterMsg(hClient, hNet, CH1_FROM_DRV + ind)
            If CAN_result <> CAN_ERR_OK Then
                CAN_result = CAN_GetErrText(CAN_result, szbuff)
                snd_par_rul_tag = RET_BAD
                log_str "snd_par_rul_tag. Invio comando di richiesta parametri di diagnostica. CAN_RegisterMsg sull'azionamento del rullo di taglio di indirizzo " & ind & szbuff, MED_PRIORITY, log_can
                Exit Function
            End If
            CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
            While (GetTickCount - cont_lenze < val(par_mac.com_azi.timeout)) And (CAN_result = CAN_ERR_QRCVEMPTY)
                CAN_result = CAN_Read(hClient, msg_can, hNet, dwReceivetime)
            Wend
            If CAN_result = CAN_ERR_QRCVEMPTY Then
                snd_par_rul_tag = RET_BAD
                log_str "snd_par_rul_tag. Invio comando di richiesta parametri di diagnostica. Timeout nella comunicazione con l'azionamento del rullo di taglio di indirizzo " & ind & szbuff, MED_PRIORITY, log_can
                Exit Function
            Else
                If com_cod_err Then
                    If par_nuo.par_pre Or imp_nuo_par Then
                          imp_nuo_par = False
                          messaggio msg("par_non_pre"), True, False
                          par_nuo.change_par_new 0, 0
                    End If
                    par_nuo.par_pre = False
                    log_str "snd_par_rul_tag. Invio comando di richiesta parametri di diagnostica. Parametro C " & Index & "/" & subindex & " non presente per l'azionamento del rullo di taglio di indirizzo " & ind & szbuff, MED_PRIORITY, log_can
                Else
                    par_nuo.par_pre = True
                    str1 = tra_msg(True)
                    par_mac.par_azi_tag(liv).mot(mot).par_new = str1
                End If
            End If
        End If
    End If
End Select
End Function


Public Function pre_str_lenze(tip_tra As Tip_tra_lenze, ind_lenze As Tip_ind_lenze, index_par_lenze As Long, subindex_par_lenze As Long, len_data_byte As Tip_len_data_bytes, Optional data As Variant) As String
Dim str As String
Dim com_cod As String
Dim app As String
Dim ind_l_b As String
Dim ind_h_b As String
Dim i As Integer

str = Right$(int_esa(CH1_TO_DRV + ind_lenze), 3)
If tip_tra = send Then
    com_cod = CS_WRITE_REQUEST
Else
    com_cod = CS_READ_REQUEST
End If
com_cod = com_cod + "0"
Select Case len_data_byte   'aggiungo al command code la lunghezza
  Case 1
    com_cod = com_cod + "11"
  Case 2
    com_cod = com_cod + "10"
  Case 3
    com_cod = com_cod + "01"
  Case 4
    com_cod = com_cod + "00"
End Select
If tip_tra = send Then  'aggiungo al command code e ed s
    com_cod = com_cod + "11"
Else
    com_cod = com_cod + "00"
 End If
str = str + Right$(bin_hex(com_cod), 2)
app = int_esa(Index - index_par_lenze)
ind_l_b = Right$(app, 2)
app = Left$(app, Len(app) - 2)
ind_h_b = Right$(app, 2)
str = str + ind_l_b + ind_h_b + Format$(subindex_par_lenze, FT2)
If tip_tra = send Then
    app = lng_esa(data * 10000)
End If
For i = 1 To len_data_byte
    If tip_tra = send Then
        str = str + Right$(app, 2)
        app = Left$(app, Len(app) - 2)
    Else
        str = str + "00"
    End If
Next i
pre_str_lenze = str
End Function


Public Sub rie_msg_can(str As String)
Dim i As Integer

msg_can.ID = val("&H" + Left$(str, 3))
msg_can.RTR = 0
msg_can.LEN = 8
For i = 0 To 7
    msg_can.data(i) = val("&H" + Mid$(str, (i * 2) + 4, 2))
Next i

End Sub

Public Sub del_msg_can()
Dim i As Integer

msg_can.ID = 0
msg_can.LEN = 8
msg_can.RTR = CByte(0)
For i = 0 To 7
  msg_can.data(0) = CByte(0)
Next i

End Sub

Public Function tra_msg(par_imp As Boolean) As String
Dim str As String
Dim i As Integer
Dim ris As Double
Dim str1 As String

str = ""
For i = 7 To 4 Step -1
  str = str + int_esa_azi(msg_can.data(i))
Next i
If Left$(str, 1) = "F" Then
  str1 = hex_bin(str, False)
  str = bin_hex_azi(comp_2(str1))
  ris = -val(Hex_Dec(str))
Else
  ris = val(Hex_Dec(str))
End If
If Not par_imp Or par_nuo.dividi Then ris = ris / 10000
If ris < 0 Then
    ris = -ris
    tra_msg = "- " + CStr(ris)
Else
    tra_msg = CStr(ris)
End If
End Function


Public Sub codadd(item1 As String, key As String, Optional item2 As String)
Dim c As CodiciAzionamenti
'On Error GoTo Fine
Set c = New CodiciAzionamenti
c.key = key
c.item1 = Mid$(item1, 1, LEN_DES)
c.item2 = Mid$(item2, 1, LEN_DES) 'If Len(item2) > 0 Then
cod_azi.add item:=c, key:=key

End Sub

