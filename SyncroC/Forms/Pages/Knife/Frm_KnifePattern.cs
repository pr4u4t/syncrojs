using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.DataBase;
using SyncroC.DataBase.EventArgs;
using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.Forms.PopUp;
using SyncroC.Controls;

//ABello_FI Aggiunta gestione CutToPattern
namespace SyncroC.Forms.Pages.Knife
{
  public partial class Frm_KnifePattern : Frm_KnifeGroup
  {
    private enum State { Insert, Modify, Read };
    private State state = State.Read;
    //private CutToPattern cutToPattern;
    private DataGridViewColumn clm_PatternCode;

    public Frm_KnifePattern()
    {
      InitializeComponent();

      //this.cutToPattern = DBManager.getDBManager().cutToPattern;

      DataView dataView = new DataView(DBManager.getDBManager().cutToPattern.getTable());
      dataView.Sort = CutToPattern.PATTERN_ID + " Asc";
      this.gdv_Pattern.DataSource = dataView;

      //gdv_Pattern
      this.clm_PatternCode = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_PatternCode.Name = CutToPattern.PATTERN_ID;
      this.clm_PatternCode.HeaderText = "Pattern Code";
      this.clm_PatternCode.DataPropertyName = CutToPattern.PATTERN_ID;
      this.clm_PatternCode.Width = 250;

      this.gdv_Pattern.Columns.Add(clm_PatternCode);

      DBManager.getDBManager().cutToPattern.OnDBCutToPatternChange += new CutToPattern.DBChangedHandler(cutToPattern_RowChanged);

      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
      {
        this.lbl_CtrlF1_YY.Text = "";
        this.lbl_CtrlF2_YY.Text = "";
        this.lbl_CtrlF3_YY.Text = "";
        this.lbl_CtrlF4_YY.Text = "";
        this.lbl_CtrlF5_YY.Text = "";
      }

      this.CleanCutToPatternDetails();
    }

