#pragma once
#include "..\..\RTC\C_SMState.h"
//Global Include
#include "..\..\Include\Global.h"

class C_StatusBar
{
public:
	C_StatusBar(void);
	~C_StatusBar(void);
	bool ChooseStaBar(String::StrType  theStaBar);
	void TerminateThread(const bool terminateThread);
	DWORD SuspendThread(void);
	DWORD ResumeThread(void);

private:
	static DWORD WINAPI DryEnd(LPVOID lpParam);
	static DWORD WINAPI WetEnd(LPVOID lpParam);
	static DWORD WINAPI Twin400(LPVOID lpParam);

	static bool end_DryEnd;
	static bool end_WetEnd;
	static bool end_Twin400;

	HANDLE pid_Sta_bar; //StaBar Thread Pid
};
