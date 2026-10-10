//agrassi_FI22-10-08
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
  public class MTHFosberMindaRevisionConverter : StringConverter
  {
    private string[] protocolRevision = null;

    public MTHFosberMindaRevisionConverter(): base()
    {
      this.protocolRevision = new string[] { "1.0", "2.0" };
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

  public class MTHFosberMindaCommsSettings : SyncroBase
  {

    string protocolVersion = null;
    string coding = null;
    BaseComms baseComms = null;

    [Category("2. MTH Fosber Minda Comms SETTINGS"), TypeConverter(typeof(MTHFosberMindaRevisionConverter)), Description("Protocol Revision")]
    public string ProtocolVersion
    {
      get { return this.protocolVersion; }
      set { this.protocolVersion = value; }
    }

    [Category("2. MTH Fosber Minda Comms SETTINGS"), Description("ASCII, UNICODE or a CodePage number")]
    public string Coding
    {
      get { return this.coding; }
      set { this.coding = value; }
    }

    [Category("2. MTH Fosber Minda Comms SETTINGS"), Description("Comms settings")]
    public BaseComms BaseComms
    {
      get { return baseComms; }
      set { baseComms = value; }
    }

    public MTHFosberMindaCommsSettings(): base()
    {
      this.sectionName = "S_MTH_FOSBER_MINDA_COMMS_HEADER" + this.sectionName;
      this.protocolVersion = "2.0";
      this.coding = "UNICODE";
      this.baseComms = new BaseComms("S_MTH_FOSBER_MINDA_COMMS");
    }
  }
}
