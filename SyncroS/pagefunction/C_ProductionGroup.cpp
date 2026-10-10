#include ".\C_ProductionGroup.h"

#include "..\C_PageManager.h"

//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;
//agrassi_FI21-03-2011_1
#include "..\Shared\C_SMTaskSettings.h"
extern TaskSettings::C_SMTaskSettings SMTaskSettings;


bool C_ProductionGroup::end_HomeDry;
bool C_ProductionGroup::end_HomeWet;
bool C_ProductionGroup::end_HomePrc;
bool C_ProductionGroup::end_InsWetOrder;
bool C_ProductionGroup::end_DryPastOrder;
bool C_ProductionGroup::end_QuickInsDryOrder;
bool C_ProductionGroup::end_DryOrdersList;
bool C_ProductionGroup::end_DryPastOrdersList;
bool C_ProductionGroup::end_DryWetOrdersList;
bool C_ProductionGroup::end_WetOrdersList;
bool C_ProductionGroup::end_Paper;
bool C_ProductionGroup::end_Alarm1;
bool C_ProductionGroup::end_Alarm2;
bool C_ProductionGroup::end_Quality;
bool C_ProductionGroup::end_Operator;
bool C_ProductionGroup::end_Printings;
bool C_ProductionGroup::end_MessagePRC;
//RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
bool C_ProductionGroup::end_Warning1;
bool C_ProductionGroup::end_Warning2;


C_ProductionGroup::C_ProductionGroup(void)
{
	pid_ProductionPage=NULL;
	TerminateThread(false);
}

C_ProductionGroup::~C_ProductionGroup(void)
{
}

bool C_ProductionGroup::ChooseProductionPage(String::StrType thePageRequest)
{
	//home
	if(thePageRequest.CompareNoCase(L"homeDry"))
	{
		end_HomeDry = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::HomeDry,NULL,0,NULL);
		return true;
	}
		
	if(thePageRequest.CompareNoCase(L"homeWet"))
	{
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::HomeWet,NULL,0,NULL);
		end_HomeWet = false;
		return true;
	}

	if(thePageRequest.CompareNoCase(L"homePrc"))
	{
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::HomePrc,NULL,0,NULL);
		end_HomePrc = false;
		return true;
	}

	//orders ins
	if(thePageRequest.CompareNoCase(L"insWetOrder"))
	{
		end_InsWetOrder = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::InsWetOrder,NULL,0,NULL);
		return true;
	}
	
	if(thePageRequest.CompareNoCase(L"quickInsDryOrder"))
	{
		end_QuickInsDryOrder = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::QuickInsDryOrder,NULL,0,NULL);
		return true;
	}

	if(thePageRequest.CompareNoCase(L"dryPastOrder"))
	{
		end_DryPastOrder = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::DryPastOrder,NULL,0,NULL);
		return true;
	}
	
	//orders list
	if(thePageRequest.CompareNoCase(L"dryOrdersList"))
	{
		end_DryOrdersList = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::DryOrdersList,NULL,0,NULL);
		return true;
	}
	
	if(thePageRequest.CompareNoCase(L"dryPastOrdersList"))
	{
		end_DryPastOrdersList = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::DryPastOrdersList,NULL,0,NULL);
		return true;
	}
	
	if(thePageRequest.CompareNoCase(L"dryWetOrdersList"))
	{
		end_DryWetOrdersList = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::DryWetOrdersList,NULL,0,NULL);
		return true;
	}
	
	if(thePageRequest.CompareNoCase(L"wetOrdersList"))
	{
		end_WetOrdersList = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::WetOrdersList,NULL,0,NULL);
		return true;
	}

	if(thePageRequest.CompareNoCase(L"alarm1"))
	{
		end_Alarm1 = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::Alarm1,NULL,0,NULL);
		return true;
	}
	
	if(thePageRequest.CompareNoCase(L"alarm2"))
	{
		end_Alarm2 = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::Alarm2,NULL,0,NULL);
		return true;
	}

  //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
	if(thePageRequest.CompareNoCase(L"warning1"))
	{
		end_Warning1 = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::Warning1,NULL,0,NULL);
		return true;
	}
	
	if(thePageRequest.CompareNoCase(L"warning2"))
	{
		end_Warning2 = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::Warning2,NULL,0,NULL);
		return true;
	}

	if(thePageRequest.CompareNoCase(L"paper"))
	{
		end_Paper = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::Paper,NULL,0,NULL);
		return true;
	}

	if(thePageRequest.CompareNoCase(L"quality"))
	{
		end_Quality = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::Quality,NULL,0,NULL);
		return true;
	}
	
	if(thePageRequest.CompareNoCase(L"operator"))
	{		
		end_Operator = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::Operator,NULL,0,NULL);
		return true;
	}

	if(thePageRequest.CompareNoCase(L"printings"))
	{
		end_Printings = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::Printings,NULL,0,NULL);
		return true;
	}

  if(thePageRequest.CompareNoCase(L"messagePRC"))
	{
		end_MessagePRC = false;
		pid_ProductionPage=::CreateThread(NULL,0,C_ProductionGroup::MessagePRC,NULL,0,NULL);
		return true;
	}

	return false;
}

void C_ProductionGroup::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_ProductionPage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_ProductionPage,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread ProductionPage ... %d", ::GetLastError());
		}
	}
	else
	{
		end_HomeDry = true;
		end_HomeWet = true;
		end_HomePrc = true;
		end_InsWetOrder = true;
		end_QuickInsDryOrder = true;
		end_DryPastOrder = true;
		end_DryOrdersList = true;
		end_DryPastOrdersList = true;
		end_DryWetOrdersList = true;
		end_WetOrdersList = true;
		end_Paper = true;
		end_Alarm1 = true;
		end_Alarm2 = true;
		end_Quality = true;
		end_Operator = true;
		end_Printings = true;
    end_MessagePRC = true;
    //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
		end_Warning1 = true;
		end_Warning2 = true;

	}
}

