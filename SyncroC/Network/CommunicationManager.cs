using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

using SyncroC.Utility;
using SyncroC.Network.Icmp;

namespace SyncroC.Network
{
	/// <summary>
	/// Implements all the client communication facilities.
	/// TCP and UDP socket connections are used and are available for users.
	/// </summary>
	public class CommunicationManager
	{
		//Constants for TCP message headers 
		/// <summary>
		/// String representing the page manager of the SyncroS server task (to be used as TCP message header)
		/// </summary>
		public readonly string SYNCROS_PAGE_MANAGER = "P";
		/// <summary>
		/// String representing the DBmanager of the SyncroS server task (to be used as TCP message header)
		/// </summary>
		public readonly string SYNCROS_DB_MANAGER = "D";
		/// <summary>
		/// String representing the RTC of the SyncroS server task (to be used as TCP message header)
		/// </summary>
		public readonly string SYNCROS_RTC_MANAGER = "R";
		/// <summary>
		/// String representing the CommunicationManager of the SyncroS server task (to be used as TCP message header)
		/// </summary>
		public readonly string SYNCROS_COMM_MANAGER = "C";//SYNCROS_COMM_MANAGER

		//Constants for UDP message headers 
		/// <summary>
		/// String representing the Communication type messages for the Starter server task (to be used as UDP message header)
		/// </summary>
		public readonly string STARTER_COMM = "C";
		/// <summary>
		/// String representing the Task settings type messages for the Starter server task (to be used as UDP message header)
		/// </summary>
		public readonly string STARTER_TASK_SETTINGS = "T";
		/// <summary>
		/// String representing the Connection request type messages for the Starter server task (to be used as UDP message header)
		/// </summary>
		public readonly string STARTER_CONN_REQ = "R";

		public readonly string STARTER_DEBUG_REQ = "D";

		/// <summary>
		/// Maximum number of trials to send string to server
		/// </summary>
		private int TCP_MAX_TRIALS = 3;
		/// <summary>
		/// Time to wait first time for the server answer in UDP connection.
		/// </summary>
		private int UDP_MILLISECONDS_MIN_DELAY = 2500;
		/// <summary>
		/// Step of time increment beetwen two consectutive UDP connection try.
		/// </summary>
		private int UDP_MILLISECONDS_INCREMENT = 250;
		/// <summary>
		/// Maximum time to wait beetwen two UDP connection try.
		/// </summary>
		private int UDP_MILLISECONDS_MAX_DELAY = 4500;
		/// <summary>
		/// Milliseconds interval beetwen two check for udp message receiving.
		/// </summary>
		private int CHECK_UDP_RECEIVED_INTERVAL = 10;

		//constats (ping)
		/// <summary>
		/// Time to wait first time pinging the server.
		/// </summary>
		private int PING_MILLISECONDS_MIN_DELAY = 100;
		/// <summary>
		/// Step of time increment beetwe two consectutive ping try.
		/// </summary>
		private int PING_MILLISECONDS_INCREMENT = 100;
		/// <summary>
		/// Maximum time to wait beetwen two ping try.
		/// </summary>
		private int PING_MILLISECONDS_MAX_DELAY = 1500;

		//other members
		/// <summary>
		/// Needed to realize the singleton pattern.
		/// </summary>
		private static CommunicationManager communicationManager = null;
		/// <summary>
		/// Name of the database for widch the end transmit command is beeing waited. 
		/// "" means no end transmit command is beeing waited
		/// </summary>
		private string NameDBEndTransmitWaited = "";
		/// <summary>
		/// The Server IP address. Used for tcp and udp transmissions.
		/// </summary>
		private String remoteIP;
		/// <summary>
		/// Remote port for TCP transmition.
		/// Sent by the server using UDP transmission.
		/// </summary>
		private int remoteTcpPort = 0;
		/// <summary>
		/// Contains the remote TCP port as a string which to connect received by the server
		/// task starter using UDP connection.
		/// </summary>
		private string remoteTcpPortString = null;

		//sockets
		/// <summary>
		/// used for UDP transmissions.
		/// </summary>
		private UDPSocket udpSocket = null;
		/// <summary>
		/// used for TCP transmissions.
		/// </summary>
		private TCPSocket tcpSocket = null;

		//threads
		/// <summary>
		/// thread used to make asyncronous the receiving on the TCPsocket.
		/// </summary>
		private Thread threadTCPReceive;
		/// <summary>
		/// thread used to receive on UDP socket.
		/// </summary>
		private Thread threadUDPReceive;

		//queues
		/// <summary>
		/// Contains all the TCPmessage to be executed by the PageManager (Switch() and Reflect() methods).
		/// (syncronized)
		/// </summary>
		public Queue PMcommandsQueue;
		/// <summary>
		/// Contains all the TCPmessage to be executed by the DBManager (Switch() and Reflect() methods).
		/// (syncronized)
		/// </summary>
		public Queue DBMcommandsQueue;
		/// <summary>
		/// Contains all the TCPmessage to be executed by the RTCManager (Switch() and Reflect() methods).
		/// (syncronized)
		/// </summary>
		public Queue RTCMcommandsQueue;

		private DateTime lastCommError;
		public DateTime LastCommError
		{
			get { return lastCommError; }
		}

		//useful references
		private PageManager pm = PageManager.getPageManager();

    //agrassi_FI20-01-09
    private bool connectionRefused = false;
    public bool ConnectionRefused
    { get { return this.connectionRefused; } }

