Attribute VB_Name = "canapi32"
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CanApi32.bas
'
'  Definition of the CAN-API.
'
'
'     Principle:
'     ~~~~~~~~~~
'     The driver supports multiple clients (= Windows or DOS programs
'     that communicate with CAN-busses), and multiple CAN-hardware
'     implemented with 82C200 controllers.
'     A cardinal point is the idea of the 'Net': it describes a CAN-bus
'     which is extended into the PC. Multiple 'Clients' can be connected
'     to one net which itself can have an interface to a physical CAN-bus
'     via an appropriate CAN-adaptor.
'     A net definition comprehends aside from the baudrate an amount
'     of CAN-messages to process.
'
'     Clients which are specialized on some kind of CAN-bus (e.g. stepper
'     motor control, car radio panel, etc.), shouldn't offer any hardware
'     selection but directly address a fixed net (e.g. 'Lab-Net').
'     The connection net - hardware can then be accomplished
'     by a separate configuration tool (the settings depend on the
'     respective PC and its CAN-hardware).
'
'     If neccessary, CAN-nodes connected to an external CAN-bus can
'     be simulated by clients on the same net. In this case there is no
'     CAN-hardware required, the complete bus can be simulated within the
'     PC. The net can then be defined as an 'Internal Net'.
'
'     Samples for possible net configurations:
'     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
'     (can all be realized at the same time):
'                                                      external
'                                       ,------------< CAN-bus 'A'
'     ,--------. ,--------.       ,-----+----.
'     |Client A| |Client B|       |Hardware 1|
'     `---+----' `----+---'       `-----+----'
'         `-----------+-----------------'
'                  N e t  I                           external
'                                       ,------------< CAN-bus 'B'
'     ,--------. ,--------.       ,-----+----.
'     |Client C| |Client D|       |Hardware 2|
'     `---+--+-' `----+---'       `-----+----'
'         |  `--------+-----------------'              external
'         |        N e t  II            ,------------< CAN-bus 'C'
'         |      ,--------.       ,-----+----.
'         |      |Client E|       |Hardware 3|
'         |      `----+---'       `-----+----'
'         `-----------+-----------------'             'Gateway'
'                  N e t  III
'
'      ,--------. ,--------. ,--------.
'      |Client F| |Client G| |Client H|
'      `---+----' `---+----' `---+----'               'Internal Net'
'         `----------+----------'
'                  N e t  IV
'
'     Features:
'     ~~~~~~~~~
'      - 1 client can be connected to multiple nets
'      - 1 net supplies multiple clients
'      - 1 hardware can be used by 1 net at the same time
'      - each net can be assigned to 1 hardware or no hardware at all
'      - if a client sends a message on the net, the message will be routed
'        to all other clients and over a connected hardware to the physical
'        bus
'      - if a message is received from a hardware, it will be routed to all
'        clients which are connected to the hardware via a net. Each client
'        only receives the messages which pass its acceptance filter
'      - CAN-hardware can be configured via a Windows Control Panel application,
'        nets can be configured with a separate tool.
'        Multipled nets can be defined for each hardware, but only one can be
'        active at the same time.
'      - clients connect to a net via the name of the net
'      - each hardware has its own Xmt-queue to buffer outgoing messages
'      - each client has a Rcv-queue to buffer received messages
'      - each client has a Xmt-queue, which holds outgoing messages until
'        their scheduled real send time. Is the send time reached they will
'        be written into the Xmt-queue of the hardware.
'      - hClient: 'client handle'. This number is used by the driver to
'                 identify and manage a client
'      - hHw :    'hardware handle'. This number is used by the driver to
'                 identify and manage a hardware
'      - hNet:    'net handle'. This number is used by the driver to
'                 identify and manage a net
'      - all handles are 1-based. 0 = illegal handle
'      - used hardware and nets are defined in SYSTEM.INI.
'        During Windows startup the driver reads the configuration and
'        initializes all hardware and nets.
'        Format:
'              [PEAKCAN]
'              Hardware<HardwareHandle>=<DriverNo>,<PortBase>,<IRQ>
'                     ...
'              Net<NetHandle>=<Name>,<HardwareHandle>,<BTR0BTR1>
'                     ...
'        Sample:
'             Hardware1=1,0x300,15
'             Net7=TESTNET,1,0x001C
'
'      - the API-functions are devided into 3 groups:
'        1) Control-API: control of the driver through configuration tools
'        2) Client-API: reading and writing of messages through applications
'        3) Info-API: helper functions
'
'
'     Control-API
'     ~~~~~~~~~~~~
'      CAN_RegisterHardware(hHw, DriverNr, Port, Int)
'                      Activate a hardware:
'                      memory test, install interrupt.
'                      Hardware can be accessed in future via 'hHw'.
'      CAN_RegisterNet(hNet, name, hHw, BTR0BTR1)
'                      Create a new net, make an assignment net - hardware.
'                      Net can be accessed in future via 'hNet'.
'      CAN_RemoveNet(hNet)
'                      Delete a net, the net handle gets invalid.
'      CAN_RemoveHardware(hHw)
'                      Remove a hardware from driver management, the
'                      hardware handle gets invalid.
'      CAN_CloseAll()
'                      Remove all hardware and nets.
'
'
'     Client-API
'     ~~~~~~~~~~
'
'      Hardware control:
'      ~~~~~~~~~~~~~~~~~
'      CAN_HardwareOfNet(hNet, hHw)
'                      Find out the hardware assigned to a given net.
'                      (e.g. for CAN_SetBaud())
'      CAN_SetBaud(hHw, BTR0BTR1)
'                      Adjustment of the hardware-baudrate.
'                      Affects other clients on the same net.
'      CAN_Status(hHw)
'                      Get the actual state of a hardware.
'      CAN_ResetHardware(hHw)
'                      Controller reset, reset Xmt-queue of hardware.
'                      Affects other clients on the same net.
'      CAN_ResetClient(hClient)
'                      Reset Rcv-queue of client.
'
'      Read/Write:
'      ~~~~~~~~~~~
'      CAN_Write(hClient, hNet, msgbuff, sendtime)
'                      Write message at time 'sendtime' to net 'hNet'.
'                      The message will be sent to the linked
'                      hardware and to all clients that have built up a
'                      connection to the net with 'CAN_ConnectToNet()'.
'      CAN_Read(hClient, msgbuff, hNet, rcvtime)
'                      Read a message from the Rcv-queue.
'
'
'      Registration and connection of clients:
'      ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
'      CAN_RegisterClient(name, hWnd, hClient)
'                      Register client at driver,
'                      get client handle and initialize Rcv-queue.
'                      (one call per client)
'      CAN_ConnectToNet(hClient, netname, hNet)
'                      Connect client to a net.
'                      (one call per client and net)
'      CAN_RegisterMsg(hClient, hNet, ID)
'                      Announce that the client wants to receive messages
'                      with CAN-ID 'ID' from the net 'hNet'.
'                      ID = 0xFFFF: receive all messages.
'                      Note: the filter mechanism does not guarantee that
'                      the client will receive only the registered messages.
'      CAN_DisconnectFromNet(hClient, hNet)
'                      Disconnect client from a net.
'      CAN_RemoveClient(hClient)
'                      Remove client from the driver. Free resources.
'
'
'      Info-API:
'      ~~~~~~~~~~
'      CAN_GetDriverName(i, namebuff)
'                      Get the name of the hardware type indexed by 'i'
'                      which is supported by the driver.
'      CAN_GetNetName(hNet, namebuff)
'                      Get the name of a net by its handle.
'      CAN_Msg2Text(msgbuff, textbuff)
'                      Debugging: transform CAN-message to text.
'      CAN_GetDiagnostic(textbuff)
'                      Debugging: get the text from the diagnosis
'                      textbuffer
'      CAN_GetSystemTime(timebuff)
'                      Access to the function 'Get_System_Time()' of the VMM:
'                      Returns time in milliseconds since Windows start.
'      CAN_GetErrText(err, textbuff)
'                      Transform error flags in 'err' to text.
'      CAN_VersionInfo(textbuff)
'                      Returns version and copyright information from driver.
'
'
'      Samples for API usage:
'      ~~~~~~~~~~~~~~~~~~~~~~
'      a) Initialization of hardware and nets at Windows startup:
'         in 'VxD -OnDeviceInit()':
'              '  controlled by SYSTEM.INI:
'                      ...
'              CAN_RegisterHardware()    '  Initilize each found hardware
'              CAN_RegisterHardware()
'                      ...
'              CAN_RegisterNet()         '  Load net definitions
'              CAN_RegisterNet()
'                      ...
'
'      b) Configuration tool:
'              LoadConfigFromFile()
'              EditConfig()            '  User sets up a configuration
'              SaveConfigToFile()
'              CAN_CloseAll()          '  Reset the driver
'              '  Controlled by the configuration
'              CAN_RegisterHardware()
'              CAN_RegisterHardware()
'              SaveHardwareToWinIni()
'                      ....
'              CAN_RegisterNet()
'              CAN_RegisterNet()
'              SaveNetsToWinIni()
'                      ....
'              '  New configuration is now and after Windows restart
'              '  active. All still connected clients are now dead.
'
'      c) Client
'              CAN_RegisterClient()    '  Just once
'              CAN_ConnectToNet(, mynet)
'              '  CAN_ConnectToNet()   '  Perhaps multiple, e.g. if Gateway
'
'              if (own_baudrate) then
'                  CAN_HardwareOfNet(mynet, hw)
'                  CAN_SetBaud(hw, MYBAUD)
'              end if
'
'              CAN_RegisterMsg()       '  For each Rcv-message
'              CAN_RegisterMsg()
'              CAN_RegisterMsg()
'
'              while active
'                  if  not (CAN_Read() AND CAN_ERR_QRCVEMPTY) then
'                      '  Something has been received
'                      delay = CAN_GetSystemTime() - rcvtime
'                  end if
'                  if (something_to_write) then
'                      CAN_Write()
'                  end if
'                  if (something_exceptional) then
'                      CAN_ResetHardware()
'                      CAN_ResetClient()
'                  end if
'              wend
'
'              CAN_RemoveClient()      '  Just once, free resources
'
'
'      Provided constants:
'      CAN_BAUD_1M ... _5K     Baudrates.
'
'      All functions return a set of error states CAN_ERR_xxx
'
'
'      Authors: Hoppe / Wolf / Stephan
'
'      -----------------------------------------------------------------------
'      Copyright (C) 1995-98 by Joerg Hoppe,
'                    PEAK-Service GmbH, Darmstadt/Germany
'

