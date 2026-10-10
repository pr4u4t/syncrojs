#pragma once

//Global include
#include "..\..\Include\Global.h"

//logger include
#include "..\..\Shared\C_Logger.h"

//Client Named pipe overlapped
#include "..\..\Include\RTCPipe.h"

class C_RotaryCommands
{
public:
	C_RotaryCommands(void);
	~C_RotaryCommands(void);
	bool ManageCommand(String::StrType rotaryCommand);
	bool CreateCommand(NamedPipe::CommandPipe& outbuf);
	
private:
	bool ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
	bool CreateConfigCommand(NamedPipe::CommandPipe& outbuf);
};