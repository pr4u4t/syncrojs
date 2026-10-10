#include ".\C_SlitterScorerCommands.h"
#include "..\C_RTCManager.h"
#include "..\..\Include\NameSpace\Network.h"
//MSTAGETTI_FI-11-05-2010_1
#include "..\C_PageManager.h"

extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;

C_SlitterScorerCommands::C_SlitterScorerCommands(void)
{
}

C_SlitterScorerCommands::~C_SlitterScorerCommands(void)
{
}

bool C_SlitterScorerCommands::CreateCommand(NamedPipe::CommandPipe& outbuf)
{
	switch ( outbuf.command.twinCommand.cmd )
	{
	case NamedPipe::TwinCommand::SendConfiguration:
		return CreateConfigCommand(outbuf);
		break;
	case NamedPipe::TwinCommand::UpdateAlarmList:
	{
		size_t len = outbuf.command.twinCommand.alarmList.size() * ( String::MAX_CHARS_SYSTEMTIME + MAX_CODE + wcslen(Network::SEP) + 1);
		len++; //for \0
		wchar_t *alarm = new wchar_t[len+1];
		if(!outbuf.command.twinCommand.alarmList.getAllActiveAlarm(alarm, len, Network::SEP))      
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"SlitterScorer Active Alarm List: Check the buffer lenght possible buffer overrun");
		else
		{
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"SlitterScorer Active Alarm List %s",alarm);
			o_socket.Send(Network::MSG_RTC_SWI,L"update.%s(%s)",outbuf.command.twinCommand.alarmList.getMachineGeneric(),alarm);
		}
		delete[] alarm;
		return true;
		break;
	}
	case NamedPipe::TwinCommand::Error:
		if(outbuf.isToView())
			o_socket.Send(Network::MSG_PAG_REF, L"pageManager._SetMessage(s[%s], b[true])", outbuf.getResponse());
		return false;
		break;
	default:
		return false;
	}
	return false;
}

bool C_SlitterScorerCommands::CreateConfigCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[String::MAX_CHARS_CONFIG_MACHINE]=L"";
	outbuf.command.twinCommand.config.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF, L"lineConfig.SlitterScorerConfig.Config(%s)", cfg);
	return true;
}

