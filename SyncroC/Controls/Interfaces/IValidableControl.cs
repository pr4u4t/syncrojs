using System;

namespace SyncroC.Controls.Interfaces
{	
	/// <summary>
	/// Define an interface to be implemented by every UserControl defined for SyncroC that are Validable.
	/// </summary>
	interface IValidableControl
	{
		void SetError(string error);
		string Value
		{
			get;
		}
	}
}
