using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class SplicerFosberLNK400RevisionConverter : StringConverter
	{
		private string[] protocolRevision = null;

		public SplicerFosberLNK400RevisionConverter()
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

	public class SplicerFosberLNK400CTRLSettings : SyncroBase
	{

		string protocolVersion = null;
		BaseComms baseComms = null;

		[Category("2. Splicer LINK400 CTRL TASK SETTINGS"), TypeConverter(typeof(SplicerFosberLNK400RevisionConverter)), Description("Protocol Revision")]
		public string ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		[Category("3. Splicer LINK400 CTRL Comms SETTINGS"), Description("Comms settings")]
		public BaseComms BaseComms
		{
			get { return baseComms; }
			set { baseComms = value; }
		}

		public SplicerFosberLNK400CTRLSettings(string splicerNumber)
			: base()
    {
			this.sectionName = "S_SPL_FOSBER_LINK400_CTRL" + splicerNumber + "_HEADER";
			this.protocolVersion = "0.0";
			this.baseComms = new BaseComms("S_SPL_FOSBER_LINK400_CTRL" + splicerNumber);
		}
	}
}
