using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.Controls;
using SyncroC.Controls.IncDecButton;

namespace SyncroC.Forms.Pages.SingleFacer.Console
{
	public partial class Frm_SingleFacerConsole02 : SyncroC.Forms.Pages.SingleFacer.Console.Frm_SingleFacerConsoleSubgroup
	{
		public Frm_SingleFacerConsole02()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Builder. Build an istance of this page for the specified SingleFacer.
		/// </summary>
		/// <param name="index">Index of the singleFacer represented on the current instance of this Form</param>
		public Frm_SingleFacerConsole02(int index)
			: this()
		{
			if (! (index == 1 || index == 2 || index == 3))
				Logger.LogError(this, "Frm_SingleFacerConsole02 - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\" values are allowed.");
			this.index = index;
			lbl_Title_YN.Text += " " + index.ToString();
		}

		protected override void F11()
		{
		}
	}
}