    public override void refreshMySQLData()
    {
      try
      {
        DataRow row = null;
        int FirstDisplayedScrollingRowIndex = this.gdv_Pattern.FirstDisplayedScrollingRowIndex;

        if (this.gdv_Pattern != null)
          if (this.gdv_Pattern.SelectedRows != null)
            if (this.gdv_Pattern.SelectedRows.Count != 0)
              row = ((DataRowView)this.gdv_Pattern.SelectedRows[0].DataBoundItem).Row;

        this.gdv_Pattern.SuspendLayout();

        this.gdv_Pattern.DataSource = DBManager.getDBManager().cutToPattern.getTable();

        for (int i = 0; i < this.gdv_Pattern.Rows.Count; i++)
        {
          if (row != null)
          {
            string a = ((DataRowView)this.gdv_Pattern.Rows[i].DataBoundItem).Row[CutToPattern.PATTERN_ID].ToString().Trim();
            string b = row[CutToPattern.PATTERN_ID].ToString().Trim();
            if (a == b)
            {
              this.gdv_Pattern.Rows[i].Selected = true;
            }
            else
              this.gdv_Pattern.Rows[i].Selected = false;
          }
        }

        if ((row == null) && (this.gdv_Pattern.Rows.Count > 0))
          this.gdv_Pattern.Rows[0].Selected = true;

        if ((FirstDisplayedScrollingRowIndex >= 0) && (FirstDisplayedScrollingRowIndex <= DBManager.getDBManager().cutToPattern.getCount()))
          this.gdv_Pattern.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex;

        this.gdv_Pattern.ResumeLayout();
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    public void cutToPattern_RowChanged(object sender, DBChangedEventArgs args)
    {
      if (this.state.Equals(State.Read))
      {
        this.gdv_Pattern_SelectionChanged(null, null);
      }
    }

    private void gdv_Pattern_SelectionChanged(object sender, EventArgs e)
    {
      if (this.gdv_Pattern.Rows.Count > 0 && this.gdv_Pattern.SelectedRows.Count == 1)
      {
        DataRow row = ((DataRowView)this.gdv_Pattern.SelectedRows[0].DataBoundItem).Row;
        if (row != null && row.RowState != DataRowState.Detached)
        {
          try
          {
            this.ttb_PatternID.Value = row[CutToPattern.PATTERN_ID].ToString();
            this.ttb_Text.Value = row[CutToPattern.PATTERN_TEXT].ToString();
            this.tnud_EyeContrast.Value = row[CutToPattern.PATTERN_EYE_CONTRAST].ToString();
            this.tnud_CutOffset.Value = row[CutToPattern.PATTERN_CUT_OFFSET_UDdmm].ToString();
            this.chk_firstMarkId.Checked = row[CutToPattern.PATTERN_FIRST_MARK_ID].ToString() == "0" ? false : true;
            this.tnud_windowBefore.Value = row[CutToPattern.PATTERN_WINDOW_BEFORE_PATTERN_UDdmm].ToString();
            this.tnud_windowAfter.Value = row[CutToPattern.PATTERN_WINDOW_AFTER_PATTERN_UDdmm].ToString();

            this.tnud_MarkDistance1.Value = row[CutToPattern.PATTERN_MARK_DISTANCE_UDdmm1].ToString();
            this.tnud_MarkDistance2.Value = row[CutToPattern.PATTERN_MARK_DISTANCE_UDdmm2].ToString();
            this.tnud_MarkDistance3.Value = row[CutToPattern.PATTERN_MARK_DISTANCE_UDdmm3].ToString();
            this.tnud_MarkDistance4.Value = row[CutToPattern.PATTERN_MARK_DISTANCE_UDdmm4].ToString();
            this.tnud_MarkDistance5.Value = row[CutToPattern.PATTERN_MARK_DISTANCE_UDdmm5].ToString();
            this.tnud_MarkDistance6.Value = row[CutToPattern.PATTERN_MARK_DISTANCE_UDdmm6].ToString();
            this.tnud_MarkDistance7.Value = row[CutToPattern.PATTERN_MARK_DISTANCE_UDdmm7].ToString();
            this.tnud_MarkDistance8.Value = row[CutToPattern.PATTERN_MARK_DISTANCE_UDdmm8].ToString();
            this.tnud_MarkDistance9.Value = row[CutToPattern.PATTERN_MARK_DISTANCE_UDdmm9].ToString();
            this.tnud_MarkDistance10.Value = row[CutToPattern.PATTERN_MARK_DISTANCE_UDdmm10].ToString();
            //RFAZZI_FI12/08/2008_1.Added lateral position of pattern
            this.tnud_PatternLateralPosition.Value = row[CutToPattern.PATTERN_LATERAL_POSITION_UDdmm].ToString();
          }
          catch (RowNotInTableException ex)
          {
            Logger.LogError(this, "gdv_Pattern_SelectionChanged - the rowState is not Detached (it is \"" + row.RowState.ToString() + "\" but the RowNotInTableException has been thrown anyway....", ex);
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Pattern_SelectionChanged - ", ex);
          }
        }
        else
          this.CleanCutToPatternDetails();
      }
      else
        this.CleanCutToPatternDetails();
    }

    private void CleanCutToPatternDetails()
    {
      this.ttb_PatternID.Value = "";
      this.ttb_Text.Value = "";
      this.tnud_EyeContrast.Value = "1";
      this.tnud_CutOffset.Value = "0";
      this.chk_firstMarkId.Checked = false;
      this.tnud_windowBefore.Value = "0";
      this.tnud_windowAfter.Value = "0";
      this.tnud_MarkDistance1.Value = "0";
      this.tnud_MarkDistance2.Value = "0";
      this.tnud_MarkDistance3.Value = "0";
      this.tnud_MarkDistance4.Value = "0";
      this.tnud_MarkDistance5.Value = "0";
      this.tnud_MarkDistance6.Value = "0";
      this.tnud_MarkDistance7.Value = "0";
      this.tnud_MarkDistance8.Value = "0";
      this.tnud_MarkDistance9.Value = "0";
      this.tnud_MarkDistance10.Value = "0";
      //RFAZZI_FI12/08/2008_1.Added lateral position of pattern
      this.tnud_PatternLateralPosition.Value = "0";

    }

    private void SetPageStatus(State newState)
    {
      if (!this.state.Equals(newState))
      {
        this.state = newState;
        switch (newState)
        {
          case State.Insert:
            this.CleanCutToPatternDetails();
            foreach (DataGridViewRow row in this.gdv_Pattern.Rows)
              row.Selected = false;
            this.ttb_PatternID.TabStop = true;
            this.ttb_PatternID.ReadOnly = false;
            this.readOnly(false);
            this.lbl_CtrlF1.BackColor = this.lbl_CtrlF1_YY.BackColor = this.lbl_Status.BackColor;
            this.gdv_Pattern.Enabled = false;
            this.ttb_PatternID.Focus();
            break;

          case State.Modify:
            this.readOnly(false);
            this.lbl_CtrlF5.BackColor = this.lbl_CtrlF5_YY.BackColor = this.lbl_Status.BackColor;
            //agrassi_FI15-01-09_1
            this.ttb_PatternID.TabStop = false;
            this.ttb_PatternID.ReadOnly = true;
            this.gdv_Pattern.Enabled = false;
            this.ttb_Text.Focus();
            break;

          case State.Read:
            this.ttb_PatternID.TabStop = false;
            this.ttb_PatternID.ReadOnly = true;
            this.readOnly(true);
            this.lbl_CtrlF5.BackColor = this.lbl_CtrlF5_YY.BackColor = this.lbl_CtrlF1.BackColor = this.lbl_CtrlF1_YY.BackColor = this.lbl_Title_YN.BackColor;
            this.gdv_Pattern.Enabled = true;
            if (gdv_Pattern.SelectedRows.Count == 0 && this.gdv_Pattern.Rows.Count > 0)
              this.gdv_Pattern.Rows[0].Selected = true;
            else
              this.gdv_Pattern_SelectionChanged(null, null);
            this.gdv_Pattern.Focus();
            this.CleanErrorProvider();
            break;
        }
      }
    }

    private void CleanErrorProvider()
    {
      this.ttb_PatternID.SetError(string.Empty);
    }

    protected override void F5()
    {
    }


    /// <summary>
    /// CtrlF1 key behaviour: New Quality
    /// </summary>
    protected override void CtrlF1()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      this.SetPageStatus(State.Insert);
    }

    /// <summary>
    /// CtrlF2 key behaviour: Delete an existing record
    /// </summary>
    protected override void CtrlF2()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      if (gdv_Pattern.Focused)
      {
        DataRowCollection rows = DBManager.getDBManager().cutToPattern.getTable().Rows;
        if (rows.Count > 0)
        {
          Frm_Confirm conf = new Frm_Confirm();
          string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_36");
          if (gdv_Pattern.SelectedRows.Count != 0
            && conf.ShowDialog(this, text).Equals(DialogResult.OK))
          {
            CommunicationManager cm = CommunicationManager.GetCommunicationManager();
            foreach (DataGridViewRow row in gdv_Pattern.SelectedRows)
              cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_CUT_TO_PATTERN + "." + Command.DB_CMD_DEL + "(" + ((DataRowView)row.DataBoundItem).Row[CutToPattern.PATTERN_ID] + ")");
          }
        }
        this.SetPageStatus(State.Read);
      }
    }

