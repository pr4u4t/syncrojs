#include ".\C_AlarmHistoryCommands.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;

C_AlarmHistoryCommands::C_AlarmHistoryCommands(void)
{
}

C_AlarmHistoryCommands::~C_AlarmHistoryCommands(void)
{
}

bool C_AlarmHistoryCommands::ManageCommand(String::StrType AlarmHistoryCommands, NamedPipe::CommandPipe& outbuf)
{
	if(ParseCommand(AlarmHistoryCommands, outbuf))
		return true;
	else
	{
    logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Alarm History wrong command: %s", AlarmHistoryCommands.getPtr());
		return false;
	}
}

bool C_AlarmHistoryCommands::CreateCommand(NamedPipe::CommandPipe& inbuffer)
{
	switch(inbuffer.command.alarmHistoryCommand.cmd)
	{
	case NamedPipe::AlarmHistoryCommand::Clr:
			o_socket.Send(Network::MSG_DB_REF, L"alarmHistory.ClrMachine(%s)",inbuffer.command.alarmHistoryCommand.alarmHistory.getMachineGeneric());
			break;
	case NamedPipe::AlarmHistoryCommand::Add:
			{int numAlarm = inbuffer.command.alarmHistoryCommand.alarmHistory.size();
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"Update Alarm History for %s Num Alarm %d", inbuffer.command.alarmHistoryCommand.alarmHistory.getMachineGeneric(),numAlarm);
			for(int i=0; i<numAlarm; i++)
			{
				wchar_t TimeStrStart[MAX_PATH];
				wchar_t TimeStrStop[MAX_PATH];
				String::SYSTEMTIME2Alpha(inbuffer.command.alarmHistoryCommand.alarmHistory[i].getStartTime(),TimeStrStart,MAX_PATH);
				String::SYSTEMTIME2Alpha(inbuffer.command.alarmHistoryCommand.alarmHistory[i].getFinishTime(),TimeStrStop,MAX_PATH);
        o_socket.Send(Network::MSG_DB_REF, L"alarmHistory.Add(%s%s%s%s%s%s%s)",
								inbuffer.command.alarmHistoryCommand.alarmHistory.getMachineGeneric(),
								Network::SEP,
								inbuffer.command.alarmHistoryCommand.alarmHistory[i].getCode(),
								Network::SEP,
								TimeStrStart,
								Network::SEP,
								TimeStrStop
								);
			}
			break;}
	}

	return true;
}

bool C_AlarmHistoryCommands::ParseCommand(String::StrType theCommand, NamedPipe::CommandPipe& outbuf)
{
	return true;
}


