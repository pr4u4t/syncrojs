using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.DataBase;
using SyncroC.Network;


namespace SyncroC.Forms.Pages.DryEndSupervisor
{
	public class Frm_DryEndSupervisor02 : SyncroC.Forms.Pages.DryEndSupervisor.Frm_DryEndSupervisorGroup
	{
		private DataTable ExternalWEC_Alarms;

		private DataGridViewColumn clm_Zone;
		private DataGridViewColumn clm_Description;
		private DataGridViewColumn clm_BeginStart;
		private DataGridViewColumn clm_Key;

		private int COLUMN_KEY_CHANGE_VIEW_WIDTH = 250;

		private DBManager dbm;

		private DataTable ExternalWECAlarmHistory;
		private DataView dataView;

		private DataGridViewColumn clm_alarmHistory_Zone;
		private DataGridViewColumn clm_alarmHistory_Description;



		private SyncroC.Controls.Title ttl_Alarms;
		private SyncroC.Controls.GridView gdv_alarm;
		private SyncroC.Controls.TitledMaskTextBox tmt_End;
		private SyncroC.Controls.TitledMaskTextBox tmt_Begin;
		private SyncroC.Controls.TitledLabel ttl_Code;
		private SyncroC.Controls.Bracket brk_AlarmDetails;
		private SyncroC.Controls.GridView gdv_alarmHistory;
		private SyncroC.Controls.Title ttl_AlarmsHistory;
		private SyncroC.Controls.Title ttl_CurrentProgram;
		private SyncroC.Controls.Title ttl_NextProgram;
		public SyncroC.Controls.StateButton stb_AutoMan;
		private SyncroC.Controls.Title ttl_manualSendButtons;
		private SyncroC.Controls.StateButton SendNextPlusOneProgram;
		private SyncroC.Controls.StateButton SendNextProgram;
		private SyncroC.Controls.StateButton SendCurrentProgram;
		private SyncroC.Controls.Title ttl_NextPlusOneProgram;
		private SyncroC.Controls.StateButton WetShiftRequest;
		private SyncroC.Controls.TitledLabel ttl_CurrentQuality;
		private SyncroC.Controls.TitledLabel ttl_CurrentLength;
		private SyncroC.Controls.TitledLabel ttl_NextLength;
		private SyncroC.Controls.TitledLabel ttl_NextQuality;
		private SyncroC.Controls.TitledLabel ttl_NextPlusOneLength;
		private SyncroC.Controls.TitledLabel ttl_NextPlusOneQuality;
		private System.ComponentModel.IContainer components = null;

