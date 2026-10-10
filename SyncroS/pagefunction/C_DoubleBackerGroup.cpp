#include ".\C_DoubleBackerGroup.h"

//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

//Page Management
#include "..\C_PageManager.h"

bool C_DoubleBackerGroup::end_Status01;
bool C_DoubleBackerGroup::end_Status02;
bool C_DoubleBackerGroup::end_Status03;
bool C_DoubleBackerGroup::end_Status04;
bool C_DoubleBackerGroup::end_Console01;
bool C_DoubleBackerGroup::end_Console02;
bool C_DoubleBackerGroup::end_Console03;
bool C_DoubleBackerGroup::end_Console04;
bool C_DoubleBackerGroup::end_JacksConsole;
bool C_DoubleBackerGroup::end_AlarmsActive;
bool C_DoubleBackerGroup::end_AlarmsHistory;
bool C_DoubleBackerGroup::end_SettingsMachine;
bool C_DoubleBackerGroup::end_SettingsStatus;
bool C_DoubleBackerGroup::end_SettingsTask;
bool C_DoubleBackerGroup::end_ProcessControlStatus;
bool C_DoubleBackerGroup::end_ProcessControlSettings;

C_DoubleBackerGroup::C_DoubleBackerGroup(void)
{
	pid_DoubleBackerPage=NULL;
	TerminateThread(false);
}

C_DoubleBackerGroup::~C_DoubleBackerGroup(void)
{
}

bool C_DoubleBackerGroup::ChooseDoubleBackerPage(String::StrType  thePageRequest)
{
	//DoubleBacker Group
	//status
	if(thePageRequest.CompareNoCase(L"Status01"))
	{
		end_Status01 = false;
		pid_DoubleBackerPage=::CreateThread(NULL,0,C_DoubleBackerGroup::Status01,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"Status02"))
	{
		end_Status02 = false;
		pid_DoubleBackerPage=::CreateThread(NULL,0,C_DoubleBackerGroup::Status02,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"Status03"))
	{
		end_Status03 = false;
		pid_DoubleBackerPage=::CreateThread(NULL,0,C_DoubleBackerGroup::Status03,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"Status04"))
	{
		end_Status04 = false;
		pid_DoubleBackerPage=::CreateThread(NULL,0,C_DoubleBackerGroup::Status04,NULL,0,NULL);
		return true;
	}
	//Console01
	if(thePageRequest.CompareNoCase(L"Console01"))
	{
		end_Console01 = false;
		pid_DoubleBackerPage=::CreateThread(NULL,0,C_DoubleBackerGroup::Console01,NULL,0,NULL);
		return true;
	}
	//Console02
	if(thePageRequest.CompareNoCase(L"Console02"))
	{
		end_Console02 = false;
		pid_DoubleBackerPage=::CreateThread(NULL,0,C_DoubleBackerGroup::Console02,NULL,0,NULL);
		return true;
	}
	//Console03
	if(thePageRequest.CompareNoCase(L"Console03"))
	{
		end_Console03 = false;
		pid_DoubleBackerPage=::CreateThread(NULL,0,C_DoubleBackerGroup::Console03,NULL,0,NULL);
		return true;
	}
	//Console04
	if(thePageRequest.CompareNoCase(L"Console04"))
	{
		end_Console04 = false;
		pid_DoubleBackerPage=::CreateThread(NULL,0,C_DoubleBackerGroup::Console04,NULL,0,NULL);
		return true;
	}

	//alarms
	if(thePageRequest.CompareNoCase(L"AlarmsActive"))
	{
		end_AlarmsActive = false;
		pid_DoubleBackerPage=::CreateThread(NULL,0,C_DoubleBackerGroup::AlarmsActive,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"AlarmsHistory"))
	{
		end_AlarmsHistory = false;
		pid_DoubleBackerPage=::CreateThread(NULL,0,C_DoubleBackerGroup::AlarmsHistory,NULL,0,NULL);
		return true;
	}
	//Settings
	if(thePageRequest.CompareNoCase(L"SettingsMachine"))
	{
		end_SettingsMachine = false;
		pid_DoubleBackerPage=::CreateThread(NULL,0,C_DoubleBackerGroup::SettingsMachine,NULL,0,NULL);
		return true;
	}
	//Settings Status
	if(thePageRequest.CompareNoCase(L"SettingsStatus"))
	{
		end_SettingsStatus = false;
		pid_DoubleBackerPage=::CreateThread(NULL,0,C_DoubleBackerGroup::SettingsStatus,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"SettingsTask"))
	{
		end_SettingsTask = false;
		pid_DoubleBackerPage=::CreateThread(NULL,0,C_DoubleBackerGroup::SettingsTask,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"ProcessControlStatus"))
	{
		end_ProcessControlStatus = false;
		pid_DoubleBackerPage=::CreateThread(NULL,0,C_DoubleBackerGroup::ProcessControlStatus,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"ProcessControlSettings"))
	{
		end_ProcessControlSettings = false;
		pid_DoubleBackerPage=::CreateThread(NULL,0,C_DoubleBackerGroup::ProcessControlSettings,NULL,0,NULL);
		return true;
	}

	return false;
}

