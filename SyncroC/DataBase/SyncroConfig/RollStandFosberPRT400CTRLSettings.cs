using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class RollStandFosberPRT400RevisionConverter : StringConverter
	{
		private string[] protocolRevision = null;

    public RollStandFosberPRT400RevisionConverter()
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

	public class RollStandFosberPRT400CTRLSettings : SyncroBase
	{

		string protocolVersion = null;
		BaseComms baseComms = null;

		[Category("2. RollStand PRT400 CTRL TASK SETTINGS"), TypeConverter(typeof(RollStandFosberPRT400RevisionConverter)), Description("Protocol Revision")]
		public string ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		[Category("3. RollStand PRT400 CTRL Comms SETTINGS"), Description("Comms settings")]
		public BaseComms BaseComms
		{
			get { return baseComms; }
			set { baseComms = value; }
		}

    public RollStandFosberPRT400CTRLSettings(string rollStandNumber)
			: base()
    {
      this.sectionName = "S_PRT_FOSBER_PRT400_CTRL" + rollStandNumber + "_HEADER";
			this.protocolVersion = "0.0";
      this.baseComms = new BaseComms("S_PRT_FOSBER_PRT400_CTRL" + rollStandNumber);
		}
	}
}
