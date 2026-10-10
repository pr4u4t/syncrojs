#include "C_CommunicationManager.h"

//Logger include
#include "..\Shared\C_Logger.h"



//Logger object
extern Logger::C_Logger logger;

//Socket
extern Network::C_SocketTCP o_socket;

C_DBManager C_CommunicationManager::dbManager; //The Db Manager
C_RTCManager C_CommunicationManager::RTCManager; //The Real Time Manager

bool C_CommunicationManager::stopCommandFromDBMS;
bool C_CommunicationManager::stopCommandFromRTC;

int C_CommunicationManager::errorNumberDBMS; 
int C_CommunicationManager::errorNumberRTC; 
int C_CommunicationManager::errorAllowed;

C_CommunicationManager::C_CommunicationManager()
{
	//create a semaphore for sinchronozation
	this->hCommunicationManagerMutex = ::CreateMutex (NULL, FALSE, NULL);
	C_CommunicationManager::stopCommandFromDBMS = true;
	C_CommunicationManager::stopCommandFromRTC = true;

	errorNumberRTC = 0; 
	errorNumberDBMS = 0; 
	errorAllowed = 5;
}

C_CommunicationManager::~C_CommunicationManager(void)
{
	//Close the semaphore
	::CloseHandle(this->hCommunicationManagerMutex);
}

void C_CommunicationManager::UpdateAllDB()
{
	//Update all XML DB
	if(!dbManager.UpdateAllDB())
		logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"ERROR: Cannot UpdateAllDB");

}

DWORD WINAPI C_CommunicationManager::Manage_CommandFromDBMS(LPVOID  lpParam)
{
	NamedPipe::DataBasePipe inbufDB;
	bool finished=false;

	do
	{
		//Check if the Client is too slow
		if(C_CommunicationManager::dbManager.ListDB.size() > 10000)
		{
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"SyncroS Automatic shutdown: The queue with DBMS is > 10000");
			exit(0);
		}

		if(C_CommunicationManager::stopCommandFromDBMS)
		{
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Communication Error Stop the queue management with DBMS, queue size %d ... Sleep for 1 sec",C_CommunicationManager::dbManager.ListDB.size());
			::Sleep(1000);
			continue;
		}

		//DATABASE Queue Management
		if(C_CommunicationManager::dbManager.ListDB.size() > 0)
		{
			inbufDB.CleanDataBasePipe();
			//ABello_FI05/10/2006_1 Fixed the bug for thread safe list
			C_CommunicationManager::dbManager.ListDB.front(inbufDB);
			logger.Fine(__FUNCTION__, L"Found command in queue: executing");
			
			CTime start = CTime::GetCurrentTime(); 			
			DWORD res = C_CommunicationManager::dbManager.CreateCommands(inbufDB);
			logger.Fine(__FUNCTION__, L"Command created and sent to client");
			switch(res)
			{
			case ERROR_SUCCESS:
				C_CommunicationManager::dbManager.ListDB.pop_front();
				break;

			case ERROR_SHUTDOWN_IN_PROGRESS:
				C_CommunicationManager::dbManager.finished = true;
				C_CommunicationManager::dbManager.ListDB.pop_front();
				break;

			case ERROR_BAD_COMMAND:
				C_CommunicationManager::dbManager.ListDB.pop_front();
				errorNumberDBMS = errorNumberDBMS + 1;
				logger.Severe(__FUNCTION__,Logger::PRE_SOCK_ERROR, L"Invalid Command from DBMS queue (size %d) is number %d / %d", C_CommunicationManager::dbManager.ListDB.size(), errorNumberDBMS, errorAllowed);
				if(errorNumberDBMS > errorAllowed)
					exit(0);
				break;

			default:
				logger.Severe(__FUNCTION__,Logger::PRE_SOCK_ERROR, L"Cannot send command NO pop_front from DBMS queue");
				break;
			}

			CTime end = CTime::GetCurrentTime();
			CTimeSpan tsetup = end - start;

			if(tsetup.GetTotalSeconds() > 5)
				logger.Warning(__FUNCTION__,Logger::PRE_NULL, L"CreateCommands DBMS very Slow ... %d secs", tsetup.GetTotalSeconds());
		}
		else //NO command to execute
      ::Sleep(20);

		finished = C_CommunicationManager::dbManager.finished;

	}while(!finished);

	logger.Severe(__FUNCTION__,Logger::PRE_NULL, L"Exit Thread for DBMS Queue management");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_CommunicationManager::Manage_CommandFromRTC(LPVOID  lpParam)
{
	NamedPipe::CommandPipe inbufCmd;
	bool finished=false;

	do
	{
		//Check if the Client is too slow
		if(C_CommunicationManager::RTCManager.ListCmd.size() > 10000)
		{
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"SyncroS Automatic shutdown: The queue with RTC is > 10000");
			exit(0);
		}

		if(C_CommunicationManager::stopCommandFromRTC)
		{
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Communication Error Stop the queue management with RTC, queue size %d ... Sleep for 1 sec",C_CommunicationManager::RTCManager.ListCmd.size());
			::Sleep(1000);
			continue;
		}

		//RTC Queue Management
		if(C_CommunicationManager::RTCManager.ListCmd.size() > 0)
		{
			inbufCmd.Clean();

			//ABello_FI05/10/2006_1 Fixed the bug for thread safe list
			//inbufCmd = C_CommunicationManager::RTCManager.ListCmd.front();
			C_CommunicationManager::RTCManager.ListCmd.front(inbufCmd);
			
			CTime start = CTime::GetCurrentTime();
			DWORD res = C_CommunicationManager::RTCManager.CreateCommands(inbufCmd);

			switch(res)
			{
			case ERROR_SUCCESS:
				C_CommunicationManager::RTCManager.ListCmd.pop_front();
				break;

			case ERROR_SHUTDOWN_IN_PROGRESS:
				C_CommunicationManager::RTCManager.finished = true;
				C_CommunicationManager::RTCManager.ListCmd.pop_front();
				break;

			case ERROR_BAD_COMMAND:
				C_CommunicationManager::RTCManager.ListCmd.pop_front();
				errorNumberRTC = errorNumberRTC + 1;
				logger.Severe(__FUNCTION__,Logger::PRE_SOCK_ERROR, L"Invalid Command from RTC queue (size %d) is number %d / %d", C_CommunicationManager::RTCManager.ListCmd.size(), errorNumberRTC, errorAllowed);
				if(errorNumberRTC > errorAllowed)
					exit(0);
				break;

			default:
				logger.Severe(__FUNCTION__,Logger::PRE_SOCK_ERROR, L"Cannot send command NO pop_front from RTC queue");
				break;
			}

			CTime end = CTime::GetCurrentTime();
			CTimeSpan tsetup = end - start;

			if(tsetup.GetTotalSeconds() > 15)
				logger.Warning(__FUNCTION__,Logger::PRE_NULL, L"CreateCommands RTC very Slow ... %d secs", tsetup.GetTotalSeconds());
		}
		else //NO command to execute
      ::Sleep(20);

		finished = C_CommunicationManager::RTCManager.finished;

	}while(!finished);

	logger.Severe(__FUNCTION__,Logger::PRE_NULL, L"Exit Thread for RTC Queue management");

	return ERROR_SUCCESS;
}

