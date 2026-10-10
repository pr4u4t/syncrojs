#pragma once

#include "..\Include\Global.h"

namespace NamedPipe
{
	class C_NamedPipeClient
	{
	public:
		C_NamedPipeClient(const wchar_t *thePipeName, const DWORD thesize);
		C_NamedPipeClient(void);
		~C_NamedPipeClient(void);
		DWORD myCreateFile(bool secureCreate=true); //Create File 
		DWORD Read(LPVOID inbuf, DWORD dwMilliseconds = INFINITE); //Read from Named Pipe
		DWORD Write(LPVOID outbuf, DWORD dwMilliseconds = INFINITE); //Write on Named Pipe
		DWORD Close(void); //Close the Named Pipe

	private:
		OVERLAPPED ovRead;
		OVERLAPPED ovWrite;
		DWORD	size;
		HANDLE pipeHandle;
		wchar_t PipeName[MAX_PATH];
	};
};