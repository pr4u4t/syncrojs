using System;
using System.Collections;

using SyncroC.Utility;
using SyncroC.Utility.EventArgs;
using SyncroC.Forms.Pages;
using SyncroC.Forms.StatusBar;
using SyncroC.Forms.Pages.Statistics;
using SyncroC.Forms.Pages.Statistics.Orders;
using SyncroC.Forms.Pages.Statistics.Rolls;
using SyncroC.Forms.Pages.Production;
using SyncroC.Forms.Pages.Production.Home;
using SyncroC.Forms.Pages.Production.OrdersIns;
using SyncroC.Forms.Pages.Production.OrdersList;
using SyncroC.Forms.Pages.Production.Alarms;
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
using SyncroC.Forms.Pages.Twin400.Settings;
using SyncroC.Forms.Pages.Twin400.Maintenance;
using SyncroC.Forms.Pages.Twin400.Status;
//RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
using SyncroC.Forms.Pages.Twin400.Warnings;
using SyncroC.Forms.Pages.DoubleBacker.Alarms;
using SyncroC.Forms.Pages.DoubleBacker.Settings;
using SyncroC.Forms.Pages.DoubleBacker.Status;
using SyncroC.Forms.Pages.DoubleBacker.Console;
using SyncroC.Forms.Pages.DoubleBacker;
using SyncroC.Forms.Pages.PreHeaterTern;
using SyncroC.Forms.Pages.PreHeaterTern.Alarms;
using SyncroC.Forms.Pages.PreHeaterTern.Settings;
using SyncroC.Forms.Pages.GlueUnit;
using SyncroC.Forms.Pages.GlueUnit.Alarms;
using SyncroC.Forms.Pages.GlueUnit.Console;
using SyncroC.Forms.Pages.GlueUnit.Settings;
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
using SyncroC.Forms.Pages.SingleFacer.Console;
using SyncroC.Forms.Pages.SingleFacer.Settings;
using SyncroC.Forms.Pages.SingleFacer.Status;
using SyncroC.Forms.Pages.Splicer;
using SyncroC.Forms.Pages.Splicer.Alarms;
using SyncroC.Forms.Pages.Splicer.Settings;
//RFazzi_FI28/09/2006_2.Added Office Page
using SyncroC.Forms.Pages.SyncroSystem.OFFICE;
using SyncroC.Forms.Pages.SyncroSystem.LOG;
//RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
using SyncroC.Forms.Pages.Production.Warnings;


namespace SyncroC
{
	/// <summary>
	/// Args for OnPageChanged event
	/// </summary>
	public class OnPageChangedEventArgs : System.EventArgs
	{
		/// <summary>
		/// Group which the new page belong to.
		/// </summary>
		public readonly Type groupType = null;
		/// <summary>
		/// Group index, 0 for non indexed groups
		/// </summary>
		public readonly int index = 0;

		public OnPageChangedEventArgs(Type groupType, int index)
		{
			this.groupType = groupType;
			this.index = index;
		}
	}

	/// <summary>
	/// Contains informations about visited pages.
	/// </summary>
	public class PagesHistorian
	{
		//constants
		/// <summary>deepth of the historian</summary>
    //agrassi_FI26-01-2012_1
		//private int MAX_HISTORIAN_LENGTH = 10;
    private int MAX_HISTORIAN_LENGTH = 5;
		private int MAX_MODULE_DSP = 60;

		//
		/// <summary>contains the name of the visited pages.</summary>
		private ArrayList historian;
		/// <summary>index of the currently showed page into the historian ArrayList.</summary>
		private int currentPagePointer = -1;
		/// <summary>index of the previously showed page into the historian ArrayList.</summary>
		private int previousPagePointer = -1;
		/// <summary>determine whether the Back button has been pressed or not before the page change.</summary>
		private bool backPressed = false;
		/// <summary>determine whether the Forward button has been pressed or not before the page change.</summary>
		private bool forwardPressed = false;

		/// <summary>last DSP module showed.</summary>
		private int currentDSPModule = 0;
		/// <summary>
		/// Gets or sets thelast DSP module showed.
		/// </summary>
		public int CurrentDSPModule
		{
			get { return currentDSPModule; }
			set
			{
				if (value <= MAX_MODULE_DSP && value >= 0)
					currentDSPModule = value;
			}
		}

		/// <summary>
		/// Delegate for handling OnPageChange events.
		/// </summary>
		public delegate void PageChangedHandler(object sender, OnPageChangedEventArgs args);

		/// <summary>
		/// Event raised when  pages change 
		/// </summary>
		public event PageChangedHandler OnPageChanged;

		///////////////////////////////////////////////////
		//////////////    STATUS BAR    ///////////////////
		///////////////////////////////////////////////////

		/// <summary>currently showed  status bar</summary>
		private string currentStatusBar = "";
		/// <summary>
		/// Gets or sets the currently showed statusbar
		/// </summary>
		public string CurrentStatusBar
		{
			get 
			{
				if (currentStatusBar == "")
					return Settings.getSettings().getFirstStatusBar();
				else
					return currentStatusBar;	
			}
			set {	currentStatusBar = value;	}
		}


		///////////////////////////////////////////////////
		/////////////////		SYNCRO		///////////////////// 
		///////////////////////////////////////////////////

		/**
		 * production group
		 */
		/// <summary>last visited page for Production group.</summary>
		private string currentPageProduction;
		/// <summary>
		/// Gets or sets the last visited page for Production group.
		/// </summary>
		public string CurrentPageProduction
		{
			get {	return currentPageProduction;	}
			set {	currentPageProduction = value;	}
		}
		/// <summary>last visited page for Production_Home subgroup.</summary>
		private string currentPageProduction_Home;
		/// <summary>
		/// Gets or sets the last visited page for Production_Home subgroup.
		/// </summary>
		public string CurrentPageProduction_Home
		{
			get { return currentPageProduction_Home; }
			set { currentPageProduction_Home = value; }
		}
    /// <summary>last visited page for currentPageProduction_Operator subgroup.</summary>
    private string currentPageProduction_Operator;
    /// <summary>
    /// Gets or sets the last visited page for currentPageProduction_Operator subgroup.
    /// </summary>
    public string CurrentPageProduction_Operator
    {
        get { return currentPageProduction_Operator; }
        set { currentPageProduction_Operator = value; }
    }
    /// <summary>last visited page for Production_OrdersIns subGroup.</summary>
		private string currentPageProduction_OrdersIns;
		/// <summary>
		/// Gets or sets the last visited page for Production_OrdersIns subGroup.
		/// </summary>
		public string CurrentPageProduction_OrdersIns
		{
			get { return currentPageProduction_OrdersIns; }
			set { currentPageProduction_OrdersIns = value; }
		}
		/// <summary>last visited page for Production_OrdersList group.</summary>
		private string currentPageProduction_OrdersList;
		/// <summary>
		/// Gets or sets the last visited page for Production_OrdersList group.
		/// </summary>
		public string CurrentPageProduction_OrdersList
		{
			get { return currentPageProduction_OrdersList; }
			set { currentPageProduction_OrdersList = value; }
		}
		/// <summary>last visited page for Production_Alarms group.</summary>
		private string currentPageProduction_Alarms;
		/// <summary>
		/// Gets or sets the last visited page for Production_Alarms group.
		/// </summary>
		public string CurrentPageProduction_Alarms
		{
			get { return currentPageProduction_Alarms; }
			set { currentPageProduction_Alarms = value; }
		}

    //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
    /// <summary>last visited page for Production_Warnings group.</summary>
    private string currentPageProduction_Warnings;
    /// <summary>
    /// Gets or sets the last visited page for Production_Warnings group.
    /// </summary>
    public string CurrentPageProduction_Warnings
    {
      get { return currentPageProduction_Warnings; }
      set { currentPageProduction_Warnings = value; }
    }

		/*
		 * statistics group
		 */
		/// <summary>last visited page for Statistics group.</summary>
		private string currentPageStatistics;
		/// <summary>
		/// Gets or sets the last visited page for Orders group.
		/// </summary>
		public string CurrentPageStatistics
		{
			get {	return currentPageStatistics;	}
			set {	currentPageStatistics = value;	}
		}

		/// <summary>last visited page for Statistics orders group.</summary>
		private string currentPageStatisticsOrders;
		/// <summary>
		/// Gets or sets the last visited page for Statistics Orders group.
		/// </summary>
		public string CurrentPageStatisticsOrders
		{
			get { return currentPageStatisticsOrders; }
			set { currentPageStatisticsOrders = value; }
		}

