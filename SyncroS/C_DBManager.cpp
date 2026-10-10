#include ".\c_dbmanager.h"

//Logger include
#include "..\Shared\C_Logger.h"
//Validation Include
#include "..\Include\ValidationExport.h"

//Logger object
extern Logger::C_Logger logger;

//Socket
extern Network::C_SocketTCP o_socket;

//Checksum Function
extern "C" DWORD FileCrc32Assembly(LPCTSTR szFilename, DWORD &dwCrc32);

NamedPipe::C_NamedPipeClient C_DBManager::DBMSSyncroS(NamedPipe::PIPENAME_DBMSSyncroS, NamedPipe::DB_SIZE_PIPE);//Named Pipe objects
bool C_DBManager::finished;

//ABello_FI05/10/2006_1 Fixed the bug for thread safe list
//std::list<NamedPipe::DataBasePipe> C_DBManager::ListDB;
Utility::C_List<NamedPipe::DataBasePipe> C_DBManager::ListDB;

C_DBManager::C_DBManager(void)
{
	size_t len = sizeof(wchar_t)*MAX_PATH;

	::ZeroMemory(this->LANGUAGES_XML_FILE_NAME, len);
	::ZeroMemory(this->LANGUAGES_SCHEMA_XML_FILE_NAME, len);
	::ZeroMemory(this->MESSAGES_SCHEMA_XML_FILE_NAME, len);
	::ZeroMemory(this->MESSAGES_XML_FILE_NAME,len);
	::ZeroMemory(this->ALARM_SCHEMA_XML_FILE_NAME, len);
	::ZeroMemory(this->ALARM_XML_FILE_NAME, len);
	::ZeroMemory(this->DOWNTIME_REASON_SCHEMA_XML_FILE_NAME, len);
	::ZeroMemory(this->DOWNTIME_REASON_XML_FILE_NAME, len);
	::ZeroMemory(this->SYNCRO_INI_FILE_NAME, len);

	this->pid_DBMSNamedPipe = NULL;
	//DataBase Name Constant
	//Languages
	//create the path of the solution
	wchar_t absolutePath[MAX_PATH];
	String::getSyncroFolder(absolutePath,MAX_PATH);

	//Synco.ini
	::swprintf(this->SYNCRO_INI_FILE_NAME, L"%sData\\Server\\Config\\syncro.ini", absolutePath);
	//Languages
	::swprintf(this->LANGUAGES_XML_FILE_NAME, L"%sSource\\_Translations\\language.xml", absolutePath);
	::swprintf(this->LANGUAGES_SCHEMA_XML_FILE_NAME, L"%sSource\\_Translations\\language.xsd",absolutePath);
	//Messages
	::swprintf(this->MESSAGES_SCHEMA_XML_FILE_NAME, L"%sSource\\_Translations\\message.xsd",absolutePath);
	::swprintf(this->MESSAGES_XML_FILE_NAME, L"%sSource\\_Translations\\message.xml",absolutePath);
	//Alarm
	::swprintf(this->ALARM_SCHEMA_XML_FILE_NAME, L"%sSource\\_Translations\\alarm.xsd",absolutePath);
	::swprintf(this->ALARM_XML_FILE_NAME, L"%sSource\\_Translations\\alarm.xml",absolutePath);
	//DownTime Reason
  //aggiunta flag Fosber
	//::swprintf(this->DOWNTIME_REASON_SCHEMA_XML_FILE_NAME, L"%sSource\\_Translations\\DownTimeReason.xsd",absolutePath);
	//::swprintf(this->DOWNTIME_REASON_XML_FILE_NAME, L"%sSource\\_Translations\\DownTimeReason.xml",absolutePath);
	::swprintf(this->DOWNTIME_REASON_SCHEMA_XML_FILE_NAME, L"%sData\\Server\\Config\\DownTimeReason.xsd",absolutePath);
	::swprintf(this->DOWNTIME_REASON_XML_FILE_NAME, L"%sData\\Server\\Config\\DownTimeReason.xml",absolutePath);

	logger.Info(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"DBManager created");
}

C_DBManager::~C_DBManager(void){
}

void C_DBManager::CloseDBManager(void)
{
	logger.Info(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"Close DBMS Named Pipe ");

	NamedPipe::DataBasePipe outbuf;
	outbuf.setExitDB();
	
	//send the command to DBMS
	if(DBMSSyncroS.Write(&outbuf)!=ERROR_SUCCESS)
		::TerminateThread(pid_DBMSNamedPipe, NULL);
	else
	{
		//wait for the thread dbms Named pipe
		DWORD dwWaitResult = ::WaitForSingleObject(pid_DBMSNamedPipe,5000L); // five-second time-out interval
		
		switch ( dwWaitResult )
		{
		case WAIT_OBJECT_0:
			break;
		default:
			logger.Warning(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"Force close DBMS Named Pipe");
			::TerminateThread(pid_DBMSNamedPipe, NULL);
			break;
		};
	}

	::CloseHandle(pid_DBMSNamedPipe);
	pid_DBMSNamedPipe=NULL;
}

void C_DBManager::AlignDBMS(bool theForSaveReport)
{
	logger.Info(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"Align DBMS with Client");

	NamedPipe::DataBasePipe outbuf;
	outbuf.setDbVerDB();
	outbuf.setCommandAlign();

	//TRUE if the client want save Report
	outbuf.multipleOperation.userBool = theForSaveReport;
	
	//send the command to DBMS
	DBMSSyncroS.Write(&outbuf);
}

DWORD C_DBManager::createPipe(void)
{
	logger.Info(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"DBManager createpipe");
	DWORD errorPipe = DBMSSyncroS.myCreateFile();
	if(errorPipe==ERROR_SUCCESS)
	{
		pid_DBMSNamedPipe=::CreateThread(NULL,0,DBMSNamedPipe,NULL,0,NULL);
		if (pid_DBMSNamedPipe == NULL)
		{
			DWORD error = GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Cannot create the NamedPipe thread ... %d",error);
			return error;
		}
	}
	else
	{
		logger.Severe(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Cannot create the NamedPipe %s ... %d", NamedPipe::PIPENAME_DBMSSyncroS,errorPipe);
		return errorPipe;
	}

	return ERROR_SUCCESS;
}

bool C_DBManager::UpdateAllDB(void)
{
	DWORD check_ini;
	DWORD error_check_ini;
	DWORD check_schema;
	DWORD check_xml;
	DWORD error_check_schema;
	DWORD error_check_xml;
	bool ok=true;



	//calculate the checksum for DownTimeReason.xsd
	error_check_ini=FileCrc32Assembly(this->SYNCRO_INI_FILE_NAME,check_ini);
	if(error_check_ini==ERROR_SUCCESS)
	{
		//Send Syncro.ini CheckSum ini CheckSum
		o_socket.Send(Network::MSG_DB_REF,L"syncroConfig.Chk(%x)",check_ini);	
		logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"Syncro.ini CheckSum %x",check_ini); 
	}
	else
	{
		logger.Warning(__FUNCTION__, Logger::PRE_NULL, L"ERROR: Cannot calculate the checksum for Syncro.ini ... %d",error_check_ini);
		ok=false;
	}

	//calculate the checksum for languages.xml
	error_check_schema=FileCrc32Assembly(LANGUAGES_SCHEMA_XML_FILE_NAME,check_schema);
	//calculate the checksum for languages.xsd
	error_check_xml=FileCrc32Assembly(LANGUAGES_XML_FILE_NAME,check_xml);

	//Languages DB
	if(error_check_schema==ERROR_SUCCESS)
	{
		if(error_check_xml==ERROR_SUCCESS)
		{
			//Send Languages Schema CheckSum and Languages XML CheckSum
			o_socket.Send(Network::MSG_DB_REF,L"language.Chk(%x,%x)",check_schema,check_xml);
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"Language CheckSum Schema %x Xml %x",check_schema,check_xml); 
		}
		else
		{
			logger.Warning(__FUNCTION__, Logger::PRE_NULL, L"ERROR: Cannot calculate the checksum for languages.xml ... %d",error_check_xml);
			ok=false;
		}
	}
	else
	{
		logger.Warning(__FUNCTION__, Logger::PRE_NULL, L"ERROR: Cannot calculate the checksum for languages.xsd ... %d",error_check_schema);
		ok=false;
	}
	
	//calculate the checksum for languages.xml
	error_check_schema=FileCrc32Assembly(MESSAGES_SCHEMA_XML_FILE_NAME,check_schema);
	//calculate the checksum for languages.xsd
	error_check_xml=FileCrc32Assembly(MESSAGES_XML_FILE_NAME,check_xml);

	//Messages DB
	if(error_check_schema==ERROR_SUCCESS)
	{
		if(error_check_xml==ERROR_SUCCESS)
		{
			//Send Languages Schema CheckSum and Messages XML CheckSum
			o_socket.Send(Network::MSG_DB_REF,L"message.Chk(%x,%x)",check_schema,check_xml);
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"Message CheckSum Schema %x Xml %x",check_schema,check_xml); 
		}
		else
		{
			logger.Warning(__FUNCTION__, Logger::PRE_NULL, L"ERROR: Cannot calculate the checksum for Messages.xml ... %d",error_check_xml);
			ok=false;
		}
	}
	else
	{
		logger.Warning(__FUNCTION__, Logger::PRE_NULL, L"ERROR: Cannot calculate the checksum for Messages.xsd ... %d",error_check_schema);
		ok=false;
	}

	//calculate the checksum for Alarm.xml
	error_check_schema=FileCrc32Assembly(this->ALARM_SCHEMA_XML_FILE_NAME,check_schema);
	//calculate the checksum for Alarm.xsd
	error_check_xml=FileCrc32Assembly(this->ALARM_XML_FILE_NAME,check_xml);

	//Alarm DB
	if(error_check_schema==ERROR_SUCCESS)
	{
		if(error_check_xml==ERROR_SUCCESS)
		{
			//Send Languages Schema CheckSum and Alarm XML CheckSum
			o_socket.Send(Network::MSG_DB_REF,L"alarm.Chk(%x,%x)",check_schema,check_xml);
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"Alarm CheckSum Schema %x Xml %x",check_schema,check_xml); 
		}
		else
		{
			logger.Warning(__FUNCTION__, Logger::PRE_NULL, L"ERROR: Cannot calculate the checksum for Alarm.xml ... %d",error_check_xml);
			ok=false;
		}
	}
	else
	{
		logger.Warning(__FUNCTION__, Logger::PRE_NULL, L"ERROR: Cannot calculate the checksum for Alarm.xsd ... %d",error_check_schema);
		ok=false;
	}

	//calculate the checksum for DownTimeReason.xml
	error_check_schema=FileCrc32Assembly(this->DOWNTIME_REASON_SCHEMA_XML_FILE_NAME,check_schema);
	//calculate the checksum for DownTimeReason.xsd
	error_check_xml=FileCrc32Assembly(this->DOWNTIME_REASON_XML_FILE_NAME,check_xml);

	//DownTime Reason DB
	if(error_check_schema==ERROR_SUCCESS)
	{
		if(error_check_xml==ERROR_SUCCESS)
		{
			//Send Languages Schema CheckSum and DownTime Reason XML CheckSum
			o_socket.Send(Network::MSG_DB_REF,L"downTimeReason.Chk(%x,%x)",check_schema,check_xml);
			logger.Info(__FUNCTION__, Logger::PRE_SOCK_SND, L"DownTime Reason CheckSum Schema %x Xml %x",check_schema,check_xml); 
		}
		else
		{
			logger.Warning(__FUNCTION__, Logger::PRE_NULL, L"ERROR: Cannot calculate the checksum for DownTimeReason.xml ... %d",error_check_xml);
			ok=false;
		}
	}
	else
	{
		logger.Warning(__FUNCTION__, Logger::PRE_NULL, L"ERROR: Cannot calculate the checksum for DownTimeReason.xsd ... %d",error_check_schema);
		ok=false;
	}

	return ok;
}

bool C_DBManager::ParseOrderCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf)
{
	String::StrType command = theCommand.extractCommand();
	if (command.isEmpty())
		return false;

	//order.Add()
	if(command.CompareNoCase(L"Add") || command.CompareNoCase(L"Mod"))
	{
		if(command.CompareNoCase(L"Add"))
      outbuf.setCommandInsert();
		else
			outbuf.setCommandModify();
		
		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);
		//RFAZZI_28/01/2009_1.Sent to SyncroS the "Quality Weight" and "Quality Thickness" when a order is inserted or modified. Required for Setup Validation . 
    //agrassi_FI02-09-2010_1
		//if(token.size()!=99)
    //agrassi_FI05-10-2010_1
		//if(token.size()!=102)
		if(token.size()!=103)
			return false;

		int i=0;
		outbuf.data.setupData.Clear();
		outbuf.data.setupData.key.setSetupId(token[i++].ToInt()); //0 SetupId
		if(!outbuf.data.setupData.setSetupCode(token[i++])) //1 setupCode
			return false;
		outbuf.data.setupData.webWidth_UDdmm = token[i++].ToInt(); //2 WebWidth
		outbuf.data.setupData.trimType = (TrimType)token[i++].ToInt(); //3 trimType
		outbuf.data.setupData.scoreGap_UDdmm = token[i++].ToInt(); //4 scoreGap_UDdmm
		if(!outbuf.data.setupData.quality.setQualityID(token[i++])) //5 QualityID
			return false;
    //agrassi_FI05-10-2010_1
    if(!outbuf.data.setupData.quality.qualityFluteData.setFluteId(token[i++]))
      return false;
		//RFAZZI_28/01/2009_1.Sent to SyncroS the "Quality Weight" and "Quality Thickness" when a order is inserted or modified. Required for Setup Validation . 
		outbuf.data.setupData.quality.setWeightFactor(token[i++].ToInt());  //6 quality weight
		outbuf.data.setupData.quality.qualityFluteData.setThickness_UDdmm(token[i++].ToInt());  //7 quality thickness

		outbuf.data.setupData.validation.trim_UDdmm = token[i++].ToInt(); //8 trim_UDdmm
		
		for (int NumOrder=0; NumOrder < NUM_ORDERS_PER_SETUP; NumOrder++) 
		{
			if(token[i].strsize()<=NUM_CHAR_ORDER_NUMBER) //9 orderNumber
				outbuf.data.setupData.order[NumOrder].setOrderNumber(token[i++].getPtr());		
			else
				return false;
			SYSTEMTIME dummySystemTime;
			token[i++].getDateTime(&dummySystemTime); //10 delivery
			outbuf.data.setupData.order[NumOrder].setDelivery(dummySystemTime);
			if(token[i].strsize()<=NUM_CHAR_CUSTOMER_NAME) //11 customerName
				outbuf.data.setupData.order[NumOrder].customer.setName(token[i++].getPtr());		
			else
				return false;
			outbuf.data.setupData.order[NumOrder].twin.sheetWidth_UDdmm=token[i++].ToInt(); //12 sheetWidth
      outbuf.data.setupData.order[NumOrder].twin.outs=token[i++].ToInt(); //13 outs
			if(token[i].strsize()<=NUM_CHAR_SCORES_MEASURES) //14 scoreMeasures
				wcscpy(outbuf.data.setupData.order[NumOrder].twin.scoreMeasures_UDdmm, token[i++].getPtr());		
			else
				return false;		
			outbuf.data.setupData.order[NumOrder].twin.scoreType = token[i++].getFirstChar();//15 scoreType
			outbuf.data.setupData.order[NumOrder].twin.positType = (PositType)token[i++].ToInt(); //16 positType
			outbuf.data.setupData.order[NumOrder].twin.teletwinOffset_UDdmm = token[i++].ToInt(); //17 teletwinOffset_UDdmm
			outbuf.data.setupData.order[NumOrder].twin.scoreGap_UDdmm = token[i++].ToInt(); //18 scoreGap_UDdmm
			outbuf.data.setupData.order[NumOrder].knife.levelName = (LevelName)token[i++].ToInt(); //19 levelName
      outbuf.data.setupData.order[NumOrder].knife.sheetLength_UDdmm = token[i++].ToInt(); //20 sheetLength
			outbuf.data.setupData.order[NumOrder].knife.segmentSheetCount = token[i++].ToInt(); //21 segmentSheetCount
			outbuf.data.setupData.order[NumOrder].stacker.sheetsPerStack = token[i++].ToInt();	//22 sheetsPerStack			
			outbuf.data.setupData.order[NumOrder].materialHandling.stacksPerPallet = token[i++].ToInt(); //23 stacksPerPallet
			outbuf.data.setupData.order[NumOrder].materialHandling.sending = (PalletSending)(token[i++].ToInt()); //24 Sending
			//agrassi_FI27-06-08
			//bool cutToMark = (NumOrder==2) ? token[i].ToBool() : token[i++].ToBool();
			bool cutToMark = token[i++].ToBool();
			if( cutToMark )
				outbuf.data.setupData.order[NumOrder].knife.cutToPattern.setTypeCutToMark(); //25 CutToMark
			else
				outbuf.data.setupData.order[NumOrder].knife.cutToPattern.setTypeNo(); //26 CutToMark

      outbuf.data.setupData.order[NumOrder].knife.cutToPattern.setCutToPatternID(token[i++]);
      outbuf.data.setupData.order[NumOrder].toPrint.setDataToPrint(token[i++],1);
      outbuf.data.setupData.order[NumOrder].toPrint.setDataToPrint(token[i++],2);
      outbuf.data.setupData.order[NumOrder].toPrint.setDataToPrint(token[i++],3);
      outbuf.data.setupData.order[NumOrder].toPrint.setDataToPrint(token[i++],4);

      outbuf.data.setupData.order[NumOrder].stacker.setContinuousStack(token[i++].ToBool());
      outbuf.data.setupData.order[NumOrder].materialHandling.pallet.setOnPlatform( token[i++].ToBool() );
      outbuf.data.setupData.order[NumOrder].materialHandling.pallet.setNote(token[i++]);
      outbuf.data.setupData.order[NumOrder].customer.setDestinationAddress(token[i++]);
      outbuf.data.setupData.order[NumOrder].materialHandling.setBoxMachineCode(token[i++]);
      outbuf.data.setupData.order[NumOrder].materialHandling.setBoxMachineName(token[i++]);
      //agrassi_FI24-02-2010_3
      if ((token[i].ToInt()>=Right) && (token[i].ToInt()<=Front))
        outbuf.data.setupData.order[NumOrder].stacker.side=(DischargeSide)(token[i++].ToInt());
      else
      {
         outbuf.data.setupData.order[NumOrder].stacker.side=Right;
         i++;
      }
      if ((token[i].ToInt()>=Total) && (token[i].ToInt()<=Scrap))
        outbuf.data.setupData.order[NumOrder].total.balance=(Balance)(token[i++].ToInt());
      else
      {
        outbuf.data.setupData.order[NumOrder].total.balance=Total;
         i++;
      }
      //agrassi_FI29-09-2010_2
      //agrassi_FI02-09-2010_1
      //outbuf.data.setupData.order[NumOrder].stacker.bundle=(_wcsicmp(token[i++], L"false")==0);
      outbuf.data.setupData.order[NumOrder].stacker.bundle=(_wcsicmp(token[i++], L"true")==0);
		}
			
		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"ORDER Command: %s setupCode: %s",command.getPtr(),outbuf.data.setupData.setupCode);

		int res = F_Validate(&outbuf.data.setupData,true,true,true,true,true);
		if(res >= SETUP_OK) 
		{
			logger.Info(__FUNCTION__, Logger::PRE_NULL, L"Validation DLL <-> setupCode: %s Valid ... %S",outbuf.data.setupData.setupCode, DecodeValidationError(res));
			DBMSSyncroS.Write(&outbuf); //send the command to DBMS
			logger.Info(__FUNCTION__, Logger::PRE_PIPE_SND, L"ORDER Command: %s setupCode: %s",command.getPtr(),outbuf.data.setupData.setupCode);
			//notify to the updating client the success of order validation
			//it will make the client change the state of the insOrder page.
			o_socket.Send(Network::MSG_DB_SWI,L"OrderValidationSucceded()");
		}
		else
		{
			logger.Warning(__FUNCTION__, Logger::PRE_NULL, L"Validation DLL <-> setupCode: %s InValid ... %S", outbuf.data.setupData.setupCode, DecodeValidationError(res));
			o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[§%S#:§%S], b[true])", stringer(SYCS_05), DecodeValidationError(res));			
		}

		return true;
	}

	//order.Clr()
	if(command.CompareNoCase(L"Clr"))
	{
		outbuf.setCommandClear();

		//send the command to DBMS
		DBMSSyncroS.Write(&outbuf);

		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"ORDER Command: CLEAR");

		return true;
	}

	//order.Del(Id0,Id1,Id2,...)
	if(command.CompareNoCase(L"Del"))
	{
		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);
		
		outbuf.setCommandDel();

		for(int i=0; i < (int) token.size(); i++)
			outbuf.multipleOperation.key[i]=token[i].ToInt();
		
		//send the command to DBMS
		DBMSSyncroS.Write(&outbuf);

		wchar_t str[5*MAX_QUEUE];	
		outbuf.multipleOperation.getKeyStrForLog(str);

		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"ORDER Command: DEL %s",str);

		return true;
	}

	//order.Move(InPosition,Id0,Id1,Id2,...)
	if(command.CompareNoCase(L"Move"))
	{
		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);
	
		outbuf.setCommandMove();

		//InPosition
		outbuf.multipleOperation.setInPosition(token[0].ToInt());

		for(int i=1; i < (int) token.size(); i++)
			outbuf.multipleOperation.key[i-1]=token[i].ToInt();

		//send the command to DBMS
		DBMSSyncroS.Write(&outbuf);

		wchar_t str[5*MAX_QUEUE];	
		outbuf.multipleOperation.getKeyStrForLog(str);

		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"ORDER Command: MOVE %s in position %d",
			str,
			outbuf.multipleOperation.getInPosition());

		return true;
	}

  //agrassi_FI11-11-2010_1
	if(command.CompareNoCase(L"SwapOrders"))
	{
		outbuf.setCommandSwap();
		outbuf.multipleOperation.key[0]=theCommand.ToInt();
		//send the command to DBMS
		DBMSSyncroS.Write(&outbuf);
		wchar_t str[5*MAX_QUEUE];	
		outbuf.multipleOperation.getKeyStrForLog(str);
		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"ORDER Command: SWAP for setup %s", str);
		return true;
	}

	//order.ExtraCutInCurrent( level, numCut )
	if(command.CompareNoCase(L"ExtraSheetsInCurrent"))
	{
		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);
		outbuf.setCommandExtraSheetsInCurrent();

		outbuf.multipleOperation.key[0] = token[0].ToInt();
		outbuf.multipleOperation.key[1] = token[1].ToInt();

		//send the command to DBMS
		DBMSSyncroS.Write(&outbuf);

		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"ORDER Command: Extra Sheets In Current: Level %d, NumSheets %d",
			outbuf.multipleOperation.key[0], outbuf.multipleOperation.key[1]);

		return true;
	}

	return false;
}

