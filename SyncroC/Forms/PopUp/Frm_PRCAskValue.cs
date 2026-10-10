using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.Forms.PopUp
{
  public partial class Frm_PRCAskValue : SyncroC.Forms.PopUp.Frm_PopUp
  {
    public Frm_PRCAskValue()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Show the form as a modal dialog
    /// </summary>
    /// <param name="owner">Window that will own the modal dialog</param>
    /// <param name="value"></param>
    public FosberDialogResult ShowDialog(IWin32Window owner, int valueStandard, int valueWarpCorrection, int valueUserCorrection, int valueSetpoint)
    {
      this.btn_Abort_NN.Text = DBManager.getDBManager().language.getTranslation("Frm_AskValue.btn_Abort_YN.Text");

      this.tnd_ValueUserCorrection.Value = valueUserCorrection.ToString();
      this.tnd_ValueSetpoint.Value = valueSetpoint.ToString();
      this.lbl_Standard.Text = valueStandard.ToString();
      this.lbl_warp.Text = valueWarpCorrection.ToString();
      this.timer_Focus.Enabled = true;
      return new FosberDialogResult(this.ShowDialog(owner), this.data);
    }

    public FosberDialogResult ShowDialog(IWin32Window owner, int valueStandard, int valueWarpCorrection, int valueUserCorrection, int valueSetpoint, int minValue, int maxValue)
    {
      this.btn_Abort_NN.Text = DBManager.getDBManager().language.getTranslation("Frm_AskValue.btn_Abort_YN.Text");

      this.tnd_ValueSetpoint.Minimum = minValue;
      this.tnd_ValueSetpoint.Maximum = maxValue;
      return ShowDialog(owner, valueStandard, valueWarpCorrection, valueUserCorrection, valueSetpoint);
    }

    private void btn_OK_Click(object sender, System.EventArgs e)
    {
      try
      {
        this.data = new object[] { Convert.ToInt32(tnd_ValueUserCorrection.Value), Convert.ToInt32(tnd_ValueSetpoint.Value) };
      }
      catch (Exception)
      {
        this.data = new object[] { -1, -1 };
      }
      this.Dispose();
    }

    protected override Size getOriginalSize()
    {
      return this.Size;
    }

    private void btn_Abort_Click(object sender, System.EventArgs e)
    {
      this.Dispose();
    }

    private void DecreaseButtonUserCorrection_OnIncDecButtonDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonDownEventArgs args)
    {
      if (args.Frequency != -2)
      {
        this.DecreaseButtonUserCorrection._signal(true);
        try
        {
          this.tnd_ValueUserCorrection.Value = (Convert.ToInt32(this.tnd_ValueUserCorrection.Value) - 1).ToString();
        }
        catch (Exception)
        {

        }
      }
      else
      {
        this.DecreaseButtonUserCorrection._signal(false);
      }
    }

    private void IncreaseButtonUserCorrection_OnIncDecButtonDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonDownEventArgs args)
    {
      if (args.Frequency != -2)
      {
        this.IncreaseButtonUserCorrection._signal(true);
        try
        {
          this.tnd_ValueUserCorrection.Value = (Convert.ToInt32(this.tnd_ValueUserCorrection.Value) + 1).ToString();
        }
        catch (Exception)
        {

        }
      }
      else
      {
        this.IncreaseButtonUserCorrection._signal(false);
      }
    }

    private void tnd_ValueUserCorrection_ValueChanged(object sender, EventArgs e)
    {
      try
      {
        int standard = Convert.ToInt32(this.lbl_Standard.Text);
        int warp = Convert.ToInt32(this.lbl_warp.Text);
        int user = this.tnd_ValueUserCorrection.getValueAsInt();

        this.tnd_ValueSetpoint.Value = (standard + warp + user).ToString();
      }
      catch (Exception)
      {

      }
    }

    private void timer_Calcola_Tick(object sender, EventArgs e)
    {
      if (!this.tnd_ValueUserCorrection.Focused)
        return;

      try
      {
        int standard = Convert.ToInt32(this.lbl_Standard.Text);
        int warp = Convert.ToInt32(this.lbl_warp.Text);
        int user = this.tnd_ValueUserCorrection.getValueAsInt();

        this.tnd_ValueSetpoint.Value = (standard + warp + user).ToString();
      }
      catch (Exception)
      {

      }
    }

    private void timer_Focus_Tick(object sender, EventArgs e)
    {
      this.tnd_ValueUserCorrection.Focus();
      this.timer_Focus.Enabled = false;
    }
  }
}
