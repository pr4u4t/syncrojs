using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SSTwin22_60ProtocolRevisionConverter : StringConverter
	{
		private string[] protocolRevision = null;

		public SSTwin22_60ProtocolRevisionConverter()
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


	public class SSTwin22_60Settings : TaskSettings
	{
		bool enable;
		string protocolVersion;

		[Category("2. SS Twin22.60 Settings TASK SETTINGS"), Description("Determine whether the Twin 22.60 task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("2. SS Twin22.60 Settings TASK SETTINGS"), TypeConverter(typeof(SSTwin22_60ProtocolRevisionConverter)), Description("Protocol Revision")]
		public string ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		public SSTwin22_60Settings()
			: base()
    {
			this.sectionName = "S_SS_FOSBER_22.60" + this.sectionName;
			this.enable = false;
			this.protocolVersion = "1.0";
		}
	}
}
