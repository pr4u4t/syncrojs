#include ".\C_WetEndSupervisorGroup.h"

//Logger include
#include "..\..\Shared\C_Logger.h"
extern Logger::C_Logger logger;

//SocketTCP Include
#include "..\..\Include\NameSpace\Network.h"
//Socket
extern Network::C_SocketTCP o_socket;

//Page Management
#include "..\C_PageManager.h"

bool C_WetEndSupervisorGroup::end_Page01;
bool C_WetEndSupervisorGroup::end_Page02;
bool C_WetEndSupervisorGroup::end_Page03;

C_WetEndSupervisorGroup::C_WetEndSupervisorGroup(void)
{
	pid_WetEndSupervisorPage=NULL;
	TerminateThread(false);
}

C_WetEndSupervisorGroup::~C_WetEndSupervisorGroup(void)
{
}

bool C_WetEndSupervisorGroup::ChooseWetEndSupervisorGroupPage(String::StrType thePageRequest)
{
	//page01
	if(thePageRequest.CompareNoCase(L"01"))
	{
		end_Page01 = false;
		pid_WetEndSupervisorPage=::CreateThread(NULL,0,C_WetEndSupervisorGroup::Page01,NULL,0,NULL);
		return true;
	}
	//page02
	if(thePageRequest.CompareNoCase(L"02"))
	{
		end_Page02 = false;
		pid_WetEndSupervisorPage=::CreateThread(NULL,0,C_WetEndSupervisorGroup::Page02,NULL,0,NULL);
		return true;
	}
	//page03
	if(thePageRequest.CompareNoCase(L"03"))
	{
		end_Page03 = false;
		pid_WetEndSupervisorPage=::CreateThread(NULL,0,C_WetEndSupervisorGroup::Page03,NULL,0,NULL);
		return true;
	}
	
	return false;
}

void C_WetEndSupervisorGroup::TerminateThread(const bool terminateThread)
{
	if(terminateThread)
	{
		if(pid_WetEndSupervisorPage!=NULL)
		{
			BOOL res = ::TerminateThread(pid_WetEndSupervisorPage,NULL);
			if(res == FALSE)
				logger.Severe(__FUNCTION__, L"Error cannot close thread WetEndSupervisorPage ... %d", ::GetLastError());
		}
	}
	else
	{
		end_Page01 = true;
		end_Page02 = true;
		end_Page03 = true;
	}
}

