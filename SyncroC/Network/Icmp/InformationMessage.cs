using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SyncroC.Network.Icmp
{
	/// <summary>
	/// Defines an ICMP message with an ID and a sequence number.
	/// </summary>
	public class InformationMessage : IcmpMessage 
	{
		/// <summary>Holds the value of the Identifier property.</summary>
		private ushort m_Identifier = 0;
		/// <summary>Holds the value of the SequenceNumber property.</summary>
		private ushort m_SequenceNumber = 0;

		/// <summary>
		/// Initializes a new InformationMessage instance.
		/// </summary>
		public InformationMessage() 
		{
		}
		
		/// <summary>
		/// Gets or sets the identification number.
		/// </summary>
		/// <value>An unsigned short that holds the identification number of this message.</value>
		public ushort Identifier 
		{
			get {	return m_Identifier;	}
			set	{	m_Identifier = value;	}
		}

		/// <summary>
		/// Gets or sets the sequence number.
		/// </summary>
		/// <value>An unsigned short that holds the sequence number of this message.</value>
		public ushort SequenceNumber 
		{
			get	{	return m_SequenceNumber;	}
			set {	m_SequenceNumber = value;	}
		}
		/// <summary>
		/// Serializes the object into an array of bytes.
		/// </summary>
		/// <returns>An array of bytes that represents the ICMP message.</returns>
		public override byte [] GetObjectBytes() 
		{
			byte [] ret = new byte[8];
			Array.Copy(base.GetObjectBytes(), 0, ret, 0, 4);
			Array.Copy(BitConverter.GetBytes(Identifier), 0, ret, 4, 2);
			Array.Copy(BitConverter.GetBytes(SequenceNumber), 0, ret, 6, 2);
			return ret;
		}
	}
}
