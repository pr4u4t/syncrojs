VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "com_plc"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private com_plc As Tip_Com_PLC


Friend Function leggi_com_plc() As Tip_Com_PLC
leggi_com_plc = com_plc
End Function

Friend Sub scrivi_com_plc(chiamante As rmt, dati As Tip_Com_PLC)
If com_plc.obj Is Nothing Then
  Set com_plc.obj = chiamante
End If
If com_plc.obj Is chiamante Then
  com_plc = dati
End If
End Sub

