using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Net.NetworkInformation;

namespace SyncroC.Network.Icmp
{
	/// <summary>
	/// Implements the ICMP messaging service.
	/// </summary>
	/// <remarks>Currently, the implementation only supports the echo message (better known as 'ping').</remarks>
	public class Icmp 
	{
		/// <summary>Stores the value of the Host property.</summary>
		private IPAddress m_Host;
		/// <summary>Stores the value of the ClientSocket property.</summary>
		private Socket m_ClientSocket;
		/// <summary>Stores the value of the StartTime property.</summary>
		private DateTime m_StartTime;
		/// <summary>Stores the value of the PingTimeOut property.</summary>
		private Timer m_PingTimeOut;
		/// <summary>Stores the value of the HasTimedOut property.</summary>
		private bool m_HasTimedOut;

		/// <summary>
		/// Initializes an instance of the Icmp class.
		/// </summary>
		/// <param name="host">The host that will be used to communicate with.</param>
		public Icmp(IPAddress host) 
		{
			Host = host;
		}

		/// <summary>
		/// Generates the Echo message to send.
		/// </summary>
		/// <returns>An array of bytes that represents the ICMP echo message to send.</returns>
		protected byte[] GetEchoMessageBuffer() 
		{
			EchoMessage message = new EchoMessage();
			message.Type = 8;	// ICMP echo
			message.Data = new Byte[32];
			for (int i = 0; i < 32; i++) 
			{
				message.Data[i] = 32;	// Send spaces
			}
			message.CheckSum = message.GetChecksum();
			return message.GetObjectBytes();
		}

		/// <summary>
		/// Initiates an ICMP ping with a timeout of 1000 milliseconds.
		/// </summary>
		/// <exception cref="SocketException">There was an error while communicating with the remote server.</exception>
		/// <returns>A TimeSpan object that holds the time it takes for a packet to travel to the remote server and back. A value of TimeSpan.MaxValue indicates a timeout.</returns>
		/// <example>
		/// The following example will ping the server www.mentalis.org ten times and print the results in the Console.
		/// <c>
		/// <pre>
		/// Icmp icmp = new Icmp(Dns.Resolve("www.mentalis.org").AddressList[0]);
		/// for (int i = 0; i &lt; 10; i++) {
		/// 	Console.WriteLine(icmp.Ping().TotalMilliseconds);
		/// }
		/// </pre>
		/// </c>
		/// </example>
		public TimeSpan Ping() 
		{
			return Ping(1000);
		}

		/// <summary>
		/// Initiates an ICMP ping.
		/// </summary>
		/// <param name="timeout">Specifies the timeout in milliseconds. If this value is set to Timeout.Infinite, the method will never time out.</param>
		/// <returns>A TimeSpan object that holds the time it takes for a packet to travel to the remote server and back. A value of TimeSpan.MaxValue indicates a timeout.</returns>
		public TimeSpan Ping(int timeout) 
		{
			/*TimeSpan ret;
			EndPoint remoteEP = new IPEndPoint(Host, 0);
			ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);
			// Get the ICMP message
			byte [] buffer = GetEchoMessageBuffer();
			// Set the timeout timer
			HasTimedOut = false;
			PingTimeOut = new Timer(new TimerCallback(PingTimedOut), null, timeout, Timeout.Infinite);
			// Get the current time
			StartTime = DateTime.Now;
			// Send the ICMP message and receive the reply
			try 
			{
				if (ClientSocket.SendTo(buffer, remoteEP) <= 0)
					throw new SocketException();
				buffer = new byte[buffer.Length + 20];
				if (ClientSocket.ReceiveFrom(buffer, ref remoteEP) <= 0)
					throw new SocketException();
			} 
			catch (SocketException e) 
			{
				if (HasTimedOut)
					return TimeSpan.MaxValue;
				else
					throw e;
			} 
			finally 
			{
				// Close the socket
				ClientSocket.Close();
				ClientSocket = null;
				// destroy the timer
				PingTimeOut.Change(Timeout.Infinite, Timeout.Infinite);
				PingTimeOut.Dispose();
				PingTimeOut = null;
			}*/

            //nowy kod pinga
            Ping p = new Ping();
            PingReply r;
            r = p.Send(Host);
            return TimeSpan.FromMilliseconds(r.RoundtripTime);
			// Get the time
			//ret = DateTime.Now.Subtract(StartTime);
			//return ret;
		}

		/// <summary>
		/// Called when the ping method times out.
		/// </summary>
		/// <param name="state">The source of the event. This is an object containing application-specific information relevant to the methods invoked by this delegate, or a null reference (Nothing in Visual Basic).</param>
		protected void PingTimedOut(object state) 
		{
			HasTimedOut = true;
			// Close the socket (this will result in a throw of a SocketException in the Ping method)
			if (ClientSocket != null)
				ClientSocket.Close();
		}

		/// <summary>
		/// Gets or sets the address of the remote host to communicate with.
		/// </summary>
		/// <value>An IPAddress instance that specifies the address of the remote host to communicate with.</value>
		/// <exception cref="ArgumentNullException">The specified value is null (Nothing in VB.NET).</exception>
		protected IPAddress Host 
		{
			get {	return m_Host;	}
			set 
			{
				if (value == null)
					throw new ArgumentNullException();
				m_Host = value;
			}
		}

		/// <summary>
		/// Gets or sets the Socket that is used to communicate with the remote server.
		/// </summary>
		/// <value>A Socket object that is used to communicate with the remote server.</value>
		protected Socket ClientSocket 
		{
			get {	return m_ClientSocket;	}
			set {	m_ClientSocket = value;	}
		}

		/// <summary>
		/// Gets or sets the time when the ping began/begins.
		/// </summary>
		/// <value>A DateTime object that specifies when the ping began.</value>
		protected DateTime StartTime 
		{
			get {	return m_StartTime;	}
			set {	m_StartTime = value;}
		}

		/// <summary>
		/// Gets or sets the timer that triggers the PingTimedOut method.
		/// </summary>
		/// <value>A Timer object that triggers the PingTimedOut method.</value>
		protected Timer PingTimeOut 
		{
			get {	return m_PingTimeOut;	}
			set	{	m_PingTimeOut = value;}
		}

		/// <summary>
		/// Gets or sets a value that indicates whether the ping has timed out or not.
		/// </summary>
		/// <value>A boolean value that indicates whether the ping has timed out or not.</value>
		protected bool HasTimedOut 
		{
			get	{	return m_HasTimedOut;	}
			set	{	m_HasTimedOut = value;}
		}

		/// <summary>
		/// Releases all the resources.
		/// </summary>
		~Icmp() 
		{
			if (ClientSocket != null)
				ClientSocket.Close();
		}
	}
}
