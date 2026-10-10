using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.Statistics.Orders
{
	public partial class Frm_StatisticsOrdersSubgroup : Frm_StatisticsGroup
	{
		public Frm_StatisticsOrdersSubgroup()
		{
			InitializeComponent();
		}

		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageStatisticsOrders = this.getRequestCommand();
			base._Show();
		}

		protected override void F3()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_StatisticsOrdersList));
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_StatisticsOrdersDetail));
		}
	}
}