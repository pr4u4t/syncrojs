#include ".\C_DebugGroup.h"

//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

//Page Management
#include "..\C_PageManager.h"

bool C_DebugGroup::end_DebugDatabase;
bool C_DebugGroup::end_DebugINI;
bool C_DebugGroup::end_DebugTwin400Status1;
bool C_DebugGroup::end_DebugTwin400Status2;
bool C_DebugGroup::end_DebugTwin400SetupSituation;
//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
bool C_DebugGroup::end_DebugTwin400Positioning;
//agrassi_FI25-05-2011_1
bool C_DebugGroup::end_DebugTwin2;

C_DebugGroup::C_DebugGroup(void)
{
	pid_DebugPage=NULL;
	TerminateThread(false);
}

C_DebugGroup::~C_DebugGroup(void)
{
}

bool C_DebugGroup::ChooseDebugPage(String::StrType  thePageRequest)
{
		//database
	if(thePageRequest.CompareNoCase(L"debugDatabase"))
	{
		end_DebugDatabase = false;
		pid_DebugPage=::CreateThread(NULL,0,DebugDatabase,NULL,0,NULL);
		return true;
	}
		//INI
	if(thePageRequest.CompareNoCase(L"debugINI"))
	{
		end_DebugINI = false;
		pid_DebugPage=::CreateThread(NULL,0,DebugINI,NULL,0,NULL);
		return true;
	}

		//DebugTwin400Status1
	if(thePageRequest.CompareNoCase(L"debugTwin400Status1"))
	{
		end_DebugTwin400Status1 = false;
		pid_DebugPage=::CreateThread(NULL,0,DebugTwin400Status1,NULL,0,NULL);
		return true;
	}

		//DebugTwin400Status2
	if(thePageRequest.CompareNoCase(L"debugTwin400Status2"))
	{
		end_DebugTwin400Status2 = false;
		pid_DebugPage=::CreateThread(NULL,0,DebugTwin400Status2,NULL,0,NULL);
		return true;
	}

		//DebugTwin400SetupSituation
	if(thePageRequest.CompareNoCase(L"debugTwin400SetupSituation"))
	{
		end_DebugTwin400SetupSituation = false;
		pid_DebugPage=::CreateThread(NULL,0,DebugTwin400SetupSituation,NULL,0,NULL);
		return true;
	}

	//Positioning
	if(thePageRequest.CompareNoCase(L"debugTwin400_Positioning"))
	{
		end_DebugTwin400Positioning=false;
		pid_DebugPage=::CreateThread(NULL,0,DebugTwin400Positioning,NULL,0,NULL);
		return true;
	}
  
  //agrassi_FI25-05-2011_1
	//Twin2
	if(thePageRequest.CompareNoCase(L"debugTwin2"))
	{
		end_DebugTwin2=false;
		pid_DebugPage=::CreateThread(NULL,0,DebugTwin2,NULL,0,NULL);
		return true;
	}
  return false;
}

void C_DebugGroup::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_DebugPage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_DebugPage,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread DebugPage ... %d", ::GetLastError());
		}
	}
	else
	{
		end_DebugDatabase = true;
		end_DebugINI = true;
		end_DebugTwin400Status1 = true;
		end_DebugTwin400Status2 = true;
		end_DebugTwin400SetupSituation = true;
		//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
		end_DebugTwin400Positioning = true;
    //agrassi_FI25-05-2011_1
    end_DebugTwin2=true;
	}
}

