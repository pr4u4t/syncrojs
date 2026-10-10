#include ".\C_WebDirectorCommands.h"
#include "..\C_RTCManager.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;
extern Logger::C_Logger logConfig;

C_WebDirectorCommands::C_WebDirectorCommands(void)
{

}

C_WebDirectorCommands::~C_WebDirectorCommands(void)
{
}

bool C_WebDirectorCommands::ManageCommand(String::StrType WebDirectorcommand)
{
	NamedPipe::CommandPipe commandPipe;	
	commandPipe.addressee = NamedPipe::CommandPipe::WebDirector;

	String::StrType command = WebDirectorcommand.extractCommand();
	if (command.isEmpty())
		return false;
	if(command.CompareNoCase(L"SendConfiguration"))
		return ParseConfigCommand(WebDirectorcommand, commandPipe);
	logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Web Director wrong command: %s", command);
	return false;
}

bool C_WebDirectorCommands::CreateConfigCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[String::MAX_CHARS_CONFIG_MACHINE]=L"";
	outbuf.command.webDirectorCommand.config.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.WebDirectorConfig.Config(%s)", cfg);
	return true;
}

bool C_WebDirectorCommands::CreateCommand(NamedPipe::CommandPipe& outbuf)
{
	switch ( outbuf.command.webDirectorCommand.cmd )
	{
	case NamedPipe::WebDirectorCommand::SendConfiguration:
		return CreateConfigCommand(outbuf);
		break;
	default:
		return false;
	}

	return false;
}

bool C_WebDirectorCommands::ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.webDirectorCommand.cmd = NamedPipe::WebDirectorCommand::SendConfiguration;
	outbuf.command.webDirectorCommand.config.Deserialize(stringConfig, Network::SEP);
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}
