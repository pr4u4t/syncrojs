using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class DIOSettings : TaskSettings
	{
		bool enable;
		[Category("2. DIO TASK SETTINGS"), Description("Determine whether the DIO task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public DIOSettings(): base()
    {
			this.sectionName = "S_DIO_DSP" + this.sectionName;
			this.enable = false;
		}
	}
}
