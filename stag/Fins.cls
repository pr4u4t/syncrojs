VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "Fins"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False

' classe di comunicazione con plc Cj usando le fins in tcp ip
Private mTimeOut As Integer
Private ActiveCmd As Integer
Private wait As Boolean
Private RetValue As Long
Public RetData As Variant
Private ReadValue() As Long
Private ClientNode As Integer
Private ServerNode As Integer
Private sid As Byte
Private LastError As Boolean
Public WithEvents sck As Winsock
Attribute sck.VB_VarHelpID = -1
Public WithEvents tim As Timer
Attribute tim.VB_VarHelpID = -1

Const ServerPort = 9600
Public Property Get TimeOut() As Integer
    TimeOut = mTimeOut
End Property
Public Property Let TimeOut(ByVal Value As Integer)
    mTimeOut = Value
    tim.Interval = Value * 2
End Property

Public Property Get Enabled() As Boolean
    Enabled = tim.Enabled
End Property
Public Property Let Enabled(ByVal Value As Boolean)
    tim.Enabled = Value
End Property

Public Property Get RemoteHost() As String
    RemoteHost = sck.RemoteHost
End Property

Public Property Let RemoteHost(ByVal Value As String)
    sck.RemoteHost = Value
End Property
Public Property Get RemotePort() As Long
    RemotePort = sck.RemotePort
End Property

Public Property Let RemotePort(ByVal Value As Long)
    sck.RemotePort = Value
End Property
Public Property Get LocalPort() As Long
    LocalPort = sck.LocalPort
End Property

Public Property Let LocalPort(ByVal Value As Long)
    sck.LocalPort = Value
End Property

Public Property Get error() As Integer
    If sck.State <> sckConnected Or ServerNode = 0 Or LastError Then
        error = True
    Else
        error = False
    End If
End Property
Private Property Get err() As Integer
    If sck.State <> sckConnected Or ServerNode = 0 Then
        err = True
    Else
        err = False
    End If
End Property


Public Function DM(offset As Integer, siz As Integer, v As Variant, wrt As Boolean) As Long
' legge un DM
Dim b() As Byte
Dim t As Long, k As Integer
    If wrt = False Then
        ReDim ReadValue(siz - 1)
    End If
    If err Then
        DM = -1
        Exit Function
    End If
' frame header
    ReDim b(15)
    CopyType b(0), "FINS", 4
    If wrt = False Then
        b(7) = 8 + 18 'lunghezza
    Else
        b(7) = 8 + 18 + siz * 2 'lunghezza
    End If
    
    b(11) = 2       ' comando
    ActiveCmd = 2
    sck.SendData b
    Debug.Print "Send DM header"
' command frame:
    If wrt = False Then
        ReDim b(17)
    Else
        ReDim b(17 + siz * 2)
    End If
    incsid
    b(0) = &H80 ' ICF
    b(1) = &H0  '  RSV
    b(2) = &H2 ' GCT
    b(3) = &H0   'DNA
    b(4) = ServerNode   'DA1
    b(5) = &H0   'DA2
    b(6) = &H0   'SNA
    b(7) = ClientNode   'SA1
    b(8) = &H0   'SA2
    b(9) = sid    'SID
    b(10) = 1   'MRC
    b(11) = IIf(wrt = False, 1, 2) 'SRC
    b(12) = &H82  'VARIABLE TYPE: DM
    b(13) = offset \ 256   'READ START ADDRESS: 100
    b(14) = offset Mod 256
    b(15) = &H0
    b(16) = &H0   'WORDS READ: 1
    b(17) = siz
    If wrt = True Then  ' assegna i valori da scrivere
        For k = 0 To siz - 1
            'Alessandrouff 28-12-2006
            b(18 + k * 2) = IIf(v(k) >= 0, v(k), 65536 + v(k)) \ 256
            b(18 + k * 2 + 1) = IIf(v(k) >= 0, v(k), 65536 + v(k)) Mod 256
        Next k
    End If
    sck.SendData b
    Debug.Print "Send DM frame"
    t = GetTickCount()
    wait = True
    While GetTickCount() - t < mTimeOut
        If wait = False Then
            If wrt = False Then  ' assegna i valori letti
                RetData = ReadValue
            End If
            DM = RetValue
            LastError = False
            Exit Function
        End If
        DoEvents
    Wend
    LastError = True
    DM = -1
