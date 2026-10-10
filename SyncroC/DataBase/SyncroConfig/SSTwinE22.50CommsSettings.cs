using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SSTwinE22_50CommsProtocolRevisionConverter : StringConverter
	{
	  private string[] protocolRevision = null;

		public SSTwinE22_50CommsProtocolRevisionConverter()
	    : base( )
	  {
      //PMASSARO_FI23/03/10 - Aggiunta protocollo 2.0 con TwinEv
	    /*
       this.protocolRevision = new string[] { "2.1" };
      */
      this.protocolRevision = new string[] { "2.0", "2.1" };
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

	public class SSTwinE22_50CommsSettings : SyncroBase 
  {

		string protocolVersion = null;
		BaseComms baseComms=null;

		[Category("2. SS TwinE 22.50 SYNCRO TASK SETTINGS"), TypeConverter(typeof(SSTwinE22_50CommsProtocolRevisionConverter)), Description("Protocol Revision")]
		public string  ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}


		[Category("3. SS TwinE 22.50  Comms SETTINGS"), Description("Comms settings")]
		public BaseComms BaseComms
		{
			get { return baseComms; }
			set { baseComms = value; }
		}

		public SSTwinE22_50CommsSettings()
      : base()
    {
			this.sectionName = "S_SS_FOSBER_22.50_Comms_HEADER" + this.sectionName;
			this.protocolVersion =  "2.1";
			this.baseComms = new BaseComms("S_SS_FOSBER_22.50_Comms");
    }
	}
	
}
