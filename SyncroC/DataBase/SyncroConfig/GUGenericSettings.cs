using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class GUGenericSettings : TaskSettings
	{
		bool enable;
		[Category("2. Glue Unit TASK SETTINGS"), Description("Determine whether the Glue Unit task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public GUGenericSettings()
			: base()
    {
			this.sectionName = "S_GU_GENERIC" + this.sectionName;
			this.enable = false;
		}
	}
}
