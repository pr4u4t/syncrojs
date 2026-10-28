Attribute VB_Name = "Produzione"
Option Explicit



Public Sub set_dat_ini_pro(indice As Integer)
If indice > 0 And indice <= ord_mac.Count Then
  ord_mac.item(indice).dat_ini = Date
  ord_mac.item(indice).ora_ini = Right$("0" + Trim$(Time), 8)	'ABorelli_FI09/09/2005_1

Else
  log_str "Sub set_dat_ini_pro del modulo Produzione: l'indice passato alla routine non è un indice esatto", MED_PRIORITY, log_lis
End If
End Sub


