#pragma once
//Global Include
#include "..\..\Include\Global.h"

class C_SettingsGroup
{
public:
	C_SettingsGroup(void);
	~C_SettingsGroup(void);
	bool ChooseSettingsPage(String::StrType  thePageRequest);
	void TerminateThread(const bool terminateThread);
	DWORD SuspendThread(void);
	DWORD ResumeThread(void);
	void SetDIOModule(const int theDSPModule);
	void SetCTCModule(const int theDSPModule);

private:
	static DWORD WINAPI Flute(LPVOID  lpParam);
	static DWORD WINAPI Languages(LPVOID  lpParam);
	static DWORD WINAPI LineConfig(LPVOID  lpParam);
	static DWORD WINAPI DownTimeReasons(LPVOID  lpParam);
	static DWORD WINAPI DIOStatus(LPVOID  lpParam);
	static DWORD WINAPI CTCStatus(LPVOID  lpParam);
	static DWORD WINAPI AIOStatus(LPVOID  lpParam);
	static DWORD WINAPI SimulationGeneral(LPVOID  lpParam);
	static DWORD WINAPI Operations(LPVOID  lpParam);
	static DWORD WINAPI RestoreBackup(LPVOID  lpParam);

	static bool end_Flute;
	static bool end_Languages;
	static bool end_LineConfig;
	static bool end_DownTimeReasons;
	static bool end_DIOStatus;
	static bool end_CTCStatus;
	static bool end_AIOStatus;
	static bool end_SimulationGeneral;
	static bool end_Operations;
  static bool end_RestoreBackup;

	HANDLE pid_SettingsPage; //StaBar Thread Pid

	static int DSPModule;
};
