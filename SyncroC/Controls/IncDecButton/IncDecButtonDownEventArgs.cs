using System;

namespace SyncroC.Controls.IncDecButton
{
	/// <summary>
	/// IncDecButtonDownEventArgs
	/// </summary>
	public class IncDecButtonDownEventArgs : System.EventArgs
	{
		/// <summary>
		/// Milliseconds between the previous OnIncDecButtonDownEvent fired.
		/// -1 means first fire.
		/// -2 means last fire.
		/// </summary>
		public int Frequency;

		public IncDecButtonDownEventArgs(int frequency)
		{
			Frequency = frequency;
		}
	}
}