    /// <summary>
		/// Builder.
		/// </summary>
    /// 
    private DateTime startTime = DateTime.Now;
		private CommunicationManager()
		{
			if ((!Settings.getSettings().dataBase.RestoreDBLanguages) || (!Settings.getSettings().configuration.getOnServer()))
			{
        this.startTime = DateTime.Now;

				remoteIP = Settings.getSettings().network.IpServerAddress;

				//check network connection
				bool connected = false;
				int timeToWait = PING_MILLISECONDS_MIN_DELAY;
				bool firstLoop = true;
        //agrassi_FI19-02-09_3
				//while (!connected)
        while ((!connected) && (!this.connectionRefused) && (!pm.CloseAll))
				{
					if (!firstLoop)
						pm.CommFailure("Trying to connect to the server \"" + remoteIP + "\".", true);
					connected = IsConnected(remoteIP, 2, timeToWait);
					timeToWait = (timeToWait + PING_MILLISECONDS_INCREMENT > PING_MILLISECONDS_MAX_DELAY)
						? PING_MILLISECONDS_MAX_DELAY : (timeToWait + PING_MILLISECONDS_INCREMENT);
					firstLoop = false;
				}

				//queue initialization
				Queue _PMcommandsQueue = new Queue();
				PMcommandsQueue = Queue.Synchronized(_PMcommandsQueue);
				Queue _DBMcommandsQueue = new Queue();
				DBMcommandsQueue = Queue.Synchronized(_DBMcommandsQueue);
				Queue _RTCMcommandsQueue = new Queue();
				RTCMcommandsQueue = Queue.Synchronized(_RTCMcommandsQueue);

				//start communication
				UDPConnection();
        //agrassi_FI20-01-09
        //agrassi_FI19-02-09_3
        //if (this.connectionRefused)
        if (this.connectionRefused || pm.CloseAll)
        {
          return;
        }
				TCPConnection();
				threadTCPReceive = new Thread(new ThreadStart(ReceiveTcp));
				threadTCPReceive.Name = "receive TCP";
				threadTCPReceive.Priority = ThreadPriority.AboveNormal;
				threadTCPReceive.Start();
			}
		}

		/// <summary>
		/// Ensure all threds are stopped.
		/// </summary>
		~CommunicationManager()
		{
			//attenzione a queste due
			StopReceiveTcp();
			if (tcpSocket != null)
				tcpSocket.close();
			StopReceiveUdp();
			if (udpSocket != null)
				udpSocket.close();
		}

		/// <summary>
		/// Factory.
		/// </summary>
		/// <returns>The one and only CommunicationManager instance of the application.</returns>
		public static CommunicationManager GetCommunicationManager()
		{
			if (communicationManager == null)
				communicationManager = new CommunicationManager();
			return communicationManager;
		}

		/// <summary>
		/// Ping the specified computer.
		/// </summary>
		/// <param name="remoteIP">Ip of the computer to ping.</param>
		/// <param name="count">maximum number of try.</param>
		/// <param name="timeout"></param>
		/// <returns>true if the specified computer answer the ping, false otherwise.</returns>
		private bool IsConnected(string remoteIP, int count, int timeout)
		{
			if (count <= 0 || timeout <= 0)
			{
				Logger.LogError(this, "IsConnected - parameter \"count\" or \"timeout\" is invalid.");
				return false;
			}
			try
			{
				IPAddress hostIP = System.Net.IPAddress.Parse(remoteIP);
				SyncroC.Network.Icmp.Icmp icmp = new SyncroC.Network.Icmp.Icmp(hostIP);
				TimeSpan ret;
        //agrassi_FI19-02-09_3
        //while (count > 0)
        while ((count > 0) && (!this.connectionRefused) && (!pm.CloseAll))
        {
				  try
					{
						pm.Invoke(new UIMethod2(pm.CommFailure), new Object[] { "Pinging \"" + remoteIP + "\".", true });
						ret = icmp.Ping(timeout);
						if (ret.Equals(TimeSpan.MaxValue))
						{
							pm.Invoke(new UIMethod2(pm.CommFailure), new Object[] { "ping \"" + remoteIP + "\" failed.", true });
							Logger.LogError(this, "IsConnected - ping \"" + remoteIP + "\" failed.");
						}
						else
						{
							pm.Invoke(new UIMethod2(pm.CommFailure),
								new Object[] { "Ping \"" + remoteIP + "\" succeded: reply in " + Math.Round(ret.TotalMilliseconds).ToString() + " milliseconds.", true });
							return true;
						}
					}
					catch (Exception ex)
					{
						Logger.LogError(this, "IsConnected - Network error.", ex);
						return false;
					}
					count--;
				}
			}
			catch (FormatException ex)
			{//may be thrown by the IPAddress.Parse
				Logger.LogError(this, "IsConnected - remoteIp " + remoteIP + "is invalid.", ex);
				return false;
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "IsConnected - Error while pinging the host \"" + remoteIP + "\".", ex);
				return false;
			}
			return false;
		}

