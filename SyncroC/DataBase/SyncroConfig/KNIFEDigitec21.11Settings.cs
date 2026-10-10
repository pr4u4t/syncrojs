using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class KNIFE21_11DigitecProtocolRevisionConverter : StringConverter
	{
		private string[] protocolRevision = null;

		public KNIFE21_11DigitecProtocolRevisionConverter()
			: base()
		{
      //PMASSARO_FI16/03/10 - Gestione cambio gap solo su livello in lavoro (versione 2.0)
      //this.protocolRevision = new string[] { "1.0" };
      this.protocolRevision = new string[] { "1.0", "2.0" };
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

	public class KNIFE21_11DigitecSettings : TaskSettings
	{
		bool enable;
		string protocolVersion;

		[Category("2. KNIFE21_11 Digitec Settings TASK SETTINGS"), Description("Determine whether the KNIFE21_11 Digitec task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		[Category("2. KNIFE21_11 Digitec Settings TASK SETTINGS"), TypeConverter(typeof(KNIFE21_11DigitecProtocolRevisionConverter)), Description("Protocol Revision")]
		public string ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		public KNIFE21_11DigitecSettings()
			: base()
    {
			this.sectionName = "S_KNIFE_FOSBER_21.11_DIGITEC" + this.sectionName;
			this.protocolVersion = "1.0";
			this.enable = false;
		}
	}
}