DWORD C_DebugGroup::SuspendThread(void)
{
	if(pid_DebugPage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_DebugPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page Debug Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_DebugGroup::ResumeThread(void)
{
	if(pid_DebugPage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_DebugPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page Debug Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}


DWORD WINAPI C_DebugGroup::DebugDatabase(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"debug_debugDatabase._Show()");

	while(!end_DebugDatabase)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated debug_debugDatabase page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DebugGroup::DebugINI(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"debug_debugINI._Show()");
	
	while(!end_DebugINI)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated debug_debugINI page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DebugGroup::DebugTwin400Status1(LPVOID  lpParam)
{
  const wchar_t* formName=L"debug_debugTwin400Status1";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;
	while(!end_DebugTwin400Status1)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;
			case State::C_SMState::SS_MachineStatus:
			case State::C_SMState::SS_ChutesAndGap:
				wchar_t stData[State::STATUS_CHANNEL_COUNT*15];
				int cnt=0;
				cnt+=swprintf(stData+cnt, L"%d%s", ssState.getShaftStatus(), Network::SEP);
				cnt+=swprintf(stData+cnt, L"%d%s", ssState.getMachineStatus(), Network::SEP);
				cnt+=swprintf(stData+cnt, L"%d%s", ssState.getSetupStatus(), Network::SEP);
				cnt+=swprintf(stData+cnt, L"%d%s", ssState.getMaintenanceStatus(), Network::SEP);
				cnt+=swprintf(stData+cnt, L"%d%s", ssState.getResetStatus(), Network::SEP);
				cnt+=swprintf(stData+cnt, L"%d%s", ssState.getSensorStatus(), Network::SEP);
				cnt+=swprintf(stData+cnt, L"%d%s", ssState.getOrderChangeStatus(), Network::SEP);
				cnt+=swprintf(stData+cnt, L"%d%s", ssState.getEnableStatus(), Network::SEP);
				cnt+=swprintf(stData+cnt, L"%d%s", ssState.getCommandStatus(), Network::SEP);
				cnt+=swprintf(stData+cnt, L"%d%s", ssState.getFunctionalityStatus(), Network::SEP);
				cnt+=swprintf(stData+cnt, L"%d%s", ssState.getPlungedSlits(Slit1), Network::SEP);
				cnt+=swprintf(stData+cnt, L"%d", ssState.getPlungedSlits(Slit2));
				o_socket.Send(Network::MSG_PAG_REF,L"%s._RefreshChannels(S[%s])", formName, stData);
				if (!allCases) break;
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated debug_debugTwin400Status1 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DebugGroup::DebugTwin400Status2(LPVOID  lpParam)
{
  const wchar_t* formName=L"debug_debugTwin400Status2";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;
	while(!end_DebugTwin400Status2)
	{
		const int ITEM_COUNT=52;
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		wchar_t stData[ITEM_COUNT*15];
		int cnt=0;
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getRequests(), Network::SEP);
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getLastMessNum(), Network::SEP);
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getLastRealTimeMessNum(), Network::SEP);
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getTrxError(), Network::SEP);
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getTrxErrorCode(), Network::SEP);
		for (int ind=0; ind<State::SS_SETUPS; ind++)
		{
			cnt+=swprintf(stData+cnt, L"%d%s", ssState.getSetupID(ind), Network::SEP);
			cnt+=swprintf(stData+cnt, L"%d%s", ssState.getModID(ind), Network::SEP);
		}
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getChuteSpan(), Network::SEP);
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getSpeed(), Network::SEP);
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getGrindDuration(), Network::SEP);
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getLubeDuration(), Network::SEP);
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getGrindToolMeters(), Network::SEP);
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getWebDir2LevRawLowMask(), Network::SEP);
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getWebDir2LevRawHighMask(), Network::SEP);
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getWebDir3LevRawLowMask(), Network::SEP);
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getWebDir3LevRawHighMask(), Network::SEP);
		//AGrassi_FI06/12/2007_1
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getTeflonCounter(), Network::SEP);
		for (Shaft sh=Score1; sh<=ScoreAux; sh++)
		{
			cnt+=swprintf(stData+cnt, L"%d%s", ssState.getScoreAngle(sh)*10, Network::SEP);
			cnt+=swprintf(stData+cnt, L"%d%s", ssState.getShaftGap(sh), Network::SEP);
			cnt+=swprintf(stData+cnt, L"%d%s", ssState.getActScoreGap(sh), Network::SEP);
		}
		for (Shaft sh=Slit1; sh<=Slit2; sh++)
			for (int i=0; i<MAX_SLITS_PER_SHAFT; i++)
				cnt+=swprintf(stData+cnt, L"%d%s", ssState.getSlitPositions(sh, i), Network::SEP);

		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getTaskStatus(), Network::SEP);
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getSlitPositionsStatus(), Network::SEP);
		cnt+=swprintf(stData+cnt, L"%d", ssState.getAlarms());
		o_socket.Send(Network::MSG_PAG_REF,L"%s._RefreshStatus(S[%s])", formName, stData);

		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated debug_debugTwin400Status2 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_DebugGroup::DebugTwin400SetupSituation(LPVOID  lpParam)
{
  const wchar_t* formName=L"debug_debugTwin400SetupSituation";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;
	while(!end_DebugTwin400SetupSituation)
	{
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		switch(event)
		{
			case State::C_SMState::SS_RefreshAll:
				allCases=true;
			case State::C_SMState::SS_SetupSituation:
			{
				wchar_t stData[(State::SS_SETUPS*(13+NUM_SLIT_SHAFTS*(3+MAX_SLITS_PER_SHAFT))+1)*10];
				int cnt=0;
				for (int stp = 0; stp < State::SS_SETUPS; stp++)
				{
					for (Shaft sh = Slit1; sh <= Slit2; sh++)
					{
						for (int slt = 0; slt < MAX_SLITS_PER_SHAFT; slt++)
							cnt+=swprintf(stData+cnt, L"%d%s", ssState.getSetupSituationSlitPosition(stp, sh, slt), Network::SEP);
						cnt+=swprintf(stData+cnt, L"0x%04X%s", ssState.getSetupSituationPlungedSlitMask(stp, sh), Network::SEP);
						cnt+=swprintf(stData+cnt, L"0x%04X%s", ssState.getSetupSituationSepSlitMask(stp, sh), Network::SEP);
						cnt+=swprintf(stData+cnt, L"0x%04X%s", ssState.getSetupSituationDummySlitMask(stp, sh), Network::SEP);
					}
					cnt+=swprintf(stData+cnt, L"0x%08X%s", ssState.getSetupSituationWebDir2levelMask(stp), Network::SEP);
					cnt+=swprintf(stData+cnt, L"0x%08X%s", ssState.getSetupSituationWebDir3levelMask(stp), Network::SEP);
					cnt+=swprintf(stData+cnt, L"0x%08X%s", ssState.getSetupSituationRCSSecotrMask(stp), Network::SEP);
					cnt+=swprintf(stData+cnt, L"0x%02X%s", ssState.getSetupSituationShaftMask(stp), Network::SEP);
          //agrassi_FI19-01-2011_1
					//cnt+=swprintf(stData+cnt, L"%s%s", String::Bool2Alpha(ssState.getSetupSituationWideToNarrow(stp)), Network::SEP);
					//cnt+=swprintf(stData+cnt, L"%s%s", String::Bool2Alpha(ssState.getSetupSituationNarrowToWide(stp)), Network::SEP);
          for (int i=0; i<2; i++)
					  cnt+=swprintf(stData+cnt, L"%d%s", ssState.getSetupSituationExtimPositioningTimeTwin400_UTms(stp, i), Network::SEP);
					cnt+=swprintf(stData+cnt, L"%d%s", ssState.getSetupSituationActPositioningTimeTwin400_UTms(stp), Network::SEP);
					cnt+=swprintf(stData+cnt, L"%s%s", String::Bool2Alpha(ssState.getSetupSituationCleanSlits(stp)), Network::SEP);
					cnt+=swprintf(stData+cnt, L"%s%s", String::Bool2Alpha(ssState.getSetupSituationSendToPlc(stp)), Network::SEP);
					cnt+=swprintf(stData+cnt, L"%s%s", String::Bool2Alpha(ssState.getSetupSituationFirstOfKnifeOnwards(stp)), Network::SEP);
					cnt+=swprintf(stData+cnt, L"%d%s", ssState.getSetupSituationSetupId(stp), Network::SEP);
					cnt+=swprintf(stData+cnt, L"%d%s", ssState.getSetupSituationModId(stp), Network::SEP);
					cnt+=swprintf(stData+cnt, L"%d%s", ssState.getSetupSituationConfigVersion(stp), Network::SEP);
					cnt+=swprintf(stData+cnt, L"%s%s", String::Bool2Alpha(ssState.getSetupSituationValid(stp)), Network::SEP);
				}		
				cnt+=swprintf(stData+cnt, L"DUMMY");
				o_socket.Send(Network::MSG_PAG_REF,L"%s._RefreshSetupSituation(S[%s])", formName, stData);
				if (!allCases) break;
			}
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated debug_debugTwin400SetupSituation page threads");

	return ERROR_SUCCESS;
}

//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
DWORD WINAPI C_DebugGroup::DebugTwin400Positioning(LPVOID  lpParam)
{
	const wchar_t* formName=L"debug_DebugTwin400_Positioning";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;

	while(!end_DebugTwin400Positioning)
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

	logger.Fine(__FUNCTION__, L"Terminated DebugTwin400_Positioning page threads");

	return ERROR_SUCCESS;
}

//agrassi_FI25-05-2011_1
DWORD WINAPI C_DebugGroup::DebugTwin2(LPVOID  lpParam)
{
  const wchar_t* formName=L"debug_debugTwin2";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	State::SSPlcState ssState;
	int StateID = 0;
	bool allCases=true;
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;
	while(!end_DebugTwin2)
	{
		const int ITEM_COUNT=14;
		StateID = C_PageManager::SMState.getSSPlcState(ssState);
		wchar_t stData[ITEM_COUNT*15];
		int cnt=0;
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getSensorStatus(), Network::SEP);//current group working
		cnt+=swprintf(stData+cnt, L"%d%s", ssState.getAOCStatusRunning(), Network::SEP);
		for (Shaft sh=Score1; sh<=ScoreAux; sh++)
		{
			cnt+=swprintf(stData+cnt, L"%d%s", ssState.getScoreAngle(sh), Network::SEP);
			cnt+=swprintf(stData+cnt, L"%d%s", ssState.getScoreGap(sh), Network::SEP);
		}
    for (int i=0; i<5; i++)
      cnt+=swprintf(stData+cnt, L"%d%s", ssState.getInputChannel((State::IOInputChannels)(i+1)), Network::SEP);
    cnt+=swprintf(stData+cnt, L"%d", ssState.getInputChannel((State::IOInputChannels)6));

		o_socket.Send(Network::MSG_PAG_REF,L"%s._RefreshStatus(S[%s])", formName, stData);

		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated debug_debugTwin2 page threads");

	return ERROR_SUCCESS;
}
