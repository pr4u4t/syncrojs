using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.DoubleBacker.Status
{
  public partial class Frm_DoubleBackerStatus04 : SyncroC.Forms.Pages.DoubleBacker.Status.Frm_DoubleBackerStatusSubgroup
	{
		public Frm_DoubleBackerStatus04()
		{
			InitializeComponent();
		}

		protected override void F13()
		{ }

		public override void CorrugatorDirectionLeft()
		{
			int leftLimit = this.pct_DoubleBacker.Location.X;
			int rightLimit = this.pct_DoubleBacker.Location.X + this.pct_DoubleBacker.Width;

			//stacker frame
			this.pct_DoubleBacker.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
		}
	}
}