//agrassi_FI30-05-08
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Controls;
using SyncroC.Utility;
using SyncroC.Forms.Pages.Production.OrdersIns;
using SyncroC.Controls.Colors;


namespace SyncroC.Forms.PopUp
{
  public partial class Frm_ScorersOptions : Frm_PopUp
  {
    public delegate void SetPositionings(TitledList t, int indexToSelect, bool Clean);
    public delegate void SetScorers(TitledList t, string SelectedPositType, int indexToSelect, bool Clean);
    public delegate void SetList(TitledList titledList, Type type, bool[] includeMask, int indexToSelect, bool Clean);
    private const int defOffset = 0;
    private const ScorerShaft defShaft = ScorerShaft.Auto;

    private DataGridViewColumn clm_order_measures;
    private DataGridViewImageColumn clm_order_optionPositioning, clm_order_optionType, clm_order_optionGap, clm_order_optionShaft;
    private Frm_QuickInsDryOrder.State state, nextState;
    private ArrayList measures;
    ScoreMeasure scoreMeasures;
    private char defScoreType;
    private PositType defPositType;
    private int defScoreGap;
    private int boxWidth;
    private SetPositionings setPositionings;
    private SetScorers setScorers;
    private SetList setList;
    private string scorerString;
    private int oldGridIndex;

    public Frm_ScorersOptions()
    {
      InitializeComponent();
      this.oldGridIndex = -1;
      clm_order_measures = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_order_measures.Width = 48;
      clm_order_measures.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      clm_order_optionPositioning = new DataGridViewImageColumn();
      clm_order_optionPositioning.Width = 51;
      clm_order_optionPositioning.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      clm_order_optionPositioning.ImageLayout = DataGridViewImageCellLayout.Stretch;
      clm_order_optionPositioning.DefaultCellStyle.NullValue = null;
      clm_order_optionType = new DataGridViewImageColumn();
      clm_order_optionType.Width = 51;
      clm_order_optionType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      clm_order_optionType.ImageLayout = DataGridViewImageCellLayout.Stretch;
      clm_order_optionType.DefaultCellStyle.NullValue = null;
      clm_order_optionGap = new DataGridViewImageColumn();
      clm_order_optionGap.Width = 51;
      clm_order_optionGap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      clm_order_optionGap.ImageLayout = DataGridViewImageCellLayout.Stretch;
      clm_order_optionGap.DefaultCellStyle.NullValue = null;
      clm_order_optionShaft = new DataGridViewImageColumn();
      clm_order_optionShaft.Width = 51;
      clm_order_optionShaft.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      clm_order_optionShaft.ImageLayout = DataGridViewImageCellLayout.Stretch;
      clm_order_optionShaft.DefaultCellStyle.NullValue = null;
      grv_ScorerOptions.Columns.Add(clm_order_measures);
      grv_ScorerOptions.Columns.Add(clm_order_optionPositioning);
      grv_ScorerOptions.Columns.Add(clm_order_optionType);
      grv_ScorerOptions.Columns.Add(clm_order_optionGap);
      grv_ScorerOptions.Columns.Add(clm_order_optionShaft);
      this.state = this.nextState = Frm_QuickInsDryOrder.State.Read;
      this.scoreMeasures = new ScoreMeasure();
      //this.grv_ScorerOptions.BackColor = ColConv.Convert(SyncroColor.LightGreen);
    }

    /// <summary>
    /// Show the form as a modal dialog
    /// </summary>
    /// <param name="owner">Window that will own the modal dialog</param>
    /// <param name="order">Index of the order (0-2)</param>
    /// <param name="scorerString">String that contains the scorers measures and options</param>
    public FosberDialogResult ShowDialog(IWin32Window owner, string orderIndex, string scoreString, Frm_QuickInsDryOrder.State state,
                                         Frm_QuickInsDryOrder.State nextState, string defScoreType, string defPositType, string defScoreGap,
                                         string boxWidthS, SetScorers setScorers, SetPositionings setPositionings, SetList setList)
    {
      bool readState = (state == Frm_QuickInsDryOrder.State.Read) || (nextState == Frm_QuickInsDryOrder.State.Read);
      this.btn_Abort_YN.Visible = (!readState);
      this.btn_OK.Location = (readState ? new Point(133, 281) : new Point(51, 281));
      char defaultScoreType = ' ';
      if (defScoreType.Length > 0) defaultScoreType = defScoreType[0];
      PositType defaultPositType = PositType.NoPositType;
      if (defPositType.Length > 0) defaultPositType = (PositType)Enum.Parse(typeof(PositType), defPositType);
      int defaultScoreGap = this.intParse(defScoreGap);
      int boxWidth = this.intParse(boxWidthS);
      this.ShowOptions(orderIndex, scoreString, state, nextState, defaultScoreType, defaultPositType, defaultScoreGap,
                                      boxWidth, setScorers, setPositionings, setList);
      return new FosberDialogResult(this.ShowDialog(owner), this.data);
    }

