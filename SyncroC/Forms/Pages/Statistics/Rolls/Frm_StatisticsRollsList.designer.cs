namespace SyncroC.Forms.Pages.Statistics.Rolls
{
  partial class Frm_StatisticsRollsList
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_StatisticsRollsList));
      this.gdv_PastRolls = new SyncroC.Controls.GridView();
      this.lbl_PastRollsTitle = new SyncroC.Controls.Title();
      this.lbl_REMAINING_DIAMETER_UDdmm = new System.Windows.Forms.Label();
      this.lbl_filter = new SyncroC.Controls.myLabel();
      this.btn_ShowGraph = new System.Windows.Forms.Button();
      this.bracket1 = new SyncroC.Controls.Bracket();
      this.ttb_Description = new SyncroC.Controls.TitledTextBox();
      this.ttb_DataToPrint = new SyncroC.Controls.TitledTextBox();
      this.nud_RollDiameter = new SyncroC.Controls.TitledNumericUpDown();
      this.myListBox_Order = new SyncroC.Controls.GroupPanel.MyListBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // gdv_PastRolls
      // 
      this.gdv_PastRolls.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_PastRolls.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_PastRolls.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.AOCInProgressColor;
      this.gdv_PastRolls.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_PastRolls.AutoGenerateColumns = false;
      this.gdv_PastRolls.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_PastRolls.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
      this.gdv_PastRolls.DataMember = "";
      this.gdv_PastRolls.DataSource = null;
      this.gdv_PastRolls.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gdv_PastRolls.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_PastRolls.ImagePassword = null;
      this.gdv_PastRolls.Location = new System.Drawing.Point(40, 224);
      this.gdv_PastRolls.MultiSelect = false;
      this.gdv_PastRolls.Name = "gdv_PastRolls";
      this.gdv_PastRolls.RowsHeight = 32;
      this.gdv_PastRolls.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_PastRolls.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.AOCInProgressColor;
      this.gdv_PastRolls.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_PastRolls.Size = new System.Drawing.Size(1160, 330);
      this.gdv_PastRolls.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_PastRolls.TabIndex = 201;
      this.gdv_PastRolls.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_PastRolls.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_PastRolls.TranslateText = true;
      this.gdv_PastRolls.SelectionChanged += new System.EventHandler(this.gdv_PastRolls_SelectionChanged);
      this.gdv_PastRolls.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_PastRolls_CellFormatting);
      // 
      // lbl_PastRollsTitle
      // 
      this.lbl_PastRollsTitle.BackColor = System.Drawing.Color.Black;
      this.lbl_PastRollsTitle.CausesValidation = false;
      this.lbl_PastRollsTitle.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.lbl_PastRollsTitle.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_PastRollsTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_PastRollsTitle.Location = new System.Drawing.Point(30, 193);
      this.lbl_PastRollsTitle.Name = "lbl_PastRollsTitle";
      this.lbl_PastRollsTitle.Size = new System.Drawing.Size(1170, 362);
      this.lbl_PastRollsTitle.Style = SyncroC.Controls.TitleStyles.Big;
      this.lbl_PastRollsTitle.TabIndex = 200;
      this.lbl_PastRollsTitle.Text = "Past Rolls List";
      this.lbl_PastRollsTitle.TranslateText = true;
      // 
      // lbl_REMAINING_DIAMETER_UDdmm
      // 
      this.lbl_REMAINING_DIAMETER_UDdmm.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_REMAINING_DIAMETER_UDdmm.CausesValidation = false;
      this.lbl_REMAINING_DIAMETER_UDdmm.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_REMAINING_DIAMETER_UDdmm.Location = new System.Drawing.Point(630, 171);
      this.lbl_REMAINING_DIAMETER_UDdmm.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
      this.lbl_REMAINING_DIAMETER_UDdmm.Name = "lbl_REMAINING_DIAMETER_UDdmm";
      this.lbl_REMAINING_DIAMETER_UDdmm.Size = new System.Drawing.Size(366, 18);
      this.lbl_REMAINING_DIAMETER_UDdmm.TabIndex = 202;
      this.lbl_REMAINING_DIAMETER_UDdmm.Text = "REMAINING_DIAMETER_UDdmm";
      this.lbl_REMAINING_DIAMETER_UDdmm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_REMAINING_DIAMETER_UDdmm.Visible = false;
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
      this.lbl_filter.Location = new System.Drawing.Point(362, 171);
      this.lbl_filter.Name = "lbl_filter";
      this.lbl_filter.OutLineText = true;
      this.lbl_filter.OutLineTextcolore = System.Drawing.Color.Black;
      this.lbl_filter.Size = new System.Drawing.Size(75, 45);
      this.lbl_filter.TabIndex = 270;
      this.lbl_filter.Testo = "!";
      this.lbl_filter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_filter.TranslateText = false;
      this.lbl_filter.Visible = false;
      this.lbl_filter.Click += new System.EventHandler(this.lbl_filter_Click);
      // 
      // btn_ShowGraph
      // 
      this.btn_ShowGraph.BackColor = System.Drawing.Color.White;
      this.btn_ShowGraph.BackgroundImage = global::SyncroC.Properties.Resources.record;
      this.btn_ShowGraph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.btn_ShowGraph.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_ShowGraph.Location = new System.Drawing.Point(1131, 559);
      this.btn_ShowGraph.Name = "btn_ShowGraph";
      this.btn_ShowGraph.Size = new System.Drawing.Size(65, 25);
      this.btn_ShowGraph.TabIndex = 475;
      this.btn_ShowGraph.UseVisualStyleBackColor = false;
      this.btn_ShowGraph.Visible = false;
      this.btn_ShowGraph.Click += new System.EventHandler(this.btn_ShowGraph_Click);
      // 
      // bracket1
      // 
      this.bracket1.BackColor = System.Drawing.Color.LightBlue;
      this.bracket1.color = SyncroC.Controls.Colors.SyncroColor.AOCInProgressColor;
      this.bracket1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bracket1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.bracket1.Location = new System.Drawing.Point(30, 560);
      this.bracket1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
      this.bracket1.Name = "bracket1";
      this.bracket1.Position = SyncroC.Controls.BracketPosition.Bottom;
      this.bracket1.ShowArrow = false;
      this.bracket1.Size = new System.Drawing.Size(1171, 154);
      this.bracket1.TabIndex = 476;
      this.bracket1.TabStop = false;
      this.bracket1.Text = "Details";
      this.bracket1.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.bracket1.TranslateText = true;
      // 
      // ttb_Description
      // 
      this.ttb_Description.AutoScroll = true;
      this.ttb_Description.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_Description.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.ttb_Description.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Description.ImagePassword = null;
      this.ttb_Description.Location = new System.Drawing.Point(235, 588);
      this.ttb_Description.MaxChar = 500;
      this.ttb_Description.Multiline = false;
      this.ttb_Description.Name = "ttb_Description";
      this.ttb_Description.PasswordChar = '\0';
      this.ttb_Description.ReadOnly = true;
      this.ttb_Description.Size = new System.Drawing.Size(838, 56);
      this.ttb_Description.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_Description.TabIndex = 477;
      this.ttb_Description.TabStop = false;
      this.ttb_Description.Text = "Description";
      this.ttb_Description.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_Description.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Description.TranslateText = true;
      this.ttb_Description.Value = "Produzione Locale";
      this.ttb_Description.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_DataToPrint
      // 
      this.ttb_DataToPrint.AutoScroll = true;
      this.ttb_DataToPrint.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_DataToPrint.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.ttb_DataToPrint.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint.ImagePassword = null;
      this.ttb_DataToPrint.Location = new System.Drawing.Point(235, 650);
      this.ttb_DataToPrint.MaxChar = 500;
      this.ttb_DataToPrint.Multiline = false;
      this.ttb_DataToPrint.Name = "ttb_DataToPrint";
      this.ttb_DataToPrint.PasswordChar = '\0';
      this.ttb_DataToPrint.ReadOnly = true;
      this.ttb_DataToPrint.Size = new System.Drawing.Size(954, 56);
      this.ttb_DataToPrint.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_DataToPrint.TabIndex = 478;
      this.ttb_DataToPrint.TabStop = false;
      this.ttb_DataToPrint.Text = "Data to Print";
      this.ttb_DataToPrint.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_DataToPrint.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint.TranslateText = true;
      this.ttb_DataToPrint.Value = "prodotta male";
      this.ttb_DataToPrint.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // nud_RollDiameter
      // 
      this.nud_RollDiameter.DecimalPlaces = 0;
      this.nud_RollDiameter.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_RollDiameter.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_RollDiameter.ImagePassword = null;
      this.nud_RollDiameter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_RollDiameter.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_RollDiameter.Location = new System.Drawing.Point(1079, 587);
      this.nud_RollDiameter.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_RollDiameter.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_RollDiameter.Name = "nud_RollDiameter";
      this.nud_RollDiameter.ReadOnly = true;
      this.nud_RollDiameter.Size = new System.Drawing.Size(113, 60);
      this.nud_RollDiameter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_RollDiameter.TabIndex = 481;
      this.nud_RollDiameter.Text = "Roll Diameter";
      this.nud_RollDiameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_RollDiameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_RollDiameter.TranslateText = true;
      this.nud_RollDiameter.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_RollDiameter.Value = "25000";
      this.nud_RollDiameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_RollDiameter.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // myListBox_Order
      // 
      this.myListBox_Order.BackColor = System.Drawing.Color.LightSkyBlue;
      this.myListBox_Order.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.myListBox_Order.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.myListBox_Order.Location = new System.Drawing.Point(40, 588);
      this.myListBox_Order.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.myListBox_Order.Name = "myListBox_Order";
      this.myListBox_Order.Size = new System.Drawing.Size(188, 126);
      this.myListBox_Order.TabIndex = 482;
      this.myListBox_Order.Visible = false;
      // 
      // Frm_StatisticsRollsList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.myListBox_Order);
      this.Controls.Add(this.btn_ShowGraph);
      this.Controls.Add(this.nud_RollDiameter);
      this.Controls.Add(this.ttb_DataToPrint);
      this.Controls.Add(this.ttb_Description);
      this.Controls.Add(this.bracket1);
      this.Controls.Add(this.lbl_filter);
      this.Controls.Add(this.lbl_REMAINING_DIAMETER_UDdmm);
      this.Controls.Add(this.gdv_PastRolls);
      this.Controls.Add(this.lbl_PastRollsTitle);
      this.Name = "Frm_StatisticsRollsList";
      this.Text = "Frm_PastRolls";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
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
      this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
      this.Controls.SetChildIndex(this.btn_Group1, 0);
      this.Controls.SetChildIndex(this.btn_Group2, 0);
      this.Controls.SetChildIndex(this.btn_Group3, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      this.Controls.SetChildIndex(this.lbl_PastRollsTitle, 0);
      this.Controls.SetChildIndex(this.gdv_PastRolls, 0);
      this.Controls.SetChildIndex(this.lbl_REMAINING_DIAMETER_UDdmm, 0);
      this.Controls.SetChildIndex(this.lbl_filter, 0);
      this.Controls.SetChildIndex(this.bracket1, 0);
      this.Controls.SetChildIndex(this.ttb_Description, 0);
      this.Controls.SetChildIndex(this.ttb_DataToPrint, 0);
      this.Controls.SetChildIndex(this.nud_RollDiameter, 0);
      this.Controls.SetChildIndex(this.btn_ShowGraph, 0);
      this.Controls.SetChildIndex(this.myListBox_Order, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}

		#endregion

		private SyncroC.Controls.GridView gdv_PastRolls;
		private SyncroC.Controls.Title lbl_PastRollsTitle;
		private System.Windows.Forms.Label lbl_REMAINING_DIAMETER_UDdmm;
    private SyncroC.Controls.myLabel lbl_filter;
    private System.Windows.Forms.Button btn_ShowGraph;
    private SyncroC.Controls.Bracket bracket1;
    private SyncroC.Controls.TitledTextBox ttb_Description;
    private SyncroC.Controls.TitledTextBox ttb_DataToPrint;
    private SyncroC.Controls.TitledNumericUpDown nud_RollDiameter;
    private SyncroC.Controls.GroupPanel.MyListBox myListBox_Order;
	}
}