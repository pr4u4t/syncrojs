using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.Utility;
using SyncroC.DataBase;

namespace SyncroC.Forms.Pages.Production.Alarms
{
	public class Frm_Alarm2 : SyncroC.Forms.Pages.Production.Alarms.Frm_AlarmsSubGroup
	{
		private Label lbl_Present;
		private Label lbl_codeZone;
		private Label lbl_present_Description;
		private Label lbl_codeZone_Description;
		private SyncroC.Controls.Title ttl_SyncroHistoryAlarms;
		private SyncroC.Controls.GridView gdv_alarm;
		private System.ComponentModel.IContainer components = null;

		private DataGridViewColumn clm_Zone;
		private DataGridViewColumn clm_Description;

		private DataTable SyncroAlarmHistory;
		private SyncroC.Controls.TitledMaskTextBox tmt_End;
		private SyncroC.Controls.TitledMaskTextBox tmt_Begin;
		private SyncroC.Controls.TitledLabel ttl_Code;
		private SyncroC.Controls.Bracket brk_AlarmDetails;

		private bool showDebugOption = false;

		public Frm_Alarm2()
		{
			InitializeComponent();

			SyncroAlarmHistory = DBManager.getDBManager().alarmHistory.alarmHistory.Tables[AlarmHistory.ALARM_HISTORY_LIST_SYC_SW];
			SyncroAlarmHistory.Merge(DBManager.getDBManager().alarmHistory.alarmHistory.Tables[AlarmHistory.ALARM_HISTORY_LIST_SYC_HW]);
			DataView dataView = new DataView(SyncroAlarmHistory);
      //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
      dataView.RowFilter = RTCManager.CODE_ZONE + " <> 'Warning'";

      dataView.Sort = AlarmHistory.BEGIN_TIME + " Desc";
			this.gdv_alarm.DataSource = dataView;

			clm_Zone = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Zone.HeaderText = "Zone";
			clm_Zone.Name = RTCManager.DESCRIPTION_ZONE;
			clm_Zone.DataPropertyName = RTCManager.DESCRIPTION_ZONE;
			clm_Zone.Width = 200;

			clm_Description = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Description.HeaderText = "Description";
			clm_Description.Name = RTCManager.DESCRIPTION;
			clm_Description.DataPropertyName = RTCManager.DESCRIPTION;
			//clm_Description.Width = 585;
      clm_Description.Width = 689;

			gdv_alarm.Columns.Add(clm_Zone);
			gdv_alarm.Columns.Add(clm_Description);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			this.gdv_alarm.DataSource = null;//unbind
			base.Dispose(disposing);
		}

