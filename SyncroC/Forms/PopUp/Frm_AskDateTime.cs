using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;

namespace SyncroC.Forms.PopUp
{
  public partial class Frm_AskDateTime : SyncroC.Forms.PopUp.Frm_PopUp
  {
    public Frm_AskDateTime()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Show the form as a modal dialog
    /// </summary>
    /// <param name="owner">Window that will own the modal dialog</param>
    /// <param name="value"></param>
    public FosberDialogResult ShowDialog(IWin32Window owner, string text, DateTime dtUniversal)
    {
      this.Text = text;
      this.dateTimePicker.Value = dtUniversal.ToLocalTime();
      return new FosberDialogResult(this.ShowDialog(owner), this.data);
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
      this.data = new object[1];
      this.data[0] = this.dateTimePicker.Value.ToUniversalTime();
      this.Dispose();
    }

    private void btn_Abort_YN_Click(object sender, EventArgs e)
    {
      this.Dispose();
    }
  }
}
