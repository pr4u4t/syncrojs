using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SFSmart400CTRLProtocolRevisioneConverter: StringConverter
	{
		private string[] protocolRevision = null;

		public SFSmart400CTRLProtocolRevisioneConverter()
			: base()
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

	public class SFSmart400CTRLSettings : TaskSettings
	{
		bool enable;
		string protocolVersion;

		[Category("2. SF SMART400 CTRL TASK SETTINGS"), Description("Determine whether the SF Smart400 CTRL task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("2. SF SMART400 CTRL TASK SETTINGS"), TypeConverter(typeof(SFSmart400CTRLProtocolRevisioneConverter)), Description("Protocol Revision")]
		public string ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		private PLCComm plcCommunication;
		[Category("3. TASKS WITH PLC COMMUNICATION SETTINGS"), Description("The parameters for the communication with PLC.")]
		public PLCComm PLCCommunication
		{
			get { return plcCommunication; }
			set { plcCommunication = value; }
		}

    public SFSmart400CTRLSettings(string singleFacerNumber)
			: base()
    {
      this.sectionName = "S_SF_FOSBER_SMART400_CTRL" + singleFacerNumber + this.sectionName;
			this.enable = false;
			this.ProtocolVersion = "1.0";
      this.plcCommunication = new PLCComm("S_SF_FOSBER_SMART400_CTRL" + singleFacerNumber);
		}
	}
}
