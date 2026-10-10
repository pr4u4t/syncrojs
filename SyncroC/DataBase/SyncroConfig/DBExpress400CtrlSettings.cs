using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
  public class DBExpress400ProtocolRevisionConverter : StringConverter
  {
    private string[] protocolRevision = null;

    public DBExpress400ProtocolRevisionConverter()
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

	public class DBExpress400CtrlSettings : TaskSettings
	{
		bool enable;

    string protocolVersion = null;

		[Category("2. DB EXPRESS 400 CTRL TASK SETTINGS"), Description("Determine whether the DB Express400 Ctrl task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

    [Category("2. DB EXPRESS 400 CTRL TASK SETTINGS"), TypeConverter(typeof(DBExpress400ProtocolRevisionConverter)), Description("Protocol Revision")]
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

		public DBExpress400CtrlSettings()
			: base()
    {
			this.sectionName = "S_DB_FOSBER_EXPRESS400_CTRL" + this.sectionName;
			this.enable = false;
			this.plcCommunication = new PLCComm("S_DB_FOSBER_EXPRESS400_CTRL");

      this.protocolVersion = "1.0";
		}
	}
}
