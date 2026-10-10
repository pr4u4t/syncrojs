#include ".\C_SyncroSystemGroup.h"

#include "..\C_PageManager.h"

//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

#include "..\Shared\C_SMTaskSettings.h"
extern TaskSettings::C_SMTaskSettings SMTaskSettings;


bool C_SyncroSystemGroup::end_SYNCROSettings;
bool C_SyncroSystemGroup::end_SYNCROStatus;
bool C_SyncroSystemGroup::end_SYNCROTask1;
bool C_SyncroSystemGroup::end_SYNCROTask2;
bool C_SyncroSystemGroup::end_AOCSettings;
bool C_SyncroSystemGroup::end_AOCStatus;
bool C_SyncroSystemGroup::end_WECSettings;
bool C_SyncroSystemGroup::end_WECStatus;
bool C_SyncroSystemGroup::end_SpeedSettings;
bool C_SyncroSystemGroup::end_SpeedStatus;
bool C_SyncroSystemGroup::end_SpeedReferenceSettings;
bool C_SyncroSystemGroup::end_SpeedReferenceStatus;
bool C_SyncroSystemGroup::end_PRINTERSSettings;
bool C_SyncroSystemGroup::end_PRINTERSStatus;
//RFazzi_FI28/09/2006_2.Added Office LineConfig section
bool C_SyncroSystemGroup::end_OFFICESettings;
bool C_SyncroSystemGroup::end_OFFICEStatus;
bool C_SyncroSystemGroup::end_PRCSettings;
bool C_SyncroSystemGroup::end_LOG1;
bool C_SyncroSystemGroup::end_LOG2;
bool C_SyncroSystemGroup::end_LOG3;
bool C_SyncroSystemGroup::end_LOG4;

C_SyncroSystemGroup::C_SyncroSystemGroup(void)
{
	pid_SyncroSystemPage=NULL;
	TerminateThread(false);
}

C_SyncroSystemGroup::~C_SyncroSystemGroup(void)
{
}

bool C_SyncroSystemGroup::ChooseSyncroSystemPage(String::StrType thePageRequest)
{
	//AOCSettings
	if(thePageRequest.CompareNoCase(L"AOCSettings"))
	{
		end_AOCSettings = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::AOCSettings,NULL,0,NULL);
		return true;
	}
	//AOCStatus
	if(thePageRequest.CompareNoCase(L"AOCStatus"))
	{
		end_AOCStatus = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::AOCStatus,NULL,0,NULL);
		return true;
	}
	//WECSettings
	if(thePageRequest.CompareNoCase(L"WECSettings"))
	{
		end_WECSettings = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::WECSettings,NULL,0,NULL);
		return true;
	}
	//WECStatus
	if(thePageRequest.CompareNoCase(L"WECStatus"))
	{
		end_WECStatus = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::WECStatus,NULL,0,NULL);
		return true;
	}
	//SpeedSettings
	if(thePageRequest.CompareNoCase(L"SpeedSettings"))
	{
		end_SpeedSettings = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::SpeedSettings,NULL,0,NULL);
		return true;
	}
	//SpeedStatus
	if(thePageRequest.CompareNoCase(L"SpeedStatus"))
	{
		end_SpeedStatus = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::SpeedStatus,NULL,0,NULL);
		return true;
	}
	//SpeedReferenceSettings
	if(thePageRequest.CompareNoCase(L"SpeedReferenceSettings"))
	{
		end_SpeedReferenceSettings = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::SpeedReferenceSettings,NULL,0,NULL);
		return true;
	}
	//SpeedReferenceStatus
	if(thePageRequest.CompareNoCase(L"SpeedReferenceStatus"))
	{
		end_SpeedReferenceStatus = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::SpeedReferenceStatus,NULL,0,NULL);
		return true;
	}

	//SYNCROSettings
	if(thePageRequest.CompareNoCase(L"SYNCROSettings"))
	{
		end_SYNCROSettings = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::SYNCROSettings,NULL,0,NULL);
		return true;
	}
	//SYNCROStatus
	if(thePageRequest.CompareNoCase(L"SYNCROStatus"))
	{
		end_SYNCROStatus = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::SYNCROStatus,NULL,0,NULL);
		return true;
	}
	//SYNCROTask1
	if(thePageRequest.CompareNoCase(L"SYNCROTask1"))
	{
		end_SYNCROTask1 = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::SYNCROTask1,NULL,0,NULL);
		return true;
	}
	//SYNCROTask2
	if(thePageRequest.CompareNoCase(L"SYNCROTask2"))
	{
		end_SYNCROTask2 = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::SYNCROTask2,NULL,0,NULL);
		return true;
	}
	//PRINTERSSettings
	if(thePageRequest.CompareNoCase(L"PRINTERSSettings"))
	{
		end_PRINTERSSettings = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::PRINTERSSettings,NULL,0,NULL);
		return true;
	}
	//PRINTERSStatus
	if(thePageRequest.CompareNoCase(L"PRINTERSStatus"))
	{
		end_PRINTERSStatus = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::PRINTERSStatus,NULL,0,NULL);
		return true;
	}
	//RFazzi_FI28/09/2006_2.Added Office LineConfig section
	//OFFICESettings
	if(thePageRequest.CompareNoCase(L"OFFICESettings"))
	{
		end_OFFICESettings = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::OFFICESettings,NULL,0,NULL);
		return true;
	}
	//OFFICEStatus
	if(thePageRequest.CompareNoCase(L"OFFICEStatus"))
	{
		end_OFFICEStatus = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::OFFICEStatus,NULL,0,NULL);
		return true;
	}
	//PrcSettings 
	if(thePageRequest.CompareNoCase(L"PrcSettings"))
	{
		end_PRCSettings = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::PrcSettings,NULL,0,NULL);
		return true;
	}

	//LOG1
	if(thePageRequest.CompareNoCase(L"Log1"))
	{
		end_LOG1 = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::LOG1,NULL,0,NULL);
		return true;
	}
	//LOG2
	if(thePageRequest.CompareNoCase(L"Log2"))
	{
		end_LOG2 = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::LOG2,NULL,0,NULL);
		return true;
	}
	//LOG3
	if(thePageRequest.CompareNoCase(L"Log3"))
	{
		end_LOG3 = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::LOG3,NULL,0,NULL);
		return true;
	}
	//LOG4
	if(thePageRequest.CompareNoCase(L"Log4"))
	{
		end_LOG4 = false;
		pid_SyncroSystemPage=::CreateThread(NULL,0,C_SyncroSystemGroup::LOG4,NULL,0,NULL);
		return true;
	}


	return false;
}

