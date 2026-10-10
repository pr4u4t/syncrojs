#include ".\C_RotaryFormatCommands.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;

C_RotaryFormatCommands::C_RotaryFormatCommands(void)
{

}

C_RotaryFormatCommands::~C_RotaryFormatCommands(void)
{
}

//bool C_RotaryFormatCommands::ManageCommand(StrType RotaryFormatCommand, CommandPipe& outbuf)
//{
//	/*StrType command = RotaryFormatCommand.extractCommand();
//	if (command.isEmpty())
//		return false;*/
//
//	if(ParseCommand(RotaryFormatCommand, outbuf))
//		return true;
//	else
//	{
//    logger.Warning(__FUNCTION__, L"P <-> ERROR: Rotary Format wrong command: %s", RotaryFormatCommand);
//		return false;
//	}
//}

bool C_RotaryFormatCommands::CreateCommand(NamedPipe::CommandPipe& inbuffer)
{

	String::StrType init;
	DWORD exec=ERROR_SUCCESS;

	switch ( inbuffer.command.rotaryFormatCommand.cmd )
	{	
	//Insert or Mod command
	case NamedPipe::RotaryFormatCommand::Add:
	case NamedPipe::RotaryFormatCommand::Mod:
		init = (inbuffer.command.rotaryFormatCommand.cmd == NamedPipe::RotaryFormatCommand::Mod) ? L"Mod" : L"Add";
		
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV, L"RotaryFormat Command: ADD format: %d",inbuffer.command.rotaryFormatCommand.rotaryFormatData.format);

		do
			{
				wchar_t format[500]={0};
				int cnt=0;
				cnt+=swprintf(format+cnt, L"%s=%d%s", L"format", inbuffer.command.rotaryFormatCommand.rotaryFormatData.format,Network::SEP);
				cnt+=swprintf(format+cnt, L"%s=%s%s", L"ctsPresent", (inbuffer.command.rotaryFormatCommand.rotaryFormatData.ctsPresent?L"true":L"false"),Network::SEP);
				cnt+=swprintf(format+cnt, L"%s=%d%s", L"sectorsNum", inbuffer.command.rotaryFormatCommand.rotaryFormatData.sectorsNum,Network::SEP);
				cnt+=swprintf(format+cnt, L"%s=%d%s", L"sectorWidth", inbuffer.command.rotaryFormatCommand.rotaryFormatData.sectorWidth,Network::SEP);
				cnt+=swprintf(format+cnt, L"%s=%d%s", L"edgeSectorWidth", inbuffer.command.rotaryFormatCommand.rotaryFormatData.edgeSectorWidth,Network::SEP);
				cnt+=swprintf(format+cnt, L"%s=%d%s", L"sectorsDist", inbuffer.command.rotaryFormatCommand.rotaryFormatData.sectorsDist,Network::SEP);
				cnt+=swprintf(format+cnt, L"%s=%d%s", L"firstCentralSector", inbuffer.command.rotaryFormatCommand.rotaryFormatData.firstCentralSector,Network::SEP);
				exec=o_socket.Send(Network::MSG_DB_REF, L"rotaryFormat.%s(%s)", init.getPtr(), format);
		
			}while(exec!=ERROR_SUCCESS);

			if(inbuffer.isToView())
				o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s])",inbuffer.getResponse());

			break;

	default:		
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"RotaryFormat Invalid Command");
		return false;
	}

	return true;
}
