using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	/// <summary>
	/// Summary description for AOC.
	/// </summary>
	/// 

	[TypeConverter(typeof(Converters))]
	public class StackerCancelAOCMask : SyncroC.DataBase.LineConfig.LineBase
	{
		private bool cutLength;										// Cut length = if bit set
		private bool numberOut;										// Number out = if bit set
		private bool numberOutLess;								// Number out <= if bit set
		private bool slitWidth;										// Slit width = if bit set
		private bool knifeLevel;									// Knife = if bit set
		private bool qualityCode;									// Quality code = if bit set
		private bool nextMachine;									// Next machine = if bit set
		private bool panels;											// Panels = if bit set
		private bool orderNumber;									// Order number = if bit set
		private bool customerReference;						// Customer ref = if bit set
		private bool orderIndex;									// Order Index = if bit set
		private bool setupWidth;									// Format Width = if bit set
		private bool scorersType;                 // Scorers Tyoe = if bit set
		private bool scorersPositType;            // Scorers Positioning Type = if bit set
		//RFAZZI_FI09/06/2008
		private bool cutToPatternCode;						// Cut to Pattern Code
    //agrassi_FI14-07-2010_1
    int minSheetWidthForRemovingNoGap_UDdmm;  // The minimum sheet width (in tenths of a mm) in the next order below which the no-gap 
                                              // order change has priority against cancel-aoc (the value 0 disable this functionality)
		public StackerCancelAOCMask()
			: base()
		{

			this.sectionName = "StackerCancelAOCMask";

			this.cutLength = true;
			this.numberOut = true;
			this.numberOutLess = true;
			this.slitWidth = true;
			this.knifeLevel = true;
			this.qualityCode = true;
			this.nextMachine = true;
			this.panels = true;
			this.orderNumber = true;
			this.customerReference = true;
			this.orderIndex = true;
			this.setupWidth = true;
			this.scorersType = true;
			this.scorersPositType = true;
			//RFAZZI_FI09/06/2008
			this.cutToPatternCode = true;
      //agrassi_FI14-07-2010_1
      this.minSheetWidthForRemovingNoGap_UDdmm=0;
    }

		[PropertyOrder(10), Category("StackerCancelAOCMask"), Description("Enable check on Cut Length")]
		public bool EnableCheckOnCutLength
		{
			get { return this.cutLength; }
			set { this.cutLength = value; }
		}

		[PropertyOrder(20), Category("StackerCancelAOCMask"), Description("Viene controllato che tra ordine corrente e successivo la resa sia identica.")]
		public bool EnableCheckOnNumberOut
		{
			get { return this.numberOut; }
			set { this.numberOut = value; }
		}

		[PropertyOrder(30), Category("StackerCancelAOCMask"), Description("Viene controllato che la resa dell’ordine corrente sia superiore o uguale a quella dell’ordine successivo.")]
		public bool EnableCheckOnNumberOutLess
		{
			get { return this.numberOutLess; }
			set { this.numberOutLess = value; }
		}

		[PropertyOrder(40), Category("StackerCancelAOCMask"), Description("Enable Check on Slit Width")]
		public bool EnableCheckOnSlitWidth
		{
			get { return this.slitWidth; }
			set { this.slitWidth = value; }
		}

		[PropertyOrder(50), Category("StackerCancelAOCMask"), Description("Enable Check on Knife Level")]
		public bool EnableCheckOnKnifeLevel
		{
			get { return this.knifeLevel; }
			set { this.knifeLevel = value; }
		}

		[PropertyOrder(60), Category("StackerCancelAOCMask"), Description("Enable Check on Quality Code")]
		public bool EnableCheckOnQualityCode
		{
			get { return this.qualityCode; }
			set { this.qualityCode = value; }
		}

		[PropertyOrder(70), Category("StackerCancelAOCMask"), Description("Enable Check on Next Machine")]
		public bool EnableCheckOnNextMachine
		{
			get { return this.nextMachine; }
			set { this.nextMachine = value; }
		}

		[PropertyOrder(80), Category("StackerCancelAOCMask"), Description("Vengono controllate che le distanze di cordonatura tra ordine corrente e successivo siano identiche.")]
		public bool EnableCheckOnPanelDimension
		{
			get { return this.panels; }
			set { this.panels = value; }
		}

		[PropertyOrder(90), Category("StackerCancelAOCMask"), Description("Viene controllato che l’identificativo dell’ordine tra ordine corrente e successivo sia identico.")]
		public bool EnableCheckOnOrderNumber
		{
			get { return this.orderNumber; }
			set { this.orderNumber = value; }
		}

		[PropertyOrder(100), Category("StackerCancelAOCMask"), Description("Viene controllato che il cliente tra ordine corrente e successivo sia identico")]
		public bool EnableCheckOnCustomerReference
		{
			get { return this.customerReference; }
			set { this.customerReference = value; }
		}

		[PropertyOrder(110), Category("StackerCancelAOCMask"), Description("Per ogni livello presente nell’ordine corrente e successivo si controlla che la posizione delle relative scatole possano permettere il sovrapposizionamento. Ad esempio Syncro non abiliterebbe l’annulla AOC nel seguente caso: Il livello superiore è presente sia nell’ordine corrente che nel successivo, ma le scatole dell’ordine corrente sono alla sinistra rispetto al senso cartone, mentre per l’ordine successivo sono a destra (è chiaro che la sovrapposizione dei fogli non sarebbe fattibile).")]
		public bool EnableCheckOnOrderIndex
		{
			get { return this.orderIndex; }
			set { this.orderIndex = value; }
		}

		[PropertyOrder(120), Category("StackerCancelAOCMask"), Description("Enable Check on Setup Width")]
		public bool EnableCheckOnSetupWidth
		{
			get { return this.setupWidth; }
			set { this.setupWidth = value; }
		}

		[PropertyOrder(130), Category("StackerCancelAOCMask"), Description("Viene controllato che il tipo di cordone utilizzato sia lo stesso tra ordine corrente e successivo.")]
		public bool EnableCheckOnScorersType
		{
			get { return this.scorersType; }
			set { this.scorersType = value; }
		}

		[PropertyOrder(140), Category("StackerCancelAOCMask"), Description("Viene controllato che il tipo di posizionamento utilizzato sia lo stesso tra ordine corrente e successivo.")]
		public bool EnableCheckOnScorersPositType
		{
			get { return this.scorersPositType; }
			set { this.scorersPositType = value; }
		}

		//RFAZZI_FI09/06/2008
		[PropertyOrder(150), Category("StackerCancelAOCMask"), Description("Viene controllato che il codice pattern associato all’ordine corrente e successivo sia identico.")]
		public bool EnableCheckOnCutToPatternCode
		{
			get { return this.cutToPatternCode; }
			set { this.cutToPatternCode = value; }
		}

    //agrassi_FI14-07-2010_1
    [PropertyOrder(160), Category("StackerCancelAOCMask"), Description("The minimum sheet width (in tenths of a mm) in the next order below which the no-gap order change has priority against cancel-aoc (the value 0 disable this functionality)")]
    public int MinSheetWidthForRemovingNoGap_UDdmm
    {
      get { return this.minSheetWidthForRemovingNoGap_UDdmm; }
      set { this.minSheetWidthForRemovingNoGap_UDdmm = value; }
    }
  };

	[TypeConverter(typeof(Converters))]
	public class AOC : SyncroC.DataBase.LineConfig.LineBase
	{
		public const int MAX_NUM_LEVELS = 3;

		int AdjustRSHNoGapAOC_UDmm;								// Offset for RSH on gapless change
		int StartSyncAnticipValue_UDmm;						// Start Sync anticipation point
		int DelayBetweenAOC_UTs;									// Configures number of seconds minimum delay before another AOC can be made
		int NoGapValidOnDeckleChange_UDmm;				// UD of deckle change below which a NoGap Change can be
		int SandwichOverSpeed_UPerc;							// Sandwich OverSpeed in %
		int EarlyWarning_UDm;											// Early Warning Distance
		int EarlyWarning_UTs;											// Early Warning Time
		int PrepareAOC_UDm;												// Prepare AOC Distance
		int PrepareAOC_UTs;												// Prepare AOC Time
		int[] DistShearToKnife_UDmm;							// Distance RSH to Knife ( for level )
		int DistRSHtoEndAccPoint_UDmm;						// Distance RSH to End Acceleration Point
		int DistRSHtoMWPoint_UDmm;								// Distance RSH to MW Point
		int distShearToSecondShear_UDmm;				// Distance between RSH's ( if present )

    //agrassi_FI01-07-08
		//int NumChopAOC;															// Scrapped sheets during AOC
		int MinNoGapAOCSpeed_UVmm_m;							// Minimum speed for no-gap order change
		int distShearToWebAtKnife_UDmm;
		int TailOutDecelation_URmm_m;					  	 // Deceleration ramp after tail out
		int MaxTailAcceleration_URmm_m;				  	// Maximum tail acceleration ramp during gap AOC

		//RFazzi_FI28/05/2007_1.Added parameter for Tandem AOC Speed
		int TandemAOCSpeed_UV_mm_m;		//Tandem AOC
    int MaxAOCSpeedSS_UV_mm_m;    //max AOC speed for the slitter scorer

    //RFAZZI_FI18/04/2011_1. Aggiunto parametro per la misura a cui eseguire il taglio RSH sui cambi ordini Gap rispetto al taglio del livello dominante. 
    int DistRSHCutFromKnifeSyncCutGapAOC_mm;        //Distance in mm between Sync Knife cut and RSH Cut in GapAOC

		double AOCSSGapRequired_UTs;							// Gap Required at SS
		double AOCSSAuxGapRequired_UTs;						// Gap Required at SS with Auxiliary Shaft

		bool EnableTandem;												// Enables the tandem AOC system 
    //agrassi_FI31-10-08
    //bool EnableTandemForBackToBackAux;				// Enables the tandem AOC system when changing between attempted
		bool EnableKnifeOnwardsChange;						// Enable the selection of the units that have to change
		bool EnableStackerOnwardsChange;					// Enable the selection of the units that have to change
    //agrassi_FI30_04_08
    bool EnableStackerCancelAOC;              // Enable the cancel AOC on the stacker
    //MLAMIONI_FI02/05/2011 - Aggiunta variabile ingegneristica per attivare/disattivare cambio NoGap durante Stacker Cancel AOC
    bool EnableNoGapOnStackerCancelAOC;       // Enable the NoGap AOC on Stacker Cancel AOC
    bool EnableNoGapOnQualityChange;					// Enables NoGap AOC if paper change
		bool EnableNoGapValidOnFluteChange;				// Enables NoGap AOC if flute changes
		bool EnableGaplessKnifeScrap;							// Enable scrap cutting at knife on a gapless AOC
		bool NoGapAOCPossible;										// Indicates if a gapless system is present - not water jets
		bool RequireSSConfirmReady;								// TRUE if the SS has to confirm that it is ready after
		// AOC Init event has been sent
		bool LastCutSignalPresent;								// TRUE when the last cut signal from the knife is present
		bool EnableExternalAOCPresent;							// TRUE when a external AOC System is Present
		bool DisableAOCModeSEMIonWetEndChange;		// TRUE when disable change AOC Mode to SEMI on WetEnd Change
		bool ForceAOCModeAutoAfterAOC;		        // TRUE when force the AOC Mode to Auto afetr a AOC

		StackerCancelAOCMask stackerCancelAOCMask;
		//Fanfold
		int fanFoldLevel;
		int cutsAddedAtStartOfOrder;
		int startAOCSpeedWithFanFold_UVm_m;
		int finalAOCSpeedWithFanFold_UVm_m;
		int finalAOCSpeedWithOnlyFanFold_UVm_m;
		int finalWasteSpeedWithFanFold_UVm_m;
    //PMASSARO_FI24/02/10 - Aggiunta gestione accellerazione a cambio con FANFOLD
    int maxRampAccOnFANFOLD_URmm_m;
    //RFAZZI_FI15/07/2010_1. Aggiunta gestione per conteggio fogli con ordini FanFold ad inizio ordine usando i metri fra Master e FanFold. 
    int distanceBetweenMasterFanFold_UDmm;

		public AOC()
			: base()
		{
			this.sectionName = "AOC";
			this.AdjustRSHNoGapAOC_UDmm = 600;
			this.StartSyncAnticipValue_UDmm = 7000;
			this.DelayBetweenAOC_UTs = 50;
			this.NoGapValidOnDeckleChange_UDmm = 300;
			this.SandwichOverSpeed_UPerc = 5;
			this.EarlyWarning_UDm = 200;
			this.EarlyWarning_UTs = 0;
			this.PrepareAOC_UDm = 50;
			this.PrepareAOC_UTs = 0;
			this.DistShearToKnife_UDmm = new int[MAX_NUM_LEVELS];
			this.DistRSHtoEndAccPoint_UDmm = 5000;
			this.DistRSHtoMWPoint_UDmm = 3000;
			//this.ShearToAutoSpeedDistance_UDmm = 0;
			this.DistShearToWebAtKnife_UDmm = 0;
			this.distShearToSecondShear_UDmm = 0;

      //agrassi_FI01-07-08
      //this.NumChopAOC = 0;
			this.MinNoGapAOCSpeed_UVmm_m = 60000;
			this.TailOutDecelation_URmm_m = 3000;
			this.MaxTailAcceleration_URmm_m = 18000;

			//RFazzi_FI28/05/2007_1.Added parameter for Tandem AOC Speed
			this.TandemAOCSpeed_UV_mm_m = 20000;

      //RFAZZI_FI18/04/2011_1. Aggiunto parametro per la misura a cui eseguire il taglio RSH sui cambi ordini Gap rispetto al taglio del livello dominante. 
      DistRSHCutFromKnifeSyncCutGapAOC_mm = 600;

      //AGrassi_FI01/06/2007_1
      this.MaxAOCSpeedSS_UV_mm_m = 250000;

			this.AOCSSGapRequired_UTs = 0.1;
			this.AOCSSAuxGapRequired_UTs = 0.2;

			this.EnableTandem = true;
      //agrassi_FI31-10-08
      //this.EnableTandemForBackToBackAux = true;
			this.EnableKnifeOnwardsChange = true;
			this.EnableStackerOnwardsChange = false;
      //agrassi_FI30_04_08
      this.EnableStackerCancelAOC = true;
      //MLAMIONI_FI02/05/2011 - Aggiunta variabile ingegneristica per attivare/disattivare cambio NoGap durante Stacker Cancel AOC
      this.EnableNoGapOnStackerCancelAOC = false;
      this.EnableNoGapOnQualityChange = true;
			this.EnableNoGapValidOnFluteChange = true;
			this.EnableGaplessKnifeScrap = true;
			this.NoGapAOCPossible = true;
			this.RequireSSConfirmReady = true;

			this.LastCutSignalPresent = true;
			this.EnableExternalAOCPresent = false;
			this.DisableAOCModeSEMIonWetEndChange = false;
			this.ForceAOCModeAutoAfterAOC = false;

			this.stackerCancelAOCMask = new StackerCancelAOCMask();
			this.fanFoldLevel = 1;
			this.cutsAddedAtStartOfOrder = 3;
			this.startAOCSpeedWithFanFold_UVm_m = 70;
			this.finalAOCSpeedWithFanFold_UVm_m = 30;
			this.finalAOCSpeedWithOnlyFanFold_UVm_m = 30;
			this.finalWasteSpeedWithFanFold_UVm_m = 30;
      //PMASSARO_FI24/02/10 - Aggiunta gestione accellerazione a cambio con FANFOLD
      this.finalWasteSpeedWithFanFold_UVm_m = 0;
      //RFAZZI_FI15/07/2010_1. Aggiunta gestione per conteggio fogli con ordini FanFold ad inizio ordine usando i metri fra Master e FanFold. 
      this.distanceBetweenMasterFanFold_UDmm = 0;

		}

		// *************************************  AOC TYPES ENABLES  ************************************
    [Category("1. AOC TYPES ENABLES"), PropertyOrder(10), Description("Se 'TRUE' abilita la gestione di cambio ordine NoGap. Questo deve essere abilitato solo in presenza di Rotary Shear NoGap e/o Twin con rifili continui.")]
		public bool AOCNoGapEnable
		{
			get { return this.NoGapAOCPossible; }
			set { this.NoGapAOCPossible = value; }
		}

    [Category("1. AOC TYPES ENABLES"), PropertyOrder(20), Description("Se 'TRUE' abilita la gestione di cambio ordine Master + Terminal. Questo deve essere abilitato solo se la Master permette di inviare i comandi di 'Cambio Immediato'.")]
		public bool AOCKnifeOnwardsEnable
		{
			get { return this.EnableKnifeOnwardsChange; }
			set { this.EnableKnifeOnwardsChange = value; }
		}

    //agrassi_FI30_04_08
    [Category("1. AOC TYPES ENABLES"), PropertyOrder(25), Description("Se 'TRUE' abilita la gestione del bit di “Cancel AOC” allo Stacker. Una volta abilitato questo parametro, vengono controllati gli ordini in funzione delle abilitazioni riportate nella sezione “SetStackerCancelAOCMask”. ATTENZIONE!!! Questa opzione deve essere disabilitata nel caso di stampa cartellini con ingressi digitali.")]
		public bool StackerCancelAOCEnable
		{
			get { return this.EnableStackerCancelAOC; }
			set { this.EnableStackerCancelAOC = value; }
		}
    
    //MLAMIONI_FI02/05/2011 - Aggiunta variabile ingegneristica per attivare/disattivare cambio NoGap durante Stacker Cancel AOC
    [Category("1. AOC TYPES ENABLES"), PropertyOrder(27), Description("Se 'TRUE' abilita il cambio NoGap durante un anulla cambio ordine allo stacker con resa >1. ATTENZIONE!!!Questa abilitazione potrebbe causare problemi di smistamento pile in zona scarico stacker")]
    public bool NoGapOnStackerCancelAOC
    {
      get { return this.EnableNoGapOnStackerCancelAOC; }
      set { this.EnableNoGapOnStackerCancelAOC = value; }
    }

		[Category("1. AOC TYPES ENABLES"), Browsable(false), PropertyOrder(30), Description("Simile al AOCKnifeOnwardsEnable, ma lavora quando sia la taglierina che il taglia Cordona non richiede un cambio ordine.")]
		public bool AOCStackerOnwardsEnable
		{
			get { return this.EnableStackerOnwardsChange; }
			set { this.EnableStackerOnwardsChange = value; }
		}

    [Category("1. AOC TYPES ENABLES"), PropertyOrder(40), Description("Se 'TRUE' abilita la gestione di Cambio Ordine Tandem. Per abilitare questa modalità di Cambio Ordine, deve essere presente un tagliacordona che lo supporti.")]
		public bool AOCTandemEnable
		{
			get { return this.EnableTandem; }
			set { this.EnableTandem = value; }
		}

    //agrassi_FI31-10-08
    //[Category("1. AOC TYPES ENABLES"), PropertyOrder(50), Description("Enables the tandem AOC system when the current order and the next both require the use of the auxiliary shaft")]
    //public bool AOCTandemEnableBackToBackAux
    //{
    //  get { return this.EnableTandemForBackToBackAux; }
    //  set { this.EnableTandemForBackToBackAux = value; }
    //}



		// *************************************  DISTANCES  ************************************
    [Category("2. DISTANCES"), PropertyOrder(10), Description("Corrisponde alla distanza (in mm) tra il punto di taglio delle lame della RSH al momento in cui viene settata l’uscita di 'taglio effettuato' (generalmente al momento in cui le lame toccano il cartone) ed il punto di lettura della fotocellula di presenza cartone sulla ruota di misura (fotocellula di Syncro). In caso di Dual Shear, la distanza va presa dalla lama/poliuretano.")]
		public int DistRSHToMWPoint
		{
			get { return this.DistRSHtoMWPoint_UDmm; }
			set { this.DistRSHtoMWPoint_UDmm = value; }
		}

    [Category("2. DISTANCES"), PropertyOrder(20), Description("Considerando che si sta parlando di Master Fosber 21.30 o superiore, corrisponde alla distanza (in mm) tra il punto di taglio delle lame della RSH al momento in cui viene settata l’uscita di 'taglio effettuato' (generalmente al momento in cui le lame toccano il cartone) ed il punto in cui si incrociano le lame della master (livello superiore). In caso di Dual Shear, la distanza va presa dalla lama/poliuretano.")]
		public int DistShearToWebAtKnife_UDmm
		{
			get { return this.distShearToWebAtKnife_UDmm; }
			set { this.distShearToWebAtKnife_UDmm = value; }
		}


    [Category("2. DISTANCES"), PropertyOrder(30), Description("Queste distanze (in mm) sono divise per livello (livello inferiore = indice 0). Ammesso che non ci sia una differenza eccessiva tra i livelli (differenza di +- 10 cm) Il dato può essere lo stesso e può inoltre corrispondere a quello inserito nel parametro 'DistShearToWebAtKnife', infatti questo parametro corrisponde alla distanza tra il punto di taglio delle lame della RSH al momento in cui viene settata l’uscita di 'taglio effettuato' (generalmente al momento in cui le lame toccano il cartone) ed il punto in cui si incrociano le lame della master (livello superiore). In caso di Dual Shear, la distanza va presa dalla lama/poliuretano."), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
		public int[] DistShearToKnife
		{
			get { return this.DistShearToKnife_UDmm; }
			set { this.DistShearToKnife_UDmm = value; }
		}

    [Category("2. DISTANCES"), PropertyOrder(40), Description("Distanza (in mm) fra le lame della RSH quando è presente una Dual Shear.")]
		public int DistBetweenRSH_UDmm
		{
			get { return this.distShearToSecondShear_UDmm; }
			set { this.distShearToSecondShear_UDmm = value; }
		}

    [Category("2. DISTANCES"), PropertyOrder(100), ReadOnly(true), Description("Distanza ( in mm ) in cui il sistema entra in zona Syncro e richiesto per sincronizzare il taglio della Taglierina Ausiliaria con quella della Taglierina Trasversale ( livello dominante ). Di Solito è 7000.")]
		public int StartSyncAnticipValue
		{
			get { return this.StartSyncAnticipValue_UDmm; }
			set { this.StartSyncAnticipValue_UDmm = value; }
		}


		// *************************************  GAP AOC PARAMETERS  ************************************

    [Category("3. GAP AOC"), PropertyOrder(10), Description("Gap (in sec) richiesto fra il vecchio e il nuovo ordine al tagliacordona per eseguire il cambio ordine. Di Solito per Twin400 si imposta 0,1, per TwinE si imposta 0,2 per Compact si imposta 1.")]
		public double SSGapRequired
		{
			get { return this.AOCSSGapRequired_UTs; }
			set { this.AOCSSGapRequired_UTs = value; }
		}

    [Category("3. GAP AOC"), PropertyOrder(20), Description("Gap (in sec) richiesto di buco al tagliacordona quando la sequenza di Cambio Ordine include l'Asse Ausiliario. Di Solito per Twin, deve essere impostato il solito valore del parametro precedente, nel caso di Compact, deve essere verificato in funzione della velocità di ingresso/uscita da lavoro del gruppo.")]
		public double SSAuxiliaryGapRequired
		{
			get { return this.AOCSSAuxGapRequired_UTs; }
			set { this.AOCSSAuxGapRequired_UTs = value; }
		}

    [Category("3. GAP AOC"), PropertyOrder(30), Description("Accelerazione (in mm/min*sec) massima applicabile alla coda dell'ordine in fase di Cambio Ordine. Normalmente il limite è dato dalla Taglierina Trasversale.")]
		public int MaxTailAcceleration
		{
			get { return this.MaxTailAcceleration_URmm_m; }
			set { this.MaxTailAcceleration_URmm_m = value; }
		}

		[Category("3. GAP AOC"), PropertyOrder(35), Description("Distance (in mm) that the system has to make the required gap in. In case of Dual Shear, the distance has to be taken from the blade/polyurethane shear.")]
		public int DistRSHToEndAccPoint
		{
			get { return this.DistRSHtoEndAccPoint_UDmm; }
			set { this.DistRSHtoEndAccPoint_UDmm = value; }
		}

    	[Category("3. GAP AOC"), PropertyOrder(40), Description("Decelerazione (in mm/min*sec) applicata al riferimento DryEnd appena terminata la sequenza di Cambio Ordine per riportare la velocità delle macchine del DryEnd uguale a quella dei Piani Caldi in modo graduale. Questo parametro DEVE come essere impostato >= 1000")]
		public int DeceleratTailOut
		{
			get { return this.TailOutDecelation_URmm_m; }
			set { this.TailOutDecelation_URmm_m = value; }
		}

    [Category("3. GAP AOC"), PropertyOrder(50), Description("Sovravelocità (in %) dei Sandwich della Taglierina Trasversale rispetto al cartone. Viene usata per calcolare la massima velocità di Cambio Ordine per non rischiare di portare la macchina ad una velocità superiore alla sua curva di taglio.")]
		public int SandwichOverSpeed
		{
			get { return this.SandwichOverSpeed_UPerc; }
			set { this.SandwichOverSpeed_UPerc = value; }
		}

    //agrassi_FI01-07-08
    //[Category("3. GAP AOC"), PropertyOrder(60), Description("Number of Chop in AOC")]
    //public int NumChop
    //{
    //  get { return this.NumChopAOC; }
    //  set { this.NumChopAOC = value; }
    //}

		//RFazzi_FI28/05/2007_1.Added parameter for Tandem AOC Speed
    [Category("3. GAP AOC"), PropertyOrder(70), Description("Velocità massima (in mm/min) a cui viene eseguita la sequenza di Cambio Ordine Tandem. Durante questa sequenza, l'accelerazione applicata alla coda dell'ordine è pari alla 'MaxTailAcceleration' per velocizzare al massimo il completamento dell'intera procedura di Cambio Ordine.")]
		public int TandemAOCSpeed
		{
			get { return this.TandemAOCSpeed_UV_mm_m; }
			set { this.TandemAOCSpeed_UV_mm_m = value; }
		}

    //RFAZZI_FI18/04/2011_1. Aggiunto parametro per la misura a cui eseguire il taglio RSH sui cambi ordini Gap rispetto al taglio del livello dominante. 
    [Category("3. GAP AOC"), PropertyOrder(80), Description("Distanza in mm a cui viene fatto il taglio della RSH in Cambio Ordine Gap rispetto al taglio della Master del Livello Dominante")]
    public int DistRSHCutFromKnifeSyncCutGapAOC
    {
      get { return this.DistRSHCutFromKnifeSyncCutGapAOC_mm; }
      set { this.DistRSHCutFromKnifeSyncCutGapAOC_mm = value; }
    }
    
    // *************************************  NO GAP AOC PARAMETERS  ************************************

    [Category("4. NO GAP AOC"), PropertyOrder(20), Description("Parametro (in mm) utilizzato per centrare il taglio del NoGap rispetto al taglio di default della Master. Per valori negativi, il taglio della Taglierina Ausiliaria viene anticipato.")]
		public int AdjustRSHNoGap
		{
			get { return this.AdjustRSHNoGapAOC_UDmm; }
			set { this.AdjustRSHNoGapAOC_UDmm = value; }
		}

    [Category("4. NO GAP AOC"), PropertyOrder(30), Description("Velocità (in mm/min) minima per permettere l'esecuzione di un Cambio Ordine NoGap (Di Solito è 60 mt/min)")]
		public int MinNoGapAOCSpeed
		{
			get { return this.MinNoGapAOCSpeed_UVmm_m; }
			set { this.MinNoGapAOCSpeed_UVmm_m = value; }
		}

    [Category("4. NO GAP AOC"), PropertyOrder(40), Description("Abilitazione ad eseguire Cambi Ordine NoGap quando il cambio larghezza della carta è inferiore al valore impostato (in mm).")]
		public int NoGapValidOnDeckleChange
		{
			get { return this.NoGapValidOnDeckleChange_UDmm; }
			set { this.NoGapValidOnDeckleChange_UDmm = value; }
		}

    [Category("4. NO GAP AOC"), PropertyOrder(50), Description("Abilitazione ad eseguire Cambi Ordine NoGap quando c'è un cambio qualità.")]
		public bool NoGapAOConQualityChangeEnable
		{
			get { return this.EnableNoGapOnQualityChange; }
			set { this.EnableNoGapOnQualityChange = value; }
		}

    [Category("4. NO GAP AOC"), PropertyOrder(60), Description("Abilitazione ad eseguire Cambi Ordine NoGap quando c'è un cambio onda.")]
		public bool NoGapAOConFluteChangeEnable
		{
			get { return this.EnableNoGapValidOnFluteChange; }
			set { this.EnableNoGapValidOnFluteChange = value; }
		}

    [Category("4. NO GAP AOC"), PropertyOrder(70), Description("Abilitazione ad utilizzare il segnale di Cambio a Misura di Default durante la sequenza di Cambio Ordine NoGap alla Taglierina Trasversale.")]
		public bool KnifeDefaultCutSignalEnable
		{
			get { return this.EnableGaplessKnifeScrap; }
			set { this.EnableGaplessKnifeScrap = value; }
		}


		// *************************************  AOC GENERAL PARAMETERS  ************************************
    [Category("5. GENERAL PARAMETERS"), PropertyOrder(10), Description("Distanza (in mt) mancante a cambio ordine al di sotto del quale Syncro passa in preallarme.")]
		public int EarlyWarning_m
		{
			get { return this.EarlyWarning_UDm; }
			set { this.EarlyWarning_UDm = value; }
		}

    [Category("5. GENERAL PARAMETERS"), PropertyOrder(20), ReadOnly(false), Description("Tempo (in Sec) mancante a cambio ordine al di sotto del quale Syncro passa in preallarme. Qualora sia il Preallarme a Metri che a Tempo siano abilitati, Syncro entrerà in preallarme quando la prima delle due condizioni sarà verificata.")]
    public int EarlyWarning_s
		{
			get { return this.EarlyWarning_UTs; }
			set { this.EarlyWarning_UTs = value; }
		}

    [Category("5. GENERAL PARAMETERS"), PropertyOrder(30), Description("Metri mancanti a cambio ordine per inviare il segnale di 'Prepare AOC' alle macchine.")]
		public int PrepareAOC_m
		{
			get { return this.PrepareAOC_UDm; }
			set { this.PrepareAOC_UDm = value; }
		}

    [Category("5. GENERAL PARAMETERS"), PropertyOrder(40), ReadOnly(false), Description("Tempo (in Sec) mancante a cambio ordine per inviare il segnale di 'Prepare AOC' alle macchine . Qualora sia utilizzato sia il Prepare AOC a Metri che a Tempo, Syncro invierà il segnale quando la prima delle due condizioni sarà verificata.")]
    public int PrepareAOC_s
		{
			get { return this.PrepareAOC_UTs; }
			set { this.PrepareAOC_UTs = value; }
		}

    [Category("5. GENERAL PARAMETERS"), PropertyOrder(50), Description("Tempo (in Sec) Minimo fra due sequenze di Cambio Ordine consecutive per evitare l'avvio indesiderato.")]
		public int DelayBetweenAOC
		{
			get { return this.DelayBetweenAOC_UTs; }
			set { this.DelayBetweenAOC_UTs = value; }
		}

    [Category("5. GENERAL PARAMETERS"), PropertyOrder(60), Description("se 'TRUE' il segnale è presente nella interfaccia con la Taglierina Trasversale e il sistema attende questo per completare la sequenza di Cambio Ordine.")]
		public bool KnifeLastCutSignalPresent
		{
			get { return this.LastCutSignalPresent; }
			set { this.LastCutSignalPresent = value; }
		}

    [Category("5. GENERAL PARAMETERS"), PropertyOrder(70), Description("se 'TRUE' il sistema attende il segnale per avviare la sequenza di Cambio Ordine.")]
		public bool SSReadyAOCPresent
		{
			get { return this.RequireSSConfirmReady; }
			set { this.RequireSSConfirmReady = value; }
		}
    
    //AGrassi_FI01/06/2007_1
    [Category("5. GENERAL PARAMETERS"), PropertyOrder(75), Description("Massima velocità (in mm/min) di Cambio Ordine gestita dal Tagliacordona.")]
    public int MaxAOCSpeedSS
    {
      get { return this.MaxAOCSpeedSS_UV_mm_m; }
      set { this.MaxAOCSpeedSS_UV_mm_m = value; }
    }

		[Category("5. GENERAL PARAMETERS"), PropertyOrder(80), Browsable(false), Description("Mettere a TRUE se c'è un sistema esterno di Cambio Ordine.")]
		public bool ExternalAOCSystemPresent
		{
			get { return this.EnableExternalAOCPresent; }
			set { this.EnableExternalAOCPresent = value; }
		}


		[Category("5. GENERAL PARAMETERS"), PropertyOrder(90), Description("Questo parametro solitamente viene concordato con il cliente in base alle proprie esigenze. Se impostato a TRUE, indica che il tipo cambio verrà impostato a 'SEMI' in corrispondenza di cambi qualità o cambio altezza. ")]
		public bool AOCModeSEMIonWetEndChange
		{
			get { return this.DisableAOCModeSEMIonWetEndChange; }
			set { this.DisableAOCModeSEMIonWetEndChange = value; }
		}

		[Category("5. GENERAL PARAMETERS"), PropertyOrder(95), Description("Questo parametro solitamente viene concordato con il cliente in base alle proprie esigenze. Se impostato a TRUE indica che il tipo cambio verrà impostato in 'AUTO' in corrispondenza di cambi ordini Dry - End (ossia qualora il prossimo cambio non corrisponda a cambio qualità o cambio altezza).")]
		public bool ForceAOCModeAUTOafterAOC
		{
			get { return this.ForceAOCModeAutoAfterAOC; }
			set { this.ForceAOCModeAutoAfterAOC = value; }
		}

		[Category("5. GENERAL PARAMETERS"), PropertyOrder(100), Description("Gestione dell’ annulla cambio ordine per ogni singolo livello dell’impilatore. Deve essere concordata con il cliente poiché i valori abilitati di default possono non coprire tutte le casistiche che il cliente chiede di rispettare.")]
		public StackerCancelAOCMask SetStackerCancelAOCMask
		{
			get { return this.stackerCancelAOCMask; }
			set { this.stackerCancelAOCMask = value; }
		}

		[Category("6. FAN FOLD PARAMETERS"), PropertyOrder(110), Description("Level where fan fold is present")]
		public int FanFoldLevel 
		{
			get { return this.fanFoldLevel; }
			set { this.fanFoldLevel = value; }
		}

		[Category("6. FAN FOLD PARAMETERS"), PropertyOrder(120), Description("Cuts added at start of a fan fold Order")]
		public int CutsAddedAtStartOfOrder
		{
			get { return this.cutsAddedAtStartOfOrder; }
			set { this.cutsAddedAtStartOfOrder = value; }
		}

		[Category("6. FAN FOLD PARAMETERS"), PropertyOrder(130), Description("Start GAP AOC Speed when fan fold is involved")]
		public int StartAOCSpeedWithFanFold_UVm_m
		{
			get { return this.startAOCSpeedWithFanFold_UVm_m; }
			set { this.startAOCSpeedWithFanFold_UVm_m = value; }
		}

		[Category("6. FAN FOLD PARAMETERS"), PropertyOrder(140), Description("Final GAP AOC Speed when fan fold starts or ends")]
		public int FinalAOCSpeedWithFanFold_UVm_m
		{
			get { return this.finalAOCSpeedWithFanFold_UVm_m; }
			set { this.finalAOCSpeedWithFanFold_UVm_m = value; }
		}

		[Category("6. FAN FOLD PARAMETERS"), PropertyOrder(150), Description("Final GAP AOC Speed from fan fold to fan fold")]
		public int FinalAOCSpeedWithOnlyFanFold_UVm_m
		{
			get { return this.finalAOCSpeedWithOnlyFanFold_UVm_m; }
			set { this.finalAOCSpeedWithOnlyFanFold_UVm_m = value; }
		}

		[Category("6. FAN FOLD PARAMETERS"), PropertyOrder(160), Description("Final waste speed with fan fold Orders")]
		public int FinalWasteSpeedWithFanFold_UVm_m
		{
			get { return this.finalWasteSpeedWithFanFold_UVm_m; }
			set { this.finalWasteSpeedWithFanFold_UVm_m = value; }
		}

    //PMASSARO_FI24/02/10 - Aggiunta gestione accellerazione a cambio con FANFOLD
    [Category("6. FAN FOLD PARAMETERS"), PropertyOrder(170), Description("Max Accelleration Ramp In Case FANFOLD Is Included In The Order Change")]
    public int MaxRampAccOnFANFOLD_URmm_m
    {
      get { return this.maxRampAccOnFANFOLD_URmm_m; }
      set { this.maxRampAccOnFANFOLD_URmm_m = value; }
    }

    //RFAZZI_FI15/07/2010_1. Aggiunta gestione per conteggio fogli con ordini FanFold ad inizio ordine usando i metri fra Master e FanFold. 
    [Category("6. FAN FOLD PARAMETERS"), PropertyOrder(180), Description("Distance between Master and FanFold System. Used for calculate the number of sheet already produced after a Order Change")]
    public int DistanceBetweenMasterFanFold_UDmm
    {
      get { return this.distanceBetweenMasterFanFold_UDmm; }
      set { this.distanceBetweenMasterFanFold_UDmm = value; }
    }

	}
}