#pragma once
//Global Include
#include "..\..\Include\Global.h"

class C_OldFosberSlitterScorerGroup
{
public:
	C_OldFosberSlitterScorerGroup(void);
	~C_OldFosberSlitterScorerGroup(void);
	bool ChooseOldFosberSlitterScorerPage(String::StrType  thePageRequest);
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

	static bool end_Status;
	static bool end_Console;
	static bool end_AlarmsActive;
	static bool end_AlarmsHistory;
	static bool end_SettingsMachine;
	static bool end_SettingsTask;

	HANDLE pid_OldFosberSlitterScorerPage;
};
