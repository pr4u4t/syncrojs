#pragma once
//Global Include
#include "..\..\Include\Global.h"

class C_SplicerGroup
{
public:
	C_SplicerGroup(void);
	~C_SplicerGroup(void);
	bool ChooseSplicerPage(String::StrType  thePageRequest, const int index);
	void TerminateThread(const bool terminateThread);
	DWORD SuspendThread(void);
	DWORD ResumeThread(void);

private:
	static DWORD WINAPI Status(LPVOID  lpParam);
	static DWORD WINAPI Console(LPVOID  lpParam);
	static DWORD WINAPI AlarmActive(LPVOID  lpParam);
	static DWORD WINAPI AlarmHistory(LPVOID  lpParam);
	static DWORD WINAPI SettingsMachine(LPVOID  lpParam);
	static DWORD WINAPI SettingsTask(LPVOID  lpParam);
	static DWORD WINAPI SettingsStatus(LPVOID  lpParam);
	static DWORD WINAPI ProcessControlStatus(LPVOID  lpParam);
	static DWORD WINAPI ProcessControlSettings(LPVOID  lpParam);

	static int k; //splicer number passed to page thread 	

	static bool end_Status;
	static bool end_Console;
	static bool end_AlarmsActive;
	static bool end_AlarmsHistory;
	static bool end_SettingsMachine;
	static bool end_SettingsTask;
	static bool end_SettingsStatus;
	static bool end_ProcessControlStatus;
	static bool end_ProcessControlSettings;

	HANDLE pid_SplicerPage;
};
