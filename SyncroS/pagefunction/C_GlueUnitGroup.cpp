#include ".\C_GlueUnitGroup.h"

//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

//Page Management
#include "..\C_PageManager.h"
//agrassi_FI27-06-2011_1
#include "..\..\Shared\C_SMTaskSettings.h"
extern TaskSettings::Settings::GeneralSettings generalSettings;
extern TaskSettings::C_SMTaskSettings SMTaskSettings;

bool C_GlueUnitGroup::end_Status;
bool C_GlueUnitGroup::end_ConsoleWrapArms;
bool C_GlueUnitGroup::end_ConsoleGlueGaps;
bool C_GlueUnitGroup::end_AlarmsActive;
bool C_GlueUnitGroup::end_AlarmsHistory;
bool C_GlueUnitGroup::end_SettingsMachine;
bool C_GlueUnitGroup::end_SettingsTask;
bool C_GlueUnitGroup::end_ProcessControlStatus;
bool C_GlueUnitGroup::end_ProcessControlSettings;


C_GlueUnitGroup::C_GlueUnitGroup(void)
{
	pid_GlueUnitPage=NULL;
	TerminateThread(false);
}

C_GlueUnitGroup::~C_GlueUnitGroup(void)
{
}

bool C_GlueUnitGroup::ChooseGlueUnitPage(String::StrType  thePageRequest)
{
	//status
	if(thePageRequest.CompareNoCase(L"Status"))
	{
		end_Status = false;
		pid_GlueUnitPage=::CreateThread(NULL,0,Status,NULL,0,NULL);
		return true;
	}

	//Console Wrap arms
	if(thePageRequest.CompareNoCase(L"ConsoleWrapArms"))
	{
		end_ConsoleWrapArms = false;
		pid_GlueUnitPage=::CreateThread(NULL,0,ConsoleWrapArms,NULL,0,NULL);
		return true;
	}
	//Console Glue gaps
	if(thePageRequest.CompareNoCase(L"ConsoleGlueGaps"))
	{
		end_ConsoleGlueGaps = false;
		pid_GlueUnitPage=::CreateThread(NULL,0,ConsoleGlueGaps,NULL,0,NULL);
		return true;
	}

	//alarms
	if(thePageRequest.CompareNoCase(L"AlarmsActive"))
	{
		end_AlarmsActive=false;
		pid_GlueUnitPage=::CreateThread(NULL,0,C_GlueUnitGroup::AlarmsActive,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"AlarmsHistory"))
	{
		end_AlarmsHistory=false;
		pid_GlueUnitPage=::CreateThread(NULL,0,C_GlueUnitGroup::AlarmsHistory,NULL,0,NULL);
		return true;
	}
	//Settings
	if(thePageRequest.CompareNoCase(L"SettingsMachine"))
	{
		end_SettingsMachine=false;
		pid_GlueUnitPage=::CreateThread(NULL,0,C_GlueUnitGroup::SettingsMachine,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"SettingsTask"))
	{
		end_SettingsTask=false;
		pid_GlueUnitPage=::CreateThread(NULL,0,C_GlueUnitGroup::SettingsTask,NULL,0,NULL);
		return true;
	}
	//Process Control status
	if(thePageRequest.CompareNoCase(L"ProcessControlStatus"))
	{
		end_ProcessControlStatus = false;
		pid_GlueUnitPage=::CreateThread(NULL,0,ProcessControlStatus,NULL,0,NULL);
		return true;
	}
	//Process Control settings
	if(thePageRequest.CompareNoCase(L"ProcessControlSettings"))
	{
		end_ProcessControlSettings = false;
		pid_GlueUnitPage=::CreateThread(NULL,0,ProcessControlSettings,NULL,0,NULL);
		return true;
	}

	return false;
}

void C_GlueUnitGroup::TerminateThread(const bool terminateThread)
{	
	if(terminateThread)
	{
		if(pid_GlueUnitPage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_GlueUnitPage,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread GlueUnitPage ... %d", ::GetLastError());
		}
	}
	else
	{
		end_Status = true;
		end_ConsoleWrapArms = true;
		end_ConsoleGlueGaps = true;
		end_AlarmsActive = true;
		end_AlarmsHistory = true;
		end_SettingsMachine = true;
		end_SettingsTask = true;
	}
}

