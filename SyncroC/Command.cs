
using System;

using SyncroC.Forms.Pages;

namespace SyncroC
{
	/// <summary>
	/// Provides functionalities and constants to build every kind of command to be sent 
	/// to the server tasks.
	/// </summary>
	public class Command
	{
		//separators ecc.
		/// <summary> Special character used to separate parameters into strings sent/received to/from the server./// </summary>
		public readonly static char PARAMETER_SEPARATOR = '\u241E';

		/////////////////////////     COMMANDS FOR DATABASE      ///////////////////////////
		//translation database 
		/// <summary></summary>
		public readonly static string DB_SYNCRO_CONFIG = "SyncroConfig";
		/// <summary></summary>
		public readonly static string DB_LANGUAGE = "Language";
		/// <summary></summary>
		public readonly static string DB_MESSAGE = "Message";
		/// <summary></summary>
		public readonly static string DB_ALARM = "Alarm";
		/// <summary></summary>
		public readonly static string DB_DOWNTIMEREASON = "DownTimeReason";
		/// <summary></summary>
		public readonly static string RELOAD_SETTINGS = "ReloadSettings";

		//other database
		/// <summary>String to compose commands for the Paper database.</summary>
		public readonly static string DB_PAPER = "Paper";
		/// <summary>String to compose commands for the Quality database.</summary>
		public readonly static string DB_QUALITY = "Quality";
		/// <summary>String to compose commands for the Flute database.</summary>
		public readonly static string DB_FLUTE = "Flute";
		/// <summary>String to compose commands for the Scorer database.</summary>
		public readonly static string DB_SCORER = "Scorer";
		/// <summary>String to compose commands for the Order database.</summary>
		public readonly static string DB_ORDER = "Order";
		/// <summary>String to compose commands for the PastOrder database.</summary>
		public readonly static string DB_PAST_ORDER = "PastOrder";
		/// <summary>String to compose commands for the Shift database.</summary>
		public readonly static string DB_SHIFT = "Shift";
		/// <summary>String to compose commands for the Downtime database.</summary>
		public readonly static string DB_DOWNTIME = "Downtime";
    /// <summary>String to compose commands for the Downtime database.</summary>
    public readonly static string DB_CUT_TO_PATTERN = "cutToPattern";

		//database commands
		/// <summary>String to compose commands for the Paper database.</summary>
		public readonly static string DB_CMD_VER = "Ver";
		/// <summary>String to compose commands for the database.</summary>
		public readonly static string DB_CMD_ADD = "Add";
		/// <summary>String to compose commands for the database.</summary>
		public readonly static string DB_CMD_MOD = "Mod";
		/// <summary>String to compose commands for the database.</summary>
		public readonly static string DB_CMD_DEL = "Del";
		/// <summary>String to compose commands for the database.</summary>
		public readonly static string DB_CMD_CLR = "Clr";
		/// <summary>String to compose commands for the database.</summary>
		public readonly static string DB_CMD_MOVE = "Move";
		/// <summary>String to compose commands for the database.</summary>
		public readonly static string DB_CMD_RESTORE = "Restore";
		/// <summary>String to compose commands for the database.</summary>
		public readonly static string DB_CMD_COPY = "Copy";
		/// <summary>String to compose commands for the database.</summary>
		public readonly static string DB_CMD_TOOLSDETAIL = "ToolsDetail";
		/// <summary>String to compose commands for the database.</summary>
		public readonly static string DB_CMD_SHIFT = "Shift";
		/// <summary>String to compose commands for the database.</summary>
		public readonly static string DB_CMD_STOPSHIFT = "Stop";

    //PMASSARO_FI/24/11/08 - Check Time Since Last System Restart
    /// <summary>String to compose commands for the database.</summary>
    public readonly static string DB_CMD_SHUTDOWN_AND_RESTART_PC = "RestartPC";
    /// <summary>String to compose commands for the database.</summary>
    public readonly static string DB_CMD_SHUTDOWN_PC = "ShutDownPC";
    //agrassi_FI11-11-2010_1
    public readonly static string DB_CMD_SWAP_ORDERS = "SwapOrders";

		//dummy pages
		public readonly static string FRM_WEBDIRECTOR_MACHINESETTINGS = "webDirector_machineSettings";
		public readonly static string FRM_ROTARY_MACHINESETTINGS = "rotary_machineSettings";


		/////////////////////////     OTHER COMMANDS       ///////////////////////////

		public readonly static string SYNCRO = "syncro";
		public readonly static string WEC = "TaskWec";
		public readonly static string AOC = "aoc";
		public readonly static string ENCODERS = "encoders";
		//RFazzi_FI28/09/2006_2.Added Office LineConfig section
		public readonly static string OFFICE = "office";
		//RFazzi_FI29/09/2006_1.Moved the Printers Settings into LineConfig section
		public readonly static string PRINTERS = "printers";
		public readonly static string PRC = "processControl";


