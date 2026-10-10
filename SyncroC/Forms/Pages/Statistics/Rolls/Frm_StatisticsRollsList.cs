using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.DataBase.EventArgs;
using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.DataBase;
using SyncroC.Forms.PopUp;

namespace SyncroC.Forms.Pages.Statistics.Rolls
{
	public partial class Frm_StatisticsRollsList : SyncroC.Forms.Pages.Statistics.Rolls.Frm_StatisticsRollsSubgroup
	{
		//colonne che avrebbe dovuto fare il designer.
		private DataGridViewColumn clm_paper_type;
    private DataGridViewColumn clm_roll_id;
		private DataGridViewColumn clm_width;
		private DataGridViewColumn clm_weight_factor;
		private DataGridViewColumn clm_time_removed;
		private DataGridViewColumn clm_remaining;
		private DataGridViewColumn clm_splNmber;

    private string filter = "";

    public Frm_StatisticsRollsList()
		{
			InitializeComponent();

      this.gdv_PastRolls.DataSource = DBManager.getDBManager().pastRolls.getTable();

			clm_paper_type = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_paper_type.HeaderText = "Paper Type";
			clm_paper_type.DataPropertyName = PastRoll.PAPER_TYPE;
			clm_paper_type.Name = PastRoll.PAPER_TYPE;
			clm_paper_type.Width = 203;

      //ABELLO_FI04/06/2008_1
      clm_roll_id = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_roll_id.HeaderText = "Roll Id";
      clm_roll_id.DataPropertyName = PastRoll.ROLL_ID;
      clm_roll_id.Name = PastRoll.ROLL_ID;
      clm_roll_id.Width = 250;

			clm_weight_factor = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_weight_factor.HeaderText = "Weight Factor";
			clm_weight_factor.DataPropertyName = PastRoll.WEIGHT_FACTOR_UWg_m;
			clm_weight_factor.Name = PastRoll.WEIGHT_FACTOR_UWg_m;
			clm_weight_factor.Width = 160;

			clm_width = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_width.HeaderText = "Width";
			clm_width.DataPropertyName = PastRoll.WIDTH_UDdmm;
			clm_width.Name = PastRoll.WIDTH_UDdmm;
			clm_width.Width = 160;

			clm_remaining = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_remaining.HeaderText = "Remaining";
			clm_remaining.DataPropertyName = PastRoll.REMAINING_UDmm;
			clm_remaining.Name = PastRoll.REMAINING_UDmm;
			clm_remaining.Width = 160;

			clm_time_removed = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_time_removed.HeaderText = "Time Removed";
			clm_time_removed.DataPropertyName = PastRoll.TIME_REMOVED;
			clm_time_removed.Name = PastRoll.TIME_REMOVED;
			clm_time_removed.Width = 200;

			clm_splNmber = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_splNmber.HeaderText = "Splicer Number";
			clm_splNmber.DataPropertyName = PastRoll.SPLICER_NUMBER;
			clm_splNmber.Name = PastRoll.SPLICER_NUMBER;
			clm_splNmber.Width = 50;

			gdv_PastRolls.Columns.Add(clm_time_removed);
			gdv_PastRolls.Columns.Add(clm_splNmber);
			gdv_PastRolls.Columns.Add(clm_paper_type);
      gdv_PastRolls.Columns.Add(clm_roll_id);
			gdv_PastRolls.Columns.Add(clm_weight_factor);
			gdv_PastRolls.Columns.Add(clm_width);
			gdv_PastRolls.Columns.Add(clm_remaining);

			gdv_PastRolls.Sort(clm_time_removed, ListSortDirection.Descending);

      this.lbl_filter.Testo = (this.filter == "") ? "" : "!";

      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        this.lbl_filter.Visible = true;
        this.btn_ShowGraph.Visible = true;
        this.myListBox_Order.Visible = true;
      }
		}

