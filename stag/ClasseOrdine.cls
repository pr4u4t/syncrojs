VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "ClasseOrdine"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit
Private order As Tip_Ordine


Friend Property Get tem_fer() As String
' ABorelli_FI09/09/2005_1
    tem_fer = order.rep_pro.tem_fer
End Property
Friend Property Get tem_mar() As String
' ABorelli_FI09/09/2005_1
    tem_mar = order.rep_pro.tem_mar
End Property
Friend Property Get num_fer() As String
' ABorelli_FI09/09/2005_1
    num_fer = order.rep_pro.num_fer
End Property
Friend Property Let tem_fer(d As String)
' ABorelli_FI09/09/2005_1
    order.rep_pro.tem_fer = d
End Property
Friend Property Let tem_mar(d As String)
' ABorelli_FI09/09/2005_1
    order.rep_pro.tem_mar = d
End Property
Friend Property Let num_fer(d As String)
' ABorelli_FI09/09/2005_1
    order.rep_pro.num_fer = d
End Property



Friend Property Let dat_fin(d As String)
order.rep_pro.dat_fin = d
End Property

Friend Property Let dat_ini(d As String)
order.rep_pro.dat_ini = d
End Property
Friend Property Get met(liv As Integer) As Integer
met = order.ord(liv).met
End Property

Friend Property Get met_pro() As String
met_pro = order.rep_pro.met_tot
End Property

Friend Property Get met_tot_ord() As String
met_tot_ord = order.met_tot_ord
End Property
Friend Property Get fog_buo(liv As Integer) As String
fog_buo = order.rep_pro.liv(liv).fog_buo
End Property
Friend Property Get fog_sca(liv As Integer) As String
fog_sca = order.rep_pro.liv(liv).fog_sca
End Property

'RFAZZI_FI 09-01-08
Friend Property Get num_mil_sca(liv As Integer) As String
num_mil_sca = order.rep_pro.liv(liv).mil_sca
End Property

Friend Property Get fog_err(liv As Integer) As String
fog_err = order.rep_pro.liv(liv).fog_err
End Property
Friend Property Let fog_err(liv As Integer, val As String)
order.rep_pro.liv(liv).fog_err = val
End Property
Friend Property Let fog_sca(liv As Integer, val As String)
order.rep_pro.liv(liv).fog_sca = val
End Property


'RFAZZI_FI 09-01-08
Friend Property Let num_mil_sca(liv As Integer, val As String)
order.rep_pro.liv(liv).mil_sca = val
End Property

Friend Property Let fog_buo(liv As Integer, val As String)
order.rep_pro.liv(liv).fog_buo = val
End Property
Friend Property Let ora_fin(o As String)
order.rep_pro.ora_fin = o
End Property
Friend Property Let met_pro(met As String)
order.rep_pro.met_tot = met
End Property
Friend Property Let met_tot_ord(met As String)
order.met_tot_ord = met
End Property
Friend Property Let ora_ini(o As String)
order.rep_pro.ora_ini = o
End Property

Friend Property Let vel_med(vel As String)
order.rep_pro.vel_med = Format$(vel, FT3)
End Property
Friend Property Get dat_fin() As String
dat_fin = order.rep_pro.dat_fin
End Property
Friend Property Get dat_ini() As String
dat_ini = order.rep_pro.dat_ini
End Property
Friend Property Get ind_liv(ind_for As Integer) As Integer
If Trim$(order.ord(ind_for).Ord_liv) <> "" Then
  Select Case val(order.ord(ind_for).Ord_liv)
    Case LIV_ALT
      ind_liv = 3
    Case LIV_MED
      ind_liv = 2
    Case LIV_BAS
      ind_liv = 1
    Case Else
      ind_liv = 0
  End Select
Else
  ind_liv = 0
End If
End Property

Friend Property Get ora_fin() As String
ora_fin = order.rep_pro.ora_fin
End Property

Friend Property Get ora_ini() As String
ora_ini = order.rep_pro.ora_ini
End Property

Property Get spde_id() As String
spde_id = order.spde_id
End Property

'RFAZZI_FI 05-01-08
Property Get spde_ModID() As String
spde_ModID = order.spde_ModID
End Property


