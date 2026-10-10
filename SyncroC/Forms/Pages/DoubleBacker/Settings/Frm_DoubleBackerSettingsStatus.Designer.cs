namespace SyncroC.Forms.Pages.DoubleBacker.Settings
{
	partial class Frm_DoubleBackerSettingsStatus
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DoubleBackerSettingsStatus));
      this.grp_ZoneControl = new System.Windows.Forms.GroupBox();
      this.chk_SpeedSetpointStop = new SyncroC.Controls.CheckBox();
      this.ttl_SpeedCommand = new SyncroC.Controls.TitledLabel();
      this.tnud_ResumeSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_SpeedSetpoint = new SyncroC.Controls.TitledNumericUpDown();
      this.ttl_SpeedSetpointReason = new SyncroC.Controls.TitledLabel();
      this.ttl_ZoneModeStr = new SyncroC.Controls.TitledLabel();
      this.ttl_ZoneLocalModeStr = new SyncroC.Controls.TitledLabel();
      this.grp_ZoneMonitor = new System.Windows.Forms.GroupBox();
      this.chk_ZoneRunning = new SyncroC.Controls.CheckBox();
      this.tnud_ZoneSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_Production = new SyncroC.Controls.TitledNumericUpDown();
      this.grp_SingleFacerMonitor = new System.Windows.Forms.GroupBox();
      this.tnud_SFSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.chk_SFRunning = new SyncroC.Controls.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.grp_ZoneControl.SuspendLayout();
      this.grp_ZoneMonitor.SuspendLayout();
      this.grp_SingleFacerMonitor.SuspendLayout();
      this.SuspendLayout();
      // 
      // tab_TabC
      // 
      this.tab_TabC.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // grp_ZoneControl
      // 
      this.grp_ZoneControl.Controls.Add(this.chk_SpeedSetpointStop);
      this.grp_ZoneControl.Controls.Add(this.ttl_SpeedCommand);
      this.grp_ZoneControl.Controls.Add(this.tnud_ResumeSpeed);
      this.grp_ZoneControl.Controls.Add(this.tnud_SpeedSetpoint);
      this.grp_ZoneControl.Controls.Add(this.ttl_SpeedSetpointReason);
      this.grp_ZoneControl.Controls.Add(this.ttl_ZoneModeStr);
      this.grp_ZoneControl.Controls.Add(this.ttl_ZoneLocalModeStr);
      this.grp_ZoneControl.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grp_ZoneControl.Location = new System.Drawing.Point(38, 192);
      this.grp_ZoneControl.Name = "grp_ZoneControl";
      this.grp_ZoneControl.Size = new System.Drawing.Size(704, 256);
      this.grp_ZoneControl.TabIndex = 119;
      this.grp_ZoneControl.TabStop = false;
      this.grp_ZoneControl.Text = "Zone Control";
      // 
      // chk_SpeedSetpointStop
      // 
      this.chk_SpeedSetpointStop.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SpeedSetpointStop.Checked = true;
      this.chk_SpeedSetpointStop.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_SpeedSetpointStop.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_SpeedSetpointStop.ImagePassword = null;
      this.chk_SpeedSetpointStop.Location = new System.Drawing.Point(506, 98);
      this.chk_SpeedSetpointStop.Name = "chk_SpeedSetpointStop";
      this.chk_SpeedSetpointStop.ReadOnly = true;
      this.chk_SpeedSetpointStop.Size = new System.Drawing.Size(163, 64);
      this.chk_SpeedSetpointStop.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_SpeedSetpointStop.TabIndex = 124;
      this.chk_SpeedSetpointStop.Text = "Speed Setpoint Stop";
      this.chk_SpeedSetpointStop.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SpeedSetpointStop.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_SpeedSetpointStop.TranslateText = false;
      // 
      // ttl_SpeedCommand
      // 
      this.ttl_SpeedCommand.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SpeedCommand.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SpeedCommand.ImagePassword = null;
      this.ttl_SpeedCommand.Location = new System.Drawing.Point(6, 168);
      this.ttl_SpeedCommand.Name = "ttl_SpeedCommand";
      this.ttl_SpeedCommand.Size = new System.Drawing.Size(305, 64);
      this.ttl_SpeedCommand.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_SpeedCommand.TabIndex = 123;
      this.ttl_SpeedCommand.Text = "Speed Command";
      this.ttl_SpeedCommand.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_SpeedCommand.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SpeedCommand.TranslateText = false;
      this.ttl_SpeedCommand.Value = "";
      this.ttl_SpeedCommand.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_SpeedCommand.ValueMultiline = true;
      // 
      // tnud_ResumeSpeed
      // 
      this.tnud_ResumeSpeed.DecimalPlaces = 0;
      this.tnud_ResumeSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_ResumeSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_ResumeSpeed.ImagePassword = null;
      this.tnud_ResumeSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_ResumeSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_ResumeSpeed.Location = new System.Drawing.Point(323, 168);
      this.tnud_ResumeSpeed.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.tnud_ResumeSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_ResumeSpeed.Name = "tnud_ResumeSpeed";
      this.tnud_ResumeSpeed.ReadOnly = true;
      this.tnud_ResumeSpeed.Size = new System.Drawing.Size(165, 64);
      this.tnud_ResumeSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_ResumeSpeed.TabIndex = 122;
      this.tnud_ResumeSpeed.Text = "Resume Speed";
      this.tnud_ResumeSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ResumeSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_ResumeSpeed.TranslateText = false;
      this.tnud_ResumeSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_ResumeSpeed.Value = "100000";
      this.tnud_ResumeSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ResumeSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_SpeedSetpoint
      // 
      this.tnud_SpeedSetpoint.DecimalPlaces = 0;
      this.tnud_SpeedSetpoint.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_SpeedSetpoint.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_SpeedSetpoint.ImagePassword = null;
      this.tnud_SpeedSetpoint.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_SpeedSetpoint.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_SpeedSetpoint.Location = new System.Drawing.Point(323, 97);
      this.tnud_SpeedSetpoint.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.tnud_SpeedSetpoint.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_SpeedSetpoint.Name = "tnud_SpeedSetpoint";
      this.tnud_SpeedSetpoint.ReadOnly = true;
      this.tnud_SpeedSetpoint.Size = new System.Drawing.Size(165, 64);
      this.tnud_SpeedSetpoint.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_SpeedSetpoint.TabIndex = 121;
      this.tnud_SpeedSetpoint.Text = "Speed Setpoint";
      this.tnud_SpeedSetpoint.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SpeedSetpoint.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_SpeedSetpoint.TranslateText = false;
      this.tnud_SpeedSetpoint.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_SpeedSetpoint.Value = "100000";
      this.tnud_SpeedSetpoint.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SpeedSetpoint.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // ttl_SpeedSetpointReason
      // 
      this.ttl_SpeedSetpointReason.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SpeedSetpointReason.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SpeedSetpointReason.ImagePassword = null;
      this.ttl_SpeedSetpointReason.Location = new System.Drawing.Point(6, 97);
      this.ttl_SpeedSetpointReason.Name = "ttl_SpeedSetpointReason";
      this.ttl_SpeedSetpointReason.Size = new System.Drawing.Size(305, 64);
      this.ttl_SpeedSetpointReason.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_SpeedSetpointReason.TabIndex = 120;
      this.ttl_SpeedSetpointReason.Text = "Speed Setpoint Reason";
      this.ttl_SpeedSetpointReason.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_SpeedSetpointReason.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SpeedSetpointReason.TranslateText = false;
      this.ttl_SpeedSetpointReason.Value = "";
      this.ttl_SpeedSetpointReason.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_SpeedSetpointReason.ValueMultiline = true;
      // 
      // ttl_ZoneModeStr
      // 
      this.ttl_ZoneModeStr.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ZoneModeStr.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ZoneModeStr.ImagePassword = null;
      this.ttl_ZoneModeStr.Location = new System.Drawing.Point(323, 26);
      this.ttl_ZoneModeStr.Name = "ttl_ZoneModeStr";
      this.ttl_ZoneModeStr.Size = new System.Drawing.Size(305, 65);
      this.ttl_ZoneModeStr.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_ZoneModeStr.TabIndex = 119;
      this.ttl_ZoneModeStr.Text = "Mode";
      this.ttl_ZoneModeStr.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_ZoneModeStr.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ZoneModeStr.TranslateText = false;
      this.ttl_ZoneModeStr.Value = "";
      this.ttl_ZoneModeStr.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_ZoneModeStr.ValueMultiline = true;
      // 
      // ttl_ZoneLocalModeStr
      // 
      this.ttl_ZoneLocalModeStr.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ZoneLocalModeStr.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ZoneLocalModeStr.ImagePassword = null;
      this.ttl_ZoneLocalModeStr.Location = new System.Drawing.Point(6, 26);
      this.ttl_ZoneLocalModeStr.Name = "ttl_ZoneLocalModeStr";
      this.ttl_ZoneLocalModeStr.Size = new System.Drawing.Size(305, 65);
      this.ttl_ZoneLocalModeStr.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_ZoneLocalModeStr.TabIndex = 118;
      this.ttl_ZoneLocalModeStr.Text = "Local Mode";
      this.ttl_ZoneLocalModeStr.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_ZoneLocalModeStr.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ZoneLocalModeStr.TranslateText = false;
      this.ttl_ZoneLocalModeStr.Value = "";
      this.ttl_ZoneLocalModeStr.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_ZoneLocalModeStr.ValueMultiline = true;
      // 
      // grp_ZoneMonitor
      // 
      this.grp_ZoneMonitor.Controls.Add(this.chk_ZoneRunning);
      this.grp_ZoneMonitor.Controls.Add(this.tnud_ZoneSpeed);
      this.grp_ZoneMonitor.Controls.Add(this.tnud_Production);
      this.grp_ZoneMonitor.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grp_ZoneMonitor.Location = new System.Drawing.Point(276, 452);
      this.grp_ZoneMonitor.Name = "grp_ZoneMonitor";
      this.grp_ZoneMonitor.Size = new System.Drawing.Size(639, 245);
      this.grp_ZoneMonitor.TabIndex = 125;
      this.grp_ZoneMonitor.TabStop = false;
      this.grp_ZoneMonitor.Text = "Zone Monitor";
      // 
      // chk_ZoneRunning
      // 
      this.chk_ZoneRunning.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_ZoneRunning.Checked = true;
      this.chk_ZoneRunning.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_ZoneRunning.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_ZoneRunning.ImagePassword = null;
      this.chk_ZoneRunning.Location = new System.Drawing.Point(218, 29);
      this.chk_ZoneRunning.Name = "chk_ZoneRunning";
      this.chk_ZoneRunning.ReadOnly = true;
      this.chk_ZoneRunning.Size = new System.Drawing.Size(200, 64);
      this.chk_ZoneRunning.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_ZoneRunning.TabIndex = 127;
      this.chk_ZoneRunning.Text = "Running";
      this.chk_ZoneRunning.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_ZoneRunning.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_ZoneRunning.TranslateText = false;
      // 
      // tnud_ZoneSpeed
      // 
      this.tnud_ZoneSpeed.DecimalPlaces = 0;
      this.tnud_ZoneSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_ZoneSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_ZoneSpeed.ImagePassword = null;
      this.tnud_ZoneSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_ZoneSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_ZoneSpeed.Location = new System.Drawing.Point(6, 28);
      this.tnud_ZoneSpeed.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.tnud_ZoneSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_ZoneSpeed.Name = "tnud_ZoneSpeed";
      this.tnud_ZoneSpeed.ReadOnly = true;
      this.tnud_ZoneSpeed.Size = new System.Drawing.Size(200, 64);
      this.tnud_ZoneSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_ZoneSpeed.TabIndex = 125;
      this.tnud_ZoneSpeed.Text = "Speed";
      this.tnud_ZoneSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ZoneSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_ZoneSpeed.TranslateText = false;
      this.tnud_ZoneSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_ZoneSpeed.Value = "100000";
      this.tnud_ZoneSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ZoneSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_Production
      // 
      this.tnud_Production.DecimalPlaces = 0;
      this.tnud_Production.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_Production.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Production.ImagePassword = null;
      this.tnud_Production.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Production.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tnud_Production.Location = new System.Drawing.Point(6, 99);
      this.tnud_Production.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.tnud_Production.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_Production.Name = "tnud_Production";
      this.tnud_Production.ReadOnly = true;
      this.tnud_Production.Size = new System.Drawing.Size(200, 64);
      this.tnud_Production.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_Production.TabIndex = 126;
      this.tnud_Production.Text = "Production";
      this.tnud_Production.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Production.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Production.TranslateText = false;
      this.tnud_Production.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tnud_Production.Value = "0";
      this.tnud_Production.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Production.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // grp_SingleFacerMonitor
      // 
      this.grp_SingleFacerMonitor.Controls.Add(this.tnud_SFSpeed);
      this.grp_SingleFacerMonitor.Controls.Add(this.chk_SFRunning);
      this.grp_SingleFacerMonitor.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grp_SingleFacerMonitor.Location = new System.Drawing.Point(843, 192);
      this.grp_SingleFacerMonitor.Name = "grp_SingleFacerMonitor";
      this.grp_SingleFacerMonitor.Size = new System.Drawing.Size(357, 256);
      this.grp_SingleFacerMonitor.TabIndex = 126;
      this.grp_SingleFacerMonitor.TabStop = false;
      this.grp_SingleFacerMonitor.Text = "Single Facer Monitor";
      // 
      // tnud_SFSpeed
      // 
      this.tnud_SFSpeed.DecimalPlaces = 0;
      this.tnud_SFSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_SFSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_SFSpeed.ImagePassword = null;
      this.tnud_SFSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_SFSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_SFSpeed.Location = new System.Drawing.Point(181, 25);
      this.tnud_SFSpeed.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.tnud_SFSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_SFSpeed.Name = "tnud_SFSpeed";
      this.tnud_SFSpeed.ReadOnly = true;
      this.tnud_SFSpeed.Size = new System.Drawing.Size(161, 64);
      this.tnud_SFSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_SFSpeed.TabIndex = 116;
      this.tnud_SFSpeed.Text = "Speed";
      this.tnud_SFSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SFSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_SFSpeed.TranslateText = false;
      this.tnud_SFSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_SFSpeed.Value = "100000";
      this.tnud_SFSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SFSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // chk_SFRunning
      // 
      this.chk_SFRunning.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SFRunning.Checked = true;
      this.chk_SFRunning.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_SFRunning.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_SFRunning.ImagePassword = null;
      this.chk_SFRunning.Location = new System.Drawing.Point(6, 25);
      this.chk_SFRunning.Name = "chk_SFRunning";
      this.chk_SFRunning.ReadOnly = true;
      this.chk_SFRunning.Size = new System.Drawing.Size(161, 64);
      this.chk_SFRunning.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_SFRunning.TabIndex = 115;
      this.chk_SFRunning.Text = "Running";
      this.chk_SFRunning.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SFRunning.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_SFRunning.TranslateText = false;
      // 
      // Frm_DoubleBackerSettingsStatus
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.grp_SingleFacerMonitor);
      this.Controls.Add(this.grp_ZoneMonitor);
      this.Controls.Add(this.grp_ZoneControl);
      this.Name = "Frm_DoubleBackerSettingsStatus";
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
      this.Controls.SetChildIndex(this.grp_ZoneControl, 0);
      this.Controls.SetChildIndex(this.grp_ZoneMonitor, 0);
      this.Controls.SetChildIndex(this.grp_SingleFacerMonitor, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.grp_ZoneControl.ResumeLayout(false);
      this.grp_ZoneMonitor.ResumeLayout(false);
      this.grp_SingleFacerMonitor.ResumeLayout(false);
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grp_ZoneControl;
		private SyncroC.Controls.CheckBox chk_SpeedSetpointStop;
		private SyncroC.Controls.TitledLabel ttl_SpeedCommand;
		private SyncroC.Controls.TitledNumericUpDown tnud_ResumeSpeed;
		private SyncroC.Controls.TitledNumericUpDown tnud_SpeedSetpoint;
		private SyncroC.Controls.TitledLabel ttl_SpeedSetpointReason;
		private SyncroC.Controls.TitledLabel ttl_ZoneModeStr;
		private SyncroC.Controls.TitledLabel ttl_ZoneLocalModeStr;
		private System.Windows.Forms.GroupBox grp_ZoneMonitor;
		private SyncroC.Controls.CheckBox chk_ZoneRunning;
		private SyncroC.Controls.TitledNumericUpDown tnud_ZoneSpeed;
		private SyncroC.Controls.TitledNumericUpDown tnud_Production;
		private System.Windows.Forms.GroupBox grp_SingleFacerMonitor;
		private SyncroC.Controls.TitledNumericUpDown tnud_SFSpeed;
		private SyncroC.Controls.CheckBox chk_SFRunning;
	}
}
