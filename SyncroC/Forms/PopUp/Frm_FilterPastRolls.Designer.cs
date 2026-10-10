namespace SyncroC.Forms.PopUp
{
  partial class Frm_FilterPastRolls
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_FilterPastRolls));
      this.ttb_rollId = new SyncroC.Controls.TitledTextBox();
      this.ttb_paperType = new SyncroC.Controls.TitledTextBox();
      this.btn_Abort_NN = new System.Windows.Forms.Button();
      this.btn_OK = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "");
      // 
      // ttb_rollId
      // 
      this.ttb_rollId.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.ttb_rollId.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_rollId.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_rollId.Location = new System.Drawing.Point(25, 17);
      this.ttb_rollId.MaxChar = 32767;
      this.ttb_rollId.Multiline = false;
      this.ttb_rollId.Name = "ttb_rollId";
      this.ttb_rollId.PasswordChar = '\0';
      this.ttb_rollId.ReadOnly = false;
      this.ttb_rollId.Size = new System.Drawing.Size(303, 50);
      this.ttb_rollId.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_rollId.TabIndex = 14;
      this.ttb_rollId.Text = "Roll Id";
      this.ttb_rollId.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_rollId.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_rollId.TranslateText = false;
      this.ttb_rollId.Value = "123456789";
      this.ttb_rollId.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_paperType
      // 
      this.ttb_paperType.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.ttb_paperType.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_paperType.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_paperType.Location = new System.Drawing.Point(25, 69);
      this.ttb_paperType.MaxChar = 32767;
      this.ttb_paperType.Multiline = false;
      this.ttb_paperType.Name = "ttb_paperType";
      this.ttb_paperType.PasswordChar = '\0';
      this.ttb_paperType.ReadOnly = false;
      this.ttb_paperType.Size = new System.Drawing.Size(303, 50);
      this.ttb_paperType.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_paperType.TabIndex = 13;
      this.ttb_paperType.Text = "Paper Type";
      this.ttb_paperType.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_paperType.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_paperType.TranslateText = false;
      this.ttb_paperType.Value = "Andrea Bello";
      this.ttb_paperType.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // btn_Abort_NN
      // 
      this.btn_Abort_NN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_Abort_NN.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Abort_NN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btn_Abort_NN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_Abort_NN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_Abort_NN.Location = new System.Drawing.Point(191, 125);
      this.btn_Abort_NN.Name = "btn_Abort_NN";
      this.btn_Abort_NN.Size = new System.Drawing.Size(135, 57);
      this.btn_Abort_NN.TabIndex = 12;
      this.btn_Abort_NN.Text = "ABORT";
      this.btn_Abort_NN.UseVisualStyleBackColor = false;
      this.btn_Abort_NN.Click += new System.EventHandler(this.btn_Abort_YN_Click);
      // 
      // btn_OK
      // 
      this.btn_OK.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_OK.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_OK.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_OK.Location = new System.Drawing.Point(22, 125);
      this.btn_OK.Name = "btn_OK";
      this.btn_OK.Size = new System.Drawing.Size(135, 57);
      this.btn_OK.TabIndex = 11;
      this.btn_OK.Text = "OK";
      this.btn_OK.UseVisualStyleBackColor = false;
      this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
      // 
      // Frm_FilterPastRolls
      // 
      this.AcceptButton = this.btn_OK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btn_Abort_NN;
      this.ClientSize = new System.Drawing.Size(350, 200);
      this.Controls.Add(this.ttb_rollId);
      this.Controls.Add(this.ttb_paperType);
      this.Controls.Add(this.btn_Abort_NN);
      this.Controls.Add(this.btn_OK);
      this.Name = "Frm_FilterPastRolls";
      this.Text = "Frm_FilterPastRolls";
      this.ResumeLayout(false);

    }

    #endregion

    private SyncroC.Controls.TitledTextBox ttb_rollId;
    private SyncroC.Controls.TitledTextBox ttb_paperType;
    private System.Windows.Forms.Button btn_Abort_NN;
    private System.Windows.Forms.Button btn_OK;
  }
}