bool C_DBManager::ParsePaperCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf)
{
	String::StrType command = theCommand.extractCommand();
	if (command.isEmpty())
		return false;

	//paper.Del(TypeId);
	if(command.CompareNoCase(L"Del"))
	{
		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);
		if(token.size()!=2)
			return false;

		//paper ID
		if(!outbuf.data.paperData.setPaperID(token[0]))
        	return false;

		//WeightFactor_UWg_m
		outbuf.data.paperData.setWeightFactor_UWg_m(token[1].ToInt());

		//Set the type of message
		outbuf.setCommandDel();

		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"PAPER Command: DEL PaperType: %s", outbuf.data.paperData.getPaperID());

		return true;
	}

	//paper.AddRecord(
	//      TypeId,
	//			CoreDiameterUD,
	//			WeightFactorUWF,
	//			MaxSpliceSpeed,
	//			MaxSpliceSpeedUV[0],
	//				.....
	//			MaxSpliceSpeedUV[7])
	if(command.CompareNoCase(L"Add")||command.CompareNoCase(L"Mod"))
	{
		if(command.CompareNoCase(L"Mod"))
			outbuf.setCommandModify();
		else
			outbuf.setCommandInsert();

		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);
		if(token.size()!=17)
			return false;

		//TypeId
		if(!outbuf.data.paperData.setPaperID(token[0]))
        	return false;
		//CoreDiameterUD
		outbuf.data.paperData.setCoreDiameter_UDdmm(token[1].ToInt() );
		//WeightFactorUWF
		outbuf.data.paperData.setWeightFactor_UWg_m(token[2].ToInt());
		//Text
		outbuf.data.paperData.setText(token[3]);
		//Thickness
		outbuf.data.paperData.setDefaultThickness_UDdmmm(token[4].ToInt());
		//Classification
		outbuf.data.paperData.setClassification(token[5]);
		//spare
			//token[6]
		//MaxSpliceSpeedUV 0
		outbuf.data.paperData.setMaxSpliceSpeed_UVm_m(0,token[7].ToInt());
		//MaxSpliceSpeedUV 1
		outbuf.data.paperData.setMaxSpliceSpeed_UVm_m(1,token[8].ToInt());
		//MaxSpliceSpeedUV 2
		outbuf.data.paperData.setMaxSpliceSpeed_UVm_m(2,token[9].ToInt());
		//MaxSpliceSpeedUV 3
		outbuf.data.paperData.setMaxSpliceSpeed_UVm_m(3,token[10].ToInt());
		//MaxSpliceSpeedUV 4
		outbuf.data.paperData.setMaxSpliceSpeed_UVm_m(4,token[11].ToInt());
		//MaxSpliceSpeedUV 5
		outbuf.data.paperData.setMaxSpliceSpeed_UVm_m(5,token[12].ToInt());
		//MaxSpliceSpeedUV 6
		outbuf.data.paperData.setMaxSpliceSpeed_UVm_m(6,token[13].ToInt());
		//spare
			//token[14]
		//spare
			//token[15]
		//spare
			//token[16]
		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"PAPER Command: %s PaperType: %s",command.getPtr() ,outbuf.data.paperData.getPaperID());

		return true;
	}

	//paper.clr()
	if(command.CompareNoCase(L"Clr"))
	{
		outbuf.setCommandClear();

		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"PAPER Command: CLEAR");

		return true;
	}
		return false;
}

bool C_DBManager::ParsePastOrderCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf)
{
	String::StrType command = theCommand.extractCommand();
	if (command.isEmpty())
		return false;

	//pastOrder.Restore(SetupID,StartTime);
	if(command.CompareNoCase(L"Restore"))
	{
		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);
		if(token.size()!=2)
			return false;

		outbuf.setCommandRestore();
		outbuf.data.pastOrderData.setupData.key.setSetupId(token[0].ToInt());

		SYSTEMTIME dummySystemTime;
		token[1].getDateTime(&dummySystemTime);
		outbuf.data.pastOrderData.setStartTime(dummySystemTime);

		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"PAST ORDER Command  : RESTORE %d",outbuf.data.setupData.key.getSetupId());

		return true;
	};
	
	return false;
}

bool C_DBManager::ParseShiftCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf)
{
	String::StrType command = theCommand.extractCommand();
	if (command.isEmpty())
		return false;

	//shift.Stop();
	if(command.CompareNoCase(L"Stop"))
	{
		outbuf.setCommandShift();
		outbuf.data.shiftData.setStopShift();

		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"SHIFT Command  : STOP");

		return true;
	};

	//shift.Add(
	//					shiftId
	//					shiftManagerName
	//					nrOfPeople
	//					);
	if(command.CompareNoCase(L"Shift"))
	{
		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);
		if(token.size()!=3)
			return false;

		outbuf.setCommandShift();
		
		//shiftId
		outbuf.data.shiftData.setShiftId(token[0].getFirstChar());

		//shiftManagerName
		outbuf.data.shiftData.setShiftManagerName(token[1]);

		//nrOfPeople
		outbuf.data.shiftData.setNumberOfPeople(token[2].ToInt());

		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"SHIFT Command  : ADD %c %s", outbuf.data.shiftData.getShiftId(), outbuf.data.shiftData.getShiftManagerName());

		return true;
	};
	
	return false;
}

//ABello_FI Aggiunta gestione CutToPattern
bool C_DBManager::ParseCutToPatternCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf)
{
	String::StrType command = theCommand.extractCommand();
	if (command.isEmpty())
		return false;

  //CutToPattern.Del(CutToPatternId);
	if(command.CompareNoCase(L"Del"))
	{
		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);
    if(token.size()!=1)	
      return false;
		//CutToPatternId
    outbuf.data.cutToPatternData.setCutToPatternID(token[0]);
    //Set the command type
		outbuf.setCommandDel();
    logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"CUT TO PATTERN Command: DEL CutToPatternID: %s ",outbuf.data.cutToPatternData.getCutToPatternID());
		return true;
  }

  //PMASSARO_FI18/01/10 - Non modifichiamo più la pressione di cordonatura impostata nell'ordine se non espressamente richiesto
  /*
	if(command.CompareNoCase(L"Add")||command.CompareNoCase(L"ModWithSetup")||command.CompareNoCase(L"Mod"))
  */
  if(command.CompareNoCase(L"Add")||command.CompareNoCase(L"ModWithSetup")||command.CompareNoCase(L"Mod")||command.CompareNoCase(L"ModWithSetup_NoPressure"))
	{
		if(command.CompareNoCase(L"Add"))
			outbuf.setCommandInsert();

		if(command.CompareNoCase(L"Mod"))
			outbuf.setCommandModify();

		if(command.CompareNoCase(L"ModWithSetup"))
		{
			outbuf.setCommandModify();
			outbuf.multipleOperation.setInPosition(1);
		}
      
    //PMASSARO_FI18/01/10 - Non modifichiamo più la pressione di cordonatura impostata nell'ordine se non espressamente richiesto
		if(command.CompareNoCase(L"ModWithSetup_NoPressure"))
		{
			outbuf.setCommandModify();
			outbuf.multipleOperation.setInPosition(1);
      outbuf.multipleOperation.setUserChar(L"NOT_UPDATE_PRESSURE");
		}

		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);	
		//RFAZZI_FI12/08/2008_1.Added lateral position of pattern
		//if(token.size()!=35)
		if(token.size()!=36)
				return false;

    int i = 0;
		//CutToPatternID
		outbuf.data.cutToPatternData.setCutToPatternID(token[i++]);
		//Text
    outbuf.data.cutToPatternData.setText(token[i++]);
		//MarkDistance_UDdmm 0
    outbuf.data.cutToPatternData.setMarkDistance_UDdmm(0,token[i++].ToInt());		
		//MarkDistance_UDdmm 1
    outbuf.data.cutToPatternData.setMarkDistance_UDdmm(1,token[i++].ToInt());		
		//MarkDistance_UDdmm 2
    outbuf.data.cutToPatternData.setMarkDistance_UDdmm(2,token[i++].ToInt());		
		//MarkDistance_UDdmm 3
    outbuf.data.cutToPatternData.setMarkDistance_UDdmm(3,token[i++].ToInt());		
		//MarkDistance_UDdmm 4
    outbuf.data.cutToPatternData.setMarkDistance_UDdmm(4,token[i++].ToInt());		
		//MarkDistance_UDdmm 5
    outbuf.data.cutToPatternData.setMarkDistance_UDdmm(5,token[i++].ToInt());		
		//MarkDistance_UDdmm 6
    outbuf.data.cutToPatternData.setMarkDistance_UDdmm(6,token[i++].ToInt());		
		//MarkDistance_UDdmm 7
    outbuf.data.cutToPatternData.setMarkDistance_UDdmm(7,token[i++].ToInt());		
		//MarkDistance_UDdmm 8
    outbuf.data.cutToPatternData.setMarkDistance_UDdmm(8,token[i++].ToInt());		
		//MarkDistance_UDdmm 9
    outbuf.data.cutToPatternData.setMarkDistance_UDdmm(9,token[i++].ToInt());		
    //MarkDistance_UDdmm_Spare1
    i++;
    //MarkDistance_UDdmm_Spare2
    i++;
    //MarkDistance_UDdmm_Spare3
    i++;
    //MarkDistance_UDdmm_Spare4
    i++;
    //MarkDistance_UDdmm_Spare5
    i++;
    //EyeContrast
    outbuf.data.cutToPatternData.setEyeContrast(token[i++].ToInt());
    //CutOffset_UDdmm
    outbuf.data.cutToPatternData.setCutOffset_UDdmm(token[i++].ToInt());
    //FirstMarkId
    outbuf.data.cutToPatternData.setFirstMarkId(token[i++].ToInt());
    //WindowBeforePattern_UDdmm
    outbuf.data.cutToPatternData.setWindowBeforePattern_UDdmm(token[i++].ToInt());
    //WindowAfterPattern_UDdmm
    outbuf.data.cutToPatternData.setWindowAfterPattern_UDdmm(token[i++].ToInt());

		//RFAZZI_FI12/08/2008_1.Added lateral position of pattern
		//Lateral Pattern Position
		outbuf.data.cutToPatternData.setLateralPatternPosition_UDdmm(token[i++].ToInt());


		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"CUT TO PATTERN Command: %s CutToPatternID: %s ",command.getPtr(), outbuf.data.cutToPatternData.getCutToPatternID());

		return true;
	}

	//CutToPattern.Clr()
	if(command.CompareNoCase(L"Clr"))
	{
		outbuf.setCommandClear();
		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"CUT TO PATTERN Command: CLEAR ");
		return true;
	}

	return false;
}

bool C_DBManager::ParseDownTimeCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf)
{
	String::StrType command = theCommand.extractCommand();
	if (command.isEmpty())
		return false;

	//downTime.Add(
	//					shiftId,
	//					Start Time,
	//					Finish Time,
	//					Code Of Stop,
	//					Description Of Stop,
	//					Place Code Of Stop,
	//					Place Description Of Stop,
	//					Text,
	//					Setup Id,
	//					Setup ModId,
	//);
	if(command.CompareNoCase(L"Add")||command.CompareNoCase(L"Mod"))
	{
		outbuf.setCommandModify();

		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);
		//MSTAGETTI_FI-25-06-2010
		//if(token.size()!=10)
		if(token.size()!=12)
			return false;

		//0 shiftId
		outbuf.data.downTime.setShiftIdOfStop(token[0].getFirstChar());
		//1 Start Time
		SYSTEMTIME time;
		if(!token[1].getDateTime(&time)) 
			return false;
		outbuf.data.downTime.setStartStoppageTime(time);
		//2 Finish Time
		if(!token[2].getDateTime(&time)) 
			return false;
		outbuf.data.downTime.setFinishStoppageTime(time);
		//3 Code Of Stop
		outbuf.data.downTime.setCodeOfStop(token[3]);
		//4 Description Of Stop
		outbuf.data.downTime.setDescriptionOfStop(token[4]);
		//5 Place Code Of Stop
		outbuf.data.downTime.setPlaceCodeOfStop(token[5]);
		//6 Place Description Of Stop
		outbuf.data.downTime.setPlaceDescriptionOfStop(token[6]);
		//7 Text
		outbuf.data.downTime.setText(token[7]);
		//8 Setup Id
		outbuf.data.downTime.setupKeyDownTime.setSetupId(token[8].ToInt());
		//9 Setup ModId
		outbuf.data.downTime.setupKeyDownTime.setModId(token[9].ToInt());		
		//MSTAGETTI_FI-25-06-2010
		//10 Zone Customer Key
		outbuf.data.downTime.setCustomerCodeOfZone(token[10]);
		//11 Reason Customer Key
		outbuf.data.downTime.setCustomerCodeOfReason(token[11]);


		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"DOWNTIME Command  : ADD %s", outbuf.data.downTime.getCodeOfStop());

		return true;
	};
	
	return false;
}

bool C_DBManager::ParseFluteCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf)
{
	String::StrType command = theCommand.extractCommand();
	if (command.isEmpty())
		return false;

	//flute.Del(FluteID);
	if(command.CompareNoCase(L"Del"))
		{
			std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);
			if(token.size()!=1)
				return false;

			outbuf.setCommandDel();

			if(!outbuf.data.fluteData.setFluteId(token[0]))
				return false;

			logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"FLUTE Command: DEL FluteId: %d ", outbuf.data.fluteData.getFluteId());

			return true;
		}

	//flute.Add(
	//      FluteId,
	//			SFSPosition,
	//			FluteText, 
		// DRY DATA
	//			ScoreGap_UDdmm,	
	//			GrindingParameter_UDm,
	//			LubricationParameter_UDm,
	//			TrimGrindingsMultiplier,
	//			GrindingTime_UDm,
	//			LubrificationTime_UTs,
	//			ProductionSpeed_UVm_m,
	//			AOCSpeed_UVm_m,
	//			BoardThickness_UDmm,
	//			TerminalFluteCode
		// WET DATA
	//			MinWorkingPressure_UPerc,				
	//			SpeedForTargetPressure_UVm_m,		
	//			TargetJack1_UPerc,							
	//			TargetJack2_UPerc,							
	//			TargetJack3_UPerc,							
	//			TargetJack4_UPerc,							
	//			SteamPressureZone1_UPdb,				
	//			SteamPressureZone2_UPdb,				
	//			SteamPressureZone3_UPdb,				
	//			WrappingMaxSpeed_UVm_m,					
	//			MaxWrapArm_UPerc,								
	//			WrappingMinSpeed_UVm_m,					
	//			MinWrapArm_UPerc;								
	//			)

	if(command.CompareNoCase(L"Add")||command.CompareNoCase(L"Mod"))
	{
		if(command.CompareNoCase(L"Mod"))
			outbuf.setCommandModify();
		else
			outbuf.setCommandInsert();

		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);
			if(token.size()!=26)
				return false;

		if(!outbuf.data.fluteData.setFluteId(token[0])) //FluteId
				return false;
		if(token[1].strsize()<=FLUTE_SFSPOSITION_LEN) //SFSPosition
			outbuf.data.fluteData.setSFSPosition(token[1]);
		else
			return false;
		if(!outbuf.data.fluteData.setFluteText(token[2])) //FluteText
			return false;
