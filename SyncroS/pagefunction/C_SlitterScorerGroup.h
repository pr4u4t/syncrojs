#pragma once
//Global Include
#include "..\..\Include\Global.h"

class C_SlitterScorerGroup
{
public:
	C_SlitterScorerGroup(void);
	~C_SlitterScorerGroup(void);
	bool ChooseSlitterScorerPage(String::StrType  thePageRequest);
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
	//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
	static DWORD WINAPI Blades(LPVOID  lpParam);
	static DWORD WINAPI WebDirector(LPVOID  lpParam);
	static DWORD WINAPI Regulations(LPVOID  lpParam);

	static bool end_Status;
	static bool end_Console;
	static bool end_AlarmsActive;
	static bool end_AlarmsHistory;
	static bool end_SettingsMachine;
	static bool end_SettingsTask;
	//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
	static bool end_Blades;
	static bool end_WebDirector;
	static bool end_Regulations;

	HANDLE pid_SlitterScorerPage;
};
