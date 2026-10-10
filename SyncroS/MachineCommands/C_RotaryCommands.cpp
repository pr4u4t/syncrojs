#include ".\C_RotaryCommands.h"
#include "..\C_RTCManager.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;
extern Logger::C_Logger logConfig;

C_RotaryCommands::C_RotaryCommands(void)
{

}

C_RotaryCommands::~C_RotaryCommands(void)
{
}

bool C_RotaryCommands::ManageCommand(String::StrType rotaryCommand)
{
	NamedPipe::CommandPipe commandPipe;
	commandPipe.addressee=NamedPipe::CommandPipe::RotaryCS;

	String::StrType command = rotaryCommand.extractCommand();
	if (command.isEmpty())
		return false;
	if(command.CompareNoCase(L"SendConfiguration"))
		return ParseConfigCommand(rotaryCommand, commandPipe);
	logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Rotary wrong command: %s", command);
	return false;
}

bool C_RotaryCommands::CreateConfigCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[String::MAX_CHARS_CONFIG_MACHINE]=L"";
	outbuf.command.rotaryCommand.config.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.RotaryConfig.Config(%s)", cfg);
	
	return true;
}

bool C_RotaryCommands::CreateCommand(NamedPipe::CommandPipe& outbuf)
{
	switch ( outbuf.command.rotaryCommand.cmd )
	{
	case NamedPipe::RotaryCommand::SendConfiguration:
		return CreateConfigCommand(outbuf);
		break;
	default:
		return false;
	}

	return false;
}

bool C_RotaryCommands::ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.rotaryCommand.cmd = NamedPipe::RotaryCommand::SendConfiguration;
	outbuf.command.rotaryCommand.config.Deserialize(stringConfig, Network::SEP);
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}