//DRY DATA
		outbuf.data.fluteData.setScoreGap(token[3].ToInt());//ScoreGap_UDdmm
		outbuf.data.fluteData.setGrindingParameter(token[4].ToInt());//GrindingParameter_UDm
		outbuf.data.fluteData.setLubricationParameter(token[5].ToInt());//LubricationParameter_UDm
		outbuf.data.fluteData.setTrimGrindingsMultiplier(token[6].ToInt());//TrimGrindingsMultiplier
		outbuf.data.fluteData.setGrindingDistance(token[7].ToInt());//GrindingDistance_UDm
		outbuf.data.fluteData.setLubricationTime(token[8].ToInt());//LubricationTime_UTs
		outbuf.data.fluteData.setProductionSpeed_UVm_m(token[9].ToInt());//ProductionSpeed_UVm_m
		outbuf.data.fluteData.setAOCSpeed_UVm_m(token[10].ToInt());//AOCSpeed_UVm_m
		outbuf.data.fluteData.setThickness_UDdmm(token[11].ToInt());//Thickness_UDdmm
		outbuf.data.fluteData.setTerminalFluteCode(token[12].ToInt());//TerminalFluteCode
		//RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
		outbuf.data.fluteData.setTwinFlapDisabled(token[13].ToInt());//Internal Twin400 Flap Disabled
    //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
    outbuf.data.fluteData.setCruisingSpeed_UVm_m(token[14].ToInt());//CruisingSpeed_UVm_m
	
		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"FLUTE Command: %s FluteId: %s", command.getPtr(), outbuf.data.fluteData.getFluteId());

		return true;
	}

	//flute.Clr()
	if(command.CompareNoCase(L"Clr"))
	{
		outbuf.setCommandClear();
		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"FLUTE Command: CLEAR");
		return true;
	}
	return false;
}

bool C_DBManager::ParseQualityCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf)
{
	String::StrType command = theCommand.extractCommand();
	if (command.isEmpty())
		return false;

	//quality.Del(QualityCode);
	if(command.CompareNoCase(L"Del"))
	{
		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);
			if(token.size()!=1)
				return false;
		//QualityCode
		if(!outbuf.data.qualityData.setQualityID(token[0]))
			return false;
		//Set the command type
		outbuf.setCommandDel();

		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"QUALITY Command: DEL QualityCode: %s ",outbuf.data.qualityData.getQualityID());

		return true;
	}

	//quality.Add(
	//			QualityCode,
	//			QualityText,
	//      FluteId,
	//			ScoreGap_UDdmm,	
	//			GrindingParameter_UDm,
	//			LubricationParameter_UDm,
	//			TrimGrindingsMultiplier,
	//			GrindingTime_UDm,
	//			LubrificationTime_UTs,
	//			ProductionSpeed_UVm_m,
	//			AOCSpeed_UVm_m,
	//			BoardThickness_UDmm,
	//			sheets_per_stack
	//			BundleLayersPerStack
	//			BundleSheetsPerBundle
	//			BundleReverseMode
	//			TypeId 0
	//			MaxSpliceSpeedUV 0
	//			TypeId 1
	//			MaxSpliceSpeedUV 1
	//			TypeId 2
	//			MaxSpliceSpeedUV 2
	//			TypeId 3
	//			MaxSpliceSpeedUV 3
	//			TypeId 4
	//			MaxSpliceSpeedUV 4
	//			TypeId 5
	//			MaxSpliceSpeedUV 5
	//			TypeId 6
	//			MaxSpliceSpeedUV 6
	//			WeightFactorUWg_m
	//			PrePrint
	//			MinWorkingPressure_UPerc
	//			SpeedForTargetPressure_UVm_m
	//			TargetJack1_UPerc
	//			TargetJack2_UPerc
	//			TargetJack3_UPerc
	//			TargetJack4_UPerc
	//			SteamPressureZone1_UPdb
	//			SteamPressureZone2_UPdb
	//			SteamPressureZone3_UPdb
	//			WrappingMaxSpeed_UVm_m
	//			MaxWrapArm_UPerc
	//			WrappingMinSpeed_UVm_m
	//			MinWrapArm_UPerc
	//			) 45 Items
 
  //PMASSARO_FI18/01/10 - Non modifichiamo più la pressione di cordonatura impostata nell'ordine se non espressamente richiesto
	/*
  if(command.CompareNoCase(L"Add")||command.CompareNoCase(L"ModWithSetup")||command.CompareNoCase(L"Mod"))
  */
  if(command.CompareNoCase(L"Add")||command.CompareNoCase(L"ModWithSetup")||command.CompareNoCase(L"Mod")||command.CompareNoCase(L"ModWithSetup_NoPressure"))
	{

		if(command.CompareNoCase(L"Add"))
			outbuf.setCommandInsert();

		if(command.CompareNoCase(L"Mod"))
			outbuf.setCommandModify();

		if(command.CompareNoCase(L"ModWithSetup"))
		{
			outbuf.setCommandModify();
			outbuf.multipleOperation.setInPosition(1);
		}

    //PMASSARO_FI18/01/10 - Non modifichiamo più la pressione di cordonatura impostata nell'ordine se non espressamente richiesto
		if(command.CompareNoCase(L"ModWithSetup_NoPressure"))
		{
			outbuf.setCommandModify();
			outbuf.multipleOperation.setInPosition(1);
      outbuf.multipleOperation.setUserChar(L"NOT_UPDATE_PRESSURE");
		}

		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);	
		if(token.size()!=45)
				return false;

		//QualityCode
		if(!outbuf.data.qualityData.setQualityID(token[0]))
			return false;		
		//QualityText
		if(!outbuf.data.qualityData.setQualityText(token[1]))
			return false;
		//FluteId
		if(!outbuf.data.qualityData.qualityFluteData.setFluteId(token[2]))
			return false;

		//DRY DATA
		//ScoreGap_UDdmm
		outbuf.data.qualityData.qualityFluteData.setScoreGap(token[3].ToInt());
		//GrindingParameter_UDm
		outbuf.data.qualityData.qualityFluteData.setGrindingParameter(token[4].ToInt());
		//LubricationParameter_UDm
		outbuf.data.qualityData.qualityFluteData.setLubricationParameter(token[5].ToInt());
		//TrimGrindingsMultiplier
		outbuf.data.qualityData.qualityFluteData.setTrimGrindingsMultiplier(token[6].ToInt());
		//GrindingDistance_UDm
		outbuf.data.qualityData.qualityFluteData.setGrindingDistance(token[7].ToInt());
		//LubricationTime_UTs
		outbuf.data.qualityData.qualityFluteData.setLubricationTime(token[8].ToInt());
		//ProductionSpeed_UVm_m
		outbuf.data.qualityData.qualityFluteData.setProductionSpeed_UVm_m(token[9].ToInt());
		//AOCSpeed_UVm_m
		outbuf.data.qualityData.qualityFluteData.setAOCSpeed_UVm_m(token[10].ToInt());
		//Thickness_UDdmm
		outbuf.data.qualityData.qualityFluteData.setThickness_UDdmm(token[11].ToInt());
		//ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
    outbuf.data.qualityData.qualityFluteData.setCruisingSpeed_UVm_m(token[12].ToInt());
		//BundleLayersPerStack
		outbuf.data.qualityData.setBundleLayersPerStack(token[13].ToInt());
		////BundleSheetsPerBundle
		//outbuf.data.qualityData.setBundleSheetsPerBundle(token[14].ToInt());
		//BundleReverseMode
		outbuf.data.qualityData.setBundleReverseMode(token[15]);

		//WET DATA
		//Paper 1
		if(!outbuf.data.qualityData.paper1.setPaperID(token[16])) //TypeId
			return false;
		outbuf.data.qualityData.paper1.setWeightFactor_UWg_m(token[17].ToInt()); //WeightFactor_UWg_m
		//Paper 2
		if(!outbuf.data.qualityData.paper2.setPaperID(token[18])) //TypeId
			return false;
		outbuf.data.qualityData.paper2.setWeightFactor_UWg_m(token[19].ToInt()); //WeightFactor_UWg_m
		//Paper 3
		if(!outbuf.data.qualityData.paper3.setPaperID(token[20])) //TypeId
			return false;
		outbuf.data.qualityData.paper3.setWeightFactor_UWg_m(token[21].ToInt()); //WeightFactor_UWg_m
		//Paper 4
		if(!outbuf.data.qualityData.paper4.setPaperID(token[22])) //TypeId
			return false;
		outbuf.data.qualityData.paper4.setWeightFactor_UWg_m(token[23].ToInt()); //WeightFactor_UWg_m
		//Paper 5
		if(!outbuf.data.qualityData.paper5.setPaperID(token[24])) //TypeId
			return false;
		outbuf.data.qualityData.paper5.setWeightFactor_UWg_m(token[25].ToInt()); //WeightFactor_UWg_m
		//Paper 6
		if(!outbuf.data.qualityData.paper6.setPaperID(token[26])) //TypeId
			return false;
		outbuf.data.qualityData.paper6.setWeightFactor_UWg_m(token[27].ToInt()); //WeightFactor_UWg_m
		//Paper 7
		if(!outbuf.data.qualityData.paper7.setPaperID(token[28])) //TypeId
			return false;
		outbuf.data.qualityData.paper7.setWeightFactor_UWg_m(token[29].ToInt()); //WeightFactor_UWg_m
		//WeightFactorUWg_m
		outbuf.data.qualityData.setWeightFactor(token[30].ToInt());
		//PrePrint
		outbuf.data.qualityData.setPrePrint(token[31]);
		//PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
		outbuf.data.qualityData.setInclinateLastBelt(token[32]);

		//RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
		outbuf.data.qualityData.qualityFluteData.setTwinFlapDisabled(token[33].ToInt());

		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"QUALITY Command: %s QualityCode: %s ",command.getPtr(), outbuf.data.qualityData.getQualityID());

		return true;
	}

	//quality.Clr()
	if(command.CompareNoCase(L"Clr"))
	{
		outbuf.setCommandClear();
		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"QUALITY Command: CLEAR ");
		return true;
	}

	return false;
}

bool C_DBManager::ParseDBManagerCommand(String::StrType theCommand)
{
	String::StrType command = theCommand.extractCommand();
	if (command.isEmpty())
		return false;

	//DataBase File language update Commands
	if(command.CompareNoCase(L"syncroConfig"))
	{
		o_socket.TransmitFile(this->SYNCRO_INI_FILE_NAME);
		o_socket.Send(Network::MSG_DB_REF,L"syncroConfig.Load()");
		return true;
	};

	//DataBase File language update Commands
	if(command.CompareNoCase(L"language"))
	{
		o_socket.TransmitFile(this->LANGUAGES_SCHEMA_XML_FILE_NAME);
		o_socket.TransmitFile(this->LANGUAGES_XML_FILE_NAME);
		o_socket.Send(Network::MSG_DB_REF,L"language.Load()");
		return true;
	};

	//DataBase File Messages update Commands
	if(command.CompareNoCase(L"message"))
	{
		o_socket.TransmitFile(this->MESSAGES_SCHEMA_XML_FILE_NAME);
		o_socket.TransmitFile(this->MESSAGES_XML_FILE_NAME);
		o_socket.Send(Network::MSG_DB_REF,L"message.Load()");
		return true;
	};

	//DataBase File Alarm update Commands
	if(command.CompareNoCase(L"alarm"))
	{
		o_socket.TransmitFile(this->ALARM_SCHEMA_XML_FILE_NAME);
		o_socket.TransmitFile(this->ALARM_XML_FILE_NAME);
		o_socket.Send(Network::MSG_DB_REF,L"alarm.Load()");
		return true;
	};

	//DataBase File DownTime Reason update Commands
	if(command.CompareNoCase(L"downTimeReason"))
	{
		o_socket.TransmitFile(this->DOWNTIME_REASON_SCHEMA_XML_FILE_NAME);
		o_socket.TransmitFile(this->DOWNTIME_REASON_XML_FILE_NAME);
		o_socket.Send(Network::MSG_DB_REF,L"downTimeReason.Load()");
		return true;
	};

	if(command.CompareNoCase(L"Ver"))
	{
		//Init message
		NamedPipe::DataBasePipe outbuf;
		outbuf.CleanDataBasePipe();
		//set the Db to
		outbuf.setDbVerDB();

		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);

		if(token.size()!=9)
			return false;

		String::StrType tokenDataBasePipeVer = token[0];//Setup
		String::StrType tokenFlute = token[1];//Flute
		String::StrType tokenPaper = token[2];//paper
		String::StrType tokenQuality = token[3];//quality
		String::StrType tokenPastOrder = token[4];//pastOrder
		String::StrType tokenShift = token[5];//Shift
		String::StrType tokenDownTime = token[6];//DownTime
		String::StrType tokenCutToPattern = token[7];//CutToPattern
		String::StrType tokenPastRolls = token[8];//PastRolls
		
		//SetupVer
		if(!tokenDataBasePipeVer.getDateTime(&outbuf.data.DBMSDBVer.DbVer.setupVer))
			return false;
		
		//fluteVer
		if(!tokenFlute.getDateTime(&outbuf.data.DBMSDBVer.DbVer.fluteVer))
			return false;
		
		//paperVer
		if(!tokenPaper.getDateTime(&outbuf.data.DBMSDBVer.DbVer.paperVer))
			return false;
		
		//qualityVer
		if(!tokenQuality.getDateTime(&outbuf.data.DBMSDBVer.DbVer.qualityVer))
			return false;

		//pastOrderVer
		if(!tokenPastOrder.getDateTime(&outbuf.data.DBMSDBVer.DbVer.pastOrderVer))
			return false;

		//ShiftVer
		if(!tokenShift.getDateTime(&outbuf.data.DBMSDBVer.DbVer.shiftVer))
			return false;

		//downTimeVer
		if(!tokenDownTime.getDateTime(&outbuf.data.DBMSDBVer.DbVer.downTimeVer))
			return false;

		//CutToMarkVer
		if(!tokenCutToPattern.getDateTime(&outbuf.data.DBMSDBVer.DbVer.cutToPatternVer))
			return false;

		//PastRolls
		if(!tokenPastRolls.getDateTime(&outbuf.data.DBMSDBVer.DbVer.pastRollsVer))
			return false;
		
		logger.Info(__FUNCTION__, Logger::PRE_SOCK_RCV_AND_PIPE_SEND, L"DataBase Check Version");

		//send the command to DBMS
		DBMSSyncroS.Write(&outbuf);

		return true;
	}

	return false;
}

bool C_DBManager::ManageDB(String::StrType command)
{	
	logger.Info(__FUNCTION__, L"DB Manager has received command: %s", command.getPtr());

	String::StrType dataBase=command.extractFirstToken(L".");

	//DataBase File version update
	if(dataBase.CompareNoCase(L"DbManager"))
		return this->ParseDBManagerCommand(command);

	//PAPER
	if(dataBase.CompareNoCase(L"paper"))
	{
		NamedPipe::DataBasePipe outbuf;//Init message
		outbuf.CleanDataBasePipe();
		outbuf.setPaperDB();//set the Db to
		if(this->ParsePaperCommand(command,outbuf))
		{
			DBMSSyncroS.Write(&outbuf);//send the command to DBMS
			return true;
		}
		else
			return false;
	}

	//FLUTE
	if(dataBase.CompareNoCase(L"flute"))
	{
		NamedPipe::DataBasePipe outbuf;//Init message
		outbuf.CleanDataBasePipe();
		outbuf.setFluteDB();//set the Db to
		if(this->ParseFluteCommand(command,outbuf))
		{
			DBMSSyncroS.Write(&outbuf);//send the command to DBMS
			return true;
		}
		else
			return false;
	}

	//QUALITY
	if(dataBase.CompareNoCase(L"quality"))
	{
		NamedPipe::DataBasePipe outbuf;//Init message
		outbuf.CleanDataBasePipe();
		outbuf.setQualityDB();//set the Db to
		if(this->ParseQualityCommand(command,outbuf))
		{
			DBMSSyncroS.Write(&outbuf);//send the command to DBMS
			return true;
		}
		else
			return false;
	}

	//ORDER
	if(dataBase.CompareNoCase(L"order"))
	{
		NamedPipe::DataBasePipe outbuf;//Init message
		outbuf.CleanDataBasePipe();
		outbuf.setOrderDB();//set the Db to
		if(this->ParseOrderCommand(command,outbuf))
			return true;
		else
			return false;
	}

	//PAST ORDER
	if(dataBase.CompareNoCase(L"pastOrder"))
	{
		NamedPipe::DataBasePipe outbuf;//Init message
		outbuf.CleanDataBasePipe();
		outbuf.setPastOrderDB();//set the Db to
		if(this->ParsePastOrderCommand(command,outbuf))
		{
			DBMSSyncroS.Write(&outbuf);//send the command to DBMS
			return true;
		}
		else
			return false;
	}

	//SHIFT
	if(dataBase.CompareNoCase(L"shift"))
	{
		NamedPipe::DataBasePipe outbuf;//Init message
		outbuf.CleanDataBasePipe();
		outbuf.setShiftDB();//set the Db to
		if(this->ParseShiftCommand(command,outbuf))
		{
			DBMSSyncroS.Write(&outbuf);//send the command to DBMS
			return true;
		}
		else
			return false;
	}

	//DOWNTIME
	if(dataBase.CompareNoCase(L"downTime"))
	{
		NamedPipe::DataBasePipe outbuf;//Init message
		outbuf.CleanDataBasePipe();
		outbuf.setDownTimeDB();//set the Db to
		if(this->ParseDownTimeCommand(command,outbuf))
		{
			DBMSSyncroS.Write(&outbuf);//send the command to DBMS
			return true;
		}
		else
			return false;
	}

	//CUT TO PATTERN
	if(dataBase.CompareNoCase(L"cutToPattern"))
	{
		NamedPipe::DataBasePipe outbuf;//Init message
		outbuf.CleanDataBasePipe();
		outbuf.setCutToPatternDB();//set the Db to
		if(this->ParseCutToPatternCommand(command,outbuf))
		{
			DBMSSyncroS.Write(&outbuf);//send the command to DBMS
			return true;
		}
		else
			return false;
	}

	//Printings
	if(dataBase.CompareNoCase(L"Printings"))
	{
		NamedPipe::DataBasePipe outbuf;//Init message
		outbuf.CleanDataBasePipe();
		outbuf.setPrintingsDB();//set the Db
		command.extractCommand();
		outbuf.multipleOperation.setUserChar(command);
		DBMSSyncroS.Write(&outbuf);//send the command to DBMS
		return true;
	}

	//pastRolls
	if(dataBase.CompareNoCase(L"pastRolls"))
	{
		NamedPipe::DataBasePipe outbuf;//Init message
		outbuf.CleanDataBasePipe();
		outbuf.setPastRollsDB();//set the Db to
		if(this->ParsePastRollsCommand(command,outbuf))
		{
			DBMSSyncroS.Write(&outbuf);//send the command to DBMS
			return true;
		}
		else
			return false;
	}

	return false;
}

