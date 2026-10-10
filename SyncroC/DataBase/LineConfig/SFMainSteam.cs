using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	[TypeConverter(typeof(Converters))]
	public class SFMainSteam : SyncroC.DataBase.LineConfig.LineBase
	{
		bool present;
		[PropertyOrder(10), Category("1. SF Main Steam"), Description("True if this SF Main Steam is in the system.")]
		public bool Present
		{
			get { return present; }
			set { present = value; }
		}

		Regulation sfMainSteamRegulation;
		[Category("2. Regulation Config"), PropertyOrder(10), Description("Main Steam Regulation Config")]
		public Regulation SFMainSteamRegulation
		{
			get { return sfMainSteamRegulation; }
			set { sfMainSteamRegulation = value; }
		}

		ScaleConfig sfMainSteamScaleConfig;
		[Category("3. Scale Config"), PropertyOrder(10), Description("Main Steam Scaling Config")]
		public ScaleConfig SFMainSteamScaleConfig
		{
			get { return sfMainSteamScaleConfig; }
			set { sfMainSteamScaleConfig = value; }
		}

		/// <summary>
		/// Builder.
		/// </summary>
		public SFMainSteam(int number)
			: base()
		{
			this.sectionName = "SFMainSteam_" + number.ToString();

			this.present = false;

			this.sfMainSteamRegulation = new Regulation("SFMainSteamRegulation");
			this.sfMainSteamScaleConfig = new ScaleConfig("SFMainSteamScaleConfig");

		}
	}
}
