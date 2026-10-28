rem net send /DOMAIN "Server will go down due to extended powerfailure !"
rem This command send a message to all users in your DOMAIN
rem
rem SYNTAX: /USER
rem         /DOMAIN             Send message to all computers in your domain          
rem         /DOMAIN:PDC1        Send message to your special domain group
rem                             More information use "net help send" 
rem
rem Interplatform shutdown command for remote NT hosts. This is a example 
rem how to shutdown any NT computer in a homogene NT network
rem c:\ups\shutcmd.exe \\NT_COMPUTERNAME /T:05 /C /Y "UPS Alarm shutdown!"
rem 
rem
rem 
rem Internetwork shutdown command for any computer via RCCMD send command. This
rem is a example how to send a shutdown signal to any other computer in a TCPIP
rem network, running RCCMD in listening mode.
rem c:\ups\RCCMD -s -a 192.10.200.3
rem 
rem
rem This command makes a shutdown to of your local NT computer after all remote
rem shutdown commands have been executet. This command closes all applications
rem without further input request. Do not use longer messages that 20 characters !
rem 
rem SYNTAX: \\COMPUTER          Enter NT computername to send shutdown commnad
rem         /L                  Local NT computer shutdown only
rem         /T:xx               Time delay for shutdown computer in seconds
rem         /C                  Force open applications to close and exit
rem 	    /Y                  Dont request keyboard input
rem  
%1\shutcmd.exe  /L /Y /C /T:20 "Chiusura in corso  Shutdown in progress !"
