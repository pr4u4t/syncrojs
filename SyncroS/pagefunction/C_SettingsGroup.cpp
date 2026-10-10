#include ".\C_SettingsGroup.h"

//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

#include "..\..\Shared\C_SMTaskSettings.h"
extern TaskSettings::C_SMTaskSettings SMTaskSettings;

#include "..\..\Include\ite.h"
#include "..\..\Include\ite_system.h"
#include "..\..\Include\dio_ite.h"
#include "..\..\Include\dsp_ctc_ite.h"
#include "..\..\Include\aio_ite.h"

#include "..\..\Include\IB_Utility_Lib.h"
#include "..\C_PageManager.h"

int C_SettingsGroup::DSPModule;

bool C_SettingsGroup::end_Flute;
bool C_SettingsGroup::end_Languages;
bool C_SettingsGroup::end_LineConfig;
bool C_SettingsGroup::end_DownTimeReasons;
bool C_SettingsGroup::end_DIOStatus;
bool C_SettingsGroup::end_CTCStatus;
bool C_SettingsGroup::end_AIOStatus;
bool C_SettingsGroup::end_SimulationGeneral;
bool C_SettingsGroup::end_RestoreBackup;
bool C_SettingsGroup::end_Operations;

C_SettingsGroup::C_SettingsGroup(void)
{
	pid_SettingsPage = NULL;
	DSPModule = 0;
	TerminateThread(false);
}

C_SettingsGroup::~C_SettingsGroup(void)
{
}

bool C_SettingsGroup::ChooseSettingsPage(String::StrType  thePageRequest)
{
	if(thePageRequest.CompareNoCase(L"Languages"))
	{
		end_Languages = false;
		pid_SettingsPage = ::CreateThread(NULL,0,Languages,NULL,0,NULL);
		return true;
	}

	if(thePageRequest.CompareNoCase(L"LineConfig"))
	{
		end_LineConfig = false;
		pid_SettingsPage = ::CreateThread(NULL,0,LineConfig,NULL,0,NULL);
		return true;
	}

	if(thePageRequest.CompareNoCase(L"Flute"))
	{
		end_Flute = false;
		pid_SettingsPage=::CreateThread(NULL,0,Flute,NULL,0,NULL);
		return true;
	}

	if(thePageRequest.CompareNoCase(L"DownTimeReasons"))
	{
		end_DownTimeReasons = false;
		pid_SettingsPage=::CreateThread(NULL,0,DownTimeReasons,NULL,0,NULL);
		return true;
	};

	if(thePageRequest.CompareNoCase(L"DIOStatus"))
	{
		end_DIOStatus = false;
		pid_SettingsPage=::CreateThread(NULL,0,DIOStatus,NULL,0,NULL);
		return true;
	};

	if(thePageRequest.CompareNoCase(L"CTCStatus"))
	{
		end_CTCStatus = false;
		pid_SettingsPage=::CreateThread(NULL,0,CTCStatus,NULL,0,NULL);
		return true;
	};

	if(thePageRequest.CompareNoCase(L"AIOStatus"))
	{
		end_AIOStatus = false;
		pid_SettingsPage=::CreateThread(NULL,0,AIOStatus,NULL,0,NULL);
		return true;
	};

	if(thePageRequest.CompareNoCase(L"SimulationGeneral"))
	{
		end_SimulationGeneral = false;
		pid_SettingsPage=::CreateThread(NULL,0,SimulationGeneral,NULL,0,NULL);
		return true;
	};

	if(thePageRequest.CompareNoCase(L"Operations"))
	{
		end_SimulationGeneral = false;
		pid_SettingsPage=::CreateThread(NULL,0,Operations,NULL,0,NULL);
		return true;
	};

	if(thePageRequest.CompareNoCase(L"RestoreBackup"))
	{
		end_RestoreBackup = false;
		pid_SettingsPage=::CreateThread(NULL,0,RestoreBackup,NULL,0,NULL);
		return true;
	};

	return false;
}

