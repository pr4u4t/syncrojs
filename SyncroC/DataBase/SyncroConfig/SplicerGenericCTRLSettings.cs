using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SplicerGenericCTRLSettings : TaskSettings
	{
		bool enable;
		[Category("2. Splicer Generic CTRL TASK SETTINGS"), Description("Determine whether the Splicer Generic CTRL task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public SplicerGenericCTRLSettings(string splicerNumber)
			: base()
    {
			this.sectionName = "S_SPL_GENERIC_CTRL" + splicerNumber + this.sectionName;
			this.enable = false;
		}
	}
}
