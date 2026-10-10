#include ".\C_SimulationCommands.h"
#include "..\C_RTCManager.h"

//Socket
#include "..\..\Include\NameSpace\Network.h"
extern Network::C_SocketTCP o_socket;
extern Logger::C_Logger logger;
extern Logger::C_Logger logConfig;

C_SimulationCommands::C_SimulationCommands(void)
{

}

C_SimulationCommands::~C_SimulationCommands(void)
{
}

bool C_SimulationCommands::ManageCommand(String::StrType simulationCommand)
{
	NamedPipe::CommandPipe debugCommand;	
	debugCommand.Clean();
	debugCommand.addressee=NamedPipe::CommandPipe::Starter;
	String::StrType command = simulationCommand.extractCommand();
	if (command.isEmpty())
		return false;
	if(command.CompareNoCase(L"Simulation"))
	{
		//Controllare che la linea non sia in marcia e altre condizioni
		debugCommand.command.starterCommand.cmd = NamedPipe::StarterCommand::Simulation;
		std::vector<String::StrType> vect = simulationCommand.Tokenize(L",");
		debugCommand.command.starterCommand.dataInt[0] = vect[0].ToInt()*1000; //Speed m/min -> mm/min
		debugCommand.command.starterCommand.dataInt[1] = vect[1].ToInt(); //Cut per seconds
		debugCommand.command.starterCommand.dataInt[2] = vect[2].ToInt()*1000; //Speed Sf1 m/min -> mm/min
		debugCommand.command.starterCommand.dataInt[3] = vect[3].ToInt()*1000; //Speed Sf2 m/min -> mm/min
		debugCommand.command.starterCommand.dataInt[4] = vect[4].ToInt()*1000; //Speed Sf3 m/min -> mm/min
		//CM11-19
		debugCommand.command.starterCommand.dataInt[5] = vect[5].ToInt(); //SS Is Rdy to Change
		debugCommand.command.starterCommand.dataInt[6] = vect[6].ToInt(); //RSH Has Cut emulated signal
		debugCommand.command.starterCommand.dataInt[7] = vect[7].ToInt(); //SS Has Changed emulated signal
		debugCommand.command.starterCommand.dataInt[8] = vect[8].ToInt(); //Knife Has Changed emulated signal


		C_RTCManager::RTCSyncroS.Write(&debugCommand);
		return true;
	}
	logger.Warning(__FUNCTION__, Logger::PRE_SOCK_ERROR, L"Web Director wrong command: %s", command);
	return false;
}