void C_SyncroSystemGroup::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_SyncroSystemPage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_SyncroSystemPage,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread SyncroSystemPage ... %d", ::GetLastError());
		}
	}
	else
	{
		end_SYNCROSettings = true;
		end_SYNCROStatus = true;
		end_SYNCROTask1 = true;
		end_SYNCROTask2 = true;
		end_AOCSettings = true;
		end_AOCStatus = true;
		end_WECSettings = true;
		end_WECStatus = true;
		end_SpeedSettings = true;
		end_SpeedStatus = true;
		end_SpeedReferenceSettings = true;
		end_SpeedReferenceStatus = true;
		end_PRINTERSSettings = true;
		end_PRINTERSStatus = true;
		//RFazzi_FI28/09/2006_2.Added Office LineConfig section
		end_OFFICESettings = true;
		end_OFFICEStatus = true;
		end_PRCSettings = true;
	}
}

DWORD C_SyncroSystemGroup::SuspendThread(void)
{
	if(pid_SyncroSystemPage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_SyncroSystemPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page SyncroSystem Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_SyncroSystemGroup::ResumeThread(void)
{
	if(pid_SyncroSystemPage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_SyncroSystemPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page SyncroSystem Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::SYNCROTask1(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_SYNCROTask1._Show()");

	while(!end_SYNCROTask1)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_SYNCROTask1 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::SYNCROTask2(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_SYNCROTask2._Show()");

	while(!end_SYNCROTask2)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_SYNCROTask2 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::SYNCROSettings(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_SYNCROSettings._Show()");

	while(!end_SYNCROSettings)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_SYNCROSettings page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::SYNCROStatus(LPVOID  lpParam)
{
	wchar_t strTime[MAX_PATH];
	State::DownTimeState downTimeState;

	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_SYNCROStatus._Show()");

	while(!end_SYNCROStatus)
	{
		downTimeState = C_PageManager::SMState.getDownTimeState();

		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SYNCROStatus.chk_DownTimeProgress.Checked=%s",String::Bool2Alpha(downTimeState.getDownTimeProgressFlag()));
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SYNCROStatus.chk_MicroStopElapsed.Checked=%s",String::Bool2Alpha(downTimeState.getMicroStopTimeElapsedFlag()));
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SYNCROStatus.chk_WaitingRestartDelay.Checked=%s",String::Bool2Alpha(downTimeState.getWaitRestartTimeFlag()));
		String::SYSTEMTIME2Alpha( downTimeState.getStartDownTime(),strTime,MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SYNCROStatus.tmt_LineStopped.ValueAsUNIVERSALSyncroStyleDateTimeString=%s",strTime);
		String::SYSTEMTIME2Alpha( downTimeState.getEndDownTime(),strTime,MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SYNCROStatus.tmt_LineStarted.ValueAsUNIVERSALSyncroStyleDateTimeString=%s",strTime);

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_SYNCROStatus page threads");

	return ERROR_SUCCESS;
}

//RFazzi_FI28/09/2006_2.Added Office LineConfig section
DWORD WINAPI C_SyncroSystemGroup::OFFICESettings(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_OfficeSettings._Show()");

	while(!end_OFFICESettings)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_OfficeSettings page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::OFFICEStatus(LPVOID  lpParam)
{	

	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_OfficeStatus._Show()");

	while(!end_OFFICEStatus)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_OfficeStatus page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::PrcSettings(LPVOID  lpParam)
{	

	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_PrcSettings._Show()");

	while(!end_PRCSettings)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_PrcSettings page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::PRINTERSSettings(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_PrintersSettings._Show()");

	while(!end_PRINTERSSettings)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_PrintersSettings page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::PRINTERSStatus(LPVOID  lpParam)
{	
	State::StackerLabelPrintingData stkStatusLabelPrinting[MAX_NUM_LEVELS];

	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_PrintersStatus._Show()");

	while(!end_PRINTERSStatus)
	{
		for (int i=0;i<MAX_NUM_LEVELS;i++)
		{
			C_PageManager::SMState.getStackerLabelPrintingData((KnifeLevel)(i+1),stkStatusLabelPrinting[i]);
		}

		//Send data to the client per stk1
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_SetupIDStk1Value.Value=%d",stkStatusLabelPrinting[0].getSetupId().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingStacksStk1Value.Value=%d",stkStatusLabelPrinting[0].getWaitingStacks().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack1Stk1Value.Value=%d",stkStatusLabelPrinting[0].getWaitingSheets(0).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack2Stk1Value.Value=%d",stkStatusLabelPrinting[0].getWaitingSheets(1).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack3Stk1Value.Value=%d",stkStatusLabelPrinting[0].getWaitingSheets(2).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack4Stk1Value.Value=%d",stkStatusLabelPrinting[0].getWaitingSheets(3).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack5Stk1Value.Value=%d",stkStatusLabelPrinting[0].getWaitingSheets(4).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack6Stk1Value.Value=%d",stkStatusLabelPrinting[0].getWaitingSheets(5).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack7Stk1Value.Value=%d",stkStatusLabelPrinting[0].getWaitingSheets(6).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack8Stk1Value.Value=%d",stkStatusLabelPrinting[0].getWaitingSheets(7).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack9Stk1Value.Value=%d",stkStatusLabelPrinting[0].getWaitingSheets(8).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack10Stk1Value.Value=%d",stkStatusLabelPrinting[0].getWaitingSheets(9).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_TotalDischargedSheetsStk1Value.Value=%d",stkStatusLabelPrinting[0].getTotalDischargedSheet().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_LabelCounterStk1Value.Value=%d",stkStatusLabelPrinting[0].getLabelCounter().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_NrOutStk1Value.Value=%d",stkStatusLabelPrinting[0].getOuts().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.chk_LastDischargeStk1.Checked=%s",String::Bool2Alpha(stkStatusLabelPrinting[0].getLastDischarge().getValue()));
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_StackxPalletStk1Value.Value=%d",stkStatusLabelPrinting[0].getStacksXPallet().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_DischargeCounterStk1Value.Value=%d",stkStatusLabelPrinting[0].getDischargeCounter().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_ProducedSheetsCounterStk1Value.Value=%d",stkStatusLabelPrinting[0].getProducedSheetCounter().getValue());


		//Send data to the client per stk2														 
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_SetupIDStk2Value.Value=%d",stkStatusLabelPrinting[1].getSetupId().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingStacksStk2Value.Value=%d",stkStatusLabelPrinting[1].getWaitingStacks().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack1Stk2Value.Value=%d",stkStatusLabelPrinting[1].getWaitingSheets(0).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack2Stk2Value.Value=%d",stkStatusLabelPrinting[1].getWaitingSheets(1).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack3Stk2Value.Value=%d",stkStatusLabelPrinting[1].getWaitingSheets(2).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack4Stk2Value.Value=%d",stkStatusLabelPrinting[1].getWaitingSheets(3).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack5Stk2Value.Value=%d",stkStatusLabelPrinting[1].getWaitingSheets(4).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack6Stk2Value.Value=%d",stkStatusLabelPrinting[1].getWaitingSheets(5).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack7Stk2Value.Value=%d",stkStatusLabelPrinting[1].getWaitingSheets(6).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack8Stk2Value.Value=%d",stkStatusLabelPrinting[1].getWaitingSheets(7).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack9Stk2Value.Value=%d",stkStatusLabelPrinting[1].getWaitingSheets(8).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack10Stk2Value.Value=%d",stkStatusLabelPrinting[1].getWaitingSheets(9).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_TotalDischargedSheetsStk2Value.Value=%d",stkStatusLabelPrinting[1].getTotalDischargedSheet().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_LabelCounterStk2Value.Value=%d",stkStatusLabelPrinting[1].getLabelCounter().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_NrOutStk2Value.Value=%d",stkStatusLabelPrinting[1].getOuts().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.chk_LastDischargeStk2.Checked=%s",String::Bool2Alpha(stkStatusLabelPrinting[1].getLastDischarge().getValue()));
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_StackxPalletStk2Value.Value=%d",stkStatusLabelPrinting[1].getStacksXPallet().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_DischargeCounterStk2Value.Value=%d",stkStatusLabelPrinting[1].getDischargeCounter().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_ProducedSheetsCounterStk2Value.Value=%d",stkStatusLabelPrinting[1].getProducedSheetCounter().getValue());

		//Send data to the client per stk3
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_SetupIDStk3Value.Value=%d",stkStatusLabelPrinting[2].getSetupId().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingStacksStk3Value.Value=%d",stkStatusLabelPrinting[2].getWaitingStacks().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack1Stk3Value.Value=%d",stkStatusLabelPrinting[2].getWaitingSheets(0).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack2Stk3Value.Value=%d",stkStatusLabelPrinting[2].getWaitingSheets(1).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack3Stk3Value.Value=%d",stkStatusLabelPrinting[2].getWaitingSheets(2).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack4Stk3Value.Value=%d",stkStatusLabelPrinting[2].getWaitingSheets(3).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack5Stk3Value.Value=%d",stkStatusLabelPrinting[2].getWaitingSheets(4).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack6Stk3Value.Value=%d",stkStatusLabelPrinting[2].getWaitingSheets(5).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack7Stk3Value.Value=%d",stkStatusLabelPrinting[2].getWaitingSheets(6).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack8Stk3Value.Value=%d",stkStatusLabelPrinting[2].getWaitingSheets(7).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack9Stk3Value.Value=%d",stkStatusLabelPrinting[2].getWaitingSheets(8).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_WaitingSheetsStack10Stk3Value.Value=%d",stkStatusLabelPrinting[2].getWaitingSheets(9).getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_TotalDischargedSheetsStk3Value.Value=%d",stkStatusLabelPrinting[2].getTotalDischargedSheet().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_LabelCounterStk3Value.Value=%d",stkStatusLabelPrinting[2].getLabelCounter().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_NrOutStk3Value.Value=%d",stkStatusLabelPrinting[2].getOuts().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.chk_LastDischargeStk3.Checked=%s",String::Bool2Alpha(stkStatusLabelPrinting[2].getLastDischarge().getValue()));
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_StackxPalletStk3Value.Value=%d",stkStatusLabelPrinting[2].getStacksXPallet().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_DischargeCounterStk3Value.Value=%d",stkStatusLabelPrinting[2].getDischargeCounter().getValue());
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_PrintersStatus.nud_ProducedSheetsCounterStk3Value.Value=%d",stkStatusLabelPrinting[2].getProducedSheetCounter().getValue());

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_PrintersStatus page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::AOCSettings(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_AOCSettings._Show()");

	while(!end_AOCSettings)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_AOCSettings page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::AOCStatus(LPVOID  lpParam)
{

	//RFAZZI_FI15/10/2009_1.Aggiunta gestione per visualizzazione su grafica dell'ultima sequenza di AOC eseguita

	State::AOCState aocState,lastAOCState;
	State::RSHState rshState;

	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_AOCStatus._Show()");

	while(!end_AOCStatus)
	{
		aocState = C_PageManager::SMState.getAOCState();
		//RFAZZI_FI15/10/2009_1.Aggiunta gestione per visualizzazione su grafica dell'ultima sequenza di AOC eseguita
		lastAOCState = C_PageManager::SMState.getLastAOCState();
		rshState = C_PageManager::SMState.getRSHState();

		//Max AOC Speed
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_MaxAOCSpeedValue.Value=%d",aocState.Speed.getAOCMaxSpeed_UVmm_m() );

		//Min AOC Speed
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_MinAOCSpeedValue.Value=%d",aocState.Speed.getAOCMinSpeed_UVmm_m() );

		//Acceleration Speed																				
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_DryEndAccelerationValue.Value=%d",aocState.Speed.getAOCAccel_URmm_m() );

    //PMASSARO_FI29/08/11 - Visualizzazione livello dominante nelle pagine ingegneristiche
		////Sync Knife
		//o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_SyncKnifeValue.Value=%d",aocState.getAOCSyncKnife());
    o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_SyncLevel.Value=%d",aocState.getAOCSyncKnife());

		////Sync Knife Length																					
		//o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_SyncLengthValue.Value=%d",aocState.getAOCSyncLenght_UDmm());

		////RSH Reaction Distance
		//o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_RSHFireDistanceValue.Value=%d",aocState.getAOCSyncRSHReacDist_UDmm());

		////RSH Reaction Time Estimated
		//o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_RSHReactionTimeEstimatedValue.Value=%d",rshState.monitor.getNextReactionTime_UTms());

		////Last RSH One Cut Reaction Time 
		//o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_RSHLastSingleCutReactionTimeValue.Value=%d",rshState.monitor.getLastOneCutReactionTime_UTms());

		////Last RSH Continuous Cut Reaction Time 
		//o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_RSHLastContinuousCutReactionTimeValue.Value=%d",rshState.monitor.getLastContinuousCutReactionTime_UTms());
		
		//AOC Mode
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_Manual.Checked=%s",String::Bool2Alpha(aocState.isModeManual()));
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_Semi.Checked=%s",String::Bool2Alpha(aocState.isModeSemi()));
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_Auto.Checked=%s",String::Bool2Alpha(aocState.isModeAuto()));

		//AOC Type
		//RFazzi_FI01/06/2007_1.Added some modification for NoGapContinuousTrim
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_NoGapAOC.Checked=%s",String::Bool2Alpha(aocState.Type.getNoGap() || aocState.Type.getNoGapCTrimOneLevel()));
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_NormalAOC.Checked=%s",String::Bool2Alpha(aocState.Type.getNormal()));
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_TandemAOC.Checked=%s",String::Bool2Alpha(aocState.Type.getTandem()));
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_MTS_TER.Checked=%s",String::Bool2Alpha(aocState.Type.getKnifeOnwards()));

		//AOC at EarlyWarning 
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_EarlyWarning.Checked=%s",String::Bool2Alpha(aocState.getIsAOCEarlyWarning()));

		//AOC at PrepareAOC
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_PrepareAOC.Checked=%s",String::Bool2Alpha(aocState.getIsAOCPrepareAOC()));

		//AOC at SlowDown
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_SlowDown.Checked=%s",String::Bool2Alpha(aocState.getIsAOCSlowDown()));

		//AOC WaitMachineReadyAOC
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_WaitingMachinesReady.Checked=%s",String::Bool2Alpha(aocState.getIsAOCWaitingForMachineReady()));

		//AOC WaitSSReadyAOC
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_WaitingSSReady.Checked=%s",String::Bool2Alpha(aocState.getIsAOCWaitingSSConfirmRdy()));

		//DB at AOC Speed
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_DBAtAOCSpeed.Checked=%s",String::Bool2Alpha(aocState.getIsAOCAtAOCSpeed()));

		//AOC RSH Started AOC
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_RSHStartedAOC.Checked=%s",String::Bool2Alpha(aocState.getIsRSHStartedAOC()));

		//AOC in Progress
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_AOCinProgress.Checked=%s",String::Bool2Alpha(aocState.getIsAOCInProgress()));

		//Sequence AOC 
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_SequenceAOC.Checked=%s",String::Bool2Alpha(aocState.getIsSequenceAOC()));

		//Sync Knife Cut Received
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_SyncKnifeCutReceived.Checked=%s",String::Bool2Alpha(aocState.getIsSyncKnifeCutisReceived()));

		//Old Setup at MW
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_OldSetupAtMW.Checked=%s",String::Bool2Alpha(aocState.getIsOldSetupAtMW()));

		//New Setup at MW
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_NewSetupAtMW.Checked=%s",String::Bool2Alpha(aocState.getIsNewSetupAtMW()));

		//Signal per level1

		//AOC. Change at Default
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_ChangeAtDefaultLengthLev1.Checked=%s",String::Bool2Alpha(aocState.getIsChangeAtDefault(KnifeLevel(1))));

		//AOC. Change Now
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_ChangeNowLev1.Checked=%s",String::Bool2Alpha(aocState.getIsChangeNow(KnifeLevel(1))));

		//AOC. Knife Has Changed
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_KnifeHasChangedLev1.Checked=%s",String::Bool2Alpha(aocState.getIsKnifeHasChanged(KnifeLevel(1))));

		//AOC. Knife Last Cut
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_LastCutLev1.Checked=%s",String::Bool2Alpha(aocState.getIsLastCut(KnifeLevel(1))));

		//AOC. Tail Out Cuts Required
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_TailOutCutsRequiredLev1.Value=%d",aocState.getNumberCutsToChange(KnifeLevel(1)));

		//AOC. Default Cuts Required
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_DefaultCutsRequiredLev1.Value=%d",aocState.getNumberDefaultCuts(KnifeLevel(1)));

		//AOC. CutCount
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_TailOutCutsDoneLev1.Value=%d",aocState.getCutCount(KnifeLevel(1)));


		//Signal per level2

		//AOC. Change at Default
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_ChangeAtDefaultLengthLev2.Checked=%s",String::Bool2Alpha(aocState.getIsChangeAtDefault(KnifeLevel(2))));

		//AOC. Change Now
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_ChangeNowLev2.Checked=%s",String::Bool2Alpha(aocState.getIsChangeNow(KnifeLevel(2))));

		//AOC. Knife Has Changed
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_KnifeHasChangedLev2.Checked=%s",String::Bool2Alpha(aocState.getIsKnifeHasChanged(KnifeLevel(2))));

		//AOC. Knife Last Cut
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_LastCutLev2.Checked=%s",String::Bool2Alpha(aocState.getIsLastCut(KnifeLevel(2))));

		//AOC. Tail Out Cuts Required
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_TailOutCutsRequiredLev2.Value=%d",aocState.getNumberCutsToChange(KnifeLevel(2)));

		//AOC. Default Cuts Required
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_DefaultCutsRequiredLev2.Value=%d",aocState.getNumberDefaultCuts(KnifeLevel(2)));

		//AOC. CutCount
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_TailOutCutsDoneLev2.Value=%d",aocState.getCutCount(KnifeLevel(2)));


		//Signal per level3

		//AOC. Change at Default
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_ChangeAtDefaultLengthLev3.Checked=%s",String::Bool2Alpha(aocState.getIsChangeAtDefault(KnifeLevel(3))));

		//AOC. Change Now
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_ChangeNowLev3.Checked=%s",String::Bool2Alpha(aocState.getIsChangeNow(KnifeLevel(3))));

		//AOC. Knife Has Changed
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_KnifeHasChangedLev3.Checked=%s",String::Bool2Alpha(aocState.getIsKnifeHasChanged(KnifeLevel(3))));

		//AOC. Knife Last Cut
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_LastCutLev3.Checked=%s",String::Bool2Alpha(aocState.getIsLastCut(KnifeLevel(3))));

		//AOC. Tail Out Cuts Required
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_TailOutCutsRequiredLev3.Value=%d",aocState.getNumberCutsToChange(KnifeLevel(3)));

		//AOC. Default Cuts Required
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_DefaultCutsRequiredLev3.Value=%d",aocState.getNumberDefaultCuts(KnifeLevel(3)));

		//AOC. CutCount
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_TailOutCutsDoneLev3.Value=%d",aocState.getCutCount(KnifeLevel(3)));



		//RFAZZI_FI15/10/2009_1.Aggiunta gestione per visualizzazione su grafica dell'ultima sequenza di AOC eseguita

		//AOC Speed
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_LastAOCStartAOCSpeed.Value=%d",lastAOCState.getSequenceAOCSpeed_UVm_m() );

		//Acceleration Speed																				
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_LastAOCDryEndAccelerationValue.Value=%d",lastAOCState.Speed.getAOCAccel_URmm_m() );

		//Start AOC to SS from FD/MW
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_LastAOCStartAOCtoSSUsedFD.Checked=%s",String::Bool2Alpha(lastAOCState.isStartAOCSSsentFromFDMW()));

		//Sync Knife
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_DistBetweenFDandEncoderSSAOCPoint.Value=%d",lastAOCState.getDistanceBetweenStartAOCSSbyEncoder_FD());

		//getDistanceBetweenStartAOCSSbyEncoder_FD
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_LastAOCSyncKnifeValue.Value=%d",lastAOCState.getAOCSyncKnife());

		//Sync Knife Length																					
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_LastAOCSyncLengthValue.Value=%d",lastAOCState.getAOCSyncLenght_UDmm());

		//Nr Chop at RSH
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_LatAOCNrChopAtRSH.Value=%d",lastAOCState.getDRYRSHCutsRequired());


		//RSH Reaction Distance
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_LastAOCRSHFireDistanceValue.Value=%d",lastAOCState.getAOCSyncRSHReacDist_UDmm());

		//RSH Reaction Time Estimated
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_LastAOCRSHReactionTimeEstimatedValue.Value=%d",rshState.monitor.getNextReactionTime_UTms());

		//Last RSH One Cut Reaction Time 
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_LastAOCRSHLastSingleCutReactionTimeValue.Value=%d",rshState.monitor.getLastOneCutReactionTime_UTms());

		//Last RSH Continuous Cut Reaction Time 
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_LastAOCRSHLastContinuousCutReactionTimeValue.Value=%d",rshState.monitor.getLastContinuousCutReactionTime_UTms());
		
		//AOC Mode
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_LastAOCManual.Checked=%s",String::Bool2Alpha(lastAOCState.isModeManual()));
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_LastAOCSemi.Checked=%s",String::Bool2Alpha(lastAOCState.isModeSemi()));
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_LastAOCAuto.Checked=%s",String::Bool2Alpha(lastAOCState.isModeAuto()));

		//AOC Type
		//RFazzi_FI01/06/2007_1.Added some modification for NoGapContinuousTrim
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_LastAOCNoGapAOC.Checked=%s",String::Bool2Alpha(lastAOCState.Type.getNoGap() || lastAOCState.Type.getNoGapCTrimOneLevel()));
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_LastAOCNormalAOC.Checked=%s",String::Bool2Alpha(lastAOCState.Type.getNormal()));
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_LastAOCTandemAOC.Checked=%s",String::Bool2Alpha(lastAOCState.Type.getTandem()));
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.chk_LastAOCMTS_TER.Checked=%s",String::Bool2Alpha(lastAOCState.Type.getKnifeOnwards()));



		//Signal per level1

		//AOC. Tail Out Cuts Required
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_LastAOCTailOutCutsRequiredLev1.Value=%d",lastAOCState.getNumberCutsToChange(KnifeLevel(1)));

		//AOC. Default Cuts Required
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_LastAOCDefaultCutsRequiredLev1.Value=%d",lastAOCState.getNumberDefaultCuts(KnifeLevel(1)));


		//Signal per level2

		//AOC. Tail Out Cuts Required
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_LastAOCTailOutCutsRequiredLev2.Value=%d",lastAOCState.getNumberCutsToChange(KnifeLevel(2)));

		//AOC. Default Cuts Required
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_LastAOCDefaultCutsRequiredLev2.Value=%d",lastAOCState.getNumberDefaultCuts(KnifeLevel(2)));


		//Signal per level3

		//AOC. Tail Out Cuts Required
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_LastAOCTailOutCutsRequiredLev3.Value=%d",lastAOCState.getNumberCutsToChange(KnifeLevel(3)));

		//AOC. Default Cuts Required
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_AOCStatus.nud_LastAOCDefaultCutsRequiredLev3.Value=%d",lastAOCState.getNumberDefaultCuts(KnifeLevel(3)));

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_AOCStatus page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::WECSettings(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_WECSettings._Show()");

	while(!end_WECSettings)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_WECSettings page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::WECStatus(LPVOID  lpParam)
{
	State::WECState wecState;
	wchar_t dummyChar[MAX_PATH];

	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_WECStatus._Show()");

	while(!end_WECStatus)
	{
		wecState = C_PageManager::SMState.getWECState();

		//CONTROL

		switch (wecState.Control.getMode())
		{
		case State::WECMODE_NOMODE:
			break;
		case State::WECMODE_MANUAL:
			o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_WECStatus.stb_WECMode.Auto=false");
			break;
		case State::WECMODE_AUTO:
			o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_WECStatus.stb_WECMode.Auto=true");
			break;
		case State::WECMODE_OVERRIDE:
			o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_WECStatus.stb_WECMode.switching=true");
			break;
		default:
			break;
		}

		wecState.Control.getModeToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_WECStatus.tlb_WECModeStr.Value=%s",dummyChar);
		wecState.Control.getSyncModeToStr(dummyChar, MAX_PATH);
		o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_WECStatus.tlb_SyncModeStr.Value=%s",dummyChar);

		//MONITOR
		o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_WECStatus.ttl_LengthProduced.Value=%d",wecState.Monitor.getLengthProduced_UDmm());
		o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_WECStatus.ttl_LengthRemaining.Value=%d",wecState.Monitor.getLengthRemaining_UDmm());
		o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_WECStatus.ttl_LengthTarget.Value=%d",wecState.Monitor.getLengthTarget_UDmm());
	
		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_WECStatus page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::SpeedSettings (LPVOID  lpParam)
{

	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_SpeedSettings._Show()");

	while(!end_SpeedSettings)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_SpeedSettings page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::SpeedStatus(LPVOID  lpParam)
{
	State::SpeedState speedState;
	State::EncoderState encoderState;

  //PMASSARO_FI13/09/11 - Aggiunta la lunghezza di taglio in corso (calcolata da FCT) in Shared Memory
  LineConfig lineConf;

	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_SpeedStatus._Show()");

	while(!end_SpeedStatus)
	{
    //PMASSARO_FI13/09/11 - Aggiunta la lunghezza di taglio in corso (calcolata da FCT) in Shared Memory
    C_PageManager::SMLineConfig.getLineConfig(lineConf);

		speedState = C_PageManager::SMState.getSpeedState();

		encoderState = C_PageManager::SMState.getEncoderState();
		//DB Speed
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_DBSpeed.Value=%d",encoderState.getDBEncoderSpeed_UVmm_m());
		//MW Speed
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_MWSpeed.Value=%d",encoderState.getMWEncoderSpeed_UVmm_m());
		
		//Line Speed
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_LineSpeed.Value=%d",speedState.getSpeed_UVmm_m());

		//Production Speed
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_ProductionSpeed.Value=%d",speedState.getProductionSpeed_UVmm_m());

		//Last AOC Speed
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_LastAOCSpeed.Value=%d",speedState.getSpeedAtSlowDown_UVmm_m());

		//Operator Production Speed
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_OperatorProductionSpeed.Value=%d",speedState.getOperatorProductionSpeed_UVmm_m());

		//Line Run ASR Status
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_LineRunStatusASR.Value=%d",speedState.getLineRunStatusASR());

		//Valid MW Conditions
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.chk_ValidMWConditions.Checked=%s",String::Bool2Alpha(speedState.getValidMWConditions()));

		//agrassi_FI28-07-08
		for (int i=1; i<=MAX_NUM_LEVELS; i++)
			//Exit Belt Speed
			o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_ExitBelt%dSpeed.Value=%d",i, encoderState.getExitBeltEncoderSpeed_UVmm_m(i-1));

    //PMASSARO_FI13/09/11 - Aggiunta la lunghezza di taglio in corso (calcolata da FCT) in Shared Memory
    bool validConditions = (!C_PageManager::SMState.getGeneralState().getWaitingWebAtKnife());
    if (validConditions)
      o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.lbl_NotValidConditions.Visible=False");
    else
      o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.lbl_NotValidConditions.Visible=True");

    for (int i=1; i<=MAX_NUM_LEVELS; i++)
    {
      bool levWork = C_PageManager::SMOrder.DRYGetCurrentSetup().isLevelWorking((KnifeLevel)i);
      if (levWork && validConditions)
      {
        o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.lbl_CutL%d.Visible=True", i);
        o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.lbl_CutLenReqL%d.Visible=True", i);
        o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.lbl_CutLenDiffL%d.Visible=True", i);

        int cutL = C_PageManager::SMState.getAOCState().getCalculatedKnifeCutLength(i);
        o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_CutlengthL%d.Visible=True", i);
        o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_CutlengthL%d.Value=%d", i, cutL);
        
        int cutReqL = C_PageManager::SMOrder.DRYGetCurrentSetup().getCutLenghtLevel_UDdmm((KnifeLevel)i);
        o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_CutlengthReqL%d.Visible=True", i);
        o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_CutlengthReqL%d.Value=%d", i, cutReqL);
        
        int difCutLen = cutL - cutReqL;        
        o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_CutlengthDiff%d.Visible=True", i);
        if ( abs(difCutLen) > lineConf.knife.getCutLengthTolerance_UDdmm() )
          o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_CutlengthDiff%d.ValueForeColor=%s", i, L"Red");
        else
          o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_CutlengthDiff%d.ValueForeColor=%s", i, L"Black");
        o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_CutlengthDiff%d.Value=%d", i, difCutLen);
      }
      else
      {
        o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.lbl_CutL%d.Visible=False", i);
        o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.lbl_CutLenReqL%d.Visible=False", i);
        o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.lbl_CutLenDiffL%d.Visible=False", i);
        o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_CutlengthL%d.Visible=False", i);
        o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_CutlengthReqL%d.Visible=False", i);
        o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_CutlengthDiff%d.Visible=False", i);
      }
    }

		//Speed Command
		switch (speedState.getSpeedCommand())
		{
			case 0:
				o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.lbl_SpeedCommandDescription.Text=%s",L"IDLE");
				break;

			case 3:
				o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.lbl_SpeedCommandDescription.Text=%s",L"SLOWER");
				break;

			case 4:
				o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.lbl_SpeedCommandDescription.Text=%s",L"SLOWER IN BAND");
				break;

			case 1:
				o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.lbl_SpeedCommandDescription.Text=%s",L"FASTER");
				break;

			case 2:
				o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.lbl_SpeedCommandDescription.Text=%s",L"FASTER IN BAND");
				break;
		}


		//Speed Control Reason
		o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_SpeedControlReason.Value=%d",speedState.getSpeedControlReason());

		//Faster Push Button Presseded
		if (speedState.getFasterPushButtonPressed() == -1 )
			o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_FasterPushButtonPressed.Value=%d",0);	
		else
			o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_FasterPushButtonPressed.Value=%d",speedState.getFasterPushButtonPressed()+1);
		
		//Slower Push Button Presseded
		if (speedState.getSlowerPushButtonPressed() == -1 )
			o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_SlowerPushButtonPressed.Value=%d",0);
		else
			o_socket.Send(Network::MSG_PAG_REF,L"syncroSystem_SpeedStatus.nud_SlowerPushButtonPressed.Value=%d",speedState.getSlowerPushButtonPressed()+1);

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_SpeedStatus page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_SyncroSystemGroup::SpeedReferenceSettings (LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_SpeedReferenceSettings._Show()");

	while(!end_SpeedReferenceSettings)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_SpeedReferenceSettings page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::SpeedReferenceStatus(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_SpeedReferenceStatus._Show()");

	while(!end_SpeedReferenceStatus)		
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_SpeedReferenceStatus page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::LOG1(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_Log1._Show()");

	while(!end_LOG1)		
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_Log1 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::LOG2(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_Log2._Show()");

	while(!end_LOG2)		
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_Log2 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::LOG3(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_Log3._Show()");

	while(!end_LOG3)		
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_Log3 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SyncroSystemGroup::LOG4(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"syncroSystem_Log4._Show()");

	while(!end_LOG4)		
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated syncroSystem_Log4 page threads");

	return ERROR_SUCCESS;
}
