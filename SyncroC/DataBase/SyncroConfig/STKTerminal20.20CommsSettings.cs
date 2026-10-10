using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	//RFazzi_FI17/01/2008_1.Added task configurations on graphics
	public class STKTerminal20_20ProtocolRevisionConverter : StringConverter
	{
	  private string[] protocolRevision = null;

		public STKTerminal20_20ProtocolRevisionConverter()
	    : base( )
	  {
      //PMASSARO_12/07/10 - Gestione Protocollo Fosber 1.1
	    //this.protocolRevision = new string[] { "3.0" };
      this.protocolRevision = new string[] { "1.1", "2.1", "3.0" };
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

	public class STKTerminal20_20CommsSettings : SyncroBase 
  {

		string protocolVersion = null;
		int plcNodeNumber;
		BaseComms baseComms=null;

		[Category("2. STK Terminal 20.20 SYNCRO TASK SETTINGS"), TypeConverter(typeof(STKTerminal20_20ProtocolRevisionConverter)), Description("Protocol Revision")]
		public string  ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		[Category("2. STK Terminal 20.20 SYNCRO TASK SETTINGS"), Description("PLC Number of node")]
		public int PLCNodeNumber
		{
			get { return this.plcNodeNumber; }
			set { this.plcNodeNumber = value; }
		}

		[Category("3. STK Terminal 20.20  Comms SETTINGS"), Description("Comms settings")]
		public BaseComms BaseComms
		{
			get { return baseComms; }
			set { baseComms = value; }
		}

		public STKTerminal20_20CommsSettings(string level)
      : base()
    {
			this.sectionName = "S_STK_FOSBER_22.20_Comms" + level + "_HEADER" + this.sectionName;
			this.protocolVersion =  "3.0";
			this.plcNodeNumber = 0;
			this.baseComms = new BaseComms("S_STK_FOSBER_22.20_Comms" + level);
    }
	}
	
}
