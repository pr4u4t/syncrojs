namespace SyncroC.Controls
{
  partial class LowLevelControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LowLevelControl));
      this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
      this.lbl_Name = new System.Windows.Forms.Label();
      this.pct_AutoMan = new System.Windows.Forms.PictureBox();
      this.pct_LocaleRemote = new System.Windows.Forms.PictureBox();
      this.lbl_position = new SyncroC.Controls.myLabel();
      this.iml_AutoManImages = new System.Windows.Forms.ImageList(this.components);
      this.iml_LocaleRemoteImages = new System.Windows.Forms.ImageList(this.components);
      this.tableLayoutPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_AutoMan)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_LocaleRemote)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel
      // 
      this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel.ColumnCount = 3;
      this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
      this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
      this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
      this.tableLayoutPanel.Controls.Add(this.lbl_Name, 1, 1);
      this.tableLayoutPanel.Controls.Add(this.pct_AutoMan, 0, 1);
      this.tableLayoutPanel.Controls.Add(this.pct_LocaleRemote, 2, 1);
      this.tableLayoutPanel.Controls.Add(this.lbl_position, 0, 0);
      this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel.Name = "tableLayoutPanel";
      this.tableLayoutPanel.RowCount = 2;
      this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
      this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel.Size = new System.Drawing.Size(135, 135);
      this.tableLayoutPanel.TabIndex = 18;
      // 
      // lbl_Name
      // 
      this.lbl_Name.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Name.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbl_Name.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
      this.lbl_Name.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.lbl_Name.Location = new System.Drawing.Point(44, 101);
      this.lbl_Name.Margin = new System.Windows.Forms.Padding(0);
      this.lbl_Name.Name = "lbl_Name";
      this.lbl_Name.Size = new System.Drawing.Size(45, 34);
      this.lbl_Name.TabIndex = 8;
      this.lbl_Name.Text = "69";
      this.lbl_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // pct_AutoMan
      // 
      this.pct_AutoMan.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_AutoMan.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pct_AutoMan.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pct_AutoMan.Location = new System.Drawing.Point(0, 101);
      this.pct_AutoMan.Margin = new System.Windows.Forms.Padding(0);
      this.pct_AutoMan.Name = "pct_AutoMan";
      this.pct_AutoMan.Size = new System.Drawing.Size(44, 34);
      this.pct_AutoMan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_AutoMan.TabIndex = 7;
      this.pct_AutoMan.TabStop = false;
      this.pct_AutoMan.Click += new System.EventHandler(this.pct_AutoMan_Click);
      this.pct_AutoMan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pct_AutoMan_MouseDown);
      this.pct_AutoMan.Paint += new System.Windows.Forms.PaintEventHandler(this.pct_LocaleRemoteAndAutoMan_Paint);
      this.pct_AutoMan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pct_AutoMan_MouseUp);
      // 
      // pct_LocaleRemote
      // 
      this.pct_LocaleRemote.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_LocaleRemote.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pct_LocaleRemote.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pct_LocaleRemote.Location = new System.Drawing.Point(89, 101);
      this.pct_LocaleRemote.Margin = new System.Windows.Forms.Padding(0);
      this.pct_LocaleRemote.Name = "pct_LocaleRemote";
      this.pct_LocaleRemote.Size = new System.Drawing.Size(46, 34);
      this.pct_LocaleRemote.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_LocaleRemote.TabIndex = 6;
      this.pct_LocaleRemote.TabStop = false;
      this.pct_LocaleRemote.Click += new System.EventHandler(this.pct_LocaleRemote_Click);
      this.pct_LocaleRemote.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pct_LocaleRemote_MouseDown);
      this.pct_LocaleRemote.Paint += new System.Windows.Forms.PaintEventHandler(this.pct_LocaleRemoteAndAutoMan_Paint);
      this.pct_LocaleRemote.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pct_LocaleRemote_MouseUp);
      // 
      // lbl_position
      // 
      this.lbl_position.BackcolorTesto = System.Drawing.Color.Transparent;
      this.lbl_position.BackgroundImage = global::SyncroC.Properties.Resources.GLUE_GAP;
      this.lbl_position.Colore1 = System.Drawing.Color.Black;
      this.lbl_position.Colore2 = System.Drawing.Color.Black;
      this.tableLayoutPanel.SetColumnSpan(this.lbl_position, 3);
      this.lbl_position.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbl_position.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_position.Location = new System.Drawing.Point(0, 0);
      this.lbl_position.Margin = new System.Windows.Forms.Padding(0);
      this.lbl_position.Name = "lbl_position";
      this.lbl_position.OutLineText = false;
      this.lbl_position.OutLineTextcolore = System.Drawing.Color.DarkBlue;
      this.lbl_position.Size = new System.Drawing.Size(135, 101);
      this.lbl_position.TabIndex = 9;
      this.lbl_position.Testo = "Design Mode";
      this.lbl_position.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      this.lbl_position.TranslateText = false;
      this.lbl_position.Click += new System.EventHandler(this.lbl_LowLevelControl_Click);
      // 
      // iml_AutoManImages
      // 
      this.iml_AutoManImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AutoManImages.ImageStream")));
      this.iml_AutoManImages.TransparentColor = System.Drawing.Color.WhiteSmoke;
      this.iml_AutoManImages.Images.SetKeyName(0, "Automatico_ISO_0017.bmp");
      this.iml_AutoManImages.Images.SetKeyName(1, "Manuale_ISO_0096.bmp");
      this.iml_AutoManImages.Images.SetKeyName(2, "Automatico_ISO_0017_Reverse.bmp");
      this.iml_AutoManImages.Images.SetKeyName(3, "Manuale_ISO_0096_Reverse.bmp");
      // 
      // iml_LocaleRemoteImages
      // 
      this.iml_LocaleRemoteImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_LocaleRemoteImages.ImageStream")));
      this.iml_LocaleRemoteImages.TransparentColor = System.Drawing.Color.WhiteSmoke;
      this.iml_LocaleRemoteImages.Images.SetKeyName(0, "LOCALE_ISO_1109.bmp");
      this.iml_LocaleRemoteImages.Images.SetKeyName(1, "REMOTO_ISO_1108.bmp");
      this.iml_LocaleRemoteImages.Images.SetKeyName(2, "LOCALE_ISO_1109l_PREMUTO.bmp");
      this.iml_LocaleRemoteImages.Images.SetKeyName(3, "REMOTO_ISO_1108_PREMUTO.bmp");
      // 
      // LowLevelControl
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
      this.Controls.Add(this.tableLayoutPanel);
      this.Name = "LowLevelControl";
      this.Size = new System.Drawing.Size(135, 135);
      this.SizeChanged += new System.EventHandler(this.LowLevelControl_SizeChanged);
      this.tableLayoutPanel.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct_AutoMan)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_LocaleRemote)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    protected System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    protected System.Windows.Forms.Label lbl_Name;
    protected System.Windows.Forms.PictureBox pct_AutoMan;
    protected System.Windows.Forms.PictureBox pct_LocaleRemote;
    protected myLabel lbl_position;
    private System.Windows.Forms.ImageList iml_AutoManImages;
    private System.Windows.Forms.ImageList iml_LocaleRemoteImages;
  }
}