bool C_DBManager::ParsePastRollsCommand(String::StrType theCommand, NamedPipe::DataBasePipe& outbuf)
{
	String::StrType command = theCommand.extractCommand();
	if (command.isEmpty())
		return false;

	 //0 splicerNumber
	 //1 rollQueue
	 //2 rollId
	 //3 paperType
	 //4 width
	 //5 suppliedDiameter
	 //6 coreSize
	 //7 suppliedLength
	 //8 currentLength
	 //9 grammage
	 //10 thickness
	 //11 description
	 //12 dataToPrint
	
	if(command.CompareNoCase(L"UpdateRollData"))
	{
		std::vector<String::StrType> token = theCommand.Tokenize(Network::SEP);	
		if(token.size() != 13)
				return false;

		outbuf.setCommandInsert();
		outbuf.multipleOperation.inPosition = token[0].ToInt();
		outbuf.multipleOperation.userInt = token[1].ToInt();

		//RollId ->	2
		//ShortRollId -> Vuoto
		//PaperType -> 3
		//Width_UDdmm -> 4
		//Diameter_UDdmm -> 5
		//CoreDiameter_UDdmm -> 6
		//Length_UDmm -> 7
		//WeightFactor_UWg_m -> 9
		//Thickness_UDdmmm -> 10
		//RollDataEnteredByOperator -> false
		//Description -> 11
		//DataToPrint -> 12

		outbuf.data.pastRollsData.RSMInitRoll(
			token[2],
			L"",
			token[3],
			token[4].ToInt(),
			token[5].ToInt(),
			token[6].ToInt(),
			token[7].ToInt(),
			token[9].ToInt(),
			token[10].ToInt(),
			true,
			token[11],
			token[12]
			);

			return true;
	}

	return false;
}

DWORD C_DBManager::CreateExitCommand(NamedPipe::DataBasePipe& inbuf)
{
	logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"DataBase Set Version");

	wchar_t timeStr[MAX_PATH];

	//N.B. the System Time is the GMT orario
	String::SYSTEMTIME2Alpha(inbuf.data.DBMSDBVer.DbVer.setupVer,timeStr,MAX_PATH);
	o_socket.Send(Network::MSG_DB_REF,L"order.SetVer(%s)", timeStr);

	//N.B. the System Time is the GMT orario
	String::SYSTEMTIME2Alpha(inbuf.data.DBMSDBVer.DbVer.fluteVer,timeStr,MAX_PATH);
	o_socket.Send(Network::MSG_DB_REF,L"flute.SetVer(%s)", timeStr);

	//N.B. the System Time is the GMT orario
	String::SYSTEMTIME2Alpha(inbuf.data.DBMSDBVer.DbVer.paperVer,timeStr,MAX_PATH);
	o_socket.Send(Network::MSG_DB_REF,L"paper.SetVer(%s)", timeStr);

	//N.B. the System Time is the GMT orario
	String::SYSTEMTIME2Alpha(inbuf.data.DBMSDBVer.DbVer.qualityVer,timeStr,MAX_PATH);
	o_socket.Send(Network::MSG_DB_REF,L"quality.SetVer(%s)", timeStr);

	//N.B. the System Time is the GMT orario
	String::SYSTEMTIME2Alpha(inbuf.data.DBMSDBVer.DbVer.pastOrderVer,timeStr,MAX_PATH);
	o_socket.Send(Network::MSG_DB_REF,L"pastOrder.SetVer(%s)", timeStr);

	//N.B. the System Time is the GMT orario
	String::SYSTEMTIME2Alpha(inbuf.data.DBMSDBVer.DbVer.shiftVer,timeStr,MAX_PATH);
	o_socket.Send(Network::MSG_DB_REF,L"shift.SetVer(%s)", timeStr);

	//N.B. the System Time is the GMT orario
	String::SYSTEMTIME2Alpha(inbuf.data.DBMSDBVer.DbVer.downTimeVer,timeStr,MAX_PATH);
	o_socket.Send(Network::MSG_DB_REF,L"downTime.SetVer(%s)", timeStr);

	//N.B. the System Time is the GMT orario
	String::SYSTEMTIME2Alpha(inbuf.data.DBMSDBVer.DbVer.cutToPatternVer,timeStr,MAX_PATH);
	o_socket.Send(Network::MSG_DB_REF,L"cutToPattern.SetVer(%s)", timeStr);

	//N.B. the System Time is the GMT orario
	String::SYSTEMTIME2Alpha(inbuf.data.DBMSDBVer.DbVer.pastRollsVer,timeStr,MAX_PATH);
	o_socket.Send(Network::MSG_DB_REF,L"pastRolls.SetVer(%s)", timeStr);

	//TRUE if the client want save Report
	if(inbuf.multipleOperation.userBool)	
	{
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"Save Report");
		o_socket.Send(Network::MSG_DB_SWI,L"SaveReport()");
	}

	return ERROR_SUCCESS;
}

DWORD C_DBManager::CreateDBVerCommand(NamedPipe::DataBasePipe& in)
{
	logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"DataBase Load");

	if(in.data.DBMSDBVer.sameVer.setupVer)
		o_socket.Send(Network::MSG_DB_REF,L"order.Load()");

	if(in.data.DBMSDBVer.sameVer.fluteVer)
		o_socket.Send(Network::MSG_DB_REF,L"flute.Load()");

	if(in.data.DBMSDBVer.sameVer.paperVer)
		o_socket.Send(Network::MSG_DB_REF,L"paper.Load()");

	if(in.data.DBMSDBVer.sameVer.qualityVer)
		o_socket.Send(Network::MSG_DB_REF,L"quality.Load()");

	if(in.data.DBMSDBVer.sameVer.pastOrderVer)
		o_socket.Send(Network::MSG_DB_REF,L"pastOrder.Load()");

	if(in.data.DBMSDBVer.sameVer.shiftVer)
		o_socket.Send(Network::MSG_DB_REF,L"shift.Load()");

	if(in.data.DBMSDBVer.sameVer.downTimeVer)
		o_socket.Send(Network::MSG_DB_REF,L"downTime.Load()");

	if(in.data.DBMSDBVer.sameVer.cutToPatternVer)
		o_socket.Send(Network::MSG_DB_REF,L"cutToPattern.Load()");

	if(in.data.DBMSDBVer.sameVer.pastRollsVer)
		o_socket.Send(Network::MSG_DB_REF,L"pastRolls.Load()");
	
	return ERROR_SUCCESS;
}

DWORD C_DBManager::CreateProgramsCommands(NamedPipe::DataBasePipe& in)
{
	String::StrType init;
	DWORD exec=ERROR_SUCCESS;

	switch (in.command)
	{
	//Insert or Mod command
	case NamedPipe::DataBasePipe::Insert:
	case NamedPipe::DataBasePipe::Modify:
		init = in.isCommandInsert() ? L"WETAdd" : L"WETMod";
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"PROGRAM Command: %s programCode %s, programID %d",init.getPtr(), in.data.programData.programKey.getProgramCode(), in.data.programData.programKey.getProgramId());

		wchar_t dryIDs[MAX_SETUP_PER_PROGRAM*5];
		::ZeroMemory(dryIDs, sizeof(wchar_t) * (MAX_SETUP_PER_PROGRAM*5));
		in.data.programData.getDryIDs(dryIDs);
		exec=o_socket.Send(Network::MSG_DB_REF,
			L"order.%s(%s%s%d%s%d%s%s%s%d%s%d%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s)",
							//  0 S 1 S 2 S 3 S 4 S 5 s 6 s 7 s 8 s 9 s10 s11 s12 s13 s14 s15 s16 s17 s18 s19 s20 s21
			init.getPtr(),
			//Program Code
			in.data.programData.programKey.getProgramCode(),Network::SEP,//0
			in.data.programData.programKey.getProgramId(),Network::SEP,//1
			in.data.programData.programKey.getProgramModId(),Network::SEP,//2
			dryIDs,Network::SEP,//3

			//Header
			in.data.programData.getPaperWidth_UDdmm(),Network::SEP,//4
			in.data.programData.getOrderLength(),Network::SEP,//5
			in.data.programData.getQuality().getQualityID(),Network::SEP,//6
			in.data.programData.getQuality().qualityFluteData.getSFSPosition(),Network::SEP,//7
			in.data.programData.getNote(), Network::SEP,//8
			L" ", Network::SEP,//9
			L" ", Network::SEP,//10
			L" ", Network::SEP,//11
			L" ", Network::SEP,//12
			L" ", Network::SEP,//13
			L" ", Network::SEP,//14
			L" ", Network::SEP,//15
			L" ", Network::SEP,//16
			L" ", Network::SEP,//17
			L" ", Network::SEP,//18
			L" ", Network::SEP,//19
			L" ", Network::SEP,//20
			L" "//21
			);
		
		break;

	//delete all record command
	case NamedPipe::DataBasePipe::Clear:
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"PROGRAM Command: CLEAR");
		exec=o_socket.Send(Network::MSG_DB_REF,L"order.WETClr()");
		break;

	//Shift command
	case NamedPipe::DataBasePipe::Shift:
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"PROGRAM Command: SHIFT ProgramId %d",in.data.programData.programKey.getProgramId());
		exec=o_socket.Send(Network::MSG_DB_REF,L"order.WETShift(%d)",
																										 // 0 
			in.data.programData.programKey.getProgramId()// 0
			);
		break;

	default:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"PROGRAM Command Not Found %d", in.command);
		return ERROR_SUCCESS;	break;

	}//switch

	if(in.isToView())
		o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s])",in.getResponse());
		
	return exec;
}

