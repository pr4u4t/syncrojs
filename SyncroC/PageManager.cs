//agrassi_FI19-08-2010_1
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Net.NetworkInformation;

using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.Controls;
using SyncroC.Forms;
using SyncroC.Forms.Pages;
using SyncroC.Forms.PopUp;
using SyncroC.Forms.StatusBar;
using SyncroC.Forms.LateralBar;
using SyncroC.Forms.Pages.Statistics;
using SyncroC.Forms.Pages.Statistics.Orders;
using SyncroC.Forms.Pages.Statistics.Rolls;
using SyncroC.Forms.Pages.Production;
using SyncroC.Forms.Pages.Production.Home;
using SyncroC.Forms.Pages.Production.OrdersIns;
using SyncroC.Forms.Pages.Production.OrdersList;
using SyncroC.Forms.Pages.Production.Alarms;
//RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
using SyncroC.Forms.Pages.Production.Warnings;

using SyncroC.Forms.Pages.Production.Operator;
using SyncroC.Forms.Pages.Settings;
using SyncroC.Forms.Pages.Settings.Simulation;
using SyncroC.Forms.Pages.SlitterScorer;
using SyncroC.Forms.Pages.SlitterScorer.Alarms;
using SyncroC.Forms.Pages.SlitterScorer.Settings;
//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
using SyncroC.Forms.Pages.SlitterScorer.Status;
using SyncroC.Forms.Pages.SlitterScorer.Tools;
using SyncroC.Forms.Pages.Shear;
using SyncroC.Forms.Pages.Shear.Settings;
using SyncroC.Forms.Pages.Shear.Alarms;
using SyncroC.Forms.Pages.Twin400.Orders;
using SyncroC.Forms.Pages.Twin400.Tools;
using SyncroC.Forms.Pages.Twin400.Flutes;
using SyncroC.Forms.Pages.Twin400.Alarms;
//RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
using SyncroC.Forms.Pages.Twin400.Warnings;
using SyncroC.Forms.Pages.Twin400.Settings;
using SyncroC.Forms.Pages.Twin400.Maintenance;
using SyncroC.Forms.Pages.Twin400.Status;
using SyncroC.Forms.Pages.DoubleBacker;
using SyncroC.Forms.Pages.DoubleBacker.Status;
using SyncroC.Forms.Pages.DoubleBacker.Console;
using SyncroC.Forms.Pages.DoubleBacker.Alarms;
using SyncroC.Forms.Pages.DoubleBacker.Settings;
using SyncroC.Forms.Pages.GlueUnit;
using SyncroC.Forms.Pages.GlueUnit.Alarms;
using SyncroC.Forms.Pages.GlueUnit.Console;
using SyncroC.Forms.Pages.GlueUnit.Settings;
using SyncroC.Forms.Pages.PreHeaterTern;
using SyncroC.Forms.Pages.PreHeaterTern.Alarms;
using SyncroC.Forms.Pages.PreHeaterTern.Settings;
using SyncroC.Forms.Pages.Debug;
using SyncroC.Forms.Pages.Debug.Twin400;
using SyncroC.Forms.Pages.Knife;
using SyncroC.Forms.Pages.Knife.Alarms;
using SyncroC.Forms.Pages.Knife.Settings;
using SyncroC.Forms.Pages.Stacker;
using SyncroC.Forms.Pages.Stacker.Status;
using SyncroC.Forms.Pages.Stacker.Alarms;
using SyncroC.Forms.Pages.Stacker.Settings;
using SyncroC.Forms.Pages.DryEndSupervisor;
using SyncroC.Forms.Pages.WetEndSupervisor;
using SyncroC.Forms.Pages.SyncroSystem.AOC_WEC;
using SyncroC.Forms.Pages.SyncroSystem.SPEED;
using SyncroC.Forms.Pages.SyncroSystem.SPEED_Reference;
using SyncroC.Forms.Pages.SyncroSystem.SYNCRO;
using SyncroC.Forms.Pages.SyncroSystem.PRINTER;
using SyncroC.Forms.Pages.SyncroSystem;
using SyncroC.Forms.Pages.SingleFacer;
using SyncroC.Forms.Pages.SingleFacer.Alarms;
using SyncroC.Forms.Pages.SingleFacer.Settings;
using SyncroC.Forms.Pages.SingleFacer.Console;
using SyncroC.Forms.Pages.SingleFacer.Status;
using SyncroC.Forms.Pages.Splicer;
using SyncroC.Forms.Pages.Splicer.Alarms;
using SyncroC.Forms.Pages.Splicer.Settings;
using SyncroC.Forms.Pages.SyncroSystem.OFFICE;
using SyncroC.Forms.Pages.SyncroSystem.LOG;

using System.Net;
using System.Net.Sockets;
using System.IO;

namespace SyncroC
{
    

	//Delegates
	/// <summary>
	/// Delegate with NO parameters used to call the invoke function on the UI thread (User Interface thread).
	/// </summary>
	public delegate void UIMethod0();
	/// <summary>
	/// Delegate with ONE parameter (string) used to call the invoke function on the UI thread (User Interface thread).
	/// </summary>
	public delegate void UIMethod1(string msg);
	/// <summary>
	/// Delegate with TWO parameters (string, bool) used to call the invoke function on the UI thread (User Interface thread).
	/// </summary>
	public delegate void UIMethod2(string msg, bool b);
  
  //agrassi_FI26-11-08
  public struct FosberNICData
  {
    public string Name;
    //agrassi_FI08-01-09_4
    public string ID;
    public string MACAddress;
    //agrassi_FI14-01-09_1
    //public int IndexIntoRegistry;
    public string[] IPAddresses;
    public string[] SubnetMasks;
    public string[] Gateways;
    //agrassi_FI14-01-09_1
    public string[] DNS;
    public string[] WINS;
  };

	public class PageManager : System.Windows.Forms.Form
	{

		//components
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label lbl_State;
		private System.Windows.Forms.Label lbl_OS;
		private System.Windows.Forms.Label lbl_ProductVersion;
		public System.Windows.Forms.Label lbl_Message_NY;
		public System.Windows.Forms.Label lbl_Date;
		public System.Windows.Forms.Label lbl_Time;
		//RFAZZI_FI04/02/08
    //agrassi_FI19-02-09_3
    public bool closeAll = false;
    public bool CloseAll
    { get { return this.closeAll; } }

		//timers
		/// <summary>Used to start all the application modules.</summary>
		private System.Windows.Forms.Timer timer_Start;
		/// <summary>Used to remove messages from the message bar of the pages.</summary>
		private System.Windows.Forms.Timer timer_Message;
		/// <summary>Used to periodically update the time on the pages.</summary>
		private System.Windows.Forms.Timer timer_DateTime;

		/// <summary>Needed to realize the singleton pattern.</summary>
		private static PageManager pageManager = null;

		/// <summary>
		/// thread used to execute commands received throw TCP connection from the server..
		/// </summary>
		private Thread threadCommandExec;

		//constants for string message code coming from the server
		/// <summary>
		/// Determine if the rest of a string has to be decoded and translated
		/// </summary>
		public static string PRE_MSG_CODE = "�";
		/// <summary>
		/// Into a string that has to be decoded specify a portion of the string that
		/// has not to be decoded.
		/// </summary>
		public static string PRE_MSG_ATTRIBUTE = "#";

    private string SyncroVersion;
		private string SyncroDataVersion;
		private string ITEVersion;
		//RFAZZI_FI04/02/08
    //agrassi_FI19-08-2010_1
		//private string syncroSoftwareStandard;

		/// <summary>
		/// form's cache: contains the form's name already instantiated
		/// and not yet disposed. (This cache is only for Frm_Page form type,
		/// the pop up are not cached).
		/// </summary>
		private ArrayList cache = new ArrayList(Settings.getSettings().forms.MaxCacheSize); 

		/// <summary>
		/// Reference to the currently visible StatusBar
		/// NOTE: DO NOT CHANGE THE NAME OF THIS VARIABLE (CurrentStatusBar)!!!! 
		/// (it is used as string into the method UpdateCurrentStatusBar() )
		/// </summary>
		private Frm_StatusBar CurrentStatusBar = null; //DO NOT CHANGE THE NAME OF THIS VARIABLE (CurrentStatusBar)

		//form's identifiers
		//statusBar
		private Frm_StatusBarDryEnd statusBar_statusBarDryEnd;
		private Frm_StatusBarWetEnd statusBar_statusBarWetEnd;
		private Frm_StatusBarTwin400 statusBar_statusBarTwin400;

    //LateralBar
    private Frm_LateralBar lateralBar_lateralBar;

		//production group
		private Frm_HomeDry production_homeDry;
		private Frm_HomeWet production_homeWet;
		private Frm_HomePrc production_homePrc;
		//ordersIns subgroup
		//private Frm_FullInsDryOrder production_fullInsDryOrder;
		private Frm_InsWetOrder production_insWetOrder;
		private Frm_QuickInsDryOrder production_quickInsDryOrder;
		private Frm_DryPastOrders production_dryPastOrder;
		//ordersList subgroup
		private Frm_DryOrdersList production_dryOrdersList;
		private Frm_DryPastOrdersList production_dryPastOrdersList;
		private Frm_DryWetOrdersList production_dryWetOrdersList;
		private Frm_WetOrdersList production_wetOrdersList;
		//alarms subgroup
		private Frm_Alarm1 production_alarm1;
		private Frm_Alarm2 production_alarm2;
        public Frm_HomeDry getHomeDryForm() { return production_homeDry;  }
    //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
    //warnings subgroup
    private Frm_Warning1 production_warning1;
    private Frm_Warning2 production_warning2;

    //Operator subgroup
    private Frm_Operator production_operator;
    private Frm_Printings production_printings;
    private Frm_MessagePRC production_MessagePRC;
    private Frm_Paper production_paper;
		private Frm_Quality production_quality;

		//SyncroSystem group
		//AOC subgroup
		private Frm_AOCSettings syncroSystem_AOCsettings;
		private Frm_AOCStatus syncroSystem_AOCstatus;
		private Frm_WECSettings syncroSystem_WECsettings;
		private Frm_WECStatus syncroSystem_WECstatus;

		//speed subgroup
		private Frm_SpeedSettings syncroSystem_SpeedSettings;
		private Frm_SpeedStatus syncroSystem_SpeedStatus;

		//speedref subgroup
		private Frm_SpeedReferenceSettings  syncroSystem_SpeedReferenceSettings;
		private Frm_SpeedReferenceStatus syncroSystem_SpeedReferenceStatus;

		//Syncro subgroup
		private Frm_SyncroSettings syncroSystem_SyncroSettings;
		private Frm_SyncroStatus syncroSystem_SyncroStatus;
		private Frm_SyncroTask1 syncroSystem_SyncroTask1;
		private Frm_SyncroTask2 syncroSystem_SyncroTask2;

    //Printers subgroup
    private Frm_PRINTERSSettings syncroSystem_PrintersSettings;
    private Frm_PRINTERSStatus syncroSystem_PrintersStatus;

		//RFazzi_FI28/09/2006_2.Added Office LineConfig section
		//Office subgroup
		private Frm_OFFICESettings syncroSystem_OfficeSettings;
		private Frm_OFFICEStatus syncroSystem_OfficeStatus;
		private Frm_PRCSettings syncroSystem_PrcSettings;

		private Frm_Log1 syncroSystem_Log1;
		private Frm_Log2 syncroSystem_Log2;
		private Frm_Log3 syncroSystem_Log3;
		private Frm_Log4 syncroSystem_Log4;

		//statistics group
		public Frm_ShiftReport statistics_shiftReport;
		private Frm_DowntimeReport statistics_downtimeReport;
		private Frm_StatisticsOrdersList statistics_ordersList;
		private Frm_StatisticsOrdersDetail statistics_ordersDetail;
    private Frm_StatisticsRollsList statistics_pastRollsList;
    private Frm_StatisticsRollsTrace statistics_pastRollsTrace;
    private Frm_ProductionTrace statistics_productionTrace;

    public Frm_ShiftReport getShiftReport() {
        return statistics_shiftReport;
    }

    public Frm_HomeDry getHome() {
        return production_homeDry;
    }

	public string getOperatorStatusXml()
	{
		if (this.InvokeRequired)
			return (string)this.Invoke(new Func<string>(getOperatorStatusXml));
		if (statusBar_statusBarDryEnd == null)
			return null;
		return statusBar_statusBarDryEnd.getOperatorStatusXml();
	}

		//settings group
		private Frm_Languages settings_languages;
		private Frm_Flute settings_flute;
		private Frm_LineConfig settings_lineConfig;
		private Frm_DownTimeReasons settings_downTimeReasons;
		private Frm_DIOStatus settings_DIOStatus;
		private Frm_CTCStatus settings_CTCStatus;
		private Frm_AIOStatus settings_AIOStatus;
    private Frm_RestoreBackup settings_RestoreBackup;

    //simulation subgroup
    private Frm_SimulationGeneral settings_SimulationGeneral;
    //operations
    private Frm_SyncroSystemOperations settings_Operations;

		//SingleFacer group
		private Frm_SingleFacerStatus01 singleFacer1_status01;
		private Frm_SingleFacerStatus02 singleFacer1_status02;
		private Frm_SingleFacerStatus03 singleFacer1_status03;
		private Frm_SingleFacerStatus04 singleFacer1_status04;
		private Frm_SingleFacerConsole01 singleFacer1_console01;
		private Frm_SingleFacerConsole02 singleFacer1_console02;
		private Frm_SingleFacerConsole03 singleFacer1_console03;
		private Frm_SingleFacerConsole04 singleFacer1_console04;
		private Frm_SingleFacerAlarmActive singleFacer1_alarmActive;
		private Frm_SingleFacerAlarmHistory singleFacer1_alarmHistory;
		private Frm_SingleFacerSettingsMachine singleFacer1_settingsMachine;
		private Frm_SingleFacerSettingsTask singleFacer1_settingsTask;
		private Frm_SingleFacerSettingsSFStatus singleFacer1_settingsSFStatus;
    private Frm_SingleFacerSettingsBridgeStatus singleFacer1_settingsBridgeStatus;

		private Frm_SingleFacerStatus01 singleFacer2_status01;
		private Frm_SingleFacerStatus02 singleFacer2_status02;
		private Frm_SingleFacerStatus03 singleFacer2_status03;
		private Frm_SingleFacerStatus04 singleFacer2_status04;
		private Frm_SingleFacerConsole01 singleFacer2_console01;
		private Frm_SingleFacerConsole02 singleFacer2_console02;
		private Frm_SingleFacerConsole03 singleFacer2_console03;
		private Frm_SingleFacerConsole04 singleFacer2_console04;
		private Frm_SingleFacerAlarmActive singleFacer2_alarmActive;
		private Frm_SingleFacerAlarmHistory singleFacer2_alarmHistory;
		private Frm_SingleFacerSettingsMachine singleFacer2_settingsMachine;
		private Frm_SingleFacerSettingsTask singleFacer2_settingsTask;
    private Frm_SingleFacerSettingsSFStatus singleFacer2_settingsSFStatus;
    private Frm_SingleFacerSettingsBridgeStatus singleFacer2_settingsBridgeStatus;

		private Frm_SingleFacerStatus01 singleFacer3_status01;
		private Frm_SingleFacerStatus02 singleFacer3_status02;
		private Frm_SingleFacerStatus03 singleFacer3_status03;
		private Frm_SingleFacerStatus04 singleFacer3_status04;
		private Frm_SingleFacerConsole01 singleFacer3_console01;
		private Frm_SingleFacerConsole02 singleFacer3_console02;
		private Frm_SingleFacerConsole03 singleFacer3_console03;
		private Frm_SingleFacerConsole04 singleFacer3_console04;
		private Frm_SingleFacerAlarmActive singleFacer3_alarmActive;
		private Frm_SingleFacerAlarmHistory singleFacer3_alarmHistory;
		private Frm_SingleFacerSettingsMachine singleFacer3_settingsMachine;
		private Frm_SingleFacerSettingsTask singleFacer3_settingsTask;
    private Frm_SingleFacerSettingsSFStatus singleFacer3_settingsSFStatus;
    private Frm_SingleFacerSettingsBridgeStatus singleFacer3_settingsBridgeStatus;

		//Splicer group
		private Frm_SplicerStatus splicer1_status;
		private Frm_SplicerConsole splicer1_console;
		private Frm_SplicerAlarmActive splicer1_alarmActive;
		private Frm_SplicerAlarmHistory splicer1_alarmHistory;
		private Frm_SplicerSettingsMachine splicer1_settingsMachine;
		private Frm_SplicerSettingsTask splicer1_settingsTask;
		private Frm_SplicerSettingsStatus splicer1_settingsStatus;

		private Frm_SplicerStatus splicer2_status;
		private Frm_SplicerConsole splicer2_console;
		private Frm_SplicerAlarmActive splicer2_alarmActive;
		private Frm_SplicerAlarmHistory splicer2_alarmHistory;
		private Frm_SplicerSettingsMachine splicer2_settingsMachine;
		private Frm_SplicerSettingsTask splicer2_settingsTask;
		private Frm_SplicerSettingsStatus splicer2_settingsStatus;

		private Frm_SplicerStatus splicer3_status;
		private Frm_SplicerConsole splicer3_console;
		private Frm_SplicerAlarmActive splicer3_alarmActive;
		private Frm_SplicerAlarmHistory splicer3_alarmHistory;
		private Frm_SplicerSettingsMachine splicer3_settingsMachine;
		private Frm_SplicerSettingsTask splicer3_settingsTask;
		private Frm_SplicerSettingsStatus splicer3_settingsStatus;

		private Frm_SplicerStatus splicer4_status;
		private Frm_SplicerConsole splicer4_console;
		private Frm_SplicerAlarmActive splicer4_alarmActive;
		private Frm_SplicerAlarmHistory splicer4_alarmHistory;
		private Frm_SplicerSettingsMachine splicer4_settingsMachine;
		private Frm_SplicerSettingsTask splicer4_settingsTask;
		private Frm_SplicerSettingsStatus splicer4_settingsStatus;

		private Frm_SplicerStatus splicer5_status;
		private Frm_SplicerConsole splicer5_console;
		private Frm_SplicerAlarmActive splicer5_alarmActive;
		private Frm_SplicerAlarmHistory splicer5_alarmHistory;
		private Frm_SplicerSettingsMachine splicer5_settingsMachine;
		private Frm_SplicerSettingsTask splicer5_settingsTask;
		private Frm_SplicerSettingsStatus splicer5_settingsStatus;

		private Frm_SplicerStatus splicer6_status;
		private Frm_SplicerConsole splicer6_console;
		private Frm_SplicerAlarmActive splicer6_alarmActive;
		private Frm_SplicerAlarmHistory splicer6_alarmHistory;
		private Frm_SplicerSettingsMachine splicer6_settingsMachine;
		private Frm_SplicerSettingsTask splicer6_settingsTask;
		private Frm_SplicerSettingsStatus splicer6_settingsStatus;

		private Frm_SplicerStatus splicer7_status;
		private Frm_SplicerConsole splicer7_console;
		private Frm_SplicerAlarmActive splicer7_alarmActive;
		private Frm_SplicerAlarmHistory splicer7_alarmHistory;
		private Frm_SplicerSettingsMachine splicer7_settingsMachine;
		private Frm_SplicerSettingsTask splicer7_settingsTask;
		private Frm_SplicerSettingsStatus splicer7_settingsStatus;

		//DoubleBacker
		private Frm_DoubleBackerStatus01 doubleBacker_status01;
		private Frm_DoubleBackerStatus02 doubleBacker_status02;
		private Frm_DoubleBackerStatus03 doubleBacker_status03;
		private Frm_DoubleBackerStatus04 doubleBacker_status04;
		private Frm_DoubleBackerConsole01 doubleBacker_console01;
		private Frm_DoubleBackerConsole02 doubleBacker_console02;
		private Frm_DoubleBackerConsole03 doubleBacker_console03;
		private Frm_DoubleBackerConsole04 doubleBacker_console04;
		private Frm_DoubleBackerAlarmsActive doubleBacker_AlarmsActive;
		private Frm_DoubleBackerAlarmsHistory doubleBacker_AlarmsHistory;
		private Frm_DoubleBackerSettingsMachine doubleBacker_SettingsMachine;
		private Frm_DoubleBackerSettingsTask doubleBacker_SettingsTask;
    private Frm_DoubleBackerSettingsStatus doubleBacker_SettingsStatus;

