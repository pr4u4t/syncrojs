#include ".\C_SplicerCommands.h"

#include "..\C_RTCManager.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;

C_SplicerCommands::C_SplicerCommands(const int theSplicerNumber)
{
	this->splicerNumber = theSplicerNumber;
}

C_SplicerCommands::~C_SplicerCommands(void)
{
}

bool C_SplicerCommands::CreateCommand(NamedPipe::CommandPipe& outbuf)
{
	switch ( outbuf.command.splicerCommand.cmd )
	{
	case NamedPipe::SplicerCommand::SendConfiguration:
		return CreateConfigCommand(outbuf);
		break;
	case NamedPipe::SplicerCommand::UpdateAlarmList:
	{
		size_t size = outbuf.command.glueUnitCommand.alarmList.size() * ( String::MAX_CHARS_SYSTEMTIME + MAX_CODE + wcslen(Network::SEP) + 1);
		size++; //for \0
		wchar_t *alarm = new wchar_t[size];
		if(!outbuf.command.splicerCommand.alarmList.getAllActiveAlarm(alarm, size, Network::SEP))
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Splicer%d Active Alarm List: Check the buffer lenght possible buffer overrun", this->splicerNumber);
		else
		{
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"Splicer%d Active Alarm List %s",this->splicerNumber, alarm);
			o_socket.Send(Network::MSG_RTC_SWI,L"update.%s(%s)",outbuf.command.splicerCommand.alarmList.getMachineGeneric(),alarm);
		}
		delete[] alarm;
		return true;
		break;
	}

	case NamedPipe::SplicerCommand::Error:
		if(outbuf.isToView())
			o_socket.Send(Network::MSG_DB_REF, L"pageManager._SetMessage(s[%s], b[true])", outbuf.getResponse());
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"Impossible to execute command: %s",outbuf.getResponse());
		return true;
		break;

	case NamedPipe::GlueUnitCommand::InvalidCommand:
		break;
	default:
		return false;
	}
	return false;
}

bool C_SplicerCommands::CreateConfigCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[String::MAX_CHARS_CONFIG_MACHINE]=L"";
	outbuf.command.splicerCommand.config.Serialize(cfg, Network::SEP, this->splicerNumber);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.Splicer%dConfig.Config(%s)", this->splicerNumber, cfg);
	
	return true;
}

bool C_SplicerCommands::ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.splicerCommand.cmd = NamedPipe::SplicerCommand::SendConfiguration;
	outbuf.command.splicerCommand.config.Deserialize(stringConfig, Network::SEP);
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}

