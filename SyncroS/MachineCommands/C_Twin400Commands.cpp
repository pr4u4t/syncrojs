#include "C_Twin400Commands.h"
#include "..\C_RTCManager.h"
#include "..\C_PageManager.h"
#include "..\..\Include\NameSpace\Network.h"
#include "..\..\Shared\C_SMLineConfig.h"

extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;
extern Logger::C_Logger logConfig;

C_Twin400Commands::C_Twin400Commands(void)
{
}

C_Twin400Commands::~C_Twin400Commands(void)
{
}

bool C_Twin400Commands::CreateCommand(NamedPipe::CommandPipe& outbuf)
{
	switch ( outbuf.command.slitterScorerCommand.cmd )
	{
	case NamedPipe::SlitterScorerCommand::SendConfiguration:
		return CreateConfigCommand(outbuf);
	case NamedPipe::SlitterScorerCommand::SendLevelCount:
		o_socket.Send(Network::MSG_PAG_REF, L"twin400_machineSettings._SetLevelCount(s[%s])", outbuf.command.slitterScorerCommand.data[1]);
		return true;
	case NamedPipe::SlitterScorerCommand::SendMeasureWheel:
		o_socket.Send(Network::MSG_PAG_REF, L"twin400_machineSettings._SetMeasureWheel(s[%s])", outbuf.command.slitterScorerCommand.data[1]);
		return true;
	case NamedPipe::SlitterScorerCommand::BrokenSlitsChange:
		return CreateBrokenSlitsCommand(outbuf);
	case NamedPipe::SlitterScorerCommand::ToggleDefaultShaft:
		return CreateToggleDefaultShaftCommand(outbuf);
	case NamedPipe::SlitterScorerCommand::UpdateAlarmList:
	{
		int numAlarm = outbuf.command.slitterScorerCommand.alarmList.size();
		if(numAlarm!=0)
		{
			size_t size = outbuf.command.slitterScorerCommand.alarmList.size() * ( String::MAX_CHARS_SYSTEMTIME + MAX_CODE + wcslen(Network::SEP) + 1);
			size++; //for \0
			wchar_t *alarm = new wchar_t[size];
			if(!outbuf.command.slitterScorerCommand.alarmList.getAllActiveAlarm(alarm, size, Network::SEP))
				logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"TWIN400 Active Alarm List: Check the buffer lenght possible buffer overrun");
			else
			{
				logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"TWIN400 Active Alarm List %s",alarm);
				o_socket.Send(Network::MSG_RTC_SWI,L"update.%s(%s)",outbuf.command.slitterScorerCommand.alarmList.getMachineGeneric(),alarm);
			}

			delete alarm;
			return true;
		}
		else
			return true;
	}
	case NamedPipe::SlitterScorerCommand::Error:
		if(outbuf.isToView())
			o_socket.Send(Network::MSG_PAG_REF, L"pageManager._SetMessage(s[%s], b[true])", outbuf.getResponse());
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"Impossible to execute command: %s",outbuf.getResponse());
		return true;
	default:
		return false;
	}
	return false;
}

bool C_Twin400Commands::CreateConfigCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[String::MAX_CHARS_CONFIG_MACHINE]=L"";
	outbuf.command.slitterScorerCommand.config.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF, L"lineConfig.SlitterScorerConfig.Config(%s)", cfg);
	return true;
}

bool C_Twin400Commands::CreateBrokenSlitsCommand(NamedPipe::CommandPipe& outbuf)
{
	o_socket.Send(Network::MSG_DB_REF, L"lineConfig.SlitterScorerConfig.SetBrokenBlades(%s%s%s)", 
			outbuf.command.slitterScorerCommand.data[1],Network::SEP,outbuf.command.slitterScorerCommand.data[2]);
	return true;
}