    /// <summary>last visited page for Statistics orders group.</summary>
    private string currentPageStatisticsRolls;
    /// <summary>
    /// Gets or sets the last visited page for Statistics Orders group.
    /// </summary>
    public string CurrentPageStatisticsRolls
    {
      get { return currentPageStatisticsRolls; }
      set { currentPageStatisticsRolls = value; }
    }

		/*
		 * settings group
		 */
		/// <summary>last visited page for Settings group.</summary>
		private string currentPageSettings;
		/// <summary>
		/// Gets or sets the last visited page for Settings group.
		/// </summary>
		public string CurrentPageSettings
		{
			get {	return currentPageSettings;	}
			set {	currentPageSettings = value;	}
		}
    private string currentPageSettings_Simulation;
    public string CurrentPageSettings_Simulation
    {
      get { return currentPageSettings_Simulation; }
      set { currentPageSettings_Simulation = value; }
    }

		/*
		 * SyncroSystem group
		 */
		/// <summary>last visited page for SyncroSystem group.</summary>
		private string currentPageSyncroSystem;
		/// <summary>
		/// Gets or sets the last visited page for SyncroSystem group.
		/// </summary>
		public string CurrentPageSyncroSystem
		{
			get { return currentPageSyncroSystem; }
			set { currentPageSyncroSystem = value; }
		}
		/// <summary>last visited page for SyncroSystem_AOC_WEC subgroup.</summary>
		private string currentPageSyncroSystem_AOC_WEC;
		/// <summary>
		/// Gets or sets the last visited page for SyncroSystem_AOC_WEC subgroup.
		/// </summary>
		public string CurrentPageSyncroSystem_AOC_WEC
		{
			get { return currentPageSyncroSystem_AOC_WEC; }
			set { currentPageSyncroSystem_AOC_WEC = value; }
		}
		/// <summary>last visited page for SyncroSystem_Speed subgroup.</summary>
		private string currentPageSyncroSystem_Speed;
		/// <summary>
		/// Gets or sets the last visited page for SyncroSystem_Speed subgroup.
		/// </summary>
		public string CurrentPageSyncroSystem_Speed
		{
			get { return currentPageSyncroSystem_Speed; }
			set { currentPageSyncroSystem_Speed = value; }
		}
		/// <summary>last visited page for SyncroSystem_SpeedReference subgroup.</summary>
		private string currentPageSyncroSystem_SpeedReference;
		/// <summary>
		/// Gets or sets the last visited page for SyncroSystem_SpeedReference subgroup.
		/// </summary>
		public string CurrentPageSyncroSystem_SpeedReference
		{
			get { return currentPageSyncroSystem_SpeedReference; }
			set { currentPageSyncroSystem_SpeedReference = value; }
		}
		/// <summary>last visited page for SyncroSystem_Syncro subgroup.</summary>
		private string currentPageSyncroSystem_Syncro;
		/// <summary>
		/// Gets or sets the last visited page for SyncroSystem_Syncro subgroup.
		/// </summary>
		public string CurrentPageSyncroSystem_Syncro
		{
			get { return currentPageSyncroSystem_Syncro; }
			set { currentPageSyncroSystem_Syncro = value; }
		}
    /// <summary>last visited page for SyncroSystem_Printers subgroup.</summary>
    private string currentPageSyncroSystem_Printers;
    /// <summary>
    /// Gets or sets the last visited page for SyncroSystem_Printers subgroup.
    /// </summary>
    public string CurrentPageSyncroSystem_Printers
    {
        get { return currentPageSyncroSystem_Printers; }
        set { currentPageSyncroSystem_Printers = value; }
    }

		//RFazzi_FI28/09/2006_2.Added Office Page
		/// <summary>last visited page for SyncroSystem_Office subgroup.</summary>
		private string currentPageSyncroSystem_Office;
		/// <summary>
		/// Gets or sets the last visited page for SyncroSystem_Office subgroup.
		/// </summary>
		public string CurrentPageSyncroSystem_Office
		{
			get { return currentPageSyncroSystem_Office; }
			set { currentPageSyncroSystem_Office = value; }
		}

		//RFazzi_FI28/09/2006_2.Added Office Page
		/// <summary>last visited page for SyncroSystem_Office subgroup.</summary>
		private string currentPageSyncroSystem_PRC;
		/// <summary>
		/// Gets or sets the last visited page for SyncroSystem_Office subgroup.
		/// </summary>
		public string CurrentPageSyncroSystem_PRC
		{
			get { return currentPageSyncroSystem_PRC; }
			set { currentPageSyncroSystem_PRC = value; }
		}

		/// <summary>last visited page for SyncroSystem_Log subgroup.</summary>
		private string currentPageSyncroSystem_Log;
		/// <summary>
		/// Gets or sets the last visited page for SyncroSystem_Log subgroup.
		/// </summary>
		public string CurrentPageSyncroSystem_Log
		{
			get { return currentPageSyncroSystem_Log; }
			set { currentPageSyncroSystem_Log = value; }
		}

    /// <summary>last visited page for SyncroSystem_Operations subgroup.</summary>
    private string currentPageSyncroSystem_Operations;
    /// <summary>
    /// Gets or sets the last visited page for SyncroSystem_Operations subgroup.
    /// </summary>
    public string CurrentPageSyncroSystem_Operations
    {
      get { return currentPageSyncroSystem_Operations; }
      set { currentPageSyncroSystem_Operations = value; }
    }


		/**
		 * single facer 1 group
		 */
		/// <summary>last visited page for SingleFacer1 group.</summary>
		private string currentPageSingleFacer1;
		/// <summary>
		/// Gets or sets the last visited page for SingleFacer1 group.
		/// </summary>
		public string CurrentPageSingleFacer1
		{
			get {	return currentPageSingleFacer1;	}
			set {	currentPageSingleFacer1 = value;	}
		}
		/// <summary>last visited page for SingleFacerAlarm1 group.</summary>
		private string currentPageSingleFacerAlarm1;
		/// <summary>
		/// Gets or sets the last visited page for SingleFacerAlarm1 group.
		/// </summary>
		public string CurrentPageSingleFacerAlarm1
		{
			get { return currentPageSingleFacerAlarm1; }
			set { currentPageSingleFacerAlarm1 = value; }
		}
		/// <summary>last visited page for SingleFacerSettings1 group.</summary>
		private string currentPageSingleFacerSettings1;
		/// <summary>
		/// Gets or sets the last visited page for SingleFacerSettings1 group.
		/// </summary>
		public string CurrentPageSingleFacerSettings1
		{
			get { return currentPageSingleFacerSettings1; }
			set { currentPageSingleFacerSettings1 = value; }
		}
		/// <summary>last visited page for SingleFacerStatus1 group.</summary>
		private string currentPageSingleFacerStatus1;
		/// <summary>
		/// Gets or sets the last visited page for SingleFacerStatus1 group.
		/// </summary>
		public string CurrentPageSingleFacerStatus1
		{
			get { return currentPageSingleFacerStatus1; }
			set { currentPageSingleFacerStatus1 = value; }
		}
		/// <summary>last visited page for SingleFacerConsole1 group.</summary>
		private string currentPageSingleFacerConsole1;
		/// <summary>
		/// Gets or sets the last visited page for SingleFacerConsole1 group.
		/// </summary>
		public string CurrentPageSingleFacerConsole1
		{
			get { return currentPageSingleFacerConsole1; }
			set { currentPageSingleFacerConsole1 = value; }
		}


