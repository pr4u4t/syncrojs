#include ".\C_SlitterScorerGroup.h"

//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

//Page Management
#include "..\C_PageManager.h"

bool C_SlitterScorerGroup::end_Status;
bool C_SlitterScorerGroup::end_Console;
bool C_SlitterScorerGroup::end_AlarmsActive;
bool C_SlitterScorerGroup::end_AlarmsHistory;
bool C_SlitterScorerGroup::end_SettingsMachine;
bool C_SlitterScorerGroup::end_SettingsTask;
//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
bool C_SlitterScorerGroup::end_Blades;
bool C_SlitterScorerGroup::end_WebDirector;
bool C_SlitterScorerGroup::end_Regulations;

C_SlitterScorerGroup::C_SlitterScorerGroup(void)
{
	pid_SlitterScorerPage=NULL;
	TerminateThread(false);
}

C_SlitterScorerGroup::~C_SlitterScorerGroup(void)
{
}

bool C_SlitterScorerGroup::ChooseSlitterScorerPage(String::StrType  thePageRequest)
{
	//Slitter scorer status
	if(thePageRequest.CompareNoCase(L"Status"))
	{
		end_Status=false;
		pid_SlitterScorerPage=::CreateThread(NULL,0,Status,NULL,0,NULL);
		return true;
	}
	//Slitter scorer console
	if(thePageRequest.CompareNoCase(L"Console"))
	{
		end_Console=false;
		pid_SlitterScorerPage=::CreateThread(NULL,0,Console,NULL,0,NULL);
		return true;
	}
	//Slitter scorer alarm
	if(thePageRequest.CompareNoCase(L"AlarmActive"))
	{
		end_AlarmsActive=false;
		pid_SlitterScorerPage=::CreateThread(NULL,0,AlarmActive,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"AlarmHistory"))
	{
		end_AlarmsHistory=false;
		pid_SlitterScorerPage=::CreateThread(NULL,0,AlarmHistory,NULL,0,NULL);
		return true;
	}
	//Slitter scorer settings
	if(thePageRequest.CompareNoCase(L"SettingsMachine"))
	{
		end_SettingsMachine=false;
		pid_SlitterScorerPage=::CreateThread(NULL,0,SettingsMachine,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"SettingsTask"))
	{
		end_SettingsTask=false;
		pid_SlitterScorerPage=::CreateThread(NULL,0,SettingsTask,NULL,0,NULL);
		return true;
	}
	//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
	if(thePageRequest.CompareNoCase(L"Blades"))
	{
		end_Blades=false;
		pid_SlitterScorerPage=::CreateThread(NULL,0,Blades,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"WebDirector"))
	{
		end_WebDirector=false;
		pid_SlitterScorerPage=::CreateThread(NULL,0,WebDirector,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"Regulations"))
	{
		end_Regulations=false;
		pid_SlitterScorerPage=::CreateThread(NULL,0,Regulations,NULL,0,NULL);
		return true;
	}	return false;
}

void C_SlitterScorerGroup::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_SlitterScorerPage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_SlitterScorerPage,NULL);
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
		end_WebDirector = true;
		end_Blades = true;
		end_Regulations = true;
	}
}

