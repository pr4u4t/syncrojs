using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
  public class MTHCommsSettings: TaskSettings
  {
    bool enable;
    [Category("2. MTH SYNCRO TASK SETTINGS"), Description("Determine whether the MTH task should be automatically started.")]
    public bool Enable
    {
      get { return enable; }
      set { enable = value; }
    }

    public MTHCommsSettings()
      : base()
    {
      this.sectionName = "S_MTH_Comms" + this.sectionName;
      this.enable = false;
    }
  }
}
