#include ".\C_RTCManager.h"

//Logger object
extern Logger::C_Logger logger;

//Socket
extern Network::C_SocketTCP o_socket;

//Named Pipe objects
NamedPipe::C_NamedPipeClient C_RTCManager::RTCSyncroS(NamedPipe::PIPENAME_RTCSyncroS, NamedPipe::RTC_SIZE_PIPE);
//Generic objects command
C_GenericCommands C_RTCManager::genericCommands;
//Scorer Profile objects command
C_ScorerProfileCommands C_RTCManager::scorerProfileCommands;
//Rotary Format objects command
C_RotaryFormatCommands C_RTCManager::rotaryFormatCommands;
//Web Director objects command
C_WebDirectorCommands C_RTCManager::webDirectorCommands;
//Single Facer 1 objects command
C_SingleFacerCommands C_RTCManager::singleFacer1Commands(1);
//Single Facer 2 objects command
C_SingleFacerCommands C_RTCManager::singleFacer2Commands(2);
//Single Facer 3 objects command
C_SingleFacerCommands C_RTCManager::singleFacer3Commands(3);
//Pre-Haeters Tern objects command
C_PreHeatersTernCommands C_RTCManager::preHeatersTernCommands;
//Glue Unit objects command
C_GlueUnitCommands C_RTCManager::glueUnitCommands;
//Double Backer objects command
C_DoubleBackerCommands C_RTCManager::doubleBackerCommands;
//Twin400 objects command
C_Twin400Commands C_RTCManager::twin400Commands;
//SlitterScorer objects command
C_SlitterScorerCommands C_RTCManager::slitterScorerCommands;
//Rotary objects command
C_RotaryCommands C_RTCManager::rotaryCommands;
//Knife objects command
C_KnifeCommands C_RTCManager::knifeCommands;
//Stacker objects command
C_StackerCommands C_RTCManager::stacker1Commands(1);
C_StackerCommands C_RTCManager::stacker2Commands(2);
C_StackerCommands C_RTCManager::stacker3Commands(3);
//Mth object command
C_MthCommands C_RTCManager::mthCommands;
//Extern WEC object command
C_ExternWECCommands C_RTCManager::externWECCommands;
//Splicer objects command
C_SplicerCommands C_RTCManager::splicer1Commands(1);
C_SplicerCommands C_RTCManager::splicer2Commands(2);
C_SplicerCommands C_RTCManager::splicer3Commands(3);
C_SplicerCommands C_RTCManager::splicer4Commands(4);
C_SplicerCommands C_RTCManager::splicer5Commands(5);
C_SplicerCommands C_RTCManager::splicer6Commands(6);
C_SplicerCommands C_RTCManager::splicer7Commands(7);
C_ProcessControlCommands C_RTCManager::processControlCommands;
//alarm History Commands
C_AlarmHistoryCommands C_RTCManager::alarmHistoryCommands;
//Starter Commands
C_StarterCommands C_RTCManager::starterCommands;
C_SimulationCommands C_RTCManager::simulationCommands;

bool C_RTCManager::finished;

//ABello_FI05/10/2006_1 Fixed the bug for thread safe list
//std::list<NamedPipe::CommandPipe> C_RTCManager::ListCmd;
Utility::C_List<NamedPipe::CommandPipe> C_RTCManager::ListCmd;

C_RTCManager::C_RTCManager(void)
{
	pid_RTCNamedPipe = NULL;
}

C_RTCManager::~C_RTCManager(void)
{
}

void C_RTCManager::CloseRTCManager(void)
{
	logger.Info(__FUNCTION__, Logger::PRE_PIPE_INFO, L"Close RTC Named Pipe ");

	NamedPipe::CommandPipe outbuf;
	outbuf.Clean();
	outbuf.addressee=NamedPipe::CommandPipe::RTC;
	outbuf.command.myRTCCommand.cmd=NamedPipe::RTCCommand::Exit;
	
	//send the command to DBMS
	if(RTCSyncroS.Write(&outbuf)!=ERROR_SUCCESS)
	{
		::TerminateThread(pid_RTCNamedPipe, NULL);
		logger.Warning(__FUNCTION__, L"Exit command to RTC not sent");
	}
	else
	{
		//wait for the thread dbms Named pipe
		DWORD dwWaitResult = ::WaitForSingleObject(pid_RTCNamedPipe,5000L); // five-second time-out interval
		
		switch ( dwWaitResult )
		{
		case WAIT_OBJECT_0:
			logger.Info(__FUNCTION__, Logger::PRE_EVENT_INFO, L"RTC thread is closed: tutto OK");
			break;
		default:
			logger.Warning(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"Force close RTC Named Pipe");
			::TerminateThread(pid_RTCNamedPipe, NULL);
			break;
		};
	}

	::CloseHandle(pid_RTCNamedPipe);
	pid_RTCNamedPipe=NULL;
}

