using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using SyncroC.DataBase.LineConfig;

namespace SyncroC.DataBase.SyncroConfig
{
  [Description("Score types"), TypeConverter(typeof(Converters))]
  public class PLCSiemensComm : SyncroBase
	{
		string ipAddress;
    [Category("2. PLC COMMUNICATION"), Description("The IP address of the PLC.")]
		public string IpAddress
		{
			get { return ipAddress; }
			set { ipAddress = value; }
		}
		
    int remotePort;
    [Category("2. PLC COMMUNICATION"), Description("The remote port for the connection with the PLC.")]
    public int RemotePort
		{
			get { return remotePort; }
			set { remotePort = value; }
		}

    int slot;
    [Category("2. PLC COMMUNICATION"), Description("The slot for the connection with the PLC.")]
    public int Slot
    {
        get { return slot; }
        set { slot = value; }
    }

    int rack;
    [Category("2. PLC COMMUNICATION"), Description("The rack for the connection with the PLC.")]
      public int Rack
    {
        get { return rack; }
        set { rack = value; }
    }

      int timeout_UTms;
    [Category("2. PLC COMMUNICATION"), Description("Timeout (msec) for the communication with the PLC.")]
      public int Timeout_UTms
    {
        get { return timeout_UTms; }
        set { timeout_UTms = value; }
    }

      public PLCSiemensComm(string section)
    {
      this.sectionName = "_PLC";

      this.sectionName = section + this.sectionName;
      this.ipAddress = "127.0.0.1";
      this.remotePort = 102;
      this.rack = 0;
      this.slot = 2;
      this.timeout_UTms = 5000;
    }
	}
}