    private void btn_OK_YN_Click(object sender, EventArgs e)
    {
      this.data = new object[] { this.GetScoreString() };
      this.Dispose();
    }

    private void btn_Abort_Click(object sender, EventArgs e)
    {
      this.data = null;
      this.Dispose();
    }

    protected override Size getOriginalSize()
    {
      return this.Size;
    }

    public void ShowOptions(string orderIndex, string scoreString, Frm_QuickInsDryOrder.State state,
                            Frm_QuickInsDryOrder.State nextState, char defScoreType,
                            PositType defPositType, int defScoreGap, int boxWidth, SetScorers setScorers,
                            SetPositionings setPositionings, SetList setList)
    {
      this.ReadOnly(false);
      this.lbl_OrderIndex.Text = orderIndex;
      this.state = state;
      this.nextState = nextState;
      this.defScoreType = defScoreType;
      this.defPositType = defPositType;
      this.defScoreGap = defScoreGap;
      this.boxWidth = boxWidth;
      this.setScorers = setScorers;
      this.setPositionings = setPositionings;
      this.scorerString = scoreString;
      this.setList = setList;
      this.setPositionings(this.cmb_Positioning, -1, true);
      this.setList(this.cmb_Shaft, typeof(ScorerShaft), new bool[] { true, true, true, true }, -1, true);
      this.measures = ScoreMeasure.ParseMeasures(scoreString, defPositType, defScoreType, defScoreGap, defOffset, defShaft);
      this.SetDefault();
      this.grv_ScorerOptions.Rows.Clear();
      for (int j = 0; j < this.measures.Count; j++)
      {
        object[] rowOv = this.ScoreOverrides((int)SyncroConverter.Convert(((ScoreMeasure)measures[j]).measure, UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre),
                                              j, defPositType, defScoreType, defScoreGap, defOffset, defShaft);
        if (rowOv != null)
        {
          try
          {
            this.grv_ScorerOptions.Rows.Add(rowOv);
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "rowOv.Length=" + rowOv.Length + ", rowOv=" + rowOv.ToString(), ex);
          }
        }
      }
      if (grv_ScorerOptions.Rows.Count > 0)
        grv_ScorerOptions.Rows[0].Selected = true;
      if (this.state == Frm_QuickInsDryOrder.State.Read)
        this.ReadOnly(true);
    }

    public string GetScoreString()
    {
      int index = this.grv_ScorerOptions.SelectedRows[0].Index;
      this.FillInMeasure(index, (this.nud_Measure.Value == "" ? 0 : this.intParse(this.nud_Measure.Value)),
                      (this.cmb_Positioning.SelectedItem.Length > 0) ? (PositType)Enum.Parse(typeof(PositType), this.cmb_Positioning.SelectedItemData, true) : PositType.NoPositType,
                      (this.cmb_Type.SelectedItem.Length > 0) ? this.cmb_Type.SelectedItem[0] : ' ',
                      (this.nud_ScoreGap.Value == "" ? 0 : this.intParse(this.nud_ScoreGap.Value)),
                      (this.nud_Offset.Value == "" ? 0 : this.intParse(this.nud_Offset.Value)),
                      (ScorerShaft)Enum.Parse(typeof(ScorerShaft), this.cmb_Shaft.SelectedItemData, true));
      ScoreMeasure defValues = new ScoreMeasure(0, this.defPositType, this.defScoreType, this.defScoreGap, defOffset, defShaft);
      string newScorerString = ScoreMeasure.ComposeScoreMeasures(this.measures, defValues).Replace('\n', ' ');
      return newScorerString;
    }

    private void FillInMeasure(int ind, int meas, PositType posType, char scoreType, int gap, int ttwOffset, ScorerShaft sh)
    {
      if (ind < this.measures.Count)
      {
        ((ScoreMeasure)this.measures[ind]).measure = meas;
        ((ScoreMeasure)this.measures[ind]).positType = posType;
        ((ScoreMeasure)this.measures[ind]).scoreTypeLetter = scoreType;
        ((ScoreMeasure)this.measures[ind]).gap = gap;
        ((ScoreMeasure)this.measures[ind]).offset = ttwOffset;
        ((ScoreMeasure)this.measures[ind]).scorerShaft = sh;
      }
    }

