
using System;
using System.Data;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.IO;

using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.DataBase;

namespace SyncroC.DataBase.LineConfig
{
	/// <summary>
	/// Summary description for LineConfig.
	/// </summary>
	public class LineConfig : SyncroC.DataBase.LineConfig.LineBase
	{
#if DEBUG
		[DllImport(@"Utility.dll", CallingConvention = CallingConvention.Cdecl)]
#else
		[DllImport(@"Utility.dll",CallingConvention=CallingConvention.Cdecl)]
#endif

		public static extern int FileCrc32Assembly([MarshalAs(UnmanagedType.LPTStr)] string szFilename, ref int dwCrc32);

		public const int MAX_NUM_LEV = 3; //Maximum number of levels in the line

		/// <summary>String to be sent as version in case of error</summary>
		public static readonly string WRONG_VERSION = SyncroConverter.SYNCRO_STYLE_DATETIME_WRONG_STRING;

		//splicer7
		private bool splicer7Enabled;
		private Splicer splicer7 = null;

		//sigleFacer3
		private bool singleFacer3Enabled;
		private SingleFacer singleFacer3 = null;

		//splicer6
		private bool splicer6Enabled;
		private Splicer splicer6 = null;

		//splicer5
		private bool splicer5Enabled;
		private Splicer splicer5 = null;

		//singleFacer2
		private bool singleFacer2Enabled;
		private SingleFacer singleFacer2 = null;

		//splicer4
		private bool splicer4Enabled;
		private Splicer splicer4 = null;

		//splicer3
		private bool splicer3Enabled;
		private Splicer splicer3 = null;

		//sigleFacer1
		private bool singleFacer1Enabled;
		private SingleFacer singleFacer1 = null;

		//splicer2
		private bool splicer2Enabled;
		private Splicer splicer2 = null;

		//splicer1
		private bool splicer1Enabled;
		private Splicer splicer1 = null;

		//Pre-heater tern
		private bool preHeatersTernEnabled;
		private PreHeatersTern preHeatersTern = null;

		//Glue Unit
		private bool glueUnitEnabled;
		private GlueUnit glueUnit = null;

		//Double Backer
		private bool doubleBackerEnabled;
		private DoubleBacker doubleBacker = null;

		//Rotary
		private bool rotaryEnabled;
		private Rotary rotary = null;

		//Slitter Scorer
		private bool slitterScorerEnabled;
		private SlitterScorer slitterScorer = null;

		//Web Director
		private bool webDirectorEnabled;
		private WebDirector webDirector = null;

		//Knife
		private bool knifeEnabled;
		private Knife knife = null;

		//Stacker1
		private bool stacker1Enabled;
		private Stacker stacker1 = null;

		//Stacker2
		private bool stacker2Enabled;
		private Stacker stacker2 = null;

		//Stacker3
		private bool stacker3Enabled;
		private Stacker stacker3 = null;

		//Bridge1
		private bool bridge1Enabled;
		private Bridge bridge1 = null;

		//Bridge2
		private bool bridge2Enabled;
		private Bridge bridge2 = null;

		//Bridge3
		private bool bridge3Enabled;
		private Bridge bridge3 = null;

		//Encoders
		private Encoders encoders = null;

		//AOC
		private bool aocEnabled;
		private AOC aoc = null;

		//RFazzi_FI28/09/2006_2.Added Office LineConfig section
		//OFFICE
		private bool officeEnabled;
		private OFFICE office = null;

		//RFazzi_FI29/09/2006_1.Moved the Printers Settings into LineConfig section
		//PRINTERS
		private bool printersEnabled;
		private PRINTERS printers = null;

		//WEC
		private WEC wec = null;

		//SPEED
		private bool speedEnabled;
		private SPEED speed = null;

		//SPEEDReference
		private bool speedReferenceEnabled;
		private SPEEDReference speedReference = null;

		//Syncro
		private Syncro syncro = null;

		//Operator
		private Operator operatorConfig = null;

    private bool prcEnabled;
    private PRC prc = null;

