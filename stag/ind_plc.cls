VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "ind_plc"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private net_id As Integer
Private node_id As Integer
Private unit_id As Integer

Public Event newplc(ind_rete As Integer, ind_nodo As Integer, ind_unita As Integer)



Sub change_ind_plc(rete As Integer, nodo As Integer, unita As Integer)

If net_id <> rete Or node_id <> nodo Or unit_id <> unita Then
    old_ind.net_id = net_id
    old_ind.node_id = node_id
    old_ind.unit_id = unit_id
    net_id = rete
    node_id = nodo
    unit_id = unita
    RaiseEvent newplc(net_id, node_id, unit_id)
End If
      'ABorelli_FI17/11/2006_1

If par_mac.com_plc_lan.abi_lan <> 0 Then
    RaiseEvent newplc(net_id, node_id, unit_id)
End If
End Sub


