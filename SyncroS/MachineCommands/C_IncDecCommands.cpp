#include ".\C_IncDecCommands.h"

#include "..\C_RTCManager.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;

IncDecButtonsData C_IncDecCommands::incDecButtonsData;

C_IncDecCommands::C_IncDecCommands(void)
{
}

C_IncDecCommands::~C_IncDecCommands(void)
{
}

//e.g. page -> Twin400_Console   buttonCommand -> IncreaseScoreGap1(42)
bool C_IncDecCommands::ManageButton(String::StrType Page, String::StrType buttonCommand)
{
	int lastFrameFrequency=0;
	if (buttonCommand.isEmpty())
		return false;

	String::StrType command = buttonCommand.extractCommand();
	if (buttonCommand.isEmpty())
		return false;
	else
		lastFrameFrequency=buttonCommand.ToInt();

	if(lastFrameFrequency==0) return false;

	switch (lastFrameFrequency)
	{
	case -1:
	{	
		if(C_IncDecCommands::incDecButtonsData.Running)
		{
			o_socket.Send(Network::MSG_PAG_REF, L"%s(b[false])",incDecButtonsData.PageCommand.getPtr());
			C_RTCManager::RTCSyncroS.Write(&incDecButtonsData.commandPipeStop);
			logger.Info(__FUNCTION__,Logger::PRE_NULL, L"STOP THREAD for button: %s", incDecButtonsData.PageCommand.getPtr());
		}
		C_IncDecCommands::incDecButtonsData.Clean();
		C_IncDecCommands::incDecButtonsData.EndTime = GetTickCount() + Network::ETHERNET_DELAY;
		break;
	}
	case -2:
		C_IncDecCommands::incDecButtonsData.stop = true;
		break;
	default:
		{
			if ( (C_IncDecCommands::incDecButtonsData.EndTime + lastFrameFrequency - GetTickCount()) > Network::ETHERNET_DELAY)
				C_IncDecCommands::incDecButtonsData.EndTime = GetTickCount() + Network::ETHERNET_DELAY;
			else
				C_IncDecCommands::incDecButtonsData.EndTime += lastFrameFrequency;
			break;
		}
	};

	if ((!C_IncDecCommands::incDecButtonsData.Running) && ((C_IncDecCommands::incDecButtonsData.EndTime - GetTickCount()) > 0))
	{
		if(!this->getPipeCmd(command, C_IncDecCommands::incDecButtonsData.commandPipeStart, C_IncDecCommands::incDecButtonsData.commandPipeStop))
		{
			logger.Fine(__FUNCTION__,Logger::PRE_NULL, L"Not a real-time command %s", C_IncDecCommands::incDecButtonsData.PageCommand.getPtr());
			C_IncDecCommands::incDecButtonsData.Running = false;
			C_IncDecCommands::incDecButtonsData.PageCommand="";
			return false;
		}
		else
		{
			C_IncDecCommands::incDecButtonsData.Running = true;
			C_IncDecCommands::incDecButtonsData.PageCommand = Page + L"." + command.getPtr();
      C_IncDecCommands::incDecButtonsData.ThreadHandle = 
				::CreateThread(NULL, 0, C_IncDecCommands::IncreaseDecrease, NULL , 0, NULL);
		}
	}

	return true;
}

DWORD WINAPI C_IncDecCommands::IncreaseDecrease(LPVOID  lpParam)
{

	logger.Info(__FUNCTION__, Logger::PRE_NULL, L"START THREAD for button: %s", incDecButtonsData.PageCommand.getPtr());
	
	o_socket.Send(Network::MSG_PAG_REF, L"%s(b[true])",incDecButtonsData.PageCommand.getPtr());

  const int SECURITY_CLOCK=2500;

	DWORD securityTime = GetTickCount() + SECURITY_CLOCK;

	C_RTCManager::RTCSyncroS.Write(&incDecButtonsData.commandPipeStart);
	//wait until the button is kept pressed
	//EndTime (and consequently timeOut too) is continuously updated by the thread father 
	//(ManageSSButton method) until the button is pressed
	int timeout = 0;
	int maxtimeout = 0;
	int mintimeout = MAX_INT_32;
	do
	{
		if(C_IncDecCommands::incDecButtonsData.stop)
			break;
		::Sleep(timeout/4);
		
		if(C_IncDecCommands::incDecButtonsData.stop)
			break;
		::Sleep(timeout/4);

		if(C_IncDecCommands::incDecButtonsData.stop)
			break;
		::Sleep(timeout/4);
			
		if(C_IncDecCommands::incDecButtonsData.stop)		
			break;	
		::Sleep(timeout/4);
		
		try 
		{
			DWORD tick = ::GetTickCount();
			timeout = C_IncDecCommands::incDecButtonsData.EndTime - tick;
			if(securityTime <= tick)
			{
				C_RTCManager::RTCSyncroS.Write(&incDecButtonsData.commandPipeStart);
				securityTime = GetTickCount() + SECURITY_CLOCK;
				logger.Info(__FUNCTION__, Logger::PRE_NULL, L"Security Clock Activated");
			}
		}
		catch(...)
		{
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Inc/Dec EXTRA_DELAY on timeout activated");
			timeout = Network::EXTRA_DELAY;
		}
		if (timeout > maxtimeout)
			maxtimeout = timeout;
		if (timeout > 0 && (timeout < mintimeout))
			mintimeout = timeout;

		if((timeout <=0)&&(!C_IncDecCommands::incDecButtonsData.stop))
		{
			C_IncDecCommands::incDecButtonsData.stop = true;
			timeout=50;
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Inc/Dec No Stop arrivated on time, wait an extra time");
		}
		else
			C_IncDecCommands::incDecButtonsData.stop = false;

	} while(timeout > 0);
	//
	//execute stop
	//
		
	o_socket.Send(Network::MSG_PAG_REF, L"%s(b[false])",incDecButtonsData.PageCommand.getPtr());

	C_RTCManager::RTCSyncroS.Write(&incDecButtonsData.commandPipeStop);

	logger.Info(__FUNCTION__, Logger::PRE_NULL, L"STOP THREAD for button: %s", incDecButtonsData.PageCommand.getPtr());
	logger.Info(__FUNCTION__, Logger::PRE_NULL, L"MIN_TIMEOUT: %d; MAX_TIMEOUT: %d", mintimeout, maxtimeout);

	incDecButtonsData.Running = false;
	incDecButtonsData.ThreadHandle=NULL;
	
	return 0;
}