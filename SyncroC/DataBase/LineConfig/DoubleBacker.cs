using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	[TypeConverter(typeof(Converters))]
	public class ZoneSpeedConfig : SyncroC.DataBase.LineConfig.LineBase
	{

		private int speedBand_UVmm_m;					// Speed band when decelerating
    [PropertyOrder(10), Category("ZoneSpeedConfig"), Description("Durante la fase di variazione della velocità, se la differenza fra la velocità attuale e richiesta è superiore a questo parametro, il comando di Incrementa/Decrementa viene dato come da parametri 'PulseWidthOnTicks' / 'PulseWidthOffTicks'. La dimensione di questa banda dipende dalla capacità della macchina di seguire in modo 'pronto' i comandi e si raccomanda di tenere il valore abbastanza basso.")]
		public int SpeedBand_UVmm_m
		{
			get { return speedBand_UVmm_m; }
			set { speedBand_UVmm_m = value; }
		}

		private double bandFactor;
    [PropertyOrder(20), Category("ZoneSpeedConfig"), Description("Rampa di Accelerazione/Decelerazione che la macchina segue quando siamo all'interno della banda di velocità.")]
		public double BandFactor
		{
			get { return bandFactor; }
			set { bandFactor = value; }
		}

		private double factor;								// Engineering
    [PropertyOrder(30), Category("ZoneSpeedConfig"), Description("Rampa di Accelerazione/Decelerazione che la macchina segue quando siamo al di fuori della banda di velocità.")]
		public double Factor
		{
			get { return factor; }
			set { factor = value; }
		}

		private int pulseWidthOnTicks;				// Pulse width for outputs when contolling outside speed band
    [PropertyOrder(40), Category("ZoneSpeedConfig"), Description("Periodo che viene tenuto Alto il segnale di variazione velocità quando siamo al di fuori della banda di velocità. La somma dei tempi dei due parametri deve essere = 1000 ( 1 sec ). BandPulseWidthOnTicks + BandPulseWidthOffTicks is 1000 = 1 sec")]
		public int PulseWidthOnTicks
		{
			get { return pulseWidthOnTicks; }
			set { pulseWidthOnTicks = value; }
		}

		private int pulseWidthOffTicks;
    [PropertyOrder(50), Category("ZoneSpeedConfig"), Description("Periodo che viene tenuto Basso il segnale di variazione velocità quando siamo al di fuori della banda di velocità. La somma dei tempi dei due parametri deve essere = 1000 ( 1 sec ). BandPulseWidthOnTicks + BandPulseWidthOffTicks is 1000 = 1 sec")]
		public int PulseWidthOffTicks
		{
			get { return pulseWidthOffTicks; }
			set { pulseWidthOffTicks = value; }
		}

		private int bandPulseWidthOnTicks;		// Pulse width for outputs when contolling inside speed band
    [PropertyOrder(60), Category("ZoneSpeedConfig"), Description("Periodo che viene tenuto Alto il segnale di variazione velocità quando siamo all'interno della banda di velocità. La somma dei tempi dei due parametri deve essere = 1000 ( 1 sec ). BandPulseWidthOnTicks + BandPulseWidthOffTicks is 1000 = 1 sec")]
		public int BandPulseWidthOnTicks
		{
			get { return bandPulseWidthOnTicks; }
			set { bandPulseWidthOnTicks = value; }
		}

		private int bandPulseWidthOffTicks;
    [PropertyOrder(70), Category("ZoneSpeedConfig"), Description("Periodo che viene tenuto Basso il segnale di variazione velocità quando siamo all'interno della banda di velocità. La somma dei tempi dei due parametri deve essere = 1000 ( 1 sec ). BandPulseWidthOnTicks + BandPulseWidthOffTicks is 1000 = 1 sec")]
		public int BandPulseWidthOffTicks
		{
			get { return bandPulseWidthOffTicks; }
			set { bandPulseWidthOffTicks = value; }
		}

		public ZoneSpeedConfig(string name)
			: base()
		{

			this.sectionName = name;

			this.speedBand_UVmm_m = 0;
			this.bandFactor = 0;
			this.factor = 0;
			this.pulseWidthOnTicks = 0;
			this.pulseWidthOffTicks = 0;
			this.bandPulseWidthOnTicks = 0;
			this.bandPulseWidthOffTicks = 0;

		}
	}

	/// <summary>
	/// Summary description for DoubleBacker.
	/// </summary>
	public class DoubleBacker : SyncroC.DataBase.LineConfig.LineBase
	{
		// *************************************  DOUBLE BACKER SPEED   ************************************
		int crawlSpeed_UVmm_m; // Minimum speed for the unit
    [Category("1. DoubleBacker SPEED"), PropertyOrder(10), Description("Velocità a cui portare la macchina ( mm/min ) ad esempio nei casi in cui la giuntatrice 1 non è pronta alla giunta.")]
		public int CrawlSpeed_UVmm_m
		{
			get { return crawlSpeed_UVmm_m; }
			set { crawlSpeed_UVmm_m = value; }
		}

		int speedTolerance_UVmm_m;					// Speed within which speed is not adjusted
    [Category("1. DoubleBacker SPEED"), PropertyOrder(20), Description("Tolleranza di velocità utilizzata durante la fase di raggiungimento del target previsto ( mm/mt ).")]
		public int SpeedTolerance_UVmm_m
		{
			get { return speedTolerance_UVmm_m; }
			set { speedTolerance_UVmm_m = value; }
		}

		int maxSpeed_UVmm_m;								// Absolute max speed of machine
    [Category("1. DoubleBacker SPEED"), PropertyOrder(30), Description("Massima velocità a cui la macchina può andare ( mm/mt ).")]
		public int MaxSpeed_UVmm_m
		{
			get { return maxSpeed_UVmm_m; }
			set { maxSpeed_UVmm_m = value; }
		}

		ZoneSpeedConfig acceleration;
		[Category("1. DoubleBacker SPEED"), PropertyOrder(40), Description("Accellerazione")]
		public ZoneSpeedConfig Acceleration
		{
			get { return acceleration; }
			set { acceleration = value; }
		}

		ZoneSpeedConfig deceleration;
		[Category("1. DoubleBacker SPEED"), PropertyOrder(50), Description("Decellerazione")]
		public ZoneSpeedConfig Deceleration
		{
			get { return deceleration; }
			set { deceleration = value; }
		}


		// *************************************  DOUBLE BACKER WRAP ARMS   ************************************
		WrapArm preheater31;
		[Category("2. DoubleBacker WRAP-ARMS"), PropertyOrder(10), Description("Preheater31")]
		public WrapArm Preheater31
		{
			get { return preheater31; }
			set { preheater31 = value; }
		}

		WrapArm preheater41;
		[Category("2. DoubleBacker WRAP-ARMS"), PropertyOrder(20), Description("Preheater41")]
		public WrapArm Preheater41
		{
			get { return preheater41; }
			set { preheater41 = value; }
		}

		// *************************************  DOUBLE BACKER HOT SECTIONS   ************************************
		DbHotSection hotSection1;	//section 1
		[PropertyOrder(50), Category("3. DoubleBacker Hot Section"), Description("Hot Section 1.")]
		public DbHotSection HotSection1
		{
			get { return this.hotSection1; }
			set { this.hotSection1 = value; }
		}
		DbHotSection hotSection2;	//section 2
		[PropertyOrder(60), Category("3. DoubleBacker Hot Section"), Description("Hot Section 2.")]
		public DbHotSection HotSection2
		{
			get { return this.hotSection2; }
			set { this.hotSection2 = value; }
		}
		DbHotSection hotSection3;	//section 3
		[PropertyOrder(70), Category("3. DoubleBacker Hot Section"), Description("Hot Section 3.")]
		public DbHotSection HotSection3
		{
			get { return this.hotSection3; }
			set { this.hotSection3 = value; }
		}
		DbHotSection hotSection4;	//section 4
		[PropertyOrder(80), Category("3. DoubleBacker Hot Section"), Description("Hot Section 4.")]
		public DbHotSection HotSection4
		{
			get { return this.hotSection4; }
			set { this.hotSection4 = value; }
		}

		// *************************************  DOUBLE BACKER COLD SECTIONS   ************************************
		DbColdSection coldSection1;	//section 1
		[PropertyOrder(50), Category("4. DoubleBacker Cold Section"), Description("Cold Section 1.")]
		public DbColdSection ColdSection1
		{
			get { return this.coldSection1; }
			set { this.coldSection1 = value; }
		}

		// *************************************  DOUBLE BACKER COLD SECTIONS   ************************************
		DbSteamGroup steamGroup1;	//section 1
		[PropertyOrder(50), Category("5. DoubleBacker Steam Group"), Description("Steam Group 1.")]
		public DbSteamGroup SteamGroup1
		{
			get { return this.steamGroup1; }
			set { this.steamGroup1 = value; }
		}
		DbSteamGroup steamGroup2;	//section 2
		[PropertyOrder(60), Category("5. DoubleBacker Steam Group"), Description("Steam Group 2.")]
		public DbSteamGroup SteamGroup2
		{
			get { return this.steamGroup2; }
			set { this.steamGroup2 = value; }
		}
		DbSteamGroup steamGroup3;	//section 3
		[PropertyOrder(70), Category("5. DoubleBacker Steam Group"), Description("Steam Group 3.")]
		public DbSteamGroup SteamGroup3
		{
			get { return this.steamGroup3; }
			set { this.steamGroup3 = value; }
		}
		DbSteamGroup steamGroup0;	//section 0
		[PropertyOrder(80), Category("5. DoubleBacker Steam Group"), Description("Steam Group 0.")]
		public DbSteamGroup SteamGroup0
		{
			get { return this.steamGroup0; }
			set { this.steamGroup0 = value; }
		}

    // *************************************  DOUBLE BACKER PARAFFIN MACHINE   ************************************
    int extraPaperPathParaffinMachineLiner_UDmm;
    [Category("6. Paraffin Machine"), PropertyOrder(10), Description("Quantitativo di carta da incrementare nelle giunte sincronizzate quando è presente il dispositivo. Se = 0 il dispositivo non è presente nella zona. Quando invece è presente e in lavoro, deve essere valorizzato questo parametro con un valore diverso da zero ed abilitato sulla grafica attraverso il pulsante specifico presente nella pagina di stato della macchina.")]
    public int ExtraPaperPathParaffinMachineLiner_UDmm
    {
      get { return extraPaperPathParaffinMachineLiner_UDmm; }
      set { extraPaperPathParaffinMachineLiner_UDmm = value; }
    }

    // *************************************  DoubleBacker Steam Shower   ************************************
    SteamShower steamShower;
    [Category("7. DoubleBacker STEAM SHOWER"), PropertyOrder(10), Description("Steam Shower")]
    public SteamShower SteamShower
    {
      get { return steamShower; }
      set { steamShower = value; }
    }

		/// <summary>
		/// Builder.
		/// </summary>
		public DoubleBacker()
			: base()
		{
			this.sectionName = "DOUBLE_BACKER";
			this.CrawlSpeed_UVmm_m = 0;
			this.MaxSpeed_UVmm_m = 0;
			this.SpeedTolerance_UVmm_m = 0;
      this.extraPaperPathParaffinMachineLiner_UDmm = 0;
			this.acceleration = new ZoneSpeedConfig("Acceleration");
			this.deceleration = new ZoneSpeedConfig("Deceleration");
			this.preheater31 = new WrapArm(31);
			this.preheater41 = new WrapArm(41);

			this.hotSection1 = new DbHotSection(1);
			this.hotSection2 = new DbHotSection(2);
			this.hotSection3 = new DbHotSection(3);
			this.hotSection4 = new DbHotSection(4);

			this.coldSection1 = new DbColdSection(1);

			this.steamGroup1 = new DbSteamGroup(1);
			this.steamGroup2 = new DbSteamGroup(2);
			this.steamGroup3 = new DbSteamGroup(3);
			this.steamGroup0 = new DbSteamGroup(0);

      this.steamShower = new SteamShower(0);
		}
	}
}