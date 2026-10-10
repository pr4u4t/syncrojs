using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

using SyncroC.Utility;

namespace SyncroC.Network
{
	/// <summary>
	/// Implements an UDP socket connection with the server.
	/// </summary>
	public class UDPSocket
	{
		//constants for message composing
		/// <summary>
		/// Beginning of every message transitted using UDP socket connection.
		/// </summary>
		private string BEGIN_UDP_MSG = "<MSG>";
		/// <summary>
		/// End of every message transitted using UDP socket connection.
		/// </summary>
		private string END_UDP_MSG = "</MSG>";
		/// <summary>
		/// Beginning of header of every message transitted using UDP socket connection.
		/// </summary>
		private string BEGIN_UDP_HEADER = "<HDR>";
		/// <summary>
		/// End of header of every message transitted using UDP socket connection.
		/// </summary>
		private string END_UDP_HEADER = "</HDR>";
		/// <summary>
		/// Beginning of text of every message transitted using UDP socket connection.
		/// </summary>
		private string BEGIN_UDP_TEXT = "<TXT>";
		/// <summary>
		/// End of text of every message transitted using UDP socket connection.
		/// </summary>
		private string END_UDP_TEXT = "</TXT>";

		/// <summary>
		/// Beginning of every error message returned by UDPSocket.initialize, UDPSocket.send, UDPSocket.receive methods. 
		/// </summary>
		public static string RETURN_ERROR = "ERROR_";
		/// <summary>
		/// Message returned by UDPSocket.initialize, UDPSocket.send methods when operations succeded.
		/// </summary>
		public static string RETURN_OK = "OK";
		/// <summary>
		/// The socket used for the connection.
		/// </summary>
		private Socket socket;
		/// <summary>
		/// The remote ip to connect the socket.
		/// </summary>
		private string remoteIP = "";
		/// <summary>
		/// The port to use to send data.
		/// </summary>
		private int sendPort;
		/// <summary>
		/// The port to use to receive data.
		/// </summary>
		private int receivePort;
		/// <summary>
		/// The local IPEndPoint.
		/// </summary>
		private IPEndPoint localIpEndPoint = null;
		/// <summary>
		/// The remote IPEndPoint.
		/// </summary>
		private IPEndPoint remoteIpEndPoint= null; 

		/// <summary>
		/// Builder.
		/// </summary>
		/// <param name="ipServerAddress">ip address of the server to connect with.</param>
		/// <param name="sendPort">port number to use to send data to the server.</param>
		/// <param name="receivePort">port number to receive data from the server.</param>
		public UDPSocket(string ipServerAddress, int sendPort, int receivePort)
		{
			remoteIP = ipServerAddress;
			this.sendPort = sendPort;
			this.receivePort = receivePort;
		}

		/// <summary>
		/// Initialize the UDPSocket.
		/// </summary>
		/// <returns>UDPSocket.RETURN_OK string if operation succeded, UDPSocket.RETURN_ERROR 
		/// fallowed by the socket error number in case of error.</returns>
		public string initialize()
		{
			IPAddress localIpAddress = null;
			try 
			{
				socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				//remoteIpEndPoint to Send
				IPAddress remoteIpAddress = System.Net.IPAddress.Parse(remoteIP);
				remoteIpEndPoint = new IPEndPoint(remoteIpAddress, sendPort);
				//localIpEndPoint to Receive
				localIpAddress = System.Net.IPAddress.Parse("0.0.0.0");
				localIpEndPoint = new IPEndPoint(localIpAddress, receivePort);
				socket.Bind(localIpEndPoint);
			}
			catch (FormatException ex)
			{//may be thrown by the IPAddress.Parse
				Logger.LogError(this, "initialize - local Ip " + localIpAddress + "is invalid. The receiveSocket has not been created", ex);
				this.close();
				return RETURN_ERROR + "0";
			}
			catch (ArgumentOutOfRangeException ex)
			{//may be thrown while IPEndPoint creation
				Logger.LogError(this, "initialize - udp port is out of the range. The receiveSocket has not been created", ex);
				this.close();
				return RETURN_ERROR + "0";
			}
			catch (SocketException socketException)
			{//may be thrown while socket creation
				Logger.LogError(this, "initialize - error creating socket", socketException);
				this.close();
				return RETURN_ERROR + socketException.ErrorCode.ToString();
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "initialize - error creating socket", ex);
				this.close();
				return RETURN_ERROR + "0";
			}
			return RETURN_OK;
		}

