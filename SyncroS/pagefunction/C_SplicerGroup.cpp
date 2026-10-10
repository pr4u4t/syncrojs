#include ".\C_SplicerGroup.h"

#include "..\C_PageManager.h"
#include "..\..\Include\Syncro.h"

//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

int C_SplicerGroup::k;

bool C_SplicerGroup::end_Status;
bool C_SplicerGroup::end_Console;
bool C_SplicerGroup::end_AlarmsActive;
bool C_SplicerGroup::end_AlarmsHistory;
bool C_SplicerGroup::end_SettingsMachine;
bool C_SplicerGroup::end_SettingsTask;
bool C_SplicerGroup::end_SettingsStatus;
bool C_SplicerGroup::end_ProcessControlStatus;
bool C_SplicerGroup::end_ProcessControlSettings;

C_SplicerGroup::C_SplicerGroup(void)
{
	pid_SplicerPage=NULL;
	k = 0;
	TerminateThread(false);
}

C_SplicerGroup::~C_SplicerGroup(void)
{
}

bool C_SplicerGroup::ChooseSplicerPage(String::StrType  thePageRequest, const int index)
{
	//Splicer status
	if(thePageRequest.CompareNoCase(L"Status"))
	{
		k = index;
		end_Status = false;
		pid_SplicerPage=::CreateThread(NULL,0,Status,&k,0,NULL);
		return true;
	}

	//Splicer alarmActive
	if(thePageRequest.CompareNoCase(L"AlarmActive"))
	{
		k = index;
		end_AlarmsActive = false;
		pid_SplicerPage=::CreateThread(NULL,0,AlarmActive,&k,0,NULL);
		return true;
	}
	
	//Splicer alarmHistory
	if(thePageRequest.CompareNoCase(L"AlarmHistory"))
	{
		k = index;
		end_AlarmsHistory = false;
		pid_SplicerPage=::CreateThread(NULL,0,AlarmHistory,&k,0,NULL);
		return true;
	}

	//Splicer console
	if(thePageRequest.CompareNoCase(L"Console"))
	{
		k = index;
		end_Console = false;
		pid_SplicerPage=::CreateThread(NULL,0,Console,&k,0,NULL);
		return true;
	}
	
	//Splicer settingsMachine
	if(thePageRequest.CompareNoCase(L"SettingsMachine"))
	{
		k = index;
		end_SettingsMachine = false;
		pid_SplicerPage=::CreateThread(NULL,0,SettingsMachine,&k,0,NULL);
		return true;
	}

	//Splicer settingsStatus
	if(thePageRequest.CompareNoCase(L"SettingsStatus"))
	{
		k = index;
		end_SettingsStatus = false;
		pid_SplicerPage=::CreateThread(NULL,0,SettingsStatus,&k,0,NULL);
		return true;
	}
	
	//Splicer settingsTask
	if(thePageRequest.CompareNoCase(L"SettingsTask"))
	{
		k = index;
		end_SettingsTask = false;
		pid_SplicerPage=::CreateThread(NULL,0,SettingsTask,&k,0,NULL);
		return true;
	}

	//Process Control status
	if(thePageRequest.CompareNoCase(L"ProcessControlStatus"))
	{
		k = index;
		end_ProcessControlStatus = false;
		pid_SplicerPage=::CreateThread(NULL,0,ProcessControlStatus,&k,0,NULL);
		return true;
	}

	//Process Control settings
	if(thePageRequest.CompareNoCase(L"ProcessControlSettings"))
	{
		k = index;
		end_ProcessControlSettings = false;
		pid_SplicerPage=::CreateThread(NULL,0,ProcessControlSettings,&k,0,NULL);
		return true;
	}

	return false;

}

void C_SplicerGroup::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_SplicerPage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_SplicerPage,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread SplicerPage ... %d", ::GetLastError());
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
		end_SettingsStatus = true;
		end_ProcessControlStatus = true;
		end_ProcessControlSettings = true;
	}
}

