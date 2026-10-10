//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

//Page Management
#include "..\C_PageManager.h"
#include "..\..\Include\ValidationExport.h"
#include ".\C_Twin400Group.h"
#include "..\..\Shared\C_SMTaskSettings.h"
TaskSettings::Settings::GeneralSettings generalSettings;
extern TaskSettings::C_SMTaskSettings SMTaskSettings;

bool C_Twin400Group::end_BladesAndScorers;
bool C_Twin400Group::end_Rotary;
bool C_Twin400Group::end_WebDirector;
bool C_Twin400Group::end_Blades;
bool C_Twin400Group::end_Console;
bool C_Twin400Group::end_OrderDetails;
bool C_Twin400Group::end_OrdersList;
bool C_Twin400Group::end_PastOrders;
bool C_Twin400Group::end_Flutes;
bool C_Twin400Group::end_Quality;
bool C_Twin400Group::end_Faults;
bool C_Twin400Group::end_Tasks;
bool C_Twin400Group::end_Plunges;
bool C_Twin400Group::end_MachineSettings;
bool C_Twin400Group::end_Misc;
bool C_Twin400Group::end_Alarm1;
bool C_Twin400Group::end_Alarm4;
//RFAZZI_FI26/01/2011_2 Aggiunta gestione Warning su grafica Twin400 di Syncro. 
bool C_Twin400Group::end_Warning1;
bool C_Twin400Group::end_Warning4;
bool C_Twin400Group::end_Maintenance;
bool C_Twin400Group::end_Simulations;
bool C_Twin400Group::end_TestInput;
bool C_Twin400Group::end_TestOutput;
bool C_Twin400Group::end_Overview;
bool C_Twin400Group::end_Positioning;
bool C_Twin400Group::end_Regulations;

C_Twin400Group::C_Twin400Group(void)
{
	pid_Twin400Page=NULL;
	TerminateThread(false);
}

C_Twin400Group::~C_Twin400Group(void)
{
}

bool C_Twin400Group::ChooseTwin400Page(String::StrType thePageRequest)
{
	//Twin400 group
		//production
	if(thePageRequest.CompareNoCase(L"Console"))
	{
		end_Console=false;
		pid_Twin400Page=::CreateThread(NULL,0,Console,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"Blades"))
	{
		end_Blades=false;
		pid_Twin400Page=::CreateThread(NULL,0,Blades,NULL,0,NULL);
		return true;
	}
		//orders
	if(thePageRequest.CompareNoCase(L"OrdersList"))
	{
		end_OrdersList=false;
		pid_Twin400Page=::CreateThread(NULL,0,OrdersList,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"OrderDetails"))
	{
		end_OrderDetails=false;
		thePageRequest.extractCommand();
		pid_Twin400Page=::CreateThread(NULL,0,OrderDetails,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"PastOrders"))
	{
		end_PastOrders=false;
		pid_Twin400Page=::CreateThread(NULL,0,PastOrders,NULL,0,NULL);
		return true;
	}
		//tools
	if(thePageRequest.CompareNoCase(L"BladesAndScorers"))
	{
		end_BladesAndScorers=false;
		pid_Twin400Page=::CreateThread(NULL,0,BladesAndScorers,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"Rotary"))
	{
		end_Rotary=false;
		pid_Twin400Page=::CreateThread(NULL,0,Rotary,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"WebDirector"))
	{
		end_WebDirector=false;
		pid_Twin400Page=::CreateThread(NULL,0,WebDirector,NULL,0,NULL);
		return true;
	}
		//flutes
	if(thePageRequest.CompareNoCase(L"Flutes"))
	{
		end_Flutes=false;
		pid_Twin400Page=::CreateThread(NULL,0,Flutes,NULL,0,NULL);
		return true;
	}
		//quality
	if(thePageRequest.CompareNoCase(L"Quality"))
	{
		end_Quality=false;
		pid_Twin400Page=::CreateThread(NULL,0,Quality,NULL,0,NULL);
		return true;
	}
		//faults
	if(thePageRequest.CompareNoCase(L"Faults"))
	{
		end_Faults=false;
		pid_Twin400Page=::CreateThread(NULL,0,Faults,NULL,0,NULL);
		return true;
	}
		//alarms
	if(thePageRequest.CompareNoCase(L"Alarm1"))
	{
		end_Alarm1=false;
		pid_Twin400Page=::CreateThread(NULL,0,Alarm1,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"Alarm4"))
	{
		end_Alarm4=false;
		pid_Twin400Page=::CreateThread(NULL,0,Alarm4,NULL,0,NULL);
		return true;
	}

  //RFAZZI_FI26/01/2011_2 Aggiunta gestione Warning su grafica Twin400 di Syncro. 
		//warnings
	if(thePageRequest.CompareNoCase(L"Warning1"))
	{
		end_Warning1=false;
		pid_Twin400Page=::CreateThread(NULL,0,Warning1,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"Warning4"))
	{
		end_Warning4=false;
		pid_Twin400Page=::CreateThread(NULL,0,Warning4,NULL,0,NULL);
		return true;
	}

		//Misc
	if(thePageRequest.CompareNoCase(L"Misc"))
	{
		end_Misc=false;
		pid_Twin400Page=::CreateThread(NULL,0,Misc,NULL,0,NULL);
		return true;
	}
	//Machine Settings
	if(thePageRequest.CompareNoCase(L"MachineSettings"))
	{
		end_MachineSettings=false;
		pid_Twin400Page=::CreateThread(NULL,0,MachineSettings,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"Tasks"))
	{
		end_Tasks=false;
		pid_Twin400Page=::CreateThread(NULL,0,Tasks,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"Plunges"))
	{
		end_Plunges=false;
		pid_Twin400Page=::CreateThread(NULL,0,Plunges,NULL,0,NULL);
		return true;
	}

	//Maintenance
	if(thePageRequest.CompareNoCase(L"Maintenance"))
	{
		end_Maintenance=false;
		pid_Twin400Page=::CreateThread(NULL,0,Maintenance,NULL,0,NULL);
		return true;
	}
	//Simulations
	if(thePageRequest.CompareNoCase(L"Simulations"))
	{
		end_Simulations=false;
		pid_Twin400Page=::CreateThread(NULL,0,Simulations,NULL,0,NULL);
		return true;
	}
	//TestInput
	if(thePageRequest.CompareNoCase(L"TestInput"))
	{
		end_TestInput=false;
		pid_Twin400Page=::CreateThread(NULL,0,TestInput,NULL,0,NULL);
		return true;
	}
	//TestOutput
	if(thePageRequest.CompareNoCase(L"TestOutput"))
	{
		end_TestOutput=false;
		pid_Twin400Page=::CreateThread(NULL,0,TestOutput,NULL,0,NULL);
		return true;
	}

	//Overview
	if(thePageRequest.CompareNoCase(L"Overview"))
	{
		end_Overview=false;
		pid_Twin400Page=::CreateThread(NULL,0,Overview,NULL,0,NULL);
		return true;
	}
	//Positioning
	if(thePageRequest.CompareNoCase(L"Positioning"))
	{
		end_Positioning=false;
		pid_Twin400Page=::CreateThread(NULL,0,Positioning,NULL,0,NULL);
		return true;
	}

	//Regulations
	if(thePageRequest.CompareNoCase(L"Regulations"))
	{
		end_Regulations=false;
		pid_Twin400Page=::CreateThread(NULL,0,Regulations,NULL,0,NULL);
		return true;
	}

	return false;
}

void C_Twin400Group::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_Twin400Page!=NULL)
		{
			BOOL res = ::TerminateThread(pid_Twin400Page,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread Twin400Page ... %d", ::GetLastError());
		}
	}
	else
	{
		end_BladesAndScorers = true;
		end_Rotary = true;
		end_WebDirector = true;
		end_Blades = true;
		end_Console = true;
		end_OrderDetails = true;
		end_OrdersList = true;
		end_PastOrders = true;
		end_Flutes = true;
		end_Quality = true;
		end_Faults = true;
		end_Tasks = true;
		end_Plunges = true;
		end_MachineSettings = true;
		end_Misc = true;
		end_Alarm1 = true;
		end_Alarm4 = true;
    //RFAZZI_FI26/01/2011_2 Aggiunta gestione Warning su grafica Twin400 di Syncro. 
		end_Warning1 = true;
		end_Warning4 = true;

		end_Maintenance = true;
		end_Simulations = true;
		end_TestInput = true;
		end_TestOutput = true;
		end_Overview = true;
		end_Positioning = true;
		end_Regulations = true;
	}
}

