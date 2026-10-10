using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class PRINTERSettings : TaskSettings
	{
		bool enable;

		[Category("2. PRINTER TASK SETTINGS"), Description("Determine whether the PRINTER task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public PRINTERSettings()
			: base()
    {
			this.sectionName = "S_PRINTER" + this.sectionName;
			this.enable = true;
		}
	}
}
