using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

using SyncroC.Utility;

namespace SyncroC.Network
{
	/// <summary>
	/// Implements a TCP socket connection with the server.
	/// </summary>
	public class TCPSocket
	{
		//constants
		/// <summary>
		/// Beginning of every packet transitted using TCP socket connection.
		/// </summary>
		private string BEGIN_TCP_PACKET="<PKT>";
		/// <summary>
		/// End of every packet transitted using TCP socket connection.
		/// </summary>
		private string END_TCP_PACKET="</PKT>";
		/// <summary>
		/// Beginning of every message transitted using TCP socket connection.
		/// </summary>
		private string BEGIN_TCP_MSG = "<MSG>";
		/// <summary>
		/// End of every message transitted using TCP socket connection.
		/// </summary>
		private string END_TCP_MSG = "</MSG>";
		/// <summary>
		/// Beginning of header of every message transitted using TCP socket connection.
		/// </summary>
		private string BEGIN_TCP_HEADER = "<HDR>";
		/// <summary>
		/// End of header of every message transitted using TCP socket connection.
		/// </summary>
		private string END_TCP_HEADER = "</HDR>";
		/// <summary>
		/// Beginning of text of every message transitted using TCP socket connection.
		/// </summary>
		private string BEGIN_TCP_TEXT = "<TXT>";
		/// <summary>
		/// End of text of every message transitted using TCP socket connection.
		/// </summary>
		private string END_TCP_TEXT = "</TXT>";
		/// <summary>
		/// Maximum number of bytes composing every message between client and server.
		/// </summary>
		public readonly static int TCP_PKT_ADD_BYTES = 32;
		/// <summary>
		/// Maximum number of bytes composing every message between client and server.
		/// </summary>
		public readonly static int TCP_MSG_BYTES_NUMBER = 1000;
		/// <summary>
		/// Maximum number of parts of a message client-server
		/// </summary>
		public readonly static int TCP_MSG_PART_NUMBER = 99;
		/// <summary>
		/// Socket time out. must be longer than the frequency the server send the "keep alive" command.
		/// </summary>
		private const int TCP_TIME_OUT = 11000;
		/// <summary>
		/// Beginning of every error message returned by TCPSocket.initialize, TCPSocket.send, TCPSocket.receive, TCPSocket.receiveBytes methods. 
		/// </summary>
		public static string RETURN_ERROR = "ERROR_";
		/// <summary>
		/// Message returned by TCPSocket.initialize, send methods when operations succeded.
		/// </summary>
		public static string RETURN_OK = "OK";
		/// <summary>
		/// The socket used for the connection.
		/// </summary>
		private Socket socket;
		/// <summary>
		/// The remote ip to connect the socket.
		/// </summary>
		private string remoteIP;
		/// <summary>
		/// The port used to communicate (both send and receive).
		/// </summary>
		private int port;

		/// <summary>
		/// Data red from the socket, in some cases might not match with protocol packets
		/// </summary>
		private string receivedData = string.Empty;

		/// <summary>
		/// Gets a value indicating whether the TCPSocket is connected to a remote host
		/// </summary>
		public bool Connected
		{
			get {	return ((socket != null) && (socket.Connected));	}
		}

		/// <summary>
		/// Builder.
		/// note: to use the TCPSocket after the builder the initialize method must be called.
		/// </summary>
		/// <param name="remoteIP">ip address of the server.</param>
		/// <param name="port">port number to use to communicate.</param>
		public TCPSocket(string remoteIP, int port)
		{
			this.remoteIP = remoteIP;
			this.port = port;
		}

		/// <summary>
		/// Initialize the TCP socket.
		/// </summary>
		/// <returns>TCPSocket.RETURN_OK string if operation succeded, TCPSocket.RETURN_ERROR fallowed by the socket error number in case of error.</returns>
		public string initialize()
		{
			try
			{
				IPAddress remoteIpAddress = System.Net.IPAddress.Parse(remoteIP);
				IPEndPoint remoteEndPoint = new IPEndPoint(remoteIpAddress, port);
				socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				socket.SendBufferSize = TCP_MSG_BYTES_NUMBER + TCP_PKT_ADD_BYTES;
				socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, TCP_TIME_OUT);
				socket.Connect(remoteEndPoint);
			}
			catch (FormatException ex)
			{//may be thrown by the IPAddress.Parse
				Logger.LogError(this, "initialize - local Ip " + remoteIP + "is invalid. The receiveSocket has not been created", ex);
				return RETURN_ERROR + "0";
			}
			catch (ArgumentOutOfRangeException ex)
			{//may be thrown while IPEndPoint creation
				Logger.LogError(this, "initialize - udp port is out of the range. The receiveSocket has not been created", ex);
				return RETURN_ERROR + "0";
			}
			catch (SocketException socketException)
			{//may be thrown while socket creation or connection
				//in this case (initialize) instead of the exact windows socket error it is returned
				//the error 0, so the CommunicationManager.TCPErrorHandler routine surely will
				//manage this error from the udp connection avoiding dangerouse recursive call on the routine.
				Logger.LogError(this, "initialize - (try unplug / plug the cable)", socketException);
				return RETURN_ERROR + "0";
			}
			catch (Exception ex)
			{
				Logger.LogError(this, ex);
				return RETURN_ERROR + "0";
			}
			return RETURN_OK;
		}
		
