using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	/// <summary>
	/// Summary description for KnifeCurveTable.
	/// </summary>
	/// 
	[TypeConverter(typeof(Converters))]
	public class KnifeCurveTable : SyncroC.DataBase.LineConfig.LineBase
	{
		int Index;										// Index 
		int BoardSpeed_UVmm_m;				// Def of speed curve
		int KnifeCutLength_UDdmm;			// Def of cut length curve

		public KnifeCurveTable(int theKnifeLevel): base()
		{

			this.Index = theKnifeLevel + 1;

			this.sectionName = "KNIFE_"+this.Index.ToString();

			this.BoardSpeed_UVmm_m  = 0;
			this.KnifeCutLength_UDdmm = 0;
		}

    [PropertyOrder(10), Category("Knife Curve"), Description("Lunghezza di taglio ( mm/10 )")]
		public int KnifeCutLengthCurve
		{
			get { return this.KnifeCutLength_UDdmm; }
			set { this.KnifeCutLength_UDdmm = value; }
		}

    [PropertyOrder(20), Category("Knife Curve"), Description("Massima velocità di taglio per la lunghezza impostata ( mm/m )")]
		public int BoardSpeedCurve
		{
			get { return this.BoardSpeed_UVmm_m; }
			set { this.BoardSpeed_UVmm_m = value; }
		}
	}

	[TypeConverter(typeof(Converters))]
	public class KnifeLevelSettings : SyncroC.DataBase.LineConfig.LineBase
	{
		public const int SZ_KNIFE_CURVE_TABLE = 20;
		int Index;
		int minCutLength_UDdmm;															// Min Cut length
		int maxCutLength_UDdmm;															// Max Cut length

		//RFAZZI_FI.Changed the above value for level
		bool cutToMark;																			//TRUE if cut to mark is enabled
		bool cutToPattern;																	//TRUE if cut to Pattern is enabled
    //RFAZZI_FI02/06/2011_2. Aggiunta gestione cutToPattern Automatico ( modello con FD con Ricerca automatica della tacca ). 
    bool cutToPatternAutomatic;													//TRUE if cut to Automatic Cut to Pattern is enabled
    //RFAZZI_FI. Added disharge group for level
		bool dischargeGroupPresent;													// TRUE when the discharge group is present

    KnifeCurveTable[] knifeCurveTable;

		public KnifeLevelSettings(int theKnifeLevel): base()
		{
			this.Index = theKnifeLevel;
			this.sectionName = "KNIFE_";

			this.minCutLength_UDdmm = 5000;
			this.maxCutLength_UDdmm  = 50000;
			//RFAZZI_FI.Changed the above value for level
			this.cutToMark = false;
			this.cutToPattern = false;
      //RFAZZI_FI02/06/2011_2. Aggiunta gestione cutToPattern Automatico ( modello con FD con Ricerca automatica della tacca ). 
      this.cutToPatternAutomatic = false;													

			//RFAZZI_FI. Added disharge group for level
			this.dischargeGroupPresent = false;			
			
			this.knifeCurveTable = new KnifeCurveTable[SZ_KNIFE_CURVE_TABLE];
			for (int i = 0; i < SZ_KNIFE_CURVE_TABLE; i++)
				this.knifeCurveTable[i] = new KnifeCurveTable(this.Index);
		}

    [PropertyOrder(10), Category("Knife Setting"), Description("Lunghezza minima di taglio ( mm/10 ). Viene usata per la validazione degli ordini.")]
		public int MinCutLength
		{
			get { return this.minCutLength_UDdmm; }
			set { this.minCutLength_UDdmm = value; }
		}

    [PropertyOrder(20), Category("Knife Setting"), Description("Lunghezza massima di taglio ( mm/10 ). Viene usata per la validazione degli ordini.")]
		public int MaxCutLength
		{
			get { return this.maxCutLength_UDdmm; }
			set { this.maxCutLength_UDdmm = value; }
		}

		//RFAZZI_FI.Changed the above value for level
    [PropertyOrder(30), Category("Knife Setting"), Description("Se TRUE il sistema di Cut to Mark è presente sul livello di taglio.")]
		public bool CutToMark
		{
			get { return cutToMark; }
			set { cutToMark = value; }
		}

    [PropertyOrder(32), Category("Knife Setting"), Description("Se TRUE il sistema di Cut to Pattern ( escluso il sistema automatico ) è presente sul livello di taglio.")]
		public bool CutToPattern
		{
			get { return cutToPattern; }
			set { cutToPattern = value; }
		}

    //RFAZZI_FI02/06/2011_2. Aggiunta gestione cutToPattern Automatico ( modello con FD con Ricerca automatica della tacca ). 
    [PropertyOrder(34), Category("Knife Setting"), Description("Se TRUE il sistema di Cut to Pattern Automatico con spostamento sulla tacca delle fotocellule è presente sul livello di taglio.")]
    public bool CutToPatternAutomatic
    {
      get { return cutToPatternAutomatic; }
      set { cutToPatternAutomatic = value; }
    }

		//RFAZZI_FI. Added disharge group for level
    [PropertyOrder(35), Category("Knife Setting"), Description("Se TRUE il gruppo di scarto è presente sul livello di taglio.")]
		public bool DischargeGroup
		{
			get { return dischargeGroupPresent; }
			set { dischargeGroupPresent = value; }
		}	

		[PropertyOrder(40), Category("Knife Curve"), Description("Knife Curve"),TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
		public KnifeCurveTable[] KnifeCurve
		{
			get {return this.knifeCurveTable;}
			set {this.knifeCurveTable = value;}
		}
	}

  [TypeConverter(typeof(Converters))]
  public class Knife : SyncroC.DataBase.LineConfig.LineBase
	{
		public const int MAX_NUM_LEVELS = 3;

		int levelCount;																				//number of knife levels
		bool modibeltsPresent;																//TRUE if modibelts Present
		int	cutLengthTolerance_UDdmm;													// +/- mm not checked during AOC
		int chopOutLenghtAOC_UDmm;														// Length knife chops out on AOC (mm)
    
    //RFAZZI_FI02/06/2011_1. Aggiunta gestione gruppo di scarto alla Master nei calcoli per il Cambio Ordine NoGap. 
    int chopOutLengthAOCWithScrapGroup_UDmm;										// Length knife chops out on AOC with Scrap Group (mm)
    int chopOutLengthAOCWithScrapGroupAndNextPattern_UDmm;			// Length knife chops out on AOC with Scrap Group when Next is Pattern(mm)
    
    //RFAZZI_FI.Changed the above value for level
		//bool cutToMark;																				//TRUE if cut to mark is enabled
		//bool cutToPattern;																		//TRUE if cut to Pattern is enabled

		KnifeLevelSettings[] knifeLevelSettings;

		public Knife(): base()
		{
			this.sectionName = "KNIFE";
      //ZETA 01
      this.levelCount = 3;
			this.modibeltsPresent = false;
			this.cutLengthTolerance_UDdmm = 10;
			this.chopOutLenghtAOC_UDmm = 1200;
      //RFAZZI_FI02/06/2011_1. Aggiunta gestione gruppo di scarto alla Master nei calcoli per il Cambio Ordine NoGap. 
      this.chopOutLengthAOCWithScrapGroup_UDmm = 750;										// Length knife chops out on AOC with Scrap Group (mm)
      this.chopOutLengthAOCWithScrapGroupAndNextPattern_UDmm = 1100;			// Length knife chops out on AOC with Scrap Group when Next is Pattern(mm)
      //RFAZZI_FI.Changed the above value for level
			//this.cutToMark = false;
			//this.cutToPattern = false;

			this.knifeLevelSettings = new KnifeLevelSettings[MAX_NUM_LEVELS];
			for (int i = 0; i < MAX_NUM_LEVELS; i++)
				this.knifeLevelSettings[i] = new KnifeLevelSettings(i);
		}


    /// <summary>
		/// Builder.
		/// </summary>
		/// <param name="number"></param>
    [PropertyOrder(10), Category("Knife"), Description("Numero di livelli di taglio presenti sulla macchina")]
    public int LevelCount
    {
      get { return this.levelCount; }
      set { this.levelCount = value; }
    }

    [PropertyOrder(20), Category("Knife"), Description("Tolleranza sulla lunghezza di taglio ( mm/10 ). Al momento è usato per salvare sui Log che la lunghezza di taglio è errata.")]
		public int CutLengthTolerance
		{
			get { return this.cutLengthTolerance_UDdmm; }
			set { this.cutLengthTolerance_UDdmm = value; }
		}

    //agrassi_FI19-10-2009_1
    //[PropertyOrder(30), Category("Knife"), Description("Modibelt Present")]
    [Browsable(false), PropertyOrder(30), Category("Knife"), Description("Modibelt Present")]
    public bool ModibeltsPresent
		{
			get { return this.modibeltsPresent; }
			set { this.modibeltsPresent = value; }
		}

    [PropertyOrder(40), Category("Knife"), Description("Lunghezza di taglio a Default utilizzata dalla macchina durante la fase di Cambio Ordine NoGap.")]
		public int ChopOutLengthAOC
		{
			get { return this.chopOutLenghtAOC_UDmm; }
			set { this.chopOutLenghtAOC_UDmm = value; }
		}

    //RFAZZI_FI02/06/2011_1. Aggiunta gestione gruppo di scarto alla Master nei calcoli per il Cambio Ordine NoGap. 
    [PropertyOrder(42), Category("Knife"), Description("Lunghezza di taglio a Default utilizzata dalla macchina durante la fase di Cambio Ordine NoGap quando è presente e rdy il gruppo di scarto.")]
    public int ChopOutLengthAOCWithScrapGroup
    {
      get { return this.chopOutLengthAOCWithScrapGroup_UDmm; }
      set { this.chopOutLengthAOCWithScrapGroup_UDmm = value; }
    }

    [PropertyOrder(42), Category("Knife"), Description("Lunghezza di taglio a Default utilizzata dalla macchina durante la fase di Cambio Ordine NoGap quando è presente e rdy il gruppo di scarto e l'ordine successivo è un Pattern.")]
    public int ChopOutLengthAOCWithScrapGroupAndNextPattern
    {
      get { return this.chopOutLengthAOCWithScrapGroupAndNextPattern_UDmm; }
      set { this.chopOutLengthAOCWithScrapGroupAndNextPattern_UDmm  = value; }
    }

		//RFAZZI_FI.Changed the above value for level
		//[PropertyOrder(45), Category("Knife"), Description("TRUE if cut to mark is enabled")]
		//public bool CutToMark
		//{
		//  get { return cutToMark; }
		//  set { cutToMark = value; }
		//}

		//[PropertyOrder(47), Category("Knife"), Description("TRUE if cut to Pattern is enabled")]
		//public bool CutToPattern
		//{
		//  get { return cutToPattern; }
		//  set { cutToPattern = value; }
		//}

		[PropertyOrder(50), Category("Knife"), Description("Knife Level Settings"),TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
		public KnifeLevelSettings[] KnifeLevelSettings
		{
			get { return this.knifeLevelSettings; }
			set { this.knifeLevelSettings = value; }
		}
		
	}
}