		/// <summary>String to compose commands for the LineConfig.</summary>
		public readonly static string CONF_LINE = "lineConfig";
		/// <summary></summary>
		public readonly static string STACKER = "stacker";
		/// <summary></summary>
		public readonly static string BRIDGE = "bridge";
		/// <summary></summary>
		public readonly static string SINGLE_FACER = "SINGLEFACER";
		/// <summary></summary>
		public readonly static string SPLICER = "Splicer";
		/// <summary></summary>
		public readonly static string KNIFE = "Knife";
		/// <summary></summary>
		public readonly static string EXTERNAL_WEC = "ExternWEC";
		/// <summary></summary>
		public readonly static string SLITTER_SCORER = "SlitterScorer";
		/// <summary></summary>
		public readonly static string CONF_CONFIG = "SendConfiguration";
		/// <summary></summary>
		public readonly static string CONF_PLUNGES = "SendCalibration";
		/// <summary></summary>
		public readonly static string TOGGLE_CALIBRATION_MODE = "Calibration";
		/// <summary></summary>
		public readonly static string CONF_PLUNGES_GET_ANGLE = "GetCalibrationAngle";
		/// <summary></summary>
		public readonly static string CONF_PLUNGES_GET_DIST = "GetCalibrationDist";
		/// <summary></summary>
		public readonly static string CONF_LEVEL_COUNT = "SendLevelCount";
		/// <summary></summary>
		public readonly static string CONF_MEASURE_WHEEL = "SendMeasureWheel";
		/// <summary></summary>
		public readonly static string SETUP_DEBUG = "SendSetupDebug";
		/// <summary></summary>
		public readonly static string SIMULATE_PLC = "ToggleSimulatePLC";
		/// <summary></summary>
		public readonly static string SET_OUT_CH = "SetOutputChannel";
		/// <summary></summary>
		public readonly static string TEST_IO = "TestIO";
		/// <summary></summary>
		public readonly static string GRIND_SLIT = "GrindSlit";
		/// <summary></summary>
		public readonly static string LUBE_SLIT = "LubeSlit";
		/// <summary></summary>
		public readonly static string RESET_SLIT_COUNTERS = "ResetSlitCounters";
		/// <summary></summary>
		public readonly static string CLEANCUT_DATA = "SendCleanCutData";
		/// <summary></summary>
		public readonly static string BROKEN_SLITS = "BrokenSlitsChange";
		/// <summary>Name of the IncDecButton method that change the visual feedback</summary>
		public readonly static string INCDEC_BUTTON_SIGNAL = "_signal";
		/// <summary></summary>
		public readonly static string MOD_WEB_DIR = "ModifyWebDirector";
		/// <summary></summary>
		public readonly static string REMOTE_CONTROL = "RemoteControl";


		/////////////////////////     COMMANDS FOR SYNCRO SERVER MODULES       ///////////////////////////
		/// <summary>String to compose commands for the RTCManager</summary>
		public readonly static string RTC_MANAGER = "RTCManager";
		/// <summary></summary>
		public readonly static string INCREASE = "Increase";
		/// <summary></summary>
		public readonly static string DECREASE = "Decrease";
		/// <summary></summary>
		public readonly static string TOGGLE_WITH_TIMEOUT = "Toggle(-1)";
		//ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
		/// <summary></summary>
		public readonly static string GO_TO_SETPOINT_WRAP = "GoToSetPointWrapArmPreHeater";
		/// <summary></summary>
		public readonly static string GO_TO_SETPOINT_GLUE_GAP = "GoToSetPointGlueGap";
		/// <summary></summary>
		public readonly static string GO_TO_SETPOINT_PAPER_TENSION = "GoToSetPointPaperTension";

    //RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea         
    /// <summary></summary>
    public readonly static string GO_TO_SETPOINT_WRAPARM_STEAM = "GoToSetPointWrapArmSteam";

		/// <summary>String to compose commands for the CommunicationManager</summary>
		public readonly static string COMMUNICATION_MANAGER = "CommunicationManager";
		/// <summary></summary>
		public readonly static string SHUTDOWN = "ShutDown";
		public readonly static string SHUTDOWNALL = "Stop(ALL)";

    //PMASSARO_FI/24/11/08 - Check Time Since Last System Restart
    public readonly static string SHUTDOWN_AND_RESTART_PC = "Stop(SHUTDOWN_AND_RESTART)";
    public readonly static string SHUTDOWN_AND_OFF_PC = "Stop(SHUTDOWN_AND_OFF)";

    //PMASSARO_FI03/02/09 - ShutDown From Operator Page
    public readonly static string SHUTDOWN_AND_RESTART_PC_BY_OPERATOR = "Stop(SHUTDOWN_AND_RESTART_PC_BY_OPERATOR)";
    public readonly static string SHUTDOWN_AND_OFF_PC_BY_OPERATOR = "Stop(SHUTDOWN_AND_OFF_PC_BY_OPERATOR)";

    //graffaelli_FI07/11/2011_1 - Added align second display on desktop command
    public readonly static string ALIGN_SECOND_DISPLAY_ON_DESKTOP = "SecondClientAlign()";

    //PMASSARO_FI10/02/09 - Try To Restart Failed Task
    public readonly static string FORCE_TASKS_RESTART = "ForceRestartTask(RestartTasks)";

		/// <summary>String to compose commands for the DBManager (not to a specific database of it).</summary>
		public readonly static string DB_MANAGER = "DBManager";
		/// <summary></summary>
		public readonly static string DB_ALIGNMENT = "Alignment";

		/// <summary>String to compose commands for the PageManager</summary>
		public readonly static string PAGE_MANAGER = "PageManager";
		/// <summary></summary>
		public readonly static string SET_DIO_MODULE = "SetDIOModule";
		/// <summary></summary>
		public readonly static string SET_CTC_MODULE = "SetCTCModule";

		/// <summary>String to compose commands for the Starter</summary>
		public readonly static string STARTER = "starter";
		/// <summary></summary>
		public readonly static string PULSE_DIGOUT = "PulseDigiout";
		/// <summary></summary>
		public readonly static string SET_DIGOUT = "SetDigiout";
		/// <summary></summary>
		public readonly static string CLEAR_DIGOUT = "ClearDigiout";
		/// <summary></summary>
		public readonly static string TOGGLE_AUTO_MAN = "ToggleAutoMan";
		public readonly static string TOGGLE_CONFIRM_SPLICE = "ToggleConfirmSplice";
		public readonly static string BACKUP_EXTENSION = ".BackupForSimulation";
	}
}
