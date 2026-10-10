using System;
using System.ComponentModel;
using System.Reflection;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	/// <summary>
	/// Summary description for SingleFacer.
	/// </summary>
	public class Splicer : SyncroC.DataBase.LineConfig.LineBase
	{
    bool endOfRollSpliceEnabled;
    bool useHardwareForRollCalcs;
    bool paperBreakEnabled;
    bool forceToManualAfterSpliceMadeEnabled;
    bool downloadPaperPathContentsEnabled;

    double mmPerPulse;
    double cycle_UTs;
    double zeroSpeed_UTs;

    int maxSpliceSpeed_UVmm_m;
    int defaultSpliceSpeed_UVmm_m;
    int requiredUnitSpliceSpeedDiff_UVmm_m;
    int distanceHeadToExit_UDmm;
    int distanceHeadToExitOffsetHeadB_UDmm;
    int distanceExitToRoll_UDmm;
    int steadySpeedPostSpliceDistance_UDmm;
    int earlyWarningOperator_UDmm;
    int earlyWarningMachine_UDmm;
    int sfAOCOffsetDistance_UDmm;
    int requiredPrefill_UDmm;
    int requiredExtraPrefill_UDmm;
    int steadySpeedPreSplice_UTs;
    int unusedDefaultCore_UDmm;

    //splicer number
    private int splicerNumber;

		//RFAZZI_FI21/01/2009_2.Added Paper Tension into Splicer Monitor Data. 
		PaperTension paperTension;

    /// <summary>
    /// Builder.
    /// </summary>
    /// <param name="number"></param>
    public Splicer(int SplicerNumber)
      : base()
    {
      this.sectionName = "Splicer_" + SplicerNumber.ToString();
      this.splicerNumber = SplicerNumber;

      this.mmPerPulse = 0;
      this.cycle_UTs = 0;
      this.zeroSpeed_UTs = 0;
      this.endOfRollSpliceEnabled = false;
      this.paperBreakEnabled = false;
      this.steadySpeedPreSplice_UTs = 0;
      this.steadySpeedPostSpliceDistance_UDmm = 0;
      this.earlyWarningOperator_UDmm = 0;
      this.earlyWarningMachine_UDmm = 0;
      this.useHardwareForRollCalcs = true;
      this.distanceHeadToExit_UDmm = 0;
      this.distanceHeadToExitOffsetHeadB_UDmm = 0;
      this.distanceExitToRoll_UDmm = 0;
      this.downloadPaperPathContentsEnabled = false;
      this.requiredPrefill_UDmm = 0;
      this.requiredExtraPrefill_UDmm = 0;
      this.requiredUnitSpliceSpeedDiff_UVmm_m = 0;
      this.maxSpliceSpeed_UVmm_m = 0;
      this.defaultSpliceSpeed_UVmm_m = 0;
      this.unusedDefaultCore_UDmm = 0;
      this.forceToManualAfterSpliceMadeEnabled = false;
			//RFAZZI_FI21/01/2009_2.Added Paper Tension into Splicer Monitor Data. 
			this.paperTension = new PaperTension(splicerNumber );
    }


    /***********************************************************************************
     *                  SPLICER ENABLES
     **********************************************************************************/
    [Category("1. SPLICER ENABLES"), PropertyOrder(10), Description("se TRUE Syncro eseguirà le giunte di fine bobina. Da ricordare che la funzionalità verrà fatta solo se è stato messo il controllo della giuntatrice in automatico nella pagina HomeWetEnd.")]
    public bool EndOfRollSpliceEnabled
    {
      get { return this.endOfRollSpliceEnabled; }
      set { this.endOfRollSpliceEnabled = value; }
    }

		[Category("1. SPLICER ENABLES"), PropertyOrder(20), Browsable(false), Description("TRUE enables use of hardware signals to calculate meters on roll")]
    public bool UseHardwareForRollCalcs
    {
      get { return this.useHardwareForRollCalcs; }
      set { this.useHardwareForRollCalcs = value; }
    }

    [Category("1. SPLICER ENABLES"), PropertyOrder(30), Description("se TRUE Syncro abilita il controllo del sistema di rilevazione della rottura carta ( tipicamente usando le fotocellule che possono essere fornite per una giuntatrice NON Fosber ). Se abilitato e viene rilevato uno strappo carta, verrà dato il comando di 'Stop Normale' al Gruppo Ondulatore e/o Piani Caldi.")]
    public bool PaperBreakEnabled
    {
      get { return this.paperBreakEnabled; }
      set { this.paperBreakEnabled = value; }
    }

    [Category("1. SPLICER ENABLES"), PropertyOrder(40), Description("se TRUE Syncro cambia lo stato del controllo della giuntatrice in 'Manuale' dopo aver ricevuto il segnale di 'Giunta Effettuata'. Chiedere conferma ad UTP prima di abilitare questa funzionalità.")]
    public bool ForceToManualAfterSpliceMadeEnabled
    {
      get { return this.forceToManualAfterSpliceMadeEnabled; }
      set { this.forceToManualAfterSpliceMadeEnabled = value; }
    }

    //PMASSARO_FI - Abilitata la visualizzazione a grafica del parametro 'DownloadPaperPathContentsEnabled'
    //[Category("1. SPLICER ENABLES"), PropertyOrder(50), Browsable(false), Description("TRUE if using PLC downloaded paper path  contents instead of splice head to exit + roll B offset")]
    [Category("1. SPLICER ENABLES"), PropertyOrder(50), Description("se TRUE Syncro utilizza il dato 'Contenuto Festone' fornito dalla giuntatrice (comprensivo già dell' Offset testa B in lavoro). Ad oggi disponibile solo per LINK400 e protocollo ethernet attivo")]
    public bool DownloadPaperPathContentsEnabled
    {
      get { return this.downloadPaperPathContentsEnabled; }
      set { this.downloadPaperPathContentsEnabled = value; }
    }


    /***********************************************************************************
     *                  SPLICER SPEED
     **********************************************************************************/
    [Category("2. SPLICER SPEED"), PropertyOrder(10), Description("Rappresenta i millimetri svolti dalla bobina per ogni impulso encoder ricevuto. Viene utilizzato per il calcolo della velocità della macchina.")]
		public double MMPerPulse
		{
			get { return this.mmPerPulse; }
			set { this.mmPerPulse = value; }
		}

		[Category("2. SPLICER SPEED"), PropertyOrder(20), Browsable(false), Description("Absolute max splice speed")]
    public int MaxSpliceSpeed_UVmm_m
    {
      get { return this.maxSpliceSpeed_UVmm_m; }
      set { this.maxSpliceSpeed_UVmm_m = value; }
    }

    [Category("2. SPLICER SPEED"), PropertyOrder(30), Description("Velocità di giunta di default della macchina. Utilizzato per valorizzare la velocità di giunta associato nel dbase delle carte per ogni giuntatrice. Inserire il valore rispetto alla velocità del Gruppo Ondulatore e/o Piani Caldi.")]
    public int DefaultSpliceSpeed_UVmm_m
    {
      get { return this.defaultSpliceSpeed_UVmm_m; }
      set { this.defaultSpliceSpeed_UVmm_m = value; }
    }

    [Category("2. SPLICER SPEED"), PropertyOrder(40), Description("Differenza di velocità in mm/min tra velocità di giunta e velocità della splicer al di sopra della quale si aggiunge la quantità RequiredExtraPrefill alla quantità di RequiredPrefill sul ponte.")]
    public int RequiredUnitSpliceSpeedDiff_UVmm_m
    {
      get { return this.requiredUnitSpliceSpeedDiff_UVmm_m; }
      set { this.requiredUnitSpliceSpeedDiff_UVmm_m = value; }
    }

    /***********************************************************************************
     *                  SPLICER DISTANCES
     **********************************************************************************/
    [Category("3. SPLICER DISTANCES"), PropertyOrder(10), Description("Distanza (in mm) fra il punto di giunta e il rullo di uscita macchina ( in mm ) quando è in lavoro sul lato A del portabobine.")]
    public int DistanceHeadToExit_UDmm
    {
      get { return this.distanceHeadToExit_UDmm; }
      set { this.distanceHeadToExit_UDmm = value; }
    }

    [Category("3. SPLICER DISTANCES"), PropertyOrder(20), Description("Offset (in mm) di carta presente fra il punto di giunta e il rullo di uscita macchina quando è in lavoro la bobina sul lato B del portabobine.  (In caso di giuntatrice Fosber Links il valore deve essere negativo)")]
    public int DistanceHeadToExitOffsetHeadB_UDmm
    {
      get { return this.distanceHeadToExitOffsetHeadB_UDmm; }
      set { this.distanceHeadToExitOffsetHeadB_UDmm = value; }
    }

    [Category("3. SPLICER DISTANCES"), PropertyOrder(30), Description("Distanza (in mm) tra l’uscita della giuntatrice ed il rullo ondulatore misurata con tutti i fasciatori al minimo. Nel caso della giuntatrice 1 è la distanza tra l’uscita e l’ingresso dei piani.")]
    public int DistanceExitToRoll_UDmm
    {
      get { return this.distanceExitToRoll_UDmm; }
      set { this.distanceExitToRoll_UDmm = value; }
    }

    [Category("3. SPLICER DISTANCES"), PropertyOrder(40), Description("Distanza (in mm) dopo la giunta effettuata a cui la velocità della macchina deve essere tenuta costante.")]
    public int SteadySpeedPostSpliceDistance_UDmm
    {
      get { return this.steadySpeedPostSpliceDistance_UDmm; }
      set { this.steadySpeedPostSpliceDistance_UDmm = value; }
    }

    [Category("3. SPLICER DISTANCES"), PropertyOrder(50), Description("Distanza (in mm) prima della giunta per attivare lo stato di preallarme per l’operatore (EarlyWarningOperator >= EarlyWarningMachine).")]
    public int EarlyWarningOperator_UDmm
    {
      get { return this.earlyWarningOperator_UDmm; }
      set { this.earlyWarningOperator_UDmm = value; }
    }

    [Category("3. SPLICER DISTANCES"), PropertyOrder(60), Description("Distanza (in mm) prima della giunta per attivare lo stato di preallarme della macchina.")]
    public int EarlyWarningMachine_UDmm
    {
      get { return this.earlyWarningMachine_UDmm; }
      set { this.earlyWarningMachine_UDmm = value; }
    }

    [Category("3. SPLICER DISTANCES"), PropertyOrder(70), Description("Distanza da contare prima di inviare il segnale di 'Cambio Qualità' al Gruppo Ondulatore dopo che è stata effettuata la giunta.")]
    public int SfAOCOffsetDistance_UDmm
    {
      get { return this.sfAOCOffsetDistance_UDmm; }
      set { this.sfAOCOffsetDistance_UDmm = value; }
    }


    /***********************************************************************************
     *                  SPLICER SPLICE CYCLE
     **********************************************************************************/
    [Category("4. SPLICE CYCLE"), PropertyOrder(10), Description("Tempo medio (in sec) di reazione della giuntatrice al comando di giunta . Per la visualizzazione di quanto inserire, accedere alla pagina di Stato Giuntatrice presente all'interno delle pagine ingegneristiche di questa macchina e verficare parametro 'Last Cycle of Splice'.")]
    public double Cycle_UTs
    {
      get { return this.cycle_UTs; }
      set { this.cycle_UTs = value; }
    }

    [Category("4. SPLICE CYCLE"), PropertyOrder(20), Description("Tempo medio (in sec) a cui la carta in macchina è ferma in fase di giunta.")]
    public double ZeroSpeed_UTs
    {
      get { return this.zeroSpeed_UTs; }
      set { this.zeroSpeed_UTs = value; }
    }

    [Category("4. SPLICE CYCLE"), PropertyOrder(30), Description("Quantità di carta (in mm) da aggiungere alla scorta ponte prima di una giunta.")]
    public int RequiredPrefill_UDmm
    {
      get { return this.requiredPrefill_UDmm; }
      set { this.requiredPrefill_UDmm = value; }
    }

    [Category("4. SPLICE CYCLE"), PropertyOrder(40), Description("Quantità (in mm) aggiuntiva di carta da avere sul ponte quando la differenza di velocità è maggiore del valore impostato nel parametro RequiredUnitSpliceSpeedDiff_UVmm_m. NB, il presente valore si aggiunge a quello presente nel parametro RequiredPrefill_UDmm.")]
    public int RequiredExtraPrefill_UDmm
    {
      get { return this.requiredExtraPrefill_UDmm; }
      set { this.requiredExtraPrefill_UDmm = value; }
    }

    [Category("4. SPLICE CYCLE"), PropertyOrder(50), Description("Tempo (in sec) di anticipo a cui arrivare alla velocità di giunta desiderata.")]
    public int SteadySpeedPreSplice_UTs
    {
			get { return this.steadySpeedPreSplice_UTs; }
			set { this.steadySpeedPreSplice_UTs = value; }
    }

		[Category("4. SPLICE CYCLE"), PropertyOrder(60), Browsable(false), Description("Default core size - for paper with no core configured")]
    public int UnusedDefaultCore_UDmm
		{
			get { return this.unusedDefaultCore_UDmm; }
			set { this.unusedDefaultCore_UDmm = value; }
		}

		//RFAZZI_FI21/01/2009_2.Added Paper Tension into Splicer Monitor Data. 
		/***********************************************************************************
		 *                  SPLICER TENSION CONTROL
		 **********************************************************************************/
		[Category("5. SPLICER PAPER TENSION"), PropertyOrder(10), Description("PaperTension")]
		public PaperTension PaperTension
		{
			get { return paperTension; }
			set { paperTension = value; }
		}

	}
}



			