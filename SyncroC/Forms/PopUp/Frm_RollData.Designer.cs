namespace SyncroC.Forms.PopUp
{
  partial class Frm_RollData
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_RollData));
      this.ttb_RollID = new SyncroC.Controls.TitledTextBox();
      this.btn_OK_YN = new System.Windows.Forms.Button();
      this.pct_Splicer = new System.Windows.Forms.PictureBox();
      this.lbl_SplNumber = new System.Windows.Forms.Label();
      this.pct_Roll1 = new System.Windows.Forms.PictureBox();
      this.pct_Roll2 = new System.Windows.Forms.PictureBox();
      this.pct_Roll3 = new System.Windows.Forms.PictureBox();
      this.pct_Roll4 = new System.Windows.Forms.PictureBox();
      this.pct_RollSelected = new System.Windows.Forms.PictureBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.nud_SuppliedLength = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_SuppliedDiameter = new SyncroC.Controls.TitledNumericUpDown();
      this.ttb_DataToPrint = new SyncroC.Controls.TitledTextBox();
      this.ttb_Description = new SyncroC.Controls.TitledTextBox();
      this.nud_Thickness = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_WeightFactor = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Width = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_CoreDiameter = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_CurrentLength = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_CurrentDiameter = new SyncroC.Controls.TitledNumericUpDown();
      this.ttb_PaperType = new SyncroC.Controls.TitledTextBox();
      this.chk_Modify = new SyncroC.Controls.CheckBox();
      this.btn_Save_YN = new System.Windows.Forms.Button();
      this.btn_Abort_YN = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Splicer)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Roll1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Roll2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Roll3)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Roll4)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_RollSelected)).BeginInit();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "");
      // 
      // ttb_RollID
      // 
      this.ttb_RollID.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_RollID.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_RollID.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_RollID.Location = new System.Drawing.Point(31, 90);
      this.ttb_RollID.MaxChar = 30;
      this.ttb_RollID.Multiline = false;
      this.ttb_RollID.Name = "ttb_RollID";
      this.ttb_RollID.PasswordChar = '\0';
      this.ttb_RollID.ReadOnly = true;
      this.ttb_RollID.Size = new System.Drawing.Size(213, 49);
      this.ttb_RollID.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_RollID.TabIndex = 22;
      this.ttb_RollID.Text = "Roll ID";
      this.ttb_RollID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.ttb_RollID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_RollID.TranslateText = true;
      this.ttb_RollID.Value = " ";
      this.ttb_RollID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // btn_OK_YN
      // 
      this.btn_OK_YN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_OK_YN.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_OK_YN.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btn_OK_YN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_OK_YN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_OK_YN.Location = new System.Drawing.Point(269, 509);
      this.btn_OK_YN.Name = "btn_OK_YN";
      this.btn_OK_YN.Size = new System.Drawing.Size(135, 46);
      this.btn_OK_YN.TabIndex = 7;
      this.btn_OK_YN.Text = "OK";
      this.btn_OK_YN.UseVisualStyleBackColor = false;
      this.btn_OK_YN.Click += new System.EventHandler(this.btn_OK_YN_Click);
      // 
      // pct_Splicer
      // 
      this.pct_Splicer.Image = global::SyncroC.Properties.Resources.link2_1;
      this.pct_Splicer.Location = new System.Drawing.Point(107, 24);
      this.pct_Splicer.Name = "pct_Splicer";
      this.pct_Splicer.Size = new System.Drawing.Size(72, 61);
      this.pct_Splicer.TabIndex = 23;
      this.pct_Splicer.TabStop = false;
      // 
      // lbl_SplNumber
      // 
      this.lbl_SplNumber.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SplNumber.Location = new System.Drawing.Point(185, 13);
      this.lbl_SplNumber.Name = "lbl_SplNumber";
      this.lbl_SplNumber.Size = new System.Drawing.Size(41, 38);
      this.lbl_SplNumber.TabIndex = 24;
      this.lbl_SplNumber.Text = "1";
      this.lbl_SplNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // pct_Roll1
      // 
      this.pct_Roll1.Image = ((System.Drawing.Image)(resources.GetObject("pct_Roll1.Image")));
      this.pct_Roll1.Location = new System.Drawing.Point(185, 54);
      this.pct_Roll1.Name = "pct_Roll1";
      this.pct_Roll1.Size = new System.Drawing.Size(39, 31);
      this.pct_Roll1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Roll1.TabIndex = 25;
      this.pct_Roll1.TabStop = false;
      // 
      // pct_Roll2
      // 
      this.pct_Roll2.Image = ((System.Drawing.Image)(resources.GetObject("pct_Roll2.Image")));
      this.pct_Roll2.Location = new System.Drawing.Point(230, 54);
      this.pct_Roll2.Name = "pct_Roll2";
      this.pct_Roll2.Size = new System.Drawing.Size(39, 31);
      this.pct_Roll2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Roll2.TabIndex = 26;
      this.pct_Roll2.TabStop = false;
      // 
      // pct_Roll3
      // 
      this.pct_Roll3.Image = ((System.Drawing.Image)(resources.GetObject("pct_Roll3.Image")));
      this.pct_Roll3.Location = new System.Drawing.Point(275, 54);
      this.pct_Roll3.Name = "pct_Roll3";
      this.pct_Roll3.Size = new System.Drawing.Size(39, 31);
      this.pct_Roll3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Roll3.TabIndex = 27;
      this.pct_Roll3.TabStop = false;
      // 
      // pct_Roll4
      // 
      this.pct_Roll4.Image = ((System.Drawing.Image)(resources.GetObject("pct_Roll4.Image")));
      this.pct_Roll4.Location = new System.Drawing.Point(320, 54);
      this.pct_Roll4.Name = "pct_Roll4";
      this.pct_Roll4.Size = new System.Drawing.Size(39, 31);
      this.pct_Roll4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Roll4.TabIndex = 28;
      this.pct_Roll4.TabStop = false;
      // 
      // pct_RollSelected
      // 
      this.pct_RollSelected.Image = ((System.Drawing.Image)(resources.GetObject("pct_RollSelected.Image")));
      this.pct_RollSelected.Location = new System.Drawing.Point(442, 24);
      this.pct_RollSelected.Name = "pct_RollSelected";
      this.pct_RollSelected.Size = new System.Drawing.Size(39, 31);
      this.pct_RollSelected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_RollSelected.TabIndex = 29;
      this.pct_RollSelected.TabStop = false;
      this.pct_RollSelected.Visible = false;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.nud_SuppliedLength);
      this.groupBox1.Controls.Add(this.nud_SuppliedDiameter);
      this.groupBox1.Controls.Add(this.ttb_DataToPrint);
      this.groupBox1.Controls.Add(this.ttb_Description);
      this.groupBox1.Controls.Add(this.nud_Thickness);
      this.groupBox1.Controls.Add(this.nud_WeightFactor);
      this.groupBox1.Controls.Add(this.nud_Width);
      this.groupBox1.Controls.Add(this.nud_CoreDiameter);
      this.groupBox1.Controls.Add(this.nud_CurrentLength);
      this.groupBox1.Controls.Add(this.nud_CurrentDiameter);
      this.groupBox1.Controls.Add(this.ttb_PaperType);
      this.groupBox1.Location = new System.Drawing.Point(24, 126);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(457, 379);
      this.groupBox1.TabIndex = 30;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Data";
      // 
      // nud_SuppliedLength
      // 
      this.nud_SuppliedLength.DecimalPlaces = 0;
      this.nud_SuppliedLength.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SuppliedLength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_SuppliedLength.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_SuppliedLength.Location = new System.Drawing.Point(5, 74);
      this.nud_SuppliedLength.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_SuppliedLength.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_SuppliedLength.Name = "nud_SuppliedLength";
      this.nud_SuppliedLength.ReadOnly = true;
      this.nud_SuppliedLength.Size = new System.Drawing.Size(213, 49);
      this.nud_SuppliedLength.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_SuppliedLength.TabIndex = 35;
      this.nud_SuppliedLength.Text = "Supplied Length";
      this.nud_SuppliedLength.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_SuppliedLength.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SuppliedLength.TranslateText = true;
      this.nud_SuppliedLength.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_SuppliedLength.Value = "8000000";
      this.nud_SuppliedLength.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // nud_SuppliedDiameter
      // 
      this.nud_SuppliedDiameter.DecimalPlaces = 0;
      this.nud_SuppliedDiameter.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SuppliedDiameter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_SuppliedDiameter.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_SuppliedDiameter.Location = new System.Drawing.Point(5, 129);
      this.nud_SuppliedDiameter.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.nud_SuppliedDiameter.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_SuppliedDiameter.Name = "nud_SuppliedDiameter";
      this.nud_SuppliedDiameter.ReadOnly = true;
      this.nud_SuppliedDiameter.Size = new System.Drawing.Size(213, 49);
      this.nud_SuppliedDiameter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_SuppliedDiameter.TabIndex = 34;
      this.nud_SuppliedDiameter.Text = "Supplied Diameter";
      this.nud_SuppliedDiameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_SuppliedDiameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SuppliedDiameter.TranslateText = true;
      this.nud_SuppliedDiameter.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_SuppliedDiameter.Value = "20000";
      this.nud_SuppliedDiameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_DataToPrint
      // 
      this.ttb_DataToPrint.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_DataToPrint.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_DataToPrint.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint.Location = new System.Drawing.Point(5, 312);
      this.ttb_DataToPrint.MaxChar = 32767;
      this.ttb_DataToPrint.Multiline = false;
      this.ttb_DataToPrint.Name = "ttb_DataToPrint";
      this.ttb_DataToPrint.PasswordChar = '\0';
      this.ttb_DataToPrint.ReadOnly = true;
      this.ttb_DataToPrint.Size = new System.Drawing.Size(446, 61);
      this.ttb_DataToPrint.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_DataToPrint.TabIndex = 33;
      this.ttb_DataToPrint.Text = "Data To Print";
      this.ttb_DataToPrint.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_DataToPrint.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint.TranslateText = true;
      this.ttb_DataToPrint.Value = "Stampami";
      this.ttb_DataToPrint.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_Description
      // 
      this.ttb_Description.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_Description.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_Description.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Description.Location = new System.Drawing.Point(5, 246);
      this.ttb_Description.MaxChar = 32767;
      this.ttb_Description.Multiline = false;
      this.ttb_Description.Name = "ttb_Description";
      this.ttb_Description.PasswordChar = '\0';
      this.ttb_Description.ReadOnly = true;
      this.ttb_Description.Size = new System.Drawing.Size(446, 61);
      this.ttb_Description.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_Description.TabIndex = 32;
      this.ttb_Description.Text = "Description";
      this.ttb_Description.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_Description.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Description.TranslateText = true;
      this.ttb_Description.Value = "This is a big roll";
      this.ttb_Description.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // nud_Thickness
      // 
      this.nud_Thickness.DecimalPlaces = 0;
      this.nud_Thickness.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Thickness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Thickness.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMicron;
      this.nud_Thickness.Location = new System.Drawing.Point(161, 186);
      this.nud_Thickness.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.nud_Thickness.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Thickness.Name = "nud_Thickness";
      this.nud_Thickness.ReadOnly = true;
      this.nud_Thickness.Size = new System.Drawing.Size(130, 58);
      this.nud_Thickness.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Thickness.TabIndex = 31;
      this.nud_Thickness.Text = "Thickness";
      this.nud_Thickness.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Thickness.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Thickness.TranslateText = true;
      this.nud_Thickness.UnitMeasure = SyncroC.Utility.UnitMeasure.Micron;
      this.nud_Thickness.Value = "333";
      this.nud_Thickness.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // nud_WeightFactor
      // 
      this.nud_WeightFactor.DecimalPlaces = 0;
      this.nud_WeightFactor.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_WeightFactor.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_WeightFactor.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.nud_WeightFactor.Location = new System.Drawing.Point(321, 186);
      this.nud_WeightFactor.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.nud_WeightFactor.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_WeightFactor.Name = "nud_WeightFactor";
      this.nud_WeightFactor.ReadOnly = true;
      this.nud_WeightFactor.Size = new System.Drawing.Size(130, 58);
      this.nud_WeightFactor.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_WeightFactor.TabIndex = 30;
      this.nud_WeightFactor.Text = "Weight Factor";
      this.nud_WeightFactor.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_WeightFactor.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_WeightFactor.TranslateText = true;
      this.nud_WeightFactor.UnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.nud_WeightFactor.Value = "120";
      this.nud_WeightFactor.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // nud_Width
      // 
      this.nud_Width.DecimalPlaces = 0;
      this.nud_Width.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Width.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Width.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_Width.Location = new System.Drawing.Point(7, 186);
      this.nud_Width.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.nud_Width.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Width.Name = "nud_Width";
      this.nud_Width.ReadOnly = true;
      this.nud_Width.Size = new System.Drawing.Size(130, 58);
      this.nud_Width.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Width.TabIndex = 29;
      this.nud_Width.Text = "Width";
      this.nud_Width.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Width.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Width.TranslateText = true;
      this.nud_Width.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_Width.Value = "24600";
      this.nud_Width.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // nud_CoreDiameter
      // 
      this.nud_CoreDiameter.DecimalPlaces = 0;
      this.nud_CoreDiameter.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CoreDiameter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_CoreDiameter.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_CoreDiameter.Location = new System.Drawing.Point(237, 129);
      this.nud_CoreDiameter.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.nud_CoreDiameter.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_CoreDiameter.Name = "nud_CoreDiameter";
      this.nud_CoreDiameter.ReadOnly = true;
      this.nud_CoreDiameter.Size = new System.Drawing.Size(214, 49);
      this.nud_CoreDiameter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_CoreDiameter.TabIndex = 28;
      this.nud_CoreDiameter.Text = "Core Diameter";
      this.nud_CoreDiameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_CoreDiameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CoreDiameter.TranslateText = true;
      this.nud_CoreDiameter.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_CoreDiameter.Value = "1000";
      this.nud_CoreDiameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // nud_CurrentLength
      // 
      this.nud_CurrentLength.DecimalPlaces = 0;
      this.nud_CurrentLength.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CurrentLength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_CurrentLength.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_CurrentLength.Location = new System.Drawing.Point(237, 19);
      this.nud_CurrentLength.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_CurrentLength.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_CurrentLength.Name = "nud_CurrentLength";
      this.nud_CurrentLength.ReadOnly = true;
      this.nud_CurrentLength.Size = new System.Drawing.Size(214, 49);
      this.nud_CurrentLength.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_CurrentLength.TabIndex = 26;
      this.nud_CurrentLength.Text = "Current Length";
      this.nud_CurrentLength.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_CurrentLength.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CurrentLength.TranslateText = true;
      this.nud_CurrentLength.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_CurrentLength.Value = "8000000";
      this.nud_CurrentLength.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // nud_CurrentDiameter
      // 
      this.nud_CurrentDiameter.DecimalPlaces = 0;
      this.nud_CurrentDiameter.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CurrentDiameter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_CurrentDiameter.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_CurrentDiameter.Location = new System.Drawing.Point(237, 74);
      this.nud_CurrentDiameter.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.nud_CurrentDiameter.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_CurrentDiameter.Name = "nud_CurrentDiameter";
      this.nud_CurrentDiameter.ReadOnly = true;
      this.nud_CurrentDiameter.Size = new System.Drawing.Size(214, 49);
      this.nud_CurrentDiameter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_CurrentDiameter.TabIndex = 24;
      this.nud_CurrentDiameter.Text = "Current Diameter";
      this.nud_CurrentDiameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_CurrentDiameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CurrentDiameter.TranslateText = true;
      this.nud_CurrentDiameter.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_CurrentDiameter.Value = "12330";
      this.nud_CurrentDiameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_PaperType
      // 
      this.ttb_PaperType.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_PaperType.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_PaperType.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_PaperType.Location = new System.Drawing.Point(7, 19);
      this.ttb_PaperType.MaxChar = 32767;
      this.ttb_PaperType.Multiline = false;
      this.ttb_PaperType.Name = "ttb_PaperType";
      this.ttb_PaperType.PasswordChar = '\0';
      this.ttb_PaperType.ReadOnly = true;
      this.ttb_PaperType.Size = new System.Drawing.Size(213, 49);
      this.ttb_PaperType.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_PaperType.TabIndex = 23;
      this.ttb_PaperType.Text = "PaperType";
      this.ttb_PaperType.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_PaperType.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_PaperType.TranslateText = true;
      this.ttb_PaperType.Value = "KB120";
      this.ttb_PaperType.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // chk_Modify
      // 
      this.chk_Modify.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Modify.Checked = false;
      this.chk_Modify.Cursor = System.Windows.Forms.Cursors.Hand;
      this.chk_Modify.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_Modify.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Modify.Location = new System.Drawing.Point(382, 39);
      this.chk_Modify.Name = "chk_Modify";
      this.chk_Modify.ReadOnly = false;
      this.chk_Modify.Size = new System.Drawing.Size(80, 57);
      this.chk_Modify.Style = SyncroC.Controls.CheckBoxStyles.Normal;
      this.chk_Modify.TabIndex = 1;
      this.chk_Modify.Text = "Modify";
      this.chk_Modify.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_Modify.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_Modify.TranslateText = true;
      this.chk_Modify.OnCheckedChanged += new System.EventHandler(this.chk_Modify_OnCheckedChanged);
      // 
      // btn_Save_YN
      // 
      this.btn_Save_YN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_Save_YN.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Save_YN.DialogResult = System.Windows.Forms.DialogResult.Yes;
      this.btn_Save_YN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_Save_YN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_Save_YN.Location = new System.Drawing.Point(346, 509);
      this.btn_Save_YN.Name = "btn_Save_YN";
      this.btn_Save_YN.Size = new System.Drawing.Size(135, 46);
      this.btn_Save_YN.TabIndex = 32;
      this.btn_Save_YN.Text = "Save";
      this.btn_Save_YN.UseVisualStyleBackColor = false;
      this.btn_Save_YN.Visible = false;
      this.btn_Save_YN.Click += new System.EventHandler(this.btn_Save_Click);
      // 
      // btn_Abort_YN
      // 
      this.btn_Abort_YN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_Abort_YN.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Abort_YN.DialogResult = System.Windows.Forms.DialogResult.No;
      this.btn_Abort_YN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_Abort_YN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_Abort_YN.Location = new System.Drawing.Point(191, 509);
      this.btn_Abort_YN.Name = "btn_Abort_YN";
      this.btn_Abort_YN.Size = new System.Drawing.Size(135, 46);
      this.btn_Abort_YN.TabIndex = 33;
      this.btn_Abort_YN.Text = "Abort";
      this.btn_Abort_YN.UseVisualStyleBackColor = false;
      this.btn_Abort_YN.Visible = false;
      this.btn_Abort_YN.Click += new System.EventHandler(this.btn_Abort_Click);
      // 
      // Frm_RollData
      // 
      this.AcceptButton = this.btn_OK_YN;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.LightGray;
      this.CancelButton = this.btn_OK_YN;
      this.ClientSize = new System.Drawing.Size(506, 577);
      this.Controls.Add(this.chk_Modify);
      this.Controls.Add(this.ttb_RollID);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.pct_RollSelected);
      this.Controls.Add(this.pct_Roll4);
      this.Controls.Add(this.pct_Roll3);
      this.Controls.Add(this.pct_Roll2);
      this.Controls.Add(this.pct_Roll1);
      this.Controls.Add(this.lbl_SplNumber);
      this.Controls.Add(this.pct_Splicer);
      this.Controls.Add(this.btn_OK_YN);
      this.Controls.Add(this.btn_Abort_YN);
      this.Controls.Add(this.btn_Save_YN);
      this.DesignGridHorizontalLines = 8;
      this.DesignGridVerticalLines = 1;
      this.Name = "Frm_RollData";
      this.Text = "Frm_RollData";
      ((System.ComponentModel.ISupportInitialize)(this.pct_Splicer)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Roll1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Roll2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Roll3)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Roll4)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_RollSelected)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

		private SyncroC.Controls.TitledTextBox ttb_RollID;
		private System.Windows.Forms.Button btn_OK_YN;
		private System.Windows.Forms.PictureBox pct_Splicer;
		private System.Windows.Forms.Label lbl_SplNumber;
		private System.Windows.Forms.PictureBox pct_Roll1;
		private System.Windows.Forms.PictureBox pct_Roll2;
		private System.Windows.Forms.PictureBox pct_Roll3;
		private System.Windows.Forms.PictureBox pct_Roll4;
		private System.Windows.Forms.PictureBox pct_RollSelected;
		private System.Windows.Forms.GroupBox groupBox1;
		private SyncroC.Controls.TitledTextBox ttb_Description;
		private SyncroC.Controls.TitledNumericUpDown nud_Thickness;
		private SyncroC.Controls.TitledNumericUpDown nud_WeightFactor;
		private SyncroC.Controls.TitledNumericUpDown nud_Width;
		private SyncroC.Controls.TitledNumericUpDown nud_CoreDiameter;
		private SyncroC.Controls.TitledNumericUpDown nud_CurrentLength;
		private SyncroC.Controls.TitledNumericUpDown nud_CurrentDiameter;
		private SyncroC.Controls.TitledTextBox ttb_PaperType;
		private SyncroC.Controls.CheckBox chk_Modify;
		private System.Windows.Forms.Button btn_Save_YN;
		private System.Windows.Forms.Button btn_Abort_YN;
		private SyncroC.Controls.TitledTextBox ttb_DataToPrint;
		private SyncroC.Controls.TitledNumericUpDown nud_SuppliedLength;
		private SyncroC.Controls.TitledNumericUpDown nud_SuppliedDiameter;
  }
}