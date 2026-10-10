using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{

	//RFAZZI_FI14/07/2009_1.Handled the Metal Foils Sensor. 
	[TypeConverter(typeof(Converters))]
	public class ScrapSpliceDetected : SyncroC.DataBase.LineConfig.LineBase
	{

    private bool enableScrapSpliceDetected;
    //agrassi_FI26-08-09_1
    private bool enableScrapAOCSpliceDetected;
      
		private int distanceScrapFromSpliceDetector_UDmm;

		public ScrapSpliceDetected(int offset)
			: base()
		{
			
			this.sectionName = "ScrapSpliceDetected_" + offset.ToString();
      this.enableScrapSpliceDetected = false;
      //agrassi_FI26-08-09_1
      this.enableScrapAOCSpliceDetected = false;
      this.distanceScrapFromSpliceDetector_UDmm = 0;

		}

		[Description("se TRUE la giunta rilevata determina l'inizio della sequenza di scarto solo se non è stato ricevuto il segnale di 'Prewarn AOC'.")]
    //agrassi_FI29-08-09_1
    //public bool EnableScrapSpliceDetected
    public bool EnableScrapForSpliceDetected
    {
			get { return this.enableScrapSpliceDetected; }
			set { this.enableScrapSpliceDetected = value; }
		}

    //agrassi_FI26-08-09_1
    [Description("se TRUE la giunta rilevata determina l'inizio della sequenza di Cambio Ordine con scarto se precedentemente ricevuto il segnale di 'Prewarn AOC'.")]
    //agrassi_FI29-08-09_1
    //public bool EnableScrapAOCSpliceDetected
    public bool EnableScrapAOCForSpliceDetected
    {
      get { return this.enableScrapAOCSpliceDetected; }
      set { this.enableScrapAOCSpliceDetected = value; }
    }

    [Description("Distanza (in mm) fra il dispositivo di rilevazione della giunta e la Taglierina Ausiliaria")]
		public int DistanceScrapFromSpliceDetector_UDmm
		{
			get { return this.distanceScrapFromSpliceDetector_UDmm; }
			set { this.distanceScrapFromSpliceDetector_UDmm = value; }
		}

	};

  [TypeConverter(typeof(Converters))]
  public class Syncro : SyncroC.DataBase.LineConfig.LineBase
  {

		public const int MAX_NUM_SPLICER_DETECTOR = 4;
	
		//WASTE
    private bool enableDBWasteScrap;																// TRUE if the Scrap waste from the double backer is enabled
    private bool enableWasteScrapAfterStop;													// TRUE if the automatic waste scrap after a stop is enabled.
    private int wasteRemovalDistance;																// For waste removal tracking
    private int wasteRemovalMinimum;																// Minimum that can be removed
    private int wasteRemovalExtra;																	// Amount that is added to desired waste production - allows distance to be configured to start early if required
    private int wasteRemovalCutThrough;															// Determines wether to combine two pieces of waste into one
    private int wasteRemovalCruiseDelayTicks;												// Delay before cruise is re-enabled after removing waste
    private int maximumStopWithoutAutomaticWasteScrap_UTs;					//maximum stop time (seconds) without activate the automatic waste scrap
    private int automaticWasteScrapDelay_UDmm;											//mm of board not to be scraped on restart before automatic waste scrap.
    private int automaticWasteScrapLength_UDmm;											//mm of board to discharge on automatic waste scrap
    private int finalSplicerDistance;																//Used for remote chop out and AOC 
    private int finalSplicerPrewarnDistance;												//Used in wet end only mode to give the 'RSH prewarn' signal to ensure that the dry end reduces the speed to that required at AOC
    private int minimumStopTime_UTs;																// Delay for valid DownTime
    private int restartTime_UTs;																		// Delay for valid restart
    private bool enableDownTime;																		// Enables the DownTime 
    private int delayEnableDownloadMachinesSetup_UTms;							// Delay for enable Syncro tyo Download the setups to the machines
    private int delayBeforeLockDowntime_UTm;												// Delay for disable Syncro to modify the Downtime
    private bool enableAlarmForUnassignedDowntime;									// TRUE if Syncro set an alarm for unassigned Downtime

    //PMASSARO_FI10/06/08 - 1 - Added To Manage The First Visualization Of Sending Into Order Field
    private int firstSendingSelection0;
    private int firstSendingSelection1;
    private int firstSendingSelection2;
    //RFAZZI_FI27/05/2008_2.Enable Scrap Splice Made on bottom liner when cut to pattern program is running
    private bool enableScrapPrePrintPaper;		// TRUE when the splice made on Cut to Pattern Order must be discharged
		//RFAZZI_FI14/07/2009_1.Handled the Metal Foils Sensor. 
		private ScrapSpliceDetected[] scrapSpliceDetected;

    ////RFAZZI_FI27/05/2008_2.Enable Scrap Splice Made on bottom liner when cut to pattern program is running
    //cnt+=swprintf(scfg+cnt, L"%s=%s%s", L"EnableScrapAtRSHSpliceMadeWithCutToPatternOrder", String::Bool2Alpha(this->getEnableScrapAtRSHSpliceMadeWithCutToPatternOrder()), SEP);
    //for (int i=1;i<=SYNCRO_MAX_SPLICERS;i++)
    //{
    //  cnt+=swprintf(scfg+cnt, L"%s=%s%s", L"EnableScrapForSpliceMadeAtRSH", String::Bool2Alpha(this->getEnableScrapForSpliceMadeAtRSH(i)), SEP);
    //}

    public Syncro()
      : base()
    {
      this.sectionName = "SYNCRO";
      this.enableDBWasteScrap = false;
      this.enableWasteScrapAfterStop = false;
      this.wasteRemovalDistance = 0;
      this.wasteRemovalMinimum = 0;
      this.wasteRemovalExtra = 0;
      this.wasteRemovalCutThrough = 0;
      this.wasteRemovalCruiseDelayTicks = 0;
      this.maximumStopWithoutAutomaticWasteScrap_UTs = 0;
      this.automaticWasteScrapDelay_UDmm = 0;
      this.automaticWasteScrapLength_UDmm = 0;
      this.finalSplicerPrewarnDistance = 0;
      this.finalSplicerDistance = 0;
      this.minimumStopTime_UTs = 0;
      this.restartTime_UTs = 0;
      this.enableDownTime = false;
      this.delayEnableDownloadMachinesSetup_UTms = 0;
      this.delayBeforeLockDowntime_UTm = 0;
      this.enableAlarmForUnassignedDowntime = false;

      //PMASSARO_FI10/06/08 - 1 - Added To Manage The First Visualization Of Sending Into Order Field 
      this.firstSendingSelection0 = 0;
      this.firstSendingSelection1 = 0;
      this.firstSendingSelection2 = 0;

      //RFAZZI_FI27/05/2008_2.Enable Scrap Splice Made on bottom liner when cut to pattern program is running
      this.enableScrapPrePrintPaper = false;		// TRUE when the splice made on Cut to Pattern Order must be discharged

			this.scrapSpliceDetected = new ScrapSpliceDetected[MAX_NUM_SPLICER_DETECTOR];
			for (int i = 0; i < MAX_NUM_SPLICER_DETECTOR; i++)
			{
				this.scrapSpliceDetected[i] = new ScrapSpliceDetected(i);
			}
    }

    // ************************************************  SCRAP FROM GLUE UNIT  ******************************************
    [Category("1. SCRAP FROM GLUE UNIT"), PropertyOrder(10), Description("Se TRUE è abilitata la gestione del pulsante di comando di scarto dall'incollatore.")]
    public bool EnableDBWasteScrap
    {
      get { return enableDBWasteScrap; }
      set { enableDBWasteScrap = value; }
    }

    [Category("1. SCRAP FROM GLUE UNIT"), PropertyOrder(20), Description("Distanza da utilizzare per comandare lo scarto una volta ricevuto il segnale dal pulsante.")]
    public int WasteRemovalDistance
    {
      get { return wasteRemovalDistance; }
      set { wasteRemovalDistance = value; }
    }


    // ************************************************  AUTO SCRAP AFTER STOP  ******************************************
    [Category("2. AUTO SCRAP AFTER STOP"), PropertyOrder(10), Description("Se TRUE è abilitata la gestione automatica dello scarto dopo una ripartenza della linea.")]
    public bool EnableWasteScrapAfterStop
    {
      get { return enableWasteScrapAfterStop; }
      set { enableWasteScrapAfterStop = value; }
    }

    [Category("2. AUTO SCRAP AFTER STOP"), PropertyOrder(20), Description("Tempo (in Sec) minimo atteso per abilitare il ciclo di scarto automatico alla ripartenza della linea.")]
    public int MaximumStopWithoutAutomaticWasteScrap_UTs
    {
      get { return maximumStopWithoutAutomaticWasteScrap_UTs; }
      set { maximumStopWithoutAutomaticWasteScrap_UTs = value; }
    }

    [Category("2. AUTO SCRAP AFTER STOP"), PropertyOrder(30), Description("Quantitativo di cartone (in mm) da produrre prima di iniziare a scartare alla ripartenza della linea.")]
    public int AutomaticWasteScrapDelay_UDmm
    {
      get { return automaticWasteScrapDelay_UDmm; }
      set { automaticWasteScrapDelay_UDmm = value; }
    }

    [Category("2. AUTO SCRAP AFTER STOP"), PropertyOrder(40), Description("Quantitativo di cartone (n mm) da scartare alla ripartenza della linea.")]
    public int AutomaticWasteScrapLength_UDmm
    {
      get { return automaticWasteScrapLength_UDmm; }
      set { automaticWasteScrapLength_UDmm = value; }
    }


    // ************************************************  SPLICER REMOVAL  ******************************************
    [Category("4. SPLICER REMOVAL"), PropertyOrder(10), Description("Distanza da contare prima di comandare lo scarto una volta ricevuto il segnale di 'Giunta ai Piani' o in Cambio Ordine.")]
    public int FinalSplicerDistance
    {
      get { return finalSplicerDistance; }
      set { finalSplicerDistance = value; }
    }

    [Category("4. SPLICER REMOVAL"), PropertyOrder(20), Browsable(false), Description("Usato quando il sistema è configurato come WetEnd Control per dare il segnale di 'RSH prewarning' e gestire la decelerazione della linea per arrivare alla velocità di scarto richiesta.")]
    public int FinalSplicerPrewarnDistance
    {
      get { return finalSplicerPrewarnDistance; }
      set { finalSplicerPrewarnDistance = value; }
    }

    [Category("4. SPLICER REMOVAL"), PropertyOrder(30), Description("Enable Scrap when Splice is Detected on Preprint Paper")]
    public bool EnableScrapPrePrintPaper
    {
      get { return enableScrapPrePrintPaper; }
      set { enableScrapPrePrintPaper = value; }
    }

		//RFAZZI_FI14/07/2009_1.Handled the Metal Foils Sensor. 
    [Category("4. SPLICER REMOVAL"), PropertyOrder(40), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor)), Description("se TRUE il sistema attende il segnale per avviare la sequenza di Cambio Ordine. 0=BottomLiner,1-3 Bridge 1-3")]
		public ScrapSpliceDetected[] ScrapSpliceDetected
		{
			get { return this.scrapSpliceDetected; }
			set { this.scrapSpliceDetected = value; }
		}

    // ************************************************  DOWN TIME  ******************************************
    [Category("5. DOWN TIME"), PropertyOrder(10), Description("Se TRUE è abilitata la gestione dei DownTime")]
    public bool EnableDownTime
    {
      get { return enableDownTime; }
      set { enableDownTime = value; }
    }

    [Category("5. DOWN TIME"), PropertyOrder(20), Description("Ritardo (in sec) per considerare effettivamente ferma la linea.")]
    public int MinimumStopTime_UTs
    {
      get { return minimumStopTime_UTs; }
      set { minimumStopTime_UTs = value; }
    }

    [Category("5. DOWN TIME"), PropertyOrder(30), Description("Ritardo (in sec) per considerare effettivamente partita la linea")]
    public int RestartTime_UTs
    {
      get { return restartTime_UTs; }
      set { restartTime_UTs = value; }
    }

    [Category("5. DOWN TIME"), PropertyOrder(40), Description("Tempo massimo per assegnare /modificare un Downtime (in min) presente nel turno precedente.")]
    public int DelayBeforeLockDowntime_UTm
    {
      get { return delayBeforeLockDowntime_UTm; }
      set { delayBeforeLockDowntime_UTm = value; }
    }

    [Category("5. DOWN TIME"), PropertyOrder(50), Description("se TRUE viene visualizzato un messaggio quando una causale di fermata deve essere inserita.")]
    public bool EnableAlarmForUnassignedDowntime
    {
      get { return enableAlarmForUnassignedDowntime; }
      set { enableAlarmForUnassignedDowntime = value; }
    }

    // ************************************************  GENERAL SETTINGS  ******************************************
    [Category("6. GENERAL SETTINGS"), PropertyOrder(10), Description("Minimo quantitativo (in mm) che verrà scartato.")]
    public int WasteRemovalMinimum
    {
      get { return wasteRemovalMinimum; }
      set { wasteRemovalMinimum = value; }
    }

    [Category("6. GENERAL SETTINGS"), PropertyOrder(20), Description("Quantitativo di scarto che verrà aggiunto a quello identificato.")]
    public int WasteRemovalExtra
    {
      get { return wasteRemovalExtra; }
      set { wasteRemovalExtra = value; }
    }

    [Category("6. GENERAL SETTINGS"), PropertyOrder(25), Browsable(false), Description("Determines wether to combine two pieces of waste into one")]
    public int WasteRemovalCutThrough
    {
      get { return wasteRemovalCutThrough; }
      set { wasteRemovalCutThrough = value; }
    }

    [Category("6. GENERAL SETTINGS"), PropertyOrder(30), Description("Ritardo (in msec) per permettere di riabilitare la 'Cruise Speed' dopo che è stato rimosso dello scarto. NON GESTITO ATTUALMENTE DA SYNCRO.")]
    public int WasteRemovalCruiseDelayTicks
    {
      get { return wasteRemovalCruiseDelayTicks; }
      set { wasteRemovalCruiseDelayTicks = value; }
    }

    [Category("6. GENERAL SETTINGS"), PropertyOrder(40), Description("Ritardo (in msec) per abilitare Syncro all'invio degli ordini alle macchine dopo una sequenza di Cambio Ordine.")]
    public int DelayEnableDownloadMachinesSetup_UTms
    {
      get { return delayEnableDownloadMachinesSetup_UTms; }
      set { delayEnableDownloadMachinesSetup_UTms = value; }
    }

    // ************************************************  OPERATOR SETTINGS  ******************************************

    //PMASSARO_FI10/06/08 - 1 - Added To Manage The First Visualization Of Sending Into Order Field 
    [Category("7. OPERATOR SETTINGS"), PropertyOrder(10), Description("Imposta quale è la destinazione principale dei bancali quando viene inserito un ordine da Syncro. In base a questo valore, viene per prima proposta la destinazione ad esso associata. Order Position: Left (0 = Sending, 1 = Other, 2 = Transformation)")]
    public int FirstSendingSelection0
    {
      get { return firstSendingSelection0; }
      set { firstSendingSelection0 = value; }
    }

    //PMASSARO_FI10/06/08 - 1 - Added To Manage The First Visualization Of Sending Into Order Field 
    [Category("7. OPERATOR SETTINGS"), PropertyOrder(20), Description("Imposta quale è la destinazione principale dei bancali quando viene inserito un ordine da Syncro. In base a questo valore, viene per prima proposta la destinazione ad esso associata. Order Position: Left (0 = Sending, 1 = Other, 2 = Transformation)")]
    public int FirstSendingSelection1
    {
      get { return firstSendingSelection1; }
      set { firstSendingSelection1 = value; }
    }

    //PMASSARO_FI10/06/08 - 1 - Added To Manage The First Visualization Of Sending Into Order Field 
    [Category("7. OPERATOR SETTINGS"), PropertyOrder(30), Description("Imposta quale è la destinazione principale dei bancali quando viene inserito un ordine da Syncro. In base a questo valore, viene per prima proposta la destinazione ad esso associata. Order Position: Left (0 = Sending, 1 = Other, 2 = Transformation)")]
    public int FirstSendingSelection2
    {
      get { return firstSendingSelection2; }
      set { firstSendingSelection2 = value; }
    }
  }
}
