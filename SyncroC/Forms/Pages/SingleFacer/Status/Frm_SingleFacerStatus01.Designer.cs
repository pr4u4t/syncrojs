namespace SyncroC.Forms.Pages.SingleFacer.Status
{
	partial class Frm_SingleFacerStatus01
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SingleFacerStatus01));
      this.pct_machine = new System.Windows.Forms.PictureBox();
      this.tnud_Speed_UVmm_m = new SyncroC.Controls.TitledNumericUpDown();
      this.wa_wrapArmPreHeaterEl = new SyncroC.Controls.WrapArm();
      this.wa_wrapArmPreHeaterIm = new SyncroC.Controls.WrapArm();
      this.gg_glueGap1 = new SyncroC.Controls.LowLevelControl();
      this.lbl_SFCartridge = new System.Windows.Forms.Label();
      this.tlb_CurrentQuality = new SyncroC.Controls.TitledLabel();
      this.ttl_CurrentProgram = new SyncroC.Controls.Title();
      this.tlb_NextQuality = new SyncroC.Controls.TitledLabel();
      this.ttl_NextProgram = new SyncroC.Controls.Title();
      this.wrapArmPreHeaterEl = new SyncroC.Controls.IncDecButton.IncDecButtonKit();
      this.glueGap1 = new SyncroC.Controls.IncDecButton.IncDecButtonKit();
      this.wrapArmPreHeaterIm = new SyncroC.Controls.IncDecButton.IncDecButtonKit();
      this.pnl_currentOrder = new System.Windows.Forms.Panel();
      this.SendNextOrder = new SyncroC.Controls.StateButton();
      this.tlb_NextLength = new SyncroC.Controls.TitledNumericUpDown();
      this.tlb_NextWidth = new SyncroC.Controls.TitledNumericUpDown();
      this.pnl_nextOrder = new System.Windows.Forms.Panel();
      this.tlb_CurrentLength = new SyncroC.Controls.TitledNumericUpDown();
      this.SendCurrentOrder = new SyncroC.Controls.StateButton();
      this.tlb_CurrentWidth = new SyncroC.Controls.TitledNumericUpDown();
      this.wrapArmPreHeaterEm = new SyncroC.Controls.IncDecButton.IncDecButtonKit();
      this.wa_wrapArmPreHeaterEm = new SyncroC.Controls.WrapArm();
      this.wrapArmPreHeaterIl = new SyncroC.Controls.IncDecButton.IncDecButtonKit();
      this.wa_wrapArmPreHeaterIl = new SyncroC.Controls.WrapArm();
      this.MoistureSensor = new SyncroC.Controls.StateButton();
      this.lbl_MoistureSensorGreen = new System.Windows.Forms.Label();
      this.lbl_MoistureSensorRed = new System.Windows.Forms.Label();
      this.ParaffinMachineMedium = new SyncroC.Controls.StateButton();
      this.ParaffinMachineLiner = new SyncroC.Controls.StateButton();
      this.pct_LocRem_SF = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_machine)).BeginInit();
      this.pnl_currentOrder.SuspendLayout();
      this.pnl_nextOrder.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_LocRem_SF)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // pct_machine
      // 
      this.pct_machine.CausesValidation = false;
      this.pct_machine.Image = ((System.Drawing.Image)(resources.GetObject("pct_machine.Image")));
      this.pct_machine.Location = new System.Drawing.Point(354, 193);
      this.pct_machine.Name = "pct_machine";
      this.pct_machine.Size = new System.Drawing.Size(520, 520);
      this.pct_machine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pct_machine.TabIndex = 208;
      this.pct_machine.TabStop = false;
      // 
      // tnud_Speed_UVmm_m
      // 
      this.tnud_Speed_UVmm_m.DecimalPlaces = 0;
      this.tnud_Speed_UVmm_m.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_Speed_UVmm_m.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Speed_UVmm_m.ImagePassword = null;
      this.tnud_Speed_UVmm_m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Speed_UVmm_m.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_Speed_UVmm_m.Location = new System.Drawing.Point(751, 322);
      this.tnud_Speed_UVmm_m.Maximum = new decimal(new int[] {
            5000000,
            0,
            0,
            0});
      this.tnud_Speed_UVmm_m.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_Speed_UVmm_m.Name = "tnud_Speed_UVmm_m";
      this.tnud_Speed_UVmm_m.ReadOnly = true;
      this.tnud_Speed_UVmm_m.Size = new System.Drawing.Size(121, 83);
      this.tnud_Speed_UVmm_m.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.tnud_Speed_UVmm_m.TabIndex = 209;
      this.tnud_Speed_UVmm_m.TabStop = false;
      this.tnud_Speed_UVmm_m.Text = "Speed";
      this.tnud_Speed_UVmm_m.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Speed_UVmm_m.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Speed_UVmm_m.TranslateText = true;
      this.tnud_Speed_UVmm_m.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Speed_UVmm_m.Value = "400000";
      this.tnud_Speed_UVmm_m.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Speed_UVmm_m.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_Speed_UVmm_m.Visible = false;
      // 
      // wa_wrapArmPreHeaterEl
      // 
      this.wa_wrapArmPreHeaterEl.Auto = false;
      this.wa_wrapArmPreHeaterEl.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeaterEl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeaterEl.InLocal = false;
      this.wa_wrapArmPreHeaterEl.IsMaxPos = false;
      this.wa_wrapArmPreHeaterEl.IsMinPos = false;
      this.wa_wrapArmPreHeaterEl.IsUnder = false;
      this.wa_wrapArmPreHeaterEl.Location = new System.Drawing.Point(63, 432);
      this.wa_wrapArmPreHeaterEl.LowLevelEnabled = true;
      this.wa_wrapArmPreHeaterEl.Name = "wa_wrapArmPreHeaterEl";
      this.wa_wrapArmPreHeaterEl.Position = "0";
      this.wa_wrapArmPreHeaterEl.Size = new System.Drawing.Size(133, 135);
      this.wa_wrapArmPreHeaterEl.TabIndex = 220;
      this.wa_wrapArmPreHeaterEl.Temperature = 0;
      this.wa_wrapArmPreHeaterEl.TranslateText = false;
      this.wa_wrapArmPreHeaterEl.Visible = false;
      this.wa_wrapArmPreHeaterEl.WrapName = "El";
      this.wa_wrapArmPreHeaterEl.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater_OnWrapArmButtonPressed);
      // 
      // wa_wrapArmPreHeaterIm
      // 
      this.wa_wrapArmPreHeaterIm.Auto = false;
      this.wa_wrapArmPreHeaterIm.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeaterIm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeaterIm.InLocal = false;
      this.wa_wrapArmPreHeaterIm.IsMaxPos = false;
      this.wa_wrapArmPreHeaterIm.IsMinPos = false;
      this.wa_wrapArmPreHeaterIm.IsUnder = false;
      this.wa_wrapArmPreHeaterIm.Location = new System.Drawing.Point(968, 576);
      this.wa_wrapArmPreHeaterIm.LowLevelEnabled = true;
      this.wa_wrapArmPreHeaterIm.Name = "wa_wrapArmPreHeaterIm";
      this.wa_wrapArmPreHeaterIm.Position = "0";
      this.wa_wrapArmPreHeaterIm.Size = new System.Drawing.Size(133, 135);
      this.wa_wrapArmPreHeaterIm.TabIndex = 222;
      this.wa_wrapArmPreHeaterIm.Temperature = 0;
      this.wa_wrapArmPreHeaterIm.TranslateText = false;
      this.wa_wrapArmPreHeaterIm.Visible = false;
      this.wa_wrapArmPreHeaterIm.WrapName = "Im";
      this.wa_wrapArmPreHeaterIm.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater_OnWrapArmButtonPressed);
      // 
      // gg_glueGap1
      // 
      this.gg_glueGap1.Auto = false;
      this.gg_glueGap1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gg_glueGap1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.gg_glueGap1.InLocal = false;
      this.gg_glueGap1.IsMaxPos = false;
      this.gg_glueGap1.IsMinPos = false;
      this.gg_glueGap1.Location = new System.Drawing.Point(433, 470);
      this.gg_glueGap1.LowLevelControlName = "1";
      this.gg_glueGap1.LowLevelEnabled = true;
      this.gg_glueGap1.Margin = new System.Windows.Forms.Padding(0);
      this.gg_glueGap1.MaxSetPointValue = 100;
      this.gg_glueGap1.MinSetPointValue = 0;
      this.gg_glueGap1.MyTypeOfLowLevelControl = SyncroC.Controls.LowLevelControl.TypeOfLowLevelControl.GlueGap;
      this.gg_glueGap1.Name = "gg_glueGap1";
      this.gg_glueGap1.Position = "0";
      this.gg_glueGap1.Size = new System.Drawing.Size(133, 135);
      this.gg_glueGap1.TabIndex = 224;
      this.gg_glueGap1.TranslateText = false;
      this.gg_glueGap1.Visible = false;
      this.gg_glueGap1.OnLowLevelControlButtonPressed += new SyncroC.Controls.LowLevelControlButtonHandler(this.gg_glueGap_OnGlueGapButtonPressed);
      // 
      // lbl_SFCartridge
      // 
      this.lbl_SFCartridge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_SFCartridge.Font = new System.Drawing.Font("Arial Unicode MS", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SFCartridge.ForeColor = System.Drawing.Color.Blue;
      this.lbl_SFCartridge.Location = new System.Drawing.Point(659, 280);
      this.lbl_SFCartridge.Name = "lbl_SFCartridge";
      this.lbl_SFCartridge.Size = new System.Drawing.Size(58, 54);
      this.lbl_SFCartridge.TabIndex = 419;
      this.lbl_SFCartridge.Text = "-";
      this.lbl_SFCartridge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // tlb_CurrentQuality
      // 
      this.tlb_CurrentQuality.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_CurrentQuality.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_CurrentQuality.ImagePassword = null;
      this.tlb_CurrentQuality.Location = new System.Drawing.Point(14, 71);
      this.tlb_CurrentQuality.Name = "tlb_CurrentQuality";
      this.tlb_CurrentQuality.Size = new System.Drawing.Size(262, 55);
      this.tlb_CurrentQuality.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_CurrentQuality.TabIndex = 421;
      this.tlb_CurrentQuality.Text = "Quality";
      this.tlb_CurrentQuality.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_CurrentQuality.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_CurrentQuality.TranslateText = true;
      this.tlb_CurrentQuality.Value = "";
      this.tlb_CurrentQuality.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_CurrentQuality.ValueMultiline = true;
      // 
      // ttl_CurrentProgram
      // 
      this.ttl_CurrentProgram.BackColor = System.Drawing.Color.Black;
      this.ttl_CurrentProgram.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentProgram.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentProgram.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_CurrentProgram.Location = new System.Drawing.Point(4, 51);
      this.ttl_CurrentProgram.Name = "ttl_CurrentProgram";
      this.ttl_CurrentProgram.Size = new System.Drawing.Size(272, 197);
      this.ttl_CurrentProgram.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_CurrentProgram.TabIndex = 420;
      this.ttl_CurrentProgram.Text = "CURRENT PROGRAM: Empty code";
      this.ttl_CurrentProgram.TranslateText = false;
      // 
      // tlb_NextQuality
      // 
      this.tlb_NextQuality.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_NextQuality.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_NextQuality.ImagePassword = null;
      this.tlb_NextQuality.Location = new System.Drawing.Point(11, 71);
      this.tlb_NextQuality.Name = "tlb_NextQuality";
      this.tlb_NextQuality.Size = new System.Drawing.Size(264, 55);
      this.tlb_NextQuality.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_NextQuality.TabIndex = 425;
      this.tlb_NextQuality.Text = "Quality";
      this.tlb_NextQuality.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_NextQuality.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_NextQuality.TranslateText = true;
      this.tlb_NextQuality.Value = "";
      this.tlb_NextQuality.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_NextQuality.ValueMultiline = true;
      // 
      // ttl_NextProgram
      // 
      this.ttl_NextProgram.BackColor = System.Drawing.Color.Black;
      this.ttl_NextProgram.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextProgram.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NextProgram.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_NextProgram.Location = new System.Drawing.Point(3, 51);
      this.ttl_NextProgram.Name = "ttl_NextProgram";
      this.ttl_NextProgram.Size = new System.Drawing.Size(272, 197);
      this.ttl_NextProgram.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_NextProgram.TabIndex = 424;
      this.ttl_NextProgram.Text = "NEXT PROGRAM: Empty code";
      this.ttl_NextProgram.TranslateText = false;
      // 
      // wrapArmPreHeaterEl
      // 
      this.wrapArmPreHeaterEl.BackColor = System.Drawing.Color.Transparent;
      this.wrapArmPreHeaterEl.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.wrapArmPreHeaterEl.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.wrapArmPreHeaterEl.Location = new System.Drawing.Point(202, 431);
      this.wrapArmPreHeaterEl.LowLevelEnabled = true;
      this.wrapArmPreHeaterEl.Name = "wrapArmPreHeaterEl";
      this.wrapArmPreHeaterEl.Position = SyncroC.Controls.IncDecButton.IncDecButtonPositionType.Vertical;
      this.wrapArmPreHeaterEl.Size = new System.Drawing.Size(78, 135);
      this.wrapArmPreHeaterEl.TabIndex = 456;
      this.wrapArmPreHeaterEl.TranslateText = false;
      this.wrapArmPreHeaterEl.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.wrapArmPreHeaterEl.UnitMeasureVisible = false;
      this.wrapArmPreHeaterEl.Value = "0";
      this.wrapArmPreHeaterEl.ValueVisible = false;
      this.wrapArmPreHeaterEl.Visible = false;
      this.wrapArmPreHeaterEl.OnIncDecButtonKitDown += new SyncroC.Controls.IncDecButton.IncDecButtonKitDownHandler(this.wrapArmPreHeater_OnIncDecButtonKitDown);
      // 
      // glueGap1
      // 
      this.glueGap1.BackColor = System.Drawing.Color.Transparent;
      this.glueGap1.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.glueGap1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.glueGap1.Location = new System.Drawing.Point(433, 608);
      this.glueGap1.LowLevelEnabled = true;
      this.glueGap1.Name = "glueGap1";
      this.glueGap1.Position = SyncroC.Controls.IncDecButton.IncDecButtonPositionType.Horizontal;
      this.glueGap1.Size = new System.Drawing.Size(135, 78);
      this.glueGap1.TabIndex = 457;
      this.glueGap1.TranslateText = false;
      this.glueGap1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.glueGap1.UnitMeasureVisible = false;
      this.glueGap1.Value = "0";
      this.glueGap1.ValueVisible = false;
      this.glueGap1.Visible = false;
      this.glueGap1.OnIncDecButtonKitDown += new SyncroC.Controls.IncDecButton.IncDecButtonKitDownHandler(this.wrapArmPreHeater_OnIncDecButtonKitDown);
      // 
      // wrapArmPreHeaterIm
      // 
      this.wrapArmPreHeaterIm.BackColor = System.Drawing.Color.Transparent;
      this.wrapArmPreHeaterIm.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.wrapArmPreHeaterIm.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.wrapArmPreHeaterIm.Location = new System.Drawing.Point(884, 576);
      this.wrapArmPreHeaterIm.LowLevelEnabled = true;
      this.wrapArmPreHeaterIm.Name = "wrapArmPreHeaterIm";
      this.wrapArmPreHeaterIm.Position = SyncroC.Controls.IncDecButton.IncDecButtonPositionType.Vertical;
      this.wrapArmPreHeaterIm.Size = new System.Drawing.Size(78, 135);
      this.wrapArmPreHeaterIm.TabIndex = 458;
      this.wrapArmPreHeaterIm.TranslateText = false;
      this.wrapArmPreHeaterIm.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.wrapArmPreHeaterIm.UnitMeasureVisible = false;
      this.wrapArmPreHeaterIm.Value = "0";
      this.wrapArmPreHeaterIm.ValueVisible = false;
      this.wrapArmPreHeaterIm.Visible = false;
      this.wrapArmPreHeaterIm.OnIncDecButtonKitDown += new SyncroC.Controls.IncDecButton.IncDecButtonKitDownHandler(this.wrapArmPreHeater_OnIncDecButtonKitDown);
      // 
      // pnl_currentOrder
      // 
      this.pnl_currentOrder.Controls.Add(this.SendNextOrder);
      this.pnl_currentOrder.Controls.Add(this.tlb_NextLength);
      this.pnl_currentOrder.Controls.Add(this.tlb_NextWidth);
      this.pnl_currentOrder.Controls.Add(this.ttl_NextProgram);
      this.pnl_currentOrder.Controls.Add(this.tlb_NextQuality);
      this.pnl_currentOrder.Location = new System.Drawing.Point(30, 171);
      this.pnl_currentOrder.Name = "pnl_currentOrder";
      this.pnl_currentOrder.Size = new System.Drawing.Size(282, 255);
      this.pnl_currentOrder.TabIndex = 460;
      // 
      // SendNextOrder
      // 
      this.SendNextOrder.Active = false;
      this.SendNextOrder.Alarmed = false;
      this.SendNextOrder.Auto = false;
      this.SendNextOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.SendNextOrder.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SendNextOrder.FillBackColorWithImage = true;
      this.SendNextOrder.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SendNextOrder.ForeColor = System.Drawing.Color.Black;
      this.SendNextOrder.FramePresent = true;
      this.SendNextOrder.HighLightEnable = false;
      this.SendNextOrder.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SendNextOrder.ImagePassword = null;
      this.SendNextOrder.Inemergency = false;
      this.SendNextOrder.InLocal = false;
      this.SendNextOrder.IsButton = true;
      this.SendNextOrder.IsForceToInvisible = false;
      this.SendNextOrder.Location = new System.Drawing.Point(177, 0);
      this.SendNextOrder.Name = "SendNextOrder";
      this.SendNextOrder.OnLine = false;
      this.SendNextOrder.ShowImage = true;
      this.SendNextOrder.ShowText = false;
      this.SendNextOrder.Size = new System.Drawing.Size(100, 51);
      this.SendNextOrder.Switching = false;
      this.SendNextOrder.TabIndex = 472;
      this.SendNextOrder.Text = "2";
      this.SendNextOrder.TranslateText = false;
      this.SendNextOrder.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendNextOrder.UserDefinedActiveImage")));
      this.SendNextOrder.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendNextOrder.UserDefinedImage")));
      this.SendNextOrder.Warned = false;
      this.SendNextOrder.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // tlb_NextLength
      // 
      this.tlb_NextLength.DecimalPlaces = 0;
      this.tlb_NextLength.Font = new System.Drawing.Font("Arial Unicode MS", 20F, System.Drawing.FontStyle.Bold);
      this.tlb_NextLength.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tlb_NextLength.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_NextLength.ImagePassword = null;
      this.tlb_NextLength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tlb_NextLength.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tlb_NextLength.Location = new System.Drawing.Point(11, 191);
      this.tlb_NextLength.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tlb_NextLength.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tlb_NextLength.Name = "tlb_NextLength";
      this.tlb_NextLength.ReadOnly = true;
      this.tlb_NextLength.Size = new System.Drawing.Size(262, 55);
      this.tlb_NextLength.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tlb_NextLength.TabIndex = 469;
      this.tlb_NextLength.Text = "Length";
      this.tlb_NextLength.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_NextLength.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_NextLength.TranslateText = true;
      this.tlb_NextLength.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tlb_NextLength.Value = "587600";
      this.tlb_NextLength.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_NextLength.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tlb_NextWidth
      // 
      this.tlb_NextWidth.DecimalPlaces = 0;
      this.tlb_NextWidth.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_NextWidth.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tlb_NextWidth.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_NextWidth.ImagePassword = null;
      this.tlb_NextWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tlb_NextWidth.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.tlb_NextWidth.Location = new System.Drawing.Point(11, 130);
      this.tlb_NextWidth.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tlb_NextWidth.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tlb_NextWidth.Name = "tlb_NextWidth";
      this.tlb_NextWidth.ReadOnly = true;
      this.tlb_NextWidth.Size = new System.Drawing.Size(262, 55);
      this.tlb_NextWidth.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tlb_NextWidth.TabIndex = 470;
      this.tlb_NextWidth.Text = "Width";
      this.tlb_NextWidth.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_NextWidth.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_NextWidth.TranslateText = true;
      this.tlb_NextWidth.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tlb_NextWidth.Value = "587600";
      this.tlb_NextWidth.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_NextWidth.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // pnl_nextOrder
      // 
      this.pnl_nextOrder.Controls.Add(this.tlb_CurrentLength);
      this.pnl_nextOrder.Controls.Add(this.SendCurrentOrder);
      this.pnl_nextOrder.Controls.Add(this.ttl_CurrentProgram);
      this.pnl_nextOrder.Controls.Add(this.tlb_CurrentWidth);
      this.pnl_nextOrder.Controls.Add(this.tlb_CurrentQuality);
      this.pnl_nextOrder.Location = new System.Drawing.Point(916, 171);
      this.pnl_nextOrder.Name = "pnl_nextOrder";
      this.pnl_nextOrder.Size = new System.Drawing.Size(285, 254);
      this.pnl_nextOrder.TabIndex = 461;
      // 
      // tlb_CurrentLength
      // 
      this.tlb_CurrentLength.DecimalPlaces = 0;
      this.tlb_CurrentLength.Font = new System.Drawing.Font("Arial Unicode MS", 20F, System.Drawing.FontStyle.Bold);
      this.tlb_CurrentLength.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tlb_CurrentLength.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_CurrentLength.ImagePassword = null;
      this.tlb_CurrentLength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tlb_CurrentLength.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tlb_CurrentLength.Location = new System.Drawing.Point(14, 191);
      this.tlb_CurrentLength.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tlb_CurrentLength.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tlb_CurrentLength.Name = "tlb_CurrentLength";
      this.tlb_CurrentLength.ReadOnly = true;
      this.tlb_CurrentLength.Size = new System.Drawing.Size(262, 55);
      this.tlb_CurrentLength.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tlb_CurrentLength.TabIndex = 471;
      this.tlb_CurrentLength.Text = "Length";
      this.tlb_CurrentLength.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_CurrentLength.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_CurrentLength.TranslateText = true;
      this.tlb_CurrentLength.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tlb_CurrentLength.Value = "587600";
      this.tlb_CurrentLength.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_CurrentLength.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // SendCurrentOrder
      // 
      this.SendCurrentOrder.Active = false;
      this.SendCurrentOrder.Alarmed = false;
      this.SendCurrentOrder.Auto = false;
      this.SendCurrentOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.SendCurrentOrder.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SendCurrentOrder.FillBackColorWithImage = true;
      this.SendCurrentOrder.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SendCurrentOrder.ForeColor = System.Drawing.Color.Black;
      this.SendCurrentOrder.FramePresent = true;
      this.SendCurrentOrder.HighLightEnable = false;
      this.SendCurrentOrder.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SendCurrentOrder.ImagePassword = null;
      this.SendCurrentOrder.Inemergency = false;
      this.SendCurrentOrder.InLocal = false;
      this.SendCurrentOrder.IsButton = true;
      this.SendCurrentOrder.IsForceToInvisible = false;
      this.SendCurrentOrder.Location = new System.Drawing.Point(178, 0);
      this.SendCurrentOrder.Name = "SendCurrentOrder";
      this.SendCurrentOrder.OnLine = false;
      this.SendCurrentOrder.ShowImage = true;
      this.SendCurrentOrder.ShowText = false;
      this.SendCurrentOrder.Size = new System.Drawing.Size(100, 51);
      this.SendCurrentOrder.Switching = false;
      this.SendCurrentOrder.TabIndex = 471;
      this.SendCurrentOrder.Text = "1";
      this.SendCurrentOrder.TranslateText = false;
      this.SendCurrentOrder.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendCurrentOrder.UserDefinedActiveImage")));
      this.SendCurrentOrder.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendCurrentOrder.UserDefinedImage")));
      this.SendCurrentOrder.Warned = false;
      this.SendCurrentOrder.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // tlb_CurrentWidth
      // 
      this.tlb_CurrentWidth.DecimalPlaces = 0;
      this.tlb_CurrentWidth.Font = new System.Drawing.Font("Arial Unicode MS", 20F, System.Drawing.FontStyle.Bold);
      this.tlb_CurrentWidth.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tlb_CurrentWidth.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_CurrentWidth.ImagePassword = null;
      this.tlb_CurrentWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tlb_CurrentWidth.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.tlb_CurrentWidth.Location = new System.Drawing.Point(14, 132);
      this.tlb_CurrentWidth.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tlb_CurrentWidth.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tlb_CurrentWidth.Name = "tlb_CurrentWidth";
      this.tlb_CurrentWidth.ReadOnly = true;
      this.tlb_CurrentWidth.Size = new System.Drawing.Size(262, 55);
      this.tlb_CurrentWidth.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tlb_CurrentWidth.TabIndex = 472;
      this.tlb_CurrentWidth.Text = "Width";
      this.tlb_CurrentWidth.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_CurrentWidth.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_CurrentWidth.TranslateText = true;
      this.tlb_CurrentWidth.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tlb_CurrentWidth.Value = "587600";
      this.tlb_CurrentWidth.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_CurrentWidth.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // wrapArmPreHeaterEm
      // 
      this.wrapArmPreHeaterEm.BackColor = System.Drawing.Color.Transparent;
      this.wrapArmPreHeaterEm.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.wrapArmPreHeaterEm.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.wrapArmPreHeaterEm.Location = new System.Drawing.Point(948, 432);
      this.wrapArmPreHeaterEm.LowLevelEnabled = true;
      this.wrapArmPreHeaterEm.Name = "wrapArmPreHeaterEm";
      this.wrapArmPreHeaterEm.Position = SyncroC.Controls.IncDecButton.IncDecButtonPositionType.Vertical;
      this.wrapArmPreHeaterEm.Size = new System.Drawing.Size(78, 135);
      this.wrapArmPreHeaterEm.TabIndex = 463;
      this.wrapArmPreHeaterEm.TranslateText = false;
      this.wrapArmPreHeaterEm.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.wrapArmPreHeaterEm.UnitMeasureVisible = false;
      this.wrapArmPreHeaterEm.Value = "0";
      this.wrapArmPreHeaterEm.ValueVisible = false;
      this.wrapArmPreHeaterEm.Visible = false;
      this.wrapArmPreHeaterEm.OnIncDecButtonKitDown += new SyncroC.Controls.IncDecButton.IncDecButtonKitDownHandler(this.wrapArmPreHeater_OnIncDecButtonKitDown);
      // 
      // wa_wrapArmPreHeaterEm
      // 
      this.wa_wrapArmPreHeaterEm.Auto = false;
      this.wa_wrapArmPreHeaterEm.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeaterEm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeaterEm.InLocal = false;
      this.wa_wrapArmPreHeaterEm.IsMaxPos = false;
      this.wa_wrapArmPreHeaterEm.IsMinPos = false;
      this.wa_wrapArmPreHeaterEm.IsUnder = false;
      this.wa_wrapArmPreHeaterEm.Location = new System.Drawing.Point(1032, 432);
      this.wa_wrapArmPreHeaterEm.LowLevelEnabled = true;
      this.wa_wrapArmPreHeaterEm.MaximumSize = new System.Drawing.Size(133, 135);
      this.wa_wrapArmPreHeaterEm.MinimumSize = new System.Drawing.Size(133, 135);
      this.wa_wrapArmPreHeaterEm.Name = "wa_wrapArmPreHeaterEm";
      this.wa_wrapArmPreHeaterEm.Position = "0";
      this.wa_wrapArmPreHeaterEm.Size = new System.Drawing.Size(133, 135);
      this.wa_wrapArmPreHeaterEm.TabIndex = 462;
      this.wa_wrapArmPreHeaterEm.Temperature = 0;
      this.wa_wrapArmPreHeaterEm.TranslateText = false;
      this.wa_wrapArmPreHeaterEm.Visible = false;
      this.wa_wrapArmPreHeaterEm.WrapName = "Em";
      this.wa_wrapArmPreHeaterEm.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater_OnWrapArmButtonPressed);
      // 
      // wrapArmPreHeaterIl
      // 
      this.wrapArmPreHeaterIl.BackColor = System.Drawing.Color.Transparent;
      this.wrapArmPreHeaterIl.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.wrapArmPreHeaterIl.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.wrapArmPreHeaterIl.Location = new System.Drawing.Point(258, 577);
      this.wrapArmPreHeaterIl.LowLevelEnabled = true;
      this.wrapArmPreHeaterIl.Name = "wrapArmPreHeaterIl";
      this.wrapArmPreHeaterIl.Position = SyncroC.Controls.IncDecButton.IncDecButtonPositionType.Vertical;
      this.wrapArmPreHeaterIl.Size = new System.Drawing.Size(78, 135);
      this.wrapArmPreHeaterIl.TabIndex = 465;
      this.wrapArmPreHeaterIl.TranslateText = false;
      this.wrapArmPreHeaterIl.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.wrapArmPreHeaterIl.UnitMeasureVisible = false;
      this.wrapArmPreHeaterIl.Value = "0";
      this.wrapArmPreHeaterIl.ValueVisible = false;
      this.wrapArmPreHeaterIl.Visible = false;
      this.wrapArmPreHeaterIl.OnIncDecButtonKitDown += new SyncroC.Controls.IncDecButton.IncDecButtonKitDownHandler(this.wrapArmPreHeater_OnIncDecButtonKitDown);
      // 
      // wa_wrapArmPreHeaterIl
      // 
      this.wa_wrapArmPreHeaterIl.Auto = false;
      this.wa_wrapArmPreHeaterIl.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeaterIl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeaterIl.InLocal = false;
      this.wa_wrapArmPreHeaterIl.IsMaxPos = false;
      this.wa_wrapArmPreHeaterIl.IsMinPos = false;
      this.wa_wrapArmPreHeaterIl.IsUnder = false;
      this.wa_wrapArmPreHeaterIl.Location = new System.Drawing.Point(122, 577);
      this.wa_wrapArmPreHeaterIl.LowLevelEnabled = true;
      this.wa_wrapArmPreHeaterIl.Name = "wa_wrapArmPreHeaterIl";
      this.wa_wrapArmPreHeaterIl.Position = "0";
      this.wa_wrapArmPreHeaterIl.Size = new System.Drawing.Size(133, 135);
      this.wa_wrapArmPreHeaterIl.TabIndex = 464;
      this.wa_wrapArmPreHeaterIl.Temperature = 0;
      this.wa_wrapArmPreHeaterIl.TranslateText = false;
      this.wa_wrapArmPreHeaterIl.Visible = false;
      this.wa_wrapArmPreHeaterIl.WrapName = "Il";
      this.wa_wrapArmPreHeaterIl.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater_OnWrapArmButtonPressed);
      // 
      // MoistureSensor
      // 
      this.MoistureSensor.Active = false;
      this.MoistureSensor.Alarmed = false;
      this.MoistureSensor.Auto = false;
      this.MoistureSensor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.MoistureSensor.Cursor = System.Windows.Forms.Cursors.Hand;
      this.MoistureSensor.FillBackColorWithImage = true;
      this.MoistureSensor.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.MoistureSensor.FramePresent = true;
      this.MoistureSensor.HighLightEnable = false;
      this.MoistureSensor.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.MoistureSensor.ImagePassword = null;
      this.MoistureSensor.Inemergency = false;
      this.MoistureSensor.InLocal = false;
      this.MoistureSensor.IsButton = true;
      this.MoistureSensor.IsForceToInvisible = false;
      this.MoistureSensor.Location = new System.Drawing.Point(762, 207);
      this.MoistureSensor.Name = "MoistureSensor";
      this.MoistureSensor.OnLine = false;
      this.MoistureSensor.ShowImage = true;
      this.MoistureSensor.ShowText = false;
      this.MoistureSensor.Size = new System.Drawing.Size(89, 75);
      this.MoistureSensor.Switching = false;
      this.MoistureSensor.TabIndex = 466;
      this.MoistureSensor.Text = "Auto";
      this.MoistureSensor.TranslateText = false;
      this.MoistureSensor.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("MoistureSensor.UserDefinedActiveImage")));
      this.MoistureSensor.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("MoistureSensor.UserDefinedImage")));
      this.MoistureSensor.Warned = false;
      this.MoistureSensor.Click += new System.EventHandler(this.MoistureSensor_Click);
      // 
      // lbl_MoistureSensorGreen
      // 
      this.lbl_MoistureSensorGreen.BackColor = System.Drawing.Color.Lime;
      this.lbl_MoistureSensorGreen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_MoistureSensorGreen.Location = new System.Drawing.Point(766, 283);
      this.lbl_MoistureSensorGreen.Name = "lbl_MoistureSensorGreen";
      this.lbl_MoistureSensorGreen.Size = new System.Drawing.Size(35, 14);
      this.lbl_MoistureSensorGreen.TabIndex = 467;
      this.lbl_MoistureSensorGreen.Visible = false;
      // 
      // lbl_MoistureSensorRed
      // 
      this.lbl_MoistureSensorRed.BackColor = System.Drawing.Color.Red;
      this.lbl_MoistureSensorRed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_MoistureSensorRed.Location = new System.Drawing.Point(814, 283);
      this.lbl_MoistureSensorRed.Name = "lbl_MoistureSensorRed";
      this.lbl_MoistureSensorRed.Size = new System.Drawing.Size(35, 14);
      this.lbl_MoistureSensorRed.TabIndex = 468;
      this.lbl_MoistureSensorRed.Visible = false;
      // 
      // ParaffinMachineMedium
      // 
      this.ParaffinMachineMedium.Active = false;
      this.ParaffinMachineMedium.Alarmed = false;
      this.ParaffinMachineMedium.Auto = false;
      this.ParaffinMachineMedium.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ParaffinMachineMedium.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ParaffinMachineMedium.FillBackColorWithImage = false;
      this.ParaffinMachineMedium.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ParaffinMachineMedium.FramePresent = true;
      this.ParaffinMachineMedium.HighLightEnable = false;
      this.ParaffinMachineMedium.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.ParaffinMachineMedium.ImagePassword = null;
      this.ParaffinMachineMedium.Inemergency = false;
      this.ParaffinMachineMedium.InLocal = false;
      this.ParaffinMachineMedium.IsButton = true;
      this.ParaffinMachineMedium.IsForceToInvisible = false;
      this.ParaffinMachineMedium.Location = new System.Drawing.Point(842, 432);
      this.ParaffinMachineMedium.Name = "ParaffinMachineMedium";
      this.ParaffinMachineMedium.OnLine = false;
      this.ParaffinMachineMedium.ShowImage = true;
      this.ParaffinMachineMedium.ShowText = false;
      this.ParaffinMachineMedium.Size = new System.Drawing.Size(100, 100);
      this.ParaffinMachineMedium.Switching = false;
      this.ParaffinMachineMedium.TabIndex = 469;
      this.ParaffinMachineMedium.Text = "ParaffinMachine";
      this.ParaffinMachineMedium.TranslateText = false;
      this.ParaffinMachineMedium.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ParaffinMachineMedium.UserDefinedActiveImage")));
      this.ParaffinMachineMedium.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ParaffinMachineMedium.UserDefinedImage")));
      this.ParaffinMachineMedium.Visible = false;
      this.ParaffinMachineMedium.Warned = false;
      this.ParaffinMachineMedium.Click += new System.EventHandler(this.ParaffinMachineMedium_Click);
      // 
      // ParaffinMachineLiner
      // 
      this.ParaffinMachineLiner.Active = false;
      this.ParaffinMachineLiner.Alarmed = false;
      this.ParaffinMachineLiner.Auto = false;
      this.ParaffinMachineLiner.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ParaffinMachineLiner.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ParaffinMachineLiner.FillBackColorWithImage = false;
      this.ParaffinMachineLiner.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ParaffinMachineLiner.FramePresent = true;
      this.ParaffinMachineLiner.HighLightEnable = false;
      this.ParaffinMachineLiner.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.ParaffinMachineLiner.ImagePassword = null;
      this.ParaffinMachineLiner.Inemergency = false;
      this.ParaffinMachineLiner.InLocal = false;
      this.ParaffinMachineLiner.IsButton = true;
      this.ParaffinMachineLiner.IsForceToInvisible = false;
      this.ParaffinMachineLiner.Location = new System.Drawing.Point(284, 432);
      this.ParaffinMachineLiner.Name = "ParaffinMachineLiner";
      this.ParaffinMachineLiner.OnLine = false;
      this.ParaffinMachineLiner.ShowImage = true;
      this.ParaffinMachineLiner.ShowText = false;
      this.ParaffinMachineLiner.Size = new System.Drawing.Size(100, 100);
      this.ParaffinMachineLiner.Switching = false;
      this.ParaffinMachineLiner.TabIndex = 470;
      this.ParaffinMachineLiner.Text = "ParaffinMachineLiner";
      this.ParaffinMachineLiner.TranslateText = false;
      this.ParaffinMachineLiner.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ParaffinMachineLiner.UserDefinedActiveImage")));
      this.ParaffinMachineLiner.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ParaffinMachineLiner.UserDefinedImage")));
      this.ParaffinMachineLiner.Visible = false;
      this.ParaffinMachineLiner.Warned = false;
      this.ParaffinMachineLiner.Click += new System.EventHandler(this.ParaffinMachineLiner_Click);
      // 
      // pct_LocRem_SF
      // 
      this.pct_LocRem_SF.Image = global::SyncroC.Properties.Resources.LOCALE_ISO_1109;
      this.pct_LocRem_SF.Location = new System.Drawing.Point(363, 303);
      this.pct_LocRem_SF.Name = "pct_LocRem_SF";
      this.pct_LocRem_SF.Size = new System.Drawing.Size(100, 58);
      this.pct_LocRem_SF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pct_LocRem_SF.TabIndex = 506;
      this.pct_LocRem_SF.TabStop = false;
      this.pct_LocRem_SF.Visible = false;
      // 
      // Frm_SingleFacerStatus01
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.pct_LocRem_SF);
      this.Controls.Add(this.ParaffinMachineLiner);
      this.Controls.Add(this.ParaffinMachineMedium);
      this.Controls.Add(this.lbl_MoistureSensorRed);
      this.Controls.Add(this.lbl_MoistureSensorGreen);
      this.Controls.Add(this.MoistureSensor);
      this.Controls.Add(this.wrapArmPreHeaterIl);
      this.Controls.Add(this.wa_wrapArmPreHeaterIl);
      this.Controls.Add(this.wrapArmPreHeaterEm);
      this.Controls.Add(this.wa_wrapArmPreHeaterEm);
      this.Controls.Add(this.pnl_nextOrder);
      this.Controls.Add(this.pnl_currentOrder);
      this.Controls.Add(this.wrapArmPreHeaterIm);
      this.Controls.Add(this.glueGap1);
      this.Controls.Add(this.wrapArmPreHeaterEl);
      this.Controls.Add(this.lbl_SFCartridge);
      this.Controls.Add(this.gg_glueGap1);
      this.Controls.Add(this.wa_wrapArmPreHeaterIm);
      this.Controls.Add(this.wa_wrapArmPreHeaterEl);
      this.Controls.Add(this.tnud_Speed_UVmm_m);
      this.Controls.Add(this.pct_machine);
      this.Name = "Frm_SingleFacerStatus01";
      this.Load += new System.EventHandler(this.Frm_SingleFacerStatus01_Load);
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
      this.Controls.SetChildIndex(this.pct_machine, 0);
      this.Controls.SetChildIndex(this.tnud_Speed_UVmm_m, 0);
      this.Controls.SetChildIndex(this.wa_wrapArmPreHeaterEl, 0);
      this.Controls.SetChildIndex(this.wa_wrapArmPreHeaterIm, 0);
      this.Controls.SetChildIndex(this.gg_glueGap1, 0);
      this.Controls.SetChildIndex(this.lbl_SFCartridge, 0);
      this.Controls.SetChildIndex(this.wrapArmPreHeaterEl, 0);
      this.Controls.SetChildIndex(this.glueGap1, 0);
      this.Controls.SetChildIndex(this.wrapArmPreHeaterIm, 0);
      this.Controls.SetChildIndex(this.pnl_currentOrder, 0);
      this.Controls.SetChildIndex(this.pnl_nextOrder, 0);
      this.Controls.SetChildIndex(this.wa_wrapArmPreHeaterEm, 0);
      this.Controls.SetChildIndex(this.wrapArmPreHeaterEm, 0);
      this.Controls.SetChildIndex(this.wa_wrapArmPreHeaterIl, 0);
      this.Controls.SetChildIndex(this.wrapArmPreHeaterIl, 0);
      this.Controls.SetChildIndex(this.MoistureSensor, 0);
      this.Controls.SetChildIndex(this.lbl_MoistureSensorGreen, 0);
      this.Controls.SetChildIndex(this.lbl_MoistureSensorRed, 0);
      this.Controls.SetChildIndex(this.ParaffinMachineMedium, 0);
      this.Controls.SetChildIndex(this.ParaffinMachineLiner, 0);
      this.Controls.SetChildIndex(this.pct_LocRem_SF, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_machine)).EndInit();
      this.pnl_currentOrder.ResumeLayout(false);
      this.pnl_nextOrder.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct_LocRem_SF)).EndInit();
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pct_machine;
		private SyncroC.Controls.TitledNumericUpDown tnud_Speed_UVmm_m;
		private SyncroC.Controls.WrapArm wa_wrapArmPreHeaterEl;
		private SyncroC.Controls.WrapArm wa_wrapArmPreHeaterIm;
    private SyncroC.Controls.LowLevelControl gg_glueGap1;
		private System.Windows.Forms.Label lbl_SFCartridge;
		private SyncroC.Controls.TitledLabel tlb_CurrentQuality;
		private SyncroC.Controls.Title ttl_CurrentProgram;
		private SyncroC.Controls.TitledLabel tlb_NextQuality;
		private SyncroC.Controls.Title ttl_NextProgram;
		private SyncroC.Controls.IncDecButton.IncDecButtonKit wrapArmPreHeaterEl;
		private SyncroC.Controls.IncDecButton.IncDecButtonKit glueGap1;
		private SyncroC.Controls.IncDecButton.IncDecButtonKit wrapArmPreHeaterIm;
		private System.Windows.Forms.Panel pnl_currentOrder;
		private System.Windows.Forms.Panel pnl_nextOrder;
		private SyncroC.Controls.IncDecButton.IncDecButtonKit wrapArmPreHeaterEm;
		private SyncroC.Controls.WrapArm wa_wrapArmPreHeaterEm;
		private SyncroC.Controls.IncDecButton.IncDecButtonKit wrapArmPreHeaterIl;
		private SyncroC.Controls.WrapArm wa_wrapArmPreHeaterIl;
		private SyncroC.Controls.StateButton MoistureSensor;
		private System.Windows.Forms.Label lbl_MoistureSensorGreen;
		private System.Windows.Forms.Label lbl_MoistureSensorRed;
		private SyncroC.Controls.TitledNumericUpDown tlb_NextLength;
		private SyncroC.Controls.TitledNumericUpDown tlb_NextWidth;
		private SyncroC.Controls.TitledNumericUpDown tlb_CurrentLength;
		private SyncroC.Controls.TitledNumericUpDown tlb_CurrentWidth;
    private SyncroC.Controls.StateButton ParaffinMachineMedium;
    private SyncroC.Controls.StateButton ParaffinMachineLiner;
		private SyncroC.Controls.StateButton SendCurrentOrder;
		private SyncroC.Controls.StateButton SendNextOrder;
    private System.Windows.Forms.PictureBox pct_LocRem_SF;


	}
}