		/// <summary>
		/// Close the connection.
		/// </summary>
		public void close()
		{
      //ABELLO_FI17/11/2010_2 Sul client ogni tanto all'accensione il socket UDP dava eccezione non gestita
      if (socket != null)
      {
        try
        {
          socket.Shutdown(SocketShutdown.Both);
          socket.Close();
          socket = null;
        }
        catch (SocketException ex)
        {
          Logger.LogError(this, "close - socket exception on shutdown", ex);
        }
        catch (ObjectDisposedException ex)
        {
          Logger.LogError(this, "close - the UDP socket has been already disposed.", ex);
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "close - Error on UDP socket", ex);
        }
        //socket.Close();
        //socket = null;
      }
		}

		/// <summary>
		/// Sends an UDPMessage to the Server.
		/// </summary>
		/// <param name="message">the UDPMessage to send</param>
		/// <returns>UDPSocket.RETURN_OK string if operation succeded, UDPSocket.RETURN_ERROR 
		/// fallowed by the socket error number in case of error.</returns>
		public string Send(UDPMessage message) 
		{
			if (message != null)
			{
				int bytesSent = 0;
				byte[] byteData = Encoding.Unicode.GetBytes(createString(message.header, message.text));
				try 
				{
					bytesSent = socket.SendTo(byteData, remoteIpEndPoint);
				} 
				catch(SocketException socketException)
				{
					Logger.LogError(this, "Send - Error accessing socket", socketException);
					return UDPSocket.RETURN_ERROR + socketException.ErrorCode.ToString();
				}
				catch(Exception e)
				{
					Logger.LogError(this, e);
					return UDPSocket.RETURN_ERROR + "0";
				}
				return (byteData.Length == bytesSent) ? UDPSocket.RETURN_OK : UDPSocket.RETURN_ERROR + "0";
			}
			return UDPSocket.RETURN_OK;
		}

		/// <summary>
		/// Receive an UDPMessage from the server.
		/// </summary>
		/// <returns>The received UDPMessage or an UDPMessage containing the occurred error.</returns>
		public UDPMessage Receive() 
		{
			string receivedData = "";
			UDPMessage message = null;
			try 
			{
				byte[] bytes = new byte[1024];
				socket.Receive(bytes);
				receivedData = Encoding.Unicode.GetString(bytes);
			}
			catch(SocketException socketException)
			{//may be thrown by the Socket.receive method
				Logger.LogError(this, "Receive - Error accessing the socket", socketException);
				return new UDPMessage(UDPSocket.RETURN_ERROR + socketException.ErrorCode.ToString());
			}
			catch(ArgumentNullException e)
			{//may be thrown by the Encoding.ASCII.GetString method
				Logger.LogError(this, "Receive - Error encoding receiving data", e);
				return new UDPMessage(UDPSocket.RETURN_ERROR + "0");
			}
			catch(Exception e)
			{
				Logger.LogError(this, e);
				return new UDPMessage(UDPSocket.RETURN_ERROR + "0");
			}
			//parsing the data
			string msg = receivedData.Trim();

			string regularExpression = 
				BEGIN_UDP_MSG + 
				BEGIN_UDP_HEADER +	"(.*?)" + END_UDP_HEADER + 
				BEGIN_UDP_TEXT	+	"(.*?)" + END_UDP_TEXT + 
				END_UDP_MSG;
			try
			{
				if (! Regex.IsMatch(msg, regularExpression)) 
				{	//message is not well formed
					Logger.LogError(this, "Receive - wrong message: \"" + receivedData + "\".");
					return new UDPMessage(RETURN_ERROR + "-3");
				} 
				else 
				{	//well formed message: split
					string[] tokens = Regex.Split(msg, regularExpression);
					message = new UDPMessage(tokens[1], tokens[2]);
				}
			}
			catch (Exception ex)
			{	// Regex.IsMatch() threw an exception:
				// Most likely cause is a syntax error in the regular expression
				Logger.LogError(this, "Receive - probable syntax error in the regular expression", ex);
				return new UDPMessage(RETURN_ERROR + "-3");
			}
			return message;
		}	
	
		/// <summary>
		/// Create a string to be sent throw the udp socket according to the protocol.
		/// </summary>
		/// <param name="header">a string representing the header text
		/// <param name="text">a string representing the text of the message.</param>
		/// <returns></returns>
		private string createString(String header, String text) 
		{
			return BEGIN_UDP_MSG + 
				BEGIN_UDP_HEADER + header + END_UDP_HEADER +
				BEGIN_UDP_TEXT + text + END_UDP_TEXT + 
				END_UDP_MSG;
		}
	}
}
