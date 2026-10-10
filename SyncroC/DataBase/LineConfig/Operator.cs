using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	[TypeConverter(typeof(Converters))]
	public class Operator : SyncroC.DataBase.LineConfig.LineBase
	{
		private int dryEndRSHCutsRequired;		// Number of cuts at dry-end change

		[PropertyOrder(10), Category("DryEndRSHCutsRequired"), Description("Number of cuts at dry-end change")]
		public int DryEndRSHCutsRequired
		{
			get { return dryEndRSHCutsRequired; }
			set { dryEndRSHCutsRequired = value; }
		}

		private int wetEndRSHCutsRequired;		// Number of cuts at wet-end change

		[PropertyOrder(20), Category("WetEndRSHCutsRequired"), Description("Number of cuts at wet-end change - not used at Flute/Deckle Change")]
		public int WetEndRSHCutsRequired
		{
			get { return wetEndRSHCutsRequired; }
			set { wetEndRSHCutsRequired = value; }
		}

		//RFazzi_FI18/01/2007_2.Added new parameter for make a different scrap at Quality Change when it has Flute or Deckle Change
		private int wetEndFluteChangeRSHCutsRequired;		// Number of cuts at wet-end change

		[PropertyOrder(25), Category("WetEndFluteChangeRSHCutsRequired"), Description("Number of cuts at Flute/Deckle Change")]
		public int WetEndFluteChangeRSHCutsRequired
		{
			get { return wetEndFluteChangeRSHCutsRequired; }
			set { wetEndFluteChangeRSHCutsRequired = value; }
		}

		private int spliceRemovalRSHCutsRequired;		// Number of cuts at Splice

		[PropertyOrder(30), Category("SpliceRemovalRSHCutsRequired"), Description("Number of cuts at Splice")]
		public int SpliceRemovalRSHCutsRequired
		{
			get { return spliceRemovalRSHCutsRequired; }
			set { spliceRemovalRSHCutsRequired = value; }
		}

		private int operatorMaxProductionSpeed; //

		[PropertyOrder(40), Category("OperatorMaxProductionSpeed"), Description("Operator Max Production Speed")]
		public int OperatorMaxProductionSpeed
		{
			get { return operatorMaxProductionSpeed; }
			set { operatorMaxProductionSpeed = value; }
		}

		private int slowSpeed_UVmm_m;									// Speed selected when slow speed push button used

		[PropertyOrder(50), Category("SlowSpeed"), Description("Speed selected when slow speed push button used (mm/m)")]
		public int SlowSpeed_UVmm_m
		{
			get { return slowSpeed_UVmm_m; }
			set { slowSpeed_UVmm_m = value; }
		}

		private int maxAOCSpeed_UVmm_m;							// Max AOC Speed selected	

		[PropertyOrder(60), Category("MaxAOCSpeed_UVmm_m"), Description("Speed selected when MAX AOC speed (mm/m)")]
		public int MaxAOCSpeed_UVmm_m
		{
			get { return maxAOCSpeed_UVmm_m; }
			set { maxAOCSpeed_UVmm_m = value; }
		}

		private int slowAOCSpeed_UVmm_m;						// Slow AOC Speed selected 

		[PropertyOrder(70), Category("SlowAOCSpeed_UVmm_m"), Description("Speed selected when slow AOC speed (mm/m)")]
		public int SlowAOCSpeed_UVmm_m
		{
			get { return slowAOCSpeed_UVmm_m; }
			set { slowAOCSpeed_UVmm_m = value; }
		}

		private bool autoSpeedMode;

		[PropertyOrder(80), Category("AutoSpeedMode"), Description("Auto Speed Mode enabled")]
		public bool AutoSpeedMode
		{
			get { return autoSpeedMode; }
			set { autoSpeedMode = value; }
		}

    //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
		private int typeAutoSpeedToProductionSpeed;

		[PropertyOrder(90), Category("EnableAutoSpeedToProductionSpeed"), Description("Type of Auto Speed (0 = Production, 1 = Target, 2 = Max)")]
		public int TypeAutoSpeedToProductionSpeed
		{
			get { return typeAutoSpeedToProductionSpeed; }
			set { typeAutoSpeedToProductionSpeed = value; }
		}

    //PMASSARO_FI18/10/10 - Aggiunta Gestione Per Limitare La Velocità Di Linea In Base Alla Lunghezza Dell'Ordine
    private int orderMaxProductionSpeed_Length_UDm;
    [PropertyOrder(100), Category("OrderMaxProductionSpeed_Length_UDm"), Description("Order Meters Length To Fix Max Production Speed. 0 = Disable")]
    public int OrderMaxProductionSpeed_Length_UDm
    {
      get { return orderMaxProductionSpeed_Length_UDm; }
      set { orderMaxProductionSpeed_Length_UDm = value; }
    }

    //agrassi_FI23-03-2011_3
    private bool limitateSpeedAccordingToPosTime;
    [PropertyOrder(110), Category("limitateSpeedAccordingToPosTime"), Description("Per limitare la velocità in base al tempo di posizionamento del tagliacordona")]
    public bool LimitateSpeedAccordingToPosTime
    {
      get { return limitateSpeedAccordingToPosTime; }
      set { limitateSpeedAccordingToPosTime = value; }
    }

    //RFAZZI_FI12/09/2011_1. . Aggiunta funzionalità di "Disabilita scarto giunte interne al cartone". Se abilitato, Syncro esegue lo scarto al cambio qualità solo se presente almeno una giunta sulle copertini del cartone altrimenti eseguirà lo scarto quantificato come da pagina operatore. 
    private bool enableNoScrapWhenOnlyInternalSplice;
    [PropertyOrder(120), Category("EnableNoScrapWhenOnlyInternalSplice"), Description("Se abilitato, Syncro esegue lo scarto al cambio qualità solo se presente almeno una giunta sulle copertina del cartone altrimenti eseguirà lo scarto quantificato come da pagina operatore")]
    public bool EnableNoScrapWhenOnlyInternalSplice
    {
      get { return enableNoScrapWhenOnlyInternalSplice; }
      set { enableNoScrapWhenOnlyInternalSplice = value; }
    }

    public Operator()
			: base()
		{
			this.sectionName = "OPERATOR";
			this.DryEndRSHCutsRequired = 1;
			//RFazzi_FI18/01/2007_2.Added new parameter for make a different scrap at Quality Change when it has Flute or Deckle Change
			this.WetEndRSHCutsRequired = 5;
			this.wetEndFluteChangeRSHCutsRequired = 5;
			this.SpliceRemovalRSHCutsRequired = 1;
			this.OperatorMaxProductionSpeed = 200000;
			this.SlowSpeed_UVmm_m = 50000;
			this.AutoSpeedMode = false;
			this.slowAOCSpeed_UVmm_m = 50000;
			this.maxAOCSpeed_UVmm_m = 200000;
			this.typeAutoSpeedToProductionSpeed = 0;
      
      //PMASSARO_FI18/10/10 - Aggiunta Gestione Per Limitare La Velocità Di Linea In Base Alla Lunghezza Dell'Ordine      
      this.orderMaxProductionSpeed_Length_UDm = 0;
      //agrassi_FI23-03-2011_3
      this.limitateSpeedAccordingToPosTime = false;

      //RFAZZI_FI12/09/2011_1. . Aggiunta funzionalità di "Disabilita scarto giunte interne al cartone". Se abilitato, Syncro esegue lo scarto al cambio qualità solo se presente almeno una giunta sulle copertini del cartone altrimenti eseguirà lo scarto quantificato come da pagina operatore. 
      this.enableNoScrapWhenOnlyInternalSplice = false;
    }
	}
}
