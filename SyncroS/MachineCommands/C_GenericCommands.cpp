#include ".\C_GenericCommands.h"

#include "..\C_RTCManager.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;
extern Logger::C_Logger logConfig;

C_GenericCommands::C_GenericCommands(void)
{
}

C_GenericCommands::~C_GenericCommands(void)
{
}

bool C_GenericCommands::CreateCommand(NamedPipe::CommandPipe& outbuf)
{

	switch ( outbuf.command.myRTCCommand.cmd )
	{
	case NamedPipe::RTCCommand::configOperator:
		return CreateOperatorCommand(outbuf);
		break;

	case NamedPipe::RTCCommand::configSyncro:
		return CreateSyncroCommand(outbuf);
		break;

	case NamedPipe::RTCCommand::configAOC:
		return CreateAOCCommand(outbuf);
		break;

	case NamedPipe::RTCCommand::configWEC:
		return CreateWECCommand(outbuf);
		break;

	case NamedPipe::RTCCommand::configSpeed:
		return CreateSpeedCommand(outbuf);
		break;
		
	case NamedPipe::RTCCommand::configSpeedReference :
		return CreateSpeedReferenceCommand(outbuf);
		break;

	case NamedPipe::RTCCommand::configEncoders :
		return CreateEncodersCommand(outbuf);
		break;

	//RFazzi_FI28/09/2006_2.Added Office LineConfig section
	case NamedPipe::RTCCommand::configOffice :
		return CreateOfficeCommand(outbuf);
		break;
	//RFazzi_FI29/09/2006_1.Moved the Printers Settings into LineConfig section
	case NamedPipe::RTCCommand::configPrinters :
		return CreatePrintersCommand(outbuf);
		break;


	case NamedPipe::RTCCommand::Error:
		if(outbuf.isToView())
		{
			logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"Receved an error from RTC ... %s", outbuf.getResponse());
			o_socket.Send(Network::MSG_PAG_REF, L"pageManager._SetMessage(s[%s], b[true])", outbuf.getResponse());
		}
		return false;
		break;
	default:
			logger.Severe(__FUNCTION__,Logger::PRE_PIPE_RCV_AND_SOCK_SEND,L"Invalid Command received from RTC %d",outbuf.command.myRTCCommand.cmd);
			return false;
	}
	return false;
}

bool C_GenericCommands::ManageOperatorCommand(String::StrType OperatorCommand)
{
	NamedPipe::CommandPipe commandPipe;
	commandPipe.Clean();
	commandPipe.addressee=NamedPipe::CommandPipe::RTC;
	bool res = false;

	String::StrType theSyncroCommand = OperatorCommand.extractCommand();
	if (theSyncroCommand.isEmpty())
		return false;

	//OperatorCommand
	if(theSyncroCommand.CompareNoCase(L"SendConfiguration"))
		res = ParseOperatorConfigCommand(OperatorCommand, commandPipe);

	if(res)
    C_RTCManager::RTCSyncroS.Write(&commandPipe);
	else
    logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Generic wrong command: %s", OperatorCommand.getPtr());

	return res;
}

bool C_GenericCommands::ManageSyncroCommand(String::StrType SyncroCommand)
{
	NamedPipe::CommandPipe commandPipe;
	commandPipe.Clean();
	commandPipe.addressee=NamedPipe::CommandPipe::RTC;
	bool res = false;

	String::StrType theSyncroCommand = SyncroCommand.extractCommand();
	if (theSyncroCommand.isEmpty())
		return false;

	//SyncroConfiguration
	if(theSyncroCommand.CompareNoCase(L"SendConfiguration"))
		res = ParseSyncroConfigCommand(SyncroCommand, commandPipe);

	if(res)
    C_RTCManager::RTCSyncroS.Write(&commandPipe);
	else
    logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Generic wrong command: %s", SyncroCommand.getPtr());

	return res;
}