		//Shear group
		private Frm_ShearStatus shear_status;
		private Frm_ShearConsole shear_console;
		private Frm_ShearAlarmActive shear_alarmsActive;
		private Frm_ShearAlarmHistory shear_alarmsHistory;
		private Frm_ShearSettingsMachine shear_settingsMachine;
		private Frm_ShearSettingsTask shear_settingsTask;

		//Slitter Scorer group
		private Frm_SlitterScorerStatus slitterScorer_status;
		private Frm_SlitterScorerConsole slitterScorer_console;
		private Frm_SlitterScorerAlarmActive slitterScorer_alarmActive;
		private Frm_SlitterScorerAlarmHistory slitterScorer_alarmHistory;
		private Frm_SlitterScorerSettingsMachine slitterScorer_settingsMachine;
		private Frm_SlitterScorerSettingsTask slitterScorer_settingsTask;

		//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
		private Frm_SlitterScorerBlades slitterScorer_blades;
		private Frm_SlitterScorerWebDirector slitterScorer_WebDirector;
		private Frm_SlitterScorerRegulations slitterScorer_regulations;

		//Knife group
		private Frm_KnifeStatus knife_status;
    private Frm_KnifePattern knife_pattern;
		private Frm_KnifeConsole knife_console;
		private Frm_KnifeAlarmActive knife_alarmsActive;
		private Frm_KnifeAlarmHistory knife_alarmsHistory;
		private Frm_KnifeSettingsMachine knife_settingsMachine;
		private Frm_KnifeSettingsTask knife_settingsTask;

		//Stacker group
		private Frm_StackerStatus01 stacker1_status01;
    private Frm_StackerStatus02 stacker1_status02;
    private Frm_StackerStatus03 stacker1_status03;
    private Frm_StackerStatus04 stacker1_status04;
		private Frm_StackerConsole stacker1_console;
		private Frm_StackerAlarmActive stacker1_alarmActive;
		private Frm_StackerAlarmHistory stacker1_alarmHistory;
		private Frm_StackerSettingsMachine stacker1_settingsMachine;
		private Frm_StackerSettingsTask stacker1_settingsTask;
		private Frm_StackerSettingsStatus stacker1_settingsStatus;

    private Frm_StackerStatus01 stacker2_status01;
    private Frm_StackerStatus02 stacker2_status02;
    private Frm_StackerStatus03 stacker2_status03;
    private Frm_StackerStatus04 stacker2_status04;
		private Frm_StackerConsole stacker2_console;
		private Frm_StackerAlarmActive stacker2_alarmActive;
		private Frm_StackerAlarmHistory stacker2_alarmHistory;
		private Frm_StackerSettingsMachine stacker2_settingsMachine;
		private Frm_StackerSettingsTask stacker2_settingsTask;
		private Frm_StackerSettingsStatus stacker2_settingsStatus;

    private Frm_StackerStatus01 stacker3_status01;
    private Frm_StackerStatus02 stacker3_status02;
    private Frm_StackerStatus03 stacker3_status03;
    private Frm_StackerStatus04 stacker3_status04;
		private Frm_StackerConsole stacker3_console;
		private Frm_StackerAlarmActive stacker3_alarmActive;
		private Frm_StackerAlarmHistory stacker3_alarmHistory;
		private Frm_StackerSettingsMachine stacker3_settingsMachine;
		private Frm_StackerSettingsTask stacker3_settingsTask;
		private Frm_StackerSettingsStatus stacker3_settingsStatus;

		//Glue Unit group
		private Frm_GlueUnitConsoleWrapArms glueUnit_ConsoleWrapArms;
		private Frm_GlueUnitConsoleGlueGaps glueUnit_ConsoleGlueGaps;
		private Frm_GlueUnitStatus glueUnit_Status;
		private Frm_GlueUnitAlarmActive glueUnit_alarmsActive;
		private Frm_GlueUnitAlarmHistory glueUnit_alarmsHistory;
		private Frm_GlueUnitSettingsMachine glueUnit_settingsMachine;
		private Frm_GlueUnitSettingsTask glueUnit_settingsTask;

		//Pre-Heater Tern group
		private Frm_PreHeaterTernConsole preHeaterTern_Console;
		private Frm_PreHeaterTernStatus preHeaterTern_Status;
		private Frm_PreHeaterTernAlarmActive preHeaterTern_alarmsActive;
		private Frm_PreHeaterTernAlarmHistory preHeaterTern_alarmsHistory;
		private Frm_PreHeaterTernSettingsMachine preHeaterTern_settingsMachine;
    private Frm_PreHeaterTernSettingsTask preHeaterTern_settingsTask;

		//Debug group
		private Frm_DebugDatabase debug_DebugDatabase;
    private Frm_DebugINI debug_DebugINI;
      //Twin400
    private Frm_DebugTwin400Status1 debug_DebugTwin400Status1;
    private Frm_DebugTwin400Status2 debug_DebugTwin400Status2;
    private Frm_DebugTwin400SetupSituation debug_DebugTwin400SetupSituation;
		//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
		private Frm_DebugTwin400_positioning debug_DebugTwin400_Positioning;
    //agrassi_FI25-05-2011_1
    private Frm_DebugTwin2 debug_DebugTwin2;

    //WetEnd Supervisor group
    private Frm_WetEndSupervisor01 wetEndSupervisor_01;
		private Frm_WetEndSupervisor02 wetEndSupervisor_02;
		private Frm_WetEndSupervisor03 wetEndSupervisor_03;

		//DryEnd Supervisor group
		private Frm_DryEndSupervisor01 dryEndSupervisor_01;
		private Frm_DryEndSupervisor02 dryEndSupervisor_02;
		private Frm_DryEndSupervisor03 dryEndSupervisor_03;

		//Twin400 group
		//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
			//production
		//private Frm_twin400_console twin400_console;
		//private Frm_twin400_blades twin400_blades;
		//private Frm_twin400_regulations twin400_regulations;
    //orders
		private Frm_twin400_ordersList twin400_ordersList;
		private Frm_twin400_orderDetails twin400_orderDetails;
		private Frm_twin400_pastOrders twin400_pastOrders;
			//tools
		private Frm_twin400_bladesAndScorers twin400_bladesAndScorers;
		private Frm_twin400_rotary twin400_rotary;
		private Frm_twin400_webDirector twin400_webDirector;
			//flutes
		private Frm_twin400_flutes twin400_flutes;
		private Frm_twin400_quality twin400_quality;
			//alarms
		private Frm_twin400_alarm1 twin400_alarm1;
		private Frm_twin400_alarm4 twin400_alarm4;

    //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
    //warnings
    private Frm_twin400_warning1 twin400_warning1;
    private Frm_twin400_warning4 twin400_warning4;

			//settings
		private Frm_twin400_misc twin400_misc;
		private Frm_twin400_machineSettings twin400_machineSettings;
    private Frm_twin400_tasks twin400_tasks;
    private Frm_twin400_plunges twin400_plunges;
		//maintenance
    private Frm_twin400_maintenance twin400_maintenance;
    private Frm_twin400_simulations twin400_simulations;
    private Frm_twin400_testInput twin400_testInput;
    private Frm_twin400_testOutput twin400_testOutput;
		//status
		//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
		//private Frm_twin400_positioning twin400_Positioning;
		private Frm_twin400_overview twin400_Overview;
		private Frm_twin400_console twin400_console;
		private Frm_twin400_blades twin400_blades;
		private Frm_twin400_regulations twin400_regulations;

    //PopUps
    private Frm_Alert alert;
    private Frm_AskValue askValue;
    //agrassi_FI05-03-08
    private Frm_AskString askString;
    private Frm_AskValueAndConfirm askValueAndConfirm;
    private Frm_RollData rollData;
    private Frm_Confirm confirm;
    private Frm_MoveOrders moveOrders;
    private Frm_MultipleDelete multipleDelete;
    //agrassi_FI30-05-08
    private Frm_ScorersOptions scorersOptions;
    private Frm_FilterQuality filterQuality;

    /// <summary>
    ///Client private page. 
		///It can be displayed only by the client when it detects a communication failure calling the
		///method CommFailure().
		///The server won't never ask the client for showing this page. 
		///This page won't be never cached.
		///This won't never be the "current page".
		/// </summary>
		private Frm_CommFailure commFailure;

		private static Logger logger;
		private static CommunicationManager communicationManager;
		private static DBManager dBManager;
		private static RTCManager rtcManager;
		private static Settings settings;
		private PictureBox pct_LogoFosber;
		private PictureBox pct_LogoSyncro;
		private Label lbl_FosberInfo;
    private System.Windows.Forms.Timer timer_Closable;
    private Label lbl_ProductDataVersion;

		/// <summary>
		/// Contains all the useful informations about visited pages.
		/// </summary>
		public PagesHistorian pagesHistorian;

    public static bool simulationSyncroStart = false;
    public static bool simulationTwin400Start = false;
    private static string[] args;
    private ProgBar progressBar1;

		public static string FOSBER_INFO = "Fosber S.p.A. - 55060 Monsagrati (Lucca) Italy\n Via Provinciale per Camaiore, 27/28\n T.+3905833891 - F.+390583389205\n www.fosber.it";
    //agrassi_FI26-11-08
    //agrassi_FI08-01-09_1
    //private static string NET_CONFIG_FILE = "NetConfig.ini";
    //private static string NET_CONFIG_FILE_OLD = "NetConfigOld.ini";
    public static string NET_CONFIG_FILE = "NetConfig.ini";
    public static string NET_CONFIG_FILE_OLD = "NetConfigOld.ini";
    private static string IP_DSP = "10.151.152.1";
    private static string IP_PC = "10.151.151.1";
    private static string IP_PC_PMAC400 = "10.151.151.2";
    private static string SUBNET_DSP_PC = "255.255.255.0";
    private static string DEF_GATEWAY_PC = "10.151.151.254";
    private static int NIC_NUMBER = 2;
    private static int MAX_NIC = 3;
    private static int NIC_NUMBER_TWIN400 = 1;
    private static int MAX_NIC_TWIN400 = 1;
    private const int NIC_NOT_RECOGNISED = -1;
    private static bool reboot = false;


    /// <summary>
		/// Builder.
		/// </summary>
		private PageManager()
		{
			InitializeComponent();

			lbl_FosberInfo.Text = PageManager.FOSBER_INFO;		
			lbl_OS.Text = Environment.OSVersion.Platform.ToString() + " - " + Environment.OSVersion.Version.ToString();
		}

		//RFAZZI_FI04/02/08
    //agrassi_FI19-08-2010_1
    //public void setSyncroSoftwareStandard(string softwareStd)
    //{
    //  this.syncroSoftwareStandard = softwareStd;
    //  this.lbl_SyncroSoftwareStandard.Text = "."+syncroSoftwareStandard;
    //  Application.DoEvents();
    //}

		//RFAZZI_FI04/02/08
    //agrassi_FI19-08-2010_1
    //public string getSyncroSoftwareStandard()
    //{
    //  return this.syncroSoftwareStandard;
    //}

    public void setSyncroVersion(string version)
    {
      this.SyncroVersion = version;
      this.lbl_ProductVersion.Text = "Version " + version;
      Application.DoEvents();
    }

    public string getSyncroVersion()
    {
      return this.SyncroVersion;
    }

		public void setSyncroDataVersion(string version)
		{
			this.SyncroDataVersion = version;
			this.lbl_ProductDataVersion.Text = "Data Version " + version;
			Application.DoEvents();
		}

		public string getSyncroDataVersion()
		{
			return this.SyncroDataVersion;
		}

		public void setITEVersion(string version)
		{
			this.ITEVersion = version;
			Application.DoEvents();
		}

		public string getITEVersion()
		{
			return this.ITEVersion;
		}

    private void LauyoutComponents()
		{
			pct_LogoSyncro.Visible = true;
		}

		#region avoid warning