DWORD C_CommunicationManager::initialization()
{
	HANDLE pid_CommandFromDBMS = ::CreateThread(NULL,0,Manage_CommandFromDBMS,NULL,0,NULL);
	if (pid_CommandFromDBMS == NULL)
	{
		DWORD error = GetLastError();
		logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Cannot create the CommandFromDBMS thread ... %d",error);
		return error;
	}
	C_CommunicationManager::stopCommandFromDBMS = false;

	HANDLE pid_CommandFromRTC = ::CreateThread(NULL,0,Manage_CommandFromRTC,NULL,0,NULL);
	if (pid_CommandFromRTC == NULL)
	{
		DWORD error = GetLastError();
		logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Cannot create the CommandFromRTC thread ... %d",error);
		return error;
	}
	C_CommunicationManager::stopCommandFromRTC = false;

	//Named Pipe Data Base Management
	DWORD error=dbManager.createPipe();
	if(error!=ERROR_SUCCESS)
		return error;

	//Named Pipe Real Time Management
	error=RTCManager.createPipe();
	if(error!=ERROR_SUCCESS)
		return error;
	
	///Update all XML DB
	this->UpdateAllDB();
	
	error=pageManager.CreateSM();
	if(error!=ERROR_SUCCESS)
		return error;
		
	//Create the GlobalUpdate thread
	error=pageManager.CreateGlobalUpdate();
	if(error!=ERROR_SUCCESS)
		return error;

	return ERROR_SUCCESS;
}

