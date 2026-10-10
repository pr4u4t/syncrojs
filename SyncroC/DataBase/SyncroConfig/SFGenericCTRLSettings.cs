using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SingleFacerGenericCTRLSettings : TaskSettings
	{
		bool enable;
		[Category("2. Single Facer Generic CTRL TASK SETTINGS"), Description("Determine whether the Single Facer Generic CTRL task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public SingleFacerGenericCTRLSettings(string singleFacerNumber)
			: base()
    {
			this.sectionName = "S_SF_GENERIC_CTRL" + singleFacerNumber + this.sectionName;
			this.enable = false;
		}
	}
}
