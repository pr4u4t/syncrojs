using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.DataBase;

namespace SyncroC.Forms.Pages.Splicer.Alarms
{
	public partial class Frm_SplicerAlarmHistory : SyncroC.Forms.Pages.Splicer.Alarms.Frm_SplicerAlarmsSubgroup
	{
		private DataTable SplicerAlarmHistory;
		private DataView dataView;

		private DataGridViewColumn clm_alarmHistory_Zone;
		private DataGridViewColumn clm_alarmHistory_Description;


		public Frm_SplicerAlarmHistory()
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
		/// Builder. Build an istance of this page for the specified Splicer.
		/// </summary>
		/// <param name="index">Index of the splicer represented on the current instance of this Form</param>
		public Frm_SplicerAlarmHistory(int index)
			: this()
		{
			if (!(index == 1 || index == 2 || index == 3 || index == 4 || index == 5 || index == 6 || index == 7))
				Logger.LogError(this, "Frm_SplicerAlarmHistory - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\", \"4\", \"5\", \"6\", \"7\" values are allowed.");
			this.index = index;
			lbl_Title_YN.Text += " " + index.ToString();

			/**
			 * data view and data source initialization
			 */
			string alarmHistoryName = "";
			switch (index)
			{
				case 1:
					alarmHistoryName = AlarmHistory.ALARM_HISTORY_LIST_SPLICER1;
					break;
				case 2:
					alarmHistoryName = AlarmHistory.ALARM_HISTORY_LIST_SPLICER2;
					break;
				case 3:
					alarmHistoryName = AlarmHistory.ALARM_HISTORY_LIST_SPLICER3;
					break;
				case 4:
					alarmHistoryName = AlarmHistory.ALARM_HISTORY_LIST_SPLICER4;
					break;
				case 5:
					alarmHistoryName = AlarmHistory.ALARM_HISTORY_LIST_SPLICER5;
					break;
				case 6:
					alarmHistoryName = AlarmHistory.ALARM_HISTORY_LIST_SPLICER6;
					break;
				case 7:
					alarmHistoryName = AlarmHistory.ALARM_HISTORY_LIST_SPLICER7;
					break;
				default:
					alarmHistoryName = AlarmHistory.ALARM_HISTORY_LIST_SPLICER1;
					break;
			}
			SplicerAlarmHistory = DBManager.getDBManager().alarmHistory.alarmHistory.Tables[alarmHistoryName];
			dataView = new DataView(SplicerAlarmHistory);
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

		private void Frm_SplicerAlarmHistory_Load(object sender, EventArgs e)
		{
			if (gdv_alarmHistory.Rows.Count > 0)
				gdv_alarmHistory.Rows[0].Selected = true;
		}
	}
}

