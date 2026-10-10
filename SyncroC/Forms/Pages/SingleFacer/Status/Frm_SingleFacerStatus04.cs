using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;

namespace SyncroC.Forms.Pages.SingleFacer.Status
{
	public partial class Frm_SingleFacerStatus04 : SyncroC.Forms.Pages.SingleFacer.Status.Frm_SingleFacerStatusSubgroup
	{

		public Frm_SingleFacerStatus04()
		{
			InitializeComponent();
		}

			/// <summary>
		/// Builder. Build an istance of this page for the specified SingleFacer.
		/// </summary>
		/// <param name="index">Index of the singleFacer represented on the current instance of this Form</param>
		public Frm_SingleFacerStatus04(int index)
			: this()
		{
			if (! (index == 1 || index == 2 || index == 3))
				Logger.LogError(this, "Frm_SingleFacerStatus04 - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\" values are allowed.");
			this.index = index;
			lbl_Title_YN.Text += " " + index.ToString();
		}

		protected override void F13()
		{
		}

		public override void CorrugatorDirectionLeft()
		{
			//Single Facer Picture
			this.pct_machine.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
		}
	}
}

