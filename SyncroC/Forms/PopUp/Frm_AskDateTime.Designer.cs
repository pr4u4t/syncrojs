namespace SyncroC.Forms.PopUp
{
  partial class Frm_AskDateTime
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AskDateTime));
      this.btn_Abort_YN = new System.Windows.Forms.Button();
      this.btn_OK = new System.Windows.Forms.Button();
      this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
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
      this.btn_Abort_YN.Location = new System.Drawing.Point(190, 117);
      this.btn_Abort_YN.Name = "btn_Abort_YN";
      this.btn_Abort_YN.Size = new System.Drawing.Size(135, 57);
      this.btn_Abort_YN.TabIndex = 57;
      this.btn_Abort_YN.Text = "ABORT";
      this.btn_Abort_YN.UseVisualStyleBackColor = false;
      this.btn_Abort_YN.Click += new System.EventHandler(this.btn_Abort_YN_Click);
      // 
      // btn_OK
      // 
      this.btn_OK.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_OK.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_OK.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_OK.Location = new System.Drawing.Point(26, 117);
      this.btn_OK.Name = "btn_OK";
      this.btn_OK.Size = new System.Drawing.Size(135, 57);
      this.btn_OK.TabIndex = 56;
      this.btn_OK.Text = "OK";
      this.btn_OK.UseVisualStyleBackColor = false;
      this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
      // 
      // dateTimePicker
      // 
      this.dateTimePicker.CalendarFont = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dateTimePicker.CustomFormat = "HH:mm:ss dd/MM/yyyy";
      this.dateTimePicker.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dateTimePicker.Location = new System.Drawing.Point(26, 44);
      this.dateTimePicker.Name = "dateTimePicker";
      this.dateTimePicker.ShowUpDown = true;
      this.dateTimePicker.Size = new System.Drawing.Size(299, 44);
      this.dateTimePicker.TabIndex = 55;
      this.dateTimePicker.Value = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
      // 
      // Frm_AskDateTime
      // 
      this.AcceptButton = this.btn_OK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btn_Abort_YN;
      this.ClientSize = new System.Drawing.Size(350, 200);
      this.Controls.Add(this.btn_Abort_YN);
      this.Controls.Add(this.btn_OK);
      this.Controls.Add(this.dateTimePicker);
      this.Name = "Frm_AskDateTime";
      this.Text = "Frm_AskDateTime";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btn_Abort_YN;
    private System.Windows.Forms.Button btn_OK;
    private System.Windows.Forms.DateTimePicker dateTimePicker;
  }
}