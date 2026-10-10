using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class FCTSettings : TaskSettings
	{
		bool enable;
		[Category("2. FTC TASK SETTINGS"), Description("Determine whether the FCT task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public FCTSettings(): base()
    {
			this.sectionName = "S_FCT_CONSTANT" + this.sectionName;
			this.enable = false;
		}
	}
}
