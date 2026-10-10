#pragma once

//Global include
#include "..\..\Include\Global.h"

//logger include
#include "..\..\\Shared\\C_Logger.h"

//Client Named pipe overlapped
#include "..\..\Include\RTCPipe.h"

#include ".\C_IncDecCommands.h"


class C_ExternWECCommands: private C_IncDecCommands
{
public:
	C_ExternWECCommands(void);
	~C_ExternWECCommands(void);
	bool ManageCommand(String::StrType page, String::StrType theExternWECStringCommand);
	bool CreateCommand(NamedPipe::CommandPipe& outbuf);
	
private:
	//bool CreateConfigCommand(NamedPipe::CommandPipe& outbuf);
	bool getPipeCmd(String::StrType cmd, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop);
	//bool ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
};