		/**
		 * single facer 2 group
		 */
		/// <summary>last visited page for SingleFacer2 group.</summary>
		private string currentPageSingleFacer2;
		/// <summary>
		/// Gets or sets the last visited page for SingleFacer2 group.
		/// </summary>
		public string CurrentPageSingleFacer2
		{
			get { return currentPageSingleFacer2; }
			set { currentPageSingleFacer2 = value; }
		}
		/// <summary>last visited page for SingleFacerAlarm2 group.</summary>
		private string currentPageSingleFacerAlarm2;
		/// <summary>
		/// Gets or sets the last visited page for SingleFacerAlarm2 group.
		/// </summary>
		public string CurrentPageSingleFacerAlarm2
		{
			get { return currentPageSingleFacerAlarm2; }
			set { currentPageSingleFacerAlarm2 = value; }
		}
		/// <summary>last visited page for SingleFacerSettings2 group.</summary>
		private string currentPageSingleFacerSettings2;
		/// <summary>
		/// Gets or sets the last visited page for SingleFacerSettings2 group.
		/// </summary>
		public string CurrentPageSingleFacerSettings2
		{
			get { return currentPageSingleFacerSettings2; }
			set { currentPageSingleFacerSettings2 = value; }
		}
		/// <summary>last visited page for SingleFacerStatus2 group.</summary>
		private string currentPageSingleFacerStatus2;
		/// <summary>
		/// Gets or sets the last visited page for SingleFacerStatus2 group.
		/// </summary>
		public string CurrentPageSingleFacerStatus2
		{
			get { return currentPageSingleFacerStatus2; }
			set { currentPageSingleFacerStatus2 = value; }
		}
		/// <summary>last visited page for SingleFacerConsole2 group.</summary>
		private string currentPageSingleFacerConsole2;
		/// <summary>
		/// Gets or sets the last visited page for SingleFacerConsole2 group.
		/// </summary>
		public string CurrentPageSingleFacerConsole2
		{
			get { return currentPageSingleFacerConsole2; }
			set { currentPageSingleFacerConsole2 = value; }
		}

		/**
		 * single facer 3 group
		 */
		/// <summary>last visited page for SingleFacer3 group.</summary>
		private string currentPageSingleFacer3;
		/// <summary>
		/// Gets or sets the last visited page for SingleFacer3 group.
		/// </summary>
		public string CurrentPageSingleFacer3
		{
			get { return currentPageSingleFacer3; }
			set { currentPageSingleFacer3 = value; }
		}
		/// <summary>last visited page for SingleFacerAlarm3 group.</summary>
		private string currentPageSingleFacerAlarm3;
		/// <summary>
		/// Gets or sets the last visited page for SingleFacerAlarm3 group.
		/// </summary>
		public string CurrentPageSingleFacerAlarm3
		{
			get { return currentPageSingleFacerAlarm3; }
			set { currentPageSingleFacerAlarm3 = value; }
		}
		/// <summary>last visited page for SingleFacerSettings3 group.</summary>
		private string currentPageSingleFacerSettings3;
		/// <summary>
		/// Gets or sets the last visited page for SingleFacerSettings3 group.
		/// </summary>
		public string CurrentPageSingleFacerSettings3
		{
			get { return currentPageSingleFacerSettings3; }
			set { currentPageSingleFacerSettings3 = value; }
		}
		/// <summary>last visited page for SingleFacerStatus3 group.</summary>
		private string currentPageSingleFacerStatus3;
		/// <summary>
		/// Gets or sets the last visited page for SingleFacerStatus3 group.
		/// </summary>
		public string CurrentPageSingleFacerStatus3
		{
			get { return currentPageSingleFacerStatus3; }
			set { currentPageSingleFacerStatus3 = value; }
		}
		/// <summary>last visited page for SingleFacerConsole3 group.</summary>
		private string currentPageSingleFacerConsole3;
		/// <summary>
		/// Gets or sets the last visited page for SingleFacerConsole3 group.
		/// </summary>
		public string CurrentPageSingleFacerConsole3
		{
			get { return currentPageSingleFacerConsole3; }
			set { currentPageSingleFacerConsole3 = value; }
		}

		/*
		* splicer1 group
		*/
		/// <summary>last visited page for splicer1 group.</summary>
		private string currentPageSplicer1;
		/// <summary>
		/// Gets or sets the last visited page for splicer1 group.
		/// </summary>
		public string CurrentPageSplicer1
		{
			get { return currentPageSplicer1; }
			set { currentPageSplicer1 = value; }
		}
		/// <summary>last visited page for splicerAlarm1 subgroup.</summary>
		private string currentPageSplicerAlarm1;
		/// <summary>
		/// Gets or sets the last visited page for splicerAlarm1 subgroup.
		/// </summary>
		public string CurrentPageSplicerAlarm1
		{
			get { return currentPageSplicerAlarm1; }
			set { currentPageSplicerAlarm1 = value; }
		}
		/// <summary>last visited page for splicerSettings1 subgroup.</summary>
		private string currentPageSplicerSettings1;
		/// <summary>
		/// Gets or sets the last visited page for stackerSettings1 subgroup.
		/// </summary>
		public string CurrentPageSplicerSettings1
		{
			get { return currentPageSplicerSettings1; }
			set { currentPageSplicerSettings1 = value; }
		}

		/*
		* splicer2 group
		*/
		/// <summary>last visited page for splicer2 group.</summary>
		private string currentPageSplicer2;
		/// <summary>
		/// Gets or sets the last visited page for splicer2 group.
		/// </summary>
		public string CurrentPageSplicer2
		{
			get { return currentPageSplicer2; }
			set { currentPageSplicer2 = value; }
		}
		/// <summary>last visited page for splicerAlarm2 subgroup.</summary>
		private string currentPageSplicerAlarm2;
		/// <summary>
		/// Gets or sets the last visited page for splicerAlarm2 subgroup.
		/// </summary>
		public string CurrentPageSplicerAlarm2
		{
			get { return currentPageSplicerAlarm2; }
			set { currentPageSplicerAlarm2 = value; }
		}
		/// <summary>last visited page for splicerSettings2 subgroup.</summary>
		private string currentPageSplicerSettings2;
		/// <summary>
		/// Gets or sets the last visited page for stackerSettings2 subgroup.
		/// </summary>
		public string CurrentPageSplicerSettings2
		{
			get { return currentPageSplicerSettings2; }
			set { currentPageSplicerSettings2 = value; }
		}

		/*
		* splicer3 group
		*/
		/// <summary>last visited page for splicer3 group.</summary>
		private string currentPageSplicer3;
		/// <summary>
		/// Gets or sets the last visited page for splicer3 group.
		/// </summary>
		public string CurrentPageSplicer3
		{
			get { return currentPageSplicer3; }
			set { currentPageSplicer3 = value; }
		}
		/// <summary>last visited page for splicerAlarm3 subgroup.</summary>
		private string currentPageSplicerAlarm3;
		/// <summary>
		/// Gets or sets the last visited page for splicerAlarm3 subgroup.
		/// </summary>
		public string CurrentPageSplicerAlarm3
		{
			get { return currentPageSplicerAlarm3; }
			set { currentPageSplicerAlarm3 = value; }
		}
		/// <summary>last visited page for splicerSettings3 subgroup.</summary>
		private string currentPageSplicerSettings3;
		/// <summary>
		/// Gets or sets the last visited page for stackerSettings3 subgroup.
		/// </summary>
		public string CurrentPageSplicerSettings3
		{
			get { return currentPageSplicerSettings3; }
			set { currentPageSplicerSettings3 = value; }
		}

		/*
		* splicer4 group
		*/
		/// <summary>last visited page for splicer4 group.</summary>
		private string currentPageSplicer4;
		/// <summary>
		/// Gets or sets the last visited page for splicer4 group.
		/// </summary>
		public string CurrentPageSplicer4
		{
			get { return currentPageSplicer4; }
			set { currentPageSplicer4 = value; }
		}
		/// <summary>last visited page for splicerAlarm4 subgroup.</summary>
		private string currentPageSplicerAlarm4;
		/// <summary>
		/// Gets or sets the last visited page for splicerAlarm4 subgroup.
		/// </summary>
		public string CurrentPageSplicerAlarm4
		{
			get { return currentPageSplicerAlarm4; }
			set { currentPageSplicerAlarm4 = value; }
		}
		/// <summary>last visited page for splicerSettings4 subgroup.</summary>
		private string currentPageSplicerSettings4;
		/// <summary>
		/// Gets or sets the last visited page for stackerSettings4 subgroup.
		/// </summary>
		public string CurrentPageSplicerSettings4
		{
			get { return currentPageSplicerSettings4; }
			set { currentPageSplicerSettings4 = value; }
		}

		/*
		* splicer5 group
		*/
		/// <summary>last visited page for splicer5 group.</summary>
		private string currentPageSplicer5;
		/// <summary>
		/// Gets or sets the last visited page for splicer5 group.
		/// </summary>
		public string CurrentPageSplicer5
		{
			get { return currentPageSplicer5; }
			set { currentPageSplicer5 = value; }
		}
		/// <summary>last visited page for splicerAlarm5 subgroup.</summary>
		private string currentPageSplicerAlarm5;
		/// <summary>
		/// Gets or sets the last visited page for splicerAlarm5 subgroup.
		/// </summary>
		public string CurrentPageSplicerAlarm5
		{
			get { return currentPageSplicerAlarm5; }
			set { currentPageSplicerAlarm5 = value; }
		}
		/// <summary>last visited page for splicerSettings5 subgroup.</summary>
		private string currentPageSplicerSettings5;
		/// <summary>
		/// Gets or sets the last visited page for stackerSettings5 subgroup.
		/// </summary>
		public string CurrentPageSplicerSettings5
		{
			get { return currentPageSplicerSettings5; }
			set { currentPageSplicerSettings5 = value; }
		}

