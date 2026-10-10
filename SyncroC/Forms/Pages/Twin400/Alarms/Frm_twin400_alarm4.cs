using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Globalization;

using SyncroC.DataBase.EventArgs;
using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.DataBase;
using SyncroC.Forms.PopUp;
using SyncroC.Controls;

namespace SyncroC.Forms.Pages.Twin400.Alarms
{
	public class Frm_twin400_alarm4 : SyncroC.Forms.Pages.Twin400.Alarms.Frm_twin400_alarmsSubgroup
	{
    //CM6 16-09-2005
		private SyncroC.Controls.Title ttl_AlarmsHistory;
		private System.ComponentModel.IContainer components = null;
		private SyncroC.Controls.GridView gdv_alarmHistory;

		private DataTable Twin400alarmHistory;
		private DataView dataView;

		//colonne che avrebbe dovuto fare il designer.
		private DataGridViewColumn clm_alarmHistory_Zone;
		private DataGridViewColumn clm_alarmHistory_Description;

		private SyncroC.Controls.Bracket brk_AlarmDetails;
		private SyncroC.Controls.CheckBox chk_OnBoardFilter;
		private SyncroC.Controls.CheckBox chk_LogicFilter;
		private SyncroC.Controls.CheckBox chk_PowerFilter;
		private SyncroC.Controls.CheckBox chk_HardwareFilter;
		private SyncroC.Controls.CheckBox chk_ConsoleFilter;
		private SyncroC.Controls.TitledMaskTextBox tmt_Begin;
		private SyncroC.Controls.TitledMaskTextBox tmt_End;
		private SyncroC.Controls.TitledList cmb_TimeFilter;
    private SyncroC.Controls.CheckBox chk_SoftwareFilter;
		private SyncroC.Controls.CheckBox chk_DrivesFilter;
		private Label lbl_filter;
    private SyncroC.Controls.CheckBox chk_DeviceNETFilter;
		private SyncroC.Controls.TitledLabel ttl_Code;
		
		public Frm_twin400_alarm4()
		{
			InitializeComponent();

			/**
			 * data view and data source initialization
			 */
			Twin400alarmHistory = DBManager.getDBManager().alarmHistory.alarmHistory.Tables[AlarmHistory.ALARM_HISTORY_LIST_SLITTERSCORER];
			dataView = new DataView(Twin400alarmHistory);
      dataView.Sort = AlarmHistory.BEGIN_TIME + " Desc";
			this.gdv_alarmHistory.DataSource = dataView;
      //AGrassi_FI08/11/2007_1
      this.gdv_alarmHistory.RowsHeight = 50;

		 /**
			* Filter Initialization
			*/
			string[] items = Enum.GetNames(typeof(TimeFilterOptions));
			for (int i = 0; i < items.Length; i++)
			{
				cmb_TimeFilter.AddItem(items[i]);
				//select the last one
				if (i == items.Length - 1)
					cmb_TimeFilter.SelectedIndex = i;
			}

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
			//clm_alarmHistory_Description.Width = 585;
      clm_alarmHistory_Description.Width = 689;
      //AGrassi_FI08/11/2007_1
      clm_alarmHistory_Description.CellTemplate.Style.WrapMode = DataGridViewTriState.True;

			gdv_alarmHistory.Columns.Add(clm_alarmHistory_Zone);
			gdv_alarmHistory.Columns.Add(clm_alarmHistory_Description);
		}

