#pragma once
//Global Include
#include "..\..\Include\Global.h"

class C_Twin400Group
{
public:
	C_Twin400Group(void);
	~C_Twin400Group(void);
	bool ChooseTwin400Page(String::StrType  thePageRequest);
	void TerminateThread(const bool terminateThread);
	DWORD SuspendThread(void);
	DWORD ResumeThread(void);

private:
	//AGrassi_FI06/12/2007_1
	static char* getTeflonReasonCode(State::SSPlcState* ssPLCState);
	static DWORD WINAPI BladesAndScorers(LPVOID  lpParam);
	static DWORD WINAPI Rotary(LPVOID  lpParam);
	static DWORD WINAPI WebDirector(LPVOID  lpParam);
	static DWORD WINAPI Blades(LPVOID  lpParam);
	static DWORD WINAPI Console(LPVOID  lpParam);
	static DWORD WINAPI OrderDetails(LPVOID  lpParam);
	static DWORD WINAPI OrdersList(LPVOID  lpParam);
	static DWORD WINAPI PastOrders(LPVOID  lpParam);
	static DWORD WINAPI Flutes(LPVOID  lpParam);
	static DWORD WINAPI Quality(LPVOID  lpParam);
	static DWORD WINAPI Faults(LPVOID  lpParam);
	static DWORD WINAPI Tasks(LPVOID  lpParam);
	static DWORD WINAPI Plunges(LPVOID  lpParam);
	static DWORD WINAPI MachineSettings(LPVOID  lpParam);
	static DWORD WINAPI Misc(LPVOID  lpParam);
	static DWORD WINAPI Alarm1(LPVOID  lpParam);
	static DWORD WINAPI Alarm4(LPVOID  lpParam);
  //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
	static DWORD WINAPI Warning1(LPVOID  lpParam);
	static DWORD WINAPI Warning4(LPVOID  lpParam);

	static DWORD WINAPI Maintenance(LPVOID  lpParam);
	static DWORD WINAPI Simulations(LPVOID  lpParam);
	static DWORD WINAPI TestInput(LPVOID  lpParam);
	static DWORD WINAPI TestOutput(LPVOID  lpParam);
	static DWORD WINAPI Overview(LPVOID  lpParam);
	static DWORD WINAPI Positioning(LPVOID  lpParam);
	static DWORD WINAPI Regulations(LPVOID  lpParam);

	static bool end_BladesAndScorers;
	static bool end_Rotary;
	static bool end_WebDirector;
	static bool end_Blades;
	static bool end_Console;
	static bool end_OrderDetails;
	static bool end_OrdersList;
	static bool end_PastOrders;
	static bool end_Flutes;
	static bool end_Quality;
	static bool end_Faults;
	static bool end_Tasks;
	static bool end_Plunges;
	static bool end_MachineSettings;
	static bool end_Misc;
	static bool end_Alarm1;
	static bool end_Alarm4;
  //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
	static bool end_Warning1;
	static bool end_Warning4;
	static bool end_Maintenance;
	static bool end_Simulations;
	static bool end_TestInput;
	static bool end_TestOutput;
	static bool end_Overview;
	static bool end_Positioning;
	static bool end_Regulations;

	HANDLE pid_Twin400Page; //StaBar Thread Pid
};
