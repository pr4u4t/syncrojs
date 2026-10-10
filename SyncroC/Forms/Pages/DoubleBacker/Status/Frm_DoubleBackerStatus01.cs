using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.DataBase.EventArgs;
using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.DataBase;
using SyncroC.Controls.IncDecButton;

namespace SyncroC.Forms.Pages.DoubleBacker.Status
{
  public class Frm_DoubleBackerStatus01 : SyncroC.Forms.Pages.DoubleBacker.Status.Frm_DoubleBackerStatusSubgroup
	{
		private System.Windows.Forms.PictureBox pct_DoubleBacker;
		private SyncroC.Controls.TitledNumericUpDown tnud_Speed_UVmm_m;
		private SyncroC.Controls.WrapArm wa_wrapArmPreHeater31;
		private SyncroC.Controls.WrapArm wa_wrapArmPreHeater41;
		private SyncroC.Controls.Title ttl_CurrentProgram;
		private SyncroC.Controls.Title ttl_NextProgram;
		private SyncroC.Controls.TitledLabel tlb_CurrentQuality;
		private SyncroC.Controls.TitledLabel tlb_NextQuality;
		private System.ComponentModel.IContainer components = null;

		private int currId = 0, currModId = 0, nextId = 0, nextModId = 0;
		private SyncroC.Controls.IncDecButton.IncDecButtonKit wrapArmPreHeater31;
		private Panel pnl_CurrentOrder;
		private Panel pnl_nextOrder;
		private SyncroC.Controls.TitledNumericUpDown tlb_NextLength;
		private SyncroC.Controls.TitledNumericUpDown tlb_NextWidth;
		private SyncroC.Controls.TitledNumericUpDown tlb_CurrentWidth;
		private SyncroC.Controls.TitledNumericUpDown tlb_CurrentLength;
		private SyncroC.Controls.StateButton ParaffinMachineLiner;
		private SyncroC.Controls.StateButton SendCurrentOrder;
		private SyncroC.Controls.StateButton SendNextOrder;
    private PictureBox pct_LocRem_DB;
		private SyncroC.Controls.IncDecButton.IncDecButtonKit wrapArmPreHeater41;

