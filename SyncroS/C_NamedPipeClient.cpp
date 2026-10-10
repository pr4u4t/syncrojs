#include "C_NamedPipeClient.h"

//Logger include
#include "..\Shared\C_Logger.h"

extern Logger::C_Logger logger;

namespace NamedPipe
{
	C_NamedPipeClient::C_NamedPipeClient()
	{
	}

	C_NamedPipeClient::C_NamedPipeClient(const wchar_t *thePipeName, const DWORD thesize)
	{
		
		::wcscpy(this->PipeName,thePipeName);
		size=thesize;

		// set up overlapped structure fields 
		ovRead.Offset     = 0; 
		ovRead.OffsetHigh = 0; 
		ovRead.hEvent = CreateEvent( 
				NULL,    // no security attribute 
				TRUE,    // manual-reset event 
				TRUE,    // initial state = signaled 
				NULL);   // unnamed event object

		// set up overlapped structure fields 
		ovWrite.Offset     = 0; 
		ovWrite.OffsetHigh = 0; 
		ovWrite.hEvent = CreateEvent( 
				NULL,    // no security attribute 
				TRUE,    // manual-reset event 
				TRUE,    // initial state = signaled 
				NULL);   // unnamed event object
	}

	C_NamedPipeClient::~C_NamedPipeClient(void)
	{
	}

	DWORD C_NamedPipeClient::myCreateFile(bool secureCreate)
	{
		logger.Info(__FUNCTION__,Logger::PRE_PIPE_INFO, L"Inizialized Named Pipe: %s with size: %d", PipeName, size);

		PSECURITY_DESCRIPTOR    pSD;
		SECURITY_ATTRIBUTES     sa;

		pSD = (PSECURITY_DESCRIPTOR) LocalAlloc(LPTR, SECURITY_DESCRIPTOR_MIN_LENGTH);
	  
		if (pSD == NULL) 
		{
			logger.Severe(__FUNCTION__,Logger::PRE_PIPE_ERROR, L"Security Descriptor Named Pipe Client: %s", PipeName);     
			return -1;  
		}

		if (!InitializeSecurityDescriptor(pSD, SECURITY_DESCRIPTOR_REVISION)) 
		{
				LocalFree((HLOCAL)pSD);
				return -1;
		}

		if (!SetSecurityDescriptorDacl(pSD, TRUE, (PACL) NULL, FALSE)) 
		{
				LocalFree((HLOCAL)pSD);
				return -1;
		}

		sa.nLength = sizeof(sa);
		sa.lpSecurityDescriptor = pSD;
		sa.bInheritHandle = TRUE;
    //agrassi_FI12-04-2020_1
    pipeHandle=INVALID_HANDLE_VALUE;
		do
		{
      //agrassi_FI12-04-2020_1
			//wait for named pipe creation
			//BOOL res=WaitNamedPipe(PipeName,NMPWAIT_USE_DEFAULT_WAIT);
			BOOL res=WaitNamedPipe(PipeName,20000);
		  
      if (!res)
      {
        logger.Warning(__FUNCTION__, L"Pipe %s has not been created: error %d", PipeName, GetLastError()); 
        //agrassi_FI23-07-2010_2
        Sleep(2500);
        continue;
      }

			//Create the NamedPipe Client
			pipeHandle=CreateFile(
					PipeName,
					GENERIC_READ|GENERIC_WRITE | SYNCHRONIZE,
					FILE_SHARE_READ | FILE_SHARE_WRITE ,
					&sa,
					OPEN_EXISTING,
					FILE_FLAG_OVERLAPPED | FILE_FLAG_NO_BUFFERING,
					NULL);

			if (pipeHandle==INVALID_HANDLE_VALUE)
			{
				DWORD error = GetLastError();
				logger.Severe(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"Cannot create pipe %s .... %d", PipeName, error);
				if(!secureCreate) return error;
				Sleep(500);
			}

		}while(pipeHandle==INVALID_HANDLE_VALUE);

		logger.Info(__FUNCTION__, Logger::PRE_PIPE_INFO, L"Create Named Pipe Client: %s", PipeName); 

		return ERROR_SUCCESS;
	}


	DWORD C_NamedPipeClient::Read(LPVOID inbuf, DWORD dwMilliseconds)
	{   
		BOOL ret;
		DWORD	bytesRead;

		ret = ReadFile(
						pipeHandle,		// file to read from
						inbuf,				// address of input buffer
						size,					// number of bytes to read
						&bytesRead,		// number of bytes read
						&ovRead);			// overlapped stuff

		if(!ret)
		{
			DWORD errorCode=GetLastError();

			if(errorCode==ERROR_IO_PENDING)
			{
				//BOOL bResult = GetOverlappedResult(pipeHandle, &ovRead, &bytesRead, TRUE);

				BOOL bResult = FALSE;
				DWORD dwWaitResult = WaitForSingleObject(ovRead.hEvent,dwMilliseconds);

				switch (dwWaitResult) 
				{
				case WAIT_OBJECT_0:
					bResult = TRUE;
					break; 
				case WAIT_TIMEOUT:
					bResult = FALSE;
					break;
				case WAIT_ABANDONED: 	
					bResult = FALSE;
					break;
				}
				
				if(!bResult)
				{
					errorCode=GetLastError();
					if(errorCode!=ERROR_IO_PENDING)
            logger.Severe(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"Cannot Read ... %d", errorCode);
					return errorCode;
				}
				else        
					return ERROR_SUCCESS;
			}
			else
			{
				logger.Severe(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"Cannot Read ... %d", errorCode);
				return errorCode;
			}
		}

		return ERROR_SUCCESS;
		
	}

	DWORD C_NamedPipeClient::Write(LPVOID outbuf, DWORD dwMilliseconds)
	{
		BOOL ret;
		DWORD bytesWritten;

		ret = WriteFile(				
							pipeHandle,		// file to write to
							outbuf,				// address of output buffer
							size,					// number of bytes to write
							&bytesWritten,// number of bytes written
							&ovWrite);		// overlapped stuff

		if(!ret)
		{
			DWORD errorCode=GetLastError();

			if(errorCode==ERROR_IO_PENDING)
			{
				//BOOL bResult = GetOverlappedResult(pipeHandle, &ovWrite, &bytesWritten, TRUE);

				BOOL bResult = FALSE;
				DWORD dwWaitResult = WaitForSingleObject(ovWrite.hEvent,dwMilliseconds);

				switch (dwWaitResult) 
				{
				case WAIT_OBJECT_0:
					bResult = TRUE;
					break; 
				case WAIT_TIMEOUT:
					bResult = FALSE;
					break;
				case WAIT_ABANDONED: 	
					bResult = FALSE;
					break;
				}

				if(!bResult)
				{
					errorCode=GetLastError();
					logger.Severe(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"Cannot Write ... %d", errorCode);
					return errorCode;
				}
				else
					return ERROR_SUCCESS;
			}
			else
			{
				logger.Severe(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"Cannot Write ... %d", errorCode);
				return errorCode;
			}
		}

		return ERROR_SUCCESS;
	}

	DWORD C_NamedPipeClient::Close()
	{
		if(!CloseHandle(pipeHandle))
		{
			logger.Info(__FUNCTION__, Logger::PRE_NULL, L"Close Named Pipe %s", PipeName);
			return ERROR_SUCCESS;
		}
		else
		{
			DWORD errorCode=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"Cannot Close ... %d", errorCode);
			return errorCode;
		}
	}
};