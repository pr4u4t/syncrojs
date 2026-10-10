//NO Console
#pragma comment( linker, "/subsystem:\"windows\" /entry:\"wmainCRTStartup\"" ) 

//For Memory Leak detection
//#include <vld.h>

//Validation function
#include "..\Include\ValidationExport.h"

//Commands manager include
#include "C_CommunicationManager.h"

//a CommandsManager objects
C_CommunicationManager communicationManager;
bool ShutDownInProgress = false;
bool finished=false;

//Task Settings include
#include "..\Shared\C_SMTaskSettings.h"
TaskSettings::Settings::SyncroSSettings syncroSSettings;

//Logger include
#include "..\Shared\C_Logger.h"

#include "..\Include\PortXP.h"

//objects for task settings
TaskSettings::C_SMTaskSettings SMTaskSettings;

//Object for log
Logger::C_Logger logger;

//a Socket objects
Network::C_SocketTCP o_socket;

wchar_t dummy[20];

HANDLE registerStop = NULL;

bool firstTime = true;

VOID CALLBACK F_SetSettings(PVOID lpParam, BOOLEAN TimerOrWaitFired)
{
	logger.Warning(__FUNCTION__, Logger::PRE_EVENT_RCV, L"Reload Task Settings");

	//read the task settings from the Shared Memory
	SMTaskSettings.CreateShared();
	
	SMTaskSettings.getSyncroSSettings(syncroSSettings);
	//agrassi_FI19-03-08
	TaskSettings::Settings::GeneralSettings generalSetting;
	SMTaskSettings.getGeneralSettings(generalSetting);
	logger.SetAllSettings(
		syncroSSettings.getLogLevel(),
		syncroSSettings.getDiskLogEnable(),
		syncroSSettings.getMaxLogFileSize(),
		syncroSSettings.getSocketLogEnable(),
		syncroSSettings.getSocketLogIP(),
		generalSetting.getLogDiskUTF8());

	F_SetValidationLogger(
		dummy,
		syncroSSettings.getLogLevel(),
		syncroSSettings.getDiskLogEnable(),
		syncroSSettings.getMaxLogFileSize(),
		syncroSSettings.getSocketLogEnable(),
		syncroSSettings.getSocketLogIP(),
		generalSetting.getLogDiskUTF8());

	if (! firstTime)
	{
		communicationManager.UpdateAllDB();
    //agrassi_FI03-11-2010_3
    o_socket.Send(L"V",L"UpdateVersion()");
	}
	firstTime = false;
}


VOID CALLBACK F_StopFn(PVOID lpParam, BOOLEAN TimerOrWaitFired)
{
	logger.Severe(__FUNCTION__, L"Start Stop ALL Syncro");
	
	ShutDownInProgress = true;

	TaskSettings::Settings::GeneralSettings theGeneralSettings;
	SMTaskSettings.getGeneralSettings(theGeneralSettings);

	if(theGeneralSettings.getCloseAllPC())
	{
		logger.Severe(__FUNCTION__, L"Close all PC");
		communicationManager.Choose(L"<MSG><HDR>C</HDR><TXT>CommunicationManager.ShutDown(true)</TXT></MSG>");
	}
	else
		communicationManager.Choose(L"<MSG><HDR>C</HDR><TXT>CommunicationManager.ShutDown(false)</TXT></MSG>");

	UnregisterWait(registerStop);
	finished = true;

	logger.Severe(__FUNCTION__, L"Finish Stop ALL Syncro");
}

