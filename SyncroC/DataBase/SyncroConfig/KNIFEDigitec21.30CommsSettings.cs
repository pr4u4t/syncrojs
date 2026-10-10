using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	//RFazzi_FI17/01/2008_1.Added task configurations on graphics
	public class Knife21_30ProtocolRevisionConverter : StringConverter
	{
	  private string[] protocolRevision = null;

		public Knife21_30ProtocolRevisionConverter()
	    : base( )
	  {
      //GRAFFAELLI_FI19-04-2011_1.Added stag protocol version 2.0 management
	    this.protocolRevision = new string[] { "2.0" , "4.0" };
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

	public class KNIFE21_30CommsSettings : SyncroBase 
  {

		//RFazzi_FI17/01/2008_1.Added task configurations on graphics
		string protocolVersion = null;
		BaseComms baseComms = null;

		[Category("2. KNIFE21_30 Comms Settings TASK SETTINGS"), TypeConverter(typeof(Knife21_30ProtocolRevisionConverter)), Description("Protocol Revision")]
		public string  ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		[Category("3. KNIFE21_30 Comms SETTINGS"), Description("Comms settings")]
		public BaseComms BaseComms
		{
			get { return baseComms; }
			set { baseComms = value; }
		}

		public KNIFE21_30CommsSettings()
      : base()
    {
      //GRAFFAELLI_FI19-04-2011_2.Added "_HEADER" to set/get the ProtocolVersion in the right section
			this.sectionName = "S_KNIFE_FOSBER_21.30_Comms_HEADER" + this.sectionName;
			//RFazzi_FI17/01/2008_1.Added task configurations on graphics
			this.protocolVersion =  "4.0";
			this.baseComms = new BaseComms("S_KNIFE_FOSBER_21.30_Comms");			
    }
	}
	
}
