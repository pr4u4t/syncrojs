using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SSCompact22_2XCommsProtocolRevisionConverter : StringConverter
	{
	  private string[] protocolRevision = null;

		public SSCompact22_2XCommsProtocolRevisionConverter()
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

	public class SSCompact22_2XCommsSettings : TaskSettings 
  {

		string protocolVersion = null;
		BaseSocketComms plcBaseComms = null;
		BaseSocketComms triaxBaseComms = null;

		[Category("2. SS Compact22.2X SYNCRO TASK SETTINGS"), TypeConverter(typeof(SSCompact22_2XCommsProtocolRevisionConverter)), Description("Protocol Revision")]
		public string  ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		private bool enable;
		[Category("2. SS Compact22.2X SYNCRO TASK SETTINGS")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("3. SS Compact22.2X PLC Comms SETTINGS"), Description("PLC Comms settings")]
		public BaseSocketComms PLCBaseComms
		{
			get { return plcBaseComms; }
			set { plcBaseComms = value; }
		}

		[Category("4. SS Compact22.2X TRIAX Comms SETTINGS"), Description("TRIAX Comms settings")]
		public BaseSocketComms TRIAXBaseComms
		{
			get { return triaxBaseComms; }
			set { triaxBaseComms = value; }
		}

		public SSCompact22_2XCommsSettings()
      : base()
    {
			this.sectionName = "S_SS_FOSBER_22.2X_Comms" + this.sectionName;
			this.protocolVersion =  "1.0";
			this.enable = false;
			this.plcBaseComms = new BaseSocketComms("S_SS_FOSBER_22.2X_Comms_PLC");
			this.triaxBaseComms = new BaseSocketComms("S_SS_FOSBER_22.2X_Comms_TRIAX");
		}
	}
	
}