Option Explicit

' Constant definitions
Public Const CAN_ErrorDummyID = &HFFFF     ' ID of messages that do not transport
                                    ' messages but errors


' I/O-configuration
' 0: internal reference voltage, 1 = reference over RX1
Public Const CAN_CRX1REF = 1


' Baudrate codes = values for CAN-controller registers BTR0/BTR1
Public Const CAN_BAUD_1M = &H14        '   1 MBit / s
Public Const CAN_BAUD_500K = &H1C      ' 500 kBit / s
Public Const CAN_BAUD_250K = &H11C     ' 250 kBit / s
Public Const CAN_BAUD_125K = &H31C     ' 125 kBit / s
Public Const CAN_BAUD_100K = &H432F    ' 100 kBit / s
Public Const CAN_BAUD_50K = &H472F     '  50 kBit / s
Public Const CAN_BAUD_20K = &H532F     '  20 kBit / s
Public Const CAN_BAUD_10K = &H672F     '  10 kBit / s
Public Const CAN_BAUD_5K = &H7F7F      '   5 kBit / s

' Error state flags
Public Const CAN_ERR_OK = &H0
Public Const CAN_ERR_XMTFULL = &H1       ' Transmit buffer of CAN-controller full
Public Const CAN_ERR_OVERRUN = &H2       ' CAN-controller read too late
Public Const CAN_ERR_BUSLIGHT = &H4      ' Bus-error: an errorcounter has reached limit
Public Const CAN_ERR_BUSHEAVY = &H8      ' Bus-error: an errorcounter has reached limit
Public Const CAN_ERR_BUSOFF = &H10       ' Bus-error: CAN_Controller engaged 'Bus-Off'
Public Const CAN_ERR_QRCVEMPTY = &H20    ' Rcv-queue is empty
Public Const CAN_ERR_QOVERRUN = &H40     ' Rcv-queue read too late
Public Const CAN_ERR_QXMTFULL = &H80     ' Xmt-queue full
Public Const CAN_ERR_REGTEST = &H100     ' Error during register test of the CAN-controller
Public Const CAN_ERR_NOVXD = &H200       ' VxD (virtual device driver) not found
Public Const CAN_ERRMASK_ILLHANDLE = &H1C00   ' Handle error mask
Public Const CAN_ERR_HWINUSE = &H400     ' Hardware occupied by a net
Public Const CAN_ERR_NETINUSE = &H800    ' Clients are connected to net
Public Const CAN_ERR_ILLHW = &H1400      ' Invalid hardware handle
Public Const CAN_ERR_ILLNET = &H1800     ' Invalid net handle
Public Const CAN_ERR_ILLCLIENT = &H1C00  ' Invalid client handle
Public Const CAN_ERR_RESOURCE = &H2000   ' Resource (FIFO, client, timeout) not available

