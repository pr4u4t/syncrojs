using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
  public class RSHMhi_Settings : TaskSettings
  {
    bool enable;
    [Category("2. RSH TASK SETTINGS"), Description("Determine whether the RSH Mhi task should be automatically started.")]
    public bool Enable
    {
      get { return enable; }
      set { enable = value; }
    }

    string protocolVersion;
    [Category("2. RSH TASK SETTINGS"), Description("The version of the protocol")]
    public string ProtocolVersion
    {
      get { return protocolVersion; }
      set { protocolVersion = value; }
    }


    public RSHMhi_Settings()
        : base()
    {
      this.sectionName = "S_RSH_MHI" + this.sectionName;
      this.enable = false;
      this.protocolVersion = "1.0";
    }
  }
}
