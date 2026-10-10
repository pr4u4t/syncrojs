using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class DBGenericSettings : TaskSettings
	{
		bool enable;
		[Category("2. DB TASK SETTINGS"), Description("Determine whether the DB Generic task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public DBGenericSettings()
			: base()
    {
			this.sectionName = "S_DB_GENERIC" + this.sectionName;
			this.enable = false;
		}
	}
}
