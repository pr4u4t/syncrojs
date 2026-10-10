using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
	public class WECSyncroProtocolRevisionConverter : StringConverter
	{
		private string[] protocolRevision = null;

		public WECSyncroProtocolRevisionConverter()
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

	public class WECSyncroSettings : SyncroBase
  {
		string protocolVersion;
		BaseComms baseComms = null;

		[Category("2. WEC SYNCRO TASK SETTINGS"), TypeConverter(typeof(WECSyncroProtocolRevisionConverter)), Description("Protocol Revision")]
		public string ProtocolVersion
		{
			get { return this.protocolVersion; }
			set { this.protocolVersion = value; }
		}

		[Category("3. WEC SYNCRO  Comms SETTINGS"), Description("Comms settings")]
		public BaseComms BaseComms
		{
			get { return baseComms; }
			set { baseComms = value; }
		}
    public WECSyncroSettings()
      : base()
    {
      this.sectionName = "S_WEC_SYNCRO" + this.sectionName;
			this.protocolVersion = "1.0";
			this.baseComms = new BaseComms("S_WEC_SYNCRO");
		}
  }
}
