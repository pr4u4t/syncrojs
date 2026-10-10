using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.PreHeaterTern.Alarms
{
	public partial class Frm_PreHeaterTernAlarmSubgroup : SyncroC.Forms.Pages.PreHeaterTern.Frm_PreHeaterTernGroup
	{
		public Frm_PreHeaterTernAlarmSubgroup()
		{
			InitializeComponent();
		}

		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPagePreHeaterTernAlarm = this.getRequestCommand();
			base._Show();
		}

		protected override void F1()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_PreHeaterTernAlarmActive));
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_PreHeaterTernAlarmHistory));
		}

	}
}

