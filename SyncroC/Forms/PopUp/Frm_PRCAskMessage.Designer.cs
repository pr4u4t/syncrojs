namespace SyncroC.Forms.PopUp
{
  partial class Frm_PRCAskMessage
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PRCAskMessage));
      this.ttb_Value = new SyncroC.Controls.TitledTextBox();
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
      // ttb_Value
      // 
      this.ttb_Value.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.ttb_Value.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_Value.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Value.Location = new System.Drawing.Point(25, 25);
      this.ttb_Value.MaxChar = 32767;
      this.ttb_Value.Multiline = true;
      this.ttb_Value.Name = "ttb_Value";
      this.ttb_Value.PasswordChar = '\0';
      this.ttb_Value.ReadOnly = false;
      this.ttb_Value.Size = new System.Drawing.Size(299, 165);
      this.ttb_Value.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_Value.TabIndex = 6;
      this.ttb_Value.Text = "Value";
      this.ttb_Value.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_Value.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Value.TranslateText = false;
      this.ttb_Value.Value = " ";
      this.ttb_Value.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // btn_Abort_NN
      // 
      this.btn_Abort_NN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_Abort_NN.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Abort_NN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btn_Abort_NN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_Abort_NN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_Abort_NN.Location = new System.Drawing.Point(189, 197);
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
      this.btn_OK.Location = new System.Drawing.Point(25, 197);
      this.btn_OK.Name = "btn_OK";
      this.btn_OK.Size = new System.Drawing.Size(135, 57);
      this.btn_OK.TabIndex = 4;
      this.btn_OK.Text = "OK";
      this.btn_OK.UseVisualStyleBackColor = false;
      this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
      // 
      // Frm_PRCAskMessage
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(350, 280);
      this.Controls.Add(this.ttb_Value);
      this.Controls.Add(this.btn_Abort_NN);
      this.Controls.Add(this.btn_OK);
      this.KeyPreview = false;
      this.Name = "Frm_PRCAskMessage";
      this.Text = "Frm_PRCAskMessage";
      this.ResumeLayout(false);

    }

    #endregion

    private SyncroC.Controls.TitledTextBox ttb_Value;
    private System.Windows.Forms.Button btn_Abort_NN;
    private System.Windows.Forms.Button btn_OK;
  }
}