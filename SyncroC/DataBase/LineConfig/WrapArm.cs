using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	[TypeConverter(typeof(Converters))]
	public class WrapArmTemperatureConfig : SyncroC.DataBase.LineConfig.LineBase
	{
		bool temperatureSensorPresent;
		[Browsable(true), PropertyOrder(15), Category("WrapArm"), Description("se True è presente il sensore di temperatura.")]
		public bool TemperatureSensorPresent
		{
			get { return temperatureSensorPresent; }
			set { temperatureSensorPresent = value; }
		}

		Regulation wrapArmTemperatureRegulation;
		[Category("2. Temperature Regulation Config"), PropertyOrder(10), Description("Wrap Arm Temperature Regulation Config")]
		public Regulation WrapArmTemperatureRegulation
		{
			get { return wrapArmTemperatureRegulation; }
			set { wrapArmTemperatureRegulation = value; }
		}

		ScaleConfig wrapArmTemperatureScaleConfig;
		[Category("2. Temperature Scale Config"), PropertyOrder(10), Description("Wrap Arm Temperature Scale Config")]
		public ScaleConfig WrapArmTemperatureScaleConfig
		{
			get { return wrapArmTemperatureScaleConfig; }
			set { wrapArmTemperatureScaleConfig = value; }
		}

		public WrapArmTemperatureConfig(int number)
			: base()
		{
			this.sectionName = "WrapArmTemperatureConfig_" + number.ToString();

			this.temperatureSensorPresent = false;

			this.wrapArmTemperatureRegulation = new Regulation("WrapArmTemperatureRegulation_" + number.ToString());
			this.wrapArmTemperatureScaleConfig = new ScaleConfig("WrapArmTemperatureScaleConfig_" + number.ToString());

		}

	}

	[TypeConverter(typeof(Converters))]
	public class WrapArm : SyncroC.DataBase.LineConfig.LineBase
	{
		bool present;
    [PropertyOrder(10), Category("WrapArm"), Description("Se TRUE allora l'interfaccia con il WrapArm è presente.")]
		public bool Present
		{
			get { return present; }
			set { present = value; }
		}

		bool disablePositionCorrectionForDirection;
    [PropertyOrder(20), Category("WrapArm"), Description("Disabilita il ricalcolo della percentuale di fasciatura in funzione del fatto che il valore analogico sia riferito ad una fasciatura variabile in senso orario e/o antiorario. Se TRUE il valore analogico viene gestito direttamente dalla macchina.")]
		public bool DisablePositionCorrectionForDirection
		{
			get { return disablePositionCorrectionForDirection; }
			set { disablePositionCorrectionForDirection = value; }
		}

		bool ioSwapOnReverseWrap;
    [PropertyOrder(30), Category("WrapArm"), Description("Se TRUE i limiti di Min/Max posizione del WrapArm vengono invertiti a seconda che si stia fasciando in senso orario/antiorario.")]
		public bool IOSwapOnReverseWrap
		{
			get { return ioSwapOnReverseWrap; }
			set { ioSwapOnReverseWrap = value; }
		}

		bool zeroWrapForSplice;
    [PropertyOrder(40), Category("WrapArm"), Description("Se TRUE la fasciatura viene portata al minimo quando è in transito una giunta. L'inizio movimento avviene con il segnale di 'Giunta Effettuata'")]
		public bool ZeroWrapForSplice
		{
			get { return zeroWrapForSplice; }
			set { zeroWrapForSplice = value; }
		}

		int minSpeedAutoControl_UVmm_m;
    [PropertyOrder(50), Category("WrapArm"), Description("Velocità al di sotto della quale si porta la fasciatura alla percentuale impostata. Se 0, la funzionalità è disabilitata.")]
		public int MinSpeedAutoControl_UVmm_m
		{
			get { return minSpeedAutoControl_UVmm_m; }
			set { minSpeedAutoControl_UVmm_m = value; }
		}

		int setpointAutoControl_UPerc;
    [PropertyOrder(55), Category("WrapArm"), Description("Percentuale a cui si porta la fasciatura quando la velocità scende al di sotto della soglia impostata.")]
		public int SetpointAutoControl_UPerc
		{
			get { return setpointAutoControl_UPerc; }
			set { setpointAutoControl_UPerc = value; }
		}

		int[] paperPathUnderWrapped_UDmm;
    [PropertyOrder(110), Category("WrapArm"), Description("(0 ... 10) – quantitativo di carta (in mm) presente sul fasciatore con percentuale di fasciatura da 0 %a 100% a step di 10% ( 0 = 0%, 1 = 10% … 10 = 100% ).")]
		public int[] PaperPathUnderWrapped_UDmm
		{
			get { return paperPathUnderWrapped_UDmm; }
			set { paperPathUnderWrapped_UDmm = value; }
		}

		int[] paperPathOverWrapped_UDmm;
    [PropertyOrder(120), Category("WrapArm"), Description("(0 ... 10) – quantitativo di carta (in mm) presente sul fasciatore con percentuale di fasciatura da 0 %a 100% a step di 10% ( 0 = 0%, 1 = 10% … 10 = 100% ).")]
		public int[] PaperPathOverWrapped_UDmm
		{
			get { return paperPathOverWrapped_UDmm; }
			set { paperPathOverWrapped_UDmm = value; }
		}

		Regulation wrapArmRegulation;
		[Category("2. Regulation Config"), PropertyOrder(10), Description("Wrap Arm Regulation Config")]
		public Regulation WrapArmRegulation
		{
			get { return wrapArmRegulation; }
			set { wrapArmRegulation = value; }
		}

		ScaleConfig wrapArmScaleConfig;
		[Category("3. Scale Config"), PropertyOrder(10), Description("Wrap Arm Scaling Config")]
		public ScaleConfig WrapArmScaleConfig
		{
			get { return wrapArmScaleConfig; }
			set { wrapArmScaleConfig = value; }
		}

		WrapArmTemperatureConfig wrapArmTemperatureConfig;
		[Browsable(true), Category("2. Wrap Arm Temperature Regulaiton Config"), PropertyOrder(10), Description("Wrap Arm Temperature Regulation Config")]
		public WrapArmTemperatureConfig WrapArmTemperatureConfig
		{
			get { return wrapArmTemperatureConfig; }
			set { wrapArmTemperatureConfig = value; }
		}

		/// <summary>
		/// Builder.
		/// </summary>
		public WrapArm(int number)
			: base()
		{
			this.sectionName = "WrapArm_" + number.ToString();

			this.disablePositionCorrectionForDirection = false;
			this.present = false;
			this.IOSwapOnReverseWrap = false;
			this.zeroWrapForSplice = false;
			this.minSpeedAutoControl_UVmm_m = 0;
			this.setpointAutoControl_UPerc = 0;

			this.paperPathOverWrapped_UDmm = new int[11];
			this.paperPathUnderWrapped_UDmm = new int[11];

			this.wrapArmRegulation = new Regulation("WrapArmRegulation_" + number.ToString());
			this.wrapArmScaleConfig = new ScaleConfig("WrapArmScaleConfig_" + number.ToString());

			this.wrapArmTemperatureConfig = new WrapArmTemperatureConfig(number);
	
		}

	}
}
