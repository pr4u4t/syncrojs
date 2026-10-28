VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "par_new"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Index As Long
Private subindex As Long
Private descrizione As String * 50
Private par_imp_pre As Boolean
Private div As Boolean
Sub change_par_new(par_index As Long, par_subindex As Long)
If Index <> par_index Or subindex <> par_subindex Then
    Index = par_index
    subindex = par_subindex
    setFileINIString "com_azi", "par_new_azi_index", Index, app.EXEName
    setFileINIString "com_azi", "par_new_azi_subindex", subindex, app.EXEName
End If
End Sub

Sub change_descr(descr As String)
If descrizione <> descr Then
    descrizione = descr
    setFileINIString "com_azi", "par_new_azi_descr", descr, app.EXEName
End If
End Sub

Friend Property Get get_index() As Long
get_index = Index
End Property


Friend Property Get get_descr() As String
get_descr = descrizione
End Property
Friend Property Get get_subindex() As Long
get_subindex = subindex
End Property

Friend Property Get par_pre() As Boolean
par_pre = par_imp_pre
End Property

Friend Property Get dividi() As Boolean
dividi = div
End Property
Friend Property Let par_pre(pre As Boolean)
par_imp_pre = pre
End Property
Friend Property Let dividi(s As Boolean)
div = s
End Property
Private Sub Class_Initialize()
Index = val(getFileINIString("com_azi", "par_new_azi_index", "0"))
subindex = val(getFileINIString("com_azi", "par_new_azi_subindex", "0"))
descrizione = getFileINIString("com_azi", "par_new_azi_descr", "")
par_imp_pre = True
div = True
End Sub


