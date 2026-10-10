using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;

using SyncroC.Forms.Pages;
using SyncroC.Forms.StatusBar;
using SyncroC.Forms.LateralBar;
using SyncroC.Forms.Pages.Statistics;
using SyncroC.Forms.Pages.Production;
using SyncroC.Forms.Pages.Production.Home;
using SyncroC.Forms.Pages.Production.OrdersIns;
using SyncroC.Forms.Pages.Production.OrdersList;
using SyncroC.Forms.Pages.Settings;
using SyncroC.Forms.Pages.SingleFacer;
using SyncroC.Forms.Pages.SlitterScorer;
using SyncroC.Forms.Pages.Shear;
using SyncroC.Forms.Pages.Twin400.Orders;
using SyncroC.Forms.Pages.Twin400.Tools;
using SyncroC.Forms.Pages.Twin400.Flutes;
using SyncroC.Forms.Pages.Twin400.Alarms;
using SyncroC.Forms.Pages.Twin400.Settings;
using SyncroC.Forms.Pages.Twin400.Maintenance;
using SyncroC.Forms.Pages.DoubleBacker;
using SyncroC.Forms.Pages.Debug;
using SyncroC.Forms.Pages.Debug.Twin400;
using SyncroC.Forms.Pages.Knife;
using SyncroC.Forms.Pages.Stacker;
using SyncroC.Forms.Pages.DryEndSupervisor;
using SyncroC.Forms.Pages.WetEndSupervisor;


namespace SyncroC.Utility
{
	/// <summary>
	/// Provides all the application settings.
	/// </summary>
	public class Settings
	{
		//sections
		#region section Network

		/// <summary>
		/// Provides all the application settings for network connection.
		/// </summary>
		public struct Network
		{
			private string ipServerAddress;
			private int udpReceivePort;
			private int udpSendPort;
			private int tcpPort;
      //agrassi_FI08-01-09_4
      private bool netConfigManagerEnable;
			//RFAZZI_FI02/10/2009_1. Aggiunta possibilità di avviare più client sul solito PC.
			private int clientID;

			/// <summary>
			/// Initialize the struct whit default values.
			/// </summary>
			/// <param name="unuseful">parameter required by the compiler but never used.</param>
			public Network(Settings settings)
			{
				//Default values: the actual values are set by Settings' constructor
        //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        //IPAddress localIpAddress = ipHostInfo.AddressList[0];
				this.ipServerAddress = "10.151.151.1";
        this.udpReceivePort = 2001;
				this.udpSendPort = 2000;
				this.tcpPort = 0;
        //agrassi_FI08-01-09_4
        this.netConfigManagerEnable = true;
				//RFAZZI_FI02/10/2009_1. Aggiunta possibilità di avviare più client sul solito PC.
        this.clientID = 1;
        //graffaelli_FI07/11/2011_2 - Change the ipaddress and clientID values for the second client
        if (Process.GetCurrentProcess().MainModule.ModuleName.Contains("2"))
        {
          this.ipServerAddress = "127.0.0.1";
          this.clientID = 2;
        }
      }

			[Category("TCP"), Description("IP address of the server.")]
			public string IpServerAddress
			{
				get { return ipServerAddress; }
				set { ipServerAddress = value; }
			}
			[Category("UDP"), Description("Port number to use for receiving messages in UDP protocol from the server.")]
			public int UdpReceivePort
			{
				get { return udpReceivePort; }
				set { udpReceivePort = value; }
			}
			[Category("UDP"), Description("Port number to use for sending messages in UDP protocol to te server.")]
			public int UdpSendPort
			{
				get { return udpSendPort; }
				set { udpSendPort = value; }
			}
			[Category("TCP"), Description("Port number to use for receiving and sending messages in TCP protocol from and to the server.")]
			public int TcpPort
			{
				get { return tcpPort; }
				set { tcpPort = value; }
			}

      //agrassi_FI08-01-09_4
      [Category("Misc"), Description("Enable or disable the automatic backup and restore of network configuration")]
      public bool NetConfigManagerEnable
      {
        get { return netConfigManagerEnable; }
        set { netConfigManagerEnable = value; }
      }
			//RFAZZI_FI02/10/2009_1. Aggiunta possibilità di avviare più client sul solito PC.
			[Category("Misc"), Description("Client ID number. Handled in server side for have multiple client on the same PC. Must be set as in the Syncro.INI/SyncroS section related of this client")]
			public int ClientID
			{
				get { return clientID; }
				set { clientID = value; }
			}
		}

