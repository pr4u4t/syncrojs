#include ".\C_PreHeaterTernGroup.h"

//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

//Page Management
#include "..\C_PageManager.h"

bool C_PreHeaterTernGroup::end_Status;
bool C_PreHeaterTernGroup::end_Console;
bool C_PreHeaterTernGroup::end_AlarmsActive;
bool C_PreHeaterTernGroup::end_AlarmsHistory;
bool C_PreHeaterTernGroup::end_SettingsMachine;
bool C_PreHeaterTernGroup::end_SettingsTask;
bool C_PreHeaterTernGroup::end_ProcessControlStatus;
bool C_PreHeaterTernGroup::end_ProcessControlSettings;

C_PreHeaterTernGroup::C_PreHeaterTernGroup(void)
{
	pid_PreHeaterTernPage=NULL;
	TerminateThread(false);
}

C_PreHeaterTernGroup::~C_PreHeaterTernGroup(void)
{
}

bool C_PreHeaterTernGroup::ChoosePreHeaterTernPage(String::StrType  thePageRequest)
{
	//status
	if(thePageRequest.CompareNoCase(L"Status"))
	{
		end_Status = false;
		pid_PreHeaterTernPage=::CreateThread(NULL,0,Status,NULL,0,NULL);
		return true;
	}
	//Console
	if(thePageRequest.CompareNoCase(L"Console"))
	{
		end_Console = false;
		pid_PreHeaterTernPage=::CreateThread(NULL,0,Console,NULL,0,NULL);
		return true;
	}

	//alarms
	if(thePageRequest.CompareNoCase(L"AlarmsActive"))
	{
		end_AlarmsActive=false;
		pid_PreHeaterTernPage=::CreateThread(NULL,0,C_PreHeaterTernGroup::AlarmsActive,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"AlarmsHistory"))
	{
		end_AlarmsHistory=false;
		pid_PreHeaterTernPage=::CreateThread(NULL,0,C_PreHeaterTernGroup::AlarmsHistory,NULL,0,NULL);
		return true;
	}
	//Settings
	if(thePageRequest.CompareNoCase(L"SettingsMachine"))
	{
		end_SettingsMachine=false;
		pid_PreHeaterTernPage=::CreateThread(NULL,0,C_PreHeaterTernGroup::SettingsMachine,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"SettingsTask"))
	{
		end_SettingsTask=false;
		pid_PreHeaterTernPage=::CreateThread(NULL,0,C_PreHeaterTernGroup::SettingsTask,NULL,0,NULL);
		return true;
	}
	//Process Control status
	if(thePageRequest.CompareNoCase(L"ProcessControlStatus"))
	{
		end_ProcessControlStatus = false;
		pid_PreHeaterTernPage=::CreateThread(NULL,0,ProcessControlStatus,NULL,0,NULL);
		return true;
	}
	//Process Control settings
	if(thePageRequest.CompareNoCase(L"ProcessControlSettings"))
	{
		end_ProcessControlSettings = false;
		pid_PreHeaterTernPage=::CreateThread(NULL,0,ProcessControlSettings,NULL,0,NULL);
		return true;
	}
	return false;
}

