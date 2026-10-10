#include "C_StackerGroup.h"

#include "..\C_PageManager.h"

//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

int C_StackerGroup::k;

bool C_StackerGroup::end_Status01;
bool C_StackerGroup::end_Status02;
bool C_StackerGroup::end_Status03;
bool C_StackerGroup::end_Status04;
bool C_StackerGroup::end_Console;
bool C_StackerGroup::end_AlarmsActive;
bool C_StackerGroup::end_AlarmsHistory;
bool C_StackerGroup::end_SettingsMachine;
bool C_StackerGroup::end_SettingsTask;
bool C_StackerGroup::end_SettingsStatus;

C_StackerGroup::C_StackerGroup(void)
{
	pid_StackerPage=NULL;
	k = 0;
	TerminateThread(false);
}

C_StackerGroup::~C_StackerGroup(void)
{
}

bool C_StackerGroup::ChooseStackerPage(String::StrType  thePageRequest, const int index)
{
//stacker status
	if(thePageRequest.CompareNoCase(L"Status01"))
	{
		k = index;
		end_Status01 = false;
		pid_StackerPage=::CreateThread(NULL,0,Status01,&k,0,NULL);
		return true;
	}
  if(thePageRequest.CompareNoCase(L"Status02"))
	{
		k = index;
		end_Status02 = false;
		pid_StackerPage=::CreateThread(NULL,0,Status02,&k,0,NULL);
		return true;
	}
  if(thePageRequest.CompareNoCase(L"Status03"))
	{
		k = index;
		end_Status03 = false;
		pid_StackerPage=::CreateThread(NULL,0,Status03,&k,0,NULL);
		return true;
	}
  if(thePageRequest.CompareNoCase(L"Status04"))
	{
		k = index;
		end_Status04 = false;
		pid_StackerPage=::CreateThread(NULL,0,Status04,&k,0,NULL);
		return true;
	}

	//stacker alarmActive
	if(thePageRequest.CompareNoCase(L"AlarmActive"))
	{
		k = index;
		end_AlarmsActive = false;
		pid_StackerPage=::CreateThread(NULL,0,AlarmActive,&k,0,NULL);
		return true;
	}

	//stacker alarmHistory
	if(thePageRequest.CompareNoCase(L"AlarmHistory"))
	{
		k = index;
		end_AlarmsHistory = false;
		pid_StackerPage=::CreateThread(NULL,0,AlarmHistory,&k,0,NULL);
		return true;
	}

	//stacker console
	if(thePageRequest.CompareNoCase(L"Console"))
	{
		k = index;
		end_Console = false;
		pid_StackerPage=::CreateThread(NULL,0,Console,&k,0,NULL);
		return true;
	}
	
	//stacker settingsMachine
	if(thePageRequest.CompareNoCase(L"SettingsMachine"))
	{
		k = index;
		end_SettingsMachine = false;
		pid_StackerPage=::CreateThread(NULL,0,SettingsMachine,&k,0,NULL);
		return true;
	}

	//stacker settingsStatusMachine
	if(thePageRequest.CompareNoCase(L"SettingsStatus"))
	{
		k = index;
		end_SettingsStatus = false;
		pid_StackerPage=::CreateThread(NULL,0,SettingsStatus,&k,0,NULL);
		return true;
	}
	
	//stacker settingsTask
	if(thePageRequest.CompareNoCase(L"SettingsTask"))
	{
		k = index;
		end_SettingsTask = false;
		pid_StackerPage=::CreateThread(NULL,0,SettingsTask,&k,0,NULL);
		return true;
	}

	return false;
}

void C_StackerGroup::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_StackerPage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_StackerPage,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread StackerPage ... %d", ::GetLastError());
		}
	}
	else
	{
		end_Status01 = true;
    end_Status02 = true;
    end_Status03 = true;
    end_Status04 = true;
		end_Console = true;
		end_AlarmsActive = true;
		end_AlarmsHistory = true;
		end_SettingsMachine = true;
		end_SettingsTask = true;
		end_SettingsStatus = true;
	}
}