DWORD C_SplicerGroup::SuspendThread(void)
{
	if(pid_SplicerPage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_SplicerPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page Splicer Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_SplicerGroup::ResumeThread(void)
{
	if(pid_SplicerPage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_SplicerPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page Splicer Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SplicerGroup::Status(LPVOID  lpParam)
{
	int splNumber=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status._Show()",splNumber);
			
	State::SplicerState theSplicerState;
	theSplicerState.Clear();
	
	while(!end_Status)
	{
		C_PageManager::SMState.getSplicerState(theSplicerState, splNumber);
		//auto man button
		switch (theSplicerState.Control.getMode())
		{
			case State::WECMODE_MANUAL:
				o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.ToggleAutoMan.Auto=false", splNumber);
				break;
			case State::WECMODE_AUTO:
				o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.ToggleAutoMan.Auto=true", splNumber);
				break;
			case State::WECMODE_OVERRIDE:
				o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.ToggleAutoMan.switching=true", splNumber);
			case State::WECMODE_NOMODE:
			default:
				break;
		}
		
		//papers
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.tlb_NextPaperID.Value=%s",splNumber,theSplicerState.rtData.getPaperType(splNumber, State::RtData::isnext));
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.tnud_NextPaperWF.Value=%d",splNumber,theSplicerState.rtData.getPaperWeigthFactor_UWg_m(splNumber, State::RtData::isnext));
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.tlb_CurrentPaperID.Value=%s",splNumber,theSplicerState.rtData.getPaperType(splNumber, State::RtData::iscurrent));
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.tnud_CurrentPaperWF.Value=%d",splNumber,theSplicerState.rtData.getPaperWeigthFactor_UWg_m(splNumber, State::RtData::iscurrent));

		//next splice
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.tnud_MeterLeftNextSplice.Value=%d",splNumber,theSplicerState.Monitor.getLengthUntilSplice_UDmm());
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.tnud_ProductionLeftNextSplice.Value=%d",splNumber,theSplicerState.Monitor.getProductionUntilSplice_UDmm());
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status._setTimeLeftNextSplice(I[%d])",splNumber,theSplicerState.Monitor.getTimeToSplice_UTs());
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.chk_NextSpliceEndOfRoll.Checked=%s",splNumber, String::Bool2Alpha(theSplicerState.Monitor.getButt_UDmm() == 0));
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.tnud_Speed.Value=%d",splNumber,theSplicerState.rtData.getSpliceSpeed_UVmm_m(splNumber, State::RtData::iscurrent));
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.tnud_CoreDiameter.Value=%d",splNumber,theSplicerState.rtData.getCoreDiameter_UDdmm(splNumber, State::RtData::iscurrent));

		//program change
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.tnud_ProductionLeftProgramChange.Value=%d",splNumber,theSplicerState.Monitor.getProductionRemaininProgramEnd_UDmm());
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.tnud_ProductionLeftRollEnd.Value=%d",splNumber,theSplicerState.Monitor.getProductionRemaining_UDmm());

		//paper speed
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.tnud_PaperSpeed.Value=%d",splNumber,theSplicerState.Monitor.getSpeed_UVmm_m());

		//rdy splice, roll running, rev roll
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status._setRollsStatus(b[%s], b[%s], b[%s])", splNumber, 
			String::Bool2Alpha(theSplicerState.CtrlData.getReadyToSplice()),
			String::Bool2Alpha(theSplicerState.Monitor.getRunningRollStand_A()),
			(theSplicerState.Monitor.getSpeed_UVmm_m() == 0)?L"false":L"true"
			);
		
		//PAPER TENSION
		if ( theSplicerState.CtrlData.paperTensionState.getPaperTension_UFkgxm_s2().getEnable() )
		{
			o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.pt_paperTension.Visible=true",splNumber);
			o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.pt_paperTension.Position=%d",splNumber,
				(int)theSplicerState.CtrlData.paperTensionState.getPaperTension_UFkgxm_s2().getValue());
	    o_socket.Send(Network::MSG_PAG_REF, L"splicer%d_status.pt_paperTension.InLocal=%s",splNumber,
		    String::Bool2Alpha(!theSplicerState.CtrlData.paperTensionState.getIsPaperTensionRemote().getValue()));
	    o_socket.Send(Network::MSG_PAG_REF, L"splicer%d_status.pt_paperTension.Auto=%s",splNumber,
		    String::Bool2Alpha(theSplicerState.CtrlData.paperTensionState.getIsPaperTensionAuto().getValue()));

		}

		//ROLL MANAGEMENT
		//agrassi_FI03-03-08
		//bool isButton = false;
		//if(!theSplicerState.Rolls.Current.isEmpty())
		//{
		//	if(C_PageManager::SMState.getZoneStateFromSplicer(splNumber).rtData.getProgramId( State::RtData::isnext ) == 0)
		//		isButton = true;
		//}	
		//o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status.RollChange.IsButton=%s", splNumber, String::Bool2Alpha(isButton));
		
		for(int rollQueue = 1; rollQueue <= 4; rollQueue++)
		{
			PastRollsData pastRoll = theSplicerState.Rolls.Current;

			switch(rollQueue)
			{
			case 1:
				pastRoll = theSplicerState.Rolls.Current;
				break;
			case 2:
				pastRoll = theSplicerState.Rolls.Next;
				break;
			case 3:
				pastRoll = theSplicerState.Rolls.NextPlusOne;
				break;
			case 4:
				pastRoll = theSplicerState.Rolls.NextPlusTwo;
				break;
			default:
				break;
			}

			o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_status._UpdateRollData(I[%d], I[%d], s[%s%s%s%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%s%s%s%s%s])", 
				splNumber, 
				splNumber, 
				rollQueue, 
				pastRoll.RSMgetRollId(), Network::SEP,
				pastRoll.getPaperType(), Network::SEP,
				pastRoll.getWidth_UDdmm(), Network::SEP,
				pastRoll.RSMgetSuppliedDiameter_UDdmm(), Network::SEP,
				pastRoll.RSMgetDiameter_UDdmm(), Network::SEP,
				pastRoll.getCoreDiameter_UDdmm(), Network::SEP,
				pastRoll.RSMgetSuppliedLength_UDmm(), Network::SEP,
				pastRoll.STATISTICgetRemaining_UDmm(), Network::SEP,
				pastRoll.getWeightFactor_UWg_m(), Network::SEP,
				pastRoll.getThickness_UDdmmm(), Network::SEP,
				pastRoll.RSMgetDescription(), Network::SEP,
				String::Bool2Alpha(pastRoll.RSMgetRollDataEntered()), Network::SEP,
				pastRoll.RSMgetDataToPrint()
				);
		}

		Sleep(500);
	}

	logger.Fine(__FUNCTION__, L"Terminated splicer%d_status page threads",splNumber);

	return ERROR_SUCCESS;
}


DWORD WINAPI C_SplicerGroup::Console(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_Console._Show()",index);
			
	while(!end_Console)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated splicer%d_Console page threads",index);

	return ERROR_SUCCESS;
}


DWORD WINAPI C_SplicerGroup::AlarmActive(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_AlarmActive._Show()",index);

	while(!end_AlarmsActive)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated splicer%d_AlarmActive page threads",index);
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SplicerGroup::AlarmHistory(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_AlarmHistory._Show()",index);

	while(!end_AlarmsHistory)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated splicer%d_AlarmHistory page threads",index);
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SplicerGroup::SettingsMachine(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_settingsMachine._Show()",index);

	while(!end_SettingsMachine)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated splicer%d_settingsMachine page threads",index);
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SplicerGroup::SettingsTask(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_SettingsTask._Show()",index);

	while(!end_SettingsTask)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated splicer%d_SettingsTask page threads",index);
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SplicerGroup::SettingsStatus(LPVOID  lpParam)
{
	int splNumber=*(DWORD*)lpParam ;

	o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_SettingsStatus._Show()", splNumber);
	o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_SettingsStatus.lbl_Title_YN.Text=%s %d", splNumber, C_PageManager::SMState.getSplicerState(splNumber).getMachineName(), splNumber);

	State::SplicerState theSplicerState;
	wchar_t dummyChar[MAX_PATH];

	while(!end_SettingsStatus)
	{
		C_PageManager::SMState.getSplicerState(theSplicerState, splNumber);

		//Splicer Control
		theSplicerState.Control.getModeToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"splicer%d_SettingsStatus.ttl_SplicerModeStr.Value=%s", splNumber, dummyChar);
		theSplicerState.Control.getLocalModeToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"splicer%d_SettingsStatus.ttl_SplicerLocalModeStr.Value=%s", splNumber, dummyChar);
		switch (theSplicerState.Control.getMode())
		{
			case State::WECMODE_MANUAL:
				o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_SettingsStatus.ToggleAutoMan.Auto=false", splNumber);
				break;
			case State::WECMODE_AUTO:
				o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_SettingsStatus.ToggleAutoMan.Auto=true", splNumber);
				break;
			case State::WECMODE_OVERRIDE:
				o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_SettingsStatus.ToggleAutoMan.switching=true", splNumber);
			case State::WECMODE_NOMODE:
			default:
				break;
		}

		theSplicerState.Control.getRequestUnitToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"splicer%d_SettingsStatus.ttl_RequestUnit.Value=%s", splNumber, dummyChar);
		o_socket.Send(Network::MSG_PAG_REF, L"splicer%d_SettingsStatus.tnud_BridgeRequestedPrefill_UDmm.Value=%d", splNumber, 
			theSplicerState.Control.getBridgeRequestedPrefill_UDmm());

		//Splicer Monitor
		o_socket.Send(Network::MSG_PAG_REF, L"splicer%d_SettingsStatus.tnud_Speed.Value=%d", splNumber, 
			theSplicerState.Monitor.getSpeed_UVmm_m());
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_SettingsStatus.chk_ReadyToSplice.Checked=%s",splNumber,
			String::Bool2Alpha(theSplicerState.CtrlData.getReadyToSplice()));
		o_socket.Send(Network::MSG_PAG_REF, L"splicer%d_SettingsStatus.tnud_LastCycle_UTs.Value=%d", splNumber,
			(int)C_PageManager::SMState.getSplicerState(splNumber).Monitor.getLastCycle_UTms());
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_SettingsStatus.ttl_RollRunning.Value=%s",splNumber,
			(theSplicerState.Monitor.getRunningRollStand_A() ? L"A" : L"B") );

		//Previous Roll
		o_socket.Send(Network::MSG_PAG_REF, L"splicer%d_SettingsStatus.ttl_PaperTypePrevious.Value=%s (%d)", splNumber, 
			theSplicerState.Rolls.Previous.getPaperType(), theSplicerState.Rolls.Previous.getWeightFactor_UWg_m());
		o_socket.Send(Network::MSG_PAG_REF, L"splicer%d_SettingsStatus.ttl_PreviousDiameter.Value=%d", splNumber, 
			theSplicerState.Rolls.Previous.STATISTICgetRemainingDiameter_UDdmm());
		o_socket.Send(Network::MSG_PAG_REF, L"splicer%d_SettingsStatus.ttl_PreviousThickness.Value=%d", splNumber, 
			theSplicerState.Rolls.Previous.getThickness_UDdmmm());
		o_socket.Send(Network::MSG_PAG_REF, L"splicer%d_SettingsStatus.ttl_PreviousRemaining.Value=%d", splNumber, 
			theSplicerState.Rolls.Previous.STATISTICgetRemaining_UDmm());

		//Current Roll
		o_socket.Send(Network::MSG_PAG_REF, L"splicer%d_SettingsStatus.ttl_PaperTypeCurrent.Value=%s (%d)", splNumber, 
			theSplicerState.Rolls.Current.getPaperType(), theSplicerState.Rolls.Current.getWeightFactor_UWg_m());
		o_socket.Send(Network::MSG_PAG_REF, L"splicer%d_SettingsStatus.ttl_CurrentDiameter.Value=%d", splNumber, 
			theSplicerState.Rolls.Current.STATISTICgetRemainingDiameter_UDdmm());
		o_socket.Send(Network::MSG_PAG_REF, L"splicer%d_SettingsStatus.ttl_CurrentThickness.Value=%d", splNumber, 
			theSplicerState.Rolls.Current.getThickness_UDdmmm());
		o_socket.Send(Network::MSG_PAG_REF, L"splicer%d_SettingsStatus.ttl_CurrentRemaining.Value=%d", splNumber, 
			theSplicerState.Rolls.Current.STATISTICgetRemaining_UDmm());

		//Splicer Alarms
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_SettingsStatus.chk_CommsAlarm.Checked=%s",splNumber,
			String::Bool2Alpha(! theSplicerState.CtrlData.Alarms.getCommsAlarm()) );
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_SettingsStatus.chk_PaperBreak.Checked=%s",splNumber,
			String::Bool2Alpha(! theSplicerState.CtrlData.Alarms.getPaperBreak()) );
		o_socket.Send(Network::MSG_PAG_REF,L"splicer%d_SettingsStatus.chk_NotReadyAtEarlyWarning.Checked=%s",splNumber,
			String::Bool2Alpha(! theSplicerState.Monitor.getNotReadyAtEarlyWarning()) );

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated splicer%d_SettingsStatus page threads", splNumber);
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SplicerGroup::ProcessControlStatus(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;

	wchar_t formName[MAX_PATH];
	swprintf(formName, L"splicer%d_ProcessControlStatus", index);

	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);

	State::SplicerState theSplicerState;

	while(!end_ProcessControlStatus)
	{

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated %s page threads", formName);
	
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SplicerGroup::ProcessControlSettings(LPVOID  lpParam)
{
	int index=*(DWORD*)lpParam ;
	wchar_t formName[MAX_PATH];
	swprintf(formName, L"splicer%d_ProcessControlSettings", index);

	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);

	State::SplicerState theSplicerState;

	while(!end_ProcessControlSettings)
	{
		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated %s page threads", formName);

	return ERROR_SUCCESS;
}