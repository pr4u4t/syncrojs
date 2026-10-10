namespace SyncroC.Forms.Pages.Statistics.Orders
{
	partial class Frm_StatisticsOrdersList
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
			this.gdv_Orders.DataSource = null;
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_StatisticsOrdersList));
      this.lbl_Customer1 = new System.Windows.Forms.Label();
      this.lbl_Customer0 = new System.Windows.Forms.Label();
      this.lbl_Customer2 = new System.Windows.Forms.Label();
      this.tlb_bad2 = new SyncroC.Controls.TitledLabel();
      this.tlb_bad1 = new SyncroC.Controls.TitledLabel();
      this.tlb_bad0 = new SyncroC.Controls.TitledLabel();
      this.tlb_produced2 = new SyncroC.Controls.TitledLabel();
      this.tlb_desired2 = new SyncroC.Controls.TitledLabel();
      this.tlb_produced1 = new SyncroC.Controls.TitledLabel();
      this.tlb_desired1 = new SyncroC.Controls.TitledLabel();
      this.tlb_produced0 = new SyncroC.Controls.TitledLabel();
      this.tlb_desired0 = new SyncroC.Controls.TitledLabel();
      this.lbl_Misc1 = new System.Windows.Forms.Label();
      this.lbl_Misc2 = new System.Windows.Forms.Label();
      this.lbl_Misc0 = new System.Windows.Forms.Label();
      this.lbl_ModID = new System.Windows.Forms.Label();
      this.lbl_SetupID = new System.Windows.Forms.Label();
      this.bracket1 = new SyncroC.Controls.Bracket();
      this.ttl_Order0 = new SyncroC.Controls.Title();
      this.gdv_Orders = new SyncroC.Controls.GridView();
      this.lbl_DryEndTitle = new SyncroC.Controls.Title();
      this.ttl_Order2 = new SyncroC.Controls.Title();
      this.ttl_Order1 = new SyncroC.Controls.Title();
      this.pct_OrderPreprint1 = new System.Windows.Forms.PictureBox();
      this.pct_OrderPreprint2 = new System.Windows.Forms.PictureBox();
      this.pct_OrderPreprint0 = new System.Windows.Forms.PictureBox();
      this.lbl_filter = new SyncroC.Controls.myLabel();
      this.btn_ShowGraph = new System.Windows.Forms.Button();
      this.nud_ScrapDischargeGroup0 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_ScrapDischargeGroup1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_ScrapDischargeGroup2 = new SyncroC.Controls.TitledNumericUpDown();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint0)).BeginInit();
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
      // lbl_Customer1
      // 
      this.lbl_Customer1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Customer1.CausesValidation = false;
      this.lbl_Customer1.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Customer1.Location = new System.Drawing.Point(432, 588);
      this.lbl_Customer1.Name = "lbl_Customer1";
      this.lbl_Customer1.Size = new System.Drawing.Size(370, 30);
      this.lbl_Customer1.TabIndex = 240;
      this.lbl_Customer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_Customer0
      // 
      this.lbl_Customer0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Customer0.CausesValidation = false;
      this.lbl_Customer0.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Customer0.Location = new System.Drawing.Point(48, 588);
      this.lbl_Customer0.Name = "lbl_Customer0";
      this.lbl_Customer0.Size = new System.Drawing.Size(370, 30);
      this.lbl_Customer0.TabIndex = 239;
      this.lbl_Customer0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_Customer2
      // 
      this.lbl_Customer2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Customer2.CausesValidation = false;
      this.lbl_Customer2.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Customer2.Location = new System.Drawing.Point(821, 588);
      this.lbl_Customer2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.lbl_Customer2.Name = "lbl_Customer2";
      this.lbl_Customer2.Size = new System.Drawing.Size(370, 30);
      this.lbl_Customer2.TabIndex = 241;
      this.lbl_Customer2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // tlb_bad2
      // 
      this.tlb_bad2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_bad2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_bad2.ImagePassword = null;
      this.tlb_bad2.Location = new System.Drawing.Point(1083, 662);
      this.tlb_bad2.Name = "tlb_bad2";
      this.tlb_bad2.Size = new System.Drawing.Size(110, 50);
      this.tlb_bad2.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_bad2.TabIndex = 238;
      this.tlb_bad2.Text = "Bad + Default";
      this.tlb_bad2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_bad2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_bad2.TranslateText = true;
      this.tlb_bad2.Value = "";
      this.tlb_bad2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_bad2.ValueMultiline = true;
      // 
      // tlb_bad1
      // 
      this.tlb_bad1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_bad1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_bad1.ImagePassword = null;
      this.tlb_bad1.Location = new System.Drawing.Point(691, 662);
      this.tlb_bad1.Name = "tlb_bad1";
      this.tlb_bad1.Size = new System.Drawing.Size(110, 50);
      this.tlb_bad1.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_bad1.TabIndex = 237;
      this.tlb_bad1.Text = "Bad + Default";
      this.tlb_bad1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_bad1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_bad1.TranslateText = true;
      this.tlb_bad1.Value = "";
      this.tlb_bad1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_bad1.ValueMultiline = true;
      // 
      // tlb_bad0
      // 
      this.tlb_bad0.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_bad0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_bad0.ImagePassword = null;
      this.tlb_bad0.Location = new System.Drawing.Point(309, 662);
      this.tlb_bad0.Name = "tlb_bad0";
      this.tlb_bad0.Size = new System.Drawing.Size(110, 50);
      this.tlb_bad0.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_bad0.TabIndex = 236;
      this.tlb_bad0.Text = "Bad + Default";
      this.tlb_bad0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_bad0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_bad0.TranslateText = true;
      this.tlb_bad0.Value = "";
      this.tlb_bad0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_bad0.ValueMultiline = true;
      // 
      // tlb_produced2
      // 
      this.tlb_produced2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_produced2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_produced2.ImagePassword = null;
      this.tlb_produced2.Location = new System.Drawing.Point(959, 662);
      this.tlb_produced2.Name = "tlb_produced2";
      this.tlb_produced2.Size = new System.Drawing.Size(110, 50);
      this.tlb_produced2.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_produced2.TabIndex = 235;
      this.tlb_produced2.Text = "Produced";
      this.tlb_produced2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_produced2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_produced2.TranslateText = true;
      this.tlb_produced2.Value = "";
      this.tlb_produced2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_produced2.ValueMultiline = true;
      // 
      // tlb_desired2
      // 
      this.tlb_desired2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_desired2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_desired2.ImagePassword = null;
      this.tlb_desired2.Location = new System.Drawing.Point(826, 662);
      this.tlb_desired2.Name = "tlb_desired2";
      this.tlb_desired2.Size = new System.Drawing.Size(110, 50);
      this.tlb_desired2.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_desired2.TabIndex = 234;
      this.tlb_desired2.Text = "Desired";
      this.tlb_desired2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_desired2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_desired2.TranslateText = true;
      this.tlb_desired2.Value = "";
      this.tlb_desired2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_desired2.ValueMultiline = true;
      // 
      // tlb_produced1
      // 
      this.tlb_produced1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_produced1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_produced1.ImagePassword = null;
      this.tlb_produced1.Location = new System.Drawing.Point(571, 662);
      this.tlb_produced1.Name = "tlb_produced1";
      this.tlb_produced1.Size = new System.Drawing.Size(110, 50);
      this.tlb_produced1.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_produced1.TabIndex = 233;
      this.tlb_produced1.Text = "Produced";
      this.tlb_produced1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_produced1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_produced1.TranslateText = true;
      this.tlb_produced1.Value = "";
      this.tlb_produced1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_produced1.ValueMultiline = true;
      // 
      // tlb_desired1
      // 
      this.tlb_desired1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_desired1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_desired1.ImagePassword = null;
      this.tlb_desired1.Location = new System.Drawing.Point(438, 662);
      this.tlb_desired1.Name = "tlb_desired1";
      this.tlb_desired1.Size = new System.Drawing.Size(110, 50);
      this.tlb_desired1.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_desired1.TabIndex = 232;
      this.tlb_desired1.Text = "Desired";
      this.tlb_desired1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_desired1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_desired1.TranslateText = true;
      this.tlb_desired1.Value = "";
      this.tlb_desired1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_desired1.ValueMultiline = true;
      // 
      // tlb_produced0
      // 
      this.tlb_produced0.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_produced0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_produced0.ImagePassword = null;
      this.tlb_produced0.Location = new System.Drawing.Point(180, 662);
      this.tlb_produced0.Name = "tlb_produced0";
      this.tlb_produced0.Size = new System.Drawing.Size(110, 50);
      this.tlb_produced0.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_produced0.TabIndex = 231;
      this.tlb_produced0.Text = "Produced";
      this.tlb_produced0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_produced0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_produced0.TranslateText = true;
      this.tlb_produced0.Value = "";
      this.tlb_produced0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_produced0.ValueMultiline = true;
      // 
      // tlb_desired0
      // 
      this.tlb_desired0.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_desired0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_desired0.ImagePassword = null;
      this.tlb_desired0.Location = new System.Drawing.Point(55, 662);
      this.tlb_desired0.Name = "tlb_desired0";
      this.tlb_desired0.Size = new System.Drawing.Size(110, 50);
      this.tlb_desired0.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_desired0.TabIndex = 230;
      this.tlb_desired0.Text = "Desired";
      this.tlb_desired0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_desired0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_desired0.TranslateText = true;
      this.tlb_desired0.Value = "";
      this.tlb_desired0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_desired0.ValueMultiline = true;
      // 
      // lbl_Misc1
      // 
      this.lbl_Misc1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Misc1.CausesValidation = false;
      this.lbl_Misc1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Misc1.Location = new System.Drawing.Point(438, 627);
      this.lbl_Misc1.Name = "lbl_Misc1";
      this.lbl_Misc1.Size = new System.Drawing.Size(365, 28);
      this.lbl_Misc1.TabIndex = 229;
      this.lbl_Misc1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_Misc2
      // 
      this.lbl_Misc2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Misc2.CausesValidation = false;
      this.lbl_Misc2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Misc2.Location = new System.Drawing.Point(826, 627);
      this.lbl_Misc2.Name = "lbl_Misc2";
      this.lbl_Misc2.Size = new System.Drawing.Size(365, 28);
      this.lbl_Misc2.TabIndex = 228;
      this.lbl_Misc2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_Misc0
      // 
      this.lbl_Misc0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Misc0.CausesValidation = false;
      this.lbl_Misc0.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Misc0.Location = new System.Drawing.Point(52, 627);
      this.lbl_Misc0.Name = "lbl_Misc0";
      this.lbl_Misc0.Size = new System.Drawing.Size(365, 28);
      this.lbl_Misc0.TabIndex = 227;
      this.lbl_Misc0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_ModID
      // 
      this.lbl_ModID.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_ModID.CausesValidation = false;
      this.lbl_ModID.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ModID.Location = new System.Drawing.Point(1078, 171);
      this.lbl_ModID.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
      this.lbl_ModID.Name = "lbl_ModID";
      this.lbl_ModID.Size = new System.Drawing.Size(124, 18);
      this.lbl_ModID.TabIndex = 226;
      this.lbl_ModID.Text = "ModID";
      this.lbl_ModID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_ModID.Visible = false;
      // 
      // lbl_SetupID
      // 
      this.lbl_SetupID.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_SetupID.CausesValidation = false;
      this.lbl_SetupID.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SetupID.Location = new System.Drawing.Point(937, 171);
      this.lbl_SetupID.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
      this.lbl_SetupID.Name = "lbl_SetupID";
      this.lbl_SetupID.Size = new System.Drawing.Size(124, 18);
      this.lbl_SetupID.TabIndex = 225;
      this.lbl_SetupID.Text = "SetUpID";
      this.lbl_SetupID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_SetupID.Visible = false;
      // 
      // bracket1
      // 
      this.bracket1.BackColor = System.Drawing.Color.LightBlue;
      this.bracket1.color = SyncroC.Controls.Colors.SyncroColor.AOCInProgressColor;
      this.bracket1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bracket1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.bracket1.Location = new System.Drawing.Point(29, 533);
      this.bracket1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
      this.bracket1.Name = "bracket1";
      this.bracket1.Position = SyncroC.Controls.BracketPosition.Bottom;
      this.bracket1.ShowArrow = false;
      this.bracket1.Size = new System.Drawing.Size(1172, 179);
      this.bracket1.TabIndex = 224;
      this.bracket1.TabStop = false;
      this.bracket1.Text = "Details";
      this.bracket1.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.bracket1.TranslateText = true;
      // 
      // ttl_Order0
      // 
      this.ttl_Order0.BackColor = System.Drawing.Color.Black;
      this.ttl_Order0.CausesValidation = false;
      this.ttl_Order0.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Order0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Order0.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Order0.Location = new System.Drawing.Point(39, 563);
      this.ttl_Order0.Name = "ttl_Order0";
      this.ttl_Order0.Size = new System.Drawing.Size(380, 149);
      this.ttl_Order0.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_Order0.TabIndex = 223;
      this.ttl_Order0.TranslateText = false;
      // 
      // gdv_Orders
      // 
      this.gdv_Orders.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_Orders.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Orders.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.AOCInProgressColor;
      this.gdv_Orders.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Orders.AutoGenerateColumns = false;
      this.gdv_Orders.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_Orders.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
      this.gdv_Orders.DataMember = "";
      this.gdv_Orders.DataSource = null;
      this.gdv_Orders.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gdv_Orders.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Orders.ImagePassword = null;
      this.gdv_Orders.Location = new System.Drawing.Point(39, 224);
      this.gdv_Orders.MultiSelect = false;
      this.gdv_Orders.Name = "gdv_Orders";
      this.gdv_Orders.RowsHeight = 32;
      this.gdv_Orders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_Orders.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.AOCInProgressColor;
      this.gdv_Orders.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Orders.Size = new System.Drawing.Size(1162, 302);
      this.gdv_Orders.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_Orders.TabIndex = 222;
      this.gdv_Orders.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Orders.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Orders.TranslateText = true;
      this.gdv_Orders.DoubleClick += new System.EventHandler(this.gdv_Orders_DoubleClick);
      this.gdv_Orders.SelectionChanged += new System.EventHandler(this.gdv_Orders_SelectionChanged);
      this.gdv_Orders.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_Orders_CellFormatting);
      // 
      // lbl_DryEndTitle
      // 
      this.lbl_DryEndTitle.BackColor = System.Drawing.Color.Black;
      this.lbl_DryEndTitle.CausesValidation = false;
      this.lbl_DryEndTitle.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.lbl_DryEndTitle.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DryEndTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DryEndTitle.Location = new System.Drawing.Point(29, 193);
      this.lbl_DryEndTitle.Name = "lbl_DryEndTitle";
      this.lbl_DryEndTitle.Size = new System.Drawing.Size(1172, 333);
      this.lbl_DryEndTitle.Style = SyncroC.Controls.TitleStyles.Big;
      this.lbl_DryEndTitle.TabIndex = 221;
      this.lbl_DryEndTitle.Text = "Dry End Orders List";
      this.lbl_DryEndTitle.TranslateText = true;
      // 
      // ttl_Order2
      // 
      this.ttl_Order2.BackColor = System.Drawing.Color.Black;
      this.ttl_Order2.CausesValidation = false;
      this.ttl_Order2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Order2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Order2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Order2.Location = new System.Drawing.Point(813, 563);
      this.ttl_Order2.Name = "ttl_Order2";
      this.ttl_Order2.Size = new System.Drawing.Size(380, 149);
      this.ttl_Order2.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_Order2.TabIndex = 220;
      this.ttl_Order2.TranslateText = false;
      // 
      // ttl_Order1
      // 
      this.ttl_Order1.BackColor = System.Drawing.Color.Black;
      this.ttl_Order1.CausesValidation = false;
      this.ttl_Order1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Order1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Order1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Order1.Location = new System.Drawing.Point(426, 563);
      this.ttl_Order1.Name = "ttl_Order1";
      this.ttl_Order1.Size = new System.Drawing.Size(380, 149);
      this.ttl_Order1.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_Order1.TabIndex = 219;
      this.ttl_Order1.TranslateText = false;
      // 
      // pct_OrderPreprint1
      // 
      this.pct_OrderPreprint1.Image = ((System.Drawing.Image)(resources.GetObject("pct_OrderPreprint1.Image")));
      this.pct_OrderPreprint1.Location = new System.Drawing.Point(771, 566);
      this.pct_OrderPreprint1.Name = "pct_OrderPreprint1";
      this.pct_OrderPreprint1.Size = new System.Drawing.Size(30, 18);
      this.pct_OrderPreprint1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_OrderPreprint1.TabIndex = 268;
      this.pct_OrderPreprint1.TabStop = false;
      this.pct_OrderPreprint1.Visible = false;
      // 
      // pct_OrderPreprint2
      // 
      this.pct_OrderPreprint2.Image = ((System.Drawing.Image)(resources.GetObject("pct_OrderPreprint2.Image")));
      this.pct_OrderPreprint2.Location = new System.Drawing.Point(1160, 566);
      this.pct_OrderPreprint2.Name = "pct_OrderPreprint2";
      this.pct_OrderPreprint2.Size = new System.Drawing.Size(30, 18);
      this.pct_OrderPreprint2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_OrderPreprint2.TabIndex = 267;
      this.pct_OrderPreprint2.TabStop = false;
      this.pct_OrderPreprint2.Visible = false;
      // 
      // pct_OrderPreprint0
      // 
      this.pct_OrderPreprint0.Image = ((System.Drawing.Image)(resources.GetObject("pct_OrderPreprint0.Image")));
      this.pct_OrderPreprint0.Location = new System.Drawing.Point(385, 566);
      this.pct_OrderPreprint0.Name = "pct_OrderPreprint0";
      this.pct_OrderPreprint0.Size = new System.Drawing.Size(30, 18);
      this.pct_OrderPreprint0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_OrderPreprint0.TabIndex = 266;
      this.pct_OrderPreprint0.TabStop = false;
      this.pct_OrderPreprint0.Visible = false;
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
      this.lbl_filter.TabIndex = 269;
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
      this.btn_ShowGraph.Location = new System.Drawing.Point(1130, 533);
      this.btn_ShowGraph.Name = "btn_ShowGraph";
      this.btn_ShowGraph.Size = new System.Drawing.Size(65, 25);
      this.btn_ShowGraph.TabIndex = 474;
      this.btn_ShowGraph.UseVisualStyleBackColor = false;
      this.btn_ShowGraph.Visible = false;
      this.btn_ShowGraph.Click += new System.EventHandler(this.btn_ShowGraph_Click);
      // 
      // nud_ScrapDischargeGroup0
      // 
      this.nud_ScrapDischargeGroup0.DecimalPlaces = 0;
      this.nud_ScrapDischargeGroup0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ScrapDischargeGroup0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScrapDischargeGroup0.ImagePassword = null;
      this.nud_ScrapDischargeGroup0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup0.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_ScrapDischargeGroup0.Location = new System.Drawing.Point(309, 662);
      this.nud_ScrapDischargeGroup0.Margin = new System.Windows.Forms.Padding(2, 2, 2, 1);
      this.nud_ScrapDischargeGroup0.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup0.Name = "nud_ScrapDischargeGroup0";
      this.nud_ScrapDischargeGroup0.ReadOnly = true;
      this.nud_ScrapDischargeGroup0.Size = new System.Drawing.Size(110, 50);
      this.nud_ScrapDischargeGroup0.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_ScrapDischargeGroup0.TabIndex = 478;
      this.nud_ScrapDischargeGroup0.TabStop = false;
      this.nud_ScrapDischargeGroup0.Text = "Scrap Group";
      this.nud_ScrapDischargeGroup0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScrapDischargeGroup0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScrapDischargeGroup0.TranslateText = true;
      this.nud_ScrapDischargeGroup0.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_ScrapDischargeGroup0.Value = "11500";
      this.nud_ScrapDischargeGroup0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScrapDischargeGroup0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_ScrapDischargeGroup0.Visible = false;
      // 
      // nud_ScrapDischargeGroup1
      // 
      this.nud_ScrapDischargeGroup1.DecimalPlaces = 0;
      this.nud_ScrapDischargeGroup1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ScrapDischargeGroup1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScrapDischargeGroup1.ImagePassword = null;
      this.nud_ScrapDischargeGroup1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_ScrapDischargeGroup1.Location = new System.Drawing.Point(692, 662);
      this.nud_ScrapDischargeGroup1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 1);
      this.nud_ScrapDischargeGroup1.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup1.Name = "nud_ScrapDischargeGroup1";
      this.nud_ScrapDischargeGroup1.ReadOnly = true;
      this.nud_ScrapDischargeGroup1.Size = new System.Drawing.Size(110, 50);
      this.nud_ScrapDischargeGroup1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_ScrapDischargeGroup1.TabIndex = 479;
      this.nud_ScrapDischargeGroup1.TabStop = false;
      this.nud_ScrapDischargeGroup1.Text = "Scrap Group";
      this.nud_ScrapDischargeGroup1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScrapDischargeGroup1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScrapDischargeGroup1.TranslateText = true;
      this.nud_ScrapDischargeGroup1.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_ScrapDischargeGroup1.Value = "11500";
      this.nud_ScrapDischargeGroup1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScrapDischargeGroup1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_ScrapDischargeGroup1.Visible = false;
      // 
      // nud_ScrapDischargeGroup2
      // 
      this.nud_ScrapDischargeGroup2.DecimalPlaces = 0;
      this.nud_ScrapDischargeGroup2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ScrapDischargeGroup2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScrapDischargeGroup2.ImagePassword = null;
      this.nud_ScrapDischargeGroup2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_ScrapDischargeGroup2.Location = new System.Drawing.Point(1083, 662);
      this.nud_ScrapDischargeGroup2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 1);
      this.nud_ScrapDischargeGroup2.Maximum = new decimal(new int[] {
            450000,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ScrapDischargeGroup2.Name = "nud_ScrapDischargeGroup2";
      this.nud_ScrapDischargeGroup2.ReadOnly = true;
      this.nud_ScrapDischargeGroup2.Size = new System.Drawing.Size(110, 50);
      this.nud_ScrapDischargeGroup2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_ScrapDischargeGroup2.TabIndex = 480;
      this.nud_ScrapDischargeGroup2.TabStop = false;
      this.nud_ScrapDischargeGroup2.Text = "Scrap Group";
      this.nud_ScrapDischargeGroup2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScrapDischargeGroup2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScrapDischargeGroup2.TranslateText = true;
      this.nud_ScrapDischargeGroup2.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_ScrapDischargeGroup2.Value = "11500";
      this.nud_ScrapDischargeGroup2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScrapDischargeGroup2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_ScrapDischargeGroup2.Visible = false;
      // 
      // Frm_StatisticsOrdersList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.btn_ShowGraph);
      this.Controls.Add(this.lbl_filter);
      this.Controls.Add(this.pct_OrderPreprint1);
      this.Controls.Add(this.pct_OrderPreprint2);
      this.Controls.Add(this.pct_OrderPreprint0);
      this.Controls.Add(this.lbl_Customer1);
      this.Controls.Add(this.lbl_Customer0);
      this.Controls.Add(this.lbl_Customer2);
      this.Controls.Add(this.tlb_produced2);
      this.Controls.Add(this.tlb_desired2);
      this.Controls.Add(this.tlb_produced1);
      this.Controls.Add(this.tlb_desired1);
      this.Controls.Add(this.tlb_produced0);
      this.Controls.Add(this.tlb_desired0);
      this.Controls.Add(this.lbl_Misc1);
      this.Controls.Add(this.lbl_Misc2);
      this.Controls.Add(this.lbl_Misc0);
      this.Controls.Add(this.lbl_ModID);
      this.Controls.Add(this.lbl_SetupID);
      this.Controls.Add(this.bracket1);
      this.Controls.Add(this.ttl_Order0);
      this.Controls.Add(this.gdv_Orders);
      this.Controls.Add(this.lbl_DryEndTitle);
      this.Controls.Add(this.ttl_Order2);
      this.Controls.Add(this.ttl_Order1);
      this.Controls.Add(this.tlb_bad1);
      this.Controls.Add(this.nud_ScrapDischargeGroup1);
      this.Controls.Add(this.tlb_bad2);
      this.Controls.Add(this.nud_ScrapDischargeGroup2);
      this.Controls.Add(this.tlb_bad0);
      this.Controls.Add(this.nud_ScrapDischargeGroup0);
      this.Name = "Frm_StatisticsOrdersList";
      this.Text = "Frm_StatisticsOrdersList";
      this.Load += new System.EventHandler(this.Frm_OrdersList_Load);
      this.Controls.SetChildIndex(this.nud_ScrapDischargeGroup0, 0);
      this.Controls.SetChildIndex(this.tlb_bad0, 0);
      this.Controls.SetChildIndex(this.nud_ScrapDischargeGroup2, 0);
      this.Controls.SetChildIndex(this.tlb_bad2, 0);
      this.Controls.SetChildIndex(this.nud_ScrapDischargeGroup1, 0);
      this.Controls.SetChildIndex(this.tlb_bad1, 0);
      this.Controls.SetChildIndex(this.ttl_Order1, 0);
      this.Controls.SetChildIndex(this.ttl_Order2, 0);
      this.Controls.SetChildIndex(this.lbl_DryEndTitle, 0);
      this.Controls.SetChildIndex(this.gdv_Orders, 0);
      this.Controls.SetChildIndex(this.ttl_Order0, 0);
      this.Controls.SetChildIndex(this.bracket1, 0);
      this.Controls.SetChildIndex(this.lbl_SetupID, 0);
      this.Controls.SetChildIndex(this.lbl_ModID, 0);
      this.Controls.SetChildIndex(this.lbl_Misc0, 0);
      this.Controls.SetChildIndex(this.lbl_Misc2, 0);
      this.Controls.SetChildIndex(this.lbl_Misc1, 0);
      this.Controls.SetChildIndex(this.tlb_desired0, 0);
      this.Controls.SetChildIndex(this.tlb_produced0, 0);
      this.Controls.SetChildIndex(this.tlb_desired1, 0);
      this.Controls.SetChildIndex(this.tlb_produced1, 0);
      this.Controls.SetChildIndex(this.tlb_desired2, 0);
      this.Controls.SetChildIndex(this.tlb_produced2, 0);
      this.Controls.SetChildIndex(this.lbl_Customer2, 0);
      this.Controls.SetChildIndex(this.lbl_Customer0, 0);
      this.Controls.SetChildIndex(this.lbl_Customer1, 0);
      this.Controls.SetChildIndex(this.pct_OrderPreprint0, 0);
      this.Controls.SetChildIndex(this.pct_OrderPreprint2, 0);
      this.Controls.SetChildIndex(this.pct_OrderPreprint1, 0);
      this.Controls.SetChildIndex(this.lbl_filter, 0);
      this.Controls.SetChildIndex(this.btn_ShowGraph, 0);
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
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint0)).EndInit();
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lbl_Customer1;
		private System.Windows.Forms.Label lbl_Customer0;
		private System.Windows.Forms.Label lbl_Customer2;
		private SyncroC.Controls.TitledLabel tlb_bad2;
		private SyncroC.Controls.TitledLabel tlb_bad1;
		private SyncroC.Controls.TitledLabel tlb_bad0;
		private SyncroC.Controls.TitledLabel tlb_produced2;
		private SyncroC.Controls.TitledLabel tlb_desired2;
		private SyncroC.Controls.TitledLabel tlb_produced1;
		private SyncroC.Controls.TitledLabel tlb_desired1;
		private SyncroC.Controls.TitledLabel tlb_produced0;
		private SyncroC.Controls.TitledLabel tlb_desired0;
		private System.Windows.Forms.Label lbl_Misc1;
		private System.Windows.Forms.Label lbl_Misc2;
		private System.Windows.Forms.Label lbl_Misc0;
		private System.Windows.Forms.Label lbl_ModID;
		private System.Windows.Forms.Label lbl_SetupID;
		private SyncroC.Controls.Bracket bracket1;
		private SyncroC.Controls.Title ttl_Order0;
		private SyncroC.Controls.GridView gdv_Orders;
		private SyncroC.Controls.Title lbl_DryEndTitle;
		private SyncroC.Controls.Title ttl_Order2;
		private SyncroC.Controls.Title ttl_Order1;
        private System.Windows.Forms.PictureBox pct_OrderPreprint1;
        private System.Windows.Forms.PictureBox pct_OrderPreprint2;
        private System.Windows.Forms.PictureBox pct_OrderPreprint0;
        private SyncroC.Controls.myLabel lbl_filter;
        private System.Windows.Forms.Button btn_ShowGraph;
        private SyncroC.Controls.TitledNumericUpDown nud_ScrapDischargeGroup0;
        private SyncroC.Controls.TitledNumericUpDown nud_ScrapDischargeGroup1;
        private SyncroC.Controls.TitledNumericUpDown nud_ScrapDischargeGroup2;
	}
}