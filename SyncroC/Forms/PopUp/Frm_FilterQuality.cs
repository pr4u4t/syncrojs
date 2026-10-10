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
  public partial class Frm_FilterQuality : SyncroC.Forms.PopUp.Frm_PopUp
  {
    public Frm_FilterQuality()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Show the form as a modal dialog
    /// </summary>
    /// <param name="owner">Window that will own the modal dialog</param>
    /// <param name="value"></param>
    public FosberDialogResult ShowDialog(IWin32Window owner, string qualityIdFilter, string fluteFilter, string[] fluteStrings, string paperIdFilter, string paperWeightFactor)
    {
      this.btn_Abort_NN.Text = DBManager.getDBManager().language.getTranslation("Frm_AskValue.btn_Abort_YN.Text");

      this.tlst_PaperWeightFactor.Text = DBManager.getDBManager().language.getTranslation("Frm_Paper.ttl_PapersList.Text");

      this.ttb_QualityId.Text = DBManager.getDBManager().language.getTranslation("Frm_Quality.ttb_QualityID.Text");
      this.ttb_QualityId.Value = qualityIdFilter;
      this.ttb_QualityId.Select();

      this.tlst_FluteID.Text = DBManager.getDBManager().language.getTranslation("Frm_Quality.tlst_FluteID.Text");
      this.tlst_FluteID.ClearItems();
      this.tlst_FluteID.AddRangeItem(fluteStrings);
      this.tlst_FluteID.SelectedItem = fluteFilter;

      this.ttb_PaperID.Text = DBManager.getDBManager().language.getTranslation("Frm_Paper.ttb_PaperID.Text");
      this.ttb_PaperID.Value = paperIdFilter;

      this.tlst_PaperWeightFactor.Text = DBManager.getDBManager().language.getTranslation("Frm_Paper.tnud_WeightFactor.Text");
      this.tlst_PaperWeightFactor.ClearItems();
      this.tlst_PaperWeightFactor.AddRangeItem(SyncroC.DataBase.PRCManager.getPRCManager().getAllPaperTreshold());
      this.tlst_PaperWeightFactor.SelectedItem = paperWeightFactor;

      return new FosberDialogResult(this.ShowDialog(owner), this.data);
    }

    protected override Size getOriginalSize()
    {
      return this.Size;
    }

    private void btn_OK_Click(object sender, System.EventArgs e)
    {
      try
      {
        this.data = new object[] { this.ttb_QualityId.Value, this.tlst_FluteID.Value, this.ttb_PaperID.Value, this.tlst_PaperWeightFactor.Value };
      }
      catch (Exception)
      {
        this.data = new object[] { "" };
      }
      this.Dispose();
    }

    private void btn_Abort_Click(object sender, System.EventArgs e)
    {
      this.Dispose();
    }

    private void btn_selectCurrent_Click(object sender, EventArgs e)
    {
      if (DBManager.getDBManager().order.order.Tables[SyncroC.DataBase.Order.DRYEND_LIST].Rows.Count > 0)
        this.ttb_QualityId.Value = DBManager.getDBManager().order.order.Tables[SyncroC.DataBase.Order.DRYEND_LIST].Rows[0][SyncroC.DataBase.Order.DEL_QUALITY_ID].ToString().Trim();
    }
  }
}