void C_RTCManager::AlignRTC(void)
{
	logger.Info(__FUNCTION__, Logger::PRE_PIPE_INFO, L"Align RTC");

	NamedPipe::CommandPipe outbuf;
	outbuf.Clean();
	outbuf.addressee=NamedPipe::CommandPipe::RTC;
	outbuf.command.myRTCCommand.cmd = NamedPipe::RTCCommand::Align;
	
	//send the command to DBMS
	RTCSyncroS.Write(&outbuf);
}

DWORD WINAPI C_RTCManager::Manage_NamedPipeRTC(LPVOID  lpParam)
{

	DWORD  ret;
	NamedPipe::CommandPipe inbuf;

	//TODO:qui dico al client che posso iniziare a gestire i comandi real Time
	//o_socket.Send(Network::MSG_PAG_REF,L"statusBar_statusBar._Clear()");

	C_RTCManager::finished=false;

	while(!C_RTCManager::finished)
	{
		inbuf.Clean();
		//read from pipe
		ret = RTCSyncroS.Read(&inbuf);

		if(C_RTCManager::finished)
			break;
		
		if(ret==ERROR_SUCCESS)
			//finished = C_RTCManager::CreateCommands(inbuf);
			C_RTCManager::ListCmd.push_back(inbuf);
		else
		{
			logger.Severe(__FUNCTION__, Logger::PRE_PIPE_ERROR ,L"Named Pipe with RTC Error ... %d",ret);
			if(ERROR_BROKEN_PIPE==ret)
			{
				logger.Severe(__FUNCTION__, Logger::PRE_PIPE_ERROR ,L"Named Pipe Down Restart RTC .... %d",ret);
				/////////////TODO: Bloccare l'uso di tutti i data Base mandando un messaggio di errore

				//TODO:qui dico al client che deve bloccare l'uso di tutti i comandi real Time

				RTCSyncroS.myCreateFile();
								
				//TODO:qui dico al client che posso ri-iniziare a gestire i comandi real Time

				logger.Severe(__FUNCTION__, Logger::PRE_PIPE_INFO ,L"OK: Named Pipe found.");
			}
			else
				Sleep(1000);
		}
	};

	logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Manage_NamedPipeRTC exit thread");

	return ERROR_SUCCESS;
}

DWORD C_RTCManager::createPipe()
{
	DWORD errorPipe = RTCSyncroS.myCreateFile();
	if(errorPipe==ERROR_SUCCESS)
	{
		pid_RTCNamedPipe=::CreateThread(NULL,0,Manage_NamedPipeRTC,NULL,0,NULL);
		if (pid_RTCNamedPipe == NULL)
		{
			DWORD error = GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Cannot create the Real Time NamedPipe thread ... ",error);
			return error;
		}
	}
	else
	{
		logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR ,L"Cannot create the Real Time NamedPipe %s ... %d", NamedPipe::PIPENAME_RTCSyncroS,errorPipe);
		return errorPipe;
	}

	return ERROR_SUCCESS;
}

