using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.DataBase;

namespace SyncroC.Forms.Pages.DryEndSupervisor
{
	public class Frm_DryEndSupervisor03 : SyncroC.Forms.Pages.DryEndSupervisor.Frm_DryEndSupervisorGroup
	{
		private DataTable MTH_Alarms;

		private DataGridViewColumn clm_Zone;
		private DataGridViewColumn clm_Description;
		private DataGridViewColumn clm_BeginStart;
		private DataGridViewColumn clm_Key;

		private int COLUMN_KEY_CHANGE_VIEW_WIDTH = 250;

		private DataTable MTHAlarmHistory;
		private DataView dataView;

		private DataGridViewColumn clm_alarmHistory_Zone;
		private DataGridViewColumn clm_alarmHistory_Description;


		private SyncroC.Controls.TitledMaskTextBox tmt_End;
		private SyncroC.Controls.TitledMaskTextBox tmt_Begin;
		private SyncroC.Controls.TitledLabel ttl_Code;
		private SyncroC.Controls.Bracket brk_AlarmDetails;
		private SyncroC.Controls.GridView gdv_alarmHistory;
		private SyncroC.Controls.Title ttl_AlarmsHistory;
		private SyncroC.Controls.Title ttl_Alarms;
		private SyncroC.Controls.GridView gdv_alarm;
		private System.ComponentModel.IContainer components = null;

		public Frm_DryEndSupervisor03()
		{
			InitializeComponent();

			/**
			 * ALARM ACTIVE data view and data source initialization
			 */
			MTH_Alarms = RTCManager.getRTCManager().ActiveAlarms.Tables[RTCManager.MTH];
			DataView dv = new DataView(MTH_Alarms);
			dv.RowFilter = RTCManager.PRESENT + " = true";
			this.gdv_alarm.DataSource = dv;


			/**
			 * ALARM ACTIVE Grid Initialization
			 */
			clm_Zone = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Zone.HeaderText = "Zone";
			clm_Zone.Name = RTCManager.DESCRIPTION_ZONE;
			clm_Zone.DataPropertyName = RTCManager.DESCRIPTION_ZONE;
			clm_Zone.Width = 250;

			clm_Description = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Description.HeaderText = "Description";
			clm_Description.Name = RTCManager.DESCRIPTION;
			clm_Description.DataPropertyName = RTCManager.DESCRIPTION;
			clm_Description.Width = 595;

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





			/**
			 * ALARM HISTORY data view and data source initialization
			 */
			MTHAlarmHistory = DBManager.getDBManager().alarmHistory.alarmHistory.Tables[AlarmHistory.ALARM_HISTORY_LIST_MTH];
			dataView = new DataView(MTHAlarmHistory);
			dataView.Sort = AlarmHistory.BEGIN_TIME + " Desc";
			this.gdv_alarmHistory.DataSource = dataView;

			/**
			 * ALARM HISTORY Grid Initialization
			 */
			//gdv_alarmHistory
			clm_alarmHistory_Zone = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_alarmHistory_Zone.HeaderText = "Zone";
			clm_alarmHistory_Zone.DataPropertyName = AlarmHistory.DESCRIPTION_ZONE;
			clm_alarmHistory_Zone.Width = 200;

			clm_alarmHistory_Description = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_alarmHistory_Description.HeaderText = "Description";
			clm_alarmHistory_Description.DataPropertyName = AlarmHistory.DESCRIPTION;
			clm_alarmHistory_Description.Width = 585;

			gdv_alarmHistory.Columns.Add(clm_alarmHistory_Zone);
			gdv_alarmHistory.Columns.Add(clm_alarmHistory_Description);
		}