DWORD C_Twin400Group::SuspendThread(void)
{
	if(pid_Twin400Page==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_Twin400Page)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page Twin400 Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_Twin400Group::ResumeThread(void)
{
	if(pid_Twin400Page==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_Twin400Page)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page Twin400 Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}

DWORD WINAPI C_Twin400Group::BladesAndScorers(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_bladesAndScorers";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;

	while(!end_BladesAndScorers)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;
			case State::C_SMState::SS_SetupSituation:
			{
				int cntSetupSit=0;
				for (int stp=0; stp<State::SS_SETUPS; stp++)
				{
					wchar_t s[1000]={L'\0'};
					int cnt=0;
					for (Shaft sh=Slit1; sh<=Slit2; sh++)
					{
						cnt+=swprintf(s+cnt, L"%d%s", ssState.getSetupSituationPlungedSlitMask(cntSetupSit, sh), Network::SEP);
						cnt+=swprintf(s+cnt, L"%d%s", ssState.getSetupSituationSepSlitMask(cntSetupSit, sh), Network::SEP);
						cnt+=swprintf(s+cnt, L"%d%s", ssState.getSetupSituationDummySlitMask(cntSetupSit, sh), Network::SEP);
						for (int slt=0; slt<MAX_SLITS_PER_SHAFT; slt++)
							cnt+=swprintf(s+cnt, L"%d%s", ssState.getSetupSituationSlitPosition(cntSetupSit, sh, slt), Network::SEP);
					}
					cnt+=swprintf(s+cnt, L"%d%s", ssState.getSetupSituationShaftMask(cntSetupSit), Network::SEP);
					cnt+=swprintf(s+cnt, L"%s%s", String::Bool2Alpha(ssState.getSetupSituationNarrowToWide(cntSetupSit)), Network::SEP);
					cnt+=swprintf(s+cnt, L"%s%s", String::Bool2Alpha(ssState.getSetupSituationWideToNarrow(cntSetupSit)), Network::SEP);
					cnt+=swprintf(s+cnt, L"%s%s", String::Bool2Alpha(ssState.getSetupSituationCleanSlits(cntSetupSit)), Network::SEP);
					cnt+=swprintf(s+cnt, L"%d", ssState.getSetupSituationSetupId(cntSetupSit));
					o_socket.Send(Network::MSG_PAG_REF, L"%s._UpdateSetupSlits(I[%d], S[%s])", formName, stp, s);
					if (/*!ssState.getSetupSituationFirstOfKnifeOnwards(cntSetupSit)*/ true) cntSetupSit++;
				}
				if (!allCases) break;
			}
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_bladesAndScorers page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_Twin400Group::Positioning(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_Positioning";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;

	while(!end_Positioning)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;
			case State::C_SMState::SS_NewPositionigShaft1:
			{
				Shaft sh=Slit1;
				int lastPosNum=ssState.getShaftPositioning(sh).counter;
				int setupInd=ssState.getShaftPositioning(sh).setupIndex;
				wchar_t s[(4+2*MAX_SLITS_PER_SHAFT)*10]={L'\0'};
				int cnt=0;
				cnt+=swprintf(s+cnt, L"%d%s", sh, Network::SEP);
				cnt+=swprintf(s+cnt, L"%d%s", ssState.getShaftPositioning(sh).plungedSlitMask, Network::SEP);
				cnt+=swprintf(s+cnt, L"%d%s", ssState.getShaftPositioning(sh).slitNotOkMask, Network::SEP);
				for (int i=0; i<MAX_SLITS_PER_SHAFT; i++)
				{
					cnt+=swprintf(s+cnt, L"%d%s", ssState.getShaftPositioning(sh).getTheoreticalPosition(i), Network::SEP);
					cnt+=swprintf(s+cnt, L"%d%s", ssState.getShaftPositioning(sh).getScannedPosition(i), Network::SEP);
				}
				cnt+=swprintf(s+cnt, L"dummy");
				o_socket.Send(Network::MSG_PAG_REF, L"%s._SetPositioningCheck(S[%s])", formName, s);
				o_socket.Send(Network::MSG_PAG_REF, L"%s.lbl_PosCounter%d.Text=%d", formName, sh, lastPosNum);
				if (!allCases) break;
			}
			case State::C_SMState::SS_NewPositionigShaft2:
			{
				Shaft sh=Slit2;
				int lastPosNum=ssState.getShaftPositioning(sh).counter;
				int setupInd=ssState.getShaftPositioning(sh).setupIndex;
				wchar_t s[(4+2*MAX_SLITS_PER_SHAFT)*10]={L'\0'};
				int cnt=0;
				cnt+=swprintf(s+cnt, L"%d%s", sh, Network::SEP);
				cnt+=swprintf(s+cnt, L"%d%s", ssState.getShaftPositioning(sh).plungedSlitMask, Network::SEP);
				cnt+=swprintf(s+cnt, L"%d%s", ssState.getShaftPositioning(sh).slitNotOkMask, Network::SEP);
				for (int i=0; i<MAX_SLITS_PER_SHAFT; i++)
				{
					cnt+=swprintf(s+cnt, L"%d%s", ssState.getShaftPositioning(sh).getTheoreticalPosition(i), Network::SEP);
					cnt+=swprintf(s+cnt, L"%d%s", ssState.getShaftPositioning(sh).getScannedPosition(i), Network::SEP);
				}
				cnt+=swprintf(s+cnt, L"dummy");
				o_socket.Send(Network::MSG_PAG_REF, L"%s._SetPositioningCheck(S[%s])", formName, s);
				o_socket.Send(Network::MSG_PAG_REF, L"%s.lbl_PosCounter%d.Text=%d", formName, sh, lastPosNum);
				if (!allCases) break;
			}
			case State::C_SMState::SS_MachineStatus:
				for (Shaft sh=Slit1; sh<=Slit2; sh++)
					o_socket.Send(Network::MSG_PAG_REF,L"%s.toolDetailMachine.SetPositioning(B[%s], I[%d])", formName, String::Bool2Alpha(ssState.getShaftStatusPositioning(sh)), sh);
				if (!allCases) break;
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_Positioning page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_Twin400Group::Regulations(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_Regulations";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;
	while(!end_Regulations)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		bool enableEnabled=ssState.getTaskStatusPLCComm() && ssState.getTaskStatusPLCRun();
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;
			case State::C_SMState::SS_TaskStatus:
				o_socket.Send(Network::MSG_PAG_REF,L"%s._SetPLCComm(b[%s])", formName, String::Bool2Alpha(ssState.getTaskStatusPLCComm()));
				if (!allCases) break;
			//per saica
			case State::C_SMState::SS_ChutesAndGap:
			case State::C_SMState::SS_SetupSituation:
			case State::C_SMState::SS_MachineStatus:
			{
				bool internalFlapDisabled=ssState.getEnableStatusInternalFlap();
				o_socket.Send(Network::MSG_PAG_REF,L"%s.InternalFlap.Text=%s", formName, (internalFlapDisabled?L"Enable Internal Flap":L"Disable Internal Flap"));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.InternalFlap.Active=%s", formName, String::Bool2Alpha(!internalFlapDisabled));
				bool speedDigitalReferenceDisabled=ssState.getEnableStatusSpeedDigitalReference();
				o_socket.Send(Network::MSG_PAG_REF,L"%s.SpeedDigitalReference.Text=%s", formName, (speedDigitalReferenceDisabled?L"Enable Digital Reference":L"Disable Digital Reference"));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.SpeedDigitalReference.Active=%s", formName, String::Bool2Alpha(!speedDigitalReferenceDisabled));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.InternalFlap.Enabled=%s", formName, String::Bool2Alpha(enableEnabled));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.SpeedDigitalReference.Enabled=%s", formName, String::Bool2Alpha(enableEnabled));
				bool photoeyeEnabled=ssState.getEnableStatusSlitPhotoeyeEnabledMan();
				o_socket.Send(Network::MSG_PAG_REF,L"%s.PhotoeyeSlitShaft.Text=%s", formName, (photoeyeEnabled?L"Disable Slit Shaft photoeyes":L"Enable Slit Shaft photoeyes"));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.PhotoeyeSlitShaft.Active=%s", formName, String::Bool2Alpha(photoeyeEnabled));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.PhotoeyeSlitShaft.BackColor=%s", formName, (photoeyeEnabled?L"White":L"Red"));
				bool slowPositioningEnabled=ssState.getEnableStatusForceSlowPositioning();
				o_socket.Send(Network::MSG_PAG_REF,L"%s.SlowPositioning.Text=%s", formName, (slowPositioningEnabled?L"Remove Slow Positioning":L"Force Slow Positioning"));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.SlowPositioning.Active=%s", formName, String::Bool2Alpha(!slowPositioningEnabled));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.SlowPositioning.BackColor=%s", formName, (!slowPositioningEnabled?L"White":L"Red"));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.PhotoeyeSlitShaft.Enabled=%s", formName, String::Bool2Alpha(enableEnabled));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.SlowPositioning.Enabled=%s", formName, String::Bool2Alpha(enableEnabled));
				bool adjustEnable=ssState.getMachineStatusRunning() && ssState.getMachineStatusBoardPresent() && (!ssState.getAOCStatusApproaching())  &&
									(!ssState.getAOCStatusRunning()) && ssState.getSetupStatusCurrentPlunged() && (ssState.IsShaftGapModified());
				o_socket.Send(Network::MSG_PAG_REF,L"%s.AdjustScoreGapOffset.Enabled=%s", formName, String::Bool2Alpha(adjustEnable));
			}
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}
	logger.Fine(__FUNCTION__, L"Terminated twin400_Regulations page threads");
	return ERROR_SUCCESS;
}

