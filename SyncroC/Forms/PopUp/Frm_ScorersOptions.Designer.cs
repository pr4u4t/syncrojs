namespace SyncroC.Forms.PopUp
{
  partial class Frm_ScorersOptions
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ScorersOptions));
      this.btn_Abort_YN = new System.Windows.Forms.Button();
      this.btn_OK = new System.Windows.Forms.Button();
      this.lbl_OrderIndex = new System.Windows.Forms.Label();
      this.nud_Offset = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Measure = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_ScoreGap = new SyncroC.Controls.TitledNumericUpDown();
      this.cmb_Positioning = new SyncroC.Controls.TitledList();
      this.cmb_Type = new SyncroC.Controls.TitledList();
      this.ttl_ScorerOptions = new SyncroC.Controls.Title();
      this.cmb_Shaft = new SyncroC.Controls.TitledList();
      this.grv_ScorerOptions = new SyncroC.Controls.GridView();
      this.SuspendLayout();
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "");
      // 
      // btn_Abort_YN
      // 
      this.btn_Abort_YN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_Abort_YN.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Abort_YN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btn_Abort_YN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_Abort_YN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_Abort_YN.Location = new System.Drawing.Point(215, 281);
      this.btn_Abort_YN.Name = "btn_Abort_YN";
      this.btn_Abort_YN.Size = new System.Drawing.Size(135, 57);
      this.btn_Abort_YN.TabIndex = 3;
      this.btn_Abort_YN.Text = "ABORT";
      this.btn_Abort_YN.UseVisualStyleBackColor = false;
      this.btn_Abort_YN.Click += new System.EventHandler(this.btn_Abort_Click);
      // 
      // btn_OK
      // 
      this.btn_OK.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_OK.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_OK.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_OK.Location = new System.Drawing.Point(51, 281);
      this.btn_OK.Name = "btn_OK";
      this.btn_OK.Size = new System.Drawing.Size(135, 57);
      this.btn_OK.TabIndex = 2;
      this.btn_OK.Text = "OK";
      this.btn_OK.UseVisualStyleBackColor = false;
      this.btn_OK.Click += new System.EventHandler(this.btn_OK_YN_Click);
      // 
      // lbl_OrderIndex
      // 
      this.lbl_OrderIndex.BackColor = System.Drawing.Color.Black;
      this.lbl_OrderIndex.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_OrderIndex.ForeColor = System.Drawing.Color.White;
      this.lbl_OrderIndex.Location = new System.Drawing.Point(345, 24);
      this.lbl_OrderIndex.Name = "lbl_OrderIndex";
      this.lbl_OrderIndex.Size = new System.Drawing.Size(31, 20);
      this.lbl_OrderIndex.TabIndex = 894;
      this.lbl_OrderIndex.Text = "1";
      this.lbl_OrderIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // nud_Offset
      // 
      this.nud_Offset.DecimalPlaces = 0;
      this.nud_Offset.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Offset.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Offset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Offset.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Offset.Location = new System.Drawing.Point(41, 161);
      this.nud_Offset.Margin = new System.Windows.Forms.Padding(2, 0, 1, 1);
      this.nud_Offset.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_Offset.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Offset.Name = "nud_Offset";
      this.nud_Offset.ReadOnly = true;
      this.nud_Offset.Size = new System.Drawing.Size(80, 52);
      this.nud_Offset.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Offset.TabIndex = 891;
      this.nud_Offset.Text = "Offset";
      this.nud_Offset.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Offset.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Offset.TranslateText = true;
      this.nud_Offset.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Offset.Value = "";
      this.nud_Offset.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // nud_Measure
      // 
      this.nud_Measure.DecimalPlaces = 0;
      this.nud_Measure.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Measure.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Measure.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Measure.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_Measure.Location = new System.Drawing.Point(41, 45);
      this.nud_Measure.Margin = new System.Windows.Forms.Padding(3, 0, 1, 1);
      this.nud_Measure.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_Measure.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Measure.Name = "nud_Measure";
      this.nud_Measure.ReadOnly = true;
      this.nud_Measure.Size = new System.Drawing.Size(80, 52);
      this.nud_Measure.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Measure.TabIndex = 887;
      this.nud_Measure.Text = "Measure";
      this.nud_Measure.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Measure.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Measure.TranslateText = true;
      this.nud_Measure.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_Measure.UnitMeasureVisible = false;
      this.nud_Measure.Value = "";
      this.nud_Measure.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // nud_ScoreGap
      // 
      this.nud_ScoreGap.DecimalPlaces = 0;
      this.nud_ScoreGap.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ScoreGap.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGap.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ScoreGap.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGap.Location = new System.Drawing.Point(41, 103);
      this.nud_ScoreGap.Margin = new System.Windows.Forms.Padding(1, 0, 3, 1);
      this.nud_ScoreGap.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_ScoreGap.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ScoreGap.Name = "nud_ScoreGap";
      this.nud_ScoreGap.ReadOnly = true;
      this.nud_ScoreGap.Size = new System.Drawing.Size(80, 52);
      this.nud_ScoreGap.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_ScoreGap.TabIndex = 890;
      this.nud_ScoreGap.Text = "Gap";
      this.nud_ScoreGap.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_ScoreGap.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGap.TranslateText = true;
      this.nud_ScoreGap.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGap.UnitMeasureVisible = false;
      this.nud_ScoreGap.Value = "";
      this.nud_ScoreGap.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // cmb_Positioning
      // 
      this.cmb_Positioning.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Positioning.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Positioning.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Positioning.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Positioning.Location = new System.Drawing.Point(125, 45);
      this.cmb_Positioning.Margin = new System.Windows.Forms.Padding(2, 0, 2, 1);
      this.cmb_Positioning.Name = "cmb_Positioning";
      this.cmb_Positioning.ReadOnly = true;
      this.cmb_Positioning.SelectedIndex = -1;
      this.cmb_Positioning.SelectedItem = "";
      this.cmb_Positioning.SelectedItemData = "";
      this.cmb_Positioning.Size = new System.Drawing.Size(182, 52);
      this.cmb_Positioning.Sorted = false;
      this.cmb_Positioning.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Positioning.TabIndex = 888;
      this.cmb_Positioning.Text = "Positioning";
      this.cmb_Positioning.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Positioning.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Positioning.TranslateText = true;
      this.cmb_Positioning.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Positioning.SelectedItemChanged += new System.EventHandler(this.cmb_Positioning_SelectedItemChanged);
      // 
      // cmb_Type
      // 
      this.cmb_Type.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Type.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Type.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Type.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Type.Location = new System.Drawing.Point(312, 45);
      this.cmb_Type.Margin = new System.Windows.Forms.Padding(2, 1, 1, 1);
      this.cmb_Type.Name = "cmb_Type";
      this.cmb_Type.ReadOnly = true;
      this.cmb_Type.SelectedIndex = -1;
      this.cmb_Type.SelectedItem = "";
      this.cmb_Type.SelectedItemData = "";
      this.cmb_Type.Size = new System.Drawing.Size(62, 52);
      this.cmb_Type.Sorted = false;
      this.cmb_Type.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Type.TabIndex = 889;
      this.cmb_Type.Text = "Type";
      this.cmb_Type.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Type.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Type.TranslateText = true;
      this.cmb_Type.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttl_ScorerOptions
      // 
      this.ttl_ScorerOptions.BackColor = System.Drawing.Color.Black;
      this.ttl_ScorerOptions.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ScorerOptions.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ScorerOptions.ForeColor = System.Drawing.Color.White;
      this.ttl_ScorerOptions.Location = new System.Drawing.Point(24, 24);
      this.ttl_ScorerOptions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
      this.ttl_ScorerOptions.Name = "ttl_ScorerOptions";
      this.ttl_ScorerOptions.Size = new System.Drawing.Size(352, 251);
      this.ttl_ScorerOptions.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_ScorerOptions.TabIndex = 893;
      this.ttl_ScorerOptions.TabStop = false;
      this.ttl_ScorerOptions.Text = "SCORER OPTIONS";
      this.ttl_ScorerOptions.TranslateText = true;
      // 
      // cmb_Shaft
      // 
      this.cmb_Shaft.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Shaft.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Shaft.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Shaft.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Shaft.Location = new System.Drawing.Point(41, 220);
      this.cmb_Shaft.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.cmb_Shaft.Name = "cmb_Shaft";
      this.cmb_Shaft.ReadOnly = true;
      this.cmb_Shaft.SelectedIndex = -1;
      this.cmb_Shaft.SelectedItem = "";
      this.cmb_Shaft.SelectedItemData = "";
      this.cmb_Shaft.Size = new System.Drawing.Size(80, 52);
      this.cmb_Shaft.Sorted = false;
      this.cmb_Shaft.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Shaft.TabIndex = 892;
      this.cmb_Shaft.Text = "Shaft";
      this.cmb_Shaft.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Shaft.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Shaft.TranslateText = true;
      this.cmb_Shaft.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // grv_ScorerOptions
      // 
      this.grv_ScorerOptions.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.grv_ScorerOptions.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.grv_ScorerOptions.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.grv_ScorerOptions.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.grv_ScorerOptions.AutoGenerateColumns = false;
      this.grv_ScorerOptions.BackColor = System.Drawing.Color.LightGray;
      this.grv_ScorerOptions.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
      this.grv_ScorerOptions.DataMember = "";
      this.grv_ScorerOptions.DataSource = null;
      this.grv_ScorerOptions.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grv_ScorerOptions.ForeColor = System.Drawing.Color.Black;
      this.grv_ScorerOptions.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.grv_ScorerOptions.Location = new System.Drawing.Point(124, 99);
      this.grv_ScorerOptions.Margin = new System.Windows.Forms.Padding(0, 1, 2, 3);
      this.grv_ScorerOptions.MultiSelect = false;
      this.grv_ScorerOptions.Name = "grv_ScorerOptions";
      this.grv_ScorerOptions.RowsHeight = 32;
      this.grv_ScorerOptions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.grv_ScorerOptions.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.grv_ScorerOptions.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.grv_ScorerOptions.Size = new System.Drawing.Size(250, 171);
      this.grv_ScorerOptions.Style = SyncroC.Controls.GridStyles.Light;
      this.grv_ScorerOptions.TabIndex = 895;
      this.grv_ScorerOptions.TabStop = false;
      this.grv_ScorerOptions.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.grv_ScorerOptions.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.grv_ScorerOptions.TranslateText = false;
      this.grv_ScorerOptions.SelectionChanged += new System.EventHandler(this.grv_ScorerOptions_SelectionChanged);
      // 
      // Frm_ScorersOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(401, 367);
      this.Controls.Add(this.grv_ScorerOptions);
      this.Controls.Add(this.lbl_OrderIndex);
      this.Controls.Add(this.nud_Offset);
      this.Controls.Add(this.nud_Measure);
      this.Controls.Add(this.nud_ScoreGap);
      this.Controls.Add(this.cmb_Positioning);
      this.Controls.Add(this.cmb_Type);
      this.Controls.Add(this.ttl_ScorerOptions);
      this.Controls.Add(this.cmb_Shaft);
      this.Controls.Add(this.btn_Abort_YN);
      this.Controls.Add(this.btn_OK);
      this.Name = "Frm_ScorersOptions";
      this.Text = "Frm_ScorersOptions";
      this.Activated += new System.EventHandler(this.Frm_ScorersOptions_Activated);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btn_Abort_YN;
    private System.Windows.Forms.Button btn_OK;
    private System.Windows.Forms.Label lbl_OrderIndex;
    private SyncroC.Controls.TitledNumericUpDown nud_Offset;
    private SyncroC.Controls.TitledNumericUpDown nud_Measure;
    private SyncroC.Controls.TitledNumericUpDown nud_ScoreGap;
    private SyncroC.Controls.TitledList cmb_Positioning;
    private SyncroC.Controls.TitledList cmb_Type;
    public SyncroC.Controls.Title ttl_ScorerOptions;
    private SyncroC.Controls.TitledList cmb_Shaft;
    private SyncroC.Controls.GridView grv_ScorerOptions;
  }
}