    /// <summary>
    /// CtrlF3 key behaviour: Delete all DB quality
    /// </summary>
    protected override void CtrlF3()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      Frm_Confirm conf = new Frm_Confirm();
      string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_37");
      if (gdv_Pattern.Rows.Count > 0 && state.Equals(State.Read) &&
        conf.ShowDialog(this, text).Equals(DialogResult.OK))
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_CUT_TO_PATTERN + "." + Command.DB_CMD_CLR + "()");
      }
      this.SetPageStatus(State.Read);
    }

    /// <summary>
    /// CtrlF4 key behaviour: Save
    /// </summary>
    protected override void CtrlF4()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      if (this.ttb_PatternID.Value.Trim() == "")
      {
        PageManager.getPageManager()._SetMessage("§SYCC_10", true);
        return;
      }
      else
      {
        this.saveData();
        this.Escape();
      }
    }

    private void saveData()
    {
      string toSend = Command.DB_CUT_TO_PATTERN + ".";
      if (state == State.Modify)
      {
        toSend += Command.DB_CMD_MOD;
        PopUp.Frm_Confirm conf = new Frm_Confirm();
        string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_65");
        DialogResult res = conf.ShowDialog(this, text);

        //PMASSARO_FI18/01/10 - Non modifichiamo più la pressione di cordonatura impostata nell'ordine se non espressamente richiesto
        /*
        if ((res == DialogResult.Yes) || (res == DialogResult.OK))
          toSend += "WithSetup";
        */
        if ((res == DialogResult.Yes) || (res == DialogResult.OK))
          toSend += "WithSetup_NoPressure";
      }
      else
        toSend += Command.DB_CMD_ADD;
      toSend += "(";
      if ((state == State.Modify) || (state == State.Insert))
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_DB_MANAGER,
          toSend +
            this.ttb_PatternID.Value + Command.PARAMETER_SEPARATOR +
            this.ttb_Text.Value + Command.PARAMETER_SEPARATOR +
            this.tnud_MarkDistance1.Value + Command.PARAMETER_SEPARATOR +
            this.tnud_MarkDistance2.Value + Command.PARAMETER_SEPARATOR +
            this.tnud_MarkDistance3.Value + Command.PARAMETER_SEPARATOR +
            this.tnud_MarkDistance4.Value + Command.PARAMETER_SEPARATOR +
            this.tnud_MarkDistance5.Value + Command.PARAMETER_SEPARATOR +
            this.tnud_MarkDistance6.Value + Command.PARAMETER_SEPARATOR +
            this.tnud_MarkDistance7.Value + Command.PARAMETER_SEPARATOR +
            this.tnud_MarkDistance8.Value + Command.PARAMETER_SEPARATOR +
            this.tnud_MarkDistance9.Value + Command.PARAMETER_SEPARATOR +
            this.tnud_MarkDistance10.Value + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            this.tnud_EyeContrast.Value + Command.PARAMETER_SEPARATOR +
            this.tnud_CutOffset.Value + Command.PARAMETER_SEPARATOR +
            (this.chk_firstMarkId.Checked ? "1" : "0") + Command.PARAMETER_SEPARATOR +
            this.tnud_windowBefore.Value + Command.PARAMETER_SEPARATOR +
            this.tnud_windowAfter.Value + Command.PARAMETER_SEPARATOR +
          //RFAZZI_FI12/08/2008_1.Added lateral position of pattern
            this.tnud_PatternLateralPosition.Value + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" +
          ")");
      }
    }

    /// <summary>
    /// CtrlF5 key behaviour: New Quality
    /// </summary>
    protected override void CtrlF5()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      if (this.gdv_Pattern.Rows.Count <= 0)
        return;

      if (this.state == State.Insert)
        return;

      this.SetPageStatus(State.Modify);
    }

    /// <summary>
    /// Escape key behaviour: Abort the operation
    /// </summary>
    protected override void Escape()
    {
      this.SetPageStatus(State.Read);
    }

    private void readOnly(bool isReadOnly)
    {
      //TAB
      //agrassi_FI15-01-09_1
      //this.ttb_PatternID.TabStop = this.ttb_Text.TabStop =
      this.ttb_Text.TabStop =
      this.tnud_EyeContrast.TabStop = this.tnud_CutOffset.TabStop =
      this.chk_firstMarkId.TabStop = this.tnud_windowBefore.TabStop =
      this.tnud_windowAfter.TabStop = this.tnud_MarkDistance1.TabStop =
      this.tnud_MarkDistance2.TabStop = this.tnud_MarkDistance3.TabStop =
      this.tnud_MarkDistance4.TabStop = this.tnud_MarkDistance5.TabStop =
      this.tnud_MarkDistance6.TabStop = this.tnud_MarkDistance7.TabStop =
      this.tnud_MarkDistance8.TabStop = this.tnud_MarkDistance9.TabStop =
      this.tnud_MarkDistance10.TabStop = !isReadOnly;
      //RFAZZI_FI12/08/2008_1.Added lateral position of pattern
      this.tnud_PatternLateralPosition.TabStop = !isReadOnly;


      //READONLY
      //agrassi_FI15-01-09_1
      //this.ttb_PatternID.ReadOnly = this.ttb_Text.ReadOnly =
      this.ttb_Text.ReadOnly =
      this.tnud_EyeContrast.ReadOnly = this.tnud_CutOffset.ReadOnly =
      this.chk_firstMarkId.ReadOnly = this.tnud_windowBefore.ReadOnly =
      this.tnud_windowAfter.ReadOnly = this.tnud_MarkDistance1.ReadOnly =
      this.tnud_MarkDistance2.ReadOnly = this.tnud_MarkDistance3.ReadOnly =
      this.tnud_MarkDistance4.ReadOnly = this.tnud_MarkDistance5.ReadOnly =
      this.tnud_MarkDistance6.ReadOnly = this.tnud_MarkDistance7.ReadOnly =
      this.tnud_MarkDistance8.ReadOnly = this.tnud_MarkDistance9.ReadOnly =
      this.tnud_MarkDistance10.ReadOnly = isReadOnly;
      //RFAZZI_FI12/08/2008_1.Added lateral position of pattern
      this.tnud_PatternLateralPosition.ReadOnly = isReadOnly;

    }

    private void gdv_Pattern_Leave(object sender, EventArgs e)
    {
      if (this.state.Equals(State.Read))
        this.gdv_Pattern.Focus();
    }

    private void Frm_KnifePattern_Activated(object sender, EventArgs e)
    {
      if (this.state.Equals(State.Read))
        this.gdv_Pattern.Focus();
    }
  }
}
