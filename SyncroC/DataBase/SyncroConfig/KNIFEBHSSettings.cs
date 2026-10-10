using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{

	public class KNIFEBHSProtocolRevisionConverter : StringConverter
	{
		private string[] protocolRevision = null;

		public KNIFEBHSProtocolRevisionConverter()
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

	public class KnifeBHSSettings : TaskSettings
	{
		bool enable;
		string protocolVersion;

		[Category("2. KNIFEBHS Settings TASK SETTINGS"), Description("Determine whether the KNIFEBHS task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("2. KNIFEBHS Settings TASK SETTINGS"), TypeConverter(typeof(KNIFEBHSProtocolRevisionConverter)), Description("Protocol Revision")]
		public string ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}
		public KnifeBHSSettings()
			: base()
    {
			this.sectionName = "S_KNIFE_BHS" + this.sectionName;
			this.enable = false;
			this.ProtocolVersion = "1.0";
		}
	}
}
