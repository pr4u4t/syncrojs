#pragma once
//Global include
#include "..\Include\Global.h"

//Pipe DBMS-SYNCROS Include
#include "..\Include\DBMSPipe.h"
//Socket Include
#include "..\Include\NameSpace\Network.h"
//Client Named pipe overlapped
#include "C_NamedPipeClient.h"

#include <list>

class C_DBManager
{
public:
	C_DBManager(void);
	~C_DBManager(void);
	bool ManageDB(String::StrType command);
	bool UpdateAllDB(void);
	DWORD createPipe(void);
//	DWORD stopPipe(void);
//	DWORD resumePipe(void);
	void CloseDBManager(void);
	void AlignDBMS(bool theForSaveReport);
	static bool finished;
	//ABello_FI05/10/2006_1 Fixed the bug for thread safe list
	//static std::list<NamedPipe::DataBasePipe> ListDB;
	static Utility::C_List<NamedPipe::DataBasePipe> ListDB;
	static DWORD CreateCommands(NamedPipe::DataBasePipe& inbuf); //Create the string for the DataBase command

private:
	wchar_t LANGUAGES_XML_FILE_NAME[MAX_PATH]; //The file name of Laguages (Xml)
	wchar_t LANGUAGES_SCHEMA_XML_FILE_NAME[MAX_PATH]; //The file name of Laguages (Xsd)
	wchar_t MESSAGES_XML_FILE_NAME[MAX_PATH]; //The file name of Messages (Xml)
	wchar_t MESSAGES_SCHEMA_XML_FILE_NAME[MAX_PATH]; //The file name of Messages (Xsd)
	wchar_t ALARM_XML_FILE_NAME[MAX_PATH]; //The file name of Alarm (Xml)
	wchar_t ALARM_SCHEMA_XML_FILE_NAME[MAX_PATH]; //The file name of Alarm (Xsd)
	wchar_t DOWNTIME_REASON_XML_FILE_NAME[MAX_PATH]; //The file name of DownTime Reason (Xml)
	wchar_t DOWNTIME_REASON_SCHEMA_XML_FILE_NAME[MAX_PATH]; //The file name of DownTime Reason (Xsd)
	wchar_t SYNCRO_INI_FILE_NAME[MAX_PATH]; //The file name of Syncro.ini (ini)

	HANDLE pid_DBMSNamedPipe; //NamedPipe Thread Pid
	bool ParseOrderCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf); //Parse the string for the order command
	bool ParsePastOrderCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf); //Parse the string for the Past Order command
	bool ParseFluteCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf); //Parse the string for the Flute command
	bool ParseQualityCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf); //Parse the string for the Quality command
	bool ParsePaperCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf); //Parse the string for the Paper command
	bool ParseShiftCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf); //Parse the string for the Shift command
	bool ParseDownTimeCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf); //Parse the string for the DownTime command
	bool ParseCutToPatternCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf); //Parse the string for the CutToPattern command
	bool ParseDBManagerCommand(String::StrType token); //Parse the string for the DB Manager command
	bool ParsePastRollsCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf);

	static NamedPipe::C_NamedPipeClient DBMSSyncroS;//Named Pipe objects
	static DWORD CreateExitCommand(NamedPipe::DataBasePipe& inbuf); //Create the string for the Exit command
	static DWORD CreateDBVerCommand(NamedPipe::DataBasePipe& in); //Create the string for the DB version command
	static DWORD CreateCutToPatternCommands(NamedPipe::DataBasePipe& in); //Create the string for the CutToPattern command
	static DWORD CreatePrintingsCommands(NamedPipe::DataBasePipe& in); //Create the string for the Printings command
	static DWORD CreatePastRollsCommands(NamedPipe::DataBasePipe& in); //Create the string for the PastRolls command
	static DWORD CreateDownTimeCommands(NamedPipe::DataBasePipe& in); //Create the string for the DownTime command
	static DWORD CreateShiftCommands(NamedPipe::DataBasePipe& in); //Create the string for the Shift command
	static DWORD CreatePastOrderCommands(NamedPipe::DataBasePipe& in); //Create the string for the Past Order command
	static DWORD CreateOrdersCommands(NamedPipe::DataBasePipe& in); //Create the string for the Order command
	static DWORD CreateProgramsCommands(NamedPipe::DataBasePipe& in); //Create the string for the Program command
	static DWORD CreateQualityCommands(NamedPipe::DataBasePipe& in); //Create the string for the Quality command
	static DWORD CreatePaperCommands(NamedPipe::DataBasePipe& in); //Create the string for the Paper command
	static DWORD CreateFluteCommands(NamedPipe::DataBasePipe& in); //Create the string for the Flute command
	static DWORD WINAPI DBMSNamedPipe(LPVOID  lpParam);  //The thread function for the Named Pipe management DataBase command
};
