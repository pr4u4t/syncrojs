namespace SyncroC.Forms.PopUp
{
  partial class Frm_AskValueAndConfirm
  {
    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.btn_OK = new System.Windows.Forms.Button();
      this.btn_Abort_YN = new System.Windows.Forms.Button();
      this.tnd_Value = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_Confirmation_YN = new System.Windows.Forms.Label();
      this.DecreaseButton = new SyncroC.Controls.IncDecButton.IncDecButton();
      this.IncreaseButton = new SyncroC.Controls.IncDecButton.IncDecButton();
      this.SuspendLayout();
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.iml_AccessibilityImages.ImageSize = new System.Drawing.Size(30, 30);
      this.iml_AccessibilityImages.ImageStream = null;
      // 
      // btn_OK
      // 
      this.btn_OK.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_OK.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_OK.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_OK.Location = new System.Drawing.Point(43, 193);
      this.btn_OK.Name = "btn_OK";
      this.btn_OK.Size = new System.Drawing.Size(135, 45);
      this.btn_OK.TabIndex = 1;
      this.btn_OK.Text = "OK";
      this.btn_OK.UseVisualStyleBackColor = false;
      this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
      // 
      // btn_Abort_YN
      // 
      this.btn_Abort_YN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_Abort_YN.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Abort_YN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btn_Abort_YN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_Abort_YN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_Abort_YN.Location = new System.Drawing.Point(200, 193);
      this.btn_Abort_YN.Name = "btn_Abort_YN";
      this.btn_Abort_YN.Size = new System.Drawing.Size(135, 45);
      this.btn_Abort_YN.TabIndex = 2;
      this.btn_Abort_YN.Text = "ABORT";
      this.btn_Abort_YN.UseVisualStyleBackColor = false;
      this.btn_Abort_YN.Click += new System.EventHandler(this.btn_Abort_Click);
      // 
      // tnd_Value
      // 
      this.tnd_Value.DecimalPlaces = 0;
      this.tnd_Value.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnd_Value.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnd_Value.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnd_Value.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnd_Value.Location = new System.Drawing.Point(107, 104);
      this.tnd_Value.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.tnd_Value.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
      this.tnd_Value.Name = "tnd_Value";
      this.tnd_Value.ReadOnly = false;
      this.tnd_Value.Size = new System.Drawing.Size(168, 75);
      this.tnd_Value.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnd_Value.TabIndex = 0;
      this.tnd_Value.Text = "Value";
      this.tnd_Value.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.tnd_Value.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnd_Value.TranslateText = true;
      this.tnd_Value.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnd_Value.Value = "0";
      this.tnd_Value.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // lbl_Confirmation_YN
      // 
      this.lbl_Confirmation_YN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Confirmation_YN.Location = new System.Drawing.Point(26, 25);
      this.lbl_Confirmation_YN.Name = "lbl_Confirmation_YN";
      this.lbl_Confirmation_YN.Size = new System.Drawing.Size(329, 67);
      this.lbl_Confirmation_YN.TabIndex = 7;
      this.lbl_Confirmation_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // DecreaseButton
      // 
      this.DecreaseButton.BackColor = System.Drawing.Color.WhiteSmoke;
      this.DecreaseButton.Cursor = System.Windows.Forms.Cursors.Hand;
      this.DecreaseButton.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.DecreaseButton.Image = SyncroC.Controls.IncDecButton.IncDecButtonArrowImage.DownArrow;
      this.DecreaseButton.Location = new System.Drawing.Point(32, 104);
      this.DecreaseButton.LowLevelEnabled = true;
      this.DecreaseButton.Name = "DecreaseButton";
      this.DecreaseButton.Size = new System.Drawing.Size(69, 75);
      this.DecreaseButton.TabIndex = 8;
      this.DecreaseButton.TimerInterval = 300;
      this.DecreaseButton.TranslateText = false;
      this.DecreaseButton.OnIncDecButtonDown += new SyncroC.Controls.IncDecButton.IncDecButtonDownHandler(this.DecreaseButton_OnIncDecButtonDown);
      // 
      // IncreaseButton
      // 
      this.IncreaseButton.BackColor = System.Drawing.Color.WhiteSmoke;
      this.IncreaseButton.Cursor = System.Windows.Forms.Cursors.Hand;
      this.IncreaseButton.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.IncreaseButton.Image = SyncroC.Controls.IncDecButton.IncDecButtonArrowImage.UpArrow;
      this.IncreaseButton.Location = new System.Drawing.Point(281, 104);
      this.IncreaseButton.LowLevelEnabled = true;
      this.IncreaseButton.Name = "IncreaseButton";
      this.IncreaseButton.Size = new System.Drawing.Size(69, 75);
      this.IncreaseButton.TabIndex = 9;
      this.IncreaseButton.TimerInterval = 300;
      this.IncreaseButton.TranslateText = false;
      this.IncreaseButton.OnIncDecButtonDown += new SyncroC.Controls.IncDecButton.IncDecButtonDownHandler(this.IncreaseButton_OnIncDecButtonDown);
      // 
      // Frm_AskValueAndConfirm
      // 
      this.AcceptButton = this.btn_OK;
      this.CancelButton = this.btn_Abort_YN;
      this.ClientSize = new System.Drawing.Size(382, 264);
      this.Controls.Add(this.IncreaseButton);
      this.Controls.Add(this.DecreaseButton);
      this.Controls.Add(this.lbl_Confirmation_YN);
      this.Controls.Add(this.tnd_Value);
      this.Controls.Add(this.btn_Abort_YN);
      this.Controls.Add(this.btn_OK);
      this.Name = "Frm_AskValueAndConfirm";
      this.Text = "Frm_AskValueAndConfirm";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lbl_Confirmation_YN;
    private SyncroC.Controls.IncDecButton.IncDecButton DecreaseButton;
    private SyncroC.Controls.IncDecButton.IncDecButton IncreaseButton;
  }
}