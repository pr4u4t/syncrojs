using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using SyncroC.DataBase.LineConfig;


namespace SyncroC.DataBase.SyncroConfig
{
  [TypeConverter(typeof(Converters))]
	public class ThredBaseComms : SyncroBase
	{
		enum CommChannel 
		{ 
			SerialChannel = 0, 
			SocketChannel = 1
		}

		CommChannel channel;
		[Category("2. THRED COMMUNICATION SETTINGS"), PropertyOrder(30)]
		private CommChannel Channel
		{
			get { return channel; }
			set { channel = value; }
		}

		int timeout;
		[Category("2. THRED COMMUNICATION SETTINGS"), PropertyOrder(40)]
    public int Timeout
		{
			get { return timeout; }
			set { timeout = value; }
		}

		int maxTryals;
		[Category("2. THRED COMMUNICATION SETTINGS"), PropertyOrder(50)]
    public int MaxTryals
		{
			get { return maxTryals; }
			set { maxTryals = value; }
		}

		int iteSocketPortIndex;
		[Category("2. THRED COMMUNICATION SETTINGS"), PropertyOrder(60)]
		public int ITESocketPortIndex
		{
			get { return iteSocketPortIndex; }
			set { iteSocketPortIndex = value; }
		}

		SocketComm sck;
		[Category("2. THRED COMMUNICATION SETTINGS"), PropertyOrder(200)]
		public SocketComm Sck
		{
			get { return sck; }
			set { sck = value; }
		}

		public ThredBaseComms(string section)
    {
      //this.sectionName = section+this.sectionName;
			this.sectionName = section + "_HEADER";
			this.channel = CommChannel.SocketChannel;
      this.timeout = 0;
      this.maxTryals = 0;
      this.sck = new SocketComm(section);
			this.iteSocketPortIndex = 50;
    }
	}
}
