using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
  public class GU_KohlerProtocolRevisionConverter : StringConverter
  {
    private string[] protocolRevision = null;

    public GU_KohlerProtocolRevisionConverter()
      : base()
    {
      this.protocolRevision = new string[] {"1.0"};
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

	public class GU_KohlerCtrlSettings : TaskSettings
	{
		bool enable;

    string protocolVersion = null;

		[Category("2. GU Kohler CTRL TASK SETTINGS"), Description("Determine whether the GU Kohler Ctrl task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

    [Category("2. GU Kohler CTRL TASK SETTINGS"), TypeConverter(typeof(GU_KohlerProtocolRevisionConverter)), Description("Protocol Revision")]
    public string ProtocolVersion
    {
      get { return this.protocolVersion; }
      set { this.protocolVersion = value; }
    }

		private PLCComm plcCommunication;
		[Category("3. TASKS WITH PLC COMMUNICATION SETTINGS"), Description("The parameters for the communication with PLC.")]
		public PLCComm PLCCommunication
		{
			get { return plcCommunication; }
			set { plcCommunication = value; }
		}

    public GU_KohlerCtrlSettings()
			: base()
    {
			this.sectionName = "S_GU_KOHLER_CTRL" + this.sectionName;
			this.enable = false;
			this.plcCommunication = new PLCComm("S_GU_KOHLER_CTRL");

      this.protocolVersion = "1.0";
		}
	}
}