DWORD C_GlueUnitGroup::SuspendThread(void)
{
	if(pid_GlueUnitPage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_GlueUnitPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page GlueUnit Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_GlueUnitGroup::ResumeThread(void)
{
	if(pid_GlueUnitPage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_GlueUnitPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page GlueUnit Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}


DWORD WINAPI C_GlueUnitGroup::Status(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status._Show()");
	
	State::GlueUnitState theGlueUnitState;
	theGlueUnitState.Clear();
  //agrassi_FI27-06-2011_1
  SMTaskSettings.getGeneralSettings(generalSettings);

	while(!end_Status)
	{
		theGlueUnitState.Clear();

		C_PageManager::SMState.getGlueUnitState(theGlueUnitState);

		for(int i=0; i<8 ; i++)
		{
			WrapArmNumber theWrapArmNumber;
			switch (i)
			{
			//WRAP ARM 21
			case 0:
				theWrapArmNumber = S1_2;
				break;
			//WRAP ARM 51
			case 1:
				theWrapArmNumber = S1_5;
				break;
			//WRAP ARM 4
			case 2:
				theWrapArmNumber = B1_GU;
				break;
			//WRAP ARM 40
			case 3:
				theWrapArmNumber = B1_GUO;
				break;
			//WRAP ARM 5
			case 4:
				theWrapArmNumber = B2_GU;
				break;
			//WRAP ARM 50
			case 5:
				theWrapArmNumber = B2_GUO;
				break;
			//WRAP ARM 6
			case 6:
				theWrapArmNumber = B3_GU;
				break;
			//WRAP ARM 60
			case 7:
				theWrapArmNumber = B3_GUO;
				break;
			
			//WRAP ARM 21
			default:
				theWrapArmNumber = S1_2;
				break;
			}

			State::WrapArmState theWrapArmState = theGlueUnitState.CtrlData.getWrapArmByNumber(theWrapArmNumber);

			if(theWrapArmState.isUsed())
			{
				o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.wrapArmPreHeater%d.Visible=true",theWrapArmNumber);
				o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.wa_wrapArmPreHeater%d.Visible=true",theWrapArmNumber);
				o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.wa_wrapArmPreHeater%d.InLocal=%s",theWrapArmNumber,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmLocal()));
				o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.wa_wrapArmPreHeater%d.Auto=%s",theWrapArmNumber,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmAuto()));
				o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.wa_wrapArmPreHeater%d.IsUnder=%s",theWrapArmNumber,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmUnder()));
				o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.wa_wrapArmPreHeater%d.Position=%d",theWrapArmNumber,
					theWrapArmState.getPosition_UPercINT().getValue());
				o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.wa_wrapArmPreHeater%d.IsMaxPos=%s",theWrapArmNumber,
					String::Bool2Alpha(theWrapArmState.getIsMaxPos()));
				o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.wa_wrapArmPreHeater%d.IsMinPos=%s",theWrapArmNumber,
					String::Bool2Alpha(theWrapArmState.getIsMinPos()));
			}
		}

		//GLUE GAP 1, 2, 3 , 4 
		for(int i=1; i<=4 ; i++)
		{
			GlueGapNumber theGlueGapNumber = GlueGapNumber(i);
			State::GlueGapState theGlueGapState = theGlueUnitState.CtrlData.getGlueGapByNumber(theGlueGapNumber);

			if(theGlueGapState.isUsed())
			{
				o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.glueGap%d.Visible=true",theGlueGapNumber);
				o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.gg_glueGap%d.Visible=true",theGlueGapNumber);
				o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.gg_glueGap%d.InLocal=%s",theGlueGapNumber,
					String::Bool2Alpha(theGlueGapState.getIsGlueGapLocal()));
				o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.gg_glueGap%d.Auto=%s",theGlueGapNumber,
					String::Bool2Alpha(theGlueGapState.getIsGlueGapAuto()));
				o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.gg_glueGap%d.Position=%d",theGlueGapNumber,
					theGlueGapState.getPositionINT().getValue());
				o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.gg_glueGap%d.IsMaxPos=%s",theGlueGapNumber,
					String::Bool2Alpha(theGlueGapState.getIsMaxPos()));
				o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.gg_glueGap%d.IsMinPos=%s",theGlueGapNumber,
					String::Bool2Alpha(theGlueGapState.getIsMinPos()));
			}
		}
    //agrassi_FI27-06-2011_1
    if (generalSettings.isJob(L"3763"))
    {
      //agrassi_FI16-05-2011_3
		  o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.ttb_thickness1.Value=%d",theGlueUnitState.thickness[0]);
		  o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.ttb_thickness2.Value=%d",theGlueUnitState.thickness[1]);
		  o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.ttb_PressorGluerGap1.Value=%d",theGlueUnitState.pressorGluerRollGap[0]);
		  o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_Status.ttb_PressorGluerGap2.Value=%d",theGlueUnitState.pressorGluerRollGap[1]);
    }
		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated GlueUnit_Status page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_GlueUnitGroup::ConsoleWrapArms(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_ConsoleWrapArms._Show()");

	while(!end_ConsoleWrapArms)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated GlueUnit_ConsoleWrapArms page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_GlueUnitGroup::ConsoleGlueGaps(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_ConsoleGlueGaps._Show()");

	while(!end_ConsoleGlueGaps)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated GlueUnit_ConsoleGlueGaps page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_GlueUnitGroup::AlarmsActive(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"GlueUnit_alarmsActive._Show()");

	while(!end_AlarmsActive)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated GlueUnit_alarmsActive page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_GlueUnitGroup::AlarmsHistory(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"GlueUnit_alarmsHistory._Show()");

	while(!end_AlarmsHistory)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated GlueUnit_alarmsActive page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_GlueUnitGroup::SettingsMachine(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"GlueUnit_settingsMachine._Show()");

	while(!end_SettingsMachine)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated GlueUnit_settingsMachine page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_GlueUnitGroup::SettingsTask(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"GlueUnit_settingsTask._Show()");

	while(!end_SettingsTask)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated GlueUnit_settingsTask page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_GlueUnitGroup::ProcessControlStatus(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"GlueUnit_ProcessControlStatus._Show()");

	while(!end_ProcessControlStatus)
	{
		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated GlueUnit_ProcessControlStatus page threads");
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_GlueUnitGroup::ProcessControlSettings(LPVOID  lpParam)
{
	wchar_t* formName=L"GlueUnit_ProcessControlSettings";

	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);

	State::GlueUnitState theGlueUnitState;
	
	while(!end_ProcessControlSettings)
	{
		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated %s page threads", formName);

	return ERROR_SUCCESS;
}