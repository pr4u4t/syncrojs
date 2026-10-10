#pragma once

//Global include
#include "..\..\Include\Global.h"

//logger include
#include "..\..\\Shared\\C_Logger.h"

//Client Named pipe overlapped
#include "..\..\Include\RTCPipe.h"

#include ".\C_IncDecCommands.h"

class C_GenericCommands : private C_IncDecCommands
{
public:
	C_GenericCommands(void);
	~C_GenericCommands(void);
	bool ManageOperatorCommand(String::StrType OperatorCommand);
	bool ManageSyncroCommand(String::StrType SyncroCommand);
	bool ManageAOCCommand(String::StrType AOCCommand);
	bool ManageWECCommand(String::StrType WECCommand);
	bool ManageSpeedCommand(String::StrType SpeedCommand);
	bool ManageSpeedReferenceCommand(String::StrType SpeedReferenceCommand);
	bool ManageEncodersCommand(String::StrType EncodersCommand);
	//RFazzi_FI28/09/2006_2.Added Office LineConfig section
	bool ManageOfficeCommand(String::StrType OfficeCommand);
	//RFazzi_FI29/09/2006_1.Moved the Printers Settings into LineConfig section
	bool ManagePrintersCommand(String::StrType PrintersCommand);
	bool CreateCommand(NamedPipe::CommandPipe& outbuf);
	
private:
	bool getPipeCmd(String::StrType cmd, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop);
	//ENCODERS
	bool CreateEncodersCommand(NamedPipe::CommandPipe& outbuf);
	bool ParseEncodersConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
	//OPERATOR
	bool CreateOperatorCommand(NamedPipe::CommandPipe& outbuf);
	bool ParseOperatorConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
	//AOC
	bool CreateAOCCommand(NamedPipe::CommandPipe& outbuf);
	bool ParseAOCConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
	//WEC
	bool CreateWECCommand(NamedPipe::CommandPipe& outbuf);
	bool ParseWECConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
	//SPEED
	bool CreateSpeedCommand(NamedPipe::CommandPipe& outbuf);
	bool ParseSpeedConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
	//SPEEDReference
	bool CreateSpeedReferenceCommand(NamedPipe::CommandPipe& outbuf);
	bool ParseSpeedReferenceConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
	//SYNCRO
	bool CreateSyncroCommand(NamedPipe::CommandPipe& outbuf);
	bool ParseSyncroConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
	//RFazzi_FI28/09/2006_2.Added Office LineConfig section
	//OFFICE
	bool CreateOfficeCommand(NamedPipe::CommandPipe& outbuf);
	bool ParseOfficeConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);
	//RFazzi_FI29/09/2006_1.Moved the Printers Settings into LineConfig section
	//PRINTERS
	bool CreatePrintersCommand(NamedPipe::CommandPipe& outbuf);
	bool ParsePrintersConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf);

};
