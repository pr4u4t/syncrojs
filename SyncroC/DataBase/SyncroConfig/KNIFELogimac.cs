using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

//PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Electric
namespace SyncroC.DataBase.SyncroConfig
{
  public class KnifeLogimac_ProtocolRevisionConverter : StringConverter
  {
    private string[] protocolRevision = null;

    public KnifeLogimac_ProtocolRevisionConverter()
      : base()
    {
      this.protocolRevision = new string[] { "1.0", "1.1" };
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return new StandardValuesCollection(this.protocolRevision);
    }
  }

  public class KnifeLogimac_Settings : TaskSettings
  {
    bool enable;
    string protocolVersion;

    [Category("2. KNIFE LOGIMAC Settings TASK SETTINGS"), Description("Determine whether the electrical KNIFE LOGIMAC task should be automatically started.")]
    public bool Enable
    {
      get { return enable; }
      set { enable = value; }
    }

    [Category("2.  KNIFE LOGIMAC Settings TASK SETTINGS"), TypeConverter(typeof(KnifeLogimac_ProtocolRevisionConverter)), Description("Protocol Revision")]
    public string ProtocolVersion
    {
      get { return this.protocolVersion; }
      set { this.protocolVersion = value; }
    }

    public KnifeLogimac_Settings()
      : base()
    {
      this.sectionName = "S_KNIFE_LOGIMAC" + this.sectionName;
      this.protocolVersion = "1.0";
      this.enable = false;
    }
  }
}