bool C_RTCManager::CreateRTCCommand(NamedPipe::CommandPipe& inbuf)
{
	switch ( inbuf.command.myRTCCommand.cmd )
	{
		case NamedPipe::RTCCommand::Exit:
		case NamedPipe::RTCCommand::Align:
			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"lineConfig.SetVer(%d %d %d %d %d %d %d %d)",
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.lineConfigVer.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.lineConfigVer.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.lineConfigVer.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.lineConfigVer.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.lineConfigVer.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.lineConfigVer.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.lineConfigVer.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.lineConfigVer.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"scorerProfile.SetVer(%d %d %d %d %d %d %d %d)",
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.scorerProfileVer.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.scorerProfileVer.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.scorerProfileVer.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.scorerProfileVer.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.scorerProfileVer.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.scorerProfileVer.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.scorerProfileVer.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.scorerProfileVer.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"rotaryFormat.SetVer(%d %d %d %d %d %d %d %d)",
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.rotaryFormatVer.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.rotaryFormatVer.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.rotaryFormatVer.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.rotaryFormatVer.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.rotaryFormatVer.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.rotaryFormatVer.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.rotaryFormatVer.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.rotaryFormatVer.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(SYCHW%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycHwVer.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycHwVer.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycHwVer.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycHwVer.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycHwVer.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycHwVer.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycHwVer.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycHwVer.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(SYCSW%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycSwVer.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycSwVer.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycSwVer.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycSwVer.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycSwVer.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycSwVer.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycSwVer.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycSwVer.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(STK1%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker1Ver.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker1Ver.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker1Ver.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker1Ver.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker1Ver.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker1Ver.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker1Ver.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker1Ver.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(STK2%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker2Ver.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker2Ver.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker2Ver.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker2Ver.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker2Ver.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker2Ver.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker2Ver.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker2Ver.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(STK3%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker3Ver.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker3Ver.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker3Ver.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker3Ver.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker3Ver.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker3Ver.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker3Ver.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker3Ver.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(KN%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmKnifeVer.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmKnifeVer.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmKnifeVer.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmKnifeVer.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmKnifeVer.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmKnifeVer.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmKnifeVer.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmKnifeVer.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(SS%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSlitterScorerVer.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSlitterScorerVer.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSlitterScorerVer.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSlitterScorerVer.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSlitterScorerVer.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSlitterScorerVer.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSlitterScorerVer.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSlitterScorerVer.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(RSH%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmRotaryVer.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmRotaryVer.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmRotaryVer.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmRotaryVer.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmRotaryVer.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmRotaryVer.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmRotaryVer.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmRotaryVer.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(DB%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmDoubleBackerVer.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmDoubleBackerVer.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmDoubleBackerVer.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmDoubleBackerVer.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmDoubleBackerVer.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmDoubleBackerVer.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmDoubleBackerVer.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmDoubleBackerVer.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(GU%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmGlueUnitVer.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmGlueUnitVer.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmGlueUnitVer.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmGlueUnitVer.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmGlueUnitVer.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmGlueUnitVer.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmGlueUnitVer.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmGlueUnitVer.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(PHT%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmPreHeatersTernVer.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmPreHeatersTernVer.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmPreHeatersTernVer.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmPreHeatersTernVer.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmPreHeatersTernVer.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmPreHeatersTernVer.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmPreHeatersTernVer.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmPreHeatersTernVer.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(SF1%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer1Ver.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer1Ver.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer1Ver.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer1Ver.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer1Ver.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer1Ver.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer1Ver.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer1Ver.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(SF2%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer2Ver.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer2Ver.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer2Ver.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer2Ver.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer2Ver.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer2Ver.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer2Ver.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer2Ver.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(SF3%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer3Ver.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer3Ver.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer3Ver.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer3Ver.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer3Ver.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer3Ver.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer3Ver.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer3Ver.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(SPL1%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer1Ver.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer1Ver.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer1Ver.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer1Ver.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer1Ver.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer1Ver.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer1Ver.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer1Ver.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(SPL2%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer2Ver.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer2Ver.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer2Ver.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer2Ver.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer2Ver.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer2Ver.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer2Ver.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer2Ver.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(SPL3%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer3Ver.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer3Ver.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer3Ver.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer3Ver.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer3Ver.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer3Ver.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer3Ver.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer3Ver.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(SPL4%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer4Ver.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer4Ver.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer4Ver.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer4Ver.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer4Ver.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer4Ver.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer4Ver.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer4Ver.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(SPL5%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer5Ver.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer5Ver.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer5Ver.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer5Ver.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer5Ver.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer5Ver.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer5Ver.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer5Ver.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(SPL6%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer6Ver.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer6Ver.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer6Ver.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer6Ver.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer6Ver.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer6Ver.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer6Ver.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer6Ver.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(SPL7%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer7Ver.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer7Ver.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer7Ver.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer7Ver.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer7Ver.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer7Ver.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer7Ver.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer7Ver.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(MTH%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmMthVer.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmMthVer.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmMthVer.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmMthVer.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmMthVer.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmMthVer.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmMthVer.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmMthVer.wMilliseconds);

			//N.B. the System Time is the GMT orario
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(EXTWEC%s%d %d %d %d %d %d %d %d)",
				Network::SEP,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmExternWECVer.wDayOfWeek,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmExternWECVer.wDay,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmExternWECVer.wMonth,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmExternWECVer.wYear,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmExternWECVer.wHour,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmExternWECVer.wMinute,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmExternWECVer.wSecond,
				inbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmExternWECVer.wMilliseconds);

			//TODO : Continuare qui con gli allarmi

			//write on file the alarm
			o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.SetVer(ALL)");

			if(inbuf.command.myRTCCommand.cmd == NamedPipe::RTCCommand::Exit)
        return true;
			else
				return false;
			break;
		case NamedPipe::RTCCommand::DbVer:
			//Data Base
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.lineConfigSameVer)
				o_socket.Send(Network::MSG_DB_REF,L"lineConfig.Load()");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.scorerProfileSameVer)
				o_socket.Send(Network::MSG_DB_REF,L"scorerProfile.Load()");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.rotaryFormatSameVer)
				o_socket.Send(Network::MSG_DB_REF,L"rotaryFormat.Load()");
			//Alarm history
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmSycSwSameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(SYCSW)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmSycHwSameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(SYCHW)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmStacker1SameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(STK1)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmStacker2SameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(STK2)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmStacker3SameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(STK3)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmKnifeSameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(KN)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmSlitterScorerSameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(SS)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmRotarySameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(RSH)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmDoubleBackerSameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(DB)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmGlueUnitSameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(GU)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmPreHeatersTernSameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(PHT)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmSingleFacer1SameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(SF1)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmSingleFacer2SameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(SF2)");	
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmSingleFacer3SameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(SF3)");	
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmSplicer1SameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(SPL1)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmSplicer2SameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(SPL2)");	
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmSplicer3SameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(SPL3)");				
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmSplicer4SameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(SPL4)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmSplicer5SameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(SPL5)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmSplicer6SameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(SPL6)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmSplicer7SameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(SPL7)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmMthSameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(MTH)");
			if(inbuf.command.myRTCCommand.RTCDataBuffer.RTCSameVer.alarmExternWECSameVer)
				o_socket.Send(Network::MSG_DB_REF,L"alarmHistory.LoadMachine(EXTWEC)");

			//Todo : aggiungere qui il resto
			return false;
			break;

		//Config
		case NamedPipe::RTCCommand::configOperator:
		case NamedPipe::RTCCommand::configEncoders:
		case NamedPipe::RTCCommand::configSyncro:
		case NamedPipe::RTCCommand::configAOC:
		case NamedPipe::RTCCommand::configWEC:
		case NamedPipe::RTCCommand::configSpeed:
		case NamedPipe::RTCCommand::configSpeedReference:
		//RFazzi_FI28/09/2006_2.Added Office LineConfig section
		case NamedPipe::RTCCommand::configOffice:
		//RFazzi_FI29/09/2006_1.Moved the Printers Settings into LineConfig section
		case NamedPipe::RTCCommand::configPrinters:
		{
			C_RTCManager::genericCommands.CreateCommand(inbuf);
			return false;
			break;
		}

		case NamedPipe::RTCCommand::SendConfiguration:
		{
			wchar_t cfg[String::MAX_CHARS_CONFIG_MACHINE]=L"";
			inbuf.command.myRTCCommand.RTCDataBuffer.macEnable.Serialize(cfg, Network::SEP);
			o_socket.Send(Network::MSG_DB_REF, L"lineConfig.Config(%s)", cfg);
			return false;
			break;
		}
		case NamedPipe::RTCCommand::Error:
			if(inbuf.isToView())
			{
				logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"Receved an error from RTC ... %s", inbuf.getResponse());
				o_socket.Send(Network::MSG_PAG_REF, L"pageManager._SetMessage(s[%s], b[true])", inbuf.getResponse());
			}
			return false;
			break;
		default:
			logger.Warning(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"Invalid Command Request from RTC");
			return false;
			break;
	}
	return false;
}

DWORD C_RTCManager::CreateCommands(NamedPipe::CommandPipe& inbuf)
{
	switch(inbuf.addressee)
	{
	case NamedPipe::CommandPipe::SingleFacer1:
		singleFacer1Commands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::SingleFacer2:
		singleFacer2Commands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::SingleFacer3:
		singleFacer3Commands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::Splicer1:
		splicer1Commands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::Splicer2:
		splicer2Commands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::Splicer3:
		splicer3Commands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::Splicer4:
		splicer4Commands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::Splicer5:
		splicer5Commands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::Splicer6:
		splicer6Commands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::Splicer7:
		splicer7Commands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::PreHeatersTern:
		preHeatersTernCommands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::GlueUnit:
		glueUnitCommands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::DoubleBacker:
		doubleBackerCommands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::RotaryCS:
		rotaryCommands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::SlitterScorer:
		twin400Commands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::SlitterScorerSupervisor:
		slitterScorerCommands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::WebDirector:
		webDirectorCommands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;	
	case NamedPipe::CommandPipe::Knife:
		knifeCommands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::Stacker1:
		stacker1Commands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::Stacker2:
		stacker2Commands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::Stacker3:
		stacker3Commands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::ExternWEC:
		externWECCommands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::Mth:
		mthCommands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::RTC:
		//If return true Stop Client
		if(C_RTCManager::CreateRTCCommand(inbuf))
			return ERROR_SHUTDOWN_IN_PROGRESS;
		else
			return ERROR_SUCCESS; 
		break;
	case NamedPipe::CommandPipe::ScorerProfile:
		scorerProfileCommands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::RotaryFormat:
		rotaryFormatCommands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::AlarmHistory:
		alarmHistoryCommands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::Starter:
		starterCommands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	case NamedPipe::CommandPipe::ProcessControl:
		processControlCommands.CreateCommand(inbuf);
		return ERROR_SUCCESS;
		break;
	default:
		logger.Severe(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"Invalid Command no valid addressee found (Addresse %d)",inbuf.addressee);
		return ERROR_BAD_COMMAND;
		break;
	}

	return ERROR_BAD_COMMAND;
}

bool C_RTCManager::ChooseMachine(String::StrType command)
{
	command.Trim();

	logger.Info(__FUNCTION__, L"RTC Manager has received command: %s", command.getPtr());

	String::StrType machine=command.extractFirstToken(L".");

	//DataBase LineConfig update Commands
	if(machine.CompareNoCase(L"RTCManager"))
	{
		//Init message
		NamedPipe::CommandPipe outbuf;
		outbuf.Clean();
		//set the Db to
		outbuf.addressee = NamedPipe::CommandPipe::RTC;

		std::vector<String::StrType> token = command.Tokenize(Network::SEP);

		if(token.size() < 25)
			return false;
		
		//TODO aggiungere qui gli allarmi per le altre macchine

		int k=0;

		//LineConfigVer (0)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.lineConfigVer))
			return false;

		k++;
		//scorerProfileVer (1)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.scorerProfileVer))
			return false;

		k++;
		//RotaryFormatVer (2)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.rotaryFormatVer))
			return false;

		k++;
		//AlarmHistoryHardwareVer (3)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycHwVer))
			return false;

		k++;
		//AlarmHistorySoftwareVer (4)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSycSwVer))
			return false;

		k++;
		//AlarmHistoryStacker1Ver (5)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker1Ver))
			return false;

		k++;
		//AlarmHistoryStacker2Ver (6)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker2Ver))
			return false;
		
		k++;
		//AlarmHistoryStacker3Ver (7)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmStacker3Ver))
			return false;

		k++;
		//AlarmHistoryKnifeVer (8)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmKnifeVer))
			return false;

		k++;
		//AlarmHistorySlitterScorerVer (9)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSlitterScorerVer))
			return false;

		k++;
		//AlarmHistoryRotaryVer (10)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmRotaryVer))
			return false;

		k++;
		//AlarmHistoryDoubleBackerVer (11)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmDoubleBackerVer))
			return false;

		k++;
		//AlarmHistoryGlueUnitVer (12)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmGlueUnitVer))
			return false;

		k++;
		//AlarmHistoryPreHeatersTernVer (13)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmPreHeatersTernVer))
			return false;

		k++;
		//AlarmHistorySingleFacer1Ver (14)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer1Ver))
			return false;

		k++;
		//AlarmHistorySingleFacer2Ver (15)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer2Ver))
			return false;

		k++;
		//AlarmHistorySingleFacer3Ver (16)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSingleFacer3Ver))
			return false;

		k++;
		//AlarmHistorySplicer1Ver (17)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer1Ver))
			return false;

		k++;
		//AlarmHistorySplicer2Ver (18)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer2Ver))
			return false;

		k++;
		//AlarmHistorySplicer3Ver (19)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer3Ver))
			return false;

		k++;
		//AlarmHistorySplicer4Ver (20)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer4Ver))
			return false;

		k++;
		//AlarmHistorySplicer5Ver (21)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer5Ver))
			return false;

		k++;
		//AlarmHistorySplicer6Ver (22)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer6Ver))
			return false;

		k++;
		//AlarmHistorySplicer7Ver (23)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmSplicer7Ver))
			return false;

		k++;
		//AlarmHistoryMTHVer (24)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmMthVer))
			return false;

		k++;
		//AlarmHistoryExternWECVer (25)
		if(!token[k].getDateTime(&outbuf.command.myRTCCommand.RTCDataBuffer.RTCDbVer.alarmExternWECVer))
			return false;

		//TODO aggiungere qui gli allarmi per le altre macchine: attenzione ad aumentare il controllo
		//sulla token size all'inizio di questa funzione altrimenti il comando viene ignorato!

		outbuf.command.myRTCCommand.cmd = NamedPipe::RTCCommand::DbVer;

    logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"Alarm Check Version");

		//send the command to DBMS
		RTCSyncroS.Write(&outbuf);

		return true;
	}

	//DataBase LineConfig update Commands
	if(machine.CompareNoCase(L"starter"))
	{
		//Init message
		NamedPipe::CommandPipe outbuf;
		outbuf.Clean();
		//set the Db to
		outbuf.addressee = NamedPipe::CommandPipe::Starter;
		
		String::StrType commandStarter = command.extractCommand();
		if (command.isEmpty())
			return false;

		if(commandStarter.CompareNoCase("PulseDigiout"))
		{
			outbuf.command.starterCommand.cmd = NamedPipe::StarterCommand::PulseDigiout;
			swprintf(outbuf.command.starterCommand.data[0],L"%s",command.getPtr());
			RTCSyncroS.Write(&outbuf);
			return true;
		}

		if(commandStarter.CompareNoCase("SetDigiout"))
		{
			outbuf.command.starterCommand.cmd = NamedPipe::StarterCommand::SetDigiout;
			swprintf(outbuf.command.starterCommand.data[0],L"%s",command.getPtr());
			RTCSyncroS.Write(&outbuf);
			return true;
		}

		if(commandStarter.CompareNoCase("ClearDigiout"))
		{
			outbuf.command.starterCommand.cmd = NamedPipe::StarterCommand::ClearDigiout;
			swprintf(outbuf.command.starterCommand.data[0],L"%s",command.getPtr());
			RTCSyncroS.Write(&outbuf);
			return true;
		}

		return false;
	}

	//DataBase LineConfig update Commands
	if(machine.CompareNoCase(L"lineConfig"))
	{
		//Init message
		NamedPipe::CommandPipe outbuf;
		outbuf.Clean();
		//set the Db to
		outbuf.addressee = NamedPipe::CommandPipe::RTC;

		if(this->ParseLineConfigCommand(command,outbuf))
		{
      logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"LineConfig Check Version");

			//send the command to DBMS
			RTCSyncroS.Write(&outbuf);
			return true;
		}
		else
			return false;
	}

	//SCORER PROFILE
	if(machine.CompareNoCase(L"scorerProfile"))
	{
		//Init message
		NamedPipe::CommandPipe outbuf;
		outbuf.Clean();
		//set the Db to
		outbuf.addressee = NamedPipe::CommandPipe::RTC;

		if(this->scorerProfileCommands.ManageCommand(command,outbuf))
		{
			//send the command to DBMS
			RTCSyncroS.Write(&outbuf);
			return true;
		}
		else
			return false;
	}	

	if(machine.SearchNoCase(L"operator"))
		return genericCommands.ManageOperatorCommand(command);

	if(machine.SearchNoCase(L"syncro"))
		return genericCommands.ManageSyncroCommand(command);

	if(machine.SearchNoCase(L"aoc"))
		return genericCommands.ManageAOCCommand(command);

	//RFazzi_FI28/09/2006_2.Added Office LineConfig section
	if(machine.SearchNoCase(L"office"))
		return genericCommands.ManageOfficeCommand(command);

	if(machine.SearchNoCase(L"ProcessControl"))
		return processControlCommands.ManageCommand(machine, command);

	//RFazzi_FI29/09/2006_1.Moved the Printers Settings into LineConfig section
	if(machine.SearchNoCase(L"printers"))
		return genericCommands.ManagePrintersCommand(command);

	if(machine.SearchNoCase(L"TaskWec"))
		return genericCommands.ManageWECCommand(command);

	if(machine.SearchNoCase(L"Speed"))
		return genericCommands.ManageSpeedCommand(command);

	if(machine.SearchNoCase(L"References"))
		return genericCommands.ManageSpeedReferenceCommand(command);

	if(machine.SearchNoCase(L"Encoders"))
		return genericCommands.ManageEncodersCommand(command);

	if (machine.SearchNoCase(L"SingleFacer1") || machine.SearchNoCase(L"Bridge1") || machine.SearchNoCase(L"wetEndSupervisor"))
		return singleFacer1Commands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"SingleFacer2") || machine.SearchNoCase(L"Bridge2") || machine.SearchNoCase(L"wetEndSupervisor"))
		return singleFacer2Commands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"SingleFacer3") || machine.SearchNoCase(L"Bridge3") || machine.SearchNoCase(L"wetEndSupervisor"))
		return singleFacer3Commands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"Splicer1"))
		return splicer1Commands.ManageCommand(machine, command);
	
	if (machine.SearchNoCase(L"Splicer2"))
		return splicer2Commands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"Splicer3"))
		return splicer3Commands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"Splicer4"))
		return splicer4Commands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"Splicer5"))
		return splicer5Commands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"Splicer6"))
		return splicer6Commands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"Splicer7"))
		return splicer7Commands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"preHeaterTern") || machine.SearchNoCase(L"wetEndSupervisor"))
		return preHeatersTernCommands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"glueUnit") || machine.SearchNoCase(L"wetEndSupervisor"))
		return glueUnitCommands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"doubleBacker") || machine.SearchNoCase(L"wetEndSupervisor"))
		return doubleBackerCommands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"Rotary"))
		return rotaryCommands.ManageCommand(command);

	if (machine.SearchNoCase(L"twin400"))
		return twin400Commands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"slitterScorer"))
		return slitterScorerCommands.ManageCommand(machine, command);
	
	if (machine.SearchNoCase(L"WebDirector"))
		return webDirectorCommands.ManageCommand(command);

	if (machine.SearchNoCase(L"Knife"))
		return knifeCommands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"Stacker1"))
		return stacker1Commands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"Stacker2"))
		return stacker2Commands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"Stacker3"))
		return stacker3Commands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"ExternWEC"))
		return externWECCommands.ManageCommand(machine, command);

	if (machine.SearchNoCase(L"Simulation"))
		return simulationCommands.ManageCommand(command);

	return false;
}

bool C_RTCManager::ParseLineConfigCommand(String::StrType theCommand, NamedPipe::CommandPipe &outbuf)
{
	String::StrType command = theCommand.extractCommand();
	if (command.isEmpty())
		return false;

	if(command.CompareNoCase(L"SendConfiguration"))
		{
			outbuf.command.myRTCCommand.cmd = NamedPipe::RTCCommand::SendConfiguration;
			outbuf.command.myRTCCommand.RTCDataBuffer.macEnable.Deserialize(theCommand, Network::SEP);
		}
	return true;
}