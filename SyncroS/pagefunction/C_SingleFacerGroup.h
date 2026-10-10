#pragma once
//Global Include
#include "..\..\Include\Global.h"

class C_SingleFacerGroup
{
public:
	C_SingleFacerGroup(void);
	~C_SingleFacerGroup(void);
	bool ChooseSingleFacerPage(String::StrType  thePageRequest, const int index);
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
	static DWORD WINAPI AlarmActive(LPVOID  lpParam);
	static DWORD WINAPI AlarmHistory(LPVOID  lpParam);
	static DWORD WINAPI SettingsMachine(LPVOID  lpParam);
	static DWORD WINAPI SettingsTask(LPVOID  lpParam);
	static DWORD WINAPI SettingsSFStatus(LPVOID  lpParam);
	static DWORD WINAPI SettingsBridgeStatus(LPVOID  lpParam);
	static DWORD WINAPI ProcessControlStatus(LPVOID  lpParam);
	static DWORD WINAPI ProcessControlSettings(LPVOID  lpParam);

	static int k; //parameter passed to page thread 	

	static bool end_Status01;
	static bool end_Status02;
	static bool end_Status03;
	static bool end_Status04;
	static bool end_Console01;
	static bool end_Console02;
	static bool end_Console03;
	static bool end_Console04;
	static bool end_AlarmsActive;
	static bool end_AlarmsHistory;
	static bool end_SettingsMachine;
	static bool end_SettingsTask;
	static bool end_SettingsSFStatus;
	static bool end_SettingsBridgeStatus;
	static bool end_ProcessControlStatus;
	static bool end_ProcessControlSettings;

	HANDLE pid_SingleFacerPage; //StaBar Thread Pid
};
