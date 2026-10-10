using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Controls.Alarmtimer
{
	public delegate void AlarmTimerTickHandler(object sender, AlarmTimerTickEventArgs args);

	public class AlarmTimer : UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		private System.Windows.Forms.Timer timer;
		private bool alarmVisible = true;

		[Description("Event raised when the alarm timer ticks so the blink has to be done."), Category("Fosbersoft")]
		public event AlarmTimerTickHandler Tick;

		#region Component Designer Generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
this.components = new System.ComponentModel.Container();
this.timer = new System.Windows.Forms.Timer(this.components);
// 
// timer
// 
this.timer.Enabled = true;
this.timer.Interval = 1000;
this.timer.Tick += new System.EventHandler(this.timer_Tick);
// 
// AlarmTimer
// 
this.Name = "AlarmTimer";

}
		#endregion

		public AlarmTimer()
		{
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (this.Tick != null)
			{
				Tick(this, new AlarmTimerTickEventArgs(alarmVisible));
				alarmVisible = ! alarmVisible;
			}
		}
	}
}
