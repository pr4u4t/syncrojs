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
	public partial class Frm_Log2 : Frm_LogSubgroup
	{
		private FileInfo file2 = null;
    private int bytesRead2 = 0;

		public Frm_Log2()
		{
			InitializeComponent();
		}

		protected override void F11()
		{
		}

		protected override void CtrlF5()
		{
			tmr_Calibration.Enabled = !tmr_Calibration.Enabled;

			if (tmr_Calibration.Enabled)
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

		private void tmr_Calibration_Tick(object sender, EventArgs e)
		{
			//todo: line configuration
			this.updateLog("CalibrationUserLog1.txt", ref file1, ref bytesRead1, ref txb_CalibrationCurrent1, ref txb_CalibrationOld1);
			this.updateLog("CalibrationUserLog2.txt", ref file2, ref bytesRead2, ref txb_CalibrationCurrent2, ref txb_CalibrationOld2);
		}
	}
}