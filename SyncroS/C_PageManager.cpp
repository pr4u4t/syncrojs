#include ".\C_PageManager.h"

//Logger include
#include "..\Shared\C_Logger.h"

//SocketTCP Include
#include "..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;

State::C_SMState C_PageManager::SMState;
DataBase::C_SMOrder C_PageManager::SMOrder;
C_SMLineConfig C_PageManager::SMLineConfig;
ProcessControl::C_SMPrc C_PageManager::SMProcess;
bool C_PageManager::stopGlobalUpdate;

C_PageManager::C_PageManager(void)
{
	stopGlobalUpdate = false;
}

C_PageManager::~C_PageManager(void)
{
}

void C_PageManager::ClosePageManager(void)
{
	logger.Info(__FUNCTION__, L"Start closing page manager");
	//Close the keep Alive thread
	::TerminateThread(pid_GlobalUpdate, NULL);
	::CloseHandle(pid_GlobalUpdate);
	pid_GlobalUpdate=NULL;
	//Close the page management
	this->TerminatePageThread(true);
	//Close the status bar thread
	statusBar.TerminateThread(true);
	Sleep(1000);
	logger.Info(__FUNCTION__, L"Page manager closed");
}

DWORD C_PageManager::CreateSM(void)
{
	DWORD res = ERROR_SUCCESS;
	res = C_PageManager::SMState.CreateShared();
	res = C_PageManager::SMProcess.CreateShared();
	res = C_PageManager::SMOrder.CreateShared();
	res = C_PageManager::SMLineConfig.CreateShared();
	return res;
}

DWORD C_PageManager::CreateGlobalUpdate(void)
{
	stopGlobalUpdate = false;
	pid_GlobalUpdate=::CreateThread(
		NULL,			// no security attributes
		0,				// use default stack size
		GlobalUpdate,		// thread function
		NULL,			// argument to thread function 
		0,				// use default creation flags 
		NULL);			// returns the thread identifier
	if (pid_GlobalUpdate == NULL)
	{
		DWORD error = GetLastError();
		logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Cannot create the GlobalUpdate thread ... %d",error);
		return error;
	}

	return ERROR_SUCCESS;
}

DWORD WINAPI C_PageManager::GlobalUpdate(LPVOID  lpParam)
{
	int i=0;
	int period = 500;
	int errorCounter = 0;
	
	SetTime();
	
	bool oldStopShift = C_PageManager::SMOrder.SHIFTGetCurrentShift().isStopShift();
	o_socket.Send(Network::MSG_RTC_REF,L"IsIdleShift = %s", String::Bool2Alpha(oldStopShift));

  bool oldZone1Used = C_PageManager::SMState.getSingleFacerState(1).rtData.getActive(State::RtData::iscurrent);
  bool oldZone2Used = C_PageManager::SMState.getSingleFacerState(2).rtData.getActive(State::RtData::iscurrent);

	o_socket.Send(Network::MSG_RTC_SWI,L"setActiveZone.SYCSW(%s,%s)", String::Bool2Alpha(oldZone1Used), String::Bool2Alpha(oldZone2Used));

  SetTimeZone();
	
	while(true)
	{
		if(stopGlobalUpdate)
		{
			Sleep(2000);
			continue;
		}

		i++;
		
		if(i % (3600000 / period) == 0)
		{//actions performed once per hour
			i=0;
		}
		
		if(i % (1800000 / period) == 0)
		{//actions performed twice per hour
			
		}

		if(i % (300000 / period) == 0)
		{//actions performed every 5 minute
			SetTime();			
		}

		if(i % (60000 / period) == 0)
		{//actions performed once per minute
			
		}
		
		if(i % (10000 / period) == 0)
		{//actions performed every 10 seconds

		}
		
		if(i % (5000 / period) == 0)
		{//actions performed every 5 seconds
			if(o_socket.Send(Network::MSG_COMM,L"keepAlive(VOID)")!=ERROR_SUCCESS)
			{
				errorCounter++;
				if(errorCounter > 6)
				{
					logger.Severe(__FUNCTION__, Logger::PRE_EVENT_RCV, L"SyncroS Automatic shutdown: The Client doesn't response in 1 minutes");
					exit(0);
				}
			}
			else
				errorCounter = 0;
		}
		
		//actions performed at every loop
		if (oldStopShift != C_PageManager::SMOrder.SHIFTGetCurrentShift().isStopShift()) 
		{
			oldStopShift = C_PageManager::SMOrder.SHIFTGetCurrentShift().isStopShift();
			o_socket.Send(Network::MSG_RTC_REF,L"IsIdleShift = %s", String::Bool2Alpha(oldStopShift));
		}
		
    if((oldZone1Used != C_PageManager::SMState.getSingleFacerState(1).rtData.getActive(State::RtData::iscurrent))
      || (oldZone2Used != C_PageManager::SMState.getSingleFacerState(2).rtData.getActive(State::RtData::iscurrent)))
    {
      oldZone1Used = C_PageManager::SMState.getSingleFacerState(1).rtData.getActive(State::RtData::iscurrent);
      oldZone2Used = C_PageManager::SMState.getSingleFacerState(2).rtData.getActive(State::RtData::iscurrent);

      o_socket.Send(Network::MSG_RTC_SWI,L"setActiveZone.SYCSW(%s,%s)", String::Bool2Alpha(oldZone1Used), String::Bool2Alpha(oldZone2Used));
    }

		::Sleep(period);
	}
}