		/*
		* splicer6 group
		*/
		/// <summary>last visited page for splicer6 group.</summary>
		private string currentPageSplicer6;
		/// <summary>
		/// Gets or sets the last visited page for splicer6 group.
		/// </summary>
		public string CurrentPageSplicer6
		{
			get { return currentPageSplicer6; }
			set { currentPageSplicer6 = value; }
		}
		/// <summary>last visited page for splicerAlarm6 subgroup.</summary>
		private string currentPageSplicerAlarm6;
		/// <summary>
		/// Gets or sets the last visited page for splicerAlarm6 subgroup.
		/// </summary>
		public string CurrentPageSplicerAlarm6
		{
			get { return currentPageSplicerAlarm6; }
			set { currentPageSplicerAlarm6 = value; }
		}
		/// <summary>last visited page for splicerSettings6 subgroup.</summary>
		private string currentPageSplicerSettings6;
		/// <summary>
		/// Gets or sets the last visited page for stackerSettings6 subgroup.
		/// </summary>
		public string CurrentPageSplicerSettings6
		{
			get { return currentPageSplicerSettings6; }
			set { currentPageSplicerSettings6 = value; }
		}

		/*
		* splicer7 group
		*/
		/// <summary>last visited page for splicer7 group.</summary>
		private string currentPageSplicer7;
		/// <summary>
		/// Gets or sets the last visited page for splicer7 group.
		/// </summary>
		public string CurrentPageSplicer7
		{
			get { return currentPageSplicer7; }
			set { currentPageSplicer7 = value; }
		}
		/// <summary>last visited page for splicerAlarm7 subgroup.</summary>
		private string currentPageSplicerAlarm7;
		/// <summary>
		/// Gets or sets the last visited page for splicerAlarm7 subgroup.
		/// </summary>
		public string CurrentPageSplicerAlarm7
		{
			get { return currentPageSplicerAlarm7; }
			set { currentPageSplicerAlarm7 = value; }
		}
		/// <summary>last visited page for splicerSettings7 subgroup.</summary>
		private string currentPageSplicerSettings7;
		/// <summary>
		/// Gets or sets the last visited page for stackerSettings7 subgroup.
		/// </summary>
		public string CurrentPageSplicerSettings7
		{
			get { return currentPageSplicerSettings7; }
			set { currentPageSplicerSettings7 = value; }
		}

		/*
		 * Double Backer group
		 */
		/// <summary>last visited page for SingleFacer1 group.</summary>
		private string currentPageDoubleBacker;
		/// <summary>
		/// Gets or sets the last visited page for SingleFacer1 group.
		/// </summary>
		public string CurrentPageDoubleBacker
		{
			get { return currentPageDoubleBacker; }
			set { currentPageDoubleBacker = value; }
		}
		private string currentPageDoubleBackerAlarms;
		public string CurrentPageDoubleBackerAlarms
		{
			get { return currentPageDoubleBackerAlarms; }
			set { currentPageDoubleBackerAlarms = value; }
		}
		private string currentPageDoubleBackerSettings;
		public string CurrentPageDoubleBackerSettings
		{
			get { return currentPageDoubleBackerSettings; }
			set { currentPageDoubleBackerSettings = value; }
		}
		private string currentPageDoubleBackerStatus;
		public string CurrentPageDoubleBackerStatus
		{
			get { return currentPageDoubleBackerStatus; }
			set { currentPageDoubleBackerStatus = value; }
		}
		private string currentPageDoubleBackerConsole;
		public string CurrentPageDoubleBackerConsole
		{
			get { return currentPageDoubleBackerConsole; }
			set { currentPageDoubleBackerConsole = value; }
		}

		/*
		 * Pre-Heater tern group
		 */
		/// <summary>last visited page for Pre-Heater tern group.</summary>
		private string currentPagePreHeaterTern;
		/// <summary>
		/// gets or sets the last visited page for PreHeaterTern group.
		/// </summary>
		public string CurrentPagePreHeaterTern
		{
			get { return currentPagePreHeaterTern; }
			set { currentPagePreHeaterTern = value; }
		}
		/// <summary>last visited page for PreHeaterTernAlarm subgroup.</summary>
		private string currentPagePreHeaterTernAlarm;
		/// <summary>
		/// Gets or sets the last visited page for PreHeaterTern subgroup.
		/// </summary>
		public string CurrentPagePreHeaterTernAlarm
		{
			get { return currentPagePreHeaterTernAlarm; }
			set { currentPagePreHeaterTernAlarm = value; }
		}
		/// <summary>last visited page for PreHeaterTernSettings subgroup.</summary>
		private string currentPagePreHeaterTernSettings;
		/// <summary>
		/// Gets or sets the last visited page for PreHeaterSettings subgroup.
		/// </summary>
		public string CurrentPagePreHeaterTernSettings
		{
			get { return currentPagePreHeaterTernSettings; }
			set { currentPagePreHeaterTernSettings = value; }
		}


		/*
		 * Glue unit group
		 */
		/// <summary>last visited page for Glue unit group.</summary>
		private string currentPageGlueUnit;
		/// <summary>
		/// gets or sets the last visited page for Glue unit group.
		/// </summary>
		public string CurrentPageGlueUnit
		{
			get { return currentPageGlueUnit; }
			set { currentPageGlueUnit = value; }
		}
		/// <summary>last visited page for GlueUnitAlarm subgroup.</summary>
		private string currentPageGlueUnitAlarm;
		/// <summary>
		/// Gets or sets the last visited page for GlueUnit subgroup.
		/// </summary>
		public string CurrentPageGlueUnitAlarm
		{
			get { return currentPageGlueUnitAlarm; }
			set { currentPageGlueUnitAlarm = value; }
		}
		/// <summary>last visited page for GlueUnitConsole subgroup.</summary>
		private string currentPageGlueUnitConsole;
		/// <summary>
		/// Gets or sets the last visited page for GlueUnitConsole subgroup.
		/// </summary>
		public string CurrentPageGlueUnitConsole
		{
			get { return currentPageGlueUnitConsole; }
			set { currentPageGlueUnitConsole = value; }
		}
		/// <summary>last visited page for GlueUnitSettings subgroup.</summary>
		private string currentPageGlueUnitSettings;
		/// <summary>
		/// Gets or sets the last visited page for GlueUnitSettings subgroup.
		/// </summary>
		public string CurrentPageGlueUnitSettings
		{
			get { return currentPageGlueUnitSettings; }
			set { currentPageGlueUnitSettings = value; }
		}
		/*
		 * Shear group
		 */
		/// <summary>last visited page for Shear group.</summary>
		private string currentPageShear;
		/// <summary>
		/// Gets or sets the last visited page for Shear group.
		/// </summary>
		public string CurrentPageShear
		{
			get { return currentPageShear; }
			set { currentPageShear = value; }
		}
		/// <summary>last visited page for ShearSettings subgroup.</summary>
		private string currentPageShearAlarms;
		/// <summary>
		/// Gets or sets the last visited page for ShearSettings subgroup.
		/// </summary>
		public string CurrentPageShearAlarms
		{
			get { return currentPageShearAlarms; }
			set { currentPageShearAlarms = value; }
		}
		/// <summary>last visited page for ShearSettings subgroup.</summary>
		private string currentPageShearSettings;
		/// <summary>
		/// Gets or sets the last visited page for ShearSettings subgroup.
		/// </summary>
		public string CurrentPageShearSettings
		{
			get { return currentPageShearSettings; }
			set { currentPageShearSettings = value; }
		}

