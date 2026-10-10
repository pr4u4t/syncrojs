using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using SyncroC.DataBase.LineConfig;

namespace SyncroC.DataBase.SyncroConfig
{
  //agrassi_FI22-02-09
  public class AcceptConnectionConverter : StringConverter
  {
    private string[] acceptConnection = null;

    public AcceptConnectionConverter(): base()
    {
      this.acceptConnection = new string[] { "N", "Y" };
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return new StandardValuesCollection(this.acceptConnection);
    }
  }

  //agrassi_FI18-01-2010_2
  public class NetworkProtocolConverter : StringConverter
  {
    private string[] acceptConnection = null;

    public NetworkProtocolConverter(): base()
    {
      this.acceptConnection = new string[] { "UDP", "TCP" };
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return new StandardValuesCollection(this.acceptConnection);
    }
  }

  [TypeConverter(typeof(Converters))]
	public class SocketComm : SyncroBase
	{
		string ipAddress;
    [Description("Indirizzo IP del dispositivo collegato a Syncro.")]
		public string IpAddress
		{
			get { return ipAddress; }
			set { ipAddress = value; }
		}
		string networkProtocol;	//Socket Protocol UDP , TCP/IP

    //agrassi_FI18-01-2010_2
    [TypeConverter(typeof(NetworkProtocolConverter)), Description("Impostare a TCP o UDP secondo il protocollo utilizzato")]
		public string NetworkProtocol
		{
			get { return networkProtocol; }
			set { networkProtocol = value; }
		}

		int localPort;					//Local port for the comunication
    [Description("Numero di porta su cui Syncro attende i messaggi dal dispositivo collegato.")]
		public int LocalPort
		{
			get { return localPort; }
			set { localPort = value; }
		}

		int remotePort;					//Remote port for the comunication
    [Description("Numero di porta a cui Syncro invia il messaggio")]
		public int RemotePort
		{
			get { return remotePort; }
			set { remotePort = value; }
		}

		int iteSocketPortIndex;
    [Description("Questo parametro deve essere utilizzato solo se la comunicazione con il dispositivo collegato è in TCP/IP e si sfrutta la libreria ITE. Il valore qui impostato deve essere univoco. Tipicamente si usa l'ITE per comunicazioni con i PLC Omron e Siemens.")]
		public int ITESocketPortIndex
		{
			get { return iteSocketPortIndex; }
			set { iteSocketPortIndex = value; }
		}

    //agrassi_FI22-02-09
    string acceptConnection;
    [TypeConverter(typeof(AcceptConnectionConverter)), Description("Da usare se il protocollo di comunicazione è in TCP/IP e il dispositivo collegato a Syncro deve eseguire la connessione.")]
    public string Accept
    {
      get { return this.acceptConnection; }
      set { this.acceptConnection = value; }
    }

    public SocketComm(string section)
    {
      this.sectionName = "_SOCKET";

      this.sectionName = section + this.sectionName;
      this.ipAddress = "127.0.0.1";
      this.networkProtocol = "UDP";
      this.remotePort = 0;
      this.localPort = 0;
			this.iteSocketPortIndex = 50;
      //agrassi_FI22-02-09
      this.acceptConnection = "N";
    }
	}
}
