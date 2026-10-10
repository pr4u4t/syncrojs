using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.SyncroSystem.AOC_WEC
{
	public class Frm_AOCStatus : SyncroC.Forms.Pages.SyncroSystem.AOC_WEC.Frm_AOC_WECSubGroup
    {
        private TabControl AOCStatus;
				private TabPage tab_Calculations;
        private Label lbl_DBDeceleration;
        private Label lbl_DryEndAcceleration;
        private Label lbl_MinAOCSpeed;
				private Label lbl_MaxAOCSpeed;
        private TabPage tab_Status;
        private Label lbl_AOCSpeed;
				private SyncroC.Controls.TitledNumericUpDown nud_GapAOCSpeedValue;
        private SyncroC.Controls.TitledNumericUpDown nud_DBDecelerationValue;
        private SyncroC.Controls.TitledNumericUpDown nud_DryEndAccelerationValue;
        private SyncroC.Controls.TitledNumericUpDown nud_MinAOCSpeedValue;
				private SyncroC.Controls.TitledNumericUpDown nud_MaxAOCSpeedValue;
        private Label lbl_AOCinProgress;
        private CheckBox chk_AOCinProgress;
        private CheckBox chk_SlowDown;
        private Label lbl_SlowDown;
        private CheckBox chk_PrepareAOC;
        private Label lbl_PrepareAOC;
        private CheckBox chk_EarlyWarning;
        private Label lbl_EarlyWarning;
        private CheckBox chk_WaitingSSReady;
        private Label lbl_WaitingSSReady;
        private CheckBox chk_WaitingMachinesReady;
        private Label lbl_WaitingMachinesReady;
        private CheckBox chk_DBAtAOCSpeed;
        private Label lbl_DBAtAOCSpeed;
        private CheckBox chk_TandemAOC;
        private CheckBox chk_NormalAOC;
        private CheckBox chk_NoGapAOC;
        private Label lbl_TypeAOC;
        private CheckBox chk_RSHStartedAOC;
        private Label lbl_RSHStartedAOC;
        private CheckBox chk_Auto;
        private CheckBox chk_Semi;
        private CheckBox chk_Manual;
        private Label lbl_StartAoc;
        private CheckBox chk_MTS_TER;
        private CheckBox chk_SyncKnifeCutReceived;
        private Label lbl_SyncKnifeCutReceived;
        private CheckBox chk_NewSetupAtMW;
        private Label lbl_NewSetupAtMW;
        private CheckBox chk_OldSetupAtMW;
        private Label lbl_OldSetupAtMW;
        private CheckBox chk_LastCutLev3;
        private CheckBox chk_LastCutLev2;
        private CheckBox chk_LastCutLev1;
        private Label lbl_LastCut;
        private CheckBox chk_KnifeHasChangedLev3;
        private CheckBox chk_KnifeHasChangedLev2;
        private CheckBox chk_KnifeHasChangedLev1;
        private Label lbl_KnifeHasChanged;
        private CheckBox chk_ChangeNowLev3;
        private CheckBox chk_ChangeNowLev2;
        private CheckBox chk_ChangeNowLev1;
        private Label lbl_ChangeNow;
        private CheckBox chk_ChangeAtDefaultLengthLev3;
        private CheckBox chk_ChangeAtDefaultLengthLev2;
        private CheckBox chk_ChangeAtDefaultLengthLev1;
        private Label lbl_ChangeAtDefaultLength;
        private Label lbl_TailOutCutsRequired;
        private SyncroC.Controls.TitledNumericUpDown nud_TailOutCutsDoneLev3;
        private SyncroC.Controls.TitledNumericUpDown nud_TailOutCutsDoneLev2;
        private SyncroC.Controls.TitledNumericUpDown nud_TailOutCutsDoneLev1;
        private SyncroC.Controls.TitledNumericUpDown nud_TailOutCutsRequiredLev3;
        private SyncroC.Controls.TitledNumericUpDown nud_TailOutCutsRequiredLev2;
        private SyncroC.Controls.TitledNumericUpDown nud_TailOutCutsRequiredLev1;
        private Label lbl_TailOutCutsDone;
        private SyncroC.Controls.TitledNumericUpDown nud_StartAOCSpeed;
        private Label lbl_StartAOCSpeed;
        private SyncroC.Controls.TitledNumericUpDown nud_DefaultCutsRequiredLev3;
        private SyncroC.Controls.TitledNumericUpDown nud_DefaultCutsRequiredLev2;
        private SyncroC.Controls.TitledNumericUpDown nud_DefaultCutsRequiredLev1;
        private Label lbl_DefaultCutsRequired;
		private CheckBox chk_SequenceAOC;
    private Label lbl_SequenceAOC;
		private TabPage tabPage1;
		private SyncroC.Controls.TitledNumericUpDown nud_LastAOCDefaultCutsRequiredLev3;
		private SyncroC.Controls.TitledNumericUpDown nud_LastAOCDefaultCutsRequiredLev2;
		private SyncroC.Controls.TitledNumericUpDown nud_LastAOCDefaultCutsRequiredLev1;
		private Label label4;
		private SyncroC.Controls.TitledNumericUpDown nud_LastAOCTailOutCutsRequiredLev3;
		private SyncroC.Controls.TitledNumericUpDown nud_LastAOCTailOutCutsRequiredLev2;
		private SyncroC.Controls.TitledNumericUpDown nud_LastAOCTailOutCutsRequiredLev1;
		private Label label5;
		private SyncroC.Controls.TitledNumericUpDown nud_LastAOCStartAOCSpeed;
		private Label label2;
		private CheckBox chk_LastAOCMTS_TER;
		private CheckBox chk_LastAOCTandemAOC;
		private CheckBox chk_LastAOCNormalAOC;
		private CheckBox chk_LastAOCNoGapAOC;
		private Label label3;
		private CheckBox chk_LastAOCAuto;
		private CheckBox chk_LastAOCSemi;
		private CheckBox chk_LastAOCManual;
		private Label label1;
		private SyncroC.Controls.TitledNumericUpDown nud_LastAOCRSHLastContinuousCutReactionTimeValue;
		private SyncroC.Controls.TitledNumericUpDown nud_LastAOCRSHLastSingleCutReactionTimeValue;
		private SyncroC.Controls.TitledNumericUpDown nud_LastAOCRSHReactionTimeEstimatedValue;
		private SyncroC.Controls.TitledNumericUpDown nud_LastAOCRSHFireDistanceValue;
		private SyncroC.Controls.TitledNumericUpDown nud_LastAOCSyncLengthValue;
		private SyncroC.Controls.TitledNumericUpDown nud_LastAOCSyncKnifeValue;
		private SyncroC.Controls.TitledNumericUpDown nud_LastAOCDryEndAccelerationValue;
		private Label label6;
		private Label label7;
		private Label label8;
		private Label label9;
		private Label label10;
		private Label label11;
		private Label label12;
		private Label label13;
		private CheckBox chk_LastAOCStartAOCtoSSUsedFD;
		private SyncroC.Controls.TitledNumericUpDown nud_DistBetweenFDandEncoderSSAOCPoint;
		private Label label14;
		private SyncroC.Controls.TitledNumericUpDown nud_LatAOCNrChopAtRSH;
    private Label label15;
    private SyncroC.Controls.TitledNumericUpDown nud_SyncLevel;
    private Label lbl_SyncLevel;
		private System.ComponentModel.IContainer components = null;

		public Frm_AOCStatus()
		{
			InitializeComponent();
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AOCStatus));
      this.AOCStatus = new System.Windows.Forms.TabControl();
      this.tab_Calculations = new System.Windows.Forms.TabPage();
      this.nud_DBDecelerationValue = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_DryEndAccelerationValue = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_MinAOCSpeedValue = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_MaxAOCSpeedValue = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_GapAOCSpeedValue = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_DBDeceleration = new System.Windows.Forms.Label();
      this.lbl_DryEndAcceleration = new System.Windows.Forms.Label();
      this.lbl_MinAOCSpeed = new System.Windows.Forms.Label();
      this.lbl_MaxAOCSpeed = new System.Windows.Forms.Label();
      this.lbl_AOCSpeed = new System.Windows.Forms.Label();
      this.tab_Status = new System.Windows.Forms.TabPage();
      this.chk_SequenceAOC = new System.Windows.Forms.CheckBox();
      this.lbl_SequenceAOC = new System.Windows.Forms.Label();
      this.nud_DefaultCutsRequiredLev3 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_DefaultCutsRequiredLev2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_DefaultCutsRequiredLev1 = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_DefaultCutsRequired = new System.Windows.Forms.Label();
      this.nud_StartAOCSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_StartAOCSpeed = new System.Windows.Forms.Label();
      this.nud_TailOutCutsDoneLev3 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_TailOutCutsDoneLev2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_TailOutCutsDoneLev1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_TailOutCutsRequiredLev3 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_TailOutCutsRequiredLev2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_TailOutCutsRequiredLev1 = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_TailOutCutsDone = new System.Windows.Forms.Label();
      this.lbl_TailOutCutsRequired = new System.Windows.Forms.Label();
      this.chk_LastCutLev3 = new System.Windows.Forms.CheckBox();
      this.chk_LastCutLev2 = new System.Windows.Forms.CheckBox();
      this.chk_LastCutLev1 = new System.Windows.Forms.CheckBox();
      this.lbl_LastCut = new System.Windows.Forms.Label();
      this.chk_KnifeHasChangedLev3 = new System.Windows.Forms.CheckBox();
      this.chk_KnifeHasChangedLev2 = new System.Windows.Forms.CheckBox();
      this.chk_KnifeHasChangedLev1 = new System.Windows.Forms.CheckBox();
      this.lbl_KnifeHasChanged = new System.Windows.Forms.Label();
      this.chk_ChangeNowLev3 = new System.Windows.Forms.CheckBox();
      this.chk_ChangeNowLev2 = new System.Windows.Forms.CheckBox();
      this.chk_ChangeNowLev1 = new System.Windows.Forms.CheckBox();
      this.lbl_ChangeNow = new System.Windows.Forms.Label();
      this.chk_ChangeAtDefaultLengthLev3 = new System.Windows.Forms.CheckBox();
      this.chk_ChangeAtDefaultLengthLev2 = new System.Windows.Forms.CheckBox();
      this.chk_ChangeAtDefaultLengthLev1 = new System.Windows.Forms.CheckBox();
      this.lbl_ChangeAtDefaultLength = new System.Windows.Forms.Label();
      this.chk_NewSetupAtMW = new System.Windows.Forms.CheckBox();
      this.lbl_NewSetupAtMW = new System.Windows.Forms.Label();
      this.chk_OldSetupAtMW = new System.Windows.Forms.CheckBox();
      this.lbl_OldSetupAtMW = new System.Windows.Forms.Label();
      this.chk_SyncKnifeCutReceived = new System.Windows.Forms.CheckBox();
      this.lbl_SyncKnifeCutReceived = new System.Windows.Forms.Label();
      this.chk_Auto = new System.Windows.Forms.CheckBox();
      this.chk_Semi = new System.Windows.Forms.CheckBox();
      this.chk_Manual = new System.Windows.Forms.CheckBox();
      this.lbl_StartAoc = new System.Windows.Forms.Label();
      this.chk_MTS_TER = new System.Windows.Forms.CheckBox();
      this.chk_TandemAOC = new System.Windows.Forms.CheckBox();
      this.chk_NormalAOC = new System.Windows.Forms.CheckBox();
      this.chk_NoGapAOC = new System.Windows.Forms.CheckBox();
      this.lbl_TypeAOC = new System.Windows.Forms.Label();
      this.chk_RSHStartedAOC = new System.Windows.Forms.CheckBox();
      this.lbl_RSHStartedAOC = new System.Windows.Forms.Label();
      this.chk_DBAtAOCSpeed = new System.Windows.Forms.CheckBox();
      this.lbl_DBAtAOCSpeed = new System.Windows.Forms.Label();
      this.chk_WaitingSSReady = new System.Windows.Forms.CheckBox();
      this.lbl_WaitingSSReady = new System.Windows.Forms.Label();
      this.chk_WaitingMachinesReady = new System.Windows.Forms.CheckBox();
      this.lbl_WaitingMachinesReady = new System.Windows.Forms.Label();
      this.chk_SlowDown = new System.Windows.Forms.CheckBox();
      this.lbl_SlowDown = new System.Windows.Forms.Label();
      this.chk_PrepareAOC = new System.Windows.Forms.CheckBox();
      this.lbl_PrepareAOC = new System.Windows.Forms.Label();
      this.chk_EarlyWarning = new System.Windows.Forms.CheckBox();
      this.lbl_EarlyWarning = new System.Windows.Forms.Label();
      this.chk_AOCinProgress = new System.Windows.Forms.CheckBox();
      this.lbl_AOCinProgress = new System.Windows.Forms.Label();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.nud_LatAOCNrChopAtRSH = new SyncroC.Controls.TitledNumericUpDown();
      this.label15 = new System.Windows.Forms.Label();
      this.nud_DistBetweenFDandEncoderSSAOCPoint = new SyncroC.Controls.TitledNumericUpDown();
      this.label14 = new System.Windows.Forms.Label();
      this.chk_LastAOCStartAOCtoSSUsedFD = new System.Windows.Forms.CheckBox();
      this.label13 = new System.Windows.Forms.Label();
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_LastAOCRSHReactionTimeEstimatedValue = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_LastAOCRSHFireDistanceValue = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_LastAOCSyncLengthValue = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_LastAOCSyncKnifeValue = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_LastAOCDryEndAccelerationValue = new SyncroC.Controls.TitledNumericUpDown();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.nud_LastAOCDefaultCutsRequiredLev3 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_LastAOCDefaultCutsRequiredLev2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_LastAOCDefaultCutsRequiredLev1 = new SyncroC.Controls.TitledNumericUpDown();
      this.label4 = new System.Windows.Forms.Label();
      this.nud_LastAOCTailOutCutsRequiredLev3 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_LastAOCTailOutCutsRequiredLev2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_LastAOCTailOutCutsRequiredLev1 = new SyncroC.Controls.TitledNumericUpDown();
      this.label5 = new System.Windows.Forms.Label();
      this.nud_LastAOCStartAOCSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.label2 = new System.Windows.Forms.Label();
      this.chk_LastAOCMTS_TER = new System.Windows.Forms.CheckBox();
      this.chk_LastAOCTandemAOC = new System.Windows.Forms.CheckBox();
      this.chk_LastAOCNormalAOC = new System.Windows.Forms.CheckBox();
      this.chk_LastAOCNoGapAOC = new System.Windows.Forms.CheckBox();
      this.label3 = new System.Windows.Forms.Label();
      this.chk_LastAOCAuto = new System.Windows.Forms.CheckBox();
      this.chk_LastAOCSemi = new System.Windows.Forms.CheckBox();
      this.chk_LastAOCManual = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.nud_SyncLevel = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_SyncLevel = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.AOCStatus.SuspendLayout();
      this.tab_Calculations.SuspendLayout();
      this.tab_Status.SuspendLayout();
      this.tabPage1.SuspendLayout();
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
      // AOCStatus
      // 
      this.AOCStatus.Controls.Add(this.tab_Calculations);
      this.AOCStatus.Controls.Add(this.tab_Status);
      this.AOCStatus.Controls.Add(this.tabPage1);
      this.AOCStatus.Location = new System.Drawing.Point(30, 193);
      this.AOCStatus.Name = "AOCStatus";
      this.AOCStatus.SelectedIndex = 0;
      this.AOCStatus.Size = new System.Drawing.Size(1171, 519);
      this.AOCStatus.TabIndex = 277;
      // 
      // tab_Calculations
      // 
      this.tab_Calculations.Controls.Add(this.nud_DBDecelerationValue);
      this.tab_Calculations.Controls.Add(this.nud_DryEndAccelerationValue);
      this.tab_Calculations.Controls.Add(this.nud_MinAOCSpeedValue);
      this.tab_Calculations.Controls.Add(this.nud_MaxAOCSpeedValue);
      this.tab_Calculations.Controls.Add(this.nud_GapAOCSpeedValue);
      this.tab_Calculations.Controls.Add(this.lbl_DBDeceleration);
      this.tab_Calculations.Controls.Add(this.lbl_DryEndAcceleration);
      this.tab_Calculations.Controls.Add(this.lbl_MinAOCSpeed);
      this.tab_Calculations.Controls.Add(this.lbl_MaxAOCSpeed);
      this.tab_Calculations.Controls.Add(this.lbl_AOCSpeed);
      this.tab_Calculations.Location = new System.Drawing.Point(4, 22);
      this.tab_Calculations.Name = "tab_Calculations";
      this.tab_Calculations.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Calculations.Size = new System.Drawing.Size(1163, 493);
      this.tab_Calculations.TabIndex = 0;
      this.tab_Calculations.Text = "Calculations";
      this.tab_Calculations.UseVisualStyleBackColor = true;
      // 
      // nud_DBDecelerationValue
      // 
      this.nud_DBDecelerationValue.DecimalPlaces = 0;
      this.nud_DBDecelerationValue.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_DBDecelerationValue.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DBDecelerationValue.ImagePassword = null;
      this.nud_DBDecelerationValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_DBDecelerationValue.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DBDecelerationValue.Location = new System.Drawing.Point(422, 85);
      this.nud_DBDecelerationValue.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_DBDecelerationValue.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_DBDecelerationValue.Name = "nud_DBDecelerationValue";
      this.nud_DBDecelerationValue.ReadOnly = true;
      this.nud_DBDecelerationValue.Size = new System.Drawing.Size(95, 19);
      this.nud_DBDecelerationValue.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_DBDecelerationValue.TabIndex = 323;
      this.nud_DBDecelerationValue.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DBDecelerationValue.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DBDecelerationValue.TranslateText = false;
      this.nud_DBDecelerationValue.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DBDecelerationValue.UnitMeasureVisible = false;
      this.nud_DBDecelerationValue.Value = "0";
      this.nud_DBDecelerationValue.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_DBDecelerationValue.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_DBDecelerationValue.Visible = false;
      // 
      // nud_DryEndAccelerationValue
      // 
      this.nud_DryEndAccelerationValue.DecimalPlaces = 0;
      this.nud_DryEndAccelerationValue.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_DryEndAccelerationValue.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DryEndAccelerationValue.ImagePassword = null;
      this.nud_DryEndAccelerationValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_DryEndAccelerationValue.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DryEndAccelerationValue.Location = new System.Drawing.Point(422, 65);
      this.nud_DryEndAccelerationValue.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_DryEndAccelerationValue.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_DryEndAccelerationValue.Name = "nud_DryEndAccelerationValue";
      this.nud_DryEndAccelerationValue.ReadOnly = true;
      this.nud_DryEndAccelerationValue.Size = new System.Drawing.Size(95, 19);
      this.nud_DryEndAccelerationValue.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_DryEndAccelerationValue.TabIndex = 322;
      this.nud_DryEndAccelerationValue.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DryEndAccelerationValue.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DryEndAccelerationValue.TranslateText = false;
      this.nud_DryEndAccelerationValue.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DryEndAccelerationValue.UnitMeasureVisible = false;
      this.nud_DryEndAccelerationValue.Value = "0";
      this.nud_DryEndAccelerationValue.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_DryEndAccelerationValue.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_MinAOCSpeedValue
      // 
      this.nud_MinAOCSpeedValue.DecimalPlaces = 0;
      this.nud_MinAOCSpeedValue.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_MinAOCSpeedValue.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_MinAOCSpeedValue.ImagePassword = null;
      this.nud_MinAOCSpeedValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_MinAOCSpeedValue.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_MinAOCSpeedValue.Location = new System.Drawing.Point(422, 46);
      this.nud_MinAOCSpeedValue.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_MinAOCSpeedValue.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_MinAOCSpeedValue.Name = "nud_MinAOCSpeedValue";
      this.nud_MinAOCSpeedValue.ReadOnly = true;
      this.nud_MinAOCSpeedValue.Size = new System.Drawing.Size(95, 19);
      this.nud_MinAOCSpeedValue.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_MinAOCSpeedValue.TabIndex = 321;
      this.nud_MinAOCSpeedValue.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_MinAOCSpeedValue.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_MinAOCSpeedValue.TranslateText = false;
      this.nud_MinAOCSpeedValue.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_MinAOCSpeedValue.UnitMeasureVisible = false;
      this.nud_MinAOCSpeedValue.Value = "0";
      this.nud_MinAOCSpeedValue.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_MinAOCSpeedValue.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_MaxAOCSpeedValue
      // 
      this.nud_MaxAOCSpeedValue.DecimalPlaces = 0;
      this.nud_MaxAOCSpeedValue.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_MaxAOCSpeedValue.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_MaxAOCSpeedValue.ImagePassword = null;
      this.nud_MaxAOCSpeedValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_MaxAOCSpeedValue.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_MaxAOCSpeedValue.Location = new System.Drawing.Point(422, 26);
      this.nud_MaxAOCSpeedValue.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_MaxAOCSpeedValue.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_MaxAOCSpeedValue.Name = "nud_MaxAOCSpeedValue";
      this.nud_MaxAOCSpeedValue.ReadOnly = true;
      this.nud_MaxAOCSpeedValue.Size = new System.Drawing.Size(95, 19);
      this.nud_MaxAOCSpeedValue.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_MaxAOCSpeedValue.TabIndex = 320;
      this.nud_MaxAOCSpeedValue.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_MaxAOCSpeedValue.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_MaxAOCSpeedValue.TranslateText = false;
      this.nud_MaxAOCSpeedValue.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_MaxAOCSpeedValue.UnitMeasureVisible = false;
      this.nud_MaxAOCSpeedValue.Value = "0";
      this.nud_MaxAOCSpeedValue.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_MaxAOCSpeedValue.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_GapAOCSpeedValue
      // 
      this.nud_GapAOCSpeedValue.DecimalPlaces = 0;
      this.nud_GapAOCSpeedValue.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_GapAOCSpeedValue.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_GapAOCSpeedValue.ImagePassword = null;
      this.nud_GapAOCSpeedValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_GapAOCSpeedValue.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_GapAOCSpeedValue.Location = new System.Drawing.Point(422, 6);
      this.nud_GapAOCSpeedValue.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_GapAOCSpeedValue.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_GapAOCSpeedValue.Name = "nud_GapAOCSpeedValue";
      this.nud_GapAOCSpeedValue.ReadOnly = true;
      this.nud_GapAOCSpeedValue.Size = new System.Drawing.Size(95, 19);
      this.nud_GapAOCSpeedValue.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_GapAOCSpeedValue.TabIndex = 319;
      this.nud_GapAOCSpeedValue.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_GapAOCSpeedValue.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_GapAOCSpeedValue.TranslateText = false;
      this.nud_GapAOCSpeedValue.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_GapAOCSpeedValue.UnitMeasureVisible = false;
      this.nud_GapAOCSpeedValue.Value = "0";
      this.nud_GapAOCSpeedValue.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_GapAOCSpeedValue.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // lbl_DBDeceleration
      // 
      this.lbl_DBDeceleration.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DBDeceleration.Location = new System.Drawing.Point(6, 84);
      this.lbl_DBDeceleration.Name = "lbl_DBDeceleration";
      this.lbl_DBDeceleration.Size = new System.Drawing.Size(410, 20);
      this.lbl_DBDeceleration.TabIndex = 311;
      this.lbl_DBDeceleration.Text = "DB Deceleration";
      this.lbl_DBDeceleration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_DBDeceleration.Visible = false;
      // 
      // lbl_DryEndAcceleration
      // 
      this.lbl_DryEndAcceleration.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DryEndAcceleration.Location = new System.Drawing.Point(6, 64);
      this.lbl_DryEndAcceleration.Name = "lbl_DryEndAcceleration";
      this.lbl_DryEndAcceleration.Size = new System.Drawing.Size(410, 20);
      this.lbl_DryEndAcceleration.TabIndex = 309;
      this.lbl_DryEndAcceleration.Text = "DryEnd Acceleration";
      this.lbl_DryEndAcceleration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_MinAOCSpeed
      // 
      this.lbl_MinAOCSpeed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_MinAOCSpeed.Location = new System.Drawing.Point(6, 45);
      this.lbl_MinAOCSpeed.Name = "lbl_MinAOCSpeed";
      this.lbl_MinAOCSpeed.Size = new System.Drawing.Size(410, 20);
      this.lbl_MinAOCSpeed.TabIndex = 307;
      this.lbl_MinAOCSpeed.Text = "MIN AOC Speed ";
      this.lbl_MinAOCSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_MaxAOCSpeed
      // 
      this.lbl_MaxAOCSpeed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_MaxAOCSpeed.Location = new System.Drawing.Point(6, 25);
      this.lbl_MaxAOCSpeed.Name = "lbl_MaxAOCSpeed";
      this.lbl_MaxAOCSpeed.Size = new System.Drawing.Size(410, 20);
      this.lbl_MaxAOCSpeed.TabIndex = 305;
      this.lbl_MaxAOCSpeed.Text = "MAX AOC Speed ";
      this.lbl_MaxAOCSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_AOCSpeed
      // 
      this.lbl_AOCSpeed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AOCSpeed.Location = new System.Drawing.Point(6, 5);
      this.lbl_AOCSpeed.Name = "lbl_AOCSpeed";
      this.lbl_AOCSpeed.Size = new System.Drawing.Size(410, 20);
      this.lbl_AOCSpeed.TabIndex = 297;
      this.lbl_AOCSpeed.Text = "AOC Speed ";
      this.lbl_AOCSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // tab_Status
      // 
      this.tab_Status.Controls.Add(this.nud_SyncLevel);
      this.tab_Status.Controls.Add(this.lbl_SyncLevel);
      this.tab_Status.Controls.Add(this.chk_SequenceAOC);
      this.tab_Status.Controls.Add(this.lbl_SequenceAOC);
      this.tab_Status.Controls.Add(this.nud_DefaultCutsRequiredLev3);
      this.tab_Status.Controls.Add(this.nud_DefaultCutsRequiredLev2);
      this.tab_Status.Controls.Add(this.nud_DefaultCutsRequiredLev1);
      this.tab_Status.Controls.Add(this.lbl_DefaultCutsRequired);
      this.tab_Status.Controls.Add(this.nud_StartAOCSpeed);
      this.tab_Status.Controls.Add(this.lbl_StartAOCSpeed);
      this.tab_Status.Controls.Add(this.nud_TailOutCutsDoneLev3);
      this.tab_Status.Controls.Add(this.nud_TailOutCutsDoneLev2);
      this.tab_Status.Controls.Add(this.nud_TailOutCutsDoneLev1);
      this.tab_Status.Controls.Add(this.nud_TailOutCutsRequiredLev3);
      this.tab_Status.Controls.Add(this.nud_TailOutCutsRequiredLev2);
      this.tab_Status.Controls.Add(this.nud_TailOutCutsRequiredLev1);
      this.tab_Status.Controls.Add(this.lbl_TailOutCutsDone);
      this.tab_Status.Controls.Add(this.lbl_TailOutCutsRequired);
      this.tab_Status.Controls.Add(this.chk_LastCutLev3);
      this.tab_Status.Controls.Add(this.chk_LastCutLev2);
      this.tab_Status.Controls.Add(this.chk_LastCutLev1);
      this.tab_Status.Controls.Add(this.lbl_LastCut);
      this.tab_Status.Controls.Add(this.chk_KnifeHasChangedLev3);
      this.tab_Status.Controls.Add(this.chk_KnifeHasChangedLev2);
      this.tab_Status.Controls.Add(this.chk_KnifeHasChangedLev1);
      this.tab_Status.Controls.Add(this.lbl_KnifeHasChanged);
      this.tab_Status.Controls.Add(this.chk_ChangeNowLev3);
      this.tab_Status.Controls.Add(this.chk_ChangeNowLev2);
      this.tab_Status.Controls.Add(this.chk_ChangeNowLev1);
      this.tab_Status.Controls.Add(this.lbl_ChangeNow);
      this.tab_Status.Controls.Add(this.chk_ChangeAtDefaultLengthLev3);
      this.tab_Status.Controls.Add(this.chk_ChangeAtDefaultLengthLev2);
      this.tab_Status.Controls.Add(this.chk_ChangeAtDefaultLengthLev1);
      this.tab_Status.Controls.Add(this.lbl_ChangeAtDefaultLength);
      this.tab_Status.Controls.Add(this.chk_NewSetupAtMW);
      this.tab_Status.Controls.Add(this.lbl_NewSetupAtMW);
      this.tab_Status.Controls.Add(this.chk_OldSetupAtMW);
      this.tab_Status.Controls.Add(this.lbl_OldSetupAtMW);
      this.tab_Status.Controls.Add(this.chk_SyncKnifeCutReceived);
      this.tab_Status.Controls.Add(this.lbl_SyncKnifeCutReceived);
      this.tab_Status.Controls.Add(this.chk_Auto);
      this.tab_Status.Controls.Add(this.chk_Semi);
      this.tab_Status.Controls.Add(this.chk_Manual);
      this.tab_Status.Controls.Add(this.lbl_StartAoc);
      this.tab_Status.Controls.Add(this.chk_MTS_TER);
      this.tab_Status.Controls.Add(this.chk_TandemAOC);
      this.tab_Status.Controls.Add(this.chk_NormalAOC);
      this.tab_Status.Controls.Add(this.chk_NoGapAOC);
      this.tab_Status.Controls.Add(this.lbl_TypeAOC);
      this.tab_Status.Controls.Add(this.chk_RSHStartedAOC);
      this.tab_Status.Controls.Add(this.lbl_RSHStartedAOC);
      this.tab_Status.Controls.Add(this.chk_DBAtAOCSpeed);
      this.tab_Status.Controls.Add(this.lbl_DBAtAOCSpeed);
      this.tab_Status.Controls.Add(this.chk_WaitingSSReady);
      this.tab_Status.Controls.Add(this.lbl_WaitingSSReady);
      this.tab_Status.Controls.Add(this.chk_WaitingMachinesReady);
      this.tab_Status.Controls.Add(this.lbl_WaitingMachinesReady);
      this.tab_Status.Controls.Add(this.chk_SlowDown);
      this.tab_Status.Controls.Add(this.lbl_SlowDown);
      this.tab_Status.Controls.Add(this.chk_PrepareAOC);
      this.tab_Status.Controls.Add(this.lbl_PrepareAOC);
      this.tab_Status.Controls.Add(this.chk_EarlyWarning);
      this.tab_Status.Controls.Add(this.lbl_EarlyWarning);
      this.tab_Status.Controls.Add(this.chk_AOCinProgress);
      this.tab_Status.Controls.Add(this.lbl_AOCinProgress);
      this.tab_Status.Location = new System.Drawing.Point(4, 22);
      this.tab_Status.Name = "tab_Status";
      this.tab_Status.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Status.Size = new System.Drawing.Size(1163, 493);
      this.tab_Status.TabIndex = 1;
      this.tab_Status.Text = "AOC Status";
      this.tab_Status.UseVisualStyleBackColor = true;
      // 
      // chk_SequenceAOC
      // 
      this.chk_SequenceAOC.AutoSize = true;
      this.chk_SequenceAOC.Location = new System.Drawing.Point(422, 254);
      this.chk_SequenceAOC.Name = "chk_SequenceAOC";
      this.chk_SequenceAOC.Size = new System.Drawing.Size(15, 14);
      this.chk_SequenceAOC.TabIndex = 391;
      this.chk_SequenceAOC.UseVisualStyleBackColor = true;
      // 
      // lbl_SequenceAOC
      // 
      this.lbl_SequenceAOC.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SequenceAOC.Location = new System.Drawing.Point(6, 250);
      this.lbl_SequenceAOC.Name = "lbl_SequenceAOC";
      this.lbl_SequenceAOC.Size = new System.Drawing.Size(410, 20);
      this.lbl_SequenceAOC.TabIndex = 390;
      this.lbl_SequenceAOC.Text = "Sequence AOC ";
      this.lbl_SequenceAOC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // nud_DefaultCutsRequiredLev3
      // 
      this.nud_DefaultCutsRequiredLev3.DecimalPlaces = 0;
      this.nud_DefaultCutsRequiredLev3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_DefaultCutsRequiredLev3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultCutsRequiredLev3.ImagePassword = null;
      this.nud_DefaultCutsRequiredLev3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_DefaultCutsRequiredLev3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DefaultCutsRequiredLev3.Location = new System.Drawing.Point(634, 432);
      this.nud_DefaultCutsRequiredLev3.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_DefaultCutsRequiredLev3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_DefaultCutsRequiredLev3.Name = "nud_DefaultCutsRequiredLev3";
      this.nud_DefaultCutsRequiredLev3.ReadOnly = true;
      this.nud_DefaultCutsRequiredLev3.Size = new System.Drawing.Size(49, 19);
      this.nud_DefaultCutsRequiredLev3.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_DefaultCutsRequiredLev3.TabIndex = 389;
      this.nud_DefaultCutsRequiredLev3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DefaultCutsRequiredLev3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultCutsRequiredLev3.TranslateText = false;
      this.nud_DefaultCutsRequiredLev3.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DefaultCutsRequiredLev3.UnitMeasureVisible = false;
      this.nud_DefaultCutsRequiredLev3.Value = "0";
      this.nud_DefaultCutsRequiredLev3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_DefaultCutsRequiredLev3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_DefaultCutsRequiredLev2
      // 
      this.nud_DefaultCutsRequiredLev2.DecimalPlaces = 0;
      this.nud_DefaultCutsRequiredLev2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_DefaultCutsRequiredLev2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultCutsRequiredLev2.ImagePassword = null;
      this.nud_DefaultCutsRequiredLev2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_DefaultCutsRequiredLev2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DefaultCutsRequiredLev2.Location = new System.Drawing.Point(527, 432);
      this.nud_DefaultCutsRequiredLev2.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_DefaultCutsRequiredLev2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_DefaultCutsRequiredLev2.Name = "nud_DefaultCutsRequiredLev2";
      this.nud_DefaultCutsRequiredLev2.ReadOnly = true;
      this.nud_DefaultCutsRequiredLev2.Size = new System.Drawing.Size(49, 19);
      this.nud_DefaultCutsRequiredLev2.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_DefaultCutsRequiredLev2.TabIndex = 388;
      this.nud_DefaultCutsRequiredLev2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DefaultCutsRequiredLev2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultCutsRequiredLev2.TranslateText = false;
      this.nud_DefaultCutsRequiredLev2.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DefaultCutsRequiredLev2.UnitMeasureVisible = false;
      this.nud_DefaultCutsRequiredLev2.Value = "0";
      this.nud_DefaultCutsRequiredLev2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_DefaultCutsRequiredLev2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_DefaultCutsRequiredLev1
      // 
      this.nud_DefaultCutsRequiredLev1.DecimalPlaces = 0;
      this.nud_DefaultCutsRequiredLev1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_DefaultCutsRequiredLev1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultCutsRequiredLev1.ImagePassword = null;
      this.nud_DefaultCutsRequiredLev1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_DefaultCutsRequiredLev1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DefaultCutsRequiredLev1.Location = new System.Drawing.Point(422, 432);
      this.nud_DefaultCutsRequiredLev1.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_DefaultCutsRequiredLev1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_DefaultCutsRequiredLev1.Name = "nud_DefaultCutsRequiredLev1";
      this.nud_DefaultCutsRequiredLev1.ReadOnly = true;
      this.nud_DefaultCutsRequiredLev1.Size = new System.Drawing.Size(49, 19);
      this.nud_DefaultCutsRequiredLev1.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_DefaultCutsRequiredLev1.TabIndex = 387;
      this.nud_DefaultCutsRequiredLev1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DefaultCutsRequiredLev1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultCutsRequiredLev1.TranslateText = false;
      this.nud_DefaultCutsRequiredLev1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DefaultCutsRequiredLev1.UnitMeasureVisible = false;
      this.nud_DefaultCutsRequiredLev1.Value = "0";
      this.nud_DefaultCutsRequiredLev1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_DefaultCutsRequiredLev1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // lbl_DefaultCutsRequired
      // 
      this.lbl_DefaultCutsRequired.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DefaultCutsRequired.Location = new System.Drawing.Point(6, 431);
      this.lbl_DefaultCutsRequired.Name = "lbl_DefaultCutsRequired";
      this.lbl_DefaultCutsRequired.Size = new System.Drawing.Size(410, 20);
      this.lbl_DefaultCutsRequired.TabIndex = 386;
      this.lbl_DefaultCutsRequired.Text = "Default Cuts Required";
      this.lbl_DefaultCutsRequired.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // nud_StartAOCSpeed
      // 
      this.nud_StartAOCSpeed.DecimalPlaces = 0;
      this.nud_StartAOCSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_StartAOCSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_StartAOCSpeed.ImagePassword = null;
      this.nud_StartAOCSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_StartAOCSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetresPerMinute;
      this.nud_StartAOCSpeed.Location = new System.Drawing.Point(422, 50);
      this.nud_StartAOCSpeed.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_StartAOCSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_StartAOCSpeed.Name = "nud_StartAOCSpeed";
      this.nud_StartAOCSpeed.ReadOnly = true;
      this.nud_StartAOCSpeed.Size = new System.Drawing.Size(49, 19);
      this.nud_StartAOCSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_StartAOCSpeed.TabIndex = 385;
      this.nud_StartAOCSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_StartAOCSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_StartAOCSpeed.TranslateText = false;
      this.nud_StartAOCSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_StartAOCSpeed.UnitMeasureVisible = false;
      this.nud_StartAOCSpeed.Value = "0";
      this.nud_StartAOCSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_StartAOCSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // lbl_StartAOCSpeed
      // 
      this.lbl_StartAOCSpeed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_StartAOCSpeed.Location = new System.Drawing.Point(6, 49);
      this.lbl_StartAOCSpeed.Name = "lbl_StartAOCSpeed";
      this.lbl_StartAOCSpeed.Size = new System.Drawing.Size(410, 20);
      this.lbl_StartAOCSpeed.TabIndex = 384;
      this.lbl_StartAOCSpeed.Text = "AOC Speed";
      this.lbl_StartAOCSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // nud_TailOutCutsDoneLev3
      // 
      this.nud_TailOutCutsDoneLev3.DecimalPlaces = 0;
      this.nud_TailOutCutsDoneLev3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_TailOutCutsDoneLev3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_TailOutCutsDoneLev3.ImagePassword = null;
      this.nud_TailOutCutsDoneLev3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_TailOutCutsDoneLev3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_TailOutCutsDoneLev3.Location = new System.Drawing.Point(634, 452);
      this.nud_TailOutCutsDoneLev3.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_TailOutCutsDoneLev3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_TailOutCutsDoneLev3.Name = "nud_TailOutCutsDoneLev3";
      this.nud_TailOutCutsDoneLev3.ReadOnly = true;
      this.nud_TailOutCutsDoneLev3.Size = new System.Drawing.Size(49, 19);
      this.nud_TailOutCutsDoneLev3.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_TailOutCutsDoneLev3.TabIndex = 383;
      this.nud_TailOutCutsDoneLev3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_TailOutCutsDoneLev3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_TailOutCutsDoneLev3.TranslateText = false;
      this.nud_TailOutCutsDoneLev3.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_TailOutCutsDoneLev3.UnitMeasureVisible = false;
      this.nud_TailOutCutsDoneLev3.Value = "0";
      this.nud_TailOutCutsDoneLev3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_TailOutCutsDoneLev3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_TailOutCutsDoneLev2
      // 
      this.nud_TailOutCutsDoneLev2.DecimalPlaces = 0;
      this.nud_TailOutCutsDoneLev2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_TailOutCutsDoneLev2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_TailOutCutsDoneLev2.ImagePassword = null;
      this.nud_TailOutCutsDoneLev2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_TailOutCutsDoneLev2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_TailOutCutsDoneLev2.Location = new System.Drawing.Point(527, 452);
      this.nud_TailOutCutsDoneLev2.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_TailOutCutsDoneLev2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_TailOutCutsDoneLev2.Name = "nud_TailOutCutsDoneLev2";
      this.nud_TailOutCutsDoneLev2.ReadOnly = true;
      this.nud_TailOutCutsDoneLev2.Size = new System.Drawing.Size(49, 19);
      this.nud_TailOutCutsDoneLev2.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_TailOutCutsDoneLev2.TabIndex = 382;
      this.nud_TailOutCutsDoneLev2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_TailOutCutsDoneLev2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_TailOutCutsDoneLev2.TranslateText = false;
      this.nud_TailOutCutsDoneLev2.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_TailOutCutsDoneLev2.UnitMeasureVisible = false;
      this.nud_TailOutCutsDoneLev2.Value = "0";
      this.nud_TailOutCutsDoneLev2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_TailOutCutsDoneLev2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_TailOutCutsDoneLev1
      // 
      this.nud_TailOutCutsDoneLev1.DecimalPlaces = 0;
      this.nud_TailOutCutsDoneLev1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_TailOutCutsDoneLev1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_TailOutCutsDoneLev1.ImagePassword = null;
      this.nud_TailOutCutsDoneLev1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_TailOutCutsDoneLev1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_TailOutCutsDoneLev1.Location = new System.Drawing.Point(422, 452);
      this.nud_TailOutCutsDoneLev1.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_TailOutCutsDoneLev1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_TailOutCutsDoneLev1.Name = "nud_TailOutCutsDoneLev1";
      this.nud_TailOutCutsDoneLev1.ReadOnly = true;
      this.nud_TailOutCutsDoneLev1.Size = new System.Drawing.Size(49, 19);
      this.nud_TailOutCutsDoneLev1.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_TailOutCutsDoneLev1.TabIndex = 381;
      this.nud_TailOutCutsDoneLev1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_TailOutCutsDoneLev1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_TailOutCutsDoneLev1.TranslateText = false;
      this.nud_TailOutCutsDoneLev1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_TailOutCutsDoneLev1.UnitMeasureVisible = false;
      this.nud_TailOutCutsDoneLev1.Value = "0";
      this.nud_TailOutCutsDoneLev1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_TailOutCutsDoneLev1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_TailOutCutsRequiredLev3
      // 
      this.nud_TailOutCutsRequiredLev3.DecimalPlaces = 0;
      this.nud_TailOutCutsRequiredLev3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_TailOutCutsRequiredLev3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_TailOutCutsRequiredLev3.ImagePassword = null;
      this.nud_TailOutCutsRequiredLev3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_TailOutCutsRequiredLev3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_TailOutCutsRequiredLev3.Location = new System.Drawing.Point(634, 411);
      this.nud_TailOutCutsRequiredLev3.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_TailOutCutsRequiredLev3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_TailOutCutsRequiredLev3.Name = "nud_TailOutCutsRequiredLev3";
      this.nud_TailOutCutsRequiredLev3.ReadOnly = true;
      this.nud_TailOutCutsRequiredLev3.Size = new System.Drawing.Size(49, 19);
      this.nud_TailOutCutsRequiredLev3.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_TailOutCutsRequiredLev3.TabIndex = 380;
      this.nud_TailOutCutsRequiredLev3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_TailOutCutsRequiredLev3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_TailOutCutsRequiredLev3.TranslateText = false;
      this.nud_TailOutCutsRequiredLev3.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_TailOutCutsRequiredLev3.UnitMeasureVisible = false;
      this.nud_TailOutCutsRequiredLev3.Value = "0";
      this.nud_TailOutCutsRequiredLev3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_TailOutCutsRequiredLev3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_TailOutCutsRequiredLev2
      // 
      this.nud_TailOutCutsRequiredLev2.DecimalPlaces = 0;
      this.nud_TailOutCutsRequiredLev2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_TailOutCutsRequiredLev2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_TailOutCutsRequiredLev2.ImagePassword = null;
      this.nud_TailOutCutsRequiredLev2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_TailOutCutsRequiredLev2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_TailOutCutsRequiredLev2.Location = new System.Drawing.Point(527, 411);
      this.nud_TailOutCutsRequiredLev2.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_TailOutCutsRequiredLev2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_TailOutCutsRequiredLev2.Name = "nud_TailOutCutsRequiredLev2";
      this.nud_TailOutCutsRequiredLev2.ReadOnly = true;
      this.nud_TailOutCutsRequiredLev2.Size = new System.Drawing.Size(49, 19);
      this.nud_TailOutCutsRequiredLev2.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_TailOutCutsRequiredLev2.TabIndex = 379;
      this.nud_TailOutCutsRequiredLev2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_TailOutCutsRequiredLev2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_TailOutCutsRequiredLev2.TranslateText = false;
      this.nud_TailOutCutsRequiredLev2.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_TailOutCutsRequiredLev2.UnitMeasureVisible = false;
      this.nud_TailOutCutsRequiredLev2.Value = "0";
      this.nud_TailOutCutsRequiredLev2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_TailOutCutsRequiredLev2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_TailOutCutsRequiredLev1
      // 
      this.nud_TailOutCutsRequiredLev1.DecimalPlaces = 0;
      this.nud_TailOutCutsRequiredLev1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_TailOutCutsRequiredLev1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_TailOutCutsRequiredLev1.ImagePassword = null;
      this.nud_TailOutCutsRequiredLev1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_TailOutCutsRequiredLev1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_TailOutCutsRequiredLev1.Location = new System.Drawing.Point(422, 411);
      this.nud_TailOutCutsRequiredLev1.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_TailOutCutsRequiredLev1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_TailOutCutsRequiredLev1.Name = "nud_TailOutCutsRequiredLev1";
      this.nud_TailOutCutsRequiredLev1.ReadOnly = true;
      this.nud_TailOutCutsRequiredLev1.Size = new System.Drawing.Size(49, 19);
      this.nud_TailOutCutsRequiredLev1.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_TailOutCutsRequiredLev1.TabIndex = 378;
      this.nud_TailOutCutsRequiredLev1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_TailOutCutsRequiredLev1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_TailOutCutsRequiredLev1.TranslateText = false;
      this.nud_TailOutCutsRequiredLev1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_TailOutCutsRequiredLev1.UnitMeasureVisible = false;
      this.nud_TailOutCutsRequiredLev1.Value = "0";
      this.nud_TailOutCutsRequiredLev1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_TailOutCutsRequiredLev1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // lbl_TailOutCutsDone
      // 
      this.lbl_TailOutCutsDone.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_TailOutCutsDone.Location = new System.Drawing.Point(6, 451);
      this.lbl_TailOutCutsDone.Name = "lbl_TailOutCutsDone";
      this.lbl_TailOutCutsDone.Size = new System.Drawing.Size(410, 20);
      this.lbl_TailOutCutsDone.TabIndex = 377;
      this.lbl_TailOutCutsDone.Text = "Tail Out Cuts Done";
      this.lbl_TailOutCutsDone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_TailOutCutsRequired
      // 
      this.lbl_TailOutCutsRequired.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_TailOutCutsRequired.Location = new System.Drawing.Point(6, 410);
      this.lbl_TailOutCutsRequired.Name = "lbl_TailOutCutsRequired";
      this.lbl_TailOutCutsRequired.Size = new System.Drawing.Size(410, 20);
      this.lbl_TailOutCutsRequired.TabIndex = 376;
      this.lbl_TailOutCutsRequired.Text = "Tail Out Cuts Required";
      this.lbl_TailOutCutsRequired.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_LastCutLev3
      // 
      this.chk_LastCutLev3.AutoSize = true;
      this.chk_LastCutLev3.Location = new System.Drawing.Point(634, 394);
      this.chk_LastCutLev3.Name = "chk_LastCutLev3";
      this.chk_LastCutLev3.Size = new System.Drawing.Size(15, 14);
      this.chk_LastCutLev3.TabIndex = 375;
      this.chk_LastCutLev3.UseVisualStyleBackColor = true;
      // 
      // chk_LastCutLev2
      // 
      this.chk_LastCutLev2.AutoSize = true;
      this.chk_LastCutLev2.Location = new System.Drawing.Point(527, 394);
      this.chk_LastCutLev2.Name = "chk_LastCutLev2";
      this.chk_LastCutLev2.Size = new System.Drawing.Size(15, 14);
      this.chk_LastCutLev2.TabIndex = 374;
      this.chk_LastCutLev2.UseVisualStyleBackColor = true;
      // 
      // chk_LastCutLev1
      // 
      this.chk_LastCutLev1.AutoSize = true;
      this.chk_LastCutLev1.Location = new System.Drawing.Point(422, 394);
      this.chk_LastCutLev1.Name = "chk_LastCutLev1";
      this.chk_LastCutLev1.Size = new System.Drawing.Size(15, 14);
      this.chk_LastCutLev1.TabIndex = 373;
      this.chk_LastCutLev1.UseVisualStyleBackColor = true;
      // 
      // lbl_LastCut
      // 
      this.lbl_LastCut.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_LastCut.Location = new System.Drawing.Point(6, 390);
      this.lbl_LastCut.Name = "lbl_LastCut";
      this.lbl_LastCut.Size = new System.Drawing.Size(410, 20);
      this.lbl_LastCut.TabIndex = 372;
      this.lbl_LastCut.Text = "Last Cut";
      this.lbl_LastCut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_KnifeHasChangedLev3
      // 
      this.chk_KnifeHasChangedLev3.AutoSize = true;
      this.chk_KnifeHasChangedLev3.Location = new System.Drawing.Point(634, 374);
      this.chk_KnifeHasChangedLev3.Name = "chk_KnifeHasChangedLev3";
      this.chk_KnifeHasChangedLev3.Size = new System.Drawing.Size(15, 14);
      this.chk_KnifeHasChangedLev3.TabIndex = 371;
      this.chk_KnifeHasChangedLev3.UseVisualStyleBackColor = true;
      // 
      // chk_KnifeHasChangedLev2
      // 
      this.chk_KnifeHasChangedLev2.AutoSize = true;
      this.chk_KnifeHasChangedLev2.Location = new System.Drawing.Point(527, 374);
      this.chk_KnifeHasChangedLev2.Name = "chk_KnifeHasChangedLev2";
      this.chk_KnifeHasChangedLev2.Size = new System.Drawing.Size(15, 14);
      this.chk_KnifeHasChangedLev2.TabIndex = 370;
      this.chk_KnifeHasChangedLev2.UseVisualStyleBackColor = true;
      // 
      // chk_KnifeHasChangedLev1
      // 
      this.chk_KnifeHasChangedLev1.AutoSize = true;
      this.chk_KnifeHasChangedLev1.Location = new System.Drawing.Point(422, 374);
      this.chk_KnifeHasChangedLev1.Name = "chk_KnifeHasChangedLev1";
      this.chk_KnifeHasChangedLev1.Size = new System.Drawing.Size(15, 14);
      this.chk_KnifeHasChangedLev1.TabIndex = 369;
      this.chk_KnifeHasChangedLev1.UseVisualStyleBackColor = true;
      // 
      // lbl_KnifeHasChanged
      // 
      this.lbl_KnifeHasChanged.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_KnifeHasChanged.Location = new System.Drawing.Point(6, 370);
      this.lbl_KnifeHasChanged.Name = "lbl_KnifeHasChanged";
      this.lbl_KnifeHasChanged.Size = new System.Drawing.Size(410, 20);
      this.lbl_KnifeHasChanged.TabIndex = 368;
      this.lbl_KnifeHasChanged.Text = "Knife Has Changed";
      this.lbl_KnifeHasChanged.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_ChangeNowLev3
      // 
      this.chk_ChangeNowLev3.AutoSize = true;
      this.chk_ChangeNowLev3.Location = new System.Drawing.Point(634, 354);
      this.chk_ChangeNowLev3.Name = "chk_ChangeNowLev3";
      this.chk_ChangeNowLev3.Size = new System.Drawing.Size(15, 14);
      this.chk_ChangeNowLev3.TabIndex = 367;
      this.chk_ChangeNowLev3.UseVisualStyleBackColor = true;
      // 
      // chk_ChangeNowLev2
      // 
      this.chk_ChangeNowLev2.AutoSize = true;
      this.chk_ChangeNowLev2.Location = new System.Drawing.Point(527, 354);
      this.chk_ChangeNowLev2.Name = "chk_ChangeNowLev2";
      this.chk_ChangeNowLev2.Size = new System.Drawing.Size(15, 14);
      this.chk_ChangeNowLev2.TabIndex = 366;
      this.chk_ChangeNowLev2.UseVisualStyleBackColor = true;
      // 
      // chk_ChangeNowLev1
      // 
      this.chk_ChangeNowLev1.AutoSize = true;
      this.chk_ChangeNowLev1.Location = new System.Drawing.Point(422, 354);
      this.chk_ChangeNowLev1.Name = "chk_ChangeNowLev1";
      this.chk_ChangeNowLev1.Size = new System.Drawing.Size(15, 14);
      this.chk_ChangeNowLev1.TabIndex = 365;
      this.chk_ChangeNowLev1.UseVisualStyleBackColor = true;
      // 
      // lbl_ChangeNow
      // 
      this.lbl_ChangeNow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ChangeNow.Location = new System.Drawing.Point(6, 350);
      this.lbl_ChangeNow.Name = "lbl_ChangeNow";
      this.lbl_ChangeNow.Size = new System.Drawing.Size(410, 20);
      this.lbl_ChangeNow.TabIndex = 364;
      this.lbl_ChangeNow.Text = "Change Now";
      this.lbl_ChangeNow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_ChangeAtDefaultLengthLev3
      // 
      this.chk_ChangeAtDefaultLengthLev3.AutoSize = true;
      this.chk_ChangeAtDefaultLengthLev3.Location = new System.Drawing.Point(634, 334);
      this.chk_ChangeAtDefaultLengthLev3.Name = "chk_ChangeAtDefaultLengthLev3";
      this.chk_ChangeAtDefaultLengthLev3.Size = new System.Drawing.Size(15, 14);
      this.chk_ChangeAtDefaultLengthLev3.TabIndex = 363;
      this.chk_ChangeAtDefaultLengthLev3.UseVisualStyleBackColor = true;
      // 
      // chk_ChangeAtDefaultLengthLev2
      // 
      this.chk_ChangeAtDefaultLengthLev2.AutoSize = true;
      this.chk_ChangeAtDefaultLengthLev2.Location = new System.Drawing.Point(527, 334);
      this.chk_ChangeAtDefaultLengthLev2.Name = "chk_ChangeAtDefaultLengthLev2";
      this.chk_ChangeAtDefaultLengthLev2.Size = new System.Drawing.Size(15, 14);
      this.chk_ChangeAtDefaultLengthLev2.TabIndex = 362;
      this.chk_ChangeAtDefaultLengthLev2.UseVisualStyleBackColor = true;
      // 
      // chk_ChangeAtDefaultLengthLev1
      // 
      this.chk_ChangeAtDefaultLengthLev1.AutoSize = true;
      this.chk_ChangeAtDefaultLengthLev1.Location = new System.Drawing.Point(422, 334);
      this.chk_ChangeAtDefaultLengthLev1.Name = "chk_ChangeAtDefaultLengthLev1";
      this.chk_ChangeAtDefaultLengthLev1.Size = new System.Drawing.Size(15, 14);
      this.chk_ChangeAtDefaultLengthLev1.TabIndex = 361;
      this.chk_ChangeAtDefaultLengthLev1.UseVisualStyleBackColor = true;
      // 
      // lbl_ChangeAtDefaultLength
      // 
      this.lbl_ChangeAtDefaultLength.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ChangeAtDefaultLength.Location = new System.Drawing.Point(6, 330);
      this.lbl_ChangeAtDefaultLength.Name = "lbl_ChangeAtDefaultLength";
      this.lbl_ChangeAtDefaultLength.Size = new System.Drawing.Size(410, 20);
      this.lbl_ChangeAtDefaultLength.TabIndex = 360;
      this.lbl_ChangeAtDefaultLength.Text = "Change at Default Length";
      this.lbl_ChangeAtDefaultLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_NewSetupAtMW
      // 
      this.chk_NewSetupAtMW.AutoSize = true;
      this.chk_NewSetupAtMW.Location = new System.Drawing.Point(422, 314);
      this.chk_NewSetupAtMW.Name = "chk_NewSetupAtMW";
      this.chk_NewSetupAtMW.Size = new System.Drawing.Size(15, 14);
      this.chk_NewSetupAtMW.TabIndex = 359;
      this.chk_NewSetupAtMW.UseVisualStyleBackColor = true;
      // 
      // lbl_NewSetupAtMW
      // 
      this.lbl_NewSetupAtMW.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_NewSetupAtMW.Location = new System.Drawing.Point(6, 310);
      this.lbl_NewSetupAtMW.Name = "lbl_NewSetupAtMW";
      this.lbl_NewSetupAtMW.Size = new System.Drawing.Size(410, 20);
      this.lbl_NewSetupAtMW.TabIndex = 358;
      this.lbl_NewSetupAtMW.Text = "New Setup at MW";
      this.lbl_NewSetupAtMW.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_OldSetupAtMW
      // 
      this.chk_OldSetupAtMW.AutoSize = true;
      this.chk_OldSetupAtMW.Location = new System.Drawing.Point(422, 294);
      this.chk_OldSetupAtMW.Name = "chk_OldSetupAtMW";
      this.chk_OldSetupAtMW.Size = new System.Drawing.Size(15, 14);
      this.chk_OldSetupAtMW.TabIndex = 357;
      this.chk_OldSetupAtMW.UseVisualStyleBackColor = true;
      // 
      // lbl_OldSetupAtMW
      // 
      this.lbl_OldSetupAtMW.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_OldSetupAtMW.Location = new System.Drawing.Point(6, 290);
      this.lbl_OldSetupAtMW.Name = "lbl_OldSetupAtMW";
      this.lbl_OldSetupAtMW.Size = new System.Drawing.Size(410, 20);
      this.lbl_OldSetupAtMW.TabIndex = 356;
      this.lbl_OldSetupAtMW.Text = "Old Setup at MW";
      this.lbl_OldSetupAtMW.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_SyncKnifeCutReceived
      // 
      this.chk_SyncKnifeCutReceived.AutoSize = true;
      this.chk_SyncKnifeCutReceived.Location = new System.Drawing.Point(422, 274);
      this.chk_SyncKnifeCutReceived.Name = "chk_SyncKnifeCutReceived";
      this.chk_SyncKnifeCutReceived.Size = new System.Drawing.Size(15, 14);
      this.chk_SyncKnifeCutReceived.TabIndex = 355;
      this.chk_SyncKnifeCutReceived.UseVisualStyleBackColor = true;
      // 
      // lbl_SyncKnifeCutReceived
      // 
      this.lbl_SyncKnifeCutReceived.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SyncKnifeCutReceived.Location = new System.Drawing.Point(6, 270);
      this.lbl_SyncKnifeCutReceived.Name = "lbl_SyncKnifeCutReceived";
      this.lbl_SyncKnifeCutReceived.Size = new System.Drawing.Size(410, 20);
      this.lbl_SyncKnifeCutReceived.TabIndex = 354;
      this.lbl_SyncKnifeCutReceived.Text = "Sync Knife Cut Received";
      this.lbl_SyncKnifeCutReceived.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_Auto
      // 
      this.chk_Auto.AutoSize = true;
      this.chk_Auto.Location = new System.Drawing.Point(634, 13);
      this.chk_Auto.Name = "chk_Auto";
      this.chk_Auto.Size = new System.Drawing.Size(48, 17);
      this.chk_Auto.TabIndex = 353;
      this.chk_Auto.Text = "Auto";
      this.chk_Auto.UseVisualStyleBackColor = true;
      // 
      // chk_Semi
      // 
      this.chk_Semi.AutoSize = true;
      this.chk_Semi.Location = new System.Drawing.Point(527, 13);
      this.chk_Semi.Name = "chk_Semi";
      this.chk_Semi.Size = new System.Drawing.Size(49, 17);
      this.chk_Semi.TabIndex = 352;
      this.chk_Semi.Text = "Semi";
      this.chk_Semi.UseVisualStyleBackColor = true;
      // 
      // chk_Manual
      // 
      this.chk_Manual.AutoSize = true;
      this.chk_Manual.Location = new System.Drawing.Point(422, 13);
      this.chk_Manual.Name = "chk_Manual";
      this.chk_Manual.Size = new System.Drawing.Size(61, 17);
      this.chk_Manual.TabIndex = 351;
      this.chk_Manual.Text = "Manual";
      this.chk_Manual.UseVisualStyleBackColor = true;
      // 
      // lbl_StartAoc
      // 
      this.lbl_StartAoc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_StartAoc.Location = new System.Drawing.Point(6, 9);
      this.lbl_StartAoc.Name = "lbl_StartAoc";
      this.lbl_StartAoc.Size = new System.Drawing.Size(410, 20);
      this.lbl_StartAoc.TabIndex = 350;
      this.lbl_StartAoc.Text = "Start AOC";
      this.lbl_StartAoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_MTS_TER
      // 
      this.chk_MTS_TER.AutoSize = true;
      this.chk_MTS_TER.Location = new System.Drawing.Point(743, 33);
      this.chk_MTS_TER.Name = "chk_MTS_TER";
      this.chk_MTS_TER.Size = new System.Drawing.Size(77, 17);
      this.chk_MTS_TER.TabIndex = 349;
      this.chk_MTS_TER.Text = "MTS+TER";
      this.chk_MTS_TER.UseVisualStyleBackColor = true;
      // 
      // chk_TandemAOC
      // 
      this.chk_TandemAOC.AutoSize = true;
      this.chk_TandemAOC.Location = new System.Drawing.Point(634, 33);
      this.chk_TandemAOC.Name = "chk_TandemAOC";
      this.chk_TandemAOC.Size = new System.Drawing.Size(65, 17);
      this.chk_TandemAOC.TabIndex = 348;
      this.chk_TandemAOC.Text = "Tandem";
      this.chk_TandemAOC.UseVisualStyleBackColor = true;
      // 
      // chk_NormalAOC
      // 
      this.chk_NormalAOC.AutoSize = true;
      this.chk_NormalAOC.Location = new System.Drawing.Point(527, 33);
      this.chk_NormalAOC.Name = "chk_NormalAOC";
      this.chk_NormalAOC.Size = new System.Drawing.Size(59, 17);
      this.chk_NormalAOC.TabIndex = 347;
      this.chk_NormalAOC.Text = "Normal";
      this.chk_NormalAOC.UseVisualStyleBackColor = true;
      // 
      // chk_NoGapAOC
      // 
      this.chk_NoGapAOC.AutoSize = true;
      this.chk_NoGapAOC.Location = new System.Drawing.Point(422, 33);
      this.chk_NoGapAOC.Name = "chk_NoGapAOC";
      this.chk_NoGapAOC.Size = new System.Drawing.Size(60, 17);
      this.chk_NoGapAOC.TabIndex = 346;
      this.chk_NoGapAOC.Text = "NoGap";
      this.chk_NoGapAOC.UseVisualStyleBackColor = true;
      // 
      // lbl_TypeAOC
      // 
      this.lbl_TypeAOC.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_TypeAOC.Location = new System.Drawing.Point(6, 29);
      this.lbl_TypeAOC.Name = "lbl_TypeAOC";
      this.lbl_TypeAOC.Size = new System.Drawing.Size(410, 20);
      this.lbl_TypeAOC.TabIndex = 345;
      this.lbl_TypeAOC.Text = "Type AOC";
      this.lbl_TypeAOC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_RSHStartedAOC
      // 
      this.chk_RSHStartedAOC.AutoSize = true;
      this.chk_RSHStartedAOC.Location = new System.Drawing.Point(422, 214);
      this.chk_RSHStartedAOC.Name = "chk_RSHStartedAOC";
      this.chk_RSHStartedAOC.Size = new System.Drawing.Size(15, 14);
      this.chk_RSHStartedAOC.TabIndex = 344;
      this.chk_RSHStartedAOC.UseVisualStyleBackColor = true;
      // 
      // lbl_RSHStartedAOC
      // 
      this.lbl_RSHStartedAOC.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_RSHStartedAOC.Location = new System.Drawing.Point(6, 210);
      this.lbl_RSHStartedAOC.Name = "lbl_RSHStartedAOC";
      this.lbl_RSHStartedAOC.Size = new System.Drawing.Size(410, 20);
      this.lbl_RSHStartedAOC.TabIndex = 343;
      this.lbl_RSHStartedAOC.Text = "RSH Started AOC";
      this.lbl_RSHStartedAOC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_DBAtAOCSpeed
      // 
      this.chk_DBAtAOCSpeed.AutoSize = true;
      this.chk_DBAtAOCSpeed.Location = new System.Drawing.Point(422, 194);
      this.chk_DBAtAOCSpeed.Name = "chk_DBAtAOCSpeed";
      this.chk_DBAtAOCSpeed.Size = new System.Drawing.Size(15, 14);
      this.chk_DBAtAOCSpeed.TabIndex = 342;
      this.chk_DBAtAOCSpeed.UseVisualStyleBackColor = true;
      // 
      // lbl_DBAtAOCSpeed
      // 
      this.lbl_DBAtAOCSpeed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DBAtAOCSpeed.Location = new System.Drawing.Point(6, 190);
      this.lbl_DBAtAOCSpeed.Name = "lbl_DBAtAOCSpeed";
      this.lbl_DBAtAOCSpeed.Size = new System.Drawing.Size(410, 20);
      this.lbl_DBAtAOCSpeed.TabIndex = 341;
      this.lbl_DBAtAOCSpeed.Text = "DB at AOC Speed";
      this.lbl_DBAtAOCSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_WaitingSSReady
      // 
      this.chk_WaitingSSReady.AutoSize = true;
      this.chk_WaitingSSReady.Location = new System.Drawing.Point(422, 174);
      this.chk_WaitingSSReady.Name = "chk_WaitingSSReady";
      this.chk_WaitingSSReady.Size = new System.Drawing.Size(15, 14);
      this.chk_WaitingSSReady.TabIndex = 340;
      this.chk_WaitingSSReady.UseVisualStyleBackColor = true;
      // 
      // lbl_WaitingSSReady
      // 
      this.lbl_WaitingSSReady.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_WaitingSSReady.Location = new System.Drawing.Point(6, 170);
      this.lbl_WaitingSSReady.Name = "lbl_WaitingSSReady";
      this.lbl_WaitingSSReady.Size = new System.Drawing.Size(410, 20);
      this.lbl_WaitingSSReady.TabIndex = 339;
      this.lbl_WaitingSSReady.Text = "Waiting Slitter Scorer Ready";
      this.lbl_WaitingSSReady.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_WaitingMachinesReady
      // 
      this.chk_WaitingMachinesReady.AutoSize = true;
      this.chk_WaitingMachinesReady.Location = new System.Drawing.Point(422, 154);
      this.chk_WaitingMachinesReady.Name = "chk_WaitingMachinesReady";
      this.chk_WaitingMachinesReady.Size = new System.Drawing.Size(15, 14);
      this.chk_WaitingMachinesReady.TabIndex = 338;
      this.chk_WaitingMachinesReady.UseVisualStyleBackColor = true;
      // 
      // lbl_WaitingMachinesReady
      // 
      this.lbl_WaitingMachinesReady.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_WaitingMachinesReady.Location = new System.Drawing.Point(6, 150);
      this.lbl_WaitingMachinesReady.Name = "lbl_WaitingMachinesReady";
      this.lbl_WaitingMachinesReady.Size = new System.Drawing.Size(410, 20);
      this.lbl_WaitingMachinesReady.TabIndex = 337;
      this.lbl_WaitingMachinesReady.Text = "Waiting Machines Ready";
      this.lbl_WaitingMachinesReady.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_SlowDown
      // 
      this.chk_SlowDown.AutoSize = true;
      this.chk_SlowDown.Location = new System.Drawing.Point(422, 134);
      this.chk_SlowDown.Name = "chk_SlowDown";
      this.chk_SlowDown.Size = new System.Drawing.Size(15, 14);
      this.chk_SlowDown.TabIndex = 336;
      this.chk_SlowDown.UseVisualStyleBackColor = true;
      // 
      // lbl_SlowDown
      // 
      this.lbl_SlowDown.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SlowDown.Location = new System.Drawing.Point(6, 130);
      this.lbl_SlowDown.Name = "lbl_SlowDown";
      this.lbl_SlowDown.Size = new System.Drawing.Size(410, 20);
      this.lbl_SlowDown.TabIndex = 335;
      this.lbl_SlowDown.Text = "Slow Down";
      this.lbl_SlowDown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_PrepareAOC
      // 
      this.chk_PrepareAOC.AutoSize = true;
      this.chk_PrepareAOC.Location = new System.Drawing.Point(422, 113);
      this.chk_PrepareAOC.Name = "chk_PrepareAOC";
      this.chk_PrepareAOC.Size = new System.Drawing.Size(15, 14);
      this.chk_PrepareAOC.TabIndex = 334;
      this.chk_PrepareAOC.UseVisualStyleBackColor = true;
      // 
      // lbl_PrepareAOC
      // 
      this.lbl_PrepareAOC.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_PrepareAOC.Location = new System.Drawing.Point(6, 109);
      this.lbl_PrepareAOC.Name = "lbl_PrepareAOC";
      this.lbl_PrepareAOC.Size = new System.Drawing.Size(410, 20);
      this.lbl_PrepareAOC.TabIndex = 333;
      this.lbl_PrepareAOC.Text = "Prepare AOC";
      this.lbl_PrepareAOC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_EarlyWarning
      // 
      this.chk_EarlyWarning.AutoSize = true;
      this.chk_EarlyWarning.Location = new System.Drawing.Point(422, 93);
      this.chk_EarlyWarning.Name = "chk_EarlyWarning";
      this.chk_EarlyWarning.Size = new System.Drawing.Size(15, 14);
      this.chk_EarlyWarning.TabIndex = 332;
      this.chk_EarlyWarning.UseVisualStyleBackColor = true;
      // 
      // lbl_EarlyWarning
      // 
      this.lbl_EarlyWarning.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_EarlyWarning.Location = new System.Drawing.Point(6, 89);
      this.lbl_EarlyWarning.Name = "lbl_EarlyWarning";
      this.lbl_EarlyWarning.Size = new System.Drawing.Size(410, 20);
      this.lbl_EarlyWarning.TabIndex = 331;
      this.lbl_EarlyWarning.Text = "Early Warning";
      this.lbl_EarlyWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_AOCinProgress
      // 
      this.chk_AOCinProgress.AutoSize = true;
      this.chk_AOCinProgress.Location = new System.Drawing.Point(422, 234);
      this.chk_AOCinProgress.Name = "chk_AOCinProgress";
      this.chk_AOCinProgress.Size = new System.Drawing.Size(15, 14);
      this.chk_AOCinProgress.TabIndex = 330;
      this.chk_AOCinProgress.UseVisualStyleBackColor = true;
      // 
      // lbl_AOCinProgress
      // 
      this.lbl_AOCinProgress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AOCinProgress.Location = new System.Drawing.Point(6, 230);
      this.lbl_AOCinProgress.Name = "lbl_AOCinProgress";
      this.lbl_AOCinProgress.Size = new System.Drawing.Size(410, 20);
      this.lbl_AOCinProgress.TabIndex = 298;
      this.lbl_AOCinProgress.Text = "AOC in Progress";
      this.lbl_AOCinProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.nud_LatAOCNrChopAtRSH);
      this.tabPage1.Controls.Add(this.label15);
      this.tabPage1.Controls.Add(this.nud_DistBetweenFDandEncoderSSAOCPoint);
      this.tabPage1.Controls.Add(this.label14);
      this.tabPage1.Controls.Add(this.chk_LastAOCStartAOCtoSSUsedFD);
      this.tabPage1.Controls.Add(this.label13);
      this.tabPage1.Controls.Add(this.nud_LastAOCRSHLastContinuousCutReactionTimeValue);
      this.tabPage1.Controls.Add(this.nud_LastAOCRSHLastSingleCutReactionTimeValue);
      this.tabPage1.Controls.Add(this.nud_LastAOCRSHReactionTimeEstimatedValue);
      this.tabPage1.Controls.Add(this.nud_LastAOCRSHFireDistanceValue);
      this.tabPage1.Controls.Add(this.nud_LastAOCSyncLengthValue);
      this.tabPage1.Controls.Add(this.nud_LastAOCSyncKnifeValue);
      this.tabPage1.Controls.Add(this.nud_LastAOCDryEndAccelerationValue);
      this.tabPage1.Controls.Add(this.label6);
      this.tabPage1.Controls.Add(this.label7);
      this.tabPage1.Controls.Add(this.label8);
      this.tabPage1.Controls.Add(this.label9);
      this.tabPage1.Controls.Add(this.label10);
      this.tabPage1.Controls.Add(this.label11);
      this.tabPage1.Controls.Add(this.label12);
      this.tabPage1.Controls.Add(this.nud_LastAOCDefaultCutsRequiredLev3);
      this.tabPage1.Controls.Add(this.nud_LastAOCDefaultCutsRequiredLev2);
      this.tabPage1.Controls.Add(this.nud_LastAOCDefaultCutsRequiredLev1);
      this.tabPage1.Controls.Add(this.label4);
      this.tabPage1.Controls.Add(this.nud_LastAOCTailOutCutsRequiredLev3);
      this.tabPage1.Controls.Add(this.nud_LastAOCTailOutCutsRequiredLev2);
      this.tabPage1.Controls.Add(this.nud_LastAOCTailOutCutsRequiredLev1);
      this.tabPage1.Controls.Add(this.label5);
      this.tabPage1.Controls.Add(this.nud_LastAOCStartAOCSpeed);
      this.tabPage1.Controls.Add(this.label2);
      this.tabPage1.Controls.Add(this.chk_LastAOCMTS_TER);
      this.tabPage1.Controls.Add(this.chk_LastAOCTandemAOC);
      this.tabPage1.Controls.Add(this.chk_LastAOCNormalAOC);
      this.tabPage1.Controls.Add(this.chk_LastAOCNoGapAOC);
      this.tabPage1.Controls.Add(this.label3);
      this.tabPage1.Controls.Add(this.chk_LastAOCAuto);
      this.tabPage1.Controls.Add(this.chk_LastAOCSemi);
      this.tabPage1.Controls.Add(this.chk_LastAOCManual);
      this.tabPage1.Controls.Add(this.label1);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(1163, 493);
      this.tabPage1.TabIndex = 2;
      this.tabPage1.Text = "Last AOC Calculation";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // nud_LatAOCNrChopAtRSH
      // 
      this.nud_LatAOCNrChopAtRSH.DecimalPlaces = 0;
      this.nud_LatAOCNrChopAtRSH.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LatAOCNrChopAtRSH.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LatAOCNrChopAtRSH.ImagePassword = null;
      this.nud_LatAOCNrChopAtRSH.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LatAOCNrChopAtRSH.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LatAOCNrChopAtRSH.Location = new System.Drawing.Point(434, 185);
      this.nud_LatAOCNrChopAtRSH.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LatAOCNrChopAtRSH.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LatAOCNrChopAtRSH.Name = "nud_LatAOCNrChopAtRSH";
      this.nud_LatAOCNrChopAtRSH.ReadOnly = true;
      this.nud_LatAOCNrChopAtRSH.Size = new System.Drawing.Size(95, 19);
      this.nud_LatAOCNrChopAtRSH.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LatAOCNrChopAtRSH.TabIndex = 421;
      this.nud_LatAOCNrChopAtRSH.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LatAOCNrChopAtRSH.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LatAOCNrChopAtRSH.TranslateText = false;
      this.nud_LatAOCNrChopAtRSH.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LatAOCNrChopAtRSH.UnitMeasureVisible = false;
      this.nud_LatAOCNrChopAtRSH.Value = "0";
      this.nud_LatAOCNrChopAtRSH.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LatAOCNrChopAtRSH.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // label15
      // 
      this.label15.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label15.Location = new System.Drawing.Point(18, 184);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(410, 20);
      this.label15.TabIndex = 420;
      this.label15.Text = "Nr Chop at RSH";
      this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // nud_DistBetweenFDandEncoderSSAOCPoint
      // 
      this.nud_DistBetweenFDandEncoderSSAOCPoint.DecimalPlaces = 0;
      this.nud_DistBetweenFDandEncoderSSAOCPoint.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_DistBetweenFDandEncoderSSAOCPoint.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DistBetweenFDandEncoderSSAOCPoint.ImagePassword = null;
      this.nud_DistBetweenFDandEncoderSSAOCPoint.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_DistBetweenFDandEncoderSSAOCPoint.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DistBetweenFDandEncoderSSAOCPoint.Location = new System.Drawing.Point(434, 126);
      this.nud_DistBetweenFDandEncoderSSAOCPoint.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_DistBetweenFDandEncoderSSAOCPoint.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_DistBetweenFDandEncoderSSAOCPoint.Name = "nud_DistBetweenFDandEncoderSSAOCPoint";
      this.nud_DistBetweenFDandEncoderSSAOCPoint.ReadOnly = true;
      this.nud_DistBetweenFDandEncoderSSAOCPoint.Size = new System.Drawing.Size(95, 19);
      this.nud_DistBetweenFDandEncoderSSAOCPoint.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_DistBetweenFDandEncoderSSAOCPoint.TabIndex = 419;
      this.nud_DistBetweenFDandEncoderSSAOCPoint.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DistBetweenFDandEncoderSSAOCPoint.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DistBetweenFDandEncoderSSAOCPoint.TranslateText = false;
      this.nud_DistBetweenFDandEncoderSSAOCPoint.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DistBetweenFDandEncoderSSAOCPoint.UnitMeasureVisible = false;
      this.nud_DistBetweenFDandEncoderSSAOCPoint.Value = "0";
      this.nud_DistBetweenFDandEncoderSSAOCPoint.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_DistBetweenFDandEncoderSSAOCPoint.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // label14
      // 
      this.label14.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label14.Location = new System.Drawing.Point(18, 125);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(410, 20);
      this.label14.TabIndex = 418;
      this.label14.Text = "Distance between SS AOC Point and FD/MW";
      this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_LastAOCStartAOCtoSSUsedFD
      // 
      this.chk_LastAOCStartAOCtoSSUsedFD.AutoSize = true;
      this.chk_LastAOCStartAOCtoSSUsedFD.Location = new System.Drawing.Point(434, 101);
      this.chk_LastAOCStartAOCtoSSUsedFD.Name = "chk_LastAOCStartAOCtoSSUsedFD";
      this.chk_LastAOCStartAOCtoSSUsedFD.Size = new System.Drawing.Size(63, 17);
      this.chk_LastAOCStartAOCtoSSUsedFD.TabIndex = 417;
      this.chk_LastAOCStartAOCtoSSUsedFD.Text = "FD MW";
      this.chk_LastAOCStartAOCtoSSUsedFD.UseVisualStyleBackColor = true;
      // 
      // label13
      // 
      this.label13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label13.Location = new System.Drawing.Point(18, 98);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(410, 20);
      this.label13.TabIndex = 415;
      this.label13.Text = "Start AOC x SS from";
      this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // nud_LastAOCRSHLastContinuousCutReactionTimeValue
      // 
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.DecimalPlaces = 0;
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.ImagePassword = null;
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.Location = new System.Drawing.Point(434, 264);
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.Name = "nud_LastAOCRSHLastContinuousCutReactionTimeValue";
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.ReadOnly = true;
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.Size = new System.Drawing.Size(95, 19);
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.TabIndex = 414;
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.TranslateText = false;
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.UnitMeasureVisible = false;
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.Value = "0";
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LastAOCRSHLastContinuousCutReactionTimeValue.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_LastAOCRSHLastSingleCutReactionTimeValue
      // 
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.DecimalPlaces = 0;
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.ImagePassword = null;
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.Location = new System.Drawing.Point(434, 245);
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.Name = "nud_LastAOCRSHLastSingleCutReactionTimeValue";
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.ReadOnly = true;
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.Size = new System.Drawing.Size(95, 19);
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.TabIndex = 413;
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.TranslateText = false;
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.UnitMeasureVisible = false;
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.Value = "0";
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LastAOCRSHLastSingleCutReactionTimeValue.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_LastAOCRSHReactionTimeEstimatedValue
      // 
      this.nud_LastAOCRSHReactionTimeEstimatedValue.DecimalPlaces = 0;
      this.nud_LastAOCRSHReactionTimeEstimatedValue.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LastAOCRSHReactionTimeEstimatedValue.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCRSHReactionTimeEstimatedValue.ImagePassword = null;
      this.nud_LastAOCRSHReactionTimeEstimatedValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LastAOCRSHReactionTimeEstimatedValue.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCRSHReactionTimeEstimatedValue.Location = new System.Drawing.Point(434, 225);
      this.nud_LastAOCRSHReactionTimeEstimatedValue.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LastAOCRSHReactionTimeEstimatedValue.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LastAOCRSHReactionTimeEstimatedValue.Name = "nud_LastAOCRSHReactionTimeEstimatedValue";
      this.nud_LastAOCRSHReactionTimeEstimatedValue.ReadOnly = true;
      this.nud_LastAOCRSHReactionTimeEstimatedValue.Size = new System.Drawing.Size(95, 19);
      this.nud_LastAOCRSHReactionTimeEstimatedValue.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LastAOCRSHReactionTimeEstimatedValue.TabIndex = 412;
      this.nud_LastAOCRSHReactionTimeEstimatedValue.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LastAOCRSHReactionTimeEstimatedValue.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCRSHReactionTimeEstimatedValue.TranslateText = false;
      this.nud_LastAOCRSHReactionTimeEstimatedValue.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCRSHReactionTimeEstimatedValue.UnitMeasureVisible = false;
      this.nud_LastAOCRSHReactionTimeEstimatedValue.Value = "0";
      this.nud_LastAOCRSHReactionTimeEstimatedValue.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LastAOCRSHReactionTimeEstimatedValue.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_LastAOCRSHFireDistanceValue
      // 
      this.nud_LastAOCRSHFireDistanceValue.DecimalPlaces = 0;
      this.nud_LastAOCRSHFireDistanceValue.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LastAOCRSHFireDistanceValue.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCRSHFireDistanceValue.ImagePassword = null;
      this.nud_LastAOCRSHFireDistanceValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LastAOCRSHFireDistanceValue.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_LastAOCRSHFireDistanceValue.Location = new System.Drawing.Point(434, 207);
      this.nud_LastAOCRSHFireDistanceValue.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LastAOCRSHFireDistanceValue.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LastAOCRSHFireDistanceValue.Name = "nud_LastAOCRSHFireDistanceValue";
      this.nud_LastAOCRSHFireDistanceValue.ReadOnly = true;
      this.nud_LastAOCRSHFireDistanceValue.Size = new System.Drawing.Size(95, 19);
      this.nud_LastAOCRSHFireDistanceValue.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LastAOCRSHFireDistanceValue.TabIndex = 411;
      this.nud_LastAOCRSHFireDistanceValue.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LastAOCRSHFireDistanceValue.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCRSHFireDistanceValue.TranslateText = false;
      this.nud_LastAOCRSHFireDistanceValue.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_LastAOCRSHFireDistanceValue.UnitMeasureVisible = false;
      this.nud_LastAOCRSHFireDistanceValue.Value = "0";
      this.nud_LastAOCRSHFireDistanceValue.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LastAOCRSHFireDistanceValue.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_LastAOCSyncLengthValue
      // 
      this.nud_LastAOCSyncLengthValue.DecimalPlaces = 0;
      this.nud_LastAOCSyncLengthValue.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LastAOCSyncLengthValue.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCSyncLengthValue.ImagePassword = null;
      this.nud_LastAOCSyncLengthValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LastAOCSyncLengthValue.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_LastAOCSyncLengthValue.Location = new System.Drawing.Point(434, 165);
      this.nud_LastAOCSyncLengthValue.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LastAOCSyncLengthValue.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LastAOCSyncLengthValue.Name = "nud_LastAOCSyncLengthValue";
      this.nud_LastAOCSyncLengthValue.ReadOnly = true;
      this.nud_LastAOCSyncLengthValue.Size = new System.Drawing.Size(95, 19);
      this.nud_LastAOCSyncLengthValue.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LastAOCSyncLengthValue.TabIndex = 410;
      this.nud_LastAOCSyncLengthValue.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LastAOCSyncLengthValue.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCSyncLengthValue.TranslateText = false;
      this.nud_LastAOCSyncLengthValue.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_LastAOCSyncLengthValue.UnitMeasureVisible = false;
      this.nud_LastAOCSyncLengthValue.Value = "0";
      this.nud_LastAOCSyncLengthValue.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LastAOCSyncLengthValue.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_LastAOCSyncKnifeValue
      // 
      this.nud_LastAOCSyncKnifeValue.DecimalPlaces = 0;
      this.nud_LastAOCSyncKnifeValue.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LastAOCSyncKnifeValue.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCSyncKnifeValue.ImagePassword = null;
      this.nud_LastAOCSyncKnifeValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LastAOCSyncKnifeValue.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCSyncKnifeValue.Location = new System.Drawing.Point(434, 146);
      this.nud_LastAOCSyncKnifeValue.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LastAOCSyncKnifeValue.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LastAOCSyncKnifeValue.Name = "nud_LastAOCSyncKnifeValue";
      this.nud_LastAOCSyncKnifeValue.ReadOnly = true;
      this.nud_LastAOCSyncKnifeValue.Size = new System.Drawing.Size(95, 19);
      this.nud_LastAOCSyncKnifeValue.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LastAOCSyncKnifeValue.TabIndex = 409;
      this.nud_LastAOCSyncKnifeValue.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LastAOCSyncKnifeValue.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCSyncKnifeValue.TranslateText = false;
      this.nud_LastAOCSyncKnifeValue.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCSyncKnifeValue.UnitMeasureVisible = false;
      this.nud_LastAOCSyncKnifeValue.Value = "0";
      this.nud_LastAOCSyncKnifeValue.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LastAOCSyncKnifeValue.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_LastAOCDryEndAccelerationValue
      // 
      this.nud_LastAOCDryEndAccelerationValue.DecimalPlaces = 0;
      this.nud_LastAOCDryEndAccelerationValue.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LastAOCDryEndAccelerationValue.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCDryEndAccelerationValue.ImagePassword = null;
      this.nud_LastAOCDryEndAccelerationValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LastAOCDryEndAccelerationValue.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCDryEndAccelerationValue.Location = new System.Drawing.Point(434, 76);
      this.nud_LastAOCDryEndAccelerationValue.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LastAOCDryEndAccelerationValue.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LastAOCDryEndAccelerationValue.Name = "nud_LastAOCDryEndAccelerationValue";
      this.nud_LastAOCDryEndAccelerationValue.ReadOnly = true;
      this.nud_LastAOCDryEndAccelerationValue.Size = new System.Drawing.Size(95, 19);
      this.nud_LastAOCDryEndAccelerationValue.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LastAOCDryEndAccelerationValue.TabIndex = 408;
      this.nud_LastAOCDryEndAccelerationValue.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LastAOCDryEndAccelerationValue.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCDryEndAccelerationValue.TranslateText = false;
      this.nud_LastAOCDryEndAccelerationValue.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCDryEndAccelerationValue.UnitMeasureVisible = false;
      this.nud_LastAOCDryEndAccelerationValue.Value = "0";
      this.nud_LastAOCDryEndAccelerationValue.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LastAOCDryEndAccelerationValue.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // label6
      // 
      this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(18, 263);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(410, 20);
      this.label6.TabIndex = 407;
      this.label6.Text = "RSH Last Continuous Cut ReactionTime";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label7
      // 
      this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(18, 244);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(410, 20);
      this.label7.TabIndex = 406;
      this.label7.Text = "RSH Last Single Cut ReactionTime";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label8
      // 
      this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(18, 225);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(410, 20);
      this.label8.TabIndex = 405;
      this.label8.Text = "RSH ReactionTime Estimated";
      this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label9
      // 
      this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label9.Location = new System.Drawing.Point(18, 75);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(410, 20);
      this.label9.TabIndex = 404;
      this.label9.Text = "DryEnd Acceleration";
      this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label10
      // 
      this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label10.Location = new System.Drawing.Point(18, 206);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(410, 20);
      this.label10.TabIndex = 403;
      this.label10.Text = "RSH Fire Distance";
      this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label11
      // 
      this.label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label11.Location = new System.Drawing.Point(18, 164);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(410, 20);
      this.label11.TabIndex = 402;
      this.label11.Text = "Sync Length";
      this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label12
      // 
      this.label12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label12.Location = new System.Drawing.Point(18, 145);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(410, 20);
      this.label12.TabIndex = 401;
      this.label12.Text = "Sync on Knife";
      this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // nud_LastAOCDefaultCutsRequiredLev3
      // 
      this.nud_LastAOCDefaultCutsRequiredLev3.DecimalPlaces = 0;
      this.nud_LastAOCDefaultCutsRequiredLev3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LastAOCDefaultCutsRequiredLev3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCDefaultCutsRequiredLev3.ImagePassword = null;
      this.nud_LastAOCDefaultCutsRequiredLev3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LastAOCDefaultCutsRequiredLev3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCDefaultCutsRequiredLev3.Location = new System.Drawing.Point(646, 381);
      this.nud_LastAOCDefaultCutsRequiredLev3.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LastAOCDefaultCutsRequiredLev3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LastAOCDefaultCutsRequiredLev3.Name = "nud_LastAOCDefaultCutsRequiredLev3";
      this.nud_LastAOCDefaultCutsRequiredLev3.ReadOnly = true;
      this.nud_LastAOCDefaultCutsRequiredLev3.Size = new System.Drawing.Size(49, 19);
      this.nud_LastAOCDefaultCutsRequiredLev3.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LastAOCDefaultCutsRequiredLev3.TabIndex = 400;
      this.nud_LastAOCDefaultCutsRequiredLev3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LastAOCDefaultCutsRequiredLev3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCDefaultCutsRequiredLev3.TranslateText = false;
      this.nud_LastAOCDefaultCutsRequiredLev3.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCDefaultCutsRequiredLev3.UnitMeasureVisible = false;
      this.nud_LastAOCDefaultCutsRequiredLev3.Value = "0";
      this.nud_LastAOCDefaultCutsRequiredLev3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LastAOCDefaultCutsRequiredLev3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_LastAOCDefaultCutsRequiredLev2
      // 
      this.nud_LastAOCDefaultCutsRequiredLev2.DecimalPlaces = 0;
      this.nud_LastAOCDefaultCutsRequiredLev2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LastAOCDefaultCutsRequiredLev2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCDefaultCutsRequiredLev2.ImagePassword = null;
      this.nud_LastAOCDefaultCutsRequiredLev2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LastAOCDefaultCutsRequiredLev2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCDefaultCutsRequiredLev2.Location = new System.Drawing.Point(539, 381);
      this.nud_LastAOCDefaultCutsRequiredLev2.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LastAOCDefaultCutsRequiredLev2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LastAOCDefaultCutsRequiredLev2.Name = "nud_LastAOCDefaultCutsRequiredLev2";
      this.nud_LastAOCDefaultCutsRequiredLev2.ReadOnly = true;
      this.nud_LastAOCDefaultCutsRequiredLev2.Size = new System.Drawing.Size(49, 19);
      this.nud_LastAOCDefaultCutsRequiredLev2.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LastAOCDefaultCutsRequiredLev2.TabIndex = 399;
      this.nud_LastAOCDefaultCutsRequiredLev2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LastAOCDefaultCutsRequiredLev2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCDefaultCutsRequiredLev2.TranslateText = false;
      this.nud_LastAOCDefaultCutsRequiredLev2.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCDefaultCutsRequiredLev2.UnitMeasureVisible = false;
      this.nud_LastAOCDefaultCutsRequiredLev2.Value = "0";
      this.nud_LastAOCDefaultCutsRequiredLev2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LastAOCDefaultCutsRequiredLev2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_LastAOCDefaultCutsRequiredLev1
      // 
      this.nud_LastAOCDefaultCutsRequiredLev1.DecimalPlaces = 0;
      this.nud_LastAOCDefaultCutsRequiredLev1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LastAOCDefaultCutsRequiredLev1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCDefaultCutsRequiredLev1.ImagePassword = null;
      this.nud_LastAOCDefaultCutsRequiredLev1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LastAOCDefaultCutsRequiredLev1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCDefaultCutsRequiredLev1.Location = new System.Drawing.Point(434, 381);
      this.nud_LastAOCDefaultCutsRequiredLev1.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LastAOCDefaultCutsRequiredLev1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LastAOCDefaultCutsRequiredLev1.Name = "nud_LastAOCDefaultCutsRequiredLev1";
      this.nud_LastAOCDefaultCutsRequiredLev1.ReadOnly = true;
      this.nud_LastAOCDefaultCutsRequiredLev1.Size = new System.Drawing.Size(49, 19);
      this.nud_LastAOCDefaultCutsRequiredLev1.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LastAOCDefaultCutsRequiredLev1.TabIndex = 398;
      this.nud_LastAOCDefaultCutsRequiredLev1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LastAOCDefaultCutsRequiredLev1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCDefaultCutsRequiredLev1.TranslateText = false;
      this.nud_LastAOCDefaultCutsRequiredLev1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCDefaultCutsRequiredLev1.UnitMeasureVisible = false;
      this.nud_LastAOCDefaultCutsRequiredLev1.Value = "0";
      this.nud_LastAOCDefaultCutsRequiredLev1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LastAOCDefaultCutsRequiredLev1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // label4
      // 
      this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(18, 380);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(410, 20);
      this.label4.TabIndex = 397;
      this.label4.Text = "Default Cuts Required";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // nud_LastAOCTailOutCutsRequiredLev3
      // 
      this.nud_LastAOCTailOutCutsRequiredLev3.DecimalPlaces = 0;
      this.nud_LastAOCTailOutCutsRequiredLev3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LastAOCTailOutCutsRequiredLev3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCTailOutCutsRequiredLev3.ImagePassword = null;
      this.nud_LastAOCTailOutCutsRequiredLev3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LastAOCTailOutCutsRequiredLev3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCTailOutCutsRequiredLev3.Location = new System.Drawing.Point(646, 360);
      this.nud_LastAOCTailOutCutsRequiredLev3.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LastAOCTailOutCutsRequiredLev3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LastAOCTailOutCutsRequiredLev3.Name = "nud_LastAOCTailOutCutsRequiredLev3";
      this.nud_LastAOCTailOutCutsRequiredLev3.ReadOnly = true;
      this.nud_LastAOCTailOutCutsRequiredLev3.Size = new System.Drawing.Size(49, 19);
      this.nud_LastAOCTailOutCutsRequiredLev3.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LastAOCTailOutCutsRequiredLev3.TabIndex = 396;
      this.nud_LastAOCTailOutCutsRequiredLev3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LastAOCTailOutCutsRequiredLev3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCTailOutCutsRequiredLev3.TranslateText = false;
      this.nud_LastAOCTailOutCutsRequiredLev3.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCTailOutCutsRequiredLev3.UnitMeasureVisible = false;
      this.nud_LastAOCTailOutCutsRequiredLev3.Value = "0";
      this.nud_LastAOCTailOutCutsRequiredLev3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LastAOCTailOutCutsRequiredLev3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_LastAOCTailOutCutsRequiredLev2
      // 
      this.nud_LastAOCTailOutCutsRequiredLev2.DecimalPlaces = 0;
      this.nud_LastAOCTailOutCutsRequiredLev2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LastAOCTailOutCutsRequiredLev2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCTailOutCutsRequiredLev2.ImagePassword = null;
      this.nud_LastAOCTailOutCutsRequiredLev2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LastAOCTailOutCutsRequiredLev2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCTailOutCutsRequiredLev2.Location = new System.Drawing.Point(539, 360);
      this.nud_LastAOCTailOutCutsRequiredLev2.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LastAOCTailOutCutsRequiredLev2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LastAOCTailOutCutsRequiredLev2.Name = "nud_LastAOCTailOutCutsRequiredLev2";
      this.nud_LastAOCTailOutCutsRequiredLev2.ReadOnly = true;
      this.nud_LastAOCTailOutCutsRequiredLev2.Size = new System.Drawing.Size(49, 19);
      this.nud_LastAOCTailOutCutsRequiredLev2.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LastAOCTailOutCutsRequiredLev2.TabIndex = 395;
      this.nud_LastAOCTailOutCutsRequiredLev2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LastAOCTailOutCutsRequiredLev2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCTailOutCutsRequiredLev2.TranslateText = false;
      this.nud_LastAOCTailOutCutsRequiredLev2.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCTailOutCutsRequiredLev2.UnitMeasureVisible = false;
      this.nud_LastAOCTailOutCutsRequiredLev2.Value = "0";
      this.nud_LastAOCTailOutCutsRequiredLev2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LastAOCTailOutCutsRequiredLev2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_LastAOCTailOutCutsRequiredLev1
      // 
      this.nud_LastAOCTailOutCutsRequiredLev1.DecimalPlaces = 0;
      this.nud_LastAOCTailOutCutsRequiredLev1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LastAOCTailOutCutsRequiredLev1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCTailOutCutsRequiredLev1.ImagePassword = null;
      this.nud_LastAOCTailOutCutsRequiredLev1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LastAOCTailOutCutsRequiredLev1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCTailOutCutsRequiredLev1.Location = new System.Drawing.Point(434, 360);
      this.nud_LastAOCTailOutCutsRequiredLev1.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LastAOCTailOutCutsRequiredLev1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LastAOCTailOutCutsRequiredLev1.Name = "nud_LastAOCTailOutCutsRequiredLev1";
      this.nud_LastAOCTailOutCutsRequiredLev1.ReadOnly = true;
      this.nud_LastAOCTailOutCutsRequiredLev1.Size = new System.Drawing.Size(49, 19);
      this.nud_LastAOCTailOutCutsRequiredLev1.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LastAOCTailOutCutsRequiredLev1.TabIndex = 394;
      this.nud_LastAOCTailOutCutsRequiredLev1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LastAOCTailOutCutsRequiredLev1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCTailOutCutsRequiredLev1.TranslateText = false;
      this.nud_LastAOCTailOutCutsRequiredLev1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_LastAOCTailOutCutsRequiredLev1.UnitMeasureVisible = false;
      this.nud_LastAOCTailOutCutsRequiredLev1.Value = "0";
      this.nud_LastAOCTailOutCutsRequiredLev1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LastAOCTailOutCutsRequiredLev1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // label5
      // 
      this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(18, 359);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(410, 20);
      this.label5.TabIndex = 393;
      this.label5.Text = "Tail Out Cuts Required";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // nud_LastAOCStartAOCSpeed
      // 
      this.nud_LastAOCStartAOCSpeed.DecimalPlaces = 0;
      this.nud_LastAOCStartAOCSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LastAOCStartAOCSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCStartAOCSpeed.ImagePassword = null;
      this.nud_LastAOCStartAOCSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LastAOCStartAOCSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_LastAOCStartAOCSpeed.Location = new System.Drawing.Point(434, 56);
      this.nud_LastAOCStartAOCSpeed.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_LastAOCStartAOCSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_LastAOCStartAOCSpeed.Name = "nud_LastAOCStartAOCSpeed";
      this.nud_LastAOCStartAOCSpeed.ReadOnly = true;
      this.nud_LastAOCStartAOCSpeed.Size = new System.Drawing.Size(95, 19);
      this.nud_LastAOCStartAOCSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_LastAOCStartAOCSpeed.TabIndex = 392;
      this.nud_LastAOCStartAOCSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_LastAOCStartAOCSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_LastAOCStartAOCSpeed.TranslateText = false;
      this.nud_LastAOCStartAOCSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.nud_LastAOCStartAOCSpeed.UnitMeasureVisible = false;
      this.nud_LastAOCStartAOCSpeed.Value = "0";
      this.nud_LastAOCStartAOCSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_LastAOCStartAOCSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // label2
      // 
      this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(18, 55);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(410, 20);
      this.label2.TabIndex = 391;
      this.label2.Text = "AOC Speed";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_LastAOCMTS_TER
      // 
      this.chk_LastAOCMTS_TER.AutoSize = true;
      this.chk_LastAOCMTS_TER.Location = new System.Drawing.Point(755, 39);
      this.chk_LastAOCMTS_TER.Name = "chk_LastAOCMTS_TER";
      this.chk_LastAOCMTS_TER.Size = new System.Drawing.Size(77, 17);
      this.chk_LastAOCMTS_TER.TabIndex = 390;
      this.chk_LastAOCMTS_TER.Text = "MTS+TER";
      this.chk_LastAOCMTS_TER.UseVisualStyleBackColor = true;
      // 
      // chk_LastAOCTandemAOC
      // 
      this.chk_LastAOCTandemAOC.AutoSize = true;
      this.chk_LastAOCTandemAOC.Location = new System.Drawing.Point(646, 39);
      this.chk_LastAOCTandemAOC.Name = "chk_LastAOCTandemAOC";
      this.chk_LastAOCTandemAOC.Size = new System.Drawing.Size(65, 17);
      this.chk_LastAOCTandemAOC.TabIndex = 389;
      this.chk_LastAOCTandemAOC.Text = "Tandem";
      this.chk_LastAOCTandemAOC.UseVisualStyleBackColor = true;
      // 
      // chk_LastAOCNormalAOC
      // 
      this.chk_LastAOCNormalAOC.AutoSize = true;
      this.chk_LastAOCNormalAOC.Location = new System.Drawing.Point(539, 39);
      this.chk_LastAOCNormalAOC.Name = "chk_LastAOCNormalAOC";
      this.chk_LastAOCNormalAOC.Size = new System.Drawing.Size(59, 17);
      this.chk_LastAOCNormalAOC.TabIndex = 388;
      this.chk_LastAOCNormalAOC.Text = "Normal";
      this.chk_LastAOCNormalAOC.UseVisualStyleBackColor = true;
      // 
      // chk_LastAOCNoGapAOC
      // 
      this.chk_LastAOCNoGapAOC.AutoSize = true;
      this.chk_LastAOCNoGapAOC.Location = new System.Drawing.Point(434, 39);
      this.chk_LastAOCNoGapAOC.Name = "chk_LastAOCNoGapAOC";
      this.chk_LastAOCNoGapAOC.Size = new System.Drawing.Size(60, 17);
      this.chk_LastAOCNoGapAOC.TabIndex = 387;
      this.chk_LastAOCNoGapAOC.Text = "NoGap";
      this.chk_LastAOCNoGapAOC.UseVisualStyleBackColor = true;
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(18, 35);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(410, 20);
      this.label3.TabIndex = 386;
      this.label3.Text = "Type AOC";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // chk_LastAOCAuto
      // 
      this.chk_LastAOCAuto.AutoSize = true;
      this.chk_LastAOCAuto.Location = new System.Drawing.Point(646, 19);
      this.chk_LastAOCAuto.Name = "chk_LastAOCAuto";
      this.chk_LastAOCAuto.Size = new System.Drawing.Size(48, 17);
      this.chk_LastAOCAuto.TabIndex = 357;
      this.chk_LastAOCAuto.Text = "Auto";
      this.chk_LastAOCAuto.UseVisualStyleBackColor = true;
      // 
      // chk_LastAOCSemi
      // 
      this.chk_LastAOCSemi.AutoSize = true;
      this.chk_LastAOCSemi.Location = new System.Drawing.Point(539, 19);
      this.chk_LastAOCSemi.Name = "chk_LastAOCSemi";
      this.chk_LastAOCSemi.Size = new System.Drawing.Size(49, 17);
      this.chk_LastAOCSemi.TabIndex = 356;
      this.chk_LastAOCSemi.Text = "Semi";
      this.chk_LastAOCSemi.UseVisualStyleBackColor = true;
      // 
      // chk_LastAOCManual
      // 
      this.chk_LastAOCManual.AutoSize = true;
      this.chk_LastAOCManual.Location = new System.Drawing.Point(434, 19);
      this.chk_LastAOCManual.Name = "chk_LastAOCManual";
      this.chk_LastAOCManual.Size = new System.Drawing.Size(61, 17);
      this.chk_LastAOCManual.TabIndex = 355;
      this.chk_LastAOCManual.Text = "Manual";
      this.chk_LastAOCManual.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(18, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(410, 20);
      this.label1.TabIndex = 354;
      this.label1.Text = "Start AOC";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // nud_SyncLevel
      // 
      this.nud_SyncLevel.DecimalPlaces = 0;
      this.nud_SyncLevel.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_SyncLevel.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SyncLevel.ImagePassword = null;
      this.nud_SyncLevel.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_SyncLevel.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_SyncLevel.Location = new System.Drawing.Point(422, 70);
      this.nud_SyncLevel.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_SyncLevel.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_SyncLevel.Name = "nud_SyncLevel";
      this.nud_SyncLevel.ReadOnly = true;
      this.nud_SyncLevel.Size = new System.Drawing.Size(49, 19);
      this.nud_SyncLevel.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_SyncLevel.TabIndex = 393;
      this.nud_SyncLevel.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_SyncLevel.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SyncLevel.TranslateText = false;
      this.nud_SyncLevel.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_SyncLevel.UnitMeasureVisible = false;
      this.nud_SyncLevel.Value = "0";
      this.nud_SyncLevel.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.nud_SyncLevel.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // lbl_SyncLevel
      // 
      this.lbl_SyncLevel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SyncLevel.Location = new System.Drawing.Point(6, 69);
      this.lbl_SyncLevel.Name = "lbl_SyncLevel";
      this.lbl_SyncLevel.Size = new System.Drawing.Size(410, 20);
      this.lbl_SyncLevel.TabIndex = 392;
      this.lbl_SyncLevel.Text = "Sync Level";
      this.lbl_SyncLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // Frm_AOCStatus
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.AOCStatus);
      this.Name = "Frm_AOCStatus";
      this.Text = "Frm_AOCStatus";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.AOCStatus, 0);
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
      this.AOCStatus.ResumeLayout(false);
      this.tab_Calculations.ResumeLayout(false);
      this.tab_Status.ResumeLayout(false);
      this.tab_Status.PerformLayout();
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.ResumeLayout(false);

		}
		#endregion



	}
}

