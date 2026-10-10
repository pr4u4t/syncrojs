#include ".\C_ExternWECCommands.h"

#include "..\C_RTCManager.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;

C_ExternWECCommands::C_ExternWECCommands(void)
{
}

C_ExternWECCommands::~C_ExternWECCommands(void)
{
}

bool C_ExternWECCommands::CreateCommand(NamedPipe::CommandPipe& outbuf)
{
	switch ( outbuf.command.externWECCommand.cmd )
	{
	case NamedPipe::ExternWECCommand::UpdateAlarmList:
	{
		size_t size = outbuf.command.externWECCommand.alarmList.size() * ( String::MAX_CHARS_SYSTEMTIME + MAX_CODE + wcslen(Network::SEP) + 1);
		size++; //for \0
		wchar_t *alarm = new wchar_t[size];
		if(!outbuf.command.externWECCommand.alarmList.getAllActiveAlarm(alarm, size, Network::SEP))
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Extern WEC Active Alarm List: Check the buffer lenght possible buffer overrun");
		else
		{
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"Extern WEC Active Alarm List %s",alarm);
			o_socket.Send(Network::MSG_RTC_SWI,L"update.%s(%s)",outbuf.command.externWECCommand.alarmList.getMachineGeneric(),alarm);
		}
		delete[] alarm;
		return true;
		break;
	}
	case NamedPipe::ExternWECCommand::Error:
		if(outbuf.isToView())
			o_socket.Send(Network::MSG_DB_REF, L"pageManager._SetMessage(s[%s], b[true])", outbuf.getResponse());
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"Impossible to execute command: %s",outbuf.getResponse());
		return true;
		break;

	case NamedPipe::ExternWECCommand::InvalidCommand:
		break;
	default:
		return false;
	}
	return false;
}


bool C_ExternWECCommands::ManageCommand(String::StrType page, String::StrType ExternWECCommand)
{
	NamedPipe::CommandPipe commandPipe;
	commandPipe.Clean();
	commandPipe.addressee=NamedPipe::CommandPipe::ExternWEC;

	String::StrType theExternWEC = ExternWECCommand.extractCommand();
	if (theExternWEC.isEmpty())
		return false;

	//SendCurrentProgram
	if (theExternWEC.CompareNoCase(L"SendCurrentProgram"))
	{
		commandPipe.command.externWECCommand.cmd = NamedPipe::ExternWECCommand::SendCurrentProgram;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//SendNextProgram
	if (theExternWEC.CompareNoCase(L"SendNextProgram"))
	{
		commandPipe.command.externWECCommand.cmd = NamedPipe::ExternWECCommand::SendNextProgram;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//SendNextP1Program
	if (theExternWEC.CompareNoCase(L"SendNextPlusOneProgram"))
	{
		commandPipe.command.externWECCommand.cmd = NamedPipe::ExternWECCommand::SendNextPlusOneProgram;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//WetShiftRequest
	if (theExternWEC.CompareNoCase(L"WetShiftRequest"))
	{
		commandPipe.command.externWECCommand.cmd = NamedPipe::ExternWECCommand::WetShiftRequest;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};

	logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Extern WEC wrong command: %s", theExternWEC.getPtr());
	return false;
}

bool C_ExternWECCommands::getPipeCmd(String::StrType command, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop)
{
	commandPipeStart.addressee = NamedPipe::CommandPipe::ExternWEC;
	commandPipeStop.addressee = NamedPipe::CommandPipe::ExternWEC;

	return false;
}