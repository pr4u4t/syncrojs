#pragma once
#include "..\..\RTC\C_SMState.h"
//Global Include
#include "..\..\Include\Global.h"

class C_StatisticsGroup
{
public:
	C_StatisticsGroup(void);
	~C_StatisticsGroup(void);
	bool ChooseStatisticsPage(String::StrType  thePageRequest);
	void TerminateThread(const bool terminateThread);
	DWORD SuspendThread(void);
	DWORD ResumeThread(void);

private:
	static DWORD WINAPI ShiftReport(LPVOID  lpParam);
	static DWORD WINAPI DowntimeReport(LPVOID  lpParam);
	static DWORD WINAPI OrdersList(LPVOID  lpParam);
	static DWORD WINAPI OrdersDetail(LPVOID  lpParam);
	static DWORD WINAPI PastRollsList(LPVOID  lpParam);
  static DWORD WINAPI PastRollsTrace(LPVOID  lpParam);
  static DWORD WINAPI ProductionTrace(LPVOID  lpParam);

	static bool end_ShiftReport;
	static bool end_DowntimeReport;
	static bool end_OrdersList;
	static bool end_OrdersDetail;
	static bool end_PastRollsList;
  static bool end_PastRollsTrace;
  static bool end_ProductionTrace;

	HANDLE pid_StatisticsPage;
};