DWORD C_DBManager::CreateOrdersCommands(NamedPipe::DataBasePipe& in)
{
	wchar_t init[MAX_PATH]={L'\0'};
	DWORD exec=ERROR_SUCCESS;

	switch (in.command)
	{
	//delete all record command
	case NamedPipe::DataBasePipe::Clear:
		exec=o_socket.Send(Network::MSG_DB_REF,L"order.Clr()");
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"ORDER Command: CLEAR");

		if(in.data.setupData.key.isNotUsed())
      break;
		else
			in.command = NamedPipe::DataBasePipe::Insert;

	//Insert or Mod command
	case NamedPipe::DataBasePipe::Insert:
	case NamedPipe::DataBasePipe::Modify:
	case NamedPipe::DataBasePipe::OverrideCurrentSetup:
	case NamedPipe::DataBasePipe::InsertAfterSetup:
		{
		wchar_t Delivery[MAX_PATH];
		if(in.isCommandInsert())
      swprintf(init,L"Add(");
		if(in.isCommandModify())
			swprintf(init,L"Mod(");
		if(in.isCommandOverrideCurrentSetup())
			swprintf(init,L"OverrideCurrentSetup(");
		if(in.isCommandInsertAfterSetup())
      swprintf(init,L"InsertInPosition(%d%s", in.multipleOperation.key[1] , Network::SEP);

			wchar_t ord[10000]={L'\0'};
			int grp=in.data.setupData.assignedGroup;
      //PMASSARO_FI16/08/11 - SyncroS: Aggiunta sicurezza in caso di aggiornamento DB ordini verso il Client
      if ((grp<0) || (grp>=NUM_GROUPS))
        grp = 0;

			int cnt=0;
			cnt+=swprintf(ord+cnt, L"order.%s", init);
			//Unique Internal Code
			cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.key.getSetupId(), Network::SEP);//1
			cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.key.getModId(), Network::SEP);//2
			//Header
			cnt+=swprintf(ord+cnt, L"%s%s", in.data.setupData.setupCode, Network::SEP);//3
			cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.webWidth_UDdmm, Network::SEP);//4
			cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.trimType, Network::SEP);//5
			cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.setupData.key.getValid()), Network::SEP);//6
			cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.scoreGap_UDdmm, Network::SEP);//7
			cnt+=swprintf(ord+cnt, L"%s%s", in.data.setupData.quality.getQualityID(), Network::SEP);//8
			cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.validation.trim_UDdmm, Network::SEP);//9
			cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.calculations[grp].slitsTandem, Network::SEP);//10
			cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.calculations[grp].scoresTandem, Network::SEP);//11
			cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.calculations[grp].shaftMask, Network::SEP);//12
			//RFazzi_FI01/06/2007_1.Added some modification for NoGapContinuousTrim
			cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.setupData.aocType.getNoGap() || in.data.setupData.aocType.getNoGapCTrimOneLevel()), Network::SEP);//13
			cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.setupData.aocType.getTandem()), Network::SEP);//14
			cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.setupData.aocType.getKnifeOnwards()), Network::SEP);//15
			cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.setupData.calculations[grp].StackerValidCancelAOC[0]), Network::SEP);//16
			cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.setupData.calculations[grp].StackerValidCancelAOC[1]), Network::SEP);//17
			cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.setupData.calculations[grp].StackerValidCancelAOC[2]), Network::SEP);//18			
			cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.setupData.order[0].knife.cutToPattern.isTypeCutToMark()), Network::SEP);//19
			cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.setupData.order[1].knife.cutToPattern.isTypeCutToMark()), Network::SEP);//20
			cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.setupData.order[2].knife.cutToPattern.isTypeCutToMark()), Network::SEP);//21

			for (int n=0; n<NUM_ORDERS_PER_SETUP; n++)
			{
				//Order n
				cnt+=swprintf(ord+cnt, L"%s%s", in.data.setupData.order[n].getOrderNumber(), Network::SEP);         //22, 38 and 54
				String::SYSTEMTIME2Alpha(in.data.setupData.order[n].getDelivery(), Delivery, MAX_PATH);
				cnt+=swprintf(ord+cnt, L"%s%s", Delivery, Network::SEP);                                            //23, 39 and 55
				cnt+=swprintf(ord+cnt, L"%s%s", in.data.setupData.order[n].customer.getName(), Network::SEP);       //24, 40 and 56
				cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].twin.sheetWidth_UDdmm, Network::SEP);    //25, 41 and 57
				cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].twin.outs, Network::SEP);                //26, 42 and 58
				cnt+=swprintf(ord+cnt, L"%s%s", in.data.setupData.order[n].twin.scoreMeasures_UDdmm, Network::SEP); //27, 43 and 59
				cnt+=swprintf(ord+cnt, L"%c%s", in.data.setupData.order[n].twin.getScoreType(), Network::SEP);      //28, 44 and 60
				cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].twin.positType, Network::SEP);           //29, 45 and 61
				cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].twin.teletwinOffset_UDdmm, Network::SEP);//30, 46 and 62
				cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].twin.scoreGap_UDdmm, Network::SEP);      //31, 47 and 63
				cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].knife.levelName, Network::SEP);          //32, 48 and 64
				cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].knife.sheetLength_UDdmm, Network::SEP);  //33, 49 and 65
				cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].knife.segmentSheetCount, Network::SEP);  //34, 50 and 66
				cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].stacker.sheetsPerStack, Network::SEP);   //35, 51 and 67
				cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].materialHandling.stacksPerPallet, Network::SEP);//36, 52 and 68
				cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].materialHandling.sending, Network::SEP); //37, 53 and 69
			}
      //agrassi_FI04-02-09
      wchar_t plungedMasks[20], separationMasks[20], sltPositions[160];
	  ::ZeroMemory(plungedMasks, sizeof(wchar_t)*20);
	  ::ZeroMemory(separationMasks, sizeof(wchar_t)*20);
	  ::ZeroMemory(sltPositions, sizeof(wchar_t)*160);
      int cntPl=0, cntSep=0, cntPos=0;
			for (int sh=0; sh<NUM_SLIT_SHAFTS; sh++)
			{
				int grpSh=F_GrpShaft(grp, (Shaft)sh);
        //int plungedSlitMask=0, dummySlitMask=0, separationSlitMask=0;
				int plungedSlitMask=0, separationSlitMask=0;
				for (int slt=0; slt<MAX_SLITS_PER_SHAFT; slt++)
				{
					if (slt<in.data.setupData.calculations[grp].slitShafts[grpSh].number)
					{
						if (in.data.setupData.calculations[grp].slitShafts[grpSh].slitsReq[slt].plunged)
							plungedSlitMask|=BIT(in.data.setupData.calculations[grp].slitShafts[grpSh].usedSlits[slt]);
            //agrassi_FI04-02-09
						//if (in.data.setupData.calculations[grp].slitShafts[grpSh].slitsReq[slt].dummy)
						//	dummySlitMask|=BIT(in.data.setupData.calculations[grp].slitShafts[grpSh].usedSlits[slt]);
						if (in.data.setupData.calculations[grp].slitShafts[grpSh].slitsReq[slt].sep)
							separationSlitMask|=BIT(in.data.setupData.calculations[grp].slitShafts[grpSh].usedSlits[slt]);
					}
				}
        //agrassi_FI04-02-09
        //cnt+=swprintf(ord+cnt, L"%d%s", plungedSlitMask, Network::SEP);
        cntPl+=swprintf(plungedMasks+cntPl, L"%d%s", plungedSlitMask, ((sh==NUM_SLIT_SHAFTS-1)? L"" : L","));
        //cnt+=swprintf(ord+cnt, L"%d%s", dummySlitMask, Network::SEP);
				//cnt+=swprintf(ord+cnt, L"%d%s", separationSlitMask, Network::SEP);
				cntSep+=swprintf(separationMasks+cntSep, L"%d%s", separationSlitMask, ((sh==NUM_SLIT_SHAFTS-1)? L"": L","));
				for (int slt=0; slt<MAX_SLITS_PER_SHAFT; slt++)
					//cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.calculations[grp].slitShafts[grpSh].slitsPositions_UDdmm[slt], Network::SEP);
					cntPos+=swprintf(sltPositions+cntPos, L"%d%s", in.data.setupData.calculations[grp].slitShafts[grpSh].slitsPositions_UDdmm[slt], 
                                                ((slt==MAX_SLITS_PER_SHAFT-1)?((sh==NUM_SLIT_SHAFTS-1)? L"" : L";"): L","));
			}
      //agrassi_FI04-02-09
			cnt+=swprintf(ord+cnt, L"%s%s", plungedMasks, Network::SEP);//70
			cnt+=swprintf(ord+cnt, L"%s%s", separationMasks, Network::SEP);//71
			cnt+=swprintf(ord+cnt, L"%s%s", sltPositions, Network::SEP);//72

			//agrassi_FI04-02-09
      wchar_t startScorers[20], scroreCounts[20], supportScorers[20], scrPositions[650];
	  ::ZeroMemory(startScorers,sizeof(wchar_t)*20);
	  ::ZeroMemory(scroreCounts,sizeof(wchar_t)*20);
	  ::ZeroMemory(supportScorers,sizeof(wchar_t)*20);
	  ::ZeroMemory(scrPositions,sizeof(wchar_t)*650);
      int cntSt=0, cntCnt=0, cntSupp=0, cntPs=0;
      for (int sh=0; sh<MAX_NUM_SCORE_SHAFTS; sh++)
			{
				int grpSh=F_GrpShaft(grp, (Shaft)(sh+Score1));
				int startScorer=in.data.setupData.calculations[grp].scoreShafts[grpSh].startScorer;
				int scoreCount=in.data.setupData.calculations[grp].scoreShafts[grpSh].number;
				//cnt+=swprintf(ord+cnt, L"%d%s", startScorer, Network::SEP);
				//cnt+=swprintf(ord+cnt, L"%d%s", scoreCount, Network::SEP);
				//cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.setupData.calculations[grp].scoreShafts[grpSh].scoresReq[0].support), Network::SEP);
				cntSt+=swprintf(startScorers+cntSt, L"%d%s", startScorer, ((sh==MAX_NUM_SCORE_SHAFTS-1)? L"": L","));
				cntCnt+=swprintf(scroreCounts+cntCnt, L"%d%s", scoreCount, ((sh==MAX_NUM_SCORE_SHAFTS-1)? L"": L","));
				cntSupp+=swprintf(supportScorers+cntSupp, L"%s%s", String::Bool2Alpha(in.data.setupData.calculations[grp].scoreShafts[grpSh].scoresReq[0].support), ((sh==MAX_NUM_SCORE_SHAFTS-1)? L"": L","));
				for (int scr=0; scr<MAX_SCORES_PER_SHAFT; scr++)
					//cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.calculations[grp].scoreShafts[grpSh].scoresPositions_UDdmm[scr], Network::SEP);
					cntPs+=swprintf(scrPositions+cntPs, L"%d%s", in.data.setupData.calculations[grp].scoreShafts[grpSh].scoresPositions_UDdmm[scr], 
                                                ((scr==MAX_SCORES_PER_SHAFT-1)?((sh==MAX_NUM_SCORE_SHAFTS-1)? L"": L";"): L","));
        //agrassi_FI04-02-09
				//for (int scr=0; scr<MAX_SCORES_PER_SHAFT; scr++)
				//	cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.calculations[grp].scoreShafts[grpSh].upperRelPositions_UDdmm[scr], Network::SEP);
				//for (int scr=0; scr<MAX_SCORES_PER_SHAFT; scr++)
				//	cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.calculations[grp].scoreShafts[grpSh].leftScoresPositions_UDdmm[scr], Network::SEP);
				//for (int scr=0; scr<MAX_SCORES_PER_SHAFT; scr++)
				//{
				//	PositType p=NoPositType;
				//	if ((scr>=startScorer) && (scr<startScorer+scoreCount))
				//		p=in.data.setupData.calculations[grp].scoreShafts[grpSh].scoresReq[scr-startScorer].positType;
				//	cnt+=swprintf(ord+cnt, L"%d%s", p, Network::SEP);
				//}
				//for (int scr=0; scr<MAX_SCORES_PER_SHAFT; scr++)
				//{
				//	int scoreGap=0;
				//	if ((scr>=startScorer) && (scr<startScorer+scoreCount))
				//		scoreGap=in.data.setupData.calculations[grp].scoreShafts[grpSh].scoresReq[scr-startScorer].scoreGap_UDdmm;
				//	cnt+=swprintf(ord+cnt, L"%d%s", scoreGap, Network::SEP);
				//}
				//for (int scr=0; scr<MAX_SCORES_PER_SHAFT; scr++)
				//{
				//	int shaftGap=0;
				//	if ((scr>=startScorer) && (scr<startScorer+scoreCount))
				//		shaftGap=in.data.setupData.calculations[grp].scoreShafts[grpSh].scoresReq[scr-startScorer].shaftGap_UDdmm;
				//	cnt+=swprintf(ord+cnt, L"%d%s", shaftGap, Network::SEP);
				//}
				//for (int scr=0; scr<MAX_SCORES_PER_SHAFT; scr++)
				//{
				//	int measureIndex=-1;
				//	if ((scr>=startScorer) && (scr<startScorer+scoreCount) && (in.data.setupData.validation.numScoresReq>0))
				//	{
				//		int scrIdx;
				//		for (scrIdx=0; scrIdx<in.data.setupData.validation.numScoresReq; scrIdx++)
				//			if (in.data.setupData.validation.scores[scrIdx].scoringPointPos_UDdmm==in.data.setupData.calculations[grp].scoreShafts[grpSh].scoresPositions_UDdmm[scr])
				//				break;
				//		if (scrIdx>=in.data.setupData.validation.numScoresReq) scrIdx=0;
				//		measureIndex=scrIdx;
				//	}
				//	cnt+=swprintf(ord+cnt, L"%d%s", measureIndex, Network::SEP);
				//}
			}
      //agrassi_FI04-02-09
			cnt+=swprintf(ord+cnt, L"%s%s", startScorers, Network::SEP);//73
			cnt+=swprintf(ord+cnt, L"%s%s", scroreCounts, Network::SEP);//74
			cnt+=swprintf(ord+cnt, L"%s%s", supportScorers, Network::SEP);//75
			cnt+=swprintf(ord+cnt, L"%s%s", scrPositions, Network::SEP);//76
			//TODO 2-level WebDir Mask
			cnt+=swprintf(ord+cnt, L"%d%s", 0, Network::SEP);//77
			//TODO 3-level WebDir Mask
			cnt+=swprintf(ord+cnt, L"%d%s", 0, Network::SEP);//78
			//TODO RCS sector Mask
			cnt+=swprintf(ord+cnt, L"%d%s", 0, Network::SEP);//79

      //agrassi_FI04-02-09
      int cntTT=0;
      wchar_t teatTapes[60];
	  ::ZeroMemory(teatTapes, sizeof(wchar_t)*60);
			for (int tt=0; tt<MAX_SLITS_IN_SETUP; tt++)
				//cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.validation.tearTapePositions_UDdmm[tt], Network::SEP);
				cntTT+=swprintf(teatTapes+cntTT, L"%d%s", in.data.setupData.validation.tearTapePositions_UDdmm[tt], ((tt==MAX_SLITS_IN_SETUP-1)? L"": L","));

		  cnt+=swprintf(ord+cnt, L"%s%s", teatTapes, Network::SEP);//80
			cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.validation.numTearTapes, Network::SEP);//81

      for (int n=0; n<NUM_ORDERS_PER_SETUP; n++)
			{
				//Order n
				cnt+=swprintf(ord+cnt, L"%s%s", in.data.setupData.order[n].knife.cutToPattern.getCutToPatternID(), Network::SEP);             //82, 101, 120
        cnt+=swprintf(ord+cnt, L"%s%s", in.data.setupData.order[n].toPrint.getDataToPrint(1), Network::SEP);                          //83, 102, 121
        cnt+=swprintf(ord+cnt, L"%s%s", in.data.setupData.order[n].toPrint.getDataToPrint(2), Network::SEP);                          //84, 103, 122
        cnt+=swprintf(ord+cnt, L"%s%s", in.data.setupData.order[n].toPrint.getDataToPrint(3), Network::SEP);                          //85, 104, 123
        cnt+=swprintf(ord+cnt, L"%s%s", in.data.setupData.order[n].toPrint.getDataToPrint(4), Network::SEP);                          //86, 105, 124
        cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.setupData.order[n].stacker.getContinuousStack()), Network::SEP);   //87, 106, 125
        cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.setupData.order[n].materialHandling.pallet.getOnPlatform()), Network::SEP);//88, 107, 126
        cnt+=swprintf(ord+cnt, L"%s%s", in.data.setupData.order[n].materialHandling.pallet.getNote(), Network::SEP);                  //89, 108, 127
        cnt+=swprintf(ord+cnt, L"%s%s", in.data.setupData.order[n].customer.getDestinationAddress(), Network::SEP);                   //90, 109, 128
        cnt+=swprintf(ord+cnt, L"%s%s", in.data.setupData.order[n].materialHandling.getBoxMachineCode(), Network::SEP);               //91, 110, 129
        cnt+=swprintf(ord+cnt, L"%s%s", in.data.setupData.order[n].materialHandling.getBoxMachineName(), Network::SEP);               //92, 111, 130
        cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].toPrint.getNumberOfPalletLayout(), Network::SEP);                  //93, 112, 131
        cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].toPrint.getAmountOfPalletCopy(), Network::SEP);                    //94, 113, 132
        cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].toPrint.getNumberOfPastOrderLayout(), Network::SEP);               //95, 114, 133
        cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].toPrint.getAmountOfPastOrderCopy(), Network::SEP);                 //96, 115, 134
				cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.setupData.order[n].knife.getPrePrintPaper()), Network::SEP);       //97, 116, 135
        //agrassi_FI24-02-2010_3
				cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].stacker.getDischargeSide(), Network::SEP);                         //98, 117, 136
				cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.order[n].total.balance, Network::SEP);                                      //99, 118, 137
        //agrassi_FI02-09-2010_1
				cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.setupData.order[n].stacker.bundle), Network::SEP);                 //100, 119, 138
        
      }
      //agrassi_FI26-05-08_1
      //agrassi_FI05-10-2010_1
			//cnt+=swprintf(ord+cnt, L"%d", in.data.setupData.key.getOfficeId());
			cnt+=swprintf(ord+cnt, L"%d%s", in.data.setupData.key.getOfficeId(), Network::SEP);         //139
			cnt+=swprintf(ord+cnt, L"%s", in.data.setupData.quality.qualityFluteData.getFluteId());         //140
			//cnt+=swprintf(ord+cnt, L"%s)", L" ");
			cnt+=swprintf(ord+cnt, L")");
      //agrassi_FI03-07-09_1
      wchar_t* ordOK=new wchar_t[10500];
      int iOK=0;
      for (size_t i=0; i<=wcslen(ord); i++)
      {
        ordOK[iOK++]=ord[i];
        if (ord[i]==L'%')
          ordOK[iOK++]=L'%';
      }
			//exec=o_socket.Send(Network::MSG_DB_REF, ord);
			exec=o_socket.Send(Network::MSG_DB_REF, ordOK);
      delete ordOK;
		}

		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"ORDER Command: %s) setupId: %d setupCode: %s",init, in.data.setupData.key.getSetupId(), in.data.setupData.setupCode);
		break;

	//delete multiple record command
	case NamedPipe::DataBasePipe::Del:
		{
			wchar_t str[5*MAX_QUEUE];
			::ZeroMemory(str,sizeof(wchar_t)*5*MAX_QUEUE);
			in.multipleOperation.getKeyStrWithSEP(str,Network::SEP);
			exec=o_socket.Send(Network::MSG_DB_REF, L"order.Del(%s)",str);
			in.multipleOperation.getKeyStrForLog(str);
			logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"ORDER Command: DEL MULTIPLE %s",str);
			break;
		}

	//move record command
	case NamedPipe::DataBasePipe::Move:
		{
			wchar_t str[5*MAX_QUEUE];
			::ZeroMemory(str,sizeof(wchar_t)*5*MAX_QUEUE);
			in.multipleOperation.getKeyStrWithSEP(str,Network::SEP);
			exec=o_socket.Send(Network::MSG_DB_REF, L"order.Move(%d%s%s)",in.multipleOperation.getInPosition(),Network::SEP,str);
			in.multipleOperation.getKeyStrForLog(str);
			logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"ORDER Command: MOVE %s in pos %d",str,in.multipleOperation.getInPosition());	
			break;
		}

	//shift command
	case NamedPipe::DataBasePipe::Shift:
		exec=o_socket.Send(Network::MSG_DB_REF,L"order.Shift(%d)",in.data.setupData.key.getSetupId());
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"ORDER Command: SHIFT setupId: %d",in.data.setupData.key.getSetupId());
		break;

	//Error Command
	case NamedPipe::DataBasePipe::Error:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"ORDER Command: ERROR Error: %s",in.getResponse());
		break;

	//Detail command
	case NamedPipe::DataBasePipe::DetailWithId:
	{
		exec=o_socket.Send(Network::MSG_DB_SWI,
			L"DetailRecord(orders,DryEndList,%d,%s,%s,%d,%s,%s,%d,%d,%d,%d,%s,%s,%d,%d,%d,%d,%s,%s,%d,%d,%d,%d)",
										//    0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19 20 21
			//Unique Internal Code
			in.data.setupData.key.getSetupId(),//0
			//Header
			in.data.setupData.setupCode,//1
			in.data.setupData.quality.qualityFluteData.getFluteId(),//2
			in.data.setupData.trimType,//3
			//Order 0
			in.data.setupData.order[0].getOrderNumber(),//4
			in.data.setupData.order[0].customer.getName(),//5
			in.data.setupData.order[0].twin.sheetWidth_UDdmm,//6
			in.data.setupData.order[0].knife.sheetLength_UDdmm,//7
			in.data.setupData.order[0].twin.outs,//8
			in.data.setupData.order[0].knife.knifeLevel,//9
			//Order 1
			in.data.setupData.order[1].getOrderNumber(),//10
			in.data.setupData.order[1].customer.getName(),//11
			in.data.setupData.order[1].twin.sheetWidth_UDdmm,//12
			in.data.setupData.order[1].knife.sheetLength_UDdmm,//13
			in.data.setupData.order[1].twin.outs,//14
			in.data.setupData.order[1].knife.knifeLevel,//15
			//Order 2
			in.data.setupData.order[2].getOrderNumber(),//16
			in.data.setupData.order[2].customer.getName(),//17
			in.data.setupData.order[2].twin.sheetWidth_UDdmm,//18
			in.data.setupData.order[2].knife.sheetLength_UDdmm,//19
			in.data.setupData.order[2].twin.outs,//20
			in.data.setupData.order[2].knife.knifeLevel//21
			);				
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"ORDER Command: DETAIL setupId: %d setupCode: %s",in.data.setupData.key.getSetupId(), in.data.setupData.setupCode);
		break;
	}
	default:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"ORDER Command Not Found %d", in.command);
		return ERROR_SUCCESS;	break;

	}//switch

	if(in.isToView())
	{
		logger.Info(__FUNCTION__,Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"SetMessage %s",in.getResponse());
		o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s])",in.getResponse());
	}

	return exec;
}

DWORD C_DBManager::CreateQualityCommands(NamedPipe::DataBasePipe& in)
{
	String::StrType init;
	DWORD exec=ERROR_SUCCESS;

	switch (in.command)
	{
	//Insert or Mod command
	case NamedPipe::DataBasePipe::Insert:
	case NamedPipe::DataBasePipe::Modify:
		init = in.isCommandInsert() ? L"Add" : L"Mod";
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"QUALITY Command: %s QualityCode: %s",init.getPtr(),in.data.qualityData.getQualityID());			

		exec=o_socket.Send(Network::MSG_DB_REF,
			//RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
			//L"quality.%s(%s%s%s%s%s%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%s%s%d%s%d%s%s%s%s%s%d%s%s%s%d%s%s%s%d%s%s%s%d%s%s%s%d%s%s%s%d%s%s%s%d%s%d%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s)",
			//				//    0 S 1 S 2 S 3 S 4 S 5 S 6 S 7 S 8 S 9 S10 S11 S12 S13 S14 S15 S16 S17 S18 S19 S20 S21 S22 S23 S24 S25 S26 S27 S28 S29 S30 S31 S32 S33 S34 S35 S36 S37 S38 S39 S40 S41 S42 S43 S44 
			//L"quality.%s(%s%s%s%s%s%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%s%s%d%s%d%s%s%s%s%s%d%s%s%s%d%s%s%s%d%s%s%s%d%s%s%s%d%s%s%s%d%s%s%s%d%s%d%s%s%s%s%s%d%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s)",
			//				//    0 S 1 S 2 S 3 S 4 S 5 S 6 S 7 S 8 S 9 S10 S11 S12 S13 S14 S15 S16 S17 S18 S19 S20 S21 S22 S23 S24 S25 S26 S27 S28 S29 S30 S31 S32 S33 S34 S35 S36 S37 S38 S39 S40 S41 S42 S43 S44 
      //agrassi_FI27-08-09_1
			L"quality.%s(%s%s%s%s%s%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%s%s%d%s%s%s%s%s%s%s%d%s%s%s%d%s%s%s%d%s%s%s%d%s%s%s%d%s%s%s%d%s%s%s%d%s%d%s%s%s%s%s%d%s%d%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s)",
							//    0 S 1 S 2 S 3 S 4 S 5 S 6 S 7 S 8 S 9 S10 S11 S12 S13 S14 S15 S16 S17 S18 S19 S20 S21 S22 S23 S24 S25 S26 S27 S28 S29 S30 S31 S32 S33 S34 S35 S36 S37 S38 S39 S40 S41 S42 S43 S44 

			init.getPtr(),
			in.data.qualityData.getQualityID(), Network::SEP, //0 QualityCode
			in.data.qualityData.getQualityText(), Network::SEP, //1 QualityText
			in.data.qualityData.qualityFluteData.getFluteId(), Network::SEP, //2 qualityFluteData.fluteId
			in.data.qualityData.qualityFluteData.getScoreGap(), Network::SEP, //3 qualityFluteData.ScoreGap_UDdmm
			in.data.qualityData.qualityFluteData.getGrindingParameter(), Network::SEP, //4 qualityFluteData.GrindingParameter_UDm
			in.data.qualityData.qualityFluteData.getLubricationParameter(), Network::SEP, //5 qualityFluteData.LubricationParameter_UDm
			in.data.qualityData.qualityFluteData.getTrimGrindingsMultiplier(), Network::SEP, //6 qualityFluteData.TrimGrindingsMultiplier
			in.data.qualityData.qualityFluteData.getGrindingDistance(), Network::SEP, //7 qualityFluteData.GrindingTime_UDm
			in.data.qualityData.qualityFluteData.getLubricationTime(), Network::SEP, //8 qualityFluteData.LubricationTime_UTs
			in.data.qualityData.qualityFluteData.getProductionSpeed_UVm_m(), Network::SEP, //9 qualityFluteData.ProductionSpeed_UVm_m
			in.data.qualityData.qualityFluteData.getAOCSpeed_UVm_m(), Network::SEP, //10 qualityFluteData.AOCSpeed_UVm_m
			in.data.qualityData.qualityFluteData.getThickness_UDdmm(), Network::SEP, //11 qualityFluteData.FluteThickness_UDdmm
			L"SPARE", Network::SEP, //12 SPARE (ex Sheets per Stack)
			in.data.qualityData.getBundleLayersPerStack(), Network::SEP, //13 BundleLayersPerStack
			L"SPARE", Network::SEP, //14 SPARE (ex Sheets per Stack)
			String::Bool2Alpha(in.data.qualityData.getBundleReverseMode()), Network::SEP, //15 BundleReverseMode
      //agrassi_FI07-09-09_1
			//in.data.qualityData.paper1.getPaperID(), Network::SEP, //16 PaperID
      (wcslen(in.data.qualityData.paper1.getPaperID())!=0 ? in.data.qualityData.paper1.getPaperID() : L" "), Network::SEP, //16 PaperID 1
			in.data.qualityData.paper1.getWeightFactor_UWg_m(), Network::SEP, //17 WeightFactor_UWg_m
			//in.data.qualityData.paper2.getPaperID(), Network::SEP, //18 PaperID
      (wcslen(in.data.qualityData.paper2.getPaperID())!=0 ? in.data.qualityData.paper2.getPaperID() : L" "), Network::SEP, //18 PaperID 2
      in.data.qualityData.paper2.getWeightFactor_UWg_m(), Network::SEP, //19 WeightFactor_UWg_m
			//in.data.qualityData.paper3.getPaperID(), Network::SEP, //20 PaperID
      (wcslen(in.data.qualityData.paper3.getPaperID())!=0 ? in.data.qualityData.paper3.getPaperID() : L" "), Network::SEP, //20 PaperID 3
			in.data.qualityData.paper3.getWeightFactor_UWg_m(), Network::SEP, //21 WeightFactor_UWg_m
			//in.data.qualityData.paper4.getPaperID(), Network::SEP, //22 PaperID
      (wcslen(in.data.qualityData.paper4.getPaperID())!=0 ? in.data.qualityData.paper4.getPaperID() : L" "), Network::SEP, //22 PaperID 4
			in.data.qualityData.paper4.getWeightFactor_UWg_m(), Network::SEP, //23 WeightFactor_UWg_m
			//in.data.qualityData.paper5.getPaperID(), Network::SEP, //24 PaperID
      (wcslen(in.data.qualityData.paper5.getPaperID())!=0 ? in.data.qualityData.paper5.getPaperID() : L" "), Network::SEP, //24 PaperID 5
			in.data.qualityData.paper5.getWeightFactor_UWg_m(), Network::SEP, //25 WeightFactor_UWg_m
			//in.data.qualityData.paper6.getPaperID(), Network::SEP, //26 PaperID
      (wcslen(in.data.qualityData.paper6.getPaperID())!=0 ? in.data.qualityData.paper6.getPaperID() : L" "), Network::SEP, //26 PaperID 6
			in.data.qualityData.paper6.getWeightFactor_UWg_m(), Network::SEP, //27 WeightFactor_UWg_m
			//in.data.qualityData.paper7.getPaperID(), Network::SEP, //28 PaperID
      (wcslen(in.data.qualityData.paper7.getPaperID())!=0 ? in.data.qualityData.paper7.getPaperID() : L" "), Network::SEP, //28 PaperID 7
			in.data.qualityData.paper7.getWeightFactor_UWg_m(), Network::SEP, //29 WeightFactor_UWg_m
			in.data.qualityData.getWeightFactorUWg_m(), Network::SEP, //30 WeightFactorUWg_m
			String::Bool2Alpha(in.data.qualityData.getPrePrint()), Network::SEP, //31 PrePrint
			//PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
			//L"SPARE", Network::SEP, //32 Spare
			String::Bool2Alpha(in.data.qualityData.getInclinateLastBelt()), Network::SEP, //32 MoveUpLastBelt
			//RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
			//L"SPARE", Network::SEP, //33 Spare
			in.data.qualityData.qualityFluteData.getTwinFlapDisabled(), Network::SEP, //33 Twin400 InternalFlap Disabled
      //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
			in.data.qualityData.qualityFluteData.getCruisingSpeed_UVm_m(), Network::SEP, //34 CruisingSpeed_UVm_m
			L"SPARE", Network::SEP, //35 Spare
			L"SPARE", Network::SEP, //36 Spare
			L"SPARE", Network::SEP, //37 Spare
			L"SPARE", Network::SEP, //38 Spare
			L"SPARE", Network::SEP, //39 Spare
			L"SPARE", Network::SEP, //40 Spare
			L"SPARE", Network::SEP, //41 Spare
			L"SPARE", Network::SEP, //42 Spare
			L"SPARE", Network::SEP, //43 Spare
			L"SPARE"								//44 Spare
			);

		break;

	//delete one record command
	case NamedPipe::DataBasePipe::Del:
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"QUALITY Command: DEL SINGLE QualityCode: %s",in.data.qualityData.getQualityID());
		exec=o_socket.Send(Network::MSG_DB_REF,L"quality.Del(%s)",in.data.qualityData.getQualityID());
		break;

	//delete all record command
	case NamedPipe::DataBasePipe::Clear:
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"QUALITY Command: CLEAR");
		exec=o_socket.Send(Network::MSG_DB_REF,L"quality.Clr()");
		break;

	//error
	case NamedPipe::DataBasePipe::Error:
    logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"QUALITY Command: ERROR Error : %s",in.getResponse());
		break;

	default:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"QUALITY Command Not Found %d", in.command);
		return ERROR_SUCCESS;	break;
	}//switch

	if(in.isToView())
			o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s])",in.getResponse());

	return exec;
}

