#pragma once

//Global include
#include "..\..\Include\Global.h"

//logger include
#include "..\..\\Shared\\C_Logger.h"

//Client Named pipe overlapped
#include "..\..\Include\RTCPipe.h"

#include ".\C_IncDecCommands.h"

class C_StackerCommands : private C_IncDecCommands
{
public:
	C_StackerCommands(int theLevel);
	C_StackerCommands(void);
	~C_StackerCommands(void);
	bool ManageCommand(String::StrType page, String::StrType StackerCommand);
	bool CreateCommand(NamedPipe::CommandPipe& outbuf);
	
private:
	int level;
	bool CreateConfigCommand(NamedPipe::CommandPipe& outbuf);
	bool getPipeCmd(String::StrType cmd, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop);
	bool ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
};