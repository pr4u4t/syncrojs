#include ".\C_StatusBar.h"

#include "..\C_PageManager.h"
//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

bool C_StatusBar::end_DryEnd;
bool C_StatusBar::end_WetEnd;
bool C_StatusBar::end_Twin400;

C_StatusBar::C_StatusBar(void)
{
	pid_Sta_bar=NULL;
	TerminateThread(false);
}

C_StatusBar::~C_StatusBar(void)
{
}

bool C_StatusBar::ChooseStaBar(String::StrType  theStaBar)
{

	if(theStaBar.CompareNoCase(L"statusBarDryEnd"))
	{
		end_DryEnd = false;
		pid_Sta_bar=::CreateThread(NULL,0,DryEnd,NULL,0,NULL);	
	}

	if(theStaBar.CompareNoCase(L"statusBarWetEnd"))
	{
		end_WetEnd = false;
		pid_Sta_bar=::CreateThread(NULL,0,WetEnd,NULL,0,NULL);	
	}

	if(theStaBar.CompareNoCase(L"statusBarTwin400"))
	{
		end_Twin400 = false;
		pid_Sta_bar=::CreateThread(NULL,0,Twin400,NULL,0,NULL);	
	}

	if (pid_Sta_bar == NULL)
	{
		logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"ERROR: Cannot create StaBar thread ... %d",GetLastError());
		return false;
	}
	return true;
}

void C_StatusBar::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_Sta_bar!=NULL)
		{
			BOOL res = ::TerminateThread(pid_Sta_bar,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread StatusBar ... %d", ::GetLastError());
		}
	}
	else
	{
		end_DryEnd = true;
		end_WetEnd = true;
		end_Twin400 = true;
	}
}

