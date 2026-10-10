using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class DBGenericCtrlSettings : TaskSettings
	{
		bool enable;
		[Category("2. DB GENERIC CTRL TASK SETTINGS"), Description("Determine whether the DB Generic Ctrl task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		public DBGenericCtrlSettings()
			: base()
    {
			this.sectionName = "S_DB_GENERIC_CTRL" + this.sectionName;
			this.enable = false;
		}
	}
}