int _tmain(int argc, _TCHAR* argv[])
{
	int serverPort=0;
/*
	String::StrType scfg(L"[TSENSOR_16]\x241Epresent=true\x241EPdirect=true\x241E");
	String::StrType scfgCpy = scfg;
	String::StrType s = scfgCpy.extractFirstToken(L"_");
	int x = scfgCpy.ToInt(); 
	scfgCpy.RemoveSubString(L"[TSENSOR_");				
*/


	//Read the setting from line commands
	//ex. SyncroS 1 2001 -> Server 1 is waiting the client on the port 2001
	::ZeroMemory(dummy,sizeof(dummy));
	wcscpy(dummy,L"SyncroS");
	if(argc==3)
	{
		serverPort=_wtoi(argv[2]);
		//Start the logger for SyncroS
		swprintf(dummy,L"SyncroS %s",argv[1]);
		logger.SetWho(dummy);
	}
	else
	{
		logger.SetWho(dummy);
		logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"ERROR: Invalid Parameter Usage(SyncroS ClientNumber PortNumber)");
		return -1;
	}

	//The settings Management
	F_SetSettings(NULL,TRUE);
	if(RegisterWaitForSingleObjectEx(SMTaskSettings.getSettingChangeEvent(), F_SetSettings, NULL, INFINITE, WT_EXECUTEINWAITTHREAD)==NULL)
	{
		logger.Severe(__FUNCTION__, L"ERROR: Create Register Wait For Single Object failed on Setting Change Event (%d)", GetLastError());
		return 0;
	}

	// The Syncro Stop Management
	registerStop = RegisterWaitForSingleObjectEx(SMTaskSettings.getStopClientEvent(), F_StopFn, NULL, INFINITE, WT_EXECUTEINWAITTHREAD);
	if(registerStop==NULL)
	{
		logger.Severe(__FUNCTION__, L"ERROR: Create Register Wait For Single Object failed on Stop All Event (%d)", GetLastError());
		return 0;
	}

	logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"***************************************************");
	logger.Severe(__FUNCTION__, Logger::PRE_SOCK_INFO, L"OK: New SyncroS %s is created",argv[1]);

	//create the socket
	DWORD errorCreate=o_socket.CreateSocket(serverPort);
	if (errorCreate!=ERROR_SUCCESS)
	{
		logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Cannot create Socket on port %d",serverPort);
		return -1;
	}
	else
		logger.Info(__FUNCTION__, Logger::PRE_SOCK_INFO, L"Create Socket no error %d",errorCreate);

	//wait for a client in the socket
	DWORD errorWait=o_socket.WaitClient();
	if (errorWait!=ERROR_SUCCESS)
	{
		logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Cannot wait a client on port %d",serverPort);
		return -1;
	}
	else
		logger.Info(__FUNCTION__, Logger::PRE_SOCK_INFO, L"Wait on Socket no error %d",errorWait);
	
	//initialization
	if(communicationManager.initialization()!=ERROR_SUCCESS)
		return -1;

	//Variable where is stored the client request	
	wchar_t command[Network::SOCK_MAX_MSG];
	C_CommunicationManager::CommandResult result;
	//lifetime while
	do
	{
		//Read on socket the message from client
		if(o_socket.Receive(command)==ERROR_SUCCESS) 
		{
			if(ShutDownInProgress) 
				continue;

			

			//execute the command
			result=communicationManager.Choose(command);
			//check the result
			switch( result )
			{
			case C_CommunicationManager::InvalidCommand:
				logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Invalid Command: %s",command);
				break;
			case C_CommunicationManager::ValidCommand:
				break;
			case C_CommunicationManager::ShutDown:
				finished=true;
				break;
			default:
				logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Invalid Command: %s",command);
				break;
			}
		}
		else //Communication fault
		{
			if(ShutDownInProgress) 
				continue;

			logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Communication fault");

			//Stop all page management
			DWORD errorStop=communicationManager.Stop();
			if(errorStop!=ERROR_SUCCESS)
			{
				logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Cannot Stop Thread for page management %d",errorStop);
				return errorStop;
			}

			//try to restore the communications
			DWORD errorRestore=o_socket.Restore();
			if(errorRestore!=ERROR_SUCCESS)
			{
				logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Cannot Restore Socket %d",errorRestore);
				return errorRestore;
			}

			//Restart all page management
			DWORD errorRestart=communicationManager.Restart();
			if(errorRestart!=ERROR_SUCCESS)
			{
				logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"ERROR: Cannot Restart Thread for page management %d",errorRestart);
				return errorRestart;
			}
		}//end Communication fault
	
	} while(!finished);//end lifetime while

	o_socket.Close();

	logger.Severe(__FUNCTION__, Logger::PRE_SOCK_INFO, L"OK: The SyncroS %s is stopped",argv[1]);
	logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"***************************************************");

	return ERROR_SUCCESS;
}