		#endregion
		public Settings.Network network;

		#region section Forms

		/// <summary>
		/// Provides all the application settings for the forms management.
		/// </summary>
		public struct Forms
		{
			private int maxCacheSize;
			private string firstPage;
			private bool corrugatorDirectionRight;
			//RFAZZI_FI01/08/2009_1. Aggiunta possibilità di invertire la pagina grafica in Verticale
			private bool machineDirectionVisualizationInverted;

			/// <summary>
			/// Initialize the struct whit default values.
			/// </summary>
			/// <param name="unuseful">parameter required by the compiler but never used.</param>
			public Forms(int unuseful)
			{
				maxCacheSize = 3;
				firstPage = PageManager.getRequestCommand(typeof(Frm_HomeDry));
				corrugatorDirectionRight = true;
				//RFAZZI_FI01/08/2009_1. Aggiunta possibilità di invertire la pagina grafica in Verticale
				machineDirectionVisualizationInverted=false;

			}
			[Category("Misc"), Description("Maximum number of the forms kept cached.")]
			public int MaxCacheSize
			{
				get { return maxCacheSize; }
				set 
				{
					if (value <= 1)
					{
						maxCacheSize = 2;
						SyncroC.Utility.Logger.LogError(this, "MaxCacheSize Set - specified value is \"" + value + "\", it must be greather than 0 and recommended to be at least 2");
					}
					else
						maxCacheSize = value;
				}
			}
			[Category("Misc"), Description("page to start the application from.")]
			public string FirstPage
			{
				get { return firstPage; }
				set 
				{
					Type pageManagerType = typeof(PageManager);
					FieldInfo field = pageManagerType.GetField(value, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
					if (field != null && typeof(Frm_Page).IsAssignableFrom(field.FieldType))
						firstPage = value;
					else
						SyncroC.Utility.Logger.LogError(this, "FirstPage property set - incorrect value \"" + value + "\"");
				}
			}
			[Category("Misc"), Description("page to start the application from.")]
			public bool CorrugatorDirectionRight
			{
				get { return corrugatorDirectionRight; }
				set { corrugatorDirectionRight = value;}
			}

			//RFAZZI_FI01/08/2009_1. Aggiunta possibilità di invertire la pagina grafica in Verticale
			[Category("Misc"), Description("invert the graphics in vertical for have a good visualization when the client is fitted with the stacker on the back.")]
			public bool MachineDirectionVisualizationInverted
			{
				get { return machineDirectionVisualizationInverted; }
				set { machineDirectionVisualizationInverted = value; }
			}
		}

		#endregion
		public Settings.Forms forms;

		#region section Logger

		/// <summary>
		/// Provides all the settings for the Log stuf.
		/// </summary>
		public struct Logger
		{
			private bool enabled;
			//private string logFolder;
			private long maxLogFileSize;
			/// <summary>
			/// Initialize the struct whit default values.
			/// </summary>
			/// <param name="unuseful">parameter required by the compiler but never used.</param>
			public Logger(int unuseful)
			{
				enabled = true;
				maxLogFileSize = 5000000; //5 megabyte;
			}
			[Category("Misc"), Description("Determine if the Log system should be enabled.")]
			public bool Enabled
			{
				get { return enabled; }
				set { enabled = value; }
			}
			[Category("Misc"), Description("Maximum number of bytes for a log file.")]
			public long MaxLogFileSize
			{
				get { return maxLogFileSize; }
				set { maxLogFileSize = value; }
			}
		}

		#endregion
		public Settings.Logger logger;

		#region section DataBase

