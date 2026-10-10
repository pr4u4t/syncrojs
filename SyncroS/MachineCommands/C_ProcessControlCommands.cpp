#include ".\C_ProcessControlCommands.h"

#include "..\C_RTCManager.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;
extern Logger::C_Logger logConfig;

C_ProcessControlCommands::C_ProcessControlCommands(void)
{
}

C_ProcessControlCommands::~C_ProcessControlCommands(void)
{
}

bool C_ProcessControlCommands::CreateCommand(NamedPipe::CommandPipe& outbuf)
{
	switch ( outbuf.command.processControlCommand.cmd )
	{
		case NamedPipe::ProcessControlCommand::SendConfiguration:			// too big message, just send header
		case NamedPipe::ProcessControlCommand::SendHeaderConfiguration:
			return CreateHeaderConfigCommand(outbuf);
			break;

		case NamedPipe::ProcessControlCommand::Error:
			if(outbuf.isToView())
				o_socket.Send(Network::MSG_PAG_REF, L"pageManager._SetMessage(s[%s], b[true])", outbuf.getResponse());
			return false;
			break;
		default:
			return false;
	}
	return false;
}

bool C_ProcessControlCommands::ManageCommand(String::StrType page, String::StrType processControlCommands)
{
  //check if the command is an increase/decrease
	if(ManageButton(page, processControlCommands))
		return true;

	NamedPipe::CommandPipe commandPipe;
	commandPipe.Clean();
	commandPipe.addressee=NamedPipe::CommandPipe::ProcessControl;

	String::StrType theProcessControlCommand = processControlCommands.extractCommand();
	if (theProcessControlCommand.isEmpty())
		return false;

  //Value
  if(theProcessControlCommand.CompareNoCase(L"SendWarpUpDownValue"))
    return ParseSendWarpUpDownValueCommand(processControlCommands,commandPipe);
  if(theProcessControlCommand.CompareNoCase(L"SendWarpEndToEndValue"))
    return ParseSendWarpEndToEndValueCommand(processControlCommands,commandPipe);
  if(theProcessControlCommand.CompareNoCase(L"SendUserValue"))
		return ParseSendUserValueCommand(processControlCommands, commandPipe);
  if(theProcessControlCommand.CompareNoCase(L"ResetUserValue"))
		return ParseResetUserValueCommand(processControlCommands, commandPipe);

  //Force Zone Local Remote
  if(theProcessControlCommand.CompareNoCase(L"ForceLocalPrcBoardLevelNumber"))
    return ParseForceLocalPrcBoardLevelNumberCommand(processControlCommands,commandPipe);
  if(theProcessControlCommand.CompareNoCase(L"ForceRemotePrcBoardLevelNumber"))
    return ParseForceRemotePrcBoardLevelNumberCommand(processControlCommands,commandPipe);
  
	//SendConfiguration
	if(theProcessControlCommand.CompareNoCase(L"SendHeaderConfiguration"))
		return ParseConfigCommandHeader(processControlCommands, commandPipe);
	if(theProcessControlCommand.CompareNoCase(L"SendConfiguration"))
		return ParseConfigCommand(processControlCommands, commandPipe);
	if(theProcessControlCommand.CompareNoCase(L"SendWorkingPoint"))
		return ParseSaveWorkingPointCommand(commandPipe);

  //RFAZZI_FI25/08/2011_2. Aggiunto controllo PRC CloseLoop ON/OFF. 
	//ToggleAutoManCloseLoopWarpControl
	if(theProcessControlCommand.CompareNoCase(L"ToggleAutoManCloseLoopWarpControl"))
    return ParseToggleWarpCloseLoopEndToEndCommand(processControlCommands, commandPipe);

	logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"ProcessControl wrong command: %s", theProcessControlCommand.getPtr());
	return false;
}

bool C_ProcessControlCommands::getPipeCmd(String::StrType command, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop)
{
	return false;
}

bool C_ProcessControlCommands::ParseForceLocalPrcBoardLevelNumberCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
  int PrcBoardLevelNumber = stringConfig.ToInt();
  if(PrcBoardLevelNumber < 0)
  {
    outbuf.command.processControlCommand.cmd = NamedPipe::ProcessControlCommand::ForceAllZonetoLocal;
  }
  else
  {
    outbuf.command.processControlCommand.cmd = NamedPipe::ProcessControlCommand::ForceZoneNtoLocal;
	  outbuf.command.processControlCommand.warpValue = PrcBoardLevelNumber;
  }
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}

bool C_ProcessControlCommands::ParseForceRemotePrcBoardLevelNumberCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
  int PrcBoardLevelNumber = stringConfig.ToInt();
  if(PrcBoardLevelNumber < 0)
  {
    outbuf.command.processControlCommand.cmd = NamedPipe::ProcessControlCommand::ForceAllZonetoRemote;
  }
  else
  {
    outbuf.command.processControlCommand.cmd = NamedPipe::ProcessControlCommand::ForceZoneNtoRemote;
	  outbuf.command.processControlCommand.warpValue = PrcBoardLevelNumber;
  }
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}

bool C_ProcessControlCommands::CreateConfigCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[String::MAX_CHARS_CONFIG_MACHINE]=L"";
	outbuf.command.processControlCommand.config.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.PRCConfig.Config(%s)", cfg);
	
	return true;
}

