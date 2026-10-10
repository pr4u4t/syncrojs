using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.DataBase;


namespace SyncroC.Forms.Pages.SingleFacer.Alarms
{
	public partial class Frm_SingleFacerAlarmHistory : SyncroC.Forms.Pages.SingleFacer.Alarms.Frm_SingleFacerAlarmSubgroup
	{
		private DataTable SingleFacerAlarmHistory;
		private DataView dataView;

		private DataGridViewColumn clm_alarmHistory_Zone;
		private DataGridViewColumn clm_alarmHistory_Description;

		public Frm_SingleFacerAlarmHistory()
		{
			InitializeComponent();

			/**
			 * Grid Initialization
			 */
			//gdv_alarmHistory
			clm_alarmHistory_Zone = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_alarmHistory_Zone.HeaderText = "Zone";
			clm_alarmHistory_Zone.DataPropertyName = AlarmHistory.DESCRIPTION_ZONE;
			clm_alarmHistory_Zone.Width = 200;

			clm_alarmHistory_Description = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_alarmHistory_Description.HeaderText = "Description";
			clm_alarmHistory_Description.DataPropertyName = AlarmHistory.DESCRIPTION;
			clm_alarmHistory_Description.Width = 699;

			gdv_alarmHistory.Columns.Add(clm_alarmHistory_Zone);
			gdv_alarmHistory.Columns.Add(clm_alarmHistory_Description);
		}

		/// <summary>
		/// Builder. Build an istance of this page for the specified SingleFacer.
		/// </summary>
		/// <param name="index">Index of the singleFacer represented on the current instance of this Form</param>
		public Frm_SingleFacerAlarmHistory(int index)
			: this()
		{
			if (! (index == 1 || index == 2 || index == 3))
				Logger.LogError(this, "Frm_SingleFacerAlarmHistory - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\" values are allowed.");
			this.index = index;
			lbl_Title_YN.Text += " " + index.ToString();

			/**
			 * data view and data source initialization
			 */
			string alarmHistoryName = "";
			switch (index)
			{
				case 1:
					alarmHistoryName = AlarmHistory.ALARM_HISTORY_LIST_SF1;
					break;
				case 2:
					alarmHistoryName = AlarmHistory.ALARM_HISTORY_LIST_SF2;
					break;
				case 3:
					alarmHistoryName = AlarmHistory.ALARM_HISTORY_LIST_SF3;
					break;
				default:
					alarmHistoryName = AlarmHistory.ALARM_HISTORY_LIST_SF1;
					break;
			}
			SingleFacerAlarmHistory = DBManager.getDBManager().alarmHistory.alarmHistory.Tables[alarmHistoryName];
			dataView = new DataView(SingleFacerAlarmHistory);
			dataView.Sort = AlarmHistory.END_TIME + " Desc";
			this.gdv_alarmHistory.DataSource = dataView;

		}

		protected override void F11()
		{
		}

		private void gdv_alarmHistory_SelectionChanged(object sender, EventArgs e)
		{
			if (gdv_alarmHistory.SelectedRows.Count == 1)
			{
				DataRow dataRow = ((DataRowView)gdv_alarmHistory.SelectedRows[0].DataBoundItem).Row;
				ttl_Code.Value = dataRow[AlarmHistory.KEY].ToString();
				tmt_Begin.ValueAsUNIVERSALDateTime = ((System.DateTime)dataRow[AlarmHistory.BEGIN_TIME]);
				tmt_End.ValueAsUNIVERSALDateTime = ((System.DateTime)dataRow[AlarmHistory.END_TIME]);
			}
			else
			{
				ttl_Code.Value = tmt_Begin.Value = tmt_End.Value = String.Empty;
			}
		}

		private void Frm_StackerAlarmHistory_Load(object sender, EventArgs e)
		{
			if (gdv_alarmHistory.Rows.Count > 0)
				gdv_alarmHistory.Rows[0].Selected = true;
		}
	}
}

