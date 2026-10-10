using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SFSmartCTRLProtocolRevisioneConverter: StringConverter
	{
		private string[] protocolRevision = null;

		public SFSmartCTRLProtocolRevisioneConverter()
			: base()
		{
      //PMASSARO_FI08/07/09 - Added New Protocols 1.1 For SF Smart (Corrisponding To 2.0 Into SYC1)
			/*
       this.protocolRevision = new string[] { "2.0" };
       */
      this.protocolRevision = new string[] { "2.0", "1.1" };
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

	public class SFSmartCTRLSettings : TaskSettings
	{
		bool enable;
		string protocolVersion;

		[Category("2. SF SMART CTRL TASK SETTINGS"), Description("Determine whether the SF Smart CTRL task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("2. SF SMART CTRL TASK SETTINGS"), TypeConverter(typeof(SFSmartCTRLProtocolRevisioneConverter)), Description("Protocol Revision")]
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

		public SFSmartCTRLSettings(string singleFacerNumber)
			: base()
    {
			this.sectionName = "S_SF_FOSBER_SMART_CTRL" + singleFacerNumber + this.sectionName;
			this.enable = false;
			this.ProtocolVersion = "2.0";
			this.plcCommunication = new PLCComm("S_SF_FOSBER_SMART_CTRL" + singleFacerNumber);
		}
	}
}
