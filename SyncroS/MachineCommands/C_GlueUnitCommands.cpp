#include ".\C_GlueUnitCommands.h"

#include "..\C_RTCManager.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;

C_GlueUnitCommands::C_GlueUnitCommands(void)
{
}

C_GlueUnitCommands::~C_GlueUnitCommands(void)
{
}

bool C_GlueUnitCommands::CreateCommand(NamedPipe::CommandPipe& outbuf)
{
	switch ( outbuf.command.glueUnitCommand.cmd )
	{
	case NamedPipe::GlueUnitCommand::SendConfiguration:
		return CreateConfigCommand(outbuf);
		break;
	case NamedPipe::GlueUnitCommand::UpdateAlarmList:
	{
		size_t size = outbuf.command.glueUnitCommand.alarmList.size() * ( String::MAX_CHARS_SYSTEMTIME + MAX_CODE + wcslen(Network::SEP) + 1);
		size++; //for \0
		wchar_t *alarm = new wchar_t[size];
		if(!outbuf.command.glueUnitCommand.alarmList.getAllActiveAlarm(alarm, size, Network::SEP))
      logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"GlueUnit Active Alarm List: Check the buffer lenght possible buffer overrun");
		else
		{
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"GlueUnit Active Alarm List %s",alarm);
			o_socket.Send(Network::MSG_RTC_SWI,L"update.%s(%s)",outbuf.command.glueUnitCommand.alarmList.getMachineGeneric(),alarm);
		}
		delete[] alarm;
		return true;
		break;
	}

	case NamedPipe::GlueUnitCommand::Error:
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

bool C_GlueUnitCommands::CreateConfigCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[String::MAX_CHARS_CONFIG_MACHINE]=L"";
	outbuf.command.glueUnitCommand.config.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.GlueUnitConfig.Config(%s)", cfg);
	
	return true;
}

bool C_GlueUnitCommands::ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::SendConfiguration;
	outbuf.command.glueUnitCommand.config.Deserialize(stringConfig, Network::SEP);
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}

