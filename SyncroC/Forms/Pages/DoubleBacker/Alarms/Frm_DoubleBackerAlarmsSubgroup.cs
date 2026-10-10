using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.DoubleBacker.Alarms
{
	public partial class Frm_DoubleBackerAlarmsSubgroup : SyncroC.Forms.Pages.DoubleBacker.Frm_DoubleBackerGroup
	{
		public Frm_DoubleBackerAlarmsSubgroup()
		{
			InitializeComponent();
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_DoubleBackerAlarmsActive));
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_DoubleBackerAlarmsHistory));
		}
	}
}

