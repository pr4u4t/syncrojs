using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class STKLREProtocolRevisionConverter : StringConverter
	{
		private string[] protocolRevision = null;

		public STKLREProtocolRevisionConverter()
			: base()
		{
			this.protocolRevision = new string[] { "0.0","0.1" };
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

	public class STK_LRE_CommsSettings : TaskSettings
	{
		bool enable;
		string protocolVersion;

		[Category("2. STK Favalessa LRE TASK SETTINGS"), Description("Determine whether the Favalessa LRE Comms task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("2. STK Favalessa LRE TASK SETTINGS"), TypeConverter(typeof(STKLREProtocolRevisionConverter)), Description("Protocol Revision")]
		public string ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		private PLCSiemensComm plcCommunication;
		[Category("3. TASKS WITH PLC COMMUNICATION SETTINGS"), Description("The parameters for the communication with PLC.")]
		public PLCSiemensComm PLCCommunication
		{
			get { return plcCommunication; }
			set { plcCommunication = value; }
		}

        public STK_LRE_CommsSettings(string level)
			: base()
        {
			this.sectionName = "S_STK_FAVALESSA_LRE_Comms"+level + this.sectionName;
			this.enable = false;
			this.ProtocolVersion = "0.1";
			this.plcCommunication = new PLCSiemensComm("S_STK_FAVALESSA_LRE_Comms" + level);
		}
	}
}