		/*
		 * slitter scorer group
		 */
		/// <summary>last visited page for SlitterScorer group.</summary>
		private string currentPageSlitterScorer;
		/// <summary>
		/// Gets or sets the last visited page for SlitterScorerStatus group.
		/// </summary>
		public string CurrentPageSlitterScorer
		{
			get {	return currentPageSlitterScorer;	}
			set {	currentPageSlitterScorer = value;	}
		}
		/// <summary>last visited page for SlitterScorerAlarms subgroup.</summary>
		private string currentPageSlitterScorerAlarms;
		/// <summary>
		/// Gets or sets the last visited page for SlitterScorerAlarms subgroup.
		/// </summary>
		public string CurrentPageSlitterScorerAlarms
		{
			get { return currentPageSlitterScorerAlarms; }
			set { currentPageSlitterScorerAlarms = value; }
		}
		/// <summary>last visited page for SlitterScorerSettings subgroup.</summary>
		private string currentPageSlitterScorerSettings;
		/// <summary>
		/// Gets or sets the last visited page for SlitterScorerSettings subgroup.
		/// </summary>
		public string CurrentPageSlitterScorerSettings
		{
			get { return currentPageSlitterScorerSettings; }
			set { currentPageSlitterScorerSettings = value; }
		}

		////RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
		///// <summary>last visited page for SlitterScorerBlades subgroup.</summary>
		//private string currentPageSlitterScorerBlades;
		///// <summary>
		///// Gets or sets the last visited page for SlitterScorerBlades subgroup.
		///// </summary>
		//public string CurrentPageSlitterScorerBlades
		//{
		//  get { return currentPageSlitterScorerBlades; }
		//  set { currentPageSlitterScorerBlades = value; }
		//}

		///// <summary>last visited page for SlitterScorerConsole subgroup.</summary>
		//private string currentPageSlitterScorerConsole;
		///// <summary>
		///// Gets or sets the last visited page for SlitterScorerConsole subgroup.
		///// </summary>
		//public string CurrentPageSlitterScorerConsole
		//{
		//  get { return currentPageSlitterScorerConsole; }
		//  set { currentPageSlitterScorerConsole = value; }
		//}

		/// <summary>last visited page for SlitterScorerStatus subgroup.</summary>
		private string currentPageSlitterScorerStatus;
		/// <summary>
		/// Gets or sets the last visited page for SlitterScorerStatus subgroup.
		/// </summary>
		public string CurrentPageSlitterScorerStatus
		{
			get { return currentPageSlitterScorerStatus; }
			set { currentPageSlitterScorerStatus = value; }
		}

		/// <summary>last visited page for SlitterScorerTools subgroup.</summary>
		private string currentPageSlitterScorerTools;
		/// <summary>
		/// Gets or sets the last visited page for SlitterScorerTools subgroup.
		/// </summary>
		public string CurrentPageSlitterScorerTools
		{
			get { return currentPageSlitterScorerTools; }
			set { currentPageSlitterScorerTools = value; }
		}

		/*
		 * knife group
		 */
		/// <summary>last visited page for knife group.</summary>
		private string currentPageKnife;
		/// <summary>
		/// Gets or sets the last visited page for knife group.
		/// </summary>
		public string CurrentPageKnife
		{
			get { return currentPageKnife; }
			set { currentPageKnife = value; }
		}
		/// <summary>last visited page for knifeAlarms subgroup.</summary>
		private string currentPageKnifeAlarms;
		/// <summary>
		/// Gets or sets the last visited page for knifeAlarms subgroup.
		/// </summary>
		public string CurrentPageKnifeAlarms
		{
			get { return currentPageKnifeAlarms; }
			set { currentPageKnifeAlarms = value; }
		}
		/// <summary>last visited page for knifeSettings subgroup.</summary>
		private string currentPageKnifeSettings;
		/// <summary>
		/// Gets or sets the last visited page for knifeSettings subgroup.
		/// </summary>
		public string CurrentPageKnifeSettings
		{
			get { return currentPageKnifeSettings; }
			set { currentPageKnifeSettings = value; }
		}

		/*
		 * stacker1 group
		 */
		/// <summary>last visited page for stacker1 group.</summary>
		private string currentPageStacker1;
		/// <summary>
		/// Gets or sets the last visited page for stacker1 group.
		/// </summary>
		public string CurrentPageStacker1
		{
			get { return currentPageStacker1; }
			set { currentPageStacker1 = value; }
		}
    /// <summary>last visited page for StackerStatus1 group.</summary>
    private string currentPageStackerStatus1;
    /// <summary>
    /// Gets or sets the last visited page for StackerStatus1 group.
    /// </summary>
    public string CurrentPageStackerStatus1
    {
      get { return currentPageStackerStatus1; }
      set { currentPageStackerStatus1 = value; }
    }
		/// <summary>last visited page for stackerAlarm1 subgroup.</summary>
		private string currentPageStackerAlarm1;
		/// <summary>
		/// Gets or sets the last visited page for stackerAlarm1 subgroup.
		/// </summary>
		public string CurrentPageStackerAlarm1
		{
			get { return currentPageStackerAlarm1; }
			set { currentPageStackerAlarm1 = value; }
		}
		/// <summary>last visited page for stackerSettings1 subgroup.</summary>
		private string currentPageStackerSettings1;
		/// <summary>
		/// Gets or sets the last visited page for stackerSettings1 subgroup.
		/// </summary>
		public string CurrentPageStackerSettings1
		{
			get { return currentPageStackerSettings1; }
			set { currentPageStackerSettings1 = value; }
		}

		/*
		 * stacker2 group
		 */
		/// <summary>last visited page for stacker2 group.</summary>
		private string currentPageStacker2;
		/// <summary>
		/// Gets or sets the last visited page for stacker2 group.
		/// </summary>
		public string CurrentPageStacker2
		{
			get { return currentPageStacker2; }
			set { currentPageStacker2 = value; }
		}
    /// <summary>last visited page for StackerStatus2 group.</summary>
    private string currentPageStackerStatus2;
    /// <summary>
    /// Gets or sets the last visited page for StackerStatus2 group.
    /// </summary>
    public string CurrentPageStackerStatus2
    {
      get { return currentPageStackerStatus2; }
      set { currentPageStackerStatus2 = value; }
    }
		/// <summary>last visited page for stackerAlarm2 subgroup.</summary>
		private string currentPageStackerAlarm2;
		/// <summary>
		/// Gets or sets the last visited page for stackerAlarm2 subgroup.
		/// </summary>
		public string CurrentPageStackerAlarm2
		{
			get { return currentPageStackerAlarm2; }
			set { currentPageStackerAlarm2 = value; }
		}
		/// <summary>last visited page for stackerSettings2 subgroup.</summary>
		private string currentPageStackerSettings2;
		/// <summary>
		/// Gets or sets the last visited page for stackerSettings2 subgroup.
		/// </summary>
		public string CurrentPageStackerSettings2
		{
			get { return currentPageStackerSettings2; }
			set { currentPageStackerSettings2 = value; }
		}

		/*
		 * stacker3 group
		 */
		/// <summary>last visited page for stacker3 group.</summary>
		private string currentPageStacker3;
		/// <summary>
		/// Gets or sets the last visited page for stacker3 group.
		/// </summary>
		public string CurrentPageStacker3
		{
			get { return currentPageStacker3; }
			set { currentPageStacker3 = value; }
		}
    /// <summary>last visited page for StackerStatus3 group.</summary>
    private string currentPageStackerStatus3;
    /// <summary>
    /// Gets or sets the last visited page for StackerStatus3 group.
    /// </summary>
    public string CurrentPageStackerStatus3
    {
      get { return currentPageStackerStatus3; }
      set { currentPageStackerStatus3 = value; }
    }
		/// <summary>last visited page for stackerAlarm3 subgroup.</summary>
		private string currentPageStackerAlarm3;
		/// <summary>
		/// Gets or sets the last visited page for stackerAlarm3 subgroup.
		/// </summary>
		public string CurrentPageStackerAlarm3
		{
			get { return currentPageStackerAlarm3; }
			set { currentPageStackerAlarm3 = value; }
		}
		/// <summary>last visited page for stackerSettings3 subgroup.</summary>
		private string currentPageStackerSettings3;
		/// <summary>
		/// Gets or sets the last visited page for stackerSettings3 subgroup.
		/// </summary>
		public string CurrentPageStackerSettings3
		{
			get { return currentPageStackerSettings3; }
			set { currentPageStackerSettings3 = value; }
		}