Public Const CAN_ERR_ANYBUSERR = (CAN_ERR_BUSLIGHT Or CAN_ERR_BUSHEAVY Or CAN_ERR_BUSOFF)

Public Const MAX_HCANW = 8               ' Resource (FIFO, client, timeout) not available
Public Const MAX_HCANNET = 32            ' Allow net handles from 1 to MAX_HCANNET
Public Const MAX_HCANCLIENT = 32         ' Allow client handles from 1 to MAX_HCANCLIENT
Public Const MAX_HCANCLIENT_LIGHT = 3    ' The light-version supports only 3 clients

Public Const MAX_NETNAMELEN = 20         ' Max. length of net names
Public Const MAX_CLIENTNAMELEN = 20      ' Max. length of net names
Public Const MAX_DRIVERNAMELEN = 32      ' Max. length of driver (hardware type) names


' Type definitions

' A CAN-message
Public Type TCANMsg
        ID As Integer             ' 11 Bit identifier
        RTR As Byte               ' TRUE, if Remote Frame
        LEN As Byte               ' Number of valid data bytes (0..8)
        data(7) As Byte           ' Data bytes 0..7
End Type


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_RegisterHardware()
'
'  Activates a hardware, tests the 82C200 registers,
'  assigns a transmit buffer and a hardware handle.
'  Programs configuration of the CAN-controller,
'  sets acceptance mask to minimum.

