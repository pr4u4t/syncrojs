using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;

namespace SyncroC.Forms.Pages.PreHeaterTern.Alarms
{
	public partial class Frm_PreHeaterTernAlarmActive : SyncroC.Forms.Pages.PreHeaterTern.Alarms.Frm_PreHeaterTernAlarmSubgroup
	{
		private DataTable PreHeaterTern_Alarms;

		private DataGridViewColumn clm_Zone;
		private DataGridViewColumn clm_Description;
		private DataGridViewColumn clm_BeginStart;
		private DataGridViewColumn clm_Key;

		private int COLUMN_KEY_CHANGE_VIEW_WIDTH = 250;
		private bool showDebugOption = false;

		public Frm_PreHeaterTernAlarmActive()
		{
			InitializeComponent();

			PreHeaterTern_Alarms = RTCManager.getRTCManager().ActiveAlarms.Tables[RTCManager.TERN];
			DataView dv = new DataView(PreHeaterTern_Alarms);
			dv.RowFilter = RTCManager.PRESENT + " = true";
			this.gdv_alarm.DataSource = dv;

			clm_Zone = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Zone.HeaderText = "Zone";
			clm_Zone.Name = RTCManager.DESCRIPTION_ZONE;
			clm_Zone.DataPropertyName = RTCManager.DESCRIPTION_ZONE;
			clm_Zone.Width = 250;

			clm_Description = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Description.HeaderText = "Description";
			clm_Description.Name = RTCManager.DESCRIPTION;
			clm_Description.DataPropertyName = RTCManager.DESCRIPTION;
			clm_Description.Width = 699;

			clm_BeginStart = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_BeginStart.HeaderText = "Begin";
			clm_BeginStart.Name = RTCManager.BEGIN_TIME;
			clm_BeginStart.DataPropertyName = RTCManager.BEGIN_TIME;
			clm_BeginStart.Width = 190;

			clm_Key = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Key.HeaderText = "Code";
			clm_Key.Name = RTCManager.KEY;
			clm_Key.DataPropertyName = RTCManager.KEY;
			clm_Key.Visible = false;
			clm_Key.Width = COLUMN_KEY_CHANGE_VIEW_WIDTH;

			gdv_alarm.Columns.Add(clm_Zone);
			gdv_alarm.Columns.Add(clm_Description);
			gdv_alarm.Columns.Add(clm_BeginStart);
			gdv_alarm.Columns.Add(clm_Key);

			gdv_alarm.Sort(clm_BeginStart, SyncroC.DataBase.Alarm.listSortDirection);
		}

		protected override void F10()
		{
		}

		protected override void ControlD()
		{
			base.ControlD();
			showDebugOption = !showDebugOption;
			gdv_alarm.SelectionBackColor = (showDebugOption) ? SyncroC.Controls.Colors.SyncroColor.White : SyncroC.Controls.Colors.SyncroColor.Green;
			gdv_alarm.AlternateSelectionBackColor = (showDebugOption) ? SyncroC.Controls.Colors.SyncroColor.LightGray : SyncroC.Controls.Colors.SyncroColor.Green;
			lbl_codeZone_Description.Visible = lbl_codeZone.Visible = lbl_Key.Visible = lbl_Key_Description.Visible =
				lbl_codeZone_Description.Visible = lbl_codeZone.Visible =
				lbl_present_Description.Visible = lbl_Present.Visible = showDebugOption;
		}

		protected override void CtrlF1()
		{
			if (!clm_Key.Visible)
			{//wiew without key, so show key column
				clm_Description.Width -= COLUMN_KEY_CHANGE_VIEW_WIDTH;
				clm_Key.Visible = true;
			}
			else
			{//view with key, so hide key column
				clm_Description.Width += COLUMN_KEY_CHANGE_VIEW_WIDTH;
				clm_Key.Visible = false;
			}
			gdv_alarm.Invalidate();
		}

		private void gdv_alarm_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (this.gdv_alarm.Columns[e.ColumnIndex].Name == RTCManager.BEGIN_TIME
						&& e.Value != null)
			{
				try
				{
					//FOS2 ABello
					//e.Value =  ((DateTime)e.Value).ToLocalTime().ToString().Replace('.', ':');
					e.Value = SyncroC.Utility.SyncroConverter.ConvertDateTimeToStrForDataGridView(((DateTime)e.Value).ToLocalTime());
				}
				catch (InvalidCastException ex)
				{
					Logger.LogError(this, "gdv_alarm_CellFormatting - " + RTCManager.BEGIN_TIME, ex);
				}
			}
		}

		private void gdv_alarm_SelectionChanged(object sender, EventArgs e)
		{
			if (gdv_alarm.SelectedRows.Count == 1/* &&
			Knife_Alarms.Rows.Count >= gdv_alarm.SelectedRows[0].Index*/
																																	)
			{
				//DataRow dataRow = Knife_Alarms.Rows[gdv_alarm.SelectedRows[0].Index];
				DataRow dataRow = ((DataRowView)gdv_alarm.SelectedRows[0].DataBoundItem).Row;
				if (dataRow != null)
				{
					lbl_Key.Text = dataRow[RTCManager.KEY].ToString();
					lbl_codeZone.Text = dataRow[RTCManager.CODE_ZONE].ToString();
					lbl_Present.Text = dataRow[RTCManager.PRESENT].ToString();
				}
				else
				{
					lbl_codeZone.Text = lbl_Present.Text = string.Empty;
				}
			}
			else
			{
				lbl_codeZone.Text = lbl_Present.Text = string.Empty;
			}
		}
	}
}

