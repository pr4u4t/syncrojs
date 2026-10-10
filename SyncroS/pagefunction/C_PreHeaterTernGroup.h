#pragma once
//Global Include
#include "..\..\Include\Global.h"

#include "..\..\RTC\C_SMState.h"

class C_PreHeaterTernGroup
{
public:
	C_PreHeaterTernGroup(void);
	~C_PreHeaterTernGroup(void);
	bool ChoosePreHeaterTernPage(String::StrType  thePageRequest);
	void TerminateThread(const bool terminateThread);
	DWORD SuspendThread(void);
	DWORD ResumeThread(void);

private:
	
	static DWORD WINAPI Status(LPVOID  lpParam);
	static DWORD WINAPI Console(LPVOID  lpParam);
	static DWORD WINAPI AlarmsActive(LPVOID  lpParam);
	static DWORD WINAPI AlarmsHistory(LPVOID  lpParam);
	static DWORD WINAPI SettingsMachine(LPVOID  lpParam);
	static DWORD WINAPI SettingsTask(LPVOID  lpParam);
	static DWORD WINAPI ProcessControlStatus(LPVOID  lpParam);
	static DWORD WINAPI ProcessControlSettings(LPVOID  lpParam);

	static bool end_Status;
	static bool end_Console;
	static bool end_AlarmsActive;
	static bool end_AlarmsHistory;
	static bool end_SettingsMachine;
	static bool end_SettingsTask;
	static bool end_ProcessControlStatus;
	static bool end_ProcessControlSettings;

	HANDLE pid_PreHeaterTernPage; 
};
