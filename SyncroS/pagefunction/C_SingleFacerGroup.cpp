#include ".\C_SingleFacerGroup.h"

#include "..\C_PageManager.h"

//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Sockte
extern Network::C_SocketTCP o_socket;

int C_SingleFacerGroup::k;

bool C_SingleFacerGroup::end_Status01;
bool C_SingleFacerGroup::end_Status02;
bool C_SingleFacerGroup::end_Status03;
bool C_SingleFacerGroup::end_Status04;
bool C_SingleFacerGroup::end_Console01;
bool C_SingleFacerGroup::end_Console02;
bool C_SingleFacerGroup::end_Console03;
bool C_SingleFacerGroup::end_Console04;
bool C_SingleFacerGroup::end_AlarmsActive;
bool C_SingleFacerGroup::end_AlarmsHistory;
bool C_SingleFacerGroup::end_SettingsMachine;
bool C_SingleFacerGroup::end_SettingsTask;
bool C_SingleFacerGroup::end_SettingsSFStatus;
bool C_SingleFacerGroup::end_SettingsBridgeStatus;
bool C_SingleFacerGroup::end_ProcessControlStatus;
bool C_SingleFacerGroup::end_ProcessControlSettings;

C_SingleFacerGroup::C_SingleFacerGroup(void)
{
	pid_SingleFacerPage=NULL;
	k = 0;
	TerminateThread(false);
}

C_SingleFacerGroup::~C_SingleFacerGroup(void)
{
}

bool C_SingleFacerGroup::ChooseSingleFacerPage(String::StrType  thePageRequest, const int index)
{
	//Single Facer status 01
	if(thePageRequest.CompareNoCase(L"Status01"))
	{
		k = index;
		end_Status01 = false;
		pid_SingleFacerPage=::CreateThread(NULL,0,Status01,&k,0,NULL);
		return true;
	}

	//Single Facer status 02
	if(thePageRequest.CompareNoCase(L"Status02"))
	{
		k = index;
		end_Status02 = false;
		pid_SingleFacerPage=::CreateThread(NULL,0,Status02,&k,0,NULL);
		return true;
	}

	//Single Facer status 03
	if(thePageRequest.CompareNoCase(L"Status03"))
	{
		k = index;
		end_Status03 = false;
		pid_SingleFacerPage=::CreateThread(NULL,0,Status03,&k,0,NULL);
		return true;
	}

	//Single Facer status 04
	if(thePageRequest.CompareNoCase(L"Status04"))
	{
		k = index;
		end_Status04 = false;
		pid_SingleFacerPage=::CreateThread(NULL,0,Status04,&k,0,NULL);
		return true;
	}
	//Single Facer alarmActive
	if(thePageRequest.CompareNoCase(L"AlarmActive"))
	{
		k = index;
		end_AlarmsActive = false;
		pid_SingleFacerPage=::CreateThread(NULL,0,AlarmActive,&k,0,NULL);
		return true;
	}
	
	//Single Facer alarmHistory
	if(thePageRequest.CompareNoCase(L"AlarmHistory"))
	{
		k = index;
		end_AlarmsHistory = false;
		pid_SingleFacerPage=::CreateThread(NULL,0,AlarmHistory,&k,0,NULL);
		return true;
	}

	//Single Facer console01
	if(thePageRequest.CompareNoCase(L"Console01"))
	{
		k = index;
		end_Console01 = false;
		pid_SingleFacerPage=::CreateThread(NULL,0,Console01,&k,0,NULL);
		return true;
	}

	//Single Facer console02
	if(thePageRequest.CompareNoCase(L"Console02"))
	{
		k = index;
		end_Console02 = false;
		pid_SingleFacerPage=::CreateThread(NULL,0,Console02,&k,0,NULL);
		return true;
	}

	//Single Facer console03
	if(thePageRequest.CompareNoCase(L"Console03"))
	{
		k = index;
		end_Console03 = false;
		pid_SingleFacerPage=::CreateThread(NULL,0,Console03,&k,0,NULL);
		return true;
	}

	//Single Facer console04
	if(thePageRequest.CompareNoCase(L"Console04"))
	{
		k = index;
		end_Console04 = false;
		pid_SingleFacerPage=::CreateThread(NULL,0,Console04,&k,0,NULL);
		return true;
	}
	
	//Single Facer settingsMachine
	if(thePageRequest.CompareNoCase(L"SettingsMachine"))
	{
		k = index;
		end_SettingsMachine = false;
		pid_SingleFacerPage=::CreateThread(NULL,0,SettingsMachine,&k,0,NULL);
		return true;
	}

	//Single Facer settingsSFStatus
	if(thePageRequest.CompareNoCase(L"SettingsSFStatus"))
	{
		k = index;
		end_SettingsSFStatus = false;
		pid_SingleFacerPage=::CreateThread(NULL,0,SettingsSFStatus,&k,0,NULL);
		return true;
	}
	
	//Single Facer settingsBridgeStatus
	if(thePageRequest.CompareNoCase(L"SettingsBridgeStatus"))
	{
		k = index;
		end_SettingsBridgeStatus = false;
		pid_SingleFacerPage=::CreateThread(NULL,0,SettingsBridgeStatus,&k,0,NULL);
		return true;
	}

	//Single Facer settingsTask
	if(thePageRequest.CompareNoCase(L"SettingsTask"))
	{
		k = index;
		end_SettingsTask = false;
		pid_SingleFacerPage=::CreateThread(NULL,0,SettingsTask,&k,0,NULL);
		return true;
	}

	//Process Control status
	if(thePageRequest.CompareNoCase(L"ProcessControlStatus"))
	{
		k = index;
		end_ProcessControlStatus = false;
		pid_SingleFacerPage=::CreateThread(NULL,0,ProcessControlStatus,&k,0,NULL);
		return true;
	}

	//Process Control settings
	if(thePageRequest.CompareNoCase(L"ProcessControlSettings"))
	{
		k = index;
		end_ProcessControlSettings = false;
		pid_SingleFacerPage=::CreateThread(NULL,0,ProcessControlSettings,&k,0,NULL);
		return true;
	}

	return false;
}