		/// <summary>
		/// Provides all the application settings for database managing.
		/// </summary>
		public struct DataBase
		{
			//private string dataBaseFolder;
			private bool restoreDBLanguages;
      private Settings settings;
			/// <summary>
			/// Initialize the struct whit default values.
			/// </summary>
			/// <param name="unuseful">parameter required by the compiler but never used.</param>
			public DataBase(Settings settings)
			{
				restoreDBLanguages = false;
        this.settings = settings;
			}
			[Category("Misc"), Description("Determine if the language database should be restored.")]
			public bool RestoreDBLanguages
			{
				get { return restoreDBLanguages; }
				set 
        {
          restoreDBLanguages = value;
          this.settings.Save(this.GetType().Name, "RestoreDBLanguages", Convert.ToString(value));
        }
			}
		}

		#endregion
		public Settings.DataBase dataBase;

		#region section Languages

		/// <summary>
		/// Provides all the application settings for languages.
		/// </summary>
		public struct Languages
		{
			private string firstLanguage;
			private string secondLanguage;
			private bool rapidTranslationEnabled;
      private Settings settings;
			/// <summary>
			/// Initialize the struct whit default values.
			/// </summary>
			/// <param name="unuseful">parameter required by the compiler but never used.</param>
			public Languages(Settings settings)
			{
				firstLanguage = "English";
				secondLanguage = "Italian";
				rapidTranslationEnabled = true;
        this.settings = settings;
			}
			[Category("Languages"), Description("First language used by the application.")]
			public string FirstLanguage
			{
				get { return firstLanguage; }
				set
				{
					if (firstLanguage != null && !firstLanguage.Equals(value))
					{
						firstLanguage = value;
            this.settings.Save(this.GetType().Name, "FirstLanguage", value);
					}
				}
			}
			[Category("Languages"), Description("Second language used by the application.")]
			public string SecondLanguage
			{
				get { return secondLanguage; }
				set
				{
					if (!secondLanguage.Equals(value))
					{
						secondLanguage = value;
            this.settings.Save(this.GetType().Name, "SecondLanguage", value);
					}
				}
			}
			[Category("Misc"), Description("Determine if the rapid translation facility is enabled.")]
			public bool RapidTranslationEnabled
			{
				get { return rapidTranslationEnabled; }
				set
				{
					if (!rapidTranslationEnabled.Equals(value))
					{
						rapidTranslationEnabled = value;
            this.settings.Save(this.GetType().Name, "RapidTranslationEnabled", Convert.ToString(value));
					}
				}
			}
		}

		#endregion
    public Settings.Languages languages;

		#region section Configuration

		/// <summary>
		/// Provides all the information to configure the Client for requested purposes.
		/// </summary>
		public struct Configuration
		{
			private bool dryEndControl;
			private bool wetEndControl;
			private bool pMac400;
			private bool prc;
			private bool onServer;
      //agrassi_FI25-08-08_5
      private bool onFosberNetwork;

