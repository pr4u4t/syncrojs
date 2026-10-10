using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;

namespace SyncroC.Forms.PopUp
{
  public partial class Frm_AskValueAndConfirm : Frm_PopUp
  {
    private System.Windows.Forms.Button btn_OK;
    private System.Windows.Forms.Button btn_Abort_YN;
    private SyncroC.Controls.TitledNumericUpDown tnd_Value;
    private System.ComponentModel.IContainer components = null;
    
    public Frm_AskValueAndConfirm()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Show the form as a modal dialog
    /// </summary>
    /// <param name="owner">Window that will own the modal dialog</param>
    /// <param name="value"></param>
    public FosberDialogResult ShowDialog(IWin32Window owner, string text, int value)
    {
      lbl_Confirmation_YN.Text = text;
      tnd_Value.Value = value.ToString();
      return new FosberDialogResult(this.ShowDialog(owner), this.data);
    }

    //ABELLO_FI30/10/2008_1 Messi i pulsanti Inc/Dec sulle PopUp che chiedono un valore numerico.
    public FosberDialogResult ShowDialog(IWin32Window owner, string text, int value, int minValue, int maxValue)
    {
      this.tnd_Value.Minimum = minValue;
      this.tnd_Value.Maximum = maxValue;
      return ShowDialog(owner, text, value);
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

    private void btn_OK_Click(object sender, System.EventArgs e)
    {
      try
      {
        this.data = new object[] { Convert.ToInt32(tnd_Value.Value) };
      }
      catch (Exception)
      {
        this.data = new object[] { -1 };
      }
      this.Dispose();
    }

    private void btn_Abort_Click(object sender, System.EventArgs e)
    {
      this.Dispose();
    }

    private void DecreaseButton_OnIncDecButtonDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonDownEventArgs args)
    {
      if (args.Frequency != -2)
      {
        this.DecreaseButton._signal(true);
        try
        {
          this.tnd_Value.Value = (Convert.ToInt32(this.tnd_Value.Value) - 1).ToString();
        }
        catch (Exception)
        {

        }
      }
      else
      {
        this.DecreaseButton._signal(false);
      }
    }

    private void IncreaseButton_OnIncDecButtonDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonDownEventArgs args)
    {
      if (args.Frequency != -2)
      {
        this.IncreaseButton._signal(true);
        try
        {
          this.tnd_Value.Value = (Convert.ToInt32(this.tnd_Value.Value) + 1).ToString();
        }
        catch (Exception)
        {

        }
      }
      else
      {
        this.IncreaseButton._signal(false);
      }
    }

    //agrassi_FI25-11-08
    protected override Size getOriginalSize()
    {
      return this.Size;
    }
  }
}