namespace SyncroC.Forms.Pages.Production.Operator
{
  partial class Frm_MessagePRC
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MessagePRC));
      this.gdv_Messages = new SyncroC.Controls.GridView();
      this.brk_PRCMessageBitmap = new SyncroC.Controls.Bracket();
      this.ttl_PRCMessagesList = new SyncroC.Controls.Title();
      this.pct_MessagePRC = new System.Windows.Forms.PictureBox();
      this.ttb_Comment = new SyncroC.Controls.TitledTextBox();
      this.bracket1 = new SyncroC.Controls.Bracket();
      this.ttb_QualityID = new SyncroC.Controls.TitledTextBox();
      this.ttb_ShiftManager = new SyncroC.Controls.TitledTextBox();
      this.ttb_ShiftID = new SyncroC.Controls.TitledTextBox();
      this.ttb_fluteid = new SyncroC.Controls.TitledTextBox();
      this.ttb_setupcode = new SyncroC.Controls.TitledTextBox();
      this.lbl_bottomComment = new System.Windows.Forms.Label();
      this.printGrid = new System.Windows.Forms.Button();
      this.btn_ShowGraph = new System.Windows.Forms.Button();
      this.lbl_filter = new SyncroC.Controls.myLabel();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_MessagePRC)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_TabC
      // 
      this.tab_TabC.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Text = "Erase";
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.Text = "Erase All";
      // 
      // iml_navigationImages
      // 
      this.iml_navigationImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_navigationImages.ImageStream")));
      this.iml_navigationImages.Images.SetKeyName(0, "");
      this.iml_navigationImages.Images.SetKeyName(1, "");
      this.iml_navigationImages.Images.SetKeyName(2, "");
      this.iml_navigationImages.Images.SetKeyName(3, "");
      // 
      // btn_Group1
      // 
      this.btn_Group1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedActiveImage")));
      this.btn_Group1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedImage")));
      // 
      // btn_Group2
      // 
      this.btn_Group2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedActiveImage")));
      this.btn_Group2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedImage")));
      // 
      // btn_Group3
      // 
      this.btn_Group3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedActiveImage")));
      this.btn_Group3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedImage")));
      // 
      // btn_Group5
      // 
      this.btn_Group5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedActiveImage")));
      this.btn_Group5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedImage")));
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // gdv_Messages
      // 
      this.gdv_Messages.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_Messages.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Messages.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Messages.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Messages.AutoGenerateColumns = false;
      this.gdv_Messages.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_Messages.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
      this.gdv_Messages.DataMember = "";
      this.gdv_Messages.DataSource = null;
      this.gdv_Messages.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gdv_Messages.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Messages.ImagePassword = null;
      this.gdv_Messages.Location = new System.Drawing.Point(34, 192);
      this.gdv_Messages.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
      this.gdv_Messages.MultiSelect = false;
      this.gdv_Messages.Name = "gdv_Messages";
      this.gdv_Messages.RowsHeight = 30;
      this.gdv_Messages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_Messages.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Messages.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Messages.Size = new System.Drawing.Size(155, 264);
      this.gdv_Messages.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_Messages.TabIndex = 152;
      this.gdv_Messages.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Messages.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Messages.TranslateText = true;
      this.gdv_Messages.SelectionChanged += new System.EventHandler(this.gdv_Messages_SelectionChanged);
      this.gdv_Messages.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_Messages_CellFormatting);
      // 
      // brk_PRCMessageBitmap
      // 
      this.brk_PRCMessageBitmap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.brk_PRCMessageBitmap.color = SyncroC.Controls.Colors.SyncroColor.Green;
      this.brk_PRCMessageBitmap.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.brk_PRCMessageBitmap.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.brk_PRCMessageBitmap.Location = new System.Drawing.Point(190, 171);
      this.brk_PRCMessageBitmap.Name = "brk_PRCMessageBitmap";
      this.brk_PRCMessageBitmap.Position = SyncroC.Controls.BracketPosition.Right;
      this.brk_PRCMessageBitmap.ShowArrow = false;
      this.brk_PRCMessageBitmap.Size = new System.Drawing.Size(1013, 558);
      this.brk_PRCMessageBitmap.TabIndex = 150;
      this.brk_PRCMessageBitmap.Text = "Message";
      this.brk_PRCMessageBitmap.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.brk_PRCMessageBitmap.TranslateText = true;
      // 
      // ttl_PRCMessagesList
      // 
      this.ttl_PRCMessagesList.BackColor = System.Drawing.Color.Black;
      this.ttl_PRCMessagesList.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PRCMessagesList.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_PRCMessagesList.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_PRCMessagesList.Location = new System.Drawing.Point(29, 171);
      this.ttl_PRCMessagesList.Name = "ttl_PRCMessagesList";
      this.ttl_PRCMessagesList.Size = new System.Drawing.Size(161, 285);
      this.ttl_PRCMessagesList.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_PRCMessagesList.TabIndex = 151;
      this.ttl_PRCMessagesList.TabStop = false;
      this.ttl_PRCMessagesList.Text = "Messages";
      this.ttl_PRCMessagesList.TranslateText = true;
      // 
      // pct_MessagePRC
      // 
      this.pct_MessagePRC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pct_MessagePRC.Location = new System.Drawing.Point(196, 195);
      this.pct_MessagePRC.Name = "pct_MessagePRC";
      this.pct_MessagePRC.Size = new System.Drawing.Size(1006, 453);
      this.pct_MessagePRC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_MessagePRC.TabIndex = 153;
      this.pct_MessagePRC.TabStop = false;
      // 
      // ttb_Comment
      // 
      this.ttb_Comment.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_Comment.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_Comment.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_Comment.ImagePassword = null;
      this.ttb_Comment.Location = new System.Drawing.Point(35, 480);
      this.ttb_Comment.MaxChar = 32767;
      this.ttb_Comment.Multiline = true;
      this.ttb_Comment.Name = "ttb_Comment";
      this.ttb_Comment.PasswordChar = '\0';
      this.ttb_Comment.ReadOnly = true;
      this.ttb_Comment.Size = new System.Drawing.Size(155, 224);
      this.ttb_Comment.Style = SyncroC.Controls.TitledTextBoxStyles.NoTitle;
      this.ttb_Comment.TabIndex = 154;
      this.ttb_Comment.Text = "Value";
      this.ttb_Comment.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_Comment.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Comment.TranslateText = false;
      this.ttb_Comment.Value = " ";
      this.ttb_Comment.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // bracket1
      // 
      this.bracket1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.bracket1.color = SyncroC.Controls.Colors.SyncroColor.Green;
      this.bracket1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bracket1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.bracket1.Location = new System.Drawing.Point(29, 456);
      this.bracket1.Name = "bracket1";
      this.bracket1.Position = SyncroC.Controls.BracketPosition.Bottom;
      this.bracket1.ShowArrow = false;
      this.bracket1.Size = new System.Drawing.Size(166, 273);
      this.bracket1.TabIndex = 155;
      this.bracket1.Text = "Comment";
      this.bracket1.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.bracket1.TranslateText = true;
      // 
      // ttb_QualityID
      // 
      this.ttb_QualityID.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_QualityID.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_QualityID.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_QualityID.ImagePassword = null;
      this.ttb_QualityID.ImeMode = System.Windows.Forms.ImeMode.On;
      this.ttb_QualityID.Location = new System.Drawing.Point(793, 648);
      this.ttb_QualityID.MaxChar = 30;
      this.ttb_QualityID.Multiline = false;
      this.ttb_QualityID.Name = "ttb_QualityID";
      this.ttb_QualityID.PasswordChar = '\0';
      this.ttb_QualityID.ReadOnly = true;
      this.ttb_QualityID.Size = new System.Drawing.Size(349, 56);
      this.ttb_QualityID.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_QualityID.TabIndex = 156;
      this.ttb_QualityID.TabStop = false;
      this.ttb_QualityID.Text = "Quality Code";
      this.ttb_QualityID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_QualityID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_QualityID.TranslateText = true;
      this.ttb_QualityID.Value = " ";
      this.ttb_QualityID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_ShiftManager
      // 
      this.ttb_ShiftManager.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_ShiftManager.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F);
      this.ttb_ShiftManager.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ShiftManager.ImagePassword = null;
      this.ttb_ShiftManager.Location = new System.Drawing.Point(258, 648);
      this.ttb_ShiftManager.MaxChar = 32767;
      this.ttb_ShiftManager.Multiline = false;
      this.ttb_ShiftManager.Name = "ttb_ShiftManager";
      this.ttb_ShiftManager.PasswordChar = '\0';
      this.ttb_ShiftManager.ReadOnly = true;
      this.ttb_ShiftManager.Size = new System.Drawing.Size(260, 56);
      this.ttb_ShiftManager.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_ShiftManager.TabIndex = 159;
      this.ttb_ShiftManager.Text = "Shift Manager";
      this.ttb_ShiftManager.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_ShiftManager.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ShiftManager.TranslateText = true;
      this.ttb_ShiftManager.Value = " ";
      this.ttb_ShiftManager.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_ShiftID
      // 
      this.ttb_ShiftID.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_ShiftID.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F);
      this.ttb_ShiftID.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ShiftID.ImagePassword = null;
      this.ttb_ShiftID.Location = new System.Drawing.Point(198, 648);
      this.ttb_ShiftID.MaxChar = 1;
      this.ttb_ShiftID.Multiline = false;
      this.ttb_ShiftID.Name = "ttb_ShiftID";
      this.ttb_ShiftID.PasswordChar = '\0';
      this.ttb_ShiftID.ReadOnly = true;
      this.ttb_ShiftID.Size = new System.Drawing.Size(60, 56);
      this.ttb_ShiftID.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_ShiftID.TabIndex = 158;
      this.ttb_ShiftID.Text = "Shift ID";
      this.ttb_ShiftID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_ShiftID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ShiftID.TranslateText = false;
      this.ttb_ShiftID.Value = " ";
      this.ttb_ShiftID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_fluteid
      // 
      this.ttb_fluteid.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_fluteid.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_fluteid.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_fluteid.ImagePassword = null;
      this.ttb_fluteid.ImeMode = System.Windows.Forms.ImeMode.On;
      this.ttb_fluteid.Location = new System.Drawing.Point(1142, 648);
      this.ttb_fluteid.MaxChar = 30;
      this.ttb_fluteid.Multiline = false;
      this.ttb_fluteid.Name = "ttb_fluteid";
      this.ttb_fluteid.PasswordChar = '\0';
      this.ttb_fluteid.ReadOnly = true;
      this.ttb_fluteid.Size = new System.Drawing.Size(60, 56);
      this.ttb_fluteid.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_fluteid.TabIndex = 160;
      this.ttb_fluteid.TabStop = false;
      this.ttb_fluteid.Text = "Flute Id";
      this.ttb_fluteid.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_fluteid.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_fluteid.TranslateText = true;
      this.ttb_fluteid.Value = " ";
      this.ttb_fluteid.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_setupcode
      // 
      this.ttb_setupcode.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_setupcode.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F);
      this.ttb_setupcode.ForeColor = System.Drawing.Color.Black;
      this.ttb_setupcode.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_setupcode.ImagePassword = null;
      this.ttb_setupcode.ImeMode = System.Windows.Forms.ImeMode.On;
      this.ttb_setupcode.Location = new System.Drawing.Point(518, 648);
      this.ttb_setupcode.MaxChar = 18;
      this.ttb_setupcode.Multiline = false;
      this.ttb_setupcode.Name = "ttb_setupcode";
      this.ttb_setupcode.PasswordChar = '\0';
      this.ttb_setupcode.ReadOnly = true;
      this.ttb_setupcode.Size = new System.Drawing.Size(275, 56);
      this.ttb_setupcode.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_setupcode.TabIndex = 161;
      this.ttb_setupcode.TabStop = false;
      this.ttb_setupcode.Text = "Setup Code";
      this.ttb_setupcode.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_setupcode.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_setupcode.TranslateText = true;
      this.ttb_setupcode.Value = " ";
      this.ttb_setupcode.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // lbl_bottomComment
      // 
      this.lbl_bottomComment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.lbl_bottomComment.Location = new System.Drawing.Point(35, 705);
      this.lbl_bottomComment.Name = "lbl_bottomComment";
      this.lbl_bottomComment.Size = new System.Drawing.Size(207, 24);
      this.lbl_bottomComment.TabIndex = 162;
      // 
      // printGrid
      // 
      this.printGrid.BackgroundImage = global::SyncroC.Properties.Resources.PrinterIcon;
      this.printGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.printGrid.Cursor = System.Windows.Forms.Cursors.Hand;
      this.printGrid.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.printGrid.Location = new System.Drawing.Point(1125, 172);
      this.printGrid.Name = "printGrid";
      this.printGrid.Size = new System.Drawing.Size(75, 23);
      this.printGrid.TabIndex = 171;
      this.printGrid.UseVisualStyleBackColor = true;
      this.printGrid.Click += new System.EventHandler(this.printGrid_Click);
      // 
      // btn_ShowGraph
      // 
      this.btn_ShowGraph.BackColor = System.Drawing.Color.White;
      this.btn_ShowGraph.BackgroundImage = global::SyncroC.Properties.Resources.record;
      this.btn_ShowGraph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.btn_ShowGraph.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_ShowGraph.Location = new System.Drawing.Point(1044, 172);
      this.btn_ShowGraph.Name = "btn_ShowGraph";
      this.btn_ShowGraph.Size = new System.Drawing.Size(75, 23);
      this.btn_ShowGraph.TabIndex = 476;
      this.btn_ShowGraph.UseVisualStyleBackColor = false;
      this.btn_ShowGraph.Visible = false;
      this.btn_ShowGraph.Click += new System.EventHandler(this.btn_ShowGraph_Click);
      // 
      // lbl_filter
      // 
      this.lbl_filter.BackcolorTesto = System.Drawing.Color.Transparent;
      this.lbl_filter.BackgroundImage = global::SyncroC.Properties.Resources.filter;
      this.lbl_filter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.lbl_filter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_filter.Colore1 = System.Drawing.Color.Red;
      this.lbl_filter.Colore2 = System.Drawing.Color.Red;
      this.lbl_filter.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lbl_filter.Font = new System.Drawing.Font("Arial Unicode MS", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_filter.Location = new System.Drawing.Point(145, 159);
      this.lbl_filter.Name = "lbl_filter";
      this.lbl_filter.OutLineText = true;
      this.lbl_filter.OutLineTextcolore = System.Drawing.Color.Black;
      this.lbl_filter.Size = new System.Drawing.Size(46, 32);
      this.lbl_filter.TabIndex = 477;
      this.lbl_filter.Testo = "!";
      this.lbl_filter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_filter.TranslateText = false;
      this.lbl_filter.Click += new System.EventHandler(this.lbl_filter_Click);
      // 
      // Frm_MessagePRC
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.lbl_filter);
      this.Controls.Add(this.btn_ShowGraph);
      this.Controls.Add(this.printGrid);
      this.Controls.Add(this.lbl_bottomComment);
      this.Controls.Add(this.ttb_setupcode);
      this.Controls.Add(this.ttb_fluteid);
      this.Controls.Add(this.ttb_ShiftManager);
      this.Controls.Add(this.ttb_QualityID);
      this.Controls.Add(this.ttb_ShiftID);
      this.Controls.Add(this.bracket1);
      this.Controls.Add(this.ttb_Comment);
      this.Controls.Add(this.pct_MessagePRC);
      this.Controls.Add(this.gdv_Messages);
      this.Controls.Add(this.brk_PRCMessageBitmap);
      this.Controls.Add(this.ttl_PRCMessagesList);
      this.Name = "Frm_MessagePRC";
      this.Text = "Frm_MessagePRC";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.ttl_PRCMessagesList, 0);
      this.Controls.SetChildIndex(this.brk_PRCMessageBitmap, 0);
      this.Controls.SetChildIndex(this.gdv_Messages, 0);
      this.Controls.SetChildIndex(this.pct_MessagePRC, 0);
      this.Controls.SetChildIndex(this.ttb_Comment, 0);
      this.Controls.SetChildIndex(this.bracket1, 0);
      this.Controls.SetChildIndex(this.ttb_ShiftID, 0);
      this.Controls.SetChildIndex(this.ttb_QualityID, 0);
      this.Controls.SetChildIndex(this.ttb_ShiftManager, 0);
      this.Controls.SetChildIndex(this.ttb_fluteid, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
      this.Controls.SetChildIndex(this.lbl_Date, 0);
      this.Controls.SetChildIndex(this.lbl_Time, 0);
      this.Controls.SetChildIndex(this.lbl_Status, 0);
      this.Controls.SetChildIndex(this.tab_Tab1, 0);
      this.Controls.SetChildIndex(this.tab_Tab2, 0);
      this.Controls.SetChildIndex(this.tab_Tab3, 0);
      this.Controls.SetChildIndex(this.tab_Tab4, 0);
      this.Controls.SetChildIndex(this.tab_Tab5, 0);
      this.Controls.SetChildIndex(this.tab_Tab6, 0);
      this.Controls.SetChildIndex(this.tab_Tab7, 0);
      this.Controls.SetChildIndex(this.tab_Tab8, 0);
      this.Controls.SetChildIndex(this.tab_TabA, 0);
      this.Controls.SetChildIndex(this.tab_TabD, 0);
      this.Controls.SetChildIndex(this.tab_TabC, 0);
      this.Controls.SetChildIndex(this.tab_TabB, 0);
      this.Controls.SetChildIndex(this.pct_Home_NY, 0);
      this.Controls.SetChildIndex(this.pct_Back_NY, 0);
      this.Controls.SetChildIndex(this.pct_Forward_NY, 0);
      this.Controls.SetChildIndex(this.lbl_Title_YN, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
      this.Controls.SetChildIndex(this.btn_Group1, 0);
      this.Controls.SetChildIndex(this.btn_Group2, 0);
      this.Controls.SetChildIndex(this.btn_Group3, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
      this.Controls.SetChildIndex(this.ttb_setupcode, 0);
      this.Controls.SetChildIndex(this.lbl_bottomComment, 0);
      this.Controls.SetChildIndex(this.printGrid, 0);
      this.Controls.SetChildIndex(this.btn_ShowGraph, 0);
      this.Controls.SetChildIndex(this.lbl_filter, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_MessagePRC)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private SyncroC.Controls.GridView gdv_Messages;
    private SyncroC.Controls.Bracket brk_PRCMessageBitmap;
    private SyncroC.Controls.Title ttl_PRCMessagesList;
    private System.Windows.Forms.PictureBox pct_MessagePRC;
    private SyncroC.Controls.TitledTextBox ttb_Comment;
    private SyncroC.Controls.Bracket bracket1;
    private SyncroC.Controls.TitledTextBox ttb_QualityID;
    private SyncroC.Controls.TitledTextBox ttb_ShiftManager;
    private SyncroC.Controls.TitledTextBox ttb_ShiftID;
    private SyncroC.Controls.TitledTextBox ttb_fluteid;
    private SyncroC.Controls.TitledTextBox ttb_setupcode;
    private System.Windows.Forms.Label lbl_bottomComment;
    private System.Windows.Forms.Button printGrid;
    private System.Windows.Forms.Button btn_ShowGraph;
    private SyncroC.Controls.myLabel lbl_filter;
  }
}