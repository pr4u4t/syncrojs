using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class WECBMUProtocolRevisionConverter : StringConverter
	{
		private string[] protocolRevision = null;

		public WECBMUProtocolRevisionConverter()
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

  public class WECBMUSettings :SyncroBase 
  {
		string protocolVersion = null;
		BaseComms baseComms = null;

		[Category("2. WEC BMU TASK SETTINGS"), TypeConverter(typeof(WECBMUProtocolRevisionConverter)), Description("Protocol Revision")]
		public string ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		[Category("3. WEC BMU  Comms SETTINGS"), Description("Comms settings")]
		public BaseComms BaseComms
		{
			get { return baseComms; }
			set { baseComms = value; }
		}

    public WECBMUSettings()
      : base()
    {
      this.sectionName = "S_WEC_BMU" + this.sectionName;
			this.protocolVersion = "1.0";
			this.baseComms = new BaseComms("S_WEC_BMU" );
    }
  }
}
