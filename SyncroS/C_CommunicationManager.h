#pragma once
//Global Include
#include "..\Include\Global.h"
//SocketTCP Include
#include "..\Include\NameSpace\Network.h"
//DataBase functionallity
#include ".\C_DBManager.h"
//Real Time functionallity
#include ".\C_RTCManager.h"
//Page Management
#include ".\C_PageManager.h"

class C_CommunicationManager
{
public:
	enum CommandResult {InvalidCommand, ShutDown, ValidCommand};
	C_CommunicationManager(void);
	~C_CommunicationManager(void);
	C_CommunicationManager::CommandResult Choose(String::StrType command);
	DWORD Stop(void); //Stop all thread
	DWORD Restart(void); //Restart all thread
	DWORD initialization(void);// Initialization function
	void UpdateAllDB(void);//Update all file dataBase

private:
	static int errorNumberRTC; 
	static int errorNumberDBMS; 
	static int errorAllowed;
	C_CommunicationManager::CommandResult ManageCommand(String::StrType command);
	static DWORD WINAPI Manage_CommandFromDBMS(LPVOID  lpParam);
	static DWORD WINAPI Manage_CommandFromRTC(LPVOID  lpParam);
	HANDLE hCommunicationManagerMutex; //A Semaphore for the syncronization

	static C_DBManager dbManager; //The Db Manager
	static bool stopCommandFromDBMS;
	static C_RTCManager RTCManager; //The Real Time Manager
	static bool stopCommandFromRTC;
	C_PageManager pageManager; //Page Manager
};
