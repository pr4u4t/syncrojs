namespace SyncroC.Forms.PopUp
{
  partial class Frm_FilterQuality
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_FilterQuality));
      this.ttb_QualityId = new SyncroC.Controls.TitledTextBox();
      this.btn_Abort_NN = new System.Windows.Forms.Button();
      this.btn_OK = new System.Windows.Forms.Button();
      this.tlst_FluteID = new SyncroC.Controls.TitledList();
      this.ttb_PaperID = new SyncroC.Controls.TitledTextBox();
      this.tlst_PaperWeightFactor = new SyncroC.Controls.TitledList();
      this.ttl_PaperData = new SyncroC.Controls.Title();
      this.ttl_Quality = new SyncroC.Controls.Title();
      this.btn_selectCurrent_YN = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "");
      // 
      // ttb_QualityId
      // 
      this.ttb_QualityId.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.ttb_QualityId.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_QualityId.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_QualityId.Location = new System.Drawing.Point(25, 34);
      this.ttb_QualityId.MaxChar = 32767;
      this.ttb_QualityId.Multiline = false;
      this.ttb_QualityId.Name = "ttb_QualityId";
      this.ttb_QualityId.PasswordChar = '\0';
      this.ttb_QualityId.ReadOnly = false;
      this.ttb_QualityId.Size = new System.Drawing.Size(236, 50);
      this.ttb_QualityId.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_QualityId.TabIndex = 6;
      this.ttb_QualityId.Text = "Quality Id";
      this.ttb_QualityId.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_QualityId.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_QualityId.TranslateText = false;
      this.ttb_QualityId.Value = "Cartacce";
      this.ttb_QualityId.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // btn_Abort_NN
      // 
      this.btn_Abort_NN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_Abort_NN.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Abort_NN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btn_Abort_NN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_Abort_NN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_Abort_NN.Location = new System.Drawing.Point(239, 168);
      this.btn_Abort_NN.Name = "btn_Abort_NN";
      this.btn_Abort_NN.Size = new System.Drawing.Size(135, 57);
      this.btn_Abort_NN.TabIndex = 5;
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
      this.btn_OK.Location = new System.Drawing.Point(70, 168);
      this.btn_OK.Name = "btn_OK";
      this.btn_OK.Size = new System.Drawing.Size(135, 57);
      this.btn_OK.TabIndex = 4;
      this.btn_OK.Text = "OK";
      this.btn_OK.UseVisualStyleBackColor = false;
      this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
      // 
      // tlst_FluteID
      // 
      this.tlst_FluteID.BackColor = System.Drawing.Color.LightGray;
      this.tlst_FluteID.CanCancel = true;
      this.tlst_FluteID.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold);
      this.tlst_FluteID.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlst_FluteID.Location = new System.Drawing.Point(269, 34);
      this.tlst_FluteID.Name = "tlst_FluteID";
      this.tlst_FluteID.ReadOnly = false;
      this.tlst_FluteID.SelectedIndex = -1;
      this.tlst_FluteID.SelectedItem = "";
      this.tlst_FluteID.SelectedItemData = "";
      this.tlst_FluteID.Size = new System.Drawing.Size(82, 50);
      this.tlst_FluteID.Sorted = true;
      this.tlst_FluteID.Style = SyncroC.Controls.TitledListStyles.Light;
      this.tlst_FluteID.TabIndex = 7;
      this.tlst_FluteID.Text = "Flute ID";
      this.tlst_FluteID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.tlst_FluteID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlst_FluteID.TranslateText = false;
      this.tlst_FluteID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_PaperID
      // 
      this.ttb_PaperID.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.ttb_PaperID.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold);
      this.ttb_PaperID.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_PaperID.Location = new System.Drawing.Point(25, 112);
      this.ttb_PaperID.MaxChar = 12;
      this.ttb_PaperID.Multiline = false;
      this.ttb_PaperID.Name = "ttb_PaperID";
      this.ttb_PaperID.PasswordChar = '\0';
      this.ttb_PaperID.ReadOnly = false;
      this.ttb_PaperID.Size = new System.Drawing.Size(236, 50);
      this.ttb_PaperID.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_PaperID.TabIndex = 16;
      this.ttb_PaperID.Text = "Paper ID";
      this.ttb_PaperID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_PaperID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_PaperID.TranslateText = false;
      this.ttb_PaperID.Value = "Cartaccia";
      this.ttb_PaperID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // tlst_PaperWeightFactor
      // 
      this.tlst_PaperWeightFactor.BackColor = System.Drawing.Color.LightGray;
      this.tlst_PaperWeightFactor.CanCancel = true;
      this.tlst_PaperWeightFactor.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold);
      this.tlst_PaperWeightFactor.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlst_PaperWeightFactor.Location = new System.Drawing.Point(267, 112);
      this.tlst_PaperWeightFactor.Name = "tlst_PaperWeightFactor";
      this.tlst_PaperWeightFactor.ReadOnly = false;
      this.tlst_PaperWeightFactor.SelectedIndex = -1;
      this.tlst_PaperWeightFactor.SelectedItem = "";
      this.tlst_PaperWeightFactor.SelectedItemData = "";
      this.tlst_PaperWeightFactor.Size = new System.Drawing.Size(123, 50);
      this.tlst_PaperWeightFactor.Sorted = false;
      this.tlst_PaperWeightFactor.Style = SyncroC.Controls.TitledListStyles.Light;
      this.tlst_PaperWeightFactor.TabIndex = 18;
      this.tlst_PaperWeightFactor.Text = "Weight Factor";
      this.tlst_PaperWeightFactor.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.tlst_PaperWeightFactor.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlst_PaperWeightFactor.TranslateText = false;
      this.tlst_PaperWeightFactor.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttl_PaperData
      // 
      this.ttl_PaperData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(205)))), ((int)(((byte)(203)))));
      this.ttl_PaperData.color = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.ttl_PaperData.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_PaperData.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_PaperData.Location = new System.Drawing.Point(12, 90);
      this.ttl_PaperData.Name = "ttl_PaperData";
      this.ttl_PaperData.Size = new System.Drawing.Size(418, 72);
      this.ttl_PaperData.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_PaperData.TabIndex = 107;
      this.ttl_PaperData.TabStop = false;
      this.ttl_PaperData.Text = "PAPER DATA";
      this.ttl_PaperData.TranslateText = false;
      // 
      // ttl_Quality
      // 
      this.ttl_Quality.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.ttl_Quality.color = SyncroC.Controls.Colors.SyncroColor.Green;
      this.ttl_Quality.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Quality.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Quality.Location = new System.Drawing.Point(12, 12);
      this.ttl_Quality.Name = "ttl_Quality";
      this.ttl_Quality.Size = new System.Drawing.Size(418, 72);
      this.ttl_Quality.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Quality.TabIndex = 108;
      this.ttl_Quality.TabStop = false;
      this.ttl_Quality.Text = "QUALITY DATA";
      this.ttl_Quality.TranslateText = false;
      // 
      // btn_selectCurrent_YN
      // 
      this.btn_selectCurrent_YN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_selectCurrent_YN.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_selectCurrent_YN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_selectCurrent_YN.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_selectCurrent_YN.Location = new System.Drawing.Point(359, 34);
      this.btn_selectCurrent_YN.Name = "btn_selectCurrent_YN";
      this.btn_selectCurrent_YN.Size = new System.Drawing.Size(71, 50);
      this.btn_selectCurrent_YN.TabIndex = 109;
      this.btn_selectCurrent_YN.Text = "Current Quality";
      this.btn_selectCurrent_YN.UseVisualStyleBackColor = false;
      this.btn_selectCurrent_YN.Click += new System.EventHandler(this.btn_selectCurrent_Click);
      // 
      // Frm_FilterQuality
      // 
      this.AcceptButton = this.btn_OK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btn_Abort_NN;
      this.ClientSize = new System.Drawing.Size(444, 252);
      this.Controls.Add(this.btn_selectCurrent_YN);
      this.Controls.Add(this.ttl_Quality);
      this.Controls.Add(this.ttl_PaperData);
      this.Controls.Add(this.tlst_PaperWeightFactor);
      this.Controls.Add(this.ttb_PaperID);
      this.Controls.Add(this.tlst_FluteID);
      this.Controls.Add(this.ttb_QualityId);
      this.Controls.Add(this.btn_Abort_NN);
      this.Controls.Add(this.btn_OK);
      this.Name = "Frm_FilterQuality";
      this.Text = "Frm_FilterQuality";
      this.ResumeLayout(false);

    }

    #endregion

    private SyncroC.Controls.TitledTextBox ttb_QualityId;
    private System.Windows.Forms.Button btn_Abort_NN;
    private System.Windows.Forms.Button btn_OK;
    private SyncroC.Controls.TitledList tlst_FluteID;
    private SyncroC.Controls.TitledTextBox ttb_PaperID;
    private SyncroC.Controls.TitledList tlst_PaperWeightFactor;
    private SyncroC.Controls.Title ttl_PaperData;
    private SyncroC.Controls.Title ttl_Quality;
    private System.Windows.Forms.Button btn_selectCurrent_YN;
  }
}