Friend Property Let cod_com_car(v As String)
  order.tip_car.cod_com_car = Format$(v, FT36)
End Property


Friend Property Let Pes_mt2(v As Integer)
  order.tip_car.Pes_mt2 = Format$(v, FT4)
End Property
Friend Property Let Alt_ond(v As Integer)
  order.tip_car.Alt_ond = Format$(v, FT3)
End Property
Friend Property Let cod_com_ord(i As Integer, v As String)
  order.ord(i).com.cod_com_ord = Format$(v, FT15)
End Property


Friend Property Let cod_cli(i As Integer, v As String)
  order.ord(i).com.cod_cli = Format$(v, FT12)
End Property
Friend Property Let alt_bob(v As Integer)
  order.alt_bob = Format$(v, FT5)
End Property

Property Get stag_id() As String
stag_id = order.stag_id
End Property

Property Get old_stag_id() As String
old_stag_id = order.old_stag_id
End Property



Friend Property Let Ord_liv(i As Integer, v As Integer)
  order.ord(i).Ord_liv = Format$(v, FT1)
End Property
Friend Property Get Ord_liv(i As Integer) As Integer
  Ord_liv = val(order.ord(i).Ord_liv)
End Property

Friend Property Get vel_med() As String
vel_med = order.rep_pro.vel_med
End Property

Friend Property Let vel_pro(v As Integer)
  order.tip_car.vel_pro = Format$(v, FT3)
End Property
Friend Property Let Cod_ond(v As String)
  order.tip_car.Cod_ond = Format$(v, FT3)
End Property

Friend Property Get Cod_ond() As String
  Cod_ond = order.tip_car.Cod_ond
End Property



Friend Property Let Vel_aoc(v As Integer)
  order.tip_car.Vel_aoc = Format$(v, FT3)
End Property
Friend Property Get Vel_aoc() As Integer
  Vel_aoc = val(order.tip_car.Vel_aoc)
End Property
Friend Property Get vel_pro() As Integer
  vel_pro = val(order.tip_car.vel_pro)
End Property















Friend Property Get Alt_ond() As Integer
  Alt_ond = val(order.tip_car.Alt_ond)
End Property

Friend Property Get alt_bob() As Integer
  alt_bob = val(order.alt_bob)
End Property
Friend Property Get cut_to_mark(Optional ByVal i As Variant) As Integer

Dim j As Integer

  If Not IsMissing(i) Then
    cut_to_mark = val(order.ord(i).cut_to_mark)
  Else
    For j = 1 To 3
      If val(order.ord(j).cut_to_mark) <> 0 And cut_to_mark < val(order.ord(j).cut_to_mark) Then cut_to_mark = val(order.ord(j).cut_to_mark)
    Next
  End If
End Property

Friend Property Let cut_to_mark(ByVal i As Variant, v As Integer)
  order.ord(i).cut_to_mark = Format$(v, FT1)
End Property
Friend Property Let fst_FTC(i As Integer, v As Integer)
'Iciliouff 20-11-2003
  order.ord(i).fst_FTC = Format$(v, FT1)
End Property
Friend Property Let pat_cod(i As Integer, v As String)
'Iciliouff 20-11-2003
  order.ord(i).pat_cod = v
End Property
Friend Property Let pat_con(i As Integer, v As Integer)
'Iciliouff 20-11-2003
  order.ord(i).pat_con = v
End Property
Friend Property Get pat_cod(i As Integer) As String
'Iciliouff 20-11-2003
  pat_cod = order.ord(i).pat_cod
End Property
Friend Property Get pat_con(i As Integer) As Integer
'Iciliouff 20-11-2003
  pat_con = val(order.ord(i).pat_con)
End Property
Friend Property Get fst_FTC(i As Integer) As Integer
'Iciliouff 20-11-2003
  fst_FTC = val(order.ord(i).fst_FTC)
End Property
Friend Property Get Ofs_Cut(i As Integer) As Integer
'Iciliouff 20-11-2003
  Ofs_Cut = val(order.ord(i).Ofs_Cut)
End Property
Friend Property Let Ofs_Cut(i As Integer, v As Integer)
'Iciliouff 20-11-2003
  order.ord(i).Ofs_Cut = Format$(v)