DWORD WINAPI C_Twin400Group::Overview(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_Overview";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::SSPlcState::ShaftStatus shStatus[NUM_SLIT_SHAFTS+MAX_NUM_SCORE_SHAFTS];
	int shSetup[NUM_SLIT_SHAFTS+MAX_NUM_SCORE_SHAFTS];
	State::SSPlcState::MachineStatus macStatus;
	State::SSPlcState::OrderChangeStatus aocStatus;
  State::SSPlcState::SlitPositionsStatus spStatus;
  State::SSPlcState::SlitPositioningStatus spgStatus;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;

	while(!end_Overview)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;
			case State::C_SMState::SS_WaitTimeOut:
				o_socket.Send(Network::MSG_PAG_REF, L"%s.tlb_Speed.Value=%d", formName, ssState.getSpeed());
				if (!allCases) break;
			case State::C_SMState::SS_TaskStatus:
				o_socket.Send(Network::MSG_PAG_REF, L"%s.PLCComm.Alarmed=%s", formName, String::Bool2Alpha(!ssState.getTaskStatusPLCComm()));
        o_socket.Send(Network::MSG_PAG_REF, L"%s.SupervisorComm.Active=%s", formName, String::Bool2Alpha(ssState.getTaskStatusRemoteControl()));
        o_socket.Send(Network::MSG_PAG_REF, L"%s.SupervisorComm.Alarmed=%s", formName, String::Bool2Alpha(!ssState.getTaskStatusSupervisorComm()));
				spStatus=ssState.GetSlitPositionsStatus();
				o_socket.Send(Network::MSG_PAG_REF, L"%s._SetSlitPositionsStatus(I[%d])", formName, spStatus);
				spgStatus=ssState.GetSlitPositioningStatus();
				o_socket.Send(Network::MSG_PAG_REF, L"%s._SetSlitPositioningStatus(I[%d])", formName, spgStatus);
				o_socket.Send(Network::MSG_PAG_REF, L"%s._SetWaitForManualCommandCurr(B[%s])", formName, String::Bool2Alpha(ssState.getTaskStatusWaitForManualSendingCurr()));
				o_socket.Send(Network::MSG_PAG_REF, L"%s._SetWaitForManualCommandNext(B[%s])", formName, String::Bool2Alpha(ssState.getTaskStatusWaitForManualSendingNext()));
				o_socket.Send(Network::MSG_PAG_REF, L"%s.setupStatus_YN.SetOrderSendingEnabled(B[%s])", formName, String::Bool2Alpha(ssState.getTaskStatusOrderSendingEnabled()));
			//No break here please!!
			case State::C_SMState::SS_MachineStatus:
				macStatus=ssState.GetMachineStatus();
				o_socket.Send(Network::MSG_PAG_REF, L"%s._SetMachineStatus(I[%d])", formName, macStatus);
				aocStatus=ssState.GetOrderChangeStatus();
				o_socket.Send(Network::MSG_PAG_REF, L"%s._SetOrderChangeStatus(I[%d])", formName, aocStatus);
				o_socket.Send(Network::MSG_PAG_REF, L"%s._SetPositioningFailed(B[%s])", formName, String::Bool2Alpha(ssState.getMachineStatusPositioningFailed()));
				//No break here please!!
			case State::C_SMState::SS_MachineSetup:
				ssState.GetShaftStatus(shStatus, shSetup);
				o_socket.Send(Network::MSG_PAG_REF, L"%s._ShowShaftStatus(I[%d],I[%d],I[%d],I[%d],I[%d],I[%d],I[%d],I[%d],I[%d],I[%d],I[%d],I[%d],I[%d],I[%d],I[%d],I[%d])",
					formName, shStatus[Slit1], shStatus[Slit2], shStatus[Score1], shStatus[Score2], shStatus[ScoreAux], 
					shSetup[Slit1], shSetup[Slit2], shSetup[Score1], shSetup[Score2], shSetup[ScoreAux],
					ssState.getSetupID(0), ssState.getModID(0), ssState.getSetupID(1), ssState.getModID(1), ssState.getSetupID(2), ssState.getModID(2));
				if (!allCases) break;
			case State::C_SMState::SS_ChutesAndGap:
			{
					o_socket.Send(Network::MSG_PAG_REF, L"%s._SetGap(I[%d], I[%d], I[%d], I[%d], I[%d], I[%d])", formName,
						ssState.getScoreGap(Score1), ssState.getScoreGap(Score2), ssState.getScoreGap(ScoreAux),
						ssState.getActScoreGap(Score1), ssState.getActScoreGap(Score2), ssState.getActScoreGap(ScoreAux));
				if (!allCases) break;
			}
			case State::C_SMState::SS_Alarms:
				o_socket.Send(Network::MSG_PAG_REF, L"%s.Hardware.Alarmed=%s", formName, String::Bool2Alpha(ssState.getAlarmsHardware()));
				o_socket.Send(Network::MSG_PAG_REF, L"%s.Power.Alarmed=%s", formName, String::Bool2Alpha(ssState.getAlarmsPower()));
				o_socket.Send(Network::MSG_PAG_REF, L"%s.Logic.Alarmed=%s", formName, String::Bool2Alpha(ssState.getAlarmsLogic()));
				o_socket.Send(Network::MSG_PAG_REF, L"%s.OnBoard.Alarmed=%s", formName, String::Bool2Alpha(ssState.getAlarmsOnBoard()));
				o_socket.Send(Network::MSG_PAG_REF, L"%s.Console.Alarmed=%s", formName, String::Bool2Alpha(ssState.getAlarmsConsole()));
				o_socket.Send(Network::MSG_PAG_REF, L"%s.Drives.Alarmed=%s", formName, String::Bool2Alpha(ssState.getAlarmsDrives()));
				o_socket.Send(Network::MSG_PAG_REF, L"%s.DeviceNET.Alarmed=%s", formName, String::Bool2Alpha(ssState.getAlarmsDeviceNET()));
				o_socket.Send(Network::MSG_PAG_REF, L"%s.Warnings.Alarmed=%s", formName, String::Bool2Alpha(ssState.getAlarmsWarnings()));
				o_socket.Send(Network::MSG_PAG_REF, L"%s.Software.Alarmed=%s", formName, String::Bool2Alpha(ssState.getAlarmsSoftware()));
				if (!allCases) break;
      //agrassi_FI09-11-2009_1
			//case State::C_SMState::SS_SetupSituation:
			//	o_socket.Send(Network::MSG_PAG_REF,L"%s._SetSetupSituation(b[%s],b[%s],b[%s],b[%s],b[%s],b[%s])", formName, 
			//		String::Bool2Alpha(ssState.getSetupSituationWideToNarrow(0) && ssState.getSetupSituationValid(0)), 
			//		String::Bool2Alpha(ssState.getSetupSituationWideToNarrow(1) && ssState.getSetupSituationValid(1)), 
			//		String::Bool2Alpha(ssState.getSetupSituationWideToNarrow(2) && ssState.getSetupSituationValid(2)),
			//		String::Bool2Alpha(ssState.getSetupSituationNarrowToWide(0) && ssState.getSetupSituationValid(0)), 
			//		String::Bool2Alpha(ssState.getSetupSituationNarrowToWide(1) && ssState.getSetupSituationValid(1)),
			//		String::Bool2Alpha(ssState.getSetupSituationNarrowToWide(2) && ssState.getSetupSituationValid(2)));
			//	if (!allCases) break;
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_Overview page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_Twin400Group::Rotary(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_rotary";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;

	while(!end_Rotary)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;
			case State::C_SMState::SS_SetupSituation:
			{
				for (int stp=0; stp<State::SS_SETUPS-1; stp++)
					o_socket.Send(Network::MSG_PAG_REF, L"%s._UpdateRCSMask(I[%d], I[%d], B[%s])", 
												formName, stp, ssState.getSetupSituationRCSSecotrMask(stp+1),
												String::Bool2Alpha(ssState.getSetupSituationValid(stp+1)));
				if (!allCases) break;
			}
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_rotary page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_Twin400Group::WebDirector(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_webDirector";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;
	
	while(!end_WebDirector)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;
			case State::C_SMState::SS_WebDirector:
				o_socket.Send(Network::MSG_PAG_REF, L"%s._UpdateWebDirMask(I[%d], I[%d], I[%d], B[%s])", 
											formName, 0, ssState.getWebDir2LevMask(), ssState.getWebDir3LevMask(), L"true");
				if (!allCases) break;
			case State::C_SMState::SS_SetupSituation:
			{
				for (int stp=1; stp<State::SS_SETUPS; stp++)
					o_socket.Send(Network::MSG_PAG_REF, L"%s._UpdateWebDirMask(I[%d], I[%d], I[%d], B[%s])", 
												formName, stp, ssState.getSetupSituationWebDir2levelMask(stp),
												ssState.getSetupSituationWebDir3levelMask(stp),
												String::Bool2Alpha(ssState.getSetupSituationValid(stp)));
				if (!allCases) break;
			}
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_webDirector page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_Twin400Group::Blades(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_blades";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event = State::C_SMState::SS_RefreshAll;
	//RFAZZI_FI19/08/2008_1.Added New Twin400 PLC Protocol 6. Now the Manual Grinding is enabled when the line is stopped. 
	//bool lowSpeed=false, oldLowSpeed=false;
	bool oldManualGrindingDisabled=false;

	while(!end_Blades)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;
			case State::C_SMState::SS_CleanCutData:
			case State::C_SMState::SS_WaitTimeOut:
			{
				//RFAZZI_FI27/08/08_1. Added value MetersSinceLastSlitRegolation on graphics
				//wchar_t slitsData[(NUM_SLIT_SHAFTS*MAX_SLITS_PER_SHAFT*4+6)*10];
				wchar_t slitsData[(NUM_SLIT_SHAFTS*MAX_SLITS_PER_SHAFT*6+6)*10];
				int cnt=0;
				for (Shaft sh=Slit1; sh<=Slit2; sh++)
				{
					for (int slt=0; slt<MAX_SLITS_PER_SHAFT; slt++)
					{
						cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.getTotalRunMeters(sh, slt), Network::SEP);
						cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.getTotalGrindings(sh, slt), Network::SEP);
						cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.getMetersSinceLastGrind(sh, slt), Network::SEP);
						cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.getMetersSinceLastLube(sh, slt), Network::SEP);
						//RFAZZI_FI27/08/08_1. Added value MetersSinceLastSlitRegolation on graphics
						cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.getGrindMetersSinceLastRegulation(sh, slt), Network::SEP);
						//RFAZZI_FI04/11/08_1. APM TW4084. Added new counters "TotalGrindMeters" and "grindMetersSinceLastRegulation" in the machine protocol.
						cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.getTotalGrindMetres(sh, slt), Network::SEP);
					}
				}
				cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.getGrindToolMeters(), Network::SEP);
				cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.getGrindDuration(), Network::SEP);
				cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.getLubeDuration(), Network::SEP);
				cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.getSetupSituationGrindInterval(0), Network::SEP);
				cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.getSetupSituationLubeInterval(0), Network::SEP);
				cnt+=swprintf(slitsData+cnt, L"%d", ssState.getSetupSituationTrimGrindMultiplier(0));
				o_socket.Send(Network::MSG_PAG_REF,L"%s._UpdateCleanCutCounters(S[%s])", formName, slitsData);
				//RFAZZI_FI19/08/2008_1.Added New Twin400 PLC Protocol 6. Now the Manual Grinding is enabled when the line is stopped. 
				//lowSpeed=(ssState.getSpeed()<50);
				//if (lowSpeed ^ oldLowSpeed)
				//	allCases=true;
				//oldLowSpeed=lowSpeed;
				if (ssState.getManualGrindingDisabled() ^ oldManualGrindingDisabled)
					allCases=true;
				oldManualGrindingDisabled=ssState.getManualGrindingDisabled();

				if (!allCases) break;
			}
			case State::C_SMState::SS_MachineStatus:
			case State::C_SMState::SS_TaskStatus:
       {
        bool pushEnabled=(ssState.getTaskStatusPLCComm() && ssState.getTaskStatusPLCRun());
				for (Shaft sh=Slit1; sh<=Slit2; sh++)
					o_socket.Send(Network::MSG_PAG_REF,L"%s.toolDetail.SetPositioning(B[%s], I[%d])", formName, String::Bool2Alpha(ssState.getShaftStatusPositioning(sh)), sh);
				int shaft=-1;
				int slit=-1;
				//if (!ssState.getMaintenanceStatusMachineInMaintenance())
				//{
					shaft=ssState.getGrindingData().shaft;
					slit=ssState.getGrindingData().slit;
				//}
				//MA4 mul
				wchar_t grindCounters[30];
				int cnt=0;
				for (int i=0; i<MAX_SLITS_PER_SHAFT; i++)
					cnt+=swprintf(grindCounters+cnt, L"%d%s", ssState.getGrindingData().multiple.slitToGrind[i], ((i<MAX_SLITS_PER_SHAFT-1)?Network::SEP:L""));
		    //agrassi_FI18-04-08_1
				//o_socket.Send(Network::MSG_PAG_REF,L"%s._SetGrinding(B[%s],I[%d],I[%d],B[%s],S[%s])", formName, 
				//		String::Bool2Alpha(ssState.getMachineStatusGrinding()), shaft, slit, String::Bool2Alpha(ssState.getGrindingData().multiple.running), grindCounters);
				o_socket.Send(Network::MSG_PAG_REF,L"%s._SetGrinding(B[%s],I[%d],I[%d],B[%s],S[%s],B[%s])", formName, 
						String::Bool2Alpha(ssState.getMachineStatusGrinding()), shaft, slit, String::Bool2Alpha(ssState.getGrindingData().multiple.running), 
						grindCounters, String::Bool2Alpha(ssState.getMaintenanceStatusMachineInMaintenance()));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.ResetGrinder.Enabled=%s", formName, String::Bool2Alpha(pushEnabled));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.LubeSlit.Enabled=%s", formName, String::Bool2Alpha(pushEnabled && ssState.getMachineStatusRunning()));
				//AGrassi_FI06/12/2007_1

				//RFAZZI_FI19/08/2008_1.Added New Twin400 PLC Protocol 6. Now the Manual Grinding is enabled when the line is stopped. 
				//bool normalGrindingDisabled=ssState.getMachineStatusGrinding() || ssState.getShaftStatusPositioning(Slit1) || ssState.getShaftStatusPositioning(Slit2) ||
				//		ssState.getMachineStatusPositioningFailed() || lowSpeed || ssState.getMachineStatusScanRunning() ||
				//		ssState.getAOCStatusApproaching() || ssState.getAOCStatusRunning() || (ssState.getMaintenanceStatusMachineInMaintenance() && (!ssState.getMaintenanceStatusMachinePositionedForGrinding())) ||
				//		ssState.getGrindingData().multiple.running || ssState.getFunctionalityStatusTeflonCycleRunning();
				//o_socket.Send(Network::MSG_PAG_REF,L"%s.GrindSlit.Enabled=%s", formName, String::Bool2Alpha(pushEnabled && ssState.getFunctionalityStatusGrindEnable() && (!normalGrindingDisabled)));

				o_socket.Send(Network::MSG_PAG_REF,L"%s.GrindSlit.Enabled=%s", formName, String::Bool2Alpha(pushEnabled && ssState.getFunctionalityStatusGrindEnable() && (!ssState.getManualGrindingDisabled())));

				//RFAZZI_FI03/10/2008.Added dedicated bit for enable the "Grinding Enabled" push button on graphics
				//bool canEnableGrinding=(!ssState.getFunctionalityStatusGrindEnable()) && pushEnabled;
				bool canEnableGrinding=(ssState.getFunctionalityStatusGrindingAlarmPresentEnable()) && pushEnabled;
				//bool canEnableGrinding=(!ssState.getFunctionalityStatusGrindEnable()) && (!normalGrindingDisabled) && pushEnabled;
				o_socket.Send(Network::MSG_PAG_REF,L"%s.EnableGrinding.Visible=%s", formName, String::Bool2Alpha(canEnableGrinding));
        //agrassi_FI08-09-2010_1
				o_socket.Send(Network::MSG_PAG_REF,L"%s.StopMultiple.Visible=%s", formName, String::Bool2Alpha(ssState.getGrindingData().multiple.running));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.ResetSlitCounters.Enabled=%s", formName, String::Bool2Alpha(pushEnabled));
				o_socket.Send(Network::MSG_PAG_REF,L"%s._SetMaintenance(B[%s])", formName, String::Bool2Alpha(ssState.getMaintenanceStatusMachineInMaintenance()));
				if (!allCases) break;
       }
			case State::C_SMState::SS_Luricating:
       {
				int lubeChannel=ssState.getOutputChannel(State::OUTLubrication);
				logger.Info(__FUNCTION__, L"Set lubrication: lubeChannel=%d", lubeChannel);
				bool lubricating=(lubeChannel!=0);
				int shaft=-1;
				int slit=-1;
				if (lubricating)
				{
					int bit=Utility::LowestBIT(lubeChannel);
					shaft=(bit / 8);
					slit=(bit % 8);
				}
				o_socket.Send(Network::MSG_PAG_REF,L"%s._SetLubricating(B[%s],I[%d],I[%d])", formName, String::Bool2Alpha(lubricating), shaft, slit);
				if (!allCases) break;
       }
			case State::C_SMState::SS_SlitPositions:
			{
				wchar_t slitsData[NUM_SLIT_SHAFTS*(MAX_SLITS_PER_SHAFT+1)*10];
				int cnt=0;
				for (Shaft sh=Slit1; sh<=Slit2; sh++)
				{
					for (int slt=0; slt<MAX_SLITS_PER_SHAFT; slt++)
					{
						cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.getSlitPositions(sh, slt), Network::SEP);
					}
					cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.getPlungedSlits(sh), Network::SEP);
					cnt+=swprintf(slitsData+cnt, L"%s%s", String::Bool2Alpha(ssState.getRequestsGoodSlitPositions(sh)), (sh==Slit1?(Network::SEP):L""));
				}
				o_socket.Send(Network::MSG_PAG_REF,L"%s._UpdateSlitPositions(S[%s])", formName, slitsData);
				break;
			}
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(500, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_blades page threads");

	return ERROR_SUCCESS;
}

