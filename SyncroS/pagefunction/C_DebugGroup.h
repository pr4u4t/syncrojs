#pragma once
//Global Include
#include "..\..\Include\Global.h"

#include "..\..\RTC\C_SMState.h"

class C_DebugGroup
{
public:
	C_DebugGroup(void);
	~C_DebugGroup(void);
	bool ChooseDebugPage(String::StrType  thePageRequest);
	void TerminateThread(const bool terminateThread);
	DWORD SuspendThread(void);
	DWORD ResumeThread(void);

private:
	
	static DWORD WINAPI DebugDatabase(LPVOID  lpParam);
	static DWORD WINAPI DebugINI(LPVOID  lpParam);
	static DWORD WINAPI DebugTwin400Status1(LPVOID  lpParam);
	static DWORD WINAPI DebugTwin400Status2(LPVOID  lpParam);
	static DWORD WINAPI DebugTwin400SetupSituation(LPVOID  lpParam);
	//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
	static DWORD WINAPI DebugTwin400Positioning(LPVOID  lpParam);
  //agrassi_FI25-05-2011_1
	static DWORD WINAPI DebugTwin2(LPVOID  lpParam);

	static bool end_DebugDatabase;
	static bool end_DebugINI;
	static bool end_DebugTwin400Status1;
	static bool end_DebugTwin400Status2;
	static bool end_DebugTwin400SetupSituation;
	//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
	static bool end_DebugTwin400Positioning;
	//agrassi_FI25-05-2011_1
	static bool end_DebugTwin2;

	HANDLE pid_DebugPage; 
};
