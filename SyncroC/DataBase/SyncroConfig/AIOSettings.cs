using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class AIOSettings : TaskSettings
	{
		bool enable;
		[Category("2. AIO TASK SETTINGS"), Description("Determine whether the AIO task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public AIOSettings(): base()
    {
			this.sectionName = "S_AIO_DSP" + this.sectionName;
			this.enable = false;
		}
	}
}
