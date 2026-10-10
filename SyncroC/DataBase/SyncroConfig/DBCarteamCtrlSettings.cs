using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

//PMASSARO_FI01/02/11 - Aggiunto task di comunicazione con DB Carteam

namespace SyncroC.DataBase.SyncroConfig
{
  public class DBCarteamProtocolRevisionConverter : StringConverter
  {
    private string[] protocolRevision = null;

    public DBCarteamProtocolRevisionConverter()
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

  public class DBCarteamCtrlSettings : TaskSettings
  {
    bool enable;
    int wayToSetTheQuality;

    string protocolVersion = null;

    [Category("2. DB CARTEAM CTRL TASK SETTINGS"), Description("Determine whether the DB Carteam Ctrl task should be automatically started.")]
    public bool Enable
    {
      get { return enable; }
      set { enable = value; }
    }

    [Category("2. DB CARTEAM CTRL TASK SETTINGS"), Description("0 -> Send quality code using the name present in Syncro. 1 -> Differentiate the code by quality grammage.")]
    public int WayToSetTheQuality
    {
      get { return wayToSetTheQuality; }
      set { wayToSetTheQuality = value; }
    }

    [Category("2. DB CARTEAM CTRL TASK SETTINGS"), TypeConverter(typeof(DBCarteamProtocolRevisionConverter)), Description("Protocol Revision")]
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

    public DBCarteamCtrlSettings()
      : base()
    {
      this.sectionName = "S_DB_CARTEAM_CTRL" + this.sectionName;
      this.enable = false;
      this.wayToSetTheQuality = 0; 
      this.plcCommunication = new PLCComm("S_DB_CARTEAM_CTRL");

      this.protocolVersion = "1.0";
    }
  }
}
