using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.SyncroSystem.OFFICE
{
	public partial class Frm_OFFICESubGroup : SyncroC.Forms.Pages.SyncroSystem.Frm_SyncroSystemGroup
	{
		public Frm_OFFICESubGroup()
		{
			InitializeComponent();
		}

		protected override void F6()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_OFFICESettings));
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_OFFICEStatus));
		}

		protected override void F12()
		{
			askForPage(typeof(Frm_PRCSettings));
		}

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (Historian.CurrentPageSyncroSystem_Office) for the SyncroSystem_Office subgroup is set here.
		/// </summary>
		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageSyncroSystem_Office = this.getRequestCommand();
			base._Show();
		}
	}

}