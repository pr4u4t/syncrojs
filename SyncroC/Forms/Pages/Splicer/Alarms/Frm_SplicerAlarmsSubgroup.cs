using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.Splicer.Alarms
{
	public partial class Frm_SplicerAlarmsSubgroup : SyncroC.Forms.Pages.Splicer.Frm_SplicerGroup
	{
		public Frm_SplicerAlarmsSubgroup()
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
					pm.pagesHistorian.CurrentPageSplicerAlarm1 = this.getRequestCommand(index);
					break;
				case 2:
					pm.pagesHistorian.CurrentPageSplicerAlarm2 = this.getRequestCommand(index);
					break;
				case 3:
					pm.pagesHistorian.CurrentPageSplicerAlarm3 = this.getRequestCommand(index);
					break;
				case 4:
					pm.pagesHistorian.CurrentPageSplicerAlarm4 = this.getRequestCommand(index);
					break;
				case 5:
					pm.pagesHistorian.CurrentPageSplicerAlarm5 = this.getRequestCommand(index);
					break;
				case 6:
					pm.pagesHistorian.CurrentPageSplicerAlarm6 = this.getRequestCommand(index);
					break;
				case 7:
					pm.pagesHistorian.CurrentPageSplicerAlarm7 = this.getRequestCommand(index);
					break;
			}
			base._Show();
		}

		protected override void F1()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_SplicerAlarmActive), index);
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_SplicerAlarmHistory), index);
		}
	}
}

