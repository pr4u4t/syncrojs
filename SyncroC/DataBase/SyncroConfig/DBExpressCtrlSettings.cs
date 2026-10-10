using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

namespace SyncroC.DataBase.SyncroConfig
{
  //PMASSARO_29-01-09 Added Protocol Version
  public class DBExpressProtocolRevisionConverter : StringConverter
  {
    private string[] protocolRevision = null;

    public DBExpressProtocolRevisionConverter()
      : base()
    {
      this.protocolRevision = new string[] {"1.0", "1.1", "1.2" };
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

	public class DBExpressCtrlSettings : TaskSettings
	{
		bool enable;
    int wayToSetTheQuality; //PMASSARO_FI23/09/10 - Rivista gestione della qualità inviata ad EXPRESS

    //PMASSARO_29-01-09 Added Protocol Version
    string protocolVersion = null;

		[Category("2. DB EXPRESS CTRL TASK SETTINGS"), Description("Determine whether the DB Express Ctrl task should be automatically started.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

    //PMASSARO_FI23/09/10 - Rivista gestione della qualità inviata ad EXPRESS
    [Category("2. DB EXPRESS CTRL TASK SETTINGS"), Description("0 -> Send quality code using the name present in Syncro. 1 -> Differentiate the code by quality grammage.")]
    public int WayToSetTheQuality
    {
      get { return wayToSetTheQuality; }
      set { wayToSetTheQuality = value; }
    }

    //PMASSARO_29-01-09 Added Protocol Version
    [Category("2. DB EXPRESS CTRL TASK SETTINGS"), TypeConverter(typeof(DBExpressProtocolRevisionConverter)), Description("Protocol Revision")]
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

		public DBExpressCtrlSettings()
			: base()
    {
			this.sectionName = "S_DB_FOSBER_EXPRESS_CTRL" + this.sectionName;
			this.enable = false;
      this.wayToSetTheQuality = 0; //PMASSARO_FI23/09/10 - Rivista gestione della qualità inviata ad EXPRESS
			this.plcCommunication = new PLCComm("S_DB_FOSBER_EXPRESS_CTRL");

      //PMASSARO_29-01-09 Added Protocol Version
      this.protocolVersion = "1.1";
		}
	}
}
