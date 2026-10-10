#pragma once

//Global include
#include "..\..\Include\Global.h"

//logger include
#include "..\..\\Shared\\C_Logger.h"

//Client Named pipe overlapped
#include "..\..\Include\RTCPipe.h"

#include ".\C_IncDecCommands.h"

class C_ProcessControlCommands  : private C_IncDecCommands
{
public:
	C_ProcessControlCommands(void);
	~C_ProcessControlCommands(void);
	bool ManageCommand(String::StrType page, String::StrType processControlCommands);
	bool CreateCommand(NamedPipe::CommandPipe& outbuf);
	
private:
	bool CreateConfigCommand(NamedPipe::CommandPipe& outbuf);
	bool CreateHeaderConfigCommand (NamedPipe::CommandPipe& outbuf);
	bool getPipeCmd(String::StrType cmd, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop);
	bool ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
	bool ParseConfigCommandHeader(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
	bool ParseSaveWorkingPointCommand(NamedPipe::CommandPipe& outbuf);
	bool ParseSendWarpUpDownValueCommand(String::StrType command, NamedPipe::CommandPipe& outbuf);
  bool ParseSendWarpEndToEndValueCommand(String::StrType command, NamedPipe::CommandPipe& outbuf);
	bool ParseSendUserValueCommand(String::StrType command, NamedPipe::CommandPipe& outbuf);
  bool ParseForceRemotePrcBoardLevelNumberCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
  bool ParseForceLocalPrcBoardLevelNumberCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
  bool ParseResetUserValueCommand(String::StrType command, NamedPipe::CommandPipe& outbuf);
  //RFAZZI_FI25/08/2011_2. Aggiunto controllo PRC CloseLoop ON/OFF. 
  bool ParseToggleWarpCloseLoopEndToEndCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);

};