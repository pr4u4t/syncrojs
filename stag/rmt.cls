VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "rmt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Public Event retok(s As String)
Public Event retbad(err As String)



' variabili per la gestione di richieste multiple per un singolo comando
Dim loc_cnt As Integer
Dim loc_ret As String
Dim loc_cod_ret  As cod_ret
Dim loc_dst As rmt          ' destinatario finale
Dim ute As Integer
Dim loc_name As String



Function add_ese() As Boolean
' aggiunge un altro comando di cui si dovra' aspettare il completamento
' dst ricevera' l' evento finale, la rmt corrente validera' la risposta
' ( se un dispositivo non ha dato errore seriale ma ha comunque ritornato un not acknoledge
If com_plc_ute(ute).ric = PLC_NUL Then
  add_ese = False
Else
  com_plc_ute(ute).ret = RET_EXE
  com_plc_ute(ute).obj.cnt = com_plc_ute(ute).obj.cnt + 1
  add_ese = True
End If
End Function

Property Let cnt(v As Integer)
' legge il numero di richieste in attesa di risposta
  loc_cnt = v
End Property

Property Let abi_vis_err(v As Boolean)
' scrive abilitazione visualizzazione codice di errore
  com_plc_ute(ute).abi_vis_err = v
End Property
Property Get cnt() As Integer
' legge il numero di richieste in attesa di risposta
  cnt = loc_cnt
End Property

Property Get err() As String
' legge l' errore
  err = com_plc_ute(ute).err
End Property

Property Let err_cod(v As Integer)
' legge il codice di errore
   com_plc_ute(ute).cod_err = v
End Property

Property Get err_cod() As Integer
' legge il codice di errore
  err_cod = com_plc_ute(ute).cod_err
End Property

Property Get abi_vis_err() As Boolean
' legge abilitazione visualizzazione codice di errore
  err_cod = com_plc_ute(ute).abi_vis_err
End Property
Function esegui(ric As Integer, tim_out As Long, liv As Integer, num_ord As Integer, Optional dat As Variant = False) As Boolean
' imposta i membri ric e obj
' se busy ritorna false
  If com_plc_ute(ute).ric = PLC_NUL Then
    com_plc_ute(ute).ric = ric
    com_plc_ute(ute).tim_out = tim_out
    com_plc_ute(ute).liv = liv
    com_plc_ute(ute).num_ord = num_ord
    com_plc_ute(ute).ret = RET_WRK
    If Not IsMissing(dat) Then com_plc_ute(ute).dat = dat
    loc_cnt = 1
    loc_ret = ""
    loc_cod_ret = 0
    Set com_plc_ute(ute).obj = Me
    esegui = True
    log_str "rmt: esegui " + Me.name + " " + Format$(ric) + " " + Format$(liv) & IIf(IsMissing(dat), "", " con dati aggiuntivi"), MED_PRIORITY, log_rmt
  Else
    esegui = False
    log_str "rmt: rmt occupata " + Me.name + " " + Format$(ric) + " " + Format$(liv) & IIf(IsMissing(dat), "", " con dati aggiuntivi"), MED_PRIORITY, log_rmt
  End If
End Function


Property Get num_ord() As Integer
' legge il numero dell' asse
  num_ord = com_plc_ute(ute).num_ord
End Property

Property Let name(nome As String)
  loc_name = nome
End Property

Property Get name() As String
' ritorna il nome, per debug
  name = loc_name
End Property



Sub set_plc_ute(aoc As Integer)
' se aoc = 1 variabile gestita per l' AOC, se 0 da utente
  ute = aoc
  com_plc_ute(ute).abi_vis_err = True
End Sub

Property Get tim_out() As Long
' legge il time out
  tim_out = com_plc_ute(ute).tim_out
End Property

Property Get liv() As Integer
' legge l' indice del coltello
  liv = com_plc_ute(ute).liv
End Property

Property Get dat() As Variant
' legge i dati associati al comando
  dat = com_plc_ute(ute).dat
End Property
Sub resetta()
' ripulisce la struttura comandi utente
    com_plc_ute(ute).ric = PLC_NUL
    com_plc_ute(ute).ret = RET_ZRO
    loc_cnt = 0
    loc_ret = ""
    loc_cod_ret = RET_OK
'  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX da rivedere
    'controlla se c'è già una risposta
    'Set com_plc_ute(ute).obj = Nothing
End Sub


Property Get ret() As Integer
' legge il codice di risposta
  ret = com_plc_ute(ute).ret
End Property

Property Let ret(v As Integer)
' imposta il codice di risposta
  com_plc_ute(ute).ret = v
End Property


Property Get ric() As Integer
' legge il codice di richiesta
  ric = com_plc_ute(ute).ric
End Property

Property Get obj() As rmt
' serve per sapere chi ha generato la richiesta al task plc
 Set obj = com_plc_ute(ute).obj
End Property

Sub rispondi(ByVal ret As Integer, s As String, Optional TRA)
' genera l' evento di risposta
' ret vale RET_OK , RET_BAD o codice di errore

  If loc_cod_ret <> RET_BAD And ret = RET_BAD Then
    loc_ret = s         ' memorizza il primo codice di errore verificatosi
    loc_cod_ret = ret
  End If
  If loc_cnt <= 1 Then       ' non ci sono piu' richieste accodate, risponde
    If loc_cnt = 1 Then loc_cnt = loc_cnt - 1
    log_str "rmt: rispondi " + Format$(ret) + " per " + Me.name, MED_PRIORITY, log_rmt
    com_plc_ute(ute).err = s
    com_plc_ute(ute).ret = ret
    If Not com_plc_ute(ute).abi_vis_err Then s = "": com_plc_ute(ute).abi_vis_err = True
    If ret = RET_OK Then
      RaiseEvent retok(s)
    Else
      If Not IsMissing(TRA) Then   ' si traduce
        com_plc_ute(ute).err = msg(s)
      Else
        com_plc_ute(ute).err = s
      End If
      RaiseEvent retbad(s)
    End If
  Else
    loc_cnt = loc_cnt - 1
    log_str "rmt: rispondi ha decrementato cnt per " + Me.name + " " + Format$(cnt), MED_PRIORITY, log_rmt
  End If

End Sub

Private Sub Class_Initialize()
  ute = 1
  com_plc_ute(ute).abi_vis_err = True
End Sub