    private string rollIdFilter = "";
    private string paperTypeFilter = "";
    private void lbl_filter_Click(object sender, EventArgs e)
    {
      try
      {
        Frm_FilterPastRolls askFilterPastOrder = new Frm_FilterPastRolls();
        FosberDialogResult ret = askFilterPastOrder.ShowDialog(this, this.rollIdFilter, this.paperTypeFilter);
        if (ret.Dialog == DialogResult.OK)
        {
          this.filter = "";

          this.rollIdFilter = ((string)ret.Data[0]).Trim();
          this.paperTypeFilter = ((string)ret.Data[1]).Trim();

          if (this.rollIdFilter != "")
            this.filter = String.Format("(rollid LIKE '{0}%')", this.rollIdFilter);

          if (this.paperTypeFilter != "")
          {
            if (this.filter != "")
              this.filter += String.Format("AND (papertype LIKE '{0}%')", this.paperTypeFilter);
            else
              this.filter = String.Format("(papertype LIKE '{0}%')", this.paperTypeFilter);
          }
        }

        this.gdv_PastRolls.SuspendLayout();
        this.gdv_PastRolls.DataSource = DBManager.getDBManager().pastRolls.getTable(this.filter);
        if (this.gdv_PastRolls.Rows.Count > 0)
          this.gdv_PastRolls.Rows[0].Selected = true;
        this.gdv_PastRolls.ResumeLayout();

        this.lbl_filter.Testo = (this.filter == "") ? "" : "!";
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    private void btn_ShowGraph_Click(object sender, EventArgs e)
    {
      if (gdv_PastRolls.SelectedRows.Count == 1)
      {
        DataRow dataRow = ((DataRowView)gdv_PastRolls.SelectedRows[0].DataBoundItem).Row;
        DateTime dateTime = DateTime.MinValue;
        if (dataRow != null)
        {
          try
          {
            dateTime = (DateTime)dataRow[PastRoll.TIME_ENTERED];
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "btn_ShowGraph_Click gdv_PastRolls - error converting value.", ex);
          }
        }
        else
        {
          Logger.LogError(this, "btn_ShowGraph_Click gdv_PastRolls - datarow was null");
        }

        SyncroC.Forms.Pages.Statistics.Frm_ProductionTrace.from_Universal = dateTime;
      }

      askForPage(typeof(SyncroC.Forms.Pages.Statistics.Frm_ProductionTrace));

    }

		/// <summary>
		/// Show / Hide debug info.
		/// </summary>
		protected override void ControlD()
		{
			base.ControlD();
			this.lbl_REMAINING_DIAMETER_UDdmm.Visible = !this.lbl_REMAINING_DIAMETER_UDdmm.Visible;
		}

    public override void refreshMySQLData()
    {
      try
      {
        DataRow row = null;
        int FirstDisplayedScrollingRowIndex = this.gdv_PastRolls.FirstDisplayedScrollingRowIndex;

        if (this.gdv_PastRolls != null)
          if (this.gdv_PastRolls.SelectedRows != null)
            if (this.gdv_PastRolls.SelectedRows.Count != 0)
              row = ((DataRowView)this.gdv_PastRolls.SelectedRows[0].DataBoundItem).Row;

        this.gdv_PastRolls.SuspendLayout();

        this.gdv_PastRolls.DataSource = DBManager.getDBManager().pastRolls.getTable(this.filter);

        for (int i = 0; i < this.gdv_PastRolls.Rows.Count; i++)
        {
          if (row != null)
          {
            string a = ((DataRowView)this.gdv_PastRolls.Rows[i].DataBoundItem).Row[PastRoll.TIME_REMOVED].ToString().Trim();
            string b = row[PastRoll.TIME_REMOVED].ToString().Trim();
            if (a == b)
            {
              this.gdv_PastRolls.Rows[i].Selected = true;
            }
          }
        }

        if ((row == null) && (this.gdv_PastRolls.Rows.Count > 0))
          this.gdv_PastRolls.Rows[0].Selected = true;

        if ((FirstDisplayedScrollingRowIndex >= 0) && (FirstDisplayedScrollingRowIndex <= DBManager.getDBManager().pastRolls.getCount()))
          this.gdv_PastRolls.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex;

        this.gdv_PastRolls.ResumeLayout();
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

		protected override void CtrlF4()
		{
		}

    public override void _Show()
		{
      base._Show();
			this.gdv_PastRolls.Focus();
		}

		private void gdv_PastRolls_SelectionChanged(object sender, EventArgs e)
		{
			if (this.gdv_PastRolls.SelectedRows.Count == 1)
			{
				DataRow dataRow = ((DataRowView)this.gdv_PastRolls.SelectedRows[0].DataBoundItem).Row;
				if (dataRow != null && dataRow.RowState != DataRowState.Detached)
				{
					try
					{
						this.lbl_REMAINING_DIAMETER_UDdmm.Text = "REMAINING_DIAMETER_UDdmm: " + dataRow[PastRoll.REMAINING_DIAMETER_UDdmm].ToString();

            //RFAZZI_FI03/03/2010_1. Aggiunta visualizzazione dei dati aggiuntivi per le bobine lavorate nella pagina Report Bobine. 
            this.nud_RollDiameter.Value = dataRow[PastRoll.REMAINING_DIAMETER_UDdmm].ToString();
            this.ttb_DataToPrint.Value = dataRow[PastRoll.DATA_TO_PRINT].ToString();
            this.ttb_Description.Value = dataRow[PastRoll.DESCRIPTION].ToString();

            this.traceSetupForRoll((DateTime)dataRow[PastRoll.TIME_ENTERED],(DateTime)dataRow[PastRoll.TIME_REMOVED]);

          }
					catch (RowNotInTableException ex)
					{
						Logger.LogError(this, "gdv_PastRolls - the rowState is not Detached (it is \"" + dataRow.RowState.ToString() + "\" but the RowNotInTableException has been thrown anyway....", ex);
					}
				}
			}
		}

    private void traceSetupForRoll(DateTime from, DateTime to)
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        return;

      if (from.Equals(DateTime.MinValue) || to.Equals(DateTime.MinValue))
        return;

      try
      {
        string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".trace_syc_rolls";
        string where = String.Format("(time BETWEEN '{0}' AND '{1}')", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(from), SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(to));
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT DISTINCT ON (setupcode) setupcode,time FROM " + myTableName + " WHERE " + where + ";");

        if ((dt != null) && (dt.Rows.Count != 0))
        {
          List<SyncroC.Controls.GroupPanel.Triple<string, string>> myLista = new List<SyncroC.Controls.GroupPanel.Triple<string, string>>();

          foreach (DataRow a in dt.Rows)
          {
            myLista.Add(new SyncroC.Controls.GroupPanel.Triple<string, string>((DateTime)a["time"], (string)a["setupcode"], ""));
          }

          this.myListBox_Order.fillData(myLista);
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "traceSetupForRoll - ", ex);
      }
    }

		private void gdv_PastRolls_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
      DataRow dataRow = ((DataRowView)this.gdv_PastRolls.Rows[e.RowIndex].DataBoundItem).Row;

			if (this.gdv_PastRolls.Columns[e.ColumnIndex].Name == PastRoll.REMAINING_UDmm
						&& e.Value != null)
			{
				e.Value = Convert.ToInt32(e.Value) / 1000;
			}

			if (this.gdv_PastRolls.Columns[e.ColumnIndex].Name == PastRoll.WIDTH_UDdmm
						&& e.Value != null)
			{
				e.Value = Convert.ToInt32(e.Value) / 10;
			}

			if (this.gdv_PastRolls.Columns[e.ColumnIndex].Name.Equals(PastRoll.TIME_REMOVED)
						&& e.Value != null)
			{
				try
				{
          e.Value = SyncroC.Utility.SyncroConverter.ConvertDateTimeToStrForDataGridView(e.Value);
				}
				catch (InvalidCastException ex)
				{
					Logger.LogError(this, "gdv_PastRolls_CellFormatting - " + PastRoll.TIME_REMOVED, ex);
				}
			}
		}
	}
}