DWORD C_SlitterScorerGroup::SuspendThread(void)
{
	if(pid_SlitterScorerPage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_SlitterScorerPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page Slitter Scorer Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_SlitterScorerGroup::ResumeThread(void)
{
	if(pid_SlitterScorerPage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_SlitterScorerPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page Slitter Scorer Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SlitterScorerGroup::Status(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"slitterScorer_status.lbl_Title_YN.Text=%s",C_PageManager::SMState.getSlitterScorerState().getMachineName());

	State::SlitterScorerState theSlitterScorerState;

	//RFAZZI_FI28/06/2008_1.Added send order Push Button on machine status page
	bool communicationError = false;
  //agrassi_FI25-05-2011_1
  LineConfig* cfg=new LineConfig();
  C_PageManager::SMLineConfig.getLineConfig(*cfg);

	while(!end_Status)
	{	
		C_PageManager::SMState.getSlitterScorerState(theSlitterScorerState);

		//manual auto
		if(theSlitterScorerState.control.getIsInManual().getEnable())	
			o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.stb_AutoMan.Auto=%s",(!theSlitterScorerState.control.getIsInManual().getValue()) ? L"True" : L"False");

		//maintenance
		if(theSlitterScorerState.control.getIsInMaintenance().getEnable())	
			o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.pct_Maintenance.Visible=%s",(theSlitterScorerState.control.getIsInMaintenance().getValue()) ? L"True" : L"False");

		//Lubrification
		if(theSlitterScorerState.monitor.getIsLubrificationInProgess().getEnable())	
			o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.pct_Lubrification.Visible=%s",(theSlitterScorerState.monitor.getIsLubrificationInProgess().getValue()) ? L"True" : L"False");

		//Grinding
		if(theSlitterScorerState.monitor.getIsGrindingInProgess().getEnable())	
			o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.pct_Grinding.Visible=%s",(theSlitterScorerState.monitor.getIsGrindingInProgess().getValue()) ? L"True" : L"False");


		//ScoreGap shaft 1
		if(theSlitterScorerState.monitor.getScoreGap1().getEnable())
    {
      //agrassi_FI25-05-2011_1
      if (!cfg->slitterScorer.isTwin2())
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.nud_ScoreGap1.Value=%d",theSlitterScorerState.monitor.getScoreGap1().getValue());
      else
  			o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ScoreGap1.Text=%d",theSlitterScorerState.monitor.getScoreGap1().getValue());
    }

		//ScoreGap shaft 2
		if(theSlitterScorerState.monitor.getScoreGap2().getEnable())	
    {
      //agrassi_FI25-05-2011_1
      if (!cfg->slitterScorer.isTwin2())
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.nud_ScoreGap2.Value=%d",theSlitterScorerState.monitor.getScoreGap2().getValue());
      else
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ScoreGap2.Text=%d",theSlitterScorerState.monitor.getScoreGap2().getValue());
    }

		//ScoreGap shaft Aux
		if(theSlitterScorerState.monitor.getScoreGap3().getEnable())	
    {
      //agrassi_FI25-05-2011_1
      if (!cfg->slitterScorer.isTwin2())
  			o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.nud_ScoreGapAux.Value=%d",theSlitterScorerState.monitor.getScoreGap3().getValue());
      else
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ScoreGap3.Text=%d",theSlitterScorerState.monitor.getScoreGap3().getValue());
    }

		int score = theSlitterScorerState.monitor.getIsCurrentOnScore1().getValue() ? 1 : 2;
		int slit = theSlitterScorerState.monitor.getIsCurrentOnSlit1().getValue() ? 1 : 2;
	
		if(theSlitterScorerState.orders.getIDCurrent().getEnable())
		{
			if(theSlitterScorerState.orders.getIDCurrent().getValue() != 0)
			{
				o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._SetCurrentOrderStatus(i[%d],i[%d],i[%d],i[%d],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s])",
								theSlitterScorerState.orders.getIDCurrent().getValue(),
								theSlitterScorerState.orders.getModIDCurrent().getValue(),
								score,
								slit,
								String::Bool2Alpha(theSlitterScorerState.orders.getIsCurrentValidAUX().getValue()),
								String::Bool2Alpha(theSlitterScorerState.orders.getIsCurrentValidTandem().getValue()),
								String::Bool2Alpha(theSlitterScorerState.orders.getNoGapPossibleOrder1().getValue()),
								String::Bool2Alpha(theSlitterScorerState.orders.getIsCurrentValid().getValue()),
								String::Bool2Alpha(theSlitterScorerState.monitor.getIsPositioningCurrent().getValue()),
								String::Bool2Alpha(theSlitterScorerState.monitor.getIsCurrentPositioned().getValue()),
								String::Bool2Alpha(theSlitterScorerState.monitor.getIsCurrentWorking().getValue()));
			}
			else
			{
				o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._ClearCurrentOrderStatus()");
			}
		}

		//Uso il corrente per calcolare il successivo
		score = theSlitterScorerState.monitor.getIsCurrentOnScore1().getValue() ? 2 : 1;
		slit = theSlitterScorerState.monitor.getIsCurrentOnSlit1().getValue() ? 2 : 1;

    //agrassi_FI25-05-2011_1
    if (cfg->slitterScorer.isTwin2())
    {
      if (theSlitterScorerState.monitor.getIsCurrentOnSlit1())
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ord1.Text=§%S",stringer(SYCC_62));
      else if (theSlitterScorerState.monitor.getIsNextOnSlit1())
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ord1.Text=§%S",stringer(SYCC_63));
      else
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ord1.Text=%s",L"");
      if (theSlitterScorerState.monitor.getIsCurrentOnSlit2())
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ord2.Text=§%S",stringer(SYCC_62));
      else if (theSlitterScorerState.monitor.getIsNextOnSlit2())
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ord2.Text=§%S",stringer(SYCC_63));
      else
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ord2.Text=%s",L"");
      if (theSlitterScorerState.monitor.getIsCurrentOnScore1())
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ord3.Text=§%S",stringer(SYCC_62));
      else if (theSlitterScorerState.monitor.getIsNextOnScore1())
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ord3.Text=§%S",stringer(SYCC_63));
      else
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ord3.Text=%s",L"");
      if (theSlitterScorerState.monitor.getIsCurrentOnScore2())
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ord4.Text=§%S",stringer(SYCC_62));
      else if (theSlitterScorerState.monitor.getIsNextOnScore2())
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ord4.Text=§%S",stringer(SYCC_63));
      else
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ord4.Text=%s",L"");
      if (theSlitterScorerState.monitor.getIsCurrentOnScoreAux())
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ord5.Text=§%S",stringer(SYCC_62));
      else if (theSlitterScorerState.monitor.getIsNextOnScoreAux())
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ord5.Text=§%S",stringer(SYCC_63));
      else
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ord5.Text=%s",L"");

      o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_AOC.Visible=%s",String::Bool2Alpha(theSlitterScorerState.monitor.aoc.getIsInAOC()));
      
			for (Shaft sh=Slit1; sh<=ScoreAux; sh++)
			{
				wchar_t color[40]=L"Silver";
				if (theSlitterScorerState.plcState.getShaftStatusPlunged(sh))
					wcscpy(color, L"SteelBlue");
				else if (theSlitterScorerState.plcState.getShaftStatusPositioning(sh))
					wcscpy(color, L"Gold");
				else if (theSlitterScorerState.plcState.getShaftStatusPositioned(sh))
					wcscpy(color, L"MediumSeaGreen");
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.Shaft%d.BackColor=%s", (int)(sh+1), color);
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ord%d.BackColor=%s", (int)(sh+1), color);
			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_Num%d.BackColor=%s", (int)(sh+1), color);
        if (sh>=Score1)
  			  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.lbl_ScoreGap%d.BackColor=%s", (int)(sh+1-Score1), color);
      }
    }
    bool enableSendNext=(!theSlitterScorerState.monitor.getIsPositioning()) && (!theSlitterScorerState.alarms.getIsAlarmCommunication());
    bool enableSendCurrent=enableSendNext && (!theSlitterScorerState.monitor.getIsCurrentWorking());
    o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.SendNextOrder.Enable=%s", String::Bool2Alpha(enableSendNext));
	  o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status.SendCurrentOrder.Enable=%s", String::Bool2Alpha(enableSendCurrent));

		if(theSlitterScorerState.orders.getIDNext().getEnable())
		{
			if(theSlitterScorerState.orders.getIDCurrent().getValue() != 0)
			{
				o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._SetNextOrderStatus(i[%d],i[%d],i[%d],i[%d],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s])",
								theSlitterScorerState.orders.getIDNext().getValue(),
								theSlitterScorerState.orders.getModIDNext().getValue(),
								score,
								slit,
								String::Bool2Alpha(theSlitterScorerState.orders.getIsNextValidAUX().getValue()),
								String::Bool2Alpha(theSlitterScorerState.orders.getIsNextValidTandem().getValue()),
								String::Bool2Alpha(theSlitterScorerState.orders.getNoGapPossibleOrder2().getValue()),
								String::Bool2Alpha(theSlitterScorerState.orders.getIsNextValid().getValue()),
								String::Bool2Alpha(theSlitterScorerState.monitor.getIsPositioningNext().getValue()),
								String::Bool2Alpha(theSlitterScorerState.monitor.getIsNextPositioned().getValue()),
								String::Bool2Alpha(theSlitterScorerState.monitor.getIsNextWorking().getValue()));
			}
			else
			{
				o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._ClearNextOrderStatus()");
			}
		}

		//Slit shaft 1 status
		o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._SetSlitShaft1Status(s[%s])",theSlitterScorerState.monitor.getMaskSlitShaft1());

		//Slit shaft 2 status
		o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._SetSlitShaft2Status(s[%s])",theSlitterScorerState.monitor.getMaskSlitShaft2());

		//Shaft Status
		if(theSlitterScorerState.monitor.getIsSlits1Plunged().getEnable())
		{
			o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._SetShaftStatus(b[%s],b[%s],b[%s],b[%s],b[%s])",
				String::Bool2Alpha(theSlitterScorerState.monitor.getIsSlits1Plunged().getValue()),
				String::Bool2Alpha(theSlitterScorerState.monitor.getIsSlits2Plunged().getValue()),
				String::Bool2Alpha(theSlitterScorerState.monitor.getIsScores1Plunged().getValue()),
				String::Bool2Alpha(theSlitterScorerState.monitor.getIsScores2Plunged().getValue()),
				String::Bool2Alpha(theSlitterScorerState.monitor.getIsScoresAuxPlunged().getValue()));
		}

		//RFAZZI_FI28/06/2008_1.Added send order Push Button on machine status page
		if(communicationError!=C_PageManager::SMState.getSlitterScorerState().alarms.getIsAlarmMCPCommError())
		{
			communicationError = C_PageManager::SMState.getSlitterScorerState().alarms.getIsAlarmMCPCommError();
			o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_status._CommunicationError(b[%s])",
        String::Bool2Alpha(communicationError));
		}

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated slitterScorer_status page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SlitterScorerGroup::Console(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_console._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"slitterScorer_console.lbl_Title_YN.Text=%s",C_PageManager::SMState.getSlitterScorerState().getMachineName());

	bool communicationError = false;
	while(!end_Console)
	{
		if(communicationError!=C_PageManager::SMState.getSlitterScorerState().alarms.getIsAlarmMCPCommError())
		{
			communicationError = C_PageManager::SMState.getSlitterScorerState().alarms.getIsAlarmMCPCommError();
			o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_console._CommunicationError(b[%s])",
        String::Bool2Alpha(communicationError));
		}

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated slitterScorer_console page threads");

	return ERROR_SUCCESS;
}