Public Declare Function CAN_RegisterHardware Lib "VCANW32.DLL" _
    (ByVal hHw As Byte, _
     ByVal DriverNr As Integer, _
     ByVal PortBase As Integer, _
     ByVal IntNr As Byte) As Integer
       
    ' hHw:      Demanded hardware handle
    ' DriverNr: Driver No.(hardware-type) to use
    ' PortBase: I/O-address of hardware
    ' IntNr:    Used hardware interrupt


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_RegisterNet()
'
'  Create a new net, make an assignment 'net - hardware'.

Public Declare Function CAN_RegisterNet Lib "VCANW32.DLL" _
    (ByVal hNet As Byte, _
     ByVal szName As String, _
     ByVal hHw As Byte, _
     ByVal BTR0BTR1 As Integer) As Integer

    'hNet:     Demanded handle of the new net
    'szName:   Name of the new net
    'hHw:      Connected hardware, 0 if none
    'BTR0BTR1: Baudrate


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_RemoveNet()
'
'  Delete a net definition.

Public Declare Function CAN_RemoveNet Lib "VCANW32.DLL" _
    (ByVal hNet As Byte) As Integer

    ' hNet: Handle of net to delete


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_RemoveHardware()
'
'  Remove a hardware from driver management, release buffer,
'  deactivate Interrupt.