bool C_SlitterScorerCommands::ManageCommand(String::StrType page, String::StrType SScommand)
{
	NamedPipe::CommandPipe commandPipe;
	commandPipe.Clean();
	commandPipe.addressee=NamedPipe::CommandPipe::SlitterScorerSupervisor;
	//MSTAGETTI_FI-11-05-2010_1
  LineConfig cfg;
  C_PageManager::SMLineConfig.getLineConfig(cfg);

	String::StrType theSScommand = SScommand.extractCommand();
	if (theSScommand.isEmpty())
		return false;

	if (theSScommand.CompareNoCase(L"SendConfiguration"))
		return ParseConfigCommand(SScommand, commandPipe);

	//SendCurrentOrder
	if (theSScommand.CompareNoCase(L"SendCurrentOrder"))
	{
    //agrassi_FI25-05-2011_1
		//MSTAGETTI_FI-11-05-2010_1
		//if (cfg.slitterScorer.isCompact2())
		if (cfg.slitterScorer.isCompact2() || cfg.slitterScorer.isTwin2())
		{
			if (_wcsicmp(SScommand.getPtr(),L"True")==0)
				wcscpy(commandPipe.command.twinCommand.data[0],L"noTrident");
			else
				wcscpy(commandPipe.command.twinCommand.data[0],L"");
		}
		else
			wcscpy(commandPipe.command.twinCommand.data[0],L"");

		commandPipe.command.twinCommand.cmd = NamedPipe::TwinCommand::SendCurrentOrder;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//SendNextOrder
	if (theSScommand.CompareNoCase(L"SendNextOrder"))
	{
		//MSTAGETTI_FI-11-05-2010_1
		if (cfg.slitterScorer.isCompact2())
		{
			if (_wcsicmp(SScommand.getPtr(),L"True")==0)
				wcscpy(commandPipe.command.twinCommand.data[0],L"noTrident");
			else
				wcscpy(commandPipe.command.twinCommand.data[0],L"");
		}
		else
			wcscpy(commandPipe.command.twinCommand.data[0],L"");
		
		commandPipe.command.twinCommand.cmd = NamedPipe::TwinCommand::SendNextOrder;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//SendNextP1Order
	if (theSScommand.CompareNoCase(L"SendNextP1Order"))
	{
		commandPipe.command.twinCommand.cmd = NamedPipe::TwinCommand::SendNextP1Order;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//SendNextP2Order
	if (theSScommand.CompareNoCase(L"SendNextP2Order"))
	{
		commandPipe.command.twinCommand.cmd = NamedPipe::TwinCommand::SendNextP2Order;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//SendNextP3Order
	if (theSScommand.CompareNoCase(L"SendNextP3Order"))
	{
		commandPipe.command.twinCommand.cmd = NamedPipe::TwinCommand::SendNextP3Order;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};
	//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
	//KK Data
	if (theSScommand.CompareNoCase(L"SendCleanCutData"))
	{
		commandPipe.command.twinCommand.cmd = NamedPipe::TwinCommand::SendCleanCutData;
		std::vector<String::StrType> args=SScommand.Tokenize(L",");
		wcscpy(commandPipe.command.twinCommand.data[1], args[0]);
		wcscpy(commandPipe.command.twinCommand.data[2], args[1]);

		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};	
	//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
	//GrindSlit
	if (theSScommand.CompareNoCase(L"GrindSlit"))
	{
		std::vector<String::StrType> args=SScommand.Tokenize(L",");
		for (size_t i=0; i<args.size(); i++)
			wcscpy(commandPipe.command.twinCommand.data[i+1], args[i]);
		if (args.size()==2)
			wcscpy(commandPipe.command.twinCommand.data[3], L"0");
    //agrassi_FI25-05-2011_1
		//C_RTCManager::RTCSyncroS.Write(&commandPipe);	

		commandPipe.command.twinCommand.cmd = NamedPipe::TwinCommand::GrindSlit;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};	

	//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
	//LubeSlit
	if (theSScommand.CompareNoCase(L"LubeSlit"))
	{
		std::vector<String::StrType> args=SScommand.Tokenize(L",");
		for (size_t i=0; i<args.size(); i++)
			wcscpy(commandPipe.command.twinCommand.data[i+1], args[i]);
		if (args.size()==2)
			wcscpy(commandPipe.command.twinCommand.data[3], L"0");
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	

		commandPipe.command.twinCommand.cmd = NamedPipe::TwinCommand::LubeSlit;
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};	
	
	//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
	//ModifyWebDirector
	if (theSScommand.CompareNoCase(L"ModifyWebDirector"))
	{
		commandPipe.command.twinCommand.cmd = NamedPipe::TwinCommand::ModifyWebDirector;

		std::vector<String::StrType> args= SScommand.Tokenize(L",");
		wcscpy(commandPipe.command.twinCommand.data[1], args[0]);
		wcscpy(commandPipe.command.twinCommand.data[2], args[1]);
		C_RTCManager::RTCSyncroS.Write(&commandPipe);	
		return true;
	};	

	logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Slitter Scorer wrong command: %s", theSScommand.getPtr());
	return false;
}

bool C_SlitterScorerCommands::ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.twinCommand.cmd = NamedPipe::TwinCommand::SendConfiguration;
	outbuf.command.twinCommand.config.Deserialize(stringConfig, Network::SEP);
	int errCode=F_CheckSlitterScorerConfig(&outbuf.command.slitterScorerCommand.config);
	if (errCode<CONFIG_OK)
		o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[§%s#%d], b[true])", L"SYCS_06", errCode);
	else
	{
		C_RTCManager::RTCSyncroS.Write(&outbuf);	
	}
	return true;
}
