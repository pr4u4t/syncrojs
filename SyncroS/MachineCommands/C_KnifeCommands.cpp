#include ".\C_KnifeCommands.h"

#include "..\C_RTCManager.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;
extern Logger::C_Logger logConfig;

C_KnifeCommands::C_KnifeCommands(void)
{
}

C_KnifeCommands::~C_KnifeCommands(void)
{
}

bool C_KnifeCommands::CreateCommand(NamedPipe::CommandPipe& outbuf)
{
	switch ( outbuf.command.knifeCommand.cmd )
	{

	case NamedPipe::KnifeCommand::SendConfiguration:
		return CreateConfigCommand(outbuf);
		break;

	case NamedPipe::KnifeCommand::UpdateAlarmList:
	{
		size_t len = outbuf.command.knifeCommand.alarmList.size() * ( String::MAX_CHARS_SYSTEMTIME + MAX_CODE + wcslen(Network::SEP) + 1);
		len++; //for \0
		wchar_t *alarm = new wchar_t[len+1];
		if(!outbuf.command.knifeCommand.alarmList.getAllActiveAlarm(alarm, len, Network::SEP))      
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Knife Active Alarm List: Check the buffer lenght possible buffer overrun");
		else
		{
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"Knife Active Alarm List %s",alarm);
			o_socket.Send(Network::MSG_RTC_SWI,L"update.%s(%s)",outbuf.command.knifeCommand.alarmList.getMachineGeneric(),alarm);
		}
		delete[] alarm;
		return true;
		break;
	}
	case NamedPipe::KnifeCommand::Error:
		if(outbuf.isToView())
			o_socket.Send(Network::MSG_PAG_REF, L"pageManager._SetMessage(s[%s], b[true])", outbuf.getResponse());
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"Impossible to execute command: %s",outbuf.getResponse());
		return false;
		break;
	default:
		return false;
	}
	return false;
}

bool C_KnifeCommands::ManageCommand(String::StrType page, String::StrType knifeCommand)
{
	NamedPipe::CommandPipe commandPipe;
	commandPipe.Clean();
	commandPipe.addressee=NamedPipe::CommandPipe::Knife;

	String::StrType theKnifeCommand = knifeCommand.extractCommand();
	if (theKnifeCommand.isEmpty())
		return false;

	//SendConfiguration
	if(theKnifeCommand.CompareNoCase(L"SendConfiguration"))
		return ParseConfigCommand(knifeCommand, commandPipe);

	//SendCurrentOrder
	if (theKnifeCommand.CompareNoCase(L"SendCurrentOrder"))
	{
		commandPipe.command.knifeCommand.cmd = NamedPipe::KnifeCommand::SendCurrentOrder;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//SendNextOrder
	if (theKnifeCommand.CompareNoCase(L"SendNextOrder"))
	{
		commandPipe.command.knifeCommand.cmd = NamedPipe::KnifeCommand::SendNextOrder;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//SendNextP1Order
	if (theKnifeCommand.CompareNoCase(L"SendNextP1Order"))
	{
		commandPipe.command.knifeCommand.cmd = NamedPipe::KnifeCommand::SendNextP1Order;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//SendNextP2Order
	if (theKnifeCommand.CompareNoCase(L"SendNextP2Order"))
	{
		commandPipe.command.knifeCommand.cmd = NamedPipe::KnifeCommand::SendNextP2Order;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//SendNextP3Order
	if (theKnifeCommand.CompareNoCase(L"SendNextP3Order"))
	{
		commandPipe.command.knifeCommand.cmd = NamedPipe::KnifeCommand::SendNextP3Order;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//addBadCutOnLevel
	if(theKnifeCommand.CompareNoCase(L"addBadCutsOnLevel")) 
	{
		std::vector<String::StrType> vect = knifeCommand.Tokenize(Network::SEP);
		if(vect.size()==2)
		{
			commandPipe.command.knifeCommand.cmd = NamedPipe::KnifeCommand::addBadCutOnLevel;
			swprintf(commandPipe.command.knifeCommand.data[0],L"%s",vect[0].getPtr()); // Level
			swprintf(commandPipe.command.knifeCommand.data[1],L"%s",vect[1].getPtr()); // Num Bad cuts
			C_RTCManager::RTCSyncroS.Write(&commandPipe);
			return true;
		}
	}

	logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Knife wrong command: %s", theKnifeCommand.getPtr());
	return false;
}

bool C_KnifeCommands::getPipeCmd(String::StrType command, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop)
{
	commandPipeStart.addressee = NamedPipe::CommandPipe::Knife;
	commandPipeStop.addressee = NamedPipe::CommandPipe::Knife;



	///



	return false;
}

bool C_KnifeCommands::CreateConfigCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[String::MAX_CHARS_CONFIG_MACHINE]=L"";
	outbuf.command.knifeCommand.config.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.KnifeConfig.Config(%s)", cfg);
	
	return true;
}

bool C_KnifeCommands::ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.knifeCommand.cmd = NamedPipe::KnifeCommand::SendConfiguration;
	outbuf.command.knifeCommand.config.Deserialize(stringConfig, Network::SEP);
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}

