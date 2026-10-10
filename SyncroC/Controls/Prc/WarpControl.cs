using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SyncroC.Controls.Prc
{
  public partial class WarpControl : SyncroUserControl
  {
    private PictureBox clicked = null;
    private int boarder = 4;

    /// <summary>
    /// Raised when the control is clicked.
    /// </summary>
    public event EventHandler SelectedWarpChanged;

    private bool isWarpUpDown = true;
    [Description("True: Warp UpDown. False: Warp EndToEnd."), Category("FosberSoft")]
    public bool IsWarpUpDown
    {
      get { return isWarpUpDown; }
      set 
      { 
        isWarpUpDown = value;
        this.setUpImage();
      }
    }

    public void setSelectedWarp(int theSelectedWarp)
    {
      int realSelectedWarp = (-theSelectedWarp) + 4;

      foreach (Control a in this.tableLayoutPanel1.Controls)
      {
        if (a is PictureBox)
        {
          if (a.Name == "pct_" + realSelectedWarp.ToString())
          {
            this.clicked = ((PictureBox)a);
            this.Invalidate();
            break;
          }
        }
      }
    }

    public int getSelectedWarp()
    {
      int res = 0;
      try
      {
        res = Convert.ToInt32(this.clicked.Name.Replace("pct_", "")) - 4;
      }
      catch(Exception)
      {
        res = 0;
      }

      return -res;
    }

    public WarpControl()
    {
      InitializeComponent();
      this.setUpImage();
      this.clicked = this.pct_4;
    }

    private void pct_Click(object sender, EventArgs e)
    {
      this.clicked = ((PictureBox)sender);
      this.Invalidate();
      if ((this.SelectedWarpChanged != null) && this.Enabled)
      {
        this.SelectedWarpChanged(this, e);
      }
    }

    private void pct_Paint(object sender, PaintEventArgs e)
    {
      if(this.clicked == ((PictureBox)sender))
        e.Graphics.DrawRectangle(new Pen(Color.Black, this.boarder), 0, 0, this.clicked.Width, this.clicked.Height);
    }

    private void setUpImage()
    {
      this.pct_7.Image = this.isWarpUpDown ? global::SyncroC.Properties.Resources.warp_updown_pos_M3 : global::SyncroC.Properties.Resources.warp_endtoend_pos_M3;
      this.pct_6.Image = this.isWarpUpDown ? global::SyncroC.Properties.Resources.warp_updown_pos_M2 : global::SyncroC.Properties.Resources.warp_endtoend_pos_M2;
      this.pct_5.Image = this.isWarpUpDown ? global::SyncroC.Properties.Resources.warp_updown_pos_M1 : global::SyncroC.Properties.Resources.warp_endtoend_pos_M1;
      this.pct_4.Image = this.isWarpUpDown ? global::SyncroC.Properties.Resources.warp_updown_pos_0 : global::SyncroC.Properties.Resources.warp_endtoend_pos_0;
      this.pct_3.Image = this.isWarpUpDown ? global::SyncroC.Properties.Resources.warp_updown_pos_1 : global::SyncroC.Properties.Resources.warp_endtoend_pos_1;
      this.pct_2.Image = this.isWarpUpDown ? global::SyncroC.Properties.Resources.warp_updown_pos_2 : global::SyncroC.Properties.Resources.warp_endtoend_pos_2;
      this.pct_1.Image = this.isWarpUpDown ? global::SyncroC.Properties.Resources.warp_updown_pos_3 : global::SyncroC.Properties.Resources.warp_endtoend_pos_3;
    }
  }
}
