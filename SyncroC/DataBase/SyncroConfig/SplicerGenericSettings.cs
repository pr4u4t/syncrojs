using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SplicerGenericSettings : TaskSettings
	{
		bool enable;
		[Category("2. Splicer Generic TASK SETTINGS"), Description("Determine whether the Splicer Generic task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public SplicerGenericSettings(string splicerNumber)
			: base()
    {
			this.sectionName = "S_SPL_GENERIC" + splicerNumber  + this.sectionName;
			this.enable = false;
		}
	}
}