		/*
		 * DryEnd supervisor group
		 */
		/// <summary>last visited page for DryEnd supervisor group.</summary>
		private string currentPageDryEndSupervisor;
		/// <summary>
		/// Gets or sets the last visited page for DryEnd supervisor group.
		/// </summary>
		public string CurrentPageDryEndSupervisor
		{
			get { return currentPageDryEndSupervisor; }
			set { currentPageDryEndSupervisor = value; }
		}
		//WetEnd supervisor group
		/// <summary>last visited page for WetEnd supervisor group.</summary>
		private string currentPageWetEndSupervisor;
		/// <summary>
		/// Gets or sets the last visited page for WetEnd supervisor group.
		/// </summary>
		public string CurrentPageWetEndSupervisor
		{
			get { return currentPageWetEndSupervisor; }
			set { currentPageWetEndSupervisor = value; }
		}
		//debug group
		/// <summary>last visited page for Debug group.</summary>
		private string currentPageDebug;
		/// <summary>
		/// Gets or sets the last visited page for Debug group.
		/// </summary>
		public string CurrentPageDebug
		{
			get { return currentPageDebug; }
			set { currentPageDebug = value; }
		}

    /*
		 * twin400 subgroup 
		 */
    /// <summary>last visited page for Debugtwin400 subgroup.</summary>
    private string currentPageDebugTwin400;
    /// <summary>
    /// Gets or sets the last visited page for twin400 group.
    /// </summary>
    public string CurrentPageDebugTwin400
    {
      get { return currentPageDebugTwin400; }
      set { currentPageDebugTwin400 = value; }
    }








		//////////////////////////////////////////////////////////////
    ///////////////////		SYNCRO400 group		//////////////////////
		//////////////////////////////////////////////////////////////

    /*
		 * twin400 group 
		 */
		/// <summary>last visited page for twin400 group.</summary>
		private string currentPageTwin400;
		/// <summary>
		/// Gets or sets the last visited page for twin400 group.
		/// </summary>
		public string CurrentPageTwin400
		{
			get {	return currentPageTwin400;	}
			set {	currentPageTwin400 = value;	}
		}








		////////////////////////////////////////////////////////////
		///////////////////		TWIN400 group		//////////////////////
		////////////////////////////////////////////////////////////

		/*
		 * twin400_Alarms subgroup
		 */
		/// <summary>last visited page for twin400_Alarms subgroup.</summary>
		private string currentPageTwin400_Alarms;
		/// <summary>
		/// gets or sets the last visited page for twin400_Alarms subgroup.
		/// </summary>
		public string CurrentPageTwin400_Alarms
		{
			get {	return currentPageTwin400_Alarms;	}
			set {	currentPageTwin400_Alarms = value;	}
		}

    //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
    /*
     * twin400_Warnings subgroup
     */
    /// <summary>last visited page for twin400_Warnings subgroup.</summary>
    private string currentPageTwin400_Warnings;
    /// <summary>
    /// gets or sets the last visited page for twin400_Warnings subgroup.
    /// </summary>
    public string CurrentPageTwin400_Warnings
    {
      get { return currentPageTwin400_Warnings; }
      set { currentPageTwin400_Warnings = value; }
    }

		/*
		 * twin400_Flutes subgroup
		 */
		/// <summary>last visited page for twin400_Flutes subgroup.</summary>
		private string currentPageTwin400_Flutes;
		/// <summary>
		/// gets or sets the last visited page for twin400_Flutes subgroup.
		/// </summary>
		public string CurrentPageTwin400_Flutes
		{
			get {	return currentPageTwin400_Flutes;	}
			set {	currentPageTwin400_Flutes = value;	}
		}

		/*
		 * twin400_Maintenance subgroup
		 */
		/// <summary>last visited page for twin400_Maintenance subgroup.</summary>
		private string currentPageTwin400_Maintenance;
		/// <summary>
		/// gets or sets the last visited page for twin400_Maintenance subgroup.
		/// </summary>
		public string CurrentPageTwin400_Maintenance
		{
			get {	return currentPageTwin400_Maintenance;	}
			set {	currentPageTwin400_Maintenance = value;	}
		}

		/*
		 * twin400_Orders subgroup
		 */
		/// <summary>last visited page for twin400_Orders subgroup.</summary>
		private string currentPageTwin400_Orders;
		/// <summary>
		/// gets or sets the last visited page for twin400_Orders subgroup.
		/// </summary>
		public string CurrentPageTwin400_Orders
		{
			get {	return currentPageTwin400_Orders;	}
			set {	currentPageTwin400_Orders = value;	}
		}

		/*
		 * twin400_Production subgroup
		 */
		/// <summary>last visited page for twin400_Production subgroup.</summary>
		private string currentPageTwin400_Production;
		/// <summary>
		/// gets or sets the last visited page for twin400_Production subgroup.
		/// </summary>
		public string CurrentPageTwin400_Production
		{
			get {	return currentPageTwin400_Production;	}
			set {	currentPageTwin400_Production = value;	}
		}

		/*
		 * twin400_Settings subgroup
		 */
		/// <summary>last visited page for twin400_Settings subgroup.</summary>
		private string currentPageTwin400_Settings;
		/// <summary>
		/// gets or sets the last visited page for twin400_Settings subgroup.
		/// </summary>
		public string CurrentPageTwin400_Settings
		{
			get {	return currentPageTwin400_Settings;	}
			set {	currentPageTwin400_Settings = value;	}
		}

		/*
		 * twin400_Tools subgroup
		 */
		/// <summary>last visited page for twin400_Tools subgroup.</summary>
		private string currentPageTwin400_Tools;
		/// <summary>
		/// gets or sets the last visited page for twin400_Tools subgroup.
		/// </summary>
		public string CurrentPageTwin400_Tools
		{
			get {	return currentPageTwin400_Tools;	}
			set {	currentPageTwin400_Tools = value;	}
		}

		/*
		 * twin400_Status subgroup
		 */
		/// <summary>last visited page for twin400_Status subgroup.</summary>
		private string currentPageTwin400_Status;
		/// <summary>
		/// gets or sets the last visited page for twin400_Status subgroup.
		/// </summary>
		public string CurrentPageTwin400_Status
		{
			get { return currentPageTwin400_Status; }
			set { currentPageTwin400_Status = value; }
		}










		////////////////////////////////////////////////////////
		/////////////////		HISTORIAN METHODS		////////////////
		////////////////////////////////////////////////////////

		/// <summary>
		/// Get the first page to show at the start up of the application. 
		/// </summary>
		public string FirstPage
		{
			get {	return Settings.getSettings().forms.FirstPage;	}
		}

		/// <summary>
		/// Builder.
		/// </summary>
		public PagesHistorian()
		{
			historian = new ArrayList(MAX_HISTORIAN_LENGTH);
			this.Reset();
			Accessibility.getAccessibility().OnAccessibilityChanged+=new SyncroC.Utility.Accessibility.AccessibilityChangedHandler(this.OnAccessibilityChange);
		}

		/// <summary>
		/// Supposed to be called when the Back button is pressed.
		/// </summary>
		public void BackPressed()
		{
			if (IsPossibleGoBack())
				backPressed = true;
		}

		/// <summary>
		/// Supposed to be called when the Forward button is pressed.
		/// </summary>
		public void ForwardPressed()
		{
			if (IsPossibleGoForward())
				forwardPressed = true;
		}

		/// <summary>
		/// Supposed to be called every time a page is showed (Frm_XXXGroup._Show()).
		/// Keep the historian of visited page updated.
		/// </summary>
		/// <param name="requestCommandPage">The new page beeing showed.</param>
		public void PageChanged(Frm_Page newPage)
		{
			PageChanged(newPage, 0);
		}

		/// <summary>
		/// Supposed to be called every time a page is showed (Frm_XXXGroup._Show()).
		/// Keep the historian of visited page updated.
		/// </summary>
		/// <param name="requestCommandPage">The new page beeing showed.</param>
		/// <param name="index">The index of the group of the new page (0 for non indexed groups).</param>
		public void PageChanged(Frm_Page newPage, int index)
		{
			if (backPressed || forwardPressed)
			{//Page change requested, Back or Forward button has been pressed: update only pointers
				if (backPressed && currentPagePointer > 0)
				{
					previousPagePointer = currentPagePointer;
					currentPagePointer--;
				}
				else if (forwardPressed && currentPagePointer < historian.Count - 1)
				{
					previousPagePointer = currentPagePointer;
					currentPagePointer++;
				}
			}
			else
			{//Page change requested without using Back or Forward buttons: historian need to be updated

        //ABello_FI08/11/2007_2 bug fix nello storico delle pagine indicizzate
        //string requestCommandPage = newPage.getRequestCommand() +((index != 0) ? index.ToString() : "");
        string requestCommandPage = (index != 0) ? newPage.getRequestCommand(index) : newPage.getRequestCommand();
				if (historian.Count == 0 || ! requestCommandPage.Equals((string)historian[historian.Count - 1]))
					{
						previousPagePointer = currentPagePointer;
						if (historian.Count == MAX_HISTORIAN_LENGTH)
						{
							historian.RemoveAt(0);
							previousPagePointer--;
						}
						historian.Add(requestCommandPage);
						currentPagePointer = historian.Count - 1;
					}
			}
			//reset both falgs
			backPressed = false;
			forwardPressed = false;
			//fire OnPageChange event
			if (OnPageChanged != null)
			{
				OnPageChanged(this, new OnPageChangedEventArgs(newPage.GetType().BaseType, index));
			}
		}