DWORD C_StatusBar::SuspendThread(void)
{
	if(::SuspendThread(pid_Sta_bar)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Status Bar Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_StatusBar::ResumeThread(void)
{
	if(::ResumeThread(pid_Sta_bar)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Status Bar Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}


DWORD WINAPI C_StatusBar::WetEnd(LPVOID  lpParam)
{
	State::SpeedState speedState;
	speedState.Clean();

	State::WECState wecState;
	wecState.Clear();
	
	State::ReadyMachineState rdyRunState;
	rdyRunState.Clean();

	State::PreHeatersTernState preHeatersTernState;
	preHeatersTernState.Clean();

	ProgramData currentProgram;
	currentProgram.Clear();

	int producPercentToGo = 0;
	int numPrograms = 0;
	int bridgeContent;
  int meterLeft = 0;
  int programNumber = 0;
  int speedDB = 0;
  int speedSF1 = 0;
  int speedSF2 = 0;
  //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
  int speedSF3 = 0;

  int sp1ProductUntilSplice = 0;
  int sp2ProductUntilSplice = 0;
  int sp3ProductUntilSplice = 0;
  int sp4ProductUntilSplice = 0;
  int sp5ProductUntilSplice = 0;
  //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
  int sp6ProductUntilSplice = 0;
  int sp7ProductUntilSplice = 0;

	wchar_t PushFaster[MAX_PATH]; 
	wchar_t PushSlower[MAX_PATH];

	bool first = true;
  bool dbZoneActive = false;
  bool sf1ZoneActive = false; 
  bool sf2ZoneActive = false;
  //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
  bool sf3ZoneActive = false;
  bool dbEstopPres = false;
  bool dbAlarmed = false;
  bool guEstopPres = false;
  bool guAlarmed = false;
  bool phEstopPres = false;
  bool phAlarmed = false;
  bool s1EstopPres = false;
  bool s1Alarmed = false;
  bool s2EstopPres = false;
  bool s2Alarmed = false;
  bool s3EstopPres = false;
  bool s3Alarmed = false;
  bool s4EstopPres = false;
  bool s4Alarmed = false;
  bool s5EstopPres = false;
	bool s5Alarmed = false;
  //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
  bool s6EstopPres = false;
  bool s6Alarmed = false;
  bool s7EstopPres = false;
	bool s7Alarmed = false;

	bool statusSimulation = false;

  LineConfig cfg;
  C_PageManager::SMLineConfig.getLineConfig(cfg);

	o_socket.Send(Network::MSG_PAG_REF, L"statusBar_statusBarWetEnd._ShowStatusBarWetEnd(b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s])",
    String::Bool2Alpha(cfg.macEnable.getDoubleBacker()),
    String::Bool2Alpha(cfg.macEnable.getGlueUnit()),
    String::Bool2Alpha(cfg.macEnable.getPreHeatersTern()),
    String::Bool2Alpha(cfg.macEnable.getSingleFacer(1)),
    String::Bool2Alpha(cfg.macEnable.getSingleFacer(2)),
    String::Bool2Alpha(cfg.macEnable.getSingleFacer(3)),
    String::Bool2Alpha(cfg.macEnable.getSplicer(1)),
    String::Bool2Alpha(cfg.macEnable.getSplicer(2)),
    String::Bool2Alpha(cfg.macEnable.getSplicer(3)),
    String::Bool2Alpha(cfg.macEnable.getSplicer(4)),
    String::Bool2Alpha(cfg.macEnable.getSplicer(5)),
    String::Bool2Alpha(cfg.macEnable.getSplicer(6)),
    String::Bool2Alpha(cfg.macEnable.getSplicer(7)),
    String::Bool2Alpha(cfg.macEnable.getSplicer(12)),
    String::Bool2Alpha(cfg.macEnable.getSplicer(14)),
    String::Bool2Alpha(cfg.macEnable.getSplicer(16))
    );

	while(!end_WetEnd)
	{
		C_PageManager::SMState.getWECState(wecState);

    //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
    //zone Active/not Active
    if ( C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.CtrlData.orders.isActive() != dbZoneActive ||
         C_PageManager::SMState.getZoneSingleFacerState(1).singleFacerState.CtrlData.orders.isActive() != sf1ZoneActive ||
         C_PageManager::SMState.getZoneSingleFacerState(2).singleFacerState.CtrlData.orders.isActive() != sf2ZoneActive ||
         C_PageManager::SMState.getZoneSingleFacerState(3).singleFacerState.CtrlData.orders.isActive() != sf3ZoneActive ||
         first )
    {
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd._setActive(b[%s],b[%s],b[%s],b[%s])",
			  String::Bool2Alpha(C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.CtrlData.orders.isActive()),
			  String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(1).singleFacerState.CtrlData.orders.isActive()),
			  String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(2).singleFacerState.CtrlData.orders.isActive()),
        String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(3).singleFacerState.CtrlData.orders.isActive()));
      dbZoneActive = C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.CtrlData.orders.isActive();
      sf1ZoneActive = C_PageManager::SMState.getZoneSingleFacerState(1).singleFacerState.CtrlData.orders.isActive();
      sf2ZoneActive = C_PageManager::SMState.getZoneSingleFacerState(2).singleFacerState.CtrlData.orders.isActive();
      sf3ZoneActive = C_PageManager::SMState.getZoneSingleFacerState(3).singleFacerState.CtrlData.orders.isActive();
    }

		if ( C_PageManager::SMState.getSimulationState().getSimulationActive() != statusSimulation )
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.ttl_SimulationTitle.Visible=%s", String::Bool2Alpha(C_PageManager::SMState.getSimulationState().getSimulationActive()));
		statusSimulation = C_PageManager::SMState.getSimulationState().getSimulationActive();

		//progress bars
		C_PageManager::SMOrder.WETGetCurrentProgram(currentProgram);
    if (currentProgram.getProgramMetersLeft_UDm() != meterLeft ||
        first )
    {
		  producPercentToGo = (int)((double)( (double)currentProgram.getProgramMetersLeft_UDm() / currentProgram.getOrderLength()) * 100);
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.prb_WetEnd.Percentage=%d",(100-producPercentToGo));
		  //meters Left
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.tlb_WetEndMetersLeft.Value=%d",currentProgram.getProgramMetersLeft_UDm());
      meterLeft = currentProgram.getProgramMetersLeft_UDm();
    }

		//Programs Number
    if ( C_PageManager::SMOrder.WETGetNumProgramInList() != programNumber ||
          first )
    {
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.tlb_WetEndProgram.Value=%d",C_PageManager::SMOrder.WETGetNumProgramInList());
      programNumber = C_PageManager::SMOrder.WETGetNumProgramInList();
    }

    //RFAZZI_FI20/05/2010_1. Aggiunta visualizzazione in barra di stato Wet della macchina che ha fermato la linea. Invertiti anche i campi di Metri e Tempo a Cambio Wet. 
    SYSTEMTIME timeOfFirstStop;
    wchar_t reasonFirstOfStop[MAX_PATH] = L"";
    wchar_t timeOfFirstStopStr[MAX_PATH] = L"";

    if(C_PageManager::SMState.getStopReasonStateMODIFY().getFirstReasonOfStopItem(timeOfFirstStop, reasonFirstOfStop))
    {
      String::SYSTEMTIME2Alpha(timeOfFirstStop,timeOfFirstStopStr,MAX_PATH);
      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd._setFirstReasonOfStop(s[%s],s[%s])", timeOfFirstStopStr, reasonFirstOfStop);
    }
    else
      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd._setFirstReasonOfStop(s[ ],s[ ])");

    if ( numPrograms != C_PageManager::SMOrder.WETGetNumProgramInList() ||
        first )
    {
      numPrograms = C_PageManager::SMOrder.WETGetNumProgramInList();
		  if ( numPrograms < 3 )
			  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.tlb_WetEndProgram.ForeColor=Red");
		  else
			  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.tlb_WetEndProgram.ForeColor=Black");
    }
			
		//Speeds
    //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
		C_PageManager::SMState.getSpeedState(speedState);
    if ( C_PageManager::SMState.getZoneDoubleBackerState().Monitor.getSpeed_UVmm_m() != speedDB ||
         C_PageManager::SMState.getZoneSingleFacerState(1).Monitor.getSpeed_UVmm_m() != speedSF1 ||
         C_PageManager::SMState.getZoneSingleFacerState(2).Monitor.getSpeed_UVmm_m() != speedSF2 ||
         C_PageManager::SMState.getZoneSingleFacerState(3).Monitor.getSpeed_UVmm_m() != speedSF3 ||
         first )
    {
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_SpeedLine.Value=%d",C_PageManager::SMState.getZoneDoubleBackerState().Monitor.getSpeed_UVmm_m()); 
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_SpeedZone1.Value=%d",C_PageManager::SMState.getZoneSingleFacerState(1).Monitor.getSpeed_UVmm_m()); 
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_SpeedZone2.Value=%d",C_PageManager::SMState.getZoneSingleFacerState(2).Monitor.getSpeed_UVmm_m()); 
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_SpeedZone3.Value=%d",C_PageManager::SMState.getZoneSingleFacerState(3).Monitor.getSpeed_UVmm_m()); 
      speedDB = C_PageManager::SMState.getZoneDoubleBackerState().Monitor.getSpeed_UVmm_m();
      speedSF1 = C_PageManager::SMState.getZoneSingleFacerState(1).Monitor.getSpeed_UVmm_m();
      speedSF2 = C_PageManager::SMState.getZoneSingleFacerState(2).Monitor.getSpeed_UVmm_m();
      speedSF3 = C_PageManager::SMState.getZoneSingleFacerState(3).Monitor.getSpeed_UVmm_m();
    }

		//Increase / Decrease commands
		wchar_t dummyPushFaster[MAX_PATH]; 
		wchar_t dummyPushSlower[MAX_PATH];
		speedState.pushButton.getFasterPushButtonPressed(Network::SEP,dummyPushFaster,MAX_PATH);
		speedState.pushButton.getSlowerPushButtonPressed(Network::SEP,dummyPushSlower,MAX_PATH);
		if ( (wcscmp(dummyPushFaster,PushFaster)!=0) || (wcscmp(dummyPushSlower,PushSlower)!=0) || (first) )
		{
			speedState.pushButton.getFasterPushButtonPressed(Network::SEP,PushFaster,MAX_PATH);
			speedState.pushButton.getSlowerPushButtonPressed(Network::SEP,PushSlower,MAX_PATH);
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd._setPushButtonPressed(s[%s],s[%s])",PushFaster,PushSlower);
		}

		//SYC IncDec
		C_PageManager::SMState.getZoneDoubleBackerState().Control.getSpeedCommandToStr(dummyPushFaster, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd._setStateButtonSYCIncDec(s[DB],s[%s])",dummyPushFaster);
		C_PageManager::SMState.getZoneSingleFacerState(1).Control.getSpeedCommandToStr(dummyPushFaster, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd._setStateButtonSYCIncDec(s[SF1],s[%s])",dummyPushFaster);
		C_PageManager::SMState.getZoneSingleFacerState(2).Control.getSpeedCommandToStr(dummyPushFaster, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd._setStateButtonSYCIncDec(s[SF2],s[%s])",dummyPushFaster);
    //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
		C_PageManager::SMState.getZoneSingleFacerState(3).Control.getSpeedCommandToStr(dummyPushFaster, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd._setStateButtonSYCIncDec(s[SF3],s[%s])",dummyPushFaster);

	
		//Ready To run
		C_PageManager::SMState.getReadyToRunState(rdyRunState);
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.pct_DBNotRdyRun.Visible=%s",(rdyRunState.getDBReady())?L"false":L"true");
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.pct_GUNotRdyRun.Visible=%s",(rdyRunState.getGUReady())?L"false":L"true");
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.pct_PHTNotRdyRun.Visible=%s",(rdyRunState.getPHTReady())?L"false":L"true");
    //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
		for(int i = 1; i <= 7; i++)
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.pct_SPL%dNotRdyRun.Visible=%s",i,(rdyRunState.getSPLReady(i))?L"false":L"true");
		for(int i = 1; i <= 3; i++)
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.pct_SF%dNotRdyRun.Visible=%s",i,(rdyRunState.getSFReady(i))?L"false":L"true");

		//Mode Zone DB (WEC)
		switch (wecState.Control.getMode())
		{
		case State::WECMODE_NOMODE:
		case State::WECMODE_MANUAL:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd._setWETStatus(s[Manual])");
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.ToggleAutoManWetEnd.Auto=false");
			break;
		case State::WECMODE_AUTO:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd._setWETStatus(s[Auto])");
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.ToggleAutoManWetEnd.Auto=true");
			break;
		case State::WECMODE_OVERRIDE:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd._setWETStatus(s[Override])");
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.ToggleAutoManWetEnd.switching=true");
			break;
		default:
			break;
		}

		switch ( wecState.Control.getSyncMode() )
		{
			case State::SYNCMODE_OFF:
				break;
			case State::SYNCMODE_IDLE:
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.lbl_WECType_YY.Text=§%S", stringer(SYCS_41));
				break;
			case State::SYNCMODE_METRES:
					o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.lbl_WECType_YY.Text=§%S", stringer(SYCS_42));
				break;
			case State::SYNCMODE_SPLICER1:																											
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.lbl_WECType_YY.Text=§%S #1", stringer(SYCS_43));
				break;
			case State::SYNCMODE_SPLICER2:
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.lbl_WECType_YY.Text=§%S #2", stringer(SYCS_43));
				break;
			case State::SYNCMODE_SPLICER3:
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.lbl_WECType_YY.Text=§%S #3", stringer(SYCS_43));
				break;
			case State::SYNCMODE_SPLICER4:
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.lbl_WECType_YY.Text=§%S #4", stringer(SYCS_43));
				break;
			case State::SYNCMODE_SPLICER5:
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.lbl_WECType_YY.Text=§%S #5", stringer(SYCS_43));
				break;
			case State::SYNCMODE_SPLICER6:
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.lbl_WECType_YY.Text=§%S #6", stringer(SYCS_43));
				break;
			case State::SYNCMODE_SPLICER7:
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.lbl_WECType_YY.Text=§%S #7", stringer(SYCS_43));
				break;		
			case State::SYNCMODE_CHANGE:
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.lbl_WECType_YY.Text=§%S", stringer(SYCS_44));
				break;		
		}

		//Mode Zone 1
		switch (C_PageManager::SMState.getZoneSingleFacerState(1).Control.getMode())
		{
		case State::WECMODE_NOMODE:
			break;
		case State::WECMODE_MANUAL:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.ToggleAutoManZone1.Auto=false");
			break;
		case State::WECMODE_AUTO:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.ToggleAutoManZone1.Auto=true");
			break;
		case State::WECMODE_OVERRIDE:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.ToggleAutoManZone1.switching=true");
			break;
		default:
			break;
		}

		//Mode Zone 2
		switch (C_PageManager::SMState.getZoneSingleFacerState(2).Control.getMode())
		{
		case State::WECMODE_NOMODE:
			break;
		case State::WECMODE_MANUAL:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.ToggleAutoManZone2.Auto=false");
			break;
		case State::WECMODE_AUTO:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.ToggleAutoManZone2.Auto=true");
			break;
		case State::WECMODE_OVERRIDE:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.ToggleAutoManZone2.switching=true");
			break;
		default:
			break;
		}

    //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
		//Mode Zone 3
		switch (C_PageManager::SMState.getZoneSingleFacerState(3).Control.getMode())
		{
		case State::WECMODE_NOMODE:
			break;
		case State::WECMODE_MANUAL:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.ToggleAutoManZone3.Auto=false");
			break;
		case State::WECMODE_AUTO:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.ToggleAutoManZone3.Auto=true");
			break;
		case State::WECMODE_OVERRIDE:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.ToggleAutoManZone3.switching=true");
			break;
		default:
			break;
		}

    //Machines Emergencies, Alarms and Warnings (local mode)		
    //db
		if(C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.CtrlData.orders.getIsOnLine().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.DoubleBacker.Warned=%s",String::Bool2Alpha(!C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.CtrlData.orders.getIsOnLine()));

    if ( C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.CtrlData.alarms.getIsEStopPressed() != dbEstopPres ||
          first )
    {
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.DoubleBacker.InEmergency=%s",
			  String::Bool2Alpha(C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.CtrlData.alarms.getIsEStopPressed()));
      dbEstopPres = C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.CtrlData.alarms.getIsEStopPressed();
    }
    if ( C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.CtrlData.alarms.isAlarmed() != dbAlarmed ||
          first )
    {
      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.DoubleBacker.Alarmed=%s",
			  String::Bool2Alpha(C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.CtrlData.alarms.isAlarmed()));
      dbAlarmed = C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.CtrlData.alarms.isAlarmed();
    }

    //gu
    //RFAZZI_FI10/08/2011_1. Aggiunta visualizzazione in barra di stato WetEnd di GU Warned
		if(C_PageManager::SMState.getZoneDoubleBackerState().glueUnitState.CtrlData.getMachineControlRemote().getEnable() )
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.GlueUnit.Warned=%s",String::Bool2Alpha(!C_PageManager::SMState.getZoneDoubleBackerState().glueUnitState.CtrlData.getMachineControlRemote()));

    if ( C_PageManager::SMState.getZoneDoubleBackerState().glueUnitState.CtrlData.Alarms.getIsEStopPressed() != guEstopPres ||
          first )
    {
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.GlueUnit.InEmergency=%s",
			  String::Bool2Alpha(C_PageManager::SMState.getZoneDoubleBackerState().glueUnitState.CtrlData.Alarms.getIsEStopPressed()));
      guEstopPres = C_PageManager::SMState.getZoneDoubleBackerState().glueUnitState.CtrlData.Alarms.getIsEStopPressed();
    }
    if ( C_PageManager::SMState.getZoneDoubleBackerState().glueUnitState.CtrlData.Alarms.isAlarmed() != guAlarmed ||
          first )
    {
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.GlueUnit.Alarmed=%s",
			  String::Bool2Alpha(C_PageManager::SMState.getZoneDoubleBackerState().glueUnitState.CtrlData.Alarms.isAlarmed()));
      guAlarmed = C_PageManager::SMState.getZoneDoubleBackerState().glueUnitState.CtrlData.Alarms.isAlarmed();
    }

		//ABello_FI09/07/2007_2 Non era gestito il fungo di emergenza sulla console del client zona piani
    //pht
    //if ( C_PageManager::SMState.getZoneDoubleBackerState().preHeatersTernState.CtrlData.Alarms.getIsEStopPressed() != phEstopPres ||
    //      first )
    //{
		  //o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.PreHeaterTern.InEmergency=%s",
			 // String::Bool2Alpha(C_PageManager::SMState.getZoneDoubleBackerState().preHeatersTernState.CtrlData.Alarms.getIsEStopPressed()));
    //  phEstopPres = C_PageManager::SMState.getZoneDoubleBackerState().preHeatersTernState.CtrlData.Alarms.getIsEStopPressed();
    //}
		bool emerPHT = (C_PageManager::SMState.getZoneDoubleBackerState().preHeatersTernState.CtrlData.Alarms.getIsEStopPressed().getValue() || C_PageManager::SMState.getZoneDoubleBackerState().preHeatersTernState.CtrlData.Alarms.getIsEStopPressedOnDBConsoleClient().getValue());
    if ( emerPHT != phEstopPres ||
          first )
    {
			
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.PreHeaterTern.InEmergency=%s", String::Bool2Alpha(emerPHT));
      phEstopPres = emerPHT;
    }

    if ( C_PageManager::SMState.getZoneDoubleBackerState().preHeatersTernState.CtrlData.Alarms.isAlarmed() != phAlarmed ||
      first )
    {
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.PreHeaterTern.Alarmed=%s",
			  String::Bool2Alpha(C_PageManager::SMState.getZoneDoubleBackerState().preHeatersTernState.CtrlData.Alarms.isAlarmed()));
      phAlarmed = C_PageManager::SMState.getZoneDoubleBackerState().preHeatersTernState.CtrlData.Alarms.isAlarmed();
    }

    //ABELLO_FI07/05/10 Aggiunta gestione del non pronta alla giunta della giuntatrice sulla barra di stato WET.
    //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
    o_socket.Send(Network::MSG_PAG_REF, L"statusBar_statusBarWetEnd._setNotReadyToSplice(b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s])",
      String::Bool2Alpha(C_PageManager::SMState.getSplicerState(1).rtData.getActive(State::RtData::iscurrent) && C_PageManager::SMState.getSplicerState(1).Control.isModeAuto() && !C_PageManager::SMState.getSplicerState(1).CtrlData.getReadyToSplice()),
      String::Bool2Alpha(C_PageManager::SMState.getSplicerState(2).rtData.getActive(State::RtData::iscurrent) && C_PageManager::SMState.getSplicerState(2).Control.isModeAuto() && !C_PageManager::SMState.getSplicerState(2).CtrlData.getReadyToSplice()),
      String::Bool2Alpha(C_PageManager::SMState.getSplicerState(3).rtData.getActive(State::RtData::iscurrent) && C_PageManager::SMState.getSplicerState(3).Control.isModeAuto() && !C_PageManager::SMState.getSplicerState(3).CtrlData.getReadyToSplice()),
      String::Bool2Alpha(C_PageManager::SMState.getSplicerState(4).rtData.getActive(State::RtData::iscurrent) && C_PageManager::SMState.getSplicerState(4).Control.isModeAuto() && !C_PageManager::SMState.getSplicerState(4).CtrlData.getReadyToSplice()),
      String::Bool2Alpha(C_PageManager::SMState.getSplicerState(5).rtData.getActive(State::RtData::iscurrent) && C_PageManager::SMState.getSplicerState(5).Control.isModeAuto() && !C_PageManager::SMState.getSplicerState(5).CtrlData.getReadyToSplice()),
      String::Bool2Alpha(C_PageManager::SMState.getSplicerState(6).rtData.getActive(State::RtData::iscurrent) && C_PageManager::SMState.getSplicerState(6).Control.isModeAuto() && !C_PageManager::SMState.getSplicerState(6).CtrlData.getReadyToSplice()),
      String::Bool2Alpha(C_PageManager::SMState.getSplicerState(7).rtData.getActive(State::RtData::iscurrent) && C_PageManager::SMState.getSplicerState(7).Control.isModeAuto() && !C_PageManager::SMState.getSplicerState(7).CtrlData.getReadyToSplice())
      );
    
	  //PMASSARO - MANANGEMENT OF ACTIVE ROLL FOR SPLICERS
	  o_socket.Send(Network::MSG_PAG_REF, L"statusBar_statusBarWetEnd._setSplicerRollActive(b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s])",
		  String::Bool2Alpha(C_PageManager::SMState.getSplicerState(1).Monitor.getRunningRollStand_A()),
		  String::Bool2Alpha(C_PageManager::SMState.getSplicerState(2).Monitor.getRunningRollStand_A()),
		  String::Bool2Alpha(C_PageManager::SMState.getSplicerState(3).Monitor.getRunningRollStand_A()),
		  String::Bool2Alpha(C_PageManager::SMState.getSplicerState(4).Monitor.getRunningRollStand_A()),
		  String::Bool2Alpha(C_PageManager::SMState.getSplicerState(5).Monitor.getRunningRollStand_A()),
		  String::Bool2Alpha(C_PageManager::SMState.getSplicerState(6).Monitor.getRunningRollStand_A()),
		  String::Bool2Alpha(C_PageManager::SMState.getSplicerState(7).Monitor.getRunningRollStand_A()));

    //s1
		if(C_PageManager::SMState.getZoneSingleFacerState(1).singleFacerState.CtrlData.orders.getIsOnLine().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.SingleFacer1.Warned=%s",String::Bool2Alpha(!C_PageManager::SMState.getZoneSingleFacerState(1).singleFacerState.CtrlData.orders.getIsOnLine()));

    if ( C_PageManager::SMState.getZoneDoubleBackerState().splicerLinerState.CtrlData.Alarms.getIsEStopPressed() != s1EstopPres ||
          first )
    {
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer1.InEmergency=%s",
			  String::Bool2Alpha(C_PageManager::SMState.getZoneDoubleBackerState().splicerLinerState.CtrlData.Alarms.getIsEStopPressed()));
      s1EstopPres = C_PageManager::SMState.getZoneDoubleBackerState().splicerLinerState.CtrlData.Alarms.getIsEStopPressed();
    }
    if ( C_PageManager::SMState.getZoneDoubleBackerState().splicerLinerState.CtrlData.Alarms.isAlarmed() != s1Alarmed ||
          first )
    {
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer1.Alarmed=%s",
			  String::Bool2Alpha(C_PageManager::SMState.getZoneDoubleBackerState().splicerLinerState.CtrlData.Alarms.isAlarmed()));
      s1Alarmed = C_PageManager::SMState.getZoneDoubleBackerState().splicerLinerState.CtrlData.Alarms.isAlarmed();
    }

    switch (C_PageManager::SMState.getZoneDoubleBackerState().splicerLinerState.Control.getMode())
		{
		case State::WECMODE_NOMODE:
			break;
		case State::WECMODE_MANUAL:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS1.Auto=false");
			break;
		case State::WECMODE_AUTO:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS1.Auto=true");
			break;
		case State::WECMODE_OVERRIDE:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS1.switching=true");
			break;
		default:
			break;
		}

    if ( C_PageManager::SMState.getSplicerState(1).Monitor.getProductionUntilSplice_UDmm() != sp1ProductUntilSplice ||
          first )
    {
		  if (C_PageManager::SMState.getSplicerState(1).Monitor.getNotReadyAtEarlyWarning())
			  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S1Meters.BackColor=Red");
		  else
		  {
			  if (C_PageManager::SMState.getSplicerState(1).Monitor.getEarlyWarning())
				  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S1Meters.BackColor=Yellow");
        else if (C_PageManager::SMState.getSplicerState(1).Monitor.getPrefill())
				  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S1Meters.BackColor=GreenYellow");
        else
				  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S1Meters.BackColor=WhiteSmoke");
		  }

		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S1Meters.Value=%d",
			  C_PageManager::SMState.getSplicerState(1).Monitor.getProductionUntilSplice_UDmm());

		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S1Meters.ValueBackColor=%s",
			  ( C_PageManager::SMState.getSplicerState(1).Control.getSpliceConfirmed() &&
			   ( C_PageManager::SMState.getSplicerState(1).Monitor.getButt_UDmm() > 0 ||
				   wecState.Control.getSyncMode() == State::SYNCMODE_SPLICER1 )  ) ? L"AOCInProgressColor" : L"White");

      sp1ProductUntilSplice = C_PageManager::SMState.getSplicerState(1).Monitor.getProductionUntilSplice_UDmm();
    }

		//s2
		if(C_PageManager::SMState.getZoneSingleFacerState(2).singleFacerState.CtrlData.orders.getIsOnLine().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.SingleFacer2.Warned=%s",String::Bool2Alpha(!C_PageManager::SMState.getZoneSingleFacerState(2).singleFacerState.CtrlData.orders.getIsOnLine()));

    if ( C_PageManager::SMState.getZoneSingleFacerState(1).splicerMediumState.CtrlData.Alarms.getIsEStopPressed() != s2EstopPres ||
      first )
    {
	    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer2.InEmergency=%s",
		    String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(1).splicerMediumState.CtrlData.Alarms.getIsEStopPressed()));
      s2EstopPres = C_PageManager::SMState.getZoneSingleFacerState(1).splicerMediumState.CtrlData.Alarms.getIsEStopPressed();
    }
        
    //ABELLO_FI20/10/2009_1 Rimossi gli allarmi dalla barra di stato quando una zona non è attiva.
    if(C_PageManager::SMState.getZoneSingleFacerState(1).singleFacerState.rtData.getActive(State::RtData::iscurrent))
    {
      if ( C_PageManager::SMState.getZoneSingleFacerState(1).splicerMediumState.CtrlData.Alarms.isAlarmed() != s2Alarmed ||
            first )
      {
		    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer2.Alarmed=%s",
			    String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(1).splicerMediumState.CtrlData.Alarms.isAlarmed()));
        s2Alarmed = C_PageManager::SMState.getZoneSingleFacerState(1).splicerMediumState.CtrlData.Alarms.isAlarmed();
      }
    }
    else
    {
      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer2.Alarmed=false");
    }

    switch (C_PageManager::SMState.getZoneSingleFacerState(1).splicerMediumState.Control.getMode())
		{
		case State::WECMODE_NOMODE:
			break;
		case State::WECMODE_MANUAL:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS2.Auto=false");
			break;
		case State::WECMODE_AUTO:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS2.Auto=true");
			break;
		case State::WECMODE_OVERRIDE:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS2.switching=true");
			break;
		default:
			break;
		}

    if ( C_PageManager::SMState.getSplicerState(2).Monitor.getProductionUntilSplice_UDmm() != sp2ProductUntilSplice ||
          first )
    {
		  if (C_PageManager::SMState.getSplicerState(2).Monitor.getNotReadyAtEarlyWarning())
			  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S2Meters.BackColor=Red");
		  else
		  {
			  if (C_PageManager::SMState.getSplicerState(2).Monitor.getEarlyWarning())
				  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S2Meters.BackColor=Yellow");
        else if (C_PageManager::SMState.getSplicerState(2).Monitor.getPrefill())
				  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S2Meters.BackColor=GreenYellow");
			  else
				  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S2Meters.BackColor=WhiteSmoke");
		  }

		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S2Meters.Value=%d",
			  C_PageManager::SMState.getSplicerState(2).Monitor.getProductionUntilSplice_UDmm());

		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S2Meters.ValueBackColor=%s",
			  ( C_PageManager::SMState.getSplicerState(2).Control.getSpliceConfirmed() &&
			   ( C_PageManager::SMState.getSplicerState(2).Monitor.getButt_UDmm() > 0 ||
				   wecState.Control.getSyncMode() == State::SYNCMODE_SPLICER2 ) ) ? L"AOCInProgressColor" : L"White");

      sp2ProductUntilSplice = C_PageManager::SMState.getSplicerState(2).Monitor.getProductionUntilSplice_UDmm();
    }

    //s3
    if ( C_PageManager::SMState.getZoneSingleFacerState(1).splicerLinerState.CtrlData.Alarms.getIsEStopPressed() != s3EstopPres ||
        first )
    {
	    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer3.InEmergency=%s",
		    String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(1).splicerLinerState.CtrlData.Alarms.getIsEStopPressed()));
      s3EstopPres = C_PageManager::SMState.getZoneSingleFacerState(1).splicerLinerState.CtrlData.Alarms.getIsEStopPressed();
    }
    //ABELLO_FI20/10/2009_1 Rimossi gli allarmi dalla barra di stato quando una zona non è attiva.
    if(C_PageManager::SMState.getZoneSingleFacerState(1).singleFacerState.rtData.getActive(State::RtData::iscurrent))
    {
      if ( C_PageManager::SMState.getZoneSingleFacerState(1).splicerLinerState.CtrlData.Alarms.isAlarmed() != s3Alarmed ||
            first )
      {
		    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer3.Alarmed=%s",
			    String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(1).splicerLinerState.CtrlData.Alarms.isAlarmed()));
        s3Alarmed = C_PageManager::SMState.getZoneSingleFacerState(1).splicerLinerState.CtrlData.Alarms.isAlarmed();
      }
    }
    else
    {
      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer3.Alarmed=false");
    }

		switch (C_PageManager::SMState.getZoneSingleFacerState(1).splicerLinerState.Control.getMode())
		{
		case State::WECMODE_NOMODE:
			break;
		case State::WECMODE_MANUAL:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS3.Auto=false");
			break;
		case State::WECMODE_AUTO:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS3.Auto=true");
			break;
		case State::WECMODE_OVERRIDE:
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS3.switching=true");
			break;
		default:
			break;
		}

    if ( C_PageManager::SMState.getSplicerState(3).Monitor.getProductionUntilSplice_UDmm() != sp3ProductUntilSplice ||
          first )
    {
		  if (C_PageManager::SMState.getSplicerState(3).Monitor.getNotReadyAtEarlyWarning())
			  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S3Meters.BackColor=Red");
		  else
		  {
			  if (C_PageManager::SMState.getSplicerState(3).Monitor.getEarlyWarning())
				  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S3Meters.BackColor=Yellow");
        else if (C_PageManager::SMState.getSplicerState(3).Monitor.getPrefill())
				  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S3Meters.BackColor=GreenYellow");
			  else
				  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S3Meters.BackColor=WhiteSmoke");
		  }

		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S3Meters.Value=%d",
			  C_PageManager::SMState.getSplicerState(3).Monitor.getProductionUntilSplice_UDmm());

		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S3Meters.ValueBackColor=%s",
			  ( C_PageManager::SMState.getSplicerState(3).Control.getSpliceConfirmed() &&
			   ( C_PageManager::SMState.getSplicerState(3).Monitor.getButt_UDmm() > 0  ||
			   wecState.Control.getSyncMode() == State::SYNCMODE_SPLICER3 ) ) ? L"AOCInProgressColor" : L"White");

      sp3ProductUntilSplice = C_PageManager::SMState.getSplicerState(3).Monitor.getProductionUntilSplice_UDmm();
    }

		//sf1
    		  
    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.SingleFacer1.InEmergency=%s",
			  String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(1).singleFacerState.CtrlData.Alarms.getIsEStopPressed()));

    //ABELLO_FI20/10/2009_1 Rimossi gli allarmi dalla barra di stato quando una zona non è attiva.
    if(C_PageManager::SMState.getZoneSingleFacerState(1).singleFacerState.rtData.getActive(State::RtData::iscurrent))
    {
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.SingleFacer1.Alarmed=%s",
			  String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(1).singleFacerState.CtrlData.Alarms.isAlarmed()));
    }
    else
    {
      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.SingleFacer1.Alarmed=false");
    }
 
    //cartridge sf1
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd._setSFNCartridge(i[1],s[%c],b[%s])",
			C_PageManager::SMState.getZoneSingleFacerState(1).singleFacerState.CtrlData.Monitor.getFluteId(),
			String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(1).singleFacerState.CtrlData.Alarms.getFluteError()));
		//bridge1
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.pct_BridgeError1.Visible=%s",
			String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(1).bridgeState.Alarms.isAlarmed()));

		//s4
    if ( cfg.macEnable.getSplicer(4) )
    {
      if ( C_PageManager::SMState.getZoneSingleFacerState(2).splicerMediumState.CtrlData.Alarms.getIsEStopPressed() != s4EstopPres ||
              first )
      {
        o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer4.InEmergency=%s",
	        String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(2).splicerMediumState.CtrlData.Alarms.getIsEStopPressed()));
        s4EstopPres = C_PageManager::SMState.getZoneSingleFacerState(2).splicerMediumState.CtrlData.Alarms.getIsEStopPressed();
      }
      //ABELLO_FI20/10/2009_1 Rimossi gli allarmi dalla barra di stato quando una zona non è attiva.
      if(C_PageManager::SMState.getZoneSingleFacerState(2).singleFacerState.rtData.getActive(State::RtData::iscurrent))
      {
        if ( C_PageManager::SMState.getZoneSingleFacerState(2).splicerMediumState.CtrlData.Alarms.isAlarmed() != s4Alarmed ||
              first )
        {
	        o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer4.Alarmed=%s",
		        String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(2).splicerMediumState.CtrlData.Alarms.isAlarmed()));
          s4Alarmed = C_PageManager::SMState.getZoneSingleFacerState(2).splicerMediumState.CtrlData.Alarms.isAlarmed();
        }
      }
      else
      {
        o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer4.Alarmed=false");
      }
   
      switch (C_PageManager::SMState.getZoneSingleFacerState(2).splicerMediumState.Control.getMode())
	    {
	    case State::WECMODE_NOMODE:
		    break;
	    case State::WECMODE_MANUAL:
		    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS4.Auto=false");
		    break;
	    case State::WECMODE_AUTO:
		    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS4.Auto=true");
		    break;
	    case State::WECMODE_OVERRIDE:
		    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS4.switching=true");
		    break;
	    default:
		    break;
	    }

      if ( C_PageManager::SMState.getSplicerState(4).Monitor.getProductionUntilSplice_UDmm() != sp4ProductUntilSplice ||
            first )
      {
	      if (C_PageManager::SMState.getSplicerState(4).Monitor.getNotReadyAtEarlyWarning())
		      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S4Meters.BackColor=Red");
	      else
	      {
		      if (C_PageManager::SMState.getSplicerState(4).Monitor.getEarlyWarning())
			      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S4Meters.BackColor=Yellow");
          else if (C_PageManager::SMState.getSplicerState(4).Monitor.getPrefill())
			      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S4Meters.BackColor=GreenYellow");
		      else
			      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S4Meters.BackColor=WhiteSmoke");
	      }

        o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S4Meters.Value=%d",
		      C_PageManager::SMState.getSplicerState(4).Monitor.getProductionUntilSplice_UDmm());

	      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S4Meters.ValueBackColor=%s",
		      ( C_PageManager::SMState.getSplicerState(4).Control.getSpliceConfirmed() &&
		       ( C_PageManager::SMState.getSplicerState(4).Monitor.getButt_UDmm() > 0 ||
				      wecState.Control.getSyncMode()==State::SYNCMODE_SPLICER4 ) ) ? L"AOCInProgressColor" : L"White");

        sp4ProductUntilSplice = C_PageManager::SMState.getSplicerState(4).Monitor.getProductionUntilSplice_UDmm();
      }
    }

    //s5
    if ( cfg.macEnable.getSplicer(5) )
    {
      if ( C_PageManager::SMState.getZoneSingleFacerState(2).splicerLinerState.CtrlData.Alarms.getIsEStopPressed() != s5EstopPres ||
              first )
      {
        o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer5.InEmergency=%s",
	        String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(2).splicerLinerState.CtrlData.Alarms.getIsEStopPressed()));
        s5EstopPres = C_PageManager::SMState.getZoneSingleFacerState(2).splicerLinerState.CtrlData.Alarms.getIsEStopPressed();
      }
      //ABELLO_FI20/10/2009_1 Rimossi gli allarmi dalla barra di stato quando una zona non è attiva.
      if(C_PageManager::SMState.getZoneSingleFacerState(2).singleFacerState.rtData.getActive(State::RtData::iscurrent))
      {
        if ( C_PageManager::SMState.getZoneSingleFacerState(2).splicerLinerState.CtrlData.Alarms.isAlarmed() != s5Alarmed ||
              first )
        {
	        o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer5.Alarmed=%s",
		        String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(2).splicerLinerState.CtrlData.Alarms.isAlarmed()));
          s5Alarmed = C_PageManager::SMState.getZoneSingleFacerState(2).splicerLinerState.CtrlData.Alarms.isAlarmed();
        }
      }
      else
      {
        o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer5.Alarmed=false");
      }
   
      switch (C_PageManager::SMState.getZoneSingleFacerState(2).splicerLinerState.Control.getMode())
	    {
	    case State::WECMODE_NOMODE:
		    break;
	    case State::WECMODE_MANUAL:
		    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS5.Auto=false");
		    break;
	    case State::WECMODE_AUTO:
		    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS5.Auto=true");
		    break;
	    case State::WECMODE_OVERRIDE:
		    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS5.switching=true");
		    break;
	    default:
		    break;
	    }

      if ( C_PageManager::SMState.getSplicerState(5).Monitor.getProductionUntilSplice_UDmm() != sp5ProductUntilSplice ||
            first )
      {
	      if (C_PageManager::SMState.getSplicerState(5).Monitor.getNotReadyAtEarlyWarning())
		      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S5Meters.BackColor=Red");
	      else
	      {
		      if (C_PageManager::SMState.getSplicerState(5).Monitor.getEarlyWarning())
			      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S5Meters.BackColor=Yellow");
          else if (C_PageManager::SMState.getSplicerState(5).Monitor.getPrefill())
			      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S5Meters.BackColor=GreenYellow");
		      else
			      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S5Meters.BackColor=WhiteSmoke");
	      }

        o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S5Meters.Value=%d",
		      C_PageManager::SMState.getSplicerState(5).Monitor.getProductionUntilSplice_UDmm());

	      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S5Meters.ValueBackColor=%s",
		      ( C_PageManager::SMState.getSplicerState(5).Control.getSpliceConfirmed() &&
		       ( C_PageManager::SMState.getSplicerState(5).Monitor.getButt_UDmm() > 0	||
		       wecState.Control.getSyncMode()==State::SYNCMODE_SPLICER5 ) ) ? L"AOCInProgressColor" : L"White");

        sp5ProductUntilSplice = C_PageManager::SMState.getSplicerState(5).Monitor.getProductionUntilSplice_UDmm();
      }
    }

    //sf2
    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.SingleFacer2.InEmergency=%s",
			  String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(2).singleFacerState.CtrlData.Alarms.getIsEStopPressed()));

    //ABELLO_FI20/10/2009_1 Rimossi gli allarmi dalla barra di stato quando una zona non è attiva.
    if(C_PageManager::SMState.getZoneSingleFacerState(2).singleFacerState.rtData.getActive(State::RtData::iscurrent))
    {
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.SingleFacer2.Alarmed=%s",
			  String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(2).singleFacerState.CtrlData.Alarms.isAlarmed()));
    }
    else
    {
      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.SingleFacer2.Alarmed=false");
    }
 
    //cartridge sf2
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd._setSFNCartridge(i[2],s[%c],b[%s])",
			C_PageManager::SMState.getZoneSingleFacerState(2).singleFacerState.CtrlData.Monitor.getFluteId(),
			String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(2).singleFacerState.CtrlData.Alarms.getFluteError()));
		//bridge2
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.pct_BridgeError2.Visible=%s",
			String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(2).bridgeState.Alarms.isAlarmed()));
 		

    //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
		//s6
    if ( cfg.macEnable.getSplicer(6) )
    {
      if ( C_PageManager::SMState.getZoneSingleFacerState(3).splicerMediumState.CtrlData.Alarms.getIsEStopPressed() != s6EstopPres ||
              first )
      {
	      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer6.InEmergency=%s",
		      String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(3).splicerMediumState.CtrlData.Alarms.getIsEStopPressed()));
        s6EstopPres = C_PageManager::SMState.getZoneSingleFacerState(3).splicerMediumState.CtrlData.Alarms.getIsEStopPressed();
      }
      //ABELLO_FI20/10/2009_1 Rimossi gli allarmi dalla barra di stato quando una zona non è attiva.
      if(C_PageManager::SMState.getZoneSingleFacerState(3).singleFacerState.rtData.getActive(State::RtData::iscurrent))
      {
        if ( C_PageManager::SMState.getZoneSingleFacerState(3).splicerMediumState.CtrlData.Alarms.isAlarmed() != s6Alarmed ||
              first )
        {
		      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer6.Alarmed=%s",
			      String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(3).splicerMediumState.CtrlData.Alarms.isAlarmed()));
          s6Alarmed = C_PageManager::SMState.getZoneSingleFacerState(3).splicerMediumState.CtrlData.Alarms.isAlarmed();
        }
      }
      else
      {
        o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer6.Alarmed=false");
      }
   
      switch (C_PageManager::SMState.getZoneSingleFacerState(3).splicerMediumState.Control.getMode())
		  {
		  case State::WECMODE_NOMODE:
			  break;
		  case State::WECMODE_MANUAL:
			  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS6.Auto=false");
			  break;
		  case State::WECMODE_AUTO:
			  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS6.Auto=true");
			  break;
		  case State::WECMODE_OVERRIDE:
			  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS6.switching=true");
			  break;
		  default:
			  break;
		  }

      if ( C_PageManager::SMState.getSplicerState(6).Monitor.getProductionUntilSplice_UDmm() != sp6ProductUntilSplice ||
            first )
      {
		    if (C_PageManager::SMState.getSplicerState(6).Monitor.getNotReadyAtEarlyWarning())
			    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S6Meters.BackColor=Red");
		    else
		    {
			    if (C_PageManager::SMState.getSplicerState(6).Monitor.getEarlyWarning())
				    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S6Meters.BackColor=Yellow");
          else if (C_PageManager::SMState.getSplicerState(6).Monitor.getPrefill())
				    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S6Meters.BackColor=GreenYellow");
			    else
				    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S6Meters.BackColor=WhiteSmoke");
		    }

        o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S6Meters.Value=%d",
			    C_PageManager::SMState.getSplicerState(6).Monitor.getProductionUntilSplice_UDmm());

		    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S6Meters.ValueBackColor=%s",
			    ( C_PageManager::SMState.getSplicerState(6).Control.getSpliceConfirmed() &&
			     ( C_PageManager::SMState.getSplicerState(6).Monitor.getButt_UDmm() > 0 ||
					    wecState.Control.getSyncMode()==State::SYNCMODE_SPLICER6 ) ) ? L"AOCInProgressColor" : L"White");

        sp6ProductUntilSplice = C_PageManager::SMState.getSplicerState(6).Monitor.getProductionUntilSplice_UDmm();
      }
    }

    //s7
    if ( cfg.macEnable.getSplicer(7) )
    {
      if ( C_PageManager::SMState.getZoneSingleFacerState(3).splicerLinerState.CtrlData.Alarms.getIsEStopPressed() != s7EstopPres ||
              first )
      {
	      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer7.InEmergency=%s",
		      String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(3).splicerLinerState.CtrlData.Alarms.getIsEStopPressed()));
        s7EstopPres = C_PageManager::SMState.getZoneSingleFacerState(3).splicerLinerState.CtrlData.Alarms.getIsEStopPressed();
      }
      //ABELLO_FI20/10/2009_1 Rimossi gli allarmi dalla barra di stato quando una zona non è attiva.
      if(C_PageManager::SMState.getZoneSingleFacerState(3).singleFacerState.rtData.getActive(State::RtData::iscurrent))
      {
        if ( C_PageManager::SMState.getZoneSingleFacerState(3).splicerLinerState.CtrlData.Alarms.isAlarmed() != s7Alarmed ||
              first )
        {
		      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer7.Alarmed=%s",
			      String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(3).splicerLinerState.CtrlData.Alarms.isAlarmed()));
          s7Alarmed = C_PageManager::SMState.getZoneSingleFacerState(3).splicerLinerState.CtrlData.Alarms.isAlarmed();
        }
      }
      else
      {
        o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.Splicer7.Alarmed=false");
      }
   
      switch (C_PageManager::SMState.getZoneSingleFacerState(3).splicerLinerState.Control.getMode())
		  {
		  case State::WECMODE_NOMODE:
			  break;
		  case State::WECMODE_MANUAL:
			  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS7.Auto=false");
			  break;
		  case State::WECMODE_AUTO:
			  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS7.Auto=true");
			  break;
		  case State::WECMODE_OVERRIDE:
			  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.AutoManS7.switching=true");
			  break;
		  default:
			  break;
		  }

      if ( C_PageManager::SMState.getSplicerState(7).Monitor.getProductionUntilSplice_UDmm() != sp7ProductUntilSplice ||
            first )
      {
		    if (C_PageManager::SMState.getSplicerState(7).Monitor.getNotReadyAtEarlyWarning())
			    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S7Meters.BackColor=Red");
		    else
		    {
			    if (C_PageManager::SMState.getSplicerState(7).Monitor.getEarlyWarning())
				    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S7Meters.BackColor=Yellow");
          else if (C_PageManager::SMState.getSplicerState(7).Monitor.getPrefill())
				    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S7Meters.BackColor=GreenYellow");
			    else
				    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S7Meters.BackColor=WhiteSmoke");
		    }

        o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S7Meters.Value=%d",
			    C_PageManager::SMState.getSplicerState(7).Monitor.getProductionUntilSplice_UDmm());

		    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.nud_S7Meters.ValueBackColor=%s",
			    ( C_PageManager::SMState.getSplicerState(7).Control.getSpliceConfirmed() &&
			     ( C_PageManager::SMState.getSplicerState(7).Monitor.getButt_UDmm() > 0	||
			     wecState.Control.getSyncMode()==State::SYNCMODE_SPLICER7 ) ) ? L"AOCInProgressColor" : L"White");

        sp7ProductUntilSplice = C_PageManager::SMState.getSplicerState(7).Monitor.getProductionUntilSplice_UDmm();
      }
    }

    //sf3
    o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.SingleFacer3.InEmergency=%s",
			  String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(3).singleFacerState.CtrlData.Alarms.getIsEStopPressed()));

    //ABELLO_FI20/10/2009_1 Rimossi gli allarmi dalla barra di stato quando una zona non è attiva.
    if(C_PageManager::SMState.getZoneSingleFacerState(3).singleFacerState.rtData.getActive(State::RtData::iscurrent))
    {
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.SingleFacer3.Alarmed=%s",
			  String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(3).singleFacerState.CtrlData.Alarms.isAlarmed()));
    }
    else
    {
      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.SingleFacer3.Alarmed=false");
    }
 
    //cartridge sf3
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd._setSFNCartridge(i[3],s[%c],b[%s])",
			C_PageManager::SMState.getZoneSingleFacerState(3).singleFacerState.CtrlData.Monitor.getFluteId(),
			String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(3).singleFacerState.CtrlData.Alarms.getFluteError()));
		
    
    //bridge3
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.pct_BridgeError3.Visible=%s",
			String::Bool2Alpha(C_PageManager::SMState.getZoneSingleFacerState(3).bridgeState.Alarms.isAlarmed()));


		//bridge3 contents: more precise and proportional management could improve this management...
		if (! C_PageManager::SMState.getZoneSingleFacerState(1).bridgeState.Monitor.isLowEyeOn())
			bridgeContent = 20;
		if (C_PageManager::SMState.getZoneSingleFacerState(1).bridgeState.Monitor.isLowEyeOn())
			bridgeContent = 50;
		if (C_PageManager::SMState.getZoneSingleFacerState(1).bridgeState.Monitor.areLowAndHighEyesOn())
			bridgeContent = 100;
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.prb_BridgeContents1.Percentage=%d", bridgeContent);

		if(bridgeContent <= 20)
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.prb_BridgeContents1.ForeColor=Red");
		else
		{
			if(bridgeContent >= 100)
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.prb_BridgeContents1.ForeColor=Orange");
			else
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.prb_BridgeContents1.ForeColor=Black");
		}
		
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.lbl_BridgeContents1.Text=%d", 
			(int)C_PageManager::SMState.getZoneSingleFacerState(1).bridgeState.Monitor.getContents_UDm());


		//bridge2 contents: more precise and proportional management could improve this management...
		if (! C_PageManager::SMState.getZoneSingleFacerState(2).bridgeState.Monitor.isLowEyeOn())
			bridgeContent = 20;
		if (C_PageManager::SMState.getZoneSingleFacerState(2).bridgeState.Monitor.isLowEyeOn())
			bridgeContent = 50;
		if (C_PageManager::SMState.getZoneSingleFacerState(2).bridgeState.Monitor.areLowAndHighEyesOn())
			bridgeContent = 100;
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.prb_BridgeContents2.Percentage=%d", bridgeContent);
		
		if(bridgeContent <= 20)
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.prb_BridgeContents2.ForeColor=Red");
		else
		{
			if(bridgeContent >= 100)
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.prb_BridgeContents2.ForeColor=Orange");
			else
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.prb_BridgeContents2.ForeColor=Black");
		}
		
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.lbl_BridgeContents2.Text=%d", 
			(int)C_PageManager::SMState.getZoneSingleFacerState(2).bridgeState.Monitor.getContents_UDm());

    //RFAZZI_FI12/08/2011_1. Aggiunto in barra di stato Wet la terza zona. 
		//bridge3 contents: more precise and proportional management could improve this management...
		if (! C_PageManager::SMState.getZoneSingleFacerState(3).bridgeState.Monitor.isLowEyeOn())
			bridgeContent = 20;
		if (C_PageManager::SMState.getZoneSingleFacerState(3).bridgeState.Monitor.isLowEyeOn())
			bridgeContent = 50;
		if (C_PageManager::SMState.getZoneSingleFacerState(3).bridgeState.Monitor.areLowAndHighEyesOn())
			bridgeContent = 100;
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.prb_BridgeContents3.Percentage=%d", bridgeContent);
		
		if(bridgeContent <= 20)
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.prb_BridgeContents3.ForeColor=Red");
		else
		{
			if(bridgeContent >= 100)
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.prb_BridgeContents3.ForeColor=Orange");
			else
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.prb_BridgeContents3.ForeColor=Black");
		}
		
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarWetEnd.lbl_BridgeContents3.Text=%d", 
			(int)C_PageManager::SMState.getZoneSingleFacerState(3).bridgeState.Monitor.getContents_UDm());

		first = false;
		Sleep(1000);
	}
	
	logger.Fine(__FUNCTION__, L"Terminated statusBar_statusBarWetEnd page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_StatusBar::DryEnd(LPVOID  lpParam)
{
	State::SpeedState speedState;
	State::StatisticsStateSetup  statisticsStateSetup;
	State::AOCState aocState;
	State::SlitterScorerState slitterScorerState;
	State::KnifeState knifeState;
	State::StackerState stackerState[MAX_NUM_LEVELS];
	State::ReadyMachineState rdyRunState;
	State::ReadyMachineState rdyAOCState;

	SetupData setupData;

	int 
		speed = 0,
		meterLeft = 0,
		numSetups = 0;

	bool typeAOC = false;

	bool

		//PMASSARO_FI06/10/08 - Bug Fix: During The "Reset OC" The Backcolor Was Not Correctly Set  
		lastAOCRunningStatus = false,

		first = true,

		SYCReadyRun = true,
		DBReadyRun = true,
		RSHReadyRun = true,
		SSReadyRun = true,
		KnifeReadyRun = true,

		Stk1ReadyRun = true,
		Stk2ReadyRun = true,
		Stk3ReadyRun = true,

		SYCReadyAOC = true,
		DBReadyAOC = true,
		RSHReadyAOC = true,
		SSReadyAOC = true,
		KnifeReadyAOC = true,

		Stk1ReadyAOC = true,
		Stk2ReadyAOC = true,
		Stk3ReadyAOC = true,
		
		ModeAuto = true,
		ModeSemi = true,
		WebAtRSH = true,
		WebAtMW = true,
		WebAtKnife = true,

		SSAlarm = false,
		KnifeAlarm = false,
		StkAlarm[MAX_NUM_LEVELS] = {false,false,false},
		
		SourceDryEnd = false,
		statusSimulation = false;

		wchar_t PushFaster[MAX_PATH]; 
		wchar_t PushSlower[MAX_PATH];

		LineConfig cfg;
		C_PageManager::SMLineConfig.getLineConfig(cfg);

	o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd._ShowStatusBarDryEnd(b[%s], b[%s], b[%s], b[%s], b[%s], b[%s], b[%s], i[%d])", 
    String::Bool2Alpha(cfg.macEnable.getDoubleBacker()),
    String::Bool2Alpha(cfg.macEnable.getRotary()),
    String::Bool2Alpha(cfg.macEnable.getSlitterScorer()),
    String::Bool2Alpha(cfg.macEnable.getKnife()),
    String::Bool2Alpha(cfg.macEnable.getStacker1()),
    String::Bool2Alpha(cfg.macEnable.getStacker2()),
    String::Bool2Alpha(cfg.macEnable.getStacker3()),
    cfg.macEnable.getNumberOfStackerConfigured());

	while(!end_DryEnd)
	{
		speedState = C_PageManager::SMState.getSpeedState();
		statisticsStateSetup = C_PageManager::SMState.getStatisticsStateCurrentSetup();
		aocState = C_PageManager::SMState.getAOCState();
		C_PageManager::SMOrder.DRYGetCurrentSetup(setupData);
		C_PageManager::SMState.getReadyToRunState(rdyRunState);
		C_PageManager::SMState.getReadyToAOCState(rdyAOCState);

		if ( C_PageManager::SMState.getSimulationState().getSimulationActive() != statusSimulation )
		  o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.ttl_SimulationTitle.Visible=%s", String::Bool2Alpha(C_PageManager::SMState.getSimulationState().getSimulationActive()));
		statusSimulation = C_PageManager::SMState.getSimulationState().getSimulationActive();

		//Speeds
		if ((speedState.getSpeed_UVm_m() != speed )||
			(first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.nud_LineSpeed.Value=%d",speedState.getSpeed_UVmm_m()); 
			speed = speedState.getSpeed_UVm_m();
		}

		//Increase / Decrease commands
		wchar_t dummyPushFaster[MAX_PATH]; 
		wchar_t dummyPushSlower[MAX_PATH];
		speedState.pushButton.getFasterPushButtonPressed(Network::SEP,dummyPushFaster,MAX_PATH);
		speedState.pushButton.getSlowerPushButtonPressed(Network::SEP,dummyPushSlower,MAX_PATH);
		if ( (wcscmp(dummyPushFaster,PushFaster)!=0) || (wcscmp(dummyPushSlower,PushSlower)!=0) || (first) )
		{
			speedState.pushButton.getFasterPushButtonPressed(Network::SEP,PushFaster,MAX_PATH);
			speedState.pushButton.getSlowerPushButtonPressed(Network::SEP,PushSlower,MAX_PATH);
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd._setPushButtonPressed(s[%s],s[%s])",PushFaster,PushSlower);
		}

		//Dry End Production

		//PMASSARO_FI06/10/08 - Bug Fix: During The "Reset OC" The Backcolor Was Not Correctly Set  
		/*
		if ( (statisticsStateSetup.getMetersLeft_UDm() != meterLeft )||
				 (first) ||
				 (aocState.getIsAOCInProgress()) )
	    */
		if ( (statisticsStateSetup.getMetersLeft_UDm() != meterLeft )||
				 (first) ||
				 (aocState.getIsAOCInProgress() != lastAOCRunningStatus) )
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.tlb_DryEndMetersLeft.Value=%d",statisticsStateSetup.getMetersLeft_UDm());
			meterLeft = statisticsStateSetup.getMetersLeft_UDm();

			//Meter Left BackColor according to the AOC Early warning and the AOC in progress status
			if (aocState.getIsAOCInProgress())
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.tlb_DryEndMetersLeft.BackColor=LightBlue");
			else if (aocState.getIsAOCEarlyWarning())
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.tlb_DryEndMetersLeft.BackColor=Yellow");
			else
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.tlb_DryEndMetersLeft.BackColor=WhiteSmoke");

			//PMASSARO_FI06/10/08 - Bug Fix: During The "Reset OC" The Backcolor Was Not Correctly Set 
			lastAOCRunningStatus = aocState.getIsAOCInProgress();

			//progress bars
			int producPercent = (int)((double)( (double)statisticsStateSetup.getDryEndProducedGoodMetersLevel_UDm() / setupData.getSetupLength_UDm()) * 100);
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.prb_DryEnd.Percentage=%d",producPercent);
		}

		//AOC
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd._setDRYStatus(b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s])", 
			String::Bool2Alpha(!C_PageManager::SMState.getRSHState().control.getScrapDisabled()),
			String::Bool2Alpha(false),
			String::Bool2Alpha(aocState.isModeAuto()),
			String::Bool2Alpha(aocState.isModeSemi()),
			String::Bool2Alpha(aocState.isModeManual()),
			String::Bool2Alpha((aocState.Type.getNoGap() || aocState.Type.getNoGapCTrimOneLevel())),
			String::Bool2Alpha(aocState.Type.getTandem()),
			String::Bool2Alpha(aocState.Type.getKnifeOnwards()),
			String::Bool2Alpha(C_PageManager::SMState.getGeneralState().getWebAtRSH()),
			String::Bool2Alpha(C_PageManager::SMState.getGeneralState().getWebAtMW()),
			String::Bool2Alpha(C_PageManager::SMState.getGeneralState().getWebAtKnife())
			);

		//ABELLO_FI01/02/11_2 Aggiunto sullo status bar la tracciabilità degli stop
    SYSTEMTIME timeOfFirstStop;
    wchar_t reasonFirstOfStop[MAX_PATH] = L"";
    wchar_t timeOfFirstStopStr[MAX_PATH] = L"";

    if(C_PageManager::SMState.getStopReasonStateMODIFY().getFirstReasonOfStopItem(timeOfFirstStop, reasonFirstOfStop))
    {
      String::SYSTEMTIME2Alpha(timeOfFirstStop,timeOfFirstStopStr,MAX_PATH);
      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd._setFirstReasonOfStop(s[%s],s[%s])", timeOfFirstStopStr, reasonFirstOfStop);
    }
    else
      o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd._setFirstReasonOfStop(s[ ],s[ ])");

		if (( C_PageManager::SMOrder.DRYGetNumSetupInList() != numSetups ) ||
			(first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.tlb_DryEndSetup.Value=%d",C_PageManager::SMOrder.DRYGetNumSetupInList());
			numSetups = C_PageManager::SMOrder.DRYGetNumSetupInList();
			if ( numSetups < 5 )
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.tlb_DryEndSetup.ForeColor=Red");
			else
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.tlb_DryEndSetup.ForeColor=Black");

		}

		//Check Machines Alarms and Warnings (manual)

		//DB Status
		//db
		if(C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.CtrlData.orders.getIsOnLine().getEnable())
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.DoubleBacker.Warned=%s",String::Bool2Alpha(!C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.CtrlData.orders.getIsOnLine()));
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.DoubleBacker.InEmergency=%s", String::Bool2Alpha(C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.CtrlData.alarms.getIsEStopPressed()));
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.DoubleBacker.Alarmed=%s", String::Bool2Alpha(C_PageManager::SMState.getZoneDoubleBackerState().doubleBackerState.CtrlData.alarms.isAlarmed()));
     
		//RSH Status
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.Shear.InEmergency=%s", String::Bool2Alpha(C_PageManager::SMState.getRSHState().alarms.getIsEStopPressed().getValue()));
		
		//SS Status
		slitterScorerState = C_PageManager::SMState.getSlitterScorerState();
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.SlitterScorer.InEmergency=%s", String::Bool2Alpha(slitterScorerState.alarms.getIsEStopPressed().getValue()));
		if (slitterScorerState.alarms.getIsInAlarm() && !SSAlarm)
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.SlitterScorer.Alarmed=true");
		else if (!slitterScorerState.alarms.getIsInAlarm() && SSAlarm)
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.SlitterScorer.Alarmed=false");
		SSAlarm = slitterScorerState.alarms.getIsInAlarm();

    //RFAZZI_FI26/01/2011_2 Aggiunta gestione Warning su grafica Twin400 di Syncro. 
		//if (slitterScorerState.control.getIsInManual())
		if (slitterScorerState.control.getIsInManual() || slitterScorerState.alarms.getIsAlarmWarning() )
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.SlitterScorer.Warned=true");
		else
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.SlitterScorer.Warned=false");
		
		o_socket.Send(Network::MSG_PAG_REF, L"statusBar_statusBarDryEnd.pct_maintenance.Visible=%s", String::Bool2Alpha(slitterScorerState.control.getIsInMaintenance()));
	
		//Knife Status
		C_PageManager::SMState.getKnifeState(knifeState);
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.Knife.InEmergency=%s", String::Bool2Alpha(knifeState.alarms.getIsEStopPressed().getValue()));
		if (knifeState.alarms.getIsInAlarm() && !KnifeAlarm)
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.Knife.Alarmed=true");
		else if (!knifeState.alarms.getIsInAlarm() && KnifeAlarm)
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.Knife.Alarmed=false");
		KnifeAlarm = knifeState.alarms.getIsInAlarm();
		if (knifeState.control.getIsInManual())
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.Knife.Warned=true");
		else
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.Knife.Warned=false");
		
		//Stackers status
		for (int knifeLevel = 0; knifeLevel<MAX_NUM_LEVELS; knifeLevel++)
		{
			stackerState[knifeLevel] = C_PageManager::SMState.getStackerState(knifeLevel+1);
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.Stacker%d.InEmergency=%s", (knifeLevel+1), String::Bool2Alpha(stackerState[knifeLevel].alarms.getIsEStopPressed().getValue()));
			if (stackerState[knifeLevel].alarms.getIsInAlarm() && !StkAlarm[knifeLevel])
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.Stacker%d.Alarmed=true",(knifeLevel+1));
			else if(!stackerState[knifeLevel].alarms.getIsInAlarm() && StkAlarm[knifeLevel])
				o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.Stacker%d.Alarmed=false",(knifeLevel+1));	
			
			StkAlarm[knifeLevel] = stackerState[knifeLevel].alarms.getIsInAlarm().getValue();
		}

		//Check ready to Run Status

		//SYCReadyRun,
		if ((rdyRunState.getSYCReady() != SYCReadyRun) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.pct_SYCNotRdyRun.Visible=%s",(rdyRunState.getSYCReady())?L"false":L"true");
			SYCReadyRun = rdyRunState.getSYCReady();
		}
		//DBReadyRun,
		if ((rdyRunState.getDBReady()!= DBReadyRun) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.pct_DBNotRdyRun.Visible=%s",(rdyRunState.getDBReady())?L"false":L"true");
			DBReadyRun = rdyRunState.getDBReady();
		}
		//RSHReadyRun,
		if ((rdyRunState.getRSHReady()!= RSHReadyRun) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.pct_RSHNotRdyRun.Visible=%s",(rdyRunState.getRSHReady())?L"false":L"true");
			RSHReadyRun = rdyRunState.getRSHReady();
		}
		//SSReadyRun,
		if ((rdyRunState.getSSReady()!= SSReadyRun) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.pct_SSNotRdyRun.Visible=%s",(rdyRunState.getSSReady())?L"false":L"true");
			SSReadyRun = rdyRunState.getSSReady();
		}

		//KnifeReadyRun,
		if ((rdyRunState.getAllKnifeReady()!= KnifeReadyRun) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.pct_KNIFENotRdyRun.Visible=%s",(rdyRunState.getAllKnifeReady())?L"false":L"true");
			KnifeReadyRun = rdyRunState.getAllKnifeReady();
		}
		//Stk1ReadyRun,
		if ((rdyRunState.getStk1Ready()!= Stk1ReadyRun) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.pct_STK1NotRdyRun.Visible=%s",(rdyRunState.getStk1Ready())?L"false":L"true");
			Stk1ReadyRun = rdyRunState.getStk1Ready();
		}
		//Stk2ReadyRun,
		if ((rdyRunState.getStk2Ready()!= Stk2ReadyRun) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.pct_STK2NotRdyRun.Visible=%s",(rdyRunState.getStk2Ready())?L"false":L"true");
			Stk2ReadyRun = rdyRunState.getStk2Ready();
		}

		//Stk3ReadyRun,
		if ((rdyRunState.getStk3Ready()!= Stk3ReadyRun) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.pct_STK3NotRdyRun.Visible=%s",(rdyRunState.getStk3Ready())?L"false":L"true");
			Stk3ReadyRun = rdyRunState.getStk3Ready();
		}

		//Check ready to AOC Status

		//SYCReadyAOC,
		if ((rdyAOCState.getSYCReady() != SYCReadyAOC) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.pct_SYCNotRdyAOC.Visible=%s",(rdyAOCState.getSYCReady())?L"false":L"true");
			SYCReadyAOC = rdyAOCState.getSYCReady();
		}

		//DBReadyAOC,
		if ((rdyAOCState.getDBReady()!= DBReadyAOC) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.pct_DBNotRdyAOC.Visible=%s",(rdyAOCState.getDBReady())?L"false":L"true");
			DBReadyAOC = rdyAOCState.getDBReady();
		}
		//RSHReadyAOC,
		if ((rdyAOCState.getRSHReady()!= RSHReadyAOC) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.pct_RSHNotRdyAOC.Visible=%s",(rdyAOCState.getRSHReady())?L"false":L"true");
			RSHReadyAOC = rdyAOCState.getRSHReady();
		}
		//SSReadyAOC,
		if ((rdyAOCState.getSSReady()!= SSReadyAOC) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.pct_SSNotRdyAOC.Visible=%s",(rdyAOCState.getSSReady())?L"false":L"true");
			SSReadyAOC = rdyAOCState.getSSReady();
		}
		//KnifeReadyAOC,
		if ((rdyAOCState.getAllKnifeReady()!= KnifeReadyAOC) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.pct_KNIFENotRdyAOC.Visible=%s",(rdyAOCState.getAllKnifeReady())?L"false":L"true");
			KnifeReadyAOC = rdyAOCState.getAllKnifeReady();
		}
		//Stk1ReadyAOC,
		if ((rdyAOCState.getStk1Ready()!= Stk1ReadyAOC) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.pct_STK1NotRdyAOC.Visible=%s",(rdyAOCState.getStk1Ready())?L"false":L"true");
			Stk1ReadyAOC = rdyAOCState.getStk1Ready();
		}
		//Stk2ReadyAOC;
		if ((rdyAOCState.getStk2Ready()!= Stk2ReadyAOC) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.pct_STK2NotRdyAOC.Visible=%s",(rdyAOCState.getStk2Ready())?L"false":L"true");
			Stk2ReadyAOC = rdyAOCState.getStk2Ready();
		}
		//Stk3ReadyAOC;
		if ((rdyAOCState.getStk3Ready()!= Stk3ReadyAOC) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.pct_STK3NotRdyAOC.Visible=%s",(rdyAOCState.getStk3Ready())?L"false":L"true");
			Stk3ReadyAOC = rdyAOCState.getStk3Ready();
		}

		ProgramData currentProgramData;
		currentProgramData.Clear();
		C_PageManager::SMOrder.WETGetCurrentProgram(currentProgramData);
		o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.tlb_WetEndMetersLeft.Value=%d",currentProgramData.getProgramMetersLeft_UDm());
		if ( (aocState.isSourceDryEnd() != SourceDryEnd ) || (first) )
		{
			o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBarDryEnd.tlb_WetEndMetersLeft.BackColor=%s",
				(  aocState.isSourceDryEnd() )  ? L"WhiteSmoke" : L"Yellow");
			SourceDryEnd = aocState.isSourceDryEnd();
		}

		Sleep(1000);
		first = false;
	}

	logger.Fine(__FUNCTION__, L"Terminated statusBar_statusBarDryEnd page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_StatusBar::Twin400(LPVOID lpParam)
{
	const wchar_t* formName=L"statusBar_statusBarTwin400";
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

	while(!end_Twin400)
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
        //RFAZZI_FI26/01/2011_2. Aggiunta gestione Warning su grafica Twin400 di Syncro. 
				//o_socket.Send(Network::MSG_PAG_REF, L"%s.Warnings.Alarmed=%s", formName, String::Bool2Alpha(ssState.getAlarmsWarnings()));
				o_socket.Send(Network::MSG_PAG_REF, L"%s.Warnings.Warned=%s", formName, String::Bool2Alpha(ssState.getAlarmsWarnings()));
				o_socket.Send(Network::MSG_PAG_REF, L"%s.Software.Alarmed=%s", formName, String::Bool2Alpha(ssState.getAlarmsSoftware()));
				if (!allCases) break;
      //agrassi_FI09-11-2009_1
			//case State::C_SMState::SS_SetupSituation:
				//o_socket.Send(Network::MSG_PAG_REF,L"%s._SetSetupSituation(b[%s],b[%s],b[%s],b[%s],b[%s],b[%s])", formName, 
				//	String::Bool2Alpha(ssState.getSetupSituationWideToNarrow(0) && ssState.getSetupSituationValid(0)), 
				//	String::Bool2Alpha(ssState.getSetupSituationWideToNarrow(1) && ssState.getSetupSituationValid(1)), 
				//	String::Bool2Alpha(ssState.getSetupSituationWideToNarrow(2) && ssState.getSetupSituationValid(2)),
				//	String::Bool2Alpha(ssState.getSetupSituationNarrowToWide(0) && ssState.getSetupSituationValid(0)), 
				//	String::Bool2Alpha(ssState.getSetupSituationNarrowToWide(1) && ssState.getSetupSituationValid(1)),
				//	String::Bool2Alpha(ssState.getSetupSituationNarrowToWide(2) && ssState.getSetupSituationValid(2)));
				//if (!allCases) break;
		}
		allCases=false;
		event=C_PageManager::SMState.WaitSlitterScorerPlcChangeStatus(1000, StateID);
	}

	logger.Fine(__FUNCTION__, L"Terminated statusBar_statusBarTwin400 page threads");

	return ERROR_SUCCESS;
}

