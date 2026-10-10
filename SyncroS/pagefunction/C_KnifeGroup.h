#pragma once
//Global Include
#include "..\..\Include\Global.h"

class C_KnifeGroup
{
public:
	C_KnifeGroup(void);
	~C_KnifeGroup(void);
	bool ChooseKnifePage(String::StrType thePageRequest);
	void TerminateThread(const bool terminateThread);
	DWORD SuspendThread(void);
	DWORD ResumeThread(void);

private:
	static DWORD WINAPI Status(LPVOID  lpParam);
  static DWORD WINAPI Pattern(LPVOID  lpParam);
	static DWORD WINAPI Console(LPVOID  lpParam);
	static DWORD WINAPI AlarmsActive(LPVOID  lpParam);
	static DWORD WINAPI AlarmsHistory(LPVOID  lpParam);
	static DWORD WINAPI SettingsMachine(LPVOID  lpParam);
	static DWORD WINAPI SettingsTask(LPVOID  lpParam);

	static bool end_Status;
	static bool end_Console;
  static bool end_Pattern;
	static bool end_AlarmsActive;
	static bool end_AlarmsHistory;
	static bool end_SettingsMachine;
	static bool end_SettingsTask;
	
	HANDLE pid_KnifePage; 
};