		/// <summary>
		/// Start an UDP connection in anyway.
		/// If a TCP port has NOT been specified in configuration (common situation) 
		/// then ask the server for a TCP port and when received set the global variable remoteTCPPort with the received value.
		/// If a TCP port has been specified (usually for SyncroS debug purpose) then the global variable remoteTCPPort is set to the specified value.
		/// </summary>
		private void UDPConnection()
		{
			Settings s = Settings.getSettings();
			udpSocket = new UDPSocket(remoteIP, s.network.UdpSendPort, s.network.UdpReceivePort+s.network.ClientID-1);
			string ret = "";
			while (!ret.Equals(UDPSocket.RETURN_OK) )
				ret = udpSocket.initialize();
			if (s.network.TcpPort != 0)
			{
				//a TCP port to be used for debugging SyncroS server task has been specified in the Syncro.ini file
				//so instead of connect in UDP to get the port, the specified port is going to be used.
				remoteTcpPort = s.network.TcpPort;
			}
			else
			{//a TCP port to connect has NOT been specified in the Syncro.ini file so 
				//UDP connection is going to be attemped to get the TCP port number to use for TCP communication
				int timeOut = UDP_MILLISECONDS_MIN_DELAY;
				do
				{
					//sending udp message
					bool sent = false;
					while (!sent)
					{
						//RFAZZI_FI02/10/2009_1. Aggiunta possibilità di avviare più client sul solito PC.
            //agrassi_FI23-12-2010_1
            //sent = SendUdp(new UDPMessage(STARTER_CONN_REQ, "OpenConnection(" + s.network.ClientID.ToString() + ")"));
            sent = SendUdp(new UDPMessage(STARTER_CONN_REQ, "OpenConnection(" + s.network.ClientID.ToString() + "," + Environment.UserName + ")"));
          }
					pm.Invoke(new UIMethod2(pm.CommFailure), new Object[] { "UDP message sent, waiting for reply...", true });
					//start receiving udp answer
					if (threadUDPReceive == null || !threadUDPReceive.IsAlive)
					{
						threadUDPReceive = new Thread(new ThreadStart(ReceiveUdp));
						threadUDPReceive.Name = "Receive UDP";
						if (!threadUDPReceive.ThreadState.Equals(System.Threading.ThreadState.Running))
						{
							try
							{
								threadUDPReceive.Start();
							}
							catch (ThreadStateException e)
							{
								Logger.LogError(this, "UDPConnection - Cannot start thread " + threadUDPReceive.Name + ", the thread has already been started.", e);
							}
							catch (Exception e)
							{
								Logger.LogError(this, "UDPConnection - Cannot start thread " + threadUDPReceive.Name + ".", e);
							}
						}
					}
					int residualTimeOut = timeOut;
					while (remoteTcpPortString == null && residualTimeOut > 0)
					{
						Thread.Sleep(CHECK_UDP_RECEIVED_INTERVAL);
						residualTimeOut -= CHECK_UDP_RECEIVED_INTERVAL;
					}
					if (remoteTcpPortString == null)
					{		//timeout was expired before receiving the message
						string message = "UDP connection \"" + remoteIP + "\" failed: " + timeOut.ToString() + " milliseconds had been waited but the server hasn't answered yet.";
						pm.Invoke(new UIMethod2(pm.CommFailure), new Object[] { message, true });
						Logger.LogError(this, message);
						if (timeOut == UDP_MILLISECONDS_MAX_DELAY)
						{		//server is not answering for long time... check again for connection (ping).
							bool connected = false;
							timeOut = PING_MILLISECONDS_MIN_DELAY;
              //agrassi_FI19-02-09_3
							//while (!connected)
              while ((!connected) && (!this.connectionRefused) && (!pm.CloseAll))
							{
								connected = IsConnected(remoteIP, 2, timeOut);
								timeOut = (timeOut + PING_MILLISECONDS_INCREMENT > PING_MILLISECONDS_MAX_DELAY)
									? PING_MILLISECONDS_MAX_DELAY : (timeOut + PING_MILLISECONDS_INCREMENT);
							}
							timeOut = UDP_MILLISECONDS_MIN_DELAY;
						}
						timeOut = (timeOut + UDP_MILLISECONDS_INCREMENT > UDP_MILLISECONDS_MAX_DELAY)
							? UDP_MILLISECONDS_MAX_DELAY : (timeOut + UDP_MILLISECONDS_INCREMENT);
					}
					else
					{		//message received
						try
						{
							remoteTcpPort = Convert.ToInt32(remoteTcpPortString);
						}
						catch (Exception ex)
						{
							remoteTcpPortString = null; //ensure keep trying receiving
							Logger.LogError(this, "UDPConnection - wrong remote TCP port received \"" + remoteTcpPortString + "\"", ex);
						}
					}
				}
        //agrassi_FI20-01-09
        //while (remoteTcpPortString == null);
        //agrassi_FI19-02-09_£
        //while ((remoteTcpPortString == null) && (!this.connectionRefused));
        while ((remoteTcpPortString == null) && (!this.connectionRefused) && (!pm.CloseAll)) ;
        remoteTcpPortString = null;
        //agrassi_FI20-01-09
        //agrassi_FI19-02-09_£
        //if (this.connectionRefused)
        if (this.connectionRefused || pm.CloseAll)
        {
          return;
        }
			}
		}

		/// <summary>
		/// Start a TCP connection with the port received in udp.
		/// </summary>
		private void TCPConnection()
		{
			tcpSocket = new TCPSocket(remoteIP, remoteTcpPort);
			string ret = tcpSocket.initialize();
			if (ret.StartsWith(TCPSocket.RETURN_ERROR))
				TCPErrorHandler(ret);
			pm.Invoke(new UIMethod0(pm.CommRestored));
		}

