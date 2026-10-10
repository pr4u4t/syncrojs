using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{

	public class KNIFE21_30ProtocolRevisionConverter : StringConverter
	{
		private string[] protocolRevision = null;

		public KNIFE21_30ProtocolRevisionConverter()
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

	public class KNIFE21_30Settings : TaskSettings
	{
		bool enable;
		string protocolVersion;

		[Category("2. KNIFE21_30 Settings TASK SETTINGS"), Description("Determine whether the KNIFE21_30 task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("2. KNIFE21_30 Settings TASK SETTINGS"), TypeConverter(typeof(KNIFE21_30ProtocolRevisionConverter)), Description("Protocol Revision")]
		public string ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}
		public KNIFE21_30Settings()
			: base()
    {
			this.sectionName = "S_KNIFE_FOSBER_21.30" + this.sectionName;
			this.enable = false;
			this.ProtocolVersion = "2.0";
		}
	}
}
