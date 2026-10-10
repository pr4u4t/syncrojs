using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SSTwin22_60CommsProtocolRevisionConverter : StringConverter
	{
	  private string[] protocolRevision = null;

		public SSTwin22_60CommsProtocolRevisionConverter()
	    : base( )
	  {
	    this.protocolRevision = new string[] { "0.0" };
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

	public class SSTwin22_60CommsSettings : SyncroBase 
  {

		string protocolVersion = null;
		BaseComms baseComms=null;

		[Category("2. SS Twin 22.60 SYNCRO TASK SETTINGS"), TypeConverter(typeof(SSTwin22_60CommsProtocolRevisionConverter)), Description("Protocol Revision")]
		public string  ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}


		[Category("3. SS Twin 22.60  Comms SETTINGS"), Description("Comms settings")]
		public BaseComms BaseComms
		{
			get { return baseComms; }
			set { baseComms = value; }
		}

		public SSTwin22_60CommsSettings()
      : base()
    {
			this.sectionName = "S_SS_FOSBER_22.60_Comms_HEADER" + this.sectionName;
			this.protocolVersion =  "0.0";
			this.baseComms = new BaseComms("S_SS_FOSBER_22.60_Comms");
    }
	}
	
}
