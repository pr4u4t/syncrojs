using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace SyncroC.Forms.Pages.SyncroSystem.LOG
{
	public partial class Frm_Log1 : Frm_LogSubgroup
	{

		public Frm_Log1()
		{
			InitializeComponent();
		}

		protected override void F10()
		{
		}

		protected override void CtrlF5()
		{
			tmr_Configuration.Enabled = !tmr_Configuration.Enabled;

			if (tmr_Configuration.Enabled)
			{
				lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
				lbl_CtrlF5_YY.Text = "Pause";
			}
			else
			{
				lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.Black;
				lbl_CtrlF5_YY.Text = "Start";
			}
		}

		private void tmr_Configuration_Tick(object sender, EventArgs e)
		{
			//todo: line configuration
			this.updateLog("Config.txt", ref file1, ref bytesRead1, ref txb_ConfigurationCurrent, ref txb_ConfigurationOld);
		}
	}
}