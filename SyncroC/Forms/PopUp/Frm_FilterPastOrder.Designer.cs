namespace SyncroC.Forms.PopUp
{
  partial class Frm_FilterPastOrder
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_FilterPastOrder));
      this.btn_Abort_NN = new System.Windows.Forms.Button();
      this.btn_OK = new System.Windows.Forms.Button();
      this.ttb_SetupCode = new SyncroC.Controls.TitledTextBox();
      this.ttb_customerName = new SyncroC.Controls.TitledTextBox();
      this.ttb_orderNumber = new SyncroC.Controls.TitledTextBox();
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
      this.btn_Abort_NN.Location = new System.Drawing.Point(190, 196);
      this.btn_Abort_NN.Name = "btn_Abort_NN";
      this.btn_Abort_NN.Size = new System.Drawing.Size(135, 57);
      this.btn_Abort_NN.TabIndex = 7;
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
      this.btn_OK.Location = new System.Drawing.Point(21, 196);
      this.btn_OK.Name = "btn_OK";
      this.btn_OK.Size = new System.Drawing.Size(135, 57);
      this.btn_OK.TabIndex = 6;
      this.btn_OK.Text = "OK";
      this.btn_OK.UseVisualStyleBackColor = false;
      this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
      // 
      // ttb_SetupCode
      // 
      this.ttb_SetupCode.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.ttb_SetupCode.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_SetupCode.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_SetupCode.Location = new System.Drawing.Point(24, 23);
      this.ttb_SetupCode.MaxChar = 32767;
      this.ttb_SetupCode.Multiline = false;
      this.ttb_SetupCode.Name = "ttb_SetupCode";
      this.ttb_SetupCode.PasswordChar = '\0';
      this.ttb_SetupCode.ReadOnly = false;
      this.ttb_SetupCode.Size = new System.Drawing.Size(303, 50);
      this.ttb_SetupCode.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_SetupCode.TabIndex = 8;
      this.ttb_SetupCode.Text = "Setup Code";
      this.ttb_SetupCode.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_SetupCode.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_SetupCode.TranslateText = false;
      this.ttb_SetupCode.Value = "123456789";
      this.ttb_SetupCode.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_customerName
      // 
      this.ttb_customerName.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.ttb_customerName.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_customerName.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_customerName.Location = new System.Drawing.Point(24, 135);
      this.ttb_customerName.MaxChar = 32767;
      this.ttb_customerName.Multiline = false;
      this.ttb_customerName.Name = "ttb_customerName";
      this.ttb_customerName.PasswordChar = '\0';
      this.ttb_customerName.ReadOnly = false;
      this.ttb_customerName.Size = new System.Drawing.Size(303, 50);
      this.ttb_customerName.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_customerName.TabIndex = 9;
      this.ttb_customerName.Text = "Customer Name";
      this.ttb_customerName.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_customerName.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_customerName.TranslateText = false;
      this.ttb_customerName.Value = "Andrea Bello";
      this.ttb_customerName.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_orderNumber
      // 
      this.ttb_orderNumber.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.ttb_orderNumber.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_orderNumber.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_orderNumber.Location = new System.Drawing.Point(24, 79);
      this.ttb_orderNumber.MaxChar = 32767;
      this.ttb_orderNumber.Multiline = false;
      this.ttb_orderNumber.Name = "ttb_orderNumber";
      this.ttb_orderNumber.PasswordChar = '\0';
      this.ttb_orderNumber.ReadOnly = false;
      this.ttb_orderNumber.Size = new System.Drawing.Size(303, 50);
      this.ttb_orderNumber.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_orderNumber.TabIndex = 10;
      this.ttb_orderNumber.Text = "Order Number";
      this.ttb_orderNumber.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_orderNumber.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_orderNumber.TranslateText = false;
      this.ttb_orderNumber.Value = "123456789";
      this.ttb_orderNumber.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // Frm_FilterPastOrder
      // 
      this.AcceptButton = this.btn_OK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btn_Abort_NN;
      this.ClientSize = new System.Drawing.Size(350, 279);
      this.Controls.Add(this.ttb_orderNumber);
      this.Controls.Add(this.ttb_customerName);
      this.Controls.Add(this.ttb_SetupCode);
      this.Controls.Add(this.btn_Abort_NN);
      this.Controls.Add(this.btn_OK);
      this.Name = "Frm_FilterPastOrder";
      this.Text = "Frm_FilterPastOrder";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btn_Abort_NN;
    private System.Windows.Forms.Button btn_OK;
    private SyncroC.Controls.TitledTextBox ttb_SetupCode;
    private SyncroC.Controls.TitledTextBox ttb_customerName;
    private SyncroC.Controls.TitledTextBox ttb_orderNumber;
  }
}