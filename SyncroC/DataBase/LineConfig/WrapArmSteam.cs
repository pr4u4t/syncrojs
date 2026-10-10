//RFAZZI_FI21/11/2010_1. Aggiunta possibilità di controllare la pressione del vapore per i preriscaldatori della linea         
using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{

	[TypeConverter(typeof(Converters))]
	public class WrapArmSteam : SyncroC.DataBase.LineConfig.LineBase
	{
		bool present;
    [PropertyOrder(10), Category("WrapArmSteam"), Description("True if this WrapArm Steam is in the system.")]
		public bool Present
		{
			get { return present; }
			set { present = value; }
		}

		Regulation wrapArmSteamRegulation;
    [Category("2. Regulation Config"), PropertyOrder(10), Description("WrapArm Steam Regulation Config")]
		public Regulation WrapArmSteamRegulation
		{
			get { return wrapArmSteamRegulation; }
			set { wrapArmSteamRegulation = value; }
		}

		ScaleConfig wrapArmSteamScaleConfig;
    [Category("3. Scale Config"), PropertyOrder(10), Description("WrapArm Steam Scaling Config")]
		public ScaleConfig WrapArmSteamScaleConfig
		{
			get { return wrapArmSteamScaleConfig; }
			set { wrapArmSteamScaleConfig = value; }
		}

    //RFAZZI_FI07/07/2011_1. Aggiunta gestione di un numero di sezioni del Vapore per ogni singolo PreHeater. 
    SectionDimension wrapArmSteamSectionDimension;
    [Category("4. Steam Section Dimension"), PropertyOrder(10), Description("WrapArm Steam Sections dimension")]
    public SectionDimension WrapArmSteamSectionDimension
    {
      get { return wrapArmSteamSectionDimension; }
      set { wrapArmSteamSectionDimension = value; }
    }

		/// <summary>
		/// Builder.
		/// </summary>
    public WrapArmSteam(int wrapArmSteamNumber)
			: base()
		{
			this.sectionName = "WrapArmSteam_" + wrapArmSteamNumber.ToString();

			this.present = false;
			this.wrapArmSteamRegulation = new Regulation("WrapArmSteamRegulation_" + wrapArmSteamNumber.ToString());
			this.wrapArmSteamScaleConfig = new ScaleConfig("WrapArmSteamScaleConfig_" + wrapArmSteamNumber.ToString());
      //RFAZZI_FI07/07/2011_1. Aggiunta gestione di un numero di sezioni del Vapore per ogni singolo PreHeater. 
      this.wrapArmSteamSectionDimension = new SectionDimension("WrapArmSteamSectionDimension_" + wrapArmSteamNumber.ToString());
		}
	}
}
