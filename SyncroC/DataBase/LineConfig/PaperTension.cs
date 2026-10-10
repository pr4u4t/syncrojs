using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	[TypeConverter(typeof(Converters))]
	public class PaperTension : SyncroC.DataBase.LineConfig.LineBase
	{
		bool present;
		[PropertyOrder(10), Category("PaperTension"), Description("Se True la tensione della carta è presente nel sistema.")]
		public bool Present
		{
			get { return present; }
			set { present = value; }
		}

		Regulation paperTensionRegulation;
		[Category("2. Regulation Config"), PropertyOrder(10), Description("Paper Tension Regulation Config")]
		public Regulation PaperTensionRegulation
		{
			get { return paperTensionRegulation; }
			set { paperTensionRegulation = value; }
		}

		ScaleConfig paperTensionScaleConfig;
		[Category("3. Scale Config"), PropertyOrder(10), Description("Paper Tension Scaling Config")]
		public ScaleConfig PaperTensionScaleConfig
		{
			get { return paperTensionScaleConfig; }
			set { paperTensionScaleConfig = value; }
		}

		/// <summary>
		/// Builder.
		/// </summary>
		public PaperTension(int paperTensionNumber)
			: base()
		{
			this.sectionName = "PaperTension_" + paperTensionNumber.ToString();

			this.present = false;
			this.paperTensionRegulation = new Regulation("PaperTensionRegulation_" + paperTensionNumber.ToString());
			this.paperTensionScaleConfig = new ScaleConfig("PaperTensionScaleConfig_" + paperTensionNumber.ToString());
		}
	}
}
