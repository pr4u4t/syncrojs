#pragma once
#include "..\..\RTC\C_SMState.h"
//Global Include
#include "..\..\Include\Global.h"

class C_LateralBar
{
public:
	C_LateralBar(void);
	~C_LateralBar(void);
	bool ChooseLateralBar(String::StrType  theLateralBar);
	void TerminateThread(const bool terminateThread);
	DWORD SuspendThread(void);
	DWORD ResumeThread(void);

private:
	static DWORD WINAPI LateralBar(LPVOID lpParam);
  static void LateralBar_UpdateTab(int lateralBarNumber, int tab_lateralBarNumber);

	static bool end_LateralBar;
  static int tab_lateralBar_1;
  static int tab_lateralBar_2;
  static int tab_lateralBar_3;

	HANDLE pid_Lateral_bar; //Lateral Bar Thread Pid
};
