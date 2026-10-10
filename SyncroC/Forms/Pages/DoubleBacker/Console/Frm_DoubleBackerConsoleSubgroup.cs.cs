using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.DoubleBacker.Console
{
	public partial class Frm_DoubleBackerConsoleSubgroup : SyncroC.Forms.Pages.DoubleBacker.Frm_DoubleBackerGroup
	{
		public Frm_DoubleBackerConsoleSubgroup()
		{
			InitializeComponent();
		}

		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageDoubleBackerConsole = this.getRequestCommand();
			base._Show();
		}

		protected override void F4()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_DoubleBackerConsole01));
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_DoubleBackerConsole02));
		}

		protected override void F12()
		{
			askForPage(typeof(Frm_DoubleBackerConsole03));
		}

		protected override void F13()
		{
			askForPage(typeof(Frm_DoubleBackerConsole04));
		}
	}
}