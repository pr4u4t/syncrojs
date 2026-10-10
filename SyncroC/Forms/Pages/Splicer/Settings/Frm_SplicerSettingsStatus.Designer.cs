namespace SyncroC.Forms.Pages.Splicer.Settings
{
	partial class Frm_SplicerSettingsStatus
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SplicerSettingsStatus));
      this.grp_SplicerControl = new System.Windows.Forms.GroupBox();
      this.ToggleAutoMan = new SyncroC.Controls.StateButton();
      this.tnud_BridgeRequestedPrefill_UDmm = new SyncroC.Controls.TitledNumericUpDown();
      this.ttl_RequestUnit = new SyncroC.Controls.TitledLabel();
      this.ttl_SplicerModeStr = new SyncroC.Controls.TitledLabel();
      this.ttl_SplicerLocalModeStr = new SyncroC.Controls.TitledLabel();
      this.grp_SplicerMonitor = new System.Windows.Forms.GroupBox();
      this.ttl_RollRunning = new SyncroC.Controls.TitledLabel();
      this.tnud_LastCycle_UTs = new SyncroC.Controls.TitledNumericUpDown();
      this.chk_NotReadyAtEarlyWarning = new SyncroC.Controls.CheckBox();
      this.tnud_Speed = new SyncroC.Controls.TitledNumericUpDown();
      this.chk_ReadyToSplice = new SyncroC.Controls.CheckBox();
      this.grp_CurrentRoll = new System.Windows.Forms.GroupBox();
      this.ttl_PaperTypeCurrent = new SyncroC.Controls.TitledLabel();
      this.ttl_CurrentThickness = new SyncroC.Controls.TitledNumericUpDown();
      this.ttl_CurrentRemaining = new SyncroC.Controls.TitledNumericUpDown();
      this.ttl_CurrentDiameter = new SyncroC.Controls.TitledNumericUpDown();
      this.grp_SplicerAlarm = new System.Windows.Forms.GroupBox();
      this.chk_PaperBreak = new SyncroC.Controls.CheckBox();
      this.chk_CommsAlarm = new SyncroC.Controls.CheckBox();
      this.ttl_PreviousRemaining = new SyncroC.Controls.TitledNumericUpDown();
      this.ttl_PreviousDiameter = new SyncroC.Controls.TitledNumericUpDown();
      this.grp_PreviousRoll = new System.Windows.Forms.GroupBox();
      this.ttl_PaperTypePrevious = new SyncroC.Controls.TitledLabel();
      this.ttl_PreviousThickness = new SyncroC.Controls.TitledNumericUpDown();
      this.grp_CtrlData = new System.Windows.Forms.GroupBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.grp_SplicerControl.SuspendLayout();
      this.grp_SplicerMonitor.SuspendLayout();
      this.grp_CurrentRoll.SuspendLayout();
      this.grp_SplicerAlarm.SuspendLayout();
      this.grp_PreviousRoll.SuspendLayout();
      this.grp_CtrlData.SuspendLayout();
      this.SuspendLayout();
      // 
      // tab_TabC
      // 
      this.tab_TabC.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // grp_SplicerControl
      // 
      this.grp_SplicerControl.Controls.Add(this.ToggleAutoMan);
      this.grp_SplicerControl.Controls.Add(this.tnud_BridgeRequestedPrefill_UDmm);
      this.grp_SplicerControl.Controls.Add(this.ttl_RequestUnit);
      this.grp_SplicerControl.Controls.Add(this.ttl_SplicerModeStr);
      this.grp_SplicerControl.Controls.Add(this.ttl_SplicerLocalModeStr);
      this.grp_SplicerControl.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grp_SplicerControl.ForeColor = System.Drawing.Color.Black;
      this.grp_SplicerControl.Location = new System.Drawing.Point(80, 183);
      this.grp_SplicerControl.Name = "grp_SplicerControl";
      this.grp_SplicerControl.Size = new System.Drawing.Size(704, 176);
      this.grp_SplicerControl.TabIndex = 119;
      this.grp_SplicerControl.TabStop = false;
      this.grp_SplicerControl.Text = "Splicer Control";
      // 
      // ToggleAutoMan
      // 
      this.ToggleAutoMan.Active = false;
      this.ToggleAutoMan.Alarmed = false;
      this.ToggleAutoMan.Auto = false;
      this.ToggleAutoMan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.ToggleAutoMan.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ToggleAutoMan.FillBackColorWithImage = false;
      this.ToggleAutoMan.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ToggleAutoMan.FramePresent = false;
      this.ToggleAutoMan.HighLightEnable = false;
      this.ToggleAutoMan.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.ToggleAutoMan.ImagePassword = null;
      this.ToggleAutoMan.Inemergency = false;
      this.ToggleAutoMan.InLocal = false;
      this.ToggleAutoMan.IsButton = true;
      this.ToggleAutoMan.IsForceToInvisible = false;
      this.ToggleAutoMan.Location = new System.Drawing.Point(633, 26);
      this.ToggleAutoMan.Name = "ToggleAutoMan";
      this.ToggleAutoMan.OnLine = false;
      this.ToggleAutoMan.ShowImage = true;
      this.ToggleAutoMan.ShowText = false;
      this.ToggleAutoMan.Size = new System.Drawing.Size(65, 65);
      this.ToggleAutoMan.Switching = false;
      this.ToggleAutoMan.TabIndex = 384;
      this.ToggleAutoMan.TranslateText = false;
      this.ToggleAutoMan.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoMan.UserDefinedActiveImage")));
      this.ToggleAutoMan.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ToggleAutoMan.UserDefinedImage")));
      this.ToggleAutoMan.Warned = false;
      this.ToggleAutoMan.Click += new System.EventHandler(this.ToggleAutoMan_Click);
      // 
      // tnud_BridgeRequestedPrefill_UDmm
      // 
      this.tnud_BridgeRequestedPrefill_UDmm.DecimalPlaces = 0;
      this.tnud_BridgeRequestedPrefill_UDmm.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_BridgeRequestedPrefill_UDmm.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_BridgeRequestedPrefill_UDmm.ImagePassword = null;
      this.tnud_BridgeRequestedPrefill_UDmm.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_BridgeRequestedPrefill_UDmm.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tnud_BridgeRequestedPrefill_UDmm.Location = new System.Drawing.Point(323, 97);
      this.tnud_BridgeRequestedPrefill_UDmm.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.tnud_BridgeRequestedPrefill_UDmm.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_BridgeRequestedPrefill_UDmm.Name = "tnud_BridgeRequestedPrefill_UDmm";
      this.tnud_BridgeRequestedPrefill_UDmm.ReadOnly = true;
      this.tnud_BridgeRequestedPrefill_UDmm.Size = new System.Drawing.Size(305, 64);
      this.tnud_BridgeRequestedPrefill_UDmm.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_BridgeRequestedPrefill_UDmm.TabIndex = 121;
      this.tnud_BridgeRequestedPrefill_UDmm.Text = "Bridge Requested Prefill UD_mm";
      this.tnud_BridgeRequestedPrefill_UDmm.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_BridgeRequestedPrefill_UDmm.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_BridgeRequestedPrefill_UDmm.TranslateText = false;
      this.tnud_BridgeRequestedPrefill_UDmm.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tnud_BridgeRequestedPrefill_UDmm.Value = "1";
      this.tnud_BridgeRequestedPrefill_UDmm.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_BridgeRequestedPrefill_UDmm.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // ttl_RequestUnit
      // 
      this.ttl_RequestUnit.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_RequestUnit.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_RequestUnit.ImagePassword = null;
      this.ttl_RequestUnit.Location = new System.Drawing.Point(6, 97);
      this.ttl_RequestUnit.Name = "ttl_RequestUnit";
      this.ttl_RequestUnit.Size = new System.Drawing.Size(305, 64);
      this.ttl_RequestUnit.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_RequestUnit.TabIndex = 120;
      this.ttl_RequestUnit.Text = "Request Unit";
      this.ttl_RequestUnit.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_RequestUnit.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_RequestUnit.TranslateText = false;
      this.ttl_RequestUnit.Value = "";
      this.ttl_RequestUnit.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_RequestUnit.ValueMultiline = true;
      // 
      // ttl_SplicerModeStr
      // 
      this.ttl_SplicerModeStr.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SplicerModeStr.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SplicerModeStr.ImagePassword = null;
      this.ttl_SplicerModeStr.Location = new System.Drawing.Point(323, 26);
      this.ttl_SplicerModeStr.Name = "ttl_SplicerModeStr";
      this.ttl_SplicerModeStr.Size = new System.Drawing.Size(305, 65);
      this.ttl_SplicerModeStr.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_SplicerModeStr.TabIndex = 119;
      this.ttl_SplicerModeStr.Text = "Mode";
      this.ttl_SplicerModeStr.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_SplicerModeStr.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SplicerModeStr.TranslateText = false;
      this.ttl_SplicerModeStr.Value = "";
      this.ttl_SplicerModeStr.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_SplicerModeStr.ValueMultiline = true;
      // 
      // ttl_SplicerLocalModeStr
      // 
      this.ttl_SplicerLocalModeStr.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SplicerLocalModeStr.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SplicerLocalModeStr.ImagePassword = null;
      this.ttl_SplicerLocalModeStr.Location = new System.Drawing.Point(6, 26);
      this.ttl_SplicerLocalModeStr.Name = "ttl_SplicerLocalModeStr";
      this.ttl_SplicerLocalModeStr.Size = new System.Drawing.Size(305, 65);
      this.ttl_SplicerLocalModeStr.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_SplicerLocalModeStr.TabIndex = 118;
      this.ttl_SplicerLocalModeStr.Text = "Local Mode";
      this.ttl_SplicerLocalModeStr.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_SplicerLocalModeStr.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SplicerLocalModeStr.TranslateText = false;
      this.ttl_SplicerLocalModeStr.Value = "";
      this.ttl_SplicerLocalModeStr.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_SplicerLocalModeStr.ValueMultiline = true;
      // 
      // grp_SplicerMonitor
      // 
      this.grp_SplicerMonitor.Controls.Add(this.ttl_RollRunning);
      this.grp_SplicerMonitor.Controls.Add(this.tnud_LastCycle_UTs);
      this.grp_SplicerMonitor.Controls.Add(this.chk_NotReadyAtEarlyWarning);
      this.grp_SplicerMonitor.Controls.Add(this.tnud_Speed);
      this.grp_SplicerMonitor.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grp_SplicerMonitor.ForeColor = System.Drawing.Color.Black;
      this.grp_SplicerMonitor.Location = new System.Drawing.Point(80, 365);
      this.grp_SplicerMonitor.Name = "grp_SplicerMonitor";
      this.grp_SplicerMonitor.Size = new System.Drawing.Size(442, 350);
      this.grp_SplicerMonitor.TabIndex = 120;
      this.grp_SplicerMonitor.TabStop = false;
      this.grp_SplicerMonitor.Text = "Splicer Monitor";
      // 
      // ttl_RollRunning
      // 
      this.ttl_RollRunning.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_RollRunning.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_RollRunning.ImagePassword = null;
      this.ttl_RollRunning.Location = new System.Drawing.Point(249, 112);
      this.ttl_RollRunning.Name = "ttl_RollRunning";
      this.ttl_RollRunning.Size = new System.Drawing.Size(154, 64);
      this.ttl_RollRunning.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_RollRunning.TabIndex = 132;
      this.ttl_RollRunning.Text = "Roll Running";
      this.ttl_RollRunning.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_RollRunning.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_RollRunning.TranslateText = false;
      this.ttl_RollRunning.Value = "A";
      this.ttl_RollRunning.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_RollRunning.ValueMultiline = true;
      // 
      // tnud_LastCycle_UTs
      // 
      this.tnud_LastCycle_UTs.DecimalPlaces = 0;
      this.tnud_LastCycle_UTs.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_LastCycle_UTs.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_LastCycle_UTs.ImagePassword = null;
      this.tnud_LastCycle_UTs.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_LastCycle_UTs.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliSecond;
      this.tnud_LastCycle_UTs.Location = new System.Drawing.Point(249, 28);
      this.tnud_LastCycle_UTs.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.tnud_LastCycle_UTs.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_LastCycle_UTs.Name = "tnud_LastCycle_UTs";
      this.tnud_LastCycle_UTs.ReadOnly = true;
      this.tnud_LastCycle_UTs.Size = new System.Drawing.Size(149, 64);
      this.tnud_LastCycle_UTs.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_LastCycle_UTs.TabIndex = 131;
      this.tnud_LastCycle_UTs.Text = "Last Cycle of Splice";
      this.tnud_LastCycle_UTs.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LastCycle_UTs.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_LastCycle_UTs.TranslateText = false;
      this.tnud_LastCycle_UTs.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliSecond;
      this.tnud_LastCycle_UTs.Value = "10000";
      this.tnud_LastCycle_UTs.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LastCycle_UTs.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // chk_NotReadyAtEarlyWarning
      // 
      this.chk_NotReadyAtEarlyWarning.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_NotReadyAtEarlyWarning.Checked = true;
      this.chk_NotReadyAtEarlyWarning.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_NotReadyAtEarlyWarning.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_NotReadyAtEarlyWarning.ImagePassword = null;
      this.chk_NotReadyAtEarlyWarning.Location = new System.Drawing.Point(6, 112);
      this.chk_NotReadyAtEarlyWarning.Name = "chk_NotReadyAtEarlyWarning";
      this.chk_NotReadyAtEarlyWarning.ReadOnly = true;
      this.chk_NotReadyAtEarlyWarning.Size = new System.Drawing.Size(200, 64);
      this.chk_NotReadyAtEarlyWarning.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_NotReadyAtEarlyWarning.TabIndex = 130;
      this.chk_NotReadyAtEarlyWarning.Text = "Not Ready At Early Warning";
      this.chk_NotReadyAtEarlyWarning.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_NotReadyAtEarlyWarning.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_NotReadyAtEarlyWarning.TranslateText = false;
      // 
      // tnud_Speed
      // 
      this.tnud_Speed.DecimalPlaces = 0;
      this.tnud_Speed.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_Speed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Speed.ImagePassword = null;
      this.tnud_Speed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Speed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_Speed.Location = new System.Drawing.Point(6, 28);
      this.tnud_Speed.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.tnud_Speed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_Speed.Name = "tnud_Speed";
      this.tnud_Speed.ReadOnly = true;
      this.tnud_Speed.Size = new System.Drawing.Size(200, 64);
      this.tnud_Speed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_Speed.TabIndex = 125;
      this.tnud_Speed.Text = "Speed";
      this.tnud_Speed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Speed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Speed.TranslateText = false;
      this.tnud_Speed.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_Speed.Value = "100000";
      this.tnud_Speed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Speed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // chk_ReadyToSplice
      // 
      this.chk_ReadyToSplice.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_ReadyToSplice.Checked = true;
      this.chk_ReadyToSplice.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_ReadyToSplice.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_ReadyToSplice.ImagePassword = null;
      this.chk_ReadyToSplice.Location = new System.Drawing.Point(11, 37);
      this.chk_ReadyToSplice.Name = "chk_ReadyToSplice";
      this.chk_ReadyToSplice.ReadOnly = true;
      this.chk_ReadyToSplice.Size = new System.Drawing.Size(200, 64);
      this.chk_ReadyToSplice.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_ReadyToSplice.TabIndex = 127;
      this.chk_ReadyToSplice.Text = "Ready To Splice";
      this.chk_ReadyToSplice.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_ReadyToSplice.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_ReadyToSplice.TranslateText = false;
      // 
      // grp_CurrentRoll
      // 
      this.grp_CurrentRoll.Controls.Add(this.ttl_PaperTypeCurrent);
      this.grp_CurrentRoll.Controls.Add(this.ttl_CurrentThickness);
      this.grp_CurrentRoll.Controls.Add(this.ttl_CurrentRemaining);
      this.grp_CurrentRoll.Controls.Add(this.ttl_CurrentDiameter);
      this.grp_CurrentRoll.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grp_CurrentRoll.ForeColor = System.Drawing.Color.Black;
      this.grp_CurrentRoll.Location = new System.Drawing.Point(794, 465);
      this.grp_CurrentRoll.Name = "grp_CurrentRoll";
      this.grp_CurrentRoll.Size = new System.Drawing.Size(359, 250);
      this.grp_CurrentRoll.TabIndex = 128;
      this.grp_CurrentRoll.TabStop = false;
      this.grp_CurrentRoll.Text = "Current Roll";
      // 
      // ttl_PaperTypeCurrent
      // 
      this.ttl_PaperTypeCurrent.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_PaperTypeCurrent.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PaperTypeCurrent.ImagePassword = null;
      this.ttl_PaperTypeCurrent.Location = new System.Drawing.Point(8, 28);
      this.ttl_PaperTypeCurrent.Name = "ttl_PaperTypeCurrent";
      this.ttl_PaperTypeCurrent.Size = new System.Drawing.Size(346, 65);
      this.ttl_PaperTypeCurrent.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_PaperTypeCurrent.TabIndex = 129;
      this.ttl_PaperTypeCurrent.Text = "PaperType + WeightFactor_UWg_m";
      this.ttl_PaperTypeCurrent.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_PaperTypeCurrent.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PaperTypeCurrent.TranslateText = false;
      this.ttl_PaperTypeCurrent.Value = "";
      this.ttl_PaperTypeCurrent.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_PaperTypeCurrent.ValueMultiline = true;
      // 
      // ttl_CurrentThickness
      // 
      this.ttl_CurrentThickness.DecimalPlaces = 0;
      this.ttl_CurrentThickness.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentThickness.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentThickness.ImagePassword = null;
      this.ttl_CurrentThickness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.ttl_CurrentThickness.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMicron;
      this.ttl_CurrentThickness.Location = new System.Drawing.Point(7, 99);
      this.ttl_CurrentThickness.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.ttl_CurrentThickness.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.ttl_CurrentThickness.Name = "ttl_CurrentThickness";
      this.ttl_CurrentThickness.ReadOnly = true;
      this.ttl_CurrentThickness.Size = new System.Drawing.Size(347, 64);
      this.ttl_CurrentThickness.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.ttl_CurrentThickness.TabIndex = 127;
      this.ttl_CurrentThickness.Text = "Thickness";
      this.ttl_CurrentThickness.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentThickness.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentThickness.TranslateText = false;
      this.ttl_CurrentThickness.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMicron;
      this.ttl_CurrentThickness.Value = "1";
      this.ttl_CurrentThickness.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentThickness.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // ttl_CurrentRemaining
      // 
      this.ttl_CurrentRemaining.DecimalPlaces = 0;
      this.ttl_CurrentRemaining.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentRemaining.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentRemaining.ImagePassword = null;
      this.ttl_CurrentRemaining.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.ttl_CurrentRemaining.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.ttl_CurrentRemaining.Location = new System.Drawing.Point(187, 171);
      this.ttl_CurrentRemaining.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.ttl_CurrentRemaining.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.ttl_CurrentRemaining.Name = "ttl_CurrentRemaining";
      this.ttl_CurrentRemaining.ReadOnly = true;
      this.ttl_CurrentRemaining.Size = new System.Drawing.Size(167, 64);
      this.ttl_CurrentRemaining.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.ttl_CurrentRemaining.TabIndex = 126;
      this.ttl_CurrentRemaining.Text = "Remaining";
      this.ttl_CurrentRemaining.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentRemaining.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentRemaining.TranslateText = false;
      this.ttl_CurrentRemaining.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.ttl_CurrentRemaining.Value = "1";
      this.ttl_CurrentRemaining.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentRemaining.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // ttl_CurrentDiameter
      // 
      this.ttl_CurrentDiameter.DecimalPlaces = 0;
      this.ttl_CurrentDiameter.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentDiameter.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentDiameter.ImagePassword = null;
      this.ttl_CurrentDiameter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.ttl_CurrentDiameter.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.ttl_CurrentDiameter.Location = new System.Drawing.Point(7, 171);
      this.ttl_CurrentDiameter.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.ttl_CurrentDiameter.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.ttl_CurrentDiameter.Name = "ttl_CurrentDiameter";
      this.ttl_CurrentDiameter.ReadOnly = true;
      this.ttl_CurrentDiameter.Size = new System.Drawing.Size(166, 64);
      this.ttl_CurrentDiameter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.ttl_CurrentDiameter.TabIndex = 125;
      this.ttl_CurrentDiameter.Text = "Diameter";
      this.ttl_CurrentDiameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentDiameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentDiameter.TranslateText = false;
      this.ttl_CurrentDiameter.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.ttl_CurrentDiameter.Value = "1";
      this.ttl_CurrentDiameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_CurrentDiameter.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // grp_SplicerAlarm
      // 
      this.grp_SplicerAlarm.Controls.Add(this.chk_PaperBreak);
      this.grp_SplicerAlarm.Controls.Add(this.chk_CommsAlarm);
      this.grp_SplicerAlarm.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grp_SplicerAlarm.ForeColor = System.Drawing.Color.Black;
      this.grp_SplicerAlarm.Location = new System.Drawing.Point(14, 152);
      this.grp_SplicerAlarm.Name = "grp_SplicerAlarm";
      this.grp_SplicerAlarm.Size = new System.Drawing.Size(197, 168);
      this.grp_SplicerAlarm.TabIndex = 128;
      this.grp_SplicerAlarm.TabStop = false;
      this.grp_SplicerAlarm.Text = "Splicer Alarms";
      // 
      // chk_PaperBreak
      // 
      this.chk_PaperBreak.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_PaperBreak.Checked = true;
      this.chk_PaperBreak.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_PaperBreak.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_PaperBreak.ImagePassword = null;
      this.chk_PaperBreak.Location = new System.Drawing.Point(7, 94);
      this.chk_PaperBreak.Name = "chk_PaperBreak";
      this.chk_PaperBreak.ReadOnly = true;
      this.chk_PaperBreak.Size = new System.Drawing.Size(166, 64);
      this.chk_PaperBreak.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_PaperBreak.TabIndex = 128;
      this.chk_PaperBreak.Text = "Paper Break";
      this.chk_PaperBreak.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_PaperBreak.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_PaperBreak.TranslateText = false;
      // 
      // chk_CommsAlarm
      // 
      this.chk_CommsAlarm.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_CommsAlarm.Checked = true;
      this.chk_CommsAlarm.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_CommsAlarm.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_CommsAlarm.ImagePassword = null;
      this.chk_CommsAlarm.Location = new System.Drawing.Point(7, 24);
      this.chk_CommsAlarm.Name = "chk_CommsAlarm";
      this.chk_CommsAlarm.ReadOnly = true;
      this.chk_CommsAlarm.Size = new System.Drawing.Size(166, 64);
      this.chk_CommsAlarm.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_CommsAlarm.TabIndex = 127;
      this.chk_CommsAlarm.Text = "Comms";
      this.chk_CommsAlarm.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_CommsAlarm.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_CommsAlarm.TranslateText = false;
      // 
      // ttl_PreviousRemaining
      // 
      this.ttl_PreviousRemaining.DecimalPlaces = 0;
      this.ttl_PreviousRemaining.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_PreviousRemaining.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PreviousRemaining.ImagePassword = null;
      this.ttl_PreviousRemaining.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.ttl_PreviousRemaining.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.ttl_PreviousRemaining.Location = new System.Drawing.Point(187, 171);
      this.ttl_PreviousRemaining.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.ttl_PreviousRemaining.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.ttl_PreviousRemaining.Name = "ttl_PreviousRemaining";
      this.ttl_PreviousRemaining.ReadOnly = true;
      this.ttl_PreviousRemaining.Size = new System.Drawing.Size(167, 64);
      this.ttl_PreviousRemaining.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.ttl_PreviousRemaining.TabIndex = 126;
      this.ttl_PreviousRemaining.Text = "Remaining";
      this.ttl_PreviousRemaining.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_PreviousRemaining.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PreviousRemaining.TranslateText = false;
      this.ttl_PreviousRemaining.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.ttl_PreviousRemaining.Value = "1";
      this.ttl_PreviousRemaining.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_PreviousRemaining.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // ttl_PreviousDiameter
      // 
      this.ttl_PreviousDiameter.DecimalPlaces = 0;
      this.ttl_PreviousDiameter.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_PreviousDiameter.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PreviousDiameter.ImagePassword = null;
      this.ttl_PreviousDiameter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.ttl_PreviousDiameter.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.ttl_PreviousDiameter.Location = new System.Drawing.Point(7, 171);
      this.ttl_PreviousDiameter.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.ttl_PreviousDiameter.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.ttl_PreviousDiameter.Name = "ttl_PreviousDiameter";
      this.ttl_PreviousDiameter.ReadOnly = true;
      this.ttl_PreviousDiameter.Size = new System.Drawing.Size(166, 64);
      this.ttl_PreviousDiameter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.ttl_PreviousDiameter.TabIndex = 125;
      this.ttl_PreviousDiameter.Text = "Diameter";
      this.ttl_PreviousDiameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_PreviousDiameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PreviousDiameter.TranslateText = false;
      this.ttl_PreviousDiameter.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.ttl_PreviousDiameter.Value = "1";
      this.ttl_PreviousDiameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_PreviousDiameter.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // grp_PreviousRoll
      // 
      this.grp_PreviousRoll.Controls.Add(this.ttl_PaperTypePrevious);
      this.grp_PreviousRoll.Controls.Add(this.ttl_PreviousThickness);
      this.grp_PreviousRoll.Controls.Add(this.ttl_PreviousRemaining);
      this.grp_PreviousRoll.Controls.Add(this.ttl_PreviousDiameter);
      this.grp_PreviousRoll.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grp_PreviousRoll.ForeColor = System.Drawing.Color.Black;
      this.grp_PreviousRoll.Location = new System.Drawing.Point(790, 183);
      this.grp_PreviousRoll.Name = "grp_PreviousRoll";
      this.grp_PreviousRoll.Size = new System.Drawing.Size(359, 250);
      this.grp_PreviousRoll.TabIndex = 129;
      this.grp_PreviousRoll.TabStop = false;
      this.grp_PreviousRoll.Text = "Previous Roll";
      // 
      // ttl_PaperTypePrevious
      // 
      this.ttl_PaperTypePrevious.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_PaperTypePrevious.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PaperTypePrevious.ImagePassword = null;
      this.ttl_PaperTypePrevious.Location = new System.Drawing.Point(7, 28);
      this.ttl_PaperTypePrevious.Name = "ttl_PaperTypePrevious";
      this.ttl_PaperTypePrevious.Size = new System.Drawing.Size(346, 65);
      this.ttl_PaperTypePrevious.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_PaperTypePrevious.TabIndex = 128;
      this.ttl_PaperTypePrevious.Text = "PaperType + WeightFactor_UWg_m";
      this.ttl_PaperTypePrevious.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_PaperTypePrevious.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PaperTypePrevious.TranslateText = false;
      this.ttl_PaperTypePrevious.Value = "";
      this.ttl_PaperTypePrevious.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_PaperTypePrevious.ValueMultiline = true;
      // 
      // ttl_PreviousThickness
      // 
      this.ttl_PreviousThickness.DecimalPlaces = 0;
      this.ttl_PreviousThickness.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_PreviousThickness.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PreviousThickness.ImagePassword = null;
      this.ttl_PreviousThickness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.ttl_PreviousThickness.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMicron;
      this.ttl_PreviousThickness.Location = new System.Drawing.Point(7, 99);
      this.ttl_PreviousThickness.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
      this.ttl_PreviousThickness.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.ttl_PreviousThickness.Name = "ttl_PreviousThickness";
      this.ttl_PreviousThickness.ReadOnly = true;
      this.ttl_PreviousThickness.Size = new System.Drawing.Size(347, 64);
      this.ttl_PreviousThickness.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.ttl_PreviousThickness.TabIndex = 127;
      this.ttl_PreviousThickness.Text = "Thickness";
      this.ttl_PreviousThickness.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_PreviousThickness.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PreviousThickness.TranslateText = false;
      this.ttl_PreviousThickness.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMicron;
      this.ttl_PreviousThickness.Value = "1";
      this.ttl_PreviousThickness.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_PreviousThickness.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // grp_CtrlData
      // 
      this.grp_CtrlData.Controls.Add(this.chk_ReadyToSplice);
      this.grp_CtrlData.Controls.Add(this.grp_SplicerAlarm);
      this.grp_CtrlData.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grp_CtrlData.ForeColor = System.Drawing.Color.Black;
      this.grp_CtrlData.Location = new System.Drawing.Point(528, 365);
      this.grp_CtrlData.Name = "grp_CtrlData";
      this.grp_CtrlData.Size = new System.Drawing.Size(250, 350);
      this.grp_CtrlData.TabIndex = 130;
      this.grp_CtrlData.TabStop = false;
      this.grp_CtrlData.Text = "Splicer CtrlData";
      // 
      // Frm_SplicerSettingsStatus
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.grp_CtrlData);
      this.Controls.Add(this.grp_PreviousRoll);
      this.Controls.Add(this.grp_CurrentRoll);
      this.Controls.Add(this.grp_SplicerMonitor);
      this.Controls.Add(this.grp_SplicerControl);
      this.Name = "Frm_SplicerSettingsStatus";
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
      this.Controls.SetChildIndex(this.grp_SplicerControl, 0);
      this.Controls.SetChildIndex(this.grp_SplicerMonitor, 0);
      this.Controls.SetChildIndex(this.grp_CurrentRoll, 0);
      this.Controls.SetChildIndex(this.grp_PreviousRoll, 0);
      this.Controls.SetChildIndex(this.grp_CtrlData, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.grp_SplicerControl.ResumeLayout(false);
      this.grp_SplicerMonitor.ResumeLayout(false);
      this.grp_CurrentRoll.ResumeLayout(false);
      this.grp_SplicerAlarm.ResumeLayout(false);
      this.grp_PreviousRoll.ResumeLayout(false);
      this.grp_CtrlData.ResumeLayout(false);
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grp_SplicerControl;
		private SyncroC.Controls.StateButton ToggleAutoMan;
		private SyncroC.Controls.TitledNumericUpDown tnud_BridgeRequestedPrefill_UDmm;
		private SyncroC.Controls.TitledLabel ttl_RequestUnit;
		private SyncroC.Controls.TitledLabel ttl_SplicerModeStr;
		private SyncroC.Controls.TitledLabel ttl_SplicerLocalModeStr;
		private System.Windows.Forms.GroupBox grp_SplicerMonitor;
		private SyncroC.Controls.CheckBox chk_ReadyToSplice;
		private SyncroC.Controls.TitledNumericUpDown tnud_Speed;
		private System.Windows.Forms.GroupBox grp_CurrentRoll;
		private SyncroC.Controls.TitledNumericUpDown ttl_CurrentDiameter;
		private SyncroC.Controls.TitledNumericUpDown ttl_CurrentThickness;
		private SyncroC.Controls.TitledNumericUpDown ttl_CurrentRemaining;
		private System.Windows.Forms.GroupBox grp_SplicerAlarm;
		private SyncroC.Controls.CheckBox chk_CommsAlarm;
		private SyncroC.Controls.CheckBox chk_NotReadyAtEarlyWarning;
		private SyncroC.Controls.CheckBox chk_PaperBreak;
        private SyncroC.Controls.TitledNumericUpDown ttl_PreviousRemaining;
        private SyncroC.Controls.TitledNumericUpDown ttl_PreviousDiameter;
        private System.Windows.Forms.GroupBox grp_PreviousRoll;
        private SyncroC.Controls.TitledNumericUpDown ttl_PreviousThickness;
		private System.Windows.Forms.GroupBox grp_CtrlData;
		private SyncroC.Controls.TitledNumericUpDown tnud_LastCycle_UTs;
		private SyncroC.Controls.TitledLabel ttl_RollRunning;
		private SyncroC.Controls.TitledLabel ttl_PaperTypePrevious;
		private SyncroC.Controls.TitledLabel ttl_PaperTypeCurrent;
	}
}
