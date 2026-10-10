#include ".\C_StatisticsGroup.h"

#include "..\C_PageManager.h"
//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

bool C_StatisticsGroup::end_ShiftReport;
bool C_StatisticsGroup::end_DowntimeReport;
bool C_StatisticsGroup::end_OrdersList;
bool C_StatisticsGroup::end_OrdersDetail;
bool C_StatisticsGroup::end_PastRollsList;
bool C_StatisticsGroup::end_PastRollsTrace;
bool C_StatisticsGroup::end_ProductionTrace;

C_StatisticsGroup::C_StatisticsGroup(void)
{
	pid_StatisticsPage=NULL;
	TerminateThread(false);
}

C_StatisticsGroup::~C_StatisticsGroup(void)
{
}

bool C_StatisticsGroup::ChooseStatisticsPage(String::StrType  thePageRequest)
{
	if(thePageRequest.CompareNoCase(L"shiftReport"))
	{
		end_ShiftReport = false;
		pid_StatisticsPage=::CreateThread(NULL,0,ShiftReport,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"downtimeReport"))
	{
		end_DowntimeReport = false;
		pid_StatisticsPage=::CreateThread(NULL,0,DowntimeReport,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"ordersList"))
	{
		end_OrdersList = false;
		pid_StatisticsPage=::CreateThread(NULL,0,OrdersList,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"pastRollsList"))
	{
		end_PastRollsList = false;
		pid_StatisticsPage=::CreateThread(NULL,0,PastRollsList,NULL,0,NULL);
		return true;
	}
  if(thePageRequest.CompareNoCase(L"pastRollsTrace"))
	{
		end_PastRollsTrace = false;
		pid_StatisticsPage=::CreateThread(NULL,0,PastRollsTrace,NULL,0,NULL);
		return true;
	}
	if(thePageRequest.CompareNoCase(L"ordersDetail"))
	{
		end_OrdersDetail = false;
		pid_StatisticsPage=::CreateThread(NULL,0,OrdersDetail,NULL,0,NULL);
		return true;
	}
  if(thePageRequest.CompareNoCase(L"productionTrace"))
	{
		end_ProductionTrace = false;
		pid_StatisticsPage=::CreateThread(NULL,0,ProductionTrace,NULL,0,NULL);
		return true;
	}
	return false;
}

void C_StatisticsGroup::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_StatisticsPage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_StatisticsPage,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread StatisticsPage ... %d", ::GetLastError());
		}
	}
	else
	{
		end_ShiftReport = true;
		end_DowntimeReport = true;
		end_OrdersList = true;
		end_OrdersDetail = true;
		end_PastRollsList = true;
    end_PastRollsTrace = true;
    end_ProductionTrace = true;
	}
}

