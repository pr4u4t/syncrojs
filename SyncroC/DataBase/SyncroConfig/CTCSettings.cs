using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class CTCSettings : TaskSettings
	{
		bool enable;
		[Category("2. CTC TASK SETTINGS"), Description("Determine whether the CTC task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public CTCSettings(): base()
    {
			this.sectionName = "S_CTC_DSP" + this.sectionName;
			this.enable = false;
		}
	}
}