Public Declare Function CAN_RemoveHardware Lib "VCANW32.DLL" _
    (ByVal hHw As Byte) As Integer

    ' hHw: Handle of hardware to remove


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_CloseAll()
'
'  Remove all hardware and nets.

Public Declare Function CAN_CloseAll Lib "VCANW32.DLL" () As Integer


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_HardwareOfNet()
'
'  Find out the hardware assigned to the given net (e.g. for CAN_SetBaud()).

Public Declare Function CAN_HardwareOfNet Lib "VCANW32.DLL" _
    (ByVal hNet As Byte, _
     ByRef hHw As Byte) As Integer

    ' hNet: Net handle
    ' hHw:  Resulting hardware handle


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_SetBaud()
'
'  Adjustment of the hardware-baudrate.
'  Affects other clients on the same net.

Public Declare Function CAN_SetBaud Lib "VCANW32.DLL" _
    (ByVal hHw As Byte, _
     ByVal BTR0BTR1 As Integer) As Integer

    ' hHw:      Hardware to manipulate
    ' BTR0BTR1: New value for baudrate register


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_Status()
'
'  Get the actual state (e.g. BUS-OFF) of a hardware.

Public Declare Function CAN_Status Lib "VCANW32.DLL" _
    (ByVal hHw As Byte) As Integer

    ' hHw: Handle of hardware to inspect


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_ResetHardware()
'
'  Controller reset, reset Transmit-queue of hardware.
'  Affects other clients on the same net.

Public Declare Function CAN_ResetHardware Lib "VCANW32.DLL" _
    (ByVal hHw As Byte) As Integer

    ' hHw: Handle of hardware to reset


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_ResetClient()
'
'  Reset the Receive-queue of a client

Public Declare Function CAN_ResetClient Lib "VCANW32.DLL" _
    (ByVal hClient As Byte) As Integer

    ' hClient: Handle of client to reset


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_Write()
'
'  Write message at time 'sendtime' to net 'hNet'.
'  The message will be written into the Xmt-queue of the linked
'  hardware and into the Rcv-queues of all clients that have
'  established a connection to the net with 'CAN_ConnectToNet()'.

Public Declare Function CAN_Write Lib "VCANW32.DLL" _
    (ByVal hClient As Byte, _
     ByVal hNet As Byte, _
     ByRef msgbuff As TCANMsg, _
     ByRef dwSendTime As Long) As Integer

    ' hClient:    Handle of sending client
    ' hNet:       Send on this net
    ' msgbuff:    Message to send
    ' dwSendTime: Send time


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_Read()
'
'  Read the next message or the next error from the Rcv-queue of
'  the client. The message will be written into the provided message
'  buffer 'msgbuff'.'
'
'  Return values:
'      CAN_ERR_OK        - a valid message has been read
'      CAN_ERR_QRCVEMPTY - no more messages received
'      else              - error bits

Public Declare Function CAN_Read Lib "VCANW32.DLL" _
    (ByVal hClient As Byte, _
     ByRef msgbuff As TCANMsg, _
     ByRef hNet, _
     ByRef dwRcvTime As Long) As Integer

    ' hClient:   Read from the Rcv-queue of this client
    ' msgbuff:   Resulting message/error
    ' hNet:      The net which is the origin of the message
    ' dwRcvTime: Receive time


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_RegisterClient()
'
'  Register client at driver, get client handle and initialize
'  Rcv-queue (one call per client).
'  Client does not receive messages yet, call 'CAN_RegisterMsg()'
'  for each message to receive.

Public Declare Function CAN_RegisterClient Lib "VCANW32.DLL" _
    (ByVal szName As String, _
     ByVal hwnd As Long, _
     ByRef hClient As Byte) As Integer

    ' szName:  Name of the client
    ' hWnd:    Main window handle of the client
    ' hClient: Resulting client handle


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_ConnectToNet()
'
'  Connect client to a net.
'  Initialize a linked hardware with the baudrate of the net
'  (once per client and net).
'  Error CAN_ERR_HWINUSE if hardware is already occupied by another net.

