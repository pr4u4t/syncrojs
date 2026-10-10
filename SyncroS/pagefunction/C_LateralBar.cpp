#include ".\C_LateralBar.h"

#include "..\C_PageManager.h"
//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

bool C_LateralBar::end_LateralBar;
int C_LateralBar::tab_lateralBar_1;
int C_LateralBar::tab_lateralBar_2;
int C_LateralBar::tab_lateralBar_3;


C_LateralBar::C_LateralBar(void)
{
	pid_Lateral_bar=NULL;
	TerminateThread(false);
}

C_LateralBar::~C_LateralBar(void)
{
}

bool C_LateralBar::ChooseLateralBar(String::StrType theLateralBar)
{
  if(theLateralBar.CompareNoCase(L"LateralBar"))
	{
    tab_lateralBar_1 = 0;
    tab_lateralBar_2 = 1;
    tab_lateralBar_3 = 2;
		end_LateralBar = false;
    pid_Lateral_bar=::CreateThread(NULL,0,C_LateralBar::LateralBar,NULL,0,NULL);
	}

  if(theLateralBar.SearchNoCase(L"LateralBar_1"))
	{
    tab_lateralBar_1 = theLateralBar.extractCommandParameter().ToInt();
	}

  if(theLateralBar.SearchNoCase(L"LateralBar_2"))
	{
		tab_lateralBar_2 = theLateralBar.extractCommandParameter().ToInt();
	}

  if(theLateralBar.SearchNoCase(L"LateralBar_3"))
	{
		tab_lateralBar_3 = theLateralBar.extractCommandParameter().ToInt();
	}

	if (pid_Lateral_bar == NULL)
	{
		logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"ERROR: Cannot create LateralBar thread ... %d",GetLastError());
		return false;
	}
	return true;
}

void C_LateralBar::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_Lateral_bar!=NULL)
		{
			BOOL res = ::TerminateThread(pid_Lateral_bar,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread LateralBar ... %d", ::GetLastError());
		}
	}
	else
	{
    //questa non deve essere fatta perchè il thread è uno solo
		//end_LateralBar = true;
	}
}

DWORD C_LateralBar::SuspendThread(void)
{
	if(::SuspendThread(pid_Lateral_bar)==-1)
	{
		DWORD error=GetLastError();
		logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Lateral Bar Thread ... %d",error);
		return error;
	};
  
	return ERROR_SUCCESS;
}

DWORD C_LateralBar::ResumeThread(void)
{
	if(::ResumeThread(pid_Lateral_bar)==-1)
	{
		DWORD error=GetLastError();
		logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Lateral Bar Thread ... %d",error);
		return error;
	}

	return ERROR_SUCCESS;
}

