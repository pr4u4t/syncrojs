#pragma once

//Global Include
#include "..\Include\Global.h"

//Graphics Messages
#include "..\Include\Messages\T_SyncroSCodes.h"

//DBMS
#include "..\DBMS\C_SMOrder.h"

//Line Config
#include "..\Shared\C_SMLineConfig.h"

//StatusBar group
#include ".\pagefunction\C_StatusBar.h"
//LaterlaBar group
#include ".\pagefunction\C_LateralBar.h"
//Production group
#include ".\pagefunction\C_ProductionGroup.h"
//Statistics group
#include ".\pagefunction\C_StatisticsGroup.h"
//Statistics group
#include ".\pagefunction\C_SyncroSystemGroup.h"
//Settings group
#include ".\pagefunction\C_SettingsGroup.h"
//SingleFacer group
#include ".\pagefunction\C_SingleFacerGroup.h"
//Splicer group
#include ".\pagefunction\C_SplicerGroup.h"
//Stacker group
#include ".\pagefunction\C_StackerGroup.h"
//Shear group
#include ".\pagefunction\C_ShearGroup.h"
//SlitterScorer group
#include ".\pagefunction\C_SlitterScorerGroup.h"
//Knife group
#include ".\pagefunction\C_KnifeGroup.h"
//Twin400 group
#include ".\pagefunction\C_Twin400Group.h"
//SlitterScorer group
#include ".\pagefunction\C_SlitterScorerGroup.h"
//DoubleBacker group
#include ".\pagefunction\C_DoubleBackerGroup.h"
//Pre-heater tern group
#include ".\pagefunction\C_PreHeaterTernGroup.h"
//GlueUnit group
#include ".\pagefunction\C_GlueUnitGroup.h"
//Debug group
#include ".\pagefunction\C_DebugGroup.h"
//DryEnd Supervisor group
#include ".\pagefunction\C_DryEndSupervisorGroup.h"
//WetEnd Supervisor group
#include ".\pagefunction\C_WetEndSupervisorGroup.h"
#include ".\machinecommands\C_Twin400Commands.h"
#include ".\machinecommands\C_SlitterScorerCommands.h"
#include "..\Shared\C_SMPrc.h"

class C_PageManager
{
public:
	C_PageManager(void); //costructor
	~C_PageManager(void); //destructor
	bool ManagePage(String::StrType page); //execute the command
	DWORD CreateGlobalUpdate(void); //create the GlobalUpdate thread
	DWORD StopPage(void); //stop managaement page
	DWORD ResumePage(void); //resume management page
	void ClosePageManager(void); //terminate all thread page, statusbar, GlobalUpdate
	DWORD CreateSM(void); //create the shared memory

private:
	HANDLE pid_GlobalUpdate; //GlobalUpdate Thread Pid
	static bool stopGlobalUpdate;
	//the friendly class
	friend C_Twin400Group;
	friend C_StatusBar;
	friend C_DoubleBackerGroup;
	friend C_KnifeGroup;
	friend C_StackerGroup;
	friend C_PreHeaterTernGroup;
	friend C_GlueUnitGroup;
	friend C_DebugGroup;
	friend C_ProductionGroup;
	friend C_StatisticsGroup;
	friend C_SettingsGroup;
	friend C_SyncroSystemGroup;
	friend C_Twin400Commands;
	friend C_SlitterScorerGroup;
	friend C_SingleFacerGroup;
	friend C_SplicerGroup;
	friend C_DryEndSupervisorGroup;
	friend C_WetEndSupervisorGroup;
	friend C_SlitterScorerCommands;
  friend C_LateralBar;

	//the page management class
	C_StatusBar statusBar;
  C_LateralBar lateralBar;
	C_ProductionGroup productionGroup;
	C_StatisticsGroup statisticsGroup;
	C_SettingsGroup settingsGroup;
	C_SyncroSystemGroup syncroSystemGroup;
	C_SingleFacerGroup singleFacerGroup;
	C_SplicerGroup splicerGroup;
	C_ShearGroup shearGroup;
	C_SlitterScorerGroup slitterScorerGroup;
	C_KnifeGroup knifeGroup;
	C_StackerGroup stackerGroup;
	C_Twin400Group twin400Group;
	C_DoubleBackerGroup doubleBackerGroup;
	C_GlueUnitGroup glueUnitGroup;
	C_PreHeaterTernGroup preHeaterTernGroup;
	C_DebugGroup debugGroup;
	C_WetEndSupervisorGroup wetEndSupervisorGroup;
	C_DryEndSupervisorGroup dryEndSupervisorGroup;

	static C_SMLineConfig SMLineConfig;
	static State::C_SMState SMState; //State Shared memory
	static DataBase::C_SMOrder SMOrder; //Order Shared memory
	static ProcessControl::C_SMPrc SMProcess;				//State of the Process Control
	static void SetTime(void); //Update client date and time
  static void SetTimeZone(void); //Update client Zone Time
	static DWORD WINAPI GlobalUpdate(LPVOID  lpParam); //GlobalUpdate function
	void TerminatePageThread(const bool terminateThread); //Terminate all page management thread
};