			/// <summary>
			/// Initialize the struct whit default values.
			/// </summary>
			/// <param name="unuseful">parameter required by the compiler but never used.</param>
			public Configuration(int unuseful)
			{
				dryEndControl = true;
				wetEndControl = true;
				pMac400 = false;
				prc = false;
        //agrassi_FI25-08-08_5
        onServer = false;
        //onServer = true;
        onFosberNetwork = false;
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="serverIpAddress"></param>
			//RFAZZI_FI02/10/2009_1. Aggiunta possibilità di avviare più client sul solito PC.
			public void DetermineOnServer(string serverIpAddress, int clientID)
			{
				//IPAddress[] clientIPAddress = Dns.GetHostByName(Dns.GetHostName()).AddressList;
				IPAddress[] clientIPAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
				int addrIdx;
        //agrassi_FI25-08-08_5
        string IPAddressFosberNetwork = "10.151.151";
				for (addrIdx = 0; addrIdx < clientIPAddress.Length; addrIdx++)
				{
					string ipAddress = clientIPAddress[addrIdx].ToString();
          //agrassi_FI25-08-08_5
          if (ipAddress.StartsWith(IPAddressFosberNetwork))
            this.onFosberNetwork = true;
					if (serverIpAddress.Equals(ipAddress)  )
            //agrassi_FI25-08-08_5
            this.onServer=true;
            //break;
				}
        //agrassi_FI25-08-08_5
        //if ((addrIdx == clientIPAddress.Length) && (!serverIpAddress.Equals("127.0.0.1")))
        //  this.onServer = false;
				//RFAZZI_FI02/10/2009_1. Aggiunta possibilità di avviare più client sul solito PC.
				if (!this.onServer)
          this.onServer = serverIpAddress.Equals("127.0.0.1") && (clientID == 1);
        if (!this.onFosberNetwork)
          this.onFosberNetwork = serverIpAddress.Equals("127.0.0.1");
      }
			[Category("Misc"), Description("Determine if the client control the Dry End.")]
			public bool DryEndControl
			{
				get { return dryEndControl; }
				set { dryEndControl = value; }
			}
			[Category("Misc"), Description("Determine if the client control the Wet End.")]
			public bool WetEndControl
			{
				get { return wetEndControl; }
				set { wetEndControl = value; }
			}
			[Category("Misc"), Description("Determine if the client control the Twin 400.")]
			public bool PMac400
			{
				get { return pMac400; }
				set { pMac400 = value; }
			}
			[Category("Misc"), Description("Determine if the client includes the process control.")]
			public bool PRC
			{
				get { return prc; }
				set { prc = value; }
			}

			/// <summary>
			/// 
			/// </summary>
			/// <returns>true if the client is running on same pc as the server, false otherwise.</returns>
			public bool getOnServer()
			{
				return onServer;
			}

      //agrassi_25-08-08_5
      public bool getOnFosberNetwork()
      {
        return onFosberNetwork;
      }
      /// <summary>
			/// Determine if the client is configured as a Twin400 supervisor only.
			/// </summary>
			public bool IsPmac400()
			{
				return (this.pMac400 && (!this.dryEndControl) && (!this.wetEndControl)
									&& (!this.prc));
			}
			/// <summary>
			/// Determine if the client is configured as a Twin400 supervisor and 
			/// as DryEnd control or WetEnd control.
			/// </summary>
			public bool IsSyncro400()
			{
				return (this.pMac400 && (this.dryEndControl || this.wetEndControl));
			}
			/// <summary>
			/// Determine if the client is configured as a Dry End supervisor only.
			/// </summary>
			public bool IsDryEndControlOnly()
			{
				return (this.dryEndControl && !this.pMac400 && !this.wetEndControl);
			}
			/// <summary>
			/// Determine if the client is configured as a Dry End supervisor only.
			/// </summary>
			public bool IsDryEndControlAndPMac400()
			{
				return (this.dryEndControl && this.pMac400 && !this.wetEndControl);
			}
			/// <summary>
			/// Determine if the client is configured as a Wet End supervisor only.
			/// </summary>
			public bool IsWetEndControlOnly()
			{
				return (!this.dryEndControl && this.wetEndControl);
			}
			/// <summary>
			/// Determine if the client is configured as DryEnd control or WetEnd control.
			/// </summary>
			public bool IsSyncro()
			{
				return (!this.pMac400 && (this.dryEndControl || this.wetEndControl));
			}
			/// <summary>
			/// Determine if the client is configured as Process Control.
			/// </summary>
			public bool IsPRCOnly()
			{
				return (this.prc && !(this.pMac400 || this.dryEndControl || this.wetEndControl));
			}
		}

		#endregion
		public Settings.Configuration configuration;

    #region section Paths
    public struct Paths
    {
      private string syncroRoot;
      private string serverProduction;
      private string serverConfig;
      //agrassi_FI26-11-08
      private string serverPrinterConfig;
      private string translations;
      private string serverSharedOnServer;
      private string serverSharedFromNetwork;
      private string clientData;
      private string log;
      private string logZip;
			private string data;
			private string dataZip;
      //PMASSARO_FI05/02/09 - Added Command For Resetting Production
      private string savedProduction;
			private string source;
			private string sourceZip;
      //agrassi_FI24-09-08
      //private string reportWithoutDate;
      private string reportZip;
      private string documentation;