Public Declare Function CAN_ConnectToNet Lib "VCANW32.DLL" _
    (ByVal hClient As Byte, _
     ByVal szNetName As String, _
     ByRef hNet As Byte) As Integer

    ' hClient:   Connect this client...
    ' szNetName: to this net
    ' hNet:      Resulting net handle


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_RegisterMsg(hClient, hNet, ID)
'
'  Announce that the client wants to receive messages with CAN-ID
'  'ID' from the net 'hNet'.
'  'ID' = 0xFFFF: receive all messages.
'  Each call to this function possibly expands the acceptance filters
'  of afflicted CAN-controllers, which then can change to RESET state
'  for a short time.
'  Remote Request messages that shall be answered must be
'  registered with 'CAN_RegisterMsg()' also.

Public Declare Function CAN_RegisterMsg Lib "VCANW32.DLL" _
    (ByVal hClient As Byte, _
     ByVal hNet As Byte, _
     ByVal ID As Integer) As Integer

    ' hClient: This client...
    ' hNet:    wants to receive from this net...
    ' ID:      messages with this CAN-ID.


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_DisconnectFromNet()
'
'  Disconnect client from a net,
'  do not receive messages any longer.

Public Declare Function CAN_DisconnectFromNet Lib "VCANW32.DLL" _
    (ByVal hClient As Byte, _
     ByVal hNet As Byte) As Integer

    ' hClient: Disconnect this client...
    ' hNet:    ...from this net.


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_RemoveClient()
'
'  Remove client from driver, free resources.
'  Each call to this function possibly narrows the acceptance filters
'  of afflicted CAN-controllers, which then can change to RESET state
'  for a short time.

Public Declare Function CAN_RemoveClient Lib "VCANW32.DLL" _
    (ByVal hClient As Byte) As Integer

    ' hClient: Remove this client.


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_GetDriverName()
'
'  Get the name of the hardware type indexed by 'i' which is
'  supported by the driver.
'  End of list is reached if an empty string is returned.

Public Declare Function CAN_GetDriverName Lib "VCANW32.DLL" _
    (ByVal i As Integer, _
     ByVal szNameBuff As String) As Integer

    ' i:          Index of hardware type (1..n)
    ' szNameBuff: Buffer which takes the name


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_GetNetName()
'
'  Get the name of a net by its handle 'hNet'.


Public Declare Function CAN_GetNetName Lib "VCANW32.DLL" _
    (ByVal hNet As Byte, _
     ByVal szNameBuff As String) As Integer

    ' hNet:       Handle of net
    ' szNameBuff: Buffer which takes the name


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_Msg2Text()
'
'  Debugging: Transform CAN-message to text.

Public Declare Function CAN_Msg2Text Lib "VCANW32.DLL" _
    (ByRef msgbuff As TCANMsg, _
     ByVal sztxtbuff As String) As Integer

    ' msgbuff:   Message to transform
    ' szTxtBuff: Buffer which takes the text


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_GetDiagnostic()
'
'  Debugging: Get the text from the diagnosis textbuffer
'  (max. 255 chars).

Public Declare Function CAN_GetDiagnostic Lib "VCANW32.DLL" _
    (ByVal sztxtbuff As String) As Integer

    ' szTxtBuff: Buffer which takes the text


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_GetSystemTime()
'
'  Access to the function 'Get_System_Time()' of the Virtual Machine
'  Manager. Return the time in milliseconds since Windows start.

Public Declare Function CAN_GetSystemTime Lib "VCANW32.DLL" _
    (ByRef dwTimeBuff As Long) As Integer

    ' dwTimeBuff: Resulting time


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_VersionInfo()
'
'  Returns version and copyright information from driver
'  (max. 255 chars).

Public Declare Function CAN_VersionInfo Lib "VCANW32.DLL" _
    (ByVal sztxtbuff As String) As Integer

    ' sztxtbuff: Buffer which takes the text


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  CAN_GetErrText()
'
'  Transform error flags in 'err' into a string.

Public Declare Function CAN_GetErrText Lib "VCANW32.DLL" _
    (ByVal nErr As Integer, _
     ByVal sztxtbuff As String) As Integer

    ' nErr:      Error flags
    ' szTxtBuff: Buffer which takes the text