		/// <summary>
		///  Asyncronously (because running on an independent thread) receive data from UDP connection.
		///  And put the message in the udpReceived "global" variable to make available to the others thread. 
		/// </summary>
		private void ReceiveUdp()
		{
			UDPMessage msg = udpSocket.Receive();
			if (msg == null)
			{
				Logger.LogError(this, "ReceiveUdp - received a null message.");
				return;
			}
			if (msg.error.StartsWith(UDPSocket.RETURN_ERROR))
			{
				Logger.LogError(this, "ReceivedUdp - error receiving udp " + msg.error);
				return;
			}
			if (msg.header.Equals(String.Empty))
			{
				Logger.LogError(this, "ReceiveUdp - wrong message: " + msg.ToString());
				return;
			}
			switch (msg.header[0])
			{
				case 'r':
				case 'R':
					ConnectionRequest(msg.text);
					break;
        //agrassi_FI03-11-2010_3
        case 'U':
          UpdateVersion(msg.text);
          break;
				default:
					Logger.LogError(this, "ReceiveUDP - wrong header \"" + msg.header + "\".");
					break;
			}
		}

		/// <summary>
		/// Management of ConnectionRequest type commands.
		/// </summary>
		/// <param name="text">text of the message to be executed.</param>
		private void ConnectionRequest(string text)
		{
			if (!text.EndsWith(")"))
			{
				Logger.LogError(this, "ConnectionRequest - wrong message received: " + text);
				return;
			}
			text = text.Substring(0, text.Length - 1); //remove final ")"
			if (text.StartsWith("ConnectionDone("))
			{
				string connectionDataString = text.Substring(text.IndexOf('(') + 1);
				string[] connectionData = connectionDataString.Split(',');
				//RFAZZI_FI04/02/08, RFAZZI_FI01/08/2009_1
				if (connectionData.Length != 20)
				{
					Logger.LogError(this, "ConnectionRequest - wrong parameter number in the message received: " + text);
					return;
				}
				remoteTcpPortString = connectionData[0];
				Settings.getSettings().forms.FirstPage = connectionData[1];
				Settings.getSettings().forms.CorrugatorDirectionRight = Convert.ToBoolean(connectionData[2]);
				Settings.getSettings().configuration.DryEndControl = Convert.ToBoolean(connectionData[3]);
				Settings.getSettings().configuration.WetEndControl = Convert.ToBoolean(connectionData[4]);
				Settings.getSettings().configuration.PMac400 = Convert.ToBoolean(connectionData[5]);
				Settings.getSettings().configuration.PRC = Convert.ToBoolean(connectionData[6]);
				Settings.getSettings().facility.EditSetupQueue = Convert.ToBoolean(connectionData[7]);
				Settings.getSettings().facility.ChangeAOCMode = Convert.ToBoolean(connectionData[8]);
				Settings.getSettings().facility.EditOperatorSettings = Convert.ToBoolean(connectionData[9]);
				Settings.getSettings().facility.AssignDowntimes = Convert.ToBoolean(connectionData[10]);
				Settings.getSettings().facility.ChangeShift = Convert.ToBoolean(connectionData[11]);
				Settings.getSettings().facility.ChangeWETAutoManMode = Convert.ToBoolean(connectionData[12]);
				Settings.getSettings().facility.LowLevelEnabled = Convert.ToBoolean(connectionData[13]);
				Settings.getSettings().facility.ManualOrdersSending = Convert.ToBoolean(connectionData[14]);
				//ABello_FI12/07/2007_1 Bisogna ricalcolare le pagine dopo che il server ha mandato la configurazione
				PageManager.getPageManager().pagesHistorian.Reset();
				pm.Invoke(new UIMethod1(pm.setSyncroVersion), new object[] { connectionData[15] });
				pm.Invoke(new UIMethod1(pm.setSyncroDataVersion), new object[] { connectionData[16] });
				pm.Invoke(new UIMethod1(pm.setITEVersion), new object[] { connectionData[17] });
				//RFAZZI_FI04/02/08
        //agrassi_FI19-08-2010_1
				//pm.Invoke(new UIMethod1(pm.setSyncroSoftwareStandard), new object[] { connectionData[18] });
				//RFAZZI_FI01/08/2009_1. Aggiunta possibilità di invertire la pagina grafica in Verticale
				Settings.getSettings().forms.MachineDirectionVisualizationInverted = Convert.ToBoolean(connectionData[19]);
			}
      //agrassi_FI20-01-09
      else if (text.StartsWith("ConnectionRefused("))
      {
        Logger.LogError(this, "ConnectionRequest - The connection to the server has been refused because the licence is missing");
        PageManager.getPageManager().pagesHistorian.Reset();
        this.connectionRefused = true;
        Application.ExitThread();
        return;
      }
		}

