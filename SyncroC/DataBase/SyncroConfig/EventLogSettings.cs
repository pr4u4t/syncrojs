using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class EVENTLOGSettings : TaskSettings
	{
		bool enable;

		[Category("2. EVENTLOG TASK SETTINGS"), Description("Determine whether the EVENTLOG task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public EVENTLOGSettings()
			: base()
    {
			this.sectionName = "S_EVENTLOG" + this.sectionName;
			this.enable = true;
		}
	}
}
