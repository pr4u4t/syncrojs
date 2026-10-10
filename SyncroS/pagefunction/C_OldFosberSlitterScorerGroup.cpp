#include ".\C_OldFosberSlitterScorerGroup.h"

//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

//Page Management
#include "..\C_PageManager.h"

bool C_OldFosberSlitterScorerGroup::end_Status;
bool C_OldFosberSlitterScorerGroup::end_Console;
bool C_OldFosberSlitterScorerGroup::end_AlarmsActive;
bool C_OldFosberSlitterScorerGroup::end_AlarmsHistory;
bool C_OldFosberSlitterScorerGroup::end_SettingsMachine;
bool C_OldFosberSlitterScorerGroup::end_SettingsTask;

C_OldFosberSlitterScorerGroup::C_OldFosberSlitterScorerGroup(void)
{
	pid_OldFosberSlitterScorerPage=NULL;
	TerminateThread(false);
}

C_OldFosberSlitterScorerGroup::~C_OldFosberSlitterScorerGroup(void)
{
}

bool C_OldFosberSlitterScorerGroup::ChooseOldFosberSlitterScorerPage(String::StrType  thePageRequest)
{
	//Slitter scorer status
	if(thePageRequest.CompareNoCase(L"Status"))
	{
		end_Status=false;
		pid_OldFosberSlitterScorerPage=::CreateThread(NULL,0,Status,NULL,0,NULL);
		return true;
	}
	//Slitter scorer console
	if(thePageRequest.CompareNoCase(L"Console"))
	{
		end_Console=false;
		pid_OldFosberSlitterScorerPage=::CreateThread(NULL,0,Console,NULL,0,NULL);
		return true;
	}
	//Slitter scorer alarm
	if(thePageRequest.CompareNoCase(L"AlarmActive"))
	{
		end_AlarmsActive=false;
		pid_OldFosberSlitterScorerPage=::CreateThread(NULL,0,AlarmActive,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"AlarmHistory"))
	{
		end_AlarmsHistory=false;
		pid_OldFosberSlitterScorerPage=::CreateThread(NULL,0,AlarmHistory,NULL,0,NULL);
		return true;
	}
	//Slitter scorer settings
	if(thePageRequest.CompareNoCase(L"SettingsMachine"))
	{
		end_SettingsMachine=false;
		pid_OldFosberSlitterScorerPage=::CreateThread(NULL,0,SettingsMachine,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"SettingsTask"))
	{
		end_SettingsTask=false;
		pid_OldFosberSlitterScorerPage=::CreateThread(NULL,0,SettingsTask,NULL,0,NULL);
		return true;
	}

	return false;
}

void C_OldFosberSlitterScorerGroup::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_OldFosberSlitterScorerPage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_OldFosberSlitterScorerPage,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread SlitterScorerPage ... %d", ::GetLastError());
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

