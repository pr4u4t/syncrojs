//agrassi_FI18-03-2010_1
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class KnifeBSCOMGapoProtocolRevisionConverter : StringConverter
	{
	  private string[] protocolRevision = null;

    public KnifeBSCOMGapoProtocolRevisionConverter()
	    : base( )
	  {
	    this.protocolRevision = new string[] { "1.0" };
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

	public class KnifeBSCOMGapoCommsSettings : SyncroBase 
  {

		string protocolVersion = null;
		BaseComms baseComms = null;

    [Category("2. KNIFEBSCOMGapo Comms Settings TASK SETTINGS"), TypeConverter(typeof(KnifeBSCOMGapoProtocolRevisionConverter)), Description("Protocol Revision")]
		public string  ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		[Category("3. KNIFEBSCOMGapo Comms SETTINGS"), Description("Comms settings")]
		public BaseComms BaseComms
		{
			get { return baseComms; }
			set { baseComms = value; }
		}

    public KnifeBSCOMGapoCommsSettings()
      : base()
    {
      this.sectionName = "S_KNIFE_BSCOM_GAPO_Comms" + "_HEADER" + this.sectionName;
			//RFazzi_FI17/01/2008_1.Added task configurations on graphics
			this.protocolVersion =  "1.0";
      this.baseComms = new BaseComms("S_KNIFE_BSCOM_GAPO_Comms");			
    }
	}
	
}