//CHOOSE COMMAND FUNCTION
C_CommunicationManager::CommandResult C_CommunicationManager::Choose(String::StrType command)
{
	//Lock the Queue
	WaitForSingleObject( this->hCommunicationManagerMutex, INFINITE );

	command.Trim();

	//Messages mess;
	wchar_t header = command.extractHeader();
	bool res=false;

	switch ( header )
		{
		case L'c':
			{C_CommunicationManager::CommandResult res2 = this->ManageCommand(command);
			ReleaseMutex( this->hCommunicationManagerMutex);
			return res2;
			break;}
		case L'C':
			{C_CommunicationManager::CommandResult res2 = this->ManageCommand(command);
			ReleaseMutex( this->hCommunicationManagerMutex);
			return res2;
			break;}
		case L'p':
			res = pageManager.ManagePage(command);
			break;
		case L'P':
			res = pageManager.ManagePage(command);
			break;
		case L'd':
			res = dbManager.ManageDB(command);
			break;
		case L'D':
			res = dbManager.ManageDB(command);
			break;
		case L'r':
			res =  RTCManager.ChooseMachine(command);
			break;
		case L'R':
			res =  RTCManager.ChooseMachine(command);
			break;
		case L' ':
			res = false;
			break;
		default:
			res = false;
			break;
		};

	//release the Mutex
	ReleaseMutex( this->hCommunicationManagerMutex);

  if(res)
		return C_CommunicationManager::ValidCommand;
	else
    return C_CommunicationManager::InvalidCommand;
}

C_CommunicationManager::CommandResult C_CommunicationManager::ManageCommand(String::StrType command)
{
	//get the group token
	String::StrType group = command.extractFirstToken(L".");
	if(group.isEmpty())
		return C_CommunicationManager::InvalidCommand;
	if(!group.CompareNoCase(L"CommunicationManager"))
		return C_CommunicationManager::InvalidCommand;

	//get the group token
	String::StrType cmd = command.extractCommand();
	if(cmd.isEmpty())
		return C_CommunicationManager::InvalidCommand;

	if(cmd.CompareNoCase(L"ShutDown"))
	{
		//Close the client and SyncroS
		dbManager.CloseDBManager(); //Close the Db Manager
		RTCManager.CloseRTCManager(); //delete the Real Time Manager
		pageManager.ClosePageManager(); //delete the Page Manager

		if(command.ToBool()==true)
		{
			//Close the client without shutdown windows
			o_socket.Send(Network::MSG_COMM,L"CloseAndShutDown()");
		}
		else
		{	//Close the client without shutdown windows
			o_socket.Send(Network::MSG_COMM,L"ShutDown()");
		}
		
		return C_CommunicationManager::ShutDown;
	}

	if(cmd.CompareNoCase(L"Alignment"))
	{
		bool forSaveReport = false;
		if(!command.isEmpty())
			forSaveReport= command.ToBool();

		dbManager.AlignDBMS(forSaveReport); //Align the Db of DBMS
		RTCManager.AlignRTC(); //Align the DB of RTC

		return C_CommunicationManager::ValidCommand;
	}

	return C_CommunicationManager::InvalidCommand;
}

DWORD C_CommunicationManager::Stop(void)
{
	/*	DWORD error=dbManager.stopPipe();
	if(error!=ERROR_SUCCESS)	
	{
		logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop NamedPipe DBMS Thread ... %d",error);
		return error;
	}

	error=RTCManager.stopPipe();
	if(error!=ERROR_SUCCESS)	
	{
		logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop NamedPipe RTC Thread ... %d",error);
		return error;
	}
*/
	logger.Info(__FUNCTION__, Logger::PRE_PIPE_INFO, L"Stop Thread for DBMS Queue management");
	C_CommunicationManager::stopCommandFromDBMS = true;

	logger.Info(__FUNCTION__, Logger::PRE_PIPE_INFO, L"Stop Thread for RTC Queue management");
	C_CommunicationManager::stopCommandFromRTC = true;

	DWORD error=pageManager.StopPage();
	if(error!=ERROR_SUCCESS)	
	{
		logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page Manager Thread ... %d",error);
		return error;
	}

	logger.Info(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Stop all page management");

	return ERROR_SUCCESS;
}

DWORD C_CommunicationManager::Restart(void)
{
/*	DWORD error=dbManager.resumePipe();
	if(error!=ERROR_SUCCESS)	
	{
		logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume NamedPipe DBMS Thread ... %d",error);
		return error;
	}

	error=RTCManager.resumePipe();
	if(error!=ERROR_SUCCESS)	
	{
		logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume NamedPipe RTC Thread ... %d",error);
		return error;
	}
*/

	logger.Info(__FUNCTION__, Logger::PRE_PIPE_INFO, L"Resume Thread for DBMS Queue management");
	C_CommunicationManager::stopCommandFromDBMS = false;

	logger.Info(__FUNCTION__, Logger::PRE_PIPE_INFO, L"Resume Thread for RTC Queue management");
	C_CommunicationManager::stopCommandFromRTC = false;

	DWORD error=pageManager.ResumePage();
	if(error!=ERROR_SUCCESS)	
	{
		logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page Thread ... %d",error);
		return error;
	}

	logger.Info(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Restart all page management");

	return ERROR_SUCCESS;
}