DWORD C_DBManager::CreatePaperCommands(NamedPipe::DataBasePipe& in)
{
	String::StrType init;
	DWORD exec=ERROR_SUCCESS;

	switch (in.command)
	{
	//Insert or Mod command
	case NamedPipe::DataBasePipe::Insert:
	case NamedPipe::DataBasePipe::Modify:
		init = in.isCommandInsert() ? L"Add" : L"Mod";
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"PAPER Command: %s PaperType: %s %d",init.getPtr(),in.data.paperData.getPaperID(),in.data.paperData.getWeightFactor_UWg_m());

		exec=o_socket.Send(Network::MSG_DB_REF,
			L"paper.%s(%s%s%d%s%d%s%s%s%d%s%s%s%s%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%s%s%s%s%s)",
					//  0 S 1 S 2 S 3 S 4 S 5 S 6 S 7 S 8 S 9 S10 S11 S12 S13 S14 S15 S16 
			init.getPtr(),
			in.data.paperData.getPaperID(),Network::SEP,//1 paper Type
			in.data.paperData.getCoreDiameter_UDdmm()  ,Network::SEP,//2 CoreDiameterUDdmm
			in.data.paperData.getWeightFactor_UWg_m(),Network::SEP,//3 WeightFactorUWg_m (grammatura)
			in.data.paperData.getText(),Network::SEP,//4 Text
			in.data.paperData.getDefaultThickness_UDdmmm(),Network::SEP,//5 DefaultThickness_UDdmmm
			in.data.paperData.getClassification(),Network::SEP,//6 Classification
			L"SPARE",Network::SEP,//7 SPARE
			in.data.paperData.getMaxSpliceSpeed_UVm_m(0),Network::SEP,//8 MaxSpliceSpeed_UVm_m 1
			in.data.paperData.getMaxSpliceSpeed_UVm_m(1),Network::SEP,//9 MaxSpliceSpeed_UVm_m 2
			in.data.paperData.getMaxSpliceSpeed_UVm_m(2),Network::SEP,//10 MaxSpliceSpeed_UVm_m 3
			in.data.paperData.getMaxSpliceSpeed_UVm_m(3),Network::SEP,//11 MaxSpliceSpeed_UVm_m 4
			in.data.paperData.getMaxSpliceSpeed_UVm_m(4),Network::SEP,//12 MaxSpliceSpeed_UVm_m 5
			in.data.paperData.getMaxSpliceSpeed_UVm_m(5),Network::SEP,//13 MaxSpliceSpeed_UVm_m 6
			in.data.paperData.getMaxSpliceSpeed_UVm_m(6),Network::SEP,//14 MaxSpliceSpeed_UVm_m 7
			L"SPARE",Network::SEP,//15 SPARE
			L"SPARE",Network::SEP,//16 SPARE
			L"SPARE"//17 SPARE
			);
		break;

	//delete one record command
	case NamedPipe::DataBasePipe::Del:
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"PAPER Command: DEL PaperType: %s %d",in.data.paperData.getPaperID(),in.data.paperData.getWeightFactor_UWg_m());
		exec=o_socket.Send(Network::MSG_DB_REF,L"paper.Del(%s%s%d)",
			in.data.paperData.getPaperID(),Network::SEP, // 0 paper Type
			in.data.paperData.getWeightFactor_UWg_m());  // 2 WeightFactorUWg_m (grammatura)
		break;

	//delete all record command
	case NamedPipe::DataBasePipe::Clear:
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"PAPER Command: CLEAR");
		exec=o_socket.Send(Network::MSG_DB_REF,L"paper.Clr()");
		break;

	//Error
	case NamedPipe::DataBasePipe::Error:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"PAPER Command: ERROR Error : %s",in.getResponse());
		break;

	//default
	default:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"PAPER Command Not Found %d", in.command);
		return ERROR_SUCCESS;	break;

	}//Switch

	if(in.isToView())
		o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s])",in.getResponse());
	
	return exec;
}

DWORD C_DBManager::CreatePastOrderCommands(NamedPipe::DataBasePipe& in)
{
	DWORD exec=ERROR_SUCCESS;
	switch (in.command)
	{
	//Insert or Mod command
	case NamedPipe::DataBasePipe::Insert:
    { wchar_t TimeStrStop[MAX_PATH];
			wchar_t TimeStrStart[MAX_PATH];
			wchar_t TimeStrShiftStart[MAX_PATH];
			wchar_t DeliveryChar0[MAX_PATH];
			wchar_t DeliveryChar1[MAX_PATH];
			wchar_t DeliveryChar2[MAX_PATH];
			
			String::SYSTEMTIME2Alpha(in.data.pastOrderData.getFinishTime(),TimeStrStop,MAX_PATH);
			String::SYSTEMTIME2Alpha(in.data.pastOrderData.getStartTime(),TimeStrStart,MAX_PATH);
			String::SYSTEMTIME2Alpha(in.data.pastOrderData.SHIFTgetStartTime(),TimeStrShiftStart,MAX_PATH);
			String::SYSTEMTIME2Alpha(in.data.pastOrderData.setupData.order[0].getDelivery(), DeliveryChar0, MAX_PATH);
			String::SYSTEMTIME2Alpha(in.data.pastOrderData.setupData.order[1].getDelivery(), DeliveryChar1, MAX_PATH);
			String::SYSTEMTIME2Alpha(in.data.pastOrderData.setupData.order[2].getDelivery(), DeliveryChar2, MAX_PATH);

      wchar_t ord[10000]={L'\0'};
      int cnt = 0;

      //PMASSARO_FI16/08/11 - SyncroS: Aggiunta sicurezza in caso di aggiornamento DB ordini verso il Client
      int grp = in.data.pastOrderData.setupData.assignedGroup;
      if ((grp<0) || (grp>=NUM_GROUPS))
        grp = 0;      

      cnt+=swprintf(ord+cnt, 
			L"pastOrder.Add(%d%s%d%s%s%s%d%s%d%s%s%s%d%s%s%s%d%s%d%s%d%s%d%s%s%s%s%s%s%s%s%s%s%s%s%s%d%s%d%s%d%s%s%s%s%s%s%s%s%s%d%s%d%s%s%s%c%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%s%s%s%s%s%s%d%s%d%s%s%s%c%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%s%s%s%s%s%s%d%s%d%s%s%s%c%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%c%s%d%s%s%s%s%s%s%s%s%s%s%s",
										// 0 S 1 S 2 S 3 S 4 S 5 S 6 S 7 S 8 S 9 S10 S11 S12 S13 S14 S15 S16 S17 S18 S19 S20 S21 S30 S31 S32 S33 S34 S35 S36 S37 S38 S39 S40 S41 S42 S43 S44 S45 S46 S47 S48 S50 S51 S52 S53 S54 S55 S56 S57 S58 S59 S60 S61 S62 S63 S64 S65 S66 S67 S68 S70 S71 S72 S73 S74 S75 S76 S77 S78 S79 S80 S81 S82 S83 S84 S85 S86 S87 S88 S90 S91 S92 S93 S94 S95 S96 S97 S98 S99 S00 S01 S02 S03
			//Unique Internal Code
			in.data.pastOrderData.setupData.key.getSetupId(),Network::SEP,//0
			in.data.pastOrderData.setupData.key.getModId(),Network::SEP,//1
			//Header
			in.data.pastOrderData.setupData.setupCode,Network::SEP,//2
			in.data.pastOrderData.setupData.webWidth_UDdmm,Network::SEP,//3
			in.data.pastOrderData.setupData.trimType,Network::SEP,//4
			String::Bool2Alpha(in.data.pastOrderData.setupData.key.getValid()),Network::SEP,//5
			in.data.pastOrderData.setupData.scoreGap_UDdmm,Network::SEP,//6
			in.data.pastOrderData.setupData.quality.getQualityID(),Network::SEP,//7
			in.data.pastOrderData.setupData.validation.trim_UDdmm,Network::SEP,//8

      //PMASSARO_FI16/08/11 - SyncroS: Aggiunta sicurezza in caso di aggiornamento DB ordini verso il Client
      /*
			in.data.pastOrderData.setupData.calculations[in.data.pastOrderData.setupData.assignedGroup].slitsTandem,Network::SEP,//9
			in.data.pastOrderData.setupData.calculations[in.data.pastOrderData.setupData.assignedGroup].scoresTandem,Network::SEP,//10
			in.data.pastOrderData.setupData.calculations[in.data.pastOrderData.setupData.assignedGroup].shaftMask,Network::SEP,//11
      */
			in.data.pastOrderData.setupData.calculations[grp].slitsTandem,Network::SEP,//9
			in.data.pastOrderData.setupData.calculations[grp].scoresTandem,Network::SEP,//10
			in.data.pastOrderData.setupData.calculations[grp].shaftMask,Network::SEP,//11

			//RFazzi_FI01/06/2007_1.Added some modification for NoGapContinuousTrim
			String::Bool2Alpha(in.data.pastOrderData.setupData.aocType.getNoGap() || in.data.pastOrderData.setupData.aocType.getNoGapCTrimOneLevel()),Network::SEP,//12
			String::Bool2Alpha(in.data.pastOrderData.setupData.aocType.getTandem()),Network::SEP,//13
			String::Bool2Alpha(in.data.pastOrderData.setupData.aocType.getKnifeOnwards()),Network::SEP,//14

      //PMASSARO_FI16/08/11 - SyncroS: Aggiunta sicurezza in caso di aggiornamento DB ordini verso il Client
      /*
			String::Bool2Alpha(in.data.pastOrderData.setupData.calculations[in.data.pastOrderData.setupData.assignedGroup].StackerValidCancelAOC[0]),Network::SEP,//15
			String::Bool2Alpha(in.data.pastOrderData.setupData.calculations[in.data.pastOrderData.setupData.assignedGroup].StackerValidCancelAOC[1]),Network::SEP,//16
			String::Bool2Alpha(in.data.pastOrderData.setupData.calculations[in.data.pastOrderData.setupData.assignedGroup].StackerValidCancelAOC[2]),Network::SEP,//17
			*/
			String::Bool2Alpha(in.data.pastOrderData.setupData.calculations[grp].StackerValidCancelAOC[0]),Network::SEP,//15
			String::Bool2Alpha(in.data.pastOrderData.setupData.calculations[grp].StackerValidCancelAOC[1]),Network::SEP,//16
			String::Bool2Alpha(in.data.pastOrderData.setupData.calculations[grp].StackerValidCancelAOC[2]),Network::SEP,//17

      in.data.pastOrderData.getStopTime_UTs(),Network::SEP,//18
			in.data.pastOrderData.getAverageSpeed_UVm_m(),Network::SEP,//19
			in.data.pastOrderData.getScrapRSH_UDm(),Network::SEP,//20
			in.data.pastOrderData.setupData.quality.qualityFluteData.getFluteId(), Network::SEP,//21 used for flute
			
			//Order 0
			in.data.pastOrderData.setupData.order[0].getOrderNumber(),Network::SEP,//30
			DeliveryChar0, Network::SEP,//31
			in.data.pastOrderData.setupData.order[0].customer.getName(),Network::SEP,//32
			in.data.pastOrderData.setupData.order[0].twin.sheetWidth_UDdmm,Network::SEP,//33
			in.data.pastOrderData.setupData.order[0].twin.outs,Network::SEP,//34
			in.data.pastOrderData.setupData.order[0].twin.scoreMeasures_UDdmm,Network::SEP,//35
			in.data.pastOrderData.setupData.order[0].twin.getScoreType(),Network::SEP,//36
			in.data.pastOrderData.setupData.order[0].twin.positType,Network::SEP,//37
			in.data.pastOrderData.setupData.order[0].twin.teletwinOffset_UDdmm,Network::SEP,//38
			in.data.pastOrderData.setupData.order[0].twin.scoreGap_UDdmm,Network::SEP,//39
			in.data.pastOrderData.setupData.order[0].knife.levelName,Network::SEP,//40
			in.data.pastOrderData.setupData.order[0].knife.sheetLength_UDdmm,Network::SEP,//41
			in.data.pastOrderData.setupData.order[0].knife.segmentSheetCount,Network::SEP,//42
			in.data.pastOrderData.setupData.order[0].stacker.sheetsPerStack,Network::SEP,//43
			in.data.pastOrderData.setupData.order[0].materialHandling.stacksPerPallet,Network::SEP,//44
			in.data.pastOrderData.setupData.order[0].materialHandling.sending,Network::SEP,//45
			in.data.pastOrderData.getCutsProducedGoodOrder1(),Network::SEP,//46
			in.data.pastOrderData.getCutsProducedBadOrder1(),Network::SEP,//47
			in.data.pastOrderData.getCutsProducedDefaultOrder1(),Network::SEP,//48
			
			//Order 1
			in.data.pastOrderData.setupData.order[1].getOrderNumber(),Network::SEP,//50	
			DeliveryChar1, Network::SEP, //51
			in.data.pastOrderData.setupData.order[1].customer.getName(),Network::SEP,//52
			in.data.pastOrderData.setupData.order[1].twin.sheetWidth_UDdmm,Network::SEP,//53
			in.data.pastOrderData.setupData.order[1].twin.outs,Network::SEP,//54
			in.data.pastOrderData.setupData.order[1].twin.scoreMeasures_UDdmm,Network::SEP,//55
			in.data.pastOrderData.setupData.order[1].twin.getScoreType(),Network::SEP,//56
			in.data.pastOrderData.setupData.order[1].twin.positType,Network::SEP,//57
			in.data.pastOrderData.setupData.order[1].twin.teletwinOffset_UDdmm,Network::SEP,//58
			in.data.pastOrderData.setupData.order[1].twin.scoreGap_UDdmm,Network::SEP,//59
			in.data.pastOrderData.setupData.order[1].knife.levelName,Network::SEP,//60
			in.data.pastOrderData.setupData.order[1].knife.sheetLength_UDdmm,Network::SEP,//61
			in.data.pastOrderData.setupData.order[1].knife.segmentSheetCount,Network::SEP,//62
			in.data.pastOrderData.setupData.order[1].stacker.sheetsPerStack,Network::SEP,//63
			in.data.pastOrderData.setupData.order[1].materialHandling.stacksPerPallet,Network::SEP,//64
			in.data.pastOrderData.setupData.order[1].materialHandling.sending,Network::SEP,//65
			in.data.pastOrderData.getCutsProducedGoodOrder2(),Network::SEP,//66
			in.data.pastOrderData.getCutsProducedBadOrder2(),Network::SEP,//67
			in.data.pastOrderData.getCutsProducedDefaultOrder2(),Network::SEP,//68

			//Order 2
			in.data.pastOrderData.setupData.order[2].getOrderNumber(),Network::SEP,//70
			DeliveryChar2, Network::SEP,
			in.data.pastOrderData.setupData.order[2].customer.getName(),Network::SEP,//72
			in.data.pastOrderData.setupData.order[2].twin.sheetWidth_UDdmm,Network::SEP,//73
			in.data.pastOrderData.setupData.order[2].twin.outs,Network::SEP,//74
			in.data.pastOrderData.setupData.order[2].twin.scoreMeasures_UDdmm,Network::SEP,//75
			in.data.pastOrderData.setupData.order[2].twin.getScoreType(),Network::SEP,//76
			in.data.pastOrderData.setupData.order[2].twin.positType,Network::SEP,//77
			in.data.pastOrderData.setupData.order[2].twin.teletwinOffset_UDdmm,Network::SEP,//78
			in.data.pastOrderData.setupData.order[2].twin.scoreGap_UDdmm,Network::SEP,//79
			in.data.pastOrderData.setupData.order[2].knife.levelName,Network::SEP,//80
			in.data.pastOrderData.setupData.order[2].knife.sheetLength_UDdmm,Network::SEP,//81
			in.data.pastOrderData.setupData.order[2].knife.segmentSheetCount,Network::SEP,//82
			in.data.pastOrderData.setupData.order[2].stacker.sheetsPerStack,Network::SEP,//83
			in.data.pastOrderData.setupData.order[2].materialHandling.stacksPerPallet,Network::SEP,//84
			in.data.pastOrderData.setupData.order[2].materialHandling.sending,Network::SEP,//85
			in.data.pastOrderData.getCutsProducedGoodOrder3(),Network::SEP,//86
			in.data.pastOrderData.getCutsProducedBadOrder3(),Network::SEP,//87
			in.data.pastOrderData.getCutsProducedDefaultOrder3(),Network::SEP,//88

      TimeStrStart,Network::SEP, //90  
			TimeStrStop,Network::SEP, //91
			TimeStrShiftStart,Network::SEP,//92
			String::Bool2Alpha(in.data.pastOrderData.setupData.order[0].knife.cutToPattern.isTypeCutToMark()),Network::SEP,//93 
			String::Bool2Alpha(in.data.pastOrderData.setupData.order[1].knife.cutToPattern.isTypeCutToMark()),Network::SEP,//94 
			String::Bool2Alpha(in.data.pastOrderData.setupData.order[2].knife.cutToPattern.isTypeCutToMark()),Network::SEP,//95 
			L" ",Network::SEP,//96 spare (already dateTime on SyncroC)
			in.data.pastOrderData.SHIFTgetShiftId(),Network::SEP,//97 ShiftID
      //agrassi_FI26-05-08_3
			in.data.pastOrderData.setupData.key.getOfficeId(),Network::SEP,//98 OfficeID
			L" ",Network::SEP,//99 spare
			L" ",Network::SEP,//100 spare
			L" ",Network::SEP,//101 spare
			L" ",Network::SEP,//102 spare
			L" ",Network::SEP//103 spare
			);

      for (int n=0; n<NUM_ORDERS_PER_SETUP; n++)
			{
				//Order n
				cnt+=swprintf(ord+cnt, L"%s%s", in.data.pastOrderData.setupData.order[n].knife.cutToPattern.getCutToPatternID(), Network::SEP);
        cnt+=swprintf(ord+cnt, L"%s%s", in.data.pastOrderData.setupData.order[n].toPrint.getDataToPrint(1), Network::SEP);
        cnt+=swprintf(ord+cnt, L"%s%s", in.data.pastOrderData.setupData.order[n].toPrint.getDataToPrint(2), Network::SEP);
        cnt+=swprintf(ord+cnt, L"%s%s", in.data.pastOrderData.setupData.order[n].toPrint.getDataToPrint(3), Network::SEP);
        cnt+=swprintf(ord+cnt, L"%s%s", in.data.pastOrderData.setupData.order[n].toPrint.getDataToPrint(4), Network::SEP);
        cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.pastOrderData.setupData.order[n].stacker.getContinuousStack()), Network::SEP);
        cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.pastOrderData.setupData.order[n].materialHandling.pallet.getOnPlatform()), Network::SEP);
        cnt+=swprintf(ord+cnt, L"%s%s", in.data.pastOrderData.setupData.order[n].materialHandling.pallet.getNote(), Network::SEP);
        cnt+=swprintf(ord+cnt, L"%s%s", in.data.pastOrderData.setupData.order[n].customer.getDestinationAddress(), Network::SEP);
        cnt+=swprintf(ord+cnt, L"%s%s", in.data.pastOrderData.setupData.order[n].materialHandling.getBoxMachineCode(), Network::SEP);
        cnt+=swprintf(ord+cnt, L"%s%s", in.data.pastOrderData.setupData.order[n].materialHandling.getBoxMachineName(), Network::SEP);
        cnt+=swprintf(ord+cnt, L"%d%s", in.data.pastOrderData.setupData.order[n].toPrint.getNumberOfPalletLayout(), Network::SEP);
        cnt+=swprintf(ord+cnt, L"%d%s", in.data.pastOrderData.setupData.order[n].toPrint.getAmountOfPalletCopy(), Network::SEP);
        cnt+=swprintf(ord+cnt, L"%d%s", in.data.pastOrderData.setupData.order[n].toPrint.getNumberOfPastOrderLayout(), Network::SEP);
        cnt+=swprintf(ord+cnt, L"%d%s", in.data.pastOrderData.setupData.order[n].toPrint.getAmountOfPastOrderCopy(), Network::SEP);
		    cnt+=swprintf(ord+cnt, L"%s%s", String::Bool2Alpha(in.data.pastOrderData.setupData.order[n].knife.getPrePrintPaper()), Network::SEP);
        //agrassi_FI24-02-2010_3
		    cnt+=swprintf(ord+cnt, L"%d%s", in.data.pastOrderData.setupData.order[n].stacker.getDischargeSide(), Network::SEP);
        cnt+=swprintf(ord+cnt, L"%d%s", in.data.pastOrderData.setupData.order[n].total.balance, Network::SEP);
      }

      cnt+=swprintf(ord+cnt, L"%s%s",L" ",Network::SEP);

      //RFAZZI_FI07/06/2011_3. Aggiunto campo metri scartati nel gruppo di scarto della Master per livello nelle statistiche degli ordini prodotti. 
			cnt+=swprintf(ord+cnt, L"%d%s", in.data.pastOrderData.getDefaultCutLenghtOrder(1),Network::SEP);
			cnt+=swprintf(ord+cnt, L"%d%s", in.data.pastOrderData.getDefaultCutLenghtOrder(2),Network::SEP);
			cnt+=swprintf(ord+cnt, L"%d%s", in.data.pastOrderData.getDefaultCutLenghtOrder(3),Network::SEP);
			cnt+=swprintf(ord+cnt, L"%d%s", in.data.pastOrderData.getScrapMadeDischargeGroup_UDmm(1),Network::SEP);
			cnt+=swprintf(ord+cnt, L"%d%s", in.data.pastOrderData.getScrapMadeDischargeGroup_UDmm(2),Network::SEP);
			cnt+=swprintf(ord+cnt, L"%d)", in.data.pastOrderData.getScrapMadeDischargeGroup_UDmm(3));
			

      //agrassi_FI03-07-09_1
      wchar_t* ordOK=new wchar_t[10500];
      int iOK=0;
      for (size_t i=0; i<=wcslen(ord); i++)
      {
        ordOK[iOK++]=ord[i];
        if (ord[i]==L'%')
          ordOK[iOK++]=L'%';
      }
      //exec=o_socket.Send(Network::MSG_DB_REF, ord);
      exec=o_socket.Send(Network::MSG_DB_REF, ordOK);
      delete ordOK;

			logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, 
				L"PAST ORDER Command ADD ID: %d, start:%s, end: %s",
				in.data.pastOrderData.setupData.key.getSetupId(),
				TimeStrStart, TimeStrStop);
      }
		break;

	//delete a record command
	case NamedPipe::DataBasePipe::Del:
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"PAST ORDER Command: DELETE");
		exec=o_socket.Send(Network::MSG_DB_REF,L"pastOrder.Del(%d)",in.data.pastOrderData.setupData.key.getSetupId());
		break;

	//delete all record command
	case NamedPipe::DataBasePipe::Clear:
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"PAST ORDER Command: CLEAR");
		exec=o_socket.Send(Network::MSG_DB_REF,L"pastOrder.Clr()");
		break;

	//Error Command
	case NamedPipe::DataBasePipe::Error:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"ORDER Command: ERROR Error: %s",in.getResponse());
		break;
	
	//default
	default:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"PAST ORDER Command Not Found %d", in.command);
		return ERROR_SUCCESS;	break;

	}//switch

	if(in.isToView())
		o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s])",in.getResponse());
	
	return exec;
}