//AGrassi_FI06/12/2007_1
char* C_Twin400Group::getTeflonReasonCode(State::SSPlcState* ssPLCState)
{
	if (!ssPLCState->getMachineStatusRunning()) return stringer(SYCS_59);
	if (ssPLCState->getMachineStatusPositioning()) return stringer(SYCS_45);
	if (ssPLCState->getMachineStatusPositioningFailed()) return stringer(SYCS_46);
	if (ssPLCState->getMachineStatusScanRunning()) return stringer(SYCS_47);
	if (ssPLCState->getMachineStatusGrinding()) return stringer(SYCS_48);
	if (ssPLCState->getAOCStatusApproaching()) return stringer(SYCS_49);
	if (ssPLCState->getAOCStatusRunning()) return stringer(SYCS_50);
	if (ssPLCState->getFunctionalityStatusTeflonCycleRunning()) return stringer(SYCS_51);
	if (ssPLCState->getFunctionalityStatusTeflonLoading(Slit1) || ssPLCState->getFunctionalityStatusTeflonLoading(Slit2)) return stringer(SYCS_52);
	if (ssPLCState->getMaintenanceStatusMachineInMaintenance()) return stringer(SYCS_53);
	if ((!ssPLCState->getShaftStatusPlunged(Slit1)) && (!ssPLCState->getShaftStatusPlunged(Slit2))) return stringer(SYCS_54);
	if ((!ssPLCState->getEnableStatusSlitPhotoeyeEnabled(Slit1)) || (!ssPLCState->getEnableStatusSlitPhotoeyeEnabled(Slit2))) return stringer(SYCS_55);
	if ((!ssPLCState->getRequestsGoodSlitPositions(Slit1)) || (!ssPLCState->getRequestsGoodSlitPositions(Slit2))) return stringer(SYCS_56);
	if  ((ssPLCState->getSetupStatusCurrentOnShaft(Slit1) && ssPLCState->getSetupStatusCurrentOnShaft(Slit2)) ||
			 (ssPLCState->getSetupStatusCurrentOnShaft(Score1) && ssPLCState->getSetupStatusCurrentOnShaft(Score2)))
			 return stringer(SYCS_57);
	return stringer(SYCS_58);
}

