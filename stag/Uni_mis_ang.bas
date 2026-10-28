Attribute VB_Name = "Uni_mis_ang"
Option Explicit

'Modulo per la gestione delle unità di misura Anglosassoni

  
  
'variabile che stabilisce quale unità di misura usare
'False=si utilizza quella standard
Public abi_mis_ang  As Boolean

Public Const INCHMM = 25.4    'mm / INCHMM = inch
Public Const FEETMT = 3.2808  'mt * FEETMT = feet
Const POS_CAR_APP = 4
Function appr(s As String, inc_dec As Boolean) As String
Dim tmp As String
Dim i As Integer
Dim ini As String
Dim fin As String

i = InStr(1, s, ",")
If i <= 0 Then appr = s: Exit Function 's non ha il carattere ","
ini = Left$(s, i - 1)
fin = Right$(s, Len(s) - i)
If Not inc_dec Then
  If Len(fin) = 3 Then
    appr = ini & "." & Left$(fin, POS_CAR_APP - 1)
  Else
    If val(Mid$(fin, POS_CAR_APP, 1)) >= 5 Then 'da decimale ad inch
      appr = ini & "." & Format$((val(Left$(fin, POS_CAR_APP - 1)) + 1), "000")
    Else
      appr = ini & "." & Left$(fin, POS_CAR_APP - 1)
    End If
  End If
Else
  If val(Left$(fin, 1)) >= 5 Then 'da inch a decimale
    appr = CStr(val(ini) + 1)
  Else
    appr = ini
  End If
End If

End Function
Function con_dmm_to_inc(dmm As String)
Dim s As String
Dim i As Integer
Dim ini As String
Dim fin As String
Dim tmp As String

s = Format(val(dmm) / (INCHMM * TODECMM), "#######0." & String$(POS_CAR_APP, "0"))

con_dmm_to_inc = appr(s, False)

'tmp = Right$(s, Len(s) - i)
'Select Case tmp
'  Case "030", "032"
'    fin = "031"
'  Case "062", "064"
'    fin = "063"
'  Case "093", "095"
'    fin = "094"
'  Case "155", "157"
'    fin = "156"
'  Case "187", "189"
'    fin = "188"
'  Case "218", "220"
'    fin = "219"
'  Case "249", "251"
'    fin = "250"
'  Case "280", "282"
'    fin = "281"
'  Case "312", "314"
'    fin = "313"
'  Case "343", "345"
'    fin = "344"
'  Case "374", "376"
'    fin = "375"
'  Case "405", "407"
'    fin = "406"
'  Case "437", "439"
'    fin = "438"
'  Case "468", "470"
'    fin = "469"
'  Case "499", "501"
'    fin = "500"
'  Case "530", "532"
'    fin = "531"
'  Case "562", "564"
'    fin = "563"
'  Case "593", "595"
'    fin = "594"
'  Case "624", "626"
'    fin = "625"
'  Case "655", "657"
'    fin = "656"
'  Case "687", "689"
'    fin = "688"
'  Case "718", "720"
'    fin = "719"
'  Case "750", "752"
'    fin = "750"
'  Case "780", "782"
'    fin = "781"
'  Case "811", "813"
'    fin = "812"
'  Case "843", "845"
'    fin = "844"
'  Case "874", "876"
'    fin = "875"
'  Case "905", "907"
'    fin = "906"
'  Case "937", "939"
'    fin = "938"
'  Case "968", "970"
'    fin = "969"
'   Case Else
'    fin = tmp
'End Select

End Function


Function con_inc_to_dmm(inc As String) As String
'da inch a decimi di mm sono eliminate le cifre decimali
Dim s As String
Dim i As Integer
Dim ini As String
Dim fin As String
Dim car_fin As String
Dim m As Integer

s = Format$(CDbl(val(inc) * TODECMM * INCHMM))

con_inc_to_dmm = appr(s, True)

End Function

Function con_mis_crd_dmm_to_mm(rig_in As String) As String

  'Restituisce TRUE se non ci sono problemi
  Dim i As Integer
  Dim rig_loc As String
  Dim num As String
  Dim let_aux As String
  Dim rig_out As String
  rig_loc = Trim$(rig_in)
  i = 1
  
  'And i <= max_nel
  While Len(rig_loc) > 0
    'scarto gli spazi
    While Left$(rig_loc, 1) = " " And Len(rig_loc) > 0
      rig_loc = Mid$(rig_loc, 2)
    Wend
    num = ""
    While Left$(rig_loc, 1) <> " " And Len(rig_loc) > 0
      num = num + Left$(rig_loc, 1)
      rig_loc = Mid$(rig_loc, 2)
    Wend
    
    ' num contiene la quota da analizzare
    ' memorizza la lettera del cordone sull'asse aux
      let_aux = Right$(num, 1)
      If let_aux >= "A" And let_aux <= "D" Or _
      (UCase(let_aux) = "S") Then '3508Arturo 30-05-02
        num = Left$(num, Len(num) - 1)
      Else
        let_aux = ""
      End If
    
      num = Format$(val(num) \ TODECMM)
      num = " " + num + let_aux
      rig_out = rig_out + num
    
  Wend
  con_mis_crd_dmm_to_mm = rig_out