bool C_GenericCommands::ManageAOCCommand(String::StrType AOCCommand)
{
	NamedPipe::CommandPipe commandPipe;
	commandPipe.Clean();
	commandPipe.addressee=NamedPipe::CommandPipe::RTC;
	bool res = false;

	String::StrType theAOCCommand = AOCCommand.extractCommand();
	if (theAOCCommand.isEmpty())
		return false;

	//AOCConfiguration
	if(theAOCCommand.CompareNoCase(L"SendConfiguration"))
		res = ParseAOCConfigCommand(AOCCommand, commandPipe);

	//SwitchDryEndMode
	if (theAOCCommand.CompareNoCase(L"SwitchDryEndMode"))
	{
		commandPipe.command.myRTCCommand.cmd = NamedPipe::RTCCommand::ToggleModeDryEnd;
		res = true;
	};

	if(res)
    C_RTCManager::RTCSyncroS.Write(&commandPipe);
	else
    logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Generic wrong command: %s", AOCCommand.getPtr());

	return res;
}

bool C_GenericCommands::ManageWECCommand(String::StrType WECCommand)
{
	NamedPipe::CommandPipe commandPipe;
	commandPipe.Clean();
	commandPipe.addressee=NamedPipe::CommandPipe::RTC;
	bool res = false;

	String::StrType theWECCommand = WECCommand.extractCommand();
	if (theWECCommand.isEmpty())
		return false;

	//WECConfiguration
	if(theWECCommand.CompareNoCase(L"SendConfiguration"))
		res = ParseWECConfigCommand(WECCommand, commandPipe);

	//ToggleAutoManWetEnd
	if (theWECCommand.CompareNoCase(L"ToggleAutoManWetEnd"))
	{
		commandPipe.command.myRTCCommand.cmd = NamedPipe::RTCCommand::ToggleAutoManWetEnd;
		res = true;
	};

	//ToggleAutoManZone0
	if (theWECCommand.CompareNoCase(L"ToggleAutoManZone0"))
	{
		commandPipe.command.myRTCCommand.cmd = NamedPipe::RTCCommand::ToggleAutoManZone0;
		res = true;
	};

	//ToggleAutoManZone1
	if (theWECCommand.CompareNoCase(L"ToggleAutoManZone1"))
	{
		commandPipe.command.myRTCCommand.cmd = NamedPipe::RTCCommand::ToggleAutoManZone1;
		res = true;
	};

	//ToggleAutoManZone2
	if (theWECCommand.CompareNoCase(L"ToggleAutoManZone2"))
	{
		commandPipe.command.myRTCCommand.cmd = NamedPipe::RTCCommand::ToggleAutoManZone2;
		res = true;
	};

	//ToggleAutoManZone3
	if (theWECCommand.CompareNoCase(L"ToggleAutoManZone3"))
	{
		commandPipe.command.myRTCCommand.cmd = NamedPipe::RTCCommand::ToggleAutoManZone3;
		res = true;
	};

	//CancelSync
	if (theWECCommand.CompareNoCase(L"CancelSync"))
	{
		commandPipe.command.myRTCCommand.cmd = NamedPipe::RTCCommand::CancelSync;
		res = true;
	};

	//CancelSync
	if (theWECCommand.CompareNoCase(L"SyncMode"))
	{
		commandPipe.command.myRTCCommand.cmd = NamedPipe::RTCCommand::SyncMode;
		commandPipe.command.myRTCCommand.RTCDataBuffer.dataNumeric[0] = WECCommand.ToInt();
		res = true;
	};

	if(res)
    C_RTCManager::RTCSyncroS.Write(&commandPipe);
	else
    logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Generic wrong command: %s", WECCommand.getPtr());

	return res;
}

bool C_GenericCommands::ManageSpeedCommand(String::StrType SpeedCommand)
{
	NamedPipe::CommandPipe commandPipe;
	commandPipe.Clean();
	commandPipe.addressee=NamedPipe::CommandPipe::RTC;
	bool res = false;

	String::StrType theSpeedCommand =  SpeedCommand.extractCommand();
	if (theSpeedCommand.isEmpty())
		return false;

	//SpeedConfiguration
	if(theSpeedCommand.CompareNoCase(L"SendConfiguration"))
		res = ParseSpeedConfigCommand(SpeedCommand, commandPipe);

	if(res)
    C_RTCManager::RTCSyncroS.Write(&commandPipe);
	else
    logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Generic wrong command: %s", SpeedCommand.getPtr());
	return false;
}

