#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace SyncroC.Controls.Alarmtimer
{
	public class AlarmTimerTickEventArgs
	{
		private bool alarmVisible;
		public bool AlarmVisible
		{
			get {return alarmVisible;}
		}

		public AlarmTimerTickEventArgs(bool alarmVisible)
		{
			this.alarmVisible = alarmVisible;
		}
	}
}