void C_DoubleBackerGroup::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_DoubleBackerPage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_DoubleBackerPage,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread DoubleBackerPage ... %d", ::GetLastError());
		}
	}
	else
	{
		end_Status01 = true;
		end_Status02 = true;
		end_Status03 = true;
		end_Status04 = true;
		end_Console01 = true;
		end_Console02 = true;
		end_Console03 = true;
		end_Console04 = true;
		end_JacksConsole = true;
		end_AlarmsActive = true;
		end_AlarmsHistory = true;
		end_SettingsMachine = true;
		end_SettingsTask = true;
		end_SettingsStatus = true;
		end_ProcessControlStatus=true;
		end_ProcessControlSettings=true;
	}
}

DWORD C_DoubleBackerGroup::SuspendThread(void)
{
	if(pid_DoubleBackerPage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_DoubleBackerPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page Double Backer Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_DoubleBackerGroup::ResumeThread(void)
{
	if(pid_DoubleBackerPage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_DoubleBackerPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page Double Backer Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}


DWORD WINAPI C_DoubleBackerGroup::Status01(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_status01.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.getMachineName());	

	State::DoubleBackerState theDoubleBackerState;
	theDoubleBackerState.Clear();

	//RFAZZI_FI28/06/2008_1.Added send order Push Button on machine status page
	bool communicationError = false;

	while(!end_Status01)
	{
		theDoubleBackerState.Clear();
		C_PageManager::SMState.getDoubleBackerState(theDoubleBackerState);
	
		o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01.tnud_Speed_UVmm_m.Value=%d",theDoubleBackerState.CtrlData.Monitor.getSpeed_UVmm_m());

    o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01._setLocaleRemoteMachine(b[%s])", 
      String::Bool2Alpha(theDoubleBackerState.CtrlData.Monitor.getIsMachineRemoteStatus()));

		for(int i=31; i<=41 ; i=i+10)
		{
			//WRAP ARM 31, 41
			State::WrapArmState theWrapArmState = theDoubleBackerState.CtrlData.getWrapArmByNumber(WrapArmNumber(i));

			if(theWrapArmState.isUsed())
			{
				o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01.wrapArmPreHeater%d.Visible=true",i);
				o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01.wa_wrapArmPreHeater%d.Visible=true",i);
				o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01.wa_wrapArmPreHeater%d.InLocal=%s",i,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmLocal()));
				o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01.wa_wrapArmPreHeater%d.Auto=%s",i,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmAuto()));
				o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01.wa_wrapArmPreHeater%d.IsUnder=%s",i,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmUnder()));
				o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01.wa_wrapArmPreHeater%d.Position=%d",i,
					theWrapArmState.getPosition_UPercINT().getValue());
				o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01.wa_wrapArmPreHeater%d.IsMaxPos=%s",i,
					String::Bool2Alpha(theWrapArmState.getIsMaxPos()));
				o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01.wa_wrapArmPreHeater%d.IsMinPos=%s",i,
					String::Bool2Alpha(theWrapArmState.getIsMinPos()));

        if(C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(WrapArmNumber(i)).getEnable())
          o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_status01.wa_wrapArmPreHeater%d.Temperature=%d", i, C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(WrapArmNumber(i)).getValue());
			}
		}

		//ORDERS
		if(theDoubleBackerState.CtrlData.orders.getIDCurrent().getEnable() && theDoubleBackerState.CtrlData.orders.getModIDCurrent().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01._setCurrentProgram(i[%d],i[%d])", theDoubleBackerState.CtrlData.orders.getIDCurrent().getValue(),theDoubleBackerState.CtrlData.orders.getModIDCurrent().getValue());
		else
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01._setCurrentProgram(i[%d],i[%d])", 0,0);
		if(theDoubleBackerState.CtrlData.orders.getIDNext().getEnable() && theDoubleBackerState.CtrlData.orders.getModIDNext().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01._setNextProgram(i[%d],i[%d])", theDoubleBackerState.CtrlData.orders.getIDNext().getValue(),theDoubleBackerState.CtrlData.orders.getModIDNext().getValue());
		else
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01._setNextProgram(i[%d],i[%d])", 0,0);

    LineConfig cfg;
    C_PageManager::SMLineConfig.getLineConfig(cfg);

    if( cfg.zoneDBConfig.doubleBacker.isExtraPaperPathParaffinMachineLinerPresent() )
    {
      o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01.ParaffinMachineLiner.Visible=true");
      o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01.ParaffinMachineLiner.Active=%s",
        String::Bool2Alpha(theDoubleBackerState.Control.getEnableParaffinMachineLiner()));
    }

		//RFAZZI_FI28/06/2008_1.Added send order Push Button on machine status page
		if(communicationError != theDoubleBackerState.CtrlData.alarms.getPLCCommError())
		{
			communicationError = theDoubleBackerState.CtrlData.alarms.getPLCCommError();
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status01._CommunicationError(b[%s])", String::Bool2Alpha(communicationError));
		}

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated doubleBacker_status01 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DoubleBackerGroup::Status02(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_status02.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.getMachineName());	

	State::DoubleBackerCtrlData theDoubleBackerCtrlData;
	theDoubleBackerCtrlData.Clear();

	while(!end_Status02)
	{
		theDoubleBackerCtrlData.Clear();
		C_PageManager::SMState.getDoubleBackerStateCtrlData(theDoubleBackerCtrlData);

		o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02._setVisibility(b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s])",
			String::Bool2Alpha(theDoubleBackerCtrlData.group1.getSteamPressure_UPdb().getEnable()),
			String::Bool2Alpha(theDoubleBackerCtrlData.group2.getSteamPressure_UPdb().getEnable()),
			String::Bool2Alpha(theDoubleBackerCtrlData.group3.getSteamPressure_UPdb().getEnable()),
      String::Bool2Alpha(theDoubleBackerCtrlData.group0.getSteamPressure_UPdb().getEnable()),
			String::Bool2Alpha(theDoubleBackerCtrlData.coldSection1.getPressure_UPerc().getEnable()),
			String::Bool2Alpha(theDoubleBackerCtrlData.screwJack1.getPressure_UPerc().getEnable()),
      String::Bool2Alpha(theDoubleBackerCtrlData.screwJack1.getMaxPressureValue_UPerc().getEnable() & !theDoubleBackerCtrlData.screwJack1.getIsRemote()) );
		
		//SteamPressureGroup1
		if(theDoubleBackerCtrlData.group1.getSteamPressure_UPdb().getEnable())
		{
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.AutoManualSteamPressureGroup1.Auto=%s",String::Bool2Alpha(theDoubleBackerCtrlData.group1.getIsAuto().getValue()));
			//RFAZZI_FI04/03/2009_1. Bug Fix showing the Loc/Rem status on the DoubleBacker and SingleFacer Status. It is inverted. 
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.LocalRemoteSteamPressureGroup1.InLocal=%s",String::Bool2Alpha(theDoubleBackerCtrlData.group1.getIsLocal().getValue()));
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.tnud_steamRequestGroup1_UPdb.Value=%d",theDoubleBackerCtrlData.group1.getSteamPressureRequest_UPdb().getValue());
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.tnud_steamValueGroup1_UPdb.Value=%d",theDoubleBackerCtrlData.group1.getSteamPressure_UPdb().getValue());
		}
		//SteamPressureGroup2
		if(theDoubleBackerCtrlData.group2.getSteamPressure_UPdb().getEnable())
		{
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.AutoManualSteamPressureGroup2.Auto=%s",String::Bool2Alpha(theDoubleBackerCtrlData.group2.getIsAuto().getValue()));
			//RFAZZI_FI04/03/2009_1. Bug Fix showing the Loc/Rem status on the DoubleBacker and SingleFacer Status. It is inverted. 
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.LocalRemoteSteamPressureGroup2.InLocal=%s",String::Bool2Alpha(theDoubleBackerCtrlData.group2.getIsLocal().getValue()));
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.tnud_steamRequestGroup2_UPdb.Value=%d",theDoubleBackerCtrlData.group2.getSteamPressureRequest_UPdb().getValue());
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.tnud_steamValueGroup2_UPdb.Value=%d",theDoubleBackerCtrlData.group2.getSteamPressure_UPdb().getValue());
		}
		//SteamPressureGroup3
		if(theDoubleBackerCtrlData.group3.getSteamPressure_UPdb().getEnable())
		{
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.AutoManualSteamPressureGroup3.Auto=%s",String::Bool2Alpha(theDoubleBackerCtrlData.group3.getIsAuto().getValue()));
			//RFAZZI_FI04/03/2009_1. Bug Fix showing the Loc/Rem status on the DoubleBacker and SingleFacer Status. It is inverted. 
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.LocalRemoteSteamPressureGroup3.InLocal=%s",String::Bool2Alpha(theDoubleBackerCtrlData.group3.getIsLocal().getValue()));
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.tnud_steamRequestGroup3_UPdb.Value=%d",theDoubleBackerCtrlData.group3.getSteamPressureRequest_UPdb().getValue());
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.tnud_steamValueGroup3_UPdb.Value=%d",theDoubleBackerCtrlData.group3.getSteamPressure_UPdb().getValue());
		}
    //SteamPressureGroup0
		if(theDoubleBackerCtrlData.group0.getSteamPressure_UPdb().getEnable())
		{
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.AutoManualSteamPressureGroup0.Auto=%s",String::Bool2Alpha(theDoubleBackerCtrlData.group0.getIsAuto().getValue()));
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.LocalRemoteSteamPressureGroup0.InLocal=%s",String::Bool2Alpha(theDoubleBackerCtrlData.group0.getIsLocal().getValue()));
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.tnud_steamRequestGroup0_UPdb.Value=%d",theDoubleBackerCtrlData.group0.getSteamPressureRequest_UPdb().getValue());
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.tnud_steamValueGroup0_UPdb.Value=%d",theDoubleBackerCtrlData.group0.getSteamPressure_UPdb().getValue());
		}
		//ColdSection
		if(theDoubleBackerCtrlData.coldSection1.getPressure_UPerc().getEnable())
		{
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.AutoManualColdSection.Auto=%s",String::Bool2Alpha(theDoubleBackerCtrlData.coldSection1.getIsAuto().getValue()));
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.LocalRemoteColdSection.InLocal=%s",String::Bool2Alpha(theDoubleBackerCtrlData.coldSection1.getIsLocal().getValue()));
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.ColdSection.Value=%d",theDoubleBackerCtrlData.coldSection1.getPressure_UPerc().getValue());
		}
		//ScrewJacks
		if(theDoubleBackerCtrlData.screwJack1.getPressure_UPerc().getEnable())
		{
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.AutoManualScrewJacks.Auto=%s",String::Bool2Alpha(theDoubleBackerCtrlData.screwJack1.getIsAuto().getValue()));
			//RFAZZI_FI04/03/2009_1. Bug Fix showing the Loc/Rem status on the DoubleBacker and SingleFacer Status. It is inverted. 
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.LocalRemoteScrewJacks.InLocal=%s",String::Bool2Alpha(theDoubleBackerCtrlData.screwJack1.getIsLocal().getValue()));
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.ScrewJack1.Value=%d",theDoubleBackerCtrlData.screwJack1.getPressure_UPerc().getValue());
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.ScrewJack2.Value=%d",theDoubleBackerCtrlData.screwJack2.getPressure_UPerc().getValue());
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.ScrewJack3.Value=%d",theDoubleBackerCtrlData.screwJack3.getPressure_UPerc().getValue());
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.ScrewJack4.Value=%d",theDoubleBackerCtrlData.screwJack4.getPressure_UPerc().getValue());
		}
		if(theDoubleBackerCtrlData.screwJack1.getMaxPressureValue_UPerc().getEnable())
		{
      o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.lbl_MaxValScrewJack1.Text=%d",theDoubleBackerCtrlData.screwJack1.getMaxPressureValue_UPerc().getValue());
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.lbl_MaxValScrewJack2.Text=%d",theDoubleBackerCtrlData.screwJack2.getMaxPressureValue_UPerc().getValue());
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.lbl_MaxValScrewJack3.Text=%d",theDoubleBackerCtrlData.screwJack3.getMaxPressureValue_UPerc().getValue());
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status02.lbl_MaxValScrewJack4.Text=%d",theDoubleBackerCtrlData.screwJack4.getMaxPressureValue_UPerc().getValue());
		}

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated doubleBacker_status02 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DoubleBackerGroup::Status03(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status03._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_status03.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.getMachineName());	

	State::DoubleBackerState theDoubleBackerState;
	theDoubleBackerState.Clear();

	while(!end_Status03)
	{
		theDoubleBackerState.Clear();
		C_PageManager::SMState.getDoubleBackerState(theDoubleBackerState);

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated doubleBacker_status03 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DoubleBackerGroup::Status04(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_status04._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_status04.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.getMachineName());	

	State::DoubleBackerState theDoubleBackerState;
	theDoubleBackerState.Clear();

	while(!end_Status04)
	{
		theDoubleBackerState.Clear();
		C_PageManager::SMState.getDoubleBackerState(theDoubleBackerState);

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated doubleBacker_status04 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DoubleBackerGroup::Console01(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_Console01._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_Console01.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.getMachineName());

	State::DoubleBackerState theDoubleBackerState;
	bool communicationError = false;

	while(!end_Console01)
	{
		theDoubleBackerState.Clear();
		C_PageManager::SMState.getDoubleBackerState(theDoubleBackerState);

		if(communicationError != theDoubleBackerState.CtrlData.alarms.getPLCCommError())
		{
			communicationError = theDoubleBackerState.CtrlData.alarms.getPLCCommError();
			o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_Console01._CommunicationError(b[%s])", String::Bool2Alpha(communicationError));
		}

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated doubleBacker_Console01 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DoubleBackerGroup::Console02(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_console02._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_console02.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.getMachineName());

	while(!end_Console02)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated doubleBacker_console02 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DoubleBackerGroup::Console03(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_Console03._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_Console03.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.getMachineName());

	while(!end_Console03)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated doubleBacker_Console03 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DoubleBackerGroup::Console04(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"doubleBacker_Console04._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_Console04.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.getMachineName());

	while(!end_Console04)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated doubleBacker_Console04 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DoubleBackerGroup::AlarmsActive(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_alarmsActive._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_alarmsActive.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.getMachineName());

	while(!end_AlarmsActive)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated doubleBacker_alarmsActive page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_DoubleBackerGroup::AlarmsHistory(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_alarmsHistory._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_alarmsHistory.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.getMachineName());

	while(!end_AlarmsHistory)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated doubleBacker_alarmsHistory page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DoubleBackerGroup::SettingsMachine(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsMachine._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsMachine.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.getMachineName());

	while(!end_SettingsMachine)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated doubleBacker_settingsMachine page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DoubleBackerGroup::SettingsStatus(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsStatus._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsStatus.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.getMachineName());

	State::ZoneDoubleBackerState theZoneDoubleBackerState;
	wchar_t dummyChar[MAX_PATH];

	while(!end_SettingsStatus)
	{
		C_PageManager::SMState.getZoneDoubleBackerState(theZoneDoubleBackerState);

		//CONTROL
		theZoneDoubleBackerState.Control.getModeToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsStatus.ttl_ZoneModeStr.Value=%s", dummyChar);
		theZoneDoubleBackerState.Control.getLocalModeToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsStatus.ttl_ZoneLocalModeStr.Value=%s", dummyChar);
		theZoneDoubleBackerState.Control.getSpeedSetpointReasonToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsStatus.ttl_SpeedSetpointReason.Value=%s", dummyChar);
		o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsStatus.tnud_SpeedSetpoint.Value=%d", 
			theZoneDoubleBackerState.Control.getSpeedSetpoint_UVmm_m());
		o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsStatus.chk_SpeedSetpointStop.Checked=%s", 
			String::Bool2Alpha(theZoneDoubleBackerState.Control.getSpeedSetpointStop()));
		theZoneDoubleBackerState.Control.getSpeedCommandToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsStatus.ttl_SpeedCommand.Value=%s", dummyChar);
		o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsStatus.tnud_ResumeSpeed.Value=%d", 
			theZoneDoubleBackerState.Control.getResumeSpeed_UVmm_m());

		//Zone Monitor
		o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsStatus.tnud_ZoneSpeed.Value=%d",  
			theZoneDoubleBackerState.Monitor.getSpeed_UVmm_m());
		o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsStatus.chk_ZoneRunning.Checked=%s",  
			(theZoneDoubleBackerState.Monitor.isRunning())? L"true" : L"false");
		o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsStatus.tnud_Production.Value=%d",  
			theZoneDoubleBackerState.Monitor.getProduction_UDmm());

		//Double Backer Monitor
		o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsStatus.chk_SFRunning.Checked=%s", 
			String::Bool2Alpha(theZoneDoubleBackerState.doubleBackerState.CtrlData.Monitor.getIsRunning()));
		o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsStatus.tnud_SFSpeed.Value=%d",  
			theZoneDoubleBackerState.doubleBackerState.CtrlData.Monitor.getSpeed_UVmm_m());

		
		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated doubleBacker_settingsStatus page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DoubleBackerGroup::SettingsTask(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsTask._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"doubleBacker_settingsTask.lbl_Title_YN.Text=%s",C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.getMachineName());

	while(!end_SettingsTask)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated doubleBacker_settingsTask page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DoubleBackerGroup::ProcessControlStatus(LPVOID  lpParam)
{
	wchar_t* formName=L"doubleBacker_ProcessControlStatus";

	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);

	State::ZoneDoubleBackerState theZoneDoubleBackerState;

	while(!end_ProcessControlStatus)
	{
		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated %s page threads", formName);
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_DoubleBackerGroup::ProcessControlSettings(LPVOID  lpParam)
{
	wchar_t* formName=L"doubleBacker_ProcessControlSettings";

	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);

	State::ZoneDoubleBackerState theZoneDoubleBackerState;

	while(!end_ProcessControlSettings)
	{
		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated %s page threads", formName);
	
	return ERROR_SUCCESS;
}
