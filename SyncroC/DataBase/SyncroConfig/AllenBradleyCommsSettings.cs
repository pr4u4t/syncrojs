using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using SyncroC.DataBase.LineConfig;

namespace SyncroC.DataBase.SyncroConfig
{
  [TypeConverter(typeof(Converters))]
  public class AllenBradleyCommsSettings: TaskSettings
  {
		bool enable;
    [Category("2. TASKS WITH PLC COMMUNICATION SETTINGS"), PropertyOrder(10), Description("Determine whether the task should be automatically started.")]
    public bool Enable
    {
      get { return enable; }
      set { enable = value; }
    }

    string protocolVersion;	//Version of the protocol
    [Category("2. TASKS WITH PLC COMMUNICATION SETTINGS"), PropertyOrder(20), Description("Versione del Protocollo da usare.")]
    public string ProtocolVersion
    {
      get { return protocolVersion; }
      set { protocolVersion = value; }
    }

    private PLCComm plcCommunication;
    [Category("2. TASKS WITH PLC COMMUNICATION SETTINGS"), PropertyOrder(30), Description("The parameters for the communication with PLC.")]
    public PLCComm PLCCommunication
    {
      get { return plcCommunication; }
      set { plcCommunication = value; }
    }

    public AllenBradleyCommsSettings(string section): base()
    {
      this.sectionName = section + this.sectionName;
      this.enable = false;
      this.protocolVersion = "0.0";
      this.plcCommunication = new PLCComm(section);
    }
  }
}