		/// <summary>
		/// does not do anything but it is useful to avoid some warnings.
		/// </summary>
		private void avoidWarnings()
		{
			//initialization of form's identifiers to avoid warning: "the variable ??? is declared but never used".
			bool alwaysFalse = false;
			if (alwaysFalse) 
			{
				//the fallowing code won't be NEVER executed
				//status bar
				statusBar_statusBarDryEnd = new Frm_StatusBarDryEnd();
				statusBar_statusBarWetEnd = new Frm_StatusBarWetEnd();
				statusBar_statusBarTwin400 = new Frm_StatusBarTwin400();
        //lateralBar
        lateralBar_lateralBar = new Frm_LateralBar();
				//production group
				production_homeDry = new Frm_HomeDry();
				production_homeWet = new Frm_HomeWet();
				production_homePrc = new Frm_HomePrc();
				production_alarm1 = new Frm_Alarm1();
				production_alarm2 = new Frm_Alarm2();

        //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
        production_warning1 = new Frm_Warning1();
        production_warning2 = new Frm_Warning2();

				production_paper = new Frm_Paper();
				//production_fullInsDryOrder = new Frm_FullInsDryOrder();
				production_insWetOrder = new Frm_InsWetOrder();
				production_dryPastOrder = new Frm_DryPastOrders();
				production_quickInsDryOrder = new Frm_QuickInsDryOrder();
				production_dryOrdersList = new Frm_DryOrdersList();
				production_dryPastOrdersList = new Frm_DryPastOrdersList();
				production_dryWetOrdersList = new Frm_DryWetOrdersList();
				production_wetOrdersList = new Frm_WetOrdersList();
				production_quality = new Frm_Quality();
        production_operator = new Frm_Operator();
        production_printings = new Frm_Printings();
        production_MessagePRC = new Frm_MessagePRC();
        //statistic group
				statistics_shiftReport = new Frm_ShiftReport();
				statistics_downtimeReport = new Frm_DowntimeReport();
				statistics_ordersList = new Frm_StatisticsOrdersList();
				statistics_ordersDetail = new Frm_StatisticsOrdersDetail();
				statistics_pastRollsList = new Frm_StatisticsRollsList();
        statistics_pastRollsTrace = new Frm_StatisticsRollsTrace();
        statistics_productionTrace= new Frm_ProductionTrace();
				//syncroSystem group
				syncroSystem_AOCsettings = new Frm_AOCSettings();
				syncroSystem_AOCstatus = new Frm_AOCStatus();
				syncroSystem_WECsettings = new Frm_WECSettings();
				syncroSystem_WECstatus = new Frm_WECStatus();
				syncroSystem_SpeedSettings = new Frm_SpeedSettings();
				syncroSystem_SpeedStatus = new Frm_SpeedStatus();
				syncroSystem_SpeedReferenceSettings = new Frm_SpeedReferenceSettings();
				syncroSystem_SpeedReferenceStatus = new Frm_SpeedReferenceStatus();
				syncroSystem_SyncroSettings = new Frm_SyncroSettings();
				syncroSystem_SyncroStatus = new Frm_SyncroStatus();
				syncroSystem_SyncroTask1 = new Frm_SyncroTask1();
				syncroSystem_SyncroTask2 = new Frm_SyncroTask2();
				//RFazzi_FI28/09/2006_2.Added Office LineConfig section
				syncroSystem_OfficeSettings = new Frm_OFFICESettings();
				syncroSystem_OfficeStatus = new Frm_OFFICEStatus();
				syncroSystem_PrcSettings = new Frm_PRCSettings();
				syncroSystem_Log1 = new Frm_Log1();
				syncroSystem_Log2 = new Frm_Log2();
				syncroSystem_Log3 = new Frm_Log3();
				syncroSystem_Log4 = new Frm_Log4();
				//settings group
				settings_languages = new Frm_Languages();
				settings_flute = new Frm_Flute();
				settings_lineConfig = new Frm_LineConfig();
				settings_downTimeReasons = new Frm_DownTimeReasons();
				settings_DIOStatus = new Frm_DIOStatus();
				settings_CTCStatus = new Frm_CTCStatus();
				settings_AIOStatus = new Frm_AIOStatus();
        settings_RestoreBackup = new Frm_RestoreBackup();
        //simulation subgroup
        settings_SimulationGeneral = new Frm_SimulationGeneral();
        //operations
        settings_Operations = new Frm_SyncroSystemOperations();
				//SingleFacer group (1)
				singleFacer1_status01 = new Frm_SingleFacerStatus01(0);
				singleFacer1_status02 = new Frm_SingleFacerStatus02(0);
				singleFacer1_status03 = new Frm_SingleFacerStatus03(0);
				singleFacer1_status04 = new Frm_SingleFacerStatus04(0);
				singleFacer1_console01 = new Frm_SingleFacerConsole01(0);
				singleFacer1_console02 = new Frm_SingleFacerConsole02(0);
				singleFacer1_console03 = new Frm_SingleFacerConsole03(0);
				singleFacer1_console04 = new Frm_SingleFacerConsole04(0);
				singleFacer1_alarmActive = new Frm_SingleFacerAlarmActive(0);
				singleFacer1_alarmHistory = new Frm_SingleFacerAlarmHistory(0);
				singleFacer1_settingsMachine = new Frm_SingleFacerSettingsMachine(0);
				singleFacer1_settingsTask = new Frm_SingleFacerSettingsTask(0);
				singleFacer1_settingsSFStatus = new Frm_SingleFacerSettingsSFStatus(0);
        singleFacer1_settingsBridgeStatus = new Frm_SingleFacerSettingsBridgeStatus(0);
        //SingleFacer group (2)
				singleFacer2_status01 = new Frm_SingleFacerStatus01(0);
				singleFacer2_status02 = new Frm_SingleFacerStatus02(0);
				singleFacer2_status03 = new Frm_SingleFacerStatus03(0);
				singleFacer2_status04 = new Frm_SingleFacerStatus04(0);
				singleFacer2_console01  = new Frm_SingleFacerConsole01(0);
				singleFacer2_console02  = new Frm_SingleFacerConsole02(0);
				singleFacer2_console03  = new Frm_SingleFacerConsole03(0);
				singleFacer2_console04 = new Frm_SingleFacerConsole04(0);
				singleFacer2_alarmActive = new Frm_SingleFacerAlarmActive(0);
				singleFacer2_alarmHistory = new Frm_SingleFacerAlarmHistory(0);
				singleFacer2_settingsMachine = new Frm_SingleFacerSettingsMachine(0);
				singleFacer2_settingsTask = new Frm_SingleFacerSettingsTask(0);
        singleFacer2_settingsSFStatus = new Frm_SingleFacerSettingsSFStatus(0);
        singleFacer2_settingsBridgeStatus = new Frm_SingleFacerSettingsBridgeStatus(0);
        //SingleFacer group (3)
				singleFacer3_status01 = new Frm_SingleFacerStatus01(0);
				singleFacer3_status02 = new Frm_SingleFacerStatus02(0);
				singleFacer3_status03 = new Frm_SingleFacerStatus03(0);
				singleFacer3_status04 = new Frm_SingleFacerStatus04(0);
				singleFacer3_console01 = new Frm_SingleFacerConsole01(0);
				singleFacer3_console02 = new Frm_SingleFacerConsole02(0);
				singleFacer3_console03 = new Frm_SingleFacerConsole03(0);
				singleFacer3_console04 = new Frm_SingleFacerConsole04(0);
				singleFacer3_alarmActive = new Frm_SingleFacerAlarmActive(0);
				singleFacer3_alarmHistory = new Frm_SingleFacerAlarmHistory(0);
				singleFacer3_settingsMachine = new Frm_SingleFacerSettingsMachine(0);
				singleFacer3_settingsTask = new Frm_SingleFacerSettingsTask(0);
        singleFacer3_settingsSFStatus = new Frm_SingleFacerSettingsSFStatus(0);
        singleFacer3_settingsBridgeStatus = new Frm_SingleFacerSettingsBridgeStatus(0);

				//Splicer group (1)
				splicer1_status = new Frm_SplicerStatus(0);
				splicer1_console = new Frm_SplicerConsole(0);
				splicer1_alarmActive = new Frm_SplicerAlarmActive(0);
				splicer1_alarmHistory = new Frm_SplicerAlarmHistory(0);
				splicer1_settingsMachine = new Frm_SplicerSettingsMachine(0);
				splicer1_settingsTask = new Frm_SplicerSettingsTask(0);
				splicer1_settingsStatus = new Frm_SplicerSettingsStatus(0);
				//Splicer group (2)
				splicer2_status = new Frm_SplicerStatus(0);
				splicer2_console = new Frm_SplicerConsole(0);
				splicer2_alarmActive = new Frm_SplicerAlarmActive(0);
				splicer2_alarmHistory = new Frm_SplicerAlarmHistory(0);
				splicer2_settingsMachine = new Frm_SplicerSettingsMachine(0);
				splicer2_settingsTask = new Frm_SplicerSettingsTask(0);
				splicer2_settingsStatus = new Frm_SplicerSettingsStatus(0);
				//Splicer group (3)
				splicer3_status = new Frm_SplicerStatus(0);
				splicer3_console = new Frm_SplicerConsole(0);
				splicer3_alarmActive = new Frm_SplicerAlarmActive(0);
				splicer3_alarmHistory = new Frm_SplicerAlarmHistory(0);
				splicer3_settingsMachine = new Frm_SplicerSettingsMachine(0);
				splicer3_settingsTask = new Frm_SplicerSettingsTask(0);
				splicer3_settingsStatus = new Frm_SplicerSettingsStatus(0);
				//Splicer group (4)
				splicer4_status = new Frm_SplicerStatus(0);
				splicer4_console = new Frm_SplicerConsole(0);
				splicer4_alarmActive = new Frm_SplicerAlarmActive(0);
				splicer4_alarmHistory = new Frm_SplicerAlarmHistory(0);
				splicer4_settingsMachine = new Frm_SplicerSettingsMachine(0);
				splicer4_settingsTask = new Frm_SplicerSettingsTask(0);
				splicer4_settingsStatus = new Frm_SplicerSettingsStatus(0);
				//Splicer group (5)
				splicer5_status = new Frm_SplicerStatus(0);
				splicer5_console = new Frm_SplicerConsole(0);
				splicer5_alarmActive = new Frm_SplicerAlarmActive(0);
				splicer5_alarmHistory = new Frm_SplicerAlarmHistory(0);
				splicer5_settingsMachine = new Frm_SplicerSettingsMachine(0);
				splicer5_settingsTask = new Frm_SplicerSettingsTask(0);
				splicer5_settingsStatus = new Frm_SplicerSettingsStatus(0);
				//Splicer group (6)
				splicer6_status = new Frm_SplicerStatus(0);
				splicer6_console = new Frm_SplicerConsole(0);
				splicer6_alarmActive = new Frm_SplicerAlarmActive(0);
				splicer6_alarmHistory = new Frm_SplicerAlarmHistory(0);
				splicer6_settingsMachine = new Frm_SplicerSettingsMachine(0);
				splicer6_settingsTask = new Frm_SplicerSettingsTask(0);
				splicer6_settingsStatus = new Frm_SplicerSettingsStatus(0);
				//Splicer group (7)
				splicer7_status = new Frm_SplicerStatus(0);
				splicer7_console = new Frm_SplicerConsole(0);
				splicer7_alarmActive = new Frm_SplicerAlarmActive(0);
				splicer7_alarmHistory = new Frm_SplicerAlarmHistory(0);
				splicer7_settingsMachine = new Frm_SplicerSettingsMachine(0);
				splicer7_settingsTask = new Frm_SplicerSettingsTask(0);
				splicer7_settingsStatus = new Frm_SplicerSettingsStatus(0);

				//Double Backer Group
				doubleBacker_AlarmsActive = new Frm_DoubleBackerAlarmsActive();
				doubleBacker_AlarmsHistory = new Frm_DoubleBackerAlarmsHistory();
				doubleBacker_SettingsMachine = new Frm_DoubleBackerSettingsMachine();
				doubleBacker_SettingsTask = new Frm_DoubleBackerSettingsTask();
				doubleBacker_SettingsStatus = new Frm_DoubleBackerSettingsStatus();
				doubleBacker_status01 = new Frm_DoubleBackerStatus01();
				doubleBacker_status02 = new Frm_DoubleBackerStatus02();
				doubleBacker_status03 = new Frm_DoubleBackerStatus03();
				doubleBacker_status04 = new Frm_DoubleBackerStatus04();				
				doubleBacker_console01 = new Frm_DoubleBackerConsole01();
				doubleBacker_console02 = new Frm_DoubleBackerConsole02();
				doubleBacker_console03 = new Frm_DoubleBackerConsole03();
        doubleBacker_console04 = new Frm_DoubleBackerConsole04();
        //Shear Group
				shear_status = new Frm_ShearStatus();
				shear_console = new Frm_ShearConsole();
				shear_settingsMachine = new Frm_ShearSettingsMachine();
				shear_settingsTask = new Frm_ShearSettingsTask();
				shear_alarmsActive = new Frm_ShearAlarmActive();
				shear_alarmsHistory = new Frm_ShearAlarmHistory();
				//SlitterScore Group
				slitterScorer_status = new Frm_SlitterScorerStatus();
				slitterScorer_console = new Frm_SlitterScorerConsole();
				slitterScorer_alarmActive = new Frm_SlitterScorerAlarmActive();
				slitterScorer_alarmHistory = new Frm_SlitterScorerAlarmHistory();
				slitterScorer_settingsMachine = new Frm_SlitterScorerSettingsMachine();
				slitterScorer_settingsTask = new Frm_SlitterScorerSettingsTask();
				//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
				slitterScorer_blades = new Frm_SlitterScorerBlades();
				slitterScorer_WebDirector = new Frm_SlitterScorerWebDirector();
				slitterScorer_regulations = new Frm_SlitterScorerRegulations();

				//Knife Group
				knife_status = new Frm_KnifeStatus();
        knife_pattern = new Frm_KnifePattern();
				knife_console = new Frm_KnifeConsole();
				knife_alarmsActive = new Frm_KnifeAlarmActive();
				knife_alarmsHistory = new Frm_KnifeAlarmHistory();
				knife_settingsMachine = new Frm_KnifeSettingsMachine();
				knife_settingsTask = new Frm_KnifeSettingsTask();
				//Stacker Group
				stacker1_status01 = new Frm_StackerStatus01(0);
        stacker1_status02 = new Frm_StackerStatus02(0);
        stacker1_status03 = new Frm_StackerStatus03(0);
        stacker1_status04 = new Frm_StackerStatus04(0);
				stacker1_console = new Frm_StackerConsole(0);
				stacker1_alarmActive = new Frm_StackerAlarmActive(0);
				stacker1_alarmHistory = new Frm_StackerAlarmHistory(0);
				stacker1_settingsMachine = new Frm_StackerSettingsMachine(0);
				stacker1_settingsTask = new Frm_StackerSettingsTask(0);
				stacker1_settingsStatus = new Frm_StackerSettingsStatus(0);
        stacker2_status01 = new Frm_StackerStatus01(0);
        stacker2_status02 = new Frm_StackerStatus02(0);
        stacker2_status03 = new Frm_StackerStatus03(0);
        stacker2_status04 = new Frm_StackerStatus04(0);
				stacker2_console = new Frm_StackerConsole(0);
				stacker2_alarmActive = new Frm_StackerAlarmActive(0);
				stacker2_alarmHistory = new Frm_StackerAlarmHistory(0);
				stacker2_settingsMachine = new Frm_StackerSettingsMachine(0);
				stacker2_settingsTask = new Frm_StackerSettingsTask(0);
				stacker2_settingsStatus = new Frm_StackerSettingsStatus(0);
        stacker3_status01 = new Frm_StackerStatus01(0);
        stacker3_status02 = new Frm_StackerStatus02(0);
        stacker3_status03 = new Frm_StackerStatus03(0);
        stacker3_status04 = new Frm_StackerStatus04(0);
				stacker3_console = new Frm_StackerConsole(0);
				stacker3_alarmActive = new Frm_StackerAlarmActive(0);
				stacker3_alarmHistory = new Frm_StackerAlarmHistory(0);
				stacker3_settingsMachine = new Frm_StackerSettingsMachine(0);
				stacker3_settingsTask = new Frm_StackerSettingsTask(0);
				stacker3_settingsStatus = new Frm_StackerSettingsStatus(0);
				//Dry End Supervisor Group
				dryEndSupervisor_01 = new Frm_DryEndSupervisor01();
				dryEndSupervisor_02 = new Frm_DryEndSupervisor02();
				dryEndSupervisor_03 = new Frm_DryEndSupervisor03();
				//Wet End Supervisor Group
				wetEndSupervisor_01 = new Frm_WetEndSupervisor01();
				wetEndSupervisor_02 = new Frm_WetEndSupervisor02();
				wetEndSupervisor_03 = new Frm_WetEndSupervisor03();
				//Twin400 Group
					//production
				twin400_console = new Frm_twin400_console();
        twin400_blades = new Frm_twin400_blades();
        twin400_regulations= new Frm_twin400_regulations();
        //orders
				twin400_ordersList = new Frm_twin400_ordersList();
				twin400_orderDetails = new Frm_twin400_orderDetails();
				twin400_pastOrders = new Frm_twin400_pastOrders();
					//tools
				twin400_bladesAndScorers = new Frm_twin400_bladesAndScorers();
				twin400_rotary = new Frm_twin400_rotary();
				twin400_webDirector = new Frm_twin400_webDirector();
					//flutes
				twin400_flutes = new Frm_twin400_flutes();
				twin400_quality = new Frm_twin400_quality();
					//alarms
				twin400_alarm1 = new Frm_twin400_alarm1();
				twin400_alarm4 = new Frm_twin400_alarm4();

        //RFAZZI_FI26/01/2011_2 Aggiunta gestione Warning su grafica Twin400 di Syncro. 
        //warnings
        twin400_warning1 = new Frm_twin400_warning1();
        twin400_warning4 = new Frm_twin400_warning4();

					//settings
				twin400_misc = new Frm_twin400_misc();
				twin400_machineSettings = new Frm_twin400_machineSettings();
        twin400_tasks = new Frm_twin400_tasks();
        twin400_plunges = new Frm_twin400_plunges();
          //maintenance
        twin400_maintenance = new Frm_twin400_maintenance();
        twin400_simulations = new Frm_twin400_simulations();
        twin400_testInput = new Frm_twin400_testInput();
        twin400_testOutput = new Frm_twin400_testOutput();
				//status
				twin400_Overview = new Frm_twin400_overview();
				//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
				//twin400_Positioning = new Frm_twin400_positioning();

        //Glue Unit group
				glueUnit_ConsoleWrapArms = new Frm_GlueUnitConsoleWrapArms();
				glueUnit_ConsoleGlueGaps = new Frm_GlueUnitConsoleGlueGaps();
				glueUnit_Status = new Frm_GlueUnitStatus();
				glueUnit_alarmsActive = new Frm_GlueUnitAlarmActive();
				glueUnit_alarmsHistory = new Frm_GlueUnitAlarmHistory();
				glueUnit_settingsMachine = new Frm_GlueUnitSettingsMachine();
				glueUnit_settingsTask = new Frm_GlueUnitSettingsTask();

				//Pre-Heater Tern group
				preHeaterTern_Console = new Frm_PreHeaterTernConsole();
				preHeaterTern_Status = new Frm_PreHeaterTernStatus();
				preHeaterTern_alarmsActive= new Frm_PreHeaterTernAlarmActive();
				preHeaterTern_alarmsHistory = new Frm_PreHeaterTernAlarmHistory();
				preHeaterTern_settingsMachine = new Frm_PreHeaterTernSettingsMachine();
        preHeaterTern_settingsTask = new Frm_PreHeaterTernSettingsTask();

				//debug database
				debug_DebugDatabase = new Frm_DebugDatabase();
				debug_DebugINI = new Frm_DebugINI();
          //Twin400
        debug_DebugTwin400Status1 = new Frm_DebugTwin400Status1();
        debug_DebugTwin400Status2 = new Frm_DebugTwin400Status2();
        debug_DebugTwin400SetupSituation = new Frm_DebugTwin400SetupSituation();
				//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
				debug_DebugTwin400_Positioning = new Frm_DebugTwin400_positioning();
        //agrassi_FI25-05-2011_1
        debug_DebugTwin2= new Frm_DebugTwin2();

				//printers group
				syncroSystem_PrintersSettings = new Frm_PRINTERSSettings();
				syncroSystem_PrintersStatus = new Frm_PRINTERSStatus();

        //PopUps
        alert=new Frm_Alert();
        askValue=new Frm_AskValue();
        confirm=new Frm_Confirm();
        moveOrders=new Frm_MoveOrders();
        multipleDelete=new Frm_MultipleDelete();
        //agrassi_FI05-03-08
        askString=new Frm_AskString();
        askValueAndConfirm=new Frm_AskValueAndConfirm();
        rollData=new Frm_RollData();
        //agrassi_FI30-05-08
        scorersOptions = new Frm_ScorersOptions();

        filterQuality = new Frm_FilterQuality();
      }
    }

		#endregion

