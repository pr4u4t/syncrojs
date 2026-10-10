using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.GlueUnit.Alarms
{
	public partial class Frm_GlueUnitAlarmSubgroup : SyncroC.Forms.Pages.GlueUnit.Frm_GlueUnitGroup
	{
		public Frm_GlueUnitAlarmSubgroup()
		{
			InitializeComponent();
		}

		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageGlueUnitAlarm = this.getRequestCommand();
			base._Show();
		}

		protected override void F1()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_GlueUnitAlarmActive));
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_GlueUnitAlarmHistory));
		}
	}
}

