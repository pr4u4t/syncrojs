#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;

#endregion

namespace SyncroC.DataBase.LineConfig
{
	[TypeConverter(typeof(Converters))]
	public class Encoders : SyncroC.DataBase.LineConfig.LineBase
	{
		double mmPerPulseMeasureWheel;
		double mmPerPulseDoubleBacker;
    //agrassi_FI26-06-08
    double[] mmPerPulseExitBelt;

		public Encoders()
			: base()
		{
			this.sectionName = "ENCODERS";

			this.mmPerPulseMeasureWheel = 0.24414;
			this.mmPerPulseDoubleBacker = 0.24414;
      //agrassi_FI26-06-08
      this.mmPerPulseExitBelt = new double[Knife.MAX_NUM_LEVELS];
      for (int i = 0; i < Knife.MAX_NUM_LEVELS; i++)
        this.mmPerPulseExitBelt[i] = 0.24414;
		}

    [Browsable(true), PropertyOrder(10), Category("1. Encoders"), Description("Millimeters per pulse of measuring wheel encoder")]
    [RefreshProperties(RefreshProperties.All)]
		public double MMPerPulseMeasureWheel
		{
			get { return this.mmPerPulseMeasureWheel; }
			set { this.mmPerPulseMeasureWheel = value; }
		}

    [Browsable(true), PropertyOrder(20), Category("1. Encoders"), Description("Millimeters per pulse of double backer encoder")]
		public double MMPerPulseDoubleBacker
		{
			get { return mmPerPulseDoubleBacker; }
			set { mmPerPulseDoubleBacker = value; }
		}

    //agrassi_FI26-06-08
    [Browsable(true), PropertyOrder(30), Category("1. Encoders"), Description("Millimeters per pulse of encoders of the Modibelt"), TypeConverter(typeof(FosberArrayConverter)), Editor(typeof(ArrayTypeEditor), typeof(UITypeEditor))]
    public double[] MMPerPulseExitBelt
    {
      get { return mmPerPulseExitBelt; }
      set { mmPerPulseExitBelt = value; }
    }
  }
}