		/// <summary>
		/// Factory.
		/// </summary>
		/// <returns>the one and only PageManager object of the application</returns>
		public static PageManager getPageManager()
		{
			if (pageManager == null) pageManager = new PageManager();
			return pageManager;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
			if (threadCommandExec != null)
			{
				try
				{
					threadCommandExec.Abort();
				}
				catch (System.Security.SecurityException ex)
				{
					Logger.LogError(this, "Dispose", ex);
				}
				catch (ThreadStateException ex)
				{
					Logger.LogError(this, "Dispose - the thread \"" + threadCommandExec.Name + "\" was suspended.", ex);
				}
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageManager));
      this.lbl_State = new System.Windows.Forms.Label();
      this.timer_Start = new System.Windows.Forms.Timer(this.components);
      this.lbl_OS = new System.Windows.Forms.Label();
      this.lbl_ProductVersion = new System.Windows.Forms.Label();
      this.timer_Message = new System.Windows.Forms.Timer(this.components);
      this.lbl_Message_NY = new System.Windows.Forms.Label();
      this.timer_DateTime = new System.Windows.Forms.Timer(this.components);
      this.lbl_Date = new System.Windows.Forms.Label();
      this.lbl_Time = new System.Windows.Forms.Label();
      this.lbl_FosberInfo = new System.Windows.Forms.Label();
      this.timer_Closable = new System.Windows.Forms.Timer(this.components);
      this.lbl_ProductDataVersion = new System.Windows.Forms.Label();
      this.pct_LogoFosber = new System.Windows.Forms.PictureBox();
      this.pct_LogoSyncro = new System.Windows.Forms.PictureBox();
      this.progressBar1 = new SyncroC.Controls.ProgBar();
      ((System.ComponentModel.ISupportInitialize)(this.pct_LogoFosber)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_LogoSyncro)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_State
      // 
      this.lbl_State.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_State.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_State.Location = new System.Drawing.Point(29, 220);
      this.lbl_State.Name = "lbl_State";
      this.lbl_State.Size = new System.Drawing.Size(390, 20);
      this.lbl_State.TabIndex = 2;
      this.lbl_State.Text = "starting PageManager. . .";
      // 
      // timer_Start
      // 
      this.timer_Start.Enabled = true;
      this.timer_Start.Interval = 1000;
      this.timer_Start.Tick += new System.EventHandler(this.timer_Start_Tick);
      // 
      // lbl_OS
      // 
      this.lbl_OS.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_OS.Font = new System.Drawing.Font("Tahoma", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_OS.Location = new System.Drawing.Point(29, 196);
      this.lbl_OS.Name = "lbl_OS";
      this.lbl_OS.Size = new System.Drawing.Size(390, 23);
      this.lbl_OS.TabIndex = 9;
      this.lbl_OS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_ProductVersion
      // 
      this.lbl_ProductVersion.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_ProductVersion.Font = new System.Drawing.Font("Tahoma", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ProductVersion.Location = new System.Drawing.Point(29, 172);
      this.lbl_ProductVersion.Name = "lbl_ProductVersion";
      this.lbl_ProductVersion.Size = new System.Drawing.Size(166, 23);
      this.lbl_ProductVersion.TabIndex = 10;
      this.lbl_ProductVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // timer_Message
      // 
      this.timer_Message.Interval = 5000;
      this.timer_Message.Tick += new System.EventHandler(this.timer_Message_Tick);
      // 
      // lbl_Message_NY
      // 
      this.lbl_Message_NY.Location = new System.Drawing.Point(389, 47);
      this.lbl_Message_NY.Name = "lbl_Message_NY";
      this.lbl_Message_NY.Size = new System.Drawing.Size(48, 23);
      this.lbl_Message_NY.TabIndex = 13;
      this.lbl_Message_NY.Visible = false;
      // 
      // timer_DateTime
      // 
      this.timer_DateTime.Enabled = true;
      this.timer_DateTime.Interval = 998;
      this.timer_DateTime.Tick += new System.EventHandler(this.timer_DateTime_Tick);
      // 
      // lbl_Date
      // 
      this.lbl_Date.Location = new System.Drawing.Point(397, 87);
      this.lbl_Date.Name = "lbl_Date";
      this.lbl_Date.Size = new System.Drawing.Size(40, 16);
      this.lbl_Date.TabIndex = 151;
      this.lbl_Date.Text = "Date";
      this.lbl_Date.Visible = false;
      // 
      // lbl_Time
      // 
      this.lbl_Time.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Time.ForeColor = System.Drawing.SystemColors.WindowText;
      this.lbl_Time.Location = new System.Drawing.Point(397, 63);
      this.lbl_Time.Name = "lbl_Time";
      this.lbl_Time.Size = new System.Drawing.Size(40, 16);
      this.lbl_Time.TabIndex = 150;
      this.lbl_Time.Text = "Time";
      this.lbl_Time.Visible = false;
      // 
      // lbl_FosberInfo
      // 
      this.lbl_FosberInfo.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_FosberInfo.Location = new System.Drawing.Point(29, 34);
      this.lbl_FosberInfo.Name = "lbl_FosberInfo";
      this.lbl_FosberInfo.Size = new System.Drawing.Size(390, 80);
      this.lbl_FosberInfo.TabIndex = 155;
      this.lbl_FosberInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // timer_Closable
      // 
      this.timer_Closable.Interval = 30000;
      this.timer_Closable.Tick += new System.EventHandler(this.timer_Closable_Tick);
      // 
      // lbl_ProductDataVersion
      // 
      this.lbl_ProductDataVersion.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_ProductDataVersion.Font = new System.Drawing.Font("Tahoma", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ProductDataVersion.Location = new System.Drawing.Point(250, 174);
      this.lbl_ProductDataVersion.Name = "lbl_ProductDataVersion";
      this.lbl_ProductDataVersion.Size = new System.Drawing.Size(166, 23);
      this.lbl_ProductDataVersion.TabIndex = 156;
      this.lbl_ProductDataVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // pct_LogoFosber
      // 
      this.pct_LogoFosber.Image = global::SyncroC.Properties.Resources.logo_Fosber;
      this.pct_LogoFosber.Location = new System.Drawing.Point(167, 2);
      this.pct_LogoFosber.Name = "pct_LogoFosber";
      this.pct_LogoFosber.Size = new System.Drawing.Size(113, 31);
      this.pct_LogoFosber.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_LogoFosber.TabIndex = 153;
      this.pct_LogoFosber.TabStop = false;
      // 
      // pct_LogoSyncro
      // 
      this.pct_LogoSyncro.Image = global::SyncroC.Properties.Resources.LogoSyncro;
      this.pct_LogoSyncro.Location = new System.Drawing.Point(154, 117);
      this.pct_LogoSyncro.Name = "pct_LogoSyncro";
      this.pct_LogoSyncro.Size = new System.Drawing.Size(139, 56);
      this.pct_LogoSyncro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_LogoSyncro.TabIndex = 152;
      this.pct_LogoSyncro.TabStop = false;
      this.pct_LogoSyncro.Visible = false;
      // 
      // progressBar1
      // 
      this.progressBar1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.progressBar1.BarStyle = SyncroC.Controls.BarStyles.Solid;
      this.progressBar1.color = SyncroC.Controls.Colors.SyncroColor.White;
      this.progressBar1.ForeColor = System.Drawing.Color.Black;
      this.progressBar1.Location = new System.Drawing.Point(6, 244);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Percentage = 0;
      this.progressBar1.PercentageVisible = false;
      this.progressBar1.Size = new System.Drawing.Size(430, 24);
      this.progressBar1.Style = SyncroC.Controls.ProgBarStyles.Big;
      this.progressBar1.TabIndex = 159;
      this.progressBar1.TranslateText = false;
      // 
      // PageManager
      // 
      this.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ClientSize = new System.Drawing.Size(440, 275);
      this.ControlBox = false;
      this.Controls.Add(this.progressBar1);
      this.Controls.Add(this.lbl_ProductDataVersion);
      this.Controls.Add(this.lbl_Date);
      this.Controls.Add(this.lbl_Time);
      this.Controls.Add(this.lbl_Message_NY);
      this.Controls.Add(this.lbl_FosberInfo);
      this.Controls.Add(this.pct_LogoFosber);
      this.Controls.Add(this.pct_LogoSyncro);
      this.Controls.Add(this.lbl_ProductVersion);
      this.Controls.Add(this.lbl_OS);
      this.Controls.Add(this.lbl_State);
      this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "PageManager";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Syncro Client";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PageManager_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.pct_LogoFosber)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_LogoSyncro)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

    //ABello_FI02/10/2007_1 Client AutoUpdate
    public static bool checkForUpdate()
    {
      try
      {
        //RFAZZI_FI11/06/2010_3. Aggiunta gestione per avviare automaticamente da Starter il secondo client presente sul server. Se non � presente, crea anche le directory.
        //if (SyncroC.Utility.Settings.getSettings().configuration.getOnServer() )
        if (SyncroC.Utility.Settings.getSettings().configuration.getOnServer() && SyncroC.Utility.Settings.getSettings().network.ClientID == 1)
          return false;

        string IpServerAddress = SyncroC.Utility.Settings.getSettings().network.IpServerAddress;
        string serverSharedUpdateFromNetwork = System.IO.Path.GetFullPath("\\\\" + IpServerAddress + "\\Update\\_Client");

#if DEBUG
        string sharedFileName = serverSharedUpdateFromNetwork + "\\Debug\\S_SyncroC.exe";
#else
        string sharedFileName = serverSharedUpdateFromNetwork + "\\Release\\S_SyncroC.exe";
#endif

        if (!System.IO.File.Exists(sharedFileName))
          return false;

        string sharedVersion = Assembly.LoadFrom(sharedFileName).GetName().Version.ToString();
        string myVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        if (Assembly.LoadFrom(sharedFileName).GetName().Version <= Assembly.GetExecutingAssembly().GetName().Version)
        {
          return false;
        }
        else
        {
          System.Diagnostics.Process.Start(@".\SyncroGadgets.exe", IpServerAddress);
          return true;
        }
      }
      catch (Exception ex)
      {
        System.Console.WriteLine(ex.Message);
        return false;
      }
    }

		/// <summary>
		/// Main thread of the SYNCRO Graphic client. 
		/// </summary>
		[STAThread]
		static void Main(string[] args) 
		{
      //ABello_FI08/11/2010_1 se c'� gi� un SyncroC che si chiama come me allora non lo faccio partire
      if (System.Diagnostics.Process.GetProcessesByName(Process.GetCurrentProcess().MainModule.ModuleName).Length > 1)
        return;

      //se sono il secondo monito attendo un p� prima di partire
      if (Process.GetCurrentProcess().MainModule.ModuleName.Contains("2"))
        System.Threading.Thread.Sleep(15000);

      //ABello_FI02/10/2007_1 Client AutoUpdate
      if (checkForUpdate())
        return;

      PageManager.args = args;
      //AGrassi_FI 08-03-2007 Automatic simulation
      if (args.Length > 0)
      {
        if (args[0] == "simulation")
          simulationSyncroStart = true;
        else if (args[0] == "simulTwin400")
          simulationTwin400Start = true;
      }
      try
      {
      //agrassi_FI15-10-08_1
      //  if ((Environment.UserName.ToUpper() != "ABELLO") && (Environment.UserName.ToUpper() != "AGRASSI") && (Environment.UserName.ToUpper() != "RFAZZI") && (Environment.UserName.ToUpper() != "PMASSARO") && (Environment.UserName.ToUpper() != "ABORELLI"))
      //  {
      //    SyncroC.CDisplaySettings ChangeRes = new SyncroC.CDisplaySettings();
      //    //agrassi_FI26-09-08
      //    RemoveBallons();
      //  }
        
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(PageManager.getPageManager());
      }
      catch (Exception e)
      {
        string log = "An unhandled exception has been caught at the Application.Run level.";
        //agrassi_FI19-12-08
        //Logger.LogWindowsEvent("PageManager", "Main", log, EventLogEntryType.Error);
        Logger.LogError("PageManager", "Main - " + log, e);
        Frm_Alert alert = new Frm_Alert();
        string text = DBManager.getDBManager().message.getTranslatedMessage("�SYCC_42");
        alert.ShowDialog(text);
        CommunicationManager.GetCommunicationManager().ShutDown(false,false);
      }
		}

    //agrassi_FI15-10-08_1
    //agrassi_FI26-11-08
    //agrassi_FI08-01-09_4
    //private void CheckPCSettings(bool onFosberNetwork)
    private void CheckPCSettings(bool onFosberNetwork, bool onServer, string serverConfigPath, bool twin400Only, bool enableNetConfigManager)
    {
      if (!onFosberNetwork)
        return;
      try
      {
        //agrassi_FI26-11-08
        //if ((Environment.UserName.ToUpper() != "ABELLO") && (Environment.UserName.ToUpper() != "AGRASSI") && (Environment.UserName.ToUpper() != "RFAZZI") && (Environment.UserName.ToUpper() != "PMASSARO") && (Environment.UserName.ToUpper() != "ABORELLI"))
        if (Environment.UserName.ToUpper() == "FOSBER")
        {
					//RFAZZI_FI02/12/2009_1. Rimossa la forzatura della risoluzione della scheda video a 1280x1024
					//Logger.LogError("PageManager", "Change display settings");

					////ABELLO_FI29/05/09 Aggiunta gestione del mega-monitor
					//if (Screen.PrimaryScreen.WorkingArea.Width < 2560)
					//{
					//  SyncroC.CDisplaySettings ChangeRes = new SyncroC.CDisplaySettings();
					//}

					Logger.LogError("PageManager", "Change PC settings");
					
					//agrassi_FI26-09-08
          RemoveBallons();
          //agrassi_FI22-11-08
          DisableInternetBrowser();
          //agrassi_FI26-11-08
          CheckNICData(onServer, serverConfigPath, twin400Only, enableNetConfigManager);
          //PMASSARO_FI04/02/09 - Sets AutoAdminLogON 
          SetAutoAdminLogON(Environment.UserName);
        }
        else
          Logger.LogError("PageManager", "User " + Environment.UserName);
      }
      catch (Exception e)
      {
        Logger.LogError("Main: CheckPCSettings", e);
      }
    }

    //agrassi_FI26-09-08
    private static void RemoveBallons()
    {
      Logger.LogError("PageManager", "RemoveBallons");
      try
      {
        RegistryKey r = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("Explorer").OpenSubKey("Advanced", true);
        if (r != null)
          r.SetValue("EnableBallonTips", 0, RegistryValueKind.DWord);
      }
      catch(Exception ex)
      {
        Logger.LogError("RemoveBallons", ex);
      }
    }

    //agrassi_FI22-11-08
    private static void DisableInternetBrowser()
    {
      Logger.LogError("PageManager", "DisableInternetBrowser");
      try
      {
        RegistryKey r = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("Internet Settings", true);
        if (r != null)
        {
          r.SetValue("ProxyEnable", 1, RegistryValueKind.DWord);
          r.SetValue("ProxyServer", "10.151.153.1:8888", RegistryValueKind.String);
        }
      }
      catch (Exception ex)
      {
        Logger.LogError("DisableInternetExplorer", ex);
      }
    }

    //agrassi_FI26-11-08
    //agrassi_FI08-01-09_4
    private static void SaveNICData(FosberNICData[] nicData, string fileName)
    {
      Logger.LogError("PageManager", "NETconfig - SaveNICData");
      try
      {
        //agrassi_FI08-01-09_4
        for (int i = 0; i < nicData.Length; i++)
        {
          string section = "NIC_" + (i+1).ToString();
          Ini.Set(section, "Name", nicData[i].Name, fileName);
          Ini.Set(section, "ID", nicData[i].ID, fileName);
          Ini.Set(section, "MACaddress", nicData[i].MACAddress, fileName);
          //agrassi_FI14-01-09_1
          //Ini.Set(section, "IndexIntoRegistry", nicData[i].IndexIntoRegistry.ToString(), fileName);
          for (int j = 0; j < nicData[i].IPAddresses.Length; j++)
          {
            Ini.Set(section, "IPAddress_" + (j + 1).ToString(), nicData[i].IPAddresses[j], fileName);
            Ini.Set(section, "SubnetMask_" + (j + 1).ToString(), nicData[i].SubnetMasks[j], fileName);
          }
          for (int j = 0; j < nicData[i].Gateways.Length; j++)
          {
            Ini.Set(section, "Gateway_" + (j + 1).ToString(), nicData[i].Gateways[j], fileName);
          }
          //agrassi_FI14-01-09_1
          for (int j = 0; j < nicData[i].DNS.Length; j++)
          {
            Ini.Set(section, "DNS_" + (j + 1).ToString(), nicData[i].DNS[j], fileName);
          }
          for (int j = 0; j < nicData[i].WINS.Length; j++)
          {
            Ini.Set(section, "WINS_" + (j + 1).ToString(), nicData[i].WINS[j], fileName);
          }
        }
      }
      catch (Exception e)
      {
        Logger.LogError("PageManager", "NETconfig - SaveNICData error: " + e.Message);
      }
    }

    //agrassi_FI08-01-09_4
    private static void ReadPresentNetCards(ref FosberNICData[] presNicData)
    {
      Logger.LogError("PageManager", "NETconfig - ReadActNetCards");
      int cnt = 0;
      //agrassi_FI14-01-09_1
      //int[] indexIntoNetCards = { -1, -1, -1, -1, -1 }, indexIntoRegistry = { -1, -1, -1, -1, -1 };
      int[] indexIntoNetCards = { -1, -1, -1, -1, -1 };
      try
      {
        NetworkInterface[] netCards = NetworkInterface.GetAllNetworkInterfaces();
        RegistryKey r = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows NT").OpenSubKey("CurrentVersion").OpenSubKey("NetworkCards");
        if ((r != null) && (netCards != null) && (netCards.Length > 0))
        {
          int n = 9999;
          string[] regNetCards = r.GetSubKeyNames();
          for (int i = 0; i < netCards.Length; i++)
          {
            if ((netCards[i].NetworkInterfaceType == NetworkInterfaceType.Ethernet) || (netCards[i].NetworkInterfaceType == NetworkInterfaceType.Ethernet3Megabit) ||
               (netCards[i].NetworkInterfaceType == NetworkInterfaceType.FastEthernetFx) || (netCards[i].NetworkInterfaceType == NetworkInterfaceType.FastEthernetT) ||
               (netCards[i].NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet))
            {
              if (netCards[i].Supports(NetworkInterfaceComponent.IPv4))
              {
                for (n = 0; n < regNetCards.Length; n++)
                {
                  string cardId = (string)r.OpenSubKey(regNetCards[n]).GetValue("ServiceName");
                  if (cardId == netCards[i].Id)
                    break;
                }
                if ((regNetCards != null) && (n < regNetCards.Length))
                {
                  indexIntoNetCards[cnt] = i;
                  //agrassi_FI14-01-09_1
                  //indexIntoRegistry[cnt] = n;
                  cnt++;
                }
              }
            }
          }
          presNicData = new FosberNICData[cnt];
          for (int j = 0; j < presNicData.Length; j++)
          {
            presNicData[j].Name = netCards[indexIntoNetCards[j]].Name;
            presNicData[j].ID = netCards[indexIntoNetCards[j]].Id;
            presNicData[j].MACAddress = netCards[indexIntoNetCards[j]].GetPhysicalAddress().ToString();
            //agrassi_FI14-01-09_1
            //presNicData[j].IndexIntoRegistry = indexIntoRegistry[j];
            presNicData[j].IPAddresses = new string[netCards[indexIntoNetCards[j]].GetIPProperties().UnicastAddresses.Count];
            presNicData[j].SubnetMasks = new string[presNicData[j].IPAddresses.Length];
            Logger.LogError("PageManager", "NETconfig - NIC_" + (j + 1).ToString() + ": " + presNicData[j].Name + ", ID=" + presNicData[j].ID + ", MAC=" + presNicData[j].MACAddress);
            string log = "";
            for (int a = 0; a < presNicData[j].IPAddresses.Length; a++)
            {
              presNicData[j].IPAddresses[a] = netCards[indexIntoNetCards[j]].GetIPProperties().UnicastAddresses[a].Address.ToString();
              presNicData[j].SubnetMasks[a] = netCards[indexIntoNetCards[j]].GetIPProperties().UnicastAddresses[a].IPv4Mask.ToString();
              log += "IP_" + (a + 1).ToString() + ": " + presNicData[j].IPAddresses[a] + ", ";
            }
            presNicData[j].Gateways = new string[netCards[indexIntoNetCards[j]].GetIPProperties().GatewayAddresses.Count];
            for (int a = 0; a < presNicData[j].Gateways.Length; a++)
            {
              presNicData[j].Gateways[a] = netCards[indexIntoNetCards[j]].GetIPProperties().GatewayAddresses[a].Address.ToString();
              log += "GW_" + (a + 1).ToString() + ": " + presNicData[j].Gateways[a] + ", ";
            }
            //agrassi_FI14-01-09_1
            presNicData[j].DNS = new string[netCards[indexIntoNetCards[j]].GetIPProperties().DnsAddresses.Count];
            for (int a = 0; a < presNicData[j].DNS.Length; a++)
            {
              presNicData[j].DNS[a] = netCards[indexIntoNetCards[j]].GetIPProperties().DnsAddresses[a].ToString();
              log += "DNS_" + (a + 1).ToString() + ": " + presNicData[j].DNS[a] + ", ";
            }
            presNicData[j].WINS = new string[netCards[indexIntoNetCards[j]].GetIPProperties().WinsServersAddresses.Count];
            for (int a = 0; a < presNicData[j].WINS.Length; a++)
            {
              presNicData[j].WINS[a] = netCards[indexIntoNetCards[j]].GetIPProperties().WinsServersAddresses[a].ToString();
              log += "WINS_" + (a + 1).ToString() + ": " + presNicData[j].WINS[a] + ", ";
            }
            Logger.LogError("PageManager", "NETconfig - " + log);
          }
        }
      }
      catch (Exception e)
      {
        Logger.LogError("PageManager", "NETconfig - ReadActNetCards error: " + e.Message);
      }
    }

    //agrassi_FI08-01-09_4
    private static bool DetectNetConfigChanges(FosberNICData[] oldNicData, FosberNICData[] presNicData, ref bool hardwareChange)
    {
      Logger.LogError("PageManager", "NETconfig - DetectNetConfigChanges");
      if (oldNicData.Length != presNicData.Length)
      {
        Logger.LogError("PageManager", "NETconfig - There are "+presNicData.Length.ToString()+" network cards, before there were "+oldNicData.Length.ToString());
        hardwareChange = true;
        return true;
      }
      for (int i = 0; i < oldNicData.Length; i++)
      {
        int j;
        for (j = 0; j < presNicData.Length; j++)
        {
          if (oldNicData[i].MACAddress == presNicData[j].MACAddress)
            break;
        }
        if (j >= presNicData.Length)
        {
          Logger.LogError("PageManager", "NETconfig - The previous MAC address " + oldNicData[i].MACAddress + " was NOT found!");
          hardwareChange = true;
          return true;
        }
        else
        {
          //Same MAC address: check the IP addresses
          if (oldNicData[i].IPAddresses.Length != presNicData[j].IPAddresses.Length)
          {
            Logger.LogError("PageManager", "NETconfig - The card " + oldNicData[i].Name+ " with MAC address " + oldNicData[i].MACAddress + " had " + oldNicData[i].IPAddresses.Length.ToString() +
                            " IP addresses: now they are " + presNicData[j].IPAddresses.Length.ToString());
            hardwareChange = false;
            return true;
          }
          else
          {
            for (int k = 0; k < oldNicData[i].IPAddresses.Length; k++)
            {
              if (oldNicData[i].IPAddresses[k] != presNicData[j].IPAddresses[k])
              {
                Logger.LogError("PageManager", "NETconfig - The card " + oldNicData[i].Name + " with MAC address " + oldNicData[i].MACAddress + " had as IP address n." + (k + 1).ToString() + " " +
                                oldNicData[i].IPAddresses[k] + ": now it is " + presNicData[j].IPAddresses[k]);
                hardwareChange = false;
                return true;
              }
              if (oldNicData[i].SubnetMasks[k] != presNicData[j].SubnetMasks[k])
              {
                Logger.LogError("PageManager", "NETconfig - The card " + oldNicData[i].Name + " with MAC address " + oldNicData[i].MACAddress + " had as subnet n." + (k + 1).ToString() + " " +
                                oldNicData[i].SubnetMasks[k] + ": now it is " + presNicData[j].SubnetMasks[k]);
                hardwareChange = false;
                return true;
              }
            }
          }
          //Same MAC address: check the gateways
          if (oldNicData[i].Gateways.Length != presNicData[j].Gateways.Length)
          {
            Logger.LogError("PageManager", "NETconfig - The card " + oldNicData[i].Name + " with MAC address " + oldNicData[i].MACAddress + " had " + oldNicData[i].Gateways.Length.ToString() +
                            " gateways: now they are " + presNicData[j].Gateways.Length.ToString());
            hardwareChange = false;
            return true;
          }
          else
          {
            for (int k = 0; k < oldNicData[i].Gateways.Length; k++)
            {
              if (oldNicData[i].Gateways[k] != presNicData[j].Gateways[k])
              {
                Logger.LogError("PageManager", "NETconfig - The card " + oldNicData[i].Name + " with MAC address " + oldNicData[i].MACAddress + " had as gateway n." + (k + 1).ToString() + " " +
                                oldNicData[i].Gateways[k] + ": now it is " + presNicData[j].Gateways[k]);
                hardwareChange = false;
                return true;
              }
            }
          }
          //agrassi_FI14-01-09_1
          //Same MAC address: check the DNS
          if (oldNicData[i].DNS.Length != presNicData[j].DNS.Length)
          {
            Logger.LogError("PageManager", "NETconfig - The card " + oldNicData[i].Name + " with MAC address " + oldNicData[i].MACAddress + " had " + oldNicData[i].DNS.Length.ToString() +
                            " DNS: now they are " + presNicData[j].DNS.Length.ToString());
            hardwareChange = false;
            return true;
          }
          else
          {
            for (int k = 0; k < oldNicData[i].DNS.Length; k++)
            {
              if (oldNicData[i].DNS[k] != presNicData[j].DNS[k])
              {
                Logger.LogError("PageManager", "NETconfig - The card " + oldNicData[i].Name + " with MAC address " + oldNicData[i].MACAddress + " had as DNS n." + (k + 1).ToString() + " " +
                                oldNicData[i].DNS[k] + ": now it is " + presNicData[j].DNS[k]);
                hardwareChange = false;
                return true;
              }
            }
          }
          //Same MAC address: check the WINS
          if (oldNicData[i].WINS.Length != presNicData[j].WINS.Length)
          {
            Logger.LogError("PageManager", "NETconfig - The card " + oldNicData[i].Name + " with MAC address " + oldNicData[i].MACAddress + " had " + oldNicData[i].WINS.Length.ToString() +
                            " WINS: now they are " + presNicData[j].WINS.Length.ToString());
            hardwareChange = false;
            return true;
          }
          else
          {
            for (int k = 0; k < oldNicData[i].WINS.Length; k++)
            {
              if (oldNicData[i].WINS[k] != presNicData[j].WINS[k])
              {
                Logger.LogError("PageManager", "NETconfig - The card " + oldNicData[i].Name + " with MAC address " + oldNicData[i].MACAddress + " had as WINS n." + (k + 1).ToString() + " " +
                                oldNicData[i].WINS[k] + ": now it is " + presNicData[j].WINS[k]);
                hardwareChange = false;
                return true;
              }
            }
          }
        }
      }
      hardwareChange = false;
      return false;
    }

    //agrassi_FI08-01-09_4
    private static bool ConsistantConfig(FosberNICData[] nicData, bool twin400Only, string name)
    {
      Logger.LogError("PageManager", "NETconfig - ConsistantConfig on " + name + " (twin400Only=" + twin400Only.ToString() + ")");
      //check for the number of network cards
      bool nicNumberOK = false;
      bool overMaxNic = false;
      if (!twin400Only)
      {
        nicNumberOK = (nicData.Length >= NIC_NUMBER);
        overMaxNic = (nicData.Length > MAX_NIC);
      }
      else
      {
        nicNumberOK = (nicData.Length >= NIC_NUMBER_TWIN400);
        overMaxNic = (nicData.Length > MAX_NIC_TWIN400);
      }
      if (overMaxNic)
        Logger.LogError("PageManager", "NETconfig - " + nicData.Length + " network card!");
      else if (!nicNumberOK)
      {
        Logger.LogError("PageManager", "NETconfig - " + nicData.Length + " network card! Too few.");
        return false;
      }

      bool consistant = false;
      bool foundPC = false, foundGateway = false, foundDSP = twin400Only;
      try
      {
        //check for IP_PC (with gateway) and IP_DSP as first addresses
        for (int i = 0; i < nicData.Length; i++)
        {
          if (!twin400Only)
          {
            if (nicData[i].IPAddresses.Length > 0)
            {
              if (nicData[i].IPAddresses[0] == IP_DSP)
              {
                if (nicData[i].SubnetMasks[0] == SUBNET_DSP_PC)
                {
                  foundDSP = true;
                  Logger.LogError("PageManager", "NETconfig - Found " + IP_DSP + " in " + name);
                }
              }
            }
          }
          if (nicData[i].IPAddresses.Length > 0)
          {
            if (nicData[i].IPAddresses[0] == (twin400Only ? IP_PC_PMAC400 : IP_PC))
            {
              if (nicData[i].SubnetMasks[0] == SUBNET_DSP_PC)
              {
                foundPC = true;
                Logger.LogError("PageManager", "NETconfig - Found " + (twin400Only ? IP_PC_PMAC400 : IP_PC) + " in " + name);
              }
              if (nicData[i].Gateways.Length > 0)
              {
                if (nicData[i].Gateways[0] == DEF_GATEWAY_PC)
                {
                  foundGateway = true;
                  Logger.LogError("PageManager", "NETconfig - Found gateway" + DEF_GATEWAY_PC + " in " + name);
                }
              }
            }
          }
        }
      }
      catch (Exception e)
      {
        foundDSP = foundPC = foundGateway = false;
        Logger.LogError("PageManager", "NETconfig - ConsistantConfig on " + name + " error: "+e.Message);
      }
      //agrassi_FI13-04-2010_1
      //consistant = foundDSP && foundPC && foundGateway;
      consistant = foundDSP && foundPC;
      if (consistant)
        Logger.LogError("PageManager", "NETconfig - " + name + " is consistant");
      else
        Logger.LogError("PageManager", "NETconfig - " + name + " is NOT consistant: DSP="+foundDSP.ToString()+", PC="+foundPC.ToString()+", Gateway="+foundGateway.ToString());
      return consistant;
    }

    //PMASSARO_FI04/02/09 - Sets AutoAdminLogON 
    private static void SetAutoAdminLogON(string usr)
    {
      string strPSW = "FOSBER";
      string logOnEnabled = "1";

      try
      {
        RegistryKey rw = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Windows NT").OpenSubKey("CurrentVersion").OpenSubKey("Winlogon");
        if (rw != null)
        {
          if (rw.GetValue("AutoAdminLogon") != null)
          {
            if (rw.GetValue("AutoAdminLogon").ToString() == logOnEnabled)
            {
              Logger.LogError("PageManager", "SetAutoAdminLogON -  'Hkey_Local_Machine\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\WinLogon\\AutoAdminLogOn' already present and is Enabled. I Don't do nothing ");
              rw.Close();
              return;
            }
            else
            {
              Logger.LogError("PageManager", "SetAutoAdminLogON -  'Hkey_Local_Machine\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\WinLogon\\AutoAdminLogOn' already present but is NOT Enabled. I continue to analize (maybe is a new PC)");
            }
          }
          else
          {
            Logger.LogError("PageManager", "SetAutoAdminLogON - Registry 'Hkey_Local_Machine\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\WinLogon\\AutoAdminLogOn' is NULL. I continue to analize (maybe is a new PC)");
          }

          if (rw.GetValue("DefaultPassword") != null)
          {
            Logger.LogError("PageManager", "SetAutoAdminLogON - A Password is already set into registry 'Hkey_Local_Machine\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\WinLogon\\DefaultPassword' ( = " + rw.GetValue("DefaultPassword").ToString() + " ) I don't do nothing more (maybe is an old spare PC)");
            rw.Close();
            return;
          }
          else
          {
            Logger.LogError("PageManager", "SetAutoAdminLogON - Registry 'Hkey_Local_Machine\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\WinLogon\\DefaultPassword' is empty. I try to set Autoadminlogon (maybe is a new PC)");
          }
        }
        else
        {
          Logger.LogError("PageManager", "SetAutoAdminLogON -  Value NULL into 'Hkey_Local_Machine\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\WinLogon'!. I try to create it (maybe is a new PC)");
        }
        rw.Close();

        rw = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon");
        if (rw == null)
        {
          Logger.LogError("PageManager", "SetAutoAdminLogON -  Problem creating 'Hkey_Local_Machine\\SOFTWARE\\Microsoft\\Windwos NT\\CurrentVesion\\WinLogon'!. I can't do nothing more");
          rw.Close();
          return;
        }
        else
        {
          //these are our hero like values here
          //simply use your RegistryKey objects SetValue method to set these keys
          rw.SetValue("AutoAdminLogon", "1");
          rw.SetValue("DefaultUserName", usr);
          rw.SetValue("DefaultPassword", strPSW);
          rw.Close();
          return;
        }
      }
      catch (Exception e)
      {
        Logger.LogError("PageManager", "SetAutoAdminLogON - Error: " + e.Message);
      }
    }

    //agrassi_FI08-01-09_4
    private static void CheckNICData(bool onServer, string serverConfigPath, bool twin400Only, bool enableNetConfigManager)
    {
      if (!onServer)
        return;
      Logger.LogError("PageManager", "NETconfig - CheckNICData");
      if (!enableNetConfigManager)
      {
        Logger.LogError("PageManager", "NETconfig - NetConfigManager NOT enabled");
        return;
      }
      try
      {
        string fileName = serverConfigPath + NET_CONFIG_FILE;
        string fileNameOld = serverConfigPath + NET_CONFIG_FILE_OLD;
        bool existFile = System.IO.File.Exists(fileName);
        bool anyHardwareChange = false;
        bool anyChange = false;
        FosberNICData[] presNicData = null;
        ReadPresentNetCards(ref presNicData);
        bool presentConsistant = ConsistantConfig(presNicData, twin400Only, "Present config");
        if (existFile)
        {
          FosberNICData[] oldNicData = null;
          ReadNICData(fileName, ref oldNicData);
          bool previousConsistant = ConsistantConfig(oldNicData, twin400Only, "Previous config");
          anyChange = DetectNetConfigChanges(oldNicData, presNicData, ref anyHardwareChange);
          if (anyHardwareChange && previousConsistant)
          {
            Logger.LogError("PageManager", "NETconfig - There have been hardware changes: reload previous network config!");
            ReloadNicData(ref presNicData, oldNicData, twin400Only);
          }
        }
        if ((!existFile) || anyChange)
        {
          if (existFile)
          {
            if (System.IO.File.Exists(fileNameOld))
              System.IO.File.Delete(fileNameOld);
            System.IO.File.Move(fileName, fileNameOld);
          }
          SaveNICData(presNicData, fileName);
        }
      }
      catch (Exception e)
      {
        Logger.LogError("PageManager", "NETconfig - CheckNICData error: " + e.Message);
      }
    }

    //agrassi_FI08-01-09_4
    private static void ReloadNicData(ref FosberNICData[] presNicData, FosberNICData[] oldNicData, bool twin400Only)
    {
      Logger.LogError("PageManager", "NETconfig - ReloadNicData");
      try
      {
        bool[] found = new bool[oldNicData.Length];
        bool[] used = new bool[presNicData.Length];
        int mainCardIndex=0;
        for (int i = 0; i < oldNicData.Length; i++)
        {
          int j;
          for (j = 0; j < presNicData.Length; j++)
          {
            if (oldNicData[i].IPAddresses[0] == presNicData[j].IPAddresses[0])
            {
              if (presNicData[j].IPAddresses[0]==(twin400Only?IP_PC_PMAC400:IP_PC))
                mainCardIndex=j;
              break;
            }
          }
          if (j < presNicData.Length)
          {
            ConfigureNIC(ref presNicData[j], oldNicData[i], false);
            found[i] = true;
            used[j] = true;
          }
          else
          {
            Logger.LogError("PageManager", "NETconfig - ReloadNicData: IP " + oldNicData[i].IPAddresses[0] + " not found");
          }
        }
        int cntNotFound = 0;
        for (int i = 0; i < found.Length; i++)
          if (!found[i])
            cntNotFound++;
        if (cntNotFound>0)
        {
          int cntNotUsed = 0;
          for (int i = 0; i < used.Length; i++)
            if (!used[i])
              cntNotUsed++;
          for (int i = 0; i < found.Length; i++)
          {
            if (!found[i])
            {
              int j;
              for (j = 0; j < used.Length; j++)
              {
                if (!used[j])
                {
                  ConfigureNIC(ref presNicData[j], oldNicData[i], false);
                  used[j] = true;
                  found[i] = true;
                  break;
                }
              }
              if (j >= used.Length)
              {
                //not found a not used card
                for (int k=0; k<oldNicData[i].IPAddresses.Length; k++)
                  Logger.LogError("PageManger", "NETconfig - There aren't other NIC: IP " + oldNicData[i].IPAddresses[k] + " has not been configured");
                //ConfigureNIC(ref presNicData[mainCardIndex], oldNicData[i], true);
                //found[i] = true;
              }
              //found[i] = true;
            }
          } 
        }
        if (reboot)
        {
          MessageBox.Show("Network cards settings have been changed.\r\nRestart the computer to make it work properly.", "Syncro - Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          Application.Exit();
        }
      }
      catch (Exception e)
      {
        Logger.LogError("PageManager", "NETconfig - ReloadNicData error: " + e.Message);
      }
    }

    //agrassi_FI08-01-09_4
    private static void ConfigureNIC(ref FosberNICData destNicData, FosberNICData srcNicData, bool addConfig)
    {
      Logger.LogError("PageManager", "NETconfig - ConfigureNIC " + destNicData.Name + " (MAC=" + destNicData.MACAddress+ ", ID=" + destNicData.ID + ")");
      try
      {
        RegistryKey rw = Registry.LocalMachine.OpenSubKey("System").OpenSubKey("CurrentControlSet").OpenSubKey("Services").
          OpenSubKey("Tcpip").OpenSubKey("Parameters").OpenSubKey("Interfaces").OpenSubKey(destNicData.ID, true);
        if (rw != null)
        {
          string logStr = "Set NIC " + destNicData.Name + " " + destNicData.ID + ": DHCP=Off, ";
          rw.SetValue("EnableDHCP", 0, RegistryValueKind.DWord);
          int ipOldLength = 0;
          string[] ipOld = null;
          string[] maskOld = null;
          if (addConfig)
          {
            ipOld = (string[])rw.GetValue("IPAddress");
            maskOld = (string[])rw.GetValue("SubnetMask");
            ipOldLength = ipOld.Length;
          }
          string[] ips = new string[srcNicData.IPAddresses.Length + ipOldLength], masks = new string[srcNicData.IPAddresses.Length + ipOldLength];
          for (int i = 0; i < ips.Length; i++)
          {
            if (i < ipOldLength)
            {
              ips[i] = ipOld[i];
              masks[i] = maskOld[i];
            }
            else
            {
              ips[i] = srcNicData.IPAddresses[i-ipOldLength];
              masks[i] = srcNicData.SubnetMasks[i-ipOldLength];
            }
            logStr += "IP=" + ips[i] + ", Sub=" + masks[i] + ", ";
          }
          rw.SetValue("IPAddress", ips, RegistryValueKind.MultiString);
          rw.SetValue("SubnetMask", masks, RegistryValueKind.MultiString);
          destNicData.IPAddresses = new string[ips.Length];
          ips.CopyTo(destNicData.IPAddresses, 0);
          destNicData.SubnetMasks = new string[masks.Length];
          masks.CopyTo(destNicData.SubnetMasks, 0);
          int gwOldLength = 0;
          string[] gwOld = null;
          if (addConfig)
          {
            gwOld = (string[])rw.GetValue("DefaultGateway");
            gwOldLength = gwOld.Length;
          }
          if (srcNicData.Gateways.Length + gwOldLength> 0)
          {
            string[] gw = new string[srcNicData.Gateways.Length + gwOldLength];
            for (int i = 0; i < gw.Length; i++)
            {
              if (i < gwOldLength)
                gw[i] = gwOld[i];
              else
                gw[i] = srcNicData.Gateways[i - gwOldLength];
              logStr += "Gw=" + gw[i] + ", ";
            }
            rw.SetValue("DefaultGateway", gw, RegistryValueKind.MultiString);
            destNicData.Gateways = new string[gw.Length];
            gw.CopyTo(destNicData.Gateways, 0);
          }
          Logger.LogError("PageManager", "NETconfig - " + logStr);
          if (!reboot)
            reboot = true;
        }
        else
          Logger.LogError("PageManager", "NETconfig - Registry error!");
      }
      catch (Exception e)
      {
        Logger.LogError("PageManager", "NETconfig - ConfigureNIC error: " + e.Message);
      }
    }

    //agrassi_FI08-01-09_4
    private static void ReadNICData(string fileName, ref FosberNICData[] nicData)
    {
      Logger.LogError("PageManager", "NETconfig - ReadNICData");
      try
      {
        const int MAX_ITER = 5;
        int nicCount = 0, ipCount = 0, gatewayCount = 0;
        string notPresent = "__DEFAULT__";
        nicData = null;
        for (int i = 1; i <= MAX_ITER; i++)
        {
          if (!Ini.GetNoRebuild("NIC_" + i.ToString(), "Name", notPresent, fileName).Equals(notPresent))
            nicCount++;
          else
            break;
        }
        nicData = new FosberNICData[nicCount];
        for (int i = 1; i <= nicCount; i++)
        {
          string section = "NIC_" + i.ToString();
          nicData[i - 1].Name = Ini.GetNoRebuild(section, "Name", notPresent, fileName);
          nicData[i - 1].MACAddress = Ini.GetNoRebuild(section, "MACaddress", notPresent, fileName);
          nicData[i - 1].ID = Ini.GetNoRebuild(section, "ID", notPresent, fileName);
          //agrassi_FI14-01-09_1
          //nicData[i - 1].IndexIntoRegistry = int.Parse(Ini.GetNoRebuild(section, "IndexIntoRegistry", notPresent, fileName));
          ipCount = 0;
          for (int j = 1; j <= MAX_ITER; j++)
          {
            if (!Ini.GetNoRebuild(section, "IPAddress_" + j.ToString(), notPresent, fileName).Equals(notPresent))
              ipCount++;
            else
              break;
          }
          nicData[i - 1].IPAddresses = new string[ipCount];
          nicData[i - 1].SubnetMasks = new string[ipCount];
          for (int j = 1; j <= ipCount; j++)
          {
            nicData[i - 1].IPAddresses[j - 1] = Ini.GetNoRebuild(section, "IPAddress_" + j.ToString(), notPresent, fileName);
            nicData[i - 1].SubnetMasks[j - 1] = Ini.GetNoRebuild(section, "SubnetMask_" + j.ToString(), notPresent, fileName);
          }
          gatewayCount = 0;
          for (int j = 1; j <= MAX_ITER; j++)
          {
            if (!Ini.GetNoRebuild(section, "Gateway_" + j.ToString(), notPresent, fileName).Equals(notPresent))
              gatewayCount++;
            else
              break;
          }
          nicData[i - 1].Gateways = new string[gatewayCount];
          for (int j = 1; j <= gatewayCount; j++)
          {
            nicData[i - 1].Gateways[j - 1] = Ini.GetNoRebuild(section, "Gateway_" + j.ToString(), notPresent, fileName);
          }
        }
      }
      catch (Exception e)
      {
        Logger.LogError("PageManager", "NETconfig - ReadNICData error: " + e.Message);
      }
    }

    /// <summary>
    /// starts all the application modules at the first tick of the timer
    /// </summary>
    private void timer_Start_Tick(object sender, System.EventArgs e)
		{
			timer_Start.Enabled = false;

			progressBar1.Percentage = 5;

			setLbl_State("loading Settings. . .", Color.Black);
			settings = Settings.getSettings();
      //agrassi_FI15-10-08_1
      //agrassi_FI26-11-08
      //this.CheckPCSettings(settings.configuration.getOnFosberNetwork());
      LauyoutComponents();
			this.Refresh();
			pagesHistorian = new PagesHistorian();
			progressBar1.Percentage = 20;

			setLbl_State("starting Logger. . .", Color.Black);
			this.Refresh();
			logger = Logger.startLogger();
      //agrassi_FI26-11-08
      Logger.LogError(this, "******************************************************");
      Logger.LogError(this, "**************** SYNCRO CLIENT START *****************");
      Logger.LogError(this, "******************************************************");
      progressBar1.Percentage = 40;

      setLbl_State("starting RTCManager. . .", Color.Black);
			this.Refresh();
			rtcManager = RTCManager.getRTCManager();
      progressBar1.Percentage = 60;

      if ((!settings.dataBase.RestoreDBLanguages) || (!settings.configuration.getOnServer()))
      {
        setLbl_State("starting CommunicationManager. . .", Color.Black);
        this.Refresh();
        communicationManager = CommunicationManager.GetCommunicationManager();
        if (communicationManager.ConnectionRefused)
        {
          Logger.LogError(this, "Connection refused!! Exit with warning about licence");
          MessageBox.Show("The connection has been refused\r\nbecause the licence is required.\r\nPlease contact Fosber.", "You need a licence!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          Application.ExitThread();
          Application.Exit();
          return;
        }
        progressBar1.Percentage = 80;
      }

      setLbl_State("starting DBManager. . .", Color.Black);
      progressBar1.Percentage = 100;
      this.Refresh();
      dBManager = DBManager.getDBManager();
      //agrassi_FI08-01-09_4
      //this.CheckPCSettings(settings.configuration.getOnFosberNetwork(), settings.configuration.getOnServer(), settings.paths.ServerConfig, dBManager.syncroConfig.IsTwin400Only());
      this.CheckPCSettings(settings.configuration.getOnFosberNetwork(), settings.configuration.getOnServer(), settings.paths.ServerConfig, 
        dBManager.syncroConfig.IsTwin400Only(), settings.network.NetConfigManagerEnable);

      //initialize the alarmtimer to show blinking alarm on stateButtons
		  StateButton.initializeAlarmTimer();

      if (this.RestoreLanguage())
        return;

			//start server command executor thread
			threadCommandExec = new Thread(new ThreadStart(CommandExec));
			threadCommandExec.Name = "CommandExec";
			threadCommandExec.Start();
            OrderProxy.start(communicationManager, dBManager);
			//log on the windows event log the start of the application
      //agrassi_FI19-12-08
			//Logger.LogWindowsEvent(this, "timer1_Tick", "Application started correctly.", EventLogEntryType.Information);

			//request the status bar and first page to the server.
			communicationManager.SendTcp(communicationManager.SYNCROS_PAGE_MANAGER, pagesHistorian.CurrentStatusBar);
      
      if (simulationSyncroStart)
        communicationManager.SendTcp(communicationManager.SYNCROS_PAGE_MANAGER, "settings_SimulationGeneral");
      else if (simulationTwin400Start)
        communicationManager.SendTcp(communicationManager.SYNCROS_PAGE_MANAGER, "twin400_simulations");
      else
        communicationManager.SendTcp(communicationManager.SYNCROS_PAGE_MANAGER, pagesHistorian.FirstPage);

      if (Screen.PrimaryScreen.Bounds.Width > 1280)
      {
        if (Settings.getSettings().configuration.getOnFosberNetwork())
          communicationManager.SendTcp(communicationManager.SYNCROS_PAGE_MANAGER, PageManager.getRequestCommand(typeof(Frm_LateralBar)));
        else if (SyncroC.Utility.Accessibility.getAccessibility().IamASuperUser())
          communicationManager.SendTcp(communicationManager.SYNCROS_PAGE_MANAGER, PageManager.getRequestCommand(typeof(Frm_LateralBar)));
      }

      //Once every modules has been started the commFailure page can be showed when needed.
      //It is kept in always in memory so that when it is not in memory it means that cannot
      //be showed and every communication failure message will be sent to the pageManager form.
      commFailure = new Frm_CommFailure();
    }

    private bool RestoreLanguage()
    {
      //rebuild languages database only if it is enabled (.ini) AND onServer.
      //The DBManager.language.Update() routine must be called here before instantiate any
      //forms in order to eventually detect any kind of text changing into the pages and then 
      //correct the database.
      if (settings.dataBase.RestoreDBLanguages && settings.configuration.getOnServer())
      {
        setLbl_State("Restoring DB Languages . . .", Color.Red);
        this.Refresh();
        DateTime start = DateTime.Now;
        bool ret = dBManager.language.Update();
        string text = "timer1_Tick - languages database has" +
          (ret ? " " : " NOT ") +
          "been restored correctly in " + ((DateTime.Now.Ticks - start.Ticks) / 10000).ToString() + " milliseconds";
        Settings.getSettings().dataBase.RestoreDBLanguages = false;
        Logger.LogLanguages(this, text);
        Frm_Alert alert = new Frm_Alert();
        alert.ShowDialog(this, text);
        Application.ExitThread();
        Application.Exit();
        return true;
      }
      return false;
    }

		//TODO: following lines are for debug, remove
		//TimeSpan tfile;
		//bool b = true;
		/// <summary>
		/// 
		/// </summary>
		private void CommandExec()
		{
			int CHECK_FOR_COMMANDS_IN_QUEUE_FREQUENCY = 1;	//milliseconds frequency to wait before next loop when all queues are empty
			int PAGE_MANAGER_COMMAND_PRIORITY = 30;					//max page manager commands to be executed for each loop
			int DB_MANAGER_COMMAND_PRIORITY = 3;						//max db manager commands to be executed for each loop
			int RTC_MANAGER_COMMAND_PRIORITY = 2;						//max rtc manager commands to be executed for each loop
			//TODO: following lines are for debug, remove
	//		int MAX_QUEUE_LENGTH_FOR_LOG = 100;
			while (true)
			{
				if (communicationManager.PMcommandsQueue.Count == 0
					&& communicationManager.DBMcommandsQueue.Count == 0
					&& communicationManager.RTCMcommandsQueue.Count == 0)
				{	//all commands queues are empty
					System.Threading.Thread.Sleep(CHECK_FOR_COMMANDS_IN_QUEUE_FREQUENCY);
				}
				else
				{	//at least one command queue is not empty

					//TODO: following lines are for debug, remove
					//if ((communicationManager.PMcommandsQueue.Count > MAX_QUEUE_LENGTH_FOR_LOG) ||
					//    (communicationManager.DBMcommandsQueue.Count > MAX_QUEUE_LENGTH_FOR_LOG) ||
					//    (communicationManager.RTCMcommandsQueue.Count > MAX_QUEUE_LENGTH_FOR_LOG))
					//{
					//  Logger.LogError(this, "CommandExec - PMcommandsQueue.Count = " + communicationManager.PMcommandsQueue.Count.ToString() +
					//    "; DBMcommandsQueue.Count = " + communicationManager.DBMcommandsQueue.Count.ToString() +
					//    "; RTCMcommandsQueue.Count = " + communicationManager.RTCMcommandsQueue.Count.ToString());
					//}

					if (communicationManager.PMcommandsQueue.Count > 0)
					{
						for (int i = 0;
							i < PAGE_MANAGER_COMMAND_PRIORITY && communicationManager.PMcommandsQueue.Count > 0;
							i++)
						{
							TCPMessage message = (TCPMessage)communicationManager.PMcommandsQueue.Dequeue();
							if (message.header.Length != 2)
								Logger.LogError(this, "CommandExec - PM - Wrong header \"" + message.header + "\".");
							else
							{
								try
								{
									if (message.header[1].Equals('R'))
									{//PageManager command to be interpreted by reflection
										this.Invoke(new UIMethod1(this.Reflect), new Object[] { message.text });
									}
									else
									{//PageManager command to be interpreted by a switch
										this.Invoke(new UIMethod1(this.Switch), new Object[] { message.text });
									}
								}
								catch (Exception ex)
								{
									string logMsg = "CommandExec - PM - ";
									if (message.text != null)
										logMsg += "a problem occurred during execution of command: \"" + message.text + "\".";
									Logger.LogError(this, logMsg, ex);
								}
							}
						}
					}

					if (communicationManager.DBMcommandsQueue.Count > 0)
					{
						for (int i = 0;
							i < DB_MANAGER_COMMAND_PRIORITY && communicationManager.DBMcommandsQueue.Count > 0;
							i++)
						{
							TCPMessage message = (TCPMessage)communicationManager.DBMcommandsQueue.Dequeue();
							if (message.header.Length != 2)
								Logger.LogError(this, "CommandExec - DB - Wrong header \"" + message.header + "\".");
							else
							{
								try
								{
									DBManager dbm = DBManager.getDBManager();
									if (message.header[1].Equals('R'))
									{	//DBManager command to be interpreted by reflection
										DateTime now = DateTime.Now;	
										this.Invoke(new UIMethod1(dbm.Reflect), new Object[] { message.text });
										TimeSpan interval = DateTime.Now - now;
										if (interval.TotalSeconds > 5)
											Logger.LogError(this, "CommandExec - command: \"" + message.text + "\" took " + (interval.TotalMilliseconds.ToString() + " milliseconds."));
									}
									else
									{	//DBManager command to be interpreted by a switch
										this.Invoke(new UIMethod1(dbm.Collect), new Object[] { message.text });
									}
								}
								catch (Exception ex)
								{
									string logMsg = "CommandExec - DB - ";
									if (message.text != null)
										logMsg += "a problem occurred during execution of command: \"" + message.text + "\".";
									Logger.LogError(this, logMsg, ex);
								}
							}
						}
					}

					if (communicationManager.RTCMcommandsQueue.Count > 0)
					{
						for (int i = 0;
							i < RTC_MANAGER_COMMAND_PRIORITY && communicationManager.RTCMcommandsQueue.Count > 0;
							i++)
						{
							TCPMessage message = (TCPMessage)communicationManager.RTCMcommandsQueue.Dequeue();
							if (message.header.Length != 2)
								Logger.LogError(this, "CommandExec - RTC - Wrong header \"" + message.header + "\".");
							else
							{
								try
								{
									RTCManager rtc = RTCManager.getRTCManager();
									if (message.header[1].Equals('R'))
									{	//AlarmManager command to be interpreted by reflection
										this.Invoke(new UIMethod1(rtc.Reflect), new Object[] { message.text });
									}
									else
									{	//AlarmManager command to be interpreted by a switch
										this.Invoke(new UIMethod1(rtc.Collect), new Object[] { message.text });
									}
								}
								catch (Exception ex)
								{
									string logMsg = "CommandExec - RTC - ";
									if (message.text != null)
										logMsg += "a problem occurred during execution of command: \"" + message.text + "\".";
									Logger.LogError(this, logMsg, ex);
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Show that a communication error between the client and the server occurred.
		/// If it is possible the message is showed on the Frm_CommFailure page, if it is 
		/// not possible it is showed on the PageManager form.
		/// </summary>
		/// <param name="msg">the error message to be showed</param>
		/// <param name="append">true means the message is to be appended to the previous one
		/// false means the previous message is to be overwritten.</param>
		public void CommFailure(string msg, bool append)
		{
			if (commFailure == null)
			{	//the Frm_CommFailure form has not been instanciated yet because every client module
				//arent ready so the message is being showed on the PageManager form.
				setLbl_State(msg, Color.Red);
        if (!this.ControlBox)
          if (!this.timer_Closable.Enabled)
            this.timer_Closable.Enabled = true;
        Application.DoEvents();
      }
			else
			{
				//the Frm_CommFailure form is instanciated so the message is showed on it.
				commFailure.setTxt_Description(msg, append);
				commFailure.Show();
				commFailure.Focus();
			}
		}

		/// <summary>
		/// To be called when the communication beetwen client and server has been restored
		/// after an error. 
		/// </summary>
    public void CommRestored()
    {
      if (commFailure == null)
      {
        setLbl_State("", Color.Black);
        this.timer_Closable.Enabled = false;
        this.ControlBox = false;
      }
      else
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();

        //ABELLO_FI26/05/10 Ogni tanto non si vedeva la Home page sull'accensione
        if (pagesHistorian.GetCurrentPage() != "")
          cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pagesHistorian.GetCurrentPage());
        else
          if (pagesHistorian.FirstPage != "")
            cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pagesHistorian.FirstPage);
          else
            cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getRequestCommand(typeof(Frm_HomeDry)));

        if (pagesHistorian.CurrentStatusBar != "")
          cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pagesHistorian.CurrentStatusBar);
        else
          cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getRequestCommand(typeof(Frm_StatusBarDryEnd)));

        if ((Screen.PrimaryScreen.Bounds.Width > 1280) && Settings.getSettings().configuration.getOnFosberNetwork())
          cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getRequestCommand(typeof(Frm_LateralBar)));

        commFailure.Hide();
        commFailure.setTxt_Description("", false);
      }
    }

		/// <summary>
		/// PageManager collector for commands to be interpred by a switch
		/// </summary>
		/// <param name="msg">a string representing the command to be interpreted</param>
		public void Switch(string msg)
		{
			if (msg == null || msg.Equals(""))
			{
				Logger.LogError(this, "Switch - msg parameter was null or empty.");
				return;
			}

      //graffaelli_FI09/11/2011_2 - Aggiunto log su client
      Logger.LogError(this, "Switch - PM - msg parameter: " + msg);

			int delimiterPosition = msg.IndexOf('(');
			if (delimiterPosition == -1)
			{
				Logger.LogError(this, "Switch - invalid command \"" + msg + "\"; '(' is missing");
				return;
			}
			string command = msg.Substring(0, delimiterPosition);
			string parametersStr = msg.Substring(delimiterPosition + 1);
			if (! parametersStr.EndsWith(")"))
			{
				Logger.LogError(this, "Switch - invalid command \"" + msg +"\"; final ')' is missing.");
				return;
			}
			parametersStr = parametersStr.Substring(0, parametersStr.Length - 1); //remove the final ')'
			string[] parameters = 
				(parametersStr.Length == 0) ?
				new String[0] :
				parametersStr.Split(new Char[]{','});

			if (command.Equals("new"))
			{
				if (parameters.Length != 1)
				{
					Logger.LogError(this,"Switch - invalid command \"" + msg + "\" - new must take only one parameter.");
					return;
				}
				else
				{
					Form newForm = getForm(parameters[0].Trim());				
					if (! (newForm is Frm_Base))
					{
						Logger.LogError(this, "Switch - the new command can work only with Frm_Base form.");
					}
					else
					{
						((Frm_Base)newForm)._Show();
					}
				}
			}

				//the close command is only for pop up
				//the other page are automatically disposed when 
				//discarged from the cache
			else if (command.Equals("close"))
			{
				if (parameters.Length != 1)
				{
					Logger.LogError(this,"Switch - invalid command \"" + msg + "\" - close must take one parameter.");
					return;
				}
				else
				{
					Form form = getForm(parameters[0].Trim());
					if (! (form is Frm_Base))
					{
						Logger.LogError(this, "Switch - the close command can work only with Frm_Base form.");
					}
					if (! Type.GetType("SyncroC.Forms.Frm_PopUp").IsAssignableFrom(form.GetType())) 	
					{
						Logger.LogError(this,"Switch - cannot close the form \""+ parameters[0] + "\" because it is not a pop up");
						return;
					}
					form.Dispose();
					FieldInfo formInfo = searchForm(parameters[0].Trim());
					formInfo.SetValue(this, null);
				}
			}
		}
			
		/// <summary>
		/// PageManager collector for commands to be interpreted by the reflection
		/// </summary>
		/// <param name="msg">a string representing the command to be interpreted</param>
		public void Reflect(string message)
		{
			if (message == null || message.Equals(""))
			{
				Logger.LogError(this,"Reflect - msg parameter was null or empty.");
				return;
			}

      //graffaelli_FI09/11/2011_2 - Aggiunto log su client
      Logger.LogError(this, "Reflect - PM - msg parameter: " + message);

			string msg = message;

			//first part is always a form: look for the form
			int index = msg.IndexOf('.');
			if (index == -1)
			{
				Logger.LogError(this,"Reflect - Invalid command \"" + message + "\".");
				return; 
			}
			string formName = msg.Substring(0, index);
			Form form = getForm(formName);
			if (form == null) 
			{
				Logger.LogError(this,"Reflect - form \"" + msg.Substring(0, index) + "\" does not exist into the PageManager.");
				return;
			}
			msg = msg.Substring(index + 1);
			
			//second part
			index = msg.IndexOfAny(new char[]{'.','=','('});
			if (index == -1)
			{
				Logger.LogError(this,"Reflect - Invalid command \"" + message + "\".");
				return; 
			}
			switch (msg[index])
			{
				case '(':
					//form's method
					if (! msg.EndsWith(")")) 
					{
						Logger.LogError(this,"Reflect - invalid command \"" + message +" \")\" is missing." );
						return; 
					}
					msg = msg.Substring(0, msg.Length - 1);	//remove the final ')'
					invokeMethod(form, msg.Substring(0, index), msg.Substring(index + 1));//form, method name, coded parameters
					break;
				case '=':
					//form's property					
					setProperty(form, msg.Substring(0, index), msg.Substring(index + 1));
					break;
				case '.':
					//form's control
					Control control = getControl(form, msg.Substring(0, index));
					if (control == null) 
					{
						Logger.LogError(this,"Reflect - control \"" + msg.Substring(0, index) + "\" does not exist into the form \"" + formName + "\" or it has not been instantiated yet.");
						return;
					}

          //gestione del doppio oggetto
          string msgDoppioOggetto = msg.Substring(index + 1);

          //PMASSARO_FI30704/11 - Migliorata la gestione del doppio oggetto all'interno della Reflection 
          //Dobbiamo eseguire il controllo successivo sun una strigna che non contiene ci� che � dopo "=" perch� il valore da associare
          //all'oggetto potrebbe corrispondere a una stringa che contiene il "." (Esempio: "program + seq" come succedeva a ST Just Malmont) 
          /*
          if (msgDoppioOggetto.Contains("."))
          {
            control = getControl(control, msgDoppioOggetto.Substring(0, msgDoppioOggetto.IndexOf('.')));
            msg = msgDoppioOggetto;
            index = msgDoppioOggetto.IndexOf('.');
          }
          */
          string bckMsgDoppioOggetto = msgDoppioOggetto;
          int nextEqual = msgDoppioOggetto.IndexOf("=");
          if (nextEqual > 0)
            msgDoppioOggetto = msgDoppioOggetto.Substring(0, nextEqual);

          if (msgDoppioOggetto.Contains("."))
          {
            control = getControl(control, msgDoppioOggetto.Substring(0, msgDoppioOggetto.IndexOf('.')));
            msg = bckMsgDoppioOggetto; //Associo il msgDoppioOggetto perch� altrimenti non funzionerebbero i controlli successivi 
            index = msg.IndexOf('.');
          }

          msg = msg.Substring(index + 1);
					index = msg.IndexOfAny(new char[]{'=','('});
					if (index == -1)
					{
						Logger.LogError(this,"Reflect - invalid command \"" + message +" \")\" is missing." );
						return; 
					}
					if (msg[index].Equals('='))
						//control's property
						setProperty(control, msg.Substring(0, index), msg.Substring(index + 1));
					else
					{
						//control's method
						if (! msg.EndsWith(")"))
						{
							Logger.LogError(this,"Reflect - invalid command \"" + message + " \" final ')' is missing." );
							return; 
						}
						msg = msg.Substring(0, msg.Length - 1);//remove the final ')'
						invokeMethod(control, msg.Substring(0, index), msg.Substring(index + 1));//control, method name, coded parameters
					}
					break;
			}
		}

		/// <summary>
		/// looks for the form into the PageManager
		/// </summary>
		/// <param name="formName">the name of the form to look for</param>
		/// <returns></returns>
		private FieldInfo searchForm(string formName)
		{
			return this.GetType().GetField(formName, 
				BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.IgnoreCase);
		}

		/// <summary>
		/// Looks for the form into the PageManager.
		/// If the form exists but it has not been instantiated yet then 
		/// this method instantiate the form. If the form does not exist null is returned.
		/// </summary>
		/// <param name="formName">the name of the form to look for</param>
		/// <returns></returns>
		private Form getForm(string formName)
		{
			//search the form to operate to
			FieldInfo formInfo = searchForm(formName);
			if (formInfo == null) return null; //form does not exist
					
			Type formType = formInfo.FieldType;
			Form form = (Form)formInfo.GetValue(this);
			if (form == null)
			{
				//requested page has not been instantiated yet: instantiation
				object[] parameters = null;
				if ( IsIndexedPageGroup(formType))
				{
					//int	index = Convert.ToInt32(formName[formName.Length -1].ToString());
					string indexStr = ExtractIndexFromFormName(formName);
					int index = 0;
					try
					{
						index = Convert.ToInt32(indexStr);
					}
					catch (Exception ex)
					{
						Logger.LogError(this, "getForm - InvalidIndexExtracted \"" + indexStr + "\" from string \"" + formName + "\"", ex);
					}
					parameters = new Object[]{index};
				}
					
				formInfo.SetValue(this, Activator.CreateInstance(formType, parameters)); 
				form = (Frm_Base)formInfo.GetValue(this);
			}
			return form;
		}

		/// <summary>
		/// Supposed to be called into the Frm_Page._Show() method.
		/// Keep the forms cache consistent 
		/// </summary>
		/// <param name="page">the last accessed form</param>
		public void UpdateCache(Frm_Page page)
		{
			UpdateCache(page, 0);
		}

		/// <summary>
		/// Supposed to be called into the Frm_Page._Show() method.
		/// Keep the forms cache consistent 
		/// </summary>
		/// <param name="page">the last accessed form</param>
		/// <param name="index">The index of the group which the page belong, 0 for non indexed groups</param>
		public void UpdateCache(Frm_Page page, int index)
		{
			string formName;
			if (index == 0)
				formName = page.getRequestCommand();
			else
				formName = page.getRequestCommand(index);

			if (cache.Contains(formName))
			{
				cache.Remove(formName);
				cache.Add(formName);
				return;
			}

			int i = 0;
			while ( (cache.Count >= Settings.getSettings().forms.MaxCacheSize)
				&& (i < cache.Count) )
			{
				string closingFormName = (string)cache[i]; 
				
				if (closingFormName == null)
					Logger.LogError(this, "UpdateCache - problems dequeuing the form " + closingFormName + " from the cache.");
				else
				{		
					FieldInfo closingFormInfo = searchForm(closingFormName);	
					if (closingFormInfo == null)
					{
						cache.RemoveAt(i);
						Logger.LogError(this, "UpdateCache - the form \"" + closingFormName + "\" was into the cache but it does not exist into the pageManager.");
					}
					else
					{
						Frm_Page closingForm = (Frm_Page)closingFormInfo.GetValue(this);
						if (closingForm != null && closingForm.IsRemovableFromCache())
						{
							cache.RemoveAt(i);
							closingForm.Dispose();
							closingFormInfo.SetValue(this, null);
						}
					}
				}
				i++;
			}
			cache.Add(formName);
		}

		/// <summary>
		/// Keep trace of the currently showed statusbar.
		/// </summary>
		/// <param name="statusBar">The new StatusBar being showed.</param>
		public void UpdateCurrentStatusBar(Frm_StatusBar statusBar)
		{
			CurrentStatusBar = statusBar;
			//dispose and set to null any other statusbar potentially existing
			FieldInfo[] fieldsInfo = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
			if (fieldsInfo != null)
			{
				for (int i = 0; i < fieldsInfo.Length; i++)
				{
					if (Type.GetType("SyncroC.Forms.StatusBar.Frm_StatusBar").IsAssignableFrom(fieldsInfo[i].FieldType))
					{
						if (statusBar.GetType() != fieldsInfo[i].FieldType && fieldsInfo[i].Name != "CurrentStatusBar")
						{
							Frm_StatusBar disposingStatusBar = (Frm_StatusBar)fieldsInfo[i].GetValue(this);
							if (disposingStatusBar != null && !disposingStatusBar.IsDisposed)
							{
								disposingStatusBar.Dispose();
							}
							fieldsInfo[i].SetValue(this, null);
						}
					}
				}
			}
		}	

		/// <summary>
		/// Determine whether the status bar of the type passed as parameter is already showed.
		/// </summary>
		/// <param name="type"></param>
		/// <returns>True if the status bar is already showed, or wrong parameter has been passed. False otherwise.</returns>
		public bool IsStatusBarCurrentlyShowed(Type type)
		{
			if (type == null)
			{
				Logger.LogError(this, "IsStatusBarAlreadyShowed - the type parameter was null.");
				return true;
			}
			if (! typeof(Frm_StatusBar).IsAssignableFrom(type))
			{
				Logger.LogError(this, "IsStatusBarAlreadyShowed - the type must be a Frm_StatusBar type.");
				return true;
			}
			return (type.IsAssignableFrom(CurrentStatusBar.GetType()));
		}

		/// <summary>
		/// looks for the control into the form
		/// if it is not found, null is returned
		/// </summary>
		/// <param name="form">the form to look into</param>
		/// <param name="controlName">the control to look for</param>
		/// <returns></returns>
		private Control getControl(object form, string controlName)
		{
			FieldInfo controlInfo = form.GetType().
				GetField(controlName, 
				BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			if (controlInfo == null) return null; //the control does not exist
			Control control = (System.Windows.Forms.Control)controlInfo.GetValue(form);
			if (control == null) return null; //the control has not been instantiated yet
			return control;
		}

		/// <summary>
		/// invoke the method methodName of obj object with coded parameters in paramString
		/// </summary>
		/// <param name="obj">the object where to invoke the method</param>
		/// <param name="methodName">the name of the method to be invoked</param>
		/// <param name="paramString">a string representing the parameters to be passed to the method</param>
		private void invokeMethod(Object obj, string methodName, string  paramString)
		{
			//parsing
			string[] parameters;		//array containing all the parametres as string according to the convetions
			Type[] parametersType;		//array containing parametre types
			Object[] parametersValue;	//array containing parametre values already casted to the correspondent types in parametres type array.
			int mergeNumber = 0;		//number of merged parameters because the split was not correct
			if (paramString.Equals(""))
			{	//Empty string: so there is no parametres for the method
				parametersType = new Type[]{};	
				parametersValue = new Object[]{};
			}
			else
			{	//the fallowing regular expression allow a correct split even if there are some comma or 
				//square brakets into the value of the parameters so a paramString like the fallowing is 
				//correct I[a,bc[],I[a[]b,c] and it is splitted as I[a,bc[] and I[a[]b,c].
				//The only case it doesn't work is when into the value of a parameter there is a 
				//closed square braket fallowed by a comma, this last case is "manually" managed 
				parameters = Regex.Split(paramString, "(?<=]),");
	
				parametersType = new Type[parameters.Length];
				parametersValue = new Object[parameters.Length];

				for (int i = 0; i < parameters.Length; i++)
				{
          parameters[i]=parameters[i].Trim();
          if (! (parameters[i][1].Equals('[') && parameters[i][parameters[i].Length - 1].Equals(']')))
					{//square brakets are NOT in the correct position
						if (i == 0)
						{	// it is the first parameter so it can be only an error
							Logger.LogError(this, "invokeMethod - parameter \"" + parameters[i] + "\" is not correct.");
							return;
						}
						else
						{	// could be an error or maybe the split was not correct because into a value
							// of a parameter was specified a ",]" string:
							// merge the value with the previous value and keep trak of this operation
							parametersValue[i - 1] += "," + parameters[i]; 
							mergeNumber++;
						}
					}
					else
					{//square brakets are in the CORRECT position
						parametersType[i] = getParameterType(parameters[i][0]);	//get the type of the parameter
						if (parametersType[i] == null) 
						{
							Logger.LogError(this, "invokeMethod - invalid command,  parameter type " + parameters[i][0] + " is not correct.");
							return; 
						}
						try 
						{
							parametersValue[i] = Convert.ChangeType(parameters[i].Substring(2, parameters[i].Length - 2 -1), parametersType[i]);
						}
						catch (InvalidCastException ex)
						{
							Logger.LogError(this, "invokeMethod - type of the parameter " + parameters[i] + " not valid", ex);
							return; 
						}
						//coded messages from the server management
						if (parametersType[i].Equals(typeof(System.String)))
							if (((string)parametersValue[i]).StartsWith(PRE_MSG_CODE))
								parametersValue[i] = DBManager.getDBManager().message.getTranslatedMessage((string)parametersValue[i]);
					}
				}
			}
			if (mergeNumber != 0)
			{	//the code inside the current block is executed ONLY if into a string of a value 
				//has been specified the string ",]":
				//manually (because the merge value operations could be occurred anywhere into the arrays)
				//copy the parametersType and parametersValue in new arrays to obtain a correct behaviour
				//of the fallowing invoked methods GetMethod() and Invoke()
				Type[] parametersTypeCopy = new Type[parametersType.Length - mergeNumber];
				Object[] parametersValueCopy = new Object[parametersValue.Length - mergeNumber];
				int offset = 0;
				for (int k = 0; k < parametersType.Length; k++)
				{
					if (parametersType[k] != null)
					{
						parametersTypeCopy[k - offset] = parametersType[k];
						parametersValueCopy[k - offset] = parametersValue[k];
					}
					else
						offset++;
				}
				parametersType = parametersTypeCopy;
				parametersValue = parametersValueCopy;
			}
			//execution
			try
			{
				MethodInfo method = (MethodInfo)obj.GetType().
					GetMethod(methodName, 
						BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase,
						null, CallingConventions.Any,parametersType, null);
				if (method == null) 
				{
					Logger.LogError(this, "invokeMethod - object \"" + obj.ToString() + "\"; method \"" + methodName + "\" not found.");
					return;
				}
				method.Invoke(obj, parametersValue);	
			}
			catch (AmbiguousMatchException ex)
			{
				Logger.LogError(this, "invokeMethod - object \"" + obj.ToString() + "\"; method \"" + methodName + "\" is ambiguaous.", ex);
				return;
			}
			catch (TargetParameterCountException ex)
			{
				Logger.LogError(this, "invokeMethod - object \"" + obj.ToString() + "\"; method \"" + methodName + "\" some target parameter are wrong.", ex);
				return;
			}
			catch (TargetInvocationException ex)
			{
				Logger.LogError(this, "invokeMethod - The following exception has been thrown by: object \"" + obj.ToString() + "\"; method \"" + methodName + "\"", ex.InnerException);
				return;
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "invokeMethod - object \"" + obj.ToString() + "\"; method \"" + methodName + "\" error during invocation.", ex);
				return;
			}
		}

		/// <summary>
		/// set the property proertyName of obj object with the value val
		/// </summary>
		/// <param name="obj">the object in which the property will be set</param>
		/// <param name="propertyName">the name of the property to be set</param>
		/// <param name="val">a string representing the value to be set the property; the value can be a string to be decoded and translated.</param>
		private void setProperty(Object obj, string propertyName, string val)
		{
      PropertyInfo propertyInfo=null;
      try
      {
         propertyInfo= obj.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
      }
      catch(AmbiguousMatchException e)
      {
        Logger.LogError(this, "SetProperty - two properties whith name "+propertyName, e.InnerException);
        e.ToString();
        propertyInfo = obj.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
      }
			if (propertyInfo == null) 
			{
				Logger.LogError(this, "setProperty - property \"" + propertyName + "\" not found into object \"" + obj + "\".");
				return; 
			}

			if (propertyInfo.PropertyType.Equals(typeof(System.Drawing.Color)))
			{//the val string has to be "converted" (before being assigned to the property) to the Color type 
				try
				{
					propertyInfo.SetValue(obj, Color.FromName(val), null);
				}
				catch(Exception ex)
				{
					Logger.LogError(this, "SetProperty - error setting the property \"" + propertyInfo.Name + "\" with the color \"" + val + "\"", ex);
					return;
				}
			}

			else if (propertyInfo.PropertyType.Equals(typeof(SyncroC.Controls.Colors.SyncroColor)))
			{//the val string has to be "converted" (before being assigned to the property) to the SyncroColor type 
				try
				{
					propertyInfo.SetValue(obj, Enum.Parse(typeof(SyncroC.Controls.Colors.SyncroColor), val), null);

				}
				catch (Exception ex)
				{
					Logger.LogError(this, "SetProperty - error setting the property \"" + propertyInfo.Name + "\" with the SyncroColor \"" + val + "\"", ex);
					return;
				}
			}

			else if (val.StartsWith(PRE_MSG_CODE))
			{// the val string has to be decoded before being assigned to the property
				//set the property with the decoded message
				string msg = DBManager.getDBManager().message.getTranslatedMessage(val, Settings.getSettings().CurrentLanguage);
				try
				{
					propertyInfo.SetValue(obj, msg, null);
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "SetProperty - the property \"" + propertyInfo.Name + "\" is not a string", ex);
					return;  
				}
				//set the Tag property with the coded string
				if (obj is System.Windows.Forms.Control)
					((System.Windows.Forms.Control)obj).Tag = val;
			}

			else 
			{//the val string has to be converted (before being assigned to the property) to the property type 
				try 
				{
					//settaggio del valore della property con il valore specificato castato al tipo della property stessa.
					propertyInfo.SetValue(obj, Convert.ChangeType(val, propertyInfo.PropertyType), null);
				}
				catch (InvalidCastException ex)
				{//may be thrown by the ChangeType method
					Logger.LogError(this, "SetProperty - string \"" + val + "\" is not compatible with the property \"" + propertyInfo.Name + "\"", ex);
					return;  
				}
				catch (Exception ex)
				{//may be thrown by the SetValue method
					Logger.LogError(this, ex);
					return;  
				}
			}
		}

		/// <summary>
		/// decode the parameter type according to convetions
		/// if typeCode is wrong return null
		/// </summary>
		/// <param name="typeCode">a string representing the type to be decoded</param>
		/// <returns></returns>
		private Type getParameterType(char typeCode)
		{
			switch(typeCode)
			{
				case 'i':
				case 'I': return Type.GetType("System.Int32");

				case 's':
				case 'S': return Type.GetType("System.String");

				case 'b':
				case 'B': return Type.GetType("System.Boolean");

				default: return null;
			}
		}

		/// <summary>
		/// return the current page.
		/// it could be null if no page has been instantiated yet.
		/// </summary>
		/// <returns>a reference to the current page.</returns>
		public Frm_Page getCurrentPage()
		{
			Form form = getForm(pagesHistorian.GetCurrentPage());
			if (form != null)
			{
				if (form is Frm_Page)
					return (Frm_Page)form;
				else
				{
					Logger.LogError(this, "getCurrentPage - the current page is not a Frm_Page.");
					return null;
				}
			}
			return null;
		}

		/// <summary>
		/// return the current page.
		/// it could be null if no page has been instantiated yet.
		/// </summary>
		/// <returns>a reference to the previous page.</returns>
		public Frm_Page getPreviousPage()
		{
			Form form = getForm(pagesHistorian.GetPreviousPage());
			if (form != null)
			{
				if (form is Frm_Page)
				{
					return (Frm_Page)form;
				}
				else
				{
					Logger.LogError(this, "getPreviousPage - the previous page is not a Frm_Page.");
					return null;
				}
			}
			return null;
		}
		
		/// <summary>
		/// translate all instantiated forms.
		/// </summary>
		/// <param name="language">the language to translate into all the forms.</param>
		public void translateForms(string language)
		{
			FieldInfo[] fieldsInfo = this.GetType().GetFields(
				BindingFlags.Instance | BindingFlags.NonPublic);
			if (fieldsInfo != null)
				for (int i = 0; i < fieldsInfo.Length; i++)
          if (Type.GetType("SyncroC.Forms.Frm_Base").
            IsAssignableFrom(fieldsInfo[i].FieldType))
          {
            Frm_Base translatingForm = (Frm_Base)fieldsInfo[i].GetValue(this);
            //there is the need to translate the currentPage as last one because of the settings of the Link Label
            if (translatingForm != null
              && !translatingForm.IsDisposed
              && translatingForm != getCurrentPage())
              translatingForm.SetUp(language, false);
          }

    if (getCurrentPage() != null)
      getCurrentPage().SetUp(language, false);
		}

		/// <summary>
		/// Call the refreshData method on every instancieted forms.
		/// </summary>
		private void refreshAllFormsData(bool mysql)
		{
			FieldInfo[] fieldsInfo = this.GetType().GetFields(
				BindingFlags.Instance | BindingFlags.NonPublic);
			if (fieldsInfo != null)
				for (int i = 0; i < fieldsInfo.Length; i++)
					if (Type.GetType("SyncroC.Forms.Frm_Base").
						IsAssignableFrom(fieldsInfo[i].FieldType))
					{
						Frm_Base refreshingForm = (Frm_Base)fieldsInfo[i].GetValue(this);
            if (refreshingForm != null && !refreshingForm.IsDisposed)
            {
              if (mysql)
                refreshingForm.refreshMySQLData();
              else
                refreshingForm.refreshData();
            }
					}
		}

    public void refreshAllFormsData()
    {
      this.refreshAllFormsData(false);
    }

    /// <summary>
    /// Call the refreshData method on every instancieted forms.
    /// </summary>
    public void refreshAllFormsMySQLData()
    {
      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        this.refreshAllFormsData(true);
    }
		
		/// <summary>
		/// Call the setUp method for every form in order to restore the language database.
		/// </summary>
		/// <returns>true, if operation completed correctly, false if some errors occured</returns>
		public bool SetUpAllForms()
		{
			bool correct = true;
			FieldInfo[] fieldsInfo = this.GetType().GetFields(
				BindingFlags.Instance | BindingFlags.NonPublic);
      if (fieldsInfo != null)
      {
        for (int i = 0; i < fieldsInfo.Length; i++)
        {
          if (Type.GetType("SyncroC.Forms.Frm_Base").IsAssignableFrom(fieldsInfo[i].FieldType))
          {
            Frm_Base form = null;
            try
            {
              form = (Frm_Base)fieldsInfo[i].GetValue(this);
            }
            catch (Exception e)
            {
              correct = false;
              Logger.LogError(this, "SetUpAllForms - Exception getting value of form \"" + /*(Frm_Base)*/fieldsInfo[i].Name + "\".", e);
            }
            if (form == null)
            {	//the form has not been instantiated yet
              //so instantiation, call setUp, and then dispose
              //System.Runtime.Remoting.ObjectHandle handle = null;
              Object handle = null;
              try
              {
                handle = Activator.CreateInstance(fieldsInfo[i].FieldType);
              }
              catch (Exception e)
              {
                correct = false;
                Logger.LogError(this, "SetUpAllForms - Exception creating instance of form \"" + fieldsInfo[i].FieldType.Name + "\".", e);
              }
              if (handle != null)
              {
                try
                {
                  fieldsInfo[i].SetValue(this, handle);
                }
                catch (Exception e)
                {
                  correct = false;
                  Logger.LogError(this, "SetUpAllForms - Exception setting value of form \"" + fieldsInfo[i].Name + "\".", e);
                }
                try
                {
                  form = (Frm_Base)fieldsInfo[i].GetValue(this);
                }
                catch (Exception e)
                {
                  correct = false;
                  Logger.LogError(this, "SetUpAllForms - Exception getting value of form \"" + /*(Frm_Base)*/fieldsInfo[i].Name + "\" after instantiation.", e);
                }
                if (form != null)
                {
                  form.SetUp(Settings.getSettings().CurrentLanguage, true);
                  form.Dispose();
                }
                fieldsInfo[i].SetValue(this, null);
              }
            }
            else //if (form != null)
              //the form has already been instantiated so just call the setUp
              form.SetUp(Settings.getSettings().CurrentLanguage, true);
          }
        }
      }
			return correct;
		}

		/// <summary>
		/// Set the text and the foreColor of the lbl_State.
		/// </summary>
		/// <param name="state">string to set into the lbl_State label.</param>
		/// <param name="color">foreColor to set for the lbl_State label font.</param>
		private void setLbl_State(string state, Color color)
		{
			lbl_State.Text = state;
			lbl_State.ForeColor = color;
			lbl_State.Refresh();
		}

		/// <summary>
		/// Sets a user message using current language.
		/// </summary>
		/// <param name="message"> a string message to be decoded with same rules as SyncroC.DataBase.Message.getTranslatedMessage method.</param>
		/// /// <returns>the string that is displayed</returns>
		public string _SetMessage(string message)
		{
			return _SetMessage(message, false);
		}

		/// <summary>
		/// Sets a user message using current language.
		/// </summary>
		/// <param name="message"> a string message to be decoded with same rules as SyncroC.DataBase.Message.getTranslatedMessage method.</param>
		/// <param name="error">if true the message is displayed using alarm color</param>
		/// /// <returns>the string that is displayed</returns>
    public string _SetMessage(string message, bool error)
    {
			return _SetMessage(message, error, false);
    }

		/// <summary>
		/// Sets a user message using current language.
		/// </summary>
		/// <param name="message"> a string message to be decoded with same rules as SyncroC.DataBase.Message.getTranslatedMessage method.</param>
		/// <param name="error">if true the message is displayed using alarm color</param>
		/// <param name="still">if true the message will be displayed till next call of _SetMessage, otherwise for 5 seconds</param>
		/// <returns>the string that is displayed</returns>
		public string _SetMessage(string message, bool error, bool still)
    {
      lbl_Message_NY.ForeColor = (error ? Color.Orange : Color.WhiteSmoke);
			lbl_Message_NY.Text = dBManager.message.getTranslatedMessage(message);
			lbl_Message_NY.Tag = message;
      timer_Message.Enabled = !still;
			return lbl_Message_NY.Text;
    }

    /// <summary>
    /// Clean the message showed in the mnessage bar of the pages. 
		/// </summary>
		private void timer_Message_Tick(object sender, System.EventArgs e)
		{
			lbl_Message_NY.Text = "";
			lbl_Message_NY.Tag = "";
			timer_Message.Enabled = false;
		}

		/// <summary>
		///keep lbl_Date and lbl_Time aligned with the system clock.
		///Frm_Page has a dataBinding to these above labels so that the pages can show
		///the correct date and time at any time.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer_DateTime_Tick(object sender, System.EventArgs e)
		{
			//FOS2 ABello
			//Split date string
			string[] data = DateTime.Now.ToString("HH:mm:ss dd/MM/yy").Split(' ');
			lbl_Date.Text = data[1].Replace('/',' ');
			lbl_Time.Text = data[0];
		}

		/// <summary>
		/// Set the time of the computer according to the parameter..
		/// </summary>
		/// <param name="date">The date to be set. It must be format in the following way:
		/// dd dd mm yyyy hh mm ss mm
		/// 02 30 04 2003 08 45 56
		/// b[0] -> 02 (dd) Specifies the current day of the week; Sunday = 0, Monday = 1, and so on. 
		/// b[1] -> 30 (dd)
		/// b[2] -> 04 (mm) Specifies the current month; January = 1, February = 2, and so on. 
		/// b[3] -> 2003 (yyyy)
		/// b[4] -> 08 (hh)
		/// b[5] -> 45 (mm)
		/// b[6] -> 56 (ss)
		/// b[7] -> 34 (mm) millisecond
		/// <param name="date"></param>
    public void _SetDateTime(string date)
    {
      //ABELLO_FI04/11/2008_1 Si cambiava la Data e Ora ache dei client che non erano nella rete Fosber.
      if (!Settings.getSettings().configuration.getOnServer() && Settings.getSettings().configuration.getOnFosberNetwork())
      {
        string[] data = date.Trim().Split(' ');
        try
        {
          SyncroC.Utility.TimeZoneInformation.SYSTEMTIME lpSystemTime = new SyncroC.Utility.TimeZoneInformation.SYSTEMTIME();
          lpSystemTime.DayOfWeek = Convert.ToUInt16(data[0]);
          lpSystemTime.Day = Convert.ToUInt16(data[1]);
          lpSystemTime.Month = Convert.ToUInt16(data[2]);
          lpSystemTime.Year = Convert.ToUInt16(data[3]);
          lpSystemTime.Hour = Convert.ToUInt16(data[4]);
          lpSystemTime.Minute = Convert.ToUInt16(data[5]);
          lpSystemTime.Second = Convert.ToUInt16(data[6]);
          lpSystemTime.Milliseconds = Convert.ToUInt16(data[7]);
          //N.B. the System Time is the GMT orario
          long i = SyncroC.Utility.TimeZoneInformation.NativeMethods.SetSystemTime(ref lpSystemTime);
        }
        catch (Exception e)
        {
          Logger.LogError(this, "_SetDateTime - CANNOT SET DATE TIME ", e);
        }
      }
    }

    //ABELLO_FI04/11/2008_1 Aggiunta la PopUp per il cambio di Data e Ora.
    /// <summary>
    /// Set the time of the computer according to the parameter..
    /// </summary>
    private SyncroC.Utility.TimeZoneInformation myTimeZoneInformation = SyncroC.Utility.TimeZoneInformation.CurrentTimeZone;
    public void _SetTimeZone(string TimeZone)
    {
      if (!Settings.getSettings().configuration.getOnServer() && Settings.getSettings().configuration.getOnFosberNetwork())
      {
        if (!((myTimeZoneInformation.Name == TimeZone) || (myTimeZoneInformation.StandardName == TimeZone)))
        {
          try
          {
            SyncroC.Utility.TimeZoneInformation.TIME_ZONE_INFORMATION lpTimeZoneInformation = SyncroC.Utility.TimeZoneInformation.FromName(TimeZone).TziNative();
            SyncroC.Utility.TimeZoneInformation.NativeMethods.SetTimeZoneInformation(ref lpTimeZoneInformation);
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            myTimeZoneInformation = SyncroC.Utility.TimeZoneInformation.CurrentTimeZone;
          }
          catch (Exception e)
          {
            Logger.LogError(this, "_SetTimeZone - CANNOT SET TIME ZONE ", e);
          }
        }
      }
    }

		public void _Clear()
		{
      this.Hide();
		}

		/// <summary>
		/// Calculates the command to be sent to the server to request the status bar or the non indexed page of the type specified as parameter
		/// </summary>
		/// <param name="type">the type of the status bar or non indexed page to calculate the request command for</param>
		/// <returns>the command</returns>
		public static string getRequestCommand(Type type)
		{
			string command = string.Empty;
      if (type == null || !(typeof(Frm_Page).IsAssignableFrom(type) || typeof(Frm_StatusBar).IsAssignableFrom(type) || typeof(Frm_LateralBar).IsAssignableFrom(type)))
			{
				Logger.LogError("Pagemanager", "getRequestPageCommand - the type cannot be null and must be a Frm_Page or Frm_StatusBar type.");
				return command;
			}
			FieldInfo[] fields = typeof(PageManager).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
			for (int i = 0; i < fields.Length; i++)
				if (fields[i].FieldType.Equals(type))
				{
					command = fields[i].Name;
					break;
				}
			return command;
		}

		/// <summary>
		/// Calculates the command to be sent to the server to request the indexed page of the type specified as parameter
		/// </summary>
		/// <param name="type">the type of the indexed page to calculate the request command for</param>
		/// <param name="index">the index to be used into the command</param>
		/// <returns>the command</returns>
		public static string getRequestCommand(Type type, int index)
		{
			string command = string.Empty;
			if (!IsIndexedPageGroup(type))
			{
				Logger.LogError("Pagemanager", "getRequestCommand(Type type, int index) has NOT been called with a type of indexed form...");
				return command;
			}
			command = getRequestCommand(type);
			ReplaceIndexInFormName(ref command, index);
			return command;
		}

		/// <summary>
		/// Calculate the index of the passed page
		/// </summary>
		/// <param name="formName"></param>
		/// <returns>the index</returns>
		private static string ExtractIndexFromFormName(string formName)
		{
			return ReplaceIndexInFormName(ref formName, -1);
		}

		/// <summary>
		/// Replace the index into the command with the one possed as parameter
		/// </summary>
		/// <param name="formName">the command to be modified</param>
		/// <param name="newIndex">the new index to be use into the command</param>
		/// <returns>the old index been replaced </returns>
		private static string ReplaceIndexInFormName(ref string formName, int newIndex)
		{
			string indexOld = string.Empty;
			int endPositionIndex = formName.IndexOf('_') - 1;
			if (endPositionIndex < 0)
				return indexOld;
			int startPositionIndex = endPositionIndex;
			while (startPositionIndex - 1 > 0 && Char.IsDigit(formName[startPositionIndex - 1]))
				startPositionIndex--;
			indexOld = formName.Substring(startPositionIndex, endPositionIndex - startPositionIndex + 1);
			if (newIndex >= 0)
			{
				formName = formName.Remove(startPositionIndex, endPositionIndex - startPositionIndex + 1);
				formName = formName.Insert(startPositionIndex, newIndex.ToString());
			}
			return indexOld;
		}

		/// <summary>
		/// return true wheter the page group type passed as parameter in an indexed group
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private static bool IsIndexedPageGroup(Type type)
		{
			//add here any other indexed group
			return (
						typeof(Frm_SingleFacerGroup).IsAssignableFrom(type) 
				||	typeof(Frm_StackerGroup).IsAssignableFrom(type)
				||	typeof(Frm_SplicerGroup).IsAssignableFrom(type)
				);
		}

    //ABELLO_FI24/11/2010_3 Rimosso questo che non serve pi� perch� � fatto sul main senza sleep
    //private void PageManager_Load(object sender, EventArgs e)
    //{
    //  System.Threading.Thread.Sleep(2000);
    //  //Modifica per avviare pi� di un client sul solito PC
    //  Screen[] screens = Screen.AllScreens;

    //  if ( (System.Diagnostics.Process.GetProcessesByName("S_SyncroC").Length > 1) &&
    //    (screens.Length == 1 ) )
    //    Application.Exit();
    //}

    private void timer_Closable_Tick(object sender, EventArgs e)
    {
      this.timer_Closable.Enabled = false;
      this.ControlBox = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
    }

    private void PageManager_FormClosing(object sender, FormClosingEventArgs e)
    {
      //agrassi_FI19-02-09_3
      Logger.LogError(this, "User requested to close all, because the connection with server is impossible");
      this.closeAll=true;
      //Application.ExitThread();
      //Application.Exit();
    }
	}

    class OrderTcpHandlerData
    {
        private CommunicationManager m_cm;
        private TcpClient m_cl;
        private DBManager m_dbm;

        public OrderTcpHandlerData(CommunicationManager cm, DBManager dbm, TcpClient cl = null)
        {
            m_cm = cm;
            m_cl = cl;
            m_dbm = dbm;
        }

        public CommunicationManager communication()
        {
            return m_cm;
        }

        public TcpClient client()
        {
            return m_cl;
        }

        public DBManager db()
        {
            return m_dbm;
        }
    }

    class OrderTcpServerThreaded
    {
		private static readonly object orderAckLock = new object();
		private static readonly Hashtable pendingOrderAcks = new Hashtable();

		private static void LogOrderTcp(string message)
		{
			Logger.LogOrderTcp("[ORDER_TCP] " + message);
		}

		private static void LogOrderTcp(string message, Exception exception)
		{
			Logger.LogOrderTcp("[ORDER_TCP] " + message, exception);
		}

		private static string EscapeOrderTcpLogText(string value)
		{
			return (value ?? String.Empty)
				.Replace("\\", "\\\\")
				.Replace("\r", "\\r")
				.Replace("\n", "\\n")
				.Replace("\0", "\\0")
				.Replace("\u241E", "<SEP>");
		}

		public static void Initialize(DBManager dbm)
		{
			dbm.order.OnDBOrderChange += new SyncroC.DataBase.Order.DBChangedHandler(OrderChanged);
		}

		private static void OrderChanged(object sender, SyncroC.DataBase.EventArgs.DBChangedEventArgs args)
		{
			if (args == null || args.Changement != SyncroC.DataBase.EventArgs.DBChangement.Insert)
				return;

			int position;
			if (!Int32.TryParse(args.Key, out position) || position < 1)
				return;

			SyncroC.DataBase.Order order = sender as SyncroC.DataBase.Order;
			if (order == null)
				return;

			DataRow row = order.getRow(position - 1);
			if (row == null)
				return;

			string setupCode = row[SyncroC.DataBase.Order.DEL_SETUP_CODE].ToString();
			ManualResetEvent acknowledgement;
			int pendingCount;
			lock (orderAckLock)
			{
				acknowledgement = pendingOrderAcks[setupCode] as ManualResetEvent;
				pendingCount = pendingOrderAcks.Count;
			}
			LogOrderTcp("Order insert event; setup=" + setupCode + "; matched waiter=" + (acknowledgement != null) + "; pending=" + pendingCount);
			if (acknowledgement != null)
				acknowledgement.Set();
		}

		private static bool TryGetSetupCode(string command, out string setupCode)
		{
			setupCode = null;
			int openParenthesis = command.IndexOf('(');
			int firstSeparator = openParenthesis < 0 ? -1 : command.IndexOf(Command.PARAMETER_SEPARATOR, openParenthesis + 1);
			int secondSeparator = firstSeparator < 0 ? -1 : command.IndexOf(Command.PARAMETER_SEPARATOR, firstSeparator + 1);
			if (secondSeparator < 0)
				return false;

			string value = command.Substring(firstSeparator + 1, secondSeparator - firstSeparator - 1).Trim();
			if (value.Length == 0 || value.Length > 30 || value.IndexOf('\r') >= 0 || value.IndexOf('\n') >= 0)
				return false;

			setupCode = value;
			return true;
		}

		private static string SendOrderAdd(OrderTcpHandlerData data, string command)
		{
			string setupCode;
			if (!TryGetSetupCode(command, out setupCode))
			{
				LogOrderTcp("Order.Add rejected: setup code could not be read");
				return "invalid\n";
			}
			LogOrderTcp("Order.Add started; setup=" + setupCode);

			ManualResetEvent acknowledgement = new ManualResetEvent(false);
			lock (orderAckLock)
			{
				if (pendingOrderAcks.ContainsKey(setupCode))
				{
					acknowledgement.Close();
					LogOrderTcp("Order.Add already pending; setup=" + setupCode);
					return "pending\n";
				}
				if (data.db().order.getRowBySetupCode(setupCode) != null)
				{
					acknowledgement.Close();
					LogOrderTcp("Order.Add duplicate; setup=" + setupCode);
					return "duplicate\n";
				}
				pendingOrderAcks.Add(setupCode, acknowledgement);
			}

			try
			{
				if (!data.communication().SendTcp(data.communication().SYNCROS_DB_MANAGER, command))
				{
					LogOrderTcp("Order.Add failed to queue in DBManager; setup=" + setupCode);
					return "transport-error\n";
				}

				LogOrderTcp("Order.Add queued; waiting for insert event; setup=" + setupCode);
				bool acknowledged = acknowledgement.WaitOne(25000);
				LogOrderTcp("Order.Add acknowledgement=" + (acknowledged ? "ok" : "timeout") + "; setup=" + setupCode);
				return acknowledged ? "ok\n" : "timeout\n";
			}
			catch (Exception exception)
			{
				LogOrderTcp("Order.Add handler failed; setup=" + setupCode, exception);
				return "transport-error\n";
			}
			finally
			{
				lock (orderAckLock)
				{
					pendingOrderAcks.Remove(setupCode);
				}
				acknowledgement.Close();
			}
		}

        //static String ordPath = @"C:\usr\Syncro\Data\Client\Order.xml";

        public static void TcpClientHandle(Object obj)
        {
            OrderTcpHandlerData cd = (OrderTcpHandlerData)obj;
			try
			{
            Byte[] bytes = new Byte[4096];
            String data = null;
            int i;

            // Get a stream object for reading and writing
            NetworkStream stream = cd.client().GetStream();

			// The Node bridge uses a base64 line frame for Order.Add so TCP packet
			// boundaries cannot truncate the command. Legacy commands remain accepted.
			System.Text.StringBuilder received = new System.Text.StringBuilder();
			string framePrefix = "Order.Add64:";
			stream.ReadTimeout = 10000;
			try
            {
				while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
				{
					string receivedChunk = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
					LogOrderTcp("Socket RX chunk bytes=" + i + "; data=" + EscapeOrderTcpLogText(receivedChunk));
					received.Append(receivedChunk);
					string text = received.ToString();

					if (framePrefix.StartsWith(text) && text.Length <= framePrefix.Length)
						continue;

					if (text.StartsWith(framePrefix))
					{
						int lineEnd = text.IndexOf('\n');
						if (lineEnd < 0)
						{
							if (text.Length > 16384)
								data = "invalid\n";
							else
								continue;
						}
						else
						{
							string encodedCommand = text.Substring(framePrefix.Length, lineEnd - framePrefix.Length);
							if (encodedCommand.Length > 16384)
								data = "invalid\n";
							else
							{
								try
								{
									data = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedCommand));
								}
								catch (FormatException)
								{
									data = "invalid\n";
								}
							}
						}
						break;
					}

					data = text.Replace("\n", "").Replace("\r", "");
					break;
				}
			}
			catch (IOException)
			{
				data = "timeout\n";
				LogOrderTcp("TCP command read timed out");
			}

			if (data == null)
			{
				if (received.Length == 0)
				{
					LogOrderTcp("TCP connection closed without request payload");
					return;
				}
				data = "invalid\n";
				LogOrderTcp("TCP connection closed with incomplete request; chars=" + received.Length);
			}

			if (data.StartsWith("Order.Add64:"))
				data = "invalid\n";
			LogOrderTcp("Socket RX decoded command chars=" + data.Length + "; data=" + EscapeOrderTcpLogText(data));

			if (data.Length > 0)
			{
				string commandName = data.StartsWith("Order.Add") ? "Order.Add"
					: data.StartsWith("Orders.Has:") ? "Orders.Has"
					: data.StartsWith("Orders.Get") ? "Orders.Get"
					: data.StartsWith("Orders.Delete") ? "Orders.Delete"
					: data.StartsWith("Syncro.Version") ? "Syncro.Version"
					: data.StartsWith("Syncro.DataVersion") ? "Syncro.DataVersion"
					: data.StartsWith("Syncro.IteVersion") ? "Syncro.IteVersion"
					: data.StartsWith("Home.Info") ? "Home.Info"
					: data.StartsWith("Operator.Status") ? "Operator.Status"
					: data.StartsWith("Shift.State") ? "Shift.State"
					: data.StartsWith("Shift.List") ? "Shift.List"
					: "Other";
				LogOrderTcp("Received " + commandName + "; chars=" + data.Length);
                //Console.WriteLine("Received: {0}", data);
                if (data.StartsWith("Syncro.Version")) {
                    data = PageManager.getPageManager().getSyncroVersion();
                } else if(data.StartsWith("Syncro.IteVersion")){
                    data = PageManager.getPageManager().getITEVersion();
                } else if(data.StartsWith("Syncro.DataVersion")){
                    data = PageManager.getPageManager().getSyncroDataVersion();
                } else if (data.StartsWith("Home.Info")) {
                    if (PageManager.getPageManager().getHomeDryForm() != null)
                    {
                        data = PageManager.getPageManager().getHomeDryForm().getHomePageXml();
                    }
                    else {
                        data = "error\n";
                    }
				} else if (data.Equals("Operator.Status")) {
					data = PageManager.getPageManager().getOperatorStatusXml();
					if (data == null)
						data = "error\n";
                } else if (data.StartsWith("Shift.State")) {
                    if (PageManager.getPageManager().getShiftReport() != null)
                    {
                        data = PageManager.getPageManager().getShiftReport().getCurrentShiftState() ? "idle" : "working";
                    }
                    else
                    {
                        data = PageManager.getPageManager().getHomeDryForm().getIsStopShift() ? "idle" : "working";
                    }
                } else if (data.StartsWith("Shift.List"))
                {
                    data = DBManager.getDBManager().shift.getTableShift().DataSet.GetXml();
                }
                else if (data.StartsWith("Order.Add"))
                {
					data = SendOrderAdd(cd, data);
                }
				else if (data.StartsWith("Orders.Has:"))
				{
					string setupCode = data.Substring("Orders.Has:".Length).Trim();
					if (setupCode.Length > 0 && setupCode.Length <= 30 &&
						setupCode.IndexOf('\r') < 0 && setupCode.IndexOf('\n') < 0)
					{
						DataRow row = DBManager.getDBManager().order.getRowBySetupCode(setupCode);
						data = row == null ? "false\n" : "true\n";
					}
					else
					{
						data = "invalid\n";
					}
				}
                else if (data.Equals("Orders.Get"))
                {
                    data = DBManager.getDBManager().order.order.GetXml();
                }
                else if (data.Equals("Orders.Delete"))
                {
                    cd.communication().SendTcp(cd.communication().SYNCROS_DB_MANAGER, Command.DB_ORDER + "." + Command.DB_CMD_CLR + "()");
                    data = "ok\n";
                }
                else
                {
                    data = "unknown\n";
                }

				string responseSummary = data.StartsWith("<") ? "xml" : data.Trim();
				LogOrderTcp("Responding to " + commandName + "; result=" + responseSummary);
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                stream.Write(msg, 0, msg.Length);
            }
			}
			catch (Exception exception)
			{
				LogOrderTcp("TCP client handler failed", exception);
			}
			finally
			{
				if (cd.client() != null)
					cd.client().Close();
				LogOrderTcp("TCP client handler closed connection");
			}
        }

        public static void TcpServerListener(Object obj)
        {
            String addr = "0.0.0.0";
            Int32 port = 13000;
            OrderTcpHandlerData data = (OrderTcpHandlerData)obj;
            TcpListener server = null;

            try
            {
                IPAddress localAddr = IPAddress.Parse(addr);
                server = new TcpListener(localAddr, port);

                server.Start();

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
					LogOrderTcp("Accepted TCP client " + client.Client.RemoteEndPoint);
                    Thread t = new Thread(TcpClientHandle);
                    t.Start(new OrderTcpHandlerData(data.communication(), data.db(), client));
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }

        }
    }

    class OrderProxy
    {
        public static void start(CommunicationManager cm, DBManager dbm)
        {
			OrderTcpServerThreaded.Initialize(dbm);
            Thread t = new Thread(OrderTcpServerThreaded.TcpServerListener);
            t.Start(new OrderTcpHandlerData(cm, dbm));
        }
    }
}