		/// <summary>
		/// F11 key behaviour: must not do anything
		/// </summary>
		protected override void F11()
		{
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Alarm2));
      this.lbl_Present = new System.Windows.Forms.Label();
      this.lbl_codeZone = new System.Windows.Forms.Label();
      this.lbl_present_Description = new System.Windows.Forms.Label();
      this.lbl_codeZone_Description = new System.Windows.Forms.Label();
      this.ttl_SyncroHistoryAlarms = new SyncroC.Controls.Title();
      this.gdv_alarm = new SyncroC.Controls.GridView();
      this.tmt_End = new SyncroC.Controls.TitledMaskTextBox();
      this.tmt_Begin = new SyncroC.Controls.TitledMaskTextBox();
      this.ttl_Code = new SyncroC.Controls.TitledLabel();
      this.brk_AlarmDetails = new SyncroC.Controls.Bracket();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_TabB
      // 
      this.tab_TabB.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.SelectedAlarm;
      // 
      // iml_navigationImages
      // 
      this.iml_navigationImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_navigationImages.ImageStream")));
      this.iml_navigationImages.Images.SetKeyName(0, "");
      this.iml_navigationImages.Images.SetKeyName(1, "");
      this.iml_navigationImages.Images.SetKeyName(2, "");
      this.iml_navigationImages.Images.SetKeyName(3, "");
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
      // lbl_Present
      // 
      this.lbl_Present.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Present.Location = new System.Drawing.Point(385, 66);
      this.lbl_Present.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
      this.lbl_Present.Name = "lbl_Present";
      this.lbl_Present.Size = new System.Drawing.Size(68, 23);
      this.lbl_Present.TabIndex = 126;
      this.lbl_Present.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_Present.Visible = false;
      // 
      // lbl_codeZone
      // 
      this.lbl_codeZone.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_codeZone.Location = new System.Drawing.Point(173, 66);
      this.lbl_codeZone.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
      this.lbl_codeZone.Name = "lbl_codeZone";
      this.lbl_codeZone.Size = new System.Drawing.Size(148, 23);
      this.lbl_codeZone.TabIndex = 125;
      this.lbl_codeZone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_codeZone.Visible = false;
      // 
      // lbl_present_Description
      // 
      this.lbl_present_Description.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_present_Description.Location = new System.Drawing.Point(331, 66);
      this.lbl_present_Description.Name = "lbl_present_Description";
      this.lbl_present_Description.Size = new System.Drawing.Size(54, 23);
      this.lbl_present_Description.TabIndex = 124;
      this.lbl_present_Description.Text = "Present:";
      this.lbl_present_Description.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_present_Description.Visible = false;
      // 
      // lbl_codeZone_Description
      // 
      this.lbl_codeZone_Description.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_codeZone_Description.Location = new System.Drawing.Point(108, 66);
      this.lbl_codeZone_Description.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.lbl_codeZone_Description.Name = "lbl_codeZone_Description";
      this.lbl_codeZone_Description.Size = new System.Drawing.Size(65, 23);
      this.lbl_codeZone_Description.TabIndex = 123;
      this.lbl_codeZone_Description.Text = "Code Zone:";
      this.lbl_codeZone_Description.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_codeZone_Description.Visible = false;
      // 
      // ttl_SyncroHistoryAlarms
      // 
      this.ttl_SyncroHistoryAlarms.BackColor = System.Drawing.Color.Black;
      this.ttl_SyncroHistoryAlarms.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SyncroHistoryAlarms.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SyncroHistoryAlarms.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_SyncroHistoryAlarms.Location = new System.Drawing.Point(30, 193);
      this.ttl_SyncroHistoryAlarms.Name = "ttl_SyncroHistoryAlarms";
      this.ttl_SyncroHistoryAlarms.Size = new System.Drawing.Size(926, 519);
      this.ttl_SyncroHistoryAlarms.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_SyncroHistoryAlarms.TabIndex = 122;
      this.ttl_SyncroHistoryAlarms.Text = "Syncro history alarms";
      this.ttl_SyncroHistoryAlarms.TranslateText = true;
      // 
      // gdv_alarm
      // 
      this.gdv_alarm.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_alarm.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.gdv_alarm.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
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
      this.gdv_alarm.Location = new System.Drawing.Point(40, 221);
      this.gdv_alarm.MultiSelect = false;
      this.gdv_alarm.Name = "gdv_alarm";
      this.gdv_alarm.RowsHeight = 32;
      this.gdv_alarm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_alarm.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_alarm.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.gdv_alarm.Size = new System.Drawing.Size(916, 491);
      this.gdv_alarm.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_alarm.TabIndex = 121;
      this.gdv_alarm.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_alarm.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_alarm.TranslateText = true;
      this.gdv_alarm.SelectionChanged += new System.EventHandler(this.gdv_alarm_SelectionChanged);
      this.gdv_alarm.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_alarm_CellFormatting);
      // 
      // tmt_End
      // 
      this.tmt_End.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.tmt_End.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tmt_End.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_End.ImagePassword = null;
      this.tmt_End.Location = new System.Drawing.Point(1003, 398);
      this.tmt_End.Name = "tmt_End";
      this.tmt_End.PasswordChar = '\0';
      this.tmt_End.ReadOnly = true;
      this.tmt_End.Size = new System.Drawing.Size(198, 52);
      this.tmt_End.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.tmt_End.TabIndex = 130;
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
      this.tmt_Begin.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tmt_Begin.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_Begin.ImagePassword = null;
      this.tmt_Begin.Location = new System.Drawing.Point(1003, 312);
      this.tmt_Begin.Name = "tmt_Begin";
      this.tmt_Begin.PasswordChar = '\0';
      this.tmt_Begin.ReadOnly = true;
      this.tmt_Begin.Size = new System.Drawing.Size(198, 52);
      this.tmt_Begin.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.tmt_Begin.TabIndex = 129;
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
      this.ttl_Code.Location = new System.Drawing.Point(1003, 237);
      this.ttl_Code.Name = "ttl_Code";
      this.ttl_Code.Size = new System.Drawing.Size(198, 52);
      this.ttl_Code.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Code.TabIndex = 128;
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
      this.brk_AlarmDetails.Location = new System.Drawing.Point(962, 193);
      this.brk_AlarmDetails.Name = "brk_AlarmDetails";
      this.brk_AlarmDetails.Position = SyncroC.Controls.BracketPosition.Right;
      this.brk_AlarmDetails.ShowArrow = true;
      this.brk_AlarmDetails.Size = new System.Drawing.Size(239, 519);
      this.brk_AlarmDetails.TabIndex = 127;
      this.brk_AlarmDetails.TabStop = false;
      this.brk_AlarmDetails.Text = "DETAILS";
      this.brk_AlarmDetails.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.brk_AlarmDetails.TranslateText = true;
      // 
      // Frm_Alarm2
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tmt_End);
      this.Controls.Add(this.tmt_Begin);
      this.Controls.Add(this.ttl_Code);
      this.Controls.Add(this.brk_AlarmDetails);
      this.Controls.Add(this.lbl_Present);
      this.Controls.Add(this.lbl_codeZone);
      this.Controls.Add(this.lbl_present_Description);
      this.Controls.Add(this.lbl_codeZone_Description);
      this.Controls.Add(this.ttl_SyncroHistoryAlarms);
      this.Controls.Add(this.gdv_alarm);
      this.Name = "Frm_Alarm2";
      this.Text = "Frm_Alarm2";
      this.Load += new System.EventHandler(this.Frm_Alarm2_Load);
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
      this.Controls.SetChildIndex(this.ttl_SyncroHistoryAlarms, 0);
      this.Controls.SetChildIndex(this.lbl_codeZone_Description, 0);
      this.Controls.SetChildIndex(this.lbl_present_Description, 0);
      this.Controls.SetChildIndex(this.lbl_codeZone, 0);
      this.Controls.SetChildIndex(this.lbl_Present, 0);
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

		protected override void ControlD()
		{
			base.ControlD();
			showDebugOption = !showDebugOption;
			gdv_alarm.SelectionBackColor = (showDebugOption) ? SyncroC.Controls.Colors.SyncroColor.LightGray : SyncroC.Controls.Colors.SyncroColor.Green;
			gdv_alarm.AlternateSelectionBackColor = (showDebugOption) ? SyncroC.Controls.Colors.SyncroColor.LightGray : SyncroC.Controls.Colors.SyncroColor.Green;
			lbl_codeZone_Description.Visible = lbl_codeZone.Visible =
				lbl_present_Description.Visible = lbl_Present.Visible = showDebugOption;
		}

		private void gdv_alarm_SelectionChanged(object sender, EventArgs e)
		{
			if (gdv_alarm.SelectedRows.Count == 1 /*&&
				SyncroAlarmHistory.Rows.Count >= gdv_alarm.SelectedRows[0].Index*/)
			{
				//DataRow dataRow = SyncroAlarmHistory.Rows[gdv_alarm.SelectedRows[0].Index];
				DataRow dataRow = ((DataRowView)gdv_alarm.SelectedRows[0].DataBoundItem).Row;
				if (dataRow != null)
				{
					ttl_Code.Value = dataRow[AlarmHistory.KEY].ToString();
					tmt_Begin.ValueAsUNIVERSALDateTime = ((System.DateTime)dataRow[AlarmHistory.BEGIN_TIME]);
					tmt_End.ValueAsUNIVERSALDateTime = ((System.DateTime)dataRow[AlarmHistory.END_TIME]);
				}
				else
				{
					ttl_Code.Value = tmt_Begin.Value = tmt_End.Value = String.Empty;
				}
			}
			else
			{
				ttl_Code.Value = tmt_Begin.Value = tmt_End.Value = String.Empty;
			}
		}

		private void gdv_alarm_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (this.gdv_alarm.Columns[e.ColumnIndex].Name == RTCManager.BEGIN_TIME
						&& e.Value != null)
			{
				try
				{
					string formattedTime = ((DateTime)e.Value).ToLocalTime().ToLongTimeString();
					e.Value = (formattedTime.Length == 8)
						? formattedTime.Replace('.', ':')
						: "0" + formattedTime.Replace('.', ':');
				}
				catch (InvalidCastException ex)
				{
					Logger.LogError(this, "gdv_alarm_CellFormatting - " + RTCManager.BEGIN_TIME, ex);
				}
			}
		}

		private void Frm_Alarm2_Load(object sender, EventArgs e)
		{
			if (gdv_alarm.Rows.Count > 0)
				gdv_alarm.Rows[0].Selected = true;
		}
	}
}

