#include ".\C_SingleFacerCommands.h"
#include "..\C_RTCManager.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;

C_SingleFacerCommands::C_SingleFacerCommands(int theSFNum)
{
	if((theSFNum > 0)&&(theSFNum < 4))
		SFNum = theSFNum;
	else
		SFNum = 0;
}

C_SingleFacerCommands::~C_SingleFacerCommands(void)
{
}

bool C_SingleFacerCommands::CreateCommand(NamedPipe::CommandPipe& outbuf)
{
	switch ( outbuf.command.singleFacerCommand.cmd )
	{
	case NamedPipe::SingleFacerCommand::SendConfiguration:
		return CreateConfigCommand(outbuf);
		break;
	case NamedPipe::SingleFacerCommand::B_SendConfiguration:
		return B_CreateConfigCommand(outbuf);
		break;
	case NamedPipe::SingleFacerCommand::UpdateAlarmList:
	{
		size_t len = outbuf.command.singleFacerCommand.alarmList.size() * ( String::MAX_CHARS_SYSTEMTIME + MAX_CODE + wcslen(Network::SEP) + 1);
		len++; //for \0
		wchar_t *alarm = new wchar_t[len+1];
		if(!outbuf.command.singleFacerCommand.alarmList.getAllActiveAlarm(alarm, len, Network::SEP))      
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"SingleFacer Active Alarm List: Check the buffer lenght possible buffer overrun");
		else
		{
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"SingleFacer Active Alarm List %s",alarm);
			o_socket.Send(Network::MSG_RTC_SWI,L"update.%s(%s)",outbuf.command.singleFacerCommand.alarmList.getMachineGeneric(),alarm);
		}
		delete[] alarm;
		return true;
		break;
	}
	case NamedPipe::SingleFacerCommand::Error:
		if(outbuf.isToView())
			o_socket.Send(Network::MSG_PAG_REF, L"pageManager._SetMessage(s[%s], b[true])", outbuf.getResponse());
		return false;
		break;
	default:
		return false;
	}
	return false;
}

