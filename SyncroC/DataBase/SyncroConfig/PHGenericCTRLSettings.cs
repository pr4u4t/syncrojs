using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class PHGenericCTRLSettings : TaskSettings
	{
		bool enable;
		[Category("2. Thermostack CTRL TASK SETTINGS"), Description("Determine whether the Thermostac CTRL task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public PHGenericCTRLSettings()
			: base()
    {
			this.sectionName = "S_PH_GENERIC_CTRL" + this.sectionName;
			this.enable = false;
		}
	}
}
