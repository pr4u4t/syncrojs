#include ".\C_KnifeGroup.h"

//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

//Page Management
#include "..\C_PageManager.h"

bool C_KnifeGroup::end_Status;
bool C_KnifeGroup::end_Console;
bool C_KnifeGroup::end_AlarmsActive;
bool C_KnifeGroup::end_AlarmsHistory;
bool C_KnifeGroup::end_SettingsMachine;
bool C_KnifeGroup::end_SettingsTask;
bool C_KnifeGroup::end_Pattern;

C_KnifeGroup::C_KnifeGroup(void)
{
	pid_KnifePage=NULL;
	TerminateThread(false);
}

C_KnifeGroup::~C_KnifeGroup(void)
{
}

bool C_KnifeGroup::ChooseKnifePage(String::StrType thePageRequest)
{
	//status
	if(thePageRequest.CompareNoCase(L"Status"))
	{
		end_Status=false;
		pid_KnifePage=::CreateThread(NULL,0,C_KnifeGroup::Status,NULL,0,NULL);
		return true;
	}
  //Pattern
	if(thePageRequest.CompareNoCase(L"Pattern"))
	{
		end_Pattern=false;
		pid_KnifePage=::CreateThread(NULL,0,C_KnifeGroup::Pattern,NULL,0,NULL);
		return true;
	}
	//console
	if(thePageRequest.CompareNoCase(L"Console"))
	{
		end_Console=false;
		pid_KnifePage=::CreateThread(NULL,0,C_KnifeGroup::Console,NULL,0,NULL);
		return true;
	}
	//alarms
	if(thePageRequest.CompareNoCase(L"AlarmsActive"))
	{
		end_AlarmsActive=false;
		pid_KnifePage=::CreateThread(NULL,0,C_KnifeGroup::AlarmsActive,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"AlarmsHistory"))
	{
		end_AlarmsHistory=false;
		pid_KnifePage=::CreateThread(NULL,0,C_KnifeGroup::AlarmsHistory,NULL,0,NULL);
		return true;
	}
	//Settings
	if(thePageRequest.CompareNoCase(L"SettingsMachine"))
	{
		end_SettingsMachine=false;
		pid_KnifePage=::CreateThread(NULL,0,C_KnifeGroup::SettingsMachine,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"SettingsTask"))
	{
		end_SettingsTask=false;
		pid_KnifePage=::CreateThread(NULL,0,C_KnifeGroup::SettingsTask,NULL,0,NULL);
		return true;
	}

	return false;
}

void C_KnifeGroup::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_KnifePage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_KnifePage,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread KnifePage ... %d", ::GetLastError());
		}
	}
	else
	{
		end_Status = true;
    end_Pattern = true;
		end_Console = true;
		end_AlarmsActive = true;
		end_AlarmsHistory = true;
		end_SettingsMachine = true;
		end_SettingsTask = true;
	}
}

DWORD C_KnifeGroup::SuspendThread(void)
{
	if(pid_KnifePage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_KnifePage)==-1)
	{
		DWORD error=GetLastError();
		logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page Knife Thread ... %d",error);
		return error;
	};
  
	return ERROR_SUCCESS;
}

