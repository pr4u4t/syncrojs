using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class WECSettings : TaskSettings
	{
		bool enable;
		[Category("2. WEC TASK SETTINGS"), Description("Determine whether the WEC task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public WECSettings(): base()
    {
			this.sectionName = "S_WEC" + this.sectionName;
			this.enable = false;
		}
	}
}
