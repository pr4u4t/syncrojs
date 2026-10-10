using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.GlueUnit.Console
{
	public partial class Frm_GlueUnitConsoleSubgroup : SyncroC.Forms.Pages.GlueUnit.Frm_GlueUnitGroup
	{
		public Frm_GlueUnitConsoleSubgroup()
		{
			InitializeComponent();
		}

		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageGlueUnitConsole = this.getRequestCommand();
			base._Show();
		}

		protected override void F4()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_GlueUnitConsoleWrapArms));
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_GlueUnitConsoleGlueGaps));
		}
	}
}

