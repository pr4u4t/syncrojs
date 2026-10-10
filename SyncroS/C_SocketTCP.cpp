#include "..\Include\NameSpace\Network.h"
#include "Mswsock.h"

//Logger include
#include "..\Shared\C_Logger.h"

extern Logger::C_Logger logger;

namespace Network
{

	C_SocketTCP::C_SocketTCP(void)
	{
	}

	C_SocketTCP::~C_SocketTCP(void)
	{
		this->Close();
	}

	DWORD C_SocketTCP::init_tcpip()
	{
		WORD
				wVersionRequested = MAKEWORD(1, 1);

		WSADATA
				wsaData;

		int
				error;

		error = ::WSAStartup(wVersionRequested, &wsaData);

		if (error != 0)
		{
			logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Init_tcpip - WSAStartup error");
			return ::WSAGetLastError();
		}

		if ((LOBYTE(wsaData.wVersion) != 1)||(HIBYTE(wsaData.wVersion) != 1))
		{
			logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Init_tcpip - DLL not version 1.1");
			::WSACleanup();
			return ::WSAGetLastError();
		}

		return ERROR_SUCCESS;
	}

	DWORD C_SocketTCP::CreateSocket(int PORT)
	{
		this->hSocketMutex= ::CreateMutex (NULL, FALSE, NULL);
		this->mySocket=INVALID_SOCKET;

		this->SERVER_PORT = PORT;

		//Init TCP IP 
		if(this->init_tcpip()!=ERROR_SUCCESS)
		{
			DWORD error=::WSAGetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Cannot init TCP Port: %d for error: %d",PORT,error);
			return error;
		}

		// create socket
		this->sock = ::socket(AF_INET, SOCK_STREAM, 0);
		if(this->sock==INVALID_SOCKET) 
		{
			DWORD error=::WSAGetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Cannot open socket on port: %d",PORT);
			return error;
		}

		// bind server port
		struct sockaddr_in servAddr;

		servAddr.sin_family = AF_INET;
		servAddr.sin_addr.s_addr = ::htonl(INADDR_ANY);
		servAddr.sin_port = ::htons(SERVER_PORT);

		if(::bind(this->sock, (struct sockaddr *) &servAddr, sizeof(servAddr))==SOCKET_ERROR) 
		{
			DWORD error=::WSAGetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Cannot bind TCP Port: %d for error: %d",PORT,error);
			this->Close();
			this->sock=INVALID_SOCKET;
			return error;
		}

		return ERROR_SUCCESS;
	}

	DWORD C_SocketTCP::WaitClient()
	{
		struct sockaddr_in cliAddr;
		int cliLen=sizeof(cliAddr);

		//listen
		if(::listen(this->sock,1)==SOCKET_ERROR )
		{
			DWORD error=::WSAGetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Cannot listen on TCP Port: %d for error: %d",SERVER_PORT,error);
			return error;
		}
		else    
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_INFO, L"Port: %d",SERVER_PORT);