bool C_SingleFacerCommands::ManageCommand(String::StrType page, String::StrType SingleFacerStringCommand)
{
	//check if the command is an increase/decrease
	if(ManageButton(page, SingleFacerStringCommand))
		return true;

	NamedPipe::CommandPipe commandPipe;	
	//choose the level
	switch(this->SFNum)
	{
	case 1:
		commandPipe.addressee=NamedPipe::CommandPipe::SingleFacer1;
		break;
	case 2:
		commandPipe.addressee=NamedPipe::CommandPipe::SingleFacer2;
		break;
	case 3:
		commandPipe.addressee=NamedPipe::CommandPipe::SingleFacer3;
		break;
	}

	String::StrType theSingleFacerCommand = SingleFacerStringCommand.extractCommand();

	if(theSingleFacerCommand.isEmpty()) 
		return false;

	//SendConfiguration
	if (theSingleFacerCommand.CompareNoCase(L"SendConfiguration"))
	{
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::SendConfiguration;

		if (ParseConfigCommand(SingleFacerStringCommand,commandPipe))
		{
			C_RTCManager::RTCSyncroS.Write(&commandPipe);
			return true;
		}
		else
			return false;
		
	};

	//SendConfiguration
	if (theSingleFacerCommand.CompareNoCase(L"B_SendConfiguration"))
	{
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::B_SendConfiguration;

		if (B_ParseConfigCommand(SingleFacerStringCommand,commandPipe))
		{
			C_RTCManager::RTCSyncroS.Write(&commandPipe);
			return true;
		}
		else
			return false;
		
	};

	//SendCurrentOrder
	if (theSingleFacerCommand.CompareNoCase(L"SendCurrentOrder"))
	{
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::SendCurrentOrder;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//SendNextOrder
	if (theSingleFacerCommand.CompareNoCase(L"SendNextOrder"))
	{
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::SendNextOrder;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//SendNextP1Order
	if (theSingleFacerCommand.CompareNoCase(L"SendNextP1Order"))
	{
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::SendNextP1Order;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};

	int move = (this->SFNum-1)*2; //0, 2, 4
	
	//Wrap Arm Pre-Heater [12,14,16] = Em
	if( theSingleFacerCommand.CompareNoCase(L"WrapArmPreHeaterEm.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.singleFacerCommand.setPoint = SingleFacerStringCommand.ToDouble();
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_1 + move);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater [22,24,26] = Im
	if( theSingleFacerCommand.CompareNoCase(L"WrapArmPreHeaterIm.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.singleFacerCommand.setPoint = SingleFacerStringCommand.ToDouble();
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_2 + move);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater [32,34,36] = 1m
	if( theSingleFacerCommand.CompareNoCase(L"WrapArmPreHeater1m.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.singleFacerCommand.setPoint = SingleFacerStringCommand.ToDouble();
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_3 + move);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater [42,44,46] = 2m
	if( theSingleFacerCommand.CompareNoCase(L"WrapArmPreHeater2m.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.singleFacerCommand.setPoint = SingleFacerStringCommand.ToDouble();
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_4 + move);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater [52,54,56] = 3m
	if( theSingleFacerCommand.CompareNoCase(L"WrapArmPreHeater3m.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.singleFacerCommand.setPoint = SingleFacerStringCommand.ToDouble();
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_5 + move);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater [62,64,66] = 4m
	if( theSingleFacerCommand.CompareNoCase(L"WrapArmPreHeater4m.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.singleFacerCommand.setPoint = SingleFacerStringCommand.ToDouble();
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_6 + move);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater [13,15,17] = El
	if( theSingleFacerCommand.CompareNoCase(L"WrapArmPreHeaterEl.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.singleFacerCommand.setPoint = SingleFacerStringCommand.ToDouble();
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_1 + move);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater [23,25,27] = Il
	if( theSingleFacerCommand.CompareNoCase(L"WrapArmPreHeaterIl.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.singleFacerCommand.setPoint = SingleFacerStringCommand.ToDouble();
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_2 + move);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater [33,35,37] = 1l
	if( theSingleFacerCommand.CompareNoCase(L"WrapArmPreHeater1l.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.singleFacerCommand.setPoint = SingleFacerStringCommand.ToDouble();
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_3 + move);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater [43,45,47] = 2l
	if( theSingleFacerCommand.CompareNoCase(L"WrapArmPreHeater2l.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.singleFacerCommand.setPoint = SingleFacerStringCommand.ToDouble();
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_4 + move);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater [53,55,57] = 3l
	if( theSingleFacerCommand.CompareNoCase(L"WrapArmPreHeater3l.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.singleFacerCommand.setPoint = SingleFacerStringCommand.ToDouble();
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_5 + move);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//Wrap Arm Pre-Heater [63,65,67] = 4l
	if( theSingleFacerCommand.CompareNoCase(L"WrapArmPreHeater4l.GoToSetPointWrapArmPreHeater")  )
	{
		commandPipe.command.singleFacerCommand.setPoint = SingleFacerStringCommand.ToDouble();
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::GoToSetPointWrapArmPreHeater;
		commandPipe.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_6 + move);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	int moveGlueGap = (this->SFNum-1)*10; //0, 10, 20
	//GlueGap [11,21,31] = 1
	if( theSingleFacerCommand.CompareNoCase(L"GlueGap1.GoToSetPointGlueGap")  )
	{
		commandPipe.command.singleFacerCommand.setPoint = SingleFacerStringCommand.ToDouble();
		commandPipe.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::GoToSetPointGlueGap;
		commandPipe.command.singleFacerCommand.glueGapNumber = GlueGapNumber(G1_SF + moveGlueGap);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Single Facer wrong command: %s", theSingleFacerCommand.getPtr());
	return false;

}

bool C_SingleFacerCommands::getPipeCmd(String::StrType command, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop)
{
	//choose the level
	switch(this->SFNum)
	{
	case 1:
		commandPipeStart.addressee=NamedPipe::CommandPipe::SingleFacer1;
		commandPipeStop.addressee = NamedPipe::CommandPipe::SingleFacer1;
		break;
	case 2:
		commandPipeStart.addressee=NamedPipe::CommandPipe::SingleFacer2;
		commandPipeStop.addressee = NamedPipe::CommandPipe::SingleFacer2;
		break;
	case 3:
		commandPipeStart.addressee=NamedPipe::CommandPipe::SingleFacer3;
		commandPipeStop.addressee = NamedPipe::CommandPipe::SingleFacer3;
		break;
	}

	int move = (this->SFNum-1)*2; //0, 2, 4

	//Wrap Arm Pre-Heater [12,14,16] = Em
	if( command.CompareNoCase(L"LocalRemoteWrapArmPreHeaterEm.Toggle") )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_1 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_1 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"AutoManualWrapArmPreHeaterEm.Toggle") )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_1 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_1 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"WrapArmPreHeaterEm.Increase")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_1 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_1 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}
	if( command.CompareNoCase(L"WrapArmPreHeaterEm.Decrease")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_1 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_1 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater [22,24,26] = Im
	if( command.CompareNoCase(L"LocalRemoteWrapArmPreHeaterIm.Toggle") )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_2 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_2 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"AutoManualWrapArmPreHeaterIm.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_2 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_2 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"WrapArmPreHeaterIm.Increase")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_2 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_2 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}
	if( command.CompareNoCase(L"WrapArmPreHeaterIm.Decrease")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_2 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_2 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater [32,34,36] = 1m
	if( command.CompareNoCase(L"LocalRemoteWrapArmPreHeater1m.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_3 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_3 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"AutoManualWrapArmPreHeater1m.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_3 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_3 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"WrapArmPreHeater1m.Increase")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_3 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_3 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}
	if( command.CompareNoCase(L"WrapArmPreHeater1m.Decrease")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_3 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_3 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater [42,44,46] = 2m
	if( command.CompareNoCase(L"LocalRemoteWrapArmPreHeater2m.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_4 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_4 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"AutoManualWrapArmPreHeater2m.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_4 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_4 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"WrapArmPreHeater2m.Increase")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_4 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_4 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}
	if( command.CompareNoCase(L"WrapArmPreHeater2m.Decrease")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_4 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_4 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater [52,54,56] = 3m
	if( command.CompareNoCase(L"LocalRemoteWrapArmPreHeater3m.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_5 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_5 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"AutoManualWrapArmPreHeater3m.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_5 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_5 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"WrapArmPreHeater3m.Increase")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_5 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_5 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}
	if( command.CompareNoCase(L"WrapArmPreHeater3m.Decrease")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_5 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_5 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater [62,64,66] = 4m
	if( command.CompareNoCase(L"LocalRemoteWrapArmPreHeater4m.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_6 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_6 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"AutoManualWrapArmPreHeater4m.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_6 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_6 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"WrapArmPreHeater4m.Increase")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_6 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_6 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}
	if( command.CompareNoCase(L"WrapArmPreHeater4m.Decrease")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_6 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S2_6 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater [13,15,17] = El
	if( command.CompareNoCase(L"LocalRemoteWrapArmPreHeaterEl.Toggle") )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_1 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_1 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"AutoManualWrapArmPreHeaterEl.Toggle") )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_1 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_1 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"WrapArmPreHeaterEl.Increase")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_1 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_1 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}
	if( command.CompareNoCase(L"WrapArmPreHeaterEl.Decrease")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_1 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_1 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater [23,25,27] = Il
	if( command.CompareNoCase(L"LocalRemoteWrapArmPreHeaterIl.Toggle") )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_2 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_2 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"AutoManualWrapArmPreHeaterIl.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_2 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_2 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"WrapArmPreHeaterIl.Increase")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_2 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_2 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}
	if( command.CompareNoCase(L"WrapArmPreHeaterIl.Decrease")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_2 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_2 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater [33,35,37] = 1l
	if( command.CompareNoCase(L"LocalRemoteWrapArmPreHeater1l.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_3 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_3 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"AutoManualWrapArmPreHeater1l.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_3 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_3 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"WrapArmPreHeater1l.Increase")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_3 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_3 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}
	if( command.CompareNoCase(L"WrapArmPreHeater1l.Decrease")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_3 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_3 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater [43,45,47] = 2l
	if( command.CompareNoCase(L"LocalRemoteWrapArmPreHeater2l.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_4 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_4 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"AutoManualWrapArmPreHeater2l.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_4 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_4 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"WrapArmPreHeater2l.Increase")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_4 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_4 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}
	if( command.CompareNoCase(L"WrapArmPreHeater2l.Decrease")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_4 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_4 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater [53,55,57] = 3l
	if( command.CompareNoCase(L"LocalRemoteWrapArmPreHeater3l.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_5 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_5 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"AutoManualWrapArmPreHeater3l.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_5 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_5 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"WrapArmPreHeater3l.Increase")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_5 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_5 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}
	if( command.CompareNoCase(L"WrapArmPreHeater3l.Decrease")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_5 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_5 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}

	//Wrap Arm Pre-Heater [63,65,67] = 4l
	if( command.CompareNoCase(L"LocalRemoteWrapArmPreHeater4l.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_6 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_6 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleLocalRemoteWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleLocalRemoteWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"AutoManualWrapArmPreHeater4l.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_6 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_6 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleAutoManualWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleAutoManualWrapArmPreHeater;
		return true;
	};
	if( command.CompareNoCase(L"WrapArmPreHeater4l.Increase")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_6 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_6 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}
	if( command.CompareNoCase(L"WrapArmPreHeater4l.Decrease")  )
	{
		commandPipeStart.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_6 + move);
		commandPipeStop.command.singleFacerCommand.wrapArmNumber = WrapArmNumber(S3_6 + move);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreaseWrapArmPreHeater;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopWrapArmPreHeater;
		return true;
	}

	int moveGlueGap = (this->SFNum-1)*10; //0, 10, 20
	//GlueGap [11,21,31] = 1
	if( command.CompareNoCase(L"LocalRemoteGlueGap1.Toggle") )
	{
		commandPipeStart.command.singleFacerCommand.glueGapNumber = GlueGapNumber(G1_SF + moveGlueGap);
		commandPipeStop.command.singleFacerCommand.glueGapNumber = GlueGapNumber(G1_SF + moveGlueGap);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleLocalRemoteGlueGap;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleLocalRemoteGlueGap;
		return true;
	};
	if( command.CompareNoCase(L"AutoManualGlueGap1.Toggle") )
	{
		commandPipeStart.command.singleFacerCommand.glueGapNumber = GlueGapNumber(G1_SF + moveGlueGap);
		commandPipeStop.command.singleFacerCommand.glueGapNumber = GlueGapNumber(G1_SF + moveGlueGap);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleAutoManualGlueGap;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleAutoManualGlueGap;
		return true;
	};
	if( command.CompareNoCase(L"GlueGap1.Increase") )
	{
		commandPipeStart.command.singleFacerCommand.glueGapNumber = GlueGapNumber(G1_SF + moveGlueGap);
		commandPipeStop.command.singleFacerCommand.glueGapNumber = GlueGapNumber(G1_SF + moveGlueGap);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreaseGlueGap;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopGlueGap;
		return true;
	}
	if( command.CompareNoCase(L"GlueGap1.Decrease") )
	{
		commandPipeStart.command.singleFacerCommand.glueGapNumber = GlueGapNumber(G1_SF + moveGlueGap);
		commandPipeStop.command.singleFacerCommand.glueGapNumber = GlueGapNumber(G1_SF + moveGlueGap);
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreaseGlueGap;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopGlueGap;
		return true;
	}

	//Pressure Roll
	if( command.CompareNoCase(L"LocalRemotePressureRoll.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleLocalRemotePressureRoll;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleLocalRemotePressureRoll;
		return true;
	}
	if( command.CompareNoCase(L"AutoManualPressureRoll.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleAutoManualPressureRoll;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleAutoManualPressureRoll;
		return true;
	}
	if( command.CompareNoCase(L"PressureRoll.Increase") )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreasePressureRoll;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopPressureRoll;
		return true;
	}
	if( command.CompareNoCase(L"PressureRoll.Decrease") )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreasePressureRoll;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopPressureRoll;
		return true;
	}

	//Torpress
	if( command.CompareNoCase(L"AutoManualTorpress.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleAutoManualTorpress;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleAutoManualTorpress;
		return true;
	}
	if( command.CompareNoCase(L"Torpress.Increase") )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreaseTorpress;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopTorpress;
		return true;
	}
	if( command.CompareNoCase(L"Torpress.Decrease") )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreaseTorpress;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopTorpress;
		return true;
	}

	//Main Steam
	if( command.CompareNoCase(L"SfMainSteam.Increase") )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreaseMainSteam;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopMainSteam;
		return true;
	}
	if( command.CompareNoCase(L"SfMainSteam.Decrease") )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreaseMainSteam;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopMainSteam;
		return true;
	}
  if( command.CompareNoCase(L"LocalRemoteSfMainSteam.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleLocalRemoteMainSteam;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleLocalRemoteMainSteam;
		return true;
	}

	//Steam Shower
	if( command.CompareNoCase(L"AutoManualSteamShower.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleAutoManualSteamShower;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleAutoManualSteamShower;
		return true;
	}
	if( command.CompareNoCase(L"SteamShower.Increase") )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartIncreaseSteamShower;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopSteamShower;
		return true;
	}
	if( command.CompareNoCase(L"SteamShower.Decrease") )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartDecreaseSteamShower;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopSteamShower;
		return true;
	}
	if( command.CompareNoCase(L"OnOffSteamShower.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleOnOffSteamShower;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleOnOffSteamShower;
		return true;
	}
	if( command.CompareNoCase(L"LocalRemoteSteamShower.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleLocalRemoteSteamShower;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleLocalRemoteSteamShower;
		return true;
	}

  //MOISTURE SENSOR
	if( command.CompareNoCase(L"RecalibrateMoinstureSensor.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::B_StartRecalibrateMoinstureSensor;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::B_StopRecalibrateMoinstureSensor;
		return true;
	}
  if( command.CompareNoCase(L"RecalibrateBridgeNow.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::B_StartRecalibrateBridgeNow;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::B_StopRecalibrateBridgeNow;
		return true;
	}

  //PARAFFIN MACHINE
  if( command.CompareNoCase(L"ParaffinMachineLiner.Toggle")  )
	{
    commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleEnableParaffinMachineLiner;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleEnableParaffinMachineLiner;
		return true;
	}
  if( command.CompareNoCase(L"ParaffinMachineMedium.Toggle")  )
	{
		commandPipeStart.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StartToggleEnableParaffinMachineMedium;
		commandPipeStop.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::StopToggleEnableParaffinMachineMedium;
		return true;
	}

	return false;
}

bool C_SingleFacerCommands::CreateConfigCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[String::MAX_CHARS_CONFIG_MACHINE]=L"";
	outbuf.command.singleFacerCommand.config.Serialize(cfg, Network::SEP, this->SFNum);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.SingleFacer%dConfig.Config(%s)",this->SFNum , cfg);

	return true;
}

bool C_SingleFacerCommands::ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::SendConfiguration;
	outbuf.command.singleFacerCommand.config.Deserialize(stringConfig, Network::SEP);

	return true;
}

bool C_SingleFacerCommands::B_CreateConfigCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[String::MAX_CHARS_CONFIG_MACHINE]=L"";
	outbuf.command.singleFacerCommand.B_config.Serialize(cfg, Network::SEP, this->SFNum);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.Bridge%dConfig.Config(%s)",this->SFNum , cfg);
	
	return true;
}

bool C_SingleFacerCommands::B_ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.singleFacerCommand.cmd = NamedPipe::SingleFacerCommand::B_SendConfiguration;
	outbuf.command.singleFacerCommand.B_config.Deserialize(stringConfig, Network::SEP);

	return true;
}

