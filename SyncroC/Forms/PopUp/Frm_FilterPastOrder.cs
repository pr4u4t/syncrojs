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
  public partial class Frm_FilterPastOrder : SyncroC.Forms.PopUp.Frm_PopUp
  {
    public Frm_FilterPastOrder()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Show the form as a modal dialog
    /// </summary>
    /// <param name="owner">Window that will own the modal dialog</param>
    /// <param name="value"></param>
    public FosberDialogResult ShowDialog(IWin32Window owner, string setupCodeFilter, string orderNumberFilter, string customerNameFilter)
    {
      this.btn_Abort_NN.Text = DBManager.getDBManager().language.getTranslation("Frm_AskValue.btn_Abort_YN.Text");

      this.ttb_SetupCode.Text = DBManager.getDBManager().language.getTranslation("Frm_StatusBarDryEnd.gdv_SetupData.0");
      this.ttb_SetupCode.Value = setupCodeFilter;
      this.ttb_SetupCode.Select();

      this.ttb_orderNumber.Text = DBManager.getDBManager().language.getTranslation("Frm_QuickInsDryOrder.ttb_OrderNumber1.Text");
      this.ttb_orderNumber.Value = orderNumberFilter;

      this.ttb_customerName.Text = DBManager.getDBManager().language.getTranslation("Frm_QuickInsDryOrder.ttb_CustName1.Text");
      this.ttb_customerName.Value = customerNameFilter;

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
        this.data = new object[] { this.ttb_SetupCode.Value, this.ttb_orderNumber.Value, this.ttb_customerName.Value };
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
