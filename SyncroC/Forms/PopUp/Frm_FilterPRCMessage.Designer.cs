namespace SyncroC.Forms.PopUp
{
  partial class Frm_FilterPRCMessage
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_FilterPRCMessage));
      this.ttl_Quality = new SyncroC.Controls.Title();
      this.tlst_FluteID = new SyncroC.Controls.TitledList();
      this.ttb_QualityId = new SyncroC.Controls.TitledTextBox();
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
      // ttl_Quality
      // 
      this.ttl_Quality.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.ttl_Quality.color = SyncroC.Controls.Colors.SyncroColor.Green;
      this.ttl_Quality.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Quality.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Quality.Location = new System.Drawing.Point(12, 12);
      this.ttl_Quality.Name = "ttl_Quality";
      this.ttl_Quality.Size = new System.Drawing.Size(348, 72);
      this.ttl_Quality.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Quality.TabIndex = 113;
      this.ttl_Quality.TabStop = false;
      this.ttl_Quality.Text = "QUALITY DATA";
      this.ttl_Quality.TranslateText = false;
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
      this.tlst_FluteID.TabIndex = 112;
      this.tlst_FluteID.Text = "Flute ID";
      this.tlst_FluteID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.tlst_FluteID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlst_FluteID.TranslateText = false;
      this.tlst_FluteID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
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
      this.ttb_QualityId.TabIndex = 111;
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
      this.btn_Abort_NN.Location = new System.Drawing.Point(201, 103);
      this.btn_Abort_NN.Name = "btn_Abort_NN";
      this.btn_Abort_NN.Size = new System.Drawing.Size(135, 57);
      this.btn_Abort_NN.TabIndex = 110;
      this.btn_Abort_NN.Text = "ABORT";
      this.btn_Abort_NN.UseVisualStyleBackColor = false;
      this.btn_Abort_NN.Click += new System.EventHandler(this.btn_Abort_NN_Click);
      // 
      // btn_OK
      // 
      this.btn_OK.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_OK.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_OK.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_OK.Location = new System.Drawing.Point(32, 103);
      this.btn_OK.Name = "btn_OK";
      this.btn_OK.Size = new System.Drawing.Size(135, 57);
      this.btn_OK.TabIndex = 109;
      this.btn_OK.Text = "OK";
      this.btn_OK.UseVisualStyleBackColor = false;
      this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
      // 
      // Frm_FilterPRCMessage
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(376, 187);
      this.Controls.Add(this.ttl_Quality);
      this.Controls.Add(this.tlst_FluteID);
      this.Controls.Add(this.ttb_QualityId);
      this.Controls.Add(this.btn_Abort_NN);
      this.Controls.Add(this.btn_OK);
      this.Name = "Frm_FilterPRCMessage";
      this.Text = "Frm_FilterPRCMessage";
      this.ResumeLayout(false);

    }

    #endregion

    private SyncroC.Controls.Title ttl_Quality;
    private SyncroC.Controls.TitledList tlst_FluteID;
    private SyncroC.Controls.TitledTextBox ttb_QualityId;
    private System.Windows.Forms.Button btn_Abort_NN;
    private System.Windows.Forms.Button btn_OK;
  }
}