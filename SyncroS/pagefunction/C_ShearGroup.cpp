#include ".\C_ShearGroup.h"

//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

bool C_ShearGroup::end_Status;
bool C_ShearGroup::end_Console;
bool C_ShearGroup::end_AlarmsActive;
bool C_ShearGroup::end_AlarmsHistory;
bool C_ShearGroup::end_SettingsMachine;
bool C_ShearGroup::end_SettingsTask;

C_ShearGroup::C_ShearGroup(void)
{
	pid_ShearPage=NULL;
	TerminateThread(false);
}

C_ShearGroup::~C_ShearGroup(void)
{
}

bool C_ShearGroup::ChooseShearPage(String::StrType thePageRequest)
{
	//status
	if(thePageRequest.CompareNoCase(L"Status"))
	{
		end_Status=false;
		pid_ShearPage=::CreateThread(NULL,0,C_ShearGroup::Status,NULL,0,NULL);
		return true;
	}
	//console
	if(thePageRequest.CompareNoCase(L"Console"))
	{
		end_Console=false;
		pid_ShearPage=::CreateThread(NULL,0,C_ShearGroup::Console,NULL,0,NULL);
		return true;
	}
	//alarms
	if(thePageRequest.CompareNoCase(L"AlarmsActive"))
	{
		end_AlarmsActive=false;
		pid_ShearPage=::CreateThread(NULL,0,C_ShearGroup::AlarmsActive,NULL,0,NULL);
		return true;
	}
		
	if(thePageRequest.CompareNoCase(L"AlarmsHistory"))
	{
		end_AlarmsHistory=false;
		pid_ShearPage=::CreateThread(NULL,0,C_ShearGroup::AlarmsHistory,NULL,0,NULL);
		return true;
	}
	//settings
	if(thePageRequest.CompareNoCase(L"SettingsMachine"))
	{
		end_SettingsMachine=false;
		pid_ShearPage=::CreateThread(NULL,0,C_ShearGroup::SettingsMachine,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"SettingsTask"))
	{
		end_SettingsTask=false;
		pid_ShearPage=::CreateThread(NULL,0,C_ShearGroup::SettingsTask,NULL,0,NULL);
		return true;
	}
	
	return false;
}

void C_ShearGroup::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_ShearPage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_ShearPage,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread ShearPage ... %d", ::GetLastError());
		}
	}
	else
	{
		end_Status = true;
		end_Console = true;
		end_AlarmsActive = true;
		end_AlarmsHistory = true;
		end_SettingsMachine = true;
		end_SettingsTask = true;
	}
}

DWORD C_ShearGroup::SuspendThread(void)
{
	if(pid_ShearPage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_ShearPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page Shear Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_ShearGroup::ResumeThread(void)
{
	if(pid_ShearPage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_ShearPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page Shear Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}

DWORD WINAPI C_ShearGroup::Status(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"shear_status._Show()");

	while(!end_Status)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated shear_status page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ShearGroup::Console(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"shear_console._Show()");

	while(!end_Console)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated shear_console page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ShearGroup::AlarmsActive(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"shear_alarmsActive._Show()");

	while(!end_AlarmsActive)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated shear_alarmsActive page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ShearGroup::AlarmsHistory(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"shear_alarmsHistory._Show()");

	while(!end_AlarmsHistory)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated shear_alarmsHistory page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ShearGroup::SettingsMachine(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"shear_settingsMachine._Show()");

	while(!end_SettingsMachine)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated shear_settingsMachine page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_ShearGroup::SettingsTask(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"shear_settingsTask._Show()");

	while(!end_SettingsTask)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated shear_settingsTask page threads");

	return ERROR_SUCCESS;
}
