using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class STKTerminal400ProtocolRevisionConverter : StringConverter
	{
		private string[] protocolRevision = null;

		public STKTerminal400ProtocolRevisionConverter()
			: base()
		{
    			//PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
    			/*
    			this.protocolRevision = new string[] { "0.0", "0.1" };
			*/
			this.protocolRevision = new string[] { "0.0", "0.1" , "0.2" };
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

	public class STKTerminal400CommsSettings : TaskSettings
	{
		bool enable;
		string protocolVersion;

		[Category("2. STK TERMINAL400 TASK SETTINGS"), Description("Determine whether the Terminal 400 Comms task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("2. STK TERMINAL400 TASK SETTINGS"), TypeConverter(typeof(STKTerminal400ProtocolRevisionConverter)), Description("Protocol Revision")]
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

		public STKTerminal400CommsSettings(string level)
			: base()
    {
			this.sectionName = "S_STK_FOSBER_TR400_Comms"+level + this.sectionName;
			this.enable = false;
			
			//PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
			/*
			this.ProtocolVersion = "0.1";
			*/
			this.ProtocolVersion = "0.2";
			
			this.plcCommunication = new PLCComm("S_STK_FOSBER_TR400_Comms" + level);
		}
	}
}