void C_PreHeaterTernGroup::TerminateThread(const bool terminateThread)
{	
	if(terminateThread)
	{
		if(pid_PreHeaterTernPage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_PreHeaterTernPage,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread PreHeaterTernPage ... %d", ::GetLastError());
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
		end_ProcessControlStatus = true;
		end_ProcessControlSettings = true;
	}
}

DWORD C_PreHeaterTernGroup::SuspendThread(void)
{
	if(pid_PreHeaterTernPage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_PreHeaterTernPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page PreHeaterTern Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_PreHeaterTernGroup::ResumeThread(void)
{
	if(pid_PreHeaterTernPage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_PreHeaterTernPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page PreHeaterTern Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}


DWORD WINAPI C_PreHeaterTernGroup::Status(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"PreHeaterTern_Status._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"PreHeaterTern_Status.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().preHeatersTernState.getMachineName());	
	
	State::PreHeatersTernState thePreHeatersTernState;
	State::SingleFacerState theSingleFacerState;

	int StateID = 0;

	while(!end_Status)
	{
		StateID = C_PageManager::SMState.getPreHeatersTernState(thePreHeatersTernState);

		for(int i=0; i<8 ; i++)
		{
			WrapArmNumber theWrapArmNumber;
			switch (i)
			{
			//WRAP ARM 11
			case 0:
				theWrapArmNumber = S1_1;
				break;
			//WRAP ARM 1
			case 1:
				theWrapArmNumber = B1_PH;
				break;
			//WRAP ARM 2
			case 2:
				theWrapArmNumber = B2_PH;
				break;
			//WRAP ARM 3
			case 3:
				theWrapArmNumber = B3_PH;
				break;
			//WRAP ARM 61
			case 4:
				theWrapArmNumber = S1_6;
				break;
			//WRAP ARM 10
			case 5:
				theWrapArmNumber = B1_PHO;
				break;
			//WRAP ARM 20
			case 6:
				theWrapArmNumber = B2_PHO;
				break;
			//WRAP ARM 30
			case 7:
				theWrapArmNumber = B3_PHO;
				break;
			
			//WRAP ARM 1
			default:
				theWrapArmNumber = S1_1;
				break;
			}


			State::WrapArmState theWrapArmState = thePreHeatersTernState.CtrlData.getWrapArmByNumber(theWrapArmNumber);

			if(theWrapArmState.isUsed())
			{
				o_socket.Send(Network::MSG_PAG_REF,L"PreHeaterTern_Status.wrapArmPreHeater%d.Visible=true",theWrapArmNumber);
				o_socket.Send(Network::MSG_PAG_REF,L"PreHeaterTern_Status.wa_wrapArmPreHeater%d.Visible=true",theWrapArmNumber);
				o_socket.Send(Network::MSG_PAG_REF,L"PreHeaterTern_Status.wa_wrapArmPreHeater%d.InLocal=%s",theWrapArmNumber,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmLocal()));
				o_socket.Send(Network::MSG_PAG_REF,L"PreHeaterTern_Status.wa_wrapArmPreHeater%d.Auto=%s",theWrapArmNumber,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmAuto()));
				o_socket.Send(Network::MSG_PAG_REF,L"PreHeaterTern_Status.wa_wrapArmPreHeater%d.IsUnder=%s",theWrapArmNumber,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmUnder()));
				o_socket.Send(Network::MSG_PAG_REF,L"PreHeaterTern_Status.wa_wrapArmPreHeater%d.Position=%d",theWrapArmNumber,
					theWrapArmState.getPosition_UPercINT().getValue());
				o_socket.Send(Network::MSG_PAG_REF,L"PreHeaterTern_Status.wa_wrapArmPreHeater%d.IsMaxPos=%s",theWrapArmNumber,
					String::Bool2Alpha(theWrapArmState.getIsMaxPos()));
				o_socket.Send(Network::MSG_PAG_REF,L"PreHeaterTern_Status.wa_wrapArmPreHeater%d.IsMinPos=%s",theWrapArmNumber,
					String::Bool2Alpha(theWrapArmState.getIsMinPos()));

        if(C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(theWrapArmNumber).getEnable())
          o_socket.Send(Network::MSG_PAG_REF, L"PreHeaterTern_Status.wa_wrapArmPreHeater%d.Temperature=%d", theWrapArmNumber, C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(theWrapArmNumber).getValue());

			} //IF
		} // FOR
		
		//// SINGLE FACER [1,2]
		for(int index=1; index<=SYNCRO_MAX_SFS ; index++)
		{
			int paperTensionNumber = index + B1_PT - 1;
			theSingleFacerState.Clear();
			C_PageManager::SMState.getSingleFacerState(theSingleFacerState,index);

			if ( C_PageManager::SMState.getBridgeState(index).Monitor.paperTensionState.getPaperTension_UFkgxm_s2().getEnable() )
			{
         o_socket.Send(Network::MSG_PAG_REF, L"PreHeaterTern_Status.pt_paperTension%d.Position=%d", paperTensionNumber, 
                (int)C_PageManager::SMState.getBridgeState(index).Monitor.paperTensionState.getPaperTension_UFkgxm_s2().getValue());
				o_socket.Send(Network::MSG_PAG_REF,L"PreHeaterTern_Status.pt_paperTension%d.Visible=true",paperTensionNumber);
				o_socket.Send(Network::MSG_PAG_REF, L"PreHeaterTern_Status.pt_paperTension%d.InLocal=%s",paperTensionNumber,
					String::Bool2Alpha(!C_PageManager::SMState.getBridgeState(index).Monitor.paperTensionState.getIsPaperTensionRemote().getValue()));
				o_socket.Send(Network::MSG_PAG_REF, L"PreHeaterTern_Status.pt_paperTension%d.Auto=%s",paperTensionNumber,
					String::Bool2Alpha(C_PageManager::SMState.getBridgeState(index).Monitor.paperTensionState.getIsPaperTensionAuto().getValue()));
			}
		}

    //RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea         
		for(int i=0; i<6 ; i++)
		{
			WrapArmSteamNumber theWrapArmSteamNumber;
			switch (i)
			{
			//WRAP ARM 11
			case 0:
				theWrapArmSteamNumber = S1_ST_1;
				break;
			//WRAP ARM 1
			case 1:
				theWrapArmSteamNumber = B1_ST_PH;
				break;
			//WRAP ARM 2
			case 2:
				theWrapArmSteamNumber = B2_ST_PH;
				break;
			//WRAP ARM 3
			case 3:
				theWrapArmSteamNumber = B3_ST_PH;
				break;
			//WRAP ARM 10
			case 4:
				theWrapArmSteamNumber = B1_ST_PHO;
				break;
			//WRAP ARM 20
			case 5:
				theWrapArmSteamNumber = B2_ST_PHO;
				break;
			
			//WRAP ARM 1
			default:
				theWrapArmSteamNumber = S1_ST_1;
				break;
			}


			State::WrapArmSteamState theWrapArmSteamState = thePreHeatersTernState.Monitor.getWrapArmSteamByNumber(theWrapArmSteamNumber);

			if(theWrapArmSteamState.isUsed())
			{
				o_socket.Send(Network::MSG_PAG_REF,L"PreHeaterTern_Status.st_wrapArmSteam%d.Visible=true",theWrapArmSteamNumber);
				o_socket.Send(Network::MSG_PAG_REF,L"PreHeaterTern_Status.st_wrapArmSteam%d.InLocal=%s",theWrapArmSteamNumber,
					String::Bool2Alpha(!theWrapArmSteamState.getIsWrapArmSteamRemote().getValue()));
				o_socket.Send(Network::MSG_PAG_REF,L"PreHeaterTern_Status.st_wrapArmSteam%d.Auto=%s",theWrapArmSteamNumber,
					String::Bool2Alpha(theWrapArmSteamState.getIsWrapArmSteamAuto().getValue()));
				o_socket.Send(Network::MSG_PAG_REF,L"PreHeaterTern_Status.st_wrapArmSteam%d.Position=%d",theWrapArmSteamNumber,
					(int)theWrapArmSteamState.getWrapArmSteam_UPdb().getValue());
			} //IF
		} // FOR


    C_PageManager::SMState.WaitPreHeatersTernChangeStatus(1000, StateID);
	}//WHILE

	logger.Fine(__FUNCTION__, L"Terminated PreHeaterTern_Status page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_PreHeaterTernGroup::Console(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"PreHeaterTern_Console._Show()");

	while(!end_Console)
	  Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated PreHeaterTern_Console page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_PreHeaterTernGroup::AlarmsActive(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"PreHeaterTern_alarmsActive._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"PreHeaterTern_alarmsActive.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().preHeatersTernState.getMachineName());	

	while(!end_AlarmsActive)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated PreHeaterTern_alarmsActive page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_PreHeaterTernGroup::AlarmsHistory(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"PreHeaterTern_alarmsHistory._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"PreHeaterTern_alarmsHistory.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().preHeatersTernState.getMachineName());	

	while(!end_AlarmsHistory)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated PreHeaterTern_alarmsActive page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_PreHeaterTernGroup::SettingsMachine(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"PreHeaterTern_settingsMachine._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"PreHeaterTern_settingsMachine.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().preHeatersTernState.getMachineName());	

	while(!end_SettingsMachine)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated PreHeaterTern_settingsMachine page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_PreHeaterTernGroup::SettingsTask(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"PreHeaterTern_settingsTask._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"PreHeaterTern_settingsTask.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().preHeatersTernState.getMachineName());	

	while(!end_SettingsTask)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated PreHeaterTern_settingsTask page threads");

	return ERROR_SUCCESS;
}


DWORD WINAPI C_PreHeaterTernGroup::ProcessControlStatus(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"PreHeaterTern_ProcessControlStatus._Show()");

	while(!end_ProcessControlStatus)
	{
		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated PreHeaterTern_ProcessControlStatus page threads");
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_PreHeaterTernGroup::ProcessControlSettings(LPVOID  lpParam)
{
	wchar_t* formName=L"PreHeaterTern_ProcessControlSettings";

	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);

	State::PreHeatersTernState thePreHeatersTernState;
	
	while(!end_ProcessControlSettings)
	{
		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated %s page threads", formName);

	return ERROR_SUCCESS;
}