		public void _setFrmMachineName(string machineName)
		{
			if (machineName != null && !machineName.Equals(string.Empty))
				lbl_Title_YN.Text = machineName;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
			this.gdv_alarm.DataSource = null;
			this.gdv_alarmHistory.DataSource = null;
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DryEndSupervisor03));
      this.tmt_End = new SyncroC.Controls.TitledMaskTextBox();
      this.tmt_Begin = new SyncroC.Controls.TitledMaskTextBox();
      this.ttl_Code = new SyncroC.Controls.TitledLabel();
      this.brk_AlarmDetails = new SyncroC.Controls.Bracket();
      this.gdv_alarmHistory = new SyncroC.Controls.GridView();
      this.ttl_AlarmsHistory = new SyncroC.Controls.Title();
      this.ttl_Alarms = new SyncroC.Controls.Title();
      this.gdv_alarm = new SyncroC.Controls.GridView();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Text = "Material Handling";
      // 
      // tab_Tab3
      // 
      this.tab_Tab3.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab3.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // btn_Group1
      // 
      this.btn_Group1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedActiveImage")));
      this.btn_Group1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedImage")));
      // 
      // btn_Group2
      // 
      this.btn_Group2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedActiveImage")));
      this.btn_Group2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedImage")));
      // 
      // btn_Group3
      // 
      this.btn_Group3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedActiveImage")));
      this.btn_Group3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedImage")));
      // 
      // btn_Group5
      // 
      this.btn_Group5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedActiveImage")));
      this.btn_Group5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedImage")));
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // tmt_End
      // 
      this.tmt_End.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.tmt_End.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tmt_End.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_End.ImagePassword = null;
      this.tmt_End.Location = new System.Drawing.Point(933, 582);
      this.tmt_End.Name = "tmt_End";
      this.tmt_End.PasswordChar = '\0';
      this.tmt_End.ReadOnly = true;
      this.tmt_End.Size = new System.Drawing.Size(211, 50);
      this.tmt_End.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.tmt_End.TabIndex = 156;
      this.tmt_End.TabStop = false;
      this.tmt_End.Text = "End";
      this.tmt_End.TextBoxMask = SyncroC.Controls.TitledMaskTextBoxMask.TimeAndDate;
      this.tmt_End.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tmt_End.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_End.TranslateText = true;
      this.tmt_End.Value = " ";
      this.tmt_End.ValueAsLOCALDateTime = new System.DateTime(((long)(0)));
      this.tmt_End.ValueAsLOCALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_End.ValueAsUNIVERSALDateTime = new System.DateTime(((long)(0)));
      this.tmt_End.ValueAsUNIVERSALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_End.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tmt_Begin
      // 
      this.tmt_Begin.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.tmt_Begin.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tmt_Begin.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_Begin.ImagePassword = null;
      this.tmt_Begin.Location = new System.Drawing.Point(933, 532);
      this.tmt_Begin.Name = "tmt_Begin";
      this.tmt_Begin.PasswordChar = '\0';
      this.tmt_Begin.ReadOnly = true;
      this.tmt_Begin.Size = new System.Drawing.Size(211, 50);
      this.tmt_Begin.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.tmt_Begin.TabIndex = 155;
      this.tmt_Begin.TabStop = false;
      this.tmt_Begin.Text = "Begin";
      this.tmt_Begin.TextBoxMask = SyncroC.Controls.TitledMaskTextBoxMask.TimeAndDate;
      this.tmt_Begin.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tmt_Begin.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_Begin.TranslateText = true;
      this.tmt_Begin.Value = " ";
      this.tmt_Begin.ValueAsLOCALDateTime = new System.DateTime(((long)(0)));
      this.tmt_Begin.ValueAsLOCALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_Begin.ValueAsUNIVERSALDateTime = new System.DateTime(((long)(0)));
      this.tmt_Begin.ValueAsUNIVERSALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.tmt_Begin.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttl_Code
      // 
      this.ttl_Code.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Code.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ttl_Code.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Code.ImagePassword = null;
      this.ttl_Code.Location = new System.Drawing.Point(933, 482);
      this.ttl_Code.Name = "ttl_Code";
      this.ttl_Code.Size = new System.Drawing.Size(211, 50);
      this.ttl_Code.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Code.TabIndex = 154;
      this.ttl_Code.TabStop = false;
      this.ttl_Code.Text = "Code";
      this.ttl_Code.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Code.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Code.TranslateText = true;
      this.ttl_Code.Value = "";
      this.ttl_Code.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Code.ValueMultiline = true;
      // 
      // brk_AlarmDetails
      // 
      this.brk_AlarmDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.brk_AlarmDetails.color = SyncroC.Controls.Colors.SyncroColor.Green;
      this.brk_AlarmDetails.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.brk_AlarmDetails.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.brk_AlarmDetails.Location = new System.Drawing.Point(901, 458);
      this.brk_AlarmDetails.Name = "brk_AlarmDetails";
      this.brk_AlarmDetails.Position = SyncroC.Controls.BracketPosition.Right;
      this.brk_AlarmDetails.ShowArrow = true;
      this.brk_AlarmDetails.Size = new System.Drawing.Size(243, 200);
      this.brk_AlarmDetails.TabIndex = 153;
      this.brk_AlarmDetails.TabStop = false;
      this.brk_AlarmDetails.Text = "DETAILS";
      this.brk_AlarmDetails.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.brk_AlarmDetails.TranslateText = true;
      // 
      // gdv_alarmHistory
      // 
      this.gdv_alarmHistory.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.gdv_alarmHistory.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.gdv_alarmHistory.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_alarmHistory.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.gdv_alarmHistory.AutoGenerateColumns = false;
      this.gdv_alarmHistory.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_alarmHistory.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Large;
      this.gdv_alarmHistory.DataMember = "";
      this.gdv_alarmHistory.DataSource = null;
      this.gdv_alarmHistory.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gdv_alarmHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(2)))), ((int)(((byte)(28)))));
      this.gdv_alarmHistory.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_alarmHistory.ImagePassword = null;
      this.gdv_alarmHistory.Location = new System.Drawing.Point(86, 484);
      this.gdv_alarmHistory.MultiSelect = false;
      this.gdv_alarmHistory.Name = "gdv_alarmHistory";
      this.gdv_alarmHistory.RowsHeight = 32;
      this.gdv_alarmHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_alarmHistory.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_alarmHistory.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.gdv_alarmHistory.Size = new System.Drawing.Size(810, 174);
      this.gdv_alarmHistory.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_alarmHistory.TabIndex = 152;
      this.gdv_alarmHistory.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_alarmHistory.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_alarmHistory.TranslateText = true;
      this.gdv_alarmHistory.SelectionChanged += new System.EventHandler(this.gdv_alarmHistory_SelectionChanged);
      // 
      // ttl_AlarmsHistory
      // 
      this.ttl_AlarmsHistory.BackColor = System.Drawing.Color.Black;
      this.ttl_AlarmsHistory.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_AlarmsHistory.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_AlarmsHistory.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_AlarmsHistory.Location = new System.Drawing.Point(77, 458);
      this.ttl_AlarmsHistory.Name = "ttl_AlarmsHistory";
      this.ttl_AlarmsHistory.Size = new System.Drawing.Size(822, 200);
      this.ttl_AlarmsHistory.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_AlarmsHistory.TabIndex = 151;
      this.ttl_AlarmsHistory.TabStop = false;
      this.ttl_AlarmsHistory.Text = "ALARMS\' HISTORY";
      this.ttl_AlarmsHistory.TranslateText = true;
      // 
      // ttl_Alarms
      // 
      this.ttl_Alarms.BackColor = System.Drawing.Color.Black;
      this.ttl_Alarms.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Alarms.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Alarms.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Alarms.Location = new System.Drawing.Point(77, 250);
      this.ttl_Alarms.Name = "ttl_Alarms";
      this.ttl_Alarms.Size = new System.Drawing.Size(1067, 200);
      this.ttl_Alarms.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_Alarms.TabIndex = 150;
      this.ttl_Alarms.Text = "MATERIAL HANDLING ALARMS";
      this.ttl_Alarms.TranslateText = true;
      // 
      // gdv_alarm
      // 
      this.gdv_alarm.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_alarm.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.gdv_alarm.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_alarm.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.gdv_alarm.AutoGenerateColumns = false;
      this.gdv_alarm.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_alarm.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Large;
      this.gdv_alarm.DataMember = "";
      this.gdv_alarm.DataSource = null;
      this.gdv_alarm.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gdv_alarm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(2)))), ((int)(((byte)(28)))));
      this.gdv_alarm.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_alarm.ImagePassword = null;
      this.gdv_alarm.Location = new System.Drawing.Point(87, 276);
      this.gdv_alarm.MultiSelect = false;
      this.gdv_alarm.Name = "gdv_alarm";
      this.gdv_alarm.RowsHeight = 32;
      this.gdv_alarm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_alarm.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_alarm.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.gdv_alarm.Size = new System.Drawing.Size(1057, 174);
      this.gdv_alarm.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_alarm.TabIndex = 149;
      this.gdv_alarm.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_alarm.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_alarm.TranslateText = true;
      this.gdv_alarm.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_alarm_CellFormatting);
      // 
      // Frm_DryEndSupervisor03
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tmt_End);
      this.Controls.Add(this.tmt_Begin);
      this.Controls.Add(this.ttl_Code);
      this.Controls.Add(this.brk_AlarmDetails);
      this.Controls.Add(this.gdv_alarmHistory);
      this.Controls.Add(this.ttl_AlarmsHistory);
      this.Controls.Add(this.ttl_Alarms);
      this.Controls.Add(this.gdv_alarm);
      this.Name = "Frm_DryEndSupervisor03";
      this.Load += new System.EventHandler(this.Frm_DryEndSupervisor03_Load);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.lbl_Date, 0);
      this.Controls.SetChildIndex(this.lbl_Time, 0);
      this.Controls.SetChildIndex(this.lbl_Status, 0);
      this.Controls.SetChildIndex(this.tab_Tab1, 0);
      this.Controls.SetChildIndex(this.tab_Tab2, 0);
      this.Controls.SetChildIndex(this.tab_Tab3, 0);
      this.Controls.SetChildIndex(this.tab_Tab4, 0);
      this.Controls.SetChildIndex(this.tab_Tab5, 0);
      this.Controls.SetChildIndex(this.tab_Tab6, 0);
      this.Controls.SetChildIndex(this.tab_Tab7, 0);
      this.Controls.SetChildIndex(this.tab_Tab8, 0);
      this.Controls.SetChildIndex(this.tab_TabA, 0);
      this.Controls.SetChildIndex(this.tab_TabD, 0);
      this.Controls.SetChildIndex(this.tab_TabC, 0);
      this.Controls.SetChildIndex(this.tab_TabB, 0);
      this.Controls.SetChildIndex(this.pct_Home_NY, 0);
      this.Controls.SetChildIndex(this.pct_Back_NY, 0);
      this.Controls.SetChildIndex(this.pct_Forward_NY, 0);
      this.Controls.SetChildIndex(this.lbl_Title_YN, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
      this.Controls.SetChildIndex(this.btn_Group1, 0);
      this.Controls.SetChildIndex(this.btn_Group2, 0);
      this.Controls.SetChildIndex(this.btn_Group3, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      this.Controls.SetChildIndex(this.gdv_alarm, 0);
      this.Controls.SetChildIndex(this.ttl_Alarms, 0);
      this.Controls.SetChildIndex(this.ttl_AlarmsHistory, 0);
      this.Controls.SetChildIndex(this.gdv_alarmHistory, 0);
      this.Controls.SetChildIndex(this.brk_AlarmDetails, 0);
      this.Controls.SetChildIndex(this.ttl_Code, 0);
      this.Controls.SetChildIndex(this.tmt_Begin, 0);
      this.Controls.SetChildIndex(this.tmt_End, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		protected override void F3()
		{
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
					e.Value = ((DateTime)e.Value).ToLocalTime().ToString().Replace('.', ':');
				}
				catch (InvalidCastException ex)
				{
					Logger.LogError(this, "gdv_alarm_CellFormatting - " + RTCManager.BEGIN_TIME, ex);
				}
			}
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

		private void Frm_DryEndSupervisor03_Load(object sender, EventArgs e)
		{
			if (gdv_alarmHistory.Rows.Count > 0)
				gdv_alarmHistory.Rows[0].Selected = true;
		}

	}
}