			public Paths(string theIpServerAddress)
      {
				this.syncroRoot = Path.GetFullPath(Application.StartupPath + "\\..\\..\\..\\");
				this.source = Path.GetFullPath(Application.StartupPath + "\\..\\..\\");
        this.serverProduction = Path.GetFullPath(this.syncroRoot + "Data\\Server\\Production\\");
        this.serverConfig = Path.GetFullPath(this.syncroRoot + "Data\\Server\\Config\\");
        //agrassi_FI26-11-08
        this.serverPrinterConfig = Path.GetFullPath(this.serverConfig + "Printers\\");
        this.translations = Path.GetFullPath(this.source + "_Translations\\");
        this.serverSharedOnServer = Path.GetFullPath(this.syncroRoot + "Data\\Server\\Shared\\");
				//this.serverSharedFromNetwork = Path.GetFullPath("\\\\Syncro\\Shared\\");
				this.serverSharedFromNetwork = Path.GetFullPath("\\\\" + theIpServerAddress + "\\Shared\\");

        this.clientData = Path.GetFullPath(this.syncroRoot + "Data\\Client\\");
        this.log = Path.GetFullPath(this.syncroRoot + "Log\\");
				this.logZip = Path.GetFullPath("c:\\Backup\\Syncro\\Log\\");
				this.data = Path.GetFullPath(this.syncroRoot + "Data\\");
				this.dataZip = Path.GetFullPath("c:\\Backup\\Syncro\\Data\\");
        //PMASSARO_FI05/02/09 - Added Command For Resetting Production
        this.savedProduction = Path.GetFullPath("c:\\Backup\\Syncro\\SavedProduction\\");
        this.sourceZip = Path.GetFullPath("c:\\Backup\\Syncro\\Source\\");
        this.documentation = Path.GetFullPath("c:\\Backup\\Syncro\\Doc\\");
        //agrassi_FI24-09-08
        //this.reportWithoutDate = Path.GetFullPath("c:\\Backup\\Syncro\\Report");
        this.reportZip = Path.GetFullPath("c:\\Backup\\Syncro\\Report\\");
      }

      public string SyncroRoot
      { get { return this.syncroRoot; } }

      public string ServerProduction
      { get { return this.serverProduction; } }

      public string ServerConfig
      { get { return this.serverConfig; } }

      //agrassi_FI26-11-08
      public string ServerPrinterConfig
      { get { return this.serverPrinterConfig; } }

      public string Translations
      { get { return this.translations; } }

      public string ServerSharedOnServer
      { get { return this.serverSharedOnServer; } }

      public string ServerSharedFromNetwork
      { get { return this.serverSharedFromNetwork; } }

      public string ClientData
      { get { return this.clientData; } }

      public string Log
      { get { return this.log; } }

			//RFAZZI_FI31/03/2009_1.Added Backup Data and Source on External Backup HD
			public string LogZip
			{
				get { return this.logZip; }
				set { this.logZip = value; }
			}

			public string Data
			{ get { return this.data; } }

      //PMASSARO_FI05/02/09 - Added Command For Resetting Production
			public string SavedProduction 
			{ get { return this.savedProduction ; } }

			public string DataZip
			{ get { return this.dataZip; }}

			public string Source
			{ get { return this.source; } }

			public string SourceZip
			{ get { return this.sourceZip; }}

      //agrassi_FI24-09-08
      //public string ReportWithoutDate
      //{ get { return this.reportWithoutDate; } }
			public string ReportZip
      { get { return this.reportZip; }}

			public string Documentation
      { get { return this.documentation; }}
    }
    #endregion
    public Settings.Paths paths;

		#region section Facility

		/// <summary>
		/// Provides all the information to configure the Client for requested purposes.
		/// </summary>
		public struct Facility
		{
			private bool editSetupQueue;

			public bool EditSetupQueue
			{
				get 
				{
          //ABELLO_FI10/01/2011_1 Inserendo la password Media ora si possono modificare le qualità, gli ordini, etc.. anche dagli uffici
					//if (Accessibility.getAccessibility().isLevelHigh())
          if (Accessibility.getAccessibility().isLevelAtLeastMedium())
						return true;
					else
						return editSetupQueue; 
				}
				set { editSetupQueue = value; }
			}
			private bool changeAOCMode;

			public bool ChangeAOCMode
			{
				get 
				{
					if (Accessibility.getAccessibility().isLevelHigh())
						return true;
					else
						return changeAOCMode; 
				}
				set { changeAOCMode = value; }
			}
			private bool editOperatorSettings;

