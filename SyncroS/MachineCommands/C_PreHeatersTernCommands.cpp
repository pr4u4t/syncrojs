#include ".\C_PreHeatersTernCommands.h"

#include "..\C_RTCManager.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;

C_PreHeatersTernCommands::C_PreHeatersTernCommands(void)
{
}

C_PreHeatersTernCommands::~C_PreHeatersTernCommands(void)
{
}

bool C_PreHeatersTernCommands::CreateCommand(NamedPipe::CommandPipe& outbuf)
{
	switch ( outbuf.command.preHeatersTernCommand.cmd )
	{
	case NamedPipe::PreHeatersTernCommand::SendConfiguration:
		return CreateConfigCommand(outbuf);
		break;
	case NamedPipe::PreHeatersTernCommand::UpdateAlarmList:
	{
		size_t size = outbuf.command.preHeatersTernCommand.alarmList.size() * ( String::MAX_CHARS_SYSTEMTIME + MAX_CODE + wcslen(Network::SEP) + 1);
		size++; //for \0
		wchar_t *alarm = new wchar_t[size];
		if(!outbuf.command.preHeatersTernCommand.alarmList.getAllActiveAlarm(alarm, size, Network::SEP))
      logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Pre-HeatersTern Active Alarm List: Check the buffer lenght possible buffer overrun");
		else
		{
			logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"Pre-HeatersTern Active Alarm List %s",alarm);
			o_socket.Send(Network::MSG_RTC_SWI,L"update.%s(%s)",outbuf.command.preHeatersTernCommand.alarmList.getMachineGeneric(),alarm);
		}
		delete[] alarm;
		return true;
		break;
	}

	case NamedPipe::PreHeatersTernCommand::Error:
		if(outbuf.isToView())
			o_socket.Send(Network::MSG_DB_REF, L"pageManager._SetMessage(s[%s], b[true])", outbuf.getResponse());
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"Impossible to execute command: %s",outbuf.getResponse());
		return true;
		break;

	case NamedPipe::PreHeatersTernCommand::InvalidCommand:
		break;
	default:
		return false;
	}

	return false;
}

bool C_PreHeatersTernCommands::CreateConfigCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[String::MAX_CHARS_CONFIG_MACHINE]=L"";
	outbuf.command.preHeatersTernCommand.config.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.PreHeatersTernConfig.Config(%s)", cfg);
	
	logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"Pre-HeatersTern send Configuration");

	return true;
}

bool C_PreHeatersTernCommands::ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::SendConfiguration;
	outbuf.command.preHeatersTernCommand.config.Deserialize(stringConfig, Network::SEP);
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}

