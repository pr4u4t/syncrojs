using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.DataBase;
using SyncroC.Forms.Pages.Splicer;

namespace SyncroC.Forms.PopUp
{
  public partial class Frm_RollData : Frm_PopUp
  {
    int splicerNumber;
    int rollQueue;

    public Frm_RollData()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Show the form as a modal dialog
    /// </summary>
    /// <param name="owner">Window that will own the modal dialog</param>
    /// <param name="value"></param>
    public FosberDialogResult ShowDialog(IWin32Window owner, int splicerNumber, int rollQueue, RollData rollData)
    {
      this.splicerNumber = splicerNumber;
      this.rollQueue = rollQueue;
      this.lbl_SplNumber.Text = splicerNumber.ToString();

      switch (rollQueue)
      {
        case 1:
          this.pct_Roll1.Image = this.pct_RollSelected.Image;
          break;
        case 2:
          this.pct_Roll2.Image = this.pct_RollSelected.Image;
          break;
        case 3:
          this.pct_Roll3.Image = this.pct_RollSelected.Image;
          break;
        case 4:
          this.pct_Roll4.Image = this.pct_RollSelected.Image;
          break;
        default:
          break;
      }

      this.UpdateRollData(rollData);
      //sulla PopUp delle Bobine, se l'identificativo è grosso non si vedeva bene (ABELLO_FI04-03-09)
      //this.ttb_RollID.Select();
      return new FosberDialogResult(this.ShowDialog(owner), this.data);
    }

    public void UpdateRollData(RollData rollData)
    {
      if (!this.chk_Modify.Checked)
      {
        this.ttb_RollID.Value = rollData.rollId;
        this.ttb_PaperType.Value = rollData.paperType;
        this.nud_CurrentDiameter.Value = rollData.diameter_UDdmm.ToString();
        this.nud_SuppliedDiameter.Value = rollData.suppliedDiameter_UDdmm.ToString();
        this.nud_CurrentLength.Value = rollData.length_UDmm.ToString();
        this.nud_SuppliedLength.Value = rollData.suppliedLength_UDmm.ToString();
        this.nud_CoreDiameter.Value = rollData.coreDiameter_UDdmm.ToString();
        this.nud_Width.Value = rollData.width_UDdmm.ToString();
        this.nud_WeightFactor.Value = rollData.weightFactor_UWg_m.ToString();
        this.nud_Thickness.Value = rollData.thickness_UDdmmm.ToString();
        this.ttb_Description.Value = rollData.description;
        this.ttb_DataToPrint.Value = rollData.dataToPrint;
      }
    }

    private void FillInTheData()
    {
      try
      {
        RollData rollData = new RollData();
        rollData.rollId = this.ttb_RollID.Value;
        rollData.paperType = this.ttb_PaperType.Value;
        rollData.diameter_UDdmm = this.intParse(this.nud_CurrentDiameter.Value);
        rollData.suppliedDiameter_UDdmm = this.intParse(this.nud_SuppliedDiameter.Value);
        rollData.length_UDmm = this.intParse(this.nud_CurrentLength.Value);
        rollData.suppliedLength_UDmm = this.intParse(this.nud_SuppliedLength.Value);
        rollData.coreDiameter_UDdmm = this.intParse(this.nud_CoreDiameter.Value);
        rollData.width_UDdmm = this.intParse(this.nud_Width.Value);
        rollData.weightFactor_UWg_m = this.intParse(this.nud_WeightFactor.Value);
        rollData.thickness_UDdmmm = this.intParse(this.nud_Thickness.Value);
        rollData.description = this.ttb_Description.Value;
        rollData.dataToPrint = this.ttb_DataToPrint.Value;
        this.data = new object[] { this.splicerNumber, this.rollQueue, rollData };
      }
      catch (Exception)
      {
        this.data = new object[] { 0, 0, new RollData() };
      }
    }

    private void chk_Modify_OnCheckedChanged(object sender, EventArgs e)
    {
      this.ttb_RollID.ReadOnly = !this.chk_Modify.Checked;
      this.ttb_PaperType.ReadOnly = !this.chk_Modify.Checked;
      this.nud_CurrentDiameter.ReadOnly = !this.chk_Modify.Checked;
      this.nud_SuppliedDiameter.ReadOnly = !this.chk_Modify.Checked;
      this.nud_CurrentLength.ReadOnly = !this.chk_Modify.Checked;
      this.nud_SuppliedLength.ReadOnly = !this.chk_Modify.Checked;
      this.nud_CoreDiameter.ReadOnly = !this.chk_Modify.Checked;
      this.nud_Width.ReadOnly = !this.chk_Modify.Checked;
      this.nud_WeightFactor.ReadOnly = !this.chk_Modify.Checked;
      this.nud_Thickness.ReadOnly = !this.chk_Modify.Checked;
      this.ttb_Description.ReadOnly = !this.chk_Modify.Checked;
      this.ttb_DataToPrint.ReadOnly = !this.chk_Modify.Checked;

      this.btn_OK_YN.Visible = !this.chk_Modify.Checked;
      this.btn_Abort_YN.Visible = this.chk_Modify.Checked;
      this.btn_Save_YN.Visible = this.chk_Modify.Checked;

      if (this.btn_OK_YN.Visible)
      {
        this.AcceptButton = this.btn_OK_YN;
        this.CancelButton = this.btn_OK_YN;
      }
      else
      {
        this.AcceptButton = this.btn_Save_YN;
        this.CancelButton = this.btn_Abort_YN;
      }
    }

    private void btn_OK_YN_Click(object sender, EventArgs e)
    {
      this.data = new object[] { 0, 0, new RollData() };
      this.Dispose();
    }

    private void btn_Abort_Click(object sender, EventArgs e)
    {
      this.data = new object[] { 0, 0, new RollData() };
      this.Dispose();
    }

    private void btn_Save_Click(object sender, EventArgs e)
    {
      this.FillInTheData();
      this.Dispose();
    }

    protected override Size getOriginalSize()
    {
      return this.Size;
    }

    public int SplicerNumber
    { get { return this.splicerNumber; } }

    public int RollQueue
    { get { return this.rollQueue; } }
  }
}