DWORD C_OldFosberSlitterScorerGroup::SuspendThread(void)
{
	if(pid_OldFosberSlitterScorerPage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_OldFosberSlitterScorerPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page Slitter Scorer Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_OldFosberSlitterScorerGroup::ResumeThread(void)
{
	if(pid_OldFosberSlitterScorerPage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_OldFosberSlitterScorerPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page Slitter Scorer Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}

DWORD WINAPI C_OldFosberSlitterScorerGroup::Status(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"oldFosberSlitterScorer_status._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"oldFosberSlitterScorer_status.lbl_Title_YN.Text=%s",C_PageManager::SMState.getSlitterScorerState().getMachineName());

	State::SlitterScorerState theSlitterScorerState;

	bool communicationError = false;

	while(!end_Status)
	{
		C_PageManager::SMState.getSlitterScorerState(theSlitterScorerState);

		////manual auto
		//if(theSlitterScorerState.control.getIsInManual().getEnable())	
		//	o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.stb_AutoMan.Auto=%s",(!theSlitterScorerState.control.getIsInManual().getValue()) ? L"True" : L"False");

		////maintenance
		//if(theSlitterScorerState.control.getIsInMaintenance().getEnable())	
		//	o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.pct_Maintenance.Visible=%s",(theSlitterScorerState.control.getIsInMaintenance().getValue()) ? L"True" : L"False");

		////Lubrification
		//if(theSlitterScorerState.monitor.getIsLubrificationInProgess().getEnable())	
		//	o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.pct_Lubrification.Visible=%s",(theSlitterScorerState.monitor.getIsLubrificationInProgess().getValue()) ? L"True" : L"False");

		////Grinding
		//if(theSlitterScorerState.monitor.getIsGrindingInProgess().getEnable())	
		//	o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.pct_Grinding.Visible=%s",(theSlitterScorerState.monitor.getIsGrindingInProgess().getValue()) ? L"True" : L"False");


		////ScoreGap shaft 1
		//if(theSlitterScorerState.monitor.getScoreGap1().getEnable())	
		//	o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.nud_ScoreGap1.Value=%d",theSlitterScorerState.monitor.getScoreGap1().getValue());

		////ScoreGap shaft 2
		//if(theSlitterScorerState.monitor.getScoreGap2().getEnable())	
		//	o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.nud_ScoreGap2.Value=%d",theSlitterScorerState.monitor.getScoreGap2().getValue());

		////ScoreGap shaft Aux
		//if(theSlitterScorerState.monitor.getScoreGap3().getEnable())	
		//	o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.nud_ScoreGapAux.Value=%d",theSlitterScorerState.monitor.getScoreGap3().getValue());

		//int score = theSlitterScorerState.monitor.getIsCurrentOnScore1().getValue() ? 1 : 2;
		//int slit = theSlitterScorerState.monitor.getIsCurrentOnSlit1().getValue() ? 1 : 2;
	
		//if(theSlitterScorerState.orders.getIDCurrent().getEnable())
		//{
		//	if(theSlitterScorerState.orders.getIDCurrent().getValue() != 0)
		//	{
		//		o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._SetCurrentOrderStatus(i[%d],i[%d],i[%d],i[%d],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s])",
		//						theSlitterScorerState.orders.getIDCurrent().getValue(),
		//						theSlitterScorerState.orders.getModIDCurrent().getValue(),
		//						score,
		//						slit,
		//						String::Bool2Alpha(theSlitterScorerState.orders.getIsCurrentValidAUX().getValue()),
		//						String::Bool2Alpha(theSlitterScorerState.orders.getIsCurrentValidTandem().getValue()),
		//						String::Bool2Alpha(theSlitterScorerState.orders.getNoGapPossibleOrder1().getValue()),
		//						String::Bool2Alpha(theSlitterScorerState.orders.getIsCurrentValid().getValue()),
		//						String::Bool2Alpha(theSlitterScorerState.monitor.getIsPositioningCurrent().getValue()),
		//						String::Bool2Alpha(theSlitterScorerState.monitor.getIsCurrentPositioned().getValue()),
		//						String::Bool2Alpha(theSlitterScorerState.monitor.getIsCurrentWorking().getValue()));
		//	}
		//	else
		//	{
		//		o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._ClearCurrentOrderStatus()");
		//	}
		//}

		////Uso il corrente per calcolare il successivo
		//score = theSlitterScorerState.monitor.getIsCurrentOnScore1().getValue() ? 2 : 1;
		//slit = theSlitterScorerState.monitor.getIsCurrentOnSlit1().getValue() ? 2 : 1;

		//if(theSlitterScorerState.orders.getIDNext().getEnable())
		//{
		//	if(theSlitterScorerState.orders.getIDCurrent().getValue() != 0)
		//	{
		//		o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._SetNextOrderStatus(i[%d],i[%d],i[%d],i[%d],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s])",
		//						theSlitterScorerState.orders.getIDNext().getValue(),
		//						theSlitterScorerState.orders.getModIDNext().getValue(),
		//						score,
		//						slit,
		//						String::Bool2Alpha(theSlitterScorerState.orders.getIsNextValidAUX().getValue()),
		//						String::Bool2Alpha(theSlitterScorerState.orders.getIsNextValidTandem().getValue()),
		//						String::Bool2Alpha(theSlitterScorerState.orders.getNoGapPossibleOrder2().getValue()),
		//						String::Bool2Alpha(theSlitterScorerState.orders.getIsNextValid().getValue()),
		//						String::Bool2Alpha(theSlitterScorerState.monitor.getIsPositioningNext().getValue()),
		//						String::Bool2Alpha(theSlitterScorerState.monitor.getIsNextPositioned().getValue()),
		//						String::Bool2Alpha(theSlitterScorerState.monitor.getIsNextWorking().getValue()));
		//	}
		//	else
		//	{
		//		o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._ClearNextOrderStatus()");
		//	}
		//}

		////Slit shaft 1 status
		//o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._SetSlitShaft1Status(s[%s])",theSlitterScorerState.monitor.getMaskSlitShaft1());

		////Slit shaft 2 status
		//o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._SetSlitShaft2Status(s[%s])",theSlitterScorerState.monitor.getMaskSlitShaft2());

		////Shaft Status
		//if(theSlitterScorerState.monitor.getIsSlits1Plunged().getEnable())
		//{
		//	o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._SetShaftStatus(b[%s],b[%s],b[%s],b[%s],b[%s])",
		//		String::Bool2Alpha(theSlitterScorerState.monitor.getIsSlits1Plunged().getValue()),
		//		String::Bool2Alpha(theSlitterScorerState.monitor.getIsSlits2Plunged().getValue()),
		//		String::Bool2Alpha(theSlitterScorerState.monitor.getIsScores1Plunged().getValue()),
		//		String::Bool2Alpha(theSlitterScorerState.monitor.getIsScores2Plunged().getValue()),
		//		String::Bool2Alpha(theSlitterScorerState.monitor.getIsScoresAuxPlunged().getValue()));
		//}

		////RFAZZI_FI28/06/2008_1.Added send order Push Button on machine status page
		//if(communicationError!=C_PageManager::SMState.getSlitterScorerState().alarms.getIsAlarmMCPCommError())
		//{
		//	communicationError = C_PageManager::SMState.getSlitterScorerState().alarms.getIsAlarmMCPCommError();
		//	o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._CommunicationError(b[%s])",
  //      String::Bool2Alpha(communicationError));
		//}

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated oldFosberSlitterScorer_status page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_OldFosberSlitterScorerGroup::Console(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"oldFosberSlitterScorer_console._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"oldFosberSlitterScorer_console.lbl_Title_YN.Text=%s",C_PageManager::SMState.getSlitterScorerState().getMachineName());

	bool communicationError = false;
	while(!end_Console)
	{
		//if(communicationError!=C_PageManager::SMState.getSlitterScorerState().alarms.getIsAlarmMCPCommError())
		//{
		//	communicationError = C_PageManager::SMState.getSlitterScorerState().alarms.getIsAlarmMCPCommError();
		//	o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_console._CommunicationError(b[%s])",
  //      String::Bool2Alpha(communicationError));
		//}

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated oldFosberSlitterScorer_console page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_OldFosberSlitterScorerGroup::AlarmActive(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"oldFosberSlitterScorer_alarmActive._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"oldFosberSlitterScorer_alarmActive.lbl_Title_YN.Text=%s",C_PageManager::SMState.getSlitterScorerState().getMachineName());

	while(!end_AlarmsActive)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated oldFosberSlitterScorer_alarmActive page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_OldFosberSlitterScorerGroup::AlarmHistory(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"oldFosberSlitterScorer_alarmHistory._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"oldFosberSlitterScorer_alarmHistory.lbl_Title_YN.Text=%s",C_PageManager::SMState.getSlitterScorerState().getMachineName());

	while(!end_AlarmsHistory)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated oldFosberSlitterScorer_alarmHistory page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_OldFosberSlitterScorerGroup::SettingsMachine(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"oldFosberSlitterScorer_SettingsMachine._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"oldFosberSlitterScorer_SettingsMachine.lbl_Title_YN.Text=%s",C_PageManager::SMState.getSlitterScorerState().getMachineName());

	while(!end_SettingsMachine)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated oldFosberSlitterScorer_SettingsMachine page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_OldFosberSlitterScorerGroup::SettingsTask(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"oldFosberSlitterScorer_SettingsTask._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"oldFosberSlitterScorer_SettingsTask.lbl_Title_YN.Text=%s",C_PageManager::SMState.getSlitterScorerState().getMachineName());

	while(!end_SettingsTask)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated oldFosberSlitterScorer_SettingsTask page threads");

	return ERROR_SUCCESS;
}