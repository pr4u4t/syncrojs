//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
//namespace SyncroC.Forms.Pages.Twin400.Production
namespace SyncroC.Forms.Pages.SlitterScorer.Status
{
	partial class Frm_SlitterScorerRegulations
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SlitterScorerRegulations));
      this.nud_OffsetScore1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_OffsetScore2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_OffsetScore3 = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_PositionOffsets = new System.Windows.Forms.Label();
      this.lbl_ScoreGapOffsets = new System.Windows.Forms.Label();
      this.nud_GapOffsetScore3 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_GapOffsetScore2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_GapOffsetScore1 = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_NonScoringScores = new System.Windows.Forms.Label();
      this.ttl_NonScoringType = new SyncroC.Controls.TitledList();
      this.nud_TrimScoresDistance = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_MinTrim = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_OffsetWebDirector = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_TrimChutOffset = new SyncroC.Controls.TitledNumericUpDown();
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
      this.nud_OffsetScore1.Location = new System.Drawing.Point(137, 236);
      this.nud_OffsetScore1.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.nud_OffsetScore1.Minimum = new decimal(new int[] {
            50,
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
      this.nud_OffsetScore1.TranslateText = false;
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
      this.nud_OffsetScore2.Location = new System.Drawing.Point(137, 317);
      this.nud_OffsetScore2.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.nud_OffsetScore2.Minimum = new decimal(new int[] {
            50,
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
      this.nud_OffsetScore2.TranslateText = false;
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
      this.nud_OffsetScore3.Location = new System.Drawing.Point(137, 396);
      this.nud_OffsetScore3.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.nud_OffsetScore3.Minimum = new decimal(new int[] {
            50,
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
      this.nud_OffsetScore3.TranslateText = false;
      this.nud_OffsetScore3.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetScore3.Value = "";
      this.nud_OffsetScore3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetScore3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_OffsetScore3.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // lbl_PositionOffsets
      // 
      this.lbl_PositionOffsets.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_PositionOffsets.ForeColor = System.Drawing.Color.DarkGreen;
      this.lbl_PositionOffsets.Location = new System.Drawing.Point(206, 191);
      this.lbl_PositionOffsets.Name = "lbl_PositionOffsets";
      this.lbl_PositionOffsets.Size = new System.Drawing.Size(213, 25);
      this.lbl_PositionOffsets.TabIndex = 204;
      this.lbl_PositionOffsets.Text = "Tools Positions Offsets";
      this.lbl_PositionOffsets.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_ScoreGapOffsets
      // 
      this.lbl_ScoreGapOffsets.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ScoreGapOffsets.ForeColor = System.Drawing.Color.DarkGreen;
      this.lbl_ScoreGapOffsets.Location = new System.Drawing.Point(720, 189);
      this.lbl_ScoreGapOffsets.Name = "lbl_ScoreGapOffsets";
      this.lbl_ScoreGapOffsets.Size = new System.Drawing.Size(156, 25);
      this.lbl_ScoreGapOffsets.TabIndex = 206;
      this.lbl_ScoreGapOffsets.Text = "Score Gap Offsets";
      this.lbl_ScoreGapOffsets.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_ScoreGapOffsets.Visible = false;
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
      this.nud_GapOffsetScore3.Location = new System.Drawing.Point(726, 390);
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
      this.nud_GapOffsetScore3.TranslateText = false;
      this.nud_GapOffsetScore3.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_GapOffsetScore3.Value = "";
      this.nud_GapOffsetScore3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_GapOffsetScore3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_GapOffsetScore3.Visible = false;
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
      this.nud_GapOffsetScore2.Location = new System.Drawing.Point(726, 310);
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
      this.nud_GapOffsetScore2.TranslateText = false;
      this.nud_GapOffsetScore2.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_GapOffsetScore2.Value = "";
      this.nud_GapOffsetScore2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_GapOffsetScore2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_GapOffsetScore2.Visible = false;
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
      this.nud_GapOffsetScore1.Location = new System.Drawing.Point(723, 234);
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
      this.nud_GapOffsetScore1.TranslateText = false;
      this.nud_GapOffsetScore1.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_GapOffsetScore1.Value = "";
      this.nud_GapOffsetScore1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_GapOffsetScore1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_GapOffsetScore1.Visible = false;
      this.nud_GapOffsetScore1.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // lbl_NonScoringScores
      // 
      this.lbl_NonScoringScores.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_NonScoringScores.ForeColor = System.Drawing.Color.DarkGreen;
      this.lbl_NonScoringScores.Location = new System.Drawing.Point(939, 189);
      this.lbl_NonScoringScores.Name = "lbl_NonScoringScores";
      this.lbl_NonScoringScores.Size = new System.Drawing.Size(167, 25);
      this.lbl_NonScoringScores.TabIndex = 210;
      this.lbl_NonScoringScores.Text = "Non-scoring Scores";
      this.lbl_NonScoringScores.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_NonScoringScores.Visible = false;
      // 
      // ttl_NonScoringType
      // 
      this.ttl_NonScoringType.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_NonScoringType.CanCancel = false;
      this.ttl_NonScoringType.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NonScoringType.FrameColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.ttl_NonScoringType.ImagePassword = null;
      this.ttl_NonScoringType.Location = new System.Drawing.Point(939, 234);
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
      this.ttl_NonScoringType.TranslateText = false;
      this.ttl_NonScoringType.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_NonScoringType.Visible = false;
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
      this.nud_TrimScoresDistance.Location = new System.Drawing.Point(939, 310);
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
      this.nud_TrimScoresDistance.TranslateText = false;
      this.nud_TrimScoresDistance.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_TrimScoresDistance.Value = "";
      this.nud_TrimScoresDistance.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_TrimScoresDistance.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_TrimScoresDistance.Visible = false;
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
      this.nud_MinTrim.Location = new System.Drawing.Point(939, 390);
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
      this.nud_MinTrim.TranslateText = false;
      this.nud_MinTrim.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_MinTrim.Value = "";
      this.nud_MinTrim.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_MinTrim.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_MinTrim.Visible = false;
      this.nud_MinTrim.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
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
      this.nud_OffsetWebDirector.Location = new System.Drawing.Point(329, 236);
      this.nud_OffsetWebDirector.Maximum = new decimal(new int[] {
            1100,
            0,
            0,
            0});
      this.nud_OffsetWebDirector.Minimum = new decimal(new int[] {
            1100,
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
      this.nud_OffsetWebDirector.TranslateText = false;
      this.nud_OffsetWebDirector.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_OffsetWebDirector.Value = "";
      this.nud_OffsetWebDirector.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_OffsetWebDirector.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_OffsetWebDirector.ValueChanged += new System.EventHandler(this.nud_WedDirector_ValueChanged);
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
      this.nud_TrimChutOffset.Location = new System.Drawing.Point(521, 236);
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
      this.nud_TrimChutOffset.TranslateText = false;
      this.nud_TrimChutOffset.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_TrimChutOffset.Value = "";
      this.nud_TrimChutOffset.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_TrimChutOffset.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_TrimChutOffset.Visible = false;
      this.nud_TrimChutOffset.ValueChanged += new System.EventHandler(this.nud_Twin_ValueChanged);
      // 
      // Frm_SlitterScorerRegulations
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.nud_GapOffsetScore2);
      this.Controls.Add(this.nud_GapOffsetScore1);
      this.Controls.Add(this.lbl_ScoreGapOffsets);
      this.Controls.Add(this.nud_GapOffsetScore3);
      this.Controls.Add(this.lbl_NonScoringScores);
      this.Controls.Add(this.ttl_NonScoringType);
      this.Controls.Add(this.nud_MinTrim);
      this.Controls.Add(this.nud_TrimScoresDistance);
      this.Controls.Add(this.nud_TrimChutOffset);
      this.Controls.Add(this.nud_OffsetWebDirector);
      this.Controls.Add(this.nud_OffsetScore1);
      this.Controls.Add(this.nud_OffsetScore2);
      this.Controls.Add(this.nud_OffsetScore3);
      this.Controls.Add(this.lbl_PositionOffsets);
      this.Name = "Frm_SlitterScorerRegulations";
      this.Controls.SetChildIndex(this.lbl_PositionOffsets, 0);
      this.Controls.SetChildIndex(this.nud_OffsetScore3, 0);
      this.Controls.SetChildIndex(this.nud_OffsetScore2, 0);
      this.Controls.SetChildIndex(this.nud_OffsetScore1, 0);
      this.Controls.SetChildIndex(this.nud_OffsetWebDirector, 0);
      this.Controls.SetChildIndex(this.nud_TrimChutOffset, 0);
      this.Controls.SetChildIndex(this.nud_TrimScoresDistance, 0);
      this.Controls.SetChildIndex(this.nud_MinTrim, 0);
      this.Controls.SetChildIndex(this.ttl_NonScoringType, 0);
      this.Controls.SetChildIndex(this.lbl_NonScoringScores, 0);
      this.Controls.SetChildIndex(this.nud_GapOffsetScore3, 0);
      this.Controls.SetChildIndex(this.lbl_ScoreGapOffsets, 0);
      this.Controls.SetChildIndex(this.nud_GapOffsetScore1, 0);
      this.Controls.SetChildIndex(this.nud_GapOffsetScore2, 0);
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
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

		private SyncroC.Controls.TitledNumericUpDown nud_OffsetScore1;
    private SyncroC.Controls.TitledNumericUpDown nud_OffsetScore2;
		private SyncroC.Controls.TitledNumericUpDown nud_OffsetScore3;
		private System.Windows.Forms.Label lbl_PositionOffsets;
    private System.Windows.Forms.Label lbl_ScoreGapOffsets;
    private SyncroC.Controls.TitledNumericUpDown nud_GapOffsetScore3;
    private SyncroC.Controls.TitledNumericUpDown nud_GapOffsetScore2;
    private SyncroC.Controls.TitledNumericUpDown nud_GapOffsetScore1;
    private System.Windows.Forms.Label lbl_NonScoringScores;
    private SyncroC.Controls.TitledList ttl_NonScoringType;
    private SyncroC.Controls.TitledNumericUpDown nud_TrimScoresDistance;
		private SyncroC.Controls.TitledNumericUpDown nud_MinTrim;
		private SyncroC.Controls.TitledNumericUpDown nud_OffsetWebDirector;
		private SyncroC.Controls.TitledNumericUpDown nud_TrimChutOffset;
  }
}
