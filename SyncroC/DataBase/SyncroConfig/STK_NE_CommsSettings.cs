using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	//RFazzi_FI17/01/2008_1.Added task configurations on graphics
	public class STK_NE_ProtocolRevisionConverter : StringConverter
	{
	  private string[] protocolRevision = null;

		public STK_NE_ProtocolRevisionConverter()
	    : base( )
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

	public class STK_NE_CommsSettings : SyncroBase 
  {

		string protocolVersion = null;
		int plcNodeNumber;
		BaseComms baseComms=null;

		[Category("2. STK New Engineering SYNCRO TASK SETTINGS"), TypeConverter(typeof(STK_NE_ProtocolRevisionConverter)), Description("Protocol Revision")]
		public string  ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		[Category("2. STK New Engineering SYNCRO TASK SETTINGS"), Description("PLC Number of node")]
		public int PLCNodeNumber
		{
			get { return this.plcNodeNumber; }
			set { this.plcNodeNumber = value; }
		}

		[Category("3. STK New Engineering  Comms SETTINGS"), Description("Comms settings")]
		public BaseComms BaseComms
		{
			get { return baseComms; }
			set { baseComms = value; }
		}

		public STK_NE_CommsSettings(string level)
      : base()
    {
			this.sectionName = "S_STK_NE_Comms" + level + "_HEADER" + this.sectionName;
			this.protocolVersion =  "0.1";
			this.plcNodeNumber = 0;
			this.baseComms = new BaseComms("S_STK_NE_Comms" + level);
    }
	}
	
}
