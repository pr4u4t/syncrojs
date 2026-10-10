using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.SingleFacer.Status
{
	public partial class Frm_SingleFacerStatusSubgroup : SyncroC.Forms.Pages.SingleFacer.Frm_SingleFacerGroup
	{
		public Frm_SingleFacerStatusSubgroup()
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
					pm.pagesHistorian.CurrentPageSingleFacerStatus1 = this.getRequestCommand(index);
					break;
				case 2:
					pm.pagesHistorian.CurrentPageSingleFacerStatus2 = this.getRequestCommand(index);
					break;
				case 3:
					pm.pagesHistorian.CurrentPageSingleFacerStatus3 = this.getRequestCommand(index);
					break;
			}
			base._Show();
		}

		protected override void F3()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_SingleFacerStatus01), index);
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_SingleFacerStatus02), index);
		}

		protected override void F12()
		{
			askForPage(typeof(Frm_SingleFacerStatus03), index);
		}

		protected override void F13()
		{
			askForPage(typeof(Frm_SingleFacerStatus04), index);
		}
	}
}