DWORD C_StatisticsGroup::SuspendThread(void)
{
	if(pid_StatisticsPage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_StatisticsPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page Statistics Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_StatisticsGroup::ResumeThread(void)
{
	if(pid_StatisticsPage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_StatisticsPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page Statistics Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}

DWORD WINAPI C_StatisticsGroup::ShiftReport(LPVOID  lpParam)
{
	ShiftData shiftData;
	bool oldStopShift = C_PageManager::SMOrder.SHIFTGetCurrentShift().isStopShift();
	bool firstLoop = true;

	o_socket.Send(Network::MSG_PAG_REF,L"statistics_shiftReport._Show()");
	
	while(!end_ShiftReport)
	{
		shiftData = C_PageManager::SMOrder.SHIFTGetCurrentShift();
		
		//PMASSARO_FI/24/11/08 - Check Time Since Last System Restart	
		int tc = GetTickCount();
		o_socket.Send(Network::MSG_PAG_REF,L"statistics_shiftReport._UpdateLastRestartTime(i[%d])", tc);	   

		if(shiftData.isStopShift())
		{
			if (oldStopShift == false || firstLoop) 
			{
				oldStopShift = true;
				o_socket.Send(Network::MSG_PAG_REF,L"statistics_shiftReport._IdleShift(b[true])");
			}
		}
		else
		{
			if (oldStopShift == true || firstLoop) 
			{
				oldStopShift = false;
				o_socket.Send(Network::MSG_PAG_REF,L"statistics_shiftReport._IdleShift(b[false])");
			}
			
			wchar_t TimeStrStart[MAX_PATH];
			wchar_t TimeStrFinish[MAX_PATH];
			
			String::SYSTEMTIME2Alpha(shiftData.getStartTime(),TimeStrStart,MAX_PATH);
			String::SYSTEMTIME2Alpha(shiftData.getFinishTime(),TimeStrFinish,MAX_PATH);
			
			o_socket.Send(Network::MSG_PAG_REF,
				L"statistics_shiftReport._UpdateCurrent(s[%c%s%s%s%s%s%d%s%d%s%d%s%d%s%s%s%d%s%d])",
				//										   0 S 1 S 2 S 3 S 4 S 5 S 6 S 7 S 8 S 9
			shiftData.getShiftId(), Network::SEP,//0 ID
			TimeStrStart, Network::SEP,//1 Start Time
			TimeStrFinish, Network::SEP,//2 Finish Time
			shiftData.getRunTime_UTs(), Network::SEP,//3 RunTime_UTs
			shiftData.getStopTime_UTs(), Network::SEP,//4 StopTime
			shiftData.getNumSetupChanges(), Network::SEP,//5 SetupChanges
			shiftData.getNumQualityChanges(), Network::SEP,//6 QualityChanges
			shiftData.getShiftManagerName(), Network::SEP,//7 shiftManagerName
			shiftData.getNumberOfPeople(), Network::SEP,//8 NumberOfPeople
			shiftData.getAvgSpeed_UVm_m());//9 Avg speed
			
			for (int i = 0; i < MAX_FLUTE_PER_SHIFT; i++)
			{
				if (shiftData.getFlute(i).fluteShift.isEmptyFluteID())
				{//no more flutes worked
					break;
				}
				else
				{
					o_socket.Send(Network::MSG_PAG_REF,
					L"statistics_shiftReport._UpdateCurrentFlute(s[%d%s%s%s%d%s%d%s%d%s%d])",
																												//0 S 1 S 2 S 3 S 4 S 5 
					i, Network::SEP,//0 index of the flute
					shiftData.getFlute(i).fluteShift.getFluteId(), Network::SEP,//1 FluteText
					shiftData.getFlute(i).getMetersProducedSS_UDm(), Network::SEP,//2 
					shiftData.getFlute(i).getScrapRSH_UDm(), Network::SEP,//3 
					//PMASSARO_FI13/01/09 - Must Show The Good Meters
					/*                    
					//PMASSARO_FI04/09/08 - Bug Fix And Report Review
						//shiftData.getFlute(i).getMetersProducedSS_UAm(), Network::SEP,//4 
					//shiftData.getFlute(i).getMetersProducedSS_UAm() - shiftData.getFlute(i).getMetersBad_UAm(), Network::SEP,//4 
					*/
					(int)shiftData.getFlute(i).getMetersProducedOnlyGoodSheets_UAm(), Network::SEP,//4 
					shiftData.getFlute(i).getMetersBad_UAm());//5 
				}
			} 
		}
		firstLoop = false;
		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated statistics_shiftReport page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_StatisticsGroup::DowntimeReport(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"statistics_downtimeReport._Show()");
	
	while(!end_DowntimeReport)
	{
		Sleep(1000);
	};
	
	logger.Fine(__FUNCTION__, L"Terminated statistics_downtimeReport page threads");

	return ERROR_SUCCESS;
}
	
DWORD WINAPI C_StatisticsGroup::OrdersList(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"statistics_ordersList._Show()");

	while(!end_OrdersList)
		Sleep(1000);
	
	logger.Fine(__FUNCTION__, L"Terminated statistics_ordersList page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_StatisticsGroup::PastRollsList(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"statistics_pastRollsList._Show()");

	while(!end_PastRollsList)
		Sleep(1000);
	
	logger.Fine(__FUNCTION__, L"Terminated statistics_pastRollsList page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_StatisticsGroup::PastRollsTrace(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"statistics_pastRollsTrace._Show()");

	while(!end_PastRollsTrace)
		Sleep(1000);
	
	logger.Fine(__FUNCTION__, L"Terminated statistics_pastRollsTrace page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_StatisticsGroup::OrdersDetail(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"statistics_OrdersDetail._Show()");

	while(!end_OrdersDetail)
		Sleep(1000);
	
	logger.Fine(__FUNCTION__, L"Terminated statistics_OrdersDetail page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_StatisticsGroup::ProductionTrace(LPVOID  lpParam)
{
  ShiftData shiftData = C_PageManager::SMOrder.SHIFTGetCurrentShift();
  wchar_t TimeStrStart[MAX_PATH];
  String::SYSTEMTIME2Alpha(shiftData.getStartTime(),TimeStrStart,MAX_PATH);

	o_socket.Send(Network::MSG_PAG_REF,L"statistics_productionTrace._Show(b[%s],s[%c],s[%s],s[%s])",
        String::Bool2Alpha(shiftData.isStopShift()),
			  shiftData.getShiftId(),
			  TimeStrStart,
        shiftData.getShiftManagerName());

	while(!end_ProductionTrace)	
  {
    shiftData = C_PageManager::SMOrder.SHIFTGetCurrentShift();

    String::SYSTEMTIME2Alpha(shiftData.getStartTime(),TimeStrStart,MAX_PATH);

    o_socket.Send(Network::MSG_PAG_REF, L"statistics_productionTrace._UpdateCurrentShift(b[%s],s[%s],s[%c],s[%s])",
        String::Bool2Alpha(shiftData.isStopShift()),
        TimeStrStart,
			  shiftData.getShiftId(),
        shiftData.getShiftManagerName());

    Sleep(1000);
  }
	
	logger.Fine(__FUNCTION__, L"Terminated statistics_productionTrace page threads");

	return ERROR_SUCCESS;
}

