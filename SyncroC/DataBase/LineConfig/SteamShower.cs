using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	[TypeConverter(typeof(Converters))]
	public class SteamShower : SyncroC.DataBase.LineConfig.LineBase
	{
		bool present;
    [PropertyOrder(10), Category("1. SteamShower"), Description("Se TRUE il controllo dell'interfaccia con il controllo dell'umidificatore carta è presente.")]
		public bool Present
		{
			get { return present; }
			set { present = value; }
		}

		//double offset;
		//[PropertyOrder(80), Category("1. SteamShower"), Description("Offset for position calculation. Position = ((Position_UVoltage - Offset)*ScaleFactor)")]
		//public double Offset
		//{
		//  get { return offset; }
		//  set { offset = value; }
		//}

		//double scaleFactor;
		//[PropertyOrder(90), Category("1. SteamShower"), Description("Proportional factor for position calculation. Position = ((Position_UVoltage - Offset)*ScaleFactor)")]
		//public double ScaleFactor
		//{
		//  get { return scaleFactor; }
		//  set { scaleFactor = value; }
		//}

		Regulation steamShowerRegulation;
		[Category("2. Regulation Config"), PropertyOrder(10), Description("Steam Shower Regulation Config")]
		public Regulation SteamShowerRegulation
		{
			get { return steamShowerRegulation; }
			set { steamShowerRegulation = value; }
		}

		ScaleConfig steamShowerScaleConfig;
		[Category("3. Scale Config"), PropertyOrder(10), Description("Steam Shower Scaling Config")]
		public ScaleConfig SteamShowerScaleConfig
		{
			get { return steamShowerScaleConfig; }
			set { steamShowerScaleConfig = value; }
		}

		/// <summary>
		/// Builder.
		/// </summary>
		public SteamShower(int number)
			: base()
		{
			this.sectionName = "SteamShower_" + number.ToString();

			this.present = false;
			//this.offset = 0;
			//this.scaleFactor = 0;

      //RFAZZI_FI31/05/2011_1. Corretto errore nella classe SteamShower lato client che non indicizzava i parametri di regolazione e ScaleConfig. 
      this.steamShowerRegulation = new Regulation("SteamShowerRegulation_" + number.ToString());
      this.steamShowerScaleConfig = new ScaleConfig("SteamShowerScaleConfig_" + number.ToString());

		}
	}
}
