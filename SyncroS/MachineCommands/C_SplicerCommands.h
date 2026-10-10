#pragma once

//Global include
#include "..\..\Include\Global.h"

//logger include
#include "..\..\\Shared\\C_Logger.h"

//Client Named pipe overlapped
#include "..\..\Include\RTCPipe.h"

#include ".\C_IncDecCommands.h"

class C_SplicerCommands: private C_IncDecCommands
{
public:
	C_SplicerCommands(const int theLevel);
	~C_SplicerCommands(void);
	bool ManageCommand(String::StrType page, String::StrType theSplicerStringCommand);
	bool CreateCommand(NamedPipe::CommandPipe& outbuf);
	
private:
	int splicerNumber;
	bool CreateConfigCommand(NamedPipe::CommandPipe& outbuf);
	bool getPipeCmd(String::StrType cmd, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop);
	bool ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
};