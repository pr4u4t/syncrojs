using System;

namespace SyncroC.Controls.IncDecButton
{
	/// <summary>
	/// Summary description for IncDecButtonKitDownEventArgs.
	/// </summary>
	public class IncDecButtonKitDownEventArgs : IncDecButtonDownEventArgs
	{
		
		public IncDecButtonType buttonType;

		public IncDecButtonKitDownEventArgs(int frequency, IncDecButtonType buttonType ) : base(frequency)
		{
			this.buttonType = buttonType;
		}
	}
}