			public bool EditOperatorSettings
			{
				get 
				{
					if (Accessibility.getAccessibility().isLevelHigh())
						return true;
					else
						return editOperatorSettings; 
				}
				set { editOperatorSettings = value; }
			}
			private bool assignDowntimes;

			public bool AssignDowntimes
			{
				get 
				{
					if (Accessibility.getAccessibility().isLevelHigh())
						return true;
					else
						return assignDowntimes; 
				}
				set { assignDowntimes = value; }
			}
			private bool changeShift;

			public bool ChangeShift
			{
				get 
				{
					if (Accessibility.getAccessibility().isLevelHigh())
						return true;
					else
						return changeShift; 
				}
				set { changeShift = value; }
			}

			private bool changeWETAutoManMode;

			public bool ChangeWETAutoManMode
			{
				get
				{
					if (Accessibility.getAccessibility().isLevelHigh())
						return true;
					else
						return changeWETAutoManMode; 
				}
				set { changeWETAutoManMode = value; }
			}
			private bool lowLevelEnabled;

			public bool LowLevelEnabled
			{
				get 
				{
					if (Accessibility.getAccessibility().isLevelHigh())
						return true;
					else
						return lowLevelEnabled; 
				}
				set { lowLevelEnabled = value; }
			}

			private bool manualOrdersSending;

			public bool ManualOrdersSending
			{
				get
				{
					if (Accessibility.getAccessibility().isLevelHigh())
						return true;
					else
						return manualOrdersSending;
				}
				set { manualOrdersSending = value; }
			}

			/// <summary>
			/// Initialize the struct whit default values.
			/// </summary>
			/// <param name="unuseful">parameter required by the compiler but never used.</param>
			public Facility(int unuseful)
			{
				this.editSetupQueue = false;
				this.changeAOCMode = false;
				this.editOperatorSettings = false;
				this.assignDowntimes = false;
				this.changeShift = false;
				this.changeWETAutoManMode = false;
				this.lowLevelEnabled = false;
				this.manualOrdersSending = false;
			}
		}

		#endregion
		public Settings.Facility facility;

    //the following field is not permanently saved in any file
		private string currentLanguage;
		/// <summary>
		/// Language currently used by the application.
		/// (this data is not saved in any file)
		/// </summary>
		public string CurrentLanguage
		{
			get { return currentLanguage; }
			set { currentLanguage = value; }
		}

		/// <summary>needed to realize the singleton pattern.</summary>
		private static Settings settings = null;

		/// <summary>name of the ini file where to store the settings.</summary>
		private static string FILE_INI_NAME = 
			System.Windows.Forms.Application.ProductName.ToString() + ".ini";

		private bool excludeFromIni(Type type)
		{
			return (
								(typeof(SyncroC.Utility.Settings.Configuration).IsAssignableFrom(type)) ||
								(typeof(SyncroC.Utility.Settings.Forms).IsAssignableFrom(type)) ||
								(typeof(SyncroC.Utility.Settings.Paths).IsAssignableFrom(type)) ||
								(typeof(SyncroC.Utility.Settings.Facility).IsAssignableFrom(type))
						  ); //add in or any other section to be excluded from .ini savage here
		}

		/// <summary>
		/// factory.
		/// </summary>
		/// <returns>the one and only Settings object of the application</returns>
		public static Settings getSettings()
		{
			if (settings == null) settings = new Settings();
			return settings;
		}