End Function
Public Function SetMonitor() As Integer
' mette in monitor mode
Dim b() As Byte
Dim t As Long
    If err Then
        SetMonitor = -1
        Exit Function
    End If
' frame header
    ReDim b(15)
    CopyType b(0), "FINS", 4
    b(7) = 8 + 15   'lunghezza
    b(11) = 2       ' comando
    ActiveCmd = 2
    sck.SendData b
    Debug.Print "Send RunMode header"
' command frame:
    ReDim b(14)
    incsid
    b(0) = &H80 ' ICF
    b(1) = &H0  '  RSV
    b(2) = &H2 ' GCT
    b(3) = &H0   'DNA
    b(4) = ServerNode   'DA1
    b(5) = &H0   'DA2
    b(6) = &H0   'SNA
    b(7) = ClientNode   'SA1
    b(8) = &H0   'SA2
    b(9) = sid    'SID
    b(10) = 4   'MRC
    b(11) = 1   'SRC
    b(12) = &HFF
    b(13) = &HFF
    b(14) = 2 'monitor
    sck.SendData b
    Debug.Print "Send RunMode frame"
    t = GetTickCount()
    wait = True
    While GetTickCount() - t < mTimeOut
        If wait = False Then
             SetMonitor = RetValue
             Debug.Print "Returned Data"
             LastError = False
             Exit Function
        End If
        DoEvents
    Wend
    LastError = True
    SetMonitor = -1
End Function
Public Function RunMode() As Integer
' ritorna il modo di funzionamento (monitor, etc)
Dim b() As Byte
Dim t As Long
    If err Then
        RunMode = -1
        Exit Function
    End If
' frame header
    ReDim b(15)
    CopyType b(0), "FINS", 4
    b(7) = 8 + 12   'lunghezza
    b(11) = 2       ' comando
    ActiveCmd = 2
    sck.SendData b
    Debug.Print "Send RunMode header"
' command frame:
    ReDim b(11)
    incsid
    b(0) = &H80 ' ICF
    b(1) = &H0  '  RSV
    b(2) = &H2 ' GCT
    b(3) = &H0   'DNA
    b(4) = ServerNode   'DA1
    b(5) = &H0   'DA2
    b(6) = &H0   'SNA
    b(7) = ClientNode   'SA1
    b(8) = &H0   'SA2
    b(9) = sid    'SID
    b(10) = 6   'MRC
    b(11) = 1   'SRC
    sck.SendData b
    Debug.Print "Send RunMode frame"
    t = GetTickCount()
    wait = True
    While GetTickCount() - t < mTimeOut
        If wait = False Then
             RunMode = RetValue
             Debug.Print "RunMode"
             LastError = False
             Exit Function
        End If
        DoEvents
    Wend
    LastError = True
    RunMode = -1
End Function

Public Function writeArea(VType As Long, VOffset As Integer, VSize As Integer, VWriteData As Variant) As Long
    writeArea = DM(VOffset, VSize, VWriteData, True)
End Function
Public Function readArea(VType As Long, VOffset As Integer, VSize As Integer) As Long
Dim VRetValType As Variant
    readArea = DM(VOffset, VSize, VRetValType, False)
End Function


Private Sub Class_Initialize()
' costruttore, usa sck
'    sck.Protocol = sckTCPProtocol
'    sck.RemotePort = ServerPort
'    sck.RemoteHost = mTcpAddress
'    sck.LocalPort = 0 ' any
'    tim.Enabled = False
End Sub

Private Sub sck_Close()
    sck.Close
    Debug.Print "Close"
End Sub


