using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
  /// <summary>
  /// WEC (Wet End Control) configuration.
  /// </summary>
  public class WEC : SyncroC.DataBase.LineConfig.LineBase
  {
    bool allowSFAutoControlDisable;
    [PropertyOrder(40), Category("WEC"), Description("Se a 'True' il controllo della zona viene posta in manuale quando si vede che viene variata la velocità senza che sia Syncro a richiederlo.")]
    public bool AllowSFAutoControlDisable
    {
        get { return allowSFAutoControlDisable; }
        set { allowSFAutoControlDisable = value; }
    }

		int	distanceDBToKnife_UDmm;
    [PropertyOrder(50), Category("WEC"), Description("Distanza (in mm) fra l'inizio dei Piani Caldi e la Taglierina Trasversale. Usato per determinare l'istante di inizio delle giunte sincronizzate quando sono abilitate a 'Metri'.")]
    public int DistanceDBToKnife_UDmm
    {
        get { return distanceDBToKnife_UDmm; }
        set { distanceDBToKnife_UDmm = value; }
    }

		int	syncOnSplicerOffsetLength_UDmm;
    [PropertyOrder(60), Category("WEC"), Description("Distanza (in mm) Usata per anticipare il comando di giunta sulla giuntatrice sul cui fine bobina è sincronizzato il Cambio Qualità. Utile per non rischiare di veder anticipare il comando quando sulla giuntatrice sono presenti controlli quali 'Tail Link' od eccetera.")]
    public int SyncOnSplicerOffsetLength_UDmm
    {
        get { return syncOnSplicerOffsetLength_UDmm; }
        set { syncOnSplicerOffsetLength_UDmm = value; }
    }

		bool enableAdvancedSpliceSlowdown;
		[PropertyOrder(70), Browsable(false), Category("WEC"), Description("'True' if Syncro should begin slowdown of the line before sync starts if it detects that the splices will pass through the shear before normal sync slowdown reaches")]
		public bool EnableAdvancedSpliceSlowdown
		{
			get { return enableAdvancedSpliceSlowdown; }
			set { enableAdvancedSpliceSlowdown = value; }
		}

		int syncSpliceCountdownAdjustTolerance_UDmm;
    [PropertyOrder(80), Category("WEC"), Description("Distanza (in mm). Se Non-zero indica che viene abilitato il controllo per aggiornare il conteggio per inviare il comando di 'Giunta Ora' alla giuntatrice quando la distanza varia ( vedi ad esempio per variazione delle fasciature dopo che il conteggio è stato avviato ).")]
		public int SyncSpliceCountdownAdjustTolerance_UDmm
		{
			get { return syncSpliceCountdownAdjustTolerance_UDmm; }
			set { syncSpliceCountdownAdjustTolerance_UDmm = value; }
		}

		int splicerNotReadyStopAtRemaining_UDmm;
    [PropertyOrder(90), Category("WEC"), Description("Se la giuntatrice non è pronta alla giunta, quando i millimetri rimanenti in bobina sono inferiori a questo valore, si ferma la zona.")]
		public int SplicerNotReadyStopAtRemaining_UDmm
		{
			get { return splicerNotReadyStopAtRemaining_UDmm; }
			set { splicerNotReadyStopAtRemaining_UDmm = value; }
		}

    int qcSpliceDBPrewarnDistance_UDmm;
    [PropertyOrder(110), Category("WEC"), Description("Distanza (in mm) prima dei piani. Quando la prima giunta arriva a tale distanza, viene avviata la sequenza di Cambio Ordine e si calcola l'istante per l'eventuale decelerazione richiesta per arrivare con le giunte alla Rotary Shear alla corretta velocità di Scarto.")]
    public int QcSpliceDBPrewarnDistance_UDmm
    {
        get { return qcSpliceDBPrewarnDistance_UDmm; }
        set { qcSpliceDBPrewarnDistance_UDmm = value; }
    }

    bool enableSyncOnMetersAfterQC;
    [PropertyOrder(120), Category("WEC"), Description("Se TRUE viene forzato Syncro in 'Giunte a Metri' al temine di ogni sequenza di Cambio Qualità.")]
    public bool EnableSyncOnMetersAfterQC
    {
      get { return enableSyncOnMetersAfterQC; }
      set { enableSyncOnMetersAfterQC = value; }
    }

    bool enableSpliceConfirmedAfterQC;
    [PropertyOrder(130), Category("WEC"), Description("Se TRUE dopo una giunta sincronizzata viene automaticamente 'confermata' la giuntatrice coinvolta nel prossimo Cambio Qualità.")]
    public bool EnableSpliceConfirmedAfterQC
    {
      get { return enableSpliceConfirmedAfterQC; }
      set { enableSpliceConfirmedAfterQC = value; }
    }

    /// <summary>
    /// Builder.
    /// </summary>
    public WEC()
        : base()
    {
      sectionName = "WEC";

			this.allowSFAutoControlDisable = true;
			this.distanceDBToKnife_UDmm = 0;
			this.syncOnSplicerOffsetLength_UDmm = 0;
			this.enableAdvancedSpliceSlowdown = false;
			this.syncSpliceCountdownAdjustTolerance_UDmm = 0;
			this.splicerNotReadyStopAtRemaining_UDmm = 0;
    }
  }
}
