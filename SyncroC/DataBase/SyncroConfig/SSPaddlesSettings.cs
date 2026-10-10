using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SSPaddlesProtocolRevisionConverter : StringConverter
	{
		private string[] protocolRevision = null;

    public SSPaddlesProtocolRevisionConverter()
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


	public class SSPaddlesSettings : TaskSettings
	{
		bool enable;
		string protocolVersion;

		[Category("2. Paddles Settings TASK SETTINGS"), Description("Determine whether the Paddles task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("2. Paddles Settings TASK SETTINGS"), TypeConverter(typeof(SSPaddlesProtocolRevisionConverter)), Description("Protocol Revision")]
		public string ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

    public SSPaddlesSettings()
			: base()
    {
			this.sectionName = "S_PADDLES" + this.sectionName;
			this.enable = false;
			this.protocolVersion = "1.0";
		}
	}
}
