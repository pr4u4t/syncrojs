#pragma once
//Global Include
#include "..\..\Include\Global.h"

#include "..\..\RTC\C_SMState.h"

class C_ProductionGroup
{
public:
	C_ProductionGroup(void);
	~C_ProductionGroup(void);
	bool ChooseProductionPage(String::StrType thePageRequest);
	void TerminateThread(const bool terminateThread);
	DWORD SuspendThread(void);
	DWORD ResumeThread(void);

private:
  //agrassi_FI23-03-2011_3
  //static bool C_ProductionGroup::limitateAocSpeedCauseOfShortOrder(int &optimalSpeed, int currAOCMaxSpeed); //PMASSARO_FI18/10/10 - Aggiunta Gestione Per Limitare La Velocità Di Linea In Base Alla Lunghezza Dell'Ordine
	static DWORD WINAPI HomeDry(LPVOID  lpParam);
	static DWORD WINAPI HomeDryWet(LPVOID  lpParam);
	static DWORD WINAPI HomeWet(LPVOID  lpParam);
	static DWORD WINAPI HomePrc(LPVOID  lpParam);
	static DWORD WINAPI FullInsDryOrder(LPVOID lpParam);
	static DWORD WINAPI InsWetOrder(LPVOID lpParam);
	static DWORD WINAPI DryPastOrder(LPVOID lpParam);
	static DWORD WINAPI QuickInsDryOrder(LPVOID lpParam);
	static DWORD WINAPI DryOrdersList(LPVOID lpParam);
	static DWORD WINAPI DryPastOrdersList(LPVOID lpParam);
	static DWORD WINAPI DryWetOrdersList(LPVOID lpParam);
	static DWORD WINAPI WetOrdersList(LPVOID lpParam);
	static DWORD WINAPI Paper(LPVOID  lpParam);
	static DWORD WINAPI Alarm1(LPVOID  lpParam);
	static DWORD WINAPI Alarm2(LPVOID  lpParam);
	static DWORD WINAPI Quality(LPVOID  lpParam);
	static DWORD WINAPI Operator(LPVOID  lpParam);
	static DWORD WINAPI Printings(LPVOID  lpParam);
  static DWORD WINAPI MessagePRC(LPVOID  lpParam);
  //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
	static DWORD WINAPI Warning1(LPVOID  lpParam);
	static DWORD WINAPI Warning2(LPVOID  lpParam);

	static bool end_HomeDry;
	static bool end_HomeDryWet;
	static bool end_HomeWet;
	static bool end_HomePrc;
	static bool end_FullInsDryOrder;
	static bool end_InsWetOrder;
	static bool end_DryPastOrder;
	static bool end_QuickInsDryOrder;
	static bool end_DryOrdersList;
	static bool end_DryPastOrdersList;
	static bool end_DryWetOrdersList;
	static bool end_WetOrdersList;
	static bool end_Paper;
	static bool end_Alarm1;
	static bool end_Alarm2;
	static bool end_Quality;
	static bool end_Operator;
	static bool end_Printings;
  static bool end_MessagePRC;
  //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
	static bool end_Warning1;
	static bool end_Warning2;
	HANDLE pid_ProductionPage; //StaBar Thread Pid
};
