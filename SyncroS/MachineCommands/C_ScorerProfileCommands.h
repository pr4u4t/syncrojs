#pragma once

//Global include
#include "..\..\Include\Global.h"

//logger include
#include "..\..\\Shared\\C_Logger.h"

//Client Named pipe overlapped
#include "..\..\Include\RTCPipe.h"

class C_ScorerProfileCommands
{
public:
	C_ScorerProfileCommands(void);
	~C_ScorerProfileCommands(void);
	bool ManageCommand(String::StrType ScorerProfileCommand, NamedPipe::CommandPipe& outbuf);
	bool CreateCommand(NamedPipe::CommandPipe& inbuffer);
	
private:
	bool ParseCommand(String::StrType theCommand, NamedPipe::CommandPipe& outbuf);
	//bool CreateCommand(CommandPipe *outbuf);
};