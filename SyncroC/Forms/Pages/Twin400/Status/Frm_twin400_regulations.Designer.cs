//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
//namespace SyncroC.Forms.Pages.Twin400.Production
namespace SyncroC.Forms.Pages.Twin400.Status
{
	partial class Frm_twin400_regulations
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_regulations));
      this.SpeedDigitalReference = new SyncroC.Controls.StateButton();
      this.InternalFlap = new SyncroC.Controls.StateButton();
      this.nud_OffsetSlit2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_OffsetScore1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_OffsetScore2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_OffsetScore3 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_OffsetSlit1 = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_PositionOffsets_YN = new System.Windows.Forms.Label();
      this.nud_OffsetSlitsScores = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_ScoreGapOffsets_YN = new System.Windows.Forms.Label();
      this.nud_GapOffsetScore3 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_GapOffsetScore2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_GapOffsetScore1 = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_NonScoringScores_YN = new System.Windows.Forms.Label();
      this.ttl_NonScoringType = new SyncroC.Controls.TitledList();
      this.nud_TrimScoresDistance = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_MinTrim = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_LoadTeflonMeters = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_teflon_YN = new System.Windows.Forms.Label();
      this.nud_OffsetWebDirector = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_OffsetRotary = new SyncroC.Controls.TitledNumericUpDown();
      this.SlowPositioning = new SyncroC.Controls.StateButton();
      this.PhotoeyeSlitShaft = new SyncroC.Controls.StateButton();
      this.ChangeDefaultShaft = new SyncroC.Controls.StateButton();
      this.AdjustScoreGapOffset = new SyncroC.Controls.StateButton();
      this.nud_TeflonCycleMeters = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_TrimChutOffset = new SyncroC.Controls.TitledNumericUpDown();
      this.scrolBarSlitConsumtionOpt = new System.Windows.Forms.HScrollBar();
      this.lbl_SlitConsumptionOpt_YN = new System.Windows.Forms.Label();
      this.lbl_SlitSubstitution_YN = new System.Windows.Forms.Label();
      this.lbl_SlitRegulation_YN = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_TabD
      // 
      this.tab_TabD.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabD.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Text = "Save";
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Text = "Modify";
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
      // SpeedDigitalReference
      // 
      this.SpeedDigitalReference.Active = false;
      this.SpeedDigitalReference.Alarmed = false;
      this.SpeedDigitalReference.Auto = false;
      this.SpeedDigitalReference.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SpeedDigitalReference.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SpeedDigitalReference.FillBackColorWithImage = false;
      this.SpeedDigitalReference.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SpeedDigitalReference.ForeColor = System.Drawing.Color.Black;
      this.SpeedDigitalReference.FramePresent = true;
      this.SpeedDigitalReference.HighLightEnable = false;
      this.SpeedDigitalReference.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SpeedDigitalReference.ImagePassword = null;
      this.SpeedDigitalReference.Inemergency = false;
      this.SpeedDigitalReference.InLocal = false;
      this.SpeedDigitalReference.IsButton = true;
      this.SpeedDigitalReference.IsForceToInvisible = false;
      this.SpeedDigitalReference.Location = new System.Drawing.Point(1056, 403);
      this.SpeedDigitalReference.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.SpeedDigitalReference.Name = "SpeedDigitalReference";
      this.SpeedDigitalReference.OnLine = false;
      this.SpeedDigitalReference.ShowImage = false;
      this.SpeedDigitalReference.ShowText = true;
      this.SpeedDigitalReference.Size = new System.Drawing.Size(120, 90);
      this.SpeedDigitalReference.Switching = false;
      this.SpeedDigitalReference.TabIndex = 197;
      this.SpeedDigitalReference.Text = "Enable Digital Reference";
      this.SpeedDigitalReference.TranslateText = true;
      this.SpeedDigitalReference.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SpeedDigitalReference.UserDefinedActiveImage")));
      this.SpeedDigitalReference.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SpeedDigitalReference.UserDefinedImage")));
      this.SpeedDigitalReference.Warned = false;
      this.SpeedDigitalReference.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // InternalFlap
      // 
      this.InternalFlap.Active = false;
      this.InternalFlap.Alarmed = false;
      this.InternalFlap.Auto = false;
      this.InternalFlap.BackColor = System.Drawing.Color.WhiteSmoke;
      this.InternalFlap.Cursor = System.Windows.Forms.Cursors.Hand;
      this.InternalFlap.FillBackColorWithImage = false;
      this.InternalFlap.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.InternalFlap.ForeColor = System.Drawing.Color.Black;
      this.InternalFlap.FramePresent = true;
      this.InternalFlap.HighLightEnable = false;
      this.InternalFlap.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.InternalFlap.ImagePassword = null;
      this.InternalFlap.Inemergency = false;
      this.InternalFlap.InLocal = false;
      this.InternalFlap.IsButton = true;
      this.InternalFlap.IsForceToInvisible = false;
      this.InternalFlap.Location = new System.Drawing.Point(877, 613);
      this.InternalFlap.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.InternalFlap.Name = "InternalFlap";
      this.InternalFlap.OnLine = false;
      this.InternalFlap.ShowImage = false;
      this.InternalFlap.ShowText = true;
      this.InternalFlap.Size = new System.Drawing.Size(120, 90);
      this.InternalFlap.Switching = false;
      this.InternalFlap.TabIndex = 196;
      this.InternalFlap.Text = "Enable Internal Flap";
      this.InternalFlap.TranslateText = true;
      this.InternalFlap.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("InternalFlap.UserDefinedActiveImage")));
      this.InternalFlap.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("InternalFlap.UserDefinedImage")));
      this.InternalFlap.Visible = false;
      this.InternalFlap.Warned = false;
      this.InternalFlap.Click += new System.EventHandler(this.OnStateButtonPushDuration);
      // 
      // nud_OffsetSlit2
      // 
      this.nud_OffsetSlit2.DecimalPlaces = 0;
      this.nud_OffsetSlit2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_OffsetSlit2.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_OffsetSlit2.ImagePassword = null;
      this.nud_OffsetSlit2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_OffsetSlit2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetSlit2.Location = new System.Drawing.Point(58, 553);
      this.nud_OffsetSlit2.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.nud_OffsetSlit2.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
      this.nud_OffsetSlit2.Name = "nud_OffsetSlit2";
      this.nud_OffsetSlit2.ReadOnly = true;
      this.nud_OffsetSlit2.Size = new System.Drawing.Size(140, 53);
      this.nud_OffsetSlit2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_OffsetSlit2.TabIndex = 2;
      this.nud_OffsetSlit2.Text = "Slit Shaft 2";
      this.nud_OffsetSlit2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_OffsetSlit2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetSlit2.TranslateText = true;
      this.nud_OffsetSlit2.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetSlit2.Value = "";
      this.nud_OffsetSlit2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetSlit2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_OffsetSlit2.Visible = false;
      this.nud_OffsetSlit2.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // nud_OffsetScore1
      // 
      this.nud_OffsetScore1.DecimalPlaces = 0;
      this.nud_OffsetScore1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_OffsetScore1.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_OffsetScore1.ImagePassword = null;
      this.nud_OffsetScore1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_OffsetScore1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetScore1.Location = new System.Drawing.Point(58, 238);
      this.nud_OffsetScore1.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.nud_OffsetScore1.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
      this.nud_OffsetScore1.Name = "nud_OffsetScore1";
      this.nud_OffsetScore1.ReadOnly = true;
      this.nud_OffsetScore1.Size = new System.Drawing.Size(140, 53);
      this.nud_OffsetScore1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_OffsetScore1.TabIndex = 3;
      this.nud_OffsetScore1.Text = "Score Shaft 1";
      this.nud_OffsetScore1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_OffsetScore1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetScore1.TranslateText = true;
      this.nud_OffsetScore1.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetScore1.Value = "";
      this.nud_OffsetScore1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetScore1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_OffsetScore1.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // nud_OffsetScore2
      // 
      this.nud_OffsetScore2.DecimalPlaces = 0;
      this.nud_OffsetScore2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_OffsetScore2.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_OffsetScore2.ImagePassword = null;
      this.nud_OffsetScore2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_OffsetScore2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetScore2.Location = new System.Drawing.Point(58, 319);
      this.nud_OffsetScore2.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.nud_OffsetScore2.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
      this.nud_OffsetScore2.Name = "nud_OffsetScore2";
      this.nud_OffsetScore2.ReadOnly = true;
      this.nud_OffsetScore2.Size = new System.Drawing.Size(140, 53);
      this.nud_OffsetScore2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_OffsetScore2.TabIndex = 4;
      this.nud_OffsetScore2.Text = "Score Shaft 2";
      this.nud_OffsetScore2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_OffsetScore2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetScore2.TranslateText = true;
      this.nud_OffsetScore2.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetScore2.Value = "";
      this.nud_OffsetScore2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetScore2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_OffsetScore2.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // nud_OffsetScore3
      // 
      this.nud_OffsetScore3.DecimalPlaces = 0;
      this.nud_OffsetScore3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_OffsetScore3.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_OffsetScore3.ImagePassword = null;
      this.nud_OffsetScore3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_OffsetScore3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetScore3.Location = new System.Drawing.Point(58, 398);
      this.nud_OffsetScore3.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.nud_OffsetScore3.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
      this.nud_OffsetScore3.Name = "nud_OffsetScore3";
      this.nud_OffsetScore3.ReadOnly = true;
      this.nud_OffsetScore3.Size = new System.Drawing.Size(140, 53);
      this.nud_OffsetScore3.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_OffsetScore3.TabIndex = 5;
      this.nud_OffsetScore3.Text = "Score Shaft Aux";
      this.nud_OffsetScore3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_OffsetScore3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetScore3.TranslateText = true;
      this.nud_OffsetScore3.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetScore3.Value = "";
      this.nud_OffsetScore3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetScore3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_OffsetScore3.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // nud_OffsetSlit1
      // 
      this.nud_OffsetSlit1.DecimalPlaces = 0;
      this.nud_OffsetSlit1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_OffsetSlit1.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_OffsetSlit1.ImagePassword = null;
      this.nud_OffsetSlit1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_OffsetSlit1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetSlit1.Location = new System.Drawing.Point(58, 477);
      this.nud_OffsetSlit1.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.nud_OffsetSlit1.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
      this.nud_OffsetSlit1.Name = "nud_OffsetSlit1";
      this.nud_OffsetSlit1.ReadOnly = true;
      this.nud_OffsetSlit1.Size = new System.Drawing.Size(140, 53);
      this.nud_OffsetSlit1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_OffsetSlit1.TabIndex = 0;
      this.nud_OffsetSlit1.Text = "Slit Shaft 1";
      this.nud_OffsetSlit1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_OffsetSlit1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetSlit1.TranslateText = true;
      this.nud_OffsetSlit1.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetSlit1.Value = "";
      this.nud_OffsetSlit1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetSlit1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_OffsetSlit1.Visible = false;
      this.nud_OffsetSlit1.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // lbl_PositionOffsets_YN
      // 
      this.lbl_PositionOffsets_YN.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_PositionOffsets_YN.ForeColor = System.Drawing.Color.DarkGreen;
      this.lbl_PositionOffsets_YN.Location = new System.Drawing.Point(127, 193);
      this.lbl_PositionOffsets_YN.Name = "lbl_PositionOffsets_YN";
      this.lbl_PositionOffsets_YN.Size = new System.Drawing.Size(213, 25);
      this.lbl_PositionOffsets_YN.TabIndex = 204;
      this.lbl_PositionOffsets_YN.Text = "Tools Positions Offsets";
      this.lbl_PositionOffsets_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // nud_OffsetSlitsScores
      // 
      this.nud_OffsetSlitsScores.DecimalPlaces = 0;
      this.nud_OffsetSlitsScores.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_OffsetSlitsScores.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_OffsetSlitsScores.ImagePassword = null;
      this.nud_OffsetSlitsScores.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_OffsetSlitsScores.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetSlitsScores.Location = new System.Drawing.Point(260, 238);
      this.nud_OffsetSlitsScores.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.nud_OffsetSlitsScores.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
      this.nud_OffsetSlitsScores.Name = "nud_OffsetSlitsScores";
      this.nud_OffsetSlitsScores.ReadOnly = true;
      this.nud_OffsetSlitsScores.Size = new System.Drawing.Size(140, 53);
      this.nud_OffsetSlitsScores.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_OffsetSlitsScores.TabIndex = 6;
      this.nud_OffsetSlitsScores.Text = "All Scores Shafts";
      this.nud_OffsetSlitsScores.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_OffsetSlitsScores.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetSlitsScores.TranslateText = true;
      this.nud_OffsetSlitsScores.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetSlitsScores.Value = "";
      this.nud_OffsetSlitsScores.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetSlitsScores.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_OffsetSlitsScores.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // lbl_ScoreGapOffsets_YN
      // 
      this.lbl_ScoreGapOffsets_YN.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ScoreGapOffsets_YN.ForeColor = System.Drawing.Color.DarkGreen;
      this.lbl_ScoreGapOffsets_YN.Location = new System.Drawing.Point(534, 193);
      this.lbl_ScoreGapOffsets_YN.Name = "lbl_ScoreGapOffsets_YN";
      this.lbl_ScoreGapOffsets_YN.Size = new System.Drawing.Size(156, 25);
      this.lbl_ScoreGapOffsets_YN.TabIndex = 206;
      this.lbl_ScoreGapOffsets_YN.Text = "Score Gap Offsets";
      this.lbl_ScoreGapOffsets_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // nud_GapOffsetScore3
      // 
      this.nud_GapOffsetScore3.DecimalPlaces = 0;
      this.nud_GapOffsetScore3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_GapOffsetScore3.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_GapOffsetScore3.ImagePassword = null;
      this.nud_GapOffsetScore3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_GapOffsetScore3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_GapOffsetScore3.Location = new System.Drawing.Point(540, 394);
      this.nud_GapOffsetScore3.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.nud_GapOffsetScore3.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
      this.nud_GapOffsetScore3.Name = "nud_GapOffsetScore3";
      this.nud_GapOffsetScore3.ReadOnly = true;
      this.nud_GapOffsetScore3.Size = new System.Drawing.Size(140, 53);
      this.nud_GapOffsetScore3.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_GapOffsetScore3.TabIndex = 9;
      this.nud_GapOffsetScore3.Text = "Score Shaft Aux";
      this.nud_GapOffsetScore3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_GapOffsetScore3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_GapOffsetScore3.TranslateText = true;
      this.nud_GapOffsetScore3.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_GapOffsetScore3.Value = "";
      this.nud_GapOffsetScore3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_GapOffsetScore3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_GapOffsetScore3.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // nud_GapOffsetScore2
      // 
      this.nud_GapOffsetScore2.DecimalPlaces = 0;
      this.nud_GapOffsetScore2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_GapOffsetScore2.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_GapOffsetScore2.ImagePassword = null;
      this.nud_GapOffsetScore2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_GapOffsetScore2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_GapOffsetScore2.Location = new System.Drawing.Point(540, 314);
      this.nud_GapOffsetScore2.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.nud_GapOffsetScore2.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
      this.nud_GapOffsetScore2.Name = "nud_GapOffsetScore2";
      this.nud_GapOffsetScore2.ReadOnly = true;
      this.nud_GapOffsetScore2.Size = new System.Drawing.Size(140, 53);
      this.nud_GapOffsetScore2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_GapOffsetScore2.TabIndex = 8;
      this.nud_GapOffsetScore2.Text = "Score Shaft 2";
      this.nud_GapOffsetScore2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_GapOffsetScore2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_GapOffsetScore2.TranslateText = true;
      this.nud_GapOffsetScore2.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_GapOffsetScore2.Value = "";
      this.nud_GapOffsetScore2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_GapOffsetScore2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_GapOffsetScore2.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // nud_GapOffsetScore1
      // 
      this.nud_GapOffsetScore1.DecimalPlaces = 0;
      this.nud_GapOffsetScore1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_GapOffsetScore1.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_GapOffsetScore1.ImagePassword = null;
      this.nud_GapOffsetScore1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_GapOffsetScore1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_GapOffsetScore1.Location = new System.Drawing.Point(540, 238);
      this.nud_GapOffsetScore1.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.nud_GapOffsetScore1.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
      this.nud_GapOffsetScore1.Name = "nud_GapOffsetScore1";
      this.nud_GapOffsetScore1.ReadOnly = true;
      this.nud_GapOffsetScore1.Size = new System.Drawing.Size(140, 53);
      this.nud_GapOffsetScore1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_GapOffsetScore1.TabIndex = 7;
      this.nud_GapOffsetScore1.Text = "Score Shaft 1";
      this.nud_GapOffsetScore1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_GapOffsetScore1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_GapOffsetScore1.TranslateText = true;
      this.nud_GapOffsetScore1.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_GapOffsetScore1.Value = "";
      this.nud_GapOffsetScore1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_GapOffsetScore1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_GapOffsetScore1.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // lbl_NonScoringScores_YN
      // 
      this.lbl_NonScoringScores_YN.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_NonScoringScores_YN.ForeColor = System.Drawing.Color.DarkGreen;
      this.lbl_NonScoringScores_YN.Location = new System.Drawing.Point(800, 193);
      this.lbl_NonScoringScores_YN.Name = "lbl_NonScoringScores_YN";
      this.lbl_NonScoringScores_YN.Size = new System.Drawing.Size(167, 25);
      this.lbl_NonScoringScores_YN.TabIndex = 210;
      this.lbl_NonScoringScores_YN.Text = "Non-scoring Scores";
      this.lbl_NonScoringScores_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ttl_NonScoringType
      // 
      this.ttl_NonScoringType.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_NonScoringType.CanCancel = false;
      this.ttl_NonScoringType.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NonScoringType.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.ttl_NonScoringType.ImagePassword = null;
      this.ttl_NonScoringType.Location = new System.Drawing.Point(800, 238);
      this.ttl_NonScoringType.Name = "ttl_NonScoringType";
      this.ttl_NonScoringType.ReadOnly = true;
      this.ttl_NonScoringType.SelectedIndex = -1;
      this.ttl_NonScoringType.SelectedItem = "";
      this.ttl_NonScoringType.SelectedItemData = "";
      this.ttl_NonScoringType.Size = new System.Drawing.Size(156, 53);
      this.ttl_NonScoringType.Sorted = false;
      this.ttl_NonScoringType.Style = SyncroC.Controls.TitledListStyles.Normal;
      this.ttl_NonScoringType.TabIndex = 10;
      this.ttl_NonScoringType.Text = "Non-scoring Type";
      this.ttl_NonScoringType.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.ttl_NonScoringType.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NonScoringType.TranslateText = true;
      this.ttl_NonScoringType.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NonScoringType.SelectedItemChanged += new System.EventHandler(this.ttl_NonScoringType_SelectedItemChanged);
      // 
      // nud_TrimScoresDistance
      // 
      this.nud_TrimScoresDistance.DecimalPlaces = 0;
      this.nud_TrimScoresDistance.Enabled = false;
      this.nud_TrimScoresDistance.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_TrimScoresDistance.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_TrimScoresDistance.ImagePassword = null;
      this.nud_TrimScoresDistance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_TrimScoresDistance.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_TrimScoresDistance.Location = new System.Drawing.Point(800, 314);
      this.nud_TrimScoresDistance.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.nud_TrimScoresDistance.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_TrimScoresDistance.Name = "nud_TrimScoresDistance";
      this.nud_TrimScoresDistance.ReadOnly = true;
      this.nud_TrimScoresDistance.Size = new System.Drawing.Size(156, 53);
      this.nud_TrimScoresDistance.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_TrimScoresDistance.TabIndex = 11;
      this.nud_TrimScoresDistance.Text = "Trim Scores Distance";
      this.nud_TrimScoresDistance.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_TrimScoresDistance.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_TrimScoresDistance.TranslateText = true;
      this.nud_TrimScoresDistance.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_TrimScoresDistance.Value = "";
      this.nud_TrimScoresDistance.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_TrimScoresDistance.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_TrimScoresDistance.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // nud_MinTrim
      // 
      this.nud_MinTrim.DecimalPlaces = 0;
      this.nud_MinTrim.Enabled = false;
      this.nud_MinTrim.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_MinTrim.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_MinTrim.ImagePassword = null;
      this.nud_MinTrim.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_MinTrim.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_MinTrim.Location = new System.Drawing.Point(800, 394);
      this.nud_MinTrim.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.nud_MinTrim.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_MinTrim.Name = "nud_MinTrim";
      this.nud_MinTrim.ReadOnly = true;
      this.nud_MinTrim.Size = new System.Drawing.Size(156, 53);
      this.nud_MinTrim.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_MinTrim.TabIndex = 12;
      this.nud_MinTrim.Text = "Minimum Trim";
      this.nud_MinTrim.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_MinTrim.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_MinTrim.TranslateText = true;
      this.nud_MinTrim.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_MinTrim.Value = "";
      this.nud_MinTrim.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_MinTrim.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_MinTrim.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // nud_LoadTeflonMeters
      // 
      this.nud_LoadTeflonMeters.DecimalPlaces = 0;
      this.nud_LoadTeflonMeters.Enabled = false;
      this.nud_LoadTeflonMeters.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_LoadTeflonMeters.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_LoadTeflonMeters.ImagePassword = null;
      this.nud_LoadTeflonMeters.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_LoadTeflonMeters.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_LoadTeflonMeters.Location = new System.Drawing.Point(518, 553);
      this.nud_LoadTeflonMeters.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
      this.nud_LoadTeflonMeters.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.nud_LoadTeflonMeters.Name = "nud_LoadTeflonMeters";
      this.nud_LoadTeflonMeters.ReadOnly = true;
      this.nud_LoadTeflonMeters.Size = new System.Drawing.Size(188, 53);
      this.nud_LoadTeflonMeters.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_LoadTeflonMeters.TabIndex = 211;
      this.nud_LoadTeflonMeters.Text = "Teflon Loading Frequency";
      this.nud_LoadTeflonMeters.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_LoadTeflonMeters.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_LoadTeflonMeters.TranslateText = true;
      this.nud_LoadTeflonMeters.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_LoadTeflonMeters.Value = "";
      this.nud_LoadTeflonMeters.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_LoadTeflonMeters.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_LoadTeflonMeters.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // lbl_teflon_YN
      // 
      this.lbl_teflon_YN.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_teflon_YN.ForeColor = System.Drawing.Color.DarkGreen;
      this.lbl_teflon_YN.Location = new System.Drawing.Point(528, 519);
      this.lbl_teflon_YN.Name = "lbl_teflon_YN";
      this.lbl_teflon_YN.Size = new System.Drawing.Size(167, 25);
      this.lbl_teflon_YN.TabIndex = 212;
      this.lbl_teflon_YN.Text = "Teflon";
      this.lbl_teflon_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // nud_OffsetWebDirector
      // 
      this.nud_OffsetWebDirector.DecimalPlaces = 0;
      this.nud_OffsetWebDirector.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_OffsetWebDirector.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_OffsetWebDirector.ImagePassword = null;
      this.nud_OffsetWebDirector.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_OffsetWebDirector.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetWebDirector.Location = new System.Drawing.Point(260, 314);
      this.nud_OffsetWebDirector.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.nud_OffsetWebDirector.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
      this.nud_OffsetWebDirector.Name = "nud_OffsetWebDirector";
      this.nud_OffsetWebDirector.ReadOnly = true;
      this.nud_OffsetWebDirector.Size = new System.Drawing.Size(140, 53);
      this.nud_OffsetWebDirector.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_OffsetWebDirector.TabIndex = 213;
      this.nud_OffsetWebDirector.Text = "Web Director";
      this.nud_OffsetWebDirector.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_OffsetWebDirector.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetWebDirector.TranslateText = true;
      this.nud_OffsetWebDirector.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetWebDirector.Value = "";
      this.nud_OffsetWebDirector.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetWebDirector.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_OffsetWebDirector.ValueChanged += new System.EventHandler(this.nud_WedDirector_ValueChanged);
      // 
      // nud_OffsetRotary
      // 
      this.nud_OffsetRotary.DecimalPlaces = 0;
      this.nud_OffsetRotary.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_OffsetRotary.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_OffsetRotary.ImagePassword = null;
      this.nud_OffsetRotary.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_OffsetRotary.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetRotary.Location = new System.Drawing.Point(260, 394);
      this.nud_OffsetRotary.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.nud_OffsetRotary.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
      this.nud_OffsetRotary.Name = "nud_OffsetRotary";
      this.nud_OffsetRotary.ReadOnly = true;
      this.nud_OffsetRotary.Size = new System.Drawing.Size(140, 53);
      this.nud_OffsetRotary.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_OffsetRotary.TabIndex = 214;
      this.nud_OffsetRotary.Text = "Rotary Shear";
      this.nud_OffsetRotary.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_OffsetRotary.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetRotary.TranslateText = true;
      this.nud_OffsetRotary.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetRotary.Value = "";
      this.nud_OffsetRotary.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetRotary.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_OffsetRotary.ValueChanged += new System.EventHandler(this.nud_Rotary_ValueChanged);
      // 
      // SlowPositioning
      // 
      this.SlowPositioning.Active = false;
      this.SlowPositioning.Alarmed = false;
      this.SlowPositioning.Auto = false;
      this.SlowPositioning.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SlowPositioning.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SlowPositioning.FillBackColorWithImage = false;
      this.SlowPositioning.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SlowPositioning.ForeColor = System.Drawing.Color.Black;
      this.SlowPositioning.FramePresent = true;
      this.SlowPositioning.HighLightEnable = false;
      this.SlowPositioning.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SlowPositioning.ImagePassword = null;
      this.SlowPositioning.Inemergency = false;
      this.SlowPositioning.InLocal = false;
      this.SlowPositioning.IsButton = true;
      this.SlowPositioning.IsForceToInvisible = false;
      this.SlowPositioning.Location = new System.Drawing.Point(1056, 193);
      this.SlowPositioning.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.SlowPositioning.Name = "SlowPositioning";
      this.SlowPositioning.OnLine = false;
      this.SlowPositioning.ShowImage = false;
      this.SlowPositioning.ShowText = true;
      this.SlowPositioning.Size = new System.Drawing.Size(120, 90);
      this.SlowPositioning.Switching = false;
      this.SlowPositioning.TabIndex = 216;
      this.SlowPositioning.Text = "Force Slow Positioning";
      this.SlowPositioning.TranslateText = false;
      this.SlowPositioning.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SlowPositioning.UserDefinedActiveImage")));
      this.SlowPositioning.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SlowPositioning.UserDefinedImage")));
      this.SlowPositioning.Warned = false;
      this.SlowPositioning.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // PhotoeyeSlitShaft
      // 
      this.PhotoeyeSlitShaft.Active = false;
      this.PhotoeyeSlitShaft.Alarmed = false;
      this.PhotoeyeSlitShaft.Auto = false;
      this.PhotoeyeSlitShaft.BackColor = System.Drawing.Color.WhiteSmoke;
      this.PhotoeyeSlitShaft.Cursor = System.Windows.Forms.Cursors.Hand;
      this.PhotoeyeSlitShaft.FillBackColorWithImage = false;
      this.PhotoeyeSlitShaft.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.PhotoeyeSlitShaft.ForeColor = System.Drawing.Color.Black;
      this.PhotoeyeSlitShaft.FramePresent = true;
      this.PhotoeyeSlitShaft.HighLightEnable = false;
      this.PhotoeyeSlitShaft.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.PhotoeyeSlitShaft.ImagePassword = null;
      this.PhotoeyeSlitShaft.Inemergency = false;
      this.PhotoeyeSlitShaft.InLocal = false;
      this.PhotoeyeSlitShaft.IsButton = true;
      this.PhotoeyeSlitShaft.IsForceToInvisible = false;
      this.PhotoeyeSlitShaft.Location = new System.Drawing.Point(1056, 298);
      this.PhotoeyeSlitShaft.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.PhotoeyeSlitShaft.Name = "PhotoeyeSlitShaft";
      this.PhotoeyeSlitShaft.OnLine = false;
      this.PhotoeyeSlitShaft.ShowImage = false;
      this.PhotoeyeSlitShaft.ShowText = true;
      this.PhotoeyeSlitShaft.Size = new System.Drawing.Size(120, 90);
      this.PhotoeyeSlitShaft.Switching = false;
      this.PhotoeyeSlitShaft.TabIndex = 215;
      this.PhotoeyeSlitShaft.Text = "Enable Slit Photoeye";
      this.PhotoeyeSlitShaft.TranslateText = true;
      this.PhotoeyeSlitShaft.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("PhotoeyeSlitShaft.UserDefinedActiveImage")));
      this.PhotoeyeSlitShaft.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("PhotoeyeSlitShaft.UserDefinedImage")));
      this.PhotoeyeSlitShaft.Warned = false;
      this.PhotoeyeSlitShaft.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // ChangeDefaultShaft
      // 
      this.ChangeDefaultShaft.Active = false;
      this.ChangeDefaultShaft.Alarmed = false;
      this.ChangeDefaultShaft.Auto = false;
      this.ChangeDefaultShaft.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ChangeDefaultShaft.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ChangeDefaultShaft.FillBackColorWithImage = false;
      this.ChangeDefaultShaft.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ChangeDefaultShaft.ForeColor = System.Drawing.Color.Black;
      this.ChangeDefaultShaft.FramePresent = true;
      this.ChangeDefaultShaft.HighLightEnable = false;
      this.ChangeDefaultShaft.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.ChangeDefaultShaft.ImagePassword = null;
      this.ChangeDefaultShaft.Inemergency = false;
      this.ChangeDefaultShaft.InLocal = false;
      this.ChangeDefaultShaft.IsButton = true;
      this.ChangeDefaultShaft.IsForceToInvisible = false;
      this.ChangeDefaultShaft.Location = new System.Drawing.Point(1056, 508);
      this.ChangeDefaultShaft.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.ChangeDefaultShaft.Name = "ChangeDefaultShaft";
      this.ChangeDefaultShaft.OnLine = false;
      this.ChangeDefaultShaft.ShowImage = false;
      this.ChangeDefaultShaft.ShowText = true;
      this.ChangeDefaultShaft.Size = new System.Drawing.Size(120, 90);
      this.ChangeDefaultShaft.Switching = false;
      this.ChangeDefaultShaft.TabIndex = 217;
      this.ChangeDefaultShaft.Text = "Change Default Shaft ";
      this.ChangeDefaultShaft.TranslateText = false;
      this.ChangeDefaultShaft.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ChangeDefaultShaft.UserDefinedActiveImage")));
      this.ChangeDefaultShaft.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ChangeDefaultShaft.UserDefinedImage")));
      this.ChangeDefaultShaft.Warned = false;
      this.ChangeDefaultShaft.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // AdjustScoreGapOffset
      // 
      this.AdjustScoreGapOffset.Active = false;
      this.AdjustScoreGapOffset.Alarmed = false;
      this.AdjustScoreGapOffset.Auto = false;
      this.AdjustScoreGapOffset.BackColor = System.Drawing.Color.WhiteSmoke;
      this.AdjustScoreGapOffset.Cursor = System.Windows.Forms.Cursors.Hand;
      this.AdjustScoreGapOffset.FillBackColorWithImage = false;
      this.AdjustScoreGapOffset.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.AdjustScoreGapOffset.ForeColor = System.Drawing.Color.Black;
      this.AdjustScoreGapOffset.FramePresent = true;
      this.AdjustScoreGapOffset.HighLightEnable = false;
      this.AdjustScoreGapOffset.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.AdjustScoreGapOffset.ImagePassword = null;
      this.AdjustScoreGapOffset.Inemergency = false;
      this.AdjustScoreGapOffset.InLocal = false;
      this.AdjustScoreGapOffset.IsButton = true;
      this.AdjustScoreGapOffset.IsForceToInvisible = false;
      this.AdjustScoreGapOffset.Location = new System.Drawing.Point(1056, 613);
      this.AdjustScoreGapOffset.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.AdjustScoreGapOffset.Name = "AdjustScoreGapOffset";
      this.AdjustScoreGapOffset.OnLine = false;
      this.AdjustScoreGapOffset.ShowImage = false;
      this.AdjustScoreGapOffset.ShowText = true;
      this.AdjustScoreGapOffset.Size = new System.Drawing.Size(120, 90);
      this.AdjustScoreGapOffset.Switching = false;
      this.AdjustScoreGapOffset.TabIndex = 218;
      this.AdjustScoreGapOffset.Text = "Adjust Score Gap Offset";
      this.AdjustScoreGapOffset.TranslateText = true;
      this.AdjustScoreGapOffset.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("AdjustScoreGapOffset.UserDefinedActiveImage")));
      this.AdjustScoreGapOffset.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("AdjustScoreGapOffset.UserDefinedImage")));
      this.AdjustScoreGapOffset.Warned = false;
      this.AdjustScoreGapOffset.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // nud_TeflonCycleMeters
      // 
      this.nud_TeflonCycleMeters.DecimalPlaces = 0;
      this.nud_TeflonCycleMeters.Enabled = false;
      this.nud_TeflonCycleMeters.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_TeflonCycleMeters.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_TeflonCycleMeters.ImagePassword = null;
      this.nud_TeflonCycleMeters.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_TeflonCycleMeters.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_TeflonCycleMeters.Location = new System.Drawing.Point(518, 627);
      this.nud_TeflonCycleMeters.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
      this.nud_TeflonCycleMeters.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.nud_TeflonCycleMeters.Name = "nud_TeflonCycleMeters";
      this.nud_TeflonCycleMeters.ReadOnly = true;
      this.nud_TeflonCycleMeters.Size = new System.Drawing.Size(188, 53);
      this.nud_TeflonCycleMeters.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_TeflonCycleMeters.TabIndex = 219;
      this.nud_TeflonCycleMeters.Text = "Teflon Cycle Frequency";
      this.nud_TeflonCycleMeters.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_TeflonCycleMeters.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_TeflonCycleMeters.TranslateText = true;
      this.nud_TeflonCycleMeters.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_TeflonCycleMeters.Value = "";
      this.nud_TeflonCycleMeters.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_TeflonCycleMeters.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_TeflonCycleMeters.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // nud_TrimChutOffset
      // 
      this.nud_TrimChutOffset.DecimalPlaces = 0;
      this.nud_TrimChutOffset.Enabled = false;
      this.nud_TrimChutOffset.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_TrimChutOffset.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_TrimChutOffset.ImagePassword = null;
      this.nud_TrimChutOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_TrimChutOffset.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_TrimChutOffset.Location = new System.Drawing.Point(800, 477);
      this.nud_TrimChutOffset.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.nud_TrimChutOffset.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
      this.nud_TrimChutOffset.Name = "nud_TrimChutOffset";
      this.nud_TrimChutOffset.ReadOnly = true;
      this.nud_TrimChutOffset.Size = new System.Drawing.Size(156, 53);
      this.nud_TrimChutOffset.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_TrimChutOffset.TabIndex = 220;
      this.nud_TrimChutOffset.Text = "Trim Chute Offset";
      this.nud_TrimChutOffset.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.nud_TrimChutOffset.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_TrimChutOffset.TranslateText = true;
      this.nud_TrimChutOffset.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_TrimChutOffset.Value = "";
      this.nud_TrimChutOffset.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_TrimChutOffset.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_TrimChutOffset.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // scrolBarSlitConsumtionOpt
      // 
      this.scrolBarSlitConsumtionOpt.Cursor = System.Windows.Forms.Cursors.Hand;
      this.scrolBarSlitConsumtionOpt.LargeChange = 2;
      this.scrolBarSlitConsumtionOpt.Location = new System.Drawing.Point(209, 656);
      this.scrolBarSlitConsumtionOpt.Maximum = 12;
      this.scrolBarSlitConsumtionOpt.Name = "scrolBarSlitConsumtionOpt";
      this.scrolBarSlitConsumtionOpt.Size = new System.Drawing.Size(140, 17);
      this.scrolBarSlitConsumtionOpt.TabIndex = 221;
      this.scrolBarSlitConsumtionOpt.Visible = false;
      this.scrolBarSlitConsumtionOpt.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // lbl_SlitConsumptionOpt_YN
      // 
      this.lbl_SlitConsumptionOpt_YN.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SlitConsumptionOpt_YN.ForeColor = System.Drawing.Color.DarkGreen;
      this.lbl_SlitConsumptionOpt_YN.Location = new System.Drawing.Point(135, 627);
      this.lbl_SlitConsumptionOpt_YN.Name = "lbl_SlitConsumptionOpt_YN";
      this.lbl_SlitConsumptionOpt_YN.Size = new System.Drawing.Size(278, 25);
      this.lbl_SlitConsumptionOpt_YN.TabIndex = 222;
      this.lbl_SlitConsumptionOpt_YN.Text = "Slit Consumption Optimization Cost";
      this.lbl_SlitConsumptionOpt_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_SlitConsumptionOpt_YN.Visible = false;
      // 
      // lbl_SlitSubstitution_YN
      // 
      this.lbl_SlitSubstitution_YN.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SlitSubstitution_YN.ForeColor = System.Drawing.Color.DarkGreen;
      this.lbl_SlitSubstitution_YN.Location = new System.Drawing.Point(62, 652);
      this.lbl_SlitSubstitution_YN.Name = "lbl_SlitSubstitution_YN";
      this.lbl_SlitSubstitution_YN.Size = new System.Drawing.Size(144, 25);
      this.lbl_SlitSubstitution_YN.TabIndex = 223;
      this.lbl_SlitSubstitution_YN.Text = "Substitution";
      this.lbl_SlitSubstitution_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_SlitSubstitution_YN.Visible = false;
      // 
      // lbl_SlitRegulation_YN
      // 
      this.lbl_SlitRegulation_YN.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SlitRegulation_YN.ForeColor = System.Drawing.Color.DarkGreen;
      this.lbl_SlitRegulation_YN.Location = new System.Drawing.Point(352, 652);
      this.lbl_SlitRegulation_YN.Name = "lbl_SlitRegulation_YN";
      this.lbl_SlitRegulation_YN.Size = new System.Drawing.Size(144, 25);
      this.lbl_SlitRegulation_YN.TabIndex = 224;
      this.lbl_SlitRegulation_YN.Text = "Regulation";
      this.lbl_SlitRegulation_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_SlitRegulation_YN.Visible = false;
      // 
      // Frm_twin400_regulations
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.lbl_SlitRegulation_YN);
      this.Controls.Add(this.lbl_SlitSubstitution_YN);
      this.Controls.Add(this.lbl_SlitConsumptionOpt_YN);
      this.Controls.Add(this.scrolBarSlitConsumtionOpt);
      this.Controls.Add(this.nud_TrimChutOffset);
      this.Controls.Add(this.nud_TeflonCycleMeters);
      this.Controls.Add(this.AdjustScoreGapOffset);
      this.Controls.Add(this.ChangeDefaultShaft);
      this.Controls.Add(this.SlowPositioning);
      this.Controls.Add(this.PhotoeyeSlitShaft);
      this.Controls.Add(this.nud_OffsetRotary);
      this.Controls.Add(this.nud_OffsetWebDirector);
      this.Controls.Add(this.lbl_teflon_YN);
      this.Controls.Add(this.nud_LoadTeflonMeters);
      this.Controls.Add(this.nud_MinTrim);
      this.Controls.Add(this.nud_TrimScoresDistance);
      this.Controls.Add(this.ttl_NonScoringType);
      this.Controls.Add(this.lbl_NonScoringScores_YN);
      this.Controls.Add(this.nud_GapOffsetScore3);
      this.Controls.Add(this.nud_GapOffsetScore2);
      this.Controls.Add(this.nud_GapOffsetScore1);
      this.Controls.Add(this.lbl_ScoreGapOffsets_YN);
      this.Controls.Add(this.nud_OffsetSlitsScores);
      this.Controls.Add(this.lbl_PositionOffsets_YN);
      this.Controls.Add(this.nud_OffsetSlit1);
      this.Controls.Add(this.nud_OffsetScore3);
      this.Controls.Add(this.nud_OffsetScore2);
      this.Controls.Add(this.nud_OffsetScore1);
      this.Controls.Add(this.nud_OffsetSlit2);
      this.Controls.Add(this.SpeedDigitalReference);
      this.Controls.Add(this.InternalFlap);
      this.Name = "Frm_twin400_regulations";
      this.Activated += new System.EventHandler(this.Frm_twin400_regulations_Activated);
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
      this.Controls.SetChildIndex(this.InternalFlap, 0);
      this.Controls.SetChildIndex(this.SpeedDigitalReference, 0);
      this.Controls.SetChildIndex(this.nud_OffsetSlit2, 0);
      this.Controls.SetChildIndex(this.nud_OffsetScore1, 0);
      this.Controls.SetChildIndex(this.nud_OffsetScore2, 0);
      this.Controls.SetChildIndex(this.nud_OffsetScore3, 0);
      this.Controls.SetChildIndex(this.nud_OffsetSlit1, 0);
      this.Controls.SetChildIndex(this.lbl_PositionOffsets_YN, 0);
      this.Controls.SetChildIndex(this.nud_OffsetSlitsScores, 0);
      this.Controls.SetChildIndex(this.lbl_ScoreGapOffsets_YN, 0);
      this.Controls.SetChildIndex(this.nud_GapOffsetScore1, 0);
      this.Controls.SetChildIndex(this.nud_GapOffsetScore2, 0);
      this.Controls.SetChildIndex(this.nud_GapOffsetScore3, 0);
      this.Controls.SetChildIndex(this.lbl_NonScoringScores_YN, 0);
      this.Controls.SetChildIndex(this.ttl_NonScoringType, 0);
      this.Controls.SetChildIndex(this.nud_TrimScoresDistance, 0);
      this.Controls.SetChildIndex(this.nud_MinTrim, 0);
      this.Controls.SetChildIndex(this.nud_LoadTeflonMeters, 0);
      this.Controls.SetChildIndex(this.lbl_teflon_YN, 0);
      this.Controls.SetChildIndex(this.nud_OffsetWebDirector, 0);
      this.Controls.SetChildIndex(this.nud_OffsetRotary, 0);
      this.Controls.SetChildIndex(this.PhotoeyeSlitShaft, 0);
      this.Controls.SetChildIndex(this.SlowPositioning, 0);
      this.Controls.SetChildIndex(this.ChangeDefaultShaft, 0);
      this.Controls.SetChildIndex(this.AdjustScoreGapOffset, 0);
      this.Controls.SetChildIndex(this.nud_TeflonCycleMeters, 0);
      this.Controls.SetChildIndex(this.nud_TrimChutOffset, 0);
      this.Controls.SetChildIndex(this.scrolBarSlitConsumtionOpt, 0);
      this.Controls.SetChildIndex(this.lbl_SlitConsumptionOpt_YN, 0);
      this.Controls.SetChildIndex(this.lbl_SlitSubstitution_YN, 0);
      this.Controls.SetChildIndex(this.lbl_SlitRegulation_YN, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

    }

    void Frm_twin400_regulations_Activated(object sender, System.EventArgs e)
    {
      this.refreshData();
    }

    #endregion

    private SyncroC.Controls.StateButton SpeedDigitalReference;
    private SyncroC.Controls.StateButton InternalFlap;
    private SyncroC.Controls.TitledNumericUpDown nud_OffsetSlit2;
    private SyncroC.Controls.TitledNumericUpDown nud_OffsetScore1;
    private SyncroC.Controls.TitledNumericUpDown nud_OffsetScore2;
    private SyncroC.Controls.TitledNumericUpDown nud_OffsetScore3;
    private SyncroC.Controls.TitledNumericUpDown nud_OffsetSlit1;
    private System.Windows.Forms.Label lbl_PositionOffsets_YN;
    private SyncroC.Controls.TitledNumericUpDown nud_OffsetSlitsScores;
    private System.Windows.Forms.Label lbl_ScoreGapOffsets_YN;
    private SyncroC.Controls.TitledNumericUpDown nud_GapOffsetScore3;
    private SyncroC.Controls.TitledNumericUpDown nud_GapOffsetScore2;
    private SyncroC.Controls.TitledNumericUpDown nud_GapOffsetScore1;
    private System.Windows.Forms.Label lbl_NonScoringScores_YN;
    private SyncroC.Controls.TitledList ttl_NonScoringType;
    private SyncroC.Controls.TitledNumericUpDown nud_TrimScoresDistance;
    private SyncroC.Controls.TitledNumericUpDown nud_MinTrim;
    private SyncroC.Controls.TitledNumericUpDown nud_LoadTeflonMeters;
    private System.Windows.Forms.Label lbl_teflon_YN;
    private SyncroC.Controls.TitledNumericUpDown nud_OffsetWebDirector;
    private SyncroC.Controls.TitledNumericUpDown nud_OffsetRotary;
    private SyncroC.Controls.StateButton SlowPositioning;
    private SyncroC.Controls.StateButton PhotoeyeSlitShaft;
    private SyncroC.Controls.StateButton ChangeDefaultShaft;
    private SyncroC.Controls.StateButton AdjustScoreGapOffset;
    private SyncroC.Controls.TitledNumericUpDown nud_TeflonCycleMeters;
		private SyncroC.Controls.TitledNumericUpDown nud_TrimChutOffset;
		private System.Windows.Forms.HScrollBar scrolBarSlitConsumtionOpt;
		private System.Windows.Forms.Label lbl_SlitConsumptionOpt_YN;
		private System.Windows.Forms.Label lbl_SlitSubstitution_YN;
		private System.Windows.Forms.Label lbl_SlitRegulation_YN;
  }
}
