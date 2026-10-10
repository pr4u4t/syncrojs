#pragma once

//Global include
#include "..\..\Include\Global.h"

//logger include
#include "..\..\\Shared\\C_Logger.h"

//Client Named pipe overlapped
#include "..\..\Include\RTCPipe.h"

class C_SlitterScorerCommands
{
public:
	C_SlitterScorerCommands(void);
	~C_SlitterScorerCommands(void);
	bool ManageCommand(String::StrType page, String::StrType SScommand);
	bool CreateCommand(NamedPipe::CommandPipe& outbuf);
	
private:
	bool CreateConfigCommand(NamedPipe::CommandPipe& outbuf);
	static DWORD WINAPI IncreaseDecrease(LPVOID  lpParam);
	bool ParseConfigCommand(String::StrType stringConf, NamedPipe::CommandPipe& outbuf);
};