bool C_GlueUnitCommands::ManageCommand(String::StrType page, String::StrType theGlueUnitStringCommand)
{
	//check if the command is an increase/decrease
	if(ManageButton(page, theGlueUnitStringCommand))
		return true;

	NamedPipe::CommandPipe commandPipe;	
	commandPipe.addressee = NamedPipe::CommandPipe::GlueUnit;
	String::StrType theGlueUnitCommand = theGlueUnitStringCommand.extractCommand();
	if(theGlueUnitCommand.isEmpty()) 
		return false;

	//SendConfiguration
	if(theGlueUnitCommand.CompareNoCase(L"SendConfiguration"))
		return C_GlueUnitCommands::ParseConfigCommand(theGlueUnitStringCommand, commandPipe);

	//Wrap Arm Pre-Heater 21
	if( theGlueUnitCommand.CompareNoCase(L"WrapArmPreHeater21.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.glueUnitCommand.setPoint = theGlueUnitStringCommand.ToDouble();
		commandPipe.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.glueUnitCommand.wrapArmNumber = WrapArmNumber(S1_2);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater 4
	if( theGlueUnitCommand.CompareNoCase(L"WrapArmPreHeater4.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.glueUnitCommand.setPoint = theGlueUnitStringCommand.ToDouble();
		commandPipe.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.glueUnitCommand.wrapArmNumber = WrapArmNumber(B1_GU);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater 5
	if( theGlueUnitCommand.CompareNoCase(L"WrapArmPreHeater5.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.glueUnitCommand.setPoint = theGlueUnitStringCommand.ToDouble();
		commandPipe.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.glueUnitCommand.wrapArmNumber = WrapArmNumber(B2_GU);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater 6
	if( theGlueUnitCommand.CompareNoCase(L"WrapArmPreHeater6.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.glueUnitCommand.setPoint = theGlueUnitStringCommand.ToDouble();
		commandPipe.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.glueUnitCommand.wrapArmNumber = WrapArmNumber(B3_GU);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater 51
	if( theGlueUnitCommand.CompareNoCase(L"WrapArmPreHeater51.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.glueUnitCommand.setPoint = theGlueUnitStringCommand.ToDouble();
		commandPipe.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.glueUnitCommand.wrapArmNumber = WrapArmNumber(S1_5);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater 40
	if( theGlueUnitCommand.CompareNoCase(L"WrapArmPreHeater40.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.glueUnitCommand.setPoint = theGlueUnitStringCommand.ToDouble();
		commandPipe.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.glueUnitCommand.wrapArmNumber = WrapArmNumber(B1_GUO);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater 50
	if( theGlueUnitCommand.CompareNoCase(L"WrapArmPreHeater50.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.glueUnitCommand.setPoint = theGlueUnitStringCommand.ToDouble();
		commandPipe.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.glueUnitCommand.wrapArmNumber = WrapArmNumber(B2_GUO);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater 60
	if( theGlueUnitCommand.CompareNoCase(L"WrapArmPreHeater60.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.glueUnitCommand.setPoint = theGlueUnitStringCommand.ToDouble();
		commandPipe.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.glueUnitCommand.wrapArmNumber = WrapArmNumber(B3_GUO);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//GlueGap 1
	if( theGlueUnitCommand.CompareNoCase(L"GlueGap1.GoToSetPointGlueGap")  )
	{
		commandPipe.command.glueUnitCommand.setPoint = theGlueUnitStringCommand.ToDouble();
		commandPipe.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::GoToSetPointGlueGap;
		commandPipe.command.glueUnitCommand.glueGapNumber = GlueGapNumber(G1_GU);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//GlueGap 2
	if( theGlueUnitCommand.CompareNoCase(L"GlueGap2.GoToSetPointGlueGap")  )
	{
		commandPipe.command.glueUnitCommand.setPoint = theGlueUnitStringCommand.ToDouble();
		commandPipe.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::GoToSetPointGlueGap;
		commandPipe.command.glueUnitCommand.glueGapNumber = GlueGapNumber(G2_GU);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//GlueGap 3
	if( theGlueUnitCommand.CompareNoCase(L"GlueGap3.GoToSetPointGlueGap")  )
	{
		commandPipe.command.glueUnitCommand.setPoint = theGlueUnitStringCommand.ToDouble();
		commandPipe.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::GoToSetPointGlueGap;
		commandPipe.command.glueUnitCommand.glueGapNumber = GlueGapNumber(G3_GU);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//GlueGap 4
	if( theGlueUnitCommand.CompareNoCase(L"GlueGap4.GoToSetPointGlueGap")  )
	{
		commandPipe.command.glueUnitCommand.setPoint = theGlueUnitStringCommand.ToDouble();
		commandPipe.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::GoToSetPointGlueGap;
		commandPipe.command.glueUnitCommand.glueGapNumber = GlueGapNumber(G4_GU);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"GlueUnit wrong command: %s", theGlueUnitCommand.getPtr());
	return false;
}

bool C_GlueUnitCommands::getPipeCmd(String::StrType command, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop)
{
	commandPipeStart.addressee = NamedPipe::CommandPipe::GlueUnit;
	commandPipeStop.addressee = NamedPipe::CommandPipe::GlueUnit;

	//Wrap Arm Pre-Heater 21
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater21.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = S1_2;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = S1_2;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater21.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = S1_2;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = S1_2;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater21.Increase"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = S1_2;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = S1_2;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater21.Decrease"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = S1_2;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = S1_2;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater 4
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater4.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B1_GU;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B1_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater4.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B1_GU;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B1_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater4.Increase"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B1_GU;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B1_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater4.Decrease"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B1_GU;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B1_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater 5
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater5.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B2_GU;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B2_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater5.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B2_GU;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B2_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater5.Increase"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B2_GU;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B2_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater5.Decrease"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B2_GU;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B2_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater 6
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater6.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B3_GU;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B3_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater6.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B3_GU;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B3_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater6.Increase"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B3_GU;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B3_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater6.Decrease"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B3_GU;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B3_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater 51
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater51.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = S1_5;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = S1_5;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater51.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = S1_5;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = S1_5;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater51.Increase"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = S1_5;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = S1_5;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater51.Decrease"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = S1_5;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = S1_5;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater 40
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater40.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B1_GUO;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B1_GUO;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater40.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B1_GUO;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B1_GUO;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater40.Increase"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B1_GUO;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B1_GUO;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater40.Decrease"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B1_GUO;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B1_GUO;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater 50
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater50.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B2_GUO;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B2_GUO;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater50.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B2_GUO;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B2_GUO;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater50.Increase"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B2_GUO;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B2_GUO;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater50.Decrease"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B2_GUO;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B2_GUO;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater 60
	if (command.CompareNoCase(L"LocalRemoteWrapArmPreHeater60.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B3_GUO;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B3_GUO;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualWrapArmPreHeater60.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B3_GUO;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B3_GUO;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if (command.CompareNoCase(L"WrapArmPreHeater60.Increase"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B3_GUO;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B3_GUO;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopWrapArmPreHeater;
		return true;
	}
	if (command.CompareNoCase(L"WrapArmPreHeater60.Decrease"))
	{
		commandPipeStart.command.glueUnitCommand.wrapArmNumber = B3_GUO;
		commandPipeStop.command.glueUnitCommand.wrapArmNumber = B3_GUO;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopWrapArmPreHeater;
		return true;
	}

	//GlueGap 1
	if (command.CompareNoCase(L"LocalRemoteGlueGap1.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.glueGapNumber = G1_GU;
		commandPipeStop.command.glueUnitCommand.glueGapNumber = G1_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleLocalRemoteGlueGap;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleLocalRemoteGlueGap;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualGlueGap1.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.glueGapNumber = G1_GU;
		commandPipeStop.command.glueUnitCommand.glueGapNumber = G1_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleAutoManualGlueGap;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleAutoManualGlueGap;
		return true;
	};
	if (command.CompareNoCase(L"GlueGap1.Increase"))
	{
		commandPipeStart.command.glueUnitCommand.glueGapNumber = G1_GU;
		commandPipeStop.command.glueUnitCommand.glueGapNumber = G1_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartIncreaseGlueGap;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopGlueGap;
		return true;
	}
	if (command.CompareNoCase(L"GlueGap1.Decrease"))
	{
		commandPipeStart.command.glueUnitCommand.glueGapNumber = G1_GU;
		commandPipeStop.command.glueUnitCommand.glueGapNumber = G1_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartDecreaseGlueGap;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopGlueGap;
		return true;
	}

	//GlueGap 2
	if (command.CompareNoCase(L"LocalRemoteGlueGap2.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.glueGapNumber = G2_GU;
		commandPipeStop.command.glueUnitCommand.glueGapNumber = G2_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleLocalRemoteGlueGap;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleLocalRemoteGlueGap;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualGlueGap2.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.glueGapNumber = G2_GU;
		commandPipeStop.command.glueUnitCommand.glueGapNumber = G2_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleAutoManualGlueGap;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleAutoManualGlueGap;
		return true;
	};
	if (command.CompareNoCase(L"GlueGap2.Increase"))
	{
		commandPipeStart.command.glueUnitCommand.glueGapNumber = G2_GU;
		commandPipeStop.command.glueUnitCommand.glueGapNumber = G2_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartIncreaseGlueGap;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopGlueGap;
		return true;
	}
	if (command.CompareNoCase(L"GlueGap2.Decrease"))
	{
		commandPipeStart.command.glueUnitCommand.glueGapNumber = G2_GU;
		commandPipeStop.command.glueUnitCommand.glueGapNumber = G2_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartDecreaseGlueGap;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopGlueGap;
		return true;
	}

	//GlueGap 3
	if (command.CompareNoCase(L"LocalRemoteGlueGap3.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.glueGapNumber = G3_GU;
		commandPipeStop.command.glueUnitCommand.glueGapNumber = G3_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleLocalRemoteGlueGap;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleLocalRemoteGlueGap;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualGlueGap3.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.glueGapNumber = G3_GU;
		commandPipeStop.command.glueUnitCommand.glueGapNumber = G3_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleAutoManualGlueGap;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleAutoManualGlueGap;
		return true;
	};
	if (command.CompareNoCase(L"GlueGap3.Increase"))
	{
		commandPipeStart.command.glueUnitCommand.glueGapNumber = G3_GU;
		commandPipeStop.command.glueUnitCommand.glueGapNumber = G3_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartIncreaseGlueGap;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopGlueGap;
		return true;
	}
	if (command.CompareNoCase(L"GlueGap3.Decrease"))
	{
		commandPipeStart.command.glueUnitCommand.glueGapNumber = G3_GU;
		commandPipeStop.command.glueUnitCommand.glueGapNumber = G3_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartDecreaseGlueGap;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopGlueGap;
		return true;
	}

	//GlueGap 4
	if (command.CompareNoCase(L"LocalRemoteGlueGap4.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.glueGapNumber = G4_GU;
		commandPipeStop.command.glueUnitCommand.glueGapNumber = G4_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleLocalRemoteGlueGap;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleLocalRemoteGlueGap;
		return true;
	};
	if (command.CompareNoCase(L"AutoManualGlueGap4.Toggle"))
	{
		commandPipeStart.command.glueUnitCommand.glueGapNumber = G4_GU;
		commandPipeStop.command.glueUnitCommand.glueGapNumber = G4_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartToggleAutoManualGlueGap;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopToggleAutoManualGlueGap;
		return true;
	};
	if (command.CompareNoCase(L"GlueGap4.Increase"))
	{
		commandPipeStart.command.glueUnitCommand.glueGapNumber = G4_GU;
		commandPipeStop.command.glueUnitCommand.glueGapNumber = G4_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartIncreaseGlueGap;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopGlueGap;
		return true;
	}
	if (command.CompareNoCase(L"GlueGap4.Decrease"))
	{
		commandPipeStart.command.glueUnitCommand.glueGapNumber = G4_GU;
		commandPipeStop.command.glueUnitCommand.glueGapNumber = G4_GU;
		commandPipeStart.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StartDecreaseGlueGap;
		commandPipeStop.command.glueUnitCommand.cmd = NamedPipe::GlueUnitCommand::StopGlueGap;
		return true;
	}

	return false;
}