		/// <summary>
		/// Close the connection.
		/// </summary>
		public void close()
		{
			if (socket != null)
			{
				if (socket.Connected)
				{
					try
					{
						socket.Shutdown(SocketShutdown.Both);
					}
					catch (SocketException ex)
					{
						Logger.LogError(this, "close - socket exception on shutdown", ex);
					}
					catch (ObjectDisposedException ex)
					{
						Logger.LogError(this, "close - the TCP socket has been already disposed", ex);
					}
				}
				socket.Close();
				socket = null;
			}
		}

		private byte[] AddPacketHeader(byte[] data, int significantBytes, int part, int parts)
		{
			byte[] initHeader=Encoding.Unicode.GetBytes(BEGIN_TCP_PACKET+part.ToString("00")+"-"+parts.ToString("00"));
			byte[] finalHeader=Encoding.Unicode.GetBytes(END_TCP_PACKET);
			byte[] dataWithHeader=new byte[initHeader.Length+data.Length+finalHeader.Length];
			for (int i=0; i<initHeader.Length; i++)
				dataWithHeader[i]=initHeader[i];
			for (int i=0; i<significantBytes; i++)
				dataWithHeader[initHeader.Length+i]=data[i];
			for (int i=0; i<finalHeader.Length; i++)
				dataWithHeader[initHeader.Length+significantBytes+i]=finalHeader[i];
			return dataWithHeader;
		}
			
		/// <summary>
		/// Send a message to the remoteIp address.
		/// </summary>
		/// <param name="message">the TCPMessage to send.</param>
		/// <returns>TCPSocket.RETURN_OK string if operation succeded, TCPSocket.RETURN_ERROR fallowed by 
		/// the socket error number in case of error, 0 in case of other error.</returns>
		public string Send(TCPMessage message) 
		{
			lock(this)
			{
				if (message != null)
				{
					byte[] msg = Encoding.Unicode.GetBytes(createString(message.header, message.text));
					if (msg.Length > TCP_MSG_BYTES_NUMBER*TCP_MSG_PART_NUMBER)
					{
						Logger.LogError(this, "Send - the message to be sent is " + msg.Length + " but the bytes to send are " + TCP_MSG_BYTES_NUMBER*TCP_MSG_PART_NUMBER + " as specified in the second parameter.");
						return RETURN_ERROR + "-2";
					}
					int txParts=msg.Length/TCP_MSG_BYTES_NUMBER+((msg.Length%TCP_MSG_BYTES_NUMBER>0)?1:0);
					int bytesSent = 0;
					for (int i=0; i<txParts; i++)
					{
						int bytesToCopy=TCP_MSG_BYTES_NUMBER;
						if ((i==txParts-1) && (msg.Length%TCP_MSG_BYTES_NUMBER>0))
							bytesToCopy=msg.Length%TCP_MSG_BYTES_NUMBER;
						byte[] byteData = new byte[TCP_MSG_BYTES_NUMBER];
						Array.Copy(msg, i*TCP_MSG_BYTES_NUMBER, byteData, 0, bytesToCopy);
						byte[] byteDataToSend=this.AddPacketHeader(byteData, bytesToCopy, i+1, txParts);
						try
						{
							bytesSent+=socket.Send(byteDataToSend);	
							//System.Threading.Thread.Sleep(500);
						} 
						catch(SocketException socketException)
						{
							return RETURN_ERROR + socketException.ErrorCode.ToString();
						}
						catch(Exception e)
						{
							Logger.LogError(this, e);
							return RETURN_ERROR + "0";
						}
					}
					return (bytesSent == (TCP_MSG_BYTES_NUMBER+TCP_PKT_ADD_BYTES)*txParts) ? RETURN_OK : RETURN_ERROR + "0";
				}
				return RETURN_OK;
			}
		}

