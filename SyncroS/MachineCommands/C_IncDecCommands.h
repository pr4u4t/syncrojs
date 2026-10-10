#pragma once

//Global include
#include "..\..\Include\Global.h"

//logger include
#include "..\..\\Shared\\C_Logger.h"

//Client Named pipe overlapped
#include "..\..\Include\RTCPipe.h"


struct IncDecButtonsData
{
	String::StrType PageCommand;
	NamedPipe::CommandPipe commandPipeStop;
	NamedPipe::CommandPipe commandPipeStart;
	DWORD EndTime;				//increase / decrease estimated time
	bool Running;					//if true increase / decrease thread running, not running otherwise
	HANDLE ThreadHandle;
	bool stop;

	IncDecButtonsData::IncDecButtonsData(void)
	{
		PageCommand="";
		commandPipeStop.Clean();
		commandPipeStart.Clean();
		EndTime=0;
		Running=false;
		stop=false;
		ThreadHandle=NULL;
	}
	void Clean(void)
	{
		PageCommand="";
		commandPipeStop.Clean();
		commandPipeStart.Clean();
		EndTime=0;
		Running=false;
		stop=false;
		if(ThreadHandle!=NULL)
		{
			//::TerminateThread(ThreadHandle,NULL);
			ThreadHandle=NULL;
		}
	}
};

class C_IncDecCommands
{
public:
	C_IncDecCommands(void);
	~C_IncDecCommands(void);
	bool ManageButton(String::StrType Page, String::StrType buttonCommand);
	static DWORD WINAPI IncreaseDecrease(LPVOID  lpParam);

private:
	virtual bool getPipeCmd(String::StrType cmd, NamedPipe::CommandPipe& commandPipeStart, NamedPipe::CommandPipe& commandPipeStop) {	return false; };
	static IncDecButtonsData incDecButtonsData;
	
};