		/// <summary></summary>
		/// <returns>The name of the page currently showed.</returns>
		public string GetCurrentPage()
		{
			if (currentPagePointer >= 0 && currentPagePointer < historian.Count)
				return (string)historian[currentPagePointer];
			return "";
		}

		/// <summary></summary>
		/// <returns>The name of the page that was showed immediatly before the current one.</returns>
		public string GetPreviousPage()
		{
			if (previousPagePointer >= 0 && previousPagePointer < historian.Count)
				return (string)historian[previousPagePointer];
			return "";
		}

		/// <summary></summary>
		/// <returns>The name of the page that has to be showed when Back button
		/// is pressed, according to the historian.</returns>
		public string BackPage()
		{
			if (currentPagePointer > 0 && currentPagePointer < historian.Count)
				return (string)historian[currentPagePointer - 1];
			return "";
		} 

		/// <summary></summary>
		/// <returns>The name of the page that has to be showed when Forward button
		/// is pressed, according to the historian.</returns>
		public string ForwardPage()
		{
			if (currentPagePointer >= 0 && currentPagePointer < historian.Count - 1)
				return (string)historian[currentPagePointer + 1];
			return "";
		}

		/// <summary>
		/// Determine if it is possible going back to the historian or we are already 
		/// at the begining of the historian.
		/// </summary>
		public bool IsPossibleGoBack()
		{
			return (currentPagePointer != 0);
		}

		/// <summary>
		/// Determine if it is possible going forward to the historian or we are already 
		/// at the end of the historian.
		/// </summary>
		public bool IsPossibleGoForward()
		{
			return (currentPagePointer != historian.Count - 1);
		}

		//ABello_FI12/07/2007_1 Bisogna ricalcolare le pagine dopo che il server ha mandato la configurazione
		public void Reset()
		{
			if (historian != null && historian.Count > 0)
			{
				string CurrentPageTMP = GetCurrentPage();
				historian.Clear();
				currentPagePointer = previousPagePointer = -1;
				backPressed = forwardPressed = false;
				historian.Add(CurrentPageTMP);
				currentPagePointer = historian.Count - 1;
			}

			SyncroC.Utility.Settings.Configuration config = SyncroC.Utility.Settings.getSettings().configuration;

			//production group and subgroups
			if (config.IsDryEndControlOnly() || config.IsDryEndControlAndPMac400())
				this.currentPageProduction = PageManager.getRequestCommand(typeof(Frm_HomeDry));
			else if (config.IsWetEndControlOnly())
				this.currentPageProduction = PageManager.getRequestCommand(typeof(Frm_HomeWet));
			else if (config.DryEndControl && config.WetEndControl)
				this.currentPageProduction = PageManager.getRequestCommand(typeof(Frm_HomeDry));

      this.currentPageProduction_Home = this.currentPageProduction;
      this.currentPageProduction_Operator = PageManager.getRequestCommand(typeof(Frm_Operator)); 

			if (config.DryEndControl)
				this.currentPageProduction_OrdersIns = PageManager.getRequestCommand(typeof(Frm_QuickInsDryOrder));
			else if (config.WetEndControl)
				this.currentPageProduction_OrdersIns = PageManager.getRequestCommand(typeof(Frm_InsWetOrder));

			if (config.IsDryEndControlOnly() || config.IsDryEndControlAndPMac400())
				this.currentPageProduction_OrdersList = PageManager.getRequestCommand(typeof(Frm_DryOrdersList));
			else if (config.IsWetEndControlOnly())
				this.currentPageProduction_OrdersList = PageManager.getRequestCommand(typeof(Frm_WetOrdersList));
			else if (config.DryEndControl && config.WetEndControl)
				this.currentPageProduction_OrdersList = PageManager.getRequestCommand(typeof(Frm_DryWetOrdersList));

			this.currentPageProduction_Alarms = PageManager.getRequestCommand(typeof(Frm_Alarm1));

      //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
      this.currentPageProduction_Warnings = PageManager.getRequestCommand(typeof(Frm_Warning1));

			//statistics group
			this.currentPageStatistics = PageManager.getRequestCommand(typeof(Frm_ShiftReport));
			this.currentPageStatisticsOrders = PageManager.getRequestCommand(typeof(Frm_StatisticsOrdersList));
      this.currentPageStatisticsRolls = PageManager.getRequestCommand(typeof(Frm_StatisticsRollsList));

			//settings group
      this.currentPageSettings = PageManager.getRequestCommand(typeof(Frm_Languages));
      this.currentPageSettings_Simulation = PageManager.getRequestCommand(typeof(Frm_SimulationGeneral));

			//single facer groups
			this.currentPageSingleFacer1 = PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1);
			this.currentPageSingleFacerAlarm1 = PageManager.getRequestCommand(typeof(Frm_SingleFacerAlarmActive), 1);
			this.currentPageSingleFacerSettings1 = PageManager.getRequestCommand(typeof(Frm_SingleFacerSettingsMachine), 1);
			this.currentPageSingleFacerStatus1 = PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 1);
			this.currentPageSingleFacerConsole1 = PageManager.getRequestCommand(typeof(Frm_SingleFacerConsole01), 1);