DWORD C_ProductionGroup::SuspendThread(void)
{
	if(pid_ProductionPage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_ProductionPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page Production Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_ProductionGroup::ResumeThread(void)
{
	if(pid_ProductionPage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_ProductionPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page Production Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}

//agrassi_FI23-03-2011_3
//PMASSARO_FI18/10/10 - Aggiunta Gestione Per Limitare La Velocità Di Linea In Base Alla Lunghezza Dell'Ordine
//bool C_ProductionGroup::limitateAocSpeedCauseOfShortOrder(int &optimalSpeed, int currAOCMaxSpeed)
//{
//  bool ret = false;
//  State::AOCState AOCState;
//	AOCState = C_PageManager::SMState.getAOCState();
//  LineConfig lineConfig;
//  C_PageManager::SMLineConfig.getLineConfig(lineConfig);
//
//  //agrassi_FI22-03-2011_1
//  //if ( (!C_PageManager::SMOrder.DRYGetNextSetup().isEmptySetupCode()) && 
//  //     (!AOCState.Type.getKnifeOnwards()) &&
//  //     (!AOCState.Type.getStackerOnwards()) )
//  if (!C_PageManager::SMOrder.DRYGetNextSetup().isEmptySetupCode())
//  {
//    if (AOCState.Type.getKnifeOnwards())
//    {
//      if (C_PageManager::SMState.getLastAOCState().Speed.getLimitationForShortOrder())
//      {
//        optimalSpeed=C_PageManager::SMState.getLastAOCState().Speed.getAOCMaxSpeed_UVmm_m();
//        ret=true;
//      }
//    }
//    else
//    {
//      int lenNext_UDm = C_PageManager::SMOrder.DRYGetNextSetup().getSetupLength_UDm();
//      int pos=3;
//      bool knifeOnly=false;
//      SetupData* setup=new SetupData();
//      while (C_PageManager::SMOrder.DRYGetInPosition(pos, *setup)==ERROR_SUCCESS)
//      {
//        if (setup->aocType.getKnifeOnwards())
//        {
//          lenNext_UDm+=setup->getSetupLength_UDm();
//          knifeOnly=true;
//          pos++;
//        }
//        else
//          break;
//      }
//      delete setup;
//      if ( lineConfig.operatorConfig.getIsActiveOperatorOrderMaxProdSpeed(lenNext_UDm) )
//      {
//        //agrassi_FI21-03-2011_1
//        //optimalSpeed = (lenNext_UDm * 1000) + 10000; //offset in più per rimanere sotto il minuto
//        optimalSpeed = (int)((double)(lenNext_UDm * UV_PER_M) * 1.5f);
//	      TaskSettings::Settings::GeneralSettings generalSettings;
//	      SMTaskSettings.getGeneralSettings(generalSettings);
//        //agrassi_FI22-03-2011_1
//        //if (generalSettings.isJob(L"3763"))
//        //{
//        //  if (lenNext_UDm<=50)
//        //    optimalSpeed=80000;
//        //}
//        if (optimalSpeed > currAOCMaxSpeed)
//          optimalSpeed = currAOCMaxSpeed;
//        ret = true;
//      }
//    }
//  }
//  return ret;
//}

DWORD WINAPI C_ProductionGroup::HomeDry(LPVOID  lpParam)
{
	State::AOCState aocState;
	State::StatisticsStateSetup  statisticsStateSetup;
	SetupData setupData;
	OrderData orderData;
	ShiftData shiftData;
	ProgramData currentProgramData;
  //agrassi_FI25-05-2010_1
  LineConfig lineConfig;
  C_PageManager::SMLineConfig.getLineConfig(lineConfig);


	o_socket.Send(Network::MSG_PAG_REF, L"production_homeDry._Show()");

	int
		cont = 5,
    speedLimit = 0, //PMASSARO_FI18/10/10 - Aggiunta Gestione Per Limitare La Velocità Di Linea In Base Alla Lunghezza Dell'Ordine
    tmpOptimalSpeedShortOrder = 0,
		sheetsGoodProduced_1 = 0,
		sheetsGoodProduced_2 = 0,
		sheetsGoodProduced_3 = 0,
		sheetsBadProduced_1 = 0,
		sheetsBadProduced_2 = 0,
		sheetsBadProduced_3 = 0,
		metersDRYProduced = 0,
		metersDRYRequired = 0,
		metersWETRequired = 0,
		AOCSpeed = 0,
    previousAOCSpeed = 0, //PMASSARO_FI18/10/10 - Aggiunta Gestione Per Limitare La Velocità Di Linea In Base Alla Lunghezza Dell'Ordine
		oldStopTime_UTs = 0,
		maxAOCSpeed = 0,
    //RFAZZI_FI14/03/2011_1. Aggiunta campo "Target Speed" su pagina HomeDry. 
    targetSpeed = 0,

    //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
    scrapMadeDischargeGroup_1 = 0,
    scrapMadeDischargeGroup_2 = 0,
    scrapMadeDischargeGroup_3 = 0;

  bool 
		//PMASSARO_FI06/10/08 - Bug Fix: During The "Reset OC" The Backcolor Was Not Correctly Set  
		lastAOCRunningStatus = false,
		draw_Order1 = true,
		clear_Order1 = true,
		draw_Order2 = true,
		clear_Order2 = true,
		draw_Order3 = true,
		clear_Order3 = true,
		first = true,
    //agrassi_FI16-05-2011_2
		oldLimitateForShortOrder=false;

	wchar_t strTime[MAX_PATH];

	//Speed Graphic Init
	o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.gph_Speed.Clear()");
	std::vector<int> speedList_UVm_m = C_PageManager::SMState.getSpeedHistory().getAverageSpeed_UVm_m();
	std::vector<int>::iterator Iter;
	for (Iter = speedList_UVm_m.begin( ); Iter != speedList_UVm_m.end( ); Iter++ )
		o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.gph_Speed.NewSpeed(i[%d])",*Iter);
	o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.gph_Speed.Draw()");

	while(!end_HomeDry)
	{
		//Speed Graphic
		std::vector<int> newspeedGraph = C_PageManager::SMState.getSpeedHistory().getAverageSpeed_UVm_m();
		if(speedList_UVm_m!=newspeedGraph)
		{
			speedList_UVm_m = newspeedGraph;
			Iter = newspeedGraph.end( );
			Iter--;
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.gph_Speed.NewSpeed(i[%d])",*Iter);
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.gph_Speed.Draw()");
		}

		//Wet end management
		currentProgramData.Clear();
		C_PageManager::SMOrder.WETGetCurrentProgram(currentProgramData);

		String::SYSTEMTIME2Alpha( currentProgramData.getStartTime(), strTime, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.tmt_WetEndStartTime.ValueAsUNIVERSALSyncroStyleDateTimeString=%s",strTime);
		o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.tnud_WetEndMetersProduced.Value=%d",currentProgramData.getProgramProducedFromLastWETAOCtoNow_UDm());


		aocState = C_PageManager::SMState.getAOCState();
		statisticsStateSetup = C_PageManager::SMState.getStatisticsStateCurrentSetup();
		C_PageManager::SMOrder.DRYGetCurrentSetup(setupData);
		shiftData = C_PageManager::SMOrder.SHIFTGetCurrentShift();
		currentProgramData.Clear();
		C_PageManager::SMOrder.WETGetCurrentProgram(currentProgramData);

    //PMASSARO_FI18/10/10 - Aggiunta Gestione Per Limitare La Velocità Di Linea In Base Alla Lunghezza Dell'Ordine
    ////////////////////     INIZIO MODIFICHE    ///////////////////////////////////////
    /*
		//RFazzi_FI09/08/2006_2.Bug Fixed sending AOC Speed to Production Page
		//RFazzi_FI01/06/2007_1.Added some modification for NoGapContinuousTrim
		if ( aocState.Type.getNoGap() ||
					aocState.Type.getNoGapCTrimOneLevel() )
		{
			if ( (aocState.Speed.getAOCNoGapMaxSpeed_UVmm_m() != AOCSpeed )||(first) )
			{
        //agrassi_FI25-05-2010_1
				//o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_AOCSpeed.Value=%d",aocState.Speed.getAOCNoGapMaxSpeed_UVmm_m());
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_AOCSpeed.Value=%d", min(aocState.Speed.getAOCNoGapMaxSpeed_UVmm_m(), lineConfig.operatorConfig.getOperatorMaxAOCSpeed_UVmm_m()));
				AOCSpeed = aocState.Speed.getAOCNoGapMaxSpeed_UVmm_m();
			}
		}
		else if ( aocState.Type.getKnifeOnwards() || aocState.Type.getStackerOnwards() ) 
		{
			if ( (aocState.Speed.getAOCMaxSpeed_UVmm_m() != AOCSpeed )||(first) )
			{
        //agrassi_FI25-05-2010_1
				//o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_AOCSpeed.Value=%d",aocState.Speed.getAOCMaxSpeed_UVmm_m());
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_AOCSpeed.Value=%d", min(aocState.Speed.getAOCMaxSpeed_UVmm_m(), lineConfig.operatorConfig.getOperatorMaxAOCSpeed_UVmm_m()));
				AOCSpeed = aocState.Speed.getAOCMaxSpeed_UVmm_m();
			}
		}
    //agrassi_FI25-05-2010_1
		else if ( aocState.Type.getTandem() ) 
		{
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_AOCSpeed.Value=%d", lineConfig.aoc.getTandemAOCSpeed_UV_mm_m());
		}
		else 
		{
			if ( (aocState.Speed.getAOCGapMaxSpeed_UVmm_m() != AOCSpeed )||(first) )
			{
				//o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_AOCSpeed.Value=%d",aocState.Speed.getAOCGapMaxSpeed_UVmm_m());
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_AOCSpeed.Value=%d", min(aocState.Speed.getAOCGapMaxSpeed_UVmm_m(), lineConfig.operatorConfig.getOperatorMaxAOCSpeed_UVmm_m()));
				AOCSpeed = aocState.Speed.getAOCGapMaxSpeed_UVmm_m();
			}
		}
    */
    //agrassi_FI23-03-2011_3
		//if ( aocState.Type.getNoGap() ||
		//			aocState.Type.getNoGapCTrimOneLevel() )
		//{
  //    AOCSpeed = aocState.Speed.getAOCNoGapMaxSpeed_UVmm_m();		
  //  }
		//else if ( aocState.Type.getKnifeOnwards() || aocState.Type.getStackerOnwards() ) 
		//{
  //    AOCSpeed = aocState.Speed.getAOCMaxSpeed_UVmm_m();
		//}
		//else if ( aocState.Type.getTandem() ) 
		//{
  //    AOCSpeed = lineConfig.aoc.getTandemAOCSpeed_UV_mm_m();
		//}
		//else 
		//{
  //    AOCSpeed = aocState.Speed.getAOCGapMaxSpeed_UVmm_m();
		//}

  //  speedLimit = lineConfig.operatorConfig.getOperatorMaxAOCSpeed_UVmm_m();
  //      
  //  if (limitateAocSpeedCauseOfShortOrder(tmpOptimalSpeedShortOrder, AOCSpeed)) 
  //    speedLimit = min(speedLimit, tmpOptimalSpeedShortOrder);

  //  AOCSpeed = min(AOCSpeed, speedLimit);
  ////////////////////     FINE MODIFICHE    ///////////////////////////////////////

    //agrassi_FI23-03-2011_3
    AOCSpeed=aocState.Speed.getAOCMaxSpeed_UVmm_m();
    if ((previousAOCSpeed != AOCSpeed) || first)
    {
      o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_AOCSpeed.Value=%d", AOCSpeed);
      //agrassi_FI23-03-2011_3
      wchar_t colorSpeed[30];
      if (aocState.Speed.getLimitationForShortOrder())
        wcscpy(colorSpeed, L"LightYellow");
      else
        wcscpy(colorSpeed, L"WhiteSmoke");
      o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_AOCSpeed.BackColor=%s", colorSpeed);
        
      previousAOCSpeed = AOCSpeed;
    }

		if ((aocState.Speed.getMaxProductionSpeed_UVmm_m() != maxAOCSpeed)||(first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_MaxSpeed.Value=%d",aocState.Speed.getMaxProductionSpeed_UVmm_m());
			maxAOCSpeed = aocState.Speed.getMaxProductionSpeed_UVmm_m();
		}
		
    //RFAZZI_FI14/03/2011_1. Aggiunta campo "Target Speed" su pagina HomeDry. 
    int minimo = min(lineConfig.operatorConfig.getOperatorMaxProductionSpeed_UVmm_m(), setupData.quality.qualityFluteData.getCruisingSpeed_UVm_m()*1000);
    minimo = min(minimo, aocState.Speed.getMaxProductionSpeed_UVmm_m());
    //agrassi_FI16-05-2011_2
    State::AOCState lastAocState=C_PageManager::SMState.getLastAOCState();
    bool limitateForShortOrder=lastAocState.Speed.getLimitationForShortOrder() &&
                                  (lastAocState.Speed.getAOCMaxSpeed_UVmm_m()<minimo);
    if (limitateForShortOrder)
      minimo=lastAocState.Speed.getAOCMaxSpeed_UVmm_m();

    //agrassi_FI16-05-2011_2
		//if ((minimo != targetSpeed)||(first))
		if ((minimo != targetSpeed) || (limitateForShortOrder!=oldLimitateForShortOrder) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_TargetSpeed.Value=%d",minimo);
			targetSpeed = minimo;
      wchar_t colorSpeed[30];
      if (limitateForShortOrder)
        wcscpy(colorSpeed, L"LightYellow");
      else
        wcscpy(colorSpeed, L"WhiteSmoke");
      o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_AOCSpeed.BackColor=%s", colorSpeed);
      oldLimitateForShortOrder=limitateForShortOrder;
		}

		if (( statisticsStateSetup.getDryEndProducedMetersLevel_UDm() != metersDRYProduced ) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_DRYMetresProduced.Value=%d",statisticsStateSetup.getDryEndProducedMetersLevel_UDm());
			metersDRYProduced = statisticsStateSetup.getDryEndProducedMetersLevel_UDm();
		}

		if (( setupData.getSetupLength_UDm() != metersDRYRequired ) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_DRYMetresRequired.Value=%d",setupData.getSetupLength_UDm());
			metersDRYRequired = setupData.getSetupLength_UDm();
		}


		if (( currentProgramData.getOrderLength() != metersWETRequired ) || (first))
		{
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.tnud_WetEndMetersRequired.Value=%d",currentProgramData.getOrderLength());
			metersWETRequired = currentProgramData.getOrderLength();
		}

		//Setup Management
		String::SYSTEMTIME2Alpha( statisticsStateSetup.getSetupStartTime(),strTime,MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.tmt_DrySetupStarted.ValueAsUNIVERSALSyncroStyleDateTimeString=%s",strTime);

		//ABello_FI06/11/2006_1 I DataBindings sulla pagina Home DRY non funzionano. Portato il refresh della pagina su SuncroS.
		o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.tlb_DRYSetupCode.Value=%s",setupData.getSetupCode());

		//sheets Produced

		//Order 1
		setupData.getOrder(1,orderData);
		if (orderData.isUsed())
		{
			//ABello_FI06/11/2006_1 I DataBindings sulla pagina Home DRY non funzionano. Portato il refresh della pagina su SuncroS.
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry._setLevelBinding_Format(i[1],s[%s],i[%d],s[%d],s[%s])",
																								orderData.customer.getName(), 
																								orderData.knife.getSegmentSheetCount(),
																								orderData.knife.levelName,
																								orderData.getOrderNumber());
			if (draw_Order1)
			{
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.pnl_Order1.Visible=True");
				draw_Order1 = false;
				clear_Order1 = true;
			}

			//PMASSARO_FI06/10/08 - Bug Fix: During The "Reset OC" The Backcolor Was Not Correctly Set  
			/*
			if (( statisticsStateSetup.getOrderGoodSheets(1) != sheetsGoodProduced_1 )||
				(first))
			*/
			if (( statisticsStateSetup.getOrderGoodSheets(1) != sheetsGoodProduced_1 )||
				(first) ||
				(aocState.getIsAOCInProgress() != lastAOCRunningStatus) )
			{
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_Good_Sheets_1.Value=%d",statisticsStateSetup.getOrderGoodSheets(KnifeLevel(1)));
				sheetsGoodProduced_1 = statisticsStateSetup.getOrderGoodSheets(1);
				
				//Set the backcolor property in according with the EarlyWraning or AOCinProgress Status
				if (aocState.getIsAOCInProgress())
					o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_Good_Sheets_1.BackColor=LightBlue");
				else if (aocState.getIsAOCEarlyWarning())
					o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_Good_Sheets_1.BackColor=Yellow");
				else
					o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_Good_Sheets_1.BackColor=WhiteSmoke");
				
				//PMASSARO_FI06/10/08 - Bug Fix: During The "Reset OC" The Backcolor Was Not Correctly Set
				lastAOCRunningStatus = aocState.getIsAOCInProgress();
			}
			if (( statisticsStateSetup.getOrderBadCuts(1) != sheetsBadProduced_1)||
				(first))
			{
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_Bad_Sheets_1.Value=%d",statisticsStateSetup.getOrderBadCuts(KnifeLevel(1)) * orderData.twin.outs );
				sheetsBadProduced_1 = statisticsStateSetup.getOrderBadCuts(KnifeLevel(1));
			}

      //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
			if ( (( statisticsStateSetup.getScrapMadeDischargeGroup_UDmm(1) !=  scrapMadeDischargeGroup_1 )||
				  (first) ) &&
          lineConfig.knife.getDischargeGroupPresent(statisticsStateSetup.getKnifeLevel(1)) )
			{
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_ScrapDischargeGroup1.Value=%d",statisticsStateSetup.getScrapMadeDischargeGroup_UDmm(KnifeLevel(1)));
				scrapMadeDischargeGroup_1 = statisticsStateSetup.getScrapMadeDischargeGroup_UDmm(KnifeLevel(1));
			}

			if(orderData.knife.prePrintPaper)
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.pct_OrderPreprint1.Visible=true" );
			else
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.pct_OrderPreprint1.Visible=false" );
		}
		else
		{
			if (clear_Order1)
			{
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.pnl_Order1.Visible=False");
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.pct_OrderPreprint1.Visible=false" );
        
        //ZETA 01 pulire tutto
        o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry._setLevelBinding_Format(i[1],s[ ],i[0],s[ ],s[ ])");
				
        clear_Order1 = false;
				draw_Order1 = true;
			}
		}
		
		//Order 2
		setupData.getOrder(2,orderData);
		if (orderData.isUsed())
		{
			//ABello_FI06/11/2006_1 I DataBindings sulla pagina Home DRY non funzionano. Portato il refresh della pagina su SuncroS.
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry._setLevelBinding_Format(i[2],s[%s],i[%d],s[%d],s[%s])",
																								orderData.customer.getName(), 
																								orderData.knife.getSegmentSheetCount(),
																								orderData.knife.levelName,
																								orderData.getOrderNumber());
			if (draw_Order2)
			{
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.pnl_Order2.Visible=True");
				draw_Order2 = false;
				clear_Order2 = true;
			}

			if (( statisticsStateSetup.getOrderGoodSheets(2) != sheetsGoodProduced_2 )||
				(first))
			{
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_Good_Sheets_2.Value=%d",statisticsStateSetup.getOrderGoodSheets(KnifeLevel(2)) );
				sheetsGoodProduced_2 = statisticsStateSetup.getOrderGoodSheets(2);
			}
			if (( statisticsStateSetup.getOrderBadCuts(2) != sheetsBadProduced_2)||
				(first))
			{
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_Bad_Sheets_2.Value=%d",statisticsStateSetup.getOrderBadCuts(KnifeLevel(2)) * orderData.twin.outs );
				sheetsBadProduced_2 = statisticsStateSetup.getOrderBadCuts(2);
			}

      //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
			if ( (( statisticsStateSetup.getScrapMadeDischargeGroup_UDmm(2) !=  scrapMadeDischargeGroup_2 )||
				  (first)) &&
          lineConfig.knife.getDischargeGroupPresent(statisticsStateSetup.getKnifeLevel(2)) )
			{
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_ScrapDischargeGroup2.Value=%d",statisticsStateSetup.getScrapMadeDischargeGroup_UDmm(KnifeLevel(2)));
				scrapMadeDischargeGroup_2 = statisticsStateSetup.getScrapMadeDischargeGroup_UDmm(KnifeLevel(2));
			}

			if(orderData.knife.prePrintPaper)
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.pct_OrderPreprint2.Visible=true" );
			else
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.pct_OrderPreprint2.Visible=false" );

		}
		else
		{
			if (clear_Order2)
			{
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.pnl_Order2.Visible=False");
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.pct_OrderPreprint2.Visible=false" );
        
        //ZETA 01 pulire tutto
        o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry._setLevelBinding_Format(i[2],s[ ],i[0],s[ ],s[ ])");
				
        clear_Order2 = false;
				draw_Order2 = true;
			}
		}

		//Order 3
		setupData.getOrder(3,orderData);
		if (orderData.isUsed())
		{
			//ABello_FI06/11/2006_1 I DataBindings sulla pagina Home DRY non funzionano. Portato il refresh della pagina su SuncroS.
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry._setLevelBinding_Format(i[3],s[%s],i[%d],s[%d],s[%s])",
																								orderData.customer.getName(), 
																								orderData.knife.getSegmentSheetCount(),
																								orderData.knife.levelName,
																								orderData.getOrderNumber());
			if (draw_Order3)
			{
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.pnl_Order3.Visible=True");
				draw_Order3 = false;
				clear_Order3 = true;
			}
			if (( statisticsStateSetup.getOrderGoodSheets(3) != sheetsGoodProduced_3 )||
				(first))
			{
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_Good_Sheets_3.Value=%d",statisticsStateSetup.getOrderGoodSheets(KnifeLevel(3)) );
				sheetsGoodProduced_3 = statisticsStateSetup.getOrderGoodSheets(3);
			}
			if (( statisticsStateSetup.getOrderBadCuts(3) != sheetsBadProduced_3)||
				(first))
			{
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_Bad_Sheets_3.Value=%d",statisticsStateSetup.getOrderBadCuts(KnifeLevel(3)) * orderData.twin.outs );
				sheetsBadProduced_3 = statisticsStateSetup.getOrderBadCuts(3);
			}

      //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
			if ( (( statisticsStateSetup.getScrapMadeDischargeGroup_UDmm(3) !=  scrapMadeDischargeGroup_3 )||
				  (first))&&
          lineConfig.knife.getDischargeGroupPresent(statisticsStateSetup.getKnifeLevel(3)) )
			{
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.nud_ScrapDischargeGroup3.Value=%d",statisticsStateSetup.getScrapMadeDischargeGroup_UDmm(KnifeLevel(3)));
				scrapMadeDischargeGroup_3 = statisticsStateSetup.getScrapMadeDischargeGroup_UDmm(KnifeLevel(3));
			}

			if(orderData.knife.prePrintPaper)
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.pct_OrderPreprint3.Visible=true" );
			else
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.pct_OrderPreprint3.Visible=false" );

		}
		else
		{
			if (clear_Order3)
			{
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.pnl_Order3.Visible=False");
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.pct_OrderPreprint3.Visible=false" );

        //ZETA 01 pulire tutto
        o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry._setLevelBinding_Format(i[3],s[ ],i[0],s[ ],s[ ])");

				clear_Order3 = false;
				draw_Order3 = true;
			}
		}

		//Update every 5 seconds
		if(cont == 5)
		{
			cont = 0;

      o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry._SetIsStopShift(b[%s])", String::Bool2Alpha(shiftData.isStopShift()));

			//Shift Data
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.lbl_ShiftID.Text=%c",shiftData.getShiftId());

			String::SYSTEMTIME2Alpha( shiftData.getStartTime(),strTime,MAX_PATH);
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.tmt_ShiftStartTime.ValueAsUNIVERSALSyncroStyleDateTimeString=%s",strTime);
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry.tlb_ShiftMetresProduced.Value=%d",shiftData.getTotalMetersProducedSS_UDm());

			int newStopTime_UTs = shiftData.getStopTime_UTs();
			if( (newStopTime_UTs - oldStopTime_UTs) > 5)
			{
				oldStopTime_UTs = newStopTime_UTs;
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry._SetShiftStopTime(I[%d])",oldStopTime_UTs);
			}
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeDry._SetCurrentShiftAvgSpeed(I[%d])",shiftData.getAvgSpeed_UVm_m());

		}
		else
			cont++;		
		
		Sleep(1000);
		first = false;
	}

	logger.Fine(__FUNCTION__, L"Terminated production_homeDry page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::HomeWet(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"production_homeWet._Show()");

	State::WECState wecState;
	wecState.Clear();
	State::SplicerCtrlData theSplicerState;
	theSplicerState.Clear();
	ShiftData shiftData;
	shiftData.Clear();
	ProgramData currentProgramData;
	currentProgramData.Clear();

	wchar_t strTime[MAX_PATH];
	int cont = 5;
	int oldStopTime_UTs = 0;
  //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
  bool localSplicerConfirmed[7];
  bool first = true;

  LineConfig lineConfig;
  C_PageManager::SMLineConfig.getLineConfig(lineConfig);

	while(!end_HomeWet)
	{
		C_PageManager::SMState.getWECState(wecState);
    C_PageManager::SMLineConfig.getLineConfig(lineConfig);

		//auto man bottons; splice confirmed checkboxes

    //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
		for (int splNumber = 1; splNumber <= SYNCRO_MAX_SPLICERS; splNumber++)
		{
      if ( lineConfig.macEnable.getSplicer(splNumber) || splNumber <= 5)
      {
			  State::SplicerState theSplicerState;
			  theSplicerState.Clear();
			  C_PageManager::SMState.getSplicerState(theSplicerState, splNumber);

			  switch (theSplicerState.Control.getMode())
			  {
				  case State::WECMODE_MANUAL:
					  o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.ToggleAutoManS%d.Auto=false", splNumber);
					  o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.stb_SpliceConfirmedS%d.Visible=false", splNumber);
					  break;
				  case State::WECMODE_AUTO:
					  o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.ToggleAutoManS%d.Auto=true", splNumber);
					  if ( wecState.Control.isSyncModeIdle() )
						  o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.stb_SpliceConfirmedS%d.Visible=false", splNumber);
					  else
						  o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.stb_SpliceConfirmedS%d.Visible=true", splNumber);
					  break;
				  case State::WECMODE_OVERRIDE:
					  o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.ToggleAutoManS%d.switching=true", splNumber);
					  o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.stb_SpliceConfirmedS%d.Visible=false", splNumber);
				  case State::WECMODE_NOMODE:
				  default:
					  break;
			  }

        if ( ( theSplicerState.Control.getSpliceConfirmed() != localSplicerConfirmed[splNumber - 1] )
              || first )
        {
			    o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.stb_SpliceConfirmedS%d.Text=%s", 
				    splNumber, theSplicerState.Control.getSpliceConfirmed() ? L"Sync" : L"No Sync");
			    o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.stb_SpliceConfirmedS%d.Active=%s", 
				    splNumber, theSplicerState.Control.getSpliceConfirmed() ? L"true" : L"false");

          localSplicerConfirmed[splNumber - 1] = theSplicerState.Control.getSpliceConfirmed();
        }

			  o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.chk_S%dReady.Checked=%s", splNumber, String::Bool2Alpha(theSplicerState.CtrlData.getReadyToSplice()));

			  o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet._setPaperTypeForSplicer(i[%d],s[%s],i[%d],i[%d],i[%d],i[%d],s[%s],i[%d],i[%d])",
				  splNumber, 
				  theSplicerState.rtData.getPaperType(splNumber, State::RtData::iscurrent), 
				  theSplicerState.rtData.getPaperWeigthFactor_UWg_m(splNumber, State::RtData::iscurrent),
				  theSplicerState.rtData.getWidth_UDmm(State::RtData::iscurrent),
				  theSplicerState.Monitor.getProductionRemaining_UDmm(),
				  theSplicerState.Monitor.getProductionUntilSplice_UDmm(),
				  theSplicerState.rtData.getPaperType(splNumber, State::RtData::isnext),
				  theSplicerState.rtData.getPaperWeigthFactor_UWg_m(splNumber, State::RtData::isnext),
				  theSplicerState.rtData.getWidth_UDmm(State::RtData::isnext)
				  );

      }
    }

		//syncButtons

    //RFAZZI_FI17/08/2011_1. Aggiunta la terza zona nella Home WetEnd. 
		o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.SyncButtonsActive(b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s],b[%s])",
			String::Bool2Alpha(wecState.Control.isSyncModeIdle()),
			String::Bool2Alpha(wecState.Control.isSyncModeMetres()),
			String::Bool2Alpha(wecState.Control.isSyncModeNow()),
			String::Bool2Alpha(wecState.Control.isSyncModeOnSplicerNumber(1)),
			String::Bool2Alpha(wecState.Control.isSyncModeOnSplicerNumber(2)),
			String::Bool2Alpha(wecState.Control.isSyncModeOnSplicerNumber(3)),
			String::Bool2Alpha(wecState.Control.isSyncModeOnSplicerNumber(4)),
			String::Bool2Alpha(wecState.Control.isSyncModeOnSplicerNumber(5)),
      String::Bool2Alpha(wecState.Control.isSyncModeOnSplicerNumber(6)),
      String::Bool2Alpha(wecState.Control.isSyncModeOnSplicerNumber(7)));

		o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.SyncButtonsEnabled(b[%s])",
			String::Bool2Alpha(! wecState.Control.isSyncModeChange() && wecState.Control.isModeAuto()));

		C_PageManager::SMOrder.WETGetCurrentProgram(currentProgramData);

		String::SYSTEMTIME2Alpha( currentProgramData.getStartTime(), strTime, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.tmt_WetEndStartTime.ValueAsUNIVERSALSyncroStyleDateTimeString=%s",strTime);
		o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.tnud_WetEndMetersProduced.Value=%d",currentProgramData.getProgramProducedFromLastWETAOCtoNow_UDm());
		o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.tnud_WetEndMetersRequired.Value=%d",currentProgramData.getOrderLength());
		o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.tlb_WetEndNote.Value=%s",currentProgramData.getNote());

		//Shift Data
		//Update every 5 seconds
		if(cont==5)
		{
			cont = 0;
			shiftData = C_PageManager::SMOrder.SHIFTGetCurrentShift();

      o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet._SetIsStopShift(b[%s])", String::Bool2Alpha(shiftData.isStopShift()));
			
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.lbl_ShiftID.Text=%c",shiftData.getShiftId());
			String::SYSTEMTIME2Alpha( shiftData.getStartTime(),strTime,MAX_PATH);
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.tmt_ShiftStartTime.ValueAsUNIVERSALSyncroStyleDateTimeString=%s",strTime);
			o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet.tlb_ShiftMetresProduced.Value=%d",shiftData.getTotalMetersProducedSS_UDm());
			
			int newStopTime_UTs = shiftData.getStopTime_UTs();
			if( (newStopTime_UTs - oldStopTime_UTs) > 5)
			{
				oldStopTime_UTs = newStopTime_UTs;
				o_socket.Send(Network::MSG_PAG_REF,L"production_homeWet._SetShiftStopTime(I[%d])",oldStopTime_UTs);
			}

		}
		else
			cont++;		

    first = false;

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated production_homeWet page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::HomePrc(LPVOID  lpParam)
{
  LineConfig lineConfig;
  C_PageManager::SMLineConfig.getLineConfig(lineConfig);

  o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc._Show()");

  ProcessControl::ProcessStatus prcState;

  State::SingleFacerState theSingleFacerState;
  theSingleFacerState.Clear();

	while(!end_HomePrc)
  {
    C_PageManager::SMProcess.getPrcStatus(prcState);
    C_PageManager::SMLineConfig.getLineConfig(lineConfig);

    //WARP
    //UpDown
    o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc._updateWarpUpDownValue(i[%d],b[%s])",prcState.getWarpUpDownValue(),String::Bool2Alpha(prcState.getCloseLoopWarpUpDownControlStatus()));
    //EndToEnd
    o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc._updateWarpEndToEndValue(i[%d],b[%s])",prcState.getWarpEndToEndValue(),String::Bool2Alpha(prcState.getCloseLoopWarpEndToEndControlStatus()));

    //ABELLO_FI24/11/2010_1 [PRC] Aggiunta gestione a grafica dell'umidità sopra/sotto
    //MOISTURE
    if(lineConfig.prc.header.getEnableBottom_UpperMoistureSensor())
    {
      //o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc._updateMoistureValue(i[%d],i[%d])",
      //  C_PageManager::SMState.getTemperature_MoistureSensorState().BOTTOM_LINER_getMoistureSensorAverageValue_UGc(),
      //  C_PageManager::SMState.getTemperature_MoistureSensorState().UPPER_LINER_getMoistureSensorAverageValue_UGc());
      o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc._updateMoistureValue(i[%d],i[%d])",
        C_PageManager::SMState.getTemperature_MoistureSensorState().BOTTOM_LINER_getMoistureSensorLastValue_UGc(),
        C_PageManager::SMState.getTemperature_MoistureSensorState().UPPER_LINER_getMoistureSensorLastValue_UGc());

    }

    //DB
    //SPEED
    //ABello_FI10/11/2010_1 fatta l'inversione della pagina home PRC
    if(!C_PageManager::SMOrder.SHIFTGetCurrentShift().isStopShift())
    {
      //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
      o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.tnud_SpeedDoubleBacker_UVmm_m.Text=%d",C_PageManager::SMState.getZoneDoubleBackerState().Monitor.getSpeed_UVm_m());

      int minimo = min(lineConfig.operatorConfig.getOperatorMaxProductionSpeed_UVmm_m(), C_PageManager::SMOrder.DRYGetCurrentSetup().quality.qualityFluteData.getCruisingSpeed_UVm_m()*1000);
        minimo = min(minimo, C_PageManager::SMState.getAOCState().Speed.getMaxProductionSpeed_UVmm_m());

      o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc._setTargetSpeedDoubleBacker(i[%d],i[%d])",
        C_PageManager::SMState.getZoneDoubleBackerState().Monitor.getSpeed_UVm_m(),
        minimo/1000
        );
    }
    else
    {
      //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
      o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.tnud_SpeedDoubleBacker_UVmm_m.Text=0");
      o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc._setTargetSpeedDoubleBacker(i[0],i[0])");
    }
    //TEMPERATURE
    if(C_PageManager::SMState.getTemperature_MoistureSensorState().DB_HOT_SECTION_getTemperatureSensorValue_UGc().getEnable())
      o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_db_hot_Section.Temperature=%d", C_PageManager::SMState.getTemperature_MoistureSensorState().DB_HOT_SECTION_getTemperatureSensorValue_UGc().getValue());
    if(C_PageManager::SMState.getTemperature_MoistureSensorState().DB_COLD_SECTION_getTemperatureSensorValue_UGc().getEnable())
      o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_db_cold_Section.Temperature=%d", C_PageManager::SMState.getTemperature_MoistureSensorState().DB_COLD_SECTION_getTemperatureSensorValue_UGc().getValue());

   //SPLICER PAPER INFORMATION
    for(int splicerNumber =1; splicerNumber <= 5; splicerNumber++)
    {
      if(prcState.getZoneNActive(lineConfig.getZoneNFromSplicerN(splicerNumber)))
      {
        //RFAZZI_FI_22/11/2010_1. Ora per il calcolo della tensione carta si usa la larghezza delle carte del programma e nn quella della bobina
        o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc._setSplicerInformation(i[%d],s[%s],i[%d],i[%d])",
          splicerNumber,
          prcState.getProgramData(lineConfig.getZoneNFromSplicerN(splicerNumber)).getPaperForSplicer(splicerNumber).getPaperID(),
          prcState.getProgramData(lineConfig.getZoneNFromSplicerN(splicerNumber)).getPaperForSplicer(splicerNumber).getWeightFactor_UWg_m(),
          prcState.getProgramData(lineConfig.getZoneNFromSplicerN(splicerNumber)).getPaperWidth_UDmm());


        //ABELLO_FI17/11/2010_4 Le carte che si vedono a grafica devono essere quelle che poi si usano per fare le query
        //C_PageManager::SMState.getSplicerState(splicerNumber).Rolls.Current.getPaperType(),
        //C_PageManager::SMState.getSplicerState(splicerNumber).Rolls.Current.getWeightFactor_UWg_m(),
        //C_PageManager::SMState.getSplicerState(splicerNumber).Rolls.Current.getWidth_UDmm());
      }
      else
        o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc._setSplicerInformation(i[%d],s[ ],i[0],i[0])",splicerNumber);
    }

    //STEAM GROUP
    for (int i=0; i<sizeof(managedDBSteamGroup)/sizeof(int); i++)
		{
      int steamGroupNumber = DBSteamGroupNumber(managedDBSteamGroup[i]);
      if (lineConfig.getDBSteamGroupByNumber(steamGroupNumber).getPresent())
      {
        o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamSection%d.Visible=true",steamGroupNumber);
			  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamSection%d.InLocal=%s",steamGroupNumber, 
          String::Bool2Alpha(C_PageManager::SMState.getDoubleBackerState().CtrlData.getSteamGroupByNumber(DBSteamGroupNumber(steamGroupNumber)).getIsLocal().getValue()));
			  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamSection%d.Auto=%s",steamGroupNumber, 
          String::Bool2Alpha(C_PageManager::SMState.getDoubleBackerState().CtrlData.getSteamGroupByNumber(DBSteamGroupNumber(steamGroupNumber)).getIsAuto().getValue()));
			  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamSection%d.Position=%d",steamGroupNumber, 
          C_PageManager::SMState.getDoubleBackerState().CtrlData.getSteamGroupByNumber(DBSteamGroupNumber(steamGroupNumber)).getSteamPressure_UPdb().getValue());

        o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamSection%d.Setpoint=%d",DBSteamGroupNumber(managedDBSteamGroup[i]), 
          (int)prcState.regulationValues.regulationValuesDBSteamGroup[i].getCalculatedSetpoint());
        o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamSection%d.IsActive=%s",DBSteamGroupNumber(managedDBSteamGroup[i]), 
          String::Bool2Alpha(prcState.regulationValues.regulationValuesDBSteamGroup[i].getEnabled()));
        o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamSection%d.WarpCorrection=%d",DBSteamGroupNumber(managedDBSteamGroup[i]),
          (int)prcState.regulationValues.regulationValuesDBSteamGroup[i].getWarpCorrection());
        o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamSection%d.UserCorrection=%d",DBSteamGroupNumber(managedDBSteamGroup[i]),
          (int)prcState.regulationValues.regulationValuesDBSteamGroup[i].getUserCorrection() );
      }
    }

    //DB HOT SECTION
    for (int i=0; i<sizeof(managedDBHotSection)/sizeof(int); i++)
		{
      int hotSectionNumber = DBHotSectionNumber(managedDBHotSection[i]);
      if (lineConfig.getDBHotSectionByNumber(hotSectionNumber).getPresent())
      {
        o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_ShoesPressure%d.Visible=true",hotSectionNumber);
			  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_ShoesPressure%d.InLocal=%s",hotSectionNumber, 
          String::Bool2Alpha(C_PageManager::SMState.getDoubleBackerState().CtrlData.getHotSectionByNumber(DBHotSectionNumber(hotSectionNumber)).getIsLocal().getValue()));
			  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_ShoesPressure%d.Auto=%s",hotSectionNumber, 
          String::Bool2Alpha(C_PageManager::SMState.getDoubleBackerState().CtrlData.getHotSectionByNumber(DBHotSectionNumber(hotSectionNumber)).getIsAuto().getValue()));
			  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_ShoesPressure%d.Position=%d",hotSectionNumber, 
          C_PageManager::SMState.getDoubleBackerState().CtrlData.getHotSectionByNumber(DBHotSectionNumber(hotSectionNumber)).getPressure_UPerc().getValue());

        o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_ShoesPressure%d.Setpoint=%d",DBHotSectionNumber(managedDBHotSection[i]),
            (int)prcState.regulationValues.regulationValuesDBHotSection[i].getCalculatedSetpoint());
        o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_ShoesPressure%d.IsActive=%s",DBHotSectionNumber(managedDBHotSection[i]),
            String::Bool2Alpha(prcState.regulationValues.regulationValuesDBHotSection[i].getEnabled()));
        o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_ShoesPressure%d.WarpCorrection=%d",DBHotSectionNumber(managedDBHotSection[i]),
          (int)prcState.regulationValues.regulationValuesDBHotSection[i].getWarpCorrection() );
        o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_ShoesPressure%d.UserCorrection=%d",DBHotSectionNumber(managedDBHotSection[i]),
          (int)prcState.regulationValues.regulationValuesDBHotSection[i].getUserCorrection() );
      }
    }

    //DB STEAM SHOWER
	  if(lineConfig.getSteamShowerByNumber(S4_DB).getPresent())
	  {
		  //o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamShower%d.Visible=true",S4_DB);
      //la visible a true è fatta sul client
		  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamShower%d.InLocal=%s",S4_DB, 
        String::Bool2Alpha(!C_PageManager::SMState.getDoubleBackerState().getCtrlData().steamShower.getIsSteamShowerRemote().getValue()));
		  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamShower%d.Auto=%s",S4_DB, 
        String::Bool2Alpha(C_PageManager::SMState.getDoubleBackerState().getCtrlData().steamShower.getIsSteamShowerAuto().getValue()));
		  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamShower%d.Position=%d",S4_DB, 
        C_PageManager::SMState.getDoubleBackerState().getCtrlData().steamShower.getSteamShower_UPerc().getValue());
		  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamShower%d.Setpoint=%d",S4_DB, 
        (int)prcState.regulationValues.regulationValuesSteamShowerSection[S4_DB-1].getCalculatedSetpoint());
      o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamShower%d.IsActive=%s",S4_DB, 
        String::Bool2Alpha(prcState.regulationValues.regulationValuesSteamShowerSection[S4_DB-1].getEnabled()));
      o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamShower%d.WarpCorrection=%d",S4_DB,
        (int)prcState.regulationValues.regulationValuesSteamShowerSection[S4_DB-1].getWarpCorrection() );
      o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamShower%d.UserCorrection=%d",S4_DB,
        (int)prcState.regulationValues.regulationValuesSteamShowerSection[S4_DB-1].getUserCorrection() );
	  }
  
    //ABELLO_FI01/12/2010_1 la somma delle grammature è meglio prendere quella delle carte, perchè quella degli uffici è solitamente diversa.
    int WeightFactorUWg_m = 
      prcState.getProgramDataMODIFY(0).getQuality().getPaperForNumber(5).getWeightFactor_UWg_m() +
      prcState.getProgramDataMODIFY(0).getQuality().getPaperForNumber(4).getWeightFactor_UWg_m() +
      prcState.getProgramDataMODIFY(0).getQuality().getPaperForNumber(3).getWeightFactor_UWg_m() +
      prcState.getProgramDataMODIFY(0).getQuality().getPaperForNumber(2).getWeightFactor_UWg_m() +
      prcState.getProgramDataMODIFY(0).getQuality().getPaperForNumber(1).getWeightFactor_UWg_m();

    //PAPER INFORMATION
    o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc._setZoneInformation(i[%d],s[%s],s[%s],s[%s],s[%s],s[%s],s[%s],i[%d])",
      0,
      prcState.getProgramDataMODIFY(0).getQuality().qualityFluteData.getFluteId(),
      prcState.getProgramDataMODIFY(0).getQuality().getPaperForNumber(5).getPaperID(),
      prcState.getProgramDataMODIFY(0).getQuality().getPaperForNumber(4).getPaperID(),
      prcState.getProgramDataMODIFY(0).getQuality().getPaperForNumber(3).getPaperID(),
      prcState.getProgramDataMODIFY(0).getQuality().getPaperForNumber(2).getPaperID(),
      prcState.getProgramDataMODIFY(0).getQuality().getPaperForNumber(1).getPaperID(), 
      WeightFactorUWg_m
      );

    //Quality
    o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc._setCurrentQualityId(s[%s])",
      prcState.getProgramData(0).getQuality().getQualityID());
  
    //ACTIVE & SPEED
    o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc._setActiveAndSpeed(b[%s],s[%c],s[%s],b[%s],b[%s],i[%d],i[%d],i[%d],i[%d],i[%d],i[%d])",
      String::Bool2Alpha(C_PageManager::SMOrder.SHIFTGetCurrentShift().isStopShift()),
      C_PageManager::SMOrder.SHIFTGetCurrentShift().isStopShift() ? L' ' : C_PageManager::SMOrder.SHIFTGetCurrentShift().getShiftId(),
      C_PageManager::SMOrder.SHIFTGetCurrentShift().isStopShift() ? L" " : C_PageManager::SMOrder.SHIFTGetCurrentShift().getShiftManagerName(),
      String::Bool2Alpha(prcState.getZoneNActive(1)),
      String::Bool2Alpha(prcState.getZoneNActive(2)),
      C_PageManager::SMState.getZoneSingleFacerState(1).Monitor.getSpeed_UVmm_m(),
      C_PageManager::SMState.getZoneSingleFacerState(2).Monitor.getSpeed_UVmm_m(),
      C_PageManager::SMState.getSplicerState(2).Monitor.getSpeed_UVmm_m(),
      C_PageManager::SMState.getSplicerState(3).Monitor.getSpeed_UVmm_m(),
      C_PageManager::SMState.getSplicerState(4).Monitor.getSpeed_UVmm_m(),
      C_PageManager::SMState.getSplicerState(5).Monitor.getSpeed_UVmm_m()
      );

    //SF
		for(int sfNumber=1; sfNumber<SYNCRO_MAX_SFS ; sfNumber++)
    {

      theSingleFacerState.Clear();
      C_PageManager::SMState.getSingleFacerState(theSingleFacerState,sfNumber);

      //MAIN STEAM
      if(theSingleFacerState.CtrlData.getFeedBackMainSteam_UPdb().getEnable())
		  {
			  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_sfmainsteam%d.Visible=true",sfNumber);
			  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_sfmainsteam%d.InLocal=%s",sfNumber, 
          String::Bool2Alpha(!theSingleFacerState.CtrlData.getIsMainSteamRemote().getValue()));
			  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_sfmainsteam%d.Auto=%s",sfNumber, 
          String::Bool2Alpha(theSingleFacerState.CtrlData.getIsMainSteamAuto().getValue()));
			  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_sfmainsteam%d.Position=%d",sfNumber, 
          theSingleFacerState.CtrlData.getFeedBackMainSteam_UPdb().getValue());
			  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_sfmainsteam%d.Setpoint=%d",sfNumber, 
          (int)prcState.regulationValues.regulationValuesSFMainSteam[sfNumber-1].getCalculatedSetpoint());
        o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_sfmainsteam%d.IsActive=%s",sfNumber, 
          String::Bool2Alpha(prcState.regulationValues.regulationValuesSFMainSteam[sfNumber-1].getEnabled()));
        o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_sfmainsteam%d.WarpCorrection=%d",sfNumber,
          (int)prcState.regulationValues.regulationValuesSFMainSteam[sfNumber-1].getWarpCorrection() );
        o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_sfmainsteam%d.UserCorrection=%d",sfNumber,
          (int)prcState.regulationValues.regulationValuesSFMainSteam[sfNumber-1].getUserCorrection() );
		  }

      //STEAM SHOWER
		  if(theSingleFacerState.CtrlData.steamShower.getIsSteamShowerAuto().getEnable())
		  {
			  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamShower%d.Visible=true",sfNumber);
			  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamShower%d.InLocal=%s",sfNumber, 
          String::Bool2Alpha(!theSingleFacerState.CtrlData.steamShower.getIsSteamShowerRemote().getValue()));
			  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamShower%d.Auto=%s",sfNumber, 
          String::Bool2Alpha(theSingleFacerState.CtrlData.steamShower.getIsSteamShowerAuto().getValue()));
			  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamShower%d.Position=%d",sfNumber, 
          theSingleFacerState.CtrlData.steamShower.getSteamShower_UPerc().getValue());
			  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamShower%d.Setpoint=%d",sfNumber, 
          (int)prcState.regulationValues.regulationValuesSteamShowerSection[sfNumber-1].getCalculatedSetpoint());
        o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamShower%d.IsActive=%s",sfNumber, 
          String::Bool2Alpha(prcState.regulationValues.regulationValuesSteamShowerSection[sfNumber-1].getEnabled()));
        o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamShower%d.WarpCorrection=%d",sfNumber,
          (int)prcState.regulationValues.regulationValuesSteamShowerSection[sfNumber-1].getWarpCorrection() );
        o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_SteamShower%d.UserCorrection=%d",sfNumber,
          (int)prcState.regulationValues.regulationValuesSteamShowerSection[sfNumber-1].getUserCorrection() );
		  }

      //FLUTE
      o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.lbl_Flute_Sf%d.Text=%c", sfNumber, theSingleFacerState.CtrlData.Monitor.getFluteId());
  
      int splNumberLiner = (sfNumber*2)+1;
      int splNumberMedium = (sfNumber*2);

      ////PAPER INFORMATION
      //o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc._setSplicerInformation(i[%d],s[%s],i[%d],i[%d])",
      //    1,
      //    prcState.getProgramDataMODIFY(0).getPaperForSplicer(1).getPaperID(),
      //    prcState.getProgramDataMODIFY(0).getPaperForSplicer(1).getWeightFactor_UWg_m(),
      //    prcState.getProgramDataMODIFY(0).getPaperWidth_UDmm());

      if(prcState.getZoneNActive(sfNumber))
      {
        o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc._setZoneInformation(i[%d],s[%c],s[%s],s[%s],i[%d])",
          sfNumber,
          prcState.getProgramDataMODIFY(sfNumber).getFluteforSingleFacer(sfNumber),
          prcState.getProgramDataMODIFY(sfNumber).getPaperForSplicer(splNumberLiner).getPaperID(),
          prcState.getProgramDataMODIFY(sfNumber).getPaperForSplicer(splNumberMedium).getPaperID(),
          (prcState.getProgramDataMODIFY(sfNumber).getPaperForSplicer(splNumberLiner).getWeightFactor_UWg_m() + prcState.getProgramData(sfNumber).getPaperForSplicer(splNumberMedium).getWeightFactor_UWg_m()));

        //o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc._setSplicerInformation(i[%d],s[%s],i[%d],i[%d])",
        //  splNumberLiner,
        //  prcState.getProgramDataMODIFY(sfNumber).getPaperForSplicer(splNumberLiner).getPaperID(),
        //  prcState.getProgramDataMODIFY(sfNumber).getPaperForSplicer(splNumberLiner).getWeightFactor_UWg_m(),
        //  prcState.getProgramDataMODIFY(sfNumber).getPaperWidth_UDmm());

        //o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc._setSplicerInformation(i[%d],s[%s],i[%d],i[%d])",
        //  splNumberMedium,
        //  prcState.getProgramDataMODIFY(sfNumber).getPaperForSplicer(splNumberMedium).getPaperID(),
        //  prcState.getProgramDataMODIFY(sfNumber).getPaperForSplicer(splNumberMedium).getWeightFactor_UWg_m(),
        //  prcState.getProgramDataMODIFY(sfNumber).getPaperWidth_UDmm());

      }
      else
      {
        o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc._setZoneInformation(i[%d],s[ ],s[ ],s[ ],i[0])", sfNumber);
        //o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc._setSplicerInformation(i[%d],s[ ],i[0],i[0])",splNumberLiner);
        //o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc._setSplicerInformation(i[%d],s[ ],i[0],i[0])",splNumberMedium);
      }

    }

    //WRAP ARM
    for (int i=0; i<sizeof(managedWrapArm)/sizeof(int); i++)
		{
      int	theWrapArmNumber = managedWrapArm[i];

      if (lineConfig.isAValidWrapArmNumber(theWrapArmNumber))
      {
        if (lineConfig.getWrapArmByNumber(theWrapArmNumber).getPresent())
		    {
          if(C_PageManager::SMState.getWrapArmByNumber(theWrapArmNumber).isUsed())
			    {
            o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_wrapArmPreHeater%d.Visible=true",theWrapArmNumber);
					  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_wrapArmPreHeater%d.InLocal=%s",theWrapArmNumber,
						  String::Bool2Alpha(C_PageManager::SMState.getWrapArmByNumber(theWrapArmNumber).getIsWrapArmLocal()));
					  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_wrapArmPreHeater%d.Auto=%s",theWrapArmNumber,
						  String::Bool2Alpha(C_PageManager::SMState.getWrapArmByNumber(theWrapArmNumber).getIsWrapArmAuto()));
					  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_wrapArmPreHeater%d.IsUnder=%s",theWrapArmNumber,
						  String::Bool2Alpha(C_PageManager::SMState.getWrapArmByNumber(theWrapArmNumber).getIsWrapArmUnder()));
					  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_wrapArmPreHeater%d.Position=%d",theWrapArmNumber,
						  C_PageManager::SMState.getWrapArmByNumber(theWrapArmNumber).getPosition_UPercINT().getValue());
					  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_wrapArmPreHeater%d.IsMaxPos=%s",theWrapArmNumber,
						  String::Bool2Alpha(C_PageManager::SMState.getWrapArmByNumber(theWrapArmNumber).getIsMaxPos()));
					  o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_wrapArmPreHeater%d.IsMinPos=%s",theWrapArmNumber,
						  String::Bool2Alpha(C_PageManager::SMState.getWrapArmByNumber(theWrapArmNumber).getIsMinPos()));
            o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_wrapArmPreHeater%d.Temperature=%d",theWrapArmNumber,
              C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(theWrapArmNumber).getValue());

            o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_wrapArmPreHeater%d.Setpoint=%d",theWrapArmNumber,
              (int)prcState.regulationValues.regulationValuesWrapArm[i].getCalculatedSetpoint());
            o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_wrapArmPreHeater%d.IsActive=%s",theWrapArmNumber,
              String::Bool2Alpha(prcState.regulationValues.regulationValuesWrapArm[i].getEnabled()));
            o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_wrapArmPreHeater%d.WarpCorrection=%d",theWrapArmNumber,
              (int)prcState.regulationValues.regulationValuesWrapArm[i].getWarpCorrection() );
            o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_wrapArmPreHeater%d.UserCorrection=%d",theWrapArmNumber,
              (int)prcState.regulationValues.regulationValuesWrapArm[i].getUserCorrection() );
          }
        }
      }
    }

    //GLUE GAP
    for (int i=0; i<sizeof(managedGlueGap)/sizeof(int); i++)
		{
      int theGlueGapNumber = managedGlueGap[i];

      if (lineConfig.isAValidGlueGapNumber(theGlueGapNumber))
      {
        if (lineConfig.getGlueGapByNumber(theGlueGapNumber).getPresent())
		    {
          if(C_PageManager::SMState.getGlueGapByNumber(theGlueGapNumber).isUsed())
			    {
            o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_glueGap%d.Visible=true",theGlueGapNumber);
				    o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_glueGap%d.InLocal=%s",theGlueGapNumber,
					    String::Bool2Alpha(C_PageManager::SMState.getGlueGapByNumber(theGlueGapNumber).getIsGlueGapLocal()));
				    o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_glueGap%d.Auto=%s",theGlueGapNumber,
					    String::Bool2Alpha(C_PageManager::SMState.getGlueGapByNumber(theGlueGapNumber).getIsGlueGapAuto()));
				    o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_glueGap%d.Position=%d",theGlueGapNumber,
					    C_PageManager::SMState.getGlueGapByNumber(theGlueGapNumber).getPositionINT().getValue());
				    o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_glueGap%d.IsMaxPos=%s",theGlueGapNumber,
					    String::Bool2Alpha(C_PageManager::SMState.getGlueGapByNumber(theGlueGapNumber).getIsMaxPos()));
				    o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_glueGap%d.IsMinPos=%s",theGlueGapNumber,
					    String::Bool2Alpha(C_PageManager::SMState.getGlueGapByNumber(theGlueGapNumber).getIsMinPos()));

            o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_glueGap%d.Setpoint=%d",theGlueGapNumber,
              (int)prcState.regulationValues.regulationValuesGlueGap[i].getCalculatedSetpoint());
            o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_glueGap%d.IsActive=%s",theGlueGapNumber,
              String::Bool2Alpha(prcState.regulationValues.regulationValuesGlueGap[i].getEnabled()));
            o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_glueGap%d.WarpCorrection=%d",theGlueGapNumber,
              (int)prcState.regulationValues.regulationValuesGlueGap[i].getWarpCorrection() );
            o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_glueGap%d.UserCorrection=%d",theGlueGapNumber,
              (int)prcState.regulationValues.regulationValuesGlueGap[i].getUserCorrection() );
          }
        }
      }
    }


    for (int i=0; i<sizeof(managedPaperTension)/sizeof(int); i++)
		{
      int thePaperTensionNumber = managedPaperTension[i];

      if (lineConfig.isAValidPaperTensionNumber(thePaperTensionNumber))
      {
        if (lineConfig.getPaperTensionByNumber(thePaperTensionNumber).getPresent())
		    {
          if(C_PageManager::SMState.getPaperTensionByNumber(PaperTensionNumber(thePaperTensionNumber)).getPaperTension_UFkgxm_s2().getEnable())
			    {
            if ( thePaperTensionNumber < B1_PT )
            {
              // Splicer Paper Tension
				      o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc.prc_paperTension%d.Visible=true", thePaperTensionNumber);
				      o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc.prc_paperTension%d.Position=%d", thePaperTensionNumber, 
                (int)C_PageManager::SMState.getSplicerState(thePaperTensionNumber).CtrlData.paperTensionState.getPaperTension_UFkgxm_s2().getValue());
		          o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc.prc_paperTension%d.InLocal=%s",thePaperTensionNumber,
			          String::Bool2Alpha(!C_PageManager::SMState.getSplicerState(thePaperTensionNumber).CtrlData.paperTensionState.getIsPaperTensionRemote().getValue()));
		          o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc.prc_paperTension%d.Auto=%s",thePaperTensionNumber,
			          String::Bool2Alpha(C_PageManager::SMState.getSplicerState(thePaperTensionNumber).CtrlData.paperTensionState.getIsPaperTensionAuto().getValue()));

              o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_paperTension%d.Setpoint=%d",thePaperTensionNumber,
                (int)prcState.regulationValues.regulationValuesPaperTension[i].getCalculatedSetpoint());
              o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_paperTension%d.IsActive=%s",thePaperTensionNumber,
                String::Bool2Alpha(prcState.regulationValues.regulationValuesPaperTension[i].getEnabled()));
              o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_paperTension%d.WarpCorrection=%d",thePaperTensionNumber,
                (int)prcState.regulationValues.regulationValuesPaperTension[i].getWarpCorrection() );
              o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_paperTension%d.UserCorrection=%d",thePaperTensionNumber,
                (int)prcState.regulationValues.regulationValuesPaperTension[i].getUserCorrection() );
            }
            else
            {
				      o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc.prc_paperTension%d.Visible=true", thePaperTensionNumber);
				      o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc.prc_paperTension%d.Position=%d", thePaperTensionNumber, 
                (int)C_PageManager::SMState.getBridgeState(thePaperTensionNumber-B1_PT+1).Monitor.paperTensionState.getPaperTension_UFkgxm_s2().getValue());
		          o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc.prc_paperTension%d.InLocal=%s",thePaperTensionNumber,
			          String::Bool2Alpha(!C_PageManager::SMState.getBridgeState(thePaperTensionNumber-B1_PT+1).Monitor.paperTensionState.getIsPaperTensionRemote().getValue()));
		          o_socket.Send(Network::MSG_PAG_REF, L"production_homePrc.prc_paperTension%d.Auto=%s",thePaperTensionNumber,
			          String::Bool2Alpha(C_PageManager::SMState.getBridgeState(thePaperTensionNumber-B1_PT+1).Monitor.paperTensionState.getIsPaperTensionAuto().getValue()));

              o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_paperTension%d.Setpoint=%d",thePaperTensionNumber,
                (int)prcState.regulationValues.regulationValuesPaperTension[i].getCalculatedSetpoint());
              o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_paperTension%d.IsActive=%s",thePaperTensionNumber,
                String::Bool2Alpha(prcState.regulationValues.regulationValuesPaperTension[i].getEnabled()));
              o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_paperTension%d.WarpCorrection=%d",thePaperTensionNumber,
                (int)prcState.regulationValues.regulationValuesPaperTension[i].getWarpCorrection() );
              o_socket.Send(Network::MSG_PAG_REF,L"production_homePrc.prc_paperTension%d.UserCorrection=%d",thePaperTensionNumber,
                (int)prcState.regulationValues.regulationValuesPaperTension[i].getUserCorrection() );

            }

          }
        }
      }
    }
    
		Sleep(1000);
  }

	logger.Fine(__FUNCTION__, L"Terminated production_homePrc page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::InsWetOrder(LPVOID lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"production_insWetOrder._Show()");
	while(!end_InsWetOrder)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated production_insWetOrder page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::QuickInsDryOrder(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"production_quickInsDryOrder._Show()");

	while(!end_QuickInsDryOrder)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated production_quickInsDryOrder page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::DryPastOrder(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"production_dryPastOrder._Show()");

	while(!end_DryPastOrder)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated production_dryPastOrder page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::DryOrdersList(LPVOID lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"production_dryOrdersList._Show()");

	wchar_t strTime[String::MAX_CHARS_SYSTEMTIME];
	SYSTEMTIME systemTime;
/*
	::ZeroMemory(&systemTime,sizeof(SYSTEMTIME));
	::GetSystemTime(&systemTime);

	String::SYSTEMTIME2Alpha(systemTime,strTime,String::MAX_CHARS_SYSTEMTIME);

	o_socket.Send(Network::MSG_PAG_REF, L"production_dryOrdersList._SetSetupStartTime(s[%s],i[%d],i[%d])",
		strTime,
		C_PageManager::SMState.getSpeedState().getSpeed_UVm_m(),
		C_PageManager::SMState.getStatisticsStateCurrentSetup().getMetersLeft_UDm());
*/
	int i = -1;

	while(!end_DryOrdersList)
	{
		Sleep(1000);
		i++;
		//every 5 seconds
		if((i%5)==0)
		{
			i=0;
			::ZeroMemory(&systemTime,sizeof(SYSTEMTIME));
			::GetSystemTime(&systemTime);

			String::SYSTEMTIME2Alpha(systemTime,strTime,String::MAX_CHARS_SYSTEMTIME);

			o_socket.Send(Network::MSG_PAG_REF, L"production_dryOrdersList._SetSetupStartTime(s[%s],i[%d],i[%d])",
				strTime,
			C_PageManager::SMState.getSpeedState().getSpeed_UVm_m(),
			C_PageManager::SMState.getStatisticsStateCurrentSetup().getMetersLeft_UDm());
		}
	}

	logger.Fine(__FUNCTION__, L"Terminated production_dryOrdersList page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::DryPastOrdersList(LPVOID lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"production_dryPastOrdersList._Show()");
	while(!end_DryPastOrdersList)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated production_dryPastOrdersList page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::DryWetOrdersList(LPVOID lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"production_dryWetOrdersList._Show()");
	while(!end_DryWetOrdersList)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated production_dryWetOrdersList page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::WetOrdersList(LPVOID lpParam)
{

	o_socket.Send(Network::MSG_PAG_REF, L"production_wetOrdersList._Show()");

	int i = -1;
	wchar_t strTime[String::MAX_CHARS_SYSTEMTIME];
	SYSTEMTIME systemTime;

   //RFAZZI_FI07/06/2010_1. Modificate liste ordini Dry e Wet per visualizzare i metri mancanti alla produzione di un certo ordine e relativo tempo in funzione della vel.attuale. 
	while(!end_WetOrdersList)
  {
		Sleep(1000);
		i++;
		//every 5 seconds
		if((i%5)==0)
		{
			i=0;
			::ZeroMemory(&systemTime,sizeof(SYSTEMTIME));
			::GetSystemTime(&systemTime);

			String::SYSTEMTIME2Alpha(systemTime,strTime,String::MAX_CHARS_SYSTEMTIME);

			o_socket.Send(Network::MSG_PAG_REF, L"production_wetOrdersList._SetProgramStartTime(s[%s],i[%d],i[%d])",
				strTime,
			C_PageManager::SMState.getSpeedState().getSpeed_UVm_m(),
			C_PageManager::SMOrder.WETGetCurrentProgram().getProgramMetersLeft_UDm());
		}
  }
    //Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated production_wetOrdersList page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::Alarm1(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"production_alarm1._Show()");

  SYSTEMTIME timeOfFirstStop;
  wchar_t reasonFirstOfStop[MAX_PATH] = L"";
  wchar_t timeOfFirstStopStr[MAX_PATH] = L"";

	while(!end_Alarm1)
  {
    if(C_PageManager::SMState.getStopReasonStateMODIFY().getFirstReasonOfStopItem(timeOfFirstStop, reasonFirstOfStop))
    {
      String::SYSTEMTIME2Alpha(timeOfFirstStop,timeOfFirstStopStr,MAX_PATH);
      o_socket.Send(Network::MSG_PAG_REF,L"production_alarm1._setFirstReasonOfStop(s[%s],s[%s])", timeOfFirstStopStr, reasonFirstOfStop);
    }
    else
      o_socket.Send(Network::MSG_PAG_REF,L"production_alarm1._setFirstReasonOfStop(s[ ],s[ ])");

    if(C_PageManager::SMState.getStopReasonStateMODIFY().getHistoryFirstReasonOfStopItem(timeOfFirstStop, reasonFirstOfStop))
    {
      String::SYSTEMTIME2Alpha(timeOfFirstStop,timeOfFirstStopStr,MAX_PATH);
      o_socket.Send(Network::MSG_PAG_REF,L"production_alarm1._setHistoryFirstReasonOfStop(s[%s],s[%s])", timeOfFirstStopStr, reasonFirstOfStop);
    }
    else
      o_socket.Send(Network::MSG_PAG_REF,L"production_alarm1._setHistoryFirstReasonOfStop(s[ ],s[ ])");

		Sleep(1000);
  }

	logger.Fine(__FUNCTION__, L"Terminated production_alarm1 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::Alarm2(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"production_alarm2._Show()");

	while(!end_Alarm2)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated production_alarm2 page threads");

	return ERROR_SUCCESS;
}

//RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
DWORD WINAPI C_ProductionGroup::Warning1(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"production_warning1._Show()");

  SYSTEMTIME timeOfFirstStop;
  wchar_t reasonFirstOfStop[MAX_PATH] = L"";
  wchar_t timeOfFirstStopStr[MAX_PATH] = L"";

	while(!end_Warning1)
  {
    if(C_PageManager::SMState.getStopReasonStateMODIFY().getFirstReasonOfStopItem(timeOfFirstStop, reasonFirstOfStop))
    {
      String::SYSTEMTIME2Alpha(timeOfFirstStop,timeOfFirstStopStr,MAX_PATH);
      o_socket.Send(Network::MSG_PAG_REF,L"production_warning1._setFirstReasonOfStop(s[%s],s[%s])", timeOfFirstStopStr, reasonFirstOfStop);
    }
    else
      o_socket.Send(Network::MSG_PAG_REF,L"production_warning1._setFirstReasonOfStop(s[ ],s[ ])");

    if(C_PageManager::SMState.getStopReasonStateMODIFY().getHistoryFirstReasonOfStopItem(timeOfFirstStop, reasonFirstOfStop))
    {
      String::SYSTEMTIME2Alpha(timeOfFirstStop,timeOfFirstStopStr,MAX_PATH);
      o_socket.Send(Network::MSG_PAG_REF,L"production_warning1._setHistoryFirstReasonOfStop(s[%s],s[%s])", timeOfFirstStopStr, reasonFirstOfStop);
    }
    else
      o_socket.Send(Network::MSG_PAG_REF,L"production_warning1._setHistoryFirstReasonOfStop(s[ ],s[ ])");

		Sleep(1000);
  }

	logger.Fine(__FUNCTION__, L"Terminated production_warning1 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::Warning2(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"production_warning2._Show()");

	while(!end_Warning2)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated production_warning2 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::Paper(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"production_paper._Show()");

	while(!end_Paper)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated production_paper page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::Quality(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"production_quality._Show()");

	while(!end_Quality)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated production_quality page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::Operator(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"production_operator._Show()");

	while(!end_Operator)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated production_operator page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::Printings(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"production_printings._Show()");

	//PMASSARO_FI02/05/08 - 5 - Added To Manage Pallet Print With Hardware Signals
	/*
		while(!end_Printings)
			Sleep(1000);
	*/
	LineConfig lineConfig;
	C_PageManager::SMLineConfig.getLineConfig(lineConfig);
	bool enabledLevel[3];
	bool levelPresentInOrder[3];
	int IdForLevel[3];
	int ModIdForLevel[3];
	int numLev = 0;
	int IdxLev = 0;
	int setupToSearch = 0;
	wchar_t SetUpForLevel1[100];
	wchar_t SetUpForLevel2[100];
	wchar_t SetUpForLevel3[100];
	
	while(!end_Printings)
	{
		Sleep(1000);

		numLev = lineConfig.knife.getLevelCount();
		
		enabledLevel[0] = false;
		enabledLevel[1] = false;
		enabledLevel[2] = false;

		IdForLevel[0] = 0;
		IdForLevel[1] = 0;
		IdForLevel[2] = 0;
		
		ModIdForLevel[0] = 0;
		ModIdForLevel[1] = 0;
		ModIdForLevel[2] = 0;

		levelPresentInOrder[0] = false;
		levelPresentInOrder[1] = false;
		levelPresentInOrder[2] = false;

		::wcscpy(SetUpForLevel1, L"-----");
		::wcscpy(SetUpForLevel2, L"-----");
		::wcscpy(SetUpForLevel3, L"-----");

		if (lineConfig.printers.getEnablePalletLabel() == true && lineConfig.printers.getPrintPalletLabelUsingDigitalInput())
		{
			switch (numLev)
			{
				case 1:
				{
					enabledLevel[0] = true;
					break;
				}
				case 2:
				{
					enabledLevel[0] = true;
					enabledLevel[2] = true;
					break;
				}
				case 3:
				{
					enabledLevel[0] = true;
					enabledLevel[1] = true;
					enabledLevel[2] = true;
					break;
				}
			}
		}

		for (IdxLev = 0; IdxLev <= 2; IdxLev++)
		{
			if ( enabledLevel[IdxLev] == true )
			{				
				switch(IdxLev)
				{
					case 0:
					{
						::wcscpy(SetUpForLevel1, C_PageManager::SMState.getStackerDataToPrint(IdxLev + 1).getDischargingSetupData());
						IdForLevel[0] = C_PageManager::SMState.getStackerDataToPrint(IdxLev + 1).getDischargingSetupID();
						ModIdForLevel[0] = C_PageManager::SMState.getStackerDataToPrint(IdxLev + 1).getDischargingSetupModID();
						levelPresentInOrder[0] = C_PageManager::SMState.getStackerDataToPrint(IdxLev + 1).getLevelIsWorkingInThisSetup();
						break;
					}
					case 1:
					{
						::wcscpy(SetUpForLevel2, C_PageManager::SMState.getStackerDataToPrint(IdxLev + 1).getDischargingSetupData());
						IdForLevel[1] = C_PageManager::SMState.getStackerDataToPrint(IdxLev + 1).getDischargingSetupID();
						ModIdForLevel[1] = C_PageManager::SMState.getStackerDataToPrint(IdxLev + 1).getDischargingSetupModID();
						levelPresentInOrder[1] = C_PageManager::SMState.getStackerDataToPrint(IdxLev + 1).getLevelIsWorkingInThisSetup();
						break;
					}
					case 2:
					{
						::wcscpy(SetUpForLevel3, C_PageManager::SMState.getStackerDataToPrint(numLev).getDischargingSetupData());
						IdForLevel[2] = C_PageManager::SMState.getStackerDataToPrint(numLev).getDischargingSetupID();
						ModIdForLevel[2] = C_PageManager::SMState.getStackerDataToPrint(numLev).getDischargingSetupModID();
						levelPresentInOrder[2] = C_PageManager::SMState.getStackerDataToPrint(numLev).getLevelIsWorkingInThisSetup();
						break;
					}
					default:
					{
						break;
					}
				}
			}
		}
		
		//Managment Of Data To Print Status
		/*
		o_socket.Send(Network::MSG_PAG_REF,L"production_printings._UpdateStatusPrintingIdx(b[%s],d[%s],d[%s],s[%s],b[%s],d[%s],d[%s],s[%s],b[%s],d[%s],d[%s],s[%s])",
			String::Bool2Alpha(enabledLevel[0]),
			IdForLevel[0],
			ModIdForLevel[0],
			SetUpForLevel1,
			String::Bool2Alpha(enabledLevel[1]),
			IdForLevel[1],
			ModIdForLevel[1],
			SetUpForLevel2,
			String::Bool2Alpha(enabledLevel[2]),
			IdForLevel[2],
			ModIdForLevel[2],
			SetUpForLevel3);
		*/
   
		wchar_t separ[30] = L"  *";
		wchar_t txtLabelTranslation[200]=L"";
		wchar_t txtTranslationLevelWorking[200]=L"";
		wchar_t txt[200] = L" ";
		
		swprintf(txtLabelTranslation,L"§%S", stringer(SYCS_62));		
		if (levelPresentInOrder[0] == true)
		{
			swprintf(txtTranslationLevelWorking,L"§%S", stringer(SYCS_60));
		}
		else
		{
			swprintf(txtTranslationLevelWorking,L"§%S", stringer(SYCS_61));
		}
		swprintf(txt, L"%s#  %s (%d-%d)%s#%s", txtLabelTranslation, SetUpForLevel1, IdForLevel[0], ModIdForLevel[0], separ, txtTranslationLevelWorking);
		o_socket.Send( Network::MSG_PAG_REF,L"production_printings.lbl_PrintingIdxL_NN.Text=%s", txt);
		o_socket.Send( Network::MSG_PAG_REF,L"production_printings.lbl_PrintingIdxL_NN.Visible=%s", String::Bool2Alpha(enabledLevel[0]));
		o_socket.Send( Network::MSG_PAG_REF,L"production_printings.btn_PrintL_NN.Enabled=%s", String::Bool2Alpha(enabledLevel[0]));
		o_socket.Send( Network::MSG_PAG_REF,L"production_printings.btn_PrintL_NN.Visible=%s", String::Bool2Alpha(enabledLevel[0]));

		swprintf(txtLabelTranslation,L"§%S", stringer(SYCS_63));
		if (levelPresentInOrder[1] == true)
		{
			swprintf(txtTranslationLevelWorking,L"§%S", stringer(SYCS_60));
		}
		else
		{
			swprintf(txtTranslationLevelWorking,L"§%S", stringer(SYCS_61));
		}
		swprintf(txt, L"%s#  %s (%d-%d)%s#%s", txtLabelTranslation, SetUpForLevel2, IdForLevel[1], ModIdForLevel[1], separ, txtTranslationLevelWorking);
		o_socket.Send( Network::MSG_PAG_REF,L"production_printings.lbl_PrintingIdxM_NN.Text=%s", txt);
		o_socket.Send( Network::MSG_PAG_REF,L"production_printings.lbl_PrintingIdxM_NN.Visible=%s", String::Bool2Alpha(enabledLevel[1]));
		o_socket.Send( Network::MSG_PAG_REF,L"production_printings.btn_PrintM_NN.Enabled=%s", String::Bool2Alpha(enabledLevel[1]));
		o_socket.Send( Network::MSG_PAG_REF,L"production_printings.btn_PrintM_NN.Visible=%s", String::Bool2Alpha(enabledLevel[1]));

		swprintf(txtLabelTranslation,L"§%S", stringer(SYCS_64));
		if (levelPresentInOrder[2] == true)
		{
			swprintf(txtTranslationLevelWorking,L"§%S", stringer(SYCS_60));
		}
		else
		{
			swprintf(txtTranslationLevelWorking,L"§%S", stringer(SYCS_61));
		}
		swprintf(txt, L"%s#  %s (%d-%d)%s#%s", txtLabelTranslation, SetUpForLevel3, IdForLevel[2], ModIdForLevel[2], separ, txtTranslationLevelWorking);
		o_socket.Send( Network::MSG_PAG_REF,L"production_printings.lbl_PrintingIdxU_NN.Text=%s", txt);
		o_socket.Send( Network::MSG_PAG_REF,L"production_printings.lbl_PrintingIdxU_NN.Visible=%s", String::Bool2Alpha(enabledLevel[2]));
		o_socket.Send( Network::MSG_PAG_REF,L"production_printings.btn_PrintU_NN.Enabled=%s", String::Bool2Alpha(enabledLevel[2]));
		o_socket.Send( Network::MSG_PAG_REF,L"production_printings.btn_PrintU_NN.Visible=%s", String::Bool2Alpha(enabledLevel[2]));

	}

	logger.Fine(__FUNCTION__, L"Terminated production_printings page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_ProductionGroup::MessagePRC(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"production_MessagePRC._Show()");

	while(!end_MessagePRC)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated production_MessagePRC page threads");

	return ERROR_SUCCESS;
}