using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.SyncroSystem.PRINTER
{
    public partial class Frm_PRINTERSSubGroup : SyncroC.Forms.Pages.SyncroSystem.Frm_SyncroSystemGroup
    {
        public Frm_PRINTERSSubGroup()
        {
            InitializeComponent();
        }

				/// <summary>
				/// Clean up any resources being used.
				/// </summary>
				protected override void Dispose(bool disposing)
				{
					if (disposing)
					{
						if (components != null)
						{
							components.Dispose();
						}
					}
					base.Dispose(disposing);
				}

				protected override void F5()
				{
				}

				protected override void F10()
				{
					askForPage(typeof(Frm_PRINTERSSettings));
				}

				protected override void F11()
				{
					askForPage(typeof(Frm_PRINTERSStatus));
				}

				/// <summary>
				/// To be called by the server and invoked by the client throw the reflection to show a new page.
				/// Last visited page (Historian.CurrentPageSyncroSystem_Printers) for the SyncroSystem_Printers subgroup is set here.
				/// </summary>
				public override void _Show()
				{
					PageManager pm = PageManager.getPageManager();
					pm.pagesHistorian.CurrentPageSyncroSystem_Printers = this.getRequestCommand();
					base._Show();
				}
    }
}