bool C_Twin400Commands::CreateToggleDefaultShaftCommand(NamedPipe::CommandPipe& outbuf)
{
  //agrassi_FI03-01-2010_2
	//o_socket.Send(Network::MSG_DB_REF, L"lineConfig.SlitterScorerConfig.SetDefaultShaft(%s)", 
	//							outbuf.command.slitterScorerCommand.data[1]);
	o_socket.Send(Network::MSG_DB_REF, L"lineConfig.SlitterScorerConfig.Twin400.SetDefaultShaft(%s)", 
								outbuf.command.slitterScorerCommand.data[1]);
	return true;
}

bool C_Twin400Commands::ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::SendConfiguration;
	outbuf.command.slitterScorerCommand.config.Deserialize(stringConfig, Network::SEP);
	int errCode=F_CheckSlitterScorerConfig(&outbuf.command.slitterScorerCommand.config);
	if (errCode<CONFIG_OK)
	{
		o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%S], b[true])", DecodeValidationError(errCode));
		logger.Severe(__FUNCTION__, L"Twin400 configuration not valid: error %d, %S", errCode, DecodeValidationError(errCode));
	}
	else
	{
		C_RTCManager::RTCSyncroS.Write(&outbuf);	
		o_socket.Send(Network::MSG_RTC_SWI,L"TwinConfigValidationSucceeded.SS()");
		logger.Severe(__FUNCTION__, L"TWIN400 configuration valid!");
		o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s])", L"OK. Configuration successfully changed.");
	}
	return true;
}


