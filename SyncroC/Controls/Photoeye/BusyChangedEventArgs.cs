using System;

namespace SyncroC.Controls.Photoeye
{
	/// <summary>
	/// BusyChangedEventArgs.
	/// </summary>
	public class BusyChangedEventArgs : System.EventArgs
	{
		/// <summary>
		/// The new state of the photoeye.
		/// </summary>
		public readonly bool Busy;

		/// <summary>
		/// Builder.
		/// </summary>
		/// <param name="Busy">The new state of the photoeye.</param>
		public BusyChangedEventArgs(bool Busy)
		{
			this.Busy = Busy;
		}
	}
}
