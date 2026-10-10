#pragma once

//Global include
#include "..\..\Include\Global.h"

//logger include
#include "..\..\\Shared\\C_Logger.h"

//Client Named pipe overlapped
#include "..\..\Include\RTCPipe.h"

#include ".\C_IncDecCommands.h"

class C_Twin400Commands : private C_IncDecCommands
{
public:
	C_Twin400Commands(void);
	~C_Twin400Commands(void);
	bool ManageCommand(String::StrType page, String::StrType Twin400command);
	bool CreateCommand(NamedPipe::CommandPipe& outbuf);
	
private:
	bool CreateConfigCommand(NamedPipe::CommandPipe& outbuf);
	bool CreateBrokenSlitsCommand(NamedPipe::CommandPipe& outbuf);
	bool CreateToggleDefaultShaftCommand(NamedPipe::CommandPipe& outbuf);
	bool ParseConfigCommand(String::StrType stringConf, NamedPipe::CommandPipe& outbuf);
	bool getPipeCmd(String::StrType cmd, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop);
};