			this.currentPageSingleFacer2 = PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2);
			this.currentPageSingleFacerAlarm2 = PageManager.getRequestCommand(typeof(Frm_SingleFacerAlarmActive), 2);
			this.currentPageSingleFacerSettings2 = PageManager.getRequestCommand(typeof(Frm_SingleFacerSettingsMachine), 2);
			this.currentPageSingleFacerStatus2 = PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 2);
			this.currentPageSingleFacerConsole2 = PageManager.getRequestCommand(typeof(Frm_SingleFacerConsole01), 2);

			this.currentPageSingleFacer3 = PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 3);
			this.currentPageSingleFacerAlarm3 = PageManager.getRequestCommand(typeof(Frm_SingleFacerAlarmActive), 3);
			this.currentPageSingleFacerSettings3 = PageManager.getRequestCommand(typeof(Frm_SingleFacerSettingsMachine), 3);
			this.currentPageSingleFacerStatus3 = PageManager.getRequestCommand(typeof(Frm_SingleFacerStatus01), 3);
			this.currentPageSingleFacerConsole3 = PageManager.getRequestCommand(typeof(Frm_SingleFacerConsole01), 3);

			//splicer groups and subgroups
			this.currentPageSplicer1 = PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 1);
			this.currentPageSplicer2 = PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 2);
			this.currentPageSplicer3 = PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 3);
			this.currentPageSplicer4 = PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 4);
			this.currentPageSplicer5 = PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 5);
			this.currentPageSplicer6 = PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 6);
			this.currentPageSplicer7 = PageManager.getRequestCommand(typeof(Frm_SplicerStatus), 7);

			this.currentPageSplicerAlarm1 = PageManager.getRequestCommand(typeof(Frm_SplicerAlarmActive), 1);
			this.currentPageSplicerAlarm2 = PageManager.getRequestCommand(typeof(Frm_SplicerAlarmActive), 2);
			this.currentPageSplicerAlarm3 = PageManager.getRequestCommand(typeof(Frm_SplicerAlarmActive), 3);
			this.currentPageSplicerAlarm4 = PageManager.getRequestCommand(typeof(Frm_SplicerAlarmActive), 4);
			this.currentPageSplicerAlarm5 = PageManager.getRequestCommand(typeof(Frm_SplicerAlarmActive), 5);
			this.currentPageSplicerAlarm6 = PageManager.getRequestCommand(typeof(Frm_SplicerAlarmActive), 6);
			this.currentPageSplicerAlarm7 = PageManager.getRequestCommand(typeof(Frm_SplicerAlarmActive), 7);

			this.currentPageSplicerSettings1 = PageManager.getRequestCommand(typeof(Frm_SplicerSettingsMachine), 1);
			this.currentPageSplicerSettings2 = PageManager.getRequestCommand(typeof(Frm_SplicerSettingsMachine), 2);
			this.currentPageSplicerSettings3 = PageManager.getRequestCommand(typeof(Frm_SplicerSettingsMachine), 3);
			this.currentPageSplicerSettings4 = PageManager.getRequestCommand(typeof(Frm_SplicerSettingsMachine), 4);
			this.currentPageSplicerSettings5 = PageManager.getRequestCommand(typeof(Frm_SplicerSettingsMachine), 5);
			this.currentPageSplicerSettings6 = PageManager.getRequestCommand(typeof(Frm_SplicerSettingsMachine), 6);
			this.currentPageSplicerSettings7 = PageManager.getRequestCommand(typeof(Frm_SplicerSettingsMachine), 7);

			//stacker groups and subgroups
			this.currentPageStacker1 = PageManager.getRequestCommand(typeof(Frm_StackerStatus01), 1);
      this.currentPageStackerStatus1 = PageManager.getRequestCommand(typeof(Frm_StackerStatus01), 1);
      this.currentPageStackerAlarm1 = PageManager.getRequestCommand(typeof(Frm_StackerAlarmActive), 1);
      this.currentPageStackerSettings1 = PageManager.getRequestCommand(typeof(Frm_StackerSettingsMachine), 1);

			this.currentPageStacker2 = PageManager.getRequestCommand(typeof(Frm_StackerStatus01), 2);
      this.currentPageStackerStatus2 = PageManager.getRequestCommand(typeof(Frm_StackerStatus02), 2);
			this.currentPageStackerAlarm2 = PageManager.getRequestCommand(typeof(Frm_StackerAlarmActive), 2);
      this.currentPageStackerSettings2 = PageManager.getRequestCommand(typeof(Frm_StackerSettingsMachine), 2);

      this.currentPageStacker3 = PageManager.getRequestCommand(typeof(Frm_StackerStatus01), 3);
      this.currentPageStackerStatus3 = PageManager.getRequestCommand(typeof(Frm_StackerStatus03), 3);
			this.currentPageStackerAlarm3 = PageManager.getRequestCommand(typeof(Frm_StackerAlarmActive), 3);
			this.currentPageStackerSettings3 = PageManager.getRequestCommand(typeof(Frm_StackerSettingsMachine), 3);

			//double backer group
			this.currentPageDoubleBacker = PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01));
			this.currentPageDoubleBackerStatus  = PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01));
			this.currentPageDoubleBackerConsole = PageManager.getRequestCommand(typeof(Frm_DoubleBackerConsole01));
			this.currentPageDoubleBackerAlarms = PageManager.getRequestCommand(typeof(Frm_DoubleBackerAlarmsActive));
      this.currentPageDoubleBackerSettings = PageManager.getRequestCommand(typeof(Frm_DoubleBackerSettingsMachine));

			//preheater tern unit group
			this.currentPagePreHeaterTern = PageManager.getRequestCommand(typeof(Frm_PreHeaterTernStatus));
			this.currentPagePreHeaterTernSettings = PageManager.getRequestCommand(typeof(Frm_PreHeaterTernSettingsMachine));
      this.currentPagePreHeaterTernAlarm = PageManager.getRequestCommand(typeof(Frm_PreHeaterTernAlarmActive));

			//glue unit unit group
			this.currentPageGlueUnit = PageManager.getRequestCommand(typeof(Frm_GlueUnitStatus));
			this.currentPageGlueUnitSettings = PageManager.getRequestCommand(typeof(Frm_GlueUnitSettingsMachine));
			this.currentPageGlueUnitConsole = PageManager.getRequestCommand(typeof(Frm_GlueUnitConsoleWrapArms));
			this.currentPageGlueUnitAlarm = PageManager.getRequestCommand(typeof(Frm_GlueUnitAlarmActive));

			//shear group and subgroups
			this.currentPageShear = PageManager.getRequestCommand(typeof(Frm_ShearStatus));
			this.currentPageShearSettings = PageManager.getRequestCommand(typeof(Frm_ShearSettingsMachine));
			this.currentPageShearAlarms = PageManager.getRequestCommand(typeof(Frm_ShearAlarmActive));

			//slitter scorer group and subgroups
			this.currentPageSlitterScorer = PageManager.getRequestCommand(typeof(Frm_SlitterScorerStatus));
			this.currentPageSlitterScorerAlarms = PageManager.getRequestCommand(typeof(Frm_SlitterScorerAlarmActive));
			this.currentPageSlitterScorerSettings = PageManager.getRequestCommand(typeof(Frm_SlitterScorerSettingsMachine));
			this.currentPageSlitterScorerStatus = PageManager.getRequestCommand(typeof(Frm_SlitterScorerStatus));
			this.currentPageSlitterScorerTools = PageManager.getRequestCommand(typeof(Frm_SlitterScorerWebDirector));

			//knife group and subgroups
			this.currentPageKnife = PageManager.getRequestCommand(typeof(Frm_KnifeStatus));
			this.currentPageKnifeAlarms = PageManager.getRequestCommand(typeof(Frm_KnifeAlarmActive));
			this.currentPageKnifeSettings = PageManager.getRequestCommand(typeof(Frm_KnifeSettingsMachine));

			//debug group and subgroups
      this.currentPageDebug = PageManager.getRequestCommand(typeof(Frm_DebugDatabase));
      this.currentPageDebugTwin400 = PageManager.getRequestCommand(typeof(Frm_DebugTwin400Status1));

			//wetend supervisor group
      this.currentPageWetEndSupervisor = PageManager.getRequestCommand(typeof(Frm_WetEndSupervisor01));

			//dryend supervisor group
      this.currentPageDryEndSupervisor = PageManager.getRequestCommand(typeof(Frm_DryEndSupervisor01));

			//twin 400 group and subgroups
			this.currentPageTwin400 = PageManager.getRequestCommand(typeof(Frm_twin400_console));
			this.currentPageTwin400_Alarms = PageManager.getRequestCommand(typeof(Frm_twin400_alarm1));
      //RFAZZI_FI26/01/2011_2 Aggiunta gestione Warning su grafica Twin400 di Syncro. 
      this.currentPageTwin400_Warnings = PageManager.getRequestCommand(typeof(Frm_twin400_warning1));
			this.currentPageTwin400_Flutes = PageManager.getRequestCommand(typeof(Frm_twin400_quality));
			this.currentPageTwin400_Maintenance = PageManager.getRequestCommand(typeof(Frm_twin400_maintenance));
			this.currentPageTwin400_Orders = PageManager.getRequestCommand(typeof(Frm_twin400_ordersList));
			this.currentPageTwin400_Production = PageManager.getRequestCommand(typeof(Frm_twin400_console));
			this.currentPageTwin400_Settings = PageManager.getRequestCommand(typeof(Frm_twin400_misc));
			this.currentPageTwin400_Tools = PageManager.getRequestCommand(typeof(Frm_twin400_bladesAndScorers));
			this.currentPageTwin400_Status = PageManager.getRequestCommand(typeof(Frm_twin400_overview));
			this.currentPageSyncroSystem = PageManager.getRequestCommand(typeof(Frm_AOCSettings));
			this.currentPageSyncroSystem_AOC_WEC = PageManager.getRequestCommand(typeof(Frm_AOCSettings));
			this.currentPageSyncroSystem_Speed = PageManager.getRequestCommand(typeof(Frm_SpeedSettings));
			this.currentPageSyncroSystem_SpeedReference = PageManager.getRequestCommand(typeof(Frm_SpeedReferenceSettings));
			this.currentPageSyncroSystem_Syncro = PageManager.getRequestCommand(typeof(Frm_SyncroSettings));
      this.currentPageSyncroSystem_Printers = PageManager.getRequestCommand(typeof(Frm_PRINTERSSettings));
      this.currentPageSyncroSystem_Operations = PageManager.getRequestCommand(typeof(Frm_SyncroSystemOperations));
			//RFazzi_FI28/09/2006_2.Added Office Page
			this.currentPageSyncroSystem_Office = PageManager.getRequestCommand(typeof(Frm_OFFICESettings));
			this.currentPageSyncroSystem_Log = PageManager.getRequestCommand(typeof(Frm_Log1));
			this.currentPageSyncroSystem_PRC = PageManager.getRequestCommand(typeof(Frm_PRCSettings));
		}

		private void OnAccessibilityChange(object sender, AccessibilityChangedEventArgs e)
		{
			if (e.accessibilityLevel<e.oldAccessibilityLevel)
				this.Reset();
		}
	}
}
