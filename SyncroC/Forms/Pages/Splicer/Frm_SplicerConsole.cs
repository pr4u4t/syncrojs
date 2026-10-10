using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;

namespace SyncroC.Forms.Pages.Splicer
{
	public partial class Frm_SplicerConsole : SyncroC.Forms.Pages.Splicer.Frm_SplicerGroup
	{
		public Frm_SplicerConsole()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Builder. Build an istance of this page for the specified Splicer.
		/// </summary>
		/// <param name="index">Index of the splicer represented on the current instance of this Form</param>
		public Frm_SplicerConsole(int index)
			: this()
		{
			if (!(index == 1 || index == 2 || index == 3 || index == 4 || index == 5 || index == 6 || index == 7))
				Logger.LogError(this, "Frm_SplicerConsole - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\", \"4\", \"5\", \"6\", \"7\" values are allowed.");
			this.index = index;
			lbl_Title_YN.Text += " " + index.ToString();
		}

		protected override void F4()
		{
		}
	}
}