		/// <summary>
		/// Asyncronously (because running on an independent thread) receive messages (TCPMessage)
		/// from TCP connection, then "send" them to the specified (into the header) addressee that could be:
		/// PageManager.Reflect(), PageManager.Switch(), DBManager.Collect() or CommunicationMasnager.Collect().
		/// </summary>
		private void ReceiveTcp()
		{
      //agrassi_FI19-02-09_3
			//while (true)		//this thread is stopped using the CommunicationManager.StopReceiveTcp() method.
      while ((!this.connectionRefused) && (!pm.CloseAll))
			{
				ArrayList receivedMessages = tcpSocket.Receive();
				foreach (TCPMessage message in receivedMessages)
				{
					if (message.error.StartsWith(TCPSocket.RETURN_ERROR))
					{ //there was an error receiving the message, message discarded
						int error = 0;
						try
						{
							error = Convert.ToInt32(message.error.Substring(TCPSocket.RETURN_ERROR.Length));
						}
						catch (Exception e)
						{
							Logger.LogError(this, "ReceiveTCP - Invalid message, error: \"" + message.error + "\", message discarded.", e);
						}
						//if error -3 || -2 the message is skipped else the error is managed by the TCPHandler
						if (error != -3 && error != -2 && error != -4 && error != -5)
							TCPErrorHandler(message.error);
					}
					else
					{	//no error in the message, enqueue the message into the proper queue
						switch (message.header[0])//switch the correct addressee
						{
							case 'c':
							case 'C': //the addressee is the CommunicationManager
								Collect(message.text);
								break;
							case 'p':
							case 'P': //the addressee is the PageManager
								PMcommandsQueue.Enqueue(message);

                //if ((PMcommandsQueue.Count >= 50 && (PMcommandsQueue.Count % 100) == 0) || PMcommandsQueue.Count == 50)
                //  Logger.LogError(this, "ReceiveTcp - Length of PMcommandsQueue is " + PMcommandsQueue.Count.ToString());
                
                //ABELLO_FI30/11/09 - eseguiamo il reset dei comandi da eseguire se PMcommandQueue contiene più di 1000 elementi
                if ((PMcommandsQueue.Count >= 1000) && ((DateTime.Now - this.startTime).Minutes > 15))
                {
                  Logger.LogError(this, "Length of PMcommandsQueue is Too much for prevent deadlock ... Clear Queue");
                  PMcommandsQueue.Clear();
                }
								break;
							case 'd':
							case 'D': //the addressee is the DBManager
                //int index = message.text.IndexOf(Command.PARAMETER_SEPARATOR);
                //if (index <= 0 || index >= message.text.Length)
                //  index = message.text.Length;
								Logger.LogError(this, "Msg for DBManager: " + message.text);
								DBMcommandsQueue.Enqueue(message);
								if ((DBMcommandsQueue.Count >= 50 && (DBMcommandsQueue.Count % 100) == 0) || DBMcommandsQueue.Count == 50)
									Logger.LogError(this, "ReceiveTcp - Length of DBcommandsQueue is " + DBMcommandsQueue.Count.ToString());
								break;
							case 'r':
							case 'R': //the addressee is the AlarmManager
								RTCMcommandsQueue.Enqueue(message);
								if ((RTCMcommandsQueue.Count >= 50 && (RTCMcommandsQueue.Count % 100) == 0) || RTCMcommandsQueue.Count == 50)
									Logger.LogError(this, "ReceiveTcp - Length of RTCcommandsQueue is " + RTCMcommandsQueue.Count.ToString());
								break;
              //agrassi_FI03-11-2010_3
              case 'V':
              case 'v'://general commands
                Logger.LogError(this, "Update version request for Starter: " + message.text);
                threadUDPReceive = new Thread(new ThreadStart(UpdateVersionRequest));
						    threadUDPReceive.Name = "UpdateVersionRequest";
                try
                {
                  threadUDPReceive.Start();
                }
                catch (Exception ex)
                {
                  Logger.LogError(this, "Update version: error " + ex.Message);
                }
                break;
						}
					}
				}
			}
		}

    //agrassi_FI03-11-2010_3
    private void UpdateVersionRequest()
    {
      System.Threading.Thread.Sleep(20000);
      SendUdp(new UDPMessage(STARTER_TASK_SETTINGS, "UpdateVersionRequest()"));
      ReceiveUdp();
    }

    //agrassi_FI03-11-2010_3
    private void UpdateVersion(string text)
    {
      if (!text.EndsWith(")"))
      {
        Logger.LogError(this, "UpdateVersion - wrong message received: " + text);
        return;
      }
      text = text.Substring(0, text.Length - 1); //remove final ")"
      if (!text.StartsWith("UpdateVersion("))
      {
        Logger.LogError(this, "UpdateVersion - wrong command received: " + text);
        return;
      }
      string dataString = text.Substring(text.IndexOf('(') + 1);
      string[] arrayData = dataString.Split(',');
      //RFAZZI_FI04/02/08, RFAZZI_FI01/08/2009_1
      if (arrayData.Length != 3)
      {
        Logger.LogError(this, "UpdateVersion - wrong parameter number in the message received: " + text);
        return;
      }
      pm.Invoke(new UIMethod1(pm.setSyncroVersion), new object[] { arrayData[0] });
      pm.Invoke(new UIMethod1(pm.setSyncroDataVersion), new object[] { arrayData[1] });
      pm.Invoke(new UIMethod1(pm.setITEVersion), new object[] { arrayData[2] });
      Logger.LogError(this, "UpdateVersion - Syncro " + arrayData[0] + ", Data " + arrayData[1] + ", " + arrayData[2]);
    }