		[Category("General"), Description("Enable status")]
		public bool Splicer7
		{
			get { return this.splicer7Enabled; }
			set { this.splicer7Enabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool SingleFacer3
		{
			get { return this.singleFacer3Enabled; }
			set { this.singleFacer3Enabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool Splicer6
		{
			get { return this.splicer6Enabled; }
			set { this.splicer6Enabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool Splicer5
		{
			get { return this.splicer5Enabled; }
			set { this.splicer5Enabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool SingleFacer2
		{
			get { return this.singleFacer2Enabled; }
			set { this.singleFacer2Enabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool Splicer4
		{
			get { return this.splicer4Enabled; }
			set { this.splicer4Enabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool Splicer3
		{
			get { return this.splicer3Enabled; }
			set { this.splicer3Enabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool SingleFacer1
		{
			get { return this.singleFacer1Enabled; }
			set { this.singleFacer1Enabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool Splicer2
		{
			get { return this.splicer2Enabled; }
			set { this.splicer2Enabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool Splicer1
		{
			get { return this.splicer1Enabled; }
			set { this.splicer1Enabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool PreHeatersTern
		{
			get { return this.preHeatersTernEnabled; }
			set { this.preHeatersTernEnabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool GlueUnit
		{
			get { return this.glueUnitEnabled; }
			set { this.glueUnitEnabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool DoubleBacker
		{
			get { return this.doubleBackerEnabled; }
			set { this.doubleBackerEnabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool Rotary
		{
			get { return this.rotaryEnabled; }
			set { this.rotaryEnabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool SlitterScorer
		{
			get { return this.slitterScorerEnabled; }
			set { this.slitterScorerEnabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool WebDirector
		{
			get { return this.webDirectorEnabled; }
			set { this.webDirectorEnabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool Knife
		{
			get { return this.knifeEnabled; }
			set { this.knifeEnabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool Stacker1
		{
			get { return this.stacker1Enabled; }
			set { this.stacker1Enabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool Stacker2
		{
			get { return this.stacker2Enabled; }
			set { this.stacker2Enabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool Stacker3
		{
			get { return this.stacker3Enabled; }
			set { this.stacker3Enabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool Bridge1
		{
			get { return this.bridge1Enabled; }
			set { this.bridge1Enabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool Bridge2
		{
			get { return this.bridge2Enabled; }
			set { this.bridge2Enabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool Bridge3
		{
			get { return this.bridge3Enabled; }
			set { this.bridge3Enabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool Aoc
		{
			get { return this.aocEnabled; }
			set { this.aocEnabled = value; }
		}

		//RFazzi_FI28/09/2006_2.Added Office LineConfig section
		[Category("General"), Description("Enable status")]
		public bool Office
		{
			get { return this.officeEnabled; }
			set { this.officeEnabled = value; }
		}

		//RFazzi_FI29/09/2006_1.Moved the Printers Settings into LineConfig section
		[Category("General"), Description("Enable status")]
		public bool Printers
		{
			get { return this.printersEnabled; }
			set { this.printersEnabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool Speed
		{
			get { return this.speedEnabled; }
			set { this.speedEnabled = value; }
		}

		[Category("General"), Description("Enable status")]
		public bool SpeedReferenceEnabled
		{
			get { return this.speedReferenceEnabled; }
			set { this.speedReferenceEnabled = value; }
		}

    [Category("General"), Description("Enable status")]
    public bool PRC
    {
      get { return this.prcEnabled; }
      set { this.prcEnabled = value; }
    }
    
    public Splicer Splicer7Config
		{
			get { return splicer7; }
			set { splicer7 = value; }
		}

		public SingleFacer SingleFacer3Config
		{
			get { return singleFacer3; }
			set { singleFacer3 = value; }
		}

		public Splicer Splicer6Config
		{
			get { return splicer6; }
			set { splicer6 = value; }
		}

		public Splicer Splicer5Config
		{
			get { return splicer5; }
			set { splicer5 = value; }
		}

		public SingleFacer SingleFacer2Config
		{
			get { return singleFacer2; }
			set { singleFacer2 = value; }
		}

		public Splicer Splicer4Config
		{
			get { return splicer4; }
			set { splicer4 = value; }
		}

		public Splicer Splicer3Config
		{
			get { return splicer3; }
			set { splicer3 = value; }
		}

		public SingleFacer SingleFacer1Config
		{
			get { return singleFacer1; }
			set { singleFacer1 = value; }
		}

		public Splicer Splicer2Config
		{
			get { return splicer2; }
			set { splicer2 = value; }
		}

		public Splicer Splicer1Config
		{
			get { return splicer1; }
			set { splicer1 = value; }
		}

		public PreHeatersTern PreHeatersTernConfig
		{
			get { return preHeatersTern; }
			set { preHeatersTern = value; }
		}

		public GlueUnit GlueUnitConfig
		{
			get { return glueUnit; }
			set { glueUnit = value; }
		}

		public DoubleBacker DoubleBackerConfig
		{
			get { return doubleBacker; }
			set { doubleBacker = value; }
		}

		public Rotary RotaryConfig
		{
			get { return rotary; }
			set { rotary = value; }
		}

		public SlitterScorer SlitterScorerConfig
		{
			get { return slitterScorer; }
			set { slitterScorer = value; }
		}

		public WebDirector WebDirectorConfig
		{
			get { return webDirector; }
			set { webDirector = value; }
		}

		public Knife KnifeConfig
		{
			get { return knife; }
			set { knife = value; }
		}

		public Stacker Stacker1Config
		{
			get { return stacker1; }
			set { stacker1 = value; }
		}

		public Stacker Stacker2Config
		{
			get { return stacker2; }
			set { stacker2 = value; }
		}

		public Stacker Stacker3Config
		{
			get { return stacker3; }
			set { stacker3 = value; }
		}

		public Bridge Bridge1Config
		{
			get { return bridge1; }
			set { bridge1 = value; }
		}

		public Bridge Bridge2Config
		{
			get { return bridge2; }
			set { bridge2 = value; }
		}

		public Bridge Bridge3Config
		{
			get { return bridge3; }
			set { bridge3 = value; }
		}

		public Encoders EncodersConfig
		{
			get { return this.encoders; }
			set { this.encoders = value; }
		}

		public AOC AOCConfig
		{
			get { return this.aoc; }
			set { this.aoc = value; }
		}

		//RFazzi_FI28/09/2006_2.Added Office LineConfig section
		public OFFICE OFFICEConfig
		{
			get { return this.office; }
			set { this.office = value; }
		}

		//RFazzi_FI29/09/2006_1.Moved the Printers Settings into LineConfig section
		public PRINTERS PRINTERSConfig
		{
			get { return this.printers; }
			set { this.printers = value; }
		}

		public WEC WECConfig
		{
			get { return this.wec; }
			set { this.wec = value; }
		}

		public SPEED SPEEDConfig
		{
			get { return this.speed; }
			set { this.speed = value; }
		}

		public SPEEDReference SPEEDReferenceConfig
		{
			get { return this.speedReference; }
			set { this.speedReference = value; }
		}

		public Syncro SyncroConfig
		{
			get { return this.syncro; }
			set { this.syncro = value; }
		}

		public Operator OperatorConfig
		{
			get { return this.operatorConfig; }
			set { this.operatorConfig = value; }
		}

		 public PRC PRCConfig
		 {
			 get { return this.prc; }
			 set { this.prc = value; }
		 }

		 public HeaderPRC PRCHeaderConfig
		 {
			 get { return this.prc.Header; }
			 set { this.prc.Header = value; }
		 }

		public Splicer getSplicerNumber(int number)
		{
			switch (number)
			{
				case 1:
					return this.splicer1;
				case 2:
					return this.splicer2;
				case 3:
					return this.splicer3;
				case 4:
					return this.splicer4;
				case 5:
					return this.splicer5;
				case 6:
					return this.splicer6;
				case 7:
					return this.splicer7;
				default:
					return this.splicer1;
			}
		}

		public SingleFacer getSingleFacerNumber(int number)
		{
			switch (number)
			{
				case 1:
					return this.singleFacer1;
				case 2:
					return this.singleFacer2;
				case 3:
					return this.singleFacer3;
				default:
					return this.singleFacer1;
			}
		}

		public LevelName GetLevelName(KnifeLevel knifeLevel)
		{
			LevelName levelName = LevelName.Nolevel;
			if ((knifeLevel > KnifeLevel.NoLevel) && (knifeLevel <= KnifeLevel.Level3))
			{
				if (knifeLevel == KnifeLevel.Level1)
					levelName = LevelName.Lower;
				else if (knifeLevel == KnifeLevel.Level2)
				{
					if (this.KnifeConfig.LevelCount == DataBase.LineConfig.Knife.MAX_NUM_LEVELS)
						levelName = LevelName.Middle;
					else
						levelName = LevelName.Upper;
				}
				else if (knifeLevel == KnifeLevel.Level3)
					levelName = LevelName.Upper;
			}
			return levelName;
		}

		public KnifeLevel GetKnifeLevel(LevelName levelName)
		{
			KnifeLevel knifeLevel = KnifeLevel.NoLevel;
			if ((levelName > LevelName.Nolevel) && (levelName <= LevelName.Upper))
			{
				if (levelName == LevelName.Lower)
					knifeLevel = KnifeLevel.Level1;
				else if (levelName == LevelName.Middle)
					knifeLevel = KnifeLevel.Level2;
				else if (levelName == LevelName.Upper)
				{
					if (this.KnifeConfig.LevelCount == DataBase.LineConfig.Knife.MAX_NUM_LEVELS)
						knifeLevel = KnifeLevel.Level3;
					else if (this.KnifeConfig.LevelCount == 2)
						knifeLevel = KnifeLevel.Level2;
					else if (this.KnifeConfig.LevelCount == 1)
						knifeLevel = KnifeLevel.Level1;
				}
			} return knifeLevel;
		}

		Object thisLock = null;

		/// <summary>
		/// Builder
		/// </summary>
		/// <param name="DataBaseFolder"></param>
		public LineConfig(string DataBaseFolder)
			: base()
		{
			this.sectionName = "LINE_CONFIG";

			this.thisLock = new Object();

			splicer7 = new Splicer(7);
			singleFacer3 = new SingleFacer(3);
			splicer6 = new Splicer(6);
			splicer5 = new Splicer(5);
			singleFacer2 = new SingleFacer(2);
			splicer4 = new Splicer(4);
			splicer3 = new Splicer(3);
			singleFacer1 = new SingleFacer(1);
			splicer2 = new Splicer(2);
			splicer1 = new Splicer(1);
			preHeatersTern = new PreHeatersTern();
			glueUnit = new GlueUnit();
			doubleBacker = new DoubleBacker();
			rotary = new Rotary();
			slitterScorer = new SlitterScorer();
			webDirector = new WebDirector();
			knife = new Knife();
			stacker1 = new Stacker(0);
			stacker2 = new Stacker(1);
			stacker3 = new Stacker(2);
			bridge1 = new Bridge(1);
			bridge2 = new Bridge(2);
			bridge3 = new Bridge(3);
			encoders = new Encoders();
			aoc = new AOC();
			wec = new WEC();
			speed = new SPEED();
			speedReference = new SPEEDReference();
			syncro = new Syncro();
			operatorConfig = new Operator();
			//RFazzi_FI28/09/2006_2.Added Office LineConfig section
			office = new OFFICE();
			//RFazzi_FI29/09/2006_1.Moved the Printers Settings into LineConfig section
			printers = new PRINTERS();
      prc = new PRC();

      //ABELLO_FI25/11/2010_3 non si caricava la configurazione dal lineconfig.ini
      this.Load();

			//call the refresh data on every instantiated form
			//PageManager.getPageManager().refreshAllFormsData();
		}

		/// <summary>
		/// Set the database version.
		/// Implementation of the IDatabase interface.
		/// </summary>
		/// <param name="date">a Syncro style DateTime string that represent the date when the version was created.</param>
		public void SetVer(string ver)
		{
			//ABello_FI22/01/2007_1 For prevent dead-lock when lineConfig.ini is going to save
			System.Threading.ParameterizedThreadStart threadDelegate = new System.Threading.ParameterizedThreadStart(MySetVer);
			System.Threading.Thread newThread = new System.Threading.Thread(threadDelegate);
			newThread.Start(ver);
		}

		/// <summary>
		/// Set the database version.
		/// Implementation of the IDatabase interface.
		/// </summary>
		/// <param name="date">a Syncro style DateTime string that represent the date when the version was created.</param>
		private void MySetVer(object ver)
		{
			string myVer = (string)ver;
			lock (thisLock)
			{
				if (!myVer.Equals(GetVer()))
				{
					//firstly save the database
					DateTime now = DateTime.Now;

					this.Save();

					TimeSpan intervallo = DateTime.Now - now;
					if (intervallo.TotalSeconds > 4)
						Logger.LogError(this, "**********SETVER LINECONFIG INI seconds: " + (intervallo.TotalSeconds.ToString()));
					//update file date
					DateTime data = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(myVer);
					try
					{
						File.SetLastWriteTimeUtc(this.fullPathIniFile, data);
					}
					catch (FileNotFoundException ex)
					{
						Logger.LogError(this, "SetVer - can't find the file \"" + this.fullPathIniFile + "\".", ex);
						return;
					}
					catch (UnauthorizedAccessException ex)
					{
						Logger.LogError(this, "SetVer - can't access the file \"" + this.fullPathIniFile + "\".", ex);
						return;
					}
					catch (Exception ex)
					{
						Logger.LogError(this, "SetVer - error trying to change the date of the file \"" + this.fullPathIniFile + "\".", ex);
						return;
					}
				}
			}
		}

		/// <summary>
		/// Get the database version as a string.
		/// Implementation of the IDatabase interface.
		/// </summary>
		/// <returns>a Syncro style DateTime string that represent the date when the version was created.
		/// In case of error return the following string "1 1 1 1 1 1 1 1"</returns>
		public string GetVer()
		{
			DateTime data;
			try
			{
				data = File.GetLastWriteTimeUtc(this.fullPathIniFile);
			}
			catch (IOException ex)
			{
				Logger.LogError(this, "GetVer - can't find the file \"" + this.fullPathIniFile + "\".", ex);
				return WRONG_VERSION;
			}
			catch (UnauthorizedAccessException ex)
			{
				Logger.LogError(this, "GetVer - can't access the file \"" + this.fullPathIniFile + "\".", ex);
				return WRONG_VERSION;
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "GetVer - error trying to get the date of the file \"" + this.fullPathIniFile + "\".", ex);
				return WRONG_VERSION;
			}
			string ver = SyncroConverter.ConvertDateTimeToSyncroStyleDateTimeStringNoZerosInFront(data);
			return ver;
		}
	}
}
