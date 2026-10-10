using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class DATABRIDGESettings : TaskSettings
	{
		bool enable;

		[Category("2. DATABRIDGE TASK SETTINGS"), Description("Determine whether the DATABRIDGE task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public DATABRIDGESettings()
			: base()
    {
			this.sectionName = "S_DATABRIDGE" + this.sectionName;
			this.enable = true;
		}
	}
}
