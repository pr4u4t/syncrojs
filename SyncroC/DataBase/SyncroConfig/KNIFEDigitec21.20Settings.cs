using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class KNIFE21_20DigitecProtocolRevisionConverter : StringConverter
	{
		private string[] protocolRevision = null;

		public KNIFE21_20DigitecProtocolRevisionConverter()
			: base()
		{
      //PMASSARO_FI18/03/09 - Added Protocol 0.1 into possible values of knife 21.20 electricale task
			/*
       this.protocolRevision = new string[] { "0.0" };
      */
      this.protocolRevision = new string[] { "0.0", "0.1" };
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

	public class KNIFE21_20DigitecSettings : TaskSettings
	{
		bool enable;
		string protocolVersion;

		[Category("2. KNIFE21_20 Digitec Settings TASK SETTINGS"), Description("Determine whether the KNIFE21_20 Digitec task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("2. KNIFE21_20 Digitec Settings TASK SETTINGS"), TypeConverter(typeof(KNIFE21_20DigitecProtocolRevisionConverter)), Description("Protocol Revision")]
		public string ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		public KNIFE21_20DigitecSettings()
			: base()
    {
			this.sectionName = "S_KNIFE_FOSBER_21.20" + this.sectionName;
			this.enable = false;
			this.protocolVersion = "0.0";
		}
	}
}
