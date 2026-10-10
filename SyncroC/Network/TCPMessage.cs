using System;

namespace SyncroC.Network
{
	/// <summary>
	/// Incapsulate the information of a TCPMessage such as header, text and eventually an error code.
	/// </summary>
	public class TCPMessage
	{
		/// <summary>A string representing the error occurred. It is empty if no error occured.</summary>
		public readonly string error;
		/// <summary>The header of the message. It is empty in case of error.</summary>
		public readonly string header;
		/// <summary>The text of the message. It is empty in case of error.</summary>
		public readonly string text;

		/// <summary>
		/// Build a TCP message that has no errors.
		/// </summary>
		/// <param name="addressee">the addressee module of the server.</param>
		/// <param name="text">the text of the message.</param>
		public TCPMessage(string addressee, string text)
		{
			this.error = String.Empty;
			this.header = addressee;
			this.text = text;
		}

		/// <summary>
		/// Build a message with an error.
		/// </summary>
		/// <param name="error">A string representing the error occurred.</param>
		public TCPMessage(string error)
		{
			this.error = error;
			this.header = String.Empty;
			this.text = String.Empty;
		}
	}
}