		public Frm_DoubleBackerStatus01()
		{
			InitializeComponent();

			this.wa_wrapArmPreHeater31.setDesignModePage(this.DesignMode);
			this.wa_wrapArmPreHeater41.setDesignModePage(this.DesignMode);

      if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
      {
        this.SendCurrentOrder.IsButton = false;
        this.SendNextOrder.IsButton = false;
      }

			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
			{
				this.wrapArmPreHeater31.LowLevelEnabled = false;
				this.wrapArmPreHeater41.LowLevelEnabled = false;
				this.wa_wrapArmPreHeater31.LowLevelEnabled = false;
				this.wa_wrapArmPreHeater41.LowLevelEnabled = false;
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
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DoubleBackerStatus01));
      this.pct_DoubleBacker = new System.Windows.Forms.PictureBox();
      this.tnud_Speed_UVmm_m = new SyncroC.Controls.TitledNumericUpDown();
      this.wa_wrapArmPreHeater31 = new SyncroC.Controls.WrapArm();
      this.wa_wrapArmPreHeater41 = new SyncroC.Controls.WrapArm();
      this.ttl_CurrentProgram = new SyncroC.Controls.Title();
      this.ttl_NextProgram = new SyncroC.Controls.Title();
      this.tlb_CurrentQuality = new SyncroC.Controls.TitledLabel();
      this.tlb_NextQuality = new SyncroC.Controls.TitledLabel();
      this.wrapArmPreHeater31 = new SyncroC.Controls.IncDecButton.IncDecButtonKit();
      this.wrapArmPreHeater41 = new SyncroC.Controls.IncDecButton.IncDecButtonKit();
      this.pnl_CurrentOrder = new System.Windows.Forms.Panel();
      this.SendNextOrder = new SyncroC.Controls.StateButton();
      this.tlb_NextLength = new SyncroC.Controls.TitledNumericUpDown();
      this.tlb_NextWidth = new SyncroC.Controls.TitledNumericUpDown();
      this.pnl_nextOrder = new System.Windows.Forms.Panel();
      this.SendCurrentOrder = new SyncroC.Controls.StateButton();
      this.tlb_CurrentLength = new SyncroC.Controls.TitledNumericUpDown();
      this.tlb_CurrentWidth = new SyncroC.Controls.TitledNumericUpDown();
      this.ParaffinMachineLiner = new SyncroC.Controls.StateButton();
      this.pct_LocRem_DB = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_DoubleBacker)).BeginInit();
      this.pnl_CurrentOrder.SuspendLayout();
      this.pnl_nextOrder.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_LocRem_DB)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Text = "Double Backer";
      // 
      // tab_Tab3
      // 
      this.tab_Tab3.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab3.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // pct_DoubleBacker
      // 
      this.pct_DoubleBacker.Image = ((System.Drawing.Image)(resources.GetObject("pct_DoubleBacker.Image")));
      this.pct_DoubleBacker.Location = new System.Drawing.Point(30, 361);
      this.pct_DoubleBacker.Name = "pct_DoubleBacker";
      this.pct_DoubleBacker.Size = new System.Drawing.Size(1171, 177);
      this.pct_DoubleBacker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_DoubleBacker.TabIndex = 105;
      this.pct_DoubleBacker.TabStop = false;
      // 
      // tnud_Speed_UVmm_m
      // 
      this.tnud_Speed_UVmm_m.DecimalPlaces = 0;
      this.tnud_Speed_UVmm_m.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_Speed_UVmm_m.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Speed_UVmm_m.ImagePassword = null;
      this.tnud_Speed_UVmm_m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Speed_UVmm_m.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.tnud_Speed_UVmm_m.Location = new System.Drawing.Point(856, 589);
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
      this.tnud_Speed_UVmm_m.Size = new System.Drawing.Size(215, 100);
      this.tnud_Speed_UVmm_m.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.tnud_Speed_UVmm_m.TabIndex = 113;
      this.tnud_Speed_UVmm_m.TabStop = false;
      this.tnud_Speed_UVmm_m.Text = "Speed";
      this.tnud_Speed_UVmm_m.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Speed_UVmm_m.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Speed_UVmm_m.TranslateText = true;
      this.tnud_Speed_UVmm_m.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Speed_UVmm_m.Value = "350000";
      this.tnud_Speed_UVmm_m.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Speed_UVmm_m.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_Speed_UVmm_m.Visible = false;
      // 
      // wa_wrapArmPreHeater31
      // 
      this.wa_wrapArmPreHeater31.Auto = false;
      this.wa_wrapArmPreHeater31.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater31.InLocal = false;
      this.wa_wrapArmPreHeater31.IsMaxPos = false;
      this.wa_wrapArmPreHeater31.IsMinPos = false;
      this.wa_wrapArmPreHeater31.IsUnder = false;
      this.wa_wrapArmPreHeater31.Location = new System.Drawing.Point(30, 573);
      this.wa_wrapArmPreHeater31.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater31.MaximumSize = new System.Drawing.Size(133, 135);
      this.wa_wrapArmPreHeater31.MinimumSize = new System.Drawing.Size(133, 135);
      this.wa_wrapArmPreHeater31.Name = "wa_wrapArmPreHeater31";
      this.wa_wrapArmPreHeater31.Position = "0";
      this.wa_wrapArmPreHeater31.Size = new System.Drawing.Size(133, 135);
      this.wa_wrapArmPreHeater31.TabIndex = 134;
      this.wa_wrapArmPreHeater31.Temperature = 0;
      this.wa_wrapArmPreHeater31.TranslateText = false;
      this.wa_wrapArmPreHeater31.Visible = false;
      this.wa_wrapArmPreHeater31.WrapName = "31";
      this.wa_wrapArmPreHeater31.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater_OnWrapArmButtonPressed);
      // 
      // wa_wrapArmPreHeater41
      // 
      this.wa_wrapArmPreHeater41.Auto = false;
      this.wa_wrapArmPreHeater41.BackColor = System.Drawing.Color.WhiteSmoke;
      this.wa_wrapArmPreHeater41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.wa_wrapArmPreHeater41.InLocal = false;
      this.wa_wrapArmPreHeater41.IsMaxPos = false;
      this.wa_wrapArmPreHeater41.IsMinPos = false;
      this.wa_wrapArmPreHeater41.IsUnder = false;
      this.wa_wrapArmPreHeater41.Location = new System.Drawing.Point(323, 573);
      this.wa_wrapArmPreHeater41.LowLevelEnabled = true;
      this.wa_wrapArmPreHeater41.MaximumSize = new System.Drawing.Size(133, 135);
      this.wa_wrapArmPreHeater41.MinimumSize = new System.Drawing.Size(133, 135);
      this.wa_wrapArmPreHeater41.Name = "wa_wrapArmPreHeater41";
      this.wa_wrapArmPreHeater41.Position = "0";
      this.wa_wrapArmPreHeater41.Size = new System.Drawing.Size(133, 135);
      this.wa_wrapArmPreHeater41.TabIndex = 135;
      this.wa_wrapArmPreHeater41.Temperature = 0;
      this.wa_wrapArmPreHeater41.TranslateText = false;
      this.wa_wrapArmPreHeater41.Visible = false;
      this.wa_wrapArmPreHeater41.WrapName = "41";
      this.wa_wrapArmPreHeater41.OnWrapArmButtonPressed += new SyncroC.Controls.WrapArmButtonHandler(this.wa_wrapArmPreHeater_OnWrapArmButtonPressed);
      // 
      // ttl_CurrentProgram
      // 
      this.ttl_CurrentProgram.BackColor = System.Drawing.Color.Black;
      this.ttl_CurrentProgram.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentProgram.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentProgram.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_CurrentProgram.Location = new System.Drawing.Point(3, 33);
      this.ttl_CurrentProgram.Name = "ttl_CurrentProgram";
      this.ttl_CurrentProgram.Size = new System.Drawing.Size(433, 130);
      this.ttl_CurrentProgram.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_CurrentProgram.TabIndex = 136;
      this.ttl_CurrentProgram.Text = "CURRENT PROGRAM: Empty code";
      this.ttl_CurrentProgram.TranslateText = false;
      // 
      // ttl_NextProgram
      // 
      this.ttl_NextProgram.BackColor = System.Drawing.Color.Black;
      this.ttl_NextProgram.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextProgram.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NextProgram.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_NextProgram.Location = new System.Drawing.Point(3, 33);
      this.ttl_NextProgram.Name = "ttl_NextProgram";
      this.ttl_NextProgram.Size = new System.Drawing.Size(426, 130);
      this.ttl_NextProgram.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_NextProgram.TabIndex = 137;
      this.ttl_NextProgram.Text = "NEXT PROGRAM: Empty code";
      this.ttl_NextProgram.TranslateText = false;
      // 
      // tlb_CurrentQuality
      // 
      this.tlb_CurrentQuality.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_CurrentQuality.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_CurrentQuality.ImagePassword = null;
      this.tlb_CurrentQuality.Location = new System.Drawing.Point(12, 53);
      this.tlb_CurrentQuality.Name = "tlb_CurrentQuality";
      this.tlb_CurrentQuality.Size = new System.Drawing.Size(417, 55);
      this.tlb_CurrentQuality.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_CurrentQuality.TabIndex = 138;
      this.tlb_CurrentQuality.Text = "Quality";
      this.tlb_CurrentQuality.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_CurrentQuality.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_CurrentQuality.TranslateText = true;
      this.tlb_CurrentQuality.Value = "";
      this.tlb_CurrentQuality.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_CurrentQuality.ValueMultiline = true;
      // 
      // tlb_NextQuality
      // 
      this.tlb_NextQuality.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_NextQuality.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_NextQuality.ImagePassword = null;
      this.tlb_NextQuality.Location = new System.Drawing.Point(12, 53);
      this.tlb_NextQuality.Name = "tlb_NextQuality";
      this.tlb_NextQuality.Size = new System.Drawing.Size(417, 55);
      this.tlb_NextQuality.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_NextQuality.TabIndex = 141;
      this.tlb_NextQuality.Text = "Quality";
      this.tlb_NextQuality.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_NextQuality.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_NextQuality.TranslateText = true;
      this.tlb_NextQuality.Value = "";
      this.tlb_NextQuality.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_NextQuality.ValueMultiline = true;
      // 
      // wrapArmPreHeater31
      // 
      this.wrapArmPreHeater31.BackColor = System.Drawing.Color.Transparent;
      this.wrapArmPreHeater31.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.wrapArmPreHeater31.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.wrapArmPreHeater31.Location = new System.Drawing.Point(169, 573);
      this.wrapArmPreHeater31.LowLevelEnabled = true;
      this.wrapArmPreHeater31.Name = "wrapArmPreHeater31";
      this.wrapArmPreHeater31.Position = SyncroC.Controls.IncDecButton.IncDecButtonPositionType.Vertical;
      this.wrapArmPreHeater31.Size = new System.Drawing.Size(78, 135);
      this.wrapArmPreHeater31.TabIndex = 454;
      this.wrapArmPreHeater31.TranslateText = false;
      this.wrapArmPreHeater31.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.wrapArmPreHeater31.UnitMeasureVisible = false;
      this.wrapArmPreHeater31.Value = "0";
      this.wrapArmPreHeater31.ValueVisible = false;
      this.wrapArmPreHeater31.Visible = false;
      this.wrapArmPreHeater31.OnIncDecButtonKitDown += new SyncroC.Controls.IncDecButton.IncDecButtonKitDownHandler(this.wrapArmPreHeater_OnIncDecButtonKitDown);
      // 
      // wrapArmPreHeater41
      // 
      this.wrapArmPreHeater41.BackColor = System.Drawing.Color.Transparent;
      this.wrapArmPreHeater41.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.wrapArmPreHeater41.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.wrapArmPreHeater41.Location = new System.Drawing.Point(462, 573);
      this.wrapArmPreHeater41.LowLevelEnabled = true;
      this.wrapArmPreHeater41.Name = "wrapArmPreHeater41";
      this.wrapArmPreHeater41.Position = SyncroC.Controls.IncDecButton.IncDecButtonPositionType.Vertical;
      this.wrapArmPreHeater41.Size = new System.Drawing.Size(78, 135);
      this.wrapArmPreHeater41.TabIndex = 455;
      this.wrapArmPreHeater41.TranslateText = false;
      this.wrapArmPreHeater41.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.wrapArmPreHeater41.UnitMeasureVisible = false;
      this.wrapArmPreHeater41.Value = "0";
      this.wrapArmPreHeater41.ValueVisible = false;
      this.wrapArmPreHeater41.Visible = false;
      this.wrapArmPreHeater41.OnIncDecButtonKitDown += new SyncroC.Controls.IncDecButton.IncDecButtonKitDownHandler(this.wrapArmPreHeater_OnIncDecButtonKitDown);
      // 
      // pnl_CurrentOrder
      // 
      this.pnl_CurrentOrder.Controls.Add(this.SendNextOrder);
      this.pnl_CurrentOrder.Controls.Add(this.tlb_NextLength);
      this.pnl_CurrentOrder.Controls.Add(this.ttl_NextProgram);
      this.pnl_CurrentOrder.Controls.Add(this.tlb_NextWidth);
      this.pnl_CurrentOrder.Controls.Add(this.tlb_NextQuality);
      this.pnl_CurrentOrder.Location = new System.Drawing.Point(30, 172);
      this.pnl_CurrentOrder.Name = "pnl_CurrentOrder";
      this.pnl_CurrentOrder.Size = new System.Drawing.Size(436, 171);
      this.pnl_CurrentOrder.TabIndex = 456;
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
      this.SendNextOrder.Location = new System.Drawing.Point(333, 0);
      this.SendNextOrder.Name = "SendNextOrder";
      this.SendNextOrder.OnLine = false;
      this.SendNextOrder.ShowImage = true;
      this.SendNextOrder.ShowText = false;
      this.SendNextOrder.Size = new System.Drawing.Size(100, 58);
      this.SendNextOrder.Switching = false;
      this.SendNextOrder.TabIndex = 474;
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
      this.tlb_NextLength.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_NextLength.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tlb_NextLength.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_NextLength.ImagePassword = null;
      this.tlb_NextLength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tlb_NextLength.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tlb_NextLength.Location = new System.Drawing.Point(229, 107);
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
      this.tlb_NextLength.Size = new System.Drawing.Size(200, 55);
      this.tlb_NextLength.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tlb_NextLength.TabIndex = 471;
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
      this.tlb_NextWidth.Location = new System.Drawing.Point(12, 107);
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
      this.tlb_NextWidth.Size = new System.Drawing.Size(200, 55);
      this.tlb_NextWidth.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tlb_NextWidth.TabIndex = 472;
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
      this.pnl_nextOrder.Controls.Add(this.SendCurrentOrder);
      this.pnl_nextOrder.Controls.Add(this.tlb_CurrentLength);
      this.pnl_nextOrder.Controls.Add(this.ttl_CurrentProgram);
      this.pnl_nextOrder.Controls.Add(this.tlb_CurrentWidth);
      this.pnl_nextOrder.Controls.Add(this.tlb_CurrentQuality);
      this.pnl_nextOrder.Location = new System.Drawing.Point(765, 172);
      this.pnl_nextOrder.Name = "pnl_nextOrder";
      this.pnl_nextOrder.Size = new System.Drawing.Size(436, 171);
      this.pnl_nextOrder.TabIndex = 457;
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
      this.SendCurrentOrder.Location = new System.Drawing.Point(336, 0);
      this.SendCurrentOrder.Name = "SendCurrentOrder";
      this.SendCurrentOrder.OnLine = false;
      this.SendCurrentOrder.ShowImage = true;
      this.SendCurrentOrder.ShowText = false;
      this.SendCurrentOrder.Size = new System.Drawing.Size(100, 58);
      this.SendCurrentOrder.Switching = false;
      this.SendCurrentOrder.TabIndex = 473;
      this.SendCurrentOrder.Text = "1";
      this.SendCurrentOrder.TranslateText = false;
      this.SendCurrentOrder.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendCurrentOrder.UserDefinedActiveImage")));
      this.SendCurrentOrder.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendCurrentOrder.UserDefinedImage")));
      this.SendCurrentOrder.Warned = false;
      this.SendCurrentOrder.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // tlb_CurrentLength
      // 
      this.tlb_CurrentLength.DecimalPlaces = 0;
      this.tlb_CurrentLength.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_CurrentLength.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tlb_CurrentLength.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_CurrentLength.ImagePassword = null;
      this.tlb_CurrentLength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tlb_CurrentLength.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tlb_CurrentLength.Location = new System.Drawing.Point(229, 107);
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
      this.tlb_CurrentLength.Size = new System.Drawing.Size(200, 55);
      this.tlb_CurrentLength.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tlb_CurrentLength.TabIndex = 473;
      this.tlb_CurrentLength.Text = "Length";
      this.tlb_CurrentLength.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_CurrentLength.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_CurrentLength.TranslateText = true;
      this.tlb_CurrentLength.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tlb_CurrentLength.Value = "587600";
      this.tlb_CurrentLength.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_CurrentLength.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tlb_CurrentWidth
      // 
      this.tlb_CurrentWidth.DecimalPlaces = 0;
      this.tlb_CurrentWidth.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_CurrentWidth.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tlb_CurrentWidth.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_CurrentWidth.ImagePassword = null;
      this.tlb_CurrentWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tlb_CurrentWidth.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.tlb_CurrentWidth.Location = new System.Drawing.Point(12, 107);
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
      this.tlb_CurrentWidth.Size = new System.Drawing.Size(200, 55);
      this.tlb_CurrentWidth.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tlb_CurrentWidth.TabIndex = 474;
      this.tlb_CurrentWidth.Text = "Width";
      this.tlb_CurrentWidth.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_CurrentWidth.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_CurrentWidth.TranslateText = true;
      this.tlb_CurrentWidth.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tlb_CurrentWidth.Value = "587600";
      this.tlb_CurrentWidth.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_CurrentWidth.ValueForeColor = System.Drawing.SystemColors.WindowText;
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
      this.ParaffinMachineLiner.Location = new System.Drawing.Point(1090, 589);
      this.ParaffinMachineLiner.Name = "ParaffinMachineLiner";
      this.ParaffinMachineLiner.OnLine = false;
      this.ParaffinMachineLiner.ShowImage = true;
      this.ParaffinMachineLiner.ShowText = false;
      this.ParaffinMachineLiner.Size = new System.Drawing.Size(100, 100);
      this.ParaffinMachineLiner.Switching = false;
      this.ParaffinMachineLiner.TabIndex = 471;
      this.ParaffinMachineLiner.Text = "ParaffinMachineLiner";
      this.ParaffinMachineLiner.TranslateText = false;
      this.ParaffinMachineLiner.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ParaffinMachineLiner.UserDefinedActiveImage")));
      this.ParaffinMachineLiner.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ParaffinMachineLiner.UserDefinedImage")));
      this.ParaffinMachineLiner.Visible = false;
      this.ParaffinMachineLiner.Warned = false;
      this.ParaffinMachineLiner.Click += new System.EventHandler(this.ParaffinMachineLiner_Click);
      // 
      // pct_LocRem_DB
      // 
      this.pct_LocRem_DB.Image = global::SyncroC.Properties.Resources.LOCALE_ISO_1109;
      this.pct_LocRem_DB.Location = new System.Drawing.Point(562, 285);
      this.pct_LocRem_DB.Name = "pct_LocRem_DB";
      this.pct_LocRem_DB.Size = new System.Drawing.Size(100, 58);
      this.pct_LocRem_DB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pct_LocRem_DB.TabIndex = 505;
      this.pct_LocRem_DB.TabStop = false;
      this.pct_LocRem_DB.Visible = false;
      // 
      // Frm_DoubleBackerStatus01
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.pct_LocRem_DB);
      this.Controls.Add(this.pnl_CurrentOrder);
      this.Controls.Add(this.wrapArmPreHeater41);
      this.Controls.Add(this.wrapArmPreHeater31);
      this.Controls.Add(this.wa_wrapArmPreHeater41);
      this.Controls.Add(this.wa_wrapArmPreHeater31);
      this.Controls.Add(this.pct_DoubleBacker);
      this.Controls.Add(this.tnud_Speed_UVmm_m);
      this.Controls.Add(this.ParaffinMachineLiner);
      this.Controls.Add(this.pnl_nextOrder);
      this.Name = "Frm_DoubleBackerStatus01";
      this.Text = "";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.pnl_nextOrder, 0);
      this.Controls.SetChildIndex(this.ParaffinMachineLiner, 0);
      this.Controls.SetChildIndex(this.tnud_Speed_UVmm_m, 0);
      this.Controls.SetChildIndex(this.pct_DoubleBacker, 0);
      this.Controls.SetChildIndex(this.wa_wrapArmPreHeater31, 0);
      this.Controls.SetChildIndex(this.wa_wrapArmPreHeater41, 0);
      this.Controls.SetChildIndex(this.wrapArmPreHeater31, 0);
      this.Controls.SetChildIndex(this.wrapArmPreHeater41, 0);
      this.Controls.SetChildIndex(this.pnl_CurrentOrder, 0);
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
      this.Controls.SetChildIndex(this.pct_LocRem_DB, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_DoubleBacker)).EndInit();
      this.pnl_CurrentOrder.ResumeLayout(false);
      this.pnl_nextOrder.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct_LocRem_DB)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		protected override void F10()
		{}

    private void _setLocaleRemoteMachine(bool isDoubleBackerRemote)
    {
      if (SyncroC.DBManager.getDBManager().syncroConfig.DBExpress400CtrlSetting.Enable)
        this.pct_LocRem_DB.Visible = false;

      //RFAZZI_FI26/07/2011_1. Rimossa visualizzazione stato macchina Loc/Rem da pagine di stato DB e SF quando presenti Express400 e Smart400 xchè nn presenti sulla macchina. 
      if (DBManager.getDBManager().lineConfig.PRC &&
        !SyncroC.DBManager.getDBManager().syncroConfig.DBExpress400CtrlSetting.Enable)
      {
        this.pct_LocRem_DB.Visible = true;
        this.pct_LocRem_DB.Image = isDoubleBackerRemote ? SyncroC.Properties.Resources.REMOTO_ISO_1108 : SyncroC.Properties.Resources.LOCALE_ISO_1109;
      }
    }

		private void _setCurrentProgram(int currentProgramId, int currentProgramModId)
		{
			this.currId = currentProgramId;
			this.currModId = currentProgramModId;

			DataRow row = null;
			SyncroC.DataBase.Message msg = DBManager.getDBManager().message;

			if (this.currId != 0)
				row = DBManager.getDBManager().order.WETGetRow(currentProgramId.ToString(), currentProgramModId.ToString());

			if (row != null)
			{//valorizzazione con ordine
				ttl_CurrentProgram.Text = msg.getTranslatedMessage("§SYCC_94") + ":" + row[Order.WEL_PROGRAM_CODE].ToString();
				tlb_CurrentQuality.Value = row[Order.WEL_PROGRAM_QUALITY].ToString();

        //PMASSARO_FI22/02/08_1 - measure allignment
        //tlb_CurrentWidth.Value = SyncroConverter.ConvertString(row[Order.WEL_PAPER_WIDTH].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
        tlb_CurrentWidth.Value = row[Order.WEL_PAPER_WIDTH].ToString();
				
        tlb_CurrentLength.Value = row[Order.WEL_PROGRAM_LENGTH].ToString();
			}
			else
			{//ordine non presente tra gli ordini e nemmeno tra i past orders: svuotare tutto
				ttl_CurrentProgram.Text = msg.getTranslatedMessage("§SYCC_94");
				tlb_CurrentQuality.Value = tlb_CurrentLength.Value = tlb_CurrentWidth.Value = string.Empty;
			}
		}

		private void _setNextProgram(int nextProgramIdSetup, int nextProgramModId)
		{
			this.nextId = nextProgramIdSetup;
			this.nextModId = nextProgramModId;

			DataRow row = null;
			SyncroC.DataBase.Message msg = DBManager.getDBManager().message;

			if (this.nextId != 0)
				row = DBManager.getDBManager().order.WETGetRow(nextProgramIdSetup.ToString(), nextProgramModId.ToString());

			if (row != null)
			{//valorizzazione con ordine
				ttl_NextProgram.Text = msg.getTranslatedMessage("§SYCC_95") + ":" + row[Order.WEL_PROGRAM_CODE].ToString();
				tlb_NextQuality.Value = row[Order.WEL_PROGRAM_QUALITY].ToString();

        //PMASSARO_FI22/02/08_1 - measure allignment
        //tlb_NextWidth.Value = SyncroConverter.ConvertString(row[Order.WEL_PAPER_WIDTH].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
        tlb_NextWidth.Value = row[Order.WEL_PAPER_WIDTH].ToString();
				
        tlb_NextLength.Value = row[Order.WEL_PROGRAM_LENGTH].ToString();
			}
			else
			{//ordine non presente tra gli ordini e nemmeno tra i past orders: svuotare tutto
				ttl_NextProgram.Text = msg.getTranslatedMessage("§SYCC_95");
				tlb_NextQuality.Value = tlb_NextLength.Value = tlb_NextWidth.Value = string.Empty;
			}
		}

		private void wa_wrapArmPreHeater_OnWrapArmButtonPressed(object sender, SyncroC.Controls.WrapArmButtonEventArgs args)
		{
			if ((args.AutoMan != false) || (args.LocRem != false))
			{
				string cmd = ((Control)sender).Name.Replace("wa_", args.AutoMan ? "AutoManual" : "LocalRemote");

				//ABello_FI10/07/2007_1 I pulsanti di Toggle Auto/Man e Loc/Rem stavano ad ON per 50 ms e la macchina non vedeva l'ingresso andare su, portato a 1500 ms
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
			}
			else
			{
				string cmd = ((Control)sender).Name.Replace("wa_", "");

				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
					PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
			}
		}

		private void wrapArmPreHeater_OnIncDecButtonKitDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonKitDownEventArgs args)
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." +
				((Control)sender).Name + "." + ((args.buttonType == IncDecButtonType.Increase) ? Command.INCREASE : Command.DECREASE) + "(" +
				args.Frequency + ")");
		}

		public override void CorrugatorDirectionLeft()
		{
			int leftLimit = this.pct_DoubleBacker.Location.X;
			int rightLimit = this.pct_DoubleBacker.Location.X + this.pct_DoubleBacker.Width;

			//DoubleBacker Picture
			this.pct_DoubleBacker.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);

			//Wrap Arm & Speed
			leftLimit = this.wa_wrapArmPreHeater31.Location.X;
      rightLimit = this.ParaffinMachineLiner.Location.X + this.ParaffinMachineLiner.Width;

			HorizontalLocationMirror(this.wa_wrapArmPreHeater31, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wrapArmPreHeater31, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wa_wrapArmPreHeater41, leftLimit, rightLimit);
			HorizontalLocationMirror(this.wrapArmPreHeater41, leftLimit, rightLimit);
			HorizontalLocationMirror(this.tnud_Speed_UVmm_m, leftLimit, rightLimit);
      HorizontalLocationMirror(this.ParaffinMachineLiner, leftLimit, rightLimit);

			//current & next order
			leftLimit = this.pnl_nextOrder.Location.X;
			rightLimit = this.pnl_CurrentOrder.Location.X + this.pnl_CurrentOrder.Width;

			HorizontalLocationMirror(this.pnl_nextOrder, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pnl_CurrentOrder, leftLimit, rightLimit);
		}

    private void ParaffinMachineLiner_Click(object sender, EventArgs e)
    {
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
        PageManager.getRequestCommand(typeof(Frm_DoubleBackerStatus01)) + "." + "ParaffinMachineLiner" + "." + Command.TOGGLE_WITH_TIMEOUT);
    }

		private void OnButtonPush(object sender, System.EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
				return;

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.DoubleBacker.Console.Frm_DoubleBackerConsole01)) + "." + ((Control)sender).Name + "()");
		}

		private void _CommunicationError(bool isError)
		{
			this.SendCurrentOrder.Alarmed = isError;
			this.SendNextOrder.Alarmed = isError;
		}

		private void Frm_DoubleBackerStatus01_Load(object sender, EventArgs e)
		{
			this._CommunicationError(false);
		}
	}
}

