using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using SyncroC.DataBase.LineConfig;

namespace SyncroC.DataBase.SyncroConfig
{
  [Description("Score types"), TypeConverter(typeof(Converters))]
  public class PLCComm : SyncroBase
	{
		string ipAddress;
    [Category("2. PLC COMMUNICATION"), Description("Indirizzo IP del PLC collegato a Syncro.")]
		public string IpAddress
		{
			get { return ipAddress; }
			set { ipAddress = value; }
		}
		
    string plcName;
    [Category("2. PLC COMMUNICATION"), Description("Tipo PLC Allen Bradley collegato a Syncro. Per le macchine Fosber, il PLC usato è il 'CONTROLLOGIX'.")]
    public string PlcName
		{
			get { return plcName; }
			set { plcName = value; }
		}
		
    int remotePort;
    [Category("2. PLC COMMUNICATION"), Description("Numero di porta a cui Syncro deve mandare il messaggio. Per i PLC usati sulle macchine Fosber è la 44818.")]
    public int RemotePort
		{
			get { return remotePort; }
			set { remotePort = value; }
		}

    public PLCComm(string section)
    {
      this.sectionName = "_PLC";

      this.sectionName = section + this.sectionName;
      this.ipAddress = "127.0.0.1";
      this.plcName = "None";
      this.remotePort = 0;
    }
	}
}