bool C_ProcessControlCommands::CreateHeaderConfigCommand(NamedPipe::CommandPipe& outbuf)
{
	wchar_t cfg[String::MAX_CHARS_CONFIG_MACHINE]=L"";
	outbuf.command.processControlCommand.config.header.Serialize(cfg, Network::SEP);
	o_socket.Send(Network::MSG_DB_REF,	L"lineConfig.PRCHeaderConfig.Config(%s)", cfg);
	
	return true;
}

bool C_ProcessControlCommands::ParseConfigCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.processControlCommand.cmd = NamedPipe::ProcessControlCommand::SendConfiguration;
	outbuf.command.processControlCommand.config.Deserialize(stringConfig, Network::SEP);
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}

bool C_ProcessControlCommands::ParseConfigCommandHeader(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.processControlCommand.cmd = NamedPipe::ProcessControlCommand::SendHeaderConfiguration;
	outbuf.command.processControlCommand.config.header.Deserialize(stringConfig, Network::SEP);
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}

bool C_ProcessControlCommands::ParseSaveWorkingPointCommand(NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.processControlCommand.cmd = NamedPipe::ProcessControlCommand::SaveWorkingPoint;
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}

bool C_ProcessControlCommands::ParseSendWarpUpDownValueCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.processControlCommand.cmd = NamedPipe::ProcessControlCommand::SendWarpUpDownValue;
	outbuf.command.processControlCommand.warpValue = stringConfig.ToInt();
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}

bool C_ProcessControlCommands::ParseSendWarpEndToEndValueCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.processControlCommand.cmd = NamedPipe::ProcessControlCommand::SendWarpEndToEndValue;
	outbuf.command.processControlCommand.warpValue = stringConfig.ToInt();
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}

bool C_ProcessControlCommands::ParseResetUserValueCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
  int PrcBoardLevelNumber = stringConfig.ToInt();
  if(PrcBoardLevelNumber < 0)
  {
    outbuf.command.processControlCommand.cmd = NamedPipe::ProcessControlCommand::ResetAllZoneUserValue;
  }
  else
  {
    outbuf.command.processControlCommand.cmd = NamedPipe::ProcessControlCommand::ResetZoneNUserValue;
	  outbuf.command.processControlCommand.warpValue = PrcBoardLevelNumber;
  }
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}

bool C_ProcessControlCommands::ParseSendUserValueCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
	outbuf.command.processControlCommand.cmd = NamedPipe::ProcessControlCommand::SendUserValue;

  if(stringConfig.strsize() <= 0)
			return false;
		
  std::vector<String::StrType> cmd = stringConfig.Tokenize(Network::SEP);

  if(cmd.size() < 3)
    return false;

  if(cmd[0].SearchNoCase(L"wrapArm"))
  {
    outbuf.command.processControlCommand.userValue.wrapArmNumber = WrapArmNumber(cmd[1].ToInt());
    outbuf.command.processControlCommand.userValue.correction = cmd[2].ToInt();
  }
  else if(cmd[0].SearchNoCase(L"glueGap"))
  {
    outbuf.command.processControlCommand.userValue.glueGapNumber = GlueGapNumber(cmd[1].ToInt());
    outbuf.command.processControlCommand.userValue.correction = cmd[2].ToInt();
  }
  else if(cmd[0].SearchNoCase(L"steamShower"))
  {
    outbuf.command.processControlCommand.userValue.steamShowerNumber = SteamShowerNumber(cmd[1].ToInt());
    outbuf.command.processControlCommand.userValue.correction = cmd[2].ToInt();
  }
  else if(cmd[0].SearchNoCase(L"paperTension"))
  {
    outbuf.command.processControlCommand.userValue.paperTensionNumber = PaperTensionNumber(cmd[1].ToInt());
    outbuf.command.processControlCommand.userValue.correction = cmd[2].ToInt();
  }
  else if(cmd[0].SearchNoCase(L"ShoesPressure"))
  {
    outbuf.command.processControlCommand.userValue.dBHotSectionNumber = DBHotSectionNumber(cmd[1].ToInt());
    outbuf.command.processControlCommand.userValue.correction = cmd[2].ToInt();
  }
  else if(cmd[0].SearchNoCase(L"SteamSection"))
  {
    outbuf.command.processControlCommand.userValue.dBSteamGroupNumber = DBSteamGroupNumber(cmd[1].ToInt());
    outbuf.command.processControlCommand.userValue.correction = cmd[2].ToInt();
  }
  else if(cmd[0].SearchNoCase(L"sfmainsteam"))
  {
    outbuf.command.processControlCommand.userValue.sfMainSteamNumber = SFMainSteamNumber(cmd[1].ToInt());
    outbuf.command.processControlCommand.userValue.correction = cmd[2].ToInt();
  }
	
  C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}


//RFAZZI_FI25/08/2011_2. Aggiunto controllo PRC CloseLoop ON/OFF. 
bool C_ProcessControlCommands::ParseToggleWarpCloseLoopEndToEndCommand(String::StrType stringConfig, NamedPipe::CommandPipe& outbuf)
{
  if(stringConfig.strsize() <= 0)
			return false;
		
  std::vector<String::StrType> cmd = stringConfig.Tokenize(Network::SEP);

  if(cmd[0].SearchNoCase(L"warpUpDownAutoMan"))
    outbuf.command.processControlCommand.cmd = NamedPipe::ProcessControlCommand::ToggleAutoManCloseLoopUpDown;
  else
    outbuf.command.processControlCommand.cmd = NamedPipe::ProcessControlCommand::ToggleAutoManCloseLoopEndToEnd;
	C_RTCManager::RTCSyncroS.Write(&outbuf);
	return true;
}
