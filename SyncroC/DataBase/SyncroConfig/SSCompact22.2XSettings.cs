using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SSCompact22_2XProtocolRevisionConverter : StringConverter
	{
		private string[] protocolRevision = null;

		public SSCompact22_2XProtocolRevisionConverter()
			: base()
		{
			this.protocolRevision = new string[] { "0.0" };
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


	public class SSCompact22_2XSettings : TaskSettings
	{
		bool enable;
		string protocolVersion;

		[Category("2. SS Compact22.2X Settings TASK SETTINGS"), Description("Determine whether the Compact22.2X task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("2. SS Compact22.2X Settings TASK SETTINGS"), TypeConverter(typeof(SSCompact22_2XProtocolRevisionConverter)), Description("Protocol Revision")]
		public string ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		public SSCompact22_2XSettings()
			: base()
    {
			this.sectionName = "S_SS_FOSBER_22.2X" + this.sectionName;
			this.enable = false;
			this.protocolVersion = "0.0";
		}
	}
}
