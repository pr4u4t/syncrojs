using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	/// <summary>
	/// Bridge configuration.
	/// </summary>
	public class Bridge : SyncroC.DataBase.LineConfig.LineBase
	{
		//Bridge number
		private int number;

		int B_lowEyeOnStableTicks;						// Period that low eye must be in steady state before considered to be stable
    [PropertyOrder(30), Category("Bridge"), Description("Periodo ( in msec ) che la fotocellula deve rimanere impegnata prima che Syncro la assumi ad ON.")]
		public int B_LowEyeOnStableTicks
		{
			get { return B_lowEyeOnStableTicks; }
			set { B_lowEyeOnStableTicks = value; }
		}

		int B_highEyeOnStableTicks;						// Period that high eye must be in steady state before considered to be stable
    [PropertyOrder(40), Category("Bridge"), Description("Periodo ( in msec ) che la fotocellula deve rimanere impegnata prima che Syncro la assumi ad ON.")]
		public int B_HighEyeOnStableTicks
		{
			get { return B_highEyeOnStableTicks; }
			set { B_highEyeOnStableTicks = value; }
		}

		int B_lowEyeOffStableTicks;						// Period that low eye must be in steady state before considered to be stable
    [PropertyOrder(50), Category("Bridge"), Description("Periodo ( in msec ) che la fotocellula deve rimanere impegnata prima che Syncro la assumi ad OFF.")]
		public int B_LowEyeOffStableTicks
		{
			get { return B_lowEyeOffStableTicks; }
			set { B_lowEyeOffStableTicks = value; }
		}

		int B_highEyeOffStableTicks;					// Period that high eye must be in steady state before considered to be stable
    [PropertyOrder(60), Category("Bridge"), Description("Periodo ( in msec ) che la fotocellula deve rimanere impegnata prima che Syncro la assumi ad OFF.")]
		public int B_HighEyeOffStableTicks
		{
			get { return B_highEyeOffStableTicks; }
			set { B_highEyeOffStableTicks = value; }
		}

		int B_eyeResetTolerance_UDmm;					// Amount contents can be out when check whether to make a contents reset on the eyes
    [PropertyOrder(70), Category("Bridge"), Description("Tolleranza fra il contenuto attuale del ponte e quello previsto ed usato nel momento in cui la fotocellula di 'Ponte Pieno' diventa stabile ad ON per determinare se sovrascrivere il contenuto attuale.")]
		public int B_EyeResetTolerance_UDmm
		{
			get { return B_eyeResetTolerance_UDmm; }
			set { B_eyeResetTolerance_UDmm = value; }
		}

    //PMASSARO_FI15/09/11 - Aggiunta variabile per discriminare la fermata in F-stop del DB in caso di poca scorta ponte
    bool B_useFStopToDBWhenBridgeLow;
    [PropertyOrder(71), Category("Bridge"), Description("Settare a TRUE se le rampe di N-Stop del DB sono troppo lente e quindi si strappa la carta quando fermiamo per 'Poca Scorta Ponte'")]
    public bool B_UseFStopToDBWhenBridgeLow
    {
      get { return B_useFStopToDBWhenBridgeLow; }
      set { B_useFStopToDBWhenBridgeLow = value; }
    }

    //PMASSARO_FI08/01/10 - Put as parameter the minimum paper quantity value desidered on bridge before decrease DB speed if Low photoeye is OFF    
    int B_minContentWhenLowPhotoeyeIsOff_UDmm;					//Minimum paper quantity value desidered on bridge before decrease DB speed if Low photoeye is OFF
    //[PropertyOrder(75), Category("Bridge"), Description("Usefull when minimun photoeye is not so near to the SF: It means the minimum content that must be on bridge to decrease DB speed (just when Minimum photoeye is OFF)")]
    [PropertyOrder(75), Category("Bridge"), Description("Utile quando la Fotoc. di minimo Non è vicina al SF: è il Min. contenuto che ci deve essere sul ponte prima di decellerare il DB (solo in caso di fotoc. minima scoperta)")]
    public int B_MinContentWhenLowPhotoeyeIsOff_UDmm
    {
      get { return B_minContentWhenLowPhotoeyeIsOff_UDmm; }
      set { B_minContentWhenLowPhotoeyeIsOff_UDmm = value; }
    }

		int B_sFStopContents_UDmm;						// If DB is stopped and contents exceed this value then SF will be stopped (0 disables)
    [PropertyOrder(80), Category("Bridge"), Description("Contenuto Massimo del ponte per fermare il Gruppo Ondulatore se i Piani Caldi sono già fermi ( 0 = disabilitato ).")]
		public int B_SFStopContents_UDmm
		{
			get { return B_sFStopContents_UDmm; }
			set { B_sFStopContents_UDmm = value; }
		}

		int B_contentsTolerance_UDmm;					// Amounts contents can drift before control is exerted
		[PropertyOrder(90), Category("Bridge"), Description("Amounts contents can drift before control is exerted.")]
		public int B_ContentsTolerance_UDmm
		{
			get { return B_contentsTolerance_UDmm; }
			set { B_contentsTolerance_UDmm = value; }
		}

		int B_nudgeSpeedChange_UVmm_m;				// Speed change to use when controlling bridge with nudge
    [PropertyOrder(100), Category("Bridge"), Description("Variazione di velocità da applicare al Gruppo Ondulatore quando il contenuto del ponte è vicino al quantitativo richiesto.")]
		public int B_NudgeSpeedChange_UVmm_m
		{
			get { return B_nudgeSpeedChange_UVmm_m; }
			set { B_nudgeSpeedChange_UVmm_m = value; }
		}

		int B_overfillContentsDiff_UDmm;			// Difference between required and actual contents to give an overfill empty
    [PropertyOrder(110), Category("Bridge"), Description("Differenza di contenuto ponte oltre la quale si applica la variazione di velocità B_OverFillSpeedChange_UVmm_m.")]
		public int B_OverfillContentsDiff_UDmm
		{
			get { return B_overfillContentsDiff_UDmm; }
			set { B_overfillContentsDiff_UDmm = value; }
		}

		int B_speedChange_UVmm_m;							// Speed change to use when controlling bridge
    [PropertyOrder(120), Category("Bridge"), Description("Variazione di velocità da applicare al Gruppo Ondulatore quando il contenuto del ponte è abbastanza diverso dal valore richiesto.")]
		public int B_SpeedChange_UVmm_m
		{
			get { return B_speedChange_UVmm_m; }
			set { B_speedChange_UVmm_m = value; }
		}

    int B_overFillSpeedChange_UVmm_m;				// Speed change to use when controlling bridge and bridge is filled past max eye	
    [PropertyOrder(120), Category("Bridge"), Description("Variazione di velocità da applicare al Gruppo Ondulatore quando si richiede di riempire rapidamente il ponte.")]
		public int B_OverFillSpeedChange_UVmm_m
		{
			get { return B_overFillSpeedChange_UVmm_m; }
			set { B_overFillSpeedChange_UVmm_m = value; }
		}

		int B_distanceRollToCalStart_UDmm;		// Fixed distance from corrugator roll to calibration start point
    [PropertyOrder(122), Category("Bridge"), Description("Distanza ( in mm ) fra il cilindro ondulatore e il sistema di calibrazione del ponte.")]
		public int B_DistanceRollToCalStart_UDmm
		{
			get { return B_distanceRollToCalStart_UDmm; }
			set { B_distanceRollToCalStart_UDmm = value; }
		}

		int B_distanceCalStopToDB_UDmm;				// Distance from Cal stop sensor to DB - need wrap arm compensation if sensor before wrap arms
    [PropertyOrder(125), Category("Bridge"), Description("Distanza in mm dal sensore di calibrazione all’inizio dei piani con tutti i fasciatori al minimo.")]
		public int B_DistanceCalStopToDB_UDmm
		{
			get { return B_distanceCalStopToDB_UDmm; }
			set { B_distanceCalStopToDB_UDmm = value; }
		}

    //RFAZZI_FI19/07/2010_1. Aggiunta gestione per passaggio carta particolare per carta Cannetè. 
    int B_extraDistanceForCannetePaperCalStopToDB_UDmm;				// Extra Distance from Cal stop sensor to DB when a cannetè paper is present
    [PropertyOrder(126), Category("Bridge"), Description("Extra distance from Cal Stop to DB - needed to adjust the distance with Cannetè board")]
    public int B_ExtraDistanceForCannetePaperCalStopToDB_UDmm
    {
      get { return B_extraDistanceForCannetePaperCalStopToDB_UDmm; }
      set { B_extraDistanceForCannetePaperCalStopToDB_UDmm = value; }
    }

    //RFAZZI_FI19/07/2010_2. Aggiunta gestione per passaggio carta particolare per carta Cannetè attraverso WrapArm11. 
    int B_extraDistanceForCannetePaperCalStopToDBAcrossWrap11_UDmm;				// Extra Distance from Cal stop sensor to DB when a cannetè paper is present and running across Wrap11
    [PropertyOrder(127), Category("Bridge"), Description("Extra distance from Cal Stop to DB - needed to adjust the distance with Cannetè board and running across Wrap11")]
    public int B_ExtraDistanceForCannetePaperCalStopToDBAcrossWrap11_UDmm
    {
      get { return B_extraDistanceForCannetePaperCalStopToDBAcrossWrap11_UDmm; }
      set { B_extraDistanceForCannetePaperCalStopToDBAcrossWrap11_UDmm = value; }
    }

		bool C_calibrationEnabled;						// TRUE if calibration system is enabled
		[PropertyOrder(130), Category("Calibration"), Description("se TRUE la calibrazione del ponte è abilitata")]
		public bool C_CalibrationEnabled
		{
			get { return C_calibrationEnabled; }
			set { C_calibrationEnabled = value; }
		}

		int C_exitDeviceDelayTicks;						// Reaction time for sensing device
    [PropertyOrder(150), Category("Calibration"), Description("Ritardo indotto dal sensore di rilevazione della calibrazione.")]
		public int C_ExitDeviceDelayTicks
		{
			get { return C_exitDeviceDelayTicks; }
			set { C_exitDeviceDelayTicks = value; }
		}

		int C_calibrateMinContents_UDmm;			// Minimum valid calibration contents
    [PropertyOrder(160), Category("Calibration"), Description("Minimo contenuto del ponte ( in mm ) quando la calibrazione è attiva.")]
		public int C_CalibrateMinContents_UDmm
		{
			get { return C_calibrateMinContents_UDmm; }
			set { C_calibrateMinContents_UDmm = value; }
		}

		int C_calibrateMaxContents_UDmm;			// Maximum valid calibration contents
    [PropertyOrder(170), Category("Calibration"), Description("Massimo contenuto del ponte ( in mm ) quando la calibrazione è attiva.")]
		public int C_CalibrateMaxContents_UDmm
		{
			get { return C_calibrateMaxContents_UDmm; }
			set { C_calibrateMaxContents_UDmm = value; }
		}

		int C_calibrateContentsWindow_UDmm; 	// Tolerance within which a contents is accepted
    [PropertyOrder(180), Category("Calibration"), Description("Finestra all'interno della quale ci si aspetta di rilevare la calibrazione per essere accettata.")]
		public int C_CalibrateContentsWindow_UDmm
		{
			get { return C_calibrateContentsWindow_UDmm; }
			set { C_calibrateContentsWindow_UDmm = value; }
		}

		int C_calibrationIntervalTicks;				// Ticks between calibrate attempts
    [PropertyOrder(190), Category("Calibration"), Description("Intervallo di tempo fra due consecutive calibrazioni del ponte (in msec).")]
		public int C_CalibrationIntervalTicks
		{
			get { return C_calibrationIntervalTicks; }
			set { C_calibrationIntervalTicks = value; }
		}

		int C_minCalibrateSpeed_UVmm_m;				// Must be running faster than this to start a calibrate
    [PropertyOrder(200), Category("Calibration"), Description("Velocità minima per eseguire la calibrazione del ponte.")]
		public int C_MinCalibrateSpeed_UVmm_m
		{
			get { return C_minCalibrateSpeed_UVmm_m; }
			set { C_minCalibrateSpeed_UVmm_m = value; }
		}

		bool C_disableCalUntilProgramEnd;			// If TRUE then disables the calibration during normal running and resumes again before the end of a program for sync splice purposes
    [PropertyOrder(210), Category("Calibration"), Description("se TRUE la calibrazione del ponte viene eseguita quando i metri mancanti a cambio qualità è inferiore al valore del parametro C_CalResumeDistance_UDmm")]
		public bool C_DisableCalUntilProgramEnd
		{
			get { return C_disableCalUntilProgramEnd; }
			set { C_disableCalUntilProgramEnd = value; }
		}

		int C_calResumeDistance_UDmm;					// Distance before the end of a program to resume calibration if the option is selected to cancel calibration during running.
    [PropertyOrder(220), Category("Calibration"), Description("Distanza ( in mm ) al di sotto della quale si eseguono le calibrazioni del ponte.")]
		public int C_CalResumeDistance_UDmm
		{
			get { return C_calResumeDistance_UDmm; }
			set { C_calResumeDistance_UDmm = value; }
		}

		int C_calEntryDeviceDelayTicks;				// Reaction time for marking device
    [PropertyOrder(230), Category("Calibration"), Description("Ritardo ( in msec ) fra l comando di Calibrazione e la reale marcatura del cartone.")]
		public int C_CalEntryDeviceDelayTicks
		{
			get { return C_calEntryDeviceDelayTicks; }
			set { C_calEntryDeviceDelayTicks = value; }
		}

		int C_calibrationStartPulseLength_UDmm;	// Used to calculate ticks that start output is pulsed for
    [PropertyOrder(240), Category("Calibration"), Description("Lunghezza ( in mm ) della marcatura del cartone desiderata ( usato se il parametro C_CalibrationStartPulseTicks = 0 ) .")]
		public int C_CalibrationStartPulseLength_UDmm
		{
			get { return C_calibrationStartPulseLength_UDmm; }
			set { C_calibrationStartPulseLength_UDmm = value; }
		}

		int C_calibrationStartPulseTicks;			// Ticks that start output is pulsed for LATER check if still used
    [PropertyOrder(250), Category("Calibration"), Description("Tempo ( in msec ) per il quale si esegue la marcatura del cartone ( = 0 disabilitato e si usa il parametro C_CalibrationStartPulseLength_UDmm )")]
		public int C_CalibrationStartPulseTicks
		{
			get { return C_calibrationStartPulseTicks; }
			set { C_calibrationStartPulseTicks = value; }
		}

    //ABELLO_FI05/07/10 Aggiunta gestione del ponte con sensore di temperatura
    double C_triggerDifferenceAMC;    // Reading difference that triggers a calibrate
    //PMASSARO_FI12/09/11 - Rivista descrizione del parametro 'C_TriggerDifferenceAMC'
    //[PropertyOrder(260), Category("Calibration"), Description("Reading difference that triggers a calibrate (Di solito 0.20 - 0.25)")]
    [PropertyOrder(260), Category("Calibration"), Description("Variaz. (Volt) per identificare la spruzzata (Di solito: scala 0-5V => 0.20 - 0.25.  0-10V => 0.40 - 0.50)")]
    public double C_TriggerDifferenceAMC
    {
      get { return C_triggerDifferenceAMC; }
      set { C_triggerDifferenceAMC = value; }
    }

		/***********************************************************************************
		 *                  PAPER TENSION CONTROL
		 **********************************************************************************/
		//RFAZZI_FI21/01/2009_3.Added Paper Tension into Bridge Monitor Data. 
		PaperTension paperTension; //Paper Tension
		[Category("PaperTension"), PropertyOrder(260), Description("Tensione Carta")]
		public PaperTension PaperTension
		{
			get { return paperTension; }
			set { paperTension = value; }
		}

		/// <summary>
		/// Builder.
		/// </summary>
		/// <param name="number">1 for SF1, 2 for SF2, 3 for SF3.</param>
		public Bridge(int number)
			: base()
		{
			sectionName = "BRIDGE_" + (number).ToString();
			this.number = number;

			this.B_lowEyeOnStableTicks = 0;
			this.B_highEyeOnStableTicks = 0;
			this.B_lowEyeOffStableTicks = 0;
			this.B_highEyeOffStableTicks = 0;
			this.B_eyeResetTolerance_UDmm = 0;
			this.B_SFStopContents_UDmm = 0;
			this.B_contentsTolerance_UDmm = 0;
			this.B_nudgeSpeedChange_UVmm_m = 0;
			this.B_overfillContentsDiff_UDmm = 0;
			this.B_speedChange_UVmm_m = 0;
			this.B_distanceRollToCalStart_UDmm = 0;
			this.B_distanceCalStopToDB_UDmm = 0;
      //RFAZZI_FI19/07/2010_1. Aggiunta gestione per passaggio carta particolare per carta Cannetè. 
      this.B_extraDistanceForCannetePaperCalStopToDB_UDmm = 0;
      //RFAZZI_FI19/07/2010_2. Aggiunta gestione per passaggio carta particolare per carta Cannetè attraverso WrapArm11. 
      this.B_extraDistanceForCannetePaperCalStopToDBAcrossWrap11_UDmm = 0;				// Extra Distance from Cal stop sensor to DB when a cannetè paper is present and running across Wrap11

			this.C_calibrationEnabled = false;
			this.C_exitDeviceDelayTicks = 0;
			this.C_calibrateMinContents_UDmm = 0;
			this.C_calibrateMaxContents_UDmm = 0;
			this.C_calibrateContentsWindow_UDmm = 0;
			this.C_calibrationIntervalTicks = 0;
			this.C_minCalibrateSpeed_UVmm_m = 0;
			this.C_disableCalUntilProgramEnd = false;
			this.C_calResumeDistance_UDmm = 0;
			this.C_calEntryDeviceDelayTicks = 0;
			this.C_calibrationStartPulseLength_UDmm = 0;
			this.C_calibrationStartPulseTicks = 0;
      this.C_triggerDifferenceAMC = 0.25;

			//RFAZZI_FI21/01/2009_3.Added Paper Tension into Bridge Monitor Data. 
			this.paperTension = new PaperTension(number+20);
		}
	}
}
