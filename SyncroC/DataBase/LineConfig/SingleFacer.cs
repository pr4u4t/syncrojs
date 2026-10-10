using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	[TypeConverter(typeof(Converters))]
	public class Cartridge : SyncroC.DataBase.LineConfig.LineBase
	{
		private bool cartridgeValid;											// TRUE Indicates if the cartridge is valid for this machine	
    [PropertyOrder(10), Category("Cartridge"), Description("se TRUE indica che questa cartuccia è configurata sulla macchina. Il tipo di cartuccia presente viene identificata usando degli appositi ingressi ( per Gruppi Ondulatori NON Fosber ) o ricevuto via protocollo se Fosber.")]
		public bool CartridgeValid
		{
			get { return cartridgeValid; }
			set { cartridgeValid = value; }
		}

		private string fluteId;														// Flute Id [A, B, C, D, E, ... ]
    [PropertyOrder(20), Category("Cartridge"), Description("Onda associata alla cartuccia. Se non coincide con l’onda prevista dall’ordine sul single facer viene dato un allarme. [A, B, C, D, E, ... ].")]
		public string FluteId
		{
			get { return fluteId; }
			set { fluteId = value; }
		}

		private string cartridgeId;												// Cartridge Id displayed [A, B1, B2, B3, E, ... ]
    [PropertyOrder(30), Category("Cartridge"), Description("Nome univoco che identifica la cartuccia [A, B1, B2, B3, E, ... ].")]
		public string CartridgeId
		{
			get { return cartridgeId; }
			set { cartridgeId = value; }
		}

		private int highEyeContents_UDmm;									// Eye positions on the bridge
    [PropertyOrder(40), Category("Cartridge"), Description("Contenuto del ponte in mm in corrispondenza della fotocellula di 'massimo contenuto'. NB, il valore deve essere rivisto una volta che la calibrazione del ponte è attiva e funzionante verificando il contenuto effettivo quando la fotocellula è impegnata con velocità del Gruppo Ondulatore e dei Piani Caldi uguali.")]
		public int HighEyeContents_UDmm
		{
			get { return highEyeContents_UDmm; }
			set { highEyeContents_UDmm = value; }
		}

		private int lowEyeContents_UDmm;									// Used for bridge control
    [PropertyOrder(50), Category("Cartridge"), Description("Contenuto del ponte in mm in corrispondenza della fotocellula di 'minimo contenuto'. NB, il valore deve essere rivisto una volta che la calibrazione del ponte è attiva e funzionante verificando il contenuto effettivo quando la fotocellula è impegnata con velocità del Gruppo Ondulatore e dei Piani Caldi uguali.")]
		public int LowEyeContents_UDmm
		{
			get { return lowEyeContents_UDmm; }
			set { lowEyeContents_UDmm = value; }
		}

		private int requiredContents_UDmm;								// Control position - MUST BE BETWEEN eyes
    [PropertyOrder(60), Category("Cartridge"), Description("Contenuto del ponte richiesto durante la normale produzione. Per normale produzione si intende la fase in cui la scorta è al valore richiesto, e il Gruppo Ondulatore e Piani Caldi vanno alla medesima velocità. Durante Il valore impostato deve essere sempre compreso tra il minimo e il massimo contenuto del ponte impostato in corrispondenza delle fotocellule.")]
		public int RequiredContents_UDmm
		{
			get { return requiredContents_UDmm; }
			set { requiredContents_UDmm = value; }
		}

    //PMASSARO_FI13/01/10 - Aggiunta nuova condizione quando la situazione è molto critica: poco prefill fattibile e velocità molto basse 
    private int requiredContentsWhenSpliceCycleIsRunning_UDmm;								// Control position - MUST BE BETWEEN eyes
    [PropertyOrder(65), Category("Cartridge"), Description("Contenuto richiesto sul ponte durante il ciclo di giunta. Questo parametro può essere usato quando si può fare poco precaricamento per mancanza di spazio sul ponte e allora si gestisce la velocità dei piani caldi facendo in modo che segua la velocità del Gruppo Ondulatore. Per l'uso di questo parametro, consultare UTP.")]
    public int RequiredContentsWhenSpliceCycleIsRunning_UDmm
    {
      get { return requiredContentsWhenSpliceCycleIsRunning_UDmm; }
      set { requiredContentsWhenSpliceCycleIsRunning_UDmm = value; }
    }

		private double encoderMMPerPulse;									// Used for speed of single facer on each flute
    [PropertyOrder(70), Category("Cartridge"), Description("Rappresenta i millimetri di cartone prodotti per ogni impulso encoder ricevuto. Viene utilizzato per il calcolo della velocità della macchina e per le giunte sincronizzate.")]
		public double EncoderMMPerPulse
		{
			get { return encoderMMPerPulse; }
			set { encoderMMPerPulse = value; }
		}

		private double expectedTakeupFactor;
    [PropertyOrder(80), Category("Cartridge"), Description("Fattore di ondulazione dell’onda prodotta dalla cartuccia, ovvero rapporto tra quantità di carta del medium (messa su un piano) e la quantità di carta del liner in una data quantità di cartone.")]
		public double ExpectedTakeupFactor
		{
			get { return expectedTakeupFactor; }
			set { expectedTakeupFactor = value; }
		}

		/// <summary>
		/// Builder.
		/// </summary>
		public Cartridge(string sfName, string number)
			: base()
		{
			this.sectionName = sfName + "_CARTRIDGE_" + number;

			this.cartridgeValid = false;
			this.fluteId = "";
			this.cartridgeId = "";
			this.highEyeContents_UDmm = 0;
			this.lowEyeContents_UDmm = 0;
			this.requiredContents_UDmm = 0;
      this.requiredContentsWhenSpliceCycleIsRunning_UDmm = 0; //PMASSARO_FI13/01/10 - Aggiunta nuova condizione quando la situazione è molto critica: poco prefill fattibile e velocità molto basse
			this.encoderMMPerPulse = 0;
		}
	}

	/// <summary>
	/// Summary description for SingleFacer.
	/// </summary>
	public class SingleFacer : SyncroC.DataBase.LineConfig.LineBase
	{
    bool preHeaterAndGlueGapManualIfRemote;
    [Category("0. Generic"), PropertyOrder(10), Description("NON FOSBER PreHeaters and GlueGap if has no management LOCAL/REMOTE for preheater/GlueGap. Preheaters/GlueGap then be put in MANUAL if they are REMOTE.")]
    public bool PreHeaterAndGlueGapManualIfRemote
    {
      get { return preHeaterAndGlueGapManualIfRemote; }
      set { preHeaterAndGlueGapManualIfRemote = value; }
    }

    // *************************************  SINGLE FACER PARAFFIN MACHINE   ************************************
    int extraPaperPathParaffinMachineLiner_UDmm;
    [Category("7. Paraffin Machine"), PropertyOrder(10), Description("Inserire la variazione del quantitativo di carta presente fra la giuntatrice e il Gruppo Ondulatore quando il paraffinatore è presente (in mm) sul lato liner.")]
    public int ExtraPaperPathParaffinMachineLiner_UDmm
    {
      get { return extraPaperPathParaffinMachineLiner_UDmm; }
      set { extraPaperPathParaffinMachineLiner_UDmm = value; }
    }

    int extraPaperPathParaffinMachineMedium_UDmm;
    [Category("7. Paraffin Machine"), PropertyOrder(20), Description("Inserire la variazione del quantitativo di carta presente fra la giuntatrice e il Gruppo Ondulatore quando il paraffinatore è presente (in mm) sul lato onda.")]
    public int ExtraPaperPathParaffinMachineMedium_UDmm
    {
      get { return extraPaperPathParaffinMachineMedium_UDmm; }
      set { extraPaperPathParaffinMachineMedium_UDmm = value; }
    }

		// *************************************  SINGLE FACER SPEED   ************************************
		int crawlSpeed_UVmm_m; // Minimum speed for the unit
    [Category("1. SingleFacer SPEED"), PropertyOrder(10), Description("Velocità a cui portare la macchina ( mm/min ) ad esempio nei casi in cui la giuntatrice 1 non è pronta alla giunta.")]
		public int CrawlSpeed_UVmm_m
		{
			get { return crawlSpeed_UVmm_m; }
			set { crawlSpeed_UVmm_m = value; }
		}

		int speedTolerance_UVmm_m;					// Speed within which speed is not adjusted
    [Category("1. SingleFacer SPEED"), PropertyOrder(20), Description("Tolleranza di velocità (mm/min) utilizzata durante la fase di raggiungimento del target previsto.")]
		public int SpeedTolerance_UVmm_m
		{
			get { return speedTolerance_UVmm_m; }
			set { speedTolerance_UVmm_m = value; }
		}
		int maxSpeed_UVmm_m;								// Absolute max speed of machine
    [Browsable(true), Category("1. SingleFacer SPEED"), PropertyOrder(30), Description("Massima velocità (mm/min) a cui la macchina può andare.")]
		public int MaxSpeed_UVmm_m
		{
			get { return maxSpeed_UVmm_m; }
			set { maxSpeed_UVmm_m = value; }
		}

		int refusedMinSetpoint_UVmm_m;
    [Category("1. SingleFacer SPEED"), PropertyOrder(60), Description("Range di velocità proibito per i set point, il quale viene spostato al di fuori del range (per disabilitare la funzionalità mettere il valore 0).")]
		public int RefusedMinSetpoint_UVmm_m
		{
			get { return refusedMinSetpoint_UVmm_m; }
			set { refusedMinSetpoint_UVmm_m = value; }
		}

		int refusedMaxSetpoint_UVmm_m;
    [Category("1. SingleFacer SPEED"), PropertyOrder(70), Description("Range di velocità proibito per i set point, il quale viene spostato al di fuori del range (per disabilitare la funzionalità mettere il valore 0).")]
		public int RefusedMaxSetpoint_UVmm_m
		{
			get { return refusedMaxSetpoint_UVmm_m; }
			set { refusedMaxSetpoint_UVmm_m = value; }
		}

		ZoneSpeedConfig acceleration;
		[Category("1. SingleFacer SPEED"), PropertyOrder(40), Description("Accellerazione")]
		public ZoneSpeedConfig Acceleration
		{
			get { return acceleration; }
			set { acceleration = value; }
		}

		ZoneSpeedConfig deceleration;
		[Category("1. SingleFacer SPEED"), PropertyOrder(50), Description("Decellerazione")]
		public ZoneSpeedConfig Deceleration
		{
			get { return deceleration; }
			set { deceleration = value; }
		}

		// *************************************  SINGLE FACER CARTRIDGE   ************************************
		Cartridge cartridge_000;
		[Category("2. SingleFacer CARTRIDGE"), PropertyOrder(10), Description("Cartridge_000")]
		public Cartridge Cartridge_000
		{
			get { return cartridge_000; }
			set { cartridge_000 = value; }
		}

		Cartridge cartridge_001;
		[Category("2. SingleFacer CARTRIDGE"), PropertyOrder(20), Description("Cartridge_001")]
		public Cartridge Cartridge_001
		{
			get { return cartridge_001; }
			set { cartridge_001 = value; }
		}

		Cartridge cartridge_010;
		[Category("2. SingleFacer CARTRIDGE"), PropertyOrder(30), Description("Cartridge_010")]
		public Cartridge Cartridge_010
		{
			get { return cartridge_010; }
			set { cartridge_010 = value; }
		}

		Cartridge cartridge_011;
		[Category("2. SingleFacer CARTRIDGE"), PropertyOrder(40), Description("Cartridge_011")]
		public Cartridge Cartridge_011
		{
			get { return cartridge_011; }
			set { cartridge_011 = value; }
		}

		Cartridge cartridge_100;
		[Category("2. SingleFacer CARTRIDGE"), PropertyOrder(50), Description("Cartridge_100")]
		public Cartridge Cartridge_100
		{
			get { return cartridge_100; }
			set { cartridge_100 = value; }
		}

		Cartridge cartridge_101;
		[Category("2. SingleFacer CARTRIDGE"), PropertyOrder(60), Description("Cartridge_101")]
		public Cartridge Cartridge_101
		{
			get { return cartridge_101; }
			set { cartridge_101 = value; }
		}

		Cartridge cartridge_110;
		[Category("2. SingleFacer CARTRIDGE"), PropertyOrder(70), Description("Cartridge_110")]
		public Cartridge Cartridge_110
		{
			get { return cartridge_110; }
			set { cartridge_110 = value; }
		}

		Cartridge cartridge_111;
		[Category("2. SingleFacer CARTRIDGE"), PropertyOrder(80), Description("Cartridge_111")]
		public Cartridge Cartridge_111
		{
			get { return cartridge_111; }
			set { cartridge_111 = value; }
		}

		// *************************************  SINGLE FACER WRAP ARMS   ************************************
		WrapArm preheaterEl;
		[Category("3. SingleFacer WRAP-ARMS"), PropertyOrder(10), Description("Pre-heater External Liner")]
		public WrapArm PreheaterEl
		{
			get { return preheaterEl; }
			set { preheaterEl = value; }
		}

		WrapArm preheaterIl;
		[Browsable(true), Category("3. SingleFacer WRAP-ARMS"), PropertyOrder(20), Description("Pre-heater Internal Liner")]
		public WrapArm PreheaterIl
		{
			get { return preheaterIl; }
			set { preheaterIl = value; }
		}

		WrapArm preheater1l;
		[Category("3. SingleFacer WRAP-ARMS"), PropertyOrder(30), Description("Pre-heater Optional Liner 1")]
		public WrapArm PreheaterOptional1l
		{
			get { return preheater1l; }
			set { preheater1l = value; }
		}

		WrapArm preheater2l;
		[Category("3. SingleFacer WRAP-ARMS"), PropertyOrder(40), Description("Pre-heater Optional Liner 2")]
		public WrapArm PreheaterOptional2l
		{
			get { return preheater2l; }
			set { preheater2l = value; }
		}

		WrapArm preheater3l;
		[Category("3. SingleFacer WRAP-ARMS"), PropertyOrder(50), Description("Pre-heater Optional Liner 3")]
		public WrapArm PreheaterOptional3l
		{
			get { return preheater3l; }
			set { preheater3l = value; }
		}

		WrapArm preheater4l;
		[Category("3. SingleFacer WRAP-ARMS"), PropertyOrder(60), Description("Pre-heater Optional Liner 4")]
		public WrapArm PreheaterOptional4l
		{
			get { return preheater4l; }
			set { preheater4l = value; }
		}

		WrapArm preheaterEm;
		[Category("3. SingleFacer WRAP-ARMS"), PropertyOrder(70), Description("Pre-heater External Medium")]
		public WrapArm PreheaterEm
		{
			get { return preheaterEm; }
			set { preheaterEm = value; }
		}

		WrapArm preheaterIm;
		[Category("3. SingleFacer WRAP-ARMS"), PropertyOrder(80), Description("Pre-heater Internal Medium")]
		public WrapArm PreheaterIm
		{
			get { return preheaterIm; }
			set { preheaterIm = value; }
		}

		WrapArm preheater1m;
		[Category("3. SingleFacer WRAP-ARMS"), PropertyOrder(90), Description("Pre-heater Optional Medium 1")]
		public WrapArm PreheaterOptional1m
		{
			get { return preheater1m; }
			set { preheater1m = value; }
		}

		WrapArm preheater2m;
		[Category("3. SingleFacer WRAP-ARMS"), PropertyOrder(100), Description("Pre-heater Optional Medium 2")]
		public WrapArm PreheaterOptional2m
		{
			get { return preheater2m; }
			set { preheater2m = value; }
		}

		WrapArm preheater3m;
		[Category("3. SingleFacer WRAP-ARMS"), PropertyOrder(110), Description("Pre-heater Optional Medium 3")]
		public WrapArm PreheaterOptional3m
		{
			get { return preheater3m; }
			set { preheater3m = value; }
		}

		WrapArm preheater4m;
		[Category("3. SingleFacer WRAP-ARMS"), PropertyOrder(110), Description("Pre-heater Optional Medium 4")]
		public WrapArm PreheaterOptional4m
		{
			get { return preheater4m; }
			set { preheater4m = value; }
		}


		// *************************************  SINGLE FACER GLUE GAP   ************************************
		GlueGap gluegap1;
		[Category("4. SingleFacer GLUE GAP"), PropertyOrder(10), Description("Gluegap")]
		public GlueGap Gluegap1
		{
			get { return gluegap1; }
			set { gluegap1 = value; }
		}

		// *************************************  SINGLE FACER GLUE GAP   ************************************
		SteamShower steamShower;
		[Category("5. SingleFacer STEAM SHOWER"), PropertyOrder(10), Description("Steam Shower")]
		public SteamShower SteamShower
		{
			get { return steamShower; }
			set { steamShower = value; }
		}

		// *************************************  SINGLE FACER MAIN STEAM   ************************************
		SFMainSteam sfMainSteam;
		[Category("6. SingleFacer MAIN STEAM"), PropertyOrder(10), Description("Main Steam")]
		public SFMainSteam SFMainSteam
		{
			get { return sfMainSteam; }
			set { sfMainSteam = value; }
		}

    //RFAZZI_FI07/07/2011_1. Aggiunta gestione di un numero di sezioni del Vapore per ogni singolo PreHeater. 
    // *************************************  SINGLE FACER WRAPARM STEAM   ************************************
    WrapArmSteam preheaterSteamEl;
    [Category("8. SingleFacer WrapArm Steam"), PropertyOrder(10), Description("Gestione Vapore Preriscaldatori")]
    public WrapArmSteam PreheaterSteamEl
    {
      get { return preheaterSteamEl; }
      set { preheaterSteamEl = value; }
    }

    WrapArmSteam preheaterSteamIl;
    [Category("8. SingleFacer WrapArm Steam"), PropertyOrder(10), Description("Gestione Vapore Preriscaldatori")]
    public WrapArmSteam PreheaterSteamIl
    {
      get { return preheaterSteamIl; }
      set { preheaterSteamIl = value; }
    }

    WrapArmSteam preheaterSteamEm;
    [Category("8. SingleFacer WrapArm Steam"), PropertyOrder(10), Description("Gestione Vapore Preriscaldatori")]
    public WrapArmSteam PreheaterSteamEm
    {
      get { return preheaterSteamEm; }
      set { preheaterSteamEm = value; }
    }

    WrapArmSteam preheaterSteamIm;
    [Category("8. SingleFacer WrapArm Steam"), PropertyOrder(10), Description("Gestione Vapore Preriscaldatori")]
    public WrapArmSteam PreheaterSteamIm
    {
      get { return preheaterSteamIm; }
      set { preheaterSteamIm = value; }
    }

		/// <summary>
		/// Builder.
		/// </summary>
		public SingleFacer(int numSf)
			: base()
		{
			sectionName = "SF_" + numSf.ToString();

      this.preHeaterAndGlueGapManualIfRemote = false;
			this.CrawlSpeed_UVmm_m = 0;
			this.MaxSpeed_UVmm_m = 0;
			this.SpeedTolerance_UVmm_m = 0;
			this.refusedMaxSetpoint_UVmm_m = 0;
			this.refusedMinSetpoint_UVmm_m = 0;
      this.extraPaperPathParaffinMachineLiner_UDmm = 0;
      this.extraPaperPathParaffinMachineMedium_UDmm = 0;

			this.acceleration = new ZoneSpeedConfig("AccelerationSF" + numSf.ToString());
			this.deceleration = new ZoneSpeedConfig("DecelerationSF" + numSf.ToString());

			this.cartridge_000 = new Cartridge("SF" + numSf.ToString(), "000");
			this.cartridge_001 = new Cartridge("SF" + numSf.ToString(), "001");
			this.cartridge_010 = new Cartridge("SF" + numSf.ToString(), "010");
			this.cartridge_011 = new Cartridge("SF" + numSf.ToString(), "011");
			this.cartridge_100 = new Cartridge("SF" + numSf.ToString(), "100");
			this.cartridge_101 = new Cartridge("SF" + numSf.ToString(), "101");
			this.cartridge_110 = new Cartridge("SF" + numSf.ToString(), "110");
			this.cartridge_111 = new Cartridge("SF" + numSf.ToString(), "111");

			int move = (numSf - 1) * 2; // 0, 2, 4
			this.preheaterEl = new WrapArm(13 + move);
			this.preheaterIl = new WrapArm(23 + move);
			this.preheater1l = new WrapArm(33 + move);
			this.preheater2l = new WrapArm(43 + move);
			this.preheater3l = new WrapArm(53 + move);
			this.preheater4l = new WrapArm(63 + move);

			this.preheaterEm = new WrapArm(12 + move);
			this.preheaterIm = new WrapArm(22 + move);
			this.preheater1m = new WrapArm(32 + move);
			this.preheater2m = new WrapArm(42 + move);
			this.preheater3m = new WrapArm(52 + move);
			this.preheater4m = new WrapArm(62 + move);
			
			int moveGlueGap = (numSf - 1) * 10; // 0, 10, 20
			this.gluegap1 = new GlueGap(11 + moveGlueGap);

			this.steamShower = new SteamShower(numSf);

			this.sfMainSteam = new SFMainSteam(numSf);

      //RFAZZI_FI07/07/2011_1. Aggiunta gestione di un numero di sezioni del Vapore per ogni singolo PreHeater. 
      this.preheaterSteamEl = new WrapArmSteam(13 + move);
      this.preheaterSteamIl = new WrapArmSteam(23 + move);
      this.preheaterSteamEm = new WrapArmSteam(12 + move);
      this.preheaterSteamIm = new WrapArmSteam(22 + move);
    }
	}
}
