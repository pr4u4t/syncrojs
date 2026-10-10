#include ".\C_ScorerProfileCommands.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;

C_ScorerProfileCommands::C_ScorerProfileCommands(void)
{

}

C_ScorerProfileCommands::~C_ScorerProfileCommands(void)
{
}

bool C_ScorerProfileCommands::ManageCommand(String::StrType ScorerProfileCommand, NamedPipe::CommandPipe& outbuf)
{
	/*StrType command = ScorerProfileCommand.extractCommand();
	if (command.isEmpty())
		return false;*/

	if(ParseCommand(ScorerProfileCommand, outbuf))
		return true;
	else
	{
    logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Scorer Profile wrong command: %s", ScorerProfileCommand.getPtr());
		return false;
	}
}

bool C_ScorerProfileCommands::CreateCommand(NamedPipe::CommandPipe& inbuffer)
{

	String::StrType init;
	DWORD exec=ERROR_SUCCESS;

	switch ( inbuffer.command.scorerProfileCommand.cmd )
	{	
	//Insert or Mod command
	case NamedPipe::ScorerProfileCommand::Add:
	case NamedPipe::ScorerProfileCommand::Mod:
		init = (inbuffer.command.scorerProfileCommand.cmd == NamedPipe::ScorerProfileCommand::Mod) ? L"Mod" : L"Add";
		
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV, L"SCORER PROFILE Command: ADD ScorerId: %s",inbuffer.command.scorerProfileCommand.scorerProfileData.scoreId);

		do
			{
				wchar_t prof[500]={0};
				int cnt=0;
				cnt+=swprintf(prof+cnt, L"%s=%s%s", L"scoreId", inbuffer.command.scorerProfileCommand.scorerProfileData.scoreId,Network::SEP);
				cnt+=swprintf(prof+cnt, L"%s=%d%s", L"sClass", inbuffer.command.scorerProfileCommand.scorerProfileData.sClass,Network::SEP);
				cnt+=swprintf(prof+cnt, L"%s=%d%s", L"width", inbuffer.command.scorerProfileCommand.scorerProfileData.width_UDdmm,Network::SEP);
				cnt+=swprintf(prof+cnt, L"%s=%d%s", L"profileWidth", inbuffer.command.scorerProfileCommand.scorerProfileData.profileWidth_UDdmm,Network::SEP);
				cnt+=swprintf(prof+cnt, L"%s=%d%s", L"tipHeight", inbuffer.command.scorerProfileCommand.scorerProfileData.tipHeight_UDdmm,Network::SEP);
				cnt+=swprintf(prof+cnt, L"%s=%d%s", L"valleyDepth", inbuffer.command.scorerProfileCommand.scorerProfileData.valleyDepth_UDdmm,Network::SEP);
				cnt+=swprintf(prof+cnt, L"%s=%d%s", L"tipOffset", inbuffer.command.scorerProfileCommand.scorerProfileData.tipOffset_UDdmm,Network::SEP);
				cnt+=swprintf(prof+cnt, L"%s=%d%s", L"farValleyOffset", inbuffer.command.scorerProfileCommand.scorerProfileData.farValleyOffset_UDdmm,Network::SEP);
				cnt+=swprintf(prof+cnt, L"%s=%d%s", L"upperDiameter", inbuffer.command.scorerProfileCommand.scorerProfileData.upperDiameter_UDdmm,Network::SEP);
				cnt+=swprintf(prof+cnt, L"%s=%d%s", L"lowerDiameter", inbuffer.command.scorerProfileCommand.scorerProfileData.lowerDiameter_UDdmm,Network::SEP);
				exec=o_socket.Send(Network::MSG_DB_REF, L"scorerProfile.%s(%s)", init.getPtr(), prof);
			}while(exec!=ERROR_SUCCESS);

			if(inbuffer.isToView())
				o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s])",inbuffer.getResponse());

			break;

	//delete one record command
	case NamedPipe::ScorerProfileCommand::Del:

		logger.Info(__FUNCTION__,Logger::PRE_PIPE_RCV, L"SCORER PROFILE Command: DEL SINGLE ScorerId: %s",inbuffer.command.scorerProfileCommand.scorerProfileData.scoreId);

		o_socket.Send(Network::MSG_DB_REF,L"scorerProfile.Del(%s)",inbuffer.command.scorerProfileCommand.scorerProfileData.scoreId);

		if(inbuffer.isToView())
			o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s])",inbuffer.getResponse());
		break;

	//delete all record command
	case NamedPipe::ScorerProfileCommand::Clr:
	
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV, L"SCORER PROFILE Command: CLEAR");

		o_socket.Send(Network::MSG_DB_REF,L"scorerProfile.Clr()");

		if(inbuffer.isToView())
			o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s])",inbuffer.getResponse());
		break;

	case NamedPipe::ScorerProfileCommand::Error:

		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"SCORER PROFILE Command: ERROR Error : %s",inbuffer.getResponse());

		if(inbuffer.isToView())
			o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s], b[true])",inbuffer.getResponse());

		break;
	
	default:		
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"SCORER PROFILE Invalid Command");
		return false;
	}

	return true;
}

