#include ".\C_StarterCommands.h"

#include "..\C_RTCManager.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;

C_StarterCommands::C_StarterCommands(void)
{
}

C_StarterCommands::~C_StarterCommands(void)
{
}

bool C_StarterCommands::CreateCommand(NamedPipe::CommandPipe& outbuf)
{
	switch ( outbuf.command.starterCommand.cmd )
	{
	case NamedPipe::StarterCommand::UpdateAlarmList:
	{
		size_t size = outbuf.command.starterCommand.alarmList.size() * ( String::MAX_CHARS_SYSTEMTIME + MAX_CODE + wcslen(Network::SEP) + 1);
		size++; //for \0
		wchar_t *alarm = new wchar_t[size];
		if(!outbuf.command.starterCommand.alarmList.getAllActiveAlarm(alarm, size, Network::SEP))
      logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"%s Active Alarm List: Check the buffer lenght possible buffer overrun", outbuf.command.starterCommand.alarmList.getMachineGeneric());
		else
		{
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"%s Active Alarm List %s", outbuf.command.starterCommand.alarmList.getMachineGeneric(),alarm);
			o_socket.Send(Network::MSG_RTC_SWI,L"update.%s(%s)",outbuf.command.starterCommand.alarmList.getMachineGeneric(),alarm);
		}
		delete[] alarm;
		return true;
		break;
	}

	case NamedPipe::StarterCommand::Error:
		if(outbuf.isToView())
			o_socket.Send(Network::MSG_DB_REF, L"pageManager._SetMessage(s[%s], b[true])", outbuf.getResponse());
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"Impossible to execute command: %s",outbuf.getResponse());
		return true;
		break;

	case NamedPipe::DoubleBackerCommand::InvalidCommand:
		
		break;
	default:
		return false;
	}
	return false;
}

bool C_StarterCommands::ManageCommand(String::StrType page, String::StrType StarterStringCommand)
{
	//check if the command is an increase/decrease
	if(ManageButton(page, StarterStringCommand))
		return true;

	NamedPipe::CommandPipe commandPipe;	
	commandPipe.addressee = NamedPipe::CommandPipe::Starter;
	String::StrType theStarterCommand = StarterStringCommand.extractCommand();
	if(theStarterCommand.isEmpty()) return false;
/*
	//Esempio
	if (theStarterCommand.CompareNoCase(L" ??? "))
	{
		commandPipe.command.starterCommand.cmd = NamedPipe::StarterCommand:: ???;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
*/

	logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Starter wrong command: %s", theStarterCommand.getPtr());
	return false;

}

bool C_StarterCommands::getPipeCmd(String::StrType command, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop)
{
	commandPipeStart.addressee = NamedPipe::CommandPipe::Starter;
	commandPipeStop.addressee = NamedPipe::CommandPipe::Starter;
/*
// esempio
	if (command.CompareNoCase(L"???"))
	{
		commandPipeStart.command.starterCommand.cmd = NamedPipe::StarterCommand:: ?? ;
		commandPipeStop.command.starterCommand.cmd = NamedPipe::StarterCommand:: ??;
		return true;
	};
*/
	return false;
}