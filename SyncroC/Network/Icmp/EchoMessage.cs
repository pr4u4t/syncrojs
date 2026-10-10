using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SyncroC.Network.Icmp
{
	/// <summary>
	/// Defines an echo ICMP message.
	/// </summary>
	public class EchoMessage : InformationMessage 
	{
		/// <summary>Holds the value of the Data property.</summary>
		private byte[] m_Data;

		/// <summary>
		/// Initializes a new EchoMessage instance.
		/// </summary>
		public EchoMessage() 
		{
		}

		/// <summary>
		/// Gets or sets the data of this message.
		/// </summary>
		/// <value>An array of bytes that represents the data of this message.</value>
		public byte[] Data 
		{
			get {	return m_Data;	}
			set	{	m_Data = value;	}
		}

		/// <summary>
		/// Serializes the object into an array of bytes.
		/// </summary>
		/// <returns>An array of bytes that represents the ICMP message.</returns>
		public override byte [] GetObjectBytes() 
		{
			int length = 8;
			if (Data != null)
				length += Data.Length;
			byte [] ret = new byte[length];
			Array.Copy(base.GetObjectBytes(), 0, ret, 0, 8);
			if (Data != null)
				Array.Copy(Data, 0, ret, 8, Data.Length);
			return ret;
		}
	}
}
