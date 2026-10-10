using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.SingleFacer.Console
{
	public partial class Frm_SingleFacerConsoleSubgroup : SyncroC.Forms.Pages.SingleFacer.Frm_SingleFacerGroup
	{
		public Frm_SingleFacerConsoleSubgroup()
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
					pm.pagesHistorian.CurrentPageSingleFacerConsole1 = this.getRequestCommand(index);
					break;
				case 2:
					pm.pagesHistorian.CurrentPageSingleFacerConsole2 = this.getRequestCommand(index);
					break;
				case 3:
					pm.pagesHistorian.CurrentPageSingleFacerConsole3 = this.getRequestCommand(index);
					break;
			}
			base._Show();
		}

		protected override void F4()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_SingleFacerConsole01), index);
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_SingleFacerConsole02), index);
		}

		protected override void F12()
		{
			askForPage(typeof(Frm_SingleFacerConsole03), index);
		}

		protected override void F13()
		{
			askForPage(typeof(Frm_SingleFacerConsole04), index);
		}
	}
}

