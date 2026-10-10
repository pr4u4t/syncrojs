using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

//PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl
namespace SyncroC.DataBase.SyncroConfig
{
  public class FanFoldHOETTENProtocolRevisionConverter : StringConverter
  {
    private string[] protocolRevision = null;

    public FanFoldHOETTENProtocolRevisionConverter()
      : base()
    {
      this.protocolRevision = new string[] { "0.0"};
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

  public class FanFoldHOETTENCommsSettings : TaskSettings
  {
    bool enable;
    string protocolVersion;

    [Category("2. FANFOLD HOETTEN TASK SETTINGS"), Description("Determine whether the FanFold HOETTEN Comms task should be automatically started.")]
    public bool Enable
    {
      get { return enable; }
      set { enable = value; }
    }

    [Category("2. FANFOLD HOETTEN LRE TASK SETTINGS"), TypeConverter(typeof(FanFoldHOETTENProtocolRevisionConverter)), Description("Protocol Revision")]
    public string ProtocolVersion
    {
      get { return this.protocolVersion; }
      set { this.protocolVersion = value; }
    }

    private PLCSiemensComm plcCommunication;
    [Category("3. TASKS WITH PLC COMMUNICATION SETTINGS"), Description("The parameters for the communication with PLC.")]
    public PLCSiemensComm PLCCommunication
    {
      get { return plcCommunication; }
      set { plcCommunication = value; }
    }

    public FanFoldHOETTENCommsSettings(string level)
      : base()
    {
      this.sectionName = "S_FAN_FOLD_HOTTEN_CTRL" + level + this.sectionName;
      this.enable = false;
      this.ProtocolVersion = "0.0";
      this.plcCommunication = new PLCSiemensComm("S_FAN_FOLD_HOTTEN_CTRL" + level);
    }
  }
}

