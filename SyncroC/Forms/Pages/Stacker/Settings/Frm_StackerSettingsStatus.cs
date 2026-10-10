using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.Forms.Pages.Stacker.Settings
{
	public class Frm_StackerSettingsStatus : SyncroC.Forms.Pages.Stacker.Settings.Frm_StackerSettingsSubgroup
	{
		private GroupBox grp_dataToPrint;
		private SyncroC.Controls.TitledNumericUpDown tnud_dischargingSetupID;
		private SyncroC.Controls.TitledNumericUpDown tnud_dischargingSetupModID;
		private SyncroC.Controls.TitledNumericUpDown tnud_CutLenght;
		private SyncroC.Controls.TitledNumericUpDown tnud_SheetWidth;
		private SyncroC.Controls.TitledNumericUpDown tnud_ActualCutsinPlatform;
		private SyncroC.Controls.TitledNumericUpDown tnud_Outs;
		private SyncroC.Controls.TitledTextBox ttb_ProductionType;
		private SyncroC.Controls.TitledTextBox ttb_DischargingType;
		private SyncroC.Controls.CheckBox chk_NormalAOC;
		private SyncroC.Controls.TitledNumericUpDown tnud_GenericInfo1;
		private SyncroC.Controls.TitledNumericUpDown tnud_GenericInfo3;
		private SyncroC.Controls.TitledNumericUpDown tnud_GenericInfo2;
		private SyncroC.Controls.TitledNumericUpDown tnud_nextdischargingSetupModID;
		private SyncroC.Controls.TitledNumericUpDown tnud_nextdischargingSetupID;
		private SyncroC.Controls.CheckBox chk_Discharge;
		private SyncroC.Controls.CheckBox chk_DischargeCompleted;
		private SyncroC.Controls.TitledNumericUpDown tnud_DischargeCounter;
		private SyncroC.Controls.TitledNumericUpDown tnud_AOCDischargeCounter;
		private System.ComponentModel.IContainer components = null;

		public Frm_StackerSettingsStatus()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		public Frm_StackerSettingsStatus(int index)
			: this()
		{
			if (!(index == 1 || index == 2 || index == 3))
				Logger.LogError(this, "Frm_StackerSettingsStatus - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\" values are allowed.");
			this.index = index;

			DBManager dbm = DBManager.getDBManager();
			level = (LevelName)index;
			if ((dbm.lineConfig.KnifeConfig.LevelCount == 2) && (index == 2))
				level = LevelName.Upper;

			lbl_Title_YN.Text += " " + level.ToString();
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
			base.Dispose(disposing);
		}

		protected override void F12()
		{
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_StackerSettingsStatus));
      this.grp_dataToPrint = new System.Windows.Forms.GroupBox();
      this.tnud_nextdischargingSetupModID = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_nextdischargingSetupID = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_GenericInfo3 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_GenericInfo2 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_GenericInfo1 = new SyncroC.Controls.TitledNumericUpDown();
      this.chk_NormalAOC = new SyncroC.Controls.CheckBox();
      this.ttb_DischargingType = new SyncroC.Controls.TitledTextBox();
      this.ttb_ProductionType = new SyncroC.Controls.TitledTextBox();
      this.tnud_Outs = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_ActualCutsinPlatform = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_SheetWidth = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_CutLenght = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_dischargingSetupModID = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_dischargingSetupID = new SyncroC.Controls.TitledNumericUpDown();
      this.chk_Discharge = new SyncroC.Controls.CheckBox();
      this.chk_DischargeCompleted = new SyncroC.Controls.CheckBox();
      this.tnud_DischargeCounter = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_AOCDischargeCounter = new SyncroC.Controls.TitledNumericUpDown();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.grp_dataToPrint.SuspendLayout();
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
      // grp_dataToPrint
      // 
      this.grp_dataToPrint.Controls.Add(this.tnud_nextdischargingSetupModID);
      this.grp_dataToPrint.Controls.Add(this.tnud_nextdischargingSetupID);
      this.grp_dataToPrint.Controls.Add(this.tnud_GenericInfo3);
      this.grp_dataToPrint.Controls.Add(this.tnud_GenericInfo2);
      this.grp_dataToPrint.Controls.Add(this.tnud_GenericInfo1);
      this.grp_dataToPrint.Controls.Add(this.chk_NormalAOC);
      this.grp_dataToPrint.Controls.Add(this.ttb_DischargingType);
      this.grp_dataToPrint.Controls.Add(this.ttb_ProductionType);
      this.grp_dataToPrint.Controls.Add(this.tnud_Outs);
      this.grp_dataToPrint.Controls.Add(this.tnud_ActualCutsinPlatform);
      this.grp_dataToPrint.Controls.Add(this.tnud_SheetWidth);
      this.grp_dataToPrint.Controls.Add(this.tnud_CutLenght);
      this.grp_dataToPrint.Controls.Add(this.tnud_dischargingSetupModID);
      this.grp_dataToPrint.Controls.Add(this.tnud_dischargingSetupID);
      this.grp_dataToPrint.Location = new System.Drawing.Point(132, 193);
      this.grp_dataToPrint.Name = "grp_dataToPrint";
      this.grp_dataToPrint.Size = new System.Drawing.Size(367, 466);
      this.grp_dataToPrint.TabIndex = 110;
      this.grp_dataToPrint.TabStop = false;
      this.grp_dataToPrint.Text = "Data To Print";
      // 
      // tnud_nextdischargingSetupModID
      // 
      this.tnud_nextdischargingSetupModID.DecimalPlaces = 0;
      this.tnud_nextdischargingSetupModID.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_nextdischargingSetupModID.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_nextdischargingSetupModID.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_nextdischargingSetupModID.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_nextdischargingSetupModID.Location = new System.Drawing.Point(268, 19);
      this.tnud_nextdischargingSetupModID.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_nextdischargingSetupModID.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_nextdischargingSetupModID.Name = "tnud_nextdischargingSetupModID";
      this.tnud_nextdischargingSetupModID.ReadOnly = true;
      this.tnud_nextdischargingSetupModID.Size = new System.Drawing.Size(84, 55);
      this.tnud_nextdischargingSetupModID.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_nextdischargingSetupModID.TabIndex = 281;
      this.tnud_nextdischargingSetupModID.Text = "Next ModId";
      this.tnud_nextdischargingSetupModID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_nextdischargingSetupModID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_nextdischargingSetupModID.TranslateText = false;
      this.tnud_nextdischargingSetupModID.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_nextdischargingSetupModID.UnitMeasureVisible = false;
      this.tnud_nextdischargingSetupModID.Value = "";
      this.tnud_nextdischargingSetupModID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tnud_nextdischargingSetupID
      // 
      this.tnud_nextdischargingSetupID.DecimalPlaces = 0;
      this.tnud_nextdischargingSetupID.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_nextdischargingSetupID.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_nextdischargingSetupID.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_nextdischargingSetupID.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_nextdischargingSetupID.Location = new System.Drawing.Point(182, 19);
      this.tnud_nextdischargingSetupID.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_nextdischargingSetupID.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_nextdischargingSetupID.Name = "tnud_nextdischargingSetupID";
      this.tnud_nextdischargingSetupID.ReadOnly = true;
      this.tnud_nextdischargingSetupID.Size = new System.Drawing.Size(84, 55);
      this.tnud_nextdischargingSetupID.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_nextdischargingSetupID.TabIndex = 280;
      this.tnud_nextdischargingSetupID.Text = "Next Id";
      this.tnud_nextdischargingSetupID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_nextdischargingSetupID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_nextdischargingSetupID.TranslateText = false;
      this.tnud_nextdischargingSetupID.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_nextdischargingSetupID.UnitMeasureVisible = false;
      this.tnud_nextdischargingSetupID.Value = "";
      this.tnud_nextdischargingSetupID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tnud_GenericInfo3
      // 
      this.tnud_GenericInfo3.DecimalPlaces = 0;
      this.tnud_GenericInfo3.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_GenericInfo3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GenericInfo3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_GenericInfo3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_GenericInfo3.Location = new System.Drawing.Point(6, 383);
      this.tnud_GenericInfo3.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_GenericInfo3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_GenericInfo3.Name = "tnud_GenericInfo3";
      this.tnud_GenericInfo3.ReadOnly = true;
      this.tnud_GenericInfo3.Size = new System.Drawing.Size(347, 55);
      this.tnud_GenericInfo3.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_GenericInfo3.TabIndex = 289;
      this.tnud_GenericInfo3.Text = "Generic Info 3";
      this.tnud_GenericInfo3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GenericInfo3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GenericInfo3.TranslateText = false;
      this.tnud_GenericInfo3.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_GenericInfo3.UnitMeasureVisible = false;
      this.tnud_GenericInfo3.Value = "";
      this.tnud_GenericInfo3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tnud_GenericInfo2
      // 
      this.tnud_GenericInfo2.DecimalPlaces = 0;
      this.tnud_GenericInfo2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_GenericInfo2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GenericInfo2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_GenericInfo2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_GenericInfo2.Location = new System.Drawing.Point(6, 322);
      this.tnud_GenericInfo2.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_GenericInfo2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_GenericInfo2.Name = "tnud_GenericInfo2";
      this.tnud_GenericInfo2.ReadOnly = true;
      this.tnud_GenericInfo2.Size = new System.Drawing.Size(347, 55);
      this.tnud_GenericInfo2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_GenericInfo2.TabIndex = 288;
      this.tnud_GenericInfo2.Text = "Generic Info 2";
      this.tnud_GenericInfo2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GenericInfo2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GenericInfo2.TranslateText = false;
      this.tnud_GenericInfo2.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_GenericInfo2.UnitMeasureVisible = false;
      this.tnud_GenericInfo2.Value = "";
      this.tnud_GenericInfo2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tnud_GenericInfo1
      // 
      this.tnud_GenericInfo1.DecimalPlaces = 0;
      this.tnud_GenericInfo1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_GenericInfo1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GenericInfo1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_GenericInfo1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_GenericInfo1.Location = new System.Drawing.Point(6, 261);
      this.tnud_GenericInfo1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_GenericInfo1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_GenericInfo1.Name = "tnud_GenericInfo1";
      this.tnud_GenericInfo1.ReadOnly = true;
      this.tnud_GenericInfo1.Size = new System.Drawing.Size(347, 55);
      this.tnud_GenericInfo1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_GenericInfo1.TabIndex = 287;
      this.tnud_GenericInfo1.Text = "Generic Info 1";
      this.tnud_GenericInfo1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GenericInfo1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GenericInfo1.TranslateText = false;
      this.tnud_GenericInfo1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_GenericInfo1.UnitMeasureVisible = false;
      this.tnud_GenericInfo1.Value = "";
      this.tnud_GenericInfo1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // chk_NormalAOC
      // 
      this.chk_NormalAOC.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_NormalAOC.Checked = true;
      this.chk_NormalAOC.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_NormalAOC.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_NormalAOC.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.chk_NormalAOC.Location = new System.Drawing.Point(269, 139);
      this.chk_NormalAOC.Name = "chk_NormalAOC";
      this.chk_NormalAOC.ReadOnly = true;
      this.chk_NormalAOC.Size = new System.Drawing.Size(84, 55);
      this.chk_NormalAOC.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_NormalAOC.TabIndex = 286;
      this.chk_NormalAOC.Text = "AOC";
      this.chk_NormalAOC.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_NormalAOC.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_NormalAOC.TranslateText = false;
      // 
      // ttb_DischargingType
      // 
      this.ttb_DischargingType.CausesValidation = false;
      this.ttb_DischargingType.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_DischargingType.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_DischargingType.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DischargingType.Location = new System.Drawing.Point(179, 200);
      this.ttb_DischargingType.MaxChar = 32767;
      this.ttb_DischargingType.Multiline = false;
      this.ttb_DischargingType.Name = "ttb_DischargingType";
      this.ttb_DischargingType.PasswordChar = '\0';
      this.ttb_DischargingType.ReadOnly = true;
      this.ttb_DischargingType.Size = new System.Drawing.Size(113, 55);
      this.ttb_DischargingType.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_DischargingType.TabIndex = 285;
      this.ttb_DischargingType.Text = "Discharging Type";
      this.ttb_DischargingType.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_DischargingType.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DischargingType.TranslateText = false;
      this.ttb_DischargingType.Value = "xxxxx";
      this.ttb_DischargingType.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_ProductionType
      // 
      this.ttb_ProductionType.CausesValidation = false;
      this.ttb_ProductionType.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_ProductionType.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_ProductionType.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ProductionType.Location = new System.Drawing.Point(6, 200);
      this.ttb_ProductionType.MaxChar = 32767;
      this.ttb_ProductionType.Multiline = false;
      this.ttb_ProductionType.Name = "ttb_ProductionType";
      this.ttb_ProductionType.PasswordChar = '\0';
      this.ttb_ProductionType.ReadOnly = true;
      this.ttb_ProductionType.Size = new System.Drawing.Size(159, 55);
      this.ttb_ProductionType.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_ProductionType.TabIndex = 284;
      this.ttb_ProductionType.Text = "Production Type";
      this.ttb_ProductionType.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_ProductionType.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ProductionType.TranslateText = false;
      this.ttb_ProductionType.Value = "xxxxxxxxxx";
      this.ttb_ProductionType.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tnud_Outs
      // 
      this.tnud_Outs.DecimalPlaces = 0;
      this.tnud_Outs.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_Outs.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Outs.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Outs.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_Outs.Location = new System.Drawing.Point(179, 139);
      this.tnud_Outs.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_Outs.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_Outs.Name = "tnud_Outs";
      this.tnud_Outs.ReadOnly = true;
      this.tnud_Outs.Size = new System.Drawing.Size(84, 55);
      this.tnud_Outs.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_Outs.TabIndex = 283;
      this.tnud_Outs.Text = "Outs";
      this.tnud_Outs.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Outs.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Outs.TranslateText = false;
      this.tnud_Outs.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_Outs.Value = "";
      this.tnud_Outs.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tnud_ActualCutsinPlatform
      // 
      this.tnud_ActualCutsinPlatform.DecimalPlaces = 0;
      this.tnud_ActualCutsinPlatform.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_ActualCutsinPlatform.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_ActualCutsinPlatform.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_ActualCutsinPlatform.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_ActualCutsinPlatform.Location = new System.Drawing.Point(6, 139);
      this.tnud_ActualCutsinPlatform.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_ActualCutsinPlatform.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_ActualCutsinPlatform.Name = "tnud_ActualCutsinPlatform";
      this.tnud_ActualCutsinPlatform.ReadOnly = true;
      this.tnud_ActualCutsinPlatform.Size = new System.Drawing.Size(159, 55);
      this.tnud_ActualCutsinPlatform.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_ActualCutsinPlatform.TabIndex = 282;
      this.tnud_ActualCutsinPlatform.Text = "Actual Cuts in Platform";
      this.tnud_ActualCutsinPlatform.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ActualCutsinPlatform.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_ActualCutsinPlatform.TranslateText = false;
      this.tnud_ActualCutsinPlatform.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_ActualCutsinPlatform.Value = "";
      this.tnud_ActualCutsinPlatform.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tnud_SheetWidth
      // 
      this.tnud_SheetWidth.DecimalPlaces = 0;
      this.tnud_SheetWidth.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_SheetWidth.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_SheetWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_SheetWidth.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tnud_SheetWidth.Location = new System.Drawing.Point(269, 78);
      this.tnud_SheetWidth.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_SheetWidth.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_SheetWidth.Name = "tnud_SheetWidth";
      this.tnud_SheetWidth.ReadOnly = true;
      this.tnud_SheetWidth.Size = new System.Drawing.Size(84, 55);
      this.tnud_SheetWidth.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_SheetWidth.TabIndex = 281;
      this.tnud_SheetWidth.Text = "Sheet Width";
      this.tnud_SheetWidth.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_SheetWidth.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_SheetWidth.TranslateText = false;
      this.tnud_SheetWidth.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tnud_SheetWidth.Value = "";
      this.tnud_SheetWidth.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tnud_CutLenght
      // 
      this.tnud_CutLenght.DecimalPlaces = 0;
      this.tnud_CutLenght.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_CutLenght.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_CutLenght.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_CutLenght.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tnud_CutLenght.Location = new System.Drawing.Point(179, 78);
      this.tnud_CutLenght.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_CutLenght.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_CutLenght.Name = "tnud_CutLenght";
      this.tnud_CutLenght.ReadOnly = true;
      this.tnud_CutLenght.Size = new System.Drawing.Size(84, 55);
      this.tnud_CutLenght.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_CutLenght.TabIndex = 280;
      this.tnud_CutLenght.Text = "Cut Lenght";
      this.tnud_CutLenght.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_CutLenght.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_CutLenght.TranslateText = false;
      this.tnud_CutLenght.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tnud_CutLenght.Value = "";
      this.tnud_CutLenght.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tnud_dischargingSetupModID
      // 
      this.tnud_dischargingSetupModID.DecimalPlaces = 0;
      this.tnud_dischargingSetupModID.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_dischargingSetupModID.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_dischargingSetupModID.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_dischargingSetupModID.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_dischargingSetupModID.Location = new System.Drawing.Point(92, 19);
      this.tnud_dischargingSetupModID.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_dischargingSetupModID.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_dischargingSetupModID.Name = "tnud_dischargingSetupModID";
      this.tnud_dischargingSetupModID.ReadOnly = true;
      this.tnud_dischargingSetupModID.Size = new System.Drawing.Size(84, 55);
      this.tnud_dischargingSetupModID.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_dischargingSetupModID.TabIndex = 279;
      this.tnud_dischargingSetupModID.Text = "ModId";
      this.tnud_dischargingSetupModID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_dischargingSetupModID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_dischargingSetupModID.TranslateText = false;
      this.tnud_dischargingSetupModID.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_dischargingSetupModID.UnitMeasureVisible = false;
      this.tnud_dischargingSetupModID.Value = "";
      this.tnud_dischargingSetupModID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tnud_dischargingSetupID
      // 
      this.tnud_dischargingSetupID.DecimalPlaces = 0;
      this.tnud_dischargingSetupID.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_dischargingSetupID.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_dischargingSetupID.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_dischargingSetupID.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_dischargingSetupID.Location = new System.Drawing.Point(6, 19);
      this.tnud_dischargingSetupID.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_dischargingSetupID.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_dischargingSetupID.Name = "tnud_dischargingSetupID";
      this.tnud_dischargingSetupID.ReadOnly = true;
      this.tnud_dischargingSetupID.Size = new System.Drawing.Size(84, 55);
      this.tnud_dischargingSetupID.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_dischargingSetupID.TabIndex = 277;
      this.tnud_dischargingSetupID.Text = "Id";
      this.tnud_dischargingSetupID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_dischargingSetupID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_dischargingSetupID.TranslateText = false;
      this.tnud_dischargingSetupID.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_dischargingSetupID.UnitMeasureVisible = false;
      this.tnud_dischargingSetupID.Value = "";
      this.tnud_dischargingSetupID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // chk_Discharge
      // 
      this.chk_Discharge.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Discharge.Checked = true;
      this.chk_Discharge.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_Discharge.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Discharge.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Discharge.Location = new System.Drawing.Point(522, 202);
      this.chk_Discharge.Name = "chk_Discharge";
      this.chk_Discharge.ReadOnly = true;
      this.chk_Discharge.Size = new System.Drawing.Size(100, 55);
      this.chk_Discharge.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_Discharge.TabIndex = 287;
      this.chk_Discharge.Text = "Discharge";
      this.chk_Discharge.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Discharge.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_Discharge.TranslateText = false;
      // 
      // chk_DischargeCompleted
      // 
      this.chk_DischargeCompleted.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_DischargeCompleted.Checked = true;
      this.chk_DischargeCompleted.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_DischargeCompleted.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_DischargeCompleted.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.chk_DischargeCompleted.Location = new System.Drawing.Point(522, 263);
      this.chk_DischargeCompleted.Name = "chk_DischargeCompleted";
      this.chk_DischargeCompleted.ReadOnly = true;
      this.chk_DischargeCompleted.Size = new System.Drawing.Size(100, 55);
      this.chk_DischargeCompleted.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_DischargeCompleted.TabIndex = 288;
      this.chk_DischargeCompleted.Text = "End Discharge";
      this.chk_DischargeCompleted.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_DischargeCompleted.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_DischargeCompleted.TranslateText = false;
      // 
      // tnud_DischargeCounter
      // 
      this.tnud_DischargeCounter.DecimalPlaces = 0;
      this.tnud_DischargeCounter.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_DischargeCounter.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_DischargeCounter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_DischargeCounter.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_DischargeCounter.Location = new System.Drawing.Point(522, 332);
      this.tnud_DischargeCounter.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_DischargeCounter.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_DischargeCounter.Name = "tnud_DischargeCounter";
      this.tnud_DischargeCounter.ReadOnly = true;
      this.tnud_DischargeCounter.Size = new System.Drawing.Size(165, 55);
      this.tnud_DischargeCounter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_DischargeCounter.TabIndex = 289;
      this.tnud_DischargeCounter.Text = "Discharge Counter";
      this.tnud_DischargeCounter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_DischargeCounter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_DischargeCounter.TranslateText = false;
      this.tnud_DischargeCounter.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_DischargeCounter.Value = "";
      this.tnud_DischargeCounter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tnud_AOCDischargeCounter
      // 
      this.tnud_AOCDischargeCounter.DecimalPlaces = 0;
      this.tnud_AOCDischargeCounter.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_AOCDischargeCounter.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_AOCDischargeCounter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_AOCDischargeCounter.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_AOCDischargeCounter.Location = new System.Drawing.Point(522, 395);
      this.tnud_AOCDischargeCounter.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.tnud_AOCDischargeCounter.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_AOCDischargeCounter.Name = "tnud_AOCDischargeCounter";
      this.tnud_AOCDischargeCounter.ReadOnly = true;
      this.tnud_AOCDischargeCounter.Size = new System.Drawing.Size(165, 55);
      this.tnud_AOCDischargeCounter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_AOCDischargeCounter.TabIndex = 290;
      this.tnud_AOCDischargeCounter.Text = "AOC Discharge Counter";
      this.tnud_AOCDischargeCounter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_AOCDischargeCounter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_AOCDischargeCounter.TranslateText = false;
      this.tnud_AOCDischargeCounter.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_AOCDischargeCounter.Value = "";
      this.tnud_AOCDischargeCounter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // Frm_StackerSettingsStatus
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tnud_AOCDischargeCounter);
      this.Controls.Add(this.tnud_DischargeCounter);
      this.Controls.Add(this.chk_DischargeCompleted);
      this.Controls.Add(this.chk_Discharge);
      this.Controls.Add(this.grp_dataToPrint);
      this.Name = "Frm_StackerSettingsStatus";
      this.Controls.SetChildIndex(this.grp_dataToPrint, 0);
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
      this.Controls.SetChildIndex(this.chk_Discharge, 0);
      this.Controls.SetChildIndex(this.chk_DischargeCompleted, 0);
      this.Controls.SetChildIndex(this.tnud_DischargeCounter, 0);
      this.Controls.SetChildIndex(this.tnud_AOCDischargeCounter, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.grp_dataToPrint.ResumeLayout(false);
      this.ResumeLayout(false);

		}
		#endregion

	}
}