		public Frm_DryEndSupervisor02()
		{
			InitializeComponent();
			dbm = DBManager.getDBManager();

		/**
		 * ALARM ACTIVE data view and data source initialization
		 */
			ExternalWEC_Alarms = RTCManager.getRTCManager().ActiveAlarms.Tables[RTCManager.EXTERN_WEC];
			DataView dv = new DataView(ExternalWEC_Alarms);
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
			ExternalWECAlarmHistory = DBManager.getDBManager().alarmHistory.alarmHistory.Tables[AlarmHistory.ALARM_HISTORY_LIST_EXTERNAL_WEC];
			dataView = new DataView(ExternalWECAlarmHistory);
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

		public void fillCurrentProgram(DataRow currentRow)
		{
			ttl_CurrentProgram.Text = dbm.message.getTranslatedMessage("§SYCC_94");
			if (currentRow == null)
			{
				ttl_CurrentQuality.Value = string.Empty;
				ttl_CurrentLength.Value = string.Empty;
			}
			else 
			{
				ttl_CurrentProgram.Text += ": " + currentRow[Order.WEL_PROGRAM_CODE].ToString(); 
				ttl_CurrentQuality.Value = currentRow[Order.WEL_PROGRAM_QUALITY].ToString();
				ttl_CurrentLength.Value = currentRow[Order.WEL_PROGRAM_LENGTH].ToString();
			}
		}

		public void fillNextProgram(DataRow nextRow)
		{
			ttl_NextProgram.Text = dbm.message.getTranslatedMessage("§SYCC_95"); 
			if (nextRow == null)
			{
				ttl_NextQuality.Value = string.Empty;
				ttl_NextLength.Value = string.Empty;
			}
			else
			{
				ttl_NextProgram.Text += ": " + nextRow[Order.WEL_PROGRAM_CODE].ToString(); 
				ttl_NextQuality.Value = nextRow[Order.WEL_PROGRAM_QUALITY].ToString();
				ttl_NextLength.Value = nextRow[Order.WEL_PROGRAM_LENGTH].ToString();
			}
		}

		public void fillNextPlusOneProgram(DataRow nextPlusOneRow)
		{
			ttl_NextPlusOneProgram.Text = dbm.message.getTranslatedMessage("§SYCC_96");
			if (nextPlusOneRow == null)
			{
				ttl_NextPlusOneQuality.Value = string.Empty;
				ttl_NextPlusOneLength.Value = string.Empty;
			}
			else
			{
				ttl_NextPlusOneProgram.Text += ": " + nextPlusOneRow[Order.WEL_PROGRAM_CODE].ToString();
				ttl_NextPlusOneQuality.Value = nextPlusOneRow[Order.WEL_PROGRAM_QUALITY].ToString();
				ttl_NextPlusOneLength.Value = nextPlusOneRow[Order.WEL_PROGRAM_LENGTH].ToString();
			}
		}

		public void _fillProgram(int position, int programID, int programModID)
		{
			DataRow row = DBManager.getDBManager().order.WETGetRow(programID.ToString(), programModID.ToString());
			switch (position)
			{
				case 1:
					fillCurrentProgram(row);
					break;
				case 2:
					fillNextProgram(row);
					break;
				case 3:
					fillNextPlusOneProgram(row);
					break;
				default:
					Logger.LogError(this, "_fillProgram - Invalid position (" + position.ToString() + ") specified.");
					break;
			}
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DryEndSupervisor02));
      this.ttl_Alarms = new SyncroC.Controls.Title();
      this.gdv_alarm = new SyncroC.Controls.GridView();
      this.tmt_End = new SyncroC.Controls.TitledMaskTextBox();
      this.tmt_Begin = new SyncroC.Controls.TitledMaskTextBox();
      this.ttl_Code = new SyncroC.Controls.TitledLabel();
      this.brk_AlarmDetails = new SyncroC.Controls.Bracket();
      this.gdv_alarmHistory = new SyncroC.Controls.GridView();
      this.ttl_AlarmsHistory = new SyncroC.Controls.Title();
      this.ttl_CurrentProgram = new SyncroC.Controls.Title();
      this.ttl_NextProgram = new SyncroC.Controls.Title();
      this.stb_AutoMan = new SyncroC.Controls.StateButton();
      this.ttl_manualSendButtons = new SyncroC.Controls.Title();
      this.SendNextPlusOneProgram = new SyncroC.Controls.StateButton();
      this.SendNextProgram = new SyncroC.Controls.StateButton();
      this.SendCurrentProgram = new SyncroC.Controls.StateButton();
      this.ttl_NextPlusOneProgram = new SyncroC.Controls.Title();
      this.WetShiftRequest = new SyncroC.Controls.StateButton();
      this.ttl_CurrentQuality = new SyncroC.Controls.TitledLabel();
      this.ttl_CurrentLength = new SyncroC.Controls.TitledLabel();
      this.ttl_NextLength = new SyncroC.Controls.TitledLabel();
      this.ttl_NextQuality = new SyncroC.Controls.TitledLabel();
      this.ttl_NextPlusOneLength = new SyncroC.Controls.TitledLabel();
      this.ttl_NextPlusOneQuality = new SyncroC.Controls.TitledLabel();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Text = "External Wet End Control";
      // 
      // tab_Tab2
      // 
      this.tab_Tab2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab2.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // ttl_Alarms
      // 
      this.ttl_Alarms.BackColor = System.Drawing.Color.Black;
      this.ttl_Alarms.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Alarms.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Alarms.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Alarms.Location = new System.Drawing.Point(83, 401);
      this.ttl_Alarms.Name = "ttl_Alarms";
      this.ttl_Alarms.Size = new System.Drawing.Size(1067, 100);
      this.ttl_Alarms.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Alarms.TabIndex = 134;
      this.ttl_Alarms.Text = "EXTERNAL WET END CONTROL ALARMS";
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
      this.gdv_alarm.Location = new System.Drawing.Point(92, 421);
      this.gdv_alarm.MultiSelect = false;
      this.gdv_alarm.Name = "gdv_alarm";
      this.gdv_alarm.RowsHeight = 32;
      this.gdv_alarm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_alarm.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_alarm.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.gdv_alarm.Size = new System.Drawing.Size(1057, 80);
      this.gdv_alarm.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_alarm.TabIndex = 133;
      this.gdv_alarm.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_alarm.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_alarm.TranslateText = true;
      this.gdv_alarm.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_alarm_CellFormatting);
      // 
      // tmt_End
      // 
      this.tmt_End.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.tmt_End.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tmt_End.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_End.ImagePassword = null;
      this.tmt_End.Location = new System.Drawing.Point(939, 636);
      this.tmt_End.Name = "tmt_End";
      this.tmt_End.PasswordChar = '\0';
      this.tmt_End.ReadOnly = true;
      this.tmt_End.Size = new System.Drawing.Size(211, 50);
      this.tmt_End.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.tmt_End.TabIndex = 148;
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
      this.tmt_Begin.Location = new System.Drawing.Point(939, 583);
      this.tmt_Begin.Name = "tmt_Begin";
      this.tmt_Begin.PasswordChar = '\0';
      this.tmt_Begin.ReadOnly = true;
      this.tmt_Begin.Size = new System.Drawing.Size(211, 50);
      this.tmt_Begin.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.tmt_Begin.TabIndex = 147;
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
      this.ttl_Code.Location = new System.Drawing.Point(939, 530);
      this.ttl_Code.Name = "ttl_Code";
      this.ttl_Code.Size = new System.Drawing.Size(211, 50);
      this.ttl_Code.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Code.TabIndex = 146;
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
      this.brk_AlarmDetails.Location = new System.Drawing.Point(907, 505);
      this.brk_AlarmDetails.Name = "brk_AlarmDetails";
      this.brk_AlarmDetails.Position = SyncroC.Controls.BracketPosition.Right;
      this.brk_AlarmDetails.ShowArrow = true;
      this.brk_AlarmDetails.Size = new System.Drawing.Size(243, 207);
      this.brk_AlarmDetails.TabIndex = 145;
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
      this.gdv_alarmHistory.Location = new System.Drawing.Point(92, 525);
      this.gdv_alarmHistory.MultiSelect = false;
      this.gdv_alarmHistory.Name = "gdv_alarmHistory";
      this.gdv_alarmHistory.RowsHeight = 32;
      this.gdv_alarmHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_alarmHistory.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_alarmHistory.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.gdv_alarmHistory.Size = new System.Drawing.Size(810, 187);
      this.gdv_alarmHistory.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_alarmHistory.TabIndex = 144;
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
      this.ttl_AlarmsHistory.Location = new System.Drawing.Point(83, 505);
      this.ttl_AlarmsHistory.Name = "ttl_AlarmsHistory";
      this.ttl_AlarmsHistory.Size = new System.Drawing.Size(822, 207);
      this.ttl_AlarmsHistory.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_AlarmsHistory.TabIndex = 143;
      this.ttl_AlarmsHistory.TabStop = false;
      this.ttl_AlarmsHistory.Text = "ALARMS\' HISTORY";
      this.ttl_AlarmsHistory.TranslateText = true;
      // 
      // ttl_CurrentProgram
      // 
      this.ttl_CurrentProgram.BackColor = System.Drawing.Color.Black;
      this.ttl_CurrentProgram.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentProgram.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentProgram.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_CurrentProgram.Location = new System.Drawing.Point(83, 297);
      this.ttl_CurrentProgram.Name = "ttl_CurrentProgram";
      this.ttl_CurrentProgram.Size = new System.Drawing.Size(350, 100);
      this.ttl_CurrentProgram.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_CurrentProgram.TabIndex = 149;
      this.ttl_CurrentProgram.Text = "CURRENT PROGRAM";
      this.ttl_CurrentProgram.TranslateText = false;
      // 
      // ttl_NextProgram
      // 
      this.ttl_NextProgram.BackColor = System.Drawing.Color.Black;
      this.ttl_NextProgram.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextProgram.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NextProgram.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_NextProgram.Location = new System.Drawing.Point(439, 297);
      this.ttl_NextProgram.Name = "ttl_NextProgram";
      this.ttl_NextProgram.Size = new System.Drawing.Size(350, 100);
      this.ttl_NextProgram.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_NextProgram.TabIndex = 150;
      this.ttl_NextProgram.Text = "NEXT PROGRAM";
      this.ttl_NextProgram.TranslateText = false;
      // 
      // stb_AutoMan
      // 
      this.stb_AutoMan.Active = false;
      this.stb_AutoMan.Alarmed = false;
      this.stb_AutoMan.Auto = false;
      this.stb_AutoMan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.stb_AutoMan.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.stb_AutoMan.Enabled = false;
      this.stb_AutoMan.FillBackColorWithImage = false;
      this.stb_AutoMan.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_AutoMan.FramePresent = false;
      this.stb_AutoMan.HighLightEnable = false;
      this.stb_AutoMan.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.stb_AutoMan.ImagePassword = null;
      this.stb_AutoMan.Inemergency = false;
      this.stb_AutoMan.InLocal = false;
      this.stb_AutoMan.IsButton = false;
      this.stb_AutoMan.IsForceToInvisible = false;
      this.stb_AutoMan.Location = new System.Drawing.Point(83, 193);
      this.stb_AutoMan.Name = "stb_AutoMan";
      this.stb_AutoMan.OnLine = false;
      this.stb_AutoMan.ShowImage = true;
      this.stb_AutoMan.ShowText = false;
      this.stb_AutoMan.Size = new System.Drawing.Size(100, 100);
      this.stb_AutoMan.Switching = false;
      this.stb_AutoMan.TabIndex = 218;
      this.stb_AutoMan.TranslateText = false;
      this.stb_AutoMan.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_AutoMan.UserDefinedActiveImage")));
      this.stb_AutoMan.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_AutoMan.UserDefinedImage")));
      this.stb_AutoMan.Warned = false;
      // 
      // ttl_manualSendButtons
      // 
      this.ttl_manualSendButtons.BackColor = System.Drawing.Color.Black;
      this.ttl_manualSendButtons.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_manualSendButtons.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_manualSendButtons.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_manualSendButtons.Location = new System.Drawing.Point(261, 193);
      this.ttl_manualSendButtons.Name = "ttl_manualSendButtons";
      this.ttl_manualSendButtons.Size = new System.Drawing.Size(889, 100);
      this.ttl_manualSendButtons.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_manualSendButtons.TabIndex = 222;
      this.ttl_manualSendButtons.Text = "MANUAL ORDER SENDING";
      this.ttl_manualSendButtons.TranslateText = true;
      // 
      // SendNextPlusOneProgram
      // 
      this.SendNextPlusOneProgram.Active = false;
      this.SendNextPlusOneProgram.Alarmed = false;
      this.SendNextPlusOneProgram.Auto = false;
      this.SendNextPlusOneProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.SendNextPlusOneProgram.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SendNextPlusOneProgram.FillBackColorWithImage = true;
      this.SendNextPlusOneProgram.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SendNextPlusOneProgram.ForeColor = System.Drawing.Color.Black;
      this.SendNextPlusOneProgram.FramePresent = true;
      this.SendNextPlusOneProgram.HighLightEnable = false;
      this.SendNextPlusOneProgram.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SendNextPlusOneProgram.ImagePassword = null;
      this.SendNextPlusOneProgram.Inemergency = false;
      this.SendNextPlusOneProgram.InLocal = false;
      this.SendNextPlusOneProgram.IsButton = true;
      this.SendNextPlusOneProgram.IsForceToInvisible = false;
      this.SendNextPlusOneProgram.Location = new System.Drawing.Point(800, 215);
      this.SendNextPlusOneProgram.Name = "SendNextPlusOneProgram";
      this.SendNextPlusOneProgram.OnLine = false;
      this.SendNextPlusOneProgram.ShowImage = true;
      this.SendNextPlusOneProgram.ShowText = true;
      this.SendNextPlusOneProgram.Size = new System.Drawing.Size(115, 76);
      this.SendNextPlusOneProgram.Switching = false;
      this.SendNextPlusOneProgram.TabIndex = 221;
      this.SendNextPlusOneProgram.Text = "3";
      this.SendNextPlusOneProgram.TranslateText = false;
      this.SendNextPlusOneProgram.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendNextPlusOneProgram.UserDefinedActiveImage")));
      this.SendNextPlusOneProgram.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendNextPlusOneProgram.UserDefinedImage")));
      this.SendNextPlusOneProgram.Warned = false;
      this.SendNextPlusOneProgram.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // SendNextProgram
      // 
      this.SendNextProgram.Active = false;
      this.SendNextProgram.Alarmed = false;
      this.SendNextProgram.Auto = false;
      this.SendNextProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.SendNextProgram.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SendNextProgram.FillBackColorWithImage = true;
      this.SendNextProgram.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SendNextProgram.ForeColor = System.Drawing.Color.Black;
      this.SendNextProgram.FramePresent = true;
      this.SendNextProgram.HighLightEnable = false;
      this.SendNextProgram.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SendNextProgram.ImagePassword = null;
      this.SendNextProgram.Inemergency = false;
      this.SendNextProgram.InLocal = false;
      this.SendNextProgram.IsButton = true;
      this.SendNextProgram.IsForceToInvisible = false;
      this.SendNextProgram.Location = new System.Drawing.Point(558, 217);
      this.SendNextProgram.Name = "SendNextProgram";
      this.SendNextProgram.OnLine = false;
      this.SendNextProgram.ShowImage = true;
      this.SendNextProgram.ShowText = true;
      this.SendNextProgram.Size = new System.Drawing.Size(117, 76);
      this.SendNextProgram.Switching = false;
      this.SendNextProgram.TabIndex = 220;
      this.SendNextProgram.Text = "2";
      this.SendNextProgram.TranslateText = false;
      this.SendNextProgram.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendNextProgram.UserDefinedActiveImage")));
      this.SendNextProgram.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendNextProgram.UserDefinedImage")));
      this.SendNextProgram.Warned = false;
      this.SendNextProgram.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // SendCurrentProgram
      // 
      this.SendCurrentProgram.Active = false;
      this.SendCurrentProgram.Alarmed = false;
      this.SendCurrentProgram.Auto = false;
      this.SendCurrentProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.SendCurrentProgram.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SendCurrentProgram.FillBackColorWithImage = true;
      this.SendCurrentProgram.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SendCurrentProgram.ForeColor = System.Drawing.Color.Black;
      this.SendCurrentProgram.FramePresent = true;
      this.SendCurrentProgram.HighLightEnable = false;
      this.SendCurrentProgram.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SendCurrentProgram.ImagePassword = null;
      this.SendCurrentProgram.Inemergency = false;
      this.SendCurrentProgram.InLocal = false;
      this.SendCurrentProgram.IsButton = true;
      this.SendCurrentProgram.IsForceToInvisible = false;
      this.SendCurrentProgram.Location = new System.Drawing.Point(307, 215);
      this.SendCurrentProgram.Name = "SendCurrentProgram";
      this.SendCurrentProgram.OnLine = false;
      this.SendCurrentProgram.ShowImage = true;
      this.SendCurrentProgram.ShowText = true;
      this.SendCurrentProgram.Size = new System.Drawing.Size(126, 76);
      this.SendCurrentProgram.Switching = false;
      this.SendCurrentProgram.TabIndex = 219;
      this.SendCurrentProgram.Text = "1";
      this.SendCurrentProgram.TranslateText = false;
      this.SendCurrentProgram.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendCurrentProgram.UserDefinedActiveImage")));
      this.SendCurrentProgram.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendCurrentProgram.UserDefinedImage")));
      this.SendCurrentProgram.Warned = false;
      this.SendCurrentProgram.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // ttl_NextPlusOneProgram
      // 
      this.ttl_NextPlusOneProgram.BackColor = System.Drawing.Color.Black;
      this.ttl_NextPlusOneProgram.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextPlusOneProgram.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NextPlusOneProgram.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_NextPlusOneProgram.Location = new System.Drawing.Point(800, 297);
      this.ttl_NextPlusOneProgram.Name = "ttl_NextPlusOneProgram";
      this.ttl_NextPlusOneProgram.Size = new System.Drawing.Size(350, 100);
      this.ttl_NextPlusOneProgram.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_NextPlusOneProgram.TabIndex = 223;
      this.ttl_NextPlusOneProgram.Text = "THIRD PROGRAM";
      this.ttl_NextPlusOneProgram.TranslateText = false;
      // 
      // WetShiftRequest
      // 
      this.WetShiftRequest.Active = false;
      this.WetShiftRequest.Alarmed = false;
      this.WetShiftRequest.Auto = false;
      this.WetShiftRequest.BackColor = System.Drawing.Color.WhiteSmoke;
      this.WetShiftRequest.Cursor = System.Windows.Forms.Cursors.Hand;
      this.WetShiftRequest.FillBackColorWithImage = false;
      this.WetShiftRequest.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.WetShiftRequest.ForeColor = System.Drawing.Color.Black;
      this.WetShiftRequest.FramePresent = true;
      this.WetShiftRequest.HighLightEnable = false;
      this.WetShiftRequest.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.WetShiftRequest.ImagePassword = null;
      this.WetShiftRequest.Inemergency = false;
      this.WetShiftRequest.InLocal = false;
      this.WetShiftRequest.IsButton = true;
      this.WetShiftRequest.IsForceToInvisible = false;
      this.WetShiftRequest.Location = new System.Drawing.Point(1023, 215);
      this.WetShiftRequest.Name = "WetShiftRequest";
      this.WetShiftRequest.OnLine = false;
      this.WetShiftRequest.ShowImage = false;
      this.WetShiftRequest.ShowText = true;
      this.WetShiftRequest.Size = new System.Drawing.Size(115, 76);
      this.WetShiftRequest.Switching = false;
      this.WetShiftRequest.TabIndex = 224;
      this.WetShiftRequest.Text = "Shift List";
      this.WetShiftRequest.TranslateText = true;
      this.WetShiftRequest.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("WetShiftRequest.UserDefinedActiveImage")));
      this.WetShiftRequest.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("WetShiftRequest.UserDefinedImage")));
      this.WetShiftRequest.Warned = false;
      this.WetShiftRequest.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // ttl_CurrentQuality
      // 
      this.ttl_CurrentQuality.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_CurrentQuality.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ttl_CurrentQuality.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentQuality.ImagePassword = null;
      this.ttl_CurrentQuality.Location = new System.Drawing.Point(90, 320);
      this.ttl_CurrentQuality.Name = "ttl_CurrentQuality";
      this.ttl_CurrentQuality.Size = new System.Drawing.Size(166, 77);
      this.ttl_CurrentQuality.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_CurrentQuality.TabIndex = 225;
      this.ttl_CurrentQuality.TabStop = false;
      this.ttl_CurrentQuality.Text = "Quality";
      this.ttl_CurrentQuality.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentQuality.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentQuality.TranslateText = true;
      this.ttl_CurrentQuality.Value = "";
      this.ttl_CurrentQuality.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentQuality.ValueMultiline = true;
      // 
      // ttl_CurrentLength
      // 
      this.ttl_CurrentLength.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_CurrentLength.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ttl_CurrentLength.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentLength.ImagePassword = null;
      this.ttl_CurrentLength.Location = new System.Drawing.Point(267, 320);
      this.ttl_CurrentLength.Name = "ttl_CurrentLength";
      this.ttl_CurrentLength.Size = new System.Drawing.Size(166, 77);
      this.ttl_CurrentLength.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_CurrentLength.TabIndex = 226;
      this.ttl_CurrentLength.TabStop = false;
      this.ttl_CurrentLength.Text = "Length";
      this.ttl_CurrentLength.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentLength.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentLength.TranslateText = true;
      this.ttl_CurrentLength.Value = "";
      this.ttl_CurrentLength.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentLength.ValueMultiline = true;
      // 
      // ttl_NextLength
      // 
      this.ttl_NextLength.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_NextLength.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ttl_NextLength.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextLength.ImagePassword = null;
      this.ttl_NextLength.Location = new System.Drawing.Point(623, 320);
      this.ttl_NextLength.Name = "ttl_NextLength";
      this.ttl_NextLength.Size = new System.Drawing.Size(166, 77);
      this.ttl_NextLength.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_NextLength.TabIndex = 228;
      this.ttl_NextLength.TabStop = false;
      this.ttl_NextLength.Text = "Length";
      this.ttl_NextLength.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextLength.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextLength.TranslateText = true;
      this.ttl_NextLength.Value = "";
      this.ttl_NextLength.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextLength.ValueMultiline = true;
      // 
      // ttl_NextQuality
      // 
      this.ttl_NextQuality.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_NextQuality.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ttl_NextQuality.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextQuality.ImagePassword = null;
      this.ttl_NextQuality.Location = new System.Drawing.Point(446, 320);
      this.ttl_NextQuality.Name = "ttl_NextQuality";
      this.ttl_NextQuality.Size = new System.Drawing.Size(166, 77);
      this.ttl_NextQuality.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_NextQuality.TabIndex = 227;
      this.ttl_NextQuality.TabStop = false;
      this.ttl_NextQuality.Text = "Quality";
      this.ttl_NextQuality.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextQuality.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextQuality.TranslateText = true;
      this.ttl_NextQuality.Value = "";
      this.ttl_NextQuality.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextQuality.ValueMultiline = true;
      // 
      // ttl_NextPlusOneLength
      // 
      this.ttl_NextPlusOneLength.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_NextPlusOneLength.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ttl_NextPlusOneLength.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextPlusOneLength.ImagePassword = null;
      this.ttl_NextPlusOneLength.Location = new System.Drawing.Point(984, 320);
      this.ttl_NextPlusOneLength.Name = "ttl_NextPlusOneLength";
      this.ttl_NextPlusOneLength.Size = new System.Drawing.Size(166, 77);
      this.ttl_NextPlusOneLength.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_NextPlusOneLength.TabIndex = 230;
      this.ttl_NextPlusOneLength.TabStop = false;
      this.ttl_NextPlusOneLength.Text = "Length";
      this.ttl_NextPlusOneLength.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextPlusOneLength.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextPlusOneLength.TranslateText = true;
      this.ttl_NextPlusOneLength.Value = "";
      this.ttl_NextPlusOneLength.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextPlusOneLength.ValueMultiline = true;
      // 
      // ttl_NextPlusOneQuality
      // 
      this.ttl_NextPlusOneQuality.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_NextPlusOneQuality.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ttl_NextPlusOneQuality.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextPlusOneQuality.ImagePassword = null;
      this.ttl_NextPlusOneQuality.Location = new System.Drawing.Point(807, 320);
      this.ttl_NextPlusOneQuality.Name = "ttl_NextPlusOneQuality";
      this.ttl_NextPlusOneQuality.Size = new System.Drawing.Size(166, 77);
      this.ttl_NextPlusOneQuality.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_NextPlusOneQuality.TabIndex = 229;
      this.ttl_NextPlusOneQuality.TabStop = false;
      this.ttl_NextPlusOneQuality.Text = "Quality";
      this.ttl_NextPlusOneQuality.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextPlusOneQuality.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextPlusOneQuality.TranslateText = true;
      this.ttl_NextPlusOneQuality.Value = "";
      this.ttl_NextPlusOneQuality.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NextPlusOneQuality.ValueMultiline = true;
      // 
      // Frm_DryEndSupervisor02
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.ttl_NextPlusOneLength);
      this.Controls.Add(this.ttl_NextPlusOneQuality);
      this.Controls.Add(this.ttl_NextLength);
      this.Controls.Add(this.ttl_NextQuality);
      this.Controls.Add(this.ttl_CurrentLength);
      this.Controls.Add(this.ttl_CurrentQuality);
      this.Controls.Add(this.WetShiftRequest);
      this.Controls.Add(this.ttl_NextPlusOneProgram);
      this.Controls.Add(this.ttl_manualSendButtons);
      this.Controls.Add(this.SendNextPlusOneProgram);
      this.Controls.Add(this.SendNextProgram);
      this.Controls.Add(this.SendCurrentProgram);
      this.Controls.Add(this.stb_AutoMan);
      this.Controls.Add(this.ttl_NextProgram);
      this.Controls.Add(this.ttl_CurrentProgram);
      this.Controls.Add(this.tmt_End);
      this.Controls.Add(this.tmt_Begin);
      this.Controls.Add(this.ttl_Code);
      this.Controls.Add(this.brk_AlarmDetails);
      this.Controls.Add(this.gdv_alarmHistory);
      this.Controls.Add(this.ttl_AlarmsHistory);
      this.Controls.Add(this.ttl_Alarms);
      this.Controls.Add(this.gdv_alarm);
      this.DesignGridHorizontalLines = 4;
      this.Name = "Frm_DryEndSupervisor02";
      this.Load += new System.EventHandler(this.Frm_DryEndSupervisor02_Load);
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
      this.Controls.SetChildIndex(this.ttl_CurrentProgram, 0);
      this.Controls.SetChildIndex(this.ttl_NextProgram, 0);
      this.Controls.SetChildIndex(this.stb_AutoMan, 0);
      this.Controls.SetChildIndex(this.SendCurrentProgram, 0);
      this.Controls.SetChildIndex(this.SendNextProgram, 0);
      this.Controls.SetChildIndex(this.SendNextPlusOneProgram, 0);
      this.Controls.SetChildIndex(this.ttl_manualSendButtons, 0);
      this.Controls.SetChildIndex(this.ttl_NextPlusOneProgram, 0);
      this.Controls.SetChildIndex(this.WetShiftRequest, 0);
      this.Controls.SetChildIndex(this.ttl_CurrentQuality, 0);
      this.Controls.SetChildIndex(this.ttl_CurrentLength, 0);
      this.Controls.SetChildIndex(this.ttl_NextQuality, 0);
      this.Controls.SetChildIndex(this.ttl_NextLength, 0);
      this.Controls.SetChildIndex(this.ttl_NextPlusOneQuality, 0);
      this.Controls.SetChildIndex(this.ttl_NextPlusOneLength, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		protected override void F2()
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

		/// <summary>
		/// Handles event raised by generic button that are in the page, 
		/// build the proper command and send it to the RTC_MANAGER.
		/// </summary>
		private void OnButtonPush(object sender, System.EventArgs e)
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				Command.EXTERNAL_WEC + "." + ((Control)sender).Name + "()");
		}

		private void _CommunicationError(bool isError)
		{
			this.SendCurrentProgram.Alarmed = isError;
			this.SendNextProgram.Alarmed = isError;
			this.SendNextPlusOneProgram.Alarmed = isError;
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

		private void Frm_DryEndSupervisor02_Load(object sender, EventArgs e)
		{
			this._CommunicationError(false);
			if (gdv_alarmHistory.Rows.Count > 0)
				gdv_alarmHistory.Rows[0].Selected = true;
		}
	}
}

