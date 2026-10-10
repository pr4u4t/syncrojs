#pragma once
//Global Include
#include "..\Include\Global.h"
//Socket TCP include
#include "..\Include\NameSpace\Network.h"
//Pipe Command Include
#include "..\Include\RTCPipe.h"
//Client Named pipe overlapped
#include ".\C_NamedPipeClient.h"
//machine command class

#include ".\MachineCommands\C_GenericCommands.h"
#include ".\MachineCommands\C_ScorerProfileCommands.h"
#include ".\MachineCommands\C_AlarmHistoryCommands.h"
#include ".\MachineCommands\C_RotaryFormatCommands.h"
#include ".\MachineCommands\C_WebDirectorCommands.h"
#include ".\MachineCommands\C_SingleFacerCommands.h"
#include ".\MachineCommands\C_SplicerCommands.h"
#include ".\MachineCommands\C_Twin400Commands.h"
#include ".\MachineCommands\C_SlitterScorerCommands.h"
#include ".\MachineCommands\C_RotaryCommands.h"
#include ".\MachineCommands\C_KnifeCommands.h"
#include ".\MachineCommands\C_StackerCommands.h"
#include ".\MachineCommands\C_MthCommands.h"
#include ".\MachineCommands\C_DoubleBackerCommands.h"
#include ".\MachineCommands\C_PreHeatersTernCommands.h"
#include ".\MachineCommands\C_GlueUnitCommands.h"
#include ".\MachineCommands\C_StarterCommands.h"
#include ".\MachineCommands\C_ExternWECCommands.h"
#include ".\MachineCommands\C_SimulationCommands.h"
#include ".\MachineCommands\C_ProcessControlCommands.h"
#include "..\Include\ValidationExport.h"

#include <list>

class C_RTCManager
{
public:
	C_RTCManager(void);
	~C_RTCManager(void);
	DWORD createPipe(void);
//	DWORD stopPipe(void);
//	DWORD resumePipe(void);
	bool ChooseMachine(String::StrType command);
	void CloseRTCManager(void);
	void AlignRTC(void);

	static bool finished;

	//ABello_FI05/10/2006_1 Fixed the bug for thread safe list
	//static std::list<NamedPipe::CommandPipe> ListCmd;
	static Utility::C_List<NamedPipe::CommandPipe> ListCmd;
	
	static DWORD CreateCommands(NamedPipe::CommandPipe& inbuf);

private:
	friend class C_GenericCommands;
	friend class C_PreHeatersTernCommands;
	friend class C_DoubleBackerCommands;
	friend class C_Twin400Commands;
	friend class C_SlitterScorerCommands;
	friend class C_WebDirectorCommands;
	friend class C_RotaryCommands;
	friend class C_KnifeCommands;
	friend class C_StackerCommands;
	friend class C_MthCommands;
	friend class C_SingleFacerCommands;
	friend class C_GlueUnitCommands;
	friend class C_SplicerCommands;
	friend class C_ExternWECCommands;
	friend class C_IncDecCommands;
	friend class C_SimulationCommands;
	friend class C_ProcessControlCommands;

	static C_ScorerProfileCommands scorerProfileCommands;
	static C_GenericCommands genericCommands;
	static C_RotaryFormatCommands rotaryFormatCommands;
	static C_AlarmHistoryCommands alarmHistoryCommands;
	static C_WebDirectorCommands webDirectorCommands;
	static C_SingleFacerCommands singleFacer1Commands;
	static C_SingleFacerCommands singleFacer2Commands;
	static C_SingleFacerCommands singleFacer3Commands;
	static C_Twin400Commands twin400Commands;
	static C_SlitterScorerCommands slitterScorerCommands;
	static C_RotaryCommands rotaryCommands;
	static C_KnifeCommands knifeCommands;
	static C_StackerCommands stacker1Commands;
	static C_StackerCommands stacker2Commands;
	static C_StackerCommands stacker3Commands;
	static C_MthCommands mthCommands;
	static C_ExternWECCommands externWECCommands;
	static C_DoubleBackerCommands doubleBackerCommands;
	static C_PreHeatersTernCommands preHeatersTernCommands;
	static C_GlueUnitCommands glueUnitCommands;
	static C_StarterCommands starterCommands;
	static C_SplicerCommands splicer1Commands;
	static C_SplicerCommands splicer2Commands;
	static C_SplicerCommands splicer3Commands;
	static C_SplicerCommands splicer4Commands;
	static C_SplicerCommands splicer5Commands;
	static C_SplicerCommands splicer6Commands;
	static C_SplicerCommands splicer7Commands;
	static C_SimulationCommands simulationCommands;
	static C_ProcessControlCommands processControlCommands;

	static NamedPipe::C_NamedPipeClient RTCSyncroS;
	HANDLE pid_RTCNamedPipe; //NamedPipe Thread Pid
	static DWORD WINAPI Manage_NamedPipeRTC(LPVOID  lpParam);

	static bool CreateRTCCommand(NamedPipe::CommandPipe& inbuf);

	static bool ParseLineConfigCommand(String::StrType theCommand, NamedPipe::CommandPipe &outbuf);
};
