using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	//RFazzi_FI17/01/2008_1.Added task configurations on graphics
	public class STKTerminal20_11ProtocolRevisionConverter : StringConverter
	{
	  private string[] protocolRevision = null;

		public STKTerminal20_11ProtocolRevisionConverter()
	    : base( )
	  {

      //PMASSARO_FI25/03/10 - Added Protocol 0.3 For Stacker 20.11
      /*
      //PMASSARO_FI07/07/09 - Added Protocol 0.2 For Stacker 20.11
      //this.protocolRevision = new string[] { "0.1" };
      this.protocolRevision = new string[] { "0.1", "0.2" };
      */
      this.protocolRevision = new string[] { "0.1", "0.2", "0.3" };
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

	public class STKTerminal20_11CommsSettings : SyncroBase 
  {

		string protocolVersion = null;
		int plcNodeNumber;
		BaseComms baseComms=null;

		[Category("2. STK Terminal 20.11 SYNCRO TASK SETTINGS"), TypeConverter(typeof(STKTerminal20_11ProtocolRevisionConverter)), Description("Protocol Revision")]
		public string  ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		[Category("2. STK Terminal 20.11 SYNCRO TASK SETTINGS"), Description("PLC Number of node")]
		public int PLCNodeNumber
		{
			get { return this.plcNodeNumber; }
			set { this.plcNodeNumber = value; }
		}

		[Category("3. STK Terminal 20.11  Comms SETTINGS"), Description("Comms settings")]
		public BaseComms BaseComms
		{
			get { return baseComms; }
			set { baseComms = value; }
		}

		public STKTerminal20_11CommsSettings(string level)
      : base()
    {
			this.sectionName = "S_STK_FOSBER_20.11_Comms"+level+"_HEADER"+ this.sectionName;
			this.protocolVersion =  "0.1";
			this.plcNodeNumber = 0;
			this.baseComms = new BaseComms("S_STK_FOSBER_20.11_Comms"+level);
    }
	}
	
}
