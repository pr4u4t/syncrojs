using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;

namespace SyncroC.Forms.Pages.Stacker.Status
{
  public partial class Frm_StackerStatus04 : SyncroC.Forms.Pages.Stacker.Status.Frm_StackerStatusSubgroup
  {
    public Frm_StackerStatus04()
    {
      InitializeComponent();
    }

    /// <summary>
		/// Builder. Build an istance of this page for the specified Stacker.
		/// </summary>
		/// <param name="index">Index of the stacker represented on the current instance of this Form</param>
		public Frm_StackerStatus04(int index) : this()
		{
			if (! (index == 1 || index == 2 || index == 3)) 
				Logger.LogError(this, "Frm_StackerStatus - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\" values are allowed.");
			this.index = index;

			lbl_Title_YN.Text += " " + index.ToString();
		}
  }
}