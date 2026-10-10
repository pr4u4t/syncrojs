//agrassi_FI24-02-2010_1
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
  public class MonitorSettings : TaskSettings
  {
		bool enable;
		[Category("2. MONITOR TASK SETTINGS"), Description("Determine whether the MONITOR task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

    string commandArguments;
    [Category("2. MONITOR TASK SETTINGS"), Description("The arguments that should be passed to MONITOR task")]
    public string CommandArguments
    {
      get { return commandArguments; }
      set { commandArguments = value; }
    }

		public MonitorSettings(): base()
    {
      this.sectionName = "S_Monitor" + this.sectionName;
			this.enable = true;
      this.commandArguments = "";
		}
  }
}
