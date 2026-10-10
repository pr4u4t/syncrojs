using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.DataBase;

namespace SyncroC.Forms.Pages.PreHeaterTern.Alarms
{
	public partial class Frm_PreHeaterTernAlarmHistory : SyncroC.Forms.Pages.PreHeaterTern.Alarms.Frm_PreHeaterTernAlarmSubgroup
	{
		private DataTable PreHeaterTernAlarmHistory;
		private DataView dataView;

		//colonne che avrebbe dovuto fare il designer.
		private DataGridViewColumn clm_alarmHistory_Zone;
		private DataGridViewColumn clm_alarmHistory_Description;

		public Frm_PreHeaterTernAlarmHistory()
		{
			InitializeComponent();

			/**
			 * data view and data source initialization
			 */
			PreHeaterTernAlarmHistory = DBManager.getDBManager().alarmHistory.alarmHistory.Tables[AlarmHistory.ALARM_HISTORY_LIST_TERN];
			dataView = new DataView(PreHeaterTernAlarmHistory);
			dataView.Sort = AlarmHistory.BEGIN_TIME + " Desc";
			this.gdv_alarmHistory.DataSource = dataView;

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

		private void Frm_PreHeaterTernAlarmHistory_Load(object sender, EventArgs e)
		{
			if (gdv_alarmHistory.Rows.Count > 0)
				gdv_alarmHistory.Rows[0].Selected = true;
		}
	}
}