		private void Frm_twin400_alarm4_Load(object sender, EventArgs e)
		{
			if (gdv_alarmHistory.Rows.Count > 0)
				gdv_alarmHistory.Rows[0].Selected = true;
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
			this.gdv_alarmHistory.DataSource = null;
			base.Dispose( disposing );
		}

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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_alarm4));
      this.ttl_AlarmsHistory = new SyncroC.Controls.Title();
      this.gdv_alarmHistory = new SyncroC.Controls.GridView();
      this.brk_AlarmDetails = new SyncroC.Controls.Bracket();
      this.chk_OnBoardFilter = new SyncroC.Controls.CheckBox();
      this.chk_LogicFilter = new SyncroC.Controls.CheckBox();
      this.chk_PowerFilter = new SyncroC.Controls.CheckBox();
      this.chk_HardwareFilter = new SyncroC.Controls.CheckBox();
      this.chk_ConsoleFilter = new SyncroC.Controls.CheckBox();
      this.ttl_Code = new SyncroC.Controls.TitledLabel();
      this.tmt_Begin = new SyncroC.Controls.TitledMaskTextBox();
      this.tmt_End = new SyncroC.Controls.TitledMaskTextBox();
      this.cmb_TimeFilter = new SyncroC.Controls.TitledList();
      this.chk_SoftwareFilter = new SyncroC.Controls.CheckBox();
      this.chk_DrivesFilter = new SyncroC.Controls.CheckBox();
      this.lbl_filter = new System.Windows.Forms.Label();
      this.chk_DeviceNETFilter = new SyncroC.Controls.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      // 
      // lbl_Time
      // 
      this.lbl_Time.Margin = new System.Windows.Forms.Padding(1, 3, 3, 1);
      // 
      // lbl_Date
      // 
      this.lbl_Date.Margin = new System.Windows.Forms.Padding(1, 1, 3, 3);
      // 
      // lbl_Status
      // 
      this.lbl_Status.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      // 
      // tab_TabA
      // 
      this.tab_TabA.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      // 
      // tab_TabB
      // 
      this.tab_TabB.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.SelectedAlarm;
      // 
      // pct_Home_NY
      // 
      this.pct_Home_NY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // pct_Back_NY
      // 
      this.pct_Back_NY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // pct_Forward_NY
      // 
      this.pct_Forward_NY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF3
      // 
      this.lbl_CtrlF3.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF5
      // 
      this.lbl_CtrlF5.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      // 
      // lbl_CtrlF2
      // 
      this.lbl_CtrlF2.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF4
      // 
      this.lbl_CtrlF4.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
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
      // ttl_AlarmsHistory
      // 
      this.ttl_AlarmsHistory.BackColor = System.Drawing.Color.Black;
      this.ttl_AlarmsHistory.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_AlarmsHistory.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_AlarmsHistory.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_AlarmsHistory.Location = new System.Drawing.Point(30, 193);
      this.ttl_AlarmsHistory.Name = "ttl_AlarmsHistory";
      this.ttl_AlarmsHistory.Size = new System.Drawing.Size(926, 454);
      this.ttl_AlarmsHistory.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_AlarmsHistory.TabIndex = 0;
      this.ttl_AlarmsHistory.TabStop = false;
      this.ttl_AlarmsHistory.Text = "ALARMS\' HISTORY";
      this.ttl_AlarmsHistory.TranslateText = true;
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
      this.gdv_alarmHistory.Location = new System.Drawing.Point(42, 220);
      this.gdv_alarmHistory.MultiSelect = false;
      this.gdv_alarmHistory.Name = "gdv_alarmHistory";
      this.gdv_alarmHistory.RowsHeight = 32;
      this.gdv_alarmHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_alarmHistory.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_alarmHistory.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.gdv_alarmHistory.Size = new System.Drawing.Size(914, 427);
      this.gdv_alarmHistory.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_alarmHistory.TabIndex = 0;
      this.gdv_alarmHistory.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_alarmHistory.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_alarmHistory.TranslateText = true;
      this.gdv_alarmHistory.SelectionChanged += new System.EventHandler(this.gdv_alarmHistory_SelectionChanged);
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
      this.brk_AlarmDetails.Size = new System.Drawing.Size(239, 454);
      this.brk_AlarmDetails.TabIndex = 0;
      this.brk_AlarmDetails.TabStop = false;
      this.brk_AlarmDetails.Text = "DETAILS";
      this.brk_AlarmDetails.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.brk_AlarmDetails.TranslateText = true;
      // 
      // chk_OnBoardFilter
      // 
      this.chk_OnBoardFilter.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_OnBoardFilter.Checked = true;
      this.chk_OnBoardFilter.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_OnBoardFilter.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_OnBoardFilter.ImagePassword = null;
      this.chk_OnBoardFilter.Location = new System.Drawing.Point(660, 667);
      this.chk_OnBoardFilter.Name = "chk_OnBoardFilter";
      this.chk_OnBoardFilter.ReadOnly = false;
      this.chk_OnBoardFilter.Size = new System.Drawing.Size(103, 45);
      this.chk_OnBoardFilter.Style = SyncroC.Controls.CheckBoxStyles.Normal;
      this.chk_OnBoardFilter.TabIndex = 5;
      this.chk_OnBoardFilter.Text = "ON BOARD";
      this.chk_OnBoardFilter.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_OnBoardFilter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_OnBoardFilter.TranslateText = true;
      this.chk_OnBoardFilter.OnCheckedChanged += new System.EventHandler(this.setRowFilter);
      // 
      // chk_LogicFilter
      // 
      this.chk_LogicFilter.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_LogicFilter.Checked = true;
      this.chk_LogicFilter.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_LogicFilter.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_LogicFilter.ImagePassword = null;
      this.chk_LogicFilter.Location = new System.Drawing.Point(549, 667);
      this.chk_LogicFilter.Name = "chk_LogicFilter";
      this.chk_LogicFilter.ReadOnly = false;
      this.chk_LogicFilter.Size = new System.Drawing.Size(103, 45);
      this.chk_LogicFilter.Style = SyncroC.Controls.CheckBoxStyles.Normal;
      this.chk_LogicFilter.TabIndex = 4;
      this.chk_LogicFilter.Text = "LOGIC";
      this.chk_LogicFilter.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_LogicFilter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_LogicFilter.TranslateText = true;
      this.chk_LogicFilter.OnCheckedChanged += new System.EventHandler(this.setRowFilter);
      // 
      // chk_PowerFilter
      // 
      this.chk_PowerFilter.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_PowerFilter.Checked = true;
      this.chk_PowerFilter.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_PowerFilter.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_PowerFilter.ImagePassword = null;
      this.chk_PowerFilter.Location = new System.Drawing.Point(440, 667);
      this.chk_PowerFilter.Name = "chk_PowerFilter";
      this.chk_PowerFilter.ReadOnly = false;
      this.chk_PowerFilter.Size = new System.Drawing.Size(103, 45);
      this.chk_PowerFilter.Style = SyncroC.Controls.CheckBoxStyles.Normal;
      this.chk_PowerFilter.TabIndex = 3;
      this.chk_PowerFilter.Text = "POWER";
      this.chk_PowerFilter.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_PowerFilter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_PowerFilter.TranslateText = true;
      this.chk_PowerFilter.OnCheckedChanged += new System.EventHandler(this.setRowFilter);
      // 
      // chk_HardwareFilter
      // 
      this.chk_HardwareFilter.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_HardwareFilter.Checked = true;
      this.chk_HardwareFilter.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_HardwareFilter.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_HardwareFilter.ImagePassword = null;
      this.chk_HardwareFilter.Location = new System.Drawing.Point(331, 667);
      this.chk_HardwareFilter.Name = "chk_HardwareFilter";
      this.chk_HardwareFilter.ReadOnly = false;
      this.chk_HardwareFilter.Size = new System.Drawing.Size(103, 45);
      this.chk_HardwareFilter.Style = SyncroC.Controls.CheckBoxStyles.Normal;
      this.chk_HardwareFilter.TabIndex = 2;
      this.chk_HardwareFilter.Text = "HARDWARE";
      this.chk_HardwareFilter.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_HardwareFilter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_HardwareFilter.TranslateText = true;
      this.chk_HardwareFilter.OnCheckedChanged += new System.EventHandler(this.setRowFilter);
      // 
      // chk_ConsoleFilter
      // 
      this.chk_ConsoleFilter.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_ConsoleFilter.Checked = true;
      this.chk_ConsoleFilter.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_ConsoleFilter.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_ConsoleFilter.ImagePassword = null;
      this.chk_ConsoleFilter.Location = new System.Drawing.Point(769, 667);
      this.chk_ConsoleFilter.Name = "chk_ConsoleFilter";
      this.chk_ConsoleFilter.ReadOnly = false;
      this.chk_ConsoleFilter.Size = new System.Drawing.Size(103, 45);
      this.chk_ConsoleFilter.Style = SyncroC.Controls.CheckBoxStyles.Normal;
      this.chk_ConsoleFilter.TabIndex = 6;
      this.chk_ConsoleFilter.Text = "CONSOLE";
      this.chk_ConsoleFilter.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_ConsoleFilter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_ConsoleFilter.TranslateText = true;
      this.chk_ConsoleFilter.OnCheckedChanged += new System.EventHandler(this.setRowFilter);
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
      this.ttl_Code.TabIndex = 119;
      this.ttl_Code.TabStop = false;
      this.ttl_Code.Text = "Code";
      this.ttl_Code.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Code.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Code.TranslateText = true;
      this.ttl_Code.Value = "";
      this.ttl_Code.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Code.ValueMultiline = true;
      // 
      // tmt_Begin
      // 
      this.tmt_Begin.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.tmt_Begin.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tmt_Begin.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_Begin.ImagePassword = null;
      this.tmt_Begin.Location = new System.Drawing.Point(1003, 312);
      this.tmt_Begin.Name = "tmt_Begin";
      this.tmt_Begin.PasswordChar = '\0';
      this.tmt_Begin.ReadOnly = true;
      this.tmt_Begin.Size = new System.Drawing.Size(198, 52);
      this.tmt_Begin.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.tmt_Begin.TabIndex = 120;
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
      // tmt_End
      // 
      this.tmt_End.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.tmt_End.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tmt_End.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tmt_End.ImagePassword = null;
      this.tmt_End.Location = new System.Drawing.Point(1003, 398);
      this.tmt_End.Name = "tmt_End";
      this.tmt_End.PasswordChar = '\0';
      this.tmt_End.ReadOnly = true;
      this.tmt_End.Size = new System.Drawing.Size(198, 52);
      this.tmt_End.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.tmt_End.TabIndex = 121;
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
      // cmb_TimeFilter
      // 
      this.cmb_TimeFilter.BackColor = System.Drawing.Color.WhiteSmoke;
      this.cmb_TimeFilter.CanCancel = false;
      this.cmb_TimeFilter.Enabled = false;
      this.cmb_TimeFilter.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.cmb_TimeFilter.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_TimeFilter.ImagePassword = null;
      this.cmb_TimeFilter.Location = new System.Drawing.Point(30, 667);
      this.cmb_TimeFilter.Name = "cmb_TimeFilter";
      this.cmb_TimeFilter.ReadOnly = false;
      this.cmb_TimeFilter.SelectedIndex = -1;
      this.cmb_TimeFilter.SelectedItem = "";
      this.cmb_TimeFilter.SelectedItemData = "";
      this.cmb_TimeFilter.Size = new System.Drawing.Size(295, 45);
      this.cmb_TimeFilter.Sorted = false;
      this.cmb_TimeFilter.Style = SyncroC.Controls.TitledListStyles.Normal;
      this.cmb_TimeFilter.TabIndex = 1;
      this.cmb_TimeFilter.Text = "TIME FILTER";
      this.cmb_TimeFilter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_TimeFilter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_TimeFilter.TranslateText = true;
      this.cmb_TimeFilter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_TimeFilter.SelectedItemChanged += new System.EventHandler(this.setRowFilter);
      // 
      // chk_SoftwareFilter
      // 
      this.chk_SoftwareFilter.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SoftwareFilter.Checked = true;
      this.chk_SoftwareFilter.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_SoftwareFilter.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_SoftwareFilter.ImagePassword = null;
      this.chk_SoftwareFilter.Location = new System.Drawing.Point(1098, 667);
      this.chk_SoftwareFilter.Name = "chk_SoftwareFilter";
      this.chk_SoftwareFilter.ReadOnly = false;
      this.chk_SoftwareFilter.Size = new System.Drawing.Size(103, 45);
      this.chk_SoftwareFilter.Style = SyncroC.Controls.CheckBoxStyles.Normal;
      this.chk_SoftwareFilter.TabIndex = 122;
      this.chk_SoftwareFilter.Text = "SOFTWARE";
      this.chk_SoftwareFilter.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SoftwareFilter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_SoftwareFilter.TranslateText = true;
      this.chk_SoftwareFilter.OnCheckedChanged += new System.EventHandler(this.setRowFilter);
      // 
      // chk_DrivesFilter
      // 
      this.chk_DrivesFilter.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_DrivesFilter.Checked = true;
      this.chk_DrivesFilter.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_DrivesFilter.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_DrivesFilter.ImagePassword = null;
      this.chk_DrivesFilter.Location = new System.Drawing.Point(878, 667);
      this.chk_DrivesFilter.Name = "chk_DrivesFilter";
      this.chk_DrivesFilter.ReadOnly = false;
      this.chk_DrivesFilter.Size = new System.Drawing.Size(103, 45);
      this.chk_DrivesFilter.Style = SyncroC.Controls.CheckBoxStyles.Normal;
      this.chk_DrivesFilter.TabIndex = 124;
      this.chk_DrivesFilter.Text = "DRIVES";
      this.chk_DrivesFilter.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_DrivesFilter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_DrivesFilter.TranslateText = true;
      this.chk_DrivesFilter.OnCheckedChanged += new System.EventHandler(this.setRowFilter);
      // 
      // lbl_filter
      // 
      this.lbl_filter.Location = new System.Drawing.Point(108, 66);
      this.lbl_filter.Name = "lbl_filter";
      this.lbl_filter.Size = new System.Drawing.Size(1148, 29);
      this.lbl_filter.TabIndex = 125;
      this.lbl_filter.Visible = false;
      // 
      // chk_DeviceNETFilter
      // 
      this.chk_DeviceNETFilter.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_DeviceNETFilter.Checked = true;
      this.chk_DeviceNETFilter.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_DeviceNETFilter.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_DeviceNETFilter.ImagePassword = null;
      this.chk_DeviceNETFilter.Location = new System.Drawing.Point(987, 667);
      this.chk_DeviceNETFilter.Name = "chk_DeviceNETFilter";
      this.chk_DeviceNETFilter.ReadOnly = false;
      this.chk_DeviceNETFilter.Size = new System.Drawing.Size(103, 45);
      this.chk_DeviceNETFilter.Style = SyncroC.Controls.CheckBoxStyles.Normal;
      this.chk_DeviceNETFilter.TabIndex = 126;
      this.chk_DeviceNETFilter.Text = "DEVICENET";
      this.chk_DeviceNETFilter.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_DeviceNETFilter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_DeviceNETFilter.TranslateText = true;
      this.chk_DeviceNETFilter.OnCheckedChanged += new System.EventHandler(this.setRowFilter);
      // 
      // Frm_twin400_alarm4
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.chk_DeviceNETFilter);
      this.Controls.Add(this.lbl_filter);
      this.Controls.Add(this.chk_DrivesFilter);
      this.Controls.Add(this.chk_SoftwareFilter);
      this.Controls.Add(this.cmb_TimeFilter);
      this.Controls.Add(this.tmt_End);
      this.Controls.Add(this.tmt_Begin);
      this.Controls.Add(this.ttl_Code);
      this.Controls.Add(this.chk_ConsoleFilter);
      this.Controls.Add(this.chk_HardwareFilter);
      this.Controls.Add(this.chk_PowerFilter);
      this.Controls.Add(this.chk_LogicFilter);
      this.Controls.Add(this.chk_OnBoardFilter);
      this.Controls.Add(this.brk_AlarmDetails);
      this.Controls.Add(this.gdv_alarmHistory);
      this.Controls.Add(this.ttl_AlarmsHistory);
      this.Name = "Frm_twin400_alarm4";
      this.Tag = "";
      this.Text = "Frm_twin400_alarm4";
      this.Load += new System.EventHandler(this.Frm_twin400_alarm4_Load);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      this.Controls.SetChildIndex(this.lbl_Date, 0);
      this.Controls.SetChildIndex(this.pct_Home_NY, 0);
      this.Controls.SetChildIndex(this.pct_Back_NY, 0);
      this.Controls.SetChildIndex(this.pct_Forward_NY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
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
      this.Controls.SetChildIndex(this.btn_Group1, 0);
      this.Controls.SetChildIndex(this.btn_Group2, 0);
      this.Controls.SetChildIndex(this.btn_Group3, 0);
      this.Controls.SetChildIndex(this.lbl_Title_YN, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
      this.Controls.SetChildIndex(this.ttl_AlarmsHistory, 0);
      this.Controls.SetChildIndex(this.gdv_alarmHistory, 0);
      this.Controls.SetChildIndex(this.brk_AlarmDetails, 0);
      this.Controls.SetChildIndex(this.chk_OnBoardFilter, 0);
      this.Controls.SetChildIndex(this.chk_LogicFilter, 0);
      this.Controls.SetChildIndex(this.chk_PowerFilter, 0);
      this.Controls.SetChildIndex(this.chk_HardwareFilter, 0);
      this.Controls.SetChildIndex(this.chk_ConsoleFilter, 0);
      this.Controls.SetChildIndex(this.ttl_Code, 0);
      this.Controls.SetChildIndex(this.tmt_Begin, 0);
      this.Controls.SetChildIndex(this.tmt_End, 0);
      this.Controls.SetChildIndex(this.cmb_TimeFilter, 0);
      this.Controls.SetChildIndex(this.chk_SoftwareFilter, 0);
      this.Controls.SetChildIndex(this.chk_DrivesFilter, 0);
      this.Controls.SetChildIndex(this.lbl_filter, 0);
      this.Controls.SetChildIndex(this.chk_DeviceNETFilter, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

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

		/// <summary>
		/// Set the DataView rowfilter accordint to page filters current state
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setRowFilter(object sender, EventArgs e)
		{
			StringBuilder filter = new StringBuilder();

			//timeFilter
			TimeFilterOptions timeFilter = (TimeFilterOptions)Enum.Parse(typeof(TimeFilterOptions), cmb_TimeFilter.SelectedItem);
			if (!timeFilter.Equals(TimeFilterOptions.Always))
			{
				DateTime limit;
				if (filter.Length != 0)
					filter.Append(" AND ");
				filter.Append(AlarmHistory.END_TIME);
				filter.Append(" > #");
				switch (timeFilter)
				{
					case TimeFilterOptions.LastHour:
						limit = new DateTime(DateTime.Now.Ticks - TimeSpan.TicksPerHour);//last hour
						break;
					case TimeFilterOptions.LastTenMinutes:
						limit = new DateTime(DateTime.Now.Ticks - (TimeSpan.TicksPerMinute * 10));//last ten minutes
						break;
                    case TimeFilterOptions.LastDay:
                    default:
                        limit = new DateTime(DateTime.Now.Ticks - TimeSpan.TicksPerDay * 2);//last day
                        break;
				}
				//filter.Append(limit.ToString("dd/MM/yyyy HH:mm:ss", DateTimeFormatInfo.InvariantInfo));
//				filter.Append(limit.ToString("yyyy-MM-ddTHH:mm:ss", DateTimeFormatInfo.InvariantInfo));
				filter.Append(limit.ToString("yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo));
			//	filter.Append(limit.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo));
				filter.Append("#");
			}

			if (! chk_HardwareFilter.Checked)
			{//exclude Hardware zone
				if ( filter.Length != 0)
					filter.Append(" AND ");
				filter.Append(AlarmHistory.CODE_ZONE);
				filter.Append(" <> '");
				filter.Append(Alarm.Zone.twin400.Hardware);//hardware zone code
				filter.Append("'");
			}
			if (! chk_PowerFilter.Checked)
			{//exclude Power zone
				if (filter.Length != 0)
					filter.Append(" AND ");
				filter.Append(AlarmHistory.CODE_ZONE);
				filter.Append(" <> '");
				filter.Append(Alarm.Zone.twin400.PowerCabinet);//Power zone code
				filter.Append("'");
			} 
			if (! chk_LogicFilter.Checked)
			{//exclude Logic zone
				if (filter.Length != 0)
					filter.Append(" AND ");
				filter.Append(AlarmHistory.CODE_ZONE);
				filter.Append(" <> '");
				filter.Append(Alarm.Zone.twin400.LogicCabinet);//Logic zone code
				filter.Append("'");
			}
			if (! chk_OnBoardFilter.Checked)
			{//exclude OnBoard zone
				if (filter.Length != 0)
					filter.Append(" AND ");
				filter.Append(AlarmHistory.CODE_ZONE);
				filter.Append(" <> '");
				filter.Append(Alarm.Zone.twin400.BoardMachine);//OnBoard zone code
				filter.Append("'");
			}
			if (! chk_ConsoleFilter.Checked)
			{//Exclude console zone
				if (filter.Length != 0)
					filter.Append(" AND ");
				filter.Append(AlarmHistory.CODE_ZONE);
				filter.Append(" <> '");
				filter.Append(Alarm.Zone.twin400.Console);//Console zone code
				filter.Append("'");
			}
			if (!chk_DrivesFilter.Checked)
			{//Exclude drives zone
				if (filter.Length != 0)
					filter.Append(" AND ");
				filter.Append(AlarmHistory.CODE_ZONE);
				filter.Append(" <> '");
				filter.Append(Alarm.Zone.twin400.Drives);//Drives zone code
				filter.Append("'");
			}
			if (!chk_DeviceNETFilter.Checked)
      {//Exclude DeviceNET zone
				if (filter.Length != 0)
					filter.Append(" AND ");
				filter.Append(AlarmHistory.CODE_ZONE);
				filter.Append(" <> '");
        filter.Append(Alarm.Zone.twin400.DeviceNET);//DeviceNET zone code
				filter.Append("'");
			}
      //RFAZZI_FI26/01/2011_2 Aggiunta gestione Warning su grafica Twin400 di Syncro. 
      //if (!chk_WarningsFilter.Checked)
      //{//Exclude warnings zone
      if (filter.Length != 0)
        filter.Append(" AND ");
      filter.Append(AlarmHistory.CODE_ZONE);
      filter.Append(" <> '");
      filter.Append(Alarm.Zone.twin400.Warnings);//Warnings zone code
      filter.Append("'");
      //}
      if (!chk_SoftwareFilter.Checked)
			{//Exclude software zone
				if (filter.Length != 0)
					filter.Append(" AND ");
				filter.Append(AlarmHistory.CODE_ZONE);
				filter.Append(" <> '");
				filter.Append(Alarm.Zone.twin400.Software);//Software zone code
				filter.Append("'");
			}
			try
			{
				lbl_filter.Text = filter.ToString(); //string visible when debug on
				dataView.RowFilter = filter.ToString();
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "setRowFilter - the filter \"" + filter.ToString() + "\" contains an error", ex);
			}
		}

		protected override void ControlD()
		{
			base.ControlD();
			lbl_filter.Visible = !lbl_filter.Visible;
		}
	}
}

