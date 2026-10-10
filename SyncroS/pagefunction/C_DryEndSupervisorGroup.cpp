#include ".\C_DryEndSupervisorGroup.h"

#include "..\C_PageManager.h"

//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//Order Shared Memory include
#include "..\DBMS\C_SMOrder.h"
	
DataBase::C_SMOrder SMOrder;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

bool C_DryEndSupervisorGroup::end_Page01;
bool C_DryEndSupervisorGroup::end_Page02;
bool C_DryEndSupervisorGroup::end_Page03;
bool C_DryEndSupervisorGroup::end_Settings;

C_DryEndSupervisorGroup::C_DryEndSupervisorGroup(void)
{
	pid_DryEndSupervisorPage=NULL;
	TerminateThread(false);
}

C_DryEndSupervisorGroup::~C_DryEndSupervisorGroup(void)
{
}

bool C_DryEndSupervisorGroup::ChooseDryEndSupervisorGroupPage(String::StrType thePageRequest)
{
	//page01
	if(thePageRequest.CompareNoCase(L"01"))
	{
		end_Page01 = false;
		pid_DryEndSupervisorPage=::CreateThread(NULL,0,C_DryEndSupervisorGroup::Page01,NULL,0,NULL);
		return true;
	}
	//page02
	if(thePageRequest.CompareNoCase(L"02"))
	{
		end_Page02 = false;
		pid_DryEndSupervisorPage=::CreateThread(NULL,0,C_DryEndSupervisorGroup::Page02,NULL,0,NULL);
		return true;
	}
	//page03
	if(thePageRequest.CompareNoCase(L"03"))
	{
		end_Page03 = false;
		pid_DryEndSupervisorPage=::CreateThread(NULL,0,C_DryEndSupervisorGroup::Page03,NULL,0,NULL);
		return true;
	}
	//Settings
	if(thePageRequest.CompareNoCase(L"Settings"))
	{
		end_Settings = false;
		pid_DryEndSupervisorPage=::CreateThread(NULL,0,C_DryEndSupervisorGroup::Settings,NULL,0,NULL);
		return true;
	}
	
	return false;
}

void C_DryEndSupervisorGroup::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_DryEndSupervisorPage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_DryEndSupervisorPage,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread DryEndSupervisorPage ... %d", ::GetLastError());
		}
	}
	else
	{
		end_Page01 = true;
		end_Page02 = true;
		end_Page03 = true;
		end_Settings = true;
	}
}

DWORD C_DryEndSupervisorGroup::SuspendThread(void)
{
	if(pid_DryEndSupervisorPage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_DryEndSupervisorPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page DryEnd Supervisor Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_DryEndSupervisorGroup::ResumeThread(void)
{
	if(pid_DryEndSupervisorPage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_DryEndSupervisorPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page DryEnd Supervisor Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}

DWORD WINAPI C_DryEndSupervisorGroup::Page01(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"dryEndSupervisor_01._Show()");

	while(!end_Page01)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated dryEndSupervisor_01 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DryEndSupervisorGroup::Page02(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"dryEndSupervisor_02._Show()");
	o_socket.Send(Network::MSG_PAG_REF,L"dryEndSupervisor_02._setFrmMachineName(s[%s])", C_PageManager::SMState.getExternWECStateMODIFY().getMachineName());

	State::ExternWECState externWECState;

	while(!end_Page02)
	{
		if (externWECState != C_PageManager::SMState.getExternWECState())
		{
			C_PageManager::SMState.getExternWECState(externWECState);

			//comms alarm
			o_socket.Send(Network::MSG_PAG_REF,L"dryEndSupervisor_02.SendCurrentProgram.Alarmed=%s", String::Bool2Alpha(externWECState.alarm.getCommsAlarm().getValue()));
			o_socket.Send(Network::MSG_PAG_REF,L"dryEndSupervisor_02.SendNextProgram.Alarmed=%s", String::Bool2Alpha(externWECState.alarm.getCommsAlarm().getValue()));
			o_socket.Send(Network::MSG_PAG_REF,L"dryEndSupervisor_02.SendNextPlusOneProgram.Alarmed=%s", String::Bool2Alpha(externWECState.alarm.getCommsAlarm().getValue()));
			o_socket.Send(Network::MSG_PAG_REF,L"dryEndSupervisor_02.WetShiftRequest.Alarmed=%s", String::Bool2Alpha(externWECState.alarm.getCommsAlarm().getValue()));

			//auto / manual
			o_socket.Send(Network::MSG_PAG_REF,L"dryEndSupervisor_02.stb_AutoMan.Auto=%s", String::Bool2Alpha(!externWECState.control.getIsInManual().getValue()));
			o_socket.Send(Network::MSG_PAG_REF,L"dryEndSupervisor_02.stb_AutoMan.Visible=%s", String::Bool2Alpha(externWECState.control.getIsInManual().getEnable()));

			//programs
			for (int i = 1; i <= 3; i++)
				o_socket.Send(Network::MSG_PAG_REF,L"dryEndSupervisor_02._fillProgram(i[%d],i[%d],i[%d])", /*position*/i, /*ID from status*/externWECState.orders.getID(i).getValue(), /*ModID from status*/externWECState.orders.getModID(i).getValue());

		}		
		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated dryEndSupervisor_02 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DryEndSupervisorGroup::Page03(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"dryEndSupervisor_03._Show()");

	while(!end_Page03)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated dryEndSupervisor_03 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DryEndSupervisorGroup::Settings(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"dryEndSupervisor_settings._Show()");

	while(!end_Settings)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated dryEndSupervisor_settings page threads");

	return ERROR_SUCCESS;
}
