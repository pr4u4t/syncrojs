#pragma once

//Global include
#include "..\..\Include\Global.h"

//logger include
#include "..\..\Shared\C_Logger.h"

//Client Named pipe overlapped
#include "..\..\Include\RTCPipe.h"

class C_SimulationCommands
{
public:
	C_SimulationCommands(void);
	~C_SimulationCommands(void);
	bool ManageCommand(String::StrType simulationCommand);
//	bool CreateCommand(NamedPipe::CommandPipe& outbuf);
//	
//private:
//	bool ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
//	bool CreateConfigCommand(NamedPipe::CommandPipe& outbuf);
};