End Property
Friend Property Let L1(i As Integer, v As Integer)
'Iciliouff 20-11-2003
  order.ord(i).L1 = Format$(v)
End Property
Friend Property Get L1(i As Integer) As Integer
'Iciliouff 20-11-2003
  L1 = val(order.ord(i).L1)
End Property
Friend Property Get L2(i As Integer) As Integer
'Iciliouff 20-11-2003
  L2 = val(order.ord(i).L2)
End Property
Friend Property Get L3(i As Integer) As Integer
'Iciliouff 20-11-2003
  L3 = val(order.ord(i).L3)
End Property
Friend Property Get L4(i As Integer) As Integer
'Iciliouff 20-11-2003
  L4 = val(order.ord(i).L4)
End Property
Friend Property Get L5(i As Integer) As Integer
'Iciliouff 20-11-2003
  L5 = val(order.ord(i).L5)
End Property
Friend Property Get L6(i As Integer) As Integer
'Iciliouff 20-11-2003
  L6 = val(order.ord(i).L6)
End Property
Friend Property Get L7(i As Integer) As Integer
'Iciliouff 20-11-2003
  L7 = val(order.ord(i).L7)
End Property
Friend Property Get L8(i As Integer) As Integer
'Iciliouff 20-11-2003
  L8 = val(order.ord(i).L8)
End Property
Friend Property Get L9(i As Integer) As Integer
'Iciliouff 20-11-2003
  L9 = val(order.ord(i).L9)
End Property
Friend Property Get L10(i As Integer) As Integer
'Iciliouff 20-11-2003
  L10 = val(order.ord(i).L10)
End Property
Friend Property Get pos_pattern(i As Integer) As Integer
'Robertouff 26-11-2007
  pos_pattern = val(order.ord(i).pos_pattern)
End Property
Friend Property Get fin_pri(i As Integer) As Integer
'Iciliouff 20-11-2003
  fin_pri = val(order.ord(i).fin_pri)
End Property
Friend Property Get fin_dop(i As Integer) As Integer
'Iciliouff 20-11-2003
  fin_dop = val(order.ord(i).fin_dop)
End Property
Friend Property Let fin_dop(i As Integer, v As Integer)
'Iciliouff 20-11-2003
  order.ord(i).fin_dop = Format$(v)
End Property
Friend Property Let fin_pri(i As Integer, v As Integer)
'Iciliouff 20-11-2003
  order.ord(i).fin_pri = Format$(v)
End Property

Friend Property Let L2(i As Integer, v As Integer)
'Iciliouff 20-11-2003
  order.ord(i).L2 = Format$(v)
End Property
Friend Property Let L3(i As Integer, v As Integer)
'Iciliouff 20-11-2003
  order.ord(i).L3 = Format$(v)
End Property
Friend Property Let L4(i As Integer, v As Integer)
'Iciliouff 20-11-2003
  order.ord(i).L4 = Format$(v)
End Property
Friend Property Let L5(i As Integer, v As Integer)
'Iciliouff 20-11-2003
  order.ord(i).L5 = Format$(v)
End Property
Friend Property Let L6(i As Integer, v As Integer)
'Iciliouff 20-11-2003
  order.ord(i).L6 = Format$(v)
End Property
Friend Property Let L7(i As Integer, v As Integer)
'Iciliouff 20-11-2003
  order.ord(i).L7 = Format$(v)
End Property
Friend Property Let L8(i As Integer, v As Integer)
'Iciliouff 20-11-2003
  order.ord(i).L8 = Format$(v)
End Property
Friend Property Let L9(i As Integer, v As Integer)
'Iciliouff 20-11-2003
  order.ord(i).L9 = Format$(v)
End Property
Friend Property Let L10(i As Integer, v As Integer)
'Iciliouff 20-11-2003
  order.ord(i).L10 = Format$(v)
End Property
Friend Property Let pos_pattern(i As Integer, v As Integer)
'Robertouff 26-11-2007
  order.ord(i).pos_pattern = Format$(v)
End Property
Friend Property Get Pes_mt2() As Integer
  Pes_mt2 = val(order.tip_car.Pes_mt2)
End Property

Friend Property Get cod_com_car() As String
  cod_com_car = order.tip_car.cod_com_car
End Property


