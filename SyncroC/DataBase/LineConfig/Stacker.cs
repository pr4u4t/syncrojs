using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
  /// <summary>
  /// Stacker configuration.
  /// </summary>
  public class Stacker : SyncroC.DataBase.LineConfig.LineBase
  {
    //stacker number
    private int number;

    //PMASSARO_FI02/05/08 - 8 - Added To Manage Pallet Print With Hardware Signals
    private int maxOrdersStackerCanManageOnBelts;
    [PropertyOrder(20), Category("Stacker"), Description("Numero massimo di ordini che possono essere gestiti dalla macchina sui nastri. L'impostazione di questo valore deve essere fatta in accordo con le specifiche della macchina ed è necessaria solo nei casi in cui Syncro gestisce la stampa dei cartellini per bancale utilizzando i segnali hardware di 'Scarico in Corso'. (MIN:2, MAX:5).")]
    public int MaxOrdersStackerCanManageOnBelts
    {
      get { return this.maxOrdersStackerCanManageOnBelts; }
      set { this.maxOrdersStackerCanManageOnBelts = value; }
    }

    private int maxStackHeight;
    [PropertyOrder(30), Category("Stacker"), Description("Massima altezza ( in mm ) del bancale di fogli. Il valore viene utilizzato congiuntamente con l'altezza dell'onda per verificare che il numero di fogli per bancale non sia superiore al massimo consentito dalla meccanica della macchina. In tal caso, l'ordine viene rifiutato.")]
    public int MaxStackHeight
    {
      get { return this.maxStackHeight; }
      set { this.maxStackHeight = value; }
    }

    private int maxBundleHeight;
    [PropertyOrder(40), Category("Stacker"), Description("Massima altezza ( in mm ) della mazzetta di fogli. Il valore viene utilizzato congiuntamente con l'altezza dell'onda per verificare che il numero di fogli per mazzetta non sia superiore al massimo consentito dalla meccanica della macchina. In tal caso, l'ordine viene rifiutato.")]
    public int MaxBundleHeight
    {
      get { return this.maxBundleHeight; }
      set { this.maxBundleHeight = value; }
    }

    private int maxDeliveryStackHeight;
    [PropertyOrder(50), Category("Stacker"), Description("Massima altezza del bancale per il bancale con destinazione “Spedizione” ( in mm ). Il valore impostato, viene utilizzato congiuntamente con l'altezza dell'onda per determinare se il numero di fogli impostato è corretto fornendo un eventuale messaggio di “warning” qualora sia superiore a questo limite ma comunque inferiore all'altezza prevista dal parametro MaxStackHeight.")]
    public int MaxDeliveryStackHeight
    {
      get { return this.maxDeliveryStackHeight; }
      set { this.maxDeliveryStackHeight = value; }
    }

    private int maxTransformStackHeight;
    [PropertyOrder(60), Category("Stacker"), Description("Massima altezza del bancale per il bancale con destinazione “Trasformazione” ( in mm ). Il valore impostato, viene utilizzato congiuntamente con l'altezza dell'onda per determinare se il numero di fogli impostato è corretto fornendo un eventuale messaggio di “warning” qualora sia superiore a questo limite ma comunque inferiore all'altezza prevista dal parametro MaxStackHeight.")]
    public int MaxTransformStackHeight
    {
      get { return this.maxTransformStackHeight; }
      set { this.maxTransformStackHeight = value; }
    }

    private double stabilityStackFactor;
    [PropertyOrder(70), Category("Stacker"), Description("Questo fattore viene moltiplicato per la larghezza del foglio; se il valore ricavato è inferiore all'altezza del bancale prevista in funzione della destinazione, il numero dei fogli per bancale verrò ridotto all'altezza calcolata.")]
    public double StabilityStackFactor
    {
      get { return this.stabilityStackFactor; }
      set { this.stabilityStackFactor = value; }
    }

    private int cutPulseWidth;
    [PropertyOrder(80), Category("Stacker"), Description("Utilizzato per fornire il segnale di taglio alla macchina. Il segnale verrà tenuto attivo per il periodo impostato in questo parametro ( in msec ). Il segnale non deve comunque stare alto per un periodo maggiore del tempo minimo fra due tagli consecutivi ( intervallo di tempo fra due tagli alla massima velocità ).")]
    public int CutPulseWidth
    {
      get { return this.cutPulseWidth; }
      set { this.cutPulseWidth = value; }
    }

    //RFazzi_FI28/05/2007_3.Added parameter sending the CutLength to the stacker when not working
    private int notWorkingCutLength_UDmm;		// Fixed value when the level doesn't work
    [PropertyOrder(90), Category("Stacker"), Description("Posizione per il cancello ( in mm ) passato alla macchina quando il livello non è in lavoro.")]
    public int notWorkingCutLength
    {
      get { return this.notWorkingCutLength_UDmm; }
      set { this.notWorkingCutLength_UDmm = value; }
    }

    private int optimizationWidth_UDmm;
    [PropertyOrder(100), Category("Stacker"), Description("Larghezza del bancale ( in mm ) a cui si esegue l'ottimizzazione delle pile sulle rulliere. Viene utilizzato per calcolare il numero di pile da accoppiare in fase di inserimento manuale degli ordini e/o quando si riceve l'ordine dagli uffici e il campo non è valorizzato.")]
    public int OptimizationWidth
    {
      get { return this.optimizationWidth_UDmm; }
      set { this.optimizationWidth_UDmm = value; }
    }

    //RFAZZI_FI12/06/2008_1.Added eng.verb. for Continuous Stack and Pallet Insert into Stacker section
    private bool continuousStack;
    [PropertyOrder(110), Category("Stacker"), Description("se TRUE l'impilamento continuo sulla macchina è possibile.")]
    public bool ContinuousStack
    {
      get { return this.continuousStack; }
      set { this.continuousStack = value; }
    }

    private bool palletInsertion;
    [PropertyOrder(120), Category("Stacker"), Description("Se TRUE sulla piattaforma dell'Impilatore è possibile mettere un pancale.")]
    public bool PalletInsertion
    {
      get { return this.palletInsertion; }
      set { this.palletInsertion = value; }
    }

    //agrassi_FI26-06-08
    bool trackCutSignal;
    [PropertyOrder(130), Category("Stacker"), Description("Se TRUE il segnale di taglio effettuato verrà inseguito fino al I° nastro. E' necessario un encoder montato sui sandwich di uscita della taglierina trasversale.")]
    public bool TrackCutSignal
    {
      get { return this.trackCutSignal; }
      set { this.trackCutSignal = value; }
    }

    bool trackBoardPresence;
    [PropertyOrder(140), Category("Stacker"), Description("Se TRUE la presenza cartone sotto le lame verrà inseguita fino al I° nastro. E' necessario un encoder montato sui sandwich di uscita della taglierina trasversale.")]
    public bool TrackBoardPresence
    {
      get { return this.trackBoardPresence; }
      set { this.trackBoardPresence = value; }
    }


    bool trackLastCut;
    [PropertyOrder(150), Category("Stacker"), Description("Se TRUE il segnale di Ultimo Taglio verrà inseguito fino al I° nastro. E' necessario un encoder montato sui sandwich di uscita della taglierina trasversale.")]
    public bool TrackLastCut
    {
      get { return this.trackLastCut; }
      set { this.trackLastCut = value; }
    }

    bool trackFirstCut;
    [PropertyOrder(160), Category("Stacker"), Description("Se TRUE il segnale di I° Taglio verrà inseguito fino al I° nastro. E' necessario un encoder montato sui sandwich di uscita della taglierina trasversale.")]
    public bool TrackFirstCut
    {
      get { return this.trackFirstCut; }
      set { this.trackFirstCut = value; }
    }


    /// <summary>
    /// Builder.
    /// </summary>
    /// <param name="number">0 for Low level, 1 for Mid level, 2 for High level.</param>
    public Stacker(int number)
      : base()
    {
      sectionName = "STACKER_" + (number + 1).ToString();
      this.number = number;

      maxStackHeight = 0;
      maxBundleHeight = 0;
      maxDeliveryStackHeight = 0;
      maxTransformStackHeight = 0;
      stabilityStackFactor = 0;
      cutPulseWidth = 0;
      notWorkingCutLength_UDmm = 1000;
      this.palletInsertion = false;
      this.continuousStack = false;
      //agrassi_FI26-06-08
      this.trackCutSignal = false;
      this.trackBoardPresence = false;
      this.trackLastCut = false;
      this.trackFirstCut = false;
    }
  }
}