End Function



Function con_mis_crd_mm_to_dmm(rig_in As String) As String

  'Restituisce TRUE se non ci sono problemi
  Dim i As Integer
  Dim rig_loc As String
  Dim num As String
  Dim let_aux As String
  Dim rig_out As String
  rig_loc = Trim$(rig_in)
  i = 1
  
  'And i <= max_nel
  While Len(rig_loc) > 0
    'scarto gli spazi
    While Left$(rig_loc, 1) = " " And Len(rig_loc) > 0
      rig_loc = Mid$(rig_loc, 2)
    Wend
    num = ""
    While Left$(rig_loc, 1) <> " " And Len(rig_loc) > 0
      num = num + Left$(rig_loc, 1)
      rig_loc = Mid$(rig_loc, 2)
    Wend
    
    ' num contiene la quota da analizzare
    ' memorizza la lettera del cordone sull'asse aux
      let_aux = Right$(num, 1)
      If let_aux >= "A" And let_aux <= "D" Or _
      (UCase(let_aux) = "S") Then '3508Arturo 30-05-02
        num = Left$(num, Len(num) - 1)
      Else
        let_aux = ""
      End If
    
    
      num = Format$(val(num) * TODECMM)
      num = " " + num + let_aux
      rig_out = rig_out + num
    
  Wend
  con_mis_crd_mm_to_dmm = rig_out
End Function





Function con_mm_to_inc(mm As String) As String
'da mm a inch fino a 3 cifre decimali
Dim s As String
Dim i As Integer
Dim ini As String
Dim fin As String
Dim tmp As String

s = Format(val(mm) / (INCHMM), "#######0.000")
i = InStr(1, s, ",")
ini = Left$(s, i - 1)
tmp = Right$(s, Len(s) - i)

fin = tmp

s = ini + "." + fin

con_mm_to_inc = s

End Function


Function con_fet_to_met(fet As String) As String
'converte i piedi in metri eliminando le cifre decimali

con_fet_to_met = Format$(val(fet) / FEETMT, "#######")

End Function


Function con_inc_to_mm(inc As String) As String
'da inch a decimi di mm sono eliminate le cifre decimali
Dim s As String
Dim i As Integer
Dim ini As String
Dim fin As String


s = Format$(CLng(val(inc) * INCHMM))

'i = InStr(1, s, ",")
'ini = Left$(s, i - 1)
'fin = Right$(s, Len(s) - i)
's = ini '+ "." + fin

con_inc_to_mm = s
End Function


Function con_mis_crd_eur_ang(rig_in As String, abi_ang As Boolean) As String
  'Restituisce TRUE se non ci sono problemi
  Dim i As Integer
  Dim rig_loc As String
  Dim num As String
  Dim let_aux As String
  Dim rig_out As String
  rig_loc = Trim$(rig_in)
  i = 1
  
  'And i <= max_nel
  While Len(rig_loc) > 0
    'scarto gli spazi
    While Left$(rig_loc, 1) = " " And Len(rig_loc) > 0
      rig_loc = Mid$(rig_loc, 2)
    Wend
    num = ""
    While Left$(rig_loc, 1) <> " " And Len(rig_loc) > 0
      num = num + Left$(rig_loc, 1)
      rig_loc = Mid$(rig_loc, 2)
    Wend
    
    ' num contiene la quota da analizzare
    ' memorizza la lettera del cordone sull'asse aux
      let_aux = Right$(num, 1)
      If let_aux >= "A" And let_aux <= "D" Or _
      (UCase(let_aux) = "S") Then '3508Arturo 30-05-02
        num = Left$(num, Len(num) - 1)
      Else
        let_aux = ""
      End If
    
    
    If abi_ang Then
      num = con_dmm_to_inc(num)
      num = " " + num + let_aux
      rig_out = rig_out + num
    Else
      num = con_inc_to_dmm(num)
      num = " " + num + let_aux
      rig_out = rig_out + num
    End If
    
  Wend
  con_mis_crd_eur_ang = rig_out
End Function


Function con_met_to_fet(met As String) As String
'converte i metri in piedi eliminando le cifre decimali

con_met_to_fet = Format$(val(met) * FEETMT, "######0")

End Function

Function con_met_qua_to_squ_fet(met_qua As String) As String
'converte i metri in piedi eliminando le cifre decimali

con_met_qua_to_squ_fet = Format$(val(met_qua) * FEETMT * FEETMT \ 1, "######0")     ' ABorelli_FI09/09/2005_1


End Function