Friend Property Let Sca_per_tag(i As Integer, v As Integer)
  order.ord(i).Sca_per_tag = Format$(v, FT1)
End Property
Friend Property Get Sca_per_tag(i As Integer) As Integer
  Sca_per_tag = val(order.ord(i).Sca_per_tag)
End Property

Friend Property Let nom_cli(i As Integer, v As String)
  order.ord(i).nom_cli = Format$(v, FT30)
End Property









Friend Property Get cod_com_ord(i As Integer) As String
  cod_com_ord = order.ord(i).com.cod_com_ord
End Property
Friend Property Get cod_cli(i As Integer) As String
  cod_cli = order.ord(i).com.cod_cli
End Property

Friend Property Get nom_cli(i As Integer) As String
  nom_cli = order.ord(i).nom_cli
End Property
Friend Property Let sca_lun(i As Integer, v As Long)
  order.ord(i).sca_lun = Format$(v, FT5)
End Property

Friend Property Let sca_lar(i As Integer, v As Integer)
  order.ord(i).sca_lar = Format$(v, FT5)
End Property












Friend Property Get sca_tot_num(i As Integer) As Long
  sca_tot_num = val(order.ord(i).sca_tot_num)
End Property



























Friend Property Get sca_lar(i As Integer) As Integer
  sca_lar = val(order.ord(i).sca_lar)
End Property
Friend Property Let sca_tot_num(i As Integer, v As Long)
  order.ord(i).sca_tot_num = Format$(v, FT6)
End Property
Friend Property Get sca_lun(i As Integer) As Long
  sca_lun = val(order.ord(i).sca_lun)
End Property

Friend Property Let Cod_cod_ond(v As String)
  order.tip_car.Cod_cod_ond = Format$(v, FT4)
End Property
Friend Property Get Cod_cod_ond() As String
  Cod_cod_ond = order.tip_car.Cod_cod_ond
End Property

Friend Property Let cod_ord_abb(v As String)
  order.cod_ord_abb = Format$(v, FT11)
End Property
Friend Property Get cod_ord_abb() As String
  cod_ord_abb = order.cod_ord_abb
End Property

Friend Property Get ordine() As String
Dim str_ord As String
  str_ord = Space$(Len(order))
  CopyString str_ord, order, Len(order)
  ordine = str_ord
End Property

Friend Function Alt_ord() As Integer
'Iciliouff 20-11-2003
'Restituisce altezza ordine
Dim i As Integer
Dim alt_ord_liv As Integer, alt_ord_tot As Integer
  
  For i = 1 To 3
    alt_ord_liv = val(order.ord(i).sca_lar) * val(order.ord(i).Sca_per_tag)
    alt_ord_tot = alt_ord_tot + alt_ord_liv
  Next i
  Alt_ord = alt_ord_tot
  
End Function

Friend Property Let ordine(new_ord As String)
  Dim tmp_ord As Tip_Ordine
  If Len(new_ord) = Len(tmp_ord) Then
    CopyType tmp_ord, new_ord, Len(new_ord)
    order = tmp_ord
  Else
    log_str "Let Ordine: stringa di lunghezza non valida", MED_PRIORITY, log_ord
  End If
End Property



Property Get mod_id() As String
  mod_id = order.mod_id
End Property


Property Get old_mod_id() As String
  old_mod_id = order.old_mod_id
End Property
Property Let mod_id(v As String)
  order.mod_id = Format$(v, FT2)
End Property

Property Let old_mod_id(v As String)
  order.old_mod_id = Format$(v, FT2)
End Property


Property Let stag_id(v As String)
  order.stag_id = Format$(v, FT4)
  'agrassi_FI05-09-2011_1
  If par_mac.com_spde.ver_spde = Simon10 Or par_mac.com_spde.ver_spde = MHI10 Then
    Me.cod_ord_abb = Format$(order.stag_id, "0000") 'MLamioni_FI03/11/2010 Emulazione con protocollo taglierina SIMON
  End If
End Property
Property Let old_stag_id(v As String)
order.old_stag_id = Format$(v, FT4)
End Property

Property Let spde_id(v As String)
order.spde_id = Format$(v, FT4)
End Property

'RFAZZI_FI 05-01-08
Property Let spde_ModID(v As String)
order.spde_ModID = Format$(v, FT4)
End Property