		/// <summary>
		/// Manage the TCP communication error. Restart the TCP socket when needed and eventualy ask the server to restart task
		/// </summary>
		/// <param name="errorMsg">string containing the error returned by a TCPSocket method</param>
    private void TCPErrorHandler(string errorMsg)
    {
      int error = 0;
      try
      {
        error = Convert.ToInt32(errorMsg.Substring(TCPSocket.RETURN_ERROR.Length));
      }
      catch (Exception e)
      {
        Logger.LogError(this, "TCPErrorHandler - invalid error \"" + errorMsg + "\"", e);
        return;
      }
      lastCommError = DateTime.Now;
      try
      {
        pm.Invoke(new UIMethod2(pm.CommFailure), new Object[] { "Communication error \"" + error + "\".", true });
      }
      catch (NullReferenceException e)
      {
        Logger.LogError(this, "TCPErrorHandler - a NullReferenceException occured during invocation of CommFailure method on the graphical thread.", e);
      }
      catch (InvalidOperationException e)
      {
        Logger.LogError(this, "TCPErrorHandler - an InvalidOperationException occured during invocation of CommFailure method on the graphical thread.", e);
      }
      catch (Exception e)
      {
        Logger.LogError(this, "TCPErrorHandler - an Exception occured during invocation of CommFailure method on the graphical thread.", e);
      }
      Logger.LogError(this, "TCPErrorHandler - handling error " + error.ToString() + ".");
      bool connected;
      int timeToWait;
      //ABELLO_FI17/11/2010_3 Sul Client la classe DBManager va presa quando serve e non prima
      //DBManager dbm = DBManager.getDBManager(false);//gets the dbmanager only if it has been already instantiated
      switch (error)
      {
        case 10054:	//server task is down or unplugged network cable on the client side.
          tcpSocket.close();
          connected = false;
          timeToWait = PING_MILLISECONDS_MIN_DELAY;
          if (!IsConnected(remoteIP, 4, timeToWait))
          {//unplugged the cable on the client side:
            //wait connection wire restoring then reconnect TCP
            //agrassi_FI19-02-09_3
            //while (!connected)
            while ((!connected) && (!this.connectionRefused) && (!pm.CloseAll))
            {
              connected = IsConnected(remoteIP, 2, timeToWait);
              timeToWait = (timeToWait + PING_MILLISECONDS_INCREMENT > PING_MILLISECONDS_MAX_DELAY)
                ? PING_MILLISECONDS_MAX_DELAY : (timeToWait + PING_MILLISECONDS_INCREMENT);
            }
          }
          else
          {//server task is down
            //clear dataset possibly not consistent, then restoring UDP and TCP
            if (DBManager.getDBManager(false) != null)
              pm.Invoke(new UIMethod0(DBManager.getDBManager(false).ClearDatabase));
            StopReceiveUdp();
            if (udpSocket != null)
              udpSocket.close();
            UDPConnection();
          }
          TCPConnection();
          if (DBManager.getDBManager(false) != null)
            pm.Invoke(new UIMethod0(DBManager.getDBManager(false).SendVersions));
          break;
        case -1:	//user defined error: a message was not entirely received because probably the cannection was interrupted....
        case 10060:	//unplugged network cable server side
          //wait connection wire restoring then reconnect TCP
          tcpSocket.close();
          connected = false;
          timeToWait = PING_MILLISECONDS_MIN_DELAY;
          //agrassi_FI19-02-09_3
          //while (!connected)
          while ((!connected) && (!this.connectionRefused) && (!pm.CloseAll))
          {
            connected = IsConnected(remoteIP, 2, timeToWait);
            timeToWait = (timeToWait + PING_MILLISECONDS_INCREMENT > PING_MILLISECONDS_MAX_DELAY)
              ? PING_MILLISECONDS_MAX_DELAY : (timeToWait + PING_MILLISECONDS_INCREMENT);
          }
          TCPConnection();
          if (DBManager.getDBManager(false) != null)
            pm.Invoke(new UIMethod0(DBManager.getDBManager(false).SendVersions));
          break;
        case -2:	//user defined error: error not due to the connection or to the server so nothing can be done here....
          break;//it has been managed by the TCPReceive
        case -3:	//user defined error: message bad format, nothing can be done: skip the message
          break;//it has been managed by the TCPReceive
        case 10061:	//windows error: probably the server task is down....
        case 64:	//windows error: net name error....
        case 0:		//user defined error: not well specified error....
        default:
          //clear dataset possibly not consistent, then wait connection wire restoring then reconnect UDP and TCP
          if (DBManager.getDBManager(false) != null)
            pm.Invoke(new UIMethod0(DBManager.getDBManager(false).ClearDatabase));
          tcpSocket.close();
          connected = false;
          timeToWait = PING_MILLISECONDS_MIN_DELAY;
          //agrassi_FI19-02-09_3
          //while (!connected)
          while ((!connected) && (!this.connectionRefused) && (!pm.CloseAll))
          {
            connected = IsConnected(remoteIP, 2, timeToWait);
            timeToWait = (timeToWait + PING_MILLISECONDS_INCREMENT > PING_MILLISECONDS_MAX_DELAY)
              ? PING_MILLISECONDS_MAX_DELAY : (timeToWait + PING_MILLISECONDS_INCREMENT);
          }
          StopReceiveUdp();
          if (udpSocket != null)
            udpSocket.close();
          UDPConnection();
          TCPConnection();
          if (DBManager.getDBManager(false) != null)
            pm.Invoke(new UIMethod0(DBManager.getDBManager(false).SendVersions));
          break;
      }
      pm.Invoke(new UIMethod0(pm.CommRestored));
      Logger.LogError(this, "TCPErrorHandler - Communication restored");
    }