		//accept
		this->mySocket=::accept(sock, (struct sockaddr *) &cliAddr, &cliLen);
		if(mySocket==INVALID_SOCKET) 
		{
			DWORD error=::WSAGetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Cannot accept connection on TCP Port: %d for error: %d",SERVER_PORT,error);
			return error;
			}
		else
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_INFO, L"Connect Client on Port: %d",SERVER_PORT);

		return ERROR_SUCCESS;
	}

	DWORD C_SocketTCP::Receive(wchar_t *rcv_msg)
	{
		//clean the buffer
		::ZeroMemory(rcv_msg, sizeof(rcv_msg));
		wchar_t pack[MAX_LENGTH_ONE_MSG + 16];
		int nPack = 0;
		int totalPack = 0;
		size_t n=0;

		do
		{
			::ZeroMemory(pack,sizeof(pack));
			// read data from socket 
			n = ::recv(this->mySocket,(char *) pack, SOCK_MAX_BYTE, 0); // wait for data 

			//Error control
			if ((n<=0) || (n>SOCK_MAX_BYTE))
			{
				DWORD error=::WSAGetLastError();
				logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Connection interrupted for error ... %d",error);
				return error;
			}

			if((wcsstr(pack,L"<PKT>")==NULL) || (wcsstr(pack,L"</PKT>")==NULL) || (pack[7] != L'-'))
			{
				logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"The string doesn't respect the protocol : %s",pack);
				::ZeroMemory(rcv_msg, sizeof(rcv_msg));
				nPack = 0;
				totalPack = -1;
				continue;
			}

			wchar_t nPackStr[3]=L"";
			wchar_t totalPackStr[3]=L"";
			wcsncpy(nPackStr,pack+5,2);
			wcsncpy(totalPackStr,pack+8,2);

			nPack = _wtoi(nPackStr);
			totalPack = _wtoi(totalPackStr);
			if((nPack > totalPack) || (nPack==0) || (totalPack==0))
			{
				logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"The string doesn't respect the protocol : %s",rcv_msg);
				::ZeroMemory(rcv_msg, sizeof(rcv_msg));
				nPack = 0;
				totalPack = -1;
				continue;
			}

			::wcsncat(rcv_msg, pack+10, min(wcslen(pack),n/2)-16);

		}	while(nPack!=totalPack);

		logger.Fine(__FUNCTION__, Logger::PRE_SOCK_RCV ,L"%s", rcv_msg);

		return ERROR_SUCCESS;
	}

	DWORD C_SocketTCP::Restore()
	{
		logger.Info(__FUNCTION__, Logger::PRE_SOCK_INFO, L"Restore_Socket()");

		if (this->WaitClient()==ERROR_SUCCESS)
			return ERROR_SUCCESS;
		else 
		{	
			if(this->Close()==ERROR_SUCCESS)
				if (this->WaitClient()==ERROR_SUCCESS)
					return ERROR_SUCCESS;
		}
		return SOCKET_ERROR;
	}

	DWORD C_SocketTCP::TransmitFile(const String::StrType FileName)
	{

		//Lock the pipe with the judge and the Queue
		::WaitForSingleObject( this->hSocketMutex, INFINITE );

		HANDLE fp=::CreateFile(FileName.getPtr(),GENERIC_READ,FILE_SHARE_READ,NULL,OPEN_EXISTING,FILE_FLAG_SEQUENTIAL_SCAN,NULL);

		const wchar_t *dummy=::wcsrchr(FileName.getPtr(),'\\')+1;

		this->Send(Network::MSG_COMM,L"START_TRANSMIT(%s,%d)",dummy,::GetFileSize(fp,NULL));

		if(::TransmitFile(mySocket,fp,0,0,NULL,NULL,TF_WRITE_BEHIND)==TRUE)
			this->Send(Network::MSG_COMM,L"END_TRANSMIT(%s)",dummy);
		else
		{ 
			DWORD error=::WSAGetLastError();
			this->Send(Network::MSG_COMM,L"Transmit File %d",error);
			return error;
		}
		
		::CloseHandle(fp);

		//release the Mutex
		::ReleaseMutex( this->hSocketMutex);
		
		return ERROR_SUCCESS;
	}


	DWORD C_SocketTCP::Send(const wchar_t* header_text,wchar_t * command_text, ...)
	{
		//Lock the Socket
		::WaitForSingleObject( this->hSocketMutex, INFINITE );

		va_list args;  
		va_start( args, command_text );
		//create the command to send
		wchar_t buf[Network::SOCK_MAX_MSG+1];
		::ZeroMemory(buf,sizeof(wchar_t)*Network::SOCK_MAX_MSG);
		::vswprintf(  buf, sizeof(wchar_t)*Network::SOCK_MAX_MSG, command_text, args );
		va_end( args);

		//init the message to send
		::ZeroMemory(this->snd_msg, sizeof(this->snd_msg));

		::swprintf(this->snd_msg,L"<MSG><HDR>%s</HDR><TXT>%s</TXT></MSG>",header_text,buf);
		// Calculate the number of packet to send
		int ab= (wcslen(this->snd_msg) % MAX_LENGTH_ONE_MSG) == 0 ? 0 : 1; 
		size_t nPack = (wcslen(this->snd_msg) / MAX_LENGTH_ONE_MSG) + ab;

		wchar_t pack[MAX_LENGTH_ONE_MSG +16+1];
		for(size_t i=1; i<=nPack; i++)
		{
			::ZeroMemory(pack,sizeof(pack));
			::swprintf(pack,L"<PKT>%.2d-%.2d",i,nPack);
			::wcsncat(pack, this->snd_msg+((i-1)*MAX_LENGTH_ONE_MSG), MAX_LENGTH_ONE_MSG);
			::wcscat(pack,L"</PKT>");

			//send the message 
			if(::send(this->mySocket,(char *) pack, SOCK_MAX_BYTE , 0)==SOCKET_ERROR) 
			{
				//get the error
				DWORD error=::WSAGetLastError();
				logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Cannot send data %s ... %d", this->snd_msg, error);
				//release the Mutex
				::ReleaseMutex( this->hSocketMutex);

				return error;
				}
		}

		logger.Fine(__FUNCTION__, Logger::PRE_SOCK_SND, L"%s",this->snd_msg);

		//release the Mutex
		::ReleaseMutex( this->hSocketMutex);

		return ERROR_SUCCESS;
	}

	DWORD C_SocketTCP::Close()
	{
		::shutdown(this->mySocket,SD_BOTH);
		::closesocket(this->mySocket);
		::WSACleanup();
		
		return ERROR_SUCCESS;
	}
}