DWORD C_WetEndSupervisorGroup::SuspendThread(void)
{
	if(pid_WetEndSupervisorPage==NULL)
		return ERROR_SUCCESS;

	if(::SuspendThread(pid_WetEndSupervisorPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot stop Page WetEnd Supervisor Thread ... %d",error);
			return error;
		};
  
	return ERROR_SUCCESS;
}

DWORD C_WetEndSupervisorGroup::ResumeThread(void)
{
	if(pid_WetEndSupervisorPage==NULL)
		return ERROR_SUCCESS;

	if(::ResumeThread(pid_WetEndSupervisorPage)==-1)
		{
			DWORD error=GetLastError();
			logger.Severe(__FUNCTION__, Logger::PRE_NULL, L"Error: Cannot resume Page WetEnd Supervisor Thread ... %d",error);
			return error;
		}
		
	return ERROR_SUCCESS;
}

DWORD WINAPI C_WetEndSupervisorGroup::Page01(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"wetEndSupervisor_01._Show()");

	State::DoubleBackerState theDoubleBackerState;
	theDoubleBackerState.Clear();

	State::GlueUnitState theGlueUnitState;
	theGlueUnitState.Clear();

	State::PreHeatersTernState thePreHeatersTernState;
	thePreHeatersTernState.Clean();

	State::SingleFacerState theSingleFacerState; 
	theSingleFacerState.Clear();

	while(!end_Page01)	
	{
	
		//RFAZZI_FI11/12/2008_1.Added missed Double Backer WrapArm in the WetEnd Overview 1 ( Low Level Page ).
		//// DOUBLE BACKER
		o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.tnud_SpeedDoubleBacker_UVmm_m.Value=%d",C_PageManager::SMState.getZoneDoubleBackerState().Monitor.getSpeed_UVmm_m());

		theDoubleBackerState.Clear();
		C_PageManager::SMState.getDoubleBackerState(theDoubleBackerState);

		//WRAP ARM 31
		int i=31;
		State::WrapArmState theWrapArmState = theDoubleBackerState.CtrlData.getWrapArmByNumber(WrapArmNumber(i));

		if(theWrapArmState.isUsed())
		{
			o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wrapArmPreHeater%d.Visible=true",i);
			o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.Visible=true",i);
			o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.InLocal=%s",i,
				String::Bool2Alpha(theWrapArmState.getIsWrapArmLocal()));
			o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.Auto=%s",i,
				String::Bool2Alpha(theWrapArmState.getIsWrapArmAuto()));
			o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.IsUnder=%s",i,
				String::Bool2Alpha(theWrapArmState.getIsWrapArmUnder()));
			o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.Position=%d",i,
				theWrapArmState.getPosition_UPercINT().getValue());
			o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.IsMaxPos=%s",i,
				String::Bool2Alpha(theWrapArmState.getIsMaxPos()));
			o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.IsMinPos=%s",i,
				String::Bool2Alpha(theWrapArmState.getIsMinPos()));
              
      if(C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(i).getEnable())
          o_socket.Send(Network::MSG_PAG_REF, L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.Temperature=%d", i, C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(i).getValue());
		}

		//o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.tnud_SpeedGlueUnit_UVmm_m.Value=%d",C_PageManager::SMState.getZoneDoubleBackerState().Monitor.getSpeed_UVmm_m());

	//// GLUE UNIT

		theGlueUnitState.Clear();
		C_PageManager::SMState.getGlueUnitState(theGlueUnitState);

		//GLUE GAP 1, 2, 3 , 4 
		for(int i=1; i<=4 ; i++)
		{
			GlueGapNumber theGlueGapNumber = GlueGapNumber(i);
			State::GlueGapState theGlueGapState = theGlueUnitState.CtrlData.getGlueGapByNumber(theGlueGapNumber);

			if(theGlueGapState.isUsed())
			{
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.gg_glueGap%d.Visible=true",theGlueGapNumber);
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.gg_glueGap%d.InLocal=%s",theGlueGapNumber,
					String::Bool2Alpha(theGlueGapState.getIsGlueGapLocal()));
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.gg_glueGap%d.Auto=%s",theGlueGapNumber,
					String::Bool2Alpha(theGlueGapState.getIsGlueGapAuto()));
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.gg_glueGap%d.Position=%d",theGlueGapNumber,
					theGlueGapState.getPositionINT().getValue());
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.gg_glueGap%d.IsMaxPos=%s",theGlueGapNumber,
					String::Bool2Alpha(theGlueGapState.getIsMaxPos()));
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.gg_glueGap%d.IsMinPos=%s",theGlueGapNumber,
					String::Bool2Alpha(theGlueGapState.getIsMinPos()));
			}
		}

	//// PRE HETERS TERN
		thePreHeatersTernState.Clean();
		C_PageManager::SMState.getPreHeatersTernState(thePreHeatersTernState);
		for(int i=0; i<8 ; i++)
		{
			WrapArmNumber theWrapArmNumber;
			switch (i)
			{
			//WRAP ARM 11
			case 0:
				theWrapArmNumber = S1_1;
				break;
			//WRAP ARM 1
			case 1:
				theWrapArmNumber = B1_PH;
				break;
			//WRAP ARM 2
			case 2:
				theWrapArmNumber = B2_PH;
				break;
			//WRAP ARM 3
			case 3:
				theWrapArmNumber = B3_PH;
				break;
			//WRAP ARM 61
			case 4:
				theWrapArmNumber = S1_6;
				break;
			//WRAP ARM 10
			case 5:
				theWrapArmNumber = B1_PHO;
				break;
			//WRAP ARM 20
			case 6:
				theWrapArmNumber = B2_PHO;
				break;
			//WRAP ARM 30
			case 7:
				theWrapArmNumber = B3_PHO;
				break;
			
			//WRAP ARM 1
			default:
				theWrapArmNumber = S1_1;
				break;
			}

			State::WrapArmState theWrapArmState = thePreHeatersTernState.CtrlData.getWrapArmByNumber(theWrapArmNumber);
			if(theWrapArmState.isUsed())
			{
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.Visible=true",theWrapArmNumber);
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.InLocal=%s",theWrapArmNumber,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmLocal()));
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.Auto=%s",theWrapArmNumber,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmAuto()));
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.IsUnder=%s",theWrapArmNumber,
					String::Bool2Alpha(theWrapArmState.getIsWrapArmUnder()));
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.Position=%d",theWrapArmNumber,
					theWrapArmState.getPosition_UPercINT().getValue());
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.IsMaxPos=%s",theWrapArmNumber,
					String::Bool2Alpha(theWrapArmState.getIsMaxPos()));
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.IsMinPos=%s",theWrapArmNumber,
					String::Bool2Alpha(theWrapArmState.getIsMinPos()));
              
        if(C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(theWrapArmNumber).getEnable())
          o_socket.Send(Network::MSG_PAG_REF, L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.Temperature=%d", theWrapArmNumber, C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(theWrapArmNumber).getValue());

			}
		}

    //RFAZZI_FI23/08/2011_1. Aggiunta la terza zona in WetEndSupervisor01. 
	//// SINGLE FACER [1,2,3]
		//for(int index=1; index<3 ; index++)
		for(int index=1; index<=SYNCRO_MAX_SFS ; index++)
		{
			theSingleFacerState.Clear();
			C_PageManager::SMState.getSingleFacerState(theSingleFacerState,index);

			o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.tnud_SpeedSF%d_UVmm_m.Value=%d", index, C_PageManager::SMState.getZoneSingleFacerState(index).Monitor.getSpeed_UVmm_m());
			
			//12,13,14,15
			int wrapFrom = 12;
			int wrapTo = 13;
			if(index==2)
			{
				wrapFrom = 14;
				wrapTo = 15;
			}
      else if (index == 3)
      {
				wrapFrom = 16;
				wrapTo = 17;
      }

			for(int i = wrapFrom; i<=wrapTo ; i=i++)
			{
				WrapArmNumber theWrapArmNumber = WrapArmNumber(i);

				State::WrapArmState theWrapArmState = theSingleFacerState.CtrlData.getWrapArmByNumber(theWrapArmNumber);

				if(theWrapArmState.isUsed())
				{
					o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.Visible=true",theWrapArmNumber);
					o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.InLocal=%s",theWrapArmNumber,
						String::Bool2Alpha(theWrapArmState.getIsWrapArmLocal()));
					o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.Auto=%s",theWrapArmNumber,
						String::Bool2Alpha(theWrapArmState.getIsWrapArmAuto()));
					o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.IsUnder=%s",theWrapArmNumber,
						String::Bool2Alpha(theWrapArmState.getIsWrapArmUnder()));
					o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.Position=%d",theWrapArmNumber,
						theWrapArmState.getPosition_UPercINT().getValue());
					o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.IsMaxPos=%s",theWrapArmNumber,
						String::Bool2Alpha(theWrapArmState.getIsMaxPos()));
					o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.IsMinPos=%s",theWrapArmNumber,
						String::Bool2Alpha(theWrapArmState.getIsMinPos()));
                
          if(C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(theWrapArmNumber).getEnable())
            o_socket.Send(Network::MSG_PAG_REF, L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.Temperature=%d", theWrapArmNumber, C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(theWrapArmNumber).getValue());

				}
			}
			wrapFrom = wrapFrom + 10;
			wrapTo = wrapTo + 10;
			//22,23,24,25
			for(int i = wrapFrom; i<=wrapTo ; i=i++)
			{
				WrapArmNumber theWrapArmNumber = WrapArmNumber(i);

				State::WrapArmState theWrapArmState = theSingleFacerState.CtrlData.getWrapArmByNumber(theWrapArmNumber);

				if(theWrapArmState.isUsed())
				{
					o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.Visible=true",theWrapArmNumber);
					o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.InLocal=%s",theWrapArmNumber,
						String::Bool2Alpha(theWrapArmState.getIsWrapArmLocal()));
					o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.Auto=%s",theWrapArmNumber,
						String::Bool2Alpha(theWrapArmState.getIsWrapArmAuto()));
					o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.IsUnder=%s",theWrapArmNumber,
						String::Bool2Alpha(theWrapArmState.getIsWrapArmUnder()));
					o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.Position=%d",theWrapArmNumber,
						theWrapArmState.getPosition_UPercINT().getValue());
					o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.IsMaxPos=%s",theWrapArmNumber,
						String::Bool2Alpha(theWrapArmState.getIsMaxPos()));
					o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.IsMinPos=%s",theWrapArmNumber,
						String::Bool2Alpha(theWrapArmState.getIsMinPos()));
                    
          if(C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(theWrapArmNumber).getEnable())
            o_socket.Send(Network::MSG_PAG_REF, L"wetEndSupervisor_01.wa_wrapArmPreHeater%d.Temperature=%d", theWrapArmNumber, C_PageManager::SMState.getTemperature_MoistureSensorState().WRAP_ARM_getTemperatureSensorValue_UGc(theWrapArmNumber).getValue());

				}
			}


		//// GLUE GAP [11, 21]
			State::GlueGapState theGlueGapState = theSingleFacerState.CtrlData.gluegap;
			if(theGlueGapState.isUsed())
			{
				int moveGlueGap = 1 + (10 * (index));
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.gg_glueGap%d.Visible=true",moveGlueGap);
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.gg_glueGap%d.InLocal=%s",moveGlueGap,
					String::Bool2Alpha(theGlueGapState.getIsGlueGapLocal()));
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.gg_glueGap%d.Auto=%s",moveGlueGap,
					String::Bool2Alpha(theGlueGapState.getIsGlueGapAuto()));
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.gg_glueGap%d.Position=%d",moveGlueGap,
					theGlueGapState.getPositionINT().getValue());
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.gg_glueGap%d.IsMaxPos=%s",moveGlueGap,
					String::Bool2Alpha(theGlueGapState.getIsMaxPos()));
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_01.gg_glueGap%d.IsMinPos=%s",moveGlueGap,
					String::Bool2Alpha(theGlueGapState.getIsMinPos()));
			}
		}

		Sleep(1000);
	}

	logger.Fine(__FUNCTION__, L"Terminated wetEndSupervisor_01 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_WetEndSupervisorGroup::Page02(LPVOID  lpParam)
{
	o_socket.Send(Network::MSG_PAG_REF, L"wetEndSupervisor_02._Show()");

	State::DoubleBackerState theDoubleBackerState;
	theDoubleBackerState.Clear();

	State::BridgeState theBridgeState;
	theBridgeState.Clear();

	State::SplicerState theSplicerState;
	theSplicerState.Clear();

	State::SingleFacerState theSingleFacerState;

	while(!end_Page02)

	{
		//RFAZZI_FI29/01/2009_1. Added new LowLevel WetEnd Page showing the Paper Tension Configured. 
		//// DOUBLE BACKER
		o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_02.tnud_SpeedDoubleBacker_UVmm_m.Value=%d",C_PageManager::SMState.getZoneDoubleBackerState().Monitor.getSpeed_UVmm_m());


		//// BRIDGE
		for(int i=1; i<=3 ; i++)
		{
			theBridgeState.Clear();
			C_PageManager::SMState.getBridgeState(theBridgeState,i);

			if (theBridgeState.Monitor.paperTensionState.getPaperTension_UFkgxm_s2().getEnable() )
			{
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_02.pt_paperTension%d.Visible=true",i);
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_02.pt_paperTension%d.Position=%d",i,
					(int)theBridgeState.Monitor.paperTensionState.getPaperTension_UFkgxm_s2().getValue());

			}
		}

		//SPLICER
		for ( int i=1;i<=5;i++)
		{
			theSplicerState.Clear();
			C_PageManager::SMState.getSplicerState(theSplicerState,i);
			if ( theSplicerState.CtrlData.paperTensionState.getPaperTension_UFkgxm_s2().getEnable() )
			{
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_02.pt_paperTension%d.Visible=true",i);
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_02.pt_paperTension%d.Position=%d",i,
					(int)theSplicerState.CtrlData.paperTensionState.getPaperTension_UFkgxm_s2().getValue());
		    o_socket.Send(Network::MSG_PAG_REF, L"wetEndSupervisor_02.pt_paperTension%d.InLocal=%s",i,
			    String::Bool2Alpha(!C_PageManager::SMState.getSplicerState(i).CtrlData.paperTensionState.getIsPaperTensionRemote().getValue()));
		    o_socket.Send(Network::MSG_PAG_REF, L"wetEndSupervisor_02.pt_paperTension%d.Auto=%s",i,
			    String::Bool2Alpha(C_PageManager::SMState.getSplicerState(i).CtrlData.paperTensionState.getIsPaperTensionAuto().getValue()));

			}
		}

	//// SINGLE FACER [1,2]
		for(int index=1; index<3 ; index++)
		{
			int paperTensionNumber = index + B1_PT - 1;
			theSingleFacerState.Clear();
			C_PageManager::SMState.getSingleFacerState(theSingleFacerState,index);

			o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_02.tnud_SpeedSF%d_UVmm_m.Value=%d", index, C_PageManager::SMState.getZoneSingleFacerState(index).Monitor.getSpeed_UVmm_m());

			if ( C_PageManager::SMState.getBridgeState(index).Monitor.paperTensionState.getPaperTension_UFkgxm_s2().getEnable() )
			{
				o_socket.Send(Network::MSG_PAG_REF,L"wetEndSupervisor_02.pt_paperTension%d.Visible=true",paperTensionNumber);
				o_socket.Send(Network::MSG_PAG_REF, L"wetEndSupervisor_02.pt_paperTension%d.InLocal=%s",paperTensionNumber,
					String::Bool2Alpha(!C_PageManager::SMState.getBridgeState(index).Monitor.paperTensionState.getIsPaperTensionRemote().getValue()));
				o_socket.Send(Network::MSG_PAG_REF, L"wetEndSupervisor_02.pt_paperTension%d.Auto=%s",paperTensionNumber,
					String::Bool2Alpha(C_PageManager::SMState.getBridgeState(index).Monitor.paperTensionState.getIsPaperTensionAuto().getValue()));
			}
		}

		Sleep(1000);
	}
	logger.Fine(__FUNCTION__, L"Terminated wetEndSupervisor_02 page threads");

	return ERROR_SUCCESS;
}

DWORD WINAPI C_WetEndSupervisorGroup::Page03(LPVOID  lpParam)
{
  o_socket.Send(Network::MSG_PAG_REF, L"wetEndSupervisor_03._Show()");

	while(!end_Page03)
  {
		Sleep(1000);
  }

	logger.Fine(__FUNCTION__, L"Terminated wetEndSupervisor_03 page threads");

	return ERROR_SUCCESS;
}
