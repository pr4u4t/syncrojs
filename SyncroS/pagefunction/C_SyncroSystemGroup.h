#pragma once
//Global Include
#include "..\..\Include\Global.h"

#include "..\..\RTC\C_SMState.h"

class C_SyncroSystemGroup
{
public:
	C_SyncroSystemGroup(void);
	~C_SyncroSystemGroup(void);
	bool ChooseSyncroSystemPage(String::StrType thePageRequest);
	void TerminateThread(const bool terminateThread);
	DWORD SuspendThread(void);
	DWORD ResumeThread(void);

private:
	static DWORD WINAPI SYNCROSettings(LPVOID  lpParam);
	static DWORD WINAPI SYNCROStatus(LPVOID  lpParam);
	static DWORD WINAPI SYNCROTask1(LPVOID  lpParam);
	static DWORD WINAPI SYNCROTask2(LPVOID  lpParam);
	static DWORD WINAPI AOCSettings(LPVOID  lpParam);
	static DWORD WINAPI AOCStatus(LPVOID  lpParam);
	static DWORD WINAPI WECSettings(LPVOID  lpParam);
	static DWORD WINAPI WECStatus(LPVOID  lpParam);
	static DWORD WINAPI SpeedSettings(LPVOID  lpParam);
	static DWORD WINAPI SpeedStatus(LPVOID  lpParam);
	static DWORD WINAPI SpeedReferenceSettings(LPVOID  lpParam);
	static DWORD WINAPI SpeedReferenceStatus(LPVOID  lpParam);
	static DWORD WINAPI PRINTERSSettings(LPVOID  lpParam);
	static DWORD WINAPI PRINTERSStatus(LPVOID  lpParam);
	//RFazzi_FI28/09/2006_2.Added Office LineConfig section
	static DWORD WINAPI OFFICESettings(LPVOID  lpParam);
	static DWORD WINAPI OFFICEStatus(LPVOID  lpParam);
	static DWORD WINAPI PrcSettings(LPVOID  lpParam);
	static DWORD WINAPI LOG1(LPVOID  lpParam);
	static DWORD WINAPI LOG2(LPVOID  lpParam);
	static DWORD WINAPI LOG3(LPVOID  lpParam);
	static DWORD WINAPI LOG4(LPVOID  lpParam);
	
	static bool end_SYNCROSettings;
	static bool end_SYNCROStatus;
	static bool end_SYNCROTask1;
	static bool end_SYNCROTask2;
	static bool end_AOCSettings;
	static bool end_AOCStatus;
	static bool end_WECSettings;
	static bool end_WECStatus;
	static bool end_SpeedSettings;
	static bool end_SpeedStatus;
	static bool end_SpeedReferenceSettings;
	static bool end_SpeedReferenceStatus;
	static bool end_PRINTERSSettings;
	static bool end_PRINTERSStatus;
	//RFazzi_FI28/09/2006_2.Added Office LineConfig section
	static bool end_OFFICESettings;
	static bool end_OFFICEStatus;
	static bool end_PRCSettings;
	static bool end_LOG1;
	static bool end_LOG2;
	static bool end_LOG3;
	static bool end_LOG4;

	HANDLE pid_SyncroSystemPage; 
};
