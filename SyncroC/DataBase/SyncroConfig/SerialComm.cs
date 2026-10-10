using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using SyncroC.DataBase.LineConfig;

namespace SyncroC.DataBase.SyncroConfig
{
  [TypeConverter(typeof(Converters))]
	public class SerialComm : SyncroBase
	{
		int comPort;

		public int ComPort
		{
			get { return comPort; }
			set { comPort = value; }
		}
		int baudRate;

		public int BaudRate
		{
			get { return baudRate; }
			set { baudRate = value; }
		}
		int dataBit;

		public int DataBit
		{
			get { return dataBit; }
			set { dataBit = value; }
		}
		int stopBit;

		public int StopBit
		{
			get { return stopBit; }
			set { stopBit = value; }
		}
		char parity;

		public char Parity
		{
			get { return parity; }
			set { parity = value; }
		}

    public SerialComm(string section)
    {
      this.sectionName = "_SERIAL";

      this.sectionName = section + this.sectionName;
      this.comPort = 0;
      this.baudRate = 0;
      this.dataBit = 0;
      this.stopBit = 0;
      this.parity = 'n';
    }
	}
}
