using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class RSHFosber23_XXSettings : TaskSettings
	{
		bool enable;
		[Category("2. RSH TASK SETTINGS"), Description("Determine whether the RSH Fosber 23.XX task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

    //agrassi_FI30-08-08
    string protocolVersion;
    [Category("2. RSH TASK SETTINGS"), Description("The version of the protocol")]
    public string ProtocolVersion
    {
      get { return protocolVersion; }
      set { protocolVersion = value; }
    }


		public RSHFosber23_XXSettings()
			: base()
    {
			this.sectionName = "S_RSH_FOSBER_23.XX" + this.sectionName;
			this.enable = false;
      //agrassi_FI30-08-08
      this.protocolVersion = "";
		}
	}
}
