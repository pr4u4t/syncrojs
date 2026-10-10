using System;

namespace SyncroC.Network
{
	/// <summary>
	/// Incapsulate informations about a message sent by UDP protocol to/from the server.
	/// </summary>
	public class UDPMessage
	{
		/// <summary>A string representing an eventual occurred error. Empty if no error occurred.</summary>
		public readonly string error;
		/// <summary>The header of the message. (first part) In case of error is empty.</summary>
		public readonly string header;
		/// <summary>The text of the message. (second part) In case of error is empty.</summary>
		public readonly string text;

		/// <summary>
		/// Build an UDPMessage that has no error.
		/// </summary>
		/// <param name="header">the first part of the message.</param>
		/// <param name="text">the second part of the message.</param>
		public UDPMessage(string header, string text)
		{
			this.error = String.Empty;
			if (header != null)
				this.header = header;
			else
				this.header = String.Empty;
			this.text = text;
		}
	
		/// <summary>
		/// Build an UDPMessage with an error.
		/// </summary>
		/// <param name="error"> A string representing the error occurred.</param>
		public UDPMessage(string error)
		{
			this.error = error;
			this.header = String.Empty;
			this.text = String.Empty;
		}
	}
}