bool C_ScorerProfileCommands::ParseCommand(String::StrType theCommand, NamedPipe::CommandPipe& outbuf)
{

	String::StrType command = theCommand.extractCommand();
	if (command.isEmpty())
		return false;

	//scorerProfile.Del(TypeId);
	if(command.CompareNoCase(L"Del"))
	{
		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);
		if(token.size()!=1)
			return false;

		outbuf.command.scorerProfileCommand.cmd = NamedPipe::ScorerProfileCommand::Del;

		if(token[0].strsize()<=NUM_CHAR_SCORER_ID)
			swprintf(outbuf.command.scorerProfileCommand.scorerProfileData.scoreId,token[0].getPtr());
		else
			return false;

		return true;
	}

	//scorerProfile.Add(
	//      ScoreId,
	//			sClass,
	//			width,
	//			profileWidth,
	//			tipHeight,
	//			valleyDepth,
	//			tipOffset,
	//			farValleyOffset,
	//			upperDiameter,
	//			lowerDiameter)
	if(command.CompareNoCase(L"Add")||command.CompareNoCase(L"Mod"))
	{
		outbuf.command.scorerProfileCommand.cmd = command.CompareNoCase(L"Mod") ? NamedPipe::ScorerProfileCommand::Mod : outbuf.command.scorerProfileCommand.cmd = NamedPipe::ScorerProfileCommand::Add;

		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);
		if(token.size()!=10)
			return false;

		//scoreId
		if(token[0].strsize()<=NUM_CHAR_SCORER_ID)
			swprintf(outbuf.command.scorerProfileCommand.scorerProfileData.scoreId,token[0].getPtr());
		else
			return false;
		//sClass
		outbuf.command.scorerProfileCommand.scorerProfileData.sClass=(ScoreClass) token[1].ToInt();
		//width
		outbuf.command.scorerProfileCommand.scorerProfileData.width_UDdmm = token[2].ToInt();
		//profileWidth
		outbuf.command.scorerProfileCommand.scorerProfileData.profileWidth_UDdmm = token[3].ToInt();
		//tipHeight
		outbuf.command.scorerProfileCommand.scorerProfileData.tipHeight_UDdmm = token[4].ToInt();
		//valleyDepth
		outbuf.command.scorerProfileCommand.scorerProfileData.valleyDepth_UDdmm = token[5].ToInt();
		//tipOffset
		outbuf.command.scorerProfileCommand.scorerProfileData.tipOffset_UDdmm = token[6].ToInt();
		//farValleyOffset
		outbuf.command.scorerProfileCommand.scorerProfileData.farValleyOffset_UDdmm = token[7].ToInt();
		//upperDiameter
		outbuf.command.scorerProfileCommand.scorerProfileData.upperDiameter_UDdmm = token[8].ToInt();
		//lowerDiameter
		outbuf.command.scorerProfileCommand.scorerProfileData.lowerDiameter_UDdmm = token[9].ToInt();

		return true;
	}

	//scorerProfile.clr()
	if(command.CompareNoCase(L"Clr"))
	{
		outbuf.command.scorerProfileCommand.cmd = NamedPipe::ScorerProfileCommand::Clr;

		return true;
	}
	
	return false;
}


