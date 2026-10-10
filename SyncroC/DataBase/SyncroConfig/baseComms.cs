using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using SyncroC.DataBase.LineConfig;


namespace SyncroC.DataBase.SyncroConfig
{

	[TypeConverter(typeof(Converters))]
	public class BaseCommsSettings : SyncroBase
	{
		enum CommChannel
		{
			SerialChannel = 0,
			SocketChannel = 1
		}

		SocketComm sck;
    [Category("2. TASKS WITH COMMUNICATION SETTINGS"), PropertyOrder(200), Description("")]
		public SocketComm Sck
		{
			get { return sck; }
			set { sck = value; }
		}

		CommChannel channel;
    [Category("2. TASKS WITH COMMUNICATION SETTINGS"), PropertyOrder(30), Description("")]
		private CommChannel Channel
		{
			get { return channel; }
			set { channel = value; }
		}

		int timeout;
    [Category("2. TASKS WITH COMMUNICATION SETTINGS"), PropertyOrder(40), Description("Tempo massimo per ricevere una risposta dal dispositivo collegato ( in msec )")]
		public int Timeout
		{
			get { return timeout; }
			set { timeout = value; }
		}

		int maxTryals;
    [Category("2. TASKS WITH COMMUNICATION SETTINGS"), PropertyOrder(50), Description("Numero di tentativi da fare in comunicazione con il dispositivo collegato prima di dare un errore.")]
		public int MaxTryals
		{
			get { return maxTryals; }
			set { maxTryals = value; }
		}

		private bool enable;
    [Category("2. TASKS WITH COMMUNICATION SETTINGS"), PropertyOrder(60), Description("Abilitazione all'avvio del task di comunicazione con il dispositivo collegato.")]
		public bool Enable
		{
			get { return enable; }
			set { enable = value; }
		}

		private int refresh_UTms;
    [Category("2. TASKS WITH COMMUNICATION SETTINGS"), PropertyOrder(70), Description("")]
		public int Refreh_UTms
		{
			get { return refresh_UTms; }
			set { refresh_UTms = value; }
		}

		public BaseCommsSettings(string section)
		{
			this.sectionName = section + "_HEADER";
			this.channel = CommChannel.SocketChannel;
			this.timeout = 0;
			this.maxTryals = 0;
			this.enable = false;
			this.refresh_UTms = 1000;
			this.sck = new SocketComm(section);
		}
	}
	
	
	
	[TypeConverter(typeof(Converters))]
	public class BaseComms : TaskSettings
	{
		enum CommChannel 
		{ 
			SerialChannel = 0, 
			SocketChannel = 1
		}

		SocketComm sck;
    [Category("2. TASKS WITH COMMUNICATION SETTINGS"), PropertyOrder(200)]
		public SocketComm Sck
		{
			get { return sck; }
			set { sck = value; }
		}

		//RFazzi_FI17/01/2008_1.Added task configurations on graphics
		//SerialComm ser;
		//[Category("2. TASKS WITH COMMUNICATION SETTINGS"), PropertyOrder(300)]
		//public SerialComm Ser
		//{
		//  get { return ser; }
		//  set { ser = value; }
		//}

		CommChannel channel;
    [Category("2. TASKS WITH COMMUNICATION SETTINGS"), PropertyOrder(30)]
		private CommChannel Channel
		{
			get { return channel; }
			set { channel = value; }
		}

		int timeout;
    [Category("2. TASKS WITH COMMUNICATION SETTINGS"), PropertyOrder(40)]
    public int Timeout
		{
			get { return timeout; }
			set { timeout = value; }
		}

		int maxTryals;
    [Category("2. TASKS WITH COMMUNICATION SETTINGS"), PropertyOrder(50)]
    public int MaxTryals
		{
			get { return maxTryals; }
			set { maxTryals = value; }
		}

    private bool enable;
    [Category("2. TASKS WITH COMMUNICATION SETTINGS"), PropertyOrder(60)]
    public bool Enable
    {
      get { return enable; }
      set { enable = value; }
    }

    //agrassi_FI22-10-08
    private int refresh_UTms;
    [Category("2. TASKS WITH COMMUNICATION SETTINGS"), PropertyOrder(70)]
    public int Refreh_UTms
    {
      get { return refresh_UTms; }
      set { refresh_UTms = value; }
    }

		//string protocolVersion;	//Version of the protocol
		//[Category("2. TASKS WITH COMMUNICATION SETTINGS"), PropertyOrder(20), Description("The version of the protocol.")]
		//public string ProtocolVersion
		//{
		//  get { return protocolVersion; }
		//  set { protocolVersion = value; }
		//}

		//int iteSocketPortIndex;
		//[Category("2. TASKS WITH COMMUNICATION SETTINGS"), PropertyOrder(50)]
		//public int ITESocketPortIndex
		//{
		//  get { return iteSocketPortIndex; }
		//  set { iteSocketPortIndex = value; }
		//}

    public BaseComms(string section)
    {
      this.sectionName = section+this.sectionName;
      this.channel = CommChannel.SocketChannel;
      this.timeout = 0;
      this.maxTryals = 0;
      this.enable = false;
			//RFazzi_FI17/01/2008_1.Added task configurations on graphics
      //this.protocolVersion = "0.0";
      this.sck = new SocketComm(section);
			//this.iteSocketPortIndex = 50;
      //this.ser = new SerialComm(section);
    }
	}
}
