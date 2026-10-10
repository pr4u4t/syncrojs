#pragma once

//Global include
#include "..\..\Include\Global.h"

//logger include
#include "..\..\\Shared\\C_Logger.h"

//Client Named pipe overlapped
#include "..\..\Include\RTCPipe.h"

class C_AlarmHistoryCommands
{
public:
	C_AlarmHistoryCommands(void);
	~C_AlarmHistoryCommands(void);
	bool ManageCommand(String::StrType AlarmHistoryCommands, NamedPipe::CommandPipe& outbuf);
	bool CreateCommand(NamedPipe::CommandPipe& inbuffer);
	
private:
	bool ParseCommand(String::StrType theCommand, NamedPipe::CommandPipe& outbuf);
};