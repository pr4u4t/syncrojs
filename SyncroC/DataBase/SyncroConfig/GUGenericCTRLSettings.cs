using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class GUGenericCTRLSettings : TaskSettings
	{
		bool enable;
		[Category("2. Glue Unit CTRL TASK SETTINGS"), Description("Determine whether the Glue Unit CTRL task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public GUGenericCTRLSettings()
			: base()
    {
			this.sectionName = "S_GU_GENERIC_CTRL" + this.sectionName;
			this.enable = false;
		}
	}
}
