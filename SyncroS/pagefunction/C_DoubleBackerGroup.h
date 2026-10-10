#pragma once
//Global Include
#include "..\..\Include\Global.h"

#include "..\..\RTC\C_SMState.h"

class C_DoubleBackerGroup
{
public:
	C_DoubleBackerGroup(void);
	~C_DoubleBackerGroup(void);
	bool ChooseDoubleBackerPage(String::StrType  thePageRequest);
	void TerminateThread(const bool terminateThread);
	DWORD SuspendThread(void);
	DWORD ResumeThread(void);

private:
	
	static DWORD WINAPI Status01(LPVOID  lpParam);
	static DWORD WINAPI Status02(LPVOID  lpParam);
	static DWORD WINAPI Status03(LPVOID  lpParam);
	static DWORD WINAPI Status04(LPVOID  lpParam);
	static DWORD WINAPI Console01(LPVOID  lpParam);
	static DWORD WINAPI Console02(LPVOID  lpParam);
	static DWORD WINAPI Console03(LPVOID  lpParam);
	static DWORD WINAPI Console04(LPVOID  lpParam);
	static DWORD WINAPI AlarmsActive(LPVOID  lpParam);
	static DWORD WINAPI AlarmsHistory(LPVOID  lpParam);
	static DWORD WINAPI SettingsMachine(LPVOID  lpParam);
	static DWORD WINAPI SettingsStatus(LPVOID  lpParam);
	static DWORD WINAPI SettingsTask(LPVOID  lpParam);
	static DWORD WINAPI ProcessControlStatus(LPVOID  lpParam);
	static DWORD WINAPI ProcessControlSettings(LPVOID  lpParam);

	static bool end_Status01;
	static bool end_Status02;
	static bool end_Status03;
	static bool end_Status04;
	static bool end_Console01;
	static bool end_Console02;
	static bool end_Console03;
	static bool end_Console04;
	static bool end_JacksConsole;
	static bool end_AlarmsActive;
	static bool end_AlarmsHistory;
	static bool end_SettingsMachine;
	static bool end_SettingsTask;
	static bool end_SettingsStatus;
	static bool end_ProcessControlStatus;
	static bool end_ProcessControlSettings;

	
	HANDLE pid_DoubleBackerPage; //Double Backer page Thread Pid
};
