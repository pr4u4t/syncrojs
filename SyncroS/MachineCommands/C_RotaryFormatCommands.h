#pragma once

//Global include
#include "..\..\Include\Global.h"

//logger include
#include "..\..\\Shared\\C_Logger.h"

//Client Named pipe overlapped
#include "..\..\Include\RTCPipe.h"

class C_RotaryFormatCommands
{
public:
	C_RotaryFormatCommands(void);
	~C_RotaryFormatCommands(void);
	//bool ManageCommand(StrType RotaryFormatCommand, CommandPipe& outbuf);
	bool CreateCommand(NamedPipe::CommandPipe& inbuffer);
	
private:
	//bool ParseCommand(StrType theCommand, CommandPipe& outbuf);
	//bool CreateCommand(CommandPipe *outbuf);
};