    protected override void Return()
    {
      if ((this.state != Frm_QuickInsDryOrder.State.Modify) && (this.state != Frm_QuickInsDryOrder.State.Insert))
        return;
      if (this.cmb_Shaft.Focused)
      {
        int index = this.grv_ScorerOptions.SelectedRows[0].Index;
        this.FillInMeasure(index, (this.nud_Measure.Value == "" ? 0 : this.intParse(this.nud_Measure.Value)),
                        (this.cmb_Positioning.SelectedItem.Length > 0) ? (PositType)Enum.Parse(typeof(PositType), this.cmb_Positioning.SelectedItemData, true) : PositType.NoPositType,
                        (this.cmb_Type.SelectedItem.Length > 0) ? this.cmb_Type.SelectedItem[0] : ' ',
                        (this.nud_ScoreGap.Value == "" ? 0 : this.intParse(this.nud_ScoreGap.Value)),
                        (this.nud_Offset.Value == "" ? 0 : this.intParse(this.nud_Offset.Value)),
                        (ScorerShaft)Enum.Parse(typeof(ScorerShaft), this.cmb_Shaft.SelectedItemData, true));
        this.grv_ScorerOptions.Rows[index].SetValues(this.ScoreOverrides((int)SyncroConverter.Convert(((ScoreMeasure)measures[index]).measure, UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre),
                                                  index, this.defPositType, this.defScoreType, this.defScoreGap, defOffset, defShaft));
        if (index == this.grv_ScorerOptions.Rows.Count - 1)
        {
          this.grv_ScorerOptions.Rows.Add();
          this.measures.Add(new ScoreMeasure());
        }
        this.grv_ScorerOptions.Rows[index + 1].Selected = true;
        this.nud_Measure.Focus();
        return;
      }
      else if (this.nud_Measure.Focused)
      {
        if (this.nud_Measure.Value.Equals(String.Empty) || this.intParse(this.nud_Measure.Value) <= 0)
        {
          int index = this.grv_ScorerOptions.SelectedRows[0].Index;
          ((ScoreMeasure)measures[index]).measure = 0;
          ((ScoreMeasure)measures[index]).positType = PositType.NoPositType;
          ((ScoreMeasure)measures[index]).scoreTypeLetter = ' ';
          ((ScoreMeasure)measures[index]).gap = 0;
          ((ScoreMeasure)measures[index]).offset = 0;
          ((ScoreMeasure)measures[index]).scorerShaft = ScorerShaft.Auto;
          this.grv_ScorerOptions.Rows[index].SetValues(this.ScoreOverrides((int)SyncroConverter.Convert(((ScoreMeasure)measures[index]).measure, UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre),
                                        index, this.defPositType, this.defScoreType, this.defScoreGap, defOffset, defShaft));
          for (int ind = this.grv_ScorerOptions.Rows.Count - 1; ind >= index + 1; ind--)
          {
            if (grv_ScorerOptions.Rows.Count > ind)
              this.grv_ScorerOptions.Rows.RemoveAt(ind);
            if (this.measures.Count > ind)
              this.measures.RemoveAt(ind);
          }
          this.btn_OK.Focus();
        }
        else
          base.Return();
        return;
      }
      else
        base.Return();
    }

    private void Frm_ScorersOptions_Activated(object sender, EventArgs e)
    {
      if (this.state == Frm_QuickInsDryOrder.State.Read)
        this.grv_ScorerOptions.Focus();
      else
        this.nud_Measure.Focus();
    }

    private object[] ScoreOverrides(int measure, int ind, PositType defPosType, char defScorer, int defGap, int defOffset, ScorerShaft defShaft)
    {
      int MAX_NUM_OPTIONS = 4;
      object[] overrides = new object[MAX_NUM_OPTIONS+1];
      int optionIndex=0;
      overrides[optionIndex++]=measure;
      if (measure > 0)
      {
        if (!defScorer.Equals(((ScoreMeasure)measures[ind]).scoreTypeLetter))
          overrides[optionIndex++] = global::SyncroC.Properties.Resources.OptionType;
        if (!defPosType.Equals(((ScoreMeasure)measures[ind]).positType))
          overrides[optionIndex++] = global::SyncroC.Properties.Resources.OptionPositioning;
        if (!defGap.Equals(((ScoreMeasure)measures[ind]).gap))
          overrides[optionIndex++] = global::SyncroC.Properties.Resources.OptionGap;
        if (!defShaft.Equals(((ScoreMeasure)measures[ind]).scorerShaft))
          overrides[optionIndex++] = global::SyncroC.Properties.Resources.OptionShaft;
        //if (!defOffset.Equals(((ScoreMeasure)measures[ind]).offset))
        //  overrides[optionIndex++] = global::SyncroC.Properties.Resources.OptionOffset;
      }
      return overrides;
    }