bool C_GenericCommands::ManageSpeedReferenceCommand(String::StrType SpeedReferenceCommand)
{
	NamedPipe::CommandPipe commandPipe;
	commandPipe.Clean();
	commandPipe.addressee=NamedPipe::CommandPipe::RTC;
	bool res = false;

	String::StrType theSpeedReferenceCommand =  SpeedReferenceCommand.extractCommand();
	if (theSpeedReferenceCommand.isEmpty())
		return false;

	//SpeedConfiguration
	if(theSpeedReferenceCommand.CompareNoCase(L"SendConfiguration"))
		res = ParseSpeedReferenceConfigCommand(SpeedReferenceCommand, commandPipe);

	if(res)
		C_RTCManager::RTCSyncroS.Write(&commandPipe);
	else
    logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Generic wrong command: %s", SpeedReferenceCommand.getPtr());
	return false;
}

//RFazzi_FI28/09/2006_2.Added Office LineConfig section
bool C_GenericCommands::ManageOfficeCommand(String::StrType OfficeCommand)
{
	NamedPipe::CommandPipe commandPipe;
	commandPipe.Clean();
	commandPipe.addressee=NamedPipe::CommandPipe::RTC;
	bool res = false;

	String::StrType theOfficeCommand =  OfficeCommand.extractCommand();
	if (theOfficeCommand.isEmpty())
		return false;

	//OfficeConfiguration
	if(theOfficeCommand.CompareNoCase(L"SendConfiguration"))
		res = ParseOfficeConfigCommand(OfficeCommand, commandPipe);

	if(res)
		C_RTCManager::RTCSyncroS.Write(&commandPipe);
	else
    logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Generic wrong command: %s", OfficeCommand.getPtr());
	return false;
}

//RFazzi_FI29/09/2006_1.Moved the Printers Settings into LineConfig section
bool C_GenericCommands::ManagePrintersCommand(String::StrType PrintersCommand)
{
	NamedPipe::CommandPipe commandPipe;
	commandPipe.Clean();
	commandPipe.addressee=NamedPipe::CommandPipe::RTC;
	bool res = false;

	String::StrType thePrintersCommand =  PrintersCommand.extractCommand();
	if (thePrintersCommand.isEmpty())
		return false;

	//PrintersConfiguration
	if(thePrintersCommand.CompareNoCase(L"SendConfiguration"))
		res = ParsePrintersConfigCommand(PrintersCommand, commandPipe);

	if(res)
		C_RTCManager::RTCSyncroS.Write(&commandPipe);
	else
    logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Generic wrong command: %s", PrintersCommand.getPtr());
	return false;
}

bool C_GenericCommands::ManageEncodersCommand(String::StrType EncodersCommand)
{
	NamedPipe::CommandPipe commandPipe;
	commandPipe.Clean();
	commandPipe.addressee=NamedPipe::CommandPipe::RTC;
	bool res = false;

	String::StrType theEncodersCommand =  EncodersCommand.extractCommand();
	if (theEncodersCommand.isEmpty())
		return false;

	//EncodersCommand
	if(theEncodersCommand.CompareNoCase(L"SendConfiguration"))
		res = ParseEncodersConfigCommand(EncodersCommand, commandPipe);

	if(res)
		C_RTCManager::RTCSyncroS.Write(&commandPipe);
	else
    logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Generic wrong command: %s", EncodersCommand.getPtr());
	return false;
}

bool C_GenericCommands::getPipeCmd(String::StrType command, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop)
{
	commandPipeStart.addressee = NamedPipe::CommandPipe::Knife;
	commandPipeStop.addressee = NamedPipe::CommandPipe::Knife;



	///




	return false;
}

bool C_GenericCommands::CreateOperatorCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[5000]=L"";
	outbuf.command.myRTCCommand.RTCDataBuffer.operatorConfig.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.OperatorConfig.Config(%s)", cfg);
	
	return true;
}

bool C_GenericCommands::ParseOperatorConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.myRTCCommand.cmd = NamedPipe::RTCCommand::configOperator;
	outbuf.command.myRTCCommand.RTCDataBuffer.operatorConfig.Deserialize(stringConfig,Network::SEP);
	return true;
}