void C_SettingsGroup::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_SettingsPage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_SettingsPage,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread SettingsPage ... %d", ::GetLastError());
		}
	}
	else
	{
		end_Flute = true;
		end_Languages = true;
		end_LineConfig = true;
		end_DownTimeReasons = true;
		end_DIOStatus = true;
		end_CTCStatus = true;
		end_AIOStatus = true;
		end_SimulationGeneral=true;
		end_Operations=true;
    end_RestoreBackup=true;
	}
}

DWORD C_SettingsGroup::SuspendThread(void)
{
	if(pid_SettingsPage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_SettingsPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page Settings Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_SettingsGroup::ResumeThread(void)
{
	if(pid_SettingsPage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_SettingsPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page Settings Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}

void C_SettingsGroup::SetDIOModule(const int theDSPModule)
{
	DSPModule = theDSPModule;
	
	if (ValidModuleDIO(theDSPModule) == NO)
	{
		o_socket.Send(Network::MSG_PAG_REF,L"settings_DIOStatus._clearDIOStatus()");
		o_socket.Send(Network::MSG_PAG_REF,L"settings_DIOStatus.lbl_Title_YN.Text=Illegal Address %d", theDSPModule);

		//ABello_FI13/07/2007_1Accedendo alla pagina dei DIO scorrendo in avanti tutti i dsp fino ad arrivare all'ultimo SYNCROS genera un errore critico
		return;
	}
	else
	{
		//o_socket.Send(Network::MSG_PAG_REF,L"settings_DIOStatus.lbl_Title_YN.Text=DSP%02d   %S  %S", DSPModuleDIO(theDSPModule),  NameModuleTypeDIO(theDSPModule),ite->IO[theDSPModule].ConfigFile);
		char DIOFileName[260];
		if ( strstr(pITE->IO[theDSPModule].ConfigFile,"IO") != NULL )
			sprintf (DIOFileName,"%s",strstr(pITE->IO[theDSPModule].ConfigFile,"IO")+3);
		else
			sprintf(DIOFileName,"Not Standard %s",pITE->IO[theDSPModule].ConfigFile);

    //ABELLO_FI26/11/2010_1 non si vedevano i percorsi dei file .ite da cui si caricavano i DIO
    String::StrType dioNameStr = String::StrType(DIOFileName);
    dioNameStr.RemoveSubString(L".ite");
    o_socket.Send(Network::MSG_PAG_REF,L"settings_DIOStatus.lbl_Title_YN.Text=%S - %s", NameModuleTypeDIO(theDSPModule), dioNameStr.getPtr());
	}

	for (int i = 0; i < dio_ite->ModuleInfo[theDSPModule].TotalChannels; i++)
	{
		int index = dio_ite->ModuleInfo[theDSPModule].ModuleIndex + i;

		if (   (dio_ite->channel[index].type != NOT_USED_DIO)
			&& (dio_ite->channel[index].number != ERROR_ITE) )
		{
			int ChannelId = ID_DIO(index);
			if (VALID_DIO(ChannelId) == FALSE)
			{
				o_socket.Send(Network::MSG_PAG_REF,L"settings_DIOStatus._setDIOStatus(i[%d],s[ ])", i);
				continue;
			}

			//Number
			int number = dio_ite->channel[index].number;

			//Name
			wchar_t name[60];
			swprintf(name,L"%c%c%S", (dio_ite->channel[index].SlaveOutputListPtr == NULL_SLAVE_OUTPUT_LIST_DIO) ? L' ':L'!',
										 (dio_ite->channel[index].auto_alarm_list_ptr == NULL_AUTO_ALARM_LIST_DIO) ? L' ':L'*',
										 dio_ite->channel[index].name);
			//Type
			char *TypeId = NULL;
			switch (dio_ite->channel[index].type)
			{
				case DIGIN_DIO:
          //agrassi_FI25-05-2010_2
					//TypeId = "Digin";
			    if (   (dio_ite->channel[index].DebounceOnTicks)
				    || (dio_ite->channel[index].DebounceOffTicks) )
			    {
				    TypeId = "DiginDB";
			    }
			    else
			    {
				    TypeId = "Digin  ";
			    }
					break;

				case DIGOUT_DIO:
					TypeId = "Digout";
					break;

				case ALARM_OUT_DIO:
					TypeId = "Alm Out";
					break;

				case REMOTE_OUT_DIO:
					TypeId = "Rem Out";
					break;

			case ENCODER_IN_DIO:		
					TypeId = "Enc In";
					break;

				case SLAVE_OUT_DIO:		
					TypeId = "Slv Out";
					break;

				case ALARM_PULSE_DIO:
					TypeId = "Alm Pul";
					break;

				case NOT_USED_DIO:
					TypeId = "Spare";	
					break;

				default:
					TypeId = "ERROR";	
					logger.Severe(__FUNCTION__,L"Unknown channel type %d (ID %08X)", dio_ite->channel[index].type, ChannelId );
					break;
			}
			//Invert
			bool Invert = (dio_ite->channel[index].invert == YES); 

			//Status
			bool status = (dio_ite->channel[index].status==ON);

			//Send the string
			o_socket.Send(Network::MSG_PAG_REF,L"settings_DIOStatus._setDIOStatus(i[%d],i[%d],s[%s],s[%S],b[%s],b[%s])", 
																				i, number, name, TypeId, String::Bool2Alpha(Invert), String::Bool2Alpha(status));
		}
		else
			o_socket.Send(Network::MSG_PAG_REF,L"settings_DIOStatus._clearDIOStatus(i[%d])", i);
	}
}

void C_SettingsGroup::SetCTCModule(const int theDSPModule)
{
	DSPModule = theDSPModule;
	
	if (ValidModuleCTC(theDSPModule) == NO)
	{
		o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._clearCTCStatus()");
		o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus.lbl_Title_YN.Text=Illegal Module ID %d", theDSPModule);
		
		//ABello_FI13/07/2007_1Accedendo alla pagina dei DIO scorrendo in avanti tutti i dsp fino ad arrivare all'ultimo SYNCROS genera un errore critico
		return;
	}
	else
	{
		//o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus.lbl_Title_YN.Text=DSP%02d   %S", DSPModuleCTC(theDSPModule),  NameModuleTypeDIO(theDSPModule));
		char CTCFileName[260];
		if ( strstr(pITE->DSP[theDSPModule].ConfigFileCTC,"IO") != NULL )
			sprintf (CTCFileName,"%s",strstr(pITE->DSP[theDSPModule].ConfigFileCTC,"IO")+3);
		else
			sprintf(CTCFileName,"Not Standard %s",pITE->DSP[theDSPModule].ConfigFileCTC);
		o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus.lbl_Title_YN.Text=%S - %S", NameModuleTypeDIO(theDSPModule), CTCFileName);
	}

	for (int i = 0; i < COUNTERS_PER_DSP_CTC; i++)
	{
		int index = DSPModule * COUNTERS_PER_DSP_CTC + i;
		if ( dspCTC->Counter[index].Type != NOT_USED_CTC)
		{
			int CounterId = ID_CTC(index);
			if (VALID_CTC(CounterId) == FALSE)
			{
				o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._setCTCStatus(i[%d],i[0],s[ ],s[ ],s[ ],s[ ],s[ ],s[ ],s[ ],s[ ])", i);
				continue;
			}

			struct CounterType *Counter = &dspCTC->Counter[ INDEX_CTC(CounterId) ];

			//Number
			int number = Counter->CounterNumber;

			//Name
			wchar_t name[60];
			swprintf(name,L"%S", Counter->Name);

			//Type type, Source, Config, Value, Count, Increment, Signal
			//			1			2				3				4			5					6					7
			wchar_t Buffer[LINE_BUF_SIZE];
			int Size=0;
			switch (Counter->Type)
			{
			case COUNTDOWN_CTC://																	1			    2			3		4		  5		 6		7
				Size += swprintf(Buffer+Size, L"s[Countdown],s[%d],s[ ],s[ ],s[ ],s[ ],s[ ]", 
								Counter->DisplaySource);
				break;

			//RFAZZI_FI03/06/2009_1. Allineato ITE alla versione 7v5.14.
			case REV_NO_DIV_CTC://											1				2			 3		 4			5			6 	7
				Size += swprintf(Buffer+Size, L"s[Rev],s[%d],s[%d],s[%d],s[%d],s[ ],s[ ]",
								Counter->DisplaySource, Counter->DisplayConfig,
								Counter->CountRegister, Counter->GateCountRegister);
				break;

			case REV_DIV_2_CTC://										1		  	2			3			4			5		6		7
				Size += swprintf(Buffer+Size, L"s[Rev / 2],s[%d],s[%d],s[%d],s[%d],s[ ],s[ ]",
								Counter->DisplaySource, Counter->DisplayConfig,
								Counter->CountRegister, Counter->GateCountRegister);
				break;

			case NOT_USED_CTC://															 1		2		3			4			5		6			7
				Size += swprintf(Buffer+Size, L"s[ ],s[ ],s[ ],s[ ],s[ ],s[ ],s[ ]");
				break;

			//RFAZZI_FI03/06/2009_1. Allineato ITE alla versione 7v5.14.
			case SPEED_HF_CTC://											1				2			3			4			5			6		7
				Size += swprintf(Buffer+Size, L"s[Speed],s[%d],s[%d],s[%d],s[ ],s[ ],s[ ]",
								Counter->DisplaySource, Counter->DisplayConfig,
								Counter->CountRegister );
				break;

			//RFAZZI_FI03/06/2009_1. Allineato ITE alla versione 7v5.14.
			case SPEED_LF_CTC://											1				2			3			4			5			6		7
				Size += swprintf(Buffer+Size, L"s[Speed],s[%d],s[%d],s[%d],s[ ],s[ ],s[ ]",
								Counter->DisplaySource, Counter->DisplayConfig,
								Counter->CountRegister );
				break;

			case DIVIDE_TOGGLE_CTC://								1			 2			3			4		5			6		7
				Size += swprintf(Buffer+Size, L"s[Div/Tog],s[%d],s[%d],s[ ],s[ ],s[ ],s[ ]",
								Counter->DisplaySource, Counter->DisplayConfig);
				break;

			case LENGTH_CTC://										1				2			 3			4			5		6			7
				Size += swprintf(Buffer+Size, L"s[Length],s[%d],s[%d],s[%d],s[ ],s[ ],s[ ]",
								Counter->DisplaySource, Counter->DisplayConfig,
								Counter->CountRegister );
				break;

			case FRT_CTC:
				if (Counter->TaskFRT == NOT_CONFIGURED_TASK)
				{//																		1				2			 3		 4		 5		6		7
					Size += swprintf(Buffer+Size, L"s[FRT],s[%d],s[%d],s[%u],s[ ],s[ ],s[ ]",
								Counter->DisplaySource, Counter->DisplayConfig,
								Counter->CountRegister );
				}
				else
				{//																	1				2			3			 4			5		6			 7
					Size += swprintf(Buffer+Size, L"s[FRT],s[%d],s[%d],s[%d],s[ ],s[ ],s[%S]",
									Counter->DisplaySource, Counter->DisplayConfig,
									Counter->CountRegister, OutputTaskId(Counter->TaskFRT));
				}
				break;

			//case KNIFE_CTC:
			//	if (Counter->TaskFRT == NOT_CONFIGURED_TASK)
			//	{//																		1			 2			 3		4			 5			6			7
			//		Size += swprintf(Buffer+Size, L"s[Knife],s[%d],s[%d],s[%d],s[%d],s[ ],s[ ]",
			//						Counter->DisplaySource, Counter->DisplayConfig,
			//						Counter->CountRegister,
			//						Counter->GateCountRegister);
			//	}
			//	else
			//	{//																		 1		 2		 3		 4			5			6			 7
			//		Size += swprintf(Buffer+Size, L"s[Knife],s[%d],s[%d],s[%d],s[%d],s[ ],s[%S]",
			//						Counter->DisplaySource, Counter->DisplayConfig,
			//						Counter->CountRegister,
			//						Counter->GateCountRegister, OutputTaskId(Counter->TaskFRT));
			//	}
			case KNIFE_NO_DIV_CTC:
				if (Counter->TaskFRT == NOT_CONFIGURED_TASK)
				{//																		1			 2			 3		4			 5			6			7
					Size += swprintf(Buffer+Size, L"s[Knife],s[%d],s[%d],s[%d],s[%d],s[ ],s[ ]",
									Counter->DisplaySource, Counter->DisplayConfig,
									Counter->CountRegister,
									Counter->GateCountRegister);
				}
				else
				{//																		 1		 2		 3		 4			5			6			 7
					Size += swprintf(Buffer+Size, L"s[Knife],s[%d],s[%d],s[%d],s[%d],s[ ],s[%S]",
									Counter->DisplaySource, Counter->DisplayConfig,
									Counter->CountRegister,
									Counter->GateCountRegister, OutputTaskId(Counter->TaskFRT));
				}
				break;

			case KNIFE_DIV_2_CTC:
				if (Counter->TaskFRT == NOT_CONFIGURED_TASK)
				{//																		1			 2			 3		4			 5			6			7
					Size += swprintf(Buffer+Size, L"s[Knife],s[%d],s[%d],s[%d],s[%d],s[ ],s[ ]",
									Counter->DisplaySource, Counter->DisplayConfig,
									Counter->CountRegister,
									Counter->GateCountRegister);
				}
				else
				{//																		 1		 2		 3		 4			5			6			 7
					Size += swprintf(Buffer+Size, L"s[Knife],s[%d],s[%d],s[%d],s[%d],s[ ],s[%S]",
									Counter->DisplaySource, Counter->DisplayConfig,
									Counter->CountRegister,
									Counter->GateCountRegister, OutputTaskId(Counter->TaskFRT));
				}
				break;
			default:
				logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"FormatCounterDiagsCTC - Unknown counter type %X (%08X)", Counter->Type, CounterId );
				break;
			}//Switch

			//Send the string
			o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._setCTCStatus(i[%d],i[%d],s[%s],%s)", 
																				i, number, name, Buffer);
		}//if
		else
			o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._clearCTCStatus(i[%d])", i);
	}
}

DWORD WINAPI C_SettingsGroup::DIOStatus(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"settings_DIOStatus._Show()");
	
	SMTaskSettings.waitForStartITE();

	char exePath[MAX_PATH];

	GetModuleFileNameA(NULL,exePath,MAX_PATH);
	//Store the runDirectory
	*strrchr(exePath,'\\') = 0;
	F_SetRunDirectory(exePath);

	BOOLEAN res = InitialiseDIO();
	
	while(!end_DIOStatus)
	{
		if(res == FALSE)
		{
			o_socket.Send(Network::MSG_PAG_REF,L"settings_DIOStatus.lbl_Title_YN.Text=ERROR ITE NOT INSTALLED");
			Sleep(15000);
			continue;
		}

		int module = DSPModule;

    //ABELLO_FI26/11/2010_1 non si vedevano i percorsi dei file .ite da cui si caricavano i DIO
		//while(ValidModuleDIO(module) == NO)
		//{
		//	module = DSPModule;
		//	o_socket.Send(Network::MSG_PAG_REF,L"settings_DIOStatus._clearDIOStatus()");
		//	::Sleep(500);
		//}


    //RFAZZI_FI20/04/2011_1. Apportate modifiche ad ITE per Windows7. 
		if( TryLinkToITE(NULL, NULL, NULL,FALSE) == OK )
		{
			int	ModuleAlarm = ModuleAlarmDIO(module);
			o_socket.Send(Network::MSG_PAG_REF,L"settings_DIOStatus._setErrorDIOStatus(b[%s])", String::Bool2Alpha((StatusAlarm(ModuleAlarm) == ALARM_SET)));

			for (int i = 0; i < dio_ite->ModuleInfo[module].TotalChannels; i++)
			{
				int index = dio_ite->ModuleInfo[module].ModuleIndex + i;

				if (   (dio_ite->channel[index].type != NOT_USED_DIO)
					&& (dio_ite->channel[index].number != ERROR_ITE) )
				{
					o_socket.Send(Network::MSG_PAG_REF,L"settings_DIOStatus._setDIOStatus(i[%d],b[%s])", i, String::Bool2Alpha(dio_ite->channel[index].status==ON));
				}
			}	
		}
		else
		{
			logger.Warning(__FUNCTION__, L"TryLinkToITE - Failed ... try again in 1,5 sec");
			Sleep(1000);
		}

		Sleep(500);
	}

	logger.Fine(__FUNCTION__, L"Terminated settings_DIOStatus page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_SettingsGroup::CTCStatus(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._Show()");
	char exePath[MAX_PATH];

	GetModuleFileNameA(NULL,exePath,MAX_PATH);
	//Store the runDirectory
	*strrchr(exePath,'\\') = 0;
	F_SetRunDirectory(exePath);

	BOOLEAN res = InitialiseCTC();

	while(!end_CTCStatus)
	{
		if(res == FALSE)
		{
			o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus.lbl_Title_YN.Text=ERROR ITE NOT INSTALLED");
			Sleep(15000);
			continue;
		}

		int module = DSPModule;

		
		//RFAZZI_FI04/06/2009_1. Bug Fix showing the CTC page. Now the Page is cleared only loading it. 
		//while (ValidModuleCTC(module) == NO)
		//{
		//	module = DSPModule;
		//	o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._clearCTCStatus()");
		//	::Sleep(500);
		//}


    //RFAZZI_FI20/04/2011_1. Apportate modifiche ad ITE per Windows7. 
		if( TryLinkToITE(NULL, NULL, NULL,FALSE) == OK )
		{
			int	ModuleAlarm = ModuleAlarmDIO(module);
			o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._setErrorCTCStatus(b[%s])", String::Bool2Alpha((StatusAlarm(ModuleAlarm) == ALARM_SET)));
		
			for (int i = 0; i < COUNTERS_PER_DSP_CTC; i++)
			{
				int index = module * COUNTERS_PER_DSP_CTC + i;
				if ( dspCTC->Counter[index].Type != NOT_USED_CTC)
				{
					int CounterId = ID_CTC(index);
					if (VALID_CTC(CounterId) == FALSE)
					{
						o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._setCTCStatus(i[%d],i[0],s[ ],s[ ],s[ ],s[ ],s[ ],s[ ],s[ ],s[ ])", i);
						continue;
					}

				struct CounterType *Counter = &dspCTC->Counter[ INDEX_CTC(CounterId) ];

				switch (Counter->Type)
					{
					case COUNTDOWN_CTC:
						break;

					//RFAZZI_FI03/06/2009_1. Allineato ITE alla versione 7v5.14.
					case REV_NO_DIV_CTC:
						o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._setCTCStatus(i[%d],s[%d],s[%d])", i,
														Counter->CountRegister, Counter->GateCountRegister);
						break;

					case REV_DIV_2_CTC:
						o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._setCTCStatus(i[%d],s[%d],s[%d])", i,
														Counter->CountRegister, Counter->GateCountRegister);
						break;

					case NOT_USED_CTC:
						break;

					//RFAZZI_FI03/06/2009_1. Allineato ITE alla versione 7v5.14.
					case SPEED_HF_CTC:
						o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._setCTCStatus(i[%d],s[%d],s[ ])", i,
														Counter->CountRegister);
						break;

					//RFAZZI_FI03/06/2009_1. Allineato ITE alla versione 7v5.14.
					case SPEED_LF_CTC:
						o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._setCTCStatus(i[%d],s[%d],s[ ])", i,
														Counter->CountRegister);
						break;

					case DIVIDE_TOGGLE_CTC:
						break;

					case LENGTH_CTC:
						o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._setCTCStatus(i[%d],s[%d],s[ ])", i,
														Counter->CountRegister);
						break;

					case FRT_CTC:
						o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._setCTCStatus(i[%d],s[%d],s[ ])", i,
														Counter->CountRegister);
						break;

          //RFAZZI_FI21/09/2011_1. Bug Fix su visualizzazione nella pagina ingegnerisca CTC dei contatori di tipo KNIFE dopo aggiornamento all'ultima versione di ITE 7v6.16
					//case KNIFE_GRP_CTC:
					//	o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._setCTCStatus(i[%d],s[%d],s[%d])", i,
					//									Counter->CountRegister, Counter->GateCountRegister);
					//	break;
					case KNIFE_NO_DIV_CTC:
						o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._setCTCStatus(i[%d],s[%d],s[%d])", i,
														Counter->CountRegister, Counter->GateCountRegister);
						break;

					case KNIFE_DIV_2_CTC:
						o_socket.Send(Network::MSG_PAG_REF,L"settings_CTCStatus._setCTCStatus(i[%d],s[%d],s[%d])", i,
														Counter->CountRegister, Counter->GateCountRegister);
						break;

					default:
						logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"FormatCounterDiagsCTC - Unknown counter type %X (%08X)", Counter->Type, CounterId );
						break;
					}//Switch
				}//IF
			} //For
      Sleep(500);
		}//IF LINK ITE
		else
		{
			logger.Warning(__FUNCTION__, L"TryLinkToITE - Failed ... try again in 1,5 sec");
			Sleep(1500);
		}
	}//while

	logger.Fine(__FUNCTION__, L"Terminated settings_CTCStatus page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SettingsGroup::AIOStatus(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"settings_AIOStatus._Show()");
	char exePath[MAX_PATH];

	GetModuleFileNameA(NULL,exePath,MAX_PATH);
	//Store the runDirectory
	*strrchr(exePath,'\\') = 0;
	F_SetRunDirectory(exePath);

	BOOLEAN res = InitialiseAIO();
	
	if (res==TRUE)
	{
		o_socket.Send(Network::MSG_PAG_REF,L"settings_AIOStatus.lbl_Title_YN.Text=Analogue I/O");
		for (int i = 0; i < AIO->ActiveChannels; i++)
		{
			int ChannelId = ChannelIndexIdAIO(i);
			if (AIO->Channel[i].Type != NOT_USED_AIO)
			{
				o_socket.Send(Network::MSG_PAG_REF,L"settings_AIOStatus._setAIOStatus(i[%d],i[%d],s[%S],s[%S],s[%5.2f])", i,
					AIO->Channel[i].Number, AIO->Channel[i].Name, AIO->Channel[i].TypeText, AIO->Channel[i].Value);
			}
			else
					o_socket.Send(Network::MSG_PAG_REF,L"settings_AIOStatus._clearAIOStatus(i[%d])", i);
		}
		
		while(!end_AIOStatus)
		{
			for (int i = 0; i < AIO->ActiveChannels; i++)
			{
				int ChannelId = ChannelIndexIdAIO(i);
				if (AIO->Channel[i].Type != NOT_USED_AIO)
				{
					o_socket.Send(Network::MSG_PAG_REF,L"settings_AIOStatus._setAIOStatus(i[%d],s[%5.2f])", i, AIO->Channel[i].Value);
				}
			}
			Sleep(500);
		}
	}
	else
	{
		o_socket.Send(Network::MSG_PAG_REF,L"settings_AIOStatus.lbl_Title_YN.Text=Analogue I/O in ERROR");
		while(!end_AIOStatus)
		{
			Sleep(500);
		}
	}

	logger.Fine(__FUNCTION__, L"Terminated settings_AIOStatus page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SettingsGroup::Flute(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"settings_flute._Show()");

	while(!end_Flute)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated settings_flute page threads");

	return ERROR_SUCCESS;
}
DWORD WINAPI C_SettingsGroup::Languages(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"settings_languages._Show()");

	while(!end_Languages)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated settings_languages page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SettingsGroup::LineConfig(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"settings_lineConfig._Show()");
	
	while(!end_LineConfig)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated settings_lineConfig page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SettingsGroup::DownTimeReasons(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"settings_downTimeReasons._Show()");
	
	while(!end_DownTimeReasons)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated settings_downTimeReasons page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SettingsGroup::SimulationGeneral(LPVOID  lpParam)
{
	State::SimulationState simState;
	
	const wchar_t* formName=L"settings_SimulationGeneral";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	while(!end_SimulationGeneral)
	{
		simState=C_PageManager::SMState.getSimulationState();
		o_socket.Send(Network::MSG_PAG_REF,L"%s._SetSimulation(b[%s])", formName, String::Bool2Alpha(simState.getSimSpeed()));
		if (simState.getSimSpeed())
			o_socket.Send(Network::MSG_PAG_REF,L"%s.tnud_Speed.Value=%d", formName, simState.getSimSpeed_UVmm_m()/1000);
		if (simState.getSimSpeed())
			o_socket.Send(Network::MSG_PAG_REF,L"%s.tnud_CutsPerSecond.Value=%d", formName, simState.getCutPerSec());
		if (simState.getSimSpeedZone1())
			o_socket.Send(Network::MSG_PAG_REF,L"%s.tnud_SF1Speed.Value=%d", formName, simState.getSimSpeedZone1_UVmm_m()/1000);
		if (simState.getSimSpeedZone2())
			o_socket.Send(Network::MSG_PAG_REF,L"%s.tnud_SF2Speed.Value=%d", formName, simState.getSimSpeedZone2_UVmm_m()/1000);
		if (simState.getSimSpeedZone3())
			o_socket.Send(Network::MSG_PAG_REF,L"%s.tnud_SF3Speed.Value=%d", formName, simState.getSimSpeedZone3_UVmm_m()/1000);
		//CM11-19
		if ( simState.getSimSpeed() )
		{
			o_socket.Send(Network::MSG_PAG_REF,L"%s.chk_SSIsRdyChange.Checked=%s", formName,String::Bool2Alpha( simState.getSimSSIsRdyChange()) );
			o_socket.Send(Network::MSG_PAG_REF,L"%s.chk_RSHHasCut.Checked=%s", formName,String::Bool2Alpha( simState.getSimRSHHasCut()) );
			o_socket.Send(Network::MSG_PAG_REF,L"%s.chk_SSHasChanged.Checked=%s", formName,String::Bool2Alpha( simState.getSimSSHasChanged()) );
			o_socket.Send(Network::MSG_PAG_REF,L"%s.chk_KnifeHasChanged.Checked=%s", formName,String::Bool2Alpha( simState.getSimKnifeHasChanged()) );
		}
		Sleep(1000);
	}
	logger.Fine(__FUNCTION__, L"Terminated %s page threads", formName);

	return ERROR_SUCCESS;
}

DWORD WINAPI C_SettingsGroup::RestoreBackup(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF,L"settings_RestoreBackup._Show()");
	
	while(!end_RestoreBackup)
		Sleep(1000);

	logger.Fine(__FUNCTION__, L"Terminated settings_RestoreBackup page threads");

	return ERROR_SUCCESS;
}


DWORD WINAPI C_SettingsGroup::Operations(LPVOID  lpParam)
{
	const wchar_t* formName=L"settings_Operations";
	o_socket.Send(Network::MSG_PAG_REF,L"%s._Show()", formName);
	while(!end_Operations)
	{
		Sleep(1000);
	}
	logger.Fine(__FUNCTION__, L"Terminated %s page threads", formName);

	return ERROR_SUCCESS;
}