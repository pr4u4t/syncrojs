using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.Shear.Alarms
{
	public partial class Frm_ShearAlarmsSubgroup : SyncroC.Forms.Pages.Shear.Frm_ShearGroup
	{
		public Frm_ShearAlarmsSubgroup()
		{
			InitializeComponent();
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_ShearAlarmActive));
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_ShearAlarmHistory));
		}
	}
}

