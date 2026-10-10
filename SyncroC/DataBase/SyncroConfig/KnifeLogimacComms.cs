using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

//PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Comms 
namespace SyncroC.DataBase.SyncroConfig
{
  public class KnifeLogimacComms_ProtocolRevisionConverter : StringConverter
  {
    private string[] protocolRevision = null;

    public KnifeLogimacComms_ProtocolRevisionConverter()
      : base()
    {
      this.protocolRevision = new string[] { "0.1" };
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

  public class KnifeLogimacComms_Settings : SyncroBase
  {

    string protocolVersion = null;
    int plcNodeNumber;
    //BaseComms baseComms = null;
    ThredBaseComms baseComms = null;

    [Category("2. KNIFE LOGIMAC COMMS SYNCRO TASK SETTINGS"), TypeConverter(typeof(KnifeLogimacComms_ProtocolRevisionConverter)), Description("Protocol Revision")]
    public string ProtocolVersion
    {
      get { return this.protocolVersion; }
      set { this.protocolVersion = value; }
    }

    [Category("2. KNIFE LOGIMAC SYNCRO TASK SETTINGS"), Description("PLC Number of node")]
    public int PLCNodeNumber
    {
      get { return this.plcNodeNumber; }
      set { this.plcNodeNumber = value; }
    }

    [Category("3. KNIFE LOGIMAC Comms SETTINGS"), Description("Comms settings")]
    public ThredBaseComms BaseComms
    {
      get { return baseComms; }
      set { baseComms = value; }
    }

    public KnifeLogimacComms_Settings()
      : base()
    {
      this.sectionName = "S_KNIFE_LOGIMAC_Comms" + "_HEADER" + this.sectionName;
      this.plcNodeNumber = 0;
      this.protocolVersion = "0.1";
      this.baseComms = new ThredBaseComms("S_KNIFE_LOGIMAC_Comms");		
    }
  }
}
