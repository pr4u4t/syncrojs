#pragma once
//Global Include
#include "..\..\Include\Global.h"

class C_DryEndSupervisorGroup
{
public:
	C_DryEndSupervisorGroup(void);
	~C_DryEndSupervisorGroup(void);
	bool ChooseDryEndSupervisorGroupPage(String::StrType thePageRequest);
	void TerminateThread(const bool terminateThread);
	DWORD SuspendThread(void);
	DWORD ResumeThread(void);

private:
	static DWORD WINAPI Page01(LPVOID  lpParam);
	static DWORD WINAPI Page02(LPVOID  lpParam);
	static DWORD WINAPI Page03(LPVOID  lpParam);
	static DWORD WINAPI Settings(LPVOID  lpParam);

	static bool end_Page01;
	static bool end_Page02;
	static bool end_Page03;
	static bool end_Settings;
	
	HANDLE pid_DryEndSupervisorPage; 
};
