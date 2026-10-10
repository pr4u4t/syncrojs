namespace SyncroC.Forms.PopUp
{
  partial class Frm_TimeSetup
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_TimeSetup));
      this.cmb_TimeZones = new System.Windows.Forms.ComboBox();
      this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
      this.btn_Abort_YN = new System.Windows.Forms.Button();
      this.btn_OK = new System.Windows.Forms.Button();
      this.timerDateTimePicker = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "");
      // 
      // cmb_TimeZones
      // 
      this.cmb_TimeZones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmb_TimeZones.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_TimeZones.FormattingEnabled = true;
      this.cmb_TimeZones.Location = new System.Drawing.Point(25, 29);
      this.cmb_TimeZones.Name = "cmb_TimeZones";
      this.cmb_TimeZones.Size = new System.Drawing.Size(299, 23);
      this.cmb_TimeZones.TabIndex = 52;
      // 
      // dateTimePicker
      // 
      this.dateTimePicker.CalendarFont = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dateTimePicker.CustomFormat = "HH:mm:ss dd/MM/yyyy";
      this.dateTimePicker.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dateTimePicker.Location = new System.Drawing.Point(25, 62);
      this.dateTimePicker.Name = "dateTimePicker";
      this.dateTimePicker.ShowUpDown = true;
      this.dateTimePicker.Size = new System.Drawing.Size(299, 44);
      this.dateTimePicker.TabIndex = 51;
      this.dateTimePicker.Value = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
      this.dateTimePicker.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dateTimePicker_PreviewKeyDown);
      // 
      // btn_Abort_YN
      // 
      this.btn_Abort_YN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btn_Abort_YN.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Abort_YN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btn_Abort_YN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_Abort_YN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_Abort_YN.Location = new System.Drawing.Point(189, 117);
      this.btn_Abort_YN.Name = "btn_Abort_YN";
      this.btn_Abort_YN.Size = new System.Drawing.Size(135, 57);
      this.btn_Abort_YN.TabIndex = 54;
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
      this.btn_OK.Location = new System.Drawing.Point(25, 117);
      this.btn_OK.Name = "btn_OK";
      this.btn_OK.Size = new System.Drawing.Size(135, 57);
      this.btn_OK.TabIndex = 53;
      this.btn_OK.Text = "OK";
      this.btn_OK.UseVisualStyleBackColor = false;
      this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
      // 
      // timerDateTimePicker
      // 
      this.timerDateTimePicker.Enabled = true;
      this.timerDateTimePicker.Interval = 500;
      this.timerDateTimePicker.Tick += new System.EventHandler(this.timerDateTimePicker_Tick);
      // 
      // Frm_TimeSetup
      // 
      this.AcceptButton = this.btn_OK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btn_Abort_YN;
      this.ClientSize = new System.Drawing.Size(350, 200);
      this.Controls.Add(this.btn_Abort_YN);
      this.Controls.Add(this.btn_OK);
      this.Controls.Add(this.cmb_TimeZones);
      this.Controls.Add(this.dateTimePicker);
      this.Name = "Frm_TimeSetup";
      this.Text = "Frm_TimeSetup";
      this.Load += new System.EventHandler(this.Frm_TimeSetup_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ComboBox cmb_TimeZones;
    private System.Windows.Forms.DateTimePicker dateTimePicker;
    private System.Windows.Forms.Button btn_Abort_YN;
    private System.Windows.Forms.Button btn_OK;
    private System.Windows.Forms.Timer timerDateTimePicker;
  }
}