//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
DWORD WINAPI C_SlitterScorerGroup::Blades(LPVOID  lpParam)
{

	const wchar_t* formName=L"slitterScorer_blades";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	o_socket.Send(Network::MSG_PAG_REF, L"%s.lbl_Title_YN.Text=%s", formName,C_PageManager::SMState.getSlitterScorerState().getMachineName());
	State::SlitterScorerState ssState;
	int StateID = 0;
	bool oldManualGrindingDisabled=false;
	SetupData setupData;
	
	bool communicationError = false;

	while(!end_Blades)
	{
		StateID = C_PageManager::SMState.getSlitterScorerState(ssState);

		wchar_t slitsData[NUM_SLIT_SHAFTS*MAX_SLITS_PER_SHAFT*10];
		int cnt=0;
		cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.plcState.getGrindDuration(), Network::SEP);
		cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.plcState.getLubeDuration(), Network::SEP);
		cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.plcState.getSetupSituationGrindInterval(0), Network::SEP);
		cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.plcState.getSetupSituationLubeInterval(0), Network::SEP);
		cnt+=swprintf(slitsData+cnt, L"%d", ssState.plcState.getSetupSituationTrimGrindMultiplier(0));
		o_socket.Send(Network::MSG_PAG_REF,L"%s._UpdateCleanCutCounters(S[%s])", formName, slitsData);

		oldManualGrindingDisabled=ssState.plcState.getManualGrindingDisabled();

    bool pushEnabled=(ssState.plcState.getTaskStatusPLCComm() && ssState.plcState.getTaskStatusPLCRun());
		for (Shaft sh=Slit1; sh<=Slit2; sh++)
			o_socket.Send(Network::MSG_PAG_REF,L"%s.toolDetail.SetPositioning(B[%s], I[%d])", formName, String::Bool2Alpha(ssState.plcState.getShaftStatusPositioning(sh)), sh);

		int shaft=-1;
		int slit=-1;
			shaft=ssState.plcState.getGrindingData().shaft;
			slit=ssState.plcState.getGrindingData().slit;

		o_socket.Send(Network::MSG_PAG_REF,L"%s.LubeSlit.Enabled=%s", formName, String::Bool2Alpha(pushEnabled && ssState.plcState.getMachineStatusRunning()));

		o_socket.Send(Network::MSG_PAG_REF,L"%s.GrindSlit.Enabled=%s", formName, String::Bool2Alpha(pushEnabled && ssState.plcState.getFunctionalityStatusGrindEnable() && (!ssState.plcState.getManualGrindingDisabled())));

		bool canEnableGrinding=(!ssState.plcState.getFunctionalityStatusGrindEnable()) && pushEnabled;
		o_socket.Send(Network::MSG_PAG_REF,L"%s.EnableGrinding.Visible=%s", formName, String::Bool2Alpha(canEnableGrinding));

		int lubeChannel=ssState.plcState.getOutputChannel(State::OUTLubrication);
		logger.Info(__FUNCTION__, L"Set lubrication: lubeChannel=%d", lubeChannel);
		bool lubricating=(lubeChannel!=0);
		
    //agrassi_FI22-08-2011_1
		//Slit Position on the shafts
		//C_PageManager::SMOrder.DRYGetCurrentSetup(setupData);
		//C_PageManager::SMOrder.DRYGetNextSetup(setupData);
    bool isCurr=false;

		cnt=0;

		//Send Slits position Ax1
		if ( ssState.monitor.getIsCurrentOnSlit1().getValue() )
    {
      isCurr=true;
			C_PageManager::SMOrder.DRYFindProgramID(ssState.orders.getIDCurrent(),4,setupData);
    }
		else
			C_PageManager::SMOrder.DRYFindProgramID(ssState.orders.getIDNext(),4,setupData);

		for (int slt=0; slt<MAX_SLITS_PER_SHAFT; slt++)
		{
			cnt+=swprintf(slitsData+cnt, L"%d%s", setupData.calculations->slitShafts[0].slitsPositions_UDdmm[slt], Network::SEP);
		}
    //agrassi_FI22-08-2011_1
		//cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.plcState.getPlungedSlits(Slit1), Network::SEP);
    cnt+=swprintf(slitsData+cnt, L"%d%s", (isCurr ? ssState.plcState.getPlungedSlits(Slit1) : 0), Network::SEP);
		cnt+=swprintf(slitsData+cnt, L"%s%s", String::Bool2Alpha(true), Network::SEP);

		//Send Slits position Ax2
    //agrassi_FI22-08-2011_1
    isCurr=false;
		//if ( ssState.monitor.getIsCurrentOnSlit1().getValue() )
		if ( ssState.monitor.getIsCurrentOnSlit2().getValue() )
    {
      isCurr=true;
			C_PageManager::SMOrder.DRYFindProgramID(ssState.orders.getIDCurrent(),4,setupData);
    }
		else
			C_PageManager::SMOrder.DRYFindProgramID(ssState.orders.getIDNext(),4,setupData);

		for (int slt=0; slt<MAX_SLITS_PER_SHAFT; slt++)
		{
			cnt+=swprintf(slitsData+cnt, L"%d%s", setupData.calculations->slitShafts[0].slitsPositions_UDdmm[slt], Network::SEP);
		}
    //agrassi_FI22-08-2011_1
		//cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.plcState.getPlungedSlits(Slit2), Network::SEP);
    cnt+=swprintf(slitsData+cnt, L"%d%s", (isCurr ? ssState.plcState.getPlungedSlits(Slit2) : 0), Network::SEP);
		cnt+=swprintf(slitsData+cnt, L"%s%s", String::Bool2Alpha(true), L"");

		o_socket.Send(Network::MSG_PAG_REF,L"%s._UpdateSlitPositions(S[%s])", formName, slitsData);

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated slitterScorer_blades page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SlitterScorerGroup::WebDirector(LPVOID  lpParam)
{
	const wchar_t* formName=L"slitterScorer_webDirector";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
  //agrassi_FI25-05-2011_1
	//State::SlitterScorerState ssState;
  State::SlitterScorerState* ssState=new State::SlitterScorerState();
  State::PaddlesState* padState=new State::PaddlesState();
	State::C_SMState::SSEventsEnum event=State::C_SMState::SS_RefreshAll;
	//int StateID = 0;
	int plcOldWebDir2LevMask=-1, plcOldWebDir3LevMask=-1;
  //agrassi_FI25-05-2011_1
  LineConfig* cfg=new LineConfig();
  C_PageManager::SMLineConfig.getLineConfig(*cfg);
  int twoLevMask, threeLevMask;
	
	while(!end_WebDirector)
	{
    //agrassi_FI25-05-2011_1
    if (cfg->webDirector.getSyncroInterface())
    {
      C_PageManager::SMState.getPaddlesState(*padState);
      twoLevMask=padState->getTwoLevelMask(1);
      threeLevMask=padState->getThreeLevelMask(1);
    }
    else
    {
      C_PageManager::SMState.getSlitterScorerState(*ssState);
      twoLevMask=ssState->plcState.getWebDir2LevMask();
      threeLevMask=ssState->plcState.getWebDir3LevMask();
    }
		//StateID = C_PageManager::SMState.getSlitterScorerState(ssState);
		//if ( ( ssState.plcState.getWebDir2LevMask() != plcOldWebDir2LevMask ) ||
		//			( ssState.plcState.getWebDir3LevMask() != plcOldWebDir3LevMask ) )
		//	o_socket.Send(Network::MSG_PAG_REF, L"%s._UpdateWebDirMask(I[%d], I[%d], I[%d], B[%s], I[%d])", 
		//								formName, 0, ssState.plcState.getWebDir2LevMask(), 
		//								ssState.plcState.getWebDir3LevMask(), 
		//								L"true", 
		//								(int)!ssState.monitor.getIsCurrentOnSlit1().getValue());
		//plcOldWebDir2LevMask = ssState.plcState.getWebDir2LevMask();
		//plcOldWebDir3LevMask = ssState.plcState.getWebDir3LevMask();

    if ( ( twoLevMask != plcOldWebDir2LevMask ) || ( threeLevMask != plcOldWebDir3LevMask ) )
			o_socket.Send(Network::MSG_PAG_REF, L"%s._UpdateWebDirMask(I[%d], I[%d], I[%d], B[%s], I[%d])", 
										formName, 0, twoLevMask, threeLevMask, L"true", 
										(int)!ssState->monitor.getIsCurrentOnSlit1().getValue());
		plcOldWebDir2LevMask = twoLevMask;
		plcOldWebDir3LevMask = threeLevMask;
		Sleep(500);
	}
  //agrassi_FI25-05-2011_1
  delete cfg;
  delete ssState;
  delete padState;
	logger.Fine(__FUNCTION__, L"Terminated SlitterScorerWebDirector page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SlitterScorerGroup::Regulations(LPVOID  lpParam)
{
	const wchar_t* formName=L"slitterScorer_Regulations";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);

	while(!end_Regulations)
	{
		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated twin400_Regulations page threads");
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SlitterScorerGroup::AlarmActive(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_alarmActive._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"slitterScorer_alarmActive.lbl_Title_YN.Text=%s",C_PageManager::SMState.getSlitterScorerState().getMachineName());

	while(!end_AlarmsActive)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated slitterScorer_alarmActive page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_SlitterScorerGroup::AlarmHistory(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_alarmHistory._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"slitterScorer_alarmHistory.lbl_Title_YN.Text=%s",C_PageManager::SMState.getSlitterScorerState().getMachineName());

	while(!end_AlarmsHistory)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated slitterScorer_alarmHistory page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SlitterScorerGroup::SettingsMachine(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_SettingsMachine._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"slitterScorer_SettingsMachine.lbl_Title_YN.Text=%s",C_PageManager::SMState.getSlitterScorerState().getMachineName());

	while(!end_SettingsMachine)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated slitterScorer_SettingsMachine page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SlitterScorerGroup::SettingsTask(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"slitterScorer_SettingsTask._Show()");
	o_socket.Send(Network::MSG_PAG_REF, L"slitterScorer_SettingsTask.lbl_Title_YN.Text=%s",C_PageManager::SMState.getSlitterScorerState().getMachineName());

	while(!end_SettingsTask)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated slitterScorer_SettingsTask page threads");

	return ERROR_SUCCESS;
}