bool C_PreHeatersTernCommands::ManageCommand(String::StrType page, String::StrType theTernStringCommand)
{
	//check if the command is an increase/decrease
	if(ManageButton(page, theTernStringCommand))
		return true;

	NamedPipe::CommandPipe commandPipe;	
	commandPipe.addressee = NamedPipe::CommandPipe::PreHeatersTern;
	String::StrType theTernCommand = theTernStringCommand.extractCommand();
	if(theTernCommand.isEmpty()) 
		return false;

	//SendConfiguration
	if(theTernCommand.CompareNoCase(L"SendConfiguration"))
		return C_PreHeatersTernCommands::ParseConfigCommand(theTernStringCommand, commandPipe);

	//Wrap Arm Pre-Heater 11
	if( theTernCommand.CompareNoCase(L"WrapArmPreHeater11.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.preHeatersTernCommand.setPoint = theTernStringCommand.ToDouble();
		commandPipe.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.preHeatersTernCommand.wrapArmNumber = WrapArmNumber(S1_1);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater 1
	if( theTernCommand.CompareNoCase(L"WrapArmPreHeater1.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.preHeatersTernCommand.setPoint = theTernStringCommand.ToDouble();
		commandPipe.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.preHeatersTernCommand.wrapArmNumber = WrapArmNumber(B1_PH);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater 2
	if( theTernCommand.CompareNoCase(L"WrapArmPreHeater2.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.preHeatersTernCommand.setPoint = theTernStringCommand.ToDouble();
		commandPipe.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.preHeatersTernCommand.wrapArmNumber = WrapArmNumber(B2_PH);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater 3
	if( theTernCommand.CompareNoCase(L"WrapArmPreHeater3.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.preHeatersTernCommand.setPoint = theTernStringCommand.ToDouble();
		commandPipe.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.preHeatersTernCommand.wrapArmNumber = WrapArmNumber(B3_PH);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater 61
	if( theTernCommand.CompareNoCase(L"WrapArmPreHeater61.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.preHeatersTernCommand.setPoint = theTernStringCommand.ToDouble();
		commandPipe.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.preHeatersTernCommand.wrapArmNumber = WrapArmNumber(S1_6);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater 10
	if( theTernCommand.CompareNoCase(L"WrapArmPreHeater10.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.preHeatersTernCommand.setPoint = theTernStringCommand.ToDouble();
		commandPipe.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.preHeatersTernCommand.wrapArmNumber = WrapArmNumber(B1_PHO);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater 20
	if( theTernCommand.CompareNoCase(L"WrapArmPreHeater20.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.preHeatersTernCommand.setPoint = theTernStringCommand.ToDouble();
		commandPipe.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.preHeatersTernCommand.wrapArmNumber = WrapArmNumber(B2_PHO);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater 30
	if( theTernCommand.CompareNoCase(L"WrapArmPreHeater30.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.preHeatersTernCommand.setPoint = theTernStringCommand.ToDouble();
		commandPipe.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.preHeatersTernCommand.wrapArmNumber = WrapArmNumber(B3_PHO);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Pre-Heaters Tern wrong command: %s", theTernCommand.getPtr());
	
	return false;
}

bool C_PreHeatersTernCommands::getPipeCmd(String::StrType command, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop)
{
	commandPipeStart.addressee = NamedPipe::CommandPipe::PreHeatersTern;
	commandPipeStop.addressee = NamedPipe::CommandPipe::PreHeatersTern;

	//Wrap Arm Pre-Heater 11
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater11.Toggle"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = S1_1;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = S1_1;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater11.Toggle"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = S1_1;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = S1_1;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater11.Increase"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = S1_1;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = S1_1;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater11.Decrease"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = S1_1;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = S1_1;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater 1
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater1.Toggle"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B1_PH;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B1_PH;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater1.Toggle"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B1_PH;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B1_PH;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater1.Increase"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B1_PH;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B1_PH;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater1.Decrease"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B1_PH;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B1_PH;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater 2
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater2.Toggle"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B2_PH;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B2_PH;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater2.Toggle"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B2_PH;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B2_PH;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater2.Increase"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B2_PH;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B2_PH;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater2.Decrease"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B2_PH;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B2_PH;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater 3
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater3.Toggle"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B3_PH;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B3_PH;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater3.Toggle"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B3_PH;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B3_PH;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater3.Increase"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B3_PH;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B3_PH;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater3.Decrease"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B3_PH;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B3_PH;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater 61
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater61.Toggle"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = S1_6;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = S1_6;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater61.Toggle"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = S1_6;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = S1_6;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater61.Increase"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = S1_6;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = S1_6;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater61.Decrease"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = S1_6;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = S1_6;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater 10
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater10.Toggle"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B1_PHO;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B1_PHO;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater10.Toggle"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B1_PHO;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B1_PHO;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater10.Increase"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B1_PHO;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B1_PHO;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater10.Decrease"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B1_PHO;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B1_PHO;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater 20
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater20.Toggle"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B2_PHO;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B2_PHO;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater20.Toggle"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B2_PHO;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B2_PHO;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater20.Increase"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B2_PHO;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B2_PHO;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater20.Decrease"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B2_PHO;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B2_PHO;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater 30
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater30.Toggle"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B3_PHO;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B3_PHO;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater30.Toggle"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B3_PHO;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B3_PHO;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater30.Increase"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B3_PHO;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B3_PHO;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater30.Decrease"))
	{
		commandPipeStart.command.preHeatersTernCommand.wrapArmNumber = B3_PHO;
		commandPipeStop.command.preHeatersTernCommand.wrapArmNumber = B3_PHO;
		commandPipeStart.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.preHeatersTernCommand.cmd = NamedPipe::PreHeatersTernCommand::StopWrapArmPreHeater;
		return true;
	}

	return false;
}