DWORD C_StackerGroup::SuspendThread(void)
{
	if(pid_StackerPage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_StackerPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page Stacker Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_StackerGroup::ResumeThread(void)
{
	if(pid_StackerPage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_StackerPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page Stacker Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}

DWORD WINAPI C_StackerGroup::AlarmActive(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;
	
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_alarmActive._Show()",index);
	
	State::StackerState stackerState = C_PageManager::SMState.getStackerState(index);
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_alarmActive._setFrmMachineName(s[%s])",index,stackerState.getMachineName());
	
	while((!end_AlarmsActive) || (k!=index))
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated stacker%d_alarmActive page threads",index);
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_StackerGroup::AlarmHistory(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;
	
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_alarmHistory._Show()",index);
	State::StackerState stackerState = C_PageManager::SMState.getStackerState(index);
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_alarmHistory._setFrmMachineName(s[%s])",index,stackerState.getMachineName());

	while((!end_AlarmsHistory) || (k!=index))
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated stacker%d_alarmHistory page threads",index);
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_StackerGroup::Console(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;
	
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_console._Show()",index);
	State::StackerState stackerState = C_PageManager::SMState.getStackerState(index);
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_console._setFrmMachineName(s[%s])",index,stackerState.getMachineName());

	bool communicationError = false;

	while((!end_Console) || (k!=index))
	{
		if(communicationError!=C_PageManager::SMState.getStackerState(index).alarms.getIsAlarmPLCCommError())
		{
			communicationError = C_PageManager::SMState.getStackerState(index).alarms.getIsAlarmPLCCommError();
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_console._CommunicationError(b[%s])", index,
        String::Bool2Alpha(communicationError));
		}

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated stacker%d_console page threads",index);
	
	return ERROR_SUCCESS;
}
DWORD WINAPI C_StackerGroup::Status01(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;
	int setupCurrentKeyStk1 = 99;	// 99 = Force send at first cycle
	int setupCurrentKeyStk2 = 99;
	int setupCurrentKeyStk3 = 99;
	int setupNextKeyStk1 = 99;
	int setupNextKeyStk2 = 99;
	int setupNextKeyStk3 = 99;

	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01._Show()",index);
	State::StackerState stackerState = C_PageManager::SMState.getStackerState(index);
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01._setFrmMachineName(s[%s])",index,stackerState.getMachineName());

	int stateId = 0;

	if(stackerState.monitor.getBackStopPosition_UDmm().getEnable() || 
		stackerState.monitor.getPlatformPosition_UDmm().getEnable())
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.ttl_MachineStatus.Visible=true",index);

	if(stackerState.alarms.getIsDetectedAScannerIntrusion().getEnable())
	{
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.ttl_IntrusionDetection.Visible=true",index);
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.btn_IntrusionDetection.Visible=true",index);
	}

	if(stackerState.monitor.getbrushDown1().getEnable() ||
		stackerState.monitor.getbrushDown2().getEnable() ||
		stackerState.monitor.getbrushDown3().getEnable() ||
		stackerState.monitor.getbrushDown4().getEnable() ||
		stackerState.monitor.getbrushDown5().getEnable())
	{
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.ttl_BrushStatus.Visible=true",index);

		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.btn_brush1.Visible=true",index);
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.btn_brush2.Visible=true",index);
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.btn_brush3.Visible=true",index);
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.btn_brush4.Visible=true",index);
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.btn_brush5.Visible=true",index);
	}

	//RFAZZI_FI28/06/2008_1.Added send order Push Button on machine status page
	bool communicationError = false;

	while((!end_Status01) || (k!=index))
	{
		stateId = C_PageManager::SMState.getStackerState(index,stackerState);

		if (stackerState.orders.getIDCurrent() != setupCurrentKeyStk1)
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01._setCurrentSetup(i[%d],i[%d])",index,stackerState.orders.getIDCurrent().getValue(),stackerState.orders.getModIDCurrent().getValue());

		if (stackerState.orders.getIDNext() != setupNextKeyStk1)
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01._setNextSetup(i[%d],i[%d])",index,stackerState.orders.getIDNext().getValue(),stackerState.orders.getModIDNext().getValue());

		if(stackerState.monitor.aoc.getNextAOCCancelled().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.chk_NextAOCCancelled.Checked=%s",index,String::Bool2Alpha(!stackerState.monitor.aoc.getNextAOCCancelled().getValue()));

		if(stackerState.monitor.getBackStopPosition_UDmm().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.tnud_BackstopPosition.Value=%d",index,stackerState.monitor.getBackStopPosition_UDmm().getValue());
		
		if(stackerState.monitor.getPlatformPosition_UDmm().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.tnud_PlatformPosition.Value=%d",index,stackerState.monitor.getPlatformPosition_UDmm().getValue());
		
		if(stackerState.alarms.getIsDetectedAScannerIntrusion().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.btn_IntrusionDetection.Alarmed=%s",index,String::Bool2Alpha(stackerState.alarms.getIsDetectedAScannerIntrusion().getValue()));

		if(stackerState.monitor.getBrushGroup1Pressure_UPmb().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.tnud_brushGroup1Pressure.Value=%d",index,stackerState.monitor.getBrushGroup1Pressure_UPmb().getValue());

		if(stackerState.monitor.getBrushGroup2Pressure_UPmb().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.tnud_brushGroup2Pressure.Value=%d",index,stackerState.monitor.getBrushGroup2Pressure_UPmb().getValue());

		if(stackerState.monitor.getbrushDown1().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.btn_brush1.Auto=%s",index,String::Bool2Alpha(stackerState.monitor.getbrushDown1().getValue()));

		if(stackerState.monitor.getbrushDown2().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.btn_brush2.Auto=%s",index,String::Bool2Alpha(stackerState.monitor.getbrushDown2().getValue()));

		if(stackerState.monitor.getbrushDown3().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.btn_brush3.Auto=%s",index,String::Bool2Alpha(stackerState.monitor.getbrushDown3().getValue()));

		if(stackerState.monitor.getbrushDown4().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.btn_brush4.Auto=%s",index,String::Bool2Alpha(stackerState.monitor.getbrushDown4().getValue()));

		if(stackerState.monitor.getbrushDown5().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01.btn_brush5.Auto=%s",index,String::Bool2Alpha(stackerState.monitor.getbrushDown5().getValue()));

		//RFAZZI_FI28/06/2008_1.Added send order Push Button on machine status page
		if(communicationError!=C_PageManager::SMState.getStackerState(index).alarms.getIsAlarmPLCCommError())
		{
			communicationError = C_PageManager::SMState.getStackerState(index).alarms.getIsAlarmPLCCommError();
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status01._CommunicationError(b[%s])", index,
        String::Bool2Alpha(communicationError));
		}

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated stacker%d_status01 page threads",index);

	return ERROR_SUCCESS;
}

DWORD WINAPI C_StackerGroup::Status02(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;
			
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status02._Show()",index);
	State::StackerState stackerState = C_PageManager::SMState.getStackerState(index);
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status02._setFrmMachineName(s[%s])",index,stackerState.getMachineName());
	
	while((!end_Status02) || (k!=index))
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated stacker%d_Status02 page threads",index);

	return ERROR_SUCCESS;
}

DWORD WINAPI C_StackerGroup::Status03(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;
			
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status03._Show()",index);
	State::StackerState stackerState = C_PageManager::SMState.getStackerState(index);
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status03._setFrmMachineName(s[%s])",index,stackerState.getMachineName());
	
	while((!end_Status03) || (k!=index))
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated stacker%d_Status03 page threads",index);

	return ERROR_SUCCESS;
}

DWORD WINAPI C_StackerGroup::Status04(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;
			
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status04._Show()",index);
	State::StackerState stackerState = C_PageManager::SMState.getStackerState(index);
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_status04._setFrmMachineName(s[%s])",index,stackerState.getMachineName());
	
	while((!end_Status04) || (k!=index))
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated stacker%d_Status04 page threads",index);

	return ERROR_SUCCESS;
}

DWORD WINAPI C_StackerGroup::SettingsStatus(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;
	
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus._Show()",index);

	//CM14-03
	//State::StackerState stackerState = C_PageManager::SMState.getStackerState(index);
	State::StackerState stackerState = C_PageManager::SMState.getStackerState(index);
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus._setFrmMachineName(s[%s])",index,stackerState.getMachineName());

	State::StackerDataToPrint stackerDataToPrint;
	//CM14-03
	//C_PageManager::SMState.getStackerDataToPrint(index,stackerDataToPrint);

	while((!end_SettingsStatus) || (k!=index))
	{
		C_PageManager::SMState.getStackerDataToPrint(index,stackerDataToPrint);
		stackerState = C_PageManager::SMState.getStackerState(index);

		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.tnud_dischargingSetupID.Value=%d",index,stackerDataToPrint.getDischargingSetupID().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.tnud_dischargingSetupModID.Value=%d",index,stackerDataToPrint.getDischargingSetupModID().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.tnud_CutLenght.Value=%d",index,stackerDataToPrint.getCutLenght_UDmm().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.tnud_SheetWidth.Value=%d",index,stackerDataToPrint.getSheetWidth_UDmm().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.tnud_ActualCutsinPlatform.Value=%d",index,stackerDataToPrint.getActualCutsinPlatform().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.tnud_Outs.Value=%d",index,stackerDataToPrint.getOuts().getValue());

		if(stackerDataToPrint.getProductionTypeBundle().getValue())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.ttb_ProductionType.Value=Bundle",index);
		if(stackerDataToPrint.getProductionTypeContinuous().getValue())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.ttb_ProductionType.Value=Continuous",index);
		if(stackerDataToPrint.getProductionTypeStack().getValue())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.ttb_ProductionType.Value=Stack",index);

		if(stackerDataToPrint.isDischargingTypeRight().getValue())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.ttb_DischargingType.Value=Right",index);
		if(stackerDataToPrint.isDischargingTypeLeft().getValue())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.ttb_DischargingType.Value=Left",index);
		if(stackerDataToPrint.isDischargingTypeFront().getValue())
			o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.ttb_DischargingType.Value=Front",index);

		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.chk_NormalAOC.Checked=%s",index,String::Bool2Alpha(stackerDataToPrint.getIsNormalAOC().getValue()));

		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.tnud_GenericInfo1.Value=%d",index,stackerDataToPrint.getGenericInfo1().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.tnud_GenericInfo2.Value=%d",index,stackerDataToPrint.getGenericInfo2().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.tnud_GenericInfo3.Value=%d",index,stackerDataToPrint.getGenericInfo3().getValue());

		//CM14-03
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.tnud_nextdischargingSetupID.Value=%d",index,stackerDataToPrint.getNextDischargingSetupID().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.tnud_nextdischargingSetupModID.Value=%d",index,stackerDataToPrint.getNextDischargingSetupModID().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.tnud_DischargeCounter.Value=%d",index,stackerDataToPrint.getDischargeCounter().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.tnud_AOCDischargeCounter.Value=%d",index,stackerDataToPrint.getLastDischargeCounter().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.chk_Discharge.Checked=%s",index,String::Bool2Alpha(stackerState.getDischargeCycleInProgress().getValue()));
		o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsStatus.chk_DischargeCompleted.Checked=%s",index,String::Bool2Alpha(stackerState.getDischargeCycleCompleted().getValue()));

		//CM14-03
		//Sleep(2000);
		Sleep(500);
	}

	logger.Fine(__FUNCTION__, L"Terminated stacker%d_SettingsStatus page threads",index);

	return ERROR_SUCCESS;
}

DWORD WINAPI C_StackerGroup::SettingsMachine(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsMachine._Show()",index);
	State::StackerState stackerState = C_PageManager::SMState.getStackerState(index);
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsMachine._setFrmMachineName(s[%s])",index,stackerState.getMachineName());
	
	while((!end_SettingsMachine) || (k!=index))
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated stacker%d_SettingsMachine page threads",index);
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_StackerGroup::SettingsTask(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;
			
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsTask._Show()",index);
	State::StackerState stackerState = C_PageManager::SMState.getStackerState(index);
	o_socket.Send(Network::MSG_PAG_REF,L"stacker%d_SettingsTask._setFrmMachineName(s[%s])",index,stackerState.getMachineName());
	
	while((!end_SettingsTask) || (k!=index))
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated stacker%d_SettingsTask page threads",index);

	return ERROR_SUCCESS;
}