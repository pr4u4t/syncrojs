using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SyncroC.Network.Icmp
{
	/// <summary>
	/// Defines a base ICMP message
	/// </summary>
	public abstract class IcmpMessage 
	{
		/// <summary>Holds the value of the Type property.</summary>
		private byte m_Type = 0;
		/// <summary>Holds the value of the Code property.</summary>
		private byte m_Code = 0;
		/// <summary>Holds the value of the CheckSum property.</summary>
		private ushort m_CheckSum = 0;

		/// <summary>
		/// Initializes a new IcmpMessage instance.
		/// </summary>
		public IcmpMessage() 
		{
		}

		/// <summary>
		/// Gets or sets the type of the message.
		/// </summary>
		/// <value>A byte that specifies the type of the message.</value>
		public byte Type 
		{
			get {	return m_Type;	}
			set	{	m_Type = value;	}
		}

		/// <summary>
		/// Gets or sets the message code.
		/// </summary>
		/// <value>A byte that specifies the message code.</value>
		public byte Code 
		{
			get {	return m_Code;	}
			set	{	m_Code = value;	}
		}

		/// <summary>
		/// Gets or sets the chacksum for this message.
		/// </summary>
		/// <value>An unsigned short that holds the checksum of this message.</value>
		public ushort CheckSum 
		{
			get {	return m_CheckSum;	}
			set	{	m_CheckSum = value;	}
		}

		/// <summary>
		/// Serializes the object into an array of bytes.
		/// </summary>
		/// <returns>An array of bytes that represents the ICMP message.</returns>
		public virtual byte [] GetObjectBytes() 
		{
			byte [] ret = new byte[4];
			Array.Copy(BitConverter.GetBytes(Type), 0, ret, 0, 1);
			Array.Copy(BitConverter.GetBytes(Code), 0, ret, 1, 1);
			Array.Copy(BitConverter.GetBytes(CheckSum), 0, ret, 2, 2);
			return ret;
		}

		/// <summary>
		/// Calculates the checksum of this message.
		/// </summary>
		/// <returns>An unsigned short that holds the checksum of this ICMP message.</returns>
		public ushort GetChecksum() 
		{
			ulong sum = 0;
			byte [] bytes = GetObjectBytes();
			// Sum all the words together, adding the final byte if size is odd
			int i;
			for (i = 0; i < bytes.Length - 1; i += 2) 
			{
				sum += BitConverter.ToUInt16(bytes, i);
			}
			if (i != bytes.Length)
				sum += bytes[i];
			// Do a little shuffling
			sum = (sum >> 16) + (sum & 0xFFFF);
			sum += (sum >> 16);
			return (ushort)(~sum);
		}
	}
}
