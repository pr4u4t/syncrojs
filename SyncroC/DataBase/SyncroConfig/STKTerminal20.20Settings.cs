using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class STKTerminal20_20RevisionConverter : StringConverter
	{
		private string[] protocolRevision = null;

		public STKTerminal20_20RevisionConverter()
			: base()
		{
			this.protocolRevision = new string[] { "1.0","2.0" };
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
	public class STKTerminal20_20Settings : TaskSettings
	{
		bool enable;
		string protocolVersion = null;

		[Category("2. STK Terminal TASK SETTINGS"), Description("Determine whether the STK Terminal task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("2. STK Terminal TASK SETTINGS"), TypeConverter(typeof(STKTerminal20_20RevisionConverter)), Description("Protocol Revision")]
		public string ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		public STKTerminal20_20Settings()
			: base()
    {
			this.sectionName = "S_STK_FOSBER_22.20" + this.sectionName;
			this.protocolVersion = "2.0";
			this.enable = true;
		}
	}
}