Private Sub sck_DataArrival(ByVal bytesTotal As Long)
    Dim b As Variant, k As Integer
    sck.GetData b
    If UBound(b) + 1 <> bytesTotal Then
        Exit Sub  ' scarta i dati
    End If
    If b(15) = 0 Then   ' ok
      If b(11) = ActiveCmd Then
        Select Case b(11)
            Case 1
                ServerNode = b(23)
                ClientNode = b(19)
                Debug.Print "Assigning nodes"
            Case 2
                If b(23) = ServerNode Then
                    If b(25) = sid Then
                        wait = False    ' la risposta e' per il comando in attesa
                        If b(28) = 0 And b(29) = 0 Then
                            RetValue = 0
                            Select Case Format$(b(26)) + Format$(b(27))
                                Case "41"   ' set status monitor
                                    Debug.Print "Ok Response to set monitor: " + Format$(b(28)) + Format$(b(29))
                                    RetValue = 0
                                Case "11"   ' read DM
                                    Debug.Print "Ok Response to read data Value: " + Format$(b(30)) + Format$(b(31))
                                    ReDim ReadValue((UBound(b) - 30 + 1) \ 2 - 1)
                                    For k = 0 To UBound(ReadValue)
                                        ReadValue(k) = b(30 + k * 2) * 256 + b(30 + k * 2 + 1)
                                    Next k
                                    RetValue = b(30) * 256 + b(31)
                                Case "12"   ' write DM
                                    Debug.Print "Ok Response to write data Value: " + Format$(b(28)) + Format$(b(29))
                                    RetValue = b(28) * 256 + b(29)
                                Case "61"   ' read status mode
                                     RetValue = b(31)
                                     Debug.Print "Ok Response to read status Value: " + Format$(b(31))
                            End Select
                        Else
                            Debug.Print "Error code" + Format$(b(28)) + Format$(b(29)) + " returned to command " + Format$(b(26)) + Format$(b(27)), MED_PRIORITY, log_plc
                            RetValue = -1
                        End If
                    Else
                        Debug.Print "worng sid"
                        log_str "Received reply with wrong sid " + Format$(b(25)) + " instead of " + Format$(sid), MED_PRIORITY, log_plc
                    End If
                Else
                    Debug.Print "Wrong ServerNode " + Format$(b(23))
                    log_str "Received data from Wrong ServerNode " + Format$(b(23)), MED_PRIORITY, log_plc
                End If
            Case 3
                ' notifica errore
                Debug.Print "Complaining error " & Format$(b(15))
                log_str "Received error frame: error " + Format$(b(15)), MED_PRIORITY, log_plc
            Case Else
                Debug.Print "Unpredicted answer"
                log_str "Unpredicted answer, command " + Format$(b(11)), MED_PRIORITY, log_plc
        End Select
      Else
        Debug.Print "Wrong Command: was " & Format$(ActiveCmd) & " answer to " & Format$(b(11))
        log_str "Wrong answer: command " + Format$(ActiveCmd) + ", answer to command " + Format$(b(11)), MED_PRIORITY, log_plc
      End If
    Else
        Debug.Print "Error Number" & Format$(b(15))
        log_str "Error number " + Format$(b(15)) + " on command " + Format$(b(11)), MED_PRIORITY, log_plc
    End If
End Sub

Private Sub tim_Timer()
Dim b() As Byte
    
    ' timer di riconnessione
    Select Case sck.State
        Case 0 ' closed
            'Set sck = Nothing
            'Set sck = avvio.Winsock1
            ClientNode = 0
            ServerNode = 0
            sck.LocalPort = 0
            sck.Connect
        Case 1 ' open
            sck.Close
        Case 2 ' listening
            sck.Close
        Case 3 ' ConnectionPending
            sck.Close
        Case 4 ' ResolvingHost
            sck.Close
        Case 5 ' HostResolved
            sck.Close
        Case 6 ' Connecting
            sck.Close
            Debug.Print "Closing state"
        Case 7 ' connected
            If ServerNode = 0 Then
                ReDim b(19)
                CopyType b(0), "FINS", 4
                b(7) = 12
                ActiveCmd = 1
                sck.SendData b
            End If
            If LastError = True Then
              sck.Close
              Debug.Print "LastErrorPresent, resetting socket"
              LastError = False
            End If
        Case 8 ' closing
            sck.Close
        Case 9 ' error
            sck.Close
    End Select
End Sub
Sub incsid()
If sid = 255 Then
    sid = 0
Else
    sid = sid + 1
End If
End Sub