DWORD C_DBManager::CreateFluteCommands(NamedPipe::DataBasePipe& in)
{
	String::StrType init;
	DWORD exec=ERROR_SUCCESS;

	switch (in.command)
	{
	//Insert or Mod command
	case NamedPipe::DataBasePipe::Insert:
	case NamedPipe::DataBasePipe::Modify:
		init = in.isCommandInsert() ? L"Add" : L"Mod";
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"FLUTE Command: %s FluteId : %s",init.getPtr(),in.data.fluteData.getFluteId());
			
		exec=o_socket.Send(Network::MSG_DB_REF,
			//RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
			//L"flute.%s(%s%s%s%s%s%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s)",
			//				//  0 S 1 S 2 S 3 S 4 S 5 S 6 S 7 S 8 S 9 S 10S 11S 12S 20S 21S 22S 23S 24S 25S 26S 27S 28S 29S 30S 31S 32
			L"flute.%s(%s%s%s%s%s%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s)",
							//  0 S 1 S 2 S 3 S 4 S 5 S 6 S 7 S 8 S 9 S 10S 11S 12S 13S 21S 22S 23S 24S 25S 26S 27S 28S 29S 30S 31S 32
			init.getPtr(),
			in.data.fluteData.getFluteId(),Network::SEP,//0 fluteId
			in.data.fluteData.getSFSPosition(),Network::SEP,//1 SFSPosition
			in.data.fluteData.getFluteText(),Network::SEP,//2 FluteText
//DRY DATA
			in.data.fluteData.getScoreGap(),Network::SEP,//3 ScoreGap_UDdmm
			in.data.fluteData.getGrindingParameter(),Network::SEP,//4 GrindingParameter_UDm
			in.data.fluteData.getLubricationParameter(),Network::SEP,//5 LubricationParameter_UDm
			in.data.fluteData.getTrimGrindingsMultiplier(),Network::SEP,//6 TrimGrindingsMultiplier
			in.data.fluteData.getGrindingDistance(),Network::SEP,//7 GrindingTime_UDm
			in.data.fluteData.getLubricationTime(),Network::SEP,//8 LubricationTime_UTs
			in.data.fluteData.getProductionSpeed_UVm_m(),Network::SEP,//9 ProductionSpeed_UVm_m
			in.data.fluteData.getAOCSpeed_UVm_m(),Network::SEP,//10 AOCSpeed_UVm_m
			in.data.fluteData.getThickness_UDdmm(),Network::SEP,//11 BoardThickness_UDmm
			in.data.fluteData.getTerminalFluteCode(),Network::SEP,//12 TerminalFluteCode
			//RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
			//L"SPARE",Network::SEP,//20 Spare
			in.data.fluteData.getTwinFlapDisabled(),Network::SEP,//13 Internal Twin400 Flap Enabled
      //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
			in.data.fluteData.getCruisingSpeed_UVm_m(),Network::SEP,//21 CruisingSpeed_UVm_m
			L"SPARE",Network::SEP,//22 Spare
			L"SPARE",Network::SEP,//23 Spare
			L"SPARE",Network::SEP,//24 Spare
			L"SPARE",Network::SEP,//25 Spare
			L"SPARE",Network::SEP,//26 Spare
			L"SPARE",Network::SEP,//27 Spare
			L"SPARE",Network::SEP,//28 Spare
			L"SPARE",Network::SEP,//29 Spare
			L"SPARE",Network::SEP,//30 Spare
			L"SPARE",Network::SEP,//31 Spare
			L"SPARE"							//32 Spare
			);
		break;

	//delete one record command
	case NamedPipe::DataBasePipe::Del:
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"FLUTE Command: DEL FluteId : %s",in.data.fluteData.getFluteId());
		exec=o_socket.Send(Network::MSG_DB_REF,L"flute.Del(%s)",in.data.paperData.getPaperID());
		break;

	//delete all record command
	case NamedPipe::DataBasePipe::Clear:
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"FLUTE Command: CLEAR");
		exec=o_socket.Send(Network::MSG_DB_REF,L"flute.Clr()");
		break;

	//error
	case NamedPipe::DataBasePipe::Error:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"FLUTE Command: ERROR Error : %s",in.getResponse());
		break;

	default:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"FLUTE Command Not Found %d", in.command);
		return ERROR_SUCCESS;	break;

	}//Switch

	if(in.isToView())
		o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s])",in.getResponse());

	return exec;
}

//ABello_FI Aggiunta gestione CutToPattern
//Create the string for the CutToPattern command
DWORD C_DBManager::CreateCutToPatternCommands(NamedPipe::DataBasePipe& in)
{
	DWORD exec=ERROR_SUCCESS;
	String::StrType init;

	switch (in.command)
	{
	//Insert or Mod command
	//Insert or Mod command
	case NamedPipe::DataBasePipe::Insert:
	case NamedPipe::DataBasePipe::Modify:

    init = in.isCommandInsert() ? L"Add" : L"Mod";
    logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"CUT TO PATTERN Command: %s PatternId : %s",init.getPtr(), in.data.cutToPatternData.getCutToPatternID());
			
		exec=o_socket.Send(Network::MSG_DB_REF,
			//RFAZZI_FI12/08/2008_1.Added lateral position of pattern
			//L"cutToPattern.%s(%s%s%s%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s)", 
   //      				   // 0  1 S 2 S 3 S 4 S 5 S 6 S 7 S 8 S 9 S 10S 11S 12S 13S 14S 15S 16S17 S 18S 19S 20S 21S 22S 23S24 S 25S26 S27 S28 S29 S30 S31 S32 S33 S34 S35
			L"cutToPattern.%s(%s%s%s%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%d%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s)", 
         				   // 0  1 S 2 S 3 S 4 S 5 S 6 S 7 S 8 S 9 S 10S 11S 12S 13S 14S 15S 16S17 S 18S 19S 20S 21S 22S 23S24 S 25S26 S27 S28 S29 S30 S31 S32 S33 S34 S35
                init.getPtr(), //0
                in.data.cutToPatternData.getCutToPatternID(), Network::SEP, // 1 PatternID
                in.data.cutToPatternData.getText(), Network::SEP,// 2 PatternText
                in.data.cutToPatternData.getMarkDistance_UDdmm(0), Network::SEP,// 3 MarkDistance_UDdmm1
                in.data.cutToPatternData.getMarkDistance_UDdmm(1), Network::SEP,// 4 MarkDistance_UDdmm2
                in.data.cutToPatternData.getMarkDistance_UDdmm(2), Network::SEP,// 5 MarkDistance_UDdmm3
                in.data.cutToPatternData.getMarkDistance_UDdmm(3), Network::SEP,// 6 MarkDistance_UDdmm4
                in.data.cutToPatternData.getMarkDistance_UDdmm(4), Network::SEP,// 7 MarkDistance_UDdmm5
                in.data.cutToPatternData.getMarkDistance_UDdmm(5), Network::SEP,// 8 MarkDistance_UDdmm6
                in.data.cutToPatternData.getMarkDistance_UDdmm(6), Network::SEP,// 9 MarkDistance_UDdmm7
                in.data.cutToPatternData.getMarkDistance_UDdmm(7), Network::SEP,//10 MarkDistance_UDdmm8
                in.data.cutToPatternData.getMarkDistance_UDdmm(8), Network::SEP,//11 MarkDistance_UDdmm9
                in.data.cutToPatternData.getMarkDistance_UDdmm(9), Network::SEP,//12 MarkDistance_UDdmm10
                0, Network::SEP,//13 MarkDistance_UDdmm_Spare1
                0, Network::SEP,//14 MarkDistance_UDdmm_Spare2
                0, Network::SEP,//15 MarkDistance_UDdmm_Spare3
                0, Network::SEP,//16 MarkDistance_UDdmm_Spare4
                0, Network::SEP,//17 MarkDistance_UDdmm_Spare5
                in.data.cutToPatternData.getEyeContrast(), Network::SEP,//18 EyeContrast
                in.data.cutToPatternData.getFirstMarkId(), Network::SEP,//19 FirstMarkId
                in.data.cutToPatternData.getCutOffset_UDdmm(), Network::SEP,//20 CutOffset_UDdmm
                in.data.cutToPatternData.getWindowBeforePattern_UDdmm(), Network::SEP,//21 WindowBeforePattern_UDdmm
                in.data.cutToPatternData.getWindowAfterPattern_UDdmm(), Network::SEP,//22 WindowAfterPattern_UDdmm
								//RFAZZI_FI12/08/2008_1.Added lateral position of pattern
                in.data.cutToPatternData.getLateralPatternPosition_UDdmm(), Network::SEP,//23 LateralPatternPosition_UDdmm
                L"SPARE",Network::SEP,//24 SPARE1
                L"SPARE",Network::SEP,//25 SPARE2
                L"SPARE",Network::SEP,//26 SPARE3
                L"SPARE",Network::SEP,//27 SPARE4
                L"SPARE",Network::SEP,//28 SPARE5
                L"SPARE",Network::SEP,//29 SPARE6
                L"SPARE",Network::SEP,//30 SPARE7
                L"SPARE",Network::SEP,//31 SPARE8
                L"SPARE",Network::SEP,//32 SPARE9
                L"SPARE",Network::SEP,//33 SPARE10
                L"SPARE",Network::SEP,//34 SPARE11
                L"SPARE",Network::SEP,//35 SPARE12
                L"SPARE"//35 SPARE13
									);					

		break;

  //delete one record command
	case NamedPipe::DataBasePipe::Del:
    logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"CUT TO PATTERN Command: DEL PatternId : %s",in.data.cutToPatternData.getCutToPatternID());
		exec=o_socket.Send(Network::MSG_DB_REF,L"cutToPattern.Del(%s)", in.data.cutToPatternData.getCutToPatternID());
		break;

	//delete all record command
	case NamedPipe::DataBasePipe::Clear:
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"CUT TO PATTERN Command: CLEAR");
		exec=o_socket.Send(Network::MSG_DB_REF,L"cutToPattern.Clr()");
		break;

	//error
	case NamedPipe::DataBasePipe::Error:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"CUT TO PATTERN Command: ERROR Error : %s",in.getResponse());
		break;

	default:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"CUT TO PATTERN Command Not Found %d", in.command);
		return ERROR_SUCCESS;	
		break;
	}
	
	if(in.isToView())
		o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s])",in.getResponse());

	return exec;
}

//Create the string for the Printings command
DWORD C_DBManager::CreatePrintingsCommands(NamedPipe::DataBasePipe& in)
{
	DWORD exec=ERROR_SUCCESS;

	switch (in.command)
	{
	//PrintPalletLabel
	case NamedPipe::DataBasePipe::PrintPalletLabel:
	//PrintPastOrderLabel
	case NamedPipe::DataBasePipe::PrintPastOrderLabel:
	//PrintShiftReport
	case NamedPipe::DataBasePipe::PrintShiftReport:
		break;

	//error
	case NamedPipe::DataBasePipe::Error:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"Printings Command: ERROR Error : %s",in.getResponse());
		break;

	default:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"Printings Command Not Found %d", in.command);
		return ERROR_SUCCESS;	
		break;
	}
	
	if(in.isToView())
		o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s])",in.getResponse());

	return exec;
}