DWORD WINAPI C_Twin400Group::Console(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_console";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;
	SMTaskSettings.getGeneralSettings(generalSettings);
	bool pmac400=generalSettings.getTwin400Only();
  //agrassi_FI11-03-2010_1
  LineConfig cfg;
  C_PageManager::SMLineConfig.getLineConfig(cfg);

	while(!end_Console)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
    bool pushEnabled=(!ssState.getMachineStatusInEmergency()) && ssState.getTaskStatusPLCComm() && ssState.getTaskStatusPLCRun();
		//AGrassi_FI06/12/2007_1
		//bool enableEnabled=ssState.getTaskStatusPLCComm() && ssState.getTaskStatusPLCRun();
		bool enableEnabled=ssState.getTaskStatusPLCComm() && ssState.getTaskStatusPLCRun() && (!ssState.getFunctionalityStatusTeflonCycleRunning());
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;
			case State::C_SMState::SS_TaskStatus:
			{
				bool remoteActive=ssState.getTaskStatusRemoteControl() && ssState.getTaskStatusSupervisorComm();
        o_socket.Send(Network::MSG_PAG_REF,L"%s.RemoteControl.Active=%s", formName, String::Bool2Alpha(ssState.getTaskStatusRemoteControl()));
        o_socket.Send(Network::MSG_PAG_REF,L"%s.RemoteControl.BackColor=%s", formName, (ssState.getTaskStatusRemoteControl()?L"White":L"Red"));
        o_socket.Send(Network::MSG_PAG_REF,L"%s.RemoteControl.Text=§%S", formName, (remoteActive?stringer(SYCS_11):stringer(SYCS_12)));
        //o_socket.Send(Network::MSG_PAG_REF,L"%s.RemoteControl.Enabled=%s", formName, String::Bool2Alpha(true));
        //No break here, please!
			}
			case State::C_SMState::SS_MachineStatus:
			{
				bool enableSendSetup = enableEnabled && (!ssState.getMaintenanceStatusMachineInMaintenance()) && (!ssState.getMachineStatusPositioning());
				enableSendSetup = enableSendSetup && (!ssState.getAOCStatusApproaching()) && (!ssState.getAOCStatusRunning()) && (!ssState.getAOCStatusComplete());
				enableSendSetup = enableSendSetup && (!ssState.getMachineStatusPositioningFailed());
				//per mariestad
				bool enableSendCurrent = enableSendSetup && (!ssState.getMachineStatusRunning()) && (!ssState.getMachineStatusResetRunning());
				bool enableSendNext = enableSendSetup && ssState.getSetupStatusCurrentPositioned() && ssState.getTaskStatusSetupAligned(0);
				bool highlightCurrent=false, highlightNext=false;
				for (Shaft sh=Slit1; sh<=Slit2; sh++)
					highlightCurrent=highlightCurrent || (((!ssState.getSlitPositionsStatusPositioningCheckOK(sh))) && (ssState.getSetupStatusCurrentOnShaft(sh)));
				//AGrassi_FI06/12/2007_1
				//highlightCurrent=highlightCurrent || ssState.getTaskStatusWaitForManualSendingCurr(); 
				for (Shaft sh=Slit1; sh<=Slit2; sh++)
					highlightNext=highlightNext || (((!ssState.getSlitPositionsStatusPositioningCheckOK(sh))) && ((!ssState.getSetupStatusCurrentOnShaft(sh))));
				//AGrassi_FI06/12/2007_1
				//highlightNext=highlightNext || ssState.getTaskStatusWaitForManualSendingNext();
 				o_socket.Send(Network::MSG_PAG_REF,L"%s.SendSetup0.HighLightEnable=%s", formName, String::Bool2Alpha(highlightCurrent));
 				o_socket.Send(Network::MSG_PAG_REF,L"%s.SendSetup1.HighLightEnable=%s", formName, String::Bool2Alpha(highlightNext));

 				o_socket.Send(Network::MSG_PAG_REF,L"%s.SendSetup0.Enabled=%s", formName, String::Bool2Alpha(enableSendCurrent));
 				o_socket.Send(Network::MSG_PAG_REF,L"%s.SendSetup1.Enabled=%s", formName, String::Bool2Alpha(enableSendNext));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.ManualAutomaticMachineMove.Active=%s", formName, (ssState.getFunctionalityStatusEdgeGuideAuto()?L"true":L"false"));
        bool jamupPhEyeDisabled=ssState.getEnableStatusJamupPhotoeye();
				o_socket.Send(Network::MSG_PAG_REF,L"%s.JamupPhotoeye.Text=§%S", formName, (jamupPhEyeDisabled?stringer(SYCS_15):stringer(SYCS_16)));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.JamupPhotoeye.Active=%s", formName, String::Bool2Alpha(!jamupPhEyeDisabled));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.JamupPhotoeye.BackColor=%s", formName, (!jamupPhEyeDisabled?L"White":L"Red"));

				//RFAZZI_FI19/06/2008_1.Added command for lift the Trim Slits
				bool SlitEnabledAx1 = enableEnabled && ssState.getShaftStatusPlunged(Slit1);
				bool SlitEnabledAx2 = enableEnabled && ssState.getShaftStatusPlunged(Slit2);
				o_socket.Send(Network::MSG_PAG_REF,L"%s.SlitterAx1.Enabled=%s", formName, String::Bool2Alpha(SlitEnabledAx1));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.SlitterAx2.Enabled=%s", formName, String::Bool2Alpha(SlitEnabledAx2));

				bool sxTrimSlitEnabledAx1 = enableEnabled && ssState.getShaftStatusPlunged(Slit1) && ssState.getSetupSituation(0).getTrimSlitFlag(Slit1,true);
				bool dxTrimSlitEnabledAx1 = enableEnabled && ssState.getShaftStatusPlunged(Slit1) && ssState.getSetupSituation(0).getTrimSlitFlag(Slit1,false);
				bool sxTrimSlitEnabledAx2 = enableEnabled && ssState.getShaftStatusPlunged(Slit2) && ssState.getSetupSituation(0).getTrimSlitFlag(Slit2,true);
				bool dxTrimSlitEnabledAx2 = enableEnabled && ssState.getShaftStatusPlunged(Slit2) && ssState.getSetupSituation(0).getTrimSlitFlag(Slit2,false);
				o_socket.Send(Network::MSG_PAG_REF,L"%s.TrimSlitterSxAx1.Enabled=%s", formName, String::Bool2Alpha(sxTrimSlitEnabledAx1));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.TrimSlitterDxAx1.Enabled=%s", formName, String::Bool2Alpha(dxTrimSlitEnabledAx1));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.TrimSlitterSxAx2.Enabled=%s", formName, String::Bool2Alpha(sxTrimSlitEnabledAx2));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.TrimSlitterDxAx2.Enabled=%s", formName, String::Bool2Alpha(dxTrimSlitEnabledAx2));

				o_socket.Send(Network::MSG_PAG_REF,L"%s.TrimSlitterSxAx1.Active=%s", formName, String::Bool2Alpha(sxTrimSlitEnabledAx1 && ssState.getFunctionalityStatusSxTrimSlitRemoved() ));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.TrimSlitterDxAx1.Active=%s", formName, String::Bool2Alpha(dxTrimSlitEnabledAx1 && ssState.getFunctionalityStatusDxTrimSlitRemoved()));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.TrimSlitterSxAx2.Active=%s", formName, String::Bool2Alpha(sxTrimSlitEnabledAx2 && ssState.getFunctionalityStatusSxTrimSlitRemoved()));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.TrimSlitterDxAx2.Active=%s", formName, String::Bool2Alpha(dxTrimSlitEnabledAx2 && ssState.getFunctionalityStatusDxTrimSlitRemoved()));

				bool webFlapDown=ssState.getFunctionalityStatusWebFlapDown();
        //agrassi_FI25-11-2009_1
				//o_socket.Send(Network::MSG_PAG_REF,L"%s.WebFlap.Text=§%S", formName, (webFlapDown?stringer(SYCS_18):stringer(SYCS_17)));
        //agrassi_FI26-08-2010_1
        //agrassi_FI16-12-2010_1
        //o_socket.Send(Network::MSG_PAG_REF,L"%s._SetWebFlapText(S[%S], S[%S])", formName,(webFlapDown?stringer(SYCS_18):stringer(SYCS_17)), (webFlapDown?stringer(SYCS_65):stringer(SYCS_66)));
        //o_socket.Send(Network::MSG_PAG_REF,L"%s._SetWebFlapText(S[%S], S[%S])", formName,(webFlapDown?stringer(SYCS_18):stringer(SYCS_17)), (webFlapDown?stringer(SYCS_65):stringer(SYCS_66)));
        o_socket.Send(Network::MSG_PAG_REF,L"%s._SetWebFlapText(S[%S], S[%S])", formName,(webFlapDown?stringer(SYCS_18):stringer(SYCS_17)), (webFlapDown?stringer(SYCS_66):stringer(SYCS_65)));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.WebFlap.Active=%s", formName, String::Bool2Alpha(webFlapDown));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.LeftLimitMachineMove.Busy=%s", formName, String::Bool2Alpha(ssState.getSensorStatusMaxLeftPosition()));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.RightLimitMachineMove.Busy=%s", formName, String::Bool2Alpha(ssState.getSensorStatusMaxRightPosition()));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.ExternalLeftMachineMove.Busy=%s", formName, String::Bool2Alpha(ssState.getSensorStatusLeftOuterPhotoeye()));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.ExternalRightMachineMove.Busy=%s", formName, String::Bool2Alpha(ssState.getSensorStatusRightOuterPhotoeye()));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.InternalLeftMachineMove.Busy=%s", formName, String::Bool2Alpha(ssState.getSensorStatusLeftInnerPhotoeye()));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.InternalRightMachineMove.Busy=%s", formName, String::Bool2Alpha(ssState.getSensorStatusRightInnerPhotoeye()));
				//agrassi_FI24-09-08_2
				//o_socket.Send(Network::MSG_PAG_REF,L"%s.RecoverPositioning.Enabled=%s", formName, String::Bool2Alpha(ssState.getMachineStatusPositioningFailed() && pushEnabled && ssState.getEnableStatusSlitPhotoeyeEnabled(Slit1) && ssState.getEnableStatusSlitPhotoeyeEnabled(Slit2)));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.RecoverPositioning.Enabled=%s", formName, String::Bool2Alpha(ssState.getMachineStatusPositioningFailed() && pushEnabled));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.MoveMachineLeft.Enabled=%s", formName, String::Bool2Alpha(!ssState.getFunctionalityStatusEdgeGuideAuto() && pushEnabled));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.MoveMachineRight.Enabled=%s", formName, String::Bool2Alpha(!ssState.getFunctionalityStatusEdgeGuideAuto() && pushEnabled));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.OpenTrimChutes.Enabled=%s", formName, String::Bool2Alpha(pushEnabled));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.CloseTrimChutes.Enabled=%s", formName, String::Bool2Alpha(pushEnabled));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.ManualAutomaticMachineMove.Enabled=%s", formName, String::Bool2Alpha(pushEnabled));
				//agrassi_FI08-10-08
				//o_socket.Send(Network::MSG_PAG_REF,L"%s.WebBlow.Enabled=%s", formName, String::Bool2Alpha(pushEnabled));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.WebFlap.Enabled=%s", formName, String::Bool2Alpha(pushEnabled));
				bool genEnableLoadTeflon=pushEnabled && enableEnabled && (!ssState.getAOCStatusApproaching()) && (!ssState.getAOCStatusRunning()) && (!ssState.getSetupStatusCurrentPositioning()) && (ssState.getMachineStatusResetDone());
				o_socket.Send(Network::MSG_PAG_REF,L"%s.LoadTeflon0.Enabled=%s", formName, String::Bool2Alpha(genEnableLoadTeflon && (!ssState.getShaftStatusPlunged(Slit1))));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.LoadTeflon1.Enabled=%s", formName, String::Bool2Alpha(genEnableLoadTeflon && (!ssState.getShaftStatusPlunged(Slit2))));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.JamupPhotoeye.Enabled=%s", formName, String::Bool2Alpha(enableEnabled));
				//o_socket.Send(Network::MSG_PAG_REF,L"%s.PhotoeyeSlitShaft.Enabled=%s", formName, String::Bool2Alpha(enableEnabled));
				//o_socket.Send(Network::MSG_PAG_REF,L"%s.SlowPositioning.Enabled=%s", formName, String::Bool2Alpha(enableEnabled));
				//agrassi_FI07-04-08_1
				//bool enableForceScan=(!ssState.getShaftStatusPositioning(Slit1)) && (!ssState.getShaftStatusPositioning(Slit2)) && ssState.getMachineStatusResetDone() && (ssState.getMachineStatusStarted());
				bool enableForceScan=(!ssState.getShaftStatusPositioning(Slit1)) && (!ssState.getShaftStatusPositioning(Slit2)) && ssState.getMachineStatusResetDone() && (ssState.getMachineStatusStarted()) &&
															(!ssState.getAOCStatusApproaching()) && (!ssState.getAOCStatusRunning()) && (!ssState.getMachineStatusScanRunning());
				//AGrassi_FI06/12/2007_1
				//o_socket.Send(Network::MSG_PAG_REF,L"%s.ForceScan.Enabled=%s", formName, String::Bool2Alpha(pushEnabled && enableForceScan));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.ForceScan.Enabled=%s", formName, String::Bool2Alpha(pushEnabled && enableEnabled && enableForceScan));
				//RFAZZI_FI28/08/2008_1.Bug Fix. Added more control for enable the ForceReset button on twin400 console page.
				//bool enableForceReset=(!ssState.getMachineStatusRunning()) && (!ssState.getMachineStatusPositioning()) && (!ssState.getMachineStatusResetRunning()) && (!ssState.getMachineStatusScanRunning()) && (!ssState.getMachineStatusPositioningFailed());
				bool enableForceReset=(!ssState.getMachineStatusRunning()) && (!ssState.getMachineStatusPositioning()) && 
															(!ssState.getMachineStatusResetRunning()) && (!ssState.getMachineStatusScanRunning()) && 
															(!ssState.getMachineStatusPositioningFailed()) &&
															(!ssState.getAOCStatusRunning() ) && (!ssState.getAOCStatusApproaching());
				o_socket.Send(Network::MSG_PAG_REF,L"%s.ForceReset.Enabled=%s", formName, String::Bool2Alpha(pushEnabled && enableForceReset));
        bool manualAOCAllowed=pmac400 && ssState.getSetupStatusCurrentPlunged() && ssState.getSetupStatusNextPositionedPart1() && 
                              ssState.getMachineStatusStarted() && (!ssState.getMachineStatusRunning()) && ((!ssState.getTaskStatusRemoteControl()) || (!ssState.getTaskStatusSupervisorComm()));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.ManualAOC.Enabled=%s", formName, String::Bool2Alpha(pushEnabled && manualAOCAllowed));
				//AGrassi_FI06/12/2007_1
				//o_socket.Send(Network::MSG_PAG_REF,L"%s.LiftAllShafts.Enabled=%s", formName, String::Bool2Alpha(pushEnabled && liftAllShaftsAllowed));
        //agrassi_FI26-11-2009_1
        //bool liftAllShaftsAllowed=ssState.getSetupStatusCurrentPlunged() && (!ssState.getMachineStatusRunning()) && (!ssState.getMachineStatusPositioning());
				//o_socket.Send(Network::MSG_PAG_REF,L"%s.LiftAllShafts.Enabled=%s", formName, String::Bool2Alpha(pushEnabled && enableEnabled && liftAllShaftsAllowed));
        //agrassi_FI03-01-2010_1
        //bool liftAllShaftsAllowed=ssState.getMachineStatusStarted() && (!ssState.getMachineStatusRunning()) && (!ssState.getMachineStatusPositioning());
        //agrassi_FI11-03-2010_1
        bool Twin400_V1=cfg.slitterScorer.isTwin400_V1();
        //bool liftAllShaftsAllowed=((ssState.getMachineStatusStarted() && ssState.getSetupStatusCurrentPlunged()) || (ssState.getMachineStatusRestored() && (!ssState.getSetupStatusCurrentPlunged()))) && 
        //        (!ssState.getMachineStatusRunning()) && (!ssState.getMachineStatusPositioning());
        //agrassi_FI06-01-2010_1
        //bool liftAllShaftsAllowed=((ssState.getMachineStatusStarted() && ssState.getSetupStatusCurrentPlunged()) || (Twin400_V1 && ssState.getMachineStatusRestored() && (!ssState.getSetupStatusCurrentPlunged()))) && 
        //        (!ssState.getMachineStatusRunning()) && (!ssState.getMachineStatusPositioning());
        //MLAMIONI_FI10/05/11 - rivista modifica 'agrassi_FI06-01-2010_1' perchè non veniva più abilitato il pulsante 'LiftAllShafts'
        /*
        bool liftAllShaftsAllowed=((ssState.getMachineStatusStarted() && ssState.getSetupStatusCurrentPlunged()) || (Twin400_V1 && ssState.getMachineStatusRestored() && (!ssState.getSetupStatusCurrentPlunged()))) && 
                (!ssState.getMachineStatusRunning()) && (!ssState.getMachineStatusPositioning() && (ssState.getAOCStatusRunning()));
        */
        bool liftAllShaftsAllowed=((ssState.getMachineStatusStarted() && ssState.getSetupStatusCurrentPlunged()) || (Twin400_V1 && ssState.getMachineStatusRestored() && (!ssState.getSetupStatusCurrentPlunged()))) && 
                (!ssState.getMachineStatusRunning()) && (!ssState.getMachineStatusPositioning() && (!ssState.getAOCStatusRunning()));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.LiftAllShafts.Enabled=%s", formName, String::Bool2Alpha(pushEnabled && enableEnabled && liftAllShaftsAllowed));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.LiftAllShafts.Active=%s", formName, String::Bool2Alpha(!ssState.getSetupStatusCurrentPlunged()));
        //o_socket.Send(Network::MSG_PAG_REF,L"%s.LiftAllShafts.Text=§%S", formName, (ssState.getSetupStatusCurrentPlunged()?stringer(SYCS_67):stringer(SYCS_68)));
        o_socket.Send(Network::MSG_PAG_REF,L"%s.LiftAllShafts.Text=§%S", formName, (ssState.getSetupStatusCurrentPlunged() || (!Twin400_V1) ? stringer(SYCS_67):stringer(SYCS_68)));

        for (Shaft sh=Score1; sh<=ScoreAux; sh++)
				{
          //agrassi_FI05-01-2011_1
					//bool enableGapChange=pushEnabled && ssState.getShaftStatusPlunged(sh) && (!ssState.getAOCStatusApproaching()) && (!ssState.getAOCStatusRunning());
					bool enableGapChange=pushEnabled && ssState.getShaftStatusPlunged(sh) && (!ssState.getAOCStatusRunning());
				  o_socket.Send(Network::MSG_PAG_REF,L"%s.ScoreGap%d.Enabled=%s", formName, sh-Score1+1, String::Bool2Alpha(enableGapChange));
				  o_socket.Send(Network::MSG_PAG_REF,L"%s.ModifyScoreGap%d.Enabled=%s", formName, sh-Score1+1, String::Bool2Alpha(enableGapChange));
				}
				//AGrassi_FI06/12/2007_1
			  o_socket.Send(Network::MSG_PAG_REF,L"%s._SetTeflonPossible(B[%s], S[%S])", formName, String::Bool2Alpha(ssState.getTaskStatusTeflonCyclePossible()), getTeflonReasonCode(&ssState));
			  o_socket.Send(Network::MSG_PAG_REF,L"%s.TeflonCycleStart.Enabled=%s", formName, String::Bool2Alpha(pushEnabled && enableEnabled));
        o_socket.Send(Network::MSG_PAG_REF,L"%s.TeflonCycleStart.BackColor=%s", formName, (ssState.getTaskStatusTeflonCyclePossible() || (!pushEnabled) || (!enableEnabled))?L"White":L"Red");

				//agrassi_FI25-08-08_2
				for (Shaft sh=Slit1; sh<=ScoreAux; sh++)
				{
					wchar_t color[40]=L"Silver";
					if (ssState.getShaftStatusPlunged(sh))
						wcscpy(color, L"SteelBlue");
					else if (ssState.getShaftStatusPositioning(sh))
						wcscpy(color, L"Gold");
					else if (ssState.getShaftStatusPositioned(sh))
						wcscpy(color, L"MediumSeaGreen");
				  o_socket.Send(Network::MSG_PAG_REF,L"%s.Shaft%d.BackColor=%s", formName, (int)(sh+1), color);
				}
				if (!allCases) break;
			}
			case State::C_SMState::SS_ChutesAndGap:
        for (Shaft sh=Score1; sh<=ScoreAux; sh++)
  				o_socket.Send(Network::MSG_PAG_REF,L"%s.ScoreGap%d.Value=%d",formName, sh-Score1+1, ssState.getScoreGap(sh));
				o_socket.Send(Network::MSG_PAG_REF,L"%s._SetChutesSpan(I[%d])", formName, ssState.getChuteSpan()/TH_PER_MM);
				if (!allCases) break;
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_console page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_Twin400Group::OrderDetails(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_orderDetails";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;
	while(!end_OrderDetails)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;	
      case State::C_SMState::SS_TaskStatus:
        o_socket.Send(Network::MSG_PAG_REF,L"%s._SetOnline(b[%s])", formName, String::Bool2Alpha(ssState.getTaskStatusRemoteControl() && ssState.getTaskStatusSupervisorComm()));
				if (!allCases) break;
			case State::C_SMState::SS_MachineStatus:
				o_socket.Send(Network::MSG_PAG_REF,L"%s._SetCurrentUnchangeable(b[%s])", formName, String::Bool2Alpha(ssState.getMachineStatusRunning()));
				if (!allCases) break;
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_orderDetails page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_Twin400Group::OrdersList(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_ordersList";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;

	while(!end_OrdersList)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;	
      case State::C_SMState::SS_TaskStatus:
				o_socket.Send(Network::MSG_PAG_REF,L"%s._SetOnline(b[%s])", formName, String::Bool2Alpha(ssState.getTaskStatusRemoteControl() && ssState.getTaskStatusSupervisorComm()));
				if (!allCases) break;
			case State::C_SMState::SS_MachineSetup:
				o_socket.Send(Network::MSG_PAG_REF,L"%s._RefreshList(I[%d],I[%d],I[%d],I[%d],I[%d],I[%d])", formName, 
					ssState.getSetupID(0), ssState.getModID(0), ssState.getSetupID(1), ssState.getModID(1), ssState.getSetupID(2), ssState.getModID(2));
				if (!allCases) break;
      //agrassi_FI09-11-2009_1
			//case State::C_SMState::SS_SetupSituation:
			//	o_socket.Send(Network::MSG_PAG_REF,L"%s._SetSetupSituation(b[%s],b[%s],b[%s],b[%s],b[%s],b[%s])", formName, 
			//		String::Bool2Alpha(ssState.getSetupSituationWideToNarrow(0) && ssState.getSetupSituationValid(0)), 
			//		String::Bool2Alpha(ssState.getSetupSituationWideToNarrow(1) && ssState.getSetupSituationValid(1)), 
			//		String::Bool2Alpha(ssState.getSetupSituationWideToNarrow(2) && ssState.getSetupSituationValid(2)),
			//		String::Bool2Alpha(ssState.getSetupSituationNarrowToWide(0) && ssState.getSetupSituationValid(0)), 
			//		String::Bool2Alpha(ssState.getSetupSituationNarrowToWide(1) && ssState.getSetupSituationValid(1)),
			//		String::Bool2Alpha(ssState.getSetupSituationNarrowToWide(2) && ssState.getSetupSituationValid(2)));
			//	if (!allCases) break;
			case State::C_SMState::SS_MachineStatus:
				o_socket.Send(Network::MSG_PAG_REF,L"%s._SetCurrentUnchangeable(b[%s])", formName, String::Bool2Alpha(ssState.getMachineStatusRunning()));
				if (!allCases) break;
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_ordersList page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_Twin400Group::PastOrders(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_pastOrders";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;

	while(!end_PastOrders)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;	
      case State::C_SMState::SS_TaskStatus:
        o_socket.Send(Network::MSG_PAG_REF,L"%s._SetOnline(b[%s])", formName, String::Bool2Alpha(ssState.getTaskStatusRemoteControl() && ssState.getTaskStatusSupervisorComm()));
				if (!allCases) break;
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_pastOrders page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_Twin400Group::Flutes(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"twin400_flutes._Show()");

	while(!end_Flutes)
		Sleep(1000);
	
	logger.Fine(__FUNCTION__, L"Terminated twin400_flutes page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_Twin400Group::Quality(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"twin400_quality._Show()");

	while(!end_Quality)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated twin400_quality page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_Twin400Group::Faults(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"twin400_faults._Show()");

	while(!end_Faults)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated twin400_faults page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_Twin400Group::Tasks(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_tasks";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);

	while(!end_Tasks)
		Sleep(5000);

	logger.Fine(__FUNCTION__, L"Terminated twin400_tasks page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_Twin400Group::Plunges(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_plunges";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);

	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;

	while(!end_Plunges)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;	
      case State::C_SMState::SS_MachineStatus:
      case State::C_SMState::SS_TaskStatus:
				for (int i=0; i<MAX_NUM_SCORE_SHAFTS; i++)
				{
					o_socket.Send(Network::MSG_PAG_REF,L"%s.btn_StartCalibration%d.Enabled=%s", formName, i+1, String::Bool2Alpha(ssState.getMaintenanceStatusMachineInCalibration()));
					if (!ssState.getMaintenanceStatusMachineInCalibration())
						o_socket.Send(Network::MSG_PAG_REF,L"%s.SetEnable(B[%s],I[%d])", formName, L"false", i);
				}
				bool NoCommPlc=(!ssState.getTaskStatusPLCComm()) || (!ssState.getTaskStatusPLCRun());
        o_socket.Send(Network::MSG_PAG_REF,L"%s.lbl_MachineNotRunning.BackColor=%s", formName, (ssState.getMachineStatusRunning() || NoCommPlc?L"Red":L"LightGreen"));
        o_socket.Send(Network::MSG_PAG_REF,L"%s.lbl_BoardNotPresent.BackColor=%s", formName, (ssState.getMachineStatusBoardPresent() || NoCommPlc?L"Red":L"LightGreen"));
        o_socket.Send(Network::MSG_PAG_REF,L"%s.lbl_CalibrationMode.BackColor=%s", formName, (ssState.getMaintenanceStatusMachineInCalibration()?L"LightBlue":L"White"));
			o_socket.Send(Network::MSG_PAG_REF,L"%s.lbl_MachineNotRunning.Visible=%s", formName, String::Bool2Alpha(!ssState.getMaintenanceStatusMachineInCalibration()));
			o_socket.Send(Network::MSG_PAG_REF,L"%s.lbl_BoardNotPresent.Visible=%s", formName, String::Bool2Alpha(!ssState.getMaintenanceStatusMachineInCalibration()));
				if (!allCases) break;
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_plunges page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_Twin400Group::MachineSettings(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_machineSettings";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;
	while(!end_MachineSettings)
	{
		int ver, verForValidation;
		F_GetConfigVersions(ver, verForValidation);
		o_socket.Send(Network::MSG_PAG_REF,L"%s.ConfigVer.Text=%d", formName, ver);
		o_socket.Send(Network::MSG_PAG_REF,L"%s.ConfigVerForValidation.Text=%d", formName, verForValidation);
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;	
			case State::C_SMState::SS_TaskStatus:
				o_socket.Send(Network::MSG_PAG_REF,L"%s._SetPLCComm(b[%s])", formName, String::Bool2Alpha(ssState.getTaskStatusPLCComm()));
				if (!allCases) break;
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_machineSettings page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_Twin400Group::Misc(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"twin400_misc._Show()");

	while(!end_Misc)
		Sleep(1000);
	
	logger.Fine(__FUNCTION__, L"Terminated twin400_misc page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_Twin400Group::Alarm1(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"twin400_alarm1._Show()");

	while(!end_Alarm1)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated twin400_alarm1 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_Twin400Group::Alarm4(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"twin400_alarm4._Show()");

	while(!end_Alarm4)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated twin400_alarm4 page threads");

	return ERROR_SUCCESS;
}
    
//RFAZZI_FI26/01/2011_2 Aggiunta gestione Warning su grafica Twin400 di Syncro. 
DWORD WINAPI C_Twin400Group::Warning1(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"twin400_warning1._Show()");

	while(!end_Warning1)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated twin400_warning1 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_Twin400Group::Warning4(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"twin400_warning4._Show()");

	while(!end_Warning4)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated twin400_warning4 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_Twin400Group::Maintenance(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_maintenance";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum myEvent=State::C_SMState::SS_RefreshAll;
	while(!end_Maintenance)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);

    //ABELLO_FI25/07/2008_1 aggiunte informazioni di debug sulla pagina manutenzione del Twin400
    o_socket.Send(Network::MSG_PAG_REF,L"%s.chk_PLCComm.Checked=%s", formName, String::Bool2Alpha(ssState.getTaskStatusPLCComm()));
    o_socket.Send(Network::MSG_PAG_REF,L"%s.chk_PLCRun.Checked=%s", formName, String::Bool2Alpha(ssState.getTaskStatusPLCRun()));
    o_socket.Send(Network::MSG_PAG_REF,L"%s.chk_MachineInMaintenance.Checked=%s", formName, String::Bool2Alpha(ssState.getMaintenanceStatusMachineInMaintenance()));
    o_socket.Send(Network::MSG_PAG_REF,L"%s.chk_MachineStatusStarted.Checked=%s", formName, String::Bool2Alpha(ssState.getMachineStatusStarted()));
    o_socket.Send(Network::MSG_PAG_REF,L"%s.chk_MachineStatusNotPositioning.Checked=%s", formName, String::Bool2Alpha(!ssState.getMachineStatusPositioning()));

		switch(myEvent)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;
			case State::C_SMState::SS_MachineStatus:
			case State::C_SMState::SS_TaskStatus:
			{
        bool pushEnabled=(ssState.getTaskStatusPLCComm() && ssState.getTaskStatusPLCRun());
				bool mainten=ssState.getMaintenanceStatusMachineInMaintenance();
				o_socket.Send(Network::MSG_PAG_REF,L"%s.Maintenance.Active=%s", formName, String::Bool2Alpha(mainten));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.Maintenance.Text=§%S", formName, (mainten?stringer(SYCS_19):stringer(SYCS_20)));
        o_socket.Send(Network::MSG_PAG_REF,L"%s.Maintenance.Enabled=%s", formName, String::Bool2Alpha(pushEnabled && (!ssState.getMachineStatusRunning()) && (!ssState.getMachineStatusBoardPresent()) && (!ssState.getMachineStatusPositioning())));
        for (int i=1; i<=8; i++)
          o_socket.Send(Network::MSG_PAG_REF,L"%s.MaintenancePositioning%d.Enabled=%s", formName, i, String::Bool2Alpha(pushEnabled && mainten && ssState.getMachineStatusStarted() && (!ssState.getMachineStatusPositioning())));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.SpinningForGrinding.Visible=%s", formName, String::Bool2Alpha(ssState.getMaintenanceStatusMachinePositionedForGrinding()));
				bool NoCommPlc=(!ssState.getTaskStatusPLCComm()) || (!ssState.getTaskStatusPLCRun());
			o_socket.Send(Network::MSG_PAG_REF,L"%s.lbl_MachineNotRunning_YN.BackColor=%s", formName, (ssState.getMachineStatusRunning() || NoCommPlc?L"Red":L"LightGreen"));
			o_socket.Send(Network::MSG_PAG_REF,L"%s.lbl_BoardNotPresent_YN.BackColor=%s", formName, (ssState.getMachineStatusBoardPresent() || NoCommPlc?L"Red":L"LightGreen"));
			//AGrassi_FI06/12/2007_1
			o_socket.Send(Network::MSG_PAG_REF,L"%s.lbl_MachineNotPositioning_YN.BackColor=%s", formName, (ssState.getMachineStatusPositioning() || NoCommPlc?L"Red":L"LightGreen"));
			o_socket.Send(Network::MSG_PAG_REF,L"%s.lbl_MachineNotRunning_YN.Visible=%s", formName, String::Bool2Alpha(!ssState.getMaintenanceStatusMachineInMaintenance()));
			o_socket.Send(Network::MSG_PAG_REF,L"%s.lbl_BoardNotPresent_YN.Visible=%s", formName, String::Bool2Alpha(!ssState.getMaintenanceStatusMachineInMaintenance()));
			//AGrassi_FI06/12/2007_1
			o_socket.Send(Network::MSG_PAG_REF,L"%s.lbl_MachineNotPositioning_YN.Visible=%s", formName, String::Bool2Alpha(!ssState.getMaintenanceStatusMachineInMaintenance()));
			
			bool positionedSlits=false, positioning=false;
			for (Shaft sh=Slit1; sh<=Slit2; sh++)
			{
				o_socket.Send(Network::MSG_PAG_REF,L"%s.toolDetail.SetPositioning(B[%s], I[%d])", formName, String::Bool2Alpha(ssState.getShaftStatusPositioning(sh)), sh);
				positionedSlits=positionedSlits || ssState.getShaftStatusPositioned(sh);
				positioning=positioning || ssState.getShaftStatusPositioning(sh);
			}
			bool positionedScores=false;
			for (Shaft sh=Score1; sh<=ScoreAux; sh++)
				positionedScores=positionedScores || ssState.getShaftStatusPositioned(sh);
			for (int i=1; i<=8; i++)
			{
				bool positionedMachine=(positionedSlits && (i!=3)) || (positionedScores && (i==3));
				wchar_t boolVal[10];
				if (mainten && (ssState.getSetupID(0)==9999) && (ssState.getModID(0)==i) && positionedMachine)
					wcscpy(boolVal, L"true");
				else
					wcscpy(boolVal, L"false");
				o_socket.Send(Network::MSG_PAG_REF,L"%s.MaintenancePositioning%d.Active=%s", formName, i, boolVal);
				if (mainten && (ssState.getSetupID(0)==9999) && (ssState.getModID(0)==i) && positioning)
					wcscpy(boolVal, L"true");
				else
					wcscpy(boolVal, L"false");
				o_socket.Send(Network::MSG_PAG_REF,L"%s.MaintenancePositioning%d.HighLightEnable=%s", formName, i, boolVal);
			}
			if (!allCases) break;
			}
		}
		allCases=false;
		myEvent=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_maintenance page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_Twin400Group::Simulations(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_simulations";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;
	while(!end_Simulations)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;
			case State::C_SMState::SS_MachineStatus:
			case State::C_SMState::SS_TaskStatus:
			{
        bool pushEnabled=(!ssState.getMachineStatusInEmergency()) && ssState.getTaskStatusPLCComm() && ssState.getTaskStatusPLCRun();
				bool simul=ssState.getMaintenanceStatusMachineInSimulation();
				o_socket.Send(Network::MSG_PAG_REF,L"%s.Simulation.Active=%s", formName, String::Bool2Alpha(simul));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.Simulation.Text=%s", formName, (simul?L"Stop Simulation":L"Start Simulation"));
				if (simul)
				{
					o_socket.Send(Network::MSG_PAG_REF,L"%s.txt_SimSpeed.Value=%d", formName, ssState.getSimulationData().speed);
					o_socket.Send(Network::MSG_PAG_REF,L"%s.chk_Grinding.Checked=%s", formName, String::Bool2Alpha(ssState.getSimulationData().enableGrinding));
					o_socket.Send(Network::MSG_PAG_REF,L"%s.chk_Lube.Checked=%s", formName, String::Bool2Alpha(ssState.getSimulationData().enableLube));
				}

  			bool aocSimul=ssState.getMaintenanceStatusMachineInAOCSimulation();
				o_socket.Send(Network::MSG_PAG_REF,L"%s.AOCSimulation.Active=%s", formName, String::Bool2Alpha(aocSimul));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.AOCSimulation.Text=%s", formName, (aocSimul?L"Stop AOC Simulation":L"Start AOC Simulation"));
        o_socket.Send(Network::MSG_PAG_REF,L"%s.tlb_TimeUntilAOC.Value=%d", formName, ssState.getCountDownAOCSimulation());
        o_socket.Send(Network::MSG_PAG_REF,L"%s.tlb_TimeUntilAOC.Visible=%s", formName, String::Bool2Alpha(ssState.getMaintenanceStatusMachineInAOCSimulation()));
				if (aocSimul)
				{
					o_socket.Send(Network::MSG_PAG_REF,L"%s.txt_Cycle.Value=%d", formName, ssState.getSimulationData().aocPeriod);
					o_socket.Send(Network::MSG_PAG_REF,L"%s.chk_Nogap.Checked=%s", formName, String::Bool2Alpha(ssState.getSimulationData().noGapAOC));
				}
				bool simulationEnabled=((!ssState.getMachineStatusRunning()) && ssState.getMachineStatusRestored()) || ssState.getMaintenanceStatusMachineInSimulation();
				o_socket.Send(Network::MSG_PAG_REF,L"%s.Simulation.Enabled=%s", formName, String::Bool2Alpha(pushEnabled && simulationEnabled));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.AOCSimulation.Enabled=%s", formName, String::Bool2Alpha(pushEnabled && simul));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.txt_SimSpeed.Enabled=%s", formName, String::Bool2Alpha(!simul));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.chk_Grinding.Enabled=%s", formName, String::Bool2Alpha(!simul));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.chk_Lube.Enabled=%s", formName, String::Bool2Alpha(!simul));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.txt_Cycle.Enabled=%s", formName, String::Bool2Alpha(!aocSimul));
				o_socket.Send(Network::MSG_PAG_REF,L"%s.chk_Nogap.Enabled=%s", formName, String::Bool2Alpha(!aocSimul));
				bool NoCommPlc=(!ssState.getTaskStatusPLCComm()) || (!ssState.getTaskStatusPLCRun());
        o_socket.Send(Network::MSG_PAG_REF,L"%s.lbl_MachineNotRunning.BackColor=%s", formName, (ssState.getMachineStatusRunning() || NoCommPlc?L"Red":L"LightGreen"));
        o_socket.Send(Network::MSG_PAG_REF,L"%s.lbl_MachineRestored.BackColor=%s", formName, (ssState.getMachineStatusRestored()?L"LightGreen":L"Red"));
			o_socket.Send(Network::MSG_PAG_REF,L"%s.lbl_MachineNotRunning.Visible=%s", formName, String::Bool2Alpha(!ssState.getMaintenanceStatusMachineInSimulation()));
			o_socket.Send(Network::MSG_PAG_REF,L"%s.lbl_MachineRestored.Visible=%s", formName, String::Bool2Alpha(!ssState.getMaintenanceStatusMachineInSimulation()));
				if (!allCases) break;
			}
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_simulations page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_Twin400Group::TestInput(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_testInput";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;

	while(!end_TestInput)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;
			case State::C_SMState::SS_WaitTimeOut:
				wchar_t ioData[State::INPUT_CHANNEL_COUNT*15];
				int cnt=0;
				for (State::IOInputChannels ch=State::INLogicCabinet0; ch<=State::INFree; ch++)
					cnt+=swprintf(ioData+cnt, L"%d%s", ssState.getInputChannel(ch), Network::SEP);
				o_socket.Send(Network::MSG_PAG_REF,L"%s._RefreshChannels(S[%s])", formName, ioData);
				if (!allCases) break;
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_testInput page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_Twin400Group::TestOutput(LPVOID  lpParam)
{
	const wchar_t* formName=L"twin400_testOutput";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;

	while(!end_TestOutput)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;
			case State::C_SMState::SS_WaitTimeOut:
				wchar_t ioData[State::OUTPUT_CHANNEL_COUNT*11];
				int cnt=0;
				cnt+=swprintf(ioData+cnt, L"%s%s", String::Bool2Alpha(ssState.getMaintenanceStatusMachineInTestIO()), Network::SEP);
				for (State::IOOutputChannels ch=State::OUTLogicCabinet0; ch<=State::OUTFree2; ch++)
					cnt+=swprintf(ioData+cnt, L"%d%s", ssState.getOutputChannel(ch), Network::SEP);
				o_socket.Send(Network::MSG_PAG_REF,L"%s._RefreshChannels(S[%s])", formName, ioData);
				if (!allCases) break;
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_testOutput page threads");

	return ERROR_SUCCESS;
}