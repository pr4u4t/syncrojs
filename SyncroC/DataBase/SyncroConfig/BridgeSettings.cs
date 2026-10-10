using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class BRIDGESettings : TaskSettings
	{
		bool enable;
		[Category("2. Bridge TASK SETTINGS"), Description("Determine whether the Bridge task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public BRIDGESettings(string bridgeNumber)
			: base()
    {
			this.sectionName = "S_BRIDGE" + bridgeNumber  + this.sectionName;
			this.enable = false;
		}
	}
}