void C_SingleFacerGroup::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_SingleFacerPage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_SingleFacerPage,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread SingleFacerPage ... %d", ::GetLastError());
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
		end_AlarmsActive = true;
		end_AlarmsHistory = true;
		end_SettingsMachine = true;
		end_SettingsTask = true;
		end_SettingsSFStatus = true;
		end_SettingsBridgeStatus = true;
		end_ProcessControlStatus = true;
		end_ProcessControlSettings = true;
	}
}

DWORD C_SingleFacerGroup::SuspendThread(void)
{
	if(pid_SingleFacerPage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_SingleFacerPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page Single Facer Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_SingleFacerGroup::ResumeThread(void)
{
	if(pid_SingleFacerPage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_SingleFacerPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page Single Facer Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SingleFacerGroup::Status01(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01._Show()",index);
	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.lbl_Title_YN.Text=%s %d",index, C_PageManager::SMState.getZoneSingleFacerState(index).singleFacerState.getMachineName(),index);
	
	State::SingleFacerState theSingleFacerState; 
	theSingleFacerState.Clear();

	wchar_t wrapArmName[MAX_PATH];

	bool communicationError = false;
	
	while(!end_Status01)
	{	
		C_PageManager::SMState.getSingleFacerState(theSingleFacerState,index);

		if(communicationError != theSingleFacerState.CtrlData.Alarms.getIsPLCCommError())
		{
			communicationError = theSingleFacerState.CtrlData.Alarms.getIsPLCCommError();
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01._CommunicationError(b[%s])", index, String::Bool2Alpha(communicationError));
		}

    o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01._setLocaleRemoteMachine(b[%s])", index, 
      String::Bool2Alpha(theSingleFacerState.CtrlData.Monitor.getIsMachineRemoteStatus()));

		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.tnud_Speed_UVmm_m.Value=%d",index,theSingleFacerState.CtrlData.Monitor.getSpeed_UVmm_m());
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01._setSFNCartridge(s[%c],b[%s])",index,
			theSingleFacerState.CtrlData.Monitor.getFluteId(),
			String::Bool2Alpha(theSingleFacerState.CtrlData.Alarms.getFluteError()));

		//12 = Em, 22 = Im
		for(int i = 12; i<=22 ; i=i+10)
		{
			::ZeroMemory(wrapArmName, sizeof(wchar_t) * MAX_PATH);
			WrapArmNumber theWrapArmNumber = WrapArmNumber(i);
			Utility::getWrapArmNameByNumber(theWrapArmNumber,wrapArmName, MAX_PATH);

			State::WrapArmState theWrapArmState = theSingleFacerState.CtrlData.getWrapArmByNumber(theWrapArmNumber);

			if(theWrapArmState.isUsed())
			{
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.wrapArmPreHeater%s.Visible=true",index,wrapArmName);
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.wa_wrapArmPreHeater%s.Visible=true",index,wrapArmName);
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.wa_wrapArmPreHeater%s.InLocal=%s",index,wrapArmName,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmLocal()));
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.wa_wrapArmPreHeater%s.Auto=%s",index,wrapArmName,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmAuto()));
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.wa_wrapArmPreHeater%s.IsUnder=%s",index,wrapArmName,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmUnder()));
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.wa_wrapArmPreHeater%s.Position=%d",index,wrapArmName,
					theWrapArmState.getPosition_UPercINT().getValue());
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.wa_wrapArmPreHeater%s.IsMaxPos=%s",index,wrapArmName,
					String::Bool2Alpha(theWrapArmState.getIsMaxPos().getValue()));
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.wa_wrapArmPreHeater%s.IsMinPos=%s",index,wrapArmName,
					String::Bool2Alpha(theWrapArmState.getIsMinPos().getValue()));

        int theWrapArmTemperatureNumber = WrapArmNumber(i+(2*(index-1)));
        if(C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(theWrapArmTemperatureNumber).getEnable())
          o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_status01.wa_wrapArmPreHeater%s.Temperature=%d",index, wrapArmName, 
          C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(theWrapArmTemperatureNumber).getValue());

			}
		}

		//13 = El, 23 = Il
		for(int i = 13; i<=23 ; i=i+10)
		{
			::ZeroMemory(wrapArmName, sizeof(wchar_t) * MAX_PATH);
			WrapArmNumber theWrapArmNumber = WrapArmNumber(i);
			Utility::getWrapArmNameByNumber(theWrapArmNumber,wrapArmName, MAX_PATH);

			State::WrapArmState theWrapArmState = theSingleFacerState.CtrlData.getWrapArmByNumber(theWrapArmNumber);

			if(theWrapArmState.isUsed())
			{
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.wrapArmPreHeater%s.Visible=true",index,wrapArmName);
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.wa_wrapArmPreHeater%s.Visible=true",index,wrapArmName);
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.wa_wrapArmPreHeater%s.InLocal=%s",index,wrapArmName,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmLocal()));
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.wa_wrapArmPreHeater%s.Auto=%s",index,wrapArmName,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmAuto()));
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.wa_wrapArmPreHeater%s.IsUnder=%s",index,wrapArmName,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmUnder()));
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.wa_wrapArmPreHeater%s.Position=%d",index,wrapArmName,
					theWrapArmState.getPosition_UPercINT().getValue());
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.wa_wrapArmPreHeater%s.IsMaxPos=%s",index,wrapArmName,
					String::Bool2Alpha(theWrapArmState.getIsMaxPos().getValue()));
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.wa_wrapArmPreHeater%s.IsMinPos=%s",index,wrapArmName,
					String::Bool2Alpha(theWrapArmState.getIsMinPos().getValue()));

        int theWrapArmTemperatureNumber = WrapArmNumber(i+(2*(index-1)));
        if(C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(theWrapArmTemperatureNumber).getEnable())
          o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_status01.wa_wrapArmPreHeater%s.Temperature=%d",index, wrapArmName, 
          C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(theWrapArmTemperatureNumber).getValue());
			}
		}

		State::GlueGapState theGlueGapState = theSingleFacerState.CtrlData.gluegap;
		if(theGlueGapState.isUsed())
		{
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.glueGap1.Visible=true",index);
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.gg_glueGap1.Visible=true",index);
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.gg_glueGap1.InLocal=%s",index,
				String::Bool2Alpha(theGlueGapState.getIsGlueGapLocal()));
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.gg_glueGap1.Auto=%s",index,
				String::Bool2Alpha(theGlueGapState.getIsGlueGapAuto()));
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.gg_glueGap1.Position=%d",index,
				theGlueGapState.getPositionINT().getValue());
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.gg_glueGap1.IsMaxPos=%s",index,
				String::Bool2Alpha(theGlueGapState.getIsMaxPos().getValue()));
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.gg_glueGap1.IsMinPos=%s",index,
				String::Bool2Alpha(theGlueGapState.getIsMinPos().getValue()));
		}

		//ORDERS
		if(theSingleFacerState.CtrlData.orders.getIDCurrent().getEnable() && theSingleFacerState.CtrlData.orders.getModIDCurrent().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01._setCurrentProgram(i[%d],i[%d])",index, theSingleFacerState.CtrlData.orders.getIDCurrent().getValue(),theSingleFacerState.CtrlData.orders.getModIDCurrent().getValue());
		else
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01._setCurrentProgram(i[%d],i[%d])",index,0,0);
		if(theSingleFacerState.CtrlData.orders.getIDNext().getEnable() && theSingleFacerState.CtrlData.orders.getModIDNext().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01._setNextProgram(i[%d],i[%d])",index, theSingleFacerState.CtrlData.orders.getIDNext().getValue(),theSingleFacerState.CtrlData.orders.getModIDNext().getValue());
		else
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01._setNextProgram(i[%d],i[%d])",index,0,0);

    LineConfig cfg;
    C_PageManager::SMLineConfig.getLineConfig(cfg);

    if( cfg.getSFZone(index).singleFacer.isExtraPaperPathParaffinMachineLinerPresent() )
    {
      o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.ParaffinMachineLiner.Visible=true",index);
      o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.ParaffinMachineLiner.Active=%s",index,
        String::Bool2Alpha(theSingleFacerState.Control.getEnableParaffinMachineLiner()));
    }
    if( cfg.getSFZone(index).singleFacer.isExtraPaperPathParaffinMachineMediumPresent() )
    {
      o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.ParaffinMachineMedium.Visible=true",index);
      o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01.ParaffinMachineMedium.Active=%s",index,
        String::Bool2Alpha(theSingleFacerState.Control.getEnableParaffinMachineMedium()));
    }

		//RFAZZI_FI28/06/2008_1.Added send order Push Button on machine status page
		if(communicationError != theSingleFacerState.CtrlData.Alarms.getIsPLCCommError())
		{
			communicationError = theSingleFacerState.CtrlData.Alarms.getIsPLCCommError();
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status01._CommunicationError(b[%s])", index, String::Bool2Alpha(communicationError));
		}

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated singleFacer%d_status01 page threads",index);

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SingleFacerGroup::Status02(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02._Show()",index);
	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.lbl_Title_YN.Text=%s %d",index, C_PageManager::SMState.getZoneSingleFacerState(index).singleFacerState.getMachineName(),index);
	
	State::SingleFacerState theSingleFacerState; 
	theSingleFacerState.Clear();

	bool communicationError = false;

	while(!end_Status02)
	{
		C_PageManager::SMState.getSingleFacerState(theSingleFacerState,index);

		if(communicationError != theSingleFacerState.CtrlData.Alarms.getIsPLCCommError())
		{
			communicationError = theSingleFacerState.CtrlData.Alarms.getIsPLCCommError();
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02._CommunicationError(b[%s])", index, String::Bool2Alpha(communicationError));
		}

		// Main Steam Pressure
		if(theSingleFacerState.CtrlData.getIsMainSteamAuto().getEnable())
		{
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.pnl_MainSteam.Visible=true",index);
      o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.tnud_feedBackMainSteamPressure_UPdb.Value=%d",index, theSingleFacerState.CtrlData.getFeedBackMainSteam_UPdb().getValue());
      o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.tnud_requestedMainSteamPressure_UPdb.Value=%d",index, theSingleFacerState.CtrlData.getRequestedMainSteam_UPdb().getValue());
      o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.AutoManualMainSteam.Auto=%s",index, String::Bool2Alpha(theSingleFacerState.CtrlData.getIsMainSteamAuto().getValue()));
      o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.LocalRemoteSfMainSteam.InLocal=%s",index, String::Bool2Alpha(!theSingleFacerState.CtrlData.getIsMainSteamRemote().getValue()));
		}
		
		// Pressure Roll
		if(theSingleFacerState.CtrlData.getIsPressureRollAuto().getEnable())
		{
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.pnl_pressureRoll.Visible=true",index);
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.AutoManualPressureRoll.Auto=%s",index, String::Bool2Alpha(theSingleFacerState.CtrlData.getIsPressureRollAuto().getValue()));
			
      if(theSingleFacerState.CtrlData.getPressureRollPressureOp_UPKg_cm().getEnable())
        o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.tnud_pressRollOp_UPKg_cm.Value=%d",index, theSingleFacerState.CtrlData.getPressureRollPressureOp_UPKg_cm().getValue());
			
      if(theSingleFacerState.CtrlData.getPressureRollPressureNonOp_UPKg_cm().getEnable())
        o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.tnud_pressRollNoOp_UPKg_cm.Value=%d",index, theSingleFacerState.CtrlData.getPressureRollPressureNonOp_UPKg_cm().getValue());
			
      o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.tnud_pressRollOp_UPerc.Value=%d",index, theSingleFacerState.CtrlData.getPressureRollPositionOp_UPerc().getValue());
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.tnud_pressRollNoOp_UPerc.Value=%d",index, theSingleFacerState.CtrlData.getPressureRollPositionNonOp_UPerc().getValue());
		}
		
		//TORPRESS	
		if(theSingleFacerState.CtrlData.getIsTorpressAuto().getEnable())
		{
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.pnl_torpress.Visible=true",index);
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.AutoManualTorpress.Auto=%s",index, String::Bool2Alpha(theSingleFacerState.CtrlData.getIsTorpressAuto().getValue()));
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.tnud_torpressPressureRoll_UPdb.Value=%d",index, theSingleFacerState.CtrlData.getTorpressPressureRoll_UPdb().getValue());
      
      if(theSingleFacerState.CtrlData.getTorpressCorrFeedbackOp_UPdb().getEnable())
      {
			  o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.tnud_torpressOp_UPdb.Value=%d",index, theSingleFacerState.CtrlData.getTorpressCorrFeedbackOp_UPdb().getValue());
			  o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.tnud_torpressNoOp_UPdb.Value=%d",index, theSingleFacerState.CtrlData.getTorpressCorrFeedbackNonOp_UPdb().getValue());
      }
      else
      {			  
        o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.tnud_torpressOp_UPdb.Visible=false",index);
        o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.pct_torpressOp.Visible=false",index);
        o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.ttl_torpressOp.Visible=false",index);
        
			  o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.tnud_torpressNoOp_UPdb.Visible=false",index);
        o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.pct_torpressNoOp.Visible=false",index);
        o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.ttl_torpressNoOp.Visible=false",index);
      }
		}
		
		//steamShower
		if(theSingleFacerState.CtrlData.steamShower.getIsSteamShowerAuto().getEnable())
		{
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.pnl_steamShower.Visible=true",index);
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.AutoManualSteamShower.Auto=%s",index, String::Bool2Alpha(theSingleFacerState.CtrlData.steamShower.getIsSteamShowerAuto().getValue()));
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.stb_steamShowerOnOff.Active=%s",index, String::Bool2Alpha(theSingleFacerState.CtrlData.steamShower.getIsSteamShowerOn().getValue()));
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.tnud_steamShower_UPerc.Value=%d",index, theSingleFacerState.CtrlData.steamShower.getSteamShower_UPerc().getValue());
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status02.LocalRemoteSteamShower.InLocal=%s",index, String::Bool2Alpha(!theSingleFacerState.CtrlData.steamShower.getIsSteamShowerRemote().getValue()));
		}
		
		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated singleFacer%d_status02 page threads",index);

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SingleFacerGroup::Status03(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status03._Show()",index);
	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status03.lbl_Title_YN.Text=%s %d",index, C_PageManager::SMState.getZoneSingleFacerState(index).singleFacerState.getMachineName(),index);
			
	while(!end_Status03)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated singleFacer%d_status03 page threads",index);

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SingleFacerGroup::Status04(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status04._Show()",index);
	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_status04.lbl_Title_YN.Text=%s %d",index, C_PageManager::SMState.getZoneSingleFacerState(index).singleFacerState.getMachineName(),index);
			
	while(!end_Status04)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated singleFacer%d_status04 page threads",index);

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SingleFacerGroup::Console01(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console01._Show()",index);
	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console01.lbl_Title_YN.Text=%s %d",index, C_PageManager::SMState.getZoneSingleFacerState(index).singleFacerState.getMachineName(),index);
	
	State::SingleFacerState theSingleFacerState; 
	theSingleFacerState.Clear();

	bool communicationError = false;

	while(!end_Console01)
	{
		C_PageManager::SMState.getSingleFacerState(theSingleFacerState,index);

		if(communicationError != theSingleFacerState.CtrlData.Alarms.getIsPLCCommError())
		{
			communicationError = theSingleFacerState.CtrlData.Alarms.getIsPLCCommError();
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console01._CommunicationError(b[%s])", index, String::Bool2Alpha(communicationError));
		}

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated singleFacer%d_Console01 page threads",index);

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SingleFacerGroup::Console02(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02._Show()",index);
	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.lbl_Title_YN.Text=%s %d",index, C_PageManager::SMState.getZoneSingleFacerState(index).singleFacerState.getMachineName(),index);

	State::SingleFacerState theSingleFacerState; 
	theSingleFacerState.Clear();

	wchar_t wrapArmName[MAX_PATH];
	bool communicationError = false;

	while(!end_Console02)
	{
		C_PageManager::SMState.getSingleFacerState(theSingleFacerState,index);

		if(communicationError != theSingleFacerState.CtrlData.Alarms.getIsPLCCommError())
		{
			communicationError = theSingleFacerState.CtrlData.Alarms.getIsPLCCommError();
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02._CommunicationError(b[%s])", index, String::Bool2Alpha(communicationError));
		}

		//12 = Em, 22 = Im
		for(int i =12; i<=22 ; i=i+10)
		{
			::ZeroMemory(wrapArmName, sizeof(wchar_t) * MAX_PATH);
			WrapArmNumber theWrapArmNumber = WrapArmNumber(i);
			Utility::getWrapArmNameByNumber(theWrapArmNumber,wrapArmName, MAX_PATH);

			State::WrapArmState theWrapArmState = theSingleFacerState.CtrlData.getWrapArmByNumber(theWrapArmNumber);

			if(theWrapArmState.isUsed())
			{
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.ttl_WrapArm%s.Visible=true",index,wrapArmName);
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.LocalRemoteWrapArmPreHeater%s.Visible=true",index,wrapArmName);
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.AutoManualWrapArmPreHeater%s.Visible=true",index,wrapArmName);
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.WrapArmPreHeater%s.Visible=true",index,wrapArmName);
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.WrapArmPreHeater%s.Value=%d",index,wrapArmName, theWrapArmState.getPosition_UPercINT().getValue());
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.LocalRemoteWrapArmPreHeater%s.InLocal=%s",index,wrapArmName, String::Bool2Alpha(theWrapArmState.getIsWrapArmLocal()));
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.AutoManualWrapArmPreHeater%s.Auto=%s",index,wrapArmName, String::Bool2Alpha(theWrapArmState.getIsWrapArmAuto()));
			}
		}

		//13 = El, 23 = Il
		for(int i =13; i<=23 ; i=i+10)
		{		
			::ZeroMemory(wrapArmName, sizeof(wchar_t) * MAX_PATH);
			WrapArmNumber theWrapArmNumber = WrapArmNumber(i);
			Utility::getWrapArmNameByNumber(theWrapArmNumber,wrapArmName, MAX_PATH);

			State::WrapArmState theWrapArmState = theSingleFacerState.CtrlData.getWrapArmByNumber(theWrapArmNumber);

			if(theWrapArmState.isUsed())
			{
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.ttl_WrapArm%s.Visible=true",index,wrapArmName);
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.LocalRemoteWrapArmPreHeater%s.Visible=true",index,wrapArmName);
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.AutoManualWrapArmPreHeater%s.Visible=true",index,wrapArmName);
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.WrapArmPreHeater%s.Visible=true",index,wrapArmName);
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.WrapArmPreHeater%s.Value=%d",index,wrapArmName, theWrapArmState.getPosition_UPercINT().getValue());
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.LocalRemoteWrapArmPreHeater%s.InLocal=%s",index,wrapArmName, String::Bool2Alpha(theWrapArmState.getIsWrapArmLocal()));
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.AutoManualWrapArmPreHeater%s.Auto=%s",index,wrapArmName, String::Bool2Alpha(theWrapArmState.getIsWrapArmAuto()));
			}
		}

		State::GlueGapState theGlueGapState = theSingleFacerState.CtrlData.gluegap;
		if(theGlueGapState.isUsed())
		{
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.ttl_GlueGap1.Visible=true",index);
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.LocalRemoteGlueGap1.Visible=true",index);
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.AutoManualGlueGap1.Visible=true",index);
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.GlueGap1.Visible=true",index);
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.GlueGap1.Value=%d",index, theGlueGapState.getPositionINT().getValue());
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.LocalRemoteGlueGap1.InLocal=%s",index, String::Bool2Alpha(theGlueGapState.getIsGlueGapLocal()));
			o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console02.AutoManualGlueGap1.Auto=%s",index, String::Bool2Alpha(theGlueGapState.getIsGlueGapAuto()));
		}

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated singleFacer%d_Console02 page threads",index);

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SingleFacerGroup::Console03(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console03._Show()",index);
	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console03.lbl_Title_YN.Text=%s %d",index, C_PageManager::SMState.getZoneSingleFacerState(index).singleFacerState.getMachineName(),index);
			
	while(!end_Console03)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated singleFacer%d_Console03 page threads",index);

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SingleFacerGroup::Console04(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console04._Show()",index);
	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_Console04.lbl_Title_YN.Text=%s %d",index, C_PageManager::SMState.getZoneSingleFacerState(index).singleFacerState.getMachineName(),index);
			
	while(!end_Console04)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated singleFacer%d_Console04 page threads",index);

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SingleFacerGroup::AlarmActive(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_AlarmActive._Show()",index);
	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_AlarmActive.lbl_Title_YN.Text=%s %d",index, C_PageManager::SMState.getZoneSingleFacerState(index).singleFacerState.getMachineName(),index);

	while(!end_AlarmsActive)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated singleFacer%d_AlarmActive page threads",index);
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SingleFacerGroup::AlarmHistory(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_AlarmHistory._Show()",index);
	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_AlarmHistory.lbl_Title_YN.Text=%s %d",index, C_PageManager::SMState.getZoneSingleFacerState(index).singleFacerState.getMachineName(),index);

	while(!end_AlarmsHistory)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated singleFacer%d_AlarmHistory page threads",index);
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SingleFacerGroup::SettingsMachine(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_settingsMachine._Show()",index);
	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_settingsMachine.lbl_Title_YN.Text=%s %d",index, C_PageManager::SMState.getZoneSingleFacerState(index).singleFacerState.getMachineName(),index);

	while(!end_SettingsMachine)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated singleFacer%d_settingsMachine page threads",index);
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SingleFacerGroup::SettingsTask(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsTask._Show()",index);
	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsTask.lbl_Title_YN.Text=%s %d",index, C_PageManager::SMState.getZoneSingleFacerState(index).singleFacerState.getMachineName(),index);

	while(!end_SettingsTask)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated singleFacer%d_SettingsTask page threads",index);
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SingleFacerGroup::SettingsSFStatus(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsSFStatus._Show()",index);
	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsSFStatus.lbl_Title_YN.Text=%s %d",index, C_PageManager::SMState.getZoneSingleFacerState(index).singleFacerState.getMachineName(),index);

	State::ZoneSingleFacerState theZoneSingleFacerState;
	wchar_t dummyChar[MAX_PATH];

	while(!end_SettingsSFStatus)
	{
		C_PageManager::SMState.getZoneSingleFacerState(theZoneSingleFacerState,index);

		//Zone Control
		theZoneSingleFacerState.Control.getModeToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.ttl_ZoneModeStr.Value=%s", index, dummyChar);
		theZoneSingleFacerState.Control.getLocalModeToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.ttl_ZoneLocalModeStr.Value=%s", index, dummyChar);
		switch (theZoneSingleFacerState.Control.getMode())
		{
			case State::WECMODE_MANUAL:
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsSFStatus.stb_ZoneMode.Auto=false", index);
				break;
			case State::WECMODE_AUTO:
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsSFStatus.stb_ZoneMode.Auto=true", index);
				break;
			case State::WECMODE_OVERRIDE:
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsSFStatus.stb_ZoneMode.switching=true", index);
			case State::WECMODE_NOMODE:
			default:
				break;
		}
		theZoneSingleFacerState.Control.getSpeedSetpointReasonToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.ttl_SpeedSetpointReason.Value=%s", index, dummyChar);
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.tnud_SpeedSetpoint.Value=%d", index, 
			theZoneSingleFacerState.Control.getSpeedSetpoint_UVmm_m());
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.chk_SpeedSetpointStop.Checked=%s", index, 
			(theZoneSingleFacerState.Control.getSpeedSetpointStop())? L"true" : L"false");
		theZoneSingleFacerState.Control.getSpeedCommandToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.ttl_SpeedCommand.Value=%s", index, dummyChar);
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.tnud_ResumeSpeed.Value=%d", index, 
			theZoneSingleFacerState.Control.getResumeSpeed_UVmm_m());

		//Zone Monitor
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.tnud_ZoneSpeed.Value=%d", index, 
			theZoneSingleFacerState.Monitor.getSpeed_UVmm_m());
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.chk_ZoneRunning.Checked=%s", index, 
			(theZoneSingleFacerState.Monitor.isRunning())? L"true" : L"false");
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.tnud_Production.Value=%d", index, 
			theZoneSingleFacerState.Monitor.getProduction_UDmm());

		//SingleFacer Monitor
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.chk_SFRunning.Checked=%s", index, 
			(theZoneSingleFacerState.singleFacerState.CtrlData.Monitor.getIsRunning())? L"true" : L"false");
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.tnud_SFSpeed.Value=%d", index, 
			theZoneSingleFacerState.singleFacerState.CtrlData.Monitor.getSpeed_UVmm_m());
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.tlb_CartridgeID.Value=%s", index, 
			theZoneSingleFacerState.singleFacerState.CtrlData.Monitor.getCartridgeId());

		//Splicer Medium
		theZoneSingleFacerState.splicerMediumState.Control.getRequestUnitToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.ttl_SplicerMediumRequestUnit.Value=%s", index, dummyChar);
		switch (theZoneSingleFacerState.splicerMediumState.Control.getMode())
		{
			case State::WECMODE_MANUAL:
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsSFStatus.SplicerMediumAutoMan.Auto=false", index);
				break;
			case State::WECMODE_AUTO:
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsSFStatus.SplicerMediumAutoMan.Auto=true", index);
				break;
			case State::WECMODE_OVERRIDE:
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsSFStatus.SplicerMediumAutoMan.switching=true", index);
			case State::WECMODE_NOMODE:
			default:
				break;
		}
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.tnud_SplicerMediumBridgeRequestedPrefill_UDmm.Value=%d", index, 
			theZoneSingleFacerState.splicerMediumState.Control.getBridgeRequestedPrefill_UDmm());

		//Splicer Liner
		theZoneSingleFacerState.splicerLinerState.Control.getRequestUnitToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.ttl_SplicerLinerRequestUnit.Value=%s", index, dummyChar);
		switch (theZoneSingleFacerState.splicerLinerState.Control.getMode())
		{
			case State::WECMODE_MANUAL:
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsSFStatus.SplicerLinerAutoMan.Auto=false", index);
				break;
			case State::WECMODE_AUTO:
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsSFStatus.SplicerLinerAutoMan.Auto=true", index);
				break;
			case State::WECMODE_OVERRIDE:
				o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsSFStatus.SplicerLinerAutoMan.switching=true", index);
			case State::WECMODE_NOMODE:
			default:
				break;
		}
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsSFStatus.tnud_SplicerLinerBridgeRequestedPrefill_UDmm.Value=%d", index, 
			theZoneSingleFacerState.splicerLinerState.Control.getBridgeRequestedPrefill_UDmm());


		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated singleFacer%d_SettingsSFStatus page threads",index);
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SingleFacerGroup::SettingsBridgeStatus(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus._Show()",index);
	o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.lbl_Title_YN.Text=%s %d",index, C_PageManager::SMState.getZoneSingleFacerState(index).singleFacerState.getMachineName(),index);

	State::ZoneSingleFacerState theZoneSingleFacerState;
	State::ZoneDoubleBackerState theZoneDoubleBackerState;

	while(!end_SettingsBridgeStatus)
	{
		C_PageManager::SMState.getZoneSingleFacerState(theZoneSingleFacerState,index);
		C_PageManager::SMState.getZoneDoubleBackerState(theZoneDoubleBackerState);

		//ALARM
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.chk_BridgeAlarmLow.Checked=%s",index,
			String::Bool2Alpha(theZoneSingleFacerState.bridgeState.Alarms.isLow()));
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.chk_BridgeAlarmBreak.Checked=%s",index,
			String::Bool2Alpha(theZoneSingleFacerState.bridgeState.Alarms.isBreak()));
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.chk_BridgeAlarmPhotoEyeFail.Checked=%s",index,
			String::Bool2Alpha(theZoneSingleFacerState.bridgeState.Alarms.isPhotoEyeFail()));
	

		//MONITOR
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tnud_ProductionDB_UDmm.Value=%d",index,
			theZoneDoubleBackerState.Monitor.getProduction_UDmm());
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsBridgeStatus.tnud_ProductionSF_UDmm.Value=%d", index, 
			theZoneSingleFacerState.Monitor.getProduction_UDmm());
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsBridgeStatus.tnud_DB_Bridge_UDmm.Value=%d", index, 
			(theZoneDoubleBackerState.Monitor.getProduction_UDmm() -  theZoneSingleFacerState.bridgeState.Monitor.getProduction_UDmm()));
		
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tnud_Contents_UDmm.Value=%d",index,
			theZoneSingleFacerState.bridgeState.Monitor.getContents_UDmm());
		wchar_t dummyChar[MAX_PATH];
		theZoneSingleFacerState.bridgeState.Monitor.getBridgeEyeStatus().getBridgeEyeStatusToStr(dummyChar,MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tlbl_BridgeEyeStatus.Value=%s",index,dummyChar);
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.photoeyeLow.Busy=%s",index,
			String::Bool2Alpha(theZoneSingleFacerState.bridgeState.Monitor.isLowEyeOn()));
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.photoeyeHigh.Busy=%s",index,
			String::Bool2Alpha(theZoneSingleFacerState.bridgeState.Monitor.isHighEyeOn()));

		//CALIBRATION
		theZoneSingleFacerState.bridgeState.Monitor.getCalibrationStatus().getCalibrationStatusToStr(dummyChar,MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tlbl_CalibrationStatus.Value=%s",index,dummyChar);
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tnud_LastCalibrationContents_UDmm.Value=%d",index,
			theZoneSingleFacerState.bridgeState.Monitor.getLastCalibrationContents_UDmm());
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tnud_CalibrationContents_UDmm.Value=%d",index,
			theZoneSingleFacerState.bridgeState.Monitor.getCalibrationContents_UDmm());
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tnud_LastCalibrateProduction_UDmm.Value=%d",index,
			theZoneSingleFacerState.bridgeState.Monitor.getLastCalibrateProduction_UDmm());
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.chk_BridgeAlarmCalibrateFail.Checked=%s",index,
			String::Bool2Alpha(theZoneSingleFacerState.bridgeState.Alarms.isCalibrateFail()));

		//CONTROL
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tnud_RequiredContents_UDmm.Value=%d",index,
			theZoneSingleFacerState.bridgeState.Control.getRequiredContents_UDmm());
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tnud_RequiredContentsDiff_UDmm.Value=%d",index,
			theZoneSingleFacerState.bridgeState.Control.getRequiredContentsDiff_UDmm());
		theZoneSingleFacerState.bridgeState.Control.getRequestSFToStr(dummyChar,MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tlbl_RequestSF.Value=%s",index,dummyChar);
		theZoneSingleFacerState.bridgeState.Control.getRequestDBToStr(dummyChar,MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tlbl_RequestDB.Value=%s",index,dummyChar);

		//RUNNING
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.chk_ZoneSFRunning.Checked=%s",index,
			String::Bool2Alpha(theZoneSingleFacerState.Monitor.isRunning()));
		o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.chk_ZoneDBRunning.Checked=%s",index,
			String::Bool2Alpha(theZoneDoubleBackerState.Monitor.isRunning()));

		//Double Backer
		theZoneDoubleBackerState.Control.getSpeedSetpointReasonToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsBridgeStatus.ttl_DBSpeedSetpointReason.Value=%s", index, dummyChar);
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsBridgeStatus.tnud_DBSpeedSetpoint.Value=%d", index, 
			theZoneDoubleBackerState.Control.getSpeedSetpoint_UVmm_m());
		theZoneDoubleBackerState.Control.getSpeedCommandToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsBridgeStatus.ttl_DBSpeedCommand.Value=%s", index, dummyChar);

		//SingleFacer
		theZoneSingleFacerState.Control.getSpeedSetpointReasonToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsBridgeStatus.ttl_SFSpeedSetpointReason.Value=%s", index, dummyChar);
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsBridgeStatus.tnud_SFSpeedSetpoint.Value=%d", index, 
			theZoneSingleFacerState.Control.getSpeedSetpoint_UVmm_m());
		theZoneSingleFacerState.Control.getSpeedCommandToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsBridgeStatus.ttl_SFSpeedCommand.Value=%s", index, dummyChar);


		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated singleFacer%d_SettingsBridgeStatus page threads",index);
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SingleFacerGroup::ProcessControlStatus(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;
	wchar_t formName[MAX_PATH];
	swprintf(formName, L"singleFacer%d_ProcessControlStatus", index);

	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);

	State::ZoneSingleFacerState theZoneSingleFacerState;
	State::ZoneDoubleBackerState theZoneDoubleBackerState;

	while(!end_ProcessControlStatus)
	{
		//C_PageManager::SMState.getZoneSingleFacerState(theZoneSingleFacerState,index);
		//C_PageManager::SMState.getZoneDoubleBackerState(theZoneDoubleBackerState);

		////ALARM
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.chk_BridgeAlarmLow.Checked=%s",index,
		//	String::Bool2Alpha(theZoneSingleFacerState.bridgeState.Alarms.isLow()));
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.chk_BridgeAlarmBreak.Checked=%s",index,
		//	String::Bool2Alpha(theZoneSingleFacerState.bridgeState.Alarms.isBreak()));
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.chk_BridgeAlarmPhotoEyeFail.Checked=%s",index,
		//	String::Bool2Alpha(theZoneSingleFacerState.bridgeState.Alarms.isPhotoEyeFail()));
	

		////MONITOR
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tnud_ProductionDB_UDmm.Value=%d",index,
		//	theZoneDoubleBackerState.Monitor.getProduction_UDmm());
		//o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsBridgeStatus.tnud_ProductionSF_UDmm.Value=%d", index, 
		//	theZoneSingleFacerState.Monitor.getProduction_UDmm());
		//o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsBridgeStatus.tnud_DB_Bridge_UDmm.Value=%d", index, 
		//	(theZoneDoubleBackerState.Monitor.getProduction_UDmm() -  theZoneSingleFacerState.bridgeState.Monitor.getProduction_UDmm()));
		//
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tnud_Contents_UDmm.Value=%d",index,
		//	theZoneSingleFacerState.bridgeState.Monitor.getContents_UDmm());
		//wchar_t dummyChar[MAX_PATH];
		//theZoneSingleFacerState.bridgeState.Monitor.getBridgeEyeStatus().getBridgeEyeStatusToStr(dummyChar,MAX_PATH);
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tlbl_BridgeEyeStatus.Value=%s",index,dummyChar);
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.photoeyeLow.Busy=%s",index,
		//	String::Bool2Alpha(theZoneSingleFacerState.bridgeState.Monitor.isLowEyeOn()));
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.photoeyeHigh.Busy=%s",index,
		//	String::Bool2Alpha(theZoneSingleFacerState.bridgeState.Monitor.isHighEyeOn()));

		////CALIBRATION
		//theZoneSingleFacerState.bridgeState.Monitor.getCalibrationStatus().getCalibrationStatusToStr(dummyChar,MAX_PATH);
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tlbl_CalibrationStatus.Value=%s",index,dummyChar);
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tnud_LastCalibrationContents_UDmm.Value=%d",index,
		//	theZoneSingleFacerState.bridgeState.Monitor.getLastCalibrationContents_UDmm());
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tnud_CalibrationContents_UDmm.Value=%d",index,
		//	theZoneSingleFacerState.bridgeState.Monitor.getCalibrationContents_UDmm());
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tnud_LastCalibrateProduction_UDmm.Value=%d",index,
		//	theZoneSingleFacerState.bridgeState.Monitor.getLastCalibrateProduction_UDmm());
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.chk_BridgeAlarmCalibrateFail.Checked=%s",index,
		//	String::Bool2Alpha(theZoneSingleFacerState.bridgeState.Alarms.isCalibrateFail()));

		////CONTROL
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tnud_RequiredContents_UDmm.Value=%d",index,
		//	theZoneSingleFacerState.bridgeState.Control.getRequiredContents_UDmm());
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tnud_RequiredContentsDiff_UDmm.Value=%d",index,
		//	theZoneSingleFacerState.bridgeState.Control.getRequiredContentsDiff_UDmm());
		//theZoneSingleFacerState.bridgeState.Control.getRequestSFToStr(dummyChar,MAX_PATH);
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tlbl_RequestSF.Value=%s",index,dummyChar);
		//theZoneSingleFacerState.bridgeState.Control.getRequestDBToStr(dummyChar,MAX_PATH);
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.tlbl_RequestDB.Value=%s",index,dummyChar);

		////RUNNING
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.chk_ZoneSFRunning.Checked=%s",index,
		//	String::Bool2Alpha(theZoneSingleFacerState.Monitor.isRunning()));
		//o_socket.Send(Network::MSG_PAG_REF,L"singleFacer%d_SettingsBridgeStatus.chk_ZoneDBRunning.Checked=%s",index,
		//	String::Bool2Alpha(theZoneDoubleBackerState.Monitor.isRunning()));

		////Double Backer
		//theZoneDoubleBackerState.Control.getSpeedSetpointReasonToStr(dummyChar, MAX_PATH);
		//o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsBridgeStatus.ttl_DBSpeedSetpointReason.Value=%s", index, dummyChar);
		//o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsBridgeStatus.tnud_DBSpeedSetpoint.Value=%d", index, 
		//	theZoneDoubleBackerState.Control.getSpeedSetpoint_UVmm_m());
		//theZoneDoubleBackerState.Control.getSpeedCommandToStr(dummyChar, MAX_PATH);
		//o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsBridgeStatus.ttl_DBSpeedCommand.Value=%s", index, dummyChar);

		////SingleFacer
		//theZoneSingleFacerState.Control.getSpeedSetpointReasonToStr(dummyChar, MAX_PATH);
		//o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsBridgeStatus.ttl_SFSpeedSetpointReason.Value=%s", index, dummyChar);
		//o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsBridgeStatus.tnud_SFSpeedSetpoint.Value=%d", index, 
		//	theZoneSingleFacerState.Control.getSpeedSetpoint_UVmm_m());
		//theZoneSingleFacerState.Control.getSpeedCommandToStr(dummyChar, MAX_PATH);
		//o_socket.Send(Network::MSG_PAG_REF, L"singleFacer%d_SettingsBridgeStatus.ttl_SFSpeedCommand.Value=%s", index, dummyChar);


		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated %s page threads", formName);
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SingleFacerGroup::ProcessControlSettings(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;
	wchar_t formName[MAX_PATH];
	swprintf(formName, L"singleFacer%d_ProcessControlSettings", index);

	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);

	State::ZoneSingleFacerState theZoneSingleFacerState;
	State::ZoneDoubleBackerState theZoneDoubleBackerState;

	while(!end_ProcessControlSettings)
	{
		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated %s page threads", formName);

	return ERROR_SUCCESS;
}