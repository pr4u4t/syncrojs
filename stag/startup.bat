@echo Starting STAG...Please Wait
@echo off
rem c:\programmi\pcanywhere\data\modem.bhf
rem c:\programmi\pcanywhere\data\rete.bhf
choice /n /t:n,10
if errorlevel 2 goto esegui
:esegui
start c:\usr\STAG\STAG.exe 
rem start c:\programmi\devstudio\vb\vb5.exe /r c:\usr\STAG\STAG.vbp