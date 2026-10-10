using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	[TypeConverter(typeof(Converters))]
	public class HeaderPRC : SyncroC.DataBase.LineConfig.LineBase
	{
    public enum CloseLoopUpDownType { NotPresent = 0, 	Moisture_Sensor = 1, Warp_Detector_Bar = 2};
    public enum CloseLoopCorrectionType { Continuous = 0, Step = 1};

		public HeaderPRC()
			: base()
		{
			this.sectionName = "PRC";
			this.paperTresholdVeryLight_UWg = 0;
			this.paperTresholdLight_UWg = 0;
			this.paperTresholdMedium_UWg = 0;
			this.paperTresholdHeavy_UWg = 0;
			this.paperTresholdVeryHeavy_UWg = 0;
      this.showWarningWhenTemperatureLess = 0;
      this.showWarningWhenTemperatureLessDBHotSection = 0;
      this.showWarningWhenTemperatureLessDBColdSection = 0;
      this.standardRollWidthForCalculateTension_UDmm = 2500;
      //RFAZZI_FI20/11/2010_2. Aggiunta gestione sensore umidità ai Piani Caldi per Process. 
      this.enableBottom_UpperMoistureSensor = false;
      //RFAZZI_FI25/08/2011_1. Aggiunti parametri per PRC Warp Detector e CloseLoop. 
      this.enableWarpUpDownCloseLoopCorrectionTypeUsed = CloseLoopCorrectionType.Step;
      this.EnableWarpDetectorBar = false;
      this.closeLoopUpDownTypeUsed = CloseLoopUpDownType.NotPresent;
    }
		int paperTresholdVeryLight_UWg;
		[PropertyOrder(140), Category("Soglia Carte"), Description("Very light (VL). Se la grammatura <= di questa soglia, allora la carta è VL.")]
		public int PaperTresholdVeryLight_UWg 
		{
			get { return this.paperTresholdVeryLight_UWg; }
			set { this.paperTresholdVeryLight_UWg = value; }
		}
		int paperTresholdLight_UWg;
    [PropertyOrder(150), Category("Soglia Carte"), Description("Light (L). Se la grammatura <= di questa soglia, allora la carta è L")]
		public int PaperTresholdLight_UWg
		{
			get { return this.paperTresholdLight_UWg; }
			set { this.paperTresholdLight_UWg = value; }
		}
		int paperTresholdMedium_UWg;
    [PropertyOrder(160), Category("Soglia Carte"), Description("Medium (M). Se la grammatura <= di questa soglia, allora la carta è M")]
		public int PaperTresholdMedium_UWg
		{
			get { return this.paperTresholdMedium_UWg; }
			set { this.paperTresholdMedium_UWg = value; }
		}
		int paperTresholdHeavy_UWg;
    [PropertyOrder(170), Category("Soglia Carte"), Description("Heavy (H). Se la grammatura <= di questa soglia, allora la carta è H")]
		public int PaperTresholdHeavy_UWg
		{
			get { return this.paperTresholdHeavy_UWg; }
			set { this.paperTresholdHeavy_UWg = value; }
		}
		int paperTresholdVeryHeavy_UWg;
    [PropertyOrder(180), Category("Soglia Carte"), Description("Very heavy (VH). Se la grammatura <= di questa soglia, allora la carta è VH")]
		public int PaperTresholdVeryHeavy_UWg
		{
			get { return this.paperTresholdVeryHeavy_UWg; }
			set { this.paperTresholdVeryHeavy_UWg = value; }
		}

    int standardRollWidthForCalculateTension_UDmm;
    [PropertyOrder(185), Category("Tensione Carta"), Description("Larghezza (in mm) Standard delle Bobine per calcolare la curva della tensione.")]
    public int StandardRollWidthForCalculateTension_UDmm
    {
      get { return standardRollWidthForCalculateTension_UDmm; }
      set { standardRollWidthForCalculateTension_UDmm = value; }
    }

    int errorTemperatureVeryBig;
    [PropertyOrder(190), Category("Errore Temperatura"), Description("se l'errore di temperatura è > ErrorTemperatureVeryBig (Di solito ErrorTemperatureBig + 20). Si corregge la posizione in modo MOLTO VELOCE.")]
    public int ErrorTemperatureVeryBig
    {
      get { return errorTemperatureVeryBig; }
      set { errorTemperatureVeryBig = value; }
    }

    int errorTemperatureBig;
    [PropertyOrder(200), Category("Errore Temperatura"), Description("se l'errore di temperatura è > ErrorTemperatureBig (Di solito ErrorTemperatureMedium + 15). Si corregge la posizione in modo VELOCE.")]
    public int ErrorTemperatureBig
    {
      get { return errorTemperatureBig; }
      set { errorTemperatureBig = value; }
    }

    int errorTemperatureMedium;
    [PropertyOrder(210), Category("Errore Temperatura"), Description("se l'errore di temperatura è > ErrorTemperatureMedium (Di solito ErrorTemperatureLittle + 10). Si corregge la posizione in modo NORMALE.")]
    public int ErrorTemperatureMedium
    {
      get { return errorTemperatureMedium; }
      set { errorTemperatureMedium = value; }
    }

    int errorTemperatureLittle;
    [PropertyOrder(220), Category("Errore Temperatura"), Description("se l'errore di temperatura è > ErrorTemperatureLittle (Di solito 15) . Si corregge la posizione in modo LENTO. Altrimenti in MOLTO LENTO.")]
    public int ErrorTemperatureLittle
    {
      get { return errorTemperatureLittle; }
      set { errorTemperatureLittle = value; }
    }

    int showWarningWhenTemperatureLess;
    [PropertyOrder(230), Category("Errore Temperatura"), Description("(WRAP ARM) Fai vedere un Attenzione se la temperatura è minore di ")]
    public int ShowWarningWhenTemperatureLess
    {
      get { return showWarningWhenTemperatureLess; }
      set { showWarningWhenTemperatureLess = value; }
    }

    int showWarningWhenTemperatureLessDBHotSection;
    [PropertyOrder(240), Category("Errore Temperatura"), Description("(DB HOT SECTION) Fai vedere un Attenzione se la temperatura è minore di ")]
    public int ShowWarningWhenTemperatureLessDBHotSection
    {
      get { return showWarningWhenTemperatureLessDBHotSection; }
      set { showWarningWhenTemperatureLessDBHotSection = value; }
    }

    int showWarningWhenTemperatureLessDBColdSection;
    [PropertyOrder(250), Category("Errore Temperatura"), Description("(DB COLD SECTION) Fai vedere un Attenzione se la temperatura è minore di ")]
    public int ShowWarningWhenTemperatureLessDBColdSection
    {
      get { return showWarningWhenTemperatureLessDBColdSection; }
      set { showWarningWhenTemperatureLessDBColdSection = value; }
    }

    //RFAZZI_FI20/11/2010_2. Aggiunta gestione sensore umidità ai Piani Caldi per Process. 
    bool enableBottom_UpperMoistureSensor;
    [PropertyOrder(300), Category("Sensori di Umidità"), Description("se TRUE è presente la gestione dei sensori di umidità.")]
    public bool EnableBottom_UpperMoistureSensor
    {
      get { return enableBottom_UpperMoistureSensor; }
      set { enableBottom_UpperMoistureSensor = value; }
    }

    //RFAZZI_FI25/08/2011_1. Aggiunti parametri per PRC Warp Detector e CloseLoop. 
    bool enableWarpDetectorBar;
    [PropertyOrder(310), Category("Barra Rilevazione Imbarcamento"), Description("se TRUE è presente la gestione della barra per la rilevazione dell'imbarcamento del cartone.")]
    public bool EnableWarpDetectorBar
    {
      get { return enableWarpDetectorBar; }
      set { enableWarpDetectorBar = value; }
    }

    CloseLoopCorrectionType enableWarpUpDownCloseLoopCorrectionTypeUsed;
    [PropertyOrder(320), Category("Close Loop"), Description("Selezionare se correggere l'imbarcamento Up/Down a Step o Continuamente")]
    public CloseLoopCorrectionType EnableWarpUpDownCloseLoopCorrectionTypeUsed
    {
      get { return enableWarpUpDownCloseLoopCorrectionTypeUsed; }
      set { enableWarpUpDownCloseLoopCorrectionTypeUsed = value; }
    }

    CloseLoopUpDownType closeLoopUpDownTypeUsed;
    [PropertyOrder(250), Category("Close Loop"), Description("Selezionare il sistema da utilizzare per il Close Loop")]
    public CloseLoopUpDownType CloseLoopUpDownTypeUsed
    {
      get { return closeLoopUpDownTypeUsed; }
      set { closeLoopUpDownTypeUsed = value; }
    }

  }

	[TypeConverter(typeof(Converters))]
	public class PRC : SyncroC.DataBase.LineConfig.LineBase
	{
		public PRC()
			: base()
		{
			this.sectionName = "PRC";
			this.header=new HeaderPRC();
		}

		HeaderPRC header;
		[PropertyOrder(5), Category("Process"), Description("Header")]
		public HeaderPRC Header
		{
			get { return this.header; }
			set { this.header = value; }
		}

	}
}