//ABELLO_FI04/11/2008_1 Aggiunta la PopUp per il cambio di Data e Ora.
void C_PageManager::SetTimeZone(void)
{
  TIME_ZONE_INFORMATION tziOld;
  ZeroMemory(&tziOld, sizeof(tziOld));

  if(GetTimeZoneInformation(&tziOld) != TIME_ZONE_ID_INVALID)
  {
    o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetTimeZone(s[%s])", tziOld.StandardName);
  }
}

void C_PageManager::SetTime(void)
{
	//Update client date and time
	SYSTEMTIME systemtime;
	GetSystemTime(&systemtime);
	//N.B. the System Time is the GMT orario
	o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetDateTime(s[%d %d %d %d %d %d %d %d])",systemtime.wDayOfWeek,systemtime.wDay,systemtime.wMonth,systemtime.wYear,systemtime.wHour,systemtime.wMinute,systemtime.wSecond,systemtime.wMilliseconds);
}

DWORD C_PageManager::StopPage(void)
{
	stopGlobalUpdate = true;
	logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Communication OK restart the  GlobalUpdate");

	DWORD error=ERROR_SUCCESS;

	error=statusBar.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=productionGroup.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=statisticsGroup.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=settingsGroup.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=syncroSystemGroup.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=singleFacerGroup.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=splicerGroup.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=stackerGroup.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=knifeGroup.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=shearGroup.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=slitterScorerGroup.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=twin400Group.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=doubleBackerGroup.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=glueUnitGroup.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=preHeaterTernGroup.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=debugGroup.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=wetEndSupervisorGroup.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;
	error=dryEndSupervisorGroup.SuspendThread();
	if(error!=ERROR_SUCCESS)
		return error;

	return ERROR_SUCCESS;
}

DWORD C_PageManager::ResumePage(void)
{
	stopGlobalUpdate = false;
	logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Communication Error Stop the  GlobalUpdate");

	DWORD error=ERROR_SUCCESS;

	error=statusBar.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;

	error=productionGroup.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;

	error=statisticsGroup.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;

	error=settingsGroup.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;
		
	error=syncroSystemGroup.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;

	error=singleFacerGroup.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;

	error=splicerGroup.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;

	error=stackerGroup.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;

	error=slitterScorerGroup.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;
		
	error=knifeGroup.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;

	error=twin400Group.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;

	error=doubleBackerGroup.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;

	error=glueUnitGroup.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;

	error=preHeaterTernGroup.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;

	error=debugGroup.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;
		
	error=shearGroup.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;
		
	error=dryEndSupervisorGroup.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;
		
	error=wetEndSupervisorGroup.ResumeThread();
	if(error!=ERROR_SUCCESS)
		return error;

	return ERROR_SUCCESS;
}

void C_PageManager::TerminatePageThread(const bool terminateThread)
{
	productionGroup.TerminateThread(terminateThread);
	statisticsGroup.TerminateThread(terminateThread);
	syncroSystemGroup.TerminateThread(terminateThread);
	settingsGroup.TerminateThread(terminateThread);
	singleFacerGroup.TerminateThread(terminateThread);
	splicerGroup.TerminateThread(terminateThread);
	stackerGroup.TerminateThread(terminateThread);
	shearGroup.TerminateThread(terminateThread);
	slitterScorerGroup.TerminateThread(terminateThread);
	knifeGroup.TerminateThread(terminateThread);
	twin400Group.TerminateThread(terminateThread);
	doubleBackerGroup.TerminateThread(terminateThread);
	glueUnitGroup.TerminateThread(terminateThread);
	preHeaterTernGroup.TerminateThread(terminateThread);
	debugGroup.TerminateThread(terminateThread);
	wetEndSupervisorGroup.TerminateThread(terminateThread);
	dryEndSupervisorGroup.TerminateThread(terminateThread);
}