//Create the string for the PastRolls command
DWORD C_DBManager::CreatePastRollsCommands(NamedPipe::DataBasePipe& in)
{
	DWORD exec=ERROR_SUCCESS;

	switch (in.command)
	{
	//Insert command
	case NamedPipe::DataBasePipe::Insert:
		wchar_t TimeStrFinish[MAX_PATH];
    //agrassi_FI17-03-08
		wchar_t ShiftStartTimeStr[MAX_PATH];
		wchar_t TimeEnteredStr[MAX_PATH];
		String::SYSTEMTIME2Alpha(in.data.pastRollsData.STATISTICgetTimeRemoved(),TimeStrFinish,MAX_PATH);
    //agrassi_FI17-03-08
		String::SYSTEMTIME2Alpha(in.data.pastRollsData.STATISTICgetShiftStartTime(),ShiftStartTimeStr,MAX_PATH);
		String::SYSTEMTIME2Alpha(in.data.pastRollsData.STATISTICgetTimeEntered(),TimeEnteredStr,MAX_PATH);
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"PastRolls Command: ADD %s%s", TimeStrFinish, in.data.pastRollsData.getPaperType());
		exec=o_socket.Send(Network::MSG_DB_REF,
			L"pastRolls.Add(%s%s%d%s%d%s%d%s%s%s%s%s%d%s%d%s%d%s%d%s%d%s%s%s%c%s%s%s%s%s%s%s%s%s%s%s%s%s%d%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s)",
										// 0 S 1 S 2 S 3 S 4 S 5 S 6 S 7 S 8 S 9 S10 S11 S12 S13 S14 S15 S16 S17 S18 S19 S20 S21 S22 S23 S24 S25 S26 S27 S28 S29 S30 S31 S32 S33 S34 S35 S36 S37 S38 S39 S40 S41 S42 S43 S44 S45 S46 S47 S48 S49 S50
										in.data.pastRollsData.getPaperType(), Network::SEP,	//0 PaperType
										in.data.pastRollsData.getCoreDiameter_UDdmm(), Network::SEP,	//1 CoreDiameter_UDdmm
										in.data.pastRollsData.getWidth_UDdmm(), Network::SEP,	//2 Width_UDdmm
										in.data.pastRollsData.getWeightFactor_UWg_m(), Network::SEP,	//3 WeightFactor_UWg_m
										String::Bool2Alpha(in.data.pastRollsData.STATISTICgetSyncroSplice()), Network::SEP,	//4 syncroSplice
										TimeStrFinish, Network::SEP,	//5 timeRemoved
										in.data.pastRollsData.STATISTICgetRemainingDiameter_UDdmm(), Network::SEP,	//6 remainingDiameter_UDdmm
										in.data.pastRollsData.STATISTICgetRun_UDmm(), Network::SEP,	//7 run_UDmm
										in.data.pastRollsData.STATISTICgetRemaining_UDmm(), Network::SEP,	//8 remaining_UDmm
										in.data.pastRollsData.getThickness_UDdmmm(), Network::SEP,	//9 thickness_UDdmmm
										in.data.pastRollsData.TRACEgetSplicerNumber(), Network::SEP, //10 SplicerNumber
							      //agrassi_FI17-03-08
										ShiftStartTimeStr, Network::SEP, //11 Shift start time
										in.data.pastRollsData.STATISTICgetShiftId(), Network::SEP, //12 Shift id
										TimeEnteredStr, Network::SEP, //13 Time entered
										in.data.pastRollsData.STATISTICgetProgramCode(), Network::SEP, //14 Program code
										String::Bool2Alpha(in.data.pastRollsData.RSMgetRollDataEntered()), Network::SEP, //15 Roll data entered
                    //ABELLO_FI04/06/2008_1
										in.data.pastRollsData.RSMgetRollId() , Network::SEP, //16 Roll ID
										in.data.pastRollsData.RSMgetDescription() , Network::SEP, //17 Description
                    in.data.pastRollsData.RSMgetDataToPrint() , Network::SEP, //18 Data To Print
                    //agrassi_FI15-07-2010_1
                    in.data.pastRollsData.STATISTICgetSpliceSpeed_UVmm_min() , Network::SEP, //19 Splice Speed
                    String::Bool2Alpha(in.data.pastRollsData.STATISTICgetSpliceDone()) , Network::SEP, //20 Splice Done
										//L"SPARE", Network::SEP, //19 SPARE
										//L"SPARE", Network::SEP, //20 SPARE
										L"SPARE", Network::SEP, //21 SPARE
										L"SPARE", Network::SEP, //22 SPARE
										L"SPARE", Network::SEP, //23 SPARE
										L"SPARE", Network::SEP, //24 SPARE
										L"SPARE", Network::SEP, //25 SPARE
										L"SPARE", Network::SEP, //26 SPARE
										L"SPARE", Network::SEP, //27 SPARE
										L"SPARE", Network::SEP, //28 SPARE
										L"SPARE", Network::SEP, //29 SPARE
										L"SPARE", Network::SEP, //30 SPARE
										L"SPARE", Network::SEP, //31 SPARE
										L"SPARE", Network::SEP, //32 SPARE
										L"SPARE", Network::SEP, //33 SPARE
										L"SPARE", Network::SEP, //34 SPARE
										L"SPARE", Network::SEP, //35 SPARE
										L"SPARE", Network::SEP, //36 SPARE
										L"SPARE", Network::SEP, //37 SPARE
										L"SPARE", Network::SEP, //38 SPARE
										L"SPARE", Network::SEP, //39 SPARE
										L"SPARE", Network::SEP, //40 SPARE
										L"SPARE", Network::SEP, //41 SPARE
										L"SPARE", Network::SEP, //42 SPARE
										L"SPARE", Network::SEP, //43 SPARE
										L"SPARE", Network::SEP, //44 SPARE
										L"SPARE", Network::SEP, //45 SPARE
										L"SPARE", Network::SEP, //46 SPARE
										L"SPARE", Network::SEP, //47 SPARE
										L"SPARE", Network::SEP, //48 SPARE
										L"SPARE", Network::SEP, //49 SPARE
										L"SPARE" //50 SPARE
										);					

		break;

	//delete a record command
	case NamedPipe::DataBasePipe::Del:
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"PastRolls Command: DELETE ... non yet implemented");
		break;

	//delete all record command
	case NamedPipe::DataBasePipe::Clear:
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"PastRolls Command: CLEAR");
		exec=o_socket.Send(Network::MSG_DB_REF,L"pastRolls.Clr()");
		break;

	//error
	case NamedPipe::DataBasePipe::Error:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"PastRolls Command: ERROR Error : %s",in.getResponse());
		break;

	default:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"PastRolls Command Not Found %d", in.command);
		return ERROR_SUCCESS;	
		break;
	}
	
	if(in.isToView())
		o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s])",in.getResponse());

	return exec;
}

//Create the string for the DownTime command
DWORD C_DBManager::CreateDownTimeCommands(NamedPipe::DataBasePipe& in)
{
	DWORD exec=ERROR_SUCCESS;
	String::StrType init;

	switch (in.command)
	{
	//Insert or Mod command
	//Insert or Mod command
	case NamedPipe::DataBasePipe::Insert:
	case NamedPipe::DataBasePipe::Modify:
		init = in.isCommandInsert() ? L"Add" : L"Mod";
		wchar_t TimeStrStart[MAX_PATH];
		wchar_t TimeStrFinish[MAX_PATH];
		wchar_t TimeStrShiftStart[MAX_PATH];
		String::SYSTEMTIME2Alpha(in.data.downTime.getStartStoppageTime(),TimeStrStart,MAX_PATH);
		String::SYSTEMTIME2Alpha(in.data.downTime.getFinishStoppageTime(),TimeStrFinish,MAX_PATH);
		String::SYSTEMTIME2Alpha(in.data.downTime.getShiftStartOfStop(),TimeStrShiftStart,MAX_PATH);
		exec=o_socket.Send(Network::MSG_DB_REF,
		L"downTime.%s(%c%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%s%d%s%d%s%s%s%s%s%s%s%s)", init.getPtr(),
								// 0 S 1 S 2 S 3 S 4 S 5 S 6 S 7 S 8 S 9 S10 S11 S12 S13 S14 S15 S16 S17 S18
								in.data.downTime.getShiftIdOfStop(),Network::SEP,	//0 shiftId
								TimeStrShiftStart,Network::SEP,	//1  Shift start time
								TimeStrStart,	Network::SEP,	//2 Start Time
								TimeStrFinish,		Network::SEP,	//3 Finish Time
								L" ",		Network::SEP,	//4 spare (already dateTime on SyncroC)
								L" ",		Network::SEP,	//5 spare (already dateTime on SyncroC)
								L" ",		Network::SEP,	//6 spare (already dateTime on SyncroC)
								L" ",		Network::SEP,	//7 spare (already dateTime on SyncroC)
								in.data.downTime.getCodeOfStop(),	Network::SEP,	//8 Code Of Stop
								in.data.downTime.getDescriptionOfStop(),		Network::SEP,	//9 Description Of Stop
								in.data.downTime.getPlaceCodeOfStop(),		Network::SEP,	//10 Place Code Of Stop
								in.data.downTime.getPlaceDescriptionOfStop(),		Network::SEP,	//11 Place Description Of Stop
								in.data.downTime.getText(),		Network::SEP,	//12 Text
								in.data.downTime.setupKeyDownTime.getSetupId(),			Network::SEP,	//13 Setup Id
								in.data.downTime.setupKeyDownTime.getModId(),Network::SEP,	//14 Setup ModId
								L" ",		Network::SEP,	//15 spare
								L" ",		Network::SEP,	//16 spare
								L" ",		Network::SEP,	//17 spare
								L" "	//18 spare
								);					

		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"DOWNTIME Command: ADD %s %c",TimeStrStart,in.data.downTime.getShiftIdOfStop());
		break;

	//delete all record command
	case NamedPipe::DataBasePipe::Clear:
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"DOWNTIME Command: CLEAR");
		exec=o_socket.Send(Network::MSG_DB_REF,L"downTime.Clr()");
		break;

	//error
	case NamedPipe::DataBasePipe::Error:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"DOWNTIME Command: ERROR Error : %s",in.getResponse());
		break;

	default:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"DOWNTIME Command Not Found %d", in.command);
		return ERROR_SUCCESS;	
		break;
	}
	
	if(in.isToView())
		o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s])",in.getResponse());

	return exec;
}

//Create the string for the Shift command
DWORD C_DBManager::CreateShiftCommands(NamedPipe::DataBasePipe& in)
{
	DWORD exec=ERROR_SUCCESS;

	switch (in.command)
	{
	//Insert or Mod command
	case NamedPipe::DataBasePipe::Insert:
		wchar_t TimeStrStart[MAX_PATH];
		wchar_t TimeStrFinish[MAX_PATH];
		String::SYSTEMTIME2Alpha(in.data.shiftData.getFinishTime(),TimeStrFinish,MAX_PATH);
		String::SYSTEMTIME2Alpha(in.data.shiftData.getStartTime(),TimeStrStart,MAX_PATH);
		exec=o_socket.Send(Network::MSG_DB_REF,
			L"shift.Add(%c%s%s%s%s%s%d%s%d%s%d%s%d%s%d%s%s%s%d%s%d%s%s%s%s%s%s%s%s%s%s%s%s)",
					  // 0 S 1 S 2 S 3 S 4 S 5 S 6 S 7 S 8 S 9 S10 S11 S12 S13 S14 S15 S16
			in.data.shiftData.getShiftId(), Network::SEP,//0 shiftId
			TimeStrStart, Network::SEP,//1 Start Time
			TimeStrFinish, Network::SEP,//2 Finish Time
			in.data.shiftData.getRunTime_UTs(), Network::SEP,//3 RunTime_UTs
			in.data.shiftData.getStopTime_UTs(), Network::SEP,//4 StopTime_UTs
			in.data.shiftData.getNumberOfStop(), Network::SEP,//5 NumberOfStop
			in.data.shiftData.getNumSetupChanges(), Network::SEP,//5 SetupChanges
			in.data.shiftData.getNumQualityChanges(), Network::SEP,//7 QualityChanges
			in.data.shiftData.getShiftManagerName(), Network::SEP,//8 shiftManagerName
			in.data.shiftData.getNumberOfPeople(), Network::SEP,//9 NumberOfPeople
			in.data.shiftData.getAvgSpeed_UVm_m(), Network::SEP,// 10 Average speed
			L" ", Network::SEP,//11 spare
			L" ", Network::SEP,//12 spare
			L" ", Network::SEP,//13 spare
			L" ", Network::SEP,//14 spare
			L" ", Network::SEP,//15 spare
			L" "//16 spare
			);
			for (int i = 0; i < MAX_FLUTE_PER_SHIFT; i++)
			{
				if (in.data.shiftData.getFlute(i).fluteShift.isEmptyFluteID())
				{//no more flutes worked
					break;
				}
				else
				{
					exec=o_socket.Send(Network::MSG_DB_REF,
						L"shift.AddFlute(%c%s%s%s%s%s%d%s%d%s%d%s%d%s%s%s%s%s%s%s%s)",
									   // 0 S 1 S 2 S 3 S 4 S 5 S 6 S 7 S 8 S 9 S10
						in.data.shiftData.getShiftId(), Network::SEP,//0 shiftId
						TimeStrStart, Network::SEP,//1 Start Time
						in.data.shiftData.getFlute(i).fluteShift.getFluteId(), Network::SEP,//2 FluteText
						in.data.shiftData.getFlute(i).getMetersProducedSS_UDm(), Network::SEP,//3 
						in.data.shiftData.getFlute(i).getScrapRSH_UDm(), Network::SEP,//4 
						
						//PMASSARO_FI13/01/09 - Must Show The Good Meters
						/*  						
							//PMASSARO_FI04/09/08 - Bug Fix And Report Review   
							//
							//in.data.shiftData.getFlute(i).getMetersProducedSS_UAm(), Network::SEP,//5 
							//
							//in.data.shiftData.getFlute(i).getMetersProducedSS_UAm() - in.data.shiftData.getFlute(i).getMetersBad_UAm(), Network::SEP,//5 
						*/
						(int) in.data.shiftData.getFlute(i).getMetersProducedOnlyGoodSheets_UAm(), Network::SEP,//5 
						in.data.shiftData.getFlute(i).getMetersBad_UAm(), Network::SEP,//6 
						L" ", Network::SEP, //7 spare
						L" ", Network::SEP, //8 spare
						L" ", Network::SEP, //9 spare
						L" " //10 spare
						);
				}
			} 
		
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"SHIFT Command: ADD %s %c",TimeStrStart,in.data.shiftData.getShiftId());
		break;

	//delete all record command
	case NamedPipe::DataBasePipe::Clear:
		logger.Info(__FUNCTION__, Logger::PRE_PIPE_RCV_AND_SOCK_SEND, L"SHIFT Command: CLEAR");
		exec=o_socket.Send(Network::MSG_DB_REF,L"shift.Clr()");
		break;

	//error
	case NamedPipe::DataBasePipe::Error:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"SHIFT Command: ERROR Error : %s",in.getResponse());
		break;

	default:
		logger.Warning(__FUNCTION__, Logger::PRE_PIPE_RCV, L"SHIFT Command Not Found %d", in.command);
		return ERROR_SUCCESS;	
		break;
	}
	
	if(in.isToView())
		o_socket.Send(Network::MSG_PAG_REF,L"pageManager._SetMessage(s[%s])",in.getResponse());

	return exec;
}

DWORD C_DBManager::CreateCommands(NamedPipe::DataBasePipe& inbuf)
{
	switch(inbuf.getDB())
	{
	case NamedPipe::DataBasePipe::Order:			
		return C_DBManager::CreateOrdersCommands(inbuf);
		break;

	case NamedPipe::DataBasePipe::Program:			
		return C_DBManager::CreateProgramsCommands(inbuf);
		break;
	
	case NamedPipe::DataBasePipe::Quality:
		return C_DBManager::CreateQualityCommands(inbuf);
		break;

	case NamedPipe::DataBasePipe::Paper:
		return C_DBManager::CreatePaperCommands(inbuf);
		break;

	case NamedPipe::DataBasePipe::Flute:
		return C_DBManager::CreateFluteCommands(inbuf);
		break;

	case NamedPipe::DataBasePipe::PastOrder:
		return C_DBManager::CreatePastOrderCommands(inbuf);
		break;

	case NamedPipe::DataBasePipe::ShiftDB:
		return C_DBManager::CreateShiftCommands(inbuf);
		break;

	case NamedPipe::DataBasePipe::Downtime:
		return C_DBManager::CreateDownTimeCommands(inbuf);
		break;

	case NamedPipe::DataBasePipe::CutToPattern:
		return C_DBManager::CreateCutToPatternCommands(inbuf);
		break;

	case NamedPipe::DataBasePipe::PastRolls:
		return C_DBManager::CreatePastRollsCommands(inbuf);
		break;

	case NamedPipe::DataBasePipe::Printings:
		return C_DBManager::CreatePrintingsCommands(inbuf);
		break;

	case NamedPipe::DataBasePipe::DbVer:
		return C_DBManager::CreateDBVerCommand(inbuf);
		break;

	case NamedPipe::DataBasePipe::Exit:
		{DWORD err = C_DBManager::CreateExitCommand(inbuf);
		if(inbuf.isCommandAlign())
      return err;
		else
			return ERROR_SHUTDOWN_IN_PROGRESS;
		break;}

	default:
		logger.Severe(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"Strange command arrived from DBMS (DataBase %d)",inbuf.getDB());
		return ERROR_BAD_COMMAND;
		break;
	}

	return ERROR_BAD_COMMAND;
}
DWORD WINAPI C_DBManager::DBMSNamedPipe(LPVOID  lpParam)
{
	DWORD  ret;
	NamedPipe::DataBasePipe inbuf;
	inbuf.CleanDataBasePipe();

	o_socket.Send(Network::MSG_PAG_REF,L"pageManager._Clear()");

	C_DBManager::finished=false;
	
	while(!C_DBManager::finished)
	{
		inbuf.CleanDataBasePipe();

		//read from pipe
		ret = DBMSSyncroS.Read(&inbuf);

		if(C_DBManager::finished)
			break;
		
		if(ret==ERROR_SUCCESS)
		{
			logger.Fine(__FUNCTION__, Logger::PRE_PIPE_RCV, L"Received a command from DBMS, ListDB.push_back");
			C_DBManager::ListDB.push_back(inbuf);
		}
		else
		{
			logger.Severe(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"Named Pipe with DBMS Error ... %d",ret);
			if(ERROR_BROKEN_PIPE==ret)
			{
				logger.Severe(__FUNCTION__, Logger::PRE_PIPE_ERROR, L"Named Pipe Down Restart DBMS .... %d",ret);
				/////////////TODO: Bloccare l'uso di tutti i data Base mandando un messaggio di errore

				//Clean all db on client
				o_socket.Send(Network::MSG_PAG_REF,L"pageManager._Clear()");
				o_socket.Send(Network::MSG_DB_SWI,L"ClearAll()");

				DBMSSyncroS.myCreateFile();
				/////////////TODO: Sbloccare l'uso di tutti i data Base mandando un messaggio di OK
				//Alignment all data base
				o_socket.Send(Network::MSG_DB_SWI,L"DBAlignment()");

				logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"OK: Named Pipe found.");
			}	
			else
				Sleep(1000);
		}
	}
	logger.Severe(__FUNCTION__,Logger::PRE_NULL, L"DBMSNamedPipe exit thread");

	return ERROR_SUCCESS;
}
