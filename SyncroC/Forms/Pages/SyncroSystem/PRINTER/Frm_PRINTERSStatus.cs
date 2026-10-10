using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.SyncroSystem.PRINTER
{
    public partial class Frm_PRINTERSStatus : SyncroC.Forms.Pages.SyncroSystem.PRINTER.Frm_PRINTERSSubGroup
    {
      public Frm_PRINTERSStatus()
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

			protected override void F11()
			{
			}

        private void titledNumericUpDown4_Load(object sender, EventArgs e)
        {

        }
    }
}

