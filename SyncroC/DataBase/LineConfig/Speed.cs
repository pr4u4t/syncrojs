using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	/// <summary>
	/// Summary description for SPEED
	/// </summary>
	/// 
  [TypeConverter(typeof(Converters))]
  public class SPEED : SyncroC.DataBase.LineConfig.LineBase
	{
		int wetEndMinSpeed_UVmm_m;
		int dryEndMinSpeed_UVmm_m;
		//RFazzi_FI31/05/2007_2.Removed DB Speed Parameters into Speed Settings using the parameters present into DB Section
		//int speedTolerance_UVmm_m;
		//int speedControlBand_UVmm_m;
		int cruiseControlTolerance_UVmm_m;
		int maintainLineRun_UTms;
		//RFazzi_FI31/05/2007_2.Removed DB Speed Parameters into Speed Settings using the parameters present into DB Section
		//int	accelerationPulseWidthOn_UTms;		// are pulsed to gradually approach a speed
		//int	accelerationPulseWidthOff_UTms;	// rather then being held on 
		//int	decelerationPulseWidthOn_UTms;
		//int	decelerationPulseWidthOff_UTms;
		int autoSpeedDelay_UTms;
		int deJogSpeed_UVmm_m;
		int deJogAccDecRamp_UVmm_m;
		int webAtMWDebounceDistance_UDmm;

		//RFazzi_FI31/05/2007_2.Removed DB Speed Parameters into Speed Settings using the parameters present into DB Section
		//double dbDecelerationRamp_UVmm_m;				//DB Decelaration ramp mm/m/s
		double dbSlowDownOffset_UTs;				// When in auto gives the slowdown a fixed minimum time to allow reaction of double backer etc when running just over AOC speed

		bool limitKnifeSpeed;					//Enable control of Knife speed curve
		bool alwaysGiveReference;			// TRUE forces output even when line run input drops off
		bool useMeasuringWheelWhenValid;	// TRUE if MW encoder is used when possible
		bool adcPresentDB;				// TRUE allows speed ref from ADC
		bool useADC;						// TRUE if the ADC should be used
		bool mwBeforeRSH;				// TRUE indicates the MW is before the RSH		//CC 04/08/04 Alignment with Escada Syncro 2v0.02
		bool disableAutoSpeedOnWetEndChange;
		bool disableAutoSpeedOnFluteChange;

		public SPEED(): base()
		{
			this.sectionName = "SPEED";
			this.wetEndMinSpeed_UVmm_m = 15000;
			this.dryEndMinSpeed_UVmm_m = 15000;
			//RFazzi_FI31/05/2007_2.Removed DB Speed Parameters into Speed Settings using the parameters present into DB Section
			//this.speedTolerance_UVmm_m = 2000;
			//this.speedControlBand_UVmm_m = 5000;
			this.cruiseControlTolerance_UVmm_m = 10000;
			this.maintainLineRun_UTms = 0;
			//RFazzi_FI31/05/2007_2.Removed DB Speed Parameters into Speed Settings using the parameters present into DB Section
			//this.accelerationPulseWidthOn_UTms = 750;		// are pulsed to gradually approach a speed
			//this.accelerationPulseWidthOff_UTms = 1500;	// rather then being held on 
			//this.decelerationPulseWidthOn_UTms = 750;
			//this.decelerationPulseWidthOff_UTms = 1500;
			this.autoSpeedDelay_UTms = 0;
			this.deJogSpeed_UVmm_m = 30000;
			this.deJogAccDecRamp_UVmm_m = 1000;
			this.webAtMWDebounceDistance_UDmm = 3000;

			//RFazzi_FI31/05/2007_2.Removed DB Speed Parameters into Speed Settings using the parameters present into DB Section
			//this.dbDecelerationRamp_UVmm_m = 5000.0;				//DB Decelaration ramp mm/m/s
			this.dbSlowDownOffset_UTs = 0.0;				// When in auto gives the slowdown a fixed minimum time to allow reaction of double backer etc when running just over AOC speed

			this.limitKnifeSpeed = true;					//Enable control of Knife speed curve
			this.alwaysGiveReference = true;			// TRUE forces output even when line run input drops off
			this.useMeasuringWheelWhenValid = true;	// TRUE if MW encoder is used when possible
			this.ADCPresentDB = false;				// TRUE allows speed ref from ADC
			this.useADC = false;						// TRUE if the ADC should be used
			this.MWBeforeRSH = false;				// TRUE indicates the MW is before the RSH		//CC 04/08/04 Alignment with Escada Syncro 2v0.02
			this.disableAutoSpeedOnFluteChange = true;
			this.disableAutoSpeedOnWetEndChange = true;
		}

		// *************************************  SPEED SETTINGS  ***********************************
		[Category("1. SPEED SETTINGS"), PropertyOrder(10), Description("Corrisponde alla minima velocità (in mm/min) dei piani caldi in fase di passaggio carta o comunque al momento in cui sono in marcia. Questa informazione deve essere fornita dal cliente nei casi in cui il DB non sia Fosber.")]
		public int WetEndMinSpeed_UVmm_m
		{
			get { return this.wetEndMinSpeed_UVmm_m; }
			set { this.wetEndMinSpeed_UVmm_m = value; }
		}

    [Category("1. SPEED SETTINGS"), PropertyOrder(20), Description("Corrisponde alla minima velocità (in mm/min) a cui far marciare il DryEnd quando si riceve il segnale di 'Piani in Marcia'. In caso in cui la velocità dei piani sia superiore a questo valore, la velocità data alle macchine sarà uguale a quella dei piani")]
		public int DryEndMinSpeed_UVmm_m
		{
			get { return this.dryEndMinSpeed_UVmm_m ; }
			set { this.dryEndMinSpeed_UVmm_m  = value; }
		}

    [Category("1. SPEED SETTINGS"), PropertyOrder(25), Description("Corrisponde alla velocità (in mm/min) data alle macchine del DryEnd quando viene premuto il pulsante di jog. Qualora una delle macchine del DryEnd non sia pronta alla marcia, il riferimento non verrà fornito.")]
		public int DEJogSpeed_UVmm_m
		{
			get { return this.deJogSpeed_UVmm_m; }
			set { this.deJogSpeed_UVmm_m = value; }
		}

		//RFazzi_FI31/05/2007_2.Removed DB Speed Parameters into Speed Settings using the parameters present into DB Section
		//[Category("1. SPEED SETTINGS"), PropertyOrder(30), Description("Speed Tolerance ( mm/min )")]
		//public int SpeedTolerance_UVmm_m
		//{
		//  get { return this.speedTolerance_UVmm_m; }
		//  set { this.speedTolerance_UVmm_m = value; }
		//}

		//RFazzi_FI31/05/2007_2.Removed DB Speed Parameters into Speed Settings using the parameters present into DB Section
		//[Category("1. SPEED SETTINGS"), PropertyOrder(40), Description("Speed Control Band ( mm/min )")]
		//public int SpeedControlBand_UVmm_m
		//{
		//  get { return this.speedControlBand_UVmm_m; }
		//  set { this.speedControlBand_UVmm_m = value; }
		//}

    [Category("1. SPEED SETTINGS"), PropertyOrder(50), Description("Tolleranza (in mm/min) fra velocità reale e velocità desiderata quando il 'Cruise Control' è abilitato. AL MOMENTO QUESTA FUNZIONALITA' NON E' PRESENTE.")]
		public int CruiseControlTolerance_UVmm_m
		{
			get { return this.cruiseControlTolerance_UVmm_m; }
			set { this.cruiseControlTolerance_UVmm_m = value; }
		}


		// *************************************  SPEED RAMPS  ***********************************
		//RFazzi_FI31/05/2007_2.Removed DB Speed Parameters into Speed Settings using the parameters present into DB Section
		//[Category("2. SPEED RAMPS"), PropertyOrder(5), Description("DB Deceleration Ramp ( mm/min )")]
		//public double DBDecelerationRamp_UVmm_m
		//{
		//  get { return this.dbDecelerationRamp_UVmm_m; }
		//  set { this.dbDecelerationRamp_UVmm_m = value; }
		//}

		//[Category("2. SPEED RAMPS"), PropertyOrder(10), Description("DB Accelation Pulse Width ON ( msec )")]
		//public int AccelerationPulseWidthOn_UTms
		//{
		//  get { return this.accelerationPulseWidthOn_UTms; }
		//  set { this.accelerationPulseWidthOn_UTms = value; }
		//}

		//[Category("2. SPEED RAMPS"), PropertyOrder(20), Description("DB Accelation Pulse Width OFF ( msec )")]
		//public int AccelerationPulseWidthOff_UTms
		//{
		//  get { return this.accelerationPulseWidthOff_UTms; }
		//  set { this.accelerationPulseWidthOff_UTms = value; }
		//}

		//[Category("2. SPEED RAMPS"), PropertyOrder(30), Description("DB Decelation Pulse Width ON ( msec )")]
		//public int DecelerationPulseWidthOn_UTms
		//{
		//  get { return this.decelerationPulseWidthOn_UTms; }
		//  set { this.decelerationPulseWidthOn_UTms = value; }
		//}

		//[Category("2. SPEED RAMPS"), PropertyOrder(40), Description("DB Decelation Pulse Width OFF ( msec )")]
		//public int DecelerationPulseWidthOff_UTms
		//{
		//  get { return this.decelerationPulseWidthOff_UTms; }
		//  set { this.decelerationPulseWidthOff_UTms = value; }
		//}

    [Category("2. SPEED RAMPS"), PropertyOrder(45), Description("Ritardo fra comando di inizio decelerazione ed effettiva decelerazione ( sec )")]
		public double DBSlowDownOffset_UTs
		{
			get { return this.dbSlowDownOffset_UTs; }
			set { this.dbSlowDownOffset_UTs = value; }
		}

    [Category("2. SPEED RAMPS"), PropertyOrder(50), Description("Rampa di Accelerazione/Decelerazione del riferimento di velocità fornito per il Jog DryEnd ( mm/min )")]
		public int DEJogAccDecRamp_UVmm_m
		{
			get { return this.deJogAccDecRamp_UVmm_m; }
			set { this.deJogAccDecRamp_UVmm_m = value; }
		}


		// *************************************  GENERAL SETTINGS  ***********************************
    [Category("3. GENERAL SETTINGS"), PropertyOrder(10), Description("Abilita il controllo della curva di taglio della Master per la determinazione della massima velocità di linea.")]
		public bool LimitKnifeSpeed
		{
			get { return this.limitKnifeSpeed; }
			set { this.limitKnifeSpeed = value; }
		}

    [Category("3. GENERAL SETTINGS"), PropertyOrder(20), Description("Se TRUE il riferimento di velocità viene sempre fornito al DryEnd anche in assenza dell'ingresso di 'Line Run' dai Piani. A linea ferma, verrà fornito un riferimento di velocità proporzionale alla velocità minima impostata.")]
		public bool AlwaysGiveReference
		{
			get { return this.alwaysGiveReference; }
			set { this.alwaysGiveReference = value; }
		}

    [Category("3. GENERAL SETTINGS"), PropertyOrder(30), Description("Se TRUE la velocità data alla macchine del DryEnd è quella acquisita dalla ruota di misura.")]
		public bool UseMeasuringWheelWhenValid
		{
			get { return this.useMeasuringWheelWhenValid; }
			set { this.useMeasuringWheelWhenValid = value; }
		}

		[Category("3. GENERAL SETTINGS"), PropertyOrder(40), Description("Distanza (in mm) da contare dopo che è stata impegnata la fotocellula di presenza cartone alla ruota di misura prima di fornire il riferimento di velocità usando l'encoder su di essa montato. Tipicamente si deve inserire la distanza fra la Ruota di Misura e la Taglierina Trasversale.")]
		public int WebAtMWDebounceDistance_UDmm
		{
			get { return this.webAtMWDebounceDistance_UDmm; }
			set { this.webAtMWDebounceDistance_UDmm = value; }
		}

		[Category("3. GENERAL SETTINGS"), PropertyOrder(50), Browsable(false), Description("MW Before RSH")]
		public bool MWBeforeRSH
		{
			get { return this.mwBeforeRSH; }
			set { this.mwBeforeRSH = value; }
		}

    [Category("3. GENERAL SETTINGS"), PropertyOrder(60), Description("Disabilita 'Auto Speed' dopo un Cambio Ordine su un Cambio WetEnd.")]
		public bool DisableAutoSpeedOnWetEndChange
		{
			get { return this.disableAutoSpeedOnWetEndChange; }
			set { this.disableAutoSpeedOnWetEndChange = value; }
		}

    [Category("3. GENERAL SETTINGS"), PropertyOrder(70), Description("Disabilita 'Auto Speed' dopo un Cambio Ordine su un Cambio Onda.")]
		public bool DisableAutoSpeedOnFluteChange
		{
			get { return this.disableAutoSpeedOnFluteChange; }
			set { this.disableAutoSpeedOnFluteChange = value; }
		}

    [Category("3. GENERAL SETTINGS"), PropertyOrder(100), Description("Mantiene ad ON il segnale di 'Line Run' dopo aver ricevuto lo stop dai Piani Caldi ( msec ). Utilizzato per evitare le gobbe alla fermata della linea.")]
		public int MaintainLineRun_UTms
		{
			get { return this.maintainLineRun_UTms; }
			set { this.maintainLineRun_UTms = value; }
		}

		[Category("3. GENERAL SETTINGS"), PropertyOrder(110), Browsable(false), Description("Auto Speed Delay ( msec )")]
		public int AutoSpeedDelay_UTms
		{
			get { return this.autoSpeedDelay_UTms; }
			set { this.autoSpeedDelay_UTms = value; }
		}

		[Category("3. GENERAL SETTINGS"), PropertyOrder(120), Browsable(false), Description("DB ADC Present")]
		public bool ADCPresentDB
		{
			get { return this.adcPresentDB; }
			set { this.adcPresentDB = value; }
		}

		[Category("3. GENERAL SETTINGS"), PropertyOrder(130), Browsable(false), Description("Enable Use DB ADC")]
		public bool UseADC
		{
			get { return this.useADC; }
			set { this.useADC = value; }
		}

	}
}