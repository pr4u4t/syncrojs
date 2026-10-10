using System;
using System.Drawing.Design;
using System.ComponentModel;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{

	[TypeConverter(typeof(Converters))]
	public class RotaryFormatType : SyncroC.DataBase.LineConfig.LineBase
	{
		public enum FormatsOfRotary
		{
			format1800=1800,
			format2500=2500,
			format2800=2800
		};

        FormatsOfRotary format;														//1800, 2500 or 2800											
		bool ctsPresent;
		int sectorsNum;																		//number of sectors
		int sectorWidth;																	//central sector's width
		int edgeSectorWidth;															//edge sector's width
		int sectorsDist;																	//distance between two consecutive sectors
		int firstCentralSector;

		public RotaryFormatType():base()
		{
			this.sectionName="ROTARY_SHEAR";
			this.Init();
		}

		public RotaryFormatType(string file, int ind):base()
		{
			this.fullPathIniFile=this.fullPathIniFile.Substring(0, this.fullPathIniFile.LastIndexOf("\\")+1)+file;
			this.sectionName="FORMAT_"+(ind+1);
			this.Init();
		}

		private void Init()
		{
			this.format=FormatsOfRotary.format2500;
			this.ctsPresent=false;
			this.sectorsNum=0;
			this.sectorWidth=0;
			this.edgeSectorWidth=0;
			this.sectorsDist=0;
			this.firstCentralSector=0;

		}

    [PropertyOrder(10), Category("Rotary format"), Description("Formato massimo lavorabile sulla Rotary Shear.")]
		public FormatsOfRotary Format
		{
			get{return this.format;}
			set{this.format=value;}
		}

    [PropertyOrder(20), Category("Rotary format"), Description("Presenza dell'optional del tagliacordona 'Continuous Trim'")]
		public bool CtsPresent
		{
			get{return this.ctsPresent;}
			set{this.ctsPresent=value;}
		}

    [PropertyOrder(30), ReadOnly(true), Category("Rotary format"), Description("Numero di lamelle a settori presente sulla macchina (sola scrittura perchè calcolato in funzione del formato macchina)")]
		public int SectorsNum
		{
			get{return this.sectorsNum;}
			set{this.sectorsNum=value;}
		}

    [PropertyOrder(40), ReadOnly(true), Category("Rotary format"), Description("Larghezza della lamella (sola scrittura perchè calcolato in funzione del formato macchina)")]
		public int SectorWidth
		{
			get{return this.sectorWidth;}
			set{this.sectorWidth=value;}
		}

    [PropertyOrder(50), ReadOnly(true), Category("Rotary format"), Description("Larghezza della lamella laterale (sola scrittura perchè calcolato in funzione del formato macchina)")]
		public int EdgeSectorWidth
		{
			get{return this.edgeSectorWidth;}
			set{this.edgeSectorWidth=value;}
		}

    [PropertyOrder(60), ReadOnly(true), Category("Rotary format"), Description("Distanza fra le lamelle (sola scrittura perchè calcolato in funzione del formato macchina)")]
		public int SectorsDist
		{
			get{return this.sectorsDist;}
			set{this.sectorsDist=value;}
		}

    [PropertyOrder(70), ReadOnly(true), Category("Rotary format"), Description("Indice della prima lamella centrale (sola scrittura perchè calcolato in funzione del formato macchina)")]
		public int FirstCentralSector
		{
			get{return this.firstCentralSector;}
			set{this.firstCentralSector=value;}
		}
	}


  [TypeConverter(typeof(Converters))]
  public class RotaryReactionTable : SyncroC.DataBase.LineConfig.LineBase
  {
    int reactionSpeed;																		// Line Speed
    int reactionTime;																	// Reaction Time at Speed

    public RotaryReactionTable():base()
    {
      this.sectionName = "ROTARY_SHEAR";

      this.reactionSpeed = 0;
      this.reactionTime = 0;
    }

    [PropertyOrder(10), Category("Rotary Shear"), Description("Velocità di taglio ( mm/min )")]
    public int ReactionSpeed
    {
      get { return this.reactionSpeed; }
      set { this.reactionSpeed = value; }
    }

    [PropertyOrder(20), Category("Rotary Shear"), Description("Tempo impiegato per eseguire il taglio singolo alla velocità impostata ( msec ). Inserire il valore letto nella pagina ingegneristica dell'AOC.")]
    public int ReactionTime
    {
      get { return this.reactionTime; }
      set { this.reactionTime = value; }
    }
  }

  [TypeConverter(typeof(Converters))]
	public class Rotary : SyncroC.DataBase.LineConfig.LineBase
	{
        public enum ConstantSystemType
        {
          NoConstantSystem = 0,
          StandardConstantSystem = 1,
          ExtendedConstantSystem = 2
        };

        //PMASSARO_FI05/06/08 - 1 - DualShear 2800 MAX SPEED Managment
        public enum AuxiliaryShearModel
        {
            NotFosber = 0,
            RLL = 1,
            RLP = 2,
            RPS = 3,
            DualShear = 4
        };

        public const int MAX_RSH_REACTION_TABLE = 9;
        //PMASSARO_FI05/06/08 - 1 - DualShear 2800 MAX SPEED Managment
        AuxiliaryShearModel auxiliaryShear;   
        ConstantSystemType constantSystem;
	    bool ctsEnabled;															
	    int maxCut;																				//maximum width of central cut
	    int boardMoveMargin;															//safety margin for the movement of the machine
        int minContinuitySectors;                         //minimum sectors that do not cut between central cutting sectors and trim sectors: under the minimum no-gap is not possible
		    int offset;																				//distance between the board center line and the rotary shear center line
        int reactionTimeFixedOffset;											//Reaction Time fixed offset
        int maxSpeedSingleCut;													  //Max RSH single cut speed 
        //PMASSARO_FI05/06/08 - 2 - DualShear 2800 
        int dualShearMaxSpeedSingleCutForBladeToBladeSide_UVmm_m;				//DUAL SHEAR Max Single Cut Speed When Has To Make A Single Cut Using Blade To Blade Side
        int maxSpeedContinuousCut;											  //Max RSH continuous cut speed 
        int scrapLength;																	//Length of a scrap sheet
		    bool scrapRequiresLineRun;												//
		    int scrapIdleTicks;																//
		    bool pushButtonControlEnabled;										//
		    int maximumWeightFactorForNoGap;
        RotaryFormatType formatData;
        RotaryReactionTable[] reactionTable;

    /// <summary>
		/// Builder.
		/// </summary>
		/// <param name="number"></param>
		public Rotary():base()
		{
			this.sectionName="ROTARY_SHEAR";

      //PMASSARO_FI05/06/08 - 1 - DualShear 2800 MAX SPEED Managment
      this.auxiliaryShear = AuxiliaryShearModel.RPS;
			this.constantSystem=ConstantSystemType.NoConstantSystem;
			this.ctsEnabled=false;
			this.maxCut=0;
			this.boardMoveMargin=0;
      this.minContinuitySectors = 0;
			this.offset=0;
      this.reactionTimeFixedOffset = 0;
      this.maxSpeedSingleCut = 0;
      //PMASSARO_FI05/06/08 - 2 - DualShear 2800 MAX SPEED Managment
      this.dualShearMaxSpeedSingleCutForBladeToBladeSide_UVmm_m = 0;				//DUAL SHEAR Max Single Cut Speed When Has To Make A Single Cut Using Blade To Blade Side
      this.maxSpeedContinuousCut=0;
      this.scrapLength = 0;
			this.scrapRequiresLineRun = true;
			this.scrapIdleTicks = 0;
			this.pushButtonControlEnabled = false;
			this.maximumWeightFactorForNoGap = 0;
      this.formatData = new RotaryFormatType();
      this.reactionTable = new RotaryReactionTable[MAX_RSH_REACTION_TABLE];
      for (int i = 0; i < MAX_RSH_REACTION_TABLE; i++)
          this.reactionTable[i] = new RotaryReactionTable();
		}

    //PMASSARO_FI05/06/08 - 1 - DualShear 2800 MAX SPEED Managment
    [PropertyOrder(5), Category("Rotary Shear"), Description("Inserire modello della taglierina ausiliaria")]
    public AuxiliaryShearModel AuxiliaryShear
    {
      get { return this.auxiliaryShear; }
      set { this.auxiliaryShear = value; }
    }

    [PropertyOrder(10), Category("Rotary Shear"), Description("Inserire se la taglierina ausiliaria supporta il taglio NoGap. Impostare 'NoCostantSystem' o 'StandardConstantSystem' a seconda del modello di macchina. 'ExtendedConstantSystem' NON è al momento supportato.")]
    public ConstantSystemType ConstantSystem
    {
			get{return this.constantSystem;}
			set{this.constantSystem=value;}
		}

    [PropertyOrder(20), Category("Rotary Shear"), Description("Abilita l'uso del sistema Continuous Trim sul tagliacordona ( da abilitare solo se l'optional è presente sul tagliacordona ). Warning!! To enable the presence of continuous trim system, use the item \"CtsPresent\" into the \"FormatData\" item.")]
		public bool CtsEnabled
		{
			get{return this.ctsEnabled;}
			set{this.ctsEnabled=value;}
		}

		[PropertyOrder(30), Category("Rotary Shear"), Browsable(false), Description("Maximum width of the cut during no gap order change")]
		public int MaxCut
		{
			get{return this.maxCut;}
			set{this.maxCut=value;}
		}

    //RFAZZI_FI25/10/2010_1. Creato gruppo per parametri RPS NoGap in pagina ingegneristica Rotary Shear. 
    //[PropertyOrder(40), Category("Rotary Shear"), Description("Maximum shift (left or right) of the board during running in mm/10")]
    //public int BoardMoveMargin
    //{
    //  get{return this.boardMoveMargin;}
    //  set{this.boardMoveMargin=value;}
    //}

    //RFAZZI_FI25/10/2010_1. Creato gruppo per parametri RPS NoGap in pagina ingegneristica Rotary Shear. 
    //[PropertyOrder(45), Category("Rotary Shear"), Description("Minimum sectors that do not cut between central cutting sectors and trim sectors: under the minimum, no-gap is not possible")]
    //public int MinContinuitySectors
    //{
    //  get { return this.minContinuitySectors; }
    //  set { this.minContinuitySectors = value; }
    //}

    //RFAZZI_FI25/10/2010_1. Creato gruppo per parametri RPS NoGap in pagina ingegneristica Rotary Shear. 
    //[PropertyOrder(50), Category("Rotary Shear"), Description("Offset to compensate for mechanical disalignment")]
    //public int Offset
    //{
    //  get{return this.offset;}
    //  set{this.offset=value;}
    //}

		[PropertyOrder(60), Category("Rotary Shear"), Browsable(false), Description("Reaction time fixed offset")]
    public int ReactionTimeFixedOffset
    {
      get { return this.reactionTimeFixedOffset; }
      set { this.reactionTimeFixedOffset = value; }
    }

    [PropertyOrder(70), Category("Rotary Shear"), Description("Massima velocità di taglio singolo ( mm/min ). Qualora la macchina sia una DualShear, impostare la velocità massima di taglio singolo per la parte a settori.")]
    public int MaxSpeedSingleCut
    {
      get { return this.maxSpeedSingleCut; }
      set { this.maxSpeedSingleCut = value; }
    }

    //PMASSARO_FI05/06/08 - 1 - DualShear 2800 MAX SPEED Managment
    [PropertyOrder(75), Category("Rotary Shear"), Description("Massima velocità di taglio singolo Lama/Lama della Dual Shear ( mm/min ).")]
    public int DualShearMaxSpeedSingleCutForBladeToBladeSide
    {
      get { return this.dualShearMaxSpeedSingleCutForBladeToBladeSide_UVmm_m; }
      set { this.dualShearMaxSpeedSingleCutForBladeToBladeSide_UVmm_m = value; }
    }

    [PropertyOrder(80), Category("Rotary Shear"), Description("Massima velocità di scarto della macchina ( mm/min ).")]
    public int MaxSpeedContinuousCut
    {
      get { return this.maxSpeedContinuousCut; }
      set { this.maxSpeedContinuousCut = value; }
    }

    [PropertyOrder(85), Category("Rotary Shear"), Description("Lunghezza del foglio scartato ( in mm ). inserire la lunghezza laterale del foglio scartato quando il formato è circa 2500 mm.")]
    public int ScrapLength
    {
      get { return this.scrapLength; }
      set { this.scrapLength = value; }
    }

		[PropertyOrder(86), Category("Rotary Shear"), Browsable(false), Description("TRUE prevents scrap sheet generation unless line run input")]
		public bool ScrapRequiresLineRun
		{
			get { return this.scrapRequiresLineRun; }
			set { this.scrapRequiresLineRun = value; }
		}

		[PropertyOrder(87), Category("Rotary Shear"), Browsable(false), Description("Max ticks between cuts to allow scrap to be counted")]
		public int ScrapIdleTicks
		{
			get { return this.scrapIdleTicks; }
			set { this.scrapIdleTicks = value; }
		}

		[PropertyOrder(88), Category("Rotary Shear"), Browsable(false), Description("TRUE if the continuous cut buttons are push buttons rather than toggle switches")]
		public bool PushButtonControlEnabled
		{
			get { return this.pushButtonControlEnabled; }
			set { this.pushButtonControlEnabled = value; }
		}

    //RFAZZI_FI25/10/2010_1. Creato gruppo per parametri RPS NoGap in pagina ingegneristica Rotary Shear. 
    //[PropertyOrder(89), Category("Rotary Shear"), Description("Maximum weight factor value for disabling no GAP on Dual Shear. 0 means not used")]
    //public int MaximumWeightFactorForNoGap
    //{
    //  get { return this.maximumWeightFactorForNoGap; }
    //  set { this.maximumWeightFactorForNoGap = value; }
    //}

    //RFAZZI_FI25/10/2010_1. Creato gruppo per parametri RPS NoGap in pagina ingegneristica Rotary Shear. 
    [PropertyOrder(40), Category("RPS NoGap"), Description("Massimo spostamento (sinistro o destro) del cartone durante la marcia dei piani ( mm/10 )")]
    public int BoardMoveMargin
    {
      get { return this.boardMoveMargin; }
      set { this.boardMoveMargin = value; }
    }

    //RFAZZI_FI25/10/2010_1. Creato gruppo per parametri RPS NoGap in pagina ingegneristica Rotary Shear. 
    [PropertyOrder(45), Category("RPS NoGap"), Description("Numero minimo di settori fra la fine del taglio centrale e quello di rifilo per disabilitare il Cambio Ordine NoGap. Sotto questo numero, il Cambio NoGap viene disabilitato.")]
    public int MinContinuitySectors
    {
      get { return this.minContinuitySectors; }
      set { this.minContinuitySectors = value; }
    }

    //RFAZZI_FI25/10/2010_1. Creato gruppo per parametri RPS NoGap in pagina ingegneristica Rotary Shear. 
    [PropertyOrder(50), Category("RPS NoGap"), Description("Offset utilizzato nel calcolo delle lamelle da usare nel Cambio Ordine NoGap per compensare disallineamenti fra la Taglierina Ausiliaria e il Tagliacordona.")]
    public int Offset
    {
      get { return this.offset; }
      set { this.offset = value; }
    }

    //RFAZZI_FI25/10/2010_1. Creato gruppo per parametri RPS NoGap in pagina ingegneristica Rotary Shear. 
    [PropertyOrder(89), Category("RPS NoGap"), Description("Grammatura massima del cartone al di sopra della quale il Cambio Ordine NoGap viene disabilitato.")]
    public int MaximumWeightFactorForNoGap
    {
      get { return this.maximumWeightFactorForNoGap; }
      set { this.maximumWeightFactorForNoGap = value; }
    }



    [PropertyOrder(90), Category("Rotary Shear"), Description("Data related to the format")]
    public RotaryFormatType FormatData
    {
      get { return this.formatData; }
      set { this.formatData = value; }
    }

    [PropertyOrder(100), Category("Rotary Shear"), Description("Reaction time for each speed"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
    public RotaryReactionTable[] ReactionTable
    {
      get { return this.reactionTable; }
      set { this.reactionTable = value; }
    }

		public void CheckFormats(object s)
		{
			Rotary rs=(Rotary)s;
//			if ((!this.formatData.Format.Equals(rs.formatData.Format)) ||
//					(this.formatData.CtsPresent!=rs.formatData.CtsPresent))
//			{
				int fIdx;
				RotaryFormat rf=DBManager.getDBManager().rotaryFormat;
				for (fIdx=0; fIdx<RotaryFormat.FORMAT_COUNT; fIdx++)
					if (rf.Formats[fIdx].Format.Equals(rs.formatData.Format) &&
							rf.Formats[fIdx].CtsPresent==rs.formatData.CtsPresent)
						break;
				if (fIdx<RotaryFormat.FORMAT_COUNT)
				{
					rs.formatData.SectorsNum=rf.Formats[fIdx].SectorsNum;
					rs.formatData.SectorWidth=rf.Formats[fIdx].SectorWidth;
					rs.formatData.EdgeSectorWidth=rf.Formats[fIdx].EdgeSectorWidth;
					rs.formatData.SectorsDist=rf.Formats[fIdx].SectorsDist;
					rs.formatData.FirstCentralSector=rf.Formats[fIdx].FirstCentralSector;
				}
//			}
		}
	}
}