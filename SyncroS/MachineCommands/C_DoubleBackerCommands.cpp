#include ".\C_DoubleBackerCommands.h"

#include "..\C_RTCManager.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;

C_DoubleBackerCommands::C_DoubleBackerCommands(void)
{
}

C_DoubleBackerCommands::~C_DoubleBackerCommands(void)
{
}

bool C_DoubleBackerCommands::CreateCommand(NamedPipe::CommandPipe& outbuf)
{
	switch ( outbuf.command.doubleBackerCommand.cmd )
	{
	case NamedPipe::DoubleBackerCommand::SendConfiguration:
		return CreateConfigCommand(outbuf);
		break;
	case NamedPipe::DoubleBackerCommand::UpdateAlarmList:
	{
		size_t size = outbuf.command.doubleBackerCommand.alarmList.size() * ( String::MAX_CHARS_SYSTEMTIME + MAX_CODE + wcslen(Network::SEP) + 1);
		size++; //for \0
		wchar_t *alarm = new wchar_t[size];
		if(!outbuf.command.doubleBackerCommand.alarmList.getAllActiveAlarm(alarm, size, Network::SEP))
      logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"DoubleBacker Active Alarm List: Check the buffer lenght possible buffer overrun");
		else
		{
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"DoubleBacker Active Alarm List %s",alarm);
			o_socket.Send(Network::MSG_RTC_SWI,L"update.%s(%s)",outbuf.command.doubleBackerCommand.alarmList.getMachineGeneric(),alarm);
		}
		delete[] alarm;
		return true;
		break;
	}

	case NamedPipe::DoubleBackerCommand::Error:
		if(outbuf.isToView())
			o_socket.Send(Network::MSG_PAG_REF, L"pageManager._SetMessage(s[%s], b[true])", outbuf.getResponse());
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

bool C_DoubleBackerCommands::CreateConfigCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[String::MAX_CHARS_CONFIG_MACHINE]=L"";
	outbuf.command.doubleBackerCommand.config.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.DoubleBackerConfig.Config(%s)", cfg);
	
	return true;
}

bool C_DoubleBackerCommands::ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::SendConfiguration;
	outbuf.command.doubleBackerCommand.config.Deserialize(stringConfig, Network::SEP);
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}

