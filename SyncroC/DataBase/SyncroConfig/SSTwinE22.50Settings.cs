using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SSTwinEProtocolRevisionConverter : StringConverter
	{
		private string[] protocolRevision = null;

		public SSTwinEProtocolRevisionConverter()
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


	public class SSTwinESettings : TaskSettings
	{
		bool enable;
		string protocolVersion;

		[Category("2. SS TwinE Settings TASK SETTINGS"), Description("Determine whether the TwinE task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("2. SS TwinE Settings TASK SETTINGS"), TypeConverter(typeof(SSTwinEProtocolRevisionConverter)), Description("Protocol Revision")]
		public string ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		public SSTwinESettings()
			: base()
    {
			this.sectionName = "S_SS_FOSBER_22.50" + this.sectionName;
			this.enable = false;
			this.protocolVersion = "1.0";
		}
	}
}
