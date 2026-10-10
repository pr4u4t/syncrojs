namespace SyncroC.Forms.Pages.DoubleBacker.Alarms
{
	partial class Frm_DoubleBackerAlarmsActive
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DoubleBackerAlarmsActive));
      this.ttl_DoubleBackerAlarms = new SyncroC.Controls.Title();
      this.gdv_alarm = new SyncroC.Controls.GridView();
      this.lbl_Key = new System.Windows.Forms.Label();
      this.lbl_Key_Description = new System.Windows.Forms.Label();
      this.lbl_Present = new System.Windows.Forms.Label();
      this.lbl_codeZone = new System.Windows.Forms.Label();
      this.lbl_present_Description = new System.Windows.Forms.Label();
      this.lbl_codeZone_Description = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.SelectedAlarm;
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.Text = "Change View";
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
      // ttl_DoubleBackerAlarms
      // 
      this.ttl_DoubleBackerAlarms.BackColor = System.Drawing.Color.Black;
      this.ttl_DoubleBackerAlarms.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_DoubleBackerAlarms.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_DoubleBackerAlarms.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_DoubleBackerAlarms.Location = new System.Drawing.Point(30, 194);
      this.ttl_DoubleBackerAlarms.Name = "ttl_DoubleBackerAlarms";
      this.ttl_DoubleBackerAlarms.Size = new System.Drawing.Size(1170, 519);
      this.ttl_DoubleBackerAlarms.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_DoubleBackerAlarms.TabIndex = 130;
      this.ttl_DoubleBackerAlarms.Text = "Double Backer Alarms";
      this.ttl_DoubleBackerAlarms.TranslateText = true;
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
      this.gdv_alarm.Location = new System.Drawing.Point(40, 222);
      this.gdv_alarm.MultiSelect = false;
      this.gdv_alarm.Name = "gdv_alarm";
      this.gdv_alarm.RowsHeight = 32;
      this.gdv_alarm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_alarm.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_alarm.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
      this.gdv_alarm.Size = new System.Drawing.Size(1160, 491);
      this.gdv_alarm.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_alarm.TabIndex = 129;
      this.gdv_alarm.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_alarm.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_alarm.TranslateText = true;
      this.gdv_alarm.SelectionChanged += new System.EventHandler(this.gdv_alarm_SelectionChanged);
      this.gdv_alarm.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_alarm_CellFormatting);
      // 
      // lbl_Key
      // 
      this.lbl_Key.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Key.Location = new System.Drawing.Point(396, 57);
      this.lbl_Key.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
      this.lbl_Key.Name = "lbl_Key";
      this.lbl_Key.Size = new System.Drawing.Size(269, 23);
      this.lbl_Key.TabIndex = 144;
      this.lbl_Key.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_Key.Visible = false;
      // 
      // lbl_Key_Description
      // 
      this.lbl_Key_Description.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Key_Description.Location = new System.Drawing.Point(331, 57);
      this.lbl_Key_Description.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.lbl_Key_Description.Name = "lbl_Key_Description";
      this.lbl_Key_Description.Size = new System.Drawing.Size(65, 23);
      this.lbl_Key_Description.TabIndex = 143;
      this.lbl_Key_Description.Text = "Key:";
      this.lbl_Key_Description.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_Key_Description.Visible = false;
      // 
      // lbl_Present
      // 
      this.lbl_Present.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Present.Location = new System.Drawing.Point(725, 57);
      this.lbl_Present.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
      this.lbl_Present.Name = "lbl_Present";
      this.lbl_Present.Size = new System.Drawing.Size(68, 23);
      this.lbl_Present.TabIndex = 142;
      this.lbl_Present.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_Present.Visible = false;
      // 
      // lbl_codeZone
      // 
      this.lbl_codeZone.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_codeZone.Location = new System.Drawing.Point(177, 57);
      this.lbl_codeZone.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
      this.lbl_codeZone.Name = "lbl_codeZone";
      this.lbl_codeZone.Size = new System.Drawing.Size(148, 23);
      this.lbl_codeZone.TabIndex = 141;
      this.lbl_codeZone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_codeZone.Visible = false;
      // 
      // lbl_present_Description
      // 
      this.lbl_present_Description.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_present_Description.Location = new System.Drawing.Point(671, 57);
      this.lbl_present_Description.Name = "lbl_present_Description";
      this.lbl_present_Description.Size = new System.Drawing.Size(54, 23);
      this.lbl_present_Description.TabIndex = 140;
      this.lbl_present_Description.Text = "Present:";
      this.lbl_present_Description.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_present_Description.Visible = false;
      // 
      // lbl_codeZone_Description
      // 
      this.lbl_codeZone_Description.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_codeZone_Description.Location = new System.Drawing.Point(112, 57);
      this.lbl_codeZone_Description.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.lbl_codeZone_Description.Name = "lbl_codeZone_Description";
      this.lbl_codeZone_Description.Size = new System.Drawing.Size(65, 23);
      this.lbl_codeZone_Description.TabIndex = 139;
      this.lbl_codeZone_Description.Text = "Code Zone:";
      this.lbl_codeZone_Description.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_codeZone_Description.Visible = false;
      // 
      // Frm_DoubleBackerAlarmsActive
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.lbl_Key);
      this.Controls.Add(this.lbl_Key_Description);
      this.Controls.Add(this.lbl_Present);
      this.Controls.Add(this.lbl_codeZone);
      this.Controls.Add(this.lbl_present_Description);
      this.Controls.Add(this.lbl_codeZone_Description);
      this.Controls.Add(this.ttl_DoubleBackerAlarms);
      this.Controls.Add(this.gdv_alarm);
      this.Name = "Frm_DoubleBackerAlarmsActive";
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
      this.Controls.SetChildIndex(this.ttl_DoubleBackerAlarms, 0);
      this.Controls.SetChildIndex(this.lbl_codeZone_Description, 0);
      this.Controls.SetChildIndex(this.lbl_present_Description, 0);
      this.Controls.SetChildIndex(this.lbl_codeZone, 0);
      this.Controls.SetChildIndex(this.lbl_Present, 0);
      this.Controls.SetChildIndex(this.lbl_Key_Description, 0);
      this.Controls.SetChildIndex(this.lbl_Key, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}

		#endregion

		private SyncroC.Controls.Title ttl_DoubleBackerAlarms;
		private SyncroC.Controls.GridView gdv_alarm;
		private System.Windows.Forms.Label lbl_Key;
		private System.Windows.Forms.Label lbl_Key_Description;
		private System.Windows.Forms.Label lbl_Present;
		private System.Windows.Forms.Label lbl_codeZone;
		private System.Windows.Forms.Label lbl_present_Description;
		private System.Windows.Forms.Label lbl_codeZone_Description;
	}
}