		/// <summary>
		/// Stop the thread used for asyncronous TCP connection receiving and closes the TCP socket
		/// </summary>
		public void StopReceiveTcp()
		{
			if (threadTCPReceive != null)
			{
				try
				{
					threadTCPReceive.Abort();
				}
				catch (System.Security.SecurityException ex)
				{
					Logger.LogError(this, "StopReceiveTcp", ex);
				}
				catch (ThreadStateException ex)
				{
					Logger.LogError(this, "StopReceiveTcp - the thread \"" + threadTCPReceive.Name + "\" was suspended", ex);
				}
			}
			if (tcpSocket != null)
				tcpSocket.close();
		}

		/// <summary>
		/// Stop the thread used for UDP connection receiving  and closes the UDP socket
		/// </summary>
		public void StopReceiveUdp()
		{
			if (threadUDPReceive != null)
			{
				try
				{
					threadUDPReceive.Abort();
				}
				catch (System.Security.SecurityException ex)
				{
					Logger.LogError(this, "StopReceiveUdp", ex);
				}
				catch (ThreadStateException ex)
				{
					Logger.LogError(this, "StopReceiveUdp - the thread \"" + threadUDPReceive.Name + "\" was suspended", ex);
				}
			}
			if (udpSocket != null)
				udpSocket.close();
		}

		/// <summary>
		/// CommunicationManager collector for commands to be interpreted by a "switch".
		/// </summary>
		/// <param name="message">command to be executed by the CommunicationManager</param>
		private void Collect(String command)
		{
			if (command.StartsWith("keepAlive"))
				return;

			else if (command.StartsWith("START_TRANSMIT"))
			{
				command = command.Substring("START_TRANSMIT".Length);
				int index = command.IndexOf(")");
				if (!command.StartsWith("(") || index == -1)
				{
					Logger.LogError(this, "Collect - command \"" + command + "\" is wrong.");
					return;
				}
				command = command.Substring(1, index - 1);//remove '(' and ')' 
				int separatorIndex = command.IndexOf(",");
				if (separatorIndex == -1)
				{
					Logger.LogError(this, "Collect - command \"" + command + "\" is wrong.");
					return;
				}

				string dbName = command.Substring(0, separatorIndex);
				int dbSize = Convert.ToInt32(command.Substring(separatorIndex + 1));
				string fileName = Settings.getSettings().paths.ClientData + dbName;
				FileStream filestream = null;
				try
				{
					if (File.Exists(fileName))
						File.Delete(fileName);
					filestream = new FileStream(
						fileName, FileMode.Create, FileAccess.Write);
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "Collect - ", ex);
				}
				if (filestream != null)
				{
					try
					{
						while (dbSize > 0)
						{
							byte[] buffer = null;
							if (dbSize > TCPSocket.TCP_MSG_BYTES_NUMBER)
								//try to get the maximum number of bytes
								buffer = tcpSocket.ReceiveBytes(TCPSocket.TCP_MSG_BYTES_NUMBER);
							else
								//try to get all the remaining bytes
								buffer = tcpSocket.ReceiveBytes(dbSize);
							if (buffer != null && !Encoding.Unicode.GetString(buffer).StartsWith(TCPSocket.RETURN_ERROR))
							{
								filestream.Write(buffer, 0, buffer.Length);
								dbSize -= buffer.Length;
							}
							else
							{
								Logger.LogError(this, "Collect - error transmitting the databese \"" + dbName + "\": " + Encoding.Unicode.GetString(buffer) + ".");
								return;
							}
						}
						filestream.Flush();
						filestream.Close();
						NameDBEndTransmitWaited = dbName;
					}
					catch (Exception ex)
					{
						Logger.LogError(this, "Collect - ", ex);
					}
				}
			}

