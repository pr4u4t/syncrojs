Attribute VB_Name = "API_Calls"
Option Explicit

Public Const ERROR_SUCCESS = 0

'Matteo F. 18/02/02
Public Const HKEY_CLASSES_ROOT = &H80000000
Public Const HKEY_CURRENT_USER = &H80000001
Public Const HKEY_LOCAL_MACHINE = &H80000002
Public Const HKEY_USERS = &H80000003
Public Const HKEY_PERFORMANCE_DATA = &H80000004
'--
Public Const WM_QUIT = &H12
Public Const WM_MBUTTONDBLCLK = &H209

Public Const SW_SHOWNOACTIVATE = 4
Public Const SW_HIDE = 4

Public Const VK_SHIFT = &H10
Public Const VK_TAB = &H9
Public Const VK_NUMLOCK = &H90
Public Const KEYEVENTF_KEYUP = &H2
Public Const EM_SETREADONLY = &HCF

Public Const SOL_SOCKET = &HFFFF& ' 0xffff options for socket level
Public Const SO_LINGER% = &H80          '* linger on close if data present  **
Public Const VER_REQUIRED = &H101

'Iciliouff 1-12-2001 Aggiunto per tasto funzione .INI
Public Const RESOURCE_CONNECTED As Long = &H1&
Public Const RESOURCE_GLOBALNET As Long = &H2&
Public Const RESOURCETYPE_DISK = &H1
Public Const RESOURCEDISPLAYTYPE_SHARE = &H3
Public Const RESOURCEUSAGE_CONNECTABLE = &H1
Public Const CONNECT_UPDATE_PROFILE = &H1

Type POINTAPI
        X As Long
        y As Long
End Type

Type RECT
        Left As Long
        Top As Long
        Right As Long
        Bottom As Long
End Type


Type WINDOWPLACEMENT
        Length As Long
        flags As Long
        showCmd As Long
        ptMinPosition As POINTAPI
        ptMaxPosition As POINTAPI
        rcNormalPosition As RECT
End Type

Type MEMORYSTATUS
  dwLength As Long '        // sizeof(MEMORYSTATUS)
  dwMemoryLoad As Long '    // percent of memory in use
  dwTotalPhys As Long '     // bytes of physical memory
  dwAvailPhys As Long  '     // free physical memory bytes
  dwTotalPageFile As Long  ' // bytes of paging file
  dwAvailPageFile As Long ' // free bytes of paging file
  dwTotalVirtual As Long '  // user bytes of address space
  dwAvailVirtual As Long '  // free user bytes
End Type

Type WSAdata_type
   wVersion As Integer
   wHighVersion As Integer
   szDescription As String * 257
   szSystemStatus As String * 129
   iMaxSockets As Integer
   iMaxUdpDg As Long
   lpVendorInfo As String * 200
End Type

Type LingerType
    l_onoff As Integer
    l_linger As Integer
End Type

'Iciliouff 1-12-2001 Aggiunto per tasto funzione .INI
Type NETRESOURCE
    dwScope As Long
    dwType As Long
    dwDisplayType As Long
    dwUsage As Long
    lpLocalName As String
    lpRemoteName As String
    lpComment As String
    lpProvider As String
End Type

'Dichiarazioni delle chiamate alle API
Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Long, ByVal lpFileName As String) As Long
Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Long
Declare Function GetTickCount Lib "kernel32" () As Long
Declare Sub CopyString Lib "kernel32" Alias "RtlMoveMemory" (ByVal hpvDest As String, hpvSource As Any, ByVal cbCopy As Long) 'cbCopy è la lunghezza del sorgente (e del destinatario)
Declare Sub CopyType Lib "kernel32" Alias "RtlMoveMemory" (hpvDest As Any, ByVal hpvSource As String, ByVal cbCopy As Long)  'cbCopy è la lunghezza del sorgente (e del destinatario)
Declare Function GetWindowPlacement Lib "user32" (ByVal hwnd As Long, lpwndpl As WINDOWPLACEMENT) As Long
Declare Function SetWindowPlacement Lib "user32" (ByVal hwnd As Long, lpwndpl As WINDOWPLACEMENT) As Long
Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Long, ByVal dwExtraInfo As Long)
Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Long, ByVal wMapType As Long) As Long
Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, Param As Any) As Long
Declare Sub GlobalMemoryStatus Lib "kernel32" (lpBuffer As MEMORYSTATUS)
Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As Long) As Long
'Declare Function GetForegroundWindow Lib "user32" () As Long 'NON USATA
Declare Function RegOpenKeyEx Lib "advapi32.dll" Alias "RegOpenKeyExA" (ByVal Hkey As Long, ByVal lpSubKey As String, ByVal ulOptions As Long, ByVal samDesired As Long, phkResult As Long) As Long
Declare Function RegSetValueEx Lib "advapi32.dll" Alias "RegSetValueExA" (ByVal Hkey As Long, ByVal lpValueName As String, ByVal Reserved As Long, ByVal dwType As Long, lpData As Any, ByVal cbData As Long) As Long         ' Note that if you declare the lpData parameter as String, you must pass it By Value.
Declare Function RegOpenKey Lib "advapi32.dll" Alias "RegOpenKeyA" (ByVal Hkey As Long, ByVal lpSubKey As String, phkResult As Long) As Long
Declare Function RegQueryValueEx Lib "advapi32.dll" Alias "RegQueryValueExA" (ByVal Hkey As Long, ByVal lpValueName As String, ByVal lpReserved As Long, lpType As Long, lpData As Any, lpcbData As Long) As Long
Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Long
Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hwnd As Long, lpdwProcessId As Long) As Long
Declare Function PostThreadMessage Lib "user32" Alias "PostThreadMessageA" (ByVal idThread As Long, ByVal msg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
Declare Function GetTempFileName Lib "kernel32" Alias "GetTempFileNameA" (ByVal lpszPath As String, ByVal lpPrefixString As String, ByVal wUnique As Long, ByVal lpTempFileName As String) As Long

Declare Function setsockopt Lib "wsock32.dll" (ByVal s As Long, ByVal level As Long, ByVal optname As Long, optval As Any, ByVal optlen As Long) As Long
Declare Function WSAStartup Lib "wsock32.dll" (ByVal a As Long, b As WSAdata_type) As Integer
Declare Function WSACleanup Lib "wsock32.dll" () As Integer

Declare Function GetKeyState Lib "user32.dll" (ByVal nVirtKey As Long) As Integer

'Iciliouff 1-12-2001 Aggiunto per tasto funzione .INI
Declare Function WNetAddConnection2 Lib "mpr.dll" Alias "WNetAddConnection2A" (lpNetResource As NETRESOURCE, ByVal lpPassword As String, ByVal lpUserName As String, ByVal dwFlags As Long) As Long
Declare Function WNetCancelConnection2 Lib "mpr.dll" Alias "WNetCancelConnection2A" (ByVal lpName As String, ByVal dwFlags As Long, ByVal fForce As Long) As Long
Declare Function CopyFile Lib "kernel32" Alias "CopyFileA" (ByVal lpExistingFileName As String, ByVal lpNewFileName As String, ByVal bFailIfExists As Long) As Long