		/// <summary>
		/// Receive a message from the remoteIP.
		/// </summary>
		/// <returns>Array List of TCPMessages
		/// TCPMessages of the array list might contain errors: 
		/// the socket error number in case of socket error, -1 if the message was not completely received, 0 in case of other error.</returns>
		public ArrayList Receive()  
		{
			//receiving the data
			int numBytesReceived = 0;
			string[] receivedPacket=null;
			bool nextPacket;
			ArrayList TCPMessages = new ArrayList(1);
			do
			{
				nextPacket = false;
				try
				{
					int bytesToBeReceived = TCP_MSG_BYTES_NUMBER + TCP_PKT_ADD_BYTES;
					byte[] byteData = new byte[bytesToBeReceived];
					numBytesReceived = 0;
					int loop = 0;
					while (numBytesReceived < bytesToBeReceived)
					{
						if (loop >= 1)
						{
							Logger.LogError(this, "Receive - loop " + (loop - 1).ToString() + " received only " + numBytesReceived.ToString() + " bytes");
							if (loop >= 50)
							{
								Logger.LogError(this, "Receive - Cannot completely receive the message: " + Encoding.Unicode.GetString(byteData).ToString());
								TCPMessages.Add(new TCPMessage(RETURN_ERROR + "0"));
								return TCPMessages;
							}
							System.Threading.Thread.Sleep(100);
						}
						numBytesReceived += socket.Receive(byteData, numBytesReceived, bytesToBeReceived - numBytesReceived, SocketFlags.None);
						loop++;
					}

					receivedData += Encoding.Unicode.GetString(byteData);

					//int available = socket.Available;
					//if (available > 0)
					//{
					//  if (available > bytesToBeReceived - receivedData.Length)
					//    available = bytesToBeReceived - receivedData.Length;
					//  numBytesReceived += socket.Receive(byteData, numBytesReceived, available, SocketFlags.None);
					//  Console.WriteLine("available: " + available.ToString() + "; received: " + numBytesReceived.ToString());

					//  receivedData += Encoding.Unicode.GetString(byteData);
					//  if (receivedData.Length > 0)
					//    Console.WriteLine(receivedData);
					//}
					//else
					//{ 
					
					//}

					//numBytesReceived += socket.Receive(byteData, numBytesReceived, bytesToBeReceived - numBytesReceived, SocketFlags.None);

					//if (numBytesReceived < bytesToBeReceived)
					//{
					//  if (loop >= 1)
					//  {
					//    Logger.LogError(this, "Receive - loop " + (loop - 1).ToString() + " received only " + numBytesReceived.ToString() + " bytes");
					//  }
					//  numBytesReceived += socket.Receive(byteData, numBytesReceived, bytesToBeReceived - numBytesReceived, SocketFlags.None);
					//  loop++;
					//}

					//receivedData += Encoding.Unicode.GetString(byteData);

        }
				catch (SocketException socketException)
				{//may be thrown by the Socket.Receive method
					TCPMessages.Add(new TCPMessage(RETURN_ERROR + socketException.ErrorCode.ToString()));
					return TCPMessages;
				}
				catch(ArgumentNullException ex)
				{//may be thrown by the Encoding.ASCII.GetString method
					Logger.LogError(this, "Receive - Error encoding receiving data", ex);
					TCPMessages.Add(new TCPMessage(RETURN_ERROR + "0"));
					return TCPMessages;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, ex);
					TCPMessages.Add(new TCPMessage(RETURN_ERROR + "0"));
					return TCPMessages;
				}

				//joining the packets
				string regExpr=BEGIN_TCP_PACKET+"([0-9]{2})"+"-"+"([0-9]{2})"+"(.*?)"+END_TCP_PACKET;
				try
				{
					receivedData = receivedData.Replace("\r\n", " ");
					receivedData = receivedData.Replace("\0", " ");
					receivedData = receivedData.Trim();	
          if (!Regex.IsMatch(receivedData, regExpr))
					{
            if (receivedData.StartsWith(BEGIN_TCP_PACKET) && receivedData.EndsWith(END_TCP_PACKET))
            {
                Logger.LogError(this, "Wrong packet: " + receivedData);
                TCPMessages.Add(new TCPMessage(RETURN_ERROR + "-4"));
								return TCPMessages;
            }
            else
            {
              Logger.LogError(this, "Partial packet: " + receivedData);
              nextPacket = true;
                continue;
            }
					}
					else
					{
						do
						{
							string value = Regex.Match(receivedData, regExpr).Value;
							string[] tokens = Regex.Split(value, regExpr);
							if (receivedPacket == null)
								receivedPacket = new string[int.Parse(tokens[2])];
							receivedPacket[int.Parse(tokens[1]) - 1] = tokens[3];
							nextPacket = (int.Parse(tokens[1]) < int.Parse(tokens[2]));
							if (!nextPacket)
							{
								TCPMessages.Add(createTCPMessage(receivedPacket));
								receivedPacket = null;
							}
							receivedData = receivedData.Substring(receivedData.IndexOf(value) + value.Length);
						}
						while (Regex.IsMatch(receivedData, regExpr));
					}
				}
				catch (Exception ex)
				{	// Regex.IsMatch() threw an exception:
					// Most likely cause is a syntax error in the regular expression
					Logger.LogError(this, "Receive - probable syntax error in the regular expression", ex);
					TCPMessages.Add(new TCPMessage(RETURN_ERROR + "-4"));
					return TCPMessages;
				}
			}while(nextPacket);
			
