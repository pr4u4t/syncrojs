#pragma once

//Global include
#include "..\..\Include\Global.h"

//logger include
#include "..\..\\Shared\\C_Logger.h"

//Client Named pipe overlapped
#include "..\..\Include\RTCPipe.h"

#include ".\C_IncDecCommands.h"

class C_SingleFacerCommands : private C_IncDecCommands
{
public:
	C_SingleFacerCommands(int SFNum);
	~C_SingleFacerCommands(void);
	bool ManageCommand(String::StrType page, String::StrType SingleFacerStringCommand);
	bool CreateCommand(NamedPipe::CommandPipe& outbuf);

private:
	int SFNum;
	bool CreateConfigCommand(NamedPipe::CommandPipe& outbuf);
	bool B_CreateConfigCommand(NamedPipe::CommandPipe& outbuf);
	bool getPipeCmd(String::StrType cmd, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop);
	bool ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
	bool B_ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
};