			else if (command.StartsWith("END_TRANSMIT"))
			{
				string cmd = command;
				cmd = cmd.Substring("END_TRANSMIT".Length);
				int index = cmd.IndexOf(")");
				if (!cmd.StartsWith("(") || index == -1)
				{
					Logger.LogError(this, "Collect - command \"" + command + "\" is wrong.");
					return;
				}
				cmd = cmd.Substring(1, index - 1);
				if (cmd.Equals(NameDBEndTransmitWaited))
					NameDBEndTransmitWaited = "";
				else
					Logger.LogError(this, "Collect - END_TRANSMIT, an End Transmit command for \"" + NameDBEndTransmitWaited + "\" was attended but the fallowing EndTransmit command has been received \"" + command + "\"");
			}
			else if (command.StartsWith("ShutDown"))
			{
				Logger.LogError(this, "Collect - Received shut-down command from server");
				ShutDown(false, false);
			}
			else if (command.StartsWith("CloseAndShutDown"))
			{
				Logger.LogError(this, "Collect - Received ShutDownAndClose command from server");
				ShutDown(true, false);
			}
			else
				Logger.LogError(this, "Collect - Invalid command \"" + command + "\".");
		}

		/// <summary>
		/// Sends specified message using UDP connection to the Server. 
		/// </summary>
		/// <param name="udpMessage">The message to be sent.</param>
		public bool SendUdp(UDPMessage udpMessage)
		{
			return udpSocket.Send(udpMessage).StartsWith(UDPSocket.RETURN_OK);
		}

		/// <summary>
		/// Sends specified data with the specified header using TCP connection to the Server. 
		/// </summary>
		/// <param name="addressee">a string specifing the addressee of the message.</param>
		/// <param name="data">the string specifing the text to use for the message.</param>
		public bool SendTcp(string addressee, string data)
		{
			string ret = tcpSocket.Send(new TCPMessage(addressee, data));
			int cnt = 0;
			while ((ret.StartsWith(TCPSocket.RETURN_ERROR)) && (++cnt < TCP_MAX_TRIALS))
			{
				//manage the error and try again
				TCPErrorHandler(ret);
				ret = tcpSocket.Send(new TCPMessage(addressee, data));
			}
			return (cnt < TCP_MAX_TRIALS);
		}

		/// <summary>
		/// Return a value indicating whether the socket TCP is connected.
		/// </summary>
		/// <returns></returns>
		public bool CanSendTCP()
		{
			return tcpSocket.Connected;
		}

		/// <summary>
		/// Shut the application down.
		/// Stop the receiving threads of the CommunicationManager (UDP, TCP)
		/// and call the Application.Exit().
		/// The server is sopposed to set the correct version of every local database of the client 
		/// before calling this method.
		/// </summary>
		public void ShutDown(bool isRequestClosePC, bool isRequestRestartApplication)
		{
			Settings s = Settings.getSettings();
			Logger.LogError(this, "ShutDown - Client start shut-down.");
			//ask the starter to shout down the SyncroS
			//RFAZZI_FI02/10/2009_1. Aggiunta possibilità di avviare più client sul solito PC.
			SendUdp(new UDPMessage(STARTER_CONN_REQ, "CloseConnection(" + s.network.ClientID.ToString() + ")"));
			//stop the receiving thread in UDP protocol
			StopReceiveUdp();
			//close the TCP socket so no more commands will be received (cannot use StopReceiveTcp() here because it would
			//stop the current thread and instructions below wouldn't never be executed.
			if (tcpSocket != null)
				tcpSocket.close();
			//wait for the DBMcommands queue to be emptied out (timeout is 20 secs)
			int waitIntervalMillisecs = 100;
			int i = 0;
			while ((DBMcommandsQueue.Count > 0) && (i < 200))
			{
				Thread.Sleep(waitIntervalMillisecs);
				i++;
			}
			//shut-down logs
			if (i > 0)
			{
				Logger.LogError(this, "ShutDown - waited milliseconds " + (i * waitIntervalMillisecs).ToString() + " for DBCommands queue to be empty.");
			}
			if (i >= 200)
			{
        //agrassi_FI19-12-08
        //Logger.LogWindowsEvent(this, "Exit", "Application closed but all the DB commands were not completely executed.", EventLogEntryType.Information);
				Logger.LogError(this, "ShutDown - Client shutted-down but all the DB commands were not completely executed.");
			}
			else
			{
        //agrassi_FI19-12-08
        //Logger.LogWindowsEvent(this, "Exit", "Application closed correctly", EventLogEntryType.Information);
				Logger.LogError(this, "ShutDown - Client shutted-down correctly.");
			}

			if (isRequestClosePC)
			{
				//CM69 si piantava
				if (!Settings.getSettings().configuration.getOnServer())
				{
					Logger.LogError(this, "ShutDown - Client is requested to shutdown the PC");
          //agrassi_FI25-08-08_5
          //bool onFosberNetwork = false;
					bool clientOnDxConsole = false;
					IPAddress[] IPAddresses = Dns.GetHostAddresses(Dns.GetHostName());
          //agrassi_FI25-08-08_5
					//string IPAddressFosberNetwork = "10.151.151";
					string IPAddressClientOnDxConsole = "10.151.151.140";
					Logger.LogError(this, "ShutDown - Client is scan the IP Address");
					for (int j = 0; j < IPAddresses.Length; j++)
					{
						Logger.LogError(this, "ShutDown - Client is scan the IP Addres " + IPAddresses[j].ToString());
            //agrassi_FI25-08-08_5
            //if (IPAddresses[j].ToString().StartsWith(IPAddressFosberNetwork))
            //  onFosberNetwork = true;

						if (IPAddresses[j].ToString() == IPAddressClientOnDxConsole)
							clientOnDxConsole = true;
					}
          //agrassi_FI25-08-08_5
          if (Settings.getSettings().configuration.getOnFosberNetwork())
          //if (onFosberNetwork)
          {
					  Logger.LogError(this, "ShutDown - Client is in Fosber Network");
					  if (clientOnDxConsole)
					  {
						  Logger.LogError(this, "ShutDown - Client is on Dx Console: shutdown.");
						  Utility.Shoutdown.ShoutdownWindows(false);
					  }
					}
					else
						Logger.LogError(this, "ShutDown - Client is NOT on Fosber network: no shutdown.");
				}
			}

      //PMASSARO_05/03/09 - Close Acrobat Reader Application
      string AcrobatExe = "AcroRd32";
      Process[] myProcesses = Process.GetProcesses(Environment.MachineName);
      foreach (Process myProcess in myProcesses)
      {
        if (myProcess.ProcessName.ToUpper().CompareTo(AcrobatExe.ToUpper()) == 0)
        {
          Logger.LogError(this, "ShutDown - Found " + AcrobatExe + " Process. I Try To Kill It");
          myProcess.Kill();
          break;
        }
      }

			//close the client application
			if (isRequestRestartApplication && !isRequestClosePC)
				Application.Restart();
			else
				Application.Exit();
			//stop the current thread
			StopReceiveTcp();
		}
	}
}


