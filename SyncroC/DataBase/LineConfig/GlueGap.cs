using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	[TypeConverter(typeof(Converters))]
	public class GlueGap : SyncroC.DataBase.LineConfig.LineBase
	{
		bool present;
		[PropertyOrder(10), Category("1. GlueGap"), Description("True if this Glue Gap is in the system.")]
		public bool Present
		{
			get { return present; }
			set { present = value; }
		}

		////ABello_FI24/09/2007_1 Modify the glue gap calculation
		//double deadZone;
		//[PropertyOrder(30), Category("GlueGap"), Description("When use a GoToSetPoint function. We move the GlueGap only if (RequestSetPoint > (i.e. <) Currentposition + (i.e. -) DeadZone)")]
		//public double DeadZone
		//{
		//  get { return deadZone; }
		//  set { deadZone = value; }
		//}

		//int minVal;
		//[PropertyOrder(40), Category("GlueGap"), Description("GlueGap Min Value.")]
		//public int MinVal
		//{
		//  get { return minVal; }
		//  set { minVal = value; }
		//}

		//int maxVal;
		//[PropertyOrder(50), Category("GlueGap"), Description("GlueGap Max Value.")]
		//public int MaxVal
		//{
		//  get { return maxVal; }
		//  set { maxVal = value; }
		//}

		//double timeOutValue_UTs;
		//[PropertyOrder(60), Category("GlueGap"), Description("When use a GoToSetPoint function. If in the last (TimeOutValue_UTs) the GlueGap doesn't move than at last (timeOutRange_UTs) the function exit.")]
		//public double TimeOutValue_UTs
		//{
		//  get { return timeOutValue_UTs; }
		//  set { timeOutValue_UTs = value; }
		//}

		//double timeOutRange_UTs;
		//[PropertyOrder(70), Category("GlueGap"), Description("When use a GoToSetPoint function. If in the last (TimeOutValue_UTs) the GlueGap doesn't move than at last (timeOutRange_UTs) the function exit.")]
		//public double TimeOutRange_UTs
		//{
		//  get { return timeOutRange_UTs; }
		//  set { timeOutRange_UTs = value; }
		//}

		//double offset;
		//[PropertyOrder(80), Category("GlueGap"), Description("Offset for position calculation. Position = ((Position_UVoltage - Offset)*ScaleFactor)")]
		//public double Offset
		//{
		//  get { return offset; }
		//  set { offset = value; }
		//}

		//double scaleFactor;
		//[PropertyOrder(90), Category("GlueGap"), Description("Proportional factor for position calculation. Position = ((Position_UVoltage - Offset)*ScaleFactor)")]
		//public double ScaleFactor
		//{
		//  get { return scaleFactor; }
		//  set { scaleFactor = value; }
		//}

		int minSpeedAutoControl_UVmm_m;
		[PropertyOrder(100), Category("1. GlueGap"), Description("Min speed at which auto wrap control is attempted (set 0 for disable).")]
		public int MinSpeedAutoControl_UVmm_m
		{
			get { return minSpeedAutoControl_UVmm_m; }
			set { minSpeedAutoControl_UVmm_m = value; }
		}

		int setpointAutoControl;
		[PropertyOrder(110), Category("1. GlueGap"), Description("Setpoint where wrap go when actualSpeed <= minSpeedAutoControl_UVmm_m.")]
		public int SetpointAutoControl
		{
			get { return setpointAutoControl; }
			set { setpointAutoControl = value; }
		}

		Regulation glueGapRegulation;
		[Category("2. Regulation Config"), PropertyOrder(10), Description("Glue Gap Regulation Config")]
		public Regulation GlueGapRegulation
		{
			get { return glueGapRegulation; }
			set { glueGapRegulation = value; }
		}

		ScaleConfig glueGapScaleConfig;
		[Category("3. Scale Config"), PropertyOrder(10), Description("Glue Gap Scaling Config")]
		public ScaleConfig GlueGapScaleConfig
		{
			get { return glueGapScaleConfig; }
			set { glueGapScaleConfig = value; }
		}

		/// <summary>
		/// Builder.
		/// </summary>
		public GlueGap(int number)
			: base()
		{
			this.sectionName = "GlueGap_" + number.ToString();

			this.present = false;
			this.minSpeedAutoControl_UVmm_m = 0;
			this.setpointAutoControl = 0;

			this.glueGapRegulation = new Regulation("GlueGapRegulation_" + number.ToString());
			this.glueGapScaleConfig = new ScaleConfig("GlueGapScaleConfig_" + number.ToString());
		}
	}
}