bool C_DoubleBackerCommands::ManageCommand(String::StrType page, String::StrType DoubleBackerStringCommand)
{
	//check if the command is an increase/decrease
	if(ManageButton(page, DoubleBackerStringCommand))
		return true;

	NamedPipe::CommandPipe commandPipe;	
	commandPipe.addressee = NamedPipe::CommandPipe::DoubleBacker;
	String::StrType theDoubleBackerCommand = DoubleBackerStringCommand.extractCommand();
	if(theDoubleBackerCommand.isEmpty()) 
		return false;

	//SendConfiguration
	if(theDoubleBackerCommand.CompareNoCase(L"SendConfiguration"))
		return C_DoubleBackerCommands::ParseConfigCommand(DoubleBackerStringCommand, commandPipe);

	//SendCurrentOrder
	if (theDoubleBackerCommand.CompareNoCase(L"SendCurrentOrder"))
	{
		commandPipe.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::SendCurrentOrder;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//SendNextOrder
	if (theDoubleBackerCommand.CompareNoCase(L"SendNextOrder"))
	{
		commandPipe.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::SendNextOrder;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//SendNextP1Order
	if (theDoubleBackerCommand.CompareNoCase(L"SendNextP1Order"))
	{
		commandPipe.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::SendNextP1Order;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};

	//Wrap Arm Pre-Heater 31
	if( theDoubleBackerCommand.CompareNoCase(L"WrapArmPreHeater31.GoToSetPointWrapArmPreHeater"))
	{
		commandPipe.command.doubleBackerCommand.setPoint = DoubleBackerStringCommand.ToDouble();
		commandPipe.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.doubleBackerCommand.wrapArmNumber = WrapArmNumber(S1_3);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater 41
	if( theDoubleBackerCommand.CompareNoCase(L"WrapArmPreHeater41.GoToSetPointWrapArmPreHeater"))
	{
		commandPipe.command.doubleBackerCommand.setPoint = DoubleBackerStringCommand.ToDouble();
		commandPipe.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.doubleBackerCommand.wrapArmNumber = WrapArmNumber(S1_4);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Paper Tension Bridge 1
	if( theDoubleBackerCommand.CompareNoCase(L"PaperTensionBridge21.GoToSetPointPaperTension"))
	{
		commandPipe.command.doubleBackerCommand.setPoint = DoubleBackerStringCommand.ToDouble();
		commandPipe.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::GoToSetPointBridgePaperTension;
		commandPipe.command.doubleBackerCommand.paperTensionNumber = PaperTensionNumber(B1_PT);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Paper Tension Bridge 2
	if( theDoubleBackerCommand.CompareNoCase(L"PaperTensionBridge22.GoToSetPointPaperTension"))
	{
		commandPipe.command.doubleBackerCommand.setPoint = DoubleBackerStringCommand.ToDouble();
		commandPipe.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::GoToSetPointBridgePaperTension;
		commandPipe.command.doubleBackerCommand.paperTensionNumber = PaperTensionNumber(B2_PT);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Paper Tension Bridge 3
	if( theDoubleBackerCommand.CompareNoCase(L"PaperTensionBridge23.GoToSetPointPaperTension"))
	{
		commandPipe.command.doubleBackerCommand.setPoint = DoubleBackerStringCommand.ToDouble();
		commandPipe.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::GoToSetPointBridgePaperTension;
		commandPipe.command.doubleBackerCommand.paperTensionNumber = PaperTensionNumber(B3_PT);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

  //RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea         
	//Steam WrapArm 11
	if( theDoubleBackerCommand.CompareNoCase(L"WrapArmSteam11.GoToSetPointWrapArmSteam"))
	{
		commandPipe.command.doubleBackerCommand.setPoint = DoubleBackerStringCommand.ToDouble();
    commandPipe.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::GoToSetpointWrapArmSteam;
		commandPipe.command.doubleBackerCommand.wrapArmSteamNumber = WrapArmSteamNumber(S1_ST_1);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	//Steam WrapArm 1
	if( theDoubleBackerCommand.CompareNoCase(L"WrapArmSteam1.GoToSetPointWrapArmSteam"))
	{
		commandPipe.command.doubleBackerCommand.setPoint = DoubleBackerStringCommand.ToDouble();
    commandPipe.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::GoToSetpointWrapArmSteam;
		commandPipe.command.doubleBackerCommand.wrapArmSteamNumber = WrapArmSteamNumber(B1_ST_PH);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	//Steam WrapArm 2
	if( theDoubleBackerCommand.CompareNoCase(L"WrapArmSteam2.GoToSetPointWrapArmSteam"))
	{
		commandPipe.command.doubleBackerCommand.setPoint = DoubleBackerStringCommand.ToDouble();
    commandPipe.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::GoToSetpointWrapArmSteam;
		commandPipe.command.doubleBackerCommand.wrapArmSteamNumber = WrapArmSteamNumber(B2_ST_PH);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	//Steam WrapArm 10
	if( theDoubleBackerCommand.CompareNoCase(L"WrapArmSteam10.GoToSetPointWrapArmSteam"))
	{
		commandPipe.command.doubleBackerCommand.setPoint = DoubleBackerStringCommand.ToDouble();
    commandPipe.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::GoToSetpointWrapArmSteam;
		commandPipe.command.doubleBackerCommand.wrapArmSteamNumber = WrapArmSteamNumber(B1_ST_PHO);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	//Steam WrapArm 20
	if( theDoubleBackerCommand.CompareNoCase(L"WrapArmSteam20.GoToSetPointWrapArmSteam"))
	{
		commandPipe.command.doubleBackerCommand.setPoint = DoubleBackerStringCommand.ToDouble();
    commandPipe.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::GoToSetpointWrapArmSteam;
		commandPipe.command.doubleBackerCommand.wrapArmSteamNumber = WrapArmSteamNumber(B2_ST_PHO);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	//Steam WrapArm 3
	if( theDoubleBackerCommand.CompareNoCase(L"WrapArmSteam3.GoToSetPointWrapArmSteam"))
	{
		commandPipe.command.doubleBackerCommand.setPoint = DoubleBackerStringCommand.ToDouble();
    commandPipe.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::GoToSetpointWrapArmSteam;
		commandPipe.command.doubleBackerCommand.wrapArmSteamNumber = WrapArmSteamNumber(B3_ST_PH);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Double Backer wrong command: %s", theDoubleBackerCommand.getPtr());
	return false;

}

bool C_DoubleBackerCommands::getPipeCmd(String::StrType command, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop)
{
	commandPipeStart.addressee = NamedPipe::CommandPipe::DoubleBacker;
	commandPipeStop.addressee = NamedPipe::CommandPipe::DoubleBacker;

	//Wrap Arm DoubleBacker 31
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater31.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.wrapArmNumber = S1_3;
		commandPipeStop.command.doubleBackerCommand.wrapArmNumber = S1_3;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater31.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.wrapArmNumber = S1_3;
		commandPipeStop.command.doubleBackerCommand.wrapArmNumber = S1_3;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater31.Increase"))
	{
		commandPipeStart.command.doubleBackerCommand.wrapArmNumber = S1_3;
		commandPipeStop.command.doubleBackerCommand.wrapArmNumber = S1_3;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater31.Decrease"))
	{
		commandPipeStart.command.doubleBackerCommand.wrapArmNumber = S1_3;
		commandPipeStop.command.doubleBackerCommand.wrapArmNumber = S1_3;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm DoubleBacker 41
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater41.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.wrapArmNumber = S1_4;
		commandPipeStop.command.doubleBackerCommand.wrapArmNumber = S1_4;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater41.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.wrapArmNumber = S1_4;
		commandPipeStop.command.doubleBackerCommand.wrapArmNumber = S1_4;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater41.Increase"))
	{
		commandPipeStart.command.doubleBackerCommand.wrapArmNumber = S1_4;
		commandPipeStop.command.doubleBackerCommand.wrapArmNumber = S1_4;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater41.Decrease"))
	{
		commandPipeStart.command.doubleBackerCommand.wrapArmNumber = S1_4;
		commandPipeStop.command.doubleBackerCommand.wrapArmNumber = S1_4;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopWrapArmPreHeater;
		return true;
	}

	//ScrewJack 1 DoubleBacker
	if (command.CompareNoCase(L"LocalRemoteScrewJack1.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 1;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 1;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleLocalRemoteScrewJacks;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleLocalRemoteScrewJacks;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualScrewJack1.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 1;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 1;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleAutoManualScrewJacks;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleAutoManualScrewJacks;
		return true;
	};
	if (command.CompareNoCase(L"ScrewJack1.Increase"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 1;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 1;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartIncreasePercScrewJackPressure;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopPercScrewJackPressure;
		return true;
	}
	if (command.CompareNoCase(L"ScrewJack1.Decrease"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 1;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 1;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartDecreasePercScrewJackPressure;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopPercScrewJackPressure;
		return true;
	}

	//ScrewJack 2 DoubleBacker
	if (command.CompareNoCase(L"LocalRemoteScrewJack2.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 2;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 2;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleLocalRemoteScrewJacks;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleLocalRemoteScrewJacks;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualScrewJack2.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 2;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 2;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleAutoManualScrewJacks;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleAutoManualScrewJacks;
		return true;
	};
	if (command.CompareNoCase(L"ScrewJack2.Increase"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 2;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 2;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartIncreasePercScrewJackPressure;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopPercScrewJackPressure;
		return true;
	}
	if (command.CompareNoCase(L"ScrewJack2.Decrease"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 2;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 2;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartDecreasePercScrewJackPressure;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopPercScrewJackPressure;
		return true;
	}

	//ScrewJack 3 DoubleBacker
	if (command.CompareNoCase(L"LocalRemoteScrewJack3.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 3;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 3;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleLocalRemoteScrewJacks;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleLocalRemoteScrewJacks;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualScrewJack3.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 3;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 3;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleAutoManualScrewJacks;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleAutoManualScrewJacks;
		return true;
	};
	if (command.CompareNoCase(L"ScrewJack3.Increase"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 3;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 3;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartIncreasePercScrewJackPressure;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopPercScrewJackPressure;
		return true;
	}
	if (command.CompareNoCase(L"ScrewJack3.Decrease"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 3;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 3;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartDecreasePercScrewJackPressure;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopPercScrewJackPressure;
		return true;
	}

	//ScrewJack 4 DoubleBacker
	if (command.CompareNoCase(L"LocalRemoteScrewJack4.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 4;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 4;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleLocalRemoteScrewJacks;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleLocalRemoteScrewJacks;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualScrewJack4.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 4;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 4;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleAutoManualScrewJacks;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleAutoManualScrewJacks;
		return true;
	};
	if (command.CompareNoCase(L"ScrewJack4.Increase"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 4;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 4;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartIncreasePercScrewJackPressure;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopPercScrewJackPressure;
		return true;
	}
	if (command.CompareNoCase(L"ScrewJack4.Decrease"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 4;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 4;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartDecreasePercScrewJackPressure;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopPercScrewJackPressure;
		return true;
	}

	//ScrewJacks DoubleBacker
	if (command.CompareNoCase(L"LocalRemoteScrewJacks.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 0;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 0;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleLocalRemoteScrewJacks;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleLocalRemoteScrewJacks;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualScrewJacks.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 0;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 0;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleAutoManualScrewJacks;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleAutoManualScrewJacks;
		return true;
	};
	if (command.CompareNoCase(L"ScrewJacks.Increase"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 0;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 0;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartIncreasePercScrewJackPressure;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopPercScrewJackPressure;
		return true;
	}
	if (command.CompareNoCase(L"ScrewJacks.Decrease"))
	{
		commandPipeStart.command.doubleBackerCommand.jackNumber = 0;
		commandPipeStop.command.doubleBackerCommand.jackNumber = 0;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartDecreasePercScrewJackPressure;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopPercScrewJackPressure;
		return true;
	}

	//Cold Section DoubleBacker
	if (command.CompareNoCase(L"LocalRemoteColdSection.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleLocalRemoteColdSection;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleLocalRemoteColdSection;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualColdSection.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleAutoManualColdSection;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleAutoManualColdSection;
		return true;
	};
	if (command.CompareNoCase(L"ColdSection.Increase"))
	{
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartIncreaseColdSectionPressure;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopColdSectionPressure;
		return true;
	}
	if (command.CompareNoCase(L"ColdSection.Decrease"))
	{
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartDecreaseColdSectionPressure;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopColdSectionPressure;
		return true;
	}

	//Steam pressure 1 DoubleBacker
	if (command.CompareNoCase(L"LocalRemoteSteamPressureGroup1.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.steamPressureGroupNumber = 1;
		commandPipeStop.command.doubleBackerCommand.steamPressureGroupNumber = 1;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleLocalRemoteSteamPressureGroup;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleLocalRemoteSteamPressureGroup;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualSteamPressureGroup1.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.steamPressureGroupNumber = 1;
		commandPipeStop.command.doubleBackerCommand.steamPressureGroupNumber = 1;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleAutoManualSteamPressureGroup;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleAutoManualSteamPressureGroup;
		return true;
	};
	if (command.CompareNoCase(L"SteamPressureGroup1.Increase"))
	{
		commandPipeStart.command.doubleBackerCommand.steamPressureGroupNumber = 1;
		commandPipeStop.command.doubleBackerCommand.steamPressureGroupNumber = 1;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartIncreaseRequestedSteamPressureGroup;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopRequestedSteamPressureGroup;
		return true;
	}
	if (command.CompareNoCase(L"SteamPressureGroup1.Decrease"))
	{
		commandPipeStart.command.doubleBackerCommand.steamPressureGroupNumber = 1;
		commandPipeStop.command.doubleBackerCommand.steamPressureGroupNumber = 1;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartDecreaseRequestedSteamPressureGroup;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopRequestedSteamPressureGroup;
		return true;
	}

	//Steam pressure 2 DoubleBacker
	if (command.CompareNoCase(L"LocalRemoteSteamPressureGroup2.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.steamPressureGroupNumber = 2;
		commandPipeStop.command.doubleBackerCommand.steamPressureGroupNumber = 2;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleLocalRemoteSteamPressureGroup;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleLocalRemoteSteamPressureGroup;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualSteamPressureGroup2.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.steamPressureGroupNumber = 2;
		commandPipeStop.command.doubleBackerCommand.steamPressureGroupNumber = 2;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleAutoManualSteamPressureGroup;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleAutoManualSteamPressureGroup;
		return true;
	};
	if (command.CompareNoCase(L"SteamPressureGroup2.Increase"))
	{
		commandPipeStart.command.doubleBackerCommand.steamPressureGroupNumber = 2;
		commandPipeStop.command.doubleBackerCommand.steamPressureGroupNumber = 2;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartIncreaseRequestedSteamPressureGroup;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopRequestedSteamPressureGroup;
		return true;
	}
	if (command.CompareNoCase(L"SteamPressureGroup2.Decrease"))
	{
		commandPipeStart.command.doubleBackerCommand.steamPressureGroupNumber = 2;
		commandPipeStop.command.doubleBackerCommand.steamPressureGroupNumber = 2;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartDecreaseRequestedSteamPressureGroup;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopRequestedSteamPressureGroup;
		return true;
	}

	//Steam pressure 3 DoubleBacker
	if (command.CompareNoCase(L"LocalRemoteSteamPressureGroup3.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.steamPressureGroupNumber = 3;
		commandPipeStop.command.doubleBackerCommand.steamPressureGroupNumber = 3;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleLocalRemoteSteamPressureGroup;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleLocalRemoteSteamPressureGroup;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualSteamPressureGroup3.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.steamPressureGroupNumber = 3;
		commandPipeStop.command.doubleBackerCommand.steamPressureGroupNumber = 3;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleAutoManualSteamPressureGroup;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleAutoManualSteamPressureGroup;
		return true;
	};
	if (command.CompareNoCase(L"SteamPressureGroup3.Increase"))
	{
		commandPipeStart.command.doubleBackerCommand.steamPressureGroupNumber = 3;
		commandPipeStop.command.doubleBackerCommand.steamPressureGroupNumber = 3;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartIncreaseRequestedSteamPressureGroup;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopRequestedSteamPressureGroup;
		return true;
	}
	if (command.CompareNoCase(L"SteamPressureGroup3.Decrease"))
	{
		commandPipeStart.command.doubleBackerCommand.steamPressureGroupNumber = 3;
		commandPipeStop.command.doubleBackerCommand.steamPressureGroupNumber = 3;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartDecreaseRequestedSteamPressureGroup;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopRequestedSteamPressureGroup;
		return true;
	}  

  //RFAZZI_FI02/03/2010_1. Aggiunta gestione sezione 0 vapore della macchina 
	//Steam pressure 0 DoubleBacker
	if (command.CompareNoCase(L"LocalRemoteSteamPressureGroup0.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.steamPressureGroupNumber = 0;
		commandPipeStop.command.doubleBackerCommand.steamPressureGroupNumber = 0;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleLocalRemoteSteamPressureGroup;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleLocalRemoteSteamPressureGroup;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualSteamPressureGroup0.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.steamPressureGroupNumber = 0;
		commandPipeStop.command.doubleBackerCommand.steamPressureGroupNumber = 0;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleAutoManualSteamPressureGroup;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleAutoManualSteamPressureGroup;
		return true;
	};
	if (command.CompareNoCase(L"SteamPressureGroup0.Increase"))
	{
		commandPipeStart.command.doubleBackerCommand.steamPressureGroupNumber = 0;
		commandPipeStop.command.doubleBackerCommand.steamPressureGroupNumber = 0;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartIncreaseRequestedSteamPressureGroup;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopRequestedSteamPressureGroup;
		return true;
	}
	if (command.CompareNoCase(L"SteamPressureGroup0.Decrease"))
	{
		commandPipeStart.command.doubleBackerCommand.steamPressureGroupNumber = 0;
		commandPipeStop.command.doubleBackerCommand.steamPressureGroupNumber = 0;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartDecreaseRequestedSteamPressureGroup;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopRequestedSteamPressureGroup;
		return true;
	}  


  if( command.CompareNoCase(L"ParaffinMachineLiner.Toggle")  )
	{
    commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleEnableParaffinMachineLiner;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleEnableParaffinMachineLiner;
		return true;
	}

	//Paper Tension Bridge 1 
	if (command.CompareNoCase(L"LocalRemotePaperTensionBridge21.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.paperTensionNumber = B1_PT;
		commandPipeStop.command.doubleBackerCommand.paperTensionNumber = B1_PT;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleLocalRemoteBridgePaperTension;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleLocalRemoteBridgePaperTension;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualPaperTensionBridge21.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.paperTensionNumber = B1_PT;
		commandPipeStop.command.doubleBackerCommand.paperTensionNumber = B1_PT;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleAutoManualBridgePaperTension;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleAutoManualBridgePaperTension;
		return true;
	};
	if (command.CompareNoCase(L"PaperTensionBridge21.Increase"))
	{
		commandPipeStart.command.doubleBackerCommand.paperTensionNumber = B1_PT;
		commandPipeStop.command.doubleBackerCommand.paperTensionNumber = B1_PT;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartIncreaseBridgePaperTension;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopBridgePaperTension;
		return true;
	}
	if (command.CompareNoCase(L"PaperTensionBridge21.Decrease"))
	{
		commandPipeStart.command.doubleBackerCommand.paperTensionNumber = B1_PT;
		commandPipeStop.command.doubleBackerCommand.paperTensionNumber = B1_PT;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartDecreaseBridgePaperTension;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopBridgePaperTension;
		return true;
	}

	//Paper Tension Bridge 2
	if (command.CompareNoCase(L"LocalRemotePaperTensionBridge22.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.paperTensionNumber = B2_PT;
		commandPipeStop.command.doubleBackerCommand.paperTensionNumber = B2_PT;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleLocalRemoteBridgePaperTension;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleLocalRemoteBridgePaperTension;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualPaperTensionBridge22.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.paperTensionNumber = B2_PT;
		commandPipeStop.command.doubleBackerCommand.paperTensionNumber = B2_PT;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleAutoManualBridgePaperTension;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleAutoManualBridgePaperTension;
		return true;
	};
	if (command.CompareNoCase(L"PaperTensionBridge22.Increase"))
	{
		commandPipeStart.command.doubleBackerCommand.paperTensionNumber = B2_PT;
		commandPipeStop.command.doubleBackerCommand.paperTensionNumber = B2_PT;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartIncreaseBridgePaperTension;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopBridgePaperTension;
		return true;
	}
	if (command.CompareNoCase(L"PaperTensionBridge22.Decrease"))
	{
		commandPipeStart.command.doubleBackerCommand.paperTensionNumber = B2_PT;
		commandPipeStop.command.doubleBackerCommand.paperTensionNumber = B2_PT;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartDecreaseBridgePaperTension;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopBridgePaperTension;
		return true;
	}

	//Paper Tension Bridge 3
	if (command.CompareNoCase(L"LocalRemotePaperTensionBridge23.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.paperTensionNumber = B3_PT;
		commandPipeStop.command.doubleBackerCommand.paperTensionNumber = B3_PT;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleLocalRemoteBridgePaperTension;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleLocalRemoteBridgePaperTension;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualPaperTensionBridge23.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.paperTensionNumber = B3_PT;
		commandPipeStop.command.doubleBackerCommand.paperTensionNumber = B3_PT;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleAutoManualBridgePaperTension;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleAutoManualBridgePaperTension;
		return true;
	};
	if (command.CompareNoCase(L"PaperTensionBridge23.Increase"))
	{
		commandPipeStart.command.doubleBackerCommand.paperTensionNumber = B3_PT;
		commandPipeStop.command.doubleBackerCommand.paperTensionNumber = B3_PT;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartIncreaseBridgePaperTension;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopBridgePaperTension;
		return true;
	}
	if (command.CompareNoCase(L"PaperTensionBridge23.Decrease"))
	{
		commandPipeStart.command.doubleBackerCommand.paperTensionNumber = B3_PT;
		commandPipeStop.command.doubleBackerCommand.paperTensionNumber = B3_PT;
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartDecreaseBridgePaperTension;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopBridgePaperTension;
		return true;
	}

  //Steam Shower
	if (command.CompareNoCase(L"LocalRemoteSteamShower.Toggle"))
	{
    commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleLocalRemoteSteamShower;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleLocalRemoteSteamShower;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualSteamShower.Toggle"))
	{
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartToggleAutoManualSteamShower;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopToggleAutoManualSteamShower;
		return true;
	};
	if (command.CompareNoCase(L"SteamShower.Increase"))
	{
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartIncreaseSteamShower;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopSteamShower;
		return true;
	}
	if (command.CompareNoCase(L"SteamShower.Decrease"))
	{
		commandPipeStart.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StartDecreaseSteamShower;
		commandPipeStop.command.doubleBackerCommand.cmd = NamedPipe::DoubleBackerCommand::StopSteamShower;
		return true;
	}

	return false;
}