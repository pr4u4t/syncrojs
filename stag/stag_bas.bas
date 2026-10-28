Attribute VB_Name = "stag_bas"
Option Explicit

Public Sub set_tag_man()
  If Not (sta_tag_ute.MAC_IN_MANUALE_UTE) Then
    sta_tag_ute.MAC_IN_MANUALE_UTE = True
    messaggio msg("mac_in_man"), False, False
    sta_bar.lin_no_com_spde.BorderColor = RED_GRU_ALL
  Else
    sta_tag_ute.MAC_IN_MANUALE_UTE = False
    messaggio msg("mac_in_auto"), False, False
    sta_bar.lin_no_com_spde.BorderColor = vbBlack
  End If
End Sub


