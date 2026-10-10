using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SPEEDSettings : TaskSettings
	{
		bool enable;

		[Category("2. SPEED TASK SETTINGS"), Description("Determine whether the SPEED task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public SPEEDSettings()
			: base()
    {
			this.sectionName = "S_SPEED_DSP" + this.sectionName;
			this.enable = true;
		}
	}
}
