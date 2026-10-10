using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.DoubleBacker.Status
{
	public partial class Frm_DoubleBackerStatusSubgroup: SyncroC.Forms.Pages.DoubleBacker.Frm_DoubleBackerGroup
	{
    public Frm_DoubleBackerStatusSubgroup()
		{
			InitializeComponent();
		}

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (Historian.CurrentPageDoubleBackerStatus) for the DoubleBackerGroup subgroup is set here.
		/// </summary>
		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageDoubleBacker = this.getRequestCommand();
			base._Show();
		}

    protected override void F3()
    {
    }

		protected override void F10()
		{
			askForPage(typeof(Frm_DoubleBackerStatus01));
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_DoubleBackerStatus02));
		}

		protected override void F12()
		{
			askForPage(typeof(Frm_DoubleBackerStatus03));
		}

		protected override void F13()
		{
			askForPage(typeof(Frm_DoubleBackerStatus04));
		}
	}
}