		/// <summary>
		/// Builder.
		/// </summary>
		public Settings()
		{
      this.network = new Settings.Network(this);
			this.paths = new Settings.Paths(this.network.IpServerAddress);
			this.forms = new Forms(0);
			this.logger = new Settings.Logger(0);
			this.dataBase = new Settings.DataBase(this);
			this.languages = new Settings.Languages(this);
			this.configuration = new Settings.Configuration(0);
			this.facility = new Settings.Facility(0);
			//These were default values: the actual values are set below

			//folder management
			if (! Directory.Exists(this.paths.ClientData))
			{	
				try
				{
          Directory.CreateDirectory(this.paths.ClientData);
				}
				catch (Exception)
				{
				}
			}
			//loop all the sections (fields of Settings class)
			Type typeSettings = this.GetType();
			FieldInfo[] sections = typeSettings.GetFields();
			for (int i = 0; i < sections.Length; i++)
			{
				Object section = null;
				try
				{
					section = sections[i].GetValue(this);
				}
				catch (Exception ex)
				{
					SyncroC.Utility.Logger.LogError(this, "Settings Could't get the value of the field \"" + sections[i].Name + "\" - " + ex.ToString());
				}
				if (section != null)
				{
					//loop all the items of the current section (properties of the struct)
					Type typeSection = section.GetType();
					if (! excludeFromIni(typeSection))
					{
						PropertyInfo[] properties = typeSection.GetProperties();
						for (int j = 0; j < properties.Length; j++)
						{
							Object item = null;
							try
							{
								item = properties[j].GetValue(section, null);
							}
							catch (Exception ex)
							{
                SyncroC.Utility.Logger.LogError(this, "Settings Could't get the value of the property \"" + sections[i].Name + "." + properties[j].Name + "\" - " + ex.ToString());
							}
							if (item != null)
							{
								string mydefault = item.ToString();
								string val = Ini.Get(sections[i].Name, properties[j].Name, mydefault, this.paths.ClientData + FILE_INI_NAME);
								try
								{//set the property of the struct (item)
									properties[j].SetValue(section, Convert.ChangeType(val, properties[j].PropertyType), null);
								}	
								catch(Exception ex)
								{
                  SyncroC.Utility.Logger.LogError(this, "Settings Could't set the value of the property \"" + sections[i].Name + "." + properties[j].Name + "\" - " + ex.ToString());
								}
							}
						}
						try
						{//set the field of Settings class (section)
							sections[i].SetValue(this, section);
						}
						catch (Exception ex)
						{
              SyncroC.Utility.Logger.LogError(this, "Settings Could't set the value of the field \"" + sections[i].Name + "\" - " + ex.ToString());
						}
					}
				}
			}

			this.paths = new Settings.Paths(this.network.IpServerAddress);
			currentLanguage = languages.FirstLanguage;
			//RFAZZI_FI02/10/2009_1. Aggiunta possibilità di avviare più client sul solito PC.
			this.configuration.DetermineOnServer(this.network.IpServerAddress, this.network.ClientID);
		}

		public string getFileIniFullPath()
		{
      return this.paths.ClientData + FILE_INI_NAME;
		}

		/// <summary>
		/// Save a property in the .ini file.
		/// </summary>
		/// <param name="section">section where to save the item.</param>
		/// <param name="item">item to save</param>
		/// <param name="val">value to save.</param>
		private void Save(string section, string item, string val)
		{
      Ini.Set(section, item, val, this.paths.ClientData + FILE_INI_NAME);
		}

		public string getFirstStatusBar()
		{
			if (this.forms.FirstPage.ToUpper().Contains("DRY"))
			{
				return PageManager.getRequestCommand(typeof(Frm_StatusBarDryEnd));
			}
			if (this.forms.FirstPage.ToUpper().Contains("WET"))
			{
				return PageManager.getRequestCommand(typeof(Frm_StatusBarWetEnd));
			}
			if (this.forms.FirstPage.ToUpper().Contains("TWIN400"))
			{
        //agrassi_FI25-08-08
        if (configuration.IsPmac400() || ((!configuration.getOnServer()) && (this.forms.FirstPage == "twin400_console")))
        //if (configuration.IsPmac400())
          return PageManager.getRequestCommand(typeof(Frm_StatusBarTwin400));
				else
					return PageManager.getRequestCommand(typeof(Frm_StatusBarDryEnd));
			}

			if (configuration.IsPmac400())
			{
				return PageManager.getRequestCommand(typeof(Frm_StatusBarTwin400));
			}
			if (configuration.IsDryEndControlOnly())
			{
				return PageManager.getRequestCommand(typeof(Frm_StatusBarDryEnd));
			}
			if (configuration.IsWetEndControlOnly())
			{
				return PageManager.getRequestCommand(typeof(Frm_StatusBarWetEnd));
			}
			if (configuration.IsSyncro() || configuration.IsSyncro400())
			{
				return PageManager.getRequestCommand(typeof(Frm_StatusBarDryEnd));
			}

			return PageManager.getRequestCommand(typeof(Frm_StatusBarDryEnd));
		}
	}
}
