using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SyncroC.Utility;

namespace SyncroC.Forms.Pages.SyncroSystem.LOG
{
	public partial class Frm_Log3 : Frm_LogSubgroup
	{

		public Frm_Log3()
		{
			InitializeComponent();
		}

		protected override void F12()
		{
		}

    protected override void CtrlF5()
    {
      this.tmr_ProcessControl.Enabled = !this.tmr_ProcessControl.Enabled;

      if (this.tmr_ProcessControl.Enabled)
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

    private void tmr_ProcessControl_Tick(object sender, EventArgs e)
    {
      this.updateLog("PrcUserLog.txt", ref file1, ref bytesRead1, ref txb_ProcessControlCurrent, ref txb_ProcessControlOld);
    }
	}
}