    private void grv_ScorerOptions_SelectionChanged(object sender, EventArgs e)
    {
      int index = ((GridView)sender).SelectedRows[0].Index;
      if ((this.state==Frm_QuickInsDryOrder.State.Modify) || (this.state==Frm_QuickInsDryOrder.State.Modify) ||
          (this.state==Frm_QuickInsDryOrder.State.Insert) || (this.state==Frm_QuickInsDryOrder.State.Insert))
      {
        if ((this.oldGridIndex>=0) && (measures!=null) && (this.oldGridIndex<measures.Count))
          this.FillInMeasure(this.oldGridIndex, (this.nud_Measure.Value == "" ? 0 : this.intParse(this.nud_Measure.Value)),
                          (this.cmb_Positioning.SelectedItem.Length > 0) ? (PositType)Enum.Parse(typeof(PositType), this.cmb_Positioning.SelectedItemData, true) : PositType.NoPositType,
                          (this.cmb_Type.SelectedItem.Length > 0) ? this.cmb_Type.SelectedItem[0] : ' ',
                          (this.nud_ScoreGap.Value == "" ? 0 : this.intParse(this.nud_ScoreGap.Value)),
                          (this.nud_Offset.Value == "" ? 0 : this.intParse(this.nud_Offset.Value)),
                          (ScorerShaft)Enum.Parse(typeof(ScorerShaft), this.cmb_Shaft.SelectedItemData, true));
      }
      if (measures != null)
        this.nud_Measure.Value = ((ScoreMeasure)measures[index]).measure.ToString();
      if (((this.state == Frm_QuickInsDryOrder.State.Modify) && (index > ((GridView)sender).Rows.Count - 2) && (this.nextState != Frm_QuickInsDryOrder.State.Read)) ||
         (this.state == Frm_QuickInsDryOrder.State.Insert) || (this.nextState == Frm_QuickInsDryOrder.State.Insert))
      {
        this.SetDefault();
        int boxWidthRemaining = this.boxWidth;
        for (int i = 0; i < index; i++)
          boxWidthRemaining -= ((ScoreMeasure)measures[i]).measure;
        if (boxWidthRemaining < 0)
        {
          this.nud_Measure.SetError("§SYCC_33");
          boxWidthRemaining = 0;
        }
        else
          this.nud_Measure.SetError("");
        this.nud_Measure.Value = boxWidthRemaining.ToString();
      }
      else if (measures != null)
      {
        this.cmb_Type.SelectedItem = ((ScoreMeasure)measures[index]).scoreTypeLetter.ToString();
        if (((ScoreMeasure)measures[index]).positType == PositType.NoPositType)
          this.cmb_Positioning.SelectedItem = "";
        else
          this.cmb_Positioning.SelectedItemData = ((ScoreMeasure)measures[index]).positType.ToString();
        this.nud_ScoreGap.Value = ((ScoreMeasure)measures[index]).gap.ToString();
        this.cmb_Shaft.SelectedItemData = ((ScoreMeasure)measures[index]).scorerShaft.ToString();
        this.nud_Offset.Value = ((ScoreMeasure)measures[index]).offset.ToString();
      }
      this.oldGridIndex = index;
    }

    private void SetDefault()
    {
      this.cmb_Positioning.SelectedItemData = Enum.Format(typeof(PositType), this.defPositType, "g");
      this.cmb_Type.SelectedItem = this.defScoreType.ToString();
      this.nud_ScoreGap.Value = this.defScoreGap.ToString();
      this.cmb_Shaft.SelectedItemData = Enum.Format(typeof(ScorerShaft), defShaft, "g");
      this.nud_Offset.Value = defOffset.ToString();
    }


    private void cmb_Positioning_SelectedItemChanged(object sender, EventArgs e)
    {
      string scorePositType = ((TitledList)sender).SelectedItemData;
      this.setScorers(cmb_Type, scorePositType, -1, true);
      this.nud_Offset.Enabled = scorePositType.Equals(PositType.TeletwinP.ToString());
      this.cmb_Type.Enabled = !scorePositType.Equals(PositType.TearTape.ToString());
      this.nud_ScoreGap.Enabled = !scorePositType.Equals(PositType.TearTape.ToString());
    }

    private void ReadOnly(bool val)
    {
      this.nud_Measure.ReadOnly = this.cmb_Positioning.ReadOnly =
        this.cmb_Type.ReadOnly = this.nud_ScoreGap.ReadOnly =
        this.nud_Offset.ReadOnly = this.cmb_Shaft.ReadOnly = val;
    }

    protected override void Escape()
    {
      this.btn_Abort_Click(null, null);
    }
  }
}