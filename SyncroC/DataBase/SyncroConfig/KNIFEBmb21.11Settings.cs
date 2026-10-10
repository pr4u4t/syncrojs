using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class KNIFE21_11BmbProtocolRevisionConverter : StringConverter
	{
		private string[] protocolRevision = null;

		public KNIFE21_11BmbProtocolRevisionConverter()
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

	public class KNIFE21_11BmbSettings : TaskSettings
	{
		bool enable;
		string protocolVersion;

		[Category("2. KNIFE21_11 Bmb Settings TASK SETTINGS"), Description("Determine whether the KNIFE21_11 Bmb task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("2. KNIFE21_11 Bmb Settings TASK SETTINGS"), TypeConverter(typeof(KNIFE21_11BmbProtocolRevisionConverter)), Description("Protocol Revision")]
		public string ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		public KNIFE21_11BmbSettings()
			: base()
    {
            this.sectionName = "S_KNIFE_FOSBER_21.11_BMB" + this.sectionName;
			this.protocolVersion = "1.0";
			this.enable = false;
		}
	}
}