bool C_Twin400Commands::ManageCommand(String::StrType page, String::StrType Twin400StringCommand)
{
	//check if the command is an increase/decrease	
	if(ManageButton(page, Twin400StringCommand))
		return true;

	NamedPipe::CommandPipe commandPipe;	
	commandPipe.addressee = NamedPipe::CommandPipe::SlitterScorer;
	String::StrType Twin400command = Twin400StringCommand.extractCommand();
	if(Twin400command.isEmpty()) return false;

	if (Twin400command.CompareNoCase(L"SendConfiguration"))
		return ParseConfigCommand(Twin400StringCommand, commandPipe);

	if (Twin400command.CompareNoCase(L"SendLevelCount"))
	{
		commandPipe.command.slitterScorerCommand.cmd=NamedPipe::SlitterScorerCommand::SendLevelCount;
		int levelCount=Twin400StringCommand.ToInt();
		wcscpy(commandPipe.command.slitterScorerCommand.data[1], Twin400StringCommand);
		if ((levelCount<=0) || (levelCount>NUM_ORDERS_PER_SETUP))
			o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[#%d§%s], b[true])", levelCount, L"SYCS_07");
		else
			C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	if (Twin400command.CompareNoCase(L"SendMeasureWheel"))
	{
		commandPipe.command.slitterScorerCommand.cmd=NamedPipe::SlitterScorerCommand::SendMeasureWheel;
		double measureWheel=_wtof(Twin400StringCommand.getPtr());
		wcscpy(commandPipe.command.slitterScorerCommand.data[1], Twin400StringCommand);
		if (measureWheel<=0)
			o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[#%f§%s], b[true])", measureWheel, L"SYCS_08");
		else
			C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	if (Twin400command.CompareNoCase(L"SendCalibration"))
	{
		commandPipe.command.slitterScorerCommand.cmd=NamedPipe::SlitterScorerCommand::SendConfiguration;
		std::vector<String::StrType> args=Twin400StringCommand.Tokenize(L",");
		State::SSPlcState ssState;
		C_PageManager::SMState.getSSPlcState(ssState);
		if (ssState.getTaskStatusPLCComm() && ssState.getTaskStatusPLCRun())
		{
			if (ssState.getMaintenanceStatusMachineInCalibration())
			{
				if (F_CalibratePlunge(args[0].ToInt(), args[1].ToInt(), args[2].ToInt(), args[3].ToInt(), (Shaft)(args[4].ToInt()+Score1)))
				{
					LineConfig cfg;
					C_SMLineConfig smLineConfig;
					smLineConfig.CreateShared();
					smLineConfig.getLineConfig(cfg);
					commandPipe.command.slitterScorerCommand.config=cfg.slitterScorer;
					C_RTCManager::RTCSyncroS.Write(&commandPipe);
					o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[§%s])", L"SYCS_09");
				}
				else
					o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[§%s], b[true])", L"SYCS_10");
			}
			else
				o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[§%s], b[true])", L"SYCS_40");
		}
		else
			o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[§%s], b[true])", L"SYCS_33");
		return true;
	}

	if (Twin400command.CompareNoCase(L"GetCalibrationDist"))
	{
		std::vector<String::StrType> args=Twin400StringCommand.Tokenize(L",");
		int dist=F_TestAngleToGap(args[0].ToInt(), (Shaft)(args[1].ToInt()+Score1));
		o_socket.Send(Network::MSG_PAG_REF,L"twin400_plunges._SetDist(I[%d], I[%d])", dist, args[1].ToInt());
		return true;
	}

	if (Twin400command.CompareNoCase(L"GetCalibrationAngle"))
	{
		std::vector<String::StrType> args=Twin400StringCommand.Tokenize(L",");
		int angle=F_TestGapToAngle(args[0].ToInt(), (Shaft)(args[1].ToInt()+Score1));
		o_socket.Send(Network::MSG_PAG_REF,L"twin400_plunges._SetAngle(I[%d], I[%d])", angle, args[1].ToInt());
		return true;
	}

	if (Twin400command.CompareNoCase(L"BrokenSlitsChange"))
	{
		commandPipe.command.slitterScorerCommand.cmd=NamedPipe::SlitterScorerCommand::BrokenSlitsChange;
		std::vector<String::StrType> args=Twin400StringCommand.Tokenize(L",");
		wcscpy(commandPipe.command.slitterScorerCommand.data[1], args[0]);
		wcscpy(commandPipe.command.slitterScorerCommand.data[2], args[1]);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	if (Twin400command.CompareNoCase(L"SendCleanCutData"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::SendCleanCutData;
		std::vector<String::StrType> args=Twin400StringCommand.Tokenize(L",");
		wcscpy(commandPipe.command.slitterScorerCommand.data[1], args[0]);
		wcscpy(commandPipe.command.slitterScorerCommand.data[2], args[1]);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	if (Twin400command.CompareNoCase(L"MaintenancePositioning", wcslen(L"MaintenancePositioning")))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::SendMaintenancePositioning;
		wcscpy(commandPipe.command.slitterScorerCommand.data[1],Twin400command+wcslen(Twin400command)-1); 
		std::vector<String::StrType> args=Twin400StringCommand.Tokenize(L",");
		wcscpy(commandPipe.command.slitterScorerCommand.data[2], args[0]);
		wcscpy(commandPipe.command.slitterScorerCommand.data[3], args[1]);
		wcscpy(commandPipe.command.slitterScorerCommand.data[4], args[2]);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"SpinningForGrinding"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleSpinningForGrinding;
		logger.Info(__FUNCTION__, L"SpinningForGrinding");
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"LeftTrimBlade"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleLeftTrimBlade;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"RightTrimBlade"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleRightTrimBlade;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	if (Twin400command.CompareNoCase(L"JamupPhotoeye"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleJamupPhotoeye;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//RFAZZI_FI19/06/2008.Added command for lift the Trim Slits
	if (Twin400command.CompareNoCase(L"TrimSlitterSxAx1"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleTrimSlit;
		wcscpy(commandPipe.command.slitterScorerCommand.data[1], L"SX");
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//RFAZZI_FI19/06/2008.Added command for lift the Trim Slits
	if (Twin400command.CompareNoCase(L"TrimSlitterDxAx1"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleTrimSlit;
		wcscpy(commandPipe.command.slitterScorerCommand.data[1], L"DX");
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//RFAZZI_FI19/06/2008.Added command for lift the Trim Slits
	if (Twin400command.CompareNoCase(L"TrimSlitterSxAx2"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleTrimSlit;
		wcscpy(commandPipe.command.slitterScorerCommand.data[1], L"SX");
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	//RFAZZI_FI19/06/2008.Added command for lift the Trim Slits
	if (Twin400command.CompareNoCase(L"TrimSlitterDxAx2"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleTrimSlit;
		wcscpy(commandPipe.command.slitterScorerCommand.data[1], L"DX");
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	if (Twin400command.CompareNoCase(L"InternalFlap"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleInternalFlap;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	if (Twin400command.CompareNoCase(L"SpeedDigitalReference"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleSpeedDigitalReference;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	if (Twin400command.CompareNoCase(L"PhotoeyeSlitShaft"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleSlitPhotoeyeEnabled;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	if (Twin400command.CompareNoCase(L"SlowPositioning"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleSlowPositioning;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	if (Twin400command.CompareNoCase(L"ChangeDefaultShaft"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleDefaultShaft;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	if (Twin400command.CompareNoCase(L"AdjustScoreGapOffset"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::AdjustScoreGapOffset;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	if (Twin400command.CompareNoCase(L"ResetSlitCounters"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ResetSlitCounters;
		std::vector<String::StrType> args=Twin400StringCommand.Tokenize(L",");
		wcscpy(commandPipe.command.slitterScorerCommand.data[1], args[0]);
		wcscpy(commandPipe.command.slitterScorerCommand.data[2], args[1]);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"ResetGrinder"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ResetGrindingToolCounter;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"EnableGrinding"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::EnableGrinding;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
 	
	if (Twin400command.CompareNoCase(L"GrindSlit"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::GrindSlit;
		std::vector<String::StrType> args=Twin400StringCommand.Tokenize(L",");
		for (size_t i=0; i<args.size(); i++)
			wcscpy(commandPipe.command.slitterScorerCommand.data[i+1], args[i]);
		if (args.size()==2)
			wcscpy(commandPipe.command.slitterScorerCommand.data[3], L"0");
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

  if (Twin400command.CompareNoCase(L"ResetMultipleGrinding"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ResetMultipleGrinding;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"LubeSlit"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::LubeSlit;
		std::vector<String::StrType> args=Twin400StringCommand.Tokenize(L",");
		wcscpy(commandPipe.command.slitterScorerCommand.data[1], args[0]);
		wcscpy(commandPipe.command.slitterScorerCommand.data[2], args[1]);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"LiftAllShafts"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::LiftAllShafts;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"ManualAOC"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::SendManualAOC;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"ForceReset"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ForceReset;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"ForceScan"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ForceReadSlitPositions;
		logger.Info(__FUNCTION__, L"ForceReadSlitPositions");
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"RemoteControl"))
	{
    commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleRemoteControl;
		logger.Info(__FUNCTION__, L"ToggleRemoteControl");
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	if (Twin400command.CompareNoCase(L"SendSetup0") || Twin400command.CompareNoCase(L"SendSetup1") ||
			Twin400command.CompareNoCase(L"SendSetup2"))
	{
		size_t len=wcslen(Twin400command.getPtr());
		int pos=Twin400command.getPtr()[len-1]-L'0';
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::SendSetup;
		wcscpy(commandPipe.command.slitterScorerCommand.data[0], L"0"); //manual command
		swprintf(commandPipe.command.slitterScorerCommand.data[1], L"%d", pos);	//position
		logger.Info(__FUNCTION__, L"Send Manually setup %d", pos+1);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"LoadTeflon0") || Twin400command.CompareNoCase(L"LoadTeflon1"))
	{
		size_t len=wcslen(Twin400command.getPtr());
		int shaft=Twin400command.getPtr()[len-1]-L'0';
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::LoadTeflon;
		wcscpy(commandPipe.command.slitterScorerCommand.data[0], L"0"); //manual command
		swprintf(commandPipe.command.slitterScorerCommand.data[1], L"%d", shaft);	//shaft
		logger.Info(__FUNCTION__, L"Load teflon shaft %d", shaft+1);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"SendSetupDebug"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::SendSetup;
		std::vector<String::StrType> args=Twin400StringCommand.Tokenize(L",");
		wcscpy(commandPipe.command.slitterScorerCommand.data[0], L"0"); //manual command
		wcscpy(commandPipe.command.slitterScorerCommand.data[1], args[0]);	//position
		logger.Info(__FUNCTION__, L"Send Manually Setup %d for DEBUG sake");
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"ToggleSimulatePLC"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleSimulatePLC;
		logger.Info(__FUNCTION__, L"ToggleSimulatePLC");
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"RecoverPositioning"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::RecoverPositioning;
		logger.Info(__FUNCTION__, L"RecoverPositioning");
		C_RTCManager::RTCSyncroS.Write(&commandPipe);
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"Maintenance"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleMaintenanceMode;
		logger.Info(__FUNCTION__, L"Maintenance");
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"TestIO"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleTestIOMode;
		logger.Info(__FUNCTION__, L"TestIO");
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"Calibration"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleCalibrationMode;
		logger.Info(__FUNCTION__, L"Calibration");
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"ModifyWebDirector"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ModifyWebDirector;
		logger.Info(__FUNCTION__, L"ModifyWebDirector");
		std::vector<String::StrType> args=Twin400StringCommand.Tokenize(L",");
		wcscpy(commandPipe.command.slitterScorerCommand.data[1], args[0]);
		wcscpy(commandPipe.command.slitterScorerCommand.data[2], args[1]);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"ModifyScoreGap1") || Twin400command.CompareNoCase(L"ModifyScoreGap2") ||
			Twin400command.CompareNoCase(L"ModifyScoreGap3"))
	{
		size_t len=wcslen(Twin400command.getPtr());
		int shaft=Twin400command.getPtr()[len-1]-L'0'; //1, 2 or 3
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ModifyScoreGap;
		wcscpy(commandPipe.command.slitterScorerCommand.data[0], L"0"); //manual command
		swprintf(commandPipe.command.slitterScorerCommand.data[1], L"%d", shaft-1);	//position
		wcscpy(commandPipe.command.slitterScorerCommand.data[2], Twin400StringCommand);	//position
		logger.Info(__FUNCTION__, L"ModifyScoreGap %d, value %s", shaft, commandPipe.command.slitterScorerCommand.data[2]);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"Simulation"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleSimulationMode;
		logger.Info(__FUNCTION__, L"Simulation");
		std::vector<String::StrType> args=Twin400StringCommand.Tokenize(L",");
		wcscpy(commandPipe.command.slitterScorerCommand.data[1], args[0]);
		wcscpy(commandPipe.command.slitterScorerCommand.data[2], args[1]);
		wcscpy(commandPipe.command.slitterScorerCommand.data[3], args[2]);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	if (Twin400command.CompareNoCase(L"AOCSimulation"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::ToggleAOCSimulationMode;
		logger.Info(__FUNCTION__, L"AOCSimulation");
		std::vector<String::StrType> args=Twin400StringCommand.Tokenize(L",");
		wcscpy(commandPipe.command.slitterScorerCommand.data[1], args[0]);
		wcscpy(commandPipe.command.slitterScorerCommand.data[2], args[1]);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
	//if (Twin400command.CompareNoCase(L"SetIOReadChannels"))
	//{
	//	commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::SetIOReadChannels;
	//	//commandPipe->command.slitterScorerCommand.data = parametri

	//	C_RTCManager::RTCSyncroS.Write(&commandPipe);	
	//	return true;
	//}
	
	if (Twin400command.CompareNoCase(L"SetOutputChannel"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::SetOutputChannel;
		logger.Info(__FUNCTION__, L"SetOutputChannel");
		std::vector<String::StrType> args=Twin400StringCommand.Tokenize(L",");
		wcscpy(commandPipe.command.slitterScorerCommand.data[1], args[0]);
		wcscpy(commandPipe.command.slitterScorerCommand.data[2], args[1]);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}
	
  //AGrassi_FI06/12/2007_1
	if (Twin400command.CompareNoCase(L"TeflonCycleStart"))
	{
		commandPipe.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::TeflonCycleStart;
		logger.Info(__FUNCTION__, L"TeflonCycleStart");
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	}

	logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Twin400 wrong command: %s", Twin400command.getPtr());
	return false;

}

bool C_Twin400Commands::getPipeCmd (String::StrType command, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop)
{
	commandPipeStart.addressee = NamedPipe::CommandPipe::SlitterScorer;
	commandPipeStop.addressee = NamedPipe::CommandPipe::SlitterScorer;


	//increase / decrease commands
	if (command.CompareNoCase(L"OpenTrimChutes._signal"))
	{
		commandPipeStart.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StartOpenChutes;
		commandPipeStop.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StopChutes;
		return true;
	}

	if (command.CompareNoCase(L"CloseTrimChutes._signal"))
	{
		commandPipeStart.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StartCloseChutes;
		commandPipeStop.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StopChutes;
		return true;
	}
	
	if (command.CompareNoCase(L"MoveMachineRight._signal"))
	{
		commandPipeStart.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StartMoveMachineRight;
		commandPipeStop.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StopMoveMachine;
		return true;
	}

	if (command.CompareNoCase(L"MoveMachineLeft._signal"))
	{
		commandPipeStart.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StartMoveMachineLeft;
		commandPipeStop.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StopMoveMachine;
		return true;
	}

	if (command.CompareNoCase(L"ScoreGap1.Increase"))
	{
		commandPipeStart.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StartIncreaseScoreGap1;
		commandPipeStop.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StopScoreGap1;
		return true;
	}

	if (command.CompareNoCase(L"ScoreGap1.Decrease"))
	{
		commandPipeStart.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StartDecreaseScoreGap1;
		commandPipeStop.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StopScoreGap1;
		return true;
	}
	
	if (command.CompareNoCase(L"ScoreGap2.Increase"))
	{
		commandPipeStart.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StartIncreaseScoreGap2;
		commandPipeStop.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StopScoreGap2;
		return true;
	}
	
	if (command.CompareNoCase(L"ScoreGap2.Decrease"))
	{
		commandPipeStart.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StartDecreaseScoreGap2;
		commandPipeStop.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StopScoreGap2;
		return true;
	}
	
	if (command.CompareNoCase(L"ScoreGap3.Increase"))
	{
		commandPipeStart.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StartIncreaseScoreGapAux;
		commandPipeStop.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StopScoreGapAux;
		return true;
	}
	
	if (command.CompareNoCase(L"ScoreGap3.Decrease"))
	{
		commandPipeStart.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StartDecreaseScoreGapAux;
		commandPipeStop.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StopScoreGapAux;
		return true;
	}

	if (command.CompareNoCase(L"ManualAutomaticMachineMove"))
	{
		commandPipeStart.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StartToggleManualAutomaticMachineMove;
		commandPipeStop.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StopToggleManualAutomaticMachineMove;
		return true;
	}

	if (command.CompareNoCase(L"WebFlap"))
	{
		commandPipeStart.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StartToggleWebDirectorFlap;
		commandPipeStop.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StopToggleWebDirectorFlap;
		return true;
	}
	
	if (command.CompareNoCase(L"WebBlow._signal"))
	{
		commandPipeStart.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StartBlowOnWebDirector;
		commandPipeStop.command.slitterScorerCommand.cmd = NamedPipe::SlitterScorerCommand::StopBlowOnWebDirector;
		return true;
	}

	
	return false;
}