bool C_GenericCommands::CreateSyncroCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[5000]=L"";
	outbuf.command.myRTCCommand.RTCDataBuffer.syncro.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.SyncroConfig.Config(%s)", cfg);
	
	return true;
}

bool C_GenericCommands::ParseSyncroConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.myRTCCommand.cmd = NamedPipe::RTCCommand::configSyncro;
	outbuf.command.myRTCCommand.RTCDataBuffer.syncro.Deserialize(stringConfig, Network::SEP);
	return true;
}

bool C_GenericCommands::CreateAOCCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[5000]=L"";
	outbuf.command.myRTCCommand.RTCDataBuffer.aoc.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.AOCConfig.Config(%s)", cfg);
	
	return true;
}

bool C_GenericCommands::CreateWECCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[5000]=L"";
	outbuf.command.myRTCCommand.RTCDataBuffer.wec.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.WECConfig.Config(%s)", cfg);
	
	return true;
}

bool C_GenericCommands::ParseAOCConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.myRTCCommand.cmd = NamedPipe::RTCCommand::configAOC;
	outbuf.command.myRTCCommand.RTCDataBuffer.aoc.Deserialize(stringConfig, Network::SEP);
	return true;
}

bool C_GenericCommands::ParseWECConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.myRTCCommand.cmd = NamedPipe::RTCCommand::configWEC;
	outbuf.command.myRTCCommand.RTCDataBuffer.wec.Deserialize(stringConfig, Network::SEP);
	return true;
}

bool C_GenericCommands::CreateSpeedCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[5000]=L"";

	outbuf.command.myRTCCommand.RTCDataBuffer.speed.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.SPEEDConfig.Config(%s)", cfg);
	
	return true;
}

bool C_GenericCommands::ParseSpeedConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.myRTCCommand.cmd = NamedPipe::RTCCommand::configSpeed;
	outbuf.command.myRTCCommand.RTCDataBuffer.speed.Deserialize(stringConfig, Network::SEP);
	return true;
}

bool C_GenericCommands::CreateSpeedReferenceCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[5000]=L"";

	outbuf.command.myRTCCommand.RTCDataBuffer.speedReference.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.SPEEDReferenceConfig.Config(%s)", cfg);
	
	return true;
}

bool C_GenericCommands::ParseSpeedReferenceConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.myRTCCommand.cmd = NamedPipe::RTCCommand::configSpeedReference;
	outbuf.command.myRTCCommand.RTCDataBuffer.speedReference.Deserialize(stringConfig, Network::SEP);
	return true;
}

bool C_GenericCommands::CreateEncodersCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[5000]=L"";

	outbuf.command.myRTCCommand.RTCDataBuffer.encoders.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.EncodersConfig.Config(%s)", cfg);
	
	return true;
}

bool C_GenericCommands::ParseEncodersConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.myRTCCommand.cmd = NamedPipe::RTCCommand::configEncoders;
	outbuf.command.myRTCCommand.RTCDataBuffer.encoders.Deserialize(stringConfig, Network::SEP);
	return true;
}

//RFazzi_FI28/09/2006_2.Added Office LineConfig section
bool C_GenericCommands::CreateOfficeCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[5000]=L"";

	outbuf.command.myRTCCommand.RTCDataBuffer.officeConfig.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.OFFICEConfig.Config(%s)", cfg);
	
	return true;
}

bool C_GenericCommands::ParseOfficeConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.myRTCCommand.cmd = NamedPipe::RTCCommand::configOffice;
	outbuf.command.myRTCCommand.RTCDataBuffer.officeConfig.Deserialize(stringConfig, Network::SEP);
	return true;
}

//RFazzi_FI29/09/2006_1.Moved the Printers Settings into LineConfig section
bool C_GenericCommands::CreatePrintersCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[5000]=L"";

	outbuf.command.myRTCCommand.RTCDataBuffer.printersConfig.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.PRINTERSConfig.Config(%s)", cfg);
	
	return true;
}

bool C_GenericCommands::ParsePrintersConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.myRTCCommand.cmd = NamedPipe::RTCCommand::configPrinters;
	outbuf.command.myRTCCommand.RTCDataBuffer.printersConfig.Deserialize(stringConfig, Network::SEP);
	return true;
}