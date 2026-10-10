namespace SyncroC.Forms.LateralBar
{
  partial class Frm_LateralBar
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_LateralBar));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.lateralBarItem1 = new SyncroC.Controls.GroupPanel.LateralBarItem();
      this.lateralBarItem2 = new SyncroC.Controls.GroupPanel.LateralBarItem();
      this.lateralBarItem3 = new SyncroC.Controls.GroupPanel.LateralBarItem();
      this.timer_setUpButton = new System.Windows.Forms.Timer(this.components);
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel1.BackColor = System.Drawing.Color.Black;
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Controls.Add(this.lateralBarItem1, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.lateralBarItem2, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.lateralBarItem3, 1, 2);
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(400, 1025);
      this.tableLayoutPanel1.TabIndex = 6;
      // 
      // lateralBarItem1
      // 
      this.lateralBarItem1.BackColor = System.Drawing.Color.Transparent;
      this.lateralBarItem1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lateralBarItem1.Location = new System.Drawing.Point(5, 0);
      this.lateralBarItem1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
      this.lateralBarItem1.Name = "lateralBarItem1";
      this.lateralBarItem1.Size = new System.Drawing.Size(395, 338);
      this.lateralBarItem1.TabIndex = 0;
      this.lateralBarItem1.TranslateText = false;
      this.lateralBarItem1.OnLateralBarItemSelectedIndexChanged += new SyncroC.Controls.GroupPanel.LateralBarItemSelectedIndexChanged(this.lateralBarItemN_OnLateralBarItemSelectedIndexChanged);
      // 
      // lateralBarItem2
      // 
      this.lateralBarItem2.BackColor = System.Drawing.Color.Transparent;
      this.lateralBarItem2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lateralBarItem2.Location = new System.Drawing.Point(5, 341);
      this.lateralBarItem2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
      this.lateralBarItem2.Name = "lateralBarItem2";
      this.lateralBarItem2.Size = new System.Drawing.Size(395, 338);
      this.lateralBarItem2.TabIndex = 2;
      this.lateralBarItem2.TranslateText = false;
      this.lateralBarItem2.OnLateralBarItemSelectedIndexChanged += new SyncroC.Controls.GroupPanel.LateralBarItemSelectedIndexChanged(this.lateralBarItemN_OnLateralBarItemSelectedIndexChanged);
      // 
      // lateralBarItem3
      // 
      this.lateralBarItem3.BackColor = System.Drawing.Color.Transparent;
      this.lateralBarItem3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lateralBarItem3.Location = new System.Drawing.Point(5, 682);
      this.lateralBarItem3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
      this.lateralBarItem3.Name = "lateralBarItem3";
      this.lateralBarItem3.Size = new System.Drawing.Size(395, 338);
      this.lateralBarItem3.TabIndex = 3;
      this.lateralBarItem3.TranslateText = false;
      this.lateralBarItem3.OnLateralBarItemSelectedIndexChanged += new SyncroC.Controls.GroupPanel.LateralBarItemSelectedIndexChanged(this.lateralBarItemN_OnLateralBarItemSelectedIndexChanged);
      // 
      // timer_setUpButton
      // 
      this.timer_setUpButton.Enabled = true;
      this.timer_setUpButton.Interval = 500;
      this.timer_setUpButton.Tick += new System.EventHandler(this.timer_setUpButton_Tick);
      // 
      // Frm_LateralBar
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.ClientSize = new System.Drawing.Size(400, 1025);
      this.Controls.Add(this.tableLayoutPanel1);
      this.ForeColor = System.Drawing.Color.Black;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Location = new System.Drawing.Point(1280, 0);
      this.Name = "Frm_LateralBar";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Tag = "Frm_LateralBar";
      this.Text = "Frm_LateralBar";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private SyncroC.Controls.GroupPanel.LateralBarItem lateralBarItem1;
    private SyncroC.Controls.GroupPanel.LateralBarItem lateralBarItem2;
    private SyncroC.Controls.GroupPanel.LateralBarItem lateralBarItem3;
    private System.Windows.Forms.Timer timer_setUpButton;
  }
}