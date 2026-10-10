#include ".\C_StackerCommands.h"

#include "..\C_RTCManager.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;
extern Logger::C_Logger logConfig;

C_StackerCommands::C_StackerCommands(int theLevel)
{
	this->level = theLevel;
}

C_StackerCommands::C_StackerCommands(void)
{
}

C_StackerCommands::~C_StackerCommands(void)
{
}

bool C_StackerCommands::CreateCommand(NamedPipe::CommandPipe& outbuf)
{
	switch ( outbuf.command.stackerCommand.cmd )
	{
	case NamedPipe::StackerCommand::SendConfiguration:
		return CreateConfigCommand(outbuf);
		break;
	case NamedPipe::StackerCommand::UpdateAlarmList:
	{
		size_t len = outbuf.command.stackerCommand.alarmList.size() * ( String::MAX_CHARS_SYSTEMTIME + MAX_CODE + wcslen(Network::SEP) + 1);
		len++; //for \0
		wchar_t *alarm = new wchar_t[len+1];
		if(!outbuf.command.stackerCommand.alarmList.getAllActiveAlarm(alarm, len, Network::SEP))      
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Stacker Active Alarm List: Check the buffer lenght possible buffer overrun");
		else
		{
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"Stacker Active Alarm List %s",alarm);
			o_socket.Send(Network::MSG_RTC_SWI,L"update.%s(%s)",outbuf.command.stackerCommand.alarmList.getMachineGeneric(),alarm);
		}
		delete[] alarm;
		return true;
		break;
	}
	case NamedPipe::StackerCommand::Error:
		if(outbuf.isToView())
			o_socket.Send(Network::MSG_PAG_REF, L"pageManager._SetMessage(s[%s], b[true])", outbuf.getResponse());
		return false;
		break;
	default:
		return false;
	}
	return false;
}

bool C_StackerCommands::ManageCommand(String::StrType page, String::StrType StackerCommand)
{
	NamedPipe::CommandPipe commandPipe;

	//choose the level
	switch(this->level)
	{
	case 1:
		commandPipe.addressee=NamedPipe::CommandPipe::Stacker1;
		break;
	case 2:
		commandPipe.addressee=NamedPipe::CommandPipe::Stacker2;
		break;
	case 3:
		commandPipe.addressee=NamedPipe::CommandPipe::Stacker3;
		break;
	}

	String::StrType theStackerCommand = StackerCommand.extractCommand();
	if (theStackerCommand.isEmpty())
		return false;

	//SendConfiguration
	if(theStackerCommand.CompareNoCase(L"SendConfiguration"))
		return ParseConfigCommand(StackerCommand, commandPipe);

	//SendCurrentOrder
	if (theStackerCommand.CompareNoCase(L"SendCurrentOrder"))
	{
		commandPipe.command.stackerCommand.cmd = NamedPipe::StackerCommand::SendCurrentOrder;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//SendNextOrder
	if (theStackerCommand.CompareNoCase(L"SendNextOrder"))
	{
		commandPipe.command.stackerCommand.cmd = NamedPipe::StackerCommand::SendNextOrder;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//SendNextP1Order
	if (theStackerCommand.CompareNoCase(L"SendNextP1Order"))
	{
		commandPipe.command.stackerCommand.cmd = NamedPipe::StackerCommand::SendNextP1Order;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	
	//PMASSARO_FI02/05/08 - 5 - Added To Manage Pallet Print With Hardware Signals
	//UpdatePrinterIndexAtCurrent
	if (theStackerCommand.CompareNoCase(L"UpdatePrinterIndexAtCurrent"))
	{
		commandPipe.command.stackerCommand.cmd = NamedPipe::StackerCommand::UpdatePrinterIndexAtCurrent;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//PMASSARO_FI02/05/08 - 5 - Added To Manage Pallet Print With Hardware Signals
	//UpdatePrinterIndexAtPrevious
	if (theStackerCommand.CompareNoCase(L"UpdatePrinterIndexAtPrevious"))
	{
		commandPipe.command.stackerCommand.cmd = NamedPipe::StackerCommand::UpdatePrinterIndexAtPrevious;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//PMASSARO_FI02/05/08 - 5 - Added To Manage Pallet Print With Hardware Signals
	//UpdatePrinterIndexAtSecondPrevious
	if (theStackerCommand.CompareNoCase(L"UpdatePrinterIndexAtSecondPrevious"))
	{
		commandPipe.command.stackerCommand.cmd = NamedPipe::StackerCommand::UpdatePrinterIndexAtSecondPrevious;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
		//PMASSARO_FI02/05/08 - 5 - Added To Manage Pallet Print With Hardware Signals
	//UpdatePrinterIndexAtThirdPrevious
	if (theStackerCommand.CompareNoCase(L"UpdatePrinterIndexAtThirdPrevious"))
	{
		commandPipe.command.stackerCommand.cmd = NamedPipe::StackerCommand::UpdatePrinterIndexAtThirdPrevious;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
		//PMASSARO_FI02/05/08 - 5 - Added To Manage Pallet Print With Hardware Signals
	//UpdatePrinterIndexAtFourthPrevious
	if (theStackerCommand.CompareNoCase(L"UpdatePrinterIndexAtFourthPrevious"))
	{
		commandPipe.command.stackerCommand.cmd = NamedPipe::StackerCommand::UpdatePrinterIndexAtFourthPrevious;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
		//PMASSARO_FI02/05/08 - 5 - Added To Manage Pallet Print With Hardware Signals
	//UpdatePrinterIndexAtFifthPrevious
	if (theStackerCommand.CompareNoCase(L"UpdatePrinterIndexAtFifthPrevious"))
	{
		commandPipe.command.stackerCommand.cmd = NamedPipe::StackerCommand::UpdatePrinterIndexAtFifthPrevious;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};

	if (theStackerCommand.CompareNoCase(L"PrintNowData"))
	{
		commandPipe.command.stackerCommand.cmd = NamedPipe::StackerCommand::PrintNowData;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};

	logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Stacker wrong command: %s", theStackerCommand.getPtr());
	return false;
}

bool C_StackerCommands::getPipeCmd(String::StrType command, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop)
{
	switch(this->level)
	{
	case 1:
		commandPipeStart.addressee = NamedPipe::CommandPipe::Stacker1;
		commandPipeStop.addressee = NamedPipe::CommandPipe::Stacker1;
		break;
	case 2:
		commandPipeStart.addressee = NamedPipe::CommandPipe::Stacker2;
		commandPipeStop.addressee = NamedPipe::CommandPipe::Stacker2;
		break;
	case 3:
		commandPipeStart.addressee = NamedPipe::CommandPipe::Stacker3;
		commandPipeStop.addressee = NamedPipe::CommandPipe::Stacker3;
		break;
	}



	///



	return false;
}

bool C_StackerCommands::CreateConfigCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[String::MAX_CHARS_CONFIG_MACHINE]=L"";
	outbuf.command.stackerCommand.config.Serialize(cfg, Network::SEP, this->level);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.Stacker%dConfig.Config(%s)",level , cfg);
	
	return true;
}

bool C_StackerCommands::ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.stackerCommand.cmd = NamedPipe::StackerCommand::SendConfiguration;
	outbuf.command.stackerCommand.config.Deserialize(stringConfig, Network::SEP);
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}
