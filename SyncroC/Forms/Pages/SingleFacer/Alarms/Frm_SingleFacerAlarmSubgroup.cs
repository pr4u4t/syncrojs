using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.SingleFacer.Alarms
{
	public partial class Frm_SingleFacerAlarmSubgroup : SyncroC.Forms.Pages.SingleFacer.Frm_SingleFacerGroup
	{
		public Frm_SingleFacerAlarmSubgroup()
		{
			InitializeComponent();
		}

		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			switch (index)
			{
				case 1:
				default:
					pm.pagesHistorian.CurrentPageSingleFacerAlarm1 = this.getRequestCommand(index);
					break;
				case 2:
					pm.pagesHistorian.CurrentPageSingleFacerAlarm2 = this.getRequestCommand(index);
					break;
				case 3:
					pm.pagesHistorian.CurrentPageSingleFacerAlarm3 = this.getRequestCommand(index);
					break;
			}
			base._Show();
		}

		protected override void F1()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_SingleFacerAlarmActive), index);
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_SingleFacerAlarmHistory), index);
		}
	}
}

