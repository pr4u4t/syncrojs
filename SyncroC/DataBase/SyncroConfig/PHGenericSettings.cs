using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class PHGenericSettings : TaskSettings
	{
		bool enable;
		[Category("2. Thermostack TASK SETTINGS"), Description("Determine whether the Thermostac task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public PHGenericSettings()
			: base()
    {
			this.sectionName = "S_PH_GENERIC" + this.sectionName;
			this.enable = false;
		}
	}
}