			return TCPMessages;
		}

		private TCPMessage createTCPMessage(string[] receivedPacket)
		{
			TCPMessage message = null;
			string receivedCompleteData = string.Empty;
			for (int i = 0; i < receivedPacket.Length; i++)
			{
				if ((receivedPacket[i] == null) || (receivedPacket[i].Equals(string.Empty)))
				{
					Logger.LogError(this, "Receive - packet " + i + " is missing");
					return new TCPMessage(RETURN_ERROR + "-5");
				}
				receivedCompleteData += receivedPacket[i];
			}

			//parsing the data
			string msg = receivedCompleteData.Trim();

			string regularExpression =
				BEGIN_TCP_MSG +
					BEGIN_TCP_HEADER + "(.*?)" + END_TCP_HEADER +
					BEGIN_TCP_TEXT + "(.*?)" + END_TCP_TEXT +
				END_TCP_MSG;
			try
			{
				if (!Regex.IsMatch(msg, regularExpression))
				{	//message is not well formed
					Logger.LogError(this, "Receive - wrong message: \"" + receivedData + "\".");
					return new TCPMessage(RETURN_ERROR + "-3");
				}
				else
				{	//well formed message: split
					string[] tokens = Regex.Split(msg, regularExpression);
					message = new TCPMessage(tokens[1], tokens[2]);
				}
			}
			catch (Exception ex)
			{	// Regex.IsMatch() threw an exception:
				// Most likely cause is a syntax error in the regular expression
				Logger.LogError(this, "Receive - probable syntax error in the regular expression", ex);
				return new TCPMessage(RETURN_ERROR + "-3");
			}
			return message;
		}

		/// <summary>
		/// Receive a message from the remoteIP.
		/// The returned array of byte is lenght variable. the maximum size is the specified lenght but 
		/// it can be smaller if the number of bytes received is smaller.
		/// </summary>
		/// <param name="bytes">number of bytes to receive.</param>
		/// <returns>an array of bytes containing the message received if operation succeded, an array of bytes containing the string TCPSocket.RETURN_ERROR fallowed by 
		/// the socket error number in case of socket error, 0 in case of other error.</returns>
		public byte[] ReceiveBytes(int bytes)  
		{
			int numBytesReceived = 0;
			byte[] byteData = new byte[bytes];
			try
			{
				numBytesReceived = socket.Receive(byteData, 0, bytes, SocketFlags.None);
			}
			catch (SocketException socketException)
			{//may be thrown by the Socket.Receive method
				return Encoding.Unicode.GetBytes(RETURN_ERROR + socketException.ErrorCode.ToString());
			}
			catch (Exception ex)
			{
				Logger.LogError(this, ex);
				return Encoding.Unicode.GetBytes(RETURN_ERROR + "0");
			}
			byte[] ret = new byte[numBytesReceived];
			Array.Copy(byteData, ret, numBytesReceived);
			return ret;
		} 

		/// <summary>
		/// Create a string to be sent throw the tcp socket according to the protocol.
		/// </summary>
		/// <param name="header">a string representing the header of the message.</param>
		/// <param name="text">a string representing the text of the message.</param>
		/// <returns></returns>
		private String createString(String header, String text) 
		{
			return BEGIN_TCP_MSG + 
				BEGIN_TCP_HEADER + header + END_TCP_HEADER +
				BEGIN_TCP_TEXT + text + END_TCP_TEXT + 
				END_TCP_MSG;
		}
	}
}
