#include ".\C_MthCommands.h"

#include "..\C_RTCManager.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;
extern Logger::C_Logger logConfig;

C_MthCommands::C_MthCommands(void)
{
}

C_MthCommands::~C_MthCommands(void)
{
}

bool C_MthCommands::CreateCommand(NamedPipe::CommandPipe& outbuf)
{
	switch ( outbuf.command.mthCommand.cmd )
	{

	case NamedPipe::MthCommand::SendConfiguration:
		return CreateConfigCommand(outbuf);
		break;

	case NamedPipe::MthCommand::UpdateAlarmList:
	{
		size_t len = outbuf.command.mthCommand.alarmList.size() * ( String::MAX_CHARS_SYSTEMTIME + MAX_CODE + wcslen(Network::SEP) + 1);
		len++; //for \0
		wchar_t *alarm = new wchar_t[len+1];
		if(!outbuf.command.mthCommand.alarmList.getAllActiveAlarm(alarm, len, Network::SEP))      
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Mth Active Alarm List: Check the buffer lenght possible buffer overrun");
		else
		{
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"Mth Active Alarm List %s",alarm);
			o_socket.Send(Network::MSG_RTC_SWI,L"update.%s(%s)",outbuf.command.mthCommand.alarmList.getMachineGeneric(),alarm);
		}
		delete[] alarm;
		return true;
		break;
	}
	case NamedPipe::MthCommand::Error:
		if(outbuf.isToView())
			o_socket.Send(Network::MSG_PAG_REF, L"pageManager._SetMessage(s[%s], b[true])", outbuf.getResponse());
		return false;
		break;
	default:
		return false;
	}
	return false;
}

bool C_MthCommands::ManageCommand(String::StrType page, String::StrType mthCommand)
{
	NamedPipe::CommandPipe commandPipe;
	commandPipe.Clean();
	commandPipe.addressee=NamedPipe::CommandPipe::Mth;

	String::StrType theMthCommand = mthCommand.extractCommand();
	if (theMthCommand.isEmpty())
		return false;

	//SendConfiguration
	if(theMthCommand.CompareNoCase(L"SendConfiguration"))
		return ParseConfigCommand(mthCommand, commandPipe);

	logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Mth wrong command: %s", theMthCommand.getPtr());
	return false;
}

bool C_MthCommands::getPipeCmd(String::StrType command, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop)
{
	commandPipeStart.addressee = NamedPipe::CommandPipe::Mth;
	commandPipeStop.addressee = NamedPipe::CommandPipe::Mth;

	return false;
}

bool C_MthCommands::CreateConfigCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[String::MAX_CHARS_CONFIG_MACHINE]=L"";
	outbuf.command.mthCommand.config.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.MthConfig.Config(%s)", cfg);
	
	return true;
}

bool C_MthCommands::ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.mthCommand.cmd = NamedPipe::MthCommand::SendConfiguration;
	outbuf.command.mthCommand.config.Deserialize(stringConfig, Network::SEP);
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}