bool C_SplicerCommands::ManageCommand(String::StrType page, String::StrType theSplicerStringCommand)
{
	//check if the command is an increase/decrease
	if(ManageButton(page, theSplicerStringCommand))
		return true;

	NamedPipe::CommandPipe commandPipe;	
	commandPipe.Clean();

	switch(this->splicerNumber)
	{
	case 1:
		commandPipe.addressee = NamedPipe::CommandPipe::Splicer1;
		break;
	case 2:
		commandPipe.addressee = NamedPipe::CommandPipe::Splicer2;
		break;
	case 3:
		commandPipe.addressee = NamedPipe::CommandPipe::Splicer3;
		break;
	case 4:
		commandPipe.addressee = NamedPipe::CommandPipe::Splicer4;
		break;
	case 5:
		commandPipe.addressee = NamedPipe::CommandPipe::Splicer5;
		break;
	case 6:
		commandPipe.addressee = NamedPipe::CommandPipe::Splicer6;
		break;
	case 7:
		commandPipe.addressee = NamedPipe::CommandPipe::Splicer7;
		break;
	}
	
	String::StrType theSplicerCommand = theSplicerStringCommand.extractCommand();
	if(theSplicerCommand.isEmpty()) 
		return false;

	//SendConfiguration
	if(theSplicerCommand.CompareNoCase(L"SendConfiguration"))
		return C_SplicerCommands::ParseConfigCommand(theSplicerStringCommand, commandPipe);

	//RollChange
	if(theSplicerCommand.CompareNoCase(L"RollChange"))
	{	
		commandPipe.command.splicerCommand.cmd = NamedPipe::SplicerCommand::RollChange;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		logger.Info(__FUNCTION__, L"RollChange splicer %d", this->splicerNumber);
		return true;
	}
	//ToggleAutoMan
	if(theSplicerCommand.CompareNoCase(L"ToggleAutoMan"))
	{	
		commandPipe.command.splicerCommand.cmd = NamedPipe::SplicerCommand::ToggleAutoMan;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		logger.Info(__FUNCTION__, L"ToggleAutoMan splicer %d", this->splicerNumber);
		return true;
	}
	//ToggleConfirmSplice
	if(theSplicerCommand.CompareNoCase(L"ToggleConfirmSplice"))
	{	
		commandPipe.command.splicerCommand.cmd = NamedPipe::SplicerCommand::ToggleConfirmSplice;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		logger.Info(__FUNCTION__, L"ToggleConfirmSplice splicer %d", this->splicerNumber);
		return true;
	};
	//EditCoreDiameter
	if(theSplicerCommand.CompareNoCase(L"EditCoreDiameter"))
	{	
		commandPipe.command.splicerCommand.cmd = NamedPipe::SplicerCommand::EditCoreDiameter;
		commandPipe.command.splicerCommand.intData = theSplicerStringCommand.ToInt();
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		logger.Info(__FUNCTION__, L"EditCoreDiameter splicer %d", this->splicerNumber);
		return true;
	};
	//EditSpliceSpeed
	if(theSplicerCommand.CompareNoCase(L"EditSpliceSpeed"))
	{	
		commandPipe.command.splicerCommand.cmd = NamedPipe::SplicerCommand::EditSpliceSpeed;
		commandPipe.command.splicerCommand.intData = theSplicerStringCommand.ToInt();
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		logger.Info(__FUNCTION__, L"EditSpliceSpeed splicer %d", this->splicerNumber);
		return true;
	};

	//Paper Tension Local/Remote Toggle
	if (theSplicerCommand.CompareNoCase(L"LocalRemotePaperTension.Toggle"))
	{
		commandPipe.command.splicerCommand.cmd = NamedPipe::SplicerCommand::TogglePaperTensionLocRem;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		logger.Info(__FUNCTION__, L"TogglePaperTensionLocRem splicer %d", this->splicerNumber);
		return true;
	};

	//Paper Tension Manual/Automatic Toggle
	if (theSplicerCommand.CompareNoCase(L"AutoManualPaperTension.Toggle"))
	{
		commandPipe.command.splicerCommand.cmd = NamedPipe::SplicerCommand::TogglePaperTensionAutoMan;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		logger.Info(__FUNCTION__, L"TogglePaperTensionAutoMan splicer %d", this->splicerNumber);
		return true;
	};


	//Paper Tension GoToSetPointPaperTension
	if( theSplicerCommand.CompareNoCase(L"PaperTension.GoToSetPointPaperTension"))
	{
		commandPipe.command.splicerCommand.setPoint = theSplicerStringCommand.ToDouble();
		commandPipe.command.splicerCommand.cmd = NamedPipe::SplicerCommand::GoToSetPointPaperTensionSplicer;
		commandPipe.command.splicerCommand.paperTensionNumber = PaperTensionNumber(this->splicerNumber);
		logger.Info(__FUNCTION__, L"GoToSetPointPaperTension splicer %d", this->splicerNumber);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}


	logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Splicer%d wrong command: %s", this->splicerNumber, theSplicerCommand.getPtr());
	return false;
}

bool C_SplicerCommands::getPipeCmd(String::StrType command, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop)
{
	switch(this->splicerNumber)
	{
	case 1:
		commandPipeStart.addressee = NamedPipe::CommandPipe::Splicer1;
		commandPipeStop.addressee = NamedPipe::CommandPipe::Splicer1;
		break;
	case 2:
		commandPipeStart.addressee = NamedPipe::CommandPipe::Splicer2;
		commandPipeStop.addressee = NamedPipe::CommandPipe::Splicer2;
		break;
	case 3:
		commandPipeStart.addressee = NamedPipe::CommandPipe::Splicer3;
		commandPipeStop.addressee = NamedPipe::CommandPipe::Splicer3;
		break;
	case 4:
		commandPipeStart.addressee = NamedPipe::CommandPipe::Splicer4;
		commandPipeStop.addressee = NamedPipe::CommandPipe::Splicer4;
		break;
	case 5:
		commandPipeStart.addressee = NamedPipe::CommandPipe::Splicer5;
		commandPipeStop.addressee = NamedPipe::CommandPipe::Splicer5;
		break;
	case 6:
		commandPipeStart.addressee = NamedPipe::CommandPipe::Splicer6;
		commandPipeStop.addressee = NamedPipe::CommandPipe::Splicer6;
		break;
	case 7:
		commandPipeStart.addressee = NamedPipe::CommandPipe::Splicer7;
		commandPipeStop.addressee = NamedPipe::CommandPipe::Splicer7;
		break;
	}
	

	return false;
}