namespace SyncroC.Forms.PopUp
{
  partial class Frm_PRCAskValue
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PRCAskValue));
      this.btn_Abort_NN = new System.Windows.Forms.Button();
      this.btn_OK = new System.Windows.Forms.Button();
      this.pnl_userCorrection = new System.Windows.Forms.Panel();
      this.DecreaseButtonUserCorrection = new SyncroC.Controls.IncDecButton.IncDecButton();
      this.tnd_ValueUserCorrection = new SyncroC.Controls.TitledNumericUpDown();
      this.IncreaseButtonUserCorrection = new SyncroC.Controls.IncDecButton.IncDecButton();
      this.lbl_Standard = new System.Windows.Forms.Label();
      this.lbl_warp = new System.Windows.Forms.Label();
      this.timer_Calcola = new System.Windows.Forms.Timer(this.components);
      this.tnd_ValueSetpoint = new SyncroC.Controls.TitledNumericUpDown();
      this.timer_Focus = new System.Windows.Forms.Timer(this.components);
      this.pnl_userCorrection.SuspendLayout();
      this.SuspendLayout();
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "");
      // 
      // btn_Abort_NN
      // 
      this.btn_Abort_NN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_Abort_NN.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Abort_NN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btn_Abort_NN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_Abort_NN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_Abort_NN.Location = new System.Drawing.Point(189, 274);
      this.btn_Abort_NN.Name = "btn_Abort_NN";
      this.btn_Abort_NN.Size = new System.Drawing.Size(135, 57);
      this.btn_Abort_NN.TabIndex = 7;
      this.btn_Abort_NN.Text = "ABORT";
      this.btn_Abort_NN.UseVisualStyleBackColor = false;
      this.btn_Abort_NN.Click += new System.EventHandler(this.btn_Abort_Click);
      // 
      // btn_OK
      // 
      this.btn_OK.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_OK.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_OK.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_OK.Location = new System.Drawing.Point(25, 274);
      this.btn_OK.Name = "btn_OK";
      this.btn_OK.Size = new System.Drawing.Size(135, 57);
      this.btn_OK.TabIndex = 6;
      this.btn_OK.Text = "OK";
      this.btn_OK.UseVisualStyleBackColor = false;
      this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
      // 
      // pnl_userCorrection
      // 
      this.pnl_userCorrection.BackColor = System.Drawing.Color.Lime;
      this.pnl_userCorrection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pnl_userCorrection.Controls.Add(this.DecreaseButtonUserCorrection);
      this.pnl_userCorrection.Controls.Add(this.tnd_ValueUserCorrection);
      this.pnl_userCorrection.Controls.Add(this.IncreaseButtonUserCorrection);
      this.pnl_userCorrection.Location = new System.Drawing.Point(18, 106);
      this.pnl_userCorrection.Name = "pnl_userCorrection";
      this.pnl_userCorrection.Size = new System.Drawing.Size(315, 90);
      this.pnl_userCorrection.TabIndex = 10;
      // 
      // DecreaseButtonUserCorrection
      // 
      this.DecreaseButtonUserCorrection.BackColor = System.Drawing.Color.WhiteSmoke;
      this.DecreaseButtonUserCorrection.Cursor = System.Windows.Forms.Cursors.Hand;
      this.DecreaseButtonUserCorrection.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.DecreaseButtonUserCorrection.Image = SyncroC.Controls.IncDecButton.IncDecButtonArrowImage.DownArrow;
      this.DecreaseButtonUserCorrection.Location = new System.Drawing.Point(7, 7);
      this.DecreaseButtonUserCorrection.LowLevelEnabled = true;
      this.DecreaseButtonUserCorrection.Name = "DecreaseButtonUserCorrection";
      this.DecreaseButtonUserCorrection.Size = new System.Drawing.Size(69, 75);
      this.DecreaseButtonUserCorrection.TabIndex = 9;
      this.DecreaseButtonUserCorrection.TimerInterval = 300;
      this.DecreaseButtonUserCorrection.TranslateText = false;
      this.DecreaseButtonUserCorrection.OnIncDecButtonDown += new SyncroC.Controls.IncDecButton.IncDecButtonDownHandler(this.DecreaseButtonUserCorrection_OnIncDecButtonDown);
      // 
      // tnd_ValueUserCorrection
      // 
      this.tnd_ValueUserCorrection.DecimalPlaces = 0;
      this.tnd_ValueUserCorrection.Font = new System.Drawing.Font("Arial Unicode MS", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnd_ValueUserCorrection.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnd_ValueUserCorrection.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnd_ValueUserCorrection.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnd_ValueUserCorrection.Location = new System.Drawing.Point(82, 9);
      this.tnd_ValueUserCorrection.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.tnd_ValueUserCorrection.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
      this.tnd_ValueUserCorrection.Name = "tnd_ValueUserCorrection";
      this.tnd_ValueUserCorrection.ReadOnly = false;
      this.tnd_ValueUserCorrection.Size = new System.Drawing.Size(149, 73);
      this.tnd_ValueUserCorrection.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnd_ValueUserCorrection.TabIndex = 0;
      this.tnd_ValueUserCorrection.Text = "+ / -";
      this.tnd_ValueUserCorrection.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.PRCUserValueColor;
      this.tnd_ValueUserCorrection.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnd_ValueUserCorrection.TranslateText = false;
      this.tnd_ValueUserCorrection.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnd_ValueUserCorrection.UnitMeasureVisible = false;
      this.tnd_ValueUserCorrection.Value = "0";
      this.tnd_ValueUserCorrection.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.PRCUserValueColor;
      this.tnd_ValueUserCorrection.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnd_ValueUserCorrection.ValueChanged += new System.EventHandler(this.tnd_ValueUserCorrection_ValueChanged);
      // 
      // IncreaseButtonUserCorrection
      // 
      this.IncreaseButtonUserCorrection.BackColor = System.Drawing.Color.WhiteSmoke;
      this.IncreaseButtonUserCorrection.Cursor = System.Windows.Forms.Cursors.Hand;
      this.IncreaseButtonUserCorrection.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.IncreaseButtonUserCorrection.Image = SyncroC.Controls.IncDecButton.IncDecButtonArrowImage.UpArrow;
      this.IncreaseButtonUserCorrection.Location = new System.Drawing.Point(237, 7);
      this.IncreaseButtonUserCorrection.LowLevelEnabled = true;
      this.IncreaseButtonUserCorrection.Name = "IncreaseButtonUserCorrection";
      this.IncreaseButtonUserCorrection.Size = new System.Drawing.Size(69, 75);
      this.IncreaseButtonUserCorrection.TabIndex = 8;
      this.IncreaseButtonUserCorrection.TimerInterval = 300;
      this.IncreaseButtonUserCorrection.TranslateText = false;
      this.IncreaseButtonUserCorrection.OnIncDecButtonDown += new SyncroC.Controls.IncDecButton.IncDecButtonDownHandler(this.IncreaseButtonUserCorrection_OnIncDecButtonDown);
      // 
      // lbl_Standard
      // 
      this.lbl_Standard.BackColor = System.Drawing.Color.Blue;
      this.lbl_Standard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_Standard.Font = new System.Drawing.Font("Arial Unicode MS", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Standard.Location = new System.Drawing.Point(18, 24);
      this.lbl_Standard.Name = "lbl_Standard";
      this.lbl_Standard.Size = new System.Drawing.Size(315, 42);
      this.lbl_Standard.TabIndex = 12;
      this.lbl_Standard.Text = "999";
      this.lbl_Standard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_warp
      // 
      this.lbl_warp.BackColor = System.Drawing.Color.Gold;
      this.lbl_warp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_warp.Font = new System.Drawing.Font("Arial Unicode MS", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_warp.Location = new System.Drawing.Point(18, 65);
      this.lbl_warp.Name = "lbl_warp";
      this.lbl_warp.Size = new System.Drawing.Size(315, 42);
      this.lbl_warp.TabIndex = 13;
      this.lbl_warp.Text = "999";
      this.lbl_warp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // timer_Calcola
      // 
      this.timer_Calcola.Enabled = true;
      this.timer_Calcola.Tick += new System.EventHandler(this.timer_Calcola_Tick);
      // 
      // tnd_ValueSetpoint
      // 
      this.tnd_ValueSetpoint.BackColor = System.Drawing.Color.Black;
      this.tnd_ValueSetpoint.DecimalPlaces = 0;
      this.tnd_ValueSetpoint.Font = new System.Drawing.Font("Arial Unicode MS", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnd_ValueSetpoint.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnd_ValueSetpoint.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnd_ValueSetpoint.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnd_ValueSetpoint.Location = new System.Drawing.Point(18, 205);
      this.tnd_ValueSetpoint.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.tnd_ValueSetpoint.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
      this.tnd_ValueSetpoint.Name = "tnd_ValueSetpoint";
      this.tnd_ValueSetpoint.ReadOnly = true;
      this.tnd_ValueSetpoint.Size = new System.Drawing.Size(315, 61);
      this.tnd_ValueSetpoint.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnd_ValueSetpoint.TabIndex = 10;
      this.tnd_ValueSetpoint.TabStop = false;
      this.tnd_ValueSetpoint.Text = "Value";
      this.tnd_ValueSetpoint.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnd_ValueSetpoint.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnd_ValueSetpoint.TranslateText = false;
      this.tnd_ValueSetpoint.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnd_ValueSetpoint.Value = "999";
      this.tnd_ValueSetpoint.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnd_ValueSetpoint.ValueForeColor = System.Drawing.Color.WhiteSmoke;
      // 
      // timer_Focus
      // 
      this.timer_Focus.Enabled = true;
      this.timer_Focus.Tick += new System.EventHandler(this.timer_Focus_Tick);
      // 
      // Frm_PRCAskValue
      // 
      this.AcceptButton = this.btn_OK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.LightGray;
      this.CancelButton = this.btn_Abort_NN;
      this.ClientSize = new System.Drawing.Size(350, 357);
      this.Controls.Add(this.tnd_ValueSetpoint);
      this.Controls.Add(this.lbl_warp);
      this.Controls.Add(this.lbl_Standard);
      this.Controls.Add(this.pnl_userCorrection);
      this.Controls.Add(this.btn_Abort_NN);
      this.Controls.Add(this.btn_OK);
      this.Name = "Frm_PRCAskValue";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Frm_PRCAskValue";
      this.pnl_userCorrection.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btn_Abort_NN;
    private System.Windows.Forms.Button btn_OK;
    private System.Windows.Forms.Panel pnl_userCorrection;
    private SyncroC.Controls.IncDecButton.IncDecButton DecreaseButtonUserCorrection;
    private SyncroC.Controls.TitledNumericUpDown tnd_ValueUserCorrection;
    private SyncroC.Controls.IncDecButton.IncDecButton IncreaseButtonUserCorrection;
    private System.Windows.Forms.Label lbl_Standard;
    private System.Windows.Forms.Label lbl_warp;
    private System.Windows.Forms.Timer timer_Calcola;
    private SyncroC.Controls.TitledNumericUpDown tnd_ValueSetpoint;
    private System.Windows.Forms.Timer timer_Focus;
  }
}