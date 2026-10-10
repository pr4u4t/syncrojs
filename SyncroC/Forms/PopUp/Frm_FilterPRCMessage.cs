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
  public partial class Frm_FilterPRCMessage : SyncroC.Forms.PopUp.Frm_PopUp
  {
    public Frm_FilterPRCMessage()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Show the form as a modal dialog
    /// </summary>
    /// <param name="owner">Window that will own the modal dialog</param>
    /// <param name="value"></param>
    public FosberDialogResult ShowDialog(IWin32Window owner, string qualityIdFilter, string fluteFilter, string[] fluteStrings)
    {
      this.btn_Abort_NN.Text = DBManager.getDBManager().language.getTranslation("Frm_AskValue.btn_Abort_YN.Text");

      this.ttb_QualityId.Text = DBManager.getDBManager().language.getTranslation("Frm_Quality.ttb_QualityID.Text");
      this.ttb_QualityId.Value = qualityIdFilter;
      this.ttb_QualityId.Select();

      this.tlst_FluteID.Text = DBManager.getDBManager().language.getTranslation("Frm_Quality.tlst_FluteID.Text");
      this.tlst_FluteID.ClearItems();
      this.tlst_FluteID.AddRangeItem(fluteStrings);
      this.tlst_FluteID.SelectedItem = fluteFilter;

      return new FosberDialogResult(this.ShowDialog(owner), this.data);
    }

    protected override Size getOriginalSize()
    {
      return this.Size;
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
      try
      {
        this.data = new object[] { this.ttb_QualityId.Value, this.tlst_FluteID.Value };
      }
      catch (Exception)
      {
        this.data = new object[] { "" };
      }
      this.Dispose();
    }

    private void btn_Abort_NN_Click(object sender, EventArgs e)
    {
      this.Dispose();
    }
  }
}
