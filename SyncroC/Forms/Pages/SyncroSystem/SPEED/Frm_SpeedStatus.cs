using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.SyncroSystem.SPEED
{
	public class Frm_SpeedStatus : SyncroC.Forms.Pages.SyncroSystem.SPEED.Frm_SpeedSubGroup
	{
        private TabControl tab_SpeedGroup;
        private TabPage tab_Speeds;
        private Label lbl_MWSpeed;
        private Label lbl_DBSpeed;
        private SyncroC.Controls.TitledNumericUpDown nud_DBSpeed;
        private SyncroC.Controls.TitledNumericUpDown nud_MWSpeed;
        private SyncroC.Controls.TitledNumericUpDown nud_LastAOCSpeed;
        private Label lbl_LastAOCSpeed;
        private SyncroC.Controls.TitledNumericUpDown nud_ProductionSpeed;
        private Label lbl_ProductionSpeed;
        private SyncroC.Controls.TitledNumericUpDown nud_LineSpeed;
        private Label lbl_LineSpeed;
        private SyncroC.Controls.TitledNumericUpDown nud_LineRunStatusASR;
        private Label lbl_LineRunStatusASR;
        private Label lbl_ValidMWConditions;
        private SyncroC.Controls.TitledNumericUpDown nud_OperatorProductionSpeed;
        private Label lbl_OperatorProductionSpeed;
        private CheckBox chk_ValidMWConditions;
        private Label lbl_SpeedCommand;
        private Label lbl_SlowerPushButtonPressed;
        private Label lbl_FasterPushButtonPressed;
        private SyncroC.Controls.TitledNumericUpDown nud_SpeedControlReason;
        private Label lbl_SpeedControlReason;
        private SyncroC.Controls.TitledNumericUpDown nud_FasterPushButtonPressed;
        private SyncroC.Controls.TitledNumericUpDown nud_SlowerPushButtonPressed;
        private Label lbl_SpeedCommandDescription;
        private SyncroC.Controls.TitledNumericUpDown nud_ExitBelt3Speed;
        private Label lbl_ExitBelt3Speed;
        private SyncroC.Controls.TitledNumericUpDown nud_ExitBelt2Speed;
        private Label lbl_ExitBelt2Speed;
        private SyncroC.Controls.TitledNumericUpDown nud_ExitBelt1Speed;
        private Label lbl_ExitBelt1Speed;
        private SyncroC.Controls.TitledNumericUpDown nud_CutlengthL1;
        private Label lbl_CutL1;
        private SyncroC.Controls.TitledNumericUpDown nud_CutlengthL3;
        private Label lbl_CutL3;
        private SyncroC.Controls.TitledNumericUpDown nud_CutlengthL2;
        private Label lbl_CutL2;
        private Label lbl_CutLenReqL1;
        private SyncroC.Controls.TitledNumericUpDown nud_CutlengthReqL3;
        private SyncroC.Controls.TitledNumericUpDown nud_CutlengthReqL2;
        private SyncroC.Controls.TitledNumericUpDown nud_CutlengthReqL1;
        private Label lbl_CutLenDiffL1;
        private SyncroC.Controls.TitledNumericUpDown nud_CutlengthDiff3;
        private SyncroC.Controls.TitledNumericUpDown nud_CutlengthDiff2;
        private SyncroC.Controls.TitledNumericUpDown nud_CutlengthDiff1;
        private Label lbl_NotValidConditions;
        private Label lbl_CutLenDiffL3;
        private Label lbl_CutLenDiffL2;
        private Label lbl_CutLenReqL3;
        private Label lbl_CutLenReqL2;
		private System.ComponentModel.IContainer components = null;

		public Frm_SpeedStatus()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SpeedStatus));
      this.tab_SpeedGroup = new System.Windows.Forms.TabControl();
      this.tab_Speeds = new System.Windows.Forms.TabPage();
      this.lbl_NotValidConditions = new System.Windows.Forms.Label();
      this.lbl_CutLenDiffL3 = new System.Windows.Forms.Label();
      this.lbl_CutLenDiffL2 = new System.Windows.Forms.Label();
      this.lbl_CutLenReqL3 = new System.Windows.Forms.Label();
      this.lbl_CutLenReqL2 = new System.Windows.Forms.Label();
      this.lbl_CutLenDiffL1 = new System.Windows.Forms.Label();
      this.lbl_CutLenReqL1 = new System.Windows.Forms.Label();
      this.lbl_CutL3 = new System.Windows.Forms.Label();
      this.lbl_CutL2 = new System.Windows.Forms.Label();
      this.lbl_CutL1 = new System.Windows.Forms.Label();
      this.nud_ExitBelt3Speed = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_ExitBelt3Speed = new System.Windows.Forms.Label();
      this.nud_ExitBelt2Speed = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_ExitBelt2Speed = new System.Windows.Forms.Label();
      this.nud_ExitBelt1Speed = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_ExitBelt1Speed = new System.Windows.Forms.Label();
      this.lbl_SpeedCommandDescription = new System.Windows.Forms.Label();
      this.nud_FasterPushButtonPressed = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_SlowerPushButtonPressed = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_SlowerPushButtonPressed = new System.Windows.Forms.Label();
      this.lbl_FasterPushButtonPressed = new System.Windows.Forms.Label();
      this.nud_SpeedControlReason = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_SpeedControlReason = new System.Windows.Forms.Label();
      this.lbl_SpeedCommand = new System.Windows.Forms.Label();
      this.chk_ValidMWConditions = new System.Windows.Forms.CheckBox();
      this.lbl_ValidMWConditions = new System.Windows.Forms.Label();
      this.nud_OperatorProductionSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_OperatorProductionSpeed = new System.Windows.Forms.Label();
      this.nud_LineRunStatusASR = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_LineRunStatusASR = new System.Windows.Forms.Label();
      this.nud_LineSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_LineSpeed = new System.Windows.Forms.Label();
      this.nud_LastAOCSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_LastAOCSpeed = new System.Windows.Forms.Label();
      this.nud_ProductionSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_ProductionSpeed = new System.Windows.Forms.Label();
      this.nud_MWSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_DBSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_DBSpeed = new System.Windows.Forms.Label();
      this.lbl_MWSpeed = new System.Windows.Forms.Label();
      this.nud_CutlengthDiff3 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_CutlengthDiff2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_CutlengthDiff1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_CutlengthReqL3 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_CutlengthReqL1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_CutlengthL3 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_CutlengthL2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_CutlengthL1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_CutlengthReqL2 = new SyncroC.Controls.TitledNumericUpDown();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.tab_SpeedGroup.SuspendLayout();
      this.tab_Speeds.SuspendLayout();
      this.SuspendLayout();
      // 
      // tab_TabB
      // 
      this.tab_TabB.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // tab_SpeedGroup
      // 
      this.tab_SpeedGroup.Controls.Add(this.tab_Speeds);
      this.tab_SpeedGroup.Location = new System.Drawing.Point(30, 193);
      this.tab_SpeedGroup.Name = "tab_SpeedGroup";
      this.tab_SpeedGroup.SelectedIndex = 0;
      this.tab_SpeedGroup.Size = new System.Drawing.Size(1171, 519);
      this.tab_SpeedGroup.TabIndex = 110;
      // 
      // tab_Speeds
      // 
      this.tab_Speeds.Controls.Add(this.lbl_NotValidConditions);
      this.tab_Speeds.Controls.Add(this.lbl_CutLenDiffL3);
      this.tab_Speeds.Controls.Add(this.lbl_CutLenDiffL2);
      this.tab_Speeds.Controls.Add(this.lbl_CutLenReqL3);
      this.tab_Speeds.Controls.Add(this.lbl_CutLenReqL2);
      this.tab_Speeds.Controls.Add(this.lbl_CutLenDiffL1);
      this.tab_Speeds.Controls.Add(this.lbl_CutLenReqL1);
      this.tab_Speeds.Controls.Add(this.lbl_CutL3);
      this.tab_Speeds.Controls.Add(this.lbl_CutL2);
      this.tab_Speeds.Controls.Add(this.lbl_CutL1);
      this.tab_Speeds.Controls.Add(this.nud_ExitBelt3Speed);
      this.tab_Speeds.Controls.Add(this.lbl_ExitBelt3Speed);
      this.tab_Speeds.Controls.Add(this.nud_ExitBelt2Speed);
      this.tab_Speeds.Controls.Add(this.lbl_ExitBelt2Speed);
      this.tab_Speeds.Controls.Add(this.nud_ExitBelt1Speed);
      this.tab_Speeds.Controls.Add(this.lbl_ExitBelt1Speed);
      this.tab_Speeds.Controls.Add(this.lbl_SpeedCommandDescription);
      this.tab_Speeds.Controls.Add(this.nud_FasterPushButtonPressed);
      this.tab_Speeds.Controls.Add(this.nud_SlowerPushButtonPressed);
      this.tab_Speeds.Controls.Add(this.lbl_SlowerPushButtonPressed);
      this.tab_Speeds.Controls.Add(this.lbl_FasterPushButtonPressed);
      this.tab_Speeds.Controls.Add(this.nud_SpeedControlReason);
      this.tab_Speeds.Controls.Add(this.lbl_SpeedControlReason);
      this.tab_Speeds.Controls.Add(this.lbl_SpeedCommand);
      this.tab_Speeds.Controls.Add(this.chk_ValidMWConditions);
      this.tab_Speeds.Controls.Add(this.lbl_ValidMWConditions);
      this.tab_Speeds.Controls.Add(this.nud_OperatorProductionSpeed);
      this.tab_Speeds.Controls.Add(this.lbl_OperatorProductionSpeed);
      this.tab_Speeds.Controls.Add(this.nud_LineRunStatusASR);
      this.tab_Speeds.Controls.Add(this.lbl_LineRunStatusASR);
      this.tab_Speeds.Controls.Add(this.nud_LineSpeed);
      this.tab_Speeds.Controls.Add(this.lbl_LineSpeed);
      this.tab_Speeds.Controls.Add(this.nud_LastAOCSpeed);
      this.tab_Speeds.Controls.Add(this.lbl_LastAOCSpeed);
      this.tab_Speeds.Controls.Add(this.nud_ProductionSpeed);
      this.tab_Speeds.Controls.Add(this.lbl_ProductionSpeed);
      this.tab_Speeds.Controls.Add(this.nud_MWSpeed);
      this.tab_Speeds.Controls.Add(this.nud_DBSpeed);
      this.tab_Speeds.Controls.Add(this.lbl_DBSpeed);
      this.tab_Speeds.Controls.Add(this.lbl_MWSpeed);
      this.tab_Speeds.Controls.Add(this.nud_CutlengthDiff3);
      this.tab_Speeds.Controls.Add(this.nud_CutlengthDiff2);
      this.tab_Speeds.Controls.Add(this.nud_CutlengthDiff1);
      this.tab_Speeds.Controls.Add(this.nud_CutlengthReqL3);
      this.tab_Speeds.Controls.Add(this.nud_CutlengthReqL1);
      this.tab_Speeds.Controls.Add(this.nud_CutlengthL3);
      this.tab_Speeds.Controls.Add(this.nud_CutlengthL2);
      this.tab_Speeds.Controls.Add(this.nud_CutlengthL1);
      this.tab_Speeds.Controls.Add(this.nud_CutlengthReqL2);
      this.tab_Speeds.Location = new System.Drawing.Point(4, 22);
      this.tab_Speeds.Name = "tab_Speeds";
      this.tab_Speeds.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Speeds.Size = new System.Drawing.Size(1163, 493);
      this.tab_Speeds.TabIndex = 0;
      this.tab_Speeds.Text = "Speeds";
      this.tab_Speeds.UseVisualStyleBackColor = true;
      // 
      // lbl_NotValidConditions
      // 
      this.lbl_NotValidConditions.AutoSize = true;
      this.lbl_NotValidConditions.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_NotValidConditions.ForeColor = System.Drawing.Color.Red;
      this.lbl_NotValidConditions.Location = new System.Drawing.Point(560, 34);
      this.lbl_NotValidConditions.Name = "lbl_NotValidConditions";
      this.lbl_NotValidConditions.Size = new System.Drawing.Size(464, 22);
      this.lbl_NotValidConditions.TabIndex = 368;
      this.lbl_NotValidConditions.Text = "Cut Length Calculation: Waiting Web At Cut-Off-Knife!!!";
      this.lbl_NotValidConditions.Visible = false;
      // 
      // lbl_CutLenDiffL3
      // 
      this.lbl_CutLenDiffL3.AutoSize = true;
      this.lbl_CutLenDiffL3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CutLenDiffL3.Location = new System.Drawing.Point(932, 57);
      this.lbl_CutLenDiffL3.Name = "lbl_CutLenDiffL3";
      this.lbl_CutLenDiffL3.Size = new System.Drawing.Size(65, 16);
      this.lbl_CutLenDiffL3.TabIndex = 372;
      this.lbl_CutLenDiffL3.Text = "Difference";
      // 
      // lbl_CutLenDiffL2
      // 
      this.lbl_CutLenDiffL2.AutoSize = true;
      this.lbl_CutLenDiffL2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CutLenDiffL2.Location = new System.Drawing.Point(747, 57);
      this.lbl_CutLenDiffL2.Name = "lbl_CutLenDiffL2";
      this.lbl_CutLenDiffL2.Size = new System.Drawing.Size(65, 16);
      this.lbl_CutLenDiffL2.TabIndex = 371;
      this.lbl_CutLenDiffL2.Text = "Difference";
      // 
      // lbl_CutLenReqL3
      // 
      this.lbl_CutLenReqL3.AutoSize = true;
      this.lbl_CutLenReqL3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CutLenReqL3.Location = new System.Drawing.Point(932, 36);
      this.lbl_CutLenReqL3.Name = "lbl_CutLenReqL3";
      this.lbl_CutLenReqL3.Size = new System.Drawing.Size(84, 16);
      this.lbl_CutLenReqL3.TabIndex = 370;
      this.lbl_CutLenReqL3.Text = "Len Required";
      // 
      // lbl_CutLenReqL2
      // 
      this.lbl_CutLenReqL2.AutoSize = true;
      this.lbl_CutLenReqL2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CutLenReqL2.Location = new System.Drawing.Point(747, 36);
      this.lbl_CutLenReqL2.Name = "lbl_CutLenReqL2";
      this.lbl_CutLenReqL2.Size = new System.Drawing.Size(84, 16);
      this.lbl_CutLenReqL2.TabIndex = 369;
      this.lbl_CutLenReqL2.Text = "Len Required";
      // 
      // lbl_CutLenDiffL1
      // 
      this.lbl_CutLenDiffL1.AutoSize = true;
      this.lbl_CutLenDiffL1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CutLenDiffL1.Location = new System.Drawing.Point(557, 57);
      this.lbl_CutLenDiffL1.Name = "lbl_CutLenDiffL1";
      this.lbl_CutLenDiffL1.Size = new System.Drawing.Size(65, 16);
      this.lbl_CutLenDiffL1.TabIndex = 367;
      this.lbl_CutLenDiffL1.Text = "Difference";
      // 
      // lbl_CutLenReqL1
      // 
      this.lbl_CutLenReqL1.AutoSize = true;
      this.lbl_CutLenReqL1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CutLenReqL1.Location = new System.Drawing.Point(557, 36);
      this.lbl_CutLenReqL1.Name = "lbl_CutLenReqL1";
      this.lbl_CutLenReqL1.Size = new System.Drawing.Size(84, 16);
      this.lbl_CutLenReqL1.TabIndex = 363;
      this.lbl_CutLenReqL1.Text = "Len Required";
      // 
      // lbl_CutL3
      // 
      this.lbl_CutL3.AutoSize = true;
      this.lbl_CutL3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CutL3.Location = new System.Drawing.Point(932, 15);
      this.lbl_CutL3.Name = "lbl_CutL3";
      this.lbl_CutL3.Size = new System.Drawing.Size(89, 16);
      this.lbl_CutL3.TabIndex = 354;
      this.lbl_CutL3.Text = "Cut Lenght L3";
      // 
      // lbl_CutL2
      // 
      this.lbl_CutL2.AutoSize = true;
      this.lbl_CutL2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CutL2.Location = new System.Drawing.Point(747, 15);
      this.lbl_CutL2.Name = "lbl_CutL2";
      this.lbl_CutL2.Size = new System.Drawing.Size(89, 16);
      this.lbl_CutL2.TabIndex = 352;
      this.lbl_CutL2.Text = "Cut Lenght L2";
      // 
      // lbl_CutL1
      // 
      this.lbl_CutL1.AutoSize = true;
      this.lbl_CutL1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CutL1.Location = new System.Drawing.Point(557, 15);
      this.lbl_CutL1.Name = "lbl_CutL1";
      this.lbl_CutL1.Size = new System.Drawing.Size(89, 16);
      this.lbl_CutL1.TabIndex = 350;
      this.lbl_CutL1.Text = "Cut Lenght L1";
      // 
      // nud_ExitBelt3Speed
      // 
      this.nud_ExitBelt3Speed.DecimalPlaces = 0;
      this.nud_ExitBelt3Speed.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ExitBelt3Speed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ExitBelt3Speed.ImagePassword = null;
      this.nud_ExitBelt3Speed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ExitBelt3Speed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_ExitBelt3Speed.Location = new System.Drawing.Point(421, 393);
      this.nud_ExitBelt3Speed.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_ExitBelt3Speed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ExitBelt3Speed.Name = "nud_ExitBelt3Speed";
      this.nud_ExitBelt3Speed.ReadOnly = true;
      this.nud_ExitBelt3Speed.Size = new System.Drawing.Size(76, 19);
      this.nud_ExitBelt3Speed.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_ExitBelt3Speed.TabIndex = 349;
      this.nud_ExitBelt3Speed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_ExitBelt3Speed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ExitBelt3Speed.TranslateText = false;
      this.nud_ExitBelt3Speed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_ExitBelt3Speed.UnitMeasureVisible = false;
      this.nud_ExitBelt3Speed.Value = "0";
      this.nud_ExitBelt3Speed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_ExitBelt3Speed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // lbl_ExitBelt3Speed
      // 
      this.lbl_ExitBelt3Speed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ExitBelt3Speed.Location = new System.Drawing.Point(17, 392);
      this.lbl_ExitBelt3Speed.Name = "lbl_ExitBelt3Speed";
      this.lbl_ExitBelt3Speed.Size = new System.Drawing.Size(384, 20);
      this.lbl_ExitBelt3Speed.TabIndex = 348;
      this.lbl_ExitBelt3Speed.Text = "Exit Belt 3 Speed ";
      this.lbl_ExitBelt3Speed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // nud_ExitBelt2Speed
      // 
      this.nud_ExitBelt2Speed.DecimalPlaces = 0;
      this.nud_ExitBelt2Speed.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ExitBelt2Speed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ExitBelt2Speed.ImagePassword = null;
      this.nud_ExitBelt2Speed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ExitBelt2Speed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_ExitBelt2Speed.Location = new System.Drawing.Point(421, 367);
      this.nud_ExitBelt2Speed.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_ExitBelt2Speed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ExitBelt2Speed.Name = "nud_ExitBelt2Speed";
      this.nud_ExitBelt2Speed.ReadOnly = true;
      this.nud_ExitBelt2Speed.Size = new System.Drawing.Size(76, 19);
      this.nud_ExitBelt2Speed.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_ExitBelt2Speed.TabIndex = 347;
      this.nud_ExitBelt2Speed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_ExitBelt2Speed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ExitBelt2Speed.TranslateText = false;
      this.nud_ExitBelt2Speed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_ExitBelt2Speed.UnitMeasureVisible = false;
      this.nud_ExitBelt2Speed.Value = "0";
      this.nud_ExitBelt2Speed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_ExitBelt2Speed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // lbl_ExitBelt2Speed
      // 
      this.lbl_ExitBelt2Speed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ExitBelt2Speed.Location = new System.Drawing.Point(17, 366);
      this.lbl_ExitBelt2Speed.Name = "lbl_ExitBelt2Speed";
      this.lbl_ExitBelt2Speed.Size = new System.Drawing.Size(384, 20);
      this.lbl_ExitBelt2Speed.TabIndex = 346;
      this.lbl_ExitBelt2Speed.Text = "Exit Belt 3 Speed ";
      this.lbl_ExitBelt2Speed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // nud_ExitBelt1Speed
      // 
      this.nud_ExitBelt1Speed.DecimalPlaces = 0;
      this.nud_ExitBelt1Speed.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ExitBelt1Speed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ExitBelt1Speed.ImagePassword = null;
      this.nud_ExitBelt1Speed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ExitBelt1Speed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_ExitBelt1Speed.Location = new System.Drawing.Point(421, 341);
      this.nud_ExitBelt1Speed.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_ExitBelt1Speed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ExitBelt1Speed.Name = "nud_ExitBelt1Speed";
      this.nud_ExitBelt1Speed.ReadOnly = true;
      this.nud_ExitBelt1Speed.Size = new System.Drawing.Size(76, 19);
      this.nud_ExitBelt1Speed.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_ExitBelt1Speed.TabIndex = 345;
      this.nud_ExitBelt1Speed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_ExitBelt1Speed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ExitBelt1Speed.TranslateText = false;
      this.nud_ExitBelt1Speed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_ExitBelt1Speed.UnitMeasureVisible = false;
      this.nud_ExitBelt1Speed.Value = "0";
      this.nud_ExitBelt1Speed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_ExitBelt1Speed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // lbl_ExitBelt1Speed
      // 
      this.lbl_ExitBelt1Speed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ExitBelt1Speed.Location = new System.Drawing.Point(17, 340);
      this.lbl_ExitBelt1Speed.Name = "lbl_ExitBelt1Speed";
      this.lbl_ExitBelt1Speed.Size = new System.Drawing.Size(384, 20);
      this.lbl_ExitBelt1Speed.TabIndex = 344;
      this.lbl_ExitBelt1Speed.Text = "Exit Belt 1 Speed ";
      this.lbl_ExitBelt1Speed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_SpeedCommandDescription
      // 
      this.lbl_SpeedCommandDescription.AutoSize = true;
      this.lbl_SpeedCommandDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SpeedCommandDescription.Location = new System.Drawing.Point(424, 223);
      this.lbl_SpeedCommandDescription.Name = "lbl_SpeedCommandDescription";
      this.lbl_SpeedCommandDescription.Size = new System.Drawing.Size(38, 15);
      this.lbl_SpeedCommandDescription.TabIndex = 343;
      this.lbl_SpeedCommandDescription.Text = "Value";
      // 
      // nud_FasterPushButtonPressed
      // 
      this.nud_FasterPushButtonPressed.DecimalPlaces = 0;
      this.nud_FasterPushButtonPressed.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_FasterPushButtonPressed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_FasterPushButtonPressed.ImagePassword = null;
      this.nud_FasterPushButtonPressed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_FasterPushButtonPressed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_FasterPushButtonPressed.Location = new System.Drawing.Point(421, 262);
      this.nud_FasterPushButtonPressed.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_FasterPushButtonPressed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_FasterPushButtonPressed.Name = "nud_FasterPushButtonPressed";
      this.nud_FasterPushButtonPressed.ReadOnly = true;
      this.nud_FasterPushButtonPressed.Size = new System.Drawing.Size(76, 19);
      this.nud_FasterPushButtonPressed.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_FasterPushButtonPressed.TabIndex = 342;
      this.nud_FasterPushButtonPressed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_FasterPushButtonPressed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_FasterPushButtonPressed.TranslateText = false;
      this.nud_FasterPushButtonPressed.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_FasterPushButtonPressed.UnitMeasureVisible = false;
      this.nud_FasterPushButtonPressed.Value = "0";
      this.nud_FasterPushButtonPressed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_FasterPushButtonPressed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_SlowerPushButtonPressed
      // 
      this.nud_SlowerPushButtonPressed.DecimalPlaces = 0;
      this.nud_SlowerPushButtonPressed.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_SlowerPushButtonPressed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SlowerPushButtonPressed.ImagePassword = null;
      this.nud_SlowerPushButtonPressed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_SlowerPushButtonPressed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_SlowerPushButtonPressed.Location = new System.Drawing.Point(421, 281);
      this.nud_SlowerPushButtonPressed.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_SlowerPushButtonPressed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_SlowerPushButtonPressed.Name = "nud_SlowerPushButtonPressed";
      this.nud_SlowerPushButtonPressed.ReadOnly = true;
      this.nud_SlowerPushButtonPressed.Size = new System.Drawing.Size(76, 19);
      this.nud_SlowerPushButtonPressed.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_SlowerPushButtonPressed.TabIndex = 341;
      this.nud_SlowerPushButtonPressed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_SlowerPushButtonPressed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SlowerPushButtonPressed.TranslateText = false;
      this.nud_SlowerPushButtonPressed.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_SlowerPushButtonPressed.UnitMeasureVisible = false;
      this.nud_SlowerPushButtonPressed.Value = "0";
      this.nud_SlowerPushButtonPressed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_SlowerPushButtonPressed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // lbl_SlowerPushButtonPressed
      // 
      this.lbl_SlowerPushButtonPressed.AutoSize = true;
      this.lbl_SlowerPushButtonPressed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SlowerPushButtonPressed.Location = new System.Drawing.Point(18, 284);
      this.lbl_SlowerPushButtonPressed.Name = "lbl_SlowerPushButtonPressed";
      this.lbl_SlowerPushButtonPressed.Size = new System.Drawing.Size(175, 16);
      this.lbl_SlowerPushButtonPressed.TabIndex = 340;
      this.lbl_SlowerPushButtonPressed.Text = "Slower Push Button Pressed";
      // 
      // lbl_FasterPushButtonPressed
      // 
      this.lbl_FasterPushButtonPressed.AutoSize = true;
      this.lbl_FasterPushButtonPressed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_FasterPushButtonPressed.Location = new System.Drawing.Point(18, 265);
      this.lbl_FasterPushButtonPressed.Name = "lbl_FasterPushButtonPressed";
      this.lbl_FasterPushButtonPressed.Size = new System.Drawing.Size(173, 16);
      this.lbl_FasterPushButtonPressed.TabIndex = 338;
      this.lbl_FasterPushButtonPressed.Text = "Faster Push Button Pressed";
      // 
      // nud_SpeedControlReason
      // 
      this.nud_SpeedControlReason.DecimalPlaces = 0;
      this.nud_SpeedControlReason.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_SpeedControlReason.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SpeedControlReason.ImagePassword = null;
      this.nud_SpeedControlReason.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_SpeedControlReason.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_SpeedControlReason.Location = new System.Drawing.Point(421, 242);
      this.nud_SpeedControlReason.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_SpeedControlReason.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_SpeedControlReason.Name = "nud_SpeedControlReason";
      this.nud_SpeedControlReason.ReadOnly = true;
      this.nud_SpeedControlReason.Size = new System.Drawing.Size(76, 19);
      this.nud_SpeedControlReason.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_SpeedControlReason.TabIndex = 337;
      this.nud_SpeedControlReason.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_SpeedControlReason.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SpeedControlReason.TranslateText = false;
      this.nud_SpeedControlReason.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_SpeedControlReason.UnitMeasureVisible = false;
      this.nud_SpeedControlReason.Value = "0";
      this.nud_SpeedControlReason.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_SpeedControlReason.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // lbl_SpeedControlReason
      // 
      this.lbl_SpeedControlReason.AutoSize = true;
      this.lbl_SpeedControlReason.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SpeedControlReason.Location = new System.Drawing.Point(17, 246);
      this.lbl_SpeedControlReason.Name = "lbl_SpeedControlReason";
      this.lbl_SpeedControlReason.Size = new System.Drawing.Size(138, 16);
      this.lbl_SpeedControlReason.TabIndex = 336;
      this.lbl_SpeedControlReason.Text = "Speed Control Reason";
      // 
      // lbl_SpeedCommand
      // 
      this.lbl_SpeedCommand.AutoSize = true;
      this.lbl_SpeedCommand.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SpeedCommand.Location = new System.Drawing.Point(17, 225);
      this.lbl_SpeedCommand.Name = "lbl_SpeedCommand";
      this.lbl_SpeedCommand.Size = new System.Drawing.Size(108, 16);
      this.lbl_SpeedCommand.TabIndex = 334;
      this.lbl_SpeedCommand.Text = "Speed Command";
      // 
      // chk_ValidMWConditions
      // 
      this.chk_ValidMWConditions.AutoSize = true;
      this.chk_ValidMWConditions.Location = new System.Drawing.Point(421, 206);
      this.chk_ValidMWConditions.Name = "chk_ValidMWConditions";
      this.chk_ValidMWConditions.Size = new System.Drawing.Size(15, 14);
      this.chk_ValidMWConditions.TabIndex = 333;
      this.chk_ValidMWConditions.UseVisualStyleBackColor = true;
      // 
      // lbl_ValidMWConditions
      // 
      this.lbl_ValidMWConditions.AutoSize = true;
      this.lbl_ValidMWConditions.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ValidMWConditions.Location = new System.Drawing.Point(17, 206);
      this.lbl_ValidMWConditions.Name = "lbl_ValidMWConditions";
      this.lbl_ValidMWConditions.Size = new System.Drawing.Size(130, 16);
      this.lbl_ValidMWConditions.TabIndex = 189;
      this.lbl_ValidMWConditions.Text = "Valid MW Conditions";
      // 
      // nud_OperatorProductionSpeed
      // 
      this.nud_OperatorProductionSpeed.DecimalPlaces = 0;
      this.nud_OperatorProductionSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_OperatorProductionSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_OperatorProductionSpeed.ImagePassword = null;
      this.nud_OperatorProductionSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_OperatorProductionSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_OperatorProductionSpeed.Location = new System.Drawing.Point(421, 142);
      this.nud_OperatorProductionSpeed.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_OperatorProductionSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_OperatorProductionSpeed.Name = "nud_OperatorProductionSpeed";
      this.nud_OperatorProductionSpeed.ReadOnly = true;
      this.nud_OperatorProductionSpeed.Size = new System.Drawing.Size(76, 19);
      this.nud_OperatorProductionSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_OperatorProductionSpeed.TabIndex = 188;
      this.nud_OperatorProductionSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_OperatorProductionSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_OperatorProductionSpeed.TranslateText = false;
      this.nud_OperatorProductionSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_OperatorProductionSpeed.UnitMeasureVisible = false;
      this.nud_OperatorProductionSpeed.Value = "0";
      this.nud_OperatorProductionSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_OperatorProductionSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // lbl_OperatorProductionSpeed
      // 
      this.lbl_OperatorProductionSpeed.AutoSize = true;
      this.lbl_OperatorProductionSpeed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_OperatorProductionSpeed.Location = new System.Drawing.Point(17, 146);
      this.lbl_OperatorProductionSpeed.Name = "lbl_OperatorProductionSpeed";
      this.lbl_OperatorProductionSpeed.Size = new System.Drawing.Size(165, 16);
      this.lbl_OperatorProductionSpeed.TabIndex = 187;
      this.lbl_OperatorProductionSpeed.Text = "Operator Production Speed";
      // 
      // nud_LineRunStatusASR
      // 
      this.nud_LineRunStatusASR.DecimalPlaces = 0;
      this.nud_LineRunStatusASR.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LineRunStatusASR.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LineRunStatusASR.ImagePassword = null;
      this.nud_LineRunStatusASR.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LineRunStatusASR.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LineRunStatusASR.Location = new System.Drawing.Point(421, 183);
      this.nud_LineRunStatusASR.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LineRunStatusASR.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LineRunStatusASR.Name = "nud_LineRunStatusASR";
      this.nud_LineRunStatusASR.ReadOnly = true;
      this.nud_LineRunStatusASR.Size = new System.Drawing.Size(76, 19);
      this.nud_LineRunStatusASR.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LineRunStatusASR.TabIndex = 186;
      this.nud_LineRunStatusASR.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LineRunStatusASR.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LineRunStatusASR.TranslateText = false;
      this.nud_LineRunStatusASR.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LineRunStatusASR.UnitMeasureVisible = false;
      this.nud_LineRunStatusASR.Value = "0";
      this.nud_LineRunStatusASR.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LineRunStatusASR.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // lbl_LineRunStatusASR
      // 
      this.lbl_LineRunStatusASR.AutoSize = true;
      this.lbl_LineRunStatusASR.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_LineRunStatusASR.Location = new System.Drawing.Point(17, 187);
      this.lbl_LineRunStatusASR.Name = "lbl_LineRunStatusASR";
      this.lbl_LineRunStatusASR.Size = new System.Drawing.Size(132, 16);
      this.lbl_LineRunStatusASR.TabIndex = 185;
      this.lbl_LineRunStatusASR.Text = "Line Run Status ASR";
      // 
      // nud_LineSpeed
      // 
      this.nud_LineSpeed.DecimalPlaces = 0;
      this.nud_LineSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LineSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LineSpeed.ImagePassword = null;
      this.nud_LineSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LineSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_LineSpeed.Location = new System.Drawing.Point(421, 81);
      this.nud_LineSpeed.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LineSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LineSpeed.Name = "nud_LineSpeed";
      this.nud_LineSpeed.ReadOnly = true;
      this.nud_LineSpeed.Size = new System.Drawing.Size(76, 19);
      this.nud_LineSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LineSpeed.TabIndex = 184;
      this.nud_LineSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LineSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LineSpeed.TranslateText = false;
      this.nud_LineSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_LineSpeed.UnitMeasureVisible = false;
      this.nud_LineSpeed.Value = "0";
      this.nud_LineSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LineSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // lbl_LineSpeed
      // 
      this.lbl_LineSpeed.AutoSize = true;
      this.lbl_LineSpeed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_LineSpeed.Location = new System.Drawing.Point(17, 85);
      this.lbl_LineSpeed.Name = "lbl_LineSpeed";
      this.lbl_LineSpeed.Size = new System.Drawing.Size(73, 16);
      this.lbl_LineSpeed.TabIndex = 183;
      this.lbl_LineSpeed.Text = "Line Speed";
      // 
      // nud_LastAOCSpeed
      // 
      this.nud_LastAOCSpeed.DecimalPlaces = 0;
      this.nud_LastAOCSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LastAOCSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCSpeed.ImagePassword = null;
      this.nud_LastAOCSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LastAOCSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_LastAOCSpeed.Location = new System.Drawing.Point(421, 121);
      this.nud_LastAOCSpeed.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LastAOCSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LastAOCSpeed.Name = "nud_LastAOCSpeed";
      this.nud_LastAOCSpeed.ReadOnly = true;
      this.nud_LastAOCSpeed.Size = new System.Drawing.Size(76, 19);
      this.nud_LastAOCSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LastAOCSpeed.TabIndex = 182;
      this.nud_LastAOCSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LastAOCSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCSpeed.TranslateText = false;
      this.nud_LastAOCSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_LastAOCSpeed.UnitMeasureVisible = false;
      this.nud_LastAOCSpeed.Value = "0";
      this.nud_LastAOCSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LastAOCSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // lbl_LastAOCSpeed
      // 
      this.lbl_LastAOCSpeed.AutoSize = true;
      this.lbl_LastAOCSpeed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_LastAOCSpeed.Location = new System.Drawing.Point(17, 125);
      this.lbl_LastAOCSpeed.Name = "lbl_LastAOCSpeed";
      this.lbl_LastAOCSpeed.Size = new System.Drawing.Size(106, 16);
      this.lbl_LastAOCSpeed.TabIndex = 181;
      this.lbl_LastAOCSpeed.Text = "Last AOC Speed";
      // 
      // nud_ProductionSpeed
      // 
      this.nud_ProductionSpeed.DecimalPlaces = 0;
      this.nud_ProductionSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ProductionSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ProductionSpeed.ImagePassword = null;
      this.nud_ProductionSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ProductionSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_ProductionSpeed.Location = new System.Drawing.Point(421, 102);
      this.nud_ProductionSpeed.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_ProductionSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ProductionSpeed.Name = "nud_ProductionSpeed";
      this.nud_ProductionSpeed.ReadOnly = true;
      this.nud_ProductionSpeed.Size = new System.Drawing.Size(76, 19);
      this.nud_ProductionSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_ProductionSpeed.TabIndex = 180;
      this.nud_ProductionSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_ProductionSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ProductionSpeed.TranslateText = false;
      this.nud_ProductionSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_ProductionSpeed.UnitMeasureVisible = false;
      this.nud_ProductionSpeed.Value = "0";
      this.nud_ProductionSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_ProductionSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // lbl_ProductionSpeed
      // 
      this.lbl_ProductionSpeed.AutoSize = true;
      this.lbl_ProductionSpeed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ProductionSpeed.Location = new System.Drawing.Point(17, 106);
      this.lbl_ProductionSpeed.Name = "lbl_ProductionSpeed";
      this.lbl_ProductionSpeed.Size = new System.Drawing.Size(111, 16);
      this.lbl_ProductionSpeed.TabIndex = 179;
      this.lbl_ProductionSpeed.Text = "Production Speed";
      // 
      // nud_MWSpeed
      // 
      this.nud_MWSpeed.DecimalPlaces = 0;
      this.nud_MWSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_MWSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_MWSpeed.ImagePassword = null;
      this.nud_MWSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_MWSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_MWSpeed.Location = new System.Drawing.Point(421, 36);
      this.nud_MWSpeed.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_MWSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_MWSpeed.Name = "nud_MWSpeed";
      this.nud_MWSpeed.ReadOnly = true;
      this.nud_MWSpeed.Size = new System.Drawing.Size(76, 19);
      this.nud_MWSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_MWSpeed.TabIndex = 178;
      this.nud_MWSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_MWSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_MWSpeed.TranslateText = false;
      this.nud_MWSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_MWSpeed.UnitMeasureVisible = false;
      this.nud_MWSpeed.Value = "0";
      this.nud_MWSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_MWSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_DBSpeed
      // 
      this.nud_DBSpeed.DecimalPlaces = 0;
      this.nud_DBSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_DBSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DBSpeed.ImagePassword = null;
      this.nud_DBSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_DBSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_DBSpeed.Location = new System.Drawing.Point(421, 16);
      this.nud_DBSpeed.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_DBSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_DBSpeed.Name = "nud_DBSpeed";
      this.nud_DBSpeed.ReadOnly = true;
      this.nud_DBSpeed.Size = new System.Drawing.Size(76, 19);
      this.nud_DBSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_DBSpeed.TabIndex = 177;
      this.nud_DBSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DBSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DBSpeed.TranslateText = false;
      this.nud_DBSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_DBSpeed.UnitMeasureVisible = false;
      this.nud_DBSpeed.Value = "0";
      this.nud_DBSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_DBSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // lbl_DBSpeed
      // 
      this.lbl_DBSpeed.AutoSize = true;
      this.lbl_DBSpeed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DBSpeed.Location = new System.Drawing.Point(17, 19);
      this.lbl_DBSpeed.Name = "lbl_DBSpeed";
      this.lbl_DBSpeed.Size = new System.Drawing.Size(67, 16);
      this.lbl_DBSpeed.TabIndex = 1;
      this.lbl_DBSpeed.Text = "DB Speed";
      // 
      // lbl_MWSpeed
      // 
      this.lbl_MWSpeed.AutoSize = true;
      this.lbl_MWSpeed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_MWSpeed.Location = new System.Drawing.Point(17, 40);
      this.lbl_MWSpeed.Name = "lbl_MWSpeed";
      this.lbl_MWSpeed.Size = new System.Drawing.Size(73, 16);
      this.lbl_MWSpeed.TabIndex = 0;
      this.lbl_MWSpeed.Text = "MW Speed";
      // 
      // nud_CutlengthDiff3
      // 
      this.nud_CutlengthDiff3.DecimalPlaces = 0;
      this.nud_CutlengthDiff3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_CutlengthDiff3.ForeColor = System.Drawing.Color.Black;
      this.nud_CutlengthDiff3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthDiff3.ImagePassword = null;
      this.nud_CutlengthDiff3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_CutlengthDiff3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_CutlengthDiff3.Location = new System.Drawing.Point(1032, 57);
      this.nud_CutlengthDiff3.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_CutlengthDiff3.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
      this.nud_CutlengthDiff3.Name = "nud_CutlengthDiff3";
      this.nud_CutlengthDiff3.ReadOnly = true;
      this.nud_CutlengthDiff3.Size = new System.Drawing.Size(76, 19);
      this.nud_CutlengthDiff3.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_CutlengthDiff3.TabIndex = 366;
      this.nud_CutlengthDiff3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_CutlengthDiff3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthDiff3.TranslateText = false;
      this.nud_CutlengthDiff3.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_CutlengthDiff3.UnitMeasureVisible = false;
      this.nud_CutlengthDiff3.Value = "0";
      this.nud_CutlengthDiff3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_CutlengthDiff3.ValueForeColor = System.Drawing.Color.Black;
      // 
      // nud_CutlengthDiff2
      // 
      this.nud_CutlengthDiff2.DecimalPlaces = 0;
      this.nud_CutlengthDiff2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_CutlengthDiff2.ForeColor = System.Drawing.Color.Black;
      this.nud_CutlengthDiff2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthDiff2.ImagePassword = null;
      this.nud_CutlengthDiff2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_CutlengthDiff2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_CutlengthDiff2.Location = new System.Drawing.Point(842, 57);
      this.nud_CutlengthDiff2.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_CutlengthDiff2.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
      this.nud_CutlengthDiff2.Name = "nud_CutlengthDiff2";
      this.nud_CutlengthDiff2.ReadOnly = true;
      this.nud_CutlengthDiff2.Size = new System.Drawing.Size(76, 19);
      this.nud_CutlengthDiff2.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_CutlengthDiff2.TabIndex = 365;
      this.nud_CutlengthDiff2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_CutlengthDiff2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthDiff2.TranslateText = false;
      this.nud_CutlengthDiff2.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_CutlengthDiff2.UnitMeasureVisible = false;
      this.nud_CutlengthDiff2.Value = "0";
      this.nud_CutlengthDiff2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_CutlengthDiff2.ValueForeColor = System.Drawing.Color.Black;
      // 
      // nud_CutlengthDiff1
      // 
      this.nud_CutlengthDiff1.DecimalPlaces = 0;
      this.nud_CutlengthDiff1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_CutlengthDiff1.ForeColor = System.Drawing.Color.Black;
      this.nud_CutlengthDiff1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthDiff1.ImagePassword = null;
      this.nud_CutlengthDiff1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_CutlengthDiff1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_CutlengthDiff1.Location = new System.Drawing.Point(652, 57);
      this.nud_CutlengthDiff1.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_CutlengthDiff1.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
      this.nud_CutlengthDiff1.Name = "nud_CutlengthDiff1";
      this.nud_CutlengthDiff1.ReadOnly = true;
      this.nud_CutlengthDiff1.Size = new System.Drawing.Size(76, 19);
      this.nud_CutlengthDiff1.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_CutlengthDiff1.TabIndex = 364;
      this.nud_CutlengthDiff1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_CutlengthDiff1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthDiff1.TranslateText = false;
      this.nud_CutlengthDiff1.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_CutlengthDiff1.UnitMeasureVisible = false;
      this.nud_CutlengthDiff1.Value = "0";
      this.nud_CutlengthDiff1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_CutlengthDiff1.ValueForeColor = System.Drawing.Color.Black;
      // 
      // nud_CutlengthReqL3
      // 
      this.nud_CutlengthReqL3.DecimalPlaces = 0;
      this.nud_CutlengthReqL3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_CutlengthReqL3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthReqL3.ImagePassword = null;
      this.nud_CutlengthReqL3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_CutlengthReqL3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_CutlengthReqL3.Location = new System.Drawing.Point(1032, 34);
      this.nud_CutlengthReqL3.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_CutlengthReqL3.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
      this.nud_CutlengthReqL3.Name = "nud_CutlengthReqL3";
      this.nud_CutlengthReqL3.ReadOnly = true;
      this.nud_CutlengthReqL3.Size = new System.Drawing.Size(76, 19);
      this.nud_CutlengthReqL3.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_CutlengthReqL3.TabIndex = 362;
      this.nud_CutlengthReqL3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_CutlengthReqL3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthReqL3.TranslateText = false;
      this.nud_CutlengthReqL3.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_CutlengthReqL3.UnitMeasureVisible = false;
      this.nud_CutlengthReqL3.Value = "0";
      this.nud_CutlengthReqL3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_CutlengthReqL3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_CutlengthReqL1
      // 
      this.nud_CutlengthReqL1.DecimalPlaces = 0;
      this.nud_CutlengthReqL1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_CutlengthReqL1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthReqL1.ImagePassword = null;
      this.nud_CutlengthReqL1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_CutlengthReqL1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_CutlengthReqL1.Location = new System.Drawing.Point(652, 34);
      this.nud_CutlengthReqL1.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_CutlengthReqL1.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
      this.nud_CutlengthReqL1.Name = "nud_CutlengthReqL1";
      this.nud_CutlengthReqL1.ReadOnly = true;
      this.nud_CutlengthReqL1.Size = new System.Drawing.Size(76, 19);
      this.nud_CutlengthReqL1.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_CutlengthReqL1.TabIndex = 360;
      this.nud_CutlengthReqL1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_CutlengthReqL1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthReqL1.TranslateText = false;
      this.nud_CutlengthReqL1.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_CutlengthReqL1.UnitMeasureVisible = false;
      this.nud_CutlengthReqL1.Value = "0";
      this.nud_CutlengthReqL1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_CutlengthReqL1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_CutlengthL3
      // 
      this.nud_CutlengthL3.DecimalPlaces = 0;
      this.nud_CutlengthL3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_CutlengthL3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthL3.ImagePassword = null;
      this.nud_CutlengthL3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_CutlengthL3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_CutlengthL3.Location = new System.Drawing.Point(1032, 12);
      this.nud_CutlengthL3.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_CutlengthL3.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
      this.nud_CutlengthL3.Name = "nud_CutlengthL3";
      this.nud_CutlengthL3.ReadOnly = true;
      this.nud_CutlengthL3.Size = new System.Drawing.Size(76, 19);
      this.nud_CutlengthL3.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_CutlengthL3.TabIndex = 355;
      this.nud_CutlengthL3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_CutlengthL3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthL3.TranslateText = false;
      this.nud_CutlengthL3.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_CutlengthL3.UnitMeasureVisible = false;
      this.nud_CutlengthL3.Value = "0";
      this.nud_CutlengthL3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_CutlengthL3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_CutlengthL2
      // 
      this.nud_CutlengthL2.DecimalPlaces = 0;
      this.nud_CutlengthL2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_CutlengthL2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthL2.ImagePassword = null;
      this.nud_CutlengthL2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_CutlengthL2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_CutlengthL2.Location = new System.Drawing.Point(842, 12);
      this.nud_CutlengthL2.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_CutlengthL2.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
      this.nud_CutlengthL2.Name = "nud_CutlengthL2";
      this.nud_CutlengthL2.ReadOnly = true;
      this.nud_CutlengthL2.Size = new System.Drawing.Size(76, 19);
      this.nud_CutlengthL2.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_CutlengthL2.TabIndex = 353;
      this.nud_CutlengthL2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_CutlengthL2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthL2.TranslateText = false;
      this.nud_CutlengthL2.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_CutlengthL2.UnitMeasureVisible = false;
      this.nud_CutlengthL2.Value = "0";
      this.nud_CutlengthL2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_CutlengthL2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_CutlengthL1
      // 
      this.nud_CutlengthL1.DecimalPlaces = 0;
      this.nud_CutlengthL1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_CutlengthL1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthL1.ImagePassword = null;
      this.nud_CutlengthL1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_CutlengthL1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_CutlengthL1.Location = new System.Drawing.Point(652, 12);
      this.nud_CutlengthL1.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_CutlengthL1.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
      this.nud_CutlengthL1.Name = "nud_CutlengthL1";
      this.nud_CutlengthL1.ReadOnly = true;
      this.nud_CutlengthL1.Size = new System.Drawing.Size(76, 19);
      this.nud_CutlengthL1.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_CutlengthL1.TabIndex = 351;
      this.nud_CutlengthL1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_CutlengthL1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthL1.TranslateText = false;
      this.nud_CutlengthL1.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_CutlengthL1.UnitMeasureVisible = false;
      this.nud_CutlengthL1.Value = "0";
      this.nud_CutlengthL1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_CutlengthL1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_CutlengthReqL2
      // 
      this.nud_CutlengthReqL2.DecimalPlaces = 0;
      this.nud_CutlengthReqL2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_CutlengthReqL2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthReqL2.ImagePassword = null;
      this.nud_CutlengthReqL2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_CutlengthReqL2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_CutlengthReqL2.Location = new System.Drawing.Point(842, 34);
      this.nud_CutlengthReqL2.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_CutlengthReqL2.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
      this.nud_CutlengthReqL2.Name = "nud_CutlengthReqL2";
      this.nud_CutlengthReqL2.ReadOnly = true;
      this.nud_CutlengthReqL2.Size = new System.Drawing.Size(76, 19);
      this.nud_CutlengthReqL2.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_CutlengthReqL2.TabIndex = 361;
      this.nud_CutlengthReqL2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_CutlengthReqL2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CutlengthReqL2.TranslateText = false;
      this.nud_CutlengthReqL2.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_CutlengthReqL2.UnitMeasureVisible = false;
      this.nud_CutlengthReqL2.Value = "0";
      this.nud_CutlengthReqL2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_CutlengthReqL2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // Frm_SpeedStatus
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tab_SpeedGroup);
      this.Name = "Frm_SpeedStatus";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.tab_SpeedGroup, 0);
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
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.tab_SpeedGroup.ResumeLayout(false);
      this.tab_Speeds.ResumeLayout(false);
      this.tab_Speeds.PerformLayout();
      this.ResumeLayout(false);

		}
		#endregion
	}
}