//MANAGE PAGE
bool C_PageManager::ManagePage(String::StrType thePage)
{
	logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV, L"RequestPage: %s", thePage.getPtr());

	//put the command for "PageManager" here ....
	if(thePage.SearchNoCase(L"PageManager"))
	{
		String::StrType cmd = thePage.extractCommand();
		if(cmd.CompareNoCase("PageManager.SetDIOModule"))
		{
			this->settingsGroup.SetDIOModule(thePage.ToInt());
			return true;
		}
		if(cmd.CompareNoCase("PageManager.SetCTCModule"))
		{
			this->settingsGroup.SetCTCModule(thePage.ToInt());
			return true;
		}
	}

	//get the group token
	String::StrType group = thePage.extractFirstToken(L"_");
	if(group.isEmpty())
		return false;

	if(group.CompareNoCase(L"statusBar"))
	{
		statusBar.TerminateThread(false);
		return statusBar.ChooseStaBar(thePage);
	} 
  if(group.CompareNoCase(L"lateralBar"))
	{
		lateralBar.TerminateThread(false);
		return lateralBar.ChooseLateralBar(thePage);
	}
	else
    //Close the previous thread
		this->TerminatePageThread(false);

	if(group.CompareNoCase(L"production"))
		return productionGroup.ChooseProductionPage(thePage);

	if(group.CompareNoCase(L"statistics"))
		return statisticsGroup.ChooseStatisticsPage(thePage);
		
	if(group.CompareNoCase(L"syncroSystem"))
		return syncroSystemGroup.ChooseSyncroSystemPage(thePage);

	if(group.CompareNoCase(L"settings"))		
		return settingsGroup.ChooseSettingsPage(thePage);

	if(group.CompareNoCase(L"singleFacer1"))
		return singleFacerGroup.ChooseSingleFacerPage(thePage,1);
	if(group.CompareNoCase(L"singleFacer2"))
		return singleFacerGroup.ChooseSingleFacerPage(thePage,2);
	if(group.CompareNoCase(L"singleFacer3"))
		return singleFacerGroup.ChooseSingleFacerPage(thePage,3);

	if(group.CompareNoCase(L"splicer1"))
		return splicerGroup.ChooseSplicerPage(thePage,1);
	if(group.CompareNoCase(L"splicer2"))
		return splicerGroup.ChooseSplicerPage(thePage,2);
	if(group.CompareNoCase(L"splicer3"))
		return splicerGroup.ChooseSplicerPage(thePage,3);
	if(group.CompareNoCase(L"splicer4"))
		return splicerGroup.ChooseSplicerPage(thePage,4);
	if(group.CompareNoCase(L"splicer5"))
		return splicerGroup.ChooseSplicerPage(thePage,5);
	if(group.CompareNoCase(L"splicer6"))
		return splicerGroup.ChooseSplicerPage(thePage,6);
	if(group.CompareNoCase(L"splicer7"))
		return splicerGroup.ChooseSplicerPage(thePage,7);

	if(group.CompareNoCase(L"stacker1"))
		return stackerGroup.ChooseStackerPage(thePage,1);
	if(group.CompareNoCase(L"stacker2"))
		return stackerGroup.ChooseStackerPage(thePage,2);
	if(group.CompareNoCase(L"stacker3"))
		return stackerGroup.ChooseStackerPage(thePage,3);

	if(group.CompareNoCase(L"shear"))
		return shearGroup.ChooseShearPage(thePage);

	if(group.CompareNoCase(L"slitterScorer"))
		return slitterScorerGroup.ChooseSlitterScorerPage(thePage);
		
	if(group.CompareNoCase(L"knife"))
		return knifeGroup.ChooseKnifePage(thePage);

	if(group.CompareNoCase(L"twin400"))
		return twin400Group.ChooseTwin400Page(thePage);

	if(group.CompareNoCase(L"doubleBacker"))
		return doubleBackerGroup.ChooseDoubleBackerPage(thePage);

	if(group.CompareNoCase(L"glueUnit"))
		return glueUnitGroup.ChooseGlueUnitPage(thePage);

	if(group.CompareNoCase(L"preHeaterTern"))
		return preHeaterTernGroup.ChoosePreHeaterTernPage(thePage);

	if(group.CompareNoCase(L"debug"))
		return debugGroup.ChooseDebugPage(thePage);

	if(group.CompareNoCase(L"dryEndSupervisor"))
		return dryEndSupervisorGroup.ChooseDryEndSupervisorGroupPage(thePage);
		
	if(group.CompareNoCase(L"wetEndSupervisor"))
		return wetEndSupervisorGroup.ChooseWetEndSupervisorGroupPage(thePage);

	return false;
}
