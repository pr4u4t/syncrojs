#pragma once
//Global Include
#include "..\..\Include\Global.h"

class C_StackerGroup
{
public:
	C_StackerGroup(void);
	~C_StackerGroup(void);
	bool ChooseStackerPage(String::StrType  thePageRequest, const int index);
	void TerminateThread(const bool terminateThread);
	DWORD SuspendThread(void);
	DWORD ResumeThread(void);

private:
	static DWORD WINAPI Status01(LPVOID  lpParam);
  static DWORD WINAPI Status02(LPVOID  lpParam);
  static DWORD WINAPI Status03(LPVOID  lpParam);
  static DWORD WINAPI Status04(LPVOID  lpParam);
	static DWORD WINAPI Console(LPVOID  lpParam);
	static DWORD WINAPI AlarmActive(LPVOID  lpParam);
	static DWORD WINAPI AlarmHistory(LPVOID  lpParam);
	static DWORD WINAPI SettingsMachine(LPVOID  lpParam);
	static DWORD WINAPI SettingsTask(LPVOID  lpParam);
	static DWORD WINAPI SettingsStatus(LPVOID  lpParam);

	static bool end_Status01;
  static bool end_Status02;
  static bool end_Status03;
  static bool end_Status04;
	static bool end_Console;
	static bool end_AlarmsActive;
	static bool end_AlarmsHistory;
	static bool end_SettingsMachine;
	static bool end_SettingsTask;
	static bool end_SettingsStatus;

	HANDLE pid_StackerPage; //Stacker Thread Pid
	static int k; //parameter passed to page thread 	
};