DWORD C_KnifeGroup::ResumeThread(void)
{
	if(pid_KnifePage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_KnifePage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page Knife Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}

DWORD WINAPI C_KnifeGroup::Status(LPVOID  lpParam)
{
	State::KnifeState theKnifeState;
	State::StatisticsStateSetup  theStatisticsStateSetup;

	DWORD timedOut = -1;
	int StateID = 0;
	
	o_socket.Send(Network::MSG_PAG_REF, L"knife_status._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"knife_status.lbl_Title_YN.Text=%s",C_PageManager::SMState.getKnifeState().getMachineName());	

	//RFAZZI_FI28/06/2008_1.Added send order Push Button on machine status page
	bool communicationError = false;

	while(!end_Status)
	{
		StateID = C_PageManager::SMState.getKnifeState(theKnifeState);
		theStatisticsStateSetup = C_PageManager::SMState.getStatisticsStateCurrentSetup();

		//statistics data update (always)
		o_socket.Send(Network::MSG_PAG_REF,L"knife_status.ttb_CurrentGoodSheets0.Value=%d",theStatisticsStateSetup.getKnifeGoodSheetsProduced(KnifeLevel(1)));
    o_socket.Send(Network::MSG_PAG_REF,L"knife_status.ttb_CurrentGoodSheets1.Value=%d",theStatisticsStateSetup.getKnifeGoodSheetsProduced(KnifeLevel(2)));
    o_socket.Send(Network::MSG_PAG_REF,L"knife_status.ttb_CurrentGoodSheets2.Value=%d",theStatisticsStateSetup.getKnifeGoodSheetsProduced(KnifeLevel(3)));

		o_socket.Send(Network::MSG_PAG_REF,L"knife_status.ttb_CurrentDefaultCuts0.Value=%d",theStatisticsStateSetup.getKnifeDefaultSheetsProduced(KnifeLevel(1)));
    o_socket.Send(Network::MSG_PAG_REF,L"knife_status.ttb_CurrentDefaultCuts1.Value=%d",theStatisticsStateSetup.getKnifeDefaultSheetsProduced(KnifeLevel(2)));
    o_socket.Send(Network::MSG_PAG_REF,L"knife_status.ttb_CurrentDefaultCuts2.Value=%d",theStatisticsStateSetup.getKnifeDefaultSheetsProduced(KnifeLevel(3)));
    
    o_socket.Send(Network::MSG_PAG_REF,L"knife_status.ttb_CurrentBadCuts0.Value=%d",theStatisticsStateSetup.getKnifeBadSheetsProduced(KnifeLevel(1)));
    o_socket.Send(Network::MSG_PAG_REF,L"knife_status.ttb_CurrentBadCuts1.Value=%d",theStatisticsStateSetup.getKnifeBadSheetsProduced(KnifeLevel(2)));
    o_socket.Send(Network::MSG_PAG_REF,L"knife_status.ttb_CurrentBadCuts2.Value=%d",theStatisticsStateSetup.getKnifeBadSheetsProduced(KnifeLevel(3)));

		//setups data update (only when changed)
		if (timedOut != WAIT_TIMEOUT)
		{
			//manual auto
			if(theKnifeState.control.getIsInManual().getEnable())
				o_socket.Send(Network::MSG_PAG_REF,L"knife_status.stb_AutoMan.Auto=%s",(!theKnifeState.control.getIsInManual().getValue()) ? L"True" : L"False");
						
			//current and next setup ID, modID
			if(theKnifeState.orders.getIDCurrent().getEnable() && theKnifeState.orders.getModIDCurrent().getEnable())
				o_socket.Send(Network::MSG_PAG_REF,L"knife_status._setCurrentSetup(i[%d],i[%d])",theKnifeState.orders.getIDCurrent().getValue(),theKnifeState.orders.getModIDCurrent().getValue());
			if(theKnifeState.orders.getIDNext().getEnable() && theKnifeState.orders.getModIDNext().getEnable())
				o_socket.Send(Network::MSG_PAG_REF,L"knife_status._setNextSetup(i[%d],i[%d])",theKnifeState.orders.getIDNext().getValue(),theKnifeState.orders.getModIDNext().getValue());				
		
			//current and next cut length
			if(theKnifeState.orders.getActive_UDdmm(KnifeLevel(1)).getEnable())
				o_socket.Send(Network::MSG_PAG_REF,L"knife_status.nud_CurrentCutLenght0.Value=%d",(theKnifeState.orders.getActive_UDdmm(KnifeLevel(1)).getValue()));
			if(theKnifeState.orders.getActive_UDdmm(KnifeLevel(2)).getEnable())
				o_socket.Send(Network::MSG_PAG_REF,L"knife_status.nud_CurrentCutLenght1.Value=%d",(theKnifeState.orders.getActive_UDdmm(KnifeLevel(2)).getValue()));
			if(theKnifeState.orders.getActive_UDdmm(KnifeLevel(3)).getEnable())
				o_socket.Send(Network::MSG_PAG_REF,L"knife_status.nud_CurrentCutLenght2.Value=%d",(theKnifeState.orders.getActive_UDdmm(KnifeLevel(3)).getValue()));	
			if(theKnifeState.orders.getNext_UDdmm(KnifeLevel(1)).getEnable())
				o_socket.Send(Network::MSG_PAG_REF,L"knife_status.nud_NextCutLenght0.Value=%d",(theKnifeState.orders.getNext_UDdmm(KnifeLevel(1)).getValue()));	
			if(theKnifeState.orders.getNext_UDdmm(KnifeLevel(2)).getEnable())
				o_socket.Send(Network::MSG_PAG_REF,L"knife_status.nud_NextCutLenght1.Value=%d",(theKnifeState.orders.getNext_UDdmm(KnifeLevel(2)).getValue()));	
			if(theKnifeState.orders.getNext_UDdmm(KnifeLevel(3)).getEnable())
				o_socket.Send(Network::MSG_PAG_REF,L"knife_status.nud_NextCutLenght2.Value=%d",(theKnifeState.orders.getNext_UDdmm(KnifeLevel(3)).getValue()));	
		}

		//RFAZZI_FI28/06/2008_1.Added send order Push Button on machine status page
		if(communicationError!=C_PageManager::SMState.getKnifeState().alarms.getIsAlarmMCPCommError())
		{
			communicationError = C_PageManager::SMState.getKnifeState().alarms.getIsAlarmMCPCommError();
			o_socket.Send(Network::MSG_PAG_REF,L"knife_status._CommunicationError(b[%s])",
        String::Bool2Alpha(communicationError));
		}

		timedOut = C_PageManager::SMState.WaitKnifeChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated knife_status page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_KnifeGroup::Pattern(LPVOID  lpParam)
{
  o_socket.Send(Network::MSG_PAG_REF, L"knife_pattern._Show()");
	o_socket.Send(Network::MSG_PAG_REF,L"knife_pattern.lbl_Title_YN.Text=%s",C_PageManager::SMState.getKnifeState().getMachineName());

	while(!end_Pattern)
	{
		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated knife_pattern page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_KnifeGroup::Console(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"knife_console._Show()");
	o_socket.Send(Network::MSG_PAG_REF,L"knife_console.lbl_Title_YN.Text=%s",C_PageManager::SMState.getKnifeState().getMachineName());

	bool communicationError = false;
	while(!end_Console)
	{
		if(communicationError!=C_PageManager::SMState.getKnifeState().alarms.getIsAlarmMCPCommError())
		{
			communicationError = C_PageManager::SMState.getKnifeState().alarms.getIsAlarmMCPCommError();
			o_socket.Send(Network::MSG_PAG_REF,L"knife_console._CommunicationError(b[%s])",
        String::Bool2Alpha(communicationError));
		}

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated knife_console page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_KnifeGroup::AlarmsActive(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"knife_alarmsActive._Show()");
	o_socket.Send(Network::MSG_PAG_REF,L"knife_alarmsActive.lbl_Title_YN.Text=%s",C_PageManager::SMState.getKnifeState().getMachineName());

	while(!end_AlarmsActive)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated dryEndSupervisor_01 page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_KnifeGroup::AlarmsHistory(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"knife_alarmsHistory._Show()");
	o_socket.Send(Network::MSG_PAG_REF,L"knife_alarmsHistory.lbl_Title_YN.Text=%s",C_PageManager::SMState.getKnifeState().getMachineName());

	while(!end_AlarmsHistory)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated knife_alarmsActive page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_KnifeGroup::SettingsMachine(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"knife_settingsMachine._Show()");
	o_socket.Send(Network::MSG_PAG_REF,L"knife_settingsMachine.lbl_Title_YN.Text=%s",C_PageManager::SMState.getKnifeState().getMachineName());

	while(!end_SettingsMachine)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated knife_settingsMachine page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_KnifeGroup::SettingsTask(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"knife_settingsTask._Show()");
	o_socket.Send(Network::MSG_PAG_REF,L"knife_settingsTask.lbl_Title_YN.Text=%s",C_PageManager::SMState.getKnifeState().getMachineName());

	while(!end_SettingsTask)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated knife_settingsTask page threads");

	return ERROR_SUCCESS;
}

