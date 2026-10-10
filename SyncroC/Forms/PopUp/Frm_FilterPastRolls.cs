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
  public partial class Frm_FilterPastRolls : SyncroC.Forms.PopUp.Frm_PopUp
  {
    public Frm_FilterPastRolls()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Show the form as a modal dialog
    /// </summary>
    /// <param name="owner">Window that will own the modal dialog</param>
    /// <param name="value"></param>
    public FosberDialogResult ShowDialog(IWin32Window owner, string rollIdFilter, string paperTypeFilter)
    {
      this.btn_Abort_NN.Text = DBManager.getDBManager().language.getTranslation("Frm_AskValue.btn_Abort_YN.Text");

      this.ttb_rollId.Text = DBManager.getDBManager().language.getTranslation("Frm_StatisticsRollsList.gdv_PastRolls.3");
      this.ttb_rollId.Value = rollIdFilter;
      this.ttb_rollId.Select();

      this.ttb_paperType.Text = DBManager.getDBManager().language.getTranslation("Frm_StatisticsRollsList.gdv_PastRolls.2");
      this.ttb_paperType.Value = paperTypeFilter;

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
        this.data = new object[] { this.ttb_rollId.Value, this.ttb_paperType.Value };
      }
      catch (Exception)
      {
        this.data = new object[] { "" };
      }
      this.Dispose();
    }

    private void btn_Abort_YN_Click(object sender, EventArgs e)
    {
      this.Dispose();
    }
  }
}