void C_LateralBar::LateralBar_UpdateTab(int lateralBarNumber, int tab_lateralBarNumber)
{
  int appoInt = -1;
  wchar_t appoStr[MAX_PATH] = L"";
  wchar_t appoStr2[MAX_PATH] = L"";

  bool  SourceDryEnd = false;

  LineConfig lineConfig;
  C_PageManager::SMLineConfig.getLineConfig(lineConfig);

  switch(tab_lateralBarNumber)
  {
  //LOW LEVEL SF 1
  case 0:
    if(appoInt == -1)
    {
      swprintf(appoStr2,L"SF1");
      appoInt = 1;
    }
    //NON METTERE QUI IL BREAK !!!

  //LOW LEVEL SF 2
  case 1:
    if(appoInt == -1)
    {
      swprintf(appoStr2,L"SF2");
      appoInt = 2;
    }
    //NON METTERE QUI IL BREAK !!!

  //RFAZZI_FI18/98/2011_1. Aggiunta la terza zona nella LateralBar. 
  //LOW LEVEL SF 2
  case 12:
    if(appoInt == -1)
    {
      swprintf(appoStr2,L"SF3");
      appoInt = 3;
    }
    //NON METTERE QUI IL BREAK !!!

  //LOW LEVEL PHT + GU
  case 2:
    if(appoInt == -1)
    {
      swprintf(appoStr2,L"PHTGU");
      appoInt = 0;
    }
		
    //ABELLO_FI18/04/2011_1 questi controlli sulla lateral bar non ci sono e quindi è inutile mandare le informazioni
    if(appoInt != 0)
      o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.%s_Info.Text=%d", lateralBarNumber, appoStr2,  
        C_PageManager::SMState.getZoneState(appoInt).Monitor.getSpeed_UVm_m());

    //WRAP ARM
    for (int i=0; i<sizeof(managedWrapArm)/sizeof(int); i++)
		{
      int	theWrapArmNumber = managedWrapArm[i];

      if (
        lineConfig.isAValidWrapArmNumber(theWrapArmNumber) && 
        (lineConfig.getZoneNFromWrapArmNumber(theWrapArmNumber)==appoInt) && 
        lineConfig.getWrapArmByNumber(theWrapArmNumber).getPresent() /*&&
        C_PageManager::SMState.getWrapArmByNumber(theWrapArmNumber).isUsed()*/
        )
      {

        //RFAZZI_FI18/98/2011_1. Aggiunta la terza zona nella LateralBar. 
        switch(theWrapArmNumber)
        {
        //ABELLO_FI18/04/2011_1 questi controlli sulla lateral bar non ci sono e quindi è inutile mandare le informazioni
        //questi wrap arm non sono gestiti sulla lateral bar
        case S1_3:
        case S1_4:
          continue;
          break;
        case S2_1:
        case S4_1:
        case S6_1:
          swprintf(appoStr,L"Em");
          break;
        case S2_2:
        case S4_2:
        case S6_2:
          swprintf(appoStr,L"Im");
          break;
        case S3_1:
        case S5_1:
        case S7_1:
          swprintf(appoStr,L"El");
          break;
        case S3_2:
        case S5_2:
        case S7_2:
          swprintf(appoStr,L"Il");
          break;
        default:
          swprintf(appoStr,L"%d",theWrapArmNumber);
          break;
        }

        o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.%s_wrapArmPreHeater%s.Visible=true",lateralBarNumber, appoStr2, appoStr); 

			  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.%s_wrapArmPreHeater%s.InLocal=%s",lateralBarNumber, appoStr2, appoStr,
				  String::Bool2Alpha(C_PageManager::SMState.getWrapArmByNumber(theWrapArmNumber).getIsWrapArmLocal()));
			  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.%s_wrapArmPreHeater%s.Auto=%s",lateralBarNumber, appoStr2, appoStr,
				  String::Bool2Alpha(C_PageManager::SMState.getWrapArmByNumber(theWrapArmNumber).getIsWrapArmAuto()));
			  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.%s_wrapArmPreHeater%s.IsUnder=%s",lateralBarNumber, appoStr2, appoStr,
				  String::Bool2Alpha(C_PageManager::SMState.getWrapArmByNumber(theWrapArmNumber).getIsWrapArmUnder()));
			  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.%s_wrapArmPreHeater%s.Position=%d",lateralBarNumber, appoStr2, appoStr,
				  C_PageManager::SMState.getWrapArmByNumber(theWrapArmNumber).getPosition_UPercINT().getValue());
			  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.%s_wrapArmPreHeater%s.IsMaxPos=%s",lateralBarNumber, appoStr2, appoStr,
				  String::Bool2Alpha(C_PageManager::SMState.getWrapArmByNumber(theWrapArmNumber).getIsMaxPos()));
			  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.%s_wrapArmPreHeater%s.IsMinPos=%s",lateralBarNumber, appoStr2, appoStr,
				  String::Bool2Alpha(C_PageManager::SMState.getWrapArmByNumber(theWrapArmNumber).getIsMinPos()));
        
        if(C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(theWrapArmNumber).getEnable())
          o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.%s_wrapArmPreHeater%s.Temperature=%d",lateralBarNumber, appoStr2, appoStr,
            C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(theWrapArmNumber).getValue());
      }
    }

    //GLUE GAP
    for (int i=0; i<sizeof(managedGlueGap)/sizeof(int); i++)
		{
      int theGlueGapNumber = managedGlueGap[i];

      if (
        lineConfig.isAValidGlueGapNumber(theGlueGapNumber) && 
        (lineConfig.getZoneNFromGlueGapNumber(GlueGapNumber(theGlueGapNumber))==appoInt) && 
        lineConfig.getGlueGapByNumber(theGlueGapNumber).getPresent() /*&&
        C_PageManager::SMState.getGlueGapByNumber(theGlueGapNumber).isUsed()*/
        )
        {
				  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.%s_glueGap%d.Visible=true", lateralBarNumber, appoStr2, theGlueGapNumber);
				  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.%s_glueGap%d.InLocal=%s", lateralBarNumber, appoStr2, theGlueGapNumber,
            String::Bool2Alpha(C_PageManager::SMState.getGlueGapByNumber(theGlueGapNumber).getIsGlueGapLocal()));
				  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.%s_glueGap%d.Auto=%s", lateralBarNumber, appoStr2, theGlueGapNumber,
					  String::Bool2Alpha(C_PageManager::SMState.getGlueGapByNumber(theGlueGapNumber).getIsGlueGapAuto()));
				  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.%s_glueGap%d.Position=%d", lateralBarNumber, appoStr2, theGlueGapNumber,
					  C_PageManager::SMState.getGlueGapByNumber(theGlueGapNumber).getPositionINT().getValue());
				  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.%s_glueGap%d.IsMaxPos=%s", lateralBarNumber, appoStr2, theGlueGapNumber,
					  String::Bool2Alpha(C_PageManager::SMState.getGlueGapByNumber(theGlueGapNumber).getIsMaxPos()));
				  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.%s_glueGap%d.IsMinPos=%s", lateralBarNumber, appoStr2, theGlueGapNumber,
					  String::Bool2Alpha(C_PageManager::SMState.getGlueGapByNumber(theGlueGapNumber).getIsMinPos()));
      }
    }

    //PAPER TENSION
    for (int i=0; i<sizeof(managedPaperTension)/sizeof(int); i++)
		{
      int thePaperTensionNumber = managedPaperTension[i];

      if (
        lineConfig.isAValidPaperTensionNumber(thePaperTensionNumber) && 
        (lineConfig.getZoneNFromPaperTensionNumber(PaperTensionNumber(thePaperTensionNumber))==appoInt) && 
        lineConfig.getPaperTensionByNumber(thePaperTensionNumber).getPresent() /*&&
        C_PageManager::SMState.getPaperTensionByNumber(PaperTensionNumber(thePaperTensionNumber)).getPaperTension_UFkgxm_s2().getEnable()*/
        )
			  {
          if ( thePaperTensionNumber == B1_PT )
            swprintf(appoStr2,L"B1");
          else if ( thePaperTensionNumber == B2_PT )
            swprintf(appoStr2,L"B2");
          else 
            swprintf(appoStr2,L"SPL");
            
            // Splicer Paper Tension
			      o_socket.Send(Network::MSG_PAG_REF, L"lateralBar_lateralBar.lateralBarItem%d.%s_PaperTension%d.Visible=true", lateralBarNumber, appoStr2, thePaperTensionNumber);
			      o_socket.Send(Network::MSG_PAG_REF, L"lateralBar_lateralBar.lateralBarItem%d.%s_PaperTension%d.Position=%d", lateralBarNumber, appoStr2, thePaperTensionNumber, 
              (int)C_PageManager::SMState.getPaperTensionByNumber(PaperTensionNumber(thePaperTensionNumber)).getPaperTension_UFkgxm_s2().getValue());
	          o_socket.Send(Network::MSG_PAG_REF, L"lateralBar_lateralBar.lateralBarItem%d.%s_PaperTension%d.InLocal=%s", lateralBarNumber, appoStr2, thePaperTensionNumber,
		          String::Bool2Alpha(!C_PageManager::SMState.getPaperTensionByNumber(PaperTensionNumber(thePaperTensionNumber)).getIsPaperTensionRemote().getValue()));
	          o_socket.Send(Network::MSG_PAG_REF, L"lateralBar_lateralBar.lateralBarItem%d.%s_PaperTension%d.Auto=%s", lateralBarNumber, appoStr2, thePaperTensionNumber,
		          String::Bool2Alpha(C_PageManager::SMState.getPaperTensionByNumber(PaperTensionNumber(thePaperTensionNumber)).getIsPaperTensionAuto().getValue()));
      }
    }

    break;

  case 7:
    {
      ShiftData shiftData = C_PageManager::SMOrder.SHIFTGetCurrentShift();

      o_socket.Send(Network::MSG_PAG_REF, L"lateralBar_lateralBar.lateralBarItem%d._UpdateCurrentShift(b[%s],i[%d],i[%d],i[%d],i[%d])",
        lateralBarNumber,
        String::Bool2Alpha(shiftData.isStopShift()),
        shiftData.getRunTime_UTs(), 
			  shiftData.getStopTime_UTs(),
        shiftData.getAvgSpeed_UVm_m(),
        shiftData.getTotalMetersProducedSS_UDm()+shiftData.getTotalScrapRSH_UDm()); 

      break;
    }

  case 3:
  case 4:
  case 5:
  case 6:
  //agrassi_FI08-09-2010_1
  //case 8:
  //RFAZZI_FI26/05/2010_1. Aggiunto in lateral bar la visualizzazione dei dettagli dell'ordine DryEnd Corrente
  case 9:
    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d._Update()", lateralBarNumber);
    break;
 
  //RFAZZI_FI27/05/2010_1.Aggiunto in lateral bar la visualizzazione della HomeWetEnd. 
  case 10:
    {
      State::WECState wecState;
      wecState.Clear();
      State::SplicerCtrlData theSplicerState;
      theSplicerState.Clear();
      ProgramData currentProgramData;
      currentProgramData.Clear();

		  C_PageManager::SMState.getWECState(wecState);

		  //auto man bottons; splice confirmed checkboxes
		  for (int splNumber = 1; splNumber <= 5; splNumber++)
		  {
			  State::SplicerState theSplicerState;
			  theSplicerState.Clear();
			  C_PageManager::SMState.getSplicerState(theSplicerState, splNumber);
        
		    switch (theSplicerState.Control.getMode())
		    {
			    case State::WECMODE_MANUAL:
				    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.stb_SpliceConfirmedS%d.Visible=false",lateralBarNumber, splNumber);
				    break;
			    case State::WECMODE_AUTO:
				    if ( wecState.Control.isSyncModeIdle() )
					    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.stb_SpliceConfirmedS%d.Visible=false",lateralBarNumber, splNumber);
				    else
					    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.stb_SpliceConfirmedS%d.Visible=true",lateralBarNumber, splNumber);
				    break;
			    case State::WECMODE_OVERRIDE:
				    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.stb_SpliceConfirmedS%d.Visible=false",lateralBarNumber, splNumber);
			    case State::WECMODE_NOMODE:
			    default:
				    break;
		    }

	      o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.stb_SpliceConfirmedS%d.Text=%s",lateralBarNumber, 
		      splNumber, theSplicerState.Control.getSpliceConfirmed() ? L"Sync" : L"No Sync");
	      o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.stb_SpliceConfirmedS%d.Active=%s",lateralBarNumber, 
		      splNumber, theSplicerState.Control.getSpliceConfirmed() ? L"true" : L"false");

        if (theSplicerState.CtrlData.getReadyToSplice())
        {
		      o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.ttl_Splicer%d.ForeColor=Lime",lateralBarNumber, splNumber);
        }
        else
        {
		      o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.ttl_Splicer%d.ForeColor=Red",lateralBarNumber, splNumber);
        }

		    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d._setPaperTypeForSplicer(i[%d],s[%s],i[%d],i[%d],i[%d],i[%d],s[%s],i[%d],i[%d])",
          lateralBarNumber,
			    splNumber, 
			    theSplicerState.rtData.getPaperType(splNumber, State::RtData::iscurrent), 
			    theSplicerState.rtData.getPaperWeigthFactor_UWg_m(splNumber, State::RtData::iscurrent),
			    theSplicerState.rtData.getWidth_UDmm(State::RtData::iscurrent),
			    theSplicerState.Monitor.getProductionRemaining_UDmm() / 1000,
			    theSplicerState.Monitor.getProductionUntilSplice_UDmm() / 1000,
			    theSplicerState.rtData.getPaperType(splNumber, State::RtData::isnext),
			    theSplicerState.rtData.getPaperWeigthFactor_UWg_m(splNumber, State::RtData::isnext),
			    theSplicerState.rtData.getWidth_UDmm(State::RtData::isnext)
			    );
	    }

		  //syncButtons
		  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.SyncButtonsActive(b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s])",
        lateralBarNumber,
			  String::Bool2Alpha(wecState.Control.isSyncModeIdle()),
			  String::Bool2Alpha(wecState.Control.isSyncModeMetres()),
			  String::Bool2Alpha(wecState.Control.isSyncModeNow()),
			  String::Bool2Alpha(wecState.Control.isSyncModeOnSplicerNumber(1)),
			  String::Bool2Alpha(wecState.Control.isSyncModeOnSplicerNumber(2)),
			  String::Bool2Alpha(wecState.Control.isSyncModeOnSplicerNumber(3)),
			  String::Bool2Alpha(wecState.Control.isSyncModeOnSplicerNumber(4)),
			  String::Bool2Alpha(wecState.Control.isSyncModeOnSplicerNumber(5)));

		  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.SyncButtonsEnabled(b[%s])",lateralBarNumber,
			  String::Bool2Alpha(! wecState.Control.isSyncModeChange() && wecState.Control.isModeAuto()));

      //Meters left to Splice
      //S1
	    if (C_PageManager::SMState.getSplicerState(1).Monitor.getNotReadyAtEarlyWarning())
		    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S1Meters.BackColor=Red",lateralBarNumber);
	    else
	    {
		    if (C_PageManager::SMState.getSplicerState(1).Monitor.getEarlyWarning())
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S1Meters.BackColor=Yellow",lateralBarNumber);
        else if (C_PageManager::SMState.getSplicerState(1).Monitor.getPrefill())
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S1Meters.BackColor=GreenYellow",lateralBarNumber);
		    else if ( C_PageManager::SMState.getSplicerState(1).Control.getSpliceConfirmed() &&
		           ( C_PageManager::SMState.getSplicerState(1).Monitor.getButt_UDmm() > 0	||
    			     wecState.Control.getSyncMode()==State::SYNCMODE_SPLICER1 ) )
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S1Meters.BackColor=LightBlue",lateralBarNumber);
        else
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S1Meters.BackColor=WhiteSmoke",lateralBarNumber);
	    }

      //S2
	    if (C_PageManager::SMState.getSplicerState(2).Monitor.getNotReadyAtEarlyWarning())
		    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S2Meters.BackColor=Red",lateralBarNumber);
	    else
	    {
		    if (C_PageManager::SMState.getSplicerState(2).Monitor.getEarlyWarning())
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S2Meters.BackColor=Yellow",lateralBarNumber);
        else if (C_PageManager::SMState.getSplicerState(2).Monitor.getPrefill())
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S2Meters.BackColor=GreenYellow",lateralBarNumber);
		    else if ( C_PageManager::SMState.getSplicerState(2).Control.getSpliceConfirmed() &&
		           ( C_PageManager::SMState.getSplicerState(2).Monitor.getButt_UDmm() > 0	||
    			     wecState.Control.getSyncMode()==State::SYNCMODE_SPLICER2 ) )
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S2Meters.BackColor=LightBlue",lateralBarNumber);
		    else
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S2Meters.BackColor=WhiteSmoke",lateralBarNumber);
	    }

      //S3
	    if (C_PageManager::SMState.getSplicerState(3).Monitor.getNotReadyAtEarlyWarning())
		    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S3Meters.BackColor=Red",lateralBarNumber);
	    else
	    {
		    if (C_PageManager::SMState.getSplicerState(3).Monitor.getEarlyWarning())
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S3Meters.BackColor=Yellow",lateralBarNumber);
        else if (C_PageManager::SMState.getSplicerState(3).Monitor.getPrefill())
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S3Meters.BackColor=GreenYellow",lateralBarNumber);
		    else if ( C_PageManager::SMState.getSplicerState(3).Control.getSpliceConfirmed() &&
		           ( C_PageManager::SMState.getSplicerState(3).Monitor.getButt_UDmm() > 0	||
    			     wecState.Control.getSyncMode()==State::SYNCMODE_SPLICER3 ) )
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S3Meters.BackColor=LightBlue",lateralBarNumber);
		    else
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S3Meters.BackColor=WhiteSmoke",lateralBarNumber);
	    }

      //S4
	    if (C_PageManager::SMState.getSplicerState(4).Monitor.getNotReadyAtEarlyWarning())
		    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S4Meters.BackColor=Red",lateralBarNumber);
	    else
	    {
		    if (C_PageManager::SMState.getSplicerState(4).Monitor.getEarlyWarning())
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S4Meters.BackColor=Yellow",lateralBarNumber);
        else if (C_PageManager::SMState.getSplicerState(4).Monitor.getPrefill())
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S4Meters.BackColor=GreenYellow",lateralBarNumber);
		    else if ( C_PageManager::SMState.getSplicerState(4).Control.getSpliceConfirmed() &&
		           ( C_PageManager::SMState.getSplicerState(4).Monitor.getButt_UDmm() > 0	||
    			     wecState.Control.getSyncMode()==State::SYNCMODE_SPLICER4 ) )
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S4Meters.BackColor=LightBlue",lateralBarNumber);
		    else
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S4Meters.BackColor=WhiteSmoke",lateralBarNumber);
	    }

      //S5
      if (C_PageManager::SMState.getSplicerState(5).Monitor.getNotReadyAtEarlyWarning())
		    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S5Meters.BackColor=Red",lateralBarNumber);
	    else
	    {
		    if (C_PageManager::SMState.getSplicerState(5).Monitor.getEarlyWarning())
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S5Meters.BackColor=Yellow",lateralBarNumber);
        else if (C_PageManager::SMState.getSplicerState(5).Monitor.getPrefill())
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S5Meters.BackColor=GreenYellow",lateralBarNumber);
		    else if ( C_PageManager::SMState.getSplicerState(5).Control.getSpliceConfirmed() &&
		           ( C_PageManager::SMState.getSplicerState(5).Monitor.getButt_UDmm() > 0	||
    			     wecState.Control.getSyncMode()==State::SYNCMODE_SPLICER5 ) )
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S5Meters.BackColor=LightBlue",lateralBarNumber);
        else
			    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.nud_S5Meters.BackColor=WhiteSmoke",lateralBarNumber);
	    }

      C_PageManager::SMOrder.WETGetCurrentProgram(currentProgramData);
		  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.tlb_WetEndMetersLeft.Value=%d",lateralBarNumber,currentProgramData.getProgramMetersLeft_UDm());
		  if ( C_PageManager::SMState.getAOCState().isSourceDryEnd() != SourceDryEnd )
		  {
			  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.tlb_WetEndMetersLeft.BackColor=%s",lateralBarNumber,
				  (  C_PageManager::SMState.getAOCState().isSourceDryEnd() )  ? L"WhiteSmoke" : L"Yellow");
			  SourceDryEnd = C_PageManager::SMState.getAOCState().isSourceDryEnd();
		  }


    }

  case 11:
    //o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d._UpdateMoistureTrace(i[%d],i[%d])",
    //  lateralBarNumber,
    //  C_PageManager::SMState.getTemperature_MoistureSensorState().BOTTOM_LINER_getMoistureSensorAverageValue_UGc(),
    //  C_PageManager::SMState.getTemperature_MoistureSensorState().UPPER_LINER_getMoistureSensorAverageValue_UGc());
    o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d._UpdateMoistureTrace(i[%d],i[%d])",
      lateralBarNumber,
      C_PageManager::SMState.getTemperature_MoistureSensorState().BOTTOM_LINER_getMoistureSensorLastValue_UGc(),
      C_PageManager::SMState.getTemperature_MoistureSensorState().UPPER_LINER_getMoistureSensorLastValue_UGc());
    break;

  //agrassi_FI08-09-2010_1
  case 8:
    {
     static int oldStateID=-1;
     //agrassi_FI22-08-2011_1
     //State::SSPlcState ssState;
     //int stateID = C_PageManager::SMState.getSSPlcState(ssState);
     State::SlitterScorerState ssState;
     int stateID = C_PageManager::SMState.getSlitterScorerState(ssState);
     if (stateID!=oldStateID)
     {
        oldStateID=stateID;
				int shaft=ssState.plcState.getGrindingData().shaft;
				int slit=ssState.plcState.getGrindingData().slit;
       	wchar_t grindCounters[30];
				int cnt=0;
				for (int i=0; i<MAX_SLITS_PER_SHAFT; i++)
					cnt+=swprintf(grindCounters+cnt, L"%d%s", ssState.plcState.getGrindingData().multiple.slitToGrind[i], ((i<MAX_SLITS_PER_SHAFT-1)?Network::SEP:L""));
				wchar_t slitsData[NUM_SLIT_SHAFTS*(MAX_SLITS_PER_SHAFT+1)*10];
        cnt=0;
        //agrassi_FI22-08-2011_1
        if (lineConfig.slitterScorer.isTwin2() || lineConfig.slitterScorer.isCompact2())
        {
          SetupData* setupData=new SetupData();
		      //Send Slits position Ax1
          bool isCurr=false;
		      if ( ssState.monitor.getIsCurrentOnSlit1().getValue() )
          {
            isCurr=true;
			      C_PageManager::SMOrder.DRYFindProgramID(ssState.orders.getIDCurrent(),4,*setupData);
          }
		      else
			      C_PageManager::SMOrder.DRYFindProgramID(ssState.orders.getIDNext(),4,*setupData);
		      for (int slt=0; slt<MAX_SLITS_PER_SHAFT; slt++)
			      cnt+=swprintf(slitsData+cnt, L"%d%s", setupData->calculations->slitShafts[0].slitsPositions_UDdmm[slt], Network::SEP);
          cnt+=swprintf(slitsData+cnt, L"%d%s", (isCurr ? ssState.plcState.getPlungedSlits(Slit1) : 0), Network::SEP);
		      cnt+=swprintf(slitsData+cnt, L"%s%s", String::Bool2Alpha(true), Network::SEP);

		      //Send Slits position Ax2
          isCurr=false;
		      if ( ssState.monitor.getIsCurrentOnSlit2().getValue() )
          {
            isCurr=true;
			      C_PageManager::SMOrder.DRYFindProgramID(ssState.orders.getIDCurrent(),4,*setupData);
          }
		      else
			      C_PageManager::SMOrder.DRYFindProgramID(ssState.orders.getIDNext(),4,*setupData);

		      for (int slt=0; slt<MAX_SLITS_PER_SHAFT; slt++)
			      cnt+=swprintf(slitsData+cnt, L"%d%s", setupData->calculations->slitShafts[0].slitsPositions_UDdmm[slt], Network::SEP);
          cnt+=swprintf(slitsData+cnt, L"%d%s", (isCurr ? ssState.plcState.getPlungedSlits(Slit2) : 0), Network::SEP);
		      cnt+=swprintf(slitsData+cnt, L"%s%s", String::Bool2Alpha(true), L"");

          delete setupData;
        }
        else
        {
				  for (Shaft sh=Slit1; sh<=Slit2; sh++)
				  {
					  for (int slt=0; slt<MAX_SLITS_PER_SHAFT; slt++)
					  {
						  cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.plcState.getSlitPositions(sh, slt), Network::SEP);
					  }
					  cnt+=swprintf(slitsData+cnt, L"%d%s", ssState.plcState.getPlungedSlits(sh), Network::SEP);
					  cnt+=swprintf(slitsData+cnt, L"%s%s", String::Bool2Alpha(ssState.plcState.getRequestsGoodSlitPositions(sh)), (sh==Slit1?(Network::SEP):L""));
          }
        }
				o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d._UpdateDRYBlades(B[%s],I[%d],I[%d],B[%s],S[%s],B[%s],S[%s])", lateralBarNumber, 
						String::Bool2Alpha(ssState.plcState.getMachineStatusGrinding()), shaft, slit, String::Bool2Alpha(ssState.plcState.getGrindingData().multiple.running), 
 						grindCounters, String::Bool2Alpha(ssState.plcState.getMaintenanceStatusMachineInMaintenance()), slitsData);
        bool pushEnabled=(ssState.plcState.getTaskStatusPLCComm() && ssState.plcState.getTaskStatusPLCRun());
 				o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.GrindSlit.Enabled=%s", lateralBarNumber, String::Bool2Alpha(pushEnabled && ssState.plcState.getFunctionalityStatusGrindEnable() && (!ssState.plcState.getManualGrindingDisabled())));
        o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.StopMultiple.Visible=%s", lateralBarNumber, String::Bool2Alpha(ssState.plcState.getGrindingData().multiple.running));
				bool canEnableGrinding=(ssState.plcState.getFunctionalityStatusGrindingAlarmPresentEnable()) && pushEnabled;
				o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar.lateralBarItem%d.EnableGrinding.Visible=%s", lateralBarNumber, String::Bool2Alpha(canEnableGrinding));
      }
    }
  }
}


DWORD WINAPI C_LateralBar::LateralBar(LPVOID  lpParam)
{
  o_socket.Send(Network::MSG_PAG_REF,L"lateralBar_lateralBar._Show()");

	while(!end_LateralBar)
	{
    Sleep(500);
    LateralBar_UpdateTab(1,tab_lateralBar_1);
    Sleep(500);
    LateralBar_UpdateTab(2,tab_lateralBar_2);
    Sleep(500);
    LateralBar_UpdateTab(3,tab_lateralBar_3);
	}
	
	logger.Fine(__FUNCTION__, L"Terminated lateralBar page threads");

	return ERROR_SUCCESS;
}